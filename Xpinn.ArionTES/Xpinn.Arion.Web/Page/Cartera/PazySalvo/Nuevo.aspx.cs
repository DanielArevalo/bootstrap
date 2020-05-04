using System;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Services;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Design;
using System.Threading;
using System.Data;
using Microsoft.Reporting.WebForms;

partial class Nuevo : GlobalWeb
{
    PazySalvoService PazySalvoService = new PazySalvoService();
    Credito CreditoServicio = new Credito();

    /// <summary>
    /// Mostrar la barra de herramientas al ingresar a la funcionalidad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[PazySalvoService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(PazySalvoService.CodigoPrograma, "E");
            else
                VisualizarOpciones(PazySalvoService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlFormatos.eventoClick += btnImpresion_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PazySalvoService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    /// <summary>
    /// Cargar datos de la funcionalidad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.ctlFormatos);

            if (!IsPostBack)
            {
                CargarDropDown();
                if (Session[PazySalvoService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[PazySalvoService.CodigoPrograma + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                    mvCertificado.ActiveViewIndex = 0;
                    txtFechaCertificado.ToDateTime = System.DateTime.Now;
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PazySalvoService.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void CargarDropDown()
    {
        //CARGANDO DOCUMENTOS PERTENECIENTES A PAZ Y SALVO
        ctlFormatos.Inicializar("3");
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        ctlMensaje.MostrarMensaje("Desea generar la certificación?");
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        ctlFormatos.lblErrorText = "";
        if (ctlFormatos.ddlFormatosItem == null)
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            // ---------------------------------------------------------------------------------------------------------
            // Pasar datos al reporte
            // ---------------------------------------------------------------------------------------------------------
            ReportParameter[] param = new ReportParameter[10];

            param[0] = new ReportParameter("Entidad", pUsuario.empresa);
            param[1] = new ReportParameter("Numero_Radicacion", txtNumero_radicacion.Text);
            param[2] = new ReportParameter("FechaCertificacion", txtFechaCertificado.Texto);
            param[3] = new ReportParameter("Nombre", txtNombre.Text);
            param[4] = new ReportParameter("Identificacion", txtIdentificacion.Text);
            param[5] = new ReportParameter("FechaApertura", txtFechaAprobacion.Text);
            if (txtFechaUltimoPago.Text.Trim() == "")
                param[6] = new ReportParameter("FechaUltimoPago", System.DateTime.Now.ToString());
            else
                param[6] = new ReportParameter("FechaUltimoPago", txtFechaUltimoPago.Text);
            param[7] = new ReportParameter("SaldoCapital", txtSaldoCapital.Text);
            param[8] = new ReportParameter("UsuarioElabora", pUsuario.nombre);
            param[9] = new ReportParameter("ImagenReport", ImagenReporte());

            rvCertificado.LocalReport.EnableExternalImages = true;
            rvCertificado.LocalReport.SetParameters(param);

            rvCertificado.LocalReport.DataSources.Clear();
            rvCertificado.LocalReport.Refresh();

            // Mostrar el reporte en pantalla.
            mvCertificado.ActiveViewIndex = 2;
        }
        else
        {
            if (ctlFormatos.ddlFormatosItem != null)
                ctlFormatos.ddlFormatosIndex = 0;
            ctlFormatos.MostrarControl();
        }
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {

    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
        return;
    }

    /// <summary>
    /// Evento para cancelar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    /// <summary>
    /// Mostrar los datos del crédito
    /// </summary>
    /// <param name="pIdObjeto"></param>
    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            CreditoService CreditoServicio = new CreditoService();
            Credito vCredito = new Credito();
            vCredito = CreditoServicio.ConsultarCredito(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vCredito.numero_radicacion != Int64.MinValue)
                txtNumero_radicacion.Text = vCredito.numero_radicacion.ToString().Trim();
            if (vCredito.identificacion != string.Empty)
                txtIdentificacion.Text = HttpUtility.HtmlDecode(vCredito.identificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.tipo_identificacion))
                txtTipo_identificacion.Text = HttpUtility.HtmlDecode(vCredito.tipo_identificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.nombre))
                txtNombre.Text = HttpUtility.HtmlDecode(vCredito.nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.linea_credito))
                txtLinea_credito.Text = HttpUtility.HtmlDecode(vCredito.linea_credito.ToString().Trim());
            if (vCredito.monto != Int64.MinValue)
                txtMonto.Text = HttpUtility.HtmlDecode(vCredito.monto.ToString().Trim());
            if (vCredito.plazo != Int64.MinValue)
                txtPlazo.Text = HttpUtility.HtmlDecode(vCredito.plazo.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.periodicidad))
                txtPeriodicidad.Text = HttpUtility.HtmlDecode(vCredito.periodicidad.ToString().Trim());
            if (vCredito.valor_cuota != Int64.MinValue)
                txtValor_cuota.Text = HttpUtility.HtmlDecode(vCredito.valor_cuota.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.forma_pago))
                txtForma_pago.Text = HttpUtility.HtmlDecode(vCredito.forma_pago.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.estado))
                if (vCredito.estado == "A")
                    txtEstado.Text = "Aprobado";
                else if ((vCredito.estado == "G"))
                    txtEstado.Text = "Generado";
                else if ((vCredito.estado == "C"))
                    txtEstado.Text = "Desembolsado";
                else if ((vCredito.estado == "T"))
                    txtEstado.Text = "Terminado";
                else
                    txtEstado.Text = vCredito.estado;
            if (!string.IsNullOrEmpty(vCredito.moneda))
                txtMoneda.Text = HttpUtility.HtmlDecode(vCredito.moneda.ToString().Trim());
            if (vCredito.saldo_capital != Int64.MinValue)
                txtSaldoCapital.Text = HttpUtility.HtmlDecode(vCredito.saldo_capital.ToString().Trim());
            if (vCredito.fecha_prox_pago != DateTime.MinValue)
                txtFechaProximoPago.Text = HttpUtility.HtmlDecode(vCredito.fecha_prox_pago.ToString(GlobalWeb.gFormatoFecha).Trim());
            if (vCredito.fecha_ultimo_pago != DateTime.MinValue)
                txtFechaUltimoPago.Text = HttpUtility.HtmlDecode(vCredito.fecha_ultimo_pago.ToString(GlobalWeb.gFormatoFecha).Trim());
            if (vCredito.fecha_aprobacion != DateTime.MinValue)
                txtFechaAprobacion.Text = HttpUtility.HtmlDecode(Convert.ToDateTime(vCredito.fecha_aprobacion).ToString(GlobalWeb.gFormatoFecha).Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PazySalvoService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void btnImpresion_Click(object sender, EventArgs e)
    {
        try
        {
            // ELIMINANDO ARCHIVOS GENERADOS
            try
            {
                string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Documentos\\"));
                foreach (string ficheroActual in ficherosCarpeta)
                    File.Delete(ficheroActual);
            }
            catch
            { }
            string pRuta = "/Documentos/";
            string pVariable = txtNumero_radicacion.Text.Trim();
            //Cuando se envia numero credito origen es 0
            string origen ="0";
            if (!ctlFormatos.ImprimirFormato(pVariable, pRuta,origen))
                return;

            //Descargando el Archivo PDF
            string cNombreDeArchivo = pVariable.Trim() + "_" + ctlFormatos.ddlFormatosValue + ".pdf";
            string cRutaLocalDeArchivoPDF = Server.MapPath("/Documentos\\" + cNombreDeArchivo);
            FileInfo file = new FileInfo(cRutaLocalDeArchivoPDF);
            var sjs = File.ReadAllBytes(cRutaLocalDeArchivoPDF);
            Response.AddHeader("Content-Disposition", "attachment; filename= PazYSalvo.pdf");
            Response.AddHeader("Content-Length", sjs.Length.ToString());
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(sjs);
            Response.Flush();
            Response.Close();

            try
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
            }
            catch (Exception exception)
            {
            }

        }
        catch (Exception ex)
        {
            ctlFormatos.lblErrorIsVisible = true;
            ctlFormatos.lblErrorText = ex.Message;
        }

    }


    protected void btnVerData_Click(object sender, EventArgs e)
    {
        panelReporte.Visible = false;
        mvCertificado.ActiveViewIndex = 0;
    }


}