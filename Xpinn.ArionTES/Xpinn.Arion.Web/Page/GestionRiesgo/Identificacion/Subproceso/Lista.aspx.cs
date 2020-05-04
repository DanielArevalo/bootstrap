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
            VisualizarOpciones(identificacionServicio.CodigoProgramaS, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaS, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarLista();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaS, "Page_Load", ex);
        }
    }
    private void CargarLista()
    {
        PoblarListas poblar = new PoblarListas();

        poblar.PoblarListaDesplegable("GR_PROCESO_ENTIDAD", "COD_PROCESO, DESCRIPCION", "", "1", ddlProceso, (Usuario)Session["usuario"]);
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
        Identificacion vSubProceso = new Identificacion();

        if (txtCodigoSubproceso.Text != "")
            vSubProceso.cod_subproceso = Convert.ToInt64(txtCodigoSubproceso.Text);
        if (txtDescripcionSubproceso.Text != "")
            vSubProceso.descripcion = Convert.ToString(txtDescripcionSubproceso.Text);
        if (ddlProceso.SelectedValue != "")
            vSubProceso.cod_proceso = Convert.ToInt64(ddlProceso.SelectedValue);
        gvSubproceso.DataSource = null;
        gvSubproceso.DataBind();

        lstProcesos = identificacionServicio.ListarSubProcesosEntidad(vSubProceso, (Usuario)Session["usuario"]);
        if (lstProcesos.Count > 0)
        {
            Session["lstProcesos"] = lstProcesos;
            panelListado.Visible = true;
            gvSubproceso.DataSource = lstProcesos.OrderBy(x => x.cod_subproceso).ToList();
            gvSubproceso.DataBind();
            lblTotalRegs.Text = "Registros encontrados: " + lstProcesos.Count;
        }
        else
        {
            panelListado.Visible = false;
            lblTotalRegs.Text = "La consulta no obtuvo resultado";
        }

    }

    protected void gvSubproceso_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            VerError("");
            Identificacion pSubproceso = new Identificacion();
            List<Identificacion> lstFactores = new List<Identificacion>();
            pSubproceso.cod_subproceso = Convert.ToInt64(gvSubproceso.DataKeys[e.RowIndex].Values[0]);

            lstFactores = identificacionServicio.ListarFactoresRiesgo(pSubproceso, "", (Usuario)Session["usuario"]);
            if (lstFactores.Count == 0)
            {
                identificacionServicio.EliminarSubProcesoEntidad(pSubproceso, (Usuario)Session["usuario"]);
                Actualizar();
            }
            else
                VerError("No puede eliminar el subproceso, hay factores de riesgo asociados");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaS, "gvSubproceso_RowDeleting", ex);
        }

        Actualizar();
    }

    protected void gvSubproceso_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvSubproceso.DataKeys[e.NewEditIndex].Values[0].ToString();
        Session[identificacionServicio.CodigoProgramaS + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvSubproceso_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvSubproceso.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaS, "gvSubproceso_PageIndexChanging", ex);
        }
    }    
}