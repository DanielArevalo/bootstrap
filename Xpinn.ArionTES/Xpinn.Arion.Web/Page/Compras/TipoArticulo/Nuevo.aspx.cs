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
    private Xpinn.ActivosFijos.Services.TipoArticuloservices TipoArticuloservices = new Xpinn.ActivosFijos.Services.TipoArticuloservices();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[TipoArticuloservices.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(TipoArticuloservices.CodigoPrograma, "E");
            else
                VisualizarOpciones(TipoArticuloservices.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoArticuloservices.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[TipoArticuloservices.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[TipoArticuloservices.CodigoPrograma + ".id"].ToString();
                    Session.Remove(TipoArticuloservices.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtCodigo.Text = Convert.ToString(TipoArticuloservices.ObtenerSiguienteCodigo((Usuario)Session["Usuario"]));
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoArticuloservices.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.ActivosFijos.Entities.TipoArticulo vTipoArticulo = new Xpinn.ActivosFijos.Entities.TipoArticulo();

            if (idObjeto != "")
                vTipoArticulo = TipoArticuloservices.ConsultarTipoArticulo(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vTipoArticulo.IdTipo_Articulo   = Convert.ToInt32(txtCodigo.Text.Trim());
            vTipoArticulo.Descripcion   = Convert.ToString(txtDescripcion.Text.Trim());
            vTipoArticulo.Dias_Periodicidad   = Convert.ToInt32(txtDias.Text.Trim());

            if (idObjeto != "")
            {
                vTipoArticulo.IdTipo_Articulo   = Convert.ToInt32(idObjeto);
                TipoArticuloservices.ModificarTipoArticulo(vTipoArticulo, (Usuario)Session["usuario"]);
            }
            else
            {
                vTipoArticulo = TipoArticuloservices .CrearTipoArticulo (vTipoArticulo, (Usuario)Session["usuario"]);
                idObjeto = vTipoArticulo.IdTipo_Articulo  .ToString();
            }

            Session[TipoArticuloservices.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoArticuloservices.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.ActivosFijos.Entities.TipoArticulo vTipoArticulo = new Xpinn.ActivosFijos.Entities.TipoArticulo();
            vTipoArticulo = TipoArticuloservices.ConsultarTipoArticulo  (Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vTipoArticulo.IdTipo_Articulo  .ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(vTipoArticulo.IdTipo_Articulo.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoArticulo.Descripcion ))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vTipoArticulo.Descripcion.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoArticulo.Dias_Periodicidad .ToString()))
                txtDias .Text = HttpUtility.HtmlDecode(vTipoArticulo.Dias_Periodicidad.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoArticuloservices.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


}