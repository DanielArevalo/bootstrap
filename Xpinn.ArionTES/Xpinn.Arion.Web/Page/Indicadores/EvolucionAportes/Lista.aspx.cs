using System;
using System.Data;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Indicadores.Services;
using Xpinn.Indicadores.Entities;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Drawing.Printing;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;


partial class Nuevo : GlobalWeb
{
    private Xpinn.Indicadores.Services.IndicadoresAportesService IndicadoresAportesService = new Xpinn.Indicadores.Services.IndicadoresAportesService();
    private Usuario _usuario;
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[IndicadoresAportesService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(IndicadoresAportesService.CodigoPrograma, "E");
            else
                VisualizarOpciones(IndicadoresAportesService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            txtColorFondo.eventoCambiar += txtColorFondo_TextChanged;
    
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(IndicadoresAportesService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargardrop();
            if (Session[IndicadoresAportesService.CodigoPrograma + ".id"] != null)
            {
                Usuario usuap = (Usuario)Session["usuario"];
            }
            List<IndicadoresAportes> lstFechas = new List<IndicadoresAportes>();
            IndicadoresAportesService carteraOficinasServicio = new IndicadoresAportesService();
            lstFechas = carteraOficinasServicio.consultarfecha((Usuario)Session["Usuario"]);
            ddlFechaCorte.DataSource = lstFechas;
            ddlFechaCorte.DataValueField = "fecha_corte";
            ddlFechaCorte.DataTextField = "fecha_corte";
            ddlFechaCorte.DataBind();
           // ch3D.Checked = true;
        }
        btnInforme_Click(null, null);
    }

    protected void cargardrop()
    {
        

    Xpinn.Aportes.Services.AporteServices aporteService = new Xpinn.Aportes.Services.AporteServices();
        Xpinn.Aportes.Entities.Aporte aporte = new Xpinn.Aportes.Entities.Aporte();

        ddllinea.DataSource = aporteService.ListarLineaAporte(aporte, (Usuario)Session["Usuario"]);
        ddllinea.DataTextField = "nom_linea_aporte";
        ddllinea.DataValueField = "cod_linea_aporte";
        ddllinea.DataBind();
    }

    protected void btnInforme_Click(object sender, EventArgs e)
    {
        Configuracion conf = new Configuracion();
        string fechaInicial = "";
        if (ddlPeriodo.SelectedValue == "1")
            fechaInicial = Convert.ToDateTime(ddlFechaCorte.SelectedValue).AddDays(-365).ToString(conf.ObtenerFormatoFecha());
        if (ddlPeriodo.SelectedValue == "2")
            fechaInicial = Convert.ToDateTime(ddlFechaCorte.SelectedValue).AddDays(-180).ToString(conf.ObtenerFormatoFecha());
        if (ddlPeriodo.SelectedValue == "3")
            fechaInicial = Convert.ToDateTime(ddlFechaCorte.SelectedValue).AddDays(-90).ToString(conf.ObtenerFormatoFecha());

        // Haciendo visible las gráficas
        Chart1.Visible = true;
        Chart2.Visible = true;

        // Traer los datos según criterio seleccionado
        List<IndicadoresAportes> LstDetalleComprobante = new List<IndicadoresAportes>();
        //LstDetalleComprobante = IndicadoresAportesService.consultarAportes(fechaInicial, ddlFechaCorte.SelectedValue, (Usuario)Session["Usuario"]);
        Int64 cod_linea = 0;
        cod_linea = Convert.ToInt64(ddllinea.SelectedValue);
        LstDetalleComprobante = IndicadoresAportesService.consultarAportes(fechaInicial, ddlFechaCorte.SelectedValue, (Usuario)Session["Usuario"], cod_linea);

        // --------------------------------------------------------------------------------------------------------------------------------------------
        // Mostrar la primera gráfica
        // -------------------------------------------------------------------------------------------------------------------------------------------
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        // Determinar los títulos de la gráfica
        Chart1.Titles["Title1"].Text = "Evolución Aportes a  " + ddlFechaCorte.SelectedValue;
        Chart1.Titles["Title2"].Text = "($ millones)";
        // Cargar los datos a la gráfica
        Chart1.DataSource = LstDetalleComprobante;
        // Determinar el tipo de gráfica
        Chart1.Series["Series1"].ChartType = TipoGrafica(ddlTipoGrafica1);
        
        if (ddlTipoGrafica1.Text == "2")
            {
                Chart1.Series["Series1"].LabelMapAreaAttributes = "numero";
                Chart1.Series["Series1"].IsValueShownAsLabel = true;
                Chart1.Series["Series1"].YValueMembers = "numero";
                Chart1.Series["Series1"].XValueMember = "nombre";
            }
        // Mostrar los nombres de las seríes
        Chart1.Series["Series1"].LegendText = "Total";
        Chart1.Series["Series1"].YValueMembers = "total";
        Chart1.Series["Series1"].XValueMember = "fecha_historico";
        Chart1.Series["Series1"].LabelMapAreaAttributes = "total";
        Chart1.Series["Series1"].IsValueShownAsLabel = true;
        Chart1.Series["Series1"]["LabelStyle"] = "Top";
        Chart1.Series["Series1"].LabelFormat = "{0:N0}";
        Chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;
      
        // Mostrar la gráfica en pantalla
        Chart1.DataBind();
        // llenar gridview
        List<IndicadoresAportes> LstDetalleComprobantet = new List<IndicadoresAportes>();
        LstDetalleComprobantet = IndicadoresAportesService.consultarAportes(fechaInicial, ddlFechaCorte.SelectedValue, (Usuario)Session["Usuario"], cod_linea);
        gvDatos.DataSource = LstDetalleComprobantet;
        gvDatos.DataBind();


        // --------------------------------------------------------------------------------------------------------------------------------------------
        // Mostrar la segunda gráfica    
        // -------------------------------------------------------------------------------------------------------------------------------------------
        Chart2.ChartAreas["ChartArea2"].Area3DStyle.Enable3D = ch3D.Checked;
        // Determinar los títulos de la gráfica
        Chart2.Titles["Title1"].Text = "Evolución de Aportes " + ddlFechaCorte.SelectedValue;
        Chart2.Titles["Title2"].Text = "(# Número)";
        // Cargar los datos a la gráfica
        Chart2.DataSource = LstDetalleComprobante;
        // Determinar el tipo de gráfica
        Chart2.Series["Series1"].ChartType = TipoGrafica(ddlTipoGrafica2);
        // Mostrar los nombres de las seríes
        Chart2.Series["Series1"].LegendText = "Total";
        Chart2.Series["Series1"].LabelMapAreaAttributes = "numero";
        Chart2.Series["Series1"].IsValueShownAsLabel = true;
        Chart2.Series["Series1"].YValueMembers = "numero";
        Chart2.Series["Series1"].XValueMember = "fecha_historico";

        // Refrescar datos de la gráfica en pantalla
        Chart2.DataBind();

       

        // Refrescar datos de la gráfica en pantalla
        Chart2.DataBind();
        List<IndicadoresAportes> LstDetalleComprobanteapor = new List<IndicadoresAportes>();
        LstDetalleComprobanteapor = IndicadoresAportesService.consultarAportes(fechaInicial, ddlFechaCorte.SelectedValue, (Usuario)Session["Usuario"], cod_linea);

        //LstDetalleComprobanteapor = IndicadoresAportesService.consultarAportes(fechaInicial, ddlFechaCorte.SelectedValue, (Usuario)Session["Usuario"]);
        gvCantidad.DataSource = LstDetalleComprobanteapor;
        gvCantidad.DataBind();
    

}
    protected void ddlTipoGrafica1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Generar();
    }

