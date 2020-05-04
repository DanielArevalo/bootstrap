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

partial class Lista : GlobalWeb
{
    private Xpinn.Seguridad.Services.AuditoriaService AuditoriaServicio = new Xpinn.Seguridad.Services.AuditoriaService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AuditoriaServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
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
                CargarValoresConsulta(pConsulta, AuditoriaServicio.CodigoPrograma);
                if (Session[AuditoriaServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AuditoriaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, AuditoriaServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, AuditoriaServicio.CodigoPrograma);
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[AuditoriaServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
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
            BOexcepcion.Throw(AuditoriaServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Seguridad.Entities.Auditoria> lstConsulta = new List<Xpinn.Seguridad.Entities.Auditoria>();
            lstConsulta = AuditoriaServicio.ListarAuditoria(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
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

            Session.Add(AuditoriaServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AuditoriaServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.Seguridad.Entities.Auditoria ObtenerValores()
    {
        Xpinn.Seguridad.Entities.Auditoria vAuditoria = new Xpinn.Seguridad.Entities.Auditoria();

    if(txtCodusuario.Text.Trim() != "")
        vAuditoria.codusuario = Convert.ToInt64(txtCodusuario.Text.Trim());
    if(txtCodopcion.Text.Trim() != "")
        vAuditoria.codopcion = Convert.ToInt64(txtCodopcion.Text.Trim());
    if(txtFecha.Text.Trim() != "")
        vAuditoria.fecha = Convert.ToDateTime(txtFecha.Text.Trim());
    if(txtAccion.SelectedItem != null)
        vAuditoria.accion = txtAccion.Text.Trim() == "Todos" ? null : Convert.ToString(txtAccion.SelectedItem);

        return vAuditoria;
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

            txtCodopcion.Items.Insert(0, "");
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

            txtCodusuario.Items.Insert(0, "");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AuditoriaServicio.CodigoPrograma, "CargarUsuarios", ex);
        }
    }
}