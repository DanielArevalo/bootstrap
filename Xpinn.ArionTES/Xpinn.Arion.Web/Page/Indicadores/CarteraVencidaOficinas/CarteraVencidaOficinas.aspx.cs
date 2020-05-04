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
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Indicadores.Services.EvolucionDesembolsoService EvolucionDesembolsoService = new Xpinn.Indicadores.Services.EvolucionDesembolsoService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[EvolucionDesembolsoService.CodigoProgramaVencida + ".id"] != null)
                VisualizarOpciones(EvolucionDesembolsoService.CodigoProgramaVencida, "E");
            else
                VisualizarOpciones(EvolucionDesembolsoService.CodigoProgramaVencida, "A");

            Site toolBar = (Site)this.Master;
            txtColorFondo.eventoCambiar += txtColorFondo_TextChanged;
    
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EvolucionDesembolsoService.CodigoProgramaVencida, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session[EvolucionDesembolsoService.CodigoProgramaVencida + ".id"] != null)
            {
                Usuario usuap = (Usuario)Session["usuario"];
            }
            List<CarteraOficinas> lstFechas = new List<CarteraOficinas>();
            CarteraOficinasService carteraOficinasServicio = new CarteraOficinasService();
            lstFechas = carteraOficinasServicio.consultarfecha((Usuario)Session["Usuario"]);
            ddlFechaCorte.DataSource = lstFechas;
            ddlFechaCorte.DataValueField = "fecha_corte";
            ddlFechaCorte.DataTextField = "fecha_corte";
            ddlFechaCorte.DataBind();
        }
        btnInforme_Click(null, null);
    }
 

    protected void btnInforme_Click(object sender, EventArgs e)
    {
        Chart1.Visible = true;
        Chart2.Visible = true;

        Configuracion conf = new Configuracion();

        string fechaInicial = "";
        if (ddlPeriodo.SelectedValue == "1")
            fechaInicial = Convert.ToDateTime(ddlFechaCorte.SelectedValue).AddDays(-365).ToString(conf.ObtenerFormatoFecha());
        if (ddlPeriodo.SelectedValue == "2")
            fechaInicial = Convert.ToDateTime(ddlFechaCorte.SelectedValue).AddDays(-180).ToString(conf.ObtenerFormatoFecha());
        if (ddlPeriodo.SelectedValue == "3")
            fechaInicial = Convert.ToDateTime(ddlFechaCorte.SelectedValue).AddDays(-90).ToString(conf.ObtenerFormatoFecha());

        List<EvolucionDesembolsos> LstDetalleComprobante = new List<EvolucionDesembolsos>();
        LstDetalleComprobante = EvolucionDesembolsoService.consultarDesembolso(fechaInicial, ddlFechaCorte.SelectedValue,"", (Usuario)Session["Usuario"]);
        // --------------------------------------------------------------------------------------------------------------------------------------------
        // Mostrar la primera gráfica
        // -------------------------------------------------------------------------------------------------------------------------------------------
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        // Determinar los títulos de la gráfica
        Chart1.Titles["Title1"].Text = "Evolución Monto Desembolsado a " + ddlFechaCorte.SelectedValue;
        Chart1.Titles["Title2"].Text = "($ millones)";
        // Cargar los datos a la gráfica
        Chart1.DataSource = LstDetalleComprobante;
        // Determinar el tipo de gráfica
        if (ddlPeriodo.SelectedValue == "3")
        {
            Chart1.Series["Series1"].ChartType = SeriesChartType.Column;
            Chart1.Series["Series2"].ChartType = SeriesChartType.Column;
            Chart1.Series["Series3"].ChartType = SeriesChartType.Column;
            Chart1.Series["Series4"].ChartType = SeriesChartType.Column;
        }
        else
        {
            Chart1.Series["Series1"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
            Chart1.Series["Series2"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
            Chart1.Series["Series3"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
            Chart1.Series["Series4"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);

        }
        // Mostrar los nombres de las seríes
        Chart1.Series["Series1"].LegendText = "Total";
        Chart1.Series["Series1"].YValueMembers = "total_desembolsos";
        Chart1.Series["Series1"].XValueMember = "fecha_historico";
        Chart1.Series["Series1"].LabelMapAreaAttributes = "total_desembolsos";
        Chart1.Series["Series1"].IsValueShownAsLabel = true;
        Chart1.Series["Series1"]["LabelStyle"] = "Top";

        Chart1.Series["Series2"].LegendText = "Microcrédito";
        Chart1.Series["Series2"].YValueMembers = "total_desembolsos_microcredito";
        Chart1.Series["Series2"].XValueMember = "fecha_historico";
        Chart1.Series["Series2"].LabelMapAreaAttributes = "total_desembolsos_microcredito";
        Chart1.Series["Series2"].IsValueShownAsLabel = true;
        Chart1.Series["Series2"]["LabelStyle"] = "Top";

        Chart1.Series["Series3"].LegendText = "Consumo";
        Chart1.Series["Series3"].YValueMembers = "total_desembolsos_consumo";
        Chart1.Series["Series3"].XValueMember = "fecha_historico";
        Chart1.Series["Series3"].LabelMapAreaAttributes = "total_desembolsos_consumo";
        Chart1.Series["Series3"].IsValueShownAsLabel = true;
        Chart1.Series["Series3"]["LabelStyle"] = "Top";

        Chart1.Series["Series4"].LegendText = "Vivienda";
        Chart1.Series["Series4"].YValueMembers = "total_desembolsos_vivienda";
        Chart1.Series["Series4"].XValueMember = "fecha_historico";
        Chart1.Series["Series4"].LabelMapAreaAttributes = "total_desembolsos_vivienda";
        Chart1.Series["Series4"].IsValueShownAsLabel = true;
        Chart1.Series["Series4"]["LabelStyle"] = "Top";

        Chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;
        // Mostrar la gráfica en pantalla
        Chart1.DataBind();

        // --------------------------------------------------------------------------------------------------------------------------------------------
        // Mostrar la segunda gráfica    
        // -------------------------------------------------------------------------------------------------------------------------------------------
        Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        Chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        // Determinar los títulos de la gráfica
        Chart2.Titles["Title1"].Text = "Evolución  de Créditos " + ddlFechaCorte.SelectedValue;
        Chart2.Titles["Title2"].Text = "(# Número)";
        // Cargar los datos a la gráfica
        Chart2.DataSource = LstDetalleComprobante;
        // Determinar el tipo de gráfica
        if (ddlPeriodo.SelectedValue == "3")
        {
            Chart2.Series["Series1"].ChartType = SeriesChartType.Column;
            Chart2.Series["Series2"].ChartType = SeriesChartType.Column;
            Chart2.Series["Series3"].ChartType = SeriesChartType.Column;
            Chart2.Series["Series4"].ChartType = SeriesChartType.Column;
        }
        else
        {
            Chart2.Series["Series1"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
            Chart2.Series["Series2"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
            Chart2.Series["Series3"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
            Chart2.Series["Series4"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);

        }
        // Mostrar los nombres de las seríes
        Chart2.Series["Series1"].LegendText = "Total";
        Chart2.Series["Series1"].LabelMapAreaAttributes = "numero_desembolsos";
        Chart2.Series["Series1"].IsValueShownAsLabel = true;
        Chart2.Series["Series1"].YValueMembers = "numero_desembolsos";
        Chart2.Series["Series1"].XValueMember = "fecha_historico";

        Chart2.Series["Series2"].LegendText = "Microcrédito";
        Chart2.Series["Series2"].YValueMembers = "numero_desembolsos_microcredito";
        Chart2.Series["Series2"].XValueMember = "fecha_historico";
        Chart2.Series["Series2"].LabelMapAreaAttributes = "numero_desembolsos_microcredito";
        Chart2.Series["Series2"].IsValueShownAsLabel = true;
        Chart2.Series["Series2"]["LabelStyle"] = "Top";

        Chart2.Series["Series3"].LegendText = "Consumo";
        Chart2.Series["Series3"].YValueMembers = "numero_desembolsos_consumo";
        Chart2.Series["Series3"].XValueMember = "fecha_historico";
        Chart2.Series["Series3"].LabelMapAreaAttributes = "numero_desembolsos_consumo";
        Chart2.Series["Series3"].IsValueShownAsLabel = true;
        Chart2.Series["Series3"]["LabelStyle"] = "Top";

        Chart2.Series["Series4"].LegendText = "Vivienda";
        Chart2.Series["Series4"].YValueMembers = "numero_desembolsos_vivienda";
        Chart2.Series["Series4"].XValueMember = "fecha_historico";
        Chart2.Series["Series4"].LabelMapAreaAttributes = "numero_desembolsos_vivienda";
        Chart2.Series["Series4"].IsValueShownAsLabel = true;
        Chart2.Series["Series4"]["LabelStyle"] = "Top";

        // Refrescar datos de la gráfica en pantalla
        Chart2.DataBind();
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {

        string tmpChartName = "grafica.jpg";
        string imgPath = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName;

        Chart1.SaveImage(imgPath);
        string imgPath2 = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartName);

        Response.ContentType = "application/vnd.ms-excel";
        StringWriter stringWrite = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        string headerTable = @"<div><Table><tr><td><img src='" + imgPath2 + @"' \></td></tr></Table></div>";
        Response.Write(headerTable);

        Response.Write(stringWrite.ToString());
        Response.End();
    }

    protected void btnExportarpor_Click(object sender, EventArgs e)
    {

        string tmpChartName = "grafica.jpg";
        string imgPath = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName;

        Chart1.SaveImage(imgPath);
        string imgPath2 = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartName);

        Response.ContentType = "application/vnd.ms-excel";
        StringWriter stringWrite = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        string headerTable = @"<div><Table><tr><td><img src='" + imgPath2 + @"' \></td></tr></Table></div>";
        Response.Write(headerTable);

        Response.Write(stringWrite.ToString());
        Response.End();
    }

    protected void ch3D_CheckedChanged(object sender, EventArgs e)
    {
        btnInforme_Click(null, null);
    }

    protected void ddlFechaCorte_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnInforme_Click(null, null);
    }

    protected void ddlPeriodo_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnInforme_Click(null, null);
    }
    protected void txtColorFondo_TextChanged(object sender, EventArgs e)
    {
        //asignar color de fondo
        Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
        Chart2.BackColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
        btnInforme_Click(null, null);
    }
}
