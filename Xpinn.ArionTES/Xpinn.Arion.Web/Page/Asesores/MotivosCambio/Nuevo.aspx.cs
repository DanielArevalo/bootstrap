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
    private Xpinn.Asesores.Services.MotivosCambioService MotivosCambioServicio = new Xpinn.Asesores.Services.MotivosCambioService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[MotivosCambioServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(MotivosCambioServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(MotivosCambioServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MotivosCambioServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[MotivosCambioServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[MotivosCambioServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(MotivosCambioServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(MotivosCambioServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Asesores.Entities.MotivosCambio vMotivosCambio = new Xpinn.Asesores.Entities.MotivosCambio();

            if (idObjeto != "")
                vMotivosCambio = MotivosCambioServicio.ConsultarMotivosCambio(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vMotivosCambio.descripcion = Convert.ToString(txtDescripcion.Text.Trim());

            if (idObjeto != "")
            {
                vMotivosCambio.cod_motivo_cambio = Convert.ToInt64(idObjeto);
                MotivosCambioServicio.ModificarMotivosCambio(vMotivosCambio, (Usuario)Session["usuario"]);
            }
            else
            {
                vMotivosCambio = MotivosCambioServicio.CrearMotivosCambio(vMotivosCambio, (Usuario)Session["usuario"]);
                idObjeto = vMotivosCambio.cod_motivo_cambio.ToString();
            }

            Session[MotivosCambioServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MotivosCambioServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Session[MotivosCambioServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Asesores.Entities.MotivosCambio vMotivosCambio = new Xpinn.Asesores.Entities.MotivosCambio();
            vMotivosCambio = MotivosCambioServicio.ConsultarMotivosCambio(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vMotivosCambio.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vMotivosCambio.descripcion.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MotivosCambioServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
}