    protected void ddlTipoGrafica2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Generar();
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        string tmpChartName = "grafica.jpg";
        string imgPath = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName;

        Chart1.SaveImage(imgPath);
        string imgPath2 = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartName);

        Response.Clear();
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=IndicadorAportes.xls;");
        StringWriter stringWrite = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        string headerTable = @"<Table><tr><td><img src='" + imgPath2 + @"' \></td></tr></Table>";
        Response.Write(headerTable);
        Response.Write(stringWrite.ToString());
        Response.End();
    }

    protected void btnExportarpor_Click(object sender, EventArgs e)
    {
        string tmpChartName = "grafica.jpg";
        string imgPath = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName;

        Chart2.SaveImage(imgPath);
        string imgPath2 = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartName);

        Response.Clear();
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=IndicadorAportes.xls;");
        StringWriter stringWrite = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        string headerTable = @"<Table><tr><td><img src='" + imgPath2 + @"' \></td></tr></Table>";
        Response.Write(headerTable);
        Response.Write(stringWrite.ToString());
        Response.End();
    }

    private void Generar()
    {
        btnInforme_Click(null, null);
    }

    protected void ddlFechaCorte_SelectedIndexChanged(object sender, EventArgs e)
    {
        Generar();
    }

    protected void ddlPeriodo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Generar();
    }

    protected void ch3D_CheckedChanged(object sender, EventArgs e)
    {
        Generar();
    }

    void MostrarAnotacion(Chart pChart, Int32 pItem, double pPosicion, string pTexto)
    {
        CalloutAnnotation anotacion = new CalloutAnnotation();
        anotacion.Name = "Anotacion" + pItem;
        anotacion.Alignment = System.Drawing.ContentAlignment.TopCenter;
        anotacion.AnchorOffsetX = 0.0;
        anotacion.AnchorOffsetY = 0.0;
        anotacion.CalloutAnchorCap = System.Web.UI.DataVisualization.Charting.LineAnchorCapStyle.None;
        anotacion.CalloutStyle = System.Web.UI.DataVisualization.Charting.CalloutStyle.SimpleLine;
        anotacion.LineDashStyle = System.Web.UI.DataVisualization.Charting.ChartDashStyle.NotSet;
        anotacion.LineWidth = 0;
        anotacion.Text = pTexto;
        if (ddlPeriodo.Text.Trim() != "")
            anotacion.Font = new Font("Microsoft Sans Serif", 6);
        else
            anotacion.Font = new Font("Microsoft Sans Serif", 7);
        anotacion.ForeColor = System.Drawing.Color.White;
        anotacion.ResizeToContent();
        pChart.Annotations.Add(anotacion);
        pChart.Annotations[pItem].AxisX = pChart.ChartAreas["ChartArea1"].AxisX;
        pChart.Annotations[pItem].AxisY = pChart.ChartAreas["ChartArea1"].AxisY;
        pChart.Annotations[pItem].Width = double.NaN;
        pChart.Annotations[pItem].Height = double.NaN;
        pChart.Annotations[pItem].X = double.NaN;
        pChart.Annotations[pItem].Y = double.NaN;
        pChart.Annotations[pItem].AnchorDataPoint = null;
        pChart.Annotations[pItem].AnchorX = pItem + 1;
        pChart.Annotations[pItem].AnchorY = pPosicion;
    }
    protected void txtColorFondo_TextChanged(object sender, EventArgs e)
    {
        //asignar color de fondo
        Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
        Chart2.BackColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
        btnInforme_Click(null, null);
    }
    protected SeriesChartType TipoGrafica(DropDownList ddlTipGra)
    {
        if (ddlTipGra.SelectedIndex == 0)
            return SeriesChartType.Column;
        if (ddlTipGra.SelectedIndex == 1)
            return SeriesChartType.Pie;
        if (ddlTipGra.SelectedIndex == 2)
            return SeriesChartType.Line;
        if (ddlTipGra.SelectedIndex == 3)
            return SeriesChartType.Area;
        return SeriesChartType.Bar;
    }

    protected void ddllinea_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnInforme_Click(null, null);
    }
}
