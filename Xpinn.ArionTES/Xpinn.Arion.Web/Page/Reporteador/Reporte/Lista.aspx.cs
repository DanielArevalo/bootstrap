using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Reporteador.Services;
using Xpinn.Reporteador.Entities;

public partial class Lista : GlobalWeb
{
    Xpinn.Reporteador.Services.ReporteService reporteServicio = new Xpinn.Reporteador.Services.ReporteService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(reporteServicio.CodigoProgramaRep, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, reporteServicio.GetType().Name);
                if (Session[reporteServicio.GetType().Name + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.GetType().Name + "L", "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GuardarValoresConsulta(pConsulta, reporteServicio.GetType().Name);
            Navegar(Pagina.Nuevo);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.GetType().Name + "L", "btnNuevo_Click", ex);
        }

    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GuardarValoresConsulta(pConsulta, reporteServicio.GetType().Name);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.GetType().Name + "L", "btnConsultar_Click", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, reporteServicio.GetType().Name);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnEliminar");
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.SelectedRow.Cells[2].Text;
        Session[reporteServicio.CodigoProgramaRep + ".idreporte"] = id;
        Response.Redirect("Nuevo.aspx", false);
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
            BOexcepcion.Throw(reporteServicio.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            Configuracion conf = new Configuracion();
            List<Xpinn.Reporteador.Entities.Reporte> lstConsulta = new List<Xpinn.Reporteador.Entities.Reporte>();
            lstConsulta = reporteServicio.ListarReporte(ObtenerValores(), (Usuario)Session["usuario"]);

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

            Session.Add(reporteServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private Xpinn.Reporteador.Entities.Reporte ObtenerValores()
    {
        Xpinn.Reporteador.Entities.Reporte eReporte = new Xpinn.Reporteador.Entities.Reporte();

        try
        {
            if (txtCodigo.Text.Trim() != "")
                eReporte.idreporte = Convert.ToInt64(txtCodigo.Text.Trim());
            if (ddlTipoReporte.SelectedValue != null && ddlTipoReporte.SelectedIndex != 0)
                eReporte.tipo_reporte = Convert.ToInt32(ddlTipoReporte.SelectedValue);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.GetType().Name + "L", "ObtenerValores", ex);
        }

        return eReporte;
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id;
        id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[reporteServicio.CodigoProgramaRep + ".idreporte"] = id;

        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deleted")
        {
            try
            {
                int RowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                int codReporte = Convert.ToInt32(gvLista.Rows[RowIndex].Cells[2].Text);
                reporteServicio.EliminarReporte(codReporte, (Usuario)Session["usuario"]);//, (UserSession)Session["user"]);
                Actualizar();
                Navegar(Pagina.Lista);
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(reporteServicio.GetType().Name + "L", "gvLista_RowCommand", ex);
            }
        }
    }

 
}