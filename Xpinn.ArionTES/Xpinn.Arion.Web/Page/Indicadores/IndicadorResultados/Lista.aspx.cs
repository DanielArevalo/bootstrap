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
    private Xpinn.Indicadores.Services.IndicadoresLiquidezService IndicadoresLiquidezService = new Xpinn.Indicadores.Services.IndicadoresLiquidezService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[IndicadoresLiquidezService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(IndicadoresLiquidezService.CodigoPrograma, "E");
            else
                VisualizarOpciones(IndicadoresLiquidezService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
           // txtColorFondo.eventoCambiar += txtColorFondo_TextChanged;
    
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(IndicadoresLiquidezService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session[IndicadoresLiquidezService.CodigoPrograma + ".id"] != null)
            {
                Usuario usuap = (Usuario)Session["usuario"];
            }
            List<IndicadoresLiquidez> lstFechas = new List<IndicadoresLiquidez>();
            IndicadoresLiquidezService carteraOficinasServicio = new IndicadoresLiquidezService();
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
        Configuracion conf = new Configuracion();
        string fechaInicial = ddlFechaCorte.SelectedValue;

        //// Haciendo visible las gráficas
        //Chart1.Visible = true;
        ////Chart2.Visible = true;


        // Traer los datos según criterio seleccionado
        List<IndicadoresLiquidez> LstDetalleComprobante = new List<IndicadoresLiquidez>();
        if (ddlDescripcion.SelectedValue == "1")
        {
            LstDetalleComprobante = IndicadoresLiquidezService.consultarDepositosLiquidez(fechaInicial, (Usuario)Session["Usuario"]);
        }
        if (ddlDescripcion.SelectedValue == "2")
        {
            LstDetalleComprobante = IndicadoresLiquidezService.consultarDisponible(fechaInicial, (Usuario)Session["Usuario"]);
        }

        if (LstDetalleComprobante.Count() > 0)
        {
            gvDatos.Visible = true;
            gvDatos.DataSource = LstDetalleComprobante;
            gvDatos.DataBind();
        
        }
        else
        {
            gvDatos.Visible = false;
           
        }

        // Traer los datos según criterio seleccionado para fondo de liquidez 
        if (ddlDescripcion.SelectedValue == "1")
        {

            List<IndicadoresLiquidez> LstDetalleComprobanteliquidez = new List<IndicadoresLiquidez>();
            LstDetalleComprobanteliquidez = IndicadoresLiquidezService.consultarFondoLiquidez(fechaInicial, (Usuario)Session["Usuario"]);
            if (LstDetalleComprobanteliquidez.Count() > 0)
            {
                gvFondo.Visible = true;
                gvFondo.DataSource = LstDetalleComprobanteliquidez;
                gvFondo.DataBind();

            }
            else
            {
                gvFondo.Visible = false;

            }
        }
        // Traer los datos según criterio seleccionado para fondo de liquidez 
        if (ddlDescripcion.SelectedValue == "2")
        {
                gvFondo.Visible = false;
               
        }

    } 

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        string tmpChartName = "grafica.jpg";
        string imgPath = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName;

       // Chart1.SaveImage(imgPath);
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

    //    Chart2.SaveImage(imgPath);
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
      //  if (ddlPeriodo.Text.Trim() != "")
         //  anotacion.Font = new Font("Microsoft Sans Serif", 6);
       // else
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
       // Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
    //    Chart2.BackColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
        btnInforme_Click(null, null);
    }
    private Double suma  =0;
    private Double sumaLiquidez = 0;
    private Double sumadepositos = 0;
    protected void gvDatos_RowDataBound(object sender, GridViewRowEventArgs e)
    {



        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            suma += Double.Parse(e.Row.Cells[2].Text);
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "TOTAL:";
            e.Row.Cells[2].Text = suma.ToString("n0");
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Font.Bold = true;
            txttotal.Text = suma.ToString();
        }
      




        }


    protected void gvFondo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        sumadepositos = Convert.ToDouble(txttotal.Text);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            sumaLiquidez += Double.Parse(e.Row.Cells[2].Text);
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "% FONDO LIQUIDEZ:";
            e.Row.Cells[2].Text = (sumaLiquidez / sumadepositos).ToString();
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Font.Bold = true;
           
        }
    }
}
