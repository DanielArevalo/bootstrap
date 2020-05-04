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
    private Xpinn.Asesores.Services.CreacionMensajeService mensajeService = new Xpinn.Asesores.Services.CreacionMensajeService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(mensajeService.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(mensajeService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[mensajeService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[mensajeService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(mensajeService.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(mensajeService.CodigoPrograma, "Page_Load", ex);
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
            mensajeService.EliminarMensaje(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(mensajeService.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[mensajeService.CodigoPrograma + ".id"] = idObjeto;
        Navegar(Pagina.Nuevo);
    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((ImageButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Asesores.Entities.CreacionMensaje vCreacionMensaje = new Xpinn.Asesores.Entities.CreacionMensaje();
            vCreacionMensaje = mensajeService.ConsultarMensajes(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vCreacionMensaje.IdMensaje != Int64.MinValue)
                txtIdMensaje.Text = vCreacionMensaje.IdMensaje.ToString().Trim();
            if (!string.IsNullOrEmpty(vCreacionMensaje.Descripcion))
                txtDescripcion.Text = vCreacionMensaje.Descripcion.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(mensajeService.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }
}