using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;

public partial class Lista : GlobalWeb
{

    CreditoSolicitadoService creditoServicio = new CreditoSolicitadoService();
    ControlTiempos control = new ControlTiempos();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {            
            VisualizarOpciones(creditoServicio.CodigoPrograma, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarlistas();
                CargarValoresConsulta(pConsulta, creditoServicio.GetType().Name);
                if (Session[creditoServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
                    
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();

        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, creditoServicio.CodigoPrograma);
            Actualizar();
        }
    }

    protected void cargarlistas()
    {
        Xpinn.FabricaCreditos.Services.OficinaService oficinaService = new Xpinn.FabricaCreditos.Services.OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
        Usuario usuap = (Usuario)Session["usuario"];

        try
        {            
            int cod = Convert.ToInt32(usuap.codusuario);
            int consulta = 0;        
            consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas(cod, (Usuario)Session["Usuario"]);
            ddloficina.Visible = true;
            lbloficina.Visible = true;
            if (consulta >= 1)
            {
                ddloficina.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);
                ddloficina.DataTextField = "nombre";
                ddloficina.DataValueField = "codigo";
                ddloficina.DataBind();
                ddloficina.SelectedValue = Convert.ToString(usuap.cod_oficina);
                ddloficina.Enabled = true;
                ddloficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            }
            else
            {            
                ddloficina.Items.Insert(1, new ListItem(Convert.ToString(usuap.nombre_oficina), Convert.ToString(usuap.cod_oficina)));
                ddloficina.DataBind();
                ddloficina.SelectedValue = Convert.ToString(usuap.cod_oficina);
                ddloficina.Enabled = false;
                ddloficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            }
        }
        catch
        {
            ddloficina.Visible = false;
            lbloficina.Visible = false;
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, creditoServicio.CodigoPrograma);
    }

    private void Actualizar()
    {
        try
        {
            List<CreditoSolicitado> lstConsulta = new List<CreditoSolicitado>();
            CreditoSolicitado credito = new CreditoSolicitado();
            String filtro = obtFiltro(ObtenerValores());
            lstConsulta = creditoServicio.ListarCreditos(credito, (Usuario)Session["usuario"], filtro);

            gvLista.DataSource = lstConsulta;
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

            Session.Add(creditoServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            // BOexcepcion.Throw(creditoServicio.CodigoPrograma, "Actualizar", ex);
            VerError("Error al consultar. " + ex.Message);
        }
    }

    private CreditoSolicitado ObtenerValores()
    {
        CreditoSolicitado credito = new CreditoSolicitado();
        if (txtCredito.Text.Trim() != "")
            credito.NumeroCredito = Convert.ToInt64(txtCredito.Text.Trim());
        if (txtIdentificacion.Text.Trim() != "")
            credito.Identificacion = Convert.ToString(txtIdentificacion.Text.Trim());
        return credito;
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.SelectedRow.Cells[1].Text;
        String codigolinea = gvLista.SelectedRow.Cells[8].Text;
        Session["CodLinea"] = codigolinea;
        Session[creditoServicio.CodigoPrograma + ".id"] = id;
        Session[creditoServicio.CodigoPrograma + ".from"] = "l";
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private string obtFiltro(CreditoSolicitado credito)
    {
        String filtro = String.Empty;       

        //Agregado para consultar proceso anterior a Aprobación según la parametrización de la entidad        
        string estado = "Aprobado";
        control = creditoServicio.ConsultarProcesoAnterior(estado, (Usuario)Session["usuario"]);
        if(control.estado != null && control.estado != "")
            filtro += " and c.estado='" + control.estado + "'";
        else
            filtro = " and (c.estado = 'L' Or c.estado = 'V' Or c.estado = 'W' Or c.estado = 'S') ";

        if (txtCredito.Text.Trim() != "")
            filtro += " and c.numero_radicacion=" + credito.NumeroCredito;
        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and p.identificacion like '%" + credito.Identificacion + "%'";
        if (ddloficina.Visible == true)
        {
            if (ddloficina.SelectedIndex != 0)
                filtro += " and c.cod_oficina = " + ddloficina.SelectedValue + "";
        }
        if (txtFecSolicitud.Text != "")
        {
            filtro += " and trunc(c.fecha_solicitud) = to_Date('" + txtFecSolicitud.Text + "','" + gFormatoFecha + "')";
        }
        if (txtCodigoNomina.Text != "")
            filtro += " and p.cod_nomina like '%" + txtCodigoNomina.Text + "%'";

        string Orden =  "  Order by c.numero_radicacion";
        filtro += Orden;
        return filtro;
    }
}