using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using xpinnWSEstadoCuenta;
using Xpinn.Util;
using System.Net.Mail;

public partial class Nuevo : GlobalWeb
{
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient EstadoServicio = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    xpinnWSCredito.WSCreditoSoapClient BOCredito = new xpinnWSCredito.WSCreditoSoapClient();
    xpinnWSLogin.Persona1 _persona;
    xpinnWSDeposito.WSDepositoSoapClient BoDeposito = new xpinnWSDeposito.WSDepositoSoapClient();
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ValidarSession();
   
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
            
            //verificando si esta logeado o persona reciente.
            if (Request.QueryString["cod_persona"] != null)
            {
                lblcod_persona.Text = Request.QueryString["cod_persona"].ToString().Trim();
               
            }
            else
            {
                if (Request.QueryString["id"] != null)
                {
                    lblid.Text = Request.QueryString["id"].ToString().Trim();
                }
            }
            panelData.Visible = true;
            panelFinal.Visible = false;
            CargarDropDownYCheckBox();
            DateTime pFechaActual = DateTime.Now;
            txtDiaEncabezado.Text = pFechaActual.Day.ToString();
            ddlMesEncabezado.SelectedValue = pFechaActual.Month.ToString();
            txtAnioEncabezado.Text = pFechaActual.Year.ToString();
        }
    }

    protected void CargarDropDownYCheckBox()
    {
        LlenarMesesDrop(ddlMesEncabezado);
      

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstLineas = new List<xpinnWSEstadoCuenta.ListaDesplegable>();

        lstLineas = EstadoServicio.PoblarListaDesplegable("LINEAAHORRO", "", " ESTADO = 1 ", "2", Session["sec"].ToString());
        if (lstLineas.Count > 0)
        {
            LlenarDrop(ddlLinea, lstLineas);
        }
        lstLineas = EstadoServicio.PoblarListaDesplegable("lineaprogramado", "", " ESTADO = 1 ", "2", Session["sec"].ToString());
        if (lstLineas.Count > 0)
        {
            LlenarDrop(ddlLinea1, lstLineas);
        }
        lstLineas = EstadoServicio.PoblarListaDesplegable("lineacdat", "", " ESTADO = 1 ", "2", Session["sec"].ToString());
        if (lstLineas.Count > 0)
        {
            LlenarDrop(ddlLinea2, lstLineas);
        }
        lstLineas = EstadoServicio.PoblarListaDesplegable("periodicidad","", " ", "", Session["sec"].ToString());
        if (lstLineas.Count > 0)
        {
            LlenarDrop(ddlPeriodicidad, lstLineas);
            LlenarDrop(ddlPeriodicidad2, lstLineas);
            LlenarDrop(ddlPeriodicidad3, lstLineas);
        }
        lstLineas = EstadoServicio.PoblarListaDesplegable("oficina", "Cod_Oficina,Nombre", " ", "", Session["sec"].ToString());
        if (lstLineas.Count > 0)
        {
            LlenarDrop(ddlOficina1, lstLineas);
            LlenarDrop(ddlOficina2, lstLineas);
            LlenarDrop(ddlOficina3, lstLineas);
        }
        lstLineas = EstadoServicio.PoblarListaDesplegable("asejecutivos", "iusuario, QUITARESPACIOS(Substr(snombre1 || ' ' || snombre2 || ' ' || sapellido1 || ' ' || sapellido2, 0, 240))", " ", "", Session["sec"].ToString());
        if (lstLineas.Count > 0)
        {
            LlenarDrop(ddlAsesores1, lstLineas);
            LlenarDrop(ddlAsesores2, lstLineas);
            LlenarDrop(ddlAsesores3, lstLineas);
        }

    }

    void LlenarDrop(DropDownList ddlDropCarga, List<xpinnWSEstadoCuenta.ListaDesplegable> listaData)
    {
        ddlDropCarga.DataSource = listaData;
        ddlDropCarga.DataTextField = "descripcion";
        ddlDropCarga.DataValueField = "idconsecutivo";
        ddlDropCarga.AppendDataBoundItems = true;
        ddlDropCarga.Items.Insert(0, new ListItem("Seleccione un item", "0"));
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
      
        if (ddlLinea.SelectedItem == null)
        {
            lblError.Text = "No existen líneas creadas, comuníquese con nosotros para reportar este inconveniente ( Informaci&oacute;n Financiera )";
            ddlLinea.Focus();
            return false;
        }
        if (ddlLinea.SelectedIndex == 0 && TipAhorro.SelectedValue=="1")
        {
            lblError.Text = "Seleccione la linea de ahorros a la vista ( Informaci&oacute;n Financiera )";
            ddlLinea.Focus();
            return false;
        }
        if (ddlLinea1.SelectedIndex == 0 && TipAhorro.SelectedValue == "2")
        {
            lblError.Text = "Seleccione la linea de ahorros programados ( Informaci&oacute;n Financiera )";
            ddlLinea1.Focus();
            return false;
        }
        if (ddlLinea2.SelectedIndex == 0 && TipAhorro.SelectedValue == "3")
        {
            lblError.Text = "Seleccione la linea de CDAT ( Informaci&oacute;n Financiera )";
            ddlLinea2.Focus();
            return false;
        }
        if (ddlPeriodicidad.SelectedIndex == 0 && TipAhorro.SelectedValue == "1")
        {
            lblError.Text = "Seleccione la periodicidad ( Informaci&oacute;n Financiera )";
            ddlPeriodicidad.Focus();
            return false;
        }
        if (ddlPeriodicidad2.SelectedIndex == 0 && TipAhorro.SelectedValue == "2")
        {
            lblError.Text = "Seleccione la periodicidad ( Informaci&oacute;n Financiera )";
            ddlPeriodicidad2.Focus();
            return false;
        }
        if (ddlPeriodicidad3.SelectedIndex == 0 && TipAhorro.SelectedValue == "3")
        {
            lblError.Text = "Seleccione la periodicidad ( Informaci&oacute;n Financiera )";
            ddlPeriodicidad3.Focus();
            return false;
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
                ctlMensaje.MostrarMensaje("¿Desea generar la solicitud del producto?");
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
            
            //Xpinn.Ahorros.Entities.SolicitudProductosWeb pEntidad = new Xpinn.Ahorros.Entities.SolicitudProductosWeb();
            xpinnWSDeposito.SolicitudProductosWeb pSolicitud = new xpinnWSDeposito.SolicitudProductosWeb();
            pSolicitud = BoDeposito.ConsultarSolicitud(Session["sec"].ToString());
            pSolicitud.IDSOLICITUD = pSolicitud.IDSOLICITUD + 1;


            pSolicitud.TIPOAHORRO = TipAhorro.SelectedValue;
            pSolicitud.COD_PERSONA = 0;
            if (lblcod_persona.Text.Trim() != "")
                pSolicitud.COD_PERSONA = Convert.ToInt64(lblcod_persona.Text.Trim());
            pSolicitud.FECHA = DateTime.Now;
          

            if (TipAhorro.SelectedValue=="3")
            {
                pSolicitud.COD_LINEAAHORRO = ddlLinea2.SelectedValue;
                pSolicitud.PERIODICIDAD = Convert.ToInt64(ddlPeriodicidad3.SelectedValue);
                pSolicitud.ESTADO = 0;
                pSolicitud.FORMA_PAGO = Convert.ToInt32(ddlFormaPago3.SelectedValue);
                pSolicitud.VALOR_CUOTA = Convert.ToDecimal(Decimales2.Text.Replace(".", ""));
                pSolicitud.MODALIDAD =Convert.ToInt32(ddlModalidad2.SelectedValue);
                pSolicitud.OFICINA = Convert.ToInt32(ddlOficina3.SelectedValue);
                pSolicitud.COD_ASESOR = Convert.ToInt32(ddlAsesores3.SelectedValue);
                pSolicitud.PLAZO = Convert.ToInt32(txtPlazo2.Text);
                
            }
            if (TipAhorro.SelectedValue == "2")
            {
                pSolicitud.COD_LINEAAHORRO = ddlLinea1.SelectedValue;
                pSolicitud.PERIODICIDAD = Convert.ToInt64(ddlPeriodicidad2.SelectedValue);
                pSolicitud.ESTADO = 0;
                pSolicitud.FORMA_PAGO = Convert.ToInt32(ddlFormaPago2.SelectedValue);
                pSolicitud.VALOR_CUOTA = Convert.ToDecimal(Decimales1.Text.Replace(".", ""));
                pSolicitud.MODALIDAD = 0;
                pSolicitud.OFICINA = Convert.ToInt32(ddlOficina2.SelectedValue);
                pSolicitud.COD_ASESOR = Convert.ToInt32(ddlAsesores2.SelectedValue);
                pSolicitud.PLAZO = Convert.ToInt32(txtPlazo1.Text);
            }
            if (TipAhorro.SelectedValue == "1")
            {
                pSolicitud.COD_LINEAAHORRO = ddlLinea.SelectedValue;
                pSolicitud.PERIODICIDAD = Convert.ToInt64(ddlPeriodicidad.SelectedValue);
                pSolicitud.ESTADO = 0;
                pSolicitud.FORMA_PAGO = Convert.ToInt32(ddlFormaPago.SelectedValue);
                pSolicitud.VALOR_CUOTA = Convert.ToDecimal(txtVrCredito.Text.Replace(".", ""));
                pSolicitud.MODALIDAD = Convert.ToInt32(ddlModalidad.SelectedValue);
                pSolicitud.OFICINA = Convert.ToInt32(ddlOficina1.SelectedValue);
                pSolicitud.COD_ASESOR = Convert.ToInt32(ddlAsesores1.SelectedValue);
                pSolicitud.PLAZO = 0;
            }
       
           
             BoDeposito.CrearSolicitudProduAAC(pSolicitud, Session["sec"].ToString());

            panelFinal.Visible = true;
            EnviarCorreoAlAsociado(pSolicitud);
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    
   
    protected void btnGuardarDoc_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        try
        {
            List<xpinnWSCredito.DocumentosAnexos> lstDocumentos = new List<xpinnWSCredito.DocumentosAnexos>();

           
            xpinnWSCredito.SolicitudCreditoAAC pEntidad = new xpinnWSCredito.SolicitudCreditoAAC();
            if (ViewState["Soli_Credito"] == null)
            {
                lblError.Text = "Ocurrió un problema al generar la solicitud, vuelva a realizar el proceso por favor.";
                return;
            }
            pEntidad = (xpinnWSCredito.SolicitudCreditoAAC)ViewState["Soli_Credito"];
            pEntidad = BOCredito.CrearSolicitudCreditoAAC(pEntidad, _persona.identificacion, _persona.clavesinecriptar, Session["sec"].ToString(), lstDocumentos,null);
            if (pEntidad.numerosolicitud != 0)
            {
                
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


    void EnviarCorreoAlAsociado(xpinnWSDeposito.SolicitudProductosWeb pEntidad)
    {
        try
        {
            lblError.Text = "";
            
            xpinnWSCredito.TiposDocCobranzas documentoFormatoCorreo = BOCredito.ConsultarInformacionCorreoSolicitudCred(Session["sec"].ToString());

            if (string.IsNullOrWhiteSpace(documentoFormatoCorreo.empresa.e_mail) || string.IsNullOrWhiteSpace(documentoFormatoCorreo.empresa.clave_e_mail))
            {
                //VerError("La empresa no tiene configurado un email para enviar el correo");
                return;
            }
            else if (string.IsNullOrWhiteSpace(documentoFormatoCorreo.texto))
            {
                //VerError("No esta parametrizado el formato del correo a enviar");
                return;
            }
            else if (string.IsNullOrWhiteSpace(_persona.email))
            {
                return;
            }


            LlenarDiccionarioGlobalWebParaCorreo(pEntidad);

            documentoFormatoCorreo.texto = ReemplazarParametrosEnElMensajeCorreo(documentoFormatoCorreo.texto);

            CorreoHelper correoHelper = new CorreoHelper(_persona.email, documentoFormatoCorreo.empresa.e_mail, documentoFormatoCorreo.empresa.clave_e_mail);
            //bool exitoso = correoHelper.EnviarCorreoConHTML(documentoFormatoCorreo.texto, Correo.Gmail, documentoFormatoCorreo.descripcion);

            // hacer algo si el correo a un asociado falla, no se si informar o ignorar y pasar
            //if (!exitoso)
            //{

            //}

            //

            using (System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage("ExpinnAlertas@gmail.com", documentoFormatoCorreo.empresa.e_mail))
            {
                mm.Subject = documentoFormatoCorreo.descripcion;
                mm.Body = documentoFormatoCorreo.texto;

                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential();
                credentials.UserName = "ExpinnAlertas@gmail.com";
                credentials.Password = "Exp1nnAdm";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = credentials;
                smtp.Port = 587;
                smtp.Send(mm);

            }
        }
        catch (Exception ex)
        {
            lblError.Text = "Error al enviar el correo, " + ex.Message;
        }
    }


    void LlenarDiccionarioGlobalWebParaCorreo(xpinnWSDeposito.SolicitudProductosWeb entidad)
    {
        parametrosFormatoCorreo = new Dictionary<ParametroCorreo, string>();

        parametrosFormatoCorreo.Add(ParametroCorreo.NombreCompletoPersona, _persona.nombre);
        parametrosFormatoCorreo.Add(ParametroCorreo.Identificacion, _persona.identificacion);
        parametrosFormatoCorreo.Add(ParametroCorreo.FechaCredito, DateTime.Today.ToShortDateString());
        parametrosFormatoCorreo.Add(ParametroCorreo.MontoCredito, entidad.VALOR_CUOTA.ToString());
        parametrosFormatoCorreo.Add(ParametroCorreo.PlazoCredito, entidad.PLAZO.ToString());
        parametrosFormatoCorreo.Add(ParametroCorreo.LineaCredito, ddlLinea.SelectedItem.Text);
        //parametrosFormatoCorreo.Add(ParametroCorreo.NumeroSolicitud, entidad.NUM_CUENTA.ToString());
    }


    protected void TipAhorro_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (TipAhorro.SelectedValue=="1")
        {
            pnAhorroVista.Visible = true;
            pnProgramados.Visible = false;
            pnCDATS.Visible = false;
            LbldatosPersonales.Text = "Datos de Ahorros a la Vista ";
            lbltipAhorro.Text = "Ahorros a la Vista: ";
        }
        if (TipAhorro.SelectedValue == "2")
        {
            pnProgramados.Visible = true;
            pnAhorroVista.Visible = false;
            pnCDATS.Visible = false;
            LbldatosPersonales.Text = "Datos de Ahorros Programados ";
            lbltipAhorro1.Text = "Ahorros Programados: ";
        }
        if (TipAhorro.SelectedValue == "3")
        {
            pnCDATS.Visible = true;
            pnAhorroVista.Visible = false;
            pnProgramados.Visible = false;
            LbldatosPersonales.Text = "Datos de CDATS";
            lbltipAhorro2.Text = "CDATS: ";
        }
    }

    protected void cblPropiedad_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    protected void TotalizarGastos()
    {
        decimal pVrTotal = 0, pVrArri = 0, pVrGastos = 0, pVrOtros = 0;

    
        pVrTotal = pVrArri + pVrGastos + pVrOtros;
      
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
       
    }
    protected void rbllPignorado_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void TotalizarMontoCreditos(int pOpcion)
    {
        decimal pVrTotal = 0, pVr1 = 0, pVr2 = 0;
        if (pOpcion == 1)
        {
         
            pVrTotal = pVr1 + pVr2;
          
        }
        else
        {
         
            pVrTotal = pVr1 + pVr2;
          
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
          
            panelData.Visible = false;
           
            return true;
        }
        else
        {
         
            panelData.Visible = false;
            return false;
        }
    }


    protected void btnInicio_Click(object sender, EventArgs e)
    {
        Navegar("~/Detalle.aspx");
    }

   
}