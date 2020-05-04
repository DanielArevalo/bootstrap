using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Comun.Services;
using Xpinn.Comun.Entities;

public partial class Lista : GlobalWeb
{
    private Usuario _usuario;
    private PoblarListas poblar = new PoblarListas();
    CiereaService CierreServicio = new CiereaService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(CierreServicio.CodigoProgramaC, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CierreServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["Usuario"];

            if (!IsPostBack)
            {
                CargarListas();
                mvControlCierres.ActiveViewIndex = 0;
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CierreServicio.CodigoProgramaC, "Page_Load", ex);
        }
    }

    protected void CargarListas()
    {
        poblar.PoblarListaDesplegable("PROCESO_CIERRE", ddlTipoCierre, _usuario);

        CiereaService ComunServicio = new CiereaService();
        ddlFechaPeriodo.Items.Clear();
        List<string> fechas = ComunServicio.ListarPeriodosCierres((Usuario)Session["usuario"]);
        fechas.Insert(0, "Seleccione un item");
        //ddlFechaPeriodo.DataValueField = "fecha";
        ddlFechaPeriodo.DataSource = fechas;
        ddlFechaPeriodo.DataBind();
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        GuardarValoresConsulta(pConsulta, CierreServicio.CodigoProgramaC);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        gvPendientes.DataSourceID = null;
        gvRealizados.DataSourceID = null;
        txtFechaCierre.Text = "";
        ddlFechaPeriodo.SelectedIndex = 0;
        ddlTipoCierre.SelectedIndex = 0;
    }

    protected string ObtenerFiltro()
    {
        string filtro = "";
        if (txtFechaCierre.Text != null && txtFechaCierre.Text != "")
        {
            if (filtro == "")
                filtro += " where TO_DATE(t.FECHA_REALIZACION) = TO_DATE('" + txtFechaCierre.Text + "','DD-MM-YYYY')";
        }
        if (ddlTipoCierre.SelectedIndex > 0)
        {
            if (filtro == "")
                filtro += " where t.COD_PROCESO = " + Convert.ToInt64(ddlTipoCierre.SelectedValue);
            else
                filtro += " and t.COD_PROCESO = " + Convert.ToInt64(ddlTipoCierre.SelectedValue);
        }
        if (ddlFechaPeriodo.SelectedIndex > 0)
        {
            if (filtro == "")
                filtro += " where TO_DATE(t.FECHA_PROCESO) = TO_DATE('" + ddlFechaPeriodo.Text + "','DD-MM-YYYY')";
            else
                filtro += " and TO_DATE(t.FECHA_PROCESO) = TO_DATE('" + ddlFechaPeriodo.Text + "','DD-MM-YYYY')";
        }
        return filtro;
    }

    protected void Actualizar()
    {
        CiereaService ComunServicio = new CiereaService();
        Cierea pCierre;
        List<Cierea> lstCierres = new List<Cierea>();

        if (chkRealizados.Checked == false)
        {
            mvControlCierres.ActiveViewIndex = 0;
            pCierre = ComunServicio.ConsultarSigCierre((Usuario)Session["usuario"]);
            if(pCierre.cod_proceso != 0)
                lstCierres.Add(pCierre);
            if (lstCierres.Count > 0)
            {
                gvPendientes.DataSource = lstCierres;
                gvPendientes.DataBind();
            }
        }
        else if (chkRealizados.Checked == true)
        {
            mvControlCierres.ActiveViewIndex = 1;
            lstCierres = ComunServicio.ListarControlCierres(ObtenerFiltro(), (Usuario)Session["usuario"]);
            if (lstCierres.Count > 0)
            {
                gvRealizados.DataSource = lstCierres;
                gvRealizados.DataBind();
            }
        }
    }

    protected void gvPendientes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink HyperConsulta = (HyperLink)e.Row.FindControl("HyperConsulta");
            ImageButton btnEditar = (ImageButton)e.Row.FindControl("btnEditar");
            if (mvControlCierres.ActiveViewIndex == 1)
            {
                if (btnEditar != null)
                    btnEditar.Visible = true;
                if (e.Row.Cells[2].Text != null && e.Row.Cells[2].Text != "")
                {
                    HyperConsulta.Visible = true;
                    HyperConsulta.NavigateUrl = e.Row.Cells[2].Text;
                }
            }
        }

        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CierreServicio.CodigoProgramaC + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvPendientes_RowEditing(object sender, GridViewEditEventArgs e)
    {
        HyperLink HyperConsulta = (HyperLink)gvPendientes.Rows[e.NewEditIndex].FindControl("HyperConsulta");
        ImageButton btnEditar = (ImageButton)gvPendientes.Rows[e.NewEditIndex].FindControl("btnEditar");
        Label lblRuta = (Label)gvPendientes.Rows[e.NewEditIndex].FindControl("lblRuta");
        HyperConsulta.Enabled = false;

        if (HyperConsulta != null)
        {
            if (HyperConsulta.Visible == true)
            {
                Response.Redirect(HyperConsulta.NavigateUrl, false);
                return;
            }
            Navegar("~/Page" + lblRuta.Text);
        }
    }

    protected void gvPendientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvPendientes.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CierreServicio.CodigoProgramaC, "gvPendientes_PageIndexChanging", ex);
        }
    }
}