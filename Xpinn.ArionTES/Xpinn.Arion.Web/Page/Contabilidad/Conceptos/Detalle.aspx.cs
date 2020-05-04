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
    private Xpinn.Contabilidad.Services.ConceptoService ConceptoServicio = new Xpinn.Contabilidad.Services.ConceptoService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ConceptoServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConceptoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[ConceptoServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ConceptoServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ConceptoServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(ConceptoServicio.CodigoPrograma, "Page_Load", ex);
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
            Xpinn.Contabilidad.Entities.Concepto vConcepto = new Xpinn.Contabilidad.Entities.Concepto();
            vConcepto = ConceptoServicio.ConsultarConcepto(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            txtCodigo.Text = vConcepto.concepto.ToString().Trim();
            if (!string.IsNullOrEmpty(vConcepto.descripcion))
                txtNombre.Text = vConcepto.descripcion.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConceptoServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }

}