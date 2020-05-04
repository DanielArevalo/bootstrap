using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using System.Linq;

partial class Lista : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.CreditoService CreditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();
    PoblarListas poblarLista = new PoblarListas();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {

            VisualizarOpciones(CreditoServicio.CodigoProgramaoriginal, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaoriginal, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDropdown();
                txtFechaRealiza.ToDateTime = DateTime.Now;
                pDatos.Visible = false;
                ddlForma_Desem_SelectedIndexChanged(ddlForma_Desem, null);
                CargarValoresConsulta(pConsulta, CreditoServicio.CodigoProgramaoriginal);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaoriginal, "Page_Load", ex);
        }
    }



    #region METHODS

    protected void CargarDropdown()
    {
        OficinaService oficinaService = new OficinaService();

        int consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas((int)Usuario.codusuario, (Usuario)Session["Usuario"]);
        if (consulta >= 1)
        {
            poblarLista.PoblarListaDesplegable("OFICINA", "COD_OFICINA, NOMBRE", "ESTADO = 1", "NOMBRE", ddlOficinas, Usuario);
            ddlOficinas.SelectedValue = Usuario.cod_oficina.ToString();
            ddlOficinas.Enabled = true;
        }
        else
        {
            ddlOficinas.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            ddlOficinas.Items.Insert(1, new ListItem(Usuario.nombre_oficina, Usuario.cod_oficina.ToString()));
            ddlOficinas.DataBind();
            ddlOficinas.SelectedValue = Usuario.cod_oficina.ToString();
            ddlOficinas.Enabled = false;
        }
        poblarLista.PoblarListaDesplegable("LINEASCREDITO", "COD_LINEA_CREDITO, NOMBRE", "ESTADO = 1", "NOMBRE", ddlLineaCredito, Usuario);
        poblarLista.PoblarListaDesplegable("TIPOTASA", ddltipotasa, Usuario);

        ddlForma_Desem.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        ddlForma_Desem.Items.Insert(1, new ListItem("Efectivo", "1"));
        ddlForma_Desem.Items.Insert(2, new ListItem("Cheque", "2"));
        ddlForma_Desem.Items.Insert(3, new ListItem("Transferencia", "3"));
        ddlForma_Desem.Items.Insert(4, new ListItem("Otros", "4"));
        ddlForma_Desem.SelectedIndex = 0;
        ddlForma_Desem.DataBind();

        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        ddlEntidad_giro.DataSource = bancoService.ListarBancosEntidad(new Xpinn.Caja.Entities.Bancos(), (Usuario)Session["usuario"]);
        ddlEntidad_giro.DataTextField = "nombrebanco";
        ddlEntidad_giro.DataValueField = "cod_banco";
        ddlEntidad_giro.DataBind();

    }

    private void ActivarDesembolso()
    {
        TipoFormaDesembolso formaDesembolso = ddlForma_Desem.SelectedValue.ToEnum<TipoFormaDesembolso>();
        if (ddlForma_Desem.SelectedValue != "")
        {
            pnlCtasBanc.Visible = false;
            if (formaDesembolso == TipoFormaDesembolso.Cheque || formaDesembolso == TipoFormaDesembolso.Transferencia)
            {
                pnlCtasBanc.Visible = true;
            }
        }
    }

    private void CargarCuentas()
    {
        Int64 codbanco = 0;
        try
        {
            codbanco = Convert.ToInt64(ddlEntidad_giro.SelectedValue);
        }
        catch
        {
        }
        if (codbanco != 0)
        {
            Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
            ddlCuenta_Giro.DataSource = bancoService.ListarCuentaBancos(codbanco, Usuario);
            ddlCuenta_Giro.DataTextField = "num_cuenta";
            ddlCuenta_Giro.DataValueField = "idctabancaria";
            ddlCuenta_Giro.DataBind();
        }
    }


    private Xpinn.FabricaCreditos.Entities.Credito ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.Credito vCredito = new Xpinn.FabricaCreditos.Entities.Credito();
        if (txtIdentificacion.Text.Trim() != "")
            vCredito.identificacion = Convert.ToString(txtIdentificacion.Text.Trim());
        if (txtnumero_radicacion.Text.Trim() != "")
            vCredito.numero_radicacion = int.Parse(txtnumero_radicacion.Text.Trim());
        if (txtnro_radicacion.Text.Trim() != "")
            vCredito.numero_radicacion2 = int.Parse(txtnro_radicacion.Text.Trim());
        if (txtNombre.Text.Trim() != "")
            vCredito.nombre = Convert.ToString(txtNombre.Text.Trim());
        if (txtAprobacion_ini.Text.Trim() != "")
            vCredito.fecha_aproba = Convert.ToDateTime(txtAprobacion_ini.Text.Trim());
        if (txtAprobacion_fin.Text.Trim() != "")
            vCredito.fecha_aprobacion = Convert.ToDateTime(txtAprobacion_fin.Text.Trim());
        if (ddlOficinas.SelectedIndex != 0)
            vCredito.oficina = ddlOficinas.SelectedValue;
        if (ddlLineaCredito.SelectedIndex != 0)
            vCredito.linea_credito = ddlLineaCredito.SelectedValue;

        return vCredito;
    }

    /// <summary>
    /// Generar las condiciones de acuerdo a los filtros ingresados
    /// </summary>
    /// <param name="credito"></param>
    /// <returns></returns>
    /// 

    private string obtFiltro(Credito credito)
    {

        Configuracion conf = new Configuracion();

        String filtro = String.Empty;
        if (txtnumero_radicacion.Text.Trim() != "")
            filtro += " and v_creditos.numero_radicacion >= " + credito.numero_radicacion;
        if (txtnro_radicacion.Text.Trim() != "")
            filtro += " and v_creditos.numero_radicacion <= " + credito.numero_radicacion2;
        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and v_creditos.identificacion like '%" + credito.identificacion + "%'";
        if (txtNombre.Text.Trim() != "")
            filtro += " and v_creditos.nombres like '%" + credito.nombre + "%'";
        if (txtCodigoNomina.Text.Trim() != "")
            filtro += " and v_creditos.cod_nomina like '%" + txtCodigoNomina.Text + "%'";
        if (ddlLineaCredito.SelectedIndex != 0)
            filtro += " and v_creditos.cod_linea_credito= '" + credito.linea_credito + "'";
        if (ddlOficinas.SelectedIndex != 0)
            filtro += " and v_creditos.cod_oficina= '" + credito.oficina + "'";

        //Agregado para consultar proceso anterior a Desembolso según la parametrización de la entidad
        ControlTiempos control = new ControlTiempos();
        CreditoSolicitadoService CreditoSolicitadoServicio = new CreditoSolicitadoService();
        string estado = "Desembolsado";
        control = CreditoSolicitadoServicio.ConsultarProcesoAnterior(estado, (Usuario)Session["usuario"]);
        if (control.estado != null && control.estado != "")
            filtro += " and v_creditos.estado='" + control.estado + "'";
        else
            filtro += " and (v_creditos.estado = 'G')";

        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "where " + filtro;
        }
        return filtro;
    }


    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.Credito> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Credito>();
            String filtro = obtFiltro(ObtenerValores());
            DateTime pFechaIni, pFechaFin;
            pFechaIni = txtAprobacion_ini.ToDateTime == null ? DateTime.MinValue : txtAprobacion_ini.ToDateTime;
            pFechaFin = txtAprobacion_fin.ToDateTime == null ? DateTime.MinValue : txtAprobacion_fin.ToDateTime;
            lstConsulta = CreditoServicio.ListarCreditoMasivo(ObtenerValores(), pFechaIni, pFechaFin, (Usuario)Session["usuario"], filtro);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            Site toolBar = (Site)this.Master;
            if (lstConsulta.Count > 0)
            {
                toolBar.MostrarGuardar(true);
                pDatos.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                lblTotalRegs.Text = string.Empty;
                toolBar.MostrarGuardar(false);
                pDatos.Visible = false;
            }

            TipoFormaDesembolso formaDesembolso = ddlForma_Desem.SelectedValue.ToEnum<TipoFormaDesembolso>();
            ValidateGridVisible(formaDesembolso);

            Session.Add(CreditoServicio.CodigoProgramaoriginal + ".consulta", 1);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaoriginal, "Actualizar", ex);
        }
    }


    protected void ValidateGridVisible(TipoFormaDesembolso pFormaDesem)
    {
        bool result = pFormaDesem == TipoFormaDesembolso.Transferencia ? true : false;
        gvLista.Columns[13].Visible = result;
        gvLista.Columns[14].Visible = result;
        gvLista.Columns[15].Visible = result;
    }


    #endregion


    #region EVENTS BUTTONS

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Page.Validate();
        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, CreditoServicio.CodigoProgramaoriginal);
            if (ddlForma_Desem.SelectedIndex != 0)
                Actualizar();
            else
                VerError("Digite una forma de desembolso");
        }
    }

   
    /// <summary>
    /// EVENTO MODIFICAR VENTANA EMERGENTE
    /// </summary>
    /// :::::::::::::::::::::::::::::::::::::::
    protected void btnModificar_Click(object sender, EventArgs e)
    {
        if (chkTasa.Checked == false)
            txttasa.Text = "";

        CreditoServicio.cambiotasa_fecha(txttasa.Text, ddltipotasa.SelectedValue, txtNum_radicacion.Text, txtFechaAct.ToDateTime, Usuario);

        Actualizar();
        mpeActualizarTasa.Hide();
        txtnro_radicacion.Text = string.Empty;
    }

    protected void btnCloseReg1_Click(object sender, EventArgs e)
    {
        mpeActualizarTasa.Hide();
    }
    /// :::::::::::::::::::::::::::::::::::::::


    /// <summary>
    /// EVENTO BOTONES DE CONTROL DE PROCESO CONTABLE
    /// </summary>
    /// :::::::::::::::::::::::::::::::::::::::
    private void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            if (GenerarDesembolso())
            {
                panelGeneral.Visible = true;
                panelProceso.Visible = false;
                Site toolBar = (Site)this.Master;
                toolBar.MostrarConsultar(true);
                toolBar.MostrarGuardar(false);
                mvDesembolsoMasivo.ActiveViewIndex = 1;
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    private void btnCancelarProceso_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(false);
        if (gvLista.Rows.Count > 0)
            toolBar.MostrarGuardar(true);
        toolBar.MostrarConsultar(true);
        panelGeneral.Visible = true;
        panelProceso.Visible = false;
    }
    /// :::::::::::::::::::::::::::::::::::::::


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (string.IsNullOrEmpty(txtFechaRealiza.Text))
        {
            VerError("Ingrese la fecha de desembolso.");
            txtFechaRealiza.Focus();
            return;
        }
        int numRegistros = gvLista.Rows.OfType<GridViewRow>().Where(x => ((CheckBox)x.FindControl("cbSeleccionar")).Checked).Count();
        if (numRegistros == 0)
        {
            VerError("Seleccione algún crédito a desembolsar");
            return;
        }
        // Determinar el proceso del desembolso
        Xpinn.Contabilidad.Services.ProcesoContableService procesoContable = new Xpinn.Contabilidad.Services.ProcesoContableService();
        Xpinn.Contabilidad.Entities.ProcesoContable eproceso = new Xpinn.Contabilidad.Entities.ProcesoContable();
        eproceso = procesoContable.ConsultarProcesoContableOperacion(1, Usuario);
        if (eproceso == null)
        {
            VerError("No hay ningún proceso contable parametrizado para el desembolso de créditos");
            return;
        }
        if (eproceso.cod_proceso == null)
        {
            VerError("No hay ningún proceso contable parametrizado para el desembolso de créditos");
            return;
        }

        ctlMensaje.MostrarMensaje("Desea realizar el desembolso de los crèditos seleccionados?");
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        Int64? rpta = ctlproceso.Inicializar(1, Convert.ToDateTime(txtFechaRealiza.Text), Usuario);
        if (rpta > 1)
        {
            panelGeneral.Visible = false;
            panelProceso.Visible = true;
        }
        else
        {
            if (GenerarDesembolso())
            {
                panelGeneral.Visible = true;
                panelProceso.Visible = false;

                Site toolBar = (Site)this.Master;
                toolBar.MostrarConsultar(true);
                toolBar.MostrarGuardar(false);

                mvDesembolsoMasivo.ActiveViewIndex = 1;
            }
        }
    }

    #endregion


    #region EVENTS CONTROLS

    protected void ddlForma_Desem_SelectedIndexChanged(object sender, EventArgs e)
    {
        ActivarDesembolso();
        ddlEntidad_giro_SelectedIndexChanged(ddlEntidad_giro, null);
    }

    protected void cbSeleccionarEncabezado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbSeleccionarEncabezado = (CheckBox)sender;
        if (cbSeleccionarEncabezado != null)
        {
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
                cbSeleccionar.Checked = cbSeleccionarEncabezado.Checked;
            }
        }
    }

    protected void chkTasa_CheckedChanged(object sender, EventArgs e)
    {
        bool result = chkTasa.Checked ? true : false;
        txttasa.Enabled = result;
        ddltipotasa.Enabled = result;
    }

    protected void ddlEntidad_giro_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarCuentas();
    }


    #endregion


    #region EVENTS GRIDVIEW


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        txtNum_radicacion.Text = id;
        txtFechaAct.Text = gvLista.Rows[e.NewEditIndex].Cells[10].Text;
        chkTasa.Checked = false;
        chkTasa_CheckedChanged(chkTasa, null);
        txttasa.Text = string.Empty;
        ddltipotasa.SelectedIndex = 0;
        e.NewEditIndex = -1;
        mpeActualizarTasa.Show();
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaoriginal, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Xpinn.Tesoreria.Services.EmpresaRecaudoServices serviciosempresarecaudo = new Xpinn.Tesoreria.Services.EmpresaRecaudoServices();

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlpagaduria = (DropDownListGrid)e.Row.FindControl("ddlpagaduria");
            if (ddlpagaduria != null)
            {
                Xpinn.Tesoreria.Entities.EmpresaRecaudo pData = new Xpinn.Tesoreria.Entities.EmpresaRecaudo();
                List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstConsulta = null;
                if (ViewState[Usuario.codusuario + "DTPAGADURIAS"] == null)
                    lstConsulta = serviciosempresarecaudo.ListarEmpresaRecaudo(pData, (Usuario)Session["usuario"]);
                else
                    lstConsulta = (List<Xpinn.Tesoreria.Entities.EmpresaRecaudo>)ViewState[Usuario.codusuario + "DTPAGADURIAS"];

                if (lstConsulta != null)
                {
                    if (lstConsulta.Count > 0)
                    {
                        ViewState[Usuario.codusuario + "DTPAGADURIAS"] = lstConsulta;
                        ddlpagaduria.DataSource = lstConsulta;
                        ddlpagaduria.DataTextField = "NOM_EMPRESA";
                        ddlpagaduria.DataValueField = "COD_EMPRESA";
                        ddlpagaduria.AppendDataBoundItems = true;
                        ddlpagaduria.Items.Insert(0, new ListItem("Seleccione un item", "0"));
                        ddlpagaduria.SelectedIndex = 0;
                        ddlpagaduria.DataBind();
                    }
                }

                Label lbltipoPagaduria = (Label)e.Row.FindControl("lbltipoPagaduria");
                if (lbltipoPagaduria != null)
                    ddlpagaduria.SelectedValue = lbltipoPagaduria.Text;
            }


            DropDownListGrid ddltipocuenta = (DropDownListGrid)e.Row.FindControl("ddltipocuenta");
            if (ddltipocuenta != null)
            {
                ddltipocuenta.Items.Insert(0, new ListItem("Seleccione un item", "-1"));
                ddltipocuenta.Items.Insert(1, new ListItem("AHORROS", "0"));
                ddltipocuenta.Items.Insert(2, new ListItem("CORRIENTE", "1"));
                ddltipocuenta.SelectedIndex = 0;
                ddltipocuenta.DataBind();

                Label lbltipocuenta = (Label)e.Row.FindControl("lbltipocuenta");
                if (lbltipocuenta != null)
                    ddltipocuenta.SelectedValue = lbltipocuenta.Text;
            }
        }

    } 


    #endregion

    
    Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
    Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
    protected bool GenerarDesembolso()
    {
        try
        {
            Int64 pnum_comp = 0, ptipo_comp = 0;
            List<Xpinn.Tesoreria.Entities.Operacion> lstConsulta = new List<Xpinn.Tesoreria.Entities.Operacion>();

            int codigo = 0;
            ctlproceso.NoGeneraComprobante = true;
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
                if (cbSeleccionar != null)
                    if (cbSeleccionar.Checked == true)
                    {
                        // Desembolsar

                        Credito cred = new Credito();
                        cred.numero_radicacion = Convert.ToInt64(gvLista.DataKeys[rFila.RowIndex].Value.ToString());
                        cred.estado = "C";

                        Credito vCredito = new Credito();
                        vCredito = CreditoServicio.ConsultarCredito(cred.numero_radicacion, Usuario);
                        if (vCredito.cod_deudor != Int64.MinValue)
                            codigo = int.Parse(HttpUtility.HtmlDecode(vCredito.cod_deudor.ToString().Trim()));

                        cred.fecha_desembolso = txtFechaRealiza.ToDateTime;
                        cred.fecha_inicio = Convert.ToDateTime(rFila.Cells[11].Text.ToString());
                        cred.cod_deudor = codigo;
                        cred.cod_ope = 0;

                        TextBox txtCuentaBancaria = (TextBox)rFila.FindControl("txtCuentaBancaria");
                        DropDownListGrid ddltipocuenta = (DropDownListGrid)rFila.FindControl("ddltipocuenta");
                        CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ddlEntidad_giro.SelectedValue), ddlCuenta_Giro.SelectedItem.Text, (Usuario)Session["usuario"]);
                        Int32 idCta = CuentaBanc.idctabancaria;

                        bool pOpcion = true;
                        //VALIDAR SI ESTA ACTIVO LA OPCION ORDEN SERVICIO PARA LA LINEA.
                        Xpinn.FabricaCreditos.Services.LineasCreditoService LineasCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();

                        Xpinn.FabricaCreditos.Entities.LineasCredito vLineasCredito = new Xpinn.FabricaCreditos.Entities.LineasCredito();
                        vLineasCredito = LineasCreditoServicio.ConsultarLineasCredito(Convert.ToString(vCredito.cod_linea_credito), (Usuario)Session["usuario"]);
                        if (vLineasCredito.orden_servicio == 1)
                        {
                            pOpcion = false;
                        }

                        //DATOS DE FORMA DE PAGO
                        int idctabancaria = 0, cod_banco = 0, tipo_cuenta = 0;
                        string num_cuenta = "";
                        if (pOpcion == true)
                        {
                            if (ddlForma_Desem.SelectedItem.Text == "Efectivo")
                            {
                                idctabancaria = idCta;
                                cod_banco = Convert.ToInt32(ddlEntidad_giro.SelectedValue);
                                if (txtCuentaBancaria.Text != null)
                                    num_cuenta = txtCuentaBancaria.Text;
                                tipo_cuenta = Convert.ToInt32(ddltipocuenta.SelectedValue);
                            }
                            if (ddlForma_Desem.SelectedItem.Text == "Cheque")
                            {
                                idctabancaria = idCta;
                                cod_banco = Convert.ToInt32(ddlEntidad_giro.SelectedValue);
                                if (txtCuentaBancaria.Text != null)
                                    num_cuenta = txtCuentaBancaria.Text;
                                tipo_cuenta = Convert.ToInt32(ddltipocuenta.SelectedValue);
                            }
                            if (ddlForma_Desem.SelectedItem.Text == "Transferencia")
                            {
                                idctabancaria = idCta;
                                cod_banco = Convert.ToInt32(ddlEntidad_giro.SelectedValue);
                                if (txtCuentaBancaria.Text != null)
                                    num_cuenta = txtCuentaBancaria.Text;
                                tipo_cuenta = Convert.ToInt32(ddltipocuenta.SelectedValue);
                            }
                            else if (ddlForma_Desem.SelectedItem.Text == "otros")
                            {
                                idctabancaria = idCta;
                                cod_banco = 0; //NULO
                                num_cuenta = null; //NULO
                                tipo_cuenta = -1; //NULO
                            }
                            else
                            {
                                idctabancaria = 0;
                                cod_banco = 0;
                                num_cuenta = null;
                                tipo_cuenta = -1;
                            }
                        }
                        string sError = "";
                        CreditoServicio.DesembolsarCreditoMasivo(cred, pOpcion, Convert.ToInt64(ddlForma_Desem.SelectedValue), idctabancaria, cod_banco, num_cuenta, tipo_cuenta, ref sError, Usuario);
                        if (cred.cod_ope != 0)
                        {
                            if (ddlCuenta_Giro.SelectedIndex == 0)
                                CreditoServicio.GuardarCuentaBancariaCliente(cred.cod_deudor, txtCuentaBancaria.Text, Convert.ToInt64(ddltipocuenta.SelectedValue), Convert.ToInt64(ddlEntidad_giro.SelectedValue), Usuario);
                            Xpinn.Contabilidad.Services.ComprobanteService comprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                            if (comprobanteServicio.GenerarComprobante(cred.cod_ope, 1, cred.fecha_desembolso, Usuario.cod_oficina, cred.cod_deudor, (long)ctlproceso.cod_proceso, ref pnum_comp, ref ptipo_comp, ref sError, Usuario))
                            {
                                Xpinn.Tesoreria.Entities.Operacion oper = new Xpinn.Tesoreria.Entities.Operacion();
                                oper.cod_ope = cred.cod_ope;
                                oper.num_comp = pnum_comp;
                                oper.tipo_comp = ptipo_comp;
                                lstConsulta.Add(oper);
                            }
                        }
                        else
                        {
                            if (sError != "")
                                VerError(sError);
                        }
                        Session.Remove(CreditoServicio.CodigoProgramaoriginal + ".id");
                    }
            }
            Actualizar();
            gvOperacion.DataSource = lstConsulta;
            gvOperacion.DataBind();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    


}