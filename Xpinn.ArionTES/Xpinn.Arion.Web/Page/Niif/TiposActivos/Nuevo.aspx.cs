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
using Xpinn.NIIF.Services;
using Xpinn.NIIF.Entities;

partial class Nuevo : GlobalWeb
{
    TipoActivoNIFServices TipoService = new TipoActivoNIFServices();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[TipoService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(TipoService.CodigoPrograma, "E");
            else
                VisualizarOpciones(TipoService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                mvPrincipal.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;
                cargarDropdown();
                if (Session[TipoService.CodigoPrograma + ".id"] != null)
                {
                    txtCodigo.Visible = true;
                    lblAuto.Visible = false;
                    idObjeto = Session[TipoService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(TipoService.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    lblAuto.Visible = true;
                    txtCodigo.Visible = false;
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoService.CodigoPrograma, "Page_Load", ex);
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

    void cargarDropdown()
    {
        PoblarLista("Clasificacion_Activo_Nif", ddlClasificacion);
    }


    Boolean ValidarDatos()
    {
        VerError("");
        txtCodCuentaNIF.BackColor = System.Drawing.ColorTranslator.FromHtml("#f4f5ff");
        if (txtDescripcion.Text == "")
        {
            VerError("Ingrese una descripción");
            return false;
        }
        if (ddlClasificacion.SelectedIndex == 0)
        {
            VerError("Seleccione una clasificación");
            return false;
        }
        if (txtCodCuentaNIF.Text == "" || txtNomCuentaNif.Text == "")
        {
            VerError("Ingrese el Codigo de la Cuenta");
            txtCodCuentaNIF.BackColor = System.Drawing.Color.LightBlue;
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string msj = idObjeto != "" ? "Modificación" : "Grabación";
        if(ValidarDatos())
            ctlMensaje.MostrarMensaje("Desea realizar la " +msj+"?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            TipoActivoNIF vTipo = new TipoActivoNIF();

            if (idObjeto != "")
                vTipo.tipo_activo_nif = Convert.ToInt32(txtCodigo.Text);
            else
                vTipo.tipo_activo_nif = 0;

            vTipo.descripcion = txtDescripcion.Text.ToUpper();
            vTipo.codclasificacion_nif = Convert.ToInt32(ddlClasificacion.SelectedValue);

            vTipo.cod_cuenta = txtCodCuentaNIF.Text;

            if (txtCodCuentaNIF2.Text != "")
                vTipo.cod_cuenta_deterioro = txtCodCuentaNIF2.Text;
            else
                vTipo.cod_cuenta_deterioro = null;

            if (txtCodCuentaNIF3.Text != "")
                vTipo.cod_cuenta_revaluacion = txtCodCuentaNIF3.Text;
            else
                vTipo.cod_cuenta_revaluacion = null;
            vTipo.cod_cuenta_gastodet = txtCodCuentaNIF4.Text != "" && txtNomCuentaNif4.Text.Trim() != "" ? txtCodCuentaNIF4.Text : null;  

            if (idObjeto != "")
            {
                TipoService.CrearTipoActivoNIF(vTipo, (Usuario)Session["usuario"], 2);//MODIFICAR
            }
            else
            {
                TipoService.CrearTipoActivoNIF(vTipo, (Usuario)Session["usuario"], 1);//CREAR
            }

            lblmsjFin.Text = idObjeto != "" ? " Modificado " : " Grabado ";
            
            mvPrincipal.ActiveViewIndex = 1;
        }
        catch (ExceptionBusiness ex)
        {
            VerError("btnContinuarMen_Click <= "+ ex.Message);
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
            TipoActivoNIF vData = new TipoActivoNIF();

            vData = TipoService.ConsultarTipoActivo(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);

            if (vData.tipo_activo_nif != 0)
                txtCodigo.Text = vData.tipo_activo_nif.ToString();

            if (vData.descripcion != "")
                txtDescripcion.Text = vData.descripcion;

            if (vData.codclasificacion_nif != 0)
                ddlClasificacion.SelectedValue = vData.codclasificacion_nif.ToString();

            if (vData.cod_cuenta != "" && vData.cod_cuenta != null)
                txtCodCuentaNIF.Text = vData.cod_cuenta;
            txtCodCuentaNIF_TextChanged(txtCodCuentaNIF, null);

            if (vData.cod_cuenta_deterioro != "" && vData.cod_cuenta_deterioro != null)
                txtCodCuentaNIF2.Text = vData.cod_cuenta_deterioro;
            txtCodCuentaNIF2_TextChanged(txtCodCuentaNIF2, null);

            if (vData.cod_cuenta_revaluacion != null && vData.cod_cuenta_revaluacion != "")
                txtCodCuentaNIF3.Text = vData.cod_cuenta_revaluacion;
            txtCodCuentaNIF3_TextChanged(txtCodCuentaNIF3, null);

            if (vData.cod_cuenta_gastodet != null && vData.cod_cuenta_gastodet != "")
                txtCodCuentaNIF4.Text = vData.cod_cuenta_gastodet;
            txtCodCuentaNIF4_TextChanged(txtCodCuentaNIF4, null);
        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(TipoService.CodigoPrograma, "ObtenerDatos", ex);
            VerError(ex.Message);
        }
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
    protected void txtCodCuentaNIF2_TextChanged(object sender, EventArgs e)
    {
        Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
        Xpinn.NIIF.Entities.PlanCuentasNIIF PlanCuentas = new Xpinn.NIIF.Entities.PlanCuentasNIIF();
        PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentasNIIF(txtCodCuentaNIF2.Text, (Usuario)Session["usuario"]);

        // Mostrar el nombre de la cuenta
        if (txtNomCuentaNif2 != null)
            txtNomCuentaNif2.Text = PlanCuentas.nombre;
    }
    protected void txtCodCuentaNIF3_TextChanged(object sender, EventArgs e)
    {
        Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
        Xpinn.NIIF.Entities.PlanCuentasNIIF PlanCuentas = new Xpinn.NIIF.Entities.PlanCuentasNIIF();
        PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentasNIIF(txtCodCuentaNIF3.Text, (Usuario)Session["usuario"]);

        // Mostrar el nombre de la cuenta
        if (txtNomCuentaNif3 != null)
            txtNomCuentaNif3.Text = PlanCuentas.nombre;
    }
    protected void txtCodCuentaNIF4_TextChanged(object sender, EventArgs e)
    {
        Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
        Xpinn.NIIF.Entities.PlanCuentasNIIF PlanCuentas = new Xpinn.NIIF.Entities.PlanCuentasNIIF();
        PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentasNIIF(txtCodCuentaNIF4.Text, (Usuario)Session["usuario"]);

        // Mostrar el nombre de la cuenta
        if (txtNomCuentaNif4 != null)
            txtNomCuentaNif4.Text = PlanCuentas.nombre;
    }

    protected void btnListaNIF_Click(object sender, EventArgs e)
    {
        ctlListadoPlanNif.Motrar(true,"txtCodCuentaNIF", "txtNomCuentaNif");       
    }
    protected void btnListaNIF2_Click(object sender, EventArgs e)
    {
        ctlListadoPlanNif2.Motrar(true, "txtCodCuentaNIF2", "txtNomCuentaNif2");
    }
    protected void btnListaNIF3_Click(object sender, EventArgs e)
    {
        ctlListadoPlanNif3.Motrar(true, "txtCodCuentaNIF3", "txtNomCuentaNif3");
    }
    protected void btnListaNIF4_Click(object sender, EventArgs e)
    {
        ctlListadoPlanNif4.Motrar(true, "txtCodCuentaNIF4", "txtNomCuentaNif4");
    }
}