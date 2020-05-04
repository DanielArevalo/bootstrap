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

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[serviceCalifica.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(serviceCalifica.CodigoPrograma, "E");
            else
                VisualizarOpciones(serviceCalifica.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceCalifica.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                UpdatePanel3.Visible = false;
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceCalifica.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            panelGeneral.Visible = true;
            panelProceso.Visible = false;
            GenerarComprobanteClasificacion();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void btnCancelarProceso_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        panelGeneral.Visible = true;
        panelProceso.Visible = false;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        // Validar que el proceso se halla realizado definitivo
        Xpinn.Comun.Entities.Cierea ecierea = serviceCalifica.ConsultarControlCierres(Convert.ToDateTime(txtFechaIni.Text), "R", (Usuario)Session["Usuario"]);
        if (ecierea == null)
        {
            VerError("No se ha realizado proceso de cierre histórico de cartera para la fecha " + txtFechaIni.Text);
            return;
        }
        if (ecierea.estado == null)
        {
            VerError("No se ha realizado proceso de cierre histórico de cartera para la fecha " + txtFechaIni.Text);
            return;
        }
        if (ecierea.estado.Trim() != "D")
        {
            VerError("No se ha realizado proceso de cierre histórico de cartera definitivo para la fecha " + txtFechaIni.Text);
            return;
        }
        if (ecierea.campo1.Trim() != "")
        {
            VerError("Ya se genero el comprobante de proceso de cierre histórico de cartera definitivo para la fecha " + txtFechaIni.Text + " Num.Comp:" + ecierea.campo1 + " Tipo:" + ecierea.campo2);
            return;
        }
        // Validar que exista la parametrización contable por procesos
        if (ValidarProcesoContable(Convert.ToDateTime(txtFechaIni.Text), 37) == false)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación 37 = Clasificación de Cartera");
            return;
        }
        // Determinar código de proceso contable para generar el comprobante
        Int64? rpta = 0;
        if (!panelProceso.Visible && panelGeneral.Visible)
        {
            rpta = ctlproceso.Inicializar(37, Convert.ToDateTime(txtFechaIni.Text), (Usuario)Session["Usuario"]);
            if (rpta > 1)
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                panelGeneral.Visible = false;
                panelProceso.Visible = true;
            }
            else
            {
                if (GenerarComprobanteClasificacion())
                {
                    Session["OrigenCierre"] = true;
                    Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                }
            }
        }
    }

    protected bool GenerarComprobanteClasificacion()
    {
        try
        {
            //Session["FechaComprobante"] = txtFechaIni.Text;
            //Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            //Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = 0;
            //Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 37;
            //Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = Convert.ToDateTime(txtFechaIni.Text);
            //Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = -1;
            //Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] = Session["Oficina"];
            //Session["OrigenComprobante"] = "../../Cartera/ComprobanteClasificacion/ClasificacionCartera.aspx";      
            Usuario usuario = (Usuario)Session["Usuario"];
            // Generar comprobante
            Int64 num_comp = 0, tipo_comp = 0;
            String Error = "";
            Int64 cod_proceso = Convert.ToInt64(ctlproceso.cod_proceso);
            Xpinn.Contabilidad.Services.ComprobanteService comprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            if (!comprobanteServicio.GenerarComprobante(0, 37, Convert.ToDateTime(txtFechaIni.Text), usuario.cod_oficina, 0, cod_proceso, ref num_comp, ref tipo_comp, ref Error, usuario))
            {
                VerError("No se pudo generar el comprobante. " + Error.Trim());
                return false;
            }
            if (Error.Trim() != "")
            {
                VerError("Error:" + Error);
                return false;
            }
            // Se cargan las variables requeridas para generar el comprobante
            Session[comprobanteServicio.CodigoPrograma + ".tipo_ope"] = 37;
            Session[comprobanteServicio.CodigoPrograma + ".num_comp"] = num_comp;
            Session[comprobanteServicio.CodigoPrograma + ".tipo_comp"] = tipo_comp;
            ctlproceso.CargarVariables(null, 37, null, usuario);
            return true;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceCalifica.CodigoPrograma, "GenerarComprobanteClasificacion", ex);
            return false;
        }
    }

    /// <summary>
    /// Método para generar datos del informe
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInforme_Click(object sender, EventArgs e)
    {
        List<Xpinn.Cartera.Entities.ClasificacionCartera> listadetalle = new List<ClasificacionCartera>();
        List<Xpinn.Cartera.Entities.ClasificacionCartera> listacalifica = new List<ClasificacionCartera>();

        listadetalle = serviceCalifica.ListarDetalle(txtFechaIni.Text, (Usuario)Session["Usuario"]);
        listacalifica = serviceCalifica.ListarConsolidado(txtFechaIni.Text, (Usuario)Session["Usuario"]);

        UpdatePanel3.Visible = true;
        Session["Detallado"] = listadetalle;
        Session["Consolidado"] = listacalifica;
        gvConsolidado.DataSource = listacalifica;
        gvConsolidado.DataBind();
        gvdetallado.DataSource = listadetalle;
        gvdetallado.DataBind();
    }

    protected void btnExportarExcelCon_Click(object sender, EventArgs e)
    {
        if (gvConsolidado.Rows.Count > 0)
        {
            // Cambiar la grilla para que muestre todos los registros
            List<ClasificacionCartera> listacalifica = new List<ClasificacionCartera>();
            listacalifica = (List<ClasificacionCartera>)Session["Consolidado"];
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

    protected void btnExportarExcel_Click(object sender, EventArgs e)
    {
        if (gvdetallado.Rows.Count > 0)
        {
            // Cambiar la grilla para que muestre todos los registros
            List<ClasificacionCartera> listadetalle = new List<ClasificacionCartera>();
            listadetalle = (List<ClasificacionCartera>)Session["Detallado"];
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
            Response.AddHeader("Content-Disposition", "attachment;filename=ClasificacionCartera.xls");
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

    protected void btnValidar_Click(object sender, EventArgs e)
    {
        try
        {
            panelErrores.Visible = true;
            UpdatePanel3.Visible = false;
            List<String> lstErrores = serviceCalifica.ListarErroresParametrizacionClasif(1, (Usuario)Session["Usuario"]);
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