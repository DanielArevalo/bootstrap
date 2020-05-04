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
    private Xpinn.Aportes.Services.MotivoExcepcionService MotivoExcepcion = new Xpinn.Aportes.Services.MotivoExcepcionService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[MotivoExcepcion.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(MotivoExcepcion.CodigoPrograma, "E");
            else
                VisualizarOpciones(MotivoExcepcion.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MotivoExcepcion.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[MotivoExcepcion.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[MotivoExcepcion.CodigoPrograma + ".id"].ToString();
                    Session.Remove(MotivoExcepcion.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(MotivoExcepcion.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Aportes.Entities.MotivoExcepcion vMotivosCambio = new Xpinn.Aportes.Entities.MotivoExcepcion();

            if (idObjeto != "")
                vMotivosCambio = MotivoExcepcion.ConsultarMotivoExcepcion(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            vMotivosCambio.cod_motivo_excepcion = Convert.ToInt32(txtcod_motivo_excepcion.Text.Trim()); ;
            vMotivosCambio.descripcion = Convert.ToString(txtDescripcion.Text.Trim());

            if (idObjeto != "")
            {
                vMotivosCambio.cod_motivo_excepcion = Convert.ToInt32(idObjeto);
                MotivoExcepcion.ModificarMotivoExcepcion(vMotivosCambio, (Usuario)Session["usuario"]);
            }
            else
            {
                vMotivosCambio = MotivoExcepcion.CrearMotivoExcepcion(vMotivosCambio, (Usuario)Session["usuario"]);
                idObjeto = vMotivosCambio.cod_motivo_excepcion.ToString();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MotivoExcepcion.CodigoPrograma, "btnGuardar_Click", ex);
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
            Navegar(Pagina.Lista);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Aportes.Entities.MotivoExcepcion vMotivosCambio = new Xpinn.Aportes.Entities.MotivoExcepcion();
            vMotivosCambio = MotivoExcepcion.ConsultarMotivoExcepcion(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vMotivosCambio.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vMotivosCambio.descripcion.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MotivoExcepcion.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
}