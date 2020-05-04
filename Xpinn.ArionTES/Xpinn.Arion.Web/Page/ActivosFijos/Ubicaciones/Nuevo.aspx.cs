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
    private Xpinn.ActivosFijos.Services.UbicacionActivoservices UbicacionServicio = new Xpinn.ActivosFijos.Services.UbicacionActivoservices();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[UbicacionServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(UbicacionServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(UbicacionServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(UbicacionServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[UbicacionServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[UbicacionServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(UbicacionServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtCodigo.Text = Convert.ToString(UbicacionServicio.ObtenerSiguienteCodigo((Usuario)Session["Usuario"]));
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(UbicacionServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.ActivosFijos.Entities.UbicacionActivo vUbicacion = new Xpinn.ActivosFijos.Entities.UbicacionActivo();

            if (idObjeto != "")
                vUbicacion = UbicacionServicio.ConsultarUbicacionActivo(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vUbicacion.cod_ubica = Convert.ToInt32(txtCodigo.Text.Trim());
            vUbicacion.nombre = Convert.ToString(txtDescripcion.Text.Trim());

            if (idObjeto != "")
            {
                vUbicacion.cod_ubica = Convert.ToInt32(idObjeto);
                UbicacionServicio.ModificarUbicacionActivo(vUbicacion, (Usuario)Session["usuario"]);
            }
            else
            {
                vUbicacion = UbicacionServicio.CrearUbicacionActivo(vUbicacion, (Usuario)Session["usuario"]);
                idObjeto = vUbicacion.cod_ubica.ToString();
            }

            Session[UbicacionServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(UbicacionServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Xpinn.ActivosFijos.Entities.UbicacionActivo vUbicacion = new Xpinn.ActivosFijos.Entities.UbicacionActivo();
            vUbicacion = UbicacionServicio.ConsultarUbicacionActivo(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vUbicacion.cod_ubica.ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(vUbicacion.cod_ubica.ToString().Trim());
            if (!string.IsNullOrEmpty(vUbicacion.nombre))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vUbicacion.nombre.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(UbicacionServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


}