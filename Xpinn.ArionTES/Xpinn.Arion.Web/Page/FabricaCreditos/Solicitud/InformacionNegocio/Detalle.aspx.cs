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
    private Xpinn.FabricaCreditos.Services.InformacionNegocioService InformacionNegocioServicio = new Xpinn.FabricaCreditos.Services.InformacionNegocioService();
    Xpinn.FabricaCreditos.Entities.InformacionNegocio vInformacionNegocio = new Xpinn.FabricaCreditos.Entities.InformacionNegocio();
    List<Xpinn.FabricaCreditos.Entities.InformacionNegocio> lstInformacionNegocio = new List<Xpinn.FabricaCreditos.Entities.InformacionNegocio>();  //Lista de los menus desplegables
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(InformacionNegocioServicio.CodigoPrograma, "D");

            Site1 toolBar = (Site1)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;

            ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();
            ((Label)Master.FindControl("lblCod_Cliente")).Text = Session["Cod_persona"].ToString(); 
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionNegocioServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                
                AsignarEventoConfirmar();
                if (Session[InformacionNegocioServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[InformacionNegocioServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(InformacionNegocioServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(InformacionNegocioServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    private void CargarListas()
    {
        try
        {
            vInformacionNegocio.ListaSolicitada = "Localidad";
            TraerResultadosLista();
            ddlLocalidad.DataSource = lstInformacionNegocio;
            ddlLocalidad.DataTextField = "ListaDescripcion";
            ddlLocalidad.DataValueField = "ListaId";
            ddlLocalidad.DataBind();

            vInformacionNegocio.ListaSolicitada = "Barrio";
            vInformacionNegocio.localidad = vInformacionNegocio.localidad;
            TraerResultadosLista();
            ddlBarrio.DataSource = lstInformacionNegocio;
            ddlBarrio.DataTextField = "ListaDescripcion";
            ddlBarrio.DataValueField = "ListaId";
            ddlBarrio.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(lstInformacionNegocio.GetType().Name + "L", "CargarListas", ex);
        }
    }


    private void TraerResultadosLista()
    {

        lstInformacionNegocio.Clear();
        lstInformacionNegocio = InformacionNegocioServicio.ListarInformacionNegocio(vInformacionNegocio, (Usuario)Session["usuario"]);//vInformacionNegocio.usuultmod.ToString());
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
            InformacionNegocioServicio.EliminarInformacionNegocio(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionNegocioServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[InformacionNegocioServicio.CodigoPrograma + ".id"] = idObjeto;
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
            vInformacionNegocio = InformacionNegocioServicio.ConsultarInformacionNegocio(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vInformacionNegocio.cod_negocio != Int64.MinValue)
                txtCod_negocio.Text = vInformacionNegocio.cod_negocio.ToString().Trim();
            if (vInformacionNegocio.cod_persona != Int64.MinValue)
                txtCod_persona.Text = vInformacionNegocio.cod_persona.ToString().Trim();
            if (!string.IsNullOrEmpty(vInformacionNegocio.direccion))
                direccion.Text = vInformacionNegocio.direccion.ToString().Trim();
            if (!string.IsNullOrEmpty(vInformacionNegocio.telefono))
                txtTelefono.Text = vInformacionNegocio.telefono.ToString().Trim();           
            if (!string.IsNullOrEmpty(vInformacionNegocio.nombrenegocio))
                txtNombrenegocio.Text = vInformacionNegocio.nombrenegocio.ToString().Trim();
            if (!string.IsNullOrEmpty(vInformacionNegocio.descripcion))
                txtDescripcion.Text = vInformacionNegocio.descripcion.ToString().Trim();
            if (vInformacionNegocio.antiguedad != Int64.MinValue)
                txtAntiguedad.Text = vInformacionNegocio.antiguedad.ToString().Trim();
            if (vInformacionNegocio.propia != Int64.MinValue)
                rblTipoLocal.SelectedValue = vInformacionNegocio.propia.ToString().Trim();
            if (!string.IsNullOrEmpty(vInformacionNegocio.arrendador))
                txtArrendador.Text = vInformacionNegocio.arrendador.ToString().Trim();
            if (!string.IsNullOrEmpty(vInformacionNegocio.telefonoarrendador))
                txtTelefonoarrendador.Text = vInformacionNegocio.telefonoarrendador.ToString().Trim();
            if (vInformacionNegocio.codactividad != Int64.MinValue)
                rblActividad.SelectedValue = vInformacionNegocio.codactividad.ToString().Trim();
            if (vInformacionNegocio.experiencia != Int64.MinValue)
                txtExperiencia.Text = vInformacionNegocio.experiencia.ToString().Trim();
            if (vInformacionNegocio.emplperm != Int64.MinValue)
                txtEmplperm.Text = vInformacionNegocio.emplperm.ToString().Trim();
            if (vInformacionNegocio.empltem != Int64.MinValue)
                txtEmpltem.Text = vInformacionNegocio.empltem.ToString().Trim();

            //Despues de obtener datos se cargan las listas
            CargarListas();
            if (!string.IsNullOrEmpty(vInformacionNegocio.localidad))
            {
                ddlLocalidad.SelectedValue = HttpUtility.HtmlDecode(vInformacionNegocio.localidad.ToString().Trim());
                //ListItem selectedListItem = ddlLocalidad.Items.FindByValue(vInformacionNegocio.localidad.ToString().Trim());
                //if (selectedListItem != null)
                //    selectedListItem.Selected = true;
            }
            if (vInformacionNegocio.barrio != Int64.MinValue)
            {
                ddlBarrio.SelectedValue = HttpUtility.HtmlDecode(vInformacionNegocio.barrio.ToString().Trim());
                //ListItem selectedListItem = ddlBarrio.Items.FindByValue(vInformacionNegocio.barrio.ToString().Trim());
                //if (selectedListItem != null)
                //    selectedListItem.Selected = true;            
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionNegocioServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }
}