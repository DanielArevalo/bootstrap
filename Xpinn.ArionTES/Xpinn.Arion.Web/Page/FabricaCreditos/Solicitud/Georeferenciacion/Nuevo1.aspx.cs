using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.GeoreferenciaService GeoreferenciaServicio = new Xpinn.FabricaCreditos.Services.GeoreferenciaService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[GeoreferenciaServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(GeoreferenciaServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(GeoreferenciaServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GeoreferenciaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[GeoreferenciaServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[GeoreferenciaServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(GeoreferenciaServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(GeoreferenciaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Georeferencia vGeoreferencia = new Xpinn.FabricaCreditos.Entities.Georeferencia();

            if (idObjeto != "")
                vGeoreferencia = GeoreferenciaServicio.ConsultarGeoreferencia(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (txtcodgeoreferencia.Text != "") vGeoreferencia.codgeoreferencia = Convert.ToInt64(txtcodgeoreferencia.Text.Trim());
            if (txtCod_persona.Text != "") vGeoreferencia.cod_persona = Convert.ToInt64(txtCod_persona.Text.Trim());
            //if (txtLatitud.Text != "") vGeoreferencia.latitud = Convert.ToString(txtLatitud.Text.Trim());
            vGeoreferencia.latitud = (txtLatitud.Text != "") ? Convert.ToString(txtLatitud.Text.Trim()) : String.Empty;
            //if (txtLongitud.Text != "") vGeoreferencia.longitud = Convert.ToString(txtLongitud.Text.Trim());
            vGeoreferencia.longitud = (txtLongitud.Text != "") ? Convert.ToString(txtLongitud.Text.Trim()) : String.Empty;
            //if (txtObservaciones.Text != "") vGeoreferencia.observaciones = Convert.ToString(txtObservaciones.Text.Trim());
            vGeoreferencia.observaciones = (txtObservaciones.Text != "") ? Convert.ToString(txtObservaciones.Text.Trim()) : String.Empty;
            if (txtFechacreacion.Text != "") vGeoreferencia.fechacreacion = Convert.ToDateTime(txtFechacreacion.Text.Trim());
            //if (txtUsuariocreacion.Text != "") vGeoreferencia.usuariocreacion = Convert.ToString(txtUsuariocreacion.Text.Trim());
            vGeoreferencia.usuariocreacion = (txtUsuariocreacion.Text != "") ? Convert.ToString(txtUsuariocreacion.Text.Trim()) : String.Empty;
            if (txtFecultmod.Text != "") vGeoreferencia.fecultmod = Convert.ToDateTime(txtFecultmod.Text.Trim());
            //if (txtUsuultmod.Text != "") vGeoreferencia.usuultmod = Convert.ToString(txtUsuultmod.Text.Trim());
            vGeoreferencia.usuultmod = (txtUsuultmod.Text != "") ? Convert.ToString(txtUsuultmod.Text.Trim()) : String.Empty;

            if (idObjeto != "")
            {
                vGeoreferencia.codgeoreferencia = Convert.ToInt64(idObjeto);
                GeoreferenciaServicio.ModificarGeoreferencia(vGeoreferencia, (Usuario)Session["usuario"]);
            }
            else
            {
                vGeoreferencia = GeoreferenciaServicio.CrearGeoreferencia(vGeoreferencia, (Usuario)Session["usuario"]);
                idObjeto = vGeoreferencia.codgeoreferencia.ToString();
            }

            Session[GeoreferenciaServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GeoreferenciaServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Session[GeoreferenciaServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Georeferencia vGeoreferencia = new Xpinn.FabricaCreditos.Entities.Georeferencia();
            vGeoreferencia = GeoreferenciaServicio.ConsultarGeoreferencia(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vGeoreferencia.codgeoreferencia != Int64.MinValue)
                txtcodgeoreferencia.Text = HttpUtility.HtmlDecode(vGeoreferencia.codgeoreferencia.ToString().Trim());
            if (vGeoreferencia.cod_persona != Int64.MinValue)
                txtCod_persona.Text = HttpUtility.HtmlDecode(vGeoreferencia.cod_persona.ToString().Trim());
            if (!string.IsNullOrEmpty(vGeoreferencia.latitud))
                txtLatitud.Text = HttpUtility.HtmlDecode(vGeoreferencia.latitud.ToString().Trim());
            if (!string.IsNullOrEmpty(vGeoreferencia.longitud))
                txtLongitud.Text = HttpUtility.HtmlDecode(vGeoreferencia.longitud.ToString().Trim());
            if (!string.IsNullOrEmpty(vGeoreferencia.observaciones))
                txtObservaciones.Text = HttpUtility.HtmlDecode(vGeoreferencia.observaciones.ToString().Trim());
            if (vGeoreferencia.fechacreacion != DateTime.MinValue)
                txtFechacreacion.Text = HttpUtility.HtmlDecode(vGeoreferencia.fechacreacion.ToShortDateString());
            if (!string.IsNullOrEmpty(vGeoreferencia.usuariocreacion))
                txtUsuariocreacion.Text = HttpUtility.HtmlDecode(vGeoreferencia.usuariocreacion.ToString().Trim());
            if (vGeoreferencia.fecultmod != DateTime.MinValue)
                txtFecultmod.Text = HttpUtility.HtmlDecode(vGeoreferencia.fecultmod.ToShortDateString());
            if (!string.IsNullOrEmpty(vGeoreferencia.usuultmod))
                txtUsuultmod.Text = HttpUtility.HtmlDecode(vGeoreferencia.usuultmod.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GeoreferenciaServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
}