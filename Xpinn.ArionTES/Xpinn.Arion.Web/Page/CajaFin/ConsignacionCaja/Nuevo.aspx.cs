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
    Xpinn.Caja.Services.ConsignacionService consignacionService = new Xpinn.Caja.Services.ConsignacionService();
    Xpinn.Caja.Entities.Consignacion consignacion = new Xpinn.Caja.Entities.Consignacion();

    Xpinn.Caja.Entities.SaldoCaja saldo = new Xpinn.Caja.Entities.SaldoCaja();
    Xpinn.Caja.Services.SaldoCajaService saldoService = new Xpinn.Caja.Services.SaldoCajaService();

    Xpinn.Caja.Services.MovimientoCajaService movCajaServicio = new Xpinn.Caja.Services.MovimientoCajaService();
    Xpinn.Caja.Entities.MovimientoCaja movCaja = new Xpinn.Caja.Entities.MovimientoCaja();

    Xpinn.Caja.Services.HorarioOficinaService HorarioService = new Xpinn.Caja.Services.HorarioOficinaService();
    Xpinn.Caja.Entities.HorarioOficina horario = new Xpinn.Caja.Entities.HorarioOficina();

    Xpinn.Caja.Services.CajeroService cajeroService = new Xpinn.Caja.Services.CajeroService();
    Xpinn.Caja.Entities.Cajero cajero = new Xpinn.Caja.Entities.Cajero();
    Usuario user = new Usuario();

    List<Xpinn.Caja.Entities.MovimientoCaja> lstConsulta = new List<Xpinn.Caja.Entities.MovimientoCaja>();
    Int64 consignaciones = 0;
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(consignacionService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(consignacionService.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                MultiView1.SetActiveView(View1);
                user = (Usuario)Session["usuario"];
                cajero = cajeroService.ConsultarIfUserIsntCajeroPrincipal(user.codusuario, (Usuario)Session["usuario"]);
                Session["estadoCaj"] = cajero.estado;//estado Cajero
                Session["estadoOfi"] = cajero.estado_ofi;// estado Oficina
                Session["estadoCaja"] = cajero.estado_caja;// estado Caja

                Session["CajeroPrincip"] = cajero.conteo; // se verifica si el cajero es principal o no

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
                                VerError("La Oficina se encuentra por fuera del horario configurado");
                        }

                    }
                }


                //se inicializa el combo de monedas y bancos
                LlenarComboMonedas(ddlMonedas);
                LlenarComboBancos(ddlBancos);
                ObtenerDatos(long.Parse(ddlMonedas.SelectedValue));
                Actualizar(long.Parse(ddlMonedas.SelectedValue));
                setValueGrid();
                Session["CalculaTotal"] = 0;
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(consignacionService.GetType().Name + "A", "Page_Load", ex);
        }

    }

    private Xpinn.Caja.Entities.MovimientoCaja ObtenerValores(Int64 MonedaId)
    {
        Xpinn.Caja.Entities.MovimientoCaja movCaja = new Xpinn.Caja.Entities.MovimientoCaja();

        movCaja.cod_caja = long.Parse(Session["Caja"].ToString());
        movCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
        movCaja.cod_moneda = MonedaId;
        movCaja.tipo_mov = "I";
        movCaja.filtro = "";
        //if (ddlBancos.SelectedItem != null)
        //    if (ddlBancos.SelectedIndex > 0)
        //        movCaja.filtro += " and A.cod_banco = " + ddlBancos.SelectedValue;

        //if (ddlCuenta.SelectedItem != null)
        //    if (ddlCuenta.SelectedIndex > 0)
        //        movCaja.filtro += " and A.idctabancaria = " + ddlCuenta.SelectedValue;
        movCaja.cod_tipo_pago = 2;
        //averiguuar por que el estado era 0 
        movCaja.estado = 0;

        return movCaja;
    }


    public void Actualizar(Int64 MonedaId)
    {
        try
        {
            lstConsulta = movCajaServicio.ListarMovimientoCaja(ObtenerValores(MonedaId), (Usuario)Session["usuario"]);//, (UserSession)Session["user"]);

            gvConsignacion.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvConsignacion.Visible = true;
                gvConsignacion.DataBind();
                ValidarPermisosGrilla(gvConsignacion);
            }
            else
            {
                gvConsignacion.Visible = false;
            }

            Session.Add(movCajaServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(movCajaServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }


    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("../../../General/Global/inicio.aspx");
    }

    protected void btnCancelarreporte_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }


    protected void LlenarComboMonedas(DropDownList ddlMonedas)
    {

        Xpinn.Caja.Services.TipoMonedaService monedaService = new Xpinn.Caja.Services.TipoMonedaService();
        Xpinn.Caja.Entities.TipoMoneda moneda = new Xpinn.Caja.Entities.TipoMoneda();
        ddlMonedas.DataSource = monedaService.ListarTipoMoneda(moneda, (Usuario)Session["Usuario"]);
        ddlMonedas.DataTextField = "descripcion";
        ddlMonedas.DataValueField = "cod_moneda";
        ddlMonedas.DataBind();
    }

    protected void LlenarComboBancos(DropDownList ddlBancos)
    {

        Xpinn.Caja.Services.BancosService BancosService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos Bancos = new Xpinn.Caja.Entities.Bancos();
        ddlBancos.DataSource = BancosService.ListarBancosegre((Usuario)Session["Usuario"]);
        ddlBancos.DataTextField = "nombrebanco";
        ddlBancos.DataValueField = "cod_banco";
        ddlBancos.DataBind();
        ddlBancos.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }

    protected void ObtenerDatos(Int64 MonedaId)
    {
        try
        {
            consignacion = consignacionService.ConsultarCajero((Usuario)Session["usuario"]);//se consulta la informacion del cajero que se encuentra conectado

            if (!string.IsNullOrEmpty(consignacion.nom_oficina.ToString()))
                txtOficina.Text = consignacion.nom_oficina.ToString();
            if (!string.IsNullOrEmpty(consignacion.nom_caja.ToString()))
                txtCaja.Text = consignacion.nom_caja.ToString();
            if (!string.IsNullOrEmpty(consignacion.nom_cajero.ToString()))
                txtCajero.Text = consignacion.nom_cajero.ToString().Trim();
            if (!string.IsNullOrEmpty(consignacion.fecha_consignacion.ToString()))
                txtFechaConsignacion.Text = consignacion.fecha_consignacion.ToShortDateString();

            if (!string.IsNullOrEmpty(consignacion.cod_oficina.ToString()))
                Session["Office"] = consignacion.cod_oficina.ToString().Trim();
            if (!string.IsNullOrEmpty(consignacion.cod_caja.ToString()))
                Session["Caja"] = consignacion.cod_caja.ToString().Trim();
            if (!string.IsNullOrEmpty(consignacion.cod_cajero.ToString()))
                Session["Cajero"] = consignacion.cod_cajero.ToString().Trim();


            saldo.cod_caja = consignacion.cod_caja;
            saldo.cod_cajero = consignacion.cod_cajero;
            saldo.tipo_moneda = MonedaId;
            saldo.fecha = consignacion.fecha_consignacion;
            saldo.caja_principal = long.Parse(Session["CajeroPrincip"].ToString());

            saldo = saldoService.ConsultarSaldoCaja(saldo, (Usuario)Session["usuario"]);

            if (saldo.valor < 0)
                txtValorEfectivo.Text = "0";
            else
                txtValorEfectivo.Text = saldo.valor.ToString();


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(consignacionService.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {

        // si hay dinero efectivo o dinero en chque el sistema realizara la consignacion
        // si no hay ninguno de los dos el sistema sacara un mensajer de error diciendo que la Caja relacionado 
        //con el cajero debe tener Saldo ya sea dinero en efectivo o en cheque

        consignacion.valor_cheque = txtValorConsigCheque.Text == "" ? 0 : decimal.Parse(txtValorConsigCheque.Text); //este es el valor a consignar por los cheques seleccionados de la grilla
        consignacion.valor_efectivo = txtValorConsigEfecty.Text == "" ? 0 : decimal.Parse(txtValorConsigEfecty.Text); // este es el valor que suministra el usuario para guardar Efectivo en el banco
        if (ddlBancos.SelectedValue != "0")
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
                                if ((decimal.Parse(txtValorEfectivo.Text) > 0) || (decimal.Parse(txtValorCheque.Text) > 0))
                                {
                                    // se valida que el vaalor a consignar en efectivo sea mayor que cero
                                    if (consignacion.valor_efectivo >= 0)
                                    {
                                        // se valida que el valor a consignar sea menor o igual al saldo en efectivo de la Caja
                                        // txtValorConsigEfecty: Valor a Consignar digitado por el usuario
                                        // txtValorEfectivo: valor en efectivo de  saldo caja

                                        if (consignacion.valor_efectivo <= decimal.Parse(txtValorEfectivo.Text))
                                        {
                                            if (consignacion.valor_efectivo >= 0 && long.Parse(Session["CalculaTotal"].ToString()) == 1)
                                            {
                                                try
                                                {

                                                    try
                                                    {
                                                        consignacion.cod_oficina = long.Parse(Session["Office"].ToString());
                                                    }
                                                    catch
                                                    {
                                                        VerError("Error de conversion cod_oficina ");
                                                    }

                                                    try
                                                    {
                                                        consignacion.cod_caja = long.Parse(Session["Caja"].ToString());

                                                    }
                                                    catch
                                                    {
                                                        VerError("Error de conversion cod_caja ");
                                                    }

                                                    try
                                                    {

                                                        consignacion.cod_cajero = long.Parse(Session["Cajero"].ToString());
                                                    }
                                                    catch
                                                    {
                                                        VerError("Error de conversion cod_cajero ");
                                                    }

                                                    try
                                                    {
                                                        consignacion.fecha_consignacion = Convert.ToDateTime(txtFechaConsignacion.Text);
                                                    }
                                                    catch
                                                    {
                                                        VerError("Error de conversion fecha_consignacion ");
                                                    }

                                                    try
                                                    {
                                                        consignacion.cod_banco = long.Parse(ddlBancos.SelectedValue);
                                                    }
                                                    catch
                                                    {
                                                        VerError("Error de conversion cod_banco ");
                                                    }

                                                    try
                                                    {
                                                        consignacion.cod_moneda = long.Parse(ddlMonedas.SelectedValue);
                                                    }
                                                    catch
                                                    {
                                                        VerError("Error de conversion cod_moneda ");
                                                    }

                                                    try
                                                    {
                                                        consignacion.observaciones = txtObservacion.Text;
                                                    }
                                                    catch
                                                    {
                                                        VerError("Error de conversion observaciones ");
                                                    }

                                                    try
                                                    {
                                                        consignacion.tipo_ope = 31;
                                                    }
                                                    catch
                                                    {
                                                        VerError("Error de conversion tipo_ope ");
                                                    }

                                                    try
                                                    {
                                                        consignacion.Cuenta = Convert.ToString(this.ddlCuenta.SelectedItem.Text);
                                                    }
                                                    catch
                                                    {
                                                        VerError("Error de conversion Cuenta ");
                                                    }

                                                    try
                                                    {
                                                        consignacion.valor_consignacion_total = txtValorTotalAConsig.Text == "" ? 0 : decimal.Parse(txtValorTotalAConsig.Text);// valor total a consignar
                                                    }
                                                    catch
                                                    {
                                                        VerError("Error de conversion valor_consignacion_total ");
                                                    }
                                                  
                                                    consignacion = consignacionService.CrearConsignacion(consignacion, gvConsignacion, (Usuario)Session["usuario"]);
                                                    Xpinn.Caja.Entities.Consignacion consigna = new Xpinn.Caja.Entities.Consignacion();

                                                    consigna = consignacionService.ConsultarConsignacion(consignacion.cod_consignacion, (Usuario)Session["usuario"]);
                                                    if (consigna.cod_ope == 0 || consigna.cod_consignacion == 0)
                                                    {
                                                        VerError("Se genero un error al consultar la consignacion.");
                                                        return;
                                                    }
                                                    consigna = consignacionService.CrearConsignacionCheque(consigna, gvConsignacion, (Usuario)Session["usuario"]);
                                                    Session["Consignacion"] = consigna.cod_consignacion;
                                                    btnInforme0_Click(null, null);
                                                    //Navegar("../../../General/Global/inicio.aspx");
                                                }
                                                catch (ExceptionBusiness ex)
                                                {
                                                    VerError(ex.Message);
                                                }
                                                catch (Exception ex)
                                                {
                                                    BOexcepcion.Throw(consignacionService.GetType().Name + "A", "btnGuardar_Click", ex);
                                                }
                                            }
                                            else
                                                VerError("El Valor Total aún no ha sido Calculado por favor presionar Botón Calculo Total");
                                        }
                                        else
                                            VerError("El Valor en Efectivo digitado por el Cajero es superior al Valor en Efectivo en Caja");
                                    }
                                    else
                                        VerError("El Valor a Consignar debe ser Mayor que Cero");
                                }
                                else
                                    VerError("La Caja debe tener Valor de Caja en Efectivo ó Valor en Cheque en Efectivo");
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
        else
            VerError("Debe seleccionar un Banco y una Cuenta Banacaria antes de continuar");

    }

    protected void ddlMonedas_SelectedIndexChanged(object sender, EventArgs e)
    {
        int MonedaId = int.Parse(ddlMonedas.SelectedValue);
        ObtenerDatos(MonedaId);
        Actualizar(MonedaId);
        setValueGrid();
    }


    protected void setValueGrid()
    {
        decimal valorCheque = 0;
        decimal acum = 0;
        if (lstConsulta.Count > 0)
        {
            foreach (GridViewRow fila in gvConsignacion.Rows)
            {
                valorCheque = Decimal.Parse(fila.Cells[5].Text);
                acum += valorCheque;
            }

            txtValorCheque.Text = acum.ToString();
        }
        else
            txtValorCheque.Text = "0";
    }

    protected void chkRecibe_CheckedChanged(object sender, EventArgs e)
    {
        decimal acum = 0;
        CheckBox chkRecibe;
        decimal valorCheque = 0;
        decimal valEfectivo = 0;

        foreach (GridViewRow fila in gvConsignacion.Rows)
        {
            valorCheque = decimal.Parse(fila.Cells[5].Text);
            chkRecibe = (CheckBox)fila.FindControl("chkRecibe");
            if (chkRecibe.Checked == true)
            {
                acum += valorCheque;
            }
        }

        valEfectivo = txtValorConsigEfecty.Text == "" ? 0 : decimal.Parse(txtValorConsigEfecty.Text);

        if (valEfectivo > 0)
            Session["CalculaTotal"] = 1;
        else
            Session["CalculaTotal"] = 0;

        txtValorConsigCheque.Text = acum.ToString();
        setValorTotalConsignacion();
    }

    protected void setValorTotalConsignacion()
    {
        decimal valTotal = 0;
        decimal valEfectivo = 0;
        decimal valCheque = 0;

        valCheque = txtValorConsigCheque.Text == "" ? 0 : decimal.Parse(txtValorConsigCheque.Text);
        valEfectivo = txtValorConsigEfecty.Text == "" ? 0 : decimal.Parse(txtValorConsigEfecty.Text);

        valTotal = valCheque + valEfectivo;
        txtValorTotalAConsig.Text = valTotal.ToString();

    }

    protected void btnValoEfe_Click(object sender, EventArgs e)
    {
        Session["CalculaTotal"] = 1;
        setValorTotalConsignacion();
    }

    protected void txtValorCheque_TextChanged(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Método para generar el reporte de consignación
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInforme0_Click(object sender, EventArgs e)
    {
        Xpinn.Caja.Entities.Consignacion refe = new Xpinn.Caja.Entities.Consignacion();
        List<Xpinn.Caja.Entities.Consignacion> lstConsignacion = new List<Xpinn.Caja.Entities.Consignacion>();
        Xpinn.Caja.Entities.Consignacion consigna = new Xpinn.Caja.Entities.Consignacion();
        Int64 consignacion = Convert.ToInt64(Session["Consignacion"]);
        lstConsignacion = this.consignacionService.ListarConsignacionCheque(consignacion, (Usuario)Session["usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();

        table.Columns.Add("fecha");
        table.Columns.Add("cheque");
        table.Columns.Add("banco");
        table.Columns.Add("nombre_banco");
        table.Columns.Add("valor");
        table.Columns.Add("moneda");

        DataRow datarw;
        if (lstConsignacion.Count == 0)
        {
            datarw = table.NewRow();

            datarw[0] = " ";
            datarw[1] = " ";
            datarw[2] = " ";
            datarw[3] = " ";
            datarw[4] = " ";
            datarw[5] = " ";
            table.Rows.Add(datarw);

        }
        else
        {
            for (int i = 0; i < lstConsignacion.Count; i++)
            {
                datarw = table.NewRow();
                refe = lstConsignacion[i];

                datarw[0] = " " + refe.fecha_consignacion.ToString("dd/MM/yyyy");
                datarw[1] = " " + refe.documento;
                datarw[2] = " " + refe.cod_banco;
                datarw[3] = " " + refe.nom_banco;
                datarw[4] = " " + refe.valor_cheque.ToString("0,0");
                datarw[5] = " " + refe.nom_moneda;
                table.Rows.Add(datarw);

            }
        }
        ReportParameter[] param = new ReportParameter[11];
        param[0] = new ReportParameter("fecha", txtFechaConsignacion.Text);
        param[1] = new ReportParameter("oficina", txtOficina.Text);
        param[2] = new ReportParameter("caja", txtCaja.Text);
        param[3] = new ReportParameter("cajero", txtCajero.Text);
        param[4] = new ReportParameter("banco", ddlBancos.SelectedItem.Text);

        //if (txtValorCheque.Text != "")
       // {
            param[5] = new ReportParameter("valorencheque", txtValorConsigCheque.Text);
       // }
       // else
        //{
           // param[5] = new ReportParameter("total", txtValorTotalAConsig.Text);
        //}
          
        param[6] = new ReportParameter("valorenefectivo", txtValorConsigEfecty.Text);
        param[7] = new ReportParameter("cuenta", ddlCuenta.SelectedItem.Text);
  
        
        if (txtValorTotalAConsig.Text == "")
        {
            param[8] = new ReportParameter("total", "0.0");
        }
        else
        {
            param[8] = new ReportParameter("total", txtValorTotalAConsig.Text);
        }
        param[9] = new ReportParameter("observacion", txtObservacion.Text);
        param[10] = new ReportParameter("ImagenReport", ImagenReporte());

        ReportViewer1.LocalReport.EnableExternalImages = true;
        ReportViewer1.LocalReport.SetParameters(param);
        ReportDataSource rds1 = new ReportDataSource("DataSet1", table);
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(rds1);
        ReportViewer1.LocalReport.Refresh();
        MultiView1.ActiveViewIndex = 1;
    }

    /// <summary>
    /// Se selecciona el banco en el cual se va a consignar.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlBancos_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCuenta.Enabled = true;
        Xpinn.Caja.Services.BancosService BancosService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos Bancos = new Xpinn.Caja.Entities.Bancos();
        ddlCuenta.DataSource = BancosService.ListarBancosegrecuentas(ddlBancos.SelectedValue, (Usuario)Session["Usuario"]);
        ddlCuenta.DataTextField = "num_cuenta";
        ddlCuenta.DataValueField = "ctabancaria";
        ddlCuenta.DataBind();
        
    }

    
}