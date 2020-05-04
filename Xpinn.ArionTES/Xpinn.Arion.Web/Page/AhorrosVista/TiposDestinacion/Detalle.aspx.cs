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
    private Xpinn.Ahorros.Services.DestinacionServices DestinacionServicio = new Xpinn.Ahorros.Services.DestinacionServices();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[DestinacionServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(DestinacionServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(DestinacionServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DestinacionServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[DestinacionServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[DestinacionServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(DestinacionServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtCodigo.Text = Convert.ToString(DestinacionServicio.ObtenerSiguienteCodigo((Usuario)Session["Usuario"]));
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DestinacionServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Ahorros.Entities.Destinacion vEstado = new Xpinn.Ahorros.Entities.Destinacion();
            vEstado = DestinacionServicio.ConsultarDestinacion(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vEstado.cod_destino.ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(vEstado.cod_destino.ToString().Trim());
            if (!string.IsNullOrEmpty(vEstado.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vEstado.descripcion.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DestinacionServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


}