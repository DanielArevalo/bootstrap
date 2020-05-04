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
using ClosedXML.Excel;

partial class Nuevo : GlobalWeb
{
    private CarteraOficinasService CarteraOficinasService = new CarteraOficinasService();
    private IndicadorCarteraService carteraService = new IndicadorCarteraService();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[carteraService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(carteraService.CodigoPrograma, "E");
            else
                VisualizarOpciones(carteraService.CodigoPrograma, "A");

            // Site toolBar = (Site)this.Master;
            txtColorFondo.eventoCambiar += txtColorFondo_TextChanged;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(carteraService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[carteraService.CodigoPrograma + ".id"] != null)
        {
            Usuario usuap = (Usuario)Session["usuario"];
        }
        if (!IsPostBack)
        {
            ucFechaCorte.Visible = true;
            ch3D.Checked = false;
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
        List<CarteraOficinas> LstDetalleComprobante = new List<CarteraOficinas>();
        List<CarteraOficinas> LstDetalleComprobant = new List<CarteraOficinas>();
        List<CarteraOficinas> stDetalleComprobantes = new List<CarteraOficinas>();
        List<CarteraOficinas> stDetalleComprobant = new List<CarteraOficinas>();
        LstDetalleComprobante = CarteraOficinasService.consultarCarteraOficinas(ucFechaCorte.ToDateTime.ToString(conf.ObtenerFormatoFecha()), 2, (Usuario)Session["Usuario"]);
        foreach (var item in LstDetalleComprobante)
        {
            CarteraOficinas LstDetalleComprobantes = new CarteraOficinas();

            LstDetalleComprobantes.Cod_linea_credito = item.Cod_linea_credito;
            LstDetalleComprobantes.total_cartera = item.total_cartera;
            LstDetalleComprobantes.nombre = item.nombre;
            LstDetalleComprobantes.fecha_corte = Convert.ToDateTime(item.fecha_corte).ToString("dd-MM-yyyy");
            LstDetalleComprobant.Add(LstDetalleComprobantes);
        }

        Random r = new Random();
        if (sender == null && e == null)
        {
            if (chkMostrarPorcentaje.Checked && chkMostrarValores.Checked == false)
            {
                Chart2.Visible = true;
            }
            else if (chkMostrarPorcentaje.Checked == false && chkMostrarValores.Checked)
            {
                Chart1.Visible = true;
            }
            else if (chkMostrarPorcentaje.Checked && chkMostrarValores.Checked)
            {
                Chart1.Visible = true;
                Chart2.Visible = true;
            }
            else
            {
                Chart1.Visible = false;
                Chart2.Visible = false;
            }
            int number = 0;
            Chart1.Series.Clear();
            Chart2.Series.Clear();
            if (LstDetalleComprobante.Count > 0)
            {


                foreach (var item in LstDetalleComprobant.OrderBy(x => x.fecha_corte))
                {

                    Color color = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
                    //------------------------------------------------------------------------------------------------------------------------------------------------------------
                    // Generando la gráfica de indicador de cartera
                    //------------------------------------------------------------------------------------------------------------------------------------------------------------
                    //agregar las series que sean necesarias
                    var name = "Serie" + number;
                    Chart1.Series.Add(name);
                    // Determinando si la gráfica es 3d
                    Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
                    // Determinando que sea gráfica por líneas
                    Chart1.Series[name].ChartType = TipoGrafica(ddlTipoGrafica1);
                    // Determinar el intervalo
                    Chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;
                    Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                    // Determinar los títulos
                    Chart1.Titles["Title1"].Text = "Cartera Línea de Crédito al " + ucFechaCorte.ToDateTime.ToShortDateString();
                    Chart1.Titles["Title2"].Text = "(Total cartera por Millones)";
                    // Determinar los nombres de las series        
                    Chart1.Series[name].Font = new Font("Microsoft Sans Serif", 6);
                    Chart1.Series[name].MarkerSize = 7;
                    Chart1.Series[name].MarkerColor = color;
                    Chart1.Series[name].MarkerBorderWidth = 1;
                    Chart1.Series[name].MarkerStyle = MarkerStyle.Circle;
                    Chart1.Series[name].LegendText = item.Cod_linea_credito;
                    Chart1.Series[name]["DrawingStyle"] = "Emboss";
                    // Cargando datos a la gráfica
                    Chart1.DataSource = LstDetalleComprobant;
                    Chart1.Series[name].Points.AddXY(item.fecha_corte, item.total_cartera);
                    Chart1.Series[name].LabelMapAreaAttributes = Convert.ToString(item.total_cartera);
                    Chart1.Series[name].IsValueShownAsLabel = true;
                    Chart1.Series[name].Color = color;
                    Chart1.DataBind();

                    number++;
                }
                number = 0;
            }
            //   ------------------------------------------------------------------------------------------------------------------------------------------------------------
            // Generando la gráfica de indicador de cartera
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------
            stDetalleComprobantes = CarteraOficinasService.consultarCarteraOficinas(ucFechaCorte.ToDateTime.ToString(conf.ObtenerFormatoFecha()), 2, (Usuario)Session["Usuario"]);

            foreach (var item in stDetalleComprobantes)
            {
                CarteraOficinas LstDetalleComprobantes = new CarteraOficinas();

                LstDetalleComprobantes.Cod_linea_credito = item.Cod_linea_credito;
                LstDetalleComprobantes.numero_cartera = item.numero_cartera;
                LstDetalleComprobantes.nombre = item.nombre;
                LstDetalleComprobantes.fecha_corte = Convert.ToDateTime(item.fecha_corte).ToString("dd-MM-yyyy");
                stDetalleComprobant.Add(LstDetalleComprobantes);
            }

            foreach (var item in stDetalleComprobant)
            {
                Color color = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
                //agregar las series que sean necesarias
                var name = "Serie" + number;
                Chart2.Series.Add(name);
                // Determinando si la gráfica es 3d
                Chart2.ChartAreas["ChartArea2"].Area3DStyle.Enable3D = ch3D.Checked;
                // Determinando que sea gráfica por líneass
                Chart2.Series[name].ChartType = TipoGrafica(ddlTipoGrafica1);
                // Determinar los títulos
                Chart2.Titles["Title1"].Text = "Cartera Línea de Crédito al " + ucFechaCorte.ToDateTime.ToShortDateString();
                Chart2.Titles["Title2"].Text = "(Total cartera Cantidad)";
                // Determinar el intervalo
                Chart2.ChartAreas["ChartArea2"].AxisX.IsMarginVisible = true;
                Chart2.ChartAreas["ChartArea2"].AxisX.Interval = 1;
                // Determinar los nombres de las series        
                Chart2.Series[name].Font = new Font("Microsoft Sans Serif", 6);
                Chart2.Series[name].MarkerSize = 7;
                Chart2.Series[name].MarkerColor = color;
                Chart2.Series[name].MarkerBorderWidth = 1;
                Chart2.Series[name].MarkerStyle = MarkerStyle.Circle;
                Chart2.Series[name].LegendText = item.Cod_linea_credito;
                Chart2.Series[name]["DrawingStyle"] = "Emboss";

                // Cargando datos a la gráfica
                Chart2.DataSource = stDetalleComprobantes;
                Chart2.Series[name].Points.AddXY(item.fecha_corte, item.numero_cartera);
                Chart2.Series[name].LabelMapAreaAttributes = Convert.ToString(item.numero_cartera);
                Chart2.Series[name].IsValueShownAsLabel = true;
                Chart2.Series[name].Color = color;
                Chart2.DataBind();
                number++;

            }
            gvDatos.DataSource = LstDetalleComprobante;
            gvDatos.DataBind();
            gvDatos2.DataSource = stDetalleComprobantes;
            gvDatos2.DataBind();
        }
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

        // Chart2.SaveImage(imgPath);
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
        // Chart2.BackColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
        btnInforme_Click(null, null);
    }

    protected void ddlTipoGrafica1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Generar();
    }
    private void Generar()
    {
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

    protected void chkMostrarPorcentaje_CheckedChanged(object sender, EventArgs e)
    {
        btnInforme_Click(null, null);
    }
    protected void chkMostrarValores_CheckedChanged(object sender, EventArgs e)
    {
        btnInforme_Click(null, null);
    }

}
