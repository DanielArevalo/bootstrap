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
    private Xpinn.Cartera.Services.ProvisionGeneralService serviceProGeneral = new Xpinn.Cartera.Services.ProvisionGeneralService();
    int TipoOpe = 66;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[serviceProGeneral.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(serviceProGeneral.CodigoPrograma, "E");
            else
                VisualizarOpciones(serviceProGeneral.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceProGeneral.CodigoPrograma, "Page_PreInit", ex);
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
            BOexcepcion.Throw(serviceProGeneral.CodigoPrograma, "Page_Load", ex);
        }
    }

    /// <summary>
    /// Método para guardar los datos y generar el comprobante
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        try
        {
            // Validar que exista la parametrización contable por procesos
            if (ValidarProcesoContable(Convert.ToDateTime(txtFechaIni.Text), TipoOpe) == false)
            {
                VerError("No se encontró parametrización contable por procesos para el tipo de operación " + TipoOpe.ToString() + " = Provisión General");
                return;
            }
            // Determinar código de proceso contable para generar el comprobante            
            if (!panelProceso.Visible && panelGeneral.Visible)
            {
                Int64? rpta = 0;
                rpta = ctlproceso.Inicializar(TipoOpe, Convert.ToDateTime(txtFechaIni.Text), (Usuario)Session["Usuario"]);
                if (rpta > 1)
                {
                    // Si existen varios registros parametrizados para el proceso ir a la pantalla de escoger
                    Site toolBar = (Site)Master;
                    toolBar.MostrarGuardar(false);
                    panelGeneral.Visible = false;
                    panelProceso.Visible = true;
                }
                else
                {
                    if (GenerarProvision())
                    {
                        Session["OrigenCierre"] = true;
                        Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            VerError("Se presentó error al generar el comprobante. Error: " + ex.Message);
        }
    }

    protected bool GenerarProvision()
    {
        VerError("");
        try
        {
            // Cargar los datos de la provisión a un list
            List<ProvisionGeneral> LsDetalle = new List<ProvisionGeneral>();
            if (Session["Detallado"] == null)
            {
                VerError("No se ha generado el proceso");
                return false;
            }
            LsDetalle = (List<ProvisionGeneral>)Session["Detallado"]; 
            if (LsDetalle.Count() <= 0)
            {   
                VerError("No hay registros para generar la provisión general");
                return false;
            }                       
            // Realizar el proceso
            if (serviceProGeneral.GuardarProvisionGeneral(Convert.ToDateTime(txtFechaIni.Text), LsDetalle, (Usuario)Session["Usuario"]))
            {
                // Se cargan las variables requeridas para generar el comprobante
                Session["OrigenComprobante"] = "../../Cartera/ComprobanteProvisionGeneral/ProvisionCartera.aspx";
                Int64? cod_oficina;
                if (Session["Oficina"] != null)
                    cod_oficina = (Int64)Session["Oficina"];
                else
                    cod_oficina = ((Usuario)Session["Usuario"]).codusuario;
                ctlproceso.CargarVariables(0, TipoOpe, Convert.ToDateTime(txtFechaIni.Text), -1, cod_oficina, (Usuario)Session["Usuario"]);                
                return true;
            }
        }
        catch (Exception ex)
        {
            VerError("Se presentó error al generar provisión general. Error:" + ex.Message);
            return false;
        }
        return true;
    }

    /// <summary>
    /// Método para generar el informe de comprobante de causación
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInforme_Click(object sender, EventArgs e)
    {
        lblTotRegs.Text = "";
        List<ProvisionGeneral> listadetalle = new List<ProvisionGeneral>();
        listadetalle = serviceProGeneral.ProvisionGeneral(ConvertirStringToDate(txtFechaIni.Text), (Usuario)Session["Usuario"]);
        UpdatePanel3.Visible = true;
        Session["Detallado"] = listadetalle;
        gvdetallado.DataSource = listadetalle;
        gvdetallado.DataBind();
        if (listadetalle.Count <= 0)
           lblTotRegs.Text = "No se encontraron registros de provisión general para la fecha dada";
        else
            lblTotRegs.Text = "Se encontraron " + listadetalle.Count.ToString() + " registros";
    }

  
    /// <summary>
    /// Método para generar el archivo a EXCEL.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportarExcel_Click(object sender, EventArgs e)
    {
        if (gvdetallado.Rows.Count > 0)
        {
            // Cambiar la grilla para que muestre todos los registros
            List<ProvisionGeneral> listadetalle = new List<ProvisionGeneral>();
            listadetalle = (List<ProvisionGeneral>)Session["Detallado"];
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
            Response.AddHeader("Content-Disposition", "attachment;filename=ProvisionGeneral.xls");
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
    /// Si no se continua con el proceso contable.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelarProceso_Click(object sender, EventArgs e)
    {
        // Se activa botón de guardar y se regresa al panel general que es donde estan los datos
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        panelGeneral.Visible = true;
        panelProceso.Visible = false;
    }

    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        // Se continua con el proceso. Este botón en el control tiene una función delegada en donde genera el comprobante.
        try
        {
            panelGeneral.Visible = true;
            panelProceso.Visible = false;
            GenerarProvision();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


}