using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Cartera.Services.ClasificacionCarteraService serviceCalifica = new Xpinn.Cartera.Services.ClasificacionCarteraService();
    private int _tipoOpe = 36;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[serviceCalifica.CodigoProgramaComprobanteCausacion + ".id"] != null)
                VisualizarOpciones(serviceCalifica.CodigoProgramaComprobanteCausacion, "E");
            else
                VisualizarOpciones(serviceCalifica.CodigoProgramaComprobanteCausacion, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceCalifica.CodigoProgramaComprobanteCausacion, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                UpdatePanel3.Visible = false;
                LlenarCombos();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceCalifica.CodigoProgramaComprobanteCausacion, "Page_Load", ex);
        }
    }

    protected void LlenarCombos()
    {
        // Llena el DDL de la periodicidad
        List<Xpinn.Comun.Entities.Cierea> lstCiereaFecha = new List<Xpinn.Comun.Entities.Cierea>();
        Xpinn.Comun.Services.CiereaService CiereaServicio = new Xpinn.Comun.Services.CiereaService();
        Configuracion conf = new Configuracion();
        lstCiereaFecha = CiereaServicio.ListarCiereaFechaComp("U", "D", (Usuario)Session["Usuario"]);
        if (lstCiereaFecha.Count == 0)
        {
            VerError("No existen periodos de causaciòn pendientes de contabilizar");
            btnComprobante.Enabled = false;
            btnInforme.Enabled = false;
            btnValidar.Enabled = false;
            return;
        }
        txtFechaIni.DataSource = lstCiereaFecha;
        txtFechaIni.DataTextFormatString = "{0:" + conf.ObtenerFormatoFecha() + "}";
        txtFechaIni.DataTextField = "FECHA";
        txtFechaIni.DataValueField = "FECHA";
        txtFechaIni.DataBind();
    }


    /// <summary>
    /// Método para guardar los datos y generar el comprobante
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {

            VerError("");
            // Validar que el proceso se halla realizado definitivo
            Xpinn.Comun.Entities.Cierea ecierea = serviceCalifica.ConsultarControlCierres(Convert.ToDateTime(txtFechaIni.Text), "U", (Usuario)Session["Usuario"]);
            if (ecierea == null)
            {
                VerError("No se ha realizado proceso de causación de cartera para la fecha " + txtFechaIni.Text);
                return;
            }
            if (ecierea.estado == null)
            {
                VerError("No se ha realizado proceso de causación de cartera para la fecha " + txtFechaIni.Text);
                return;
            }
            if (ecierea.estado.Trim() != "D")
            {
                VerError("No se ha realizado proceso de causación de cartera definitivo para la fecha " + txtFechaIni.Text);
                return;
            }
            if (ecierea.campo1.Trim() != "")
            {
                VerError("Ya se genero el comprobante de proceso de causación de cartera definitivo para la fecha " + txtFechaIni.Text + " Num.Comp:" + ecierea.campo1 + " Tipo:" + ecierea.campo2);
                return;
            }
            // Validar que exista la parametrización contable por procesos
            if (ValidarProcesoContable(ConvertirStringToDate(txtFechaIni.Text), _tipoOpe) == false)
            {
                VerError("No se encontró parametrización contable por procesos para el tipo de operación " + _tipoOpe + " = Causación");
                return;
            }
            // Determinar código de proceso contable para generar el comprobante
            Int64? rpta = 0;
            if (!panelProceso.Visible && panelGeneral.Visible)
            {
                rpta = ctlproceso.Inicializar(_tipoOpe, ConvertirStringToDate(txtFechaIni.Text), (Usuario)Session["Usuario"]);
                if (rpta > 1)
                {
                    Site toolBar = (Site)Master;
                    toolBar.MostrarGuardar(false);
                    // Activar demás botones que se requieran
                    panelGeneral.Visible = false;
                    panelProceso.Visible = true;
                }
                else
                {
                    // Crear la tarea de ejecución del proceso                
                    if (AplicarDatos())
                    {
                        Session["OrigenCierre"] = true;
                        Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                    }
                    else
                    {
                        //      VerError("Se presentó error"); reescribia los errores entonces se cambio
                    }
                }
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceCalifica.CodigoProgramaComprobanteCausacion, "btnGuardar_Click", ex);
        }
    }

    protected bool AplicarDatos()
    {
        Usuario usuario = (Usuario)Session["Usuario"];
        // Generar comprobante
        Int64 num_comp = 0, tipo_comp = 0;
        String Error = "";
        Int64 cod_proceso = Convert.ToInt64(ctlproceso.cod_proceso);
        Xpinn.Contabilidad.Services.ComprobanteService comprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
        if (!comprobanteServicio.GenerarComprobante(0, _tipoOpe, Convert.ToDateTime(txtFechaIni.Text), usuario.cod_oficina, 0, cod_proceso, ref num_comp, ref tipo_comp, ref Error, usuario))
        {
            VerError(Error);
            return false;
        }
        if (Error.Trim() != "")
        {
            VerError(Error);
            return false;
        }
        // Se cargan las variables requeridas para generar el comprobante
        Session[comprobanteServicio.CodigoPrograma + ".num_comp"] = num_comp;
        Session[comprobanteServicio.CodigoPrograma + ".tipo_comp"] = tipo_comp;
        ctlproceso.CargarVariables(0, _tipoOpe, -1, usuario);
        return true;
    }


    /// <summary>
    /// Método para generar el informe de causación
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInforme_Click(object sender, EventArgs e)
    {
        lblTotRegs.Text = "";

        List<CausacionCartera> listadetalle = new List<CausacionCartera>();
        List<CausacionCartera> listacalifica = new List<CausacionCartera>();
        DateTime _fecha = ConvertirStringToDate(txtFechaIni.SelectedItem.Text);
        listadetalle = serviceCalifica.ListarDetalleCausacion(_fecha.ToString(gFormatoFecha), (Usuario)Session["Usuario"]);
        listacalifica = serviceCalifica.ListarConsolidadoCausacion(_fecha.ToString(gFormatoFecha), (Usuario)Session["Usuario"]);

        UpdatePanel3.Visible = true;
        Session["Detallado"] = listadetalle;
        Session["Consolidado"] = listacalifica;
        gvConsolidado.DataSource = listacalifica;
        gvConsolidado.DataBind();
        gvdetallado.DataSource = listadetalle;
        gvdetallado.DataBind();

        if (listacalifica.Count <= 0)
            lblTotRegs.Text = "No se encontraron registros de causación para la fecha dada";
    }

    /// <summary>
    /// Método para exportar los datos a excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportarExcel_Click(object sender, EventArgs e)
    {
        if (gvConsolidado.Rows.Count > 0)
        {
            // Cambiar la grilla para que muestre todos los registros
            List<CausacionCartera> listadetalle = (List<CausacionCartera>)Session["Detallado"];
            gvdetallado.AllowPaging = false;
            gvdetallado.DataSource = listadetalle;
            gvdetallado.DataBind();
            // Descargar los registros a excel
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvdetallado.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvdetallado);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
            // Volver a dejar la grilla como estaba
            gvdetallado.AllowPaging = true;
            gvdetallado.DataSource = listadetalle;
            gvdetallado.DataBind();
        }
        else
            VerError("Se debe generar el reporte primero");

    }

    /// <summary>
    /// Método para exportar a excel el informe detallado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (gvdetallado.Rows.Count > 0)
        {
            // Cambiar la grilla para que muestre todos los registros
            List<CausacionCartera> listacalifica = (List<CausacionCartera>)Session["Consolidado"];
            gvConsolidado.AllowPaging = false;
            gvConsolidado.DataSource = listacalifica;
            gvConsolidado.DataBind();
            // Descargar los registros a excel
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvConsolidado.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvConsolidado);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
            // Volver a dejar la grilla como estaba
            gvConsolidado.AllowPaging = true;
            gvConsolidado.DataSource = listacalifica;
            gvConsolidado.DataBind();
        }
        else
            VerError("Se debe generar el reporte primero");
    }

    protected void btnCancelarProceso_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarCancelar(true);
        toolBar.MostrarConsultar(true);
        panelGeneral.Visible = true;
        panelProceso.Visible = false;
    }

    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            panelGeneral.Visible = true;
            panelProceso.Visible = false;
            // Aquí va la función que hace lo que se requiera grabar en la funcionalidad
            AplicarDatos();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void btnValidar_Click(object sender, EventArgs e)
    {
        try
        {
            panelErrores.Visible = true;
            UpdatePanel3.Visible = false;
            List<String> lstErrores = serviceCalifica.ListarErroresParametrizacionCausa(3, (Usuario)Session["Usuario"]);
            if (lstErrores != null)
            {
                if (lstErrores.Count > 0)
                {
                    gvErrores.DataSource = lstErrores;
                    gvErrores.DataBind();
                    int i = 0;
                    foreach (GridViewRow gFila in gvErrores.Rows)
                    {
                        TextBox txtDato = (TextBox)gFila.FindControl("txtDato");
                        if (txtDato != null)
                            txtDato.Text = lstErrores[i];
                        i += 1;
                    }
                    UpdatePanel3.Visible = false;
                    panelErrores.Visible = true;
                    lblErrores.Text = "Errores de parametrización";
                    return;
                }
                else
                {
                    lblErrores.Text = "No existen errores en la parametrizacion";
                    UpdatePanel3.Visible = false;
                    panelErrores.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }



}