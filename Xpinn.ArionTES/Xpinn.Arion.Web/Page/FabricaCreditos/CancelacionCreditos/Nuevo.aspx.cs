using System;
using System.Diagnostics;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Design;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using System.Web.UI;

partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.CreditoService creditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();
    private ProcesosService ProcesosServicio = new ProcesosService(); // Permite iniciar la consulta del historial (Segundo GridView)
    PoblarListas poblar = new PoblarListas();
    string estado;

    /// <summary>
    /// Mostrar la barra de herramientas al ingresar a la funcionalidad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[creditoServicio.CodigoProgramaCancelacion + ".id"] != null)
                VisualizarOpciones(creditoServicio.CodigoProgramaCancelacion, "E");
            else
                VisualizarOpciones(creditoServicio.CodigoProgramaCancelacion, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoRegresar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarConsultar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlMensajeBorrar.eventoClick += btnContinuarBorrarMen_Click;
            toolBar.eventoEliminar += (o, evt) => { ctlMensajeBorrar.MostrarMensaje("Desea realizar el borrado del crédito?"); };

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoProgramaCancelacion, "Page_PreInit", ex);
        }
    }

    private void btnContinuarBorrarMen_Click(object sender, EventArgs e)
    {
        try
        {
            creditoServicio.EliminarCredito(Convert.ToInt64(txtNumero_radicacion.Text), Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Site toolBar = (Site)Master;
            toolBar.MostrarEliminar(false);
            toolBar.MostrarGuardar(false);
            mvAhorroVista.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            VerError("Error al borrar el credito, " + ex.Message);
        }
    }


    /// <summary>
    /// Cargar datos de la funcionalidad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvAhorroVista.ActiveViewIndex = 0;
                txtFecha.ToDateTime = DateTime.Now;
                CargarDllLineas();
                if (Session[creditoServicio.CodigoProgramaCancelacion + ".id"] != null)
                {
                    if (Request.UrlReferrer != null)
                        idObjeto = Session[creditoServicio.CodigoProgramaCancelacion + ".id"].ToString();
                    ObtenerDatos(idObjeto);

                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoProgramaCancelacion, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (txtFecha.Text == "")
        {
            VerError("Ingrese una fecha, verifique los datos.");
            return;
        }
        ctlMensaje.MostrarMensaje("Desea realizar el cambio de estado  al crédito?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        Guardar(idObjeto);
    }

    private void Guardar(String pIdObjeto)
    {
        try
        {
            Credito vCredito = new Credito();
            vCredito = creditoServicio.ConsultarCredito(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            Credito datos = new Credito();

            Configuracion conf = new Configuracion();
            String format = conf.ObtenerFormatoFecha();

            datos.idcontrol = 0;
            datos.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);
            datos.tipo_refinancia = rbEstado.SelectedIndex;
            datos.fechaproceso = DateTime.Now;
            datos.cod_persona = vCredito.cod_persona;
            datos.cod_motivo = vCredito.cod_motivo;
            datos.observacion = txtObservaciones.Text == "" ? " " : txtObservaciones.Text;
            datos.anexos = vCredito.anexos;
            datos.nivel = vCredito.nivel;
            datos.fecha_consulta_dat = Convert.ToDateTime(txtFecha.Text);
            creditoServicio.MODIFICARcredito(datos, (Usuario)Session["usuario"]);
            GuardarControl();
            mvAhorroVista.ActiveViewIndex = 1;

            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarConsultar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoProgramaCancelacion, "Actualizar", ex);
        }
    }
    //Metodo guardar proceso de Hoja de ruta 
    private void GuardarControl()
    {
        switch (rbEstado.SelectedIndex)
        {
            case 0:
                estado = "N";
                break;
            case 1:
                estado = "S";
                break;
        }
        List<Procesos> lstConsulta = new List<Procesos>();
        lstConsulta = ProcesosServicio.ListarProcesos(null, (Usuario)Session["usuario"], null).ToList();
        ControlCreditosService ControlCreditosServicio = new ControlCreditosService();
        String FechaDatcaredito = "";
        Usuario pUsuario = (Usuario)Session["usuario"];
        ControlCreditos vControlCreditos = new ControlCreditos();
        vControlCreditos.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);
        vControlCreditos.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);
        vControlCreditos.codtipoproceso = Convert.ToString(lstConsulta.FirstOrDefault(x => x.estado == estado) != null ? lstConsulta.FirstOrDefault(x => x.estado == estado).cod_proceso : lstConsulta.FirstOrDefault(x => x.estado == "F").cod_proceso);
        vControlCreditos.fechaproceso = Convert.ToDateTime(DateTime.Now);
        vControlCreditos.cod_persona = pUsuario.codusuario;
        vControlCreditos.cod_motivo = 0;
        vControlCreditos.observaciones = txtObservaciones.Text;
        vControlCreditos.anexos = null;
        vControlCreditos.nivel = 0;
        if (Session["Datacredito"] == null)
        {
            if (FechaDatcaredito == "" || FechaDatcaredito == null || Session["Datacredito"] == "" || Session["Datacredito"].ToString() == "" || Session["Datacredito"] == null)
            {
                vControlCreditos.fechaconsulta_dat = FechaDatcaredito == "" ? DateTime.MinValue : Convert.ToDateTime(FechaDatcaredito.Trim());
            }
            else
            {
                if (FechaDatcaredito != null || FechaDatcaredito != "" || Session["Datacredito"] != null)
                {
                    FechaDatcaredito = Convert.ToString(Session["Datacredito"].ToString());
                    vControlCreditos.fechaconsulta_dat = Convert.ToDateTime(FechaDatcaredito);
                }
            }
        }
        vControlCreditos = ControlCreditosServicio.CrearControlCreditos(vControlCreditos, (Usuario)Session["usuario"]);
    }

    /// <summary>
    /// Evento para consultar los datos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    /// <summary>
    /// Evento para cancelar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelar_Click(object sender, EventArgs e)
    {

        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[creditoServicio.CodigoProgramaCancelacion + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }

    }


    /// <summary>
    /// Mostrar los datos del crédito
    /// </summary>
    /// <param name="pIdObjeto"></param>
    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Credito vCredito = new Credito();
            vCredito = creditoServicio.ConsultarCredito(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vCredito.numero_radicacion != Int64.MinValue)
                txtNumero_radicacion.Text = vCredito.numero_radicacion.ToString().Trim();
            if (vCredito.identificacion != string.Empty)
                txtIdentificacion.Text = HttpUtility.HtmlDecode(vCredito.identificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.tipo_identificacion))
                txtTipo_identificacion.Text = HttpUtility.HtmlDecode(vCredito.tipo_identificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.nombre))
                txtNombre.Text = HttpUtility.HtmlDecode(vCredito.nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.cod_linea_credito))
                this.ddlLineas.SelectedValue = HttpUtility.HtmlDecode(vCredito.cod_linea_credito.ToString().Trim());
            if (vCredito.monto != Int64.MinValue)
                txtMonto.Text = HttpUtility.HtmlDecode(vCredito.monto.ToString().Trim());
            if (vCredito.monto_solicitado != Int64.MinValue)
                txtMontoSolicitado.Text = HttpUtility.HtmlDecode(vCredito.monto_solicitado.ToString().Trim());
            if (vCredito.plazo != Int64.MinValue)
                txtNumCuotas.Text = HttpUtility.HtmlDecode(vCredito.plazo.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.periodicidad))
                txtPeriodicidad.Text = HttpUtility.HtmlDecode(vCredito.periodicidad.ToString().Trim());
            if (vCredito.valor_cuota != Int64.MinValue)
                txtValor_cuota.Text = HttpUtility.HtmlDecode(vCredito.valor_cuota.ToString("N").Trim());
            if (!string.IsNullOrEmpty(vCredito.forma_pago))
                txtForma_pago.Text = HttpUtility.HtmlDecode(vCredito.forma_pago.ToString().Trim());
            if (vCredito.fecha_prox_pago != DateTime.MinValue)
                txtFechaAprobacion.Text = vCredito.fecha_prox_pago.ToString().Trim();

            if (vCredito.fecha_solicitud != DateTime.MinValue)
                txtFechaSolicitud.Text = vCredito.fecha_solicitud.ToString().Trim();
            if (!string.IsNullOrEmpty(vCredito.numero_obligacion))
                this.txtNumero_solicitud.Text = HttpUtility.HtmlDecode(vCredito.numero_obligacion.ToString().Trim());



        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoProgramaCancelacion, "ObtenerDatos", ex);
        }
    }

    /// <summary>
    /// Es para cuando se cambia a la siguiente página
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //gvLista.PageIndex = e.NewPageIndex;
            ObtenerDatos(idObjeto);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoProgramaCancelacion, "gvLista_PageIndexChanging", ex);
        }
    }


    void CargarDllLineas()
    {
        poblar.PoblarListaDesplegable("lineascredito", ddlLineas, (Usuario)Session["usuario"]);

        Xpinn.FabricaCreditos.Data.LineasCreditoData listaLinea = new Xpinn.FabricaCreditos.Data.LineasCreditoData();
        Xpinn.FabricaCreditos.Entities.LineasCredito linea = new Xpinn.FabricaCreditos.Entities.LineasCredito();

        var lista = listaLinea.ListarLineasCredito(linea, (Usuario)Session["usuario"]);

        if (lista != null)
        {
            lista.Insert(0, new Xpinn.FabricaCreditos.Entities.LineasCredito { nom_linea_credito = "Seleccione un Item", cod_lineacredito = 0 });
            this.ddlLineas.DataSource = lista;
            ddlLineas.DataTextField = "nom_linea_credito";
            ddlLineas.DataValueField = "Codigo";
            ddlLineas.DataBind();
        }

    }



}