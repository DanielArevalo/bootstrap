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
    private Xpinn.Tesoreria.Services.ConceptoCtaService ConceptoCtaServicio = new Xpinn.Tesoreria.Services.ConceptoCtaService();
    Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ConceptoCtaServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ConceptoCtaServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(ConceptoCtaServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConceptoCtaServicio.CodigoPrograma, "Page_PreInit", ex);
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
                if (Session[ConceptoCtaServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ConceptoCtaServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ConceptoCtaServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    InicializarImpuestos();
                    txtConceptoCta.Text = ConceptoCtaServicio.ObtenerSiguienteCodigo((Usuario)Session["usuario"]).ToString();
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConceptoCtaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void InicializarImpuestos()
    {
        List<Concepto_CuentasXpagarImp> lstImpuestos = new List<Concepto_CuentasXpagarImp>();

        for (int i = 0; i < 2; i++)
        {
            Concepto_CuentasXpagarImp eImpu = new Concepto_CuentasXpagarImp();
            eImpu.idconceptoimp = -1;
            eImpu.cod_concepto_fac = null;
            eImpu.cod_tipo_impuesto = null;
            eImpu.porcentaje_impuesto = null;
            eImpu.base_minima = null;
            eImpu.cod_cuenta_imp = "";
            lstImpuestos.Add(eImpu);
        }   
        gvImpuestos.DataSource = lstImpuestos;
        gvImpuestos.DataBind();
        Session["DatosImpuesto"] = lstImpuestos;
    }

    protected void btnDetalle_Click(object sender, EventArgs e)
    {
        ObtenerListaImpuestos();
        List<Concepto_CuentasXpagarImp> lstImpuestos = new List<Concepto_CuentasXpagarImp>();


        if (Session["DatosImpuesto"] != null)
        {
            lstImpuestos = (List<Concepto_CuentasXpagarImp>)Session["DatosImpuesto"];

            for (int i = 1; i <= 1; i++)
            {
                Concepto_CuentasXpagarImp eImpu = new Concepto_CuentasXpagarImp();
                eImpu.idconceptoimp = -1;
                eImpu.cod_concepto_fac = null;
                eImpu.cod_tipo_impuesto = null;
                eImpu.porcentaje_impuesto = null;
                eImpu.base_minima = null;
                eImpu.cod_cuenta_imp = "";
                lstImpuestos.Add(eImpu);
            }
            gvImpuestos.DataSource = lstImpuestos;
            gvImpuestos.DataBind();

            Session["DatosImpuesto"] = lstImpuestos;
        }
    }

    protected List<Concepto_CuentasXpagarImp> ObtenerListaImpuestos()
    {
        List<Concepto_CuentasXpagarImp> lstImpuestos = new List<Concepto_CuentasXpagarImp>();
        List<Concepto_CuentasXpagarImp> lista = new List<Concepto_CuentasXpagarImp>();


        foreach (GridViewRow rfila in gvImpuestos.Rows)
        {
            Concepto_CuentasXpagarImp eImpu = new Concepto_CuentasXpagarImp();
            Label lblCodigo = (Label)rfila.FindControl("lblCodigo");
            if (lblCodigo.Text != "")
                eImpu.idconceptoimp = Convert.ToInt32(lblCodigo.Text);

            DropDownListGrid ddlTipoImpuesto = (DropDownListGrid)rfila.FindControl("ddlTipoImpuesto");
            if (ddlTipoImpuesto.SelectedValue != null)
                eImpu.cod_tipo_impuesto = Convert.ToInt32(ddlTipoImpuesto.SelectedValue);

            decimales txtBaseMinima = (decimales)rfila.FindControl("txtBaseMinima");
            if (txtBaseMinima != null)
                eImpu.base_minima = Convert.ToDecimal(txtBaseMinima.Text);
            TextBox txtPorcentajeImpuesto = (TextBox)rfila.FindControl("txtPorcentajeImpuesto");
            if (txtPorcentajeImpuesto.Text != "")
                eImpu.porcentaje_impuesto = Convert.ToDecimal(txtPorcentajeImpuesto.Text);

            TextBoxGrid txtCodCuenta_imp = (TextBoxGrid)rfila.FindControl("txtCodCuenta_imp");
            if (txtCodCuenta_imp.Text != "")
                eImpu.cod_cuenta_imp = txtCodCuenta_imp.Text;

            lista.Add(eImpu);
            Session["DatosImpuesto"] = lista;

            if (eImpu.porcentaje_impuesto != null && eImpu.cod_cuenta_imp != null && ddlTipoImpuesto.SelectedIndex != 0)
            {
                lstImpuestos.Add(eImpu);
            }
        }
        return lstImpuestos;
    }


    protected void gvImpuestos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DropDownListGrid ddlTipoImpuesto = (DropDownListGrid)e.Row.FindControl("ddlTipoImpuesto");

            List<Xpinn.Contabilidad.Entities.PlanCuentas> lstTipo = new List<Xpinn.Contabilidad.Entities.PlanCuentas>();
            Xpinn.Contabilidad.Entities.PlanCuentas vData = new Xpinn.Contabilidad.Entities.PlanCuentas();
            lstTipo = PlanCuentasServicio.ListarTipoImpuesto(vData, (Usuario)Session["usuario"]);

            if (ddlTipoImpuesto != null)
                if (lstTipo.Count > 0)
                {
                    ddlTipoImpuesto.DataSource = lstTipo;
                    ddlTipoImpuesto.DataTextField = "nombre_impuesto";
                    ddlTipoImpuesto.DataValueField = "cod_tipo_impuesto";
                    ddlTipoImpuesto.Items.Insert(0, new ListItem("Seleccione un item", "-1"));
                    ddlTipoImpuesto.SelectedIndex = 0;
                    ddlTipoImpuesto.DataBind();
                }

            Label lblTipoImpuesto = (Label)e.Row.FindControl("lblTipoImpuesto");
            if (lblTipoImpuesto != null)
                ddlTipoImpuesto.SelectedValue = lblTipoImpuesto.Text;
        }
    }

    protected void gvImpuestos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvImpuestos.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaImpuestos();

        List<Concepto_CuentasXpagarImp> LstDeta;
        LstDeta = (List<Concepto_CuentasXpagarImp>)Session["DatosImpuesto"];

        if (conseID > 0)
        {
            try
            {
                foreach (Concepto_CuentasXpagarImp Deta in LstDeta)
                {
                    if (Deta.idconceptoimp == conseID)
                    {
                        ConceptoCtaServicio.EliminarConceptoImpuesto(conseID, (Usuario)Session["usuario"]);
                        LstDeta.Remove(Deta);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
        }
        else
        {
            LstDeta.RemoveAt((gvImpuestos.PageIndex * gvImpuestos.PageSize) + e.RowIndex);
        }

        gvImpuestos.DataSourceID = null;
        gvImpuestos.DataBind();

        gvImpuestos.DataSource = LstDeta;
        gvImpuestos.DataBind();

        Session["DatosImpuesto"] = LstDeta;
    }

    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ButtonGrid btnListadoPlan = (ButtonGrid)sender;
        if (btnListadoPlan != null)
        {
            int rowIndex = Convert.ToInt32(btnListadoPlan.CommandArgument);
            ctlPlanCuentas ctlListadoPlan = (ctlPlanCuentas)gvImpuestos.Rows[rowIndex].FindControl("ctlListadoPlan");
            TextBoxGrid txtCodCuenta_imp = (TextBoxGrid)gvImpuestos.Rows[rowIndex].FindControl("txtCodCuenta_imp");
            ctlListadoPlan.Motrar(true, "txtCodCuenta_imp", "");
        }
    }

    Boolean ValidarDatos()
    {
        if (txtConceptoCta.Text == "")
        {
            VerError("Ingrese el código del concepto a registrar");
            return false;
        }

        Xpinn.Tesoreria.Entities.ConceptoCta vConceptoCta = new Xpinn.Tesoreria.Entities.ConceptoCta();
        if (idObjeto == "")
        {
            vConceptoCta = ConceptoCtaServicio.ConsultarConceptoCta(Convert.ToInt64(txtConceptoCta.Text), (Usuario)Session["usuario"]);
            if (vConceptoCta.cod_concepto_fac != null)
            {
                string cod_ant = txtConceptoCta.Text; 
                txtConceptoCta.Text = ConceptoCtaServicio.ObtenerSiguienteCodigo((Usuario)Session["usuario"]).ToString();
                VerError("El código " + cod_ant + " para el concepto ya fue registrado anteriormente, El código fue actualizado al Nro " + txtConceptoCta.Text);
                return false;
            }
        }

        if (txtDescripcion.Text == "")
        {
            VerError("Ingrese la descripción del concepto a registrar");
            return false;
        }
        List<Concepto_CuentasXpagarImp> lstImp = new List<Concepto_CuentasXpagarImp>();
        lstImp = ObtenerListaImpuestos();
        /*if (lstImp.Count == 0)
        {
            VerError("Debe ingresar al menos un registro de Impuestos");
            return false;
        }
        */
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
                ctlMensaje.MostrarMensaje("Desea realizar la " + msj + " del concepto.");
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
            Xpinn.Tesoreria.Entities.ConceptoCta vConceptoCta = new Xpinn.Tesoreria.Entities.ConceptoCta();

            if (idObjeto != "")
                vConceptoCta = ConceptoCtaServicio.ConsultarConceptoCta(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vConceptoCta.cod_concepto_fac = Convert.ToInt32(txtConceptoCta.Text.Trim());
            vConceptoCta.descripcion = Convert.ToString(txtDescripcion.Text.Trim());
            vConceptoCta.cod_cuenta = txtCodCuenta.Text != "" ? txtCodCuenta.Text : null;
            vConceptoCta.cod_cuenta_niif = txtCodCuentaNIF.Text != "" ? txtCodCuentaNIF.Text : null;
            vConceptoCta.cod_cuenta_desc = txtCodCuenta_desc.Text != "" ? txtCodCuenta_desc.Text : null;
            vConceptoCta.cod_cuenta_anticipos = txtCodCuentaAnticipo.Text != "" ? txtCodCuentaAnticipo.Text : null;

            if (txtCodCuenta.Text == "" && txtCodCuentaNIF.Text == "")
                vConceptoCta.tipo_mov = null;
            else
                vConceptoCta.tipo_mov = Convert.ToInt32(ddlTipoMov.SelectedValue);

            vConceptoCta.lstImpuesto = new List<Concepto_CuentasXpagarImp>();
            vConceptoCta.lstImpuesto = ObtenerListaImpuestos();

            if (idObjeto != "")
            {
                lblMsj.Text = "modificada";
                vConceptoCta.cod_concepto_fac = Convert.ToInt32(idObjeto);
                ConceptoCtaServicio.ModificarConceptoCta(vConceptoCta, (Usuario)Session["usuario"]);
            }
            else
            {
                lblMsj.Text = "grabada";
                vConceptoCta = ConceptoCtaServicio.CrearConceptoCta(vConceptoCta, (Usuario)Session["usuario"]);
                idObjeto = vConceptoCta.cod_concepto_fac.ToString();
            }

            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            mvImpuesto.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConceptoCtaServicio.CodigoPrograma, "btnContinuarMen_Click", ex);
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
            Xpinn.Tesoreria.Entities.ConceptoCta vConceptoComp = new Xpinn.Tesoreria.Entities.ConceptoCta();
            vConceptoComp = ConceptoCtaServicio.ConsultarConceptoCta(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vConceptoComp.cod_concepto_fac.ToString()))
                txtConceptoCta.Text = HttpUtility.HtmlDecode(vConceptoComp.cod_concepto_fac.ToString().Trim());
            if (!string.IsNullOrEmpty(vConceptoComp.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vConceptoComp.descripcion.ToString().Trim());
            if (vConceptoComp.cod_cuenta != null && vConceptoComp.cod_cuenta != "")
            {
                txtCodCuenta.Text = vConceptoComp.cod_cuenta;
                txtCodCuenta_TextChanged(txtCodCuenta, null);
            }

            if (vConceptoComp.cod_cuenta_anticipos != null && vConceptoComp.cod_cuenta_anticipos != "")
            {
                txtCodCuentaAnticipo.Text = vConceptoComp.cod_cuenta_anticipos;
                txtCodCuenta_TextChanged(txtCodCuenta, null);
            }

            if (vConceptoComp.cod_cuenta_niif != null && vConceptoComp.cod_cuenta_niif != "")
            {
                txtCodCuentaNIF.Text = vConceptoComp.cod_cuenta_niif;
                txtCodCuentaNIF_TextChanged(txtCodCuentaNIF, null);
            }
            if (vConceptoComp.cod_cuenta_desc != null && vConceptoComp.cod_cuenta_desc != "")
            {
                txtCodCuenta_desc.Text = vConceptoComp.cod_cuenta_desc;
                txtCodCuenta_desc_TextChanged(txtCodCuenta_desc, null);
            }
            if (vConceptoComp.tipo_mov != null && vConceptoComp.tipo_mov != 0)
                ddlTipoMov.Text = vConceptoComp.tipo_mov.ToString();

            if (vConceptoComp.cod_cuenta_anticipos != null && vConceptoComp.cod_cuenta_anticipos != "")
            {
                txtCodCuentaAnticipo.Text = vConceptoComp.cod_cuenta_anticipos;
                txtCodCuentaAnticipo_TextChanged(txtCodCuentaAnticipo, null);
            }



            List<Concepto_CuentasXpagarImp> lstImpuesto = new List<Concepto_CuentasXpagarImp>();
            Concepto_CuentasXpagarImp pImpuesto = new Concepto_CuentasXpagarImp();
            pImpuesto.cod_concepto_fac = Convert.ToInt32(txtConceptoCta.Text);

            lstImpuesto = ConceptoCtaServicio.ListarConceptoImpuesto(pImpuesto, (Usuario)Session["usuario"]);

            if (lstImpuesto.Count > 0)
            {
                gvImpuestos.DataSource = lstImpuesto;
                gvImpuestos.DataBind();
            }
            else
            {
                InicializarImpuestos();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConceptoCtaServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void txtCodCuentaAnticipo_TextChanged(object sender, EventArgs e)
    {
        Usuario usur = (Usuario)Session["usuario"];
        Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
        Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
        PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuentaAnticipo.Text, (Usuario)Session["usuario"]);

        // Mostrar el nombre de la cuenta           
        if (txtNomCuentaAnticipos != null)
            txtNomCuentaAnticipos.Text = PlanCuentas.nombre;
        
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

    #region cuenta contable para el descuento

    protected void txtCodCuenta_desc_TextChanged(object sender, EventArgs e)
    {
        Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
        Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
        PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta_desc.Text, (Usuario)Session["usuario"]);

        // Mostrar el nombre de la cuenta           
        if (txtNomCuentadesc != null)
            txtNomCuentadesc.Text = PlanCuentas.nombre;
    }

    protected void btnListadoPlandesc_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true, "txtCodCuenta_desc", "txtNomCuentadesc");
    }

    #endregion


}