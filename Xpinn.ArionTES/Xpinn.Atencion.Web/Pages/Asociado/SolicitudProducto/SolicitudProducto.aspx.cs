using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;


public partial class SolicitudProducto : GlobalWeb
{
    decimal tasa = 0;
    xpinnWSDeposito.WSDepositoSoapClient BODeposito = new xpinnWSDeposito.WSDepositoSoapClient();
    xpinnWSCredito.WSCreditoSoapClient BOCredito = new xpinnWSCredito.WSCreditoSoapClient();
    xpinnWSAppFinancial.WSAppFinancialSoapClient BOFinancial = new xpinnWSAppFinancial.WSAppFinancialSoapClient();
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient BOEstadoCuenta = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    xpinnWSLogin.Persona1 Datospersona = new xpinnWSLogin.Persona1();
    List<xpinnWSAppFinancial.DocumentosAnexos> lstDocs = new List<xpinnWSAppFinancial.DocumentosAnexos>();
    xpinnWSLogin.Persona1 pPersona;
    xpinnWSIntegracion.WSintegracionSoapClient wsIntegra = new xpinnWSIntegracion.WSintegracionSoapClient();
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ValidarSession();
            VisualizarTitulo(OptionsUrl.AperturaDeposito, "Sol");
            Site toolBar = (Site)Master;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("SolicitudProducto", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            obtenerDatos();
        }
    }

    private void obtenerDatos()
    {
        txtFechaSolicitud.Text = DateTime.Today.ToString("dd/MM/yyyy");
        Datospersona = (xpinnWSLogin.Persona1)Session["persona"];
        xpinnWSCredito.Persona Persona = BOCredito.ConsultarPersona(Datospersona.cod_persona, Datospersona.clavesinecriptar, Session["sec"].ToString());
        if (Persona.PrimerNombre != "" && Persona.PrimerApellido != "")
            Persona.PrimerNombre = Persona.PrimerNombre.Trim() + " " + Persona.PrimerApellido.Trim();
        Persona.SegundoNombre = Persona.Ciudad.nomciudad;
        if (Persona.Asesor.PrimerNombre != null)
            txtAsesor.Text = Persona.Asesor.PrimerNombre;
        List<xpinnWSCredito.Persona> lstData = new List<xpinnWSCredito.Persona>();
        lstData.Add(Persona);
        frvData.DataSource = lstData;
        frvData.DataBind();
        txtCodPersona.Text = Datospersona.cod_persona.ToString();
    
    }


    /// <summary>
    /// Valida la sesión del usuario 
    /// </summary>
    public void ValidarSession()
    {
        if (Session["persona"] == null)
            Response.Redirect("~/Pages/Account/FinSesion.htm");
    }

    protected void frvData_DataBound(object sender, EventArgs e)
    {
        Label lblFechaAfiliacion = (Label)frvData.FindControl("lblFechaAfiliacion");
        if (lblFechaAfiliacion != null)
        {
            if (!string.IsNullOrWhiteSpace(lblFechaAfiliacion.Text))
            {
                if (Convert.ToDateTime(lblFechaAfiliacion.Text) == DateTime.MinValue)
                    lblFechaAfiliacion.Text = "";
            }
        }
    }

    /// <summary>
    /// Almacena la solicitud de producto
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGuardarSolicitud_Click(object sender, EventArgs e)
    {
        if (ValidarCampos())
        {
            //Crea el objeto con los datos de la solicitud
            xpinnWSLogin.Persona1 Datospersona = (xpinnWSLogin.Persona1)Session["persona"];
            string salida = "";
            string opc = ddlProducto.SelectedValue.ToString();
            switch (opc)
            {
                case "3": //Ahorros
                    xpinnWSAppFinancial.AhorroVista Ahorros = new xpinnWSAppFinancial.AhorroVista();
                    Ahorros.cod_persona = Datospersona.cod_persona;
                    Ahorros.tipo_producto = Convert.ToInt64(ddlProducto.SelectedValue);
                    Ahorros.cod_linea_ahorro = ddlVLinea.SelectedValue;
                    Ahorros.valor_cuota = Convert.ToDecimal(txtVValorCuota.Text.Replace(".", "").Replace(",", "").Replace("$", ""));
                    Ahorros.cod_periodicidad = Convert.ToInt32(ddlComunPeriodicidad.SelectedValue);
                    Ahorros.cod_forma_pago = Convert.ToInt32(ddlComunFormaPago.SelectedValue);
                    string pBeneficiarios = ConfigurationManager.AppSettings["AhorrosParaBeneficiarios"] != null ?
                        ConfigurationManager.AppSettings["AhorrosParaBeneficiarios"].ToString() : "0";

                    if (pBeneficiarios=="1")
                    {
                        Ahorros.nombres_ben = txtVNombre.Text;
                        Ahorros.tipo_identificacion_ben = Convert.ToInt32(ddlTipoIdentificacion.SelectedValue);
                        Ahorros.parentesco_ben = Convert.ToInt32(ddlparentesco.SelectedValue);
                        Ahorros.identificacion_ben = txtVidentificacion.Text;
                        Ahorros.fecha_nacimiento_ben = DateTime.ParseExact(txtVfecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                  
                    Ahorros.estado_modificacion1 = 0;
                    salida = BOFinancial.GrabarSolicitudAhorros(Ahorros, Session["sec"].ToString());
                    break;
                case "5": //CDAT
                    if (validarCDAT(ddlCLinea.SelectedValue))
                    {
                        xpinnWSAppFinancial.Cdat Cdat = new xpinnWSAppFinancial.Cdat();
                        Cdat.cod_persona = Datospersona.cod_persona;
                        Cdat.cod_tipo_producto = Convert.ToInt32(ddlProducto.SelectedValue);
                        Cdat.cod_lineacdat = ddlCLinea.SelectedValue;
                        Cdat.plazo = Convert.ToInt32(ddlCPlazo.SelectedValue);
                        Cdat.valor = Convert.ToDecimal(txtCValor.Text.Replace(".", "").Replace(",", "").Replace("$", ""));
                        Cdat.estado_modificacion1 = 0;
                        if (Session["docs"] != null)
                        {
                            lstDocs = Session["docs"] as List<xpinnWSAppFinancial.DocumentosAnexos>;
                            //CARGAR DOCUMENTOS EN CONSIGNACION Y DECLARACION                            
                        }
                        salida = BOFinancial.GrabarSolicitudCDAT(Cdat, Session["sec"].ToString(), lstDocs);
                        Session["docs"] = null;
                        Session["IdSolicitud"]= salida;
                    }
                    break;
                case "9"://Programado
                    xpinnWSAppFinancial.CuentasProgramado ahoProgra = new xpinnWSAppFinancial.CuentasProgramado();
                    ahoProgra.cod_persona = Datospersona.cod_persona;
                    ahoProgra.cod_tipo_producto = Convert.ToInt32(ddlProducto.SelectedValue);
                    ahoProgra.cod_linea_programado = ddlALinea.SelectedValue;
                    ahoProgra.cuota = Convert.ToInt32(ddlANumerocuotas.SelectedValue);
                    ahoProgra.valor_cuota = Convert.ToDecimal(txtAValorCuota.Text.Replace(".", "").Replace(",", "").Replace("$", ""));
                    ahoProgra.cod_periodicidad = Convert.ToInt64(ddlComunPeriodicidad.SelectedValue);
                    ahoProgra.cod_forma_pago = Convert.ToInt32(ddlComunFormaPago.SelectedValue);
                    ahoProgra.cod_destino = Convert.ToInt64(ddlADestinacion.SelectedValue);
                    ahoProgra.estado_modificacion1 = 0;
                    salida = BOFinancial.GrabarSolicitudAhorroProgramado(ahoProgra, Session["sec"].ToString());
                    break;
                default:
                    Error("Tipo de producto no válido");
                    break;
            }
            if (!string.IsNullOrWhiteSpace(salida))
            {
                //LimpiarCampos();
                lblCodigoGenerado.Text = salida;
                panelGeneral.Visible = false;
                //panelFinal.Visible = true;
                IniciarProcesoPago();
               
            }
            else
            {
                Error("Se presentó un problema al hacer el registro");
            }
        }
    }

    private bool validarCDAT(string selectedValue)
    {
        Error();
        if (string.IsNullOrEmpty(ddlCPlazo.SelectedValue))
        {
            Error("Digite el plazo del CDAT");
            return false;
        }
        if (string.IsNullOrEmpty(txtCValor.Text))
        {
            Error("Digite el valor del CDAT");
            return false;
        }
        xpinnWSDeposito.LineaCDAT lineaCdat = Session["linea"] as xpinnWSDeposito.LineaCDAT;
        xpinnWSDeposito.RangoCDAT rango = new xpinnWSDeposito.RangoCDAT();
        rango = (from xpinnWSDeposito.RangoCDAT valores in lineaCdat.lstRangos
                 where valores.tipo_tope == 1
                 select valores).ToList()[0];
        if (Convert.ToDecimal(txtCValor.Text.Replace(".", "").Replace(",", "").Replace("$", "")) > Convert.ToDecimal(rango.maximo))
        {
            Error("El valor máximo para esta línea de CDAT es " + rango.maximo + "");
            return false;
        }

        return true;
    }

    protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
    {
        Error();
        string opc = ddlProducto.SelectedValue.ToString();
        switch (opc)
        {
            case "3": //Ahorros
                pnlVista.Visible = true;
                pnlAhoProgra.Visible = false;
                pnlCDATS.Visible = false;
                CargarDatosAhorro();
                CargarDatosComun();
                break;
            case "5": //CDATS
                pnlVista.Visible = false;
                pnlAhoProgra.Visible = false;
                pnlCDATS.Visible = true;
                CargarDatosCDAT();
                break;
            case "9": //Programado
                pnlVista.Visible = false;
                pnlAhoProgra.Visible = true;
                pnlCDATS.Visible = false;
                txtAValorCuota.Text = "";
                CargarDatosAhoProgra();
                CargarDatosComun();
                break;
            default:
                pnlCDATS.Visible = false;
                pnlAhoProgra.Visible = false;
                pnlAhoProgra.Visible = false;
                Error();
                break;
        }
    }

    /// <summary>
    /// Carga Periodicidad y forma de pago para ahorros a la Vista y Programado
    /// </summary>
    private void CargarDatosComun()
    {
        Error();
        //Cargar Periodicidad
        Datospersona = (xpinnWSLogin.Persona1)Session["persona"];
        xpinnWSAppFinancial.Periodicidad pPeriod = new xpinnWSAppFinancial.Periodicidad();
        List<xpinnWSAppFinancial.Periodicidad> lstPeriodicidad = BOFinancial.ListarPeriodicidades(pPeriod, Session["sec"].ToString());
        ddlComunPeriodicidad.DataTextField = "Descripcion";
        ddlComunPeriodicidad.DataValueField = "Codigo";
        ddlComunPeriodicidad.DataSource = lstPeriodicidad;
        ddlComunPeriodicidad.DataBind();

        string pCodPeriodicidad = ConfigurationManager.AppSettings["Periodicidad"] != null ?
                ConfigurationManager.AppSettings["Periodicidad"].ToString() : string.Empty;
        if (!string.IsNullOrEmpty(pCodPeriodicidad))
        {
            ddlComunPeriodicidad.SelectedValue = pCodPeriodicidad;
            ddlComunPeriodicidad.Enabled = false;
        }

        string pFomaPago = ConfigurationManager.AppSettings["ddlFormaPago"] != null ?
                ConfigurationManager.AppSettings["ddlFormaPago"].ToString() : string.Empty;
        if (!string.IsNullOrEmpty(pFomaPago))
        {
            ddlComunFormaPago.SelectedValue = pFomaPago;
            ddlComunFormaPago.Enabled = false;
        }
    }

    /// <summary>
    /// Carga las líneas de CDATS
    /// </summary>
    private void CargarDatosCDAT()
    {
        Error();
        //Cargar lineas CDATS
        Datospersona = (xpinnWSLogin.Persona1)Session["persona"];
        xpinnWSAppFinancial.LineaCDAT pLinea = new xpinnWSAppFinancial.LineaCDAT();
        pLinea.estado = 1;
        ddlCLinea.Items.Clear();
        ddlCLinea.Items.Add(new ListItem("Seleccione un item", "0"));
        List<xpinnWSAppFinancial.LineaCDAT> lstLineas = BOFinancial.ListarLineasCDAT(pLinea, Session["sec"].ToString());
        ddlCLinea.DataTextField = "descripcion";
        ddlCLinea.DataValueField = "cod_lineacdat";
        ddlCLinea.DataSource = lstLineas.OrderBy(x=> x.plazo_minimo);
        ddlCLinea.DataBind();
    }

    /// <summary>
    /// Carga Lineas y destinación de Ahorro Programado
    /// </summary>
    private void CargarDatosAhoProgra()
    {
        Error();
        //Cargar lineas Ahorro Programado
        Datospersona = (xpinnWSLogin.Persona1)Session["persona"];
        ddlALinea.Items.Clear();
        ddlALinea.Items.Add(new ListItem("Seleccione un item", "0"));
        List<xpinnWSAppFinancial.LineasProgramado> lstLineas = BOFinancial.ListarLineasAhoProgramado(" and estado = 1 ", Session["sec"].ToString());
        ddlALinea.DataTextField = "nombre";
        ddlALinea.DataValueField = "cod_linea_programado";
        ddlALinea.DataSource = lstLineas;
        ddlALinea.DataBind();

        //Cargar destinaciones Ahorro Programado
        xpinnWSAppFinancial.Destinacion destina = new xpinnWSAppFinancial.Destinacion();
        ddlADestinacion.Items.Clear();
        ddlADestinacion.Items.Add(new ListItem("Seleccione un item", "0"));
        List<xpinnWSAppFinancial.Destinacion> lstDestinacion = BOFinancial.ListarDestinacion(destina, Session["sec"].ToString());
        ddlADestinacion.DataTextField = "descripcion";
        ddlADestinacion.DataValueField = "cod_destino";
        ddlADestinacion.DataSource = lstDestinacion;
        ddlADestinacion.DataBind();
    }

    /// <summary>
    /// Carga las lineas de Ahorro a la Vista
    /// </summary>
    private void CargarDatosAhorro()
    {
        Error();
        //Cargar lineas Ahorro a la vista
        Datospersona = (xpinnWSLogin.Persona1)Session["persona"];
        xpinnWSAppFinancial.LineaAhorro pLinea = new xpinnWSAppFinancial.LineaAhorro();
        ddlVLinea.Items.Clear();
        ddlVLinea.Items.Add(new ListItem("Seleccione un item", "0"));
        List<xpinnWSAppFinancial.LineaAhorro> lstLineas = BOFinancial.ListarLineasAhorros(pLinea, Session["sec"].ToString());
        ddlVLinea.DataTextField = "descripcion";
        ddlVLinea.DataValueField = "cod_linea_ahorro";
        ddlVLinea.DataSource = lstLineas.Where(p => p.estado == 1).ToList();
        ddlVLinea.DataBind();
        tasa = !string.IsNullOrEmpty(lstLineas[0].tasa.ToString()) ? Convert.ToDecimal(lstLineas[0].tasa.ToString()) / 100 : 0;
        txtVTasa.Text = !string.IsNullOrEmpty(lstLineas[0].tasa.ToString()) ? tasa.ToString("P") : "";
        double efectiva = ((Math.Pow(Convert.ToDouble(1 + (tasa / 12)), 12) - 1) * 100) / 100;
        txtVTasaEA.Text = efectiva.ToString("P");
        txtVMinima.Text = !string.IsNullOrEmpty(lstLineas[0].valor_apertura.ToString()) ? lstLineas[0].valor_apertura.ToString() : "";

        string pBeneficiarios = ConfigurationManager.AppSettings["AhorrosParaBeneficiarios"] != null ?
                ConfigurationManager.AppSettings["AhorrosParaBeneficiarios"].ToString() : "0";
        if (pBeneficiarios == "1")
        {
            pnlBenef.Visible = true;
            List<xpinnWSEstadoCuenta.ListaDesplegable> lstParentesco;
            lstParentesco = BOEstadoCuenta.PoblarListaDesplegable("PARENTESCOS", "CODPARENTESCO,DESCRIPCION", "codparentesco in(4,7,9,8)", "2", Session["sec"].ToString());
            //Llenando Parentesco
            if (lstParentesco.Count > 0)
            {
                LlenarDrop(ddlparentesco, lstParentesco);
            }

            //LLenando CheckBoxList Tipo Identificacion
            List<xpinnWSEstadoCuenta.ListaDesplegable> lstTipoIdenti;
            lstTipoIdenti = BOEstadoCuenta.PoblarListaDesplegable("TIPOIDENTIFICACION", "CODTIPOIDENTIFICACION,DESCRIPCION", " CODTIPOIDENTIFICACION IN (5,7,9)", "2", Session["sec"].ToString());
            if (lstTipoIdenti.Count > 0)
            {
                LlenarDrop(ddlTipoIdentificacion, lstTipoIdenti);
            }
        }
        else
        {
            pnlBenef.Visible = false;
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

    /// <summary>
    /// Sin parametro: Limpia label - Con parametro: muestra mensaje
    /// </summary>
    /// <param name="msj"></param>
    public void Error(string msj = "")
    {
        if (!string.IsNullOrWhiteSpace(msj))
        {
            lblError.Text = msj;
            lblError.Visible = true;
        }
        else
        {
            lblError.Text = "";
            lblError.Visible = false;
        }
    }

    protected void ValidarMedioPago(object sender, EventArgs e)
    {
        if (rbConsignacion.Checked)
        {
            pnlConsignacion.Visible = true;
            pnlPse.Visible = false;
        }
        else
        {
            pnlConsignacion.Visible = false;
            pnlPse.Visible = true;
        }

    }
    public bool ValidarCampos()
    {
        Error();
        Datospersona = (xpinnWSLogin.Persona1)Session["persona"];
        string opc = ddlProducto.SelectedValue.ToString();
        switch (opc)
        {
            case "3": //Ahorros
                if (string.IsNullOrWhiteSpace(txtVValorCuota.Text.ToString()))
                { Error("Es necesario ingresar un valor para la cuota"); return false; }

                decimal valor = Convert.ToDecimal(txtVValorCuota.Text.Trim().Replace(".", "").Replace(",", "").Replace("$", ""));
                if (!string.IsNullOrEmpty(txtVAperturaMin.Text))
                {
                    decimal valorMin = Convert.ToDecimal(txtVAperturaMin.Text.Trim().Replace(".", "").Replace(",", "").Replace("$", ""));
                    if (valor < valorMin)
                    {
                        Error("El valor no cumple con la cuota mínima de apertura de: " + valorMin.ToString("C0"));
                        return false;
                    }
                }

                if (ddlComunPeriodicidad.SelectedValue.ToString() == "0")
                { Error("Es necesario seleccionar una periodicidad"); return false; }
                if (ddlComunFormaPago.SelectedValue.ToString() == "0")
                { Error("Es necesario seleccionar una forma de pago"); return false; }
                if (string.IsNullOrWhiteSpace(ddlVLinea.SelectedValue.ToString()))
                { Error("Es necesario seleccionar una línea para el Ahorro"); return false; }
                if (ddlComunFormaPago.SelectedValue.ToString() == "0")
                { Error("Es necesario seleccionar una forma de pago"); return false; }

                string pBeneficiarios = ConfigurationManager.AppSettings["AhorrosParaBeneficiarios"] != null ?
                ConfigurationManager.AppSettings["AhorrosParaBeneficiarios"].ToString() : "0";
                if (pBeneficiarios == "1")
                {
                    if (string.IsNullOrWhiteSpace(txtVNombre.Text.ToString()))
                    { Error("Es necesario el nombre del beneficiario"); return false; }
                    if (string.IsNullOrWhiteSpace(txtVidentificacion.Text.ToString()))
                    { Error("Es necesario la identificación del beneficiario"); return false; }
                    if (ddlTipoIdentificacion.SelectedValue == "0")
                    { Error("Es necesario el tipo de identificación del beneficiario"); return false; }
                    if (ddlparentesco.SelectedValue == "0")
                    { Error("Es necesario el parentesco con el beneficiario"); return false; }
                    if (string.IsNullOrWhiteSpace(txtVfecha.Text.ToString()))
                    { Error("Es necesario ingresar la fecha de nacimiento del beneficiario"); return false; }
                }

                if (!string.IsNullOrEmpty(txtVMinima.Text))
                {
                    if (Convert.ToDecimal(txtVMinima.Text) > Convert.ToDecimal(txtVValorCuota.Text.Replace(".", "").Replace(",", "").Replace("$", "")))
                    { Error("La cuota mínima para la apertura del ahorro es de $" + Convert.ToDecimal(txtVMinima.Text)); return false; }
                }

                break;
            case "5": //CDATS
                if (ddlCLinea.SelectedValue.ToString() == "0")
                { Error("Es necesario seleccionar una línea para el CDAT"); return false; }
                if (string.IsNullOrWhiteSpace(ddlCPlazo.SelectedValue))
                { Error("Es necesario ingresar un plazo para el CDAT"); return false; }
                if (string.IsNullOrWhiteSpace(txtCValor.Text.ToString()))
                { Error("Es necesario ingresar un valor para el CDAT"); return false; }
                //Valida consignación
                xpinnWSAppFinancial.DocumentosAnexos cons = cargarDocumento(fuDocCDAT1);
                if (rbConsignacion.Checked)
                {
                    if (cons != null)
                    {
                        cons.tipo_documento = 1;
                        cons.descripcion = "Consignación CDAT";
                        lstDocs.Add(cons);
                    }
                    else
                    { Error("Debe cargar la consignación realizada"); return false; }
                }

                if (fuDocCDAT2.Visible)
                {
                    xpinnWSAppFinancial.DocumentosAnexos dec = cargarDocumento(fuDocCDAT2);
                    if (dec != null)
                    {
                        dec.tipo_documento = 2;
                        dec.descripcion = "Decalaración de orígen de fondos CDAT";
                        lstDocs.Add(dec);
                    }
                    else
                    { Error("debe cargar la declaración de origen de fondos"); return false; }
                }
                Session["docs"] = lstDocs;

                //Valida el plazo
                try
                {
                    //Obtener datos de la linea CDAT
                    xpinnWSAppFinancial.LineaCDAT pLinea = new xpinnWSAppFinancial.LineaCDAT();
                    pLinea.cod_lineacdat = ddlCLinea.SelectedValue;
                    List<xpinnWSAppFinancial.LineaCDAT> lstLineasCDAT = BOFinancial.ListarLineasCDAT(pLinea, Session["sec"].ToString());
                    if (lstLineasCDAT.Count > 0)
                    {
                        pLinea = lstLineasCDAT.ElementAt(0);
                        pLinea.plazo_minimo = pLinea.plazo_minimo / 30;
                        pLinea.plazo_maximo = pLinea.plazo_maximo / 30;
                        if (Convert.ToInt32(ddlCPlazo.SelectedValue) < pLinea.plazo_minimo || Convert.ToInt32(ddlCPlazo.SelectedValue) > pLinea.plazo_maximo)
                        {
                            Error("El plazo para la linea seleccionada debe estar entre " + pLinea.plazo_minimo + " y " + pLinea.plazo_maximo + " meses"); return false;
                        }
                    }
                }
                catch
                {
                    return true;
                }

                //List<xpinnWSDeposito.Beneficiario> lstBene = new List<xpinnWSDeposito.Beneficiario>();
                //if (Session["DatosBene"] != null)
                //{
                //    lstBene = Session["DatosBene"] as List<xpinnWSDeposito.Beneficiario>;
                //    if(lstBene.Count <= 0)
                //    {
                //        { Error("Registre al menos un beneficiario"); return false; }
                //    }
                //}
                //else
                //{
                //    { Error("Registre al menos un beneficiario"); return false; }
                //}

                break;
            case "9": //Programado
                if (ddlALinea.SelectedValue.ToString() == "0")
                { Error("Es necesario seleccionar una línea para el CDAT"); return false; }
                if (ddlANumerocuotas.SelectedValue == "0")
                { Error("Es necesario ingresar un numero de cuotas"); return false; }
                if (string.IsNullOrWhiteSpace(txtAValorCuota.Text.ToString()))
                { Error("Es necesario ingresar un valor para la cuota"); return false; }
                if (ddlComunPeriodicidad.SelectedValue.ToString() == "0")
                { Error("Es necesario seleccionar una periodicidad"); return false; }
                if (ddlComunFormaPago.SelectedValue.ToString() == "0")
                { Error("Es necesario seleccionar una forma de pago"); return false; }
                if (string.IsNullOrWhiteSpace(ddlADestinacion.SelectedValue.ToString()))
                { Error("Es necesario seleccionar una destinación"); return false; }

                //Valida el plazo
                try
                {
                    //Obtener datos de la linea Ahorro Programado
                    List<xpinnWSAppFinancial.LineasProgramado> lstLineas = BOFinancial.ListarLineasAhoProgramado(" and estado = 1 and l.cod_linea_programado = " + ddlALinea.SelectedValue, Session["sec"].ToString());
                    if (lstLineas.Count > 0)
                    {
                        xpinnWSAppFinancial.LineasProgramado linea = lstLineas.ElementAt(0);
                        if (Convert.ToInt32(ddlANumerocuotas.SelectedValue) < (linea.plazo_minimo) || Convert.ToInt32(ddlANumerocuotas.SelectedValue) > (linea.plazo_maximo))
                        {
                            int minimo = linea.plazo_minimo;
                            int maximo = linea.plazo_maximo;
                            Error("El plazo para la linea seleccionada debe estar entre " + minimo + " y " + maximo + " meses"); return false;
                        }
                    }
                }
                catch
                {
                    return true;
                }
                break;
            default:
                Error("Seleccione un tipo de producto");
                return false;
        }
        return true;
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        LimpiarCampos();
        Response.Redirect("~/Index.aspx");
    }

    private void LimpiarCampos()
    {
        Error();
        pnlAhoProgra.Visible = false;
        pnlCDATS.Visible = false;
        pnlVista.Visible = false;
        pnlPse.Visible = true;
        pnlConsignacion.Visible = true;
        ddlProducto.SelectedValue = "0";
        txtCValor.Text = "";
        ddlCPlazo.SelectedValue = "0";
        ddlANumerocuotas.SelectedValue = "0";
        txtAValorCuota.Text = "";
        txtVValorCuota.Text = "";

    }

    protected void ddlALinea_SelectedIndexChanged(object sender, EventArgs e)
    {
        Error();
        try
        {
            //Obtener datos de la linea Ahorro Programado
            List<xpinnWSAppFinancial.LineasProgramado> lstLineas = BOFinancial.ListarLineasAhoProgramado(" and estado = 1 and l.cod_linea_programado = " + ddlALinea.SelectedValue, Session["sec"].ToString());
            if (lstLineas.Count > 0)
            {
                xpinnWSAppFinancial.LineasProgramado linea = lstLineas.ElementAt(0);
                if (linea.tasa_interes > 0)
                {
                    tasa = Convert.ToDecimal(linea.tasa_interes.ToString()) / 100;
                    txtATasa.Text = tasa.ToString("P");
                    double efectiva = ((Math.Pow(Convert.ToDouble(1 + (tasa / 12)), 12) - 1) * 100) / 100;
                    txtATasaEA.Text = efectiva.ToString("P");
                    int minimo = !string.IsNullOrEmpty(linea.plazo_minimo.ToString()) ? linea.plazo_minimo : 0;
                    int maximo = !string.IsNullOrEmpty(linea.plazo_maximo.ToString()) ? linea.plazo_maximo : 24;
                    ddlANumerocuotas.Items.Clear();
                    ddlANumerocuotas.Items.Add(new ListItem("Seleccione un item", "0"));
                    for (int i = minimo; i <= maximo; i++)
                    {
                        ddlANumerocuotas.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                }
            }
        }
        catch (Exception)
        {
            Error("Se presentó un error obtener la tasa de la línea seleccionada");
        }
    }


    protected void ddlVLinea_SelectedIndexChanged(object sender, EventArgs e)
    {
        Error();
        try
        {
            if (ddlVLinea.SelectedValue != "0")
            {
                //Cargar lineas Ahorro a la vista
                Datospersona = (xpinnWSLogin.Persona1)Session["persona"];
                xpinnWSAppFinancial.LineaAhorro pLinea = new xpinnWSAppFinancial.LineaAhorro();
                List<xpinnWSAppFinancial.LineaAhorro> lstLineas = BOFinancial.ListarLineasAhorros(pLinea, Session["sec"].ToString());
                xpinnWSAppFinancial.LineaAhorro lin = new xpinnWSAppFinancial.LineaAhorro();
                lin = (from xpinnWSAppFinancial.LineaAhorro valores in lstLineas
                       where valores.cod_linea_ahorro == ddlVLinea.SelectedValue
                       select valores).ToList()[0];
                txtVAperturaMin.Text = lin.valor_apertura.ToString();
                tasa = !string.IsNullOrWhiteSpace(lin.tasa.ToString()) ? Convert.ToDecimal(lin.tasa.ToString()) / 100 : 0;
                txtVTasa.Text = !string.IsNullOrWhiteSpace(lin.tasa.ToString()) ? tasa.ToString("P") : "";
                txtVRendimiento.Text = tasa.ToString("C0");
                double efectiva = ((Math.Pow(Convert.ToDouble(1 + (tasa / 12)), 12) - 1) * 100) / 100;
                txtVTasaEA.Text = efectiva.ToString("P");

                txtVMinima.Text = !string.IsNullOrWhiteSpace(lin.valor_apertura.ToString()) ? lin.valor_apertura.ToString() : "0";
            }
        }
        catch (Exception)
        {
            Error("Se presentó un error obtener la tasa de la línea seleccionada");
        }
    }

    protected void ddlCLinea_SelectedIndexChanged(object sender, EventArgs e)
    {
        Error();
        xpinnWSDeposito.LineaCDAT lineaCdat = BODeposito.ConsultarLineaCDAT(ddlCLinea.SelectedValue, Session["sec"].ToString());
        if (lineaCdat != null)
        {
            tasa = Convert.ToDecimal(lineaCdat.tasa.ToString()) / 100;
            double efectiva = ((Math.Pow(Convert.ToDouble(1 + (tasa / 12)), 12) - 1) * 100) / 100;
            txtCTasaEA.Text = efectiva.ToString("P");
            txtCTasa.Text = tasa.ToString("P");
            Session["linea"] = lineaCdat;
            xpinnWSDeposito.RangoCDAT rango = new xpinnWSDeposito.RangoCDAT();
            rango = (from xpinnWSDeposito.RangoCDAT valores in lineaCdat.lstRangos
                     where valores.tipo_tope == 2
                     select valores).ToList()[0];
            int minimo = 0;
            if ((Convert.ToInt32(rango.minimo) % 30) != 0)
            {
                minimo = (Convert.ToInt32(rango.minimo) / 30) + 1;
            }
            else
            {
                minimo = (Convert.ToInt32(rango.minimo) / 30);
            }
            int maximo = Convert.ToInt32(rango.maximo) / 30;
            ddlCPlazo.Items.Clear();
            ListItem vacio = new ListItem("seleccione un item", "0");
            ddlCPlazo.Items.Add(vacio);
            for (int i = minimo; i <= maximo; i++)
            {
                if (i > 0)
                {
                    ListItem item = new ListItem(i.ToString() + " meses", i.ToString());
                    ddlCPlazo.Items.Add(item);
                }
            }
        }
    }

    protected void calcularRentabilidadCDAT()
    {
        Error();
        try
        {
            xpinnWSDeposito.LineaCDAT lineaCdat = (xpinnWSDeposito.LineaCDAT)Session["linea"];
            if (lineaCdat.calculo_tasa != null)
            {
                List<xpinnWSDeposito.Cdat> lstConsulta = new List<xpinnWSDeposito.Cdat>();
                xpinnWSDeposito.Cdat vapertura = new xpinnWSDeposito.Cdat();

                DateTime FechaApe = DateTime.Now;
                vapertura.valor = Convert.ToDecimal(txtCValor.Text.Replace(".", "").Replace(",", "").Replace("$", ""));
                vapertura.cod_moneda = 1;
                vapertura.plazo = Convert.ToInt32(ddlCPlazo.SelectedValue) * 30;
                vapertura.tipo_interes = lineaCdat.calculo_tasa.ToString().Trim();
                vapertura.cod_tipo_tasa = Convert.ToInt32(lineaCdat.cod_tipo_tasa);
                vapertura.tasa_interes = Convert.ToDecimal(lineaCdat.tasa);
                vapertura.tipo_historico = 1;
                vapertura.desviacion = 0;
                vapertura.cod_periodicidad_int = null;
                vapertura.capitalizar_int = 0;
                vapertura.cobra_retencion = 1;
                vapertura.retencion = true.ToString();
                lstConsulta = BODeposito.Listarsimulacion(vapertura, FechaApe, Session["sec"].ToString());
                if (lstConsulta != null)
                {
                    txtCRendimiento.Text = !string.IsNullOrEmpty(lstConsulta[0].valor.ToString()) ? lstConsulta[0].valor.ToString("C0") : "0";
                }
            }
        }
        catch (Exception e)
        {
            Error("No fue posible calcular la rentabilidad del CDAT");
        }
    }

    protected void txtCDAT_TextChanged(object sender, EventArgs e)
    {
        Error();
        if (!string.IsNullOrEmpty(ddlCPlazo.SelectedValue) && !string.IsNullOrEmpty(txtCValor.Text))
        {
            xpinnWSDeposito.LineaCDAT lineaCdat = Session["linea"] as xpinnWSDeposito.LineaCDAT;
            xpinnWSDeposito.RangoCDAT rango = new xpinnWSDeposito.RangoCDAT();
            rango = (from xpinnWSDeposito.RangoCDAT valores in lineaCdat.lstRangos
                     where valores.tipo_tope == 1
                     select valores).ToList()[0];
            if (Convert.ToDecimal(txtCValor.Text.Replace(".", "").Replace(",", "").Replace("$", "")) > Convert.ToDecimal(rango.maximo))
            {
                Error("El valor máximo para esta línea de CDAT es " + rango.maximo + "");
            }
            else
            {
                calcularRentabilidadCDAT();
                try
                {
                    xpinnWSEstadoCuenta.General param = BOEstadoCuenta.ConsultarGeneral(16, "", "", Session["sec"].ToString());
                    if (!string.IsNullOrEmpty(param.valor))
                    {
                        if (Convert.ToDecimal(txtCValor.Text.Replace(".", "").Replace(",", "").Replace("$", "")) > Convert.ToDecimal(param.valor))
                        {
                            pnlCDoc.Visible = true;
                            pnlDescarga.Visible = true;
                        }
                        else
                        {
                            pnlCDoc.Visible = false;
                            pnlDescarga.Visible = false;
                        }
                    }
                }
                catch (Exception)
                {
                    pnlCDoc.Visible = false;
                }
            }
        }

    }

    protected void txtVValorCuota_TextChanged(object sender, EventArgs e)
    {
        Error();
        try
        {
            if (ddlVLinea.SelectedValue != "0" && !string.IsNullOrEmpty(txtVValorCuota.Text))
            {
                decimal rendimiento = 0;
                decimal tasa = Convert.ToDecimal(txtVTasa.Text.Trim().Replace("%", ""));
                tasa = tasa / 100;
                decimal valor = Convert.ToDecimal(txtVValorCuota.Text.Trim().Replace(".", "").Replace(",", "").Replace("$", ""));
                rendimiento = ((valor * tasa) / 360) * 30;
                rendimiento = Math.Ceiling(rendimiento);
                if (!string.IsNullOrEmpty(txtVAperturaMin.Text))
                {
                    decimal valorMin = Convert.ToDecimal(txtVAperturaMin.Text.Trim().Replace(".", "").Replace(",", "").Replace("$", ""));
                    if (valor < valorMin)
                    {
                        Error("El valor no cumple con la cuota mínima de apertura de: " + valorMin.ToString("C0"));
                        return;
                    }
                }
            }
        }
        catch (Exception)
        {
            Error("No fue posible calcular el rendimiento");
        }
    }

    protected void txtAValorCuota_TextChanged(object sender, EventArgs e)
    {
        Error();
        try
        {
            if (ddlANumerocuotas.SelectedValue != "0" && !string.IsNullOrEmpty(txtAValorCuota.Text))
            {
                decimal rendimiento = 0;
                decimal tasa = Convert.ToDecimal(txtATasa.Text.Trim().Replace("%", ""));
                tasa = tasa / 100;
                decimal valor = Convert.ToDecimal(txtAValorCuota.Text.Replace(".", "").Replace(",", "").Replace("$", ""));
                rendimiento = ((valor * tasa) / 360) * 30;
                rendimiento = Math.Ceiling(rendimiento);
            }
        }
        catch (Exception)
        {
            Error("No fue posible calcular el rendimiento");
        }
    }


    /*
    /// <summary>
    /// Abre formulario de adición de beneficiario a CDAT
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddRowBeneficio_Click(object sender, EventArgs e)
    {
        limpiarCamposBenef();
        pnlCBenef.Visible = true;
        pnlAddBenef.Visible = false;
        pnlAlmacenar.Visible = false;
        if (Session["DatosBene"] == null)
            txtPorcentajeDisponible.Text = "100";
    }

    

    /// <summary>
    /// Limpia los campos de beneficiario
    /// </summary>
    private void limpiarCamposBenef()
    {
        ddlCParentezco.SelectedValue = "0";
        txtCNombre.Text = "";
        txtCporcentaje.Text = "";
        txtCidentificacion.Text = "";
    }    

    protected void cargarCBenef()
    {
        List<xpinnWSDeposito.Beneficiario> lstBene = new List<xpinnWSDeposito.Beneficiario>();
        gvBeneficiarios.DataSource = null;
        gvBeneficiarios.DataBind();        
        if (Session["DatosBene"] != null)
            lstBene = Session["DatosBene"] as List<xpinnWSDeposito.Beneficiario>;
        gvBeneficiarios.DataSource = lstBene;
        gvBeneficiarios.DataBind();
    }

    */
    /*
    protected void btnGuardarBenef_Click(object sender, EventArgs e)
    {
        if (validarBenef())
        {
            List<xpinnWSDeposito.Beneficiario> lstBene = new List<xpinnWSDeposito.Beneficiario>();
            if (Session["DatosBene"] != null)
                lstBene = Session["DatosBene"] as List<xpinnWSDeposito.Beneficiario>;
            xpinnWSDeposito.Beneficiario eBenef = new xpinnWSDeposito.Beneficiario();
            eBenef.idbeneficiario = -1;
            eBenef.nombre = txtCNombre.Text;
            eBenef.identificacion_ben = txtCidentificacion.Text;
            eBenef.tipo_identificacion_ben = null;
            eBenef.nombre_ben = txtCNombre.Text;
            eBenef.parentesco = Convert.ToInt32(ddlCParentezco.SelectedValue);
            eBenef.porcentaje_ben = Convert.ToDecimal(txtCporcentaje.Text);
            eBenef.edad = null;
            eBenef.sexo = null;
            if (!string.IsNullOrEmpty(txtaccion.Text))
            {
                lstBene.RemoveAt(Convert.ToInt32(txtaccion.Text));
                txtaccion.Text = "";
            }
            lstBene.Add(eBenef);
            Session["DatosBene"] = lstBene;
            limpiarCamposBenef();
            pnlCBenef.Visible = false;
            pnlAddBenef.Visible = true;
            pnlAlmacenar.Visible = true;
            cargarCBenef();
            decimal disponible = Convert.ToDecimal(txtPorcentajeDisponible.Text);
            disponible = disponible - Convert.ToDecimal(eBenef.porcentaje_ben);
            txtPorcentajeDisponible.Text = disponible.ToString();
        }
        else
        {
            cargarCBenef();
        }
    }
    */
    /*
    private bool validarBenef()
    {
        if (string.IsNullOrEmpty(txtCNombre.Text))
            { Error("ingrese el nombre completo del beneficiario"); return false; }
        if (string.IsNullOrEmpty(txtCidentificacion.Text))
        { Error("ingrese el número de identificación del beneficiario"); return false; }
        if (ddlCParentezco.SelectedValue == "0")
        { Error("ingrese el número de identificación del beneficiario"); return false; }
        if (string.IsNullOrEmpty(txtCporcentaje.Text))
        { Error("ingrese el número de identificación del beneficiario"); return false; }

        decimal disponible = Convert.ToDecimal(txtPorcentajeDisponible.Text);
        if(disponible < Convert.ToDecimal(txtCporcentaje.Text))
        { Error("el porcentaje disponible es "+disponible.ToString()+"%"); return false; }


        List<xpinnWSDeposito.Beneficiario> lstBene = new List<xpinnWSDeposito.Beneficiario>();
        if (Session["DatosBene"] != null)
        {
            lstBene = Session["DatosBene"] as List<xpinnWSDeposito.Beneficiario>;
            if(lstBene.Where(x => x.identificacion_ben == txtCidentificacion.Text).Count() > 0)
            {
                btnCancelarBenef_Click(new object(), new EventArgs());
                return false;
            }            
        }
            


        return true;
    }
    */

    /*
protected void btnCancelarBenef_Click(object sender, EventArgs e)
{
    limpiarCamposBenef();
    txtaccion.Text = "";
    pnlCBenef.Visible = false;
    pnlAlmacenar.Visible = true;
    pnlAddBenef.Visible = true;
}
*/

    /*
protected void gvBeneficiario_SelectedIndexChanged(object sender, EventArgs e)
{
    int id = gvBeneficiarios.SelectedIndex;
    List<xpinnWSDeposito.Beneficiario> lstBene = new List<xpinnWSDeposito.Beneficiario>();
    xpinnWSDeposito.Beneficiario ben = new xpinnWSDeposito.Beneficiario();
    if (Session["DatosBene"] != null)
    {
        lstBene = Session["DatosBene"] as List<xpinnWSDeposito.Beneficiario>;
        ben = lstBene.ElementAt(id);
        if(ben != null)
        {
            txtCNombre.Text = ben.nombre;
            txtCidentificacion.Text = ben.identificacion_ben;
            ddlCParentezco.SelectedValue = ben.parentesco.ToString();
            txtCporcentaje.Text = ben.porcentaje_ben.ToString();
            txtaccion.Text = id.ToString();
            decimal disponible = Convert.ToDecimal(txtPorcentajeDisponible.Text);
            disponible = disponible + Convert.ToDecimal(ben.porcentaje_ben);
            txtPorcentajeDisponible.Text = disponible.ToString();
        }
    }
}

*/

    /*
protected void gvBeneficiarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
{
    int conseID = e.RowIndex;
    List<xpinnWSDeposito.Beneficiario> lstBene = new List<xpinnWSDeposito.Beneficiario>();
    if (Session["DatosBene"] != null)
    {
        lstBene = Session["DatosBene"] as List<xpinnWSDeposito.Beneficiario>;
        //Obtiene el elmento a eliminar
        xpinnWSDeposito.Beneficiario ben = new xpinnWSDeposito.Beneficiario();
        ben = lstBene.ElementAt(conseID);
        //Aumenta el porcentaje disponible
        decimal disponible = Convert.ToDecimal(txtPorcentajeDisponible.Text);
        disponible = disponible + Convert.ToDecimal(ben.porcentaje_ben);
        txtPorcentajeDisponible.Text = disponible.ToString();
        //Elimina el registro
        lstBene.RemoveAt(conseID);
        //Actualiza el registro
        Session["DatosBene"] = lstBene;
        cargarCBenef();
    }

}
*/

    protected xpinnWSAppFinancial.DocumentosAnexos cargarDocumento(FileUpload fileUp)
    {
        Error();
        try
        {
            xpinnWSAppFinancial.DocumentosAnexos doc = new xpinnWSAppFinancial.DocumentosAnexos();
            if (fileUp != null)
            {
                if (!fileUp.HasFile)
                {
                    Error("Por favor cargue el comprobante de la consignación");
                    return null;
                }
                String extension = System.IO.Path.GetExtension(fileUp.PostedFile.FileName).ToLower();
                if (extension != ".pdf" && extension != ".jpg" && extension != ".jpeg" && extension != ".bmp" && extension != ".png")
                {
                    Error("El archivo no tiene el formato correcto ('pdf','jpg','jpeg','png)");
                    return null;
                }

                int tamMax = Convert.ToInt32(ConfigurationManager.AppSettings["TamañoMaximoArchivo"]);
                if (fileUp.FileBytes.Length > tamMax)
                {
                    Error("El tamaño del archivo excede el tamaño limite de ( 2MB )");
                    return null;
                }

                StreamsHelper streamHelper = new StreamsHelper();
                byte[] bytesArrImagen;
                using (System.IO.Stream streamImagen = fileUp.PostedFile.InputStream)
                {
                    bytesArrImagen = streamHelper.LeerTodosLosBytesDeUnStream(streamImagen);
                }

                doc.extension = extension;
                doc.tipo_producto = 5;
                doc.fechaanexo = DateTime.Today;
                doc.imagen = bytesArrImagen;
                doc.estado = 1;

                return doc;
            }
            Error("por favor cargue los documentos requeridos");
            return null;
        }
        catch (Exception)
        {
            Error("se presentó un error al cargar los archivos, por favor intente nuevamente");
            return null;
        }
    }

    #region Metodos PSE
    static Dictionary<TipoDeProducto, string> _PaymentDescription = new Dictionary<TipoDeProducto, string>()
        {
            { TipoDeProducto.Aporte, "APLICACIÓN DE PAGO - APORTES" },
            { TipoDeProducto.Credito, "APLICACIÓN DE PAGO - CRÉDITO" },
            { TipoDeProducto.AhorrosVista, "APLICACIÓN DE PAGO - AHORRO VISTA" },
            { TipoDeProducto.Servicios, "APLICACIÓN DE PAGO - SERVICIOS" },
            { TipoDeProducto.CDATS, "APLICACIÓN DE PAGO - CDAT" },
            { TipoDeProducto.AhorroProgramado, "APLICACIÓN DE PAGO - AHORRO PROGRAMADO" }
        };

    protected void btnPse_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        try
        {
            ViewState[PersonaLogin.cod_persona + "IP"] = GetUserIP();
            btnGuardarSolicitud_Click(null, null);
            //IniciarProcesoPago();
        }
        catch (Exception ex)
        {
            VerError("Ocurrio un error desconocido. Mensaje: " + ex.Message);
        }
    }
    #endregion

    #region metodos nueva versión PSE

    public void IniciarProcesoPago()
    {
        ViewState["sError"] = null;
        string url = "";
        string bancos = "";
        if (ConfigurationManager.AppSettings["URLProyecto"] != null)
        {
            url = ConfigurationManager.AppSettings["URLProyecto"].ToString();
            bancos = url + "/Pages/Asociado/EstadoCuenta/DetallePago.aspx";
        }
        try
        {
            TipoDeProducto pTipoProduct = ddlProducto.Text.ToEnum<TipoDeProducto>();
            pPersona = (xpinnWSLogin.Persona1)Session["persona"];
            xpinnWSIntegracion.ACHPayment pago = new xpinnWSIntegracion.ACHPayment()
            {
                Cod_persona = pPersona.cod_persona,
                Cod_ope = 0,
                Type = 0,
                // Amount = Convert.ToDecimal(txtCValor.Text),
                Amount = Convert.ToDecimal(txtCValor.Text.Replace("$", "").Replace("$", "")),
                VATAmount = 0,
                PaymentDescription = _PaymentDescription[pTipoProduct],
                ReferenceNumber1 = ViewState[PersonaLogin.cod_persona + "IP"].ToString(),
                ReferenceNumber2 = PersonaLogin.nomtipo_identificacion,
                ReferenceNumber3 = PersonaLogin.identificacion,
                Email = pPersona.email,
                TypeProduct = (int)pTipoProduct,
                NumberProduct = 5.ToString(),//lblNroProducto.Text,
                Fields = PersonaLogin.identificacion.ToString() + "," + PersonaLogin.cod_persona + "," + PersonaLogin.nombre,
                Entity_url = bancos
            };

            pago = wsIntegra.CreatePaymentTransaction(pago, Session["sec"].ToString());
            if (pago != null)
            {
                if (string.IsNullOrEmpty(pago.ErrorMessage))
                {
                    Session.Remove(PersonaLogin.cod_persona + "PaymentTransac");
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    // Abrir nueva ventana de bancos
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    panelFinal.Visible = true;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "var bancos = window.open('" + url + "/Pages/Asociado/EstadoCuenta/AplicarPagos.aspx?pagos=1','bancos');", true);
                    // --- AMBIENTE DE PRUEBAS    
                   // Response.Redirect("https://200.1.124.62/PSEHostingUI/GetBankListWS.aspx?enc=" + pago.Identifier);
                    // --- AMBIENTE DE PRODUCCION
                    Response.Redirect("https://www.psepagos.co/PSEHostingUI/GetBankListWS.aspx?enc=" + pago.Identifier);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "setTimeout('resultado(banco)', 1000);", true);
                }
                else
                {
                    VerError("Se presentó un problema ACH: " + pago.ErrorMessage);
                }
            }
            else
            {
                VerError("Se presentó un problema, intente recargar la página");
            }

        }
        catch (Exception ex)
        {
            ViewState["sError"] = "Ocurrio un error desconocido. Mensaje: " + ex.Message;
        }
    }
    #endregion
}