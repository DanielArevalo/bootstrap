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
    private Xpinn.ActivosFijos.Services.EstadoActivoservices EstadoServicio = new Xpinn.ActivosFijos.Services.EstadoActivoservices();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[EstadoServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(EstadoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(EstadoServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstadoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[EstadoServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[EstadoServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(EstadoServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtCodigo.Text = Convert.ToString(EstadoServicio.ObtenerSiguienteCodigo((Usuario)Session["Usuario"]));
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstadoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.ActivosFijos.Entities.EstadoActivo vEstado = new Xpinn.ActivosFijos.Entities.EstadoActivo();

            if (idObjeto != "")
                vEstado = EstadoServicio.ConsultarEstadoActivo(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vEstado.estado = Convert.ToInt32(txtCodigo.Text.Trim());
            vEstado.nombre = Convert.ToString(txtDescripcion.Text.Trim());

            if (idObjeto != "")
            {
                vEstado.estado = Convert.ToInt32(idObjeto);
                EstadoServicio.ModificarEstadoActivo(vEstado, (Usuario)Session["usuario"]);
            }
            else
            {
                vEstado = EstadoServicio.CrearEstadoActivo(vEstado, (Usuario)Session["usuario"]);
                idObjeto = vEstado.estado.ToString();
            }

            Session[EstadoServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstadoServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Xpinn.ActivosFijos.Entities.EstadoActivo vEstado = new Xpinn.ActivosFijos.Entities.EstadoActivo();
            vEstado = EstadoServicio.ConsultarEstadoActivo(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vEstado.estado.ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(vEstado.estado.ToString().Trim());
            if (!string.IsNullOrEmpty(vEstado.nombre))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vEstado.nombre.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstadoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


}