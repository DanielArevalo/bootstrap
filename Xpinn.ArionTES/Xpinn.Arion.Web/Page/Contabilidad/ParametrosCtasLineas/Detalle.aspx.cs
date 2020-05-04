using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;
using System.Data;

public partial class Detalle : GlobalWeb
{
    ParametroCtasCreditosService parametroServicio = new ParametroCtasCreditosService();

    #region Eventos Iniciales
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(parametroServicio.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlmensaje.eventoClick += btnContinuar_Click;
            toolBar.MostrarCancelar(true);
            toolBar.MostrarGuardar(true);
        }
        catch (Exception ex)
        {
            VerError(parametroServicio.CodigoPrograma + ex.Message);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["lstOpe"] = null;
                CargarCombos();
                int valor = 0;
                VisualizarTabs(ref valor);                
                if (Session["cod_linea_credito"] != null)
                {
                    string Objeto = Convert.ToString(Session["cod_linea_credito"]);
                    ddlLineaCredito.SelectedValue = Objeto;
                    ObtenerDatos(Objeto, valor);
                    CargarControlesLinCastigo();
                }
            }
        }
        catch (Exception ex)
        {
            VerError(parametroServicio.CodigoPrograma + ex.Message);
        }

    }

    private void CargarCombos()
    {
        PoblarListas poblar = new PoblarListas();
        poblar.PoblarListaDesplegable("LINEASCREDITO", ddlLineaCredito, (Usuario)Session["usuario"]);
        poblar.PoblarListaDesplegable("ATRIBUTOS", ddlAtributo, (Usuario)Session["usuario"]);
        ddlAtributo.SelectedValue = "1";
    }

    private void VisualizarTabs(ref int valor)
    {
        Xpinn.FabricaCreditos.Services.LineasCreditoService LineaServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        Xpinn.FabricaCreditos.Entities.LineasCredito vLinea = new Xpinn.FabricaCreditos.Entities.LineasCredito();

        vLinea = LineaServicio.ConsultarAtributoGeneral(ddlAtributo.SelectedValue, (Usuario)Session["usuario"]);
        if (vLinea.causa == 1 || vLinea.cod_atr == 1)
        {
            tabCausacion.Visible = true;
            tabClasificacion.Visible = true;
            tabProvision.Visible = true;
            valor = 1;
        }
        else
        {
            tabCausacion.Visible = false;
            tabClasificacion.Visible = false;
            tabProvision.Visible = false;
            valor = 0;
        }
    }

    private bool VerificarLineaCastigada(string cod_linea_credito)
    {
        Xpinn.FabricaCreditos.Services.LineasCreditoService LineaServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        decimal valor = 0;

        valor = LineaServicio.ConsultarParametrosLinea(cod_linea_credito, "320", (Usuario)Session["usuario"]);
        if (valor == 1)
            return true;
        else
            return false;
    }

    private void CargarControlesLinCastigo()
    {
        if (VerificarLineaCastigada(ddlLineaCredito.SelectedValue))
        {
            lblCuentaOrden.Visible = true;
            lblCuentaOrdCon.Visible = true;
            txtCodCuentaOp1.Visible = true;
            txtNomCuentaOp1.Visible = true;
            txtCodCuentaNIIFOp1.Visible = true;
            btnPlanCuenNIIFOp1.Visible = true;
            txtNomCuentaNIIFOp1.Visible = true;
            tbOrden.Visible = true;
            cbLineaCastigada.Checked = true;
            tabCausacion.Visible = false;
            tabClasificacion.Visible = false;
            tabProvision.Visible = false;
        }
    }

    private void CrearDetalleInicial()
    {
        List<Par_Cue_LinCred> lstOpeTran = new List<Par_Cue_LinCred>();
        //lstOpeTran = (List<Par_Cue_LinCred>)Session["lstOpe"];
        Par_Cue_LinCred parametro = new Par_Cue_LinCred();

        DataTable tbOper = new DataTable();
        tbOper.Columns.Add("cuenta_ope1");
        tbOper.Columns.Add("IdParamOp1");
        tbOper.Columns.Add("TipoParamOp1");

        DataRow data = tbOper.NewRow();
        data[0] = parametro.cod_cuenta;
        data[1] = parametro.idparametro;
        data[2] = parametro.tipo_mov;

        tbOper.Rows.Add(data);

        gvOperacion.DataSource = tbOper;
        gvOperacion.DataBind();

        lstOpeTran.Add(parametro);

        Session["lstOpeTran"] = lstOpeTran;
    }

    #endregion

    #region Eventos de botones
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidarDatos())
            {
                ctlmensaje.MostrarMensaje("¿Desea registrar la parametrización?");
            }
        }
        catch (Exception ex)
        {
            VerError(parametroServicio.CodigoPrograma + ex.Message);
        }
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        try
        {
            ParametroCtasCreditosService parametroServicio = new ParametroCtasCreditosService();
            bool valor = parametroServicio.CrearParametrizacionLinea(ListaParametros(), (Usuario)Session["usuario"]);
            if (valor)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "Se registró la parametrización correctamente";
                int visualizar = 0;
                VisualizarTabs(ref visualizar);
                LimpiarCampos();
                ObtenerDatos(ddlLineaCredito.SelectedValue, visualizar);
                CargarControlesLinCastigo();
            }
            else
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "Error en el registró de la parametrización";
            }
        }
        catch (Exception ex)
        {
            VerError(parametroServicio.CodigoPrograma + ex.Message);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnDetalle_Click(object sender, EventArgs e)
    {
        List<Par_Cue_LinCred> lstParametros = new List<Par_Cue_LinCred>();
        ParametrosOperacion();
        lstParametros = (List<Par_Cue_LinCred>)Session["lstOpe"];

        DataTable tbOper = new DataTable();
        tbOper.Columns.Add("cuenta_ope1");
        tbOper.Columns.Add("IdParamOp1");
        tbOper.Columns.Add("TipoParamOp1");        

        DataRow data;       
        //Registros existentes
        foreach (Par_Cue_LinCred cuenta in lstParametros)
        {
            data = tbOper.NewRow();
            data[0] = cuenta.cod_cuenta;
            data[1] = cuenta.idparametro;
            data[2] = 0;
            tbOper.Rows.Add(data);
        }
        //Cargar nuevo detalle
        data = tbOper.NewRow();
        data[0] = "";
        data[1] = 0;
        data[2] = "";
        tbOper.Rows.Add(data);

        Par_Cue_LinCred pCuenta = new Par_Cue_LinCred();
        lstParametros.Add(pCuenta);

        Session["lstOpe"] = lstParametros;
        gvOperacion.DataSource = tbOper;
        gvOperacion.DataBind();        
    }

    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ButtonGrid btnListadoPlan = (ButtonGrid)sender;
        if (btnListadoPlan.NamingContainer is GridViewRow)
        {
            int celda = Convert.ToInt32(btnListadoPlan.CommandArgument);
            GridViewRow rParametros = btnListadoPlan.NamingContainer as GridViewRow;
            ctlPlanCuentas ctlListadoPlan = (ctlPlanCuentas)rParametros.FindControl("ctlListadoPlanContable");

            if (celda == 1)
            {
                TextBox txtCuenta = (TextBox)rParametros.Cells[1].FindControl("txtCuentaOpTran");
                ctlListadoPlan.Motrar(true, txtCuenta.ID, "");
            }
        }
        else
        {
            ctlListadoPlanContable.Motrar(true, "txtCodCuentaOp", "txtNomCuentaOp");
        }
    }
    protected void btnListadoPlanNIIF_Click(object sender, EventArgs e)
    {
        ctlListadoPlanContable.Motrar(true, "txtCodCuentaNIIFOp", "txtNomCuentaNIIFOp");
    }
    protected void btnListadoPlanCl_Click(object sender, EventArgs e)
    {
        ButtonGrid btnListadoPlan = (ButtonGrid)sender;

        int celda = Convert.ToInt32(btnListadoPlan.CommandArgument);
        GridViewRow rParametros = btnListadoPlan.NamingContainer as GridViewRow;
        ctlPlanCuentas ctlListadoPlan = (ctlPlanCuentas)rParametros.FindControl("ctlListadoPlanContable1");

        if (celda == 1)
        {
            TextBox txtCuenta = (TextBox)rParametros.Cells[1].FindControl("txtCuentaConLibAd");
            ctlListadoPlan.Motrar(true, txtCuenta.ID, "");
        }
        else if (celda == 2)
        {
            TextBox txtCuenta = (TextBox)rParametros.Cells[2].FindControl("txtCuentaSinLibAd");
            ctlListadoPlan.Motrar(true, txtCuenta.ID, "");
        }
        else if (celda == 3)
        {
            TextBox txtCuenta = (TextBox)rParametros.Cells[3].FindControl("txtCuentaConLibNoAd");
            ctlListadoPlan.Motrar(true, txtCuenta.ID, "");
        }
        else if (celda == 4)
        {
            TextBox txtCuenta = (TextBox)rParametros.Cells[4].FindControl("txtCuentaSinLibNoAd");
            ctlListadoPlan.Motrar(true, txtCuenta.ID, "");
        }
        else if (celda == 5)
        {
            TextBox txtCuenta = (TextBox)rParametros.Cells[5].FindControl("txtCuentaOrden");
            ctlListadoPlan.Motrar(true, txtCuenta.ID, "");
        }
        else if (celda == 6)
        {
            TextBox txtCuenta = (TextBox)rParametros.Cells[6].FindControl("txtCuentaOrdenContra");
            ctlListadoPlan.Motrar(true, txtCuenta.ID, "");
        }
    }
    protected void btnListadoPlanCa_Click(object sender, EventArgs e)
    {
        ButtonGrid btnListadoPlan = (ButtonGrid)sender;

        int celda = Convert.ToInt32(btnListadoPlan.CommandArgument);
        GridViewRow rParametros = btnListadoPlan.NamingContainer as GridViewRow;
        ctlPlanCuentas ctlListadoPlan = (ctlPlanCuentas)rParametros.FindControl("ctlListadoPlanContable2");

        if (celda == 1)
        {
            TextBox txtCuenta = (TextBox)rParametros.Cells[celda].FindControl("txtCodCuentaCausa");
            ctlListadoPlan.Motrar(true, txtCuenta.ID, "");
        }

    }
    protected void btnListadoPlanProv_Click(object sender, EventArgs e)
    {
        ButtonGrid btnListadoPlan = (ButtonGrid)sender;

        int celda = Convert.ToInt32(btnListadoPlan.CommandArgument);
        GridViewRow rParametros = btnListadoPlan.NamingContainer as GridViewRow;
        ctlPlanCuentas ctlListadoPlan = (ctlPlanCuentas)rParametros.FindControl("ctlListadoPlanContable3");

        if (celda == 1)
        {
            TextBox txtCuenta = (TextBox)rParametros.Cells[celda].FindControl("txtCuentaProvAd");
            ctlListadoPlan.Motrar(true, txtCuenta.ID, "");
        }
        else if (celda == 2)
        {
            TextBox txtCuenta = (TextBox)rParametros.Cells[celda].FindControl("txtCuentaProNoAd");
            ctlListadoPlan.Motrar(true, txtCuenta.ID, "");
        }
        else if (celda == 3)
        {
            TextBox txtCuenta = (TextBox)rParametros.Cells[celda].FindControl("txtCuentaGasto");
            ctlListadoPlan.Motrar(true, txtCuenta.ID, "");
        }
        else if (celda == 4)
        {
            TextBox txtCuenta = (TextBox)rParametros.Cells[celda].FindControl("txtCuentaIngre");
            ctlListadoPlan.Motrar(true, txtCuenta.ID, "");
        }
    }

    #endregion

    #region Eventos cajas texto, grillas y DDLS

    private void LimpiarCampos()
    {
        txtCodCuentaOp.Text = "";
        txtNomCuentaOp.Text = "";
        txtCodCuentaNIIFOp.Text = "";
        txtNomCuentaNIIFOp.Text = "";
        gvOperacion.DataSource = null;
        gvCausacion.DataSource = null;
        gvClasificacion.DataSource = null;
        gvProvision.DataSource = null;
    }
    protected void ddlAtributo_SelectedIndexChanged(object sender, EventArgs e)
    {
        int valor = 0;
        VisualizarTabs(ref valor);
        LimpiarCampos();
        ObtenerDatos(ddlLineaCredito.SelectedValue, valor);
        CargarControlesLinCastigo();
        lblMensaje.Text = "";
    }
    protected void txtCodCuentaOp_TextChanged(object sender, EventArgs e)
    {
        VerError("");
        TextBox txtCuenta = (TextBox)sender;
        Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
        Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
        PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCuenta.Text, (Usuario)Session["usuario"]);
        if (PlanCuentas == null)
        {
            VerError("La cuenta ingresada no existe");
            txtCuenta.Text = "";
        }
        else // Mostrar el nombre de la cuenta        
        {
            if(txtCuenta.ID == "txtCodCuentaOp")
                txtNomCuentaOp.Text = PlanCuentas.nombre;
            else if (txtCuenta.ID == "txtCodCuentaOp1")
                txtNomCuentaOp1.Text = PlanCuentas.nombre;
        }
    }

    protected void txtCodCuentaNIIFOp_TextChanged(object sender, EventArgs e)
    {
        VerError("");
        Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
        Xpinn.NIIF.Entities.PlanCuentasNIIF PlanCuentas = new Xpinn.NIIF.Entities.PlanCuentasNIIF();
        PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentasNIIF(txtCodCuentaNIIFOp.Text, (Usuario)Session["usuario"]);
        if (PlanCuentas == null)
        {
            VerError("La cuenta ingresada no existe");
            txtCodCuentaNIIFOp.Text = "";
        }
        else // Mostrar el nombre de la cuenta        
            txtNomCuentaNIIFOp.Text = PlanCuentas.nombre;
    }

    protected void txtCuenta_TextChanged(object sender, EventArgs e)
    {
        VerError("");
        TextBox txtCodCuenta = (TextBox)sender;

        GridViewRow rParametros = txtCodCuenta.NamingContainer as GridViewRow;

        Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
        Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
        PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta.Text, (Usuario)Session["usuario"]);
        if (PlanCuentas.cod_cuenta == null || PlanCuentas.cod_cuenta == "")
        {
            VerError("La cuenta ingresada no existe");
            txtCodCuenta = (TextBox)rParametros.FindControl(txtCodCuenta.ID);
            txtCodCuenta.Text = "";
        }
    }

    protected void gvOperacion_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ctlListarCodigo ctllistar = (ctlListarCodigo)e.Row.FindControl("ddlTipoTran");
            if (ctllistar != null)
            {
                ctllistar.ValueField = "tipo_tran";
                ctllistar.TextField = "nom_tipo_tran";

                try
                {
                    List<Par_Cue_LinCred> lstTransaccion = parametroServicio.ListarTransaccion((Usuario)Session["Usuario"]);
                    ctllistar.BindearControl(lstTransaccion);

                    //Cargar los tipos de transacción de cada parámetro
                    List<Par_Cue_LinCred> lstOperacion = Session["lstOpe"] != null ? (List<Par_Cue_LinCred>)Session["lstOpe"] : new List<Par_Cue_LinCred>();
                    if (lstOperacion.Count > 0)
                    {  
                        TextBox txtIdParamOp1 = (TextBox)e.Row.FindControl("txtIdParamOp1");
                        TextBox txtCuentaOpTran = (TextBox)e.Row.FindControl("txtCuentaOpTran");
                        ctlListarCodigo ddlTipoTran = (ctlListarCodigo)e.Row.FindControl("ddlTipoTran");
                        int idparam = Convert.ToInt32(txtIdParamOp1.Text);
                        Int64 tipo_tran = 0;
                        if (txtCuentaOpTran.Text == "")
                            tipo_tran = Convert.ToInt64(lstOperacion.Where(x => x.cod_cuenta == null && x.idparametro == idparam).FirstOrDefault().tipo_tran);
                        else
                            tipo_tran = Convert.ToInt64(lstOperacion.Where(x => x.cod_cuenta == txtCuentaOpTran.Text && x.idparametro == idparam).FirstOrDefault().tipo_tran);

                        ddlTipoTran.SelectedValueEqual(tipo_tran.ToString());                        
                    }
                }
                catch (Exception ex)
                {
                    VerError("Error al cargar el tipo de transacción: " + ex.Message.ToString());
                }
            }
        }
    }


    protected void gvOperacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bool valor = false;
        string error = "";
        Int64 id_parametro = 0;
        List<Par_Cue_LinCred> lstParametros = new List<Par_Cue_LinCred>();
        TextBox txtIdParamOp1 = (TextBox)gvOperacion.Rows[e.RowIndex].FindControl("txtIdParamOp1");
        TextBox txtCuentaOpTran = (TextBox)gvOperacion.Rows[e.RowIndex].FindControl("txtCuentaOpTran");
        ctlListarCodigo ddlTipoTran = (ctlListarCodigo)gvOperacion.Rows[e.RowIndex].FindControl("ddlTipoTran");

        id_parametro = Convert.ToInt64(txtIdParamOp1.Text);

        if (id_parametro != 0)
        {
            parametroServicio.EliminarPar_Cue_LinCred(id_parametro, (Usuario)Session["usuario"]);            
            ParametrosOperacion();
            lstParametros = (List<Par_Cue_LinCred>)Session["lstOpe"];
            lstParametros.Remove(lstParametros.Where(x => x.idparametro == id_parametro).FirstOrDefault());

            DataTable tbOper = new DataTable();
            tbOper.Columns.Add("cuenta_ope1");
            tbOper.Columns.Add("IdParamOp1");
            tbOper.Columns.Add("TipoParamOp1");

            DataRow data;
            //Registros existentes
            foreach (Par_Cue_LinCred cuenta in lstParametros)
            {
                data = tbOper.NewRow();
                data[0] = cuenta.cod_cuenta;
                data[1] = cuenta.idparametro;
                data[2] = 0;
                tbOper.Rows.Add(data);
            }                   
            Session["lstOpe"] = lstParametros;
            gvOperacion.DataSource = tbOper;
            gvOperacion.DataBind();

        }
        else
        {
            ParametrosOperacion();
            lstParametros = (List<Par_Cue_LinCred>)Session["lstOpe"];
            lstParametros.Remove(lstParametros.Where(x => x.cod_cuenta == txtCuentaOpTran.Text && x.tipo_tran == Convert.ToInt64(ddlTipoTran.Codigo)).FirstOrDefault());

            DataTable tbOper = new DataTable();
            tbOper.Columns.Add("cuenta_ope1");
            tbOper.Columns.Add("IdParamOp1");
            tbOper.Columns.Add("TipoParamOp1");

            DataRow data;
            //Registros existentes
            foreach (Par_Cue_LinCred cuenta in lstParametros)
            {
                data = tbOper.NewRow();
                data[0] = cuenta.cod_cuenta;
                data[1] = cuenta.idparametro;
                data[2] = 0;
                tbOper.Rows.Add(data);
            }
            Session["lstOpe"] = lstParametros;
            gvOperacion.DataSource = tbOper;
            gvOperacion.DataBind();

        }
    }

    #endregion

    #region Validacion y Carga de Datos

    private bool ValidarDatos()
    {
        if (txtCodCuentaOp.Text == "")
        {
            VerError("La cuenta de operación para la línea no se encuentra parametrizada");
            return false;
        }
        int valor = 0;
        VisualizarTabs(ref valor);
        if (valor == 1)
        {
            if (gvClasificacion.Rows.Count == 0)
            {
                VerError("Las cuentas de clasificación para la línea no se encuentran parametrizadas");
                return false;
            }
            else if (gvCausacion.Rows.Count == 0)
            {
                VerError("Las cuentas de causación para la línea no se encuentran parametrizadas");
                return false;
            }
            else if (gvProvision.Rows.Count == 0)
            {
                VerError("Las cuentas de provisión para la línea no se encuentran parametrizadas");
                return false;
            }
        }
        return true;
    }

    private void ObtenerDatos(string cod_linea_credito, int valor)
    {
        try
        {
            ParametroCtasCreditosService ParamCtasLinCredServicio = new ParametroCtasCreditosService();
            List<Par_Cue_LinCred> lstParametrizacion = new List<Par_Cue_LinCred>();
            Par_Cue_LinCred parametro = new Par_Cue_LinCred();
            parametro.cod_linea_credito = ddlLineaCredito.SelectedValue;
            parametro.cod_atr = Convert.ToInt32(ddlAtributo.SelectedValue);
            lstParametrizacion = ParamCtasLinCredServicio.ListarPar_Cue_LinCred(parametro, (Usuario)Session["usuario"]);

            //Listado de categorias
            List<Par_Cue_LinCred> lstCategorias = ParamCtasLinCredServicio.ListarClasificacion((Usuario)Session["usuario"]);

            //Cargar cuentas de operación
            if (lstParametrizacion.Count > 0)
            {
                if (!VerificarLineaCastigada(ddlLineaCredito.SelectedValue))
                {
                    parametro = lstParametrizacion.Where(x => x.cod_atr == Convert.ToInt32(ddlAtributo.SelectedValue) && x.tipo_cuenta == 1 && x.tipo == 0 && x.tipo_tran == null).FirstOrDefault();
                    if (parametro != null)
                    {
                        txtIdParamOp.Text = parametro.idparametro.ToString();
                        txtTipoParamOp.Text = parametro.tipo_mov.ToString();
                        txtCodCuentaOp.Text = parametro.cod_cuenta;
                        txtCodCuentaOp_TextChanged(txtCodCuentaOp, null);
                    }
                }
                else
                {
                    //Cuenta de orden
                    parametro = lstParametrizacion.Where(x => x.cod_atr == Convert.ToInt32(ddlAtributo.SelectedValue) && x.tipo_cuenta == 1 && x.tipo == 0 && x.tipo_tran == null && x.tipo_mov == 1).FirstOrDefault();
                    if (parametro != null)
                    {
                        txtIdParamOp.Text = parametro.idparametro.ToString();
                        txtTipoParamOp.Text = parametro.tipo_mov.ToString();
                        txtCodCuentaOp.Text = parametro.cod_cuenta;
                        txtCodCuentaOp_TextChanged(txtCodCuentaOp, null);
                    }
                    //Cuenta de orden por contra
                    parametro = lstParametrizacion.Where(x => x.cod_atr == Convert.ToInt32(ddlAtributo.SelectedValue) && x.tipo_cuenta == 1 && x.tipo == 0 && x.tipo_tran == null && x.tipo_mov == 2).FirstOrDefault();
                    if (parametro != null)
                    {
                        txtIdParamOp1.Text = parametro.idparametro.ToString();
                        txtTipoParamOp1.Text = parametro.tipo_mov.ToString();
                        txtCodCuentaOp1.Text = parametro.cod_cuenta;
                        txtCodCuentaOp_TextChanged(txtCodCuentaOp1, null);
                    }
                }

                List<Par_Cue_LinCred> lstOperacion = new List<Par_Cue_LinCred>();
                lstOperacion = lstParametrizacion.Where(x => x.cod_atr == Convert.ToInt32(ddlAtributo.SelectedValue) && x.tipo_cuenta == 1 && x.tipo == 0 && x.tipo_tran != null).ToList();
                if (lstOperacion.Count > 0)
                {
                    DataTable tbOper = new DataTable();
                    tbOper.Columns.Add("cuenta_ope1");
                    tbOper.Columns.Add("IdParamOp1");
                    tbOper.Columns.Add("TipoParamOp1");

                    DataRow data;
                    foreach (Par_Cue_LinCred cuenta in lstOperacion)
                    {
                        data = tbOper.NewRow();
                        data[0] = cuenta.cod_cuenta;
                        data[1] = cuenta.idparametro;
                        data[2] = cuenta.tipo_mov;
                        tbOper.Rows.Add(data);
                    }

                    Session["lstOpe"] = lstOperacion;
                    gvOperacion.DataSource = tbOper;
                    gvOperacion.DataBind();
                }
                else
                    CrearDetalleInicial();
            }
            else
            {
                CrearDetalleInicial();
            }

            if (valor == 1)
            {
                //Cargar cuentas de clasificación
                DataTable tbClasif = new DataTable();
                tbClasif.Columns.Add("cod_categoria");
                tbClasif.Columns.Add("cuenta_clasif1");
                tbClasif.Columns.Add("IdParamCl1");
                tbClasif.Columns.Add("TipoParamCl1");
                tbClasif.Columns.Add("cuenta_clasif2");
                tbClasif.Columns.Add("IdParamCl2");
                tbClasif.Columns.Add("TipoParamCl2");
                tbClasif.Columns.Add("cuenta_clasif3");
                tbClasif.Columns.Add("IdParamCl3");
                tbClasif.Columns.Add("TipoParamCl3");
                tbClasif.Columns.Add("cuenta_clasif4");
                tbClasif.Columns.Add("IdParamCl4");
                tbClasif.Columns.Add("TipoParamCl4");
                tbClasif.Columns.Add("cuenta_Ord");
                tbClasif.Columns.Add("IdParamCl5");
                tbClasif.Columns.Add("TipoParamCl5");
                tbClasif.Columns.Add("cuenta_OrdCon");
                tbClasif.Columns.Add("IdParamCl6");
                tbClasif.Columns.Add("TipoParamCl6");

                List<Par_Cue_LinCred> lstClasificacion = new List<Par_Cue_LinCred>();
                DataRow data;
                foreach (Par_Cue_LinCred categoria in lstCategorias)
                {
                    lstClasificacion = (from item in lstParametrizacion
                                        where item.tipo == 1 && item.cod_categoria == categoria.cod_categoria && item.cod_atr == Convert.ToInt64(ddlAtributo.SelectedValue)
                                        select item).ToList();

                    data = tbClasif.NewRow();
                    data[0] = categoria.cod_categoria;
                    foreach (Par_Cue_LinCred cuenta in lstClasificacion)
                    {
                        if (cuenta.garantia == 1 && cuenta.libranza == 2 && cuenta.tipo_cuenta == 1)
                        {
                            data[1] = cuenta.cod_cuenta;
                            data[2] = cuenta.idparametro;
                            data[3] = cuenta.tipo_mov;
                        }
                        else if (cuenta.garantia == 1 && cuenta.libranza == 1 && cuenta.tipo_cuenta == 1)
                        {
                            data[4] = cuenta.cod_cuenta;
                            data[5] = cuenta.idparametro;
                            data[6] = cuenta.tipo_mov;
                        }
                        else if (cuenta.garantia == 2 && cuenta.libranza == 2 && cuenta.tipo_cuenta == 1)
                        {
                            data[7] = cuenta.cod_cuenta;
                            data[8] = cuenta.idparametro;
                            data[9] = cuenta.tipo_mov;
                        }
                        else if (cuenta.garantia == 2 && cuenta.libranza == 1 && cuenta.tipo_cuenta == 1)
                        {
                            data[10] = cuenta.cod_cuenta;
                            data[11] = cuenta.idparametro;
                            data[12] = cuenta.tipo_mov;
                        }
                        else if (cuenta.tipo_mov == 1 && cuenta.tipo_cuenta == 3)
                        {
                            data[13] = cuenta.cod_cuenta;
                            data[14] = cuenta.idparametro;
                            data[15] = cuenta.tipo_mov;
                        }
                        else if (cuenta.tipo_mov == 2 && cuenta.tipo_cuenta == 3)
                        {
                            data[16] = cuenta.cod_cuenta;
                            data[17] = cuenta.idparametro;
                            data[18] = cuenta.tipo_mov;
                        }
                    }

                    tbClasif.Rows.Add(data);
                }

                gvClasificacion.DataSource = tbClasif;
                gvClasificacion.DataBind();

                //Cargar cuentas de causación
                DataTable tbCausa = new DataTable();
                tbCausa.Columns.Add("nom_tipo");
                tbCausa.Columns.Add("cod_cuenta");
                tbCausa.Columns.Add("IdParamCa1");
                tbCausa.Columns.Add("TipoParamCa1");

                List<Par_Cue_LinCred> lstCausacion = new List<Par_Cue_LinCred>();
                lstCausacion = (from item in lstParametrizacion
                                where item.tipo == 3 && item.cod_atr == Convert.ToInt64(ddlAtributo.SelectedValue)
                                select item).ToList();
                for (int i = 0; i < 4; i++)
                {
                    data = tbCausa.NewRow();
                    data[0] = i == 0 ? "Cuenta por cobrar" : i == 1 ? "Cuenta ingreso" : i == 2 ? "Cuenta de Orden" : "Cuenta de Orden por Contra";
                    tbCausa.Rows.Add(data);
                }

                foreach (Par_Cue_LinCred cuenta in lstCausacion)
                {
                    if (cuenta.tipo_cuenta == 2 && cuenta.tipo_mov == 1)
                    {
                        foreach (DataRow fila in tbCausa.Rows)
                        {
                            if (fila[0].ToString() == "Cuenta por cobrar")
                            {
                                fila[1] = cuenta.cod_cuenta;
                                fila[2] = cuenta.idparametro;
                                fila[3] = cuenta.tipo_mov;
                            }
                        }
                    }
                    else if (cuenta.tipo_cuenta == 2 && cuenta.tipo_mov == 2)
                    {
                        foreach (DataRow fila in tbCausa.Rows)
                        {
                            if (fila[0].ToString() == "Cuenta ingreso")
                            {
                                fila[1] = cuenta.cod_cuenta;
                                fila[2] = cuenta.idparametro;
                                fila[3] = cuenta.tipo_mov;
                            }
                        }
                    }
                    else if (cuenta.tipo_cuenta == 3 && cuenta.tipo_mov == 1)
                    {
                        foreach (DataRow fila in tbCausa.Rows)
                        {
                            if (fila[0].ToString() == "Cuenta de Orden")
                            {
                                fila[1] = cuenta.cod_cuenta;
                                fila[2] = cuenta.idparametro;
                                fila[3] = cuenta.tipo_mov;
                            }
                        }
                    }
                    else if (cuenta.tipo_cuenta == 3 && cuenta.tipo_mov == 2)
                    {
                        foreach (DataRow fila in tbCausa.Rows)
                        {
                            if (fila[0].ToString() == "Cuenta de Orden por Contra")
                            {
                                fila[1] = cuenta.cod_cuenta;
                                fila[2] = cuenta.idparametro;
                                fila[3] = cuenta.tipo_mov;
                            }
                        }
                    }
                }

                gvCausacion.DataSource = tbCausa;
                gvCausacion.DataBind();

                Xpinn.Comun.Services.GeneralService genServicio = new Xpinn.Comun.Services.GeneralService();
                Xpinn.Comun.Entities.General pGeneral = new Xpinn.Comun.Entities.General();
                pGeneral = genServicio.ConsultarGeneral(44, (Usuario)Session["usuario"]);
                if(pGeneral != null)
                {
                    if(pGeneral.valor == "0" || pGeneral.valor == "")
                    {
                        gvProvision.Columns[2].Visible = false;
                        gvProvision.Columns[1].HeaderText = "Cod. Cuenta Provisión";
                    }
                }
                else
                {
                    gvProvision.Columns[2].Visible = false;
                    gvProvision.Columns[1].HeaderText = "Cod. Cuenta Provisión";
                }

                //Cargar cuentas de provisión
                DataTable tbProvision = new DataTable();
                tbProvision.Columns.Add("cod_categoria");
                tbProvision.Columns.Add("cuenta_prov1");
                tbProvision.Columns.Add("IdParamPr1");
                tbProvision.Columns.Add("TipoParamPr1");
                tbProvision.Columns.Add("cuenta_prov2");
                tbProvision.Columns.Add("IdParamPr2");
                tbProvision.Columns.Add("TipoParamPr2");
                tbProvision.Columns.Add("cuenta_prov3");
                tbProvision.Columns.Add("IdParamPr3");
                tbProvision.Columns.Add("TipoParamPr3");
                tbProvision.Columns.Add("cuenta_prov4");
                tbProvision.Columns.Add("IdParamPr4");
                tbProvision.Columns.Add("TipoParamPr4");

                List<Par_Cue_LinCred> lstProvision = new List<Par_Cue_LinCred>();

                foreach (Par_Cue_LinCred categoria in lstCategorias)
                {
                    lstProvision = (from item in lstParametrizacion
                                    where item.tipo == 2 && item.cod_categoria == categoria.cod_categoria && item.cod_atr == Convert.ToInt64(ddlAtributo.SelectedValue)
                                    select item).ToList();

                    data = tbProvision.NewRow();
                    data[0] = categoria.cod_categoria;

                    foreach (Par_Cue_LinCred cuenta in lstProvision)
                    {
                        if (pGeneral.valor == "1")
                        {
                            if (cuenta.garantia == 1 && cuenta.tipo_cuenta == 2)
                            {
                                data[1] = cuenta.cod_cuenta;
                                data[2] = cuenta.idparametro;
                                data[3] = cuenta.tipo_mov;
                            }
                            else if (cuenta.garantia == 2 && cuenta.tipo_cuenta == 2)
                            {
                                data[4] = cuenta.cod_cuenta;
                                data[5] = cuenta.idparametro;
                                data[6] = cuenta.tipo_mov;
                            }
                            else if (cuenta.tipo_cuenta == 1) //Gasto
                            {
                                data[7] = cuenta.cod_cuenta;
                                data[8] = cuenta.idparametro;
                                data[9] = cuenta.tipo_mov;
                            }
                            else if (cuenta.tipo_cuenta == 0) //Ingreso
                            {
                                data[10] = cuenta.cod_cuenta;
                                data[11] = cuenta.idparametro;
                                data[12] = cuenta.tipo_mov;
                            }
                        }
                        else if ((pGeneral == null || pGeneral.valor == "0" || pGeneral.valor == "") && cuenta.tipo_cuenta == 2)//Provision
                        {
                            data[1] = cuenta.cod_cuenta;
                            data[2] = cuenta.idparametro;
                            data[3] = cuenta.tipo_mov;
                            
                        }
                        else if ((pGeneral == null || pGeneral.valor == "0" || pGeneral.valor == "") && cuenta.tipo_cuenta == 1) //Gasto
                        {
                            data[7] = cuenta.cod_cuenta;
                            data[8] = cuenta.idparametro;
                            data[9] = cuenta.tipo_mov;
                        }
                        else if ((pGeneral == null || pGeneral.valor == "0" || pGeneral.valor == "") && cuenta.tipo_cuenta == 0) //Ingreso
                        {
                            data[10] = cuenta.cod_cuenta;
                            data[11] = cuenta.idparametro;
                            data[12] = cuenta.tipo_mov;
                        }
                    }
                    tbProvision.Rows.Add(data);
                }

                gvProvision.DataSource = tbProvision;
                gvProvision.DataBind();
            }
        }
        catch (Exception ex)
        {
            VerError("" + ex.Message);
        }

    }

    #endregion

    #region Cargar parametros
    private List<Par_Cue_LinCred> ListaParametros()
    {
        try
        {
            List<Par_Cue_LinCred> lstParametros = new List<Par_Cue_LinCred>();

            //Parametro cuenta de operación
            /*Par_Cue_LinCred parametro = new Par_Cue_LinCred()
            {
                cod_linea_credito = ddlLineaCredito.SelectedValue,
                cod_atr = Convert.ToInt32(ddlAtributo.SelectedValue),
                tipo_cuenta = 1,
                tipo = 0,
                cod_cuenta = txtCodCuentaOp.Text,
                cod_cuenta_niif = txtCodCuentaNIIFOp.Text,
                idparametro = Convert.ToInt64(txtIdParamOp.Text),
                tipo_mov = Convert.ToInt32(txtTipoParamOp.Text)
            };*/

            lstParametros.AddRange(ParametrosOperacion());

            int valor = 0;
            VisualizarTabs(ref valor);

            if (valor == 1)
            {
                //Parametros cuentas de clasificación
                lstParametros.AddRange(ParametrosClasificacion());

                //Parametros cuentas de cuasación
                lstParametros.AddRange(ParametrosCausacion());

                //Parametros cuentas de clasificación
                lstParametros.AddRange(ParametrosProvision());
            }

            return lstParametros;
        }
        catch (Exception ex)
        {
            VerError(parametroServicio.CodigoPrograma + ex.Message);
            return null;
        }
    }

    private List<Par_Cue_LinCred> ParametrosOperacion()
    {
        try
        {
            List<Par_Cue_LinCred> lstParametros = new List<Par_Cue_LinCred>();
            List<Par_Cue_LinCred> lstParametrosTran = new List<Par_Cue_LinCred>();
            Par_Cue_LinCred parametro;
            if (VerificarLineaCastigada(ddlLineaCredito.SelectedValue))
            {
                //Cuenta de orden 
                parametro = new Par_Cue_LinCred()
                {
                    cod_linea_credito = ddlLineaCredito.SelectedValue,
                    cod_atr = Convert.ToInt32(ddlAtributo.SelectedValue),
                    tipo_cuenta = 1,
                    tipo = 0,
                    cod_cuenta = txtCodCuentaOp.Text,
                    cod_cuenta_niif = txtCodCuentaNIIFOp.Text,
                    idparametro = txtIdParamOp.Text != "" ? Convert.ToInt64(txtIdParamOp.Text) : 0,
                    tipo_mov = txtTipoParamOp.Text != "" ? Convert.ToInt32(txtTipoParamOp.Text) : 0
                };
                lstParametros.Add(parametro);
                //Cuenta de orden por contra
                parametro = new Par_Cue_LinCred()
                {
                    cod_linea_credito = ddlLineaCredito.SelectedValue,
                    cod_atr = Convert.ToInt32(ddlAtributo.SelectedValue),
                    tipo_cuenta = 1,
                    tipo = 0,
                    cod_cuenta = txtCodCuentaOp1.Text,
                    cod_cuenta_niif = txtCodCuentaNIIFOp1.Text,
                    idparametro = txtIdParamOp1.Text != "" ? Convert.ToInt64(txtIdParamOp1.Text) : 0,
                    tipo_mov = txtTipoParamOp1.Text != "" ? Convert.ToInt32(txtTipoParamOp1.Text) : 0
                };
                lstParametros.Add(parametro);
            }
            else //Si no es línea castigada solo cargar una cuenta de operación sin transacción
            {
                //Parametro cuenta de operación
                parametro = new Par_Cue_LinCred()
                {
                    cod_linea_credito = ddlLineaCredito.SelectedValue,
                    cod_atr = Convert.ToInt32(ddlAtributo.SelectedValue),
                    tipo_cuenta = 1,
                    tipo = 0,
                    cod_cuenta = txtCodCuentaOp.Text,
                    cod_cuenta_niif = txtCodCuentaNIIFOp.Text,
                    idparametro = txtIdParamOp.Text != "" ? Convert.ToInt64(txtIdParamOp.Text) : 0,
                    tipo_mov = txtTipoParamOp.Text != "" ? Convert.ToInt32(txtTipoParamOp.Text) : 0
                };
                lstParametros.Add(parametro);
            }
            
            //Cargar cuentas de operación que tengan tipo de transacción
            foreach (GridViewRow fila in gvOperacion.Rows)
            {                
                TextBox txtCuentaOpTran = (TextBox)fila.FindControl("txtCuentaOpTran");
                TextBox txtIdParamOp1 = (TextBox)fila.FindControl("txtIdParamOp1");
                TextBox txtTipoParamOp1 = (TextBox)fila.FindControl("txtTipoParamOp1");
                if (txtCuentaOpTran.Text != "")
                {
                    parametro = new Par_Cue_LinCred();
                    parametro.cod_linea_credito = ddlLineaCredito.SelectedValue;
                    parametro.cod_atr = Convert.ToInt32(ddlAtributo.SelectedValue);
                    parametro.tipo_cuenta = 1;
                    parametro.tipo = 0;
                    parametro.cod_cuenta = txtCuentaOpTran.Text;
                    parametro.idparametro = txtIdParamOp1.Text != "" && txtIdParamOp1.Text != null ? Convert.ToInt64(txtIdParamOp1.Text) : 0;
                    parametro.tipo_mov = txtTipoParamOp1.Text != "" && txtTipoParamOp1.Text != null ? Convert.ToInt32(txtTipoParamOp1.Text) : 0;
                    ctlListarCodigo ctllistar = (ctlListarCodigo)fila.FindControl("ddlTipoTran");
                    if (!string.IsNullOrWhiteSpace(ctllistar.Codigo))
                        parametro.tipo_tran = Convert.ToInt64(ctllistar.Codigo);
                    lstParametrosTran.Add(parametro);
                }
            }
            Session["lstOpe"] = lstParametrosTran;
            lstParametros.AddRange(lstParametrosTran);
            return lstParametros;
        }
        catch (Exception ex)
        {
            VerError(parametroServicio.CodigoPrograma + ex.Message);
            return null;
        }

    }

    private List<Par_Cue_LinCred> ParametrosClasificacion()
    {
        try
        {
            List<Par_Cue_LinCred> lstParametros = new List<Par_Cue_LinCred>();
            Par_Cue_LinCred parametro = new Par_Cue_LinCred();

            foreach (GridViewRow fila in gvClasificacion.Rows)
            {

                Label lblCategoriaCl = (Label)fila.FindControl("lblCategoriaCl");

                //ADMISIBLE CON LIBRANZA
                TextBox txtCuentaConLibAd = (TextBox)fila.FindControl("txtCuentaConLibAd");
                TextBox txtIdParamCl1 = (TextBox)fila.FindControl("txtIdParamCl1");
                TextBox txtTipoParamCl1 = (TextBox)fila.FindControl("txtTipoParamCl1");
                if (txtCuentaConLibAd.Text != "")
                {
                    parametro = new Par_Cue_LinCred();
                    parametro.cod_linea_credito = ddlLineaCredito.SelectedValue;
                    parametro.cod_categoria = lblCategoriaCl.Text;
                    parametro.cod_atr = Convert.ToInt32(ddlAtributo.SelectedValue);
                    parametro.tipo_cuenta = 1;
                    parametro.tipo = 1;
                    parametro.garantia = 1;
                    parametro.libranza = 2;
                    parametro.cod_cuenta = txtCuentaConLibAd.Text;
                    parametro.tipo_cuenta = 1;
                    parametro.idparametro = txtIdParamCl1.Text != "" && txtIdParamCl1.Text != null ? Convert.ToInt64(txtIdParamCl1.Text) : 0;
                    parametro.tipo_mov = txtTipoParamCl1.Text != "" && txtTipoParamCl1.Text != null ? Convert.ToInt32(txtTipoParamCl1.Text) : 0;
                    lstParametros.Add(parametro);
                }
                

                //ADMISIBLE SIN LIBRANZA
                TextBox txtCuentaSinLibAd = (TextBox)fila.FindControl("txtCuentaSinLibAd");
                TextBox txtIdParamCl2 = (TextBox)fila.FindControl("txtIdParamCl2");
                TextBox txtTipoParamCl2 = (TextBox)fila.FindControl("txtTipoParamCl2");
                if (txtCuentaSinLibAd.Text != "")
                {
                    parametro = new Par_Cue_LinCred();
                    parametro.cod_linea_credito = ddlLineaCredito.SelectedValue;
                    parametro.cod_categoria = lblCategoriaCl.Text;
                    parametro.cod_atr = Convert.ToInt32(ddlAtributo.SelectedValue);
                    parametro.tipo_cuenta = 1;
                    parametro.tipo = 1;
                    parametro.garantia = 1;
                    parametro.libranza = 1;
                    parametro.cod_cuenta = txtCuentaSinLibAd.Text;
                    parametro.tipo_cuenta = 1;
                    parametro.idparametro = txtIdParamCl2.Text != "" && txtIdParamCl2.Text != null ? Convert.ToInt64(txtIdParamCl2.Text) : 0;
                    parametro.tipo_mov = txtTipoParamCl2.Text != "" && txtTipoParamCl2.Text != null ? Convert.ToInt32(txtTipoParamCl2.Text) : 0;
                    lstParametros.Add(parametro);
                }

                //NO ADMISIBLE CON LIBRANZA
                TextBox txtCuentaConLibNoAd = (TextBox)fila.FindControl("txtCuentaConLibNoAd");
                TextBox txtIdParamCl3 = (TextBox)fila.FindControl("txtIdParamCl3");
                TextBox txtTipoParamCl3 = (TextBox)fila.FindControl("txtTipoParamCl3");
                if (txtCuentaConLibNoAd.Text != "")
                {
                    parametro = new Par_Cue_LinCred();
                    parametro.cod_linea_credito = ddlLineaCredito.SelectedValue;
                    parametro.cod_categoria = lblCategoriaCl.Text;
                    parametro.cod_atr = Convert.ToInt32(ddlAtributo.SelectedValue);
                    parametro.tipo_cuenta = 1;
                    parametro.tipo = 1;
                    parametro.garantia = 2;
                    parametro.libranza = 2;
                    parametro.cod_cuenta = txtCuentaConLibNoAd.Text;
                    parametro.tipo_cuenta = 1;
                    parametro.idparametro = txtIdParamCl3.Text != "" && txtIdParamCl3.Text != null ? Convert.ToInt64(txtIdParamCl3.Text) : 0;
                    parametro.tipo_mov = txtTipoParamCl3.Text != "" && txtTipoParamCl3.Text != null ? Convert.ToInt32(txtTipoParamCl3.Text) : 0;
                    lstParametros.Add(parametro);
                }

                //NO ADMISIBLE SIN LIBRANZA
                TextBox txtCuentaSinLibNoAd = (TextBox)fila.FindControl("txtCuentaSinLibNoAd");
                TextBox txtIdParamCl4 = (TextBox)fila.FindControl("txtIdParamCl4");
                TextBox txtTipoParamCl4 = (TextBox)fila.FindControl("txtTipoParamCl4");
                if (txtCuentaSinLibNoAd.Text != "")
                {
                    parametro = new Par_Cue_LinCred();
                    parametro.cod_linea_credito = ddlLineaCredito.SelectedValue;
                    parametro.cod_categoria = lblCategoriaCl.Text;
                    parametro.cod_atr = Convert.ToInt32(ddlAtributo.SelectedValue);
                    parametro.tipo_cuenta = 1;
                    parametro.tipo = 1;
                    parametro.garantia = 2;
                    parametro.libranza = 1;
                    parametro.cod_cuenta = txtCuentaSinLibNoAd.Text;
                    parametro.tipo_cuenta = 1;
                    parametro.idparametro = txtIdParamCl4.Text != "" && txtTipoParamCl4.Text != null ? Convert.ToInt64(txtIdParamCl4.Text) : 0;
                    parametro.tipo_mov = txtTipoParamCl4.Text != "" && txtTipoParamCl4.Text != null ? Convert.ToInt32(txtTipoParamCl4.Text) : 1;
                    lstParametros.Add(parametro);
                }

                //CUENTA DE ORDEN
                TextBox txtCuentaOrden = (TextBox)fila.FindControl("txtCuentaOrden");
                TextBox txtIdParamCl5 = (TextBox)fila.FindControl("txtIdParamCl5");
                TextBox txtTipoParamCl5 = (TextBox)fila.FindControl("txtTipoParamCl5");
                if (txtCuentaOrden.Text != "")
                {
                    parametro = new Par_Cue_LinCred();
                    parametro.cod_linea_credito = ddlLineaCredito.SelectedValue;
                    parametro.cod_categoria = lblCategoriaCl.Text;
                    parametro.cod_atr = Convert.ToInt32(ddlAtributo.SelectedValue);
                    parametro.tipo = 1;
                    parametro.garantia = null;
                    parametro.libranza = null;
                    parametro.tipo_cuenta = 3;
                    parametro.cod_cuenta = txtCuentaOrden.Text;
                    parametro.idparametro = txtIdParamCl5.Text != "" && txtIdParamCl5.Text != null ? Convert.ToInt64(txtIdParamCl5.Text) : 0;
                    parametro.tipo_mov = txtTipoParamCl5.Text != "" && txtTipoParamCl5.Text != null ? Convert.ToInt32(txtTipoParamCl5.Text) : 1;
                    lstParametros.Add(parametro);
                }

                //CUENTA DE ORDEN POR CONTRA
                TextBox txtCuentaOrdenContra = (TextBox)fila.FindControl("txtCuentaOrdenContra");
                TextBox txtIdParamCl6 = (TextBox)fila.FindControl("txtIdParamCl6");
                TextBox txtTipoParamCl6 = (TextBox)fila.FindControl("txtTipoParamCl6");
                if (txtCuentaOrdenContra.Text != "")
                {
                    parametro = new Par_Cue_LinCred();
                    parametro.cod_linea_credito = ddlLineaCredito.SelectedValue;
                    parametro.cod_categoria = lblCategoriaCl.Text;
                    parametro.cod_atr = Convert.ToInt32(ddlAtributo.SelectedValue);
                    parametro.tipo = 1;
                    parametro.garantia = null;
                    parametro.libranza = null;
                    parametro.tipo_cuenta = 3;
                    parametro.cod_cuenta = txtCuentaOrdenContra.Text;
                    parametro.idparametro = txtIdParamCl6.Text != "" && txtIdParamCl6.Text != null ? Convert.ToInt64(txtIdParamCl6.Text) : 0;
                    parametro.tipo_mov = txtTipoParamCl6.Text != "" && txtTipoParamCl6.Text != null ? Convert.ToInt32(txtTipoParamCl6.Text) : 2;
                    lstParametros.Add(parametro);
                }
            }
            return lstParametros;
        }
        catch (Exception ex)
        {
            VerError(parametroServicio.CodigoPrograma + ex.Message);
            return null;
        }

    }

    private List<Par_Cue_LinCred> ParametrosCausacion()
    {
        try
        {
            List<Par_Cue_LinCred> lstParametros = new List<Par_Cue_LinCred>();
            Par_Cue_LinCred parametro = new Par_Cue_LinCred();

            foreach (GridViewRow fila in gvCausacion.Rows)
            {
                Label lblCuentaCausa = (Label)fila.FindControl("lblCuentaCausa");
                TextBox txtCodCuentaCausa = (TextBox)fila.FindControl("txtCodCuentaCausa");
                TextBox txtIdParamCa1 = (TextBox)fila.FindControl("txtIdParamCa1");
                TextBox txtTipoParamCa1 = (TextBox)fila.FindControl("txtTipoParamCa1");

                if (txtCodCuentaCausa.Text != "")
                {
                    parametro = new Par_Cue_LinCred();
                    parametro.cod_linea_credito = ddlLineaCredito.SelectedValue;
                    parametro.cod_atr = Convert.ToInt32(ddlAtributo.SelectedValue);
                    parametro.idparametro = txtIdParamCa1.Text != null && txtIdParamCa1.Text != "" ? Convert.ToInt64(txtIdParamCa1.Text) : 0;
                    parametro.tipo_mov = txtTipoParamCa1.Text != null && txtTipoParamCa1.Text != "" ? Convert.ToInt32(txtTipoParamCa1.Text) : 0;
                    parametro.cod_cuenta = txtCodCuentaCausa.Text;
                    parametro.tipo = 3;

                    if (lblCuentaCausa.Text == "Cuenta por cobrar")                        
                    {
                        parametro.tipo_cuenta = 2;
                        parametro.tipo_mov = parametro.tipo_mov == 0 ? 1 : parametro.tipo_mov;
                    }
                    else if(lblCuentaCausa.Text == "Cuenta ingreso")
                    {
                        parametro.tipo_cuenta = 2;
                        parametro.tipo_mov = parametro.tipo_mov == 0 ? 2 : parametro.tipo_mov;
                    }
                    else if (lblCuentaCausa.Text == "Cuenta de Orden")
                    {
                        parametro.tipo_cuenta = 3;
                        parametro.tipo_mov = parametro.tipo_mov == 0 ? 1 : parametro.tipo_mov;
                    }
                    else if (lblCuentaCausa.Text == "Cuenta de Orden por Contra")
                    {
                        parametro.tipo_cuenta = 3;
                        parametro.tipo_mov = parametro.tipo_mov == 0 ? 2 : parametro.tipo_mov;
                    }
                    lstParametros.Add(parametro);
                }
            }
            return lstParametros;
        }
        catch (Exception ex)
        {
            VerError(parametroServicio.CodigoPrograma + ex.Message);
            return null;
        }
    }

    private List<Par_Cue_LinCred> ParametrosProvision()
    {
        try
        {
            List<Par_Cue_LinCred> lstParametros = new List<Par_Cue_LinCred>();
            Par_Cue_LinCred parametro = new Par_Cue_LinCred();
            Xpinn.Comun.Services.GeneralService genServicio = new Xpinn.Comun.Services.GeneralService();
            Xpinn.Comun.Entities.General pGeneral = new Xpinn.Comun.Entities.General();
            pGeneral = genServicio.ConsultarGeneral(44, (Usuario)Session["usuario"]);

            foreach (GridViewRow fila in gvProvision.Rows)
            {

                Label lblCategoriaProv = (Label)fila.FindControl("lblCategoriaProv");
                
                //ADMISIBLE
                TextBox txtCuentaProvAd = (TextBox)fila.FindControl("txtCuentaProvAd");
                TextBox txtIdParamPr1 = (TextBox)fila.FindControl("txtIdParamPr1");
                TextBox txtTipoParamPr1 = (TextBox)fila.FindControl("txtTipoParamPr1");
                if (txtCuentaProvAd.Text != "")
                {
                    parametro = new Par_Cue_LinCred();
                    parametro.cod_linea_credito = ddlLineaCredito.SelectedValue;
                    parametro.cod_categoria = lblCategoriaProv.Text;
                    parametro.cod_atr = Convert.ToInt32(ddlAtributo.SelectedValue);
                    parametro.tipo = 2;
                    if(pGeneral != null && pGeneral.valor == "1") 
                        parametro.garantia = 1;
                    else
                        parametro.garantia = null;
                    parametro.libranza = null;
                    parametro.cod_cuenta = txtCuentaProvAd.Text;
                    parametro.tipo_cuenta = 2;
                    parametro.idparametro = txtIdParamPr1.Text != null && txtIdParamPr1.Text != "" ? Convert.ToInt64(txtIdParamPr1.Text) : 0;
                    parametro.tipo_mov = txtTipoParamPr1.Text != "" && txtTipoParamPr1.Text != null ? Convert.ToInt32(txtTipoParamPr1.Text) : 0;
                    lstParametros.Add(parametro);
                }                

                //NO ADMISIBLE
                TextBox txtCuentaProNoAd = (TextBox)fila.FindControl("txtCuentaProNoAd");
                TextBox txtIdParamPr2 = (TextBox)fila.FindControl("txtIdParamPr2");
                TextBox txtTipoParamPr2 = (TextBox)fila.FindControl("txtTipoParamPr2");
                if (txtCuentaProNoAd.Text != "")
                {
                    parametro = new Par_Cue_LinCred();
                    parametro.cod_linea_credito = ddlLineaCredito.SelectedValue;
                    parametro.cod_categoria = lblCategoriaProv.Text;
                    parametro.cod_atr = Convert.ToInt32(ddlAtributo.SelectedValue);
                    parametro.tipo = 2;
                    parametro.garantia = 2;
                    parametro.libranza = null;
                    parametro.cod_cuenta = txtCuentaProNoAd.Text;
                    parametro.tipo_cuenta = 2;
                    parametro.idparametro = txtIdParamPr2.Text != null && txtIdParamPr2.Text != "" ? Convert.ToInt64(txtIdParamPr2.Text) : 0;
                    parametro.tipo_mov = txtTipoParamPr2.Text != "" && txtTipoParamPr2.Text != null ? Convert.ToInt32(txtTipoParamPr2.Text) : 0;
                    lstParametros.Add(parametro);
                }                

                //CUENTA GASTO 
                TextBox txtCuentaGasto = (TextBox)fila.FindControl("txtCuentaGasto");
                TextBox txtIdParamPr3 = (TextBox)fila.FindControl("txtIdParamPr3");
                TextBox txtTipoParamPr3 = (TextBox)fila.FindControl("txtTipoParamPr3");
                if (txtCuentaGasto.Text != "")
                {
                    parametro = new Par_Cue_LinCred();
                    parametro.cod_linea_credito = ddlLineaCredito.SelectedValue;
                    parametro.cod_categoria = lblCategoriaProv.Text;
                    parametro.cod_atr = Convert.ToInt32(ddlAtributo.SelectedValue);
                    parametro.tipo = 2;
                    parametro.garantia = null;
                    parametro.libranza = null;
                    parametro.cod_cuenta = txtCuentaGasto.Text;
                    parametro.tipo_cuenta = 1;
                    parametro.idparametro = txtIdParamPr3.Text != null && txtIdParamPr3.Text != "" ? Convert.ToInt64(txtIdParamPr3.Text) : 0;
                    parametro.tipo_mov = txtTipoParamPr3.Text != "" && txtTipoParamPr3.Text != null ? Convert.ToInt32(txtTipoParamPr3.Text) : 0;
                    lstParametros.Add(parametro);
                }
                

                //CUENTA INGRESO
                TextBox txtCuentaIngre = (TextBox)fila.FindControl("txtCuentaIngre");
                TextBox txtIdParamPr4 = (TextBox)fila.FindControl("txtIdParamPr4");
                TextBox txtTipoParamPr4 = (TextBox)fila.FindControl("txtTipoParamPr4");
                if (txtCuentaIngre.Text != "")
                {
                    parametro = new Par_Cue_LinCred();
                    parametro.cod_linea_credito = ddlLineaCredito.SelectedValue;
                    parametro.cod_categoria = lblCategoriaProv.Text;
                    parametro.cod_atr = Convert.ToInt32(ddlAtributo.SelectedValue);
                    parametro.tipo = 2;
                    parametro.garantia = null;
                    parametro.libranza = null;
                    parametro.cod_cuenta = txtCuentaIngre.Text;
                    parametro.tipo_cuenta = 0;
                    parametro.idparametro = txtIdParamPr4.Text != null && txtIdParamPr4.Text != "" ? Convert.ToInt64(txtIdParamPr4.Text) : 0;
                    parametro.tipo_mov = txtTipoParamPr4.Text != "" && txtTipoParamPr4.Text != null ? Convert.ToInt32(txtTipoParamPr4.Text) : 0;
                    lstParametros.Add(parametro);
                }
                
            }
            return lstParametros;
        }
        catch (Exception ex)
        {
            VerError(parametroServicio.CodigoPrograma + ex.Message);
            return null;
        }
    }

    #endregion


}