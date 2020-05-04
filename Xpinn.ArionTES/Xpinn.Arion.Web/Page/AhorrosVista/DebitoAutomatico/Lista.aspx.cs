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
using Xpinn.Ahorros.Services;
using Xpinn.Ahorros.Entities;
public partial class Lista : GlobalWeb
{
    private Xpinn.Ahorros.Services.AhorroVistaServices AhorroVistaServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
    private Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AhorroVistaServicio.CodigoProgramaDebCreditos, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaDebCreditos, "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtFecha.Text = DateTime.Now.ToString(gFormatoFecha);
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.GetType().Name + "A", "Page_Load", ex);
        }
    }



    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);
        Actualizar();
     }
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        Int64 total_creditos = 0;
        // Validar datos del gridview 
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBox chkseleccion = (CheckBox)rFila.FindControl("chkseleccion");
            if (chkseleccion != null)
                if (chkseleccion.Checked == true)
                {
                    TextBox txtTotalPago = (TextBox)rFila.FindControl("txtTotalPago");
                    if (txtTotalPago.Text != "0")
                    {
                        decimal valor_disponible = Convert.ToDecimal(rFila.Cells[11].Text);
                        decimal Total_pago = Convert.ToDecimal(txtTotalPago.Text);

                        if (Total_pago > valor_disponible)
                        {
                            txtTotalPago.Focus();
                            VerError("El valor a pagar no debe ser mayor al saldo disponible, intente nuevamente");
                            return;
                        }
                        else
                        {
                            total_creditos += 1;
                        }
                    }
                    else
                    {
                        txtTotalPago.Focus();
                        VerError("El valor a pagar no debe ser cero, intente nuevamente");
                        return;
                    }
                }
        }
        if (total_creditos > 0)
        {
            //VerError("");
            //if (ValidarProcesoContable(Convert.ToDateTime(txtFecha.Text), 50) == false)
            //{
            //    VerError("No se encontró parametrización contable por procesos para el tipo de operación 50 = Pago de creditos por ahorros");
            //    return;
            //}
            ctlMensaje.MostrarMensaje("Desea aplicar el pago de los creditos seleccionados?");
        }


     

    }
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        Configuracion conf = new Configuracion();
        Usuario pUsuario = (Usuario)Session["Usuario"];
        Int64 cod_ope = 0;
        Int64 pnum_comp = 0;
        Int64 ptipo_comp = 0;
        Int64 pcod_persona = 0;
        Int64 pcod_proceso = 0;
        string pError = "";
        // Realizar la transaccion del Credito-Debito en una cuenta de ahorros 
        List<CreditoDebAhorros> ListCreditoDebAhorros = new List<CreditoDebAhorros>();
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBox chkseleccion = (CheckBox)rFila.FindControl("chkseleccion");
                if (chkseleccion != null)
                    if (chkseleccion.Checked == true)
                   {
                    // realizar retiro y aplicacion de pago de la cuenta
                    TextBox txtTotalPago = (TextBox)rFila.FindControl("txtTotalPago");
                    Xpinn.Ahorros.Entities.CreditoDebAhorros CreditoDebAhorros = new CreditoDebAhorros();
                    CreditoDebAhorros.numero_cuenta = rFila.Cells[9].Text ;
                    CreditoDebAhorros.fecha_proximo_pago = Convert.ToDateTime(txtFecha.Text);
                    CreditoDebAhorros.valor_pagar = Convert.ToDecimal(txtTotalPago.Text);
                    CreditoDebAhorros.numero_radicacion = Convert.ToInt64(rFila.Cells[0].Text);
                    HiddenField cod_cliente = (HiddenField)rFila.FindControl("cod_cliente");
                    CreditoDebAhorros.cod_cliente = Convert.ToInt64(cod_cliente.Value);
                    ListCreditoDebAhorros.Add(CreditoDebAhorros);
                    }
        }
        Boolean validar = AhorroVistaServicio.AplicarCréditoDebAhorros(ListCreditoDebAhorros, Convert.ToDateTime(txtFecha.Text), ref cod_ope, pUsuario);
        
        // Determinar código de proceso contable para generar el comprobante
        Int64? rpta = 0;
            if (!panelProceso.Visible && mvPrincipal.Visible)
            {
                rpta = ctlproceso.Inicializar(50, Convert.ToDateTime(txtFecha.Text), (Usuario)Session["Usuario"]);
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
                if (validar)
                {
                    Boolean result = ComprobanteServicio.GenerarComprobante(cod_ope, 50, Convert.ToDateTime(txtFecha.Text), pUsuario.cod_oficina, pcod_persona, pcod_proceso, ref pnum_comp, ref ptipo_comp, ref pError, pUsuario);
                    if (result)
                    {
                        Actualizar();
                        Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = pnum_comp;
                        Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = ptipo_comp;
                        Response.Redirect("../../Contabilidad/Comprobante/Nuevo.aspx");
                    }
                }
            }
            }








           
       

    }
    
    private void Actualizar()
    {
        try
        {
            Usuario pUsuario = (Usuario)Session["Usuario"];
            List<Xpinn.Ahorros.Entities.CreditoDebAhorros> lstCreditoDebAhorros = new List<CreditoDebAhorros>();
            lstCreditoDebAhorros = AhorroVistaServicio.ListarCreditoDebAhorros(pUsuario);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstCreditoDebAhorros;

            if (lstCreditoDebAhorros.Count > 0)
            {
                Int64 total_aplicado = 0;
                pDatos.Visible = true;
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstCreditoDebAhorros.Count.ToString();
                gvLista.DataBind();
                lblTotalAplicar.Visible = true;
                txtTotalAplicado.Visible = true;
                foreach (GridViewRow rFila in gvLista.Rows)
                {
                    TextBox txtTotalPago = (TextBox)rFila.FindControl("txtTotalPago");
                    HiddenField cod_cliente = (HiddenField)rFila.FindControl("cod_cliente");
                    cod_cliente.Value = lstCreditoDebAhorros[rFila.DataItemIndex].cod_cliente.ToString();
                    txtTotalPago.Text = AhorroVistaServicio.Calcular_VrAPagar(Convert.ToInt64(rFila.Cells[0].Text), txtFecha.Text, pUsuario).ToString();
                    total_aplicado += Convert.ToInt64(txtTotalPago.Text);

                }
                txtTotalAplicado.Text = total_aplicado.ToString();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                lblTotalAplicar.Visible = false;
                txtTotalAplicado.Visible = false;
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
                pDatos.Visible = false;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;

            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaDebCreditos, "Actualizar", ex);
        }
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
          //  AplicarDatos();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }
   


}