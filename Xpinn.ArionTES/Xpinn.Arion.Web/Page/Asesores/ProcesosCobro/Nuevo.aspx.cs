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
    private Xpinn.Asesores.Services.ProcesosCobroService ProcesosCobroServicio = new Xpinn.Asesores.Services.ProcesosCobroService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ProcesosCobroServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ProcesosCobroServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(ProcesosCobroServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
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

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Asesores.Entities.ProcesosCobro vProcesosCobro = new Xpinn.Asesores.Entities.ProcesosCobro();

            if (idObjeto != "")
                vProcesosCobro = ProcesosCobroServicio.ConsultarProcesosCobro(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vProcesosCobro.codprocesoprecede = Convert.ToInt64(txtCodprocesoprecede.Text.Trim());
            vProcesosCobro.descripcion = Convert.ToString(txtDescripcion.Text.Trim());
            vProcesosCobro.rangoinicial = Convert.ToInt64(txtRangoinicial.Text.Trim());
            vProcesosCobro.rangofinal = Convert.ToInt64(txtRangofinal.Text.Trim());

            if (idObjeto != "")
            {
                vProcesosCobro.codprocesocobro = Convert.ToInt64(idObjeto);
                ProcesosCobroServicio.ModificarProcesosCobro(vProcesosCobro, (Usuario)Session["usuario"]);
            }
            else
            {
                vProcesosCobro = ProcesosCobroServicio.CrearProcesosCobro(vProcesosCobro, (Usuario)Session["usuario"]);
                idObjeto = vProcesosCobro.codprocesocobro.ToString();
            }

            Session[ProcesosCobroServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesosCobroServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Session[ProcesosCobroServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Asesores.Entities.ProcesosCobro vProcesosCobro = new Xpinn.Asesores.Entities.ProcesosCobro();
            vProcesosCobro = ProcesosCobroServicio.ConsultarProcesosCobro(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vProcesosCobro.codprocesoprecede != Int64.MinValue)
                txtCodprocesoprecede.Text = HttpUtility.HtmlDecode(vProcesosCobro.codprocesoprecede.ToString().Trim());
            if (!string.IsNullOrEmpty(vProcesosCobro.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vProcesosCobro.descripcion.ToString().Trim());
            if (vProcesosCobro.rangoinicial != Int64.MinValue)
                txtRangoinicial.Text = HttpUtility.HtmlDecode(vProcesosCobro.rangoinicial.ToString().Trim());
            if (vProcesosCobro.rangofinal != Int64.MinValue)
                txtRangofinal.Text = HttpUtility.HtmlDecode(vProcesosCobro.rangofinal.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesosCobroServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
}