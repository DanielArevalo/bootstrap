using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Microsoft.Reporting.WebForms;
public partial class Nuevo : GlobalWeb
{
    Xpinn.Caja.Services.TrasladoService trasladoService = new Xpinn.Caja.Services.TrasladoService();
    Xpinn.Caja.Entities.Traslado traslado = new Xpinn.Caja.Entities.Traslado();
    Xpinn.Caja.Services.MovimientoCajaService movCajaServicio = new Xpinn.Caja.Services.MovimientoCajaService();
    Xpinn.Caja.Entities.MovimientoCaja movCaja = new Xpinn.Caja.Entities.MovimientoCaja();
    Xpinn.Caja.Entities.Cajero cajero = new Xpinn.Caja.Entities.Cajero();
    Xpinn.Caja.Services.CajeroService cajeroService = new Xpinn.Caja.Services.CajeroService();
    Xpinn.Caja.Entities.SaldoCaja saldo = new Xpinn.Caja.Entities.SaldoCaja();
    Xpinn.Caja.Services.SaldoCajaService saldoService = new Xpinn.Caja.Services.SaldoCajaService();
    Xpinn.Caja.Services.HorarioOficinaService HorarioService = new Xpinn.Caja.Services.HorarioOficinaService();
    Xpinn.Caja.Entities.HorarioOficina horario = new Xpinn.Caja.Entities.HorarioOficina();
    List<Xpinn.Caja.Entities.MovimientoCaja> lstConsulta = new List<Xpinn.Caja.Entities.MovimientoCaja>();
    Xpinn.Caja.Entities.ArqueoCaja arqueoCaja = new Xpinn.Caja.Entities.ArqueoCaja();
    Xpinn.Caja.Services.ArqueoCajaService arqueoCajaService = new Xpinn.Caja.Services.ArqueoCajaService();
  
