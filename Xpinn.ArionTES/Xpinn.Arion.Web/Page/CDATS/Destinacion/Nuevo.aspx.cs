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
    private Xpinn.CDATS.Services.DestinacionService DestinacionServicio = new Xpinn.CDATS.Services.DestinacionService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[DestinacionServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(DestinacionServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(DestinacionServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DestinacionServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[DestinacionServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[DestinacionServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(DestinacionServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(DestinacionServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.CDATS.Entities.Destinacion vDestinacion = new Xpinn.CDATS.Entities.Destinacion();

            if (idObjeto != "")
            {        
                vDestinacion = DestinacionServicio.ConsultarDestinacion(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            }

            if (txtCodigo.Text.Trim() != "")
                vDestinacion.cod_destino = Convert.ToInt32(txtCodigo.Text.Trim());
            vDestinacion.descripcion = Convert.ToString(txtNombre.Text.Trim());

            if (idObjeto != "")
            {
                vDestinacion.cod_destino = Convert.ToInt32(idObjeto);
                DestinacionServicio.ModificarDestinacion(vDestinacion, (Usuario)Session["usuario"]);
            }
            else
            {
                vDestinacion = DestinacionServicio.CrearDestinacion(vDestinacion, (Usuario)Session["usuario"]);
                idObjeto = vDestinacion.cod_destino.ToString();
            }

            Session[DestinacionServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DestinacionServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Session[DestinacionServicio.CodigoPrograma + ".id"] = idObjeto;            
        }
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.CDATS.Entities.Destinacion vDestinacion = new Xpinn.CDATS.Entities.Destinacion();
            vDestinacion = DestinacionServicio.ConsultarDestinacion(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            
            txtCodigo.Text = HttpUtility.HtmlDecode(vDestinacion.cod_destino.ToString().Trim());
            if (!string.IsNullOrEmpty(vDestinacion.descripcion))
                txtNombre.Text = HttpUtility.HtmlDecode(vDestinacion.descripcion.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DestinacionServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


}