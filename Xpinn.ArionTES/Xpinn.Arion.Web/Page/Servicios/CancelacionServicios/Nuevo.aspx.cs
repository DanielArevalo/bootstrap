using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Services;
using Xpinn.Util;

public partial class Nuevo : GlobalWeb
{
    AprobacionServiciosServices servicioCancelacion = new AprobacionServiciosServices();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(servicioCancelacion.CodigoProgramaCancelacionServ, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicioCancelacion.CodigoProgramaCancelacionServ + "E", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDDL();
                mvAplicar.ActiveViewIndex = 0;
                pDatos.Enabled = false;
                txtFechaCancelacion.Text = DateTime.Now.ToShortDateString();
                if (Session[servicioCancelacion.CodigoProgramaCancelacionServ + ".id"] != null)
                {
                    idObjeto = Session[servicioCancelacion.CodigoProgramaCancelacionServ + ".id"].ToString();
                    Session.Remove(servicioCancelacion.CodigoProgramaCancelacionServ + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicioCancelacion.GetType().Name + "E", "Page_Load", ex);
        }
    }

    void CargarDDL()
    {
        PoblarListas Poblar = new PoblarListas();
        Poblar.PoblarListaDesplegable("periodicidad", ddlPeriodicidad, (Usuario)Session["usuario"]);
        Poblar.PoblarListaDesplegable("lineasservicios", ddlLinea, (Usuario)Session["usuario"]);

        ddlFormaPago.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlFormaPago.Items.Insert(1, new ListItem("Caja", "1"));
        ddlFormaPago.Items.Insert(2, new ListItem("Nomina", "2"));
        ddlFormaPago.SelectedIndex = 0;
        ddlFormaPago.DataBind();
    }

    protected void ddlLinea_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLinea.SelectedIndex != 0)
        {
            List<Servicio> lstDatos = new List<Servicio>();
            lstDatos = servicioCancelacion.CargarPlanXLinea(Convert.ToInt32(ddlLinea.SelectedValue), (Usuario)Session["usuario"]);
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

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Servicio vDetalle = new Servicio();

            vDetalle = servicioCancelacion.ConsultarSERVICIO(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);

            if (vDetalle.numero_servicio != 0)
                txtCodigo.Text = vDetalle.numero_servicio.ToString().Trim();
            if (vDetalle.fecha_solicitud != DateTime.MinValue)
                txtFecha.Text = vDetalle.fecha_solicitud.ToString(gFormatoFecha).Trim();
            if (vDetalle.cod_persona != 0)
            {
                txtCodPersona.Text = vDetalle.cod_persona.ToString().Trim();
                if (vDetalle.identificacion != null)
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
                txtValorTotal.Text = vDetalle.valor_total.ToString();
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
            if (vDetalle.cuotas_pendientes != 0)
                txtCuotasPendientes.Text = vDetalle.cuotas_pendientes.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicioCancelacion.CodigoProgramaCancelacionServ, "ObtenerDatos", ex);
        }
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        ctlMensaje.MostrarMensaje("¿Desea cancelar el servicio?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            string mensaje = servicioCancelacion.CancelarServicio(Convert.ToInt64(txtCodigo.Text), (Usuario)Session["usuario"]);
            lblMensajeConfirmacion.Text = mensaje;

            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);

            mvAplicar.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicioCancelacion.CodigoProgramaCancelacionServ, "btnContinuar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }
}