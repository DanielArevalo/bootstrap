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
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Obligaciones.Services;
using Xpinn.Obligaciones.Entities;

public partial class Detalle : GlobalWeb
{
    private Xpinn.Obligaciones.Services.SolicitudService SolicitudServicio = new Xpinn.Obligaciones.Services.SolicitudService();
    private Xpinn.Obligaciones.Services.ObligacionCreditoService ObligacionCreditoServicio = new Xpinn.Obligaciones.Services.ObligacionCreditoService();
    private Xpinn.Obligaciones.Entities.ObligacionCredito ObligaCred = new Xpinn.Obligaciones.Entities.ObligacionCredito();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ObligacionCreditoServicio.CodigoPrograma4 + ".id"] != null)
                VisualizarOpciones(ObligacionCreditoServicio.CodigoPrograma4, "E");
            else
                VisualizarOpciones(ObligacionCreditoServicio.CodigoPrograma4, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma4, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarComboEntidades(ddlEntidad);
                LlenarComboPageSize();
            }

            if (Session[ObligacionCreditoServicio.CodigoPrograma4 + ".id"] != null)
            {
                idObjeto = Session[ObligacionCreditoServicio.CodigoPrograma4 + ".id"].ToString();
                Session.Remove(ObligacionCreditoServicio.CodigoPrograma4 + ".id");
                ObtenerDatos(idObjeto);
                Actualizar();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma4, "Page_Load", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session[ObligacionCreditoServicio.CodigoPrograma4 + ".id"] = idObjeto;
        Navegar("../RegistroPago/Lista.aspx");
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Obligaciones.Entities.Solicitud vSolicitud = new Xpinn.Obligaciones.Entities.Solicitud();
            vSolicitud = SolicitudServicio.ConsultarEstadoCuenta(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vSolicitud.codobligacion.ToString()))
                txtNroObligacion.Text = vSolicitud.codobligacion.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.estadoobligacion.ToString()))
                txtEstado.Text = vSolicitud.estadoobligacion.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.codentidad.ToString()))
                ddlEntidad.SelectedValue = vSolicitud.codentidad.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.montoaprobado.ToString()))
                txtMontoApro.Text = vSolicitud.montoaprobado.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.saldocapital.ToString()))
                txtSaldo.Text = vSolicitud.saldocapital.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.fechaproximopago.ToShortDateString()))
                txtFechaProxPago.Text = vSolicitud.fechaproximopago.ToShortDateString();
            if (!string.IsNullOrEmpty(vSolicitud.montoaprobado.ToString()))
                txtMontoApro.Text = vSolicitud.montoaprobado.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.numeropagare.ToString()))
                txtPagare.Text = vSolicitud.numeropagare.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma4, "ObtenerDatos", ex);
        }
    }


    protected void LlenarComboEntidades(DropDownList ddlEntidades)
    {
        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
        ddlEntidades.DataSource = bancoService.ListarBancos(banco, (Usuario)Session["usuario"]);
        ddlEntidades.DataTextField = "nombrebanco";
        ddlEntidades.DataValueField = "cod_banco";
        ddlEntidades.DataBind();

        Xpinn.Caja.Services.TipoPagoService TipoPagoService = new Xpinn.Caja.Services.TipoPagoService();
        Xpinn.Caja.Entities.TipoPago TipoPago = new Xpinn.Caja.Entities.TipoPago();
        ddlFormaPago.DataSource = TipoPagoService.ListarTipoPago(TipoPago, (Usuario)Session["usuario"]);
        ddlFormaPago.DataTextField = "descripcion";
        ddlFormaPago.DataValueField = "cod_tipo_pago";
        ddlFormaPago.DataBind();
        ddlFormaPago.AppendDataBoundItems = true;
        ddlFormaPago.Items.Insert(0, new ListItem("Seleccione un item", "0"));

        ddlEntidad2.DataSource = bancoService.ListarBancosegre((Usuario)Session["usuario"]);
        ddlEntidad2.DataTextField = "nombrebanco";
        ddlEntidad2.DataValueField = "cod_banco";
        ddlEntidad2.AppendDataBoundItems = true;
        ddlEntidad2.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlEntidad2.DataBind();
    }


    protected void LlenarComboPageSize()
    {
        int contador = 1;
        ddlPageSize.Items.Insert(0, "1");
        for (int i = 5; i <= 100; i = i + 5)
        {
            ddlPageSize.Items.Insert(contador, i.ToString());
            contador = contador + 1;
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Obligaciones.Entities.ObPlanPagos> lstConsulta = new List<Xpinn.Obligaciones.Entities.ObPlanPagos>();
            Xpinn.Obligaciones.Services.ObPlanPagosService obPlanPagService = new Xpinn.Obligaciones.Services.ObPlanPagosService();

            lstConsulta = obPlanPagService.ListarObPlanRegistroPagos(long.Parse(idObjeto), Convert.ToDateTime(Session["FechaProxPago"]), (Usuario)Session["usuario"]);

            gvObPlanPago.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvObPlanPago.PageSize = Convert.ToInt32(ddlPageSize.Text);
                gvObPlanPago.Visible = true;
                gvObPlanPago.DataBind();
            }
            else
            {
                gvObPlanPago.Visible = false;
            }

            Session.Add(ObligacionCreditoServicio.CodigoPrograma4 + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma4, "Actualizar", ex);
        }
    }


    protected void gvObPlanPago_RowCommand(object sender, GridViewCommandEventArgs evt)
    {

        if (evt.CommandName == "DetallePago")
        {

            int index = Convert.ToInt32(evt.CommandArgument);

            GridViewRow gvObPlanRow = gvObPlanPago.Rows[index];

            Configuracion conf = new Configuracion();
            txtFechaPago.Text = DateTime.Now.ToString(conf.ObtenerFormatoFecha());
            txtFechaCuota.Text = gvObPlanPago.Rows[index].Cells[2].Text;
            txtNroCuota.Text = gvObPlanPago.Rows[index].Cells[1].Text;
            txtCapital.Text = gvObPlanPago.Rows[index].Cells[3].Text;
            txtIntCorr.Text = gvObPlanPago.Rows[index].Cells[4].Text;
            txtIntMora.Text = gvObPlanPago.Rows[index].Cells[5].Text;
            txtSeguro.Text = gvObPlanPago.Rows[index].Cells[6].Text;

            PanelFormaP.Visible = false;
            ddlFormaPago.SelectedIndex = 0;
            ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
            mpeRegObPlanPago.Show();
        }
    }


    protected void AceptarButton_Click(object sender, EventArgs e)
    {
        Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
        Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
        ObligaCred.cod_tipo_ope = 42; // Tipo de Operacion para Pagos de Obligaciones
        ObligaCred.codobligacion = long.Parse(idObjeto);
        ObligaCred.nrocuota = long.Parse(txtNroCuota.Text);
        try
        {
            ObligaCred.fechapago = DateTime.ParseExact(txtFechaPago.Text, gFormatoFecha, null);
        }
        catch
        {
            VerError("No se pudo determinar fecha de pago");
            return;
        }
        if (txtFechaCuota.Text.Trim() != "")
            ObligaCred.fechacuota = ConvertirStringToDate(txtFechaCuota.Text); //DateTime.ParseExact(txtFechaCuota.Text, gFormatoFecha, null);
        ObligaCred.amort_cap = txtCapital.Text == "" ? 0 : decimal.Parse(txtCapital.Text.Replace(".", ""));
        ObligaCred.interes_corriente = txtIntCorr.Text == "" ? 0 : decimal.Parse(txtIntCorr.Text.Replace(".", ""));
        ObligaCred.interes_mora = txtIntMora.Text == "" ? 0 : decimal.Parse(txtIntMora.Text.Replace(".", ""));
        ObligaCred.seguro = txtSeguro.Text == "" ? 0 : decimal.Parse(txtSeguro.Text.Replace(".", ""));


        //GRABAR GIRO
        try
        {
            if (ddlEntidad2.SelectedItem.Text != null && PanelFormaP.Visible == true)
            {
                Xpinn.FabricaCreditos.Services.AvanceService AvancServices = new Xpinn.FabricaCreditos.Services.AvanceService();
                Xpinn.FabricaCreditos.Entities.Giro pGiro = new Xpinn.FabricaCreditos.Entities.Giro();

                Usuario pusu = (Usuario)Session["usuario"];

                pGiro.idgiro = 0;
                Xpinn.FabricaCreditos.Entities.ControlCreditos Vdata = new Xpinn.FabricaCreditos.Entities.ControlCreditos();

                pGiro.cod_persona = Convert.ToInt64(ddlEntidad2.SelectedValue);
                pGiro.forma_pago = Convert.ToInt32(ddlFormaPago.SelectedValue);
                pGiro.tipo_acto = 1;
                pGiro.fec_reg = DateTime.Now;
                pGiro.fec_giro = DateTime.Now;
                pGiro.numero_radicacion = Convert.ToInt64(txtNroObligacion.Text);
                pGiro.usu_gen = pusu.nombre;
                pGiro.usu_apli = null;
                pGiro.estadogi = 1;
                pGiro.usu_apro = null;

                CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ddlEntidad2.SelectedValue), ddlCuenta.SelectedItem.Text, (Usuario)Session["usuario"]);
                Int64 idCta = CuentaBanc.idctabancaria;

                if (PanelFormaP.Visible == true)
                {
                    if (ddlCuenta.SelectedItem.Text != null)
                        pGiro.idctabancaria = idCta;
                    pGiro.cod_banco = 0;
                    pGiro.num_cuenta = null;
                    pGiro.tipo_cuenta = 0;
                }
                else
                {
                    pGiro.idctabancaria = 0;
                    pGiro.cod_banco = 0;
                    pGiro.num_cuenta = null;
                    pGiro.tipo_cuenta = 0;
                }
                pGiro.fec_apro = DateTime.MinValue;
                pGiro.cob_comision = 0;

                decimal total = Convert.ToDecimal(txtCapital.Text) + Convert.ToDecimal(txtIntCorr.Text) + Convert.ToDecimal(txtIntMora.Text) + Convert.ToDecimal(txtSeguro.Text);
                pGiro.valor = Convert.ToInt64(total);

                AvancServices.CrearGiro(pGiro, (Usuario)Session["usuario"], 1);
            }

        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }

        ObligaCred = ObligacionCreditoServicio.CrearTransacOpePagoOb(ObligaCred, (Usuario)Session["usuario"]);
        mpeRegObPlanPago.Hide();
        Session[ObligacionCreditoServicio.CodigoPrograma4 + ".id"] = idObjeto;

        string Cod_Tercero = SolicitudServicio.Consultar_Tercero(long.Parse(idObjeto), (Usuario)Session["usuario"]);
        // Generar el comprobante
        if (PanelFormaP.Visible == true)
            Session["numerocheque"] = txtNumSop.Text;// Enviando número de cheque        
        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
        Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = ObligaCred.cod_ope;
        Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 42;
        Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = 42;
        Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = DateTime.ParseExact(txtFechaPago.Text, gFormatoFecha, null);
        Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = Cod_Tercero;
        Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");

    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        mpeRegObPlanPago.Hide();
    }

    // Mètodo para actualizar la grilla
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvObPlanPago.PageSize = Convert.ToInt32(ddlPageSize.Text);
        Actualizar();
    }


    protected void ddlEntidad2_SelectedIndexChanged(object sender, EventArgs e)
    {
        LlenarCuenta();
        // AsignarNumeroCheque(ddlCuenta.SelectedItem.ToString());
    }

    void LlenarCuenta()
    {
        try
        {
            Int64 codbanco = Convert.ToInt64(ddlEntidad2.SelectedValue);
            if (codbanco != 0)
            {
                Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
                Usuario usuario = (Usuario)Session["usuario"];
                ddlCuenta.DataSource = bancoService.ListarCuentaBancos(codbanco, usuario);
                ddlCuenta.DataTextField = "num_cuenta";
                ddlCuenta.DataValueField = "idctabancaria";
                ddlCuenta.DataBind();
            }
            else
                ddlCuenta.Items.Clear();
        }
        catch
        {
            VerError("...");
        }
    }

    protected void ddlCuenta_SelectedIndexChanged(object sender, EventArgs e)
    {
        AsignarNumeroCheque(ddlCuenta.SelectedItem.Text);
    }


    private void AsignarNumeroCheque(string snumcuenta)
    {
        if (snumcuenta.Trim() != "")
        {
            Xpinn.Caja.Services.BancosService BancosService = new Xpinn.Caja.Services.BancosService();
            txtNumSop.Text = BancosService.soporte(snumcuenta, (Usuario)Session["Usuario"]);
        }
    }

    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFormaPago.SelectedItem.Text == "CHEQUES") //CHEQUE
        {
            PanelFormaP.Visible = true;
            ddlEntidad2_SelectedIndexChanged(ddlEntidad2, null);
        }
        else
            PanelFormaP.Visible = false;
    }
}