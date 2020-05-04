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
    private Xpinn.FabricaCreditos.Services.IngresosFamiliaService IngresosFamiliaServicio = new Xpinn.FabricaCreditos.Services.IngresosFamiliaService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(IngresosFamiliaServicio.CodigoPrograma, "D");

            Site2 toolBar = (Site2)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(IngresosFamiliaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[IngresosFamiliaServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[IngresosFamiliaServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(IngresosFamiliaServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(IngresosFamiliaServicio.CodigoPrograma, "Page_Load", ex);
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
            IngresosFamiliaServicio.EliminarIngresosFamilia(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(IngresosFamiliaServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[IngresosFamiliaServicio.CodigoPrograma + ".id"] = idObjeto;
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
            Xpinn.FabricaCreditos.Entities.IngresosFamilia vIngresosFamilia = new Xpinn.FabricaCreditos.Entities.IngresosFamilia();
            vIngresosFamilia = IngresosFamiliaServicio.ConsultarIngresosFamilia(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vIngresosFamilia.cod_ingreso != Int64.MinValue)
                txtCod_ingreso.Text = vIngresosFamilia.cod_ingreso.ToString().Trim();
            if (vIngresosFamilia.ingresos != Int64.MinValue)
                txtIngresos.Text = vIngresosFamilia.ingresos.ToString().Trim();
            if (vIngresosFamilia.negocio != Int64.MinValue)
                txtNegocio.Text = vIngresosFamilia.negocio.ToString().Trim();
            if (vIngresosFamilia.conyuge != Int64.MinValue)
                txtConyuge.Text = vIngresosFamilia.conyuge.ToString().Trim();
            if (vIngresosFamilia.hijos != Int64.MinValue)
                txtHijos.Text = vIngresosFamilia.hijos.ToString().Trim();
            if (vIngresosFamilia.arriendos != Int64.MinValue)
                txtArriendos.Text = vIngresosFamilia.arriendos.ToString().Trim();
            if (vIngresosFamilia.pension != Int64.MinValue)
                txtPension.Text = vIngresosFamilia.pension.ToString().Trim();
            if (vIngresosFamilia.otros != Int64.MinValue)
                txtOtros.Text = vIngresosFamilia.otros.ToString().Trim();
            if (vIngresosFamilia.cod_persona != Int64.MinValue)
                txtCod_persona.Text = vIngresosFamilia.cod_persona.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(IngresosFamiliaServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }
}