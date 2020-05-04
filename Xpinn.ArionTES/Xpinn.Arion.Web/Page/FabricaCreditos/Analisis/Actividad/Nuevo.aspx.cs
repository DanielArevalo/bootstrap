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
    private Xpinn.FabricaCreditos.Services.ActividadesServices ActividadServicio = new Xpinn.FabricaCreditos.Services.ActividadesServices();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ActividadServicio.CodigoProgramaActividadEconomica + ".id"] != null)
                VisualizarOpciones(ActividadServicio.CodigoProgramaActividadEconomica, "E");
            else
               VisualizarOpciones(ActividadServicio.CodigoProgramaActividadEconomica, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActividadServicio.CodigoProgramaActividadEconomica, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[ActividadServicio.CodigoProgramaActividadEconomica + ".id"] != null)
                {
                    idObjeto = Session[ActividadServicio.CodigoProgramaActividadEconomica + ".id"].ToString();
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
            BOexcepcion.Throw(ActividadServicio.CodigoProgramaActividadEconomica, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            
            Xpinn.FabricaCreditos.Entities.Actividades vActividad = new Xpinn.FabricaCreditos.Entities.Actividades();

            if (txtCodigo.Text.Trim() == "")
            {
                VerError("Debe digitar el codigo de la actividad");
                return;
            }
            else if (txtDescripcion.Text.Trim()=="")
            {
                VerError("Debe digitar el nombre de la actividad");
                return;
            }
            VerError("");
            if (idObjeto != "")
                vActividad = ActividadServicio.ConsultarActividadEconomicaId(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vActividad.codactividad = Convert.ToString(txtCodigo.Text.Trim());
            vActividad.descripcion = Convert.ToString(txtDescripcion.Text.Trim());

            if (idObjeto != "")
            {
                Xpinn.FabricaCreditos.Entities.Actividades vActividad_ = new Xpinn.FabricaCreditos.Entities.Actividades();
                vActividad_ = ActividadServicio.ConsultarActividadEconomicaId(Convert.ToInt64(txtCodigo.Text), (Usuario)Session["usuario"]);
                if ((vActividad_.codactividad != null) && txtCodigo.Text != idObjeto)
                {
                    VerError("El codigo existe en el sistema");
                    return;
                }
                VerError("");
                vActividad = ActividadServicio.ModificarActividadEconomica(idObjeto, vActividad, (Usuario)Session["usuario"]);
            }
            else
            {
                Xpinn.FabricaCreditos.Entities.Actividades vActividad_ = new Xpinn.FabricaCreditos.Entities.Actividades();
                vActividad_ = ActividadServicio.ConsultarActividadEconomicaId(Convert.ToInt64(txtCodigo.Text), (Usuario)Session["usuario"]);
                if (vActividad_.codactividad != null)
                {
                    VerError("El codigo existe en el sistema");
                    return;
                }
                VerError("");
                ActividadServicio.CrearActividadEconomica(vActividad, (Usuario)Session["usuario"]);
                idObjeto = vActividad.codactividad.ToString();
            }

            Session[ActividadServicio.CodigoProgramaActividadEconomica + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActividadServicio.CodigoProgramaActividadEconomica, "btnGuardar_Click", ex);
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
            Xpinn.FabricaCreditos.Entities.Actividades vActividad = new Xpinn.FabricaCreditos.Entities.Actividades();
            vActividad = ActividadServicio.ConsultarActividadEconomicaId(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            if (!string.IsNullOrEmpty(vActividad.codactividad.ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(vActividad.codactividad.ToString().Trim());
            if (!string.IsNullOrEmpty(vActividad.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vActividad.descripcion.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActividadServicio.CodigoProgramaActividadEconomica, "ObtenerDatos", ex);
        }
    }

}