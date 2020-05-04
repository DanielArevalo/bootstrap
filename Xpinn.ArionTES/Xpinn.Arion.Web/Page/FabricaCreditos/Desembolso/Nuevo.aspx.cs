using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;
using Xpinn.Comun.Entities;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Interfaces.Entities;
using Xpinn.Interfaces.Services;
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Services;
using Xpinn.Util;
using System.Linq;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Services;

partial class Nuevo : GlobalWeb
{
    private CreditoService CreditoServicio = new CreditoService();
    private CreditoRecogerService creditoRecogerServicio = new CreditoRecogerService();
    private PeriodicidadService periodicidadServicio = new PeriodicidadService();
    private List<Xpinn.FabricaCreditos.Entities.ControlTiempos> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.ControlTiempos>();  //Lista de los menus desplegables
    private Xpinn.FabricaCreditos.Services.ControlTiemposService ControlProcesosServicio = new Xpinn.FabricaCreditos.Services.ControlTiemposService();
    private Xpinn.FabricaCreditos.Services.ControlCreditosService ControlCreditosServicio = new Xpinn.FabricaCreditos.Services.ControlCreditosService();
    private Xpinn.Auxilios.Services.SolicitudAuxilioServices AuxilioService = new Xpinn.Auxilios.Services.SolicitudAuxilioServices();
    private CreditoSolicitadoService creditoServicio = new CreditoSolicitadoService();
    private List<Persona1> lstDatoSolicitud = new List<Persona1>();  //Lista de los menus desplegables    
    private Persona1Service Persona1Servicio = new Persona1Service();
    //Variables para historico tasa
    CreditoPlan creditoc = new CreditoPlan();
    HistoricoTasa historico = new HistoricoTasa();
    private CreditoPlanService creditoPlanServicio = new CreditoPlanService();
    HistoricoTasaService historicoTasaService = new HistoricoTasaService();
    ClasificacionCartera clasificacionCartera = new ClasificacionCartera();
    ClasificacionCarteraService clasificacionCarteraService = new ClasificacionCarteraService();
    IList<HistoricoTasa> lsHistoricoTasas = new List<HistoricoTasa>();
    General valorParametro = new General();

