using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Configuration;
using Xpinn.Cartera.Services;
using Xpinn.Cartera.Entities;

public partial class Lista : GlobalWeb
{
    ExcelService excelServicio = new ExcelService();
    CuadreCarteraService cuadreServicio = new CuadreCarteraService();
    Usuario usuario = new Usuario();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(cuadreServicio.CodigoProgramaHistorico, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.MostrarExportar(false);
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cuadreServicio.CodigoProgramaHistorico, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                panelGrilla.Visible = false;
                CargarDropDown();
                CargarValoresConsulta(pConsulta, cuadreServicio.CodigoProgramaHistorico);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cuadreServicio.CodigoProgramaHistorico, "Page_Load", ex);
        }
    }

    void CargarDropDown()
    {
        Xpinn.Caja.Services.TipoOperacionService tipoopeservices = new Xpinn.Caja.Services.TipoOperacionService();
        List<Xpinn.Caja.Entities.TipoOperacion> lsttipo = new List<Xpinn.Caja.Entities.TipoOperacion>();
        lsttipo = tipoopeservices.ListarTipoProducto((Usuario)Session["Usuario"]);
        ddlTipoProducto.DataTextField = "nom_tipo_producto";
        ddlTipoProducto.DataValueField = "tipo_producto";
        ddlTipoProducto.DataSource = lsttipo.Where(x => x.tipo_producto == 1 || x.tipo_producto == 2 || x.tipo_producto == 3 || x.tipo_producto == 5 || x.tipo_producto == 9).ToList();
        ddlTipoProducto.AppendDataBoundItems = true;
        ddlTipoProducto.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlTipoProducto.SelectedIndex = 0;
        ddlTipoProducto.DataBind();

        Xpinn.FabricaCreditos.Services.LineasCreditoService LineasServicios = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        List<LineasCredito> lstAtributos = new List<LineasCredito>();
        lstAtributos = LineasServicios.ddlatributo((Usuario)Session["Usuario"]);
        ddlAtributo.DataSource = lstAtributos.Where(x => x.cod_atr == 1 || x.cod_atr == 2).ToList();
        ddlAtributo.DataTextField = "nombre";
        ddlAtributo.DataValueField = "cod_atr";
        ddlAtributo.AppendDataBoundItems = true;
        ddlAtributo.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlAtributo.SelectedIndex = 0;
        ddlAtributo.DataBind();
        ddlAtributo.SelectedValue = "1";
    }

    /// <summary>
    /// Método que permite generar el reporte segùn las condiciones dadas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Page.Validate();
        if (Page.IsValid)
        {

            if (txtFechaCierre.Text == "")
            {
                VerError("Ingrese el rango de fechas");
                txtFechaCierre.Focus();
                return;
            }
            if (ddlTipoProducto.SelectedIndex == 0)
            {
                VerError("Seleccione el tipo de producto");
                ddlTipoProducto.Focus();
                return;
            }
            if (ddlAtributo.SelectedIndex == 0)
            {
                VerError("Seleccione el atributo");
                ddlAtributo.Focus();
                return;
            }
            GuardarValoresConsulta(pConsulta, cuadreServicio.CodigoProgramaHistorico);
            Actualizar();
        }
    }

    /// <summary>
    /// Méotodo para limpiar datos de la conuslta
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        LimpiarValoresConsulta(pConsulta, cuadreServicio.CodigoProgramaHistorico);
        panelGrilla.Visible = false;
        gvReporte.DataSource = null;
        gvReporte.DataBind();
        txtFechaCierre.Text = "";
        lblTotalRegs.Text = "";
        ddlAtributo.SelectedIndex = 0;
    }

    protected void gvReporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvReporte.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cuadreServicio.CodigoProgramaHistorico, "gvReporte_PageIndexChanging", ex);
        }
    }


    /// <summary>
    /// Método para traer los datos según las condiciones ingresadas
    /// </summary>
    private void Actualizar()
    {
        Configuracion conf = new Configuracion();
        VerError("");
        try
        {
            List<CuadreHistorico> lstConsulta = new List<CuadreHistorico>();
            CuadreCartera pCuadre = new CuadreCartera();
            pCuadre.fecha = Convert.ToDateTime(txtFechaCierre.Text);
            pCuadre.tipo_producto = Convert.ToInt64(ddlTipoProducto.SelectedValue);
            pCuadre.cod_atr = Convert.ToInt64(ddlAtributo.SelectedValue);

            lstConsulta = cuadreServicio.ConsultaCuadreHistorico(pCuadre, (Usuario)Session["usuario"]);
            gvReporte.EmptyDataText = emptyQuery;
            Site toolBar = (Site)this.Master;
            if (lstConsulta.Count > 0)
            {
                gvReporte.DataSource = lstConsulta;
                lblInfo.Visible = false;
                panelGrilla.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvReporte.DataBind();
                ValidarPermisosGrilla(gvReporte);
                toolBar.MostrarExportar(true);
                toolBar.MostrarGuardar(true);
            }
            else
            {
                gvReporte.DataSource = null;
                panelGrilla.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
                toolBar.MostrarExportar(false);
            }
        }
        catch (Exception ex)
        {
            VerError(ex.ToString());
        }
    }


    /// <summary>
    /// Método para obtener datos del filtro
    /// </summary>
    /// <returns></returns>
    private string obtFiltro()
    {
        String filtro = String.Empty;
        return filtro;
    }


    /// <summary>
    /// Método para exportar datos a excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (gvReporte.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvReporte.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvReporte);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=CuadreHistorico.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {        
        if (ddlAtributo.SelectedValue == "1")
            ctlMensaje.MostrarMensaje("¿Desea guardar los saldos finales?");
        else
            VerError("No se pueden actualizar los saldos para el atributo seleccionado");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        List<CuadreHistorico> lstSaldos = new List<CuadreHistorico>();

        foreach (GridViewRow rfila in gvReporte.Rows)
        {
            CuadreHistorico pCuadre = new CuadreHistorico();
            pCuadre.fecha = Convert.ToDateTime(rfila.Cells[0].Text);
            pCuadre.numero_radicacion = rfila.Cells[1].Text;
            StringHelper sHelper = new StringHelper();
            Configuracion config = new Configuracion();
            pCuadre.saldo_operativo = Convert.ToDecimal(rfila.Cells[5].Text.Replace(",", config.ObtenerSeparadorDecimalConfig()).Replace("$","").Replace(".", ""));
            if(pCuadre.saldo_operativo > 0)
                lstSaldos.Add(pCuadre);
        }
        cuadreServicio.ActualizarSaldoCierre(Convert.ToInt64(ddlTipoProducto.SelectedValue), lstSaldos, (Usuario)Session["usuario"]);
        Actualizar();
    }

}
