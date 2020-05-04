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
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;

public partial class Lista : GlobalWeb
{

    PlanesSegurosService planesSegurosServicio = new PlanesSegurosService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(planesSegurosServicio.CodigoPrograma.ToString(), "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(planesSegurosServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, planesSegurosServicio.CodigoPrograma);
                if (Session[planesSegurosServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(planesSegurosServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {

           Page.Validate();

           if (Page.IsValid)
           {
               GuardarValoresConsulta(pConsulta, planesSegurosServicio.CodigoPrograma);
               Actualizar();
           }
      
    }
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, planesSegurosServicio.CodigoPrograma);
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, planesSegurosServicio.GetType().Name);
        Navegar(Pagina.Nuevo);
        Session["PlanesSegurosAmparos"] = null;
        Session["PlanesSegurosAmparosVida"] = null;
        Session["PlanesSegurosAmparosAcc"] = null;
        Session["operacion"] = "N";

    }

   
    public void Actualizar()
    {
        try
        {
            List<PlanesSeguros> lstConsulta = new List<PlanesSeguros>();
            lstConsulta = planesSegurosServicio.ListarPlanesSeguros(ObtenerValores(), (Usuario)Session["usuario"]);

            gvPlanesSeguros.PageSize = pageSize;
            gvPlanesSeguros.EmptyDataText = emptyQuery;
            gvPlanesSeguros.DataSource = lstConsulta;
            
            if (lstConsulta.Count > 0)
            {
                gvPlanesSeguros.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvPlanesSeguros.DataBind();
                ValidarPermisosGrilla(gvPlanesSeguros);
            }
            else
            {
                gvPlanesSeguros.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            

            }

            Session.Add(planesSegurosServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(planesSegurosServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private PlanesSeguros ObtenerValores()
    {
        PlanesSeguros planesSeguros = new PlanesSeguros();
        if (this.txtCodigoPlan.Text.Trim() != "")
        {
            planesSeguros.tipo_plan = Convert.ToInt64("0" + txtCodigoPlan.Text.Trim());
        }
        return planesSeguros;
    }


    protected void gvPlanesSeguros_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvPlanesSeguros.SelectedRow.Cells[2].Text;
        Session[planesSegurosServicio.CodigoPlan + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvPlanesSeguros_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvPlanesSeguros.Rows[e.NewEditIndex].Cells[2].Text;    

        Session[planesSegurosServicio.CodigoPlan+ ".id"] = id;
        Session["operacion"] = "";
        Navegar(Pagina.Editar);
    }
   

    protected void gvPlanesSeguros_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvPlanesSeguros.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(planesSegurosServicio.CodigoPrograma, "gvgvPlanesSeguros_PageIndexChanging", ex);
        }
    }
}