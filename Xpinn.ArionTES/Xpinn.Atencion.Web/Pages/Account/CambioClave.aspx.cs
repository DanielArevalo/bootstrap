using System;

public partial class CambioClave : GlobalWeb
{
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient usuarioServicio = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ValidarSession();
            AdicionarTitulo("Cambiar Contraseña", "");
            Site toolBar = (Site)Master;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("CambioClave", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
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

    protected void btnCambiarClave_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            xpinnWSLogin.Persona1 pPersona = new xpinnWSLogin.Persona1();
            pPersona = (xpinnWSLogin.Persona1)Session["persona"];
            if (pPersona.clavesinecriptar != txtPassword.Text)
            {
                VerError("La clave anterior digitada, no coincide con la del usuario. Verifique por favor.");
                return;
            }
            if (txtPasswordNew.Text.Length < 6)
            {
                VerError("La clave ingresada debe tener un mínimo de 6 caracteres.");
                return;
            }
            if (txtPassword.Text != txtPasswordNew.Text)
            {
                usuarioServicio.CambiarClavePersona(pPersona.cod_persona, txtPassword.Text.Trim(), txtPasswordNew.Text.Trim(), Session["sec"].ToString());
                pPersona.clavesinecriptar = txtPasswordNew.Text.Trim();
                Session["persona"] = pPersona; 
                mvPrincipal.ActiveViewIndex = 1;
                
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Index.aspx");
    }
}