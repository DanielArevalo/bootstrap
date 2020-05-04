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

partial class Nuevo : GlobalWeb
{
    private Xpinn.Asesores.Services.AgendaTipoActividadService AgendaTipoActividadServicio = new Xpinn.Asesores.Services.AgendaTipoActividadService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            //if (Session[AgendaTipoActividadServicio.CodigoPrograma + ".id"] != null)
            //    VisualizarOpciones(AgendaTipoActividadServicio.CodigoPrograma, "E");
            //else
            //    VisualizarOpciones(AgendaTipoActividadServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
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

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Asesores.Entities.AgendaTipoActividad vAgendaTipoActividad = new Xpinn.Asesores.Entities.AgendaTipoActividad();

            if (idObjeto != "")
                vAgendaTipoActividad = AgendaTipoActividadServicio.ConsultarAgendaTipoActividad(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            //vAgendaTipoActividad.idtipo = Convert.ToInt64(txtIdtipo.Text.Trim());
            vAgendaTipoActividad.descripcion = Convert.ToString(txtDescripcion.Text.Trim());

            if (idObjeto != "")
            {
                vAgendaTipoActividad.idtipo = Convert.ToInt64(idObjeto);
                AgendaTipoActividadServicio.ModificarAgendaTipoActividad(vAgendaTipoActividad, (Usuario)Session["usuario"]);
            }
            else
            {
                vAgendaTipoActividad = AgendaTipoActividadServicio.CrearAgendaTipoActividad(vAgendaTipoActividad, (Usuario)Session["usuario"]);
                idObjeto = vAgendaTipoActividad.idtipo.ToString();
            }

            Session[AgendaTipoActividadServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AgendaTipoActividadServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[AgendaTipoActividadServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Asesores.Entities.AgendaTipoActividad vAgendaTipoActividad = new Xpinn.Asesores.Entities.AgendaTipoActividad();
            vAgendaTipoActividad = AgendaTipoActividadServicio.ConsultarAgendaTipoActividad(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            //if (vAgendaTipoActividad.idtipo != Int64.MinValue)
            //    txtIdtipo.Text = HttpUtility.HtmlDecode(vAgendaTipoActividad.idtipo.ToString().Trim());
            if (!string.IsNullOrEmpty(vAgendaTipoActividad.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vAgendaTipoActividad.descripcion.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AgendaTipoActividadServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
}