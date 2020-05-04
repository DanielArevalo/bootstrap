using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    ParametrizacionProcesoAfilicacionService _paramProceso = new ParametrizacionProcesoAfilicacionService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(_paramProceso.CodigoPrograma, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGrabar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_paramProceso.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarParametrosProceso();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_paramProceso.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void CargarParametrosProceso()
    {
        List<ParametrizacionProcesoAfiliacion> lstParamProceso = new List<ParametrizacionProcesoAfiliacion>();
        lstParamProceso = _paramProceso.ListarParametrosProcesoAfiliacion((Usuario)Session["Usuario"]);
        gvLista.DataSource = lstParamProceso;
        gvLista.DataBind();
    }
    protected void ddlOrden_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlOrden = (DropDownList)sender;

        if (ddlOrden != null)
        {
            GridViewRow rFila = (GridViewRow)ddlOrden.NamingContainer;
            Int32 order = ddlOrden.SelectedValue != " " ? Convert.ToInt32(ddlOrden.SelectedValue) : Convert.ToInt32(0);
            List<ParametrizacionProcesoAfiliacion> listaActual = new List<ParametrizacionProcesoAfiliacion>();
            List<ParametrizacionProcesoAfiliacion> listaNueva = new List<ParametrizacionProcesoAfiliacion>();
            listaActual = ObtenerListaGridView();
            Int32 cntrl = 0;
            bool siga = true;
            while (siga == true)
            {
                foreach (ParametrizacionProcesoAfiliacion viewTime in listaActual)
                {
                    if (viewTime.orden == 1 && cntrl == 0)
                    {
                        listaNueva.Add(viewTime);
                        cntrl += 1;
                        siga = true;
                    }
                    else  if (viewTime.orden == 2 && cntrl == 1) {
                        listaNueva.Add(viewTime);
                        cntrl += 1;
                        siga = true;
                    }
                    else if (viewTime.orden == 3 && cntrl == 2){
                        listaNueva.Add(viewTime);
                        cntrl += 1;
                        siga = true;
                    }
                    else if (viewTime.orden == 4 && cntrl == 3){
                        listaNueva.Add(viewTime);
                        cntrl += 1;
                        siga = true;
                    }
                    else if (viewTime.orden == 5 && cntrl == 4){
                        listaNueva.Add(viewTime);
                        cntrl += 1;
                        siga = true;
                    }
                    else if (viewTime.orden == 6 && cntrl == 5){
                        listaNueva.Add(viewTime);
                        cntrl += 1;
                        siga = true;
                    }
                    else if (viewTime.orden == 7 && cntrl == 6)
                    {
                        listaNueva.Add(viewTime);
                        cntrl += 1;
                        siga = true;
                    }
                    else if (viewTime.orden == 8 && cntrl == 7)
                    {
                        listaNueva.Add(viewTime);
                        cntrl += 1;
                        siga = true; 
                    }
                    else { siga = false; }
                }
            }
            foreach (ParametrizacionProcesoAfiliacion withoutOrder in listaActual)
            {
                if (!listaNueva.Exists(x => x.cod_proceso == withoutOrder.cod_proceso))
                {
                    listaNueva.Add(withoutOrder);
                }
            }
            gvLista.DataSource = listaNueva;
            gvLista.DataBind();
        }
    }
    List<ParametrizacionProcesoAfiliacion> ObtenerListaGridView()
    {
        List<ParametrizacionProcesoAfiliacion> vParamProceso = new List<ParametrizacionProcesoAfiliacion>();
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            ParametrizacionProcesoAfiliacion cParamProceso = new ParametrizacionProcesoAfiliacion();

            cParamProceso.cod_proceso = Convert.ToInt32(rFila.Cells[0].Text);
            cParamProceso.nombre = rFila.Cells[1].Text;

            CheckBox chbRequerido = (CheckBox)rFila.Cells[2].FindControl("chbRequerido");
            if (chbRequerido != null)
            {
                if (chbRequerido.Checked == true)
                    cParamProceso.requerido = chbRequerido.Checked ? 1 : 0;
            }

            CheckBox chbNotAsociado = (CheckBox)rFila.Cells[3].FindControl("chbNotAsociado");
            if (chbNotAsociado != null)
            {
                if (chbNotAsociado.Checked == true)
                    cParamProceso.asociado = chbNotAsociado.Checked ? 1 : 0;
            }

            CheckBox chbNotAsesor = (CheckBox)rFila.Cells[4].FindControl("chbNotAsesor");
            if (chbNotAsesor != null)
            {
                if (chbNotAsesor.Checked == true)
                    cParamProceso.asesor = chbNotAsesor.Checked ? 1 : 0;
            }

            TextBox txtNotOtro = (TextBox)rFila.FindControl("txtNotOtro");
            if (txtNotOtro != null)
            {
                cParamProceso.otro = Convert.ToString(txtNotOtro.Text);
            }

            DropDownListGrid ddlOrden = (DropDownListGrid)rFila.Cells[5].FindControl("ddlOrden");
            if (ddlOrden.SelectedValue != " ")
            {
                cParamProceso.orden = Convert.ToInt32(ddlOrden.SelectedValue);
            }else { cParamProceso.orden = 0; }
            vParamProceso.Add(cParamProceso);
        }
        return vParamProceso;
    }
    /// <summary>
    /// Crear los datos del perfil
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGrabar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ParametrizacionProcesoAfiliacion vParam = new ParametrizacionProcesoAfiliacion();
            vParam.lstParametros = ObtenerListaGridView();
            foreach (ParametrizacionProcesoAfiliacion eachParam in vParam.lstParametros)
            {
                _paramProceso.ModificarParametros(eachParam, (Usuario)Session["usuario"]);
            }
            CargarParametrosProceso();
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_paramProceso.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlOrden = (DropDownList)e.Row.FindControl("ddlOrden");

            if (ddlOrden != null)
            {
                ddlOrden.Items.Insert(0, new ListItem("<Seleccione un Item>", " "));
                ddlOrden.Items.Insert(1, new ListItem("1", "1"));
                ddlOrden.Items.Insert(2, new ListItem("2", "2"));
                ddlOrden.Items.Insert(3, new ListItem("3", "3"));
                ddlOrden.Items.Insert(4, new ListItem("4", "4"));
                ddlOrden.Items.Insert(5, new ListItem("5", "5"));
                ddlOrden.Items.Insert(6, new ListItem("6", "6"));
                ddlOrden.Items.Insert(7, new ListItem("7", "7"));
                ddlOrden.Items.Insert(8, new ListItem("8", "8"));
                ddlOrden.DataBind();
            }
            Label lblOrden = (Label)e.Row.FindControl("lblOrden");
            if (lblOrden != null)
            {
                ddlOrden.SelectedValue = lblOrden.Text;
            }
        }
    }

}