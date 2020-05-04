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
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Drawing.Printing;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using Xpinn.Indicadores.Services;
using Xpinn.Indicadores.Entities;


partial class Nuevo : GlobalWeb
{
    private Xpinn.Indicadores.Services.IndicadoresAhorrosService IndicadoresAhorrosService = new Xpinn.Indicadores.Services.IndicadoresAhorrosService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[IndicadoresAhorrosService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(IndicadoresAhorrosService.CodigoPrograma, "E");
            else
                VisualizarOpciones(IndicadoresAhorrosService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            txtColorFondo.eventoCambiar += txtColorFondo_TextChanged;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(IndicadoresAhorrosService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session[IndicadoresAhorrosService.CodigoPrograma + ".id"] != null)
            {
                Usuario usuap = (Usuario)Session["usuario"];
            }
            List<IndicadoresAhorros> lstFechas = new List<IndicadoresAhorros>();
            IndicadoresAhorrosService AhorrosServicio = new IndicadoresAhorrosService();
            lstFechas = AhorrosServicio.consultarfecha((Usuario)Session["Usuario"]);
            ddlFechaCorte.DataSource = lstFechas;
            ddlFechaCorte.DataValueField = "fecha_corte";
            ddlFechaCorte.DataTextField = "fecha_corte";
            ddlFechaCorte.DataBind();
            LlenarComboTipoProducto();
            ch3D.Checked = true;
        }
        btnInforme_Click(null, null);
    }
    protected void LlenarComboTipoProducto()
    {
        Xpinn.Indicadores.Services.IndicadoresAhorrosService tipoproductoservices = new Xpinn.Indicadores.Services.IndicadoresAhorrosService();

        // Inicializar las variables        
        Usuario usuario = new Usuario();
        usuario = (Usuario)Session["usuario"];
        List<Xpinn.Indicadores.Entities.IndicadoresAhorros> lsttipo = new List<Xpinn.Indicadores.Entities.IndicadoresAhorros>();


        // Cargando listado de tipos de productos
        lsttipo = tipoproductoservices.ListarTipoProducto(usuario);
        ddlTipoProducto.DataTextField = "nom_tipo_producto";
        ddlTipoProducto.DataValueField = "tipo_producto";
        ddlTipoProducto.DataSource = lsttipo;
        ddlTipoProducto.DataBind();

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
        if (ddlPeriodo.SelectedValue == "4")
            fechaInicial = Convert.ToDateTime(ddlFechaCorte.SelectedValue).ToString(conf.ObtenerFormatoFecha());

        gvDatos.DataSource = null;
        gvDatos.DataBind();
        // Haciendo visible las gráficas
        Chart1.Visible = true;
        Chart2.Visible = true;
        List<IndicadoresAhorros> LstDetalleComprobante = new List<IndicadoresAhorros>();

        /// AHORROS A LA VISTA 
        if (ddlTipoProducto.SelectedItem.Text == "Ahorros" || ddlTipoProducto.SelectedItem.Text == "Ahorros Vista" || ddlTipoProducto.SelectedItem.Text == "AHORROS VISTA")
        {
            // Traer los datos según criterio seleccionado

            Xpinn.Indicadores.Services.IndicadoresAhorrosService LineaService = new Xpinn.Indicadores.Services.IndicadoresAhorrosService();
            List<Xpinn.Indicadores.Entities.IndicadoresAhorros> lstLinea = new List<Xpinn.Indicadores.Entities.IndicadoresAhorros>();
            lstLinea = LineaService.ListarLineaAhorro((Usuario)Session["Usuario"]);
            var lstProductos = (from p in lstLinea orderby p.codigo select new { p.codigo, p.descripcion }).ToList();

            LstDetalleComprobante = IndicadoresAhorrosService.consultarAhorros(fechaInicial, ddlFechaCorte.SelectedValue, (Usuario)Session["Usuario"]);
        }

        /// AHORROS PORGRAMADO 
        if (ddlTipoProducto.SelectedItem.Text == "Ahorro Programado" || ddlTipoProducto.SelectedItem.Text == "Ahorros Programado" || ddlTipoProducto.SelectedItem.Text == "AHORROS PROGRAMADO")
        {

            Xpinn.Indicadores.Services.IndicadoresAhorrosService LineaService = new Xpinn.Indicadores.Services.IndicadoresAhorrosService();
            List<Xpinn.Indicadores.Entities.IndicadoresAhorros> lstLinea = new List<Xpinn.Indicadores.Entities.IndicadoresAhorros>();
            lstLinea = LineaService.ListarLineaProgramado((Usuario)Session["Usuario"]);
            var lstProductos = (from p in lstLinea orderby p.codigo select new { p.codigo, p.descripcion }).ToList();

            // Traer los datos según criterio seleccionado

            LstDetalleComprobante = IndicadoresAhorrosService.consultarProgramado(fechaInicial, ddlFechaCorte.SelectedValue, (Usuario)Session["Usuario"]);
        }

        ///  CDATS
        if (ddlTipoProducto.SelectedItem.Text == "Cdats" || ddlTipoProducto.SelectedItem.Text == "CDATS")
        {
            Xpinn.Indicadores.Services.IndicadoresAhorrosService LineaService = new Xpinn.Indicadores.Services.IndicadoresAhorrosService();
            List<Xpinn.Indicadores.Entities.IndicadoresAhorros> lstLinea = new List<Xpinn.Indicadores.Entities.IndicadoresAhorros>();
            lstLinea = LineaService.ListarLineaCdat((Usuario)Session["Usuario"]);
            var lstProductos = (from p in lstLinea orderby p.codigo select new { p.codigo, p.descripcion }).ToList();

            // Traer los datos según criterio seleccionado

            LstDetalleComprobante = IndicadoresAhorrosService.consultarCdat(fechaInicial, ddlFechaCorte.SelectedValue, (Usuario)Session["Usuario"]);
        }
        // --------------------------------------------------------------------------------------------------------------------------------------------
        // Mostrar la primera gráfica
        // -------------------------------------------------------------------------------------------------------------------------------------------
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        // Determinar los títulos de la gráfica
        if (ddlTipoProducto.SelectedItem.Text == "Cdats" || ddlTipoProducto.SelectedItem.Text == "CDATS")
        {
            Chart1.Titles["Title1"].Text = "Evolución Cdats  a  " + ddlFechaCorte.SelectedValue;
            Chart1.Titles["Title2"].Text = "($ millones)";
        }
        else
        {
            Chart1.Titles["Title1"].Text = "Evolución Ahorros  a  " + ddlFechaCorte.SelectedValue;
            Chart1.Titles["Title2"].Text = "($ millones)";
        }
        // Cargar los datos a la gráfica
        List<IndicadoresAhorros> LstDetalleComprobantes = new List<IndicadoresAhorros>();
        foreach (var item in LstDetalleComprobante)
        {
            IndicadoresAhorros Lst = new IndicadoresAhorros();
            Lst.total = Math.Round(item.total / 1000000, 2);
            Lst.fecha_historico = item.fecha_historico;
            Lst.numero_cuentas = item.numero_cuentas;
            Lst.descripcion = item.descripcion;
            LstDetalleComprobantes.Add(Lst);
            Session["ComprovantesMillones"] = LstDetalleComprobantes;
            Session["SumaTotal"] = LstDetalleComprobantes.Sum(x => x.total);
        }

        Session["Comprobantes"] = LstDetalleComprobante;
        Session["SumaTotalComprobantes"] = LstDetalleComprobante.Sum(x => x.total);
        Chart1.DataSource = LstDetalleComprobante;
        lbltotal.Text = string.Format("{0:C}", Session["SumaTotalComprobantes"]);


        // Determinar el tipo de gráfica

        Chart1.Series["Series1"].ChartType = TipoGrafica(ddlTipoGrafica1);

        // Mostrar los nombres de las seríes
        Chart1.Series["Series1"].LegendText = "Total";
        Chart1.Series["Series1"].YValueMembers = "total";
        Chart1.Series["Series1"].XValueMember = "fecha_historico";
        Chart1.Series["Series1"].LabelMapAreaAttributes = "total";
        Chart1.Series["Series1"].IsValueShownAsLabel = true;
        Chart1.Series["Series1"]["LabelStyle"] = "Top";
        Chart1.Series["Series1"].LabelFormat = "{0:N0}";

        // Mostrar los nombres de las seríes
        //Chart1.Series["Series2"].LegendText = "Total";
        //Chart1.Series["Series2"].YValueMembers = "total";
        //Chart1.Series["Series2"].XValueMember = "fecha_historico";
        //Chart1.Series["Series2"].LabelMapAreaAttributes = "total";
        //Chart1.Series["Series2"].IsValueShownAsLabel = true;
        //Chart1.Series["Series2"]["LabelStyle"] = "Top";
        //Chart1.Series["Series2"].LabelFormat = "{0:N0}";

        Chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;
        // Mostrar la gráfica en pantalla
        Chart1.DataBind();
        // llenar gridview
        if (ddlTipoProducto.SelectedItem.Text == "Cdats" || ddlTipoProducto.SelectedItem.Text == "CDATS")
        {
            List<IndicadoresAhorros> LstDetalleComprobanteCdat = new List<IndicadoresAhorros>();

            LstDetalleComprobanteCdat = IndicadoresAhorrosService.consultarCdat(fechaInicial, ddlFechaCorte.SelectedValue, (Usuario)Session["Usuario"]);
            gvDatos.DataSource = LstDetalleComprobanteCdat;
            gvDatos.DataBind();
        }
        if (ddlTipoProducto.SelectedItem.Text == "Ahorros" || ddlTipoProducto.SelectedItem.Text == "Ahorros Vista" || ddlTipoProducto.SelectedItem.Text == "AHORROS VISTA")
        {
            List<IndicadoresAhorros> LstDetalleComprobanteAho = new List<IndicadoresAhorros>();

            LstDetalleComprobanteAho = IndicadoresAhorrosService.consultarAhorros(fechaInicial, ddlFechaCorte.SelectedValue, (Usuario)Session["Usuario"]);
            gvDatos.DataSource = LstDetalleComprobanteAho;
            gvDatos.DataBind();
        }
        if (ddlTipoProducto.SelectedItem.Text == "Ahorro Programado" || ddlTipoProducto.SelectedItem.Text == "Ahorros Programado" || ddlTipoProducto.SelectedItem.Text == "AHORROS PROGRAMADO")
        {
            List<IndicadoresAhorros> LstDetalleComprobanteProg = new List<IndicadoresAhorros>();

            LstDetalleComprobanteProg = IndicadoresAhorrosService.consultarProgramado(fechaInicial, ddlFechaCorte.SelectedValue, (Usuario)Session["Usuario"]);
            gvDatos.DataSource = LstDetalleComprobanteProg;
            gvDatos.DataBind();
        }


        // --------------------------------------------------------------------------------------------------------------------------------------------
        // Mostrar la segunda gráfica    
        // -------------------------------------------------------------------------------------------------------------------------------------------
        Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        // Determinar los títulos de la gráfica
        if (ddlTipoProducto.SelectedItem.Text == "Cdats" || ddlTipoProducto.SelectedItem.Text == "CDATS")
        {
            Chart2.Titles["Title1"].Text = "Evolución Cdats  a  " + ddlFechaCorte.SelectedValue;
            Chart2.Titles["Title2"].Text = "(# Número)";
        }
        else
        {
            Chart2.Titles["Title1"].Text = "Evolución Ahorros  a  " + ddlFechaCorte.SelectedValue;
            Chart2.Titles["Title2"].Text = "(# Número)";
        }
        // Cargar los datos a la gráfica
        Chart2.DataSource = LstDetalleComprobante;
        // Determinar el tipo de gráfica

        // Determinar los títulos de la gráfica
        Chart2.Series["Series1"].ChartType = TipoGrafica(ddlTipoGrafica2);


        // Mostrar los nombres de las seríes
        Chart2.Series["Series1"].LegendText = "Total";
        Chart2.Series["Series1"].LabelMapAreaAttributes = "numero_cuentas";
        Chart2.Series["Series1"].IsValueShownAsLabel = true;
        Chart2.Series["Series1"].YValueMembers = "numero_cuentas";
        Chart2.Series["Series1"].XValueMember = "fecha_historico";

        // Mostrar los nombres de las seríes
        //Chart2.Series["Series2"].LegendText = "Total";
        //Chart2.Series["Series2"].LabelMapAreaAttributes = "numero_cuentas";
        //Chart2.Series["Series2"].IsValueShownAsLabel = true;
        //Chart2.Series["Series2"].YValueMembers = "numero_cuentas";
        //Chart2.Series["Series2"].XValueMember = "fecha_historico";

        // Refrescar datos de la gráfica en pantalla
        Chart2.DataBind();


        // llenar gridview
        if (ddlTipoProducto.SelectedItem.Text == "Cdats" || ddlTipoProducto.SelectedItem.Text == "CDATS")
        {
            List<IndicadoresAhorros> LstDetalleComprobanteCdat = new List<IndicadoresAhorros>();

            LstDetalleComprobanteCdat = IndicadoresAhorrosService.consultarCdat(fechaInicial, ddlFechaCorte.SelectedValue, (Usuario)Session["Usuario"]);
            gvCantidad.DataSource = LstDetalleComprobanteCdat;
            gvCantidad.DataBind();
            Session["DettalleComprobante"] = LstDetalleComprobanteCdat.Sum(x => x.numero_cuentas);
            lbltotalcantidad.Text = Convert.ToString(Session["DettalleComprobante"]);
        }
        if (ddlTipoProducto.SelectedItem.Text == "Ahorros" || ddlTipoProducto.SelectedItem.Text == "Ahorros Vista" || ddlTipoProducto.SelectedItem.Text == "AHORROS VISTA")
        {
            List<IndicadoresAhorros> LstDetalleComprobanteAho = new List<IndicadoresAhorros>();
            LstDetalleComprobanteAho = IndicadoresAhorrosService.consultarAhorros(fechaInicial, ddlFechaCorte.SelectedValue, (Usuario)Session["Usuario"]);
            gvCantidad.DataSource = LstDetalleComprobanteAho;
            gvCantidad.DataBind();
            Session["DettalleComprobante"] = LstDetalleComprobanteAho.Sum(x => x.numero_cuentas);
            lbltotalcantidad.Text = Convert.ToString(Session["DettalleComprobante"]);
        }
        if (ddlTipoProducto.SelectedItem.Text == "Ahorro Programado" || ddlTipoProducto.SelectedItem.Text == "Ahorros Programado" || ddlTipoProducto.SelectedItem.Text == "AHORROS PROGRAMADO")
        {
            List<IndicadoresAhorros> LstDetalleComprobanteProg = new List<IndicadoresAhorros>();
            LstDetalleComprobanteProg = IndicadoresAhorrosService.consultarProgramado(fechaInicial, ddlFechaCorte.SelectedValue, (Usuario)Session["Usuario"]);
            gvCantidad.DataSource = LstDetalleComprobanteProg;
            gvCantidad.DataBind();
            Session["DettalleComprobante"] = LstDetalleComprobanteProg.Sum(x => x.numero_cuentas);
            lbltotalcantidad.Text = Convert.ToString(Session["DettalleComprobante"]);
        }

        chKMillones_CheckedChanged(null, null);
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

    protected void Chart1_Load(object sender, EventArgs e)
    {

    }

    protected void ddlTipoGrafica1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Generar();
    }

    protected void ddlTipoGrafica2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Generar();
    }

    protected void chKMillones_CheckedChanged(object sender, EventArgs e)
    {
        if (chKMillones.Checked)
        {
            Chart1.DataSource = Session["ComprovantesMillones"];
            gvDatos.DataSource = Session["ComprovantesMillones"];
            Chart1.DataBind();
            gvDatos.DataBind();
            lbltotal.Text = Convert.ToString(Session["SumaTotal"]);
            lbltotalcantidad.Text = Convert.ToString(Session["DettalleComprobante"]);
            return;
        }
        else
        {

            gvDatos.DataSource = Session["Comprobantes"];
            Chart1.DataSource = Session["Comprobantes"];
            Chart1.DataBind();
            gvDatos.DataBind();
            lbltotal.Text = string.Format("{0:C}", Session["SumaTotalComprobantes"]);
            lbltotalcantidad.Text = Convert.ToString(Session["DettalleComprobante"]);
            return;
        }

    }

}

