using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Presupuesto.Services;
using Xpinn.Presupuesto.Entities;

public partial class Lista : GlobalWeb
{
    Xpinn.Presupuesto.Services.EjecucionService EjecucionService = new Xpinn.Presupuesto.Services.EjecucionService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(EjecucionService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EjecucionService.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDDList();
                CargarValoresConsulta(pConsulta, EjecucionService.GetType().Name);
                if (Session[EjecucionService.GetType().Name + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EjecucionService.GetType().Name + "L", "Page_Load", ex);
        }
    }

    private void CargarDDList()
    {
        Xpinn.Presupuesto.Services.TipoPresupuestoService TipoPresupuestoService = new Xpinn.Presupuesto.Services.TipoPresupuestoService();
        Xpinn.Presupuesto.Entities.TipoPresupuesto TipoPresupuesto = new Xpinn.Presupuesto.Entities.TipoPresupuesto();
        ddlTipoPresupuesto.DataSource = TipoPresupuestoService.ListarTipoPresupuesto(TipoPresupuesto, (Usuario)Session["Usuario"]);
        ddlTipoPresupuesto.DataTextField = "descripcion";
        ddlTipoPresupuesto.DataValueField = "tipo_presupuesto";
        ddlTipoPresupuesto.DataBind();
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GuardarValoresConsulta(pConsulta, EjecucionService.GetType().Name);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EjecucionService.GetType().Name + "L", "btnConsultar_Click", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, EjecucionService.GetType().Name);
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.SelectedRow.Cells[1].Text;
        Session[EjecucionService.CodigoPrograma + ".idpresupuesto"] = id;
        Response.Redirect("Nuevo.aspx", false);
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
            BOexcepcion.Throw(EjecucionService.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            Configuracion conf = new Configuracion();
            List<Xpinn.Presupuesto.Entities.Presupuesto> lstConsulta = new List<Xpinn.Presupuesto.Entities.Presupuesto>();
            lstConsulta = EjecucionService.ListarPresupuesto(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            }

            Session.Add(EjecucionService.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EjecucionService.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private Xpinn.Presupuesto.Entities.Presupuesto ObtenerValores()
    {
        Xpinn.Presupuesto.Entities.Presupuesto ePresupuesto = new Xpinn.Presupuesto.Entities.Presupuesto();

        try
        {
            if (txtCodigo.Text.Trim() != "")
                ePresupuesto.idpresupuesto = Convert.ToInt64(txtCodigo.Text.Trim());
            if (ddlTipoPresupuesto.SelectedValue != null && ddlTipoPresupuesto.SelectedIndex != 0)
                ePresupuesto.tipo_presupuesto = Convert.ToInt32(ddlTipoPresupuesto.SelectedValue);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EjecucionService.GetType().Name + "L", "ObtenerValores", ex);
        }

        return ePresupuesto;
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id;
        id = gvLista.Rows[e.NewEditIndex].Cells[1].Text;
        Session[EjecucionService.CodigoPrograma + ".idpresupuesto"] = id;

        Navegar(Pagina.Nuevo);
    }

}