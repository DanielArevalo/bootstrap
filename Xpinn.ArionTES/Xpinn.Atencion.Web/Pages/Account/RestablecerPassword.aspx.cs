using System;
using System.Web.UI;
using System.Reflection;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using Xpinn.Util;

public partial class RestablecerPassword : System.Web.UI.Page
{
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient usuarioServicio = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            buildSecure();
            lblError.Text = "";
            lblError.Visible = false;
            
            //capturamos la variable QueryString
            if (Request.QueryString["cod_persona"] != null)
            {
                lblcod_persona.Text = Request.QueryString["cod_persona"].ToString().Trim();
                if (lblcod_persona.Text == "")
                {
                    lblError.Text = "Se produjo un error al ingresar, por su seguridad vuelva a realizar el proceso.";
                    lblError.Visible = true;
                    return;
                }
                //Pendiente
                //Eliminando la Variable del URL
                PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // hacemos la colección editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // eliminamos
                this.Request.QueryString.Remove("cod_persona");
            }
            cargarRestriccion();
        }
    }

    protected void cargarRestriccion()
    {
        xpinnWSLogin.WSloginSoapClient Logservicio = new xpinnWSLogin.WSloginSoapClient();
        xpinnWSLogin.Perfil vRestriccion = new xpinnWSLogin.Perfil();
        vRestriccion = Logservicio.consultarPerfil(Session["sec"].ToString());
        txtMayuscula.Text = Convert.ToString(vRestriccion.mayuscula);
        txtCaracter.Text = Convert.ToString(vRestriccion.caracter);
        txtNumero.Text = Convert.ToString(vRestriccion.numero);
        txtLongitud.Text = vRestriccion.longitud == 0 ? "6" : Convert.ToString(vRestriccion.longitud);
    }

    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        lblError.Visible = false;
        try
        {
            if (txtPasswordNew.Text != "" && lblcod_persona.Text != "")
            {
                usuarioServicio.CambiarClavePersona(Convert.ToInt64(lblcod_persona.Text), null, txtPasswordNew.Text.Trim(), Session["sec"].ToString());
                mvPrincipal.ActiveViewIndex = 1;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }
    }

    protected void btnIngresar_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        lblError.Visible = false;
        try
        {
            Response.Redirect("~/Default.aspx");
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }
    }

    protected bool buildSecure()
    {
        string key = ConfigurationManager.AppSettings["key"].ToString();
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
            string sec = ip + ";" + usr + ";" + key;
            sec = cifrar.Encriptar(sec);
            Session["sec"] = sec;
        }                
        return true;
    }
}