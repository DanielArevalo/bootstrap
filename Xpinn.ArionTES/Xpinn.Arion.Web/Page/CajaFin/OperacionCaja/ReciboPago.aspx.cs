using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using Xpinn.Util;
using System.Configuration;

public partial class ReciboPago : GlobalWeb
{
    Xpinn.Caja.Services.PersonaService peopleServicio = new Xpinn.Caja.Services.PersonaService();
    Xpinn.Caja.Services.MovimientoCajaService movimientoServicio = new Xpinn.Caja.Services.MovimientoCajaService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Request.ApplicationPath == "/")
            lblruta.Text = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/Page/CajaFin/OperacionCaja/";
        else
            lblruta.Text = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/" + HttpContext.Current.Request.ApplicationPath + "/Page/CajaFin/OperacionCaja/";
        try
        {
            Site toolBar = (Site)Master;
            toolBar.eventoRegresar += btnRegresar_Click;

            if (!IsPostBack)
            {
                ObtenerDatos();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(peopleServicio.GetType().Name + "D", "Page_Load", ex);
        }
    }

    protected void ObtenerDatos()
    {
        long cod_ope = long.Parse(Session[Usuario.codusuario + "codOpe"].ToString());
        Xpinn.Caja.Services.TipoOperacionService tipOpeService = new Xpinn.Caja.Services.TipoOperacionService();
        Xpinn.Caja.Entities.TipoOperacion tipOpe = new Xpinn.Caja.Entities.TipoOperacion();

        try
        {
            Xpinn.Caja.Entities.Persona people = new Xpinn.Caja.Entities.Persona();
            people = peopleServicio.ConsultarEmpresa(people, (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(people.nom_empresa))
                lblEmpresa.Text = people.nom_empresa;
            if (!string.IsNullOrEmpty(people.nit))
                lblNit.Text = people.nit.Trim();
            if (!string.IsNullOrEmpty(people.direccion))
                lblDir.Text = people.direccion;
            if (!string.IsNullOrEmpty(people.telefono))
                lblTel.Text = people.telefono;
            if (!string.IsNullOrEmpty(people.identificacion))
                lblIdentific.Text = people.identificacion;

            // Consultar datos de la operación y listado de movimientos.
            tipOpe.cod_operacion = cod_ope.ToString();
            List<Xpinn.Caja.Entities.TipoOperacion> lstConsulta = new List<Xpinn.Caja.Entities.TipoOperacion>();
            lstConsulta = tipOpeService.ConsultarTranCred(tipOpe, (Usuario)Session["usuario"]);
            Xpinn.Caja.Entities.TipoOperacion operacion = lstConsulta.First();
            lblCajero.Text = operacion.nombre_cajero;
            lblOficina.Text = operacion.nombre_oficina;
            lblCaja.Text = operacion.nombre_caja;
            lblFecha.Text = operacion.fecha_operacion.HasValue ? operacion.fecha_operacion.Value.ToShortDateString() : " ";
            txtObservaciones.Text = operacion.observaciones;

            people.cod_persona = long.Parse(operacion.cod_persona);
            people = peopleServicio.ConsultarPersonaXCodigo(people, (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(people.identificacion))
                lblIdentific.Text = people.identificacion;
            if (!string.IsNullOrEmpty(people.nom_persona))
                lblCliente.Text = people.nom_persona;
            if (!string.IsNullOrEmpty(people.ciudad))
                lblCiudad.Text = people.ciudad;
            if (!string.IsNullOrEmpty(people.identificacion))
                lblIdentific.Text = people.identificacion;

            lblCodOpera.Text = cod_ope.ToString();

            // Consultar el valor en efectivo
            people.cod_ope = cod_ope;
            people = peopleServicio.ConsultarValorEfectivo(people, (Usuario)Session["usuario"]);
            lblEfectivo.Text = people.valor_total_efectivo.ToString("N0");

            // Consultar el valor en cheque
            people.cod_ope = cod_ope;
            people = peopleServicio.ConsultarValorChequeCaja(people, (Usuario)Session["usuario"]);
            lblCheque.Text = people.valor_total_cheques.ToString("N0");

            // Consultar el valor por otras formas de pago
            people.cod_ope = cod_ope;
            people = peopleServicio.ConsultarValorOtros(people, (Usuario)Session["usuario"]);
            lblOtros.Text = people.valor_total_otros.ToString("N0");



            // Consultar numero de boucher 
            Xpinn.Caja.Entities.MovimientoCaja movimiento = new Xpinn.Caja.Entities.MovimientoCaja();

            movimiento.cod_ope = cod_ope;
            movimiento = movimientoServicio.ConsultarBoucher(movimiento, (Usuario)Session["usuario"]);
            if (movimiento.num_documento != null && lblOtros.Text!="0")
            {
                lblBoucher.Visible = true;
                lblnumboucher.Visible = true;
                lblBoucher.Text = movimiento.num_documento.ToString();
            }
            
            // Consultar Cheques            
            List<Xpinn.Caja.Entities.MovimientoCaja> lstCheques = new List<Xpinn.Caja.Entities.MovimientoCaja>();
            lstCheques = movimientoServicio.ListarChequesRecibidos(Convert.ToInt64(tipOpe.cod_operacion), (Usuario)Session["usuario"]);
            if (lstConsulta != null)
            {
                if (lstConsulta.Count > 0)
                {
                    gvCheques.Visible = true;
                    gvCheques.DataSource = lstCheques;
                    gvCheques.DataBind();
                }
            }

            else
            {
                gvCheques.Visible = true;
            }



            // Mostrar el detalle de las transacciones                
            decimal acum = 0;
            gvDetalle.Visible = true;
            gvDetalle.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvDetalle.Visible = true;
                gvDetalle.DataBind();
            }
            else
            {
                gvDetalle.Visible = false;
            }

            // Despues de llenado la grilla de detalles me interesa sumar los datos de valores
            foreach (GridViewRow fila in gvDetalle.Rows)
            {
                decimal valor = 0;
                decimal.TryParse(fila.Cells[3].Text, out valor);
                acum += valor;
            }

            // Determinar el valor del IVA
            tipOpe.cod_operacion = cod_ope.ToString();
            tipOpe = tipOpeService.ConsultarValIva(tipOpe, (Usuario)Session["usuario"]);

            // Determinar totales y subtotales
            lblSubTotal.Text = acum.ToString("N0");
            lblBaseIva.Text = tipOpe.valor_base.ToString("N0");
            lblIva.Text = tipOpe.valor_iva.ToString("N0");

            decimal valTotal = 0;
            decimal valIva = 0;
            decimal subTotal = 0;

            subTotal = !string.IsNullOrWhiteSpace(lblSubTotal.Text) ? 0 : decimal.Parse(lblSubTotal.Text);
            valIva = !string.IsNullOrWhiteSpace(lblIva.Text) ? 0 : decimal.Parse(lblIva.Text);
            valTotal = subTotal + valIva;
            lblTotal.Text = valTotal.ToString("N0");

            // Determinar el número de factura
            tipOpe.cod_operacion = cod_ope.ToString();
            if (Session["vengoDeTesoreria"] != null && (bool)Session["vengoDeTesoreria"])
            {
                tipOpe.num_factura = tipOpeService.ConsultarFactura(Convert.ToInt64(tipOpe.cod_operacion), true, (Usuario)Session["usuario"]);
            }
            else
            {
                tipOpe = tipOpeService.InsertarFactura(tipOpe, (Usuario)Session["usuario"]);
                tipOpe.num_factura = tipOpeService.ConsultarFactura(Convert.ToInt64(tipOpe.cod_operacion), true, (Usuario)Session["usuario"]);
            }
            lblFactura.Text = tipOpe.num_factura;
            fechahoy.Text = Convert.ToString(DateTime.Now);

            //RECUPERAR  parametro general
            Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
            Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
            pData = ConsultaData.ConsultarGeneral(19, (Usuario)Session["usuario"]);
            Int64 parametro = Convert.ToInt64(pData.valor);

            if (parametro == 1)
            {
                lblsaldos.Visible = false;
                // Consultar datos de saldos de productos
                gvSaldos.Visible = false;
                List<Xpinn.Caja.Entities.TipoOperacion> lstProductos = new List<Xpinn.Caja.Entities.TipoOperacion>();
                lstProductos = tipOpeService.ConsultarSaldoProductos(Convert.ToInt64(tipOpe.cod_operacion), (Usuario)Session["usuario"]);
                if (lstConsulta != null)
                {
                    if (lstConsulta.Count > 0)
                    {
                        gvSaldos.Visible = true;
                        lblsaldos.Visible = true;
                        gvSaldos.DataSource = lstProductos;
                        gvSaldos.DataBind();
                    }
                }
            }
            else
            {
                gvSaldos.Visible = true;
            }
        }
        catch (Exception ex)
        {
            VerError("No se pudo determinar datos de la operación, Error:" + ex.Message);
        }
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["vengoDeTesoreria"] != null && (bool)Session["vengoDeTesoreria"])
        {
            Navegar("~/Page/Tesoreria/AnulacionOperaciones/Nuevo.aspx");
        }
        else
        {
            Navegar("~/Page/CajaFin/OperacionCaja/Nuevo.aspx");
        }
    }


}