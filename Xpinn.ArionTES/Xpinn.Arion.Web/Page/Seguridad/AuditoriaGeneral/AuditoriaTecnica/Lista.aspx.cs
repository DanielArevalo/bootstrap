using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Seguridad.Entities;
using Xpinn.Seguridad.Services;
using Xpinn.Util;

partial class Lista : GlobalWeb
{
    AuditoriaStoredProceduresService _auditoriaService = new AuditoriaStoredProceduresService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_auditoriaService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_auditoriaService.CodigoPrograma, "Page_PreInit", ex);
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
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_auditoriaService.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        ddlUsuarios.SelectedIndex = 0;
        txtNombreUsuario.Text = string.Empty;
        ddlOpciones.SelectedIndex = 0;
        txtFecha.Text = string.Empty;
        txtFechaFinal.Text = string.Empty;
        txtProcedimiento.Text = string.Empty;
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[_auditoriaService.CodigoPrograma + ".id"] = id;
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
            BOexcepcion.Throw(_auditoriaService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<AuditoriaStoredProcedures> lstConsulta = _auditoriaService.ListarAuditoriaStoredProcedures(ObtenerFiltro(), Usuario);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_auditoriaService.CodigoPrograma, "Actualizar", ex);
        }
    }

    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(ddlUsuarios.SelectedValue) && !string.IsNullOrWhiteSpace(txtNombreUsuario.Text))
        {
            VerError("");
            if (ddlUsuarios.SelectedItem.ToString() != txtNombreUsuario.Text.Trim())
            {
                VerError("El código y el nombre de usuario ingresados no coinciden");
                return "";
            }
            else
                filtro += " and aud.codigousuario = " + ddlUsuarios.SelectedValue + "and UPPER(aud.nombreusuario) LIKE '%" + txtNombreUsuario.Text.ToUpper() + "%' ";
        }
        else if (!string.IsNullOrWhiteSpace(ddlUsuarios.SelectedValue))
        {
            filtro += " and aud.codigousuario = " + ddlUsuarios.SelectedValue;
        }
        else if (!string.IsNullOrWhiteSpace(txtNombreUsuario.Text))
            filtro += " and UPPER(aud.nombreusuario) LIKE '%" + txtNombreUsuario.Text.ToUpper() + "%' ";
        if (!string.IsNullOrWhiteSpace(ddlOpciones.SelectedValue))
            filtro += " and aud.CODIGOOPCION = " + ddlOpciones.SelectedValue;
        if (!string.IsNullOrWhiteSpace(txtProcedimiento.Text))
            filtro += " and UPPER(aud.nombresp) LIKE '%" + txtProcedimiento.Text.ToUpper() + "%' ";

        if (!string.IsNullOrWhiteSpace(txtFecha.Text) && !string.IsNullOrWhiteSpace(txtFechaFinal.Text))
        {
            filtro += " and TRUNC(aud.fechaejecucion) BETWEEN to_Date('" + txtFecha.Text + "', 'dd/MM/yyyy') and to_Date('" + txtFechaFinal.Text + "', 'dd/MM/yyyy') ";
        }
        else if (!string.IsNullOrWhiteSpace(txtFecha.Text))
        {
            filtro += " and TRUNC(aud.fechaejecucion) >= to_Date('" + txtFecha.Text + "', 'dd/MM/yyyy') ";
        }
        else if (!string.IsNullOrWhiteSpace(txtFechaFinal.Text))
        {
            filtro += " and TRUNC(aud.fechaejecucion) <= to_Date('" + txtFechaFinal.Text + "', 'dd/MM/yyyy') ";
        }

        StringHelper stringHelper = new StringHelper();
        filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);

        return filtro;
    }

    void CargarOpcion()
    {
        try
        {
            OpcionService opcionServicio = new OpcionService();
            List<Opcion> lstOpcion = opcionServicio.ListarOpcion(null, (Usuario)Session["usuario"]);

            ddlOpciones.DataSource = lstOpcion;
            ddlOpciones.DataTextField = "nombre";
            ddlOpciones.DataValueField = "cod_opcion";
            ddlOpciones.DataBind();

            ddlOpciones.Items.Insert(0, " ");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_auditoriaService.CodigoPrograma, "CargarOpcion", ex);
        }
    }

    void CargarUsuarios()
    {
        try
        {
            UsuarioService opcionServicio = new UsuarioService();
            List<Usuario> lstUsuario = opcionServicio.ListarUsuario(null, Usuario);

            ddlUsuarios.DataSource = lstUsuario;
            ddlUsuarios.DataTextField = "nombre";
            ddlUsuarios.DataValueField = "codusuario";
            ddlUsuarios.DataBind();

            ddlUsuarios.Items.Insert(0, "");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_auditoriaService.CodigoPrograma, "CargarUsuarios", ex);
        }
    }

    [WebMethod]
    public static string[] GetProcedimientos(string prefix)
    {
        AuditoriaStoredProceduresService auditoriaService = new AuditoriaStoredProceduresService();

        List<string> listaProcedimientos = auditoriaService.ListarProcedimientos(prefix, new Usuario());

        return listaProcedimientos.ToArray();
    }
}