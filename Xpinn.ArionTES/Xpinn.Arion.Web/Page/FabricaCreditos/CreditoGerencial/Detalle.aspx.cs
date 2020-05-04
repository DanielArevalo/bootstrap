using System;
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
    CreditoGerencialService CredGerencialServicio = new CreditoGerencialService();
    Xpinn.Asesores.Entities.Producto producto;
    Xpinn.FabricaCreditos.Services.codeudoresService CodeudorServicio = new Xpinn.FabricaCreditos.Services.codeudoresService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CredGerencialServicio.CodigoPrograma, "L");            
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            txtFechaRes.eventoCambiar += txtFechaIngreso_TextChanged;
            ctlValidarBiometria.eventoGuardar += btnContinuar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CredGerencialServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            producto = (Xpinn.Asesores.Entities.Producto)(Session[MOV_GRAL_CRED_PRODUC]);

            if (!IsPostBack)
            {
                mvReestructuras.ActiveViewIndex = 0;
                mvAtributo.ActiveViewIndex = 0;
                CargarValoresConsulta(pConsulta, ReestructuraService.CodigoPrograma);
                CargarDDL();                
                CargarEstadoCuenta();
                InicialCodeudores();
                CargarDropDownEmpresa();
                txtFechaRes.ToDateTime = DateTime.Now;
                txtTasa.Text = "null";
                ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
                ddllineas_SelectedIndexChanged(ddllineas, null);

                ObtenerFechaInicio();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CredGerencialServicio.GetType().Name + "A", "Page_Load", ex);
        }
    }


    void ObtenerFechaInicio()
    {
        try
        {
            CreditoService BOCreditoService = new CreditoService();
            DateTime? Fecha_Ini;
            string pError = "";
            Boolean pRpta = false;
            bool Ejecuta = false;
            Int32 pValueFormaPago = ddlFormaPago.SelectedIndex == 0 ? 1 : 2;
            if (ddlperiodicidad.SelectedIndex != 0 && ddllineas.SelectedItem != null && txtFechaRes.Texto != "")
            {
                Int32? cod_empresa;
                if (ddlFormaPago.SelectedIndex == 0)
                {
                    cod_empresa = null;
                    Ejecuta = true;
                }
                else
                {
                    if (ddlEmpresa.SelectedIndex != 0)
                    {
                        cod_empresa = Convert.ToInt32(ddlEmpresa.SelectedValue);
                        Ejecuta = true;
                    }
                    else
                    {
                        cod_empresa = null;
                        Ejecuta = false;
                    }
                }
                if (Ejecuta == true)
                {
                    Fecha_Ini = BOCreditoService.FechaInicioCredito(Convert.ToDateTime(txtFechaRes.Texto), Convert.ToInt32(ddlperiodicidad.SelectedValue),
                        Convert.ToInt32(pValueFormaPago), cod_empresa, ddllineas.SelectedValue, ref pError, ref pRpta, (Usuario)Session["usuario"]);
                    if (pRpta == false)
                    {
                        if (pError != "")
                        {
                            VerError(pError.ToString());
                        }
                    }
                    else
                    {
                        if (pError == "" && Fecha_Ini != null && Fecha_Ini != DateTime.MinValue)
                        {
                            txtfechaproxpago.Texto = Convert.ToDateTime(Fecha_Ini).ToShortDateString();
                        }
                    }
                }
            }
        }
        catch
        { }
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
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
        ddlperiodicidad.DataSource = lstDatosSolicitud;
        ddlperiodicidad.DataTextField = "ListaDescripcion";
        ddlperiodicidad.DataValueField = "ListaIdStr";
        ddlperiodicidad.DataBind();
        ListItem selectedListItem2 = ddlperiodicidad.Items.FindByValue("1"); //Selecciona mensual por defecto
        if (selectedListItem2 != null)
            selectedListItem2.Selected = true;
            
        // LLena el DDL de las líneas de crédito        
        List<Xpinn.FabricaCreditos.Entities.LineasCredito> LstLineas = new List<Xpinn.FabricaCreditos.Entities.LineasCredito>();
        Xpinn.FabricaCreditos.Services.LineasCreditoService LineasServicios = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        Xpinn.FabricaCreditos.Entities.LineasCredito vLineas = new Xpinn.FabricaCreditos.Entities.LineasCredito();
        vLineas.credito_gerencial = 1;
        LstLineas = LineasServicios.ListarLineasCredito(vLineas, (Usuario)Session["Usuario"]);
        ddllineas.DataSource = LstLineas;
        ddllineas.DataTextField = "nombre";
        ddllineas.DataValueField = "cod_linea_credito";
        ddllineas.DataBind();
        ddllineas.SelectedValue = "0";

        // LLena el DDL de los atributos del crédito
        List<LineasCredito> lstAtributos = new List<LineasCredito>();
        lstAtributos = LineasServicios.ddlatributo((Usuario)Session["Usuario"]);
        ddlAtributo.DataSource = lstAtributos;
        ddlAtributo.DataTextField = "nombre";
        ddlAtributo.DataValueField = "cod_atr";
        ddlAtributo.DataBind();       
        ddllineas_SelectedIndexChanged(ddllineas, null);

        // Llena el DDL de los tipos de tasas
        List<TipoTasa> lstTipoTasa = new List<TipoTasa>();
        TipoTasaService TipoTasaServicios = new TipoTasaService();
        TipoTasa vTipoTasa = new TipoTasa();
        lstTipoTasa = TipoTasaServicios.ListarTipoTasa(vTipoTasa, (Usuario)Session["Usuario"]);
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
        lstTipoTasaHist = TipoTasaHistServicios.ListarTipoTasaHist(vTipoTasaHist, (Usuario)Session["Usuario"]);
        ddlHistorico.DataSource = lstTipoTasaHist;
        ddlHistorico.DataTextField = "descripcion";
        ddlHistorico.DataValueField = "tipo_historico";
        ddlHistorico.DataBind();

        rbCalculoTasa.SelectedIndex = 0;
        mvAtributo.ActiveViewIndex = 1;

        Xpinn.Asesores.Services.UsuarioAseService serviceEjecutivo = new Xpinn.Asesores.Services.UsuarioAseService();
        Xpinn.Asesores.Entities.UsuarioAse ejec = new Xpinn.Asesores.Entities.UsuarioAse();
        if (Session["Usuario"] == null)
            return;
        ejec.estado = 1;
        Ddlusuarios.DataSource = serviceEjecutivo.ListarUsuario(ejec, (Usuario)Session["Usuario"]);
        Ddlusuarios.DataTextField = "nombre";
        Ddlusuarios.DataValueField = "codusuario";
        Ddlusuarios.DataBind();
        Ddlusuarios.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

        if (Session["usuario"] != null)
        {
            Xpinn.Seguridad.Services.UsuarioService serviceEjecutivos = new Xpinn.Seguridad.Services.UsuarioService();
            Xpinn.Util.Usuario usu = (Usuario)Session["usuario"];
            usu.estado = 1;
            if (usu.nombre != null && usu.nombre != "")
                Ddlusuarios.SelectedValue = usu.codusuario.ToString();
        }
    }

    void CargarDropDownEmpresa()
    {
        List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstEmpresas = new List<Xpinn.Tesoreria.Entities.EmpresaRecaudo>();
        Xpinn.Tesoreria.Entities.EmpresaRecaudo empresa = new Xpinn.Tesoreria.Entities.EmpresaRecaudo();
        Xpinn.Tesoreria.Services.EmpresaRecaudoServices empresaServicio = new Xpinn.Tesoreria.Services.EmpresaRecaudoServices();
        if (txtCodigo.Text == "")
        {
            ddlEmpresa.DataSource = empresaServicio.ListarEmpresaRecaudo(empresa, (Usuario)Session["usuario"]);
        }
        else
        {
            try
            {
                Int64 Cod_persona = Convert.ToInt64(txtCodigo.Text.ToString());
                ddlEmpresa.DataSource = empresaServicio.ListarEmpresaRecaudoPersona(Cod_persona, (Usuario)Session["usuario"]);
            }
            catch
            {
                ddlEmpresa.DataSource = empresaServicio.ListarEmpresaRecaudo(empresa, (Usuario)Session["usuario"]);
            }
        }
        ddlEmpresa.DataTextField = "nom_empresa";
        ddlEmpresa.DataValueField = "cod_empresa";
        ddlEmpresa.AppendDataBoundItems = true;
        ddlEmpresa.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlEmpresa.SelectedIndex = 0;
        ddlEmpresa.DataBind();
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
                producto.Persona = EstCueService.ConsultarPersona(producto.Persona.IdPersona, (Usuario)Session["Usuario"]);
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




    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (!ValidarDatos())
            return;
        mpeNuevo.Show();
    }

    private bool ValidarDatos()
    {
        Int64 pCod_Persona = Convert.ToInt64(txtCodigo.Text);
        string tipoCredito = ddllineas.SelectedValue;
        string pFiltro = obtFiltro();
        if (!ValidarNumCreditosPorLinea(tipoCredito, pFiltro, pCod_Persona, Usuario))
            return false;
        return true;
    }

    protected string obtFiltro()
    {
        string tipoCredito = ddllineas.SelectedValue;
        int cont = gvLista.Rows.OfType<GridViewRow>().Where(x => ((CheckBox)x.FindControl("chkRecoger")).Checked).Count();
        string pConcat = string.Empty;
        if (cont > 0)
        {
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox chkRecoger = (CheckBox)rFila.FindControl("chkRecoger");
                if (chkRecoger != null)
                {
                    if (chkRecoger.Checked)
                    {
                        Int64 pNumero_radicacion = Convert.ToInt64(gvLista.DataKeys[rFila.RowIndex].Value.ToString());
                        string pLinea = rFila.Cells[2].Text != "&nbsp;" ? rFila.Cells[2].Text : null;
                        if (pLinea != null)
                        {
                            string[] pCod_linea = pLinea.ToString().Split('-');
                            string Codigo = pCod_linea[0];
                            if (Codigo == tipoCredito)
                            {
                                pConcat += pNumero_radicacion + ",";
                            }
                        }
                    }
                }
            }
        }
        if (!string.IsNullOrEmpty(pConcat))
        {
            if (pConcat.Contains(","))
                pConcat = pConcat.Substring(0, ((pConcat.Length) - 1));
        }
        return string.IsNullOrEmpty(pConcat) ? null : pConcat;
    }

    /// <summary>
    /// Método para determinar la información del deudor al cual se le van a consultar los productos
    /// </summary>
    protected void ObtenerValores()
    {
        try
        {
            if (!string.IsNullOrEmpty(producto.Persona.Telefono)) txtTelefono.Text = producto.Persona.Telefono.ToString();   
            if (!string.IsNullOrEmpty(producto.Persona.PrimerApellido)) txtPrimerNombre.Text = producto.Persona.PrimerNombre.Trim().ToString();
            if (!string.IsNullOrEmpty(producto.Persona.PrimerNombre)) txtPrimerApellido.Text = producto.Persona.PrimerApellido.Trim().ToString();
            if (!string.IsNullOrEmpty(producto.Persona.NumeroDocumento)) if (!producto.Persona.NumeroDocumento.Equals(0)) txtNumDoc.Text = producto.Persona.NumeroDocumento.ToString();
            if (!string.IsNullOrEmpty(producto.Persona.Direccion)) txtDireccion.Text = producto.Persona.Direccion;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CredGerencialServicio.GetType().Name + "A", "ObtenerDatos", ex);
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
            lstConsulta = creditoRecogerServicio.ListarCreditoARecoger(producto.Persona.IdPersona.ToString(), pfecha, (Usuario)Session["Usuario"]);
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

   
    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Session[CredGerencialServicio.CodigoPrograma + ".id"] = txtCodigo.Text;
        Navegar("~/Page/FabricaCreditos/CreditoGerencial/Lista.aspx");  
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
                vcodeudor = codeudorServicio.ConsultarDatosCodeudor(txtidentificacion.Text, (Usuario)Session["Usuario"]);
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
        foreach (GridViewRow gfila in gvLista.Rows)
        {
            CheckBox ckRecoge = (CheckBox)gfila.FindControl("chkRecoger");
            if (ckRecoge.Checked)
            {
                double valor = 0;
                try { valor = Convert.ToDouble(gfila.Cells[4].Text.Replace(GlobalWeb.gSeparadorMiles, "").Replace("$", "").Replace(" ", "").Replace("(", "").Replace(")", "").Replace(",", "")); }
                catch { }
                total_res = total_res + valor;
                try { valor = Convert.ToDouble(gfila.Cells[8].Text.Replace(GlobalWeb.gSeparadorMiles, "").Replace("$", "").Replace(" ", "").Replace("(", "").Replace(")", "").Replace(",", "")); }
                catch { }
                total = total + valor;
            }
        }
        txtvalortotal.Text = total.ToString();
        totalnocap = total - total_res;
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
        decimal totalRecoge = 0;
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
                    cRecoger.valor_recoge = Convert.ToDecimal(svalor);
                    totalRecoge = totalRecoge + cRecoger.valor_recoge;
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

        //VALIDAR EL ENDEUDAMIENTO ACTUAL DE LA PERSONA.
        CreditoService BOCredito = new CreditoService();
        Xpinn.Comun.Data.GeneralData DAGeneral = new Xpinn.Comun.Data.GeneralData();
        Xpinn.Comun.Entities.General pEntidad = new Xpinn.Comun.Entities.General();
        pEntidad = DAGeneral.ConsultarGeneral(90164, (Usuario)Session["usuario"]);
        try
        {
            if (pEntidad.valor != null)
            {
                if (Convert.ToDecimal(pEntidad.valor.Replace(".", "")) > 0)
                {
                    decimal paramCantidad = 0, pValor = 0;
                    paramCantidad = Convert.ToDecimal(pEntidad.valor.Replace(".", ""));
                    pValor = BOCredito.ObtenerSaldoTotalXpersona(Convert.ToInt64(txtCodigo.Text), (Usuario)Session["usuario"]);
                    if (pValor > 0)
                    {
                        if (pValor > paramCantidad)
                        {
                            VerError("No se puede registrar la solicitud debido a que el asociado excede el monto total de endeudamiento.");
                            return;
                        }
                    }
                }
            }
        }
        catch { }

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
            vReestructuracion.honorarios = 0;
            vReestructuracion.datacredito = 0;
            vReestructuracion.monto_nocapitaliza = 0;
            if (txtplazo.Text == "")
            {
                VerError("Debe ingresar el plazo del crédito");
                return;
            }
            vReestructuracion.numero_cuotas = Convert.ToInt64(txtplazo.Text);    
            vReestructuracion.num_cuo_nocap = 0;
            vReestructuracion.cod_linea_credito = ddllineas.SelectedValue.ToString();
            vReestructuracion.cod_periodicidad = Convert.ToInt64(ddlperiodicidad.SelectedValue.ToString());
            Usuario pUsuario = (Usuario)Session["Usuario"];
            vReestructuracion.cod_oficina = pUsuario.cod_oficina;
            vReestructuracion.cod_deudor = Convert.ToInt64(txtCodigo.Text);
            vReestructuracion.forma_pago = ddlFormaPago.SelectedValue.ToString();
            vReestructuracion.cod_empresa = ddlEmpresa.Visible == true && ddlEmpresa.SelectedIndex != 0 ? Convert.ToInt32(ddlEmpresa.SelectedValue) : 0;                
            if (txtfechaproxpago.TieneDatos == false)
            {
                VerError("Debe ingresar la fecha del primer pago");
                return;
            }
            vReestructuracion.fecha_primer_pago = DateTime.ParseExact(txtfechaproxpago.ToDate, gFormatoFecha, null);
            vReestructuracion.bGarantias = false;
            if (vReestructuracion.fecha_primer_pago < vReestructuracion.fecha)
            {
                VerError("La fecha de primer pago no puede ser anterior a la fecha del crédito gerencial");
                return;
            }
        }
        catch (Exception ex)
        {
            VerError("Error al determinar parámetros del crédito gerencial " + ex.Message);
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
            VerError("La fecha de primer pago no puede ser anterior a la fecha de la solicitud");
            return;
        }

        // Cargar los atributos del crédito
        Configuracion conf = new Configuracion();
        Atributos vAtributos = new Atributos();
        try
        {
            if (ddlAtributo.SelectedIndex != 0)
            {
                vAtributos.cod_atr = Convert.ToInt64(ddlAtributo.SelectedValue);
                vAtributos.calculo_atr = Convert.ToInt64(rbCalculoTasa.SelectedValue);
            }
            else
            {
                VerError("Seleccione el Atributo");
                return;
            }            
            if (chkCobraMora.Checked == true)
                vAtributos.cobra_mora = 1;
            else
                vAtributos.cobra_mora = 0;
            if (ddlHistorico.SelectedValue != "")
                vAtributos.tipo_historico = Convert.ToInt64(ddlHistorico.SelectedValue);
            if (txtDesviacion.Text != "")
                vAtributos.desviacion = Convert.ToDouble(txtDesviacion.Text.Replace(conf.ObtenerSeparadorMilesConfig(), ""));
            vAtributos.tipo_tasa = Convert.ToInt64(ddlTipoTasa.SelectedValue);
            if (txtTasa.Text.Trim() != "")
            {
                if (vAtributos.calculo_atr == 1)
                {
                    vAtributos.tasa = Convert.ToDouble(txtTasa.Text.Replace(conf.ObtenerSeparadorMilesConfig(), ""));
                    if (vAtributos.tasa == 0 && GlobalWeb.gValidarTasaReestructuracion == "1")
                    {
                        VerError("la tasa del crédito no puede ser cero");
                        return;
                    }
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
        DataTable dtAgre = new DataTable();
        dtAgre = (DataTable)Session["Codeudores"];
        codeudores vCodeudores = new codeudores();
        foreach (DataRow drFila in dtAgre.Rows)
        {
            if (dtAgre.Rows[0][0].ToString() != "")
            {
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


        // Validar biometria
        string codigoPrograma = RestrucServicio.CodigoPrograma;
        string sError = "";
        if (ctlValidarBiometria.IniciarValidacion(Convert.ToInt32(codigoPrograma), CredGerencialServicio.CodigoPrograma, Convert.ToInt64(txtCodigo.Text), DateTime.Now, ref sError))
        {
            VerError(sError);
            return;
        }

        Int64 numero_radicacion = Int64.MinValue;
        RestrucServicio.CrearReestructurar(vReestructuracion, LstRecoger, ref numero_radicacion, ref error, (Usuario)Session["Usuario"]);
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
            if (txtidentificacion.Text.Trim() == "")
            {
                VerError("Ingrese la Identificación del Codeudor por favor.");
                ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
                return;
            }
            Xpinn.Comun.Data.GeneralData DAGeneral = new Xpinn.Comun.Data.GeneralData();
            Xpinn.Comun.Entities.General pEntidad = new Xpinn.Comun.Entities.General();
            pEntidad = DAGeneral.ConsultarGeneral(480, (Usuario)Session["usuario"]);
            try
            {
                if (pEntidad.valor != null)
                {
                    if (Convert.ToInt32(pEntidad.valor) > 0)
                    {
                        int paramCantidad = 0, cantReg = 0;
                        paramCantidad = Convert.ToInt32(pEntidad.valor);
                        Xpinn.FabricaCreditos.Entities.codeudores pCodeu = new Xpinn.FabricaCreditos.Entities.codeudores();
                        pCodeu = CodeudorServicio.ConsultarCantidadCodeudores(txtidentificacion.Text, (Usuario)Session["usuario"]);
                        if (pCodeu.cantidad != null)
                        {
                            cantReg = Convert.ToInt32(pCodeu.cantidad);
                            if (cantReg >= paramCantidad)
                            {
                                VerError("No puede adicionar esta persona debido a que ya mantiene el límite de veces como codeudor.");
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
                                return;
                            }
                        }
                    }
                }
            }
            catch { }

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
            BOexcepcion.Throw(CredGerencialServicio.GetType().Name + "L", "gvCodeudores_RowDeleting", ex);
        }    
    }

    protected void ddllineas_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Atributos> lstAtributos = new List<Atributos>();
        lstAtributos = CredGerencialServicio.ListarAtributosXlinea(ddllineas.SelectedValue, (Usuario)Session["usuario"]);
        if (lstAtributos.Count > 0)
        {
            ddlAtributo.Items.Clear();
            ddlAtributo.DataSource = lstAtributos;
            ddlAtributo.DataTextField = "nom_atr";
            ddlAtributo.DataValueField = "cod_atr";
            ddlAtributo.AppendDataBoundItems = true;
            ddlAtributo.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlAtributo.SelectedValue = "2"; //POR DEFECTO INT.CORRIENTE
            ddlAtributo.DataBind();
            rbCalculoTasa.SelectedValue = Convert.ToString(lstAtributos[0].calculo_atr) != "" ? Convert.ToString(lstAtributos[0].calculo_atr) : "1";
            rbCalculoTasa_SelectedIndexChanged(rbCalculoTasa, null);
            if (Convert.ToString(lstAtributos[0].tipo_historico) != "")
                ddlHistorico.SelectedValue = Convert.ToString(lstAtributos[0].tipo_historico);
            txtDesviacion.Text = Convert.ToString(lstAtributos[0].desviacion);
            txtTasa.Text = Convert.ToString(lstAtributos[0].tasa);
            if (Convert.ToString(lstAtributos[0].tipo_tasa) != "")
                ddlTipoTasa.SelectedValue = Convert.ToString(lstAtributos[0].tipo_tasa);
        }
        else
        {
            ddlAtributo.Items.Clear();
            LimpiarDatosTasa();
        }
    }

    protected void ddlAtributo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAtributo.SelectedIndex != 0)
        {
            LineasCredito pdatos = new LineasCredito();
            pdatos.cod_linea_credito = ddllineas.SelectedValue;
            pdatos.cod_atr = Convert.ToInt64(ddlAtributo.SelectedValue);

            pdatos = CredGerencialServicio.ConsultarDatosXatributos(pdatos, (Usuario)Session["usuario"]);
            if (pdatos.cod_atr != null)
            {
                if (pdatos.calculo_atr != null && pdatos.calculo_atr != "")
                    if (pdatos.calculo_atr == "1" || pdatos.calculo_atr == "3" || pdatos.calculo_atr == "5")
                    {
                        rbCalculoTasa.SelectedValue = pdatos.calculo_atr;
                        if (pdatos.tipo_historico != 0 && pdatos.tipo_historico != null)
                            ddlHistorico.SelectedValue = pdatos.tipo_historico.ToString();
                        if (pdatos.desviacion != null && pdatos.desviacion != 0)
                            txtDesviacion.Text = pdatos.desviacion.ToString();
                        if (pdatos.tasa != null && pdatos.tasa != 0)
                            txtTasa.Text = pdatos.tasa.ToString();
                        if (pdatos.tipotasa != null && pdatos.tipotasa != 0)
                            ddlTipoTasa.SelectedValue = pdatos.tipotasa.ToString();

                        rbCalculoTasa_SelectedIndexChanged(rbCalculoTasa, null);
                    }
            }
        }
        else
        {
            LimpiarDatosTasa();
        }        
    }

    protected void LimpiarDatosTasa()
    {
        rbCalculoTasa.ClearSelection();
        ddlAtributo.SelectedIndex = 0;
        txtDesviacion.Text = "";
        txtTasa.Text = "";
        ddlTipoTasa.SelectedIndex = 0;
    }

    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFormaPago.SelectedItem.Value == "N")
        {
            lblEmpresa.Visible = true;
            ddlEmpresa.Visible = true;
        }
        else
        {
            lblEmpresa.Visible = false;
            ddlEmpresa.Visible = false;
        }
        ObtenerFechaInicio();
    }

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObtenerFechaInicio();
    }
}
   
   