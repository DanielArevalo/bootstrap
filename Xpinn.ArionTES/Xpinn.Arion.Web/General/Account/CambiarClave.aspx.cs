using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class General_Account_CambiarClave : System.Web.UI.Page
{
    private Xpinn.Seguridad.Services.PerfilService PerfilServicio = new Xpinn.Seguridad.Services.PerfilService();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["mensaje"] != null)
        {
            if (Session["mensaje"] != "")
            {
                lblInfo.Text = Session["mensaje"].ToString();
                lblInfo.Visible = true;
            }
        }

        bool? esSuperUsuario = Session["esSuperUsuario"] as bool?;
        cargarRestriccion();
        pnlVolver.Visible = esSuperUsuario.HasValue && esSuperUsuario.Value == true;
    }

    protected void cargarRestriccion()
    {
        Xpinn.Seguridad.Entities.Perfil vRestriccion = new Xpinn.Seguridad.Entities.Perfil();
        Usuario Usuario = (Usuario)Session["usuario"];
        vRestriccion = PerfilServicio.ConsultarPerfil(Usuario.codperfil, Usuario);
        txtMayuscula.Text = Convert.ToString(vRestriccion.mayuscula);
        txtCaracter.Text = Convert.ToString(vRestriccion.caracter);
        txtNumero.Text = Convert.ToString(vRestriccion.numero);
        txtLongitud.Text = vRestriccion.longitud == 0 ? "6" : Convert.ToString(vRestriccion.longitud);
    }

    protected void btnCambiarClave_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();

            if (CurrentPassword.Text != NewPassword.Text)
            {
                usuarioServicio.CambiarClave(CurrentPassword.Text.Trim(), ConfirmNewPassword.Text.Trim(), (Usuario)Session["usuario"]);

                lblInfo.ForeColor = System.Drawing.Color.White;
                lblInfo.Text = "La contrase&ntilde;a fue cambiada exitosamente.";
                lblInfo.Visible = true;
            }
            else
            {
                lblInfo.Visible = true;
                lblInfo.Text = "No se puede registrar la misma clave";
            }

        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            lblInfo.Text = ex.Message;
            lblInfo.Visible = true;
        }
        catch (Exception ex)
        {
            throw new Exception("CambiarClave.btnCambiarClave_Click" + ex.Message);
        }
    }

    protected void hlkModulos_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/General/Global/modulos.aspx");
    }
}