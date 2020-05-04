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
    private ParametrosCtaServiciosService objtoLineaServices = new ParametrosCtaServiciosService();
    Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[objtoLineaServices.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(objtoLineaServices.CodigoPrograma, "E");
            else
                VisualizarOpciones(objtoLineaServices.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objtoLineaServices.CodigoPrograma, "Page_PreInit", ex);
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
                if (Session[objtoLineaServices.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[objtoLineaServices.CodigoPrograma + ".id"].ToString();
                    Session.Remove(objtoLineaServices.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {                    
                    txtCodigo.Text = "Autogenerado";
                    ddlAtributos.Enabled = false;
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objtoLineaServices.CodigoPrograma, "Page_Load", ex);
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
            PoblarLista("lineasservicios", ddlLineaServicio);
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
            par_cue_linser entidad = new par_cue_linser();
            entidad = objtoLineaServices.Consultarpar_cue_linser(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);


            if (entidad.idparametro != 0)
                txtCodigo.Text = entidad.idparametro.ToString();
            if (!string.IsNullOrEmpty(entidad.cod_linea_servicio))
                ddlLineaServicio.SelectedValue = entidad.cod_linea_servicio;
            if (entidad.cod_est_det != 0)
                ddlEstructura.SelectedValue = entidad.cod_est_det.ToString();
            if (!string.IsNullOrEmpty(entidad.cod_cuenta))
                txtCodCuenta.Text = entidad.cod_cuenta;

            txtCodCuenta_TextChanged(txtCodCuenta, null);

            if (entidad.tipo_mov != null)
                ddlTipoMov.SelectedValue = entidad.tipo_mov.ToString();
            if (entidad.tipo_tran != null)
                ddltransac.SelectedValue = entidad.tipo_tran.ToString();

            if(entidad.cod_atr==2)
            {
                ddlAtributos.Enabled = true;
            }
            else
            {
                ddlAtributos.Enabled = false;
            }

        }
        catch (Exception ex)
        {            
            VerError(ex.Message);
        }
    }


    protected void txtCodCuenta_TextChanged(object sender, EventArgs e)
    {
        VerError("");
        bool esnif= PlanCuentasServicio.EsPlanCuentasNIIF((Usuario)Session["usuario"]);
        if (txtCodCuenta.Text.StartsWith("14") && esnif==false)
        {
            VerError("La cuenta no se puede seleccionar, pertenece al grupo contable de cartera");
            txtCodCuenta.Text = "";
        }
        else
        {

            if (txtCodCuenta.Text != "")
            {
               
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
    }

    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true, "txtCodCuenta", "txtNomCuenta");
    }

    Boolean validarDatos()
    {
        VerError("");
        bool esnif = PlanCuentasServicio.EsPlanCuentasNIIF((Usuario)Session["usuario"]);
        if (ddlLineaServicio.SelectedIndex == 0)
        {
            VerError("Seleccione la Linea de Aporte");
            return false;
        }
        if (txtCodCuenta.Text.Trim()=="")
        {
            VerError("Ingrese el codigo de la cuanta");
            return false;
        }
        if (ddlEstructura.SelectedIndex == 0 || ddlEstructura.SelectedItem == null)
        {
            VerError("Seleccione una estructura válida");
            return false;
        }
        if (txtCodCuenta.Text.StartsWith("14") && esnif==false)
        {
            VerError("La cuenta no se puede seleccionar, pertenece al grupo contable de cartera");
            txtCodCuenta.Text = "";
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
                par_cue_linser vData = new par_cue_linser();
                vData.cod_linea_servicio = ddlLineaServicio.SelectedValue;
                vData.cod_atr = Convert.ToInt32(ddlAtributos.SelectedValue);
                vData.cod_est_det = ddlEstructura.SelectedIndex > 0 ? Convert.ToInt32(ddlEstructura.SelectedValue) : 0;
                vData.cod_cuenta = txtCodCuenta.Text;
                vData.tipo_mov = ddlTipoMov.SelectedValue == "0" ? 0 : Convert.ToInt16(ddlTipoMov.SelectedValue);
                vData.tipo = 0;
                vData.tipo_tran = ddltransac.SelectedValue == "" ? 0 : Convert.ToInt16(ddltransac.SelectedValue);

                if (idObjeto != "")
                {
                    vData.idparametro = Convert.ToInt64(txtCodigo.Text);
                    objtoLineaServices.Modificarpar_cue_linser(vData, (Usuario)Session["usuario"]); // MODIFICA
                }
                else
                {
                    vData.idparametro = 0;
                    objtoLineaServices.Crearpar_cue_linser(vData, (Usuario)Session["usuario"]); // CREAR
                }

                string msj = idObjeto != "" ? "Modifico" : "Grabaron";
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
            BOexcepcion.Throw(objtoLineaServices.CodigoPrograma, "btnGuardar_Click", ex);
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