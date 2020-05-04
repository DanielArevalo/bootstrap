using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Collections;
using System.Threading;
using Xpinn.Util.PaymentACH;
using xpinnWSIntegracion;
using System.Configuration;
using System.IO;

public partial class Pagos : GlobalWeb
{
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient service = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    xpinnWSDeposito.WSDepositoSoapClient BODeposito = new xpinnWSDeposito.WSDepositoSoapClient();
    xpinnWSPayment.WSPaymentSoapClient PaymentService = new xpinnWSPayment.WSPaymentSoapClient();
    xpinnWSIntegracion.WSintegracionSoapClient wsIntegra = new xpinnWSIntegracion.WSintegracionSoapClient();
    xpinnWSLogin.Persona1 pPersona;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ValidarSession();
            VisualizarTitulo(OptionsUrl.EstadoCuenta, "D");
            Site toolBar = (Site)Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoContinuar += btnContinuar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarContinuar(false);
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("EstadoDeCuenta", "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            string final = "";
            try
            { final = Request["pagos"].ToString(); }
            catch { }

            if (!string.IsNullOrEmpty(final) && final == "1")
            {
                panelFinal.Visible = true;
                panelRealizaPago.Visible = false;
                panelConfirmación.Visible = false;
            }
            else
            {
                pnlInfo.Visible = false;
                lblFechaTran.Text = DateTime.Now.ToString(gFormatoFecha);
                ViewState["tablaSesion"] = null;
                CargarDropDown();
                CrearTablaTran();
                LimpiarProcesoPago();
                ObtenerDatos();
            }
        }
    }

    private void ObtenerDatos()
    {
        try
        {
            if (Session[PersonaLogin.cod_persona + "Identificacion"] != null)
                lblIdentiEmer.Text = Session[PersonaLogin.cod_persona + "Identificacion"].ToString();
            if (Session[PersonaLogin.cod_persona + "Nombre"] != null)
                lblNombreEmer.Text = Session[PersonaLogin.cod_persona + "Nombre"].ToString();
            if (Session[PersonaLogin.cod_persona + "NroProducto"] != null)
                lblNroProducto.Text = Session[PersonaLogin.cod_persona + "NroProducto"].ToString();
            if (Session[PersonaLogin.cod_persona + "VrPago"] != null)
            {
                lblValorEmer.Text = Convert.ToDecimal(Session[PersonaLogin.cod_persona + "VrPago"]).ToString("n0");
                lblVrPago.Text = Session[PersonaLogin.cod_persona + "VrPago"].ToString();
            }
            if (Session[PersonaLogin.cod_persona + "TipoProducto"] != null)
                lbltipoProducto.Text = Session[PersonaLogin.cod_persona + "TipoProducto"].ToString();
            if (Session[PersonaLogin.cod_persona + "CodProducto"] != null)
                lblCodTipoProducto.Text = Session[PersonaLogin.cod_persona + "CodProducto"].ToString();
            Session.Remove(PersonaLogin.cod_persona + "Identificacion");
            Session.Remove(PersonaLogin.cod_persona + "Nombre");
            Session.Remove(PersonaLogin.cod_persona + "NroProducto");
            Session.Remove(PersonaLogin.cod_persona + "VrPago");
            Session.Remove(PersonaLogin.cod_persona + "TipoProducto");
            Session.Remove(PersonaLogin.cod_persona + "CodProducto");
            if (!string.IsNullOrWhiteSpace(lblCodTipoProducto.Text))
                LlenarComboTipoPago(Convert.ToInt32(lblCodTipoProducto.Text));

            xpinnWSIntegracion.Integracion pse = wsIntegra.consultarConvenioIntegracion(Convenios_Integracion.pse, Session["sec"].ToString());
            string pFilter = "";

            string tele = ConfigurationManager.AppSettings["TeleFooter"] != null ? ConfigurationManager.AppSettings["TeleFooter"].ToString() : " - ";
            string email = ConfigurationManager.AppSettings["EmailFooter"] != null ? ConfigurationManager.AppSettings["EmailFooter"].ToString() : " - ";
            string tipoPSE = ConfigurationManager.AppSettings["tipoPSE"] != null ? ConfigurationManager.AppSettings["tipoPSE"].ToString() : " - ";

            if (pse != null && !string.IsNullOrEmpty(pse.entidad))
            {
                if (tipoPSE == "1")
                {
                    // CONSULT LAST PAYMENT ACH
                    pFilter = " WHERE TYPE_PRODUCT = " + lblCodTipoProducto.Text;
                    pFilter += " AND NUMBER_PRODUCT = " + lblNroProducto.Text;
                    pFilter += " AND ID_PAYMENT = (SELECT MAX(X.id_payment) FROM payment_Ach x where x.type_product = " + lblCodTipoProducto.Text + " AND x.number_product = " + lblNroProducto.Text + ")";
                    xpinnWSPayment.PaymentACH paymentACH = PaymentService.ConsultPaymentTransaction(pFilter, Session["sec"].ToString());
                    if (paymentACH != null)
                    {
                        VerError("ID_TICKETOFFICE:" + AppConstants.ID_TICKETOFFICE + " PSE_URL:" + AppConstants.PSE_URL + " USE_WS_SECURITY:" + AppConstants.USE_WS_SECURITY);
                        // CONSULT PAYMENT
                        InterfazPaymentPSE InterfazServices = new InterfazPaymentPSE();
                        PSEHostingTransactionInformationReturn ret = InterfazServices.VerifyPayment(AppConstants.ID_TICKETOFFICE, AppConstants.PASSWORD, paymentACH.ID.ToString());

                        if (ret.ReturnCode == PSEHostingTransactionInformationReturnCode.OK)
                        {
                            if (ret.State == PSEHostingTransactionState.PENDING)
                            {
                                lblContent.Text = string.Format("{0} {1} {2}", "En este momento su obligación # ", lblNroProducto.Text,
                                    " presenta un proceso de pago cuya transacción se encuentra PENDIENTE de recibir confirmación por parte de la entidad financiera, por favor espere unos minutos y vuelva a consultar más tarde para verificar si su pago fue confirmado de forma exitosa");
                                pnlInfo.Visible = true;
                                btnPse.Enabled = false;
                            }
                            else
                            {
                                // VALIDAR MI TRANSACCION EN LA BD DE FINANCIAL
                                if (paymentACH.Status == xpinnWSPayment.PaymentStatusEnum.created || paymentACH.Status == xpinnWSPayment.PaymentStatusEnum.pending)
                                {
                                    string stateMsg = "PENDIENTE";
                                    switch (paymentACH.Status)
                                    {
                                        case xpinnWSPayment.PaymentStatusEnum.created:
                                            stateMsg = "PAGO NO EMPEZADO";
                                            break;
                                        case xpinnWSPayment.PaymentStatusEnum.pending:
                                            stateMsg = "PENDIENTE";
                                            break;
                                    }

                                    lblContent.Text = string.Format("{0} {1} {2} {3} {4}", "En este momento su obligación # ", lblNroProducto.Text,
                                            " presenta un proceso de pago cuya transacción se encuentra como ", stateMsg,
                                            " de recibir confirmación por parte de la entidad financiera, por favor espere unos minutos y vuelva a consultar más tarde para verificar si su pago fue confirmado de forma exitosa");
                                    pnlInfo.Visible = true;
                                    btnPse.Enabled = false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    // CONSULT LAST PAYMENT ACH
                    pFilter = " WHERE TYPE_PRODUCT = " + lblCodTipoProducto.Text;
                    pFilter += " AND NUMBER_PRODUCT = " + lblNroProducto.Text;
                    pFilter += " AND ID_PAYMENT = (SELECT MAX(X.id_payment) FROM payment_Ach x where x.type_product = " + lblCodTipoProducto.Text + " AND x.number_product = " + lblNroProducto.Text + ")";

                    xpinnWSIntegracion.ACHPayment payment = wsIntegra.ConsultPaymentTransaction(pFilter, Session["sec"].ToString());
                    if (payment != null)
                    {
                        // CONSULT PAYMENT
                        if (payment.ID > 0 && (payment.Status == xpinnWSIntegracion.PaymentStatusEnum.created || payment.Status == xpinnWSIntegracion.PaymentStatusEnum.failed || payment.Status == xpinnWSIntegracion.PaymentStatusEnum.pending))
                        {
                            lblContent.Text = string.Format("{0} {1} {2}", "En este momento su obligación # ", lblNroProducto.Text,
                                    " presenta un proceso de pago cuya transacción se encuentra PENDIENTE de recibir confirmación por parte de su entidad financiera, por favor espere unos minutos y vuelva a consultar más tarde para verificar si su pago fue confirmado de forma exitosa. Si desea mayor información sobre el estado actual de su operación puede comunicarse a nuestras líneas de atención al cliente " + tele + " o enviar un correo electronico a " + email + " y preguntar por el estado de la transacción: " + payment.TrazabilityCode);
                            pnlInfo.Visible = true;
                            btnPse.Enabled = false;
                        }
                    }
                    //muestra boton pse
                    btnPse.Visible = true;
                }
            }
            else
            {
                btnPse.Visible = false;
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void CargarDropDown()
    {
        List<xpinnWSEstadoCuenta.ListaDesplegable> lstLineas = service.PoblarListaDesplegable("TIPOMONEDA", "", "", "2", Session["sec"].ToString());
        LlenarDrop(ddlMoneda, lstLineas);
        ddlMoneda.SelectedValue = "1";
    }

    void LlenarDrop(DropDownList ddlDropCarga, List<xpinnWSEstadoCuenta.ListaDesplegable> listaData)
    {
        ddlDropCarga.DataSource = listaData;
        ddlDropCarga.DataTextField = "descripcion";
        ddlDropCarga.DataValueField = "idconsecutivo";
        ddlDropCarga.AppendDataBoundItems = true;
        ddlDropCarga.Items.Insert(0, new ListItem("Seleccione un item", ""));
        ddlDropCarga.DataBind();
    }

    private void LlenarComboTipoPago(Int64 ptipo_producto)
    {
        ddlTipoPago.Items.Clear();
        try
        {
            xpinnWSDeposito.TipoOperacion tipOpe = new xpinnWSDeposito.TipoOperacion();
            tipOpe.tipo_producto = ptipo_producto;
            ddlTipoPago.DataSource = BODeposito.ListarTipoOpeTransacVent(tipOpe, PersonaLogin.clavesinecriptar, PersonaLogin.identificacion, Session["sec"].ToString());
            ddlTipoPago.DataTextField = "nombre";
            ddlTipoPago.DataValueField = "cod_operacion";
            ddlTipoPago.DataBind();
            ddlTipoPago.SelectedIndex = 1;
        }
        catch
        {
        }
    }

    protected void CrearTablaTran()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("tipo_producto");    // codigo de tipo de producto
        dt.Columns.Add("nom_tproducto");
        dt.Columns.Add("nroRef");       // nùmero del producto
        dt.Columns.Add("valor");
        dt.Columns.Add("cod_moneda");
        dt.Columns.Add("nom_moneda");
        dt.Columns.Add("tipo_tran");         // codigo de tipo de transaccion
        dt.Columns.Add("nom_tipo_tran");
        dt.Columns.Add("tipo_mov");

        gvTransacciones.DataSource = dt;
        gvTransacciones.DataBind();
        ViewState["tablaSesion"] = dt;
    }

    protected void LlenarTablaTran()
    {
        if (ViewState["tablaSesion"] == null)
            return;
        DataTable dtAgre = new DataTable();
        dtAgre = (DataTable)ViewState["tablaSesion"];
        DataRow fila = dtAgre.NewRow();

        //se consulta el tipo movimiento y el tipo de producto que esta relacionado al tipo de transaccion
        xpinnWSDeposito.TipoOperacion tipOpe = new xpinnWSDeposito.TipoOperacion();

        // Determina el dato del tipo de producto
        tipOpe.cod_operacion = ddlTipoPago.SelectedValue;
        tipOpe = BODeposito.ConsultarTipOpeTranCaja(tipOpe, PersonaLogin.clavesinecriptar, PersonaLogin.identificacion, Session["sec"].ToString());

        string num_producto = string.Empty, tipo_producto = string.Empty;
        Boolean control = false;

        foreach (GridViewRow posRow in gvTransacciones.Rows)
        {
            num_producto = gvTransacciones.DataKeys[posRow.RowIndex].Values[0].ToString();
            tipo_producto = gvTransacciones.DataKeys[posRow.RowIndex].Values[1].ToString();
            if (num_producto == lblNroProducto.Text.Trim() && tipo_producto == lblCodTipoProducto.Text.Trim())
                control = true;
        }

        //Capturando el valor de la transacción
        int rowIndex = gvCtaAhorros.Rows.OfType<GridViewRow>().Where(x => ((CheckBoxGrid)x.FindControl("chkSeleccion")).Checked)
                      .Select(y => y.RowIndex).FirstOrDefault();

        decimales txtVal = (decimales)gvCtaAhorros.Rows[rowIndex].FindControl("txtValorAplicar");
        string pVal = txtVal.Text.Replace(".", "");
        decimal pValorTran = Convert.ToDecimal(pVal);

        if (control == true)
        {
            VerError("Ya se cargo una Transacción a ese Número de Producto");
        }
        else
        {
            // LLena los datos de la fila 
            fila[0] = lblCodTipoProducto.Text.Trim();
            fila[1] = lbltipoProducto.Text;
            fila[2] = lblNroProducto.Text.Trim() == "" ? "0" : lblNroProducto.Text.Trim();
            fila[3] = pValorTran.ToString();
            fila[4] = ddlMoneda.SelectedValue;             // Colocar el tipo de moneda de la transacciòn
            fila[5] = ddlMoneda.SelectedItem.Text;
            fila[6] = ddlTipoPago.SelectedValue;           // Colocar el tipo de transaccion
            fila[7] = ddlTipoPago.SelectedItem.Text;
            fila[8] = tipOpe.tipo_movimiento;

            // Adiciona la fila a la tabla
            dtAgre.Rows.Add(fila);
            gvTransacciones.DataSource = dtAgre;
            gvTransacciones.DataBind();
            ViewState["tablaSesion"] = dtAgre;
        }
    }

    protected void btnCtaAhorro_Click(object sender, ImageClickEventArgs e)
    {
        CargarCuentas();
    }

    private void CargarCuentas()
    {
        List<xpinnWSDeposito.AhorroVista> lstCuentas = new List<xpinnWSDeposito.AhorroVista>();
        string pFiltro = obtFiltro();
        lstCuentas = BODeposito.ListarCuentasAplicarPagos(pFiltro, PersonaLogin.clavesinecriptar, PersonaLogin.identificacion, Session["sec"].ToString());

        panelCuentas.Visible = false;
        if (lstCuentas.Count > 0)
        {
            gvCtaAhorros.DataSource = lstCuentas;
            panelCuentas.Visible = true;
            Site toolBar = (Site)Master;
            toolBar.MostrarContinuar(true);
        }
        else
        {
            gvCtaAhorros.DataSource = null;
            VerError("No tiene cuentas válidas de las cuales hacer el pago.");
        }
        gvCtaAhorros.DataBind();
    }

    private string obtFiltro()
    {
        string pFiltro = string.Empty;
        pFiltro += " AND P.IDENTIFICACION='" + PersonaLogin.identificacion + "'";
        pFiltro += " AND A.SALDO_TOTAL > 0 ";

        if (!string.IsNullOrEmpty(pFiltro))
        {
            pFiltro = pFiltro.Substring(4);
            pFiltro = " WHERE " + pFiltro;
        }
        return pFiltro;
    }

    protected void gvCtaAhorros_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBoxGrid chkSeleccion = (CheckBoxGrid)e.Row.FindControl("chkSeleccion");
            if (chkSeleccion != null)
            {
                decimales txtValorAplicar = (decimales)e.Row.FindControl("txtValorAplicar");
                txtValorAplicar.Enabled = false;
                if (chkSeleccion.Checked)
                {
                    txtValorAplicar.Text = "";
                    txtValorAplicar.Enabled = true;
                }
            }
        }
    }

    protected void chkSeleccion_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxGrid chkSeleccion = (CheckBoxGrid)sender;
        int rowIndex = Convert.ToInt32(chkSeleccion.CommandArgument);
        if (chkSeleccion != null)
        {
            foreach (GridViewRow rFila in gvCtaAhorros.Rows)
            {
                decimales txtValorAplicar = (decimales)rFila.FindControl("txtValorAplicar");
                if (txtValorAplicar != null)
                {
                    if (rFila.RowIndex == rowIndex)
                    {
                        txtValorAplicar.Enabled = false;
                        txtValorAplicar.Text = "";
                        if (chkSeleccion.Checked)
                            txtValorAplicar.Enabled = true;
                    }
                }
            }
        }
    }

    protected void txtValorAplicar_TextChanged(object sender, EventArgs e)
    {
        lblMensaje.Text = "";
        TextBox txtValorAplicar = (TextBox)sender;
        if (!string.IsNullOrWhiteSpace(txtValorAplicar.Text))
        {
            decimal valAplica = Convert.ToDecimal(txtValorAplicar.Text.Replace(".", ""));
            GridViewRow row = ((GridViewRow)txtValorAplicar.NamingContainer.NamingContainer);
            int rowindex = row.RowIndex;
            decimal saldo = Convert.ToDecimal(gvCtaAhorros.DataKeys[rowindex].Values[1].ToString());
            if (valAplica > saldo)
            {
                lblMensaje.Text = "El valor ingresado no puede ser aplicado, se modificó al saldo actual al que tiene.";
                txtValorAplicar.Text = saldo.ToString();
                RegistrarPostBack();
            }
        }
    }


    private void LimpiarProcesoPago()
    {
        panelConfirmación.Visible = false;
        panelRealizaPago.Visible = true;
        Site toolBar = (Site)Master;
        toolBar.MostrarImprimir(false);
        toolBar.MostrarRegresar(true);
        toolBar.MostrarGuardar(false);
        gvCtaAhorros.DataSource = null;
        panelCuentas.Visible = false;
    }



    private Boolean ValidarTransaccion()
    {
        if (string.IsNullOrWhiteSpace(lblValorEmer.Text) || lblValorEmer.Text.Trim() == "0")
        {
            if (Convert.ToDecimal(lblValorEmer.Text) == 0)
            {
                VerError("El valor del producto a pagar es invalido");
                return false;
            }
        }
        if (panelCuentas.Visible == false)
        {
            VerError("No se encontraron cuentas de ahorros para realizar el pago, comuníquese con nosotros para resolver este inconveniente");
            return false;
        }

        Boolean Select = gvCtaAhorros.Rows.OfType<GridViewRow>().Where(x => ((CheckBoxGrid)x.FindControl("chkSeleccion")).Checked).Any();
        if (!Select)
        {
            VerError("Seleccione una cuenta de ahorro");
            return false;
        }
        //Capturando el Indice del ChechBox seleccionado
        int rowIndex = gvCtaAhorros.Rows.OfType<GridViewRow>().Where(x => ((CheckBoxGrid)x.FindControl("chkSeleccion")).Checked)
                       .Select(y => y.RowIndex).FirstOrDefault();
        string pVal = ((decimales)gvCtaAhorros.Rows[rowIndex].FindControl("txtValorAplicar")).Text;
        decimal pValor = Convert.ToDecimal(pVal.Replace(".", ""));
        if (pValor == 0)
        {
            VerError("Ingrese el valor del pago a realizar");
            return false;
        }
        if (ddlMoneda.SelectedItem == null)
        {
            VerError("No existen tipos de monedas, para realizar el pago, por favor reporte este inconveniente para solucionarlo");
            return false;
        }
        if (ddlMoneda.SelectedIndex == 0)
        {
            VerError("Seleccione un tipo de moneda");
            return false;
        }
        if (ddlTipoPago.SelectedItem == null)
        {
            VerError("No existen tipos de pagos, por favor infórmenos sobre este inconveniente para solucionarlo");
            return false;
        }
        return true;
    }



    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (!ValidarTransaccion())
            return;
        //Creando Tabla de Transaccion
        LlenarTablaTran();
        panelRealizaPago.Visible = false;
        panelConfirmación.Visible = true;
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarContinuar(false);
    }


    protected Boolean ValidarDatos()
    {
        if (gvTransacciones.Rows.Count == 0)
        {
            VerError("No existen transacciones por realizar, verifique los datos o inténtelo nuevamente");
            return false;
        }
        foreach (GridViewRow rFila in gvTransacciones.Rows)
        {
            if (rFila.Cells[2].Text == "&nbsp;" || rFila.Cells[2].Text == "")
            {
                VerError("Error en la transacción Nro : " + (rFila.RowIndex + 1) + " , Número de referencia inválido.");
                return false;
            }
            if (rFila.Cells[3].Text == "&nbsp;" || rFila.Cells[3].Text == "" || rFila.Cells[3].Text == "0")
            {
                VerError("Error en la transacción Nro : " + (rFila.RowIndex + 1) + " , Valor a pagar inválido.");
                return false;
            }
            if (rFila.Cells[7].Text == "&nbsp;" || rFila.Cells[7].Text == "" || rFila.Cells[7].Text == "0")
            {
                VerError("Error en la transacción Nro : " + (rFila.RowIndex + 1) + " , Tipo de pago inválido.");
                return false;
            }
        }
        // Validar que exista la parametrización contable por procesos
        if (ValidarProcesoContable(Convert.ToDateTime(lblFechaTran.Text), 125) == false)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación " + 125 + " = Cruce de ahorros con productos, Infórmenos de este inconveniente");
            return false;
        }

        return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            if (ValidarDatos())
                ctlMensaje.MostrarMensaje("¿Esta seguro de realizar el pago?");
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    private void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            //CAPTURA DE DATOS
            xpinnWSDeposito.Solicitud_cruce_ahorro pSolicitud = new xpinnWSDeposito.Solicitud_cruce_ahorro();
            List<xpinnWSDeposito.Solicitud_cruce_ahorro> lstSolicitud = new List<xpinnWSDeposito.Solicitud_cruce_ahorro>();
            pSolicitud.idcruceahorro = 0;
            Boolean rpta = false;
            rpta = ObtenerListaCuenta(ref pSolicitud);
            if (rpta == false)
            {
                VerError("Se generó un error al realizar la captura de datos.");
                return;
            }
            rpta = false;
            rpta = ObtenerListaTransaccion(ref pSolicitud);
            if (rpta == false)
            {
                VerError("Se generó un error al realizar la captura de datos.");
                return;
            }
            if (pSolicitud.numero_cuenta != null && pSolicitud.valor_pago > 0)
                lstSolicitud.Add(pSolicitud);

            xpinnWSDeposito.RespuestaApp pRespuesta = new xpinnWSDeposito.RespuestaApp();
            pRespuesta = BODeposito.Aplicar_Solicitud_Pago(lstSolicitud, PersonaLogin.identificacion, PersonaLogin.clavesinecriptar, Session["sec"].ToString());
            if (pRespuesta != null)
            {
                if (pRespuesta.rpta == false)
                {
                    if (pRespuesta.Mensaje != null)
                        VerError(pRespuesta.Mensaje.ToString());
                    else
                        VerError("Se generó un problema al realizar la transacción");
                }
                else
                {
                    panelConfirmación.Visible = false;
                    panelFinal.Visible = true;
                    Site toolbar = (Site)Master;
                    toolbar.MostrarGuardar(false);
                    toolbar.MostrarRegresar(false);
                }
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    private Boolean ObtenerListaCuenta(ref xpinnWSDeposito.Solicitud_cruce_ahorro pSolicitud)
    {
        try
        {
            foreach (GridViewRow rFila in gvCtaAhorros.Rows)
            {
                CheckBoxGrid chkSeleccion = (CheckBoxGrid)rFila.FindControl("chkSeleccion");
                if (chkSeleccion != null)
                {
                    if (chkSeleccion.Checked)
                    {
                        decimales pValor = (decimales)rFila.FindControl("txtValorAplicar");
                        pSolicitud.valor_pago = Convert.ToDecimal(pValor.Text.Replace(".", ""));
                        string pNumero_Cuenta = rFila.Cells[1].Text;
                        pSolicitud.numero_cuenta = pNumero_Cuenta;
                    }
                }
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private Boolean ObtenerListaTransaccion(ref xpinnWSDeposito.Solicitud_cruce_ahorro pSolicitud)
    {
        try
        {
            foreach (GridViewRow rFila in gvTransacciones.Rows)
            {
                pSolicitud.tipo_producto = Convert.ToInt32(gvTransacciones.DataKeys[rFila.RowIndex].Values[0].ToString());
                pSolicitud.num_producto = Convert.ToString(rFila.Cells[2].Text);
                pSolicitud.cod_persona = PersonaLogin.cod_persona;
                //cod_ope capa business
                pSolicitud.fecha_pago = DateTime.ParseExact(lblFechaTran.Text, gFormatoFecha, null);
                pSolicitud.valor_pago = Convert.ToDecimal(gvTransacciones.DataKeys[rFila.RowIndex].Values[4].ToString());
                pSolicitud.tipo_tran = Convert.ToInt64(gvTransacciones.DataKeys[rFila.RowIndex].Values[3].ToString());
                pSolicitud.codusuario = 1;
                pSolicitud.concepto = "Atencion al Cliente";
                pSolicitud.estado = 1;
                pSolicitud.ip = PersonaLogin.ipPersona;
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Detalle);
    }

    protected void btnInicio_Click(object sender, EventArgs e)
    {
        Navegar("~/Index.aspx");
    }


    #region METHODS PSE
    static Dictionary<TipoDeProducto, string> _PaymentDescription = new Dictionary<TipoDeProducto, string>()
        {
            { TipoDeProducto.Aporte, "APLICACIÓN DE PAGO - APORTES" },
            { TipoDeProducto.Credito, "APLICACIÓN DE PAGO - CRÉDITO" },
            { TipoDeProducto.AhorrosVista, "APLICACIÓN DE PAGO - AHORRO VISTA" },
            { TipoDeProducto.Servicios, "APLICACIÓN DE PAGO - SERVICIOS" },
            { TipoDeProducto.CDATS, "APLICACIÓN DE PAGO - CDAT" },
            { TipoDeProducto.AhorroProgramado, "APLICACIÓN DE PAGO - AHORRO PROGRAMADO" }
        };

    protected void btnPse_Click(object sender, ImageClickEventArgs e)

    {
        VerError("");
        try
        {
            ViewState[PersonaLogin.cod_persona + "IP"] = GetUserIP();
            IniciarProcesoPago();
        }
        catch (Exception ex)
        {
            VerError("Ocurrio un error desconocido. Mensaje: " + ex.Message);
        }
    }       
    #endregion


    #region metodos nueva versión PSE
    public void IniciarProcesoPago()
    {
        ViewState["sError"] = null;
        string url = "";
        string bancos = "";
        if (ConfigurationManager.AppSettings["URLProyecto"] != null)
        {
            url = ConfigurationManager.AppSettings["URLProyecto"].ToString();
            bancos = url + "/Pages/Asociado/EstadoCuenta/DetallePago.aspx";
        }
        try
        {
            TipoDeProducto pTipoProduct = lblCodTipoProducto.Text.ToEnum<TipoDeProducto>();
            pPersona = (xpinnWSLogin.Persona1)Session["persona"];
            xpinnWSIntegracion.ACHPayment pago = new xpinnWSIntegracion.ACHPayment()
            {
                Cod_persona = pPersona.cod_persona,
                Cod_ope = 0,
                Type = 0,
                Amount = Convert.ToDecimal(lblVrPago.Text),
                VATAmount = 0,
                PaymentDescription = _PaymentDescription[pTipoProduct],
                ReferenceNumber1 = ViewState[PersonaLogin.cod_persona + "IP"].ToString(),
                ReferenceNumber2 = PersonaLogin.nomtipo_identificacion,
                ReferenceNumber3 = PersonaLogin.identificacion,
                Email = pPersona.email,
                TypeProduct = (int)pTipoProduct,
                NumberProduct = lblNroProducto.Text,
                Fields = PersonaLogin.identificacion.ToString() + "," + PersonaLogin.cod_persona + "," + PersonaLogin.nombre,
                Entity_url = bancos
            };

            pago = wsIntegra.CreatePaymentTransaction(pago, Session["sec"].ToString());
            if(pago != null)
            {
                if (string.IsNullOrEmpty(pago.ErrorMessage))
                {
                    Session.Remove(PersonaLogin.cod_persona + "PaymentTransac");
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    // Abrir nueva ventana de bancos
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    panelFinal.Visible = true;
                    panelRealizaPago.Visible = false;
                    panelConfirmación.Visible = false;                    
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "var bancos = window.open('" + url + "/Pages/Asociado/EstadoCuenta/AplicarPagos.aspx?pagos=1','bancos');", true);
                    // --- AMBIENTE DE PRUEBAS    
                    //Response.Redirect("https://200.1.124.62/PSEHostingUI/GetBankListWS.aspx?enc=" + pago.Identifier);
                    // --- AMBIENTE DE PRODUCCION
                    Response.Redirect("https://www.psepagos.co/PSEHostingUI/GetBankListWS.aspx?enc=" + pago.Identifier);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "setTimeout('resultado(banco)', 1000);", true);
                }
                else
                {
                    VerError("Se presentó un problema ACH: " + pago.ErrorMessage);
                }
            }
            else
            {
                VerError("Se presentó un problema, intente recargar la página");
            }            

        }
        catch (Exception ex)
        {
            ViewState["sError"] = "Ocurrio un error desconocido. Mensaje: " + ex.Message;
        }
    }
    #endregion

}