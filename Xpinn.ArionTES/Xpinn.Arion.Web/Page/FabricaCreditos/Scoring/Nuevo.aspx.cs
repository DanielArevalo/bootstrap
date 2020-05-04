using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Comun.Entities;
using Xpinn.Comun.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Util;
using System.Data;

public partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    ScoringService _scoringService = new ScoringService();
    StringHelper _stringHelper = new StringHelper();
    Usuario _usuario;
    long _codPersona;
    //Variables para calcular Total Productos Seleccionados
    decimal TVI = 0;
    decimal TSa = 0;
    decimal TIn = 0;
    decimal TVC = 0;

    #region Eventos - Metodos Iniciales


    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            Site toolBar = (Site)Master;
            toolBar.eventoRegresar += (s, evt) => Navegar(Pagina.Lista);
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_scoringService.CodigoPrograma, "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[".idScoring"] != null)
                VisualizarOpciones(_scoringService.CodigoPrograma, "A");

            _usuario = (Usuario)Session["usuario"];
            _codPersona = Convert.ToInt64(Session[".idScoring"]);

            if (!IsPostBack)
            {
                InicializarPagina();
            }
            SumarTotales();

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_scoringService.CodigoPrograma, "Page_Load", ex);
        }
    }


    void InicializarPagina()
    {
        txtCodPersona.Text = _codPersona.ToString();

        Scoring scoring = ConsultarScoring();

        if (scoring == null) return;

        LlenarScoring(scoring);

        ConsultarEstadoDeCuenta();

        LlenarListas();

        ConsultarAlertas();

        ConsultarParafiscales();

        txtValidarConsulta.Text = "0";
        gvCifin.DataSource = new List<Obligacion> { new Obligacion() };
        gvCifin.DataBind();

        // Inicializar eventos para mostra todos los calculos
        ddlLineaCredito_SelectedIndexChanged(this, EventArgs.Empty);
        txtGrupoQueNecesitaCalcularDeducciones_TextChanged(this, EventArgs.Empty);

        CalcularTotalesCartera();

        TituloCupos.Visible = false;
        tbCuposLineas.Visible = false;
    }


    Scoring ConsultarScoring()
    {
        try
        {
            Scoring scoring = new Scoring() { cod_persona = _codPersona };

            return _scoringService.ConsultarDatosScoring(scoring, _usuario);
        }
        catch (Exception ex)
        {
            VerError("Error al consultar los datos del scoring," + ex.Message);
            return null;
        }
    }


    void LlenarScoring(Scoring scoring)
    {
        txtCodigoNomina.Text = scoring.cod_nomina != null ? scoring.cod_nomina.ToString() : string.Empty;
        txtCedula.Text = scoring.identificacion;
        txtTipoIdentificacion.Text = scoring.tipo_identificacion;
        txtCodTipoIdentificacion.Text = scoring.cod_tipo_identificacion;
        TxtNombres.Text = scoring.primer_nombre + " " + scoring.segundo_nombre;
        txtApellidos.Text = scoring.primer_apellido + " " + scoring.segundo_apellido;
        txtCodigoAsociado.Text = scoring.cod_afiliacion.ToString();
        txtMontoAportes.Text = _stringHelper.FormatearNumerosComoCurrency(scoring.saldo_aporte);
        txtSalarioBasico.Text = _stringHelper.FormatearNumerosComoCurrency(scoring.salario);
        txtSalarioVariable.Text = _stringHelper.FormatearNumerosComoCurrency(scoring.honorarios);
        txtTotalIngresos.Text = _stringHelper.FormatearNumerosComoCurrency((scoring.salario + scoring.honorarios));
        txtOtrosIngresos.Text = _stringHelper.FormatearNumerosComoCurrency(scoring.otros_ingresos);

        txtEmpresa.Text = scoring.empresa;
        txtCargo.Text = scoring.descripcion_cargo;
        txtUbicacion.Text = scoring.direccionempresa;
        txtFechaIngreso.Text = scoring.fecha_ingresoempresa != DateTime.MinValue ? scoring.fecha_ingresoempresa.ToShortDateString() : DateTime.Today.ToShortDateString();
        txtFechaAfiliacion.Text = scoring.fecha_afiliacion != DateTime.MinValue ? scoring.fecha_afiliacion.ToShortDateString() : DateTime.Today.ToShortDateString();

        txtCuotaAporteObligatorio.Text = _stringHelper.FormatearNumerosComoCurrency(Convert.ToInt64(scoring.valor_cuota_aporte));
        txtAhorrosVoluntarios.Text = _stringHelper.FormatearNumerosComoCurrency(scoring.ahorro_voluntario);

        //Agregado para llenar valor de servicios
        txtServicios.Text = _stringHelper.FormatearNumerosComoCurrency(scoring.valor_servicios);
    }


    void ConsultarEstadoDeCuenta()
    {
        try
        {
            CreditoService creditoService = new CreditoService();
            List<Credito> listaCreditosActivos = creditoService.ListarCarteraActiva(_codPersona, _usuario);

            if (listaCreditosActivos.Count == 0) // Si esta vacia añado una fila vacia para evitar errores
            {
                listaCreditosActivos.Add(new Credito());
            }

            var totalizacion = new // Anonymous Type para facilitar el calculo de la totalizacion con LINQ :D
            {
                totalSaldo = TSa,//listaCreditosActivos.Sum(x => x.saldo_capital),
                totalValorInicial = TVI,//listaCreditosActivos.Sum(x => x.monto_aprobado),
                totalValorCuota = TVC, //listaCreditosActivos.Sum(x => x.valor_cuota),
                totalintereses = TIn, //listaCreditosActivos.Sum(x => x.intcoriente)
            };
            gvEstadoCuenta.DataSource = listaCreditosActivos.OrderByDescending(x => x.fecha_desembolso_nullable);
            gvEstadoCuenta.DataBind();

            ViewState.Add("lstEstadoCuenta", listaCreditosActivos);

            // Footer
            TextBox txtTotalValorInicialFooter = gvEstadoCuenta.FooterRow.FindControl("txtTotalValorInicialFooter") as TextBox;
            txtTotalValorInicialFooter.Text = _stringHelper.FormatearNumerosComoCurrency(totalizacion.totalValorInicial);

            TextBox txtTotalSaldoFooter = gvEstadoCuenta.FooterRow.FindControl("txtTotalSaldoFooter") as TextBox;
            txtTotalSaldoFooter.Text = _stringHelper.FormatearNumerosComoCurrency(totalizacion.totalSaldo);

            TextBox txtTotalInteresesFooter = gvEstadoCuenta.FooterRow.FindControl("txtTotalInteresesFooter") as TextBox;
            txtTotalInteresesFooter.Text = _stringHelper.FormatearNumerosComoCurrency(totalizacion.totalintereses);

            TextBox txtTotalValorCuotaFooter = gvEstadoCuenta.FooterRow.FindControl("txtTotalValorCuotaFooter") as TextBox;
            txtTotalValorCuotaFooter.Text = _stringHelper.FormatearNumerosComoCurrency(totalizacion.totalValorCuota);

            CalcularTotalesCartera();
        }
        catch (Exception ex)
        {
            VerError("Error al consultar el estado de cuenta, " + ex.Message);
            return;
        }
    }


    void LlenarListas()
    {
        LlenarListasDesplegables("LineasCredito", ddlLineaCredito);

        LlenarListasDesplegables("Periodicidad", ddlPeriodicidad);
        ddlPeriodicidad.SelectedValue = "1";
    }


    void ConsultarAlertas()
    {
        Scoring factor = ConsultarFactorAntiguedad();

        if (factor != null)
        {
            LlenarFactoresAlerta(factor);
        }
    }


    void ConsultarParafiscales()
    {
        try
        {
            //Modificado para hacer el calculo de salud y pensión a partir del Salario Básico
            string ingresosString = _stringHelper.DesformatearNumerosEnteros(txtSalarioBasico.Text);

            if (string.IsNullOrWhiteSpace(ingresosString)) return;

            long ingresos = Convert.ToInt64(ingresosString);
            decimal parafiscales = 0;

            if (ingresos != 0)
            {
                parafiscales = _scoringService.CalcularParafiscales(ingresos, _usuario);
            }

            txtParafiscales.Text = _stringHelper.FormatearNumerosComoCurrency(parafiscales);
        }
        catch (Exception ex)
        {
            VerError("Error al consultar factor de antiguedad, " + ex.Message);
            RegistrarPostBack();
        }
    }


    void SumarTotales()
    {
        TVI = 0;
        TSa = 0;
        TIn = 0;
        TVC = 0;

        int contp = 0;

        foreach (GridViewRow row in gvEstadoCuenta.Rows)
        {
            CheckBox CheckBoxgv = row.FindControl("CheckBoxgv") as CheckBox;
            if (CheckBoxgv != null)
            {
                if (CheckBoxgv.Checked == true)
                {

                    Label VI = gvEstadoCuenta.Rows[contp].FindControl("lblValorInicial") as Label;
                    decimal pVI = Convert.ToDecimal(VI.Text.TrimStart('$'));

                    Label Sl = gvEstadoCuenta.Rows[contp].FindControl("lblSaldo") as Label;
                    decimal pSl = Convert.ToDecimal(Sl.Text.TrimStart('$'));

                    Label Ints = gvEstadoCuenta.Rows[contp].FindControl("lblInt") as Label;
                    decimal pInts = Convert.ToDecimal(Ints.Text.TrimStart('$'));

                    Label VC = gvEstadoCuenta.Rows[contp].FindControl("lblValorCuota") as Label;
                    decimal pVC = Convert.ToDecimal(VC.Text.TrimStart('$'));


                    TVI += pVI;
                    TSa += pSl;
                    TIn += pInts;
                    TVC += pVC;
                }
            }
            contp++;
        }


        var totalizacion = new // Anonymous Type para facilitar el calculo de la totalizacion con LINQ :D
        {
            totalSaldo = TSa,
            totalValorInicial = TVI,
            totalValorCuota = TVC,
            totalintereses = TIn,
        };

        // Footer
        TextBox txtTotalValorInicialFooter = gvEstadoCuenta.FooterRow.FindControl("txtTotalValorInicialFooter") as TextBox;
        txtTotalValorInicialFooter.Text = _stringHelper.FormatearNumerosComoCurrency(totalizacion.totalValorInicial);

        TextBox txtTotalSaldoFooter = gvEstadoCuenta.FooterRow.FindControl("txtTotalSaldoFooter") as TextBox;
        txtTotalSaldoFooter.Text = _stringHelper.FormatearNumerosComoCurrency(totalizacion.totalSaldo);

        TextBox txtTotalInteresesFooter = gvEstadoCuenta.FooterRow.FindControl("txtTotalInteresesFooter") as TextBox;
        txtTotalInteresesFooter.Text = _stringHelper.FormatearNumerosComoCurrency(totalizacion.totalintereses);

        TextBox txtTotalValorCuotaFooter = gvEstadoCuenta.FooterRow.FindControl("txtTotalValorCuotaFooter") as TextBox;
        txtTotalValorCuotaFooter.Text = _stringHelper.FormatearNumerosComoCurrency(totalizacion.totalValorCuota);

        //Agregado para hacer calculo del total de cuota y total saldo cuando se seleccionen los valores de la tabla
        CalcularTotalesCartera();
        //Agregado para calcular la capacidad mensual si el valor de la cuota cambia
        CalcularCapacidadMensualScore();
    }

    void ConsultarCuposLineas()
    {
        if (txtCodPersona.Text.Trim() == "")
            return;
        try
        {
            Int64 pCodPersona = Convert.ToInt64(txtCodPersona.Text);
            decimal pDisponible = ConvertirStringToDecimal(txtTotalIngresos.Text);
            decimal pMontoSolicitado = ConvertirStringToDecimal(txtMontoSolicitado.Text);
            Int64 pNumeroCuotas = 0;
            if (txtPlazo.Text.Trim() != "")
                pNumeroCuotas = Convert.ToInt64(txtPlazo.Text);
            //Agregado para validar qué tipo de PL utilizar
            bool preanalisis = false;
            Xpinn.FabricaCreditos.Services.CreditoService creditoServicio = new CreditoService();
            List<Credito> lstCredito = new List<Credito>();
            lstCredito = creditoServicio.RealizarPreAnalisis(preanalisis, DateTime.Now, pCodPersona, pDisponible, pNumeroCuotas, pMontoSolicitado, Convert.ToInt32(ddlPeriodicidad.SelectedValue), false, (Usuario)Session["usuario"]);
            if (lstCredito.Count <= 0)
            {
                Credito credito = new Credito();
                credito.cod_linea_credito = "";
                credito.cod_persona = 0;
                credito.monto = 0;
                lstCredito.Add(credito);
            }
            gvCreditos.PageIndex = 0;
            gvCreditos.Visible = true;
            gvCreditos.DataSource = lstCredito;
            gvCreditos.DataBind();

            Session["DatosCupos"] = lstCredito;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(personaServicio.CodigoProgramaPreAnalisis, "ObtenerDatos", ex);
        }
    }
    #endregion


    #region Eventos Intermedios


    protected void btnConsultarCifin_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");

        if (txtValidarConsulta.Text != "0")
        {
            VerError("Ya se efectuo la consulta en el primer click!.");
            return;
        }
        else if (string.IsNullOrWhiteSpace(txtCedula.Text) && string.IsNullOrWhiteSpace(txtCodTipoIdentificacion.Text))
        {
            VerError("No se puede consultar a Cifin sin tipo de identificacion y numero de identificación de la persona");
        }
        else
        {
            ConsultarCifin(txtCedula.Text, txtCodTipoIdentificacion.Text);
            //ConsultarCifin("1037609584", "1");
        }
    }

    protected void chkCifin_CheckedChanged(object sender, EventArgs e)
    {
        LlenarFooterObligaciones();

        // Metodos para recalcular capacidad ahora teniendo en cuenta los nuevos datos de Cifin
        CalcularTotalesCartera();
        CalcularPorcentajeSalarioComprometido();
        CalcularCapacidadMensualScore();
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        ImprimirReporteAsPDF(ReportViewerPlan, FormatoArchivo.PDF);
    }

    protected void btnIrSolicitud_Click(object sender, EventArgs e)
    {
        Persona1Service datosClienteServicio = new Persona1Service();
        Session[datosClienteServicio.CodigoPrograma + ".id"] = txtCedula.Text;
        Session["Origen"] = "SDCL";

        // Ir a la siguiente página
        Navegar("~/Page/FabricaCreditos/DatosCliente/DatosCliente.aspx");
    }

    void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            string error = string.Empty;
            if (ValidarScore(out error))
            {
                LlenarReporte();

                Credito credito = ArmarEntidadParaGuardar();
                credito.imagen = ReportViewerPlan.LocalReport.Render("PDF");

                //Agregado para consultar y actualizar datos de información financiera del cliente
                EstadosFinancieros vEstadoPersona = InformacionFinanciera();
                EstadosFinancierosService EstadosServicio = new EstadosFinancierosService();
                vEstadoPersona.cod_persona = Convert.ToInt64(txtCodPersona.Text);
                if(vEstadoPersona.conceptootrosconyuge == null)
                    vEstadoPersona.conceptootrosconyuge = "0";
                if (vEstadoPersona.conceptootros == null)
                    vEstadoPersona.conceptootros = "0";
                EstadosServicio.guardarIngreEgre(vEstadoPersona, _usuario);

                credito = _scoringService.GuardarPreanalisisCredito(credito, _usuario);

                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarRegresar(false);
                mvPrincipal.ActiveViewIndex = 1;


                //crear consulta para estado de persona 
                Boolean EstadoPersona = false;
                EstadoPersona = personaServicio.VerificarSiPersonaEsAsociado(Convert.ToInt64(txtCodPersona.Text) ,Usuario);


                if (credito.idpreanalisis != 0 && lblViable.Text=="Viable" && EstadoPersona == true)
                {

                    if (credito != null && txtCedula.Text != "")
                    {
                        Session[personaServicio.CodigoPrograma + ".id"] = txtCedula.Text;
                        Session["Origen"] = "SDCL";
                    }
                    //cargando linea
                    if (ddlLineaCredito.SelectedValue != "0")
                    {
                        Session[personaServicio.CodigoPrograma + ".linea"] = Convert.ToInt32(ddlLineaCredito.SelectedValue);
                    }
                    //Cargando el Monto
                    if (credito.monto != 0)
                    {
                        Session[personaServicio.CodigoPrograma + ".monto"] = Convert.ToInt64(credito.monto);
                    }
                    else
                    {
                        Session[personaServicio.CodigoPrograma + ".monto"] = Convert.ToInt64(txtMontoSolicitado.Text);
                    }
                    //Cargando el Plazo
                    if (credito.plazo != null || credito.plazo != 0)
                    {
                        Session[personaServicio.CodigoPrograma + ".plazo"] = Convert.ToInt64(credito.plazo);
                    }
                    else
                    {
                        Session[personaServicio.CodigoPrograma + ".plazo"] = Convert.ToInt64(txtPlazo.Text);
                    }
                    //Cargando la cuota
                    if (txtCuotaMensualAprox.Text != "")
                    {
                        Session[personaServicio.CodigoPrograma + ".cuota"] = _stringHelper.DesformatearNumerosDecimales(txtCuotaMensualAprox.Text.Remove(txtCuotaMensualAprox.Text.Length - 3, 3));
                    }
                    //Cargando la periodicidad
                    if (ddlPeriodicidad.SelectedValue != "0")
                    {
                        Session[personaServicio.CodigoPrograma + ".periodicidad"] = ddlPeriodicidad.SelectedValue;
                    }
                    //Determinar el tipo de credito
                    ClasificacionCredito clasificacion = new ClasificacionCredito();
                    LineasCreditoService lineasser = new LineasCreditoService();
                    LineasCredito linea = new LineasCredito();
                    linea = lineasser.ConsultaLineaCredito(ddlLineaCredito.SelectedValue, (Usuario)Session["Usuario"]);

                    switch (linea.cod_clasifica)
                    {
                        case 0:
                            clasificacion = ClasificacionCredito.Consumo;
                            break;
                        case 1:
                            clasificacion = ClasificacionCredito.MicroCredito;
                            break;
                        case 2:
                            clasificacion = ClasificacionCredito.Vivienda;
                            break;
                        case 3:
                            clasificacion = ClasificacionCredito.Comercial;
                            break;
                    }
                    Session["TipoCredito"] = clasificacion;


                    Session.Remove(personaServicio.CodigoProgramaPreAnalisis + ".id");
                    if (Session["Origen"] == "CEL")
                        Navegar("~/Page/FabricaCreditos/DatosCliente/DatosCliente.aspx");
                    if (Session["Origen"] == "SDCL")
                        Navegar("~/Page/FabricaCreditos/DatosCliente/DatosCliente.aspx");
                }

            }
            else
            {
                VerError(error);
            }
        }
        catch (Exception ex)
        {
            string error = "Error al guardar el score, " + ex.Message;

            if (ex.InnerException != null)
            {
                error += " , " + ex.InnerException.Message;
            }

            VerError(error);
        }
    }

    bool ValidarScore(out string error)
    {
        error = string.Empty;
        bool valido = true;

        if (string.IsNullOrWhiteSpace(txtMontoSolicitado.Text))
        {
            error += "El monto solicitado no puede estar vacio, ";
            valido = false;
        }
        else if (string.IsNullOrWhiteSpace(txtPlazo.Text))
        {
            error += "El plazo no puede estar vacio, ";
            valido = false;
        }
        else if (Primas.Text != "")
        {
            if (ValorPrima.Text == "")
            {
                error += "No debe dejar vacio el valor de la primas, ";
                valido = false;

            }
            else
            {
                if (txtFechaPrima.Text == "")
                {
                    error += "No debe dejar vacia la fecha de la primas, ";
                    valido = false;
                }
            }
        }

        else if (Cesantias.Text != "")
        {
            if (ValorCesantias.Text == "")
            {
                error += "No debe dejar vacio el valor de las cesantias, ";
                valido = false;

            }
            else
            {
                if (txtFechaCesan.Text == "")
                {
                    error += "No debe dejar vacia la fecha de las cesantias, ";
                    valido = false;
                }
            }
        }

        return valido;
    }

    Credito ArmarEntidadParaGuardar()
    {
        Credito credito = new Credito();
        credito.cod_persona = _codPersona;
        credito.fecha = DateTime.Today;

        TextBox txtTotalValorCuotaFooter = gvEstadoCuenta.FooterRow.FindControl("txtTotalValorCuotaFooter") as TextBox;
        string totalValorCuotaCreditos = _stringHelper.DesformatearNumerosDecimales(txtTotalValorCuotaFooter.Text);

        if (!string.IsNullOrWhiteSpace(totalValorCuotaCreditos))
        {
            credito.creditos = Convert.ToDecimal(totalValorCuotaCreditos);
        }

        credito.ingresos_adicionales = Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtOtrosIngresos.Text));
        credito.aportes = Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtMontoAportes.Text));
        credito.cod_usuario = Convert.ToInt32(_usuario.codusuario);
        credito.monto = Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtMontoSolicitado.Text));
        credito.plazo = Convert.ToInt32(_stringHelper.DesformatearNumerosEnteros(txtPlazo.Text));
        credito.cod_linea_credito = ddlLineaCredito.SelectedValue;
        credito.image = null;

        return credito;
    }

    protected void ddlLineaCredito_SelectedIndexChanged(object sender, EventArgs e)
    {
        VerError("");
        string cod_linea = ddlLineaCredito.SelectedValue;

        if (!string.IsNullOrWhiteSpace(cod_linea))
        {
            ConsultarCondicionesDelCredito(cod_linea);
            txtGrupoQueNecesitaCalcularCuota_TextChanged(this, EventArgs.Empty);
            CalcularCuota();
            CalcularIngresoPorCastigoLinea(cod_linea);
            CalcularPorcentajeSalarioComprometido();
        }
    }

    protected void txtSalarioBasico_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtSalarioBasico.Text))
        {
            decimal salarioBasico = !string.IsNullOrWhiteSpace(txtSalarioBasico.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtSalarioBasico.Text)) : 0;
            decimal salarioVariable = !string.IsNullOrWhiteSpace(txtSalarioVariable.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtSalarioVariable.Text)) : 0;

            decimal ingresosTotales = salarioBasico + salarioVariable;

            txtTotalIngresos.Text = _stringHelper.FormatearNumerosComoCurrency(ingresosTotales);

            CalcularPorcentajeSalarioComprometido();
            CalcularIngresoPorCastigoLinea();
            CalcularCapacidadMensualScore();
            ConsultarAlertas();
            LlenarTipoGarantia();
            //Agregado para recalcular el valor de salud y pensión si se modifica el valor del salario
            ConsultarParafiscales();
            txtSalarioBasico.Text = _stringHelper.FormatearNumerosComoCurrency(txtSalarioBasico.Text);
        }
    }

    protected void txtGrupoQueNecesitaCalcularCuota_TextChanged(object sender, EventArgs e)
    {
        VerError("");

        txtMontoSolicitado.Text = _stringHelper.FormatearNumerosComoCurrency(txtMontoSolicitado.Text);

        if (!string.IsNullOrWhiteSpace(txtPlazo.Text) && !string.IsNullOrWhiteSpace(txtMontoSolicitado.Text))
        {
            int plazo = Convert.ToInt32(txtPlazo.Text);
            decimal monto = Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtMontoSolicitado.Text));

            if (ValidarCondicionesLineaCredito(plazo, monto))
            {
                CalcularCuota();
                CalcularPorcentajeSalarioComprometido();
                LlenarTipoGarantia();
            }
        }
    }

    protected void txtGrupoQueNecesitaCalcularDeducciones_TextChanged(object sender, EventArgs e)
    {
        txtRetencion.Text = _stringHelper.FormatearNumerosComoCurrency(txtRetencion.Text);
        txtServicios.Text = _stringHelper.FormatearNumerosComoCurrency(txtServicios.Text);
        txtPensionesVoluntarias.Text = _stringHelper.FormatearNumerosComoCurrency(txtPensionesVoluntarias.Text);
        txtCuentasAFC.Text = _stringHelper.FormatearNumerosComoCurrency(txtCuentasAFC.Text);

        decimal parafiscales = !string.IsNullOrWhiteSpace(txtParafiscales.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtParafiscales.Text)) : 0;
        decimal retencion = !string.IsNullOrWhiteSpace(txtRetencion.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtRetencion.Text)) : 0;
        decimal cuotaAporteObligatorio = !string.IsNullOrWhiteSpace(txtCuotaAporteObligatorio.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtCuotaAporteObligatorio.Text)) : 0;
        decimal servicios = !string.IsNullOrWhiteSpace(txtServicios.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtServicios.Text)) : 0;
        decimal ahorrosVoluntarios = !string.IsNullOrWhiteSpace(txtAhorrosVoluntarios.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtAhorrosVoluntarios.Text)) : 0;
        decimal pensionesVoluntarias = !string.IsNullOrWhiteSpace(txtPensionesVoluntarias.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtPensionesVoluntarias.Text)) : 0;
        decimal cuentasAFC = !string.IsNullOrWhiteSpace(txtCuentasAFC.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtCuentasAFC.Text)) : 0;

        decimal totalDeducciones = parafiscales + retencion + cuotaAporteObligatorio + servicios + ahorrosVoluntarios + pensionesVoluntarias + cuentasAFC;

        txtTotalDeducciones.Text = _stringHelper.FormatearNumerosComoCurrency(totalDeducciones);

        CalcularCapacidadMensualScore();
    }

    //para formatear
    protected void Formatear_TextChanged(object sender, EventArgs e)
    {
        ValorPrima.Text = _stringHelper.FormatearNumerosComoCurrency(ValorPrima.Text);
        ValorCesantias.Text = _stringHelper.FormatearNumerosComoCurrency(ValorCesantias.Text);
        txtValorCuotaCompromiso.Text = _stringHelper.FormatearNumerosComoCurrency(txtValorCuotaCompromiso.Text);
    }


    protected void ddlPeriodicidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtPlazo.Text) && !string.IsNullOrWhiteSpace(txtMontoSolicitado.Text))
        {
            CalcularCuota();
            CalcularPorcentajeSalarioComprometido();
        }
    }

    protected void Check_Clicked(object sender, EventArgs e)
    {
        CheckBox chkHeader = sender as CheckBox;
        if (chkHeader.Checked == true)
        {
            if (gvEstadoCuenta.Rows.Count > 0)
            {
                foreach (GridViewRow row in gvEstadoCuenta.Rows)
                {
                    CheckBox CheckBoxgv = row.FindControl("CheckBoxgv") as CheckBox;
                    CheckBoxgv.Checked = true;
                }
            }
        }
        else
        {
            foreach (GridViewRow row in gvEstadoCuenta.Rows)
            {
                CheckBox CheckBoxgv = row.FindControl("CheckBoxgv") as CheckBox;
                CheckBoxgv.Checked = false;
            }
        }

        SumarTotales();
    }

    protected void btnConsultarCupos_Click(object sender, EventArgs e)
    {
        //Agregado para consultar cupo de lineas de credito
        ConsultarCuposLineas();
        TituloCupos.Visible = true;
        tbCuposLineas.Visible = true;
    }

    //Agregado método para grilla de cupos de líneas
    protected void gvCreditos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (e.NewPageIndex >= 0)
            {
                gvCreditos.PageIndex = e.NewPageIndex;
                if (Session["DatosCupos"] != null)
                {
                    List<Credito> lstCredito = new List<Credito>();
                    lstCredito = (List<Credito>)Session["DatosCupos"];
                    gvCreditos.DataSource = lstCredito;
                    gvCreditos.DataBind();
                }
                else
                {
                    ConsultarCuposLineas();
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(personaServicio.CodigoPrograma, "gvCreditos_PageIndexChanging", ex);
        }
    }

    //Agregado método para grilla de cupos de líneas
    protected void gvCreditos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbmonto = (Label)e.Row.FindControl("lbmonto");
            if (lbmonto != null)
            {
                string sMonto = lbmonto.Text.Replace(gSeparadorMiles, "");
                if (sMonto == "0,00")
                {
                    CheckBoxGrid chkSeleccionar = (CheckBoxGrid)e.Row.FindControl("chkSeleccionar");
                    if (chkSeleccionar != null)
                        chkSeleccionar.Visible = false;
                }
            }
        }
    }

    #endregion


    #region Metodos Ayuda para Cifin


    void ConsultarCifin(string identificacion, string tipoIdentificacion)
    {
        try
        {
            InterfazCIFIN interfazCifin = new InterfazCIFIN();
            HomologacionServices homologacionService = new HomologacionServices();
            Homologacion homologacion = homologacionService.ConsultarHomologacionTipoIdentificacion(tipoIdentificacion, Usuario);

            if (!homologacion.tipo_identificacion_cifin.HasValue)
            {
                VerError("No existe la homologación del tipo de identificacion!.");
                return;
            }

            tipoIdentificacion = homologacion.tipo_identificacion_cifin.ToString();
            ConsultaCifin cifin = interfazCifin.ConsultarCifin(identificacion, tipoIdentificacion);

            if (cifin.Cifin.Tercero.RespuestaConsulta == "02")
            {
                txtValidarConsulta.Text = "1";
                List<Obligacion> listaCifin = LlenarListaObligaciones(cifin);

                if (cifin.Cifin.Tercero.Score != null)
                {
                    txtPuntajeScore.Text = cifin.Cifin.Tercero.Score.Puntaje;
                    txtProbMora.Text = cifin.Cifin.Tercero.Score.TasaMorosidad;
                }

                txtFechaConsulta.Text = cifin.Cifin.Tercero.Fecha;
                txtEstadoPersona.Text = cifin.Cifin.Tercero.Estado;

                listaCifin.ForEach(x =>
                {
                    x.ValorInicial = !string.IsNullOrWhiteSpace(x.ValorInicial) ? _stringHelper.FormatearNumerosComoCurrency((Convert.ToDecimal(x.ValorInicial) * 1000)) : "0";
                    x.SaldoObligacion = !string.IsNullOrWhiteSpace(x.SaldoObligacion) ? _stringHelper.FormatearNumerosComoCurrency((Convert.ToDecimal(x.SaldoObligacion) * 1000)) : "0";
                    x.ValorMora = !string.IsNullOrWhiteSpace(x.ValorMora) ? _stringHelper.FormatearNumerosComoCurrency((Convert.ToDecimal(x.ValorMora) * 1000)) : "0";
                    x.ValorCuota = !string.IsNullOrWhiteSpace(x.ValorCuota) ? _stringHelper.FormatearNumerosComoCurrency((Convert.ToDecimal(x.ValorCuota) * 1000)) : "0";
                });

                pnlInformacionCifin.Visible = true;
                gvCifin.Visible = true;

                gvCifin.DataSource = listaCifin;
                gvCifin.DataBind();

                LlenarFooterObligaciones();

                // Metodos para recalcular capacidad ahora teniendo en cuenta los nuevos datos de Cifin
                CalcularTotalesCartera();
                CalcularPorcentajeSalarioComprometido();
                CalcularCapacidadMensualScore();
            }
            else if (cifin.Cifin.Tercero.RespuestaConsulta == "01")
            {
                VerError("Titular no existe en Cifin!.");
            }
            else
            {
                VerError("Respuesta invalida en la consulta a Cifin!.");
            }
        }
        catch (Exception ex)
        {
            VerError("Error al consultar a Cifin, " + ex.Message);
        }
    }

    List<Obligacion> LlenarListaObligaciones(ConsultaCifin cifin)
    {
        List<Obligacion> listaCifin = new List<Obligacion>();
        if (cifin.Cifin.Tercero.SectorFinancieroAlDia != null && cifin.Cifin.Tercero.SectorFinancieroAlDia.Obligacion != null && cifin.Cifin.Tercero.SectorFinancieroAlDia.Obligacion.Count > 0)
        {
            IEnumerable<Obligacion> listaObligaciones = ListarObligacionesVigentes(cifin.Cifin.Tercero.SectorFinancieroAlDia.Obligacion);

            listaCifin.AddRange(listaObligaciones);
        }
        if (cifin.Cifin.Tercero.SectorFinancieroEnMora != null && cifin.Cifin.Tercero.SectorFinancieroEnMora.Obligacion != null && cifin.Cifin.Tercero.SectorFinancieroEnMora.Obligacion.Count > 0)
        {
            IEnumerable<Obligacion> listaObligaciones = ListarObligacionesVigentes(cifin.Cifin.Tercero.SectorFinancieroEnMora.Obligacion);

            listaCifin.AddRange(listaObligaciones);
        }
        if (cifin.Cifin.Tercero.SectorRealAlDia != null && cifin.Cifin.Tercero.SectorRealAlDia.Obligacion != null && cifin.Cifin.Tercero.SectorRealAlDia.Obligacion.Count > 0)
        {
            IEnumerable<Obligacion> listaObligaciones = ListarObligacionesVigentes(cifin.Cifin.Tercero.SectorRealAlDia.Obligacion);

            listaCifin.AddRange(listaObligaciones);
        }
        if (cifin.Cifin.Tercero.SectorRealEnMora != null && cifin.Cifin.Tercero.SectorRealEnMora.Obligacion != null && cifin.Cifin.Tercero.SectorRealEnMora.Obligacion.Count > 0)
        {
            IEnumerable<Obligacion> listaObligaciones = ListarObligacionesVigentes(cifin.Cifin.Tercero.SectorRealEnMora.Obligacion);

            listaCifin.AddRange(listaObligaciones);
        }

        return listaCifin;
    }

    IEnumerable<Obligacion> ListarObligacionesVigentes(IEnumerable<Obligacion> listaObligaciones)
    {
        var listaCifin = from obligacion in listaObligaciones
                         where obligacion.EstadoContrato.Trim().ToUpperInvariant() != "NVIG"
                               && obligacion.EstadoObligacion.Trim().ToUpperInvariant() != "SALD"
                         select obligacion;

        return listaCifin;
    }

    void LlenarFooterObligaciones()
    {
        TextBox txtValorInicialFooter = gvCifin.FooterRow.FindControl("txtValorInicial") as TextBox;
        TextBox txtSaldoFooter = gvCifin.FooterRow.FindControl("txtSaldo") as TextBox;
        TextBox txtValorMoraFooter = gvCifin.FooterRow.FindControl("txtValorMora") as TextBox;
        TextBox txtCuotaFooter = gvCifin.FooterRow.FindControl("txtCuota") as TextBox;

        decimal valorInicial = 0;
        decimal saldo = 0;
        decimal valorMora = 0;
        decimal valorCuota = 0;

        foreach (GridViewRow row in gvCifin.Rows)
        {
            CheckBox checkbox = row.FindControl("chkCifin") as CheckBox;
            Label lblNumeroObligacion = row.FindControl("lblNumeroObligacion") as Label;

            if (!checkbox.Checked) continue;

            Label lblValorInicial = row.FindControl("lblValorInicial") as Label;
            Label lblSaldo = row.FindControl("lblSaldo") as Label;
            Label lblValorMora = row.FindControl("lblValorMora") as Label;
            Label lblCuota = row.FindControl("lblCuota") as Label;

            valorInicial += !string.IsNullOrWhiteSpace(lblValorInicial.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(lblValorInicial.Text)) : 0;
            saldo += !string.IsNullOrWhiteSpace(lblSaldo.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(lblSaldo.Text)) : 0;
            valorMora += !string.IsNullOrWhiteSpace(lblValorMora.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(lblValorMora.Text)) : 0;
            valorCuota += !string.IsNullOrWhiteSpace(lblCuota.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(lblCuota.Text)) : 0;
        }

        txtValorInicialFooter.Text = _stringHelper.FormatearNumerosComoCurrency(valorInicial);
        txtSaldoFooter.Text = _stringHelper.FormatearNumerosComoCurrency(saldo);
        txtValorMoraFooter.Text = _stringHelper.FormatearNumerosComoCurrency(valorMora);
        txtCuotaFooter.Text = _stringHelper.FormatearNumerosComoCurrency(valorCuota);
    }

    #endregion


    #region Metodos Ayuda para Calculos


    void CalcularIngresoPorCastigoLinea(string cod_linea = null)
    {
        LineasCreditoService lineaCreditoService = new LineasCreditoService();
        decimal castigoPorcentaje = 0;

        if (cod_linea != null)
        {
            castigoPorcentaje = lineaCreditoService.ConsultarParametrosLinea(cod_linea, "584", _usuario);
        }
        else
        {
            castigoPorcentaje = !string.IsNullOrWhiteSpace(txtPorcentajeCastigo.Text) ? Convert.ToDecimal(txtPorcentajeCastigo.Text) : 0;
        }

        decimal ingresoNeto = Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtTotalIngresos.Text));
        decimal ingresosMasHE = Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtOtrosIngresos.Text));
        decimal ingresoCalculoCastigo = ingresoNeto + ingresosMasHE;

        if (castigoPorcentaje != 0)
        {
            ingresoCalculoCastigo = ((ingresoNeto + ingresosMasHE) * castigoPorcentaje) / 100;
            txtPorcentajeCastigo.Text = castigoPorcentaje.ToString();
        }

        txtIngresoCalculoCastigo.Text = _stringHelper.FormatearNumerosComoCurrency(ingresoCalculoCastigo);
    }

    void ConsultarCondicionesDelCredito(string cod_linea)
    {
        // Clases Data no son Thread-Safe, se requiere una instancia para cada Task para poder hacer esta gracia
        // Afecta mucho menos que hacer la consulta 1 por 1
        LineasCreditoService lineaCreditoService = new LineasCreditoService();
        LineasCreditoService lineaCreditoService1 = new LineasCreditoService();
        LineasCreditoService lineaCreditoService2 = new LineasCreditoService();

        try
        {
            Task<LineasCredito> datosLineaCupo = Task.Factory.StartNew(() => lineaCreditoService.Calcular_Cupo(cod_linea, 0, DateTime.Today, _usuario));
            Task<LineasCredito> datosLineaTasa = Task.Factory.StartNew(() => lineaCreditoService1.ConsultarTasaInteresLineaCredito(cod_linea, _usuario));
            Task<LineasCredito> datosLinea = Task.Factory.StartNew(() => lineaCreditoService2.ConsultarLineasCredito(cod_linea, _usuario));

            Task.WaitAll(datosLineaCupo, datosLineaTasa, datosLinea);

            txtTipoLiquidacion.Text = datosLinea.Result.tipo_liquidacion.ToString();

            txtTasa.Text = datosLineaTasa.Result.tasa.ToString();
            txtTipoTasa.Text = datosLineaTasa.Result.tipotasa.ToString() + "-" + datosLineaTasa.Result.descripcion_tipo_tasa;

            txtPlazoMaximo.Text = datosLineaCupo.Result.Plazo_Maximo.ToString();
            txtMaximoAPrestar.Text = _stringHelper.FormatearNumerosComoCurrency(datosLineaCupo.Result.Monto_Maximo.ToString());
        }
        catch (Exception ex)
        {
            VerError("Error al calcular las condiciones de la linea de credito, " + ex.Message);
            RegistrarPostBack();
        }
    }

    void CalcularCuota()
    {
        SimulacionService simulacionServicio = new SimulacionService();
        string error = string.Empty;

        long monto = !string.IsNullOrWhiteSpace(txtMontoSolicitado.Text) ? Convert.ToInt64(_stringHelper.DesformatearNumerosEnteros(txtMontoSolicitado.Text)) : 0;
        int periodicidad = !string.IsNullOrWhiteSpace(ddlPeriodicidad.SelectedValue) ? Convert.ToInt32(ddlPeriodicidad.SelectedValue) : 1;
        int plazo = !string.IsNullOrWhiteSpace(txtPlazo.Text) ? Convert.ToInt32(txtPlazo.Text) : 0;
        int tipoLiquidacion = !string.IsNullOrWhiteSpace(txtTipoLiquidacion.Text) ? Convert.ToInt32(txtTipoLiquidacion.Text) : 0;
        decimal tasa = !string.IsNullOrWhiteSpace(txtTasa.Text) ? Convert.ToDecimal(txtTasa.Text) : 0;
        DateTime? fecha = FechaPrimPag.Text != "" ? Convert.ToDateTime(FechaPrimPag.Text) : Convert.ToDateTime(null);

        if (monto == 0 || plazo == 0) return;

        Simulacion simulacion = simulacionServicio.ConsultarSimulacionCuota(monto, plazo, periodicidad, ddlLineaCredito.SelectedValue, tipoLiquidacion, tasa, 0, 0, fecha, ref error, _usuario, _codPersona);

        if (string.IsNullOrWhiteSpace(error))
        {
            txtCuotaMensualAprox.Text = _stringHelper.FormatearNumerosComoCurrency(simulacion.cuota);
        }
        else
        {
            txtCuotaMensualAprox.Text = _stringHelper.FormatearNumerosComoCurrency("0");
            VerError(error);
            RegistrarPostBack();
        }
    }

    void CalcularPorcentajeSalarioComprometido()
    {
        string cuotaMensual = _stringHelper.DesformatearNumerosEnteros(txtCuotaMensualAprox.Text);

        if (!string.IsNullOrWhiteSpace(cuotaMensual) && cuotaMensual != "0")
        {
            string ingresosTotalesString = _stringHelper.DesformatearNumerosEnteros(txtTotalIngresos.Text) != "0" ? _stringHelper.DesformatearNumerosEnteros(txtTotalIngresos.Text) : "1";

            decimal deducciones = !string.IsNullOrWhiteSpace(txtTotalDeducciones.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtTotalDeducciones.Text)) : 0;
            decimal cuotaTotal = !string.IsNullOrWhiteSpace(txtTotalCuota.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtTotalCuota.Text)) : 0;
            decimal cuotaMensualAproximada = Convert.ToDecimal(cuotaMensual);
            decimal ingresosTotales = !string.IsNullOrWhiteSpace(ingresosTotalesString) ? Convert.ToDecimal(ingresosTotalesString) : 1;

            decimal salarioComprometido = ((deducciones + cuotaTotal + cuotaMensualAproximada) / ingresosTotales) * 100;
            salarioComprometido = Math.Round(salarioComprometido, 2);

            txtPorcentajeSalarioComprometido.Text = salarioComprometido.ToString();

            if (salarioComprometido > 60)
            {
                lblViable.Text = "No Viable";
            }
            else if (salarioComprometido > 40)
            {
                lblViable.Text = "Viable pero debe avisar violación del 40%";
            }
            else if (salarioComprometido > 0)
            {
                lblViable.Text = "Viable";
            }
        }
        else
        {
            txtPorcentajeSalarioComprometido.Text = "0";
        }
    }

    void CalcularCapacidadMensualScore()
    {
        decimal deducciones = !string.IsNullOrWhiteSpace(txtTotalDeducciones.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtTotalDeducciones.Text)) : 0;
        decimal cuotaTotal = !string.IsNullOrWhiteSpace(txtTotalCuota.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtTotalCuota.Text)) : 0;
        decimal ingresoCalculoCastigo = !string.IsNullOrWhiteSpace(txtTotalIngresos.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtTotalIngresos.Text)) : 1;

        decimal capacidadMensual = ingresoCalculoCastigo - deducciones - cuotaTotal;

        txtCapacidadMensualScore.Text = _stringHelper.FormatearNumerosComoCurrency(capacidadMensual);
    }

    bool ValidarCondicionesLineaCredito(int plazo, decimal monto)
    {
        lblMensajePlazo.Visible = false;
        lblMensajeMonto.Visible = false;
        bool valido = true;

        int plazoMaximo = Convert.ToInt32(txtPlazoMaximo.Text);

        if (plazo > plazoMaximo)
        {
            txtPlazo.Text = string.Empty;
            lblMensajePlazo.Visible = true;
            lblViable.Text = string.Empty;
            valido = false;
        }

        decimal montoMaximo = Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtMaximoAPrestar.Text));

        if (monto > montoMaximo)
        {
            txtMontoSolicitado.Text = string.Empty;
            lblMensajeMonto.Visible = true;
            lblViable.Text = string.Empty;
            valido = false;
        }

        return valido;
    }

    Scoring ConsultarFactorAntiguedad()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtFechaIngreso.Text)) return null;

            DateTimeHelper dateHelper = new DateTimeHelper();
            DateTime fechaIngreso = Convert.ToDateTime(txtFechaIngreso.Text);
            long diferenciaFechas = dateHelper.DiferenciaEntreDosFechasDias(DateTime.Today, fechaIngreso); //Modificado para calcular diferencia en dias

            Scoring factor = _scoringService.ConsultarFactorAntiguedad(diferenciaFechas, _usuario);
            return factor;
        }
        catch (Exception ex)
        {
            VerError("Error al consultar factor de antiguedad, " + ex.Message);
            RegistrarPostBack();
            return null;
        }
    }

    void LlenarFactoresAlerta(Scoring factor)
    {
        decimal totalIngresos = Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtTotalIngresos.Text));
        decimal montoAporte = Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtMontoAportes.Text));
        decimal salarioBasico = Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtSalarioBasico.Text));

        decimal calculo = (factor.factor_pendiente * factor.fecha_meses_antiguedad) + factor.factor_intercepto;
        decimal maxConCodeudor = (calculo * totalIngresos) + montoAporte;
        txtMaximoCodeudor.Text = _stringHelper.FormatearNumerosComoCurrency(maxConCodeudor);

        General diasScoringRango1 = ConsultarParametroGeneral(24, _usuario);
        General diasScoringRango2 = ConsultarParametroGeneral(26, _usuario);

        if (diasScoringRango1 != null && diasScoringRango2 != null)
        {
            if (diasScoringRango1.valor.Trim() != "" && diasScoringRango2.valor.Trim() != "")
            {
                if (factor.fecha_meses_antiguedad >= Convert.ToInt64(diasScoringRango1.valor) && factor.fecha_meses_antiguedad <= Convert.ToInt64(diasScoringRango2.valor))
                {
                    lblAntiguedad.Text = "PUEDE SIN CODEUDOR";
                }
                else
                {
                    lblAntiguedad.Text = "SOLICITAR CODEUDOR";

                    decimal maxSinCodeudor = (salarioBasico * 3) + montoAporte;
                    txtMaximoSinCodeudor.Text = _stringHelper.FormatearNumerosComoCurrency(maxSinCodeudor);
                }


                if (factor.fecha_meses_antiguedad >= Convert.ToInt64(diasScoringRango2.valor))
                {
                    lblAntiguedad5.Text = "PUEDE SIN CODEUDOR";
                }
                else
                {
                    lblAntiguedad5.Text = "SOLICITAR CODEUDOR";

                    decimal maxSinCodeudor = (salarioBasico * 5) + montoAporte;
                    txtMaximoSinCodeudor5.Text = _stringHelper.FormatearNumerosComoCurrency(maxSinCodeudor);
                }
            }
        }
        else
        {
            VerError("Debe configurar los parámetros generales 24 y 26 que determina parámetros para días de antiguedad");
        }

    }

    void LlenarTipoGarantia()
    {
        string montoString = !string.IsNullOrWhiteSpace(txtMontoSolicitado.Text) ? _stringHelper.DesformatearNumerosEnteros(txtMontoSolicitado.Text) : "0";
        decimal montoMaximoConCodeudor = Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtMaximoCodeudor.Text));
        decimal monto = Convert.ToDecimal(montoString);

        TextBox txtTotalValorCuotaFooter = gvEstadoCuenta.FooterRow.FindControl("txtTotalValorCuotaFooter") as TextBox;
        decimal totalValorCuota = Convert.ToDecimal(_stringHelper.DesformatearNumerosDecimales(txtTotalValorCuotaFooter.Text));

        if (montoMaximoConCodeudor >= (monto + totalValorCuota))
        {
            lblTipoGarantia.Text = "CODEUDOR";
        }
        else
        {
            lblTipoGarantia.Text = "GARANTÍA REAL";
        }
    }

    void CalcularTotalesCartera()
    {
        TextBox txtTotalSaldoEstadoCuenta = gvEstadoCuenta.FooterRow.FindControl("txtTotalSaldoFooter") as TextBox;
        TextBox txtTotalValorCuotaEstadoCuenta = gvEstadoCuenta.FooterRow.FindControl("txtTotalValorCuotaFooter") as TextBox;

        TextBox txtTotalSaldoCifin = gvCifin.FooterRow.FindControl("txtSaldo") as TextBox;
        TextBox txtTotalCuotaCifin = gvCifin.FooterRow.FindControl("txtCuota") as TextBox;

        decimal totalSaldoEstadoCuenta = !string.IsNullOrWhiteSpace(txtTotalSaldoEstadoCuenta.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtTotalSaldoEstadoCuenta.Text)) : 0;
        decimal totalValorCuotaEstadoCuenta = !string.IsNullOrWhiteSpace(txtTotalValorCuotaEstadoCuenta.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtTotalValorCuotaEstadoCuenta.Text)) : 0;

        decimal totalSaldoCifin = !string.IsNullOrWhiteSpace(txtTotalSaldoCifin.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtTotalSaldoCifin.Text)) : 0;
        decimal totalCuotaCifin = !string.IsNullOrWhiteSpace(txtTotalCuotaCifin.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtTotalCuotaCifin.Text)) : 0;

        txtTotalCuota.Text = _stringHelper.FormatearNumerosComoCurrency(totalValorCuotaEstadoCuenta + totalCuotaCifin);
        txtTotalSaldo.Text = _stringHelper.FormatearNumerosComoCurrency(totalSaldoEstadoCuenta + totalSaldoCifin);
    }

    //Agregado para guardar información económica del cliente
    private EstadosFinancieros InformacionFinanciera()
    {
        EstadosFinancieros vEstadoPersona = new EstadosFinancieros();
        EstadosFinancierosService EstadosServicio = new EstadosFinancierosService();
        vEstadoPersona = EstadosServicio.listarperosnainfofin(Convert.ToInt64(txtCodPersona.Text), (Usuario)Session["usuario"]);

        if (!string.IsNullOrWhiteSpace(txtSalarioBasico.Text))
        {
            vEstadoPersona.sueldo = Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtSalarioBasico.Text));
        }
        if (!string.IsNullOrWhiteSpace(txtTotalIngresos.Text))
        {
            vEstadoPersona.totalingreso = Convert.ToDecimal(_stringHelper.DesformatearNumerosEnteros(txtTotalIngresos.Text));
        }

        return vEstadoPersona;
    }

    #endregion


    #region Llenar Reporte


    void LlenarReporte()
    {
        string cRutaDeImagen = new Uri(Server.MapPath("~/Images/LogoEmpresa.jpg")).AbsoluteUri;
        //List<Credito> lstEstadoCuenta = (List<Credito>)ViewState["lstEstadoCuenta"];
        //string[] columnas = new string[] { "fecha_desembolso_nullable", "linea_credito", "numero_radicacion", "monto_aprobado", "saldo_capital", "intcoriente", "valor_cuota" };

        List<Credito> listareporte = new List<Credito>();
        int contp = 0;

        foreach (GridViewRow row in gvEstadoCuenta.Rows)
        {
            CheckBox CheckBoxgv = row.FindControl("CheckBoxgv") as CheckBox;
            if (CheckBoxgv != null)
            {
                if (CheckBoxgv.Checked == true)
                {
                    Credito entidad = new Credito();

                    //string pFecha 
                    entidad.fecha_desembolso_nullable = Convert.ToDateTime(gvEstadoCuenta.Rows[contp].Cells[1].Text);
                    entidad.linea_credito = gvEstadoCuenta.Rows[contp].Cells[2].Text;
                    entidad.numero_radicacion = Convert.ToInt64(gvEstadoCuenta.Rows[contp].Cells[3].Text);
                    Label VI = gvEstadoCuenta.Rows[contp].FindControl("lblValorInicial") as Label;
                    entidad.monto_aprobado = Convert.ToDecimal(VI.Text.TrimStart('$'));
                    Label Sl = gvEstadoCuenta.Rows[contp].FindControl("lblSaldo") as Label;
                    entidad.saldo_capital = Convert.ToDecimal(Sl.Text.TrimStart('$'));
                    Label Ints = gvEstadoCuenta.Rows[contp].FindControl("lblInt") as Label;
                    entidad.intcoriente = Convert.ToDecimal(Ints.Text.TrimStart('$'));
                    Label VC = gvEstadoCuenta.Rows[contp].FindControl("lblValorCuota") as Label;
                    entidad.valor_cuota = Convert.ToDecimal(VC.Text.TrimStart('$'));

                    listareporte.Add(entidad);

                }
            }
            contp++;
        }

        string[] columnasCifin = new string[] { "PaqueteInformacion", "NombreEntidad", "NumeroObligacion", "Reestructurado", "Calidad", "EstadoObligacion", "ValorInicial", "SaldoObligacion", "ValorMora", "ValorCuota" };

        // Lleno DataTable
        DataTable dtEstadoCuenta = listareporte.ToDataTable();
        DataTable dtCifin = ObtenerListaObligacion().ToDataTable(columnasCifin);

        // Lleno los parametros del reporte
        ReportParameter[] param = LlenarParametrosReporte(cRutaDeImagen);

        // Limpiar DatSource de Reporte
        ReportViewerPlan.LocalReport.DataSources.Clear();

        // Cargo los DataTables al DataSource del reporte y lo muestro
        ReportDataSource rds1 = new ReportDataSource("DataSetScoringCre", dtEstadoCuenta);
        ReportViewerPlan.LocalReport.DataSources.Add(rds1);

        ReportDataSource rds2 = new ReportDataSource("DataSetCifin", dtCifin);
        ReportViewerPlan.LocalReport.DataSources.Add(rds2);

        ReportViewerPlan.LocalReport.EnableExternalImages = true;
        ReportViewerPlan.LocalReport.SetParameters(param);
        ReportViewerPlan.LocalReport.Refresh();
    }

    List<Obligacion> ObtenerListaObligacion()
    {
        List<Obligacion> lstObligacion = new List<Obligacion>();

        foreach (GridViewRow row in gvCifin.Rows)
        {
            CheckBox checkbox = row.FindControl("chkCifin") as CheckBox;
            if (!checkbox.Checked) continue;

            Obligacion obligacion = new Obligacion()
            {
                PaqueteInformacion = (row.FindControl("lblObligaciones") as Label).Text,
                NombreEntidad = (row.FindControl("lblNombreEntidad") as Label).Text,
                NumeroObligacion = (row.FindControl("lblNumeroObligacion") as Label).Text,
                Reestructurado = (row.FindControl("lblReestrucutado") as Label).Text,
                Calidad = (row.FindControl("lblCalidad") as Label).Text,
                EstadoObligacion = (row.FindControl("lblEstadoObligacion") as Label).Text,
                ValorInicial = (row.FindControl("lblValorInicial") as Label).Text,
                SaldoObligacion = (row.FindControl("lblSaldo") as Label).Text,
                ValorMora = (row.FindControl("lblValorMora") as Label).Text,
                ValorCuota = (row.FindControl("lblCuota") as Label).Text
            };

            lstObligacion.Add(obligacion);
        }

        return lstObligacion;
    }

    ReportParameter[] LlenarParametrosReporte(string cRutaDeImagen)
    {
        ReportParameter[] param = new ReportParameter[69];

        param[0] = new ReportParameter("ImagenReport", cRutaDeImagen);
        param[1] = new ReportParameter("NITEmpresa", _usuario.nitempresa);
        param[2] = new ReportParameter("FechaScoreHoy", DateTime.Today.ToLongDateString());
        param[3] = new ReportParameter("EmpresaNombre", _usuario.empresa);
        param[4] = new ReportParameter("CodigoNomina", txtCodigoNomina.Text);
        param[5] = new ReportParameter("TipoIdentificacion", txtTipoIdentificacion.Text);
        param[6] = new ReportParameter("Identificacion", txtCedula.Text);
        param[7] = new ReportParameter("Nombre", TxtNombres.Text);
        param[8] = new ReportParameter("Apellido", txtApellidos.Text);
        param[9] = new ReportParameter("CodigoAsociado", txtCodigoAsociado.Text);
        param[10] = new ReportParameter("MontoAportes", txtMontoAportes.Text);
        param[11] = new ReportParameter("SalarioVariable", txtSalarioVariable.Text);
        param[12] = new ReportParameter("IngresosTotal", txtTotalIngresos.Text);
        param[13] = new ReportParameter("OtrosIngresos", txtOtrosIngresos.Text);
        param[14] = new ReportParameter("IngresoCastigoLinea", txtIngresoCalculoCastigo.Text);
        param[15] = new ReportParameter("PorcentajeCastigo", txtPorcentajeCastigo.Text);

        param[16] = new ReportParameter("NombreEmpresaAsociado", txtEmpresa.Text);
        param[17] = new ReportParameter("CargoAsociado", txtCargo.Text);
        param[18] = new ReportParameter("Ubicacion", txtUbicacion.Text);
        param[19] = new ReportParameter("Identificacion", txtCedula.Text);
        param[20] = new ReportParameter("FechaIngreso", txtFechaIngreso.Text);
        param[21] = new ReportParameter("FechaAfiliacion", txtFechaAfiliacion.Text);
        param[22] = new ReportParameter("Parafiscales", txtParafiscales.Text);
        param[23] = new ReportParameter("Retencion", _stringHelper.FormatearNumerosComoCurrency(string.IsNullOrEmpty(txtRetencion.Text) ? "$ 0" : txtRetencion.Text));
        param[24] = new ReportParameter("CuotaAporte", txtCuotaAporteObligatorio.Text);
        param[25] = new ReportParameter("Servicio", _stringHelper.FormatearNumerosComoCurrency(string.IsNullOrEmpty(txtServicios.Text) ? "$ 0" : txtServicios.Text));
        param[26] = new ReportParameter("AhorrosVoluntarios", txtAhorrosVoluntarios.Text);
        param[27] = new ReportParameter("PensionesVoluntarias", _stringHelper.FormatearNumerosComoCurrency(string.IsNullOrEmpty(txtPensionesVoluntarias.Text) ? "$ 0" : txtPensionesVoluntarias.Text));
        param[28] = new ReportParameter("TotalDeducciones", txtTotalDeducciones.Text);

        param[29] = new ReportParameter("TotalCarteraSaldo", txtTotalSaldo.Text);
        param[30] = new ReportParameter("TotalCarteraCuota", txtTotalCuota.Text);
        param[31] = new ReportParameter("Observaciones", txtObservaciones.Text);
        param[32] = new ReportParameter("LineaCredito", ddlLineaCredito.SelectedItem.Text);
        param[33] = new ReportParameter("MontoSolicitado", _stringHelper.FormatearNumerosComoCurrency(string.IsNullOrEmpty(txtMontoSolicitado.Text) ? "$ 0" : txtMontoSolicitado.Text));
        param[34] = new ReportParameter("Plazo", txtPlazo.Text);
        param[35] = new ReportParameter("Tasa", txtTasa.Text);
        param[36] = new ReportParameter("TipoTasa", txtTipoTasa.Text);
        param[37] = new ReportParameter("CuotaAproximada", txtCuotaMensualAprox.Text);

        param[38] = new ReportParameter("CapacidadMensualScore", txtCapacidadMensualScore.Text);
        param[39] = new ReportParameter("SalarioComprometido", txtPorcentajeSalarioComprometido.Text);

        param[40] = new ReportParameter("XPrimas", Primas.Text);
        param[41] = new ReportParameter("XCesantias", Cesantias.Text);

        param[42] = new ReportParameter("PrimaComprometida", _stringHelper.FormatearNumerosComoCurrency(string.IsNullOrEmpty(ValorPrima.Text) ? "$ 0" : ValorPrima.Text));
        param[43] = new ReportParameter("CuotaConCompromiso", _stringHelper.FormatearNumerosComoCurrency(string.IsNullOrEmpty(txtValorCuotaCompromiso.Text) ? "$ 0" : txtValorCuotaCompromiso.Text));
        param[44] = new ReportParameter("PlazoMaximo", txtPlazoMaximo.Text);
        param[45] = new ReportParameter("MontoMaximo", txtMaximoAPrestar.Text);
        param[46] = new ReportParameter("MaximoConCodeudor", txtMaximoCodeudor.Text);
        param[47] = new ReportParameter("GarantiaAnos", lblAntiguedad.Text);
        param[48] = new ReportParameter("MaximoSinCodeudor", txtMaximoSinCodeudor.Text);

        param[49] = new ReportParameter("TipoGarantia", lblTipoGarantia.Text);
        param[50] = new ReportParameter("Viabilidad", lblViable.Text);
        param[51] = new ReportParameter("SalarioBasico", txtSalarioBasico.Text);
        param[52] = new ReportParameter("Periodicidad", ddlPeriodicidad.SelectedItem.Text);
        param[53] = new ReportParameter("UsuarioElaboro", Usuario.nombre);
        param[54] = new ReportParameter("GarantiaAnos", lblAntiguedad5.Text);
        param[55] = new ReportParameter("MaximoSinCodeudor", txtMaximoSinCodeudor5.Text);

        param[56] = new ReportParameter("EstadoPersona", txtEstadoPersona.Text);
        param[57] = new ReportParameter("FechaConsulta", txtFechaConsulta.Text);
        param[58] = new ReportParameter("PuntajeScore", txtPuntajeScore.Text);
        param[59] = new ReportParameter("ProbMora", txtProbMora.Text);

        TextBox txtValorInicialFooter = gvCifin.FooterRow.FindControl("txtValorInicial") as TextBox;
        TextBox txtSaldoFooter = gvCifin.FooterRow.FindControl("txtSaldo") as TextBox;
        TextBox txtValorMoraFooter = gvCifin.FooterRow.FindControl("txtValorMora") as TextBox;
        TextBox txtCuotaFooter = gvCifin.FooterRow.FindControl("txtCuota") as TextBox;

        param[60] = new ReportParameter("TotalValorInicialCifin", txtValorInicialFooter.Text);
        param[61] = new ReportParameter("TotalSaldoCifin", txtSaldoFooter.Text);
        param[62] = new ReportParameter("TotalValorMoraCifin", txtValorMoraFooter.Text);
        param[63] = new ReportParameter("TotalValorCuotaCifin", txtCuotaFooter.Text);

        param[64] = new ReportParameter("FechaPrimerPago", FechaPrimPag.Text);
        param[65] = new ReportParameter("FechaPrima", txtFechaPrima.Text);
        param[66] = new ReportParameter("CesantiasComprometida", _stringHelper.FormatearNumerosComoCurrency(string.IsNullOrEmpty(ValorCesantias.Text) ? "$ 0" : ValorCesantias.Text));
        param[67] = new ReportParameter("FechaCesantias", txtFechaCesan.Text);
        param[68] = new ReportParameter("CuentasAFC", _stringHelper.FormatearNumerosComoCurrency(string.IsNullOrEmpty(txtCuentasAFC.Text) ? "$ 0" : txtCuentasAFC.Text));


        return param;
    }

    #endregion

}
