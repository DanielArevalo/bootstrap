using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Services;
using Xpinn.Caja.Entities;
using Xpinn.Caja.Services;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Configuration;
using System.Collections;
using Xpinn.Contabilidad.Entities;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.Drawing.Printing;
using System.Web.Script.Services;

public partial class Nuevo : GlobalWeb
{
    PoblarListas poblar = new PoblarListas();
    
    private Xpinn.Caja.Services.TransaccionCajaService tranCajaServicio = new Xpinn.Caja.Services.TransaccionCajaService();
    private Xpinn.Caja.Entities.TransaccionCaja tranCaja = new Xpinn.Caja.Entities.TransaccionCaja();

    private Xpinn.Caja.Services.ReintegroService reintegroService = new Xpinn.Caja.Services.ReintegroService();
    private Xpinn.Caja.Entities.Reintegro reintegro = new Xpinn.Caja.Entities.Reintegro();

    private Xpinn.Caja.Services.HorarioOficinaService HorarioService = new Xpinn.Caja.Services.HorarioOficinaService();
    private Xpinn.Caja.Entities.HorarioOficina horario = new Xpinn.Caja.Entities.HorarioOficina();

    private Xpinn.Caja.Services.CajeroService cajeroService = new Xpinn.Caja.Services.CajeroService();
    private Xpinn.Caja.Entities.Cajero cajero = new Xpinn.Caja.Entities.Cajero();

