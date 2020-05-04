using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Xpinn.Util;
using System.Linq;

public partial class CreditoAntiguo : GlobalWeb
{
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient EstadoServicio = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    xpinnWSCredito.WSCreditoSoapClient BOCredito = new xpinnWSCredito.WSCreditoSoapClient();
    xpinnWSAppFinancial.WSAppFinancialSoapClient AppServices = new xpinnWSAppFinancial.WSAppFinancialSoapClient();

    xpinnWSLogin.Persona1 _persona;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ValidarSession();
            txtArriendoViv.eventoCambiar += txtArriendoViv_TextChanged;
            txtGastosSos.eventoCambiar += txtGastosSos_TextChanged;
            txtOtrosGastos.eventoCambiar += txtOtrosGastos_TextChanged;

            txtSaldofecha.eventoCambiar += txtSaldofecha_TextChanged;
            txtSaldofecha2.eventoCambiar += txtSaldofecha2_TextChanged;
            txtValorcuota.eventoCambiar += txtValorcuota_TextChanged;
            txtValorcuota2.eventoCambiar += txtValorcuota2_TextChanged;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        _persona = (xpinnWSLogin.Persona1)Session["persona"];        
        if (!Page.IsPostBack)
        {
            CargarDropDownYCheckBox();
            //verificando si esta logeado o persona reciente.
            if (_persona.cod_persona != 0)
            {
                lblcod_persona.Text = _persona.cod_persona.ToString();
                btnInicio.Visible = true;
            }
            else
            {
                if (Request.QueryString["id"] != null)
                {
                    lblid.Text = Request.QueryString["id"].ToString().Trim();
                }
            }

            string pCodPeriodicidad = ConfigurationManager.AppSettings["Periodicidad"] != null ? 
                ConfigurationManager.AppSettings["Periodicidad"].ToString() : string.Empty;
            if (!string.IsNullOrEmpty(pCodPeriodicidad))
                ddlPeriodicidad.SelectedValue = pCodPeriodicidad;

            panelData.Visible = true;
            panelDocumentos.Visible = false;
            panelFinal.Visible = false;
            
            DateTime pFechaActual = DateTime.Now;
            txtDiaEncabezado.Text = pFechaActual.Day.ToString();
            ddlMesEncabezado.SelectedValue = pFechaActual.Month.ToString();
            txtAnioEncabezado.Text = pFechaActual.Year.ToString();
            ddlLinea_SelectedIndexChanged(ddlLinea, null);
            cargarDatosUsuario(lblcod_persona.Text);
            if (Session["simulacion"] != null)
                cargarSimulacion();
        }
    }

    private void cargarSimulacion()
    {
        xpinnWSCredito.Simulacion simulacion = new xpinnWSCredito.Simulacion();
        simulacion = Session["simulacion"] as xpinnWSCredito.Simulacion;
        if(simulacion != null)
        {
            //Asigna valores a los elementos correspondientes
            if(simulacion.lstCuotasExtras != null)
            {
                ViewState[PersonaLogin.cod_persona + "CuoExt"] = simulacion.lstCuotasExtras;
                gvCuoExt.DataSource = simulacion.lstCuotasExtras;
                gvCuoExt.DataBind();
            }
            if (simulacion.totalCuotasExtra != 0)
                ViewState[PersonaLogin.cod_persona + "TotalCuoExt"] = simulacion.totalCuotasExtra;

            //Obtiene valores
            txtVrCredito.Text = simulacion.monto.ToString();
            ddlLinea.SelectedValue = simulacion.cod_credi;
            txtPlazo.Text = simulacion.plazo.ToString();
            ddlPeriodicidad.SelectedValue = simulacion.periodic.ToString();
            txtCuotaAproximada.Text = simulacion.cuota.ToString();
            decimal TasaefectivaMensual = (decimal)simulacion.tasa / 12;
            txtTasa.Text = TasaefectivaMensual.ToString();
            if (!string.IsNullOrWhiteSpace(txtCuotaAproximada.Text))
                txtCuotaAproximada.Visible = true;

            if (ConfigurationManager.AppSettings["cuotasExtra"] != null)
            {
                string cuotaExtra = ConfigurationManager.AppSettings["cuotasExtra"].ToString();
                if (cuotaExtra != "0")
                {
                    pnlCuotaExtra.Visible = true;
                }
            }            
        }
        Session["simulacion"] = null;
    }

    /// <summary>
    /// Carga los datos básicos de una persona
    /// </summary>
    /// <param name="text"></param>
    private void cargarDatosUsuario(string cod_persona)
    {
        //Cargar datos básicos
        xpinnWSEstadoCuenta.Persona1 datos = new xpinnWSEstadoCuenta.Persona1();
        datos.cod_persona = Convert.ToInt64(cod_persona);
        datos = EstadoServicio.ConsultarPersona(datos);
        if(datos != null)
        {
            txtDirecPropiedad.Text = datos.direccion.ToString();
            ddlCiudadPropiedad.SelectedValue = datos.codciudadresidencia.ToString();
        }

        //Cargar cuenta si la tiene
        xpinnWSEstadoCuenta.AhorroVista cuenta = EstadoServicio.ConsultarCuentaBancaria(cod_persona, Session["sec"].ToString());
        if (cuenta != null && !string.IsNullOrEmpty(cuenta.numero_cuenta))
        {
            txtNumCuenta.Text = cuenta.numero_cuenta;
            ddlTipoCuenta.SelectedValue = Convert.ToString(cuenta.tipo_cuenta);
            ddlEntidad.SelectedValue = Convert.ToString(cuenta.cod_banco);
            txtNumCuenta.Enabled = false;
            ddlTipoCuenta.Enabled = false;
            ddlEntidad.Enabled = false;
        }
    }

    protected void CargarDropDownYCheckBox()
    {
        LlenarMesesDrop(ddlMesEncabezado);
        LlenarMesesDrop(ddlMesVencimiento);
        LlenarMesesDrop(ddlVencimientoMes2);
        
        string pFiltro = "and l.estado = 1 AND tasa > 0 AND l.COD_LINEA_CREDITO not in (SELECT VALOR FROM parametros_linea WHERE COD_PARAMETRO = 600)";
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

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstCiudades = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstCiudades = EstadoServicio.PoblarListaDesplegable("CIUDADES", "", " tipo = 3 ", "2", Session["sec"].ToString());
        if (lstCiudades.Count > 0)
        {
            LlenarDrop(ddlCiudadPropiedad, lstCiudades);
        }

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstEntidad = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstEntidad = EstadoServicio.PoblarListaDesplegable("BANCOS", "", "", "2", Session["sec"].ToString());
        if (lstEntidad.Count > 0)
        {
            LlenarDrop(ddlEntidad, lstEntidad);
        }

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstEmpresaRec = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstEmpresaRec = EstadoServicio.PoblarListaDesplegable("EMPRESA_RECAUDO", "", "estado = 1", "2", Session["sec"].ToString());
        if (lstEmpresaRec.Count > 0)
        {
            LlenarDrop(ddlEntidadCredito, lstEmpresaRec);
            LlenarDrop(ddlEntidadCredito2, lstEmpresaRec);
        }

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstPeriodicidad = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstPeriodicidad = EstadoServicio.PoblarListaDesplegable("PERIODICIDAD", "", "", "2", Session["sec"].ToString());
        if (lstPeriodicidad.Count > 0)
        {
            LlenarDrop(ddlPeriodicidad, lstPeriodicidad);
            LlenarDrop(ddlPeriodicidadCuotaExt, lstPeriodicidad);
        }
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
            }
            if (ConfigurationManager.AppSettings["Periodicidad"] != null)
            {
                string valPeriodicidad = ConfigurationManager.AppSettings["Periodicidad"].ToString();
                ddlPeriodicidad.SelectedValue = valPeriodicidad;
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

    void LlenarMesesDrop(DropDownList ddlDropCarga)
    {
        ddlDropCarga.Items.Insert(0, new ListItem("Enero", "1"));
        ddlDropCarga.Items.Insert(1, new ListItem("Febrero", "2"));
        ddlDropCarga.Items.Insert(2, new ListItem("Marzo", "3"));
        ddlDropCarga.Items.Insert(3, new ListItem("Abril", "4"));
        ddlDropCarga.Items.Insert(4, new ListItem("Mayo", "5"));
        ddlDropCarga.Items.Insert(5, new ListItem("Junio", "6"));
        ddlDropCarga.Items.Insert(6, new ListItem("Julio", "7"));
        ddlDropCarga.Items.Insert(7, new ListItem("Agosto", "8"));
        ddlDropCarga.Items.Insert(8, new ListItem("Septiembre", "9"));
        ddlDropCarga.Items.Insert(9, new ListItem("Octubre", "10"));
        ddlDropCarga.Items.Insert(10, new ListItem("Noviembre", "11"));
        ddlDropCarga.Items.Insert(11, new ListItem("Diciembre", "12"));
    }

    protected Boolean validarDatos()
    {
        if (txtVrCredito.Text == "0")
        {
            lblError.Text = "Ingrese el Valor a Solicitar ( Informaci&oacute;n Financiera )";
            txtVrCredito.Focus();
            return false;
        }
        if (ddlLinea.SelectedItem == null)
        {
            lblError.Text = "No existen líneas creadas, comuníquese con nosotros para reportar este inconveniente ( Informaci&oacute;n Financiera )";
            ddlLinea.Focus();
            return false;
        }
        if (ddlLinea.SelectedIndex == 0)
        {
            lblError.Text = "Seleccione la linea de cr&eacute;dito ( Informaci&oacute;n Financiera )";
            ddlLinea.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtPlazo.Text))
        {
            lblError.Text = "Ingrese el plazo correspondiente  ( Informaci&oacute;n Financiera )";
            txtPlazo.Focus();
            return false;
        }
        if (ddlPeriodicidad.SelectedIndex == 0)
        {
            lblError.Text = "Seleccione la Amortización para el crédito a solicitar ( Informaci&oacute;n Financiera )";
            ddlPeriodicidad.Focus();
            return false;
        }
        if (ddlTipoVivienda.SelectedItem == null)
        {
            lblError.Text = "Seleccione su tipo de vivienda ( Informaci&oacute;n Financiera )";
            ddlTipoVivienda.Focus();
            return false;
        }
        if ((txtNombreArrendatario.Enabled == true && txtTelArrendatario.Enabled == true) || ddlTipoVivienda.SelectedValue == "A")
        {
            if (txtNombreArrendatario.Text.Trim() == "")
            {
                lblError.Text = "Ingrese el nombre del Arrendatario ( Informaci&oacute;n Financiera )";
                txtNombreArrendatario.Focus();
                return false;
            }
        }
        if (ddlPropiedad.SelectedItem == null)
        {
            lblError.Text = "Seleccione el tipo de bien que tiene ( Relación de bienes )";
            ddlPropiedad.Focus();
            return false;
        }
        if (txtDirecPropiedad.Text.Trim() == "")
        {
            lblError.Text = "Ingrese la dirección del bien que selecciono anteriormente ( Relación de bienes )";
            txtDirecPropiedad.Focus();
            return false;
        }
        if (ddlCiudadPropiedad.SelectedIndex == 0)
        {
            lblError.Text = "Seleccione la ciudad al cual pertenece la propiedad ( Relación de bienes )";
            ddlCiudadPropiedad.Focus();
            return false;
        }
        if (ddlHipoteca.SelectedValue == "1")
        {
            if (txtVrHipoteca.Text == "0")
            {
                lblError.Text = "Ingrese el valor de Hipoteca ( Relación de bienes )";
                txtVrHipoteca.Focus();
                return false;
            }
        }
        if (rbllPignorado.SelectedValue == "1")
        {
            if (txtValorPignorado.Text == "0")
            {
                lblError.Text = "Ingrese el valor Pignorado ( Relación de bienes )";
                txtValorPignorado.Focus();
                return false;
            }
        }
        if (ddlEntidadCredito.SelectedIndex > 0)
        {
            if (txtAnioVenc.Text == "")
            {
                lblError.Text = "Ingrese el año del crédito que indico ( Crédito con otras entidades )";
                txtAnioVenc.Focus();
                return false;
            }
        }
        if (ddlEntidadCredito2.SelectedIndex > 0)
        {
            if (txtAnioVenc2.Text == "")
            {
                lblError.Text = "Ingrese el año del crédito que indico ( Crédito con otras entidades )";
                txtAnioVenc2.Focus();
                return false;
            }
        }

        return true;
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        if (string.IsNullOrEmpty(lblcod_persona.Text) && !string.IsNullOrEmpty(lblid.Text))
        {
            Navegar("~/Default.aspx");
        }
        else
            Navegar("~/Index.aspx");
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        try
        {
            if (validarDatos())
                ctlMensaje.MostrarMensaje("Desea generar la solicitud de Crédito?");
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            xpinnWSCredito.SolicitudCreditoAAC pEntidad = new xpinnWSCredito.SolicitudCreditoAAC();

            //DATA FINANCIERA
            pEntidad.numerosolicitud = 0;
            pEntidad.fechasolicitud = DateTime.Now;
            pEntidad.cod_persona = null;
            pEntidad.montosolicitado = Convert.ToDecimal(txtVrCredito.Text.Replace(".", ""));
            pEntidad.plazosolicitado = Convert.ToInt32(txtPlazo.Text);
            pEntidad.cuotasolicitada = 0;
            pEntidad.tipocredito = Convert.ToInt32(ddlLinea.SelectedValue);
            pEntidad.periodicidad = Convert.ToInt32(ddlPeriodicidad.SelectedValue);

            string cod_medio = ConfigurationManager.AppSettings["MedioDefault"] != null ? ConfigurationManager.AppSettings["MedioDefault"].ToString().Trim() : null;
            if (cod_medio != null)
                pEntidad.medio = Convert.ToInt32(cod_medio);
            pEntidad.reqpoliza = "0";
            pEntidad.otromedio = null;
            pEntidad.usuario = "WEB";
            pEntidad.oficina = null;
            pEntidad.concepto = "SOLICITUD ATENCION AL CLIENTE";
            pEntidad.garantia = 0;
            pEntidad.garantia_comunitaria = 0;
            pEntidad.tipo_liquidacion = null;
            pEntidad.forma_pago = null;
            pEntidad.idproveedor = null;
            pEntidad.nomproveedor = null;
            pEntidad.destino = null;

            pEntidad.cod_persona = 0;
            pEntidad.id_persona = 0;
            if (lblcod_persona.Text.Trim() != "")
                pEntidad.cod_persona = Convert.ToInt64(lblcod_persona.Text.Trim());
            if (lblid.Text.Trim() != "")
                pEntidad.id_persona = Convert.ToInt64(lblid.Text.Trim());            
            pEntidad.tipo_cuenta = Convert.ToInt32(ddlTipoCuenta.SelectedValue);
            
            pEntidad.num_cuenta = txtNumCuenta.Text.Trim() != "" ? txtNumCuenta.Text.Trim() : null;
            pEntidad.cod_banco = ddlEntidad.SelectedIndex > 0 ? Convert.ToInt32(ddlEntidad.SelectedValue) : 0;
            pEntidad.tipovivienda = ddlTipoVivienda.SelectedValue;
            if (ddlTipoVivienda.SelectedValue == "A")
            {
                pEntidad.arrendatario = txtNombreArrendatario.Text.Trim().ToUpper();
                pEntidad.telef_arrendatario = txtTelArrendatario.Text.Trim() != "" ? txtTelArrendatario.Text.Trim() : null;
            }
            else
            {
                pEntidad.arrendatario = null;
                pEntidad.telef_arrendatario = null;
            }

            //DATOS PROPIEDAD
            pEntidad.tipo_propiedad = ddlPropiedad.SelectedValue;
            pEntidad.otro_propiedad = ddlPropiedad.SelectedValue == "O" ? txtOtroPropiedad.Text.Trim().ToUpper() : null;
            pEntidad.direc_propiedad = txtDirecPropiedad.Text.Trim();
            pEntidad.codciudad_propiedad = Convert.ToInt64(ddlCiudadPropiedad.SelectedValue.Trim());
            pEntidad.escritura_propiedad = txtEscrituraNro.Text.Trim() != "" ? txtEscrituraNro.Text.Trim() : null;
            pEntidad.notaria = txtNotaria.Text.Trim() != "" ? txtNotaria.Text.Trim() : null;
            pEntidad.maneja_hipoteca = Convert.ToInt32(ddlHipoteca.SelectedValue);
            pEntidad.valor_hipoteca = Convert.ToDecimal(txtVrHipoteca.Text);
            pEntidad.matricula_inmov = txtMatriculaInmov.Text.Trim() != "" ? txtMatriculaInmov.Text.Trim() : null;
            pEntidad.vr_propiedad_viv = Convert.ToDecimal(txtVrComercial.Text);

            //DATA GASTOS GENERALES
            pEntidad.vr_arriendo_cuota = Convert.ToDecimal(txtArriendoViv.Text);
            pEntidad.vr_gastos = Convert.ToDecimal(txtGastosSos.Text);
            pEntidad.vr_otrosgastos = Convert.ToDecimal(txtOtrosGastos.Text);

            //DATA VEHICULO
            pEntidad.marca_modelo = txtMarcaModelo.Text.Trim() != "" ? txtMarcaModelo.Text.Trim() : null;
            pEntidad.vr_comercial_vehi = Convert.ToDecimal(txtVrComercVehi.Text);
            pEntidad.pignorado_vehi = Convert.ToInt32(rbllPignorado.SelectedValue);
            pEntidad.vr_pignorado_vehi = Convert.ToDecimal(txtValorPignorado.Text);

            //CREDITOS CON OTRAS ENTIDADES
            pEntidad.empresa_recaudo = ddlEntidadCredito.SelectedIndex > 0 ? Convert.ToInt32(ddlEntidadCredito.SelectedValue) : 0;
            DateTime pFecha;
            if (txtAnioVenc.Text.Trim() != "")
            {
                pFecha = DateTime.ParseExact("01/" + Convert.ToInt32(ddlMesVencimiento.SelectedValue).ToString("00") + "/"+ Convert.ToInt32(txtAnioVenc.Text).ToString("0000"), gFormatoFecha, null);
                if (ddlMesVencimiento.SelectedValue != "12")
                {
                    pFecha = pFecha.AddMonths(1);
                    pFecha = pFecha.AddDays(-1);
                }
                else
                {
                    pFecha = pFecha.AddMonths(1);
                    pFecha = pFecha.AddYears(1);
                    pFecha = pFecha.AddDays(-1);
                }
            }
            else
            {
                pFecha = DateTime.Now;
            }
            pEntidad.fecha_vencimiento = pFecha;
            pEntidad.saldo = txtSaldofecha.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtSaldofecha.Text);
            pEntidad.vr_cuota = txtValorcuota.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtValorcuota.Text);
            //:::::::::::::::::
            pEntidad.empresa_recaudo_seg = ddlEntidadCredito2.SelectedIndex > 0 ? Convert.ToInt32(ddlEntidadCredito2.SelectedValue) : 0;
            pFecha = DateTime.MinValue;
            if (txtAnioVenc2.Text.Trim() != "")
            {
                pFecha = DateTime.ParseExact("01/" + Convert.ToInt32(ddlVencimientoMes2.SelectedValue).ToString("00") + "/" + Convert.ToInt32(txtAnioVenc2.Text).ToString("0000"), gFormatoFecha, null);
                if (ddlVencimientoMes2.SelectedValue != "12")
                {
                    pFecha = pFecha.AddMonths(1);
                    pFecha = pFecha.AddDays(-1);
                }
                else
                {
                    pFecha = pFecha.AddMonths(1);
                    pFecha = pFecha.AddYears(1);
                    pFecha = pFecha.AddDays(-1);
                }
            }
            pEntidad.fecha_vencimiento_seg = pFecha;
            pEntidad.saldo_seg = txtSaldofecha2.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtSaldofecha2.Text);
            pEntidad.vr_cuota_seg = txtValorcuota2.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtValorcuota2.Text);
            pEntidad.estado = 0;

            Boolean rpta = ListarDocumentosRequeridos(ddlLinea.SelectedValue);
            if (rpta)
                ViewState[PersonaLogin.cod_persona + "Soli_Credito"] = pEntidad;
            else
            {
                List<xpinnWSCredito.CuotasExtras> lstCuotas = ObtenerListaCuotasExtras();
                pEntidad = BOCredito.CrearSolicitudCreditoAAC(pEntidad, _persona.identificacion, _persona.clavesinecriptar, Session["sec"].ToString(), null, lstCuotas);
                if (pEntidad.numerosolicitud != 0)
                {
                    lblCodigoGenerado.Text = pEntidad.numerosolicitud.ToString();
                    EnviarCorreoAlAsociado(pEntidad);
                    panelFinal.Visible = true;
                    panelData.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    
    private Boolean CargarDocumentosCredito(ref List<xpinnWSCredito.DocumentosAnexos> lstDocumentos)
    {
        foreach (GridViewRow rFila in gvDocumentosReq.Rows)
        {
            xpinnWSCredito.DocumentosAnexos pDocum = new xpinnWSCredito.DocumentosAnexos();
            string pTipo_documento = "";
            FileUpload fuArchivo = (FileUpload)rFila.FindControl("fuArchivo");
            if (fuArchivo != null)
            {
                if (!fuArchivo.HasFile)
                {
                    lblError.Text = "Ingrese el documento requerido en la Fila " + (rFila.RowIndex + 1) + ", Verifique si cargo todos los archivos antes de registrarlos";
                    return false;
                }
                String extension = System.IO.Path.GetExtension(fuArchivo.PostedFile.FileName).ToLower();
                if (extension != ".pdf" && extension != ".jpg" && extension != ".jpeg"  && extension != ".bmp" && extension != ".png")
                {
                    lblError.Text = "El archivo en la Fila " + (rFila.RowIndex + 1) + " no tiene el formato correcto";
                    return false;
                }

                int tamMax = Convert.ToInt32(ConfigurationManager.AppSettings["TamañoMaximoArchivo"]);
                if (fuArchivo.FileBytes.Length > tamMax)
                {
                    lblError.Text = "El tamaño del archivo en la fila " + (rFila.RowIndex + 1) + " excede el tamaño limite de ( 2MB )";
                    return false;
                }

                StreamsHelper streamHelper = new StreamsHelper();
                byte[] bytesArrImagen;
                using (System.IO.Stream streamImagen = fuArchivo.PostedFile.InputStream)
                {
                    bytesArrImagen = streamHelper.LeerTodosLosBytesDeUnStream(streamImagen);
                }
                Int64 pTipoDocumento = Convert.ToInt32(gvDocumentosReq.DataKeys[rFila.RowIndex].Values[0].ToString());
                pDocum.tipo_documento = pTipoDocumento;
                pDocum.fechaanexo = DateTime.Today;
                pDocum.imagen = bytesArrImagen;
                pDocum.descripcion = "Carga mediante Atencion al Cliente";
                pDocum.estado = 1;
                lstDocumentos.Add(pDocum);
            }
        }
        return true;
    }

    protected void btnGuardarDoc_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        try
        {
            List<xpinnWSCredito.DocumentosAnexos> lstDocumentos = new List<xpinnWSCredito.DocumentosAnexos>();

            if (!CargarDocumentosCredito(ref lstDocumentos))
                return;
            xpinnWSCredito.SolicitudCreditoAAC pEntidad = new xpinnWSCredito.SolicitudCreditoAAC();
            if (ViewState[PersonaLogin.cod_persona + "Soli_Credito"] == null)
            {
                lblError.Text = "Ocurrió un problema al generar la solicitud, vuelva a realizar el proceso por favor.";
                return;
            }
            pEntidad = (xpinnWSCredito.SolicitudCreditoAAC)ViewState[PersonaLogin.cod_persona + "Soli_Credito"];
            List<xpinnWSCredito.CuotasExtras> lstCuotas = ObtenerListaCuotasExtras();
            pEntidad = BOCredito.CrearSolicitudCreditoAAC(pEntidad, _persona.identificacion, _persona.clavesinecriptar, Session["sec"].ToString(), lstDocumentos, lstCuotas);
            if (pEntidad.numerosolicitud != 0)
            {
                lblCodigoGenerado.Text = pEntidad.numerosolicitud.ToString();
                EnviarCorreoAlAsociado(pEntidad);
                panelFinal.Visible = true;
                panelDocumentos.Visible = false;
            }
            else
            {
                lblError.Text = "Se genero un error al guardar el crédito Icetex.";
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }


    void EnviarCorreoAlAsociado(xpinnWSCredito.SolicitudCreditoAAC pEntidad)
    {
        try
        {            
            lblError.Text = "";
            
            xpinnWSCredito.TiposDocCobranzas documentoFormatoCorreo = BOCredito.ConsultarInformacionCorreoSolicitudCred(Session["sec"].ToString());

            if (string.IsNullOrWhiteSpace(documentoFormatoCorreo.empresa.e_mail) || string.IsNullOrWhiteSpace(documentoFormatoCorreo.empresa.clave_e_mail))
            {
                VerError("La empresa no tiene configurado un email para enviar el correo");
                return;
            }
            else if (string.IsNullOrWhiteSpace(documentoFormatoCorreo.texto))
            {
                VerError("No esta parametrizado el formato del correo a enviar");
                return;
            }
            else if (string.IsNullOrWhiteSpace(_persona.email))
            {
                return;
            }

            LlenarDiccionarioGlobalWebParaCorreo(pEntidad);

            documentoFormatoCorreo.texto = ReemplazarParametrosEnElMensajeCorreo(documentoFormatoCorreo.texto);

            CorreoHelper correoHelper = new CorreoHelper(_persona.email, documentoFormatoCorreo.empresa.e_mail, documentoFormatoCorreo.empresa.clave_e_mail);
            string pSubject = "Solicitud de Crédito";            
            _persona = (xpinnWSLogin.Persona1)Session["persona"];
            xpinnWSCredito.Persona Persona = BOCredito.ConsultarPersona(_persona.cod_persona, _persona.clavesinecriptar, Session["sec"].ToString());
            string asesor_email = "";
            if (Persona.Asesor.Email != null && Persona.Asesor.Email != "0")
                asesor_email = Persona.Asesor.Email;

            bool exitoso = correoHelper.sendEmail(documentoFormatoCorreo.texto, pSubject, asesor_email);
            // hacer algo si el correo a un asociado falla, no se si informar o ignorar y pasar
            if (!exitoso)
            {
                CorreoHelper correoEmergencia = new CorreoHelper(asesor_email, documentoFormatoCorreo.empresa.e_mail, documentoFormatoCorreo.empresa.clave_e_mail);
                bool exitoso2 = correoEmergencia.sendEmail(documentoFormatoCorreo.texto, pSubject);
            }            
        }
        catch (Exception ex)
        {
            lblError.Text = "Error al enviar el correo, " + ex.Message;
        }
    }


    void LlenarDiccionarioGlobalWebParaCorreo(xpinnWSCredito.SolicitudCreditoAAC entidad)
    {
        parametrosFormatoCorreo = new Dictionary<ParametroCorreo, string>();

        parametrosFormatoCorreo.Add(ParametroCorreo.NombreCompletoPersona, _persona.nombre);
        parametrosFormatoCorreo.Add(ParametroCorreo.Identificacion, _persona.identificacion);
        parametrosFormatoCorreo.Add(ParametroCorreo.FechaCredito, DateTime.Today.ToShortDateString());
        parametrosFormatoCorreo.Add(ParametroCorreo.MontoCredito, entidad.montosolicitado.ToString());
        parametrosFormatoCorreo.Add(ParametroCorreo.PlazoCredito, entidad.plazosolicitado.ToString());
        parametrosFormatoCorreo.Add(ParametroCorreo.LineaCredito, ddlLinea.SelectedItem.Text);
        parametrosFormatoCorreo.Add(ParametroCorreo.NumeroSolicitud, entidad.numerosolicitud.ToString());
    }


    protected void cblTipoVivienda_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtNombreArrendatario.Enabled = false;
        txtTelArrendatario.Enabled = false;
        if (ddlTipoVivienda.SelectedItem != null)
        {
            if (ddlTipoVivienda.SelectedValue == "A")
            {
                txtNombreArrendatario.Enabled = true;
                txtTelArrendatario.Enabled = true;
            }
        }
    }

    protected void cblPropiedad_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtOtroPropiedad.Enabled = false;
        if (ddlPropiedad.SelectedItem != null)
        {
            if (ddlPropiedad.SelectedValue == "O")
                txtOtroPropiedad.Enabled = true;
        }
    }

    protected void TotalizarGastos()
    {
        decimal pVrTotal = 0, pVrArri = 0, pVrGastos = 0, pVrOtros = 0;

        pVrArri = Convert.ToDecimal(txtArriendoViv.Text.Replace(".", ""));
        pVrGastos = Convert.ToDecimal(txtGastosSos.Text.Replace(".", ""));
        pVrOtros = Convert.ToDecimal(txtOtrosGastos.Text.Replace(".", ""));
        pVrTotal = pVrArri + pVrGastos + pVrOtros;
        txtTotalGastos.Text = pVrTotal.ToString("n0");
    }

    protected void txtArriendoViv_TextChanged(object sender, EventArgs e)
    {
        TotalizarGastos();
    }

    protected void txtGastosSos_TextChanged(object sender, EventArgs e)
    {
        TotalizarGastos();
    }

    protected void txtOtrosGastos_TextChanged(object sender, EventArgs e)
    {
        TotalizarGastos();
    }

    protected void rblHipoteca_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtVrHipoteca.Enabled = ddlHipoteca.SelectedIndex == 0 ? true : false;
    }
    protected void rbllPignorado_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtValorPignorado.Enabled = false;
        if (rbllPignorado.SelectedValue == "1")
            txtValorPignorado.Enabled = true;
    }

    protected void TotalizarMontoCreditos(int pOpcion)
    {
        decimal pVrTotal = 0, pVr1 = 0, pVr2 = 0;
        if (pOpcion == 1)
        {
            pVr1 = Convert.ToDecimal(txtSaldofecha.Text.Replace(".", ""));
            pVr2 = Convert.ToDecimal(txtSaldofecha2.Text.Replace(".", ""));
            pVrTotal = pVr1 + pVr2;
            txtSaldoConsoli.Text = pVrTotal.ToString("n0");
        }
        else
        {
            pVr1 = Convert.ToDecimal(txtValorcuota.Text.Replace(".", ""));
            pVr2 = Convert.ToDecimal(txtValorcuota2.Text.Replace(".", ""));
            pVrTotal = pVr1 + pVr2;
            txtCuotaXmes.Text = pVrTotal.ToString("n0");
        }
    }

    protected void txtSaldofecha_TextChanged(object sender, EventArgs e)
    {
        TotalizarMontoCreditos(1);
    }
    protected void txtSaldofecha2_TextChanged(object sender, EventArgs e)
    {
        TotalizarMontoCreditos(1);
    }
    protected void txtValorcuota_TextChanged(object sender, EventArgs e)
    {
        TotalizarMontoCreditos(2);
    }
    protected void txtValorcuota2_TextChanged(object sender, EventArgs e)
    {
        TotalizarMontoCreditos(2);
    }

    private bool ListarDocumentosRequeridos(string cod_linea_credito)
    {
        if (cod_linea_credito == null)
            return false;
        List<xpinnWSCredito.LineasCredito> lstDocumentos = new List<xpinnWSCredito.LineasCredito>();
        lstDocumentos = BOCredito.ListarDocumentos(cod_linea_credito, _persona.identificacion, _persona.clavesinecriptar, Session["sec"].ToString());
        if (lstDocumentos.Count > 0)
        {
            gvDocumentosReq.DataSource = lstDocumentos;
            gvDocumentosReq.DataBind();
            //panelData.Visible = false;
            panelDocumentos.Visible = true;
            panelFinal.Visible = false;
            return true;
        }
        else
        {
            panelDocumentos.Visible = false;
            panelFinal.Visible = true;
            panelData.Visible = false;
            return false;
        }
    }


    protected void btnInicio_Click(object sender, EventArgs e)
    {
        Navegar("~/Index.aspx");
    }

    protected void ddlLinea_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlCuotaExtra.Visible = false;
        if (ddlLinea.SelectedIndex != 0)
        {
            ListarDocumentosRequeridos(ddlLinea.SelectedValue);
            if (ViewState["DTLineasCredito"] != null)
            {
                List<xpinnWSAppFinancial.LineasCredito> lstLineas = (List<xpinnWSAppFinancial.LineasCredito>)ViewState["DTLineasCredito"];
                if (lstLineas.Count > 0)
                {
                    xpinnWSAppFinancial.LineasCredito pLineaSeleccionada = lstLineas.Where(x => x.cod_linea_credito == ddlLinea.SelectedValue).FirstOrDefault();
                    if (pLineaSeleccionada != null)
                    {
                        if (ConfigurationManager.AppSettings["cuotasExtra"] != null)
                        {
                            string cuotaExtra = ConfigurationManager.AppSettings["cuotasExtra"].ToString();
                            if (cuotaExtra != "0")
                            {
                                pnlCuotaExtra.Visible = pLineaSeleccionada.Cuotas_Extras == null ? false : Convert.ToInt32(pLineaSeleccionada.Cuotas_Extras) == 1 ? true : false;
                                calcularCuotaAproximada();
                            }
                        }                        
                    }
                }
            }
        }
        gvCuoExt.DataSource = null;
        gvCuoExt.DataBind();
    }


    #region METODOS DE CUOTAS EXTRAS

    protected void btnGenerarCuotaExtra_Click(object sender, EventArgs e)
    {
        lblErrorCuotaExtra.Text = string.Empty;
        lblError.Text = string.Empty;
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
            
            if (string.IsNullOrEmpty(txtVrCredito.Text) || txtVrCredito.Text == "0")
                pMessageError += "<li>Debe ingresar el valor del crédito a solicitar.</li>";
            if (string.IsNullOrEmpty(txtPlazo.Text))
                pMessageError += "<li>Debe ingresar el plazo del crédito a solicitar.</li>";
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
            if (plazo_CuotaExtra > Convert.ToInt32(txtPlazo.Text))
            {
                lblErrorCuotaExtra.Text = "<li>El numero de cuotas por la periodicidad Excede el plazo</li>";
                return;
            }
            Decimal valor_limite = (Convert.ToDecimal(txtVrCredito.Text.Replace(".","")) * Convert.ToDecimal(txtPorcentaje.Text)) / 100;
            Decimal valor_cuota = Convert.ToDecimal(txtValorCuotaExt.Text.Replace(".","")) * Convert.ToDecimal(txtNumeroCuotaExt.Text);
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
            {
                gvCuoExt.DataSource = lstCuoExt;
                calcularCuotaAproximada();
            }
            gvCuoExt.DataBind();
            ViewState[PersonaLogin.cod_persona + "CuoExt"] = lstCuoExt;

        }
        catch (Exception ex)
        {
            lblError.Text = "Error generado en la generación. " + ex.Message;
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
                Label lbltipocuota = rFila.FindControl("lbltipocuota") as Label;
                vCuotaExtra.des_tipo_cuota = lbltipocuota.Text;
                num_ter += 1;
                vCuotaExtra.cod_cuota = num_ter;
                lstCuotasExtras.Add(vCuotaExtra);
            }
        }

        return lstCuotasExtras;
    }

    #endregion

    #region CUOTA APROXIMADA

    public void calcularCuotaAproximada()
    {
        txtCuotaAproximada.Visible = false;
        if (!string.IsNullOrWhiteSpace(txtVrCredito.Text) &&
            txtVrCredito.Text != "0" &&
            ddlLinea.SelectedValue != "0" &&
            !string.IsNullOrWhiteSpace(txtPlazo.Text) &&
            ddlPeriodicidad.SelectedValue != "0")
        {
            string cuota = "";
            cuota = ActualizarCuota();
            if (!string.IsNullOrWhiteSpace(cuota))
            {
                txtCuotaAproximada.Text = cuota;
                txtCuotaAproximada.Visible = true;
            }
        }
    }

    private string ActualizarCuota()
    {
        List<xpinnWSCredito.CuotasExtras> lstCuotasExtras = ObtenerListaCuotasExtras();
        try
        {
            decimal comision = 0, Aporte = 0, seguro = 0;
            int tipo_liquidacion = 1;
            decimal tasa = 0;
            int monto = 0;
            int plazo = 0;
            int linea = 0;
            DateTime fechaprimerpago = DateTime.Now.AddMonths(1);

            //carga plazo
            if (!string.IsNullOrEmpty(txtPlazo.Text))
                plazo = Convert.ToInt32(txtPlazo.Text.ToString().Replace(",", "").Replace(".", ""));
            //carga datos de persona
            _persona = (xpinnWSLogin.Persona1)Session["persona"];
            xpinnWSCredito.Simulacion pDatos = new xpinnWSCredito.Simulacion();
            //cargar liquidación
            List<xpinnWSAppFinancial.LineasCredito> lstLineas = (List<xpinnWSAppFinancial.LineasCredito>)ViewState["DTLineasCredito"];

            if (lstLineas.Count > 0)
            {
                xpinnWSAppFinancial.LineasCredito pLineaSeleccionada = lstLineas.Where(x => x.cod_linea_credito == ddlLinea.SelectedValue).FirstOrDefault();
                if (pLineaSeleccionada != null)
                {
                    if (!string.IsNullOrEmpty(pLineaSeleccionada.tipoliquidacion))
                        tipo_liquidacion = Convert.ToInt32(pLineaSeleccionada.tipoliquidacion);
                }
            }
            //carga periodicidad
            string valPeriodicidad = ddlPeriodicidad.SelectedValue;
            //carga linea
            linea = Convert.ToInt32(ddlLinea.SelectedValue);
            //carga monto
            if (!string.IsNullOrWhiteSpace(txtVrCredito.Text))
            {
                string texto;
                texto = txtVrCredito.Text.Replace(".", "");
                monto = Convert.ToInt32(texto);
            }                
            //carga tasa
            try
            {
                tasa = AppServices.obtenerTasaEspecifica(linea.ToString(), plazo, Session["sec"].ToString());
                if(tasa > 0)
                {
                    decimal tasaEfectivaMensualMesVencido = tasa / 12;
                    txtTasa.Text = tasaEfectivaMensualMesVencido.ToString();
                    //calcula cuota
                    pDatos = BOCredito.ConsultarSimulacionCuota(monto, plazo, Convert.ToInt32(valPeriodicidad), linea.ToString(), tipo_liquidacion, tasa, comision, Aporte, fechaprimerpago, _persona.clavesinecriptar, _persona.cod_persona, lstCuotasExtras, Session["sec"].ToString());
                }                
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
            string cuotaAprox = "";
            if (pDatos != null)
                cuotaAprox = pDatos.cuota.ToString();

            return cuotaAprox;
        }
        catch (Exception ex)
        {
            VerError("Se generó un error al Calcular la cuota " + ex.Message);
            return null;
        }
    }
    #endregion

    protected void ddlPeriodicidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlPeriodicidad.SelectedValue != "0")
            calcularCuotaAproximada();
    }

    protected void txtPlazo_TextChanged(object sender, EventArgs e)
    {
        try{
            if (!string.IsNullOrWhiteSpace(txtVrCredito.Text))
            {
                if (!string.IsNullOrWhiteSpace(txtPlazo.Text))
                {
                    int plazo = Convert.ToInt32(txtPlazo.Text);
                    calcularCuotaAproximada();
                }
            }
        }
        catch
        {
            lblError.Text = "el plazo debe ser un valor entero";
            lblError.Visible = true;
        }
    }
}