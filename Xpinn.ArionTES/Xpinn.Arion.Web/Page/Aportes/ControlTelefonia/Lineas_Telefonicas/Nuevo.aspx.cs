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
    



    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[SolicServicios.CodigoPrograma + ".id"] != null)
                VisualizarOpciones("170802", "E");
            else
                VisualizarOpciones("170802", "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanServic.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {


            if (!IsPostBack)
            {

                CargarDropdown();
                mvAplicar.ActiveViewIndex = 0;


                if (Session[SolicServicios.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[SolicServicios.CodigoPrograma + ".id"].ToString();
                    Session.Remove(SolicServicios.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);

                    Session["TEXTO"] = "modificar";
                    lblmsj.Text = "modificada";
                }
                else
                {
                    Session["TEXTO"] = "grabar";
                    lblmsj.Text = "grabada";
                    txtFecha.Text = DateTime.Now.ToShortDateString();
                }
                ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicServicios.GetType().Name + "L", "Page_Load", ex);
        }

    }

    private void CargarDropdown()
    {
        PoblarLista("periodicidad", ddlPeriodicidad);
        //PoblarLista("lineasservicios", ddlLinea);
        PoblarLista("PLANES_TELEFONICOS", "COD_PLAN, NOMBRE", "", "NOMBRE", ddlPlan, true);

        //Agregado para solamente cargar CLARO PLANES
        PoblarListas poblar = new PoblarListas();
        poblar.PoblarListaDesplegable("lineasservicios", "COD_LINEA_SERVICIO, NOMBRE", "NOMBRE='CLARO PLANES'","1",ddlLinea,(Usuario)Session["usuario"]);
        
        poblar.PoblarListaDesplegable("empresa_recaudo", "COD_EMPRESA, NOM_EMPRESA", "", "1", ddlEmpresa, (Usuario)Session["usuario"]);

        ddlFormaPago.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlFormaPago.Items.Insert(1, new ListItem("Caja", "1"));
        ddlFormaPago.Items.Insert(2, new ListItem("Nomina", "2"));
        ddlFormaPago.SelectedIndex = 0;
        ddlFormaPago.DataBind();

    }

    private void CargarDropDownEmpresa()
    {
        Usuario pUsuario = (Usuario)Session["Usuario"];
        List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstEmpresas = new List<Xpinn.Tesoreria.Entities.EmpresaRecaudo>();
        Xpinn.Tesoreria.Services.EmpresaRecaudoServices empresaServicio = new Xpinn.Tesoreria.Services.EmpresaRecaudoServices();

        try
        {
            lstEmpresas = empresaServicio.ListarEmpresaRecaudoPersona(Convert.ToInt64(txtCodPersona.Text), pUsuario);
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

    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Mostrar(true, "txtCodPersona", "txtIdPersona", "txtNomPersona", "txtIdentificacionTitu", "txtNombreTit");
    }

    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl, bool EsPLan = false)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        if (EsPLan)
        {
            plista = plista.Select(x => new Xpinn.Comun.Entities.ListaDesplegable
            {
                idconsecutivo = x.idconsecutivo,
                descripcion = string.Format("{0} - {1}", x.idconsecutivo, x.descripcion)
            }).ToList();
        }
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }

    protected Boolean validarestado(String pCodPesona, string pIdentificacion)
    {
        VerError("");
        Boolean result = true;
        try
        {
            Site ToolBar = (Site)this.Master;
            Int64? codigo = null;
            if (pCodPesona.Trim() != "")
                codigo = Convert.ToInt64(pCodPesona);
            result = SolicServicios.ConsultarEstadoPersona(codigo, pIdentificacion, "A", (Usuario)Session["usuario"]);
            if (result == false)
            {
                VerError("El cliente no tiene un estado activo");
                result = false;

                ToolBar.MostrarGuardar(false);
            }
            else
            {
                ToolBar.MostrarGuardar(true);
            }
        }
        catch
        {
            VerError("El cliente no tiene un estado activo");
            result = false;
        }
        return result;
    }

    /*void ValidarPersonaVacaciones(Int64 pCod_Persona)
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
    }*/

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

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            PlanTelefonico vDetalle = new PlanTelefonico();
            vDetalle = PlanServic.ConsultarLineaTelefonica(pIdObjeto, (Usuario)Session["usuario"]);

            if (vDetalle.fecha_solicitud != null)
                txtFecha.Text = Convert.ToString(vDetalle.fecha_solicitud);
            if (vDetalle.cod_titular != null)
                txtCodPersona.Text = Convert.ToString(vDetalle.cod_titular);
            if (vDetalle.identificacion_titular != null)
                txtIdPersona.Text = Convert.ToString(vDetalle.identificacion_titular);
            if (vDetalle.nombre_titular != null)
                txtNomPersona.Text = Convert.ToString(vDetalle.nombre_titular);
            if (vDetalle.num_linea_telefonica != null)
                txtNumeroLineaTel.Text = Convert.ToString(vDetalle.num_linea_telefonica);
            if (vDetalle.cod_linea_servicio != null)
                ddlLinea.SelectedValue = Convert.ToString(vDetalle.cod_linea_servicio);
            if (vDetalle.cod_plan != 0)
                ddlPlan.SelectedValue = Convert.ToString(vDetalle.cod_plan);
            if (vDetalle.valor_cuota != 0)
                txtValorCuota.Text = Convert.ToString(vDetalle.valor_cuota);
            if (vDetalle.fecha_primera_cuota != null)
                txtFecPrimercuota.Text = Convert.ToString(vDetalle.fecha_primera_cuota);
            if (vDetalle.fecha_vencimiento != null)
                txtFecVencimiento.Text = Convert.ToString(vDetalle.fecha_vencimiento);
            if (vDetalle.cod_periodicidad != null)
                ddlPeriodicidad.SelectedValue = Convert.ToString(vDetalle.cod_periodicidad);
            if (vDetalle.forma_pago != null)
                ddlFormaPago.SelectedValue = Convert.ToString(vDetalle.forma_pago);
            /*if (vDetalle.valor_compra != null)
                // textvalorentidad.Text = Convert.ToString(vDetalle.valor_compra);
                if (vDetalle.valor_mercado != null)
                    //textvalormercado.Text = Convert.ToString(vDetalle.valor_mercado);
                    if (vDetalle.beneficio != null)
                        //txtbeneficio.Text = Convert.ToString(vDetalle.beneficio);*/
            if (vDetalle.cod_empresa != null)
                ddlEmpresa.SelectedValue = Convert.ToString(vDetalle.cod_empresa);
            if (vDetalle.identificacion_titular != null)
                txtIdentificacionTitu.Text = Convert.ToString(vDetalle.identificacion_titular);
            if (vDetalle.nombre_titular != null)
                txtNombreTit.Text = Convert.ToString(vDetalle.nombre_titular);


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
            CargarDropDownEmpresa();
            //ValidarPersonaVacaciones(DatosPersona.cod_persona);            
        }
        else
        {
            txtNomPersona.Text = "";
            txtCodPersona.Text = "";
            txtIdPersona.Text = "";
        }
        //Agregado para hacer visible el boton guardar en caso de cambiar los datos de la persona
        validarestado(Convert.ToString(DatosPersona.cod_persona),DatosPersona.identificacion);
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

        if (ddlLinea.SelectedValue == "0" || ddlLinea.SelectedValue == "" || ddlLinea.SelectedIndex == 0)
        {
            VerError("Seleccione la Linea de Servicio");
            ddlLinea.Focus();
            return false;
        }

        if (ddlPlan.SelectedValue == "0" || ddlPlan.SelectedValue == "" || ddlPlan.SelectedIndex == 0)
        {
            VerError("Seleccione el plan movil");
            ddlPlan.Focus();
            return false;
        }

        if (txtValorCuota.Text == "")
        {
            VerError("Ingrese el Valor de la cuota");
            txtValorCuota.Focus();
            return false;
        }

        if (txtFecPrimercuota.Text == "")
        {
            VerError("Seleccione la fecha de la primera cuota");
            txtFecPrimercuota.Focus();
            return false;
        }

        if (ddlPeriodicidad.SelectedIndex == 0)
        {
            VerError("Seleccione la Periodicidad");
            ddlPeriodicidad.Focus();
            return false;
        }

        if (ddlFormaPago.SelectedIndex == 0)
        {
            VerError("Seleccione la forma de pago");
            ddlFormaPago.Focus();
            return false;
        }

        if (ddlEmpresa.Visible == true)
        {
            if (ddlEmpresa.SelectedIndex == 0)
            {
                VerError("Seleccione la empresa de pagaduría.");
                ddlEmpresa.Focus();
                return false;
            }
        }

        //if (textvalorentidad.Text == "")
        //{
        //    VerError("Ingresar Valor de la entidad.");
        //    textvalorentidad.Focus();
        //    return false;
        //}


        //if (textvalormercado.Text == "")
        //{
        //    VerError("Ingresar Valor del Mercado.");
        //    textvalormercado.Focus();
        //    return false;
        //}

        //if (txtbeneficio.Text == "")
        //{
        //    VerError("Ingresar Valor del beneficio.");
        //    txtbeneficio.Focus();
        //    return false;
        //}


        LineaServicios vDetalle = BOLineaServ.ConsultarLineaSERVICIO(ddlLinea.SelectedValue, Usuario);
        if (vDetalle == null)
        {
            VerError("Error al consultar las condiciones de la linea!.");
            return false;
        }
        else //Agregado para cargar el código del proveedor
            Session["cod_proveedor"] = vDetalle.cod_proveedor;



        int servicioActuales = SolicServicios.ConsultarNumeroServiciosPersona(txtCodPersona.Text, ddlLinea.SelectedValue, Usuario);

        if (vDetalle.numero_servicios.HasValue)
        {
            if (vDetalle.numero_servicios < servicioActuales)
            {
                VerError("Número maximo de servicios solicitados superado, max servicios permitidos: " + vDetalle.numero_servicios);
                return false;
            }
        }

        return true;
    }

    protected void txtcalcular_TextChanged(object sender, EventArgs e)
    {
        //    decimal TotalBeneficio = 0;
        //    decimal a = 0;
        //    decimal b = 0;

        //    if (textvalorentidad.Text != "")
        //    {
        //        a = Convert.ToInt64(textvalorentidad.Text);
        //    }
        //    else
        //    {
        //        a = Convert.ToInt64(0);
        //    }

        //    if (textvalormercado.Text != "")
        //    {
        //        b = Convert.ToInt64(textvalormercado.Text);
        //    }
        //    else
        //    {
        //        b = Convert.ToInt64(0);
        //    };

        //    TotalBeneficio = b - a;

        //    txtbeneficio.Text = Convert.ToString(TotalBeneficio);
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");

        if (validarestado(txtCodPersona.Text, txtIdPersona.Text) == true)
        {
            if (ValidarDatos())
            {
                ctlMensaje.MostrarMensaje("Desea " + Session["TEXTO"].ToString() + " los Datos Ingresados?");
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

                //Fecha Solicitud
                if (txtFecha.Text != "")
                {
                    dtllplan.fecha_solicitud = Convert.ToDateTime(txtFecha.Text);
                    dtllplan.fecha_activacion = Convert.ToDateTime(txtFecha.Text);
                }
                //datos solicitante o titular
                if (txtCodPersona.Text != "")
                {
                    dtllplan.cod_titular = Convert.ToInt64(txtCodPersona.Text);
                }

                if (txtIdPersona.Text != "")
                {
                    dtllplan.identificacion_titular = txtIdPersona.Text;
                }

                if (txtNomPersona.Text != "")
                {
                    dtllplan.nombre_titular = txtNomPersona.Text;
                }

                //Numero Linea Telefonica

                if (txtNumeroLineaTel.Text != "")
                {
                    dtllplan.num_linea_telefonica = txtNumeroLineaTel.Text;
                }

                //Linea de Servicio

                if (ddlLinea.SelectedValue != "")
                {
                    dtllplan.cod_linea_servicio = Convert.ToInt64(ddlLinea.SelectedValue);
                }

                if (ddlPlan.SelectedValue != "")
                {
                    dtllplan.cod_plan = Convert.ToInt32(ddlPlan.SelectedValue);
                }

                if (txtValorCuota.Text != "")
                {
                    dtllplan.valor_cuota = Convert.ToDecimal(txtValorCuota.Text);
                }

                if (txtFecPrimercuota.Text != "")
                {
                    dtllplan.fecha_primera_cuota = Convert.ToDateTime(txtFecPrimercuota.Text);
                }

                if (txtFecVencimiento.Text != "")
                {
                    dtllplan.fecha_vencimiento = Convert.ToDateTime(txtFecVencimiento.Text);
                }

                if (ddlPeriodicidad.SelectedValue != "")
                {
                    dtllplan.cod_periodicidad = Convert.ToInt64(ddlPeriodicidad.SelectedValue);
                }
                if (ddlFormaPago.SelectedValue != "")
                {
                    dtllplan.forma_pago = ddlFormaPago.SelectedValue;
                }

                //if (textvalorentidad.Text != "")
                //{
                //    dtllplan.valor_compra = Convert.ToDecimal(textvalorentidad.Text);
                //}
                //if (textvalormercado.Text != "")
                //{
                //    dtllplan.valor_mercado = Convert.ToDecimal(textvalormercado.Text);
                //}

                //if (txtbeneficio.Text != "")
                //{
                //    dtllplan.beneficio = Convert.ToDecimal(txtbeneficio.Text);
                //}

                if (ddlEmpresa.Visible == true)
                {
                    if (ddlEmpresa.SelectedValue != "")
                    {
                        dtllplan.cod_empresa = Convert.ToInt64(ddlEmpresa.SelectedValue);
                    }
                }
                
            }

            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["usuario"];

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
            vOpe.cod_ofi = pUsuario.cod_oficina;

            Int64 COD_OPE = 0;
            if (idObjeto != "")
            {
                //MODIFICAR
                PlanServic.ModificarLineaTelefonica(dtllplan, Usuario);

            }
            else
            {
                //CREAR                
                PlanServic.CrearLineaTelefonica(ref COD_OPE, vOpe, dtllplan, Usuario);
            }

            //GENERAR EL COMPROBANTE
            if (COD_OPE != 0)
            {
                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = COD_OPE;
                Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 110;
                Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = DateTime.ParseExact(txtFecha.Text, gFormatoFecha, null);
                Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = Convert.ToInt64(Session["cod_proveedor"]);
                Navegar("../../../Contabilidad/Comprobante/Nuevo.aspx");
            }

            Site toolbar = (Site)Master;
            toolbar.MostrarGuardar(false);
            // lblNroMsj.Text = dtllplan.num_linea_telefonica.ToString();
            btnDesembolso.Visible = false;
            // mvAplicar.ActiveViewIndex = 1;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicServicios.CodigoPrograma, "btnContinuar_Click", ex);
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

    protected void btnDesembolso_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Page/Servicios/DesembolsoServicios/Lista.aspx?num_serv=" + lblNroMsj.Text);
    }


    //protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
}
