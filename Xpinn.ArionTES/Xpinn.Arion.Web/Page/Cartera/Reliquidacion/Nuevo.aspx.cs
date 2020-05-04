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

partial class Nuevo : GlobalWeb
{
    ReliquidacionService ReliquidacionServicio = new ReliquidacionService();
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
            if (Session[ReliquidacionServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ReliquidacionServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(ReliquidacionServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReliquidacionServicio.CodigoPrograma, "Page_PreInit", ex);
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
                mvAtributo.ActiveViewIndex = 0;
                CargarDDL();
                DropPeriodicidad();
                if (Session[ReliquidacionServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ReliquidacionServicio.CodigoPrograma + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                    mvReliquidacion.ActiveViewIndex = 0;
                    txtFechaReliquidacion.ToDateTime = System.DateTime.Now;
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReliquidacionServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    private void CargarDDL()
    {
        Xpinn.FabricaCreditos.Services.LineasCreditoService LineasServicios = new Xpinn.FabricaCreditos.Services.LineasCreditoService();

        // LLena el DDL de los atributos del crédito
        List<LineasCredito> lstAtributos = new List<LineasCredito>();
        lstAtributos = LineasServicios.ddlatributo((Usuario)Session["usuario"]);
        ddlAtributo.DataSource = lstAtributos;
        ddlAtributo.DataTextField = "nombre";
        ddlAtributo.DataValueField = "cod_atr";
        ddlAtributo.DataBind();
        ddlAtributo.SelectedValue = "2";
        ddlAtributo.Enabled = false;

        // Llena el DDL de los tipos de tasas
        List<TipoTasa> lstTipoTasa = new List<TipoTasa>();
        TipoTasaService TipoTasaServicios = new TipoTasaService();
        TipoTasa vTipoTasa = new TipoTasa();
        lstTipoTasa = TipoTasaServicios.ListarTipoTasa(vTipoTasa, (Usuario)Session["usuario"]);
        ddlTipoTasa.DataSource = lstTipoTasa;
        ddlTipoTasa.DataTextField = "nombre";
        ddlTipoTasa.DataValueField = "cod_tipo_tasa";
        ddlTipoTasa.DataBind();
        ddlTipoTasa.SelectedValue = "2";

        // Llena el DDL de los tipos de tasas historicas
        List<TipoTasaHist> lstTipoTasaHist = new List<TipoTasaHist>();
        TipoTasaHistService TipoTasaHistServicios = new TipoTasaHistService();
        TipoTasaHist vTipoTasaHist = new TipoTasaHist();
        lstTipoTasaHist = TipoTasaHistServicios.ListarTipoTasaHist(vTipoTasaHist, (Usuario)Session["usuario"]);
        ddlHistorico.DataSource = lstTipoTasaHist;
        ddlHistorico.DataTextField = "descripcion";
        ddlHistorico.DataValueField = "tipo_historico";
        ddlHistorico.DataBind();

        rbCalculoTasa.SelectedIndex = 0;
        mvAtributo.ActiveViewIndex = 1;

    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        ctlMensaje.MostrarMensaje("Desea realizar el proceso de reliquidación?");
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            CreditoService CreditoServicio = new CreditoService();
            string pError = "";
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["usuario"];
            Xpinn.Cartera.Entities.Reliquidacion eReliquidacion = new Xpinn.Cartera.Entities.Reliquidacion();
            eReliquidacion.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);
            eReliquidacion.fecha_reliquida = txtFechaReliquidacion.ToDateTime;
            eReliquidacion.plazo_rel = Convert.ToInt32(txtNuePlazo.Text);
            eReliquidacion.fecha_prox_pago_rel = txtNueFechaProximoPago.ToDateTime;
            eReliquidacion.cuota_rel = Convert.ToDecimal(txtCuota.Text);
            eReliquidacion.cod_periodicidad_rel = Convert.ToInt64(ddlNuePeriodicidad.SelectedValue);
            eReliquidacion.cod_usuario = pUsuario.codusuario;
            eReliquidacion.lstCuotasExtras = CuotasExtras.GetListCuotas(txtNumero_radicacion.Text);

            // Guardar la tasa
            string tasa, tipotasa, desviacion, tipohisto;
            if (mvAtributo.ActiveViewIndex == 1)
            {
                tasa = txtTasa.Text != "0" ? txtTasa.Text : "";
                tipotasa = ddlTipoTasa.SelectedValue != "" && ddlTipoTasa.SelectedItem != null ? ddlTipoTasa.SelectedValue : "";
            }
            else
            {
                tasa = ""; tipotasa = "";
            }
            if (mvAtributo.ActiveViewIndex == 0)
            {
                desviacion = txtDesviacion.Text != "0" ? txtDesviacion.Text : "";
                tipohisto = ddlHistorico.SelectedValue != "" && ddlHistorico.SelectedItem != null ? ddlHistorico.SelectedValue : "";
            }
            else
            {
                desviacion = "";
                tipohisto = "";
            }
            CreditoServicio.cambiotasa(txtNumero_radicacion.Text, rbCalculoTasa.SelectedValue, tasa, tipotasa, desviacion, tipohisto,"", (Usuario)Session["usuario"],"2");

            ReliquidacionServicio.CrearReliquidacion(eReliquidacion, ref pError, pUsuario);
            if (pError.Trim() != "")
            {
                VerError(pError);
                return;
            }
            panelEncabezado.Visible = false;
            mvReliquidacion.ActiveViewIndex = 2;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
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
            Configuracion conf = new Configuracion();
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
            //if (!string.IsNullOrEmpty(vCredito.periodicidad))
            //    ddlNuePeriodicidad.SelectedItem.Text = Convert.ToString(vCredito.periodicidad);
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
            if (vCredito.numero_cuotas != Int64.MinValue)
                txtNuePlazo.Text = HttpUtility.HtmlDecode((vCredito.plazo - vCredito.cuotas_pagadas).ToString());
            if (vCredito.tasa != Int64.MinValue)
                txtTasa.Text = HttpUtility.HtmlDecode(vCredito.tasa.ToString());
            if (vCredito.tipo_tasa != Int64.MinValue)
                ddlTipoTasa.SelectedValue = Convert.ToString(vCredito.tipo_tasa);
            if (vCredito.tipo_historico != Int64.MinValue)
                ddlHistorico.SelectedValue = Convert.ToString(vCredito.tipo_historico);
            if (vCredito.desviacion != Int64.MinValue)
                txtDesviacion.Text = Convert.ToString(vCredito.desviacion);
            if (!string.IsNullOrEmpty(vCredito.calculo_atr))
                rbCalculoTasa.SelectedValue = vCredito.calculo_atr;
            rbCalculoTasa_SelectedIndexChanged(null, null);
            if (vCredito.cobra_mora != Int64.MinValue)
                if (vCredito.cobra_mora == 1)
                    chkCobraMora.Checked = true;

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
            lstAtributos = CreditoServicio.AmortizarCreditoDetalle(numero_radicacion, fecha_pago, valor_pago, tipo_pago, ref Error, (Usuario)Session["usuario"]);
            txtValorReliquidar.Text = txtSaldoCapital.Text;
            txtNueFechaProximoPago.ToDateTime = Convert.ToDateTime(txtFechaProximoPago.Text);
            
            DateTimeHelper dateHelper = new DateTimeHelper();
            if (dateHelper.DiferenciaEntreDosFechasDias(DateTime.Today, vCredito.fecha_prox_pago) > 30)
            {
                VerError("No se puede efectuar la reliquidación a un crédito con mora mayor a 30 días!.");
                btnAdelante.Enabled = false;
                btnAdelante.Visible = false;
            }

            LlenarVariables();
            CuotasExtras.TablaCuoExt(this.txtNumero_radicacion.Text);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReliquidacionServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    private void DropPeriodicidad()
    {
        PeriodicidadService periodicidadServicio = new PeriodicidadService();
        Periodicidad ePeriodic = new Periodicidad();
        ddlNuePeriodicidad.DataSource = periodicidadServicio.ListarPeriodicidad(ePeriodic, (Usuario)Session["usuario"]);
        ddlNuePeriodicidad.DataTextField = "Descripcion";
        ddlNuePeriodicidad.DataValueField = "Codigo";
        ddlNuePeriodicidad.DataBind();
        //ddlNuePeriodicidad.Text = txtPeriodicidad.Text;
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
        mvReliquidacion.ActiveViewIndex = 1;
        // Insertando datos de cuotas extrass
        CuotasExtras.GuardarCuotas(txtNumero_radicacion.Text, 1);
        // Determinar datos del crédito
        gvPlanPagos.datosCred.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);
        gvPlanPagos.datosCred.monto = Convert.ToInt64(txtValorReliquidar.Text.Replace(GlobalWeb.gSeparadorMiles, ""));
        gvPlanPagos.datosCred.plazo = Convert.ToInt64(txtNuePlazo.Text);
        gvPlanPagos.datosCred.fecha_aprobacion = txtFechaReliquidacion.ToDateTime;
        gvPlanPagos.datosCred.fecha_prox_pago = txtNueFechaProximoPago.ToDateTime;
        gvPlanPagos.datosCred.periodicidad = ddlNuePeriodicidad.SelectedValue;        
        gvPlanPagos.datosCred.tipo_plan = 1;
        List<Xpinn.FabricaCreditos.Entities.CuotasExtras> lstCuotasExtras = CuotasExtras.GetListCuotas(txtNumero_radicacion.Text);
        if (lstCuotasExtras != null)
           if (lstCuotasExtras.Count > 0)
                gvPlanPagos.datosCred.tipo_plan = 2;
        txtTasa.Text = txtTasa.Text.Trim() != "" ? txtTasa.Text.Trim() : "0";
        txtDesviacion.Text = txtDesviacion.Text.Trim() != "" ? txtDesviacion.Text.Trim() : "0";
        gvPlanPagos.datosCred.tasa = mvAtributo.ActiveViewIndex == 0 ? Convert.ToDecimal(txtDesviacion.Text.Trim()) : Convert.ToDecimal(txtTasa.Text.Trim());
        gvPlanPagos.bNuevo = true;
        gvPlanPagos.TablaPlanPagos();
        txtCuota.Text = gvPlanPagos.datosCred.valor_cuota.ToString();
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);
    }

    protected void btnRegresar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        mvReliquidacion.ActiveViewIndex = 0;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
    }

    protected void rbCalculoTasa_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbCalculoTasa.SelectedIndex == 0)
            mvAtributo.ActiveViewIndex = 1;
        if (rbCalculoTasa.SelectedIndex == 1)
            mvAtributo.ActiveViewIndex = 0;
        if (rbCalculoTasa.SelectedIndex == 2)
            mvAtributo.ActiveViewIndex = 0;
    }

    protected void ddlAtributo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlAtributo_TextChanged(object sender, EventArgs e)
    {

    }

    protected void txtTasa_PreRender(object sender, EventArgs e)
    {        
        TextBox txtValor = (TextBox)sender;
        string str = txtValor.Text;
        string strDec = "";
        int posDec = 0;
        string formateado = "";

        string s = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        if (s == ".")
            str = str.Replace(",", "");
        else
        {
            str = str.Replace(".", "");
            str = str.Replace(",", ".");
        }

        try
        {
            //posDec = str.IndexOf(",");
            posDec = s == "." ? str.IndexOf(",") : str.IndexOf(".");
            if (posDec > 0)
            {
                strDec = str.Substring(posDec + 1, str.Length - (posDec + 1));
                str = str.Substring(0, posDec);
            }
            if (str != "" && (Convert.ToInt64(str) > 0 || (Convert.ToInt64(str) == 0 && Convert.ToInt64(strDec) > 0)))
            {
                var strI = Convert.ToInt64(str);  //Convierte a entero y luego a string para quitar ceros a la izquierda
                str = strI.ToString();

                if (str.Length > 10)
                { str = str.Substring(0, 10); }

                int longi = str.Length;
                string mill = "";
                string mil = "";
                string cen = "";


                if (longi > 0 && longi <= 3)
                {
                    cen = str.Substring(0, longi);
                    formateado = Convert.ToInt64(cen).ToString();
                }
                else if (longi > 3 && longi <= 6)
                {
                    mil = str.Substring(0, longi - 3);
                    cen = str.Substring(longi - 3, 3);
                    formateado = Convert.ToInt64(mil) + "." + cen;
                }
                else if (longi > 6 && longi <= 10)
                {
                    mill = str.Substring(0, longi - 6);
                    mil = str.Substring(longi - 6, 3);
                    cen = str.Substring(longi - 3, 3);
                    formateado = Convert.ToInt64(mill) + "." + mil + "." + cen;
                }
                else
                { formateado = ""; }

                if (posDec > 0 && formateado != "")
                {
                    formateado = formateado + "," + strDec;
                }

            }
            else { formateado = ""; }
            txtValor.Text = formateado.ToString();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    void LlenarVariables()
    {
        PeriodicidadService periodicidadService = new PeriodicidadService();
        CuotasExtras.Monto = txtMonto.Text;
        CuotasExtras.Periodicidad = periodicidadService.ListarPeriodicidad(null, (Usuario)Session["Usuario"]).FirstOrDefault(x => x.Descripcion.Trim() == txtPeriodicidad.Text.Trim()).numero_dias.ToString();
        CuotasExtras.PlazoTxt = txtPlazo.Text;
    }

}