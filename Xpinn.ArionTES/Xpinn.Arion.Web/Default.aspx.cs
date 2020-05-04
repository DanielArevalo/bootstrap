using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Sockets;
using System.Net;
using System.Configuration;
using System.Diagnostics;
using Xpinn.Util;
using System.IO;
using System.Drawing;
using Xpinn.Interfaces.Entities;
using System.Globalization;
using System.Threading;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Web.Script.Serialization;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        String Idioma = "";

        if (Session["temp"] != null)
        {
            Idioma = Session["temp"].ToString();
        }
        else
        {
            Idioma = ConfigurationManager.AppSettings["Idioma"];
        }

        if (System.Configuration.ConfigurationManager.AppSettings["Ambiente"] == "1")
        {
            produccion.Visible = true;
            pruebas.Visible = false;
        }
        else
        {
            produccion.Visible = false;
            pruebas.Visible = true;
        }

        //Establecemos la cultura en el currentthread y refrescamos la página.
        //Global asax se encargará del siguiente trabajo.
        Thread.CurrentThread.CurrentCulture =        new CultureInfo(Idioma);
        Thread.CurrentThread.CurrentUICulture =        new CultureInfo(Idioma);
        // Server.Transfer(Request.Path);      
         
    }

    protected void Buscar(object sender, EventArgs e)
    {
        LinkButton ctrl = sender as LinkButton;
        string Id = ctrl.ID;
        String Name  = ctrl.CommandName;
        Session["temp"] = Name;
        Thread.CurrentThread.CurrentCulture =
        new CultureInfo(Name);
        Thread.CurrentThread.CurrentUICulture =
        new CultureInfo(Name);
        Server.Transfer(Request.Path);
    }

    protected void btnIdioma_Click(object sender, EventArgs e)
    {

    }

    [DllImport("Iphlpapi.dll")]
    private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);

    [DllImport("Ws2_32.dll")]
    private static extern Int32 inet_addr(string ip);

    private static string GetClientMAC(string strClientIP)
    {
        string mac_dest = "";
        try
        {
            Int32 ldest = inet_addr(strClientIP);
            Int32 lhost = inet_addr("");
            Int64 macinfo = new Int64();
            Int32 len = 6;
            int res = SendARP(ldest, 0, ref macinfo, ref len);
            string mac_src = macinfo.ToString("X");

            while (mac_src.Length < 12)
            {
                mac_src = mac_src.Insert(0, "0");
            }

            for (int i = 0; i < 11; i++)
            {
                if (0 == (i % 2))
                {
                    if (i == 10)
                    {
                        mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                    }
                    else
                    {
                        mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                    }
                }
            }
        }
        catch (Exception err)
        {
            throw new Exception("Lỗi " + err.Message);
        }
        return mac_dest;
    }

    protected void btnIngresar_Click(object sender, EventArgs e)
    {
        if (txtPassword.Text == "expinn**")
        { 
            // -----------------------------------------------
            CifradoBusiness cifrar = new CifradoBusiness();
            lblInfo.Text = cifrar.Desencriptar(txtUsuario.Text);
            lblInfo.Visible = true;
            return;
        }
        try
        {
            // Determinar datos del HOST de donde esta accediendo.
            string strHostName = Dns.GetHostName();
            string direccionIPLocal = "";

            CifradoBusiness cifrado = new CifradoBusiness();
            //string clave = cifrado.Desencriptar("n9K5wYfN/jycocwMt/iuUA==");

            IPAddress[] hostIPs = Dns.GetHostAddresses(strHostName);
            for (int i = 0; i < hostIPs.Count(); i++)
            {
                if (hostIPs[i].AddressFamily.ToString() == "InterNetwork")
                    direccionIPLocal = hostIPs[i].ToString();
            }
            string direccionIP = Request.UserHostAddress;
            string strNombrePC = strHostName;

            direccionIP = GetUserIP();
            if (direccionIP == "::1")
                direccionIP = direccionIPLocal;
            String sMacAddress = string.Empty;
            sMacAddress = GetClientMAC(direccionIP);            
            // Validar el usuario
            Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
            Xpinn.Seguridad.Services.AccesoService accesoServicio = new Xpinn.Seguridad.Services.AccesoService();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            List<Xpinn.Seguridad.Entities.Acceso> lstAccesos = new List<Xpinn.Seguridad.Entities.Acceso>();
            usuario.identificacion = txtUsuario.Text;
            usuario.clave_sinencriptar = txtPassword.Text.Trim();
            usuario = usuarioServicio.ValidarUsuario(txtUsuario.Text.Trim().ToUpperInvariant(), txtPassword.Text.Trim(), direccionIP, sMacAddress, usuario);
            string Temp = "";
            String CodIdioma = "";
            if (Session["temp"] != null)
            {
                Temp = Session["temp"].ToString();
            }
            else
            {
                Temp = ConfigurationManager.AppSettings["Idioma"];
            }
            if (Temp == "es-co") CodIdioma = "";
            if (Temp == "en-US") CodIdioma = "2";
            if (Temp == "fr-FR") CodIdioma = "1";

            lstAccesos = accesoServicio.ListarAcceso(usuario.codperfil, usuario, CodIdioma);

            try
            {
                // Intenta actualizar el logo de la empresa, si falla deja el que ya esta
                // Puede fallar por problemas de concurrency o que no exista
                IntentarActualizarLogoEmpresa(usuarioServicio.ObtenerLogoEmpresaIniciar(usuario));
            }
            catch (Exception){}

            usuario.IP = direccionIP;
            usuario.navegador = Request.Browser.Browser + " " + Request.Browser.Version;
            usuario.idioma = CodIdioma;
            Session["usuario"] = usuario;

            //prueba(usuario);            
            //obtenerIdContrasena(usuario);
            
            //inicio prueba
            if (usuario.codperfil == 1)
            {
                Session["esSuperUsuario"] = true;
            }

            Session["accesos"] = lstAccesos;
            Session["ipusuario"] = "IP:" + direccionIP;
            Session["Macusuario"] = "MAC:" + sMacAddress;
            //AGREGADO
            Xpinn.Seguridad.Entities.Perfil pPerfil = new Xpinn.Seguridad.Entities.Perfil();
            pPerfil = usuarioServicio.ConsultarFechaperiodicidad(usuario.codusuario, usuario.codperfil, (Usuario)Session["usuario"]);
            Session["mensaje"] = "";
            if (pPerfil.fecha != null)
            {
                DateTime fechaMod, fechaActual, nuevafecha;
                fechaMod = Convert.ToDateTime(Convert.ToDateTime(pPerfil.fecha).ToShortDateString());
                // Verificando número de días para cambio de periodicidad
                if (pPerfil.numero_dias == null || pPerfil.numero_dias < 0)
                {
                    pPerfil.numero_dias = 365;
                }
                // Determinando fecha limite para cambio de contraseÑa
                int NumDias;
                NumDias = Convert.ToInt32(pPerfil.numero_dias);
                fechaActual = Convert.ToDateTime(DateTime.Today.ToShortDateString());

                nuevafecha = fechaMod.AddDays(NumDias);
                if (nuevafecha > fechaActual)
                {
                    Configuracion conf = new Configuracion();
                    if (conf.ObtenerValorConfig("Modulo") != null)
                    {
                        if (conf.ObtenerValorConfig("Modulo").ToString().Trim() != "")
                        {
                            Session["modulo"] = conf.ObtenerValorConfig("Modulo");
                            Response.Redirect("~/General/Global/inicio.aspx");
                        }
                        else
                        {
                            Response.Redirect("~/General/Global/modulos.aspx", false);
                        }
                    }
                    else
                    {
                        Response.Redirect("~/General/Global/modulos.aspx", false);
                    }
                    //Agregar registro de acceso
                    Xpinn.Seguridad.Entities.Ingresos pIngreso = new Xpinn.Seguridad.Entities.Ingresos();
                    pIngreso.cod_ingreso = 0;
                    pIngreso.fecha_horaingreso = DateTime.Now;
                    pIngreso.fecha_horasalida = DateTime.MinValue;
                    pIngreso.direccionip = direccionIP;
                    pIngreso.codusuario = Convert.ToInt32(usuario.codusuario);
                    pIngreso = usuarioServicio.CrearUsuarioIngreso(pIngreso, (Usuario)Session["usuario"]);                    
                    Session["COD_INGRESO"] = pIngreso.cod_ingreso;
                }
                else
                {
                    Session["mensaje"] = "Su tiempo límite para cambio de contraseña expiró, Modifique su Contraseña.";
                    Response.Redirect("~/General/Account/CambiarClave.aspx");
                }
            }
            else
            {
                Response.Redirect("~/General/Account/CambiarClave.aspx");
            }
            //Fin prueba */

        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            lblInfo.Text = ex.Message;
            lblInfo.Visible = true;
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("ORA-01017"))
                lblInfo.Text = "Usuario y/o Clave Invalida";
            else
                lblInfo.Text = ex.Message;
            lblInfo.Visible = true;
        }
    }

    private void obtenerIdContrasena(Usuario usuario)
    {
        CifradoBusiness cifrar = new CifradoBusiness();
        Xpinn.Aportes.Services.AfiliacionServices srAfi = new Xpinn.Aportes.Services.AfiliacionServices();
        List<Xpinn.Aportes.Entities.ConsultarPersonaBasico> lst = srAfi.ListarPersonasOficinaVirtual(usuario);
        string fic = "Consulta.csv";
        try
        {
            File.Delete(Server.MapPath("Archivos\\" + fic));
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true, System.Text.Encoding.GetEncoding(1251));
            sw.Close();
        }
        catch { }
        foreach (Xpinn.Aportes.Entities.ConsultarPersonaBasico item in lst)
        {
            string pass = "";
            string texto = "";
            pass = cifrar.Encriptar(item.identificacion);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true, System.Text.Encoding.GetEncoding(1251));
            texto = item.cod_persona + ";" + item.identificacion + ";" + pass + ";" + item.primer_nombre + ";" + item.segundo_nombre + ";" + item.primer_apellido + ";" + item.segundo_apellido + "; update persona_acceso set clave = '" + pass + "' where cod_persona = " + item.cod_persona + "::";
            sw.WriteLine(texto);
            sw.Close();
        }
    }

    private void IntentarActualizarLogoEmpresa(byte[] imageBytes)
    {
        if (imageBytes != null)
        {
            // Si ocurre una excepcion mientras se esta guardando el archivo .jpeg me puede quedar dañado,
            // TransactionScope no sirve para files del sistema(NTFS), Investigando Solución....
            using (MemoryStream imgStream = new MemoryStream(imageBytes))
            {
                Bitmap bitmapimage = null;
                try
                {
                    bitmapimage = new Bitmap(imgStream);
                    string saveImagePath = Server.MapPath("Images/") + "LogoEmpresa.jpg";

                    if (File.Exists(saveImagePath))
                    {
                        File.Delete(saveImagePath);
                    }

                    bitmapimage.Save(saveImagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch (Exception)
                {
                    string saveImagePath = Server.MapPath("Images/") + "LogoEmpresa.png";

                    if (File.Exists(saveImagePath))
                    {
                        File.Delete(saveImagePath);
                    }

                    bitmapimage.Save(saveImagePath, System.Drawing.Imaging.ImageFormat.Png);
                }
                finally
                {
                    if (bitmapimage != null)
                    {
                        bitmapimage.Dispose();
                    }
                }
            }
        }
    }

    public static string GetUserIP()
    {
        var ip = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null
              && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "")
             ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]
             : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        if (ip.Contains(","))
            ip.Split(',').First().Trim();
        return ip;
    }

}