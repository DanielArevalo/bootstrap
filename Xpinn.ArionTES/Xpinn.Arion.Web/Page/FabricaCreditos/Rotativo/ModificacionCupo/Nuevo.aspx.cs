using System;
using System.Diagnostics;
using System.Web;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Configuration;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Services;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Design;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Web.UI;

partial class Nuevo : GlobalWeb
{
    CreditoService CreditoServicio = new CreditoService();
    AvanceService AvanServicios = new AvanceService();


    #region Carga Inicial

    /// <summary>
    /// Mostrar la barra de herramientas al ingresar a la funcionalidad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[AvanServicios.CodigoProgramaModCupo + ".id"] != null)
                VisualizarOpciones(AvanServicios.CodigoProgramaModCupo, "E");
            else
                VisualizarOpciones(AvanServicios.CodigoProgramaModCupo, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AvanServicios.CodigoProgramaModCupo, "Page_PreInit", ex);
        }
    }


    /// <summary>
    /// Cargar datos de la funcionalidad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Permite modificar cierta información cuando es perfil ADMINISTRADOR.
            bool bHabilitar = false;
            //if (((Usuario)Session["usuario"]).codperfil == 1)
                bHabilitar = true;
          
            if (!IsPostBack)
            {                
                // Determinar si es superusuario para modificar saldo de capital del crédito
                if (Session[AvanServicios.CodigoProgramaModCupo + ".id"] != null)
                {
                    idObjeto = Session[AvanServicios.CodigoProgramaModCupo + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                    mvCredito.ActiveViewIndex = 0;                    
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AvanServicios.CodigoProgramaModCupo, "Page_Load", ex);
        }
    }

    #endregion


    #region Obtencion de Datos

    /// <summary>
    /// Mostrar los datos del crédito
    /// </summary>
    /// <param name="pIdObjeto"></param>
    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Configuracion conf = new Configuracion();
            Credito vCredito = new Credito();

            vCredito = CreditoServicio.ConsultarCreditoModCupoRotativo(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vCredito.numero_radicacion != Int64.MinValue)
                txtNumero_radicacion.Text = vCredito.numero_radicacion.ToString().Trim();
            if (vCredito.identificacion != string.Empty)
                txtIdentificacion.Text = HttpUtility.HtmlDecode(vCredito.identificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.tipo_identificacion))
                txtTipo_identificacion.Text = HttpUtility.HtmlDecode(vCredito.tipo_identificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.nombre))
                txtNombre.Text = HttpUtility.HtmlDecode(vCredito.nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.linea_credito))
                txtLinea_credito.Text = HttpUtility.HtmlDecode(vCredito.linea_credito.ToString().Trim());
            if (vCredito.monto != Int64.MinValue)
                txtMonto.Text = HttpUtility.HtmlDecode(vCredito.monto.ToString().Trim());
            if (vCredito.plazo != Int64.MinValue)
                txtPlazo.Text = HttpUtility.HtmlDecode(vCredito.plazo.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.periodicidad))
                txtPeriodicidad.Text = HttpUtility.HtmlDecode(vCredito.periodicidad.ToString().Trim());
            if (vCredito.valor_cuota != Int64.MinValue)
                txtValor_cuota.Text = HttpUtility.HtmlDecode(vCredito.valor_cuota.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.forma_pago))
                txtForma_pago.Text = HttpUtility.HtmlDecode(vCredito.forma_pago.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.estado))
                if (vCredito.estado == "A")
                    txtEstado.Text = "Aprobado";
                else if ((vCredito.estado == "G"))
                    txtEstado.Text = "Generado";
                else if ((vCredito.estado == "C"))
                    txtEstado.Text = "Desembolsado";
                else
                    txtEstado.Text = vCredito.estado;
            if (!string.IsNullOrEmpty(vCredito.moneda))
                txtMoneda.Text = HttpUtility.HtmlDecode(vCredito.moneda.ToString().Trim());
            if (vCredito.saldo_capital != Int64.MinValue)
                txtSaldoCapital.Text = HttpUtility.HtmlDecode(vCredito.saldo_capital.ToString().Trim());
            if (vCredito.fecha_aprobacion != DateTime.MinValue)
                txtFechaAprobacion.Text = HttpUtility.HtmlDecode(Convert.ToDateTime(vCredito.fecha_aprobacion).ToString(GlobalWeb.gFormatoFecha).Trim());
            if (vCredito.fecha_ultimo_pago != DateTime.MinValue)
                txtFechaUltimoPago.Text = HttpUtility.HtmlDecode(vCredito.fecha_ultimo_pago.ToString(GlobalWeb.gFormatoFecha).Trim());
          

           
        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(AvanServicios.CodigoProgramaModCupo, "ObtenerDatos", ex);
        }
    }

 
    
    #endregion


    #region Evento Botones


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        if(ValidarDatos())
            ctlMensaje.MostrarMensaje("Desea realizar la modificación?");
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Credito vCredito = new Credito();
            vCredito.lstAmortizaCre = new List<AmortizaCre>();
            // Cargar datos a modificar del crédito
            vCredito.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);
            vCredito.monto_aprobado = Convert.ToDecimal(txtMonto.Text.Replace(gSeparadorMiles, "").Replace(".", gSeparadorDecimal));
            // Modifica el credito OJO A veces puede Crear en vez de modificar
            // con otras entidades como AmortizaCre
            vCredito = CreditoServicio.ModificarCupoRotativo(vCredito, (Usuario)Session["usuario"]);


            mvCredito.ActiveViewIndex = 1;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

  
    /// <summary>
    /// Evento para cancelar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    #endregion

    

    #region Metodos Validaciones

    private bool ValidarCamposEntidadAmor(AmortizaCre eAmor, out string error)
    {
        error = string.Empty;

        // Si hay una fila totalmente vacia, la deja pasar y la ignora luego en Data al grabar
        if (eAmor.valor == null && eAmor.saldo == null && eAmor.fecha_cuota == DateTime.MinValue)
        {
            return true;
        }
        if (eAmor.valor == null && eAmor.saldo == null)
        {
            error = "Hay campos sin valores en valor y saldo";
            return false;
        }
        if (eAmor.valor == null)
        {
            error = "Hay campos sin valores en valor";
            return false;
        }
        if (eAmor.saldo == null)
        {
            error = "Hay campos sin valores en saldo";
            return false;
        }
        if (eAmor.fecha_cuota == DateTime.MinValue)
        {
            error = "Hay campos sin una fecha valida";
            return false;
        }
        if (eAmor.valor < eAmor.saldo)
        {
            error = "Hay campos con saldo mayor al valor";
            return false;
        }

        return true;
    }

    protected Boolean ValidarDatos()
    {
        if (txtMonto.Text == "")
        {
            VerError("Ingrese el valor aprobado");
            return false;
        }
       
        return true;
    }

    #endregion

    

    #region Metodos Obtener Valor Seleccionado De Un Control

    protected Int32? ValorSeleccionado(DropDownList ddlControl)
    {
        if (ddlControl != null)
            if (ddlControl.SelectedValue != null)
                if (ddlControl.SelectedValue != "")
                    return Convert.ToInt32(ddlControl.SelectedValue);
        return null;
    }

    protected decimal? ValorSeleccionado(TextBox txtControl)
    {
        if (txtControl != null)
            if (txtControl.Text != null)
                if (txtControl.Text != "")
                    return ConvertirStringToDecimal(txtControl.Text);
        return null;
    }

    protected Int32? ValorSeleccionado(Label txtControl)
    {
        if (txtControl != null)
            if (txtControl.Text != null)
                if (txtControl.Text != "")
                    return Convert.ToInt32(txtControl.Text);
        return null;
    }

    protected int ValorSeleccionado(CheckBox txtControl)
    {
        if (txtControl != null)
            if (txtControl.Checked != null)
                return Convert.ToInt32(txtControl.Checked);
        return 0;
    }

    #endregion

}