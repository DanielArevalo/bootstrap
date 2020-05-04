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
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;
using Xpinn.FabricaCreditos.Services;

public partial class Nuevo : GlobalWeb
{
    int tipoOpe = 58;
    TrasladoDevolucionServices TrasladoServicios = new TrasladoDevolucionServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(TrasladoServicios.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TrasladoServicios.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["OPCION"] = "";
                mvAplicar.ActiveViewIndex = 0;
                CargarDropDown(); 

                if (Session[TrasladoServicios.CodigoPrograma + ".id"] != null)
                {
                    txtFecha.ToDateTime = System.DateTime.Now;
                    idObjeto = Session[TrasladoServicios.CodigoPrograma + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                    sumar(); 
                }
                DropDownFormaDesembolso_SelectedIndexChanged(DropDownFormaDesembolso, null);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TrasladoServicios.GetType().Name + "L", "Page_Load", ex);
        }
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            List<TrasladoDevolucion> lstDeta = new List<TrasladoDevolucion>();

            lstDeta = TrasladoServicios.ConsultarTrasladoDevolucion(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (lstDeta.Count > 0)
            {
                if (lstDeta[0].num_devolucion != 0)
                    txtNum_Devolucion.Text = lstDeta[0].num_devolucion.ToString();
                if (lstDeta[0].cod_persona != 0)
                    txtcodPersona.Text = lstDeta[0].cod_persona.ToString();
                if (lstDeta[0].identificacion != "")
                    txtIdentificacion.Text = lstDeta[0].identificacion;
                if (lstDeta[0].nombre != "")
                    txtNombre.Text = lstDeta[0].nombre.Trim();
                gvDetalle.DataSource = lstDeta;
                gvDetalle.DataBind();
                chkTraslador_CheckedChanged(null,null);
            }
            foreach (GridViewRow ifila in gvDetalle.Rows)
            { 
                CheckBoxGrid chktraslado = (CheckBoxGrid)ifila.FindControl("chkTraslador");
                decimales txtvaalor = (decimales)ifila.FindControl("txtValorTraslado");
                if (chktraslado.Checked == false)
                {
                    txtvaalor.Enabled = false;
                }
                else 
                {
                    txtvaalor.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TrasladoServicios.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public Boolean ValidarDatos()
    {
        if (gvDetalle.Rows.Count > 0)
        {
            foreach (GridViewRow rFila in gvDetalle.Rows)
            {
                decimales txtValorTraslado = (decimales)rFila.FindControl("txtValorTraslado");
                if (txtValorTraslado.Text != "")
                {
                    if (Convert.ToDecimal(txtValorTraslado.Text) > Convert.ToDecimal(0))
                    {
                        if (Convert.ToDecimal(txtValorTraslado.Text) > Convert.ToDecimal(rFila.Cells[4].Text))
                        {
                            VerError("Su valor a trasladar no puede ser mayor a el saldo disponible en su cuenta");
                            return false;
                        }
                    }
                }

                if (txtFecha.Text == "")
                {
                    VerError("Ingrese la fecha de Traslado");
                    return false;
                }
            }
        }

        if (gvDetalle.Rows.Count > 0)
        {
            int val = 0;
            foreach (GridViewRow rFila in gvDetalle.Rows)
            {
                CheckBoxGrid chkTraslador = (CheckBoxGrid)rFila.FindControl("chkTraslador");
                if (chkTraslador.Checked == true)
                    val = 1;                
            }
            if (val == 0)
            {
                VerError("Seleccione el registro a Trasladar");
                return false;
            }
        }
        else 
        {
            VerError("No Existen datos seleccionados a trasladar, No puede realizar la Grabación");
            return false;
        }
        if (DropDownFormaDesembolso.SelectedIndex == 0)
        {
            VerError("Seleccione la Forma de Desembolso");
            return false;
        }
        // Validar que exista la parametrización contable por procesos
        if (ValidarProcesoContable(Convert.ToDateTime(txtFecha.Text), tipoOpe) == false)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación " + tipoOpe.ToString() + "=Traslado de Devoluciones");
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");

        if (ValidarDatos())
        {
            Session["OPCION"] = "GRABAR";
            ctlMensaje.MostrarMensaje("Desea grabar los Traslados de devolución?");          
        }
    }

    /// <summary>
    /// Guardar los datos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>    
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        if (Session["OPCION"].ToString() == "GRABAR")
        {
            VerError("");
            // Determinar código de proceso contable para generar el comprobante
            Int64? rpta = 0;
            if (!panelProceso.Visible && panelGeneral.Visible)
            {
                rpta = ctlproceso.Inicializar(tipoOpe, txtFecha.ToDateTime, (Usuario)Session["Usuario"]);
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
                    // Crear la tarea de ejecución del proceso                
                    if (AplicarDatos())
                        Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                }
            }
        }
        else
        {
            if (Session["LISTA"] != null)
            {
                try
                {
                    List<Xpinn.FabricaCreditos.Entities.CuentasBancarias> lstCuenta = (List<Xpinn.FabricaCreditos.Entities.CuentasBancarias>)Session["LISTA"];
                    if (lstCuenta[0].cod_banco != null && lstCuenta[0].cod_banco != 0)
                        DropDownEntidad.SelectedValue = lstCuenta[0].cod_banco.ToString();
                    if (lstCuenta[0].numero_cuenta != null && lstCuenta[0].numero_cuenta != "")
                        txtnumcuenta.Text = lstCuenta[0].numero_cuenta;
                    if (lstCuenta[0].tipo_cuenta != null && lstCuenta[0].tipo_cuenta != 0)
                        ddlTipoCuenta.SelectedValue = lstCuenta[0].tipo_cuenta.ToString();
                }
                catch { }
            }
        }
    }

    protected bool AplicarDatos() 
    {
        try
        {
            Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
            Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();

            //GRABACION DE LA OPERACION
            Usuario pUsu = (Usuario)Session["usuario"];
            //OperacionServices xTesoreria = new OperacionServices();
            Operacion vOpe = new Operacion();
            vOpe.cod_ope = 0;
            vOpe.tipo_ope = tipoOpe;
            vOpe.cod_caja = 0;
            vOpe.cod_cajero = 0;
            vOpe.observacion = "Grabacion de operacion-Traslado devolucion";
            vOpe.cod_proceso = null;
            vOpe.fecha_oper = DateTime.Now;
            vOpe.fecha_calc = DateTime.Now;
            vOpe.cod_ofi = pUsu.cod_oficina;                
                
            string pNum_Dev = txtNum_Devolucion.Text;

            List<TrasladoDevolucion> lstTranslados = new List<TrasladoDevolucion>();
            foreach (GridViewRow rFila in gvDetalle.Rows)
            {
                CheckBoxGrid chkTraslador = (CheckBoxGrid)rFila.FindControl("chkTraslador");
                if (chkTraslador.Checked)
                {                        
                    //GRABACION DEL TRASLADO DE LA DEVOLUCION 
                    TrasladoDevolucion pTras = new TrasladoDevolucion();
                    pTras.numero_transaccion = 0;
                    txtNum_Devolucion.Text = rFila.Cells[1].Text;
                    pTras.num_devolucion = Convert.ToInt32(txtNum_Devolucion.Text);
                    decimales txtValorTraslado = (decimales)rFila.FindControl("txtValorTraslado");
                    decimal pVr = 0;
                    //txtValorAaplicar.Text.Replace("$", "").Replace(gSeparadorMiles, "")
                    if (txtValorTraslado.Text != "")
                        pVr = Convert.ToDecimal(txtValorTraslado.Text.Replace("$", "").Replace(".", "").Trim());
                    try { pTras.valor = pVr; }
                    catch { VerError("No pudo determinar valor de la devolución"); return false; }
                    pTras.tipo_tran = 904; 
                    pTras.estado = 2;
                    lstTranslados.Add(pTras);                       
                }
            }

            //GRABACION DEL GIRO A REALIZAR
            Xpinn.FabricaCreditos.Services.AvanceService AvancServices = new Xpinn.FabricaCreditos.Services.AvanceService();
            Xpinn.FabricaCreditos.Entities.Giro pGiro = new Xpinn.FabricaCreditos.Entities.Giro();
            pGiro.idgiro = 0;
            pGiro.cod_persona = Convert.ToInt64(txtcodPersona.Text);
            pGiro.forma_pago = Convert.ToInt32(DropDownFormaDesembolso.SelectedValue);
            pGiro.tipo_acto = 3; //DEVOLUCIONES TIPO_ACTO_GIRO
            pGiro.fec_reg = Convert.ToDateTime(txtFecha.Text);
            pGiro.fec_giro = DateTime.Now;
            pGiro.numero_radicacion = 0;// NO ENVIO EL NUMERO DE RADICACION SINO EL NUMERO DE DEVOLUCION                
            pGiro.usu_gen = pUsu.nombre;
            pGiro.usu_apli = null;
            pGiro.estadogi = 0;
            pGiro.usu_apro = null;

            //DETERMINAR LA IDENTIFICACIÓN DE LA CUENTA BANCARIA
            CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ddlEntidadOrigen.SelectedValue), ddlCuentaOrigen.SelectedItem.Text, (Usuario)Session["usuario"]);
            Int64 idCta = CuentaBanc.idctabancaria;

            //DATOS DE FORMA DE PAGO
            if (DropDownFormaDesembolso.SelectedItem.Text == "Transferencia")
            {
                pGiro.idctabancaria = idCta;
                pGiro.cod_banco = Convert.ToInt32(DropDownEntidad.SelectedValue);
                pGiro.num_cuenta = txtnumcuenta.Text;
                pGiro.tipo_cuenta = Convert.ToInt32(ddlTipoCuenta.SelectedValue);
            }
            else if (DropDownFormaDesembolso.SelectedItem.Text == "Cheque")
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
            pGiro.valor = Convert.ToInt64(txtValorAaplicar.Text.Replace("$", "").Replace(gSeparadorMiles, ""));
                
            //GRABACION DE DATOS
            Int64 pCOD_OPE = 0;
            TrasladoServicios.Crear_TrasladoDevolucionALL(ref pCOD_OPE, vOpe, lstTranslados, pGiro, (Usuario)Session["usuario"]);

            if (pCOD_OPE != 0)
            {
                // Se genera el comprobante
                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = vOpe.cod_ope;
                Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = tipoOpe;
                Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = vOpe.fecha_oper;
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] = vOpe.cod_ofi;
                Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = txtcodPersona.Text;
                //Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                mvAplicar.ActiveViewIndex = 1;
            }
            else
            {
                return false;
            }

        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(TrasladoServicios.CodigoPrograma, "btnContinuar_Click", ex);
            VerError(ex.Message);
            return false;
        }

        return true;
    }


    protected void CargarDropDown()
    {
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

        DropDownFormaDesembolso.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        DropDownFormaDesembolso.Items.Insert(1, new ListItem("Efectivo", "1"));
        DropDownFormaDesembolso.Items.Insert(2, new ListItem("Cheque", "2"));
        DropDownFormaDesembolso.Items.Insert(3, new ListItem("Transferencia", "3"));
        DropDownFormaDesembolso.SelectedIndex = 0;
        DropDownFormaDesembolso.DataBind();

        ddlTipoCuenta.Items.Insert(0, new ListItem("Ahorros", "0"));
        ddlTipoCuenta.Items.Insert(1, new ListItem("Corriente", "1"));
        ddlTipoCuenta.SelectedIndex = 0;
        ddlTipoCuenta.DataBind();

        CargarCuentas();
    }



    protected void DropDownFormaDesembolso_SelectedIndexChanged(object sender, EventArgs e)
    {
        ActivarDesembolso();
        if (DropDownFormaDesembolso.SelectedItem.Text == "Transferencia")
        {
            ActividadesServices ActividadServicio = new ActividadesServices();
            List<Xpinn.FabricaCreditos.Entities.CuentasBancarias> LstCuentasBanc = new List<Xpinn.FabricaCreditos.Entities.CuentasBancarias>();
            Int64 cod = Convert.ToInt64(txtcodPersona.Text);
            string filtro = " and Principal = 1";
            LstCuentasBanc = ActividadServicio.ConsultarCuentasBancarias(cod, filtro, (Usuario)Session["usuario"]);

            if (LstCuentasBanc.Count > 0 && LstCuentasBanc.Count == 1)
            {
                Session["LISTA"] = LstCuentasBanc;
                ctlMensaje.MostrarMensaje("Desea cargar la cuenta bancaria de la persona");
            }
        }
    }

    protected void ActivarDesembolso()
    {
        if (DropDownFormaDesembolso.SelectedItem.Text == "Transferencia")
        {
            panelDeDonde.Visible = true;
            panelhaciaDonde.Visible = true;
        }
        else if (DropDownFormaDesembolso.SelectedItem.Text == "Efectivo" || DropDownFormaDesembolso.SelectedIndex == 0)
        {
            panelhaciaDonde.Visible = false;
            panelDeDonde.Visible = false;
        }
        else if (DropDownFormaDesembolso.SelectedItem.Text == "Cheque")
        {
            panelDeDonde.Visible = true;
            panelhaciaDonde.Visible = false;
        }         
    }

    protected void ddlEntidadOrigen_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarCuentas();
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

    protected void gvDetalle_RowEditing(object sender, GridViewEditEventArgs e)
    {
        return;
    }


    protected void sumar() 
    {
        // Sumar los valorees
        decimal valor = 0;
        foreach (GridViewRow rFila in gvDetalle.Rows)
        {
            CheckBoxGrid chkTraslador = (CheckBoxGrid)rFila.FindControl("chkTraslador");
            if (chkTraslador != null)
            {
                if (chkTraslador.Checked == true)
                {
                    valor += Convert.ToDecimal(rFila.Cells[5].Text);
                }
            }
        }
        txtValorAaplicar.Text = valor.ToString();
    }

    protected void cambiar_valor(object sender, EventArgs e)
    {
      decimal valor = 0;
     foreach(GridViewRow rFila in gvDetalle.Rows)
        {
            CheckBoxGrid chkTraslador = (CheckBoxGrid)rFila.FindControl("chkTraslador");
            decimales txtValorTraslado = (decimales)rFila.FindControl("txtValorTraslado");
            if (chkTraslador != null)
            {
                if (chkTraslador.Checked == true)
                {
                    valor += Convert.ToDecimal(txtValorTraslado.Text);
                }
            }
        }
        txtValorAaplicar.Text = valor.ToString();
    
    }

    protected void chkTraslador_CheckedChanged(object sender, EventArgs e)
    {
        // Determinar si esta marcado
        CheckBoxGrid chkTrasladorO = (CheckBoxGrid)sender;
        int rowIndex = 0;
        if (chkTrasladorO == null)
            return;
        rowIndex = Convert.ToInt32(chkTrasladorO.CommandArgument);       
        decimales txtValorTraslado = (decimales)gvDetalle.Rows[rowIndex].FindControl("txtValorTraslado");
        if (chkTrasladorO.Checked)
            if (txtValorTraslado != null)
                txtValorTraslado.Enabled = true;
                txtValorTraslado.Text = gvDetalle.Rows[rowIndex].Cells[5].Text;
                
        // Sumar los valorees
        decimal valor = 0;
        foreach(GridViewRow rFila in gvDetalle.Rows)
        {
            CheckBoxGrid chkTraslador = (CheckBoxGrid)rFila.FindControl("chkTraslador");
            if (chkTraslador != null)
            {
                if (chkTraslador.Checked == true)
                {
                    valor += Convert.ToDecimal(rFila.Cells[5].Text);
                }
            }
        }
        txtValorAaplicar.Text = valor.ToString();
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

    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            panelGeneral.Visible = true;
            panelProceso.Visible = false;
            // Aquí va la función que hace lo que se requiera grabar en la funcionalidad
            AplicarDatos();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }



}
