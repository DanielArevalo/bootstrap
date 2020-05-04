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

partial class Nuevo : GlobalWeb
{
    private Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
    private Xpinn.NIIF.Services.PlanCuentasNIIFService PlanNIIFServicio = new Xpinn.NIIF.Services.PlanCuentasNIIFService();
    Xpinn.Contabilidad.Services.PlanCuentasImpuestoService ImpuService = new Xpinn.Contabilidad.Services.PlanCuentasImpuestoService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Request.QueryString["nuevo"] != null)
            {
                PlanNIIFServicio.CodigoProgramaPlan = PlanNIIFServicio.CodigoProgramaNew;
                VisualizarOpciones(PlanNIIFServicio.CodigoProgramaPlan, "A");
            }
            else
            {
                if (Session[PlanNIIFServicio.CodigoProgramaPlan + ".id"] != null || Session[PlanNIIFServicio.CodigoProgramaUpdate + ".id"] != null)
                {
                    //if (Request.QueryString["modificar"] == null)
                    // Esto es para cuando se modifica
                    PlanNIIFServicio.CodigoProgramaPlan = PlanNIIFServicio.CodigoProgramaUpdate;
                    VisualizarOpciones(PlanNIIFServicio.CodigoProgramaPlan, "E");
                }
                else
                {
                    // Esto es para cuando se adiciona
                    PlanNIIFServicio.CodigoProgramaPlan = PlanNIIFServicio.CodigoProgramaNew;
                    VisualizarOpciones(PlanNIIFServicio.CodigoProgramaPlan, "A");
                }
            }

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
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
            BOexcepcion.Throw(PlanNIIFServicio.CodigoProgramaPlan, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["DatosHomo"] = null;

                NoVerObjetos();
                mvComprobante.ActiveViewIndex = 0;
                CargarDll();
                // Colocar por defecto ambos PUC
                lbl2.Visible = false;
                txtPorDistribucion.Visible = false;
                lbl3.Visible = false;
                txtvalorDistribucion.Visible = false;

                if (Session[PlanNIIFServicio.CodigoProgramaPlan + ".id"] != null)
                {
                    idObjeto = Session[PlanNIIFServicio.CodigoProgramaPlan + ".id"].ToString();
                    Session.Remove(PlanNIIFServicio.CodigoProgramaPlan + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    InicializarHomologacion();
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanNIIFServicio.CodigoProgramaPlan, "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    private void InicializarHomologacion()
    {
        List<Xpinn.NIIF.Entities.PlanCtasHomologacionNIF> lstCuentas = new List<Xpinn.NIIF.Entities.PlanCtasHomologacionNIF>();
        for (int i = 0; i < 2; i++)
        {
            lstCuentas.Add(new Xpinn.NIIF.Entities.PlanCtasHomologacionNIF() { idhomologa = -1 });
        }
        gvHomologa.DataSource = lstCuentas;
        gvHomologa.DataBind();
        Session["DatosHomo"] = lstCuentas; 
    }

    private void CargarDll()
    {
        Xpinn.Caja.Services.TipoMonedaService monedaService = new Xpinn.Caja.Services.TipoMonedaService();
        Xpinn.Caja.Entities.TipoMoneda moneda = new Xpinn.Caja.Entities.TipoMoneda();
        ddlMonedas.DataSource = monedaService.ListarTipoMoneda(moneda, (Usuario)Session["Usuario"]);
        ddlMonedas.DataTextField = "descripcion";
        ddlMonedas.DataValueField = "cod_moneda";
        ddlMonedas.DataBind();

        List<Xpinn.NIIF.Entities.PlanCuentasNIIF> LstPlanCuentasNif = new List<Xpinn.NIIF.Entities.PlanCuentasNIIF>();
        Xpinn.NIIF.Entities.PlanCuentasNIIF pPlanCuentasNif = new Xpinn.NIIF.Entities.PlanCuentasNIIF();
        LstPlanCuentasNif = PlanNIIFServicio.ListarPlanCuentasNIIF(pPlanCuentasNif, (Usuario)Session["usuario"]);
        ddlDependedeNif.DataSource = LstPlanCuentasNif;
        ddlDependedeNif.DataTextField = "cod_cuenta_niif";
        ddlDependedeNif.DataValueField = "cod_cuenta_niif";
        ddlDependedeNif.DataBind();

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
            TextBoxGrid txtCodCuenta = (TextBoxGrid)rFila.FindControl("txtCodCuenta");
            pEntidad.cod_cuenta = txtCodCuenta.Text.Trim() != "" ? txtCodCuenta.Text.Trim() : null;
            TextBoxGrid lblNombreCuenta = (TextBoxGrid)rFila.FindControl("lblNombreCuenta");
            pEntidad.nombre_cuenta = lblNombreCuenta.Text.Trim() != "" ? lblNombreCuenta.Text.Trim() : null;
            pEntidad.cod_cuenta_niif = txtCodCuentaNif.Text.Trim();
            lista.Add(pEntidad);
            Session["DatosHomo"] = lista; 
            if (pEntidad.cod_cuenta != null && pEntidad.nombre_cuenta != null)
            {
                lstData.Add(pEntidad);
            }
        }
        return lstData;
    }


    protected void btnDetalle_Click(object sender, EventArgs e)
    {
        ObtenerListaHomologacion();
        List<Xpinn.NIIF.Entities.PlanCtasHomologacionNIF> lstImpuestos = new List<Xpinn.NIIF.Entities.PlanCtasHomologacionNIF>();

        if (Session["DatosHomo"] != null)
        {
            lstImpuestos = (List<Xpinn.NIIF.Entities.PlanCtasHomologacionNIF>)Session["DatosHomo"];

            for (int i = 1; i <= 1; i++)
            {
                Xpinn.NIIF.Entities.PlanCtasHomologacionNIF eImpu = new Xpinn.NIIF.Entities.PlanCtasHomologacionNIF();
                eImpu.idhomologa = -1;
                lstImpuestos.Add(eImpu);
            }
            gvHomologa.PageIndex = gvHomologa.PageCount;
            gvHomologa.DataSource = lstImpuestos;
            gvHomologa.DataBind();

            Session["DatosHomo"] = lstImpuestos;
        }
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.NIIF.Services.PlanCuentasNIIFService BOCuentasNiff = new Xpinn.NIIF.Services.PlanCuentasNIIFService();
            Xpinn.NIIF.Entities.PlanCuentasNIIF vPlanCuentasNiif = new Xpinn.NIIF.Entities.PlanCuentasNIIF();
            String tipo = String.Empty;

            vPlanCuentasNiif = BOCuentasNiff.ConsultarPlanCuentasNIIF(Convert.ToString(pIdObjeto), (Usuario)Session["usuario"]);
            
            if (!string.IsNullOrEmpty(vPlanCuentasNiif.cod_cuenta_niif))
                txtCodCuentaNif.Text = HttpUtility.HtmlDecode(vPlanCuentasNiif.cod_cuenta_niif.ToString().Trim());
            if (!string.IsNullOrEmpty(vPlanCuentasNiif.nombre))
                txtNombreNif.Text = HttpUtility.HtmlDecode(vPlanCuentasNiif.nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(vPlanCuentasNiif.depende_de))
                ddlDependedeNif.SelectedValue = HttpUtility.HtmlDecode(vPlanCuentasNiif.depende_de.ToString());

            if (!string.IsNullOrEmpty(vPlanCuentasNiif.nivel.ToString()))
                ddlNivel.SelectedValue = HttpUtility.HtmlDecode(vPlanCuentasNiif.nivel.ToString().Trim());
            if (!string.IsNullOrEmpty(vPlanCuentasNiif.tipo.ToString()))
                ddlTipo.SelectedValue = HttpUtility.HtmlDecode(vPlanCuentasNiif.tipo.ToString().Trim());
            if (!string.IsNullOrEmpty(vPlanCuentasNiif.cod_moneda.ToString()))
                ddlMonedas.SelectedValue = HttpUtility.HtmlDecode(vPlanCuentasNiif.cod_moneda.ToString().Trim());
            // Determinar si la cuenta contable esta activa
            chkEstado.Checked = false;
            if (!string.IsNullOrEmpty(vPlanCuentasNiif.estado.ToString()))
                if (vPlanCuentasNiif.estado.ToString().Trim() == "1")
                    chkEstado.Checked = true;
            // Determinar si maneja terceros
            chkTerceros.Checked = false;
            if (!string.IsNullOrEmpty(vPlanCuentasNiif.maneja_ter.ToString()))
                if (vPlanCuentasNiif.maneja_ter.ToString().Trim() == "1")
                    chkTerceros.Checked = true;
            // Determinar si maneja centros de costo
            chkCentroCosto.Checked = false;
            if (!string.IsNullOrEmpty(vPlanCuentasNiif.maneja_cc.ToString()))
                if (vPlanCuentasNiif.maneja_cc.ToString().Trim() == "1")
                    chkCentroCosto.Checked = true;
            // Determinar si maneja centro de gestión
            chkCentroGestion.Checked = false;
            if (!string.IsNullOrEmpty(vPlanCuentasNiif.maneja_sc.ToString()))
                if (vPlanCuentasNiif.maneja_sc.ToString().Trim() == "1")
                    chkCentroGestion.Checked = true;
            // Determinar si es una cuenta por pagar
            chkCuentaPagar.Checked = false;
            if (!string.IsNullOrEmpty(vPlanCuentasNiif.maneja_gir.ToString()))
                if (vPlanCuentasNiif.maneja_gir.ToString().Trim() == "1")
                    chkCuentaPagar.Checked = true;
            // Determinar la parametrización para impuestos
            chkImpuestos.Checked = false;
            if (!string.IsNullOrEmpty(vPlanCuentasNiif.impuesto.ToString()))
                if (vPlanCuentasNiif.impuesto.ToString().Trim() == "1")
                    chkImpuestos.Checked = true;
            // Determinar si la cuenta se reporta solamente a nivel mayor a la super
            chkSupersolidaria.Checked = false;
            if (!string.IsNullOrEmpty(vPlanCuentasNiif.reportarmayor.ToString()))
                if (vPlanCuentasNiif.reportarmayor.ToString().Trim() == "1")
                    chkSupersolidaria.Checked = true;

            ////AGREGADO
            //if (vPlanCuentas.cod_tipo_impuesto != null && vPlanCuentas.cod_tipo_impuesto != -1)
            //    ddlTipoImpuesto.SelectedValue = vPlanCuentas.cod_tipo_impuesto.ToString();
            ////
            //if (!string.IsNullOrEmpty(vPlanCuentas.base_minima.ToString()))
            //    txtBaseMinima.Text = HttpUtility.HtmlDecode(vPlanCuentas.base_minima.ToString());
            //if (!string.IsNullOrEmpty(vPlanCuentas.porcentaje_impuesto.ToString()))
            //    txtPorcentajeImpuesto.Text = HttpUtility.HtmlDecode(vPlanCuentas.porcentaje_impuesto.ToString());
            // Determinar parametrización para NIIF

            // Determinar la parte corriente y no corriente
            if (vPlanCuentasNiif.corriente != 0)
                cbCorriente.Checked = true;
            if (vPlanCuentasNiif.nocorriente != 0)
                cbNoCorriente.Checked = true;
            cbCorriente_CheckedChanged(cbCorriente, null);
            if (vPlanCuentasNiif.tipo_distribucion != 0)
                ddlTipoDistribucion.SelectedValue = vPlanCuentasNiif.tipo_distribucion.ToString();
            ddlTipoDistribucion_SelectedIndexChanged(ddlTipoDistribucion, null);
            if (vPlanCuentasNiif.porcentaje_distribucion != 0)
                txtPorDistribucion.Text = vPlanCuentasNiif.porcentaje_distribucion.ToString();
            if (vPlanCuentasNiif.valor_distribucion != 0)
                txtvalorDistribucion.Text = vPlanCuentasNiif.valor_distribucion.ToString();

            //Consultar Homologacion
            List<Xpinn.NIIF.Entities.PlanCtasHomologacionNIF> lstData = new List<Xpinn.NIIF.Entities.PlanCtasHomologacionNIF>();
            string pFiltro = " where P.COD_CUENTA_NIIF = '" + vPlanCuentasNiif.cod_cuenta_niif + "'";
            lstData = PlanNIIFServicio.ListarCuentasHomologadas(pFiltro, "N" ,(Usuario)Session["usuario"]);
            gvHomologa.DataSource = lstData;
            if (lstData.Count > 0)
            {
                gvHomologa.DataBind();
                Session["DatosHomo"] = lstData;
            }
            else
                InicializarHomologacion();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanNIIFServicio.CodigoProgramaPlan, "ObtenerDatos", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Contabilidad.Entities.PlanCuentas vPlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();

            //Declarando variables 
            decimal? baseMin = null, porcenImpu = null;

            vPlanCuentas.cod_cuenta_niif = txtCodCuentaNif.Text;
            vPlanCuentas.nombre_niif = txtNombreNif.Text.Trim();
            vPlanCuentas.estado = Convert.ToInt32(chkEstado.Checked);
            vPlanCuentas.tipo = ddlTipo.SelectedValue;
            vPlanCuentas.nivel = ddlNivel.Text.Trim() == "" ? 0 : Convert.ToInt32(ddlNivel.SelectedValue);
            vPlanCuentas.depende_de_niif = ddlDependedeNif.SelectedValue;
            vPlanCuentas.cod_moneda = Convert.ToInt32(ddlMonedas.SelectedValue);
            vPlanCuentas.maneja_ter = Convert.ToInt32(chkTerceros.Checked);
            vPlanCuentas.maneja_cc = Convert.ToInt32(chkCentroCosto.Checked);
            vPlanCuentas.maneja_sc = Convert.ToInt32(chkCentroGestion.Checked);
            vPlanCuentas.maneja_gir = Convert.ToInt32(chkCuentaPagar.Checked);
            vPlanCuentas.reportarmayor = Convert.ToInt32(chkSupersolidaria.Checked);
            vPlanCuentas.impuesto = Convert.ToInt32(chkImpuestos.Checked);

            vPlanCuentas.base_minima = baseMin;
            vPlanCuentas.porcentaje_impuesto = porcenImpu;

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

            vPlanCuentas.cod_cuenta = "";

            //CAPTURANDO LOS DATOS DE HOMOLOGACION
            List<Xpinn.NIIF.Entities.PlanCtasHomologacionNIF> lstData = new List<Xpinn.NIIF.Entities.PlanCtasHomologacionNIF>();
            lstData = ObtenerListaHomologacion();

            Xpinn.NIIF.Services.PlanCuentasNIIFService planCuentasNIFServicio = new Xpinn.NIIF.Services.PlanCuentasNIIFService();

            if (idObjeto != "")
            {
                vPlanCuentas.cod_cuenta_niif = Convert.ToString(idObjeto);
                planCuentasNIFServicio.ModificarPlanCuentasNIIF(vPlanCuentas, lstData, (Usuario)Session["usuario"]);
            }
            else
            {
                vPlanCuentas = planCuentasNIFServicio.CrearPlanCuentasNIIF(vPlanCuentas, lstData, (Usuario)Session["usuario"]);
                idObjeto = vPlanCuentas.cod_cuenta;
            }

            Session.Remove(PlanCuentasServicio.CodigoPrograma + ".id");
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            mvComprobante.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
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

        if (ddlTipoDistribucion.SelectedValue == "2")
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

    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ButtonGrid btnListadoPlan = (ButtonGrid)sender;
        if (btnListadoPlan != null)
        {
            int rowIndex = Convert.ToInt32(btnListadoPlan.CommandArgument);
            ctlPlanCuentas ctlListadoPlan = (ctlPlanCuentas)gvHomologa.Rows[rowIndex].FindControl("ctlListadoPlan");
            TextBoxGrid txtCodCuenta = (TextBoxGrid)gvHomologa.Rows[rowIndex].FindControl("txtCodCuenta");
            TextBoxGrid lblNombreCuenta = (TextBoxGrid)gvHomologa.Rows[rowIndex].FindControl("lblNombreCuenta");
            ctlListadoPlan.Motrar(true, "txtCodCuenta", "lblNombreCuenta");
        }
    }

    protected void txtCodCuenta_TextChanged(object sender, EventArgs e)
    {
        TextBoxGrid txtCodCuenta = (TextBoxGrid)sender;
        if (txtCodCuenta != null)
        {
            // Determinar los datos de la cuenta contable
            Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
            Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
            
            int rowIndex = Convert.ToInt32(txtCodCuenta.CommandArgument);
            TextBoxGrid lblNombreCuenta = (TextBoxGrid)gvHomologa.Rows[rowIndex].FindControl("lblNombreCuenta");
            if (txtCodCuenta.Text.Trim() != "")
            {
                PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta.Text, (Usuario)Session["usuario"]);
                if (lblNombreCuenta != null)
                    lblNombreCuenta.Text = PlanCuentas.nombre;
            }
            else
                lblNombreCuenta.Text = "";            
        }
    }


    #region METODOS DEL GRIDVIEW


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
                        PlanNIIFServicio.EliminarHomologacionNIIFLocal(id, (Usuario)Session["usuario"]);
                        lstData.Remove(Deta);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
                RegistrarPostBack();
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

}