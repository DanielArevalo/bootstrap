using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Services;
using Xpinn.Caja.Services;
using System.Linq;
using Xpinn.Comun.Services;
using Xpinn.Comun.Entities;

partial class Lista : GlobalWeb
{
    RealizacionGirosServices RealizacionService = new RealizacionGirosServices();
    ImpresionMasivaServices ComprobanteServicio = new ImpresionMasivaServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(RealizacionService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.MostrarExportar(false);
            toolBar.MostrarLimpiar(true);
            toolBar.MostrarConsultar(true);
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
            toolBar.MostrarImprimir(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RealizacionService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                InicializarVariablesSession();
                Session["DATA_RGIROS"] = null;
                mvPrincipal.ActiveViewIndex = 0;
                panelGrilla.Visible = false;
                txtFechaRealiza.Text = DateTime.Now.ToShortDateString();
                txtFechaAplicacion.Text = DateTime.Now.ToShortDateString();
                rblFormaPago.SelectedIndex = 0;
                rblFormaPago_SelectedIndexChanged(rblFormaPago, null);
                CargarDropDown();
            }
            else
            {
                CalcularTotal();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RealizacionService.CodigoPrograma, "Page_Load", ex);
        }
    }


    void CargarDropDown()
    {
        PoblarListas PoblarLista = new PoblarListas();
        PoblarLista.PoblarListaDesplegable("TIPO_ACTO_GIRO", "", "", "1", ddlGenerado, Usuario);

        LlenarListasDesplegables(TipoLista.TipoComprobante, ddlTipoComp);
        LlenarListasDesplegables(TipoLista.Bancos, ddlEntidad);
        LlenarListasDesplegables(TipoLista.CuentaBancariasBancos, ddlCuentas);
        LlenarListasDesplegables(TipoLista.BancosEntidad, ddlEntidad_giro);
        CargarCuentas();

        string ddlusuarios = ConfigurationManager.AppSettings["ddlusuarios"].ToString();
        if (ddlusuarios == "1")
        {
            // Cargar los asesores ejecutivos
            UsuarioAseService serviceEjecutivo = new UsuarioAseService();
            List<UsuarioAse> lstAsesores = serviceEjecutivo.ListartodosUsuarios(new UsuarioAse(), Usuario);
            if (lstAsesores.Count > 0)
            {
                ddlUsuarioGen.DataSource = lstAsesores;
                ddlUsuarioGen.DataTextField = "nombre";
                ddlUsuarioGen.DataValueField = "codusuario";
                ddlUsuarioGen.DataBind();

                ddlUsuarioApro.DataSource = lstAsesores;
                ddlUsuarioApro.DataTextField = "nombre";
                ddlUsuarioApro.DataValueField = "codusuario";
                ddlUsuarioApro.DataBind();
            }
            else
            {
                ddlusuarios = "0";
            }
        }
        if (ddlusuarios != "1")
        {
            // Cargar usuarios cuando no se manejan asesores
            Xpinn.Seguridad.Services.UsuarioService serviceEjecutivo = new Xpinn.Seguridad.Services.UsuarioService();
            Usuario usu = new Usuario();
            usu.estado = 1;

            List<Usuario> lstUsuarios = serviceEjecutivo.ListarUsuario(usu, Usuario);

            ddlUsuarioGen.DataSource = lstUsuarios;
            ddlUsuarioGen.DataTextField = "nombre";
            ddlUsuarioGen.DataValueField = "codusuario";
            ddlUsuarioGen.DataBind();

            ddlUsuarioApro.DataSource = lstUsuarios;
            ddlUsuarioApro.DataTextField = "nombre";
            ddlUsuarioApro.DataValueField = "codusuario";
            ddlUsuarioApro.DataBind();
        }


        ddlOrdenadoPor.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlOrdenadoPor.Items.Insert(1, new ListItem("Número de Giro", "IDGIRO"));
        ddlOrdenadoPor.Items.Insert(2, new ListItem("Fecha Registro", "FEC_REG"));
        ddlOrdenadoPor.Items.Insert(3, new ListItem("Código Persona", "COD_PERSONA"));
        ddlOrdenadoPor.Items.Insert(4, new ListItem("Identificación", "IDENTIFICACION"));
        ddlOrdenadoPor.Items.Insert(5, new ListItem("Nombre", "NOMBRE"));
        ddlOrdenadoPor.Items.Insert(6, new ListItem("Num Comprobante", "NUM_COMP"));
        ddlOrdenadoPor.Items.Insert(7, new ListItem("Tipo Comprobante", "TIPO_COMP"));
        ddlOrdenadoPor.Items.Insert(8, new ListItem("Forma de Pago", "FORMA_PAGO"));
        ddlOrdenadoPor.Items.Insert(9, new ListItem("Valor", "VALOR"));

        ddlLuegoPor.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlLuegoPor.Items.Insert(1, new ListItem("Número de Giro", "IDGIRO"));
        ddlLuegoPor.Items.Insert(2, new ListItem("Fecha Registro", "FEC_REG"));
        ddlLuegoPor.Items.Insert(3, new ListItem("Código Persona", "COD_PERSONA"));
        ddlLuegoPor.Items.Insert(4, new ListItem("Identificación", "IDENTIFICACION"));
        ddlLuegoPor.Items.Insert(5, new ListItem("Nombre", "NOMBRE"));
        ddlLuegoPor.Items.Insert(6, new ListItem("Num Comprobante", "NUM_COMP"));
        ddlLuegoPor.Items.Insert(7, new ListItem("Tipo Comprobante", "TIPO_COMP"));
        ddlLuegoPor.Items.Insert(8, new ListItem("Forma de Pago", "FORMA_PAGO"));
        ddlLuegoPor.Items.Insert(9, new ListItem("Valor", "VALOR"));
    }

    protected void CargarCuentas()
    {
        if (!string.IsNullOrWhiteSpace(ddlEntidad_giro.SelectedValue))
        {
            BancosService bancoService = new BancosService();
            long codbanco = Convert.ToInt64(ddlEntidad_giro.SelectedValue);
            ddlCuenta_Giro.DataSource = bancoService.ListarCuentaBancos(codbanco, Usuario);
            ddlCuenta_Giro.DataTextField = "num_cuenta";
            ddlCuenta_Giro.DataValueField = "idctabancaria";
            ddlCuenta_Giro.DataBind();
        }
    }

    protected void ddlEntidad_giro_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarCuentas();
    }


    protected void CalcularTotal()
    {
        decimal TotalAprobar = 0;
        int cont = 0;


        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBoxGrid cbSeleccionar = (CheckBoxGrid)rFila.FindControl("cbSeleccionar");
            if (cbSeleccionar.Checked)
            {
                decimal valor;
                valor = rFila.Cells[14].Text != "&nbsp;" ? Convert.ToDecimal(rFila.Cells[14].Text) : 0;
                TotalAprobar += valor;
                cont++;
            }
        }
        txtVrRealizar.Text = TotalAprobar.ToString();
        txtNumGirosReali.Text = cont.ToString();
    }

    protected void cbSeleccionarEncabezado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbSeleccionarEncabezado = (CheckBox)sender;
        if (cbSeleccionarEncabezado != null)
        {
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBoxGrid cbSeleccionar = (CheckBoxGrid)rFila.FindControl("cbSeleccionar");
                cbSeleccionar.Checked = cbSeleccionarEncabezado.Checked;


            }
            CalcularTotal();
        }
    }

    public static void KeepSelection(GridView grid)
    {
        //
        // se obtienen los id de producto checkeados de la pagina actual
        //
        List<int> checkedProd = (from item in grid.Rows.Cast<GridViewRow>()
                                 let check = (CheckBox)item.FindControl("cbSeleccionar")
                                 where check.Checked
                                 select Convert.ToInt32(grid.DataKeys[item.RowIndex].Value)).ToList()
                                 ;


        //
        // se recupera de session la lista de seleccionados previamente
        //
        List<int> GirosSeleccionados = HttpContext.Current.Session["GirosSeleccionados"] as List<int>;

        if (GirosSeleccionados == null)
            GirosSeleccionados = new List<int>();

        //
        // se cruzan todos los registros de la pagina actual del gridview con la lista de seleccionados,
        // si algun item de esa pagina fue marcado previamente no se devuelve
        //
        GirosSeleccionados = (from item in GirosSeleccionados
                              join item2 in grid.Rows.Cast<GridViewRow>()
                            on item equals Convert.ToInt32(grid.DataKeys[item2.RowIndex].Value) into g
                              where !g.Any()
                              select item).ToList();




        //
        // se agregan los seleccionados
        //
        GirosSeleccionados.AddRange(checkedProd);

        HttpContext.Current.Session["GirosSeleccionados"] = GirosSeleccionados;

    }
    public static void RestoreSelection(GridView grid)
    {

        List<int> productsIdSel = HttpContext.Current.Session["GirosSeleccionados"] as List<int>;

        if (productsIdSel == null)
            return;

        //
        // se comparan los registros de la pagina del grid con los recuperados de la Session
        // los coincidentes se devuelven para ser seleccionados
        //
        List<GridViewRow> result = (from item in grid.Rows.Cast<GridViewRow>()
                                    join item2 in productsIdSel
                                    on Convert.ToInt32(grid.DataKeys[item.RowIndex].Value) equals item2 into g
                                    where g.Any()
                                    select item).ToList();


        //
        // se recorre cada item para marcarlo
        //
        result.ForEach(x => ((CheckBox)x.FindControl("cbSeleccionar")).Checked = true);

    }

    private void Actualizar()
    {
        try
        {
            DateTime pFechaGiro, pFechaApro;
            pFechaGiro = txtFechaGiro.ToDateTime == null ? DateTime.MinValue : txtFechaGiro.ToDateTime;
            pFechaApro = txtFechaAprobacion.ToDateTime == null ? DateTime.MinValue : txtFechaAprobacion.ToDateTime;

            String Orden = "";
            if (ddlOrdenadoPor.SelectedIndex != 0)
                Orden += "g." + ddlOrdenadoPor.SelectedValue;
            if (ddlLuegoPor.SelectedIndex != 0)
            {
                if (Orden != "")
                    Orden += ", g." + ddlLuegoPor.SelectedValue;
                else
                    Orden += " g." + ddlLuegoPor.SelectedValue;
            }
            Boolean forma_pago = true;
            if (rblFormaPago.SelectedIndex != 0)
                forma_pago = false;
            List<Giro> lstConsulta = RealizacionService.ListarGiroAprobados(ObtenerValores(), Orden, pFechaGiro, pFechaApro, forma_pago, Usuario);

            gvLista.PageSize = 50;
            gvLista.EmptyDataText = emptyQuery;

            Site toolBar = (Site)this.Master;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;
                ValidarPermisosGrilla(gvLista);
                Session["DTGIROS"] = lstConsulta;
                lblInfo.Visible = false;
                toolBar.MostrarGuardar(true);
                toolBar.MostrarExportar(true);
            }
            else
            {
                gvLista.DataSource = null;
                Session["DTGIROS"] = null;
                panelGrilla.Visible = false;
                lblInfo.Visible = true;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarExportar(false);
            }
            Session.Add(RealizacionService.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RealizacionService.CodigoPrograma, "Actualizar", ex);
        }
    }


    Giro ObtenerValores()
    {
        Giro vGiro = new Giro();

        if (txtCodGiro.Text.Trim() != "")
            vGiro.idgiro = Convert.ToInt32(txtCodGiro.Text.Trim());
        if (txtIdentificacion.Text.Trim() != "")
            vGiro.identificacion = Convert.ToString(txtIdentificacion.Text.Trim());
        if (txtNombres.Text.Trim() != "")
            vGiro.nombre = txtNombres.Text.Trim();
        if (txtNumComp.Text.Trim() != "")
            vGiro.num_comp = Convert.ToInt32(txtNumComp.Text.Trim());
        if (ddlTipoComp.SelectedIndex != 0)
            vGiro.tipo_comp = Convert.ToInt32(ddlTipoComp.SelectedValue);
        if (txtFechaGiro.TieneDatos)
            vGiro.fec_giro = txtFechaGiro.ToDateTime;
        if (ddlUsuarioGen.SelectedIndex != 0)
            vGiro.usu_gen = ddlUsuarioGen.SelectedItem.Text.Trim();
        if (ddlUsuarioApro.SelectedIndex != 0)
            vGiro.usu_apro = ddlUsuarioApro.SelectedItem.Text.Trim();
        if (ddlGenerado.SelectedIndex != 0)
            vGiro.tipo_acto = Convert.ToInt32(ddlGenerado.SelectedValue);
        if (txtNumRadicacion.Text.Trim() != "")
            vGiro.numero_radicacion = Convert.ToInt64(txtNumRadicacion.Text);
        if (ddlEntidad_giro.Enabled == true)
            vGiro.cod_banco = Convert.ToInt64(ddlEntidad_giro.SelectedValue);
        if (ddlCuenta_Giro.Enabled == true)
            vGiro.num_referencia = ddlCuenta_Giro.SelectedItem.Text.Trim();

        return vGiro;
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        if (mvPrincipal.ActiveViewIndex == 2 || mvPrincipal.ActiveViewIndex == 1)
        {
            Site toolBar = (Site)this.Master;
            toolBar.MostrarLimpiar(true);
            toolBar.MostrarConsultar(true);
            toolBar.MostrarExportar(false);
            toolBar.MostrarGuardar(false);
            toolBar.MostrarImprimir(false);
            if (mvPrincipal.ActiveViewIndex == 1)
            {
                toolBar.MostrarGuardar(true);
                toolBar.MostrarExportar(true);
            }
            toolBar.MostrarCancelar(false);

            VerError("");
            if (mvPrincipal.ActiveViewIndex == 2)
            {
                LimpiarValoresConsulta(pBusqueda, RealizacionService.CodigoPrograma);
                txtFechaRealiza.Text = DateTime.Now.ToShortDateString();
                rblFormaPago.SelectedIndex = 0;
                rblFormaPago_SelectedIndexChanged(rblFormaPago, null);
                txtFechaGiro.Text = "";
                txtFechaAprobacion.Text = "";
                lblInfo.Visible = false;
                ddlEntidad_giro.SelectedIndex = 0;
                ddlEntidad_giro_SelectedIndexChanged(ddlEntidad_giro, null);
                gvLista.DataSource = null;
                panelGrilla.Visible = false;
            }
            mvPrincipal.ActiveViewIndex = 0;
        }

    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Page.Validate();
        if (Page.IsValid)
        {
            Actualizar();
            CalcularTotal();
        }
    }

    private bool ValidarDatos()
    {
        VerError("");
        Giro pEntidad = new Giro();

        pEntidad.lstGiro = ObtenerListaRealizarGiros();

        // Validar que se seleccionaron giros
        if (pEntidad.lstGiro == null || pEntidad.lstGiro.Count == 0)
        {
            VerError("No existen giros seleccionados por realizar");
            return false;
        }

        // Validar que exista la parametrización contable por procesos
        if (ValidarProcesoContable(Convert.ToDateTime(txtFechaRealiza.Text), 103) == false)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación 103=Realización de Giros");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtFechaAplicacion.Texto))
        {
            VerError("Fecha de aplicación no puede estar vacía!.");
            return false;
        }

        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBoxGrid cbSeleccionar = (CheckBoxGrid)rFila.FindControl("cbSeleccionar");
            if (cbSeleccionar != null)
            {
                if (cbSeleccionar.Checked == true)
                {
                    string pNum_Giro = gvLista.DataKeys[rFila.RowIndex].Values[0].ToString();
                    CheckBoxGrid chkDistribuir = (CheckBoxGrid)rFila.FindControl("chkDistribuir");
                    if (chkDistribuir != null && chkDistribuir.Checked)
                    {
                        VerError("Error en la Fila " + (rFila.RowIndex + 1) + " El giro número : " + pNum_Giro + " no puede realizarse debido a que mantiene una distribución");
                        return false;
                    }
                    if (rFila.Cells[9].Text == "&nbsp;" || string.IsNullOrWhiteSpace(rFila.Cells[9].Text))
                    {
                        VerError("No existe una forma de pago para el giro " + pNum_Giro + " en la fila " + (rFila.RowIndex + 1) + ", verifique los datos.");
                        return false;
                    }
                    else
                    {
                        //Validar la chequera
                        if (rblFormaPago.SelectedIndex == 0) // TRANS
                        {
                            if (rFila.Cells[10].Text == "&nbsp;" || string.IsNullOrWhiteSpace(rFila.Cells[10].Text))
                            {
                                VerError("El giro " + pNum_Giro + " en la fila " + (rFila.RowIndex + 1) + " no tiene una cuenta bancaria asignada, verifique los datos.");
                                return false;
                            }
                        }
                        else  //OTROS
                        {
                            if (rFila.Cells[9].Text.ToUpper().Contains("CHEQUE") == true)
                            {
                                if (rFila.Cells[10].Text == "&nbsp;" || rFila.Cells[10].Text == "")
                                {
                                    VerError("El giro " + pNum_Giro + " en la fila " + (rFila.RowIndex + 1) + " no tiene una cuenta bancaria asignada, verifique los datos.");
                                    return false;
                                }
                                Label lblCtaBancaria = (Label)rFila.FindControl("lblCtaBancaria");
                                if (lblCtaBancaria != null && !string.IsNullOrWhiteSpace(lblCtaBancaria.Text))
                                {
                                    //Consultar datos de la chequera
                                    BancosService BOChequera = new BancosService();
                                    Chequera pChequera = new Chequera();
                                    List<Chequera> lstCheque = new List<Chequera>();
                                    string pFiltro = lblCtaBancaria.Text.Trim();
                                    pChequera = BOChequera.ConsultaChequera(pFiltro, Usuario);
                                    if (pEntidad != null && pChequera != null && pChequera.idchequera != 0)
                                    {
                                        if (pChequera.num_sig_che > pChequera.cheque_fin)
                                        {
                                            VerError("No se puede generar la realización del Giro " + pNum_Giro + " en la fila " + (rFila.RowIndex + 1) + ", El siguiente cheque se encuentra fuera del rango establecido, verifique los datos.");
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (rFila.Cells[14].Text == "&nbsp;" || rFila.Cells[14].Text == "")
                    {
                        VerError("El giro " + pNum_Giro + " en la fila " + (rFila.RowIndex + 1) + " no cuenta con un valor establecido para realizarlo");
                        return false;
                    }
                }
            }
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        Site toolBar = (Site)this.Master;
        VerError("");

        // Determinar código de proceso contable para generar el comprobante
        Int64? rpta = 0;

        if (ValidarDatos())
        {
            //determinar Forma de PAgo
            if (mvPrincipal.ActiveViewIndex == 0 && rblFormaPago.SelectedIndex == 0)
            {
                //SE ACTIVA LA OPCION DE GENERAR ARCHIVO
                toolBar.MostrarExportar(false);
                toolBar.MostrarLimpiar(false);
                toolBar.MostrarConsultar(false);
                toolBar.MostrarGuardar(true);
                toolBar.MostrarCancelar(true);

                mvPrincipal.ActiveViewIndex = 1;
                txtNombreArchivo.Text = "";
                ddlEntidad.SelectedValue = ddlEntidad_giro.SelectedValue;
                ddlCuentas.SelectedValue = ddlCuenta_Giro.SelectedValue;
                lblMensj.Text = "";
                PoblarListas PoblarLista = new PoblarListas();
                PoblarLista.PoblarListaDesplegable("ACH_PLANTILLA", "", " COD_BANCO = " + ddlEntidad.SelectedValue, "1", ddlEstructura, (Usuario)Session["usuario"]);
            }
            else
            {
                rpta = ctlproceso.Inicializar(103, Convert.ToDateTime(txtFechaRealiza.Text), (Usuario)Session["Usuario"]);
                if (rpta > 1)
                {
                    // Site toolBar = (Site)Master;
                    toolBar.MostrarGuardar(false);
                    // Activar demás botones que se requieran
                    panelGeneral.Visible = false;
                    panelProceso.Visible = true;
                }
                else
                {
                    ctlMensaje.MostrarMensaje("Desea realizar los giros seleccionados?");
                }
            }



            // Determinar código de proceso contable para generar el comprobante
            // Int64? rpta = 0;
            //if (!panelProceso.Visible && panelGeneral.Visible)
            //{
            //    rpta = ctlproceso.Inicializar(103, Convert.ToDateTime(txtFechaRealiza.Text), (Usuario)Session["Usuario"]);
            //    if (rpta > 1)
            //    {
            //       // Site toolBar = (Site)Master;
            //        toolBar.MostrarGuardar(false);
            //        // Activar demás botones que se requieran
            //        panelGeneral.Visible = false;
            //        panelProceso.Visible = true;
            //    }
            //    else
            //    {
            //       // Site toolBar = (Site)this.Master;
            //        if (mvPrincipal.ActiveViewIndex == 0 && rblFormaPago.SelectedIndex == 0)
            //        {
            //            //SE ACTIVA LA OPCION DE GENERAR ARCHIVO
            //            toolBar.MostrarExportar(false);
            //            toolBar.MostrarLimpiar(false);
            //            toolBar.MostrarConsultar(false);
            //            toolBar.MostrarGuardar(true);
            //            toolBar.MostrarCancelar(true);

            //            mvPrincipal.ActiveViewIndex = 1;
            //            txtNombreArchivo.Text = "";
            //            ddlEntidad.SelectedValue = ddlEntidad_giro.SelectedValue;
            //            ddlCuentas.SelectedValue = ddlCuenta_Giro.SelectedValue;
            //            lblMensj.Text = "";
            //            PoblarListas PoblarLista = new PoblarListas();
            //            PoblarLista.PoblarListaDesplegable("ACH_PLANTILLA", "", " COD_BANCO = " + ddlEntidad.SelectedValue, "1", ddlEstructura, (Usuario)Session["usuario"]);
            //        }
            //        else
            //        {
            //            ctlMensaje.MostrarMensaje("Desea realizar los giros seleccionados?");
            //        }
            //    }
            //}
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            this.Grabar();
        }
        catch (Exception ex)
        {
            VerError("Error al grabar:" + ex.Message);
        }
    }

    protected List<Giro> ObtenerListaRealizarGiros()
    {
        List<Giro> lstGiros = new List<Giro>();
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBoxGrid cbSeleccionar = (CheckBoxGrid)rFila.FindControl("cbSeleccionar");
            if (cbSeleccionar != null)
                if (cbSeleccionar.Checked == true)
                {
                    Giro pEntidad = new Giro();
                    pEntidad.idgiro = Convert.ToInt32(rFila.Cells[1].Text);
                    if (rFila.Cells[9].Text != "&nbsp;")
                        pEntidad.nom_forma_pago = rFila.Cells[9].Text;
                    if (rFila.Cells[3].Text != "&nbsp;" && rFila.Cells[3].Text != "")
                        pEntidad.cod_persona = Convert.ToInt64(rFila.Cells[3].Text);
                    lstGiros.Add(pEntidad);
                }
        }
        return lstGiros;
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        LimpiarValoresConsulta(pBusqueda, RealizacionService.CodigoPrograma);
        txtFechaRealiza.Text = DateTime.Now.ToShortDateString();
        txtFechaAplicacion.Text = DateTime.Now.ToShortDateString();
        rblFormaPago.SelectedIndex = 0;
        rblFormaPago_SelectedIndexChanged(rblFormaPago, null);
        txtFechaGiro.Text = "";
        txtFechaAprobacion.Text = "";
        lblInfo.Visible = false;
        ddlEntidad_giro.SelectedIndex = 0;
        ddlEntidad_giro_SelectedIndexChanged(ddlEntidad_giro, null);
        gvLista.DataSource = null;
        panelGrilla.Visible = false;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarExportar(false);
        toolBar.MostrarLimpiar(true);
        toolBar.MostrarConsultar(true);
        toolBar.MostrarGuardar(false);
    }



    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0 && Session["DTGIROS"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.Columns[0].Visible = false;
            gvLista.Columns[17].Visible = false;
            gvLista.AllowPaging = false;
            gvLista.DataSource = Session["DTGIROS"];
            gvLista.DataBind();
            gvLista.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvLista);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=RealizacionGiros.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
        {
            VerError("No existen datos, genere la consulta");
        }
    }


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            KeepSelection((GridView)sender);
            CalcularTotal();

            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RealizacionService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_PageIndexChanged(object sender, EventArgs e)
    {
        RestoreSelection((GridView)sender);
        CalcularTotal();
    }



    protected void rblFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlEntidad_giro.Enabled = false;
        ddlCuenta_Giro.Enabled = false;
        if (rblFormaPago.SelectedIndex == 0) //Si esta en modo transferencia
        {
            ddlEntidad_giro.Enabled = true;
            ddlCuenta_Giro.Enabled = true;
        }
    }

    protected void btnCerrar_Click(object sender, EventArgs e)
    {
        mvPrincipal.ActiveViewIndex = 0;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarExportar(true);
        toolBar.MostrarLimpiar(true);
        toolBar.MostrarConsultar(true);
        toolBar.MostrarGuardar(true);
        toolBar.MostrarCancelar(false);
    }


    public string FuncionObtenerDatos(List<ACHcampo> lstCampos, ACHregistro vRegistro, int pIndice)
    {
        try
        {
            string entidad = string.Empty;
            int contador = 0;
            //RECORRIENDO CADA CAMPO PERTENECIENTE
            foreach (ACHcampo rFilaCamp in vRegistro.LstCampos)
            {
                if (rFilaCamp.codigo > 0)
                {
                    if (!rFilaCamp.tipo.HasValue)
                    {
                        lblMensj.Text = "El campo +" + rFilaCamp.nombre + " no tiene especificado si es de tipo constante o sentencia SQL. Verifique los campos de la estructura.";
                        return null;
                    }
                    if (string.IsNullOrWhiteSpace(rFilaCamp.valor))
                    {
                        lblMensj.Text = "El campo +" + rFilaCamp.nombre + " no tiene un valor ingresado. Verifique los campos de la estructura.";
                        return null;
                    }
                    if (!rFilaCamp.longitud.HasValue)
                    {
                        lblMensj.Text = "El campo +" + rFilaCamp.nombre + " no tiene una longitud. Verifique los campos de la estructura.";
                        return null;
                    }
                    if (!rFilaCamp.tipo_dato.HasValue)
                    {
                        lblMensj.Text = "El campo " + rFilaCamp.nombre + " no contiene un tipo de dato seleccionado. Verifique los campos de la estructura.";
                        return null;
                    }

                    switch (rFilaCamp.tipo)
                    {
                        case 1: //CONSTANTE
                            rFilaCamp.justificacion = rFilaCamp.justificacion != null ? rFilaCamp.justificacion : 2;
                            rFilaCamp.llenado = rFilaCamp.llenado != null ? rFilaCamp.llenado : " ";
                            if (rFilaCamp.tipo_dato == 1) //NUMERICO
                            {
                                //VALIDANDO DATOS A ENVIAR
                                rFilaCamp.punto = rFilaCamp.punto != null && rFilaCamp.punto != "0" ? rFilaCamp.punto : "0";
                                rFilaCamp.num_dec = rFilaCamp.num_dec != null ? rFilaCamp.num_dec : 0;
                                rFilaCamp.num_dec = rFilaCamp.num_dec.HasValue ? rFilaCamp.num_dec : 0;
                                string Separador = ""; //SEPARADOR DECIMALES
                                if (rFilaCamp.punto == "1")
                                    Separador = ".";
                                else if (rFilaCamp.punto == "2")
                                    Separador = ",";
                                else
                                    Separador = "";

                                string pValorFin = "";
                                decimal pValor = 0;

                                // Si entra acá es que es un valor constante y no un valor fijo ":valor"
                                if (!rFilaCamp.valor.Contains(":"))
                                {
                                    pValorFin = rFilaCamp.valor;
                                }
                                // Si entra acá es que es un valor fijo y busco el valor que le corresponde
                                else
                                {
                                    pValorFin = BuscarYreemplazarConsulta(rFilaCamp.valor.Replace(gSeparadorMiles, ""), pIndice, false, contador);
                                }

                                // Elimino el separador de miles
                                pValorFin = pValorFin.Replace(gSeparadorMiles, "");

                                // Valido si esta vació y asigno el 0 si es así
                                pValorFin = !string.IsNullOrWhiteSpace(pValorFin) ? pValorFin.Trim() : "0";

                                // Si tengo numero decimales entro
                                if (pValorFin.Contains(","))
                                {
                                    // Numero de decimales actuales del string
                                    string decimales = pValorFin.Substring(pValorFin.IndexOf(','));

                                    // Si tengo menos decimales que los requeridos, saco la cuenta de cuantos necesito y se los agrego
                                    if (decimales.Count() <= rFilaCamp.num_dec)
                                    {
                                        int numeroDecimalesFaltantes = rFilaCamp.num_dec.Value - decimales.Count();
                                        string decimalesFaltantes = new string('0', numeroDecimalesFaltantes);
                                        pValorFin += decimalesFaltantes;
                                    }
                                }
                                // Si no tengo decimales agrego los decimales adecuados junto con el separador definido
                                else
                                {
                                    string decimalesFaltantes = new string('0', rFilaCamp.num_dec.Value);
                                    pValorFin += Separador + decimalesFaltantes;
                                }

                                // Corto los decimales sobrantes según los que hayan pedido
                                pValor = decimal.Round(Convert.ToDecimal(pValorFin), Convert.ToInt32(rFilaCamp.num_dec));

                                // Cambio el separador decimal por default "," al separador pedido
                                pValorFin = pValor.ToString().Replace(",", Separador);

                                //LLAMAR FUNCION DE COMPLETAR
                                entidad += FuncionLlenado(pValorFin, Convert.ToInt32(rFilaCamp.longitud), rFilaCamp.llenado, Convert.ToInt32(rFilaCamp.justificacion));
                            }
                            else if (rFilaCamp.tipo_dato == 2) //CARACTER
                            {
                                string pValorFin = "";
                                pValorFin = BuscarYreemplazarConsulta(rFilaCamp.valor, pIndice, false, contador);
                                entidad += FuncionLlenado(pValorFin, Convert.ToInt32(rFilaCamp.longitud), rFilaCamp.llenado, Convert.ToInt32(rFilaCamp.justificacion));
                            }
                            else if (rFilaCamp.tipo_dato == 3) //FECHA
                            {
                                if (rFilaCamp.formato == null)
                                {
                                    lblMensj.Text = "El campo " + rFilaCamp.nombre + " no cuenta con un formato ingresado.";
                                    return null;
                                }
                                string pValorFin = "";
                                pValorFin = BuscarYreemplazarConsulta(rFilaCamp.valor, pIndice, false, contador);
                                //Convert.ToDateTime(pValorFin).ToString(rFilaCamp.formato) //ya llega formateada la fecha
                                entidad += FuncionLlenado(pValorFin, Convert.ToInt32(rFilaCamp.longitud), rFilaCamp.llenado, Convert.ToInt32(rFilaCamp.justificacion));
                            }
                            else if (rFilaCamp.tipo_dato == 4) //HORA
                            {
                                string pValorFin = "";
                                pValorFin = BuscarYreemplazarConsulta(rFilaCamp.valor, pIndice, false, contador);
                                entidad += FuncionLlenado(pValorFin, Convert.ToInt32(rFilaCamp.longitud), rFilaCamp.llenado, Convert.ToInt32(rFilaCamp.justificacion));
                            }
                            break;
                        case 2: //SENTENCIA SQL

                            rFilaCamp.justificacion = rFilaCamp.justificacion != null ? rFilaCamp.justificacion : 2;
                            rFilaCamp.llenado = rFilaCamp.llenado != null ? rFilaCamp.llenado : " ";
                            if (rFilaCamp.tipo_dato == 1) //NUMERICO
                            {
                                string rpta = "", rptaExecute = "", pError = "";
                                rpta = BuscarYreemplazarConsulta(rFilaCamp.valor.Replace(gSeparadorMiles, ""), pIndice, true, contador);

                                RealizacionService.ReemplazarConsultaSQL(rpta, ref rptaExecute, ref pError, Usuario);
                                if (pError.Trim() != "")
                                {
                                    lblMensj.Text = pError.ToString();
                                    return null;
                                }
                                //VALIDANDO DATOS A ENVIAR
                                rFilaCamp.punto = rFilaCamp.punto != null && rFilaCamp.punto != "0" ? rFilaCamp.punto : "0";
                                rFilaCamp.num_dec = rFilaCamp.num_dec != null ? rFilaCamp.num_dec : 0;
                                string Separador = "";
                                if (rFilaCamp.punto == "1")
                                    Separador = ".";
                                else if (rFilaCamp.punto == "2")
                                    Separador = ",";
                                else
                                    Separador = "";
                                decimal pValor = Convert.ToDecimal(rptaExecute);
                                pValor = decimal.Round(pValor, Convert.ToInt32(rFilaCamp.num_dec));
                                string pValorFin = pValor.ToString().Replace(".", Separador);
                                //LAMAR FUNCION DE COMPLETAR
                                entidad += FuncionLlenado(pValorFin, Convert.ToInt32(rFilaCamp.longitud), rFilaCamp.llenado, Convert.ToInt32(rFilaCamp.justificacion));
                            }
                            else if (rFilaCamp.tipo_dato == 2) //CARACTER
                            {
                                string rpta = "", rptaExecute = "", pError = "";
                                rpta = BuscarYreemplazarConsulta(rFilaCamp.valor, pIndice, true, contador);

                                RealizacionService.ReemplazarConsultaSQL(rpta, ref rptaExecute, ref pError, Usuario);
                                if (pError.Trim() != "")
                                {
                                    lblMensj.Text = pError.ToString();
                                    return null;
                                }
                                entidad += FuncionLlenado(rptaExecute, Convert.ToInt32(rFilaCamp.longitud), rFilaCamp.llenado, Convert.ToInt32(rFilaCamp.justificacion));
                            }
                            else if (rFilaCamp.tipo_dato == 3) //FECHA
                            {
                                if (rFilaCamp.formato == null)
                                {
                                    lblMensj.Text = "El campo " + rFilaCamp.nombre + " no cuenta con un formato ingresado.";
                                    return null;
                                }
                                string rpta = "", rptaExecute = "", pError = "";
                                rpta = BuscarYreemplazarConsulta(rFilaCamp.valor, pIndice, true, contador);

                                RealizacionService.ReemplazarConsultaSQL(rpta, ref rptaExecute, ref pError, Usuario);
                                if (pError.Trim() != "")
                                {
                                    lblMensj.Text = pError.ToString();
                                    return null;
                                }
                                entidad += FuncionLlenado(Convert.ToDateTime(rptaExecute).ToString(rFilaCamp.formato), Convert.ToInt32(rFilaCamp.longitud), rFilaCamp.llenado, Convert.ToInt32(rFilaCamp.justificacion));
                            }
                            else if (rFilaCamp.tipo_dato == 4) //HORA
                            {
                                string rpta = "", rptaExecute = "", pError = "";
                                rpta = BuscarYreemplazarConsulta(rFilaCamp.valor, pIndice, true, contador);

                                RealizacionService.ReemplazarConsultaSQL(rpta, ref rptaExecute, ref pError, Usuario);
                                if (pError.Trim() != "")
                                {
                                    lblMensj.Text = pError.ToString();
                                    return null;
                                }
                                entidad += FuncionLlenado(rptaExecute, Convert.ToInt32(rFilaCamp.longitud), rFilaCamp.llenado, Convert.ToInt32(rFilaCamp.justificacion));
                            }
                            break;
                    }
                }
                entidad += vRegistro.separador;
                contador += 1;
            }

            return entidad;
        }
        catch (Exception ex)
        {
            lblMensj.Text = ex.Message;
            return null;
        }
    }


    public string FuncionLlenado(string pVariable, int pLongitud, string pVarLlenado, int pAlineacion)
    {
        try
        {
            string pLlenado = string.Empty;

            if (pVariable.Length > pLongitud)
            {
                //RECORTAR
                pLlenado = pVariable.Substring(0, pLongitud);
            }
            else
            {
                //AUTOCOMPLETAR
                int longVariable = 0, LongFaltante = 0;
                longVariable = pVariable.Length;
                LongFaltante = pLongitud - longVariable;
                if (pAlineacion == 1) //Izquierda
                {
                    pLlenado = pVariable;
                    for (int i = 0; i < LongFaltante; i++)
                    {
                        pLlenado += pVarLlenado;
                    }
                }
                else //Derecha
                {
                    for (int i = 0; i < LongFaltante; i++)
                    {
                        pLlenado += pVarLlenado;
                    }
                    pLlenado += pVariable;
                }
            }

            return pLlenado;
        }
        catch (Exception ex)
        {
            lblMensj.Text = ex.Message;
            return null;
        }
    }

    public string BuscarYreemplazarConsulta(string pConsulta, int pIndice, bool pVariable, int contador)
    {
        //pVariable = true => se usara para los de sentenciaSQL se agregaran comillas a los resultados
        //pVariable = false => se usara para los de Constante se capturaran los datos de la grid
        try
        {
            pConsulta = !string.IsNullOrWhiteSpace(pConsulta) ? pConsulta.Trim() : string.Empty;
            GridViewRow row = gvLista.Rows[pIndice];

            // Cuidado la cuenta origen es quien marca el formato
            TipoBanco tipoBancoOrigen = ddlEntidad.SelectedValue.ToEnum<TipoBanco>();

            string bancoDestino = row.Cells[12].Text.Replace("&nbsp;", "");
            TipoBanco tipoBancoDestino = TipoBanco.SinBancoDefinido;

            if (!string.IsNullOrWhiteSpace(bancoDestino))
            {
                string[] arrayDatos = bancoDestino.Trim().Split('-');
                tipoBancoDestino = arrayDatos[0].ToEnum<TipoBanco>();
            }

            string pConsultaFinal = string.Empty;

            if (pConsulta.Contains(":NUM_GIRO"))
                pConsulta = pConsulta.Replace(":NUM_GIRO", row.Cells[1].Text.Replace("&nbsp;", ""));
            else if (pConsulta.Contains(":FEC_REG"))
            {
                if (pVariable == true)
                    pConsulta = pConsulta.Replace(":FEC_REG", "'" + row.Cells[2].Text.Replace("&nbsp;", "") + "'");
                else
                    pConsulta = pConsulta.Replace(":FEC_REG", row.Cells[2].Text.Replace("&nbsp;", ""));
            }
            else if (pConsulta.Contains(":COD_PERSONA"))
                pConsulta = pConsulta.Replace(":COD_PERSONA", row.Cells[3].Text.Replace("&nbsp;", ""));
            else if (pConsulta.Contains(":IDENTIFICACION"))
            {
                string identificacion = row.Cells[4].Text.Replace("&nbsp;", "").Replace("-", "");
                if (pVariable == true)
                    pConsulta = pConsulta.Replace(":IDENTIFICACION", "'" + identificacion + "'");
                else
                    pConsulta = pConsulta.Replace(":IDENTIFICACION", identificacion);
            }
            else if (pConsulta.Contains(":NOMBRE"))
            {


                if (pVariable == true)
                    pConsulta = pConsulta.Replace(":NOMBRE", "'" + row.Cells[5].Text.Replace("&nbsp;", "") + "'");
                else
                    pConsulta = pConsulta.Replace(":NOMBRE", row.Cells[5].Text.Replace("&nbsp;", ""));
            }
            else if (pConsulta.Contains(":APELLIDOS"))
            {
                string cod_persona = row.Cells[3].Text.Replace(espacioBlancoHTML, "");
                HomologacionServices homologacionServices = new HomologacionServices();
                Xpinn.Comun.Entities.Persona entidad = new Xpinn.Comun.Entities.Persona();
                entidad = homologacionServices.PersonaDetalle(cod_persona, Usuario);

                if (pVariable == true)
                    pConsulta = pConsulta.Replace(":APELLIDOS", "'" + entidad.apellidos.Replace("&nbsp;", "") + "'");
                else
                    pConsulta = pConsulta.Replace(":APELLIDOS", entidad.apellidos.Replace("&nbsp;", ""));
            }
            else if (pConsulta.Contains(":NOMBRES"))
            {
                string cod_persona = row.Cells[3].Text.Replace(espacioBlancoHTML, "");
                HomologacionServices homologacionServices = new HomologacionServices();
                Xpinn.Comun.Entities.Persona entidad = new Xpinn.Comun.Entities.Persona();
                entidad = homologacionServices.PersonaDetalle(cod_persona, Usuario);

                if (pVariable == true)
                    pConsulta = pConsulta.Replace(":NOMBRES", "'" + entidad.nombres.Replace("&nbsp;", "") + "'");
                else
                    pConsulta = pConsulta.Replace(":NOMBRES", entidad.nombres.Replace("&nbsp;", ""));
            }

            else if (pConsulta.Contains(":COD_OPE"))
                pConsulta = pConsulta.Replace(":COD_OPE", row.Cells[6].Text.Replace("&nbsp;", ""));
            else if (pConsulta.Contains(":NUM_COMP"))
                pConsulta = pConsulta.Replace(":NUM_COMP", row.Cells[7].Text.Replace("&nbsp;", ""));
            else if (pConsulta.Contains(":TIPO_COMP"))
            {
                string tipo_comp = row.Cells[8].Text.Replace("&nbsp;", "");
                string[] pDato = tipo_comp.Trim().Split('-');

                pConsulta = pConsulta.Replace(":TIPO_COMP", pDato[0]);
            }
            else if (pConsulta.Contains(":FORMA_PAGO"))
            {
                string valor = string.Empty;
                string[] pDato;

                switch (tipoBancoOrigen)
                {
                    case TipoBanco.BancoBogota:
                        valor = "A";
                        break;
                    case TipoBanco.BancoOccidente:
                        // Si el banco al que voy a transferir es el mismo occidente es "2", si es cualquier otro banco es "3"
                        if (tipoBancoDestino == TipoBanco.BancoOccidente)
                            valor = "2";
                        else
                            valor = "3";
                        break;
                    default:
                        string forma_pago = row.Cells[9].Text.Replace("&nbsp;", "");
                        pDato = forma_pago.Trim().Split('-');
                        valor = pDato[0];
                        break;
                }

                pConsulta = pConsulta.Replace(":FORMA_PAGO", valor);
            }
            else if (pConsulta.Contains(":BANCO_GIRO"))
            {
                string banco_giro = ddlEntidad.SelectedValue;
                pConsulta = pConsulta.Replace(":BANCO_GIRO", banco_giro);
            }
            else if (pConsulta.Contains(":CTA_BANCO_GIRO"))
            {
                Label label = row.FindControl("lblCtaBancaria") as Label;
                if (label == null) return string.Empty;

                if (pVariable == true)
                    pConsulta = pConsulta.Replace(":CTA_BANCO_GIRO", "'" + label.Text.Replace("&nbsp;", "").Replace("-", "") + "'");
                else
                    pConsulta = pConsulta.Replace(":CTA_BANCO_GIRO", label.Text.Replace("&nbsp;", "").Replace("-", ""));

                switch (tipoBancoOrigen)
                {
                    // (en cta cte o ah anteponga dos ceros) - Dicho por el formato
                    case TipoBanco.BancoBogota:
                        pConsulta = pConsulta.Insert(0, "00");
                        break;
                }
            }
            else if (pConsulta.Contains(":COD_OFICINA_CTA_ORIGEN"))
            {
                Label label = row.FindControl("lblCtaBancaria") as Label;
                if (label == null) return string.Empty;

                pConsulta = pConsulta.Replace(":COD_OFICINA_CTA_ORIGEN", label.Text.Replace("&nbsp;", "").Replace("-", "").Trim().Substring(0, 3));
            }
            else if (pConsulta.Contains(":BANCO_DESTINO"))
            {
                string banco_destino = row.Cells[12].Text.Replace("&nbsp;", "");
                string[] pDato = banco_destino.Trim().Split('-');
                pConsulta = pConsulta.Replace(":BANCO_DESTINO", pDato[0]);
            }
            else if (pConsulta.Contains(":CTA_BANCO_DESTINO"))
            {
                if (pVariable == true)
                    pConsulta = pConsulta.Replace(":CTA_BANCO_DESTINO", "'" + row.Cells[13].Text.Replace("&nbsp;", "").Replace("-", "") + "'");
                else
                    pConsulta = pConsulta.Replace(":CTA_BANCO_DESTINO", row.Cells[13].Text.Replace("&nbsp;", "").Replace("-", ""));
            }
            else if (pConsulta.Contains(":VALOR_GIRO"))
                pConsulta = pConsulta.Replace(":VALOR_GIRO", row.Cells[14].Text.Replace(espacioBlancoHTML, "").Replace(".", ""));
            else if (pConsulta.Contains(":ESTADO"))
                pConsulta = pConsulta.Replace(":ESTADO", row.Cells[16].Text.Replace("&nbsp;", ""));
            else if (pConsulta.Contains(":FECHA_APLICACION"))
            {
                HomologacionServices homologacionServices = new HomologacionServices();
                string formatoFecha = homologacionServices.ValorHomologacionFechaBancos(tipoBancoOrigen);

                pConsulta = pConsulta.Replace(":FECHA_APLICACION", txtFechaAplicacion.ToDateTime.ToString(formatoFecha));
            }
            else if (pConsulta.Contains(":FECHA_TRANSACCION"))
            {
                HomologacionServices homologacionServices = new HomologacionServices();
                string formatoFecha = homologacionServices.ValorHomologacionFechaBancos(tipoBancoOrigen);

                pConsulta = pConsulta.Replace(":FECHA_TRANSACCION", txtFechaRealiza.ToDateTime.ToString(formatoFecha));
            }
            else if (pConsulta.Contains(":TIPO_CUENTA_ORIGEN"))
            {
                BancosService bancoService = new BancosService();
                int? tipoCuentaNumero = bancoService.ConsultarTipoCuenta(ddlCuenta_Giro.SelectedItem.Text, Usuario);
                TipoCuentaBanco tipoCuenta = tipoCuentaNumero.HasValue ? tipoCuentaNumero.ToEnum<TipoCuentaBanco>() : default(TipoCuentaBanco);

                HomologacionServices homologacionServices = new HomologacionServices();
                pConsulta = homologacionServices.ValorHomologacionTipoCuentaBanco(tipoBancoOrigen, tipoCuenta);
            }
            else if (pConsulta.Contains(":TIPO_CUENTA_DESTINO"))
            {
                string tipo_cuenta_string = row.Cells[21].Text.Replace("&nbsp;", "").Trim();
                TipoCuentaBanco tipoCuenta = tipo_cuenta_string.ToEnum<TipoCuentaBanco>();

                HomologacionServices homologacionServices = new HomologacionServices();
                pConsulta = homologacionServices.ValorHomologacionTipoCuentaBanco(tipoBancoOrigen, tipoCuenta);
            }
            else if (pConsulta.Contains(":NUM_REGISTROS"))
            {
                int numeroRegistros = gvLista.Rows
                                    .OfType<GridViewRow>()
                                    .Where(x => ((CheckBoxGrid)x.FindControl("cbSeleccionar")).Checked)
                                    .Count();

                pConsulta = pConsulta.Replace(":NUM_REGISTROS", numeroRegistros.ToString());
            }
            else if (pConsulta.Contains(":SUM_CREDITOS"))
            {
                decimal sumaValor = gvLista.Rows
                                    .OfType<GridViewRow>()
                                    .Where(x => ((CheckBoxGrid)x.FindControl("cbSeleccionar")).Checked)
                                    .Sum(x =>
                                    {
                                        string valor = x.Cells[14].Text;
                                        return Convert.ToDecimal(!string.IsNullOrWhiteSpace(valor) ? valor : "0");
                                    });

                pConsulta = pConsulta.Replace(":SUM_CREDITOS", sumaValor.ToString());
            }
            else if (pConsulta.Contains(":TIPO_TRANSACCION"))
            {
                string tipo_cuenta_string = row.Cells[21].Text.Replace("&nbsp;", "").Trim();
                TipoCuentaBanco tipoCuenta = tipo_cuenta_string.ToEnum<TipoCuentaBanco>();
                string valor = string.Empty;

                switch (tipoBancoOrigen)
                {
                    case TipoBanco.Bancolombia:
                        if (tipoCuenta == TipoCuentaBanco.Ahorro)
                            valor = pConsulta.Replace(":TIPO_TRANSACCION", "32");
                        else if (tipoCuenta == TipoCuentaBanco.Corriente)
                            valor = pConsulta.Replace(":TIPO_TRANSACCION", "22");
                        break;
                    case TipoBanco.BancoCoopcentral:
                        if (tipoCuenta == TipoCuentaBanco.Ahorro)
                            valor = pConsulta.Replace(":TIPO_TRANSACCION", "32");
                        else if (tipoCuenta == TipoCuentaBanco.Corriente)
                            valor = pConsulta.Replace(":TIPO_TRANSACCION", "22");
                        break;
                }

                pConsulta = valor;
            }
            else if (pConsulta.Contains(":CONCEPTO"))
            {
                string cod_persona = row.Cells[3].Text.Replace(espacioBlancoHTML, "");

                HomologacionServices homologacionServices = new HomologacionServices();
                Homologacion homologacion = homologacionServices.ConsultarHomologacionTipoIdentificacionPorCodigoPersona(cod_persona, Usuario);

                string formato = string.Format("0000000{0}0", homologacion.tipo_identificacion_bancolombia);

                pConsulta = pConsulta.Replace(":CONCEPTO", formato);
            }
            else if (pConsulta.Contains(":RELLENO"))
            {
                pConsulta = pConsulta.Replace(":RELLENO", " ");
            }
            else if (pConsulta.Contains(":TIPO_IDENTIFICACION_DESTINO"))
            {
                string cod_persona = row.Cells[3].Text.Replace(espacioBlancoHTML, "");

                HomologacionServices homologacionServices = new HomologacionServices();
                Homologacion homologacion = homologacionServices.ConsultarHomologacionTipoIdentificacionPorCodigoPersona(cod_persona, Usuario);
                string valor = string.Empty;

                switch (tipoBancoOrigen)
                {
                    case TipoBanco.BancoBogota:
                        valor = homologacion.tipo_identificacion_banbogota;
                        break;
                    case TipoBanco.Bancolombia:
                        valor = homologacion.tipo_identificacion_bancolombia.HasValue ? homologacion.tipo_identificacion_bancolombia.ToString() : string.Empty;
                        break;
                    case TipoBanco.BancoPopular:
                        valor = homologacion.tipo_identificacion_banpopular.HasValue ? homologacion.tipo_identificacion_banpopular.ToString() : string.Empty;
                        break;
                }

                pConsulta = pConsulta.Replace(":TIPO_IDENTIFICACION_DESTINO", valor);
            }
            else if (pConsulta.Contains(":TIPO_IDENTIFICACION_ORIGEN"))
            {
                string valor = string.Empty;
                // Siempre es NIT - Ver tabla homologa_tipos_iden para consultar homologaciones del tipo de identificacion
                switch (tipoBancoOrigen)
                {
                    case TipoBanco.BancoBogota:
                        valor = "N";
                        break;
                    case TipoBanco.Bancolombia:
                        valor = "3";
                        break;
                    case TipoBanco.BancoPopular:
                        valor = "3";
                        break;
                    case TipoBanco.BancoAgrario:
                        valor = "2";
                        break;
                }

                pConsulta = pConsulta.Replace(":TIPO_IDENTIFICACION_ORIGEN", valor);
            }
            else if (pConsulta.Contains(":CODIGO_COMPENSACION_BANCO_DESTINO"))
            {
                string valor = ((int)tipoBancoDestino).ToString();
                pConsulta = pConsulta.Replace(":CODIGO_COMPENSACION_BANCO_DESTINO", valor);
            }
            else if (pConsulta.Contains(":CONTADOR"))
            {
                pConsulta = pConsulta.Replace(":CONTADOR", contador.ToString());
            }
            else if (pConsulta.Contains(":DIG_VERIFICACION"))
            {
                string cod_persona = row.Cells[3].Text.Replace(espacioBlancoHTML, "");
                HomologacionServices homologacionServices = new HomologacionServices();
                Xpinn.Comun.Entities.Persona entidad = new Xpinn.Comun.Entities.Persona();
                entidad = homologacionServices.PersonaDetalle(cod_persona, Usuario);
                if (entidad.digito_verificacion == null || entidad.digito_verificacion == "") { entidad.digito_verificacion = "0"; }
                pConsulta = pConsulta.Replace(":DIG_VERIFICACION", entidad.digito_verificacion);
            }
            else if (pConsulta.Contains(":DIRECCION"))
            {
                string cod_persona = row.Cells[3].Text.Replace(espacioBlancoHTML, "");
                HomologacionServices homologacionServices = new HomologacionServices();
                Xpinn.Comun.Entities.Persona entidad = new Xpinn.Comun.Entities.Persona();
                entidad = homologacionServices.PersonaDetalle(cod_persona, Usuario);
                pConsulta = pConsulta.Replace(":DIRECCION", entidad.direccion);
            }
            else if (pConsulta.Contains(":TELEFONO"))
            {
                string cod_persona = row.Cells[3].Text.Replace(espacioBlancoHTML, "");
                HomologacionServices homologacionServices = new HomologacionServices();
                Xpinn.Comun.Entities.Persona entidad = new Xpinn.Comun.Entities.Persona();
                entidad = homologacionServices.PersonaDetalle(cod_persona, Usuario);
                pConsulta = pConsulta.Replace(":TELEFONO", entidad.telefono);
            }
            else if (pConsulta.Contains(":CONVENIO"))
            {
                pConsulta = "";
            }


            pConsultaFinal = pConsulta;
            return pConsultaFinal;
        }
        catch (Exception ex)
        {
            lblMensj.Text = ex.Message;
            return null;
        }
    }


    protected void btnAceptarEstructura_Click(object sender, EventArgs e)
    {
        //VALIDAR DATOS NULOS
        lblMensj.Text = "";
        if (ddlEstructura.SelectedIndex == 0)
        {
            lblMensj.Text = "Seleccione una Estructuras, verifique los datos.";
            ddlEstructura.Focus();
            return;
        }
        if (txtNombreArchivo.Text == "")
        {
            lblMensj.Text = "Ingrese el nombre del Archivo a descargar";
            txtNombreArchivo.Focus();
            return;
        }

        //CONSULTAR DATOS DE LA PLANTILLA
        ACHplantillaService ACHplantillaServicio = new ACHplantillaService();
        ACHplantilla vPlantilla = ACHplantillaServicio.ConsultarACHplantilla(Convert.ToInt64(ddlEstructura.SelectedValue), Usuario);

        if (vPlantilla.codigo == 0)
        {
            lblMensj.Text = "La estructura selecciona no existe";
            return;
        }
        else
        {
            if (vPlantilla.activo == null)
            {
                lblMensj.Text = "La plantilla seleccionada no esta activa. Verifique los datos";
                return;
            }
        }
        if (vPlantilla.LstRegistros == null || vPlantilla.LstRegistros.Count == 0)
        {
            lblMensj.Text = "La plantilla seleccionada no cuenta con registros enlazados para poder generar el archivo.";
            return;
        }
        //CARGAR EL DETALLE DE LA ESTRUCTURA
        List<ACHregistro> lstDetalle = vPlantilla.LstRegistros;

        //LISTA PARA ACUMULAR FILAS DEL ARCHIVO A DESCARGAR
        List<string> lstConsulta = new List<string>();

        //RECORRER LOS REGISTROS DE LA PLANTILLA SELECCIONADA
        foreach (ACHregistro rFilaReg in lstDetalle)
        {
            if (rFilaReg.codigo > 0)
            {
                long pCod_Registro = rFilaReg.codigo;
                ACHregistroService ACHregistroServicio = new ACHregistroService();

                //CARGANDO EL ENCABEZADO Y EL DETALLE DE LA ESTRUCTURA
                ACHregistro vRegistro = ACHregistroServicio.ConsultarACHregistro(pCod_Registro, Usuario);

                if (vRegistro.codigo > 0) // si existe registro
                {
                    if (vRegistro.tipo == null)
                    {
                        lblMensj.Text = "La Plantilla " + rFilaReg.nombre + " perteneciente a la estructura " + ddlEstructura.SelectedItem.Text
                            + " no contiene un  tipo especificado. Verifique los datos de la estructura.";
                        return;
                    }
                    else
                    {
                        if (vRegistro.LstCampos.Count == 0)//Si no existen campos asignados al registro
                        {
                            lblMensj.Text = "El registro " + rFilaReg.nombre + " no cuenta con campos parametrizados. verifique los datos.";
                            return;
                        }
                        switch (vRegistro.tipo)
                        {
                            case 1: //Encabezado
                                string pResultEnc = "";
                                pResultEnc = FuncionObtenerDatos(vRegistro.LstCampos, vRegistro, 0);
                                if (pResultEnc != "")
                                    lstConsulta.Add(pResultEnc);
                                break;
                            case 2: //Detalle Transacción

                                foreach (GridViewRow rFilaItem in gvLista.Rows) //RECORRIENDO LA GRIDVIEW DE GIROS SELECCIONADOS
                                {
                                    CheckBoxGrid cbSeleccionar = (CheckBoxGrid)rFilaItem.FindControl("cbSeleccionar");
                                    int rowIndex = rFilaItem.RowIndex;
                                    if (cbSeleccionar != null)
                                    {
                                        if (cbSeleccionar.Checked)
                                        {
                                            //TRAER FUNCION DE CONSULTA
                                            string pResult = "";
                                            pResult = FuncionObtenerDatos(vRegistro.LstCampos, vRegistro, rFilaItem.RowIndex);
                                            if (!string.IsNullOrWhiteSpace(pResult))
                                            {
                                                lstConsulta.Add(pResult);
                                            }
                                            else
                                            {
                                                VerError("Ocurrio un error al procesar la estructura del archivo!.");
                                                return;
                                            }
                                        }
                                    }
                                }
                                break;
                            case 3: //Detalle Prenotificación
                                string pResultDetPre = "";
                                pResultDetPre = FuncionObtenerDatos(vRegistro.LstCampos, vRegistro, 0);
                                if (pResultDetPre != "")
                                    lstConsulta.Add(pResultDetPre);
                                break;

                            case 4: //Addenda
                                string pResultAddenda = "";
                                pResultAddenda = FuncionObtenerDatos(vRegistro.LstCampos, vRegistro, 0);
                                if (pResultAddenda != "")
                                    lstConsulta.Add(pResultAddenda);
                                break;

                            case 5: //Fin Archivo
                                string pResultFin = "";
                                pResultFin = FuncionObtenerDatos(vRegistro.LstCampos, vRegistro, 0);
                                if (pResultFin != "")
                                    lstConsulta.Add(pResultFin);
                                break;

                            case 6: //Control 
                                string pResultCtrl = "";
                                pResultCtrl = FuncionObtenerDatos(vRegistro.LstCampos, vRegistro, 0);
                                if (pResultCtrl != "")
                                    lstConsulta.Add(pResultCtrl);
                                break;
                        }
                    }


                }
            }
        }

        // CARGAR LOS DATOS
        if (Session["DTGIROS"] == null)
        {
            lblMensj.Text = "No se han cargado datos de los Giros a generar";
            return;
        }

        //GENERAR EL ARCHIVO PLANO
        string texto = "", fic = "";
        if (txtNombreArchivo.Text != "")
            if (txtNombreArchivo.Text.ToLower().Contains(".xls") == true)
                fic = txtNombreArchivo.Text;
            else
                fic = txtNombreArchivo.Text + ".xls";
        if (File.Exists(Server.MapPath("Archivos\\") + fic))
            File.Delete(Server.MapPath("Archivos\\") + fic);
        try
        {
            //Guarda los Datos a la ruta especificada
            foreach (string item in lstConsulta)
            {
                texto = item;
                System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                sw.WriteLine(texto);
                sw.Close();
            }
        }
        catch (Exception ex)
        {
            lblMensj.Text = ex.Message;
            return;
        }

        if (lstConsulta.Count > 0 && File.Exists(Server.MapPath("Archivos\\") + fic))
        {
            btnAceptarEstructura.Enabled = false;
            lblMensj.Text = "Archivo generado correctamente";

            System.IO.StreamReader sr;
            sr = File.OpenText(Server.MapPath("Archivos\\") + fic);
            texto = sr.ReadToEnd();
            sr.Close();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "text/plain";
            HttpContext.Current.Response.Write(texto);
            HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + fic);

            File.Delete(Server.MapPath("Archivos\\") + fic);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        else
        {
            lblMensj.Text = "No se genero el archivo, Verifique los Datos";
        }
    }


    private void Grabar()
    {
        // Validar los datos
        VerError("");
        if (txtFechaRealiza.Text == "")
        {
            VerError("Ingrese la fecha de Realización");
            return;
        }

        // Determinar si se genera un solo comprobante para las transferencias
        bool bTrasnferenciaMasiva = false;
        string respuesta = "";
        string error = "";
        RealizacionService.ReemplazarConsultaSQL("Select valor As Respuesta From general Where codigo = 12", ref respuesta, ref error, (Usuario)Session["Usuario"]);
        if (error.Trim() == "")
            if (respuesta.Trim() != "")
                if (respuesta.Trim() == "1")
                    bTrasnferenciaMasiva = true;

        // Realizar los giros
        Usuario pUsuario = (Usuario)Session["usuario"];
        Giro pEntidad = new Giro();
        pEntidad.lstGiro = new List<Giro>();
        pEntidad.lstGiro = ObtenerListaRealizarGiros();

        //Consultando la persona del comprobante a generar
        Int64 codPersona = 0, cont = 0, pIdGiro = 0;
        bool rpta = true;
        foreach (Giro items in pEntidad.lstGiro)
        {
            if (cont == 0)
            {
                codPersona = Convert.ToInt64(items.cod_persona);
                pIdGiro = items.idgiro;
            }
            if (codPersona != items.cod_persona)
                rpta = false;

            codPersona = Convert.ToInt64(items.cod_persona);
            cont++;
        }

        Boolean rptaArchivo = false;
        String NombreArch = "";
        if (mvPrincipal.ActiveViewIndex == 1)
        {
            rptaArchivo = true;
            NombreArch = txtNombreArchivo.Text.Trim() != "" ? txtNombreArchivo.Text.Trim() : null;
        }

        // Validar que existe parametrización contable del proceso
        List<Operacion> lstOperaciones = new List<Operacion>();
        Xpinn.Contabilidad.Services.ProcesoContableService procesoContable = new Xpinn.Contabilidad.Services.ProcesoContableService();
        Xpinn.Contabilidad.Entities.ProcesoContable eproceso = new Xpinn.Contabilidad.Entities.ProcesoContable();
        string sError = "";
        if (rblFormaPago.SelectedIndex == 0)
        {
            eproceso = procesoContable.ConsultarProcesoContableOperacion(103, pUsuario);
            if (eproceso == null)
            {
                VerError("No hay ningún proceso contable parametrizado para la realización de giros");
                return;
            }
            if (eproceso.cod_proceso < 0)
            {
                VerError("No hay ningún proceso contable parametrizado para la realización de giros");
                return;
            }
            if (ctlproceso.cod_proceso != null)
                if (ctlproceso.cod_proceso != 0)
                    eproceso.cod_proceso = Convert.ToInt32(ctlproceso.cod_proceso);
            lstOperaciones = RealizacionService.RealizarGiro(bTrasnferenciaMasiva, pEntidad, Convert.ToDateTime(txtFechaRealiza.Text), eproceso.cod_proceso, rptaArchivo, NombreArch, ref sError, (Usuario)Session["usuario"]);

            if (lstOperaciones == null)
            {
                VerError("Error: no se pudo realizar el giro");
                return;
            }
            if (lstOperaciones.Count <= 0)
            {
                VerError("Error: no se generaron operaciones para el giro");
                return;
            }
            if (sError != "")
            {
                VerError("Error: " + sError);
                return;
            }

            // Generar el comprobante
            if (bTrasnferenciaMasiva == true)
            {
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = null;
                Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = null;
                if (lstOperaciones.Count > 0)
                {
                    gvOperacion.DataSource = lstOperaciones;
                    gvOperacion.DataBind();
                    Site toolBar = (Site)this.Master;
                    mvPrincipal.ActiveViewIndex = 2;
                    toolBar.MostrarExportar(false);
                    toolBar.MostrarLimpiar(false);
                    toolBar.MostrarConsultar(false);
                    toolBar.MostrarGuardar(false);
                    toolBar.MostrarCancelar(true);
                    toolBar.MostrarImprimir(true);
                }
            }
            else
            {
                if (lstOperaciones[0].cod_ope > 0)
                {
                    Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                    Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = lstOperaciones[0].cod_ope;
                    Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 103;
                    Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = rpta == false ? null : codPersona.ToString().Trim();
                    Session[ComprobanteServicio.CodigoPrograma + ".idgiro"] = cont == 1 ? pIdGiro.ToString() : null;
                    Session[ComprobanteServicio.CodigoPrograma + ".realizoGiro"] = true;
                    Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                }
            }
        }
        else //CHEQUE O EFECTIVO
        {
            // Determinar el proceso del desembolso            
            eproceso = procesoContable.ConsultarProcesoContableOperacion(103, pUsuario);
            if (eproceso == null)
            {
                VerError("No hay ningún proceso contable parametrizado para la realización de giros");
                return;
            }
            if (eproceso.cod_proceso < 0)
            {
                VerError("No hay ningún proceso contable parametrizado para la realización de giros");
                return;
            }
            if (ctlproceso.cod_proceso != null)
                if (ctlproceso.cod_proceso != 0)
                    eproceso.cod_proceso = Convert.ToInt32(ctlproceso.cod_proceso);
            lstOperaciones = RealizacionService.RealizarGiroOtros(pEntidad, Convert.ToDateTime(txtFechaRealiza.Text), eproceso.cod_proceso, ref sError, (Usuario)Session["usuario"]);
            if (sError != "")
            {
                VerError(sError);
                return;
            }
            if (lstOperaciones.Count > 0)
            {
                gvOperacion.DataSource = lstOperaciones;
                gvOperacion.DataBind();
                Site toolBar = (Site)this.Master;
                mvPrincipal.ActiveViewIndex = 2;
                toolBar.MostrarExportar(false);
                toolBar.MostrarLimpiar(false);
                toolBar.MostrarConsultar(false);
                toolBar.MostrarGuardar(false);
                toolBar.MostrarCancelar(true);
                toolBar.MostrarImprimir(true);
            }

        }
    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        try
        {
            if (gvOperacion.Rows.Count == 0)
            {
                VerError("No existen datos para imprimir, Verifique los datos.");
                return;
            }
            else
            {
                int cont = 0;
                foreach (GridViewRow rFila in gvOperacion.Rows)
                {
                    CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
                    if (cbSeleccionar != null)
                        if (cbSeleccionar.Checked)
                            cont++;
                }
                if (cont == 0)
                {
                    VerError("No existen datos seleccionados para imprimir, Verifique los datos.");
                    return;
                }
                List<Xpinn.Contabilidad.Entities.Comprobante> lstCOmpro = new List<Xpinn.Contabilidad.Entities.Comprobante>();
                lstCOmpro = ObtenerDetalleComprobantes();
                if (lstCOmpro.Count > 0)
                {
                    if (rblFormaPago.SelectedIndex == 0) //TRANSFERENCIA
                        Session[ComprobanteServicio.CodigoPrograma + ".tipo"] = "1";
                    Session["DATA_RGIROS"] = lstCOmpro;
                    Navegar("../../Tesoreria/ImpresionMasiva/Lista.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected List<Xpinn.Contabilidad.Entities.Comprobante> ObtenerDetalleComprobantes()
    {
        List<Xpinn.Contabilidad.Entities.Comprobante> lstCompro = new List<Xpinn.Contabilidad.Entities.Comprobante>();
        foreach (GridViewRow rFila in gvOperacion.Rows)
        {
            Xpinn.Contabilidad.Entities.Comprobante pEntidad = new Xpinn.Contabilidad.Entities.Comprobante();
            CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
            if (cbSeleccionar != null)
                if (cbSeleccionar.Checked)
                {
                    pEntidad.cod_ope = Convert.ToInt64(rFila.Cells[2].Text.Trim());
                    pEntidad.num_comp = Convert.ToInt64(gvOperacion.DataKeys[rFila.RowIndex].Value.ToString().Trim());
                    pEntidad.tipo_comp = Convert.ToInt64(rFila.Cells[4].Text.Trim());
                    lstCompro.Add(pEntidad);
                }
        }
        return lstCompro;
    }

    protected void cbSeleccionarEnc_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbSeleccionarEnc = (CheckBox)sender;
        if (cbSeleccionarEnc != null)
        {
            foreach (GridViewRow rFila in gvOperacion.Rows)
            {
                CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
                cbSeleccionar.Checked = cbSeleccionarEnc.Checked;
            }
        }
        CalcularTotal();
    }

    protected void btnInfo_Click(object sender, EventArgs e)
    {
        ButtonGrid btnInfo = (ButtonGrid)sender;
        if (btnInfo != null)
        {
            int rowIndex = Convert.ToInt32(btnInfo.CommandArgument);
            Label lblCtaBancaria = (Label)gvLista.Rows[rowIndex].FindControl("lblCtaBancaria");
            if (lblCtaBancaria != null && lblCtaBancaria.Text != "")
            {
                gvListaInfo.DataSource = null;
                lblTotalRegs1.Visible = false;
                //Consultar datos de la chequera
                Xpinn.Caja.Services.BancosService BOChequera = new Xpinn.Caja.Services.BancosService();
                Chequera pEntidad = new Chequera();
                List<Chequera> lstCheque = new List<Chequera>();
                string pFiltro = lblCtaBancaria.Text.Trim();
                pEntidad = BOChequera.ConsultaChequera(pFiltro, (Usuario)Session["usuario"]);
                if (pEntidad != null)
                {
                    if (pEntidad.idchequera != 0)
                    {
                        Chequera pData = new Chequera();
                        pData.idchequera = pEntidad.idchequera;
                        pData.cheque_ini = pEntidad.cheque_ini;
                        pData.cheque_fin = pEntidad.cheque_fin;
                        pData.num_sig_che = pEntidad.num_sig_che;
                        lstCheque.Add(pData);
                        gvListaInfo.DataSource = lstCheque;
                        gvListaInfo.DataBind();
                        lblTotalRegs1.Text = "Registros encontrados " + lstCheque.Count;
                        lblTotalRegs1.Visible = true;
                        mpeChequera.Show();
                    }
                }
                else
                {
                    lblTotalRegs1.Visible = true;
                    lblTotalRegs1.Text = "No se obtuvieron registros para esta cuenta bancaria.";
                }
            }
        }
    }


    protected void btnSalir_Click(object sender, EventArgs e)
    {
        mpeChequera.Hide();
    }


    protected void gvOperacion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Imprimir")
        {
            List<Xpinn.Contabilidad.Entities.Comprobante> lstCOmpro = new List<Xpinn.Contabilidad.Entities.Comprobante>();

            int rowIndex = Convert.ToInt32(e.CommandArgument); //RECUPERA INDICE
            Xpinn.Contabilidad.Entities.Comprobante pEntidad = new Xpinn.Contabilidad.Entities.Comprobante();
            pEntidad.cod_ope = Convert.ToInt64(gvOperacion.Rows[rowIndex].Cells[2].Text.Trim());
            pEntidad.num_comp = Convert.ToInt64(gvOperacion.DataKeys[rowIndex].Value.ToString().Trim());
            pEntidad.tipo_comp = Convert.ToInt64(gvOperacion.Rows[rowIndex].Cells[4].Text.Trim());
            lstCOmpro.Add(pEntidad);
            if (lstCOmpro.Count > 0)
            {

                Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
                Xpinn.Comun.Data.GeneralData ConsultaData1 = new Xpinn.Comun.Data.GeneralData();
                pData = ConsultaData1.ConsultarGeneral(90187, (Usuario)Session["usuario"]);

                if (pData.valor == "1")
                {
                    Xpinn.Contabilidad.Services.ComprobanteService TipoCompServicio = new Xpinn.Contabilidad.Services.ComprobanteService();

                    Session[TipoCompServicio.CodigoPrograma + ".num_comp"] = gvOperacion.DataKeys[rowIndex].Value.ToString().Trim();

                    String id = gvOperacion.Rows[rowIndex].Cells[4].Text.Trim();
                    Session[TipoCompServicio.CodigoPrograma + ".tipo_comp"] = id;
                    Session[TipoCompServicio.CodigoPrograma + ".detalle"] = id;



                    Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                }
                else
                {


                    if (rblFormaPago.SelectedIndex == 0) //TRANSFERENCIA
                        Session[ComprobanteServicio.CodigoPrograma + ".tipo"] = "1";
                    Session["DATA_RGIROS"] = lstCOmpro;
                    Navegar("../../Tesoreria/ImpresionMasiva/Lista.aspx");

                }

            }
        }
    }

    protected void btnCancelarProceso_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarCancelar(true);
        toolBar.MostrarConsultar(true);
        panelGeneral.Visible = true;
        panelProceso.Visible = false;
    }

    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            panelGeneral.Visible = true;
            panelProceso.Visible = false;
            // Aquí va la función que hace lo que se requiera grabar en la funcionalidad
            Grabar();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void InicializarVariablesSession()
    {
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".num_comp"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".tipo_comp"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".cod_ope"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".tipo_ope"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".fecha_ope"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".idgiro"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".esDesembolsoCredito"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".numeroRadicacion"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".realizoGiro"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".cod_ope"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".radicadoCruceCuentas"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".cod_traslado"); } catch { }

        try { Session.Remove("DetalleComprobante"); } catch { }
        try { Session.Remove("Modificar"); } catch { }
        try { Session.Remove("Nuevo"); } catch { }
        try { Session.Remove("Carga"); } catch { }
        try { Session.Remove("idNComp"); } catch { }
        try { Session.Remove("idTComp"); } catch { }
        try { Session.Remove("cod_ope"); } catch { }
        try { Session.Remove("Ruta_Cheque"); } catch { }
        try { Session.Remove("numerocheque"); } catch { }
        try { Session.Remove("entidad"); } catch { }
        try { Session.Remove("cuenta"); } catch { }
        try { Session.Remove("NumCred_Orden"); } catch { }
        try { Session.Remove("NUM_AUXILIO"); } catch { }
        try { Session.Remove("Modificar"); } catch { }
        try { Session.Remove("Comprobantecopia"); } catch { }
        try { Session.Remove("Comprobanteanulacion"); } catch { }
        try { Session.Remove("Comprobantecarga"); } catch { }
        try { Session.Remove("Carga"); } catch { }
        try { Session.Remove("Nuevo"); } catch { }
        try { Session.Remove("CENTROSCOSTO"); } catch { }

        try { Session.Remove(Usuario.codusuario + "codOpe"); } catch { }
        try { Session.Remove(Usuario.codusuario + "cod_persona"); } catch { }

    }




}