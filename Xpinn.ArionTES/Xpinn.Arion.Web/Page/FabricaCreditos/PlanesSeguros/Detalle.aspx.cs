using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;


public partial class Detalle : GlobalWeb
{

    PlanesSegurosService planSegurosServicio = new PlanesSegurosService();

    PlanesSegurosAmparosService planSegurosAmparosServicio= new PlanesSegurosAmparosService();

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {            
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[planSegurosServicio.CodigoPlan + ".id"] != null)
                {
                    idObjeto = Session[planSegurosServicio.CodigoPlan + ".id"].ToString();
                    Session.Remove(planSegurosServicio.CodigoPlan + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(planSegurosServicio.GetType().Name + "D", "Page_Load", ex);
        }


    }
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(planSegurosServicio.CodigoPrograma.ToString(), "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(planSegurosServicio.CodigoPlan + "A", "Page_PreInit", ex);
        }
    }
    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

  

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
       
        Navegar(Pagina.Lista);
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            planSegurosServicio.EliminarPlanesSeguros(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(planSegurosServicio.CodigoPlan + "C", "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[planSegurosServicio.CodigoPlan + ".id"] = idObjeto;
        Session["operacion"] = "";
        Navegar(Pagina.Editar);
    }


    protected void ObtenerDatos(String pIdObjeto)
    {

        try
        {
            PlanesSeguros planesSeguros = new PlanesSeguros();
            if (pIdObjeto != null)
            {
                planesSeguros.tipo_plan = Int32.Parse(pIdObjeto);
                planesSeguros = planSegurosServicio.ConsultarPlanesSeguros(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
               
            PlanesSegurosAmparos planesSegurosAmparos = new PlanesSegurosAmparos();
           
            if (!string.IsNullOrEmpty(planesSeguros.tipo_plan.ToString()))
            {
                txtcodigoplan.Text = planesSeguros.tipo_plan.ToString();
                txtnombreplan.Text = planesSeguros.descripcion.ToString();
                txtPrima_Ind.Text = planesSeguros.prima_individual.ToString();
                txtPrima_Cony.Text = planesSeguros.prima_conyuge.ToString();
                txtPrima_Acci_Ind.Text = planesSeguros.prima_accidentes_individual.ToString();
                txtPrima_Acci_Fam.Text = planesSeguros.prima_accidentes_familiar.ToString();
            }
            this.ConsultarVidaGrupo();
            this.ConsultarAccidentes();
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(planSegurosServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }


    private void ConsultarVidaGrupo()
    {
        String tipo_plan = "0" + txtcodigoplan.Text;
        MostrarDatosAmparos("Vida Grupo", tipo_plan, gvVidaGrupo);

    }

    private void ConsultarAccidentes()
    {
        String tipo_plan = "0" + txtcodigoplan.Text;
        MostrarDatosAmparos("Accidentes Personales", tipo_plan, gvAccPers);
    }
    

    private void MostrarDatosAmparos(String Tipo, String tipo_plan, GridView gvAmparos)
    {
        PlanesSegurosAmparos pPlanesSegurosAmparos = new PlanesSegurosAmparos();
        List<PlanesSegurosAmparos> LstPlanesSegurosAmparos = new List<PlanesSegurosAmparos>();
        pPlanesSegurosAmparos.tipo_plan = Convert.ToInt64("0" + tipo_plan);
        pPlanesSegurosAmparos.tipo = Tipo;

        LstPlanesSegurosAmparos = planSegurosAmparosServicio.ListarPlanesSegurosAmparos(pPlanesSegurosAmparos, (Usuario)Session["usuario"]);
        gvAmparos.DataSource = LstPlanesSegurosAmparos;
        gvAmparos.DataBind();

    }

    private void ListarDatosPlanesAmparos(Int64 tipo_plan)
    {
        PlanesSegurosAmparos planesegurosamparos = new PlanesSegurosAmparos();
        planesegurosamparos.tipo_plan = planesegurosamparos.tipo_plan = tipo_plan;     

        List<PlanesSegurosAmparos> lstPlanesSegurosAmparosData = new List<PlanesSegurosAmparos>();

        lstPlanesSegurosAmparosData = planSegurosAmparosServicio.ListarPlanesSegurosAmparos(planesegurosamparos, (Usuario)Session["usuario"]);

        this.gvVidaGrupo.DataSource = lstPlanesSegurosAmparosData;
        gvVidaGrupo.DataBind();
        Session["PlanesSegurosAmparos"] = lstPlanesSegurosAmparosData;

    }
   

    protected void gvVidaGrupo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void txtcodigoplan_TextChanged(object sender, EventArgs e)
    {

    }
}