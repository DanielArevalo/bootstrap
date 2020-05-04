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
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;

public partial class Detalle : GlobalWeb
{
    PoblarListas Poblar = new PoblarListas();
    AprobacionServiciosServices AproServicios = new AprobacionServiciosServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AproServicios.CodigoProgramaModifi, "E");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproServicios.GetType().Name + "E", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //txtValorCuota.Attributes.Add("readonly", "readonly");
                Session["Beneficiario"] = null;
                CargarDropdown();
                mvAplicar.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;
                pDatos.Enabled = false;
                if (Session[AproServicios.CodigoProgramaModifi + ".id"] != null)
                {
                    idObjeto = Session[AproServicios.CodigoProgramaModifi + ".id"].ToString();
                    Session.Remove(AproServicios.CodigoProgramaModifi + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproServicios.GetType().Name + "E", "Page_Load", ex);
        }

    }

    void CargarDropdown()
    {
        Poblar.PoblarListaDesplegable("periodicidad", ddlPeriodicidad, (Usuario)Session["usuario"]);
        Poblar.PoblarListaDesplegable("lineasservicios", ddlLinea, (Usuario)Session["usuario"]);
        //PoblarLista("PLANSERVICIO", ddlPlan);

        ddlFormaPago.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlFormaPago.Items.Insert(1, new ListItem("Caja", "1"));
        ddlFormaPago.Items.Insert(2, new ListItem("Nomina", "2"));
        ddlFormaPago.SelectedIndex = 0;
        ddlFormaPago.DataBind();
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


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Servicio vDetalle = new Servicio();

            vDetalle = AproServicios.ConsultarSERVICIO(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);

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
            if (vDetalle.Fec_ini != null && vDetalle.Fec_ini != DateTime.MinValue)
                txtFecIni.Text = vDetalle.Fec_ini.ToString(gFormatoFecha);
            if (vDetalle.Fec_fin != null && vDetalle.Fec_fin != DateTime.MinValue)
                txtFecFin.Text = vDetalle.Fec_fin.ToString(gFormatoFecha);
            if (vDetalle.num_poliza != "")
                txtNroPoliza.Text = vDetalle.num_poliza;
            if (vDetalle.valor_total != 0)
                txtValorTotal.Text = vDetalle.valor_total.ToString(); if (vDetalle.saldo != 0)
                txtSaldo.Text = vDetalle.saldo.ToString();

            if (vDetalle.fecha_proximo_pago != null && vDetalle.fecha_proximo_pago != DateTime.MinValue)
                txtFecProxPago.Text = Convert.ToDateTime(vDetalle.fecha_proximo_pago).ToShortDateString();
            if (vDetalle.numero_cuotas != 0)
                txtNumCuotas.Text = vDetalle.numero_cuotas.ToString();
            if (vDetalle.valor_cuota != 0)
                txtValorCuota.Text = vDetalle.valor_cuota.ToString();
            if (vDetalle.cod_periodicidad != 0)
                ddlPeriodicidad.Text = vDetalle.cod_periodicidad.ToString();
            if (vDetalle.forma_pago != null)
                ddlFormaPago.SelectedValue = vDetalle.forma_pago;

            //informacion del titular 
            if (vDetalle.identificacion_titular != "")
                txtIdentificacionTitu.Text = vDetalle.identificacion_titular;
            if (vDetalle.nombre_titular != "")
                txtNombreTit.Text = vDetalle.nombre_titular;
            if (vDetalle.fecha_aprobacion.HasValue)
                txtFechaAproba.Text = vDetalle.fecha_aprobacion.Value.ToShortDateString();

            txtFechaPrimeraCuota.Text = vDetalle.Fec_1Pago.ToShortDateString();

            ViewState["valor_total_servicio"] = vDetalle.valor_total;

            //RECUPERAR DATOS - GRILLA BENEFICIARIO
            List<DetalleServicio> LstBeneficiario = new List<DetalleServicio>();

            LstBeneficiario = AproServicios.ConsultarDETALLESERVICIO(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);
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
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproServicios.CodigoProgramaModifi, "ObtenerDatos", ex);
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
        if (string.IsNullOrWhiteSpace(txtValorTotal.Text) && txtValorTotal.Text.Trim() == "0")
        {
            VerError("Ingrese el Valor Total");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtValorCuota.Text))
        {
            VerError("No se puede modificar el servicio sin valor de cuota!.");
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

        if (txtFecProxPago.Text != "")
        {
            if (Convert.ToDateTime(txtFecProxPago.Text) < DateTime.Now)
            {
                VerError("La fecha de proximo pago no puede ser inferior a la fecha actual");
                return false;
            }
        }

        LineaServiciosServices BOLineaServ = new LineaServiciosServices();
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

        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            ctlMensaje.MostrarMensaje("Desea modificar los Datos Ingresados?");
        }
    }



    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            if (idObjeto != "")
            {
                decimal? valorTotalViejo = ViewState["valor_total_servicio"] as decimal?;
                decimal valorTotalActual = Convert.ToDecimal(txtValorTotal.Text);

                if (valorTotalViejo != valorTotalActual)
                {
                    // Determinar código de proceso contable para generar el comprobante
                    Int64? rpta = 0;

                    if (ValidarProcesoContable(DateTime.Today, 27) == false)
                    {
                        VerError("No se encontró parametrización contable por procesos para el tipo de operación 27 = Modificacion de Servicios");
                        return;
                    }

                    if (mvAplicar.ActiveViewIndex == 0)
                    {
                        rpta = ctlproceso.Inicializar(27, DateTime.Now, (Usuario)Session["Usuario"]);
                        if (rpta > 1)
                        {
                            Site toolBar = (Site)this.Master;
                            toolBar.MostrarGuardar(false);
                            mvAplicar.ActiveViewIndex = 2;
                        }
                        else
                        {
                            if (AplicarDatos())
                                Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                            else
                                VerError("Se presentó error");
                        }
                    }
                }
                else
                {
                    AplicarDatos(false);

                    Site toolBar = (Site)Master;
                    toolBar.MostrarGuardar(false);

                    mvAplicar.ActiveViewIndex = 1;
                }
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproServicios.CodigoProgramaModifi, "btnContinuar_Click", ex);
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
            BOexcepcion.Throw(AproServicios.CodigoProgramaModifi, "ObtenerListaBeneficiario", ex);
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
                Poblar.PoblarListaDesplegable("parentescos", ddlParentesco, (Usuario)Session["usuario"]);

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
                        AproServicios.EliminarDETALLESERVICIO(conseID, (Usuario)Session["usuario"]);
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
                BOexcepcion.Throw(AproServicios.CodigoProgramaModifi, "gvProgramacion_RowDeleting", ex);
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
            List<Servicio> lstDatos = new List<Servicio>();
            lstDatos = AproServicios.CargarPlanXLinea(Convert.ToInt32(ddlLinea.SelectedValue), (Usuario)Session["usuario"]);
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
        }
        else
        {
            ddlPlan.Items.Clear();
            ddlPlan.DataBind();
        }

    }

    protected bool AplicarDatos(bool generaComprobante = true)
    {
        try
        {
            Usuario vUsuario = new Usuario();
            vUsuario = (Usuario)Session["Usuario"];
            Int64 COD_OPE = 0;

            Servicio pVar = new Servicio();
            if (txtCodigo.Text != "")
                pVar.numero_servicio = Convert.ToInt32(txtCodigo.Text);
            else
                pVar.numero_servicio = 0;

            if (txtFecIni.Text != "")
                pVar.fecha_inicio_vigencia = Convert.ToDateTime(txtFecIni.Text);
            else
                pVar.fecha_inicio_vigencia = DateTime.MinValue;

            if (txtFecFin.Text != "")
                pVar.fecha_final_vigencia = Convert.ToDateTime(txtFecFin.Text);
            else
                pVar.fecha_final_vigencia = DateTime.MinValue;

            if (txtNroPoliza.Text != "")
                pVar.num_poliza = txtNroPoliza.Text;
            else
                pVar.num_poliza = null;

            pVar.valor_total = Convert.ToDecimal(txtValorTotal.Text);
            pVar.saldo = Convert.ToDecimal(txtSaldo.Text);
            pVar.cod_linea_servicio = ddlLinea.SelectedValue;

            if (txtFecProxPago.Text != "")
                pVar.fecha_proximo_pago = Convert.ToDateTime(txtFecProxPago.Text);
            else
                pVar.fecha_proximo_pago = DateTime.MinValue;

            if (txtNumCuotas.Text != "")
                pVar.numero_cuotas = Convert.ToInt32(txtNumCuotas.Text);
            else
                pVar.numero_cuotas = 0;

            if (txtValorCuota.Text != "")
                pVar.valor_cuota = Convert.ToDecimal(txtValorCuota.Text);
            else
                pVar.valor_cuota = 0;

            pVar.cod_periodicidad = Convert.ToInt32(ddlPeriodicidad.SelectedValue);
            pVar.forma_pago = ddlFormaPago.SelectedValue;
            pVar.cod_persona = Convert.ToInt64(txtCodPersona.Text);

            pVar.lstDetalle = new List<DetalleServicio>();
            pVar.lstDetalle = ObtenerListaBeneficiario();

            //DATOS DE LA OPERACION
            Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
            pOperacion.cod_ope = 0;
            pOperacion.tipo_ope = 27;
            pOperacion.cod_caja = 0;
            pOperacion.cod_cajero = 0;
            pOperacion.fecha_oper = DateTime.Now;
            pOperacion.fecha_calc = DateTime.Now;
            pOperacion.observacion = "Operacion - Modificacion servicio";
            pOperacion.cod_proceso = ctlproceso.cod_proceso;

            if (AproServicios.ModificarServiciosActivos(ref COD_OPE, pOperacion, pVar, vUsuario, generaComprobante))
            {
                if (COD_OPE > 0)
                {
                    if (generaComprobante)
                    {
                        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                        Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = COD_OPE;
                        Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 27;
                        Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = DateTime.ParseExact(DateTime.Now.ToString(gFormatoFecha), gFormatoFecha, null);
                        Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = Convert.ToInt64(txtCodPersona.Text);
                        // Se cargan las variables requeridas para generar el comprobante
                        ctlproceso.CargarVariables(COD_OPE, Convert.ToInt32(27), 0, (Usuario)Session["Usuario"]);
                    }
                    return true;
                }
            }

            return false;
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
            return false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AproServicios.CodigoProgramaModifi, "btnGuardar_Click", ex);
            return false;
        }
    }
    protected void btnCancelarProceso_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarCancelar(true);
        toolBar.MostrarConsultar(true);
        mvAplicar.ActiveViewIndex = 0;
    }
    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            mvAplicar.ActiveViewIndex = 0;

            // Aquí va la función que hace lo que se requiera grabar en la funcionalidad
            AplicarDatos();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void CalcularFechaTerminacion_TextChanged(object sender, EventArgs e)
    {

        if (ddlPeriodicidad.SelectedValue != string.Empty && !string.IsNullOrWhiteSpace(txtFechaPrimeraCuota.Texto) && !string.IsNullOrWhiteSpace(txtNumCuotas.Text))
        {
            PeriodicidadService periodicidadService = new PeriodicidadService();
            Periodicidad periodicidad = periodicidadService.ConsultarPeriodicidad(Convert.ToInt64(ddlPeriodicidad.SelectedValue), Usuario);

            int numeroCuotas = Convert.ToInt32(txtNumCuotas.Text) - 1;
            DateTime fechaPrimeraCuota = Convert.ToDateTime(txtFechaPrimeraCuota.Texto);
            DateTimeHelper dateTimeHelper = new DateTimeHelper();
            DateTime fechaTerminacion = dateTimeHelper.SumarDiasSegunTipoCalendario(fechaPrimeraCuota, Convert.ToInt32(Math.Round(periodicidad.numero_dias * numeroCuotas)), Convert.ToInt32(periodicidad.tipo_calendario));
            txtFecFin.Text = fechaTerminacion.ToShortDateString();
        }
    }
}
