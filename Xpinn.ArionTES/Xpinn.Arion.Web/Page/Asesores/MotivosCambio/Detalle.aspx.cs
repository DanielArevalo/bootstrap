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
    private Xpinn.Asesores.Services.MotivosCambioService MotivosCambioServicio = new Xpinn.Asesores.Services.MotivosCambioService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(MotivosCambioServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MotivosCambioServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[MotivosCambioServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[MotivosCambioServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(MotivosCambioServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(MotivosCambioServicio.CodigoPrograma, "Page_Load", ex);
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
            MotivosCambioServicio.EliminarMotivosCambio(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MotivosCambioServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[MotivosCambioServicio.CodigoPrograma + ".id"] = idObjeto;
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
            Xpinn.Asesores.Entities.MotivosCambio vMotivosCambio = new Xpinn.Asesores.Entities.MotivosCambio();
            vMotivosCambio = MotivosCambioServicio.ConsultarMotivosCambio(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vMotivosCambio.cod_motivo_cambio != Int64.MinValue)
                txtCod_motivo_cambio.Text = vMotivosCambio.cod_motivo_cambio.ToString().Trim();
            if (!string.IsNullOrEmpty(vMotivosCambio.descripcion))
                txtDescripcion.Text = vMotivosCambio.descripcion.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MotivosCambioServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }
}