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
    ComprobanteService ComprobanteServicio = new ComprobanteService();
    Usuario _usuario;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(ComprobanteServicio.CodigoProgramaModif, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
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
            _usuario = (Usuario)Session["Usuario"];
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
        ddlTipoComprobante.DataSource = TipoComprobanteService.ListarTipoComprobante(TipoComprobante, "", _usuario);
        ddlTipoComprobante.DataTextField = "descripcion";
        ddlTipoComprobante.DataValueField = "tipo_comprobante";
        ddlTipoComprobante.DataBind();
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
        Session.Remove(ComprobanteServicio.GetType().Name + ".consulta");
        LimpiarValoresConsulta(pConsulta, ComprobanteServicio.GetType().Name);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnEliminar");
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!ValidarAccionesGrilla("UPDATE"))
            return;
        TipoComprobanteService TipoCompServicio = new TipoComprobanteService();

        Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = gvLista.SelectedRow.Cells[1].Text;

        String id = gvLista.SelectedRow.Cells[2].Text;
        Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = id;
        Session[ComprobanteServicio.CodigoPrograma + ".modificar"] = "1";

        TipoComprobante tipComprobante = TipoCompServicio.ConsultarTipoComprobante(Convert.ToInt64(id), _usuario);
        DateTime dtUltCierre = DateTime.MinValue;
        bool requiereSegundaValidacion = false;
        DateTime dtUltCierreSegundaValidacion = DateTime.MinValue;

        try
        {
            if (tipComprobante.tipo_norma == null || tipComprobante.tipo_norma == (int)TipoNormaComprobante.Local)
            {
                dtUltCierre = Convert.ToDateTime(ComprobanteServicio.Consultafecha(_usuario));
            }
            else if (tipComprobante.tipo_norma == (int)TipoNormaComprobante.Niff)
            {
                dtUltCierre = Convert.ToDateTime(ComprobanteServicio.Consultafecha(_usuario, "'G'"));
            }
            else if (tipComprobante.tipo_norma == (int)TipoNormaComprobante.Local_Niff)
            {
                dtUltCierre = Convert.ToDateTime(ComprobanteServicio.Consultafecha(_usuario));
                dtUltCierreSegundaValidacion = Convert.ToDateTime(ComprobanteServicio.Consultafecha(_usuario, "'G'"));
                requiereSegundaValidacion = true;
            }
        }
        catch (Exception)
        {
            VerError("No se encontro la fecha del último cierre contable");
            return;
        }

        try
        {
            DateTime fechaComp = Convert.ToDateTime(gvLista.SelectedRow.Cells[3].Text);
            if (fechaComp <= dtUltCierre)
            {
                VerError("No puede modificar comprobantes en períodos ya cerrados. Fecha Ultimo Cierre:" + dtUltCierre.ToShortDateString());
                return;
            }
            if (requiereSegundaValidacion && fechaComp <= dtUltCierreSegundaValidacion)
            {
                VerError("No puede modificar comprobantes en períodos ya cerrados. Fecha Ultimo Cierre:" + dtUltCierreSegundaValidacion.ToShortDateString());
                return;
            }
        }
        catch { }
        Session.Remove(ComprobanteServicio.GetType().Name + ".consulta");
        Response.Redirect("~/Page/Contabilidad/Comprobante/Nuevo.aspx", false);

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
            Configuracion conf = new Configuracion();
            List<Comprobante> lstConsulta = new List<Comprobante>();
            string sFiltro = " ", Orden = "";
            if (txtFechaIni.TieneDatos)
                if (txtFechaIni.ToDate.Trim() != "")
                    sFiltro = sFiltro + " And fecha >= To_Date('" + txtFechaIni.ToDate.Trim() + "', '" + conf.ObtenerFormatoFecha() + "')";
            if (txtFechaFin.TieneDatos)
                if (txtFechaFin.ToDate.Trim() != "")
                    sFiltro = sFiltro + " And fecha <= To_Date('" + txtFechaFin.ToDate.Trim() + "', '" + conf.ObtenerFormatoFecha() + "')";
            sFiltro = sFiltro + " And fecha >= (Select Max(fecha) From cierea Where tipo = 'C' and estado = 'D')";

            lstConsulta = ComprobanteServicio.ListarComprobantes(ObtenerValores(), _usuario, sFiltro, Orden);

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

    private Comprobante ObtenerValores()
    {
        Comprobante Comprobante = new Comprobante();

        try
        {
            if (txtNumComp.Text.Trim() != "")
                Comprobante.num_comp = Convert.ToInt64(txtNumComp.Text.Trim());
            if (ddlTipoComprobante.SelectedValue != null && ddlTipoComprobante.SelectedIndex != 0)
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
        String id;
        id = gvLista.Rows[e.NewEditIndex].Cells[1].Text;
        Session[ComprobanteServicio.CodigoProgramaModif + ".num_comp"] = id;
        id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[ComprobanteServicio.CodigoProgramaModif + ".tipo_comp"] = id;

        string fecha = gvLista.Rows[e.NewEditIndex].Cells[3].Text;

        if (Convert.ToDateTime(fecha) > Convert.ToDateTime(ComprobanteServicio.Consultafecha(_usuario)))
            Navegar(Pagina.Nuevo);
    }


}