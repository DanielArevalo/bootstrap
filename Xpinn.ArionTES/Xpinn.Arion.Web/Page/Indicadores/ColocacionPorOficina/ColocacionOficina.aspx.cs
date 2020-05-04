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
            if (Session[EvolucionDesembolsoOficinasService.CodigoProgramacolocacion + ".id"] != null)
                VisualizarOpciones(EvolucionDesembolsoOficinasService.CodigoProgramacolocacion, "E");
            else
                VisualizarOpciones(EvolucionDesembolsoOficinasService.CodigoProgramacolocacion, "A");

            Site toolBar = (Site)this.Master;
            txtColorFondo.eventoCambiar += txtColorFondo_TextChanged;
            toolBar.eventoConsultar += btnInforme_Click;
            toolBar.eventoExportar += btnExportar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EvolucionDesembolsoOficinasService.CodigoProgramacolocacion, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargardrop();
            ddlTipoGrafica1.SelectedIndex = 1;
            ddlTipoGrafica2.SelectedIndex = 1;
            if (Session[EvolucionDesembolsoOficinasService.CodigoProgramacolocacion + ".id"] != null)
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
            
        }
        btnInforme_Click(null, null);
    }
    protected string obtfiltro()
    {
        string filtrare = "";
        if (ddlLinea.SelectedText != "")
        {
            filtrare += " and l.cod_linea_Credito In (";
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
    protected void cargardrop()
    {

        Xpinn.FabricaCreditos.Services.OficinaService linahorroServicio = new Xpinn.FabricaCreditos.Services.OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina linahorroVista = new Xpinn.FabricaCreditos.Entities.Oficina();
        ddloficina.DataTextField = "nombre";
        ddloficina.DataValueField = "codigo";
        ddloficina.DataSource = linahorroServicio.ListarOficinas(linahorroVista, (Usuario)Session["usuario"]);
        ddloficina.DataBind();

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
    


    protected void btnInforme_Click(object sender, EventArgs e)
    {
        gvDatos.Visible = true;
        if (ddlVencimmiento.SelectedItem == null)
        {
            VerError("No existen fechas de corte para realizar el reporte");
            return;
        }
       
        // Haciendo visible las gráficas
        Chart1.Visible = true;
        Chart2.Visible = cbNumero.Checked;
        string Filtro = "";
        Filtro = obtfiltro();
        ddlTipoGrafica2.Visible = cbNumero.Checked;
        lblbTipoGrafica2.Visible = cbNumero.Checked;
        btnExportarpor.Visible = cbNumero.Checked;
        if (ddlPeriodo.Text.Trim() != "")
        {
            if (gvDatos.Columns.Count >= 3)
                gvDatos.Columns[3].Visible = true;
            if (gvDatos.Columns.Count >= 4)
                gvDatos.Columns[4].Visible = true;
        }
        else
        {
            if (gvDatos.Columns.Count >= 3)
                gvDatos.Columns[3].Visible = false;
            if (gvDatos.Columns.Count >= 4)
                gvDatos.Columns[4].Visible = false;
        }
        if (cbNumero.Checked)
        {
            Chart1.Height = 400;
            Chart1.Width = 500;
        }
        else
        {
            Chart1.Height = 600;
            Chart1.Width = 800;
        }

        // Traer los datos según criterio seleccionado
        List<IndicadorCartera> LstDetalleComprobante = new List<IndicadorCartera>();
        List<IndicadorCartera> LstDetalleComprobante_c = new List<IndicadorCartera>();

        DateTime pFechaSelect = DateTime.MinValue, pfechaIni = DateTime.MinValue, pfechaFin = DateTime.MinValue;
        string oficina = "";
        oficina = ddloficina.SelectedText != "" ? ddloficina.SelectedValue : "";
        if (ddlVencimmiento.SelectedValue != "")
        {
            pFechaSelect = DateTime.MinValue; pfechaIni = DateTime.MinValue; pfechaFin = DateTime.MinValue;
            if (ddlVencimmiento.SelectedItem != null)
            {
                pFechaSelect = Convert.ToDateTime(ddlVencimmiento.SelectedValue);
                string fechaArm = "01/" + pFechaSelect.Month + "/" + pFechaSelect.Year;
                pfechaIni = Convert.ToDateTime(fechaArm);
                pfechaFin = pFechaSelect;
            }
            LstDetalleComprobante = EvolucionDesembolsoOficinasService.colocacionoficina(oficina, pfechaIni.ToShortDateString(), pfechaFin.ToShortDateString(), Filtro,(Usuario)Session["Usuario"]);
        }
        // Si se seleccionó un segundo periodo entonces cargar los datos
        if (ddlPeriodo.Text.Trim() != "")
        {
            if (ddlPeriodo.SelectedValue != "")
            {
                pFechaSelect = DateTime.MinValue; pfechaIni = DateTime.MinValue; pfechaFin = DateTime.MinValue;
                if (ddlPeriodo.SelectedItem != null)
                {
                    pFechaSelect = Convert.ToDateTime(ddlPeriodo.SelectedValue);
                    string fechaArm = "01/" + pFechaSelect.Month + "/" + pFechaSelect.Year;
                    pfechaIni = Convert.ToDateTime(fechaArm);
                    pfechaFin = pFechaSelect;
                }
                LstDetalleComprobante_c = EvolucionDesembolsoOficinasService.colocacionoficina(oficina, pfechaIni.ToShortDateString(), pfechaFin.ToShortDateString(), Filtro,(Usuario)Session["Usuario"]);
            }
            int rowIndex = 0;
            List<int> lstPosiciones = new List<int>();
            foreach (IndicadorCartera rFila_c in LstDetalleComprobante_c)
            {
                Boolean bEncontro = false;
                foreach (IndicadorCartera rFila in LstDetalleComprobante)
                {
                    if (rFila_c.nombre == rFila.nombre)
                    {
                        bEncontro = true;
                        break;
                    }
                }
                if (bEncontro == false)
                {
                    lstPosiciones.Add(rowIndex);
                }
                rowIndex++;
            }
            //eliminando los registros que no pertenecen a la lista principal
            if (lstPosiciones.Count > 0)
            {
                int x = 0;
                foreach (int pRow in lstPosiciones)
                {
                    if (x == 0)
                        LstDetalleComprobante_c.RemoveAt(pRow);
                    else
                        LstDetalleComprobante_c.RemoveAt(pRow - x);
                    x++;
                }
            }
        }

        //creando la tabla
        DataTable dtDatos = new DataTable();
        dtDatos.Clear();
        dtDatos.Columns.Add("nombre", typeof(string));
        dtDatos.Columns["nombre"].AllowDBNull = true;
        dtDatos.Columns["nombre"].DefaultValue = "0";
        dtDatos.Columns.Add("valor", typeof(decimal));
        dtDatos.Columns.Add("fecha_historico", typeof(string));
        dtDatos.Columns.Add("valor_comp", typeof(decimal));
        dtDatos.Columns.Add("fecha_historico_comp", typeof(string));

        //llenando la tabla
        Decimal total = 0;
        foreach (IndicadorCartera rFila in LstDetalleComprobante)
        {
            DataRow drDatos;
            drDatos = dtDatos.NewRow();
            if (rFila.nombre != null)
            {
                drDatos[0] = rFila.nombre;
                drDatos[1] = rFila.valor;                
                drDatos[2] = ddlVencimmiento.Text;
                foreach (IndicadorCartera rFila_c in LstDetalleComprobante_c)
                {
                    if (rFila_c.nombre != null)
                    {
                        if (rFila_c.nombre == rFila.nombre)
                        {
                            drDatos[3] = rFila_c.valor;
                        }
                    }
                }
                drDatos[4] = ddlPeriodo.Text;
                total += rFila.valor;
                int posicion = 0;                
                drDatos[posicion] = Convert.ToString(rFila.nombre);
                dtDatos.Rows.Add(drDatos);
                posicion += 1;
            }
        }
        // Adicionar el total
        DataRow rTotal = dtDatos.NewRow();;
        rTotal[0] = "TOTAL";
        rTotal[1] = total;
        rTotal[2] = ddlVencimmiento.Text;
        rTotal[4] = ddlPeriodo.Text;
        dtDatos.Rows.Add(rTotal);
        gvDatos.DataSource = dtDatos;
        gvDatos.DataBind();


        // --------------------------------------------------------------------------------------------------------------------------------------------
        // Mostrar la primera gráfica
        // -------------------------------------------------------------------------------------------------------------------------------------------
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        if (ddlTipoGrafica1.Text == "2")
            if (!cbNumero.Checked)
            {
                Chart1.Legends[0].Docking = Docking.Right;
                Chart1.Legends[0].LegendStyle = LegendStyle.Column;
            }
        // Determinar el tipo de gráfica
        Chart1.Series["Series1"].ChartType = TipoGrafica(ddlTipoGrafica1);
        Chart1.Series["Series2"].ChartType = TipoGrafica(ddlTipoGrafica1);
        // Cargar los datos a la gráfica
        Chart1.DataSource = dtDatos;

        // Determinar si se genera por totales o por número
        Chart1.Titles["Title1"].Text = "COLOCACION DE CREDITOS " + ddloficina.SelectedText;
        Chart1.Titles["Title1"].ForeColor = System.Drawing.Color.Black;
        Chart1.Titles["Title1"].Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold);
        Chart1.Titles["Title2"].Text = "($ millones)";
        Chart1.Titles["Title2"].ForeColor = System.Drawing.Color.Black;
        Chart1.Titles["Title2"].Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold);
        Chart1.Series["Series1"].LabelFormat = "n0";
        Chart1.Series["Series2"].LabelFormat = "n0";
        if (ddlTipoGrafica1.Text == "2")
        {
            // Cargar los datos a la gráfica
            Chart1.DataSource = LstDetalleComprobante;            
            Chart1.Series["Series1"].LabelMapAreaAttributes = "valor";
            Chart1.Series["Series1"].IsValueShownAsLabel = true;
            Chart1.Series["Series1"].YValueMembers = "valor";
            Chart1.Series["Series1"].XValueMember = "nombre";
        }
        else
        {
            Chart1.Series["Series1"].LegendText = ddlVencimmiento.Text;
            Chart1.Series["Series1"].YValueMembers = "valor";
            Chart1.Series["Series1"].XValueMember = "nombre";
            Chart1.Series["Series1"].LabelMapAreaAttributes = "valor";
            Chart1.Series["Series1"].IsValueShownAsLabel = true;

            Chart1.Series["Series2"].LegendText = ddlPeriodo.Text;
            Chart1.Series["Series2"].YValueMembers = "valor_comp";
            Chart1.Series["Series2"].XValueMember = "nombre";
            Chart1.Series["Series2"].LabelMapAreaAttributes = "valor_comp";
            Chart1.Series["Series2"].IsValueShownAsLabel = true;
            if (ddlPeriodo.Text.Trim() != "")
            {
                Chart1.Series["Series2"].IsVisibleInLegend = true;
            }
            else
            {
                Chart1.Series["Series2"].IsVisibleInLegend = false;
            }
        }
        // Mostrar los porcentajes de participación
        Chart1.Annotations.Clear();
        if (ddlTipoGrafica1.Text == "1")
            for (int i = 0; i < LstDetalleComprobante.Count(); i++)
            {
                MostrarAnotacion(Chart1, i, Math.Round(Convert.ToDouble(LstDetalleComprobante[i].valor) / 2), Convert.ToString(Math.Round(Convert.ToDouble(LstDetalleComprobante[i].numero), 1)) + "%");
            }
        // Mostrar la gráfica en pantalla
        Chart1.DataBind();

        // --------------------------------------------------------------------------------------------------------------------------------------------
        // Mostrar la segunda gráfica    
        // -------------------------------------------------------------------------------------------------------------------------------------------
        Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        Chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        Chart2.Series["Series1"].ChartType = TipoGrafica(ddlTipoGrafica2);
        Chart2.Series["Series2"].ChartType = TipoGrafica(ddlTipoGrafica2);
        Chart2.Titles["Title1"].Text = "Colocación de Créditos";
        Chart2.Titles["Title1"].TextStyle = TextStyle.Shadow;
        Chart2.Titles["Title1"].Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold);
        Chart2.Titles["Title2"].Text = "Cantidad";
        Chart2.DataSource = LstDetalleComprobante;
        if (ddlTipoGrafica2.Text == "2")
        {
            Chart2.Series["Series1"].LabelMapAreaAttributes = "numero";
            Chart2.Series["Series1"].IsValueShownAsLabel = true;
            Chart2.Series["Series1"].YValueMembers = "numero";
            Chart2.Series["Series1"].XValueMember = "nombre";
        }
        else
        {
            Chart2.Series["Series1"].LegendText = ddlVencimmiento.Text;
            Chart2.Series["Series1"].LabelMapAreaAttributes = "valor";
            Chart2.Series["Series1"].IsValueShownAsLabel = true;
            Chart2.Series["Series1"].YValueMembers = "numero";
            Chart2.Series["Series1"].XValueMember = "nombre";
        }
        if (ddlPeriodo.Text.Trim() != "")
        {
            Chart2.Series["Series2"].LegendText = ddlPeriodo.Text;
            Chart2.Series["Series2"].IsVisibleInLegend = true;
            Chart2.Series["Series2"].YValueMembers = "numero";
            Chart2.Series["Series2"].XValueMember = "nombre";
            Chart2.Series["Series2"].LabelMapAreaAttributes = "numero";
            Chart2.Series["Series2"].IsValueShownAsLabel = true;
        }
        else
        {
            Chart2.Series["Series2"].LegendText = ddlPeriodo.Text;
            Chart2.Series["Series2"].IsVisibleInLegend = false;
        }
        // Mostrar los porcentajes de participación
        Chart2.Annotations.Clear();
        if (ddlTipoGrafica2.Text == "1")
            for (int i = 0; i < LstDetalleComprobante.Count(); i++)
            {
                MostrarAnotacion(Chart2, i, Math.Round(Convert.ToDouble(LstDetalleComprobante[i].numero)), Convert.ToString(Math.Round(Convert.ToDouble(LstDetalleComprobante[i].numero), 1)) + "%");
            }
        // Refrescar datos de la gráfica en pantalla
        Chart2.DataBind();
        // Totalizar los Datos
        //gvDatos.ShowFooter = true;
        //gvDatos.FooterRow.Cells[0].Text = "TOTAL";
        //gvDatos.FooterRow.Cells[1].Text = total.ToString("n");
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

    protected void cbNumero_CheckedChanged(object sender, EventArgs e)
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
        //anotacion.Text = pPosicion.ToString();
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