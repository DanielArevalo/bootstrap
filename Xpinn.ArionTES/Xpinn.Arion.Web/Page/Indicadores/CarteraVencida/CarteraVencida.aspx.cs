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
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Indicadores.Services.CarteraVencidaService CarteraVencidaService = new Xpinn.Indicadores.Services.CarteraVencidaService();
    private LineasCreditoService LineasCreditoService = new LineasCreditoService();
    List<Xpinn.Cartera.Entities.ClasificacionCartera> lstCategoria = new List<Xpinn.Cartera.Entities.ClasificacionCartera>();
    Xpinn.Cartera.Services.ClasificacionCarteraService BOCartera = new Xpinn.Cartera.Services.ClasificacionCarteraService();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[CarteraVencidaService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(CarteraVencidaService.CodigoPrograma, "E");
            else
                VisualizarOpciones(CarteraVencidaService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            txtColorFondo.eventoCambiar += txtColorFondo_TextChanged;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CarteraVencidaService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session[CarteraVencidaService.CodigoPrograma + ".id"] != null)
        {
            Usuario usuap = (Usuario)Session["usuario"];
        }
        if (!IsPostBack)
        {
            ucFechaInicial.ToDateTime = new DateTime(DateTime.Today.Year, 1, 1);
            ucFechaFinal.ToDateTime = DateTime.Today;

            cargardrop();
            rblFiltro2_SelectedIndexChanged(rblFiltroCategoria, null);
            chkMostrarPorcentaje_CheckedChanged(chkMostrarPorcentaje, null);
            ddlTipoGrafica1.SelectedValue = "1";
            ddlTipoGrafica2.SelectedValue = "1";
            ucFechaInicial.Visible = true;
            ucFechaFinal.Visible = true;
            ch3D.Checked = false;
            btnInforme_Click(null, null);
        }
    }

    public String vacios(String texto)
    {
        if (String.IsNullOrEmpty(texto))
        {
            return " ";
        }
        else
        {
            return texto;
        }
    }

    protected void cargardrop()
    {
        Xpinn.FabricaCreditos.Services.OficinaService linahorroServicio = new Xpinn.FabricaCreditos.Services.OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina linahorroVista = new Xpinn.FabricaCreditos.Entities.Oficina();
        ddloficina.DataTextField = "nombre";
        ddloficina.DataValueField = "codigo";
        ddloficina.DataSource = linahorroServicio.ListarOficinas(linahorroVista, (Usuario)Session["usuario"]);
        ddloficina.DataBind();
    }

    protected string obtfiltro()
    {
        string filtrando = "";
        ConnectionDataBase conexion = new ConnectionDataBase();
        VerError(rblFiltroCategoria.SelectedValue);
        if (rblFiltroCategoria.SelectedValue == "V")
        {
            if (conexion.TipoConexion() == "ORACLE")
                filtrando += " AND V.FECHA_HISTORICO BETWEEN TO_DATE('" + ucFechaInicial.ToDate + "','" + gFormatoFecha + "') AND TO_DATE('" + ucFechaFinal.ToDate + "','" + gFormatoFecha + "') ";
            else
                filtrando += " AND V.FECHA_HISTORICO BETWEEN '" + ucFechaInicial.ToDate + "' AND '" + ucFechaFinal.ToDate + "' ";
            if (ddloficina.SelectedText != "")
                filtrando += " AND V.COD_OFICINA in( " + ddloficina.SelectedValue + ")";
        }
        else if (rblFiltroCategoria.SelectedValue == "C")
        {
            if (conexion.TipoConexion() == "ORACLE")
                filtrando += " AND H.FECHA_HISTORICO BETWEEN TO_DATE('" + ucFechaInicial.ToDate + "','" + gFormatoFecha + "') AND TO_DATE('" + ucFechaFinal.ToDate + "','" + gFormatoFecha + "') AND H.COD_CATEGORIA_CLI != 'A' ";
            else
                filtrando += " AND H.FECHA_HISTORICO BETWEEN '" + ucFechaInicial.ToDate + "' AND '" + ucFechaFinal.ToDate + "' AND H.COD_CATEGORIA_CLI != 'A' ";
            if (ddlVencCateg.SelectedIndex != 0)
                if (ddlVencCateg.SelectedValue != "T")
                    filtrando += " AND H.COD_CATEGORIA_CLI In ('" + ddlVencCateg.SelectedValue + "')";
            if (ddloficina.SelectedText != "")
                filtrando += " AND H.COD_OFICINA in( " + ddloficina.SelectedValue + ")";
        }
        else
        {
            if (conexion.TipoConexion() == "ORACLE")
                filtrando += " AND FECHA_HISTORICO BETWEEN TO_DATE('" + ucFechaInicial.ToDate + "','" + gFormatoFecha + "') AND TO_DATE('" + ucFechaFinal.ToDate + "','" + gFormatoFecha + "')";
            else
                filtrando += " AND FECHA_HISTORICO BETWEEN '" + ucFechaInicial.ToDate + "' AND '" + ucFechaFinal.ToDate + "'";
            if (ddlVencCateg.SelectedIndex != 0)
                if (dllineas.SelectedValue != "")
                    filtrando += " AND COD_LINEA_CREDITO in (" + dllineas.SelectedValue + ")";
            if (ddloficina.SelectedText != "")
                filtrando += " AND COD_LINEA_CREDITO in (" + ddloficina.SelectedValue + ")";
            if (drop_periocidadmora.SelectedValue != null)
            {
                switch (drop_periocidadmora.SelectedValue)
                {
                    case "1":
                        filtrando += " AND RANGO_MORA = 30 ";
                        break;
                    case "2":
                        filtrando += " AND RANGO_MORA = 60 ";
                        break;
                    case "3":
                        filtrando += " AND RANGO_MORA = 90 ";
                        break;
                    case "4":
                        filtrando += " AND RANGO_MORA = 120 ";
                        break;
                    case "5":
                        filtrando += " AND RANGO_MORA = 180";
                        break;
                    case "6":
                        filtrando += " AND RANGO_MORA = 360";
                        break;
                }
            }
        }

        return filtrando;
    }

    void CargarDatos(string filtro)
    {
        Configuracion conf = new Configuracion();

        //mostrar charts 
        Chart1.Visible = true;
        Chart2.Visible = true;
        // Listado de categorias
        CarteraVencidaService BOCartera = new CarteraVencidaService();
        List<CarteraVencida> lstCategoria = new List<CarteraVencida>();
        lstCategoria = BOCartera.ConsultarCarteraVencidaLinea(filtro,
            ucFechaInicial.ToDateTime.ToString(conf.ObtenerFormatoFecha()),
            ucFechaFinal.ToDateTime.ToString(conf.ObtenerFormatoFecha()), (Usuario)Session["usuario"]);
        Random r = new Random();
        int number = 0;
        int numero = 0;
        List<Color> lstcolor = new List<Color>();
        //------------------------------------------------------
        //            Limpiando datos
        //--------------------------------------------
        DataTable dtReporte = new DataTable();
        dtReporte.Clear();
        gvDatos.Columns.Clear();
        DataTable dtPorcentaje = new DataTable();
        dtPorcentaje.Clear();
        gvPorcentaje.Columns.Clear();
        Chart1.Series.Clear();
        //------------------------
        //CARGAR DATOS 
        //------------------------

        if (lstCategoria.Count > 0)
        {
            foreach (var item in lstCategoria.OrderBy(x => x.cod_linea_credito).ToList())
            {

                Color color = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));

                lstcolor.Add(color);
                //------------------------------------------------------------------------------------------------------------------------------------------------------------
                // Generando la gráfica de indicador de cartera
                //------------------------------------------------------------------------------------------------------------------------------------------------------------
                //agregar las series que sean necesarias
                var name = "Serie" + number;
                Chart1.Series.Add(new Series(name));
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
                // Determinando que sea gráfica por líneas
                Chart1.Series[name].ChartType = SeriesChartType.StackedColumn;
                // Determinar el intervalo
                Chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;
                Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                // Determinar los títulos
                Chart1.Titles["Title1"].Text = "Evolución Saldo de Cartera Vencida Total";

                Chart1.Titles["Title2"].Text = ucFechaInicial.ToDateTime.ToShortDateString() + "al " +
                                               ucFechaFinal.ToDateTime.ToShortDateString() + " (Valores en Millones)";
                // Determinar los nombres de las series        
                Chart1.Series[name].Font = new Font("Microsoft Sans Serif", 8);
                Chart1.Series[name].MarkerSize = 7;
                Chart1.Series[name].MarkerColor = color;
                Chart1.Series[name]["DrawingStyle"] = "Emboss";

                if (number < 0) Chart1.Series[name].LegendText = item.cod_linea_credito;
                var names = "Serie" + (numero - 1);
                // Cargando datos a la gráfica 
                if (number > 0)
                {
                    if (Chart1.Series[name].LegendText == Chart1.Series[names].LegendText)
                    {
                        number++;
                        numero++;
                        continue;
                    }
                    Chart1.Series[name].LegendText = item.cod_linea_credito;
                    Chart1.Series[name].IsValueShownAsLabel = true;
                    Chart1.Series[name].ChartType = TipoGrafica(ddlTipoGrafica1);
                    Chart1.Series[name].Points.AddXY(item.fecha_historico_tot, item.valor_vencido_tot);
                    Chart1.Series[name].Color = color;
                }
                else
                {
                    Chart1.Series[name].LegendText = item.cod_linea_credito;
                    Chart1.Series[name].IsValueShownAsLabel = true;
                    Chart1.Series[name].ChartType = TipoGrafica(ddlTipoGrafica1);
                    Chart1.Series[name].Points.AddXY(item.fecha_historico_tot, item.valor_vencido_tot);
                    Chart1.Series[name].Color = color;
                }
                number++;
                numero++;
            }

            //--------------------------------------------------------------
            //---------creacion tabla 2 
            //----------------------------------
            number = 0;
            numero = 0;
            foreach (var item in lstCategoria.OrderBy(x => x.cod_linea_credito).ToList())
            {

                Color color = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));

                lstcolor.Add(color);
                //------------------------------------------------------------------------------------------------------------------------------------------------------------
                // Generando la gráfica de indicador de cartera
                //------------------------------------------------------------------------------------------------------------------------------------------------------------
                //agregar las series que sean necesarias
                var name = "Serie" + number;
                Chart2.Series.Add(new Series(name));
                Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
                // Determinando que sea gráfica por líneas
                Chart2.Series[name].ChartType = SeriesChartType.StackedColumn;
                // Determinar el intervalo
                Chart2.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;
                Chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                // Determinar los títulos
                Chart2.Titles["Title1"].Text = "Evolución Saldo de Cartera Vencida Total";

                Chart2.Titles["Title2"].Text = ucFechaInicial.ToDateTime.ToShortDateString() + "al " +
                                               ucFechaFinal.ToDateTime.ToShortDateString() + " (Valores en Porcentaje)";
                // Determinar los nombres de las series        
                Chart2.Series[name].Font = new Font("Microsoft Sans Serif", 8);
                Chart2.Series[name].MarkerSize = 7;
                Chart2.Series[name].MarkerColor = color;
                Chart2.Series[name]["DrawingStyle"] = "Emboss";

                if (number < 0) Chart2.Series[name].LegendText = item.cod_linea_credito;
                var names = "Serie" + (numero - 1);
                // Cargando datos a la gráfica 
                if (number > 0)
                {
                    if (Chart2.Series[name].LegendText == Chart2.Series[names].LegendText)
                    {
                        number++;
                        numero++;
                        continue;
                    }
                    Chart2.Series[name].LegendText = item.cod_linea_credito;
                    Chart2.Series[name].IsValueShownAsLabel = true;
                    Chart2.Series[name].ChartType = TipoGrafica(ddlTipoGrafica1);
                    Chart2.Series[name].Points.AddXY(item.fecha_historico_tot, item.porcentaje_vencido_tot);
                    Chart2.Series[name].Color = color;
                }
                else
                {
                    Chart2.Series[name].LegendText = item.cod_linea_credito;
                    Chart2.Series[name].IsValueShownAsLabel = true;
                    Chart2.Series[name].ChartType = TipoGrafica(ddlTipoGrafica1);
                    Chart2.Series[name].Points.AddXY(item.fecha_historico_tot, item.porcentaje_vencido_tot);
                    Chart2.Series[name].Color = color;
                }
                number++;
                numero++;
            }

            BoundField ColumnBound1 = new BoundField { };
            ColumnBound1 = new BoundField();
            ColumnBound1.HeaderText = "Fecha Historico";
            ColumnBound1.DataField = "fecha_historico_tot";
            ColumnBound1.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            ColumnBound1.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDatos.Columns.Add(ColumnBound1);

            BoundField ColumnBound2 = new BoundField { };
            ColumnBound2 = new BoundField();
            ColumnBound2.HeaderText = "Linea Credito";
            ColumnBound2.DataField = "Cod_linea_credito";
            ColumnBound2.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            ColumnBound2.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDatos.Columns.Add(ColumnBound2);

            BoundField ColumnBound4 = new BoundField { };
            ColumnBound4 = new BoundField();
            ColumnBound4.HeaderText = "Nombre Linea Credito";
            ColumnBound4.DataField = "descripcion";
            ColumnBound4.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            ColumnBound4.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDatos.Columns.Add(ColumnBound4);

            BoundField ColumnBound3 = new BoundField { };
            ColumnBound3 = new BoundField();
            ColumnBound3.HeaderText = "Total Saldo";
            ColumnBound3.DataField = "valor_vencido_tot";
            ColumnBound3.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            ColumnBound3.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            gvDatos.Columns.Add(ColumnBound3);

            BoundField pocentaje1 = new BoundField { };
            pocentaje1 = new BoundField();
            pocentaje1.HeaderText = "Fecha Historico";
            pocentaje1.DataField = "fecha_historico_tot";
            pocentaje1.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            pocentaje1.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            gvPorcentaje.Columns.Add(pocentaje1);

            BoundField pocentaje2 = new BoundField { };
            pocentaje2 = new BoundField();
            pocentaje2.HeaderText = "Linea Credito";
            pocentaje2.DataField = "Cod_linea_credito";
            pocentaje2.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            pocentaje2.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            gvPorcentaje.Columns.Add(pocentaje2);

            BoundField pocentaje4 = new BoundField { };
            pocentaje4 = new BoundField();
            pocentaje4.HeaderText = "Nombre Linea Credito";
            pocentaje4.DataField = "descripcion";
            pocentaje4.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            pocentaje4.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            gvPorcentaje.Columns.Add(pocentaje4);


            BoundField pocentaje3 = new BoundField { };
            pocentaje3 = new BoundField();
            pocentaje3.HeaderText = "Total Saldo";
            pocentaje3.DataField = "porcentaje_vencido_tot";
            pocentaje3.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            pocentaje3.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            gvPorcentaje.Columns.Add(pocentaje3);


            gvPorcentaje.DataSource = lstCategoria;
            gvPorcentaje.DataBind();
            gvDatos.DataSource = lstCategoria;
            gvDatos.DataBind();

        }
    }

    protected void btnInforme_Click(object sender, EventArgs e)
    {

        Configuracion conf = new Configuracion();
        //validando los datos
        if (ucFechaInicial.Text == "")
        {
            VerError("Seleccione la fecha Inicial");
            return;
        }
        if (ucFechaFinal.Text == "")
        {
            VerError("Seleccione la fecha Final");
            return;
        }
        if (ucFechaFinal.ToDateTime < ucFechaInicial.ToDateTime)
        {
            VerError("Error al ingresar el rango de fechas. verifique los datos");
            return;
        }

        string filtro = obtfiltro();
        if (rblFiltroCategoria.SelectedValue == "L")
        {
            rblFiltro2_SelectedIndexChanged(null, null);

            CargarDatos(filtro);
        }
        else
        {


            // Listado de categorias

            lstCategoria = BOCartera.ListarCategoriasVencidas((Usuario)Session["usuario"]);

            //Limpiando datos
            DataTable dtReporte = new DataTable();
            dtReporte.Clear();
            gvDatos.Columns.Clear();
            DataTable dtPorcentaje = new DataTable();
            dtPorcentaje.Clear();
            gvPorcentaje.Columns.Clear();

            int seleccion = 0;
            if (rblFiltroCategoria.SelectedIndex == 0)
            {
                if (ddlVencCateg.SelectedValue == "1")
                    seleccion = 30;
                if (ddlVencCateg.SelectedValue == "2")
                    seleccion = 60;
                if (ddlVencCateg.SelectedValue == "3")
                    seleccion = 90;
                if (ddlVencCateg.SelectedValue == "4")
                    seleccion = 120;
                if (ddlVencCateg.SelectedValue == "5")
                    seleccion = 180;
                if (ddlVencCateg.SelectedValue == "6")
                    seleccion = 360;
            }

            List<CarteraVencida> LstDetalleComprobante = new List<CarteraVencida>();
            if (rblFiltroCategoria.SelectedValue == "V")
            {
                dtReporte.Columns.Add("fecha_historico_tot", typeof(String)).AllowDBNull = true;
                dtReporte.Columns.Add("valor_vencido", typeof(decimal)).AllowDBNull = true;
                dtReporte.Columns.Add("porcentaje_vencido", typeof(decimal)).AllowDBNull = true;
                dtReporte.Columns.Add("valor_vencido_tot", typeof(decimal)).AllowDBNull = true;
                dtReporte.Columns.Add("porcentaje_vencido_tot", typeof(decimal)).AllowDBNull = true;

                BoundField ColumnBound1 = new BoundField { };
                ColumnBound1 = new BoundField();
                ColumnBound1.HeaderText = "Fecha Historico";
                ColumnBound1.DataField = "fecha_historico_tot";
                ColumnBound1.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                ColumnBound1.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                gvDatos.Columns.Add(ColumnBound1);

                BoundField ColumnBound2 = new BoundField { };
                ColumnBound2 = new BoundField();
                ColumnBound2.HeaderText = "Valor Vencido";
                ColumnBound2.DataField = "valor_vencido";
                ColumnBound2.DataFormatString = "{0:n}";
                ColumnBound2.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                ColumnBound2.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                gvDatos.Columns.Add(ColumnBound2);

                BoundField ColumnBound3 = new BoundField { };
                ColumnBound3 = new BoundField();
                ColumnBound3.HeaderText = "Porcentaje";
                ColumnBound3.DataField = "porcentaje_vencido";
                ColumnBound3.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                ColumnBound3.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                gvDatos.Columns.Add(ColumnBound3);

                BoundField ColumnBound4 = new BoundField { };
                ColumnBound4 = new BoundField();
                ColumnBound4.HeaderText = "Valor Vencido Total";
                ColumnBound4.DataField = "valor_vencido_tot";
                ColumnBound4.DataFormatString = "{0:n}";
                ColumnBound4.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                ColumnBound4.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                gvDatos.Columns.Add(ColumnBound4);

                BoundField ColumnBound5 = new BoundField { };
                ColumnBound5 = new BoundField();
                ColumnBound5.HeaderText = "Porcentaje total";
                ColumnBound5.DataField = "porcentaje_vencido_tot";
                ColumnBound5.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                ColumnBound5.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                gvDatos.Columns.Add(ColumnBound5);

                //lenando columnas del GridView
                LstDetalleComprobante = CarteraVencidaService.consultarCarteraVencida(filtro, ucFechaInicial.ToDateTime.ToString(conf.ObtenerFormatoFecha()), ucFechaFinal.ToDateTime.ToString(conf.ObtenerFormatoFecha()), seleccion, (Usuario)Session["Usuario"]);
                foreach (CarteraVencida eCartera in LstDetalleComprobante)
                {
                    DataRow dtFila = dtReporte.NewRow();
                    dtFila[0] = eCartera.fecha_historico;
                    if (eCartera.fecha_historico != null)
                    {
                        dtFila[1] = Convert.ToDecimal(eCartera.valor_vencido);
                        dtFila[2] = eCartera.porcentaje_vencido;
                        dtReporte.Rows.Add(dtFila);
                    }
                }
                //llenando los datos totales
                foreach (DataRow dFila in dtReporte.Rows)
                {
                    int i = 3;
                    foreach (CarteraVencida eCartera in LstDetalleComprobante)
                    {
                        if (eCartera.fecha_historico_tot == dFila[0].ToString())
                        {
                            if (eCartera.fecha_historico_tot != null)
                            {
                                dFila[i] = eCartera.valor_vencido_tot;
                                dFila[i + 1] = eCartera.porcentaje_vencido_tot;
                            }
                        }
                    }
                }

                gvDatos.DataSource = dtReporte;
                gvDatos.DataBind();
            }
            else
            {
                // Generando las columnas del DATATABLE para poder generar la gráfica
                dtReporte.Columns.Add("fecha_historico_tot", typeof(String)).AllowDBNull = true;
                dtPorcentaje.Columns.Add("fecha_historico_tot", typeof(String)).AllowDBNull = true;
                foreach (Xpinn.Cartera.Entities.ClasificacionCartera eCategoria in lstCategoria)
                {
                    DataColumn dtColumna = new DataColumn();
                    dtColumna.AllowDBNull = true;
                    dtColumna.ColumnName = "categoria" + eCategoria.categoria;
                    dtColumna.DataType = typeof(decimal);
                    dtReporte.Columns.Add(dtColumna);
                    DataColumn dtColumnaPor = new DataColumn();
                    dtColumnaPor.AllowDBNull = true;
                    dtColumnaPor.ColumnName = "categoria" + eCategoria.categoria;
                    dtColumnaPor.DataType = typeof(decimal);
                    dtPorcentaje.Columns.Add(dtColumnaPor);
                }
                dtReporte.Columns.Add("total", typeof(decimal)).AllowDBNull = true;
                dtPorcentaje.Columns.Add("total", typeof(decimal)).AllowDBNull = true;
                // Generando las columnas de la GRIDVIEW    
                BoundField ColumnBound1 = new BoundField { };
                ColumnBound1 = new BoundField();
                ColumnBound1.HeaderText = "Fecha Historico";
                ColumnBound1.DataField = "fecha_historico_tot";
                ColumnBound1.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                ColumnBound1.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                gvDatos.Columns.Add(ColumnBound1);
                gvPorcentaje.Columns.Add(ColumnBound1);
                foreach (Xpinn.Cartera.Entities.ClasificacionCartera eCategoria in lstCategoria)
                {
                    BoundField ColumnBound = new BoundField { };
                    ColumnBound = new BoundField();
                    ColumnBound.HeaderText = eCategoria.categoria;
                    ColumnBound.DataField = "categoria" + eCategoria.categoria;
                    ColumnBound.DataFormatString = "{0:n}";
                    ColumnBound.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    ColumnBound.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    gvDatos.Columns.Add(ColumnBound);
                    gvPorcentaje.Columns.Add(ColumnBound);
                }
                BoundField ColumnBoundT = new BoundField { };
                ColumnBoundT = new BoundField();
                ColumnBoundT.HeaderText = "Total";
                ColumnBoundT.DataField = "total";
                ColumnBoundT.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                ColumnBoundT.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                gvDatos.Columns.Add(ColumnBoundT);
                gvPorcentaje.Columns.Add(ColumnBoundT);

                List<IndicadorCartera> LstIndicador = new List<IndicadorCartera>();
                string fecini = ucFechaInicial.ToDateTime.ToString(conf.ObtenerFormatoFecha());
                string fecfin = ucFechaFinal.ToDateTime.ToString(conf.ObtenerFormatoFecha());
                LstIndicador = CarteraVencidaService.ConsultarCarteraVencidaXcategoria(filtro, fecini, fecfin, (Usuario)Session["Usuario"]);
                foreach (IndicadorCartera eIndicador in LstIndicador)
                {
                    // Buscar la categoria en el DATATABLE
                    int j = 0;
                    bool bEncontro = false;
                    foreach (DataRow dFila in dtReporte.Rows)
                    {
                        string fecha;
                        fecha = dFila[0].ToString();
                        if (eIndicador.fecha_historico.Trim() == fecha.Trim())
                        {
                            bEncontro = true;
                            int i = 1;
                            foreach (Xpinn.Cartera.Entities.ClasificacionCartera eCategoria in lstCategoria)
                            {
                                if (eCategoria.categoria == eIndicador.cod_categoria)
                                {
                                    dFila[i] = eIndicador.valor_mora.ToString();
                                    DataRow dFilaPor = dtPorcentaje.Rows[j];
                                    dFilaPor[i] = eIndicador.porcentaje_90dias.ToString();
                                }
                                i += 1;
                            }
                        }
                        j += 1;
                    }
                    if (!bEncontro)
                    {
                        DataRow dFila = dtReporte.NewRow();
                        dFila[0] = eIndicador.fecha_historico.Trim();
                        DataRow dFilaPor = dtPorcentaje.NewRow();
                        dFilaPor[0] = eIndicador.fecha_historico.Trim();
                        int i = 1;
                        foreach (Xpinn.Cartera.Entities.ClasificacionCartera eCategoria in lstCategoria)
                        {
                            if (eCategoria.categoria == eIndicador.cod_categoria)
                            {
                                dFila[i] = eIndicador.valor_mora.ToString();
                                dFilaPor[i] = eIndicador.porcentaje_90dias.ToString();
                            }
                            i += 1;
                        }
                        dtReporte.Rows.Add(dFila);
                        dtPorcentaje.Rows.Add(dFilaPor);
                    }
                }
                // Totalizar los datos
                int contador = 0;
                foreach (DataRow dFila in dtReporte.Rows)
                {
                    int i = 1;
                    decimal total = 0;
                    decimal totalPor = 0;
                    DataRow dFilaPor = dtPorcentaje.Rows[contador];
                    foreach (Xpinn.Cartera.Entities.ClasificacionCartera eCategoria in lstCategoria)
                    {
                        total += ConvertirStringToDecimal(dFila[i].ToString());
                        totalPor += ConvertirStringToDecimal(dFilaPor[i].ToString());
                        i += 1;
                    }
                    dFila[i] = total;
                    dFilaPor[i] = totalPor;
                    contador += 1;
                }

                gvDatos.DataSource = dtReporte;
                gvDatos.DataBind();
                gvPorcentaje.DataSource = dtPorcentaje;
                gvPorcentaje.DataBind();

            }

            // Generando la gráfica
            Chart1.Visible = true;
            Chart2.Visible = chkMostrarPorcentaje.Checked;

            // Configurando los títulos de las gráficas
            Chart1.Titles["Title1"].Text = "Evolución Saldo de Cartera Vencida Total ";
            if (rblFiltroCategoria.SelectedValue == "V")
                Chart1.Titles["Title1"].Text += "y > A " + seleccion.ToString() + " Dias";
            Chart1.Titles["Title2"].Text = ucFechaInicial.ToDate + " A " + ucFechaFinal.ToDate + " (Valores en Millones)";
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
            Chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;
            Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            Chart2.Titles["Title1"].Text = "Evolución Indicador de Cartera Vencida Total ";
            if (rblFiltroCategoria.SelectedValue == "V")
                Chart2.Titles["Title1"].Text += "y > A " + seleccion.ToString() + " Dias";
            Chart2.Titles["Title2"].Text = ucFechaInicial.ToDate + " A " + ucFechaFinal.ToDate + " (%Cartera)";
            Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
            Chart2.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;
            Chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            
            if (chkMostrarValores.Checked && chkMostrarPorcentaje.Checked)
            {
                Chart1.Height = 400;
                Chart1.Width = 600;
                Chart2.Height = 400;
                Chart2.Width = 600;
            }
            else
            {
                if (chkMostrarValores.Checked)
                {
                    Chart1.Height = 400;
                    Chart1.Width = 800;
                }
                if (chkMostrarPorcentaje.Checked)
                {
                    Chart2.Height = 400;
                    Chart2.Width = 800;
                }
            }

            // Configurando los campos de cada una de las gráficas
            if (rblFiltroCategoria.SelectedValue == "V")
            {
                Chart1.Series["Series1"].ChartType = TipoGrafica(ddlTipoGrafica1);
                Chart1.Series["Series2"].ChartType = TipoGrafica(ddlTipoGrafica1);
                Chart1.Series["Series1"]["DrawingStyle"] = "Emboss";
                Chart1.Series["Series2"]["DrawingStyle"] = "Emboss";

                Chart1.Series["Series1"].LegendText = "Total";
                Chart1.Series["Series2"].LegendText = "> A " + seleccion.ToString() + " Dias";
                Chart1.Series["Series1"].YValueMembers = "valor_vencido_tot";
                Chart1.Series["Series1"].XValueMember = "fecha_historico_tot";
                Chart1.Series["Series1"].LabelMapAreaAttributes = "valor_vencido_tot";
                Chart1.Series["Series1"].IsValueShownAsLabel = true;
                Chart1.Series["Series1"].LabelFormat = "{0:N0}";
                Chart1.Series["Series2"].YValueMembers = "valor_vencido";
                Chart1.Series["Series2"].XValueMember = "fecha_historico";
                Chart1.Series["Series2"].LabelMapAreaAttributes = "valor_vencido";
                Chart1.Series["Series2"].IsValueShownAsLabel = true;
                Chart1.Series["Series2"].LabelFormat = "{0:N0}";
                Chart1.DataSource = LstDetalleComprobante;
                Chart1.DataBind();

                Chart2.Series["Series1"].ChartType = TipoGrafica(ddlTipoGrafica2);
                Chart2.Series["Series2"].ChartType = TipoGrafica(ddlTipoGrafica2);
                Chart2.Series["Series1"]["DrawingStyle"] = "Emboss";
                Chart2.Series["Series2"]["DrawingStyle"] = "Emboss";
                Chart2.Series["Series1"].LegendText = "Total";
                Chart2.Series["Series2"].LegendText = "> A " + seleccion.ToString() + " Dias";
                Chart2.Series["Series1"].YValueMembers = "porcentaje_vencido_tot";
                Chart2.Series["Series1"].XValueMember = "fecha_historico_tot";
                Chart2.Series["Series1"].LabelMapAreaAttributes = "porcentaje_vencido_tot";
                Chart2.Series["Series1"].IsValueShownAsLabel = true;
               
                Chart2.Series["Series2"].YValueMembers = "porcentaje_vencido";
                Chart2.Series["Series2"].XValueMember = "fecha_historico";
                Chart2.Series["Series2"].LabelMapAreaAttributes = "porcentaje_vencido";
                Chart2.Series["Series2"].IsValueShownAsLabel = true;
                Chart2.Series["Series2"].Color = Chart1.Series["Series1"].Color;
               
                Chart2.DataSource = LstDetalleComprobante;
                Chart2.DataBind();
            }
            else
            {
                int numeroSerie = 1;
                Chart1.Series.Clear();
                Chart2.Series.Clear();
                if (ddlVencCateg.SelectedValue != "T")
                {
                    foreach (Xpinn.Cartera.Entities.ClasificacionCartera eCategoria in lstCategoria)
                    {
                        Series nueSerie;
                        if (numeroSerie > Chart1.Series.Count())
                        {
                            nueSerie = new Series();
                            nueSerie.Name = "Series" + numeroSerie;
                        }
                        else
                        {
                            nueSerie = Chart1.Series[numeroSerie - 1];
                        }
                        nueSerie.ChartType = TipoGrafica(ddlTipoGrafica1);
                        nueSerie["DrawingStyle"] = "Emboss";
                        nueSerie.LegendText = eCategoria.categoria;
                        nueSerie.YValueMembers = "categoria" + eCategoria.categoria;
                        nueSerie.XValueMember = "fecha_historico_tot";
                        nueSerie.LabelMapAreaAttributes = "categoria" + eCategoria.categoria;
                        nueSerie.IsValueShownAsLabel = true;

                        if (numeroSerie > Chart1.Series.Count())
                            Chart1.Series.Add(nueSerie);

                        Series nueSerie2;
                        if (numeroSerie > Chart2.Series.Count())
                        {
                            nueSerie2 = new Series();
                            nueSerie2.Name = "Series" + numeroSerie;
                        }
                        else
                        {
                            nueSerie2 = Chart2.Series[numeroSerie - 1];
                        }
                        nueSerie2.Color = nueSerie.Color;
                        nueSerie2.ChartType = TipoGrafica(ddlTipoGrafica2);
                        nueSerie2["DrawingStyle"] = "Emboss";
                        nueSerie2.LegendText = eCategoria.categoria;
                        nueSerie2.YValueMembers = "categoria" + eCategoria.categoria;
                        nueSerie2.XValueMember = "fecha_historico_tot";
                        nueSerie2.LabelMapAreaAttributes = "categoria" + eCategoria.categoria;
                        nueSerie2.IsValueShownAsLabel = true;

                        if (numeroSerie > Chart2.Series.Count())
                            Chart2.Series.Add(nueSerie2);
                        numeroSerie += 1;
                    }
                }
                Series totSerie;
                if (numeroSerie > Chart1.Series.Count())
                {
                    totSerie = new Series();
                    totSerie.Name = "Series" + numeroSerie;
                }
                else
                {
                    totSerie = Chart1.Series[numeroSerie - 1];
                }
                totSerie.ChartType = TipoGrafica(ddlTipoGrafica1);
                totSerie["DrawingStyle"] = "Emboss";
                totSerie.LegendText = "Total";
                totSerie.YValueMembers = "total";
                totSerie.XValueMember = "fecha_historico_tot";
                totSerie.LabelMapAreaAttributes = "total";
                totSerie.IsValueShownAsLabel = true;
                if (numeroSerie > Chart1.Series.Count())
                    Chart1.Series.Add(totSerie);

                Series totSerie2;
                if (numeroSerie > Chart2.Series.Count())
                {
                    totSerie2 = new Series();
                    totSerie2.Name = "Series" + numeroSerie;
                }
                else
                {
                    totSerie2 = Chart2.Series[numeroSerie - 1];
                }
                totSerie2.Color = totSerie.Color;
                totSerie2.ChartType = TipoGrafica(ddlTipoGrafica2);
                totSerie2["DrawingStyle"] = "Emboss";
                totSerie2.LegendText = "Total";
                totSerie2.YValueMembers = "total";
                totSerie2.XValueMember = "fecha_historico_tot";
                totSerie2.LabelMapAreaAttributes = "total";
                totSerie2.IsValueShownAsLabel = true;
                if (numeroSerie > Chart2.Series.Count())
                    Chart2.Series.Add(totSerie2);
              
                Chart1.DataSource = dtReporte;
                Chart1.DataBind();

                Chart2.DataSource = dtPorcentaje;
                Chart2.DataBind();
            }

            panelPorcentaje.Visible = chkMostrarPorcentaje.Checked ? true : false;
            gvPorcentaje.Visible = chkMostrarPorcentaje.Checked ? true : false;
            panelValores.Visible = chkMostrarValores.Checked ? true : false;
            gvDatos.Visible = chkMostrarValores.Checked ? true : false;
        }
    }
    protected SeriesChartType TipoGraficas(DropDownList ddlTipGra)
    {
        if (ddlTipGra.SelectedIndex == 0)
            return SeriesChartType.StackedColumn;
        if (ddlTipGra.SelectedIndex == 1)
            return SeriesChartType.StackedBar;
        if (ddlTipGra.SelectedIndex == 2)
            return SeriesChartType.StackedArea100;
        if (ddlTipGra.SelectedIndex == 3)
            return SeriesChartType.StackedArea;
        return SeriesChartType.StackedBar;
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

    protected void ddlVencCateg_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnInforme_Click(null, null);
    }

    protected void ddlTipoGrafica1_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnInforme_Click(null, null);
    }

    protected void ddlTipoGrafica2_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnInforme_Click(null, null);
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

        sw = expGrilla.ObtenerGrilla(gvPorcentaje, null);

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

    protected void ch3D_CheckedChanged(object sender, EventArgs e)
    {
        btnInforme_Click(null, null);
    }

    protected void rblFiltro2_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvDatos.DataSource = null;
        gvDatos.DataBind();
        if (rblFiltroCategoria.SelectedItem != null)
        {
            ddlVencCateg.Items.Clear();
            if (rblFiltroCategoria.SelectedIndex == 0)
            {
                ddlVencCateg.Visible = true;
                dllineas.Visible = false;
                drop_periocidadmora.Visible = false;
                lbl_mora.Visible = false;
                ddlVencCateg.Items.Insert(0, new ListItem("> a 30 Dias", "1"));
                ddlVencCateg.Items.Insert(1, new ListItem("> a 60 Dias", "2"));
                ddlVencCateg.Items.Insert(2, new ListItem("> a 90 Dias", "3"));
                ddlVencCateg.Items.Insert(3, new ListItem("> a 120 Dias", "4"));
                ddlVencCateg.Items.Insert(4, new ListItem("> a 180 Dias", "5"));
                ddlVencCateg.Items.Insert(5, new ListItem("> a 360 Dias", "6"));
                ddlVencCateg.SelectedIndex = 0;
                ddlVencCateg.DataBind();
            }
            else if (rblFiltroCategoria.SelectedIndex == 1)
            {
                ddlVencCateg.Visible = true;
                dllineas.Visible = false;
                drop_periocidadmora.Visible = false;
                lbl_mora.Visible = false;
                Xpinn.Cartera.Services.ClasificacionCarteraService BOCartera =
                    new Xpinn.Cartera.Services.ClasificacionCarteraService();
                List<Xpinn.Cartera.Entities.ClasificacionCartera> lstCategoria =
                    new List<Xpinn.Cartera.Entities.ClasificacionCartera>();
                lstCategoria = BOCartera.ListarCategoriasVencidas((Usuario)Session["usuario"]);
                if (lstCategoria.Count > 0)
                {
                    ddlVencCateg.AppendDataBoundItems = true;
                    ddlVencCateg.DataSource = lstCategoria;
                    ddlVencCateg.DataTextField = "categoria";
                    ddlVencCateg.DataValueField = "categoria";
                    ddlVencCateg.Items.Insert(0, new ListItem("Totales", "T"));
                    ddlVencCateg.Items.Insert(0, new ListItem("Seleccione un item", "0"));
                    ddlVencCateg.SelectedIndex = 0;
                    ddlVencCateg.DataBind();
                }
            }
            else
            {
                dllineas.SelectedText = "";
                ddlVencCateg.Visible = false;
                drop_periocidadmora.Visible = true;
                lbl_mora.Visible = true;
                dllineas.Visible = true;
                Configuracion conf = new Configuracion();
                List<CarteraVencida> LstDetalleComprobante = new List<CarteraVencida>();
                LstDetalleComprobante = CarteraVencidaService.ListarLineasCredito((Usuario)Session["Usuario"]);

                dllineas.DataSource = LstDetalleComprobante;
                dllineas.DataTextField = "nombre";
                dllineas.DataValueField = "Cod_linea_credito";
                dllineas.Items.Insert(0, new ListItem("Seleccione un item", "0"));
                dllineas.DataBind();


                drop_periocidadmora.Items.Clear();
                drop_periocidadmora.Items.Insert(0, new ListItem("> a 30 Dias", "1"));
                drop_periocidadmora.Items.Insert(1, new ListItem("> a 60 Dias", "2"));
                drop_periocidadmora.Items.Insert(2, new ListItem("> a 90 Dias", "3"));
                drop_periocidadmora.Items.Insert(3, new ListItem("> a 120 Dias", "4"));
                drop_periocidadmora.Items.Insert(4, new ListItem("> a 180 Dias", "5"));
                drop_periocidadmora.Items.Insert(5, new ListItem("> a 360 Dias", "6"));
                drop_periocidadmora.SelectedIndex = 0;
                drop_periocidadmora.DataBind();
            }
        }
    }

    protected void chkMostrarPorcentaje_CheckedChanged(object sender, EventArgs e)
    {
        btnInforme_Click(null, null);
    }

    protected void chkMostrarValores_CheckedChanged(object sender, EventArgs e)
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

}

