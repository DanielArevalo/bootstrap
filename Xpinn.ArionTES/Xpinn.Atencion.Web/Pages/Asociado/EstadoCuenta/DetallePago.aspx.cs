using System;
using System.Web.UI;
using Xpinn.Util.PaymentACH;

public partial class Pagos : GlobalWeb
{
    xpinnWSIntegracion.WSintegracionSoapClient wsIntegra = new xpinnWSIntegracion.WSintegracionSoapClient();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ValidarSession();
            VisualizarTitulo(OptionsUrl.EstadoCuenta, "Inf");
            Site toolBar = (Site)Master;
            toolBar.eventoRegresar += btnRegresar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("DetallePago", "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            pnlInfo.Visible = false;
            string pID = Request["ID"].ToString();
            ObtenerDatos(pID);
        }
    }

    private void ObtenerDatos(string pID)
    {
        // CONSULTAR PAYMENT ACH
        string pFilter = " WHERE ID_PAYMENT = " + pID;
        xpinnWSIntegracion.ACHPayment payment = wsIntegra.ConsultPaymentTransaction(pFilter, Session["sec"].ToString());

        // CONSULTA DE PAGO
        xpinnWSPayment.PaymentStatusEnum newState = xpinnWSPayment.PaymentStatusEnum.pending;
        string stateMsg = "PENDIENTE";

        switch (payment.Status)
        {
            case xpinnWSIntegracion.PaymentStatusEnum.created:
                newState = xpinnWSPayment.PaymentStatusEnum.created;
                stateMsg = "PAGO NO EMPEZADO";
                break;
            case xpinnWSIntegracion.PaymentStatusEnum.pending:
                newState = xpinnWSPayment.PaymentStatusEnum.pending;
                stateMsg = "PENDIENTE";
                break;
            case xpinnWSIntegracion.PaymentStatusEnum.approved:
                newState = xpinnWSPayment.PaymentStatusEnum.approved;
                stateMsg = "APROBADA";
                break;
            case xpinnWSIntegracion.PaymentStatusEnum.rejected:
                newState = xpinnWSPayment.PaymentStatusEnum.rejected;
                stateMsg = "RECHAZADA";
                break;
            case xpinnWSIntegracion.PaymentStatusEnum.failed:
                newState = xpinnWSPayment.PaymentStatusEnum.failed;
                stateMsg = "FALLIDA";
                break;
            default:
                stateMsg = "PAGO EN ESTADO DESCONECIDO";
                break;
        }
            if (stateMsg != "APROBADA" && stateMsg != "RECHAZADA")
            {
                pnlInfo.Visible = true;
                lblContent.Text = string.Format("{0} {1} {2}", "En este momento su obligación # ", payment.NumberProduct,
                    " presenta un proceso de pago cuya transacción se encuentra PENDIENTE de recibir confirmación por parte de la entidad financiera, por favor espere unos minutos y vuelva a consultar más tarde para verificar si su pago fue confirmado de forma exitosa");
            }
            
            // CARGANDO VARIABLES
            lblNomEmpresa.Text = PersonaLogin.empresa;
            lblNitEmpresa.Text = PersonaLogin.nit;
            lblIdentificacion.Text = payment.ReferenceNumber3;
            lblNombre.Text = PersonaLogin.nombre;
            lblEmail.Text = payment.Email;
            lblReferenciaPago.Text = payment.ID.ToString();
            lblFechaPago.Text = string.Format("{0} {1}", payment.Fecha_Creacion.ToString(gFormatoFecha), payment.Fecha_Creacion.ToString(gFormatoTime));
            lblDireccionIP.Text = payment.ReferenceNumber1;
            lblVrPago.Text = payment.Amount + payment.VATAmount.ToString("n2");
            lblDescription.Text = payment.PaymentDescription;
            lblBanco.Text = string.Format("{0} - {1}", payment.BankCode, payment.BankName);
            lblEstadoTransac.Text = string.Format("{0}{1}{2}", "Transacción ", stateMsg, " en la Entidad Financiera");
            //lblCodigoCUS.Text = ret.CycleNumber.ToString();     
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Detalle);
    }

}
