using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;

public partial class Nuevo : GlobalWeb
{

    AvanceService AvancServices = new AvanceService();
    CreditoService CreditoServicio = new CreditoService();
    private string _convenio = "";

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AvancServices.CodigoProgramaDesem, "E");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AvancServices.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        _convenio = ConvenioTarjeta(0);
        try
        {
            if (!IsPostBack)
            {
                txtFechaDesem.Text = Convert.ToString(DateTime.Now);
                CargarDropdown();
                mvAplicar.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;
                txtFechaSoli.Enabled = false;
                txtValorSoli.Enabled = false;
                panel1.Enabled = false;

                if (Session[AvancServices.CodigoProgramaDesem + ".id"] != null)
                {
                    idObjeto = Session[AvancServices.CodigoProgramaDesem + ".id"].ToString();
                    Session.Remove(AvancServices.CodigoProgramaDesem + ".id");
                    ObtenerDatos(idObjeto);
                }
                //Panel5.Visible=true;
                //ActivarDesembolso();//  ddlForma_Desem_SelectedIndexChanged(ddlForma_Desem, null);


            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AvancServices.GetType().Name + "L", "Page_Load", ex);
        }

    }


    protected void ddlForma_Desem_SelectedIndexChanged(object sender, EventArgs e)
    {
        ActivarDesembolso();
    }

    void OcultarControlesDesembolso()
    {
        pnlCuentaAhorroVista.Visible = false;
        pnlCuentasBancarias.Visible = false;
        ddlEntidadOrigen.Visible = false;
        ddlCuentaOrigen.Visible = false;
        lblEntidadOrigen.Visible = false;
        lblNumCuentaOrigen.Visible = false;
        lblEntidad.Visible = false;
        lblNumCuenta.Visible = false;
        lblTipoCuenta.Visible = false;
        //ddlNumeroCuenta.Visible = false;
        txtNumeroCuenta.Visible = false;
        DropDownEntidad.Visible = false;
        ddlTipo_cuenta.Visible = false;
        ddlCuentaAhorroVista.Visible = false;
        lblCuentaAhorroVista.Visible = false;
    }


    protected void ActivarDesembolso()
    {
        TipoFormaDesembolso formaDesembolso = DropDownFormaDesembolso.SelectedValue.ToEnum<TipoFormaDesembolso>();

        OcultarControlesDesembolso();

        if (formaDesembolso == TipoFormaDesembolso.Cheque)
        {
            ddlEntidadOrigen.Visible = true;
            ddlCuentaOrigen.Visible = true;
            lblEntidadOrigen.Visible = true;
            lblNumCuentaOrigen.Visible = true;
            pnlCuentasBancarias.Visible = true;
        }
        else if (formaDesembolso == TipoFormaDesembolso.Transferencia)
        {
            lblEntidad.Visible = true;
            lblNumCuenta.Visible = true;
            lblTipoCuenta.Visible = true;
            //ddlNumeroCuenta.Visible = true;
            txtNumeroCuenta.Visible = true;
            DropDownEntidad.Visible = true;
            ddlTipo_cuenta.Visible = true;
            ddlEntidadOrigen.Visible = true;
            ddlCuentaOrigen.Visible = true;
            lblEntidadOrigen.Visible = true;
            lblNumCuentaOrigen.Visible = true;
            pnlCuentasBancarias.Visible = true;
        }
        else if (formaDesembolso == TipoFormaDesembolso.TranferenciaAhorroVistaInterna)
        {
            ddlCuentaAhorroVista.Visible = true;
            lblCuentaAhorroVista.Visible = true;
            pnlCuentaAhorroVista.Visible = true;
        }
    }


    void CargarDropdown()
    {
        // ctlTasaInteres.Inicializar();


        //PoblarLista("bancos", ddlEntidad);
        // CargarCuentas();
        // ddlForma_Desem.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        //ddlForma_Desem.Items.Insert(1, new ListItem("Efectivo", "1"));
        //ddlForma_Desem.Items.Insert(2, new ListItem("Cheque", "2"));
        // ddlForma_Desem.Items.Insert(3, new ListItem("Transferencia", "3"));
        // ddlForma_Desem.Items.Insert(4, new ListItem("Otros", "4"));
        // ddlForma_Desem.Items.Insert(5, new ListItem("Tranferencia Cuenta Ahorro Vista Interna", "5"));

        // ddlForma_Desem.SelectedIndex = 1;
        //ddlForma_Desem.DataBind();

        // ddlTipo_cuenta.Items.Insert(0, new ListItem("Ahorros", "0"));
        // ddlTipo_cuenta.Items.Insert(1, new ListItem("Corriente", "1"));
        // ddlTipo_cuenta.SelectedIndex = 1;
        //ddlTipo_cuenta.DataBind();

        //Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        //Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
        //ddlEntidad_giro.DataSource = bancoService.ListarBancosEntidad(banco, (Usuario)Session["usuario"]);
        //ddlEntidad_giro.DataTextField = "nombrebanco";
        //ddlEntidad_giro.DataValueField = "cod_banco";
        // ddlEntidad_giro.DataBind();


    }


    protected void CargarCuentas()
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


    protected void ddlEntidad_giro_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarCuentas();
    }


    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }



    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Avance vDetalle = new Avance();
            String pIdObjeto2 = Convert.ToString(Session["numavanace"]);
            String pIdCredito = Convert.ToString(Session["numcredito"]);
            Avance vDetalleTasa = new Avance();
            Avance vDetallePlazo = new Avance();
            Avance vDetallePlazoMax = new Avance();

            vDetalle = AvancServices.ConsultarCredRotativoXaprobar(Convert.ToInt64(pIdObjeto2), (Usuario)Session["usuario"]);

            if (vDetalle.idavance != 0)
                txtCodigo.Text = vDetalle.idavance.ToString();
            if (vDetalle.cod_linea_credito != "")
                txtcodLineacredito.Text = vDetalle.cod_linea_credito;


            if (vDetalle.nomlinea != "")
                txtNomLinea.Text = vDetalle.nomlinea;
            if (vDetalle.numero_radicacion != 0)
                txtNumCredito.Text = vDetalle.numero_radicacion.ToString().Trim();
            if (vDetalle.nomoficina != "")
                txtOficina.Text = vDetalle.nomoficina;

            if (vDetalle.identificacion != "")
                txtIdentificacion.Text = vDetalle.identificacion;
            if (vDetalle.nombre != "")
                txtNombre.Text = vDetalle.nombre;

            if (vDetalle.fecha_solicitud != DateTime.MinValue)
                txtFechaSoli.Text = vDetalle.fecha_solicitud.ToShortDateString();
            if (vDetalle.valor_solicitado != 0)
                txtValorSoli.Text = vDetalle.valor_solicitado.ToString();

            if (vDetalle.fecha_aprobacion != DateTime.MinValue)
                txtFechaApro.Text = vDetalle.fecha_aprobacion.ToShortDateString();
            if (vDetalle.valor_aprobado != 0)
                txtValorApro.Text = vDetalle.valor_aprobado.ToString();
            else
                txtValorApro.Text = txtValorSoli.Text;


            if (vDetalle.descforma_pago != "")
                txtFormaPago.Text = vDetalle.descforma_pago;
            if (vDetalle.observacion != "")
                txtObservacion.Text = vDetalle.observacion;

            if (vDetalle.cod_deudor != 0)
            {
                Session["codigocliente"] = vDetalle.cod_deudor;
                LblMensaje.Text = AvancServices.AlertaTarjeta(vDetalle.cod_deudor, (Usuario)Session["usuario"]);
            }            

            AhorroVistaServices ahorroServices = new AhorroVistaServices();
            List<AhorroVista> lstAhorros = ahorroServices.ListarCuentaAhorroVistaGiros(Convert.ToInt64(Session["codigocliente"]), Usuario);
            ddlCuentaAhorroVista.DataSource = lstAhorros;
            ddlCuentaAhorroVista.DataTextField = "numero_cuenta";
            ddlCuentaAhorroVista.DataValueField = "numero_cuenta";
            ddlCuentaAhorroVista.DataBind();

            // Llenando listas desplegables

            ddlTipo_cuenta.Items.Insert(0, new ListItem("Ahorros", "0"));
            ddlTipo_cuenta.Items.Insert(1, new ListItem("Corriente", "1"));
            ddlTipo_cuenta.DataBind();


            DropDownFormaDesembolso.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            DropDownFormaDesembolso.Items.Insert(1, new ListItem("Efectivo", "1"));
            DropDownFormaDesembolso.Items.Insert(2, new ListItem("Cheque", "2"));
            DropDownFormaDesembolso.Items.Insert(3, new ListItem("Transferencia", "3"));
            DropDownFormaDesembolso.Items.Insert(4, new ListItem("TranferenciaAhorroVistaInterna", "4"));
            DropDownFormaDesembolso.Items.Insert(5, new ListItem("Otros", "5"));
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
            
            vDetalleTasa = AvancServices.ConsultarTasaCreditoTotativo(Convert.ToInt64(pIdCredito), (Usuario)Session["usuario"]);
            
            vDetallePlazo = AvancServices.ConsultarPlazoCreditoTotativo(Convert.ToString(txtNomLinea.Text), (Usuario)Session["usuario"]);
            vDetallePlazoMax = AvancServices.ConsultarPlazoMaximoCredito(Convert.ToInt64(pIdCredito), (Usuario)Session["usuario"]);

            if (vDetallePlazo.diferir == 1)
            {
                txtPlazo.Enabled = true;
                txtPlazo.Text = Convert.ToString(vDetalle.plazo_diferir);
            }

            if (vDetallePlazo.diferir == 0 && vDetalle.plazo_diferir == 0)
            {
                txtPlazo.Enabled = false;
                txtPlazo.Text = vDetallePlazoMax.plazo_maximo.ToString().Trim();
            }
            if (vDetallePlazo.diferir == 0 || vDetalle.plazo_diferir > 0)
            {
                txtPlazo.Text = Convert.ToString(vDetalle.plazo_diferir);
            }

            // para la tasa


            if (vDetalleTasa.calculo_atr != null)
            {
                if (!string.IsNullOrEmpty(vDetalleTasa.calculo_atr.ToString()))
                    ctlTasaInteres.FormaTasa = HttpUtility.HtmlDecode(vDetalleTasa.calculo_atr.ToString().Trim());
                if (!string.IsNullOrEmpty(vDetalleTasa.tipo_historico.ToString()))
                    ctlTasaInteres.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(vDetalleTasa.tipo_historico.ToString().Trim()));
                if (!string.IsNullOrEmpty(vDetalleTasa.desviacion.ToString()))
                    ctlTasaInteres.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(vDetalleTasa.desviacion.ToString().Trim()));
                if (!string.IsNullOrEmpty(vDetalleTasa.tipo_tasa.ToString()))
                    ctlTasaInteres.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(vDetalleTasa.tipo_tasa.ToString().Trim()));
                if (!string.IsNullOrEmpty(vDetalleTasa.tasa.ToString()))
                    ctlTasaInteres.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(vDetalleTasa.tasa.ToString().Trim()));
            }


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AvancServices.CodigoProgramaDesem, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }



    protected Boolean ValidarDatos()
    {
        if (Panel5.Visible == true)
        {
            if (DropDownFormaDesembolso.SelectedIndex == 0)
            {
                VerError("Debe seleccionar la forma de pago para realizar el desembolso.");
                DropDownFormaDesembolso.Focus();
                return false;
            }
            if (DropDownFormaDesembolso.SelectedValue == "3")
            {
                if (txtNumeroCuenta.Text == "")
                {
                    VerError("Debe ingresar el número de la cuenta en la forma de desembolso.");
                    txtNumeroCuenta.Focus();
                    return false;
                }
            }

            if (txtFechaDesem.Text == "")
            {
                VerError("Ingrese la fecha de Desembolso");
                return false;
            }
            if (Convert.ToDateTime(txtFechaDesem.Text) < Convert.ToDateTime(txtFechaApro.Text))
            {
                VerError("No puede generar el Desembolso en una fecha menor de la que fue Aprobada");
                return false;
            }

            if (LblMensaje.Text != "" && LblMensaje.Text != null)
            {
                VerError("No puede generar el Desembolso el cupo y/o la tarjeta estan bloqueados");
                return false;
            }

        }
        return true;
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            ctlMensaje.MostrarMensaje("Desea generar el Desembolso?");
        }
    }


    Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
    Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            string error = "";
            Avance pVar = new Avance();
            pVar.idavance = Convert.ToInt32(txtCodigo.Text);
            pVar.fecha_desembolso = Convert.ToDateTime(txtFechaDesem.Text);
            pVar.estado = "C";

            if (idObjeto != "")
            {
                string sMontoSolicitado = this.txtValorSoli.Text.Replace(".", "");
                pVar.fecha_desembolso = Convert.ToDateTime(txtFechaDesem.Text);
                pVar.valor_desembolsado = Decimal.Parse(txtValorApro.Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                if (pVar.valor_desembolsado == 0)
                {
                    pVar.valor_desembolsado = Decimal.Parse(txtValorSoli.Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                }
                //MODIFICAR CREDITO AVANCE
                AvancServices.ModificarDesembolsoAvance(pVar, (Usuario)Session["usuario"]);
            }

            ////GRABACION DE CONTROL DE CREDITOS
            //ControlCreditos pcont = new ControlCreditos();
            //pcont.numero_radicacion = Convert.ToInt64(txtNumCredito.Text);
            //pcont.codtipoproceso = "3";
            //pcont.fechaproceso = Convert.ToDateTime(txtFechaDesem.Text); ;
            ////pControl.cod_persona = 0;   el codigo de persona se graba en la capa datos
            //pcont.cod_motivo = 0;
            //pcont.observaciones = "DESEMBOLSO CREDITO DE AVANCE";
            //pcont.anexos = null;
            //pcont.nivel = 0;
            //pcont.fechaconsulta_dat = DateTime.MinValue;
            //AvancServices.CrearControlCreditos(pcont, (Usuario)Session["usuario"]);

            Giro vDetalle = new Giro();
            vDetalle = AvancServices.ConsultarFormaDesembolso(Convert.ToInt64(txtNumCredito.Text), (Usuario)Session["usuario"]);

            //GUARDANDO GIRO
            Usuario pusu = (Usuario)Session["usuario"];
            Giro pGiro = new Giro();
            if (vDetalle.idgiro != 0) pGiro.idgiro = vDetalle.idgiro;
            if (vDetalle.cod_persona != 0) pGiro.cod_persona = vDetalle.cod_persona;
            if (vDetalle.forma_pago != 0) pGiro.forma_pago = vDetalle.forma_pago;
            if (vDetalle.tipo_acto != 0) pGiro.tipo_acto = vDetalle.tipo_acto;
            if (vDetalle.fec_reg != DateTime.MinValue) pGiro.fec_reg = DateTime.Now;
            pGiro.fec_giro = DateTime.MinValue;
            if (vDetalle.numero_radicacion != 0) pGiro.numero_radicacion = Convert.ToInt64(txtNumCredito.Text);
            if (vDetalle.usu_gen != "" && vDetalle.usu_gen != null) pGiro.usu_gen = vDetalle.usu_gen;
            pGiro.usu_apli = null;
            pGiro.estadogi = 1;
            pGiro.usu_apro = pusu.nombre;
            pGiro.fec_apro = DateTime.Now;
            bool pGenerarGiro = false;

            TipoFormaDesembolso formaPago = DropDownFormaDesembolso.SelectedValue.ToEnum<TipoFormaDesembolso>();
            if (formaPago == TipoFormaDesembolso.Transferencia)
            {
                CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ddlEntidadOrigen.SelectedValue), ddlCuentaOrigen.SelectedItem.Text, (Usuario)Session["usuario"]);
                Int64 idCta = CuentaBanc.idctabancaria;

                pGiro.idctabancaria = idCta;
                pGiro.cod_banco = Convert.ToInt32(DropDownEntidad.SelectedValue);
                pGiro.num_cuenta = txtNumeroCuenta.Text;
                pGiro.tipo_cuenta = Convert.ToInt32(ddlTipo_cuenta.SelectedValue);
                pGenerarGiro = true;
            }
            else if (formaPago == TipoFormaDesembolso.Cheque)
            {
                CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ddlEntidadOrigen.SelectedValue), ddlCuentaOrigen.SelectedItem.Text, (Usuario)Session["usuario"]);
                Int64 idCta = CuentaBanc.idctabancaria;

                pGiro.idctabancaria = idCta;
                pGiro.cod_banco = 0;        //NULO
                pGiro.num_cuenta = null;    //NULO
                pGiro.tipo_cuenta = -1;     //NULO
                pGenerarGiro = true;
            }
            else if (formaPago == TipoFormaDesembolso.TranferenciaAhorroVistaInterna)
            {
                pGenerarGiro = false;

                pVar.numero_cuenta_ahorro_vista = !string.IsNullOrWhiteSpace(ddlCuentaAhorroVista.SelectedValue) ? Convert.ToInt64(ddlCuentaAhorroVista.SelectedValue) : default(long?);

                if (!pVar.numero_cuenta_ahorro_vista.HasValue)
                {
                    VerError("No tiene una cuenta de ahorro a la vista asociada para esta forma de pago!.");
                }
                pGiro.tipo_cuenta = -1;
            }
            else
            {
                pGiro.idctabancaria = 0;
                pGiro.cod_banco = 0;
                pGiro.num_cuenta = null;
                pGiro.tipo_cuenta = -1;
                pGenerarGiro = true;
            }
            pGiro.cob_comision = 0;
            pGiro.valor = Convert.ToInt64(txtValorApro.Text.Replace(".", ""));

            if (pGenerarGiro == true)
            {
                if (pGiro.valor <= 0)
                {
                    VerError("El giro no puede ser menor o igual a cero");
                    return;
                }
                //AvancServices.CrearGiro(pGiro, (Usuario)Session["usuario"], 2); //MODIFICAR
            }

            if (error == "")
            {
                this.GenerarDesembolso();
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AvancServices.CodigoProgramaDesem, "btnContinuar_Click", ex);
        }
    }


    protected bool GenerarDesembolso()
    {

        // Cargar los datos del crédito
        Credito cred = new Credito();
        Usuario usuap = (Usuario)Session["usuario"];
        cred.numero_radicacion = Convert.ToInt64(this.txtNumCredito.Text);
        cred.estado = "C";
        cred.fecha_desembolso = Convert.ToDateTime(txtFechaDesem.Text);
        cred.plazo = 0;
        //  DateTime fecha_inicio = Convert.ToDateTime(txtFechaDesem.ToDate);
        DateTime fechaactual = Convert.ToDateTime(DateTime.Now);

        cred.fecha_prim_pago = Convert.ToDateTime(this.txtFechaApro.Text);
        cred.cod_deudor = Convert.ToInt64(Session["codigocliente"]);
        string sMonto = txtValorApro.Text.Replace(".", "");
        string sMontoSolicitado = this.txtValorSoli.Text.Replace(".", "");
        if (sMonto != "0")
            cred.monto = Convert.ToInt64(sMonto);
        else
            cred.monto = Convert.ToInt64(sMontoSolicitado);
        cred.cod_ope = 0;
        cred.cod_linea_credito = txtcodLineacredito.Text;

        string sError = CreditoServicio.ValidarCredito(cred, usuap).Trim();
        if (sError != "")
        {
            VerError(sError + " (Validar Desembolso)");
            return false;
        }

        // Cargando datos para el cheque
        if (ddlCuentaOrigen.Visible == true)
        {
            Xpinn.Caja.Services.BancosService BancosService = new Xpinn.Caja.Services.BancosService();
            if (DropDownFormaDesembolso.SelectedItem.Text == "Cheque")
                Session["numerocheque"] = BancosService.soporte(ddlCuentaOrigen.SelectedItem.ToString(), (Usuario)Session["Usuario"]);
            else
                Session.Remove("numerocheque");
            Session["entidad"] = ddlEntidadOrigen.SelectedValue;
            Session["cuenta"] = ddlCuentaOrigen.SelectedValue;
        }

        // Cargando datos de la cuenta para las transferencias
        Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
        Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
        int idctabancaria = 0, cod_banco = 0, tipo_cuenta = 0;
        string num_cuenta = "";
        bool pGenerarGiro = false;
        if (Panel5.Visible == true)
        {
            pGenerarGiro = true;
            Int32? idCta = null;
            if (ddlEntidadOrigen.SelectedValue != "")
            {
                CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ddlEntidadOrigen.SelectedValue), ddlCuentaOrigen.SelectedItem.Text, (Usuario)Session["usuario"]);
                idCta = CuentaBanc.idctabancaria;
            }

            TipoFormaDesembolso formaPago = DropDownFormaDesembolso.SelectedValue.ToEnum<TipoFormaDesembolso>();
            if (formaPago == TipoFormaDesembolso.Transferencia)
            {
                idctabancaria = Convert.ToInt32(idCta);
                cod_banco = Convert.ToInt32(DropDownEntidad.SelectedValue);
                //num_cuenta = ddlNumeroCuenta.SelectedValue;
                num_cuenta = txtNumeroCuenta.Text;
                tipo_cuenta = Convert.ToInt32(ddlTipo_cuenta.SelectedValue);
            }
            else if (formaPago == TipoFormaDesembolso.Cheque)
            {
                idctabancaria = Convert.ToInt32(idCta);
                cod_banco = 0; //NULO
                num_cuenta = null; //NULO
                tipo_cuenta = -1; //NULO
            }
            else if (formaPago == TipoFormaDesembolso.TranferenciaAhorroVistaInterna)
            {
                pGenerarGiro = false;
                cred.numero_cuenta_ahorro_vista = !string.IsNullOrWhiteSpace(ddlCuentaAhorroVista.SelectedValue) ? Convert.ToInt64(ddlCuentaAhorroVista.SelectedValue) : default(long?);

                if (!cred.numero_cuenta_ahorro_vista.HasValue)
                {
                    VerError("No tiene una cuenta de ahorro a la vista asociada para esta forma de pago!.");
                    return false;
                }

                tipo_cuenta = -1;
            }
            else
            {
                idctabancaria = 0;
                cod_banco = 0;
                num_cuenta = null;
                tipo_cuenta = -1;
            }
        }

        // Realizar el desembolso del crèdito
        sError = "";
        Avance avance = new Avance();
        avance.idavance = Convert.ToInt32(txtCodigo.Text);
        cred.numero_credito = avance.idavance;
        cred.cod_usuario = Convert.ToInt32(usuap.codusuario);
        sError = "";
        CreditoServicio.DesembolsarAvances(cred, pGenerarGiro, Convert.ToInt64(DropDownFormaDesembolso.SelectedValue), idctabancaria, cod_banco, num_cuenta, tipo_cuenta, ref sError, (Usuario)Session["usuario"]);
        if (sError != "")
        {
            VerError(sError);
            return false;
        }

        // Guardar datos para la hoja de ruta
        // consultarControl();

        // Guardar los datos de la cuenta del cliente y generar el comprobante si se pudo crear la operaciòn.
        if (cred.cod_ope != 0)
        {
            #region ENPACTO
            // Generar transacción ENPACTO
            Xpinn.FabricaCreditos.Services.Persona1Service servicioPersona = new Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 persona = new Persona1();
            persona = servicioPersona.ConsultaDatosPersona(txtIdentificacion.Text, (Usuario)Session["Usuario"]);
            string tipoIdentificacion = persona.tipo_identificacion.ToString();
            AplicarTransaccionEnpacto(tipoIdentificacion, txtIdentificacion.Text, txtNombre.Text, TipoDeProducto.Credito.ToString(), txtNumCredito.Text, "1", cred.monto, cred.cod_ope, "CREDITO AVANCE FINANCIAL", ref sError);

            // Si el giro es a una cuenta de ahorro tambien toca actualizar el saldo en ENPACTO
            if (!string.IsNullOrWhiteSpace(ddlCuentaAhorroVista.Text))
                AplicarTransaccionEnpacto(tipoIdentificacion, txtIdentificacion.Text, txtNombre.Text, TipoDeProducto.AhorrosVista.ToString(), ddlCuentaAhorroVista.Text, "2", cred.monto, cred.cod_ope, "DEPOSITO AVANCE FINANCIAL", ref sError);
            #endregion

            // Generar la contabilización
            if (!string.IsNullOrWhiteSpace(txtNumeroCuenta.Text))
                CreditoServicio.GuardarCuentaBancariaCliente(cred.cod_deudor, txtNumeroCuenta.Text, Convert.ToInt64(ddlTipo_cuenta.SelectedValue), Convert.ToInt64(DropDownEntidad.SelectedValue), (Usuario)Session["Usuario"]);

            Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = cred.cod_ope;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 114;
            Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = cred.cod_deudor;

            String CtaBancaria = Panel5.Visible == true && ddlCuentaOrigen.SelectedItem != null ? ddlCuentaOrigen.SelectedItem.Text : null;
            if (ddlCuentaOrigen.SelectedItem != null)
                Session[ComprobanteServicio.CodigoPrograma + ".CuentaBancaria"] = CtaBancaria;            

            Navegar("../../../Contabilidad/Comprobante/Nuevo.aspx");
        }

        Session.Remove(CreditoServicio.CodigoProgramaoriginal + ".id");
        Session.Remove("numavanace");
        Session.Remove("numcredito");
        return true;
    }


    protected void DropDownEntidad_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DropDownFormaDesembolso_SelectedIndexChanged(object sender, EventArgs e)
    {
        ActivarDesembolso();

    }

    protected void DropDownFormaDesembolso_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlEntidadOrigen_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarCuentas();
    }
}
