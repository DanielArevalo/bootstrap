using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Web.Services;
using System.Web.Script.Services;
using System.Text;
using System.Web.Script.Serialization;
using Xpinn.Util;
using System.Configuration;
using System.Net.Sockets;
using System.IO;




public partial class Login : System.Web.UI.Page
{
    xpinnWSLogin.WSloginSoapClient Acceso = new xpinnWSLogin.WSloginSoapClient();
    bool loginExterno = false;    
    
    //inicio oficina
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            buildSecure();
        }
    }

    [WebMethod]
    public static string LoginFinancial(string pIdentificacion, string pClave)
    {
        xpinnWSLogin.WSloginSoapClient Acceso = new xpinnWSLogin.WSloginSoapClient();
        bool rpta = false;
        try
        {
            if (pIdentificacion != null && pClave != null)
            {
                //Validando si la persona existe
                bool result = false;
                result = Acceso.Login(pIdentificacion, pClave);
                rpta = result ? true : false;
            }
        }
        catch 
        {
        }
        
        return rpta.ToString();
    }
    
    protected void btnIniciar_Click(object sender, EventArgs e)
    {        
        lblMsj.Text = "";
        lblMsj.Visible = false;
        if (login__username.Text.Trim() == "")
        {
            lblMsj.Text = "Debe Ingresar una identificación, verifique los datos.";
            lblMsj.Visible = true;
            login__username.Focus();
            return;
        }
        if (login__password.Text.Trim() == "")
        {
            lblMsj.Text = "Debe Ingresar una contraseña, verifique los datos.";
            lblMsj.Visible = true;
            login__password.Focus();
            return;
        }
        Logeo(login__username.Text, login__password.Text);
    }

    protected void Logeo(string pIdentificacion, string pClave, bool ext = false)
    {
        try
        {            
            string direccionIP = ""; ;
            if (direccionIP == "::1")
                direccionIP = GetUserIP();
            xpinnWSLogin.Persona1 DataPersona = new xpinnWSLogin.Persona1();
            if(Session["sec"] != null)
            {                
                DataPersona = Acceso.LoginApp(pIdentificacion, pClave, direccionIP, Session["sec"] as string);
            }

            if (DataPersona != null && DataPersona.rptaingreso == true && DataPersona.identificacion != null && DataPersona.cod_persona != 0)
            {
                if (DataPersona.estado == "R" || !Convert.ToBoolean(DataPersona.acceso_oficina))
                {
                    lblMsj.Text = "Usted no se encuentra con estado activo, por favor comuníquese con nosotros para mayor información";
                    lblMsj.Visible = true;
                }
                else
                {
                    direccionIP = Request.UserHostAddress;
                    DataPersona.ipPersona = direccionIP;
                    Session["persona"] = DataPersona;
                    Session["COD_INGRESO"] = DataPersona.codigo;
                    List<xpinnWSLogin.Acceso> lstAccesos = Acceso.ListarAccesoAAC(24, DataPersona.identificacion, DataPersona.clavesinecriptar, Session["sec"].ToString());
                    if (lstAccesos != null)
                        Session["Procesos"] = lstAccesos;

                    //VALIDAR ACTUALIZACIÓN DE DATOS            
                    if (string.IsNullOrEmpty(DataPersona.Observaciones))
                    {
                        Response.Redirect("~/Index.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("~/Pages/Asociado/ActualizarDatos/ActualizacionCompleta.aspx", false);
                    }
                }
            }
            else
            {
                lblMsj.Text = "Usuario y/o Clave Invalida";
                lblMsj.Visible = true;
            }
        }
        catch (Exception ex)
        {            
            if (ex.Message.Contains("El registro no existe. Verifique por favor"))
            {
                lblMsj.Text = "Usuario y/o Clave Invalida";
            }
            else
            {
                lblMsj.Text = "Error al validar el usuario, " + ex.Message;                
            }
            lblMsj.Visible = true;
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
   
    protected bool buildSecure()
    {
        string key =  ConfigurationManager.AppSettings["key"].ToString();
        string usr = ConfigurationManager.AppSettings["usr"].ToString();
        string ip = "";
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress address in ipHostInfo.AddressList)
        {
            if (address.AddressFamily == AddressFamily.InterNetwork)
                ip = address.ToString();
        }          
        if (!string.IsNullOrEmpty(key) || !string.IsNullOrEmpty(usr) || !string.IsNullOrEmpty(ip))
        {
            CifradoBusiness cifrar = new CifradoBusiness();            
            string sec = ip+";"+usr+";"+key;
            sec = cifrar.Encriptar(sec);
            Session["sec"] = sec;
        }
        
        return true;
    }

}