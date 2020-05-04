using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Comun.Services;
using Xpinn.Comun.Entities;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using System.Linq;
using System.Globalization;


public partial class Nuevo : GlobalWeb
{
    private Xpinn.Aportes.Services.GrupoLineaAporteServices LineaAporteServicio = new Xpinn.Aportes.Services.GrupoLineaAporteServices();
    private Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private Xpinn.Aportes.Services.AporteServices AporteServicio = new Xpinn.Aportes.Services.AporteServices();
    private Xpinn.Aportes.Services.AfiliacionServices AfiliacionServicio = new Xpinn.Aportes.Services.AfiliacionServices();
    private List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
    ParametrizacionProcesoAfilicacionService _paramProceso = new ParametrizacionProcesoAfilicacionService();

    private String ListaSolicitada = null;
    private Usuario _usuario;

    private Xpinn.FabricaCreditos.Services.BeneficiarioService BeneficiarioServicio = new Xpinn.FabricaCreditos.Services.BeneficiarioService();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {

            VisualizarOpciones(AporteServicio.ProgramaAperturaAporte, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlBusquedaPersonas.eventoEditar += gvListaAFiliados_SelectedIndexChanged;
            ctlBusquedaPersonas.eventoIdentificacion += txtNumeIdentificacion_TextChanged;
            if (Session[AfiliacionServicio.CodigoPrograma + ".id"] != null)
            {
                btnSiguiente.Visible = true;
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaAperturaAporte, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            _usuario = (Usuario)Session["usuario"];

            if (!IsPostBack)
            {
                TextBox txtNumeIdentificacion = (TextBox)ctlBusquedaPersonas.FindControl("txtNumeIdentificacion");
                string cc = Convert.ToString(Session["cedula"]);
                txtNumeIdentificacion.Text = cc;

                LlenarComboLineaAporte(DdlLineaAporte);
                LlenarComboPeriodicidad(DdlPeriodicidad);
                CargarListas();
                panelCuotaActual.Visible = false;
                if (Session[AporteServicio.ProgramaAperturaAporte + ".id"] != null)
                {
                    // Si es modificación de los datos carga la información
                    idObjeto = Session[AporteServicio.ProgramaAperturaAporte.ToString() + ".id"].ToString();
                    Session.Remove(AporteServicio.ProgramaAperturaAporte.ToString() + ".id");
                    ObtenerDatos(idObjeto);
                    Distribucion();
                    this.LblMensaje.Text = "";
                    MvAfiliados.ActiveViewIndex = 1;
                    DdlTipoCuota.Enabled = false;
                    pBusqueda.Visible = false;
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarConsultar(false);
                    upBeneficiarios.Visible = true;
                }
                else
                {
                    MvAfiliados.ActiveViewIndex = 0;
                    // Cuando es la apertura de una nueva cuenta muestra el número del aporte
                    ConsultarMaxAporte();
                    // Habilitar los campos para ingreso de datos y colocar valores por defecto
                    txtNumAporte.Enabled = false;
                    
                    txtFecha_interes.Text = DateTime.Now.ToShortDateString();
                    txtFecha_Proxppago.Text = DateTime.Now.ToShortDateString();

                    Int64 oficina = Convert.ToInt64(_usuario.cod_oficina);
                    txtOficina.Text = Convert.ToString(oficina);
                    txtOficinaNombre.Text = Convert.ToString(_usuario.nombre_oficina);
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarGuardar(false);

                    //por si en la linea dejan el mismo valor tanto minimo como maximo de inicio calcule la cuota
                    LineaAporte inflineaaport = new LineaAporte();
                    inflineaaport = AfiliacionServicio.ConsultarLineaObligatoria(Usuario);
                    if (inflineaaport.porcentaje_minimo != 0 && inflineaaport.porcentaje_maximo != 0)
                    {
                        if (inflineaaport.porcentaje_minimo == inflineaaport.porcentaje_maximo)
                        {
                            txtporcenApo.Text = Convert.ToString(inflineaaport.porcentaje_minimo);
                            CalcularCuota();
                        }
                    }
                }

                this.LblMensaje.Text = "";

                DdlFormaPago_SelectedIndexChanged(DdlFormaPago, null);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaAperturaAporte, "Page_Load", ex);
        }
    }

    protected void ConsultarDatosAfiliacion()
    {
        LblMensaje.Text = "";
        Afiliacion pAfili = new Afiliacion();
        pAfili = AfiliacionServicio.ConsultarAfiliacion(Convert.ToInt64(txtCodigoCliente.Text), _usuario);
        if(pAfili.estado != "R")
        {
            if (pAfili.cod_periodicidad != 0)
                DdlPeriodicidad.SelectedValue = pAfili.cod_periodicidad.ToString();
            if (pAfili.forma_pago != 0)
                DdlFormaPago.SelectedValue = pAfili.forma_pago.ToString();
            DdlFormaPago_SelectedIndexChanged(DdlFormaPago, null);
            if (pAfili.empresa_formapago != 0)
                ddlEmpresa.SelectedValue = pAfili.empresa_formapago.ToString();
            if (pAfili.fecha_primer_pago != null)
                txtFecha_Proxppago.Text = pAfili.fecha_primer_pago.Value.ToString(gFormatoFecha);
        }
        else
        {
            LblMensaje.Text = "No se pueden crear aportes a la persona seleccionada ya que está en estado Retirado";
        }
       
    }
    void registrarControl(Int32 cod_proceso, Int64 cod_per)
    {
        Usuario us = new Usuario();
        us = (Usuario)Session["usuario"];

        ParametrizacionProcesoAfiliacion control = new ParametrizacionProcesoAfiliacion();
        control.numero_solicitud = 0;
        control.identificacion = Convert.ToInt64(Session["identificacion"]);
        control.cod_persona = cod_per;
        control.ip_local = us.IP;
        control.cod_proceso = cod_proceso;

        AfiliacionServicio.controlRutaAfiliacion(control, (Usuario)Session["Usuario"]);
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        ImagenesService imagenService = new ImagenesService();
        if (Session[AfiliacionServicio.CodigoPrograma + ".id"] != null)
        {
            string cod_per = Session[AfiliacionServicio.CodigoPrograma + ".id"].ToString();
            Int32 act = Convert.ToInt32(Session[AfiliacionServicio.CodigoPrograma + "last"].ToString());
            String id = Session[AfiliacionServicio.CodigoPrograma + ".id"].ToString();
            /************VARIFICAR DONDE ESTABA ANTES DE LLEGAR ACA***********/
            ParametrizacionProcesoAfiliacion vParam = new ParametrizacionProcesoAfiliacion();
            vParam.lstParametros = (List<ParametrizacionProcesoAfiliacion>)Session["lstParametros"];
            int c = 0;
            foreach (ParametrizacionProcesoAfiliacion redirect in vParam.lstParametros)
            {
                if (redirect.cod_proceso == act)
                    break;
                c++;
            }
            if (c > 0)
                c = c - 1;
            switch (act)
            {
                case 2:
                    Session[AfiliacionServicio.CodigoPrograma + ".id"] = id;
                    Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c].cod_proceso;
                    Session[AfiliacionServicio.CodigoPrograma + ".modificar"] = 0;
                    Navegar("../../Aportes/ConfirmaAfiliacion/Lista.aspx");
                    break;
                case 3:
                    Session[AfiliacionServicio.CodigoPrograma + ".id"] = id;
                    Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c].cod_proceso;
                    Session[AfiliacionServicio.CodigoPrograma + ".modificar"] = 0;
                    Navegar("../../Aportes/Personas/Tabs/Nuevo.aspx");
                    break;
                case 4:
                    Session["cedula"] = rfvIdentificacion0.Text;
                    Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c].cod_proceso;
                    Navegar("../../Aportes/CuentasAportes/Nuevo.aspx");
                    break;
                case 5:
                    Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c].cod_proceso;
                    Session[imagenService.CodigoPrograma.ToString() + ".cod_persona"] = cod_per;
                    Navegar("../../Aportes/ImagenesPersona/Nuevo.aspx");
                    break;
                case 6:
                    Session[AfiliacionServicio.CodigoPrograma + ".id"] = cod_per;
                    Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c].cod_proceso;
                    Navegar("../../GestionRiesgo/ConsultasOFAC/Nuevo.aspx");
                    break;
                case 7:
                    string codOpcion = "170901";
                    Session["CodOpcion"] = codOpcion;
                    Session[codOpcion.ToString() + ".id"] = cod_per;
                    Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c].cod_proceso;
                    Navegar("../../Aportes/ProcesosAfiliacion/Nuevo.aspx");
                    break;
                case 8:
                    string codOpci = "170903";
                    Session["CodOpcion"] = codOpci;
                    Session[codOpci.ToString() + ".id"] = cod_per;
                    Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c].cod_proceso;
                    Session[AfiliacionServicio.CodigoPrograma + "next"] = "lst";
                    Navegar("../../Aportes/ProcesosAfiliacion/Nuevo.aspx");
                    break;
            }
        }
        else
        {
            Session.Remove("cedula");
            Session.Remove(imagenService.CodigoPrograma.ToString() + ".cod_persona");
            Session.Remove(AfiliacionServicio.CodigoPrograma + "last");
            Session.Remove(AfiliacionServicio.CodigoPrograma + "next");
            Session.Remove(AfiliacionServicio.CodigoPrograma + ".id");
            Navegar(Pagina.Lista);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        LblMensaje.Text = string.Empty;

        if (ddlLineaFiltro.SelectedIndex == 0)
        {
            LblMensaje.Text = "Seleccione una Linea";
            return;
        }


        GrupoLineaAporte lineaaporte = new GrupoLineaAporte();
        lineaaporte = LineaAporteServicio.ConsultarLineaAporte(Convert.ToInt64(ddlLineaFiltro.SelectedValue), (Usuario)Session["usuario"]);
        if (lineaaporte.beneficiarios != 1)
            ctlBusquedaPersonas.Filtro = " persona.cod_persona Not In (Select a.cod_persona From aporte a Where a.cod_linea_aporte = " + ddlLineaFiltro.SelectedValue + ")";

        ctlBusquedaPersonas.Actualizar(0);

    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, AporteServicio.ProgramaAperturaAporte);
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (ValidarData())
        {
            grabar();

        }
    }

    protected void btnSiguiente_Click(object sender, ImageClickEventArgs e)
    {
        string cod_per = Session[AfiliacionServicio.CodigoPrograma + ".id"].ToString();
        string nvNext = Session[AfiliacionServicio.CodigoPrograma + "next"].ToString();
        string nvLast = Session[AfiliacionServicio.CodigoPrograma + "last"].ToString();
        ParametrizacionProcesoAfiliacion vParam = new ParametrizacionProcesoAfiliacion();
        vParam.lstParametros = _paramProceso.ListarParametrosProcesoAfiliacion((Usuario)Session["usuario"]).Where(x => x.cod_proceso != 1).ToList();
        bool stop = false;
        int c = -1;
        ImagenesService imagenService = new ImagenesService();
        //DETERMINAR EL ORDEN EN QUE VA EL PROCESO
        int orden = 0;
        if (nvLast != null)
        {
            ParametrizacionProcesoAfiliacion vParamActual = new ParametrizacionProcesoAfiliacion();
            vParamActual.lstParametros = vParam.lstParametros.Where(x => x.cod_proceso == ConvertirStringToInt32(nvLast)).ToList();
            if (vParamActual.lstParametros.Count > 0)
            {
                orden = vParamActual.lstParametros[0].orden;
            }
        }
        //CONTROL DE RUTA PARA LA EVALUACIÓN 
        if (nvNext == null || nvNext == "")
            nvNext = Convert.ToString(4);
        registrarControl(Convert.ToInt32(nvNext), Convert.ToInt64(cod_per));
        _paramProceso.cambiarEstadoAsociado("C", Convert.ToInt64(cod_per), (Usuario)Session["usuario"]);
        foreach (ParametrizacionProcesoAfiliacion redirect in vParam.lstParametros)
        {
            c++;
            if (redirect.cod_proceso != Convert.ToInt32(nvLast) && redirect.cod_proceso > Convert.ToInt32(nvNext) && redirect.orden > orden)
            {
                switch (redirect.cod_proceso)
                {
                    case 3:
                        if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                        {
                            Session[AfiliacionServicio.CodigoPrograma + ".id"] = cod_per;
                            Session[AfiliacionServicio.CodigoPrograma + ".modificar"] = 0;
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                            AfiliacionServicio.notificarEmail(3, vParam.nombre, Convert.ToInt64(cod_per), redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                            Navegar("../../Aportes/Personas/Tabs/Nuevo.aspx");
                            stop = true;
                        }
                        break;
                    case 4:
                        if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                        {
                            Session["cedula"] = Convert.ToString(Session["identificacion"]);
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                            AfiliacionServicio.notificarEmail(4, vParam.nombre, Convert.ToInt64(cod_per), redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                            Navegar("../../Aportes/CuentasAportes/Nuevo.aspx");
                            stop = true;
                        }
                        break;
                    case 5:
                        if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                        {
                            Session[imagenService.CodigoPrograma.ToString() + ".cod_persona"] = Session[Usuario.codusuario + "Cod_persona"].ToString();
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                            AfiliacionServicio.notificarEmail(5, vParam.nombre, Convert.ToInt64(cod_per), redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                            Navegar("../../Aportes/ImagenesPersona/Nuevo.aspx");
                            stop = true;
                        }
                        break;
                    case 6:
                        if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                        {
                            Session[AfiliacionServicio.CodigoPrograma + ".id"] = cod_per;
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                            AfiliacionServicio.notificarEmail(6, vParam.nombre, Convert.ToInt64(cod_per), redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                            Navegar("../../GestionRiesgo/ConsultasOFAC/Nuevo.aspx");
                            stop = true;
                        }
                        break;
                    case 7:
                        if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                        {
                            Int64 id = Convert.ToInt64(cod_per);
                            string codOpcion = "170901";
                            Session["CodOpcion"] = codOpcion;
                            Session[codOpcion.ToString() + ".id"] = id;
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                            AfiliacionServicio.notificarEmail(7, vParam.nombre, Convert.ToInt64(cod_per), redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                            Navegar("../../AProcesosAfiliacion/Nuevo.aspx");
                            stop = true;
                        }
                        break;
                    case 8:
                        if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                        {
                            Int64 id = Convert.ToInt64(cod_per);
                            string codOpcion = "170902";
                            Session["CodOpcion"] = codOpcion;
                            Session[codOpcion.ToString() + ".id"] = id;
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                            AfiliacionServicio.notificarEmail(8, vParam.nombre, Convert.ToInt64(cod_per), redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                            Navegar("../../AProcesosAfiliacion/Nuevo.aspx");
                            stop = true;
                        }
                        break;
                }
            }
            if (stop) break;
        }
        if (stop == false)
        {
            _paramProceso.cambiarEstadoAsociado("A", Convert.ToInt64(cod_per), (Usuario)Session["usuario"]);
            Session.Remove("cedula");
            Session.Remove(imagenService.CodigoPrograma.ToString() + ".cod_persona");
            Session.Remove(AfiliacionServicio.CodigoPrograma + "last");
            Session.Remove(AfiliacionServicio.CodigoPrograma + "next");
            Session.Remove(AfiliacionServicio.CodigoPrograma + ".id");
            Navegar("../../Aportes/Afiliaciones/Lista.aspx");
        }
    }
    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox checkbox = e.Row.FindControl("chkPendiente") as CheckBox;

            if (checkbox != null && checkbox.Checked)
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[AporteServicio.ProgramaAperturaAporte + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaAperturaAporte, "gvLista_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Método para consultar los datos de la persona
    /// </summary>
    /// <param name="IdObjeto"></param>
    private bool ConsultarCliente(String IdObjeto)
    {
        if (!string.IsNullOrEmpty(IdObjeto))
        {
            Xpinn.Aportes.Services.AporteServices AportesServicio = new Xpinn.Aportes.Services.AporteServices();
            Xpinn.Aportes.Entities.Aporte aporte = new Xpinn.Aportes.Entities.Aporte();
            // Consultar los datos de la persona
            String IdObjeto2 = txtNumeIdentificacion.Text != "" ? txtNumeIdentificacion.Text : IdObjeto;
            aporte = AportesServicio.ConsultarCliente(IdObjeto2, _usuario);
            if (aporte.cod_persona == 0)
            {
                LblMensaje.Text = "ESTE PERSONA NO ESTA CREADA";
                txtNombre.Text = "";
            }
            else
            {
                if (aporte.cod_persona != 0)
                {
                    LblMensaje.Text = "";
                    if (!string.IsNullOrEmpty(aporte.nombre.ToString()))
                        txtNombre.Text = Convert.ToString(aporte.nombre);
                    if (!string.IsNullOrEmpty(aporte.tipo_identificacion.ToString()))
                        DdlTipoIdentificacion.SelectedValue = HttpUtility.HtmlDecode(aporte.tipo_identificacion);
                    DdlTipoIdentificacion.Enabled = false;
                    if (!string.IsNullOrEmpty(aporte.cod_persona.ToString()))
                        txtCodigoCliente.Text = Convert.ToString(aporte.cod_persona);
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Método para consultar el consecutivo de las cuentas de aportes
    /// </summary>
    private void ConsultarMaxAporte()
    {
        Int64 maxaporte = 0;
        Int64 numeroaporte = 1;
        AporteServices AportesServicio = new AporteServices();
        Aporte aporte = new Aporte();
        aporte = AportesServicio.ConsultarMaxAporte(_usuario);

        if (!string.IsNullOrEmpty(aporte.numero_aporte.ToString()))
            maxaporte = aporte.numero_aporte + numeroaporte;
        this.txtNumAporte.Text = Convert.ToInt64(maxaporte).ToString();
    }

    /// <summary>
    ///  Consultar la cuenta de aporte por el código de la persona
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    private Boolean ConsultarClienteAporte(Int64 Id)
    {
        Boolean result = true;
        VerError("");
        Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Persona1Service();
        Xpinn.FabricaCreditos.Entities.Persona1 persona1 = new Persona1();
        persona1 = personaServicio.ConsultarPersona1(Id, _usuario);
        if (persona1 == null)
            result = false;
        else
            result = ConsultarClienteAporte(persona1.identificacion);
        return result;
    }

    /// <summary>
    /// Consultar la cuenta de aporte por la identificación de la persona
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    private Boolean ConsultarClienteAporte(string Id)
    {
        VerError("");
        Boolean result = true;
        Int64 numeroaporte = 1;
        AporteServices AportesServicio = new AporteServices();
        Aporte aporte = new Aporte();
        aporte = AportesServicio.ConsultarClienteAporte(Id, Convert.ToInt32(ddlLineaFiltro.SelectedValue), _usuario);
        if (!string.IsNullOrEmpty(aporte.numero_aporte.ToString()))
            numeroaporte = aporte.numero_aporte;
        if (numeroaporte > 0)
        {
            GrupoLineaAporte lineaaporte = new GrupoLineaAporte();
            lineaaporte = LineaAporteServicio.ConsultarLineaAporte(Convert.ToInt64(ddlLineaFiltro.SelectedValue), (Usuario)Session["usuario"]);
            if (lineaaporte.beneficiarios != 1)
            {
                this.LblMensaje.Text = "Cliente ya tiene cuenta de aportes creada";
                result = false;
            }
        }
        return result;
    }

    private void Actualizar()
    {
        if (txtNumeIdentificacion.Text != "")
        {
            ConsultarCliente(txtNumeIdentificacion.Text);
            ConsultarClienteAporte(txtNumeIdentificacion.Text);
        }
        else
        {
            txtNombre.Text = "";
        }
    }

    private void DistribucionNuevo()
    {
        this.MvDistribucion.Visible = true;
        MvDistribucion.ActiveViewIndex = 0;
        /// llenar GvLista
        try
        {
            List<Xpinn.Aportes.Entities.Aporte> lstConsulta = new List<Xpinn.Aportes.Entities.Aporte>();
            lstConsulta = AporteServicio.ListarDistribucionAporteNuevo(_usuario, Convert.ToInt64(txtgrupoaporte.Text));

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
            }

            Session["LSTGRUPO"] = lstConsulta;
            Session.Add(AporteServicio.ProgramaAperturaAporte + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaAperturaAporte, "Actualizar", ex);
        }
    }

    /// <summary>
    /// Método para ver la distribución de la cuenta de aporte
    /// </summary>
    private void Distribucion()
    {
        this.MvDistribucion.Visible = true;
        MvDistribucion.ActiveViewIndex = 0;
        // Llenar datos de la gridView
        try
        {
            gvLista.Columns[6].Visible = true;

            List<Aporte> lstConsulta = new List<Aporte>();
            List<Aporte> lstCuentaGrupo = new List<Aporte>();
            string pLinea = DdlLineaAporte.SelectedItem != null ? DdlLineaAporte.SelectedValue : null;
            lstConsulta = AporteServicio.ListarDistribucionAporte(_usuario, Convert.ToInt64(this.txtCodigoCliente.Text), pLinea);

            var aportePrincipal = lstConsulta.Where(x => x.principal == 1).FirstOrDefault();

            if (lstConsulta == null)
            {
                VerError("Error al consultar aportes");
                return;
            }

            // Si el aporte tiene grupo de aporte valida que esten creadas las cuentas requeridas
            if (aportePrincipal != null)
            {
                if (aportePrincipal.grupo != 0)
                {
                    lstCuentaGrupo = AporteServicio.ConsultarCuentasPorGrupoAporte(aportePrincipal.grupo, _usuario);

                    // Valida que tenga las cuentas creadas que requiere el grupo
                    var validacionCuentas = from cuentaGrupo in lstCuentaGrupo
                                            where !lstConsulta.Exists(x => x.cod_linea_aporte == cuentaGrupo.cod_linea_aporte)
                                            select cuentaGrupo;

                    if (validacionCuentas.Count() > 0)
                    {
                        // Extension Method en ExtensionMethodsHelper XPINN.UTIL
                        validacionCuentas.ForEach(x => x.pendiente_crear = true);

                        lstConsulta.AddRange(validacionCuentas);
                    }
                }
            }

            foreach (var consulta in lstConsulta)
            {
                decimal valorCuota = Convert.ToDecimal(txtValorCuota.Text);
                consulta.cuota = valorCuota * (Convert.ToDecimal(consulta.porcentaje) / 100);
            }

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
            }

            Session["LSTGRUPO"] = lstConsulta;
            Session.Add(AporteServicio.ProgramaAperturaAporte + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaAperturaAporte, "Actualizar", ex);
        }
    }

    private Xpinn.Aportes.Entities.Aporte ObtenerValoresCliente()
    {
        Xpinn.Aportes.Entities.Aporte vAporte = new Xpinn.Aportes.Entities.Aporte();
        //if (txtCodigo.Text.Trim() != "")
        //    vAporte.cod_persona = Convert.ToInt64(txtCodigo.Text.Trim());
        if (txtNumeIdentificacion.Text.Trim() != "")
            vAporte.identificacion = Convert.ToString(txtNumeIdentificacion.Text.Trim());
        return vAporte;
    }

    #region Llenado de Listas

    private List<Xpinn.FabricaCreditos.Entities.Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, _usuario);
        return lstDatosSolicitud;
    }

    protected void LlenarComboLineaAporte(DropDownList ddlOficina)
    {
        DdlLineaAporte.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        AporteServices aporteService = new AporteServices();
        Aporte aporte = new Aporte();

        DdlLineaAporte.DataSource = aporteService.ListarLineaAporte(aporte, _usuario);
        DdlLineaAporte.DataTextField = "nom_linea_aporte";
        DdlLineaAporte.DataValueField = "cod_linea_aporte";
        DdlLineaAporte.DataBind();
        DdlLineaAporte.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    protected void LlenarComboPeriodicidad(DropDownList DdlPeriodicidad)
    {
        PeriodicidadService periodicidadService = new PeriodicidadService();
        Periodicidad periodicidad = new Periodicidad();

        DdlPeriodicidad.DataSource = periodicidadService.ListarPeriodicidad(periodicidad, _usuario);
        DdlPeriodicidad.DataTextField = "Descripcion";
        DdlPeriodicidad.DataValueField = "Codigo";
        DdlPeriodicidad.DataBind();
    }

    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, _usuario);
    }

    private void CargarListas()
    {
        try
        {
            // Tipos de identificación
            ListaSolicitada = "TipoIdentificacion";
            TraerResultadosLista();
            DdlTipoIdentificacion.DataSource = lstDatosSolicitud;
            DdlTipoIdentificacion.DataTextField = "ListaDescripcion";
            DdlTipoIdentificacion.DataValueField = "ListaId";
            DdlTipoIdentificacion.DataBind();
            // Empresas de recaudo
            if (txtCodigoCliente.Text != "")
                PoblarLista("v_persona_empresa_recaudo", "Distinct cod_empresa, nom_empresa", " cod_persona = " + txtCodigoCliente.Text, "", ddlEmpresa);
            else
                PoblarLista("empresa_recaudo", ddlEmpresa);

            PoblarLista("Lineaaporte", "", "cod_linea_aporte not in (select cod_linea_aporte from GRUPO_LINEAAPORTE where principal = 0)", "", ddlLineaFiltro);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.GetType().Name + "L", "CargarListas", ex);
        }
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
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, _usuario);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        plista.Insert(1, new ListaDesplegable() { idconsecutivo = null, descripcion = "No se encontraron datos" });
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;

        try
        {
            ddlControl.DataBind();
        }
        catch (Exception)
        {
            ddlControl.SelectedIndex = 1;
        }

    }

    #endregion

    protected Boolean ValidarData()
    {
        _usuario = (Usuario)Session["usuario"];
        LblMensaje.Text = "";
        if (DdlLineaAporte.SelectedIndex == 0 || DdlLineaAporte.SelectedValue == "0")
        {
            LblMensaje.Text = "Seleccione la Linea de Aporte";
            return false;
        }
        if (txtNumeIdentificacion.Text == "" || txtFecha_apertura.Text == "" || txtValorCuota.Text == "")
        {
            this.LblMensaje.Text = "Por favor verificar los campos Identificacion,Cuota,fechaApertura";
            return false;
        }
        if (!string.IsNullOrEmpty(txtFecha_apertura.Text))
        {
            DateTime Apertura = Convert.ToDateTime(txtFecha_apertura.Text);
            if (Apertura > DateTime.Now)
            {
                this.LblMensaje.Text = "La fecha de apertura no puede ser superior a la fecha actual";
                return false;
            }
        }
        if (DdlTipoCuota.SelectedValue == "4") // Tipo de Cuota % del sueldo
        {
            if (txtValorCuota.Text == "0" || txtValorCuota.Text == "")
            {
                VerError("No se ingreso el valor de la cuota");
                return false;
            }
        }
        if (string.IsNullOrWhiteSpace(txtFecha_Proxppago.Text) || Convert.ToDateTime(txtFecha_Proxppago.Text) == DateTime.MinValue)
        {
            VerError("Fecha proximo pago invalida!.");
            return false;
        }

        if (txtporcenApo.Visible == true)
        {
            if (txtporcenApo.Text == "")
            {
                VerError("Ingresar el porcentaje para el aporte %");
                return false;
            }
        }
        List<Aporte> lstaport = AporteServicio.ListarCuentasPersona(Convert.ToInt64(txtCodigoCliente.Text), _usuario);
        if (lstaport.Count >= 0)
        {
            VerError("El Usuario tiene Un Aporte Social Activo");
            return false;
        }


        return true;
    }

    private void grabar()
    {
        Usuario usuap = new Usuario();

        try
        {
            Aporte aporte = new Aporte();
            String idObjeto2 = txtNumAporte.Text;

            //Consultar fecha de afiliación para que esta sea la fecha de apertura
            AfiliacionServices AfiliacionServicio = new AfiliacionServices();
            DateTime? fecAfiliacion = AfiliacionServicio.FechaAfiliacion(txtCodigoCliente.Text, (Usuario)Session["usuario"]);
            DateTime Apertura = Convert.ToDateTime(txtFecha_apertura.Text);

            aporte.cod_linea_aporte = Int32.Parse(this.DdlLineaAporte.SelectedValue);
            aporte.cod_oficina = Int32.Parse(txtOficina.Text);
            aporte.cod_persona = Int64.Parse(txtCodigoCliente.Text);
            aporte.fecha_apertura = Convert.ToDateTime(Apertura);
            aporte.cuota = Convert.ToDecimal(txtValorCuota.Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
            if (txtporcenApo.Visible == true)
            {
                if (txtporcenApo.Text != "")
                {
                    aporte.porcentaje_apo = Convert.ToDecimal(txtporcenApo.Text);
                }
            }
            aporte.cod_periodicidad = Int32.Parse(DdlPeriodicidad.SelectedValue);
            aporte.forma_pago = Int32.Parse(DdlFormaPago.SelectedValue);
            if (ddlEmpresa.Visible == true && ddlEmpresa.SelectedIndex != 0)
                aporte.cod_empresa = Convert.ToInt32(ddlEmpresa.SelectedValue);
            aporte.fecha_proximo_pago = DateTime.Parse(txtFecha_Proxppago.Text);
            aporte.fecha_ultimo_pago = aporte.fecha_apertura;
            aporte.fecha_interes = aporte.fecha_apertura;
            aporte.Saldo = 0;
            aporte.total_intereses = 0;
            aporte.total_retencion = 0;
            aporte.saldo_intereses = 0;
            aporte.estado = Int32.Parse(this.DdlEstado.SelectedValue);
            aporte.cod_usuario = Int32.Parse(usuap.codusuario.ToString());
            aporte.fecha_crea = DateTime.Now;

            aporte.lstBeneficiarios = ObtenerListaBeneficiarios();

            List<Aporte> lstConsulta = null;

            if (!string.IsNullOrWhiteSpace(idObjeto))
            {
                aporte.numero_aporte = Convert.ToInt64(txtNumAporte.Text);
                if (Session["LSTGRUPO"] != null && (lstConsulta = (List<Aporte>)Session["LSTGRUPO"]).Count > 0)
                {
                    foreach (Aporte lItem in lstConsulta)
                    {
                        if (lItem.principal != Int64.MinValue && lItem.principal == 1)
                            aporte.cuota = Convert.ToDecimal(txtValorCuota.Text);
                        else
                            aporte.cuota = 0;

                        aporte.cod_linea_aporte = lItem.cod_linea_aporte;
                        aporte.nom_linea_aporte = lItem.nom_linea_aporte;
                        aporte.numero_aporte = lItem.numero_aporte;

                        if (lItem.pendiente_crear)
                        {
                            aporte = AporteServicio.CrearAporte(aporte, _usuario);
                        }

                        else
                        {
                            aporte = AporteServicio.ModificarAporte(aporte, _usuario);
                        }
                    }
                }
                else
                {
                    aporte = AporteServicio.ModificarAporte(aporte, _usuario);
                }

                MvAfiliados.ActiveViewIndex = 2;

                Site toolBar = (Site)Master;
                toolBar.MostrarCancelar(false);
                toolBar.MostrarGuardar(false);
            }
            else
            {
                if (ConsultarClienteAporte(Convert.ToInt64(aporte.cod_persona)))
                {
                    if (Session["LSTGRUPO"] != null)
                    {
                        lstConsulta = (List<Xpinn.Aportes.Entities.Aporte>)Session["LSTGRUPO"];
                        foreach (Aporte lItem in lstConsulta)
                        {
                            if (lItem.principal != Int64.MinValue && lItem.principal == 1)
                                aporte.cuota = Convert.ToDecimal(txtValorCuota.Text);
                            else
                                aporte.cuota = 0;

                            aporte.cod_linea_aporte = lItem.cod_linea_aporte;
                            aporte.nom_linea_aporte = lItem.nom_linea_aporte;
                            aporte.numero_aporte = lItem.numero_aporte;

                            String estado = "";
                            DateTime fechacierrehistorico;
                            String formato = gFormatoFecha = "dd/MM/yyyy";
                            var x = Convert.ToDateTime(txtFecha_apertura.Text).ToString("dd/MM/yyyy");
                            DateTime Fechaapertura = DateTime.ParseExact(x, formato, CultureInfo.InvariantCulture);

                            Xpinn.Aportes.Entities.Aporte vaportes = new Xpinn.Aportes.Entities.Aporte();
                            vaportes = AporteServicio.ConsultarCierreAportes((Usuario)Session["usuario"]);
                            if (vaportes != null)
                            {
                                estado = vaportes.estadocierre;
                                fechacierrehistorico = Convert.ToDateTime(vaportes.fecha_cierre.ToString());
                            }
                            else { fechacierrehistorico = DateTime.MinValue; }


                            if (estado == "D" && Fechaapertura <= fechacierrehistorico)
                            {
                                VerError("NO PUEDE APERTURAR CUENTAS  EN PERIODOS YA CERRADOS, TIPO A,'APORTES'");
                                return;
                            }
                            else
                            {
                                aporte = AporteServicio.CrearAporte(aporte, _usuario);
                            }

                        }
                    }
                    else
                    {
                        aporte = AporteServicio.CrearAporte(aporte, _usuario);
                    }

                    MvAfiliados.ActiveViewIndex = 2;

                    Site toolBar = (Site)Master;
                    toolBar.MostrarCancelar(false);
                    toolBar.MostrarGuardar(false);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.GetType().Name, "btnGuardar_Click", ex);
        }
    }

    protected void ObtenerDatosDistribucion(String pIdObjeto)
    {
        try
        {
            Aporte aporte = new Aporte();
            if (pIdObjeto != null)
            {
                aporte = AporteServicio.ConsultarGrupoAporte(Convert.ToInt64(DdlLineaAporte.SelectedValue), _usuario);
                if (aporte.grupo == 0)
                {
                    //this.LblMensaje.Text = "Esta cuenta no pertenece a ningun grupo";
                    Session["LSTGRUPO"] = null;
                    gvLista.DataSource = null;
                    gvLista.DataBind();
                    gvLista.Visible = false;
                }
                if (aporte.grupo != 0)
                {
                    txtgrupoaporte.Text = aporte.grupo.ToString();
                    DistribucionNuevo();
                    this.LblMensaje.Text = "";
                }
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.GetType().Name + "A", "ObtenerDatosDistribucion", ex);
        }
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Aporte aporte = new Aporte();
            if (pIdObjeto != null)
            {
                aporte.numero_aporte = Int64.Parse(pIdObjeto);
                aporte = AporteServicio.ConsultarAporte(Convert.ToInt64(pIdObjeto), _usuario);
                if (!string.IsNullOrEmpty(aporte.numero_aporte.ToString()))
                {
                    txtNumAporte.Text = aporte.numero_aporte.ToString();
                    this.DdlLineaAporte.SelectedValue = aporte.cod_linea_aporte.ToString();
                    txtOficina.Text = aporte.cod_oficina.ToString();
                    txtFecha_apertura.Text = aporte.fecha_apertura.ToString(gFormatoFecha);
                    txtCodigoCliente.Text = aporte.cod_persona.ToString();
                    PoblarLista("v_persona_empresa_recaudo", "Distinct cod_empresa, nom_empresa", "cod_persona = " + txtCodigoCliente.Text, "", ddlEmpresa);
                    txtNumeIdentificacion.Text = aporte.identificacion.ToString();
                    this.DdlTipoIdentificacion.SelectedValue = aporte.tipo_identificacion.ToString();
                    txtNombre.Text = aporte.nombre.ToString();

                    //---------------------------------------------------
                    txtCuotaActual.Text = aporte.cuota.ToString();
                    txtValorCuota.Text = aporte.cuota.ToString(); ;
                    //---------------------------------------------------

                    DdlPeriodicidad.SelectedValue = aporte.cod_periodicidad.ToString();
                    DdlFormaPago.SelectedValue = aporte.forma_pago.ToString();
                    if (aporte.cod_empresa != 0 && aporte.cod_empresa != null)
                    {
                        ddlEmpresa.SelectedValue = aporte.cod_empresa.ToString();
                    }
                    DdlFormaPago_SelectedIndexChanged(DdlFormaPago, null);
                    txtOficina.Text = aporte.cod_oficina.ToString();
                    txtOficinaNombre.Text = aporte.nom_oficina;
                    txtFecha_Proxppago.Text = aporte.fecha_proximo_pago.ToString(gFormatoFecha);
                    if (aporte.fecha_cierre != DateTime.MinValue)
                        txtFecha_interes.Text = aporte.fecha_cierre.ToString(gFormatoFecha);
                    DdlEstado.SelectedValue = aporte.estado.ToString();
                    DdlEstado.Enabled = true;
                    panelCuotaActual.Visible = true;
                    Xpinn.Aportes.Entities.GrupoLineaAporte lineaAporte = new GrupoLineaAporte();
                    lineaAporte = LineaAporteServicio.ConsultarLineaAporte(Convert.ToInt64(DdlLineaAporte.SelectedValue), _usuario);
                    DdlTipoCuota.SelectedValue = Convert.ToString(lineaAporte.tipo_cuota);
                    if (DdlTipoCuota.SelectedValue == "4")
                    {
                        txtValorCuota.Enabled = false;
                        lblporcentajeApo.Visible = true;
                        txtporcenApo.Visible = true;
                        txtporcenApo.Text = aporte.porcentaje_apo.ToString();
                    }
                    //Beneficiarios
                    if (aporte.lstBeneficiarios.Count > 0)
                    {
                        gvBeneficiarios.DataSource = aporte.lstBeneficiarios;
                        gvBeneficiarios.DataBind();
                    }
                    upBeneficiarios.Visible = true;
                    txtFecha_apertura.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    #region eventos

    protected void txtValorCuota_TextChanged(object sender, EventArgs e)
    {
        VerError("");
        decimal cuota_total = 0;
        try
        {
            cuota_total = Convert.ToDecimal(txtValorCuota.Text);
            List<Xpinn.Aportes.Entities.Aporte> lstConsulta = new List<Xpinn.Aportes.Entities.Aporte>();
            System.Data.DataTable table = new System.Data.DataTable();

            if (Session["LSTGRUPO"] != null)
            {
                lstConsulta = (List<Xpinn.Aportes.Entities.Aporte>)Session["LSTGRUPO"];
                foreach (Aporte lItem in lstConsulta)
                {
                    lItem.cuota = Math.Round((lItem.porcentaje * cuota_total) / 100);
                    Session[AporteServicio.Codigoaporte + ".id"] = idObjeto;
                    //   vDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];
                }
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
            }
            if (DdlTipoCuota.SelectedValue == "4" && txtValorCuota.Text != "") // Tipo de Cuota % del sueldo
            {
                CalcularCuota();
            }
        }
        catch
        {
            VerError("No pudo distribuir la cuota");
        }
    }



    protected void DdlLineaAporte_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObtenerDatosDistribucion(DdlLineaAporte.SelectedValue);
        CalcularCuota();
        ValidarSalario();
    }

    protected void CalcularCuota()
    {
        if (DdlLineaAporte.SelectedValue == "0")
            return;
        // Obtener datos de la línea
        Xpinn.Aportes.Services.GrupoLineaAporteServices LineaAporteServicio = new Xpinn.Aportes.Services.GrupoLineaAporteServices();
        Xpinn.Aportes.Entities.GrupoLineaAporte lineaAporte = new GrupoLineaAporte();
        Int64 pId = Convert.ToInt64(DdlLineaAporte.SelectedValue);
        lineaAporte = LineaAporteServicio.ConsultarLineaAporte(pId, _usuario);
        if (lineaAporte != null)
        {
            if (lineaAporte.beneficiarios == 1)
            {
                upBeneficiarios.Visible = true;
            }

            if (lineaAporte.tipo_cuota > 0)
            {
                DdlTipoCuota.SelectedValue = Convert.ToString(lineaAporte.tipo_cuota);
                DdlTipoCuota.Enabled = false;
                if (DdlTipoCuota.SelectedValue == "1") // Cuota Fija
                {
                    txtValorCuota.Text = Convert.ToString(lineaAporte.valor_cuota_minima);
                }
                else if (DdlTipoCuota.SelectedValue == "4") // Porcentaje del Sueldo
                {
                    Int64 codCliente;
                    decimal salario, cuota;
                    lblporcentajeApo.Visible = true;
                    txtporcenApo.Visible = true;
                    codCliente = Convert.ToInt64(txtCodigoCliente.Text);
                    salario = AporteServicio.ConsultarClienteSalario(codCliente, _usuario);

                    //txtValorCuota.Text = Convert.ToString(Math.Round(lineaAporte.porcentaje_minimo / 100 * salario));
                    if (txtporcenApo.Text != "")
                    {
                        var p = txtporcenApo.Text.Replace(".", ",");
                        cuota = AporteServicio.Calcular_Cuota(salario, Convert.ToDecimal(p), Convert.ToDecimal(DdlPeriodicidad.SelectedValue), _usuario);
                        txtValorCuota.Text = Convert.ToString(cuota);
                        txtValorCuota.Enabled = false;
                    }
                    else
                    {
                        txtValorCuota.Text = "";
                        txtValorCuota.Enabled = false;
                    }


                }
                else if (DdlTipoCuota.SelectedValue == "5") // Porcentaje de SMLMV
                {
                    lblporcentajeApo.Visible = true;
                    txtporcenApo.Visible = true;
                    decimal smlmv;
                    smlmv = AporteServicio.ConsultarSMLMV(_usuario);
                    //txtValorCuota.Text = Convert.ToString(Math.Round(lineaAporte.porcentaje_minimo / 100 * smlmv));
                    if (txtporcenApo.Text != "")
                    {
                        txtValorCuota.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txtporcenApo.Text) / 100 * smlmv));
                        txtValorCuota.Enabled = false;
                    }
                    else
                    {
                        txtValorCuota.Text = "";
                        txtValorCuota.Enabled = false;
                    }

                }
            }
        }
    }

    protected void txtFecha_apertura_TextChanged(object sender, EventArgs e)
    {
        this.LblMensaje.Text = "";
        if (!string.IsNullOrEmpty(txtFecha_apertura.Text))
        {
            DateTime Apertura = Convert.ToDateTime(txtFecha_apertura.Text);

            if (Apertura <= DateTime.Now)
            {
                txtFecha_interes.Text = txtFecha_apertura.Text;
            }
            else
            {
                this.LblMensaje.Text = "La fecha de apertura no puede ser superior a la fecha actual";
            }
        }        
    }

    protected void txtValorCuota_Unload(object sender, EventArgs e)
    {
        txtValorCuota.Attributes.Add("onkeypress", "return ValidNum(event);");
    }

    protected void DdlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DdlFormaPago.SelectedItem.Value == "2" || DdlFormaPago.SelectedItem.Text == "Nomina")
        {
            lblEmpresa.Visible = true;
            ddlEmpresa.Visible = true;
        }
        else
        {
            lblEmpresa.Visible = false;
            ddlEmpresa.Visible = false;
        }
    }

    #endregion

    protected void gvListaAFiliados_SelectedIndexChanged(object sender, EventArgs e)
    {
        LblMensaje.Text = "";
        if (ddlLineaFiltro.SelectedIndex != 0)
        {
            // Determinar la identificacion
            GridView gvListaAFiliados = (GridView)sender;
            String identificacion = gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[3].Text;
            txtNumeIdentificacion.Text = identificacion;
            ConsultarCliente(identificacion);
            DdlLineaAporte.Enabled = true;
            // Acticar para crear la cuenta si la persona no tiene ya una cuenta por esta línea
            if (ConsultarClienteAporte(identificacion) == true)
            {
                MvAfiliados.ActiveViewIndex = 1;
                pBusqueda.Visible = false;
                // Determinar la línea de aporte
                DdlLineaAporte.Enabled = false;
                DdlLineaAporte.SelectedValue = ddlLineaFiltro.SelectedValue;
                DdlLineaAporte_SelectedIndexChanged(DdlLineaAporte, null);
                // Consultar datos de la afiliación de la persona para tomar datos por defecto
                ConsultarDatosAfiliacion();
                // Determminar las empresas a la que pertenece la persona
                if (txtCodigoCliente.Text != "")
                {
                    PoblarLista("v_persona_empresa_recaudo", "Distinct cod_empresa, nom_empresa", " cod_persona = " + txtCodigoCliente.Text, "", ddlEmpresa);

                    //Agregado para que la fecha de apertura sea igual a la fecha de afiliación
                    AfiliacionServices AfiliacionServicio = new AfiliacionServices();
                    DateTime? fecAfiliacion = AfiliacionServicio.FechaAfiliacion(txtCodigoCliente.Text, (Usuario)Session["usuario"]);
                    txtFecha_apertura.Text = fecAfiliacion.ToString() != null ?  DateTime.Now.ToShortDateString() : Convert.ToDateTime(fecAfiliacion).ToShortDateString();
                }
                // Calcular la cuota según la línea seleccionada
                CalcularCuota();
                ValidarSalario();
                // Habilitar la barra de herramientas
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(true);
                toolBar.MostrarConsultar(false);
            }
        }
        else
        {
            LblMensaje.Text = "Seleccion una Linea";
        }
    }

    protected void txtNumeIdentificacion_TextChanged(object sender, EventArgs e)
    {

    }

    private void ValidarSalario()
    {
        if (DdlTipoCuota.SelectedValue == "4") // Tipo de Cuota % del sueldo
        {
            decimal salario;
            salario = AporteServicio.ConsultarClienteSalario(Convert.ToInt64(txtCodigoCliente.Text), _usuario);
            if (salario == 0 || salario == null)
            {
                VerError("El Titular de la cuenta no tiene un salario registrado");
            }
        }
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void txtporcenApo_TextChanged(object sender, EventArgs e)
    {
        GrupoLineaAporte inflineaaport = new GrupoLineaAporte();
        inflineaaport = LineaAporteServicio.ConsultarLineaAporte(Convert.ToInt64(DdlLineaAporte.Text), Usuario);
        var p = txtporcenApo.Text.Replace(".",",");
        if (Convert.ToDecimal(p) >= inflineaaport.porcentaje_minimo && Convert.ToDecimal(p) <= inflineaaport.porcentaje_maximo)
        {
            // Recalcular la cuota de aporte
            CalcularCuota();
            VerError("");
        }
        else
        {
            VerError("El porcentaje del aporte esta fuera de lo parametrizado en la linea de aporte de % minimo " + inflineaaport.porcentaje_minimo + " a % maximo " + inflineaaport.porcentaje_maximo);
            txtporcenApo.Text = "";
            txtValorCuota.Text = "";
        }

    }



    #region beneficiarios
    protected void gvBeneficiarios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlSexo = (DropDownList)e.Row.FindControl("ddlsexo");
            DropDownList ddlParentezco = (DropDownList)e.Row.FindControl("ddlParentezco");
            if (ddlParentezco != null)
            {
                Beneficiario Ben = new Beneficiario();
                ddlParentezco.DataTextField = "DESCRIPCION";
                ddlParentezco.DataValueField = "CODPARENTESCO";
                ddlParentezco.DataSource = BeneficiarioServicio.ListarParentesco(Ben, (Usuario)Session["usuario"]);
                ddlParentezco.Items.Insert(0, new ListItem("<Seleccione un item>", "0"));
                ddlParentezco.DataBind();
            }
            Label lblParentesco = (Label)e.Row.FindControl("lblParentesco");
            if (lblParentesco.Text != null)
                ddlParentezco.SelectedValue = lblParentesco.Text;
        }
    }

    protected void btnAddRowBeneficio_Click(object sender, EventArgs e)
    {
        //Session["DatosBene"] = null;

        List<Beneficiario> lstBene = new List<Beneficiario>();
        lstBene = ObtenerListaBeneficiarios();
        int porcentaje = 0;
        porcentaje = Convert.ToInt32(lstBene.Where(x => x.porcentaje_ben > 0).Sum(x => x.porcentaje_ben));
        if (porcentaje < 100)
        {
            for (int i = 1; i <= 1; i++)
            {
                Beneficiario eBenef = new Beneficiario();
                eBenef.idbeneficiario = -1;
                eBenef.nombre = "";
                eBenef.identificacion_ben = "";
                eBenef.tipo_identificacion_ben = null;
                eBenef.nombre_ben = "";
                eBenef.fecha_nacimiento_ben = null;
                eBenef.parentesco = 0;
                eBenef.porcentaje_ben = null;
                eBenef.edad = null;
                eBenef.sexo = "0";
                lstBene.Add(eBenef);
            }

            if (lstBene.Count > 0)
            {
                gvBeneficiarios.DataSource = lstBene;
                gvBeneficiarios.DataBind();
            }
        }
        else if (porcentaje == 100)
        {
            VerError("El porcentaje total de beneficiarios se encuentra completo");
        }
    }

    protected List<Beneficiario> ObtenerListaBeneficiarios()
    {
        List<Beneficiario> lstBeneficiarios = new List<Beneficiario>();

        foreach (GridViewRow rfila in gvBeneficiarios.Rows)
        {
            Beneficiario eBenef = new Beneficiario();
            HiddenField lblidbeneficiario = (HiddenField)rfila.FindControl("hdIdBeneficiario");
            if (lblidbeneficiario.Value != "")
                eBenef.idbeneficiario = Convert.ToInt64(lblidbeneficiario.Value);

            eBenef.numero_programado = idObjeto;

            DropDownList ddlParentezco = (DropDownList)rfila.FindControl("ddlParentezco");
            if (ddlParentezco.SelectedValue != null || ddlParentezco.SelectedIndex != 0)
                eBenef.parentesco = Convert.ToInt32(ddlParentezco.SelectedValue);

            DropDownList ddlSexo = (DropDownList)rfila.FindControl("ddlsexo");
            if (ddlSexo.SelectedValue != null)
                eBenef.sexo = Convert.ToString(ddlSexo.SelectedValue);

            TextBox txtIdentificacion = (TextBox)rfila.FindControl("txtIdentificacion");
            if (txtIdentificacion != null)
                eBenef.identificacion_ben = Convert.ToString(txtIdentificacion.Text);

            TextBox txtEdadBen = (TextBox)rfila.FindControl("txtEdadBen");
            if (txtEdadBen != null)
            {
                if (txtEdadBen.Text != "")
                {
                    eBenef.edad = Convert.ToInt32(txtEdadBen.Text);
                }
            }
            TextBox txtNombres = (TextBox)rfila.FindControl("txtNombres");
            if (txtNombres != null)
                eBenef.nombre_ben = Convert.ToString(txtNombres.Text);

            fechaeditable txtFechaNacimientoBen = (fechaeditable)rfila.FindControl("txtFechaNacimientoBen");
            if (txtFechaNacimientoBen != null)
                if (txtFechaNacimientoBen.Texto != "")
                    eBenef.fecha_nacimiento_ben = txtFechaNacimientoBen.ToDateTime;
                else
                    eBenef.fecha_nacimiento_ben = null;
            else
                eBenef.fecha_nacimiento_ben = null;
            decimalesGridRow txtPorcentaje = (decimalesGridRow)rfila.FindControl("txtPorcentaje");
            if (txtPorcentaje != null)
                eBenef.porcentaje_ben = Convert.ToDecimal(txtPorcentaje.Text);

            lstBeneficiarios.Add(eBenef);
        }
        return lstBeneficiarios;
    }

    protected void gvBeneficiarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            List<Beneficiario> LstBene = ObtenerListaBeneficiarios();
            int IdBeneficiario = Convert.ToInt32(gvBeneficiarios.DataKeys[e.RowIndex].Values[0].ToString());

            if (IdBeneficiario > 0)
            {
                BeneficiarioServicio.EliminarBeneficiarioAporte(IdBeneficiario, (Usuario)Session["usuario"]);
            }

            LstBene.RemoveAt((gvBeneficiarios.PageIndex * gvBeneficiarios.PageSize) + e.RowIndex);
            gvBeneficiarios.DataSource = LstBene;
            gvBeneficiarios.DataBind();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }

    protected string FormatoFecha()
    {
        Configuracion conf = new Configuracion();
        return conf.ObtenerFormatoFecha();
    }


    #endregion

    protected void txtPorcentaje_eventoCambiar(object sender, EventArgs e)
    {
        TextBox txtPorcentaje = sender as TextBox;
        List<Beneficiario> lstBeneficiarios = ObtenerListaBeneficiarios();
        if (lstBeneficiarios != null)
        {
            int porcentaje = 0;
            porcentaje = Convert.ToInt32(lstBeneficiarios.Where(x => x.porcentaje_ben > 0).Sum(x => x.porcentaje_ben));
            if (porcentaje > 100)
            {
                txtPorcentaje.Text = "";
                VerError("La sumatoria del porcentaje es mayor a 100");
                RegistrarPostBack();
            }
        }
    }
}