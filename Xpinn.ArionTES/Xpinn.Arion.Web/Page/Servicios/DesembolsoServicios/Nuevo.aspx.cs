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
using Xpinn.Servicios.Services;
using Xpinn.Servicios.Entities;


public partial class Detalle : GlobalWeb
{

    AprobacionServiciosServices AprobacionServicios = new AprobacionServiciosServices();
    LineaServiciosServices BOLineaServ = new LineaServiciosServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AprobacionServicios.CodigoProgramaDesem, "D");

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
            BOexcepcion.Throw(AprobacionServicios.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                Session["Tipo_Servicio"] = null;
                Session["NumCred_Orden"] = null;
                panelGeneral.Enabled = false;
                Session["Beneficiario"] = null;
                CargarDropdown();
                mvAplicar.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;

                if (Session[AprobacionServicios.CodigoProgramaDesem + ".id"] != null)
                {
                    idObjeto = Session[AprobacionServicios.CodigoProgramaDesem + ".id"].ToString();
                    Session.Remove(AprobacionServicios.CodigoProgramaDesem + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionServicios.GetType().Name + "L", "Page_Load", ex);
        }

    }

    private void CargarDropdown()
    {
        PoblarLista("periodicidad", ddlPeriodicidad);
        PoblarLista("lineasservicios", ddlLinea);

        ddlFormaPago.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlFormaPago.Items.Insert(1, new ListItem("Caja", "1"));
        ddlFormaPago.Items.Insert(2, new ListItem("Nomina", "2"));
        ddlFormaPago.SelectedIndex = 0;
        ddlFormaPago.DataBind();
    }

    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodPersona", "txtIdPersona", "txtNomPersona");
    }

    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Servicio vDetalle = new Servicio();

            vDetalle = AprobacionServicios.ConsultarSERVICIO(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);

            if (vDetalle.numero_servicio != 0)
                txtCodigo.Text = vDetalle.numero_servicio.ToString().Trim();
            if (vDetalle.fecha_solicitud != DateTime.MinValue)
                txtFecha.Text = vDetalle.fecha_solicitud.ToString(gFormatoFecha).Trim();
            if (vDetalle.cod_persona != 0)
            {
                txtCodPersona.Text = vDetalle.cod_persona.ToString().Trim();
                txtIdPersona.Text = vDetalle.identificacion.ToString().Trim();
                if (vDetalle.nombre != null)
                    txtNomPersona.Text = vDetalle.nombre.ToString().Trim();
            }
            if (vDetalle.cod_linea_servicio != "")
                ddlLinea.SelectedValue = vDetalle.cod_linea_servicio;
            ddlLinea_SelectedIndexChanged(ddlLinea, null);

            if (vDetalle.cod_plan_servicio != "")
                ddlPlan.SelectedValue = vDetalle.cod_plan_servicio;
            if (vDetalle.valor_total != 0)
                txtValorTotal.Text = vDetalle.valor_total.ToString();
            if (vDetalle.numero_cuotas != 0)
                txtNumCuotas.Text = vDetalle.numero_cuotas.ToString();
            if (vDetalle.valor_cuota != 0)
                txtValorCuota.Text = vDetalle.valor_cuota.ToString();
            if (vDetalle.cod_periodicidad != 0)
                ddlPeriodicidad.Text = vDetalle.cod_periodicidad.ToString();
            if (vDetalle.forma_pago != "")
                ddlFormaPago.Text = vDetalle.forma_pago;
            if (vDetalle.codigo_proveedor != null && vDetalle.codigo_proveedor != 0)
                lblCodProveedor.Text = vDetalle.codigo_proveedor.ToString();
            if (vDetalle.identificacion_proveedor != "")
                txtIdentificacionTitu.Text = vDetalle.identificacion_proveedor;
            if (vDetalle.nombre_proveedor != "")
                txtNombreTit.Text = vDetalle.nombre_proveedor;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionServicios.CodigoProgramaDesem, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public Boolean ValidarDatos()
    {
        if (txtFecha.Text == "")
        {
            VerError("Seleccione la fecha de Solicitud");
            return false;
        }
        if (ddlLinea.SelectedValue == "0")
        {
            VerError("Seleccione la Linea de Servicio");
            return false;
        }
        if (txtValorTotal.Text == "")
        {
            VerError("Ingrese el Valor Total");
            return false;
        }
        if (txtCodPersona.Text == "")
        {
            VerError("Seleccione el Solicitante");
            return false;
        }
        if (ddlFormaPago.SelectedIndex == 0)
        {
            VerError("Seleccione la forma de pago");
            return false;
        }
        LineaServicios vDetalle = new LineaServicios();
        vDetalle = BOLineaServ.ConsultarLineaSERVICIO(ddlLinea.SelectedValue, (Usuario)Session["usuario"]);

        if (vDetalle.tipo_servicio != 5) //Si es diferente de Tipo Orden de Servicio
        {
            if (txtValorCuota.Text == "")
            {
                VerError("El servicio no cuenta con el valor de la cuota");
                return false;
            }
        }
        if (txtValorTotal.Text != "" && txtValorCuota.Text != "")
        {
            if (txtValorCuota.Text != "")
            {
                if (Convert.ToDecimal(txtValorCuota.Text) > Convert.ToDecimal(txtValorTotal.Text))
                {
                    VerError("El valor de la cuota no puede exceder al valor Total.");
                    return false;
                }
            }
        }
        if (txtNumPreImpreso.Visible == true)
        {
            if (txtNumPreImpreso.Text == "")
            {
                VerError("Ingrese el número de Pre Impresión.");
                txtNumPreImpreso.Focus();
                return false;
            }
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        // Validar que exista la parametrización contable por procesos
        if (ValidarProcesoContable(DateTime.Now, 110) == false)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación 110 = Desembolso de Servicios");
            return;
        }

        if (ValidarDatos())
        {
            ctlMensaje.MostrarMensaje("Desea grabar el Desembolso?");
        }
    }



    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            // Determinar código de proceso contable para generar el comprobante
            Int64? rpta = 0;
            if (!panelProceso.Visible && panelGeneral.Visible)
            {
                rpta = ctlproceso.Inicializar(110, DateTime.Now, (Usuario)Session["Usuario"]);
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
                    {
                        mvAplicar.ActiveViewIndex = 1;
                        Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                    }
                    else
                    { 
                        VerError("Se presentó error");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected bool AplicarDatos()
    {
        try
        { 
            //GRABACION DE LA OPERACION
            Usuario pUsu = (Usuario)Session["usuario"];
            Xpinn.Tesoreria.Services.OperacionServices xTesoreria = new Xpinn.Tesoreria.Services.OperacionServices();
            Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
            vOpe.cod_ope = 0;
            vOpe.tipo_ope = 110;
            vOpe.cod_caja = 0;
            vOpe.cod_cajero = 0;
            vOpe.observacion = "Desembolso de Servicio";
            vOpe.cod_proceso = null;
            vOpe.fecha_oper = DateTime.Now;
            vOpe.fecha_calc = DateTime.Now;

            //GRABAR TRANSACCION_SERVICIO Y ACTUALIZAR LA TABLA SERVICIOS
            DesembosoServicios pTran = new DesembosoServicios();
            pTran.numero_transaccion = 0;
            pTran.numero_servicio = Convert.ToInt32(txtCodigo.Text);
            pTran.cod_ope = 0;
            pTran.cod_cliente = Convert.ToInt64(txtCodPersona.Text);
            pTran.cod_linea_servicio = ddlLinea.SelectedValue;
            pTran.tipo_tran = 1;
            pTran.cod_det_lis = 0; // *
            pTran.cod_atr = 1; //*
            pTran.valor = Convert.ToDecimal(txtValorTotal.Text.Replace(".", ""));
            if (Session["Tipo_Servicio"] != null)
            {
                //pTran.valor = Session["Tipo_Servicio"].ToString() != "5" ? Convert.ToDecimal(txtValorTotal.Text.Replace(".","")) : Convert.ToDecimal(txtValorCuota.Text.Replace(".",""));
                if (Session["Tipo_Servicio"].ToString() != "5")
                {
                    pTran.valor = Convert.ToDecimal(txtValorTotal.Text.Replace(".", ""));
                }
                else
                {
                    if (txtValorCuota.Text != "")
                    {
                        pTran.valor = Convert.ToDecimal(txtValorCuota.Text.Replace(".", ""));
                    }
                    else
                    {
                        pTran.valor = Convert.ToDecimal(txtValorTotal.Text.Replace(".", ""));
                    }
                    
                }
                if (Session["Tipo_Servicio"].ToString() == "5") //Si es Tipo Orden de Servicio
                    Session["NumCred_Orden"] = txtCodigo.Text;
            }

            pTran.estado = 1;
            pTran.num_tran_anula = 0;//*
            if (txtNumPreImpreso.Visible == true)
                pTran.numero_preimpreso = Convert.ToInt64(txtNumPreImpreso.Text);

            Xpinn.Servicios.Services.DesembolsoServiciosServices Transervicio = new Xpinn.Servicios.Services.DesembolsoServiciosServices();
            DesembosoServicios pEntidad = new DesembosoServicios();
            pEntidad = Transervicio.CrearTransaccionDesembolso(pTran, vOpe, (Usuario)Session["usuario"]);

            //GENERAR EL COMPROBANTE
            string cod_titular = string.IsNullOrEmpty(lblCodProveedor.Text.Trim()) ? txtCodPersona.Text : lblCodProveedor.Text.Trim();
            //Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            //Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = pEntidad.cod_ope;
            //Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 110;            
            //Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = cod_titular; //"<Colocar Aquí el código de la persona del servicio>"
            //Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
            
            // Se cargan las variables requeridas para generar el comprobante
            ctlproceso.CargarVariables(pEntidad.cod_ope, 110, Convert.ToInt64(cod_titular), (Usuario)Session["usuario"]);

            return true;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionServicios.CodigoProgramaDesem, "btnContinuar_Click", ex);
            return false;
        }        

    }


    protected void txtIdPersona_TextChanged(object sender, EventArgs e)
    {
        Xpinn.Seguridad.Services.UsuarioService UsuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
        Xpinn.FabricaCreditos.Entities.Persona1 DatosPersona = new Xpinn.FabricaCreditos.Entities.Persona1();

        DatosPersona = UsuarioServicio.ConsultarPersona1(txtIdPersona.Text, (Usuario)Session["usuario"]);

        if (DatosPersona.cod_persona != 0)
        {
            if (DatosPersona.cod_persona != 0)
                txtCodPersona.Text = DatosPersona.cod_persona.ToString();
            if (DatosPersona.identificacion != "")
                txtIdPersona.Text = DatosPersona.identificacion;
            if (DatosPersona.nombre != "")
                txtNomPersona.Text = DatosPersona.nombre;
        }
        else
        {
            txtNomPersona.Text = "";
            txtCodPersona.Text = "";
        }
    }

    protected void ddlLinea_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLinea.SelectedIndex != 0)
        {

            LineaServicios vDetalle = new LineaServicios();
            vDetalle = BOLineaServ.ConsultarLineaSERVICIO(ddlLinea.SelectedValue, (Usuario)Session["usuario"]);
            if (vDetalle != null)
            {
                Session["Tipo_Servicio"] = vDetalle.tipo_servicio;
                if (vDetalle.tipo_servicio == 5) //Si es Tipo Orden de SErvicio
                {
                    lblPlan.Visible = false;
                    ddlPlan.Visible = false;
                    lblValorCuota.Visible = false;
                    txtValorCuota.Visible = false;
                    lblNumCuotas.Visible = false;
                    txtNumCuotas.Visible = false;
                    lblPeriodicidad.Visible = false;
                    ddlPeriodicidad.Visible = false;
                    lblNumPreImpreso.Visible = true;
                    txtNumPreImpreso.Visible = true;
                    Int64 pConsecutivo = 0;
                    pConsecutivo = AprobacionServicios.ObtenerNumeroPreImpreso((Usuario)Session["usuario"]);

                    txtNumPreImpreso.Text = pConsecutivo.ToString();
                }
                else
                {
                    lblPlan.Visible = true;
                    ddlPlan.Visible = true;

                    lblValorCuota.Visible = true;
                    txtValorCuota.Visible = true;
                    lblNumCuotas.Visible = true;
                    txtNumCuotas.Visible = true;
                    lblPeriodicidad.Visible = true;
                    ddlPeriodicidad.Visible = true;
                    lblNumPreImpreso.Visible = false;
                    txtNumPreImpreso.Visible = false;
                }
                if (vDetalle.no_requiere_aprobacion == 1)
                    Session["OPCION"] = 1;
                else
                    Session["OPCION"] = null;
            }

            List<Servicio> lstDatos = new List<Servicio>();
            lstDatos = AprobacionServicios.CargarPlanXLinea(Convert.ToInt32(ddlLinea.SelectedValue), (Usuario)Session["usuario"]);
            if (lstDatos.Count > 0)
            {
                ddlPlan.DataSource = lstDatos;
                ddlPlan.DataTextField = "NOMBRE";
                ddlPlan.DataValueField = "COD_PLAN_SERVICIO";
                ddlPlan.Items.Insert(0, new ListItem("Seleccione un item", "0"));
                ddlPlan.SelectedIndex = 0;
                ddlPlan.DataBind();
            }
            else
            {
                ddlPlan.Items.Clear();
                ddlPlan.DataBind();
            }

            Xpinn.Servicios.Services.SolicitudServiciosServices SolicServicios = new Xpinn.Servicios.Services.SolicitudServiciosServices();
            Servicio vData = new Servicio();
            vData = SolicServicios.ConsultaProveedorXlinea(Convert.ToInt32(ddlLinea.SelectedValue), (Usuario)Session["usuario"]);
            //if (vData.identificacion != "")
            //    txtIdentificacionTitu.Text = vData.identificacion;
            //if (vData.nombre != "")
            //    txtNombreTit.Text = vData.nombre;
        }
        else
        {
            ddlPlan.Items.Clear();
            ddlPlan.DataBind();
            //txtIdentificacionTitu.Text = "";
            //txtNombreTit.Text = "";
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
            AplicarDatos();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


}
