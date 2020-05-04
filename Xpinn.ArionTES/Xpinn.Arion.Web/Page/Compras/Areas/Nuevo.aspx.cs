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
    private Xpinn.ActivosFijos.Services.Areasservices    AreasServicio = new Xpinn.ActivosFijos.Services.Areasservices();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[AreasServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(AreasServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(AreasServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AreasServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[AreasServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[AreasServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(AreasServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtCodigo.Text = Convert.ToString(AreasServicio.ObtenerSiguienteCodigo((Usuario)Session["Usuario"]));
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AreasServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.ActivosFijos.Entities.Areas vAreas = new Xpinn.ActivosFijos.Entities.Areas();

            if (idObjeto != "")
                vAreas = AreasServicio.ConsultarAreas (Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vAreas.IdArea  = Convert.ToInt32(txtCodigo.Text.Trim());
            vAreas.DescripcionArea  = Convert.ToString(txtDescripcion.Text.Trim());
            vAreas.IdCentroCosto  = Convert.ToInt32(txtCentroCosto.Text.Trim());

            if (idObjeto != "")
            {
                vAreas.IdArea  = Convert.ToInt32(idObjeto);
                AreasServicio.ModificarArea(vAreas, (Usuario)Session["usuario"]);
            }
            else
            {
                vAreas = AreasServicio.CrearArea(vAreas, (Usuario)Session["usuario"]);
                idObjeto = vAreas.IdArea .ToString();
            }

            Session[AreasServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AreasServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Xpinn.ActivosFijos.Entities.Areas  vAreas= new Xpinn.ActivosFijos.Entities.Areas();
            vAreas = AreasServicio.ConsultarAreas (Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vAreas.IdArea .ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(vAreas.IdArea .ToString().Trim());
            if (!string.IsNullOrEmpty(vAreas.DescripcionArea ))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vAreas.DescripcionArea.ToString().Trim());
            if (!string.IsNullOrEmpty(vAreas.IdCentroCosto.ToString()))
                txtCentroCosto.Text = HttpUtility.HtmlDecode(vAreas.IdCentroCosto.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AreasServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


}