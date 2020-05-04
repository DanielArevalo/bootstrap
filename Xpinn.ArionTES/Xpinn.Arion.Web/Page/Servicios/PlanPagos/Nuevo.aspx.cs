using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Servicios.Services;
using Xpinn.Servicios.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class Nuevo : GlobalWeb
{
    SolicitudServiciosServices _soliServicios = new SolicitudServiciosServices();
    Usuario _usuario;
    Servicio _servicio;
    string _codServicio;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_soliServicios.CodigoProgramaPlan, "L");

            Site toolBar = (Site)Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_soliServicios.CodigoProgramaPlan, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["usuario"];
            _codServicio = (string)Session[_soliServicios.CodigoProgramaPlan + ".id"];

            if (!IsPostBack)
            {
                InicializarPagina();
            }
            else
            {
                if (ViewState["servicioPlanPago"] != null)
                {
                    _servicio = (Servicio)ViewState["servicioPlanPago"];
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_soliServicios.CodigoProgramaPlan, "Page_Load", ex);
        }
    }


    void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        if (gvLista.Rows.Count > 0)
        {
            ControlsHelper controlHelper = new ControlsHelper();

            gvLista.AllowPaging = false;
            CrearPlanDePago(_servicio);

            string gridHTML = controlHelper.PrintControl(pnlPlanPagos);

            ClientScript.RegisterStartupScript(GetType(), "GridPrint", gridHTML);

            gvLista.AllowPaging = true;
            CrearPlanDePago(_servicio);
        }
        else
            VerError("Ocurrio un problema al imprimir");
    }


    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            VerError("");
            gvLista.PageIndex = e.NewPageIndex;
            CrearPlanDePago(_servicio);
        }
        catch (Exception ex)
        {
            VerError("Problemas al cambiar la pagina de la grilla, " + ex.Message);
        }
    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
        // =======
        //    NO BORRAR :D
        // =======
    }


    void InicializarPagina()
    {
        Servicio servicio = ConsultarDatosServicio();

        if (servicio == null) return;

        LlenarFormularioDatosServicio(servicio);

        servicio = ConsultarTasa(servicio);

        if (servicio == null) return;

        CrearPlanDePago(servicio);
    }


    Servicio ConsultarDatosServicio()
    {
        try
        {
            Servicio servicio = _soliServicios.ConsultarSERVICIO(Convert.ToInt64(_codServicio), _usuario);
            return servicio;
        }
        catch (Exception ex)
        {
            VerError("Error al consultar los datos del servicio, " + ex.Message);
            return null;
        }
    }


    void LlenarFormularioDatosServicio(Servicio servicio)
    {
        StringHelper stringHelper = new StringHelper();

        txtNumServ.Text = _codServicio;
        txtLinea.Text = servicio.cod_linea_servicio + " - " + servicio.nom_linea;
        txtIdentificacion.Text = servicio.identificacion;
        txtNombre.Text = servicio.nombre;
        txtPlazo.Text = servicio.numero_cuotas.HasValue ?servicio.numero_cuotas.ToString() : string.Empty;
        txtValorTotal.Text = servicio.valor_total.HasValue ? stringHelper.FormatearNumerosComoCurrency(servicio.valor_total.ToString()) : string.Empty;
        txtPeriodicidad.Text = servicio.nom_periodicidad;
        txtValorCuota.Text = servicio.valor_cuota.HasValue ? stringHelper.FormatearNumerosComoCurrency(servicio.valor_cuota.ToString()) : string.Empty;
        txtTipoCuota.Text = servicio.tipo_cuota == 1 ? "Cuota Fija" : "Cuota Escalonada";

        if (servicio.fecha_aprobacion.HasValue && servicio.Fec_1Pago != DateTime.MinValue)
        {
            DateTimeHelper dateHelper = new DateTimeHelper();
            long diasAjuste = dateHelper.DiferenciaEntreDosFechasDias(servicio.Fec_1Pago, servicio.fecha_aprobacion.Value);
            txtDiasAjuste.Text = diasAjuste.ToString();
        }
    }


    Servicio ConsultarTasa(Servicio servicio)
    {
        try
        {
            servicio = _soliServicios.ConsultarDatosPlanDePagos(servicio, _usuario);

            txtFechaAprobacion.Text = servicio.fecha_aprobacion.HasValue ? servicio.fecha_aprobacion.Value .ToShortDateString() : string.Empty;
            txtTasa.Text = servicio.tasa.ToString();

            ViewState.Add("servicioPlanPago", servicio);
            return servicio;
        }
        catch (Exception ex)
        {
            VerError("Error al consultar la tasa de interes del servicio, " + ex.Message);
            return null;
        }
    }


    void CrearPlanDePago(Servicio servicio)
    {
        ContabilidadHelper contabilidadHelper = new ContabilidadHelper();

        int numeroCuotas = servicio.numero_cuotas.HasValue ? servicio.numero_cuotas.Value : 0;
        decimal valorCuota = servicio.valor_cuota.HasValue ? servicio.valor_cuota.Value : 0;
        decimal valorTotal = servicio.valor_total.HasValue ? servicio.valor_total.Value : 0;
        TipoCuota tipoCuota = servicio.tipo_cuota.ToEnum<TipoCuota>();

        IEnumerable<FilaPlanPago> lstPlanDePagos = contabilidadHelper.CrearPlanDePagos(numeroCuotas, servicio.dias_periodicidad, valorCuota, valorTotal, servicio.Fec_1Pago, servicio.fecha_aprobacion, tipoCuota, servicio.tipo_calendario, servicio.tasa);

        if (lstPlanDePagos != null)
        {
            gvLista.DataSource = lstPlanDePagos;
            gvLista.DataBind();
            lblTotalRegs.Text = "Se han encontrado " + lstPlanDePagos.Count().ToString() + " registros";
        }
        else
        {
            lblTotalRegs.Text = "No se han encontrado registros";
        }
    }
}