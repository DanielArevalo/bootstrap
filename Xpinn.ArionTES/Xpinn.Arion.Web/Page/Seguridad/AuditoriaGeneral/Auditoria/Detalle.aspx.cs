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
    private Xpinn.Seguridad.Services.AuditoriaService AuditoriaServicio = new Xpinn.Seguridad.Services.AuditoriaService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AuditoriaServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AuditoriaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarOpcion();
                CargarUsuarios();
                AsignarEventoConfirmar();
                if (Session[AuditoriaServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[AuditoriaServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(AuditoriaServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(AuditoriaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Seguridad.Entities.Auditoria vAuditoria = new Xpinn.Seguridad.Entities.Auditoria();
            vAuditoria = AuditoriaServicio.ConsultarAuditoria(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vAuditoria.cod_auditoria != Int64.MinValue)
                txtCod_auditoria.Text = vAuditoria.cod_auditoria.ToString().Trim();
            if (vAuditoria.codusuario != Int64.MinValue)
                txtCodusuario.Text = vAuditoria.codusuario.ToString().Trim();
            if (vAuditoria.codopcion != Int64.MinValue)
                txtCodopcion.Text = vAuditoria.codopcion.ToString().Trim();
            if (vAuditoria.fecha != DateTime.MinValue)
                txtFecha.Text = vAuditoria.fecha.ToShortDateString();
            if (!string.IsNullOrEmpty(vAuditoria.ip))
                txtIp.Text = vAuditoria.ip.ToString().Trim();
            if (!string.IsNullOrEmpty(vAuditoria.navegador))
                txtNavegador.Text = vAuditoria.navegador.ToString().Trim();
            if (!string.IsNullOrEmpty(vAuditoria.accion))
                txtAccion.Text = vAuditoria.accion.ToString().Trim();
            if (!string.IsNullOrEmpty(vAuditoria.tabla))
                txtTabla.Text = vAuditoria.tabla.ToString().Trim();
            if (!string.IsNullOrEmpty(vAuditoria.detalle))
                txtDetalle.Text = OrdenarDatos(vAuditoria.detalle.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AuditoriaServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }

    private void CargarOpcion()
    {
        try
        {
            Xpinn.Seguridad.Services.OpcionService opcionServicio = new Xpinn.Seguridad.Services.OpcionService();
            List<Xpinn.Seguridad.Entities.Opcion> lstOpcion = new List<Xpinn.Seguridad.Entities.Opcion>();

            lstOpcion = opcionServicio.ListarOpcion(null, (Usuario)Session["usuario"]);

            txtCodopcion.DataSource = lstOpcion;
            txtCodopcion.DataTextField = "nombre";
            txtCodopcion.DataValueField = "cod_opcion";
            txtCodopcion.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AuditoriaServicio.CodigoPrograma, "CargarOpcion", ex);
        }
    }

    private void CargarUsuarios()
    {
        try
        {
            Xpinn.Seguridad.Services.UsuarioService opcionServicio = new Xpinn.Seguridad.Services.UsuarioService();
            List<Xpinn.Util.Usuario> lstUsuario = new List<Xpinn.Util.Usuario>();

            lstUsuario = opcionServicio.ListarUsuario(null, (Usuario)Session["usuario"]);

            txtCodusuario.DataSource = lstUsuario;
            txtCodusuario.DataTextField = "nombre";
            txtCodusuario.DataValueField = "codusuario";
            txtCodusuario.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AuditoriaServicio.CodigoPrograma, "CargarUsuarios", ex);
        }
    }

    private string OrdenarDatos(string pDetalle)
    {
        try
        {
            string detail = "";
            string[] str = pDetalle.Split('|');

            for (int c = 0; c < str.Length; c++)
            {
                detail += str[c] + "\n";
            }

            return detail;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AuditoriaServicio.CodigoPrograma, "OrdenarDatos", ex);
            return "";
        }
    }
}