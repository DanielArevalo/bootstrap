using System;
using System.Web;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Web.UI;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Services;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Xpinn.Comun.Entities;


partial class Nuevo : GlobalWeb
{
    private String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    RefinanciacionService RefinanciacionServicio = new RefinanciacionService();
    private Persona1Service DatosClienteServicio = new Persona1Service();
    private List<Persona1> lstDatosSolicitud = new List<Persona1>();  //Lista de los menus desplegables    
    private DatosSolicitudService DatosSolicitudServicio = new DatosSolicitudService();
    Credito CreditoServicio = new Credito();
    CuotasExtrasService CuoExtServicio = new CuotasExtrasService();

    /// <summary>
    /// Mostrar la barra de herramientas al ingresar a la funcionalidad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[RefinanciacionServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(RefinanciacionServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(RefinanciacionServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
            txtAbono.eventoCambiar += txtAbono_TextChanged;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RefinanciacionServicio.CodigoPrograma, "Page_PreInit", ex);
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
                SetActiveView(0);
                Session["TotalCuoExt"] = 0;
                DropPeriodicidad();
                DropReestructuracion();
                if (Session[RefinanciacionServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[RefinanciacionServicio.CodigoPrograma + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                    CtrCuotasExtras.TablaCuoExt(idObjeto, 1);
                    mvRefinanciacion.ActiveViewIndex = 0;
                    txtFechaRefinanciacion.ToDateTime = System.DateTime.Now;
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RefinanciacionServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    void DropReestructuracion()
    {
        LlenarListasDesplegables(TipoLista.LineasCreditoReestructurado, ddlLineaReestruc);

        var lstLineasCredito = ddlLineaReestruc.DataSource as List<Persona1>;
        if (lstLineasCredito == null) return;

        if (lstLineasCredito.Count > 0)
        {
            pnlReestruc.Visible = true;
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");

        // // //if (int.Parse(txtPlazo.Text) < int.Parse(txtNuePlazo.Text))
        //    {
        // VerError("El plazo de reestructuracion no puede ser mayor al plazo original del credito");
        //  return;
        //   }

        ctlMensaje.MostrarMensaje("Desea realizar el proceso de reestructuración?");
    }



    private void TraerResultadosLista()
    {
        if (ListaSolicitada == null)
            return;
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            CtrCuotasExtras.GuardarCuotas(txtNumero_radicacion.Text);

            Usuario pUsuario = Usuario;
            Refinanciacion eRefinanciacion = new Refinanciacion();
            eRefinanciacion.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);
            eRefinanciacion.fecha_refinancia = txtFechaRefinanciacion.ToDateTime;
            eRefinanciacion.plazo_ref = txtNuePlazo.Text != "0" && !string.IsNullOrWhiteSpace(txtNuePlazo.Text) ? Convert.ToInt32(txtNuePlazo.Text) : Convert.ToInt32(txtPlazo.Text);
            eRefinanciacion.fecha_prox_pago_ref = txtNueFechaProximoPago.ToDateTime;
            eRefinanciacion.cuota_ref = Convert.ToDecimal(txtCuota.Text);
            eRefinanciacion.fecha_vencimiento_ref = txtNueFechaProximoPago.ToDateTime;
            eRefinanciacion.valor_pago = Convert.ToDecimal(txtAbono.Text);
            eRefinanciacion.valor_refinancia = Convert.ToDecimal(txtRefinanciar.Text);
            eRefinanciacion.cod_usuario = pUsuario.codusuario;
            eRefinanciacion.cod_ope = 0;
            eRefinanciacion.cod_linea_resstruc = !string.IsNullOrWhiteSpace(ddlLineaReestruc.SelectedValue) ? Convert.ToInt64(ddlLineaReestruc.SelectedValue) : 0;
            eRefinanciacion.cod_nueva_periodicidad = Convert.ToInt32(ddlNuePeriodicidad.SelectedValue);

            eRefinanciacion.lstAtributosRefinanciar = new List<AtributosRefinanciar>();

            foreach (GridViewRow gfila in gvLista.Rows)
            {
                AtributosRefinanciar eAtribut = new AtributosRefinanciar();
                Label lblCodAtr = (Label)gfila.FindControl("lblCodAtr");
                eAtribut.cod_atr = Convert.ToInt64(lblCodAtr.Text);
                Label lblDescripcion = (Label)gfila.FindControl("lblDescripcion");
                eAtribut.nom_atr = lblDescripcion.Text;
                CheckBox chkAplica = (CheckBox)gfila.FindControl("chkAplica");
                eAtribut.refinanciar = Convert.ToInt32(chkAplica.Checked);
                Label txtValor = (Label)gfila.FindControl("lblValor");
                try { eAtribut.valor = Convert.ToDecimal(txtValor.Text.Replace(GlobalWeb.gSeparadorMiles, "").Replace("$", "").Replace(" ", "").Replace("(", "").Replace(")", "")); }
                catch { }
                eRefinanciacion.lstAtributosRefinanciar.Add(eAtribut);
            }
            RefinanciacionServicio.CrearRefinanciacion(eRefinanciacion, pUsuario);

            SetActiveView(1);

            //Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            //Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = eRefinanciacion.cod_ope;
            //Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 51;
            //Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = eRefinanciacion.cod_deudor;
            //Session["OrigenComprobante"] = HttpContext.Current.Request.Url.AbsoluteUri; ;
            //Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    private void SetActiveView(int pPanel)
    {
        if (pPanel == 0) { Panel0.Visible = true;  Panel1.Visible = false; return; }
        if (pPanel == 1) { Panel0.Visible = false; Panel1.Visible = true;  return; }
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
            VerError("");
            CreditoService CreditoServicio = new CreditoService();
            Credito vCredito = new Credito();
            vCredito = CreditoServicio.ConsultarCredito(Convert.ToInt64(pIdObjeto), Usuario);

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

            txtTasa.Text = vCredito.tasa.ToString();
            txtTipoTasa.Text = vCredito.tipo_tasa + "-" + vCredito.desc_tasa;

            if (vCredito.valor_cuota != Int64.MinValue)
                txtValor_cuota.Text = HttpUtility.HtmlDecode(vCredito.valor_cuota.ToString().Trim());

            List<Atributos> lstAtributos = new List<Atributos>();
            Int64 numero_radicacion = 0;
            Double valor_pago = 0;
            Int64 tipo_pago = 2;
            Int64 Error = 0;
            DateTime fecha_pago = System.DateTime.Now;
            if (!string.IsNullOrWhiteSpace(txtFechaRefinanciacion.Text))
            {
                fecha_pago = Convert.ToDateTime(txtFechaRefinanciacion.Text);
            }

            try
            {
                numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
            lstAtributos = CreditoServicio.AmortizarCreditoDetalle(numero_radicacion, fecha_pago, valor_pago, tipo_pago, ref Error, Usuario);
            gvLista.DataSource = lstAtributos;
            gvLista.DataBind();
            txtAbono.Text = "0";
            CalcularValor();

            if (lstAtributos.Where(x => (x.cod_atr == 2 || x.cod_atr == 3) && x.valor > 0).Any())
            {
                VerError("No puedes reestructurar un credito si tiene intereses pendientes!. Se debe condonar o realizar el pago pertinente del valor de intereses");
                btnAdelante.Visible = false;
            }
            else
            {
                btnAdelante.Visible = true;
            }

            // Traer los datos de los codeudores
            // ---------------------------------------------------------------------------------------------------------
            codeudoresService codeudorservicio = new codeudoresService();
            List<codeudores> lstConsulta2 = new List<codeudores>();
            codeudores codedudor = new codeudores();
            lstConsulta2 = codeudorservicio.ListarcodeudoresCredito(Convert.ToString(this.txtNumero_radicacion.Text), Usuario);
            gvCodeudores.DataSource = lstConsulta2;
            if (lstConsulta2.Count > 0)
            {
                gvCodeudores.Visible = true;
                gvCodeudores.DataBind();
            }
            else
            {
                gvCodeudores.Visible = false;
            }
            LlenarVariables();
            CtrCuotasExtras.TablaCuoExt(this.txtNumero_radicacion.Text);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RefinanciacionServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    private void DropPeriodicidad()
    {
        PeriodicidadService periodicidadServicio = new PeriodicidadService();
        Periodicidad ePeriodic = new Periodicidad();
        ddlNuePeriodicidad.DataSource = periodicidadServicio.ListarPeriodicidad(ePeriodic, Usuario);
        ddlNuePeriodicidad.DataTextField = "Descripcion";
        ddlNuePeriodicidad.DataValueField = "Codigo";
        ddlNuePeriodicidad.DataBind();
        ddlNuePeriodicidad.Text = txtPeriodicidad.Text;
    }


    protected void chkAplica_CheckedChanged(object sender, EventArgs e)
    {
        CalcularValor();
    }

    private void CalcularValor()
    {
        Double total = 0;
        Double refinanciar = 0;
        foreach (GridViewRow gfila in gvLista.Rows)
        {
            Label lblCodAtr = (Label)gfila.FindControl("lblCodAtr");
            CheckBox chkAplica = (CheckBox)gfila.FindControl("chkAplica");
            if (lblCodAtr.Text == "1")
            {
                total += Convert.ToDouble(txtAbono.Text.Replace(".", ""));
                chkAplica.Checked = true;
            }
            double valor = 0;
            Label txtValor = (Label)gfila.FindControl("lblValor");
            try { valor = Convert.ToDouble(txtValor.Text.Replace(GlobalWeb.gSeparadorMiles, "").Replace("$", "").Replace(" ", "").Replace("(", "").Replace(")", "")); }
            catch { }
            if (chkAplica.Checked == false && lblCodAtr.Text == "1")
                total += valor;
            else
                refinanciar += valor;
        }
        if (total > 0)
        {
            TextBox txtTotal = (TextBox)gvLista.FooterRow.FindControl("txtTotal");
            GlobalWeb gweb = new GlobalWeb();
            txtTotal.Text = gweb.FormatoDecimal(total.ToString());
        }
        if (Convert.ToDouble(txtAbono.Text) > Convert.ToDouble(txtSaldoCapital.Text.Replace(".", "")))
            txtAbono.Text = txtSaldoCapital.Text;
        txtRefinanciar.Text = Convert.ToString(refinanciar - Convert.ToDouble(txtAbono.Text));
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void btnAdelante_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        VerError("");
        if (txtNueFechaProximoPago.TieneDatos == false)
        {
            VerError("Debe ingresar fecha de próximo pago");
            return;
        }
        if (string.IsNullOrWhiteSpace(ddlNuePeriodicidad.SelectedValue))
        {
            VerError("Debe seleccionar una periodicidad");
            return;
        }

        // if (int.Parse(txtPlazo.Text) < int.Parse(txtNuePlazo.Text))
        //{
        //  VerError("El plazo de reestructuracion no puede ser mayor al plazo original del credito");
        // return;
        //}

        mvRefinanciacion.ActiveViewIndex = 1;
        gvPlanPagos.datosCred.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);
        gvPlanPagos.datosCred.monto = Convert.ToInt64(txtRefinanciar.Text.Replace(GlobalWeb.gSeparadorMiles, ""));
        gvPlanPagos.datosCred.plazo = Convert.ToInt64(txtNuePlazo.Text);
        gvPlanPagos.datosCred.fecha_aprobacion = txtFechaRefinanciacion.ToDateTime;
        gvPlanPagos.datosCred.fecha_prox_pago = txtNueFechaProximoPago.ToDateTime;
        gvPlanPagos.datosCred.periodicidad = ddlNuePeriodicidad.SelectedValue;
        gvPlanPagos.datosCred.tasa = !string.IsNullOrWhiteSpace(txtTasa.Text) ? Convert.ToDecimal(txtTasa.Text) : 0;
        gvPlanPagos.bNuevo = true;
        gvPlanPagos.datosCred.tipo_plan = 1;
        List<Xpinn.FabricaCreditos.Entities.CuotasExtras> lstCuotasExtras = CtrCuotasExtras.GetListCuotas(txtNumero_radicacion.Text);
        if (lstCuotasExtras != null)
            if (lstCuotasExtras.Count > 0)
                gvPlanPagos.datosCred.tipo_plan = 2;
        CtrCuotasExtras.GuardarCuotas(txtNumero_radicacion.Text, 1);
        gvPlanPagos.TablaPlanPagos();
        txtCuota.Text = gvPlanPagos.datosCred.valor_cuota.ToString();
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);
    }

    protected void btnRegresar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        mvRefinanciacion.ActiveViewIndex = 0;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
    }

    protected void txtAbono_TextChanged(object sender, EventArgs e)
    {
        if (txtAbono.Text == "")
            txtAbono.Text = "0";
        CalcularValor();
    }


    protected void txtFechaRefinanciacion_eventoCambiar(object sender, EventArgs e)
    {
        ObtenerDatos(idObjeto);
        RegistrarPostBack();
    }

    void LlenarVariables()
    {
        PeriodicidadService periodicidadService = new PeriodicidadService();
        CtrCuotasExtras.Monto = txtMonto.Text;
        CtrCuotasExtras.Periodicidad = periodicidadService.ListarPeriodicidad(null, (Usuario)Session["Usuario"]).FirstOrDefault(x => x.Descripcion.Trim() == txtPeriodicidad.Text.Trim()).numero_dias.ToString();
        CtrCuotasExtras.PlazoTxt = txtPlazo.Text;
    }




}