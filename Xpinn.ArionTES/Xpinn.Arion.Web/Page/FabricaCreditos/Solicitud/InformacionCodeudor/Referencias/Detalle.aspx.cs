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
    private Xpinn.FabricaCreditos.Services.RefernciasService RefernciasServicio = new Xpinn.FabricaCreditos.Services.RefernciasService();
    //Listas desplegables:
    String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    List<Xpinn.FabricaCreditos.Entities.Referncias> lstListasDesplegables = new List<Xpinn.FabricaCreditos.Entities.Referncias>();  //Lista de los menus desplegables
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(RefernciasServicio.CodigoPrograma, "D");

            Site1 toolBar = (Site1)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;

            ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RefernciasServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[RefernciasServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[RefernciasServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(RefernciasServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(RefernciasServicio.CodigoPrograma, "Page_Load", ex);
        }
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
            RefernciasServicio.EliminarReferncias(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RefernciasServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[RefernciasServicio.CodigoPrograma + ".id"] = idObjeto;
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
            Xpinn.FabricaCreditos.Entities.Referncias vReferncias = new Xpinn.FabricaCreditos.Entities.Referncias();
            vReferncias = RefernciasServicio.ConsultarReferncias(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vReferncias.codreferencia != Int64.MinValue)
                txtCodreferencia.Text = vReferncias.codreferencia.ToString().Trim();
            if (vReferncias.cod_persona != Int64.MinValue)
                txtCod_persona.Text = vReferncias.cod_persona.ToString().Trim();
            if (vReferncias.tiporeferencia != Int64.MinValue)
                rblTipoReferencia.SelectedValue = vReferncias.tiporeferencia.ToString().Trim();
            if (!string.IsNullOrEmpty(vReferncias.nombres))
                txtNombres.Text = vReferncias.nombres.ToString().Trim();            
            if (!string.IsNullOrEmpty(vReferncias.direccion))
                direccion.Text = vReferncias.direccion.ToString().Trim();
            if (!string.IsNullOrEmpty(vReferncias.telefono))
                txtTelefono.Text = vReferncias.telefono.ToString().Trim();
            if (!string.IsNullOrEmpty(vReferncias.teloficina))
                txtTeloficina.Text = vReferncias.teloficina.ToString().Trim();
            if (!string.IsNullOrEmpty(vReferncias.celular))
                txtCelular.Text = vReferncias.celular.ToString().Trim();
            if (vReferncias.estado != Int64.MinValue)
                txtEstado.Text = vReferncias.estado.ToString().Trim();
            if (vReferncias.codusuverifica != Int64.MinValue)
                txtCodusuverifica.Text = vReferncias.codusuverifica.ToString().Trim();
            if (vReferncias.fechaverifica != DateTime.MinValue)
                txtFechaverifica.Text = vReferncias.fechaverifica.ToShortDateString();
            if (!string.IsNullOrEmpty(vReferncias.calificacion))
                txtCalificacion.Text = vReferncias.calificacion.ToString().Trim();
            if (!string.IsNullOrEmpty(vReferncias.observaciones))
                txtObservaciones.Text = vReferncias.observaciones.ToString().Trim();
            if (vReferncias.numero_radicacion != Int64.MinValue)
                txtNumero_radicacion.Text = vReferncias.numero_radicacion.ToString().Trim();


            //Despues de obtener datos se cargan las listas
            CargarListas();
            if (vReferncias.codparentesco != Int64.MinValue)
               ddlParentesco.SelectedValue = HttpUtility.HtmlDecode(vReferncias.codparentesco.ToString().Trim());              
           
            

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RefernciasServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }



    private void CargarListas()
    {
        try
        {
            ListaSolicitada = "Parentesco";
            TraerResultadosLista();
            ddlParentesco.DataSource = lstListasDesplegables;
            ddlParentesco.DataTextField = "ListaDescripcion";
            ddlParentesco.DataValueField = "ListaId";
            ddlParentesco.DataBind();

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RefernciasServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    private void TraerResultadosLista()
    {
        lstListasDesplegables.Clear();
        lstListasDesplegables = RefernciasServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);

    }


}