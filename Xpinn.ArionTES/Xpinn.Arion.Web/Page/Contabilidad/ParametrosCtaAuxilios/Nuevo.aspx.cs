﻿using System;
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
    private Xpinn.Contabilidad.Services.ParametroCtasAuxiliosService ParametroService = new Xpinn.Contabilidad.Services.ParametroCtasAuxiliosService();

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
           
            PoblarLista("ESTRUCTURA_DETALLE", ddlEstructura);
            cargarAuxilio();

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
            Xpinn.Contabilidad.Entities.Par_Cue_LinAux pConsulta = new Xpinn.Contabilidad.Entities.Par_Cue_LinAux();
            pConsulta = ParametroService.ConsultarPar_Cue_LinAux(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (pConsulta.idparametro != 0)
                txtCodigo.Text = pConsulta.idparametro.ToString();
            if (pConsulta.cod_linea_auxilio != "")
                ddlLineaAporte.SelectedValue = pConsulta.cod_linea_auxilio.ToString();
            if (pConsulta.cod_est_det != 0)
                ddlEstructura.SelectedValue = pConsulta.cod_est_det.ToString();
            if (pConsulta.cod_cuenta != "" && pConsulta.cod_cuenta != null)
                txtCodCuenta.Text = pConsulta.cod_cuenta;
            
            txtCodCuenta_TextChanged(txtCodCuenta, null);
           
            if (pConsulta.tipo_mov != 0)
                ddlTipoMov.SelectedValue = pConsulta.tipo_mov.ToString();
            if (pConsulta.tipo_tran != 0)
                ddltransac.SelectedValue = pConsulta.tipo_tran.ToString();

            Session["cod_atr"] = pConsulta.cod_atr;
            Session["tipo"] = pConsulta.tipo;
        }
        catch (Exception ex)
        {            
            VerError(ex.Message);
        }
    }

    private void cargarAuxilio()
    {
        String filtro = "";
        Xpinn.Auxilios.Services.LineaAuxilioServices linahorroServicio = new Xpinn.Auxilios.Services.LineaAuxilioServices();
        Xpinn.Auxilios.Entities.LineaAuxilio linahorroVista = new Xpinn.Auxilios.Entities.LineaAuxilio();
        ddlLineaAporte.DataTextField = "DESCRIPCION";
        ddlLineaAporte.DataValueField = "COD_LINEA_AUXILIO";
        ddlLineaAporte.DataSource = linahorroServicio.ListarLineaAuxilio(linahorroVista, (Usuario)Session["usuario"], filtro);
        ddlLineaAporte.DataBind();
        ddlLineaAporte.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
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


    protected Boolean validarDatos()
    {
        VerError("");
        if (ddlLineaAporte.SelectedIndex == 0)
        {
            VerError("Seleccione la Linea de Auxilio");
            return false;
        }
        if (ddlTipoMov.SelectedIndex == 0)
        {
            VerError("Seleccione el Tipo de Movimiento");
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
                Xpinn.Contabilidad.Entities.Par_Cue_LinAux vData = new Xpinn.Contabilidad.Entities.Par_Cue_LinAux();

                if (idObjeto != "")
                {
                    vData.idparametro = Convert.ToInt64(txtCodigo.Text.Trim());
                    vData.cod_atr = Convert.ToInt32(Session["cod_atr"].ToString());
                    vData.tipo = Convert.ToInt32(Session["tipo"].ToString());
                }                
                else
                {
                    vData.idparametro = 0;
                    vData.cod_atr = 1;
                    vData.tipo = 0;
                }
                vData.cod_linea_auxilio = Convert.ToString(ddlLineaAporte.SelectedValue);
                if (ddlEstructura.SelectedIndex != 0)
                    vData.cod_est_det = Convert.ToInt32(ddlEstructura.SelectedValue);
                else
                    vData.cod_est_det = 0;
                
                if (txtCodCuenta.Text != "" && txtNomCuenta.Text != "")
                    vData.cod_cuenta = txtCodCuenta.Text;
                else
                    vData.cod_cuenta = null;                              
                vData.tipo_mov = Convert.ToInt32(ddlTipoMov.SelectedValue);
                if (ddltransac.SelectedValue != "")
                    vData.tipo_tran = Convert.ToInt32(ddltransac.SelectedValue);

                if (idObjeto != "")
                {
                    ParametroService.ModificarPar_Cue_LinAux(vData, (Usuario)Session["usuario"]); // MODIFICAR
                }
                else
                {
                    ParametroService.CrearPar_Cue_LinAux(vData,(Usuario)Session["usuario"]);//CREAR                    
                }

                string msj = idObjeto != "" ? "Modifico" : "Grabo";
                lblmsj.Text = "Se " + msj + " Correctamente los Datos";
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarCancelar(false);
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