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
    private Xpinn.Indicadores.Services.IndicadorCarteraService EvolucionDesembolsoOficinasService = new Xpinn.Indicadores.Services.IndicadorCarteraService();
    private Xpinn.Indicadores.Services.CarteraOficinasService carteraOficinaServicio = new Xpinn.Indicadores.Services.CarteraOficinasService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[EvolucionDesembolsoOficinasService.CodigoProgramaparticipacion + ".id"] != null)
                VisualizarOpciones(EvolucionDesembolsoOficinasService.CodigoProgramaparticipacion, "E");
            else
                VisualizarOpciones(EvolucionDesembolsoOficinasService.CodigoProgramaparticipacion, "A");

            Site toolBar = (Site)this.Master;
            txtColorFondo.eventoCambiar += txtColorFondo_TextChanged;
 
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EvolucionDesembolsoOficinasService.CodigoProgramaparticipacion, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session[EvolucionDesembolsoOficinasService.CodigoProgramaparticipacion + ".id"] != null)
            {
                Usuario usuap = (Usuario)Session["usuario"];
            }
            List<CarteraOficinas> lstFechas = new List<CarteraOficinas>();
            lstFechas = carteraOficinaServicio.consultarfecha((Usuario)Session["Usuario"]);
            ddlVencimmiento.DataSource = lstFechas;
            ddlVencimmiento.DataValueField = "fecha_corte";
            ddlVencimmiento.DataTextField = "fecha_corte";
            ddlVencimmiento.DataBind();
            ddlPeriodo.DataSource = lstFechas;
            ddlPeriodo.DataValueField = "fecha_corte";
            ddlPeriodo.DataTextField = "fecha_corte";
            ddlPeriodo.DataBind();
            cargardrop();
        }
        btnInforme_Click(null,null);
    }
    
    protected void cargardrop() 
    {
        Xpinn.FabricaCreditos.Services.OficinaService linahorroServicio = new Xpinn.FabricaCreditos.Services.OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina linahorroVista = new Xpinn.FabricaCreditos.Entities.Oficina();
        ddloficina.DataTextField = "nombre";
        ddloficina.DataValueField = "codigo";
        ddloficina.DataSource = linahorroServicio.ListarOficinas(linahorroVista, (Usuario)Session["usuario"]);
        ddloficina.DataBind();

        Xpinn.FabricaCreditos.Data.consultasdatacreditoData vData = new Xpinn.FabricaCreditos.Data.consultasdatacreditoData();
        List<Xpinn.FabricaCreditos.Entities.CreditoEmpresaRecaudo> lstEmpresas = new List<Xpinn.FabricaCreditos.Entities.CreditoEmpresaRecaudo>();
        lstEmpresas = vData.ListarEmpresa_Recaudo((Usuario)Session["usuario"]);
        ddlempresa.DataSource = lstEmpresas;
        ddlempresa.DataTextField = "NOM_EMPRESA";
        ddlempresa.DataValueField = "COD_EMPRESA";
        ddlempresa.DataBind();

        ddlformapago.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlformapago.Items.Insert(1, new ListItem("Caja", "1"));
        ddlformapago.Items.Insert(2, new ListItem("Nomina", "2"));
        ddlformapago.SelectedIndex = 0;
        ddlformapago.DataBind();

    }


    protected void ddlformapago_selectedindexchanged(object sender,EventArgs e)
    {

        if (ddlformapago.SelectedValue == "Caja" || ddlformapago.SelectedIndex == 1)
        {
            ddlempresa.Visible = false;
            lblpagaduria.Visible = false;
            ddlempresa.SelectedText = "";
            ddlempresa.SelectedValue = "";
            gvDatos.Visible = false;
        }
        else
        {
            ddlempresa.Visible = true;
            lblpagaduria.Visible = true;
            gvDatos.Visible = false;
        }
    }

    protected string obtfiltro() 
    {
        string filtrare = "";
        if (ddlempresa.SelectedText != "")
        {
            filtrare += " and cod_empresa in( " + ddlempresa.SelectedValue+")";
        }
        if (ddloficina.SelectedText != "")
        {
            filtrare += " and cod_oficina in( " + ddloficina.SelectedValue+")";
        }

        return filtrare;
    }

    protected void btnInforme_Click(object sender, EventArgs e)
    {
        if (chkmostrarlegendas.Checked == true)
        {
            gvDatos.Visible = true;
            string filtro = obtfiltro();
            string pOrden = "";
            // Haciendo visible las gráficas
            Chart1.Visible = true;

            // Traer los datos según criterio seleccionado
            List<IndicadorCartera> LstDetalleComprobante_c = new List<IndicadorCartera>();
            List<IndicadorCartera> LstDetalleComprobante = new List<IndicadorCartera>();
            string fecha1 = "";
            fecha1 += Convert.ToDateTime(ddlVencimmiento.SelectedValue).Day;
            fecha1 += "/";
            fecha1 += Convert.ToDateTime(ddlVencimmiento.SelectedValue).Month;
            fecha1 += "/";
            fecha1 += Convert.ToDateTime(ddlVencimmiento.SelectedValue).Year;
            pOrden = "v.fecha_historico";
            LstDetalleComprobante = EvolucionDesembolsoOficinasService.consultarparticipacionpagadurias(filtro,pOrden, ddlVencimmiento.SelectedValue, fecha1, (Usuario)Session["Usuario"]);
            // Si se seleccionó un segundo periodo entonces cargar los datos
            if (ddlPeriodo.Text.Trim() != "")
            {
                if (ddlVencimmiento.SelectedValue != "" && ddlPeriodo.SelectedValue != "")
                {                    
                    LstDetalleComprobante_c = EvolucionDesembolsoOficinasService.consultarparticipacionpagadurias(filtro,pOrden, ddlVencimmiento.SelectedValue, fecha1, (Usuario)Session["Usuario"]);
                }
                foreach (IndicadorCartera rFila_c in LstDetalleComprobante_c)
                {
                    int contando = 0;
                    Boolean bEncontro = false;
                    foreach (IndicadorCartera rFila in LstDetalleComprobante)
                    {
                        if (contando == 13)
                        {
                            rFila.mes = rFila_c.mes;
                            rFila.valor_mora = rFila_c.valor_mora;
                            rFila.valor_mora = Math.Round(rFila.valor_mora / 1000000);
                            rFila.valor_cartera = rFila_c.valor_cartera;
                            rFila.valor_cartera = Math.Round(rFila.valor_cartera / 1000000);
                            rFila.valor_cartera_aldia = rFila_c.valor_cartera_aldia;
                            rFila.valor_cartera_aldia = Math.Round(rFila.valor_cartera_aldia / 1000000);
                            rFila.contribucion = rFila_c.contribucion;
                        }

                        if (rFila_c.valor_cartera == rFila.valor_cartera)
                        {
                            rFila.mes = rFila_c.mes;
                            rFila.valor_mora = rFila_c.valor_mora;
                            rFila.valor_mora = Math.Round(rFila.valor_mora / 1000000);
                            rFila.valor_cartera = rFila_c.valor_cartera;
                            rFila.valor_cartera = Math.Round(rFila.valor_cartera / 1000000);
                            rFila.valor_cartera_aldia = rFila_c.valor_cartera_aldia;
                            rFila.valor_cartera_aldia = Math.Round(rFila.valor_cartera_aldia / 1000000);
                            rFila.contribucion = rFila_c.contribucion;
                            rFila.porcentaje_total = Math.Round(Convert.ToDecimal(rFila_c.valor_cartera_aldia) * 100 / Convert.ToDecimal(rFila_c.valor_cartera));
                            bEncontro = true;
                            break;
                        }
                    }
                    if (bEncontro == false)
                    {
                        foreach (IndicadorCartera rFila in LstDetalleComprobante)
                        {
                            rFila.mes = rFila_c.mes;
                            rFila.valor_mora = rFila_c.valor_mora;
                            rFila.valor_mora = Math.Round(rFila.valor_mora / 1000000);
                            rFila.valor_cartera = rFila_c.valor_cartera;
                            rFila.valor_cartera = Math.Round(rFila.valor_cartera / 1000000);
                            rFila.valor_cartera_aldia = rFila_c.valor_cartera_aldia;
                            rFila.valor_cartera_aldia = Math.Round(rFila.valor_cartera_aldia / 1000000);
                            rFila.contribucion = rFila_c.contribucion;
                        }
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
            Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            // Cargar los datos a la gráfica

            Chart1.DataSource = LstDetalleComprobante;
            // Determinar si se genera por totales o por número
            Chart1.Titles["Title1"].Text = "AGENCIA " + ddloficina.SelectedText.ToUpper();
            if (ddlformapago.SelectedItem.Text != "Nomina")
            {
                Chart1.Titles["Title2"].Text = "Pagaduria por ".ToUpper() + "CAJA".ToUpper();
            }
            else
            {
                Chart1.Titles["Title2"].Text = "Pagaduria ".ToUpper() + ddlempresa.SelectedText.ToUpper();
            }

            if (ddlTipoGrafica1.Text == "2")
            {
                Chart1.Series["Series1"].YValueMembers = "valor_cartera";
                Chart1.Series["Series1"].XValueMember = "mes";
                Chart1.Series["Series1"].MarkerSize = 2;
                Chart1.Series["Series1"].LabelMapAreaAttributes = "valor_cartera";
                Chart1.Series["Series1"].IsValueShownAsLabel = true;
                CalloutAnnotation anotacion = new CalloutAnnotation();
                anotacion.Name = "Anotacion" + Chart1.Series["Series1"].LabelMapAreaAttributes; 
                anotacion.Alignment = System.Drawing.ContentAlignment.TopCenter;
                anotacion.Font = new Font("Microsoft Sans Serif", 2);
                anotacion.ForeColor = System.Drawing.Color.White;
                anotacion.ResizeToContent();
                Chart1.Series["Series1"].BorderWidth= 12;
                Chart1.Annotations.Add(anotacion);
                
            }
            else
            {

                // Mostrar los nombres de las seríes
                if (ddlPeriodo.Text.Trim() != "")
                {
                    Chart1.Series["Series1"].LegendText = "valor cartera total";
                    Chart1.Series["Series1"].IsVisibleInLegend = true;
                    Chart1.Series["Series2"].LegendText = "valor cartera al dia";
                    Chart1.Series["Series2"].IsVisibleInLegend = true;
                    Chart1.Series["Series3"].LegendText = "valor mora";
                    Chart1.Series["Series3"].IsVisibleInLegend = true;
                    Chart1.Series["Series4"].LegendText = "valor contribucion";
                    Chart1.Series["Series4"].IsVisibleInLegend = true;
                }
                else
                {
                    Chart1.Series["Series3"].IsVisibleInLegend = false;
                }
                Chart1.Series["Series1"].YValueMembers = "valor_cartera";
                Chart1.Series["Series1"].XValueMember = "mes";
                Chart1.Series["Series1"].LabelMapAreaAttributes = "valor_cartera";
                Chart1.Series["Series1"].IsValueShownAsLabel = true;
            }
            if (ddlPeriodo.Text.Trim() != "")
            {
                Chart1.Series["Series2"].YValueMembers = "valor_cartera_aldia";
                Chart1.Series["Series2"].XValueMember = "mes";
                Chart1.Series["Series2"].LabelMapAreaAttributes = "valor_cartera";
                Chart1.Series["Series2"].IsValueShownAsLabel = true;

            }
            if (ddlPeriodo.Text.Trim() != "")
            {
                Chart1.Series["Series3"].YValueMembers = "valor_mora";
                Chart1.Series["Series3"].XValueMember = "mes";
                Chart1.Series["Series3"].LabelMapAreaAttributes = "valor_cartera";
                Chart1.Series["Series3"].IsValueShownAsLabel = true;
            }
            if (ddlPeriodo.Text.Trim() != "")
            {
                Chart1.Series["Series4"].YValueMembers = "contribucion";
                Chart1.Series["Series4"].XValueMember = "mes";
                Chart1.Series["Series4"].LabelMapAreaAttributes = "valor_cartera";
                Chart1.Series["Series4"].IsValueShownAsLabel = true;
            }

            // Mostrar la gráfica en pantalla
            Chart1.DataBind();
            LstDetalleComprobante = EvolucionDesembolsoOficinasService.consultarparticipacionpagadurias(filtro, "", ddlVencimmiento.SelectedValue, fecha1, (Usuario)Session["Usuario"]);
            gvDatos.DataSource = LstDetalleComprobante;
            gvDatos.DataBind();

        }
        else
        {
        gvDatos.Visible = true;
        string filtro = obtfiltro();
        string pOrden = "";        
        // Haciendo visible las gráficas
        Chart1.Visible = true;

        // Traer los datos según criterio seleccionado
        List<IndicadorCartera> LstDetalleComprobante_c = new List<IndicadorCartera>();
        List<IndicadorCartera> LstDetalleComprobante = new List<IndicadorCartera>();
        string fecha1 ="";
        fecha1 += Convert.ToDateTime(ddlVencimmiento.SelectedValue).Day;
        fecha1 += "/";
        fecha1 += Convert.ToDateTime(ddlVencimmiento.SelectedValue).Month;
        fecha1 += "/";
        fecha1 += Convert.ToDateTime(ddlVencimmiento.SelectedValue).Year;
        pOrden = "v.fecha_historico";
        LstDetalleComprobante = EvolucionDesembolsoOficinasService.consultarparticipacionpagadurias(filtro,pOrden, ddlVencimmiento.SelectedValue, fecha1, (Usuario)Session["Usuario"]);
        // Si se seleccionó un segundo periodo entonces cargar los datos
        if (ddlPeriodo.Text.Trim() != "")
        {            
            if (ddlVencimmiento.SelectedValue != "" && ddlPeriodo.SelectedValue != "")
            {                
                LstDetalleComprobante_c = EvolucionDesembolsoOficinasService.consultarparticipacionpagadurias(filtro,pOrden, ddlVencimmiento.SelectedValue, fecha1, (Usuario)Session["Usuario"]);
            }
            foreach (IndicadorCartera rFila_c in LstDetalleComprobante_c)
            {
                Boolean bEncontro = false;
                foreach (IndicadorCartera rFila in LstDetalleComprobante)
                {
                    if (rFila_c.mes == rFila.mes)
                    {
                        rFila.mes = rFila_c.mes;
                        rFila.valor_mora = rFila_c.valor_mora ;
                        rFila.valor_cartera = rFila_c.valor_cartera;
                        rFila.valor_cartera_aldia = rFila_c.valor_cartera_aldia ;
                        rFila.contribucion = rFila_c.contribucion;
                        rFila.porcentaje_total = Math.Round(Convert.ToDecimal(rFila_c.valor_cartera_aldia) * 100 / Convert.ToDecimal(rFila_c.valor_cartera));
                        bEncontro = true;
                        break;
                    }
                }
                if (bEncontro == false)
                {
                    IndicadorCartera rFilaNueva = new IndicadorCartera();
                    rFilaNueva.valor_mora = 0;
                    rFilaNueva.valor_cartera = 0;
                    rFilaNueva.valor_cartera_aldia = 0;
                    rFilaNueva.contribucion = 0;
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
        Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        // Cargar los datos a la gráfica
        Chart1.DataSource = LstDetalleComprobante;
        // Determinar si se genera por totales o por número
        Chart1.Titles["Title1"].Text = "AGENCIA "+ ddloficina.SelectedText.ToUpper();
        if (ddlformapago.SelectedItem.Text != "Nomina")
        {
            Chart1.Titles["Title2"].Text = "Pagaduria por ".ToUpper() + "CAJA".ToUpper();
        }
        else 
        {
            Chart1.Titles["Title2"].Text = "Pagaduria ".ToUpper() + ddlempresa.SelectedText.ToUpper();
        }
    
        if (ddlTipoGrafica1.Text == "2")
        {
            Chart1.Series["Series1"].YValueMembers = "valor_cartera";
            Chart1.Series["Series1"].XValueMember = "mes";
            Chart1.Series["Series1"].LabelMapAreaAttributes = "valor_cartera";
            Chart1.Series["Series1"].IsValueShownAsLabel = false;
        }
        else
        {

            // Mostrar los nombres de las seríes
            if (ddlPeriodo.Text.Trim() != "")
            {
                Chart1.Series["Series1"].LegendText = "valor cartera total";
                Chart1.Series["Series1"].IsVisibleInLegend = true;
                Chart1.Series["Series2"].LegendText = "valor cartera al dia";
                Chart1.Series["Series2"].IsVisibleInLegend = true;
                Chart1.Series["Series3"].LegendText = "valor mora";
                Chart1.Series["Series3"].IsVisibleInLegend = true;
                Chart1.Series["Series4"].LegendText = "valor contribucion";
                Chart1.Series["Series4"].IsVisibleInLegend = true;
            }
            else
            {
                Chart1.Series["Series3"].IsVisibleInLegend = false;
            }
            Chart1.Series["Series1"].YValueMembers = "valor_cartera";
            Chart1.Series["Series1"].XValueMember = "mes";
            Chart1.Series["Series1"].LabelMapAreaAttributes = "valor_cartera";
            Chart1.Series["Series1"].IsValueShownAsLabel = false;
        }
        if (ddlPeriodo.Text.Trim() != "")
        {
            Chart1.Series["Series2"].YValueMembers = "valor_cartera_aldia";
            Chart1.Series["Series2"].XValueMember = "mes";
            Chart1.Series["Series2"].LabelMapAreaAttributes = "valor_cartera";
            Chart1.Series["Series2"].IsValueShownAsLabel = false;
           
        }
        if (ddlPeriodo.Text.Trim() != "")
        {
            Chart1.Series["Series3"].YValueMembers = "valor_mora";
            Chart1.Series["Series3"].XValueMember = "mes";
            Chart1.Series["Series3"].LabelMapAreaAttributes = "valor_cartera";
            Chart1.Series["Series3"].IsValueShownAsLabel = false;
        }
        if (ddlPeriodo.Text.Trim() != "")
        {
            Chart1.Series["Series4"].YValueMembers = "contribucion";
            Chart1.Series["Series4"].XValueMember = "mes";
            Chart1.Series["Series4"].LabelMapAreaAttributes = "valor_cartera";
            Chart1.Series["Series4"].IsValueShownAsLabel = false;
        }
  
        // Mostrar la gráfica en pantalla
        Chart1.DataBind();
        LstDetalleComprobante = EvolucionDesembolsoOficinasService.consultarparticipacionpagadurias(filtro, "", ddlVencimmiento.SelectedValue, fecha1, (Usuario)Session["Usuario"]);
        gvDatos.DataSource = LstDetalleComprobante;
        gvDatos.DataBind();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
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

        if (chkmostrarlegendas.Checked == true)
        {
            gvDatos.Visible = true;
            string filtro = obtfiltro();
            string pOrden = "";            
            // Haciendo visible las gráficas
            Chart1.Visible = true;

            // Traer los datos según criterio seleccionado
            List<IndicadorCartera> LstDetalleComprobante_c = new List<IndicadorCartera>();
            List<IndicadorCartera> LstDetalleComprobante = new List<IndicadorCartera>();
            string fecha1 = "";
            fecha1 += Convert.ToDateTime(ddlVencimmiento.SelectedValue).Day;
            fecha1 += "/";
            fecha1 += Convert.ToDateTime(ddlVencimmiento.SelectedValue).Month;
            fecha1 += "/";
            fecha1 += Convert.ToDateTime(ddlVencimmiento.SelectedValue).Year;
            pOrden = "v.fecha_historico";
            LstDetalleComprobante = EvolucionDesembolsoOficinasService.consultarparticipacionpagadurias(filtro,pOrden, ddlVencimmiento.SelectedValue, fecha1, (Usuario)Session["Usuario"]);
            // Si se seleccionó un segundo periodo entonces cargar los datos
            if (ddlPeriodo.Text.Trim() != "")
            {
                if (ddlVencimmiento.SelectedValue != "" && ddlPeriodo.SelectedValue != "")
                {                    
                    LstDetalleComprobante_c = EvolucionDesembolsoOficinasService.consultarparticipacionpagadurias(filtro,pOrden, ddlVencimmiento.SelectedValue, fecha1, (Usuario)Session["Usuario"]);
                }
                foreach (IndicadorCartera rFila_c in LstDetalleComprobante_c)
                {
                    int contando = 0;
                    Boolean bEncontro = false;
                    foreach (IndicadorCartera rFila in LstDetalleComprobante)
                    {
                        if (contando == 13)
                        {
                            rFila.mes = rFila_c.mes;
                            rFila.valor_mora = rFila_c.valor_mora;
                            rFila.valor_mora = Math.Round(rFila.valor_mora / 1000000);
                            rFila.valor_cartera = rFila_c.valor_cartera;
                            rFila.valor_cartera = Math.Round(rFila.valor_cartera / 1000000);
                            rFila.valor_cartera_aldia = rFila_c.valor_cartera_aldia;
                            rFila.valor_cartera_aldia = Math.Round(rFila.valor_cartera_aldia / 1000000);
                            rFila.contribucion = rFila_c.contribucion;
                        }

                        if (rFila_c.valor_cartera == rFila.valor_cartera)
                        {
                            rFila.mes = rFila_c.mes;
                            rFila.valor_mora = rFila_c.valor_mora;
                            rFila.valor_mora = Math.Round(rFila.valor_mora / 1000000);
                            rFila.valor_cartera = rFila_c.valor_cartera;
                            rFila.valor_cartera = Math.Round(rFila.valor_cartera / 1000000);
                            rFila.valor_cartera_aldia = rFila_c.valor_cartera_aldia;
                            rFila.valor_cartera_aldia = Math.Round(rFila.valor_cartera_aldia / 1000000);
                            rFila.contribucion = rFila_c.contribucion;
                            rFila.porcentaje_total = Math.Round(Convert.ToDecimal(rFila_c.valor_cartera_aldia) * 100 / Convert.ToDecimal(rFila_c.valor_cartera));
                            bEncontro = true;
                            break;
                        }
                    }
                    if (bEncontro == false)
                    {
                        foreach (IndicadorCartera rFila in LstDetalleComprobante)
                        {
                            rFila.mes = rFila_c.mes;
                            rFila.valor_mora = rFila_c.valor_mora;
                            rFila.valor_mora = Math.Round(rFila.valor_mora / 1000000);
                            rFila.valor_cartera = rFila_c.valor_cartera;
                            rFila.valor_cartera = Math.Round(rFila.valor_cartera / 1000000);
                            rFila.valor_cartera_aldia = rFila_c.valor_cartera_aldia;
                            rFila.valor_cartera_aldia = Math.Round(rFila.valor_cartera_aldia / 1000000);
                            rFila.contribucion = rFila_c.contribucion;
                        }
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
            Chart1.Titles["Title1"].Text = "AGENCIA " + ddloficina.SelectedText.ToUpper();
            if (ddlformapago.SelectedItem.Text != "Nomina")
            {
                Chart1.Titles["Title2"].Text = "Pagaduria por ".ToUpper() + "CAJA".ToUpper();
            }
            else
            {
                Chart1.Titles["Title2"].Text = "Pagaduria ".ToUpper() + ddlempresa.SelectedText.ToUpper();
            }

            if (ddlTipoGrafica1.Text == "2")
            {
                Chart1.Series["Series1"].YValueMembers = "valor_cartera";
                Chart1.Series["Series1"].XValueMember = "mes";
                Chart1.Series["Series1"].MarkerSize = 2;
                Chart1.Series["Series1"].LabelMapAreaAttributes = "valor_cartera";
                Chart1.Series["Series1"].IsValueShownAsLabel = true;
                CalloutAnnotation anotacion = new CalloutAnnotation();
                anotacion.Name = "Anotacion" + Chart1.Series["Series1"].LabelMapAreaAttributes;
                anotacion.Alignment = System.Drawing.ContentAlignment.TopCenter;
                anotacion.Font = new Font("Microsoft Sans Serif", 2);
                anotacion.ForeColor = System.Drawing.Color.White;
                anotacion.ResizeToContent();
                Chart1.Series["Series1"].BorderWidth = 12;
                Chart1.Annotations.Add(anotacion);

            }
            else
            {

                // Mostrar los nombres de las seríes
                if (ddlPeriodo.Text.Trim() != "")
                {
                    Chart1.Series["Series1"].LegendText = "valor cartera total";
                    Chart1.Series["Series1"].IsVisibleInLegend = true;
                    Chart1.Series["Series2"].LegendText = "valor cartera al dia";
                    Chart1.Series["Series2"].IsVisibleInLegend = true;
                    Chart1.Series["Series3"].LegendText = "valor mora";
                    Chart1.Series["Series3"].IsVisibleInLegend = true;
                    Chart1.Series["Series4"].LegendText = "valor contribucion";
                    Chart1.Series["Series4"].IsVisibleInLegend = true;
                }
                else
                {
                    Chart1.Series["Series3"].IsVisibleInLegend = false;
                }
                Chart1.Series["Series1"].YValueMembers = "valor_cartera";
                Chart1.Series["Series1"].XValueMember = "mes";
                Chart1.Series["Series1"].LabelMapAreaAttributes = "valor_cartera";
                Chart1.Series["Series1"].IsValueShownAsLabel = true;
            }
            if (ddlPeriodo.Text.Trim() != "")
            {
                Chart1.Series["Series2"].YValueMembers = "valor_cartera_aldia";
                Chart1.Series["Series2"].XValueMember = "mes";
                Chart1.Series["Series2"].LabelMapAreaAttributes = "valor_cartera";
                Chart1.Series["Series2"].IsValueShownAsLabel = true;

            }
            if (ddlPeriodo.Text.Trim() != "")
            {
                Chart1.Series["Series3"].YValueMembers = "valor_mora";
                Chart1.Series["Series3"].XValueMember = "mes";
                Chart1.Series["Series3"].LabelMapAreaAttributes = "valor_cartera";
                Chart1.Series["Series3"].IsValueShownAsLabel = true;
            }
            if (ddlPeriodo.Text.Trim() != "")
            {
                Chart1.Series["Series4"].YValueMembers = "contribucion";
                Chart1.Series["Series4"].XValueMember = "mes";
                Chart1.Series["Series4"].LabelMapAreaAttributes = "valor_cartera";
                Chart1.Series["Series4"].IsValueShownAsLabel = true;
            }

            // Mostrar la gráfica en pantalla
            Chart1.DataBind();
            LstDetalleComprobante = EvolucionDesembolsoOficinasService.consultarparticipacionpagadurias(filtro, "", ddlVencimmiento.SelectedValue, fecha1, (Usuario)Session["Usuario"]);
            gvDatos.DataSource = LstDetalleComprobante;
            gvDatos.DataBind();

        }
        else
        {
            gvDatos.Visible = true;
            string filtro = obtfiltro();
            string pOrden = "";            
            // Haciendo visible las gráficas
            Chart1.Visible = true;

            // Traer los datos según criterio seleccionado
            List<IndicadorCartera> LstDetalleComprobante_c = new List<IndicadorCartera>();
            List<IndicadorCartera> LstDetalleComprobante = new List<IndicadorCartera>();
            string fecha1 = "";
            fecha1 += Convert.ToDateTime(ddlVencimmiento.SelectedValue).Day;
            fecha1 += "/";
            fecha1 += Convert.ToDateTime(ddlVencimmiento.SelectedValue).Month;
            fecha1 += "/";
            fecha1 += Convert.ToDateTime(ddlVencimmiento.SelectedValue).Year;
            pOrden = "v.fecha_historico";
            LstDetalleComprobante = EvolucionDesembolsoOficinasService.consultarparticipacionpagadurias(filtro,pOrden, ddlVencimmiento.SelectedValue, fecha1, (Usuario)Session["Usuario"]);
            // Si se seleccionó un segundo periodo entonces cargar los datos
            if (ddlPeriodo.Text.Trim() != "")
            {
                if (ddlVencimmiento.SelectedValue != "" && ddlPeriodo.SelectedValue != "")
                {                    
                    LstDetalleComprobante_c = EvolucionDesembolsoOficinasService.consultarparticipacionpagadurias(filtro,pOrden, ddlVencimmiento.SelectedValue, fecha1, (Usuario)Session["Usuario"]);
                }
                foreach (IndicadorCartera rFila_c in LstDetalleComprobante_c)
                {
                    Boolean bEncontro = false;
                    foreach (IndicadorCartera rFila in LstDetalleComprobante)
                    {
                        if (rFila_c.mes == rFila.mes)
                        {
                            rFila.mes = rFila_c.mes;
                            rFila.valor_mora = rFila_c.valor_mora;
                            rFila.valor_cartera = rFila_c.valor_cartera;
                            rFila.valor_cartera_aldia = rFila_c.valor_cartera_aldia;
                            rFila.contribucion = rFila_c.contribucion;
                            rFila.porcentaje_total = Math.Round(Convert.ToDecimal(rFila_c.valor_cartera_aldia) * 100 / Convert.ToDecimal(rFila_c.valor_cartera));
                            bEncontro = true;
                            break;
                        }
                    }
                    if (bEncontro == false)
                    {
                        IndicadorCartera rFilaNueva = new IndicadorCartera();
                        rFilaNueva.valor_mora = 0;
                        rFilaNueva.valor_cartera = 0;
                        rFilaNueva.valor_cartera_aldia = 0;
                        rFilaNueva.contribucion = 0;
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
            Chart1.Titles["Title1"].Text = "AGENCIA " + ddloficina.SelectedText.ToUpper();
            if (ddlformapago.SelectedItem.Text != "Nomina")
            {
                Chart1.Titles["Title2"].Text = "Pagaduria por ".ToUpper() + "CAJA".ToUpper();
            }
            else
            {
                Chart1.Titles["Title2"].Text = "Pagaduria ".ToUpper() + ddlempresa.SelectedText.ToUpper();
            }

            if (ddlTipoGrafica1.Text == "2")
            {
                Chart1.Series["Series1"].YValueMembers = "valor_cartera";
                Chart1.Series["Series1"].XValueMember = "mes";
                Chart1.Series["Series1"].LabelMapAreaAttributes = "valor_cartera";
                Chart1.Series["Series1"].IsValueShownAsLabel = false;
            }
            else
            {

                // Mostrar los nombres de las seríes
                if (ddlPeriodo.Text.Trim() != "")
                {
                    Chart1.Series["Series1"].LegendText = "valor cartera total";
                    Chart1.Series["Series1"].IsVisibleInLegend = true;
                    Chart1.Series["Series2"].LegendText = "valor cartera al dia";
                    Chart1.Series["Series2"].IsVisibleInLegend = true;
                    Chart1.Series["Series3"].LegendText = "valor mora";
                    Chart1.Series["Series3"].IsVisibleInLegend = true;
                    Chart1.Series["Series4"].LegendText = "valor contribucion";
                    Chart1.Series["Series4"].IsVisibleInLegend = true;
                }
                else
                {
                    Chart1.Series["Series3"].IsVisibleInLegend = false;
                }
                Chart1.Series["Series1"].YValueMembers = "valor_cartera";
                Chart1.Series["Series1"].XValueMember = "mes";
                Chart1.Series["Series1"].LabelMapAreaAttributes = "valor_cartera";
                Chart1.Series["Series1"].IsValueShownAsLabel = false;
            }
            if (ddlPeriodo.Text.Trim() != "")
            {
                Chart1.Series["Series2"].YValueMembers = "valor_cartera_aldia";
                Chart1.Series["Series2"].XValueMember = "mes";
                Chart1.Series["Series2"].LabelMapAreaAttributes = "valor_cartera";
                Chart1.Series["Series2"].IsValueShownAsLabel = false;

            }
            if (ddlPeriodo.Text.Trim() != "")
            {
                Chart1.Series["Series3"].YValueMembers = "valor_mora";
                Chart1.Series["Series3"].XValueMember = "mes";
                Chart1.Series["Series3"].LabelMapAreaAttributes = "valor_cartera";
                Chart1.Series["Series3"].IsValueShownAsLabel = false;
            }
            if (ddlPeriodo.Text.Trim() != "")
            {
                Chart1.Series["Series4"].YValueMembers = "contribucion";
                Chart1.Series["Series4"].XValueMember = "mes";
                Chart1.Series["Series4"].LabelMapAreaAttributes = "valor_cartera";
                Chart1.Series["Series4"].IsValueShownAsLabel = false;
            }

            // Mostrar la gráfica en pantalla
            Chart1.DataBind();
            LstDetalleComprobante_c = EvolucionDesembolsoOficinasService.consultarparticipacionpagadurias(filtro, "", ddlVencimmiento.SelectedValue, fecha1, (Usuario)Session["Usuario"]);
            gvDatos.DataSource = LstDetalleComprobante;
            gvDatos.DataBind();
        }


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
        string tmpChartName = "grafica.jpg";
        string imgPath = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName;

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

    void MostrarAnotacion(Chart pChart, Int32 pItem, string pTexto, double pPosicion)
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
        btnInforme_Click(null, null);
       
    }
}
