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
    private Xpinn.Indicadores.Services.GestionRiesgoService GestionRiesgoService = new Xpinn.Indicadores.Services.GestionRiesgoService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[GestionRiesgoService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(GestionRiesgoService.CodigoPrograma, "E");
            else
                VisualizarOpciones(GestionRiesgoService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            txtColorFondo.eventoCambiar += txtColorFondo_TextChanged;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GestionRiesgoService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarDropDown();
            if (Session[GestionRiesgoService.CodigoPrograma + ".id"] != null)
            {
                Usuario usuap = (Usuario)Session["usuario"];
            }
            // Cargar listado de fechas
            List<CarteraOficinas> lstFechas = new List<CarteraOficinas>();
            CarteraOficinasService carteraOficinasServicio = new CarteraOficinasService();
            lstFechas = carteraOficinasServicio.consultarfecha("Z", (Usuario)Session["Usuario"]) ;
            ddlFechaCorte.DataSource = lstFechas;
            ddlFechaCorte.DataValueField = "fecha_corte";
            ddlFechaCorte.DataTextField = "fecha_corte";
            ddlFechaCorte.DataBind();
            // Generar la gràfica
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
            filtrare += " and hc.cod_linea_Credito In (";
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

        string Filtro = obtfiltro();
        
        List<GestionRiesgo> lstCreditos = new List<GestionRiesgo>();
        lstCreditos = GestionRiesgoService.ListadoSegmentoCredito(ddlFechaCorte.SelectedValue,Filtro, (Usuario)Session["Usuario"]);

        // --------------------------------------------------------------------------------------------------------------------------------------------
        // Mostrar la primera gráfica
        // -------------------------------------------------------------------------------------------------------------------------------------------
        var listadoSaldo = (from p in lstCreditos group p by p.segmento into grupo select new { segmento = grupo.Key, saldo = grupo.Sum(x => x.saldo/1000) }).ToList();
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        // Determinar los títulos de la gráfica
        Chart1.Titles["Title1"].Text = "Evaluacion de Cartera por Segmentos de Riesgo " + ddlFechaCorte.SelectedValue;
        Chart1.Titles["Title2"].Text = "($ millones)";
        // Cargar los datos a la gráfica
        Chart1.DataSource = listadoSaldo;
        // Determinar el tipo de gráfica
        Chart1.Series["Series1"].ChartType = TipoGrafica(ddlTipoGrafica1);
        // Mostrar los nombres de las seríes
        if (ddlTipoGrafica1.SelectedValue != "1")
            Chart1.Series["Series1"].LegendText = "Total";
        Chart1.Series["Series1"].YValueMembers = "saldo";
        Chart1.Series["Series1"].XValueMember = "segmento";
        Chart1.Series["Series1"].LabelMapAreaAttributes = "saldo";
        Chart1.Series["Series1"].IsValueShownAsLabel = true;
        Chart1.Series["Series1"]["LabelStyle"] = "Top";
        Chart1.Series["Series1"].LabelFormat = "{0:N0}";
        Chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;
        // Mostrar la gráfica en pantalla
        Chart1.DataBind();

        // Traer los dato según criterio seleccionado
        var listadoSaldoDatos = (from p in lstCreditos where p.segmento != null group p by new {p.fecha_historico, p.segmento} into grupo select new {grupo.Key.fecha_historico, grupo.Key.segmento, numero = grupo.Sum(x => x.numero), saldo = grupo.Sum(x => x.saldo) }).ToList();
        gvDatos.DataSource = listadoSaldoDatos;
        gvDatos.DataBind();

        // --------------------------------------------------------------------------------------------------------------------------------------------
        // Mostrar la segunda gráfica    
        // -------------------------------------------------------------------------------------------------------------------------------------------
        var listadoNumero = (from p in lstCreditos group p by p.segmento into grupo select new { segmento = grupo.Key, numero = grupo.Sum(x => x.numero) }).ToList();
        Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        Chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        // Determinar los títulos de la gráfica
        Chart2.Titles["Title1"].Text = "Evaluacion de Cartera por Segmentos de Riesgo " + ddlFechaCorte.SelectedValue;
        Chart2.Titles["Title2"].Text = "# Número";
        // Cargar los datos a la gráfica
        Chart2.DataSource = listadoNumero;
        // Determinar el tipo de gráfica
        Chart2.Series["Series1"].ChartType = TipoGrafica(ddlTipoGrafica2);
        // Mostrar los nombres de las seríes        
        if (ddlTipoGrafica2.SelectedValue != "1")
            Chart2.Series["Series1"].LegendText = "Total";
        Chart2.Series["Series1"].LabelMapAreaAttributes = "numero";
        Chart2.Series["Series1"].IsValueShownAsLabel = true;
        Chart2.Series["Series1"].YValueMembers = "numero";
        Chart2.Series["Series1"].XValueMember = "segmento";
        // Refrescar datos de la gráfica en pantalla
        Chart2.DataBind();

        // Traer los dato según criterio seleccionado
        var listadoNumeroDatos = (from p in lstCreditos where p.segmento != null group p by new { p.fecha_historico, p.segmento, p.cod_categoria } into grupo select new { grupo.Key.fecha_historico, grupo.Key.segmento, grupo.Key.cod_categoria, numero = grupo.Sum(x => x.numero), saldo = grupo.Sum(x => x.saldo) }).ToList();
        gvCantidad.DataSource = listadoNumeroDatos;
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
            return SeriesChartType.Pie;
        if (ddlTipGra.SelectedIndex == 1)
            return SeriesChartType.Column;
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
