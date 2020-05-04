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

using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;

partial class Nuevo : GlobalWeb
{
    private Par_Cue_OtrosService ParametroService = new Par_Cue_OtrosService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[ParametroService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ParametroService.CodigoPrograma, "E");
            else
                VisualizarOpciones(ParametroService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParametroService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvParametros.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;
                CargarListas();
                if (Session[ParametroService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ParametroService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ParametroService.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtCodigo.Text = "Autogenerado";
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParametroService.CodigoPrograma, "Page_Load", ex);
        }
    }


    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, ctlListarCodigo ctlControlListar)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ctlControlListar.LimpiarControl();

        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);

        ctlControlListar.TextField = "descripcion";
        ctlControlListar.ValueField = "idconsecutivo";
        ctlControlListar.BindearControl(plista);
    }


    private void CargarListas()
    {
        try
        {
            PoblarLista("tipoimpuesto", "", "", "", ddlTipoImpuesto);
            PoblarLista("ESTRUCTURA_DETALLE", ddlEstructura);

            ddlTipoMov.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlTipoMov.Items.Insert(1, new ListItem("Débito", "1"));
            ddlTipoMov.Items.Insert(2, new ListItem("Crédito", "2"));
            ddlTipoMov.SelectedIndex = 0;
            ddlTipoMov.DataBind();

            //PoblarLista("tipo_tran", "", " (tipo_producto Not In (1, 2, 3, 4, 6, 9, 12) Or (tipo_producto = 2 And tipo_caja = 0) Or tipo_tran In (402, 403, 981,225,226)) ", "", ctlListarCodigoTransaccion);
            PoblarLista("tipo_tran", "", " ", "", ctlListarCodigoTransaccion);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }



    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Par_Cue_Otros pConsulta = new Par_Cue_Otros();
            pConsulta = ParametroService.ConsultarParametroCtasOtros(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);

            if (pConsulta.idparametro != 0)
                txtCodigo.Text = pConsulta.idparametro.ToString();

            if (pConsulta.cod_est_det != 0)
                ddlEstructura.SelectedValue = pConsulta.cod_est_det.ToString();
            if (pConsulta.cod_cuenta != "" && pConsulta.cod_cuenta != null)
                txtCodCuenta.Text = pConsulta.cod_cuenta;

            txtCodCuenta_TextChanged(txtCodCuenta, null);

            if (pConsulta.tipo_mov != 0)
                ddlTipoMov.SelectedValue = pConsulta.tipo_mov.ToString();
            if (pConsulta.tipo_tran != 0)
                ctlListarCodigoTransaccion.SelectedValue(pConsulta.tipo_tran.ToString());
            if (pConsulta.tipo_impuesto != null)
                ddlTipoImpuesto.SelectedValue = pConsulta.tipo_impuesto.ToString();

        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void txtCodCuenta_TextChanged(object sender, EventArgs e)
    {
        if (txtCodCuenta.Text != "")
        {
            Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
            Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta.Text, (Usuario)Session["usuario"]);

            // Mostrar el nombre de la cuenta           
            if (txtNomCuenta != null)
                txtNomCuenta.Text = PlanCuentas.nombre;
        }
        else
        {
            txtNomCuenta.Text = "";
        }
    }


    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true, "txtCodCuenta", "txtNomCuenta");
    }


    Boolean validarDatos()
    {
        VerError("");

        if (txtCodCuenta.Text == "")
        {
            VerError("Seleccione una Cuenta Contable");
            return false;
        }
        if (ddlTipoMov.SelectedIndex == 0)
        {
            VerError("Seleccione el Tipo de Movimiento");
            return false;
        }

        if (string.IsNullOrWhiteSpace(ctlListarCodigoTransaccion.Codigo))
        {
            VerError("Seleccione el tipo de transacción");
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (validarDatos())
            {
                Par_Cue_Otros vData = new Par_Cue_Otros();

                if (idObjeto != "")
                {
                    vData.idparametro = Convert.ToInt32(txtCodigo.Text.Trim());
                }
                else
                {
                    vData.idparametro = 0;
                }

                if (ddlEstructura.SelectedItem != null && ddlEstructura.SelectedIndex != 0)
                    vData.cod_est_det = Convert.ToInt32(ddlEstructura.SelectedValue);
                else
                    vData.cod_est_det = 0;

                // CODIGO CUENTA LOCAL
                if (txtCodCuenta.Text != "" && txtNomCuenta.Text != "")
                    vData.cod_cuenta = txtCodCuenta.Text;
                else
                    vData.cod_cuenta = null;

                if (ddlTipoImpuesto.SelectedItem != null && ddlTipoImpuesto.SelectedIndex != 0)
                    vData.tipo_impuesto = Convert.ToInt32(ddlTipoImpuesto.SelectedValue);
                // TIPO DE TRANSACCION
                vData.tipo_tran = 0;
                vData.tipo_mov = Convert.ToInt32(ddlTipoMov.SelectedValue);
                if (!string.IsNullOrWhiteSpace(ctlListarCodigoTransaccion.Codigo))
                        vData.tipo_tran = Convert.ToInt32(ctlListarCodigoTransaccion.Codigo);
                vData.cod_cuenta_niif = txtCodCuentaNIF.Text.Trim() != "" ? txtCodCuentaNIF.Text : null;

                if (idObjeto != "")
                {
                    ParametroService.CrearPar_Cue_Otros(vData, (Usuario)Session["usuario"], 2); // MODIFICAR
                }
                else
                {
                    ParametroService.CrearPar_Cue_Otros(vData, (Usuario)Session["usuario"], 1);//CREAR    
                    if (Session["Proceso"] !=null)

                    {
                        Xpinn.Caja.Entities.TipoOperacion vOpe = new Xpinn.Caja.Entities.TipoOperacion();
                        Xpinn.Caja.Services.TipoOperacionService objOpercion = new Xpinn.Caja.Services.TipoOperacionService();
                        vOpe = (Xpinn.Caja.Entities.TipoOperacion)Session["Proceso"];
                        vOpe.tipo_tran=vData.tipo_tran;
                        objOpercion.ModificaTipoOpServices(vOpe, (Usuario)Session["usuario"]);
                        Session["Proceso"] = null;
                    }                
                }

                string msj = idObjeto != "" ? "Modifico" : "Grabo";
                lblmsj.Text = "Se " + msj + " Correctamente los Datos";
                mvParametros.ActiveViewIndex = 1;
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParametroService.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnFin_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void txtCodCuentaNIF_TextChanged(object sender, EventArgs e)
    {
        if (txtCodCuentaNIF.Text != "")
        {
            Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
            Xpinn.NIIF.Entities.PlanCuentasNIIF PlanCuentas = new Xpinn.NIIF.Entities.PlanCuentasNIIF();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentasNIIF(txtCodCuentaNIF.Text, (Usuario)Session["usuario"]);

            // Mostrar el nombre de la cuenta
            if (txtNomCuentaNif != null)
                txtNomCuentaNif.Text = PlanCuentas.nombre;
        }
        else
        {
            txtNomCuentaNif.Text = "";
            txtCodCuentaNIF.Text = "";
        }
    }


    protected void btnListadoPlanNIF_Click(object sender, EventArgs e)
    {
        ctlListadoPlanNif.Motrar(true, "txtCodCuentaNIF", "txtNomCuentaNif");
    }
}