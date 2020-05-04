using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Linq;
using Xpinn.Util;
using System.Web;
using Xpinn.Caja.Services;
using Xpinn.Contabilidad.Entities;
using Xpinn.Reporteador.Entities;
using Xpinn.Reporteador.Services;
using Xpinn.Tesoreria.Entities;

public partial class Lista : GlobalWeb
{


    AFIANCOLService afiancolService = new AFIANCOLService();
    private Operacion pOperacion = new Operacion();
    private int tipo_ope = 146;
    private string error = "";

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(afiancolService.CodigoProgramaReporteAfiancol, "L");
            Site toolBar = (Site)Master;
            toolBar.eventoConsultar += BtnConsultar_CLik;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoGuardar += BtnAplicar_Click;
            toolBar.MostrarExportar(false);
            toolBar.MostrarGuardar(false);
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(afiancolService.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            List<FechaCorte> lst = afiancolService.ListarFecha((Usuario)Session["Usuario"]).OrderByDescending(x => x.Fecha).ToList();

            ddlFechas.DataSource = lst;
            ddlFechas.DataTextField = "Fecha";
            ddlFechas.DataValueField = "Fecha";
            ddlFechas.DataBind();
        }
    }

    private void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        try
        {
            DataTable dtResultado = afiancolService.ListarReporte((Usuario)Session["Usuario"], DateTime.Parse(ddlFechas.SelectedValue)).ToDataTable();
            if (dtResultado.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                GridView dg = new GridView();
                dg.AllowPaging = false;
                dg.DataSource = dtResultado;
                dg.DataBind();
                dg.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(dg);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename= ReporteAfiancol " + DateTime.Parse(ddlFechas.SelectedValue).Date + ".xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                //Response.End();
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
                VerError("No se encontrarón registros para exportar");
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    private void BtnAplicar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        // Validar que exista la parametrización contable por procesos
        if (ValidarProcesoContable(DateTime.Now, tipo_ope) == false)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación XX = XXXXXXX");
            return;
        }
        // Determinar código de proceso contable para generar el comprobante
        Int64? rpta = 0;
        if (!panelProceso.Visible && panelGeneral.Visible)
        {
            rpta = ctlproceso.Inicializar(tipo_ope, DateTime.Now, (Usuario)Session["Usuario"]);
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
                    Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
            }
        }

    }

    private void BtnConsultar_CLik(object sender, ImageClickEventArgs e)
    {
        List<AFIANCOL_Reporte> dtResultado = new List<AFIANCOL_Reporte>();
        Site toolBar = (Site)Master;
        dtResultado.Clear();
        gvLista.DataSource = dtResultado;
        gvLista.DataBind();

        if (afiancolService.ValidarList((Usuario)Session["Usuario"], DateTime.Parse(ddlFechas.SelectedValue)).Validar == 0)
        {
            if (afiancolService.LlenarTablaAfiancol((Usuario)Session["Usuario"], DateTime.Parse(ddlFechas.SelectedValue)))
            {
                dtResultado = afiancolService.ListarReporte((Usuario)Session["Usuario"], DateTime.Parse(ddlFechas.SelectedValue));
            }
            toolBar.MostrarGuardar(true);
            toolBar.MostrarExportar(true);
        }
        else
        {
            dtResultado = afiancolService.ListarReporte((Usuario)Session["Usuario"], DateTime.Parse(ddlFechas.SelectedValue));
            toolBar.MostrarGuardar(false);
            toolBar.MostrarExportar(true);
        }
        gvLista.DataSource = dtResultado;
        gvLista.DataBind();

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

    protected bool AplicarDatos()
    {
        string error = "";
        //Consultar proceso contable 
        ProcesoContable pro = new ProcesoContable();
        pro = ConsultarProcesoContable(tipo_ope, ref error, (Usuario)Session["usuario"]);
        // CREAR OPERACION
        pOperacion.cod_ope = 0;
        pOperacion.tipo_ope = tipo_ope;
        pOperacion.cod_caja = 0;
        pOperacion.cod_cajero = 0;
        pOperacion.observacion = "Operacion-Cuentas Por Pagar Afiancol";
        pOperacion.cod_proceso = pro.cod_proceso;
        pOperacion.fecha_oper = DateTime.Now;
        pOperacion.fecha_calc = DateTime.Now;
        pOperacion.cod_ofi = ((Usuario)Session["usuario"]).cod_oficina;
        Int64 num_comp = 0;
        int tipo_comp = 0;


        Operacion causar = afiancolService.CausarAfiancol(pOperacion, DateTime.Parse(ddlFechas.SelectedValue), ref num_comp, ref tipo_comp, ref error,
            (Usuario)Session["usuario"]);
        if (error.Trim() != "" || num_comp == 0)
        {
            VerError("No se pudo generar el comprobante de causación de AFIANCOL. Error:" + error);
            return false;
        }

        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
        Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = num_comp;
        Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = tipo_comp;

        if (causar == null) return false;
        // Se cargan las variables requeridas para generar el comprobante
        ctlproceso.CargarVariables(causar.cod_ope, tipo_ope, null, (Usuario)Session["usuario"]);
        return true;
    }

}