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
    private ParametroCtasAportesService ParametroService = new ParametroCtasAportesService();

    protected void Page_PreInit(object sender, System.EventArgs e)
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


    private void CargarListas()
    {
        try
        {
            PoblarLista("LINEAAPORTE", ddlLineaAporte);
            PoblarLista("ESTRUCTURA_DETALLE", ddlEstructura);

            ddlTipoMov.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlTipoMov.Items.Insert(1, new ListItem("Débito", "1"));
            ddlTipoMov.Items.Insert(2, new ListItem("Crédito", "2"));
            ddlTipoMov.SelectedIndex = 0;
            ddlTipoMov.DataBind();

            PoblarLista("tipo_tran", ddltransac);
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
            Par_Cue_LinApo pConsulta = new Par_Cue_LinApo();
            pConsulta = ParametroService.ConsultarPar_Cue_LinApo(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (pConsulta.idparametro != 0)
                txtCodigo.Text = pConsulta.idparametro.ToString();
            if (pConsulta.cod_linea_aporte != 0)
                ddlLineaAporte.SelectedValue = pConsulta.cod_linea_aporte.ToString();
            if (pConsulta.cod_est_det != 0)
                ddlEstructura.SelectedValue = pConsulta.cod_est_det.ToString();
            if (pConsulta.cod_cuenta != "" && pConsulta.cod_cuenta != null)
                txtCodCuenta.Text = pConsulta.cod_cuenta;
            
            txtCodCuenta_TextChanged(txtCodCuenta, null);

            if (pConsulta.cod_cuenta_niif != "" && pConsulta.cod_cuenta_niif != null)
                txtCodCuentaNIF.Text = pConsulta.cod_cuenta_niif;

            txtCodCuentaNIF_TextChanged(txtCodCuentaNIF, null);

            if (pConsulta.tipo_mov != 0)
                ddlTipoMov.SelectedValue = pConsulta.tipo_mov.ToString();
            if (pConsulta.tipo_tran != 0)
                ddltransac.SelectedValue = pConsulta.tipo_tran.ToString();

            Session["cod_atr"] = pConsulta.cod_atr;
            Session["tipo_cuenta"] = pConsulta.tipo_cuenta;
            Session["tipo"] = pConsulta.tipo;
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
        }
    }

    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true, "txtCodCuenta", "txtNomCuenta");
    }

    protected void btnListadoPlanNIF_Click(object sender, EventArgs e)
    {
        ctlListadoPlanNif.Motrar(true, "txtCodCuentaNIF", "txtNomCuentaNif");
    }


    Boolean validarDatos()
    {
        VerError("");
        if (ddlLineaAporte.SelectedIndex == 0)
        {
            VerError("Seleccione la Linea de Aporte");
            return false;
        }
        if (txtCodCuenta.Text == "" && txtCodCuentaNIF.Text == "")
        {
            VerError("Seleccione una Cuenta Contable");
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
                Par_Cue_LinApo vData = new Par_Cue_LinApo();

                if (idObjeto != "")
                {
                    vData.idparametro = Convert.ToInt64(txtCodigo.Text.Trim());
                    vData.cod_atr = Convert.ToInt32(Session["cod_atr"].ToString());
                    vData.tipo_cuenta = Convert.ToInt32(Session["tipo_cuenta"].ToString());
                    vData.tipo = Convert.ToInt32(Session["tipo"].ToString());
                }                
                else
                {
                    vData.idparametro = 0;

                    if (true)
                    {

                    }

                    vData.cod_atr = 1;
                    vData.tipo_cuenta = 1;
                    vData.tipo = 0;
                }
                vData.cod_linea_aporte = Convert.ToInt32(ddlLineaAporte.SelectedValue);
                if (ddlEstructura.SelectedIndex != 0)
                    vData.cod_est_det = Convert.ToInt32(ddlEstructura.SelectedValue);
                else
                    vData.cod_est_det = 0;
                // CODIGO CUENTA LOCAL Y NIIF
                if (txtCodCuenta.Text != "" && txtNomCuenta.Text != "")
                    vData.cod_cuenta = txtCodCuenta.Text;
                else
                    vData.cod_cuenta = null;
                if (txtCodCuentaNIF.Text != "" && txtNomCuentaNif.Text != "")
                    vData.cod_cuenta_niif = txtCodCuentaNIF.Text;
                else
                    vData.cod_cuenta_niif = null;
                // TIPO DE TRANSACCION
                vData.tipo_mov = Convert.ToInt32(ddlTipoMov.SelectedValue);
                if (ddltransac.SelectedValue != "")
                {
                    vData.tipo_tran = Convert.ToInt32(ddltransac.SelectedValue);

                    if (vData.tipo_tran == 99 || vData.tipo_tran == 100 || vData.tipo_tran == 103 || vData.tipo_tran == 104)
                    {
                        vData.cod_atr = 2;
                    }
                }


                if (idObjeto != "")
                {
                    ParametroService.CrearParametroAporte(vData, (Usuario)Session["usuario"],2); // MODIFICAR
                }
                else
                {
                    ParametroService.CrearParametroAporte(vData,(Usuario)Session["usuario"],1);//CREAR                    
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
}