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
    private Xpinn.Contabilidad.Services.TipoImpuestoService TipoImpuestoServicio = new Xpinn.Contabilidad.Services.TipoImpuestoService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(TipoImpuestoServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoImpuestoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[TipoImpuestoServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[TipoImpuestoServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(TipoImpuestoServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(TipoImpuestoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Contabilidad.Entities.TipoImpuesto vTipoImpuesto = new Xpinn.Contabilidad.Entities.TipoImpuesto();
            vTipoImpuesto = TipoImpuestoServicio.ConsultarTipoImpuesto(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            txtCodigo.Text = vTipoImpuesto.cod_tipo_impuesto.ToString().Trim();
            if (!string.IsNullOrEmpty(vTipoImpuesto.nombre_impuesto))
                txtNombre.Text = vTipoImpuesto.nombre_impuesto.ToString().Trim();
            if (!string.IsNullOrEmpty(vTipoImpuesto.descripcion))
                txtDescripcion.Text = vTipoImpuesto.descripcion.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoImpuestoServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }

}