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
    private Xpinn.Asesores.Services.ProcesosCobroService ProcesosCobroServicio = new Xpinn.Asesores.Services.ProcesosCobroService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ProcesosCobroServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesosCobroServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[ProcesosCobroServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ProcesosCobroServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ProcesosCobroServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(ProcesosCobroServicio.CodigoPrograma, "Page_Load", ex);
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
            ProcesosCobroServicio.EliminarProcesosCobro(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesosCobroServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[ProcesosCobroServicio.CodigoPrograma + ".id"] = idObjeto;
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
            Xpinn.Asesores.Entities.ProcesosCobro vProcesosCobro = new Xpinn.Asesores.Entities.ProcesosCobro();
            vProcesosCobro = ProcesosCobroServicio.ConsultarProcesosCobro(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vProcesosCobro.codprocesocobro != Int64.MinValue)
                txtCodprocesocobro.Text = vProcesosCobro.codprocesocobro.ToString().Trim();
            if (vProcesosCobro.codprocesoprecede != Int64.MinValue)
                txtCodprocesoprecede.Text = vProcesosCobro.codprocesoprecede.ToString().Trim();
            if (!string.IsNullOrEmpty(vProcesosCobro.descripcion))
                txtDescripcion.Text = vProcesosCobro.descripcion.ToString().Trim();
            if (vProcesosCobro.rangoinicial != Int64.MinValue)
                txtRangoinicial.Text = vProcesosCobro.rangoinicial.ToString().Trim();
            if (vProcesosCobro.rangofinal != Int64.MinValue)
                txtRangofinal.Text = vProcesosCobro.rangofinal.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesosCobroServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }
}