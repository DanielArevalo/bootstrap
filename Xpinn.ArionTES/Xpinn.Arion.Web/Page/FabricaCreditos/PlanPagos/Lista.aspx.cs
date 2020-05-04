using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;

public partial class Lista : GlobalWeb
{
    CreditoPlanService creditoPlanServicio = new CreditoPlanService();
    private Xpinn.Asesores.Entities.Producto producto;
    Int64 plinea_credito = 0;
    String pcodlinea = "";
    String IdPersona = "";

    private Usuario usuario = new Usuario();

    Xpinn.Asesores.Services.EstadoCuentaService serviceEstadoCuenta = new Xpinn.Asesores.Services.EstadoCuentaService();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(creditoPlanServicio.CodigoPrograma, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, creditoPlanServicio.CodigoPrograma);
                string dedondeviene = Request.UrlReferrer.ToString();
                if (dedondeviene.Contains("PlanPagos/Lista.aspx"))
                    Session.Remove(creditoPlanServicio.CodigoPrograma + ".consulta");
                if (Session[creditoPlanServicio.CodigoPrograma + ".consulta"] != null)
                    
                Session["boton"] = 0;
            }
        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        if (txtCredito.Text != "" || txtIdentificacion.Text != "")
        {
            Page.Validate();


            if (Page.IsValid)
            {
                GuardarValoresConsulta(pConsulta, creditoPlanServicio.CodigoPrograma);
                Actualizar();
            }

        }
        else 
        {
            VerError("Debe filtrar por alguno de los dos campos.");
            return;
        }
        
       
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, creditoPlanServicio.CodigoPrograma);
    }

    /// <summary>
    /// Evento para cargar valores a la grilla.
    /// </summary>
    private void Actualizar()
    {
        Int64 plinea = 0;
      
        try
        {
            VerError("");

            Session["TipoLinea"] = null;
            List<CreditoPlan> lstConsulta = new List<CreditoPlan>();
            CreditoPlan credito = new CreditoPlan();
            String filtro = obtFiltro(ObtenerValores());
            lstConsulta = creditoPlanServicio.ListarCredito(credito, (Usuario)Session["usuario"], filtro);
          
            gvLista.DataSource = lstConsulta;
            foreach (CreditoPlan creditos in lstConsulta)
            {
                plinea = creditos.tipo_linea;
                pcodlinea = creditos.Linea;
                Int64 persona = creditos.Cod_persona;
                Session[MOV_GRAL_CRED_PRODUC] = producto;
                Session["TipoLinea"] = plinea;
            }
            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            }

            Session.Add(creditoPlanServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "Actualizar", ex);
        }
    }


    /// <summary>
    /// Esta función permite mostrar el plan de pagos del crédito seleccionado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lblNumeroSolicitud = (Label)gvLista.SelectedRow.FindControl("lblNumeroSolicitud");
        Session["NumeroSolicitud"] = lblNumeroSolicitud.Text;
        String id = gvLista.SelectedRow.Cells[1].Text;
        Session[creditoPlanServicio.CodigoPrograma + ".id"] = id;
        Session[creditoPlanServicio.CodigoPrograma + ".from"] = "l";
        Session[creditoPlanServicio.CodigoPrograma + ".origen"] = null;
        plinea_credito = ConvertirStringToInt32(gvLista.SelectedRow.Cells[10].Text); // Convert.ToInt64( Session["TipoLinea"]);

        if (plinea_credito == 1)
        { 
            Navegar(Pagina.Detalle);
        }
        if (plinea_credito == 2)
        {
            producto = (Xpinn.Asesores.Entities.Producto)(Session[MOV_GRAL_CRED_PRODUC]);
            List<Xpinn.Asesores.Entities.Producto> lstConsulta = new List<Xpinn.Asesores.Entities.Producto>();
            //falta definir si se lleva a calcular pagos o simplemente no se muestra plan de pagos
            // Navegar("~/Page/Asesores/EstadoCuenta/Creditos/Pago/Detalle.aspx");
        }
      
    }


    /// <summary>
    /// Esta función actualiza la grilla de créditos al ir a la siguiente página de datos de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Evento para obtener los filtros ingresados por el usuario para realizar la consulta
    /// </summary>
    /// <param name="credito">Clase que tiene los datos del filtro</param>
    /// <returns>Retorna los filtros a aplicar</returns>
    private string obtFiltro(CreditoPlan credito)
    {
        String filtro = String.Empty;
        if (txtCredito.Text.Trim() != "")
            filtro += " and numero_radicacion=" + credito.Numero_radicacion;
        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and identificacion='" +txtIdentificacion.Text+"'";

        return filtro;
    }

    private CreditoPlan ObtenerValores()
    {
        CreditoPlan credito = new CreditoPlan();
        if (txtCredito.Text.Trim() != "")
            credito.Numero_radicacion = Convert.ToInt64(txtCredito.Text.Trim());
        return credito;
    }

}