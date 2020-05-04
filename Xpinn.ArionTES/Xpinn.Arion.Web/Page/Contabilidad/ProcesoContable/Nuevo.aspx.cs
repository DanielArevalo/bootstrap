using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;
using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;
using System.Linq;

partial class Nuevo : GlobalWeb
{
    ProcesoContableService _procesoContableServicio = new ProcesoContableService();
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[_procesoContableServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(_procesoContableServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(_procesoContableServicio.CodigoPrograma, "A");

            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_procesoContableServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["Usuario"];
            if (!IsPostBack)
            {
                txtCodigo.Enabled = false;
                CargarListas();
                //inicializargrid();
                if (Session[_procesoContableServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[_procesoContableServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(_procesoContableServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtCodigo.Text = Convert.ToString(_procesoContableServicio.ObtenerSiguienteCodigo(_usuario));
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_procesoContableServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            ProcesoContable vProcesoContable = new ProcesoContable();

            if (idObjeto != "")
                vProcesoContable = _procesoContableServicio.ConsultarProcesoContable(Convert.ToInt64(idObjeto), _usuario);

            vProcesoContable.cod_proceso = Convert.ToInt64(txtCodigo.Text.Trim());
            vProcesoContable.tipo_ope = Convert.ToInt32(ddlTipoOpe.SelectedValue);
            vProcesoContable.tipo_comp = Convert.ToInt32(ddlTipoComp.SelectedValue);
            vProcesoContable.fecha_inicial = txtFechaInicial.ToDateTime;
            vProcesoContable.fecha_final = txtFechaFinal.ToDateTime;
            if (!string.IsNullOrWhiteSpace(ctlListarCodigo.Codigo))
                    vProcesoContable.concepto = Convert.ToInt64(ctlListarCodigo.Codigo);
            vProcesoContable.cod_cuenta = txtCodCuenta.Text;//Convert.ToString(ddlCodCuenta.SelectedItem);
            vProcesoContable.tipo_mov = Convert.ToInt32(ddlTipoMov.SelectedValue);
            if (ddlEstructura.SelectedValue != "")
                vProcesoContable.cod_est_det = Convert.ToInt32(ddlEstructura.SelectedValue);

            if (idObjeto != "")
            {
                vProcesoContable.cod_proceso = Convert.ToInt64(idObjeto);
                _procesoContableServicio.ModificarProcesoContable(vProcesoContable, _usuario);
            }
            else
            {
                vProcesoContable = _procesoContableServicio.CrearProcesoContable(vProcesoContable, _usuario);
                idObjeto = vProcesoContable.cod_proceso.ToString();
            }

            if (!GuardarGVTipoGiros()) // Si falla al guardar retorna ya que el error ya esta mostrado
            {
                return;
            }

            Session[_procesoContableServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_procesoContableServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    private void CargarListas()
    {
        try
        {
            PlanCuentasService plancuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
            List<Xpinn.Contabilidad.Entities.PlanCuentas> lstplancuentas = new List<Xpinn.Contabilidad.Entities.PlanCuentas>();
            Xpinn.Contabilidad.Entities.PlanCuentas pPlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
            lstplancuentas = plancuentasServicio.ListarPlanCuentasLocal(pPlanCuentas, _usuario, "");
            ddlCodCuenta.DataSource = lstplancuentas;
            ddlCodCuenta.DataTextField = "cod_cuenta";
            ddlCodCuenta.DataValueField = "nombre";
            ddlCodCuenta.DataBind();

            ddlNomCuenta.DataSource = lstplancuentas;
            ddlNomCuenta.DataTextField = "nombre";
            ddlNomCuenta.DataValueField = "cod_cuenta";
            ddlNomCuenta.DataBind();

            Xpinn.Contabilidad.Services.TipoComprobanteService TipoComprobanteService = new Xpinn.Contabilidad.Services.TipoComprobanteService();
            Xpinn.Contabilidad.Entities.TipoComprobante TipoComprobante = new Xpinn.Contabilidad.Entities.TipoComprobante();
            ddlTipoComp.DataSource = TipoComprobanteService.ListarTipoComprobante(TipoComprobante, "", _usuario);
            ddlTipoComp.DataTextField = "descripcion";
            ddlTipoComp.DataValueField = "tipo_comprobante";
            ddlTipoComp.DataBind();

            Xpinn.Caja.Services.TipoOperacionService TipOpeService = new Xpinn.Caja.Services.TipoOperacionService();
            Xpinn.Caja.Entities.TipoOperacion TipOpe = new Xpinn.Caja.Entities.TipoOperacion();
            ddlTipoOpe.DataSource = TipOpeService.ListarTipoOpe(_usuario);
            ddlTipoOpe.DataTextField = "nom_tipo_operacion";
            ddlTipoOpe.DataValueField = "cod_operacion";
            ddlTipoOpe.DataBind();

            Xpinn.Contabilidad.Services.ConceptoService conceptoService = new Xpinn.Contabilidad.Services.ConceptoService();
            Xpinn.Contabilidad.Entities.Concepto econcepto = new Xpinn.Contabilidad.Entities.Concepto();

            ctlListarCodigo.ValueField = "concepto";
            ctlListarCodigo.TextField = "descripcion";

            var lstConcepto = conceptoService.ListarConcepto(econcepto, _usuario);

            ctlListarCodigo.BindearControl(lstConcepto);

            Xpinn.Contabilidad.Services.EstructuraDetalleService EstDetService = new Xpinn.Contabilidad.Services.EstructuraDetalleService();
            Xpinn.Contabilidad.Entities.EstructuraDetalle pEstDet = new Xpinn.Contabilidad.Entities.EstructuraDetalle();
            ddlEstructura.DataSource = EstDetService.ListarEstructuraDetalle(pEstDet, _usuario);
            ddlEstructura.DataTextField = "detalle";
            ddlEstructura.DataValueField = "cod_est_det";
            ddlEstructura.DataBind();
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
            Xpinn.Contabilidad.Entities.ProcesoContable vProcesoContable = new Xpinn.Contabilidad.Entities.ProcesoContable();
            vProcesoContable = _procesoContableServicio.ConsultarProcesoContable(Convert.ToInt64(pIdObjeto), _usuario);

            txtCodigo.Text = HttpUtility.HtmlDecode(vProcesoContable.cod_proceso.ToString().Trim());
            if (vProcesoContable.cod_cuenta != null)
            {
                if (!string.IsNullOrEmpty(vProcesoContable.cod_cuenta.ToString()))
                {
                    ddlNomCuenta.SelectedValue = HttpUtility.HtmlDecode(vProcesoContable.cod_cuenta.ToString().Trim());
                    txtCodCuenta.Text = HttpUtility.HtmlDecode(vProcesoContable.cod_cuenta.ToString().Trim());
                    ddlCodCuenta.Text = ddlNomCuenta.SelectedItem.Text;
                    txtNomCuenta.Text = ddlNomCuenta.SelectedItem.Text;
                }
            }

            if (!string.IsNullOrEmpty(vProcesoContable.tipo_ope.ToString()))
                ddlTipoOpe.SelectedValue = HttpUtility.HtmlDecode(vProcesoContable.tipo_ope.ToString().Trim());
            if (!string.IsNullOrEmpty(vProcesoContable.tipo_comp.ToString()))
                ddlTipoComp.SelectedValue = HttpUtility.HtmlDecode(vProcesoContable.tipo_comp.ToString().Trim());
            if (!string.IsNullOrEmpty(vProcesoContable.fecha_inicial.ToString()))
                txtFechaInicial.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vProcesoContable.fecha_inicial.ToString().Trim()));
            if (!string.IsNullOrEmpty(vProcesoContable.fecha_final.ToString()))
                txtFechaFinal.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vProcesoContable.fecha_final.ToString().Trim()));
            if (!string.IsNullOrEmpty(vProcesoContable.concepto.ToString()))
                ctlListarCodigo.SelectedValueEqual(HttpUtility.HtmlDecode(vProcesoContable.concepto.ToString().Trim()));
            if (vProcesoContable.cod_cuenta != null)
            {
                if (!string.IsNullOrEmpty(vProcesoContable.cod_cuenta.ToString()))
                    ddlCodCuenta.SelectedValue = HttpUtility.HtmlDecode(vProcesoContable.cod_cuenta.ToString().Trim());
            }
            if (!string.IsNullOrEmpty(vProcesoContable.cod_est_det.ToString()))
                ddlEstructura.SelectedValue = HttpUtility.HtmlDecode(vProcesoContable.cod_est_det.ToString().Trim());
            if (!string.IsNullOrEmpty(vProcesoContable.tipo_mov.ToString()))
                ddlTipoMov.SelectedValue = HttpUtility.HtmlDecode(vProcesoContable.tipo_mov.ToString().Trim());

            if (ddlTipoOpe.SelectedValue == "103" || ddlTipoOpe.SelectedValue == "149") // Codigo de operacion Giros
            {
                List<GiroDistribucion> lstGiros = LlenarGVTipoGiros(ddlTipoOpe.SelectedValue);

                if (lstGiros != null)
                {
                    LlenarDatosRegistradosEnGVTipoGiros(Convert.ToInt32(pIdObjeto), lstGiros);
                }
            }
        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(ProcesoContableServicio.CodigoPrograma, "ObtenerDatos", ex);
            VerError(ex.Message);
        }
    }


    protected void ddlCodCuenta_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlNomCuenta.SelectedValue = ddlCodCuenta.SelectedItem.Text;
    }

    protected void ddlNomCuenta_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCodCuenta.Text = ddlNomCuenta.SelectedItem.Text;
    }

    #region MyRegion
    protected void txtCodCuenta_TextChanged(object sender, EventArgs e)
    {
        if (txtCodCuenta.Text != "")
        {
            // Determinar los datos de la cuenta contable
            Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
            Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta.Text, _usuario);
            //int rowIndex = Convert.ToInt32(txtCodCuenta.CommandArgument);

            // Mostrar el nombre de la cuenta            
            if (txtNomCuenta != null)
                txtNomCuenta.Text = PlanCuentas.nombre;
        }
        else
        {
            txtNomCuenta.Text = "";
        }
    }


    protected void ddlTipoOpe_OnIndexChanged(object sender, EventArgs e)
    {
        if (ddlTipoOpe.SelectedValue == "103" || ddlTipoOpe.SelectedValue == "149")
        {
            gvTipoGiros.Visible = true;
            LlenarGVTipoGiros(ddlTipoOpe.SelectedValue);
        }
        else
        {
            gvTipoGiros.Visible = false;
        }
    }


    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true, "txtCodCuenta", "txtNomCuenta");
    }


    #endregion



    #region Metodos que trabajan con la GVTipoGiros


    private void LlenarDatosRegistradosEnGVTipoGiros(int idObjeto, List<GiroDistribucion> lstGiros)
    {
        ProcesoTipoGiroService giroService = new ProcesoTipoGiroService();
        ProcesoTipoGiro giro = new ProcesoTipoGiro() { cod_proceso = idObjeto };
        List<ProcesoTipoGiro> lstGiroCuentas = giroService.ListarProcesoTipoGiro(giro, _usuario);

        // Uno ambas list y bindeo, si no hay nada que unir en la segunda lista solo bindeo la primera 
        // La primera lista solo tiene la columna de tipo de giros, la segunda lista tiene el resto de informacion si la hay
        if (lstGiroCuentas != null && lstGiroCuentas.Count > 0)
        {
            var listadeGirosABindear = from Giros in lstGiros
                                       join GirosConsultados in lstGiroCuentas
                                       on Giros.iddetgiro equals GirosConsultados.tipo_acto into listaFiltrar
                                       from Filtro in listaFiltrar.DefaultIfEmpty()
                                       select new
                                       {
                                           iddetgiro = Giros.iddetgiro,
                                           idprocesogiro = Filtro != null ? Filtro.idprocesogiro : 0,
                                           Descripcion = Giros.Descripcion,
                                           cod_cuenta = Filtro != null ? Filtro.cod_cuenta : "",
                                           descripcion_cod_cuenta = Filtro != null ? Filtro.descripcion_cod_cuenta : ""
                                       };

            gvTipoGiros.DataSource = listadeGirosABindear;
        }
        else
        {
            var listadeGirosABindear = from listaGiros in lstGiros
                                       select new
                                       {
                                           idprocesogiro = 0,
                                           iddetgiro = listaGiros.iddetgiro,
                                           Descripcion = listaGiros.Descripcion,
                                           cod_cuenta = "",
                                           descripcion_cod_cuenta = ""
                                       };

            gvTipoGiros.DataSource = listadeGirosABindear;
        }

        gvTipoGiros.DataBind();
    }


    private List<GiroDistribucion> LlenarGVTipoGiros(string ptipoOpe)
    {
        GiroDistribucionService giroService = new GiroDistribucionService();

        try
        {
            if (ptipoOpe == "149")
                return giroService.listarDDlFormaPagoInv(_usuario);
            else
                return giroService.listarDDlGeneradoEnServices(_usuario);
        }
        catch (Exception ex)
        {
            VerError("Error en llenar GVTipoGiros, " + ex.Message);
            return null;
        }
    }


    protected void btnGVListadoPlan_Click(object sender, EventArgs e)
    {
        ButtonGrid btnListadoPlan = (ButtonGrid)sender;
        if (btnListadoPlan != null)
        {
            int rowIndex = Convert.ToInt32(btnListadoPlan.CommandArgument);
            ctlPlanCuentas ctGVlListadoPlan = (ctlPlanCuentas)gvTipoGiros.Rows[rowIndex].FindControl("ctGVlListadoPlan");
            TextBoxGrid txtGVCodCuenta = (TextBoxGrid)gvTipoGiros.Rows[rowIndex].FindControl("txtGVCodCuenta");
            TextBoxGrid ddlNomCuenta = (TextBoxGrid)gvTipoGiros.Rows[rowIndex].FindControl("txtGVNomCuenta");

            ctGVlListadoPlan.Motrar(true, "txtGVCodCuenta", "txtGVNomCuenta");
        }
    }


    protected void txtGVCodCuenta_TextChanged(object sender, EventArgs e)
    {
        TextBoxGrid txtCodChanged = sender as TextBoxGrid;

        if (txtCodChanged.Text != "")
        {
            // Determinar los datos de la cuenta contable
            PlanCuentasService PlanCuentasServicio = new PlanCuentasService();
            PlanCuentas PlanCuentas = new PlanCuentas();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodChanged.Text, _usuario);

            int rowIndex = Convert.ToInt32(txtCodChanged.CommandArgument);
            TextBoxGrid txtGVNomCuenta = gvTipoGiros.Rows[rowIndex].FindControl("txtGVNomCuenta") as TextBoxGrid;

            // Mostrar el nombre de la cuenta            
            if (txtGVNomCuenta != null)
                txtGVNomCuenta.Text = PlanCuentas.nombre;
        }
    }


    private bool GuardarGVTipoGiros()
    {
        ProcesoTipoGiroService giroService = new ProcesoTipoGiroService();

        try
        {
            List<ProcesoTipoGiro> lstTipoGiros = LlenarListaDeGiros();

            if (lstTipoGiros.Count > 0)
            {
                foreach (var giro in lstTipoGiros)
                {
                    if (giro.idprocesogiro == 0)
                    {
                        giroService.CrearProcesoTipoGiro(giro, _usuario);
                    }
                    else
                    {
                        giroService.ModificarProcesoTipoGiro(giro, _usuario);
                    }            
                }
            }
            else
            {

            }

            return true;
        }
        catch (Exception ex)
        {
            VerError("Error Guardando datos de los giros, " + ex.Message);
            return false;
        }
    }


    private List<ProcesoTipoGiro> LlenarListaDeGiros()
    {
        List<ProcesoTipoGiro> lstTipoGiros = new List<ProcesoTipoGiro>();

        foreach (GridViewRow row in gvTipoGiros.Rows)
        {
            string cod_cuenta = ((TextBoxGrid)row.FindControl("txtGVCodCuenta")).Text;

            if (string.IsNullOrWhiteSpace(cod_cuenta))
            {
                continue;
            }

            ProcesoTipoGiro giro = new ProcesoTipoGiro();
            giro.tipo_acto = Convert.ToInt32(gvTipoGiros.DataKeys[row.RowIndex].Values[0]);
            giro.idprocesogiro = Convert.ToInt32(gvTipoGiros.DataKeys[row.RowIndex].Values[1]);
            giro.cod_cuenta = cod_cuenta;
            giro.cod_proceso = Convert.ToInt32(idObjeto);
            lstTipoGiros.Add(giro);
        }

        return lstTipoGiros;
    }


    #endregion


}