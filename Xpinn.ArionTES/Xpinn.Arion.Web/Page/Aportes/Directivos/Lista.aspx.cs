using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Services;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    DirectivoService _directivoService = new DirectivoService();


    #region Eventos Iniciales

    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_directivoService.CodigoPrograma, "L");

            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlMensajeBorrar.eventoClick += CtlMensajeBorrar_eventoClick;
            toolBar.eventoNuevo += (s, evt) => {
                Session.Remove(_directivoService.CodigoPrograma + ".id");
                Navegar(Pagina.Nuevo);
            };
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_directivoService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InicializarPagina();
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.TipoDirectivo, ddlTipoDirectivo);
    }


    #endregion


    #region Eventos Intermedios GridView - Botonera


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLista.PageIndex = e.NewPageIndex;
        Actualizar();
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value);
        Session[_directivoService.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void OnRowCommandDeleting(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Delete") return;

        long idBorrar = Convert.ToInt64(gvLista.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

        ViewState.Add("idBorrar", idBorrar);

        ctlMensajeBorrar.MostrarMensaje("Seguro que deseas eliminar esta registro?");
    }

    void CtlMensajeBorrar_eventoClick(object sender, EventArgs e)
    {
        if (ViewState["idBorrar"] != null)
        {
            long idBorrar = Convert.ToInt64(ViewState["idBorrar"]);

            try
            {
                _directivoService.EliminarDirectivo(idBorrar, Usuario);
                Actualizar();
            }
            catch (Exception ex)
            {
                VerError("Error al borrar el registro, " + ex.Message);
            }
        }
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtIdentificacion.Text = string.Empty;
        ddlCalidad.SelectedIndex = 0;
        ddlEstado.SelectedIndex = 0;
        ddlTipoDirectivo.SelectedIndex = 0;
    }

    void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }


    #endregion


    #region Métodos Ayuda


    void Actualizar()
    {
        try
        {
            VerError("");
            string filtro = ObtenerFiltro();
            List<Directivo> lstDirectivo = _directivoService.ListarDirectivo(filtro, Usuario);

            if (lstDirectivo.Count > 0)
            {
                lblTotalRegs.Text = "Se encontraron " + lstDirectivo.Count + " registros!.";
            }
            else
            {
                lblTotalRegs.Text = "Su consulta no obtuvo ningún resultado!.";
            }

            gvLista.DataSource = lstDirectivo;
            gvLista.DataBind();
        }
        catch (Exception ex)
        {
            VerError("Error al actualizar la grilla, " + ex.Message);
        }
    }


    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            filtro += " and dir.IDENTIFICACION like '%" + txtIdentificacion.Text + "%'";
        }

        if (!string.IsNullOrWhiteSpace(ddlCalidad.SelectedValue))
        {
            filtro += " and dir.CALIDAD = " + ddlCalidad.SelectedValue;
        }

        if (!string.IsNullOrWhiteSpace(ddlEstado.SelectedValue))
        {
            filtro += " and dir.ESTADO = '" + ddlEstado.SelectedValue + "'";
        }

        if (!string.IsNullOrWhiteSpace(ddlTipoDirectivo.SelectedValue))
        {
            filtro += " and dir.TIPO_DIRECTIVO = " + ddlTipoDirectivo.SelectedValue;
        }

        if (!string.IsNullOrWhiteSpace(filtro))
        {
            StringHelper stringHelper = new StringHelper();
            filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);
        }

        return filtro;
    }


    #endregion


}