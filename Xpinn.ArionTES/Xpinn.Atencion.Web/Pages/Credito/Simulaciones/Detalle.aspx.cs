using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Configuration;
using System.Linq;

public partial class Detalle : GlobalWeb
{
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient EstadoServicio = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    xpinnWSCredito.WSCreditoSoapClient BOCredito = new xpinnWSCredito.WSCreditoSoapClient();
    xpinnWSAppFinancial.WSAppFinancialSoapClient AppServices = new xpinnWSAppFinancial.WSAppFinancialSoapClient();
    Configuracion global = new Configuracion();
    string FormatoFecha = " ";
    xpinnWSLogin.Persona1 pPersona;



    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ValidarSession();
            VisualizarTitulo(OptionsUrl.Simulacion, "Sim");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("SimulaciónCrédito", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["PlanPagos"] = null;
            ViewState["AtributosPlanPagos"] = null;
            panelLineas.Visible = false;
            CargarDropDown();
            HabilitarComision(false);
            HabilitarAporte(false);
            HabilitarSeguro(false);
            txtFechaPrimerPago.Text = DateTime.Now.AddMonths(1).ToString(gFormatoFecha);
            ddlLinea_SelectedIndexChanged(ddlLinea, null);
        }
    }


    public decimal cargarTasaEspecifica(int cod_linea_credito, int plazo, int monto, int cod_persona)
    {
        string tasaAnt = this.txttasa.Text.Trim().Replace("%", "");
        decimal tasaEspecifica = 0;
        if (!string.IsNullOrWhiteSpace(txtNroCuotas.Text.ToString()))
        {
            string filtro = " AND l.ESTADO = 1 AND l.APLICA_ASOCIADO = 1 AND l.COD_LINEA_CREDITO NOT IN (SELECT VALOR FROM PARAMETROS_LINEA WHERE COD_PARAMETRO = 600)";
            string pFiltro = "tasa|" + cod_linea_credito.ToString() + "|" + plazo.ToString() + "|" + monto + "|" + cod_persona.ToString() + "|" + filtro;

            tasaEspecifica = AppServices.obtenerTasaEspecifica(cod_linea_credito.ToString(), plazo, Session["sec"].ToString());
            if (tasaEspecifica > 0)
            {
                Decimal tasaMensual = (tasaEspecifica / 12) / 100;
                txttasa.Text = tasaMensual.ToString("P");
                lblTipoTasa.Text = "MVSS";
            }
        }
        return tasaEspecifica;
    }

    void CargarDropDown()
    {
        // llama en el webservices el método que le devuelve el rango
        string pFiltro = " AND l.estado = 1 AND l.APLICA_ASOCIADO = 1 and l.COD_LINEA_CREDITO not in (SELECT VALOR FROM parametros_linea WHERE COD_PARAMETRO = 600)";

        List<xpinnWSAppFinancial.LineasCredito> lstLineas = AppServices.ListarTipoCreditos(pFiltro, Session["sec"].ToString());
        if (lstLineas.Count > 0)
        {
            ViewState["DTLineasCredito"] = lstLineas;
            ddlLinea.DataSource = lstLineas;
            ddlLinea.DataTextField = "nombre";
            ddlLinea.DataValueField = "cod_linea_credito";
            ddlLinea.AppendDataBoundItems = true;
            ddlLinea.Items.Insert(0, new ListItem("Seleccione un item", ""));
            ddlLinea.DataBind();
        }

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstPeriodicidad = EstadoServicio.PoblarListaDesplegable("PERIODICIDAD", "COD_PERIODICIDAD,DESCRIPCION", "", "2", Session["sec"].ToString());
        if (lstPeriodicidad.Count > 0)
        {
            LlenarDrop(ddlPeriodicidad, lstPeriodicidad);
            LlenarDrop(ddlPeriodicidadCuotaExt, lstPeriodicidad);
            ddlPeriodicidad.SelectedValue = "1";
        }

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstTipo = EstadoServicio.PoblarListaDesplegable("TipoLiquidacion", "TIPO_LIQUIDACION, DESCRIPCION", "", "2", Session["sec"].ToString());
        if (lstTipo.Count > 0)
            LlenarDrop(ddlTipoLiquidacion, lstTipo);

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstTipoCuotasExt = EstadoServicio.PoblarListaDesplegable("TIPO_CUOTAS_EXTRAS", "IDTIPO, DESCRIPCION", "", "2", Session["sec"].ToString());
        if (lstTipoCuotasExt.Count > 0)
            LlenarDrop(ddlCuotaExtTipo, lstTipoCuotasExt);

        ValidarEnable();
    }


    protected void ValidarEnable()
    {
        if (ConfigurationManager.AppSettings["BloqueoCombos"] != null)
        {
            if (ConfigurationManager.AppSettings["BloqueoCombos"] == "1")
            {
                ddlPeriodicidad.Enabled = false;
                ddlTipoLiquidacion.Enabled = false;
                txttasa.Enabled = false;
            }
            if (ConfigurationManager.AppSettings["Periodicidad"] != null)
            {
                string valPeriodicidad = ConfigurationManager.AppSettings["Periodicidad"].ToString();
                ddlPeriodicidad.SelectedValue = valPeriodicidad;
            }

            if (ConfigurationManager.AppSettings["TipoLiquidacion"] != null)
            {
                string valTipoLiqui = ConfigurationManager.AppSettings["TipoLiquidacion"].ToString();
                ddlTipoLiquidacion.SelectedValue = valTipoLiqui;
            }
        }
    }

    void LlenarDrop(DropDownList ddlDropCarga, List<xpinnWSEstadoCuenta.ListaDesplegable> listaData)
    {
        ddlDropCarga.DataSource = listaData;
        ddlDropCarga.DataTextField = "descripcion";
        ddlDropCarga.DataValueField = "idconsecutivo";
        ddlDropCarga.AppendDataBoundItems = true;
        ddlDropCarga.Items.Insert(0, new ListItem("Seleccione un item", ""));
        ddlDropCarga.DataBind();
    }

    protected void HabilitarComision(bool pComision)
    {
        panelComision.Visible = pComision;
    }

    protected void HabilitarAporte(bool pAporte)
    {
        panelAporte.Visible = pAporte;
    }

    protected void HabilitarSeguro(bool pSeguro)
    {
        panelSeguro.Visible = pSeguro;
    }

    protected void ValidarComisionAporte_CUPO()
    {
        pPersona = (xpinnWSLogin.Persona1)Session["persona"];
        xpinnWSCredito.ComisionAporteSeguro pResult = new xpinnWSCredito.ComisionAporteSeguro();
        Decimal tasaMensual;
        try
        {
            pResult = BOCredito.ValidarComisionAporte(ddlLinea.SelectedValue, pPersona.clavesinecriptar, Session["sec"].ToString());
            if (pResult != null)
            {
                HabilitarComision(pResult.comision);
                HabilitarAporte(pResult.aporte);
                HabilitarSeguro(pResult.seguro);
            }

            xpinnWSCredito.LineasCredito pEntidad = new xpinnWSCredito.LineasCredito();
            pEntidad = BOCredito.Calcular_Cupo(ddlLinea.SelectedValue, pPersona.cod_persona, DateTime.Now, pPersona.clavesinecriptar, Session["sec"].ToString());
            if (pEntidad != null)
            {
                txtPlazoMaximo.Text = pEntidad.Plazo_Maximo.ToString();
                txtMontoMaximo.Text = Convert.ToDecimal(pEntidad.Monto_Maximo.ToString()).ToString("C0");
            }

            // CONSULTAR LOS PARAMETROS DE LA TASA
            bool result = false;
            pnlCuotaExtra.Visible = false;
            if (ViewState["DTLineasCredito"] != null)
            {
                List<xpinnWSAppFinancial.LineasCredito> lstLineas = (List<xpinnWSAppFinancial.LineasCredito>)ViewState["DTLineasCredito"];
                if (lstLineas.Count > 0)
                {
                    xpinnWSAppFinancial.LineasCredito pLineaSeleccionada = lstLineas.Where(x => x.cod_linea_credito == ddlLinea.SelectedValue).FirstOrDefault();
                    if (pLineaSeleccionada != null)
                    {
                        result = true;
                        if (!string.IsNullOrEmpty(pLineaSeleccionada.tipoliquidacion))
                            ddlTipoLiquidacion.SelectedValue = pLineaSeleccionada.tipoliquidacion;
                        
                        /*Credito Rotativo*/
                        if (pLineaSeleccionada.tipo_linea==2)
                        {
                            tasaMensual = Convert.ToDecimal(pLineaSeleccionada.tasa);
                            txttasa.Text = tasaMensual.ToString("N") + "%";
                        }
                        else
                        {
                            tasaMensual = (Convert.ToDecimal(pLineaSeleccionada.tasa) / 12) / 100;
                            txttasa.Text = tasaMensual.ToString("P");
                        }
                        lblTipoTasa.Text = "MVSS";
                        if (ConfigurationManager.AppSettings["cuotasExtra"] != null)
                        {
                            string cuotaExtra = ConfigurationManager.AppSettings["cuotasExtra"].ToString();
                            if (cuotaExtra != "0")
                            {
                                pnlCuotaExtra.Visible = pLineaSeleccionada.Cuotas_Extras == null ? false : Convert.ToInt32(pLineaSeleccionada.Cuotas_Extras) == 1 ? true : false;
                                gvCuoExt.DataSource = null;
                                gvCuoExt.DataBind();
                            }
                        }
                    }
                }
            }
            if (!result)
            {
                pEntidad = BOCredito.ConsultarTasaInteresLineaCredito(ddlLinea.SelectedValue, pPersona.clavesinecriptar, Session["sec"].ToString());
                tasaMensual = (Convert.ToDecimal(pEntidad.tasa) / 12) / 100;
                txttasa.Text = tasaMensual.ToString("P");
                lblTipoTasa.Text = "MVSS";
            }
        }
        catch
        {
        }
    }


    protected Boolean ValidarDatos()
    {
        if (ddlLinea.SelectedIndex == 0)
        {
            VerError("Seleccione la linea de crédito");
            return false;
        }
        if (txtMontoSolicitado.Text.Trim() == "0")
        {
            VerError("Ingrese el monto a solicitar");
            return false;
        }
        else
        {
            decimal vrSoli = Convert.ToDecimal(txtMontoSolicitado.Text.Replace(".", "").Replace(",", "").Replace("$", ""));
            decimal vrMax = Convert.ToDecimal(txtMontoMaximo.Text.Replace(".", "").Replace("$", "").Replace(",", ""));
            if (vrSoli > vrMax)
            {
                VerError("El monto o plazo solicitado no cumple con las condiciones de la linea");
                return false;
            }
        }
        if (txtNroCuotas.Text.Trim() == "")
        {
            VerError("Ingrese el número de cuotas");
            return false;
        }
        else
        {
            int plazo = Convert.ToInt32(txtNroCuotas.Text);
            int plazoMax = !string.IsNullOrEmpty(txtPlazoMaximo.Text) ? Convert.ToInt32(txtPlazoMaximo.Text) : 0;
            if (plazo > plazoMax)
            {
                VerError("El monto o plazo solicitado no cumple con las condiciones de la linea");
                return false;
            }
        }
        if (ddlPeriodicidad.SelectedIndex == 0)
        {
            VerError("Seleccione la periodicidad de la simulación a generar");
            return false;
        }
        if (ddlTipoLiquidacion.SelectedIndex == 0)
        {
            VerError("Seleccione el tipo de liquidación de la simulación a generar");
            return false;
        }
        return true;
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (ValidarDatos())
        {
            List<xpinnWSCredito.CuotasExtras> lstCuotasExtras = ObtenerListaCuotasExtras();
            ActualizarCuota(lstCuotasExtras);
            Actualizar(lstCuotasExtras);
        }
    }


    private void ActualizarCuota(List<xpinnWSCredito.CuotasExtras> lstCuotasExtras)
    {
        VerError("");
        try
        {
            pPersona = (xpinnWSLogin.Persona1)Session["persona"];
            string texto = txtMontoSolicitado.Text;
            texto = texto.Replace(".", "").Replace(",", "").Replace("$", "");

            decimal comision = 0, Aporte = 0, seguro = 0;
            try
            {
                if (txtComision.Text != "" && panelComision.Visible == true)
                    comision = Decimal.Parse(txtComision.Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
            }
            catch
            {
                comision = 0;
            }

            try
            {
                if (txtAporte.Text != "" && panelAporte.Visible == true)
                    Aporte = Convert.ToDecimal(txtAporte.Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                else
                    Aporte = 0;
            }
            catch
            {
                Aporte = 0;
            }

            try
            {
                if (txtSeguro.Text != "" && panelSeguro.Visible == true)
                    seguro = Convert.ToDecimal(txtSeguro.Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                else
                    seguro = 0;
            }
            catch
            {
                seguro = 0;
            }
            xpinnWSCredito.Simulacion pDatos = new xpinnWSCredito.Simulacion();
            int tipo_liquidacion = Convert.ToInt32(ddlTipoLiquidacion.SelectedValue);

            long monto = 0;
            if (!string.IsNullOrWhiteSpace(texto))
            {
                monto = Convert.ToInt64(texto.Replace(".", "").Replace(",", ""));
            }

            decimal tasa = this.txttasa.Text == "" ? 0 : Convert.ToDecimal(this.txttasa.Text.Trim().Replace("%", ""));
            DateTime? fechaprimerpago = null;
            if (txtFechaPrimerPago.TieneDatos)
                fechaprimerpago = Convert.ToDateTime(txtFechaPrimerPago.Text);
            try
            {
                tasa = cargarTasaEspecifica(Int32.Parse(ddlLinea.SelectedValue), Convert.ToInt32(txtNroCuotas.Text.Replace(".", "").Replace(",", "")), Int32.Parse(monto.ToString()), Int32.Parse(pPersona.cod_persona.ToString()));
                tasa = tasa != 0 ? tasa : Convert.ToDecimal(this.txttasa.Text.Trim().Replace("%", ""));
                pDatos = BOCredito.ConsultarSimulacionCuota(monto, Convert.ToInt32(txtNroCuotas.Text.Replace(".", "").Replace(",", "").Replace("$", "")), Convert.ToInt32(ddlPeriodicidad.SelectedValue), ddlLinea.SelectedValue, tipo_liquidacion, tasa, comision, Aporte, fechaprimerpago, pPersona.clavesinecriptar, pPersona.cod_persona, lstCuotasExtras, Session["sec"].ToString());
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
            if (pDatos != null)
            {
                txtCuota.Text = pDatos.cuota.ToString("C0");
                btnAdquirir.Visible = true;
            }
            else
            {
                txtCuota.Text = "0";
                btnAdquirir.Visible = false;
            }
        }
        catch (Exception ex)
        {
            VerError("Se generó un error al Calcular la cuota " + ex.Message);
        }
    }


    private void Actualizar(List<xpinnWSCredito.CuotasExtras> lstCuotasExtras)
    {
        VerError("");

        pPersona = (xpinnWSLogin.Persona1)Session["persona"];

        xpinnWSCredito.Simulacion datosApp = new xpinnWSCredito.Simulacion();
        List<xpinnWSCredito.DatosPlanPagos> lstConsulta = new List<xpinnWSCredito.DatosPlanPagos>();
        try
        {
            // Determinar separador de miles
            Configuracion global = new Configuracion();

            // Determinar el monto del crédito
            string texto;
            texto = txtMontoSolicitado.Text.Replace(".", "").Replace(",", "").Replace("$", "");
            datosApp.monto = Convert.ToInt32(texto);

            // Determinar condiciones deseadas del crédito
            datosApp.plazo = Convert.ToInt32(txtNroCuotas.Text.Replace(".", "").Replace(",", ""));
            datosApp.periodic = Convert.ToInt32(ddlPeriodicidad.SelectedValue);
            datosApp.for_pag = 1;
            datosApp.cod_credi = ddlLinea.SelectedValue;
            datosApp.tipo_liquidacion = Convert.ToInt32(ddlTipoLiquidacion.SelectedValue);
            datosApp.fecha = DateTime.Today;
            try
            {
                if (txtComision.Text != "")
                    datosApp.comision = Decimal.Parse(txtComision.Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
            }
            catch
            {
                datosApp.comision = 0;
            }

            try
            {
                if (txtAporte.Text != "")
                    datosApp.aporte = Convert.ToDecimal(txtAporte.Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
            }
            catch
            {
                datosApp.aporte = 0;
            }

            try
            {
                if (txtSeguro.Text != "")
                    datosApp.seguro = Convert.ToDecimal(txtSeguro.Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
            }
            catch
            {
                datosApp.seguro = 0;
            }
            if (txtFechaPrimerPago.TieneDatos)
                datosApp.fecha_primer_pago = txtFechaPrimerPago.ToDateTime;

            decimal tasa = cargarTasaEspecifica(Int32.Parse(ddlLinea.SelectedValue), Convert.ToInt32(txtNroCuotas.Text.Replace(".", "").Replace(",", "")), Convert.ToInt32(datosApp.monto), Int32.Parse(pPersona.cod_persona.ToString()));
            datosApp.tasa = tasa;
            datosApp.cod_persona = pPersona.cod_persona;
            datosApp.lstCuotasExtras = lstCuotasExtras;

            lstConsulta = BOCredito.SimularPlanPagos(datosApp, pPersona.clavesinecriptar, Session["sec"].ToString());


            gvLista.DataSource = lstConsulta;
            panelLineas.Visible = false;
            if (lstConsulta.Count > 0)
            {
                panelLineas.Visible = true;
                gvLista.DataBind();
                gvLista.Visible = true;
                gvLista.Columns[1].ItemStyle.Width = 90;
                // Ocultando las columnas que no deben mostrarse
                List<xpinnWSCredito.Atributos> lstAtr = new List<xpinnWSCredito.Atributos>();
                lstAtr = BOCredito.SimularAtributosPlan(pPersona.clavesinecriptar, Session["sec"].ToString());
                ViewState["AtributosPlanPagos"] = lstAtr;
                for (int i = 4; i <= 18; i++)
                {

                    gvLista.Columns[i].Visible = false;
                    int j = 0;
                    foreach (xpinnWSCredito.Atributos item in lstAtr)
                    {
                        if (j == i - 4)
                            gvLista.Columns[i].HeaderText = item.nom_atr;
                        j = j + 1;
                    }
                }
                foreach (xpinnWSCredito.DatosPlanPagos ItemPlanPagos in lstConsulta)
                {
                    if (ItemPlanPagos.int_1 != 0) { gvLista.Columns[4].Visible = true; }
                    if (ItemPlanPagos.int_2 != 0) { gvLista.Columns[5].Visible = true; }
                    if (ItemPlanPagos.int_3 != 0) { gvLista.Columns[6].Visible = true; }
                    if (ItemPlanPagos.int_4 != 0) { gvLista.Columns[7].Visible = true; }
                    if (ItemPlanPagos.int_5 != 0) { gvLista.Columns[8].Visible = true; }
                    if (ItemPlanPagos.int_6 != 0) { gvLista.Columns[9].Visible = true; }
                    if (ItemPlanPagos.int_7 != 0) { gvLista.Columns[10].Visible = true; }
                    if (ItemPlanPagos.int_8 != 0) { gvLista.Columns[11].Visible = true; }
                    if (ItemPlanPagos.int_9 != 0) { gvLista.Columns[12].Visible = true; }
                    if (ItemPlanPagos.int_10 != 0) { gvLista.Columns[13].Visible = true; }
                    if (ItemPlanPagos.int_11 != 0) { gvLista.Columns[14].Visible = true; }
                    if (ItemPlanPagos.int_12 != 0) { gvLista.Columns[15].Visible = true; }
                    if (ItemPlanPagos.int_13 != 0) { gvLista.Columns[16].Visible = true; }
                    if (ItemPlanPagos.int_14 != 0) { gvLista.Columns[17].Visible = true; }
                    if (ItemPlanPagos.int_15 != 0) { gvLista.Columns[18].Visible = true; }
                }
                //Oculta afiancol
                gvLista.Columns[7].Visible = false;

                gvLista.DataBind();
                ViewState["PlanPagos"] = lstConsulta;
            }
            else
            {
                VerError("No se genero el plan de pagos");
            }

        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        VerError("");
        panelLineas.Visible = false;
        CleanLinea();
        txtCuota.Text = "";
        ddlLinea.SelectedIndex = 0;
        gvLista.DataSource = null;
        gvLista.DataBind();
        txtMontoSolicitado.Text = "0";
        txtNroCuotas.Text = "";
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            if (ViewState["PlanPagos"] != null)
            {
                List<xpinnWSCredito.DatosPlanPagos> lstConsulta = new List<xpinnWSCredito.DatosPlanPagos>();
                lstConsulta = (List<xpinnWSCredito.DatosPlanPagos>)ViewState["PlanPagos"];
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
            }
            else
            {
                List<xpinnWSCredito.CuotasExtras> lstCuotasExtras = ObtenerListaCuotasExtras();
                Actualizar(lstCuotasExtras);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void CleanLinea()
    {
        if (ConfigurationManager.AppSettings["TipoLiquidacion"] != null)
        {
            string valTipoLiqui = ConfigurationManager.AppSettings["TipoLiquidacion"].ToString();
            ddlTipoLiquidacion.SelectedValue = valTipoLiqui;
        }
        else
            ddlTipoLiquidacion.SelectedIndex = 0;
        txtPlazoMaximo.Text = "";
        txtMontoMaximo.Text = "";
        txttasa.Text = "";
        lblTipoTasa.Text = "";
        HabilitarComision(false);
        HabilitarAporte(false);
        HabilitarSeguro(false);
        pnlCuotaExtra.Visible = false;
        txtCuota.Text = string.Empty;
    }

    protected void ddlLinea_SelectedIndexChanged(object sender, EventArgs e)
    {
        CleanLinea();
        if (ddlLinea.SelectedIndex != 0)
        {
            ValidarComisionAporte_CUPO();
        }
    }


    protected void btnGenerarCuotaExtra_Click(object sender, EventArgs e)
    {
        lblErrorCuotaExtra.Text = string.Empty;
        try
        {
            List<xpinnWSCredito.CuotasExtras> lstCuoExt = new List<xpinnWSCredito.CuotasExtras>();
            if (ViewState[PersonaLogin.cod_persona + "CuoExt"] != null)
            {
                lstCuoExt = (List<xpinnWSCredito.CuotasExtras>)ViewState[PersonaLogin.cod_persona + "CuoExt"];
                if (lstCuoExt.Count == 1)
                {
                    xpinnWSCredito.CuotasExtras gItem = lstCuoExt[0];
                    if (gItem.valor == 0 || gItem.valor == null)
                        lstCuoExt.Remove(gItem);
                }
            }

            string pMessageError = string.Empty;


            if (string.IsNullOrEmpty(txtMontoSolicitado.Text) || txtMontoSolicitado.Text == "0")
                pMessageError += "<li>Debe digitar el valor de la simulación</li>";
            if (ddlPeriodicidad.SelectedIndex <= 0)
                pMessageError += "<li>Seleccione la periodicidad de la simulación a realizar</li>";
            if (string.IsNullOrEmpty(txtNroCuotas.Text))
                pMessageError += "<li>Debe digitar el plazo de la simulación a realizar</li>";
            if (string.IsNullOrEmpty(txtPorcentaje.Text))
                pMessageError += "<li>Debe digitar el porcentaje de cuota extra</li>";
            if (string.IsNullOrEmpty(txtNumeroCuotaExt.Text))
                pMessageError += "<li>Debe digitar el numero de cuotas extras</li>";
            if (string.IsNullOrEmpty(txtFechaCuotaExt.Text))
                pMessageError += "<li>Debe digitar la fecha inicial de cuota extra (DD/MM/AAAA)</li>";
            if (string.IsNullOrEmpty(txtValorCuotaExt.Text) || txtValorCuotaExt.Text == "0")
                pMessageError += "<li>Debe digitar el valor de cuota extra</li>";
            if (ddlPeriodicidadCuotaExt.SelectedIndex <= 0)
                pMessageError += "<li>Seleccione la periodicidad de la cuota extra</li>";
            if (ddlCuotaExtTipo.SelectedIndex <= 0)
                pMessageError += "<li>Seleccione el tipo de cuota extra</li>";

            if (!string.IsNullOrEmpty(pMessageError))
            {
                lblErrorCuotaExtra.Text = pMessageError;
                return;
            }

            List<xpinnWSAppFinancial.Periodicidad> lstPeriodicidad = null;
            if (ViewState["DTPeriodicidad"] != null)
                lstPeriodicidad = (List<xpinnWSAppFinancial.Periodicidad>)ViewState["DTPeriodicidad"];
            else
            {
                ViewState["DTPeriodicidad"] = lstPeriodicidad;
                lstPeriodicidad = AppServices.ListarPeriodicidades(new xpinnWSAppFinancial.Periodicidad(), Session["sec"].ToString());
            }

            xpinnWSAppFinancial.Periodicidad pPeriodicidad = lstPeriodicidad.Where(x => x.Codigo == Convert.ToInt32(ddlPeriodicidad.SelectedValue)).FirstOrDefault();
            int valor_diasPeriodicidad = Convert.ToInt32(pPeriodicidad.numero_dias);
            int plazo_CuotaExtra = (Convert.ToInt32(ddlPeriodicidadCuotaExt.SelectedValue) * Convert.ToInt32(txtNumeroCuotaExt.Text)) / valor_diasPeriodicidad;
            if (plazo_CuotaExtra > Convert.ToInt32(txtNroCuotas.Text))
            {
                lblErrorCuotaExtra.Text = "<li>El numero de cuotas por la periodicidad Excede el plazo</li>";
                return;
            }
            Decimal valor_limite = (Convert.ToDecimal(txtMontoSolicitado.Text.Replace(".", "").Replace(",", "").Replace("$", "")) * Convert.ToDecimal(txtPorcentaje.Text)) / 100;
            Decimal valor_cuota = Convert.ToDecimal(txtValorCuotaExt.Text.Replace(".", "")) * Convert.ToDecimal(txtNumeroCuotaExt.Text);
            if (valor_cuota > valor_limite)
            {
                lblErrorCuotaExtra.Text = "<li>La cantidad de cuotas extras por el valor excede el porcentaje del monto</li>";
                return;
            }

            // GENERANDO CUOTAS EXTRAS
            pPeriodicidad = lstPeriodicidad.Where(x => x.Codigo == Convert.ToInt32(ddlPeriodicidadCuotaExt.SelectedValue)).FirstOrDefault();

            int total_cuota = Convert.ToInt32(txtNumeroCuotaExt.Text);
            int dias_inclemento = 0;
            decimal total = 0;
            DateTime? FechaCuotaExt = Convert.ToDateTime(txtFechaCuotaExt.Text);

            xpinnWSCredito.CuotasExtras gItemNew;
            for (int i = 1; i <= total_cuota; i++)
            {
                gItemNew = new xpinnWSCredito.CuotasExtras();
                gItemNew.fecha_pago = FechaCuotaExt;
                if (i > 1)
                {
                    dias_inclemento = Convert.ToInt32(pPeriodicidad.numero_dias);
                    Xpinn.Comun.Services.FechasService fechaServicio = new Xpinn.Comun.Services.FechasService();
                    gItemNew.fecha_pago = fechaServicio.FecSumDia(Convert.ToDateTime(gItemNew.fecha_pago), dias_inclemento, Convert.ToInt32(pPeriodicidad.tipo_calendario));
                    FechaCuotaExt = gItemNew.fecha_pago;
                }
                gItemNew.forma_pago = ddlFormaPagoCuotaExt.SelectedValue.ToString();
                gItemNew.des_forma_pago = ddlFormaPagoCuotaExt.SelectedItem.ToString();
                gItemNew.valor = Convert.ToInt64(txtValorCuotaExt.Text.Replace(".", ""));
                gItemNew.des_tipo_cuota = ddlCuotaExtTipo.SelectedValue.ToString() + "-" + ddlCuotaExtTipo.SelectedItem.ToString();

                lstCuoExt.Add(gItemNew);
                total = total + Convert.ToDecimal(gItemNew.valor);
            }

            ViewState[PersonaLogin.cod_persona + "TotalCuoExt"] = total;
            gvCuoExt.DataSource = null;
            if (lstCuoExt.Count > 0)
                gvCuoExt.DataSource = lstCuoExt;
            gvCuoExt.DataBind();
            ViewState[PersonaLogin.cod_persona + "CuoExt"] = lstCuoExt;
        }
        catch (Exception ex)
        {
            lblErrorCuotaExtra.Text = ("Error generado en la generación. " + ex.Message);
        }
    }

    protected void btnLimpiarCuotaExtra_Click(object sender, EventArgs e)
    {
        txtPorcentaje.Text = string.Empty;
        txtNumeroCuotaExt.Text = string.Empty;
        txtFechaCuotaExt.Text = string.Empty;
        txtValorCuotaExt.Text = string.Empty;
        ddlFormaPagoCuotaExt.SelectedIndex = 0;
        ddlPeriodicidadCuotaExt.SelectedIndex = 0;
        ddlCuotaExtTipo.SelectedIndex = 0;
        ViewState[PersonaLogin.cod_persona + "TotalCuoExt"] = 0;
        InicialCuoExt();
    }

    protected void InicialCuoExt()
    {
        ViewState[PersonaLogin.cod_persona + "CuoExt"] = null;
        gvCuoExt.DataSource = null;
        gvCuoExt.DataBind();
    }

    protected void gvCuoExt_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<xpinnWSCredito.CuotasExtras> lstCuoExt = new List<xpinnWSCredito.CuotasExtras>();
        lstCuoExt = (List<xpinnWSCredito.CuotasExtras>)ViewState[PersonaLogin.cod_persona + "CuoExt"];
        if (lstCuoExt.Count >= 1)
        {
            xpinnWSCredito.CuotasExtras eCuoExt = new xpinnWSCredito.CuotasExtras();
            int index = Convert.ToInt32(e.RowIndex);
            eCuoExt = lstCuoExt[index];
            if (eCuoExt.valor != 0 || eCuoExt.valor == null)
            {
                decimal total = Convert.ToDecimal(ViewState[PersonaLogin.cod_persona + "TotalCuoExt"].ToString());
                if (total != 0)
                {
                    total = total - Convert.ToDecimal(eCuoExt.valor);
                    ViewState[PersonaLogin.cod_persona + "TotalCuoExt"] = total;
                }
                lstCuoExt.Remove(eCuoExt);
            }
        }
        if (lstCuoExt.Count == 0)
        {
            InicialCuoExt();
            gvCuoExt.DataSource = lstCuoExt;
            gvCuoExt.DataBind();
            ViewState[PersonaLogin.cod_persona + "CuoExt"] = lstCuoExt;
        }
        else
        {
            gvCuoExt.DataSource = lstCuoExt;
            gvCuoExt.DataBind();
            ViewState[PersonaLogin.cod_persona + "CuoExt"] = lstCuoExt;
        }
    }


    protected List<xpinnWSCredito.CuotasExtras> ObtenerListaCuotasExtras()
    {
        int num_ter = 0;
        /////////////////////////////////////////////////////////////////////////////////////////////
        // Guardar datos de Cuotas Extras
        /////////////////////////////////////////////////////////////////////////////////////////////
        List<xpinnWSCredito.CuotasExtras> lstCuotasExtras = null;
        xpinnWSCredito.CuotasExtras vCuotaExtra;
        if (gvCuoExt.Rows.Count > 0)
        {
            lstCuotasExtras = new List<xpinnWSCredito.CuotasExtras>();
            foreach (GridViewRow rFila in gvCuoExt.Rows)
            {
                vCuotaExtra = new xpinnWSCredito.CuotasExtras();
                vCuotaExtra.numero_radicacion = Convert.ToInt64(ddlLinea.Text);
                Label lblfechapago = rFila.FindControl("lblfechapago") as Label;
                if (lblfechapago.Text == "")
                    break;
                Label lblformapago = rFila.FindControl("lblformapago") as Label;
                Label lblvalor = rFila.FindControl("lblvalor") as Label;
                vCuotaExtra.fecha_pago = Convert.ToDateTime(lblfechapago.Text);
                if (lblformapago.Text == "Caja")
                    vCuotaExtra.forma_pago = "1";
                else if (lblformapago.Text == "Nomina")
                    vCuotaExtra.forma_pago = "2";
                vCuotaExtra.valor = Convert.ToDecimal(gvCuoExt.DataKeys[rFila.RowIndex].Value.ToString());
                num_ter += 1;
                vCuotaExtra.cod_cuota = num_ter;
                lstCuotasExtras.Add(vCuotaExtra);
            }
        }

        return lstCuotasExtras;
    }

    protected void txtNroCuotas_TextChanged(object sender, EventArgs e)
    {
        //Consultar el valor de la tasa según la cantidad de cuotas
    }

    protected void btnAdquirir_Click(object sender, EventArgs e)
    {
        //Obtiene lista de cuotas extras
        xpinnWSCredito.Simulacion simulacion = new xpinnWSCredito.Simulacion();
        decimal total = 0;
        List<xpinnWSCredito.CuotasExtras> lstCuoExt = new List<xpinnWSCredito.CuotasExtras>();
        if (ViewState[PersonaLogin.cod_persona + "CuoExt"] != null)
        {
            lstCuoExt = (List<xpinnWSCredito.CuotasExtras>)ViewState[PersonaLogin.cod_persona + "CuoExt"];
            simulacion.lstCuotasExtras = lstCuoExt;
            total = Convert.ToDecimal(ViewState[PersonaLogin.cod_persona + "TotalCuoExt"]);
            simulacion.totalCuotasExtra = total;
        }

        //Obtiene valores
        string texto = txtMontoSolicitado.Text.Replace(".", "").Replace(",", "").Replace("$", "");
        long monto = 0;
        if (!string.IsNullOrWhiteSpace(texto))
        {
            monto = Convert.ToInt64(texto.Replace(".", "").Replace(",", ""));
        }
        string linea = ddlLinea.SelectedValue.ToString();
        int plazo = Convert.ToInt32(txtNroCuotas.Text.Replace(".", "").Replace(",", ""));
        int periodicidad = Convert.ToInt32(ddlPeriodicidad.SelectedValue);
        decimal cuota = Convert.ToDecimal(txtCuota.Text.Replace(",", "").Replace(".", "").Replace("$", ""));
        decimal tasa = Convert.ToDecimal(txttasa.Text.Trim().Replace("%", ""));

        //Agrega valores a simulación
        simulacion.monto = Convert.ToInt32(monto);
        simulacion.cod_credi = linea;
        simulacion.plazo = plazo;
        simulacion.periodic = periodicidad;
        simulacion.cuota = cuota;
        simulacion.tasa = tasa;
        //Crea los datos en sesion para pasarlos a solicitud
        Session["simulacion"] = simulacion;
        Response.Redirect("~/Pages/Credito/Solicitud/Credito.aspx", false);
    }
}