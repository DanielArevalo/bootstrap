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
    private Xpinn.Contabilidad.Services.ProcesoContableService ProcesoContableServicio = new Xpinn.Contabilidad.Services.ProcesoContableService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ProcesoContableServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesoContableServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[ProcesoContableServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ProcesoContableServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ProcesoContableServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(ProcesoContableServicio.CodigoPrograma, "Page_Load", ex);
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
            //ProcesoContableServicio.EliminarProcesoContable(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesoContableServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[ProcesoContableServicio.CodigoPrograma + ".id"] = idObjeto;
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
            Xpinn.Contabilidad.Entities.ProcesoContable vProcesoContable = new Xpinn.Contabilidad.Entities.ProcesoContable();
            vProcesoContable = ProcesoContableServicio.ConsultarProcesoContable(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            txtCodigo.Text = HttpUtility.HtmlDecode(vProcesoContable.cod_proceso.ToString().Trim());
            if (!string.IsNullOrEmpty(vProcesoContable.cod_cuenta.ToString()))
            {
                ddlNomCuenta.SelectedValue = HttpUtility.HtmlDecode(vProcesoContable.cod_cuenta.ToString().Trim());
                ddlCodCuenta.Text = ddlNomCuenta.SelectedItem.Text;
            }
            if (!string.IsNullOrEmpty(vProcesoContable.tipo_ope.ToString()))
                ddlTipoOpe.SelectedValue = HttpUtility.HtmlDecode(vProcesoContable.tipo_ope.ToString().Trim());
            if (!string.IsNullOrEmpty(vProcesoContable.tipo_comp.ToString()))
                ddlTipoComp.SelectedValue = HttpUtility.HtmlDecode(vProcesoContable.tipo_comp.ToString().Trim());
            if (!string.IsNullOrEmpty(vProcesoContable.fecha_inicial.ToString()))
                txtFechaInicial.Text = HttpUtility.HtmlDecode(vProcesoContable.fecha_inicial.ToString().Trim());
            if (!string.IsNullOrEmpty(vProcesoContable.fecha_final.ToString()))
                txtFechaFinal.Text = HttpUtility.HtmlDecode(vProcesoContable.fecha_final.ToString().Trim());
            if (!string.IsNullOrEmpty(vProcesoContable.concepto.ToString()))
                ddlConcepto.SelectedValue = HttpUtility.HtmlDecode(vProcesoContable.concepto.ToString().Trim());
            if (!string.IsNullOrEmpty(vProcesoContable.cod_cuenta.ToString()))
                ddlCodCuenta.SelectedValue = HttpUtility.HtmlDecode(vProcesoContable.cod_cuenta.ToString().Trim());
            if (!string.IsNullOrEmpty(vProcesoContable.cod_est_det.ToString()))
                ddlEstructura.SelectedValue = HttpUtility.HtmlDecode(vProcesoContable.cod_est_det.ToString().Trim());
            if (!string.IsNullOrEmpty(vProcesoContable.tipo_mov.ToString()))
                ddlTipoMov.SelectedValue = HttpUtility.HtmlDecode(vProcesoContable.tipo_mov.ToString().Trim());
        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(ProcesoContableServicio.CodigoPrograma, "ObtenerDatos", ex);
            VerError(ex.Message);
        }
    }

}