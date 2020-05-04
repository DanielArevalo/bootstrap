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
    private Xpinn.Contabilidad.Services.ParametroCtasCreditosService Par_Cue_LinCredServicio = new Xpinn.Contabilidad.Services.ParametroCtasCreditosService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[Par_Cue_LinCredServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(Par_Cue_LinCredServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(Par_Cue_LinCredServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Par_Cue_LinCredServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtCodigo.Enabled = false;
                CargarListas();
                if (Session[Par_Cue_LinCredServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[Par_Cue_LinCredServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(Par_Cue_LinCredServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {                    
                    txtCodigo.Text = Convert.ToString(Par_Cue_LinCredServicio.ObtenerSiguienteCodigo((Usuario)Session["Usuario"]));
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Par_Cue_LinCredServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected Boolean ValidarDatos()
    {
        if (ddlLineaCredito.SelectedIndex == 0)
        {
            VerError("Seleccione una linea de crédito");
            ddlLineaCredito.Focus();
            return false;
        }
        if (ddlAtributo.SelectedIndex == 0)
        {
            VerError("Seleccione una Atributo.");
            ddlAtributo.Focus();
            return false;
        }
        if (ddlTipoCuenta.SelectedIndex == 0)
        {
            VerError("Seleccione un tipo de Cuenta");
            ddlTipoCuenta.Focus();
            return false;
        }
        if (txtCodCuenta.Text.Trim() == "")
        {
            VerError("Ingrese el código de la cuenta");
            txtCodCuenta.Focus();
            return false;
        }
        if (txtCodCuenta.Text.Trim() != "" && txtNomCuenta.Text.Trim() == "")
        {
            VerError("El código de la cuenta ingresada no es válido.");
            txtCodCuenta.Focus();
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidarDatos())
                return;

            Xpinn.Contabilidad.Entities.Par_Cue_LinCred vPar_Cue_LinCred = new Xpinn.Contabilidad.Entities.Par_Cue_LinCred();

            if (idObjeto != "")
                vPar_Cue_LinCred = Par_Cue_LinCredServicio.ConsultarPar_Cue_LinCred(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vPar_Cue_LinCred.idparametro = Convert.ToInt64(txtCodigo.Text.Trim());
            vPar_Cue_LinCred.cod_linea_credito = Convert.ToString(ddlLineaCredito.SelectedValue);
            vPar_Cue_LinCred.cod_atr = Convert.ToInt32(ddlAtributo.SelectedValue);
            vPar_Cue_LinCred.nom_atr = Convert.ToString(ddlAtributo.SelectedValue);
            vPar_Cue_LinCred.tipo_cuenta = Convert.ToInt32(ddlTipoCuenta.SelectedValue);
            vPar_Cue_LinCred.tipo = ddlTipo.SelectedIndex == 0 ? -1 : Convert.ToInt32(ddlTipo.SelectedValue);
            // PARA CLASIFICACION DE CARTERA
            if (ddlCategoria.SelectedValue.Trim() != "")
                vPar_Cue_LinCred.cod_categoria = Convert.ToString(ddlCategoria.SelectedValue);
            if (ddlLibranza.SelectedValue.Trim() != "")
                vPar_Cue_LinCred.libranza = Convert.ToInt32(ddlLibranza.SelectedValue);
            if (ddlGarantia.SelectedValue.Trim() != "")
                vPar_Cue_LinCred.garantia = Convert.ToInt32(ddlGarantia.SelectedValue);
            if (ddlEstructura.SelectedValue.Trim() != "")
                vPar_Cue_LinCred.cod_est_det = Convert.ToInt32(ddlEstructura.SelectedValue);
            // CODIGO CUENTA LOCAL Y NIIF
            if (txtCodCuenta.Text != "" && txtNomCuenta.Text != "")
                vPar_Cue_LinCred.cod_cuenta = txtCodCuenta.Text;
            else
                vPar_Cue_LinCred.cod_cuenta = null;
            if (txtCodCuentaNIF.Text != "" && txtNomCuentaNif.Text != "")
                vPar_Cue_LinCred.cod_cuenta_niif = txtCodCuentaNIF.Text;
            else
                vPar_Cue_LinCred.cod_cuenta_niif = null;
            // TIPO DE TRANSACCION
            vPar_Cue_LinCred.tipo_mov = Convert.ToInt32(ddlTipoMov.SelectedValue);
            if (ddlTipoTran.SelectedValue.Trim() != "")
                vPar_Cue_LinCred.tipo_tran = Convert.ToInt32(ddlTipoTran.SelectedValue);
            else
                vPar_Cue_LinCred.tipo_tran = null;

            if (idObjeto != "")
            {
                vPar_Cue_LinCred.idparametro = Convert.ToInt64(idObjeto);
                Par_Cue_LinCredServicio.ModificarPar_Cue_LinCred(vPar_Cue_LinCred, (Usuario)Session["usuario"]);

                Session[Par_Cue_LinCredServicio.CodigoPrograma + ".id"] = idObjeto;
                Navegar(Pagina.Lista);
            }
            else
            {
                string error = "";
                VerError("");

                vPar_Cue_LinCred = Par_Cue_LinCredServicio.CrearPar_Cue_LinCred(vPar_Cue_LinCred, ref error, (Usuario)Session["usuario"]);
                if (error != null && error != "")
                {
                    VerError(error);
                }
                else
                {
                    idObjeto = vPar_Cue_LinCred.idparametro.ToString();

                    Session[Par_Cue_LinCredServicio.CodigoPrograma + ".id"] = idObjeto;
                    Navegar(Pagina.Lista);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Par_Cue_LinCredServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    private void CargarListas()
    {        
        try
        {
            Xpinn.FabricaCreditos.Services.LineasCreditoService lineaServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
            List<Xpinn.FabricaCreditos.Entities.LineasCredito> lstLineasCredito = new List<Xpinn.FabricaCreditos.Entities.LineasCredito>();
            Xpinn.FabricaCreditos.Entities.LineasCredito pLineasCredito = new Xpinn.FabricaCreditos.Entities.LineasCredito();
            lstLineasCredito = lineaServicio.ListarLineasCredito(pLineasCredito, (Usuario)Session["Usuario"]);
            ddlLineaCredito.DataSource = lstLineasCredito;
            ddlLineaCredito.DataTextField = "Nombre";
            ddlLineaCredito.DataValueField = "Codigo";
            ddlLineaCredito.DataBind();

            List<Xpinn.FabricaCreditos.Entities.Atributos> lstAtributos = new List<Xpinn.FabricaCreditos.Entities.Atributos>();
            Xpinn.FabricaCreditos.Entities.Atributos pAtributos = new Xpinn.FabricaCreditos.Entities.Atributos();
            lstAtributos = lineaServicio.ListarAtributos(pAtributos, (Usuario)Session["Usuario"]);
            ddlAtributo.DataSource = lstAtributos;
            ddlAtributo.DataTextField = "nom_atr";
            ddlAtributo.DataValueField = "cod_atr";
            ddlAtributo.DataBind();

            List<Xpinn.Contabilidad.Entities.Par_Cue_LinCred> lstCategorias = new List<Xpinn.Contabilidad.Entities.Par_Cue_LinCred>();
            lstCategorias = Par_Cue_LinCredServicio.ListarClasificacion((Usuario)Session["Usuario"]);
            ddlCategoria.DataSource = lstCategorias;
            ddlCategoria.DataTextField = "cod_categoria";
            ddlCategoria.DataValueField = "cod_categoria";
            ddlCategoria.DataBind();
            
            List<Xpinn.Contabilidad.Entities.Par_Cue_LinCred> lstTransaccion = new List<Xpinn.Contabilidad.Entities.Par_Cue_LinCred>();
            lstTransaccion = Par_Cue_LinCredServicio.ListarTransaccion((Usuario)Session["Usuario"]);
            ddlTipoTran.DataSource = lstTransaccion;
            ddlTipoTran.DataTextField = "nom_tipo_tran";
            ddlTipoTran.DataValueField = "tipo_tran";
            ddlTipoTran.DataBind();

            List<Xpinn.Contabilidad.Entities.Par_Cue_LinCred> lstEstructura = new List<Xpinn.Contabilidad.Entities.Par_Cue_LinCred>();
            lstEstructura = Par_Cue_LinCredServicio.ListarEstructura((Usuario)Session["Usuario"]);
            ddlEstructura.DataSource = lstEstructura;
            ddlEstructura.DataTextField = "nom_est_det";
            ddlEstructura.DataValueField = "cod_est_det";
            ddlEstructura.DataBind();

            List<Xpinn.Contabilidad.Entities.Par_Cue_LinCred> lstLibranza = new List<Xpinn.Contabilidad.Entities.Par_Cue_LinCred>();
            lstLibranza = Par_Cue_LinCredServicio.ListarLibranza((Usuario)Session["Usuario"]);
            ddlLibranza.DataSource = lstLibranza;
            ddlLibranza.DataTextField = "nom_libranza";
            ddlLibranza.DataValueField = "libranza";
            ddlLibranza.DataBind();

            List<Xpinn.Contabilidad.Entities.Par_Cue_LinCred> lstGarantia = new List<Xpinn.Contabilidad.Entities.Par_Cue_LinCred>();
            lstGarantia = Par_Cue_LinCredServicio.ListarGarantia((Usuario)Session["Usuario"]);
            ddlGarantia.DataSource = lstGarantia;
            ddlGarantia.DataTextField = "nom_garantia";
            ddlGarantia.DataValueField = "garantia";
            ddlGarantia.DataBind();

            ddlTipo_SelectedIndexChanged(null, null);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
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
            Xpinn.Contabilidad.Entities.Par_Cue_LinCred vPar_Cue_LinCred = new Xpinn.Contabilidad.Entities.Par_Cue_LinCred();
            vPar_Cue_LinCred = Par_Cue_LinCredServicio.ConsultarPar_Cue_LinCred(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            
            txtCodigo.Text = HttpUtility.HtmlDecode(vPar_Cue_LinCred.idparametro.ToString().Trim());
            if (!string.IsNullOrEmpty(vPar_Cue_LinCred.cod_linea_credito))
                ddlLineaCredito.SelectedValue = HttpUtility.HtmlDecode(vPar_Cue_LinCred.cod_linea_credito.ToString().Trim());
            if (!string.IsNullOrEmpty(vPar_Cue_LinCred.cod_atr.ToString()))
                ddlAtributo.SelectedValue = HttpUtility.HtmlDecode(vPar_Cue_LinCred.cod_atr.ToString().Trim());
            if (!string.IsNullOrEmpty(vPar_Cue_LinCred.tipo_cuenta.ToString()))
                ddlTipoCuenta.SelectedValue = HttpUtility.HtmlDecode(vPar_Cue_LinCred.tipo_cuenta.ToString().Trim());
            if (!string.IsNullOrEmpty(vPar_Cue_LinCred.tipo.ToString()))
                ddlTipo.SelectedValue = HttpUtility.HtmlDecode(vPar_Cue_LinCred.tipo.ToString().Trim());
            ddlTipo_SelectedIndexChanged(ddlTipo, null);
            if (!string.IsNullOrEmpty(vPar_Cue_LinCred.cod_categoria))
                ddlCategoria.SelectedValue = HttpUtility.HtmlDecode(vPar_Cue_LinCred.cod_categoria.ToString().Trim());
            if (!string.IsNullOrEmpty(vPar_Cue_LinCred.libranza.ToString()))
                ddlLibranza.SelectedValue = HttpUtility.HtmlDecode(vPar_Cue_LinCred.libranza.ToString().Trim());
            if (!string.IsNullOrEmpty(vPar_Cue_LinCred.garantia.ToString()))
                ddlGarantia.SelectedValue = HttpUtility.HtmlDecode(vPar_Cue_LinCred.garantia.ToString().Trim());
            if (!string.IsNullOrEmpty(vPar_Cue_LinCred.cod_est_det.ToString()))
                ddlEstructura.SelectedValue = HttpUtility.HtmlDecode(vPar_Cue_LinCred.cod_est_det.ToString().Trim());
            if (!string.IsNullOrEmpty(vPar_Cue_LinCred.cod_cuenta.ToString()))
            {                
                txtCodCuenta.Text = HttpUtility.HtmlDecode(vPar_Cue_LinCred.cod_cuenta.ToString().Trim());
            }            
            txtCodCuenta_TextChanged(txtCodCuenta, null);

            if (vPar_Cue_LinCred.cod_cuenta_niif != null && vPar_Cue_LinCred.cod_cuenta_niif != "")
                txtCodCuentaNIF.Text = HttpUtility.HtmlDecode(vPar_Cue_LinCred.cod_cuenta_niif.ToString().Trim());
            txtCodCuentaNIF_TextChanged(txtCodCuentaNIF, null);

            if (!string.IsNullOrEmpty(vPar_Cue_LinCred.tipo_mov.ToString()))
                ddlTipoMov.SelectedValue = HttpUtility.HtmlDecode(vPar_Cue_LinCred.tipo_mov.ToString().Trim());
            if (!string.IsNullOrEmpty(vPar_Cue_LinCred.tipo_tran.ToString()))
                ddlTipoTran.SelectedValue = HttpUtility.HtmlDecode(vPar_Cue_LinCred.tipo_tran.ToString().Trim());             
        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(Par_Cue_LinCredServicio.CodigoPrograma, "ObtenerDatos", ex);
            VerError(ex.Message);
        }
    }


    protected void txtCodCuenta_TextChanged(object sender, EventArgs e)
    {
        Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
        Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
        PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta.Text, (Usuario)Session["usuario"]);

        // Mostrar el nombre de la cuenta           
        if (txtNomCuenta != null)
            txtNomCuenta.Text = PlanCuentas.nombre;
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

    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true,"txtCodCuenta","txtNomCuenta");
    }

    protected void btnListadoPlanNIF_Click(object sender, EventArgs e)
    {
        ctlListadoPlanNif.Motrar(true,"txtCodCuentaNIF", "txtNomCuentaNif");       
    }

    protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Xpinn.Comun.Entities.ListasFijas> lstTipos = new List<Xpinn.Comun.Entities.ListasFijas>();
        try
        {
            lstTipos = ListaTipoCuenta(Convert.ToInt32(ddlTipo.SelectedValue));            
        }
        catch
        {
            lstTipos = ListaTipoCuenta(0);
        }
        ddlTipoCuenta.DataSource = lstTipos;
        ddlTipoCuenta.DataTextField = "descripcion";
        ddlTipoCuenta.DataValueField = "codigo";
        ddlTipoCuenta.DataBind();
        ddlAtributo.Enabled = true;
        ddlTipoCuenta.Enabled = true;
        try
        {
            if (ddlTipo.SelectedValue == "0") // Para la provisión individual
            {
                ddlTipo.Visible = true;
                ddlCategoria.Visible = false;
                lblCategoria.Visible = false;   
                ddlLibranza.Visible = false;
                lblLibranza.Visible = false;
                ddlGarantia.Visible = false;
                lblGarantia.Visible = false;

            }
            else if (ddlTipo.SelectedValue == "2") // Para la provisión individual
            {
                ddlTipo.Visible = true;
                ddlCategoria.Visible = true;
                lblCategoria.Visible = true;   
                ddlLibranza.Visible = false;
                lblLibranza.Visible = false;
                if (Par_Cue_LinCredServicio.ParametroGeneral(44, (Usuario)Session["Usuario"]) == "1")
                {
                    ddlGarantia.Visible = true;
                    lblGarantia.Visible = true;
                }
                else
                { 
                    ddlGarantia.Visible = false;
                    lblGarantia.Visible = false;
                }
            }
            else if (ddlTipo.SelectedValue == "3") // Para la causación
            {
                ddlTipo.Visible = true;
                ddlCategoria.Visible = false;
                lblCategoria.Visible = false;
                ddlLibranza.Visible = false;
                lblLibranza.Visible = false;
                ddlGarantia.Visible = false;
                lblGarantia.Visible = false;
            }
            else if (ddlTipo.SelectedValue == "4" || ddlTipo.SelectedValue == "5") // Para la provisión general
            {
                ddlTipo.Visible = true;
                ddlCategoria.Visible = false;
                lblCategoria.Visible = false;
                ddlLibranza.Visible = true;
                lblLibranza.Visible = true;
                ddlGarantia.Visible = false;
                lblGarantia.Visible = false;
                if (ddlTipo.SelectedValue == "5")
                {
                    ddlAtributo.SelectedValue = "1";
                    ddlAtributo.Enabled = false;
                    ddlTipoCuenta.SelectedValue = "1";
                    ddlTipoCuenta.Enabled = false;
                }   
            }
            else 
            {
                ddlTipo.Visible = true;
                ddlCategoria.Visible = true;
                lblCategoria.Visible = true;
                ddlLibranza.Visible = true;
                lblLibranza.Visible = true;
                ddlGarantia.Visible = true;
                lblGarantia.Visible = true;
            }
        }
        catch
        { }
    }
}