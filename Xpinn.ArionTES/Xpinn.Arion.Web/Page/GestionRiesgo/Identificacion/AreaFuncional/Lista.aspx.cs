using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Riesgo.Services;
using Xpinn.Riesgo.Entities;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    IdentificacionServices identificacionServicio = new IdentificacionServices();
    SeguimientoServices seguimientoServicio = new SeguimientoServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(identificacionServicio.CodigoProgramaA, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaA, "Page_PreInit", ex);
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
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaA, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Actualizar();
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarPanel(pConsulta);
    }

    private void Actualizar()
    {
        List<Identificacion> lstAreas = new List<Identificacion>();
        Identificacion vArea = new Identificacion();

        if (txtCodigoArea.Text != "")
            vArea.cod_area = Convert.ToInt64(txtCodigoArea.Text);
        if (txtDescripcionArea.Text != "")
            vArea.descripcion = Convert.ToString(txtDescripcionArea.Text);

        lstAreas = identificacionServicio.ListarAreasEntidad(vArea, (Usuario)Session["usuario"]);
        if (lstAreas.Count > 0)
        {
            panelGrilla.Visible = true;
            gvAreaFuncional.DataSource = lstAreas;
            gvAreaFuncional.DataBind();
            lblTotalRegs.Text = "Registros encontrados: " + lstAreas.Count;
        }
        else
        {
            panelGrilla.Visible = false;
            lblTotalRegs.Text = "La consulta no obtuvo resultado";
        }

    }

    protected void gvAreaFuncional_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            VerError("");
            Identificacion pArea = new Identificacion();
            List<Identificacion> lstCausas = new List<Identificacion>();
            List<Seguimiento> lstControl = new List<Seguimiento>();
            List<Seguimiento> lstMonitoreo = new List<Seguimiento>();
            Seguimiento pSeguimiento = new Seguimiento();

            pArea.cod_area = Convert.ToInt64(gvAreaFuncional.DataKeys[e.RowIndex].Values[0]);

            lstCausas = identificacionServicio.ListarCausas(pArea, "", (Usuario)Session["usuario"]);

            pSeguimiento.cod_cargo = Convert.ToInt64(gvAreaFuncional.DataKeys[e.RowIndex].Values[0]);
            lstControl = seguimientoServicio.ListarFormasControl(pSeguimiento, (Usuario)Session["usuario"]);
            lstMonitoreo = seguimientoServicio.ListarTiposMonitoreo(pSeguimiento, (Usuario)Session["usuario"]);

            if (lstCausas.Count == 0 && lstControl.Count == 0 && lstMonitoreo.Count == 0)
            {
                identificacionServicio.EliminarAreaFuncional(pArea, (Usuario)Session["usuario"]);
                Actualizar();
            }
            else if (lstCausas.Count > 0 || lstControl.Count > 0 || lstMonitoreo.Count > 0)
            {
                VerError("No puede eliminar el área, hay procesos asociados");
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaA, "gvAreaFuncional_RowDeleting", ex);
        }
    }

    protected void gvAreaFuncional_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvAreaFuncional.DataKeys[e.NewEditIndex].Values[0].ToString();
        Session[identificacionServicio.CodigoProgramaA + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvAreaFuncional_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvAreaFuncional.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaA, "gvAreaFuncional_PageIndexChanging", ex);
        }
    }
}