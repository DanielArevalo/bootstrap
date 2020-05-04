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
    private Xpinn.Tesoreria.Services.ConceptoCtaService ConceptoCtaServicio = new Xpinn.Tesoreria.Services.ConceptoCtaService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ConceptoCtaServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ConceptoCtaServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(ConceptoCtaServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;            
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConceptoCtaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[ConceptoCtaServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ConceptoCtaServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ConceptoCtaServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(ConceptoCtaServicio.CodigoPrograma, "Page_Load", ex);
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
            Xpinn.Tesoreria.Entities.ConceptoCta vConceptoComp = new Xpinn.Tesoreria.Entities.ConceptoCta();
            vConceptoComp = ConceptoCtaServicio.ConsultarConceptoCta(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vConceptoComp.cod_concepto_fac.ToString()))
                txtConceptoCta.Text = HttpUtility.HtmlDecode(vConceptoComp.cod_concepto_fac.ToString().Trim());
            if (!string.IsNullOrEmpty(vConceptoComp.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vConceptoComp.descripcion.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConceptoCtaServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

}