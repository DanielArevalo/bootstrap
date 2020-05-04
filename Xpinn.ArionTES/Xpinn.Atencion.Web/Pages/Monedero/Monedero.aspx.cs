using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Monedero : GlobalWeb
{
    xpinnWSIntegracion.WSintegracionSoapClient wsIntegra = new xpinnWSIntegracion.WSintegracionSoapClient();
    xpinnWSIntegracion.Monedero monedero = new xpinnWSIntegracion.Monedero();
    xpinnWSLogin.Persona1 Data = new xpinnWSLogin.Persona1();
    #region metodos iniciales 
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ValidarSession();
            VisualizarTitulo(OptionsUrl.Monedero, "Monedero");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(OptionsUrl.Monedero, "Monedero", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string final = "";
            try
            {final=Request["pagos"].ToString();}catch{}

            if (!string.IsNullOrEmpty(final) && final == "1")
            {
                panelFinal.Visible = true;
                pnlCargar.Visible = false;
            }
            else
            {
                Data = (xpinnWSLogin.Persona1)Session["persona"];
                if (Data.cod_persona != 0 && Data.identificacion != "" && Data.identificacion != null)
                {
                    CargarDatos(Data.cod_persona);
                }
            }

        }
    }
    private void CargarDatos(long cod_persona)
    {
        try
        {                 
            monedero = wsIntegra.consultarMonedero(Convert.ToInt32(cod_persona), Session["sec"].ToString());
            if(monedero != null && monedero.id_monedero > 0)
            {
                txtNombre.Text = Data.nombre;
                txtId.Text = monedero.id_monedero.ToString();
                txtSaldo.Text = monedero.saldo.ToString("C0");
                pnlMonedero.Visible = true;
                pnlApertura.Visible = false;
                cargarOperaciones();

                xpinnWSIntegracion.Integracion pse = wsIntegra.consultarConvenioIntegracion(Convenios_Integracion.pse, Session["sec"].ToString());

                if (pse != null && !string.IsNullOrEmpty(pse.entidad))
                {
                    // CONSULT LAST PAYMENT ACH
                    string pFilter = " WHERE TYPE_PRODUCT = 11";
                    pFilter += " AND NUMBER_PRODUCT = " + txtId.Text;
                    pFilter += " AND ID_PAYMENT = (SELECT MAX(X.id_payment) FROM payment_Ach x where x.type_product = 11 AND x.number_product = " + txtId.Text + ")";

                    //xpinnWSPayment.PaymentACH payment = PaymentService.ConsultPaymentTransaction(pFilter, Session["sec"].ToString());
                    xpinnWSIntegracion.ACHPayment payment = wsIntegra.ConsultPaymentTransaction(pFilter, Session["sec"].ToString());
                    if (payment != null)
                    {
                        // CONSULT PAYMENT
                        if (payment.ID > 0 && (payment.Status == xpinnWSIntegracion.PaymentStatusEnum.created || payment.Status == xpinnWSIntegracion.PaymentStatusEnum.failed || payment.Status == xpinnWSIntegracion.PaymentStatusEnum.pending))
                        {
                            lblContent.Text = "En este momento su monedero presenta un proceso de pago cuya transacción se encuentra PENDIENTE de recibir confirmación por parte de la entidad financiera, por favor espere unos minutos y vuelva a consultar más tarde para verificar si su pago fue confirmado de forma exitosa";
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
                pnlApertura.Visible = true;
                pnlMonedero.Visible = false;
            }
        }
        catch(Exception ex)
        {
            lblError.Text = "Parece que algo salió mal, intentalo más tarde.";
        }

    }
    #endregion

    #region metodos monedero
    private void cargarOperaciones()
    {
        List<xpinnWSIntegracion.Operaciones> lstOpe = new List<xpinnWSIntegracion.Operaciones>();
        lstOpe = wsIntegra.consultarOperaciones(Session["sec"].ToString());
        if (lstOpe != null && lstOpe.Count > 0)
        {
            //Activa las opciones activas
            foreach (var item in lstOpe)
            {
                switch (item.id_operacion)
                {
                    case 1://Recargas
                        card_recargas.Visible = true;
                        break;
                    case 2://Transferencias
                        card_transferencias.Visible = true;
                        break;
                    default:
                        break;
                }
            }
            pnlOperaciones.Visible = true;
        }
        pnlOperaciones.Visible = true;
    }

    protected void btnAbrir_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            Data = (xpinnWSLogin.Persona1)Session["persona"];
            monedero = wsIntegra.crearMonedero(Convert.ToInt32(Data.cod_persona), Session["sec"].ToString());
            if (monedero != null && monedero.id_monedero > 0)
            {
                CargarDatos(Convert.ToInt32(Data.cod_persona));
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "Parece que algo salió mal, intentalo más tarde.";
        }
    }
    #endregion
    

    #region CargarCuenta
    protected void btnRecargar_Click(object sender, EventArgs e)
    {
        try
        {
            Data = (xpinnWSLogin.Persona1)Session["persona"];
            List<xpinnWSIntegracion.ProductoOrigen> lstOrigen = new List<xpinnWSIntegracion.ProductoOrigen>();
            lstOrigen = wsIntegra.consultarProductosOrigen(Data.cod_persona, Session["sec"].ToString());
            xpinnWSIntegracion.Integracion pse = wsIntegra.consultarConvenioIntegracion(Convenios_Integracion.pse, Session["sec"].ToString());
            ddlOrigen.Items.Clear();
            ddlOrigen.Items.Add(new ListItem("Selecciona una opción", ""));
            if (pse != null && !string.IsNullOrEmpty(pse.entidad))
                ddlOrigen.Items.Add(new ListItem("PSE", "PSE"));

            foreach (xpinnWSIntegracion.ProductoOrigen item in lstOrigen)
            {
                ddlOrigen.Items.Add(new ListItem(item.nombre, item.referencia));
            }
            ddlOrigen.DataBind();            
            txtValorCarga.Text = "$0";
            pnlOperaciones.Visible = false;
            pnlMonedero.Visible = false;
            pnlCargar.Visible = true;
            divDisponible.Visible = false;
            divPSE.Visible = false;

        }
        catch (Exception ex)
        {
            lblError.Text = "Parece que algo salió mal, intentalo más tarde.";
        }
    }

    protected void ddlOrigen_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblError.Text = "";
        txtTipoProducto.Text = "";
        txtNumProducto.Text = "";
        txtDisponible.Text = "";

        divPSE.Visible = false;
        divDisponible.Visible = false;
        if (ddlOrigen.SelectedValue != null && ddlOrigen.SelectedValue != "")
        {
            switch (ddlOrigen.SelectedValue)
            {
                case "PSE":
                    divPSE.Visible = true;
                    divbtns.Visible = false;
                    divDisponible.Visible = false;
                    break;
                default:
                    divPSE.Visible = false;
                    divbtns.Visible = true;
                    Data = (xpinnWSLogin.Persona1)Session["persona"];
                    List<xpinnWSIntegracion.ProductoOrigen> lstOrigen = new List<xpinnWSIntegracion.ProductoOrigen>();
                    lstOrigen = wsIntegra.consultarProductosOrigen(Data.cod_persona, Session["sec"].ToString());
                    if (lstOrigen != null && lstOrigen.Count > 0)
                    {
                        lstOrigen = lstOrigen.Where(x => x.referencia == ddlOrigen.SelectedValue).ToList();
                        if (lstOrigen != null && lstOrigen.Count > 0)
                        {
                            xpinnWSIntegracion.ProductoOrigen orig = lstOrigen.ElementAt(0);
                            txtDisponible.Text = orig.saldo.ToString("C0");
                            txtTipoProducto.Text = orig.tipo_producto.ToString();
                            txtNumProducto.Text = orig.referencia;
                            divDisponible.Visible = true;
                        }
                    }
                    break;
            }
        }
    }

    protected void btnCargar_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            if (ddlOrigen.SelectedValue != null && ddlOrigen.SelectedValue != "")
            {
                string valorC = txtValorCarga.Text.Replace(".", "").Replace("$", "").Replace(",","");
                if (string.IsNullOrEmpty(valorC) || valorC == "0")
                {
                    lblError.Text = "Ingresa un valor para la carga";
                    return;
                }
                //Crear transacción desde el producto
                decimal dispon = Convert.ToDecimal(txtDisponible.Text.Replace(".", "").Replace("$", "").Replace(",", ""));
                if(Convert.ToDecimal(valorC) <= dispon)
                {
                    Data = (xpinnWSLogin.Persona1)Session["persona"];
                    xpinnWSIntegracion.TranMonedero tran = new xpinnWSIntegracion.TranMonedero()
                    {
                        num_tran = 0,
                        cod_persona =  Data.cod_persona,
                        id_monedero = Convert.ToInt32(txtId.Text),
                        tipo_tran = 1,
                        valor = Convert.ToDecimal(valorC),
                        estado = 1,
                        fecha = DateTime.Now,
                        descripcion = ddlOrigen.SelectedItem.Text,
                        tipo_credito = 1,
                        cod_tipo_producto = Convert.ToInt32(txtTipoProducto.Text),
                        referencia = txtNumProducto.Text
                    };
                    tran = wsIntegra.guardarTransaccionMonedero(tran, Session["sec"].ToString());
                    if(tran != null)
                    {
                        panelFinal.Visible = true;
                        pnlCargar.Visible = false;
                        txtDisponible.Text = "";
                        txtValorCarga.Text = "$0";
                        txtNumProducto.Text = "";
                        txtTipoProducto.Text = "";
                    }
                    else
                    {
                        lblError.Text = "Parece que algo salió mal, intentalo más tarde.";
                    }
                }
                else
                {
                    lblError.Text = "El valor ingresado supera tu saldo disponible";
                    return;
                }
            }
            else
            {
                lblError.Text = "Selecciona un producto de orígen";
                return;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "Parece que algo salió mal, intentalo más tarde.";
        }
    }

    protected void btnVolverMon_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        pnlOperaciones.Visible = true;
        pnlMonedero.Visible = true;
        pnlCargar.Visible = false;
        txtDisponible.Text = "";
        txtValorCarga.Text = "";
        pnlHistorico.Visible = false;
    }

    protected void btnInicio_Click(object sender, EventArgs e)
    {
        Navegar("~/Pages/Monedero/Monedero.aspx");
    }

    protected void btnPse_Click(object sender, ImageClickEventArgs e)
    {
        lblError.Text = "";
        try
        {
            string valor = txtValorCarga.Text.Replace("$", "").Replace(",", "").Replace(".", "");
            if (string.IsNullOrEmpty(valor) || valor == "0")
            {
                lblError.Text = "Ingresa un valor para la carga";
                return;
            }
            else
            {
                IniciarProcesoPago();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "Ocurrio un error desconocido. Mensaje: " + ex.Message;
        }
    }

    public void IniciarProcesoPago()
    {
        try
        {            
            string url = "";
            string bancos = "";
            if (ConfigurationManager.AppSettings["URLProyecto"] != null)
            {
                url = ConfigurationManager.AppSettings["URLProyecto"].ToString();
                bancos = url + "/Pages/Asociado/EstadoCuenta/DetallePago.aspx";
            }
            try
            {
                Data = (xpinnWSLogin.Persona1)Session["persona"];
                xpinnWSIntegracion.ACHPayment pago = new xpinnWSIntegracion.ACHPayment()
                {
                    Cod_persona = Data.cod_persona,
                    Cod_ope = 0,
                    Type = 0,
                    Amount = Convert.ToDecimal(txtValorCarga.Text.Replace("$", "").Replace(",", "").Replace(".", "")),
                    VATAmount = 0,
                    PaymentDescription = "Carga monedero Virtual",
                    ReferenceNumber1 = Data.ipPersona,
                    ReferenceNumber2 = Data.nomtipo_identificacion,
                    ReferenceNumber3 = Data.identificacion,
                    Email = Data.email,
                    TypeProduct = 11, // PRODUCTO 11: MONEDERO
                    NumberProduct = txtId.Text,
                    Fields = Data.identificacion.ToString() + "," + Data.cod_persona + "," + Data.nombre,
                    Entity_url = bancos
                };

                pago = wsIntegra.CreatePaymentTransaction(pago, Session["sec"].ToString());
                if (pago != null)
                {
                    if (string.IsNullOrEmpty(pago.ErrorMessage))
                    {
                        Session.Remove(PersonaLogin.cod_persona + "PaymentTransac");
                        panelFinal.Visible = true;
                        pnlCargar.Visible = false;
                        //Abrir nueva ventana de bancos
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "var bancos = window.open('" + url + "/Pages/Asociado/EstadoCuenta/AplicarPagos.aspx?pagos=1','bancos');", true);
                        Response.Redirect("https://200.1.124.62/PSEHostingUI/GetBankListWS.aspx?enc=" + pago.Identifier);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "setTimeout('resultado(banco)', 1000);", true);
                    }
                    else
                    {
                        lblError.Text = "Se presentó un problema: " + pago.ErrorMessage;
                    }
                }
                else
                {
                    lblError.Text = "Se presentó un problema, intente recargar la página";
                }

            }
            catch (Exception ex)
            {
                ViewState["sError"] = "Ocurrio un error desconocido. Mensaje: " + ex.Message;
            }

        }
        catch (Exception ex)
        {
            lblError.Text = "Parece que algo salió mal, intentalo más tarde. Mensaje: " + ex.Message;
        }
        
    }

    #endregion

    #region metodos operaciones
    protected void btnRecarga_Click(object sender, EventArgs e)
    {
        if (validarSaldo())
        {
            Response.Redirect("~/Pages/Monedero/Producto/Recarga.aspx");
        }
    }

    protected void btnTransferencia_Click(object sender, EventArgs e)
    {
        if (validarSaldo())
        {
            Response.Redirect("~/Pages/Monedero/Producto/Transferencia.aspx");
        }
    }

    public bool validarSaldo()
    {
        lblError.Text = "";
        string saldo = txtSaldo.Text.Replace(",", "").Replace(".", "").Replace("$", "");
        if (string.IsNullOrEmpty(saldo) || saldo == "0")
        {
            lblError.Text = "No cuentas con saldo disponible para hacer transferencias";
            return false;
        }
        return true;
    }
    #endregion    

    protected void btnHistory_Click(object sender, EventArgs e)
    {
        pnlOperaciones.Visible = false;
        pnlHistorico.Visible = true;
        Data = (xpinnWSLogin.Persona1)Session["persona"];
        List<xpinnWSIntegracion.TranMonedero> lstTran = wsIntegra.listarTranMonederoPersona(Data.cod_persona.ToString(), Session["sec"].ToString());
        if(lstTran != null && lstTran.Count > 0)
        {
            gvHistory.DataSource = lstTran;
            gvHistory.DataBind();
        }
    }
}