    Usuario _usuario = new Usuario();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(trasladoService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
          
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(trasladoService.GetType().Name + "A", "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["usuario"];
            txtFechaTraslado.Text= Convert.ToString(Session["FechaArqueo"]);
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                cajero = cajeroService.ConsultarIfUserIsntCajeroPrincipal(_usuario.codusuario, _usuario);
                Session["estadoCaj"] = cajero.estado;
                Session["estadoOfi"] = cajero.estado_ofi;// estado Oficina
                Session["estadoCaja"] = cajero.estado_caja;// estado Caja

                Session["CajeroPrincip"] = cajero.conteo; // se verifica si el cajero es principal o no

                horario = HorarioService.VerificarHorarioOficina(_usuario.cod_oficina, _usuario);
                Session["conteoOfiHorario"] = horario.conteo;

                horario = HorarioService.getHorarioOficina(_usuario.cod_oficina, _usuario);

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
                                VerError("La Oficina se encuentra por fuera del horario configurado");
                        }

                    }
                }
                

                //se inicializa el combo de monedas y bancos
                LlenarComboMonedas(ddlMonedas);
                LlenarComboCajeros(ddlCajeros);
                ObtenerDatos();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(trasladoService.GetType().Name + "A", "Page_Load", ex);
        }
    }

    protected void LlenarComboMonedas(DropDownList ddlMonedas)
    {

        Xpinn.Caja.Services.TipoMonedaService monedaService = new Xpinn.Caja.Services.TipoMonedaService();
        Xpinn.Caja.Entities.TipoMoneda moneda = new Xpinn.Caja.Entities.TipoMoneda();
        ddlMonedas.DataSource = monedaService.ListarTipoMoneda(moneda, _usuario);
        ddlMonedas.DataTextField = "descripcion";
        ddlMonedas.DataValueField = "cod_moneda";
        ddlMonedas.DataBind();
    }

    protected void LlenarComboCajeros(DropDownList ddlCajeros)
    {

        Xpinn.Caja.Services.UsuariosService usuariosService = new Xpinn.Caja.Services.UsuariosService();
        Xpinn.Caja.Entities.Usuarios usuarios = new Xpinn.Caja.Entities.Usuarios();
        ddlCajeros.DataSource = usuariosService.ListarComboCajero(usuarios, 1, _usuario);
        ddlCajeros.DataTextField = "nom_cajero";
        ddlCajeros.DataValueField = "cod_cajero";
        ddlCajeros.DataBind();
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("../../../General/Global/inicio.aspx");
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
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
                            if ((long.Parse(Session["cajero"].ToString())) != long.Parse(ddlCajeros.SelectedValue))
                            {
                                if (decimal.Parse(txtValor.Text) > 0)
                                {
                                    try
                                    {
                                        //se atrapan los datos del formulario
                                        traslado.cod_cajero_des = long.Parse(ddlCajeros.SelectedValue);
                                        cajero.cod_cajero = traslado.cod_cajero_des.ToString();
                                        cajero = trasladoService.ConsultarCajaXCajero(cajero, _usuario);
                                        traslado.cod_caja_des = cajero.cod_caja;
                                        traslado.cod_moneda = long.Parse(ddlMonedas.SelectedValue);
                                        traslado.fecha_traslado = Convert.ToDateTime(txtFechaTraslado.Text);
                                        traslado.valor = decimal.Parse(txtValor.Text);
                                        traslado.cod_oficina_ori = long.Parse(Session["Office"].ToString());
                                        traslado.cod_cajero_ori = long.Parse(Session["cajero"].ToString());
                                        traslado.cod_caja_ori = long.Parse(Session["caja"].ToString());
                                        traslado.tipo_traslado = 1;
                                        traslado.tipo_movimiento = "EGRESO";
                                        traslado.estado = 0;
                                        traslado.tipo_ope = 32;
                                        traslado.ip = Convert.ToString((Session["ipusuario"].ToString()));

                                        saldo.cod_caja = traslado.cod_caja_ori;
                                        saldo.cod_cajero = traslado.cod_cajero_ori;
                                        saldo.tipo_moneda = traslado.cod_moneda;
                                        saldo.fecha = traslado.fecha_traslado;
                                        saldo.caja_principal = long.Parse(Session["CajeroPrincip"].ToString());

                                        saldo = saldoService.ConsultarSaldoCaja(saldo, _usuario);
                                        //se  verifica que el saldo de la caja siempre este disponible sino hay saldo
                                        //saldra un mensaje diciendo que la caj ano posee saldo disponible para 
                                        //realizar la transaccion
                                        if (saldo.valor > 0)
                                        {
                                            //se valida que el valor a trasladar sea menor o igual que el saldo de caja
                                            // sin no lo es genera un Mensaje de Error
                                            if (traslado.valor <= saldo.valor)
                                            {
                                                traslado = trasladoService.CrearTraslado(traslado, _usuario);
                                                btnGuardar.Visible = false;
                                                reporte();
                                            }
                                            else
                                                VerError("El Valor Solicitado es Superior al Saldo que tiene la Caja");
                                        }
                                        else
                                            VerError("La Caja no cuenta con Saldo Disponible para realizar la Transaccion");

                                    }
                                    catch (ExceptionBusiness ex)
                                    {
                                        VerError(ex.Message);
                                    }
                                    catch (Exception ex)
                                    {
                                        BOexcepcion.Throw(trasladoService.GetType().Name + "A", "btnGuardar_Click", ex);
                                    }
                                }
                                else
                                {
                                    VerError("El Valor debe ser mayor que Cero");
                                }
                            }
                            else
                            {
                                VerError("El Cajero Origen debe ser diferente al Cajero de Destino");
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

    protected void ObtenerDatos()
    {
        try
        {
            traslado = trasladoService.ConsultarCajero(_usuario);
            if (!string.IsNullOrEmpty(traslado.fecha_traslado.ToString()))
                txtFechaTraslado.Text = traslado.fecha_traslado.ToLongDateString();
            if (!string.IsNullOrEmpty(traslado.nomoficina_ori.ToString()))
                txtOficina.Text = traslado.nomoficina_ori.ToString();
            if (!string.IsNullOrEmpty(traslado.nomcaja_ori.ToString()))
                txtCaja.Text = traslado.nomcaja_ori.ToString();
            if (!string.IsNullOrEmpty(traslado.nomcajero_ori.ToString()))
                txtCajero.Text = traslado.nomcajero_ori.ToString().Trim();

            if (!string.IsNullOrEmpty(traslado.cod_oficina_ori.ToString()))
                Session["Office"] = traslado.cod_oficina_ori.ToString().Trim();
            if (!string.IsNullOrEmpty(traslado.cod_caja_ori.ToString()))
                Session["Caja"] = traslado.cod_caja_ori.ToString().Trim();
            if (!string.IsNullOrEmpty(traslado.cod_cajero_ori.ToString()))
                Session["Cajero"] = traslado.cod_cajero_ori.ToString().Trim();

            saldo.cod_caja = traslado.cod_caja_ori;            
            saldo.cod_cajero = traslado.cod_cajero_ori;
            saldo.tipo_moneda = Convert.ToInt64(ddlMonedas.SelectedValue);
            saldo.fecha = traslado.fecha_traslado;
            saldo.caja_principal = long.Parse(Session["CajeroPrincip"].ToString());

            ActualizarSaldosCaja();
            ConsultarTraslados();
          
            saldo = saldoService.ConsultarSaldoCaja(saldo, _usuario);

            if (saldo.valor < 0)
                txtValorEfectivo.Text = "0";
            else
                txtValorEfectivo.Text = saldo.valor.ToString();

            lstConsulta = movCajaServicio.ListarMovimientoCaja(ObtenerValores(Convert.ToInt64(ddlMonedas.SelectedValue)), _usuario);
            if (lstConsulta.Count > 0)
                txtValorCheque.Text = lstConsulta.Sum(x => x.cheque).ToString();
            else
                txtValorCheque.Text = "0";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(trasladoService.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

     
    protected void reporte()
    {
        ReportParameter[] param = new ReportParameter[8];
        param[0] = new ReportParameter("fecha", txtFechaTraslado.Text);
        param[1] = new ReportParameter("oficina", txtOficina.Text);
        param[2] = new ReportParameter("caja", txtCaja.Text);
        param[3] = new ReportParameter("cajero", txtCajero.Text);
        param[4] = new ReportParameter("valor", txtValor.Text);
        param[5] = new ReportParameter("cajero_entrega", Convert.ToString(ddlCajeros.SelectedItem));
        param[6] = new ReportParameter("moneda", Convert.ToString(ddlMonedas.SelectedItem));
        param[7] = new ReportParameter("ImagenReport", ImagenReporte());

        ReportViewer1.LocalReport.EnableExternalImages = true;
        ReportViewer1.LocalReport.SetParameters(param);
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.Refresh();
        MultiView1.ActiveViewIndex = 1;
    }

    private Xpinn.Caja.Entities.MovimientoCaja ObtenerValores(Int64 MonedaId)
    {
        Xpinn.Caja.Entities.MovimientoCaja movCaja = new Xpinn.Caja.Entities.MovimientoCaja();

        movCaja.cod_caja = long.Parse(Session["Caja"].ToString());
        movCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
        movCaja.cod_moneda = MonedaId;
        movCaja.tipo_mov = "I";
        movCaja.cod_tipo_pago = 2;
        //averiguuar por que el estado era 0 
        movCaja.estado = 0;

        return movCaja;
    }

    private Xpinn.Caja.Entities.MovimientoCaja ObtenerValoresSaldos(Xpinn.Caja.Entities.ArqueoCaja pArqueo)
    {
        Xpinn.Caja.Entities.MovimientoCaja movCaja = new Xpinn.Caja.Entities.MovimientoCaja();

        movCaja.cod_caja = long.Parse(Session["Caja"].ToString());
        movCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
        movCaja.fechaCierre = pArqueo.fecha_cierre;
        Session["FechaArqueo"] = movCaja.fechaCierre;
        return movCaja;
    }

    public void ConsultarTraslados()
    {
        Xpinn.Caja.Entities.TransaccionCaja transac = new Xpinn.Caja.Entities.TransaccionCaja();


        List<Xpinn.Caja.Entities.TransaccionCaja> lstConsulta = new List<Xpinn.Caja.Entities.TransaccionCaja>();
        Xpinn.Caja.Entities.MovimientoCaja movCaja = new Xpinn.Caja.Entities.MovimientoCaja();
        Xpinn.Caja.Services.TransaccionCajaService tranCajaServicio = new Xpinn.Caja.Services.TransaccionCajaService();

        transac.cod_caja = long.Parse(Session["Caja"].ToString());
        transac.cod_cajero = long.Parse(Session["Cajero"].ToString());
        
        transac.fecha_consulta_final = Convert.ToDateTime(txtFechaTraslado.Text);
          Usuario pUsuario = (Usuario)Session["usuario"];
        try
        {
            if (long.Parse(Session["estadoOfi"].ToString()) == 1)
            {
                if (long.Parse(Session["estadoCaj"].ToString()) == 1)
                {
                  

                    lstConsulta = tranCajaServicio.ListarTrasladosCaja(transac, (Usuario)Session["usuario"]);

                    gvMovimiento.DataSource = lstConsulta;

                    if (lstConsulta.Count > 0)
                    {
                        gvMovimiento.Visible = true;
                        gvMovimiento.DataBind();
                     
                    }
                    else
                    {
                        gvMovimiento.Visible = false;

                    }

                    Session.Add(tranCajaServicio.GetType().Name + ".consulta", 1);

                }

            }
          
          
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tranCajaServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    protected void gvMovimiento_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt32(gvMovimiento.Rows[e.RowIndex].Cells[4].Text);
            trasladoService.EliminarTraslado(id, (Usuario)Session["usuario"]);
         
            ConsultarTraslados();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(trasladoService.CodigoPrograma, "gvMovimiento_RowDeleting", ex);
        }
    }
    public void ActualizarSaldosCaja()
    {
        try
        {

            arqueoCaja.cod_caja = long.Parse(Session["Caja"].ToString());
            arqueoCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
            arqueoCaja.fecha_cierre = Convert.ToDateTime(txtFechaTraslado.Text);
            arqueoCaja = arqueoCajaService.ConsultarUltFechaArqueoCaja(arqueoCaja, (Usuario)Session["usuario"]);

            //aqui va el codigo para borrar todos los datos de la tabla TempArqueoCaja
            arqueoCaja.cod_caja = long.Parse(Session["Caja"].ToString());
            arqueoCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
            arqueoCaja.fecha_cierre = Convert.ToDateTime(txtFechaTraslado.Text);
            movCajaServicio.EliminarTempArqueoCaja(ObtenerValoresSaldos(arqueoCaja), (Usuario)Session["usuario"]);

            //aqui va el metodo para realizar la insercion
            arqueoCaja.cod_caja = long.Parse(Session["Caja"].ToString());
            arqueoCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
            arqueoCaja.fecha_cierre = Convert.ToDateTime(txtFechaTraslado.Text);
            movCajaServicio.CrearTempArqueoCaja(ObtenerValoresSaldos(arqueoCaja), (Usuario)Session["usuario"]);

            //este es el metodo que sirve para consultar los datos que hay en ese momento en la tabla de TempArqueoCaja
            arqueoCaja.cod_caja = long.Parse(Session["Caja"].ToString());
            arqueoCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
            arqueoCaja.fecha_cierre = Convert.ToDateTime(txtFechaTraslado.Text);
            lstConsulta = movCajaServicio.ListarSaldos(ObtenerValoresSaldos(arqueoCaja), (Usuario)Session["usuario"]);
            foreach (Xpinn.Caja.Entities.MovimientoCaja movimiento in lstConsulta)
            {
                decimal saldo = 0;
                if (movimiento.orden == 8)
                    saldo = Convert.ToDecimal(movimiento.total);
            }

            Session.Add(movCajaServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(movCajaServicio.GetType().Name + "L", "ActualizarSaldosCaja", ex);
        }
    }


}