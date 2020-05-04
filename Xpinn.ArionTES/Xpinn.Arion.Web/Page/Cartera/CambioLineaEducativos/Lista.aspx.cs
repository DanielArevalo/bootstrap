using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Cartera.Services;
using Xpinn.FabricaCreditos.Entities;

public partial class Lista : GlobalWeb
{

    CambioLineaService CambioLineaServicio = new CambioLineaService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {            
            VisualizarOpciones(CambioLineaServicio.CodigoPrograma, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CambioLineaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarlistas();
                CargarValoresConsulta(pConsulta, CambioLineaServicio.GetType().Name);
                if (Session[CambioLineaServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
                    
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CambioLineaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();

        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, CambioLineaServicio.CodigoPrograma);
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
        LimpiarValoresConsulta(pConsulta, CambioLineaServicio.CodigoPrograma);
    }

    private void Actualizar()
    {
        try
        {
            List<Credito> lstConsulta = new List<Credito>();
            Credito credito = new Credito();
            String filtro = obtFiltro(ObtenerValores());
            lstConsulta = CambioLineaServicio.ListarCreditos((Usuario)Session["usuario"], filtro);

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

            Session.Add(CambioLineaServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            // BOexcepcion.Throw(CambioLineaServicio.CodigoPrograma, "Actualizar", ex);
            VerError("Error al consultar. " + ex.Message);
        }
    }

    private Credito ObtenerValores()
    {
        Credito credito = new Credito();
        if (txtCodDeudor.Text.Trim() != "")
            credito.cod_deudor = Convert.ToInt64(txtCodDeudor.Text.Trim());
        if (txtCredito.Text.Trim() != "")
            credito.numero_radicacion = Convert.ToInt64(txtCredito.Text.Trim());
        if (txtIdentificacion.Text.Trim() != "")
            credito.identificacion = Convert.ToString(txtIdentificacion.Text.Trim());
        return credito;
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.SelectedRow.Cells[1].Text;
        Session[CambioLineaServicio.CodigoPrograma + ".id"] = id;
        Session[CambioLineaServicio.CodigoPrograma + ".from"] = "l";
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
            BOexcepcion.Throw(CambioLineaServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private string obtFiltro(Credito credito)
    {
        String filtro = String.Empty;
        filtro = " and (v_creditos.estado = 'C') ";

        if (txtCodDeudor.Text.Trim() != "")
            filtro += " and cod_deudor =" + credito.cod_deudor;

        if (txtCredito.Text.Trim() != "")
            filtro += " and numero_radicacion=" + credito.numero_radicacion;
        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and identificacion like '%" + credito.identificacion + "%'";
        if (txtCodigoNomina.Text != "")
            filtro += " and cod_nomina like '%" + txtCodigoNomina.Text + "%'";
        if (ddloficina.Visible == true)
        {
            if (ddloficina.SelectedIndex != 0)
                filtro += " and cod_oficina = " + ddloficina.SelectedValue + "";
        }
        
        return filtro;
       
    }
}