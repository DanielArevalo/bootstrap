using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Services;

partial class Lista : GlobalWeb
{
    PerfilRiesgoServices _PerfilRiesgo = new PerfilRiesgoServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_PerfilRiesgo.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            //toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_PerfilRiesgo.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, _PerfilRiesgo.CodigoPrograma);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_PerfilRiesgo.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, _PerfilRiesgo.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, _PerfilRiesgo.CodigoPrograma);
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[_PerfilRiesgo.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[1].Text;
        Session[_PerfilRiesgo.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
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
            BOexcepcion.Throw(_PerfilRiesgo.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<PerfilRiesgo> lstConsulta = _PerfilRiesgo.ListarPerfilRiesgo(ObtenerValores(), Usuario);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }

            Session.Add(_PerfilRiesgo.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_PerfilRiesgo.CodigoPrograma, "Actualizar", ex);
        }
    }

    PerfilRiesgo ObtenerValores()
    {
        PerfilRiesgo vPerfilRiesgo = new PerfilRiesgo();

        if (txtCodigoPer.Text.Trim() != "")
            vPerfilRiesgo.Cod_perfil = Convert.ToInt64(txtCodigoPer.Text.Trim());
        if (txtDescripcion.Text.Trim() != "")
            vPerfilRiesgo.Descripcion = Convert.ToString(txtDescripcion.Text.Trim());
        if (txtValoracion.SelectedValue != "")
            vPerfilRiesgo.valoracion = Convert.ToString(txtValoracion.SelectedValue);
        if (ddlTipoPersona.SelectedValue != "")
            vPerfilRiesgo.tipoPersona = Convert.ToString(ddlTipoPersona.SelectedValue);

        return vPerfilRiesgo;
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int32 id = Convert.ToInt32(e.Keys[0]);

            Session["ID"] = id;
            ctlMensaje.MostrarMensaje("Desea eliminar el registro seleccionado?");
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["ID"].ToString() != "")
            {
                PerfilRiesgo pPerfilRiesgo = new PerfilRiesgo();
                pPerfilRiesgo.Cod_perfil = Convert.ToInt64(Session["ID"]);
                _PerfilRiesgo.EliminarPerfilRiesgo(pPerfilRiesgo, Usuario);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_PerfilRiesgo.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }


}