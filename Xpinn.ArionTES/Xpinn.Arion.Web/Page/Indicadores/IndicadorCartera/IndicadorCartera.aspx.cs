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
    private Xpinn.Indicadores.Services.IndicadorCarteraService indicadorCarteraService = new Xpinn.Indicadores.Services.IndicadorCarteraService();

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
            chk30dias.Checked = true;
            chk60dias.Checked = true;
            chk90dias.Checked = true;
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
        List<IndicadorCartera> LstDetalleComprobante = new List<IndicadorCartera>();
        LstDetalleComprobante = indicadorCarteraService.consultarCarteraVencida(ucFechaInicial.ToDateTime.ToString(conf.ObtenerFormatoFecha()), ucFechaFinal.ToDateTime.ToString(conf.ObtenerFormatoFecha()), (Usuario)Session["Usuario"]);

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
        Chart1.Series["Series5"].ChartType = SeriesChartType.Line;
        Chart1.Series["Series6"].ChartType = SeriesChartType.Line;
        // Determinar el intervalo
        Chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;
        Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        // Determinar los títulos
        Chart1.Titles["Title1"].Text = "Evolución Indicador de Cartera a " + ucFechaFinal.ToDateTime.ToShortDateString();
        Chart1.Titles["Title2"].Text = "Vencida (";
        if (chk30dias.Checked == true)
            Chart1.Titles["Title2"].Text += " > 30 días";
        if (chk60dias.Checked == true)
            Chart1.Titles["Title2"].Text += " > 60 días";
        if (chk90dias.Checked == true)
            Chart1.Titles["Title2"].Text += " > 90 días";
        if (chk120dias.Checked == true)
            Chart1.Titles["Title2"].Text += " > 120 días";
        if (chk180dias.Checked == true)
            Chart1.Titles["Title2"].Text += " > 180 días";
        if (chk360dias.Checked == true)
            Chart1.Titles["Title2"].Text += " > 360 días";
        Chart1.Titles["Title2"].Text += ")";
        // Determinar los nombres de las series
        if (chk30dias.Checked == true)
            Chart1.Series["Series1"].LegendText = " > 30 días";
        else
            Chart1.Series["Series1"].IsVisibleInLegend = false;
        if (chk60dias.Checked == true)
            Chart1.Series["Series2"].LegendText = " > 60 días";
        else
            Chart1.Series["Series2"].IsVisibleInLegend = false;
        if (chk90dias.Checked == true)
            Chart1.Series["Series3"].LegendText = " > 90 días";
        else
            Chart1.Series["Series3"].IsVisibleInLegend = false;
        if (chk120dias.Checked == true)
            Chart1.Series["Series4"].LegendText = " > 120 días";
        else
            Chart1.Series["Series4"].IsVisibleInLegend = false;
        if (chk180dias.Checked == true)
            Chart1.Series["Series5"].LegendText = " > 180 días";
        else
            Chart1.Series["Series5"].IsVisibleInLegend = false;
        if (chk360dias.Checked == true)
            Chart1.Series["Series6"].LegendText = " > 360 días";
        else
            Chart1.Series["Series6"].IsVisibleInLegend = false;
        Chart1.Series["Series1"]["DrawingStyle"] = "Emboss";
        Chart1.Series["Series2"]["DrawingStyle"] = "Emboss";
        Chart1.Series["Series3"]["DrawingStyle"] = "Emboss";
        Chart1.Series["Series4"]["DrawingStyle"] = "Emboss";
        Chart1.Series["Series5"]["DrawingStyle"] = "Emboss";
        Chart1.Series["Series6"]["DrawingStyle"] = "Emboss";
        // Cargando datos a la gráfica
        Chart1.DataSource = LstDetalleComprobante;
        if (chk30dias.Checked == true)
        {
            Chart1.Series["Series1"].YValueMembers = "porcentaje_30dias";
            Chart1.Series["Series1"].XValueMember = "fecha_historico";
            Chart1.Series["Series1"].LabelMapAreaAttributes = "porcentaje_30dias";
            Chart1.Series["Series1"].IsValueShownAsLabel = true;
        }
        if (chk60dias.Checked == true)
        {
            Chart1.Series["Series2"].YValueMembers = "porcentaje_60dias";
            Chart1.Series["Series2"].XValueMember = "fecha_historico";
            Chart1.Series["Series2"].LabelMapAreaAttributes = "porcentaje_60dias";
            Chart1.Series["Series2"].IsValueShownAsLabel = true;
        }
        if (chk90dias.Checked == true)
        {
            Chart1.Series["Series3"].YValueMembers = "porcentaje_90dias";
            Chart1.Series["Series3"].XValueMember = "fecha_historico";
            Chart1.Series["Series3"].LabelMapAreaAttributes = "porcentaje_90dias";
            Chart1.Series["Series3"].IsValueShownAsLabel = true;
        }
        if (chk120dias.Checked == true)
        {
            Chart1.Series["Series4"].YValueMembers = "porcentaje_120dias";
            Chart1.Series["Series4"].XValueMember = "fecha_historico";
            Chart1.Series["Series4"].LabelMapAreaAttributes = "porcentaje_120dias";
            Chart1.Series["Series4"].IsValueShownAsLabel = true;
        }
        if (chk180dias.Checked == true)
        {
            Chart1.Series["Series5"].YValueMembers = "porcentaje_180dias";
            Chart1.Series["Series5"].XValueMember = "fecha_historico";
            Chart1.Series["Series5"].LabelMapAreaAttributes = "porcentaje_180dias";
            Chart1.Series["Series5"].IsValueShownAsLabel = true;
        }
        if (chk360dias.Checked == true)
        {
            Chart1.Series["Series6"].YValueMembers = "porcentaje_360dias";
            Chart1.Series["Series6"].XValueMember = "fecha_historico";
            Chart1.Series["Series6"].LabelMapAreaAttributes = "porcentaje_360dias";
            Chart1.Series["Series6"].IsValueShownAsLabel = true;
        }
        Chart1.DataBind();

        Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        Chart2.Series["Series1"].ChartType = SeriesChartType.Line; ;
        Chart2.Series["Series2"].ChartType = SeriesChartType.Line; ;
        Chart2.Titles["Title1"].Text = "Evolución Saldo de Cartera Vencida a " + ucFechaFinal.ToDateTime.ToShortDateString();
        Chart2.Titles["Title2"].Text = "Vencida (";
        if (chk30dias.Checked == true)
            Chart2.Titles["Title2"].Text += " > 30 días";
        if (chk60dias.Checked == true)
            Chart2.Titles["Title2"].Text += " > 60 días";
        if (chk90dias.Checked == true)
            Chart2.Titles["Title2"].Text += " > 90 días";
        if (chk120dias.Checked == true)
            Chart2.Titles["Title2"].Text += " > 120 días";
        if (chk180dias.Checked == true)
            Chart2.Titles["Title2"].Text += " > 180 días";
        if (chk360dias.Checked == true)
            Chart2.Titles["Title2"].Text += " > 360 días";
        Chart2.Titles["Title2"].Text += ")";
        Chart2.Series["Series1"]["DrawingStyle"] = "Emboss";
        Chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        if (chk30dias.Checked == true)
            Chart2.Series["Series1"].LegendText = " > 30 días";
        else
            Chart2.Series["Series1"].IsVisibleInLegend = false;
        if (chk60dias.Checked == true)
            Chart2.Series["Series2"].LegendText = " > 60 días";
        else
            Chart2.Series["Series2"].IsVisibleInLegend = false;
        if (chk90dias.Checked == true)
            Chart2.Series["Series3"].LegendText = " > 90 días";
        else
            Chart2.Series["Series3"].IsVisibleInLegend = false;
        if (chk120dias.Checked == true)
            Chart2.Series["Series4"].LegendText = " > 120 días";
        else
            Chart2.Series["Series4"].IsVisibleInLegend = false;
        if (chk180dias.Checked == true)
            Chart2.Series["Series5"].LegendText = " > 180 días";
        else
            Chart2.Series["Series5"].IsVisibleInLegend = false;
        if (chk360dias.Checked == true)
            Chart2.Series["Series6"].LegendText = " > 360 días";
        else
            Chart2.Series["Series6"].IsVisibleInLegend = false;
        Chart2.DataSource = LstDetalleComprobante;
        if (chk30dias.Checked == true)
        {
            Chart2.Series["Series1"].YValueMembers = "valor_30dias";
            Chart2.Series["Series1"].XValueMember = "fecha_historico";
            Chart2.Series["Series1"].LabelMapAreaAttributes = "valor_30dias";
            Chart2.Series["Series1"].IsValueShownAsLabel = true;
        }
        if (chk60dias.Checked == true)
        {
            Chart2.Series["Series2"].YValueMembers = "valor_60dias";
            Chart2.Series["Series2"].XValueMember = "fecha_historico";
            Chart2.Series["Series2"].LabelMapAreaAttributes = "valor_60dias";
            Chart2.Series["Series2"].IsValueShownAsLabel = true;
        }
        if (chk90dias.Checked == true)
        {
            Chart2.Series["Series3"].YValueMembers = "valor_90dias";
            Chart2.Series["Series3"].XValueMember = "fecha_historico";
            Chart2.Series["Series3"].LabelMapAreaAttributes = "valor_90dias";
            Chart2.Series["Series3"].IsValueShownAsLabel = true;
        }
        if (chk120dias.Checked == true)
        {
            Chart2.Series["Series4"].YValueMembers = "valor_120dias";
            Chart2.Series["Series4"].XValueMember = "fecha_historico";
            Chart2.Series["Series4"].LabelMapAreaAttributes = "valor_120dias";
            Chart2.Series["Series4"].IsValueShownAsLabel = true;
        }
        if (chk180dias.Checked == true)
        {
            Chart2.Series["Series5"].YValueMembers = "valor_180dias";
            Chart2.Series["Series5"].XValueMember = "fecha_historico";
            Chart2.Series["Series5"].LabelMapAreaAttributes = "valor_180dias";
            Chart2.Series["Series5"].IsValueShownAsLabel = true;
        }
        if (chk360dias.Checked == true)
        {
            Chart2.Series["Series6"].YValueMembers = "valor_360dias";
            Chart2.Series["Series6"].XValueMember = "fecha_historico";
            Chart2.Series["Series6"].LabelMapAreaAttributes = "valor_360dias";
            Chart2.Series["Series6"].IsValueShownAsLabel = true;
        }
        Chart2.DataBind();

    }

    protected void chkdias_CheckedChanged(object sender, EventArgs e)
    {
        btnInforme_Click(null, null);
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
        Chart2.BackColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
        btnInforme_Click(null, null);
    }
}
