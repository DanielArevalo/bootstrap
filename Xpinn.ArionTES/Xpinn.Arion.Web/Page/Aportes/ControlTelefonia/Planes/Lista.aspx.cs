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
    private Xpinn.Aportes.Services.PlanesTelefonicosService planesService = new Xpinn.Aportes.Services.PlanesTelefonicosService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(planesService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(planesService.CodigoPrograma, "Page_PreInit", ex);
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, planesService.CodigoPrograma);
                if (Session[planesService.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(planesService.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove(planesService.CodigoPrograma + ".id");
        GuardarValoresConsulta(pConsulta, planesService.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, planesService.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, planesService.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(planesService.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[planesService.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[planesService.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            try
            {
                planesService.EliminarPlanTelefonico(id, (Usuario)Session["usuario"]);
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
            BOexcepcion.Throw(planesService.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(planesService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Aportes.Entities.PlanTelefonico> lstConsulta = new List<Xpinn.Aportes.Entities.PlanTelefonico>();
            lstConsulta = planesService.ListarPlanTelefonico(ObtenerValores(), (Usuario)Session["usuario"]);

             gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                ViewState["DTPlanesLin"] = lstConsulta;
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                ViewState["DTPlanesLin"] = null;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(planesService.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(planesService.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.Aportes.Entities.PlanTelefonico ObtenerValores()
    {
        Xpinn.Aportes.Entities.PlanTelefonico vPlanTele = new Xpinn.Aportes.Entities.PlanTelefonico();

        if (txtCodigo.Text.Trim() != "")
            vPlanTele.cod_plan = Convert.ToInt32(txtCodigo.Text.Trim());
        if (txtDescripcion.Text.Trim() != "")
            vPlanTele.nombre = Convert.ToString(txtDescripcion.Text.Trim());

        return vPlanTele;
    }

    private void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && ViewState["DTPlanesLin"] != null)
            {
                gvLista.AllowPaging = false;
                gvLista.DataSource = ViewState["DTPlanesLin"];
                gvLista.DataBind();
                ExportarGridCSVDirecto(gvLista, "PlanesLinea");
                gvLista.AllowPaging = true;
                gvLista.DataSource = ViewState["DTPlanesLin"];
                gvLista.DataBind();
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

}