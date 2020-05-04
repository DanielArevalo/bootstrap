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
    Xpinn.Presupuesto.Services.PresupuestoService PresupuestoServicio = new Xpinn.Presupuesto.Services.PresupuestoService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(PresupuestoServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDDList();
                CargarValoresConsulta(pConsulta, PresupuestoServicio.GetType().Name);
                if (Session[PresupuestoServicio.GetType().Name + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoServicio.GetType().Name + "L", "Page_Load", ex);
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

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Session.Remove("DTPRESUPUESTO");
            Session.Remove("DTFECHAS");
            Session.Remove("DTCOLOCACION");
            Session.Remove("DTSALDOS");
            Session.Remove("DTNOMINA");
            GuardarValoresConsulta(pConsulta, PresupuestoServicio.GetType().Name);
            Navegar(Pagina.Nuevo);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoServicio.GetType().Name + "L", "btnNuevo_Click", ex);
        }

    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GuardarValoresConsulta(pConsulta, PresupuestoServicio.GetType().Name);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoServicio.GetType().Name + "L", "btnConsultar_Click", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, PresupuestoServicio.GetType().Name);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnEliminar");
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.SelectedRow.Cells[2].Text;
        Session[PresupuestoServicio.CodigoPrograma + ".idpresupuesto"] = id;
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
            BOexcepcion.Throw(PresupuestoServicio.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            Configuracion conf = new Configuracion();
            List<Xpinn.Presupuesto.Entities.Presupuesto> lstConsulta = new List<Xpinn.Presupuesto.Entities.Presupuesto>();
            lstConsulta = PresupuestoServicio.ListarPresupuesto(ObtenerValores(), (Usuario)Session["usuario"]);

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

            Session.Add(PresupuestoServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoServicio.GetType().Name + "L", "Actualizar", ex);
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
            BOexcepcion.Throw(PresupuestoServicio.GetType().Name + "L", "ObtenerValores", ex);
        }

        return ePresupuesto;
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id;
        id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[PresupuestoServicio.CodigoPrograma + ".idpresupuesto"] = id;

        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long idObjeto1 = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[3].Text);
            //cajaServicio.EliminarOficina(idObjeto1, (Usuario) Session["usuario"]);//, (UserSession)Session["user"]);
            Actualizar();
            Navegar(Pagina.Lista);
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoServicio.GetType().Name + "L", "gvLista_RowDeleting", ex);
        }
    }

}