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
    private Xpinn.Indicadores.Services.DistribucionCarteraOficinasService DistribucionCarteraOficinasService = new Xpinn.Indicadores.Services.DistribucionCarteraOficinasService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[DistribucionCarteraOficinasService.CodigoPrograma + ".id"] != null )
                VisualizarOpciones(DistribucionCarteraOficinasService.CodigoPrograma, "E");
            else
                VisualizarOpciones(DistribucionCarteraOficinasService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            txtColorFondo.eventoCambiar += txtColorFondo_TextChanged;
    
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DistribucionCarteraOficinasService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session[DistribucionCarteraOficinasService.CodigoPrograma + ".id"] != null )
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
         }
        btnInforme_Click(null, null);
    }


    protected void btnInforme_Click(object sender, EventArgs e)
    {
        // Haciendo visible las gráficas
        Chart1.Visible = true;
        Chart2.Visible = true;

        // Traer los datos según criterio seleccionado
        List<DistribucionCarteraOficinas> LstDetalleComprobante = new List<DistribucionCarteraOficinas>();
        LstDetalleComprobante = DistribucionCarteraOficinasService.DistribucionCarteraOficinas(ddlVencimmiento.SelectedValue, ddlVencimmiento.SelectedValue, (Usuario)Session["Usuario"]);

        // --------------------------------------------------------------------------------------------------------------------------------------------
        // Mostrar la primera gráfica
        // -------------------------------------------------------------------------------------------------------------------------------------------
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        // Cargar los datos a la gráfica        
        Chart1.DataSource = LstDetalleComprobante;
        // Determinar si se genera por totales o por número
        Chart1.Titles["Title1"].Text = "Distribución de Cartera por Edad de Mora a " + ddlVencimmiento.SelectedValue;
        Chart1.Titles["Title2"].Text = "($ millones)";
        // Mostrar los nombres de las seríes
        Chart1.Series["Series1"].LegendText = "Entre 1 y 30";
        Chart1.Series["Series2"].LegendText = "Entre 31 y 60";
        Chart1.Series["Series3"].LegendText = "Entre 61 y 90";
        Chart1.Series["Series4"].LegendText = "Entre 91 y 120";
        Chart1.Series["Series5"].LegendText = "Entre 121 y 150";
        Chart1.Series["Series6"].LegendText = "Entre 151 y 180";
        Chart1.Series["Series7"].LegendText = "Mayor a 180";

        Chart1.Series["Series1"].YValueMembers = "valor30";
        Chart1.Series["Series1"].XValueMember = "nombre";
        Chart1.Series["Series1"].LabelMapAreaAttributes = "valor30";
        Chart1.Series["Series1"].IsValueShownAsLabel = true;

        Chart1.Series["Series2"].YValueMembers = "valor60";
        Chart1.Series["Series2"].XValueMember = "nombre";
        Chart1.Series["Series2"].LabelMapAreaAttributes = "valor60";
        Chart1.Series["Series2"].IsValueShownAsLabel = true;

        Chart1.Series["Series3"].YValueMembers = "valor90";
        Chart1.Series["Series3"].XValueMember = "nombre";
        Chart1.Series["Series3"].LabelMapAreaAttributes = "valor90";
        Chart1.Series["Series3"].IsValueShownAsLabel = true;

        Chart1.Series["Series4"].YValueMembers = "valor120";
        Chart1.Series["Series4"].XValueMember = "nombre";
        Chart1.Series["Series4"].LabelMapAreaAttributes = "valor120";
        Chart1.Series["Series4"].IsValueShownAsLabel = true;

        Chart1.Series["Series5"].YValueMembers = "valor150";
        Chart1.Series["Series5"].XValueMember = "nombre";
        Chart1.Series["Series5"].LabelMapAreaAttributes = "valor150";
        Chart1.Series["Series5"].IsValueShownAsLabel = true;

        Chart1.Series["Series6"].YValueMembers = "valor180";
        Chart1.Series["Series6"].XValueMember = "nombre";
        Chart1.Series["Series6"].LabelMapAreaAttributes = "valor180";
        Chart1.Series["Series6"].IsValueShownAsLabel = true;

        Chart1.Series["Series7"].YValueMembers = "valorult";
        Chart1.Series["Series7"].XValueMember = "nombre";
        Chart1.Series["Series7"].LabelMapAreaAttributes = "valorult";
        Chart1.Series["Series7"].IsValueShownAsLabel = true;
        // Mostrar la gráfica en pantalla
        Chart1.DataBind();

        // --------------------------------------------------------------------------------------------------------------------------------------------
        // Mostrar la segunda gráfica    
        // -------------------------------------------------------------------------------------------------------------------------------------------                        
        Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        Chart2.DataSource = LstDetalleComprobante;
        Chart2.Titles["Title1"].Text = "Distribución (%) de Cartera por Edad de Mora a " + ddlVencimmiento.SelectedItem;
        // Mostrar los nombres de las seríes
        Chart2.Series["Series1"].LegendText = "Entre 1 y 30";
        Chart2.Series["Series2"].LegendText = "Entre 31 y 60";
        Chart2.Series["Series3"].LegendText = "Entre 61 y 90";
        Chart2.Series["Series4"].LegendText = "Entre 91 y 120";
        Chart2.Series["Series5"].LegendText = "Entre 121 y 150";
        Chart2.Series["Series6"].LegendText = "Entre 151 y 180";
        Chart2.Series["Series7"].LegendText = "Mayor a 180";

        Chart2.Series["Series1"].YValueMembers = "porcentaje30";
        Chart2.Series["Series1"].XValueMember = "nombre";
        Chart2.Series["Series1"].LabelMapAreaAttributes = "porcentaje30";
        Chart2.Series["Series1"].IsValueShownAsLabel = true;

        Chart2.Series["Series2"].YValueMembers = "porcentaje60";
        Chart2.Series["Series2"].XValueMember = "nombre";
        Chart2.Series["Series2"].LabelMapAreaAttributes = "porcentaje60";
        Chart2.Series["Series2"].IsValueShownAsLabel = true;

        Chart2.Series["Series3"].YValueMembers = "porcentaje90";
        Chart2.Series["Series3"].XValueMember = "nombre";
        Chart2.Series["Series3"].LabelMapAreaAttributes = "porcentaje90";
        Chart2.Series["Series3"].IsValueShownAsLabel = true;

        Chart2.Series["Series4"].YValueMembers = "porcentaje120";
        Chart2.Series["Series4"].XValueMember = "nombre";
        Chart2.Series["Series4"].LabelMapAreaAttributes = "porcentaje120";
        Chart2.Series["Series4"].IsValueShownAsLabel = true;

        Chart2.Series["Series5"].YValueMembers = "porcentaje150";
        Chart2.Series["Series5"].XValueMember = "nombre";
        Chart2.Series["Series5"].LabelMapAreaAttributes = "porcentaje150";
        Chart2.Series["Series5"].IsValueShownAsLabel = true;

        Chart2.Series["Series6"].YValueMembers = "porcentaje180";
        Chart2.Series["Series6"].XValueMember = "nombre";
        Chart2.Series["Series6"].LabelMapAreaAttributes = "porcentaje180";
        Chart2.Series["Series6"].IsValueShownAsLabel = true;

        Chart2.Series["Series7"].YValueMembers = "porcentajeult";
        Chart2.Series["Series7"].XValueMember = "nombre";
        Chart2.Series["Series7"].LabelMapAreaAttributes = "porcentajeult";
        Chart2.Series["Series7"].IsValueShownAsLabel = true;    

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
