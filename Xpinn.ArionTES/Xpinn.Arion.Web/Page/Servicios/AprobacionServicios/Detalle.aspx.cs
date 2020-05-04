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
            VisualizarOpciones(AprobacionServicios.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
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
            if (!IsPostBack)
            {
                Session["Beneficiario"] = null;
                CargarDropdown();
                mvAplicar.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;

                if (Session[AprobacionServicios.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[AprobacionServicios.CodigoPrograma + ".id"].ToString();
                    Session.Remove(AprobacionServicios.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);                   
                }                
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionServicios.GetType().Name + "L", "Page_Load", ex);
        }

    }

    void CargarDropdown()
    {
        PoblarLista("periodicidad",ddlPeriodicidad);
        PoblarLista("lineasservicios", ddlLinea);
        //PoblarLista("PLANSERVICIO", ddlPlan);

        ddlFormaPago.Items.Insert(0,new ListItem("Seleccione un item","0"));
        ddlFormaPago.Items.Insert(1, new ListItem("Caja", "1"));
        ddlFormaPago.Items.Insert(2, new ListItem("Nomina", "2"));
        ddlFormaPago.SelectedIndex = 0;
        ddlFormaPago.DataBind();
    }

    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodPersona", "txtIdPersona", "txtNomPersona");
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
            if(vDetalle.fecha_solicitud != DateTime.MinValue)
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

            //informacion del titular 
            if (vDetalle.identificacion_titular != "")
                txtIdentificacionTitu.Text = vDetalle.identificacion_titular;
            if (vDetalle.nombre_titular != "")
                txtNombreTit.Text = vDetalle.nombre_titular;

            if (vDetalle.observaciones != "")
                txtObservaciones.Text = vDetalle.observaciones;
            
            //RECUPERAR DATOS - GRILLA BENEFICIARIO
            List<DetalleServicio> LstBeneficiario = new List<DetalleServicio>();

            LstBeneficiario = AprobacionServicios.ConsultarDETALLESERVICIO(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);
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
            BOexcepcion.Throw(AprobacionServicios.CodigoPrograma, "ObtenerDatos", ex);
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
        if (txtFechaAproba.Text == "")
        {
            VerError("Ingrese la fecha de Aprobación");
            return false;
        }

        if (ddlLinea.SelectedValue == "0" || ddlLinea.SelectedIndex == 0)
        {
            VerError("Seleccione la Linea de Servicio");
            return false;
        }
        if (txtValorTotal.Text == "0")
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
        if (txtIdentificacionTitu.Text == "")
        {
            VerError("Ingrese la Identificación del titular");
            return false;
        }
        if (txtNombreTit.Text == "")
        {
            VerError("Ingrese el nombre del titular");
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

        if (txtFec1ercuota.Visible == true)
        {
            if (txtFec1ercuota.Text == "")
            {
                VerError("Ingrese la Fecha de la 1era Cuota");
                return false;
            }
            else
            {
                if (Convert.ToDateTime(txtFec1ercuota.Text) < Convert.ToDateTime(txtFecha.Text))
                {
                    VerError("La fecha de la primera cuota no puede ser Inferior a la fecha de la Solicitud");
                    return false;
                }
                if (Convert.ToDateTime(txtFec1ercuota.Text) < DateTime.Now)
                {
                    VerError("La fecha de la primera cuota no puede ser inferior a la fecha actual");
                    return false;
                }

            }
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");       
        if (ValidarDatos())
        {
            ctlMensaje.MostrarMensaje("Desea realizar la aprobación del servicio?");          
        }
    }



    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
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

            pVar.cod_plan_servicio = ddlPlan.Visible == true ? ddlPlan.SelectedValue : null;

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
            pVar.saldo = Convert.ToDecimal(txtValorTotal.Text);

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
                {
                    pVar.numero_cuotas = Convert.ToInt32(txtNumCuotas.Text);
                    pVar.cuotas_pendientes = Convert.ToInt32(txtNumCuotas.Text);
                }
                else
                {
                    pVar.numero_cuotas = 0;
                    pVar.cuotas_pendientes = 0;
                }
            }
            else
            {
                pVar.numero_cuotas = 0;
                pVar.cuotas_pendientes = 0;
            }

            if (txtValorCuota.Visible == true)
            {
                if (txtValorCuota.Text != "")
                    pVar.valor_cuota = Convert.ToDecimal(txtValorCuota.Text);
                else
                    pVar.valor_cuota = 0;
            }
            else
                pVar.valor_cuota = 0;
            
            pVar.cod_periodicidad = ddlPeriodicidad.Visible == true ? Convert.ToInt32(ddlPeriodicidad.SelectedValue) : 0;
            pVar.forma_pago = ddlFormaPago.SelectedValue;

            pVar.identificacion_titular = txtIdentificacionTitu.Text;
            pVar.nombre_titular = txtNombreTit.Text;

            if (txtObservaciones.Text != "")
                pVar.observaciones = txtObservaciones.Text;
            else
                pVar.observaciones = null;

            //--------------------------------------
            //pVar.saldo = 0;
            pVar.fecha_proximo_pago = DateTime.MinValue;
            pVar.fecha_ultimo_pago = DateTime.MinValue;
            pVar.estado = "A";
            pVar.fecha_aprobacion = Convert.ToDateTime(txtFechaAproba.Text);
            pVar.fecha_activacion = DateTime.MinValue;
            if (txtObsAprobacion.Text != "")
                pVar.observacionaproba = txtObsAprobacion.Text;
            else
                pVar.observacionaproba = null;
            //--------------------------------------


            pVar.lstDetalle = new List<DetalleServicio>();
            pVar.lstDetalle = ObtenerListaBeneficiario();


            if (idObjeto != "")
            {
                //MODIFICAR
                AprobacionServicios.ModificarSolicitudServicio(pVar, (Usuario)Session["usuario"]);
            }

            CONTROLSERVICIOS pControl = new CONTROLSERVICIOS();

            pControl.idcontrol_servicios = 0;
            pControl.numero_servicio = pVar.numero_servicio;
            pControl.codtipo_proceso = 2;
            pControl.fechaproceso = DateTime.Now;
            AprobacionServicios.CrearControlServicios(pControl, (Usuario)Session["usuario"]);

            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);

            mvAplicar.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionServicios.CodigoPrograma, "btnContinuar_Click", ex);
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
            BOexcepcion.Throw(AprobacionServicios.CodigoPrograma, "ObtenerListaBeneficiario", ex);          
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
                        AprobacionServicios.EliminarDETALLESERVICIO(conseID, (Usuario)Session["usuario"]);
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
                BOexcepcion.Throw(AprobacionServicios.CodigoPrograma, "gvProgramacion_RowDeleting", ex);
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
            LineaServicios vDetalle = new LineaServicios();
            vDetalle = BOLineaServ.ConsultarLineaSERVICIO(ddlLinea.SelectedValue, (Usuario)Session["usuario"]);
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
                }
                else
                {
                    lblPlan.Visible = true;
                    ddlPlan.Visible = true;
                    panelPrimFila.Visible = true;
                    panelSegFila.Visible = true;
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
            if (vData.identificacion != "")
                txtProvIdentificacion.Text = vData.identificacion;
            if (vData.nombre != "")
                txtProvNombre.Text = vData.nombre;
        }
        else
        {
            ddlPlan.Items.Clear();
            ddlPlan.DataBind();
            txtProvIdentificacion.Text = "";
            txtProvNombre.Text = "";
        }
            
    }
   
  
}
