using System;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Programado.Services;
using Xpinn.Programado.Entities;
using Xpinn.Comun.Services;
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;
using Xpinn.Aportes.Services;
using Microsoft.Reporting.WebForms;
using Xpinn.FabricaCreditos.Entities;
using System.Linq;

public partial class Nuevo : GlobalWeb
{
    private CuentasProgramadoServices CuentasPrograServicios = new CuentasProgramadoServices();
    private CuentasProgramado CuentasProgramado = new CuentasProgramado();
    private AfiliacionServices AfiliacionServicios = new AfiliacionServices();
    private FechasService FechasServicio = new FechasService();
    private GeneralService GeneralServicio = new GeneralService();
    private NumeracionAhorrosServices numeracionServicio = new NumeracionAhorrosServices();
    private CuentasProgramado pCuenta = new CuentasProgramado();
    private Xpinn.Aportes.Services.AfiliacionServices AfiliacionServicio = new Xpinn.Aportes.Services.AfiliacionServices();
    private Xpinn.CDATS.Services.AperturaCDATService AperturaService = new Xpinn.CDATS.Services.AperturaCDATService();
    private NumeracionCuentas BONumeracionCuentaCDAT = new NumeracionCuentas();
    private AhorroVistaServices _ahorroService = new AhorroVistaServices();

    private Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
    private String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    private String cuenta = "";
    private decimal plazo = 0;
    private decimal cuota = 0;

    CuentasProgramadoServices cuentasProgramado = new CuentasProgramadoServices();

