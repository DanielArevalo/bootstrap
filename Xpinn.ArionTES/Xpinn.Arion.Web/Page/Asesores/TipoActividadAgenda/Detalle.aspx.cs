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
    private Xpinn.Asesores.Services.AgendaTipoActividadService AgendaTipoActividadServicio = new Xpinn.Asesores.Services.AgendaTipoActividadService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            //VisualizarOpciones(AgendaTipoActividadServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AgendaTipoActividadServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[AgendaTipoActividadServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[AgendaTipoActividadServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(AgendaTipoActividadServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(AgendaTipoActividadServicio.CodigoPrograma, "Page_Load", ex);
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
            AgendaTipoActividadServicio.EliminarAgendaTipoActividad(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AgendaTipoActividadServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[AgendaTipoActividadServicio.CodigoPrograma + ".id"] = idObjeto;
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
            Xpinn.Asesores.Entities.AgendaTipoActividad vAgendaTipoActividad = new Xpinn.Asesores.Entities.AgendaTipoActividad();
            vAgendaTipoActividad = AgendaTipoActividadServicio.ConsultarAgendaTipoActividad(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vAgendaTipoActividad.idtipo != Int64.MinValue)
                txtIdtipo.Text = vAgendaTipoActividad.idtipo.ToString().Trim();
            if (!string.IsNullOrEmpty(vAgendaTipoActividad.descripcion))
                txtDescripcion.Text = vAgendaTipoActividad.descripcion.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AgendaTipoActividadServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }
}