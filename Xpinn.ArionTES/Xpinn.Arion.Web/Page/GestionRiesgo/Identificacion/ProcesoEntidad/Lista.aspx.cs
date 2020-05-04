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

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(identificacionServicio.CodigoProgramaP, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaP, "Page_PreInit", ex);
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
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaP, "Page_Load", ex);
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
        List<Identificacion> lstProcesos = new List<Identificacion>();
        Identificacion vProceso = new Identificacion();

        if (txtCodigoProceso.Text != "")
            vProceso.cod_proceso = Convert.ToInt64(txtCodigoProceso.Text);
        if(txtDescripcion.Text != "")
            vProceso.descripcion = Convert.ToString(txtDescripcion.Text);

        lstProcesos = identificacionServicio.ListarProcesosEntidad(vProceso,(Usuario)Session["usuario"]);
        if (lstProcesos.Count > 0)
        {
            pLista.Visible = true;
            gvProcesoEntidad.DataSource = lstProcesos.OrderBy(x => x.cod_proceso).ToList(); 
            gvProcesoEntidad.DataBind();
            lblTotalRegs.Text = "Registros encontrados: " + lstProcesos.Count;
        }
        else
        {
            pLista.Visible = false;
            lblTotalRegs.Text = "La consulta no obtuvo resultado";
        }

    }

    protected void gvProcesoEntidad_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            VerError("");
            Identificacion pProceso = new Identificacion();
            List<Identificacion> lstSubprocesos = new List<Identificacion>();
            pProceso.cod_proceso = Convert.ToInt64(gvProcesoEntidad.DataKeys[e.RowIndex].Values[0]);

            lstSubprocesos = identificacionServicio.ListarSubProcesosEntidad(pProceso, (Usuario)Session["usuario"]);
            if (lstSubprocesos.Count == 0)
            {
                identificacionServicio.EliminarProcesoEntidad(pProceso, (Usuario)Session["usuario"]);
                Actualizar();
            }
            else
                VerError("No puede eliminar el proceso, hay subprocesos asociados");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaP, "gvProcesoEntidad_RowDeleting", ex);
        }
    }

    protected void gvProcesoEntidad_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvProcesoEntidad.DataKeys[e.NewEditIndex].Values[0].ToString();
        Session[identificacionServicio.CodigoProgramaP + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvProcesoEntidad_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvProcesoEntidad.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaP, "gvProcesoEntidad_PageIndexChanging", ex);
        }
    }
}