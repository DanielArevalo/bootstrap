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
    private Xpinn.FabricaCreditos.Services.TiposDocumentoService TiposDocumentoServicio = new Xpinn.FabricaCreditos.Services.TiposDocumentoService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[TiposDocumentoServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(TiposDocumentoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(TiposDocumentoServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
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

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.TiposDocumento vTiposDocumento = new Xpinn.FabricaCreditos.Entities.TiposDocumento();

            if (idObjeto != "")
                vTiposDocumento = TiposDocumentoServicio.ConsultarTiposDocumento(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vTiposDocumento.tipo_documento = Convert.ToInt64(txtTipo_documento.Text.Trim());
            vTiposDocumento.descripcion = Convert.ToString(txtDescripcion.Text.Trim());
            vTiposDocumento.tipo = Convert.ToString(txtTipo.Text.Trim());

            if (idObjeto != "")
            {
                vTiposDocumento.tipo_documento = Convert.ToInt64(idObjeto);
                TiposDocumentoServicio.ModificarTiposDocumento(vTiposDocumento, (Usuario)Session["usuario"]);
            }
            else
            {
                vTiposDocumento = TiposDocumentoServicio.CrearTiposDocumento(vTiposDocumento, (Usuario)Session["usuario"]);
                idObjeto = vTiposDocumento.tipo_documento.ToString();
            }

            Session[TiposDocumentoServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TiposDocumentoServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Session[TiposDocumentoServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.TiposDocumento vTiposDocumento = new Xpinn.FabricaCreditos.Entities.TiposDocumento();
            vTiposDocumento = TiposDocumentoServicio.ConsultarTiposDocumento(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vTiposDocumento.tipo_documento != Int64.MinValue)
                txtTipo_documento.Text = HttpUtility.HtmlDecode(vTiposDocumento.tipo_documento.ToString().Trim());
            if (!string.IsNullOrEmpty(vTiposDocumento.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vTiposDocumento.descripcion.ToString().Trim());
            if (!string.IsNullOrEmpty(vTiposDocumento.tipo))
                txtTipo.Text = HttpUtility.HtmlDecode(vTiposDocumento.tipo.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TiposDocumentoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
}