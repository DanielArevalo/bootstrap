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
    private Xpinn.Indicadores.Services.CarteraOficinasService CarteraOficinasService = new Xpinn.Indicadores.Services.CarteraOficinasService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[CarteraOficinasService.CodigoPrograma + ".id"] != null )
                VisualizarOpciones(CarteraOficinasService.CodigoPrograma, "E");
            else
                VisualizarOpciones(CarteraOficinasService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            txtColorFondo.eventoCambiar += txtColorFondo_TextChanged;
 
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CarteraOficinasService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session[CarteraOficinasService.CodigoPrograma + ".id"] != null )
            {
                Usuario usuap = (Usuario)Session["usuario"];
            }
            List<CarteraOficinas> lstFechas = new List<CarteraOficinas>();
            lstFechas = CarteraOficinasService.consultarfecha((Usuario)Session["Usuario"]);
            ddlVencimmiento.DataSource = lstFechas;
            ddlVencimmiento.DataValueField = "fecha_corte";
            ddlVencimmiento.DataTextField = "fecha_corte";
            ddlVencimmiento.DataBind();
            ddlPeriodo.DataSource = lstFechas;
            ddlPeriodo.DataValueField = "fecha_corte";
            ddlPeriodo.DataTextField = "fecha_corte";
            ddlPeriodo.DataBind();
         }
        btnInforme_Click(null, null);
    }


    protected void btnInforme_Click(object sender, EventArgs e)
    {
        // Haciendo visible las gráficas
        Chart1.Visible = true;
        Chart2.Visible = true;

        // Traer los datos según criterio seleccionado
        List<CarteraOficinas> LstDetalleComprobante = new List<CarteraOficinas>();
        if (ddlGeneracion.Text == "1" || ddlGeneracion.Text == "2" || ddlGeneracion.Text == "3" || ddlGeneracion.Text == "4" || ddlGeneracion.Text == "5")
            LstDetalleComprobante = CarteraOficinasService.consultarCarteraOficinas(ddlVencimmiento.SelectedValue, Convert.ToInt32(ddlGeneracion.Text), (Usuario)Session["Usuario"]);

        // Si se seleccionó un segundo periodo entonces cargar los datos
        if (ddlPeriodo.Text.Trim() != "")
        {
            List<CarteraOficinas> LstDetalleComprobante_c = new List<CarteraOficinas>();
            if (ddlGeneracion.Text == "1" || ddlGeneracion.Text == "2" || ddlGeneracion.Text == "3" || ddlGeneracion.Text == "4" || ddlGeneracion.Text == "5")
                LstDetalleComprobante_c = CarteraOficinasService.consultarCarteraOficinas(ddlPeriodo.SelectedValue, Convert.ToInt32(ddlGeneracion.Text), (Usuario)Session["Usuario"]);
            foreach (CarteraOficinas rFila_c in LstDetalleComprobante_c)
            {
                Boolean bEncontro = false;
                foreach (CarteraOficinas rFila in LstDetalleComprobante)
                {
                    if (rFila_c.nombre == rFila.nombre)
                    {
                        rFila.total_cartera_c = rFila_c.total_cartera;
                        rFila.numero_cartera_c = rFila_c.numero_cartera;
                        rFila.participacion_cartera_c = rFila_c.participacion_cartera;
                        rFila.participacion_numero_c = rFila_c.participacion_numero;
                        bEncontro = true;
                        break;
                    }
                }
                if (bEncontro == false)
                {
                    CarteraOficinas rFilaNueva = new CarteraOficinas();
                    rFilaNueva.cod_oficina = rFila_c.cod_oficina;
                    rFilaNueva.nombre = rFila_c.nombre;
                    rFilaNueva.total_cartera = 0m;
                    rFilaNueva.numero_cartera = 0m;
                    rFilaNueva.participacion_cartera = 0m;
                    rFilaNueva.participacion_numero = 0m;
                    rFilaNueva.total_cartera_c = rFila_c.total_cartera_c;
                    rFilaNueva.numero_cartera_c = rFila_c.numero_cartera_c;
                    rFilaNueva.participacion_cartera_c = rFila_c.participacion_cartera_c;
                    rFilaNueva.participacion_numero_c = rFila_c.participacion_numero_c;
                    LstDetalleComprobante.Add(rFilaNueva);
                }
            }
        }

        // --------------------------------------------------------------------------------------------------------------------------------------------
        // Mostrar la primera gráfica
        // -------------------------------------------------------------------------------------------------------------------------------------------
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        // Determinar el tipo de gráfica
        Chart1.Series["Series1"].ChartType = TipoGrafica(ddlTipoGrafica1);
        Chart1.Series["Series2"].ChartType = TipoGrafica(ddlTipoGrafica1);
        // Cargar los datos a la gráfica
        Chart1.DataSource = LstDetalleComprobante;
        // Determinar si se genera por totales o por número
        Chart1.Titles["Title1"].Text = "Saldo y Participación Cartera Bruta " + ddlGeneracion.SelectedItem;
        Chart1.Titles["Title2"].Text = "(millones y %)";
        if (ddlTipoGrafica1.Text == "2")
        {
            Chart1.Series["Series1"].LabelMapAreaAttributes = "participacion_cartera";
            Chart1.Series["Series1"].IsValueShownAsLabel = true;
            Chart1.Series["Series1"].YValueMembers = "participacion_cartera";
            Chart1.Series["Series1"].XValueMember = "nombre";
        }
        else
        {
            // Mostrar los nombres de las seríes
            Chart1.Series["Series1"].LegendText = ddlVencimmiento.Text;
            if (ddlPeriodo.Text.Trim() != "")
            {
                Chart1.Series["Series2"].LegendText = ddlPeriodo.Text;
                Chart1.Series["Series2"].IsVisibleInLegend = true;
            }
            else
            {
                Chart1.Series["Series2"].LegendText = null;
                Chart1.Series["Series2"].IsVisibleInLegend = false;
            }
            Chart1.Series["Series1"].YValueMembers = "total_cartera";
            Chart1.Series["Series1"].XValueMember = "nombre";
            Chart1.Series["Series1"].LabelMapAreaAttributes = "total_cartera";
            Chart1.Series["Series1"].IsValueShownAsLabel = true;
            Chart1.Series["Series1"].LabelFormat = "{0:N0}";
        }
        if (ddlPeriodo.Text.Trim() != "")
        {
            Chart1.Series["Series2"].YValueMembers = "total_cartera_c";
            Chart1.Series["Series2"].XValueMember = "nombre";
            Chart1.Series["Series2"].LabelMapAreaAttributes = "total_cartera_c";
            Chart1.Series["Series2"].IsValueShownAsLabel = true;
            Chart1.Series["Series2"].LabelFormat = "{0:N0}";
        }
        // Mostrar los porcentajes de participación
        Chart1.Annotations.Clear();
        if (ddlTipoGrafica1.Text == "1")
            for (int i = 0; i < LstDetalleComprobante.Count(); i++)
            {
                MostrarAnotacion(Chart1, i, Math.Round(Convert.ToDouble(LstDetalleComprobante[i].total_cartera) / 2), Convert.ToString(Math.Round(Convert.ToDouble(LstDetalleComprobante[i].participacion_cartera), 1)) + "%");
            }
        // Mostrar la gráfica en pantalla
        Chart1.DataBind();

        // --------------------------------------------------------------------------------------------------------------------------------------------
        // Mostrar la segunda gráfica    
        // -------------------------------------------------------------------------------------------------------------------------------------------
        Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        Chart2.Series["Series1"].ChartType = TipoGrafica(ddlTipoGrafica2);
        Chart2.Series["Series2"].ChartType = TipoGrafica(ddlTipoGrafica2);
        Chart2.Titles["Title1"].Text = "Número y ParticipaciónClientes Por " + ddlGeneracion.SelectedItem;
       // Chart2.Titles["Title2"].Text = "(# Número y %)";
        Chart2.DataSource = LstDetalleComprobante;        
        if (ddlTipoGrafica2.Text == "2")
        {
            Chart2.Series["Series1"].LabelMapAreaAttributes = "participacion_numero";
            Chart2.Series["Series1"].IsValueShownAsLabel = true;
            Chart2.Series["Series1"].YValueMembers = "participacion_numero";
            Chart2.Series["Series1"].XValueMember = "nombre";
        }
        else
        {
            Chart2.Series["Series1"].LegendText = ddlVencimmiento.Text;
            Chart2.Series["Series1"].LabelMapAreaAttributes = "numero_cartera";
            Chart2.Series["Series1"].IsValueShownAsLabel = true;
            Chart2.Series["Series1"].YValueMembers = "numero_cartera";
            Chart2.Series["Series1"].XValueMember = "nombre";        
        }
        if (ddlPeriodo.Text.Trim() != "")
        {
            Chart2.Series["Series2"].YValueMembers = "numero_cartera_c";
            Chart2.Series["Series2"].XValueMember = "nombre";
            Chart2.Series["Series2"].LabelMapAreaAttributes = "numero_cartera_c";
            Chart2.Series["Series2"].IsValueShownAsLabel = true;
        }
        else
        {
            Chart2.Series["Series2"].LegendText = null;
            Chart2.Series["Series2"].IsVisibleInLegend = false;
        }
        // Mostrar los porcentajes de participación
        Chart2.Annotations.Clear();
        if (ddlTipoGrafica2.Text == "1")
            for (int i = 0; i < LstDetalleComprobante.Count(); i++)
            {
                MostrarAnotacion(Chart2, i, Math.Round(Convert.ToDouble(LstDetalleComprobante[i].numero_cartera) / 2), Convert.ToString(Math.Round(Convert.ToDouble(LstDetalleComprobante[i].participacion_numero), 1)) + "%");
            }
        // Refrescar datos de la gráfica en pantalla
        Chart2.DataBind();

        // Mostrar la tabla de datos
        gvDatos.Columns.Clear();
        DataTable dtDatos = new DataTable();
        BoundField ColumnBoundPER = new BoundField();
        ColumnBoundPER.HeaderText = "Periodo";
        ColumnBoundPER.DataField = "Periodo";
        ColumnBoundPER.DataFormatString = "{0:D}";
        ColumnBoundPER.ItemStyle.Width = 100;
        ColumnBoundPER.ControlStyle.Width = 100;
        ColumnBoundPER.HeaderStyle.Width = 100;
        gvDatos.Columns.Add(ColumnBoundPER);
        dtDatos.Columns.Add("Periodo", typeof(string));
        dtDatos.Columns["Periodo"].AllowDBNull = true;
        dtDatos.Columns["Periodo"].DefaultValue = "0";
        foreach (CarteraOficinas rFila in LstDetalleComprobante)
        {
            if (rFila.nombre != null)
            {
                BoundField ColumnBoundKAP = new BoundField();
                ColumnBoundKAP.HeaderText = rFila.nombre;
                ColumnBoundKAP.DataField = rFila.nombre;
                ColumnBoundKAP.DataFormatString = "{0:N}";
                ColumnBoundKAP.ItemStyle.Width = 100;
                ColumnBoundKAP.ControlStyle.Width = 100;
                ColumnBoundKAP.HeaderStyle.Width = 100;
                gvDatos.Columns.Add(ColumnBoundKAP);
                dtDatos.Columns.Add(rFila.nombre, typeof(string));
                dtDatos.Columns[rFila.nombre].AllowDBNull = true;
                dtDatos.Columns[rFila.nombre].DefaultValue = "0";
            }
        }
        BoundField ColumnBoundTOT = new BoundField();
        ColumnBoundTOT.HeaderText = "Total";
        ColumnBoundTOT.DataField = "Total";
        ColumnBoundTOT.DataFormatString = "{0:N}";
        ColumnBoundTOT.ItemStyle.Width = 100;
        ColumnBoundTOT.ControlStyle.Width = 100;
        ColumnBoundTOT.HeaderStyle.Width = 100;
        gvDatos.Columns.Add(ColumnBoundTOT);
        dtDatos.Columns.Add("Total", typeof(string));
        dtDatos.Columns["Total"].AllowDBNull = true;
        dtDatos.Columns["Total"].DefaultValue = "0";

        decimal total = 0;
        DataRow drDatos;
        drDatos = dtDatos.NewRow();
        drDatos[0] = ddlVencimmiento.Text;
        int posicion = 1;
        foreach (CarteraOficinas rFila in LstDetalleComprobante)
        {
            if (rFila.nombre != null)
            {                
                drDatos[posicion] = Convert.ToString(rFila.total_cartera);
                total += rFila.total_cartera;
                posicion += 1;                
            }
        }
        drDatos["Total"] = total;
        dtDatos.Rows.Add(drDatos);
        dtDatos.AcceptChanges();
        if (ddlPeriodo.Text.Trim() != "")
        {
            DataRow drDatos_c;
            total = 0;
            drDatos_c = dtDatos.NewRow();
            drDatos_c[0] = ddlPeriodo.Text;
            int posicion_c = 1;
            foreach (CarteraOficinas rFila in LstDetalleComprobante)
            {
                if (rFila.nombre != null)
                {                                        
                    drDatos_c[posicion_c] = Convert.ToString(rFila.total_cartera_c);
                    total += rFila.total_cartera_c;
                    posicion_c += 1;
                }
            }
            drDatos["Total"] = total;
            dtDatos.Rows.Add(drDatos_c);
            dtDatos.AcceptChanges();
        }

        gvDatos.DataSource = dtDatos;
        gvDatos.DataBind();
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

    private void Generar()
    {
        btnInforme_Click(null, null);
    }

    protected void ddlVencimmiento_SelectedIndexChanged(object sender, EventArgs e)
    {
        Generar();
    }

    protected void ddlGeneracion_SelectedIndexChanged(object sender, EventArgs e)
    {
        Generar();
    }

    protected void ddlPeriodo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Generar();
    }

    protected void ddlTipoGrafica1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Generar();
    }

    protected void ddlTipoGrafica2_SelectedIndexChanged(object sender, EventArgs e)
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
