using System;
using System.Diagnostics;
using System.Web;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Configuration;
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
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Reflection;
using System.Web.UI;
using AjaxControlToolkit;
using Xpinn.Asesores.Entities;
using Xpinn.Seguridad.Entities;

partial class Nuevo : GlobalWeb
{
    #region Datos

    String ListaSolicitada = null;
    CreditoService CreditoServicio = new CreditoService();
    private Persona1Service Persona1Servicio = new Persona1Service();
    private List<Persona1> lstDatosSolicitud = new List<Persona1>(); //Lista de los menus desplegables    
    private CreditoRecogerService creditoRecogerServicio = new CreditoRecogerService();
    PeriodicidadService periodicidadService = new PeriodicidadService();
    private Xpinn.Seguridad.Services.PerfilService perfilServicio = new Xpinn.Seguridad.Services.PerfilService();
    Usuario usuap = new Usuario();
    private CamposPermiso campos = new CamposPermiso();
    private int number = 0;

    #endregion


    #region Carga Inicial

    /// <summary>
    /// Mostrar la barra de herramientas al ingresar a la funcionalidad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[CreditoServicio.CodigoProgramaModifi + ".id"] != null)
                VisualizarOpciones(CreditoServicio.CodigoProgramaModifi, "E");
            else
                VisualizarOpciones(CreditoServicio.CodigoProgramaModifi, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            Usuario usu = (Usuario)Session["Usuario"];
            if (usu.administrador == "1")
                toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaModifi, "Page_PreInit", ex);
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
                bool bHabilitar = true;
                txtDiasAjusteNuevo.Enabled = bHabilitar;
                txtValorCuotaNuevo.Enabled = bHabilitar;
                txtFechaSolicitudNuevo.Enabled = bHabilitar;
                txtFechaAprobacionNuevo.Enabled = bHabilitar;
                txtFechaDesembolsoNuevo.Enabled = bHabilitar;
                txtfechaInicio.Enabled = bHabilitar;
                gvDeducciones.Enabled = bHabilitar;
                Session["Numero_Radicacion"] = null;

                // Permite modificar cierta información cuando es perfil SUPER USUARIO.
                if ( /*Usuario.codperfil == 1 */
                    Usuario.identificacion == "XPINNADM" || Usuario.identificacion == "EXPINNADM")
                {
                    lblFecPrimerPago.Visible = true;
                    txtFecPrimerPago.Visible = true;
                    txtFecPrimerPago.Enabled = true;
                    txtSaldoCapitalNuevo.Enabled = true;
                }