    String ListaSolicitada = null;
    Usuario _usuario;
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[CreditoServicio.CodigoProgramaoriginal + ".id"] != null)
                VisualizarOpciones(CreditoServicio.CodigoProgramaoriginal, "E");
            else
                VisualizarOpciones(CreditoServicio.CodigoProgramaoriginal, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlValidarBiometria.eventoGuardar += btnGuardar_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaoriginal, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                _usuario = (Usuario)Session["usuario"];
                Session["HojaDeRuta"] = null;
                CargarListas();
                Panel5.Visible = true;
                if (Session[CreditoServicio.CodigoProgramaoriginal + ".id"] != null)
                {
                    if (Request.UrlReferrer != null)
                        if (Request.UrlReferrer.Segments[4].ToString() == "HojaDeRuta/")
                            Session["HojaDeRuta"] = "1";

                    idObjeto = Session[CreditoServicio.CodigoProgramaoriginal + ".id"].ToString();
                    ViewState["NumeroRadicacion"] = idObjeto;
                    ObtenerDatos(idObjeto);
                    CuotasExtras.TablaCuoExt(idObjeto);
                    // Determinar si se cuenta con permiso para modificar la tasa
                    Usuario usuap = new Usuario();
                    usuap = (Usuario)Session["usuario"];
                    int resultado = creditoRecogerServicio.ConsultaPermisoModificarTasa(Convert.ToString(usuap.codusuario), (Usuario)Session["Usuario"]);
                    if (resultado == 1)
                        chkTasa.Enabled = true;
                    else
                        chkTasa.Enabled = false;
                    txttasa.Enabled = false;
                    ddltipotasa.Enabled = false;

                    //VALIDAR SI ESTA ACTIVO LA OPCION ORDEN SERVICIO PARA LA LINEA.
                    if (txtCod_Linea_credito.Text != "")
                    {
                        Credito vCredito = new Credito();
                        vCredito = CreditoServicio.ConsultarCredito(Convert.ToInt64(ViewState["NumeroRadicacion"]), (Usuario)Session["usuario"]);

                        Xpinn.FabricaCreditos.Services.LineasCreditoService LineasCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();

                        Xpinn.FabricaCreditos.Entities.LineasCredito vLineasCredito = new Xpinn.FabricaCreditos.Entities.LineasCredito();
                        vLineasCredito = LineasCreditoServicio.ConsultaLineaCredito(Convert.ToString(txtCod_Linea_credito.Text), (Usuario)Session["usuario"]);
                        Session["NumCred_Orden"] = null;
                        if (vCredito.cod_proveedor != null)
                        {
                            txtorden.Text = Convert.ToString(vLineasCredito.orden_servicio);
                            // SI ES ORDEN DE SERVICIO NO ACTIVA EL PANEL DE FORMA DE DESEMBOLSO AL NO ACTIVARLO EVITA ENVIAR REGISTRO A GIRO.
                            Session["NumCred_Orden"] = txtNumero_radicacion.Text;
                            panelProveedor.Visible = true;
                            Panel5.Visible = false;
                            gvLista.Enabled = false;
                        }
                        else
                        {
                            Panel5.Visible = true;
                            panelProveedor.Visible = false;
                        }

                        panelDatosEducativo.Visible = false;
                        if (vLineasCredito.credito_educativo == 1)
                        {
                            panelDatosEducativo.Visible = true;
                            ObtenerDatosMatricula(idObjeto);
                        }
                    }
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaoriginal, "Page_Load", ex);
        }
    }

    protected void ObtenerDatosMatricula(string pNumRadicacion)
    {
        try
        {
            if (pNumRadicacion != null)
            {
                Xpinn.Auxilios.Entities.SolicitudAuxilio pEntidad = new Xpinn.Auxilios.Entities.SolicitudAuxilio();

                //CONSULTAR LOS DATOS DE LA MATRICULA
                Credito vCredito = new Credito();
                vCredito = CreditoServicio.ConsultarCreditoAsesor(Convert.ToInt64(pNumRadicacion), (Usuario)Session["usuario"]);
                if (vCredito.universidad != null)
                    txtUniversidad.Text = vCredito.universidad;
                if (vCredito.semestre != null)
                    txtSemestre.Text = vCredito.semestre;

                //CONSULTANDO DATOS DEL AUXILIO
                string pFiltro = " WHERE NUMERO_RADICACION = " + pNumRadicacion.ToString();
                pEntidad = AuxilioService.Consultar_Auxilio_Variado(pFiltro, (Usuario)Session["usuario"]);
                if (pEntidad.numero_auxilio != 0 && pEntidad.cod_persona != 0) //SI EXISTE EL AUXILIO ENLAZADO AL CREDITO EDUCATIVO
                {
                    txtNumAuxilio.Text = pEntidad.numero_auxilio.ToString();
                    decimal vrAuxilio = 0;
                    if (pEntidad.estado == "S")
                        vrAuxilio = pEntidad.valor_solicitado;
                    else
                        vrAuxilio = pEntidad.valor_aprobado;
                    txtVrAuxilio.Text = vrAuxilio.ToString("n0");
                    if (pEntidad.porc_matricula != 0)
                        txtVrMatricula.Text = pEntidad.porc_matricula.ToString("n0");
                }
                else
                {
                    txtVrAuxilio.Text = "0";
                    txtVrMatricula.Text = "0";
                    if (txtMonto.Text != "")
                        txtVrMatricula.Text = txtMonto.Text;
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaoriginal, "ObtenerDatosMatricula", ex);
        }
    }


    protected Boolean ValidarDatos()
    {
        if (Convert.ToDateTime(txtFecha.Text) < Convert.ToDateTime(Textfecha.Text))
        {
            VerError("La fecha de desembolso debe ser superior a la fecha de aprobación.");
            return false;
        }
        if (Panel5.Visible == true)
        {
            if (DropDownFormaDesembolso.SelectedIndex == 0)
            {
                VerError("Debe seleccionar la forma de pago para realizar el desembolso.");
                DropDownFormaDesembolso.Focus();
                return false;
            }
            if (DropDownFormaDesembolso.SelectedValue == "3")
            {
                //if (ddlNumeroCuenta.SelectedValue == "")
                if (txtNumeroCuenta.Text == "")
                {
                    VerError("Debe ingresar el número de la cuenta en la forma de desembolso.");
                    return false;
                }
            }
        }
        return true;
    }


    protected bool GenerarDesembolso()
    {
        // Cargar los créditos a recoger
        CreditoRecoger vCreditoRecoger = new CreditoRecoger();
        decimal TotalRecoge = 0;
        foreach (GridViewRow row in gvLista.Rows)
        {
            creditoRecogerServicio.EliminarCreditoRecoger(Convert.ToInt64(row.Cells[1].Text.Trim()), (Usuario)Session["usuario"]);
            if (((CheckBoxGrid)row.Cells[8].FindControl("chkRecoger")).Checked)
            {
                vCreditoRecoger.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text.Trim());
                vCreditoRecoger.numero_credito = Convert.ToInt64(row.Cells[1].Text.Trim());
                Int64 valor_recoge = Convert.ToInt64(row.Cells[4].Text.Trim().Replace(gSeparadorMiles, "")) + Convert.ToInt64(row.Cells[5].Text.Trim().Replace(".", "")) + Convert.ToInt64(row.Cells[6].Text.Trim().Replace(".", "")) + Convert.ToInt64(row.Cells[7].Text.Trim().Replace(".", ""));
                vCreditoRecoger.valor_recoge = valor_recoge;
                TotalRecoge += valor_recoge;
                vCreditoRecoger.fecha_pago = Convert.ToDateTime(txtFecha.Text);
                vCreditoRecoger.saldo_capital = Convert.ToInt64(row.Cells[4].Text.Trim().Replace(gSeparadorMiles, ""));
                vCreditoRecoger.valor_nominas = Convert.ToInt64(row.Cells[12].Text.Trim().Replace(gSeparadorMiles, ""));
                creditoRecogerServicio.CrearCreditoRecoger(vCreditoRecoger, (Usuario)Session["usuario"]);
                idObjeto = vCreditoRecoger.numero_radicacion.ToString();
            }
        }

        foreach (GridViewRow row in gvServiciosRecogidos.Rows)
        {
            string valorTotalTexto = HttpUtility.HtmlDecode(row.Cells[9].Text);
            decimal valorTotal = 0;

            Decimal.TryParse(valorTotalTexto, out valorTotal);

            TotalRecoge += valorTotal;
        }

        // Cargar los datos del crédito
        Credito cred = new Credito();
        Usuario usuap = (Usuario)Session["usuario"];
        cred.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);
        cred.estado = "C";
        cred.fecha_desembolso = Convert.ToDateTime(txtFecha.Text);
        DateTime fecha_inicio = Convert.ToDateTime(txtPrimerPago.ToDate);
        DateTime fechaactual = Convert.ToDateTime(txtFecha.Text);
        if (fecha_inicio < fechaactual)
        {
            VerError("La fecha de primer pago  no puede ser inferior a la fecha actual");
            return false;
        }
        else
        {
            cred.fecha_prim_pago = Convert.ToDateTime(txtPrimerPago.ToDate);
        }
        cred.cod_deudor = Convert.ToInt64(txtCodigo.Text);
        string sMonto = txtMonto.Text.Replace(".", "");
        cred.monto = Convert.ToInt64(sMonto);
        cred.cod_ope = 0;
        cred.cod_linea_credito = txtCod_Linea_credito.Text;
        Int64 VrAuxilio = 0;

        // Determinar si es una línea gerencial
        LineasCreditoService lineascredServicio = new LineasCreditoService();
        LineasCredito Lineas = new LineasCredito();
        Lineas = lineascredServicio.ConsultaLineaCredito(txtCod_Linea_credito.Text, (Usuario)Session["Usuario"]);
        if (Lineas.credito_gerencial == 1)
        {
            cred.monto = TotalRecoge;
        }
        else if (Lineas.credito_educativo == 1)
        {
            VrAuxilio = txtVrAuxilio.Text == "" ? 0 : Convert.ToInt64(txtVrAuxilio.Text.Replace(".", ""));
            cred.monto = Convert.ToDecimal(VrAuxilio);
        }

        //Validar si maneja orden de servicio
        if (Lineas.orden_servicio == 1 && panelProveedor.Visible == true)
        {
            if (txtPreImpreso.Text != "")
                cred.num_preimpreso = Convert.ToInt64(txtPreImpreso.Text);
            else
            {
                VerError("Ingrese el número de pre impresión");
                txtPreImpreso.Focus();
                return false;
            }
        }

        // Cargar atributos descontados/financiados a modificar del crédito
        #region atributos descontados
        cred.lstDescuentosCredito = new List<DescuentosCredito>();
        foreach (GridViewRow gFila in gvDeducciones.Rows)
        {
            try
            {
                DescuentosCredito eDescuento = new DescuentosCredito();
                eDescuento.numero_radicacion = cred.numero_radicacion;
                eDescuento.cod_atr = ValorSeleccionado((Label)gFila.FindControl("lblCodAtr"));
                eDescuento.nom_atr = Convert.ToString(gFila.Cells[1].ToString());
                eDescuento.tipo_descuento = ValorSeleccionado((DropDownList)gFila.FindControl("ddlTipoDescuento"));
                eDescuento.tipo_liquidacion = ValorSeleccionado((DropDownList)gFila.FindControl("ddlTipoLiquidacion"));
                eDescuento.forma_descuento = Convert.ToInt32(ValorSeleccionado((DropDownList)gFila.FindControl("ddlFormaDescuento")));
                eDescuento.val_atr = ValorSeleccionado((TextBox)gFila.FindControl("txtvalor"));
                eDescuento.numero_cuotas = ValorSeleccionado((TextBox)gFila.FindControl("txtnumerocuotas"));
                eDescuento.cobra_mora = ValorSeleccionado((CheckBox)gFila.FindControl("cbCobraMora"));
                cred.lstDescuentosCredito.Add(eDescuento);
            }
            catch (Exception ex)
            {
                VerError("Se presentaron errores al cargar atributos descontados/financiados a modificar del crédito. Error:" + ex.Message);
                return false;
            }
        }
        if (cred.lstDescuentosCredito != null)
            CreditoServicio.ModificarDescuentos(cred.lstDescuentosCredito, usuap);
        #endregion

        // Validar el monto
        if (TotalRecoge > cred.monto)
        {
            Credito vCredito = new Credito();
            vCredito = CreditoServicio.ConsultarCredito(Convert.ToInt64(txtNumero_radicacion.Text), (Usuario)Session["usuario"]);
            // Determinar si es un crédito re-estructurado                
            List<LineasCredito> lstLineas = new List<LineasCredito>();
            lstLineas = lineascredServicio.ListarLineasCreditoRes(Lineas, (Usuario)Session["Usuario"]);
            Boolean bRestructurado = false;
            foreach (LineasCredito lineas in lstLineas)
            {
                if (vCredito.cod_linea_credito == lineas.cod_linea_credito)
                    bRestructurado = true;
            }
            if (!bRestructurado)
            {
                VerError("El valor total de los créditos/servicios a recoger supera el monto del crédito");
            }
        }

        string sError = CreditoServicio.ValidarCredito(cred, usuap).Trim();
        if (sError != "")
        {
            VerError(sError + " (Validar Desembolso)");
            return false;
        }

        // Cargando los valores descontados
        cred.lstDescuentos = new List<DescuentosDesembolso>();
        foreach (GridViewRow row in gvLista0.Rows)
        {
            DescuentosDesembolso variable = new DescuentosDesembolso();
            decimales txtReferencia = (decimales)row.FindControl("TextBox1");
            if (txtReferencia.Text != "0" && txtReferencia.Text != "")
            {
                variable.numero_obligacion = Convert.ToString(txtNumero_radicacion.Text.Trim());
                variable.cod_deudor = Convert.ToInt64(txtCodigo.Text);
                variable.monto = Convert.ToInt64(txtMonto.Text.Replace(".", ""));
                variable.descuento = txtReferencia.Text.Replace(".", "");
                variable.tipo_tran = Convert.ToInt64(row.Cells[1].Text.Trim());
                variable.tipo_movimiento = Convert.ToInt64(row.Cells[0].Text.Trim());
                cred.lstDescuentos.Add(variable);
            }
        }

        // Cargando datos para el cheque
        if (ddlCuentaOrigen.Visible == true)
        {
            Xpinn.Caja.Services.BancosService BancosService = new Xpinn.Caja.Services.BancosService();
            if (DropDownFormaDesembolso.SelectedItem.Text == "Cheque")
                Session["numerocheque"] = BancosService.soporte(ddlCuentaOrigen.SelectedItem.ToString(), (Usuario)Session["Usuario"]);
            else
                Session.Remove("numerocheque");
            Session["entidad"] = ddlEntidadOrigen.SelectedValue;
            Session["cuenta"] = ddlCuentaOrigen.SelectedValue;
        }

        // Cargando datos de la cuenta para las transferencias
        Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
        Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
        int idctabancaria = 0, cod_banco = 0, tipo_cuenta = 0;
        string num_cuenta = "";
        bool pGenerarGiro = false;
        if (Panel5.Visible == true)
        {
            pGenerarGiro = true;
            Int32? idCta = null;
            if (ddlEntidadOrigen.SelectedValue != "")
            {
                CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ddlEntidadOrigen.SelectedValue), ddlCuentaOrigen.SelectedItem.Text, (Usuario)Session["usuario"]);
                idCta = CuentaBanc.idctabancaria;
            }

            TipoFormaDesembolso formaPago = DropDownFormaDesembolso.SelectedValue.ToEnum<TipoFormaDesembolso>();
            if (formaPago == TipoFormaDesembolso.Transferencia)
            {
                idctabancaria = Convert.ToInt32(idCta);
                cod_banco = Convert.ToInt32(DropDownEntidad.SelectedValue);
                //num_cuenta = ddlNumeroCuenta.SelectedValue;
                num_cuenta = txtNumeroCuenta.Text;
                tipo_cuenta = Convert.ToInt32(ddlTipo_cuenta.SelectedValue);
            }
            else if (formaPago == TipoFormaDesembolso.Cheque)
            {
                idctabancaria = Convert.ToInt32(idCta);
                cod_banco = 0; //NULO
                num_cuenta = null; //NULO
                tipo_cuenta = -1; //NULO
            }
            else if (formaPago == TipoFormaDesembolso.TranferenciaAhorroVistaInterna)
            {
                pGenerarGiro = false;
                cred.numero_cuenta_ahorro_vista = !string.IsNullOrWhiteSpace(ddlCuentaAhorroVista.SelectedValue) ? Convert.ToInt64(ddlCuentaAhorroVista.SelectedValue) : default(long?);

                if (!cred.numero_cuenta_ahorro_vista.HasValue)
                {
                    VerError("No tiene una cuenta de ahorro a la vista asociada para esta forma de pago!.");
                    return false;
                }

                tipo_cuenta = -1;
            }
            else
            {
                idctabancaria = 0;
                cod_banco = 0;
                num_cuenta = null;
                tipo_cuenta = -1;
            }
        }

        // Realizar el desembolso del crèdito
        sError = "";
        CreditoServicio.DesembolsarCredito(cred, pGenerarGiro, Convert.ToInt64(DropDownFormaDesembolso.SelectedValue), idctabancaria, cod_banco, num_cuenta, tipo_cuenta, ref sError, (Usuario)Session["usuario"]);
        if (sError != "")
        {
            VerError(sError);
            return false;
        }

        // Actualizar la tasa del crédito
        if (chkTasa.Checked)
            CreditoServicio.cambiotasa(txtNumero_radicacion.Text, "1", txttasa.Text, ddltipotasa.SelectedValue, "0", "", "", (Usuario)Session["Usuario"], "2");

        //VERIFICAR SI LA LINEA ES EDUCATIVA PARA GENERAR EL AUXILIO
        if (Lineas.credito_educativo == 1 && Lineas.maneja_auxilio == 1)
        {
            if (panelDatosEducativo.Visible == true && txtNumAuxilio.Text != "")
            {
                Xpinn.Auxilios.Entities.SolicitudAuxilio pEntidad = new Xpinn.Auxilios.Entities.SolicitudAuxilio();
                Xpinn.Auxilios.Entities.SolicitudAuxilio pAuxilio = new Xpinn.Auxilios.Entities.SolicitudAuxilio();

                //CONSULTANDO DATOS DEL AUXILIO
                string pFiltro = " WHERE NUMERO_RADICACION = " + txtNumero_radicacion.Text;
                pEntidad = AuxilioService.Consultar_Auxilio_Variado(pFiltro, (Usuario)Session["usuario"]);

                if (pEntidad.estado == "S" || pEntidad.estado == "A")
                {
                    pAuxilio.numero_auxilio = Convert.ToInt32(txtNumAuxilio.Text);
                    pAuxilio.fecha_desembolso = Convert.ToDateTime(txtFecha.Text);
                    pAuxilio.estado = pEntidad.estado;
                    pAuxilio.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);

                    //GRABAR TRAN_AUXILIO
                    Xpinn.Auxilios.Entities.DesembolsoAuxilio pDesem = new Xpinn.Auxilios.Entities.DesembolsoAuxilio();
                    pDesem.numero_transaccion = 0;
                    pDesem.numero_auxilio = Convert.ToInt64(txtNumAuxilio.Text);
                    pDesem.cod_ope = cred.cod_ope;
                    pDesem.cod_cliente = Convert.ToInt64(txtCodigo.Text);
                    pDesem.cod_linea_auxilio = pEntidad.cod_linea_auxilio;
                    pDesem.tipo_tran = 1;
                    pDesem.valor = VrAuxilio;
                    pDesem.estado = 1;
                    pDesem.num_tran_anula = 0; // NULL
                    string pError = "";

                    AuxilioService.Generar_desembolso_auxilio(pAuxilio, pDesem, ref pError, (Usuario)Session["usuario"]);
                    if (pError != "")
                    {
                        VerError(pError);
                        return false;
                    }
                }
            }
        }

        // Guardar datos para la hoja de ruta        
        consultarControl();

        // Guardar los datos de la cuenta del cliente y generar el comprobante si se pudo crear la operaciòn.
        if (cred.cod_ope != 0)
        {
            //if (ddlNumeroCuenta.SelectedValue != "")
            //    CreditoServicio.GuardarCuentaBancariaCliente(cred.cod_deudor, ddlNumeroCuenta.SelectedValue, Convert.ToInt64(ddlTipo_cuenta.SelectedValue), Convert.ToInt64(DropDownEntidad.SelectedValue), (Usuario)Session["Usuario"]);

            if (!string.IsNullOrWhiteSpace(txtNumeroCuenta.Text))
                CreditoServicio.GuardarCuentaBancariaCliente(cred.cod_deudor, txtNumeroCuenta.Text, Convert.ToInt64(ddlTipo_cuenta.SelectedValue), Convert.ToInt64(DropDownEntidad.SelectedValue), (Usuario)Session["Usuario"]);

            Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = cred.cod_ope;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 1;
            Session[ComprobanteServicio.CodigoPrograma + ".esDesembolsoCredito"] = 1;
            Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = cred.cod_deudor;
            Session[ComprobanteServicio.CodigoPrograma + ".numeroRadicacion"] = Convert.ToInt64(txtNumero_radicacion.Text);

            String CtaBancaria = Panel5.Visible == true && ddlCuentaOrigen.SelectedItem != null ? ddlCuentaOrigen.SelectedItem.Text : null;
            if (ddlCuentaOrigen.SelectedItem != null)
                Session[ComprobanteServicio.CodigoPrograma + ".CuentaBancaria"] = CtaBancaria;
        }

        CuotasExtras.GuardarCuotas(txtNumero_radicacion.Text);

        Session.Remove(CreditoServicio.CodigoProgramaoriginal + ".id");

        #region Interactuar WorkManagement


        // Parametro general para habilitar proceso de WM, lo dejo luego de aprobar porque confio mas en WM en no fallar que en Financial
        // Si fuera al revez y Financial fallara, WM estaria por delante y seria mas problema sincronizarlos de nuevo
        General parametroHabilitaOperacionesWM = ConsultarParametroGeneral(35);
        if (parametroHabilitaOperacionesWM != null && parametroHabilitaOperacionesWM.valor.Trim() == "1")
        {
            General parametroObligaOperacionesWM = ConsultarParametroGeneral(39);
            bool obligaOperaciones = parametroObligaOperacionesWM != null && parametroObligaOperacionesWM.valor == "1";
            bool operacionFueExitosa = false;

            try
            {
                WorkManagementServices workManagementService = new WorkManagementServices();

                // Se necesita el workFlowId para saber cual es el workflow que vamos a correrle el step
                // Este registro workFlowId es llenado en la solicitud del credito
                WorkFlowCreditos workFlowCredito = workManagementService.ConsultarWorkFlowCreditoPorNumeroRadicacion(Convert.ToInt32(txtNumero_radicacion.Text.Trim()), Usuario);

                if (workFlowCredito != null && workFlowCredito.workflowid > 0 && !string.IsNullOrWhiteSpace(workFlowCredito.barCodeRadicacion))
                {
                    InterfazWorkManagement interfaz = new InterfazWorkManagement(Usuario);
                    ReportViewerPlan.Visible = true;
                    WorkFlowFilesDTO file = new WorkFlowFilesDTO
                    {
                        Base64DataFile = Convert.ToBase64String(DevolverByteDeReportePlanPago(txtNumero_radicacion.Text)),
                        Descripcion = "Tabla amortizacion para credito con numero de radicacion: " + txtNumero_radicacion.Text,
                        Extension = ".pdf"
                    };
                    ReportViewerPlan.Visible = false;

                    bool anexoExitoso = interfaz.AnexarArchivoAWorkFlowCartera(file, workFlowCredito.workflowid, TipoArchivoWorkManagement.TablaAmortizacion);

                    if (anexoExitoso)
                    {
                        bool runTaskCargaTablaAmortizacion = interfaz.RunTaskWorkFlowCredito(workFlowCredito.barCodeRadicacion, workFlowCredito.workflowid, StepsWorkManagementWorkFlowCredito.CargaTablaAmortizacion, "Carga de tabla de amortizacion para el credito " + txtNumero_radicacion.Text);

                        operacionFueExitosa = runTaskCargaTablaAmortizacion;
                    }
                }

                if (obligaOperaciones && !operacionFueExitosa)
                {
                    VerError("No se pudo correr la tarea en el WM para este workFlow");
                    return false;
                }
            }
            catch (Exception)
            {
                if (obligaOperaciones)
                {
                    VerError("No se pudo correr la tarea en el WM para este workFlow");
                    return false;
                }
            }
        }


        #endregion

        return true;
    }


    byte[] DevolverByteDeReportePlanPago(string numeroRadicacion)
    {
        DatosPlanPagosService datosServicio = new DatosPlanPagosService();
        Session["AtributosPlanPagos"] = datosServicio.GenerarAtributosPlan(Usuario);
        Credito datosApp = new Credito
        {
            numero_radicacion = Convert.ToInt64(numeroRadicacion)
        };

        Session["PlanPagos"] = datosServicio.ListarDatosPlanPagos(datosApp, Usuario);

        Configuracion conf = new Configuracion();
        CreditoPlanService creditoPlanServicio = new CreditoPlanService();
        CreditoPlan credito = creditoPlanServicio.ConsultarCredito(Convert.ToInt64(numeroRadicacion), true, Usuario);

        CreditoRecogerService creditoRecogerServicio = new CreditoRecogerService();
        CreditoRecoger refe = new CreditoRecoger();
        List<CreditoRecoger> lstConsulta = creditoRecogerServicio.ConsultarCreditoRecoger(Convert.ToInt64(numeroRadicacion), Usuario);

        // LLenar data table con los datos a recoger
        DataTable table = new DataTable();
        table.Columns.Add("numero");
        table.Columns.Add("linea");
        table.Columns.Add("monto");
        table.Columns.Add("tasa");
        table.Columns.Add("saldo");
        table.Columns.Add("total");
        table.Columns.Add("Int_cte");
        table.Columns.Add("Int_Mora");
        table.Columns.Add("Otros");

        DataRow datarw;
        if (lstConsulta.Count == 0)
        {
            datarw = table.NewRow();
            datarw[0] = " ";
            datarw[1] = " ";
            datarw[2] = " ";
            datarw[3] = " ";
            datarw[4] = " ";
            datarw[5] = "0";
            datarw[6] = " ";
            datarw[7] = " ";
            datarw[8] = " ";
            table.Rows.Add(datarw);
        }
        else
        {
            CreditoService creditoservicio = new CreditoService();
            for (int i = 0; i < lstConsulta.Count; i++)
            {
                datarw = table.NewRow();
                refe = lstConsulta[i];
                datarw[0] = " " + refe.numero_radicacion;
                DateTime fecha = DateTime.Now;

                decimal intcorriente = 0, intmora = 0, intotros = 0;
                Credito referea = new Credito();
                if (refe.numero_radicacion.ToString() != "" && refe.numero_radicacion > 0)
                {
                    try
                    {
                        referea = creditoservicio.consultarinterescredito(refe.numero_radicacion, fecha, Usuario);
                        intcorriente = referea.intcoriente;
                        intmora = referea.interesmora;
                        intotros = referea.otros;
                    }
                    catch
                    {
                        intcorriente = 0;
                        intmora = 0;
                        intotros = 0;
                    }
                }
                datarw[1] = " " + refe.linea_credito;
                datarw[2] = " " + refe.monto.ToString("0,0");
                datarw[3] = " " + refe.interes_corriente.ToString("0,0");
                datarw[4] = " " + refe.saldo_capital.ToString("0,0");
                datarw[5] = " " + refe.valor_recoge.ToString("0,0");
                datarw[6] = " " + intcorriente.ToString("0,0");
                datarw[7] = " " + intmora.ToString("0,0");
                datarw[8] = " " + intotros.ToString("0,0");

                table.Rows.Add(datarw);
            }
        }

        // ---------------------------------------------------------------------------------------------------------
        // Traer los datos de los codeudores
        // ---------------------------------------------------------------------------------------------------------
        Persona1Service Persona1Servicio = new Persona1Service();
        Persona1 refere = new Persona1();
        List<Persona1> lstConsultas = Persona1Servicio.ListarPersona1(ObtenerValoresCodeudores(numeroRadicacion), Usuario);
        // LLenar la tabla con datos de codeudores
        DataTable table2 = new DataTable();
        table2.Columns.Add("codigo");
        table2.Columns.Add("identificacion");
        table2.Columns.Add("tipo");
        table2.Columns.Add("nombres");
        table2.Columns.Add("empresa");
        table2.Columns.Add("direccion");
        DataRow datarw2;
        if (lstConsultas.Count == 0)
        {
            datarw2 = table2.NewRow();
            datarw2[0] = " ";
            datarw2[1] = " ";
            datarw2[2] = " ";
            datarw2[3] = " ";
            datarw2[4] = " ";
            datarw2[5] = " ";
            table2.Rows.Add(datarw2);
        }
        else
        {
            for (int i = 0; i < lstConsultas.Count; i++)
            {
                datarw2 = table2.NewRow();
                refere = lstConsultas[i];
                datarw2[0] = " " + refere.cod_persona;
                datarw2[1] = " " + refere.identificacion;
                datarw2[2] = " " + refere.tipo_identificacion;
                datarw2[3] = " " + refere.primer_nombre + " " + refere.segundo_nombre + " " + refere.primer_apellido + " " + refere.segundo_apellido;
                datarw2[4] = " " + refere.empresa;
                datarw2[5] = " " + refere.direccionempresa;
                table2.Rows.Add(datarw2);

            }
        }

        // ---------------------------------------------------------------------------------------------------------
        // Traer los datos de las cuotas extras
        // ---------------------------------------------------------------------------------------------------------

        List<CuotasExtras> lstConsulta1 = new List<CuotasExtras>();
        CuotasExtrasService CuoExtServicio = new CuotasExtrasService();
        String Numero_solicitud = numeroRadicacion;
        if (Numero_solicitud != "")
        {
            lstConsulta1 = CuoExtServicio.ListarCuotasExtrasId(Convert.ToInt64(Numero_solicitud), Usuario);
        }
        DataTable table3 = new DataTable();
        table3.Columns.Add("fecha");
        table3.Columns.Add("valor");
        table3.Columns.Add("formapago");

        DataRow datarw3;
        if (lstConsulta1.Count == 0)
        {
            datarw3 = table3.NewRow();
            datarw3[0] = " ";
            datarw3[1] = " ";
            datarw3[2] = " ";
            table3.Rows.Add(datarw3);
        }
        else
        {
            foreach (CuotasExtras referen in lstConsulta1)
            {
                datarw3 = table3.NewRow();
                datarw3[0] = " " + referen.fecha_pago;
                datarw3[1] = " " + referen.valor;
                if (referen.forma_pago == "1")
                {
                    datarw3[2] = " Caja";
                }
                else
                {
                    datarw3[2] = " Nomina";
                }
                table3.Rows.Add(datarw3);
            }
        }

        // ---------------------------------------------------------------------------------------------------------
        // Pasar datos al reporte
        // ---------------------------------------------------------------------------------------------------------
        string cRutaDeImagen;
        cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";

        Usuario User = (Usuario)Session["usuario"];
        ReportParameter[] param = new ReportParameter[54];

        param[0] = new ReportParameter("Entidad", User.empresa);
        param[1] = new ReportParameter("numero_radicacion", HttpUtility.HtmlDecode(credito.Numero_radicacion.ToString()));
        param[2] = new ReportParameter("cod_linea_credito", HttpUtility.HtmlDecode(credito.LineaCredito));
        param[3] = new ReportParameter("linea", HttpUtility.HtmlDecode(credito.Linea));
        param[4] = new ReportParameter("nombre", HttpUtility.HtmlDecode(credito.Linea));
        param[5] = new ReportParameter("identificacion", HttpUtility.HtmlDecode(credito.Identificacion));
        param[6] = new ReportParameter("direccion", !string.IsNullOrWhiteSpace(credito.Direccion) ? credito.Direccion : " ");
        param[7] = new ReportParameter("ciudad", !string.IsNullOrWhiteSpace(credito.ciudad) ? credito.ciudad : " ");
        param[8] = new ReportParameter("fecha_inico", String.Format("{0:d}", Convert.ToDateTime(HttpUtility.HtmlDecode(credito.FechaInicio.ToString()))));
        param[9] = new ReportParameter("fecha_primer", credito.FechaPrimerPago.HasValue ? Convert.ToDateTime(credito.FechaPrimerPago).ToString(conf.ObtenerFormatoFecha()) : " ");
        param[10] = new ReportParameter("palzo", HttpUtility.HtmlDecode(credito.Plazo.ToString()));
        param[11] = new ReportParameter("fecha_generacion", credito.FechaSolicitud != DateTime.MinValue ? (credito.FechaSolicitud).ToString(gFormatoFecha) : " ");
        param[12] = new ReportParameter("periocidad", HttpUtility.HtmlDecode(credito.Periodicidad));
        param[13] = new ReportParameter("cuotas", credito.numero_cuotas);
        param[14] = new ReportParameter("valor_cuota", String.Format("{0:C}", Convert.ToDouble(HttpUtility.HtmlDecode(credito.Cuota.ToString()))));
        param[15] = new ReportParameter("forma_pago", HttpUtility.HtmlDecode(credito.FormaPago));
        param[16] = new ReportParameter("tasa_nominal", String.Format("{0:P2}", Convert.ToDouble(HttpUtility.HtmlDecode(credito.TasaNom.ToString())) / 100));
        param[17] = new ReportParameter("tasa_efectiva", String.Format("{0:P2}", credito.TasaEfe / 100));
        param[18] = new ReportParameter("desembolso", credito.Monto.ToString());
        param[19] = new ReportParameter("pagare", !string.IsNullOrWhiteSpace(credito.pagare) ? credito.pagare : " ");

        List<Atributos> lstAtr = new List<Atributos>();
        lstAtr = (List<Atributos>)Session["AtributosPlanPagos"];
        int j = 0;
        foreach (Atributos item in lstAtr)
        {
            param[20 + j] = new ReportParameter("titulo" + j, item.nom_atr);
            j = j + 1;
        }
        for (int i = j; i < 15; i++)
        {
            param[20 + i] = new ReportParameter("titulo" + i, " ");
        }
        List<DatosPlanPagos> lstPlan = new List<DatosPlanPagos>();
        lstPlan = (List<DatosPlanPagos>)Session["PlanPagos"];
        Boolean[] bVisible = new Boolean[16];
        for (int i = 1; i <= 15; i++)
        {
            bVisible[i] = false;
            i = i + 1;
        }
        foreach (DatosPlanPagos ItemPlanPagos in lstPlan)
        {
            if (ItemPlanPagos.int_1 != 0) { bVisible[1] = true; }
            if (ItemPlanPagos.int_2 != 0) { bVisible[2] = true; }
            if (ItemPlanPagos.int_3 != 0) { bVisible[3] = true; }
            if (ItemPlanPagos.int_4 != 0) { bVisible[4] = true; }
            if (ItemPlanPagos.int_5 != 0) { bVisible[5] = true; }
            if (ItemPlanPagos.int_6 != 0) { bVisible[6] = true; }
            if (ItemPlanPagos.int_7 != 0) { bVisible[7] = true; }
            if (ItemPlanPagos.int_8 != 0) { bVisible[8] = true; }
            if (ItemPlanPagos.int_9 != 0) { bVisible[9] = true; }
            if (ItemPlanPagos.int_10 != 0) { bVisible[10] = true; }
            if (ItemPlanPagos.int_11 != 0) { bVisible[11] = true; }
            if (ItemPlanPagos.int_12 != 0) { bVisible[12] = true; }
            if (ItemPlanPagos.int_13 != 0) { bVisible[13] = true; }
            if (ItemPlanPagos.int_14 != 0) { bVisible[14] = true; }
            if (ItemPlanPagos.int_15 != 0) { bVisible[15] = true; }
        }
        for (int i = 0; i < 15; i++)
        {
            param[35 + i] = new ReportParameter("visible" + i, bVisible[i + 1].ToString());
        }
        param[50] = new ReportParameter("nomUsuario", User.nombre);
        param[51] = new ReportParameter("ImagenReport", cRutaDeImagen);
        param[52] = new ReportParameter("fecha_solicitud", String.Format("{0:d}", Convert.ToDateTime(HttpUtility.HtmlDecode(credito.FechaSolicitud.ToString()))));
        if (credito.FechaDesembolso != DateTime.MinValue)
            param[53] = new ReportParameter("fecha_desembolso", String.Format("{0:d}", Convert.ToDateTime(HttpUtility.HtmlDecode(credito.FechaDesembolso.ToString()))));
        else
            param[53] = new ReportParameter("fecha_desembolso", DateTime.Now.ToString(gFormatoFecha));

        ReportViewerPlan.LocalReport.EnableExternalImages = true;
        ReportViewerPlan.LocalReport.SetParameters(param);

        ReportDataSource rds1 = new ReportDataSource("DataSet1", table);
        ReportDataSource rds2 = new ReportDataSource("DataSet2", table2);
        ReportDataSource rds3 = new ReportDataSource("DataSet3", table3);
        ReportDataSource rds = new ReportDataSource("DataSetPlanPagos", CrearDataTable(idObjeto));

        ReportViewerPlan.LocalReport.DataSources.Clear();
        ReportViewerPlan.LocalReport.DataSources.Add(rds);
        ReportViewerPlan.LocalReport.DataSources.Add(rds1);
        ReportViewerPlan.LocalReport.DataSources.Add(rds2);
        ReportViewerPlan.LocalReport.DataSources.Add(rds3);
        ReportViewerPlan.LocalReport.Refresh();

        return ObtenerBytesReporteAsPDF(ReportViewerPlan, FormatoArchivo.PDF);
    }

    Persona1 ObtenerValoresCodeudores(string numeroRadicacion)
    {
        Persona1 vPersona1 = new Persona1();

        if (numeroRadicacion != "")
            vPersona1.numeroRadicacion = Convert.ToInt64(numeroRadicacion);

        vPersona1.seleccionar = "CD"; //Bandera para ejecuta el select del CODEUDOR

        return vPersona1;
    }

    /// <summary>
    /// Generar tabla para pasar al reporte
    /// </summary>
    /// <returns></returns>
    public DataTable CrearDataTable(string numeroRadicacion)
    {
        DataTable table = new DataTable();

        DatosPlanPagos datosApp = new DatosPlanPagos
        {
            numero_radicacion = Int64.Parse(numeroRadicacion)
        };

        List<DatosPlanPagos> lstPlanPagos = (List<DatosPlanPagos>)Session["PlanPagos"];
        List<Atributos> lstAtr = (List<Atributos>)Session["AtributosPlanPagos"];

        table.Columns.Add("numerocuota");
        table.Columns.Add("fechacuota");
        table.Columns.Add("sal_ini");
        table.Columns.Add("capital");
        table.Columns.Add("int_1");
        table.Columns.Add("int_2");
        table.Columns.Add("int_3");
        table.Columns.Add("int_4");
        table.Columns.Add("int_5");
        table.Columns.Add("int_6");
        table.Columns.Add("int_7");
        table.Columns.Add("int_8");
        table.Columns.Add("int_9");
        table.Columns.Add("int_10");
        table.Columns.Add("int_11");
        table.Columns.Add("int_12");
        table.Columns.Add("int_13");
        table.Columns.Add("int_14");
        table.Columns.Add("int_15");
        table.Columns.Add("total");
        table.Columns.Add("sal_fin");

        foreach (DatosPlanPagos item in lstPlanPagos)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.numerocuota;
            if (item.fechacuota != null && item.total != 0)
            {
                datarw[1] = item.fechacuota.Value.ToShortDateString();
                datarw[2] = item.sal_ini.ToString("0,0");
                datarw[3] = item.capital.ToString("0,0");
                datarw[4] = item.int_1.ToString("0,0");
                datarw[5] = item.int_2.ToString("0,0");
                datarw[6] = item.int_3.ToString("0,0");
                datarw[7] = item.int_4.ToString("0,0");
                datarw[8] = item.int_5.ToString("0,0");
                datarw[9] = item.int_6.ToString("0,0");
                datarw[10] = item.int_7.ToString("0,0");
                datarw[11] = item.int_8.ToString("0,0");
                datarw[12] = item.int_9.ToString("0,0");
                datarw[13] = item.int_10.ToString("0,0");
                datarw[14] = item.int_11.ToString("0,0");
                datarw[15] = item.int_12.ToString("0,0");
                datarw[16] = item.int_13.ToString("0,0");
                datarw[17] = item.int_14.ToString("0,0");
                datarw[18] = item.int_15.ToString("0,0");
                datarw[19] = item.total.ToString("0,0");
                datarw[20] = item.sal_fin.ToString("0,0");
                table.Rows.Add(datarw);
            }
        }

        return table;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        try
        {
            if (!ValidarDatos())
                return;

            // Validar que los conceptos descontados no superen el valor del desembolso
            Int64 total = 0;
            foreach (GridViewRow row in gvLista0.Rows)
            {
                decimales txtReferencia = (decimales)row.FindControl("TextBox1");
                if (txtReferencia.Text == "")
                    txtReferencia.Text = "0";
                try
                {
                    total = Convert.ToInt64(txtReferencia.Text.Replace(".", "")) + total;
                }
                catch
                {
                    VerError("Error al convertir el valor descontado " + txtReferencia.Text);
                }
            }

            // Verificando valores descontados
            if (total > Convert.ToDouble(txtMonto.Text.Replace(".", "")))
            {
                VerError("El total de valores descontados (" + total.ToString() + ") supera el monto del crédito (" + txtMonto.Text + ")");
                return;
            }

            // Validar que exista la parametrización contable por procesos
            if (ValidarProcesoContable(Convert.ToDateTime(txtFecha.Text), 1) == false)
            {
                VerError("No se encontró parametrización contable por procesos para el tipo de operación 1 = Desembolso de Créditos");
                return;
            }

            // Validar la huella
            string codigoPrograma = CreditoServicio.CodigoProgramaoriginal;
            string sError = "";
            if (ctlValidarBiometria.IniciarValidacion(Convert.ToInt32(codigoPrograma), txtNumero_radicacion.Text, Convert.ToInt64(txtCodigo.Text), Convert.ToDateTime(txtFecha.Text), ref sError))
            {
                VerError(sError);
                return;
            }

            // Determinar código de proceso contable para generar el comprobante
            Int64? rpta = 0;
            if (!panelProceso.Visible && panelGeneral.Visible)
            {
                rpta = ctlproceso.Inicializar(1, Convert.ToDateTime(txtFecha.Text), (Usuario)Session["Usuario"]);
                if (rpta > 1)
                {
                    Site toolBar = (Site)Master;
                    toolBar.MostrarGuardar(false);
                    toolBar.MostrarCancelar(false);
                    toolBar.MostrarConsultar(false);
                    panelGeneral.Visible = false;
                    panelProceso.Visible = true;
                }
                else
                {
                    if (GenerarDesembolso())
                        Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaoriginal, "btnGuardar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (Session["HojaDeRuta"] != null)
        {
            if (Session["HojaDeRuta"].ToString() == "1")
            {
                Response.Redirect("~/Page/FabricaCreditos/HojaDeRuta/Lista.aspx");
            }
            else
            {
                Navegar(Pagina.Lista);
            }
        }
        else
        {
            Navegar(Pagina.Lista);
        }

    }

    public void grilladescuentos()
    {
        List<Credito> lstConsulta = new List<Credito>();
        Credito creditoRecoger = new Credito();

        lstConsulta = CreditoServicio.Consultardescuentos((Usuario)Session["usuario"]);
        gvLista0.DataSource = lstConsulta;
        if (lstConsulta.Count > 0)
        {
            lblTextoDeduc.Visible = true;
            gvLista0.Visible = true;

            gvLista0.DataBind();
            ValidarPermisosGrilla(gvLista0);
        }
        else
        {
            lblTextoDeduc.Visible = false;
            gvLista0.Visible = false;

        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Credito vCredito = new Credito();
            vCredito = CreditoServicio.ConsultarCredito(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            if (vCredito.numero_radicacion != Int64.MinValue)
                txtNumero_radicacion.Text = vCredito.numero_radicacion.ToString().Trim();
            if (vCredito.identificacion != string.Empty)
                txtIdentificacion.Text = HttpUtility.HtmlDecode(vCredito.identificacion.ToString().Trim());
            if (vCredito.cod_deudor != Int64.MinValue)
                txtCodigo.Text = HttpUtility.HtmlDecode(vCredito.cod_deudor.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.tipo_identificacion))
                txtTipo_identificacion.Text = HttpUtility.HtmlDecode(vCredito.tipo_identificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.nombre))
                txtNombre.Text = HttpUtility.HtmlDecode(vCredito.nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.cod_linea_credito))
                txtCod_Linea_credito.Text = HttpUtility.HtmlDecode(vCredito.cod_linea_credito.ToString().Trim());
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
            if (!string.IsNullOrEmpty(vCredito.observacion))
            {
                txtObs_solici.Text = HttpUtility.HtmlDecode(vCredito.observacion.ToString().Trim());
            }
            else
            {
                txtObs_solici.Text = "No hay observaciones";
            }


            Textfecha.Text = HttpUtility.HtmlDecode(Convert.ToDateTime(vCredito.fecha_aprobacion).ToString("dd/MM/yyyy").Trim());
            txtFecha.Text = DateTime.Today.ToShortDateString();

            if (vCredito.fecha_prim_pago != null)
                txtPrimerPago.ToDateTime = Convert.ToDateTime(vCredito.fecha_prim_pago);
            else
            {
                try
                {
                    DateTime? FechaInicio = CreditoServicio.FechaInicioDESEMBOLSO(Convert.ToInt64(txtNumero_radicacion.Text), (Usuario)Session["Usuario"]);
                    if (FechaInicio != null)
                        txtPrimerPago.Text = Convert.ToDateTime(FechaInicio).ToShortDateString();
                }
                catch
                { }
            }
            if (!string.IsNullOrEmpty(vCredito.idenprov))
                txtIdentificacionprov.Text = HttpUtility.HtmlDecode(vCredito.idenprov.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.nomprov))
                txtNombreProveedor.Text = HttpUtility.HtmlDecode(vCredito.nomprov.ToString().Trim());

            Int64 NumPreImpreso = CreditoServicio.ObtenerNumeroPreImpreso((Usuario)Session["usuario"]);
            txtPreImpreso.Text = NumPreImpreso.ToString();

            // Activando datos para la orden de servicio
            if (txtCod_Linea_credito.Text != "")
            {
                LineasCreditoService lineascredServicio = new LineasCreditoService();
                LineasCredito lineasCred = new LineasCredito();
                lineasCred = lineascredServicio.ConsultaLineaCredito(txtCod_Linea_credito.Text, (Usuario)Session["Usuario"]);
                if (lineasCred != null)
                {
                    if (lineasCred.orden_servicio == 1)
                    {
                        txtorden.Text = Convert.ToString(lineasCred.orden_servicio);
                    }
                }
            }

            // Llenando listas desplegables

            ddlTipo_cuenta.Items.Insert(0, new ListItem("Ahorros", "0"));
            ddlTipo_cuenta.Items.Insert(1, new ListItem("Corriente", "1"));
            ddlTipo_cuenta.DataBind();

            DropDownFormaDesembolso.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            DropDownFormaDesembolso.Items.Insert(1, new ListItem("Efectivo", "1"));
            DropDownFormaDesembolso.Items.Insert(2, new ListItem("Cheque", "2"));
            DropDownFormaDesembolso.Items.Insert(3, new ListItem("Transferencia", "3"));
            DropDownFormaDesembolso.Items.Insert(4, new ListItem("TranferenciaAhorroVistaInterna", "4"));
            DropDownFormaDesembolso.Items.Insert(5, new ListItem("Otros", "5"));
            DropDownFormaDesembolso.DataBind();
            DropDownFormaDesembolso.SelectedIndex = 1;

            ActivarDesembolso();

            Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
            Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
            DropDownEntidad.DataSource = bancoService.ListarBancos(banco, (Usuario)Session["usuario"]);
            DropDownEntidad.DataTextField = "nombrebanco";
            DropDownEntidad.DataValueField = "cod_banco";
            DropDownEntidad.DataBind();

            ddlEntidadOrigen.DataSource = bancoService.ListarBancosEntidad(banco, (Usuario)Session["usuario"]);
            ddlEntidadOrigen.DataTextField = "nombrebanco";
            ddlEntidadOrigen.DataValueField = "cod_banco";
            ddlEntidadOrigen.DataBind();
            CargarCuentas();

            Actualizar();

            AhorroVistaServices ahorroServices = new AhorroVistaServices();
            //List<AhorroVista> lstAhorros = ahorroServices.ListarCuentaAhorroVista(Convert.ToInt64(txtCodigo.Text), Usuario); Remplazado para evitar errores con listar las cuentas de ahorros a la vista
            List<AhorroVista> lstAhorros = ahorroServices.ListarCuentaAhorroVistaGiros(Convert.ToInt64(txtCodigo.Text), Usuario);

            ddlCuentaAhorroVista.DataSource = lstAhorros;
            ddlCuentaAhorroVista.DataTextField = "numero_cuenta";
            ddlCuentaAhorroVista.DataValueField = "numero_cuenta";
            ddlCuentaAhorroVista.DataBind();

            ConsultarServiciosMarcadosParaRecoger();

            ActividadesServices ActividadServicio = new ActividadesServices();
            string filtro = string.Empty;
            List<CuentasBancarias> LstCuentasBanc = ActividadServicio.ConsultarCuentasBancarias(vCredito.cod_deudor, filtro, (Usuario)Session["usuario"]);

            ViewState.Add("lstCuentasDesembolsoCreditos", LstCuentasBanc);

            if (!string.IsNullOrWhiteSpace(vCredito.forma_desembolso))
            {
                DropDownFormaDesembolso.SelectedValue = vCredito.forma_desembolso;
                TipoFormaDesembolso formaDesembolso = vCredito.forma_desembolso.ToEnum<TipoFormaDesembolso>();

                ActivarDesembolso();

                if (formaDesembolso == TipoFormaDesembolso.Transferencia)
                {
                    if (!string.IsNullOrWhiteSpace(vCredito.cod_banco) && !string.IsNullOrWhiteSpace(vCredito.numero_cuenta))
                    {
                        DropDownEntidad.SelectedValue = vCredito.cod_banco;
                        txtNumeroCuenta.Text = vCredito.numero_cuenta;
                        try { ddlTipo_cuenta.SelectedValue = vCredito.tipocuenta; }
                        catch { VerError("No se pudo determinar el tipo de cuenta. Tipo de Cuenta:" + vCredito.tipocuenta); }
                    }
                }
                else if (formaDesembolso == TipoFormaDesembolso.TranferenciaAhorroVistaInterna)
                {
                    if (!string.IsNullOrWhiteSpace(vCredito.numero_cuenta))
                    {
                        ddlCuentaAhorroVista.SelectedValue = vCredito.numero_cuenta;
                    }
                }
            }
            txtFecha_CalendarExtender.StartDate = vCredito.fecha_aprobacion;

            // Mostrar los datos de descuentos del crédito
            #region consultar datos de atributos deducciones
            try
            {
                lblAtrDesc.Visible = false;
                List<DescuentosCredito> lstDescuentosCredito = new List<DescuentosCredito>();
                DescuentosCredito credito = new DescuentosCredito();
                credito.numero_radicacion = Convert.ToInt64(pIdObjeto);
                credito.cod_linea = ConvertirStringToInt32(vCredito.cod_linea_credito);
                lstDescuentosCredito = creditoServicio.ListarDescuentosCredito(credito, _usuario);
                if (lstDescuentosCredito != null)
                {
                    lblAtrDesc.Visible = true;
                    gvDeducciones.DataSource = lstDescuentosCredito;
                    gvDeducciones.DataBind();
                }
            }
            catch { }
            #endregion
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaoriginal, "ObtenerDatos", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<CreditoRecoger> lstConsulta = new List<CreditoRecoger>();
            CreditoRecoger creditoRecoger = new CreditoRecoger();
            creditoRecoger.numero_radicacion = Convert.ToInt64(idObjeto);
            lstConsulta = creditoRecogerServicio.ListarCreditoRecoger(creditoRecoger, (Usuario)Session["usuario"]);
            gvLista.PageSize = 10;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotRec.Visible = true;
                txtVrTotRecoger.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
            }
            else
            {
                gvLista.Visible = false;
                lblTotRec.Visible = false;
                txtVrTotRecoger.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "No hay créditos para recoger";
            }
            Session.Add(CreditoServicio.CodigoProgramaoriginal + ".consulta", 1);
            grilladescuentos();
            if (txtorden.Text == "1")
            {
                gvLista.Enabled = false;
                if (gvLista.Rows.Count > 0)
                {
                    lblTotRec.Visible = true;
                    txtVrTotRecoger.Visible = true;
                }
                lblordenservicios.Text = "No se pueden recoger créditos porque la línea maneja orden de servicio.";
                lblordenservicios.Visible = true;
            }
            CalcularTotalRecoge();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaoriginal, "Actualizar", ex);
        }

        // Según parametro del WEBCONFIG no marcar los créditos a recoger
        if (GlobalWeb.gMarcarRecogerDesembolso == "1")
        {
            foreach (GridViewRow row in gvLista.Rows)
            {
                ((CheckBoxGrid)row.FindControl("chkRecoger")).Checked = false;
            }
        }

    }

    protected void CalcularTotalRecoge()
    {
        Decimal pVrTotal = 0;
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            if (!dejarCuotaPendiente())
                rFila.Cells[12].Text = "0";
            CheckBoxGrid chkRecogerRow = (CheckBoxGrid)rFila.FindControl("chkRecoger");
            if (chkRecogerRow.Checked)
            {
                pVrTotal += rFila.Cells[4].Text != "&nbsp;" && rFila.Cells[4].Text != "" ? Convert.ToDecimal(gvLista.DataKeys[rFila.RowIndex].Values[1].ToString()) : 0;
                decimal pValorNominas = rFila.Cells[12].Text != "&nbsp;" && rFila.Cells[12].Text != "" ? Convert.ToDecimal(rFila.Cells[12].Text.ToString()) : 0;
                pVrTotal -= pValorNominas;
            }
        }

        foreach (GridViewRow row in gvServiciosRecogidos.Rows)
        {
            string valorTotalTexto = HttpUtility.HtmlDecode(row.Cells[9].Text);
            decimal valorTotal = 0;

            Decimal.TryParse(valorTotalTexto, out valorTotal);

            pVrTotal += valorTotal;
        }

        txtVrTotRecoger.Text = "0";
        if (pVrTotal != 0)
            txtVrTotRecoger.Text = pVrTotal.ToString();
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            ObtenerDatos(idObjeto);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaoriginal, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void DropDownFormaDesembolso_SelectedIndexChanged(object sender, EventArgs e)
    {
        ActivarDesembolso();
    }


    protected void ActivarDesembolso()
    {
        TipoFormaDesembolso formaDesembolso = DropDownFormaDesembolso.SelectedValue.ToEnum<TipoFormaDesembolso>();

        OcultarControlesDesembolso();

        if (formaDesembolso == TipoFormaDesembolso.Cheque)
        {
            ddlEntidadOrigen.Visible = true;
            ddlCuentaOrigen.Visible = true;
            lblEntidadOrigen.Visible = true;
            lblNumCuentaOrigen.Visible = true;
            pnlCuentasBancarias.Visible = true;
        }
        else if (formaDesembolso == TipoFormaDesembolso.Transferencia)
        {
            lblEntidad.Visible = true;
            lblNumCuenta.Visible = true;
            lblTipoCuenta.Visible = true;
            txtNumeroCuenta.Visible = true;
            DropDownEntidad.Visible = true;
            ddlTipo_cuenta.Visible = true;
            ddlEntidadOrigen.Visible = true;
            ddlCuentaOrigen.Visible = true;
            lblEntidadOrigen.Visible = true;
            lblNumCuentaOrigen.Visible = true;
            pnlCuentasBancarias.Visible = true;

            //Agregado para cargar automaticamente la cuenta bancaria que se encuentre cargada como principal en la afiliación
            ActividadesServices ActividadServicio = new ActividadesServices();
            List<CuentasBancarias> lstCuentasBanc = new List<CuentasBancarias>();
            CuentasBancarias pCuentaBancaria = new CuentasBancarias();
            string filtro = " AND perc.PRINCIPAL = 1";
            lstCuentasBanc = ActividadServicio.ConsultarCuentasBancarias(Convert.ToInt64(txtCodigo.Text), filtro, (Usuario)Session["usuario"]);
            if (lstCuentasBanc != null)
            {
                if (lstCuentasBanc.Count > 0)
                {
                    pCuentaBancaria = lstCuentasBanc.FirstOrDefault();
                    if (pCuentaBancaria.numero_cuenta != "" && pCuentaBancaria.numero_cuenta != null)
                    {
                        txtNumeroCuenta.Text = pCuentaBancaria.numero_cuenta;
                        DropDownEntidad.SelectedValue = pCuentaBancaria.cod_banco.ToString();
                        pCuentaBancaria.tipo_cuenta = (pCuentaBancaria.tipo_cuenta == 0 || pCuentaBancaria.tipo_cuenta == 1) ? pCuentaBancaria.tipo_cuenta : 0;
                        try { ddlTipo_cuenta.SelectedValue = pCuentaBancaria.tipo_cuenta.ToString(); } catch { VerError("Error en el tipo de cuenta bancaria. Tipo:" + pCuentaBancaria.tipo_cuenta); }
                    }
                }
            }
        }
        else if (formaDesembolso == TipoFormaDesembolso.TranferenciaAhorroVistaInterna)
        {
            ddlCuentaAhorroVista.Visible = true;
            lblCuentaAhorroVista.Visible = true;
            pnlCuentaAhorroVista.Visible = true;
        }
    }

    void OcultarControlesDesembolso()
    {
        pnlCuentaAhorroVista.Visible = false;
        pnlCuentasBancarias.Visible = false;
        ddlEntidadOrigen.Visible = false;
        ddlCuentaOrigen.Visible = false;
        lblEntidadOrigen.Visible = false;
        lblNumCuentaOrigen.Visible = false;
        lblEntidad.Visible = false;
        lblNumCuenta.Visible = false;
        lblTipoCuenta.Visible = false;
        //ddlNumeroCuenta.Visible = false;
        txtNumeroCuenta.Visible = false;
        DropDownEntidad.Visible = false;
        ddlTipo_cuenta.Visible = false;
        ddlCuentaAhorroVista.Visible = false;
        lblCuentaAhorroVista.Visible = false;
    }

    protected void ddlEntidadOrigen_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarCuentas();
    }

    /// <summary>
    /// Método para cargar las centas bancarias según el banco seleccionado
    /// </summary>
    protected void CargarCuentas()
    {
        Int64 codbanco = 0;
        try
        {
            codbanco = Convert.ToInt64(ddlEntidadOrigen.SelectedValue);
        }
        catch
        {
        }
        if (codbanco != 0)
        {
            Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
            Usuario usuario = (Usuario)Session["usuario"];
            ddlCuentaOrigen.DataSource = bancoService.ListarCuentaBancos(codbanco, usuario);
            ddlCuentaOrigen.DataTextField = "num_cuenta";
            ddlCuentaOrigen.DataValueField = "idctabancaria";
            ddlCuentaOrigen.DataBind();
        }
    }

    /// <summary>
    /// Método para activar el cambio de tasa
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkTasa_CheckedChanged(object sender, EventArgs e)
    {
        if (chkTasa.Checked)
        {
            txttasa.Enabled = true;
            ddltipotasa.Enabled = true;
        }
        else
        {
            txttasa.Enabled = false;
            ddltipotasa.Enabled = false;
        }
    }

    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = ControlProcesosServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
    }

    private void CargarListas()
    {
        try
        {
            ListaSolicitada = "EstadoProceso";
            TraerResultadosLista();
            ddlProceso.DataSource = lstDatosSolicitud;
            ddlProceso.DataTextField = "ListaDescripcion";
            ddlProceso.DataValueField = "ListaId";
            ddlProceso.DataBind();
            ddlProceso.SelectedIndex = 10;

            // Traer datos de la tasa                    
            ddltipotasa.DataSource = periodicidadServicio.ListarTipoTasa((Usuario)Session["Usuario"]);
            ddltipotasa.DataTextField = "tipotasa";
            ddltipotasa.DataValueField = "cod_tipotasa";
            ddltipotasa.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    private void GuardarControl()
    {
        Usuario pUsuario = (Usuario)Session["usuario"];

        Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();
        vControlCreditos.numero_radicacion = Convert.ToInt64(this.txtNumero_radicacion.Text);
        vControlCreditos.numero_radicacion = Convert.ToInt64(this.txtNumero_radicacion.Text);
        vControlCreditos.codtipoproceso = ddlProceso.SelectedItem != null ? ddlProceso.SelectedValue : null;
        vControlCreditos.fechaproceso = Convert.ToDateTime(DateTime.Now);
        vControlCreditos.cod_persona = pUsuario.codusuario;
        vControlCreditos.cod_motivo = 0;
        vControlCreditos.observaciones = "CREDITO DESEMBOLSADO";

        vControlCreditos.anexos = null;
        vControlCreditos.nivel = 0;
        String FechaDatcaredito = ""; ;

        if (Session["Datacredito"] == null)
        {
            if (FechaDatcaredito == "" || FechaDatcaredito == null || Session["Datacredito"].ToString() == "" || Session["Datacredito"].ToString() == "" || Session["Datacredito"] == null)
            {
                vControlCreditos.fechaconsulta_dat = FechaDatcaredito == "" ? DateTime.MinValue : Convert.ToDateTime(FechaDatcaredito.Trim());
            }
            else
            {
                if (FechaDatcaredito != null || FechaDatcaredito != "" || Session["Datacredito"] != null)
                {
                    FechaDatcaredito = Convert.ToString(Session["Datacredito"].ToString());
                    vControlCreditos.fechaconsulta_dat = Convert.ToDateTime(FechaDatcaredito);
                }
            }
        }
        else
        {
            //FechaDatcaredito = Convert.ToString(Session["Datacredito"].ToString());
            //vControlCreditos.fechaconsulta_dat = Convert.ToDateTime(FechaDatcaredito);
        }

        vControlCreditos = ControlCreditosServicio.CrearControlCreditos(vControlCreditos, (Usuario)Session["usuario"]);

    }

    private void consultarControl()
    {
        try
        {
            Usuario pUsuario = (Usuario)Session["usuario"];
            Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();
            String radicado = Convert.ToString(this.txtNumero_radicacion.Text);

            CreditoSolicitado proceso = new CreditoSolicitado();
            proceso.estado = "Desembolsado";
            if (pUsuario.idioma == "1") { proceso.estado = "Debourse"; } else if (pUsuario.idioma == "2") { proceso.estado = "Disbursed"; } else { proceso.estado = "Desembolsado"; }
            // Consultar el proceso para aprobacion
            proceso = creditoServicio.ConsultarCodigodelProceso(proceso, (Usuario)Session["usuario"]);
            ddlProceso.SelectedValue = Convert.ToString(proceso.Codigoproceso);

            vControlCreditos = ControlCreditosServicio.ConsultarControl_Procesos(Convert.ToInt64(ddlProceso.SelectedValue), radicado, (Usuario)Session["usuario"]);
            Int64 Controlexiste = 0;
            Controlexiste = Convert.ToInt64(vControlCreditos.codtipoproceso);
            if (Controlexiste <= 0)
            {
                GuardarControl();
            }
        }
        catch
        { }
    }


    protected void chkRecoger_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            //VALIDACION SI CUMPLE EL MINIMO Y MAXIMO DE REFINANCIACION
            LineasCredito pDatosLinea = new LineasCredito();
            Xpinn.FabricaCreditos.Data.LineasCreditoData vData = new Xpinn.FabricaCreditos.Data.LineasCreditoData();

            CheckBoxGrid chkRecoger = (CheckBoxGrid)sender;
            int nItem = Convert.ToInt32(chkRecoger.CommandArgument);
            if (chkRecoger.Checked)
            {
                foreach (GridViewRow rFila in gvLista.Rows)
                {
                    if (rFila.RowIndex == nItem)
                    {
                        //CAPTURAR EL CODIGO DE LA LINEA
                        string Linea = rFila.Cells[2].Text;
                        string[] sDatos = Linea.ToString().Split('-');
                        string cod_linea = sDatos[0].ToString();
                        if (cod_linea != "")
                        {
                            pDatosLinea = vData.ConsultaLineaCredito(cod_linea, (Usuario)Session["usuario"]);
                            //VARIABLES A VALIDAR
                            decimal minimo = 0, maximo = 0;
                            minimo = Convert.ToDecimal(pDatosLinea.minimo_refinancia);
                            maximo = Convert.ToDecimal(pDatosLinea.maximo_refinancia);
                            if (pDatosLinea.tipo_refinancia == 0) //SI ES POR RANGO DE SALDO
                            {
                                //RECUPERAR SALDO CAPITAL
                                decimal saldoCapital = 0;
                                if (rFila.Cells[4].Text != "" && rFila.Cells[4].Text != "&nbsp;")
                                    saldoCapital = Convert.ToDecimal(rFila.Cells[4].Text);
                                if (saldoCapital < minimo || saldoCapital > maximo)
                                {
                                    chkRecoger.Checked = false;
                                    VerError("No puede recoger este credito ya que el Saldo Capital esta fuera del Rango establecido");
                                }
                            }
                            else if (pDatosLinea.tipo_refinancia == 2) //SI ES POR % SALDO
                            {
                                //RECUPERAR SALDO CAPITAL/MONTO
                                decimal saldoCapital = 0, monto = 0, valor = 0;
                                if (rFila.Cells[4].Text != "" && rFila.Cells[4].Text != "&nbsp;")
                                    saldoCapital = Convert.ToDecimal(rFila.Cells[4].Text);
                                if (rFila.Cells[3].Text != "" && rFila.Cells[3].Text != "&nbsp;")
                                    monto = Convert.ToDecimal(rFila.Cells[3].Text);
                                valor = saldoCapital / monto;
                                if (valor < minimo || valor > maximo)
                                {
                                    chkRecoger.Checked = false;
                                    VerError("No puede recoger este credito ya que el valor calculado esta fuera del Rango establecido");
                                }
                            }
                            else if (pDatosLinea.tipo_refinancia == 3) // SI ES POR % CUOTAS PAGAS
                            {
                                //RECUPERAR LAS CUOTAS PAGADAS
                                decimal cuotas = 0;
                                if (rFila.Cells[11].Text != "" && rFila.Cells[11].Text != "&nbsp;")
                                    cuotas = Convert.ToDecimal(rFila.Cells[11].Text);
                                if (cuotas < minimo || cuotas > maximo)
                                {
                                    chkRecoger.Checked = false;
                                    VerError("No puede recoger este credito ya que las cuotas Pagadas estan fuera del Rango establecido");
                                }
                            }
                        }
                    }
                }
            }
            CalcularTotalRecoge();
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
            GenerarDesembolso();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void gvServiciosRecogidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvServiciosRecogidos.PageIndex = e.NewPageIndex;
        ConsultarServiciosMarcadosParaRecoger();
    }

    void ConsultarServiciosMarcadosParaRecoger()
    {
        long numeroCredito = Convert.ToInt64(ViewState["NumeroRadicacion"]);

        SolicitudCredServRecogidosService serviciosRecogidosServices = new SolicitudCredServRecogidosService();
        List<SolicitudCredServRecogidos> listaServicios = serviciosRecogidosServices.ListarSolicitudCredServRecogidosActualizado(numeroCredito, Usuario);

        gvServiciosRecogidos.ShowHeaderWhenEmpty = true;
        gvServiciosRecogidos.EmptyDataText = "No se encontraron servicios para recoger!.";
        gvServiciosRecogidos.DataSource = listaServicios;
        gvServiciosRecogidos.DataBind();
        CalcularTotalRecoge();
    }

    void ValidatasaUsuara(CreditoSolicitado pcredito, List<LineasCredito> lstAtributos)
    {
        valorParametro = ConsultarParametroGeneral(981, (Usuario)Session["usuario"]);
        decimal totalAtributos = 0;
        creditoc.Numero_radicacion = pcredito.NumeroCredito;
        creditoc = creditoPlanServicio.ConsultarCredito(creditoc.Numero_radicacion, true, (Usuario)Session["usuario"]);
        clasificacionCartera = clasificacionCarteraService.ConsultarClasificacion(creditoc.cod_clasifica,
            (Usuario)Session["usuario"]);
        lsHistoricoTasas = historicoTasaService.listarhistorico(clasificacionCartera.tipo_historico, (Usuario)Session["usuario"]);
        foreach (HistoricoTasa historicoTasa in lsHistoricoTasas)
        {
            if (creditoc.FechaAprobacion >= historicoTasa.FECHA_INICIAL && creditoc.FechaAprobacion <= historicoTasa.FECHA_FINAL)
            {
                historico.VALOR = historicoTasa.VALOR;
            }
        }

        foreach (LineasCredito item in lstAtributos)
        {
            if (item.tasa > 0)
            {
                totalAtributos += Convert.ToDecimal(item.tasa);
            }
        }

        if (totalAtributos > historico.VALOR)
        {
            var texto = "";
            texto = "Las tasas parametrizadas en el crédito superan la tasa de usura. Parametro General: 981 ";

            labelWarning.InnerText = texto;
            labelWarning.Visible = true;
        }
    }

    #region métodos para atributos de deducciones
    protected Int32? ValorSeleccionado(DropDownList ddlControl)
    {
        if (ddlControl != null)
            if (ddlControl.SelectedValue != null)
                if (ddlControl.SelectedValue != "")
                    return Convert.ToInt32(ddlControl.SelectedValue);
        return null;
    }
    protected decimal? ValorSeleccionado(TextBox txtControl)
    {
        if (txtControl != null)
            if (txtControl.Text != null)
                if (txtControl.Text != "")
                    return ConvertirStringToDecimal(txtControl.Text);
        return null;
    }
    protected Int32? ValorSeleccionado(Label txtControl)
    {
        if (txtControl != null)
            if (txtControl.Text != null)
                if (txtControl.Text != "")
                    return Convert.ToInt32(txtControl.Text);
        return null;
    }
    protected int ValorSeleccionado(CheckBox txtControl)
    {
        if (txtControl != null)
            if (txtControl.Checked != null)
                return Convert.ToInt32(txtControl.Checked);
        return 0;
    }

    protected List<ListasFijas> ListarTiposdeDecuento()
    {
        GlobalWeb glob = new GlobalWeb();
        return glob.ListaCreditoTipoDeDescuento();
    }
    protected List<ListasFijas> ListaCreditoTipoDeLiquidacion()
    {
        GlobalWeb glob = new GlobalWeb();
        return glob.ListaCreditoTipoDeLiquidacion();
    }
    protected List<ListasFijas> ListaCreditoFormadeDescuento()
    {
        GlobalWeb glob = new GlobalWeb();
        return glob.ListaCreditoFormadeDescuento();
    }
    protected List<ListasFijas> ListaImpuestos()
    {
        Xpinn.FabricaCreditos.Services.LineasCreditoService LineasCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        List<Xpinn.Comun.Entities.ListasFijas> lstImpuestos = new List<Xpinn.Comun.Entities.ListasFijas>();
        lstImpuestos.Add(new Xpinn.Comun.Entities.ListasFijas { codigo = "", descripcion = "" });
        lstImpuestos.Add(new Xpinn.Comun.Entities.ListasFijas { codigo = "0", descripcion = "" });
        List<LineasCredito> lstResult = new List<LineasCredito>();
        lstResult = LineasCreditoServicio.ddlimpuestos(_usuario);
        if (lstResult.Count > 0)
        {
            foreach (LineasCredito nLinea in lstResult)
            {
                lstImpuestos.Add(new Xpinn.Comun.Entities.ListasFijas { codigo = nLinea.cod_atr.ToString(), descripcion = nLinea.nombre });
            }
        }
        return lstImpuestos;
    }
    #endregion

}