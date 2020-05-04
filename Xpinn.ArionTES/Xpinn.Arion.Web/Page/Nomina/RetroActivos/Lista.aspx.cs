using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    RetroactivoService _retroactivoService = new RetroactivoService();


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_retroactivoService.CodigoPrograma.ToString(), "L");

            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoNuevo += (s, evt) =>
            {
                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_retroactivoService.CodigoPrograma + ".idRetroactivo");
                Session.Remove(_retroactivoService.CodigoPrograma + ".idEmpleado");
                Navegar(Pagina.Nuevo);
            };

            ctlMensaje.eventoClick += ctlMensaje_eventoClick;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_retroactivoService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_retroactivoService.CodigoPrograma + ".idRetroactivo");
                Session.Remove(_retroactivoService.CodigoPrograma + ".idEmpleado");

                InicializarPagina();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_retroactivoService.GetType().Name + "A", "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {
        
    }


    #endregion


    #region Eventos Botones


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtCodigoRetroactivo.Text = string.Empty;
        txtIdentificacion.Text = string.Empty;
        txtNombre.Text = string.Empty;
        ddlConcepto.SelectedIndex = 0;
    }

    void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        gvRetroactivos.AllowPaging = false;
        Actualizar();

        ExportarGridViewEnExcel(gvRetroactivos, "Retroactivos");

        gvRetroactivos.AllowPaging = true;
        Actualizar();
    }


    #endregion


    #region Eventos Grillas


    protected void gvRetroactivos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRetroactivos.PageIndex = e.NewPageIndex;
        Actualizar();
    }

    protected void gvRetroactivos_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(gvRetroactivos.SelectedRow.Cells[2].Text);
        long idEmpleado = Convert.ToInt64(gvRetroactivos.SelectedRow.Cells[3].Text);

        Session[_retroactivoService.CodigoPrograma + ".idRetroactivo"] = id;
        Session[_retroactivoService.CodigoPrograma + ".idEmpleado"] = idEmpleado;
        Navegar(Pagina.Detalle);
    }

    protected void OnRowCommandDeleting(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Delete") return;

        long idBorrar = Convert.ToInt64(gvRetroactivos.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

        ViewState.Add("idBorrar", idBorrar);

        ctlMensaje.MostrarMensaje("Seguro que deseas eliminar esta registro?");
    }

    void ctlMensaje_eventoClick(object sender, EventArgs e)
    {
        if (ViewState["idBorrar"] != null)
        {
            long idBorrar = Convert.ToInt64(ViewState["idBorrar"]);

            try
            {
                _retroactivoService.EliminarRetroactivo(idBorrar, Usuario);
                ViewState.Remove("idBorrar");
                Actualizar();
            }
            catch (Exception ex)
            {
                VerError("Error al borrar el registro, " + ex.Message);
            }
        }
    }

    protected void gvRetroactivos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }


    #endregion


    #region Metodos Ayuda


    public void Actualizar()
    {
        try
        {
            string filtro = ObtenerFiltro();

            List<Retroactivo> lstConsulta = _retroactivoService.ListarRetroactivo(filtro, Usuario);

            gvRetroactivos.DataSource = lstConsulta;
            gvRetroactivos.DataBind();
            lblNumeroRegistros.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_retroactivoService.CodigoPrograma, "Actualizar", ex);
        }
    }

    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(txtCodigoRetroactivo.Text))
        {
            filtro += " and ret.consecutivo = " + txtCodigoRetroactivo.Text.Trim();
        }

        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            filtro += " and per.identificacion LIKE '%" + txtIdentificacion.Text.Trim() + "%' ";
        }

        if (!string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            filtro += " and per.nombre LIKE '%" + txtNombre.Text.Trim().ToUpperInvariant() + "%' ";
        }

        if (!string.IsNullOrWhiteSpace(ddlConcepto.SelectedValue))
        {
            filtro += " and ret.CONCEPTOPAGORETROACTIVO = " + ddlConcepto.SelectedValue;
        }

        StringHelper stringHelper = new StringHelper();
        filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);

        return filtro;
    }


    #endregion


}
