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
using Xpinn.Contabilidad.Entities;
using Xpinn.Reporteador.Entities;

partial class Nuevo : GlobalWeb
{
    Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
    Xpinn.NIIF.Services.PlanCuentasNIIFService PlanNIIFServicio = new Xpinn.NIIF.Services.PlanCuentasNIIFService();
    Xpinn.Contabilidad.Services.PlanCuentasImpuestoService ImpuService = new Xpinn.Contabilidad.Services.PlanCuentasImpuestoService();
    Xpinn.Reporteador.Services.ExogenaReportService ExogenaService = new Xpinn.Reporteador.Services.ExogenaReportService();
    Usuario _usuario;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[PlanCuentasServicio.CodigoPrograma + ".id"] != null || Session[PlanCuentasServicio.CodigoProgramaModif + ".id"] != null)
            {
                // Esto es para cuando se modifica
                PlanCuentasServicio.CodigoPrograma = PlanCuentasServicio.CodigoProgramaModif;
                VisualizarOpciones(PlanCuentasServicio.CodigoPrograma, "E");                
            }
            else
            {
                // Esto es para cuando se adiciona
                PlanCuentasServicio.CodigoPrograma = PlanCuentasServicio.CodigoProgramaAdic;
                VisualizarOpciones(PlanCuentasServicio.CodigoPrograma, "A");
            }

            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            if (PlanCuentasServicio.CodigoPrograma != PlanCuentasServicio.CodigoProgramaAdic)
            {
                toolBar.MostrarConsultar(true);
                toolBar.MostrarCancelar(true);
            }
            else
            {
                toolBar.MostrarConsultar(false);
                toolBar.MostrarCancelar(false);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanCuentasServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["usuario"];

            if (!IsPostBack)
            {
                Session["DatosImpuesto"] = null;
                Session["DatosHomo"] = null;

                NoVerObjetos();
                mvComprobante.ActiveViewIndex = 0;
                CargarDll();            
                lbl2.Visible = false;
                txtPorDistribucion.Visible = false;
                lbl3.Visible = false;
                txtvalorDistribucion.Visible = false;
             

                if (Session[PlanCuentasServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[PlanCuentasServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(PlanCuentasServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    InicializarTipoImpuestos();
                    InicializarHomologacion();
                }

                chkCentroCosto_CheckedChanged(this, EventArgs.Empty);
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanCuentasServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    private void CargarDll()
    {
        Xpinn.Caja.Services.TipoMonedaService monedaService = new Xpinn.Caja.Services.TipoMonedaService();
        Xpinn.Caja.Entities.TipoMoneda moneda = new Xpinn.Caja.Entities.TipoMoneda();
        ddlMonedas.DataSource = monedaService.ListarTipoMoneda(moneda, _usuario);
        ddlMonedas.DataTextField = "descripcion";
        ddlMonedas.DataValueField = "cod_moneda";
        ddlMonedas.DataBind();

        List<Xpinn.Contabilidad.Entities.PlanCuentas> LstPlanCuentas = new List<Xpinn.Contabilidad.Entities.PlanCuentas>();
        Xpinn.Contabilidad.Entities.PlanCuentas pPlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
        LstPlanCuentas = PlanCuentasServicio.ListarPlanCuentasLocal(pPlanCuentas, _usuario, "");
        ddlDependede.DataSource = LstPlanCuentas;
        ddlDependede.DataTextField = "cod_cuenta";
        ddlDependede.DataValueField = "cod_cuenta";
        ddlDependede.DataBind();

        List<ExogenaReport> LstConceptos = new List<ExogenaReport> ();

        LstConceptos = ExogenaService.TiposConceptos(_usuario);
        ddlConceptos.DataSource = LstConceptos;
        ddlConceptos.DataTextField = "nombre";
        ddlConceptos.DataValueField = "codconcepto";
        ddlConceptos.DataBind();
        ddlConceptos.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

        PoblarListaFromatosDIAN(ddlInformesDian);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
       
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            PlanCuentas vPlanCuentas = new PlanCuentas();

            //Recuperando primera fila
            int cont = 0;
            //Declarando variables 
            int? tipoImpu = null;
            decimal baseMin = 0, porcenImpu = 0;
            if (chkImpuestos.Checked)
            {
                foreach (PlanCuentasImpuesto rData in ObtenerListaImpuestos())
                {
                    if (cont == 0)
                    {
                        if (rData.base_minima != null)
                            baseMin = Convert.ToDecimal(rData.base_minima);
                        if (rData.porcentaje_impuesto != 0 && rData.porcentaje_impuesto != null)
                            porcenImpu = Convert.ToDecimal(rData.porcentaje_impuesto);
                        if (rData.cod_tipo_impuesto != null && rData.cod_tipo_impuesto != 0)
                            tipoImpu = Convert.ToInt32(rData.cod_tipo_impuesto);
                        cont++;
                    }
                }
            }

            if (idObjeto != "")
            {
                PlanCuentasServicio.CodigoPrograma = PlanCuentasServicio.CodigoProgramaModif;
                vPlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(Convert.ToString(idObjeto), _usuario);
            }

            vPlanCuentas.cod_cuenta = txtCodCuentaLocal.Text;
            vPlanCuentas.nombre = Convert.ToString(txtNombre.Text.Trim());
            vPlanCuentas.estado = Convert.ToInt32(chkEstado.Checked);
            vPlanCuentas.tipo = ddlTipo.SelectedValue;
            vPlanCuentas.nivel = ddlNivel.Text == "" ? 0 : Convert.ToInt32(ddlNivel.SelectedValue);
            vPlanCuentas.depende_de = ddlDependede.SelectedValue;
            vPlanCuentas.cod_moneda = Convert.ToInt32(ddlMonedas.SelectedValue);
            vPlanCuentas.maneja_ter = Convert.ToInt32(chkTerceros.Checked);
            vPlanCuentas.maneja_cc = Convert.ToInt32(chkCentroCosto.Checked);
            vPlanCuentas.maneja_sc = Convert.ToInt32(chkCentroGestion.Checked);
            vPlanCuentas.maneja_gir = Convert.ToInt32(chkCuentaPagar.Checked);

            vPlanCuentas.maneja_traslado = 0;
            if (chkTrasladoSaldos.Checked)
            {
                VerError("");
                if (!chkTerceros.Checked)
                {
                    VerError("La cuenta no maneja terceros, no puede permitir el traslado de saldos a terceros");
                    chkTerceros.Checked = false;
                    return;
                }
                else
                    vPlanCuentas.maneja_traslado = Convert.ToInt32(chkTrasladoSaldos.Checked);
            }

            foreach (GridViewRow wrow in gvImpuestos.Rows)
            {
                CheckBoxGrid cbAsumido = (CheckBoxGrid)wrow.FindControl("cbAsumido");
                TextBoxGrid txtcodcuenta = (TextBoxGrid)wrow.FindControl("txtCodCuenta_imps");
                vPlanCuentas.asumido = Convert.ToInt32(cbAsumido.Checked);
                vPlanCuentas.cod_cuenta_asumido = Convert.ToString(txtcodcuenta.Text);
            }

            vPlanCuentas.impuesto = Convert.ToInt32(chkImpuestos.Checked);
            vPlanCuentas.base_minima = baseMin;
            vPlanCuentas.porcentaje_impuesto = porcenImpu;

            vPlanCuentas.cod_cuenta_niif =null;
            vPlanCuentas.nombre_niif = "";
            vPlanCuentas.depende_de_niif = "";

            if (cbCorriente.Checked) vPlanCuentas.corriente = 1; else vPlanCuentas.corriente = 0;
            if (cbNoCorriente.Checked) vPlanCuentas.nocorriente = 1; else vPlanCuentas.nocorriente = 0;

            if (ddlTipoDistribucion.Visible == true)
                if (ddlTipoDistribucion.SelectedValue != "0") vPlanCuentas.tipo_distribucion = Convert.ToInt32(ddlTipoDistribucion.SelectedValue); else vPlanCuentas.tipo_distribucion = -1;
            else
                vPlanCuentas.tipo_distribucion = -1;
            if (txtPorDistribucion.Visible == true)
                if (txtPorDistribucion.Text != "0")
                    if (txtPorDistribucion.Text != "")
                        vPlanCuentas.porcentaje_distribucion = Convert.ToDecimal(txtPorDistribucion.Text);
                    else
                        vPlanCuentas.porcentaje_distribucion = -1;
                else
                    vPlanCuentas.porcentaje_distribucion = -1;
            else
                vPlanCuentas.porcentaje_distribucion = -1;

            if (txtvalorDistribucion.Visible == true)
                if (txtvalorDistribucion.Text != "0")
                    if (txtvalorDistribucion.Text != "")
                        vPlanCuentas.valor_distribucion = Convert.ToDecimal(txtvalorDistribucion.Text);
                    else
                        vPlanCuentas.valor_distribucion = -1;
                else
                    vPlanCuentas.valor_distribucion = -1;
            else
                vPlanCuentas.valor_distribucion = -1;

            if (tipoImpu != null)
                vPlanCuentas.cod_tipo_impuesto = Convert.ToInt32(tipoImpu);
            else
                vPlanCuentas.cod_tipo_impuesto = -1;


            if (chkImpuestos.Checked)
            {
                vPlanCuentas.lstImpuestos = new List<PlanCuentasImpuesto>();
                vPlanCuentas.lstImpuestos = ObtenerListaImpuestos();
            }
            else
            {
                vPlanCuentas.lstImpuestos = new List<PlanCuentasImpuesto>();
                vPlanCuentas.lstImpuestos = null;
            }


            if (chkCentroCosto.Checked)
            {
                vPlanCuentas.cod_cuenta_centro_costo = txtCodCuentaContable.Text;
                vPlanCuentas.cod_cuenta_contrapartida = txtCodContrapartida.Text;
            }


            List<Xpinn.NIIF.Entities.PlanCtasHomologacionNIF> lstData = new List<Xpinn.NIIF.Entities.PlanCtasHomologacionNIF>();
            lstData = ObtenerListaHomologacion();


            //Homologación DIAN
            ExogenaReport pExogena = new ExogenaReport();
            if (ddlConceptos.SelectedItem.Value != "0" || ddlInformesDian.SelectedItem.Value != "0")
            {
                pExogena.cod_cuenta = txtCodCuentaLocal.Text;
                pExogena.codconcepto = Convert.ToInt64(ddlConceptos.SelectedItem.Value);
                pExogena.Formato = ddlInformesDian.SelectedItem.Value;
            }
            else
            {
                pExogena.codconcepto = 0;
                pExogena.Formato = "0";
            }
            //
          

                if (idObjeto != "")
                {
                    pExogena.idhomologa = Convert.ToInt64(Session["homologaDIAN"]);
                    vPlanCuentas.cod_cuenta = Convert.ToString(idObjeto);
                    PlanCuentasServicio.ModificarPlanCuentas(vPlanCuentas, lstData, _usuario, pExogena);
                }
                else
                {
                    pExogena.idhomologa = 0;
                if (PlanCuentasServicio.VerficarAuxiliar(vPlanCuentas.depende_de,_usuario)==true)
                {
                    vPlanCuentas = PlanCuentasServicio.CrearPlanCuentas(vPlanCuentas, lstData, _usuario, pExogena);
                    idObjeto = vPlanCuentas.cod_cuenta;

                    // Cargar variable de sesión
                    Session[PlanCuentasServicio.CodigoPrograma + ".id"] = idObjeto;

                    // Ir al link correspondiente según el tipo de operación a realizar
                    if (PlanCuentasServicio.CodigoPrograma == PlanCuentasServicio.CodigoProgramaAdic)
                    {
                        //Navegar(Pagina.Detalle);
                        Session.Remove(PlanCuentasServicio.CodigoPrograma + ".id");
                    }

                    Site toolBar = (Site)Master;
                    toolBar.MostrarGuardar(false);
                    mvComprobante.ActiveViewIndex = 1;

                    //else if (PlanCuentasServicio.CodigoPrograma == PlanCuentasServicio.CodigoProgramaModif)
                    //    Navegar(Pagina.Modificar);
                    //else
                    //    Navegar(Pagina.Lista);
                }
                else
                {
                    VerError("La cuenta es auxiliar con saldo ");
                }
                  

                }
            
            
          
        }
        catch (Exception ex)
        {
            VerError("Ocurrio un problema al realizar la operacion de guardado, " + ex.Message);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.NIIF.Services.PlanCuentasNIIFService BOCuentasNiff = new Xpinn.NIIF.Services.PlanCuentasNIIFService();
            Xpinn.Contabilidad.Entities.PlanCuentas vPlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
            Xpinn.NIIF.Entities.PlanCuentasNIIF vPlanCuentasNiif = new Xpinn.NIIF.Entities.PlanCuentasNIIF();
            String tipo = String.Empty;
            if (Session[PlanCuentasServicio.CodigoProgramaModif + ".tipo"] != null)
            {
                tipo = Session[PlanCuentasServicio.CodigoProgramaModif + ".tipo"].ToString();
                Session.Remove(PlanCuentasServicio.CodigoProgramaModif + ".tipo");
            }
            vPlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(Convert.ToString(pIdObjeto), _usuario);
            if (pIdObjeto != null)
            { 
                if (vPlanCuentas == null)
                {
                    VerError("No pudo determinar datos de la cuenta contable ->" + pIdObjeto + "<-");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(vPlanCuentas.cod_cuenta))
                txtCodCuentaLocal.Text = HttpUtility.HtmlDecode(vPlanCuentas.cod_cuenta.ToString().Trim());
            if (!string.IsNullOrEmpty(vPlanCuentas.nombre))
                txtNombre.Text = HttpUtility.HtmlDecode(vPlanCuentas.nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(vPlanCuentas.nivel.ToString()))
                ddlNivel.SelectedValue = HttpUtility.HtmlDecode(vPlanCuentas.nivel.ToString().Trim());
            if (!string.IsNullOrEmpty(vPlanCuentas.tipo.ToString()))
                ddlTipo.SelectedValue = HttpUtility.HtmlDecode(vPlanCuentas.tipo.ToString().Trim());
            if (!string.IsNullOrEmpty(vPlanCuentas.cod_moneda.ToString()))
                ddlMonedas.SelectedValue = HttpUtility.HtmlDecode(vPlanCuentas.cod_moneda.ToString().Trim());
            if (vPlanCuentas.depende_de != null)
                ddlDependede.SelectedValue = HttpUtility.HtmlDecode(vPlanCuentas.depende_de.ToString());
            chkEstado.Checked = false;
            if (!string.IsNullOrEmpty(vPlanCuentas.estado.ToString()))
                if (vPlanCuentas.estado.ToString().Trim() == "1")
                    chkEstado.Checked = true;
            chkTerceros.Checked = false;
            if (!string.IsNullOrEmpty(vPlanCuentas.maneja_ter.ToString()))
                if (vPlanCuentas.maneja_ter.ToString().Trim() == "1")
                    chkTerceros.Checked = true;
            chkCentroCosto.Checked = false;
            if (!string.IsNullOrEmpty(vPlanCuentas.maneja_cc.ToString()))
                if (vPlanCuentas.maneja_cc.ToString().Trim() == "1")
                    chkCentroCosto.Checked = true;
            chkCentroGestion.Checked = false;
            if (!string.IsNullOrEmpty(vPlanCuentas.maneja_sc.ToString()))
                if (vPlanCuentas.maneja_sc.ToString().Trim() == "1")
                    chkCentroGestion.Checked = true;
            chkCuentaPagar.Checked = false;
            if (!string.IsNullOrEmpty(vPlanCuentas.maneja_gir.ToString()))
                if (vPlanCuentas.maneja_gir.ToString().Trim() == "1")
                    chkCuentaPagar.Checked = true;
            // Determinar la parametrización para impuestos
            chkImpuestos.Checked = false;
            if (!string.IsNullOrEmpty(vPlanCuentas.impuesto.ToString()))
                if (vPlanCuentas.impuesto.ToString().Trim() == "1")
                    chkImpuestos.Checked = true;

            if (!string.IsNullOrEmpty(vPlanCuentas.maneja_traslado.ToString()))
                if (vPlanCuentas.maneja_traslado.ToString().Trim() == "1")
                    chkTrasladoSaldos.Checked = true;

            txtCodCuentaContable.Text = vPlanCuentas.cod_cuenta_centro_costo;
            txtCodContrapartida.Text = vPlanCuentas.cod_cuenta_contrapartida;

            txtCodCuentaContable_TextChanged(this, EventArgs.Empty);
            txtCodContrapartida_TextChanged(this, EventArgs.Empty);

            ////AGREGADO
            //if (vPlanCuentas.cod_tipo_impuesto != null && vPlanCuentas.cod_tipo_impuesto != -1)
            //    ddlTipoImpuesto.SelectedValue = vPlanCuentas.cod_tipo_impuesto.ToString();
            ////
            //if (!string.IsNullOrEmpty(vPlanCuentas.base_minima.ToString()))
            //    txtBaseMinima.Text = HttpUtility.HtmlDecode(vPlanCuentas.base_minima.ToString());
            //if (!string.IsNullOrEmpty(vPlanCuentas.porcentaje_impuesto.ToString()))
            //    txtPorcentajeImpuesto.Text = HttpUtility.HtmlDecode(vPlanCuentas.porcentaje_impuesto.ToString());
            chkImpuestos_CheckedChanged(chkImpuestos, null);

            // Determinar la parte corriente y no corriente
            if (vPlanCuentas.corriente != 0)
                cbCorriente.Checked = true;
            if (vPlanCuentas.nocorriente != 0)
                cbNoCorriente.Checked = true;
            cbCorriente_CheckedChanged(cbCorriente, null);
            if (vPlanCuentas.tipo_distribucion != 0)
                ddlTipoDistribucion.SelectedValue = vPlanCuentas.tipo_distribucion.ToString();
            ddlTipoDistribucion_SelectedIndexChanged(ddlTipoDistribucion, null);
            if (vPlanCuentas.porcentaje_distribucion != 0)
                txtPorDistribucion.Text = vPlanCuentas.porcentaje_distribucion.ToString();
            
            if (vPlanCuentas.valor_distribucion != 0)
                txtvalorDistribucion.Text = vPlanCuentas.valor_distribucion.ToString();

            //CONSULTANDO IMPUESTOS
            List<PlanCuentasImpuesto> lstImpuesto = new List<PlanCuentasImpuesto>();
            PlanCuentasImpuesto pImpuesto = new PlanCuentasImpuesto();
            string filtro = " WHERE COD_CUENTA = '" + txtCodCuentaLocal.Text + "'";

            lstImpuesto = ImpuService.ListarPlanCuentasImpuesto(pImpuesto, filtro, _usuario);

            if (lstImpuesto.Count > 0)
            {
                gvImpuestos.DataSource = lstImpuesto;
                gvImpuestos.DataBind();
            }
            else
                InicializarTipoImpuestos();

            //Consultar Homologacion
            List<Xpinn.NIIF.Entities.PlanCtasHomologacionNIF> lstData = new List<Xpinn.NIIF.Entities.PlanCtasHomologacionNIF>();
            string pFiltro = " where P.COD_CUENTA = '" + vPlanCuentas.cod_cuenta + "'";
            lstData = PlanNIIFServicio.ListarCuentasHomologadas(pFiltro, "L", _usuario);
            gvHomologa.DataSource = lstData;
            if (lstData.Count > 0)
            {
                gvHomologa.DataBind();
                Session["DatosHomo"] = lstData;
            }
            else
                InicializarHomologacion();
            //Consultar Homologacion DIAN
            List<ExogenaReport> lstExo = new List<ExogenaReport>();
            lstExo = ExogenaService.lstHomologaDian(Convert.ToString(pIdObjeto),_usuario);
            if (lstExo.Count>0)
            {
                foreach (ExogenaReport item in lstExo)
                {
                    Session["homologaDIAN"] = item.idhomologa;
                    ddlConceptos.SelectedValue =item.codconcepto.ToString();
                    ddlInformesDian.SelectedIndex = Convert.ToInt32(item.Formato);
                }
               
            }
            else
            {
                Session["homologaDIAN"] = 0;
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanCuentasServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    
    protected void ddlDependede_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Contabilidad.Entities.PlanCuentas ePlan = new Xpinn.Contabilidad.Entities.PlanCuentas();
            ePlan = PlanCuentasServicio.ConsultarPlanCuentas(ddlDependede.SelectedValue, _usuario);
            if (txtCodCuentaLocal.Text.Trim() == "")
            {
                txtCodCuentaLocal.Text = ePlan.cod_cuenta;
                string sNivel = Convert.ToString(ePlan.nivel + 1);
                ListItem selectActual = ddlNivel.SelectedItem;
                selectActual.Selected = false;
                ListItem selectedListItem = ddlNivel.Items.FindByValue(sNivel);
                if (selectedListItem != null)
                    selectedListItem.Selected = true;
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void chkImpuestos_CheckedChanged(object sender, EventArgs e)
    {
        if (chkImpuestos.Checked)
        {
            panelGrilla.Visible = true;
            btnDetalle.Visible = true;
        }
        else
        {
            panelGrilla.Visible = false;
            btnDetalle.Visible = false;
        }
    }

    void VerObjetos()
    {
        lbl1.Visible = true;        
        ddlTipoDistribucion.Visible = true;
    }

   

    void NoVerObjetos()
    {
        lbl1.Visible = false;
        ddlTipoDistribucion.Visible = false;
    }

    protected void cbNoCorriente_CheckedChanged(object sender, EventArgs e)
    {
        if (cbCorriente.Checked && cbNoCorriente.Checked)
        {
            VerObjetos();
        }
        else
        {
            NoVerObjetos();
        }
    }

    protected void cbCorriente_CheckedChanged(object sender, EventArgs e)
    {
        if (cbNoCorriente.Checked && cbCorriente.Checked)
        {
            VerObjetos();
        }
        else 
        {
            NoVerObjetos();
        }
    }
    protected void ddlTipoDistribucion_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbl2.Visible = false;
        txtPorDistribucion.Visible = false;
        lbl3.Visible = false;
        txtvalorDistribucion.Visible = false;

        if (ddlTipoDistribucion.SelectedValue == "1")
        {
            lbl3.Visible = true;
            txtvalorDistribucion.Visible = true;
        }
        else
        {
            lbl3.Visible = false;
            txtvalorDistribucion.Visible = false;
        }
     
         if(ddlTipoDistribucion.SelectedValue == "2")
        {
            lbl2.Visible = true;
            txtPorDistribucion.Visible = true;
        }
        else
        {
            lbl2.Visible = false;
            txtPorDistribucion.Visible = false;
        }
    }


    protected void gvImpuestos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {            
            DropDownListGrid ddlTipoImpuesto = (DropDownListGrid)e.Row.FindControl("ddlTipoImpuesto");           
            List<Xpinn.Contabilidad.Entities.PlanCuentas> lstTipo = new List<Xpinn.Contabilidad.Entities.PlanCuentas>();
            Xpinn.Contabilidad.Entities.PlanCuentas vData = new Xpinn.Contabilidad.Entities.PlanCuentas();
            lstTipo = PlanCuentasServicio.ListarTipoImpuesto(vData, _usuario);
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

            CheckBoxGrid cbAsumido = (CheckBoxGrid)e.Row.FindControl("cbAsumido");
            if (cbAsumido != null)
            {
                cbAsumido.CheckedChanged += cbAsumido_CheckedChanged;
                HabilitarAsumido(e.Row); 
            }
        }
    }

    protected void gvImpuestos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvImpuestos.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaImpuestos();

        List<PlanCuentasImpuesto> LstActi;
        LstActi = (List<PlanCuentasImpuesto>)Session["DatosImpuesto"];

        try
        {
            foreach (PlanCuentasImpuesto acti in LstActi)
            {
                if (acti.idimpuesto == conseID)
                {
                    if (conseID > 0)
                        ImpuService.EliminarPlanCuentaImpuesto(conseID, _usuario);
                    LstActi.Remove(acti);
                    break;
                }
            }
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }

        gvImpuestos.DataSourceID = null;
        gvImpuestos.DataBind();

        gvImpuestos.DataSource = LstActi;
        gvImpuestos.DataBind();

        Session["DatosImpuesto"] = LstActi;
    }


    protected void InicializarTipoImpuestos()
    {
        List<PlanCuentasImpuesto> lstImpuestos = new List<PlanCuentasImpuesto>();

        for (int i = gvImpuestos.Rows.Count; i < 2; i++)
        {
            PlanCuentasImpuesto eImpu = new PlanCuentasImpuesto();
            eImpu.idimpuesto = -1;
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


    protected List<PlanCuentasImpuesto> ObtenerListaImpuestos()//Int64 cod
    {
        List<PlanCuentasImpuesto> lstImpuestos = new List<PlanCuentasImpuesto>();
        List<PlanCuentasImpuesto> lista = new List<PlanCuentasImpuesto>();


        foreach (GridViewRow rfila in gvImpuestos.Rows)
        {
            PlanCuentasImpuesto eImpu = new PlanCuentasImpuesto();
            Label lblCodigo = (Label)rfila.FindControl("lblCodigo");

            if (lblCodigo != null)
                eImpu.idimpuesto = Convert.ToInt32(lblCodigo.Text);
            
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
            if(txtCodCuenta_imp.Text != "")
                eImpu.cod_cuenta_imp = txtCodCuenta_imp.Text;

            CheckBoxGrid cbAsumido = (CheckBoxGrid)rfila.FindControl("cbAsumido");
            if (cbAsumido != null)
                eImpu.asumido = cbAsumido.Checked == true ? 1 : 0;

            TextBoxGrid txtCodCuenta_imps = (TextBoxGrid)rfila.FindControl("txtCodCuenta_imps");
            if (txtCodCuenta_imps != null)
                eImpu.cod_cuenta_asumido = txtCodCuenta_imps.Text;

            lista.Add(eImpu);
            Session["DatosImpuesto"] = lista;

            if (eImpu.porcentaje_impuesto != null && eImpu.cod_cuenta_imp != null && ddlTipoImpuesto.SelectedIndex != 0)
            {
                lstImpuestos.Add(eImpu);
            }
        }
        return lstImpuestos;
    }

    protected void btnDetalle_Click(object sender, EventArgs e)
    {
        ObtenerListaImpuestos();
        List<PlanCuentasImpuesto> lstImpuestos = new List<PlanCuentasImpuesto>();


        if (Session["DatosImpuesto"] != null)
        {
            lstImpuestos = (List<PlanCuentasImpuesto>)Session["DatosImpuesto"];

            for (int i = 1; i <= 1; i++)
            {
                PlanCuentasImpuesto eImpu = new PlanCuentasImpuesto();
                eImpu.idimpuesto = -1;
                eImpu.cod_tipo_impuesto = null;
                eImpu.porcentaje_impuesto = null;
                eImpu.base_minima = null;
                eImpu.cod_cuenta_imp = "";
                eImpu.asumido = 0;
                lstImpuestos.Add(eImpu);
            }
            gvImpuestos.PageIndex = gvImpuestos.PageCount;
            gvImpuestos.DataSource = lstImpuestos;
            gvImpuestos.DataBind();

            Session["DatosImpuesto"] = lstImpuestos;
        }
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


    protected void btnListadoPlanAsumido_Click(object sender, EventArgs e)
    {
        ButtonGrid btnListadoPlan = (ButtonGrid)sender;
        if (btnListadoPlan != null)
        {
            int rowIndex = Convert.ToInt32(btnListadoPlan.CommandArgument);
            ctlPlanCuentas ctlListadoPlan = (ctlPlanCuentas)gvImpuestos.Rows[rowIndex].FindControl("ctlListadoPlasn");
            TextBoxGrid txtCodCuenta_imp = (TextBoxGrid)gvImpuestos.Rows[rowIndex].FindControl("txtCodCuenta_imps");
            ctlListadoPlan.Motrar(true, "txtCodCuenta_imps", "");
        }
    }


    protected void HabilitarAsumido(GridViewRow pgvImpFila)
    {
        CheckBoxGrid cbAsumido = (CheckBoxGrid)pgvImpFila.FindControl("cbAsumido");
        if (cbAsumido != null)
        {
            TextBoxGrid txtCodCuenta_imps = (TextBoxGrid)pgvImpFila.FindControl("txtCodCuenta_imps");
            if (txtCodCuenta_imps != null)
            {
                if (cbAsumido.Checked == true)
                    txtCodCuenta_imps.Visible = true;
                else
                    txtCodCuenta_imps.Visible = false;
                if (txtCodCuenta_imps.Visible == false)
                    txtCodCuenta_imps.Text = "";
            }
            ButtonGrid btnListadoPlans = (ButtonGrid)pgvImpFila.FindControl("btnListadoPlans");
            if (btnListadoPlans != null)
            {
                if (cbAsumido.Checked == true)
                    btnListadoPlans.Visible = true;
                else
                    btnListadoPlans.Visible = false;
            }
        }
    }

    protected void cbAsumido_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxGrid cbAsumido = (CheckBoxGrid)sender;
        if (cbAsumido != null)
        {
            if (cbAsumido.CommandArgument != "")
            {
                try
                {
                    int rowIndex = Convert.ToInt32(cbAsumido.CommandArgument);
                    if (rowIndex >= 0)                       
                        HabilitarAsumido(gvImpuestos.Rows[rowIndex]);
                }
                catch
                {
                    return;
                }
            }
        }
    }


    #region Eventos del GridView Homologación

    protected void txtCodCuenta_TextChanged(object sender, EventArgs e)
    {
        TextBoxGrid txtCodCuenta = (TextBoxGrid)sender;
        if (txtCodCuenta != null)
        {
            // Determinar los datos de la cuenta contable
            Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
            Xpinn.NIIF.Entities.PlanCuentasNIIF PlanCuentas = new Xpinn.NIIF.Entities.PlanCuentasNIIF();

            int rowIndex = Convert.ToInt32(txtCodCuenta.CommandArgument);
            TextBoxGrid lblNombreCuenta = (TextBoxGrid)gvHomologa.Rows[rowIndex].FindControl("lblNombreCuenta");
            if (txtCodCuenta.Text.Trim() != "")
            {
                PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentasNIIF(txtCodCuenta.Text, _usuario);
                if (lblNombreCuenta != null)
                    lblNombreCuenta.Text = PlanCuentas.nombre;
            }
            else
                lblNombreCuenta.Text = "";
        }
    }


    protected void btnListadoPlanHomo_Click(object sender, EventArgs e)
    {
        ButtonGrid btnListadoPlanHomo = (ButtonGrid)sender;
        if (btnListadoPlanHomo != null)
        {
            int rowIndex = Convert.ToInt32(btnListadoPlanHomo.CommandArgument);
            ctlPlanCuentasNif ctlListadoPlanHomo = (ctlPlanCuentasNif)gvHomologa.Rows[rowIndex].FindControl("ctlListadoPlanHomo");
            //ctlPlanCuentas ctlListadoPlanHomo = (ctlPlanCuentas)gvHomologa.Rows[rowIndex].FindControl("ctlListadoPlanHomo");
            TextBoxGrid txtCodCuenta = (TextBoxGrid)gvHomologa.Rows[rowIndex].FindControl("txtCodCuenta");
            TextBoxGrid lblNombreCuenta = (TextBoxGrid)gvHomologa.Rows[rowIndex].FindControl("lblNombreCuenta");
            ctlListadoPlanHomo.Motrar(true, "txtCodCuenta", "lblNombreCuenta");
        }
    }

    private void InicializarHomologacion()
    {
        List<Xpinn.NIIF.Entities.PlanCtasHomologacionNIF> lstCuentas = new List<Xpinn.NIIF.Entities.PlanCtasHomologacionNIF>();
        for (int i = 0; i < 1; i++)
        {
            lstCuentas.Add(new Xpinn.NIIF.Entities.PlanCtasHomologacionNIF() { idhomologa = -1 });
        }
        gvHomologa.DataSource = lstCuentas;
        gvHomologa.DataBind();
        Session["DatosHomo"] = lstCuentas;
    }

    private List<Xpinn.NIIF.Entities.PlanCtasHomologacionNIF> ObtenerListaHomologacion()
    {
        List<Xpinn.NIIF.Entities.PlanCtasHomologacionNIF> lstData = new List<Xpinn.NIIF.Entities.PlanCtasHomologacionNIF>();
        List<Xpinn.NIIF.Entities.PlanCtasHomologacionNIF> lista = new List<Xpinn.NIIF.Entities.PlanCtasHomologacionNIF>();
        foreach (GridViewRow rFila in gvHomologa.Rows)
        {
            Xpinn.NIIF.Entities.PlanCtasHomologacionNIF pEntidad = new Xpinn.NIIF.Entities.PlanCtasHomologacionNIF();
            Int64 idhomologa = 0;
            if (gvHomologa.DataKeys[rFila.RowIndex].Value != null)
            {
                string pCampo = gvHomologa.DataKeys[rFila.RowIndex].Value.ToString();
                if (Convert.ToInt32(pCampo) > 0)
                    idhomologa = Convert.ToInt64(gvHomologa.DataKeys[rFila.RowIndex].Value.ToString());
            }
            pEntidad.idhomologa = idhomologa;
            pEntidad.cod_cuenta = txtCodCuentaLocal.Text;
            TextBoxGrid txtCodCuentaNif = (TextBoxGrid)rFila.FindControl("txtCodCuenta");
            pEntidad.cod_cuenta_niif = txtCodCuentaNif.Text.Trim() != "" ? txtCodCuentaNif.Text.Trim() : null;
            TextBoxGrid lblNombreCuenta = (TextBoxGrid)rFila.FindControl("lblNombreCuenta");
            pEntidad.nombre_cuenta = lblNombreCuenta.Text.Trim() != "" ? lblNombreCuenta.Text.Trim() : null;
            
            lista.Add(pEntidad);
            Session["DatosHomo"] = lista;
            if (pEntidad.cod_cuenta != null && pEntidad.nombre_cuenta != null)
            {
                lstData.Add(pEntidad);
            }
        }
        return lstData;
    }

    protected void gvHomologa_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ObtenerListaHomologacion();
        List<Xpinn.NIIF.Entities.PlanCtasHomologacionNIF> lstData = new List<Xpinn.NIIF.Entities.PlanCtasHomologacionNIF>();
        lstData = (List<Xpinn.NIIF.Entities.PlanCtasHomologacionNIF>)Session["DatosHomo"];
        Int64 id = Convert.ToInt64(gvHomologa.DataKeys[e.RowIndex].Value.ToString());
        if (id > 0)
        {
            try
            {
                foreach (Xpinn.NIIF.Entities.PlanCtasHomologacionNIF Deta in lstData)
                {
                    if (Deta.idhomologa == id)
                    {
                        PlanNIIFServicio.EliminarHomologacionNIIFLocal(id, _usuario);
                        lstData.Remove(Deta);
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
            lstData.RemoveAt(e.RowIndex);
        gvHomologa.DataSource = null;
        gvHomologa.DataBind();

        gvHomologa.DataSource = lstData;
        gvHomologa.DataBind();

        if (gvHomologa.Rows.Count == 0)
            InicializarHomologacion();
        Session["DatosHomo"] = lstData;
    }


    #endregion


    protected void chkCentroCosto_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCentroCosto.Checked)
        {
            pnlCuentaContable.Visible = true;
            pnlContraPartida.Visible = true;
        }
        else
        {
            pnlCuentaContable.Visible = false;
            pnlContraPartida.Visible = false;
        }
    }


    protected void btnListadoPlanContable_Click(object sender, EventArgs e)
    {
        ctlListadoPlanContable.Motrar(true, "txtCodCuentaContable", "txtNomCuentaContable");
    }


    protected void txtCodCuentaContable_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtCodCuentaContable.Text))
        {
            PlanCuentas PlanCuentas = ConsultarNombreCuenta(txtCodCuentaContable.Text);

            if (PlanCuentas != null)
            {
                // Mostrar el nombre de la cuenta            
                txtNomCuentaContable.Text = PlanCuentas.nombre;
            }
        }
        else
        {
            txtNomCuentaContable.Text = string.Empty;
        }
    }


    protected void btnListadoPlanContraPartida_Click(object sender, EventArgs e)
    {
        ctlListadoPlanContraPartida.Motrar(true, "txtCodContrapartida", "txtNomCuentaContraPartida");
    }


    protected void txtCodContrapartida_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtCodContrapartida.Text))
        {
            PlanCuentas PlanCuentas = ConsultarNombreCuenta(txtCodContrapartida.Text);

            if (PlanCuentas != null)
            {
                // Mostrar el nombre de la cuenta            
                txtNomCuentaContraPartida.Text = PlanCuentas.nombre;
            }
        }
        else
        {
            txtNomCuentaContraPartida.Text = string.Empty;
        }
    }


    private PlanCuentas ConsultarNombreCuenta(string codCuenta)
    {
        // Determinar los datos de la cuenta contable
        Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
        PlanCuentas PlanCuentas = new PlanCuentas();

        return PlanCuentasServicio.ConsultarPlanCuentas(codCuenta, _usuario);
    }


    protected void chkTrasladoSaldos_CheckedChanged(object sender, EventArgs e)
    {
        VerError("");
        if (chkTrasladoSaldos.Checked)
        {
            if (!chkTerceros.Checked)
            {
                VerError("La cuenta no maneja terceros, no puede permitir el traslado de saldos a terceros");
                chkTerceros.Checked = false;
            }
        }
    }
    protected void btnImp_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("~/Page/Contabilidad/Conceptos_DIAN/Importar/Detalle.aspx");
    }
}