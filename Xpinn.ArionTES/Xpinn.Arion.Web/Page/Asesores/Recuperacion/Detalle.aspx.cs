using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.IO;
using System.Web.Script.Services;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using Xpinn.Util;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
//using ListItem = System.Web.UI.WebControls.ListItem;

public partial class Detalle : GlobalWeb
{
    System.Net.WebClient webClient = new System.Net.WebClient();
    private ClienteService clienteServicio = new ClienteService();
    private CreditosService creditoServicio = new CreditosService();
    private DiligenciaService diligenciaServicio = new DiligenciaService();
    private ProcesosCobroService procesosCobroServicio = new ProcesosCobroService();
    private AtributoService atributoServicio = new AtributoService();
    private Int64 codProceso, codUsuario, codAbogado, codMotivo, codciudadproceso;
    private String IdCodeudor, TipoDocumento, NumeroDocumento, PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido, Direccion, Telefono, Barrio, Email;

    string pError = "";
    [ThemeableAttribute(false)]
    public virtual bool UseSubmitBehavior { get; set; }

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        /*try
        {
            txtObservacion.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
            txtFecha_acuerdo.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
            txtValor_acuerdo.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
            txtAtendio.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
            txtRespuesta.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
        }
        catch { }*/
        VisualizarOpciones(clienteServicio.CodigoProgramaRealRecuperacionesDetalle, "D");
        Site toolBar = (Site)this.Master;
        toolBar.eventoRegresar += btnRegresar_Click;
        Configuracion conf = new Configuracion();
        txtFecha_diligencia.Text = DateTime.Now.ToString(conf.ObtenerFormatoFecha());
        txtFecha_diliConsulta.Text = DateTime.Now.ToString(conf.ObtenerFormatoFecha());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        String radicado = "";
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.ddlTipoDoc);
        try
        {
            if (!Page.IsPostBack)
            {

                LlenarComboTipo();
                LlenarComboTipoDiligenciaConsulta();
                LlenarComboTipoContactoConsulta();
                LlenarComboUsuarios();
                LlenarComboZona();
                LlenarComboCiudadJuzgado();
                LlenarComboCiudadJuzgadopage();
                LlenarComboTipoContacto();
                LlenarTiposDocumento();

                this.txtobservaciones.Text = "0";

                if (Session[clienteServicio.CodigoPrograma + ".id"] != null && Session[creditoServicio.CodigoPrograma + ".id"] != null)
                {
                    radicado = Request["radicado"];
                    if (radicado == null)
                    {
                        // Cargar datos del cliente y del crédito
                        idObjeto = Session[clienteServicio.CodigoPrograma + ".id"].ToString();
                        Session.Remove(clienteServicio.CodigoPrograma + ".id");
                        ObtenerDatosCliente(idObjeto);
                        idObjeto = Session[creditoServicio.CodigoPrograma + ".id"].ToString();
                        Session.Remove(creditoServicio.CodigoPrograma + ".id");
                        ObtenerDatosCredito(idObjeto);
                        ObtenerDatosCodeudor(idObjeto);

                        //* error cuando no hay datos en cobros_credito
                        ObtenerDatosProceso(idObjeto);
                        ObtenerDatosProcesoAbogados(idObjeto);
                        Codeudores();
                        Diligencias();
                        Atributos();
                        Actualizar(idObjeto);

                        lblCambiarArchivo.Visible = true;
                        txtFecha_diligencia.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        txtFecha_diliConsulta.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        chkCambiarArchivoConsulta.Checked = false;
                    }
                    else
                    {
                        idObjeto = radicado;
                        ObtenerDatosClienterepo(idObjeto);
                        idObjeto = radicado;
                        ObtenerDatosCredito(idObjeto);
                        ObtenerDatosCodeudor(idObjeto);
                        ObtenerDatosProceso(idObjeto);
                        ObtenerDatosProcesoAbogados(idObjeto);
                        Codeudores();
                        Diligencias();
                        Atributos();
                        Actualizar(idObjeto);

                        lblCambiarArchivo.Visible = true;
                        txtFecha_diligencia.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        txtFecha_diliConsulta.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        chkCambiarArchivoConsulta.Checked = false;
                    }
                }

                radicado = Request["radicado"];
                if (radicado != null)
                {
                    idObjeto = radicado;
                    ObtenerDatosClienterepo(idObjeto);
                    idObjeto = radicado;
                    ObtenerDatosCredito(idObjeto);
                    ObtenerDatosCodeudor(idObjeto);
                    ObtenerDatosProceso(idObjeto);
                    ObtenerDatosProcesoAbogados(idObjeto);
                    Codeudores();
                    Diligencias();
                    Atributos();
                    Actualizar(idObjeto);
                    // Inicia;izar datos de la diligencia
                    lblCambiarArchivo.Visible = true;
                    txtFecha_diligencia.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFecha_diliConsulta.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    chkCambiarArchivoConsulta.Checked = false;
                }
                ListarGridViewCreditos();
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError("1." + ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.GetType().Name, "Page_Load", ex);
        }
    }

    void ListarGridViewCreditos()
    {
        Xpinn.FabricaCreditos.Services.CreditoService BOCredito = new Xpinn.FabricaCreditos.Services.CreditoService();
        List<Xpinn.FabricaCreditos.Entities.Credito> lstCreditos = new List<Xpinn.FabricaCreditos.Entities.Credito>();
        List<Xpinn.FabricaCreditos.Entities.Credito> lstCred = new List<Xpinn.FabricaCreditos.Entities.Credito>();
        if (txtCodigoCliente.Text != "")
        {
            lstCreditos = BOCredito.ListarCreditoAsociados(Convert.ToInt64(txtCodigoCliente.Text), Convert.ToDateTime(txtFecha_diligencia.Text), (Usuario)Session["usuario"]);
            panelCreditos.Visible = false;
            if (lstCreditos.Count > 0)
            {
                foreach (Xpinn.FabricaCreditos.Entities.Credito rEntidad in lstCreditos)
                {
                    Xpinn.FabricaCreditos.Entities.Credito nCredi = new Xpinn.FabricaCreditos.Entities.Credito();
                    if (rEntidad.numero_radicacion != Convert.ToInt64(txtIdCredito.Text))
                    {
                        nCredi = rEntidad;
                        lstCred.Add(nCredi);
                    }
                }
                panelCreditos.Visible = true;
                gvCreditos.DataSource = lstCred;
                gvCreditos.DataBind();
            }
        }
        else //Validacion agregada ya que se estaba generando error en el evento del boton regresar del navegador
            Navegar("~/Page/Asesores/Recuperacion/Lista.aspx");
    }

    private Boolean VerificarParametroHabeasData()
    {
        Boolean continuar = true;

        Creditos credito = new Creditos();
        credito = creditoServicio.ConsultarParametroHabeas((Usuario)Session["Usuario"]);
        Int64 dias_mora = Convert.ToInt32(txtdiasmora.Text);
        Int64 dias_mora_habeas = credito.dias_mora_param;
        if (dias_mora < dias_mora_habeas)
        {
            String Error = "No cumple los dias de mora  para generar archivo de Habeas Data";
            this.LblCartas.Text = Error;
            continuar = false;
        }
        else
        {
            this.LblCartas.Text = "";
        }
        return continuar;
    }

    private Boolean VerificarParametroCobroPreJuridico2()
    {
        Boolean continuar = true;


        Creditos credito = new Creditos();
        credito = creditoServicio.ConsultarParametroCobroPreJuridico2((Usuario)Session["Usuario"]);
        Int64 dias_mora = Convert.ToInt32(txtdiasmora.Text);
        Int64 dias_mora_param = credito.dias_mora_param;
        if (dias_mora < dias_mora_param)
        {
            String Error = "No cumple los dias de mora  para generar archivo de Cobro PreJurídico-2";
            this.LblCartas.Text = Error;

            continuar = false;
        }
        else
        {
            this.LblCartas.Text = "";
        }
        return continuar;
    }

    private Boolean VerificarParametroCampaña()
    {
        Boolean continuar = true;


        Creditos credito = new Creditos();
        credito = creditoServicio.ConsultarParametroCampaña((Usuario)Session["Usuario"]);
        Int64 dias_mora = Convert.ToInt32(txtdiasmora.Text);
        Int64 dias_mora_param = credito.dias_mora_param;
        if (dias_mora < dias_mora_param)
        {
            String Error = "No cumple los dias de mora  para generar archivo de Campaña";
            this.LblCartas.Text = Error;

            continuar = false;
        }
        else
        {
            this.LblCartas.Text = "";
        }
        return continuar;
    }

    private Boolean VerificarParametroCobroPreJuridico()
    {
        Boolean continuar = true;

        Creditos credito = new Creditos();
        credito = creditoServicio.ConsultarParametroCobroPreJuridico((Usuario)Session["Usuario"]);
        Int64 dias_mora = Convert.ToInt32(txtdiasmora.Text);
        Int64 dias_mora_param = credito.dias_mora_param;
        if (dias_mora < dias_mora_param)
        {
            String Error = "No cumple los dias de mora  para generar archivo de Cobro PreJurídico";
            this.LblCartas.Text = Error;

            continuar = false;
        }
        else
        {
            this.LblCartas.Text = "";
        }
        return continuar;
    }


    private Boolean VerificarParametroVisitaAbogado()
    {
        Boolean continuar = true;

        Creditos credito = new Creditos();
        credito = creditoServicio.ConsultarParametroVisitaAbogado((Usuario)Session["Usuario"]);
        Int64 dias_mora = Convert.ToInt32(txtdiasmora.Text);
        Int64 dias_mora_param = credito.dias_mora_param;
        if (dias_mora < dias_mora_param)
        {
            String Error = "No cumple los dias de mora  para generar archivo de Visita Abogado";
            this.LblCartas.Text = Error;

            continuar = false;
        }
        else
        {
            this.LblCartas.Text = "";
        }
        return continuar;
    }
    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {

        Navegar("~/Page/Asesores/Recuperacion/Lista.aspx");


    }


    protected void ObtenerDatosDiligencia(String pIdObjeto)
    {
        try
        {
            Xpinn.Asesores.Entities.Diligencia vDiligencia = new Xpinn.Asesores.Entities.Diligencia();
            vDiligencia = diligenciaServicio.ConsultarDiligenciaXcredito(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            txtNumero_radicacion.Text = this.txtIdCredito.Text;
            if (!string.IsNullOrEmpty(vDiligencia.anexo))
                if (!string.IsNullOrEmpty(vDiligencia.fecha_diligencia.ToString("dd/MM/yyyy").Trim()))
                    if (vDiligencia.tipo_diligencia != Int64.MinValue)
                        ddlTipoDiligencia.SelectedValue = HttpUtility.HtmlDecode(vDiligencia.tipo_diligencia_consulta.ToString().Trim());
            if (vDiligencia.tipo_contacto != Int64.MinValue)
                ddlTipoContacto.SelectedValue = HttpUtility.HtmlDecode(vDiligencia.tipo_contacto_consulta.ToString().Trim());
            if (!string.IsNullOrEmpty(vDiligencia.atendio))
                txtAtendio.Text = HttpUtility.HtmlDecode(vDiligencia.atendio.ToString().Trim());
            if (!string.IsNullOrEmpty(vDiligencia.respuesta))
                txtRespuesta.Text = HttpUtility.HtmlDecode(vDiligencia.respuesta.ToString().Trim());
            if (!string.IsNullOrEmpty(vDiligencia.fecha_acuerdo.ToString()))
                txtFecha_acuerdo.Text = HttpUtility.HtmlDecode(vDiligencia.fecha_acuerdo.ToString("dd/MM/yyyy").Trim());
            if (vDiligencia.valor_acuerdo != Int64.MinValue)
                txtValor_acuerdo.Text = HttpUtility.HtmlDecode(vDiligencia.valor_acuerdo.ToString().Trim());
            if (!string.IsNullOrEmpty(vDiligencia.observacion))
                txtObservacion.Text = HttpUtility.HtmlDecode(vDiligencia.observacion.ToString().Trim());
            if (vDiligencia.codigo_usuario_regis != Int64.MinValue)
                txtCodigo_usuario_regis.Text = HttpUtility.HtmlDecode(vDiligencia.codigo_usuario_regis.ToString().Trim());

            if (vDiligencia.acuerdo != Int64.MinValue)
            {
                if (vDiligencia.acuerdo == 1)
                    chkAprueba.Checked = true;
                else
                    chkAprueba.Checked = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(diligenciaServicio.CodigoPrograma, "ObtenerDatosDiligencia", ex);
        }
    }

    protected void ConsultarDatosDiligencia(Int64 pIdObjeto2)
    {

        try
        {
            Xpinn.Asesores.Entities.Diligencia vDiligencia = new Xpinn.Asesores.Entities.Diligencia();
            vDiligencia.codigo_diligencia = Convert.ToInt64(pIdObjeto2);
            vDiligencia.numero_radicacion = Convert.ToInt64(txtIdCredito.Text);
            vDiligencia = diligenciaServicio.ConsultarDiligenciaEntidad(vDiligencia, (Usuario)Session["usuario"]);

            if (vDiligencia.codigo_diligencia != Int64.MinValue)
                txtCodigo_diligencia.Text = vDiligencia.codigo_diligencia.ToString().Trim();
            if (vDiligencia.numero_radicacion != Int64.MinValue)
                txtNumero_radconsulta.Text = vDiligencia.numero_radicacion.ToString().Trim();
            if (!string.IsNullOrEmpty(vDiligencia.fecha_diligencia.ToString()))
                txtFecha_diliConsulta.Text = vDiligencia.fecha_diligencia.ToString("dd/MM/yyyy").Trim();
            if (vDiligencia.tipo_diligencia != Int64.MinValue)

                ddlTipoDiligenciaConsulta.SelectedValue = HttpUtility.HtmlDecode(vDiligencia.tipo_diligencia.ToString().Trim());

            if (ddlTipoDiligenciaConsulta.SelectedItem.Text == "Carta Habeas Data" || ddlTipoDiligenciaConsulta.SelectedItem.Text == "Carta Cobro Prejurídico" || ddlTipoDiligenciaConsulta.SelectedItem.Text == "Carta Cobro Jurídico")
            {
                ddlTipoDiligenciaConsulta.Enabled = false;
                ddlTipoContactoConsulta.Enabled = false;
                txtAnexo.Enabled = false;
                txtAtendioConsulta.Enabled = false;
                txtRespuestaConsulta.Enabled = false;
                txtValor_acuerdoConsulta.Enabled = false;
                txtObservacionConsulta.Enabled = true;
                txtCodigo_usuario_regis.Enabled = false;
                chkApruebaConsulta.Enabled = false;
                txtAtendioConsulta.Text = "";
                txtRespuestaConsulta.Text = "";
                txtValor_acuerdoConsulta.Text = "";
                txtObservacionConsulta.Text = "";
                txtCodigo_usuario_regis.Text = "";
                FileUpload2.Enabled = false;
                btnModificarDiligencia.Visible = false;
            }
            else
            {
                ddlTipoDiligenciaConsulta.Enabled = true;
                ddlTipoContactoConsulta.Enabled = true;
                txtAtendioConsulta.Enabled = true;
                txtRespuestaConsulta.Enabled = true;
                txtValor_acuerdoConsulta.Enabled = true;
                txtAnexo.Enabled = true;
                txtObservacionConsulta.Enabled = true;
                FileUpload2.Enabled = true;
                btnModificarDiligencia.Visible = true;
                chkApruebaConsulta.Enabled = true;
            }

            if (vDiligencia.tipo_contacto != 0)
                ddlTipoContactoConsulta.SelectedValue = HttpUtility.HtmlDecode(vDiligencia.tipo_contacto.ToString().Trim());
            if (ddlTipoContactoConsulta.SelectedItem.Text == "Cartas")
            {
                ddlTipoContactoConsulta.Enabled = false;
            }
            else
            {
                ddlTipoContactoConsulta.Enabled = true;
            }
            if (!string.IsNullOrEmpty(vDiligencia.atendio))
                txtAtendioConsulta.Text = vDiligencia.atendio.ToString().Trim();
            if (!string.IsNullOrEmpty(vDiligencia.respuesta))
                txtRespuestaConsulta.Text = vDiligencia.respuesta.ToString().Trim();
            if (!string.IsNullOrEmpty(vDiligencia.fecha_acuerdo.ToString()))
                txtFecha_acuerdoConsulta.Text = vDiligencia.fecha_acuerdo.ToString("dd/MM/yyyy").Trim();
            if (txtFecha_acuerdoConsulta.Text == "01/01/0001")
            {
                txtFecha_acuerdoConsulta.Text = "";
            }
            else
            {
                txtFecha_acuerdoConsulta.Text = vDiligencia.fecha_acuerdo.ToString("dd/MM/yyyy").Trim();
            }
            if (vDiligencia.valor_acuerdo != Int64.MinValue)
                txtValor_acuerdoConsulta.Text = vDiligencia.valor_acuerdo.ToString().Trim();
            if (!string.IsNullOrEmpty(vDiligencia.anexo))
            {
                txtAnexo.Text = vDiligencia.anexo.ToString().Trim();
                btnAbrirAnexo.Visible = true;
            }
            else
            {
                btnAbrirAnexo.Visible = false;
            }


            if (!string.IsNullOrEmpty(vDiligencia.observacion))
                txtObservacionConsulta.Text = vDiligencia.observacion.ToString().Trim();
            if (vDiligencia.codigo_usuario_regis != Int64.MinValue)
                txtCodigo_usuario_regis.Text = vDiligencia.codigo_usuario_regis.ToString().Trim();

            if (vDiligencia.acuerdo != Int64.MinValue)
            {
                if (vDiligencia.acuerdo == 1)
                    chkApruebaConsulta.Checked = true;
                else
                    chkApruebaConsulta.Checked = false;
            }
        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(diligenciaServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }

    private String Upload()
    {
        String saveDir = Server.MapPath("~/Archivos/Diligencias/");
        String Fecha = Convert.ToString(DateTime.Now.ToString("MMddyyyy"));

        string appPath = Request.PhysicalApplicationPath;
        if (FileUpload1.HasFile)
        {
            String fileName = txtIdCredito.Text + "-" + Fecha + "-" + FileUpload1.FileName;
            string savePath = saveDir + fileName;
            string archivo = fileName;

            FileUpload1.SaveAs(savePath);
            return savePath;
        }
        else
        {
            return String.Empty;
        }
    }


    private String UploadConsulta()
    {
        String saveDir = Server.MapPath("~/Archivos/Diligencias/");

        string appPath = Request.PhysicalApplicationPath;
        if (FileUpload2.HasFile)
        {
            String fileName = FileUpload2.FileName;
            string savePath = saveDir + fileName;

            FileUpload2.SaveAs(savePath);
            return savePath;
        }
        else
        {
            return String.Empty;
        }
    }


    protected void ObtenerDatosCliente(String pIdObjeto)
    {
        try
        {
            Cliente cliente = new Cliente();

            if (pIdObjeto != null)
            {
                cliente.IdCliente = Int64.Parse(pIdObjeto);
                cliente = clienteServicio.ConsultarClienteEjecutivo(cliente.IdCliente, (Usuario)Session["usuario"]);
                if (!string.IsNullOrEmpty(cliente.IdCliente.ToString()))
                    txtCodigoCliente.Text = HttpUtility.HtmlDecode(cliente.IdCliente.ToString());
                if (!string.IsNullOrEmpty(cliente.NumeroDocumento.ToString()))
                    txtIdentificacionCliente.Text = HttpUtility.HtmlDecode(cliente.NumeroDocumento.ToString());
                if (!string.IsNullOrEmpty(cliente.PrimerNombre))
                    txtPrimerNombreCliente.Text = HttpUtility.HtmlDecode(cliente.PrimerNombre) + " " + cliente.SegundoNombre + " " + cliente.PrimerApellido + " " + cliente.SegundoApellido;
                if (!string.IsNullOrEmpty(cliente.Direccion))
                    txtDireccionCliente.Text = HttpUtility.HtmlDecode(cliente.Direccion);
                if (!string.IsNullOrEmpty(cliente.Telefono))
                    txtTelefonoCliente.Text = HttpUtility.HtmlDecode(cliente.Telefono);
                if (!string.IsNullOrEmpty(cliente.Barrio))
                    txtBarrioCliente.Text = HttpUtility.HtmlDecode(cliente.Barrio);
                if (!string.IsNullOrEmpty(cliente.Ciudad))
                    txtCiudad.Text = HttpUtility.HtmlDecode(cliente.Ciudad);
                if (!string.IsNullOrEmpty(cliente.Email))
                    txtEmailCliente.Text = HttpUtility.HtmlDecode(cliente.Email);
                if (!string.IsNullOrEmpty(cliente.NombreAsesor))
                    txtAsesor.Text = HttpUtility.HtmlDecode(cliente.NombreAsesor);
                Session["IdUsuario"] = cliente.NumeroDocumento;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.GetType().Name, "ObtenerDatosCliente", ex);
        }
    }

    protected void ObtenerDatosClienterepo(String pIdObjeto)
    {
        try
        {
            Cliente cliente = new Cliente();

            if (pIdObjeto != null)
            {

                //cliente.IdCliente = Int64.Parse(pIdObjeto);
                cliente = clienteServicio.ConsultarClienteEjecutivorepomora(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
                if (!string.IsNullOrEmpty(cliente.IdCliente.ToString()))
                    txtCodigoCliente.Text = HttpUtility.HtmlDecode(cliente.IdCliente.ToString());
                //  if (!string.IsNullOrEmpty(cliente.TipoDocumento))
                //    txtTipoIdCliente.Text = HttpUtility.HtmlDecode(cliente.TipoDocumento);
                if (!string.IsNullOrEmpty(cliente.NumeroDocumento.ToString()))
                    txtIdentificacionCliente.Text = HttpUtility.HtmlDecode(cliente.NumeroDocumento.ToString());
                if (!string.IsNullOrEmpty(cliente.PrimerNombre))
                    txtPrimerNombreCliente.Text = HttpUtility.HtmlDecode(cliente.PrimerNombre) + " " + cliente.SegundoNombre + " " + cliente.PrimerApellido + " " + cliente.SegundoApellido;

                if (!string.IsNullOrEmpty(cliente.Direccion))
                    txtDireccionCliente.Text = HttpUtility.HtmlDecode(cliente.Direccion);
                if (!string.IsNullOrEmpty(cliente.Telefono))
                    txtTelefonoCliente.Text = HttpUtility.HtmlDecode(cliente.Telefono);
                if (!string.IsNullOrEmpty(cliente.Barrio))
                    txtBarrioCliente.Text = HttpUtility.HtmlDecode(cliente.Barrio);
                if (!string.IsNullOrEmpty(cliente.Email))
                    txtEmailCliente.Text = HttpUtility.HtmlDecode(cliente.Email);
                if (!string.IsNullOrEmpty(cliente.NombreAsesor))
                    txtAsesor.Text = HttpUtility.HtmlDecode(cliente.NombreAsesor);

            }
            //VerAuditoria(programa.UsuarioCrea, programa.FechaCrea, programa.UsuarioEdita, programa.FechaEdita);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.GetType().Name, "ObtenerDatosCliente", ex);
        }
    }


    protected void ObtenerDatosCredito(String pIdObjeto2)
    {

        Validate("vgObtnerDatos");
        if (Page.IsValid)
        {
            try
            {
                Creditos credito = new Creditos();

                String fechapago;
                if (pIdObjeto2 != null)
                {
                    credito.numero_radicacion = Int64.Parse(pIdObjeto2);
                    credito = creditoServicio.ConsultarCreditoAsesor(credito.numero_radicacion, (Usuario)Session["usuario"]);
                    if (!string.IsNullOrEmpty(credito.numero_radicacion.ToString()))
                        txtIdCredito.Text = HttpUtility.HtmlDecode(credito.numero_radicacion.ToString());
                    if (credito.numero_obligacion != null)
                        if (!string.IsNullOrEmpty(credito.numero_obligacion.ToString()))
                            txtpagare.Text = HttpUtility.HtmlDecode(credito.numero_obligacion.ToString());
                    if (!string.IsNullOrEmpty(credito.linea_credito))
                        txtLineaCredito.Text = HttpUtility.HtmlDecode(credito.linea_credito);
                    if (!string.IsNullOrEmpty(credito.fecha_solicitud.ToShortDateString()))
                        txtFechaSolicitudCredito.Text = HttpUtility.HtmlDecode(credito.fecha_solicitud.ToShortDateString());
                    if (!string.IsNullOrEmpty(credito.monto_aprobado.ToString()))
                        txtMontoCredito.Text = HttpUtility.HtmlDecode(credito.monto_aprobado.ToString("0,0", CultureInfo.InvariantCulture));
                    if (!string.IsNullOrEmpty(credito.plazo.ToString()))
                        txtPlazoCredito.Text = HttpUtility.HtmlDecode(credito.numero_cuotas.ToString());
                    LlenarComboUsuarios();
                    if (credito.NombreAsesor != null)
                        if (!string.IsNullOrEmpty(credito.NombreAsesor.ToString()))
                            codUsuario = credito.CodigoAsesor;
                    try { ddlUsuarios.SelectedValue = codUsuario.ToString(); } catch { VerError("Usuario:" + codUsuario.ToString()); }
                    if (credito.NombreAsesor != null)
                        txtAsesor.Text = credito.NombreAsesor.ToString();
                    txtSaldoCredito.Text = HttpUtility.HtmlDecode(credito.saldo_capital.ToString("0,0", CultureInfo.InvariantCulture));
                    if (!string.IsNullOrEmpty(credito.fecha_vencimiento.ToString()))
                        txtFechaTerminacionCredito.Text = HttpUtility.HtmlDecode(credito.fecha_vencimiento.ToShortDateString());
                    if (!string.IsNullOrEmpty(credito.valor_cuota.ToString()))
                        txtCuotaCredito.Text = HttpUtility.HtmlDecode(credito.valor_cuota.ToString("0,0", CultureInfo.InvariantCulture));
                    if (!string.IsNullOrEmpty(credito.cuotas_pagadas.ToString()))
                        txtCuotasPagadasCredito.Text = HttpUtility.HtmlDecode(credito.cuotas_pagadas.ToString());
                    if (!string.IsNullOrEmpty(credito.calificacion_promedio.ToString()))
                        txtCalificacionCredito.Text = HttpUtility.HtmlDecode(credito.calificacion_promedio.ToString());
                    if (!string.IsNullOrEmpty(credito.fecha_ultimo_pago.ToString()))
                        txtFechaUltimoPagoCredito.Text = HttpUtility.HtmlDecode(credito.fecha_ultimo_pago.ToShortDateString());
                    if (!string.IsNullOrEmpty(credito.ult_valor_pagado.ToString()))
                        txtUltimoValorPagadoCredito.Text = HttpUtility.HtmlDecode(credito.ult_valor_pagado.ToString("0,0", CultureInfo.InvariantCulture));
                    if (!string.IsNullOrEmpty(credito.fecha_prox_pago.ToString()))
                        txtFechaProximoPagoCredito.Text = HttpUtility.HtmlDecode(credito.fecha_prox_pago.ToShortDateString());
                    fechapago = credito.fecha_prox_pago.Day.ToString();
                    txtdiapago.Text = fechapago;
                    if (!string.IsNullOrEmpty(credito.valor_a_pagar.ToString()))
                        txtValorPagarCredito.Text = HttpUtility.HtmlDecode(credito.valor_a_pagar.ToString("0,0", CultureInfo.InvariantCulture));
                    txtSaldoMoraCredito.Text = (credito.saldo_mora + credito.saldo_atributos_mora).ToString("0,0", CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(credito.total_a_pagar.ToString()))
                        txtValorTotalPagarCredito.Text = HttpUtility.HtmlDecode(credito.total_a_pagar.ToString("0,0", CultureInfo.InvariantCulture));
                    if (!string.IsNullOrEmpty(credito.dias_mora.ToString()))
                        this.txtdiasmora.Text = HttpUtility.HtmlDecode(credito.dias_mora.ToString());
                }
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(creditoServicio.GetType().Name, "ObtenerDatosCredito", ex);
            }
        }
    }





    protected void ObtenerDatosCodeudor(String pIdObjeto2)
    {

        try
        {
            Cliente codeudor = new Cliente();
            if (pIdObjeto2 != null)
            {
                Int64 pnumero_radicacion = Convert.ToInt64(txtIdCredito.Text);
                codeudor = creditoServicio.ConsultarCodeudor(pnumero_radicacion, (Usuario)Session["usuario"]);
                if (!string.IsNullOrEmpty(codeudor.IdCliente.ToString()))
                    IdCodeudor = HttpUtility.HtmlDecode(codeudor.IdCliente.ToString());
                if (!string.IsNullOrEmpty(codeudor.TipoDocumento))
                    TipoDocumento = HttpUtility.HtmlDecode(codeudor.TipoDocumento.ToString());
                if (!string.IsNullOrEmpty(codeudor.NumeroDocumento))
                    NumeroDocumento = codeudor.NumeroDocumento.ToString();
                if (!string.IsNullOrEmpty(codeudor.PrimerNombre))
                    PrimerNombre = codeudor.PrimerNombre;
                if (!string.IsNullOrEmpty(codeudor.SegundoNombre))
                    SegundoNombre = codeudor.SegundoNombre;
                if (!string.IsNullOrEmpty(codeudor.PrimerApellido))
                    PrimerApellido = codeudor.PrimerApellido;
                if (!string.IsNullOrEmpty(codeudor.SegundoApellido))
                    SegundoApellido = codeudor.SegundoApellido;
                if (!string.IsNullOrEmpty(codeudor.Direccion))
                    Direccion = codeudor.Direccion;
                if (!string.IsNullOrEmpty(codeudor.Telefono))
                    Telefono = codeudor.Telefono;
                if (!string.IsNullOrEmpty(codeudor.Barrio))
                    Barrio = codeudor.Barrio;
                if (!string.IsNullOrEmpty(codeudor.Email))
                    Email = codeudor.Email;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.GetType().Name, "ObtenerDatosCodeudor", ex);
        }
    }

    protected void ObtenerDatosProceso(String pIdObjeto)
    {
        try
        {
            ProcesosCobro proceso = new ProcesosCobro();

            if (pIdObjeto != null)
            {
                proceso = procesosCobroServicio.ConsultarDatosProceso(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

                if (!string.IsNullOrEmpty(proceso.codprocesocobro.ToString()))
                    if (!string.IsNullOrEmpty(proceso.descripcion))
                        txtProceso.Text = HttpUtility.HtmlDecode(proceso.descripcion);

                LlenarComboUsuarios();

                if (!string.IsNullOrEmpty(proceso.codusuario.ToString()))
                    codUsuario = proceso.codusuario;
                try { ddlUsuarios.SelectedValue = codUsuario.ToString(); } catch { VerError("Usuario:" + codUsuario.ToString()); }

                LlenarComboAbogadosasignados();
                if (!string.IsNullOrEmpty(proceso.codabogado.ToString()))
                    codAbogado = proceso.codabogado;
                if (codAbogado == 0)
                {
                    if (ddlAbogado.SelectedItem != null)
                        ddlAbogado.SelectedItem.Text = "SIN NEGOCIADOR";
                }
                else
                {
                    ddlAbogado.SelectedValue = codAbogado.ToString();
                    ddlAbogado.Enabled = true;
                }

                if (!string.IsNullOrEmpty(proceso.nombremotivo))
                    txtMotivo.Text = HttpUtility.HtmlDecode(proceso.nombremotivo);
            }
            txtNumero_radicacion.Text = txtIdCredito.Text;

            LlenarComboCiudadJuzgado();
        }
        catch (Exception ex)
        {
            VerError("2." + ex.Message);
        }
    }

    private void Codeudores()
    {
        try
        {
            lblTotalRegs0.Visible = true;
            String emptyQuery = "Fila de datos vacia";
            List<Cliente> lstConsultaClientes = new List<Cliente>();
            lstConsultaClientes = clienteServicio.ListarCodeudores(Convert.ToInt64(txtIdCredito.Text), (Usuario)Session["usuario"]);
            gvListaCodeudores.EmptyDataText = emptyQuery;
            gvListaCodeudores.DataSource = lstConsultaClientes;
            if (lstConsultaClientes.Count > 0)
            {
                gvListaCodeudores.Visible = true;
                gvListaCodeudores.DataBind();
                lblTotalRegs0.Text = "<br/> Registros encontrados " + lstConsultaClientes.Count.ToString();
            }
            else
            {
                gvListaCodeudores.Visible = false;
                lblTotalRegs0.Text = "El crédito no tiene codeudores";
            }
            Session.Add(clienteServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private void Diligencias()
    {
        try
        {
            String emptyQuery = "Fila de datos vacia";
            List<Diligencia> lstConsultaDiligencias = new List<Diligencia>();
            Diligencia diligencia = new Diligencia();
            diligencia.numero_radicacion = Convert.ToInt64(txtIdCredito.Text);
            lstConsultaDiligencias = diligenciaServicio.ListarDiligencia(diligencia, (Usuario)Session["usuario"]);
            gvListaDiligencias.EmptyDataText = emptyQuery;
            gvListaDiligencias.DataSource = lstConsultaDiligencias;
            if (lstConsultaDiligencias.Count > 0)
            {
                gvListaDiligencias.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaDiligencias.Count.ToString();
                gvListaDiligencias.DataBind();
                //ValidarPermisosGrilla(gvListaDiligencias);


            }
            else
            {
                gvListaDiligencias.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(diligenciaServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(diligenciaServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private void Atributos()
    {
        try
        {
            String emptyQuery = "Fila de datos vacia";
            List<Atributo> lstConsultaAtributos = new List<Atributo>();
            Xpinn.Asesores.Services.DetallePagoService detPago = new DetallePagoService();
            lstConsultaAtributos = detPago.ListarDetallePago(DateTime.Now, Convert.ToInt64(txtIdCredito.Text), (Usuario)Session["usuario"]);
            if (lstConsultaAtributos == null)
                return;
            //lstConsultaAtributos = atributoServicio.ListarAtributo(Convert.ToInt64(txtIdCredito.Text), (Usuario)Session["usuario"]);
            gvListaAtributos.EmptyDataText = emptyQuery;
            gvListaAtributos.DataSource = lstConsultaAtributos;
            if (lstConsultaAtributos.Count > 0)
            {
                gvListaAtributos.Visible = true;
                lblTotalRegs1.Visible = true;
                lblTotalRegs1.Text = "<br/> Registros encontrados " + lstConsultaAtributos.Count.ToString();
                gvListaAtributos.DataBind();
            }
            else
            {
                gvListaAtributos.Visible = false;
                lblTotalRegs1.Visible = false;
            }

            Session.Add(clienteServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Int64 id = Convert.ToInt32(gvListaDiligencias.DataKeys[gvListaDiligencias.SelectedRow.RowIndex].Value.ToString());
        Session[diligenciaServicio.CodigoPrograma + ".id"] = id;

        String idCliente = txtCodigoCliente.Text;
        Session[clienteServicio.CodigoPrograma + ".id"] = idCliente;
        GridViewRow row = gvListaDiligencias.SelectedRow;
        int id2 = Convert.ToInt32(gvListaDiligencias.DataKeys[row.RowIndex].Value);
        ConsultarDatosDiligencia(id2);
        mpeConsultarActividad.Show();
    }



    protected void gvListaCodeudores_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvListaCodeudores.PageIndex = e.NewPageIndex;
            Codeudores();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void gvListaDiligencias_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

            gvListaDiligencias.PageIndex = e.NewPageIndex;
            Diligencias();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoPrograma, "gvListaDiligencias_PageIndexChanging", ex);
        }


    }

    [System.Web.Services.WebMethod]
    public static string GetNote()
    {
        return "- CodeFile";
    }

    protected void gvListaDiligencias_SelectedIndexChanged(object sender, EventArgs e)
    {
        Int64 id = Convert.ToInt32(gvListaDiligencias.DataKeys[gvListaDiligencias.SelectedRow.RowIndex].Value.ToString());
        Session[diligenciaServicio.CodigoPrograma + ".id"] = id;

        String idCliente = txtCodigoCliente.Text;
        Session[clienteServicio.CodigoPrograma + ".id"] = idCliente;
        GridViewRow row = gvListaDiligencias.SelectedRow;
        int id2 = Convert.ToInt32(gvListaDiligencias.DataKeys[row.RowIndex].Value);
        ConsultarDatosDiligencia(id2);
        mpeConsultarActividad.Show();
    }

    protected void gvListaAtributos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            String id = gvListaAtributos.DataKeys[gvListaAtributos.SelectedRow.RowIndex].Value.ToString();
            gvListaAtributos.PageIndex = e.NewPageIndex;
            Atributos();

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }
    protected void btnInforme_Click(object sender, EventArgs e)
    {

        ReportParameter[] param = new ReportParameter[35];
        param[0] = new ReportParameter("pFecha", Convert.ToString(DateTime.Now.ToShortDateString()));
        param[1] = new ReportParameter("pEntidad", "FUNDACIÓN EMPRENDER");
        param[2] = new ReportParameter("pCodigoCliente", vacios(txtCodigoCliente.Text));
        param[3] = new ReportParameter("pTipoIdentificacionCliente", vacios(""));
        param[4] = new ReportParameter("pIdentificacionCliente", vacios(txtIdentificacionCliente.Text));
        param[5] = new ReportParameter("pEstadoCliente", vacios(""));
        param[6] = new ReportParameter("pPrimerNombreCliente", vacios(txtPrimerNombreCliente.Text));
        param[7] = new ReportParameter("pDireccionCliente", vacios(txtDireccionCliente.Text));
        param[8] = new ReportParameter("pTelefono", vacios(txtTelefonoCliente.Text));
        param[9] = new ReportParameter("pEmailCliente", vacios(txtEmailCliente.Text));
        param[10] = new ReportParameter("pCalificacionCliente", vacios(""));
        param[11] = new ReportParameter("pZonaCliente", vacios(""));
        param[12] = new ReportParameter("pOficinaCliente", vacios(""));
        param[13] = new ReportParameter("pEjecutivoCliente", vacios(txtAsesor.Text));
        param[14] = new ReportParameter("pNoCredito", vacios(txtIdCredito.Text));
        param[15] = new ReportParameter("pLineaCredito", vacios(txtLineaCredito.Text));
        param[16] = new ReportParameter("pFechaSolicitudCredito", vacios(txtFechaSolicitudCredito.Text));
        param[17] = new ReportParameter("pMontoCredito", vacios(txtMontoCredito.Text));
        param[18] = new ReportParameter("pPlazoCredito", vacios(txtPlazoCredito.Text));
        param[19] = new ReportParameter("pProcesoCredito", vacios(txtProceso.Text));
        param[20] = new ReportParameter("pUsuarioCredito", vacios(ddlUsuarios.SelectedItem.Text));
        param[21] = new ReportParameter("pMotivoCredito", vacios(this.txtMotivo.Text));

        param[22] = new ReportParameter("pEjecutivoCredito", vacios(txtAsesor.Text));
        param[23] = new ReportParameter("pFechaTerminacionCredito", vacios(txtFechaTerminacionCredito.Text));
        param[24] = new ReportParameter("pCuotaCredito", vacios(txtCuotaCredito.Text));
        param[25] = new ReportParameter("pCuotasPagadasCredito", vacios(txtCuotasPagadasCredito.Text));
        param[26] = new ReportParameter("pCalificacionCredito", vacios(txtCalificacionCredito.Text));
        param[27] = new ReportParameter("pFechaUltimoPagoCredito", vacios(txtFechaUltimoPagoCredito.Text));
        param[28] = new ReportParameter("pUltimoValorPagadoCredito", vacios(txtUltimoValorPagadoCredito.Text));
        param[29] = new ReportParameter("pFechaProximoPagoCredito", vacios(txtFechaProximoPagoCredito.Text));
        param[30] = new ReportParameter("pSaldoCredito", vacios(txtSaldoCredito.Text));
        param[31] = new ReportParameter("pValorPagarCredito", vacios(txtValorPagarCredito.Text));
        param[32] = new ReportParameter("pSaldoMoraCredito", vacios(txtSaldoMoraCredito.Text));
        param[33] = new ReportParameter("pValorTotalCredito", vacios(txtValorTotalPagarCredito.Text));
        param[34] = new ReportParameter("ImagenReport", ImagenReporte());

        mvReporte.Visible = true;
        ReportViewer1.Reset();
        ReportViewer1.LocalReport.ReportPath = "Page/Asesores/Recuperacion/ReporteDetalle.rdlc";
        ReportViewer1.LocalReport.EnableExternalImages = true;
        ReportViewer1.LocalReport.SetParameters(param);
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportDataSource rds = new ReportDataSource("DataSet1", CrearDataTableCodeudores());
        ReportDataSource rds2 = new ReportDataSource("DataSet2", CrearDataTableDiligencias());
        ReportViewer1.LocalReport.DataSources.Add(rds);
        ReportViewer1.LocalReport.DataSources.Add(rds2);
        ReportViewer1.LocalReport.Refresh();

        mvReporte.ActiveViewIndex = 0;
        mvReporte.Visible = true;
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

    public DataTable CrearDataTableCodeudores()
    {
        List<Cliente> lstConsultaClientes = new List<Cliente>();
        lstConsultaClientes = clienteServicio.ListarCodeudores(Convert.ToInt64(txtIdCredito.Text), (Usuario)Session["usuario"]);
        Codeudor codeudor = new Codeudor();
        codeudor.NumeroRadicacion = Convert.ToInt64(txtIdCredito.Text);
        System.Data.DataTable table = new System.Data.DataTable();

        table.Columns.Add("IdCliente");
        table.Columns.Add("TipoDocumento");
        table.Columns.Add("NumeroDocumento");
        table.Columns.Add("PrimerNombre");
        table.Columns.Add("SegundoNombre");
        table.Columns.Add("PrimerApellido");
        table.Columns.Add("SegundoApellido");
        table.Columns.Add("Direccion");
        table.Columns.Add("Telefono");

        foreach (Cliente item in lstConsultaClientes)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.IdCliente;
            datarw[1] = item.TipoDocumento;
            datarw[2] = item.NumeroDocumento;
            datarw[3] = item.PrimerNombre;
            datarw[4] = item.SegundoNombre;
            datarw[5] = item.PrimerApellido;
            datarw[6] = item.SegundoApellido;
            datarw[7] = item.Direccion;
            datarw[8] = item.Telefono;
            table.Rows.Add(datarw);
        }
        return table;
    }

    public DataTable CrearDataTableDiligencias()
    {
        List<Diligencia> lstConsultaDiligencias = new List<Diligencia>();
        Diligencia diligencia = new Diligencia();
        diligencia.numero_radicacion = Convert.ToInt64(txtIdCredito.Text);
        lstConsultaDiligencias = diligenciaServicio.ListarDiligencia(diligencia, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();

        table.Columns.Add("codigo_diligencia");
        table.Columns.Add("fecha_diligencia");
        table.Columns.Add("tipo_diligencia");
        table.Columns.Add("atendio");
        table.Columns.Add("respuesta");
        table.Columns.Add("acuerdo");

        foreach (Diligencia item in lstConsultaDiligencias)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.codigo_diligencia;
            datarw[1] = item.fecha_diligencia.ToShortDateString();
            datarw[2] = item.tipo_diligencia;
            datarw[3] = item.atendio;
            datarw[4] = item.respuesta;
            datarw[5] = item.acuerdo;
            table.Rows.Add(datarw);
        }
        return table;
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {

    }

    protected void LlenarComboTipo()
    {
        TipoDiligenciaService tipoDiligenciaServicio = new TipoDiligenciaService();
        TipoDiligencia tipoDiligencia = new TipoDiligencia();
        ddlTipoDiligencia.DataSource = tipoDiligenciaServicio.ListarTipoDiligencia(tipoDiligencia, (Usuario)Session["usuario"]);
        ddlTipoDiligencia.DataTextField = "descripcion";
        ddlTipoDiligencia.DataValueField = "tipo_diligencia";
        ddlTipoDiligencia.DataBind();
        // ddlTipoDiligencia.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        txtNumero_radicacion.Text = txtIdCredito.Text;
    }

    protected void LlenarComboTipoContacto()
    {
        TipoDiligenciaService tipoDiligenciaServicio = new TipoDiligenciaService();
        TipoContacto tipoContacto = new TipoContacto();
        ddlTipoContacto.DataSource = tipoDiligenciaServicio.ListarTipoContactoAgregar(tipoContacto, (Usuario)Session["usuario"]);
        ddlTipoContacto.DataTextField = "descripcion";
        ddlTipoContacto.DataValueField = "tipocontacto";
        ddlTipoContacto.DataBind();
        //ddlTipoContacto.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    protected void LlenarComboTipoDiligenciaConsulta()
    {
        TipoDiligenciaService tipoDiligenciaServicio = new TipoDiligenciaService();
        TipoDiligencia tipoDiligencia = new TipoDiligencia();
        ddlTipoDiligenciaConsulta.DataSource = tipoDiligenciaServicio.ListarTipoDiligenciaAgregar(tipoDiligencia, (Usuario)Session["usuario"]);
        ddlTipoDiligenciaConsulta.DataTextField = "descripcion";
        ddlTipoDiligenciaConsulta.DataValueField = "tipo_diligencia";
        ddlTipoDiligenciaConsulta.DataBind();

        //ddlTipoDiligenciaConsulta.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }


    protected void LlenarComboTipoContactoConsulta()
    {
        TipoDiligenciaService tipoDiligenciaServicio = new TipoDiligenciaService();
        TipoContacto tipoContacto = new TipoContacto();
        ddlTipoContactoConsulta.DataSource = tipoDiligenciaServicio.ListarTipoContacto(tipoContacto, (Usuario)Session["usuario"]);
        ddlTipoContactoConsulta.DataTextField = "descripcion";
        ddlTipoContactoConsulta.DataValueField = "tipocontacto";
        ddlTipoContactoConsulta.DataBind();
        //ddlTipoContactoConsulta.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    protected void LlenarComboMotivos()
    {
        MotivosCambioService motivosServicio = new MotivosCambioService();
        MotivosCambio motivo = new MotivosCambio();
        ddlMotivo.DataSource = motivosServicio.ListarMotivosCambio(motivo, (Usuario)Session["usuario"]);
        ddlMotivo.DataTextField = "descripcion";
        ddlMotivo.DataValueField = "cod_motivo_cambio";
        ddlMotivo.DataBind();
        //  ddlMotivo.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    protected void LlenarComboProcesos(Int64 codProceso)
    {
        ProcesosCobro procesoCobro = new ProcesosCobro();
        procesoCobro.codprocesocobro = codProceso;
        ddlProceso.DataSource = procesosCobroServicio.ListarProcesosCobroSiguientes(procesoCobro, (Usuario)Session["usuario"]);
        ddlProceso.DataTextField = "descripcion";
        ddlProceso.DataValueField = "codprocesocobro";
        ddlProceso.DataBind();
        //ddlProceso.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }

    protected void LlenarComboUsuariosPerfilAbogados()
    {
        UsuarioAseService usuarioAseServicio = new UsuarioAseService();
        UsuarioAse usuarioAse = new UsuarioAse();
        Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
        Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
        pData = ConsultaData.ConsultarGeneral(43, (Usuario)Session["usuario"]); Int16 numdias = Convert.ToInt16(pData.valor);

        usuarioAse.codusuario = Convert.ToInt64(pData.valor);
        ddlUsuario.DataSource = usuarioAseServicio.ListarUsuariosPerfilAbogado(usuarioAse, (Usuario)Session["usuario"]);
        ddlUsuario.DataTextField = "nombre";
        ddlUsuario.DataValueField = "codusuario";
        try { ddlUsuario.DataBind(); } catch { }
        ddlUsuario.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<Seleccione Un Negociador ", "0"));

        //   ddlUsuario.Items.Insert(0, new ListItem("<Seleccione Un Negociador>", "0"));
    }


    protected void LlenarComboUsuarios()
    {
        try
        {
            UsuarioAseService usuarioAseServicio = new UsuarioAseService();
            UsuarioAse usuarioAse = new UsuarioAse();
            ddlUsuarios.DataSource = usuarioAseServicio.ListarUsuario(usuarioAse, (Usuario)Session["usuario"]);
            ddlUsuarios.DataTextField = "nombre";
            ddlUsuarios.DataValueField = "codusuario";
            ddlUsuarios.DataBind();
            //   ddlUsuarios.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        }
        catch
        {
            VerError("");
        }
    }

    protected void LlenarComboAbogadosasignados()
    {
        //UsuarioAseService usuarioAseServicio = new UsuarioAseService();
        //UsuarioAse usuarioAse = new UsuarioAse();
        //ddlAbogado.DataSource = usuarioAseServicio.ListarUsuarioAbogados(usuarioAse, (Usuario)Session["usuario"]);
        //ddlAbogado.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<SIN NEGOCIADOR> ", "0"));
        //ddlAbogado.DataTextField = "nombre";
        //ddlAbogado.DataValueField = "codusuario";
        //ddlAbogado.DataBind();

        //Se cambio tya que se agrgaron usuarios y al no estar en la tabla de cobranzas no aparecian
        UsuarioAseService usuarioAseServicio = new UsuarioAseService();
        UsuarioAse usuarioAse = new UsuarioAse();
        Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
        Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
        pData = ConsultaData.ConsultarGeneral(43, (Usuario)Session["usuario"]);
        usuarioAse.codusuario = Convert.ToInt64(pData.valor);

        ddlAbogado.DataSource = usuarioAseServicio.ListarUsuariosPerfilAbogado(usuarioAse, (Usuario)Session["usuario"]);
        ddlAbogado.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<SIN NEGOCIADOR> ", "0"));
        ddlAbogado.DataTextField = "nombre";
        ddlAbogado.DataValueField = "codusuario";
        try { ddlAbogado.DataBind(); } catch { VerError(""); }

    }

    protected void LlenarTiposDocumento()
    {
        TiposDocCobranzasServices TipDocCobranzas = new TiposDocCobranzasServices();
        TiposDocCobranzas TipDocumentoCobranza = new TiposDocCobranzas();
        ddlTipoDoc.DataSource = TipDocCobranzas.ListarTiposDocumentoCobranzas(TipDocumentoCobranza, (Usuario)Session["usuario"]);
        ddlTipoDoc.DataTextField = "DESCRIPCION";
        ddlTipoDoc.DataValueField = "TIPO_DOCUMENTO";
        ddlTipoDoc.DataBind();
        ddlTipoDoc.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<Seleccione un Item>", "0"));

        // ddlAbogado.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }

    protected void LlenarComboZona()
    {
        CiudadService ciudadService = new CiudadService();
        Ciudad zona = new Ciudad();
        zona.tipo = 6;
        // ddlZona.DataSource = ciudadService.ListarCiudadRecuperaciones(zona, (Usuario)Session["usuario"]);
        //ddlZona.DataTextField = "NOMCIUDAD";
        //ddlZona.DataValueField = "CODCIUDAD";
        // ddlZona.DataBind();
        // ddlZona.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }
    protected void LlenarComboCiudadJuzgado()
    {
        CiudadService ciudadService = new CiudadService();
        // Ciudad ciudad = new Ciudad();
        ddlCiudadJuzgado.DataSource = ciudadService.ListarCiudadjuzgados((Usuario)Session["usuario"]);
        ddlCiudadJuzgado.DataTextField = "NOMCIUDAD";
        ddlCiudadJuzgado.DataValueField = "CODCIUDAD";
        ddlCiudadJuzgado.DataBind();
        // ddlCiudadJuzgado.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }

    protected void LlenarComboCiudadJuzgadopage()
    {
        CiudadService ciudadService = new CiudadService();
        // Ciudad ciudad = new Ciudad();
        ddlCiudadJuzg.SelectedValue = null;
        ddlCiudadJuzg.DataSource = ciudadService.ListarCiudadjuzgados((Usuario)Session["usuario"]);
        ddlCiudadJuzg.DataTextField = "NOMCIUDAD";
        ddlCiudadJuzg.DataValueField = "CODCIUDAD";
        ddlCiudadJuzg.DataBind();
        // ddlCiudadJuzg.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    protected void ObtenerDatosProcesoAbogados(String pIdObjeto)
    {
        try
        {
            ProcesosCobro proceso = new ProcesosCobro();

            if (pIdObjeto != null)
            {
                proceso = procesosCobroServicio.ConsultarDatosProceso(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
                if (proceso.codmotivo == 0 || proceso.codprocesocobro == 0)
                {
                    LblReportes.Text = "ESTE CLIENTE NO TIENE NINGUN PROCESO ASIGNADO";
                    if (!string.IsNullOrEmpty(proceso.codprocesocobro.ToString()))
                        codProceso = 1;

                    LlenarComboProcesos(codProceso);
                    txtRadicado.Text = this.txtIdCredito.Text;

                    LlenarComboUsuariosPerfilAbogados();
                    if (!string.IsNullOrEmpty(proceso.codabogado.ToString()))
                        codAbogado = proceso.codabogado;

                    if (codAbogado == 0)
                    {
                        if (ddlUsuario.SelectedItem != null)
                            try
                            {
                                ddlUsuario.SelectedItem.Text = "Seleccione un Item";
                                ddlUsuario.Enabled = true;
                                txtJuzgado.Enabled = true;
                            }
                            catch
                            {
                                // ignored
                            }
                    }
                    else
                    {
                        try { ddlUsuario.SelectedValue = codAbogado.ToString(); } catch { VerError("Abogado:" + codAbogado.ToString()); }
                        ddlUsuario.Enabled = true;
                    }

                    LlenarComboMotivos();
                    // if (!string.IsNullOrEmpty(proceso.codmotivo.ToString()))                       
                    // ddlMotivo.SelectedValue = codMotivo.ToString();
                }
                else
                {
                    if (!string.IsNullOrEmpty(proceso.codprocesocobro.ToString()))
                        codProceso = proceso.codprocesocobro;
                    LlenarComboProcesos(codProceso);
                    txtRadicado.Text = this.txtIdCredito.Text;

                    LlenarComboCiudadJuzgado();
                    //ddlCiudadJuzg.SelectedValue = proceso.codciudadjuzgado.ToString();

                    LlenarComboCiudadJuzgadopage();
                    //ddlCiudadJuzgado.SelectedValue = proceso.codciudadjuzgado.ToString();
                    if (proceso.observaciones != null)
                        txtobservaciones.Text = proceso.observaciones.ToString();
                    else
                        txtobservaciones.Text = "";

                    if (proceso.numero_juzgado != null)
                        txtJuzgado.Text = proceso.numero_juzgado.ToString();
                    else
                        txtJuzgado.Text = "";

                    LlenarComboUsuariosPerfilAbogados();
                    if (!string.IsNullOrEmpty(proceso.codabogado.ToString()))
                        codAbogado = proceso.codabogado;

                    if (codAbogado == 0)
                    {
                        ddlUsuario.SelectedItem.Text = "Seleccione un Item";
                    }
                    else
                    {
                        try { ddlUsuario.SelectedValue = codAbogado.ToString(); } catch { VerError("Abogado:" + codAbogado.ToString()); }
                        ddlUsuario.Enabled = true;
                    }

                    LlenarComboMotivos();
                    if (!string.IsNullOrEmpty(proceso.codmotivo.ToString()))
                        codMotivo = proceso.codmotivo;
                    ddlMotivo.SelectedValue = codMotivo.ToString();


                    LlenarComboZona();

                    if (!string.IsNullOrEmpty(codciudadproceso.ToString()))
                        codciudadproceso = proceso.codciudad;

                }
            }
        }
        catch (Exception ex)
        {
            // BOexcepcion.Throw(procesosCobroServicio.GetType().Name, "ObtenerDatosProceso", ex);
            VerError("3." + ex.Message);
        }

    }


    protected void btnCloseReg1_Click(object sender, EventArgs e)
    {

        mpeNuevo.Hide();

    }

    protected void btnAgregar_Click1(object sender, EventArgs e)
    {

    }
    protected void btnGuardarReg_Click(object sender, EventArgs e)
    {
        lblMsj.Text = "";
        try
        {
            Xpinn.Asesores.Entities.Diligencia vDiligencia = new Xpinn.Asesores.Entities.Diligencia();
            txtNumero_radicacion.Text = txtIdCredito.Text;
            vDiligencia.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text.Trim());
            txtFecha_diligencia.Text = DateTime.Now.ToString("dd/MM/yyyy");
            String FechaDiligencia = DateTime.Now.ToString("dd/MM/yyyy");
            String format = "dd/MM/yyyy";
            vDiligencia.fecha_diligencia = DateTime.ParseExact(FechaDiligencia, format, CultureInfo.InvariantCulture);
            if (ddlTipoDiligencia.SelectedValue != "")
                vDiligencia.tipo_diligencia = Convert.ToInt64(ddlTipoDiligencia.SelectedValue);
            if (ddlTipoContacto.SelectedValue != "")
                vDiligencia.tipo_contacto = Convert.ToInt64(ddlTipoContacto.SelectedValue);
            vDiligencia.atendio = Convert.ToString(txtAtendio.Text.Trim());
            vDiligencia.respuesta = Convert.ToString(txtRespuesta.Text.Trim());

            // Determinar datos del acuerdo de pago
            if (txtFecha_acuerdo.Text == "")
            {
                vDiligencia.fecha_acuerdo = DateTime.Parse("01/01/0001".ToString());
                vDiligencia.valor_acuerdo = 0;
                vDiligencia.observacion = Convert.ToString(txtObservacion.Text.Trim());
                Usuario usuap = (Usuario)Session["usuario"];
                int cod = Convert.ToInt32(usuap.codusuario);
                vDiligencia.codigo_usuario_regis = cod;
            }



            // Validando si se ingreso datos de respuesta y de quien atendio
            if (txtAtendio.Text == "")
            {
                lblMsj.Text = "Por favor ingrese la persona que lo atendió.";
                txtAtendio.Focus();
                return;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
            }
            if (txtRespuesta.Text == "")
            {
                lblMsj.Text = "Por favor ingrese alguna respuesta.";
                txtRespuesta.Focus();
                return;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
            }
            // Validando si se determino acuerdo
            if (chkAprueba.Checked)
                vDiligencia.acuerdo = 1;
            else
                vDiligencia.acuerdo = 0;

            if (chkAprueba.Checked == true && txtFecha_acuerdo.Text == "")
            {
                lblMsj.Text = "Por favor ingrese una fecha de acuerdo";
                txtFecha_acuerdo.Focus();
                return;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
            }

            //if (txtNumCel.Text == "")
            //{
            //    lblMsj.Text = "Por favor ingrese el número de celular al que se comunico.";
            //    txtNumCel.Focus();
            //    return;
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
            //}

            vDiligencia.Num_Celular = Convert.ToString(txtNumCel.Text.Trim());

            if (idObjeto != "")
            {

                vDiligencia.anexo = Upload();
                if (chkAprueba.Checked == false && txtFecha_acuerdo.Text == "")
                {
                    String Fechaacuerdo = "01/01/0001";
                    String format1 = "dd/MM/yyyy";
                    vDiligencia.fecha_acuerdo = DateTime.ParseExact(Fechaacuerdo, format1, CultureInfo.InvariantCulture);
                    txtFecha_acuerdo.Text = Convert.ToString(vDiligencia.fecha_acuerdo).ToString();
                    vDiligencia.valor_acuerdo = 0;
                    txtValor_acuerdo.Text = Convert.ToString(vDiligencia.valor_acuerdo);
                }
                vDiligencia.fecha_acuerdo = Convert.ToDateTime(txtFecha_acuerdo.Text);
                if (txtValor_acuerdo.Text == "")
                    vDiligencia.valor_acuerdo = 0;
                else
                    vDiligencia.valor_acuerdo = Convert.ToInt64(txtValor_acuerdo.Text.Trim().Replace(".", ""));
                vDiligencia.observacion = Convert.ToString(txtObservacion.Text.Trim());

                Usuario usuap = (Usuario)Session["usuario"];
                int cod = Convert.ToInt32(usuap.codusuario);
                vDiligencia.codigo_usuario_regis = cod;
                String FechaDiligencia2 = DateTime.Now.ToString("dd/MM/yyyy");
                String format2 = "dd/MM/yyyy";
                vDiligencia.fecha_diligencia = DateTime.ParseExact(FechaDiligencia2, format2, CultureInfo.InvariantCulture);
                if (vDiligencia.fecha_acuerdo < vDiligencia.fecha_diligencia && txtFecha_acuerdo.Text != "01/01/0001 12:00:00 a.m." || vDiligencia.fecha_acuerdo.ToLongTimeString() != vDiligencia.fecha_diligencia.ToLongTimeString())
                {
                    if (chkAprueba.Checked == true)
                    {
                        lblMsj.Text = "La Fecha de acuerdo no puede ser menor  a la fecha actual";
                        txtFecha_acuerdo.Focus();
                        return;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
                    }
                }

                diligenciaServicio.CrearDiligencia(vDiligencia, (Usuario)Session["usuario"]);

                if (gvCreditos.Rows.Count > 0 && panelCreditos.Visible == true)
                {
                    //GRABAR A LOS DEMAS CREDITOS SELECCIONADOS
                    foreach (GridViewRow rFIla in gvCreditos.Rows)
                    {
                        CheckBox cbSeleccionar = (CheckBox)rFIla.FindControl("cbSeleccionar");
                        if (cbSeleccionar != null)
                        {
                            if (cbSeleccionar.Checked)
                            {
                                Diligencia pEntidad = new Diligencia();
                                Int64 pNum_Radicacion = rFIla.Cells[1].Text != "&nbsp;" ? Convert.ToInt64(rFIla.Cells[1].Text) : 0;
                                vDiligencia.numero_radicacion = pNum_Radicacion;
                                pEntidad = diligenciaServicio.CrearDiligencia(vDiligencia, (Usuario)Session["usuario"]);
                            }
                        }
                    }
                }

                // Limpiar datos
                idObjeto = txtIdCredito.Text;
                LblMensaje.Text = "";
                txtAtendio.Text = "";
                txtFecha_acuerdo.Text = "";
                txtValor_acuerdo.Text = "";
                txtRespuesta.Text = "";
                txtObservacion.Text = "";
                try { ddlTipoDiligencia.SelectedValue = "0"; }
                catch { }
                try { ddlTipoContacto.SelectedValue = "0"; }
                catch { }
                chkAprueba.Checked = false;


                Session[diligenciaServicio.CodigoPrograma + ".id"] = idObjeto;
                lblCambiarArchivo.Visible = false;
                Actualizar(idObjeto);
                Diligencias();
                mpeNuevoActividad.Hide();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError("4." + ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(diligenciaServicio.CodigoPrograma, "btnGuardarReg_Click", ex);
        }
    }

    protected void diligenciacartahabeas()
    {
        VerError("");
        try
        {
            Xpinn.Asesores.Entities.Diligencia vDiligencia = new Xpinn.Asesores.Entities.Diligencia();

            txtNumero_radicacion.Text = txtIdCredito.Text;
            vDiligencia.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text.Trim());

            txtFecha_diligencia.Text = DateTime.Now.ToString("dd/MM/yyyy");
            String FechaDiligencia = DateTime.Now.ToString("dd/MM/yyyy");
            String format = "dd/MM/yyyy";
            vDiligencia.fecha_diligencia = DateTime.ParseExact(FechaDiligencia, format, CultureInfo.InvariantCulture);


            Diligencia diligencia = new Diligencia();


            diligencia = diligenciaServicio.ConsultarparametroHabeasData((Usuario)Session["Usuario"]);
            Int64 tipo_diligencia = diligencia.codigo_parametro;

            vDiligencia.tipo_diligencia = tipo_diligencia;

            diligencia = diligenciaServicio.ConsultarparametroContactoCartas((Usuario)Session["Usuario"]);
            Int64 tipo_contacto = diligencia.codigo_parametro;
            vDiligencia.tipo_contacto = tipo_contacto;

            vDiligencia.atendio = Convert.ToString("".Trim());
            vDiligencia.respuesta = Convert.ToString("".Trim());
            vDiligencia.acuerdo = 0;
            txtFecha_acuerdo.Text = Convert.ToString(vDiligencia.fecha_acuerdo).ToString();
            String Fechaacuerdo = "01/01/0001";
            String format1 = "dd/MM/yyyy";
            vDiligencia.fecha_acuerdo = DateTime.ParseExact(Fechaacuerdo, format1, CultureInfo.InvariantCulture);

            vDiligencia.valor_acuerdo = 0;

            vDiligencia.observacion = Convert.ToString(txtObservacion.Text.Trim());
            // vDiligencia.codigo_usuario_regis = 85;


            Usuario usuap = (Usuario)Session["usuario"];
            int cod = Convert.ToInt32(usuap.codusuario);

            vDiligencia.codigo_usuario_regis = cod;

            vDiligencia.anexo = Convert.ToString("".Trim());


            diligenciaServicio.CrearDiligencia(vDiligencia, (Usuario)Session["usuario"]);
            Session[diligenciaServicio.CodigoPrograma + ".id"] = idObjeto;

        }

        catch (ExceptionBusiness ex)
        {
            VerError("5." + ex.Message);
        }

    }

    protected void diligenciaCobroPrejuridico()
    {

        try
        {
            Xpinn.Asesores.Entities.Diligencia vDiligencia = new Xpinn.Asesores.Entities.Diligencia();

            txtNumero_radicacion.Text = txtIdCredito.Text;
            vDiligencia.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text.Trim());

            txtFecha_diligencia.Text = DateTime.Now.ToString("dd/MM/yyyy");
            String FechaDiligencia = DateTime.Now.ToString("dd/MM/yyyy");
            String format = "dd/MM/yyyy";
            vDiligencia.fecha_diligencia = DateTime.ParseExact(FechaDiligencia, format, CultureInfo.InvariantCulture);


            Diligencia diligencia = new Diligencia();


            diligencia = diligenciaServicio.ConsultarparametroCobroPreJuridico((Usuario)Session["Usuario"]);
            Int64 tipo_diligencia = diligencia.codigo_parametro;

            vDiligencia.tipo_diligencia = tipo_diligencia;

            diligencia = diligenciaServicio.ConsultarparametroContactoCartas((Usuario)Session["Usuario"]);
            Int64 tipo_contacto = diligencia.codigo_parametro;
            vDiligencia.tipo_contacto = tipo_contacto;

            vDiligencia.atendio = Convert.ToString("".Trim());
            vDiligencia.respuesta = Convert.ToString("".Trim());
            vDiligencia.acuerdo = 0;
            txtFecha_acuerdo.Text = Convert.ToString(vDiligencia.fecha_acuerdo).ToString();
            String Fechaacuerdo = "01/01/0001";
            String format1 = "dd/MM/yyyy";
            if (txtFecha_acuerdo.Text == "")
                vDiligencia.fecha_acuerdo = Convert.ToDateTime("01/01/0001");
            else
                vDiligencia.fecha_acuerdo = DateTime.ParseExact(Fechaacuerdo, format1, CultureInfo.InvariantCulture);

            vDiligencia.valor_acuerdo = 0;

            vDiligencia.observacion = Convert.ToString(txtObservacion.Text.Trim());
            // vDiligencia.codigo_usuario_regis = 85;
            Usuario usuap = (Usuario)Session["usuario"];
            int cod = Convert.ToInt32(usuap.codusuario);

            vDiligencia.codigo_usuario_regis = cod;

            vDiligencia.anexo = Convert.ToString("".Trim());


            diligenciaServicio.CrearDiligencia(vDiligencia, (Usuario)Session["usuario"]);
            Session[diligenciaServicio.CodigoPrograma + ".id"] = idObjeto;

        }

        catch (ExceptionBusiness ex)
        {
            VerError("5." + ex.Message);
        }

    }

    protected void diligenciaCobroPrejuridico2()
    {

        try
        {
            Xpinn.Asesores.Entities.Diligencia vDiligencia = new Xpinn.Asesores.Entities.Diligencia();

            txtNumero_radicacion.Text = txtIdCredito.Text;
            vDiligencia.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text.Trim());

            txtFecha_diligencia.Text = DateTime.Now.ToString("dd/MM/yyyy");
            String FechaDiligencia = DateTime.Now.ToString("dd/MM/yyyy");
            String format = "dd/MM/yyyy";
            vDiligencia.fecha_diligencia = DateTime.ParseExact(FechaDiligencia, format, CultureInfo.InvariantCulture);


            Diligencia diligencia = new Diligencia();


            diligencia = diligenciaServicio.ConsultarparametroPrejuridico2((Usuario)Session["Usuario"]);
            Int64 tipo_diligencia = diligencia.codigo_parametro;

            vDiligencia.tipo_diligencia = tipo_diligencia;

            diligencia = diligenciaServicio.ConsultarparametroContactoCartas((Usuario)Session["Usuario"]);
            Int64 tipo_contacto = diligencia.codigo_parametro;
            vDiligencia.tipo_contacto = tipo_contacto;

            vDiligencia.atendio = Convert.ToString("".Trim());
            vDiligencia.respuesta = Convert.ToString("".Trim());
            vDiligencia.acuerdo = 0;
            txtFecha_acuerdo.Text = Convert.ToString(vDiligencia.fecha_acuerdo).ToString();
            String Fechaacuerdo = "01/01/0001";
            String format1 = "dd/MM/yyyy";
            vDiligencia.fecha_acuerdo = DateTime.ParseExact(Fechaacuerdo, format1, CultureInfo.InvariantCulture);

            vDiligencia.valor_acuerdo = 0;

            vDiligencia.observacion = Convert.ToString(txtObservacion.Text.Trim());
            //vDiligencia.codigo_usuario_regis = 85;
            Usuario usuap = (Usuario)Session["usuario"];
            int cod = Convert.ToInt32(usuap.codusuario);

            vDiligencia.codigo_usuario_regis = cod;
            vDiligencia.anexo = Convert.ToString("".Trim());


            diligenciaServicio.CrearDiligencia(vDiligencia, (Usuario)Session["usuario"]);
            Session[diligenciaServicio.CodigoPrograma + ".id"] = idObjeto;

        }

        catch (ExceptionBusiness ex)
        {
            VerError("7." + ex.Message);
        }

    }

    protected void diligenciaVisitaAbogado()
    {

        try
        {
            Xpinn.Asesores.Entities.Diligencia vDiligencia = new Xpinn.Asesores.Entities.Diligencia();

            txtNumero_radicacion.Text = txtIdCredito.Text;
            vDiligencia.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text.Trim());

            txtFecha_diligencia.Text = DateTime.Now.ToString("dd/MM/yyyy");
            String FechaDiligencia = DateTime.Now.ToString("dd/MM/yyyy");
            String format = "dd/MM/yyyy";
            vDiligencia.fecha_diligencia = DateTime.ParseExact(FechaDiligencia, format, CultureInfo.InvariantCulture);

            Diligencia diligencia = new Diligencia();


            diligencia = diligenciaServicio.ConsultarparametroVisitaAbogado((Usuario)Session["Usuario"]);
            Int64 tipo_diligencia = diligencia.codigo_parametro;

            vDiligencia.tipo_diligencia = tipo_diligencia;

            diligencia = diligenciaServicio.ConsultarparametroContactoCartas((Usuario)Session["Usuario"]);
            Int64 tipo_contacto = diligencia.codigo_parametro;
            vDiligencia.tipo_contacto = tipo_contacto;


            vDiligencia.atendio = Convert.ToString("".Trim());
            vDiligencia.respuesta = Convert.ToString("".Trim());
            vDiligencia.acuerdo = 0;
            txtFecha_acuerdo.Text = Convert.ToString(vDiligencia.fecha_acuerdo).ToString();
            String Fechaacuerdo = "01/01/0001";
            String format1 = "dd/MM/yyyy";
            vDiligencia.fecha_acuerdo = DateTime.ParseExact(Fechaacuerdo, format1, CultureInfo.InvariantCulture);

            vDiligencia.valor_acuerdo = 0;

            vDiligencia.observacion = Convert.ToString(txtObservacion.Text.Trim());
            //vDiligencia.codigo_usuario_regis = 85;
            Usuario usuap = (Usuario)Session["usuario"];
            int cod = Convert.ToInt32(usuap.codusuario);

            vDiligencia.codigo_usuario_regis = cod;
            vDiligencia.anexo = Convert.ToString("".Trim());


            diligenciaServicio.CrearDiligencia(vDiligencia, (Usuario)Session["usuario"]);
            Session[diligenciaServicio.CodigoPrograma + ".id"] = idObjeto;

        }

        catch (ExceptionBusiness ex)
        {
            VerError("8." + ex.Message);
        }

    }

    protected void diligenciaVisitaCampaña()
    {

        try
        {
            Xpinn.Asesores.Entities.Diligencia vDiligencia = new Xpinn.Asesores.Entities.Diligencia();

            txtNumero_radicacion.Text = txtIdCredito.Text;
            vDiligencia.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text.Trim());

            txtFecha_diligencia.Text = DateTime.Now.ToString("dd/MM/yyyy");
            String FechaDiligencia = DateTime.Now.ToString("dd/MM/yyyy");
            String format = "dd/MM/yyyy";
            vDiligencia.fecha_diligencia = DateTime.ParseExact(FechaDiligencia, format, CultureInfo.InvariantCulture);

            Diligencia diligencia = new Diligencia();


            diligencia = diligenciaServicio.ConsultarparametroCampaña((Usuario)Session["Usuario"]);
            Int64 tipo_diligencia = diligencia.codigo_parametro;

            vDiligencia.tipo_diligencia = tipo_diligencia;

            diligencia = diligenciaServicio.ConsultarparametroContactoCartas((Usuario)Session["Usuario"]);
            Int64 tipo_contacto = diligencia.codigo_parametro;
            vDiligencia.tipo_contacto = tipo_contacto;


            vDiligencia.atendio = Convert.ToString("".Trim());
            vDiligencia.respuesta = Convert.ToString("".Trim());
            vDiligencia.acuerdo = 0;
            txtFecha_acuerdo.Text = Convert.ToString(vDiligencia.fecha_acuerdo).ToString();
            String Fechaacuerdo = "01/01/0001";
            String format1 = "dd/MM/yyyy";
            vDiligencia.fecha_acuerdo = DateTime.ParseExact(Fechaacuerdo, format1, CultureInfo.InvariantCulture);

            vDiligencia.valor_acuerdo = 0;

            vDiligencia.observacion = Convert.ToString(txtObservacion.Text.Trim());
            //vDiligencia.codigo_usuario_regis = 85;
            Usuario usuap = (Usuario)Session["usuario"];
            int cod = Convert.ToInt32(usuap.codusuario);

            vDiligencia.codigo_usuario_regis = cod;
            vDiligencia.anexo = Convert.ToString("".Trim());


            diligenciaServicio.CrearDiligencia(vDiligencia, (Usuario)Session["usuario"]);
            Session[diligenciaServicio.CodigoPrograma + ".id"] = idObjeto;

        }

        catch (ExceptionBusiness ex)
        {
            VerError("9." + ex.Message);
        }

    }

    private void Actualizar(String pIdObjeto)
    {
        try
        {
            CobrosCreditoService cobroCreditoServicio = new CobrosCreditoService();

            Session[cobroCreditoServicio.Credito + ".id"] = idObjeto;
            ProcesosCobro proceso = new ProcesosCobro();
            proceso = procesosCobroServicio.ConsultarDatosProceso(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(proceso.codprocesocobro.ToString()))
                //txtProceso.Text = HttpUtility.HtmlDecode(proceso.codprocesocobro.ToString());
                if (!string.IsNullOrEmpty(proceso.descripcion))
                    txtProceso.Text = HttpUtility.HtmlDecode(proceso.descripcion);

            LlenarComboAbogadosasignados();
            if (!string.IsNullOrEmpty(proceso.codabogado.ToString()))
                codAbogado = proceso.codabogado;
            if (codAbogado == 0)
            {
                if (ddlAbogado.SelectedItem != null)
                    ddlAbogado.SelectedItem.Text = "SIN NEGOCIADOR";
            }
            else
            {
                ddlAbogado.SelectedValue = codAbogado.ToString();
                ddlAbogado.Enabled = true;
            }
            if (!string.IsNullOrEmpty(proceso.nombremotivo))
                txtMotivo.Text = HttpUtility.HtmlDecode(proceso.nombremotivo);
        }
        catch (Exception ex)
        {
            VerError("10." + ex.Message);
        }
        ObtenerDatosProcesoAbogados(idObjeto);
        LlenarComboCiudadJuzgado();
        LlenarComboCiudadJuzgadopage();

    }

    private void ImprimirCitacion()
    {

        Usuario usuariop = (Usuario)Session["usuario"];
        String pIdObjeto2 = txtIdCredito.Text;
        byte[] bytes;
        mvReporte.Visible = true;
        mvReporte.ActiveViewIndex = 0;
        Warning[] warnings;
        String[] streamids;
        String mimetype;
        String encoding;
        String extension;
        string _sSuggestedName = String.Empty;

        Int64 tipoDocumento = 8;

        if (GenerarDocumento(tipoDocumento) == false)
        {

            //String espacio = " ";
            ReportParameter[] param = new ReportParameter[7];
            param[0] = new ReportParameter("pFecha", Convert.ToString(DateTime.Now.ToShortDateString()));
            param[1] = new ReportParameter("Pcliente", vacios(txtPrimerNombreCliente.Text));
            param[2] = new ReportParameter("Pdireccion", vacios(txtDireccionCliente.Text));
            param[3] = new ReportParameter("pOficina", vacios(usuariop.nombre_oficina));
            param[4] = new ReportParameter("pDiacitacion", vacios(txtFechaCitacion.Text));

            if (usuariop.cod_oficina == 2)
            {
                param[5] = new ReportParameter("pCiudad", vacios("Chia"));
            }

            if (usuariop.cod_oficina == 6)
            {
                param[5] = new ReportParameter("pCiudad", vacios("Ubate"));

            }

            if (usuariop.cod_oficina != 6 || usuariop.cod_oficina != 2)
            {
                param[5] = new ReportParameter("pCiudad", vacios("Bogotá"));

            }
            param[6] = new ReportParameter("ImagenReport", ImagenReporte());
            ReportViewer1.Reset();
            ReportViewer1.LocalReport.ReportPath = "Page/Asesores/Recuperacion/Citacion.rdlc";
            ReportViewer1.LocalReport.EnableExternalImages = true;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.SetParameters(param);
            ReportViewer1.LocalReport.Refresh();
            bytes = this.ReportViewer1.LocalReport.Render("PDF", null, out mimetype, out encoding, out extension, out streamids, out warnings);

            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
            string ruta = Server.MapPath("~/Archivos/Cobros/");

            if (Directory.Exists(ruta))
            {
                String Fecha = Convert.ToString(DateTime.Now.ToString("MMddyyyy"));
                String fileName = "citacion-" + txtIdentificacionCliente.Text + "-" + Fecha + ".pdf";
                string savePath = ruta + fileName;
                FileStream fs = new FileStream(savePath, FileMode.Create);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                FileStream archivo = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                FileInfo file = new FileInfo(savePath);
                Response.Clear();
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/pdf";
                Response.TransmitFile(file.FullName);
                Response.End();
            }
        }
    }

    private void Actualizar2(String pIdObjeto)
    {
        CobrosCreditoService cobroCreditoServicio = new CobrosCreditoService();


        ProcesosCobro proceso = new ProcesosCobro();
        proceso = procesosCobroServicio.ConsultarDatosProceso(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

        if (!string.IsNullOrEmpty(proceso.codprocesocobro.ToString()))
            //txtProceso.Text = HttpUtility.HtmlDecode(proceso.codprocesocobro.ToString());
            if (!string.IsNullOrEmpty(proceso.descripcion))
                txtProceso.Text = HttpUtility.HtmlDecode(proceso.descripcion);

        LlenarComboAbogadosasignados();
        if (!string.IsNullOrEmpty(proceso.codabogado.ToString()))
            codAbogado = proceso.codabogado;
        if (codAbogado == 0)
        {
            ddlAbogado.SelectedItem.Text = "SIN NEGOCIADOR";
        }
        else
        {
            ddlAbogado.SelectedValue = codAbogado.ToString();
            ddlAbogado.Enabled = true;
        }




        if (!string.IsNullOrEmpty(proceso.nombremotivo))
            txtMotivo.Text = HttpUtility.HtmlDecode(proceso.nombremotivo);

        ObtenerDatosProcesoAbogados(pIdObjeto);

    }


    protected void btnCloseReg_Click(object sender, EventArgs e)
    {

    }

    protected void btnCobroPreJuridico_Click(object sender, EventArgs e)
    {
        String pIdObjeto2 = txtIdCredito.Text;
        ObtenerDatosCodeudor(pIdObjeto2);
        byte[] bytes;
        mvReporte.Visible = true;
        mvReporte.ActiveViewIndex = 0;
        Warning[] warnings;
        String[] streamids;
        String mimetype;
        String encoding;
        String extension;
        string _sSuggestedName = String.Empty;


        Int64 tipoDocumento = 1;
        if (IdCodeudor == "0")
            tipoDocumento = 3;
        else
            tipoDocumento = 4;

        if (GenerarDocumento(tipoDocumento) == false)
        {
            if (VerificarParametroCobroPreJuridico())
            {
                String espacio = " ";
                ReportParameter[] param = new ReportParameter[16];
                param[0] = new ReportParameter("pFecha", Convert.ToString(DateTime.Now.ToShortDateString()));
                param[1] = new ReportParameter("Pcliente", vacios(txtPrimerNombreCliente.Text));
                param[2] = new ReportParameter("Pdireccion", vacios(txtDireccionCliente.Text));
                param[3] = new ReportParameter("pTelefono", vacios(txtTelefonoCliente.Text));
                param[4] = new ReportParameter("pBarrio", vacios(this.txtBarrioCliente.Text));
                param[5] = new ReportParameter("pObligacion", vacios(txtIdCredito.Text));
                param[6] = new ReportParameter("pIdentificacionCliente", vacios(this.txtIdentificacionCliente.Text));
                param[7] = new ReportParameter("pPagare", vacios(txtpagare.Text));
                param[8] = new ReportParameter("pIdcodeudor", vacios(IdCodeudor));
                param[9] = new ReportParameter("pTipoDocumento", vacios(TipoDocumento));
                param[10] = new ReportParameter("pNumeroDocumento", vacios(NumeroDocumento));
                param[11] = new ReportParameter("pCodeudor", vacios(PrimerNombre + espacio + SegundoNombre + espacio + PrimerApellido + espacio + SegundoApellido));
                param[12] = new ReportParameter("pDireccioncod", vacios(Direccion));
                param[13] = new ReportParameter("pTelefonocod", vacios(Telefono));
                param[14] = new ReportParameter("pBarriocod", vacios(Barrio));
                param[15] = new ReportParameter("ImagenReport", ImagenReporte());
                ReportViewer1.Reset();
                if (IdCodeudor == "0")
                {
                    ReportViewer1.LocalReport.ReportPath = "Page/Asesores/Recuperacion/ReporteCobroPreJuridico.rdlc";
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.SetParameters(param);
                    ReportViewer1.LocalReport.Refresh();
                    bytes = this.ReportViewer1.LocalReport.Render("PDF", null, out mimetype, out encoding, out extension, out streamids, out warnings);
                }
                else
                {
                    ReportViewer1.LocalReport.ReportPath = "Page/Asesores/Recuperacion/ReporteCobroPreJur2ho.rdlc";
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.SetParameters(param);
                    ReportViewer1.LocalReport.Refresh();
                    bytes = this.ReportViewer1.LocalReport.Render("PDF", null, out mimetype, out encoding, out extension, out streamids, out warnings);
                }
                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);

                string ruta = Server.MapPath("~/Archivos/Cobros/");

                if (Directory.Exists(ruta))
                {
                    String Fecha = Convert.ToString(DateTime.Now.ToString("MMddyyyy"));
                    String fileName = "prejuridico-" + txtIdentificacionCliente.Text + "-" + Fecha + ".pdf";
                    string savePath = ruta + fileName;

                    FileStream fs = new FileStream(savePath, FileMode.Create);

                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();

                    diligenciaCobroPrejuridico();
                    FileStream archivo = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                    FileInfo file = new FileInfo(savePath);
                    Response.Clear();
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/pdf";
                    Response.TransmitFile(file.FullName);
                    Response.End();
                    LblCartas.Text = "REPORTE COBRO PREJURÍDICO GENERADO CORRECTAMENTE  ";
                }
                else
                {
                    LblCartas.Text = "NO SE ENCUENTRA LA CARPETA" + "" + ruta;
                }
            }
        }
    }

    protected void btnGuardarRegProce_Click(object sender, EventArgs e)
    {
        Usuario usuap1 = (Usuario)Session["usuario"];
        int usucreacion = Convert.ToInt32(usuap1.codusuario);
        try
        {
            if (idObjeto != "")
            {
                CobrosCreditoService cobroCreditoServicio = new CobrosCreditoService();
                CobrosCredito cobroCredito = new CobrosCredito();
                Creditos credito = new Creditos();
                cobroCredito.numero_radicacion = Convert.ToInt64(idObjeto);
                cobroCredito.usucreacion = usucreacion;
                cobroCredito.fechacreacion = DateTime.Now;
                cobroCredito.usumodif = usucreacion;
                cobroCredito.fechamodif = DateTime.Now;

                if (idObjeto != "")
                {
                    cobroCreditoServicio.ConsultarCobrosCredito(cobroCredito.numero_radicacion, (Usuario)Session["usuario"]);
                    if (this.ddlProceso.SelectedValue == "0" || ddlMotivo.SelectedValue == "0")
                    {
                        String Error = "Por favor ingrese un estado al proceso y un motivo de cambio";
                        this.Label1.Text = Error;
                    }
                    else
                    {
                        this.Label1.Text = "";
                        cobroCredito.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);
                        cobroCredito.estado_proceso = Convert.ToInt64(ddlProceso.SelectedValue);
                        cobroCredito.cod_motivo_cambio = Convert.ToInt64(ddlMotivo.SelectedValue);
                        credito = creditoServicio.ConsultarCreditoAsesor(cobroCredito.numero_radicacion, (Usuario)Session["usuario"]);
                        if (credito.NombreAsesor != null)
                            if (!string.IsNullOrEmpty(credito.NombreAsesor.ToString()))
                                codUsuario = credito.CodigoAsesor;
                        try
                        {
                            ddlUsuarios.SelectedValue = codUsuario.ToString();
                        }
                        catch
                        {
                        }
                        cobroCredito.encargado = Convert.ToInt64(ddlUsuarios.SelectedValue);
                        cobroCredito.ciudad = 11001;
                        if (cobroCredito.estado_proceso != 6)
                        {
                            this.Label1.Text = "";
                            cobroCredito.abogadoencargado = 0;
                            cobroCredito.ciudad_juzgado = Convert.ToInt32(ddlCiudadJuzgado.SelectedValue);
                            cobroCredito.numero_juzgado = txtJuzgado.Text;
                            cobroCredito.observaciones = txtobservaciones.Text;
                        }
                        if ((cobroCredito.estado_proceso >= 6 && ddlUsuario.SelectedValue == "0"))
                        {
                            String Error = "Por favor ingrese un abogado,numero juzgado,ciudad juzgado y observaciones para el proceso";
                            this.Label1.Text = Error;
                        }
                        else
                        {
                            cobroCredito.abogadoencargado = Convert.ToInt32(ddlUsuario.SelectedValue.ToString());
                            if (txtProceso.Text.Length > 0)
                            {
                                cobroCredito.numero_juzgado = txtJuzgado.Text;
                                if (ddlCiudadJuzgado.SelectedValue != null)
                                    if (ddlCiudadJuzgado.SelectedValue != "")
                                        cobroCredito.ciudad_juzgado = Convert.ToInt64(ddlCiudadJuzgado.SelectedValue);
                                cobroCredito.observaciones = txtobservaciones.Text;
                                cobroCreditoServicio.ModificarCobrosCredito(cobroCredito, (Usuario)Session["usuario"]);
                            }
                            else
                            {
                                cobroCreditoServicio.CrearCobrosCredito(cobroCredito, (Usuario)Session["usuario"]);
                            }

                        }
                    }

                }

                mpeNuevo.Hide();
                Actualizar(idObjeto);
                LblReportes.Text = "";

            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError("11." + ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(procesosCobroServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnCloseReg2_Click(object sender, EventArgs e)
    {
        mpeNuevoActividad.Hide();
        ddlTipoDiligenciaConsulta.Enabled = true;
    }

    protected void chkAprueba_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAprueba.Checked)
        {
            txtValor_acuerdo.Enabled = true;
            txtFecha_acuerdo.Enabled = true;
        }
        else
        {
            txtValor_acuerdo.Enabled = false;
            txtFecha_acuerdo.Enabled = false;
        }
        return;
    }

    protected void btnInfo1_Click(object sender, EventArgs e)
    {

    }


    protected void btnCambiar_Click(object sender, EventArgs e)
    {

        Usuario usuap1 = (Usuario)Session["usuario"];
        int usucreacion = Convert.ToInt32(usuap1.codusuario);
        try
        {
            if (idObjeto != "")
            {
                CobrosCreditoService cobroCreditoServicio = new CobrosCreditoService();
                CobrosCredito cobroCredito = new CobrosCredito();
                Creditos credito = new Creditos();
                cobroCredito.numero_radicacion = Convert.ToInt64(idObjeto);
                cobroCredito.usucreacion = usucreacion;
                cobroCredito.fechacreacion = DateTime.Now;
                cobroCredito.usumodif = usucreacion;
                cobroCredito.fechamodif = DateTime.Now;

                if (idObjeto != "")
                {
                    cobroCredito = cobroCreditoServicio.ConsultarCobrosCredito(cobroCredito.numero_radicacion, (Usuario)Session["usuario"]);
                    this.Label1.Text = "";
                    cobroCredito.abogadoencargado = Convert.ToInt32(ddlAbogado.SelectedValue.ToString());
                    cobroCredito.numero_radicacion = Convert.ToInt64(idObjeto);
                    cobroCredito.usucreacion = usucreacion;
                    cobroCredito.fechacreacion = DateTime.Now;
                    cobroCredito.usumodif = usucreacion;
                    cobroCredito.fechamodif = DateTime.Now;
                    cobroCreditoServicio.ModificarCobrosCredito(cobroCredito, (Usuario)Session["usuario"]);
                }

                // Actualizar(idObjeto);
                lblCambio.Text = "Se cambio satisfactoriamente el Negociador";
            }
        }
        catch
        {
        }
    }


    protected void btnCloseRegConsulta_Click(object sender, EventArgs e)
    {

    }

    [WebMethodAttribute]
    [ScriptMethod]
    public static string GetContent(string contextKey)
    {
        return contextKey;
    }

    protected void gvListaDiligencias_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

        Int64 id = Convert.ToInt32(gvListaDiligencias.DataKeys[gvListaDiligencias.SelectedRow.RowIndex].Value.ToString());
        Session[diligenciaServicio.CodigoPrograma + ".id"] = id;

        String idCliente = txtCodigoCliente.Text;
        Session[clienteServicio.CodigoPrograma + ".id"] = idCliente;
        GridViewRow row = gvListaDiligencias.SelectedRow;

        int id2 = Convert.ToInt32(gvListaDiligencias.DataKeys[row.RowIndex].Value);
        ConsultarDatosDiligencia(id2);
        mpeConsultarActividad.Show();

    }


    protected void gvListaDiligencias_SelectedIndexChanging1(object sender, GridViewSelectEventArgs e)
    {
        int id2 = Convert.ToInt32(gvListaDiligencias.DataKeys[e.NewSelectedIndex].Value);

        ConsultarDatosDiligencia(id2);
        mpeConsultarActividad.Show();
    }

    protected void btnInfo2_DataBinding(object sender, EventArgs e)
    {

    }

    protected void btnCloseRegConsulta_Click1(object sender, EventArgs e)
    {
        mpeConsultarActividad.Hide();
        ddlTipoContactoConsulta.Enabled = true;
    }

    protected void btnAbrirAnexo_Click(object sender, EventArgs e)
    {
        String FileName = txtAnexo.Text;
        string extension;
        string name = Request.Params["file"];
        name = System.IO.Path.GetFileName(FileName);
        extension = System.IO.Path.GetExtension(FileName);

        FileInfo file = new FileInfo(FileName);
        if (file.Exists)
        {
            Response.Clear();

            Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);

            Response.AddHeader("Content-Length", file.Length.ToString());

            Response.ContentType = ReturnExtension(file.Extension.ToLower());

            Response.TransmitFile(file.FullName);

            Response.End();
        }
    }


    protected void ddlProceso_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool noEncontro = true;
        string _aux = "";
        string[] sProcesos = _aux.Split(';');

        for (int i = 0; i < sProcesos.Count(); i++)
        {
            if (ddlProceso.SelectedValue.ToString() == sProcesos[i])
            {
                ddlUsuario.Enabled = true;
                ddlCiudadJuzgado.Enabled = true;
                txtJuzgado.Enabled = true;
                txtobservaciones.Enabled = true;
                noEncontro = false;
            }
        }

        // Si no encontro proceso entonces deshabilitar
        if (noEncontro)
        {

            ddlUsuario.Enabled = true;
            ddlCiudadJuzgado.Enabled = false;
            //ddlCiudadJuzgado.Items.Add(new ListItem("Seleccione un Item", "0"));
            ddlCiudadJuzgado.SelectedValue = "0";
            txtJuzgado.Enabled = false;
            txtobservaciones.Enabled = true;
        }
    }

    protected void btnModificarDiligencia_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Asesores.Entities.Diligencia vDiligencia = new Xpinn.Asesores.Entities.Diligencia();


            vDiligencia.codigo_diligencia = Convert.ToInt64(txtCodigo_diligencia.Text.Trim());
            txtNumero_radconsulta.Text = txtIdCredito.Text;
            vDiligencia.numero_radicacion = Convert.ToInt64(txtNumero_radconsulta.Text.Trim());

            vDiligencia.fecha_diligencia = Convert.ToDateTime(txtFecha_diliConsulta.Text.Trim());
            vDiligencia.tipo_diligencia = Convert.ToInt64(ddlTipoDiligenciaConsulta.SelectedValue);

            if (ddlTipoDiligenciaConsulta.SelectedItem.Text == "Carta Habeas Data" || ddlTipoDiligenciaConsulta.SelectedItem.Text == "Carta Cobro Prejurídico" || ddlTipoDiligenciaConsulta.SelectedItem.Text == "Carta Cobro Jurídico")
            {
                LblMensaje.Text = "Tipo de Diligencia no válido";
            }

            vDiligencia.tipo_contacto = Convert.ToInt64(ddlTipoContactoConsulta.SelectedValue);
            vDiligencia.atendio = Convert.ToString(txtAtendioConsulta.Text.Trim());
            vDiligencia.respuesta = Convert.ToString(txtRespuestaConsulta.Text.Trim());

            if (txtFecha_acuerdoConsulta.Text == "")
            {
                vDiligencia.valor_acuerdo = 0;
                vDiligencia.observacion = Convert.ToString(txtObservacionConsulta.Text.Trim());
                Usuario usuap = (Usuario)Session["usuario"];
                int cod = Convert.ToInt32(usuap.codusuario);
                vDiligencia.codigo_usuario_regis = cod;
            }

            if (txtAtendioConsulta.Text == "" || txtRespuestaConsulta.Text == "")
            {
                LblMensajeConsulta.Text = "Por favor ingrese persona que lo atendió y la respuesta";
            }
            else
            {

                if (chkApruebaConsulta.Checked)
                {
                    vDiligencia.acuerdo = 1;
                }
                else
                    vDiligencia.acuerdo = 0;
                if (chkApruebaConsulta.Checked == false)
                {
                    LblMensajeConsulta.Text = "Por favor active la casilla de acuerdo";
                }

                if (chkApruebaConsulta.Checked == true && txtFecha_acuerdoConsulta.Text == "")
                {
                    LblMensajeConsulta.Text = "Por favor ingrese una fecha de acuerdo";
                }
                else
                {
                    if (idObjeto != "")
                    {
                        vDiligencia.anexo = UploadConsulta();
                        if (chkApruebaConsulta.Checked == false && txtFecha_acuerdoConsulta.Text == "")
                        {
                            String Fechaacuerdo = "01/01/0001";
                            String format = "dd/MM/yyyy";
                            vDiligencia.fecha_acuerdo = DateTime.ParseExact(Fechaacuerdo, format, CultureInfo.InvariantCulture);
                            txtFecha_acuerdoConsulta.Text = Convert.ToString(vDiligencia.fecha_acuerdo).ToString();
                            vDiligencia.valor_acuerdo = 0;
                            txtValor_acuerdoConsulta.Text = Convert.ToString(vDiligencia.valor_acuerdo);
                        }
                        vDiligencia.fecha_acuerdo = Convert.ToDateTime(txtFecha_acuerdoConsulta.Text);
                        vDiligencia.valor_acuerdo = Convert.ToInt64(txtValor_acuerdoConsulta.Text.Trim().Replace(".", ""));
                        vDiligencia.observacion = Convert.ToString(txtObservacionConsulta.Text.Trim());
                        Usuario usuap = (Usuario)Session["usuario"];
                        int cod = Convert.ToInt32(usuap.codusuario);
                        vDiligencia.codigo_usuario_regis = cod;
                        vDiligencia.fecha_diligencia = Convert.ToDateTime(txtFecha_diliConsulta.Text);
                        if (chkApruebaConsulta.Checked)
                        {
                            if (vDiligencia.fecha_acuerdo < vDiligencia.fecha_diligencia && txtFecha_acuerdoConsulta.Text != "01/01/0001 12:00:00 a.m." || vDiligencia.fecha_acuerdo.ToLongTimeString() != vDiligencia.fecha_diligencia.ToLongTimeString())
                            {
                                LblMensajeConsulta.Text = "La Fecha de acuerdo no puede ser inferior a la fecha actual";
                            }
                        }
                        else
                        {
                            vDiligencia.anexo = UploadConsulta();
                            diligenciaServicio.ModificarDiligencia(vDiligencia, (Usuario)Session["usuario"]);
                            idObjeto = txtIdCredito.Text;
                            LblMensaje.Text = "";
                            LblMensajeConsulta.Text = "";
                            txtAtendioConsulta.Text = "";
                            txtFecha_acuerdoConsulta.Text = "";
                            txtValor_acuerdoConsulta.Text = "";
                            txtRespuestaConsulta.Text = "";
                            txtObservacionConsulta.Text = "";
                            //ddlTipoDiligenciaConsulta.SelectedValue = "1";
                            //ddlTipoContactoConsulta.SelectedValue = "1";
                            chkApruebaConsulta.Checked = false;
                        }
                    }
                }

                Session[diligenciaServicio.CodigoPrograma + ".id"] = idObjeto;
                lblCambiarArchivo.Visible = false;

                Actualizar(idObjeto);
                Diligencias();
                mpeNuevoActividad.Hide();

            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError("12." + ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(diligenciaServicio.CodigoPrograma, "btnGuardarReg_Click", ex);
        }
    }

    protected void BtnAgenda_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Page/Asesores/GestionarAgenda/Lista.aspx");
    }

    protected void chkApruebaConsulta_CheckedChanged(object sender, EventArgs e)
    {
        if (chkApruebaConsulta.Checked)
        {
            txtValor_acuerdoConsulta.Enabled = true;
            txtFecha_acuerdoConsulta.Enabled = true;
        }
        else
        {
            txtValor_acuerdoConsulta.Enabled = false;
            txtFecha_acuerdoConsulta.Enabled = false;
        }
        return;
    }

    protected void ddlTipoDiligenciaConsulta_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTipoDiligenciaConsulta.SelectedItem.Text == "Carta Habeas Data" || ddlTipoDiligenciaConsulta.SelectedItem.Text == "Carta Cobro Prejurídico" || ddlTipoDiligenciaConsulta.SelectedItem.Text == "Carta Cobro Jurídico")
        {

            ddlTipoDiligenciaConsulta.Enabled = false;
            ddlTipoContactoConsulta.Enabled = false;
            txtAnexo.Enabled = false;
            txtAtendioConsulta.Enabled = false;
            txtRespuestaConsulta.Enabled = false;
            txtValor_acuerdoConsulta.Enabled = false;
            //txtFecha_acuerdoConsulta.Enabled = false;
            txtObservacionConsulta.Enabled = false;
            txtCodigo_usuario_regis.Enabled = false;
            chkApruebaConsulta.Enabled = false;

            txtAtendioConsulta.Text = "";
            txtRespuestaConsulta.Text = "";
            txtValor_acuerdoConsulta.Text = "";
            //  txtFecha_acuerdoConsulta.Text = "";
            txtObservacionConsulta.Text = "";
            txtCodigo_usuario_regis.Text = "";

            FileUpload2.Enabled = false;
            btnModificarDiligencia.Visible = false;

        }
        else
        {

            ddlTipoContactoConsulta.Enabled = true;
            ddlTipoContacto.Enabled = true;
            txtAtendioConsulta.Enabled = true;
            txtRespuestaConsulta.Enabled = true;
            txtValor_acuerdoConsulta.Enabled = true;
            //txtFecha_acuerdoConsulta.Enabled = true;
            txtAnexo.Enabled = true;
            txtObservacionConsulta.Enabled = true;
            txtCodigo_usuario_regis.Enabled = true;
            chkApruebaConsulta.Enabled = true;
            FileUpload2.Enabled = true;
            btnModificarDiligencia.Visible = true;

        }

    }

    protected bool GenerarDocumento(Int64 pTipoDocumento)
    {
        return false;
    }

    protected void btnCartaHabeasData_Click(object sender, EventArgs e)
    {
        // Consulta datos del codeudor
        String pIdObjeto2 = txtIdCredito.Text;
        ObtenerDatosCodeudor(pIdObjeto2);

        // Variables del reporte para generar PDF
        byte[] bytes = null;
        mvReporte.Visible = true;
        mvReporte.ActiveViewIndex = 0;
        Warning[] warnings;
        String[] streamids;
        String mimetype;
        String encoding;
        String extension;
        string _sSuggestedName = String.Empty;

        // Datos de usuario para las consulta
        Int64 tipoDocumento = 1;
        if (IdCodeudor == "0")
            tipoDocumento = 1;
        else
            tipoDocumento = 2;
        if (GenerarDocumento(tipoDocumento) == false)
        {
            if (VerificarParametroHabeasData())
            {

                String espacio = " ";
                ReportParameter[] param = new ReportParameter[17];
                param[0] = new ReportParameter("pFecha", Convert.ToString(DateTime.Now.ToShortDateString()));
                param[1] = new ReportParameter("Pcliente", vacios(txtPrimerNombreCliente.Text));
                param[2] = new ReportParameter("Pdireccion", vacios(txtDireccionCliente.Text));
                param[3] = new ReportParameter("pTelefono", vacios(txtTelefonoCliente.Text));
                param[4] = new ReportParameter("pBarrio", vacios(txtBarrioCliente.Text));
                param[5] = new ReportParameter("pObligacion", vacios(txtIdCredito.Text));
                param[6] = new ReportParameter("pIdentificacionCliente", vacios(this.txtIdentificacionCliente.Text));
                param[7] = new ReportParameter("pdiasmora", vacios(txtdiasmora.Text));
                param[8] = new ReportParameter("pFechaPago", vacios(txtdiapago.Text));
                param[9] = new ReportParameter("pIdcodeudor", vacios(IdCodeudor));
                param[10] = new ReportParameter("pTipoDocumento", vacios(TipoDocumento));
                param[11] = new ReportParameter("pNumeroDocumento", vacios(NumeroDocumento));
                param[12] = new ReportParameter("pCodeudor", vacios(PrimerNombre + espacio + SegundoNombre + espacio + PrimerApellido + espacio + SegundoApellido));
                param[13] = new ReportParameter("pDireccioncod", vacios(Direccion));
                param[14] = new ReportParameter("pTelefonocod", vacios(Telefono));
                param[15] = new ReportParameter("pBarriocod", vacios(Barrio));
                param[16] = new ReportParameter("ImagenReport", ImagenReporte());
                ReportViewer1.Reset();
                if (IdCodeudor == "0")
                {
                    ReportViewer1.LocalReport.ReportPath = "Page/Asesores/Recuperacion/ReporteHabeasData.rdlc";
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.SetParameters(param);
                    ReportViewer1.LocalReport.Refresh();
                    bytes = this.ReportViewer1.LocalReport.Render("PDF", null, out mimetype, out encoding, out extension, out streamids, out warnings);
                }
                else
                {
                    ReportViewer1.LocalReport.ReportPath = "Page/Asesores/Recuperacion/ReporteHabeasData2pag.rdlc";
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.SetParameters(param);
                    ReportViewer1.LocalReport.Refresh();
                    bytes = this.ReportViewer1.LocalReport.Render("PDF", null, out mimetype, out encoding, out extension, out streamids, out warnings);
                }

                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                string ruta = Server.MapPath("~/Archivos/Cobros/");

                if (Directory.Exists(ruta))
                {
                    String Fecha = Convert.ToString(DateTime.Now.ToString("MMddyyyy"));
                    String fileName = "habeas-" + txtIdentificacionCliente.Text + "-" + Fecha + ".pdf";
                    string savePath = ruta + fileName;
                    FileStream fs = new FileStream(savePath, FileMode.Create);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    diligenciacartahabeas();

                    FileStream archivo = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                    FileInfo file = new FileInfo(savePath);
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.TransmitFile(file.FullName);
                    Response.End();

                }
            }
        }
    }

    private string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".htm":
            case ".csv":
                return "application/vnd.ms-excel";
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/ms-word";
            case ".docx":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":
                return "application/ms-excel";
            case ".xlsx":
                return "application/ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {

        Diligencias();

    }

    protected void ddlTipoContactoConsulta_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTipoContactoConsulta.SelectedItem.Text == "Cartas")
        {

            ddlTipoContactoConsulta.Enabled = false;
            btnModificarDiligencia.Visible = false;
        }
        else
        {
            ddlTipoContactoConsulta.Enabled = true;
        }
    }


    protected void txtNumero_radicacion_TextChanged(object sender, EventArgs e)
    {
        txtNumero_radicacion.Text = txtIdCredito.Text;
    }

    protected void btnGuardarCitacion_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Asesores.Entities.Diligencia vDiligencia = new Xpinn.Asesores.Entities.Diligencia();
            txtNumero_radicacion.Text = txtIdCredito.Text;
            vDiligencia.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text.Trim());
            txtFecha_diligencia.Text = DateTime.Now.ToString("dd/MM/yyyy");
            String FechaDiligencia = DateTime.Now.ToString("dd/MM/yyyy");
            String format = "dd/MM/yyyy";
            vDiligencia.fecha_diligencia = DateTime.ParseExact(FechaDiligencia, format, CultureInfo.InvariantCulture);
            vDiligencia.tipo_diligencia = 12;
            vDiligencia.tipo_contacto = 1;
            vDiligencia.atendio = Convert.ToString(txtAtendio.Text.Trim());
            vDiligencia.respuesta = "Generacion de Carta de Citacion";

            //  String FechaCitacion = "01/01/0001";
            vDiligencia.fecha_Citacion = Convert.ToDateTime(txtFechaCitacion.Text);
            if (idObjeto != "")
            {

                vDiligencia.anexo = Upload();
                if (chkAprueba.Checked == false && txtFecha_acuerdo.Text == "")
                {
                    String Fechaacuerdo = "01/01/0001";
                    String format1 = gFormatoFecha;
                    vDiligencia.fecha_acuerdo = DateTime.ParseExact(Fechaacuerdo, format1, CultureInfo.InvariantCulture);
                    txtFecha_acuerdo.Text = Convert.ToString(vDiligencia.fecha_acuerdo).ToString();
                    vDiligencia.valor_acuerdo = 0;
                    txtValor_acuerdo.Text = Convert.ToString(vDiligencia.valor_acuerdo);
                }

                vDiligencia.fecha_acuerdo = Convert.ToDateTime(txtFecha_acuerdo.Text);

                if (txtValor_acuerdo.Text == "")
                    vDiligencia.valor_acuerdo = 0;
                else
                    vDiligencia.valor_acuerdo = Convert.ToInt64(txtValor_acuerdo.Text.Trim().Replace(".", ""));
                vDiligencia.observacion = Convert.ToString(txtObservacion.Text.Trim());
                vDiligencia.fecha_Citacion = Convert.ToDateTime(txtFechaCitacion.Text);

                Usuario usuap = (Usuario)Session["usuario"];
                int cod = Convert.ToInt32(usuap.codusuario);
                vDiligencia.codigo_usuario_regis = cod;
                String FechaDiligencia2 = DateTime.Now.ToString(gFormatoFecha);
                vDiligencia.fecha_Citacion = Convert.ToDateTime(txtFechaCitacion.Text);
                diligenciaServicio.CrearDiligencia(vDiligencia, (Usuario)Session["usuario"]);
                if (vDiligencia.codigo_diligencia > 0)
                    LblMensajeConsulta.Visible = true;
                idObjeto = txtIdCredito.Text;
                LblMensaje.Text = "";
                txtAtendio.Text = "";
                txtFecha_acuerdo.Text = "";
                txtValor_acuerdo.Text = "";
                txtRespuesta.Text = "";
                txtObservacion.Text = "";
                try { ddlTipoContacto.SelectedValue = "0"; }
                catch { }
                chkAprueba.Checked = false;
            }
            Session[diligenciaServicio.CodigoPrograma + ".id"] = idObjeto;
            Actualizar(idObjeto);
            Diligencias();
            ImprimirCitacion();

        }
        catch (ExceptionBusiness ex)
        {
            VerError("13." + ex.Message);
        }
    }

    protected void BtnCerrarInforme_Click(object sender, EventArgs e)
    {
        mvReporte.Visible = false;
    }

    protected void BtnAcuerdoPagos_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Asesores.Entities.Diligencia vDiligencia = new Xpinn.Asesores.Entities.Diligencia();
            txtNumero_radicacion.Text = txtIdCredito.Text;
            vDiligencia.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text.Trim());
            txtFecha_diligencia.Text = DateTime.Now.ToString(gFormatoFecha);
            String FechaDiligencia = DateTime.Now.ToString(gFormatoFecha);
            String format = gFormatoFecha;
            vDiligencia.fecha_diligencia = DateTime.ParseExact(FechaDiligencia, format, CultureInfo.InvariantCulture);
            vDiligencia.tipo_diligencia = 13;
            vDiligencia.tipo_contacto = 1;
            vDiligencia.atendio = Convert.ToString(txtAtendio.Text.Trim());
            vDiligencia.respuesta = "Generacion de Acuerdo de pago";

            vDiligencia.fecha_Citacion = Convert.ToDateTime(txtFechaCitacion.Text);
            if (idObjeto != "")
            {
                vDiligencia.anexo = Upload();
                if (chkAprueba.Checked == false && txtFecha_acuerdo.Text == "")
                {
                    String Fechaacuerdo = "01/01/0001";
                    String format1 = gFormatoFecha;
                    vDiligencia.fecha_acuerdo = DateTime.ParseExact(Fechaacuerdo, format1, CultureInfo.InvariantCulture);
                    txtFecha_acuerdo.Text = Convert.ToString(vDiligencia.fecha_acuerdo).ToString();
                    vDiligencia.valor_acuerdo = 0;
                    txtValor_acuerdo.Text = Convert.ToString(vDiligencia.valor_acuerdo);
                }

                vDiligencia.fecha_acuerdo = Convert.ToDateTime(txtFecha_acuerdo.Text);
                if (txtValor_acuerdo.Text == "")
                    vDiligencia.valor_acuerdo = 0;
                else
                    vDiligencia.valor_acuerdo = Convert.ToInt64(txtValor_acuerdo.Text.Trim().Replace(".", ""));
                vDiligencia.observacion = Convert.ToString(txtObservacion.Text.Trim());
                vDiligencia.fecha_Citacion = Convert.ToDateTime(txtFechaCitacion.Text);


                Usuario usuap = (Usuario)Session["usuario"];
                int cod = Convert.ToInt32(usuap.codusuario);

                vDiligencia.codigo_usuario_regis = cod;

                String FechaDiligencia2 = DateTime.Now.ToString(gFormatoFecha);
                vDiligencia.fecha_Citacion = Convert.ToDateTime(txtFechaCitacion.Text);

                diligenciaServicio.CrearDiligencia(vDiligencia, (Usuario)Session["usuario"]);
                idObjeto = txtIdCredito.Text;
                LblMensaje.Text = "";
                txtAtendio.Text = "";
                txtFecha_acuerdo.Text = "";
                txtValor_acuerdo.Text = "";
                txtRespuesta.Text = "";
                txtObservacion.Text = "";
                try
                {
                    ddlTipoContacto.SelectedValue = "0";
                }
                catch
                { }
                chkAprueba.Checked = false;

            }
            Session[diligenciaServicio.CodigoPrograma + ".id"] = idObjeto;
            Actualizar(idObjeto);
            Diligencias();

            ////Genera la citacion
            Usuario usuariop = (Usuario)Session["usuario"];

            String pIdObjeto2 = txtIdCredito.Text;
            byte[] bytes;
            mvReporte.Visible = true;
            mvReporte.ActiveViewIndex = 0;

            Warning[] warnings;
            String[] streamids;
            String mimetype;
            String encoding;
            String extension;
            string _sSuggestedName = String.Empty;

            //String espacio = " ";
            ReportParameter[] param = new ReportParameter[2];
            param[0] = new ReportParameter("pFecha", Convert.ToString(DateTime.Now.ToShortDateString()));
            param[1] = new ReportParameter("ImagenReport", ImagenReporte());
            ReportViewer1.Reset();
            ReportViewer1.LocalReport.ReportPath = "Page/Asesores/Recuperacion/AcuerdoPago.rdlc";
            ReportViewer1.LocalReport.EnableExternalImages = true;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.SetParameters(param);
            ReportViewer1.LocalReport.Refresh();
            bytes = this.ReportViewer1.LocalReport.Render("PDF", null, out mimetype, out encoding, out extension, out streamids, out warnings);

            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
            string ruta = Server.MapPath("~/Archivos/Cobros/");
            if (Directory.Exists(ruta))
            {
                String Fecha = Convert.ToString(DateTime.Now.ToString("MMddyyyy"));
                String fileName = "acuerdo-" + txtIdentificacionCliente.Text + "-" + Fecha + ".pdf";
                string savePath = ruta + fileName;
                FileStream fs = new FileStream(savePath, FileMode.Create);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                FileStream archivo = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                FileInfo file = new FileInfo(savePath);
                Response.Clear();
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/pdf";
                Response.TransmitFile(file.FullName);
                //Response.End();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError("14." + ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(diligenciaServicio.CodigoPrograma, "BtnAcuerdoPagos_Click", ex);
        }

    }

    protected void btnVisita_Click(object sender, EventArgs e)
    {
        String pIdObjeto2 = txtIdCredito.Text;
        ObtenerDatosCodeudor(pIdObjeto2);
        byte[] bytes;

        Warning[] warnings;
        String[] streamids;
        String mimetype;
        String encoding;
        String extension;
        string _sSuggestedName = String.Empty;
        // Datos de usuario para las consulta
        Int64 tipoDocumento = 1;
        if (IdCodeudor == "0")
            tipoDocumento = 5;
        else
            tipoDocumento = 6;

        if (GenerarDocumento(tipoDocumento) == false)
        {
            if (VerificarParametroVisitaAbogado())
            {
                mvReporte.Visible = true;
                mvReporte.ActiveViewIndex = 0;

                String espacio = " ";
                ReportParameter[] param = new ReportParameter[19];
                param[0] = new ReportParameter("pYear", Convert.ToString(DateTime.Now.ToString("yyyy")));
                param[1] = new ReportParameter("pMes", Convert.ToString(DateTime.Now.ToString("MMMM")));
                param[2] = new ReportParameter("pDia", Convert.ToString(DateTime.Now.Day));
                param[3] = new ReportParameter("Pcliente", vacios(txtPrimerNombreCliente.Text));
                param[4] = new ReportParameter("Pdireccion", vacios(txtDireccionCliente.Text));
                param[5] = new ReportParameter("pTelefono", vacios(txtTelefonoCliente.Text));
                param[6] = new ReportParameter("pBarrio", vacios(this.txtBarrioCliente.Text));
                param[7] = new ReportParameter("pObligacion", vacios(txtIdCredito.Text));
                param[8] = new ReportParameter("pIdentificacionCliente", vacios(this.txtIdentificacionCliente.Text));
                param[9] = new ReportParameter("pPagare", vacios(txtpagare.Text));
                param[10] = new ReportParameter("pIdcodeudor", vacios(IdCodeudor));
                param[11] = new ReportParameter("pTipoDocumento", vacios(TipoDocumento));
                param[12] = new ReportParameter("pNumeroDocumento", vacios(NumeroDocumento));
                param[13] = new ReportParameter("pCodeudor", vacios(PrimerNombre + espacio + SegundoNombre + espacio + PrimerApellido + espacio + SegundoApellido));
                param[14] = new ReportParameter("pDireccioncod", vacios(Direccion));
                param[15] = new ReportParameter("pTelefonocod", vacios(Telefono));
                param[16] = new ReportParameter("pBarriocod", vacios(Barrio));
                ReportViewer1.Reset();
                if (IdCodeudor != "0")
                {
                    param[17] = new ReportParameter("pTieneCod", vacios("c.c.Copia - Codeudor"));
                    param[18] = new ReportParameter("ImagenReport", ImagenReporte());

                    ReportViewer1.LocalReport.ReportPath = "Page/Asesores/Recuperacion/VisitaAbogado2.rdlc";
                    ReportViewer1.LocalReport.Refresh();
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.SetParameters(param);
                    ReportViewer1.LocalReport.Refresh();
                    bytes = this.ReportViewer1.LocalReport.Render("PDF", null, out mimetype, out encoding, out extension, out streamids, out warnings);

                }
                else
                {
                    param[17] = new ReportParameter("pTieneCod", vacios(""));
                    param[18] = new ReportParameter("ImagenReport", ImagenReporte());
                    ReportViewer1.LocalReport.ReportPath = "Page/Asesores/Recuperacion/VisitaAbogado.rdlc";
                    ReportViewer1.LocalReport.Refresh();
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.SetParameters(param);
                    ReportViewer1.LocalReport.Refresh();
                    bytes = this.ReportViewer1.LocalReport.Render("PDF", null, out mimetype, out encoding, out extension, out streamids, out warnings);

                }

                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                string ruta = Server.MapPath("~/Archivos/Cobros/");

                if (Directory.Exists(ruta))
                {
                    String Fecha = Convert.ToString(DateTime.Now.ToString("MMddyyyy"));
                    String fileName = "visita-" + txtIdentificacionCliente.Text + "-" + Fecha + ".pdf";
                    string savePath = ruta + fileName;
                    FileStream fs = new FileStream(savePath, FileMode.Create);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    diligenciaVisitaAbogado();
                    FileStream archivo = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                    FileInfo file = new FileInfo(savePath);
                    Response.Clear();
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/pdf";
                    Response.TransmitFile(file.FullName);
                    Response.End();
                }
            }
        }
    }

    protected void btnCobroPreJuridico2_Click(object sender, EventArgs e)
    {
        String pIdObjeto2 = txtIdCredito.Text;
        ObtenerDatosCodeudor(pIdObjeto2);
        byte[] bytes;

        Warning[] warnings;
        String[] streamids;
        String mimetype;
        String encoding;
        String extension;
        string _sSuggestedName = String.Empty;

        // Datos de usuario para las consulta
        Int64 tipoDocumento = 1;
        if (IdCodeudor == "0")
            tipoDocumento = 5;
        else
            tipoDocumento = 6;

        if (GenerarDocumento(tipoDocumento) == false)
        {
            if (VerificarParametroCobroPreJuridico2())
            {
                mvReporte.Visible = true;
                mvReporte.ActiveViewIndex = 0;
                String espacio = " ";
                ReportParameter[] param = new ReportParameter[19];
                param[0] = new ReportParameter("pYear", Convert.ToString(DateTime.Now.ToString("yyyy")));
                param[1] = new ReportParameter("pMes", Convert.ToString(DateTime.Now.ToString("MMMM")));
                param[2] = new ReportParameter("pDia", Convert.ToString(DateTime.Now.Day));
                param[3] = new ReportParameter("Pcliente", vacios(txtPrimerNombreCliente.Text));
                param[4] = new ReportParameter("Pdireccion", vacios(txtDireccionCliente.Text));
                param[5] = new ReportParameter("pTelefono", vacios(txtTelefonoCliente.Text));
                param[6] = new ReportParameter("pBarrio", vacios(this.txtBarrioCliente.Text));
                param[7] = new ReportParameter("pObligacion", vacios(txtIdCredito.Text));
                param[8] = new ReportParameter("pIdentificacionCliente", vacios(this.txtIdentificacionCliente.Text));
                param[9] = new ReportParameter("pPagare", vacios(txtpagare.Text));
                param[10] = new ReportParameter("pIdcodeudor", vacios(IdCodeudor));
                param[11] = new ReportParameter("pTipoDocumento", vacios(TipoDocumento));
                param[12] = new ReportParameter("pNumeroDocumento", vacios(NumeroDocumento));
                param[13] = new ReportParameter("pCodeudor", vacios(PrimerNombre + espacio + SegundoNombre + espacio + PrimerApellido + espacio + SegundoApellido));
                param[14] = new ReportParameter("pDireccioncod", vacios(Direccion));
                param[15] = new ReportParameter("pTelefonocod", vacios(Telefono));
                param[16] = new ReportParameter("pBarriocod", vacios(Barrio));
                ReportViewer1.Reset();
                if (IdCodeudor != "0")
                {
                    param[17] = new ReportParameter("pTieneCod", vacios("c.c.Copia - Codeudor"));
                    param[18] = new ReportParameter("ImagenReport", ImagenReporte());
                    ReportViewer1.LocalReport.ReportPath = "Page/Asesores/Recuperacion/ReporteCobroJuridico.rdlc";
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.SetParameters(param);
                    ReportViewer1.LocalReport.Refresh();
                    bytes = this.ReportViewer1.LocalReport.Render("PDF", null, out mimetype, out encoding, out extension, out streamids, out warnings);

                }
                else
                {
                    param[17] = new ReportParameter("pTieneCod", vacios(""));
                    param[18] = new ReportParameter("ImagenReport", ImagenReporte());
                    ReportViewer1.LocalReport.ReportPath = "Page/Asesores/Recuperacion/ReporteCobroJuridico.rdlc";
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.SetParameters(param);
                    ReportViewer1.LocalReport.Refresh();
                    bytes = this.ReportViewer1.LocalReport.Render("PDF", null, out mimetype, out encoding, out extension, out streamids, out warnings);

                }
                ReportViewer1.Dispose();

                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                string ruta = Server.MapPath("~/Archivos/Cobros/");

                if (Directory.Exists(ruta))
                {
                    String Fecha = Convert.ToString(DateTime.Now.ToString("MMddyyyy"));
                    String fileName = "prejuridico-2-" + txtIdentificacionCliente.Text + "-" + Fecha + ".pdf";
                    string savePath = ruta + fileName;
                    FileStream fs = new FileStream(savePath, FileMode.Create);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    diligenciaCobroPrejuridico2();
                    FileStream archivo = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                    FileInfo file = new FileInfo(savePath);
                    Response.Clear();
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/pdf";
                    Response.TransmitFile(file.FullName);
                    Response.End();


                }
            }
        }
    }

    protected void btnCampaña_Click(object sender, EventArgs e)
    {
        String pIdObjeto2 = txtIdCredito.Text;
        ObtenerDatosCodeudor(pIdObjeto2);
        byte[] bytes;

        Warning[] warnings;
        String[] streamids;
        String mimetype;
        String encoding;
        String extension;
        string _sSuggestedName = String.Empty;

        // Datos de usuario para las consulta
        Int64 tipoDocumento = 1;
        if (IdCodeudor == "0")
            tipoDocumento = 7;
        else
            tipoDocumento = 7;

        if (GenerarDocumento(tipoDocumento) == false)
        {
            if (VerificarParametroCampaña())
            {
                mvReporte.Visible = true;
                mvReporte.ActiveViewIndex = 0;

                String espacio = " ";
                ReportParameter[] param = new ReportParameter[19];
                param[0] = new ReportParameter("pYear", Convert.ToString(DateTime.Now.ToString("yyyy")));
                param[1] = new ReportParameter("pMes", Convert.ToString(DateTime.Now.ToString("MMMM")));
                param[2] = new ReportParameter("pDia", Convert.ToString(DateTime.Now.Day));
                param[3] = new ReportParameter("Pcliente", vacios(txtPrimerNombreCliente.Text));
                param[4] = new ReportParameter("Pdireccion", vacios(txtDireccionCliente.Text));
                param[5] = new ReportParameter("pTelefono", vacios(txtTelefonoCliente.Text));
                param[6] = new ReportParameter("pBarrio", vacios(this.txtBarrioCliente.Text));
                param[7] = new ReportParameter("pObligacion", vacios(txtIdCredito.Text));
                param[8] = new ReportParameter("pIdentificacionCliente", vacios(this.txtIdentificacionCliente.Text));
                param[9] = new ReportParameter("pPagare", vacios(txtpagare.Text));
                param[10] = new ReportParameter("pIdcodeudor", vacios(IdCodeudor));
                param[11] = new ReportParameter("pTipoDocumento", vacios(TipoDocumento));
                param[12] = new ReportParameter("pNumeroDocumento", vacios(NumeroDocumento));
                param[13] = new ReportParameter("pCodeudor", vacios(PrimerNombre + espacio + SegundoNombre + espacio + PrimerApellido + espacio + SegundoApellido));
                param[14] = new ReportParameter("pDireccioncod", vacios(Direccion));
                param[15] = new ReportParameter("pTelefonocod", vacios(Telefono));
                param[16] = new ReportParameter("pBarriocod", vacios(Barrio));
                ReportViewer1.Reset();
                if (IdCodeudor != "0")
                {
                    param[17] = new ReportParameter("pTieneCod", vacios("c.c.Copia - Codeudor"));
                    param[18] = new ReportParameter("ImagenReport", ImagenReporte());
                    ReportViewer1.LocalReport.ReportPath = "Page/Asesores/Recuperacion/Campaña.rdlc";
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.SetParameters(param);
                    ReportViewer1.LocalReport.Refresh();
                    bytes = this.ReportViewer1.LocalReport.Render("PDF", null, out mimetype, out encoding, out extension, out streamids, out warnings);

                }
                else
                {
                    param[17] = new ReportParameter("pTieneCod", vacios(""));
                    param[18] = new ReportParameter("ImagenReport", ImagenReporte());
                    ReportViewer1.LocalReport.ReportPath = "Page/Asesores/Recuperacion/Campaña.rdlc";
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.SetParameters(param);
                    ReportViewer1.LocalReport.Refresh();
                    bytes = this.ReportViewer1.LocalReport.Render("PDF", null, out mimetype, out encoding, out extension, out streamids, out warnings);
                }
                ReportViewer1.Dispose();

                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                string ruta = Server.MapPath("~/Archivos/Cobros/");

                if (Directory.Exists(ruta))
                {
                    String Fecha = Convert.ToString(DateTime.Now.ToString("MMddyyyy"));
                    String fileName = "Campaña-" + txtIdentificacionCliente.Text + "-" + Fecha + ".pdf";
                    string savePath = ruta + fileName;
                    FileStream fs = new FileStream(savePath, FileMode.Create);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();

                    diligenciaVisitaCampaña();
                    FileStream archivo = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                    FileInfo file = new FileInfo(savePath);
                    Response.Clear();
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/pdf";
                    Response.TransmitFile(file.FullName);
                    Response.End();
                }
            }

        }
    }


    protected void ddlTipoDoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        DatosDeDocumentoService datosDeDocumentoServicio = new DatosDeDocumentoService();
        List<DatosDeDocumento> lstDatosDeDocumento = new List<DatosDeDocumento>();
        TiposDocCobranzasServices TipDocCobranzas = new TiposDocCobranzasServices();
        TiposDocCobranzas TipDocumentoCobranza = new TiposDocCobranzas();

        string cTipoDocumento = ddlTipoDoc.SelectedItem.Text;
        string cDocsSubDir = "Documentos";
        string pathFolder = Server.MapPath("~/Page/Asesores/Recuperacion/" + cDocsSubDir);
        string cDocumentoGenerado = Server.MapPath("~/Page/Asesores/Recuperacion/" + cDocsSubDir + "/" + txtNumero_radicacion.Text.Trim() + "_" + cTipoDocumento + '.' + 'p' + 'd' + 'f');
        //Si no existe la carpeta, entonces la crea
        if (!Directory.Exists(pathFolder))
            Directory.CreateDirectory(pathFolder);

        TipDocumentoCobranza = TipDocCobranzas.ConsultarTiposDocumento(Convert.ToInt32(ddlTipoDoc.SelectedItem.Value), (Usuario)Session["Usuario"]);
        lstDatosDeDocumento = datosDeDocumentoServicio.ListarDatosDeDocumentoFormatoCartasMasivas(Convert.ToInt64(txtIdCredito.Text), (Usuario)Session["usuario"]);
        DatosDeDocumento datosDeDocumento = new DatosDeDocumento();
        datosDeDocumento.Id = 999;
        datosDeDocumento.Campo = "FechaCitacion";
        datosDeDocumento.Valor = txtFechaCitacion.Text;
        lstDatosDeDocumento.Add(datosDeDocumento);

        ProcessesHTML.ReemplazarEnDocumentoDeWordYGuardarPdf(
            TipDocumentoCobranza.Textos == null
                ? TipDocumentoCobranza.texto
                : Encoding.ASCII.GetString(TipDocumentoCobranza.Textos), lstDatosDeDocumento, cDocumentoGenerado, ref pError);

        if (!string.IsNullOrEmpty(pError))
            VerError(pError);
        //Descargando el Archivo PDF
      
        FileInfo file = new FileInfo(cDocumentoGenerado);
        var sjs = File.ReadAllBytes(cDocumentoGenerado);
        Response.AddHeader("Content-Disposition", "attachment; filename= CartaCobranza.pdf");
        Response.AddHeader("Content-Length", sjs.Length.ToString());
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(sjs);
        Response.Flush();
        Response.Close();

    }
    public string getBetween(string strSource, string strStart, string strEnd)
    {
        int Start, End;
        if (strSource.Contains(strStart) && strSource.Contains(strEnd))
        {
            Start = strSource.IndexOf(strStart, 0) + strStart.Length;
            End = strSource.IndexOf(strEnd, Start);
            try
            {
                return strSource.Substring(Start, End - Start);
            }
            catch (Exception)
            {

                return "";
            }

        }
        else
        {
            return "";
        }
    }
}