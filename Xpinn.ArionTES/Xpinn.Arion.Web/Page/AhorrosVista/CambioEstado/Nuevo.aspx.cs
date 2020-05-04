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
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Ahorros.Services.AhorroVistaServices ahorrosServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    int tipoOpe = 143;
    DateTime FechaAct = DateTime.Now;
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ahorrosServicio.CodigoProgramaCie + ".id"] != null)
                VisualizarOpciones(ahorrosServicio.CodigoProgramaCambioEstado, "E");
            else
                VisualizarOpciones(ahorrosServicio.CodigoProgramaCambioEstado, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.MostrarConsultar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaCambioEstado, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
         
            if (!IsPostBack)
            {
                mvAhorroVista.ActiveViewIndex = 0;
                txtFechaCambio.ToDateTime = DateTime.Now;
                txtNumeroCuenta.Enabled = false;
                CargarListas();
                if (Session[ahorrosServicio.CodigoProgramaCambioEstado + ".id"] != null)
                {
                    idObjeto = Session[ahorrosServicio.CodigoProgramaCambioEstado + ".id"].ToString();
                    Session.Remove(ahorrosServicio.CodigoProgramaCambioEstado + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    Navegar(Pagina.Lista);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaCambioEstado, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (ValidarProcesoContable(FechaAct, tipoOpe) == false)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación 143 = Cambio estado Cuentas de Ahorro");
            return;
        }
        // Determinar código de proceso contable para generar el comprobante
        Int64? rpta = 0;
        if (!panelProceso.Visible)
        {
            rpta = ctlproceso.Inicializar(tipoOpe, FechaAct, (Usuario)Session["Usuario"]);
            if (rpta > 1)
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                // Activar demás botones que se requieran
                 panelGeneral.Visible = false;
               panelProceso.Visible = true;
                mvAhorroVista.SetActiveView(vwDatos);
            }
            else
            {
                ///verifica que todo este llenado
                if (ddlEstado.SelectedItem.Text == TxtEstado.Text)
                {
                    VerError("Seleccione un estado diferente al actual");
                    return;
                }
                if (txtMotivo.Text == "")
                {
                    VerError("Escriba un motivo para el cambio de estado");
                    return;
                }
                ctlMensaje.MostrarMensaje("Desea guardar los datos del Cambio de Estado?");

            }
        }
        else
            VerError("Se presentó error");     

       
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            ///carga todo a una entodad vAhorroVista en AhorroVista
            Usuario vUsuario = new Usuario();
            vUsuario = (Usuario)Session["Usuario"];

            Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();

            if (idObjeto != "")
                vAhorroVista = ahorrosServicio.ConsultarAhorroVista(idObjeto, (Usuario)Session["usuario"]);

            //if (lblConsecutivo.Text != "")
            //    vAhorroVista.consecutivo = Convert.ToInt64(lblConsecutivo.Text);
            //else
            //    vAhorroVista.consecutivo = 0;            
            //vAhorroVista.clase = Convert.ToInt32(ddlClase.SelectedValue);
            //vAhorroVista.txtNumeroCuenta = Convert.ToInt32(ddlTipo.SelectedValue);
            //vAhorroVista.cod_ubica = Convert.ToInt32(ddlUbicacion.SelectedValue);
            //vAhorroVista.cod_costo = Convert.ToInt32(ddlCentroCosto.SelectedValue);
            vAhorroVista.numero_cuenta = txtNumeroCuenta.Text;
            if (txtFechaApertura.Text == "")
            {
                VerError("Ingrese la fecha de apertura");
                return;
            }
            ///carga la entidad Ahorrovista
            vAhorroVista.fecha = Convert.ToDateTime(txtFechaApertura.Text);
            vAhorroVista.estado = Convert.ToInt32(ddlEstado.SelectedValue);
            vAhorroVista.codusuario = vUsuario.codusuario;

            vAhorroVista.motivos = txtMotivo.Text;

            Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
            Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
            Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
            Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
            var cod_operacion = 0L;
            
            // CREAR OPERACION
            pOperacion.cod_ope = 0;
            pOperacion.tipo_ope = tipoOpe;
            pOperacion.cod_caja = 0;
            pOperacion.cod_cajero = 0;
            pOperacion.observacion = "Cambio estado Cuentas de Ahorro";
            pOperacion.cod_proceso = null;
            pOperacion.fecha_oper = Convert.ToDateTime(FechaAct.ToShortDateString());
            pOperacion.fecha_calc = DateTime.Now;
            pOperacion.cod_ofi = Usuario.cod_oficina;

            if (idObjeto != "")
            {
                vAhorroVista.numero_cuenta = Convert.ToString(idObjeto);
                ahorrosServicio.ModificarCambioEstados(vAhorroVista, (Usuario)Session["usuario"], pOperacion);
            }
            cod_operacion = pOperacion.cod_ope;

            var usu = (Usuario)Session["usuario"];

            // Generar el comprobante
           // if (pOperacion.cod_ope != 0)
            //{
              //  ctlproceso.CargarVariables(pOperacion.cod_ope, tipoOpe, usu.codusuario, (Usuario)Session["usuario"]);
               // Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
            //}

            mvAhorroVista.ActiveViewIndex = 1;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            VerError("");
           toolBar.MostrarCancelar(false);
            toolBar.MostrarConsultar(true);       
           
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaCambioEstado, "btnGuardar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();
            vAhorroVista = ahorrosServicio.ConsultarAhorroVista(Convert.ToString(pIdObjeto), (Usuario)Session["usuario"]);
            ///carga los datos de vAhorroVista

            if (!string.IsNullOrEmpty(vAhorroVista.numero_cuenta.ToString()))
                txtNumeroCuenta.Text = HttpUtility.HtmlDecode(vAhorroVista.numero_cuenta.ToString());
            
            //numero de cuenta

            if (!string.IsNullOrEmpty(vAhorroVista.cod_linea_ahorro.ToString()))
                ddlEstado.SelectedValue = HttpUtility.HtmlDecode(vAhorroVista.cod_linea_ahorro.ToString().Trim());

            //linea de ahorro
            if (!string.IsNullOrEmpty(vAhorroVista.nombres))
              txtNombre.Text = HttpUtility.HtmlDecode(vAhorroVista.nombres.ToString());

            //nombres
            if (!string.IsNullOrEmpty(vAhorroVista.nom_linea))
                txtNombreLinea.Text = HttpUtility.HtmlDecode(vAhorroVista.nom_linea.ToString());

            //nombre linea

            if (!string.IsNullOrEmpty(vAhorroVista.estados.ToString()))
                TxtEstado.Text = HttpUtility.HtmlDecode(vAhorroVista.estados.ToString().Trim());

            //estado
           
            if (!string.IsNullOrEmpty(vAhorroVista.fecha_apertura.ToString()))
                txtFechaApertura.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vAhorroVista.fecha_apertura.ToString()));

            //Fecha de apertura

            if (!string.IsNullOrEmpty(vAhorroVista.saldo_total.ToString()))
                txtsaldo_total.Text = HttpUtility.HtmlDecode(vAhorroVista.saldo_total.ToString());

            //saldo total

            if (!string.IsNullOrEmpty(vAhorroVista.identificacion.ToString()))
                TxtIdentif.Text = HttpUtility.HtmlDecode(vAhorroVista.tipo_identificacion.ToString().Trim());   

           //tipo nidentificacion

            if (!string.IsNullOrEmpty(vAhorroVista.identificacion.ToString()))
               Txtiden.Text= HttpUtility.HtmlDecode(vAhorroVista.identificacion.ToString().Trim());

            if (vAhorroVista.cod_linea_ahorro != null)
                txtLinea.Text = vAhorroVista.cod_linea_ahorro;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaCambioEstado, "ObtenerDatos", ex);
        }
    }
    private void CargarListas()
    {
        ///carga las listas a la session
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        try
        {
            //ddlProveedor.DataTextField = "nombre";
            //ddlProveedor.DataValueField = "cod_persona";
            //ddlProveedor.DataSource = personaServicio.ListadoPersonas1(persona, pUsuario);
            //ddlProveedor.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    #region Titulares

    /// <summary>
    /// Método para instar un detalle en blanco para cuando la grilla no tiene datos
    /// </summary>
    /// <param name="consecutivo"></param>
   

    /// <summary>
    /// Método para cambio de página
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    

    protected void ActualizarDetalle()
    {
       // List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
        // LstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];
        //gvDetMovs.DataSource = LstDetalleComprobante;
        //gvDetMovs.DataBind();
    }



    /// <summary>
    /// Método para borrar un registro de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    
    
       
    #endregion

    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

}
