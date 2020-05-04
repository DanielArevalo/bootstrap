using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using Xpinn.Util;
using System.Text;
using Xpinn.Cartera.Services;
using Xpinn.Cartera.Entities;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;

public partial class Lista : GlobalWeb
{
    Xpinn.Cartera.Services.ReporteService _reporteService = new Xpinn.Cartera.Services.ReporteService();
    Usuario _usuario;


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_reporteService.CodigoProgramaReporteConsolidadoCierre, "D");
            Site toolBar = (Site)Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.MostrarExportar(false);
            toolBar.eventoExportar += btnExportar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_reporteService.CodigoProgramaReporteConsolidadoCierre, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        _usuario = (Usuario)Session["usuario"];

        if (!IsPostBack)
        {

            CargarValoresConsulta(pConsulta, _reporteService.CodigoProgramaReporteConsolidadoCierre);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            ddlConsultar_SelectedIndexChanged(this, EventArgs.Empty);
            InicializarPagina();
        }
    }


    protected void btnConsultar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarExportar(false);
        VerError("");
        string error = string.Empty;

        if (!ddlFechaCorte.fecha_cierre.HasValue || ddlFechaCorte.fecha_cierre.Value == DateTime.MinValue)
        {
            error += "Debes seleccionar una fecha de corte valida!, ";
        }
        else if (ddlConsultar.SelectedValue == "0" || string.IsNullOrWhiteSpace(ddlConsultar.SelectedValue))
        {
            error += "Debes seleccionar un tipo de reporte!.";
        }

        if (!string.IsNullOrWhiteSpace(error))
        {
            VerError(error);
            return;
        }

        switch (ddlConsultar.SelectedIndex)
        {
            case 1:
                Session["op1"] = 1;
                break;
            case 2:
                Session["op1"] = 2;
                break;
            case 3:
                Session["op1"] = 3;
                break;
        }
        btnDatos.Visible = false;

        panelReporte.Visible = true;
        LlenarReporte();
    }


    void btnLimpiar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ddlClasificacion.LimpiarSeleccion();
        ddlFormaPago.LimpiarSeleccion();
        ddlTipoGarantia.LimpiarSeleccion();
        ddlCategoria.LimpiarSeleccion();
        ddlOficina.LimpiarSeleccion();
        ddlFechaCorte.LimpiarSeleccion();
        ddlLinea.LimpiarSeleccion();
        btnDatos.Visible = false;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (gvReportecausacion.Rows.Count > 0 && Session["DTDETALLE1"] != null || gvReportecierre.Rows.Count > 0 && Session["DTDETALLE"] != null  || gvReporteProvision.Rows.Count > 0  && Session["DTDETALLE2"] != null)
        {
          
                ExportarCSV();
        }
    }

    protected void ExportarCSV()
    {

        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
             "attachment;filename=Reporte.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";

            if (gvReportecierre.Rows.Count > 0)
            {
                gvReportecierre.DataSource = Session["DTDETALLE"];
                gvReportecierre.DataBind();
                StringBuilder sb = ExportarGridCSV(gvReportecierre);
                Response.Output.Write(sb.ToString());
                Response.Flush();
                Response.End();
                Site toolBar = (Site)Master;
                toolBar.MostrarExportar(true);

            }
            if (gvReportecausacion.Rows.Count > 0)
            {
                gvReportecausacion.DataSource = Session["DTDETALLE1"];
                gvReportecausacion.DataBind();
                StringBuilder sb = ExportarGridCSV(gvReportecausacion);
                Response.Output.Write(sb.ToString());
                Response.Flush();
                Response.End();
                Site toolBar = (Site)Master;
                toolBar.MostrarExportar(true);

            }

            if (gvReporteProvision.Rows.Count > 0)
            {
                gvReporteProvision.DataSource = Session["DTDETALLE2"];
                gvReporteProvision.DataBind();
                StringBuilder sb = ExportarGridCSV(gvReporteProvision);
                Response.Output.Write(sb.ToString());
                Response.Flush();
                Response.End();
                Site toolBar = (Site)Master;
                toolBar.MostrarExportar(true);

            }
        }



        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void ddlConsultar_SelectedIndexChanged(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarExportar(false);

        Session["DTDETALLE3"] = null;
        btnDatos.Visible = false;
        TipoReporteCartera tipoReporte = ddlConsultar.SelectedValue.ToEnum<TipoReporteCartera>();
        gvReporteProvision.DataSource = Session["DTDETALLE3"];
        gvReporteProvision.DataBind();
        gvReporteProvision.Visible = false;
        gvReportecausacion.DataSource = Session["DTDETALLE3"];
        gvReportecausacion.DataBind();
        gvReportecausacion.Visible = false;
        gvReportecierre.DataSource = Session["DTDETALLE3"];
        gvReportecierre.DataBind();
        gvReportecierre.Visible = false;
        switch (tipoReporte)
        {
            case TipoReporteCartera.CierreCartera:
                ddlTipoGarantia.Enabled = true;
                ddlFormaPago.Enabled = true;
                break;
            case TipoReporteCartera.CausacionCartera:
                ddlTipoGarantia.LimpiarSeleccion();
                ddlTipoGarantia.Enabled = false;

                ddlFormaPago.LimpiarSeleccion();
                ddlFormaPago.Enabled = false;
                break;
            case TipoReporteCartera.ProvisionCartera:
                ddlFormaPago.LimpiarSeleccion();
                ddlFormaPago.Enabled = false;

                ddlTipoGarantia.Enabled = true;
                break;
        }
    }


    void InicializarPagina()
    {
        LlenarListas();

        btnDatos.Visible = false;
    }


    void LlenarListas()
    {
        ddlFechaCorte.Inicializar("R", "D");

        ClasificacionService clasificacionSer = new ClasificacionService();
        List<Clasificacion> lstClasificacion = clasificacionSer.ListarClasificacion(new Clasificacion(), _usuario);

        ddlClasificacion.DataTextField = "descripcion";
        ddlClasificacion.DataValueField = "cod_clasifica";
        ddlClasificacion.DataSource = lstClasificacion;
        ddlClasificacion.DataBind();

        var tipoGarantias = new[] {
                new { Nombre = "Admisible", Valor = 1  },
                new { Nombre = "Otras Garantías", Valor = 2  }
            };


        ddlTipoGarantia.DataTextField = "Nombre";
        ddlTipoGarantia.DataValueField = "Valor";
        ddlTipoGarantia.DataSource = tipoGarantias;
        ddlTipoGarantia.DataBind();

        var formaDePagos = new[] {
                new { Nombre = "Sin Libranza", Valor = "C"  },
                new { Nombre = "Con Libranza", Valor = "N"  }
            };

        ddlFormaPago.DataTextField = "Nombre";
        ddlFormaPago.DataValueField = "Valor";
        ddlFormaPago.DataSource = formaDePagos;
        ddlFormaPago.DataBind();

        CategoriasService categoriaSer = new CategoriasService();
        List<Categorias> lstCategorias = categoriaSer.ListarCategorias(new Categorias(), _usuario);

        lstCategorias.ForEach(x => x.descripcion = x.cod_categoria + "-" + x.descripcion);

        ddlCategoria.DataTextField = "descripcion";
        ddlCategoria.DataValueField = "cod_categoria";
        ddlCategoria.DataSource = lstCategorias;
        ddlCategoria.DataBind();

        Oficina_ciudadService oficinaSer = new Oficina_ciudadService();
        List<Oficina_ciudad> lstOficinas = oficinaSer.ListarOficina_ciudad(new Oficina_ciudad(), _usuario);

        ddlOficina.DataTextField = "Nombre_Oficina";
        ddlOficina.DataValueField = "cod_oficina";
        ddlOficina.DataSource = lstOficinas;
        ddlOficina.DataBind();

        LineasCredito linea = new LineasCredito();
        List<LineasCredito> LstLineaCredito = new List<LineasCredito>();
        LineasCreditoService lineascreditoServicio = new LineasCreditoService();
        LstLineaCredito = lineascreditoServicio.ListarLineasCredito(linea, (Usuario)Session["usuario"]);
        LstLineaCredito.ForEach(x => x.nombre = x.cod_linea_credito + "-" + x.nombre);

        ddlLinea.DataTextField = "Nombre";
        ddlLinea.DataValueField = "codigo";
        ddlLinea.DataSource = LstLineaCredito;
        ddlLinea.DataBind();
    }


    void LlenarReporte()
    {
        try
        {
            TipoReporteCartera tipoReporte = ddlConsultar.SelectedValue.ToEnum<TipoReporteCartera>();
            string filtro = ObtenerFiltro();
            string fechaCorte = ddlFechaCorte.fecha_cierre.Value.ToString("dd/MM/yyyy");

            List<ReporteConsolidadoCierre> lstReporte = ConsultarReporte(tipoReporte, fechaCorte, filtro);
         

            if (lstReporte == null || lstReporte.Count == 0)
            {
                mvReporte.ActiveViewIndex = 1;
                return;
            }
            else
            { 

                mvReporte.ActiveViewIndex = 0;
          }

            // Lleno DataTable del Reporte
            DataTable dtReporteCierre = lstReporte.ToDataTable();

            // Limpiar DataSource de Reporte
            ReportViewerPlan.Reset();
            ReportViewerPlan.LocalReport.DataSources.Clear();

            Tuple<string, string> configuracionReporte = ConfigurarReporte(tipoReporte);

            // Cargo los DataTables al DataSource del reporte y lo muestro
            // Configuro el Reporte => Item1 = Nombre del datasource a usar (definido en el RDLC)
            // Configuro el Reporte => Item2 = Ruta del RDLC
            ReportDataSource rds1 = new ReportDataSource(configuracionReporte.Item1, dtReporteCierre);
            ReportViewerPlan.LocalReport.DataSources.Add(rds1);

            ReportViewerPlan.LocalReport.ReportPath = configuracionReporte.Item2;
            ReportViewerPlan.LocalReport.Refresh();
            btnDatos.Visible = true;
        }
        catch (Exception ex)
        {
            VerError("Error al armar el reporte, " + ex.Message);
        }
    }


    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        string clasificacion = ddlClasificacion.SelectedValue;
        string formaPago = ddlFormaPago.SelectedValue;
        string tipoGarantia = ddlTipoGarantia.SelectedValue;
        string categoria = ddlCategoria.SelectedValue;
        string oficina = ddlOficina.SelectedValue;
        string linea = ddlLinea.SelectedValue;

        if (!string.IsNullOrWhiteSpace(clasificacion))
        {
            filtro += " and hist.cod_clasifica IN (" + clasificacion + ") ";
        }

        if (!string.IsNullOrWhiteSpace(formaPago))
        {
            string formaPagoReplaced = "'" + formaPago.Replace(",", "','") + "'";

            filtro += " and hist.forma_pago IN (" + formaPagoReplaced + ") ";
        }

        if (!string.IsNullOrWhiteSpace(tipoGarantia))
        {
            if (tipoGarantia.Contains("2"))
            {
                filtro += " and (hist.tipo_garantia IS NULL or hist.tipo_garantia IN (" + tipoGarantia + "))";
            }
            else
            {
                filtro += " and hist.tipo_garantia IN (" + tipoGarantia + ")";
            }
        }

        if (!string.IsNullOrWhiteSpace(categoria))
        {
            string categoriaArmada = "'" + categoria.Replace(",", "','") + "'";

            filtro += " and hist.cod_categoria_cli IN (" + categoriaArmada + ") ";
        }

        if (!string.IsNullOrWhiteSpace(linea))
        {
            string LineaArmada = "'" + linea.Replace(",", "','") + "'";

            filtro += " and hist.cod_linea_credito IN (" + LineaArmada + ") ";
        }

        if (!string.IsNullOrWhiteSpace(oficina))
        {
            filtro += " and hist.cod_oficina IN (" + oficina + ") ";
        }

        return filtro;
    }


    List<ReporteConsolidadoCierre> ConsultarReporte(TipoReporteCartera tipoReporte, string fechaCorte, string filtro)
    {
        List<ReporteConsolidadoCierre> lstReporte = null;

        switch (tipoReporte)
        {
            case TipoReporteCartera.CierreCartera:
                lstReporte = _reporteService.ConsultarReporteConsolidadoCierre(fechaCorte, filtro, _usuario);
                break;
            case TipoReporteCartera.CausacionCartera:
                lstReporte = _reporteService.ConsultarReporteConsolidadoCausacion(fechaCorte, filtro, _usuario);
                break;
            case TipoReporteCartera.ProvisionCartera:
                lstReporte = _reporteService.ConsultarReporteConsolidadoProvision(fechaCorte, filtro, _usuario);
                break;
        }

        return lstReporte;
    }


    Tuple<string, string> ConfigurarReporte(TipoReporteCartera tipoReporte)
    {
        string dataSetName = string.Empty;
        string reportPath = string.Empty;

        switch (tipoReporte)
        {
            case TipoReporteCartera.CierreCartera:
                dataSetName = "DataSetReporteConsolidadoCierre";
                reportPath = @"Page\Cartera\ReporteConsolidadoCierre\ReporteConsolidadoCierre.rdlc";
                break;
            case TipoReporteCartera.CausacionCartera:
                dataSetName = "DataSetReporteConsolidadoCausacion";
                reportPath = @"Page\Cartera\ReporteConsolidadoCierre\ReporteConsolidadoCausacion.rdlc";
                break;
            case TipoReporteCartera.ProvisionCartera:
                dataSetName = "DataSetReporteConsolidadoProvision";
                reportPath = @"Page\Cartera\ReporteConsolidadoCierre\ReporteConsolidadoProvision.rdlc";
                break;
        }

        return Tuple.Create(dataSetName, reportPath);
    }

    protected void btnDatos_Click(object sender, EventArgs e)
    {
       
    
        Actualizar();
    }

    private void Actualizar()
    {
        Configuracion conf = new Configuracion();
        VerError("");
        try
        {
            DateTime fecha;
           

            // Determinar el còdigo del usuario y determinar si el usuario es un ejecutivo      
            Usuario usuap = (Usuario)Session["usuario"];
            gvReportecierre.Visible = false;
           // gvReportecausacion.Visible = false;
            //gvReporteprovision.Visible = false;

            // Generar el reporte
            switch ((int)Session["op1"])
            {
                case 1:
                    {
                        gvReportecausacion.Visible = false;
                        gvReporteProvision.Visible = false;
                        gvReportecierre.Visible = true;
                        VerError("");
                        fecha = ddlFechaCorte.fecha_cierre.ToString() == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(ddlFechaCorte.fecha_cierre.ToString());
                         List<ReporteConsolidadoCierre> lstReporte = null;
                        string fechaCorte = ddlFechaCorte.fecha_cierre.Value.ToString("dd/MM/yyyy");

                        lstReporte = _reporteService.ConsultarReporteConsolidadoCierre(fechaCorte, ObtenerFiltro(), _usuario);
                        gvReportecierre.EmptyDataText = emptyQuery;
                        gvReportecierre.DataSource = lstReporte;
                        Session["DTDETALLE"] = lstReporte;

                        if (lstReporte.Count > 0)
                        {
                            mvReporte.ActiveViewIndex =1;
                            gvReportecierre.Visible = true;
                            Site toolBar = (Site)Master;
                            toolBar.MostrarExportar(true);

                            // mvReporte.SetActiveView(Datos);
                            //  lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaMora.Count;
                            gvReportecierre.DataBind();
                        }
                        else
                        {
                            mvReporte.ActiveViewIndex = 0;
                        }
                        Session.Add(_reporteService.CodigoProgramaReporteConsolidadoCierre + ".consulta", 1);
                        break;


                    }
                case 2:
                    {
                        gvReportecausacion.Visible = true;
                        gvReportecierre.Visible = false;
                        gvReporteProvision.Visible = false;
                        VerError("");
                        fecha = ddlFechaCorte.fecha_cierre.ToString() == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(ddlFechaCorte.fecha_cierre.ToString());
                        List<ReporteConsolidadoCierre> lstReporte = null;
                        string fechaCorte = ddlFechaCorte.fecha_cierre.Value.ToString("dd/MM/yyyy");

                        lstReporte = _reporteService.ConsultarReporteConsolidadoCausacion(fechaCorte, ObtenerFiltro(), _usuario);
                        Session["DTDETALLE1"] = lstReporte;
                        gvReportecausacion.EmptyDataText = emptyQuery;
                        gvReportecausacion.DataSource = lstReporte;
                        if (lstReporte.Count > 0)
                        {
                            mvReporte.ActiveViewIndex = 1;
                            gvReportecausacion.Visible = true;
                            Site toolBar = (Site)Master;
                            toolBar.MostrarExportar(true);
                            // mvReporte.SetActiveView(Datos);
                            //  lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaMora.Count;
                            gvReportecausacion.DataBind();
                        }
                        else
                        {
                            mvReporte.ActiveViewIndex = 0;
                        }
                        Session.Add(_reporteService.CodigoProgramaReporteConsolidadoCierre + ".consulta", 2);
                        break;
                    }


                case 3:
                    {
                     
                        gvReporteProvision.Visible = true;
                        gvReportecierre.Visible = false;
                        gvReportecierre.DataBind();
                        gvReportecausacion.Visible = false;
                        gvReportecausacion.DataBind();
                        VerError("");
                        fecha = ddlFechaCorte.fecha_cierre.ToString() == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(ddlFechaCorte.fecha_cierre.ToString());
                        List<ReporteConsolidadoCierre> lstReporte = null;
                        string fechaCorte = ddlFechaCorte.fecha_cierre.Value.ToString("dd/MM/yyyy");

                        lstReporte = _reporteService.ConsultarReporteConsolidadoProvision(fechaCorte, ObtenerFiltro(), _usuario);
                        Session["DTDETALLE2"] = lstReporte;
                        gvReporteProvision.EmptyDataText = emptyQuery;
                        gvReporteProvision.DataSource = lstReporte;
                        if (lstReporte.Count > 0)
                        {
                            mvReporte.ActiveViewIndex = 1;
                            gvReporteProvision.Visible = true;
                            Site toolBar = (Site)Master;
                            toolBar.MostrarExportar(true);
                            // mvReporte.SetActiveView(Datos);
                            //  lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaMora.Count;
                            gvReporteProvision.DataBind();
                        }
                        else
                        {
                            mvReporte.ActiveViewIndex = 0;
                        }
                        Session.Add(_reporteService.CodigoProgramaReporteConsolidadoCierre + ".consulta", 32);
                        break;
                    }


            }
            
        }
        catch (Exception ex)
        {
            VerError(ex.ToString());
        }
    }
}