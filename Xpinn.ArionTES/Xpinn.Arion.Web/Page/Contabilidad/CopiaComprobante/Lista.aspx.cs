using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;

public partial class Lista : GlobalWeb
{
    Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(ComprobanteServicio.CodigoProgramaCopia, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            //toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDDList();
                CargarValoresConsulta(pConsulta, ComprobanteServicio.GetType().Name);
                if (Session[ComprobanteServicio.GetType().Name + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "Page_Load", ex);
        }
    }

    private void CargarDDList()
    {
        Xpinn.Contabilidad.Services.TipoComprobanteService TipoComprobanteService = new Xpinn.Contabilidad.Services.TipoComprobanteService();
        Xpinn.Contabilidad.Entities.TipoComprobante TipoComprobante = new Xpinn.Contabilidad.Entities.TipoComprobante();
        ddlTipoComprobante.DataSource = TipoComprobanteService.ListarTipoComprobante(TipoComprobante, "", (Usuario)Session["Usuario"]);
        ddlTipoComprobante.DataTextField = "descripcion";
        ddlTipoComprobante.DataValueField = "tipo_comprobante";
        ddlTipoComprobante.DataBind();
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GuardarValoresConsulta(pConsulta, ComprobanteServicio.GetType().Name);
            Navegar(Pagina.Nuevo);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "btnNuevo_Click", ex);
        }

    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GuardarValoresConsulta(pConsulta, ComprobanteServicio.GetType().Name);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "btnConsultar_Click", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ComprobanteServicio.GetType().Name);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnEliminar");
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.SelectedRow.Cells[2].Text;
        Session[ComprobanteServicio.CodigoProgramaCopia + ".num_comp"] = id;
        id = gvLista.SelectedRow.Cells[3].Text;
        Session[ComprobanteServicio.CodigoProgramaCopia + ".tipo_comp"] = id;   
        Response.Redirect("~/Comprobante/Nuevo.aspx");
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
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Contabilidad.Entities.Comprobante> lstConsulta = new List<Xpinn.Contabilidad.Entities.Comprobante>();
            string sFiltro = "",Orden = "";
            lstConsulta = ComprobanteServicio.ListarComprobantes(ObtenerValores(), (Usuario)Session["usuario"], sFiltro, Orden);

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

            Session.Add(ComprobanteServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private Xpinn.Contabilidad.Entities.Comprobante ObtenerValores()
    {
        Xpinn.Contabilidad.Entities.Comprobante Comprobante = new Xpinn.Contabilidad.Entities.Comprobante();

        try
        {
            if (txtNumComp.Text.Trim() != "")
                Comprobante.num_comp = Convert.ToInt64(txtNumComp.Text.Trim());
            if (ddlTipoComprobante.SelectedValue != null)
                Comprobante.tipo_comp = Convert.ToInt64(ddlTipoComprobante.SelectedValue);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "ObtenerValores", ex);
        }

        return Comprobante;
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String num;
        String tipo;
        num = gvLista.Rows[e.NewEditIndex].Cells[1].Text;
        Session[ComprobanteServicio.CodigoProgramaCopia + ".num_comp"] = num;
        tipo = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[ComprobanteServicio.CodigoProgramaCopia + ".tipo_comp"] = tipo;
        string fecha = gvLista.Rows[e.NewEditIndex].Cells[3].Text;
        Session["Comprobantecopia"] = num;
        Response.Redirect("~/Page/Contabilidad/Comprobante/Nuevo.aspx");
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long idObjeto1 = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[3].Text);
            //cajaServicio.EliminarOficina(idObjeto1, (Usuario) Session["usuario"]);//, (UserSession)Session["user"]);
            Actualizar();
            Navegar(Pagina.Lista);
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "gvLista_RowDeleting", ex);
        }
    }


    protected void ddlTipoComprobante_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}