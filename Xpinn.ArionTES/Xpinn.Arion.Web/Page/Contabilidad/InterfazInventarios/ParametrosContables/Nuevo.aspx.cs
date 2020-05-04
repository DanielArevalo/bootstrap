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
using Xpinn.Tesoreria.Entities;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Tesoreria.Services.InventariosServices InventariosServicio = new Xpinn.Tesoreria.Services.InventariosServices();
    private Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[InventariosServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(InventariosServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(InventariosServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventariosServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["DatosImpuesto"] = null;
                mvImpuesto.ActiveViewIndex = 0;
                if (Session[InventariosServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[InventariosServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(InventariosServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(InventariosServicio.CodigoPrograma, "Page_Load", ex);
        }
    }



    Boolean ValidarDatos()
    {
        if (txtCategoria.Text == "")
        {
            VerError("Ingrese el código de la categoria a registrar");
            return false;
        }

        Xpinn.Tesoreria.Entities.ParCueInventarios vParCueInventarios = new Xpinn.Tesoreria.Entities.ParCueInventarios();
        if (idObjeto == "")
        {
            vParCueInventarios = InventariosServicio.ConsultarParCueInventarios(Convert.ToInt64(txtCategoria.Text), (Usuario)Session["usuario"]);
            if (vParCueInventarios != null)
            {
                if (vParCueInventarios.id_categoria != 0)
                {
                    string cod_ant = txtCategoria.Text;
                    VerError("El código " + cod_ant + " para la categoria ya fue registrada anteriormente");
                    return false;
                }
            }
        }

        if (txtDescripcion.Text == "")
        {
            VerError("Ingrese la descripción de la categoria registrar");
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            if (ValidarDatos())
            {
                string msj = idObjeto != "" ? "modificación" : "grabación";
                ctlMensaje.MostrarMensaje("Desea realizar la " + msj + " de la parametrización contable de inventarios.");
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Tesoreria.Entities.ParCueInventarios _Categoria = new Xpinn.Tesoreria.Entities.ParCueInventarios();

            if (idObjeto != "")
                _Categoria = InventariosServicio.ConsultarParCueInventarios(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            _Categoria.id_categoria = Convert.ToInt64(txtCategoria.Text.Trim());
            _Categoria.nombre = Convert.ToString(txtDescripcion.Text.Trim());
            _Categoria.cod_cuenta = txtCodCuenta.Text != "" ? txtCodCuenta.Text : null;
            _Categoria.cod_cuenta_niif = txtCodCuentaNIF.Text != "" ? txtCodCuentaNIF.Text : null;
            _Categoria.cod_cuenta_ingreso = txtCodCuentaIngreso.Text != "" ? txtCodCuentaIngreso.Text : null;
            _Categoria.cod_cuenta_gasto = txtCodCuentaGasto.Text != "" ? txtCodCuentaGasto.Text : null;

            if (_Categoria.idparametro != 0)
            {
                lblMsj.Text = "modificada";
                _Categoria.idparametro = Convert.ToInt64(idObjeto);
                InventariosServicio.ModificarParCueInventarios(_Categoria, (Usuario)Session["usuario"]);
            }
            else
            {
                lblMsj.Text = "grabada";
                _Categoria = InventariosServicio.CrearParCueInventarios(_Categoria, (Usuario)Session["usuario"]);
                idObjeto = _Categoria.idparametro.ToString();
            }

            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            mvImpuesto.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventariosServicio.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Tesoreria.Entities.ParCueInventarios vParCueInventarios = new Xpinn.Tesoreria.Entities.ParCueInventarios();
            vParCueInventarios = InventariosServicio.ConsultarParCueInventarios(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vParCueInventarios.id_categoria.ToString()))
                txtCategoria.Text = HttpUtility.HtmlDecode(vParCueInventarios.id_categoria.ToString().Trim());
            if (!string.IsNullOrEmpty(vParCueInventarios.nombre))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vParCueInventarios.nombre.ToString().Trim());
            if (vParCueInventarios.cod_cuenta != null && vParCueInventarios.cod_cuenta != "")
            {
                txtCodCuenta.Text = vParCueInventarios.cod_cuenta;
                txtCodCuenta_TextChanged(txtCodCuenta, null);
            }
            if (vParCueInventarios.cod_cuenta_niif != null && vParCueInventarios.cod_cuenta_niif != "")
            {
                txtCodCuentaNIF.Text = vParCueInventarios.cod_cuenta_niif;
                txtCodCuentaNIF_TextChanged(txtCodCuentaNIF, null);
            }
            if (vParCueInventarios.cod_cuenta_ingreso != null && vParCueInventarios.cod_cuenta_ingreso != "")
            {
                txtCodCuentaIngreso.Text = vParCueInventarios.cod_cuenta_ingreso;
                txtCodCuentaIngreso_TextChanged(txtCodCuentaIngreso, null);
            }
            if (vParCueInventarios.cod_cuenta_gasto != null && vParCueInventarios.cod_cuenta_gasto != "")
            {
                txtCodCuentaGasto.Text = vParCueInventarios.cod_cuenta_gasto;
                txtCodCuentaGasto_TextChanged(txtCodCuentaGasto, null);
            }

            List<Concepto_CuentasXpagarImp> lstImpuesto = new List<Concepto_CuentasXpagarImp>();
            Concepto_CuentasXpagarImp pImpuesto = new Concepto_CuentasXpagarImp();
            pImpuesto.cod_concepto_fac = Convert.ToInt32(txtCategoria.Text);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventariosServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void txtCodCuenta_TextChanged(object sender, EventArgs e)
    {
        Usuario usur = (Usuario)Session["usuario"];
        Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
        Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
        PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta.Text, (Usuario)Session["usuario"]);

        // Mostrar el nombre de la cuenta           
        if (txtNomCuenta != null)
            txtNomCuenta.Text = PlanCuentas.nombre;
        
        if(PlanCuentas.tipo=="D")
            ddlTipoMov.SelectedValue ="1";
        else
            ddlTipoMov.SelectedValue = "2";

        if (!(usur.codperfil == 1 || usur.administrador == "1"))
            ddlTipoMov.Enabled = false;
    }

    protected void txtCodCuentaNIF_TextChanged(object sender, EventArgs e)
    {
        Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
        Xpinn.NIIF.Entities.PlanCuentasNIIF PlanCuentas = new Xpinn.NIIF.Entities.PlanCuentasNIIF();
        PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentasNIIF(txtCodCuentaNIF.Text, (Usuario)Session["usuario"]);

        // Mostrar el nombre de la cuenta
        if (txtNomCuentaNif != null)
            txtNomCuentaNif.Text = PlanCuentas.nombre;
    }

    protected void txtCodCuentaIngreso_TextChanged(object sender, EventArgs e)
    {
        Usuario usur = (Usuario)Session["usuario"];
        Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
        Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
        PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuentaIngreso.Text, (Usuario)Session["usuario"]);

        // Mostrar el nombre de la cuenta           
        if (txtNomCuentaIngreso != null)
            txtNomCuentaIngreso.Text = PlanCuentas.nombre;
        else
            txtNomCuentaIngreso.Text = "";
    }

    protected void txtCodCuentaGasto_TextChanged(object sender, EventArgs e)
    {
        Usuario usur = (Usuario)Session["usuario"];
        Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
        Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
        PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuentaGasto.Text, (Usuario)Session["usuario"]);

        // Mostrar el nombre de la cuenta           
        if (txtNomCuentaGasto != null)
            txtNomCuentaGasto.Text = PlanCuentas.nombre;
        else
            txtNomCuentaGasto.Text = "";
    }


    protected void btnListadoPlan2_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true, "txtCodCuenta", "txtNomCuenta");
    }

    protected void btnListadoPlanAnticipos_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true, "txtCodCuentaAnticipo", "txtNomCuentaAnticipos");
    }

    protected void btnListadoPlanNIF_Click(object sender, EventArgs e)
    {
        ctlListadoPlanNif.Motrar(true, "txtCodCuentaNIF", "txtNomCuentaNif");
    }

    protected void btnListadoPlanIngreso_Click(object sender, EventArgs e)
    {
        ctlListadoPlanIngreso.Motrar(true, "txtCodCuentaIngreso", "txtNomCuentaIngreso");
    }

    protected void btnListadoPlanGasto_Click(object sender, EventArgs e)
    {
        ctlListadoPlanNif.Motrar(true, "txtCodCuentaGasto", "txtNomCuentaGasto");
    }


}