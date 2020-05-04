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
    private Xpinn.ActivosFijos.Services.TipoActivoservices TipoServicio = new Xpinn.ActivosFijos.Services.TipoActivoservices();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[TipoServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(TipoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(TipoServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarListas();
                if (Session[TipoServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[TipoServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(TipoServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtCodigo.Text = Convert.ToString(TipoServicio.ObtenerSiguienteCodigo((Usuario)Session["Usuario"]));
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.ActivosFijos.Entities.TipoActivo vTipo = new Xpinn.ActivosFijos.Entities.TipoActivo();

            if (idObjeto != "")
                vTipo = TipoServicio.ConsultarTipoActivo(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vTipo.tipo = Convert.ToInt32(txtCodigo.Text.Trim());
            vTipo.nombre = Convert.ToString(txtDescripcion.Text.Trim());
            vTipo.cod_cuenta_activo = ddlCodCuenta.SelectedValue;
            vTipo.cod_cuenta_depreciacion = ddlCodCuentaDepreciacion.SelectedValue;
            vTipo.cod_cuenta_depreciacion_gasto = ddlCodCuentaDepreciacionGasto.SelectedValue;
            vTipo.cod_cuenta_gasto_venta_baja = ddlCodCuentaGasto.SelectedValue;
            vTipo.cod_cuenta_ingreso_venta_baja = ddlCodCuentaIngreso.SelectedValue;

            if (idObjeto != "")
            {
                vTipo.tipo = Convert.ToInt32(idObjeto);
                TipoServicio.ModificarTipoActivo(vTipo, (Usuario)Session["usuario"]);
            }
            else
            {
                vTipo = TipoServicio.CrearTipoActivo(vTipo, (Usuario)Session["usuario"]);
                idObjeto = vTipo.tipo.ToString();
            }

            Session[TipoServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Xpinn.ActivosFijos.Entities.TipoActivo vTipo = new Xpinn.ActivosFijos.Entities.TipoActivo();
            vTipo = TipoServicio.ConsultarTipoActivo(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vTipo.tipo.ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(vTipo.tipo.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipo.nombre))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vTipo.nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipo.cod_cuenta_activo))
                ddlCodCuenta.SelectedValue = HttpUtility.HtmlDecode(vTipo.cod_cuenta_activo.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipo.cod_cuenta_depreciacion))
                ddlCodCuentaDepreciacion.SelectedValue = HttpUtility.HtmlDecode(vTipo.cod_cuenta_depreciacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipo.cod_cuenta_depreciacion_gasto))
                ddlCodCuentaDepreciacionGasto.SelectedValue = HttpUtility.HtmlDecode(vTipo.cod_cuenta_depreciacion_gasto.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipo.cod_cuenta_gasto_venta_baja))
                ddlCodCuentaGasto.SelectedValue = HttpUtility.HtmlDecode(vTipo.cod_cuenta_gasto_venta_baja.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipo.cod_cuenta_ingreso_venta_baja))
                ddlCodCuentaIngreso.SelectedValue = HttpUtility.HtmlDecode(vTipo.cod_cuenta_ingreso_venta_baja.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void CargarListas()
    {
        Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
        List<Xpinn.Contabilidad.Entities.PlanCuentas> LstPlanCuentas = new List<Xpinn.Contabilidad.Entities.PlanCuentas>();
        Xpinn.Contabilidad.Entities.PlanCuentas pPlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
        LstPlanCuentas = PlanCuentasServicio.ListarPlanCuentasLocal(pPlanCuentas, (Usuario)Session["usuario"], "");

        ddlCodCuenta.DataSource = LstPlanCuentas;
        ddlCodCuenta.DataTextField = "cod_cuenta";
        ddlCodCuenta.DataValueField = "cod_cuenta";
        ddlCodCuenta.DataBind();

        ddlCodCuentaDepreciacion.DataSource = LstPlanCuentas;
        ddlCodCuentaDepreciacion.DataTextField = "cod_cuenta";
        ddlCodCuentaDepreciacion.DataValueField = "cod_cuenta";
        ddlCodCuentaDepreciacion.DataBind();

        ddlCodCuentaDepreciacionGasto.DataSource = LstPlanCuentas;
        ddlCodCuentaDepreciacionGasto.DataTextField = "cod_cuenta";
        ddlCodCuentaDepreciacionGasto.DataValueField = "cod_cuenta";
        ddlCodCuentaDepreciacionGasto.DataBind();

        ddlCodCuentaGasto.DataSource = LstPlanCuentas;
        ddlCodCuentaGasto.DataTextField = "cod_cuenta";
        ddlCodCuentaGasto.DataValueField = "cod_cuenta";
        ddlCodCuentaGasto.DataBind();

        ddlCodCuentaIngreso.DataSource = LstPlanCuentas;
        ddlCodCuentaIngreso.DataTextField = "cod_cuenta";
        ddlCodCuentaIngreso.DataValueField = "cod_cuenta";
        ddlCodCuentaIngreso.DataBind();
    }
}

