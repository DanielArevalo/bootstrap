using System;
using System.Diagnostics;
using System.Web;
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

partial class Nuevo : GlobalWeb
{
    CastigoService CastigoServicio = new CastigoService();
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
            if (Session[CastigoServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(CastigoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(CastigoServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CastigoServicio.CodigoPrograma, "Page_PreInit", ex);
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
            if (!IsPostBack)
            {
                DropLinea();
                if (Session[CastigoServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[CastigoServicio.CodigoPrograma + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                    txtFechaCastigo.ToDateTime = System.DateTime.Now;
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CastigoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        mpeNuevo.Show();
    }

    protected void btnParar_Click(object sender, EventArgs e)
    {
        mpeNuevo.Hide();
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        try
        {
            Castigo eCastigo = new Castigo();
            eCastigo.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);
            eCastigo.fecha_castigo = txtFechaCastigo.ToDateTime;
            eCastigo.cod_linea_castigo = ddlLineaCastigo.SelectedValue;
            eCastigo.cod_deudor = Convert.ToInt64(lblCodigo.Text);
            eCastigo = CastigoServicio.CrearCastigo(eCastigo, (Usuario)Session["Usuario"]);
            if (eCastigo == null)
                return;

            Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = eCastigo.cod_ope;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 49;
            Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = eCastigo.cod_deudor;
            Session["OrigenComprobante"] = HttpContext.Current.Request.Url.AbsoluteUri; ;
            Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
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
            if (vCredito.cod_deudor != Int64.MinValue)
                lblCodigo.Text = HttpUtility.HtmlDecode(vCredito.cod_deudor.ToString().Trim());
            if (vCredito.identificacion != "")
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

            List<Atributos> lstAtributos = new List<Atributos>();
            Int64 numero_radicacion = 0;
            DateTime fecha_pago = System.DateTime.Now;
            Double valor_pago = 0;
            Int64 tipo_pago = 2;
            Int64 Error = 0;
            try
            {
                numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
            lstAtributos = CreditoServicio.AmortizarCreditoDetalle(numero_radicacion, fecha_pago, valor_pago, tipo_pago, ref Error, (Usuario)Session["Usuario"]);
            gvLista.DataSource = lstAtributos;
            gvLista.DataBind();
            CalcularValor();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CastigoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    private void DropLinea()
    {
        LineasCreditoService lineasServicio = new LineasCreditoService();
        ddlLineaCastigo.DataSource = lineasServicio.LineasCastigo((Usuario)Session["usuario"]);
        ddlLineaCastigo.DataTextField = "nombre";
        ddlLineaCastigo.DataValueField = "cod_linea_credito";
        ddlLineaCastigo.DataBind();
        ddlLineaCastigo.Text = txtPeriodicidad.Text;
    }


    protected void chkAplica_CheckedChanged(object sender, EventArgs e)
    {
        CalcularValor();
    }

    private void CalcularValor()
    {
        Double total = 0;
        Double total_causado = 0;
        foreach (GridViewRow gfila in gvLista.Rows)
        {
            CheckBox chkAplica = (CheckBox)gfila.FindControl("chkAplica");
            chkAplica.Checked = true;
            if (chkAplica.Checked)
            {
                // Valor a pagar
                double valor = 0;
                Label txtValor = (Label)gfila.FindControl("lblValor");
                try 
                { 
                    valor = Convert.ToDouble(txtValor.Text.Replace(GlobalWeb.gSeparadorMiles, "").Replace("$", "").Replace(" ", "").Replace("(", "").Replace(")", "")); 
                }
                catch (Exception ex)
                {
                    VerError(ex.Message);
                }
                total = total + valor;
                // Calcular valor causado
                double valor_causado = 0;
                Label txtTotalCausado = (Label)gfila.FindControl("lblValorCausado");
                try
                {
                    valor_causado = Convert.ToDouble(txtTotalCausado.Text.Replace(GlobalWeb.gSeparadorMiles, "").Replace("$", "").Replace(" ", "").Replace("(", "").Replace(")", ""));
                }
                catch (Exception ex)
                {
                    VerError(ex.Message);
                }
                total_causado = total_causado + valor_causado;
            }
        }
        if (gvLista.Rows.Count > 0)
        {
            try
            {
                TextBox txtTotal = (TextBox)gvLista.FooterRow.FindControl("txtTotal");
                GlobalWeb gweb = new GlobalWeb();
                txtTotal.Text = gweb.FormatoDecimal(total.ToString());
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
            try
            {
                TextBox txtTotalCausado = (TextBox)gvLista.FooterRow.FindControl("txtTotalCausado");
                GlobalWeb gweb = new GlobalWeb();
                txtTotalCausado.Text = gweb.FormatoDecimal(total_causado.ToString());
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }


}