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
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;

public partial class Nuevo : GlobalWeb
{
    private PoblarListas poblar = new PoblarListas();
    SolicitudServiciosServices SolicServicios = new SolicitudServiciosServices();
    LineaServiciosServices LineaServicios = new LineaServiciosServices();
    LineaServiciosServices BOLineaServ = new LineaServiciosServices();
    LineaServiciosServices oficinaServicio = new LineaServiciosServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(SolicServicios.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicServicios.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ctlBusquedaProveedor.CheckedOrd = true;
            ctlBusquedaProveedor.VisibleOrd = false;

            if (!IsPostBack)
            {

                List<Xpinn.Caja.Entities.Oficina> lstOficina = new List<Xpinn.Caja.Entities.Oficina>();
                Xpinn.Caja.Services.OficinaService oficinaServicio = new Xpinn.Caja.Services.OficinaService();
                Xpinn.Caja.Entities.Oficina pOficina = new Xpinn.Caja.Entities.Oficina();
                Usuario pUsuario = new Usuario();
                pUsuario = (Usuario)Session["Usuario"];
                lstOficina = oficinaServicio.ListarOficina(pOficina, pUsuario);
                ddloficina.DataSource = lstOficina;
                ddloficina.DataTextField = "nombre";
                ddloficina.DataValueField = "cod_oficina";
                ddloficina.DataBind();


                Session["cod_linea"] = null;
                txtValorCuota.Attributes.Add("readonly", "readonly");
                Session["Beneficiario"] = null;
                Session["OPCION"] = null;
                CargarDropdown();
                mvAplicar.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;

                if (Session[SolicServicios.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[SolicServicios.CodigoPrograma + ".id"].ToString();
                    Session.Remove(SolicServicios.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);

                    Session["TEXTO"] = "modificar";
                    lblmsj.Text = "modificado";
                }
                else
                {
                    Session["TEXTO"] = "grabar";
                    lblmsj.Text = "grabada";
                    txtFecha.Text = DateTime.Now.ToShortDateString();
                    txtCodigo.Text = SolicServicios.ObtenerSiguienteCodigo((Usuario)Session["usuario"]).ToString();
                    InicializargvBeneficiario();
                }
                ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);

                //Sección de confirmación de servicio web
                if (Session["solicitudServWeb"] != null)
                {
                    Servicio solicitud = Session["solicitudServWeb"] as Servicio;
                    cargarDatosPersona(Convert.ToInt64(solicitud.cod_persona));
                    ddlLinea.SelectedValue = solicitud.cod_linea_servicio;
                    txtValorTotal.Text = solicitud.valor_total.ToString();
                    txtFecIni.Texto = solicitud.fecha_solicitud.ToString();
                    txtNumCuotas.Text = solicitud.numero_cuotas.ToString();
                    
                    txtFec1ercuota.Texto = solicitud.fecha_primera_cuota.ToString();
                    ddlPeriodicidad.SelectedValue = "1";
                    txtObservaciones.Text = solicitud.observaciones;
                    ddlLinea_SelectedIndexChanged(new object(), new EventArgs());
                    CalcularFechaTerminacion_TextChanged(new object(), new EventArgs());
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "act", "CalcularDivisionTotalVariosControlesAControlUnico(arrayIDControlesCalcular, IDControlDestino)", true);
                    CargarDropDownEmpresa();
                }

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicServicios.GetType().Name + "L", "Page_Load", ex);
        }

    }

    public void cargarDatosPersona(Int64 cod_persona)
    {
        Persona1Service DatosPersona = new Persona1Service();
        VerError("");

        Persona1 pPersona = new Persona1();
        Persona1 lstOficina = new Persona1();

        if (cod_persona != 0)
        {
            pPersona = DatosPersona.ConsultaDatosPersona(cod_persona, (Usuario)Session["usuario"]);

            if (pPersona.cod_persona != 0)
                txtCodPersona.Text = pPersona.cod_persona.ToString();
            if (lstOficina.cod_oficina >= 0)
                ddloficina.Text = lstOficina.cod_oficina.ToString();
            if (pPersona.identificacion != null)
            {
                txtIdPersona.Text = pPersona.identificacion;
                txtIdentificacionTitu.Text = pPersona.identificacion;
                txtNomPersona.Text = pPersona.nombre;
                txtNombreTit.Text = pPersona.nombre;
            }            
        }
        else
        {
            VerError("No se encontraron datos de las persona");
        }
    }

    private void CargarDropdown()
    {
        PoblarLista("periodicidad", ddlPeriodicidad);
        PoblarLista("lineasservicios", ddlLinea);
        //PoblarLista("PLANSERVICIO", ddlPlan);

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
                List<Xpinn.Caja.Entities.Oficina> lstOficina = new List<Xpinn.Caja.Entities.Oficina>();
        ctlBusquedaPersonas.Mostrar(true, "txtCodPersona", "txtIdPersona", "txtNomPersona", "txtIdentificacionTitu", "txtNombreTit");
    }


    protected void InicializargvBeneficiario()
    {
        List<DetalleServicio> lstDeta = new List<DetalleServicio>();
        for (int i = gvBeneficiarios.Rows.Count; i < 5; i++)
        {
            DetalleServicio eCuenta = new DetalleServicio();
            eCuenta.codserbeneficiario = -1;
            //eCuenta.cod_empresa = -1;
            eCuenta.identificacion = "";
            eCuenta.nombre = "";
            eCuenta.codparentesco = null;
            eCuenta.porcentaje = null;

            lstDeta.Add(eCuenta);
        }
        gvBeneficiarios.DataSource = lstDeta;
        gvBeneficiarios.DataBind();

        Session["Beneficiario"] = lstDeta;
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
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]).OrderBy(x=>x.descripcion).ToList();
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }

    protected Boolean validarestado(String pCodPesona, string pIdentificacion, string lstDatos)
    {
        VerError("");
        Boolean result = true;
        try
        {
            Int64? codigo = null;
            if (pCodPesona.Trim() != "")
                codigo = Convert.ToInt64(pCodPesona);
            result = SolicServicios.ConsultarEstadoPersona(codigo, pIdentificacion, "A", (Usuario)Session["usuario"]);
            LineaServicios vDetalle = LineaServicios.ConsultarLineaSERVICIO(ddlLinea.SelectedValue, (Usuario)Session["usuario"]);
            if (vDetalle.maneja_retirados != 1)
            {
                // Si no maneja retirados obliga a que el cliente este activo.
                if (result == false)
                {
                    VerError("El cliente no tiene un estado activo");
                    result = false;
                }
            }
            else if (vDetalle.maneja_retirados == 1)
            {
                // Si maneja retirados y la persona esta retirada deja pasar. FerOrt. 15-Jun-2019. COOTRAEMCALI.
                if (!result)
                    return true;
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
        string pFiltro = " where vac.cod_persona = " + pCod_Persona + " ";
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
            Servicio vDetalle = new Servicio();

            vDetalle = SolicServicios.ConsultarSERVICIO(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);
            Session["cod_linea"] = vDetalle.cod_linea_servicio;
            if (vDetalle.numero_servicio != 0)
                txtCodigo.Text = vDetalle.numero_servicio.ToString().Trim();
            if (vDetalle.fecha_solicitud != DateTime.MinValue)
                txtFecha.Text = vDetalle.fecha_solicitud.ToString(gFormatoFecha).Trim();
            if (vDetalle.cod_persona != 0)
            {
                txtCodPersona.Text = vDetalle.cod_persona.ToString().Trim();
                txtIdPersona.Text = vDetalle.identificacion.ToString().Trim();
                txtNomPersona.Text = vDetalle.nombre.ToString().Trim();
            }
            if (vDetalle.cod_linea_servicio != "")
                ddlLinea.SelectedValue = vDetalle.cod_linea_servicio;
            ddlLinea_SelectedIndexChanged(ddlLinea, null);

            if (vDetalle.cod_plan_servicio != "")
                ddlPlan.SelectedValue = vDetalle.cod_plan_servicio;
            if (vDetalle.Fec_ini != DateTime.MinValue)
                txtFecIni.Text = vDetalle.Fec_ini.ToString(gFormatoFecha);
            if (vDetalle.Fec_fin != DateTime.MinValue)
                txtFecFin.Text = vDetalle.Fec_fin.ToString(gFormatoFecha);
            if (vDetalle.num_poliza != "")
                txtNroPoliza.Text = vDetalle.num_poliza;
            if (vDetalle.valor_total != 0)
                txtValorTotal.Text = vDetalle.valor_total.ToString();
            if (vDetalle.Fec_1Pago != DateTime.MinValue)
                txtFec1ercuota.Text = vDetalle.Fec_1Pago.ToString(gFormatoFecha);
            if (vDetalle.numero_cuotas != 0)
                txtNumCuotas.Text = vDetalle.numero_cuotas.ToString();
            if (vDetalle.valor_cuota != 0)
                txtValorCuota.Text = vDetalle.valor_cuota.ToString();
            if (vDetalle.cod_periodicidad != 0)
                ddlPeriodicidad.Text = vDetalle.cod_periodicidad.ToString();
            if (vDetalle.forma_pago != "")
                ddlFormaPago.Text = vDetalle.forma_pago;
            if (vDetalle.cod_empresa != null)
                ddlEmpresa.SelectedValue = vDetalle.cod_empresa.ToString();
            if (vDetalle.cod_destino != null)
                ddlDestino.SelectedValue = vDetalle.cod_destino.ToString();

            //informacion del titular 
            if (vDetalle.identificacion_titular != "")
                txtIdentificacionTitu.Text = vDetalle.identificacion_titular;
            if (vDetalle.nombre_titular != "")
                txtNombreTit.Text = vDetalle.nombre_titular;

            if (vDetalle.observaciones != "")
                txtObservaciones.Text = vDetalle.observaciones;
            lblCuotasPendientes.Text = vDetalle.cuotas_pendientes.ToString();
            //RECUPERAR DATOS - GRILLA BENEFICIARIO
            List<DetalleServicio> LstBeneficiario = new List<DetalleServicio>();

            LstBeneficiario = SolicServicios.ConsultarDETALLESERVICIO(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);
            if (LstBeneficiario.Count > 0)
            {
                if ((LstBeneficiario != null) || (LstBeneficiario.Count != 0))
                {
                    gvBeneficiarios.DataSource = LstBeneficiario;
                    gvBeneficiarios.DataBind();
                }
                Session["Beneficiario"] = LstBeneficiario;
            }
            else
            {
                InicializargvBeneficiario();
            }
            ValidarPersonaVacaciones(vDetalle.cod_persona);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicServicios.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public Boolean ValidarDatos()
    {
        List<DetalleServicio> LstDetalle = new List<DetalleServicio>();
        LstDetalle = ObtenerListaBeneficiario();
        if (LstDetalle.Count > 0)
        {
            int cont = 0;
            foreach (DetalleServicio Detalle in LstDetalle)
            {
                cont++;
                if (Detalle.porcentaje > 100)
                {
                    VerError("Error en la fila : " + cont + " El valor del porcentaje no puede superar el 100%");
                    return false;
                }
            }
        }
        if (txtFecha.Text == "")
        {
            VerError("Seleccione la fecha de Solicitud");
            txtFecha.Focus();
            return false;
        }

        int mesFechaSolicitud = Convert.ToDateTime(txtFecha.Text).Month;

        if (mesFechaSolicitud < DateTime.Now.Month)
        {
            VerError("La fecha de solicitud no puede ser en meses pasados!.");
            return false;
        }

        if (ddlLinea.SelectedValue == "0" || ddlLinea.SelectedValue == "" || ddlLinea.SelectedIndex == 0)
        {
            VerError("Seleccione la Linea de Servicio");
            ddlLinea.Focus();
            return false;
        }
        if (ddlPlan.Visible == true)
        {
            if (ddlPlan.SelectedValue == "0" || ddlPlan.SelectedIndex == 0)
            {
                VerError("Seleccione el plan de Servicio");
                ddlPlan.Focus();
                return false;
            }
        }
        if (string.IsNullOrWhiteSpace(txtValorTotal.Text) || txtValorTotal.Text == "0")
        {
            VerError("Ingrese el Valor Total");
            txtValorTotal.Focus();
            return false;
        }
        if (txtNumCuotas.Visible == true)
        {
            if (txtNumCuotas.Text == "")
            {
                VerError("Ingrese el número de cuotas que desea para el servicio");
                txtNumCuotas.Focus();
                return false;
            }
        }
        if (txtCodPersona.Text == "")
        {
            VerError("Seleccione el Solicitante");
            return false;
        }
        if (ddlPeriodicidad.Visible == true)
        {
            if (ddlPeriodicidad.SelectedIndex == 0)
            {
                VerError("Seleccione la Periodicidad");
                ddlPeriodicidad.Focus();
                return false;
            }
        }
        if (ddlFormaPago.SelectedIndex == 0)
        {
            VerError("Seleccione la forma de pago");
            ddlFormaPago.Focus();
            return false;
        }
        if (ddlEmpresa.Visible == true)
        {
            if (ddlEmpresa.SelectedIndex == null)
            {
                VerError("Seleccione la empresa de pagaduria.");
                ddlEmpresa.Focus();
                return false;
            }
            if (ddlEmpresa.SelectedIndex == 0)
            {
                VerError("Seleccione la empresa de pagaduria.");
                ddlEmpresa.Focus();
                return false;
            }
        }
        if (txtIdentificacionTitu.Text == "")
        {
            VerError("Ingrese la Identificación del titular");
            txtIdentificacionTitu.Focus();
            return false;
        }
        if (txtNombreTit.Text == "")
        {
            VerError("Ingrese el nombre del titular");
            txtNombreTit.Focus();
            return false;
        }
        if (txtValorCuota.Visible == true)
        {
            if (string.IsNullOrWhiteSpace(txtValorCuota.Text))
            {
                VerError("No se puede guardar el servicio sin valor de cuota!.");
                return false;
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
        }
        if (txtFec1ercuota.Visible == true)
        {
            if (string.IsNullOrWhiteSpace(txtFec1ercuota.Text))
            {
                VerError("La fecha de la primera cuota no puede estar vacía!.");
                txtFec1ercuota.Focus();
                return false;
            }

            if (Convert.ToDateTime(txtFec1ercuota.Text) < Convert.ToDateTime(txtFecha.Text))
            {
                VerError("La fecha de la primera cuota no puede ser Inferior a la fecha de la Solicitud");
                txtFec1ercuota.Focus();
                return false;
            }
        }

        LineaServicios vDetalle = BOLineaServ.ConsultarLineaSERVICIO(ddlLinea.SelectedValue, Usuario);
        if (vDetalle == null)
        {
            VerError("Error al consultar las condiciones de la linea!.");
            return false;
        }

        if (txtNumCuotas.Visible)
        {
            int numCuotasMax = Convert.ToInt32(vDetalle.maximo_plazo);
            int numCuotas = Convert.ToInt32(txtNumCuotas.Text);

            if (numCuotas > numCuotasMax)
            {
                VerError("Plazo maximo superado, max plazo permitido: " + numCuotasMax);
                txtNumCuotas.Focus();
                return false;
            }
        }

        if (txtValorTotal.Visible)
        {
            decimal valorTotal = Convert.ToDecimal(txtValorTotal.Text);
            decimal valorTotalMax = Convert.ToDecimal(vDetalle.maximo_valor);

            if (valorTotal > valorTotalMax)
            {
                VerError("Valor maximo superado, valor max permitido: " + valorTotalMax);
                return false;
            }
        }

        int servicioActuales = SolicServicios.ConsultarNumeroServiciosPersona(txtCodPersona.Text, ddlLinea.SelectedValue, Usuario);

        if (vDetalle.numero_servicios.HasValue)
        {
            if (vDetalle.numero_servicios < servicioActuales)
            {
                VerError("Número maximo de servicios solicitados superado, max servicios permitidos: " + vDetalle.numero_servicios);
                return false;
            }
        }
        if (ddlDestino.Visible == true)
        {
            if (ddlDestino.SelectedValue.Trim() == "")
            {
                VerError("Debe seleccionar un tipo de Destinación del Servicio");
                return false;
            }
        }

        return true;
    }

    protected void txtNumBeneficiario_TextChanged(object sender, EventArgs e)
    {
        VerError("");
        TextBoxGrid txtbeneficiario = (TextBoxGrid)sender;
        if (txtbeneficiario.Text == "")
        {
            VerError("Digite al menos un beneficiario");
            return;
        }
    }

    protected Boolean validarbeneficiario()
    {
        Boolean resultado = true;

        if (panelGrilla.Visible == true)
        {
            foreach (GridViewRow rfila in gvBeneficiarios.Rows)
            {
                DetalleServicio ePogra = new DetalleServicio();
                TextBoxGrid txtbeneficiario = (TextBoxGrid)rfila.FindControl("txtPorcBene");
                if (txtbeneficiario.Text == "")
                {
                    VerError("Digite al menos un beneficiario");
                    resultado = false;
                }
                else
                {
                    return true;
                }
            }
        }
        return resultado;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (validarbeneficiario())
        {
            if (validarestado(txtCodPersona.Text, txtIdPersona.Text,ddloficina.Text) == true)
            {
                if (idObjeto != "")
                {
                    VerError("");
                    if (idObjeto == "")
                        txtCodigo.Text = SolicServicios.ObtenerSiguienteCodigo((Usuario)Session["usuario"]).ToString();
                }
                if (ValidarDatos())
                {
                    ctlMensaje.MostrarMensaje("Desea " + Session["TEXTO"].ToString() + " los Datos Ingresados?");
                }
            }
        }
    }



    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            if (validarestado(txtCodPersona.Text, txtIdPersona.Text,ddloficina.Text) == true)
            {
                Servicio pVar = new Servicio();
                if (txtCodigo.Text != "")
                    pVar.numero_servicio = Convert.ToInt32(txtCodigo.Text);
                else
                    pVar.numero_servicio = 0;
                pVar.fecha_solicitud = Convert.ToDateTime(txtFecha.Text);
                if (txtCodPersona.Text != "" && txtIdPersona.Text != "" && txtNomPersona.Text != "")
                    pVar.cod_persona = Convert.ToInt64(txtCodPersona.Text);
                pVar.cod_linea_servicio = ddlLinea.SelectedValue;
                if (ddlPlan.Visible == true)
                    pVar.cod_plan_servicio = ddlPlan.SelectedValue;
                else
                    pVar.cod_plan_servicio = null;
                if (txtFecIni.Visible == true)
                {
                    if (txtFecIni.Text != "")
                        pVar.fecha_inicio_vigencia = Convert.ToDateTime(txtFecIni.Text);
                    else
                        pVar.fecha_inicio_vigencia = DateTime.MinValue;
                }
                else
                    pVar.fecha_inicio_vigencia = DateTime.MinValue;

                if (txtFecFin.Visible == true)
                {
                    if (txtFecFin.Text != "")
                        pVar.fecha_final_vigencia = Convert.ToDateTime(txtFecFin.Text);
                    else
                        pVar.fecha_final_vigencia = DateTime.MinValue;
                }
                else
                    pVar.fecha_final_vigencia = DateTime.MinValue;

                if (txtNroPoliza.Visible == true)
                {
                    if (txtNroPoliza.Text != "")
                        pVar.num_poliza = txtNroPoliza.Text;
                    else
                        pVar.num_poliza = null;
                }
                else
                    pVar.num_poliza = null;

                pVar.valor_total = Convert.ToDecimal(txtValorTotal.Text);
                if (txtFec1ercuota.Visible == true)
                {
                    if (txtFec1ercuota.Text != "")
                        pVar.fecha_primera_cuota = Convert.ToDateTime(txtFec1ercuota.Text);
                    else
                        pVar.fecha_primera_cuota = DateTime.MinValue;
                }
                else
                    pVar.fecha_primera_cuota = DateTime.MinValue;

                if (txtNumCuotas.Visible == true)
                {
                    if (txtNumCuotas.Text != "")
                        pVar.numero_cuotas = Convert.ToInt32(txtNumCuotas.Text);
                    else
                        pVar.numero_cuotas = 0;
                }
                else
                    pVar.numero_cuotas = 0;

                if (txtValorCuota.Visible == true)
                {
                    if (txtValorCuota.Text != "")
                        pVar.valor_cuota = Convert.ToDecimal(txtValorCuota.Text);
                    else
                        pVar.valor_cuota = 0;
                }
                else
                    pVar.valor_cuota = 0;

                if (ddlPeriodicidad.Visible == true)
                    pVar.cod_periodicidad = Convert.ToInt32(ddlPeriodicidad.SelectedValue);
                else
                    pVar.cod_periodicidad = 0;
                pVar.forma_pago = ddlFormaPago.SelectedValue;
                if (ddlEmpresa.Visible == true)
                    pVar.cod_empresa = ConvertirStringToIntN(ddlEmpresa.SelectedValue);
                else
                    pVar.cod_empresa = null;

                pVar.identificacion_titular = txtIdentificacionTitu.Text;
                pVar.nombre_titular = txtNombreTit.Text;

                if (txtObservaciones.Text != "")
                    pVar.observaciones = txtObservaciones.Text;
                else
                    pVar.observaciones = null;
                pVar.saldo = 0;
                pVar.fecha_proximo_pago = DateTime.MinValue;
                pVar.fecha_ultimo_pago = DateTime.MinValue;
                pVar.fecha_aprobacion = DateTime.MinValue;
                pVar.estado = Session["OPCION"] != null ? "A" : "S";
                pVar.codigo_proveedor = ctlBusquedaProveedor.TextCodigo;
                pVar.identificacion_proveedor = ctlBusquedaProveedor.TextIdentif;
                pVar.nombre_proveedor = ctlBusquedaProveedor.TextNomProv;
                if (panelGrilla.Visible == true)
                {
                    pVar.lstDetalle = new List<DetalleServicio>();
                    pVar.lstDetalle = ObtenerListaBeneficiario();
                }
                if (ddlDestino.Visible == true)
                {
                    if (ddlDestino.SelectedValue != null)
                        pVar.cod_destino = Convert.ToInt64(ddlDestino.SelectedValue);
                }
                else
                {
                    pVar.cod_destino = 0;
                }

                if (idObjeto != "")
                {
                    pVar.cuotas_pendientes = lblCuotasPendientes.Text != "" ? Convert.ToInt32(lblCuotasPendientes.Text) : 0; ;
                    //MODIFICAR
                    SolicServicios.ModificarSolicitudServicio(pVar, (Usuario)Session["usuario"]);
                }
                else
                {
                    pVar.cuotas_pendientes = Convert.ToInt32(pVar.numero_cuotas);
                    //CREAR
                    SolicServicios.CrearSolicitudServicio(pVar, (Usuario)Session["usuario"]);
                }

                Site toolbar = (Site)Master;
                toolbar.MostrarGuardar(false);

                lblNroMsj.Text = pVar.numero_servicio.ToString();

                btnDesembolso.Visible = false;
                if (Session["OPCION"] != null)
                    btnDesembolso.Visible = true;
                mvAplicar.ActiveViewIndex = 1;

                //Aprueba la solicitud de servicio si viene desde la web 
                if (Session["solicitudServWeb"] != null)
                {
                    Servicio solicitud = Session["solicitudServWeb"] as Servicio;
                    try
                    {
                        solicitud.estado = "1"; // Aprobando Solicitud
                        SolicServicios.ModificarEstadoSolicitudServicio(solicitud, (Usuario)Session["usuario"]);
                        //Envia correo al asociado
                        Xpinn.Comun.Services.Formato_NotificacionService COServices = new Xpinn.Comun.Services.Formato_NotificacionService();
                        Xpinn.Comun.Entities.Formato_Notificacion noti = new Xpinn.Comun.Entities.Formato_Notificacion(Convert.ToInt32(txtCodPersona.Text), 17, "nombreProducto;Servicio: " + ddlLinea.Text);
                        COServices.SendEmailPerson(noti, (Usuario)Session["usuario"]);
                        //Limpia variable y redirige 
                        Session["solicitudServWeb"] = null;
                        Response.Redirect("../../Servicios/ConfirmarSolicitudServicio/Lista.aspx", false);

                    }
                    catch (Exception ex)
                    {
                        VerError("No se pudo aprobar la solicitud, " + ex.Message);
                    }
                }
            }
            else
            {

                VerError("El cliente no tiene un estado activo");
                return;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicServicios.CodigoPrograma, "btnContinuar_Click", ex);
        }
    }

    protected List<DetalleServicio> ObtenerListaBeneficiario()
    {
        try
        {
            List<DetalleServicio> lstDetalle = new List<DetalleServicio>();
            List<DetalleServicio> lista = new List<DetalleServicio>();

            foreach (GridViewRow rfila in gvBeneficiarios.Rows)
            {
                DetalleServicio ePogra = new DetalleServicio();

                Label lblCodigo = (Label)rfila.FindControl("lblCodigo");
                if (lblCodigo != null)
                    ePogra.codserbeneficiario = Convert.ToInt32(lblCodigo.Text);
                else
                    ePogra.codserbeneficiario = -1;

                TextBoxGrid txtIdenti_Grid = (TextBoxGrid)rfila.FindControl("txtIdenti_Grid");
                if (txtIdenti_Grid.Text != "")
                    ePogra.identificacion = txtIdenti_Grid.Text;

                TextBoxGrid txtNombreComple = (TextBoxGrid)rfila.FindControl("txtNombreComple");
                if (txtNombreComple.Text != "")
                    ePogra.nombre = txtNombreComple.Text;

                DropDownListGrid ddlParentesco = (DropDownListGrid)rfila.FindControl("ddlParentesco");
                if (ddlParentesco.SelectedIndex != 0)
                    ePogra.codparentesco = Convert.ToInt32(ddlParentesco.SelectedValue);

                TextBoxGrid txtPorcBene = (TextBoxGrid)rfila.FindControl("txtPorcBene");
                if (txtPorcBene.Text != "")
                    ePogra.porcentaje = Convert.ToDecimal(txtPorcBene.Text);

                lista.Add(ePogra);
                Session["Beneficiario"] = lista;

                if (ePogra.identificacion != null && ePogra.codparentesco != 0 && ePogra.porcentaje != 0)
                {
                    lstDetalle.Add(ePogra);
                }
            }
            return lstDetalle;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicServicios.CodigoPrograma, "ObtenerListaBeneficiario", ex);
            return null;
        }
    }



    protected void btnAdicionarFila_Click(object sender, EventArgs e)
    {
        ObtenerListaBeneficiario();
        List<DetalleServicio> LstPrograma = new List<DetalleServicio>();
        if (Session["Beneficiario"] != null)
        {
            LstPrograma = (List<DetalleServicio>)Session["Beneficiario"];

            for (int i = 1; i <= 1; i++)
            {
                DetalleServicio pDetalle = new DetalleServicio();
                pDetalle.codserbeneficiario = -1;
                //eCuenta.cod_empresa = -1;
                pDetalle.identificacion = "";
                pDetalle.nombre = "";
                pDetalle.codparentesco = null;
                pDetalle.porcentaje = null;
                LstPrograma.Add(pDetalle);
            }

            gvBeneficiarios.DataSource = LstPrograma;
            gvBeneficiarios.DataBind();

            Session["Beneficiario"] = LstPrograma;
        }
    }
    protected void gvBeneficiarios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlParentesco = (DropDownListGrid)e.Row.FindControl("ddlParentesco");
            if (ddlParentesco != null)
                PoblarLista("parentescos", ddlParentesco);

            Label lblParentesco = (Label)e.Row.FindControl("lblParentesco");
            if (lblParentesco != null)
                ddlParentesco.SelectedValue = lblParentesco.Text;

        }
    }
    protected void gvBeneficiarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvBeneficiarios.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaBeneficiario();

        List<DetalleServicio> LstDetalle = new List<DetalleServicio>();
        LstDetalle = (List<DetalleServicio>)Session["Beneficiario"];
        if (conseID > 0)
        {
            try
            {
                foreach (DetalleServicio acti in LstDetalle)
                {
                    if (acti.codserbeneficiario == conseID)
                    {
                        SolicServicios.EliminarDETALLESERVICIO(conseID, (Usuario)Session["usuario"]);
                        LstDetalle.Remove(acti);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(SolicServicios.CodigoPrograma, "gvProgramacion_RowDeleting", ex);
            }
        }
        else
        {
            LstDetalle.RemoveAt((gvBeneficiarios.PageIndex * gvBeneficiarios.PageSize) + e.RowIndex);
        }

        Session["Beneficiario"] = LstDetalle;

        gvBeneficiarios.DataSourceID = null;
        gvBeneficiarios.DataBind();
        gvBeneficiarios.DataSource = LstDetalle;
        gvBeneficiarios.DataBind();
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
                txtIdentificacionTitu.Text = DatosPersona.identificacion;
                
            }
            if (DatosPersona.nombre != "")
            {
                txtNomPersona.Text = DatosPersona.nombre;
                txtNombreTit.Text = DatosPersona.nombre;
            }

            CargarDropDownEmpresa();
            ValidarPersonaVacaciones(DatosPersona.cod_persona);
        }
        else
        {
            txtNomPersona.Text = "";
            txtCodPersona.Text = "";
        }

    }

    protected void ddlLinea_SelectedIndexChanged(object sender, EventArgs e)
    {
       


        ddlDestino.Items.Clear();
        if (ddlLinea.SelectedIndex != 0)
        {




            LineaServicios vDetalle = new LineaServicios();
            vDetalle = BOLineaServ.ConsultarLineaSERVICIO(ddlLinea.SelectedValue, (Usuario)Session["usuario"]);
            Session["NumeroServicio"] = vDetalle.numero_servicios;
            if (vDetalle != null)
            {
                if (vDetalle.requierebeneficiarios == 1)
                    panelGrilla.Visible = true;
                else
                    panelGrilla.Visible = false;
                if (vDetalle.tipo_servicio == 5) //Si es Tipo Orden de SErvicio
                {
                    lblPlan.Visible = false;
                    ddlPlan.Visible = false;
                    panelPrimFila.Visible = false;
                    panelSegFila.Visible = false;
                    ddlDestino.Visible = false;
                }
                else
                {
                    lblPlan.Visible = true;
                    ddlPlan.Visible = true;
                    panelPrimFila.Visible = true;
                    panelSegFila.Visible = true;
                    ddlDestino.Visible = true;
                }
                if (vDetalle.no_requiere_aprobacion == 1)
                    Session["OPCION"] = 1;
                else
                    Session["OPCION"] = null;
            }
            List<Servicio> lstDatos = new List<Servicio>();
            lstDatos = SolicServicios.CargarPlanXLinea(Convert.ToInt32(ddlLinea.SelectedValue), (Usuario)Session["usuario"]);
            
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
            Servicio vData = new Servicio();
            vData = SolicServicios.ConsultaProveedorXlinea(Convert.ToInt32(ddlLinea.SelectedValue), (Usuario)Session["usuario"]);
            if (vData.identificacion != "")
                ctlBusquedaProveedor.TextIdentif = vData.identificacion;
            if (vData.nombre != "")
                ctlBusquedaProveedor.TextNomProv = vData.nombre;
        }
        else
        {
            panelGrilla.Visible = true;
            lblPlan.Visible = true;
            ddlPlan.Visible = true;
            panelPrimFila.Visible = true;
            panelSegFila.Visible = true;

            ddlPlan.Items.Clear();
            ddlPlan.DataBind();
            ctlBusquedaProveedor.TextIdentif = "";
            ctlBusquedaProveedor.TextNomProv = "";
        }

        string IdLinea = ddlLinea.SelectedValue;
        poblar.PoblarListaDesplegable3(IdLinea, ddlDestino, (Usuario)Session["usuario"]);
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

    protected void CalcularFechaTerminacion_TextChanged(object sender, EventArgs e)
    {

        if (ddlPeriodicidad.SelectedValue != string.Empty && !string.IsNullOrWhiteSpace(txtFec1ercuota.Texto) && !string.IsNullOrWhiteSpace(txtNumCuotas.Text))
        {
            PeriodicidadService periodicidadService = new PeriodicidadService();
            Periodicidad periodicidad = periodicidadService.ConsultarPeriodicidad(Convert.ToInt64(ddlPeriodicidad.SelectedValue), Usuario);

            int numeroCuotas = Convert.ToInt32(txtNumCuotas.Text) - 1;
            DateTime fechaPrimeraCuota = Convert.ToDateTime(txtFec1ercuota.Texto);
            DateTimeHelper dateTimeHelper = new DateTimeHelper();
            DateTime fechaTerminacion = dateTimeHelper.SumarDiasSegunTipoCalendario(fechaPrimeraCuota, Convert.ToInt32(Math.Round(periodicidad.numero_dias * numeroCuotas)), Convert.ToInt32(periodicidad.tipo_calendario));
            txtFecFin.Text = fechaTerminacion.ToShortDateString();
        }
    }
}
