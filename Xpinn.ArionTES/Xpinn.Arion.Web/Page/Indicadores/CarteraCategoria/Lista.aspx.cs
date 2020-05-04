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


partial class Lista : GlobalWeb
{
    private Xpinn.Indicadores.Services.IndicadorCarteraService CarteraCategoriasServicio = new Xpinn.Indicadores.Services.IndicadorCarteraService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(CarteraCategoriasServicio.CodigoProgramaCartCatego, "L");
            Site toolBar = (Site)this.Master;
            txtColorFondo.eventoCambiar += txtColorFondo_TextChanged;
 
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CarteraCategoriasServicio.CodigoProgramaCartCatego, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CargarDropDown();
            if (Session[CarteraCategoriasServicio.CodigoProgramaCartCatego + ".id"] != null)
            {
                Usuario usuap = (Usuario)Session["usuario"];
            }
            btnInforme_Click(null, null);
        }
        
    }
    
    protected void CargarDropDown() 
    {
        Xpinn.Indicadores.Services.CarteraOficinasService carteraOficinaServicio = new Xpinn.Indicadores.Services.CarteraOficinasService();
        List<CarteraOficinas> lstFechas = new List<CarteraOficinas>();
        lstFechas = carteraOficinaServicio.consultarfecha((Usuario)Session["Usuario"]);
        ddlFecCorte.DataSource = lstFechas;
        ddlFecCorte.DataValueField = "fecha_corte";
        ddlFecCorte.DataTextField = "fecha_corte";
        ddlFecCorte.DataBind();

        Xpinn.FabricaCreditos.Services.OficinaService oficinaServicio = new Xpinn.FabricaCreditos.Services.OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
        ddloficina.DataTextField = "nombre";
        ddloficina.DataValueField = "codigo";
        ddloficina.DataSource = oficinaServicio.ListarOficinas(oficina, (Usuario)Session["usuario"]);
        ddloficina.DataBind();

        Xpinn.FabricaCreditos.Services.LineasCreditoService BOLinea = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        List<Xpinn.FabricaCreditos.Entities.LineasCredito> lstLineas = new List<Xpinn.FabricaCreditos.Entities.LineasCredito>();
        Xpinn.FabricaCreditos.Entities.LineasCredito pEntidad = new Xpinn.FabricaCreditos.Entities.LineasCredito();
        pEntidad.estado = 1;
        lstLineas = BOLinea.ListarLineasCredito(pEntidad, (Usuario)Session["usuario"]);
        if (lstLineas.Count > 0)
        {
            ddlLinea.DataSource = lstLineas;
            ddlLinea.DataTextField  = "nom_linea_credito";
            ddlLinea.DataValueField = "cod_linea_credito";
            ddlLinea.DataBind();
        }

        Xpinn.Cartera.Services.ClasificacionCarteraService BOCartera = new Xpinn.Cartera.Services.ClasificacionCarteraService();
        List<Xpinn.Cartera.Entities.ClasificacionCartera> lstCategoria = new List<Xpinn.Cartera.Entities.ClasificacionCartera>();
        lstCategoria = BOCartera.ListarCategorias((Usuario)Session["usuario"]);
        if (lstCategoria.Count > 0)
        {
            ddlCategoria.DataSource = lstCategoria;
            ddlCategoria.DataTextField = "descripcion";
            ddlCategoria.DataValueField = "categoria";
            ddlCategoria.DataBind();
        }        
    }
    
    protected string obtfiltro() 
    {
        string filtrare = " and l.aplica_asociado = 1 ";
        if (ddlLinea.SelectedText != "")
        {
            filtrare += " and h.cod_linea_Credito In (";
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
        if (ddloficina.SelectedText != "")
        {
            filtrare += " and h.cod_oficina In ( " + ddloficina.SelectedValue+")";
        }
        if (ddlCategoria.SelectedText != "")
            filtrare += " and ";
        return filtrare;
    }


    protected void btnInforme_Click(object sender, EventArgs e)
    {
        gvDatos.Visible = true;
        string filtro = obtfiltro();

        // Generar columnas del DATATABLE y de la GRIDVIEW    
        DataTable dtReporte = new DataTable();
        dtReporte.Clear();
        dtReporte.Columns.Add("cod_categoria", typeof(String)).AllowDBNull = true;
        dtReporte.Columns.Add("categoria", typeof(String)).AllowDBNull = true;

        Xpinn.FabricaCreditos.Services.OficinaService oficinaServicio = new Xpinn.FabricaCreditos.Services.OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
        List<Xpinn.FabricaCreditos.Entities.Oficina> lstOficina = new List<Xpinn.FabricaCreditos.Entities.Oficina>();
        lstOficina = oficinaServicio.ListarOficinas(oficina, (Usuario)Session["usuario"]);
        gvDatos.Columns.Clear();
        BoundField ColumnBound1 = new BoundField { };
        ColumnBound1 = new BoundField();
        ColumnBound1.HeaderText = "CATEGORIA";
        ColumnBound1.DataField = "cod_categoria";
        ColumnBound1.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        ColumnBound1.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
        gvDatos.Columns.Add(ColumnBound1);
        BoundField ColumnBound2 = new BoundField { };
        ColumnBound2 = new BoundField();
        ColumnBound2.HeaderText = "DESCRIPCION";
        ColumnBound2.DataField = "categoria";
        ColumnBound2.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        ColumnBound2.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
        gvDatos.Columns.Add(ColumnBound2);
        foreach (Xpinn.FabricaCreditos.Entities.Oficina eOficina in lstOficina)
        {
            BoundField ColumnBoundKAP = new BoundField { };
            ColumnBoundKAP = new BoundField();
            ColumnBoundKAP.HeaderText = eOficina.Nombre;
            ColumnBoundKAP.DataField = "valor" + eOficina.Codigo;
            ColumnBoundKAP.DataFormatString = "{0:n}";
            ColumnBoundKAP.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            ColumnBoundKAP.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            gvDatos.Columns.Add(ColumnBoundKAP);

            DataColumn ColumnDataKAP = new DataColumn();
            ColumnDataKAP.ColumnName = "valor" + eOficina.Codigo;
            ColumnDataKAP.AllowDBNull = true;
            ColumnDataKAP.DataType = typeof(decimal);
            dtReporte.Columns.Add(ColumnDataKAP);
        }
        BoundField ColumnBoundT = new BoundField { };
        ColumnBoundT = new BoundField();
        ColumnBoundT.HeaderText = "TOTAL";
        ColumnBoundT.DataField = "total";
        ColumnBoundT.DataFormatString = "{0:c2}";
        ColumnBoundT.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        ColumnBoundT.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        gvDatos.Columns.Add(ColumnBoundT);
        DataColumn ColumnDataTot = new DataColumn();
        ColumnDataTot.ColumnName = "total";
        ColumnDataTot.AllowDBNull = true;
        ColumnDataTot.DataType = typeof(decimal);
        dtReporte.Columns.Add(ColumnDataTot);

        Xpinn.Cartera.Services.ClasificacionCarteraService BOCartera = new Xpinn.Cartera.Services.ClasificacionCarteraService();
        List<Xpinn.Cartera.Entities.ClasificacionCartera> lstCategoria = new List<Xpinn.Cartera.Entities.ClasificacionCartera>();
        lstCategoria = BOCartera.ListarCategorias((Usuario)Session["usuario"]);
        int conta = 0;
        foreach (Xpinn.Cartera.Entities.ClasificacionCartera eCategoria in lstCategoria)
        {
            if (eCategoria.categoria.Contains("E"))
            {
                if (conta == 0)
                {
                    DataRow dtFila = dtReporte.NewRow();
                    dtFila[0] = eCategoria.categoria;
                    dtFila[1] = eCategoria.descripcion;
                    dtReporte.Rows.Add(dtFila);
                }
                conta++;
            }
            else
            {
                DataRow dtFila = dtReporte.NewRow();
                dtFila[0] = eCategoria.categoria;
                dtFila[1] = eCategoria.descripcion;
                dtReporte.Rows.Add(dtFila);
            }
        }
        DataRow dtFilaTot = dtReporte.NewRow();
        dtFilaTot[0] = "GRAN TOTAL CARTERA";
        dtFilaTot[1] = "";
        dtReporte.Rows.Add(dtFilaTot);

        DataRow dtTotVenc = dtReporte.NewRow();
        dtTotVenc[0] = "TOTAL CARTERA VENCIDA";
        dtTotVenc[1] = "";
        dtReporte.Rows.Add(dtTotVenc);

        DataRow dtTotPor = dtReporte.NewRow();
        dtTotPor[0] = "% C VENC. / C TOTAL";
        dtTotPor[1] = "";
        dtReporte.Rows.Add(dtTotPor);

        // Cargar datos al DATATABLE
        decimal TotalCartera = 0;
        List<IndicadorCartera> lstIndicador = new List<IndicadorCartera>();
        lstIndicador = CarteraCategoriasServicio.ConsultarCarteraCategorias(ConvertirStringToDate(ddlFecCorte.SelectedValue), filtro, (Usuario)Session["Usuario"]);
        foreach (IndicadorCartera eIndicador in lstIndicador)
        {
            // Buscar la categoria en el DATATABLE
            foreach (DataRow dFila in dtReporte.Rows)
            {
                if (dFila[0].ToString().Substring(0, 1) == eIndicador.cod_categoria.Substring(0, 1))
                {
                    // Buscar la oficina en las columnas 
                    int i = 2;
                    foreach (Xpinn.FabricaCreditos.Entities.Oficina eOficina in lstOficina)
                    {
                        if (eOficina.Codigo == eIndicador.cod_oficina)
                        {
                            if (dFila[0].ToString().Contains("E"))
                            {
                                decimal pVal = 0, pCol = 0;
                                pCol = dFila[i].ToString() != "" ? Convert.ToDecimal(dFila[i].ToString()) : 0;
                                pVal = pCol + eIndicador.valor_cartera;
                                dFila[i] = pVal;
                            }
                            else
                                dFila[i] = eIndicador.valor_cartera;
                        }
                        i += 1;
                    }
                }
            }
            TotalCartera += eIndicador.valor_cartera;
        }

        // Totalizar las filas
        decimal[] totales = new decimal[]{};
        decimal[] totalesVencida = new decimal[] { };
        foreach (DataRow dFila in dtReporte.Rows)
        {
            decimal totalCategoria = 0;
            int i = 2;
            int cont = 1;
            foreach (Xpinn.FabricaCreditos.Entities.Oficina eOficina in lstOficina)
            {              
                // Extrayendo el valor de la casilla o celda del datarow
                decimal valor = 0;
                if (dFila[i].ToString().Trim() != "")
                    try
                    {
                        valor = Convert.ToDecimal(dFila[i].ToString().Trim().Replace("$", "").Replace(gSeparadorMiles, ""));
                    }
                    catch { }                
                if (totales.Count() < cont && cont >= 1)
                    Array.Resize(ref totales, cont);
                totales[cont-1] += valor;
                // Determinar los totales de cartera vencida
                if (dFila[0].ToString().Trim() != "A")
                {
                    if (totalesVencida.Count() < cont && cont >= 1)
                        Array.Resize(ref totalesVencida, cont);
                    totalesVencida[cont - 1] += valor;
                }
                // Determina el total de la categoria (fila).
                totalCategoria += valor;
                cont += 1;
                i += 1;
            }
            dFila[i] = totalCategoria.ToString();
        }
        //llenando totales
        decimal sumaTot = 0;
        for (int i = 0; i < totales.Count(); i++)
        {
            DataRow dTotales = dtReporte.Rows[dtReporte.Rows.Count - 3];
            if (totales[i] != 0)
            {
                dTotales[i + 2] = totales[i];
                sumaTot += totales[i];
            }
        }
        DataRow dsumaTotales = dtReporte.Rows[dtReporte.Rows.Count - 3];
        dsumaTotales[lstOficina.Count + 2] = sumaTot;
        //llenando los totales vencidos
        decimal sumaTotVen = 0;
        for (int i = 0; i < totalesVencida.Count(); i++)
        {
            DataRow dTotalesVenc = dtReporte.Rows[dtReporte.Rows.Count - 2];
            if (totalesVencida[i] != 0)
            {
                dTotalesVenc[i + 2] = totalesVencida[i];
                sumaTotVen += totalesVencida[i];
            }

            //llenando porcentaje
            decimal Total = 0, TotalVencida = 0;
            TotalVencida = totalesVencida[i];
            Total = totales[i];
            DataRow dPorcentaje = dtReporte.Rows[dtReporte.Rows.Count - 1];
            if (TotalVencida != 0 && Total != 0)
                dPorcentaje[i + 2] = Math.Round(TotalVencida / Total * 100, 2); ;                
        }
        DataRow dsumaTotalesVencida = dtReporte.Rows[dtReporte.Rows.Count - 2];
        dsumaTotalesVencida[lstOficina.Count + 2] = sumaTotVen;
        DataRow dporTotalesVencida = dtReporte.Rows[dtReporte.Rows.Count - 1];
        if (sumaTot != 0 && sumaTot != null)
            dporTotalesVencida[lstOficina.Count + 2] = Math.Round(sumaTotVen / sumaTot * 100, 2);         

        gvDatos.DataSource = dtReporte;
        gvDatos.DataBind();
    
        // Haciendo visible las gráficas
        Chart1.Visible = true;
        
        // --------------------------------------------------------------------------------------------------------------------------------------------
        // Mostrar la primera gráfica
        // -------------------------------------------------------------------------------------------------------------------------------------------
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = ch3D.Checked;
        Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        // Determinar si se genera por totales o por número
        Chart1.Titles["Title1"].Text = "CARTERA POR CATEGORIAS";
        Chart1.Legends[0].Enabled = chkmostrarlegendas.Checked;
        // Mostrar los nombres de las seríes
        Chart1.Series.Clear();        
        foreach (Xpinn.FabricaCreditos.Entities.Oficina eOficina in lstOficina)
        {
            Series item = new Series();      
            item.Name = eOficina.Nombre;
            item.ChartType = TipoGrafica(ddlTipoGrafica);
            item.YValueMembers = "valor" + eOficina.Codigo;
            item.XValueMember = "cod_categoria";
            item.LabelMapAreaAttributes = "valor" + eOficina.Codigo;
            if (ddloficina.SelectedText == "")
                item.IsValueShownAsLabel = false;
            else
                item.IsValueShownAsLabel = true;
            item.LabelFormat = "n0";
            Chart1.Series.Add(item);
        }         
        // Mostrar la gráfica en pantalla
        Chart1.DataSource = dtReporte;
        Chart1.DataBind();
    }

    protected void ddlFecCorte_SelectedIndexChanged(object sender, EventArgs e)
    {
        Generar();
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

    protected void ddlTipoGrafica_SelectedIndexChanged(object sender, EventArgs e)
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

    protected void MostrarAnotacion(Chart pChart, Int32 pItem, string pTexto, double pPosicion)
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
