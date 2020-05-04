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
    private Xpinn.Asesores.Services.AgendaHoraService AgendaHoraServicio = new Xpinn.Asesores.Services.AgendaHoraService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[AgendaHoraServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(AgendaHoraServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(AgendaHoraServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
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

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Asesores.Entities.AgendaHora vAgendaHora = new Xpinn.Asesores.Entities.AgendaHora();

            if (idObjeto != "")
                vAgendaHora = AgendaHoraServicio.ConsultarAgendaHora(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            
            vAgendaHora.hora = Convert.ToInt64(txtHora.Text.Trim());
            vAgendaHora.tipo = Convert.ToString(ddlTipo.SelectedValue);

            if (idObjeto != "")
            {
                vAgendaHora.idhora = Convert.ToInt64(idObjeto);
                AgendaHoraServicio.ModificarAgendaHora(vAgendaHora, (Usuario)Session["usuario"]);
            }
            else
            {
                vAgendaHora = AgendaHoraServicio.CrearAgendaHora(vAgendaHora, (Usuario)Session["usuario"]);
                idObjeto = vAgendaHora.idhora.ToString();
            }

            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AgendaHoraServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[AgendaHoraServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Asesores.Entities.AgendaHora vAgendaHora = new Xpinn.Asesores.Entities.AgendaHora();
            vAgendaHora = AgendaHoraServicio.ConsultarAgendaHora(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vAgendaHora.hora != Int64.MinValue)
                txtHora.Text = HttpUtility.HtmlDecode(vAgendaHora.hora.ToString().Trim());
            if (!string.IsNullOrEmpty(vAgendaHora.tipo))
                ddlTipo.SelectedValue=HttpUtility.HtmlDecode(vAgendaHora.tipo.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AgendaHoraServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
}