using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

partial class Detalle : GlobalWeb
{
    private Xpinn.Seguridad.Services.UsuarioService UsuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(UsuarioServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(UsuarioServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarPerfil();
                CargarDllOficinas();
                AsignarEventoConfirmar();
                if (Session[UsuarioServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[UsuarioServicio.CodigoPrograma + ".id"].ToString();
                    //Session.Remove(UsuarioServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(UsuarioServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove(UsuarioServicio.CodigoPrograma + ".id");
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove(UsuarioServicio.CodigoPrograma + ".id");
        Navegar(Pagina.Lista);
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            UsuarioServicio.EliminarUsuario(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(UsuarioServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[UsuarioServicio.CodigoPrograma + ".id"] = idObjeto;
        Navegar(Pagina.Nuevo);
    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Util.Usuario vUsuario = new Xpinn.Util.Usuario();
            vUsuario = UsuarioServicio.ConsultarUsuario(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vUsuario.identificacion))
                txtIdentificacion.Text = vUsuario.identificacion.ToString().Trim();
            if (!string.IsNullOrEmpty(vUsuario.nombre))
                txtNombre.Text = vUsuario.nombre.ToString().Trim();
            if (!string.IsNullOrEmpty(vUsuario.direccion))
                txtDireccion.Text = vUsuario.direccion.ToString().Trim();
            if (!string.IsNullOrEmpty(vUsuario.telefono))
                txtTelefono.Text = vUsuario.telefono.ToString().Trim();
            if (vUsuario.fechacreacion != DateTime.MinValue)
                txtFechacreacion.Text = vUsuario.fechacreacion.ToShortDateString();
            if (vUsuario.estado != Int64.MinValue)
                txtEstado.Text = vUsuario.estado.ToString().Trim();
            if (vUsuario.codperfil != Int64.MinValue)
                txtCodperfil.Text = vUsuario.codperfil.ToString().Trim();
            if (vUsuario.cod_oficina != Int64.MinValue)
                txtCod_oficina.SelectedValue = vUsuario.cod_oficina.ToString().Trim();            
            if (!string.IsNullOrEmpty(vUsuario.documento))
                txtIdentdoc.Text = HttpUtility.HtmlDecode(vUsuario.documento.ToString().Trim());
            if (!string.IsNullOrEmpty(vUsuario.cod_persona.ToString()))
                txtCod_persona.Text = HttpUtility.HtmlDecode(vUsuario.cod_persona.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(UsuarioServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }

    private void CargarPerfil()
    {
        try
        {
            Xpinn.Seguridad.Services.PerfilService perfilServicio = new Xpinn.Seguridad.Services.PerfilService();
            List<Xpinn.Seguridad.Entities.Perfil> lstPerfil = new List<Xpinn.Seguridad.Entities.Perfil>();

            lstPerfil = perfilServicio.ListarPerfil(null, (Usuario)Session["usuario"]);

            txtCodperfil.DataSource = lstPerfil;
            txtCodperfil.DataTextField = "nombreperfil";
            txtCodperfil.DataValueField = "codperfil";
            txtCodperfil.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(UsuarioServicio.CodigoPrograma, "CargarPerfil", ex);
        }
    }

    private void CargarDllOficinas()
    {
        List<Xpinn.Caja.Entities.Oficina> lstoficina = new List<Xpinn.Caja.Entities.Oficina>();
        Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();
        Xpinn.Caja.Services.OficinaService oficinaservicio = new Xpinn.Caja.Services.OficinaService();
        lstoficina = oficinaservicio.ListarOficina(oficina, (Usuario)Session["usuario"]);
        txtCod_oficina.DataTextField = "nombre";
        txtCod_oficina.DataValueField = "cod_oficina";
        txtCod_oficina.DataSource = lstoficina;
        txtCod_oficina.DataBind();
    }

}