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
    private Xpinn.Indicadores.Services.IndicadorCarteraOficinasService IndicadorCarteraOficinasService = new Xpinn.Indicadores.Services.IndicadorCarteraOficinasService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[IndicadorCarteraOficinasService.CodigoPrograma + ".id"] != null )
                VisualizarOpciones(IndicadorCarteraOficinasService.CodigoPrograma, "E");
            else
                VisualizarOpciones(IndicadorCarteraOficinasService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            txtColorFondo.eventoCambiar += txtColorFondo_TextChanged;
    
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(IndicadorCarteraOficinasService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session[IndicadorCarteraOficinasService.CodigoPrograma + ".id"] != null )
            {
                Usuario usuap = (Usuario)Session["usuario"];
            }
            List<CarteraOficinas> lstFechas = new List<CarteraOficinas>();
            CarteraOficinasService carteraOficinasServicio = new CarteraOficinasService();
            lstFechas = carteraOficinasServicio.consultarfecha((Usuario)Session["Usuario"]);
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
        List<IndicadorCarteraOficinas> LstDetalleComprobante = new List<IndicadorCarteraOficinas>();
        LstDetalleComprobante = IndicadorCarteraOficinasService.consultarCarteraVencidaOficinas(ddlVencimmiento.SelectedValue, ddlVencimmiento.SelectedValue, (Usuario)Session["Usuario"]);

        // Si se seleccionó un segundo periodo entonces cargar los datos
        if (ddlPeriodo.Text.Trim() != "")
        {
            List<IndicadorCarteraOficinas> LstDetalleComprobante_c = new List<IndicadorCarteraOficinas>();
            LstDetalleComprobante_c = IndicadorCarteraOficinasService.consultarCarteraVencidaOficinas(ddlPeriodo.SelectedValue, ddlPeriodo.SelectedValue, (Usuario)Session["Usuario"]);
            foreach (IndicadorCarteraOficinas rFila_c in LstDetalleComprobante_c)
            {
                Boolean bEncontro = false;
                foreach (IndicadorCarteraOficinas rFila in LstDetalleComprobante)
                {
                    if (rFila_c.nombre == rFila.nombre)
                    {
                        rFila.valor_total_c = rFila_c.valor_total;
                        rFila.porcentaje_total_c = rFila_c.porcentaje_total;
                        bEncontro = true;
                        break;
                    }
                }
                if (bEncontro == false)
                {
                    IndicadorCarteraOficinas rFilaNueva = new IndicadorCarteraOficinas();
                    rFilaNueva.cod_oficina = rFila_c.cod_oficina;
                    rFilaNueva.nombre = rFila_c.nombre;
                    rFilaNueva.valor_total = 0m;
                    rFilaNueva.porcentaje_total = 0m;
                    rFilaNueva.valor_total_c = rFila_c.valor_total_c;
                    rFilaNueva.porcentaje_total_c = rFila_c.porcentaje_total_c;
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
        Chart1.Titles["Title1"].Text = "ICV Total a " + ddlVencimmiento.SelectedValue;
        Chart1.Titles["Title2"].Text = "";
        if (ddlPeriodo.Text.Trim() != "")
            Chart2.Titles["Title1"].Text = "Dinámica ICV Total a " + ddlVencimmiento.SelectedItem;
        if (ddlTipoGrafica1.Text == "2")
        {
            Chart1.Series["Series1"].LabelMapAreaAttributes = "porcentaje_total";
            Chart1.Series["Series1"].IsValueShownAsLabel = true;
            Chart1.Series["Series1"].YValueMembers = "porcentaje_total";
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
            Chart1.Series["Series1"].YValueMembers = "valor_total";
            Chart1.Series["Series1"].XValueMember = "nombre";
            Chart1.Series["Series1"].LabelMapAreaAttributes = "valor_total";
            Chart1.Series["Series1"].IsValueShownAsLabel = true;
            Chart1.Series["Series1"].LabelFormat = "{0:N0}";
        }
        if (ddlPeriodo.Text.Trim() != "")
        {
            Chart1.Series["Series2"].YValueMembers = "valor_total_c";
            Chart1.Series["Series2"].XValueMember = "nombre";
            Chart1.Series["Series2"].LabelMapAreaAttributes = "valor_total_c";
            Chart1.Series["Series2"].IsValueShownAsLabel = true;
            Chart1.Series["Series2"].LabelFormat = "{0:N0}";
        }
        // Mostrar los porcentajes de participación
        Chart1.Annotations.Clear();
        if (ddlTipoGrafica1.Text == "1")
            for (int i = 0; i < LstDetalleComprobante.Count(); i++)
            {
                MostrarAnotacion(Chart1, i, Math.Round(Convert.ToDouble(LstDetalleComprobante[i].valor_total) / 2), Convert.ToString(Math.Round(Convert.ToDouble(LstDetalleComprobante[i].porcentaje_total), 1)) + "%");
            }
        // Mostrar la gráfica en pantalla
        Chart1.DataBind();

        // --------------------------------------------------------------------------------------------------------------------------------------------
        // Mostrar la segunda gráfica    
        // -------------------------------------------------------------------------------------------------------------------------------------------
        LstDetalleComprobante = IndicadorCarteraOficinasService.consultarCarteraVencida30Oficinas(ddlVencimmiento.SelectedValue, ddlVencimmiento.SelectedValue, (Usuario)Session["Usuario"]);
        Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        Chart2.Series["Series1"].ChartType = TipoGrafica(ddlTipoGrafica2);
        Chart2.Series["Series2"].ChartType = TipoGrafica(ddlTipoGrafica2);
        Chart2.Titles["Title1"].Text = "ICV > 30 días a " + ddlVencimmiento.SelectedItem;
        if (ddlPeriodo.Text.Trim() != "")
            Chart2.Titles["Title1"].Text = "Dinámica ICV > 30 Días a " + ddlVencimmiento.SelectedItem;
        Chart2.DataSource = LstDetalleComprobante;        
        if (ddlTipoGrafica2.Text == "2")
        {
            Chart2.Series["Series1"].LabelMapAreaAttributes = "porcentaje_30dias";
            Chart2.Series["Series1"].IsValueShownAsLabel = true;
            Chart2.Series["Series1"].YValueMembers = "porcentaje_30dias";
            Chart2.Series["Series1"].XValueMember = "nombre";
        }
        else
        {
            Chart2.Series["Series1"].LegendText = ddlVencimmiento.Text;
            Chart2.Series["Series1"].LabelMapAreaAttributes = "valor_30dias";
            Chart2.Series["Series1"].IsValueShownAsLabel = true;
            Chart2.Series["Series1"].YValueMembers = "valor_30dias";
            Chart2.Series["Series1"].XValueMember = "nombre";        
        }
        if (ddlPeriodo.Text.Trim() != "")
        {
            Chart2.Series["Series2"].YValueMembers = "valor_30dias_c";
            Chart2.Series["Series2"].XValueMember = "nombre";
            Chart2.Series["Series2"].LabelMapAreaAttributes = "valor_30dias_c";
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
                MostrarAnotacion(Chart2, i, Math.Round(Convert.ToDouble(LstDetalleComprobante[i].valor_30dias) / 2), Convert.ToString(Math.Round(Convert.ToDouble(LstDetalleComprobante[i].porcentaje_30dias), 1)) + "%");
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
        foreach (IndicadorCarteraOficinas rFila in LstDetalleComprobante)
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

        DataRow drDatos;
        drDatos = dtDatos.NewRow();
        drDatos[0] = ddlVencimmiento.Text;
        int posicion = 1;
        foreach (IndicadorCarteraOficinas rFila in LstDetalleComprobante)
        {
            if (rFila.nombre != null)
            {
                drDatos[posicion] = Convert.ToString(rFila.valor_total);
                posicion += 1;
            }
        }
        dtDatos.Rows.Add(drDatos);
        dtDatos.AcceptChanges();
        if (ddlPeriodo.Text.Trim() != "")
        {
            DataRow drDatos_c;
            drDatos_c = dtDatos.NewRow();
            drDatos_c[0] = ddlPeriodo.Text;
            int posicion_c = 1;
            foreach (IndicadorCarteraOficinas rFila in LstDetalleComprobante)
            {
                if (rFila.nombre != null)
                {
                    drDatos_c[posicion_c] = Convert.ToString(rFila.valor_total_c);
                    posicion_c += 1;
                }
            }
            dtDatos.Rows.Add(drDatos_c);
            dtDatos.AcceptChanges();
        }

        gvDatos.DataSource = dtDatos;
        gvDatos.DataBind();
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
