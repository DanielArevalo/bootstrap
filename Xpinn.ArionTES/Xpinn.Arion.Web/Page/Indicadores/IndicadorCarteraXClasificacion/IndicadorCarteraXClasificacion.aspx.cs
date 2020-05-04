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
    private Xpinn.Indicadores.Services.IndicadorCarteraXClasificacionService indicadorCarteraService = new Xpinn.Indicadores.Services.IndicadorCarteraXClasificacionService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[indicadorCarteraService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(indicadorCarteraService.CodigoPrograma, "E");
            else
                VisualizarOpciones(indicadorCarteraService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            txtColorFondo.eventoCambiar += txtColorFondo_TextChanged;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(indicadorCarteraService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[indicadorCarteraService.CodigoPrograma + ".id"] != null)
        {
            Usuario usuap = (Usuario)Session["usuario"];
        }
        if (!IsPostBack)
        {         
            ucFechaInicial.Visible = true;
            ucFechaFinal.Visible = true;
            ucFechaInicial.ToDateTime = DateTime.Today.AddDays(-365);
            ucFechaFinal.ToDateTime = DateTime.Today;
            ch3D.Checked = true;
         }
        btnInforme_Click(null, null);
   }

   public String vacios(String texto)
    {
        if (String.IsNullOrEmpty(texto))
        {
            return " ";
        }
        else
        {
            return texto;
        }
    }


    protected void btnInforme_Click(object sender, EventArgs e)
    {
        Chart1.Visible = true;
        Chart2.Visible = true;

        Configuracion conf = new Configuracion();
        List<IndicadorCarteraXClasificacion> LstDetalleComprobante = new List<IndicadorCarteraXClasificacion>();
        LstDetalleComprobante = indicadorCarteraService.consultarCarteraVencidaxClasificacion(ucFechaInicial.ToDateTime.ToString(conf.ObtenerFormatoFecha()), ucFechaFinal.ToDateTime.ToString(conf.ObtenerFormatoFecha()), (Usuario)Session["Usuario"]);

        //------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Generando la gráfica de indicador de cartera
        //------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Determinando si la gráfica es 3d
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        // Determinando que sea gráfica por líneas
        Chart1.Series["Series1"].ChartType = SeriesChartType.Line;
        Chart1.Series["Series2"].ChartType = SeriesChartType.Line;
        Chart1.Series["Series3"].ChartType = SeriesChartType.Line;
        Chart1.Series["Series4"].ChartType = SeriesChartType.Line;
        // Determinar el intervalo
        Chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;
        Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        // Determinar los títulos
        Chart1.Titles["Title1"].Text = "ICV > 1 día por Línea de Crédito a " + ucFechaFinal.ToDateTime.ToShortDateString();
        Chart1.Titles["Title2"].Text = "";
        // Determinar los nombres de las series        
        Chart1.Series["Series1"].LegendText = "Microcrédito";
        Chart1.Series["Series2"].LegendText = "Consumo";
        Chart1.Series["Series3"].LegendText = "Vivienda";
        Chart1.Series["Series4"].LegendText = "Comercial";   
        Chart1.Series["Series1"]["DrawingStyle"] = "Emboss";
        Chart1.Series["Series2"]["DrawingStyle"] = "Emboss";
        Chart1.Series["Series3"]["DrawingStyle"] = "Emboss";
        Chart1.Series["Series4"]["DrawingStyle"] = "Emboss";
        // Cargando datos a la gráfica
        Chart1.DataSource = LstDetalleComprobante;
        Chart1.Series["Series1"].YValueMembers = "porcentaje_microcredito";
        Chart1.Series["Series1"].XValueMember = "fecha_historico";
        Chart1.Series["Series1"].LabelMapAreaAttributes = "porcentaje_microcredito";
        Chart1.Series["Series1"].IsValueShownAsLabel = true;

        Chart1.Series["Series2"].YValueMembers = "porcentaje_consumo";
        Chart1.Series["Series2"].XValueMember = "fecha_historico";
        Chart1.Series["Series2"].LabelMapAreaAttributes = "porcentaje_consumo";
        Chart1.Series["Series2"].IsValueShownAsLabel = true;

        Chart1.Series["Series3"].YValueMembers = "porcentaje_vivienda";
        Chart1.Series["Series3"].XValueMember = "fecha_historico";
        Chart1.Series["Series3"].LabelMapAreaAttributes = "porcentaje_vivienda";
        Chart1.Series["Series3"].IsValueShownAsLabel = true;

        Chart1.Series["Series4"].YValueMembers = "porcentaje_comercial";
        Chart1.Series["Series4"].XValueMember = "fecha_historico";
        Chart1.Series["Series4"].LabelMapAreaAttributes = "porcentaje_comercial";
        Chart1.Series["Series4"].IsValueShownAsLabel = true;

        Chart1.DataBind();

        //------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Generando la gráfica de indicador de cartera
        //------------------------------------------------------------------------------------------------------------------------------------------------------------
        LstDetalleComprobante = indicadorCarteraService.consultarCarteraVencida30xClasificacion(ucFechaInicial.ToDateTime.ToString(conf.ObtenerFormatoFecha()), ucFechaFinal.ToDateTime.ToString(conf.ObtenerFormatoFecha()), (Usuario)Session["Usuario"]);
        Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        Chart2.Series["Series1"].ChartType = SeriesChartType.Line; ;
        Chart2.Series["Series2"].ChartType = SeriesChartType.Line; ;
        Chart2.Titles["Title1"].Text = "ICV > 30 días por Línea de Crédito a " + ucFechaFinal.ToDateTime.ToShortDateString();
        Chart2.Titles["Title2"].Text = "";
        Chart2.Series["Series1"].LegendText = "Microcrédito";
        Chart2.Series["Series2"].LegendText = "Consumo";
        Chart2.Series["Series3"].LegendText = "Vivienda";
        Chart2.Series["Series4"].LegendText = "Comercial";  
        Chart2.Series["Series1"]["DrawingStyle"] = "Emboss";
        Chart2.Series["Series2"]["DrawingStyle"] = "Emboss";
        Chart2.Series["Series3"]["DrawingStyle"] = "Emboss";
        Chart2.Series["Series4"]["DrawingStyle"] = "Emboss";
        Chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;

        Chart2.DataSource = LstDetalleComprobante;
        Chart2.Series["Series1"].YValueMembers = "porcentaje_30microcredito";
        Chart2.Series["Series1"].XValueMember = "fecha_historico";
        Chart2.Series["Series1"].LabelMapAreaAttributes = "porcentaje_30microcredito";
        Chart2.Series["Series1"].IsValueShownAsLabel = true;

        Chart2.Series["Series2"].YValueMembers = "porcentaje_30consumo";
        Chart2.Series["Series2"].XValueMember = "fecha_historico";
        Chart2.Series["Series2"].LabelMapAreaAttributes = "porcentaje_30consumo";
        Chart2.Series["Series2"].IsValueShownAsLabel = true;

        Chart2.Series["Series3"].YValueMembers = "porcentaje_30vivienda";
        Chart2.Series["Series3"].XValueMember = "fecha_historico";
        Chart2.Series["Series3"].LabelMapAreaAttributes = "porcentaje_30vivienda";
        Chart2.Series["Series3"].IsValueShownAsLabel = true;

        Chart2.Series["Series4"].YValueMembers = "porcentaje_30comercial";
        Chart2.Series["Series4"].XValueMember = "fecha_historico";
        Chart2.Series["Series4"].LabelMapAreaAttributes = "porcentaje_30comercial";
        Chart2.Series["Series4"].IsValueShownAsLabel = true;
        Chart2.DataBind();

    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        string tmpChartName = "grafica.jpg";
        string imgPath = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName;

        Chart1.SaveImage(imgPath);
        string imgPath2 = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartName);

        Response.Clear();
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=test.xls;");
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
        Response.AddHeader("Content-Disposition", "attachment; filename=test.xls;");
        StringWriter stringWrite = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        string headerTable = @"<Table><tr><td><img src='" + imgPath2 + @"' \></td></tr></Table>";
        Response.Write(headerTable);
        Response.Write(stringWrite.ToString());
        Response.End();
    }

    protected void ch3D_CheckedChanged(object sender, EventArgs e)
    {
        btnInforme_Click(null, null);
    }
    protected void txtColorFondo_TextChanged(object sender, EventArgs e)
    {
        //asignar color de fondo
        Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
        btnInforme_Click(null, null);
        Chart2.BackColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
        btnInforme_Click(null, null);
    }
}
