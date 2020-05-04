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
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;


partial class Nuevo : GlobalWeb
{
   
    CuentasPorPagarService CuotasXpagar = new CuentasPorPagarService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[CuotasXpagar.CodigoProgramaAnticipo + ".id"] != null)
                VisualizarOpciones(CuotasXpagar.CodigoProgramaAnticipo, "E");
                
            else
                VisualizarOpciones(CuotasXpagar.CodigoProgramaAnticipo, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuotasXpagar.CodigoProgramaAnticipo, "Page_PreInit", ex);
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            Session["DatosDetalle"] = null;
            Session["DatosFormaPago"] = null;
     
            if (!Page.IsPostBack)
            {
                ctlGiros.Inicializar();
                ctlGiros.Visible = false;
                mvCuentasxPagar.ActiveViewIndex = 0;
                txtCodigo.Enabled = true;
                txtFechaFact.Text = DateTime.Today.ToShortDateString();
                cargarDropdown();
                if (Session[CuotasXpagar.CodigoProgramaAnticipo + ".id"] != null)
                {
                    idObjeto = Session[CuotasXpagar.CodigoProgramaAnticipo + ".id"].ToString();
                    Session.Remove(CuotasXpagar.CodigoProgramaAnticipo + ".id");
                    ObtenerDatos(idObjeto);
                  
                }
                else
                {
                    txtCodigo.Text = "";
                }
                
            }
            else
            {
                //RECALCULANDO EL VALOR NETO CUANDO SE DIGITA EN ALGUN IMPUESTO DINAMICO
                          
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuotasXpagar.CodigoProgramaAnticipo, "Page_Load", ex);
        }
    }

    protected void txtValor_TextChanged(object sender, EventArgs e)
    {
        ObtenerDatos(idObjeto);
        ctlGiros.Visible = true;

        
    }
    void cargarDropdown()
    {
    
    }

    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
       
    }
    protected void ObtenerDatos(String id)
    {
        try
        {
            CuentasPorPagar cuentas = new CuentasPorPagar();
            CuentasPorPagar vCuentas = new CuentasPorPagar();
            if (idObjeto != "")
                txtCodigo.Text = idObjeto;
            if(cuentas.codigo_factura!=null)
            cuentas.codigo_factura = Convert.ToInt32(txtCodigo.Text);
            vCuentas = CuotasXpagar.CONSULTARANTICIPOS(cuentas, (Usuario)Session["usuario"]);
           
            if (vCuentas.codigo_factura != 0)
                txtNumFactura.Text = Convert.ToString(vCuentas.codigo_factura);
            if (vCuentas.fecha_factura != DateTime.MinValue)
                txtFechaFact.Text = vCuentas.fec_fact.ToString(gFormatoFecha);
            if (vCuentas.fecha_vencimiento != null)
                if (vCuentas.fecha_radicacion != DateTime.MinValue)
                    txtfechaanticipa.Text = Convert.ToDateTime(vCuentas.fecha_radicacion).ToString(gFormatoFecha);
            if (vCuentas.identificacion != "")
                txtidentificacion.Text = Convert.ToString(vCuentas.identificacion);
            if (vCuentas.estado != 0)
                txtestado.Text = Convert.ToString(vCuentas.estado);
            if (Convert.ToString(vCuentas.valorneto) != "")
                txtvalorneto.Text = Convert.ToString(vCuentas.valorneto);
            if (Convert.ToString(vCuentas.valor_total) != "")
                txtvalor.Text = Convert.ToString(vCuentas.valor_total);
            if (vCuentas.valor_total != 0)
                txtsaldo.Text = Convert.ToString(vCuentas.valor_total);
            if (vCuentas.observaciones != "");
              txtobservaciones.Text = vCuentas.observaciones;
              if (vCuentas.nombre != "")
                  txtnombre.Text = vCuentas.nombre;
              if (vCuentas.cod_persona != 0)
                  txtcodpersona.Text = Convert.ToString(vCuentas.cod_persona);
              cuentas.codigo_factura = Convert.ToInt32(txtCodigo.Text);

              ctlGiros.Visible = true;
        }
        
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuotasXpagar.CodigoProgramaAnticipo, "ObtenerDatos", ex);
        }
    }

    decimal sumVrTotal = 0;
    decimal sumDescuen = 0;
    decimal sumVrNeto = 0;

    protected void gvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlCentroCosto = (DropDownListGrid)e.Row.FindControl("ddlCentroCosto");
            if (ddlCentroCosto != null)

            sumVrTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "valor_total"));
            sumDescuen += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "porc_descuento"));

            //SI EL VR TOTAL TIENE DATOS SE ACTIVAN EL CAMPO DE PORCENTAJE

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Totales:";
                e.Row.Cells[7].Text = sumVrTotal.ToString("c0");
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[8].Text = sumDescuen.ToString();
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
                //e.Row.Cells[10].Text = sumVrNeto.ToString("c0");
                //e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Font.Bold = true;
            }
        }
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ctlGiros.ValueEntidadOrigen == "")
            {
                VerError("digite los datos del giro");
                return;            
            }
            if (txtCodigo.Text == "")
            {
                VerError("digite un codigo de la persona");
                return;
            }
            VerError("");
           
                ctlMensaje.MostrarMensaje("Desea grabar los datos ingresados?");
         
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuotasXpagar.CodigoProgramaAnticipo, "btnGuardar_Click", ex);
        }
    }

    Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
    Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            ///operacion
            ///
            Usuario usuap = new Usuario();

            Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
            Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();

            //GRABACION DEL GIRO A REALIZAR
            CUENTAXPAGAR_ANTICIPO AvancServices = new CUENTAXPAGAR_ANTICIPO();
            Usuario pusu = (Usuario)Session["usuario"];
            Xpinn.Tesoreria.Entities.Giro pGiro = new Xpinn.Tesoreria.Entities.Giro();
            pGiro.idgiro = 0;
            pGiro.cod_persona = Convert.ToInt64(txtcodpersona.Text);
            pGiro.forma_pago = Convert.ToInt32(ctlGiros.ValueFormaDesem);
            pGiro.tipo_acto = 2;
            pGiro.fec_reg = DateTime.Now;
            pGiro.fec_giro = DateTime.Now;
            pGiro.numero_radicacion = 0;
            pGiro.usu_gen = pusu.nombre;
            pGiro.usu_apli = null;
            pGiro.estado = 0;
            pGiro.usu_apro = null;

            //DETERMINAR LA IDENTIFICACIÓN DE LA CUENTA BANCARIA
            CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ctlGiros.ValueEntidadOrigen), ctlGiros.TextCuentaOrigen, (Usuario)Session["usuario"]);
            Int64 idCta = CuentaBanc.idctabancaria;

            //DATOS DE FORMA DE PAGO
            if (ctlGiros.IndiceFormaDesem == 3) //"Transferencia"
            {
                pGiro.idctabancaria = idCta;
                pGiro.cod_banco = Convert.ToInt32(ctlGiros.ValueEntidadDest);
                pGiro.num_cuenta = ctlGiros.TextNumCuenta;
                pGiro.tipo_cuenta = Convert.ToInt32(ctlGiros.ValueTipoCta);
            }
            else if (ctlGiros.IndiceFormaDesem == 2) //Cheque
            {
                pGiro.idctabancaria = idCta;
                pGiro.cod_banco = 0;        //NULO
                pGiro.num_cuenta = null;    //NULO
                pGiro.tipo_cuenta = -1;     //NULO
            }
            else
            {
                pGiro.idctabancaria = 0;
                pGiro.cod_banco = 0;
                pGiro.num_cuenta = null;
                pGiro.tipo_cuenta = -1;
            }
            pGiro.fec_apro = DateTime.MinValue;
            pGiro.cob_comision = 0;
            pGiro.valor = Convert.ToInt64(txtsaldo.Text.Replace(".", ""));

            // CREAR OPERACION
            pOperacion.cod_ope = 0;
            pOperacion.tipo_ope = 20;
            pOperacion.cod_caja = 0;
            pOperacion.cod_cajero = 0;
            pOperacion.observacion = "Operacion-Cruce Cuentas";
            pOperacion.cod_proceso = null;
            pOperacion.fecha_oper = Convert.ToDateTime(txtFechaFact.Texto);
            pOperacion.fecha_calc = DateTime.Now;
            pOperacion.cod_ofi = usuap.cod_oficina;
            CUENTAXPAGAR_ANTICIPO vCuentas = new CUENTAXPAGAR_ANTICIPO();

            if (idObjeto != "")
                vCuentas.codigo_factura = Convert.ToInt32(txtCodigo.Text);
            else
                vCuentas.codigo_factura = 0;

            if (txtNumFactura.Visible == true)
            {
                if (txtNumFactura.Text != "")
                    vCuentas.codigo_factura = Convert.ToInt32(txtNumFactura.Text);
                else
                    vCuentas.codigo_factura = 0;
            }
            else
                vCuentas.codigo_factura = 0;


            if (txtFechaFact.Visible == true)
            {
                if (txtFechaFact.Text != "")
                    vCuentas.fecha_factura = Convert.ToDateTime(txtFechaFact.Text);
                else
                    vCuentas.fecha_factura = DateTime.MinValue;
            }
            else
                vCuentas.fecha_factura = DateTime.MinValue;

            vCuentas.estado = Convert.ToInt32(txtestado.Text);
            vCuentas.fecha_anticipo = Convert.ToDateTime(txtfechaanticipa.Text);
            vCuentas.fecha_aprobacion = Convert.ToDateTime(txtFechaFact.Text);
            vCuentas.fecha_factura = Convert.ToDateTime(txtFechaFact.Text);
            if (idObjeto != "")
                vCuentas.idanticipo = Convert.ToInt64(idObjeto);
            else
                vCuentas.idanticipo = 0;
            vCuentas.saldo = Convert.ToDecimal(txtsaldo.Text);
            vCuentas.valor = Convert.ToDecimal(txtvalor.Text);
                   
            if (idObjeto != "")
                CuotasXpagar.ModificarCUENTAXPAGAR_ANTICIPO(pGiro, pOperacion,vCuentas, (Usuario)Session["usuario"]);
            else
                CuotasXpagar.CrearCUENTAXPAGAR_ANTICIPO(pGiro, pOperacion, vCuentas, (Usuario)Session["usuario"]);

            Xpinn.Caja.Entities.TransaccionCaja tranCaja = new Xpinn.Caja.Entities.TransaccionCaja();

            // Se genera el comprobante
            DateTime fecha = Convert.ToDateTime(txtFechaFact.Text);
            Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = tranCaja.cod_ope;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 2;
            Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = Convert.ToDateTime(txtFechaFact.Text);
            Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = txtcodpersona.Text;
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] = pusu.cod_oficina;
            Session[ComprobanteServicio.CodigoPrograma + ".ventanilla"] = "1";
            Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");


            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuotasXpagar.CodigoProgramaAnticipo, "btnContinuarMen_Click", ex);
        }    
    }
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }
}