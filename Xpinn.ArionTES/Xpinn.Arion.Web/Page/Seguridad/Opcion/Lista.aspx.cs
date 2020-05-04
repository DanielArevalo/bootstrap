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
    private Xpinn.Seguridad.Services.OpcionService OpcionServicio = new Xpinn.Seguridad.Services.OpcionService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(OpcionServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(OpcionServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarProcesos();
                CargarValoresConsulta(pConsulta, OpcionServicio.CodigoPrograma);
                if (Session[OpcionServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(OpcionServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, OpcionServicio.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, OpcionServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, OpcionServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(OpcionServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[OpcionServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[OpcionServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            OpcionServicio.EliminarOpcion(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(OpcionServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(OpcionServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Seguridad.Entities.Opcion> lstConsulta = new List<Xpinn.Seguridad.Entities.Opcion>();
            lstConsulta = OpcionServicio.ListarOpcion(ObtenerValores(), (Usuario)Session["usuario"]);

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

            Session.Add(OpcionServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(OpcionServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.Seguridad.Entities.Opcion ObtenerValores()
    {
        Xpinn.Seguridad.Entities.Opcion vOpcion = new Xpinn.Seguridad.Entities.Opcion();

        if (txtCod_opcion.Text.Trim() != "")
            vOpcion.cod_opcion = Convert.ToInt64(txtCod_opcion.Text.Trim());
        if (txtNombre.Text.Trim() != "")
            vOpcion.nombre = Convert.ToString(txtNombre.Text.Trim());
        if (txtCod_proceso.Text.Trim() != "")
            vOpcion.cod_proceso = Convert.ToInt64(txtCod_proceso.Text.Trim());
        if (txtGeneralog.Text.Trim() != "")
            vOpcion.generalog = Convert.ToInt64(txtGeneralog.Text.Trim());

        return vOpcion;
    }

    private void CargarProcesos()
    {
        try
        {
            Xpinn.Seguridad.Services.ProcesoService procesoServicio = new Xpinn.Seguridad.Services.ProcesoService();
            List<Xpinn.Seguridad.Entities.Proceso> lstProceso = new List<Xpinn.Seguridad.Entities.Proceso>();

            lstProceso = procesoServicio.ListarProceso(null, (Usuario)Session["usuario"]);

            txtCod_proceso.DataSource = lstProceso;
            txtCod_proceso.DataTextField = "nombre";
            txtCod_proceso.DataValueField = "cod_proceso";
            txtCod_proceso.DataBind();

            txtCod_proceso.Items.Insert(0, "");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(OpcionServicio.CodigoPrograma, "CargarProcesos", ex);
        }
    }
}