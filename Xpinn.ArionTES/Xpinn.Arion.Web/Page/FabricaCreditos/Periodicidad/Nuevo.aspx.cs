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
    private Xpinn.FabricaCreditos.Services.PeriodicidadService PeriodicidadServicio = new Xpinn.FabricaCreditos.Services.PeriodicidadService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[PeriodicidadServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(PeriodicidadServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(PeriodicidadServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PeriodicidadServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[PeriodicidadServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[PeriodicidadServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(PeriodicidadServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(PeriodicidadServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Periodicidad vPeriodicidad = new Xpinn.FabricaCreditos.Entities.Periodicidad();

            if (idObjeto != "")
                vPeriodicidad = PeriodicidadServicio.ConsultarPeriodicidad(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vPeriodicidad.Codigo = Convert.ToInt32(txtCodPeriodicidad.Text.Trim());
            vPeriodicidad.Descripcion = Convert.ToString(txtDescripcion.Text.Trim());
            vPeriodicidad.numero_dias = Convert.ToInt32(txtNumeroDias.Text.Trim());
            vPeriodicidad.numero_meses = Convert.ToInt32(txtNumeroMeses.Text.Trim());
            vPeriodicidad.periodos_anuales = Convert.ToInt32(txtPeriodosAnuales.Text.Trim());
            vPeriodicidad.tipo_calendario = Convert.ToInt32(ddlTipoCalendario.SelectedValue);

            if (idObjeto != "")
            {
                vPeriodicidad.Codigo = Convert.ToInt32(idObjeto);
                PeriodicidadServicio.ModificarPeriodicidad(vPeriodicidad, (Usuario)Session["usuario"]);
            }
            else
            {
                vPeriodicidad = PeriodicidadServicio.CrearPeriodicidad(vPeriodicidad, (Usuario)Session["usuario"]);
                idObjeto = vPeriodicidad.Codigo.ToString();
            }

            Session[PeriodicidadServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PeriodicidadServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Session[PeriodicidadServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Periodicidad vPeriodicidad = new Xpinn.FabricaCreditos.Entities.Periodicidad();
            vPeriodicidad = PeriodicidadServicio.ConsultarPeriodicidad(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vPeriodicidad.Codigo.ToString()))
                txtCodPeriodicidad.Text = HttpUtility.HtmlDecode(vPeriodicidad.Codigo.ToString().Trim());
            if (!string.IsNullOrEmpty(vPeriodicidad.Descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vPeriodicidad.Descripcion.ToString().Trim());
            if (!string.IsNullOrEmpty(vPeriodicidad.numero_dias.ToString()))
                txtNumeroDias.Text = HttpUtility.HtmlDecode(vPeriodicidad.numero_dias.ToString().Trim());
            if (!string.IsNullOrEmpty(vPeriodicidad.numero_dias.ToString()))
                txtNumeroMeses.Text = HttpUtility.HtmlDecode(vPeriodicidad.numero_meses.ToString().Trim());
            if (!string.IsNullOrEmpty(vPeriodicidad.numero_dias.ToString()))
                txtPeriodosAnuales.Text = HttpUtility.HtmlDecode(vPeriodicidad.periodos_anuales.ToString().Trim());
            if (!string.IsNullOrEmpty(vPeriodicidad.tipo_calendario.ToString()))
                ddlTipoCalendario.SelectedValue = HttpUtility.HtmlDecode(vPeriodicidad.tipo_calendario.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PeriodicidadServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


}