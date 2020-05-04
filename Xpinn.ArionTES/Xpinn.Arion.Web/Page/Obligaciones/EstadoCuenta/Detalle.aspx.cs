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
using Microsoft.Reporting.WebForms;

public partial class Detalle : GlobalWeb
{
    private Xpinn.Obligaciones.Services.SolicitudService SolicitudServicio = new Xpinn.Obligaciones.Services.SolicitudService();
    private Xpinn.Obligaciones.Services.ObligacionCreditoService ObligacionCreditoServicio = new Xpinn.Obligaciones.Services.ObligacionCreditoService();
    private Xpinn.Obligaciones.Entities.ObligacionCredito ObligaCred = new Xpinn.Obligaciones.Entities.ObligacionCredito();
    private Xpinn.Obligaciones.Services.ComponenteAdicionalService CompAdServicio = new Xpinn.Obligaciones.Services.ComponenteAdicionalService();
    private Xpinn.Obligaciones.Services.PagoExtraordService PayExtServicio = new Xpinn.Obligaciones.Services.PagoExtraordService();
    
    PeriodicidadService periodicidadServicio = new PeriodicidadService();
    ComponenteService componenteServicio = new ComponenteService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ObligacionCreditoServicio.CodigoPrograma2 + ".id"] != null)
                VisualizarOpciones(ObligacionCreditoServicio.CodigoPrograma2, "E");
            else
                VisualizarOpciones(ObligacionCreditoServicio.CodigoPrograma2, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += btnRegresar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma2, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarComboEntidades(ddlEntidad);
                LlenarComboMonedas(ddlTipoMoneda);
                LlenarComboPeriodicidadCuota(ddlPeriodCuotas);
                LlenarComboTipoTasa(ddlTipoTasa);
                
                if (Session[ObligacionCreditoServicio.CodigoPrograma2 + ".id"] != null)
                {
                    idObjeto = Session[ObligacionCreditoServicio.CodigoPrograma2 + ".id"].ToString();
                    Session.Remove(ObligacionCreditoServicio.CodigoPrograma2 + ".id");
                    ObtenerDatos(idObjeto);

                    if (txtEstado.Text != "Desembolsado")
                        btnDetPagPend.Enabled = false;

                    ActualizarRelMovsObligacion();
                    ActualizarComponentes();
                    ActualizarPagosExtra();
                }

              
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma2, "Page_Load", ex);
        }
    }


    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Navegar("../EstadoCuenta/Lista.aspx");
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
            if (!string.IsNullOrEmpty(vSolicitud.montosolicitado.ToString()))
                txtMontoSol.Text = vSolicitud.montosolicitado.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.montoaprobado.ToString()))
                txtMontoApro.Text = vSolicitud.montoaprobado.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.tipomoneda.ToString()))
                ddlTipoMoneda.SelectedValue = vSolicitud.tipomoneda.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.fechasolicitud.ToShortDateString()))
                txtFechaSolicitud.Text = vSolicitud.fechasolicitud.ToShortDateString();
            if (!string.IsNullOrEmpty(vSolicitud.fecha_aprobacion.ToShortDateString()))
                txtFechaDesembolso.Text = vSolicitud.fecha_aprobacion.ToShortDateString();
            if (!string.IsNullOrEmpty(vSolicitud.fecha_inicio.ToShortDateString()))
                txtFechaInicio.Text = vSolicitud.fecha_inicio.ToShortDateString();
            if (!string.IsNullOrEmpty(vSolicitud.fecha_terminacion.ToShortDateString()))
                txtFechaTerminacion.Text = vSolicitud.fecha_terminacion.ToShortDateString();
            if (!string.IsNullOrEmpty(vSolicitud.montoaprobado.ToString()))
                txtMontoApro.Text = vSolicitud.montoaprobado.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.saldocapital.ToString()))
                txtSaldo.Text = vSolicitud.saldocapital.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.montosolicitado.ToString()))
                txtMontoSol.Text = vSolicitud.montosolicitado.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.plazo.ToString()))
                txtPlazo.Text = vSolicitud.plazo.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.codperiodicidad.ToString()))
                ddlPeriodCuotas.SelectedValue = vSolicitud.codperiodicidad.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.numeropagare.ToString()))
                txtPagare.Text = vSolicitud.numeropagare.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.cuota.ToString()))
                txtValCuota.Text = vSolicitud.cuota.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.cod_tipo_tasa.ToString()))
                ddlTipoTasa.SelectedValue = vSolicitud.cod_tipo_tasa.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.tasa.ToString()))
                txtValorTasa.Text = vSolicitud.tasa.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.spread.ToString()))
                txtPuntosads.Text = vSolicitud.spread.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.fechaultimopago.ToShortDateString()))
                txtFechaUltPago.Text = vSolicitud.fechaultimopago.ToShortDateString();
            if (!string.IsNullOrEmpty(vSolicitud.fechaproximopago.ToShortDateString()))
                txtFechaProxPago.Text = vSolicitud.fechaproximopago.ToShortDateString();
            if (!string.IsNullOrEmpty(vSolicitud.gracia.ToString()))
                txtGracia.Text = vSolicitud.gracia.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.cuotaspagadas.ToString()))
                txtCuotasPag.Text = vSolicitud.cuotaspagadas.ToString();

            //OBCOMPONENTECREDITO
            if (!string.IsNullOrEmpty(vSolicitud.calculocomponente.ToString()))
                rbCalculoTasa.SelectedValue = vSolicitud.calculocomponente.ToString();
            if (vSolicitud.calculocomponente == 2)
            {
                LlenarComboTipoTasa(ddlTipoTasa, false);
                if (!string.IsNullOrEmpty(vSolicitud.tipo_historico.ToString()))
                    ddlTipoTasa.SelectedValue = vSolicitud.tipo_historico.ToString();
            }
            else
            {
                LlenarComboTipoTasa(ddlTipoTasa, true);
                if (!string.IsNullOrEmpty(vSolicitud.cod_tipo_tasa.ToString()))
                    ddlTipoTasa.SelectedValue = vSolicitud.cod_tipo_tasa.ToString();
            }
            if (!string.IsNullOrEmpty(vSolicitud.tasa.ToString()))
                txtValorTasa.Text = vSolicitud.tasa.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma2, "ObtenerDatos", ex);
        }
    }

    protected void LlenarComboTipoTasa(DropDownList ddlTipoTasa, Boolean tipo)
    {
        Xpinn.Obligaciones.Services.TipoTasaService tasaService = new Xpinn.Obligaciones.Services.TipoTasaService();
        Xpinn.Obligaciones.Entities.TipoTasa tasa = new Xpinn.Obligaciones.Entities.TipoTasa();
        if (tipo == true)
            ddlTipoTasa.DataSource = tasaService.ListarTipoTasa(tasa, (Usuario)Session["usuario"]);
        else
            ddlTipoTasa.DataSource = tasaService.ListarTipoHistorico(tasa, (Usuario)Session["usuario"]);
        ddlTipoTasa.DataTextField = "NOMBRE";
        ddlTipoTasa.DataValueField = "COD_TIPO_TASA";
        ddlTipoTasa.DataBind();
    }


    protected void LlenarComboEntidades(DropDownList ddlEntidades)
    {
        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
        ddlEntidades.DataSource = bancoService.ListarBancos(banco, (Usuario)Session["usuario"]);
        ddlEntidades.DataTextField = "nombrebanco";
        ddlEntidades.DataValueField = "cod_banco";
        ddlEntidades.DataBind();
    }

    protected void LlenarComboMonedas(DropDownList ddlMonedas)
    {

        Xpinn.Caja.Services.TipoMonedaService monedaService = new Xpinn.Caja.Services.TipoMonedaService();
        Xpinn.Caja.Entities.TipoMoneda moneda = new Xpinn.Caja.Entities.TipoMoneda();
        ddlMonedas.DataSource = monedaService.ListarTipoMoneda(moneda, (Usuario)Session["usuario"]);
        ddlMonedas.DataTextField = "descripcion";
        ddlMonedas.DataValueField = "cod_moneda";
        ddlMonedas.DataBind();
    }

    protected void LlenarComboTipoCuota(DropDownList ddlTipoCuotas)
    {
        Xpinn.Obligaciones.Services.TipoLiquidacionService tipoLiqService = new Xpinn.Obligaciones.Services.TipoLiquidacionService();
        Xpinn.Obligaciones.Entities.TipoLiquidacion tipoLiq = new Xpinn.Obligaciones.Entities.TipoLiquidacion();
        ddlTipoCuotas.DataSource = tipoLiqService.ListarTipoLiquidacion(tipoLiq, (Usuario)Session["usuario"]);
        ddlTipoCuotas.DataTextField = "descripcion";
        ddlTipoCuotas.DataValueField = "tipoliquidacion";
        ddlTipoCuotas.DataBind();
    }

    protected void LlenarComboPeriodicidadCuota(DropDownList ddlPeriodicidadCuotas)
    {
        Xpinn.Obligaciones.Services.PeriodicidadCuotaService PeriodicidadCuotaService = new Xpinn.Obligaciones.Services.PeriodicidadCuotaService();
        Xpinn.Obligaciones.Entities.PeriodicidadCuota PeriodicidadCuota = new Xpinn.Obligaciones.Entities.PeriodicidadCuota();
        ddlPeriodicidadCuotas.DataSource = PeriodicidadCuotaService.ListarPeriodicidadCuota(PeriodicidadCuota, (Usuario)Session["usuario"]);
        ddlPeriodicidadCuotas.DataTextField = "DESCRIPCION";
        ddlPeriodicidadCuotas.DataValueField = "COD_PERIODICIDAD";
        ddlPeriodicidadCuotas.DataBind();
    }

    protected void LlenarComboTipoTasa(DropDownList ddlTipoTasa)
    {
        Xpinn.Obligaciones.Services.TipoTasaService tasaService = new Xpinn.Obligaciones.Services.TipoTasaService();
        Xpinn.Obligaciones.Entities.TipoTasa tasa = new Xpinn.Obligaciones.Entities.TipoTasa();
        ddlTipoTasa.DataSource = tasaService.ListarTipoTasa(tasa, (Usuario)Session["usuario"]);
        ddlTipoTasa.DataTextField = "NOMBRE";
        ddlTipoTasa.DataValueField = "COD_TIPO_TASA";
        ddlTipoTasa.DataBind();
    }


    private void ActualizarComponentes()
    {
        try
        {
            List<Xpinn.Obligaciones.Entities.ComponenteAdicional> lstConsulta = new List<Xpinn.Obligaciones.Entities.ComponenteAdicional>();
            lstConsulta = CompAdServicio.ListarComponenteAdicional(long.Parse(idObjeto), (Usuario)Session["usuario"]);

            gvComponente.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvComponente.Visible = true;
                gvComponente.DataBind();
            }
            else
            {
                gvComponente.Visible = false;
            }

            Session.Add(ObligacionCreditoServicio.CodigoPrograma2 + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma2, "Actualizar", ex);
        }
    }

    private void ActualizarPagosExtra()
    {
        try
        {
            List<Xpinn.Obligaciones.Entities.PagoExtraord> lstConsulta = new List<Xpinn.Obligaciones.Entities.PagoExtraord>();
            lstConsulta = PayExtServicio.ListarPagoExtraord(long.Parse(idObjeto), (Usuario)Session["usuario"]);

            gvPagosExt.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvPagosExt.Visible = true;
                gvPagosExt.DataBind();
            }
            else
            {
                gvPagosExt.Visible = false;
            }

            Session.Add(ObligacionCreditoServicio.CodigoPrograma2 + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma2, "Actualizar", ex);
        }
    }


    private void ActualizarRelMovsObligacion()
    {
        try
        {
            List<Xpinn.Obligaciones.Entities.ObligacionCredito> lstConsulta = new List<Xpinn.Obligaciones.Entities.ObligacionCredito>();
            ObligaCred.codobligacion = long.Parse(idObjeto);
            Configuracion conf = new Configuracion();
            
            decimal monto = 0;
            if (txtMontoApro.Text == "0")
                monto = Convert.ToDecimal(txtMontoSol.Text.Replace(".", conf.ObtenerSeparadorMilesConfig()));
            else
                monto = Convert.ToDecimal(txtMontoApro.Text.Replace(".", conf.ObtenerSeparadorMilesConfig()));

            lstConsulta = ObligacionCreditoServicio.ListarMovsObligacion(ObligaCred, monto, (Usuario)Session["usuario"]);
            gvMovimiento.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvMovimiento.Visible = true;
                gvMovimiento.DataBind();
            }
            else
            {
                gvMovimiento.Visible = false;
            }
            Session["LSTMOVIMIENTOS"] = lstConsulta;

            lstConsulta = ObligacionCreditoServicio.ListarPlanObligacion(ObligaCred, monto, (Usuario)Session["usuario"]);
            gvRelMovObl.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvRelMovObl.Visible = true;
                gvRelMovObl.DataBind();
            }
            else
            {
                gvRelMovObl.Visible = false;
            }
            Session["LSTPLANPAGOS"] = lstConsulta;

            Session.Add(ObligacionCreditoServicio.CodigoPrograma2 + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma2, "Actualizar", ex);
        }
    }


    protected void btnDetPagPend_Click(object sender, EventArgs e)
    {
        Session[ObligacionCreditoServicio.CodigoPrograma2 + ".id"]= idObjeto;
        Session["FechaProxPago"]=txtFechaProxPago.Text;
        Navegar("../EstadoCuenta/DetallePagosPendientes.aspx");
    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        List<Xpinn.Obligaciones.Entities.ObligacionCredito> lstConsultas = new List<Xpinn.Obligaciones.Entities.ObligacionCredito>();                

        // Crear el datatable para poder pasar el plan de pagos al reporte
        DataTable dtPlanPagos = new DataTable();
        dtPlanPagos.Columns.Add("numcuota", typeof(Int32));
        dtPlanPagos.Columns.Add("fechacuota", typeof(DateTime));
        dtPlanPagos.Columns.Add("amort_cap", typeof(decimal));
        dtPlanPagos.Columns.Add("interes_corriente", typeof(decimal));
        dtPlanPagos.Columns.Add("interes_mora", typeof(decimal));
        dtPlanPagos.Columns.Add("seguro", typeof(decimal));
        dtPlanPagos.Columns.Add("total", typeof(decimal));
        dtPlanPagos.Columns.Add("saldo", typeof(decimal));

        int numeroregistros = 0;
        DataRow drFila;
        lstConsultas = (List<Xpinn.Obligaciones.Entities.ObligacionCredito>)Session["LSTMOVIMIENTOS"];
        if (lstConsultas.Count != 0)
        {
            Xpinn.Obligaciones.Entities.ObligacionCredito ePlanPagos = new Xpinn.Obligaciones.Entities.ObligacionCredito();
            for (int i = 0; i < lstConsultas.Count; i++)
            {
                drFila = dtPlanPagos.NewRow();
                ePlanPagos = lstConsultas[i];
                drFila[0] = ePlanPagos.nrocuota;
                drFila[1] = ePlanPagos.fecha;
                drFila[2] = ePlanPagos.amort_cap;
                drFila[3] = ePlanPagos.interes_corriente;
                drFila[4] = ePlanPagos.interes_mora;
                drFila[5] = ePlanPagos.seguro;                
                drFila[6] = ePlanPagos.total;
                drFila[7] = ePlanPagos.saldo;
                dtPlanPagos.Rows.Add(drFila);
                numeroregistros += 1;
            }
        }

        lstConsultas = (List<Xpinn.Obligaciones.Entities.ObligacionCredito>)Session["LSTPLANPAGOS"];
        if (lstConsultas.Count != 0)
        {
            Xpinn.Obligaciones.Entities.ObligacionCredito ePlanPagos = new Xpinn.Obligaciones.Entities.ObligacionCredito();
            for (int i = 0; i < lstConsultas.Count; i++)
            {
                drFila = dtPlanPagos.NewRow();
                ePlanPagos = lstConsultas[i];
                drFila[0] = ePlanPagos.nrocuota;
                drFila[1] = ePlanPagos.fecha;
                drFila[2] = ePlanPagos.amort_cap;
                drFila[3] = ePlanPagos.interes_corriente;
                drFila[4] = ePlanPagos.interes_mora;
                drFila[5] = ePlanPagos.seguro;
                drFila[6] = ePlanPagos.total;
                drFila[7] = ePlanPagos.saldo;
                dtPlanPagos.Rows.Add(drFila);
                numeroregistros += 1;
            }
        }

        if (numeroregistros == 0)
        {
            drFila = dtPlanPagos.NewRow();
            drFila[0] = 0;
            drFila[1] = DateTime.Now;
            drFila[2] = 0;
            drFila[3] = 0;
            drFila[4] = 0;
            drFila[5] = 0;
            drFila[6] = 0;
            drFila[7] = 0;
            dtPlanPagos.Rows.Add(drFila);
        }
        Usuario user =(Usuario)Session["usuario"];

        // Definir parametros a pasar al reporte
        ReportParameter[] parame = new ReportParameter[15];
        parame[0] = new ReportParameter("Empresa", " " + user.empresa);
        parame[1] = new ReportParameter("codobligacion", " " + txtNroObligacion.Text);
        parame[2] = new ReportParameter("lineaobligacion", " " );
        parame[3] = new ReportParameter("entidad", " " + ddlEntidad.SelectedItem.Text);
        parame[4] = new ReportParameter("pagare", " " + txtPagare.Text);
        parame[5] = new ReportParameter("fecha_desembolso", " " + txtFechaDesembolso.Text);
        parame[6] = new ReportParameter("plazo", " " + txtPlazo.Text);
        parame[7] = new ReportParameter("monto", " " + txtMontoSol.Text);        
        parame[8] = new ReportParameter("tasa", " " + txtValorTasa.Text);
        parame[9] = new ReportParameter("tipo_tasa", " " + ddlTipoTasa.SelectedItem.Text);
        parame[10] = new ReportParameter("gracia", " " + txtGracia.Text);
        parame[11] = new ReportParameter("spread", " " + txtPuntosads.Text);
        parame[12] = new ReportParameter("periodicidad", " " + ddlPeriodCuotas.SelectedItem.Text);
        parame[13] = new ReportParameter("ImagenReport", ImagenReporte());
        parame[14] = new ReportParameter("NombUsuario", " " + user.nombre);

        ReportViewerPlan.LocalReport.EnableExternalImages = true;
        ReportViewerPlan.LocalReport.SetParameters(parame);
        ReportDataSource rds = new ReportDataSource("DataSet1", dtPlanPagos);
        ReportViewerPlan.LocalReport.DataSources.Clear();
        ReportViewerPlan.LocalReport.DataSources.Add(rds);
        ReportViewerPlan.LocalReport.Refresh();
        mvEstCueObl.ActiveViewIndex = 1;
    }

    protected void btnCerrar_Click(object sender, EventArgs e)
    {
        mvEstCueObl.ActiveViewIndex = 0;
    }
}