                Session[Usuario.codusuario + "Codeudores"] = null;
                // Determinar si es superusuario para modificar saldo de capital del crédito
                if (Session[CreditoServicio.CodigoProgramaModifi + ".id"] != null)
                {
                    idObjeto = Session[CreditoServicio.CodigoProgramaModifi + ".id"].ToString();
                    datosAvance(idObjeto);
                    gvCuotasExtras.TablaCuoExt(idObjeto);
                    ObtenerDatos(idObjeto);
                    mvCredito.ActiveViewIndex = 0;
                }
            }

            campos.CodPerfl = Convert.ToInt32(Usuario.codperfil);
            List<CamposPermiso> lstcampos = perfilServicio.ConsultarCamposPerfil(campos, Usuario);
            FieldInfo[] propiedades = Page.GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            foreach (CamposPermiso permiso in lstcampos)
            {
                var lista = propiedades.Where(x => x.Name.ToLower().Contains(permiso.Campo.ToLower())).ToList();
                foreach (var item in lista)
                {
                    try
                    {
                        var value = (Control)item.GetValue(this);
                        ValidarDatos(value, lista.Count == 1 ? item.Name.Substring(0, 2) : item.Name.Substring(0, 3), Convert.ToBoolean(permiso.Visible),
                            Convert.ToBoolean(permiso.Editable));
                    }
                    catch (Exception exception)
                    {
                        continue;
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
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaModifi, "Page_Load", ex);
        }
    }

    #endregion


    #region Obtencion de Datos

    /// <summary>
    /// Mostrar los datos del crédito
    /// </summary>
    /// <param name="pIdObjeto"></param>
    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Configuracion conf = new Configuracion();
            Credito vCredito = new Credito();

            vCredito = CreditoServicio.ConsultarCreditoModSolicitud(Convert.ToInt64(pIdObjeto),
                (Usuario)Session["usuario"]);
            Session["Numero_Radicacion"] = vCredito.numero_radicacion;

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
            if (vCredito.fecha_aprobacion != DateTime.MinValue)
                txtFechaAprobacion.Text = HttpUtility.HtmlDecode(Convert.ToDateTime(vCredito.fecha_aprobacion)
                    .ToString(GlobalWeb.gFormatoFecha).Trim());
            if (vCredito.fecha_ultimo_pago != DateTime.MinValue)
                txtFechaUltimoPago.Text =
                    HttpUtility.HtmlDecode(vCredito.fecha_ultimo_pago.ToString(GlobalWeb.gFormatoFecha).Trim());
            if (vCredito.fecha_prox_pago != DateTime.MinValue)
                txtFechaProximoPago.Text =
                    HttpUtility.HtmlDecode(vCredito.fecha_prox_pago.ToString(GlobalWeb.gFormatoFecha).Trim());
            if (vCredito.cuotas_pagadas != Int32.MinValue)
                txtCuotasPagas.Text = vCredito.cuotas_pagadas.ToString();
            if (vCredito.fecha_vencimiento != DateTime.MinValue)
                txtFecVencimiento.Text =
                    HttpUtility.HtmlDecode(vCredito.fecha_vencimiento.ToString(GlobalWeb.gFormatoFecha).Trim());
            if (vCredito.fecha_inicio != DateTime.MinValue)
                txtfechaInicio.Text =
                    HttpUtility.HtmlDecode(vCredito.fecha_inicio.ToString(GlobalWeb.gFormatoFecha).Trim());

            // Datos para modificar
            if (vCredito.saldo_capital != Int64.MinValue)
                txtSaldoCapitalNuevo.Text = HttpUtility.HtmlDecode(vCredito.saldo_capital.ToString().Trim());
            if (vCredito.valor_cuota != Int64.MinValue)
                txtValorCuotaNuevo.Text = HttpUtility.HtmlDecode(vCredito.valor_cuota.ToString().Trim());
            if (vCredito.dias_ajuste != Int64.MinValue)
                txtDiasAjusteNuevo.Text = HttpUtility.HtmlDecode(vCredito.dias_ajuste.ToString().Trim());
            if (vCredito.fecha_solicitud != DateTime.MinValue)
                txtFechaSolicitudNuevo.Text = HttpUtility.HtmlDecode(Convert.ToDateTime(vCredito.fecha_solicitud)
                    .ToString(GlobalWeb.gFormatoFecha).Trim());
            if (vCredito.fecha_aprobacion != DateTime.MinValue)
                txtFechaAprobacionNuevo.Text = HttpUtility.HtmlDecode(Convert.ToDateTime(vCredito.fecha_aprobacion)
                    .ToString(GlobalWeb.gFormatoFecha).Trim());
            if (vCredito.fecha_desembolso != DateTime.MinValue)
                txtFechaDesembolsoNuevo.Text =
                    HttpUtility.HtmlDecode(vCredito.fecha_desembolso.ToString(GlobalWeb.gFormatoFecha).Trim());
            if (vCredito.fecha_prim_pago != null)
                txtFecPrimerPago.Text = Convert.ToDateTime(vCredito.fecha_prim_pago).ToString(gFormatoFecha);

            //Llenar Codeudores 
            gvCodeudores.TablaCodeudores();

            //Mostrar datos de Pendientes de Pago, llenar lista global si hay registros
            Xpinn.FabricaCreditos.Services.AmortizaCreService AmortizarService =
                new Xpinn.FabricaCreditos.Services.AmortizaCreService();
            Xpinn.FabricaCreditos.Entities.AmortizaCre amortiza = new AmortizaCre();
            amortiza.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);

            // Si el metodo me devuelve null me instancia la clase para evitar errores ya que el metodo devuelve null si hay alguna ex;
            List<AmortizaCre> lstAmortizarCre =
                AmortizarService.ListarAmortizaCre(amortiza, (Usuario)Session["usuario"]) ?? new List<AmortizaCre>();
            int eAmorCount = lstAmortizarCre.Count();

            if (eAmorCount == 0)
            {
                lstAmortizarCre.Add(amortiza);
                LabelInformacion.Text = "No hay pagos pendientes";
            }
            else
            {
                LabelInformacion.Text = "Numero de registros encontrados: " + eAmorCount.ToString();
            }

            //  Cargar datos en la grilla Pago Pendientes y ordenar por fecha e ID
            gvPagosPendientes.DataSource = lstAmortizarCre.OrderByDescending(cc => cc.fecha_cuota)
                .ThenByDescending(cc => cc.idamortiza).ToList();
            gvPagosPendientes.DataBind();

            //  Cargar datos en la grilla Deducciones
            if (vCredito.lstDescuentosCredito.Count > 0)
            {
                gvDeducciones.DataSource = vCredito.lstDescuentosCredito;
                gvDeducciones.DataBind();
            }
            else
            {
                gvDeducciones.Visible = false;
                lblAtrDesc.Visible = false;
            }


            List<LineasCredito> lstAtributos = ConsultarAtributosFinanciados(txtNumero_radicacion.Text);

            if (lstAtributos == null) return;
            if (lstAtributos.Count > 0)
            {
                gvAtributos.DataSource = lstAtributos;
                gvAtributos.DataBind();
            }
            else
            {
                gvAtributos.Visible = false;
                txtTittleAtr.Visible = false;
            }

            LlenarVariables();
        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaModifi, "ObtenerDatos", ex);
        }
    }

    private List<LineasCredito> ConsultarAtributosFinanciados(string numeroRadicacion)
    {
        try
        {
            List<LineasCredito> lstAtributos =
                CreditoServicio.ListarAtributosFinanciados(Convert.ToInt64(numeroRadicacion),
                    (Usuario)Session["usuario"]);
            return lstAtributos;
        }
        catch (Exception ex)
        {
            VerError("Error al consultar atributos financiados, " + ex.Message);
            return null;
        }
    }

    protected void ddlFormaCalculo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListGrid ddlFormaCalculo = (DropDownListGrid)sender;
        int nItem = Convert.ToInt32(ddlFormaCalculo.CommandArgument);
        if (ddlFormaCalculo != null)
        {
            DropDownList ddltipotasa = (DropDownList)gvAtributos.Rows[nItem].FindControl("ddltipotasa");
            DropDownList ddlAtributos = (DropDownList)gvAtributos.Rows[nItem].FindControl("ddlAtributos");
            DropDownList ddlTipoHistorico = (DropDownList)gvAtributos.Rows[nItem].FindControl("ddlTipoHistorico");
            TextBox txtDesviacion = (TextBox)gvAtributos.Rows[nItem].FindControl("txtDesviacion");

            TextBox txttasa = (TextBox)gvAtributos.Rows[nItem].FindControl("txttasa");

            if (ddlFormaCalculo.SelectedValue == "1")
            {
                txttasa.Enabled = true;
                ddltipotasa.Enabled = true;
                txtDesviacion.Enabled = false;
                ddlTipoHistorico.Enabled = false;
            }
            else
            {
                txttasa.Enabled = false;
                ddltipotasa.Enabled = false;
                txtDesviacion.Enabled = true;
                ddlTipoHistorico.Enabled = true;
                txttasa.Text = "";
                ddltipotasa.SelectedValue = "0";
            }
        }
    }


    protected void gvAtributos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LineasCreditoService Atributosservicio = new LineasCreditoService();
            HistoricoTasaService HistoricoServicio = new HistoricoTasaService();
            TipoTasaService TasaServicio = new TipoTasaService();

            DropDownList ddlAtributos = (DropDownList)e.Row.FindControl("ddlAtributos");
            if (ddlAtributos != null)
            {
                Atributos atributo = new Atributos();
                ddlAtributos.DataSource = Atributosservicio.ListarAtributos(atributo, (Usuario)Session["usuario"]);
                ddlAtributos.DataTextField = "descripcion";
                ddlAtributos.DataValueField = "cod_atr";
                ddlAtributos.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
                ddlAtributos.SelectedIndex = 0;
                ddlAtributos.DataBind();
            }

            Label lblcodatributo = (Label)e.Row.FindControl("lblcodatributo");
            if (lblcodatributo.Text != "")
                ddlAtributos.SelectedValue = lblcodatributo.Text;

            DropDownList ddlFormaCalculo = (DropDownList)e.Row.FindControl("ddlFormaCalculo");
            if (ddlFormaCalculo != null)
            {
                ddlFormaCalculo.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
                ddlFormaCalculo.Items.Insert(1, new ListItem("Tasa Fija", "1"));
                ddlFormaCalculo.Items.Insert(2, new ListItem("Ponderada", "2"));
                ddlFormaCalculo.Items.Insert(3, new ListItem("Tasa Historico", "3"));
                ddlFormaCalculo.Items.Insert(4, new ListItem("Promedio Historico", "4"));
                ddlFormaCalculo.Items.Insert(5, new ListItem("Tasa Variable", "5"));
                ddlFormaCalculo.SelectedIndex = 0;
                ddlFormaCalculo.DataBind();
            }

            Label lblformacalculo = (Label)e.Row.FindControl("lblformacalculo");
            if (lblformacalculo != null)
                ddlFormaCalculo.SelectedValue = lblformacalculo.Text;

            DropDownList ddlTipoHistorico = (DropDownList)e.Row.FindControl("ddlTipoHistorico");
            if (ddlTipoHistorico != null)
            {
                TipoTasaHist tasahistorica = new TipoTasaHist();
                ddlTipoHistorico.DataSource = HistoricoServicio.tipohistorico((Usuario)Session["usuario"]);
                ddlTipoHistorico.DataTextField = "DESCRIPCION";
                ddlTipoHistorico.DataValueField = "TIPODEHISTORICO";
                ddlTipoHistorico.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
                ddlTipoHistorico.SelectedIndex = 0;
                ddlTipoHistorico.DataBind();
            }

            Label lbltipohistorico = (Label)e.Row.FindControl("lbltipohistorico");
            if (lbltipohistorico.Text != "")
                ddlTipoHistorico.SelectedValue = lbltipohistorico.Text;

            DropDownList ddltipotasa = (DropDownList)e.Row.FindControl("ddltipotasa");
            if (ddltipotasa != null)
            {
                TipoTasa tasa = new TipoTasa();
                ddltipotasa.DataSource = TasaServicio.ListarTipoTasa(tasa, (Usuario)Session["usuario"]);
                ddltipotasa.DataTextField = "NOMBRE";
                ddltipotasa.DataValueField = "COD_TIPO_TASA";
                ddltipotasa.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
                ddltipotasa.SelectedIndex = 0;
                ddltipotasa.DataBind();
            }

            Label lbltipotasa = (Label)e.Row.FindControl("lbltipotasa");
            if (lbltipotasa.Text != "")
                ddltipotasa.SelectedValue = lbltipotasa.Text;

            usuap = (Usuario)Session["usuario"];
            if (idObjeto != "")
            {
                int resultado = creditoRecogerServicio.ConsultaPermisoModificarTasa(Convert.ToString(usuap.codusuario),
                    (Usuario)Session["Usuario"]);

                TextBox txtDesviacion = (TextBox)e.Row.FindControl("txtDesviacion");
                TextBox txttasa = (TextBox)e.Row.FindControl("txttasa");
                if (resultado == 1)
                {
                    txttasa.Enabled = true;
                    ddltipotasa.Enabled = true;
                    txtDesviacion.Enabled = true;
                    ddlTipoHistorico.Enabled = true;
                }
                else
                {
                    txttasa.Enabled = false;
                    ddltipotasa.Enabled = false;
                    txtDesviacion.Enabled = false;
                    ddlTipoHistorico.Enabled = false;
                }
            }
        }
    }

    protected void datosAvance(String pIdObjeto)
    {
        List<CuotasExtras> lstCuotasExtras = new List<CuotasExtras>();
        Credito vCredito = new Credito();
        List<ConsultaAvance> ListaDetalleAvance = new List<ConsultaAvance>();
        Xpinn.Asesores.Services.DetalleProductoService DetalleProducto =
            new Xpinn.Asesores.Services.DetalleProductoService();
        Xpinn.FabricaCreditos.Entities.DatosSolicitud datosSolicitud =
            new Xpinn.FabricaCreditos.Entities.DatosSolicitud();
        Credito codigo = null;
        try
        {
            //verificacion de linea
            vCredito = CreditoServicio.ConsultarCreditoModSolicitud(Convert.ToInt64(pIdObjeto),
                (Usuario)Session["usuario"]);
            if (vCredito.Tipo_Linea != 2)
            {
                panelCuotasExtras.Visible = true;
            }
            else
            {
                ListaDetalleAvance =
                    DetalleProducto.ListarAvances(Convert.ToInt64(pIdObjeto), (Usuario)Session["Usuario"]);
                Session["DetalleAvancess"] = ListaDetalleAvance;
                gvAvances.DataSource = ListaDetalleAvance;
                gvAvances.DataBind();
                mpePopUp.Visible = true;
                Panl1.Visible = true;
            }
        }
        catch (Exception e)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaModifi, "ObtenerDatos", e);
        }
    }

    private void TraerResultadosListas()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = Persona1Servicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
    }



    #endregion


    #region Evento Botones

    protected void ActualizarDetalle()
    {
        List<ConsultaAvance> ListaDetalleAvance = new List<ConsultaAvance>();
        ListaDetalleAvance = (List<ConsultaAvance>)Session["DetalleAvancess"];
        gvAvances.DataSource = ListaDetalleAvance;
        gvAvances.DataBind();
    }

    protected void RegsPag_SelectedIndexChanged(object sender, EventArgs e)
    {
        //  ObtenerDetalleCompOptimizado(false);
        DropDownList _DropDownList = (DropDownList)sender;
        this.gvAvances.PageSize = int.Parse(_DropDownList.SelectedValue);
        ActualizarDetalle();
        return;
    }

    protected void BtnGuardarDatos_Click(object sender, EventArgs e)
    {
        Xpinn.Asesores.Services.DetalleProductoService DetalleProducto =
            new Xpinn.Asesores.Services.DetalleProductoService();
        ConsultaAvance avance = new ConsultaAvance();
        AvanceService AvancServices = new AvanceService();
        foreach (GridViewRow row2 in gvAvances.Rows)
        {

            CheckBoxGrid chkAvanceeRow = (CheckBoxGrid)row2.FindControl("modifique");
            if (chkAvanceeRow.Checked)
            {

                var s = row2.Cells[0];
                avance.NumAvance = Convert.ToInt64(row2.Cells[0].Text);
                avance.Estado = Convert.ToString(((DropDownList)row2.FindControl("stEstado")).Text);
                avance.Plazo = Convert.ToInt64(((TextBox)row2.FindControl("Plazo")).Text);
                avance.Tasa = Convert.ToDecimal(((TextBox)row2.FindControl("Tasa")).Text);
                avance.FechaProxPago = Convert.ToDateTime(((TextBox)row2.FindControl("FeProxpago")).Text);
                avance.CuotasPendi = Convert.ToInt64(((TextBox)row2.FindControl("txtCuPendientes")).Text);
                avance.CuotasPagadas = Convert.ToInt64(((TextBox)row2.FindControl("txtCuPagadas")).Text);
                avance.FechaUltiPago = Convert.ToDateTime(((TextBox)row2.FindControl("FeUlPago")).Text);
                DetalleProducto.ModificarAvances(avance, (Usuario)Session["usuario"]);
            }
        }

        if (sender != null && e != null)
        {
            Mensajegrabar1.MostrarMensaje("Datos Modificados");
            Navegar(Pagina.Nuevo);
        }

    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        // lblErrorCod.Text = string.Empty;
        if (ValidarDatos())
            ctlMensaje.MostrarMensaje("Desea realizar la modificación?");
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {

        try
        {
            gvCuotasExtras.GuardarCuotas(txtNumero_radicacion.Text);
            Credito vCredito = new Credito();
            vCredito.lstAmortizaCre = new List<AmortizaCre>();
            // Cargar datos a modificar del crédito
            vCredito.fecha_solicitud = Convert.ToDateTime(txtFechaSolicitudNuevo.Text);
            vCredito.fecha_aprobacion = Convert.ToDateTime(txtFechaAprobacionNuevo.Text);
            vCredito.fecha_desembolso = Convert.ToDateTime(txtFechaDesembolsoNuevo.Text);
            vCredito.fecha_prox_pago = Convert.ToDateTime(txtFechaProximoPago.Text);
            vCredito.cuotas_pagadas = Convert.ToInt64(txtCuotasPagas.Text);
            vCredito.fecha_vencimiento = Convert.ToDateTime(txtFecVencimiento.Text);
            vCredito.fecha_inicio = Convert.ToDateTime(this.txtfechaInicio.Text);
            vCredito.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);
            vCredito.saldo_capital = Convert.ToDecimal(txtSaldoCapitalNuevo.Text.Replace(gSeparadorMiles, "")
                .Replace(".", gSeparadorDecimal));
            vCredito.valor_cuota =
                Convert.ToDecimal(txtValorCuotaNuevo.Text.Replace(gSeparadorMiles, "").Replace(".", gSeparadorDecimal));
            vCredito.dias_ajuste = Convert.ToInt64(txtDiasAjusteNuevo.Text);
            vCredito.fecha_prim_pago = Convert.ToDateTime(txtFecPrimerPago.Text);

            // Cargar atributos descontados/financiados a modificar del crédito
            vCredito.lstDescuentosCredito = new List<DescuentosCredito>();
            foreach (GridViewRow gFila in gvDeducciones.Rows)
            {
                try
                {
                    DescuentosCredito eDescuento = new DescuentosCredito();
                    eDescuento.numero_radicacion = vCredito.numero_radicacion;
                    eDescuento.cod_atr = ValorSeleccionado((Label)gFila.FindControl("lblCodAtr"));
                    eDescuento.nom_atr = Convert.ToString(gFila.Cells[1].ToString());
                    eDescuento.tipo_descuento = ValorSeleccionado((DropDownList)gFila.FindControl("ddlTipoDescuento"));
                    eDescuento.tipo_liquidacion =
                        ValorSeleccionado((DropDownList)gFila.FindControl("ddlTipoLiquidacion"));
                    eDescuento.forma_descuento =
                        Convert.ToInt32(ValorSeleccionado((DropDownList)gFila.FindControl("ddlFormaDescuento")));
                    eDescuento.val_atr = ValorSeleccionado((TextBox)gFila.FindControl("txtvalor"));
                    eDescuento.numero_cuotas = ValorSeleccionado((TextBox)gFila.FindControl("txtnumerocuotas"));
                    eDescuento.cobra_mora = ValorSeleccionado((CheckBox)gFila.FindControl("cbCobraMora"));
                    vCredito.lstDescuentosCredito.Add(eDescuento);
                }
                catch (Exception ex)
                {
                    VerError(
                        "Se presentaron errores al cargar atributos descontados/financiados a modificar del crédito. Error:" +
                        ex.Message);
                    return;
                }
            }

            // Cargar datos Pagos Pendientes, si alguno esta incorrecto returna la ejecución y notifica
            string error = string.Empty;
            foreach (GridViewRow gFila in gvPagosPendientes.Rows)
            {
                AmortizaCre eAmor = new AmortizaCre();
                eAmor.idamortiza = Convert.ToInt64(((Label)gFila.FindControl("lblidamortiza")).Text);
                eAmor.fecha_cuota = Convert.ToDateTime(((fecha)gFila.FindControl("txtFechaPagoPendiente")).Texto);
                eAmor.cod_atr = ValorSeleccionado((DropDownList)gFila.FindControl("ddlCodigoAtributo"));
                eAmor.valor = ValorSeleccionado((TextBox)gFila.FindControl("txtValorPago"));
                eAmor.saldo = ValorSeleccionado((TextBox)gFila.FindControl("txtSaldoPago"));
                eAmor.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);

                // Out variable para recoger el error e informar
                if (ValidarCamposEntidadAmor(eAmor, out error))
                {
                    // Ignora Rows completamente vacias,
                    // ValidarCamposEntidadAmor las deja pasar y aqui se filran
                    if (eAmor.fecha_cuota != DateTime.MinValue)
                    {
                        vCredito.lstAmortizaCre.Add(eAmor);
                    }
                }
                else
                {
                    VerError("Se presentaron errores al cargar los pagos pendientes a modificar del crédito. " + error);
                    return;
                }
            }

            // Modifica el credito OJO A veces puede Crear en vez de modificar
            // con otras entidades como AmortizaCre
            vCredito = CreditoServicio.ModificarCreditoUlt(vCredito, (Usuario)Session["usuario"]);

            List<AtributosCredito> lstAtributosCreditos = LlenarAtributosFinanciadosParaGuardar();

            foreach (var atributo in lstAtributosCreditos)
            {
                CreditoServicio.ModificarAtributosFinanciados(atributo, (Usuario)Session["usuario"]);
            }

            mvCredito.ActiveViewIndex = 1;
            Session.Remove(Usuario.codusuario + "Codeudores");

            //mostrar botones despues de dar Guardar 
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
            toolBar.MostrarConsultar(true);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    private List<AtributosCredito> LlenarAtributosFinanciadosParaGuardar()
    {
        List<AtributosCredito> lstAtributosCreditos = new List<AtributosCredito>();

        foreach (GridViewRow row in gvAtributos.Rows)
        {
            string tipoHistorico = (row.FindControl("lbltipohistorico") as Label).Text;
            string desviacion = (row.FindControl("txtDesviacion") as TextBox).Text;
            string tipoTasa = (row.FindControl("lbltipotasa") as Label).Text;
            string tasa = (row.FindControl("txttasa") as TextBox).Text;


            AtributosCredito atributo = new AtributosCredito();
            atributo.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);
            atributo.cod_atr = Convert.ToInt32((row.FindControl("lblcodatributo") as Label).Text);
            atributo.calculo_atr = (row.FindControl("lblformacalculo") as Label).Text;
            atributo.tipo_historico = !string.IsNullOrWhiteSpace(tipoHistorico)
                ? Convert.ToInt32(tipoHistorico)
                : default(int?);
            atributo.desviacion = !string.IsNullOrWhiteSpace(desviacion)
                ? Convert.ToDecimal(desviacion)
                : default(decimal?);
            atributo.tipo_tasa = !string.IsNullOrWhiteSpace(tipoTasa) ? Convert.ToInt32(tipoTasa) : default(int?);
            atributo.tasa = !string.IsNullOrWhiteSpace(tasa) ? Convert.ToDecimal(tasa) : default(decimal?);
            atributo.cobra_mora = Convert.ToInt32((row.FindControl("chkCobramora") as CheckBox).Checked);

            lstAtributosCreditos.Add(atributo);

        }

        return lstAtributosCreditos;
    }

    protected void btnAgregarFilaPagoPendiente_Click(object sender, EventArgs e)
    {
        ConcurrentBag<AmortizaCre> lstAmortizaConcurrent = new ConcurrentBag<AmortizaCre>();
        AmortizaCre eAmor = new AmortizaCre();
        eAmor.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);
        bool hayFilasYaAgregadas = false;

        // Particiono el foreach con la Paralell Api para que cada thread colabore con el recorrido y tenga mas perfomance
        Parallel.ForEach(gvPagosPendientes.Rows.Cast<GridViewRow>(), gFila =>
        {
            long idamortiza = Convert.ToInt64(((Label)gFila.FindControl("lblidamortiza")).Text);

            if (idamortiza == 0)
            {
                hayFilasYaAgregadas = true;
            }

            AmortizaCre eAmorFor = new AmortizaCre();
            eAmorFor.idamortiza = idamortiza;
            eAmorFor.fecha_cuota = ((fecha)gFila.FindControl("txtFechaPagoPendiente")).ToDateTime;
            eAmorFor.cod_atr = ValorSeleccionado((DropDownList)gFila.FindControl("ddlCodigoAtributo"));
            eAmorFor.valor = ValorSeleccionado((TextBox)gFila.FindControl("txtValorPago"));
            eAmorFor.saldo = ValorSeleccionado((TextBox)gFila.FindControl("txtSaldoPago"));
            eAmorFor.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);
            lstAmortizaConcurrent.Add(eAmorFor);
        });

        // Ordeno la lista por fecha y luego por id ya que el Paralell.Foreach devuelve la lista desordenadas por los tiempo de los threads
        List<AmortizaCre> lstAmortiza = lstAmortizaConcurrent.OrderByDescending(cc => cc.fecha_cuota)
            .ThenByDescending(cc => cc.idamortiza).ToList();

        // Busco todas las filas de ID "0", las remuevo y coloco de primero
        if (hayFilasYaAgregadas)
        {
            List<AmortizaCre> ultimasFilaAgregadaParaLlenar = lstAmortiza.FindAll(cc => cc.idamortiza == 0);
            lstAmortiza.RemoveAll(cc => cc.idamortiza == 0);
            lstAmortiza.InsertRange(0, ultimasFilaAgregadaParaLlenar);
        }

        // Inserto la nueva row
        lstAmortiza.Insert(0, eAmor);

        // Actualizo la grilla
        gvPagosPendientes.DataSource = lstAmortiza;
        gvPagosPendientes.DataBind();
        LabelInformacion.Text = "Numero de registros actuales: " + lstAmortizaConcurrent.Count().ToString();
    }

    /// <summary>
    /// Evento para cancelar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }



    protected void btnGuardar_OnClick(object sender, EventArgs e)
    {

    }

    #endregion


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        idObjeto = Session[CreditoServicio.CodigoProgramaModifi + ".id"].ToString();
        try
        {
            gvAvances.PageIndex = e.NewPageIndex;
            datosAvance(idObjeto);
        }
        catch (Exception ex)
        {
            //
        }
    }

    #region Eventos Varios gvPagosPendientes

    protected void OnRowCommandDeleting(object sender, GridViewCommandEventArgs e)
    {
        ConcurrentBag<AmortizaCre> lstAmortizaConcurrent = new ConcurrentBag<AmortizaCre>();
        AmortizaCreService amortizServicio = new AmortizaCreService();
        GridViewRow row = gvPagosPendientes.Rows[Convert.ToInt32(e.CommandArgument)];
        long idBorrar = Convert.ToInt64(((Label)row.FindControl("lblidamortiza")).Text);

        // Particiono el foreach con la Paralell Api para que cada thread colabore con el recorrido y tenga mas perfomance
        Parallel.ForEach(gvPagosPendientes.Rows.Cast<GridViewRow>(), gFila =>
        {
            long idamortiza = Convert.ToInt64(((Label)gFila.FindControl("lblidamortiza")).Text);

            // Si encuentro la filla a borrar, la ignoro evitando agregarla para luego borrarla
            if (idamortiza == idBorrar)
            {
                return;
            }

            AmortizaCre eAmor = new AmortizaCre();
            eAmor.idamortiza = idamortiza;
            eAmor.fecha_cuota = ((fecha)gFila.FindControl("txtFechaPagoPendiente")).ToDateTime;
            eAmor.cod_atr = ValorSeleccionado((DropDownList)gFila.FindControl("ddlCodigoAtributo"));
            eAmor.valor = ValorSeleccionado((TextBox)gFila.FindControl("txtValorPago"));
            eAmor.saldo = ValorSeleccionado((TextBox)gFila.FindControl("txtSaldoPago"));
            eAmor.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);
            lstAmortizaConcurrent.Add(eAmor);
        });

        // Ordeno la lista por fecha y luego por ID
        gvPagosPendientes.DataSource = lstAmortizaConcurrent.OrderByDescending(cc => cc.fecha_cuota)
            .ThenByDescending(cc => cc.idamortiza).ToList();
        gvPagosPendientes.DataBind();
        LabelInformacion.Text = "Numero de registros actuales: " + lstAmortizaConcurrent.Count().ToString();

        //  Si tiene ID 0 es que es una fila agregada por el usuario y no esta en la BD
        if (idBorrar != 0)
        {
            amortizServicio.EliminarAmortizaCre(idBorrar, (Usuario)Session["Usuario"]);
        }
    }

    // Es necesario este evento vacio para que pueda borrar la Row
    protected void gvPagosPendientes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    #endregion



    #region Metodos Validaciones


    private bool ValidarCamposEntidadAmor(AmortizaCre eAmor, out string error)
    {
        error = string.Empty;

        // Si hay una fila totalmente vacia, la deja pasar y la ignora luego en Data al grabar
        if (eAmor.valor == null && eAmor.saldo == null && eAmor.fecha_cuota == DateTime.MinValue)
        {
            return true;
        }

        if (eAmor.valor == null && eAmor.saldo == null)
        {
            error = "Hay campos sin valores en valor y saldo";
            return false;
        }

        if (eAmor.valor == null)
        {
            error = "Hay campos sin valores en valor";
            return false;
        }

        if (eAmor.saldo == null)
        {
            error = "Hay campos sin valores en saldo";
            return false;
        }

        if (eAmor.fecha_cuota == DateTime.MinValue)
        {
            error = "Hay campos sin una fecha valida";
            return false;
        }

        if (eAmor.valor < eAmor.saldo)
        {
            error = "Hay campos con saldo mayor al valor";
            return false;
        }

        return true;
    }

    protected Boolean ValidarDatos()
    {
        if (txtFechaProximoPago.Text == "")
        {
            VerError("Ingrese la fecha del próximo pago");
            return false;
        }

        if (txtCuotasPagas.Text == "")
        {
            VerError("Ingrese las cuotas pagas del Crédito");
            return false;
        }

        if (txtFecVencimiento.Text == "")
        {
            VerError("Ingrese la fecha de termino del Crédito");
            return false;
        }

        if (txtFechaSolicitudNuevo.Text == "")
        {
            VerError("Ingrese la fecha de solicitud del Crédito");
            return false;
        }

        if (txtSaldoCapitalNuevo.Text == "")
        {
            VerError("Ingrese el saldo de capital del crédito");
            return false;
        }

        if (txtValorCuotaNuevo.Text == "")
        {
            VerError("Ingrese el valor de la cuota del crédito");
            return false;
        }

        // ADICION DE VALIDACION DE LAS FECHAS YA QUE AL SER CREDITOS DESEMBOLSADOS DEBERIAN TENERLOS
        if (txtFechaAprobacionNuevo.Text == "")
        {
            VerError("Ingrese la fecha de aprobación del Crédito");
            return false;
        }

        if (txtfechaInicio.Text == "")
        {
            VerError("Ingrese la fecha de inicio del Crédito");
            return false;
        }

        if (txtFechaAprobacionNuevo.Text != "")
        {
            if (txtFechaAprobacionNuevo.TieneDatos)
            {
                if (txtFechaAprobacionNuevo.ToDateTime < txtFechaSolicitudNuevo.ToDateTime)
                {
                    VerError("La fecha de aprobación no puede ser anterior a la fecha de solicitud");
                    return false;
                }
            }
        }

        if (txtFechaDesembolsoNuevo.Text != "")
        {
            if (txtFechaDesembolsoNuevo.TieneDatos)
            {
                if (txtFechaDesembolsoNuevo.ToDateTime < txtFechaSolicitudNuevo.ToDateTime)
                {
                    VerError("La fecha de desembolso no puede ser anterior a la fecha de solicitud");
                    return false;
                }
            }
        }

        if (txtFecPrimerPago.Text == "")
        {
            VerError("La fecha del primer pago del crédito no es válida");
            return false;
        }

        // SI ES SUPER USUARIO SE VERIFICA QUE SE INGRESE CORRECTAMENTE EL CAMPO DE PRIMER PAGO.
        if (Usuario.codperfil == 1)
        {
            if (txtFecPrimerPago.TieneDatos)
            {
                if (txtFecPrimerPago.ToDateTime < txtFechaDesembolsoNuevo.ToDateTime)
                {
                    VerError("La fecha del primer pago no puede ser anterior a la fecha del desembolso");
                    return false;
                }
            }

            if (txtfechaInicio.TieneDatos)
            {
                if (txtFecPrimerPago.ToDateTime < txtfechaInicio.ToDateTime)
                {
                    VerError("La fecha del primer pago no puede ser anterior a la fecha de inicio");
                    return false;
                }
            }

            if (txtFecVencimiento.TieneDatos)
            {
                if (txtFecPrimerPago.ToDateTime > txtFecVencimiento.ToDateTime)
                {
                    VerError("La fecha del primer pago no puede ser superior a la fecha de terminación");
                    return false;
                }
            }

            if (txtFechaProximoPago.TieneDatos)
            {
                if (txtFecPrimerPago.ToDateTime > txtFechaProximoPago.ToDateTime)
                {
                    VerError("La fecha del primer pago no puede ser superior a la fecha del próximo pago");
                    return false;
                }
            }
        }

        return true;
    }

    #endregion


    #region Metodos Listar Valores Para Un Control

    protected List<Xpinn.Comun.Entities.ListasFijas> ListarTiposdeDecuento()
    {
        GlobalWeb glob = new GlobalWeb();
        return glob.ListaCreditoTipoDeDescuento();
    }

    protected List<Xpinn.Comun.Entities.ListasFijas> ListaCreditoTipoDeLiquidacion()
    {
        GlobalWeb glob = new GlobalWeb();
        return glob.ListaCreditoTipoDeLiquidacion();
    }

    protected List<Xpinn.Comun.Entities.ListasFijas> ListaCreditoFormadeDescuento()
    {
        GlobalWeb glob = new GlobalWeb();
        return glob.ListaCreditoFormadeDescuento();
    }

    protected List<LineasCredito> ListaImpuestos()
    {
        LineasCreditoService LineasCreditoServicio = new LineasCreditoService();
        List<LineasCredito> lstTipoImpuestos = new List<LineasCredito>();
        lstTipoImpuestos = LineasCreditoServicio.ddlimpuestos((Usuario)Session["Usuario"]);
        return lstTipoImpuestos;
    }

    protected List<Xpinn.FabricaCreditos.Entities.Atributos> ListaAtributos()
    {
        List<Xpinn.FabricaCreditos.Entities.Atributos> lstAtributos =
            new List<Xpinn.FabricaCreditos.Entities.Atributos>();
        Xpinn.FabricaCreditos.Services.LineasCreditoService lineasServicio = new LineasCreditoService();
        return lineasServicio.ListarAtributos(new Xpinn.FabricaCreditos.Entities.Atributos(),
            (Usuario)Session["Usuario"]);
    }

    void LlenarVariables()
    {
        gvCuotasExtras.Monto = txtMonto.Text;
        gvCuotasExtras.Periodicidad = periodicidadService.ListarPeriodicidad(null, (Usuario)Session["Usuario"])
            .FirstOrDefault(x => x.Descripcion.Trim() == txtPeriodicidad.Text.Trim()).numero_dias.ToString();
        gvCuotasExtras.PlazoTxt = txtPlazo.Text;
    }

    void ValidarDatos(Control value, string siglas, bool visible, bool modifica)
    {
        if (value is Label)
        {
            Label lbl = (Label)value;
            lbl.Visible = visible;
        }
        if (value is TextBox)
        {
            TextBox txt = (TextBox)value;
            txt.Visible = visible;
            txt.Enabled = modifica;
            if (!modifica)
            {
                var script = @"$('#" + txt.ClientID + @"').attr('readonly', 'readonly');";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "popup" + number.ToString(), script, true);

            }
            number++;
        }
        if (value is GridView)
        {
            GridView gv = (GridView)value;
            gv.Visible = visible;

            foreach (GridViewRow gridViewRow in gv.Rows)
            {
                for (int i = 0; i < gridViewRow.Cells.Count; i++)
                {
                    gridViewRow.Cells[i].Enabled = modifica;
                }
            }

        }
        if (value is decimales)
        {
            decimales decimales = (decimales)value;
            decimales.Visible = visible;
            if (!modifica)
            {
                var script = @"$('#" + decimales.Controls[1].ClientID + @"').attr('readonly', 'readonly');";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "popup" + number.ToString(), script, true);

            }
            number++;

        }
        if (value is fecha)
        {
            fecha fecha = (fecha)value;
            fecha.Visible = visible;
            if (!modifica)
            {
                var script = @"$('#" + fecha.Controls[0].ClientID + @"').attr('readonly', 'readonly');";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "popup" + number.ToString(), script, true);
                CalendarExtender calentCalendar = (CalendarExtender)fecha.Controls[2];
                calentCalendar.EnabledOnClient = false;

            }
            number++;
        }
    }



    #endregion


    #region Metodos Obtener Valor Seleccionado De Un Control

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

    #endregion


}