    private Usuario user = new Usuario();
    private Int16 nActiva = 0;
    private DateTime fechacaja;
    private DateTime fechacajacierre;
    private DateTime fechadia;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(tranCajaServicio.CodigoProgramaGirMoneda, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tranCajaServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {

                fechadia = DateTime.Now;

                txtFechaTransaccion.Text = reintegro.fechareintegro.ToString(gFormatoFecha);

                txttransacciondia.Text = fechadia.ToString(gFormatoFecha);
                fechadia = Convert.ToDateTime(txttransacciondia.Text);

                bancochquevacio.Text = "";
                numchequevacio.Text = "";
                valorchequevacio.Text = "";
                string ip = Request.ServerVariables["REMOTE_ADDR"];

                ObtenerDatos();
                user = (Usuario)Session["usuario"];
                cajero = cajeroService.ConsultarIfUserIsntCajeroPrincipal(user.codusuario, (Usuario)Session["usuario"]);
                Session["estadoCaj"] = cajero.estado;//estado Cajero
                Session["estadoOfi"] = cajero.estado_ofi;// estado Oficina
                Session["estadoCaja"] = cajero.estado_caja;// estado Caja

                horario = HorarioService.VerificarHorarioOficina(user.cod_oficina, (Usuario)Session["usuario"]);
                Session["conteoOfiHorario"] = horario.conteo;

                horario = HorarioService.getHorarioOficina(user.cod_oficina, (Usuario)Session["usuario"]);

                Session["Resp1"] = 0;
                Session["Resp2"] = 0;
                
                //si la hora actual es mayor que de la hora inicial
                if (TimeSpan.Compare(horario.fecha_hoy.TimeOfDay, horario.hora_inicial.TimeOfDay) > 0)
                    Session["Resp1"] = 1;
                //si la hora actual es menor que la hora final
                if (TimeSpan.Compare(horario.fecha_hoy.TimeOfDay, horario.hora_final.TimeOfDay) < 0)
                    Session["Resp2"] = 1;

                if (long.Parse(Session["estadoOfi"].ToString()) == 2)
                    VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");
                else
                {
                    if (long.Parse(Session["estadoCaja"].ToString()) == 0)
                        VerError("La Caja se encuentra cerrada y no puede realizar Operaciones");
                    else
                    {
                        if (long.Parse(Session["conteoOfiHorario"].ToString()) == 0)
                            VerError("La Oficina no cuenta con un horario establecido para el día de hoy");
                        else
                        {
                            if (long.Parse(Session["Resp1"].ToString()) == 1 && long.Parse(Session["Resp2"].ToString()) == 1)
                            {
                                if (long.Parse(Session["estadoCaj"].ToString()) == 0)
                                    VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
                            }
                            else
                                VerError("La Oficina se encuentra por fuera del horario configurado. Dia: " + HorarioService.getDiaHorarioOficina(user.cod_oficina, (Usuario)Session["usuario"]));
                        }
                    }
                }

                AsignarEventoConfirmar();
                CargarDropDown();

                // Crea los DATATABLES para registrar las transacciones, los cheques
                CrearTablaCheque();
                CrearTablaFormaPago();
                panelFormaPago.Visible = false;
                rblRegistro_SelectedIndexChanged(rblRegistro, null);
            }            
           
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tranCajaServicio.GetType().Name + "A", "Page_Load", ex);
        }

    }


    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnGuardar"), "Desea Aplicar los Pagos?");
    }


    protected void CargarDropDown()
    {
        poblar.PoblarListaDesplegable("TIPOIDENTIFICACION", ddlTipoIdentificacion, (Usuario)Session["usuario"]);
        poblar.PoblarListaDesplegable("BANCOS", "", "", "2", ddlBancos, (Usuario)Session["usuario"]);
        poblar.PoblarListaDesplegable("OFICINA", "COD_OFICINA,NOMBRE", " ESTADO = 1", "2", ddlOficinaEntrega, (Usuario)Session["usuario"]);
        Xpinn.Caja.Services.TipoMonedaService monedaService = new Xpinn.Caja.Services.TipoMonedaService();
        Xpinn.Caja.Entities.TipoMoneda moneda = new Xpinn.Caja.Entities.TipoMoneda();
        Usuario usuario = (Usuario)Session["usuario"];
        ddlMoneda.DataSource = monedaService.ListarTipoMoneda(moneda, usuario);
        ddlMoneda.DataTextField = "descripcion";
        ddlMoneda.DataValueField = "cod_moneda";
        ddlMoneda.DataBind();

        ddlMonCheque.DataSource = monedaService.ListarTipoMoneda(moneda, usuario);
        ddlMonCheque.DataTextField = "descripcion";
        ddlMonCheque.DataValueField = "cod_moneda";
        ddlMonCheque.DataBind();

        ddlMonedaEnvio.DataSource = monedaService.ListarTipoMoneda(moneda, usuario);
        ddlMonedaEnvio.DataTextField = "descripcion";
        ddlMonedaEnvio.DataValueField = "cod_moneda";
        ddlMonedaEnvio.DataBind();        

        Xpinn.Caja.Services.TipoPagoService pagoService = new Xpinn.Caja.Services.TipoPagoService();
        Xpinn.Caja.Entities.TipoPago paguei = new Xpinn.Caja.Entities.TipoPago();
        ddlFormaPago.DataSource = pagoService.ListarTipoPago(paguei, usuario);
        ddlFormaPago.DataTextField = "descripcion";
        ddlFormaPago.DataValueField = "cod_tipo_pago";
        ddlFormaPago.DataBind();
    }


    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodPersona", "txtIdentificacion", "ddlTipoIdentificacion", "txtNombreCliente");
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (long.Parse(Session["estadoOfi"].ToString()) == 1)
        {
            if (long.Parse(Session["estadoCaja"].ToString()) == 1)
            {
                if (long.Parse(Session["conteoOfiHorario"].ToString()) == 1)
                {
                    if (long.Parse(Session["Resp1"].ToString()) == 1 && long.Parse(Session["Resp2"].ToString()) == 1)
                    {
                        if (long.Parse(Session["estadoCaj"].ToString()) == 1)
                        {
                            if (txtIdentificacion.Text != null && !string.Equals(txtIdentificacion.Text, ""))
                            {
                                Xpinn.Caja.Services.PersonaService personaService = new Xpinn.Caja.Services.PersonaService();
                                Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();

                                persona.identificacion = txtIdentificacion.Text;
                                if (ddlTipoIdentificacion.SelectedItem != null && ddlTipoIdentificacion.SelectedIndex != 0)
                                    persona.tipo_identificacion = long.Parse(ddlTipoIdentificacion.SelectedValue);
                                VerError("");
                                persona = personaService.ConsultarPersona(persona, (Usuario)Session["usuario"]);

                                if (persona.mensajer_error == "")
                                {
                                    txtCodPersona.Text = persona.cod_persona.ToString();
                                    Session["codpersona"] = persona.cod_persona;
                                    txtNombreCliente.Text = persona.nom_persona;
                                    if (persona.tipo_identificacion != 0)
                                        ddlTipoIdentificacion.SelectedValue = persona.tipo_identificacion.ToString();
                                    // aqui se coloca los datos de la persona, Nro Radicacion, Nombre, Valor CUota, saldo capital
                                    if(rblRegistro.SelectedValue == "E")
                                        Actualizar();
                                }
                                else
                                {
                                    VerError(persona.mensajer_error);
                                    txtCodPersona.Text = "";
                                    txtNombreCliente.Text = "";
                                    txtIdentificacion.Text = "";
                                }
                            }
                        }
                        else
                            VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
                    }
                    else
                        VerError("La Oficina se encuentra por fuera del horario configurado");
                }
                else
                    VerError("La Oficina no cuenta con un horario establecido para el día de hoy");
            }
            else
                VerError("La Caja se encuentra cerrada y no puede realizar Operaciones");
        }
        else
            VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");
    }


    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("../../../General/Global/inicio.aspx");
    }


    protected void rblRegistro_SelectedIndexChanged(object sender, EventArgs e)
    {
        panelEgreso.Visible = false;
        panelIngreso.Visible = false;
        if (rblRegistro.SelectedValue == "I")
            panelIngreso.Visible = true;
        else
            panelEgreso.Visible = true;
    }

    /// <summary>
    /// Muestra los datos iniciales en pantalla
    /// </summary>
    protected void ObtenerDatos()
    {     
        try
        {
            reintegro = reintegroService.ConsultarCajero((Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(reintegro.fechareintegro.ToString()))
            {
                txtFechaTransaccion.Text = reintegro.fechareintegro.ToString();
                txttransacciondia.Text = reintegro.fechareintegro.ToString(gFormatoFecha);                                                 
            }          
         
            if (!string.IsNullOrEmpty(reintegro.nomoficina))
                txtOficina.Text = reintegro.nomoficina.ToString();
            if (!string.IsNullOrEmpty(reintegro.nomcaja))
                txtCaja.Text = reintegro.nomcaja.ToString();
            if (!string.IsNullOrEmpty(reintegro.nomcajero))
                txtCajero.Text = reintegro.nomcajero.ToString().Trim();
            if (!string.IsNullOrEmpty(reintegro.cod_oficina.ToString()))
                Session["Oficina"] = reintegro.cod_oficina.ToString().Trim();
            if (!string.IsNullOrEmpty(reintegro.cod_caja.ToString()))
                Session["Caja"] = reintegro.cod_caja.ToString().Trim();
            if (!string.IsNullOrEmpty(reintegro.cod_cajero.ToString()))
                Session["Cajero"] = reintegro.cod_cajero.ToString().Trim();
            ObtenerDatosUltCierre();

            if (fechacajacierre < fechadia)
            {
                VerError("No se ha generado APERTURA del día, no puede timbrar operaciones con esta fecha");
                ddlTipoIdentificacion.Enabled = false;
                txtIdentificacion.Enabled = false;
                btnConsultar.Enabled = false;
                txtMontoGiro.Enabled = false;
                rblRegistro.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tranCajaServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    protected void ObtenerDatosUltCierre()
    {
        try
        {
            reintegro = reintegroService.ConsultarFecUltCierre((Usuario)Session["usuario"]);
            if (!string.IsNullOrEmpty(reintegro.fechaarqueo.ToString()))
            {
                fechacajacierre = reintegro.fechaarqueo;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tranCajaServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    
    protected void CrearTablaFormaPago()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("moneda");
        dt.Columns.Add("fpago");
        dt.Columns.Add("valor");
        dt.Columns.Add("nommoneda");
        dt.Columns.Add("nomfpago");
        dt.Columns.Add("tipomov");

        foreach (ListItem monedaList in ddlMoneda.Items)
        {
            foreach (ListItem formaPagoList in ddlFormaPago.Items)
            {
                gvFormaPago.Visible = true;
                DataRow fila = dt.NewRow();
                fila[0] = monedaList.Value;
                fila[1] = formaPagoList.Value;
                fila[2] = 0;
                fila[3] = monedaList.Text;
                fila[4] = formaPagoList.Text;
                fila[5] = 0;

                dt.Rows.Add(fila);
            }
        }

        gvFormaPago.DataSource = dt;
        gvFormaPago.DataBind();
        Session["tablaSesion2"] = dt;
    }

    protected void CrearTablaCheque()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("numcheque");
        dt.Columns.Add("entidad");
        dt.Columns.Add("valor");
        dt.Columns.Add("moneda");
        dt.Columns.Add("nommoneda");
        dt.Columns.Add("nomentidad");
        gvCheques.DataSource = dt;
        gvCheques.DataBind();
        gvCheques.Visible = false;
        Session["tablaSesion3"] = dt;
    }

    protected void LlenarTablaFormaPago(int formapago, int moneda, decimal valEfectivo)
    {
        DataTable dtAgre2 = new DataTable();
        dtAgre2 = (DataTable)Session["tablaSesion2"];
        decimal acum = 0;

        Int64 tipoMov = 0;
        //EGRESO => 1 INGRESO => 2
        tipoMov = rblRegistro.SelectedValue == "E" ? 1 : 2;

        //se trata de localizar el registro que se hace necesario actualizar
        foreach (DataRow fila in dtAgre2.Rows)
        {
            if (long.Parse(fila[0].ToString()) == moneda && long.Parse(fila[1].ToString()) == formapago)
            {
                fila[2] = decimal.Parse(fila[2].ToString()) + valEfectivo;
                fila[5] = tipoMov;
            }

            acum = acum + decimal.Parse(fila[2].ToString());
            fila[5] = tipoMov;
        }

        gvFormaPago.DataSource = dtAgre2;
        gvFormaPago.DataBind();
        Session["tablaSesion2"] = dtAgre2;

        decimal valFormaPagoTotal = 0;

        valFormaPagoTotal = txtValTotalFormaPago.Text == "" ? 0 : decimal.Parse(txtValTotalFormaPago.Text);
        txtValTotalFormaPago.Text = acum.ToString();

    }

    /// <summary>
    /// En este métodos se cargan el valor del cheque registrado a la grilla de cheques validando
    /// que el valor no excede el valor total en cheques.
    /// </summary>
    /// <returns></returns>
    protected long LlenarFormaPago3()
    {
        DataTable dtAgre4 = new DataTable();
        dtAgre4 = (DataTable)Session["tablaSesion2"];
        long moneda = long.Parse(ddlMonCheque.SelectedValue);
        decimal ValorCheque = decimal.Parse(txtValCheque.Text.Replace(".", ""));
        decimal acum = 0;
        long result = 0;

        foreach (DataRow fila in dtAgre4.Rows)
        {
            if (long.Parse(fila[0].ToString()) == moneda && long.Parse(fila[1].ToString()) == 1)
            {
                //se valida que el valor de la forma de pago sea mayor que el valor de cheque
                if (decimal.Parse(fila[2].ToString()) >= ValorCheque)
                {
                    fila[2] = decimal.Parse(fila[2].ToString()) - ValorCheque;
                }
                else
                {
                    result = 1;
                }

            }

            if (result == 0)
                acum = acum + decimal.Parse(fila[2].ToString());
        }

        if (result == 0)
        {
            gvFormaPago.DataSource = dtAgre4;
            gvFormaPago.DataBind();
            Session["tablaSesion2"] = dtAgre4;

            decimal valFormaPagoTotal = 0;

            valFormaPagoTotal = txtValTotalFormaPago.Text == "" ? 0 : decimal.Parse(txtValTotalFormaPago.Text);
            txtValTotalFormaPago.Text = acum.ToString();
        }
        return result;
    }

    protected long LlenarFormaPago2()// este es el metodo que suma 
    {
        DataTable dtAgre2 = new DataTable();
        dtAgre2 = (DataTable)Session["tablaSesion2"];

        long result = 0;
        long moneda = long.Parse(ddlMoneda.SelectedValue);
        long formapago = long.Parse(ddlFormaPago.SelectedValue);
        decimal valorFormaPago = decimal.Parse(txtValor.Text.Replace(".", ""));
        decimal acum = 0;

        if (formapago != 1 && formapago != 2)// se valida que no se incerten cambios en Forma de Pagos en Efectivo y Cheque
        {
            //se trata de localizar el registro que se hace necesario actualizar
            foreach (DataRow fila in dtAgre2.Rows)
            {
                if (long.Parse(fila[0].ToString()) == moneda && long.Parse(fila[1].ToString()) == formapago)
                {
                    fila[2] = decimal.Parse(fila[2].ToString()) + valorFormaPago;
                }

                acum = acum + decimal.Parse(fila[2].ToString());
            }

            gvFormaPago.DataSource = dtAgre2;
            gvFormaPago.DataBind();
            Session["tablaSesion2"] = dtAgre2;

            decimal valFormaPagoTotal = 0;

            valFormaPagoTotal = txtValTotalFormaPago.Text == "" ? 0 : decimal.Parse(txtValTotalFormaPago.Text);
            txtValTotalFormaPago.Text = acum.ToString();
        }
        else
        {
            result = 1;
        }
        return result;
    }

    protected long LlenarFormaPago5()
    {
        DataTable dtAgre5 = new DataTable();
        dtAgre5 = (DataTable)Session["tablaSesion2"];
        long moneda = long.Parse(ddlMoneda.SelectedValue);
        decimal ValorForma = decimal.Parse(txtValor.Text.Replace(".", ""));
        long formapago = long.Parse(ddlFormaPago.SelectedValue);
        decimal acum = 0;
        long result = 0;

        if (formapago != 1 && formapago != 2)
        {
            foreach (DataRow fila in dtAgre5.Rows)
            {
                if (long.Parse(fila[0].ToString()) == moneda && long.Parse(fila[1].ToString()) == 1)
                {
                    //se valida que el valor de la forma de pago(grilla) sea mayor que el valor de forma de pago( textbox)
                    if (decimal.Parse(fila[2].ToString()) >= ValorForma)
                    {
                        fila[2] = decimal.Parse(fila[2].ToString()) - ValorForma;
                    }
                    else
                    {
                        result = 1;
                    }

                }

                if (result == 0)
                    acum = acum + decimal.Parse(fila[2].ToString());
            }

            if (result == 0)
            {
                gvFormaPago.DataSource = dtAgre5;
                gvFormaPago.DataBind();
                Session["tablaSesion2"] = dtAgre5;

                decimal valFormaPagoTotal = 0;

                valFormaPagoTotal = txtValTotalFormaPago.Text == "" ? 0 : decimal.Parse(txtValTotalFormaPago.Text);
                txtValTotalFormaPago.Text = acum.ToString();
            }
        }
        else
        {
            result = 1;
        }

        return result;
    }

    
    protected void LlenarTablaCheque()
    {
        gvCheques.Visible = true;
        DataTable dtAgre3 = new DataTable();
        dtAgre3 = (DataTable)Session["tablaSesion3"];
        DataRow fila = dtAgre3.NewRow();
        fila[0] = txtNumCheque.Text;
        fila[1] = ddlBancos.SelectedValue;
        fila[2] = txtValCheque.Text.Replace(".", "");
        fila[3] = ddlMonCheque.SelectedValue;
        fila[4] = ddlMonCheque.SelectedItem.Text;
        fila[5] = ddlBancos.SelectedItem.Text;

        dtAgre3.Rows.Add(fila);
        gvCheques.DataSource = dtAgre3;
        gvCheques.DataBind();
        Session["tablaSesion3"] = dtAgre3;

        decimal valTotal = 0;
        decimal valCheque = 0;

        valCheque = txtValCheque.Text == "" ? 0 : decimal.Parse(txtValCheque.Text.Replace(".",""));
        valTotal = txtValTotalCheque.Text == "" ? 0 : decimal.Parse(txtValTotalCheque.Text);

        valTotal = valTotal + valCheque;
        txtValTotalCheque.Text = valTotal.ToString();

        int moneda = Convert.ToInt32(ddlMonCheque.SelectedValue);
        LlenarTablaFormaPago(2, moneda, valCheque);
    }



    protected void btnGoFormaPago_Click(object sender, EventArgs e)
    {
        decimal valor = 0;
        valor = txtValor.Text == "" ? 0 : decimal.Parse(txtValor.Text.Replace(".", ""));
        long result = 0;
        long result2 = 0;

        if (valor > 0)
        {
            result = LlenarFormaPago5();

            if (result == 0)
            {

                result2 = LlenarFormaPago2();

                if (result2 == 1)
                    VerError("No se puede actualizar los valore de Forma de Pago Efectivo o Cheques, estos deben ser ingresados desde los paneles de Ingreso de Cada Uno");
            }
            else
                VerError("El Valor de la Forma de Pago debe ser menor al valor Efectivo");
        }
        else
            VerError("El Valor de Forma de Pago debe ser Mayor a Cero");
    }

    protected void btnGoCheque_Click(object sender, EventArgs e)
    {
        int control = 0;

        if (txtNumCheque.Text == "")
        {
            numchequevacio.Text = "Ingrese un Número de Cheque";
            control = 1;
        }

        if (txtValCheque.Text == "")
        {
            valorchequevacio.Text = "Ingrese el Valor";
            control = 1;
        }

        if (ddlBancos.SelectedIndex == 0)
        {
            bancochquevacio.Text = "Seleccione un Banco";
            control = 1;
        }

        if (control != 1)
        {

            decimal valor = 0;
            valor = txtValCheque.Text == "" ? 0 : decimal.Parse(txtValCheque.Text.Replace(".", ""));
            long result = 0;

            if (valor > 0)
            {
                bancochquevacio.Text = "";
                numchequevacio.Text = "";
                valorchequevacio.Text = "";
                result = LlenarFormaPago3();

                if (result == 0)
                    LlenarTablaCheque();
                else
                    VerError("El Valor del Cheque no puede ser Superior al Valor de Efectivo");
            }
            else
                VerError("El Valor de Forma de Pago debe ser Mayor a Cero");
        }
    }

    protected Boolean validarRegistro()
    {
        if (rblRegistro.SelectedValue == "I")
        {
            if (txtMontoGiro.Text == "" || txtMontoGiro.Text == "0")
            {
                VerError("Ingrese el valor de Giro");
                return false;
            }
            if (txtIdentificEnvio.Text.Trim() == "" || txtNomApe.Text.Trim() == "")
            {
                VerError("Ingrese la identificación y el nombre completo de la persona a reclamar");
                return false;
            }

        }
        return true;
    }

    protected Boolean ValidarEntrega()
    {
        if (rblRegistro.SelectedValue == "E")
        {
            //VALIDAR SI EXISTE SALDO EN LA CAJA PARA REALIZAR LA ENTREGA
            Xpinn.Caja.Entities.SaldoCaja saldo = new Xpinn.Caja.Entities.SaldoCaja();
            Xpinn.Caja.Services.SaldoCajaService saldoService = new Xpinn.Caja.Services.SaldoCajaService();
            saldo.cod_caja = long.Parse(Session["Caja"].ToString());
            saldo.cod_cajero = long.Parse(Session["Cajero"].ToString());
            saldo.tipo_moneda = 1;
            saldo.fecha = Convert.ToDateTime(txttransacciondia.Text);

            saldo = saldoService.ConsultarSaldoCaja(saldo, (Usuario)Session["usuario"]);
            if (saldo.valor <= 0)
            {
                VerError("La Caja no tiene Dinero disponible para realizar esta Operación");
                return false;
            }
            else
            {
                decimal valorTotal = Convert.ToDecimal(txtValTotalFormaPago.Text.Replace("$", "").Replace(".", ""));
                if (valorTotal > saldo.valor)
                {
                    VerError("La Caja no tiene Dinero disponible para realizar esta Operación");
                    return false;
                }
            }

            int cont = 0;
            if (gvGiros.Rows.Count == 0 || gvGiros == null)
            {
                VerError("No existen registros por entregar");
                return false;
            }
            foreach (GridViewRow rFila in gvGiros.Rows)
            {
                CheckBoxGrid cbSeleccionar = (CheckBoxGrid)rFila.FindControl("cbSeleccionar");
                if (cbSeleccionar != null)
                {
                    if (cbSeleccionar.Checked)
                        cont++;
                }
            }
            if (cont == 0)
            {
                VerError("Seleccione un giro a entregar.");
                return false;
            }
            decimales txtMontoTotalGiro = (decimales)gvGiros.FooterRow.FindControl("txtMontoTotalGiro");
            if (txtMontoTotalGiro != null)
            {
                if (txtMontoTotalGiro.Text.Trim() == "" || txtMontoTotalGiro.Text.Trim() == "0")
                {
                    VerError("El valor a entregar no puede ser 0 - Verifique los datos generados.");
                    return false;
                }
            }
        }
        return true;
    }


    protected Boolean validarGuardar()
    {
        if (txtCodPersona.Text == "" || txtIdentificacion.Text.Trim() == "")
        {
            VerError("Ingrese la persona que está generando el Giro.");
            return false;
        }
        if (ddlTipoIdentificacion.SelectedIndex == 0)
        {
            VerError("Seleccione el tipo de identificación de la persona que está generando el Giro.");
            return false;
        }
        validarRegistro();
        ValidarEntrega();
        
        return true;
    }

    /// <summary>
    /// Mètodo para aplicar las transacciones registradas segùn las formas de pago
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        string Error = "";
        VerError("");
        if (long.Parse(Session["estadoOfi"].ToString()) == 1)
        {
            if (long.Parse(Session["estadoCaja"].ToString()) == 1)
            {
                if (long.Parse(Session["conteoOfiHorario"].ToString()) == 1)
                {
                    if (long.Parse(Session["Resp1"].ToString()) == 1 && long.Parse(Session["Resp2"].ToString()) == 1)
                    {
                        if (long.Parse(Session["estadoCaj"].ToString()) == 1)
                        {

                            decimal ValTotalFPago = 0;
                            decimal ValTotalTran = 0;

                            decimales txtMontoTotalGiro = (decimales)gvGiros.FooterRow.FindControl("txtMontoTotalGiro");
                            ValTotalTran = rblRegistro.SelectedValue == "I" ? Convert.ToDecimal(txtMontoGiro.Text.Replace(".", "")) : Convert.ToDecimal(txtMontoTotalGiro.Text.Replace(".", ""));

                            bool Rpta = true;
                            if (panelFormaPago.Visible == false)
                                Rpta = false;
                            if (Rpta == false)
                            {
                                validarRegistro();
                                ValidarEntrega();

                                if (ValTotalTran == 0)
                                    return;
                                
                                int moneda = Convert.ToInt32(ddlMonedaEnvio.SelectedValue);
                                LlenarTablaFormaPago(1, moneda, ValTotalTran);

                                panelFormaPago.Visible = true;
                            }
                            else
                            {
                                try
                                {
                                    validarGuardar();

                                    ValTotalFPago = txtValTotalFormaPago.Text == "" ? 0 : decimal.Parse(txtValTotalFormaPago.Text);// valor total de Forma de Pago
                                    if (ValTotalFPago == 0 && ValTotalTran == 0)
                                    {
                                        VerError("Debe especificar los valores a pagar");
                                        return;
                                    }
                                    if (ValTotalFPago == ValTotalTran)// si son iguales en valor entonces deja guardar
                                    {
                                        if (Session["codpersona"] == null)
                                            BuscarPersona();
                                        tranCaja.cod_persona = long.Parse(Session["codpersona"].ToString());
                                        Session["PersonActive"] = tranCaja.cod_persona;
                                        tranCaja.cod_caja = long.Parse(Session["Caja"].ToString());
                                        Session["CajaActive"] = txtCaja.Text;
                                        tranCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
                                        Session["CajeroActive"] = txtCajero.Text;
                                        tranCaja.cod_oficina = long.Parse(Session["Oficina"].ToString());
                                        Session["OficinaActive"] = txtOficina.Text;
                                        tranCaja.fecha_cierre = Convert.ToDateTime(txtFechaTransaccion.Text);
                                        if (txtMontoGiro.Text != "" && txtMontoGiro.Text != "0")
                                            tranCaja.valor_pago = decimal.Parse(txtMontoGiro.Text);
                                        else
                                            tranCaja.valor_pago = 0;
                                        tranCaja.tipo_ope = 120; //PENDIENTE
                                        tranCaja.tipo_movimiento = rblRegistro.SelectedValue;

                                        //GIRO MONEDA

                                        List<GiroMoneda> lstGiros = new List<GiroMoneda>();
                                        if (rblRegistro.SelectedValue == "I")
                                        {
                                            GiroMoneda pEntidad = new GiroMoneda();
                                            pEntidad.cod_persona = Convert.ToInt64(txtCodPersona.Text);
                                            pEntidad.valor = ValTotalFPago;
                                            pEntidad.cod_moneda = Convert.ToInt32(ddlMonedaEnvio.SelectedValue);
                                            pEntidad.cod_oficina_recibe = Convert.ToInt32(ddlOficinaEntrega.SelectedValue);
                                            pEntidad.identificacion = txtIdentificEnvio.Text.Trim();
                                            pEntidad.nombre = txtNomApe.Text.Trim().ToUpper();
                                            pEntidad.observaciones = txtObservacion.Text.Trim() != "" ? txtObservacion.Text.Trim() : null;
                                            int estado = rblRegistro.SelectedValue == "I" ? 0 : 1;
                                            pEntidad.estado = estado;
                                            pEntidad.cod_usuario = Convert.ToInt32(user.codusuario);
                                            lstGiros.Add(pEntidad);
                                        }
                                        else
                                        {
                                            foreach (GridViewRow rFila in gvGiros.Rows)
                                            {
                                                CheckBoxGrid cbSeleccionar = (CheckBoxGrid)rFila.FindControl("cbSeleccionar");
                                                if (cbSeleccionar != null && cbSeleccionar.Checked)
                                                {
                                                    GiroMoneda pEntidad = new GiroMoneda();
                                                    Int64 pidGiroMoneda = Convert.ToInt64(gvGiros.DataKeys[rFila.RowIndex].Value.ToString());
                                                    decimales txtMontoGiroGrid = (decimales)rFila.FindControl("txtMontoGiro");

                                                    pEntidad.idgiromoneda = pidGiroMoneda;
                                                    pEntidad.valor = Convert.ToDecimal(txtMontoGiroGrid.Text.Replace(".", ""));
                                                    pEntidad.estado = 1;
                                                    if (pEntidad.valor != 0 && pEntidad.idgiromoneda != 0)
                                                        lstGiros.Add(pEntidad);
                                                }
                                            }
                                        }

                                        tranCajaServicio.CrearTransaccionGiroMoneda(tranCaja, lstGiros, gvFormaPago, gvCheques, (Usuario)Session["usuario"], ref Error);
                                        
                                        if (Error.Trim() != "")
                                        {
                                            VerError(Error);
                                            return;
                                        }

                                        Session[Usuario.codusuario + "codOpe"] = tranCaja.cod_ope;
                                        DateTime fecha = Convert.ToDateTime(txtFechaTransaccion.Text);

                                        Session["FechaTran"] = fecha.ToShortDateString();

                                        Session["tablaSession"] = Session["tablaSesion"];

                                        Navegar("Factura.aspx");

                                    }
                                    else
                                    {
                                        VerError("El Valor Total de Transacción debe ser igual al Valor Total de Transacción distribuida en Formas de Pago");
                                    }

                                }
                                catch (ExceptionBusiness ex)
                                {
                                    VerError(ex.Message);
                                }
                                catch (Exception ex)
                                {
                                    BOexcepcion.Throw(tranCajaServicio.GetType().Name + "A", "btnGuardar_Click", ex);
                                }
                               
                            }
                        }
                        else
                            VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
                    }
                    else
                        VerError("La Oficina se encuentra por fuera del horario configurado");
                }
                else
                    VerError("La Oficina no cuenta con un horario establecido para el día de hoy");
            }
            else
                VerError("La Caja se encuentra cerrada y no puede realizar Operaciones");
        }
        else
            VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");
    }


    protected bool validarOpcion()
    {
        if (txtCodPersona.Text == "")
        {
            VerError("Ingrese una Persona - Datos del Cliente");
            return false;
        }
        return true;
    }

    protected string obtFiltro()
    {
        string Filtro = string.Empty;
        if (txtCodPersona.Text.Trim() != "" && txtIdentificacion.Text.Trim() != "" && ddlTipoIdentificacion.SelectedIndex != 0)
            Filtro += "AND G.COD_PERSONA = " + txtCodPersona.Text.Trim();

        Filtro += "AND G.ESTADO = 0"; 
        if (string.IsNullOrEmpty(Filtro))
        {
            Filtro = Filtro.Substring(4);
            Filtro = " WHERE " + Filtro;
        }
        return Filtro;
    }

    private void Actualizar()
    {
        try
        {
            VerError("");
            List<GiroMoneda> lstGiros = new List<GiroMoneda>();
            string pFiltro = obtFiltro();
            lstGiros = tranCajaServicio.ListarGiroMoneda(pFiltro, (Usuario)Session["usuario"]);

            gvGiros.DataSource = lstGiros;
            if (lstGiros.Count > 0)
            {
                gvGiros.Visible = true;
                lblInfo.Visible = false;
                lblTotalReg.Text = "<br /> Registros encontrados: " + lstGiros.Count;
                gvGiros.DataBind();
            }
            else
            {
                gvGiros.Visible = true;
                lblTotalReg.Visible = false;
                lblInfo.Visible = true;
            }
            Session.Add(tranCajaServicio.CodigoProgramaGirMoneda + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tranCajaServicio.CodigoProgramaGirMoneda + "L", "Actualizar", ex);
        }
    }

    protected void cbSeleccionar_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBoxGrid cbSeleccionar = (CheckBoxGrid)sender;
            int rowIndex = Convert.ToInt32(cbSeleccionar.CommandArgument);

            CalcularTotal();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void CalcularTotal()
    {
        decimal pTotal = 0;
        foreach (GridViewRow rFila in gvGiros.Rows)
        {
            decimales txtMontoGiro = (decimales)rFila.FindControl("txtMontoGiro");
            if (txtMontoGiro != null)
            {
                decimal ValorFila = Convert.ToDecimal(txtMontoGiro.Text.Replace(".", ""));
                pTotal += ValorFila;
            }
        }
        decimales txtMontoTotalGiro = (decimales)gvGiros.FooterRow.FindControl("txtMontoTotalGiro");
        if (txtMontoTotalGiro != null)
        {
            txtMontoTotalGiro.Text = pTotal.ToString("n0"); 
        }
    }


    
    protected void gvCheques_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable table = new DataTable();
            table = (DataTable)Session["tablaSesion3"];//se pilla los Cheques


            DataTable tableFP = new DataTable();
            tableFP = (DataTable)Session["tablaSesion2"];// se pilla las formas de pago
            decimal acum = 0;
            long result = 1;

            foreach (DataRow fila in tableFP.Rows)
            {   // moneda y forma de pago

                if (long.Parse(fila[0].ToString()) == long.Parse(table.Rows[e.RowIndex][3].ToString()) && long.Parse(fila[1].ToString()) == 1)//Efectivo
                {
                    fila[2] = decimal.Parse(fila[2].ToString()) + decimal.Parse(table.Rows[e.RowIndex][2].ToString());
                    result = 0;
                }
                else if (long.Parse(fila[0].ToString()) == long.Parse(table.Rows[e.RowIndex][3].ToString()) && long.Parse(fila[1].ToString()) == 2)//Cheque
                {
                    fila[2] = decimal.Parse(fila[2].ToString()) - decimal.Parse(table.Rows[e.RowIndex][2].ToString());
                    result = 0;
                }
                else if (long.Parse(fila[0].ToString()) == long.Parse(table.Rows[e.RowIndex][3].ToString()) && long.Parse(fila[1].ToString()) != 1 && long.Parse(fila[1].ToString()) != 2)//Otros
                {
                    if (decimal.Parse(fila[2].ToString()) > 0)
                    {
                        fila[2] = decimal.Parse(fila[2].ToString());
                        result = 0;
                    }
                    else
                        result = 1;
                }
                else
                {
                    result = 1;
                }

                if (result == 0)
                    acum = acum + decimal.Parse(fila[2].ToString());
            }

            gvFormaPago.DataSource = tableFP;
            gvFormaPago.DataBind();
            Session["tablaSesion2"] = tableFP;

            decimal valFormaPagoTotal = 0;

            valFormaPagoTotal = txtValTotalFormaPago.Text == "" ? 0 : decimal.Parse(txtValTotalFormaPago.Text);
            txtValTotalFormaPago.Text = acum.ToString();

            txtValTotalCheque.Text = Convert.ToString(decimal.Parse(txtValTotalCheque.Text) - decimal.Parse(table.Rows[e.RowIndex][2].ToString()));

            table.Rows[e.RowIndex].Delete();
            gvCheques.DataSource = table;
            gvCheques.DataBind();
            Session["tablaSesion3"] = table;
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tranCajaServicio.GetType().Name + "L", "gvTransacciones_RowDeleting", ex);
        }
    }
    

    protected void BuscarPersona()
    {
        Xpinn.Caja.Services.PersonaService personaService = new Xpinn.Caja.Services.PersonaService();
        Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();
        persona.identificacion = txtIdentificacion.Text;
        persona.tipo_identificacion = long.Parse(ddlTipoIdentificacion.SelectedValue);
        VerError("");
        persona = personaService.ConsultarPersona(persona, (Usuario)Session["usuario"]);
        if (persona.mensajer_error == "")
        {
            Session["codpersona"] = persona.cod_persona;
            txtNombreCliente.Text = persona.nom_persona;
            // aqui se coloca los datos de la persona, Nro Radicacion, Nombre, Valor CUota, saldo capital
            Actualizar();
        }
        else
        {
            VerError(persona.mensajer_error);
        }
    }
    

}