    private Xpinn.FabricaCreditos.Services.BeneficiarioService BeneficiarioServicio = new Xpinn.FabricaCreditos.Services.BeneficiarioService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[CuentasPrograServicios.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(CuentasPrograServicios.CodigoPrograma, "E");
            else
                VisualizarOpciones(CuentasPrograServicios.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.MostrarLimpiar(false);
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            txtFechApertura.eventoCambiar += txtFechApertura_TextChanged;
            ctlBusquedaPersonas.eventoEditar += gvControl_RowEditing;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentasPrograServicios.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                InicialCuoExt();
                Session["tipocalendario"] = null;
                Session["dias"] = null;
                cbInteresCuenta.Enabled = false;
                ddlTipoMoneda.SelectedIndex = 1;
                mvReporte.Visible = false;

                CargarDropDown();
                lblInteresPagar.Visible = false;
                txtInteresPagar.Visible = false;
                txtInteresCapitalizar.Visible = false;
                lblInteresCapitalizar.Visible = false;

                pConsulta.Visible = true;
                if (Session[CuentasPrograServicios.CodigoPrograma + ".id"] != null)
                {
                    MvPrincipal.ActiveViewIndex = 1;
                    idObjeto = Session[CuentasPrograServicios.CodigoPrograma + ".id"].ToString();
                    Session.Remove(CuentasPrograServicios.CodigoPrograma + ".id");

                    ObtenerDatos(idObjeto);
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarLimpiar(true);
                    toolBar.MostrarConsultar(false);
                    toolBar.MostrarGuardar(true);
                    ddlLineaFiltro.Enabled = false;

                    if (Session[CuentasPrograServicios.CodigoPrograma + ".renovacion"] != null)
                    {
                        ddlLineaFiltro.Enabled = true;
                    }
                }
                else
                {

                    MvPrincipal.ActiveViewIndex = 0;
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarGuardar(false);
                    ddlLineaFiltro.Enabled = true;

                    //Numeracion de cuentas
                    Xpinn.CDATS.Entities.Cdat Data = new Xpinn.CDATS.Entities.Cdat();
                    Data = AperturaService.ConsultarNumeracionCDATS(Data, (Usuario)Session["usuario"]);

                    if (Data.valor == 1)
                    {
                        txtCuenta.Visible = false;
                        lblNumAuto.Visible = true;
                    }
                    else
                    {
                        txtCuenta.Visible = true;
                        lblNumAuto.Visible = false;
                    }
                }
                ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
                cargarDatosSolicitud();


            }

            //Generar Consulta de la Linea Seleccionada
            LineasProgramado vLineaAhorro = new LineasProgramado();
            LineasProgramadoServices lineaServicio = new LineasProgramadoServices();
            List<LineaProgramado_Rango> Lista_rango = new List<LineaProgramado_Rango>();
            vLineaAhorro = lineaServicio.ConsultarLineasProgramado(Convert.ToInt64(ddlLineaFiltro.SelectedValue), ref Lista_rango, (Usuario)Session["usuario"]);

            if (vLineaAhorro.maneja_cuota_extra == 1)
            {
                lblcuotasExtras.Visible = true;
                upCuotasExtras.Visible = true;
                ChkCuotasExtras.Visible = true;

            }
            else
            {
                lblcuotasExtras.Visible = false;
                upCuotasExtras.Visible = false;
                ChkCuotasExtras.Visible = false;

            }


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentasPrograServicios.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFormaPago.SelectedValue == "1")
        {
            panelEmpresa.Visible = false;
        }
        else
        {
            panelEmpresa.Visible = true;
            txtFechaProxPago.Text = Convert.ToString(DateTime.Now.AddMonths(1));
        }
        DeterminarFechaPrimPago();

    }

    protected void ConsultarDatosAfiliacion()
    {
        Afiliacion pAfili = new Afiliacion();
        pAfili = AfiliacionServicio.ConsultarAfiliacion(Convert.ToInt64(txtCodigoCliente.Text), (Usuario)Session["usuario"]);
        txtfechaafili.Text = System.DateTime.Now.ToString(gFormatoFecha);
        if (pAfili.cod_periodicidad != 0)
            this.ddlPeriodicidad.SelectedValue = pAfili.cod_periodicidad.ToString();
        if (pAfili.empresa_formapago != 0)
            try { ddlEmpresa.SelectedValue = pAfili.empresa_formapago.ToString(); } catch { }
        //Cambio en el order de asignación de las variables para poder calcular la fecha de primer pago
        if (pAfili.forma_pago != 0)
            ddlFormaPago.SelectedValue = pAfili.forma_pago.ToString();
        ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);

        /*if (pAfili.fecha_primer_pago != null) Se hace uso de la función DeterminarFechaPrimPago para calcular las fechas
        {
            txtFechaProxPago.Text = pAfili.fecha_primer_pago.Value.ToString(gFormatoFecha);
            txtFechaPrimPago.Text = pAfili.fecha_primer_pago.Value.ToString(gFormatoFecha);
        }
        else
        {
            txtFechaProxPago.Text = Convert.ToString(DateTime.Now.AddMonths(1));
            txtFechaPrimPago.Text = Convert.ToString(DateTime.Now.AddMonths(1));
        }
        if (pAfili.forma_pago == 1)
        {
            txtFechaProxPago.Text = Convert.ToString(DateTime.Now.AddMonths(1));
            txtFechaPrimPago.Text = Convert.ToString(DateTime.Now.AddMonths(1));

        }*/

        txtPlazo.Text = "1";
        //txtFechaPrimPago.Text = DateTime.Now.ToShortDateString();
        CuentasProgramado pCuenta = new CuentasProgramado();
        pCuenta = CuentasPrograServicios.ConsultarPeriodicidadProgramado(Convert.ToInt64(ddlPeriodicidad.SelectedValue), (Usuario)Session["usuario"]);
        Session["tipocalendario"] = pCuenta.tipo_calendario;
        Session["dias"] = pCuenta.numero_dias;
    }


    protected void DeterminarFechaInicioAfiliacion()
    {
        if (!txtfechaafili.TieneDatos)
            txtfechaafili.ToDateTime = System.DateTime.Now;
        if (ddlEmpresa.SelectedValue == null)
            return;
        DateTime? fechainicio;
        try
        {
            fechainicio = AfiliacionServicio.FechaInicioAfiliacion(txtfechaafili.ToDateTime, Convert.ToInt64(ddlEmpresa.SelectedValue), (Usuario)Session["Usuario"]);
            if (fechainicio != null)
                txtFechaProxPago.ToDateTime = Convert.ToDateTime(fechainicio);
        }
        catch
        { }
    }

    void DeterminarFechaPrimPago()
    {
        //Calculo de la fecha de primer pago a partir de la forma de pago
        if (idObjeto == "" && ddlPeriodicidad.SelectedIndex > 0)
        {
            CuentasProgramadoServices ProgramadoServicio = new CuentasProgramadoServices();
            CuentasProgramado vProgramado = new CuentasProgramado();
            vProgramado.cod_linea_programado = ddlLineaFiltro.SelectedValue;
            vProgramado.cod_periodicidad = Convert.ToInt64(ddlPeriodicidad.SelectedValue);
            if (ddlFormaPago.SelectedValue == "2")
            {
                if (ddlEmpresa.Items.Count == 1)
                {
                    VerError("No existen empresas asocidas a la persona");
                    ddlFormaPago.SelectedValue = "1";
                    vProgramado.cod_empresa = 0;
                    panelEmpresa.Visible = false;
                }
                else if (ddlEmpresa.SelectedValue != "")
                {
                    vProgramado.cod_empresa = Convert.ToInt32(ddlEmpresa.SelectedValue);
                }
                else
                {
                    vProgramado.cod_empresa = 0;
                }

            }
            else
            {
                ddlFormaPago.SelectedValue = "1";
                vProgramado.cod_empresa = 0;
            }
            vProgramado.fecha_apertura = Convert.ToDateTime(txtFechApertura.Text);
            vProgramado.forma_pago = Convert.ToInt32(ddlFormaPago.SelectedValue);
            if (vProgramado.cod_empresa != 0)
            {
                DateTime? pFecIni = ProgramadoServicio.FechaPrimerPago(vProgramado, (Usuario)Session["usuario"]);
                txtFechaPrimPago.Text = vProgramado.forma_pago == 2 ? Convert.ToDateTime(pFecIni).ToShortDateString() : txtFechApertura.Text;
            }
            else
            {
                txtFechaPrimPago.Text = vProgramado.forma_pago == 2 ? Convert.ToDateTime(DateTime.Now).ToShortDateString() : txtFechApertura.Text;
            }


            txtPlazo_TextChanged(txtPlazo, null);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (Session["solicitudProducto"] != null)
        {
            Session["solicitudProducto"] = null;
            Response.Redirect("../../Aportes/ConfirmarProductoAprobado/Lista.aspx", false);
        }
        else
        {
            Navegar(Pagina.Lista);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        ctlBusquedaPersonas.Filtro = "";
        ctlBusquedaPersonas.Actualizar(0);
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        ddlLineaFiltro.Enabled = true;
        MvPrincipal.ActiveViewIndex = 0;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarConsultar(true);
        toolBar.MostrarLimpiar(false);
        toolBar.MostrarGuardar(false);
    }


    private void CargarDropDown()
    {
        try
        {
            ctlTasaInteres.Inicializar();
            //PERIODICIDAD
            PoblarLista("Tipomoneda", ddlTipoMoneda);

            LlenarListasDesplegables(TipoLista.Asesor.ToString(), ddlAsesor);

            ListaSolicitada = "Periodicidad";
            TraerResultadosLista();
            ddlPeriodicidad.DataSource = lstDatosSolicitud;
            ddlPeriodicidad.DataTextField = "ListaDescripcion";
            ddlPeriodicidad.DataValueField = "ListaIdStr";
            ddlPeriodicidad.DataBind();
            ddlPeriodicidad.Items.Insert(0, "Seleccionar item");

            //TIPOIDENTIFICACION
            ListaSolicitada = "TipoIdentificacion";
            TraerResultadosLista();
            ddlTipoIdentifi.DataSource = lstDatosSolicitud;
            ddlTipoIdentifi.DataTextField = "ListaDescripcion";
            ddlTipoIdentifi.DataValueField = "ListaId";
            ddlTipoIdentifi.DataBind();

            //PERIODICIDAD DE CUOTAS EXTRAS
            ListaSolicitada = "Periodicidad";
            TraerResultadosLista();
            ddlPeriodicidadCuotaExt.DataSource = lstDatosSolicitud;
            ddlPeriodicidadCuotaExt.DataTextField = "ListaDescripcion";
            ddlPeriodicidadCuotaExt.DataValueField = "ListaIdStr";
            ddlPeriodicidadCuotaExt.DataBind();

            //LINEAS DE AHORRO PROGRAMADO
            Xpinn.Programado.Data.LineasProgramadoData vDatosLinea = new Xpinn.Programado.Data.LineasProgramadoData();
            LineasProgramado pLineas = new LineasProgramado();
            List<LineasProgramado> lstConsulta = new List<LineasProgramado>();
            pLineas.estado = 1;
            lstConsulta = vDatosLinea.ListarComboLineas(pLineas, (Usuario)Session["usuario"]);
            if (lstConsulta.Count > 0)
            {
                ddlLineaFiltro.DataSource = lstConsulta;
                ddlLineaFiltro.DataTextField = "nombre";
                ddlLineaFiltro.DataValueField = "cod_linea_programado";
                ddlLineaFiltro.Items.Insert(0, new ListItem("Seleccione un item", "0"));
                ddlLineaFiltro.AppendDataBoundItems = true;
                ddlLineaFiltro.SelectedIndex = 0;
                ddlLineaFiltro.DataBind();

            }

            //EMPRESAS DE RECAUDO
            try
            {
                if (txtCodigoCliente.Text != "")
                    PoblarLista("v_persona_empresa_recaudo", "Distinct cod_empresa, nom_empresa", " cod_persona = " + txtCodigoCliente.Text, "", ddlEmpresa);
                else
                    PoblarLista("empresa_recaudo", ddlEmpresa);
            }
            catch { }

            // LISTADO DE OFICINAS
            Xpinn.Asesores.Data.OficinaData vDatosOficina = new Xpinn.Asesores.Data.OficinaData();
            Xpinn.Asesores.Entities.Oficina pOficina = new Xpinn.Asesores.Entities.Oficina();
            List<Xpinn.Asesores.Entities.Oficina> lstOficina = new List<Xpinn.Asesores.Entities.Oficina>();
            pOficina.Estado = 1;
            lstOficina = vDatosOficina.ListarOficina(pOficina, (Usuario)Session["usuario"]);
            if (lstOficina.Count > 0)
            {
                ddlOficina.DataSource = lstOficina;
                ddlOficina.DataTextField = "NombreOficina";
                ddlOficina.DataValueField = "IdOficina";
                ddlOficina.Items.Insert(0, new ListItem("Seleccione un item", "0"));
                ddlOficina.AppendDataBoundItems = true;
                ddlOficina.SelectedIndex = 0;
                ddlOficina.DataBind();
            }
            else
            {
                VerError("La oficina se encuentra inactiva");
            }

            // MOTIVOS DE APERTURA
            try
            {
                PoblarLista("MOTIVO_PROGRAMADO", ddlMotivoApertura);
            }
            catch { }

            cargarDatosSolicitud();

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentasPrograServicios.GetType().Name + "L", "CargarDropDown", ex);
        }
    }



    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            CuentasProgramado pCuenta = new CuentasProgramado();
            CuentasProgramado pCuenta1 = new CuentasProgramado();
            if (pIdObjeto != null)
            {
                pCuenta = CuentasPrograServicios.ConsultarAhorroProgramado(Convert.ToString(pIdObjeto), (Usuario)Session["usuario"]);

                if (pCuenta.cod_asesor.HasValue)
                {
                    ddlAsesor.SelectedValue = pCuenta.cod_asesor.ToString();
                }

                if (pCuenta.numero_programado != null)
                {
                    if (pCuenta.cod_persona != 0)
                        txtCodigoCliente.Text = pCuenta.cod_persona.ToString();
                    ConsultarPersona(Convert.ToInt64(txtCodigoCliente.Text));
                    if (pCuenta.cod_linea_programado != null)
                        ddlLineaFiltro.SelectedValue = pCuenta.cod_linea_programado;

                    txtCuenta.Text = pCuenta.numero_programado;
                    if (pCuenta.fecha_apertura != DateTime.MinValue)
                        txtFechApertura.Text = pCuenta.fecha_apertura.ToShortDateString();
                    ddlOficina.SelectedValue = pCuenta.cod_oficina.ToString();

                    chkEstado.Checked = pCuenta.estado == 1 ? true : false;
                    chkEstado.Visible = true;
                    if (pCuenta.cod_motivo_apertura != 0)
                        try
                        {
                            ddlMotivoApertura.SelectedValue = pCuenta.cod_motivo_apertura.ToString();
                        }
                        catch { }

                    txtCuota.Text = pCuenta.valor_cuota.ToString();
                    txtPlazo.Text = pCuenta.plazo.ToString();
                    ddlPeriodicidad.SelectedValue = pCuenta.cod_periodicidad.ToString();

                    pCuenta1 = CuentasPrograServicios.ConsultarPeriodicidadProgramado(Convert.ToInt64(ddlPeriodicidad.SelectedValue), (Usuario)Session["usuario"]);
                    Session["tipocalendario"] = pCuenta1.tipo_calendario;
                    Session["dias"] = pCuenta1.numero_dias;

                    txtFechaPrimPago.Text = pCuenta.fecha_primera_cuota.ToShortDateString();

                    ddlFormaPago.SelectedValue = pCuenta.forma_pago.ToString();
                    if (pCuenta.cod_empresa != 0)
                        ddlEmpresa.SelectedValue = pCuenta.cod_empresa.ToString();

                    PoblarLista("v_persona_empresa_recaudo", "Distinct cod_empresa, nom_empresa", "cod_persona = " + txtCodigoCliente.Text, "", ddlEmpresa);

                    ///////TASA

                    //Generar Consulta de la Linea Seleccionada
                    LineasProgramado vLineaAhorro = new LineasProgramado();
                    LineasProgramadoServices lineaServicio = new LineasProgramadoServices();
                    List<LineaProgramado_Rango> Lista_rango = new List<LineaProgramado_Rango>();
                    vLineaAhorro = lineaServicio.ConsultarLineasProgramado(Convert.ToInt64(ddlLineaFiltro.SelectedValue), ref Lista_rango, (Usuario)Session["usuario"]);

                    if (vLineaAhorro.interes_por_cuenta == 0)
                    {
                        cbInteresCuenta.Enabled = false;
                        panelTasa.Enabled = false;
                    }
                    else
                    {
                        cbInteresCuenta.Enabled = true;
                        panelTasa.Enabled = true;
                        cbInteresCuenta.Checked = true;
                    }
                    if (pCuenta.calculo_tasa != null)
                    {
                        if (!string.IsNullOrEmpty(pCuenta.calculo_tasa.ToString()))
                            ctlTasaInteres.FormaTasa = HttpUtility.HtmlDecode(pCuenta.calculo_tasa.ToString().Trim());
                        if (!string.IsNullOrEmpty(pCuenta.tipo_historico.ToString()))
                            ctlTasaInteres.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(pCuenta.tipo_historico.ToString().Trim()));
                        if (!string.IsNullOrEmpty(pCuenta.desviacion.ToString()))
                            ctlTasaInteres.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(pCuenta.desviacion.ToString().Trim()));
                        if (!string.IsNullOrEmpty(pCuenta.cod_tipo_tasa.ToString()))
                            ctlTasaInteres.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(pCuenta.cod_tipo_tasa.ToString().Trim()));
                        if (!string.IsNullOrEmpty(pCuenta.tasa_interes.ToString()))
                            ctlTasaInteres.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(pCuenta.tasa_interes.ToString().Trim()));
                    }


                    ///////

                    if (pCuenta.fecha_interes != DateTime.MinValue)
                        txtFechaInt.Text = pCuenta.fecha_interes.ToShortDateString();
                    if (pCuenta.total_interes != 0)
                        txtTotInteres.Text = pCuenta.total_interes.ToString();
                    if (pCuenta.total_retencion != 0)
                        txtTotRetencion.Text = pCuenta.total_retencion.ToString();

                    if (pCuenta.saldo != 0)
                        txtSaldoTotal.Text = pCuenta.saldo.ToString();
                    if (pCuenta.fecha_proximo_pago != DateTime.MinValue)
                        txtFechaProxPago.Text = pCuenta.fecha_proximo_pago.ToShortDateString();
                    if (pCuenta.fecha_ultimo_pago != DateTime.MinValue)
                        txtFechaUltPago.Text = pCuenta.fecha_ultimo_pago.ToShortDateString();
                    if (pCuenta.fecha_cierre != DateTime.MinValue)
                        txtFecCierre.Text = pCuenta.fecha_cierre.ToShortDateString();

                    txtPagadas.Text = pCuenta.cuotas_pagadas.ToString();

                    if (pCuenta.fecha_vencimiento != DateTime.MinValue)
                        txtFechaVencimiento.Text = pCuenta.fecha_vencimiento.ToShortDateString();

                    //Beneficiarios
                    if (pCuenta.lstBeneficiarios.Count > 0)
                    {
                        gvBeneficiarios.DataSource = pCuenta.lstBeneficiarios;
                        gvBeneficiarios.DataBind();
                        upBeneficiarios.Visible = true;
                    }


                    //CuotasExtras
                    if (pCuenta.lstcuotasExtras.Count > 0)
                    {
                        txtValorCuotaExt.Visible = false;
                        btnGenerarCuotaext.Visible = false;
                        btnLimpiarCuotaext.Visible = false;
                        txtFechaCuotaExt.Visible = false;
                        ddlPeriodicidadCuotaExt.Visible = false;
                        lblValorCuotaExtra.Visible = false;
                        lblFechaPrimerCuotaExtra.Visible = false;
                        lblPeriodicidadCuotaExtra.Visible = false;
                        btnLimpiarCuotaext.Visible = false;
                        ChkCuotasExtras.Enabled = false;
                        lblError.Visible = false;
                        ChkCuotasExtras.Visible = true;
                        ChkCuotasExtras.Checked = true;
                        gvCuoExt.DataSource = pCuenta.lstcuotasExtras;
                        gvCuoExt.DataBind();
                        upCuotasExtras.Visible = true;
                    }




                    lblInteresPagar.Visible = false;
                    txtInteresPagar.Visible = false;
                    txtInteresCapitalizar.Visible = false;
                    lblInteresCapitalizar.Visible = false;

                    Decimal interes = 0;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //Renovacion de Cuentas 
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (Session[CuentasPrograServicios.CodigoPrograma + ".renovacion"] != null)
                    {
                        txtFechApertura.Text = Convert.ToString(DateTime.Now);
                        DeterminarFechaPrimPago();
                        //Campos Ocultar 
                        txtFechaInt.Visible = false;
                        lblFInteres.Visible = false;
                        lbltotalinteres.Visible = false;
                        txtTotInteres.Visible = false;
                        lbltotalretencion.Visible = false;
                        txtTotRetencion.Visible = false;
                        txtFechaProxPago.Visible = false;
                        lblultpago.Visible = false;
                        txtFechaUltPago.Visible = false;
                        lblfechacierre.Visible = false;
                        txtFecCierre.Visible = false;
                        lblpagadas.Visible = false;
                        txtPagadas.Visible = false;
                        lblproxpago.Visible = false;
                        idObjeto = "";
                        if (Session[cuentasProgramado.CodigoPrograma + ".renovacioninteres"] != null)
                        {
                            lblInteresPagar.Visible = true;
                            txtInteresPagar.Visible = true;
                            txtInteresPagar.Text = Convert.ToString(Session[cuentasProgramado.CodigoPrograma + ".renovacioninteres"]);
                            interes = Convert.ToDecimal(Session[cuentasProgramado.CodigoPrograma + ".renovacioninteres"]);
                        }
                        else
                        {
                            lblInteresPagar.Visible = false;
                            txtInteresPagar.Visible = false;
                        }
                        if (Session[cuentasProgramado.CodigoPrograma + ".renovacioninteresliq"] != null)
                        {
                            Decimal saldo = Convert.ToDecimal(Session[cuentasProgramado.CodigoPrograma + ".renovacionsaldo"]);
                            txtSaldoTotal.Text = Convert.ToString(saldo);
                            txtInteresCapitalizar.Visible = true;
                            lblInteresCapitalizar.Visible = true;
                            txtInteresCapitalizar.Text = interes.ToString();
                        }
                        else
                        {
                            Decimal saldo = Convert.ToDecimal(Session[cuentasProgramado.CodigoPrograma + ".renovacionsaldo"]);
                            txtSaldoTotal.Text = Convert.ToString(saldo);
                            txtInteresCapitalizar.Visible = false;
                            lblInteresCapitalizar.Visible = false;
                            txtInteresCapitalizar.Text = 0.ToString();
                        }

                        //Generar Consulta de la Linea Seleccionada
                        LineasProgramado vLineaAhorro2 = new LineasProgramado();
                        vLineaAhorro = lineaServicio.ConsultarLineasProgramado(Convert.ToInt64(ddlLineaFiltro.SelectedValue), ref Lista_rango, (Usuario)Session["usuario"]);

                        if (vLineaAhorro.interes_por_cuenta == 0)
                        {
                            cbInteresCuenta.Enabled = false;
                            panelTasa.Enabled = false;
                        }
                        else
                        {
                            cbInteresCuenta.Enabled = true;
                            panelTasa.Enabled = true;
                            cbInteresCuenta.Checked = true;
                        }
                        if (vLineaAhorro.calculo_tasa_ren != null)
                        {
                            if (!string.IsNullOrEmpty(vLineaAhorro.calculo_tasa_ren.ToString()))
                                ctlTasaInteres.FormaTasa = HttpUtility.HtmlDecode(vLineaAhorro.calculo_tasa_ren.ToString().Trim());
                            if (!string.IsNullOrEmpty(vLineaAhorro.tipo_historico_ren.ToString()))
                                ctlTasaInteres.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(vLineaAhorro.tipo_historico_ren.ToString().Trim()));
                            if (!string.IsNullOrEmpty(vLineaAhorro.desviacion_ren.ToString()))
                                ctlTasaInteres.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(vLineaAhorro.desviacion_ren.ToString().Trim()));
                            if (!string.IsNullOrEmpty(vLineaAhorro.cod_tipo_tasa_ren.ToString()))
                                ctlTasaInteres.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(vLineaAhorro.cod_tipo_tasa_ren.ToString().Trim()));
                            if (!string.IsNullOrEmpty(vLineaAhorro.tasa_interes_ren.ToString()))
                                ctlTasaInteres.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(vLineaAhorro.tasa_interes_ren.ToString().Trim()));
                        }


                        ddlLineaFiltro.Enabled = true;
                        txtCuota.Enabled = true;
                        txtPlazo.Enabled = true;
                        //Numeracion de cuentas
                        Usuario vUsuario = new Usuario();
                        Xpinn.CDATS.Entities.Cdat Data = new Xpinn.CDATS.Entities.Cdat();
                        Data = AperturaService.ConsultarNumeracionCDATS(Data, (Usuario)Session["usuario"]);
                        if (Data.valor == 1)
                        {
                            string pError = "";
                            string autogenerado = BONumeracionCuentaCDAT.ObtenerCodigoParametrizado(2, txtIdentificacion.Text, Convert.ToInt64(txtCodigoCliente.Text), ddlLineaFiltro.SelectedValue, ref pError, vUsuario);
                            if (pError != "")
                            {
                                VerError(pError);
                                return;
                            }
                            if (autogenerado == "ErrorGeneracion")
                            {
                                VerError("Se generó un error al construir el consecutivo CDAT");
                                return;
                            }
                            pCuenta.numero_programado = autogenerado;
                        }

                        else
                        {
                            pCuenta.opcion = 0;
                            pCuenta.numero_programado = txtCuenta.Text;
                        }

                        if (Data.valor == 1)
                        {
                            txtCuenta.Visible = false;
                            lblNumAuto.Visible = true;
                        }
                        else
                        {
                            txtCuenta.Visible = true;
                            lblNumAuto.Visible = false;
                        }
                    }

                }


            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentasPrograServicios.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    protected void InicializarEstructura(String pIdObjeto)
    {
        try
        {
            try
            {
                ddlMotivoApertura.SelectedIndex = 1;
            }
            catch { }
            this.ddlPeriodicidad.SelectedIndex = 1;//Selecciona mensual por defecto    
            Usuario usuap = new Usuario();
            usuap = (Usuario)Session["usuario"];

            //Armar consecutivo de acuerdo a la parametrizacion de numeración
            //ObtenerEstructuraCuenta(pIdObjeto);
            chkEstado.Checked = true;
            txtFechApertura.Text = Convert.ToString(DateTime.Now);
            ddlOficina.SelectedValue = Convert.ToString(usuap.cod_oficina);
            txtFechaProxPago.Visible = false;
            txtSaldoTotal.Visible = false;
            txtFechaUltPago.Visible = false;
            txtFecCierre.Visible = false;
            txtTotInteres.Visible = false;
            txtTotRetencion.Visible = false;
            txtPagadas.Visible = false;
            txtFechaInt.Visible = false;
            lblpagadas.Visible = false;
            lbltotalretencion.Visible = false;
            lbltotalinteres.Visible = false;
            lblFInteres.Visible = false;
            lblultpago.Visible = false;
            lblproxpago.Visible = false;
            lblfechacierre.Visible = false;
            LblSaldo.Visible = false;
            ConsultarDatosAfiliacion();
            //txtFechaPrimPago.Text = Convert.ToString(DateTime.Now.AddMonths(1));

            if (pCuenta.plazo != 0)
                txtPlazo.Text = pCuenta.plazo.ToString();
            txtPlazoMinimo.Text = pCuenta.plazo.ToString();

            if (pCuenta.cuota != 0)
            {
                txtCuota.Text = pCuenta.cuota.ToString();
            }
            txtSaldoMinimo.Text = pCuenta.cuota.ToString();

            ///////TASA

            //Generar Consulta de la Linea Seleccionada
            LineasProgramado vLineaAhorro = new LineasProgramado();
            LineasProgramadoServices lineaServicio = new LineasProgramadoServices();
            List<LineaProgramado_Rango> Lista_rango = new List<LineaProgramado_Rango>();
            vLineaAhorro = lineaServicio.ConsultarLineasProgramado(Convert.ToInt64(ddlLineaFiltro.SelectedValue), ref Lista_rango, (Usuario)Session["usuario"]);

            foreach (LineaProgramado_Rango x in Lista_rango)
            {
                bool validar = false;
                List<LineaProgramado_Requisito> Lista_requisito = new List<LineaProgramado_Requisito>();
                Lista_requisito = lineaServicio.ListarLineasProgramado_Requisito(x.idrango, x.cod_linea_programado, (Usuario)Session["usuario"]);
                foreach (LineaProgramado_Requisito y in Lista_requisito)
                {

                    if (y.tipo_tope == 0)
                    {
                        validar = true;
                        break;
                    }
                    // Fecha de aprobación
                    else if (y.tipo_tope == 1)
                    {
                        if (y.minimo != null && y.maximo != null)
                        {
                            if (txtFechApertura.ToDateTime < Convert.ToDateTime(y.minimo) || txtFechApertura.ToDateTime > Convert.ToDateTime(y.maximo))
                            {
                                validar = true;
                                break;
                            }
                        }

                    }
                    // Plazos
                    else if (y.tipo_tope == 2)
                    {
                        if (y.minimo != null || y.maximo != null)
                        {
                            if (y.minimo == null)
                            {
                                y.minimo = y.maximo;
                            }
                            if (y.maximo == null)
                            {
                                y.maximo = y.minimo;
                            }
                            if (Convert.ToInt64(txtPlazo.Text) < Convert.ToInt64(y.minimo) || Convert.ToInt64(txtPlazo.Text) > Convert.ToInt64(y.maximo))
                            {
                                validar = true;
                                break;
                            }
                        }
                    }
                    //Montos 
                    else if (y.tipo_tope == 3)
                    {
                        if (y.minimo != null || y.maximo != null)
                        {
                            if (y.minimo == null)
                            {
                                y.minimo = y.maximo;
                            }
                            if (y.maximo == null)
                            {
                                y.maximo = y.minimo;
                            }
                            if (Convert.ToInt64(txtSaldoTotal.Text) < Convert.ToInt64(y.minimo) || Convert.ToInt64(txtSaldoTotal.Text) > Convert.ToInt64(y.maximo))
                            {
                                validar = true;
                                break;
                            }
                        }
                    }

                    // Antiguedad
                    else if (y.tipo_tope == 6)
                    {
                        Int32? antiguedad = 0;
                        if (y.minimo != null || y.maximo != null)
                        {
                            DateTime? fecha_afiliacion = null;
                            fecha_afiliacion = AfiliacionServicio.FechaAfiliacion(txtCodigoCliente.Text, (Usuario)Session["usuario"]);
                            if (fecha_afiliacion != null)
                            {
                                if (txtFechApertura.ToDateTime <= fecha_afiliacion)
                                {
                                    antiguedad = FechasServicio.FecDifDia(Convert.ToDateTime(fecha_afiliacion), txtFechApertura.ToDateTime, 1);
                                    antiguedad = Convert.ToInt32(Math.Round(Convert.ToDouble(antiguedad / 30)));
                                }
                            }
                            if ((antiguedad < Convert.ToInt32(y.minimo) && y.minimo != null) || (antiguedad > Convert.ToInt32(y.maximo) && y.maximo != null))
                            {
                                validar = true;
                                break;
                            }
                        }
                    }
                    //salarios minimos legales vigentes

                    else if (y.tipo_tope == 9)
                    {
                        if (y.minimo != null || y.maximo != null)
                        {
                            if (y.minimo == null)
                            {
                                y.minimo = y.maximo;
                            }
                            if (y.maximo == null)
                            {
                                y.maximo = y.minimo;
                            }
                            Int64 smlv = GeneralServicio.SMLVGeneral((Usuario)Session["usuario"]);
                            if (Convert.ToInt64(txtSaldoTotal.Text) < (Convert.ToInt32(y.minimo) * smlv) || Convert.ToInt64(txtSaldoTotal.Text) > (Convert.ToInt32(y.maximo) * smlv))
                            {

                            }
                        }
                    }
                }

                if (validar == true)
                {

                    LineaProgramado_Tasa Linea_tasa = new LineaProgramado_Tasa();
                    Linea_tasa = lineaServicio.ConsultarLineaProgramado_tasa(x.idrango, x.cod_linea_programado, (Usuario)Session["usuario"]);
                    if (!string.IsNullOrEmpty(Linea_tasa.tipo_interes.ToString()))
                        ctlTasaInteres.FormaTasa = HttpUtility.HtmlDecode(Linea_tasa.tipo_interes.ToString().Trim());
                    if (!string.IsNullOrEmpty(Linea_tasa.tipo_historico.ToString()))
                        ctlTasaInteres.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(Linea_tasa.tipo_historico.ToString().Trim()));
                    if (!string.IsNullOrEmpty(Linea_tasa.desviación.ToString()))
                        ctlTasaInteres.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(Linea_tasa.desviación.ToString().Trim()));
                    if (!string.IsNullOrEmpty(Linea_tasa.cod_tipo_tasa.ToString()))
                        ctlTasaInteres.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(Linea_tasa.cod_tipo_tasa.ToString().Trim()));
                    if (!string.IsNullOrEmpty(Linea_tasa.tasa.ToString()))
                        ctlTasaInteres.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(Linea_tasa.tasa.ToString().Trim()));

                    break;
                }
            }

            if (vLineaAhorro.interes_por_cuenta == 0)
            {
                cbInteresCuenta.Enabled = false;
                panelTasa.Enabled = false;
            }
            else
            {
                cbInteresCuenta.Enabled = true;
                panelTasa.Enabled = true;
                cbInteresCuenta.Checked = true;
            }
            //if (vLineaAhorro.calculo_tasa != null)
            //{
            //    if (!string.IsNullOrEmpty(vLineaAhorro.calculo_tasa.ToString()))
            //        ctlTasaInteres.FormaTasa = HttpUtility.HtmlDecode(vLineaAhorro.calculo_tasa.ToString().Trim());
            //    if (!string.IsNullOrEmpty(vLineaAhorro.tipo_historico.ToString()))
            //        ctlTasaInteres.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(vLineaAhorro.tipo_historico.ToString().Trim()));
            //    if (!string.IsNullOrEmpty(vLineaAhorro.desviacion.ToString()))
            //        ctlTasaInteres.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(vLineaAhorro.desviacion.ToString().Trim()));
            //    if (!string.IsNullOrEmpty(vLineaAhorro.cod_tipo_tasa.ToString()))
            //        ctlTasaInteres.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(vLineaAhorro.cod_tipo_tasa.ToString().Trim()));
            //    if (!string.IsNullOrEmpty(vLineaAhorro.tasa_interes.ToString()))
            //        ctlTasaInteres.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(vLineaAhorro.tasa_interes.ToString().Trim()));
            //}

        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentasPrograServicios.GetType().Name + "A", "ObtenerDatos", ex);
        }

    }
    protected void ObtenerEstructuraCuenta(String pIdObjeto)
    {
        String cadena = "";
        Int32 longitud = 0;
        Int64 posicion = 0;
        string alinear = "";
        String valorfijo = "";
        String consecutivo = "";
        Usuario usuap = new Usuario();
        usuap = (Usuario)Session["usuario"];
        String oficina = Convert.ToString(usuap.cod_oficina);
        String identificacion = txtIdentificacion.Text;
        String codpersona = txtCodigoCliente.Text;
        String codlinea = ddlLineaFiltro.SelectedValue;
        Char caracter_llenado;
        try
        {

            //RECUPERAR DATOS 
            List<CuentasProgramado> LstNumeracion = new List<CuentasProgramado>();
            CuentasProgramado pNum = new CuentasProgramado();
            pNum.tipo_producto = Convert.ToInt32(2);
            LstNumeracion = CuentasPrograServicios.ListarParametrizacionCuentas(pNum, (Usuario)Session["usuario"]);
            CuentasProgramado vApert = new CuentasProgramado();

            foreach (CuentasProgramado numeracion in LstNumeracion)
            {
                longitud = numeracion.longitud;
                alinear = numeracion.alinear;
                caracter_llenado = Convert.ToChar(numeracion.caracter_llenado);
                if (numeracion.posicion > 0)
                {
                    if (numeracion.tipo_campo == 0)
                    {
                        //("VALOR_FIJO"
                        valorfijo = Convert.ToString(numeracion.valor);
                        if (numeracion.alinear == "D")
                            cadena = valorfijo.PadLeft(longitud, caracter_llenado);
                        else
                            cadena = valorfijo.PadRight(longitud, caracter_llenado);

                    }

                    //CONSECUTIVO
                    if (numeracion.tipo_campo == 1)
                    {
                        consecutivo = Convert.ToString(numeracion.valor);
                        if (numeracion.alinear == "D")
                            cadena = consecutivo.PadLeft(longitud, caracter_llenado);
                        else
                            cadena = consecutivo.PadRight(longitud, caracter_llenado);


                    }

                    //OFICINA
                    if (numeracion.tipo_campo == 2)
                    {
                        oficina = Convert.ToString(usuap.cod_oficina);
                        if (numeracion.alinear == "D")
                            cadena = oficina.PadLeft(longitud, caracter_llenado);
                        else
                            cadena = oficina.PadRight(longitud, caracter_llenado);

                    }
                    //IDENTIFICACION
                    if (numeracion.tipo_campo == 3)
                    {
                        identificacion = Convert.ToString(identificacion);
                        if (numeracion.alinear == "D")
                            cadena = identificacion.PadLeft(longitud, caracter_llenado);
                        else
                            cadena = identificacion.PadRight(longitud, caracter_llenado);

                    }

                    //CODPERSONA
                    if (numeracion.tipo_campo == 4)
                    {
                        codpersona = Convert.ToString(codpersona);

                        if (numeracion.alinear == "D")
                            cadena = codpersona.PadLeft(longitud, caracter_llenado);
                        else
                            cadena = (codpersona.PadRight(longitud, caracter_llenado));

                    }

                    //CODLINEA
                    if (numeracion.tipo_campo == 5)
                    {
                        codlinea = Convert.ToString(codlinea);

                        if (numeracion.alinear == "D")
                            cadena = codlinea.PadLeft(longitud, caracter_llenado);
                        else
                            cadena = (codlinea.PadRight(longitud, caracter_llenado));
                    }

                }

                cuenta = cuenta + cadena;


            }
            this.txtCuenta.Text = cuenta;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentasPrograServicios.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
    private List<Xpinn.FabricaCreditos.Entities.Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
        return lstDatosSolicitud;
    }


    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
    }


    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        try
        {
            ddlControl.Items.Clear();
            plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
            pentidad.idconsecutivo = null;
            pentidad.descripcion = "Seleccione un item";
            plista.Insert(0, pentidad);
            ddlControl.DataTextField = "descripcion";
            ddlControl.DataValueField = "idconsecutivo";
            ddlControl.DataSource = plista;
            ddlControl.DataBind();
        }
        catch
        { }
    }


    protected Boolean ValidarDatos()
    {
        VerError("");
        if (txtCuenta.Visible == true)
        {
            if (txtCuenta.Text == "")
            {
                VerError("Ingrese el número de  la Cuenta");
                txtCuenta.Focus();
                return false;
            }
        }
        if (ddlOficina.SelectedIndex == 0)
        {
            VerError("Seleccione la Oficina correspondiente");
            ddlOficina.Focus();
            return false;
        }
        if (ddlLineaFiltro.SelectedIndex == 0)
        {
            VerError("Seleccione la linea");
            ddlLineaFiltro.Focus();
            return false;
        }
        if (txtCodigoCliente.Text == "")
        {
            VerError("Seleccione la persona");
            return false;
        }
        if (txtFechApertura.Text == "")
        {
            VerError("Seleccione la fecha de apertura");
            txtFechApertura.Focus();
            return false;
        }
        if (Convert.ToDateTime(txtFechApertura.Text) > DateTime.Now)
        {
            VerError("No puede registrar con fecha de apertura mayor a la fecha actual");
            txtFechApertura.Focus();
            return false;
        }
        if (txtFechaPrimPago.Text == "")
        {
            VerError("Seleccione la fecha de primer pago");
            txtFechaPrimPago.Focus();
            return false;
        }
        if (txtCuota.Text == "0")
        {
            VerError("Ingrese el valor de la cuota");
            txtCuota.Focus();
            return false;
        }
        if (txtPlazo.Text == "")
        {
            VerError("Ingrese el plazo");
            txtPlazo.Focus();
            return false;
        }

        if (ddlPeriodicidad.SelectedIndex == 0)
        {
            VerError("Seleccione la periodicidad");
            ddlPeriodicidad.Focus();
            return false;
        }
        if (txtFechaProxPago.Text == "")
        {
            txtFechaProxPago.Text = txtFechaPrimPago.Text;
        }
        if (Convert.ToDateTime(txtFechaProxPago.Text) < Convert.ToDateTime(txtFechApertura.Text))
        {
            VerError("La fecha de próximo pago no puede ser menor a la fecha de apertura");
            return false;
        }
        //if (Convert.ToDateTime(txtFechaProxPago.Text) < DateTime.Now)
        //{
        //    VerError("La fecha del próximo pago no puede ser menor a la fecha actual");
        //    txtFechaProxPago.Focus();
        //    return false;
        //}


        //if (txtPagadas.Text == "")
        //{
        //    VerError("Ingrese la cantidad de cuotas pagadas");
        //    txtPagadas.Focus();
        //    return false;
        //}
        //if (txtSaldoTotal.Text == "0")
        //{
        //    VerError("Ingrese la saldo total");
        //    txtSaldoTotal.Focus();
        //    return false;
        //}
        //if (txtFecCierre.Text == "")
        //{
        //    VerError("Seleccione la fecha de cierre");
        //    return false;
        //}
        //if (txtFechaInt.Text == "")
        //{
        //    VerError("Seleccione la fecha de interés");
        //    return false;
        //}
        return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            string msj = idObjeto != "" ? "modificación" : "grabación";
            ctlMensaje.MostrarMensaje("Desea realizar la " + msj + " de los registros");
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        Boolean continuar = true;
        Usuario usuap = new Usuario();
        Usuario vUsuario = new Usuario();
        vUsuario = (Usuario)Session["Usuario"];
        usuap = (Usuario)Session["usuario"];
        decimal valor_cuota = 0;
        decimal plazo_cuenta = 0;
        try
        {
            CuentasProgramado pCuenta = new CuentasProgramado();
            pCuenta.cod_persona = Convert.ToInt64(txtCodigoCliente.Text);
            pCuenta.cod_linea_programado = ddlLineaFiltro.SelectedValue;

            //   idObjeto = txtCuenta.Text;
            //if (idObjeto != "")
            //    pCuenta.numero_programado = Convert.ToString(txtCuenta.Text);
            //else
            //    pCuenta.numero_programado = "";

            //CONSULTA DE GENERACION NUMERICA ahorro

            Xpinn.CDATS.Entities.Cdat Data = new Xpinn.CDATS.Entities.Cdat();
            Data = AperturaService.ConsultarNumeracionCDATS(Data, (Usuario)Session["usuario"]);

            if (idObjeto == "")
            {
                if (Data.valor == 1)
                {
                    string pError = "";
                    string autogenerado = BONumeracionCuentaCDAT.ObtenerCodigoParametrizado(2, txtIdentificacion.Text, Convert.ToInt64(txtCodigoCliente.Text), ddlLineaFiltro.SelectedValue, ref pError, vUsuario);
                    if (pError != "")
                    {
                        VerError(pError);
                        return;
                    }
                    if (autogenerado == "ErrorGeneracion")
                    {
                        VerError("Se generó un error al construir el consecutivo CDAT");
                        return;
                    }
                    pCuenta.numero_programado = autogenerado;
                }
                else
                {
                    pCuenta.opcion = 0;//NO AUTOGENERE
                    pCuenta.numero_programado = txtCuenta.Text;
                }
            }
            else
            {
                pCuenta.opcion = 0;
                pCuenta.numero_programado = txtCuenta.Text;
            }


            //   pCuenta.numero_programado = txtCuenta.Text.Trim();
            pCuenta.fecha_apertura = Convert.ToDateTime(txtFechApertura.Text);
            pCuenta.cod_oficina = Convert.ToInt64(ddlOficina.SelectedValue);
            pCuenta.estado = chkEstado.Checked ? 1 : 0;

            if (ddlMotivoApertura.SelectedIndex != 0)
                try
                {
                    pCuenta.cod_motivo_apertura = Convert.ToInt32(ddlMotivoApertura.SelectedValue);
                }
                catch { }
            else
                pCuenta.cod_motivo_apertura = 0;

            pCuenta.valor_cuota = Convert.ToDecimal(txtCuota.Text);
            if (cuota != 0)
            {
                cuota = Convert.ToDecimal(txtSaldoMinimo.Text);
            }
            else
                cuota = 0;
            valor_cuota = Convert.ToInt64(pCuenta.valor_cuota);
            if (valor_cuota < cuota)
            {
                continuar = false;
            }
            if (continuar == false)
            {
                VerError("El valor de la cuota debe ser mayor o igual al saldo mínimo de la linea");
                lblSaldoMinimo.Visible = true;
                txtSaldoMinimo.Visible = true;
            }
            else
            {
                pCuenta.plazo = Convert.ToInt32(txtPlazo.Text);
                if (plazo != 0)
                {
                    plazo = Convert.ToDecimal(txtPlazoMinimo.Text);
                }
                else
                {
                    plazo = 0;
                }
                plazo_cuenta = Convert.ToInt64(pCuenta.plazo);
                if (plazo_cuenta < plazo)
                {
                    continuar = false;
                }


                if (continuar == false)
                {
                    VerError("El plazo debe ser mayor o igual al plazo  mínimo de la linea");

                    lblPlazoMinimo.Visible = true;
                    txtPlazoMinimo.Visible = true;
                }
                else
                {
                    pCuenta.cod_periodicidad = Convert.ToInt64(ddlPeriodicidad.SelectedValue);

                    if (idObjeto == "")
                    {
                        DateTime fechaultcierre = CuentasPrograServicios.verificaFechaServices((Usuario)Session["usuario"]);
                        DateTime Fechavalidar = Convert.ToDateTime(txtFechaPrimPago.Text);
                        if (Fechavalidar < fechaultcierre)
                        {
                            VerError("La fecha de primer pago no puede ser inferior al último cierre");
                            return;
                        }
                    }



                    pCuenta.fecha_primera_cuota = Convert.ToDateTime(txtFechaPrimPago.Text);
                    pCuenta.fecha_proximo_pago = Convert.ToDateTime(txtFechaPrimPago.Text);
                    pCuenta.forma_pago = Convert.ToInt32(ddlFormaPago.SelectedValue);
                    if (panelEmpresa.Visible == true)
                        if (ddlEmpresa.SelectedIndex != 0)
                            pCuenta.cod_empresa = Convert.ToInt32(ddlEmpresa.SelectedValue);
                        else
                            pCuenta.cod_empresa = 0;
                    else
                        pCuenta.cod_empresa = 0;


                    if (txtFechaInt.Text != "")
                        pCuenta.fecha_interes = Convert.ToDateTime(txtFechaInt.Text);
                    else
                        pCuenta.fecha_interes = DateTime.MinValue;

                    if (txtTotInteres.Text != "")
                        pCuenta.total_interes = Convert.ToDecimal(txtTotInteres.Text);
                    else
                        pCuenta.total_interes = 0;

                    if (txtTotRetencion.Text != "")
                        pCuenta.total_retencion = Convert.ToDecimal(txtTotRetencion.Text);
                    else
                        pCuenta.total_retencion = 0;
                    if (txtSaldoTotal.Text != "")
                        pCuenta.saldo = Convert.ToDecimal(txtSaldoTotal.Text);
                    else
                        pCuenta.saldo = 0;

                    if (txtFechaUltPago.Text != "")
                        pCuenta.fecha_ultimo_pago = Convert.ToDateTime(txtFechaUltPago.Text);
                    else
                        pCuenta.fecha_ultimo_pago = DateTime.MinValue;
                    if (txtFecCierre.Text != "")
                        pCuenta.fecha_cierre = Convert.ToDateTime(txtFecCierre.Text);
                    else
                        pCuenta.fecha_cierre = DateTime.MinValue;
                    if (txtPagadas.Text != "")
                        pCuenta.cuotas_pagadas = Convert.ToInt32(txtPagadas.Text);
                    else
                        pCuenta.cuotas_pagadas = 0;

                    pCuenta.calculo_tasa = Convert.ToInt32(ctlTasaInteres.FormaTasa);
                    if (ctlTasaInteres.Indice == 0)//NIGUNA
                    {
                        pCuenta.tipo_historico = 0;
                        pCuenta.desviacion = 0;
                        pCuenta.tasa_interes = 0;
                        pCuenta.cod_tipo_tasa = 0;
                    }
                    else if (ctlTasaInteres.Indice == 1)//FIJO
                    {
                        pCuenta.tipo_historico = 0;
                        pCuenta.desviacion = 0;
                        if (ctlTasaInteres.Tasa != 0)
                            pCuenta.tasa_interes = ctlTasaInteres.Tasa;
                        pCuenta.cod_tipo_tasa = ctlTasaInteres.TipoTasa;
                    }
                    else // HISTORICO
                    {
                        pCuenta.cod_tipo_tasa = 0;
                        pCuenta.tipo_historico = ctlTasaInteres.TipoHistorico;
                        if (ctlTasaInteres.Desviacion != 0)
                            pCuenta.desviacion = ctlTasaInteres.Desviacion;
                    }

                    if (!string.IsNullOrWhiteSpace(ddlAsesor.SelectedValue))
                    {
                        pCuenta.cod_asesor = Convert.ToInt64(ddlAsesor.SelectedValue);
                    }

                    pCuenta.lstBeneficiarios = ObtenerListaBeneficiarios();


                    String estado = "";
                    DateTime fechacierrehistorico;
                    DateTime fechaapertura = Convert.ToDateTime(txtFechApertura.Text);
                    Xpinn.Programado.Entities.CuentasProgramado vAhorroProgramado = new Xpinn.Programado.Entities.CuentasProgramado();
                    vAhorroProgramado = cuentasProgramado.ConsultarCierreAhorroProgramado((Usuario)Session["usuario"]);

                    if (idObjeto != "")
                    {
                        //MODIFICAR 
                        if (txtFechaProxPago.Text != "")
                            pCuenta.fecha_proximo_pago = Convert.ToDateTime(txtFechaProxPago.Text);
                        pCuenta.opcion = 2;
                        CuentasPrograServicios.Crear_ModAhorroProgramado(pCuenta, (Usuario)Session["usuario"], 2);
                        lblMsj.Text = "modificada";
                    }
                    else
                    {

                        if (vAhorroProgramado != null)
                        {
                            estado = vAhorroProgramado.estadocierre;
                            fechacierrehistorico = Convert.ToDateTime(vAhorroProgramado.fecha_cierre.ToString());
                            if (estado == "D" && fechaapertura <= fechacierrehistorico)
                            {
                                VerError("NO PUEDE INGRESAR APERTURAS EN PERIODOS YA CERRADOS, TIPO L,'AH. PROGRAMADO'");
                                return;
                            }
                        }
                        //CREAR
                        pCuenta.opcion = 1;
                        CuentasPrograServicios.Crear_ModAhorroProgramado(pCuenta, (Usuario)Session["usuario"], 1);


                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        // si viene de renovacion para hacer la transaccion de interes o capitalizacion 
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        if (Session[CuentasPrograServicios.CodigoPrograma + ".renovacion"] != null)
                        {
                            Int64 CodigoOperacion = Convert.ToInt64(Session[cuentasProgramado.CodigoPrograma + ".cod_ope"]);

                            cierreCuentaDetalle pCuentaApertura = new cierreCuentaDetalle();
                            pCuentaApertura.NumeroProgramado = pCuenta.numero_programado;
                            pCuentaApertura.cod_ope = CodigoOperacion;
                            pCuentaApertura.Cod_persona = Convert.ToInt64(txtCodigoCliente.Text.ToString());
                            pCuentaApertura.tipo_tran = 350;
                            pCuentaApertura.fecha_Liquida = DateTime.Now;

                            pCuentaApertura.Valor = Convert.ToDecimal(txtSaldoTotal.Text.Replace(".", ""));
                            //Actualizar el estado de la cuenta aperturada
                            pCuenta.opcion = 2;
                            pCuenta.estado = 1;
                            pCuenta.numero_cuenta = pCuenta.numero_programado;
                            CuentasPrograServicios.Crear_ModAhorroProgramado(pCuenta, (Usuario)Session["usuario"], 2);

                            //para la transaccion de apertura del nuevo programado 
                            pCuentaApertura.origen = 1;
                            CuentasPrograServicios.AperturaCuentasServices(pCuentaApertura, (Usuario)Session["usuario"]);

                            //Renovación

                            pCuentaApertura.NumeroProgramado_Renovado = Convert.ToString(Session[cuentasProgramado.CodigoPrograma + ".num_programado_renovar"]);
                            pCuentaApertura.Plazo = Convert.ToInt32(txtPlazo.Text);
                            pCuentaApertura.cod_linea = Convert.ToString(ddlLineaFiltro.SelectedValue);
                            pCuentaApertura.Observacion = "Renovacion Ahorro Programado";
                            pCuentaApertura.Fecha_Vencimiento = Convert.ToDateTime(txtFechaVencimiento.Text);

                            CuentasPrograServicios.CrearRenovacionCuentasServices(pCuentaApertura, (Usuario)Session["usuario"]);


                            /////////////////////////////////////////////////////////////////////////////////////////////
                            // Guardar datos de Cuotas Extras
                            /////////////////////////////////////////////////////////////////////////////////////////////
                            CuotasExtrasService CuoExtServicio = new CuotasExtrasService();
                            List<Xpinn.Programado.Entities.ProgramadoCuotasExtras> lstCuotasExtras = new List<Xpinn.Programado.Entities.ProgramadoCuotasExtras>();
                            foreach (GridViewRow rFila in gvCuoExt.Rows)
                            {
                                Xpinn.Programado.Entities.ProgramadoCuotasExtras vCuotaExtra = new Xpinn.Programado.Entities.ProgramadoCuotasExtras();
                                vCuotaExtra.numero_programado = Convert.ToString(pCuentaApertura.NumeroProgramado);
                                Label lblfechapago = rFila.FindControl("lblfechapago") as Label;
                                if (lblfechapago.Text == "")
                                    break;

                                Label lblvalor = rFila.FindControl("lblvalor") as Label;
                                var valor = lblvalor.Text.Replace("$", "").Replace(gSeparadorMiles, "");
                                vCuotaExtra.fecha_pago = Convert.ToDateTime(lblfechapago.Text);
                                vCuotaExtra.valor = Convert.ToDecimal(valor.Replace(",00", ""));
                                vCuotaExtra.valor_capital = Convert.ToDecimal(valor.Replace(",00", ""));
                                vCuotaExtra.saldo_capital = Convert.ToInt64(lblvalor.Text.Replace("$", "").Replace(gSeparadorMiles, ""));
                                CuoExtServicio.CrearCuotasExtras(vCuotaExtra, (Usuario)Session["usuario"]);
                            }




                            pCuenta.saldo = Convert.ToDecimal(txtSaldoTotal.Text.ToString());
                            if (CodigoOperacion != 0)
                            {

                                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                                Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = CodigoOperacion;
                                Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 46;
                                Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = txtCodigoCliente.Text.ToString(); //"<Colocar Aquí el código de la persona del servicio>"
                                Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                                Session[cuentasProgramado.CodigoPrograma + ".id"] = idObjeto;
                            }
                        }
                        else
                        {


                            /////////////////////////////////////////////////////////////////////////////////////////////
                            // Guardar datos de Cuotas Extras
                            /////////////////////////////////////////////////////////////////////////////////////////////
                            CuotasExtrasService CuoExtServicio = new CuotasExtrasService();
                            List<Xpinn.Programado.Entities.ProgramadoCuotasExtras> lstCuotasExtras = new List<Xpinn.Programado.Entities.ProgramadoCuotasExtras>();
                            foreach (GridViewRow rFila in gvCuoExt.Rows)
                            {
                                Xpinn.Programado.Entities.ProgramadoCuotasExtras vCuotaExtra = new Xpinn.Programado.Entities.ProgramadoCuotasExtras();
                                vCuotaExtra.numero_programado = Convert.ToString(pCuenta.numero_programado);
                                Label lblfechapago = rFila.FindControl("lblfechapago") as Label;
                                if (lblfechapago.Text == "")
                                    break;

                                Label lblvalor = rFila.FindControl("lblvalor") as Label;
                                var valor = lblvalor.Text.Replace("$", "").Replace(gSeparadorMiles, "");
                                vCuotaExtra.fecha_pago = Convert.ToDateTime(lblfechapago.Text);
                                vCuotaExtra.valor = Convert.ToDecimal(valor.Replace(",00", ""));
                                vCuotaExtra.valor_capital = Convert.ToDecimal(valor.Replace(",00", ""));
                                vCuotaExtra.saldo_capital = Convert.ToInt64(lblvalor.Text.Replace("$", "").Replace(gSeparadorMiles, ""));
                                CuoExtServicio.CrearCuotasExtras(vCuotaExtra, (Usuario)Session["usuario"]);
                            }
                            lblMsj.Text = "grabada";

                            Site toolBar = (Site)this.Master;
                            toolBar.MostrarConsultar(false);
                            toolBar.MostrarLimpiar(false);
                            toolBar.MostrarGuardar(false);
                            toolBar.MostrarCancelar(true);

                            Session["tipocalendario"] = null;
                            Session["dias"] = null;

                            lblgenerado.Text = pCuenta.numero_programado;
                            actualizarSolicitud(Convert.ToInt64(txtCodigoCliente.Text));
                            MvPrincipal.ActiveViewIndex = 2;
                            pConsulta.Visible = false;
                        }
                    }
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }


    protected void gvControl_RowEditing(object sender, EventArgs e)
    {
        VerError("");
        if (ddlLineaFiltro.SelectedIndex == 0)
        {
            VerError("Seleccione una Linea de Ahorro programado");
            return;
        }

        ctlBusquedaPersonas.CodigoPrograma = CuentasPrograServicios.CodigoPrograma;
        String Codigo = ctlBusquedaPersonas.gvListado.DataKeys[ctlBusquedaPersonas.gvListado.SelectedRow.RowIndex].Value.ToString();

        if (Codigo != "" && Codigo != null)
        {
            ConsultarPersona(Convert.ToInt64(Codigo));
            txtPlazo.Text = "0";
            txtSaldoTotal.Text = "0";
            InicializarEstructura(idObjeto);
        }

        ddlLineaFiltro.Enabled = false;
        MvPrincipal.ActiveViewIndex = 1;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarLimpiar(true);
        toolBar.MostrarConsultar(false);
        toolBar.MostrarGuardar(true);

        PoblarLista("v_persona_empresa_recaudo", "Distinct cod_empresa, nom_empresa", " cod_persona = " + txtCodigoCliente.Text, "", ddlEmpresa);
    }


    void ConsultarPersona(Int64 Codigo)
    {
        Xpinn.FabricaCreditos.Entities.Persona1 pPersona = new Xpinn.FabricaCreditos.Entities.Persona1();
        Xpinn.FabricaCreditos.Services.Persona1Service ConsultaPersona = new Xpinn.FabricaCreditos.Services.Persona1Service();
        pPersona.seleccionar = "Cod_persona";
        pPersona.noTraerHuella = 1;
        pPersona.cod_persona = Convert.ToInt64(Codigo);

        pPersona = ConsultaPersona.ConsultarPersona1Param(pPersona, (Usuario)Session["usuario"]);

        if (pPersona.cod_persona != 0)
            txtCodigoCliente.Text = pPersona.cod_persona.ToString();
        else
            txtCodigoCliente.Text = "";
        if (pPersona.identificacion != "")
            txtIdentificacion.Text = pPersona.identificacion;
        else
            txtIdentificacion.Text = "";
        if (pPersona.tipo_identificacion != 0)
            ddlTipoIdentifi.SelectedValue = pPersona.tipo_identificacion.ToString();
        else
            ddlTipoIdentifi.SelectedIndex = 0;
        if (pPersona.nombres != "" && pPersona.nombres != null || pPersona.apellidos != "" && pPersona.apellidos != null)
        {
            pPersona.nombres = pPersona.nombres != null ? pPersona.nombres.Trim() : "";
            pPersona.apellidos = pPersona.apellidos != null ? pPersona.apellidos.Trim() : "";
            txtNombres.Text = (pPersona.nombres + " " + pPersona.apellidos).Trim();
        }
        else
            txtNombres.Text = "";

    }

    protected void btnImprime_Click(object sender, EventArgs e)
    {
        mvReporte.Visible = true;
        MvPrincipal.Visible = false;

        mvReporte.ActiveViewIndex = 0;



        // ---------------------------------------------------------------------------------------------------------
        // Pasar datos al reporte
        // ---------------------------------------------------------------------------------------------------------

        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];

        ReportParameter[] param = new ReportParameter[16];
        param[0] = new ReportParameter("entidad", pUsuario.empresa);
        param[1] = new ReportParameter("nit", pUsuario.nitempresa);
        param[2] = new ReportParameter("Oficina", pUsuario.nombre_oficina);
        param[3] = new ReportParameter("NroCuenta", this.txtCuenta.Text);
        param[4] = new ReportParameter("fecha_aper", txtFechApertura.Text);
        param[5] = new ReportParameter("tipoLinea", ddlLineaFiltro.SelectedItem.Text);
        param[6] = new ReportParameter("Periodicidad", ddlPeriodicidad.SelectedItem.Text);
        param[7] = new ReportParameter("Cuota", txtCuota.Text);
        param[8] = new ReportParameter("Moneda", ddlTipoMoneda.SelectedItem.Text);
        param[9] = new ReportParameter("Plazo", txtPlazo.Text);
        param[10] = new ReportParameter("ImagenReport", ImagenReporte());
        param[11] = new ReportParameter("Tasa", "0");
        param[12] = new ReportParameter("fecha_primer_pago", this.txtFechaPrimPago.Text);
        param[13] = new ReportParameter("Identificacion", this.txtIdentificacion.Text);
        param[14] = new ReportParameter("Cliente", this.txtNombres.Text);
        param[15] = new ReportParameter("forma_pago", ddlFormaPago.SelectedItem.Text);
        mvReporte.Visible = true;
        rvReporte.LocalReport.DataSources.Clear();
        rvReporte.LocalReport.EnableExternalImages = true;
        rvReporte.LocalReport.SetParameters(param);
        mvReporte.Visible = true;
        rvReporte.LocalReport.Refresh();
        mvReporte.Visible = true;
    }

    //IMPRIMIR

    void muestraInformeReporte()
    {



    }

    protected void btnDatos_Click(object sender, EventArgs e)
    {

    }
    protected void btnImpresion_Click(object sender, EventArgs e)
    {

        // MOSTRAR REPORTE EN PANTALLA
        Warning[] warnings;
        string[] streamids;
        string mimeType;
        string encoding;
        string extension;
        byte[] bytes = rvReporte.LocalReport.Render("PDF", null, out mimeType,
                       out encoding, out extension, out streamids, out warnings);
        FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output.pdf"),
        FileMode.Create);
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();
        Session["Archivo" + Usuario.codusuario] = Server.MapPath("output.pdf");
        // frmPrint.Visible = true;
        rvReporte.Visible = false;
    }
    protected void cbInteresCuenta_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void ddlLineaFiltro_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Generar Consulta de la Linea Seleccionada
        LineasProgramado vLineaAhorro = new LineasProgramado();
        LineasProgramadoServices lineaServicio = new LineasProgramadoServices();
        List<LineaProgramado_Rango> Lista_rango = new List<LineaProgramado_Rango>();
        if (ddlLineaFiltro.SelectedValue == null || ddlLineaFiltro.SelectedValue == "")
        {
        }
        else
        {
            vLineaAhorro = lineaServicio.ConsultarLineasProgramado(Convert.ToInt64(ddlLineaFiltro.SelectedValue), ref Lista_rango, (Usuario)Session["usuario"]);

            if (vLineaAhorro.interes_por_cuenta == 0)
            {
                cbInteresCuenta.Enabled = false;
                panelTasa.Enabled = false;
            }
            else
            {
                cbInteresCuenta.Enabled = true;
                panelTasa.Enabled = true;
                cbInteresCuenta.Checked = true;


            }
            if (vLineaAhorro.calculo_tasa != null)
            {
                //ctlTasaInteres.Inicializar();
                if (!string.IsNullOrEmpty(vLineaAhorro.calculo_tasa.ToString()))
                    ctlTasaInteres.FormaTasa = HttpUtility.HtmlDecode(vLineaAhorro.calculo_tasa.ToString().Trim());
                if (!string.IsNullOrEmpty(vLineaAhorro.tipo_historico.ToString()))
                    ctlTasaInteres.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(vLineaAhorro.tipo_historico.ToString().Trim()));
                if (!string.IsNullOrEmpty(vLineaAhorro.desviacion.ToString()))
                    ctlTasaInteres.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(vLineaAhorro.desviacion.ToString().Trim()));
                if (!string.IsNullOrEmpty(vLineaAhorro.cod_tipo_tasa.ToString()))
                    ctlTasaInteres.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(vLineaAhorro.cod_tipo_tasa.ToString().Trim()));
                if (!string.IsNullOrEmpty(vLineaAhorro.tasa_interes.ToString()))
                    ctlTasaInteres.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(vLineaAhorro.tasa_interes.ToString().Trim()));

            }


            if (Session[CuentasPrograServicios.CodigoPrograma + ".renovacion"] != null)
            {
                vLineaAhorro = lineaServicio.ConsultarLineasProgramado(Convert.ToInt64(ddlLineaFiltro.SelectedValue), ref Lista_rango, (Usuario)Session["usuario"]);


                if (vLineaAhorro.calculo_tasa_ren != null)
                {
                    //ctlTasaInteres.Inicializar();
                    if (!string.IsNullOrEmpty(vLineaAhorro.calculo_tasa_ren.ToString()))
                        ctlTasaInteres.FormaTasa = HttpUtility.HtmlDecode(vLineaAhorro.calculo_tasa_ren.ToString().Trim());
                    if (!string.IsNullOrEmpty(vLineaAhorro.tipo_historico_ren.ToString()))
                        ctlTasaInteres.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(vLineaAhorro.tipo_historico_ren.ToString().Trim()));
                    if (!string.IsNullOrEmpty(vLineaAhorro.desviacion_ren.ToString()))
                        ctlTasaInteres.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(vLineaAhorro.desviacion_ren.ToString().Trim()));
                    if (!string.IsNullOrEmpty(vLineaAhorro.cod_tipo_tasa_ren.ToString()))
                        ctlTasaInteres.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(vLineaAhorro.cod_tipo_tasa_ren.ToString().Trim()));
                    if (!string.IsNullOrEmpty(vLineaAhorro.tasa_interes_ren.ToString()))
                        ctlTasaInteres.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(vLineaAhorro.tasa_interes_ren.ToString().Trim()));

                }

            }
        }


    }

    protected List<Beneficiario> ObtenerListaBeneficiarios()
    {
        List<Beneficiario> lstBeneficiarios = new List<Beneficiario>();

        foreach (GridViewRow rfila in gvBeneficiarios.Rows)
        {
            Beneficiario eBenef = new Beneficiario();
            HiddenField lblidbeneficiario = (HiddenField)rfila.FindControl("hdIdBeneficiario");
            if (lblidbeneficiario.Value != "")
                eBenef.idbeneficiario = Convert.ToInt64(lblidbeneficiario.Value);

            eBenef.numero_programado = idObjeto;

            DropDownList ddlParentezco = (DropDownList)rfila.FindControl("ddlParentezco");
            if (ddlParentezco.SelectedValue != null || ddlParentezco.SelectedIndex != 0)
                eBenef.parentesco = Convert.ToInt32(ddlParentezco.SelectedValue);

            DropDownList ddlSexo = (DropDownList)rfila.FindControl("ddlsexo");
            if (ddlSexo.SelectedValue != null)
                eBenef.sexo = Convert.ToString(ddlSexo.SelectedValue);

            TextBox txtIdentificacion = (TextBox)rfila.FindControl("txtIdentificacion");
            if (txtIdentificacion != null)
                eBenef.identificacion_ben = Convert.ToString(txtIdentificacion.Text);

            TextBox txtEdadBen = (TextBox)rfila.FindControl("txtEdadBen");
            if (txtEdadBen != null)
            {
                if (txtEdadBen.Text != "")
                {
                    eBenef.edad = Convert.ToInt32(txtEdadBen.Text);
                }
            }
            TextBox txtNombres = (TextBox)rfila.FindControl("txtNombres");
            if (txtNombres != null)
                eBenef.nombre_ben = Convert.ToString(txtNombres.Text);

            fechaeditable txtFechaNacimientoBen = (fechaeditable)rfila.FindControl("txtFechaNacimientoBen");
            if (txtFechaNacimientoBen != null)
                if (txtFechaNacimientoBen.Texto != "")
                    eBenef.fecha_nacimiento_ben = txtFechaNacimientoBen.ToDateTime;
                else
                    eBenef.fecha_nacimiento_ben = null;
            else
                eBenef.fecha_nacimiento_ben = null;
            decimalesGridRow txtPorcentaje = (decimalesGridRow)rfila.FindControl("txtPorcentaje");
            if (txtPorcentaje != null)
                eBenef.porcentaje_ben = Convert.ToDecimal(txtPorcentaje.Text);

            lstBeneficiarios.Add(eBenef);
        }
        return lstBeneficiarios;
    }

    protected string FormatoFecha()
    {
        Configuracion conf = new Configuracion();
        return conf.ObtenerFormatoFecha();
    }

    protected void btnAddRowBeneficio_Click(object sender, EventArgs e)
    {
        //Session["DatosBene"] = null;

        List<Beneficiario> lstBene = new List<Beneficiario>();
        lstBene = ObtenerListaBeneficiarios();

        for (int i = 1; i <= 1; i++)
        {
            Beneficiario eBenef = new Beneficiario();
            eBenef.idbeneficiario = -1;
            eBenef.nombre = "";
            eBenef.identificacion_ben = "";
            eBenef.tipo_identificacion_ben = null;
            eBenef.nombre_ben = "";
            eBenef.fecha_nacimiento_ben = null;
            eBenef.parentesco = null;
            eBenef.porcentaje_ben = null;
            eBenef.edad = null;
            eBenef.sexo = null;
            lstBene.Add(eBenef);
        }

        gvBeneficiarios.DataSource = lstBene;
        gvBeneficiarios.DataBind();

        //if (Session["DatosBene"] != null)
        //{
        //    lstBene = (List<Beneficiario>)Session["DatosBene"];

        //    for (int i = 1; i <= 1; i++)
        //    {
        //        Beneficiario eBenef = new Beneficiario();
        //        eBenef.idbeneficiario = -1;
        //        eBenef.nombre = "";
        //        eBenef.identificacion_ben = "";
        //        eBenef.tipo_identificacion_ben = null;
        //        eBenef.nombre_ben = "";
        //        eBenef.fecha_nacimiento_ben = null;
        //        eBenef.parentesco = null;
        //        eBenef.porcentaje_ben = null;
        //        eBenef.edad = null;
        //        eBenef.sexo = null;
        //        lstBene.Add(eBenef);
        //    }
        //    gvBeneficiarios.DataSource = lstBene;
        //    gvBeneficiarios.DataBind();

        //    Session["DatosBene"] = lstBene;
        //}
        //else
        //{
        //    lstBene = new List<Beneficiario>(); ;

        //    for (int i = 1; i <= 1; i++)
        //    {
        //        Beneficiario eBenef = new Beneficiario();
        //        eBenef.idbeneficiario = -1;
        //        eBenef.nombre = "";
        //        eBenef.identificacion_ben = "";
        //        eBenef.tipo_identificacion_ben = null;
        //        eBenef.nombre_ben = "";
        //        eBenef.fecha_nacimiento_ben = null;
        //        eBenef.parentesco = null;
        //        eBenef.porcentaje_ben = null;
        //        eBenef.edad = null;
        //        eBenef.sexo = null;
        //        lstBene.Add(eBenef);
        //    }
        //    gvBeneficiarios.DataSource = lstBene;
        //    gvBeneficiarios.DataBind();

        //    Session["DatosBene"] = lstBene;
        //}
    }

    protected void chkBeneficiario_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkbeneficiario = (CheckBox)sender;
        upBeneficiarios.Visible = chkbeneficiario.Checked;
    }

    protected void gvBeneficiarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            List<Beneficiario> LstBene = ObtenerListaBeneficiarios();
            int IdBeneficiario = Convert.ToInt32(gvBeneficiarios.DataKeys[e.RowIndex].Values[0].ToString());

            if (IdBeneficiario > 0)
            {
                BeneficiarioServicio.EliminarBeneficiarioAhorroProgramado(IdBeneficiario, (Usuario)Session["usuario"]);
            }

            LstBene.RemoveAt((gvBeneficiarios.PageIndex * gvBeneficiarios.PageSize) + e.RowIndex);

            gvBeneficiarios.DataSource = LstBene;
            gvBeneficiarios.DataBind();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }

    protected void gvBeneficiarios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlSexo = (DropDownList)e.Row.FindControl("ddlsexo");
            DropDownList ddlParentezco = (DropDownList)e.Row.FindControl("ddlParentezco");
            if (ddlParentezco != null)
            {
                Beneficiario Ben = new Beneficiario();
                ddlParentezco.DataSource = BeneficiarioServicio.ListarParentesco(Ben, (Usuario)Session["usuario"]);
                ddlParentezco.Items.Insert(0, new ListItem("<Seleccione un item>", "0"));
                ddlParentezco.DataBind();
            }
        }
    }


    protected void txtPlazo_TextChanged(object sender, EventArgs e)
    {

        if (txtPlazo.Text.Trim() == "")
        {
            txtFechaVencimiento.Text = "";
            return;
        }

        VerError("");

        LineasProgramadoServices lineaServicio = new LineasProgramadoServices();
        LineaProgramado_Tasa Linea_tasa = new LineaProgramado_Tasa();
        LineaProgramado_Requisito Linea_requisito = new LineaProgramado_Requisito();
        List<LineaProgramado_Rango> lstRangos = new List<LineaProgramado_Rango>();
        LineaProgramado_Rango LineaRango = new LineaProgramado_Rango();

        Int64 plazo = Convert.ToInt64(txtPlazo.Text);
        Int64 idrango = plazo;
        Int64 idrangolinea = 0;

        if (txtPlazo.Text != null)
        {

            Linea_requisito = lineaServicio.ConsultarLineaProgramadoRango(idrango, ddlLineaFiltro.SelectedValue, (Usuario)Session["usuario"]);
            if (Linea_requisito.idrango == 0)
            {
                txtPlazo.Text = "";
                VerError("El plazo indicado no corresponde con los rangos establecidos para la línea");
            }
            else
            {
                idrangolinea = Linea_requisito.idrango;


                Linea_tasa = lineaServicio.ConsultarLineaProgramado_tasa(idrangolinea, ddlLineaFiltro.SelectedValue, (Usuario)Session["usuario"]);
                if (!string.IsNullOrEmpty(Linea_tasa.tipo_interes.ToString()))
                    ctlTasaInteres.FormaTasa = HttpUtility.HtmlDecode(Linea_tasa.tipo_interes.ToString().Trim());
                if (!string.IsNullOrEmpty(Linea_tasa.tipo_historico.ToString()))
                    ctlTasaInteres.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(Linea_tasa.tipo_historico.ToString().Trim()));
                if (!string.IsNullOrEmpty(Linea_tasa.desviación.ToString()))
                    ctlTasaInteres.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(Linea_tasa.desviación.ToString().Trim()));
                if (!string.IsNullOrEmpty(Linea_tasa.cod_tipo_tasa.ToString()))
                    ctlTasaInteres.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(Linea_tasa.cod_tipo_tasa.ToString().Trim()));
                if (!string.IsNullOrEmpty(Linea_tasa.tasa.ToString()))
                    ctlTasaInteres.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(Linea_tasa.tasa.ToString().Trim()));

                if (!string.IsNullOrWhiteSpace(txtPlazo.Text) && !string.IsNullOrWhiteSpace(txtFechaPrimPago.Text))
                {
                    DateTime fechavencimiento = DateTime.Now;
                    Int32 plazopro = Convert.ToInt32(txtPlazo.Text) - 1;
                    Xpinn.Comun.Services.GeneralService generalServicio = new GeneralService();
                    Xpinn.Comun.Entities.General _general = new Xpinn.Comun.Entities.General();
                    _general = generalServicio.ConsultarGeneral(97, (Usuario)Session["Usuario"]);
                    if (_general != null)
                        if (_general.valor == "1")
                            plazopro += ConvertirStringToInt32(_general.valor);
                    Int32 numdiascalen = Convert.ToInt32(Session["dias"]);
                    plazopro = plazopro * numdiascalen;
                    Int32 tipocalendario = Convert.ToInt32(Session["tipocalendario"]);
                    DateTime fecha_proximo_pago = Convert.ToDateTime(txtFechaPrimPago.Text.ToString());
                    fechavencimiento = AperturaService.Calcularfecha(fechavencimiento, fecha_proximo_pago, plazopro, tipocalendario);
                    txtFechaVencimiento.Text = Convert.ToString(fechavencimiento);
                }


                if (Session[CuentasPrograServicios.CodigoPrograma + ".renovacion"] != null)
                {
                    LineasProgramado vLineaAhorro = new LineasProgramado();
                    List<LineaProgramado_Rango> Lista_rango = new List<LineaProgramado_Rango>();
                    vLineaAhorro = lineaServicio.ConsultarLineasProgramado(Convert.ToInt64(ddlLineaFiltro.SelectedValue), ref Lista_rango, (Usuario)Session["usuario"]);

                    if (vLineaAhorro.calculo_tasa_ren != null)
                    {
                        if (!string.IsNullOrEmpty(vLineaAhorro.calculo_tasa_ren.ToString()))
                            ctlTasaInteres.FormaTasa = HttpUtility.HtmlDecode(vLineaAhorro.calculo_tasa_ren.ToString().Trim());
                        if (!string.IsNullOrEmpty(vLineaAhorro.tipo_historico_ren.ToString()))
                            ctlTasaInteres.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(vLineaAhorro.tipo_historico_ren.ToString().Trim()));
                        if (!string.IsNullOrEmpty(vLineaAhorro.desviacion_ren.ToString()))
                            ctlTasaInteres.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(vLineaAhorro.desviacion_ren.ToString().Trim()));
                        if (!string.IsNullOrEmpty(vLineaAhorro.cod_tipo_tasa_ren.ToString()))
                            ctlTasaInteres.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(vLineaAhorro.cod_tipo_tasa_ren.ToString().Trim()));
                        if (!string.IsNullOrEmpty(vLineaAhorro.tasa_interes_ren.ToString()))
                            ctlTasaInteres.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(vLineaAhorro.tasa_interes_ren.ToString().Trim()));
                    }

                }
            }
        }
    }

    protected void ddlPeriodicidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        CuentasProgramado pCuenta = new CuentasProgramado();
        if (ddlPeriodicidad.SelectedValue != null)
        {
            pCuenta = CuentasPrograServicios.ConsultarPeriodicidadProgramado(Convert.ToInt64(ddlPeriodicidad.SelectedValue), (Usuario)Session["usuario"]);
            Session["tipocalendario"] = pCuenta.tipo_calendario;
            Session["dias"] = pCuenta.numero_dias;
            DeterminarFechaPrimPago();
        }
        txtPlazo_TextChanged(txtPlazo.Text, null);
    }

    protected void txtFechaPrimPago_TextChanged(object sender, EventArgs e)
    {
        txtPlazo_TextChanged(txtPlazo.Text, null);
    }

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        DeterminarFechaPrimPago();
    }

    protected void txtFechApertura_TextChanged(object sender, EventArgs e)
    {
        DeterminarFechaPrimPago();
    }

    public void cargarDatosSolicitud()
    {
        //Creo la lista
        List<CuentaHabientes> lstDetalle = new List<CuentaHabientes>();
        int EnteTerri = 0;
        //Si tiene datos de solicitud los carga primero
        if (Session["solicitudProducto"] != null)
        {
            AhorroVista solicitud = new AhorroVista();
            solicitud = Session["solicitudProducto"] as AhorroVista;
            VerError("");
            ddlLineaFiltro.SelectedValue = solicitud.cod_linea_ahorro;

            String Codigo = Convert.ToString(solicitud.cod_persona);
            if (Codigo != "" && Codigo != null)
            {
                ConsultarPersona(Convert.ToInt64(Codigo));
                txtPlazo.Text = "0";
                txtSaldoTotal.Text = "0";
                InicializarEstructura(idObjeto);
            }

            ddlLineaFiltro.Enabled = false;
            MvPrincipal.ActiveViewIndex = 1;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarLimpiar(true);
            toolBar.MostrarConsultar(false);
            toolBar.MostrarGuardar(true);

            PoblarLista("v_persona_empresa_recaudo", "Distinct cod_empresa, nom_empresa", " cod_persona = " + txtCodigoCliente.Text, "", ddlEmpresa);

            //Carga el resto de datos de la solicitud
            txtPlazo.Text = Convert.ToString(solicitud.num_cuotas);
            txtCuota.Text = Convert.ToString(solicitud.valor_cuota).Replace(".", "");
            ddlFormaPago.SelectedValue = Convert.ToString(solicitud.cod_forma_pago);
            txtPlazo_TextChanged(new object(), new EventArgs());

        }
    }    

    protected void ChkCuotasExtras_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkCuotasExtras = (CheckBox)sender;
        upCuotasExtras.Visible = ChkCuotasExtras.Checked;
    }

    protected void btnLimpiarCuotaext_Click(object sender, EventArgs e)
    {
        List<Xpinn.Programado.Entities.ProgramadoCuotasExtras> lstCuoExt = new List<Xpinn.Programado.Entities.ProgramadoCuotasExtras>();
        gvCuoExt.DataSource = lstCuoExt;
        gvCuoExt.DataBind();
        Session["CuoExt"] = lstCuoExt;
        InicialCuoExt();
    }
    protected void InicialCuoExt()
    {
        List<Xpinn.Programado.Entities.ProgramadoCuotasExtras> lstConsulta = new List<Xpinn.Programado.Entities.ProgramadoCuotasExtras>();
        Xpinn.Programado.Entities.ProgramadoCuotasExtras eCuoExt = new Xpinn.Programado.Entities.ProgramadoCuotasExtras();
        lstConsulta.Add(eCuoExt);
        Session["CuoExt"] = lstConsulta;
        gvCuoExt.DataSource = lstConsulta;
        gvCuoExt.DataBind();
        gvCuoExt.Visible = true;
    }


    protected void btnGenerarCuotaext_Click(object sender, EventArgs e)
    {
        List<Xpinn.Programado.Entities.ProgramadoCuotasExtras> lstCuoExt = new List<Xpinn.Programado.Entities.ProgramadoCuotasExtras>();
        if (txtFechaCuotaExt.Text == "")
        {
            lblError.Text = "Debe digitar la fecha inicial de cuota extra";
            return;
        }
        if (txtValorCuotaExt.Text == "")
        {
            lblError.Text = "Debe digitar el valor de cuota extra";
            return;
        }
        if (txtFechaVencimiento.Text == "")
        {
            lblError.Text = "No se tiene la fecha de vencimiento";
            return;
        }
        Xpinn.Comun.Services.ListaDeplegableService _servicioComun = new Xpinn.Comun.Services.ListaDeplegableService();
        List<Xpinn.Comun.Entities.ListaDesplegable> lstPeriodicidad = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        lstPeriodicidad = _servicioComun.ListarPeriodicidad(ddlPeriodicidadCuotaExt.SelectedValue, (Usuario)Session["usuario"]);
        int valor_diasPeriodicidad = 0;
        if (lstPeriodicidad.Count > 0)
            valor_diasPeriodicidad = Convert.ToInt32(lstPeriodicidad[0].idconsecutivo);
        if (valor_diasPeriodicidad <= 0)
        {
            lblError.Text = "No se pudo extablecer los días de periodicidad de las cuotas extras";
            return;
        }

        int plazo = Convert.ToInt32(txtPlazo.Text);
        lblError.Text = "";
        decimal total = 0;
        DateTime fechaExtra = Convert.ToDateTime(txtFechaCuotaExt.Text);
        DateTime fechaVencimiento = Convert.ToDateTime(txtFechaVencimiento.Text);
        while (fechaExtra <= fechaVencimiento && valor_diasPeriodicidad > 0)
        {
            Xpinn.Programado.Entities.ProgramadoCuotasExtras gItemNew = new Xpinn.Programado.Entities.ProgramadoCuotasExtras();
            gItemNew.fecha_pago = fechaExtra;
            gItemNew.valor = Convert.ToInt64(txtValorCuotaExt.Text);
            lstCuoExt.Add(gItemNew);
            total = total + Convert.ToDecimal(gItemNew.valor);
            Xpinn.Comun.Services.FechasService fechaServicio = new FechasService();
            fechaExtra = fechaServicio.FecSumDia(fechaExtra, valor_diasPeriodicidad, 1);
        }
        Session["CuoExt"] = lstCuoExt;
        gvCuoExt.DataSource = lstCuoExt;
        gvCuoExt.DataBind();
    }

    protected void gvCuoExt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvCuoExt.PageIndex = e.NewPageIndex;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentasPrograServicios.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void gvCuoExt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtfechapago = (TextBox)gvCuoExt.FooterRow.FindControl("txtfechapago");
            TextBox txtvalor = (TextBox)gvCuoExt.FooterRow.FindControl("txtvalor");

            List<Xpinn.Programado.Entities.ProgramadoCuotasExtras> lstCuoExt = new List<Xpinn.Programado.Entities.ProgramadoCuotasExtras>();
            lstCuoExt = (List<Xpinn.Programado.Entities.ProgramadoCuotasExtras>)Session["CuoExt"];

            if (lstCuoExt.Count == 1)
            {
                Xpinn.Programado.Entities.ProgramadoCuotasExtras gItem = new Xpinn.Programado.Entities.ProgramadoCuotasExtras();
                gItem = lstCuoExt[0];
                if (gItem.valor == 0 || gItem.valor == null)
                    lstCuoExt.Remove(gItem);
            }

            Xpinn.Programado.Entities.ProgramadoCuotasExtras gItemNew = new Xpinn.Programado.Entities.ProgramadoCuotasExtras();
            if (txtfechapago.Text.Trim() == "" || txtvalor.Text.Trim() == "")
            {
                return;
            }
            gItemNew.fecha_pago = Convert.ToDateTime(txtfechapago.Text);
            gItemNew.valor = Convert.ToInt64(txtvalor.Text);
            lstCuoExt.Add(gItemNew);
            gvCuoExt.DataSource = lstCuoExt;
            gvCuoExt.DataBind();
            Session["CuoExt"] = lstCuoExt;
        }
    }

    protected void gvCuoExt_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<Xpinn.Programado.Entities.ProgramadoCuotasExtras> lstCuoExt = new List<Xpinn.Programado.Entities.ProgramadoCuotasExtras>();
        lstCuoExt = (List<Xpinn.Programado.Entities.ProgramadoCuotasExtras>)Session["CuoExt"];
        if (lstCuoExt.Count >= 1)
        {
            Xpinn.Programado.Entities.ProgramadoCuotasExtras eCuoExt = new Xpinn.Programado.Entities.ProgramadoCuotasExtras();
            int index = Convert.ToInt32(e.RowIndex);
            eCuoExt = lstCuoExt[index];
            if (eCuoExt.valor != 0 || eCuoExt.valor == null)
            {
                lstCuoExt.Remove(eCuoExt);
            }
        }
        Session["CuoExt"] = lstCuoExt;
        if (lstCuoExt.Count == 0)
        {
            InicialCuoExt();
            gvCuoExt.DataSource = lstCuoExt;
            gvCuoExt.DataBind();
        }
        else
        {
            gvCuoExt.DataSource = lstCuoExt;
            gvCuoExt.DataBind();
        }
    }

    public void actualizarSolicitud(long cod_persona)
    {
        if (Session["solicitudProducto"] != null)
        {
            AhorroVista solicitud = Session["solicitudProducto"] as AhorroVista;
            solicitud.estado_modificacion = "1"; // aprobando solicitud                    
            _ahorroService.ModificarEstadoSolicitudProducto(solicitud, (Usuario)Session["usuario"]);
            Session["solicitudProducto"] = null;

            Xpinn.Comun.Services.Formato_NotificacionService COServices = new Xpinn.Comun.Services.Formato_NotificacionService();
            Xpinn.Comun.Entities.Formato_Notificacion noti = new Xpinn.Comun.Entities.Formato_Notificacion(Convert.ToInt32(cod_persona), 17, "nombreProducto;Ahorro programado");
            COServices.SendEmailPerson(noti, (Usuario)Session["usuario"]);
        }
    }
}