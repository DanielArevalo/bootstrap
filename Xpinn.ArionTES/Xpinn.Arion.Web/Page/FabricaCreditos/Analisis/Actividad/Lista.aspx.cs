using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

partial class Lista : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.ActividadesServices ActividadServicio = new Xpinn.FabricaCreditos.Services.ActividadesServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ActividadServicio.CodigoProgramaActividadEconomica, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActividadServicio.CodigoProgramaActividadEconomica, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
             if (!IsPostBack)
            {
               Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActividadServicio.CodigoProgramaActividadEconomica, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove(ActividadServicio.CodigoProgramaActividadEconomica + ".id");
        GuardarValoresConsulta(pConsulta, ActividadServicio.CodigoProgramaActividadEconomica);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, ActividadServicio.CodigoProgramaActividadEconomica);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ActividadServicio.CodigoProgramaActividadEconomica);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActividadServicio.CodigoProgramaActividadEconomica + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[ActividadServicio.CodigoProgramaActividadEconomica + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[ActividadServicio.CodigoProgramaActividadEconomica + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            try
            {
                ActividadServicio.EliminarActividadEconomica(id.ToString(), (Usuario)Session["usuario"]);
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActividadServicio.CodigoProgramaActividadEconomica, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(ActividadServicio.CodigoProgramaActividadEconomica, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.Actividades> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Actividades>();
            lstConsulta = ActividadServicio.ConsultarActividadEconomica(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
             //  ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(ActividadServicio.CodigoProgramaActividadEconomica + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActividadServicio.CodigoProgramaActividadEconomica, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.Actividades ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.Actividades vActividad = new Xpinn.FabricaCreditos.Entities.Actividades();

        if (txtCodigo.Text.Trim() != "")
            vActividad.codactividad = Convert.ToString(txtCodigo.Text.Trim());
        if (txtDescripcion.Text.Trim() != "")
            vActividad.descripcion = Convert.ToString(txtDescripcion.Text.Trim());

        return vActividad;
    }

}