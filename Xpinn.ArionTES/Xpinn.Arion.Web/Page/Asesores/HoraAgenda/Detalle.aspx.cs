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
    private Xpinn.Asesores.Services.AgendaHoraService AgendaHoraServicio = new Xpinn.Asesores.Services.AgendaHoraService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            //VisualizarOpciones(AgendaHoraServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AgendaHoraServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[AgendaHoraServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[AgendaHoraServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(AgendaHoraServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(AgendaHoraServicio.CodigoPrograma, "Page_Load", ex);
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
            AgendaHoraServicio.EliminarAgendaHora(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AgendaHoraServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[AgendaHoraServicio.CodigoPrograma + ".id"] = idObjeto;
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
            Xpinn.Asesores.Entities.AgendaHora vAgendaHora = new Xpinn.Asesores.Entities.AgendaHora();
            vAgendaHora = AgendaHoraServicio.ConsultarAgendaHora(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vAgendaHora.idhora != Int64.MinValue)
                txtIdhora.Text = vAgendaHora.idhora.ToString().Trim();
            if (vAgendaHora.hora != Int64.MinValue)
                txtHora.Text = vAgendaHora.hora.ToString().Trim();
            if (!string.IsNullOrEmpty(vAgendaHora.tipo))
                ddlTipo.SelectedValue = vAgendaHora.tipo.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AgendaHoraServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }
}