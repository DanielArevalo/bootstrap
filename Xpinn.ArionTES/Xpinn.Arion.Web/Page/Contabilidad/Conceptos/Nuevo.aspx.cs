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
    private Xpinn.Contabilidad.Services.ConceptoService ConceptoServicio = new Xpinn.Contabilidad.Services.ConceptoService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ConceptoServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ConceptoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(ConceptoServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
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
                    txtCodigo.Enabled = false;
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

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Contabilidad.Entities.Concepto vConcepto = new Xpinn.Contabilidad.Entities.Concepto();

            if (idObjeto != "")
            {        
                vConcepto = ConceptoServicio.ConsultarConcepto(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            }

            if (txtCodigo.Text.Trim() != "")
                vConcepto.concepto = Convert.ToInt64(txtCodigo.Text.Trim());
            vConcepto.descripcion = Convert.ToString(txtNombre.Text.Trim());

            if (idObjeto != "")
            {
                vConcepto.concepto = Convert.ToInt64(idObjeto);
                ConceptoServicio.ModificarConcepto(vConcepto, (Usuario)Session["usuario"]);
            }
            else
            {
                vConcepto = ConceptoServicio.CrearConcepto(vConcepto, (Usuario)Session["usuario"]);
                idObjeto = vConcepto.concepto.ToString();
            }

            Session[ConceptoServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConceptoServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Session[ConceptoServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Contabilidad.Entities.Concepto vConcepto = new Xpinn.Contabilidad.Entities.Concepto();
            vConcepto = ConceptoServicio.ConsultarConcepto(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            
            txtCodigo.Text = HttpUtility.HtmlDecode(vConcepto.concepto.ToString().Trim());
            if (!string.IsNullOrEmpty(vConcepto.descripcion))
                txtNombre.Text = HttpUtility.HtmlDecode(vConcepto.descripcion.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConceptoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


}