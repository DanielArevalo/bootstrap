using System;
using System.Data;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Drawing.Printing;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using Xpinn.Util;
using Xpinn.Indicadores.Services;
using Xpinn.Indicadores.Entities;


partial class Lista : GlobalWeb
{
    private Xpinn.Indicadores.Services.IndicadorCarteraService CarteraCategoriasServicio = new Xpinn.Indicadores.Services.IndicadorCarteraService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(CarteraCategoriasServicio.CodigoProgramaPagadurias, "L");
            Site toolBar = (Site)this.Master;
            txtColorFondo.eventoCambiar += txtColorFondo_TextChanged;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CarteraCategoriasServicio.CodigoProgramaPagadurias, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CargarDropDown();
            txtFecIni.ToDateTime = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
            txtFecFin.ToDateTime = System.DateTime.Now;
            if (Session[CarteraCategoriasServicio.CodigoProgramaPagadurias + ".id"] != null)
            {
                Usuario usuap = (Usuario)Session["usuario"];
            }
            btnInforme_Click(null, null);
        }
        
    }
    
    protected void CargarDropDown() 
    {
        //Xpinn.Indicadores.Services.CarteraOficinasService carteraOficinaServicio = new Xpinn.Indicadores.Services.CarteraOficinasService();
        //List<CarteraOficinas> lstFechas = new List<CarteraOficinas>();
        //lstFechas = carteraOficinaServicio.consultarfecha((Usuario)Session["Usuario"]);
        //ddlFecCorte.DataSource = lstFechas;
        //ddlFecCorte.DataValueField = "fecha_corte";
        //ddlFecCorte.DataTextField = "fecha_corte";
        //ddlFecCorte.DataBind();

        Xpinn.FabricaCreditos.Services.OficinaService oficinaServicio = new Xpinn.FabricaCreditos.Services.OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
        ddloficina.DataTextField = "nombre";
        ddloficina.DataValueField = "codigo";
        ddloficina.DataSource = oficinaServicio.ListarOficinas(oficina, (Usuario)Session["usuario"]);
        ddloficina.DataBind();

        if (ddlTipoGrafica.SelectedItem != null)
            ddlTipoGrafica.SelectedValue = "1";
    }
    

    protected void btnInforme_Click(object sender, EventArgs e)
    {
        gvDatos.Visible = true;
        if (!txtFecIni.TieneDatos)
        {
            VerError("Debe ingresar la fecha inicial del período");
            return;
        }
        if (!txtFecFin.TieneDatos)
        {
            VerError("Debe ingresar la fecha final del período");
            return;
        }
        DateTime pFechaIni, pFechaFin;
        pFechaFin = txtFecFin.ToDateTime;
        pFechaIni = txtFecIni.ToDateTime;

        // Generar columnas del DATATABLE y de la GRIDVIEW
        DataTable dtDatos = new DataTable();
        dtDatos.Clear();
        dtDatos.Columns.Add("nombre", typeof(string));
        dtDatos.Columns["nombre"].AllowDBNull = true;
        dtDatos.Columns["nombre"].DefaultValue = "0";
        dtDatos.Columns.Add("valor", typeof(decimal));
        
        List<IndicadorCartera> LstDetalleComprobante = new List<IndicadorCartera>();
        LstDetalleComprobante = CarteraCategoriasServicio.IngresosPagadurias(pFechaIni, pFechaFin, (Usuario)Session["Usuario"]);

        //llenando la tabla
        foreach (IndicadorCartera rFila in LstDetalleComprobante)
        {
            DataRow drDatos;
            drDatos = dtDatos.NewRow();
            if (rFila.nombre != null)
            {
                drDatos[0] = rFila.nombre;
                drDatos[1] = rFila.valor;
                dtDatos.Rows.Add(drDatos);
            }
        }

        gvDatos.DataSource = dtDatos;
        gvDatos.DataBind();

        // Haciendo visible las gráficas
        Chart1.Visible = true;
        // --------------------------------------------------------------------------------------------------------------------------------------------
        // Mostrar la primera gráfica
        // -------------------------------------------------------------------------------------------------------------------------------------------
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        Chart1.DataSource = dtDatos;
        // Determinar si se genera por totales o por número
        Chart1.Titles["Title1"].Text = "INGRESOS RECIBIDOS POR PAGADURIAS";
        Chart1.Legends[0].Enabled = chkmostrarlegendas.Checked;
        Chart1.Series["Series1"].ChartType = TipoGrafica(ddlTipoGrafica);
        if (ddlTipoGrafica.SelectedValue == "1")
            Chart1.Series["Series1"].LegendText = txtFecFin.Text;
        Chart1.Series["Series1"].YValueMembers = "valor";
        Chart1.Series["Series1"].XValueMember = "nombre";
        Chart1.Series["Series1"].LabelMapAreaAttributes = "valor";
        Chart1.Series["Series1"].IsValueShownAsLabel = true;
        Chart1.Series["Series1"].LabelFormat = "n0";
        // Mostrar la gráfica en pantalla        
        Chart1.DataBind();
    }

    protected void ddlFecCorte_SelectedIndexChanged(object sender, EventArgs e)
    {
        Generar();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        // Exportar la gridView
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=IngresosDatafonos.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = System.Text.Encoding.Default; //Encoding.Default;
        StringWriter sw = new StringWriter();
        ExpGrilla expGrilla = new ExpGrilla();
        sw = expGrilla.ObtenerGrilla(gvDatos, null);
        Response.Write("<div>" + expGrilla.style + "</div>");
        Response.Output.Write("<div>" + sw.ToString() + "</div>");
        Response.Flush();
        // Exportar la gráfica
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

    protected void btnExportarData_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvDatos);
    }


    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=IngresosDatafonos.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.Default;
        StringWriter sw = new StringWriter();
        ExpGrilla expGrilla = new ExpGrilla();

        sw = expGrilla.ObtenerGrilla(GridView1, null);

        Response.Write(expGrilla.style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    private void Generar()
    {
        btnInforme_Click(null, null);
    }

    protected void ddlTipoGrafica_SelectedIndexChanged(object sender, EventArgs e)
    {
        Generar();
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

    protected void ch3D_CheckedChanged(object sender, EventArgs e)
    {
        Generar();
    }

    protected void MostrarAnotacion(Chart pChart, Int32 pItem, string pTexto, double pPosicion)
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
        anotacion.Text = pTexto + "%";
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
        btnInforme_Click(null, null);
    }
}
