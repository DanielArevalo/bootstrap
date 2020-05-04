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

    Usuario user = new Usuario();

    List<Xpinn.Caja.Entities.MovimientoCaja> lstConsulta = new List<Xpinn.Caja.Entities.MovimientoCaja>();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(consignacionService.CodigoProgramaTeso, "A");

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
                Session["Resp1"] = 0;
                Session["Resp2"] = 0;              

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

        movCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
        movCaja.cod_usuario = long.Parse(Session["Cajero"].ToString());
        movCaja.cod_moneda = MonedaId;
        movCaja.tipo_mov = "I";
        movCaja.cod_tipo_pago = 2;
        movCaja.estado = 0;

        return movCaja;
    }


    public void Actualizar(Int64 MonedaId)
    {
        try
        {
            lstConsulta = movCajaServicio.ListarMovimientoTesoreria(ObtenerValores(MonedaId), (Usuario)Session["usuario"]);//, (UserSession)Session["user"]);

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
           consignacion = consignacionService.ConsultarUsuario((Usuario)Session["usuario"]);//se consulta la informacion del cajero que se encuentra conectado

            if (!string.IsNullOrEmpty(consignacion.nom_oficina.ToString()))
                txtOficina.Text = consignacion.nom_oficina.ToString();            
            if (!string.IsNullOrEmpty(consignacion.nom_cajero.ToString()))
                txtCajero.Text = consignacion.nom_cajero.ToString().Trim();
            
            txtFechaConsignacion.Text = DateTime.Now.ToShortDateString();
            if (!string.IsNullOrEmpty(consignacion.cod_oficina.ToString()))
                Session["Office"] = consignacion.cod_oficina.ToString().Trim();
            if (!string.IsNullOrEmpty(consignacion.cod_cajero.ToString()))
                Session["Cajero"] = consignacion.cod_cajero.ToString().Trim();

            saldo.cod_cajero = consignacion.cod_cajero;
            saldo.tipo_moneda = MonedaId;
            saldo.fecha = consignacion.fecha_consignacion;
            saldo = saldoService.ConsultarSaldoTesoreriaConsig(saldo, (Usuario)Session["usuario"]);
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
        VerError("");
        // si hay dinero efectivo o dinero en chque el sistema realizara la consignacion
        // si no hay ninguno de los dos el sistema sacara un mensajer de error diciendo que la Caja relacionado 
        //con el cajero debe tener Saldo ya sea dinero en efectivo o en cheque

        consignacion.valor_cheque = txtValorConsigCheque.Text == "" ? 0 : decimal.Parse(txtValorConsigCheque.Text); //este es el valor a consignar por los cheques seleccionados de la grilla
        consignacion.valor_efectivo = txtValorConsigEfecty.Text == "" ? 0 : decimal.Parse(txtValorConsigEfecty.Text); // este es el valor que suministra el usuario para guardar Efectivo en el banco
        if (ddlBancos.SelectedValue != "0")
        {
            // se valida que el vaalor a consignar en efectivo sea mayor que cero
            if (consignacion.valor_efectivo >= 0)
            {                                       
                if (consignacion.valor_efectivo >= 0 && long.Parse(Session["CalculaTotal"].ToString()) == 1)
                {
                    try
                    {                                                  
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
                            consignacion.Cuenta = Convert.ToString(this.ddlCuenta.SelectedValue);
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
                        // Validar que exista la parametrización contable por procesos
                        if (ValidarProcesoContable(Convert.ToDateTime(txtFechaConsignacion.Text), 31) == false)
                        {
                            VerError("No se encontró parametrización contable por procesos para el tipo de operación 31 = Consignaciones");
                            return;
                        }
                        Int64 pCOD_OPE = 0;
                        consignacion = consignacionService.CrearConsignacionTesoreria(consignacion, gvConsignacion,ref pCOD_OPE, (Usuario)Session["usuario"]);
                        
                        // Se genera el comprobante
                        if (pCOD_OPE > 0)
                        {
                            Usuario pUsu = (Usuario)Session["usuario"];
                            Int64 pCodPersona = pUsu.cod_persona != null ? Convert.ToInt64(pUsu.cod_persona) : 0 ;
                            Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                            Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = pCOD_OPE;
                            Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 31;
                            Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = Convert.ToDateTime(txtFechaConsignacion.Text);
                            Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] = pUsu.cod_oficina;
                            Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = pCodPersona;
                            Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                        }
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
                VerError("El Valor a Consignar debe ser Mayor que Cero");
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

    
    /// <summary>
    /// Se selecciona el banco en el cual se va a consignar.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlBancos_SelectedIndexChanged(object sender, EventArgs e)
    {
        Xpinn.Caja.Services.BancosService BancosService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos Bancos = new Xpinn.Caja.Entities.Bancos();
        List<Xpinn.Caja.Entities.CuentaBancaria> lstConsulta = new List<Xpinn.Caja.Entities.CuentaBancaria>();
        lstConsulta = BancosService.ListarCuentaBancos(Convert.ToInt64(ddlBancos.SelectedValue), (Usuario)Session["Usuario"]);
        ddlCuenta.Items.Clear();
        if (lstConsulta.Count > 0)
        {
            ddlCuenta.DataSource = lstConsulta;
            ddlCuenta.DataTextField = "num_cuenta";
            ddlCuenta.DataValueField = "num_cuenta";
            ddlCuenta.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlCuenta.SelectedIndex = 0;
            ddlCuenta.DataBind();
            ddlCuenta.Enabled = true;
        }
        else
        {
            ddlCuenta.Enabled = false;
            ddlCuenta.Items.Clear();
        }
    }

}