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
    private Xpinn.Ahorros.Services.MotivoNovedadServices MotivoNovedadServicio = new Xpinn.Ahorros.Services.MotivoNovedadServices();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[MotivoNovedadServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(MotivoNovedadServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(MotivoNovedadServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MotivoNovedadServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[MotivoNovedadServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[MotivoNovedadServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(MotivoNovedadServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtCodigo.Text = Convert.ToString(MotivoNovedadServicio.ObtenerSiguienteCodigo((Usuario)Session["Usuario"]));
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MotivoNovedadServicio.CodigoPrograma, "Page_Load", ex);
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
            Xpinn.Ahorros.Entities.MotivoNovedad vEstado = new Xpinn.Ahorros.Entities.MotivoNovedad();
            vEstado = MotivoNovedadServicio.ConsultarMotivoNovedad(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vEstado.idmotivo_novedad.ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(vEstado.idmotivo_novedad.ToString().Trim());
            if (!string.IsNullOrEmpty(vEstado.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vEstado.descripcion.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MotivoNovedadServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


}