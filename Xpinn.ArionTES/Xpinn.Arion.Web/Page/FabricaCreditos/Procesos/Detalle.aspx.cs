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

partial class Detalle : GlobalWeb
{
     String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
     private Xpinn.FabricaCreditos.Services.ProcesosService ProcesosServicio = new Xpinn.FabricaCreditos.Services.ProcesosService(); // Permite iniciar la consulta del historial (Segundo GridView)
     List<Xpinn.FabricaCreditos.Entities.ControlTiempos> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.ControlTiempos>();  //Lista de los menus desplegables
     private Xpinn.FabricaCreditos.Services.ControlTiemposService ControlProcesosServicio = new Xpinn.FabricaCreditos.Services.ControlTiemposService();
     String operacion = "";
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ProcesosServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ProcesosServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(ProcesosServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoEditar+= btnEditar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesosServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                CargarListas();
                if (Session[ProcesosServicio.CodigoPrograma + ".id"] != null)
                {
                   
                    idObjeto = Session[ProcesosServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ProcesosServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(ProcesosServicio.CodigoPrograma, "Page_Load", ex);
        }
    }
    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[ProcesosServicio.CodigoPrograma + ".id"] = idObjeto;
        Navegar(Pagina.Nuevo);
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Procesos vProcesos = new Xpinn.FabricaCreditos.Entities.Procesos();

            if (idObjeto != "")
                vProcesos = ProcesosServicio.ConsultarProcesos(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

         //   vProcesos.cod_proceso = Convert.ToInt32(txtCodProceso.Text.Trim());
            vProcesos.descripcion = Convert.ToString(txtDescripcion.Text.Trim());
            vProcesos.tipo_proceso = Convert.ToInt32(ddlTipoProceso.SelectedValue);
            vProcesos.cod_proceso_antec = Convert.ToInt32(this.ddlAntecesor.SelectedValue);
           
           

            if (idObjeto != "")
            {
                vProcesos.cod_proceso = Convert.ToInt32(idObjeto);
                ProcesosServicio.ModificarProcesos(vProcesos, (Usuario)Session["usuario"]);
            }
            else
            {
                vProcesos = ProcesosServicio.CrearProcesos(vProcesos, (Usuario)Session["usuario"]);
                idObjeto = vProcesos.cod_proceso.ToString();
            }

            Session[ProcesosServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesosServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        txtDescripcion.Visible = true;
        try
        {
            Xpinn.FabricaCreditos.Entities.Procesos vProcesos = new Xpinn.FabricaCreditos.Entities.Procesos();
            vProcesos = ProcesosServicio.ConsultarProcesos(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vProcesos.cod_proceso.ToString()))
                txtCodProceso.Text = HttpUtility.HtmlDecode(vProcesos.cod_proceso.ToString().Trim());
            if (!string.IsNullOrEmpty(vProcesos.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vProcesos.descripcion.ToString().Trim());
            if (!string.IsNullOrEmpty(vProcesos.tipo_proceso.ToString()))
                ddlTipoProceso.SelectedValue = HttpUtility.HtmlDecode(vProcesos.tipo_proceso.ToString().Trim());

            if (!string.IsNullOrEmpty(vProcesos.cod_proceso_antec.ToString()))
                ddlAntecesor.SelectedValue = HttpUtility.HtmlDecode(vProcesos.cod_proceso_antec.ToString().Trim());

           


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesosServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = ControlProcesosServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
    }

    private void CargarListas()
    {
        try
        {
           


            ListaSolicitada = "EstadoProcesoAntecesor";
            TraerResultadosLista();
            ddlAntecesor.DataSource = lstDatosSolicitud;
            ddlAntecesor.DataTextField = "ListaDescripcion";
            ddlAntecesor.DataValueField = "ListaId";
            ddlAntecesor.DataBind();
            ddlAntecesor.Items.Add("");
            ddlAntecesor.Text = "";


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesosServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }
    

    protected void ddlTipoCuota_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
}