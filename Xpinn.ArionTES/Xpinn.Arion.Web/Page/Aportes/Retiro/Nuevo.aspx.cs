using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;
using Xpinn.Comun.Services;
using Xpinn.Comun.Entities;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using Xpinn.Caja.Services;
using Xpinn.Caja.Entities;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using Xpinn.Util;
using System.Globalization;

public partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private Xpinn.Aportes.Services.AporteServices AporteServicio = new Xpinn.Aportes.Services.AporteServices();
    private List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables
    private String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    private Decimal aportegrupo;
    private String operacion = "";
    int tipoOpe = 4;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[AporteServicio.ProgramaRetiro + ".id"] != null)
                VisualizarOpciones(AporteServicio.ProgramaRetiro, "E");
            else
                VisualizarOpciones(AporteServicio.ProgramaRetiro, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            panelProceso.Visible = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaRetiro, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {   operacion = (String)Session["operacion"];
            if (idObjeto == "")
            {                
                if (operacion == "N")
                {
                    txtNumAporte.Enabled = false;
                }
            }

            if (!IsPostBack)
            {
                LlenarComboLineaAporte(DdlLineaAporte);
   
                CargarListas();

                if (Session[AporteServicio.ProgramaRetiro + ".id"] != null)
                {
                    idObjeto = Session[AporteServicio.ProgramaRetiro.ToString() + ".id"].ToString();
                    Session.Remove(AporteServicio.ProgramaRetiro.ToString() + ".id");
                    this.LblMensaje.Text = "";
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaRetiro, "Page_Load", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnEditar_Click(object sender, EventArgs e)
    {


    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        String IdObjeto = txtNumeIdentificacion.Text;
        ConsultarCliente(IdObjeto);
        ConsultarClienteAporte();
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNumeIdentificacion.Text) || string.IsNullOrWhiteSpace(txtNumAporte.Text) || string.IsNullOrWhiteSpace(txtFecha_retiro.Text))
        {
            this.LblMensaje.Text = "Por favor verificar los campos Identificacion,numero aporte o fechaRetiro";
        }
        else
            ctlMensaje.MostrarMensaje("Esta Seguro en Grabar el retiro ?");
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(panelGeneral, AporteServicio.ProgramaRetiro);
        Navegar(Pagina.Nuevo);
        Session["operacion"] = "N";
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        VerError("");
        // Validar que exista la parametrización contable por procesos
        if (ValidarProcesoContable(Convert.ToDateTime(txtFecha_retiro.Text), tipoOpe) == false)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación 20=Retiro de Aportes");
            return;
        }
        // Determinar código de proceso contable para generar el comprobante
        Int64? rpta = 0;
        if (!panelProceso.Visible && panelGeneral.Visible)
        {
            rpta = ctlproceso.Inicializar(tipoOpe, Convert.ToDateTime(txtFecha_retiro.Text), (Usuario)Session["Usuario"]);
            if (rpta > 1)
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                // Activar demás botones que se requieran
                panelGeneral.Visible = false;
                panelProceso.Visible = true;
            }
            else
            {
                //consultar cierre historico
                String estado = "";
                DateTime fechacierrehistorico;
                String format = gFormatoFecha;
                DateTime Fecharetiro = DateTime.ParseExact(txtFecha_retiro.Text, format, CultureInfo.InvariantCulture);

                Xpinn.Aportes.Entities.Aporte vaportes = new Xpinn.Aportes.Entities.Aporte();
                vaportes = AporteServicio.ConsultarCierreAportes((Usuario)Session["usuario"]);
                estado = vaportes.estadocierre;
                fechacierrehistorico = Convert.ToDateTime(vaportes.fecha_cierre.ToString());

                if (estado == "D" && Fecharetiro <= fechacierrehistorico)
                {
                    VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO A,'APORTES'");
                    return;
                }
                else
                {
                    // Crear la tarea de ejecución del proceso                
                    if (grabar())
                        Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                    else
                    {
                        string lblErrorAnt = ((Label)Master.FindControl("lblError")).Text;
                        if (lblErrorAnt == "")
                            VerError("Se presentó error"); }
                }
            }
        }
    }
    

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(panelGeneral, AporteServicio.ProgramaRetiro);

    }

    private List<Xpinn.FabricaCreditos.Entities.Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
        return lstDatosSolicitud;
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaRetiro + "L", "gvLista_RowDataBound", ex);
        }
    }


    private void ConsultarCliente(String IdObjeto)
    {

        Xpinn.Aportes.Services.AporteServices AportesServicio = new Xpinn.Aportes.Services.AporteServices();
        Xpinn.Aportes.Entities.Aporte aporte = new Xpinn.Aportes.Entities.Aporte();
        String IdObjeto2 = txtNumeIdentificacion.Text;

        aporte = AportesServicio.ConsultarCliente(IdObjeto2, (Usuario)Session["usuario"]);
        if (aporte.cod_persona == 0)
        {
            LblMensaje.Text = "ESTE PERSONA NO ESTA CREADA";
            txtNombre.Text = "";
        }
        else
        {
            if (aporte.cod_persona != 0)
            {
                LblMensaje.Text = "";
                if (!string.IsNullOrEmpty(aporte.nombre.ToString()))
                    txtNombre.Text = Convert.ToString(aporte.nombre);
                if (!string.IsNullOrEmpty(aporte.tipo_identificacion.ToString()))
                    DdlTipoIdentificacion.SelectedValue = HttpUtility.HtmlDecode(aporte.tipo_identificacion);
                DdlTipoIdentificacion.Enabled = false;
                if (!string.IsNullOrEmpty(aporte.cod_persona.ToString()))
                    txtCodigoCliente.Text = Convert.ToString(aporte.cod_persona);
            }
        }

    }



    private Boolean ConsultarClienteAporte()
    {
        Boolean result = true;
        VerError("");
        Int64 numeroaporte = 1;
        AporteServices AportesServicio = new AporteServices();
        Aporte aporte = new Aporte();
        aporte = AportesServicio.ConsultarClienteAporte(this.txtNumeIdentificacion.Text,0, (Usuario)Session["usuario"]);

        if (!string.IsNullOrEmpty(aporte.numero_aporte.ToString()))
            numeroaporte = aporte.numero_aporte;
        if (numeroaporte > 0)
        {

            this.LblMensaje.Text = "Cliente ya tiene cuenta de aportes creada";
            result = false;

        }
        return result;

    }


    private Xpinn.Aportes.Entities.Aporte ObtenerValoresCliente()
    {
        Xpinn.Aportes.Entities.Aporte vAporte = new Xpinn.Aportes.Entities.Aporte();

        if (txtCodigoCliente.Text.Trim() != "")
            vAporte.cod_persona = Convert.ToInt64(txtCodigoCliente.Text.Trim());
        if (txtNumeIdentificacion.Text.Trim() != "")
            vAporte.identificacion = Convert.ToString(txtNumeIdentificacion.Text.Trim());

        return vAporte;
    }


    protected void LlenarComboLineaAporte(DropDownList ddlOficina)
    {
        DdlLineaAporte.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        AporteServices aporteService = new AporteServices();
        Usuario usuap = (Usuario)Session["usuario"];
        Aporte aporte = new Aporte();
        DdlLineaAporte.DataSource = aporteService.ListarLineaAporte(aporte, (Usuario)Session["usuario"]);
        DdlLineaAporte.DataTextField = "nom_linea_aporte";
        DdlLineaAporte.DataValueField = "cod_linea_aporte";
        DdlLineaAporte.DataBind();
        DdlLineaAporte.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }

    protected void LlenarComboPeriodicidad(DropDownList DdlPeriodicidad)
    {
        PeriodicidadService periodicidadService = new PeriodicidadService();
        Usuario usuap = (Usuario)Session["usuario"];
        Periodicidad periodicidad = new Periodicidad();
        DdlPeriodicidad.DataSource = periodicidadService.ListarPeriodicidad(periodicidad, (Usuario)Session["usuario"]);
        DdlPeriodicidad.DataTextField = "Descripcion";
        DdlPeriodicidad.DataValueField = "Codigo";
        DdlPeriodicidad.DataBind();

    }


    protected void LlenarComboTipoTasa(DropDownList DdlTipoTasa)
    {
        TipoTasaService tipotasaService = new TipoTasaService();
        Usuario usuap = (Usuario)Session["usuario"];
        TipoTasa tipotasa = new TipoTasa();
        DdlTipoTasa.DataSource = tipotasaService.ListarTipoTasa(tipotasa, (Usuario)Session["usuario"]);
        DdlTipoTasa.DataTextField = "nombre";
        DdlTipoTasa.DataValueField = "cod_tipo_tasa";
        DdlTipoTasa.DataBind();

    }


    protected void LlenarComboTasaHistorica(DropDownList DdlTipoHistorico)
    {
        TipoTasaHistService tipotasahistService = new TipoTasaHistService();
        Usuario usuap = (Usuario)Session["usuario"];
        TipoTasaHist tipotasa = new TipoTasaHist();
        DdlTipoHistorico.DataSource = tipotasahistService.ListarTipoTasaHist(tipotasa, (Usuario)Session["usuario"]);
        DdlTipoHistorico.DataTextField = "Descripcion";
        DdlTipoHistorico.DataValueField = "tipo_historico";
        DdlTipoHistorico.DataBind();
        DdlTipoHistorico.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
    }

    private void CargarListas()
    {
        txtFecha_retiro.Text = DateTime.Now.ToString(gFormatoFecha);
        try
        {
            ListaSolicitada = "TipoIdentificacion";
            TraerResultadosLista();
            DdlTipoIdentificacion.DataSource = lstDatosSolicitud;
            DdlTipoIdentificacion.DataTextField = "ListaDescripcion";
            DdlTipoIdentificacion.DataValueField = "ListaId";
            DdlTipoIdentificacion.DataBind();


            DropDownCuenta.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            DropDownCuenta.Items.Insert(1, new ListItem("Ahorros", "0"));
            DropDownCuenta.Items.Insert(2, new ListItem("Corriente", "1"));
            DropDownCuenta.DataBind();

            DropDownFormaDesembolso.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            DropDownFormaDesembolso.Items.Insert(1, new ListItem("Efectivo", "1"));
            DropDownFormaDesembolso.Items.Insert(2, new ListItem("Cheque", "2"));
            DropDownFormaDesembolso.Items.Insert(3, new ListItem("Transferencia", "3"));
            DropDownFormaDesembolso.DataBind();
            DropDownFormaDesembolso.SelectedIndex = 1;
            ActivarDesembolso();

            Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
            Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
            DropDownEntidad.DataSource = bancoService.ListarBancos(banco, (Usuario)Session["usuario"]);
            DropDownEntidad.DataTextField = "nombrebanco";
            DropDownEntidad.DataValueField = "cod_banco";
            DropDownEntidad.DataBind();

            ddlEntidadOrigen.DataSource = bancoService.ListarBancosEntidad(banco, (Usuario)Session["usuario"]);
            ddlEntidadOrigen.DataTextField = "nombrebanco";
            ddlEntidadOrigen.DataValueField = "cod_banco";
            ddlEntidadOrigen.DataBind();
            CargarCuentas();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }


    protected bool grabar()
    {       

        VerError("");
      
        // Inicializar las variables
        Usuario usuap = new Usuario();
        usuap = (Usuario)Session["Usuario"];
        Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
        Aporte aporte = new Aporte();
        String idObjeto2 = txtNumAporte.Text;
        String Error = "0";
            try
            {
                aporte.numero_aporte = Convert.ToInt64(txtNumAporte.Text);
                aporte.cod_linea_aporte = Int32.Parse(this.DdlLineaAporte.SelectedValue);
                aporte.cod_persona = Int64.Parse(txtCodigoCliente.Text);
                aporte.fecha_retiro = DateTime.Parse(txtFecha_retiro.Text);
                aporte.valor_retiro = Int64.Parse(txtValorRetiro.Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                aporte.autorizacion = txtNumAutorizacion.Text;
                aporte.observaciones = txtObservaciones.Text;
                aporte.cod_usuario = Int32.Parse(usuap.codusuario.ToString());
                aporte.fecha_crea = DateTime.Now;

                // Validar valor del retiro
                if (aporte.valor_retiro <= 0)
                {
                    VerError("Debe ingresar el valor del retiro");
                    return false;
                }

                //Agregado para validar que el monto a retirar no sea mayor al porcentaje indicado
                if(txtMaximoPorcentaje.Text != null && txtMaximoPorcentaje.Text != "")
                {
                    Int64 porcentaje = Convert.ToInt64(txtMaximoPorcentaje.Text.Trim());
                    Int64 saldoT = Int64.Parse(txtSaldoTotal.Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    Int64 maxSaldo = (saldoT * porcentaje) / 100;
                    if (aporte.valor_retiro > maxSaldo)
                    {
                        VerError("El valor a retirar supera el porcentaje máximo");
                        return false;
                    }                    
                }

                // Crear la operación
                pOperacion.cod_ope = 0;
                pOperacion.tipo_ope = 4;
                pOperacion.cod_usu = usuap.codusuario;
                pOperacion.cod_ofi = usuap.cod_oficina;
                pOperacion.fecha_oper = aporte.fecha_retiro;
                pOperacion.fecha_calc = aporte.fecha_crea;
                pOperacion.num_lista = 0;
                aporte.cod_ope = pOperacion.cod_ope;

                // Crear el Retiro
                decimal saldo = Convert.ToDecimal(txtSaldoTotal.Text);

            if (saldo >= aporte.valor_retiro)
            {
                Aporte aportes = new Aporte();

                TipoFormaDesembolso TipoFormaPago = DropDownFormaDesembolso.SelectedValue.ToEnum<TipoFormaDesembolso>();
                long pFornaDesembolso = (long)TipoFormaPago;
                int pIdCtaBancaria = 0;
                int pCodBanco = 0;
                string pNumeroCuenta = string.Empty;
                int pTipoCuenta = -1;

                switch (TipoFormaPago)
                {
                    case TipoFormaDesembolso.Cheque:
                        pIdCtaBancaria = Convert.ToInt32(ddlCuentaOrigen.SelectedValue);
                        break;
                    case TipoFormaDesembolso.Transferencia:
                        pIdCtaBancaria = Convert.ToInt32(ddlCuentaOrigen.SelectedValue);
                        pCodBanco = Convert.ToInt32(DropDownEntidad.SelectedValue);
                        pNumeroCuenta = txtnumcuenta.Text;
                        pTipoCuenta = Convert.ToInt32(DropDownCuenta.SelectedValue);
                        break;
                }

                if (TipoFormaPago == TipoFormaDesembolso.Cheque)
                {
                    Xpinn.Caja.Services.BancosService BancosService = new Xpinn.Caja.Services.BancosService();
                    Session["numerocheque"] = BancosService.soporte(ddlCuentaOrigen.SelectedItem.ToString(), (Usuario)Session["Usuario"]);
                    Session["entidad"] = ddlEntidadOrigen.SelectedValue;
                    Session["cuenta"] = ddlCuentaOrigen.SelectedValue;
                }
                aportes = AporteServicio.CrearRetiroAporte(aporte, pOperacion, pFornaDesembolso, pIdCtaBancaria, pCodBanco, pNumeroCuenta, pTipoCuenta, ref Error, Usuario);
                
                lblMensajeGrabar.Visible = true;
                if (Error.Trim() == "")
                {
                    // generar comprobante 
                    Xpinn.Aportes.Entities.OperacionApo voperacion = new Xpinn.Aportes.Entities.OperacionApo();
                    voperacion.cod_ope = Convert.ToInt64(aportes.cod_ope);
                    voperacion.tipo_ope = Convert.ToInt64(4);
                    voperacion.cod_ofi = Convert.ToInt64(usuap.cod_oficina);
                    voperacion.fecha_oper = Convert.ToDateTime(aportes.fecha_retiro);
                    voperacion.cod_cliente = Convert.ToInt64(aportes.cod_persona);

                    // Generar el comprobante
                    if (voperacion.cod_ope != 0)
                    {
                        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                        Session[ComprobanteServicio.CodigoPrograma + ".generado"] = "~/Page/Aportes/Retiro/Lista.aspx";
                        ctlproceso.CargarVariables(voperacion.cod_ope, Convert.ToInt16(voperacion.tipo_ope), voperacion.cod_cliente, (Usuario)Session["usuario"]);
                        return true;
                    }
                }
                else
                {
                    LblMensaje.Visible = true;
                    LblMensaje.Text = "El saldo total es menor que el valor a retirar. " + Error.Trim();
                }

            }
        }
            catch (Exception ex)
            {
                BOexcepcion.Throw(AporteServicio.ProgramaCruce, "AplicarDatos", ex);
            }

            return false;                   
            //VerError(Error); 
        
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Aporte aporte = new Aporte();
            GrupoLineaAporte linea = new GrupoLineaAporte();
            GrupoLineaAporteServices LineaAporteServicio = new GrupoLineaAporteServices();
            if (pIdObjeto != null)
            {
                aporte.numero_aporte = Int64.Parse(pIdObjeto);
                aporte = AporteServicio.ConsultarAporte(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);                
                txtNumAporte.Text = aporte.numero_aporte.ToString();
                this.DdlLineaAporte.SelectedValue = aporte.cod_linea_aporte.ToString();
                linea = LineaAporteServicio.ConsultarLineaAporte(aporte.cod_linea_aporte, (Usuario)Session["usuario"]);
                if (linea.max_porcentaje_saldo > 0)
                {
                    txtMaximoPorcentaje.Visible = true;
                    lblPorcentajeMax.Visible = true;
                }
                else
                {
                    lblPorcentajeMax.Visible = false;
                    txtMaximoPorcentaje.Visible = false;
                }                    
                txtMaximoPorcentaje.Text = linea.max_porcentaje_saldo.ToString();
                // txtFecha_retiro.Text = aporte.fecha_apertura.ToString(gFormatoFecha);
                txtCodigoCliente.Text = aporte.cod_persona.ToString();
                txtNumeIdentificacion.Text = aporte.identificacion.ToString();
                if(aporte.tipo_identificacion != null)
                    this.DdlTipoIdentificacion.SelectedValue = aporte.tipo_identificacion.ToString();
                txtNombre.Text = aporte.nombre.ToString();
                txtSaldoTotal.Text = aporte.Saldo.ToString();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    /// <summary>
    /// Método para cargar las centas bancarias según el banco seleccionado
    /// </summary>
    private void CargarCuentas()
    {
        Int64 codbanco = 0;
        try
        {
            codbanco = Convert.ToInt64(ddlEntidadOrigen.SelectedValue);
        }
        catch
        {
        }
        if (codbanco != 0)
        {
            Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
            Usuario usuario = (Usuario)Session["usuario"];
            ddlCuentaOrigen.DataSource = bancoService.ListarCuentaBancos(codbanco, usuario);
            ddlCuentaOrigen.DataTextField = "num_cuenta";
            ddlCuentaOrigen.DataValueField = "idctabancaria";
            ddlCuentaOrigen.DataBind();
        }
    }

    protected void ddlEntidadOrigen_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarCuentas();
    }

    protected void ddlCuentaOrigen_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DropDownEntidad_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DropDownFormaDesembolso_SelectedIndexChanged(object sender, EventArgs e)
    {
        ActivarDesembolso();
    }


    protected void ActivarDesembolso()
    {
        if (DropDownFormaDesembolso.SelectedItem.Text == "Transferencia")
        {
            lblEntidad.Visible = true;
            lblNumCuenta.Visible = true;
            lblTipoCuenta.Visible = true;
            txtnumcuenta.Visible = true;
            DropDownEntidad.Visible = true;
            DropDownCuenta.Visible = true;
            ddlEntidadOrigen.Visible = true;
            ddlCuentaOrigen.Visible = true;
            lblEntidadOrigen.Visible = true;
            lblNumCuentaOrigen.Visible = true;
        }
        else
        {
            lblEntidad.Visible = false;
            lblNumCuenta.Visible = false;
            lblTipoCuenta.Visible = false;
            txtnumcuenta.Visible = false;
            DropDownEntidad.Visible = false;
            DropDownCuenta.Visible = false;
            if (DropDownFormaDesembolso.SelectedItem.Text == "Efectivo")
            {
                ddlEntidadOrigen.Visible = false;
                ddlCuentaOrigen.Visible = false;
                lblEntidadOrigen.Visible = false;
                lblNumCuentaOrigen.Visible = false;
            }
            else
            {
                ddlEntidadOrigen.Visible = true;
                ddlCuentaOrigen.Visible = true;
                lblEntidadOrigen.Visible = true;
                lblNumCuentaOrigen.Visible = true;
            }
        }
    }

    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            panelGeneral.Visible = true;
            panelProceso.Visible = false;
            // Aquí va la función que hace lo que se requiera grabar en la funcionalidad
            grabar();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
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

}