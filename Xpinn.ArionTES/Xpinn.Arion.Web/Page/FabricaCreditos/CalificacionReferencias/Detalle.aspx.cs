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
    private Xpinn.FabricaCreditos.Services.CalificacionReferenciasService CalificacionReferenciasServicio = new Xpinn.FabricaCreditos.Services.CalificacionReferenciasService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            //VisualizarOpciones(CalificacionReferenciasServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CalificacionReferenciasServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[CalificacionReferenciasServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[CalificacionReferenciasServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(CalificacionReferenciasServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(CalificacionReferenciasServicio.CodigoPrograma, "Page_Load", ex);
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
            CalificacionReferenciasServicio.EliminarCalificacionReferencias(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CalificacionReferenciasServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[CalificacionReferenciasServicio.CodigoPrograma + ".id"] = idObjeto;
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
            Xpinn.FabricaCreditos.Entities.CalificacionReferencias vCalificacionReferencias = new Xpinn.FabricaCreditos.Entities.CalificacionReferencias();
            vCalificacionReferencias = CalificacionReferenciasServicio.ConsultarCalificacionReferencias(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vCalificacionReferencias.tipocalificacionref != Int64.MinValue)
                txtTipocalificacionref.Text = vCalificacionReferencias.tipocalificacionref.ToString().Trim();
            if (!string.IsNullOrEmpty(vCalificacionReferencias.nombre))
                txtNombre.Text = vCalificacionReferencias.nombre.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CalificacionReferenciasServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }
}