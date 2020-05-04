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
using Xpinn.Contabilidad.Entities;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Drawing.Printing;
using Cantidad_a_Letra;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
    private Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
    Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();

    List<Xpinn.Contabilidad.Entities.ProcesoContable> LstProcesoContable;

    String operacion = "";
    String tipobeneficiario;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Request.QueryString["tipo_comp"] != null)
            {
                if (Request.QueryString["tipo_comp"].ToString().Trim() == "0")
                {
                    ComprobanteServicio.CodigoPrograma = ComprobanteServicio.CodigoProgramaContable;
                }
                else if (Request.QueryString["tipo_comp"].ToString().Trim() == "1")
                {
                    ComprobanteServicio.CodigoPrograma = ComprobanteServicio.CodigoProgramaIngreso;
                }
                else if (Request.QueryString["tipo_comp"].ToString().Trim() == "5")
                {
                    ComprobanteServicio.CodigoPrograma = ComprobanteServicio.CodigoProgramaEgreso;
                }
                else
                {
                    ComprobanteServicio.CodigoPrograma = ComprobanteServicio.CodigoProgramaContable;
                }
            }

            if (Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] != null & Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] != null)
                VisualizarOpciones(ComprobanteServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(ComprobanteServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{           

        if (!IsPostBack)
        {

            // Permite el control del evento de modificación de datos en la identificación del beneficiario y del tercero
            //string js = "javascript:" + Page.GetPostBackEventReference(this, "@@@@@buttonPostBack") + ";";
            //txtIdentificacion.Attributes.Add("onchange", js);
            //txtIdentificD.Attributes.Add("onchange", js);

            // Determinar el separador décimal
            string s = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            Configuracion conf = new Configuracion();

            // Coloca la fecha actual por defecto a los comprobantes
            if (ddlTipoComprobante.SelectedValue == "5")
            {
                txtFecha.Text = System.DateTime.Now.ToString(conf.ObtenerFormatoFecha());
                PanelFooter.Visible = true;
            }
            else
            {
                txtFecha.Text = System.DateTime.Now.ToShortDateString();
                PanelFooter.Visible = false;
            }

            // Si ya se determinó los datos del banco, cuenta y número de cheque entonces asignarlos
            if (Session["numerocheque"] != null && Session["entidad"] != null && Session["cuenta"] != null)
            {
                txtNumSop.Text = Session["numerocheque"].ToString();
                ddlEntidad.SelectedValue = Session["entidad"].ToString();
                ddlEntidad5.SelectedValue = Session["entidad"].ToString();
                ddlCuenta.SelectedValue = Session["cuenta"].ToString();
                ddlEntidad.Enabled = false;
                ddlEntidad5.Enabled = false;
                ddlCuenta.Enabled = false;
            }

            // Llenar los DDL para poder seleccionar los datos
            LlenarCombos();

            // Si hay cuentas bancarias a nombre de la entidad las muestras
            ActivarDDLCuentas();

            //Se valida si se esta modificando un comprobante
            if (Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] != null & Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] != null)
            {
                Usuario usuap = (Usuario)Session["usuario"];
                string idNComp = Session[ComprobanteServicio.CodigoPrograma + ".num_comp"].ToString();
                string idTComp = Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"].ToString();
                Session.Remove(ComprobanteServicio.CodigoPrograma + ".num_comp");
                Session.Remove(ComprobanteServicio.CodigoPrograma + ".tipo_comp");
                idObjeto = idNComp;
                ObtenerDatos(idNComp, idTComp);
                mvComprobante.ActiveViewIndex = 1;
                tbxOficina.Text = usuap.nombre_oficina;
            }
            else
            {
                // Se valida si el comprobante nuevo corresponde a un comprobante generado o no.
                if (Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] != null)
                {
                    if (Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"].ToString() != "1")
                        Session[ComprobanteServicio.CodigoPrograma + ".CuentaBancaria"] = null;

                    Usuario usuap = (Usuario)Session["usuario"];
                    mvComprobante.ActiveViewIndex = 3;
                    Int64 pcod_ope = 0;
                    if (Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] != null)
                        pcod_ope = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"]);
                    Int64 ptip_ope = 0;
                    if (Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] != null)
                        ptip_ope = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"]);
                    DateTime pfecha = System.DateTime.Now;
                    if (Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] != null)
                        pfecha = Convert.ToDateTime(Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"]);
                    Int64 pcod_ofi = usuap.cod_oficina;
                    if (Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] != null)
                        pcod_ofi = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"]);
                    LstProcesoContable = ComprobanteServicio.ConsultaProceso(pcod_ope, ptip_ope, pfecha, usuap);
                    if (LstProcesoContable.Count() == 0)
                    {
                        VerError("No existen comprobantes parametrizados para esta operación " + ptip_ope);
                        ListItem selectedListItem = ddlTipoOperacion.Items.FindByValue(ptip_ope.ToString());
                        if (selectedListItem != null)
                        {
                            selectedListItem.Selected = true;
                            ddlTipoOperacion.Enabled = false;
                        }
                        Site toolBar = (Site)this.Master;
                        toolBar.MostrarGuardar(false);
                        mvComprobante.ActiveViewIndex = 6;
                    }
                    if (LstProcesoContable.Count() > 1)
                    {
                        lstProcesos.DataTextField = "descripcion";
                        lstProcesos.DataValueField = "cod_proceso";
                        lstProcesos.DataSource = LstProcesoContable;
                        lstProcesos.DataBind();
                    }
                    if (LstProcesoContable.Count() == 1)
                    {
                        GenerarComprobante(pcod_ope, ptip_ope, pfecha, pcod_ofi);
                    }
                }
                else
                {
                    // Se crea un detalle vacio para activar botones de la grilla
                    CrearDetalleInicial(0);
                    // Mostrar datos para seleccionar el tipo de comprobante cuando es elaboraciòn
                    string sParametros = Request.QueryString["tipo_comp"];
                    if (sParametros != null)
                    {
                        Session.Remove("Comprobantecopia");
                        if (sParametros.Trim() == "0")
                        {
                            rbContable.Checked = true;
                            mvComprobante.ActiveViewIndex = 1;
                            Activar();
                        }
                        else if (sParametros.Trim() == "1")
                        {
                            rbIngreso.Checked = true;
                            mvComprobante.ActiveViewIndex = 1;
                            Activar();
                        }
                        else if (sParametros.Trim() == "5")
                        {
                            rbEgreso.Checked = true;
                            mvComprobante.ActiveViewIndex = 1;
                            Activar();
                        }
                        else
                        {
                            mvComprobante.ActiveViewIndex = 0;
                        }
                    }
                }
                // Se valida si se va a copiar el comprobante 
                if ((String)Session["Comprobantecopia"] != null)
                {
                    String Comprobantecopia = (String)Session["Comprobantecopia"];
                    Usuario usuap = (Usuario)Session["usuario"];
                    string idNComp = (String)Session[ComprobanteServicio.CodigoProgramaCarga + ".num_comp"];
                    string idTComp = (String)Session[ComprobanteServicio.CodigoProgramaCarga + ".tipo_comp"];
                    Session.Remove(ComprobanteServicio.CodigoProgramaCarga + ".num_comp");
                    Session.Remove(ComprobanteServicio.CodigoProgramaCarga + ".tipo_comp");
                    idObjeto = idNComp;
                    ObtenerDatosCopia(idNComp, idTComp);
                    mvComprobante.ActiveViewIndex = 1;
                    tbxOficina.Text = usuap.nombre_oficina;
                    Activar();
                }
                mpeNuevo.Hide();
            }
            operacion = (String)Session["operacion"];
            if (operacion == null)
            {
                operacion = "";
            }
        }
        else
        {
            CalcularTotal();
        }


        //}
        //catch (ExceptionBusiness ex)
        //{
        //    VerError(ex.Message);
        //}
        //catch (Exception ex)
        //{
        //    BOexcepcion.Throw(ComprobanteServicio.CodigoPrograma, "Page_Load", ex);
        //}

    }


    protected void GenerarComprobante(Int64 pcod_ope, Int64 ptip_ope, DateTime pfecha, Int64 pcod_ofi)
    {
        Usuario usuap = (Usuario)Session["usuario"];

        try
        {
            tbxOficina.Text = usuap.nombre_oficina;
            // Generar el comprobante
            Int64 pcod_proceso = Convert.ToInt64(LstProcesoContable[0].cod_proceso);
            Int64 pnum_comp = 0;
            Int64 ptipo_comp = 0;
            Int64 pcod_persona = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"]);
            string pError = "";
            if (ComprobanteServicio.GenerarComprobante(pcod_ope, ptip_ope, pfecha, pcod_ofi, pcod_persona, pcod_proceso, ref pnum_comp, ref ptipo_comp, ref pError, usuap) == true)
            {
                // Asignar el número de comprobante generado
                idObjeto = pnum_comp.ToString();
                // Cargar los datos del comprobante generado
                ObtenerDatos(pnum_comp.ToString(), ptipo_comp.ToString());
                // Si se maneja una cuenta bancaria desde el proceso entonces asignarla
                if (Session[ComprobanteServicio.CodigoPrograma + ".CuentaBancaria"] != null)
                    ddlCuenta.SelectedItem.Text = Convert.ToString(Session[ComprobanteServicio.CodigoPrograma + ".CuentaBancaria"]);
                // Ir a la pantalla del comprobante
                mvComprobante.ActiveViewIndex = 1;
                // Asignar la oficina a la que pertenezca el usuario
                tbxOficina.Text = usuap.nombre_oficina;
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message + " Cod.Ope:" + pcod_ope + " Tipo Ope:" + ptip_ope + " Fecha:" + pfecha);
            mvComprobante.ActiveViewIndex = 5;
        }
    }

    /// <summary>
    /// Método para llenar los combos
    /// </summary>
    protected void LlenarCombos()
    {
        Usuario usuario = new Usuario();
        usuario = (Usuario)Session["Usuario"];

        Xpinn.Caja.Services.TipoIdenService IdenService = new Xpinn.Caja.Services.TipoIdenService();
        Xpinn.Caja.Entities.TipoIden identi = new Xpinn.Caja.Entities.TipoIden();
        ddlTipoIdentificacion.DataSource = IdenService.ListarTipoIden(identi, usuario);
        ddlTipoIdentificacion.DataTextField = "descripcion";
        ddlTipoIdentificacion.DataValueField = "codtipoidentificacion";
        ddlTipoIdentificacion.DataBind();

        ddlTipoIdentificacion0.DataSource = IdenService.ListarTipoIden(identi, usuario);
        ddlTipoIdentificacion0.DataTextField = "descripcion";
        ddlTipoIdentificacion0.DataValueField = "codtipoidentificacion";
        ddlTipoIdentificacion0.DataBind();

        Xpinn.Contabilidad.Services.TipoComprobanteService TipoComprobanteService = new Xpinn.Contabilidad.Services.TipoComprobanteService();
        Xpinn.Contabilidad.Entities.TipoComprobante TipoComprobante = new Xpinn.Contabilidad.Entities.TipoComprobante();

        ddlTipoComprobante.DataSource = TipoComprobanteService.ListarTipoComprobante(TipoComprobante, "", (Usuario)Session["Usuario"]);
        ddlTipoComprobante.DataTextField = "descripcion";
        ddlTipoComprobante.DataValueField = "tipo_comprobante";
        ddlTipoComprobante.DataBind();

        ddlTipoComp.DataSource = ddlTipoComprobante.DataSource;
        ddlTipoComp.DataTextField = "descripcion";
        ddlTipoComp.DataValueField = "tipo_comprobante";
        ddlTipoComp.DataBind();

        Xpinn.Caja.Services.TipoPagoService TipoPagoService = new Xpinn.Caja.Services.TipoPagoService();
        Xpinn.Caja.Entities.TipoPago TipoPago = new Xpinn.Caja.Entities.TipoPago();
        ddlFormaPago.DataSource = TipoPagoService.ListarTipoPago(TipoPago, usuario);
        ddlFormaPago.DataTextField = "descripcion";
        ddlFormaPago.DataValueField = "cod_tipo_pago";
        ddlFormaPago.DataBind();

        Xpinn.Caja.Services.BancosService BancosService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos Bancos = new Xpinn.Caja.Entities.Bancos();
        ddlEntidad.DataSource = BancosService.ListarBancos(Bancos, usuario);
        ddlEntidad.DataTextField = "nombrebanco";
        ddlEntidad.DataValueField = "cod_banco";
        ddlEntidad.DataBind();

        Xpinn.Caja.Services.CiudadService CiudadService = new Xpinn.Caja.Services.CiudadService();
        Xpinn.Caja.Entities.Ciudad Ciudad = new Xpinn.Caja.Entities.Ciudad();
        ddlCiudad.DataSource = CiudadService.ListadoCiudad(Ciudad, usuario);
        ddlCiudad.DataTextField = "nom_ciudad";
        ddlCiudad.DataValueField = "cod_ciudad";
        ddlCiudad.DataBind();

        Xpinn.Contabilidad.Services.ConceptoService ConceptoService = new Xpinn.Contabilidad.Services.ConceptoService();
        Xpinn.Contabilidad.Entities.Concepto Concepto = new Xpinn.Contabilidad.Entities.Concepto();
        ddlConcepto.DataSource = ConceptoService.ListarConcepto(Concepto, usuario);
        ddlConcepto.DataTextField = "descripcion";
        ddlConcepto.DataValueField = "concepto";
        ddlConcepto.DataBind();

        ddlConcep.DataSource = ddlConcepto.DataSource;
        ddlConcep.DataTextField = "descripcion";
        ddlConcep.DataValueField = "concepto";
        ddlConcep.DataBind();

        Xpinn.Caja.Services.TipoOperacionService TipOpeService = new Xpinn.Caja.Services.TipoOperacionService();
        Xpinn.Caja.Entities.TipoOperacion TipOpe = new Xpinn.Caja.Entities.TipoOperacion();
        ddlTipoOperacion.DataSource = TipOpeService.ListarTipoOpe(usuario);
        ddlTipoOperacion.DataTextField = "nom_tipo_operacion";
        ddlTipoOperacion.DataValueField = "cod_operacion";
        ddlTipoOperacion.DataBind();

        Xpinn.Contabilidad.Services.EstructuraDetalleService EstDetService = new Xpinn.Contabilidad.Services.EstructuraDetalleService();
        Xpinn.Contabilidad.Entities.EstructuraDetalle pEstDet = new Xpinn.Contabilidad.Entities.EstructuraDetalle();
        ddlEstructura.DataSource = EstDetService.ListarEstructuraDetalle(pEstDet, usuario);
        ddlEstructura.DataTextField = "detalle";
        ddlEstructura.DataValueField = "cod_est_det";
        ddlEstructura.DataBind();

        Xpinn.Contabilidad.Services.PlanCuentasService PlanService = new Xpinn.Contabilidad.Services.PlanCuentasService();
        Xpinn.Contabilidad.Entities.PlanCuentas pPlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
        ddlCodCuent.DataSource = PlanService.ListarPlanCuentasLocal(pPlanCuentas, usuario, "");
        ddlCodCuent.DataTextField = "cod_cuenta";
        ddlCodCuent.DataValueField = "nombre";
        ddlCodCuent.DataBind();
        ddlNomCuent.DataSource = ddlCodCuent.DataSource;
        ddlNomCuent.DataTextField = "nombre";
        ddlNomCuent.DataValueField = "cod_cuenta";
        ddlNomCuent.DataBind();
    }
    /// <summary>
    /// Méotodo para mostrar los datos del comprobobante
    /// </summary>
    /// <param name="pIdNComp"></param>
    /// <param name="pIdTComp"></param>
    protected void ObtenerDatosCopia(String pIdNComp, String pIdTComp)
    {
        String Comprobantecopia = (String)Session["Comprobantecopia"];
        if (Comprobantecopia != null || Comprobantecopia != "")
        {
            try
            {
                Xpinn.Contabilidad.Entities.Comprobante vComprobante = new Xpinn.Contabilidad.Entities.Comprobante();
                Xpinn.Contabilidad.Entities.DetalleComprobante vDetalleComprobante = new Xpinn.Contabilidad.Entities.DetalleComprobante();

                List<Xpinn.Contabilidad.Entities.DetalleComprobante> LstDetalleComprobante = new List<Xpinn.Contabilidad.Entities.DetalleComprobante>();
                if (ComprobanteServicio.ConsultarComprobante(Convert.ToInt64(pIdNComp), Convert.ToInt64(pIdTComp), ref vComprobante, ref LstDetalleComprobante, (Usuario)Session["Usuario"]))
                {
                    // Mostrar datos del encabezado
                    txtNumComp.Text = HttpUtility.HtmlDecode(vComprobante.num_comp.ToString().Trim());
                    txtFecha.Text = vComprobante.fecha.ToShortDateString();
                    ddlTipoComprobante.SelectedValue = vComprobante.tipo_comp.ToString();
                    ddlCiudad.SelectedValue = Convert.ToString(vComprobante.ciudad);
                    ddlConcepto.SelectedValue = vComprobante.concepto.ToString();

                    Usuario usuap = (Usuario)Session["usuario"];
                    txtElaboradoPor.Text = Convert.ToString(usuap.nombre);
                    txtCodElabora.Text = vComprobante.cod_elaboro.ToString();
                    txtCodAprobo.Text = vComprobante.cod_aprobo.ToString();
                    txtEstado.Text = vComprobante.estado;
                    if (!string.IsNullOrEmpty(vComprobante.tipo_benef))
                        tipobeneficiario = vComprobante.tipo_benef.ToString();
                    if (!string.IsNullOrEmpty(vComprobante.iden_benef))
                        txtIdentificacion.Text = HttpUtility.HtmlDecode(vComprobante.iden_benef.ToString().Trim());
                    if (!string.IsNullOrEmpty(vComprobante.tipo_identificacion))
                        ddlTipoIdentificacion.SelectedValue = vComprobante.tipo_identificacion.ToString();
                    txtNombres.Text = vComprobante.nombre;
                    tbxObservaciones.Text = vComprobante.observaciones;

                    // Determinar el tipo de pago del comprobante
                    if (vComprobante.tipo_pago != null)
                        try
                        {
                            ddlFormaPago.SelectedValue = vComprobante.tipo_pago.ToString();
                        }
                        catch
                        {
                        }

                    // Determinar la entidad bancaria y la cuenta bancaria
                    ddlEntidad.SelectedValue = vComprobante.entidad.ToString();
                    try
                    {
                        ActivarDDLCuentas();
                        if (ddlEntidad5.Enabled == true)
                            ddlEntidad5.SelectedValue = vComprobante.entidad.ToString();
                        if (ddlTipoComprobante.SelectedValue == "5")
                            LlenarCuenta();
                    }
                    catch (Exception ex)
                    {
                        VerError(ex.ToString());
                    }
                    if (vComprobante.tipo_comp == 1)
                        txtNumSop.Text = vComprobante.num_consig;
                    if (vComprobante.tipo_comp == 2)
                        txtNumSop.Text = vComprobante.n_documento;

                    // Mostrar datos del detalle
                    if ((LstDetalleComprobante == null) || (LstDetalleComprobante.Count == 0))
                    {
                        CrearDetalleInicial(0);
                    }
                    else
                    {
                        Session["DetalleComprobante"] = LstDetalleComprobante;
                    };

                    CalcularTotal();

                    gvDetMovs.DataSource = LstDetalleComprobante;
                    gvDetMovs.DataBind();

                    // Determinar el valor del giro cuando es comprobante de egreso
                    DetalleComprobante var = new DetalleComprobante();
                    if (ddlTipoComprobante.SelectedValue == "5")
                    {
                        PanelFooter.Visible = true;
                        for (int i = 0; i < LstDetalleComprobante.Count; i++)
                        {
                            var = LstDetalleComprobante[i];
                            string a = var.cod_cuenta.Substring(0, 2);
                            if (ComprobanteServicio.CuentaEsGiro(var.cod_cuenta, (Usuario)Session["Usuario"]))
                            {
                                lablerror0.Text = string.Format("{0:N2}", var.valor);
                                i = LstDetalleComprobante.Count + 1;
                            }
                        }
                    }
                    if (lablerror0.Text == "")
                        lablerror0.Text = "0";

                    // Visualizar campos dependiendo del tipo de comprobante
                    // if (vComprobante.tipo_comp == 1) rbIngreso.Checked = true;
                    // if (vComprobante.tipo_comp == 5) rbEgreso.Checked = true;
                    // if (vComprobante.tipo_comp != 1 & vComprobante.tipo_comp != 5) rbContable.Checked = true;

                    //   Activar();

                    txtidenti.Text = txtIdentificacion.Text;
                    txtnom.Text = txtNombres.Text;
                    ddlTipoIdentificacion.SelectedValue = ddlTipoIdentificacion.SelectedValue;

                }
                ddlTipoComprobante.SelectedValue = Convert.ToString(vComprobante.tipo_comp);

                // Comprobantecopia = "";
                Configuracion conf = new Configuracion();
                txtNumComp.Text = "";
                idObjeto = "";
                txtFecha.Text = "";
                txtFecha.Enabled = true;
                txtFecha.Text = System.DateTime.Now.ToString(conf.ObtenerFormatoFecha());
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(ComprobanteServicio.CodigoPrograma, "ObtenerDatos", ex);
            }

        }


    }

    /// <summary>
    /// Méotodo para mostrar los datos del comprobobante
    /// </summary>
    /// <param name="pIdNComp"></param>
    /// <param name="pIdTComp"></param>
    protected void ObtenerDatos(String pIdNComp, String pIdTComp)
    {
        try
        {
            Xpinn.Contabilidad.Entities.Comprobante vComprobante = new Xpinn.Contabilidad.Entities.Comprobante();
            Xpinn.Contabilidad.Entities.DetalleComprobante vDetalleComprobante = new Xpinn.Contabilidad.Entities.DetalleComprobante();

            List<Xpinn.Contabilidad.Entities.DetalleComprobante> LstDetalleComprobante = new List<Xpinn.Contabilidad.Entities.DetalleComprobante>();
            if (ComprobanteServicio.ConsultarComprobante(Convert.ToInt64(pIdNComp), Convert.ToInt64(pIdTComp), ref vComprobante, ref LstDetalleComprobante, (Usuario)Session["Usuario"]))
            {
                // Mostrar datos del encabezado
                txtNumComp.Text = HttpUtility.HtmlDecode(vComprobante.num_comp.ToString().Trim());
                txtFecha.Text = vComprobante.fecha.ToShortDateString();
                ddlTipoComprobante.SelectedValue = vComprobante.tipo_comp.ToString();
                ddlCiudad.SelectedValue = Convert.ToString(vComprobante.ciudad);
                ddlConcepto.SelectedValue = vComprobante.concepto.ToString();

                Usuario usuap = (Usuario)Session["usuario"];
                txtElaboradoPor.Text = Convert.ToString(usuap.nombre);
                txtCodElabora.Text = vComprobante.cod_elaboro.ToString();
                txtCodAprobo.Text = vComprobante.cod_aprobo.ToString();
                txtEstado.Text = vComprobante.estado;
                if (!string.IsNullOrEmpty(vComprobante.tipo_benef))
                    tipobeneficiario = vComprobante.tipo_benef.ToString();
                if (!string.IsNullOrEmpty(vComprobante.iden_benef))
                    txtIdentificacion.Text = HttpUtility.HtmlDecode(vComprobante.iden_benef.ToString().Trim());
                if (!string.IsNullOrEmpty(vComprobante.tipo_identificacion))
                    ddlTipoIdentificacion.SelectedValue = vComprobante.tipo_identificacion.ToString();
                txtNombres.Text = vComprobante.nombre;
                tbxObservaciones.Text = vComprobante.observaciones;

                // Determinar el tipo de pago del comprobante
                if (vComprobante.tipo_pago != null)
                    try
                    {
                        ddlFormaPago.SelectedValue = vComprobante.tipo_pago.ToString();
                    }
                    catch
                    {
                    }

                // Determinar la entidad bancaria y la cuenta bancaria
                ddlEntidad.SelectedValue = vComprobante.entidad.ToString();
                try
                {
                    ActivarDDLCuentas();
                    if (ddlEntidad5.Enabled == true)
                        ddlEntidad5.SelectedValue = vComprobante.entidad.ToString();
                    if (ddlTipoComprobante.SelectedValue == "5")
                        LlenarCuenta();
                }
                catch (Exception ex)
                {
                    VerError(ex.ToString());
                }
                if (vComprobante.tipo_comp == 1)
                    txtNumSop.Text = vComprobante.num_consig;
                if (vComprobante.tipo_comp == 2)
                    txtNumSop.Text = vComprobante.n_documento;

                // Mostrar datos del detalle
                if ((LstDetalleComprobante == null) || (LstDetalleComprobante.Count == 0))
                {
                    CrearDetalleInicial(0);
                }
                else
                {
                    Session["DetalleComprobante"] = LstDetalleComprobante;
                };

                CalcularTotal();

                gvDetMovs.DataSource = LstDetalleComprobante;
                gvDetMovs.DataBind();

                // Determinar el valor del giro cuando es comprobante de egreso
                DetalleComprobante var = new DetalleComprobante();
                if (ddlTipoComprobante.SelectedValue == "5")
                {
                    PanelFooter.Visible = true;
                    for (int i = 0; i < LstDetalleComprobante.Count; i++)
                    {
                        var = LstDetalleComprobante[i];
                        string a = var.cod_cuenta.Substring(0, 2);
                        if (ComprobanteServicio.CuentaEsGiro(var.cod_cuenta, (Usuario)Session["Usuario"]))
                        {
                            lablerror0.Text = string.Format("{0:N2}", var.valor);
                            i = LstDetalleComprobante.Count + 1;
                        }
                    }
                }
                if (lablerror0.Text == "")
                    lablerror0.Text = "0";

                // Visualizar campos dependiendo del tipo de comprobante
                if (vComprobante.tipo_comp == 1) rbIngreso.Checked = true;
                if (vComprobante.tipo_comp == 5) rbEgreso.Checked = true;
                if (vComprobante.tipo_comp != 1 & vComprobante.tipo_comp != 5) rbContable.Checked = true;

                Activar();

                txtidenti.Text = txtIdentificacion.Text;
                txtnom.Text = txtNombres.Text;
                ddlTipoIdentificacion.SelectedValue = ddlTipoIdentificacion.SelectedValue;

            }
            ddlTipoComprobante.SelectedValue = Convert.ToString(vComprobante.tipo_comp);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

    /// <summary>
    /// Método para guardar los datos del comprobante
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {

            if (ValidarDatosComprobante())
            {
                Lblerror.Text = "";
                lblMensajeGrabar.Text = "";

                Xpinn.Contabilidad.Entities.Comprobante vComprobante = new Xpinn.Contabilidad.Entities.Comprobante();
                List<Xpinn.Contabilidad.Entities.DetalleComprobante> vDetalleComprobante = new List<Xpinn.Contabilidad.Entities.DetalleComprobante>();

                Xpinn.FabricaCreditos.Entities.Persona1 Persona1 = new Xpinn.FabricaCreditos.Entities.Persona1();

                vDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];

                // Determinar el número del comprobante
                if (txtNumComp.Text != "")
                    vComprobante.num_comp = Convert.ToInt64(txtNumComp.Text);

                // Determinar el tipo del comprobante
                vComprobante.tipo_comp = Convert.ToInt64(ddlTipoComprobante.SelectedValue);

                // Determinar el número de soporte del comprobante
                if (txtNumSop.Text != "")
                {
                    vComprobante.num_consig = txtNumSop.Text;
                    vComprobante.n_documento = txtNumSop.Text;
                }
                else
                {
                    vComprobante.num_consig = "0";
                    vComprobante.n_documento = "0";
                }

                // Determinar datos del comprobante
                vComprobante.fecha = Convert.ToDateTime(txtFecha.Text);
                vComprobante.hora = Convert.ToDateTime(System.DateTime.Now);
                vComprobante.ciudad = Convert.ToInt64(ddlCiudad.SelectedValue);
                vComprobante.concepto = Convert.ToInt64(ddlConcepto.SelectedValue);
                vComprobante.tipo_pago = Convert.ToInt64(ddlFormaPago.SelectedValue);
                vComprobante.entidad = Convert.ToInt64(ddlEntidad.SelectedValue);
                vComprobante.descripcion_concepto = ddlTipoComprobante.SelectedItem.Text;
                if (tbxTotalDebitos.Text != "")
                    vComprobante.totalcom = Convert.ToDecimal(tbxTotalDebitos.Text);
                vComprobante.tipo_benef = "A";

                // Validar datos de la persona
                Persona1 = Persona1Servicio.ConsultaDatosPersona(txtIdentificacion.Text, (Usuario)Session["usuario"]);
                if (Persona1.cod_persona == Int64.MinValue)
                {
                    Lblerror.Text = "La persona con identificaciòn no existe " + txtIdentificacion.Text;
                    return;
                }
                vComprobante.cod_benef = Persona1.cod_persona;
                vComprobante.iden_benef = txtIdentificacion.Text;
                vComprobante.tipo_identificacion = ddlTipoIdentificacion.SelectedItem.ToString();
                txtNombres.Text = Persona1.nombre;
                vComprobante.nombre = txtNombres.Text;
                if (txtCodElabora.Text == "")
                {
                    Usuario usuap = (Usuario)Session["usuario"];
                    txtCodElabora.Text = Convert.ToString(usuap.codusuario);
                }
                vComprobante.cod_elaboro = Convert.ToInt64(txtCodElabora.Text);
                if (txtCodAprobo.Text == "")
                {
                    Usuario usuap = (Usuario)Session["usuario"];
                    txtCodAprobo.Text = Convert.ToString(usuap.codusuario);
                }
                vComprobante.cod_aprobo = Convert.ToInt64(txtCodAprobo.Text);
                vComprobante.estado = txtEstado.Text;
                vComprobante.observaciones = tbxObservaciones.Text;
                vComprobante.cuenta = ddlCuenta.SelectedValue;

                // Determinar beneficiario del cheque
                vComprobante.cheque_iden_benef = txtidenti.Text;
                vComprobante.cheque_tipo_identificacion = ddlTipoIdentificacion0.SelectedValue;
                vComprobante.cheque_nombre = txtnom.Text;

                if (idObjeto != "")
                {
                    ComprobanteServicio.ModificarComprobante(vDetalleComprobante, vComprobante, (Usuario)Session["Usuario"]);
                }
                else
                {
                    vComprobante = ComprobanteServicio.CrearComprobante(vDetalleComprobante, vComprobante, (Usuario)Session["Usuario"]);
                    txtNumComp.Text = vComprobante.num_comp.ToString();
                }
                mvComprobante.ActiveViewIndex = 2;
                lblMensajeGrabar.Text = "Comprobante " + txtNumComp.Text + " Grabado Correctamente";

                Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = idObjeto;

                if (Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] != null)
                    Session.Remove(ComprobanteServicio.CodigoPrograma + ".cod_ope");
                if (Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] != null)
                    Session.Remove(ComprobanteServicio.CodigoPrograma + ".tipo_ope");
                if (Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] != null)
                    Session.Remove(ComprobanteServicio.CodigoPrograma + ".fecha_ope");
                if (Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] != null)
                    Session.Remove(ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope");
            }
            else
            {
                lablerror.Visible = true;
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    /// <summary>
    /// Método que permite regresar a la ventana de consulta de comprobantes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (Session["DetalleComprobante"] != null)
            Session.Remove("DetalleComprobante");
        if (Session["cod_ope"] != null)
            Session.Remove("cod_ope");
        if (Session["tipo_ope"] != null)
            Session.Remove("tipo_ope");
        if (Session["cod_persona"] != null)
            Session.Remove("cod_persona");
        Navegar(Pagina.Lista);
    }

    /// <summary>
    /// Método para cancelar el proceso que se esta realizando
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (Session["DetalleComprobante"] != null)
            Session.Remove("DetalleComprobante");
        if (Session["cod_ope"] != null)
            Session.Remove("cod_ope");
        if (Session["tipo_ope"] != null)
            Session.Remove("tipo_ope");
        if (Session["cod_persona"] != null)
            Session.Remove("cod_persona");

        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = idObjeto;
            Navegar(Pagina.Lista);
        }
    }

    /// <summary>
    /// Método para instar un detalle en blanco para cuando la grilla no tiene datos
    /// </summary>
    /// <param name="consecutivo"></param>
    private void CrearDetalleInicial(int consecutivo)
    {
        DetalleComprobante pDetalleComprobante = new DetalleComprobante();
        List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();

        pDetalleComprobante.codigo = -1;
        if (txtNumComp.Text != "")
            pDetalleComprobante.num_comp = Convert.ToInt64(txtNumComp.Text);
        if (ddlTipoComprobante.SelectedValue != null)
            pDetalleComprobante.tipo_comp = Convert.ToInt64(ddlTipoComprobante.SelectedValue.ToString());
        pDetalleComprobante.cod_cuenta = "";
        pDetalleComprobante.nombre_cuenta = "";
        pDetalleComprobante.centro_costo = null;
        pDetalleComprobante.valor = null;
        pDetalleComprobante.tercero = null;

        LstDetalleComprobante.Add(pDetalleComprobante);
        gvDetMovs.DataSource = LstDetalleComprobante;
        gvDetMovs.DataBind();

        Session["DetalleComprobante"] = LstDetalleComprobante;

    }


    // == EVENTOS DE LA GRILA ==========================================================================================

    /// <summary>
    /// Método para cuando se selecciona un item de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDetMovs_SelectedIndexChanged(object sender, EventArgs e)
    {

        String id = gvDetMovs.DataKeys[gvDetMovs.SelectedRow.RowIndex].Value.ToString();

        GridViewRow row = gvDetMovs.SelectedRow;

        mpeNuevo.Show();

        Xpinn.Caja.Entities.CentroCosto CenCos = new Xpinn.Caja.Entities.CentroCosto();
        List<Xpinn.Caja.Entities.CentroCosto> LstCentroCosto = new List<Xpinn.Caja.Entities.CentroCosto>();
        Xpinn.Caja.Services.CentroCostoService CentroCostoServicio = new Xpinn.Caja.Services.CentroCostoService();
        LstCentroCosto = CentroCostoServicio.ListarCentroCosto(CenCos, (Usuario)Session["usuario"]);
        dropcc.DataSource = LstCentroCosto;
        dropcc.DataTextField = "centro_costo";
        dropcc.DataValueField = "centro_costo";
        Session["CentroCosto"] = LstCentroCosto;
        dropcc.DataBind();

        Xpinn.Caja.Entities.TipoMoneda eMoneda = new Xpinn.Caja.Entities.TipoMoneda();
        Xpinn.Caja.Services.TipoMonedaService TipoMonedaServicio = new Xpinn.Caja.Services.TipoMonedaService();
        List<Xpinn.Caja.Entities.TipoMoneda> LstMoneda = new List<Xpinn.Caja.Entities.TipoMoneda>();
        LstMoneda = TipoMonedaServicio.ListarTipoMoneda(eMoneda, (Usuario)Session["usuario"]);
        dropmoneda.DataTextField = "descripcion";
        dropmoneda.DataValueField = "cod_moneda";
        dropmoneda.DataSource = LstMoneda;
        Session["Moneda"] = LstMoneda;
        dropmoneda.DataBind();

        PlanCuentas eCodCuenta = new PlanCuentas();
        List<PlanCuentas> LstCodCuenta = new List<PlanCuentas>();
        LstCodCuenta = PlanCuentasServicio.ListarPlanCuentasLocal(eCodCuenta, (Usuario)Session["usuario"], "AUXILIARES");
        dropcuenta.DataSource = LstCodCuenta;
        dropcuenta.DataTextField = "cod_cuenta";
        dropcuenta.DataValueField = "cod_cuenta";
        Session["CodCuenta"] = LstCodCuenta;
        dropcuenta.DataBind();

    }


    /// <summary>
    /// Método para borrar un registro de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDetMovs_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvDetMovs.DataKeys[e.RowIndex].Values[0].ToString());

        if (conseID != 0)
        {
            try
            {
                List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
                LstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];

                LstDetalleComprobante.RemoveAt(e.RowIndex);
                Session["DetalleComprobante"] = LstDetalleComprobante;

                gvDetMovs.DataSourceID = null;
                gvDetMovs.DataBind();
                gvDetMovs.DataSource = LstDetalleComprobante;
                gvDetMovs.DataBind();
                mpeNuevo.Hide();

                CalcularTotal();
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(this.ComprobanteServicio.numero_comp + "L", "gvDetMovs_RowDeleting", ex);
            }
        }
        else
        {
            e.Cancel = true;
        }
    }


    /// <summary>
    /// Evento para comando de una fila
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDetMovs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        VerError("");

        mpeNuevo.Show();

        Xpinn.Caja.Entities.CentroCosto CenCos = new Xpinn.Caja.Entities.CentroCosto();
        List<Xpinn.Caja.Entities.CentroCosto> LstCentroCosto = new List<Xpinn.Caja.Entities.CentroCosto>();
        Xpinn.Caja.Services.CentroCostoService CentroCostoServicio = new Xpinn.Caja.Services.CentroCostoService();
        LstCentroCosto = CentroCostoServicio.ListarCentroCosto(CenCos, (Usuario)Session["usuario"]);
        dropcc.DataSource = LstCentroCosto;
        dropcc.DataTextField = "centro_costo";
        dropcc.DataValueField = "centro_costo";
        Session["CentroCosto"] = LstCentroCosto;
        dropcc.DataBind();

        Xpinn.Caja.Entities.TipoMoneda eMoneda = new Xpinn.Caja.Entities.TipoMoneda();
        Xpinn.Caja.Services.TipoMonedaService TipoMonedaServicio = new Xpinn.Caja.Services.TipoMonedaService();
        List<Xpinn.Caja.Entities.TipoMoneda> LstMoneda = new List<Xpinn.Caja.Entities.TipoMoneda>();
        LstMoneda = TipoMonedaServicio.ListarTipoMoneda(eMoneda, (Usuario)Session["usuario"]);
        dropmoneda.DataTextField = "descripcion";
        dropmoneda.DataValueField = "cod_moneda";
        dropmoneda.DataSource = LstMoneda;
        Session["Moneda"] = LstMoneda;
        dropmoneda.DataBind();

        PlanCuentas eCodCuenta = new PlanCuentas();
        List<PlanCuentas> LstCodCuenta = new List<PlanCuentas>();
        LstCodCuenta = PlanCuentasServicio.ListarPlanCuentasLocal(eCodCuenta, (Usuario)Session["usuario"], "AUXILIARES");
        dropcuenta.DataSource = LstCodCuenta;
        dropcuenta.DataTextField = "cod_cuenta";
        dropcuenta.DataValueField = "cod_cuenta";
        Session["CodCuenta"] = LstCodCuenta;
        dropcuenta.DataBind();

        int IndiceDetalle = Convert.ToInt32(e.CommandArgument);
        Session[ComprobanteServicio.CodigoPrograma + ".indice"] = IndiceDetalle;
        if (IndiceDetalle >= 0)
        {
            GridViewRow row = gvDetMovs.Rows[IndiceDetalle];

            Label lblCodCuenta = row.FindControl("lblCodCuenta") as Label;
            if (lblCodCuenta != null & lblCodCuenta.Text != "")
            {
                try
                {
                    dropcuenta.SelectedValue = lblCodCuenta.Text;
                }
                catch
                {
                    VerError("Cuenta errada: " + lblCodCuenta.Text);
                }
            }
            Label lblNomCuentaD = row.FindControl("lblNomCuenta") as Label;
            if (lblNomCuentaD != null & lblNomCuentaD.Text != "")
            {
                lblNomCuenta.Text = lblNomCuentaD.Text;
            }
            Label lblDetalle = row.FindControl("lblDetalle") as Label;
            if (lblDetalle != null)
            {
                detalle.Text = lblDetalle.Text;
            }
            Label lblValor = row.FindControl("lblValor") as Label;
            if (lblValor != null)
            {
                txtValor1.Text = lblValor.Text;
            }
            Label lblTipo = row.FindControl("lblTipo") as Label;
            if (lblTipo != null)
            {
                ddlTipo.SelectedValue = lblTipo.Text;
            }
            Label lblCentroCosto = row.FindControl("lblCentroCosto") as Label;
            if (lblCentroCosto != null)
            {
                dropcc.SelectedValue = lblCentroCosto.Text;
            }
            Label lblCentroGestion = row.FindControl("lblCentroGestion") as Label;
            if (lblCentroGestion != null)
            {
                cg.Text = lblCentroGestion.Text;
            }
            Label lblTercero = row.FindControl("lblTercero") as Label;
            if (lblTercero != null)
            {
                txtTercero1.Text = lblTercero.Text;
            }
            Label lblIdentificacion = row.FindControl("lblIdentificacion") as Label;
            if (lblIdentificacion != null)
            {
                txtIdentificD.Text = lblIdentificacion.Text;
            }
            Label lblNombre = row.FindControl("lblNombre") as Label;
            if (lblNombre != null)
            {
                txtNombreTercero1.Text = lblNombre.Text;
            }
            Label lblMoneda = row.FindControl("lblMoneda") as Label;
            if (lblMoneda != null)
            {
                dropmoneda.SelectedValue = "1";
            }
        }
    }


    // == SELECCIONAR TIPO DE COMPROBANTE =====================================================================================

    /// <summary>
    /// Método para aceptar cuando se esta haciendo un nuevo comprobante y se seleccionó el tipo de comprobante
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
    {
        mvComprobante.ActiveViewIndex = 1;
        Activar();
    }

    void Activar()
    {
        if (rbIngreso.Checked == true)
        {
            lblTipoComp.Visible = false;
            ddlTipoComprobante.Visible = false;
            ddlTipoComprobante.SelectedValue = "1";
            ddlEntidad.Visible = true;
            ddlFormaPago.Visible = true;
            lblEntidad.Visible = true;
            lblFormaPago.Visible = true;
            txtNumSop.Visible = true;
            lblSoporte.Visible = true;
        }
        else
        {
            if (rbEgreso.Checked == true)
            {
                lblTipoComp.Visible = false;
                ddlTipoComprobante.Visible = false;
                ddlTipoComprobante.SelectedValue = "5";
                ddlEntidad.Visible = true;
                ddlFormaPago.Visible = true;
                lblEntidad.Visible = true;
                lblFormaPago.Visible = true;
                txtNumSop.Visible = true;
                lblSoporte.Visible = true;
            }
            else
            {
                ddlTipoComprobante.SelectedValue = "2";
                ddlEntidad.Visible = false;
                ddlFormaPago.Visible = false;
                lblEntidad.Visible = false;
                lblFormaPago.Visible = false;
                txtNumSop.Visible = false;
                lblSoporte.Visible = false;
            }
        }
    }


    protected List<PlanCuentas> ListaPlanCuentas()
    {
        PlanCuentas pPlanCuentas = new PlanCuentas();
        List<PlanCuentas> LstPlanCuentas = new List<PlanCuentas>();
        LstPlanCuentas = PlanCuentasServicio.ListarPlanCuentasLocal(pPlanCuentas, (Usuario)Session["usuario"], "AUXILIARES");
        return LstPlanCuentas;
    }


    /// <summary>
    /// Evento para cuando se cancela la acción de modificación del detalle
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCloseReg_Click(object sender, EventArgs e)
    {
        mpeNuevo.Hide();
        Session.Remove(ComprobanteServicio.CodigoPrograma + ".indice");
    }


    /// <summary>
    /// Evento para cuando se presiona el botón de actualizar el detalle
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Guardar_modi_Click(object sender, EventArgs e)
    {

        int IndiceDetalle = 0;

        if (Session[ComprobanteServicio.CodigoPrograma + ".indice"] != null)
        {
            IndiceDetalle = Convert.ToInt16(Session[ComprobanteServicio.CodigoPrograma + ".indice"]);
            Session.Remove(ComprobanteServicio.CodigoPrograma + ".indice");
        }

        List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
        LstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];

        if (IndiceDetalle >= 0 || (IndiceDetalle < 0 && LstDetalleComprobante.Count() == 1 && LstDetalleComprobante[0].cod_cuenta == ""))
        {
            // Si se esta adicionando un nuevo registro y es el primer detalle de la lista
            if (IndiceDetalle < 0 && LstDetalleComprobante.Count() == 1 && LstDetalleComprobante[0].cod_cuenta == "")
                IndiceDetalle = 0;
            // Obtener la fila que se esta modificando en la grilla
            GridViewRow row = gvDetMovs.Rows[IndiceDetalle];
            // Instanciar nueva filla del detalle            
            DetalleComprobante nuevos = new DetalleComprobante();
            // Actualizar el registro con los datos ingresados
            try
            {
                LstDetalleComprobante[IndiceDetalle].cod_cuenta = dropcuenta.SelectedValue;
                LstDetalleComprobante[IndiceDetalle].nombre_cuenta = lblNomCuenta.Text;
                LstDetalleComprobante[IndiceDetalle].moneda = Convert.ToInt64(dropmoneda.SelectedValue);
                LstDetalleComprobante[IndiceDetalle].nom_moneda = dropmoneda.SelectedItem.Text;
                LstDetalleComprobante[IndiceDetalle].centro_costo = Convert.ToInt64(dropcc.SelectedValue);
                if (cg.Text != "")
                    LstDetalleComprobante[IndiceDetalle].centro_gestion = Convert.ToInt64(cg.Text);
                LstDetalleComprobante[IndiceDetalle].detalle = detalle.Text;
                LstDetalleComprobante[IndiceDetalle].tipo = ddlTipo.SelectedValue;
                LstDetalleComprobante[IndiceDetalle].valor = Convert.ToDecimal(txtValor1.Text);
                if (txtTercero1.Text != "")
                    LstDetalleComprobante[IndiceDetalle].tercero = Convert.ToInt64(txtTercero1.Text);
                LstDetalleComprobante[IndiceDetalle].identificacion = txtIdentificD.Text;
                LstDetalleComprobante[IndiceDetalle].nom_tercero = txtNombreTercero1.Text;
            }
            catch
            {
            }
        }

        if (IndiceDetalle < 0)
        {
            DetalleComprobante nuevos = new DetalleComprobante();
            try
            {
                if (txtNumComp.Text != "")
                    nuevos.num_comp = Convert.ToInt64(txtNumComp.Text);
                nuevos.tipo_comp = Convert.ToInt64(ddlTipoComprobante.SelectedValue);
                nuevos.cod_cuenta = dropcuenta.SelectedValue;
                nuevos.moneda = Convert.ToInt64(dropmoneda.SelectedValue);
                nuevos.nom_moneda = dropmoneda.SelectedItem.Text;
                nuevos.centro_costo = Convert.ToInt64(dropcc.SelectedValue);
                if (cg.Text != "")
                    nuevos.centro_gestion = Convert.ToInt64(cg.Text);
                nuevos.detalle = detalle.Text;
                nuevos.tipo = ddlTipo.SelectedValue;
                if (txtValor1.Text != "")
                    nuevos.valor = Convert.ToDecimal(txtValor1.Text);
                if (txtTercero1.Text != "")
                    nuevos.codigo = Convert.ToInt64(txtTercero1.Text);
                nuevos.identificacion = txtIdentificD.Text;
                nuevos.nom_tercero = txtNombreTercero1.Text;
                nuevos.nombre_cuenta = lblNomCuenta.Text;
                LstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];
                LstDetalleComprobante.Add(nuevos);
            }
            catch
            {
            }
        }
        gvDetMovs.DataSourceID = null;
        gvDetMovs.DataBind();
        Session["DetalleComprobante"] = LstDetalleComprobante;
        gvDetMovs.DataSource = LstDetalleComprobante;
        CalcularTotal();
        gvDetMovs.DataBind();
        mpeNuevo.Hide();

    }

    static public DataControlFieldCell GetCellByName(GridViewRow Row, String CellName)
    {
        foreach (DataControlFieldCell Cell in Row.Cells)
        {
            if (Cell.ContainingField.ToString().ToUpper() == CellName.ToUpper())
                return Cell;
        }
        return null;
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public void CalcularTotal()
    {
        decimal? totdeb = 0.00m;
        decimal? totcre = 0.00m;
        List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
        if (Session["DetalleComprobante"] != null)
        {
            LstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];
            for (int i = 0; i < LstDetalleComprobante.Count; i++)
            {
                if (LstDetalleComprobante[i].valor != null)
                {
                    if (LstDetalleComprobante[i].tipo == "D" | LstDetalleComprobante[i].tipo == "d")
                        totdeb = totdeb + LstDetalleComprobante[i].valor;
                    else
                        totcre = totcre + LstDetalleComprobante[i].valor;
                }
            }
            string sDeb = Convert.ToString(totdeb);
            tbxTotalDebitos.Text = sDeb;
            string sCre = Convert.ToString(totcre);
            tbxTotalCreditos.Text = sCre;
        }
    }


    public Boolean ValidarDatosComprobante()
    {
        Xpinn.Contabilidad.Entities.PlanCuentas ePlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
        Xpinn.FabricaCreditos.Entities.Persona1 Persona1 = new Xpinn.FabricaCreditos.Entities.Persona1();

        // Cargar el detalle del comprobante
        List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
        LstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];

        // Validar el detalle del comprobante
        decimal? totdeb = 0.00m;
        decimal? totcre = 0.00m;
        decimal? diferencia = 0.00m;

        // Validar cada registro del detalle
        for (int i = 0; i < LstDetalleComprobante.Count; i++)
        {
            // Sumar valor debitos y creditos
            try
            {
                if (LstDetalleComprobante[i].valor != null)
                {
                    if (LstDetalleComprobante[i].tipo == "D" | LstDetalleComprobante[i].tipo == "d")
                        totdeb = totdeb + LstDetalleComprobante[i].valor;
                    else
                        totcre = totcre + LstDetalleComprobante[i].valor;
                }
                else
                {
                    Lblerror.Text = "El valor en la cuenta " + LstDetalleComprobante[i].cod_cuenta + " no puede ser nulo";
                    return false;
                }
            }
            catch (Exception ex)
            {
                VerError("Error al realizar conversión. (" + ex.Message + ") ");
                return false;
            }
            // Verificar que la cuenta sea auxiliar
            if (ComprobanteServicio.CuentaEsAuxiliar(LstDetalleComprobante[i].cod_cuenta, (Usuario)Session["Usuario"]) == "")
            {
                Lblerror.Text = "La cuenta contable " + LstDetalleComprobante[i].cod_cuenta + " no es auxiliar o no existe";
                return false;
            }
            // Verificar datos del tercero
            ePlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(LstDetalleComprobante[i].cod_cuenta, (Usuario)Session["Usuario"]);
            if (ePlanCuentas.maneja_ter == 1)
            {
                if (LstDetalleComprobante[i].tercero == null || LstDetalleComprobante[i].tercero == -1)
                {
                    Lblerror.Text = "Debe ingresar el tercero para la cuenta " + LstDetalleComprobante[i].cod_cuenta;
                    return false;
                }
                Persona1 = Persona1Servicio.ConsultarPersona1(Convert.ToInt64(LstDetalleComprobante[i].tercero), (Usuario)Session["usuario"]);
                if (Persona1.cod_persona == Int64.MinValue)
                {
                    Lblerror.Text = "Para la cuenta contable " + LstDetalleComprobante[i].cod_cuenta + " el tercero " + LstDetalleComprobante[i].tercero + " no existe";
                    return false;
                }
            }
        }

        // Validar sumas iguales en el comprobante
        diferencia = totdeb - totcre;
        if (totdeb != totcre)
        {
            Lblerror.Text = "No hay sumas iguales en el comprobante, hay una diferencia de " + Convert.ToDecimal(diferencia).ToString("##0.00");
            return false;
        }

        return true;
    }


    protected void rbIngreso_CheckedChanged(object sender, EventArgs e)
    {
        if (rbIngreso.Checked == true)
        {
            rbEgreso.Checked = false;
            rbContable.Checked = false;
        }

    }

    protected void rbEgreso_CheckedChanged(object sender, EventArgs e)
    {
        if (rbEgreso.Checked == true)
        {
            rbIngreso.Checked = false;
            rbContable.Checked = false;
        }
    }

    protected void rbContable_CheckedChanged(object sender, EventArgs e)
    {
        if (rbContable.Checked == true)
        {
            rbIngreso.Checked = false;
            rbEgreso.Checked = false;
        }
    }

    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Xpinn.FabricaCreditos.Entities.Persona1 Persona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        Persona1 = Persona1Servicio.ConsultaDatosPersona(txtIdentificacion.Text, (Usuario)Session["usuario"]);
        txtNombres.Text = Persona1.nombre;
    }

    protected void txtIdentificD_TextChanged(object sender, EventArgs e)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Xpinn.FabricaCreditos.Entities.Persona1 Persona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        Persona1 = Persona1Servicio.ConsultaDatosPersona(txtIdentificD.Text, (Usuario)Session["usuario"]);
        if (Persona1.cod_persona != Int64.MinValue)
            txtTercero1.Text = Convert.ToString(Persona1.cod_persona);
        txtNombreTercero1.Text = Persona1.nombre;
    }

    protected void dropcuenta_SelectedIndexChanged(object sender, EventArgs e)
    {
        Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
        Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
        PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(dropcuenta.SelectedItem.Text, (Usuario)Session["usuario"]);
        lblNomCuenta.Text = PlanCuentas.nombre;
    }

    protected void imgAceptarProceso_Click(object sender, ImageClickEventArgs e)
    {
        Usuario usuap = (Usuario)Session["usuario"];
        tbxOficina.Text = usuap.nombre_oficina;
        // Generar el comprobante
        Int64 pcod_proceso = Convert.ToInt64(lstProcesos.SelectedValue);
        Int64 pnum_comp = 0;
        Int64 ptipo_comp = 0;
        Int64 pcod_persona = 0;
        Int64 pcod_ope = 0;
        string pError = "";
        if (Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] != null)
            pcod_ope = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"]);
        Int64 ptip_ope = 0;
        if (Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] != null)
            ptip_ope = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"]);
        DateTime pfecha = System.DateTime.Now;
        if (Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] != null)
            pfecha = Convert.ToDateTime(Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"]);
        Int64 pcod_ofi = usuap.cod_oficina;
        if (Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] != null)
            pcod_ofi = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"]);
        if (ComprobanteServicio.GenerarComprobante(pcod_ope, ptip_ope, pfecha, pcod_ofi, pcod_persona, pcod_proceso, ref pnum_comp, ref ptipo_comp, ref pError, usuap) == true)
        {
            ObtenerDatos(pnum_comp.ToString(), ptipo_comp.ToString());
            if (Session[ComprobanteServicio.CodigoPrograma + ".CuentaBancaria"] != null)
                ddlCuenta.SelectedValue = Convert.ToString(Session[ComprobanteServicio.CodigoPrograma + ".CuentaBancaria"]);
            mvComprobante.ActiveViewIndex = 1;
            tbxOficina.Text = usuap.nombre_oficina;
        }
        mvComprobante.ActiveViewIndex = 1;
        Activar();
    }


    protected void gvDetMovs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvDetMovs.PageIndex = e.NewPageIndex;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.CodigoPrograma, "gvDetMovs_PageIndexChanging", ex);
        }
    }

    protected void gvDetMovs_PageIndexChanged(object sender, EventArgs e)
    {

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

    public DataTable CrearDataTableMovimientos()
    {
        List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
        DetalleComprobante detalle = new DetalleComprobante();
        detalle.num_comp = Convert.ToInt64(txtNumComp.Text);
        detalle.tipo_comp = Convert.ToInt64(ddlTipoComprobante.SelectedValue);
        LstDetalleComprobante = ComprobanteServicio.ListarComprobantesreporte(detalle, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();

        table.Columns.Add("CodCuenta");
        table.Columns.Add("NomCuenta");
        table.Columns.Add("Identificacion");
        table.Columns.Add("Detalle");
        table.Columns.Add("ValorDebito");
        table.Columns.Add("ValorCredito");


        foreach (DetalleComprobante item in LstDetalleComprobante)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.cod_cuenta;
            datarw[1] = item.nombre_cuenta;
            datarw[2] = item.identificacion;
            datarw[3] = item.detalle;

            if (item.tipo == "C")
            {

                //string valordebito= item.valor.ToString();
                datarw[4] = item.valor.Value.ToString("0,0");
            }
            if (item.tipo == "D")
            {
                //string valorcredito = item.valor.Value.ToString();
                datarw[5] = item.valor.Value.ToString("0,0");
            }

            table.Rows.Add(datarw);
        }
        return table;
    }

    /// <summary>
    /// Método para la impresión
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInforme_Click(object sender, EventArgs e)
    {
        string valor = lablerror0.Text;

        // Imprimir los comprobantes de EGRESO
        if (ddlTipoComprobante.SelectedValue == "5")
        {
            // Determinar la fecha del comprobante            
            string a = Convert.ToDateTime(txtFecha.Text).ToString("yyyy/MM/dd");
            string fecha = a.Replace("/", "     ");

            // Determinando el valor en letras
            Cardinalidad numero = new Cardinalidad();
            string cardinal = " ";
            if (valor != "0")
            {
                cardinal = numero.enletras(valor.Replace(".", ""));
                int cont = cardinal.Length - 1;
                int cont2 = cont - 7;
                if (cont2 >= 0)
                {
                    string c = cardinal.Substring(cont2);
                    if (cardinal.Substring(cont2) == "MILLONES" || cardinal.Substring(cont2 + 2) == "MILLON")
                        cardinal = cardinal + " DE PESOS M/CTE";
                    else
                        cardinal = cardinal + " PESOS M/CTE";
                }
            }

            // Imprimer el reporte según sea cheque u otra forma de pago
            if (ddlFormaPago.SelectedValue == "2")
            {
                // Para cualquier OTRA ENTIDAD BANCARIA.
                RpviewComprobante.LocalReport.ReportPath = "Page/Contabilidad/Comprobante/ReportEgresos.rdlc";                
            }
            else
            {
                // Imprimir comprobante de egreso en EFECTIVO
                RpviewComprobante.LocalReport.ReportPath = "Page/Contabilidad/Comprobante/ReportEgresosEfectivo.rdlc";
            }

            // Parámetros del comprobante
            ReportParameter[] param = new ReportParameter[18];
            param[0] = new ReportParameter("pEntidad", "FUNDACIÓN EMPRENDER");
            param[1] = new ReportParameter("pNumComp", vacios(txtNumComp.Text));
            param[2] = new ReportParameter("pFecha", vacios(fecha));
            param[3] = new ReportParameter("pTipoComprobante", vacios(ddlTipoComprobante.SelectedValue));
            param[4] = new ReportParameter("pTipoComprobante2", vacios(ddlTipoComprobante.SelectedItem.Text));
            param[5] = new ReportParameter("pIdentificacion", vacios(txtidenti.Text));
            param[6] = new ReportParameter("pTipoIdentificacion", vacios(ddlTipoIdentificacion0.SelectedItem.Text));
            param[7] = new ReportParameter("pNombres", vacios(txtNombres.Text));
            param[8] = new ReportParameter("pConcepto", vacios(ddlConcepto.SelectedItem.Text));
            param[9] = new ReportParameter("pElaborado", vacios(txtElaboradoPor.Text));
            param[10] = new ReportParameter("pCiudad", vacios(ddlCiudad.SelectedItem.Text));
            param[11] = new ReportParameter("pTipoBeneficiario", vacios(tipobeneficiario));
            param[12] = new ReportParameter("pTotalDebito", vacios(tbxTotalDebitos.Text));
            param[13] = new ReportParameter("pTotalCredito", vacios(tbxTotalCreditos.Text));
            param[14] = new ReportParameter("detalle", vacios(tbxObservaciones.Text));
            param[15] = new ReportParameter("cardinalidad", cardinal);
            param[16] = new ReportParameter("valor", lablerror0.Text);
            param[17] = new ReportParameter("nombre1", vacios(txtnom.Text));
            RpviewComprobante.LocalReport.SetParameters(param);

            // Determinando las margenes del comprobante
            Margins margins = new Margins(100, 100, 100, 100);
            var sa = RpviewComprobante.LocalReport.GetDefaultPageSettings();
            RpviewComprobante.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DatasetDetalleComp", CrearDataTableMovimientos());
            RpviewComprobante.LocalReport.DataSources.Add(rds);
            RpviewComprobante.LocalReport.Refresh();
        }
        else
        {
            ReportParameter[] param = new ReportParameter[14];
            param[0] = new ReportParameter("pEntidad", "FUNDACIÓN EMPRENDER");
            param[1] = new ReportParameter("pNumComp", vacios(txtNumComp.Text));
            param[2] = new ReportParameter("pFecha", vacios(txtFecha.Text));
            param[3] = new ReportParameter("pTipoComprobante", vacios(ddlTipoComprobante.SelectedValue));
            param[4] = new ReportParameter("pTipoComprobante2", vacios(ddlTipoComprobante.SelectedItem.Text));
            param[5] = new ReportParameter("pIdentificacion", vacios(txtIdentificacion.Text));
            param[6] = new ReportParameter("pTipoIdentificacion", vacios(ddlTipoIdentificacion.SelectedItem.Text));
            param[7] = new ReportParameter("pNombres", vacios(txtNombres.Text));
            param[8] = new ReportParameter("pConcepto", vacios(ddlConcepto.SelectedItem.Text));
            param[9] = new ReportParameter("pElaborado", vacios(txtElaboradoPor.Text));
            param[10] = new ReportParameter("pCiudad", vacios(ddlCiudad.SelectedItem.Text));
            param[11] = new ReportParameter("pTipoBeneficiario", vacios(tipobeneficiario));
            param[12] = new ReportParameter("pTotalDebito", vacios(tbxTotalDebitos.Text));
            param[13] = new ReportParameter("pTotalCredito", vacios(tbxTotalCreditos.Text));

            RpviewComprobante.LocalReport.SetParameters(param);
            RpviewComprobante.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DatasetDetalleComp", CrearDataTableMovimientos());
            RpviewComprobante.LocalReport.DataSources.Add(rds);
            RpviewComprobante.LocalReport.Refresh();
        }

        RpviewComprobante.Visible = true;
        mvComprobante.Visible = true;
        mvComprobante.ActiveViewIndex = 4;

    }

    /// <summary>
    /// Método para cuando se selecciona una entidad bancaria
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlEntidad5_SelectedIndexChanged(object sender, EventArgs e)
    {
        LlenarCuenta();
        AsignarNumeroCheque(ddlCuenta.SelectedValue);
    }

    /// <summary>
    /// Método para llenar las cuentas bancarias que posea la entidad según el banco seleccionado
    /// </summary>
    private void LlenarCuenta()
    {
        try
        {
            ddlCuenta.Enabled = true;
            Xpinn.Caja.Services.BancosService BancosService = new Xpinn.Caja.Services.BancosService();
            Xpinn.Caja.Entities.Bancos Bancos = new Xpinn.Caja.Entities.Bancos();
            ddlCuenta.DataTextField = "num_cuenta";
            ddlCuenta.DataValueField = "num_cuenta";
            ddlCuenta.DataSource = BancosService.ListarBancosegrecuentas(ddlEntidad5.SelectedValue, (Usuario)Session["Usuario"]);
            ddlCuenta.DataBind();
            ddlCuenta.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            ddlEntidad.SelectedValue = ddlEntidad5.SelectedValue;
        }
        catch
        {
            VerError("...");
        }
    }

    protected void ddlCuenta_SelectedIndexChanged(object sender, EventArgs e)
    {
        AsignarNumeroCheque(ddlCuenta.SelectedValue);
    }

    private void AsignarNumeroCheque(string snumcuenta)
    {
        if (snumcuenta.Trim() != "")
        {
            Xpinn.Caja.Services.BancosService BancosService = new Xpinn.Caja.Services.BancosService();
            txtNumSop.Text = BancosService.soporte(snumcuenta, (Usuario)Session["Usuario"]);
        }
    }

    /// <summary>
    /// Método para cuando se selecciona la forma de pago
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt64(ddlFormaPago.SelectedValue) == 2)
        {
            lblCuenta.Visible = true;
            ddlCuenta.Visible = true;
        }
        else
        {
            lblCuenta.Visible = false;
            ddlCuenta.Visible = false;
        }
        AsignarNumeroCheque(ddlCuenta.SelectedValue);
    }


    private void ActivarDDLCuentas()
    {
        // Si hay cuentas bancarias a nombre de la entidad las muestras
        if (ddlEntidad5.Items.Count <= 0)
            // Si el comprobante es de egreso activa campos para datos de la cuenta
            if (ddlTipoComprobante.SelectedValue == "5")
            {
                // Llenar drop down list de entidades bancarias
                Xpinn.Caja.Services.BancosService BancosService = new Xpinn.Caja.Services.BancosService();
                Xpinn.Caja.Entities.Bancos Bancos = new Xpinn.Caja.Entities.Bancos();
                ddlEntidad5.DataSource = BancosService.ListarBancosegre((Usuario)Session["Usuario"]);
                ddlEntidad5.DataTextField = "nombrebanco";
                ddlEntidad5.DataValueField = "cod_banco";
                ddlEntidad5.DataBind();
                ddlEntidad5.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
                // Si es pago por cheque activar para seleccionar la cuenta
                ddlCuenta.Enabled = false;
                if (Convert.ToInt64(ddlFormaPago.SelectedValue) == 2)
                {
                    lblCuenta.Visible = true;
                    ddlCuenta.Visible = true;
                }
                else
                {
                    lblCuenta.Visible = false;
                    ddlCuenta.Visible = false;
                }
                // Dejar activo drop down list de entidades bancarias de la entidad
                ddlEntidad.Visible = false;
                ddlEntidad5.Visible = true;
                UpdatePanel2.Visible = false;
                UpdatePanel3.Visible = true;
            }
            else
            {
                UpdatePanel2.Visible = true;
                UpdatePanel3.Visible = false;
                lblCuenta.Visible = false;
                ddlCuenta.Visible = false;
                ddlEntidad.Visible = true;
                ddlEntidad5.Visible = false;
            }
    }

    /// <summary>
    /// Método para cuando da error al generar el comprobante se pueda regresar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        if (Session["OrigenComprobante"] != null)
            Navegar(Session["OrigenComprobante"].ToString());
    }

    protected void ddlTipoComprobante_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTipoComprobante.SelectedValue == "5")
            PanelFooter.Visible = true;
        else
            PanelFooter.Visible = false;
    }

    // Método para grabar datos del proceso contable
    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        // Determinar datos del usuario
        Usuario usuap = (Usuario)Session["usuario"];

        // Creando el proceso contable
        Xpinn.Contabilidad.Services.ProcesoContableService ProcesoService = new Xpinn.Contabilidad.Services.ProcesoContableService();
        Xpinn.Contabilidad.Entities.ProcesoContable eProceso = new Xpinn.Contabilidad.Entities.ProcesoContable();
        if (!txtFechaIni.TieneDatos)
        {
            VerError("Debe especificar la fecha inicial");
            return;
        }
        if (!txtFechaFin.TieneDatos)
        {
            VerError("Debe especificar la fecha final");
            return;
        }
        try
        {
            eProceso.cod_proceso = 0;
            eProceso.tipo_ope = Convert.ToInt64(ddlTipoOperacion.SelectedValue);
            eProceso.tipo_comp = Convert.ToInt64(ddlTipoComp.SelectedValue);
            eProceso.fecha_inicial = txtFechaIni.ToDateTime;
            eProceso.fecha_final = txtFechaFin.ToDateTime;
            eProceso.concepto = Convert.ToInt64(ddlConcep.SelectedValue);
            eProceso.cod_cuenta = ddlCodCuent.SelectedItem.Text;
            eProceso.cod_est_det = Convert.ToInt64(ddlEstructura.SelectedValue);
            if (ddlTipoMov.SelectedValue != "")
                eProceso.tipo_mov = Convert.ToInt32(ddlTipoMov.SelectedValue);
            else
                eProceso.tipo_mov = 0;
        }
        catch (Exception ex)
        {
            VerError("Se presento error al determinar datos del proceso." + ex.Message);
        }
        if (eProceso.tipo_comp == 0 || eProceso.tipo_comp == Int64.MinValue)
        {
            VerError("Debe especificar el tipo de comprobante");
            return;
        }

        try
        {
            eProceso = ProcesoService.CrearProcesoContable(eProceso, usuap);
        }
        catch (Exception ex)
        {
            VerError("Se presento error." + ex.Message);
            return;
        }

        // Ir a la pantalla de comprobantes
        mvComprobante.ActiveViewIndex = 3;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);

        // Determinar datos de la operación a generar
        Int64 pcod_ope = 0;
        if (Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] != null)
            pcod_ope = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"]);
        Int64 ptip_ope = 0;
        if (Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] != null)
            ptip_ope = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"]);
        DateTime pfecha = System.DateTime.Now;
        if (Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] != null)
            pfecha = Convert.ToDateTime(Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"]);
        Int64 pcod_ofi = usuap.cod_oficina;
        if (Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] != null)
            pcod_ofi = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"]);

        // Consultando el proceso de interfaz contable
        LstProcesoContable = ComprobanteServicio.ConsultaProceso(pcod_ope, ptip_ope, pfecha, (Usuario)Session["Usuario"]);

        // Generar el comprobante de la operación correspondiente
        GenerarComprobante(pcod_ope, ptip_ope, pfecha, pcod_ofi);
    }

    protected void ddlCodCuent_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlNomCuent.SelectedValue = ddlCodCuent.SelectedItem.ToString();
    }

    protected void ddlNomCuent_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCodCuent.SelectedValue = ddlNomCuent.SelectedItem.ToString();
    }

    protected void ddlCodCuent_TextChanged(object sender, EventArgs e)
    {
        ddlNomCuent.SelectedValue = ddlCodCuent.SelectedItem.ToString();
    }
}
