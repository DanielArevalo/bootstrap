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


public partial class Detalle : GlobalWeb
{

    AvanceService AvancServices = new AvanceService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AvancServices.CodigoPrograma, "E");

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
        try
        {
            if (!IsPostBack)
            {
                CargarDropdown();
                mvAplicar.ActiveViewIndex = 0;
                txtNumero.Enabled = false;
                panelDatos.Enabled = false;
                txtFechaSoli.Text = Convert.ToString(DateTime.Now);
                if (Session[AvancServices.CodigoPrograma + ".id"] != null)
                {
                    txtFechaUlt.Text = Convert.ToDateTime(AvancServices.ObtenerUltimaFecha((Usuario)Session["usuario"])).ToShortDateString();
                    txtNumero.Text = AvancServices.ObtenerSiguienteCodigo((Usuario)Session["usuario"]).ToString(); 
                    idObjeto = Session[AvancServices.CodigoPrograma + ".id"].ToString();
                    Session.Remove(AvancServices.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                   
                    Session["TEXTO"] = "modificar";
                    lblmsj.Text = "grabada";
                }
                else
                {
                    Session["TEXTO"] = "grabar";
                    lblmsj.Text = "grabada";
                    txtNumero.Text = AvancServices.ObtenerSiguienteCodigo((Usuario)Session["usuario"]).ToString();                   
                }
                ddlForma_Desem_SelectedIndexChanged(ddlForma_Desem, null);
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


    void ActivarDesembolso()
    {
        if (ddlForma_Desem.SelectedItem.Text == "Transferencia")
        {
            panelFormaPago.Visible = true;
            lblEntidadGiro.Visible = true;
            ddlEntidad_giro.Visible = true;
            ddlCuenta_Giro.Visible = true;
            lblCuenta_Giro.Visible = true;
            txtNum_cuenta.Visible = true;
            lblNum_cuenta.Visible = true;
            ddlEntidad.Visible = true;
            lblEntidad.Visible = true;
            ddlTipo_cuenta.Visible = true;
            lblTipo_Cuenta.Visible = false;
        }
        else if (ddlForma_Desem.SelectedItem.Text == "Efectivo")
        {
            panelFormaPago.Visible = false;
        }
        else if (ddlForma_Desem.SelectedItem.Text == "Cheque")
        {
            panelFormaPago.Visible = true;
            lblEntidadGiro.Visible = true;
            ddlEntidad_giro.Visible = true;
            ddlCuenta_Giro.Visible = true;
            lblCuenta_Giro.Visible = true;
            txtNum_cuenta.Visible = false;
            lblNum_cuenta.Visible = false;
            ddlEntidad.Visible = false;
            lblEntidad.Visible = false;
            ddlTipo_cuenta.Visible = false;
            lblTipo_Cuenta.Visible = false;
        }
        else
        {
            panelFormaPago.Visible = false;
        }
    }

    void CargarDropdown()
    {

        ctlTasaInteres.Inicializar();

        ddlForma_Desem.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        ddlForma_Desem.Items.Insert(1, new ListItem("Efectivo", "1"));
        ddlForma_Desem.Items.Insert(2, new ListItem("Cheque", "2"));
        ddlForma_Desem.Items.Insert(3, new ListItem("Transferencia", "3"));
        ddlForma_Desem.SelectedIndex = 0;
        ddlForma_Desem.DataBind();

       
        ddlTipo_cuenta.Items.Insert(0, new ListItem("Ahorros", "0"));
        ddlTipo_cuenta.Items.Insert(1, new ListItem("Corriente", "1"));
        ddlTipo_cuenta.SelectedIndex = 1;
        ddlTipo_cuenta.DataBind();

        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
        ddlEntidad_giro.DataSource = bancoService.ListarBancosEntidad(banco, (Usuario)Session["usuario"]);
        ddlEntidad_giro.DataTextField = "nombrebanco";
        ddlEntidad_giro.DataValueField = "cod_banco";
        ddlEntidad_giro.DataBind();

        PoblarLista("bancos", ddlEntidad);
        CargarCuentas();
    }


    void CargarCuentas()
    {
        Int64 codbanco = 0;
        codbanco = Convert.ToInt64(ddlEntidad_giro.SelectedValue);
        if (codbanco != 0)
        {
            Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
            Usuario usuario = (Usuario)Session["usuario"];
            ddlCuenta_Giro.DataSource = bancoService.ListarCuentaBancos(codbanco, usuario);
            ddlCuenta_Giro.DataTextField = "num_cuenta";
            ddlCuenta_Giro.DataValueField = "idctabancaria";
            ddlCuenta_Giro.DataBind();
        }
        else
        {
            ddlCuenta_Giro.Items.Clear();
            ddlCuenta_Giro.DataBind();
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
            Avance vDetalleTasa = new Avance();
            Avance vDetallePlazo = new Avance();
            Avance vDetallePlazoMax = new Avance();

            vDetalle = AvancServices.ConsultarCreditoRotativo(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            vDetalleTasa = AvancServices.ConsultarTasaCreditoTotativo(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
        
            if (vDetalle.nomlinea != "")
                txtNomLinea.Text = vDetalle.nomlinea;
            if(vDetalle.numero_radicacion != 0)
                txtNumCredito.Text = vDetalle.numero_radicacion.ToString().Trim();
            if (vDetalle.cupototal != 0)// valor o monto aprobado
                txtCupoTotal.Text = vDetalle.cupototal.ToString();
            if (vDetalle.cupodisponible != 0)
                txtCupoDisp.Text = vDetalle.cupodisponible.ToString();
            if (vDetalle.nomoficina !="")
                txtOficina.Text = vDetalle.nomoficina;
            if (vDetalle.fecha_aprobacion != DateTime.MinValue)
                txtFechaApro.Text = vDetalle.fecha_aprobacion.ToShortDateString();       
           
            if (vDetalle.descforma_pago != "")
                txtFormaPago.Text = vDetalle.descforma_pago;
            if (txtFormaPago.Text == "Caja" || txtFormaPago.Text == "C" || txtFormaPago.Text == "1")
                txtnumFormPago.Text = "1";
            else if (txtFormaPago.Text == "Nomina" || txtFormaPago.Text == "N" || txtFormaPago.Text == "2")
                txtnumFormPago.Text = "2";

            if (vDetalle.identificacion != "")
                txtIdentificacion.Text = vDetalle.identificacion;
            if (vDetalle.nombre != "")
                txtNombre.Text = vDetalle.nombre;

            if (vDetalle.cod_deudor != 0)
                
                Session["codigocliente"] = vDetalle.cod_deudor;


            vDetallePlazo = AvancServices.ConsultarPlazoCreditoTotativo(Convert.ToString(txtNomLinea.Text), (Usuario)Session["usuario"]);
            vDetallePlazoMax = AvancServices.ConsultarPlazoMaximoCredito(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);



            if (vDetallePlazo.diferir == 1)
            {
                txtPlazo.Enabled = true;
            }
           
            if (vDetallePlazo.diferir == 0)
            {
                txtPlazo.Enabled = false;
                txtPlazo.Text = Convert.ToString(vDetallePlazoMax.plazo_maximo);
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
            BOexcepcion.Throw(AvancServices.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public Boolean ValidarDatos()
    {
        
        if (txtFechaSoli.Text == "")
        {
            VerError("Ingrese fecha de solicitud");
            return false;
        }
        if (txtValorSoli.Text == "" || txtValorSoli.Text=="0")
        {
            VerError("Ingrese el valor solicitado");
            return false;
        }
       // if (ddlForma_Desem.SelectedIndex == 0)
       // {
         //   VerError("Seleccione la forma de desembolso");
           // return false;
        //}
        decimal avance = Convert.ToDecimal(txtValorSoli.Text);
        decimal cupodisponible = Convert.ToDecimal(txtCupoDisp.Text);
        if (avance > cupodisponible)
        {
            VerError("El valor del avance supera el cupo total o disponible");
            return false;
        }
         return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");       
        if (ValidarDatos())
        {
            ctlMensaje.MostrarMensaje("Desea " + Session["TEXTO"].ToString() + " los Datos Ingresados?");          
        }
    }


    Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
    Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        //try
        //{
            Avance pVar = new Avance();
            if (txtNumero.Text != "")
                pVar.idavance = Convert.ToInt32(txtNumero.Text);
            else
                pVar.idavance = 0;

            pVar.numero_radicacion = Convert.ToInt64(txtNumCredito.Text);
            pVar.fecha_solicitud = Convert.ToDateTime(txtFechaSoli.Text);
            pVar.fecha_aprobacion = DateTime.MinValue;
            pVar.fecha_desembolso = DateTime.MinValue;
            pVar.valor_solicitado = Convert.ToDecimal(txtValorSoli.Text);
            pVar.valor_aprobado = 0;
            pVar.valor_desembolsado = 0;
            if (txtnumFormPago.Text == "C" || txtnumFormPago.Text == "Caja") pVar.forma_pago = 1;            
            else if (txtnumFormPago.Text == "N" || txtnumFormPago.Text == "Nomina") pVar.forma_pago = 2;
            else if (txtnumFormPago.Text == "") pVar.forma_pago = 2;
            else pVar.forma_pago = Convert.ToInt32(txtnumFormPago.Text);
            pVar.plazo_diferir = Convert.ToInt32(txtPlazo.Text);

            pVar.estado = "S";
            if (txtObserva.Text != "")
                pVar.observacion = txtObserva.Text;
            else
                pVar.observacion = null;

            Avance valida = new Avance();

            if (idObjeto != "")
                txtNumero.Text = AvancServices.ObtenerSiguienteCodigo((Usuario)Session["usuario"]).ToString();

            // tasa 
            pVar.forma_tasa = Convert.ToInt32(ctlTasaInteres.FormaTasa);
            if (ctlTasaInteres.Indice == 0)//NIGUNA
            {
                pVar.tipo_historico = null;
                pVar.desviacion = 0;
                pVar.tasa = 0;
                pVar.tipo_tasa = 0;
            }
            else if (ctlTasaInteres.Indice == 1)//FIJO
            {
                pVar.tipo_historico = null;
                pVar.desviacion = 0;
                if (ctlTasaInteres.Tasa != 0)
                    pVar.tasa = ctlTasaInteres.Tasa;
                pVar.tipo_tasa = ctlTasaInteres.TipoTasa;
            }
            else // HISTORICO
            {
                pVar.tipo_tasa = 0;
                pVar.tipo_historico = ctlTasaInteres.TipoHistorico;
                if (ctlTasaInteres.Desviacion != 0)
                    pVar.desviacion = ctlTasaInteres.Desviacion;
            }
            
            //CREAR
            AvancServices.CrearCreditoAvance(pVar, (Usuario)Session["usuario"]);
            
            //GUARDANDO GIRO
            Usuario pusu = (Usuario)Session["usuario"];
            Giro pGiro = new Giro();
            pGiro.idgiro = 0;
            pGiro.cod_persona = Convert.ToInt64(Session["codigocliente"].ToString());
            pGiro.forma_pago = Convert.ToInt32(ddlForma_Desem.SelectedValue);
            pGiro.tipo_acto = Convert.ToInt32(1);
            pGiro.fec_reg = DateTime.Now;
            pGiro.fec_giro = DateTime.MinValue;
            pGiro.numero_radicacion = Convert.ToInt64(txtNumCredito.Text);
            pGiro.usu_gen = pusu.nombre;
            pGiro.usu_apli = null;
            pGiro.estadogi = 0;
            pGiro.usu_apro = null;
            pGiro.fec_apro = DateTime.MinValue;

            CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ddlEntidad_giro.SelectedValue), ddlCuenta_Giro.SelectedItem.Text, (Usuario)Session["usuario"]);
            Int64 idCta = CuentaBanc.idctabancaria;

            if (ddlForma_Desem.SelectedItem.Text == "Transferencia")
            {
                pGiro.idctabancaria = idCta;
                pGiro.cod_banco = Convert.ToInt32(ddlEntidad.SelectedValue);
                pGiro.num_cuenta = txtNum_cuenta.Text;
                pGiro.tipo_cuenta = Convert.ToInt32(ddlTipo_cuenta.SelectedValue);
            }
            else if (ddlForma_Desem.SelectedItem.Text == "Cheque")
            {
                pGiro.idctabancaria = idCta;
                pGiro.cod_banco = 0; //NULO
                pGiro.num_cuenta = null; //NULO
                pGiro.tipo_cuenta = -1; //NULO
            }
            else
            {
                pGiro.idctabancaria = 0;
                pGiro.cod_banco = 0;
                pGiro.num_cuenta = null;
                pGiro.tipo_cuenta = -1;
            }
            pGiro.cob_comision = 0;
            pGiro.valor = 0;

            // AvancServices.CrearGiro(pGiro, (Usuario)Session["usuario"], 1);


            //GRABAR CONTROL DE CREDITO
            ControlCreditos pcont = new ControlCreditos();

            pcont.numero_radicacion = Convert.ToInt64(txtNumCredito.Text);
            pcont.codtipoproceso = "1";
            pcont.fechaproceso = Convert.ToDateTime(txtFechaApro.Text); ;
            //pControl.cod_persona = 0;
            pcont.cod_motivo = 0;
            if (txtObserva.Text != "")
                pcont.observaciones = txtObserva.Text;
            else
                pcont.observaciones = null;

            pcont.anexos = null;
            pcont.nivel = 0;
            pcont.fechaconsulta_dat = DateTime.MinValue;


            AvancServices.CrearControlCreditos(pcont, (Usuario)Session["usuario"]);              

            Session[AvancServices.CodigoPrograma + ".id"] = idObjeto;
            mvAplicar.ActiveViewIndex = 1;
            Site toolBar1 = (Site)this.Master;
            toolBar1.MostrarGuardar(false);


        //}
        //catch (Exception ex)
        //{
        //    BOexcepcion.Throw(AvancServices.CodigoPrograma, "btnContinuar_Click", ex);
        //}
    }


   
}
