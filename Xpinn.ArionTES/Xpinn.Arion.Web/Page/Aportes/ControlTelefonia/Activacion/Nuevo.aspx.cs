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
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Services;

public partial class Nuevo : GlobalWeb
{

    SolicitudServiciosServices SolicServicios = new SolicitudServiciosServices();
    LineaServiciosServices BOLineaServ = new LineaServiciosServices();
    PlanesTelefonicosService PlanServic = new PlanesTelefonicosService();
    PoblarListas poblar = new PoblarListas();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(SolicServicios.CodigoProgramaActivacion, "E");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicServicios.GetType().Name + "E", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();

            if (!IsPostBack)
            {
                CargarDropDown();
                mvAplicar.ActiveViewIndex = 0;

                if (Session[SolicServicios.CodigoProgramaActivacion + ".id"] != null)
                {
                    idObjeto = Session[SolicServicios.CodigoProgramaActivacion + ".id"].ToString();
                    Session.Remove(SolicServicios.CodigoProgramaActivacion + ".id");
                    ObtenerDatos(idObjeto);
                    ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
                    lblmsj.Text = "registrada";
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicServicios.GetType().Name + "E", "Page_Load", ex);
        }
    }

    private void CargarDropDown()
    {
        poblar.PoblarListaDesplegable("lineasservicios", "COD_LINEA_SERVICIO, NOMBRE", "NOMBRE='CLARO PLANES'", "nombre", ddlLinea, (Usuario)Session["usuario"]);
        poblar.PoblarListaDesplegable("empresa_recaudo", "COD_EMPRESA, NOM_EMPRESA", "", "nom_empresa", ddlEmpresa, (Usuario)Session["usuario"]);
        poblar.PoblarListaDesplegable("periodicidad", "COD_PERIODICIDAD, DESCRIPCION", "", "DESCRIPCION", ddlPeriodicidad, Usuario);
        poblar.PoblarListaDesplegable("PLANES_TELEFONICOS", ddlPlan, Usuario);

        ddlFormaPago.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlFormaPago.Items.Insert(1, new ListItem("Caja", "1"));
        ddlFormaPago.Items.Insert(2, new ListItem("Nomina", "2"));
        ddlFormaPago.SelectedIndex = 0;
        ddlFormaPago.DataBind();
    }

    protected void CargarDropDownEmpresa()
    {
        List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstEmpresas = null;
        Xpinn.Tesoreria.Services.EmpresaRecaudoServices empresaServicio = new Xpinn.Tesoreria.Services.EmpresaRecaudoServices();

        try
        {
            lstEmpresas = empresaServicio.ListarEmpresaRecaudoPersona(Convert.ToInt64(txtCodPersona.Text), Usuario);
        }
        catch (Exception ex)
        {
            VerError("CargarDropDownEmpresa, " + ex.Message);
            return;
        }

        if (lstEmpresas.Count > 0)
        {
            ddlEmpresa.Items.Clear();
            ddlEmpresa.DataSource = lstEmpresas;
            ddlEmpresa.DataTextField = "nom_empresa";
            ddlEmpresa.DataValueField = "cod_empresa";
            ddlEmpresa.AppendDataBoundItems = true;
            ddlEmpresa.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlEmpresa.SelectedIndex = 0;
            ddlEmpresa.DataBind();
        }
    }

    protected Boolean validarestado(String pCodPesona, string pIdentificacion)
    {
        VerError("");
        Boolean result = true;
        try
        {
            Int64? codigo = null;
            if (pCodPesona.Trim() != "")
                codigo = Convert.ToInt64(pCodPesona);
            result = SolicServicios.ConsultarEstadoPersona(codigo, pIdentificacion, "A", (Usuario)Session["usuario"]);
            if (result == false)
            {
                VerError("El cliente no tiene un estado activo");
                result = false;
            }
        }
        catch
        {
            VerError("El cliente no tiene un estado activo");
            result = false;
        }
        return result;
    }

    void ValidarPersonaVacaciones(Int64 pCod_Persona)
    {
        if (pCod_Persona == 0)
            return;
        Xpinn.Tesoreria.Services.EmpresaNovedadService RecaudoService = new Xpinn.Tesoreria.Services.EmpresaNovedadService();
        Xpinn.Tesoreria.Entities.EmpresaNovedad pPersonaVac = new Xpinn.Tesoreria.Entities.EmpresaNovedad();
        string pFiltro = " where vac.cod_persona = " + pCod_Persona + " order by vac.fecha_novedad desc";
        pPersonaVac = RecaudoService.ConsultarPersonaVacaciones(pFiltro, Usuario);

        if (pPersonaVac != null)
        {
            if (pPersonaVac.cod_persona > 0)
            {
                if (pPersonaVac.fechacreacion != null && pPersonaVac.fecha_inicial != null && pPersonaVac.fecha_final != null)
                {
                    DateTime pFechaActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    if (pPersonaVac.fechacreacion <= pFechaActual && pPersonaVac.fecha_final >= pFechaActual)
                    {
                        VerError("La persona tiene un periodo de vacaciones del [ " + Convert.ToDateTime(pPersonaVac.fecha_inicial).ToString(gFormatoFecha) + " al " + Convert.ToDateTime(pPersonaVac.fecha_final).ToString(gFormatoFecha) + " ]");
                        RegistrarPostBack();
                    }
                }
            }
        }
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            PlanTelefonico vDetalle = new PlanTelefonico();
            vDetalle = PlanServic.ConsultarLineaTelefonica(pIdObjeto, (Usuario)Session["usuario"]);
            
            if (vDetalle.cod_titular != null)
                txtCodPersona.Text = Convert.ToString(vDetalle.cod_titular);
            if (vDetalle.identificacion_titular != null)
                txtIdPersona.Text = Convert.ToString(vDetalle.identificacion_titular);
            if (vDetalle.nombre_titular != null)
                txtNomPersona.Text = Convert.ToString(vDetalle.nombre_titular);
            if (vDetalle.num_linea_telefonica != null)
                txtNumeroLineaTel.Text = Convert.ToString(vDetalle.num_linea_telefonica);
            if (vDetalle.identificacion_titular != null)
                txtIdentificacionTitu.Text = Convert.ToString(vDetalle.identificacion_titular);
            if (vDetalle.nombre_titular != null)
                txtNombreTit.Text = Convert.ToString(vDetalle.nombre_titular);
            if (vDetalle.cod_linea_servicio != null)
                ddlLinea.SelectedValue = vDetalle.cod_linea_servicio.ToString();
            if (vDetalle.cod_plan != 0)
                ddlPlan.SelectedValue = vDetalle.cod_plan.ToString();

            CargarDropDownEmpresa();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicServicios.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void txtIdPersona_TextChanged(object sender, EventArgs e)
    {
        Xpinn.Seguridad.Services.UsuarioService UsuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
        Xpinn.FabricaCreditos.Entities.Persona1 DatosPersona = new Xpinn.FabricaCreditos.Entities.Persona1();

        DatosPersona = UsuarioServicio.ConsultarPersona1(txtIdPersona.Text, (Usuario)Session["usuario"]);

        if (DatosPersona.cod_persona != 0)
        {
            txtCodPersona.Text = DatosPersona.cod_persona.ToString();

            if (DatosPersona.identificacion != "")
            {
                txtIdPersona.Text = DatosPersona.identificacion;
            }
            if (DatosPersona.nombre != "")
            {
                txtNomPersona.Text = DatosPersona.nombre;
            }
            //ValidarPersonaVacaciones(DatosPersona.cod_persona);            
        }
        else
        {
            txtNomPersona.Text = "";
            txtCodPersona.Text = "";
            txtIdPersona.Text = "";
        }
        //Agregado para hacer visible el boton guardar en caso de cambiar los datos de la persona
        validarestado(Convert.ToString(DatosPersona.cod_persona), DatosPersona.identificacion);
    }

    protected void ddlPlan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPlan.SelectedIndex != 0)
        {
            PlanTelefonico dtplan = new PlanTelefonico();
            dtplan = PlanServic.ConsultarPlanTelefonico(Convert.ToInt64(ddlPlan.SelectedValue), Usuario);
            txtValorCuota.Text = dtplan.valor.ToString();
        }
        else
        {
            txtValorCuota.Text = "";
        }
    }

    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblEmpresa.Visible = false;
        ddlEmpresa.Visible = false;
        if (ddlFormaPago.SelectedValue == "2")
        {
            lblEmpresa.Visible = true;
            ddlEmpresa.Visible = true;
        }
    }


    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Mostrar(true, "txtCodPersona", "txtIdPersona", "txtNomPersona", "txtIdentificacionTitu", "txtNombreTit");
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
            txtFecha.Focus();
            return false;
        }
        if (txtCodPersona.Text == "")
        {
            VerError("Seleccione el Solicitante");
            txtIdPersona.Focus();
            return false;
        }
        if (txtIdentificacionTitu.Text == "")
        {
            VerError("Ingrese la Identificación del titular");
            txtIdPersona.Focus();
            return false;
        }
        if (txtNombreTit.Text == "")
        {
            VerError("Ingrese el nombre del titular");
            txtIdPersona.Focus();
            return false;
        }
        if (txtNumeroLineaTel.Text == "")
        {
            VerError("Ingrese el número de la Linea Telefonica");
            txtNumeroLineaTel.Focus();
            return false;
        }
        if (ddlLinea.SelectedItem == null)
        {
            VerError("Seleccione la línea de servicio");
            ddlLinea.Focus();
            return false;
        }
        if (ddlLinea.SelectedIndex == 0)
        {
            VerError("Seleccione la línea de servicio");
            ddlLinea.Focus();
            return false;
        }
        LineaServicios vDetalle = BOLineaServ.ConsultarLineaSERVICIO(ddlLinea.SelectedValue, Usuario);
        if (vDetalle == null)
        {
            VerError("Error al consultar las condiciones de la linea!.");
            return false;
        }
        else //Agregado para cargar el código del proveedor
            Session["cod_proveedor"] = vDetalle.cod_proveedor;

        if (string.IsNullOrEmpty(txtValorCuota.Text))
        {
            VerError("Ingrese el valor de la cuota");
            txtValorCuota.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtFecPrimercuota.Text))
        {
            VerError("Ingrese la fecha de primera cuota");
            return false;
        }
        if (string.IsNullOrEmpty(txtFecVencimiento.Text))
        {
            VerError("Ingrese la fecha de vencimiento");
            return false;
        }
        if (ddlPeriodicidad.SelectedItem == null)
        {
            VerError("Seleccione la periodicidad de la línea");
            ddlPeriodicidad.Focus();
            return false;
        }
        if (ddlPeriodicidad.SelectedIndex == 0)
        {
            VerError("Seleccione la periodicidad de la línea");
            ddlPeriodicidad.Focus();
            return false;
        }
        if (ddlFormaPago.SelectedIndex == 0)
        {
            VerError("Seleccione la forma de pago");
            ddlFormaPago.Focus();
            return false;
        }
        if(ddlFormaPago.SelectedValue == "2")
        {
            if (ddlEmpresa.SelectedItem == null)
            {
                VerError("Seleccione la empresa recaudadora.");
                ddlEmpresa.Focus();
                return false;
            }
            if (ddlEmpresa.SelectedIndex == 0)
            {
                VerError("Seleccione la empresa recaudadora.");
                ddlEmpresa.Focus();
                return false;
            }
        }
        return true;
    }
    

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");

        if (validarestado(txtCodPersona.Text, txtIdPersona.Text) == true)
        {
            if (ValidarDatos())
            {
                ctlMensaje.MostrarMensaje("Desea activar los datos de la línea?");
            }
        }

    }



    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            PlanTelefonico dtllplan = new PlanTelefonico();

            if (validarestado(txtCodPersona.Text, txtIdPersona.Text) == true)
            {
                // Fecha Traspaso
                if (txtFecha.Text != "")
                {
                    dtllplan.fecha_activacion = Convert.ToDateTime(txtFecha.Text);
                }
                // datos solicitante o titular
                if (txtCodPersona.Text != "")
                    dtllplan.cod_titular = Convert.ToInt64(txtCodPersona.Text);

                if (txtIdPersona.Text != "")
                    dtllplan.identificacion_titular = txtIdPersona.Text;

                if (txtNomPersona.Text != "")
                    dtllplan.nombre_titular = txtNomPersona.Text;

                //Numero Linea Telefonica
                if (txtNumeroLineaTel.Text != "")
                    dtllplan.num_linea_telefonica = txtNumeroLineaTel.Text;

                dtllplan.cod_linea_servicio = Convert.ToInt64(ddlLinea.SelectedValue);
                dtllplan.cod_plan = Convert.ToInt32(ddlPlan.SelectedValue);
                dtllplan.valor_cuota = Convert.ToDecimal(txtValorCuota.Text.Replace(".",""));
                dtllplan.forma_pago = ddlFormaPago.SelectedValue;
                if (ddlFormaPago.SelectedValue == "2")
                    dtllplan.cod_empresa = Convert.ToInt64(ddlEmpresa.SelectedValue);

                if (idObjeto != "")
                {
                    // REALIZAR ACTIVACION
                    //DATOS DE LA OPERACION
                    Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
                    vOpe.cod_ope = 0;
                    vOpe.tipo_ope = 110;
                    vOpe.cod_caja = 0;
                    vOpe.cod_cajero = 0;
                    vOpe.observacion = "Operacion-Carga de Servicios para línea telefónica";
                    vOpe.cod_proceso = null;
                    vOpe.fecha_oper = Convert.ToDateTime(txtFecha.Text);
                    vOpe.fecha_calc = DateTime.Now;
                    vOpe.cod_ofi = Usuario.cod_oficina;

                    string pError = string.Empty;
                    PlanServic.ActivacionDeLineasTelefonica(ref pError, ref vOpe, dtllplan, Usuario);
                    if (!string.IsNullOrEmpty(pError))
                    {
                        VerError(pError);
                        return;
                    }
                    //GENERAR EL COMPROBANTE
                    if (vOpe.cod_ope != 0)
                    {
                        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                        Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = vOpe.cod_ope;
                        Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 110;
                        Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = DateTime.ParseExact(txtFecha.Text, gFormatoFecha, null);
                        Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = Convert.ToInt64(Session["cod_proveedor"]);
                        Navegar("../../../Contabilidad/Comprobante/Nuevo.aspx");
                    }
                    Site toolbar = (Site)Master;
                    toolbar.MostrarGuardar(false);
                    lblNroMsj.Text = dtllplan.num_linea_telefonica.ToString();
                    mvAplicar.ActiveViewIndex = 1;
                }
                else
                {
                    VerError("No se cargo con exito la cancelación");
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicServicios.CodigoPrograma, "btnContinuar_Click", ex);
        }
    }
    
}
