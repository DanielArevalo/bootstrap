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
            VisualizarOpciones(ExcluServicios.CodigoProgramaReporteMovimiento, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += btnCancelar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
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
                // Si es llamado desde el estado de cuenta
                if (Request.UrlReferrer.Segments[4].ToString() == "EstadoCuenta/")
                {
                    Session["Retorno"] = "1";
                }
                else
                    Session["Retorno"] = "0";

                mvAplicar.ActiveViewIndex = 0;
                txtFechaFin.Text = DateTime.Now.ToShortDateString();
                if (Session[ExcluServicios.CodigoProgramaReporteMovimiento + ".id"] != null)
                {
                    idObjeto = Session[ExcluServicios.CodigoProgramaReporteMovimiento + ".id"].ToString();
                    Session.Remove(ExcluServicios.CodigoProgramaReporteMovimiento + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExcluServicios.GetType().Name + "E", "Page_Load", ex);
        }

    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Servicio vDetalle = new Servicio();

            vDetalle = ExcluServicios.ConsultarSERVICIO(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);

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
                ddlLinea.Text = vDetalle.cod_linea_servicio + " " + vDetalle.nom_linea;
            if (vDetalle.cod_plan_servicio != "")
                ddlPlan.Text = vDetalle.cod_plan_servicio + " " + vDetalle.nom_plan; ;
            if (vDetalle.Fec_ini != null && vDetalle.Fec_ini != DateTime.MinValue)
                txtFecIni.Text = vDetalle.Fec_ini.ToString(gFormatoFecha);
            if (vDetalle.Fec_fin != null && vDetalle.Fec_fin != DateTime.MinValue)
                txtFecFin.Text = vDetalle.Fec_fin.ToString(gFormatoFecha);
            if (vDetalle.valor_total != 0)
                txtValorTotal.Text = vDetalle.valor_total.ToString();
            if (vDetalle.saldo != 0)
            {
                txtSaldo.Text = vDetalle.saldo.ToString();
            }
            if (vDetalle.fecha_proximo_pago != null && vDetalle.fecha_proximo_pago != DateTime.MinValue)
                txtFecProxPago.Text = Convert.ToDateTime(vDetalle.fecha_proximo_pago).ToShortDateString();
            if (vDetalle.numero_cuotas != 0)
                txtNumCuotas.Text = vDetalle.numero_cuotas.ToString();
            if (vDetalle.valor_cuota != 0)
                txtValorCuota.Text = vDetalle.valor_cuota.ToString();
            txtFechaini.Text = txtFecha.Text;
            txtCutasFaltantes.Text = vDetalle.cuotas_pendientes.ToString();

            ConsultarTasa(vDetalle);

            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExcluServicios.CodigoProgramaReporteMovimiento, "Actualizar", ex);
        }
    }


    protected void Actualizar()
    {
        try
        {
            List<Servicio> lstConsulta = new List<Servicio>();

            Servicio reportemovimiento = new Servicio();
            if (txtFechaFin.TieneDatos)
                reportemovimiento.Fec_fin = DateTime.ParseExact(txtFechaFin.Texto, gFormatoFecha, null);
            if (txtFechaini.TieneDatos)
                reportemovimiento.Fec_ini = DateTime.ParseExact(txtFechaini.Texto, gFormatoFecha, null);
            reportemovimiento.numero_servicio = Convert.ToInt32(txtCodigo.Text);

            lstConsulta = ExcluServicios.Reportemovimiento(reportemovimiento, (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataBind();
                panelGrilla.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
            }
            else
            {
                panelGrilla.Visible = false;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }

            Session.Add(ExcluServicios.CodigoProgramaReporteMovimiento + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExcluServicios.CodigoProgramaReporteMovimiento, "Actualizar", ex);
        }

    }

    Servicio ConsultarTasa(Servicio servicio)
    {
        try
        {
            SolicitudServiciosServices _soliServicios = new SolicitudServiciosServices();
            servicio = _soliServicios.ConsultarDatosPlanDePagos(servicio, Usuario);

            txtTasa.Text = servicio.tasa.ToString();

            return servicio;
        }
        catch (Exception ex)
        {
            VerError("Error al consultar la tasa de interes del servicio, " + ex.Message);
            return null;
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (Session["Retorno"].ToString() == "0")
            Navegar(Pagina.Lista);
        else
            Navegar("~/Page/Asesores/EstadoCuenta/Detalle.aspx");
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Actualizar();
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["usuario"];
            mvAplicar.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExcluServicios.CodigoProgramaReporteMovimiento, "btnContinuar_Click", ex);
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


    protected void btn_visualizar_click(object sender, EventArgs e)
    {
        gvLista.Visible = true;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvLista);
    }

    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=AhorroVista.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.Default;
        StringWriter sw = new StringWriter();
        ExpGrilla expGrilla = new ExpGrilla();

        sw = expGrilla.ObtenerGrilla(GridView1, null);

        Response.Write(expGrilla.style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }


}
