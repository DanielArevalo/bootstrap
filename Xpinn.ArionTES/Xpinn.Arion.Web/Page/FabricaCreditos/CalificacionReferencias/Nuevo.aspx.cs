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
    private Xpinn.FabricaCreditos.Services.CalificacionReferenciasService CalificacionReferenciasServicio = new Xpinn.FabricaCreditos.Services.CalificacionReferenciasService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            //if (Session[CalificacionReferenciasServicio.CodigoPrograma + ".id"] != null)
            //    VisualizarOpciones(CalificacionReferenciasServicio.CodigoPrograma, "E");
            //else
            //    VisualizarOpciones(CalificacionReferenciasServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CalificacionReferenciasServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[CalificacionReferenciasServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[CalificacionReferenciasServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(CalificacionReferenciasServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(CalificacionReferenciasServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.CalificacionReferencias vCalificacionReferencias = new Xpinn.FabricaCreditos.Entities.CalificacionReferencias();

            if (idObjeto != "")
                vCalificacionReferencias = CalificacionReferenciasServicio.ConsultarCalificacionReferencias(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            //vCalificacionReferencias.tipocalificacionref = Convert.ToInt64(txtTipocalificacionref.Text.Trim());
            vCalificacionReferencias.nombre = Convert.ToString(txtNombre.Text.Trim());

            if (idObjeto != "")
            {
                vCalificacionReferencias.tipocalificacionref = Convert.ToInt64(idObjeto);
                CalificacionReferenciasServicio.ModificarCalificacionReferencias(vCalificacionReferencias, (Usuario)Session["usuario"]);
            }
            else
            {
                vCalificacionReferencias = CalificacionReferenciasServicio.CrearCalificacionReferencias(vCalificacionReferencias, (Usuario)Session["usuario"]);
                idObjeto = vCalificacionReferencias.tipocalificacionref.ToString();
            }

            Session[CalificacionReferenciasServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CalificacionReferenciasServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Session[CalificacionReferenciasServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.CalificacionReferencias vCalificacionReferencias = new Xpinn.FabricaCreditos.Entities.CalificacionReferencias();
            vCalificacionReferencias = CalificacionReferenciasServicio.ConsultarCalificacionReferencias(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            //if (vCalificacionReferencias.tipocalificacionref != Int64.MinValue)
            //    txtTipocalificacionref.Text = HttpUtility.HtmlDecode(vCalificacionReferencias.tipocalificacionref.ToString().Trim());
            if (!string.IsNullOrEmpty(vCalificacionReferencias.nombre))
                txtNombre.Text = HttpUtility.HtmlDecode(vCalificacionReferencias.nombre.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CalificacionReferenciasServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
}