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
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Globalization;
partial class Nuevo : GlobalWeb
{
    CertificacionService CertificacionService = new CertificacionService();
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
            if (Session[CertificacionService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(CertificacionService.CodigoPrograma, "E");
            else
                VisualizarOpciones(CertificacionService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            txtFechaCertificado.eventoCambiar += txtFechaCertificado_TextChanged;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CertificacionService.CodigoPrograma, "Page_PreInit", ex);
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
                if (Session[CertificacionService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[CertificacionService.CodigoPrograma + ".id"].ToString();
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
            BOexcepcion.Throw(CertificacionService.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        ctlMensaje.MostrarMensaje("Desea generar la certificación?");
    }
    public string MonthName(int month)
    {
        DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", true).DateTimeFormat;
        return dtinfo.GetMonthName(month);
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["usuario"];
        // LLenar data table con los datos a certificar
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("cod_atr", typeof(Int32)).AllowDBNull = true;
        table.Columns.Add("nombre");
        table.Columns.Add("valor", typeof(Decimal)).AllowDBNull = true;        
        DataRow datarw;
        if (gvLista.Rows.Count == 0)
        {
            datarw = table.NewRow();
            datarw[0] = DBNull.Value;
            datarw[1] = " ";
            datarw[2] = DBNull.Value;
            table.Rows.Add(datarw);
        }
        else
        {
            foreach (GridViewRow gfila in gvLista.Rows)
            {
                Label lblCodAtr = (Label)gfila.FindControl("lblCodAtr");
                Label lblDescripcion = (Label)gfila.FindControl("lblDescripcion");
                double valor = 0;
                Label txtValor = (Label)gfila.FindControl("lblValor");
                try { valor = Convert.ToDouble(txtValor.Text.Replace(GlobalWeb.gSeparadorMiles, "").Replace("$", "").Replace(" ", "").Replace("(", "").Replace(")", "")); }
                catch { }
                datarw = table.NewRow();
                datarw[0] = " " + lblCodAtr.Text;
                datarw[1] = " " + lblDescripcion.Text;
                datarw[2] = " " + valor;                
                table.Rows.Add(datarw);
            }
        }

        DateTime fecha = Convert.ToDateTime(txtFechaCertificado.Texto);
        Int64 year = fecha.Year;

        String mes = MonthName(fecha.Month);
        Int64 dia = fecha.Day;
        String fechaimprimir = dia + " de " + mes + " de " + year;


        DateTime fechapertura = Convert.ToDateTime(txtFechaAprobacion.Text);
        Int64 yearapertura = fechapertura.Year;
        String mesapertura = MonthName(fechapertura.Month);
        Int64 diaapertura = fechapertura.Day;
        String fechaimprimirapertura = diaapertura + " de " + mesapertura + " de " + yearapertura;

        DateTime fechaultpago = Convert.ToDateTime(txtFechaUltimoPago.Text);
        Int64 yearultpago= fechaultpago.Year;
        String mesultpago = MonthName(fechaultpago.Month);
        Int64 diaultpago = fechaultpago.Day;
        String fechaimprimirultpago = diaultpago + " de " + mesultpago + " de " + yearultpago;



        Usuario pUsu = (Usuario)Session["usuario"];
        // ---------------------------------------------------------------------------------------------------------
        // Pasar datos al reporte
        // ---------------------------------------------------------------------------------------------------------
        ReportParameter[] param = new ReportParameter[12];

        param[0] = new ReportParameter("Entidad", pUsuario.empresa);
        param[1] = new ReportParameter("Numero_Radicacion", txtNumero_radicacion.Text);
        param[2] = new ReportParameter("FechaCertificacion", fechaimprimir);
        param[3] = new ReportParameter("Nombre", txtNombre.Text);
        param[4] = new ReportParameter("Identificacion", txtIdentificacion.Text);
        param[5] = new ReportParameter("FechaApertura", fechaimprimirapertura);
        param[6] = new ReportParameter("FechaUltimoPago", fechaimprimirultpago);
        param[7] = new ReportParameter("SaldoCapital", txtSaldoCapital.Text);
        param[8] = new ReportParameter("TotalCertificar", txtValorCertificado.Text);
        param[9] = new ReportParameter("UsuarioElabora", pUsuario.nombre);
        param[10] = new ReportParameter("ImagenReport", ImagenReporte());
        param[11] = new ReportParameter("gerente", pUsu.representante_legal);

        rvCertificado.LocalReport.EnableExternalImages = true;
        rvCertificado.LocalReport.SetParameters(param);
        
        ReportDataSource rds = new ReportDataSource("DataSet1", table);

        rvCertificado.LocalReport.DataSources.Clear();
        rvCertificado.LocalReport.DataSources.Add(rds);
        rvCertificado.LocalReport.Refresh();

        // Mostrar el reporte en pantalla.
        mvCertificado.ActiveViewIndex = 1;
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        mvCertificado.ActiveViewIndex = 0;
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
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
            ListarValores();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CertificacionService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void ListarValores()
    {
        try
        {
            CreditoService CreditoServicio = new CreditoService();
            List<Atributos> lstAtributos = new List<Atributos>();
            Int64 numero_radicacion = 0;
            DateTime fecha_pago = System.DateTime.Now;
            if (txtFechaCertificado.TieneDatos)
                fecha_pago = txtFechaCertificado.ToDateTime;
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
            lstAtributos = CreditoServicio.AmortizarCreditoDetalle(numero_radicacion, fecha_pago, valor_pago, tipo_pago, ref Error, (Usuario)Session["usuario"]);
            if (lstAtributos.Count() <= 0)
            {
                Atributos atrib = new Atributos();
                lstAtributos.Add(atrib);
            }
            gvLista.DataSource = lstAtributos;
            gvLista.DataBind();
            CalcularValor();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CertificacionService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void chkAplica_CheckedChanged(object sender, EventArgs e)
    {
        CalcularValor();
    }

    private void CalcularValor()
    {
        Double total = 0;
        foreach (GridViewRow gfila in gvLista.Rows)
        {
            Label lblCodAtr = (Label)gfila.FindControl("lblCodAtr");
            double valor = 0;
            Label txtValor = (Label)gfila.FindControl("lblValor");
            try { valor = Convert.ToDouble(txtValor.Text.Replace(GlobalWeb.gSeparadorMiles, "").Replace("$", "").Replace(" ", "").Replace("(", "").Replace(")", "")); }
            catch { }
            total = total + valor;
        }
        if (gvLista.FooterRow != null)
        {
            TextBox txtTotal = (TextBox)gvLista.FooterRow.FindControl("txtTotal");
            GlobalWeb gweb = new GlobalWeb();
            txtTotal.Text = gweb.FormatoDecimal(total.ToString());
            txtValorCertificado.Text = Convert.ToString(Convert.ToDouble(txtTotal.Text));
        }
        else
        {
            txtValorCertificado.Text = txtSaldoCapital.Text;
        }
    }

    protected void txtFechaCertificado_TextChanged(object sender, EventArgs e)
    {
        ListarValores(); 
    }


}