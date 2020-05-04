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
    private Xpinn.FabricaCreditos.Services.GeoreferenciaService GeoreferenciaServicio = new Xpinn.FabricaCreditos.Services.GeoreferenciaService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(GeoreferenciaServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GeoreferenciaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[GeoreferenciaServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[GeoreferenciaServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(GeoreferenciaServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(GeoreferenciaServicio.CodigoPrograma, "Page_Load", ex);
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
            GeoreferenciaServicio.EliminarGeoreferencia(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GeoreferenciaServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[GeoreferenciaServicio.CodigoPrograma + ".id"] = idObjeto;
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
            Xpinn.FabricaCreditos.Entities.Georeferencia vGeoreferencia = new Xpinn.FabricaCreditos.Entities.Georeferencia();
            vGeoreferencia = GeoreferenciaServicio.ConsultarGeoreferencia(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vGeoreferencia.codgeoreferencia != Int64.MinValue)
                txtCodgeoreferencia.Text = vGeoreferencia.codgeoreferencia.ToString().Trim();
            if (vGeoreferencia.cod_persona != Int64.MinValue)
                txtCod_persona.Text = vGeoreferencia.cod_persona.ToString().Trim();
            if (!string.IsNullOrEmpty(vGeoreferencia.latitud))
                txtLatitud.Text = vGeoreferencia.latitud.ToString().Trim();
            if (!string.IsNullOrEmpty(vGeoreferencia.longitud))
                txtLongitud.Text = vGeoreferencia.longitud.ToString().Trim();
            if (!string.IsNullOrEmpty(vGeoreferencia.observaciones))
                txtObservaciones.Text = vGeoreferencia.observaciones.ToString().Trim();
            if (vGeoreferencia.fechacreacion != DateTime.MinValue)
                txtFechacreacion.Text = vGeoreferencia.fechacreacion.ToShortDateString();
            if (!string.IsNullOrEmpty(vGeoreferencia.usuariocreacion))
                txtUsuariocreacion.Text = vGeoreferencia.usuariocreacion.ToString().Trim();
            if (vGeoreferencia.fecultmod != DateTime.MinValue)
                txtFecultmod.Text = vGeoreferencia.fecultmod.ToShortDateString();
            if (!string.IsNullOrEmpty(vGeoreferencia.usuultmod))
                txtUsuultmod.Text = vGeoreferencia.usuultmod.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GeoreferenciaServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}