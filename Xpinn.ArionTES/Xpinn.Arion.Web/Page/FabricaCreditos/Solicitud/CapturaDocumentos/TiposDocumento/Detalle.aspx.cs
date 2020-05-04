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
    private Xpinn.FabricaCreditos.Services.TiposDocumentoService TiposDocumentoServicio = new Xpinn.FabricaCreditos.Services.TiposDocumentoService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(TiposDocumentoServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TiposDocumentoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[TiposDocumentoServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[TiposDocumentoServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(TiposDocumentoServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(TiposDocumentoServicio.CodigoPrograma, "Page_Load", ex);
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
            TiposDocumentoServicio.EliminarTiposDocumento(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TiposDocumentoServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[TiposDocumentoServicio.CodigoPrograma + ".id"] = idObjeto;
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
            Xpinn.FabricaCreditos.Entities.TiposDocumento vTiposDocumento = new Xpinn.FabricaCreditos.Entities.TiposDocumento();
            vTiposDocumento = TiposDocumentoServicio.ConsultarTiposDocumento(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vTiposDocumento.tipo_documento != Int64.MinValue)
                txtTipo_documento.Text = vTiposDocumento.tipo_documento.ToString().Trim();
            if (!string.IsNullOrEmpty(vTiposDocumento.descripcion))
                txtDescripcion.Text = vTiposDocumento.descripcion.ToString().Trim();
            if (!string.IsNullOrEmpty(vTiposDocumento.tipo))
                txtTipo.Text = vTiposDocumento.tipo.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TiposDocumentoServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }
}