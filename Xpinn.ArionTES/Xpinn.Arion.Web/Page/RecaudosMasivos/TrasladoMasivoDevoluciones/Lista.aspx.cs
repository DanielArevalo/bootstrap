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
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;

partial class Lista : GlobalWeb
{
    TrasladoDevolucionServices TrasladoServicios = new TrasladoDevolucionServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(TrasladoServicios.CodigoProgramaMasivo, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TrasladoServicios.CodigoProgramaMasivo, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvDesembolsoMasivo.ActiveViewIndex = 0;
                CargarDropDown();
                Actualizar();
                ddlForma_Desem_SelectedIndexChanged(ddlForma_Desem, null);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TrasladoServicios.CodigoProgramaMasivo, "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {

        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            Actualizar();
        }
    }

    void CargarDropDown()
    {
        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
        ddlEntidad.DataSource = bancoService.ListarBancos(banco, (Usuario)Session["usuario"]);
        ddlEntidad.DataTextField = "nombrebanco";
        ddlEntidad.DataValueField = "cod_banco";
        ddlEntidad.DataBind();

        ddlEntidad_giro.DataSource = bancoService.ListarBancosEntidad(banco, (Usuario)Session["usuario"]);
        ddlEntidad_giro.DataTextField = "nombrebanco";
        ddlEntidad_giro.DataValueField = "cod_banco";
        ddlEntidad_giro.DataBind();

        ddlForma_Desem.Items.Insert(0, new ListItem("Seleccione un Item", "0"));
        ddlForma_Desem.Items.Insert(1, new ListItem("Efectivo", "1"));
        ddlForma_Desem.Items.Insert(2, new ListItem("Cheque", "2"));
        ddlForma_Desem.Items.Insert(3, new ListItem("Transferencia", "3"));
        ddlForma_Desem.SelectedIndex = 0;
        ddlForma_Desem.DataBind();

        ddlTipo_cuenta.Items.Insert(0, new ListItem("Ahorros", "0"));
        ddlTipo_cuenta.Items.Insert(1, new ListItem("Corriente", "1"));
        ddlTipo_cuenta.SelectedIndex = 0;
        ddlTipo_cuenta.DataBind();

        CargarCuentas();
    }

    void CargarCuentas()
    {
        Int64 codbanco = 0;
        try
        {
            codbanco = Convert.ToInt64(ddlEntidad_giro.SelectedValue);
        }
        catch
        {
        }
        if (codbanco != 0)
        {
            Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
            Usuario usuario = (Usuario)Session["usuario"];
            ddlCuenta_Giro.DataSource = bancoService.ListarCuentaBancos(codbanco, usuario);
            ddlCuenta_Giro.DataTextField = "num_cuenta";
            ddlCuenta_Giro.DataValueField = "idctabancaria";
            ddlCuenta_Giro.DataBind();
        }
    }


    Boolean ValidarDatos()
    {
        VerError("");
        if (ddlForma_Desem.SelectedIndex == 0)
        {
            VerError("Seleccione la forma de pago");
            return false;
        }
        if (ddlForma_Desem.SelectedItem.Text == "Transferencia")
        {
            if (txtNum_cuenta.Text == "")
            {
                VerError("Ingrese el número de cuenta");
                return false;
            }
        }
        int cont = 0;
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
            if (cbSeleccionar != null)
            {
                if (cbSeleccionar.Checked == true)
                    cont++;
            }
        }
        if (cont == 0)
        {
            VerError("No existen desembolsos seleccionados");
            return false;
        }
        // Validar que exista la parametrización contable por procesos
        if (ValidarProcesoContable(DateTime.Now, 58) == false)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación 58=Traslado de Devoluciones");
            return false;
        }

        return true;
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ValidarDatos())
            {
                ctlMensaje.MostrarMensaje("Desea realizar el traslado de los registros seleccionados ?");
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TrasladoServicios.CodigoProgramaMasivo, "btnGuardar_Click", ex);
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        Xpinn.Tesoreria.Services.OperacionServices operacionServicio = new Xpinn.Tesoreria.Services.OperacionServices();
        Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
        Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
        // Variables
        DateTime fechaTraslado = DateTime.Now; 
        Usuario usuario = new Usuario();
        if (Session["usuario"] == null)
            return;
        usuario = (Usuario)Session["usuario"];
        string sError = "";
        List<Xpinn.Tesoreria.Entities.Operacion> lstConsulta = new List<Xpinn.Tesoreria.Entities.Operacion>();

        // Determinar el proceso del desembolso      
        Xpinn.Contabilidad.Entities.ProcesoContable eproceso = new Xpinn.Contabilidad.Entities.ProcesoContable();
        eproceso = ConsultarProcesoContable(58, ref sError, usuario);
        if (eproceso == null && sError.Trim() != "")
        {
            VerError("No hay ningún proceso contable parametrizado para el desembolso masivo de devoluciones");
            return;
        }

        // Realizar el proceso para cada devolución seleccionada
        try
        {
            int cont = 0;
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
                if (cbSeleccionar != null)
                {
                    if (cbSeleccionar.Checked == true)
                    {
                        Int64 pnum_comp = 0, ptipo_comp = 0;
                        //GRABACION DE LA OPERACION                   
                        Operacion vOpe = new Operacion();
                        vOpe.cod_ope = 0;
                        vOpe.tipo_ope = 58;
                        vOpe.cod_caja = 0;
                        vOpe.cod_cajero = 0;
                        vOpe.observacion = "Grabacion de operacion-Traslado devolucion";
                        vOpe.cod_proceso = null;
                        vOpe.fecha_oper = fechaTraslado;
                        vOpe.fecha_calc = fechaTraslado;
                        vOpe.cod_ofi = usuario.cod_oficina;
                        operacionServicio.GrabarOperacion(vOpe, usuario);
                        Int64 cod_operacion = vOpe.cod_ope;

                        //GRABACION DEL TRASLADO DE LA DEVOLUCION 
                        TrasladoDevolucion pTras = new TrasladoDevolucion();
                        pTras.numero_transaccion = 0;
                        pTras.cod_ope = cod_operacion;
                        pTras.num_devolucion = Convert.ToInt32(rFila.Cells[1].Text);
                        try
                        {
                            pTras.valor = Convert.ToDecimal(rFila.Cells[8].Text.Replace("$", "").Replace(gSeparadorMiles, ""));
                        }
                        catch
                        {
                            VerError("No pudo determinar valor de la devolución");
                            return;
                        }
                        pTras.tipo_tran = 904;
                        pTras.estado = 1;
                        TrasladoServicios.Crear_TrasladoDevolucion(pTras, usuario);

                        //GRABACION DEL GIRO A REALIZAR
                        Xpinn.FabricaCreditos.Services.AvanceService AvancServices = new Xpinn.FabricaCreditos.Services.AvanceService();
                        Xpinn.FabricaCreditos.Entities.Giro pGiro = new Xpinn.FabricaCreditos.Entities.Giro();
                        pGiro.idgiro = 0;
                        pGiro.cod_persona = Convert.ToInt64(rFila.Cells[3].Text);
                        pGiro.forma_pago = Convert.ToInt32(ddlForma_Desem.SelectedValue);
                        pGiro.tipo_acto = 3; //DEVOLUCIONES TIPO_ACTO_GIRO
                        pGiro.cod_ope = Convert.ToInt64(cod_operacion);
                        pGiro.fec_reg = DateTime.Now;
                        pGiro.fec_giro = DateTime.Now;
                        pGiro.numero_radicacion = Convert.ToInt64(rFila.Cells[1].Text);// NO ENVIO EL NUMERO DE RADICACION SINO EL NUMERO DE DEVOLUCION
                        pGiro.usu_gen = usuario.nombre;
                        pGiro.usu_apli = null;
                        pGiro.estadogi = 1;
                        pGiro.usu_apro = null;

                        //DETERMINAR LA IDENTIFICACIÓN DE LA CUENTA BANCARIA
                        CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ddlEntidad_giro.SelectedValue), ddlCuenta_Giro.SelectedItem.Text, usuario);
                        Int64 idCta = CuentaBanc.idctabancaria;

                        //DATOS DE FORMA DE PAGO
                        if (ddlForma_Desem.SelectedItem.Text == "Transferencia")
                        {
                            pGiro.idctabancaria = idCta;
                            pGiro.cod_banco = Convert.ToInt32(ddlForma_Desem.SelectedValue);
                            pGiro.num_cuenta = txtNum_cuenta.Text;
                            pGiro.tipo_cuenta = Convert.ToInt32(ddlTipo_cuenta.SelectedValue);
                        }
                        else if (ddlForma_Desem.SelectedItem.Text == "Cheque")
                        {
                            pGiro.idctabancaria = idCta;
                            pGiro.cod_banco = 0;        //NULO
                            pGiro.num_cuenta = null;    //NULO
                            pGiro.tipo_cuenta = -1;      //NULO
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
                        pGiro.valor = Convert.ToInt64(rFila.Cells[8].Text.Replace("$", "").Replace(gSeparadorMiles, ""));
                        AvancServices.CrearGiro(pGiro, usuario, 1);

                        // GENERACION DE COMPROBANTES
                        string Error = "";
                        Xpinn.Contabilidad.Services.ComprobanteService comprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                        if (comprobanteServicio.GenerarComprobante(cod_operacion, 58, fechaTraslado, usuario.cod_oficina, Convert.ToInt64(rFila.Cells[3].Text), eproceso.cod_proceso, ref pnum_comp, ref ptipo_comp, ref Error, usuario))
                        {
                            vOpe.num_comp = pnum_comp;
                            vOpe.tipo_comp = ptipo_comp;
                            lstConsulta.Add(vOpe);
                        }  

                        cont++;
                    }
                }
            }

            // Actualizar listado de devoluciones y mostrar los comprobantes generados
            if (cont > 0)
            {
                Actualizar();
                gvOperacion.DataSource = lstConsulta;
                gvOperacion.DataBind();
                mvDesembolsoMasivo.ActiveViewIndex = 1;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TrasladoServicios.CodigoProgramaMasivo, "btnContinuar_Click", ex);
        }
    }



    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TrasladoServicios.CodigoProgramaMasivo, "gvLista_PageIndexChanging", ex);
        }
    }
    
    
    private void Actualizar()
    {
        try
        {
            List<TrasladoDevolucion> lstConsulta = new List<TrasladoDevolucion>();
            String filtro = obtFiltro();
            String orden = "";
            lstConsulta = TrasladoServicios.ListarTrasladoDevolucion(orden, filtro,(Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;

            Site toolBar = (Site)this.Master;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;                
                lblTotalRegs.Visible = true;
                toolBar.MostrarGuardar(true);
            }
            else
            {
                panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
                toolBar.MostrarGuardar(false);
            }
            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();

            Session.Add(TrasladoServicios.CodigoProgramaMasivo + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TrasladoServicios.CodigoProgramaMasivo, "Actualizar", ex);
        }
    }
      

    private string obtFiltro()
    {        
        String filtro = String.Empty;

        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and D.IDENTIFICACION = " + txtIdentificacion.Text.Trim();
       
        if (txtNombre.Text.Trim() != "")
            filtro += " and V.PRIMER_NOMBRE ||' '|| V.SEGUNDO_NOMBRE ||' '||V.PRIMER_APELLIDO ||' '||V.SEGUNDO_APELLIDO like '%" + txtNombre.Text.Trim() + "%'";

        if (txtCodigoNomina.Text != "")
            filtro += " and cod_nomina = '" + txtCodigoNomina.Text + "'";

        return filtro;
    }


    void ActivarDesembolso()
    {
        if (ddlForma_Desem.SelectedItem.Text == "Transferencia")
        {
            panelCheque.Visible = true;
            panelTrans.Visible = true;
        }
        else if (ddlForma_Desem.SelectedItem.Text == "Efectivo" || ddlForma_Desem.SelectedIndex == 0)
        {
            panelCheque.Visible = false;
            panelTrans.Visible = false;
        }
        else if (ddlForma_Desem.SelectedItem.Text == "Cheque")
        {
            panelCheque.Visible = true;
            panelTrans.Visible = false;
        }         
    }


    protected void ddlForma_Desem_SelectedIndexChanged(object sender, EventArgs e)
    {
        ActivarDesembolso();        
    }


    protected void ddlEntidad_giro_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarCuentas();
    }


    protected void cbSeleccionarEncabezado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbSeleccionarEncabezado = (CheckBox)sender;
        if (cbSeleccionarEncabezado != null)
        {
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
                cbSeleccionar.Checked = cbSeleccionarEncabezado.Checked;
            }
        }
    }

    protected void gvOperacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }
}