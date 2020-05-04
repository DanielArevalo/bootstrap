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

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[IndicadoresAportesService.CodigoProgramaGestionAsociados + ".id"] != null)
                VisualizarOpciones(IndicadoresAportesService.CodigoProgramaGestionAsociados, "E");
            else
                VisualizarOpciones(IndicadoresAportesService.CodigoProgramaGestionAsociados, "A");

            Site toolBar = (Site)this.Master;
            txtColorFondo.eventoCambiar += txtColorFondo_TextChanged;
    
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(IndicadoresAportesService.CodigoProgramaGestionAsociados, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session[IndicadoresAportesService.CodigoProgramaGestionAsociados + ".id"] != null)
            {
                Usuario usuap = (Usuario)Session["usuario"];
            }
            List<IndicadoresAportes> lstFechas = new List<IndicadoresAportes>();
            IndicadoresAportesService aportesServicio = new IndicadoresAportesService();
            lstFechas = aportesServicio.consultarfechaAfiliacion((Usuario)Session["Usuario"]);
            ddlFechaCorte.DataSource = lstFechas;
            ddlFechaCorte.DataValueField = "año";
            ddlFechaCorte.DataTextField = "año";
            ddlFechaCorte.DataBind();
        }
        btnInforme_Click(null, null);
    }
    protected void btnInforme_Click(object sender, EventArgs e)
    {
        if (ddlPeriodo.SelectedValue != "")
        {
          //  gvDatos1.Visible = true;
            //string filtro = obtfiltro();
            string pOrden = "";
            // Haciendo visible las gráficas
            Chart1.Visible = true;

            // Traer los datos según criterio seleccionado
            List<IndicadoresAportes> LstDetalleComprobante= new List<IndicadoresAportes>();
            List<IndicadoresAportes> LstDetalleComprobantec = new List<IndicadoresAportes>();

            string fechafinal = ddlFechaCorte.SelectedValue;
            LstDetalleComprobante = IndicadoresAportesService.consultarAfiliacion(fechafinal, (Usuario)Session["Usuario"]);
            // Si se seleccionó un segundo periodo entonces cargar los datos
            if (ddlPeriodo.Text.Trim() != "")
            {
                if ( ddlPeriodo.SelectedValue != "")
                {
                    LstDetalleComprobantec = IndicadoresAportesService.consultarAfiliacion(fechafinal, (Usuario)Session["Usuario"]);
                }
                foreach (IndicadoresAportes rFila_c in LstDetalleComprobantec)
                {
                    int contando = 0;
                    Boolean bEncontro = false;
                   
                    foreach (IndicadoresAportes rFila in LstDetalleComprobante)
                    {
                        
                        if (contando == 13)
                        {
                            rFila.mesgrafica = rFila_c.mesgrafica;
                            rFila.numero = rFila_c.numero;
                          }

                        if (rFila_c.mesgrafica.ToUpper() == rFila.mesgrafica.ToUpper())
                        {
                            rFila.mesgrafica = rFila_c.mesgrafica;
                            rFila.numero = rFila_c.numero;
                            bEncontro = true;
                            break;
                        }
                    }
                    if (bEncontro == false)
                    {
                        foreach (IndicadoresAportes rFila in LstDetalleComprobante)
                        {
                            rFila.mesgrafica = rFila_c.mesgrafica;
                            rFila.numero = rFila_c.numero;
                            
                        }
                    }
                }
            }

            // --------------------------------------------------------------------------------------------------------------------------------------------
            // Mostrar la primera gráfica
            // -------------------------------------------------------------------------------------------------------------------------------------------
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
            // Determinar el tipo de gráfica
            Chart1.Series["Series1"].ChartType = TipoGrafica(ddlTipoGrafica);
           // Chart1.Series["Series2"].ChartType = TipoGrafica(ddlTipoGrafica);
            Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            // Cargar los datos a la gráfica

            Chart1.DataSource = LstDetalleComprobante;
            // Determinar si se genera por totales o por número
            Chart1.Titles["Title1"].Text = "AFILIACIONES ";
           

            if (ddlTipoGrafica.Text == "2")
            {
                Chart1.Series["Series1"].YValueMembers = "numero";
                Chart1.Series["Series1"].XValueMember = "mesgrafica";
                Chart1.Series["Series1"].MarkerSize = 2;
                Chart1.Series["Series1"].LabelMapAreaAttributes = "numero";
                Chart1.Series["Series1"].IsValueShownAsLabel = true;
                CalloutAnnotation anotacion = new CalloutAnnotation();
                anotacion.Name = "Anotacion" + Chart1.Series["Series1"].LabelMapAreaAttributes;
                anotacion.Alignment = System.Drawing.ContentAlignment.TopCenter;
                anotacion.Font = new Font("Microsoft Sans Serif", 2);
                anotacion.ForeColor = System.Drawing.Color.White;
                anotacion.ResizeToContent();
                Chart1.Series["Series1"].BorderWidth = 12;
                //Chart1.Annotations.Add(anotacion);

            }
            else
            {

                // Mostrar los nombres de las seríes
                if (ddlPeriodo.Text.Trim() != "")
                {
                    Chart1.Series["Series1"].LegendText = "numero";
                    Chart1.Series["Series1"].IsVisibleInLegend = true;
                  //  Chart1.Series["Series2"].LegendText = "mesgrafica";
                   // Chart1.Series["Series2"].IsVisibleInLegend = true;
                   
                }
                
                Chart1.Series["Series1"].YValueMembers = "numero";
                Chart1.Series["Series1"].XValueMember = "mesgrafica";
                Chart1.Series["Series1"].LabelMapAreaAttributes = "numero";
                Chart1.Series["Series1"].IsValueShownAsLabel = true;
            }
         
           

            // Mostrar la gráfica en pantalla
            Chart1.DataBind();
            LstDetalleComprobante = IndicadoresAportesService.consultarAfiliacion(fechafinal, (Usuario)Session["Usuario"]);
            gvDatosAfiliaciones.DataSource = LstDetalleComprobante;
            gvDatosAfiliaciones.DataBind();

        }
        else
        {
          
            // Haciendo visible las gráficas
            Chart1.Visible = true;

            // Traer los datos según criterio seleccionado
            List<IndicadoresAportes> LstDetalleComprobante_c = new List<IndicadoresAportes>();
            List<IndicadoresAportes> LstDetalleComprobante = new List<IndicadoresAportes>();
            string fechafinal = ddlFechaCorte.SelectedValue;
           
            LstDetalleComprobante = IndicadoresAportesService.consultarAfiliacion(fechafinal, (Usuario)Session["Usuario"]);
          //  LstDetalleComprobante = IndicadoresAportesService.consultarAfiliaciones(filtro, pOrden, ddlVencimmiento.SelectedValue, fecha1, (Usuario)Session["Usuario"]);
            // Si se seleccionó un segundo periodo entonces cargar los datos
            if (ddlPeriodo.Text.Trim() != "")
            {
                if (ddlPeriodo.SelectedValue != "")
                {
                    LstDetalleComprobante_c = IndicadoresAportesService.consultarAfiliacion(fechafinal, (Usuario)Session["Usuario"]);
                }
                foreach (IndicadoresAportes rFila_c in LstDetalleComprobante_c)
                {
                    Boolean bEncontro = false;
                    foreach (IndicadoresAportes rFila in LstDetalleComprobante)
                    {
                        if (rFila_c.mesgrafica.ToUpper() == rFila.mesgrafica.ToUpper())
                        {   
                            rFila.mesgrafica = rFila_c.mesgrafica;
                        rFila.numero = rFila_c.numero;
                        bEncontro = true;
                        break;
                        }
                    }
                    if (bEncontro == false)
                    {
                        IndicadoresAportes rFilaNueva = new IndicadoresAportes();
                        rFilaNueva.mesgrafica = "";
                        rFilaNueva.numero = 0;                        
                        LstDetalleComprobante.Add(rFilaNueva);
                    }
                }
            }

            // --------------------------------------------------------------------------------------------------------------------------------------------
            // Mostrar la primera gráfica
            // -------------------------------------------------------------------------------------------------------------------------------------------
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
            // Determinar el tipo de gráfica
            Chart1.Series["Series1"].ChartType = TipoGrafica(ddlTipoGrafica);
           // Chart1.Series["Series2"].ChartType = TipoGrafica(ddlTipoGrafica);
            Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            // Cargar los datos a la gráfica
            Chart1.DataSource = LstDetalleComprobante;
            // Determinar si se genera por totales o por número
            Chart1.Titles["Title1"].Text = "AFILIACIONES "; ;
          
            if (ddlTipoGrafica.Text == "2")
            {
                Chart1.Series["Series1"].YValueMembers = "numero";
                Chart1.Series["Series1"].XValueMember = "mesgrafica";
                Chart1.Series["Series1"].LabelMapAreaAttributes = "numero";
                Chart1.Series["Series1"].IsValueShownAsLabel = false;
            }
            else
            {

                // Mostrar los nombres de las seríes
                if (ddlPeriodo.Text.Trim() != "")
                {
                    Chart1.Series["Series1"].LegendText = "mesgrafica";
                    Chart1.Series["Series1"].IsVisibleInLegend = true;
                    Chart1.Series["Series2"].LegendText = "numero";
                    Chart1.Series["Series2"].IsVisibleInLegend = true;
                   
                }
               
                Chart1.Series["Series1"].YValueMembers = "numero";
                Chart1.Series["Series1"].XValueMember = "mesgrafica";
                Chart1.Series["Series1"].LabelMapAreaAttributes = "numero";
                Chart1.Series["Series1"].IsValueShownAsLabel = false;
            }
          
          

            // Mostrar la gráfica en pantalla
            Chart1.DataBind();
            LstDetalleComprobante = IndicadoresAportesService.consultarAfiliacion(fechafinal, (Usuario)Session["Usuario"]);
            gvDatosAfiliaciones.DataSource = LstDetalleComprobante;
             gvDatosAfiliaciones.DataBind();
        }

        //////////////////////////RETIROS //////////////////////////
        if (ddlPeriodo.SelectedValue != "")
        {
            //  gvDatos1.Visible = true;
            //string filtro = obtfiltro();
            string pOrden = "";
            // Haciendo visible las gráficas
            Chart2.Visible = true;

            // Traer los datos según criterio seleccionado
            List<IndicadoresAportes> LstDetalleComprobanteAf = new List<IndicadoresAportes>();
            string fechafinal = ddlFechaCorte.SelectedValue;
            //   LstDetalleComprobante = IndicadoresAportesService.consultarAfiliaciones(fechafinal, (Usuario)Session["Usuario"]);
            // Si se seleccionó un segundo periodo entonces cargar los datos
            if (ddlPeriodo.Text.Trim() != "")
            {
                if (ddlPeriodo.SelectedValue != "")
                {
                    LstDetalleComprobanteAf = IndicadoresAportesService.consultarRetiro(fechafinal, (Usuario)Session["Usuario"]);
                }
                foreach (IndicadoresAportes rFila_c in LstDetalleComprobanteAf)
                {
                    int contando = 0;
                    Boolean bEncontro = false;

                    foreach (IndicadoresAportes rFila in LstDetalleComprobanteAf)
                    {

                        if (contando == 13)
                        {
                            rFila.mesgrafica = rFila_c.mesgrafica;
                            rFila.numero = rFila_c.numero;
                        }

                        if (rFila_c.mesgrafica.ToUpper() == rFila.mesgrafica.ToUpper())
                        {
                        rFila.mesgrafica = rFila_c.mesgrafica;
                        rFila.numero = rFila_c.numero;
                        bEncontro = true;
                        break;
                        }
                    }
                    if (bEncontro == false)
                    {
                        foreach (IndicadoresAportes rFila in LstDetalleComprobanteAf)
                        {
                            rFila.mesgrafica = rFila_c.mesgrafica;
                            rFila.numero = rFila_c.numero;

                        }
                    }
                }
            }

            // --------------------------------------------------------------------------------------------------------------------------------------------
            // Mostrar la primera gráfica
            // -------------------------------------------------------------------------------------------------------------------------------------------
            Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
            // Determinar el tipo de gráfica
            Chart2.Series["Series1"].ChartType = TipoGrafica(ddlTipoGrafica);
            // Chart1.Series["Series2"].ChartType = TipoGrafica(ddlTipoGrafica);
            Chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            // Cargar los datos a la gráfica

            Chart2.DataSource = LstDetalleComprobanteAf;
            // Determinar si se genera por totales o por número
            Chart2.Titles["Title1"].Text = "RETIROS ";


            if (ddlTipoGrafica.Text == "2")
            {
                Chart2.Series["Series1"].YValueMembers = "numero";
                Chart2.Series["Series1"].XValueMember = "mesgrafica";
                Chart2.Series["Series1"].MarkerSize = 2;
                Chart2.Series["Series1"].LabelMapAreaAttributes = "numero";
                Chart2.Series["Series1"].IsValueShownAsLabel = true;
                CalloutAnnotation anotacion = new CalloutAnnotation();
                anotacion.Name = "Anotacion" + Chart2.Series["Series1"].LabelMapAreaAttributes;
                anotacion.Alignment = System.Drawing.ContentAlignment.TopCenter;
                anotacion.Font = new Font("Microsoft Sans Serif", 2);
                anotacion.ForeColor = System.Drawing.Color.White;
                anotacion.ResizeToContent();
                Chart2.Series["Series1"].BorderWidth = 12;
                Chart2.Annotations.Add(anotacion);

            }
            else
            {

                // Mostrar los nombres de las seríes
                if (ddlPeriodo.Text.Trim() != "")
                {
                    Chart2.Series["Series1"].LegendText = "numero";
                    Chart2.Series["Series1"].IsVisibleInLegend = true;
                    //  Chart1.Series["Series2"].LegendText = "mesgrafica";
                    // Chart1.Series["Series2"].IsVisibleInLegend = true;

                }

                Chart2.Series["Series1"].YValueMembers = "numero";
                Chart2.Series["Series1"].XValueMember = "mesgrafica";
                Chart2.Series["Series1"].LabelMapAreaAttributes = "numero";
                Chart2.Series["Series1"].IsValueShownAsLabel = true;
            }



            // Mostrar la gráfica en pantalla
            Chart2.DataBind();
            List<IndicadoresAportes> LstDetalleComprobantere = new List<IndicadoresAportes>();
            LstDetalleComprobantere = IndicadoresAportesService.consultarRetiro(fechafinal, (Usuario)Session["Usuario"]);
             gvDatosRetiros.DataSource = LstDetalleComprobantere;
             gvDatosRetiros.DataBind();

        }
        else
        {

            // Haciendo visible las gráficas
            Chart2.Visible = true;

            // Traer los datos según criterio seleccionado
            List<IndicadoresAportes> LstDetalleComprobanteAf = new List<IndicadoresAportes>();
            string fechafinal = ddlFechaCorte.SelectedValue;

            //stDetalleComprobanteAf = IndicadoresAportesService.consultarAfiliacion(fechafinal, (Usuario)Session["Usuario"]);
            //  LstDetalleComprobante = IndicadoresAportesService.consultarAfiliaciones(filtro, pOrden, ddlVencimmiento.SelectedValue, fecha1, (Usuario)Session["Usuario"]);
            // Si se seleccionó un segundo periodo entonces cargar los datos
            if (ddlPeriodo.Text.Trim() != "")
            {
                if (ddlPeriodo.SelectedValue != "")
                {
                    LstDetalleComprobanteAf = IndicadoresAportesService.consultarRetiro(fechafinal, (Usuario)Session["Usuario"]);
                }
                foreach (IndicadoresAportes rFila_c in LstDetalleComprobanteAf)
                {
                    Boolean bEncontro = false;
                    foreach (IndicadoresAportes rFila in LstDetalleComprobanteAf)
                    {

                        if (rFila_c.mesgrafica.ToUpper() == rFila.mesgrafica.ToUpper())
                        {
                        rFila.mesgrafica = rFila_c.mesgrafica;
                        rFila.numero = rFila_c.numero;
                        bEncontro = true;
                        break;
                        }
                    }
                    if (bEncontro == false)
                    {
                        IndicadoresAportes rFilaNueva = new IndicadoresAportes();
                        rFilaNueva.mesgrafica = "";
                        rFilaNueva.numero = 0;
                        LstDetalleComprobanteAf.Add(rFilaNueva);
                    }
                }
            }

            // --------------------------------------------------------------------------------------------------------------------------------------------
            // Mostrar la primera gráfica
            // -------------------------------------------------------------------------------------------------------------------------------------------
            Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
            // Determinar el tipo de gráfica
            Chart2.Series["Series1"].ChartType = TipoGrafica(ddlTipoGrafica);
            // Chart1.Series["Series2"].ChartType = TipoGrafica(ddlTipoGrafica);
            Chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            // Cargar los datos a la gráfica
            Chart2.DataSource = LstDetalleComprobanteAf;
            // Determinar si se genera por totales o por número
            Chart2.Titles["Title1"].Text = "RETIROS "; ;

            if (ddlTipoGrafica.Text == "2")
            {
                Chart2.Series["Series1"].YValueMembers = "numero";
                Chart2.Series["Series1"].XValueMember = "mesgrafica";
                Chart2.Series["Series1"].LabelMapAreaAttributes = "numero";
                Chart2.Series["Series1"].IsValueShownAsLabel = false;
            }
            else
            {

                // Mostrar los nombres de las seríes
                if (ddlPeriodo.Text.Trim() != "")
                {
                    Chart2.Series["Series1"].LegendText = "mesgrafica";
                    Chart2.Series["Series1"].IsVisibleInLegend = true;
                    Chart2.Series["Series2"].LegendText = "numero";
                    Chart2.Series["Series2"].IsVisibleInLegend = true;

                }

                Chart2.Series["Series1"].YValueMembers = "numero";
                Chart2.Series["Series1"].XValueMember = "mesgrafica";
                Chart2.Series["Series1"].LabelMapAreaAttributes = "numero";
                Chart2.Series["Series1"].IsValueShownAsLabel = false;
            }



            // Mostrar la gráfica en pantalla
            Chart2.DataBind();
            List<IndicadoresAportes> LstDetalleComprobantere = new List<IndicadoresAportes>();
            LstDetalleComprobantere = IndicadoresAportesService.consultarRetiro(fechafinal, (Usuario)Session["Usuario"]);
            gvDatosRetiros.DataSource = LstDetalleComprobantere;
            gvDatosRetiros.DataBind();
        }
    }


    //protected void btnInforme_Click(object sender, EventArgs e)
    //{
    //    Configuracion conf = new Configuracion();

    //  /*  if (ddlPeriodo.SelectedValue == "1")
    //        fechaInicial = Convert.ToDateTime(ddlFechaCorte.SelectedValue).AddDays(-365).ToString(conf.ObtenerFormatoFecha());
    //    if (ddlPeriodo.SelectedValue == "2")
    //        fechaInicial = Convert.ToDateTime(ddlFechaCorte.SelectedValue).AddDays(-180).ToString(conf.ObtenerFormatoFecha());
    //    if (ddlPeriodo.SelectedValue == "3")
    //        fechaInicial = Convert.ToDateTime(ddlFechaCorte.SelectedValue).AddDays(-90).ToString(conf.ObtenerFormatoFecha());
    //    */
    //    // Haciendo visible las gráficas
    //    Chart1.Visible = true;
    //    Chart2.Visible = true;

    //    // Traer los datos según criterio seleccionado
    //    List<IndicadoresAportes> LstDetalleComprobante = new List<IndicadoresAportes>();


    //    string fechafinal = ddlFechaCorte.SelectedValue;
    //    LstDetalleComprobante = IndicadoresAportesService.consultarAfiliaciones(fechafinal,(Usuario)Session["Usuario"]);

    //    // --------------------------------------------------------------------------------------------------------------------------------------------
    //    // Mostrar la primera gráfica
    //    // -------------------------------------------------------------------------------------------------------------------------------------------
    //    Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
    //    Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
    //    // Determinar los títulos de la gráfica
    //    Chart1.Titles["Title1"].Text = "Evolución Afiliaciones a  " + ddlFechaCorte.SelectedValue;
    //    Chart1.Titles["Title2"].Text = "($ millones)";
    //    // Cargar los datos a la gráfica
    //    Chart1.DataSource = LstDetalleComprobante;
    //    // Determinar el tipo de gráfica
    //    if (ddlPeriodo.SelectedValue == "3")
    //    {
    //        Chart1.Series["Series1"].ChartType = SeriesChartType.Column;
    //        Chart1.Series["Series2"].ChartType = SeriesChartType.Column;
    //    }
    //    else
    //    {
    //        Chart1.Series["Series1"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
    //        Chart1.Series["Series2"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
    //    }
    //    Chart2.Series["Series1"].LegendText = Convert.ToString(LstDetalleComprobante[0].numero);
    //    Chart2.Series["Series1"].YValueMembers = "numero";
    //    Chart2.Series["Series1"].XValueMember = "fecha_historico";
    //    Chart2.Series["Series1"].LabelMapAreaAttributes = "numero";
    //    Chart2.Series["Series1"].IsValueShownAsLabel = true;
    //    Chart2.Series["Series1"]["LabelStyle"] = "Top";


    //    Chart1.Series["Series2"].LegendText = Convert.ToString(LstDetalleComprobante[1].mes);
    //    Chart1.Series["Series2"].YValueMembers = "mes";
    //    Chart1.Series["Series2"].XValueMember = "fecha_historico";
    //    Chart1.Series["Series2"].LabelMapAreaAttributes = "mes";
    //    Chart1.Series["Series2"].IsValueShownAsLabel = true;
    //    Chart1.Series["Series2"]["LabelStyle"] = "Top";

    //    // --------------------------------------------------------------------------------------------------------------------------------------------
    //    // Mostrar la segunda gráfica    
    //    // -------------------------------------------------------------------------------------------------------------------------------------------

    //    // Refrescar datos de la gráfica en pantalla
    //    Chart1.DataBind();

    //}



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
}
