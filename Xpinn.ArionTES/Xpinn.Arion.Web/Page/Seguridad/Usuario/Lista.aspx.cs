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
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;

partial class Lista : GlobalWeb
{
    private Xpinn.Seguridad.Services.UsuarioService UsuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(UsuarioServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
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
                CargarValoresConsulta(pConsulta, UsuarioServicio.CodigoPrograma);
                if (Session[UsuarioServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(UsuarioServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, UsuarioServicio.CodigoPrograma);
        Session.Remove(UsuarioServicio.CodigoPrograma + ".id");
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, UsuarioServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, UsuarioServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(UsuarioServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[UsuarioServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[UsuarioServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            UsuarioServicio.EliminarUsuario(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(UsuarioServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(UsuarioServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Util.Usuario> lstConsulta = new List<Xpinn.Util.Usuario>();
            lstConsulta = UsuarioServicio.ListarUsuario(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                //foreach (Usuario item in lstConsulta)
                //{
                //    CifradoBusiness cifrar = new CifradoBusiness();
                //    item.clave_sinencriptar = cifrar.Desencriptar(item.clave);
                //}
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(UsuarioServicio.CodigoPrograma + ".consulta", 1);
            Session["DTUSUARIOS"] = lstConsulta;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(UsuarioServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.Util.Usuario ObtenerValores()
    {
        Xpinn.Util.Usuario vUsuario = new Xpinn.Util.Usuario();

        if (txtIdentificacion.Text.Trim() != "")
            vUsuario.identificacion = Convert.ToString(txtIdentificacion.Text.Trim());
        if (txtNombre.Text.Trim() != "")
            vUsuario.nombre = Convert.ToString(txtNombre.Text.Trim());
        if (txtEstado.Text.Trim() != "")
            vUsuario.estado = Convert.ToInt64(txtEstado.Text.Trim());
        if (txtCodperfil.Text.Trim() != "")
            vUsuario.codperfil = Convert.ToInt64(txtCodperfil.Text.Trim());
        
        return vUsuario;
    }

    private void CargarPerfil()
    {
        try
        {
            Xpinn.Seguridad.Services.PerfilService perfilServicio = new Xpinn.Seguridad.Services.PerfilService();
            List<Xpinn.Seguridad.Entities.Perfil> lstPerfil = new List<Xpinn.Seguridad.Entities.Perfil>();
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["usuario"];

            lstPerfil = perfilServicio.ListarPerfil(null, pUsuario);

            if (pUsuario.codperfil != 1)
                lstPerfil.Remove(lstPerfil.Where(x => x.codperfil == 1).FirstOrDefault());

            txtCodperfil.DataSource = lstPerfil;
            txtCodperfil.DataTextField = "nombreperfil";
            txtCodperfil.DataValueField = "codperfil";
            txtCodperfil.DataBind();

            txtCodperfil.Items.Insert(0, new ListItem("Todos",""));
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(UsuarioServicio.CodigoPrograma, "CargarPerfil", ex);
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTUSUARIOS"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.DataSource = Session["DTUSUARIOS"];
                gvLista.DataBind();
                gvLista.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvLista);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=Usuarios.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
                gvLista.AllowPaging = true;
                gvLista.DataBind();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch
        {
        }
    }


}