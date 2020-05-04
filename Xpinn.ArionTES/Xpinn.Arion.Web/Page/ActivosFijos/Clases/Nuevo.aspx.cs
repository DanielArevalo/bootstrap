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
    private Xpinn.ActivosFijos.Services.ClaseActivoservices ClaseServicio = new Xpinn.ActivosFijos.Services.ClaseActivoservices();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ClaseServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ClaseServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(ClaseServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ClaseServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[ClaseServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ClaseServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ClaseServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtCodigo.Text = Convert.ToString(ClaseServicio.ObtenerSiguienteCodigo((Usuario)Session["Usuario"]));
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ClaseServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.ActivosFijos.Entities.ClaseActivo vClase = new Xpinn.ActivosFijos.Entities.ClaseActivo();

            if (idObjeto != "")
                vClase = ClaseServicio.ConsultarClaseActivo(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vClase.clase = Convert.ToInt32(txtCodigo.Text.Trim());
            vClase.nombre = Convert.ToString(txtDescripcion.Text.Trim());

            if (idObjeto != "")
            {
                vClase.clase = Convert.ToInt32(idObjeto);
                ClaseServicio.ModificarClaseActivo(vClase, (Usuario)Session["usuario"]);
            }
            else
            {
                vClase = ClaseServicio.CrearClaseActivo(vClase, (Usuario)Session["usuario"]);
                idObjeto = vClase.clase.ToString();
            }

            Session[ClaseServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ClaseServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Xpinn.ActivosFijos.Entities.ClaseActivo vClase = new Xpinn.ActivosFijos.Entities.ClaseActivo();
            vClase = ClaseServicio.ConsultarClaseActivo(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vClase.clase.ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(vClase.clase.ToString().Trim());
            if (!string.IsNullOrEmpty(vClase.nombre))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vClase.nombre.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ClaseServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


}