using System;
using System.Configuration;
using System.Web.UI;
using Xpinn.Util;

public partial class General_Global_error : Page
{
    Exception _error;
    string _url;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["errorAplicacionLogear"] != null)
        {
            _error = (Exception)Session["errorAplicacionLogear"];
        }
        if (Session["paginaAnterior"] != null)
        {
            _url = (string)Session["paginaAnterior"];
        }
    }

    protected void btnVolver_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(_url);
    }


    protected void EnviarError_Click(object sender, EventArgs e)
    {
        if (_error == null)
        {
            EnviarCorreoError.Text = "No se puede determinar el error!.";
            EnviarCorreoError.Enabled = false;
            return;
        }

        LlenarFormularioError();

        string correo = ConfigurationManager.AppSettings["correoExcepciones"];
        string clave = ConfigurationManager.AppSettings["claveCorreoExcepciones"];

        CorreoHelper correoHelper = new CorreoHelper(correo, correo, clave);
        correoHelper.EnviarHTMLDeControlPorCorreo(pnlError, Correo.Gmail, "Reporte Excepciones", "Expinn");

        EnviarCorreoError.Text = "Gracias!.";
        EnviarCorreoError.Enabled = false;
    }

    protected void VerFormulario_Click(object sender, EventArgs e)
    {
        LlenarFormularioError();

        if (pnlError.Visible == false)
        {
            pnlError.Visible = true;
            VerFormulario.Text = "Ocultar Error";
        }
        else
        {
            pnlError.Visible = false;
            VerFormulario.Text = "Ver Error";
        }     
    }


    private void LlenarFormularioError()
    {
        if (Session["usuario"] != null)
        {
            txtNombreEmpresa.Text = ((Usuario)Session["usuario"]).empresa;
        }
        else
        {
            txtNombreEmpresa.Text = "La Sesion de usuario no existe, por lo que no se puede obtener nombre de la empresa";
        }

        if (!string.IsNullOrWhiteSpace(_url))
        {
            txtUrlError.Text = (string)Session["paginaAnterior"];
        }
        else
        {
            txtUrlError.Text = "URL de error no disponible, ver stack trace";
        }

        txtDescripcion.Text = _error.Message;
        txtDetalleExcepcion.Text = _error.Source;
        txtStackTrace.Text = _error.StackTrace;
    }
}