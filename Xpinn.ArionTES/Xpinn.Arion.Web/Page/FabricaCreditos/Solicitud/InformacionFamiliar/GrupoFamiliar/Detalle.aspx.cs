using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class Detalle : GlobalWeb
{
    Xpinn.FabricaCreditos.Services.FamiliaresService familiaresServicio = new Xpinn.FabricaCreditos.Services.FamiliaresService();
    String ListaSolicitada = "";
    List<Xpinn.FabricaCreditos.Entities.Familiares> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Familiares>();  //Lista de los menus desplegables
    
    private void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(familiaresServicio.CodigoPrograma, "D");

            Site1 toolBar = (Site1)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(familiaresServicio.CodigoPrograma + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[familiaresServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[familiaresServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(familiaresServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                CargarListas();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(familiaresServicio.GetType().Name + "D", "Page_Load", ex);
        }
    }
    private void CargarListas()
    {
        // Carga la lista de parentescos
        ListaSolicitada = "Parentescos";
        TraerResultadosLista();
        ddlParentesco.DataSource = lstDatosSolicitud;
        ddlParentesco.DataTextField = "ListaDescripcion";
        ddlParentesco.DataValueField = "ListaId";
        ddlParentesco.DataBind();
    }

    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = familiaresServicio.ListasDesplegables(ObtenerValores(), (Usuario)Session["usuario"], ListaSolicitada);

    }

    private Xpinn.FabricaCreditos.Entities.Familiares ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.Familiares familiares = new Xpinn.FabricaCreditos.Entities.Familiares();
        //datosCliente.numero = txtIdentificacion.Text;
        return familiares;
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
            familiaresServicio.EliminarFamiliares(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(familiaresServicio.CodigoPrograma + "C", "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[familiaresServicio.CodigoPrograma + ".id"] = idObjeto;
        Navegar(Pagina.Nuevo);
    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            
            Xpinn.FabricaCreditos.Entities.Familiares familiares = new Xpinn.FabricaCreditos.Entities.Familiares();
            familiares = familiaresServicio.ConsultarFamiliares(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

             if (!string.IsNullOrEmpty(familiares.cod_persona.ToString()))
                txtCodigoPersona.Text = HttpUtility.HtmlDecode(familiares.cod_persona.ToString());
            if (!string.IsNullOrEmpty(familiares.nombres))
                txtNombres.Text = HttpUtility.HtmlDecode(familiares.nombres.Trim().ToString());
            if (!string.IsNullOrEmpty(familiares.codparentesco.ToString()))
                ddlParentesco.SelectedValue = HttpUtility.HtmlDecode(familiares.codparentesco.ToString());
            if (!string.IsNullOrEmpty(familiares.sexo))
                rblSexo.SelectedValue = HttpUtility.HtmlDecode(familiares.sexo.Trim().ToString());
            if (!string.IsNullOrEmpty(familiares.acargo.ToString()))
                rblAcargo.SelectedValue = HttpUtility.HtmlDecode(familiares.acargo.ToString());
            if (!string.IsNullOrEmpty(familiares.observaciones))
                txtObservaciones.Text = HttpUtility.HtmlDecode(familiares.observaciones.ToString());

           
           

            //VerAuditoria(familiares.UsuarioCrea, familiares.FechaCrea, familiares.UsuarioEdita, familiares.FechaEdita);
        
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(familiaresServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }
}