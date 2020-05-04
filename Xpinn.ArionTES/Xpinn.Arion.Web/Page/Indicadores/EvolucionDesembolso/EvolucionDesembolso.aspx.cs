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
            if (Session[EvolucionDesembolsoService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(EvolucionDesembolsoService.CodigoPrograma, "E");
            else
                VisualizarOpciones(EvolucionDesembolsoService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            txtColorFondo.eventoCambiar += txtColorFondo_TextChanged;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EvolucionDesembolsoService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarDropDown();
            if (Session[EvolucionDesembolsoService.CodigoPrograma + ".id"] != null)
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
            btnInforme_Click(null, null);
        }        
    }

    protected void CargarDropDown()
    {
        Xpinn.FabricaCreditos.Services.LineasCreditoService BOLinea = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        List<Xpinn.FabricaCreditos.Entities.LineasCredito> lstLineas = new List<Xpinn.FabricaCreditos.Entities.LineasCredito>();
        Xpinn.FabricaCreditos.Entities.LineasCredito pEntidad = new Xpinn.FabricaCreditos.Entities.LineasCredito();
        pEntidad.estado = 1;
        lstLineas = BOLinea.ListarLineasCredito(pEntidad, (Usuario)Session["usuario"]);
        if (lstLineas.Count > 0)
        {
            ddlLinea.DataSource = lstLineas;
            ddlLinea.DataTextField = "nom_linea_credito";
            ddlLinea.DataValueField = "cod_linea_credito";
            ddlLinea.DataBind();
        }
    }

    protected string obtfiltro()
    {
        string filtrare = "";
        if (ddlLinea.SelectedText != "")
        {
            filtrare += " and cod_linea_Credito In (";
            string[] sLineas = ddlLinea.SelectedText.Split(',');
            for (int i = 0; i < sLineas.Count(); i++)
            {
                string[] sDato = sLineas[i].ToString().Split('-');
                if (i > 0)
                    filtrare += ", ";
                filtrare += "'" + sDato[0].Trim() + "'";
            }
            filtrare += ") ";
        }
        return filtrare;
    }
 

    protected void btnInforme_Click(object sender, EventArgs e)
    {
        Chart1.Visible = true;
        Chart2.Visible = true;

        Configuracion conf = new Configuracion();

        Xpinn.Cartera.Services.ClasificacionCarteraService clasificacionService = new Xpinn.Cartera.Services.ClasificacionCarteraService();
        List<Xpinn.Cartera.Entities.ClasificacionCartera> lstClasificacion = new List<Xpinn.Cartera.Entities.ClasificacionCartera>();
        lstClasificacion = clasificacionService.ListarClasificacion((Usuario)Session["Usuario"]);
        var lstProductos = (from p in lstClasificacion orderby p.codigo select new { p.codigo, p.descripcion }).ToList();

        string fechaInicial = "";
        if (ddlPeriodo.SelectedValue == "1")
            fechaInicial = Convert.ToDateTime(ddlFechaCorte.SelectedValue).AddDays(-365).ToString(conf.ObtenerFormatoFecha());
        if (ddlPeriodo.SelectedValue == "2")
            fechaInicial = Convert.ToDateTime(ddlFechaCorte.SelectedValue).AddDays(-180).ToString(conf.ObtenerFormatoFecha());
        if (ddlPeriodo.SelectedValue == "3")
            fechaInicial = Convert.ToDateTime(ddlFechaCorte.SelectedValue).AddDays(-90).ToString(conf.ObtenerFormatoFecha());
        if (ddlPeriodo.SelectedValue == "4")
            fechaInicial = Convert.ToDateTime(ddlFechaCorte.SelectedValue).AddDays(-30).ToString(conf.ObtenerFormatoFecha());

        string Filtro = obtfiltro();
        
        List<EvolucionDesembolsos> LstDetalleComprobante = new List<EvolucionDesembolsos>();
        LstDetalleComprobante = EvolucionDesembolsoService.consultarDesembolso(fechaInicial, ddlFechaCorte.SelectedValue,Filtro, (Usuario)Session["Usuario"]);
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

        // Determinar el tipo de gráfica
        Chart1.Series["Series1"].ChartType = TipoGrafica(ddlTipoGrafica1);
        Chart1.Series["Series2"].ChartType = TipoGrafica(ddlTipoGrafica1);
        Chart1.Series["Series3"].ChartType = TipoGrafica(ddlTipoGrafica1);
        Chart1.Series["Series4"].ChartType = TipoGrafica(ddlTipoGrafica1);
        Chart1.Series["Series5"].ChartType = TipoGrafica(ddlTipoGrafica1);

        //if (ddlPeriodo.SelectedValue == "3")
        //{
        //    Chart1.Series["Series1"].ChartType = SeriesChartType.Column;
        //    Chart1.Series["Series2"].ChartType = SeriesChartType.Column;
        //    Chart1.Series["Series3"].ChartType = SeriesChartType.Column;
        //    Chart1.Series["Series4"].ChartType = SeriesChartType.Column;
        //}
        //else
        //{
        //    Chart1.Series["Series1"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
        //    Chart1.Series["Series2"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
        //    Chart1.Series["Series3"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
        //    Chart1.Series["Series4"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);

        //}
        // Mostrar los nombres de las seríes
        Chart1.Series["Series1"].LegendText = "Total";
        Chart1.Series["Series1"].YValueMembers = "total_desembolsos";
        Chart1.Series["Series1"].XValueMember = "fecha_historico";
        Chart1.Series["Series1"].LabelMapAreaAttributes = "total_desembolsos";
        Chart1.Series["Series1"].IsValueShownAsLabel = true;
        Chart1.Series["Series1"]["LabelStyle"] = "Top";
        Chart1.Series["Series1"].LabelFormat = "{0:N0}";
        
        Chart1.Series["Series2"].LegendText = lstProductos[3].descripcion;  
        Chart1.Series["Series2"].YValueMembers = "total_desembolsos_microcredito";
        Chart1.Series["Series2"].XValueMember = "fecha_historico";
        Chart1.Series["Series2"].LabelMapAreaAttributes = "total_desembolsos_microcredito";
        Chart1.Series["Series2"].IsValueShownAsLabel = true;
        Chart1.Series["Series2"]["LabelStyle"] = "Top";
        Chart1.Series["Series2"].LabelFormat = "{0:N0}";

        Chart1.Series["Series3"].LegendText = lstProductos[0].descripcion;
        Chart1.Series["Series3"].YValueMembers = "total_desembolsos_consumo";
        Chart1.Series["Series3"].XValueMember = "fecha_historico";
        Chart1.Series["Series3"].LabelMapAreaAttributes = "total_desembolsos_consumo";
        Chart1.Series["Series3"].IsValueShownAsLabel = true;
        Chart1.Series["Series3"]["LabelStyle"] = "Top";
        Chart1.Series["Series3"].LabelFormat = "{0:N0}";

        Chart1.Series["Series4"].LegendText = lstProductos[2].descripcion;
        Chart1.Series["Series4"].YValueMembers = "total_desembolsos_vivienda";
        Chart1.Series["Series4"].XValueMember = "fecha_historico";
        Chart1.Series["Series4"].LabelMapAreaAttributes = "total_desembolsos_vivienda";
        Chart1.Series["Series4"].IsValueShownAsLabel = true;
        Chart1.Series["Series4"]["LabelStyle"] = "Top";
        Chart1.Series["Series4"].LabelFormat = "{0:N0}";

        Chart1.Series["Series5"].LegendText = lstProductos[1].descripcion;
        Chart1.Series["Series5"].YValueMembers = "total_desembolsos_comercial";
        Chart1.Series["Series5"].XValueMember = "fecha_historico";
        Chart1.Series["Series5"].LabelMapAreaAttributes = "total_desembolsos_comercial";
        Chart1.Series["Series5"].IsValueShownAsLabel = true;
        Chart1.Series["Series5"]["LabelStyle"] = "Top";
        Chart1.Series["Series5"].LabelFormat = "{0:N0}";

        Chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;
        // Mostrar la gráfica en pantalla
        Chart1.DataBind();

        // Traer los dato según criterio seleccionado
        List<EvolucionDesembolsos> LstDetalleComprobantec = new List<EvolucionDesembolsos>();
        LstDetalleComprobantec = EvolucionDesembolsoService.consultarDesembolso(fechaInicial, ddlFechaCorte.SelectedValue, Filtro, (Usuario)Session["Usuario"]);
        gvDatos.DataSource = LstDetalleComprobantec;
        gvDatos.DataBind();

        // --------------------------------------------------------------------------------------------------------------------------------------------
        // Mostrar la segunda gráfica    
        // -------------------------------------------------------------------------------------------------------------------------------------------
        Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        Chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        // Determinar los títulos de la gráfica
        Chart2.Titles["Title1"].Text = "Evolución de Créditos " + ddlFechaCorte.SelectedValue;
        Chart2.Titles["Title2"].Text = "# Número";
        // Cargar los datos a la gráfica
        Chart2.DataSource = LstDetalleComprobante;
        //// Determinar el tipo de gráfica


        // Determinar el tipo de gráfica
        Chart2.Series["Series1"].ChartType = TipoGrafica(ddlTipoGrafica2);
        Chart2.Series["Series2"].ChartType = TipoGrafica(ddlTipoGrafica2);
        Chart2.Series["Series3"].ChartType = TipoGrafica(ddlTipoGrafica2);
        Chart2.Series["Series4"].ChartType = TipoGrafica(ddlTipoGrafica2);
        Chart2.Series["Series5"].ChartType = TipoGrafica(ddlTipoGrafica2);
        //if (ddlPeriodo.SelectedValue == "3")
        //{
        //    Chart2.Series["Series1"].ChartType = SeriesChartType.Column;
        //    Chart2.Series["Series2"].ChartType = SeriesChartType.Column;
        //    Chart2.Series["Series3"].ChartType = SeriesChartType.Column;
        //    Chart2.Series["Series4"].ChartType = SeriesChartType.Column;
        //}
        //else
        //{
        //    Chart2.Series["Series1"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
        //    Chart2.Series["Series2"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
        //    Chart2.Series["Series3"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
        //    Chart2.Series["Series4"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);

        //}
        // Mostrar los nombres de las seríes
        Chart2.Series["Series1"].LegendText = "Total";
        Chart2.Series["Series1"].LabelMapAreaAttributes = "numero_desembolsos";
        Chart2.Series["Series1"].IsValueShownAsLabel = true;
        Chart2.Series["Series1"].YValueMembers = "numero_desembolsos";
        Chart2.Series["Series1"].XValueMember = "fecha_historico";

        Chart2.Series["Series2"].LegendText = lstProductos[3].descripcion;  
        Chart2.Series["Series2"].YValueMembers = "numero_desembolsos_microcredito";
        Chart2.Series["Series2"].XValueMember = "fecha_historico";
        Chart2.Series["Series2"].LabelMapAreaAttributes = "numero_desembolsos_microcredito";
        Chart2.Series["Series2"].IsValueShownAsLabel = true;
        Chart2.Series["Series2"]["LabelStyle"] = "Top";

        Chart2.Series["Series3"].LegendText = lstProductos[0].descripcion;
        Chart2.Series["Series3"].YValueMembers = "numero_desembolsos_consumo";
        Chart2.Series["Series3"].XValueMember = "fecha_historico";
        Chart2.Series["Series3"].LabelMapAreaAttributes = "numero_desembolsos_consumo";
        Chart2.Series["Series3"].IsValueShownAsLabel = true;
        Chart2.Series["Series3"]["LabelStyle"] = "Top";

        Chart2.Series["Series4"].LegendText = lstProductos[2].descripcion;
        Chart2.Series["Series4"].YValueMembers = "numero_desembolsos_vivienda";
        Chart2.Series["Series4"].XValueMember = "fecha_historico";
        Chart2.Series["Series4"].LabelMapAreaAttributes = "numero_desembolsos_vivienda";
        Chart2.Series["Series4"].IsValueShownAsLabel = true;
        Chart2.Series["Series4"]["LabelStyle"] = "Top";

        Chart2.Series["Series5"].LegendText = lstProductos[1].descripcion;
        Chart2.Series["Series5"].YValueMembers = "numero_desembolsos_comercial";
        Chart2.Series["Series5"].XValueMember = "fecha_historico";
        Chart2.Series["Series5"].LabelMapAreaAttributes = "numero_desembolsos_comercial";
        Chart2.Series["Series5"].IsValueShownAsLabel = true;
        Chart2.Series["Series5"]["LabelStyle"] = "Top";
        Chart2.Series["Series5"].LabelFormat = "{0:N0}";
        // Refrescar datos de la gráfica en pantalla
        Chart2.DataBind();


        // Traer los dato según criterio seleccionado
        List<EvolucionDesembolsos> LstDetalleComprobantecan = new List<EvolucionDesembolsos>();
        LstDetalleComprobantecan = EvolucionDesembolsoService.consultarDesembolso(fechaInicial, ddlFechaCorte.SelectedValue, Filtro, (Usuario)Session["Usuario"]);
         gvCantidad.DataSource = LstDetalleComprobantecan;
        gvCantidad.DataBind();


    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=grilla.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.Default;
        StringWriter sw = new StringWriter();
        ExpGrilla expGrilla = new ExpGrilla();

        sw = expGrilla.ObtenerGrilla(gvDatos, null);

        Response.Write("<div>" + expGrilla.style + "</div>");
        Response.Output.Write("<div>" + sw.ToString() + "</div>");
        Response.Flush();
        btnInforme_Click(null, null);

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
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void btnExportarpor_Click(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=grilla.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.Default;
        StringWriter sw = new StringWriter();
        ExpGrilla expGrilla = new ExpGrilla();

        sw = expGrilla.ObtenerGrilla(gvCantidad, null);

        Response.Write("<div>" + expGrilla.style + "</div>");
        Response.Output.Write("<div>" + sw.ToString() + "</div>");
        Response.Flush();
        btnInforme_Click(null, null);

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
    private void Generar()
    {
        btnInforme_Click(null, null);
    }
    protected void ddlTipoGrafica1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Generar();
    }

    protected void ddlTipoGrafica2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Generar();
    }


}
