﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Configuration;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using Microsoft.CSharp;
using System.IO;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;


public partial class Detalle : GlobalWeb
{
    Xpinn.Cartera.Services.ReestructuracionService ReestructuraService = new Xpinn.Cartera.Services.ReestructuracionService();
    Xpinn.Asesores.Entities.Producto producto;
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ReestructuraService.CodigoPrograma, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            txtFechaRes.eventoCambiar += txtFechaIngreso_TextChanged;
            ddlempresarecaudo.Visible = false;
            lblpagaduria.Visible = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReestructuraService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            producto = (Xpinn.Asesores.Entities.Producto)(Session[MOV_GRAL_CRED_PRODUC]);
            _usuario = (Usuario)Session["usuario"];

            if (!IsPostBack)
            {
                mvReestructuras.ActiveViewIndex = 0;
                mvAtributo.ActiveViewIndex = 0;
                CargarValoresConsulta(pConsulta, ReestructuraService.CodigoPrograma);
                CargarDDL();
                CargarEstadoCuenta();
                InicialCodeudores();
                txtFechaRes.ToDateTime = DateTime.Now;
                txtTasa.Text = "null";
            }
            VerError(GlobalWeb.gSeparadorMiles);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReestructuraService.CodigoPrograma, "Page_Load", ex);
        }
    }

    /// <summary>
    /// Método para cargar drop down list
    /// </summary>
    private void CargarDDL()
    {
        String ListaSolicitada = null;

        // Llena el DDL de la periodiciadad
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        ListaSolicitada = "Periodicidad";
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, _usuario);
        ddlperiodicidad.DataSource = lstDatosSolicitud;
        ddlperiodicidad.DataTextField = "ListaDescripcion";
        ddlperiodicidad.DataValueField = "ListaIdStr";
        ddlperiodicidad.DataBind();
        ListItem selectedListItem2 = ddlperiodicidad.Items.FindByValue("1"); //Selecciona mensual por defecto
        if (selectedListItem2 != null)
            selectedListItem2.Selected = true;

        // LLena el DDL de las líneas de crédito        
        LineasCreditoService LineasServicios = new LineasCreditoService();

        LlenarListasDesplegables(TipoLista.LineasCredito, ddllineas);

        // LLena el DDL de los atributos del crédito
        List<LineasCredito> lstAtributos = LineasServicios.ddlatributo(_usuario);
        ddlAtributo.DataSource = lstAtributos;
        ddlAtributo.DataTextField = "nombre";
        ddlAtributo.DataValueField = "cod_atr";
        ddlAtributo.DataBind();
        ddlAtributo.SelectedValue = "2";
        ddlAtributo.Enabled = false;

        // Llena el DDL de los tipos de tasas
        List<TipoTasa> lstTipoTasa = new List<TipoTasa>();
        TipoTasaService TipoTasaServicios = new TipoTasaService();
        TipoTasa vTipoTasa = new TipoTasa();
        lstTipoTasa = TipoTasaServicios.ListarTipoTasa(vTipoTasa, _usuario);
        ddlTipoTasa.DataSource = lstTipoTasa;
        ddlTipoTasa.DataTextField = "nombre";
        ddlTipoTasa.DataValueField = "cod_tipo_tasa";
        ddlTipoTasa.DataBind();
        ListItem selectedListItem1 = ddlTipoTasa.Items.FindByValue("2"); //Selecciona mensual por defecto
        if (selectedListItem1 != null)
            selectedListItem1.Selected = true;

        // Llena el DDL de los tipos de tasas historicas
        List<TipoTasaHist> lstTipoTasaHist = new List<TipoTasaHist>();
        TipoTasaHistService TipoTasaHistServicios = new TipoTasaHistService();
        TipoTasaHist vTipoTasaHist = new TipoTasaHist();
        lstTipoTasaHist = TipoTasaHistServicios.ListarTipoTasaHist(vTipoTasaHist, _usuario);
        ddlHistorico.DataSource = lstTipoTasaHist;
        ddlHistorico.DataTextField = "descripcion";
        ddlHistorico.DataValueField = "tipo_historico";
        ddlHistorico.DataBind();

        rbCalculoTasa.SelectedIndex = 0;
        mvAtributo.ActiveViewIndex = 1;

        // Si el parámetro en el WEBCONFIG esta fijao entonces llenar DDL de los asesores comerciales
        string ddlusuarios = ConfigurationManager.AppSettings["ddlusuarios"].ToString();
        if (ddlusuarios == "1")
        {
            asesores();
        }
    }

    /// <summary>
    /// Método para llenar el DDL de los asesores comerciales
    /// </summary>
    private void asesores()
    {
        Xpinn.Asesores.Services.UsuarioAseService serviceEjecutivo = new Xpinn.Asesores.Services.UsuarioAseService();
        Xpinn.Asesores.Entities.UsuarioAse ejec = new Xpinn.Asesores.Entities.UsuarioAse();
        Ddlusuarios.DataSource = serviceEjecutivo.ListartodosUsuarios(ejec, _usuario);
        Ddlusuarios.DataTextField = "nombre";
        Ddlusuarios.DataValueField = "codusuario";
        Ddlusuarios.DataBind();
        Ddlusuarios.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }


    protected void InicialCodeudores()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("codpersona");
        dt.Columns.Add("identificacion");
        dt.Columns.Add("tipo_identificacion");
        dt.Columns.Add("nombres");
        dt.Rows.Add();
        Session["Codeudores"] = dt;
        gvCodeudores.DataSource = dt;
        gvCodeudores.DataBind();
        gvCodeudores.Visible = true;
    }

    /// <summary>
    /// Método para cargar información del estado de cuenta
    /// </summary>
    private void CargarEstadoCuenta()
    {
        // Llena las grillas con la información de los productos de la persona
        if (Session[MOV_GRAL_CRED_PRODUC] != null)
        {
            producto = (Xpinn.Asesores.Entities.Producto)(Session[MOV_GRAL_CRED_PRODUC]);

            String nameCache = producto.Persona.IdPersona.ToString();
            object cacheValue = System.Web.HttpRuntime.Cache.Get(nameCache);
            DateTime timeExpiration = DateTime.Now.AddSeconds(60);
            txtCodigo.Text = producto.Persona.IdPersona.ToString();

            if (cacheValue == null)
            {
                Xpinn.Asesores.Services.EstadoCuentaService EstCueService = new Xpinn.Asesores.Services.EstadoCuentaService();
                producto.Persona = EstCueService.ConsultarPersona(producto.Persona.IdPersona, _usuario);
                System.Web.HttpRuntime.Cache.Add(nameCache, producto, null, timeExpiration, TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
            }
            else
            {
                producto = (Xpinn.Asesores.Entities.Producto)System.Web.HttpRuntime.Cache.Get(nameCache);
                Session[MOV_GRAL_CRED_PRODUC] = producto;
            }
            ObtenerValores();
            Actualizar();
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar("~/Page/Cartera/Reestructuraccion/Lista.aspx");
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        mpeNuevo.Show();
    }

    protected void ddlformapago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFormaPago.SelectedItem.Text == "Nomina")
        {
            ddlempresarecaudo.Visible = true;
            lblpagaduria.Visible = true;

            Xpinn.Tesoreria.Services.EmpresaRecaudoServices linahorroServicio = new Xpinn.Tesoreria.Services.EmpresaRecaudoServices();
            Xpinn.Tesoreria.Entities.EmpresaRecaudo linahorroVista = new Xpinn.Tesoreria.Entities.EmpresaRecaudo();
            ddlempresarecaudo.DataTextField = "NOM_EMPRESA";
            ddlempresarecaudo.DataValueField = "COD_EMPRESA";
            ddlempresarecaudo.DataSource = linahorroServicio.ListarEmpresaRecaudo(linahorroVista, _usuario);
            ddlempresarecaudo.DataBind();
            ddlempresarecaudo.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        }
        else
        {
            lblpagaduria.Visible = false;
            ddlempresarecaudo.Visible = false;
        }
    }

    /// <summary>
    /// Método para determinar la información del deudor al cual se le van a consultar los productos
    /// </summary>
    protected void ObtenerValores()
    {
        try
        {
            txtTelefono.Text = producto.Persona.Telefono != null ? producto.Persona.Telefono.ToString() : "";
            if (!string.IsNullOrEmpty(producto.Persona.PrimerApellido)) txtPrimerNombre.Text = producto.Persona.PrimerNombre.Trim().ToString();
            if (!string.IsNullOrEmpty(producto.Persona.PrimerNombre)) txtPrimerApellido.Text = producto.Persona.PrimerApellido.Trim().ToString();
            if (!producto.Persona.NumeroDocumento.Equals(0)) txtNumDoc.Text = producto.Persona.NumeroDocumento.ToString();
            if (!string.IsNullOrEmpty(producto.Persona.Direccion)) txtDireccion.Text = producto.Persona.Direccion;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReestructuraService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    private void Actualizar()
    {
        try
        {
            CreditoRecogerService creditoRecogerServicio = new CreditoRecogerService();
            List<CreditoRecoger> lstConsulta = new List<CreditoRecoger>();
            CreditoRecoger creditoRecoger = new CreditoRecoger();
            DateTime pfecha = DateTime.Now;
            try
            {
                pfecha = txtFechaRes.ToDateTime;
                if (pfecha == null)
                    pfecha = DateTime.Now;
            }
            catch
            {
                pfecha = DateTime.Now;
            }
            producto = (Xpinn.Asesores.Entities.Producto)(Session[MOV_GRAL_CRED_PRODUC]);
            lstConsulta = creditoRecogerServicio.ListarCreditoARecoger(producto.Persona.IdPersona.ToString(), pfecha, _usuario);
            gvLista.PageSize = 10;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
                foreach (GridViewRow gfila in gvLista.Rows)
                {
                    CheckBox ckRecoge = (CheckBox)gfila.FindControl("chkRecoger");
                    if ((ckRecoge != null))
                    {
                        ckRecoge.CheckedChanged += chkRecoger_CheckedChanged;
                    }
                }
            }
            else
            {
                gvLista.Visible = false;
            }
        }
        catch
        {
        }

        foreach (GridViewRow row in gvLista.Rows)
        {
            ((CheckBox)row.Cells[8].FindControl("chkRecoger")).Checked = false;
        }
    }


    /// <summary>
    /// Método para devolverse a la lista cuando se presiona el botón de regresar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("~/Page/Cartera/Reestructuraccion/Lista.aspx");
    }


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLista.PageIndex = e.NewPageIndex;
        Actualizar();
    }

    protected void chkRecoger_CheckedChanged(object sender, EventArgs e)
    {
        CalcularValor();
    }

    protected void txtidentificacion_TextChanged(object sender, EventArgs e)
    {
        Control ctrl = gvCodeudores.FooterRow.FindControl("txtidentificacion");
        if (ctrl != null)
        {
            TextBox txtidentificacion = (TextBox)ctrl;
            Control ctrln = gvCodeudores.FooterRow.FindControl("txtnombres");
            if (ctrln != null)
            {
                Label txtcodigo = (Label)gvCodeudores.FooterRow.FindControl("txtcodpersona");
                Label txtnombre = (Label)ctrln;
                Xpinn.FabricaCreditos.Services.codeudoresService codeudorServicio = new Xpinn.FabricaCreditos.Services.codeudoresService();
                Xpinn.FabricaCreditos.Entities.codeudores vcodeudor = new Xpinn.FabricaCreditos.Entities.codeudores();
                vcodeudor = codeudorServicio.ConsultarDatosCodeudor(txtidentificacion.Text, _usuario);
                txtcodigo.Text = vcodeudor.codpersona.ToString();
                txtnombre.Text = vcodeudor.nombres;
            }
        }
    }

    protected void CalcularValor()
    {
        Double total = 0;
        Double total_res = 0;
        Double totalnocap = 0;
        txtvalortotal.Text = "0";
        txtValorNoCapitaliza.Text = "0";
        foreach (GridViewRow gfila in gvLista.Rows)
        {
            CheckBox ckRecoge = (CheckBox)gfila.FindControl("chkRecoger");
            if (ckRecoge.Checked)
            {
                double valor = 0;
                //if (chkCapitalizar.Checked == true)
                //    try { valor = Convert.ToDouble(gfila.Cells[8].Text.Replace(GlobalWeb.gSeparadorMiles, "").Replace("$", "").Replace(" ", "").Replace("(", "").Replace(")", "").Replace(",", "")); }
                //    catch { }
                //else
                try { valor = Convert.ToDouble(gfila.Cells[4].Text.Replace(GlobalWeb.gSeparadorMiles, "").Replace("$", "").Replace(" ", "").Replace("(", "").Replace(")", "").Replace(",", "")); }
                catch { }
                total_res = total_res + valor;
                try { valor = Convert.ToDouble(gfila.Cells[8].Text.Replace(GlobalWeb.gSeparadorMiles, "").Replace("$", "").Replace(" ", "").Replace("(", "").Replace(")", "").Replace(",", "")); }
                catch { }
                total = total + valor;
            }
        }
        txtvalortotal.Text = total_res.ToString();
        totalnocap = total - total_res;
        txtValorNoCapitaliza.Text = totalnocap.ToString();
    }

    /// <summary>
    /// Método para el evento de continuar con la re-estructuración
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        VerError("");

        // Cargar los créditos a recoger
        List<Xpinn.FabricaCreditos.Entities.CreditoRecoger> LstRecoger = new List<Xpinn.FabricaCreditos.Entities.CreditoRecoger>();
        foreach (GridViewRow gfila in gvLista.Rows)
        {
            CheckBox ckRecoge = (CheckBox)gfila.FindControl("chkRecoger");
            if (ckRecoge.Checked)
            {
                Xpinn.FabricaCreditos.Entities.CreditoRecoger cRecoger = new Xpinn.FabricaCreditos.Entities.CreditoRecoger();
                try
                {
                    cRecoger.numero_credito = Convert.ToInt64(gfila.Cells[0].Text);
                    string svalor = gfila.Cells[8].Text.Replace("$", "").Replace(".", "").Replace(" ", "").Replace(",", "");
                    cRecoger.valor_recoge = Convert.ToInt64(svalor);
                    cRecoger.fecha_pago = Convert.ToDateTime(txtFechaRes.ToDate);
                    LstRecoger.Add(cRecoger);
                }
                catch (Exception ex)
                {
                    VerError(ex.Message);
                    return;
                }
            }
        }
        if (LstRecoger.Count <= 0)
        {
            VerError("No se han seleccionado créditos para recoger");
            return;
        }

        // Cargar datos de la re-estructuración
        Xpinn.Cartera.Services.ReestructuracionService RestrucServicio = new Xpinn.Cartera.Services.ReestructuracionService();
        Xpinn.Cartera.Entities.Reestructuracion vReestructuracion = new Xpinn.Cartera.Entities.Reestructuracion();
        vReestructuracion.fecha = DateTime.Now;
        vReestructuracion.cod_deudor = 0;
        vReestructuracion.monto_solicitado = 0;
        vReestructuracion.monto_nocapitaliza = 0;
        vReestructuracion.numero_cuotas = 0;
        vReestructuracion.num_cuo_nocap = 0;
        vReestructuracion.cod_linea_credito = "";
        vReestructuracion.cod_periodicidad = 0;
        vReestructuracion.cod_oficina = 0;
        vReestructuracion.numero_radicacion = 0;
        vReestructuracion.forma_pago = "C";
        vReestructuracion.fecha_primer_pago = DateTime.Now;
        vReestructuracion.cod_asesor = Convert.ToInt64(Ddlusuarios.SelectedValue.ToString());
        vReestructuracion.lstAtributos = new List<Atributos>();
        string error = "";
        try
        {
            vReestructuracion.fecha = Convert.ToDateTime(txtFechaRes.ToDate);
            vReestructuracion.monto_solicitado = Convert.ToDouble(txtvalortotal.Text.Replace(".", ""));
            if (!string.IsNullOrWhiteSpace(txtHonorarios.Text))
            {
                vReestructuracion.honorarios = Convert.ToDouble(txtHonorarios.Text.Replace(".", ""));
            }
            if (!string.IsNullOrWhiteSpace(txtDatacredito.Text))
            {
                vReestructuracion.datacredito = Convert.ToDouble(txtDatacredito.Text.Replace(".", ""));
            }
            if (!string.IsNullOrWhiteSpace(txtValorNoCapitaliza.Text))
            {
                vReestructuracion.monto_nocapitaliza = Convert.ToDouble(txtValorNoCapitaliza.Text.Replace(".", ""));
            }
            
            if (txtplazo.Text == "")
            {
                VerError("Debe ingresar el plazo del crédito");
                return;
            }
            vReestructuracion.numero_cuotas = Convert.ToInt64(txtplazo.Text);
            if (string.IsNullOrWhiteSpace(txtNumCuoCap.Text))
            {
                if (vReestructuracion.monto_nocapitaliza > 0)
                    txtNumCuoCap.Text = txtplazo.Text;
                else
                    txtNumCuoCap.Text = "0";
            }
            vReestructuracion.num_cuo_nocap = Convert.ToInt64(txtNumCuoCap.Text);
            vReestructuracion.cod_linea_credito = ddllineas.SelectedValue.ToString();
            vReestructuracion.cod_periodicidad = Convert.ToInt64(ddlperiodicidad.SelectedValue.ToString());
            Usuario pUsuario = _usuario;
            vReestructuracion.cod_oficina = pUsuario.cod_oficina;
            vReestructuracion.cod_deudor = Convert.ToInt64(txtCodigo.Text);
            vReestructuracion.forma_pago = ddlFormaPago.SelectedValue.ToString();
            if (ddlempresarecaudo.SelectedValue != "0" & ddlempresarecaudo.SelectedValue != "")
            {
                vReestructuracion.cod_empresa = Convert.ToInt32(ddlempresarecaudo.SelectedValue);
            }
            if (!txtfechaproxpago.TieneDatos)
            {
                VerError("Debe ingresar la fecha del primer pago");
                return;
            }
            vReestructuracion.fecha_primer_pago = Convert.ToDateTime(txtfechaproxpago.ToDate);
            vReestructuracion.bGarantias = chkGarantias.Checked;
        }
        catch (Exception ex)
        {
            VerError("Error al determinar parámetros de la reestructuración " + ex.Message);
            return;
        }

        if (vReestructuracion.num_cuo_nocap > vReestructuracion.numero_cuotas)
            vReestructuracion.num_cuo_nocap = vReestructuracion.numero_cuotas;
        if (vReestructuracion.fecha_primer_pago < DateTime.Now)
        {
            VerError("La fecha de primer pago no puede ser anterior a la fecha actual");
            return;
        }
        if (vReestructuracion.fecha_primer_pago < vReestructuracion.fecha)
        {
            VerError("La fecha de primer pago no puede ser anterior a la fecha de la re-estructuración");
            return;
        }

        // Cargar los atributos del crédito
        Configuracion conf = new Configuracion();
        Atributos vAtributos = new Atributos();
        try
        {
            vAtributos.cod_atr = Convert.ToInt64(ddlAtributo.SelectedValue);
            vAtributos.calculo_atr = Convert.ToInt64(rbCalculoTasa.SelectedValue);
            if (chkCobraMora.Checked == true)
                vAtributos.cobra_mora = 1;
            else
                vAtributos.cobra_mora = 0;
            if (rbCalculoTasa.SelectedValue == "1")
                vAtributos.tipo_historico = null;
            else
                vAtributos.tipo_historico = Convert.ToInt64(ddlHistorico.SelectedIndex);
            if (txtDesviacion.Text != "")
                vAtributos.desviacion = Convert.ToDouble(txtDesviacion.Text.Replace(conf.ObtenerSeparadorMilesConfig(), ""));
            vAtributos.tipo_tasa = Convert.ToInt64(ddlTipoTasa.SelectedValue);
            if (txtTasa.Text.Trim() != "")
            {
                vAtributos.tasa = Convert.ToDouble(txtTasa.Text.Replace(conf.ObtenerSeparadorMilesConfig(), ""));
                if (vAtributos.tasa == 0 && GlobalWeb.gValidarTasaReestructuracion == "1")
                {
                    VerError("la tasa del crédito no puede ser cero");
                    return;
                }
            }
            else
            {
                if (rbCalculoTasa.SelectedIndex == 0)
                {
                    VerError("Debe ingresar la tasa del crédito");
                    return;
                }
            }
            vReestructuracion.lstAtributos.Add(vAtributos);
        }
        catch
        {
            VerError("No pudo cargar los valores de los atributos del crédito");
            return;
        }

        // Cargar los codeudores
        vReestructuracion.lstCodeudores = new List<codeudores>();
        DataTable dtAgre = (DataTable)Session["Codeudores"];
        foreach (DataRow drFila in dtAgre.Rows)
        {
            if (dtAgre.Rows[0][0].ToString() != "")
            {
                codeudores vCodeudores = new codeudores();
                vCodeudores.idcodeud = 0;
                vCodeudores.codpersona = Convert.ToInt64(drFila[0].ToString());
                vCodeudores.identificacion = drFila[1].ToString();
                vCodeudores.tipo_codeudor = "S";
                vCodeudores.nombres = drFila[3].ToString();
                vCodeudores.opinion = " ";
                vCodeudores.parentesco = 0;
                vCodeudores.responsabilidad = " ";
                vReestructuracion.lstCodeudores.Add(vCodeudores);
            }
        }

        Int64 numero_radicacion = Int64.MinValue;
        RestrucServicio.CrearReestructurar(vReestructuracion, LstRecoger, ref numero_radicacion, ref error, _usuario);
        if (error == "")
        {
            mvReestructuras.ActiveViewIndex = 1;
            lblMensaje.Text = lblMensaje.Text + " Radicación: " + numero_radicacion;
            Session["numero_radicacion"] = numero_radicacion;
        }
        else
        {
            VerError(error);
        }

    }


    protected void ddlAtributo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlAtributo_TextChanged(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Método para el botón de parar cuando se escoge no continuar con la re-estructuración
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnParar_Click(object sender, EventArgs e)
    {
        mpeNuevo.Hide();
    }

    protected void btnFinalClick(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    /// <summary>
    /// Método para seleccionar el tipo de tasa
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rbCalculoTasa_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbCalculoTasa.SelectedIndex == 0)
            mvAtributo.ActiveViewIndex = 1;
        if (rbCalculoTasa.SelectedIndex == 1)
            mvAtributo.ActiveViewIndex = 0;
        if (rbCalculoTasa.SelectedIndex == 2)
            mvAtributo.ActiveViewIndex = 0;
    }


    protected void chkCapitalizar_CheckedChanged(object sender, EventArgs e)
    {
        CalcularValor();
    }

    protected void txtFechaIngreso_TextChanged(object sender, EventArgs e)
    {
        Actualizar();
        CalcularValor();
    }

    protected void btnPlanPagosClick(object sender, EventArgs e)
    {
        if (Session["numero_radicacion"] != null)
        {
            Xpinn.FabricaCreditos.Services.CreditoPlanService creditoPlanServicio = new Xpinn.FabricaCreditos.Services.CreditoPlanService();
            Session[creditoPlanServicio.CodigoPrograma + ".id"] = Session["numero_radicacion"];
            Navegar("~/Page/FabricaCreditos/PlanPagos/Detalle.aspx");
        }
    }

    protected void btnAprobacionClick(object sender, EventArgs e)
    {
        if (Session["numero_radicacion"] != null)
        {
            Xpinn.FabricaCreditos.Services.CreditoSolicitadoService creditoServicio = new Xpinn.FabricaCreditos.Services.CreditoSolicitadoService();
            Session[creditoServicio.CodigoPrograma + ".id"] = Session["numero_radicacion"];
            Navegar("~/Page/FabricaCreditos/CreditosPorAprobar/Detalle.aspx");
        }
    }


    protected void gvCodeudores_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtidentificacion = (TextBox)gvCodeudores.FooterRow.FindControl("txtidentificacion");
            Label txtcodigo = (Label)gvCodeudores.FooterRow.FindControl("txtcodpersona");
            Label txtnombres = (Label)gvCodeudores.FooterRow.FindControl("txtnombres");

            DataTable dtAgre = new DataTable();
            dtAgre = (DataTable)Session["Codeudores"];

            if (dtAgre.Rows[0][0] == null || dtAgre.Rows[0][0].ToString() == "")
            {
                dtAgre.Rows[0].Delete();
            }

            DataRow fila = dtAgre.NewRow();

            fila[0] = txtcodigo.Text;
            fila[1] = txtidentificacion.Text;
            fila[3] = txtnombres.Text;

            dtAgre.Rows.Add(fila);
            gvCodeudores.DataSource = dtAgre;
            gvCodeudores.DataBind();
            Session["Codeudores"] = dtAgre;
        }
    }


    protected void gvCodeudores_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Control ctrl = e.Row.FindControl("txtidentificacion");
            if (ctrl != null)
            {
                TextBox txtidentificacion = ctrl as TextBox;
                txtidentificacion.TextChanged += txtidentificacion_TextChanged;
            }
        }
    }


    protected void gvCodeudores_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable table = new DataTable();
            table = (DataTable)Session["Codeudores"];

            if ((e.RowIndex == 0) && (table.Rows[0][0] != null || table.Rows[0][0].ToString() == "") && (table.Rows.Count == 1))
            {
                table.Rows.Add();
            }

            table.Rows[e.RowIndex].Delete();

            gvCodeudores.DataSource = table;
            gvCodeudores.DataBind();
            Session["Codeudores"] = table;

            if ((e.RowIndex == 0) && (table.Rows[0][0] == null || table.Rows[0][0].ToString() == "") && (table.Rows.Count == 1))
                gvCodeudores.Rows[0].Visible = false;

        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReestructuraService.CodigoPrograma, "gvCodeudores_RowDeleting", ex);
        }

    }

}
