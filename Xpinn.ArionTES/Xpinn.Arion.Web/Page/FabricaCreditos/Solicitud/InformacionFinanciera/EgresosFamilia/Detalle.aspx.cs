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

partial class Detalle : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.EgresosFamiliaService EgresosFamiliaServicio = new Xpinn.FabricaCreditos.Services.EgresosFamiliaService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(EgresosFamiliaServicio.CodigoPrograma, "D");

            Site2 toolBar = (Site2)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EgresosFamiliaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[EgresosFamiliaServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[EgresosFamiliaServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(EgresosFamiliaServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(EgresosFamiliaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            EgresosFamiliaServicio.EliminarEgresosFamilia(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EgresosFamiliaServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[EgresosFamiliaServicio.CodigoPrograma + ".id"] = idObjeto;
        Navegar(Pagina.Nuevo);
    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.EgresosFamilia vEgresosFamilia = new Xpinn.FabricaCreditos.Entities.EgresosFamilia();
            vEgresosFamilia = EgresosFamiliaServicio.ConsultarEgresosFamilia(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vEgresosFamilia.cod_egreso != Int64.MinValue)
                txtCod_egreso.Text = vEgresosFamilia.cod_egreso.ToString().Trim();
            if (vEgresosFamilia.cod_persona != Int64.MinValue)
                txtCod_persona.Text = vEgresosFamilia.cod_persona.ToString().Trim();
            if (vEgresosFamilia.egresos != Int64.MinValue)
                txtEgresos.Text = vEgresosFamilia.egresos.ToString().Trim();
            if (vEgresosFamilia.alimentacion != Int64.MinValue)
                txtAlimentacion.Text = vEgresosFamilia.alimentacion.ToString().Trim();
            if (vEgresosFamilia.vivienda != Int64.MinValue)
                txtVivienda.Text = vEgresosFamilia.vivienda.ToString().Trim();
            if (vEgresosFamilia.educacion != Int64.MinValue)
                txtEducacion.Text = vEgresosFamilia.educacion.ToString().Trim();
            if (vEgresosFamilia.serviciospublicos != Int64.MinValue)
                txtServiciospublicos.Text = vEgresosFamilia.serviciospublicos.ToString().Trim();
            if (vEgresosFamilia.transporte != Int64.MinValue)
                txtTransporte.Text = vEgresosFamilia.transporte.ToString().Trim();
            if (vEgresosFamilia.pagodeudas != Int64.MinValue)
                txtPagodeudas.Text = vEgresosFamilia.pagodeudas.ToString().Trim();
            if (vEgresosFamilia.otros != Int64.MinValue)
                txtOtros.Text = vEgresosFamilia.otros.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EgresosFamiliaServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }
}