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
    private Xpinn.Indicadores.Services.PrestamoPromedioOficinasService PrestamoPromedioOficinasService = new Xpinn.Indicadores.Services.PrestamoPromedioOficinasService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[PrestamoPromedioOficinasService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(PrestamoPromedioOficinasService.CodigoPrograma, "E");
            else
                VisualizarOpciones(PrestamoPromedioOficinasService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            txtColorFondo.eventoCambiar += txtColorFondo_TextChanged;
    
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PrestamoPromedioOficinasService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session[PrestamoPromedioOficinasService.CodigoPrograma + ".id"] != null)
            {
                Usuario usuap = (Usuario)Session["usuario"];
            }
            List<CarteraOficinas> lstFechas = new List<CarteraOficinas>();
            lstFechas = PrestamoPromedioOficinasService.consultarfecha((Usuario)Session["Usuario"]);
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
        List<PrestamoPromedioOficinas> LstDetalleComprobante = new List<PrestamoPromedioOficinas>();
        LstDetalleComprobante = PrestamoPromedioOficinasService.consultarPrestamoPromedio(ddlVencimmiento.SelectedValue, 0, (Usuario)Session["Usuario"]);

        // Si se seleccionó un segundo periodo entonces cargar los datos
        if (ddlPeriodo.Text.Trim() != "")
        {
            List<PrestamoPromedioOficinas> LstDetalleComprobante_c = new List<PrestamoPromedioOficinas>();
            LstDetalleComprobante_c = PrestamoPromedioOficinasService.consultarPrestamoPromedio(ddlPeriodo.SelectedValue, 0, (Usuario)Session["Usuario"]);
            foreach (PrestamoPromedioOficinas rFila_c in LstDetalleComprobante_c)
            {
                Boolean bEncontro = false;
                foreach (PrestamoPromedioOficinas rFila in LstDetalleComprobante)
                {
                    if (rFila_c.nombre == rFila.nombre)
                    {
                        rFila.valor_prestamo_promedio_c = rFila_c.valor_prestamo_promedio;
                        bEncontro = true;
                        break;
                    }
                }
                if (bEncontro == false)
                {
                    PrestamoPromedioOficinas rFilaNueva = new PrestamoPromedioOficinas();
                    rFilaNueva.cod_oficina = rFila_c.cod_oficina;
                    rFilaNueva.nombre = rFila_c.nombre;
                    rFilaNueva.valor_prestamo_promedio = 0m;
                    rFilaNueva.valor_prestamo_promedio_c = rFila_c.valor_prestamo_promedio_c;
                    LstDetalleComprobante.Add(rFilaNueva);
                }
            }
        }

        // --------------------------------------------------------------------------------------------------------------------------------------------
        // Mostrar la primera gráfica
        // -------------------------------------------------------------------------------------------------------------------------------------------
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        // Determinar el tipo de gráfica
        Chart1.Series["Series1"].ChartType = SeriesChartType.Column;
        Chart1.Series["Series2"].ChartType = SeriesChartType.Column;
        // Cargar los datos a la gráfica
        Chart1.DataSource = LstDetalleComprobante;
        // Determinar si se genera por totales o por número
        Chart1.Titles["Title1"].Text = "Total Préstamo Promedio";
        Chart1.Titles["Title2"].Text = "($ millones)";
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
        Chart1.Series["Series1"].YValueMembers = "valor_prestamo_promedio";
        Chart1.Series["Series1"].XValueMember = "nombre";
        Chart1.Series["Series1"].LabelMapAreaAttributes = "valor_prestamo_promedio";
        Chart1.Series["Series1"].IsValueShownAsLabel = true;
        if (ddlPeriodo.Text.Trim() != "")
        {
            Chart1.Series["Series2"].YValueMembers = "valor_prestamo_promedio_c";
            Chart1.Series["Series2"].XValueMember = "nombre";
            Chart1.Series["Series2"].LabelMapAreaAttributes = "valor_prestamo_promedio_c";
            Chart1.Series["Series2"].IsValueShownAsLabel = true;
        }
        // Mostrar la gráfica en pantalla
        Chart1.DataBind();

        // --------------------------------------------------------------------------------------------------------------------------------------------
        // Mostrar la segunda gráfica    
        // --------------------------------------------------------------------------------------------------------------------------------------------

        // Traer los datos según criterio seleccionado
        LstDetalleComprobante = PrestamoPromedioOficinasService.consultarPrestamoPromedio(ddlVencimmiento.SelectedValue, 3, (Usuario)Session["Usuario"]);

        // Si se seleccionó un segundo periodo entonces cargar los datos
        if (ddlPeriodo.Text.Trim() != "")
        {
            List<PrestamoPromedioOficinas> LstDetalleComprobante_c = new List<PrestamoPromedioOficinas>();
            LstDetalleComprobante_c = PrestamoPromedioOficinasService.consultarPrestamoPromedio(ddlPeriodo.SelectedValue, 3, (Usuario)Session["Usuario"]);
            foreach (PrestamoPromedioOficinas rFila_c in LstDetalleComprobante_c)
            {
                Boolean bEncontro = false;
                foreach (PrestamoPromedioOficinas rFila in LstDetalleComprobante)
                {
                    if (rFila_c.nombre == rFila.nombre)
                    {
                        rFila.valor_prestamo_promedio_c = rFila_c.valor_prestamo_promedio;
                        bEncontro = true;
                        break;
                    }
                }
                if (bEncontro == false)
                {
                    PrestamoPromedioOficinas rFilaNueva = new PrestamoPromedioOficinas();
                    rFilaNueva.cod_oficina = rFila_c.cod_oficina;
                    rFilaNueva.nombre = rFila_c.nombre;
                    rFilaNueva.valor_prestamo_promedio = 0m;
                    rFilaNueva.valor_prestamo_promedio_c = rFila_c.valor_prestamo_promedio_c;
                    LstDetalleComprobante.Add(rFilaNueva);
                }
            }
        }

        Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        Chart2.Series["Series1"].ChartType = SeriesChartType.Column;
        Chart2.Series["Series2"].ChartType = SeriesChartType.Column;
        Chart2.Titles["Title1"].Text = "Total Préstamo Promedio Microcrédito";
        Chart2.Titles["Title2"].Text = "($ millones)";
        Chart2.DataSource = LstDetalleComprobante;
        Chart2.Series["Series1"].LegendText = ddlVencimmiento.Text;
        Chart2.Series["Series1"].LabelMapAreaAttributes = "valor_prestamo_promedio";
        Chart2.Series["Series1"].IsValueShownAsLabel = true;
        Chart2.Series["Series1"].YValueMembers = "valor_prestamo_promedio";
        Chart2.Series["Series1"].XValueMember = "nombre";
        if (ddlPeriodo.Text.Trim() != "")
        {
            Chart2.Series["Series2"].YValueMembers = "valor_prestamo_promedio_c";
            Chart2.Series["Series2"].XValueMember = "nombre";
            Chart2.Series["Series2"].LabelMapAreaAttributes = "valor_prestamo_promedio_c";
            Chart2.Series["Series2"].IsValueShownAsLabel = true;
        }
        else
        {
            Chart2.Series["Series2"].LegendText = null;
            Chart2.Series["Series2"].IsVisibleInLegend = false;
        }
        // Refrescar datos de la gráfica en pantalla
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

    private void Generar()
    {
        btnInforme_Click(null, null);
    }

    protected void ddlVencimmiento_SelectedIndexChanged(object sender, EventArgs e)
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

    protected void txtColorFondo_TextChanged(object sender, EventArgs e)
    {
        //asignar color de fondo
        Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
        Chart2.BackColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
        btnInforme_Click(null, null);
    }

}
