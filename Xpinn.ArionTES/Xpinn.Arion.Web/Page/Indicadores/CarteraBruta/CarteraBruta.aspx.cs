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
using System.Drawing.Text;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Indicadores.Services.CarteraBrutaService CarteraBrutaService = new Xpinn.Indicadores.Services.CarteraBrutaService();
    InstalledFontCollection ListaFuentes = new InstalledFontCollection();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[CarteraBrutaService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(CarteraBrutaService.CodigoPrograma, "E");
            else
                VisualizarOpciones(CarteraBrutaService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            txtColorFondo.eventoCambiar += txtColorFondo_TextChanged;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CarteraBrutaService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session[CarteraBrutaService.CodigoPrograma + ".id"] != null)
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
            ddlFuentes.DataSource = ListaFuentes.Families;
            ddlFuentes.DataTextField = "Name";
            ddlFuentes.DataBind();
           
            Session["ColorFondo"]=null;
            Session["ColorTotal"] = null;
            Session["ColorComercial"] = null;
            Session["ColorConsumo"] = null;
            Session["ColorVivienda"] = null;
            Session["ColorMicroCredito"] = null;

           
        }
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

        Xpinn.Cartera.Services.ClasificacionCarteraService clasificacionService = new Xpinn.Cartera.Services.ClasificacionCarteraService();
        List<Xpinn.Cartera.Entities.ClasificacionCartera> lstClasificacion = new List<Xpinn.Cartera.Entities.ClasificacionCartera>();
        lstClasificacion = clasificacionService.ListarClasificacion((Usuario)Session["Usuario"]);
        var lstProductos = (from p in lstClasificacion orderby p.codigo select new { p.codigo, p.descripcion }).ToList();



       // //Limpiando datos
       // DataTable dtReporte = new DataTable();
       // dtReporte.Clear();
       // gvDatos.Columns.Clear();
       // DataTable dtCantidad = new DataTable();
       // dtCantidad.Clear();
       // gvCantidad.Columns.Clear();
       // List<CarteraVencida> LstDetalleComprobante = new List<CarteraVencida>();

       // dtReporte.Columns.Add("fecha_historico_tot", typeof(String)).AllowDBNull = true;
       // dtReporte.Columns.Add("total_cartera", typeof(decimal)).AllowDBNull = true;
       //// dtReporte.Columns.Add("numero_cartera", typeof(decimal)).AllowDBNull = true;
       // dtReporte.Columns.Add("total_cartera_microcredito", typeof(decimal)).AllowDBNull = true;
       // //dtReporte.Columns.Add("numero_cartera_microcredito", typeof(decimal)).AllowDBNull = true;
       // dtReporte.Columns.Add("total_cartera_consumo", typeof(decimal)).AllowDBNull = true;
       // //dtReporte.Columns.Add("numero_cartera_consumo", typeof(decimal)).AllowDBNull = true;
       // dtReporte.Columns.Add("total_cartera_comercial", typeof(decimal)).AllowDBNull = true;
       // //dtReporte.Columns.Add("numero_cartera_comercial", typeof(decimal)).AllowDBNull = true;
       // dtReporte.Columns.Add("total_cartera_vivienda", typeof(decimal)).AllowDBNull = true;
       // //dtReporte.Columns.Add("numero_cartera_vivienda", typeof(decimal)).AllowDBNull = true;

       // BoundField ColumnBound1 = new BoundField { };
       // ColumnBound1 = new BoundField();
       // ColumnBound1.HeaderText = "Fecha Historico";
       // ColumnBound1.DataField = "fecha_historico_tot";
       // ColumnBound1.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
       // ColumnBound1.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
       // gvDatos.Columns.Add(ColumnBound1);

       // BoundField ColumnBound2 = new BoundField { };
       // ColumnBound2 = new BoundField();
       // ColumnBound2.HeaderText = "Valor Total";
       // ColumnBound2.DataField = "total_cartera";
       // ColumnBound2.DataFormatString = "{0:n}";
       // ColumnBound2.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
       // ColumnBound2.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
       // gvDatos.Columns.Add(ColumnBound2);

       
       // BoundField ColumnBound3 = new BoundField { };
       // ColumnBound3 = new BoundField();
       // ColumnBound3.HeaderText = "Valor  Microcredito";
       // ColumnBound3.DataField = "total_cartera_microcredito";
       // ColumnBound3.DataFormatString = "{0:n}";
       // ColumnBound3.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
       // ColumnBound3.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
       // gvDatos.Columns.Add(ColumnBound3);



       // BoundField ColumnBound4 = new BoundField { };
       // ColumnBound4 = new BoundField();
       // ColumnBound4.HeaderText = "Valor Consumo";
       // ColumnBound4.DataField = "total_cartera_consumo";
       // ColumnBound4.DataFormatString = "{0:n}";
       // ColumnBound4.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
       // ColumnBound4.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
       // gvDatos.Columns.Add(ColumnBound4);
        


       // BoundField ColumnBound5 = new BoundField { };
       // ColumnBound5 = new BoundField();
       // ColumnBound5.HeaderText = "Valor Comercial";
       // ColumnBound5.DataField = "total_cartera_comercial";
       // ColumnBound5.DataFormatString = "{0:n}";
       // ColumnBound5.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
       // ColumnBound5.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
       // gvDatos.Columns.Add(ColumnBound5);


       // BoundField ColumnBound6 = new BoundField { };
       // ColumnBound6 = new BoundField();
       // ColumnBound6.HeaderText = "Valor Vivienda";
       // ColumnBound6.DataField = "total_cartera_vivienda";
       // ColumnBound6.DataFormatString = "{0:n}";
       // ColumnBound6.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
       // ColumnBound6.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
       // gvDatos.Columns.Add(ColumnBound6);



       // //lenando columnas del GridView
       // List<CarteraBruta> LstDetalleComprobantec = new List<CarteraBruta>();

       // LstDetalleComprobantec = CarteraBrutaService.consultarCartera(fechaInicial, ddlFechaCorte.SelectedValue, (Usuario)Session["Usuario"]);

       //  foreach (CarteraBruta eCartera in LstDetalleComprobantec)
       // {
       //     DataRow dtFila = dtReporte.NewRow();
       //     dtFila[0] = eCartera.fecha_historico;
       //     if (eCartera.fecha_historico != null)
       //     {
       //         dtFila[1] = Convert.ToDecimal(eCartera.total_cartera);
       //         //dtFila[2] = eCartera.numero_cartera;
                
       //         dtFila[2] = eCartera.total_cartera_microcredito;
       //        // dtFila[4] = eCartera.numero_cartera_microcredito;
       //         dtFila[3] = eCartera.total_cartera_consumo;
       //      //   dtFil[6] = eCartera.numero_cartera_consumo;
       //         dtFila[4] = eCartera.total_cartera_comercial;
       //      //   dtFila[8] = eCartera.numero_cartera_comercial;
       //         dtFila[5] = eCartera.total_cartera_vivienda;
       //     //    dtFila[10] = eCartera.numero_cartera_vivienda;
       //         dtReporte.Rows.Add(dtFila);
       //     }
       // }
       // //llenando los datos totales
       // foreach (DataRow dFila in dtReporte.Rows)
       // {
       //     int i = 3;
       //     foreach (CarteraBruta eCartera in LstDetalleComprobantec)
       //     {
       //         if (eCartera.fecha_historico == dFila[0].ToString())
       //         {
       //             if (eCartera.fecha_historico != null)
       //             {
       //                 dFila[i] = eCartera.total_cartera;
       //                 dFila[i + 1] = eCartera.total_cartera_microcredito;
       //                 dFila[i + 2] = eCartera.total_cartera_consumo;
       //                 dFila[i + 3] = eCartera.total_cartera_comercial;
       //                 dFila[i + 4] = eCartera.total_cartera_vivienda;

       //             }
       //         }
       //     }
       // }

       // gvDatos.DataSource = dtReporte;
       // gvDatos.DataBind();
        //List<Xpinn.Indicadores.Entities.CarteraBruta> lstcartera = new List<Xpinn.Indicadores.Entities.CarteraBruta>();

        //// Generando las columnas del DATATABLE para poder generar la gráfica
        //dtReporte.Columns.Add("fecha_historico_tot", typeof(String)).AllowDBNull = true;
        //dtCantidad.Columns.Add("fecha_historico_tot", typeof(String)).AllowDBNull = true;
        //foreach (Xpinn.Cartera.Entities.ClasificacionCartera eCategoria in lstClasificacion)
        //{
        //    DataColumn dtColumna = new DataColumn();
        //    dtColumna.AllowDBNull = true;
        //    dtColumna.ColumnName = "Clasificación" + eCategoria.codigo;
        //    dtColumna.DataType = typeof(decimal);
        //    dtReporte.Columns.Add(dtColumna);
        //    DataColumn dtColumnaPor = new DataColumn();
        //    dtColumnaPor.AllowDBNull = true;
        //    dtColumnaPor.ColumnName = "Clasificación" + eCategoria.codigo;
        //    dtColumnaPor.DataType = typeof(decimal);
        //    dtCantidad.Columns.Add(dtColumnaPor);
        //}
        //dtReporte.Columns.Add("total", typeof(decimal)).AllowDBNull = true;
        //dtCantidad.Columns.Add("total", typeof(decimal)).AllowDBNull = true;
        //// Generando las columnas de la GRIDVIEW    
        //BoundField ColumnBound1 = new BoundField { };
        //ColumnBound1 = new BoundField();
        //ColumnBound1.HeaderText = "Fecha Historico";
        //ColumnBound1.DataField = "fecha_historico_tot";
        //ColumnBound1.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        //ColumnBound1.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        //gvDatos.Columns.Add(ColumnBound1);
        //gvCantidad.Columns.Add(ColumnBound1);
        //foreach (Xpinn.Cartera.Entities.ClasificacionCartera eCategoria in lstClasificacion)
        //{
        //    BoundField ColumnBound = new BoundField { };
        //    ColumnBound = new BoundField();
        //    ColumnBound.HeaderText = Convert.ToString(eCategoria.codigo);
        //    ColumnBound.DataField = "Clasificación" + (eCategoria.codigo);
        //    ColumnBound.DataFormatString = "{0:n}";
        //    ColumnBound.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        //    ColumnBound.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        //    gvDatos.Columns.Add(ColumnBound);
        //    gvCantidad.Columns.Add(ColumnBound);
        //}
        //BoundField ColumnBoundT = new BoundField { };
        //ColumnBoundT = new BoundField();
        //ColumnBoundT.HeaderText = "Total";
        //ColumnBoundT.DataField = "total";
        //ColumnBoundT.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        //ColumnBoundT.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        //gvDatos.Columns.Add(ColumnBoundT);
        //gvCantidad.Columns.Add(ColumnBoundT);

        //// Traer los datos según criterio seleccionado
        //List<CarteraBruta> LstIndicador = new List<CarteraBruta>();
        //LstIndicador = CarteraBrutaService.consultarCartera(fechaInicial, ddlFechaCorte.SelectedValue, (Usuario)Session["Usuario"]);

        //foreach (CarteraBruta eIndicador in LstIndicador)
        //{
        //    // Buscar la categoria en el DATATABLE
        //    int j = 0;
        //    bool bEncontro = false;
        //    foreach (DataRow dFila in dtReporte.Rows)
        //    {
        //        string fecha;
        //        fecha = dFila[0].ToString();
        //        if (eIndicador.fecha_historico.Trim() == fecha.Trim())
        //        {
        //            bEncontro = true;
        //            int i = 1;
        //            foreach (Xpinn.Cartera.Entities.ClasificacionCartera eCategoria in lstClasificacion)
        //            {
        //                if (eCategoria.codigo == eIndicador.)
        //                {
        //                    dFila[i] = eIndicador.total_cartera_comercial.ToString();
        //                    DataRow dFilaPor = dtCantidad.Rows[j];
        //                    dFilaPor[i] = eIndicador.numero_cartera_comercial.ToString();
        //                }
        //                i += 1;
        //            }
        //        }
        //        j += 1;
        //    }
        //    if (!bEncontro)
        //    {
        //        DataRow dFila = dtReporte.NewRow();
        //        dFila[0] = eIndicador.fecha_historico.Trim();
        //        DataRow dFilaPor = dtCantidad.NewRow();
        //        dFilaPor[0] = eIndicador.fecha_historico.Trim();
        //        int i = 1;
        //        foreach (Xpinn.Indicadores.Entities.CarteraBruta eCategoria  in lstcartera)
        //        {
        //            if (eCategoria.total_cartera == eIndicador.total_cartera)
        //            {
        //                dFila[i] = eIndicador.total_cartera_comercial.ToString();
        //                dFilaPor[i] = eIndicador.numero_cartera_comercial.ToString();
        //            }
        //            i += 1;
        //        }
        //        dtReporte.Rows.Add(dFila);
        //        dtCantidad.Rows.Add(dFilaPor);
        //    }
        //}
        //// Totalizar los datos
        //int contador = 0;
        //foreach (DataRow dFila in dtReporte.Rows)
        //{
        //    int i = 1;
        //    decimal total = 0;
        //    decimal totalPor = 0;
        //    DataRow dFilaPor = dtCantidad.Rows[contador];
        //    foreach (Xpinn.Indicadores.Entities.CarteraBruta eCategoria in lstcartera)
        //    {
        //        total += ConvertirStringToDecimal(dFila[i].ToString());
        //        totalPor += ConvertirStringToDecimal(dFilaPor[i].ToString());
        //        i += 1;
        //    }
        //    dFila[i] = total;
        //    dFilaPor[i] = totalPor;
        //    contador += 1;
        //}

        //gvDatos.DataSource = dtReporte;
        //gvDatos.DataBind();
        //gvCantidad.DataSource = dtCantidad;
        //gvCantidad.DataBind();


        // Haciendo visible las gráficas
        Chart1.Visible = true;
        Chart2.Visible = true;

      //  // Traer los datos según criterio seleccionado
        List<CarteraBruta> LstDetalleComprobantes = new List<CarteraBruta>();
        LstDetalleComprobantes = CarteraBrutaService.consultarCartera(fechaInicial, ddlFechaCorte.SelectedValue, (Usuario)Session["Usuario"]);

        // --------------------------------------------------------------------------------------------------------------------------------------------
        // Mostrar la primera gráfica
        // -------------------------------------------------------------------------------------------------------------------------------------------
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 100;
        // Determinar los títulos de la gráfica
        Chart1.Titles["Title1"].Text = "Evolución Cartera Bruta a  " + ddlFechaCorte.SelectedValue;
        Chart1.Titles["Title2"].Text = "($ millones)";

     
        // Determinar el tipo de gráfica
        Chart1.Series["Series1"].ChartType = TipoGrafica(ddlTipoGrafica1);
        Chart1.Series["Series2"].ChartType = TipoGrafica(ddlTipoGrafica1);
        Chart1.Series["Series3"].ChartType = TipoGrafica(ddlTipoGrafica1);
        Chart1.Series["Series4"].ChartType = TipoGrafica(ddlTipoGrafica1);
        Chart1.Series["Series5"].ChartType = TipoGrafica(ddlTipoGrafica1);
        // Cargar los datos a la gráfica
        Chart1.DataSource = LstDetalleComprobantes;
        //// Determinar el tipo de gráfica
        //if (ddlPeriodo.SelectedValue == "3")
        //{
        //    Chart1.Series["Series1"].ChartType = SeriesChartType.Column;
        //    Chart1.Series["Series2"].ChartType = SeriesChartType.Column;
        //    Chart1.Series["Series3"].ChartType = SeriesChartType.Column;
        //    Chart1.Series["Series4"].ChartType = SeriesChartType.Column;
        //    Chart1.Series["Series5"].ChartType = SeriesChartType.Column;
        //}
        //else
        //{
        //    Chart1.Series["Series1"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
        //    Chart1.Series["Series2"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
        //    Chart1.Series["Series3"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
        //    Chart1.Series["Series4"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
        //    Chart1.Series["Series5"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);

        //}
        // Mostrar los nombres de las seríes
        Chart1.Series["Series1"].LegendText = "Total";
        Chart1.Series["Series1"].YValueMembers = "total_cartera";
        Chart1.Series["Series1"].XValueMember = "fecha_historico";
        Chart1.Series["Series1"].LabelMapAreaAttributes = "total_cartera";
        Chart1.Series["Series1"].IsValueShownAsLabel = true;
        Chart1.Series["Series1"]["LabelStyle"] = "Top";
        Chart1.Series["Series1"].LabelFormat = "{0:N0}";
       

        Chart1.Series["Series2"].LegendText = lstProductos[3].descripcion; 
        Chart1.Series["Series2"].YValueMembers = "total_cartera_microcredito";
        Chart1.Series["Series2"].XValueMember = "fecha_historico";
        Chart1.Series["Series2"].LabelMapAreaAttributes = "total_cartera_microcredito";
        Chart1.Series["Series2"].IsValueShownAsLabel = true;
        Chart1.Series["Series2"]["LabelStyle"] = "Top";
        Chart1.Series["Series2"].LabelFormat = "{0:N0}";

        Chart1.Series["Series3"].LegendText = lstProductos[0].descripcion;
        Chart1.Series["Series3"].YValueMembers = "total_cartera_consumo";
        Chart1.Series["Series3"].XValueMember = "fecha_historico";
        Chart1.Series["Series3"].LabelMapAreaAttributes = "total_cartera_consumo";
        Chart1.Series["Series3"].IsValueShownAsLabel = true;
        Chart1.Series["Series3"]["LabelStyle"] = "Top";
        Chart1.Series["Series3"].LabelFormat = "{0:N0}";

        Chart1.Series["Series4"].LegendText = lstProductos[1].descripcion;
        Chart1.Series["Series4"].YValueMembers = "total_cartera_comercial";
        Chart1.Series["Series4"].XValueMember = "fecha_historico";
        Chart1.Series["Series4"].LabelMapAreaAttributes = "total_cartera_comercial";
        Chart1.Series["Series4"].IsValueShownAsLabel = true;
        Chart1.Series["Series4"]["LabelStyle"] = "Top";
        Chart1.Series["Series4"].LabelFormat = "{0:N0}";

        Chart1.Series["Series5"].LegendText = lstProductos[2].descripcion;
        Chart1.Series["Series5"].YValueMembers = "total_cartera_vivienda";
        Chart1.Series["Series5"].XValueMember = "fecha_historico";
        Chart1.Series["Series5"].LabelMapAreaAttributes = "total_cartera_vivienda";
        Chart1.Series["Series5"].IsValueShownAsLabel = true;
        Chart1.Series["Series5"]["LabelStyle"] = "Top";
        Chart1.Series["Series5"].LabelFormat = "{0:N0}";

        Chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;

        // Mostrar la gráfica en pantalla
        
            Chart1.DataBind();

        //Color de fondo y de Barras
        if (Session["ColorFondo"] !=null)
        {
        
            Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml(Session["ColorFondo"].ToString());
            Chart2.BackColor = System.Drawing.ColorTranslator.FromHtml(Session["ColorFondo"].ToString());
        }
            if (Session["ColorTotal"]!=null)
        {
            Chart1.Series["Series1"].Color = System.Drawing.ColorTranslator.FromHtml(Session["ColorTotal"].ToString());
            Chart2.Series["Series1"].Color = System.Drawing.ColorTranslator.FromHtml(Session["ColorTotal"].ToString());
            Chart1.Series["Series1"].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Session["ColorTotal"].ToString());
            Chart2.Series["Series1"].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Session["ColorTotal"].ToString());
            Chart1.Series["Series1"].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(Session["ColorTotal"].ToString());
            Chart2.Series["Series1"].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(Session["ColorTotal"].ToString());

        }
        if (Session["ColorComercial"]!=null)
        {
            Chart1.Series["Series2"].Color = System.Drawing.ColorTranslator.FromHtml(Session["ColorComercial"].ToString());
            Chart2.Series["Series2"].Color = System.Drawing.ColorTranslator.FromHtml(Session["ColorComercial"].ToString());
            Chart1.Series["Series2"].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Session["ColorComercial"].ToString());
            Chart2.Series["Series2"].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Session["ColorComercial"].ToString());
            Chart1.Series["Series2"].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(Session["ColorComercial"].ToString());
            Chart2.Series["Series2"].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(Session["ColorComercial"].ToString());

        }
        if (Session["ColorConsumo"] != null)
        {
            Chart1.Series["Series3"].Color = System.Drawing.ColorTranslator.FromHtml(Session["ColorConsumo"].ToString());
            Chart2.Series["Series3"].Color = System.Drawing.ColorTranslator.FromHtml(Session["ColorConsumo"].ToString());
            Chart1.Series["Series3"].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Session["ColorConsumo"].ToString());
            Chart2.Series["Series3"].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Session["ColorConsumo"].ToString());
            Chart1.Series["Series3"].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(Session["ColorConsumo"].ToString());
            Chart2.Series["Series3"].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(Session["ColorConsumo"].ToString());

        }
        if (Session["ColorVivienda"] != null)
        {
            Chart1.Series["Series4"].Color = System.Drawing.ColorTranslator.FromHtml(Session["ColorVivienda"].ToString());
            Chart2.Series["Series4"].Color = System.Drawing.ColorTranslator.FromHtml(Session["ColorVivienda"].ToString());
            Chart1.Series["Series4"].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Session["ColorVivienda"].ToString());
            Chart2.Series["Series4"].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Session["ColorVivienda"].ToString());
            Chart1.Series["Series4"].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(Session["ColorVivienda"].ToString());
            Chart2.Series["Series4"].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(Session["ColorVivienda"].ToString());

        }
        if (Session["ColorMicroCredito"] != null)
        {
            Chart1.Series["Series5"].Color = System.Drawing.ColorTranslator.FromHtml(Session["ColorMicroCredito"].ToString());
            Chart2.Series["Series5"].Color = System.Drawing.ColorTranslator.FromHtml(Session["ColorMicroCredito"].ToString());
            Chart1.Series["Series5"].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Session["ColorMicroCredito"].ToString());
            Chart2.Series["Series5"].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Session["ColorMicroCredito"].ToString());
            Chart1.Series["Series5"].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(Session["ColorMicroCredito"].ToString());
            Chart2.Series["Series5"].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(Session["ColorMicroCredito"].ToString());

        }
        //

        //Tipo de Letra
        Chart1.Titles["Title1"].Font = new System.Drawing.Font(ddlFuentes.SelectedItem.Text, 12);
        Chart1.Titles["Title2"].Font = new System.Drawing.Font(ddlFuentes.SelectedItem.Text, 12);
        Chart1.Series["Series1"].Font = new System.Drawing.Font(ddlFuentes.SelectedItem.Text, 7);
        Chart1.Series["Series2"].Font = new System.Drawing.Font(ddlFuentes.SelectedItem.Text, 7);
        Chart1.Series["Series3"].Font = new System.Drawing.Font(ddlFuentes.SelectedItem.Text, 7);
        Chart1.Series["Series4"].Font = new System.Drawing.Font(ddlFuentes.SelectedItem.Text, 7);
        Chart1.Series["Series5"].Font = new System.Drawing.Font(ddlFuentes.SelectedItem.Text, 7);


        Chart2.Titles["Title1"].Font = new System.Drawing.Font(ddlFuentes.SelectedItem.Text, 12);
        Chart2.Titles["Title2"].Font = new System.Drawing.Font(ddlFuentes.SelectedItem.Text, 12);
        Chart2.Series["Series1"].Font = new System.Drawing.Font(ddlFuentes.SelectedItem.Text, 7);
        Chart2.Series["Series2"].Font = new System.Drawing.Font(ddlFuentes.SelectedItem.Text, 7);
        Chart2.Series["Series3"].Font = new System.Drawing.Font(ddlFuentes.SelectedItem.Text, 7);
        Chart2.Series["Series4"].Font = new System.Drawing.Font(ddlFuentes.SelectedItem.Text, 7);
        Chart2.Series["Series5"].Font = new System.Drawing.Font(ddlFuentes.SelectedItem.Text, 7);

        //

        //Nombrar Checkbox
        chk1.Text = lstProductos[3].descripcion;
        chk2.Text = lstProductos[0].descripcion;
        chk3.Text = lstProductos[1].descripcion;
        chk4.Text = lstProductos[2].descripcion;
        //

        // Mostrar la gráfica en pantalla
        // Traer los dato según criterio seleccionado
        List <CarteraBruta> LstDetalleComprobantec = new List<CarteraBruta>();
        LstDetalleComprobantec = CarteraBrutaService.consultarCartera(fechaInicial, ddlFechaCorte.SelectedValue, (Usuario)Session["Usuario"]);
        gvDatos.DataSource = LstDetalleComprobantec;
        gvDatos.DataBind();
        // --------------------------------------------------------------------------------------------------------------------------------------------
        // Mostrar la segunda gráfica    
        // -------------------------------------------------------------------------------------------------------------------------------------------
        Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        Chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1000;
        // Determinar el tipo de gráfica
        Chart2.Series["Series1"].ChartType = TipoGrafica(ddlTipoGrafica2);
        Chart2.Series["Series2"].ChartType = TipoGrafica(ddlTipoGrafica2);
        Chart2.Series["Series3"].ChartType = TipoGrafica(ddlTipoGrafica2);
        Chart2.Series["Series4"].ChartType = TipoGrafica(ddlTipoGrafica2);
        Chart2.Series["Series5"].ChartType = TipoGrafica(ddlTipoGrafica2);
        // Determinar los títulos de la gráfica
        Chart2.Titles["Title1"].Text = "Evolución Cartera Bruta " + ddlFechaCorte.SelectedValue;
        Chart2.Titles["Title2"].Text = "(# Número)";
        // Cargar los datos a la gráfica
        Chart2.DataSource = LstDetalleComprobantes;
        //// Determinar el tipo de gráfica
        //if (ddlPeriodo.SelectedValue == "3")
        //{
        //    Chart2.Series["Series1"].ChartType = SeriesChartType.Column;
        //    Chart2.Series["Series2"].ChartType = SeriesChartType.Column;
        //    Chart2.Series["Series3"].ChartType = SeriesChartType.Column;
        //    Chart2.Series["Series4"].ChartType = SeriesChartType.Column;
        //    Chart2.Series["Series5"].ChartType = SeriesChartType.Column;
        //}
        //else
        //{
        //    Chart2.Series["Series1"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
        //    Chart2.Series["Series2"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
        //    Chart2.Series["Series3"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
        //    Chart2.Series["Series4"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
        //    Chart2.Series["Series5"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);

        //}
        // Mostrar los nombres de las seríes
        Chart2.Series["Series1"].LegendText = "Total";
        Chart2.Series["Series1"].LabelMapAreaAttributes = "numero_cartera";
        Chart2.Series["Series1"].IsValueShownAsLabel = true;
        Chart2.Series["Series1"].YValueMembers = "numero_cartera";
        Chart2.Series["Series1"].XValueMember = "fecha_historico";

        Chart2.Series["Series2"].LegendText = lstProductos[3].descripcion; 
        Chart2.Series["Series2"].YValueMembers = "numero_cartera_microcredito";
        Chart2.Series["Series2"].XValueMember = "fecha_historico";
        Chart2.Series["Series2"].LabelMapAreaAttributes = "numero_cartera_microcredito";
        Chart2.Series["Series2"].IsValueShownAsLabel = true;
        Chart2.Series["Series2"]["LabelStyle"] = "Top";

        Chart2.Series["Series3"].LegendText = lstProductos[0].descripcion;
        Chart2.Series["Series3"].YValueMembers = "numero_cartera_consumo";
        Chart2.Series["Series3"].XValueMember = "fecha_historico";
        Chart2.Series["Series3"].LabelMapAreaAttributes = "numero_cartera_consumo";
        Chart2.Series["Series3"].IsValueShownAsLabel = true;
        Chart2.Series["Series3"]["LabelStyle"] = "Top";


        Chart2.Series["Series4"].LegendText = lstProductos[1].descripcion;
        Chart2.Series["Series4"].YValueMembers = "numero_cartera_comercial";
        Chart2.Series["Series4"].XValueMember = "fecha_historico";
        Chart2.Series["Series4"].LabelMapAreaAttributes = "numero_cartera_comercial";
        Chart2.Series["Series4"].IsValueShownAsLabel = true;
        Chart2.Series["Series4"]["LabelStyle"] = "Top";

        Chart2.Series["Series5"].LegendText = lstProductos[2].descripcion;
        Chart2.Series["Series5"].YValueMembers = "numero_cartera_vivienda";
        Chart2.Series["Series5"].XValueMember = "fecha_historico";
        Chart2.Series["Series5"].LabelMapAreaAttributes = "numero_cartera_vivienda";
        Chart2.Series["Series5"].IsValueShownAsLabel = true;
        Chart2.Series["Series5"]["LabelStyle"] = "Top";

        // Refrescar datos de la gráfica en pantalla
        Chart2.DataBind();
        List<CarteraBruta> LstDetalleComprobantecan = new List<CarteraBruta>();
        LstDetalleComprobantecan = CarteraBrutaService.consultarCartera(fechaInicial, ddlFechaCorte.SelectedValue, (Usuario)Session["Usuario"]);
        gvCantidad.DataSource = LstDetalleComprobantecan;
        gvCantidad.DataBind();
            
    }

    protected void ddlTipoGrafica1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Generar();
    }

    protected void ddlTipoGrafica2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Generar();
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

        sw = expGrilla.ObtenerGrilla(gvCantidad, null);

        Response.Write("<div>" + expGrilla.style + "</div>");
        Response.Output.Write("<div>" + sw.ToString() + "</div>");
        Response.Flush();


        string tmpChartName = "grafica2.jpg";
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
        Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        Chart1.ChartAreas["ChartArea2"].AxisX.Interval = 1;
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
        if (chkFondo.Checked)
        {
            Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart2.BackColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Session["ColorFondo"] = txtColorFondo.Text;
        }
        if (chkTotal.Checked)
        {
            Chart1.Series["Series1"].Color = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart2.Series["Series1"].Color = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart1.Series["Series1"].MarkerColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart2.Series["Series1"].MarkerColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart1.Series["Series1"].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart2.Series["Series1"].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Session["ColorTotal"] = txtColorFondo.Text;
           

        }
        if (chk1.Checked)
        {
            Chart1.Series["Series2"].Color = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart2.Series["Series2"].Color = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart1.Series["Series2"].MarkerColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart2.Series["Series2"].MarkerColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart1.Series["Series2"].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart2.Series["Series2"].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Session["ColorComercial"] =txtColorFondo.Text;

        }
        if (chk2.Checked)
        {
            Chart1.Series["Series3"].Color = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart2.Series["Series3"].Color = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart1.Series["Series3"].MarkerColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart2.Series["Series3"].MarkerColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart1.Series["Series3"].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart2.Series["Series3"].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Session["ColorConsumo"] =txtColorFondo.Text;
        }
        if (chk3.Checked)
        {
            Chart1.Series["Series4"].Color = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart2.Series["Series4"].Color = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart1.Series["Series4"].MarkerColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart2.Series["Series4"].MarkerColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart1.Series["Series4"].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart2.Series["Series4"].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Session["ColorVivienda"] = txtColorFondo.Text;
        }
        if (chk4.Checked)
        {
            Chart1.Series["Series5"].Color = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart2.Series["Series5"].Color = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart1.Series["Series5"].MarkerColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart2.Series["Series5"].MarkerColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart1.Series["Series5"].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Chart2.Series["Series5"].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(txtColorFondo.Text);
            Session["ColorMicroCredito"] =txtColorFondo.Text;
        }


        btnInforme_Click(null, null);
      
       
    }

    protected void Chart1_Load(object sender, EventArgs e)
    {
       
    }

    protected void ddlFuentes_SelectedIndexChanged(object sender, EventArgs e)
    {
       

        btnInforme_Click(null, null);
    }
}
