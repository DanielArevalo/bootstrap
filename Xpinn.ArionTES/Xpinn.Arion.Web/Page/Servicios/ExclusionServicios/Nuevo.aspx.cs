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
    PoblarListas Poblar = new PoblarListas();
    AprobacionServiciosServices ExcluServicios = new AprobacionServiciosServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ExcluServicios.CodigoProgramaExclu, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExcluServicios.GetType().Name + "E", "Page_PreInit", ex);
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
                pDatos.Enabled = false;
                txtFechaExclusion.Text = DateTime.Now.ToShortDateString();
                txtCuotasNoExclu.Text = "0";
                if (Session[ExcluServicios.CodigoProgramaExclu + ".id"] != null)
                {
                    idObjeto = Session[ExcluServicios.CodigoProgramaExclu + ".id"].ToString();
                    Session.Remove(ExcluServicios.CodigoProgramaExclu + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExcluServicios.GetType().Name + "E", "Page_Load", ex);
        }

    }

    void CargarDropdown()
    {
        Poblar.PoblarListaDesplegable("periodicidad",ddlPeriodicidad,(Usuario)Session["usuario"]);
        Poblar.PoblarListaDesplegable("lineasservicios", ddlLinea, (Usuario)Session["usuario"]);
        Poblar.PoblarListaDesplegable("MOTIVO_EXCLUSION", ddlMotivoExclu, (Usuario)Session["usuario"]);
        
        ddlFormaPago.Items.Insert(0,new ListItem("Seleccione un item","0"));
        ddlFormaPago.Items.Insert(1, new ListItem("Caja", "1"));
        ddlFormaPago.Items.Insert(2, new ListItem("Nomina", "2"));
        ddlFormaPago.SelectedIndex = 0;
        ddlFormaPago.DataBind();
    }



    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Servicio vDetalle = new Servicio();

            vDetalle = ExcluServicios.ConsultarSERVICIO(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);

            if (vDetalle.numero_servicio != 0)
                txtCodigo.Text = vDetalle.numero_servicio.ToString().Trim();
            if(vDetalle.fecha_solicitud != DateTime.MinValue)
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
            if (vDetalle.saldo != 0)
            {
                txtSaldo.Text = vDetalle.saldo.ToString();
                txtValorExcluir.Text = vDetalle.saldo.ToString();
            }             
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
            BOexcepcion.Throw(ExcluServicios.CodigoProgramaExclu, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public Boolean ValidarDatos()
    {
        if (txtFechaExclusion.Text == "")
        {
            VerError("Ingrese la fecha exclusión");
            txtFechaExclusion.Focus();
            return false;
        }
        if (txtCuotasNoExclu.Text == "")
        {
            VerError("Ingrese el numero de cuotas a no excluir");
            txtCuotasNoExclu.Focus();
            return false;
        }
        if (ddlMotivoExclu.SelectedIndex == 0)
        {
            VerError("Seleccione un motivo por la que se realizará la exclusión");
            ddlMotivoExclu.Focus();
            return false;
        }

        if (txtCuotasNoExclu.Text != "" && txtCuotasPendientes.Text != "")
        {
            if (Convert.ToInt32(txtCuotasNoExclu.Text) > Convert.ToInt32(txtCuotasPendientes.Text))
            {
                VerError("El número de cuotas pendientes a no excluir supera al número de cuotas pendientes");
                txtCuotasNoExclu.Focus();
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
            ctlMensaje.MostrarMensaje("Desea registrar los datos a excluir?");          
        }
    }



    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["usuario"];

            //DATOS DE LA OPERACION
            Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
            vOpe.cod_ope = 0;
            vOpe.tipo_ope = 5;
            vOpe.cod_caja = 0;
            vOpe.cod_cajero = 0;
            vOpe.observacion = "Operacion-Excluir valores";
            vOpe.cod_proceso = null;
            vOpe.fecha_oper = Convert.ToDateTime(txtFechaExclusion.Text);
            vOpe.fecha_calc = DateTime.Now;
            vOpe.cod_ofi = pUsuario.cod_oficina;

            Servicio pServicio = new Servicio();
            pServicio.numero_servicio = Convert.ToInt32(txtCodigo.Text);
            pServicio.estado = "E";
            pServicio.numero_cuotas = Convert.ToInt32(txtCuotasNoExclu.Text);
            pServicio.valor_cuota = Convert.ToDecimal(txtValorExcluir.Text);

            //DATOS DE LA EXCLUSION
            ExclusionServicios pExclu = new ExclusionServicios();
            pExclu.idexclusión = 0;
            pExclu.numero_servicio = Convert.ToInt32(txtCodigo.Text);
            pExclu.fecha = Convert.ToDateTime(txtFechaExclusion.Text);
            pExclu.valor = Convert.ToDecimal(txtValorExcluir.Text.Replace(gSeparadorMiles,""));
            pExclu.codmotivo = Convert.ToInt32(ddlMotivoExclu.SelectedValue);
            pExclu.cuo_noexcluir = Convert.ToInt32(txtCuotasNoExclu.Text);
            pExclu.feccrea = DateTime.Now;
            pExclu.usuariocrea = Convert.ToInt32(pUsuario.codusuario);
            if (txtCodPersona.Text == "")
            {
                VerError("No cargaron correctamente los registros. No existe registro del código de persona");
                return;
            }
            Int64 COD_OPE = 0;
            if (idObjeto != "")
            {
                //MODIFICAR
                ExcluServicios.ExcluirServicios(ref COD_OPE,pServicio, vOpe, pExclu, (Usuario)Session["usuario"]);
            }
            //GENERAR EL COMPROBANTE
            if (COD_OPE != 0)
            {
                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = COD_OPE;
                Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 5;
                Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = DateTime.ParseExact(txtFechaExclusion.Text, gFormatoFecha, null);
                Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = txtCodPersona.Text;
                Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
            }

            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);

            mvAplicar.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExcluServicios.CodigoProgramaExclu, "btnContinuar_Click", ex);
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
            List<Servicio> lstDatos = new List<Servicio>();
            lstDatos = ExcluServicios.CargarPlanXLinea(Convert.ToInt32(ddlLinea.SelectedValue), (Usuario)Session["usuario"]);
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


    protected void txtCuotasNoExclu_TextChanged(object sender, EventArgs e)
    {
        VerError("");
        if (txtCuotasPendientes.Text != "")
        {
            if (txtCuotasNoExclu.Text != "")
            {
                if (Convert.ToInt32(txtCuotasNoExclu.Text) > Convert.ToInt32(txtCuotasPendientes.Text))
                {
                    VerError("Ingrese una cuota a no excluir menor que las cuotas pendientes");
                    txtCuotasNoExclu.Focus();
                    return;
                }

                decimal saldo = txtSaldo.Text != "0" ? Convert.ToDecimal(txtSaldo.Text) : 0;
                decimal vr_cuota = txtValorCuota.Text != "0" ? Convert.ToDecimal(txtValorCuota.Text) : 0;

                txtValorExcluir.Text = (saldo - (vr_cuota * Convert.ToInt32(txtCuotasNoExclu.Text))).ToString("n0");
            }
            else
            {
                txtValorExcluir.Text = "0";
            }
        }

    }
}
