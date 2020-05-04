using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Configuration;
using xpinnWSAppFinancial;
using System.IO;
using System.Data;
using System.Globalization;
using xpinnWSCredito;

public partial class Convenios : GlobalWeb
{
    xpinnWSAppFinancial.WSAppFinancialSoapClient AppServices = new xpinnWSAppFinancial.WSAppFinancialSoapClient();
    xpinnWSAppFinancial.LineaCred_Destinacion linea = new xpinnWSAppFinancial.LineaCred_Destinacion();
    xpinnWSCredito.WSCreditoSoapClient BOCredito = new xpinnWSCredito.WSCreditoSoapClient();
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient BOEstado = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    xpinnWSLogin.Persona1 _persona;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ValidarSession();
            VisualizarTitulo(OptionsUrl.solicitarConvenio, "Sol");
            Site toolBar = (Site)Master;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.MostrarRegresar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("EstadoDeCuenta", "Page_PreInit", ex);
        }
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        _persona = (xpinnWSLogin.Persona1)Session["persona"];

        string filtro = "";
        filtro += " D.Oficina_Virtual = 1 ";
        if (!string.IsNullOrEmpty(txtFiltro.Text))
        {
            filtro += " and D.descripcion like '%" + txtFiltro.Text.ToUpper() + "%'";
        }
        List<xpinnWSAppFinancial.LineaCred_Destinacion> lstDestinacion = new List<xpinnWSAppFinancial.LineaCred_Destinacion>();
        lstDestinacion = AppServices.ListarConvenios(linea, filtro, Session["sec"].ToString());
        if (lstDestinacion != null)
        {                        
            cargarServicios(lstDestinacion);
        }
        DateTime fecha = DateTime.Now;
        txtFechaServicio.Text = fecha.ToString();
        txtFechaPago.Text = fecha.ToString();
    }


    private void cargarServicios(List<xpinnWSAppFinancial.LineaCred_Destinacion> lstServicios)
    {
        //Ordena los servicion por tipo
        foreach (var item in lstServicios){            
            string url = "../../Imagenes/default.jpg";
            if (item.foto != null)
            {
                try
                {
                    Bytes_A_Archivo(item.cod_destino.ToString(), item.foto);
                    url = "../../Imagenes/TempImages/" + item.cod_destino.ToString() + ".jpg";
                }
                catch
                {
                    url = "../../Imagenes/default.jpg";
                }
            }

            pnlCards.Controls.Add(new LiteralControl(@"<div class='col-md-3 col-sm-12'>
                                                           <div class='card' style='width: 20rem;width: 20rem;margin: 10px auto;padding: 10px;border: 1px solid #c1b1b18f;border-radius: 5%;'>
                                                               <img class='card-img-top' style='object-fit: cover; width: 100%; height: 100px;' src='" + url+ @"' alt='Card image cap'>
                                                               <div class='card-block'>
                                                                   <h4 class='card-title' style='height: 38px;'>" + item.descripcion + @"</h4>"));

            //Agrega boton
            Button myButton = new Button();
            pnlCards.Controls.Add(myButton);
            myButton.ToolTip = item.descripcion;
            myButton.Text = "Solicitar";
            myButton.ID = item.cod_destino.ToString()+"|"+item.cod_linea_credito;
            myButton.CssClass = "btn btn-primary";
            myButton.Click += btnSolicitar_Click;

            pnlCards.Controls.Add(new LiteralControl(@"</div></div></div>"));            
        }
    }    
    

    protected void btnSolicitar_Click(object sender, EventArgs e)
    {
        try
        {             
            string cadena = ((Button)sender).ID;
            string[] datos;
            datos = cadena.Split('|');
            string linea_cred = "0";
            string codigo = datos[0].ToString();
            //carga datos de la linea de crédito
            //El campo (1) contiene la linea de crédito
            if (!string.IsNullOrEmpty(datos[1].ToString()))
            {
                linea_cred = datos[1].ToString();
                txtLineaCred.Text = linea_cred;
                xpinnWSCredito.LineasCredito pEntidad = new xpinnWSCredito.LineasCredito();
                pEntidad = BOCredito.Calcular_Cupo(linea_cred, _persona.cod_persona, DateTime.Now, _persona.clavesinecriptar, Session["sec"].ToString());
                if (pEntidad != null)
                {
                    txtPlazoMax.Text = pEntidad.Plazo_Maximo.ToString();
                    txtValorMax.Text = Convert.ToDecimal(pEntidad.Monto_Maximo.ToString()).ToString("C0");
                }
            }
            string titulo = ((Button)sender).ToolTip;
            txtLinea.Text = codigo;
            txtServicio.Text = titulo;
            pnlCards.Visible = false;
            pnlDatos.Visible = true;

            if (!string.IsNullOrEmpty(txtLinea.Text))
            {
                //carga enlace e imagen promocional
                _persona = (xpinnWSLogin.Persona1)Session["persona"];
                string filtro = " D.cod_destino = " + codigo +" ";
                List<xpinnWSAppFinancial.LineaCred_Destinacion> lstServicios = new List<xpinnWSAppFinancial.LineaCred_Destinacion>();
                xpinnWSAppFinancial.LineaCred_Destinacion lin = new xpinnWSAppFinancial.LineaCred_Destinacion();
                lstServicios = AppServices.ListarConvenios(lin, filtro, Session["sec"].ToString());
                if (lstServicios != null)
                {
                    lin = lstServicios.ElementAt(0);
                    if (!string.IsNullOrWhiteSpace(lin.enlace))
                    {
                        pnlinfo.Controls.Add(new LiteralControl("<br><br><a href='"+lin.enlace+ "' target='_blank' class='btn btn-success'>Ver página</a><br><br>"));
                    }
                    if(lin.banner != null)
                    {
                        string url = "../../Imagenes/default.jpg";
                        try
                        {
                            Bytes_A_Archivo("banner"+lin.cod_destino, lin.banner);
                            url = "../../Imagenes/TempImages/banner"+lin.cod_destino+".jpg";
                            pnlinfo.Controls.Add(new LiteralControl("<div><img class='card-img-top' style='object-fit: cover; margin:0 auto; margin-bottom: 15px; width: 700px; height: auto;' src='" + url + @"' alt='Card image cap'></div>"));
                        }
                        catch
                        {
                            url = "../../Imagenes/default.jpg";
                        }                        
                    }
                }
                //Se deben consultar el so valiporte requeridos por la destinación si es que tiene (Campo nuevo) Destinacion.soporte
                //Si tiene algo se debe mostrar el panel de adjuntos pasando el nombre del archivo requerido
                if(codigo == "769" || codigo == "770") //Cambiar esta condición por (if requerido != null)
                {
                    pnlAdjunto.Visible = true;
                    lblDocumento.Text = "Recibo impuesto predial"; //Poner el nombre del archivo requerido
                }
            }            
        }
        catch (Exception ex)
        {
            lblError.Text = "Se presento un problema " + ex.Message;
        }
    }

    private Boolean CargarDocumentosCredito()
    {
        Session["docs"] = null;
        List<xpinnWSCredito.DocumentosAnexos> LstDocum = new List<xpinnWSCredito.DocumentosAnexos>();
        xpinnWSCredito.DocumentosAnexos pDocum = new xpinnWSCredito.DocumentosAnexos();
            if (fuArchivo != null)
            {
                if (!fuArchivo.HasFile)
                {
                    lblError.Text = "Cargue el documento requerido";
                    return false;
                }
                String extension = System.IO.Path.GetExtension(fuArchivo.PostedFile.FileName).ToLower();
                if (extension != ".pdf" && extension != ".jpg" && extension != ".jpeg" && extension != ".bmp" && extension != ".png")
                {
                    lblError.Text = "El archivo no tiene el formato correcto";
                    return false;
                }
                pDocum.descripcion = "Solicitud convenio Web";
                pDocum.extension = extension;
                pDocum.tipo_producto = 2;

                int tamMax = Convert.ToInt32(ConfigurationManager.AppSettings["TamañoMaximoArchivo"]);
                if (fuArchivo.FileBytes.Length > tamMax)
                {
                    lblError.Text = "El tamaño del archivo excede el tamaño limite de ( 2MB )";
                    return false;
                }

                StreamsHelper streamHelper = new StreamsHelper();
                byte[] bytesArrImagen;
                using (System.IO.Stream streamImagen = fuArchivo.PostedFile.InputStream)
                {
                    bytesArrImagen = streamHelper.LeerTodosLosBytesDeUnStream(streamImagen);
                }
                Int64 pTipoDocumento = 0;
                pDocum.tipo_documento = pTipoDocumento;
                pDocum.fechaanexo = DateTime.Today;
                pDocum.imagen = bytesArrImagen;
                pDocum.estado = 1;
                LstDocum.Add(pDocum);
                Session["docs"] = LstDocum;
        }
        return true;
    }

    private bool Validar()
    {
        if (string.IsNullOrWhiteSpace(txtFechaPago.Text))
        {
            lblError.Text = "Diligencie todos los campos";
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtNumeroCuotas.Text))
        {
            lblError.Text = "Diligencie todos los campos";
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtValor.Text))
        {
            lblError.Text = "Diligencie todos los campos";
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtFechaServicio.Text))
        {
            lblError.Text = "Diligencie todos los campos";
            return false;
        }
        if (!string.IsNullOrWhiteSpace(txtPlazoMax.Text) && txtPlazoMax.Text != "0")
        {
            if(Convert.ToInt32(txtNumeroCuotas.Text) > Convert.ToInt32(txtPlazoMax.Text))
            {
                lblError.Text = "El número de cuotas no puede superar el plazo máximo";
                return false;
            }
        }
        if (!string.IsNullOrWhiteSpace(txtValorMax.Text.Replace("$","")) && txtPlazoMax.Text != "0")
        {
            if (Convert.ToDecimal(txtValor.Text.Replace(",","").Replace(".","").Replace("$","")) > Convert.ToDecimal(txtValorMax.Text.Replace("$", "").Replace(",", "").Replace(".", "")))
            {
                lblError.Text = "No puede superar el valor máximo para este servicio";
                return false;
            }
        }
        return true;
    }

    protected void btnCerrarCambioCuota_Click(object sender, EventArgs e)
    {
        txtFechaPago.Text = "";
        txtLinea.Text = "";
        txtNumeroCuotas.Text = "";
        txtServicio.Text = "";
        txtValor.Text = "";
        pnlCards.Visible = true;
        pnlDatos.Visible = false;
        panelFinal.Visible = false;
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        try
        {
            if (Validar())
            {
                if(pnlAdjunto.Visible)
                {
                    if (!CargarDocumentosCredito())
                        return;
                }
                ctlMensaje.MostrarMensaje("¿Desea generar la solicitud?");
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }


    public string Bytes_A_Archivo(string id, Byte[] ImgBytes)
    {
        Stream stream = null;
        try
        {
            File.Delete(Server.MapPath("..\\..\\Imagenes\\TempImages\\") + Path.GetFileName(id + ".jpg"));
        }
        catch (Exception e)
        {

        }
        string fileName = Server.MapPath("..\\..\\Imagenes\\TempImages\\") + Path.GetFileName(id + ".jpg");
        if (ImgBytes != null)
        {
            try
            {
                // Guardar imagen en un archivo
                stream = File.OpenWrite(fileName);
                foreach (byte b in ImgBytes)
                {
                    stream.WriteByte(b);
                }
                stream.Close();
                //this.hdFileName.Value = Path.GetFileName(id + ".jpg");
            }
            finally
            {
                /*Limpiamos los objetos*/
                stream.Dispose();
                stream = null;
            }
        }
        return fileName;
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {        
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            List<xpinnWSCredito.DocumentosAnexos> lstDocumentos = null;
            if (Session["docs"] != null)
                lstDocumentos = Session["docs"] as List<xpinnWSCredito.DocumentosAnexos>;

            _persona = (xpinnWSLogin.Persona1)Session["persona"];            
            xpinnWSCredito.SolicitudCreditoAAC pEntidad = new xpinnWSCredito.SolicitudCreditoAAC();    
                                                               
            //DATA FINANCIERA
            pEntidad.concepto = "SOLICITUD ATENCION AL CLIENTE - CONVENIOS - "+txtDescripcion.Text;
            pEntidad.numerosolicitud = 0;
            pEntidad.fechasolicitud = DateTime.Now;
            pEntidad.cod_persona = _persona.cod_persona;
            pEntidad.montosolicitado = Convert.ToDecimal(txtValor.Text.Replace(".", "").Replace(",","").Replace("$", ""));
            pEntidad.plazosolicitado = Convert.ToInt32(txtNumeroCuotas.Text);
            pEntidad.cuotasolicitada = 0;
            pEntidad.tipocredito = Convert.ToInt32(txtLineaCred.Text);
            pEntidad.periodicidad = 1;
            pEntidad.forma_pago = 2;          
            pEntidad.usuario = "WEB";            
            pEntidad.destino = Convert.ToInt32(txtLinea.Text);            
            pEntidad.estado = 0;
            pEntidad.empresa_recaudo = Convert.ToInt32(_persona.idEmpresa);

            //Se realiza la solicitud de crédito
            List<xpinnWSCredito.CuotasExtras> lstCuotas = new List<xpinnWSCredito.CuotasExtras>();
            
                pEntidad = BOCredito.CrearSolicitudCreditoProveedor(pEntidad, Session["sec"].ToString(), lstDocumentos);
            if (pEntidad.numerosolicitud != 0)
            {
                Session["docs"] = null;
                lblCodigoGenerado.Text = pEntidad.numerosolicitud.ToString();
                panelFinal.Visible = true;
                pnlDatos.Visible = false;
                if (ConfigurationManager.AppSettings["aprobarSoliCredito"] != null)
                {
                    string aprobar = ConfigurationManager.AppSettings["aprobarSoliCredito"].ToString();
                    if (aprobar != "0")
                    {
                        //Confirmar Solicitud
                        Int64 radicado = BOCredito.ConfirmarSolicitudCreditoAutomatico(pEntidad, Session["sec"].ToString());
                    }
                }
            }            
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}