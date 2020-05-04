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
    private Xpinn.CDATS.Services.FormaCaptacionService FormaCaptacionServicio = new Xpinn.CDATS.Services.FormaCaptacionService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[FormaCaptacionServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(FormaCaptacionServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(FormaCaptacionServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(FormaCaptacionServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[FormaCaptacionServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[FormaCaptacionServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(FormaCaptacionServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(FormaCaptacionServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.CDATS.Entities.FormaCaptacion vFormaCaptacion = new Xpinn.CDATS.Entities.FormaCaptacion();

            if (idObjeto != "")
            {        
                vFormaCaptacion = FormaCaptacionServicio.ConsultarFormaCaptacion(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            }

            if (txtCodigo.Text.Trim() != "")
                vFormaCaptacion.codforma_captacion = Convert.ToInt32(txtCodigo.Text.Trim());
            vFormaCaptacion.descripcion = Convert.ToString(txtNombre.Text.Trim());

            if (idObjeto != "")
            {
                vFormaCaptacion.codforma_captacion = Convert.ToInt32(idObjeto);
                FormaCaptacionServicio.ModificarFormaCaptacion(vFormaCaptacion, (Usuario)Session["usuario"]);
            }
            else
            {
                vFormaCaptacion = FormaCaptacionServicio.CrearFormaCaptacion(vFormaCaptacion, (Usuario)Session["usuario"]);
                idObjeto = vFormaCaptacion.codforma_captacion.ToString();
            }

            Session[FormaCaptacionServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(FormaCaptacionServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (idObjeto != "")
        {
            Session[FormaCaptacionServicio.CodigoPrograma + ".id"] = idObjeto;            
        }
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.CDATS.Entities.FormaCaptacion vFormaCaptacion = new Xpinn.CDATS.Entities.FormaCaptacion();
            vFormaCaptacion = FormaCaptacionServicio.ConsultarFormaCaptacion(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            txtCodigo.Text = HttpUtility.HtmlDecode(vFormaCaptacion.codforma_captacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vFormaCaptacion.descripcion))
                txtNombre.Text = HttpUtility.HtmlDecode(vFormaCaptacion.descripcion.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(FormaCaptacionServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


}