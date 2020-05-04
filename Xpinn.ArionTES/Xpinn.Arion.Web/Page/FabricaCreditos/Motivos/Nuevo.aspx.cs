using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;

public partial class Nuevo : GlobalWeb
{
    MotivoService motivoServicio = new MotivoService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[motivoServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(motivoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(motivoServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(motivoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[motivoServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[motivoServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(motivoServicio.CodigoPrograma + ".id");
                    //ObtenerDatos(idObjeto);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(motivoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //if (idObjeto != "")
            //    oficina = oficinaService.ConsultarOficina(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            Motivo motivo = new Motivo();
            ////se atrapan los datos del formulario
            motivo.Descripcion = txtMotivo.Text;
            motivo.Tipo = ddlTipo.SelectedValue;
            //if (idObjeto != "")
            //{
            //    motivo.CodigoMotivo = Convert.ToInt16(idObjeto);
            //    motivoServicio.ModificarAprobador(aprobador, (Usuario)Session["usuario"]);
            //}
            //else
            //{
            motivo = motivoServicio.CrearMotivo(motivo, (Usuario)Session["usuario"]);
            //idObjeto = motivo.CodigoMotivo.ToString();
            //}

            //Session[motivoServicio.Codigo + ".id"] = idObjeto;

            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(motivoServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {

        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            //Session[oficinaService.CodigoOficina + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, motivoServicio.CodigoPrograma);
    }
}