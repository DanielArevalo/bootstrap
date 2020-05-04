using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Interfaces.Entities;
using Xpinn.Interfaces.Services;
using Xpinn.Riesgo.Services;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Services;

public partial class Nuevo : GlobalWeb
{
    HistoricoSegmentacionService _historicoService = new HistoricoSegmentacionService();
    TradeUSServices _tradeService = new TradeUSServices();
    Xpinn.Aportes.Services.AfiliacionServices _afiliacionServicio = new Xpinn.Aportes.Services.AfiliacionServices();
    ParametrizacionProcesoAfilicacionService _paramProceso = new ParametrizacionProcesoAfilicacionService();
    ImagenesService imagenService = new ImagenesService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_historicoService.CodigoPrograma4, "D");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlBusquedaPersonas.eventoEditar += gvControl_RowEditing;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_historicoService.CodigoPrograma4, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lblerror.Visible = false;
                CargarLista();
                if (Session[_historicoService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[_historicoService.CodigoPrograma + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                }
                else if(Session[_afiliacionServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[_afiliacionServicio.CodigoPrograma + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    mvConsultas.ActiveViewIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_historicoService.CodigoPrograma4, "Page_Load", ex);
        }
    }

    private void CargarLista()
    {
        PoblarListas poblar = new PoblarListas();
        poblar.PoblarListaDesplegable("TIPOIDENTIFICACION", "CODTIPOIDENTIFICACION, DESCRIPCION", "", "1", ddlTipoIdentificacion, (Usuario)Session["usuario"]);
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

        _afiliacionServicio.controlRutaAfiliacion(control, (Usuario)Session["Usuario"]);
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string script = @"<script type='text/javascript'>
                                iprint('#Iframe2');
                          </script>";

        // ScriptManager.RegisterStartupScript(this, typeof(Page), "impresionlistas", script, false);
        
        List<TradeUSSearchInd> lstOFAC = new List<TradeUSSearchInd>();
        List<Individual> lstONUInd = new List<Individual>();
        List<Entity> lstONUEnt = new List<Entity>();

        lstOFAC = ObtenerListaOFAC();
        lstONUInd = ObtenerListaONUInd();
        lstONUEnt = ObtenerListaONUEnt();
        
        bool nuevo = false;
        nuevo = Session[_afiliacionServicio.CodigoPrograma + ".nuevo"] != null ? Convert.ToBoolean(Session[_afiliacionServicio.CodigoPrograma + ".nuevo"]) : false;

        SarlaftAlertaServices salarftServicio = new SarlaftAlertaServices();
        salarftServicio.CrearRegistroConsultaLista(lstOFAC, lstONUInd, lstONUEnt, Convert.ToInt64(txtCodigo.Text), nuevo, chkR.Checked, chkP.Checked, chkC.Checked, chkU.Checked, chkL.Checked, (Usuario)Session["usuario"]);

        if (Session[_historicoService.CodigoPrograma + ".id"] != null)
        {
            Session.Remove(imagenService.CodigoPrograma.ToString() + ".cod_persona");
            Session.Remove(_afiliacionServicio.CodigoPrograma + "last");
            Session.Remove(_afiliacionServicio.CodigoPrograma + "next");
            Session.Remove(_afiliacionServicio.CodigoPrograma + ".id");
            Session.Remove(_afiliacionServicio.CodigoPrograma + ".modificar");
            Session.Remove("lstParametros");
            Session.Remove(_historicoService.CodigoPrograma + ".id");
            Navegar(Pagina.Lista);
        }
        else if (Session[_afiliacionServicio.CodigoPrograma + ".id"] != null)
        {
            string cod_per = Session[_afiliacionServicio.CodigoPrograma + ".id"].ToString();
            string nvNext = Session[_afiliacionServicio.CodigoPrograma + "next"].ToString();
            string nvLast = Session[_afiliacionServicio.CodigoPrograma + "last"].ToString();
            ParametrizacionProcesoAfiliacion vParam = new ParametrizacionProcesoAfiliacion();
            vParam.lstParametros = _paramProceso.ListarParametrosProcesoAfiliacion((Usuario)Session["usuario"]).Where(x => x.cod_proceso != 1).ToList();
            // Se puso la condición para que no cambie de estado si estoy haciendo la consulta
            if (!_paramProceso.controlRegistrado(6, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]) && txtEstado.Text != "A")
                _paramProceso.cambiarEstadoAsociado("L", Convert.ToInt64(cod_per), (Usuario)Session["usuario"]);
            bool stop = false;
            int c = -1;
            //CONTROL DE RUTA PARA LA EVALUACIÓN 
            if (nvNext == null || nvNext == "")
                nvNext = Convert.ToString(6);
            registrarControl(Convert.ToInt32(nvNext), Convert.ToInt64(cod_per));
            foreach (ParametrizacionProcesoAfiliacion redirect in vParam.lstParametros)
            {
                c++;
                if (redirect.cod_proceso != Convert.ToInt32(nvLast) && redirect.cod_proceso != Convert.ToInt32(nvNext))
                {
                    switch (redirect.cod_proceso)
                    {
                        case 4:
                            if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                            {
                                Session["cedula"] = (string)Session["identificacion"].ToString();
                                Session[_afiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                                Session[_afiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                                _afiliacionServicio.notificarEmail(4, vParam.nombre, Convert.ToInt64(cod_per), redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                                Navegar("../../Aportes/CuentasAportes/Nuevo.aspx");
                                stop = true;
                            }
                            break;
                        case 5:
                            if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                            {
                                Session[imagenService.CodigoPrograma.ToString() + ".cod_persona"] = Session[Usuario.codusuario + "Cod_persona"].ToString();
                                Session[_afiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                                Session[_afiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                                _afiliacionServicio.notificarEmail(5, vParam.nombre, Convert.ToInt64(cod_per), redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                                Navegar("../../Aportes/ImagenesPersona/Nuevo.aspx");
                                stop = true;
                            }
                            break;
                        case 6:
                            if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                            {
                                Session[_afiliacionServicio.CodigoPrograma + ".id"] = cod_per;
                                Session[_afiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                                Session[_afiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                                _afiliacionServicio.notificarEmail(6, vParam.nombre, Convert.ToInt64(cod_per), redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                                Navegar("../../../GestionRiesgo/ConsultasOFAC/Nuevo.aspx");
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
                                Session[_afiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                                Session[_afiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                                _afiliacionServicio.notificarEmail(7, vParam.nombre, Convert.ToInt64(cod_per), redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
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
                                Session[_afiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                                Session[_afiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                                _afiliacionServicio.notificarEmail(8, vParam.nombre, Convert.ToInt64(cod_per), redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
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
                // Se puso la condición para que no cambie de estado si estoy haciendo la consulta
                if (!_paramProceso.controlRegistrado(6, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]) && txtEstado.Text != "A")
                    _paramProceso.cambiarEstadoAsociado("A", Convert.ToInt64(cod_per), (Usuario)Session["usuario"]);
                Session.Remove(_afiliacionServicio.CodigoPrograma + ".id");
                Navegar("../../Aportes/Afiliaciones/Lista.aspx");
            }

        }
        else
        {
            Session.Remove(imagenService.CodigoPrograma.ToString() + ".cod_persona");
            Session.Remove(_afiliacionServicio.CodigoPrograma + "last");
            Session.Remove(_afiliacionServicio.CodigoPrograma + "next");
            Session.Remove(_afiliacionServicio.CodigoPrograma + ".id");
            Session.Remove(_afiliacionServicio.CodigoPrograma + ".modificar");
            Session.Remove("lstParametros");
            Session.Remove(_historicoService.CodigoPrograma + ".id");
            Navegar(Pagina.Lista);
        }

    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        ctlBusquedaPersonas.Actualizar(0);
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        ImagenesService imagenService = new ImagenesService();
        if (Session[_historicoService.CodigoPrograma + ".id"] != null)
        {
            Session.Remove(_historicoService.CodigoPrograma + ".id");
            Navegar(Pagina.Lista);
        }
        else if (Session[_afiliacionServicio.CodigoPrograma + ".id"] != null)
        {
            string cod_per = Session[_afiliacionServicio.CodigoPrograma + ".id"].ToString();
            Int32 act = Convert.ToInt32(Session[_afiliacionServicio.CodigoPrograma + "last"].ToString());
            String id = Session[_afiliacionServicio.CodigoPrograma + ".id"].ToString();
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
                    Session[_afiliacionServicio.CodigoPrograma + ".id"] = id;
                    Session[_afiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c].cod_proceso;
                    Session[_afiliacionServicio.CodigoPrograma + ".modificar"] = 0;
                    Navegar("../../Aportes/ConfirmaAfiliacion/Lista.aspx");
                    break;
                case 3:
                    Session[_afiliacionServicio.CodigoPrograma + ".id"] = id;
                    Session[_afiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c].cod_proceso;
                    Session[_afiliacionServicio.CodigoPrograma + ".modificar"] = 0;
                    Navegar("../../Aportes/Personas/Tabs/Nuevo.aspx");
                    break;
                case 4:
                    Session["cedula"] = txtIdentificacion.Text;
                    Session[_afiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c].cod_proceso;
                    Navegar("../../Aportes/CuentasAportes/Nuevo.aspx");
                    break;
                case 5:
                    Session[_afiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c].cod_proceso;
                    Session[imagenService.CodigoPrograma.ToString() + ".cod_persona"] = cod_per;
                    Navegar("../../Aportes/ImagenesPersona/Nuevo.aspx");
                    break;
                case 7:
                    string codOpcion = "170901";
                    Session[_afiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c].cod_proceso;
                    Session["CodOpcion"] = codOpcion;
                    Session[codOpcion.ToString() + ".id"] = cod_per;
                    Navegar("../../Aportes/ProcesosAfiliacion/Nuevo.aspx");
                    break;
                case 8:
                    string codOpci = "170903";
                    Session[_afiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c].cod_proceso;
                    Session["CodOpcion"] = codOpci;
                    Session[codOpci.ToString() + ".id"] = cod_per;
                    Session[_afiliacionServicio.CodigoPrograma + "next"] = "lst";
                    Navegar("../../Aportes/ProcesosAfiliacion/Nuevo.aspx");
                    break;
            }
        }
        else
        {
            Session.Remove(imagenService.CodigoPrograma.ToString() + ".cod_persona");
            Session.Remove(_afiliacionServicio.CodigoPrograma + "last");
            Session.Remove(_afiliacionServicio.CodigoPrograma + "next");
            Session.Remove(_afiliacionServicio.CodigoPrograma + ".id");
            Session.Remove(_afiliacionServicio.CodigoPrograma + ".modificar");
            Session.Remove("lstParametros");
            Navegar(Pagina.Lista);
        }
    }
    
    protected void gvControl_RowEditing(object sender, EventArgs e)
    {
        String Codigo = ctlBusquedaPersonas.gvListado.DataKeys[ctlBusquedaPersonas.gvListado.SelectedRow.RowIndex].Value.ToString();
        /***********VALIDAR PROCESO DE AFILIACION******/
        ParametrizacionProcesoAfiliacion vParam = new ParametrizacionProcesoAfiliacion();
        vParam.lstParametros = _paramProceso.ListarParametrosProcesoAfiliacion((Usuario)Session["usuario"]).Where(x => x.cod_proceso != 1).ToList();
        int c = 0;
        int p = 0;
        foreach (ParametrizacionProcesoAfiliacion redirect in vParam.lstParametros){
            if (redirect.cod_proceso == 6 && Convert.ToBoolean(redirect.requerido))
            {
                p = c - 1;
                if (p < 0)
                    p = 0;
                while (!Convert.ToBoolean(vParam.lstParametros[p].requerido) && c > 0) {c = c - 1; }
                break;
            }
            c++;}
        if (c > 0)
            c = c - 1;
        bool bseguir = true;
        ParametrizacionProcesoAfiliacion control = new ParametrizacionProcesoAfiliacion();
        if (vParam.lstParametros.Count > 0)
        { 
            var ant = vParam.lstParametros[c].cod_proceso;        
            control = _paramProceso.validarProcesoAnterior(Codigo, ant, (Usuario)Session["usuario"]);
            if (control.estado_asociado == control.estado_proceso)
                bseguir = true;
            else
                bseguir = false;
        }
        if (bseguir == true)
        {
            ctlBusquedaPersonas.CodigoPrograma = "";
            Site toolBar = (Site)this.Master;
            toolBar.MostrarLimpiar(false);
            toolBar.MostrarGuardar(true);
            toolBar.MostrarConsultar(false);
            toolBar.MostrarRegresar(true);

            ObtenerDatos(Codigo);
        }else
        {
            lblerror.Visible = true;
            lblerror.Text = ("Aún no tienes acceso a este paso, por favor sigue la hoja de ruta para la afiliación. ");
            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Restricción", "alert('Aún no tienes acceso a este paso, por favor sigue la hoja de ruta para la afiliación');", true);
        }
    }

    private void ObtenerDatos(string idObjeto)
    {
        //Cargar datos de la persona
        Xpinn.FabricaCreditos.Entities.Persona1 pDatos = new Xpinn.FabricaCreditos.Entities.Persona1();
        Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();

        pDatos = personaServicio.ConsultaDatosPersona(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

        if(pDatos.identificacion != null && pDatos.identificacion != "")
        {
            txtCodigo.Text = idObjeto;
            txtIdentificacion.Text = pDatos.identificacion.ToString();
            ddlTipoIdentificacion.SelectedValue = pDatos.tipo_identificacion.ToString();
            txtNombres.Text = pDatos.nombres + " " + pDatos.apellidos;
            txtTipoPersona.Text = pDatos.tipo_persona == "N" ? "NATURAL" : pDatos.tipo_persona == "J" ? "JURIDICA" : "";
            txtEstado.Text = pDatos.estado;

            if(pDatos.tipo_persona == "N")
            {
                ConsultaOFAC();
                ConsultaONU();
            }
            else if (pDatos.tipo_persona == "J")
            {
                ConsultaONU();
                ConsultaOFAC();
                tabOFAQ.Visible = true;
                tabPolicia.Visible = false;
                TabProcuraduria.Visible = false;
                tabRegistraduria.Visible = false;
                TabContraloria.Visible = false;
                tabRues.Visible = true;
            }
            mvConsultas.ActiveViewIndex = 1;
        }
        else
        {
            VerError("No se puede generar la consulta, error al consultar la persona");
        }
    }

    private void ConsultaOFAC()
    {
        try
        {
            InterfazRIESGO intRiesgo = new InterfazRIESGO();
            TradeUSSearchResults tradeResult;
            List<TradeUSSearchInd> lstOFAC = new List<TradeUSSearchInd>();
            TradeUSSearchInd pObjeto;

            tradeResult = intRiesgo.ConsultarPersonaRiesgo(txtIdentificacion.Text, txtNombres.Text);
          
            if (tradeResult != null && tradeResult.results.Count > 0)
            {
                foreach (TradeUSSearchEntity entidad in tradeResult.results)
                {
                    pObjeto = new TradeUSSearchInd();
                    pObjeto.id = string.Join(",", (from i in entidad.ids where i.number != null select i.number).ToList());
                    pObjeto.source = entidad.source;
                    pObjeto.name = entidad.name;
                    pObjeto.alt_name = string.Join(",", entidad.alt_names);
                    pObjeto.date_of_birth = string.Join(",", entidad.dates_of_birth);
                    pObjeto.program = string.Join(",", entidad.programs);
                    pObjeto.nationality = string.Join(",", entidad.nationalities);
                    pObjeto.address = string.Join("-", (from i in entidad.addresses where i.address != null select i.address).ToList());
                    pObjeto.citizenship = string.Join(",", entidad.citizenships);
                    pObjeto.place_of_birth = string.Join(",", entidad.places_of_birth);
                    pObjeto.coincidencia = true;
                    lstOFAC.Add(pObjeto);
                }
            }
            else
            {
                pObjeto = new TradeUSSearchInd();
                pObjeto.id = txtIdentificacion.Text;
                pObjeto.source = "No se encuentra registro en las listas";
                pObjeto.name = txtNombres.Text;
                pObjeto.alt_name = "";
                pObjeto.date_of_birth = "";
                pObjeto.program = "";
                pObjeto.nationality = "";
                pObjeto.address = "";
                pObjeto.citizenship = "";
                pObjeto.place_of_birth = "";
                pObjeto.coincidencia = false;
                lstOFAC.Add(pObjeto);
            }

            if (lstOFAC.Count > 0)
            {
                ViewState["lstOFAC"] = lstOFAC;
                gvListaOFAC.DataSource = lstOFAC;
                gvListaOFAC.DataBind();
                lblOFAC.Visible = true;
                lblResultadosOFAC.Text = "Registros encontrados: " + lstOFAC.Count();
            }
            else
                lblResultadosOFAC.Text = "La persona no tiene registro en listas OFAC";
        }
        catch(Exception ex)
        {
            VerError("Error al generar la consulta de lista OFAC " + ex.Message);
        }
    }


    private void ConsultaONU()
    {
        try
        {
            InterfazRIESGO intRiesgo = new InterfazRIESGO();
            Individual PersonaIndividual = new Individual();
            List<Individual> lstIndividual = new List<Individual>();
            Entity PersonaEntidad = new Entity();
            List<Entity> lstEntidad = new List<Entity>();
            string URL = Request.Url.ToString();
            string tipo_persona = txtTipoPersona.Text == "NATURAL" ? "N" : txtTipoPersona.Text == "JURIDICA" ? "J" : "";
            string nombre = "";

            intRiesgo.ConsultarPersonaCSONU(txtIdentificacion.Text, txtNombres.Text, tipo_persona, ref PersonaIndividual, ref PersonaEntidad);

            if (tipo_persona == "N" && (PersonaIndividual.first_name != null || PersonaIndividual.second_name != null || PersonaIndividual.third_name != null))
            {
                if (PersonaIndividual.first_name != null)
                    nombre += PersonaIndividual.first_name.Trim();
                if (PersonaIndividual.second_name != null)
                    nombre += PersonaIndividual.second_name.Trim();
                if (PersonaIndividual.third_name != null)
                    nombre += PersonaIndividual.third_name.Trim();

                PersonaIndividual.first_name = nombre;
                PersonaIndividual.designation = PersonaIndividual.lstDesignation != null ? string.Join(",", PersonaIndividual.lstDesignation.Select(x => x.value)) : "";
                PersonaIndividual.nationality = PersonaIndividual.lstnationality != null ? string.Join(",", PersonaIndividual.lstnationality.Select(x => x.value)) : "";
                PersonaIndividual.list_type = PersonaIndividual.lstType != null ? string.Join(",", PersonaIndividual.lstType.Select(x => x.value)) : "";
                PersonaIndividual.coincidencia = true;

                lstIndividual.Add(PersonaIndividual);
                gvONUIndividual.DataSource = lstIndividual;
                gvONUIndividual.DataBind();
                gvONUIndividual.Visible = true;
                lblOnu.Text = "Registros encontrados: " + lstIndividual.Count();
            }
            else if (tipo_persona == "N" && PersonaIndividual.first_name == null || PersonaIndividual.second_name == null || PersonaIndividual.third_name == null)
            {
                PersonaIndividual.dataid = txtIdentificacion.Text;
                PersonaIndividual.first_name = txtNombres.Text;
                PersonaIndividual.comments1 = "No se encuentra registro en las listas";
                PersonaIndividual.coincidencia = false;

                lstIndividual.Add(PersonaIndividual);
                gvONUIndividual.DataSource = lstIndividual;
                gvONUIndividual.DataBind();
                gvONUIndividual.Visible = true;
                lblOnu.Text = "La persona no tiene registro en listas CS/NU";
            }
            else if (tipo_persona == "J" && PersonaEntidad != null)
            {
                PersonaEntidad.coincidencia = true;
                lstEntidad.Add(PersonaEntidad);
                gvONUEntidad.DataSource = lstEntidad;
                gvONUEntidad.DataBind();
                gvONUEntidad.Visible = true;
                lblOnu.Text = "Registros encontrados: " + lstEntidad.Count();
            }
            else if (tipo_persona == "J" && PersonaEntidad.dataid == null)
            {
                PersonaEntidad.first_name = txtNombres.Text;
                PersonaEntidad.comments1 = "No se encuentra registro en las listas";
                PersonaEntidad.un_list_type = DateTime.Now.ToShortDateString();
                PersonaEntidad.coincidencia = false;
            
                lstEntidad.Add(PersonaEntidad);
                gvONUEntidad.DataSource = lstEntidad;
                gvONUEntidad.DataBind();
                gvONUEntidad.Visible = true;
                lblOnu.Text = "La persona no tiene registro en listas CS/NU";
            }
            else
            {
                lblOnu.Text = "La persona no tiene registro en listas CS/NU";
            }

        }
        catch (Exception ex)
        {
            VerError("Error al generar la consulta de lista CS/NU " +ex.Message);
        }
    }

    protected void gvListaOFAC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvListaOFAC.PageIndex = e.NewPageIndex;
        if (ViewState["lstOFAC"] != null)
        {
            List<TradeUSSearchInd> lstOFAC = new List<TradeUSSearchInd>();
            lstOFAC = (List<TradeUSSearchInd>)ViewState["lstOFAC"];
            if (lstOFAC.Count > 0)
            {
                gvListaOFAC.DataSource = lstOFAC;
                gvListaOFAC.DataBind();
            }
        }
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gvLista = e.Row.NamingContainer as GridView;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gvLista.ID == "gvListaOFAC")
                {
                    TradeUSSearchInd dataItem = e.Row.DataItem as TradeUSSearchInd;

                    Image imageCheck = e.Row.FindControl("btnCheck") as Image;
                    Image imageEquis = e.Row.FindControl("btnEquis") as Image;

                    imageCheck.Visible = dataItem.coincidencia;
                    imageEquis.Visible = !dataItem.coincidencia;
                }

                if(gvLista.ID == "gvONUIndividual")
                {
                    Individual dataItem = e.Row.DataItem as Individual;

                    Image imageCheck = e.Row.FindControl("btnCheck") as Image;
                    Image imageEquis = e.Row.FindControl("btnEquis") as Image;

                    imageCheck.Visible = dataItem.coincidencia;
                    imageEquis.Visible = !dataItem.coincidencia;
                }

                if (gvLista.ID == "gvONUEntidad")
                {
                    Entity dataItem = e.Row.DataItem as Entity;

                    Image imageCheck = e.Row.FindControl("btnCheck") as Image;
                    Image imageEquis = e.Row.FindControl("btnEquis") as Image;

                    imageCheck.Visible = dataItem.coincidencia;
                    imageEquis.Visible = !dataItem.coincidencia;
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_historicoService.CodigoPrograma4, "gvLista_RowDataBound", ex);
        }
    }

    private List<TradeUSSearchInd> ObtenerListaOFAC()
    {
        List<TradeUSSearchInd> listaOFAC = new List<TradeUSSearchInd>();
        TradeUSSearchInd pRegistro;

        foreach (GridViewRow rFila in gvListaOFAC.Rows)
        {
            Image imageCheck = rFila.FindControl("btnCheck") as Image;
            Image imageEquis = rFila.FindControl("btnEquis") as Image;

            if(imageCheck.Visible)
            {
                pRegistro = new TradeUSSearchInd();
                pRegistro.id = txtIdentificacion.Text;
                pRegistro.source = rFila.Cells[1].Text;
                pRegistro.name = rFila.Cells[2].Text;
                pRegistro.alt_name = rFila.Cells[3].Text;
                pRegistro.address = rFila.Cells[4].Text;
                pRegistro.date_of_birth = rFila.Cells[5].Text;
                pRegistro.nationality = rFila.Cells[6].Text;
                pRegistro.place_of_birth = rFila.Cells[7].Text;
                pRegistro.citizenship = rFila.Cells[8].Text;
                pRegistro.program = rFila.Cells[9].Text;
                pRegistro.coincidencia = true;

                listaOFAC.Add(pRegistro);
            }
            else if(imageEquis.Visible)
            {
                pRegistro = new TradeUSSearchInd();
                pRegistro.id = txtIdentificacion.Text;
                pRegistro.source = "No se encuentra registro en las listas";
                pRegistro.name = txtNombres.Text;
                pRegistro.alt_name = "";
                pRegistro.date_of_birth = "";
                pRegistro.program = "";
                pRegistro.nationality = "";
                pRegistro.address = "";
                pRegistro.citizenship = "";
                pRegistro.place_of_birth = "";
                pRegistro.coincidencia = false;

                listaOFAC.Add(pRegistro);
            }
        }
        return listaOFAC;
    }

    private List<Individual> ObtenerListaONUInd()
    {
        List<Individual> listaONU = new List<Individual>();
        Individual pRegistro;

        foreach (GridViewRow rFila in gvONUIndividual.Rows)
        {
            Image imageCheck = rFila.FindControl("btnCheck") as Image;
            Image imageEquis = rFila.FindControl("btnEquis") as Image;

            if (imageCheck.Visible)
            {
                pRegistro = new Individual();
                pRegistro.dataid = rFila.Cells[0].Text;
                pRegistro.first_name = rFila.Cells[1].Text;
                pRegistro.un_list_type = rFila.Cells[2].Text;
                pRegistro.listed_on = rFila.Cells[3].Text;
                pRegistro.comments1 = rFila.Cells[4].Text;
                pRegistro.designation = rFila.Cells[5].Text;
                pRegistro.nationality = rFila.Cells[6].Text;
                pRegistro.list_type = rFila.Cells[7].Text;
                pRegistro.coincidencia = true;
                
                listaONU.Add(pRegistro);
            }
            else if (imageEquis.Visible)
            {
                pRegistro = new Individual();
                pRegistro.dataid = "";
                pRegistro.first_name = txtNombres.Text;
                pRegistro.un_list_type = "";
                pRegistro.listed_on = "";
                pRegistro.comments1 = rFila.Cells[4].Text;
                pRegistro.designation = "";
                pRegistro.nationality = "";
                pRegistro.list_type = "";
                pRegistro.coincidencia = false;

                listaONU.Add(pRegistro);
            }
        }
        return listaONU;
    }

    private List<Entity> ObtenerListaONUEnt()
    {
        List<Entity> listaONU = new List<Entity>();
        Entity pRegistro;

        foreach (GridViewRow rFila in gvONUEntidad.Rows)
        {
            Image imageCheck = rFila.FindControl("btnCheck") as Image;
            Image imageEquis = rFila.FindControl("btnEquis") as Image;

            if (imageCheck.Visible)
            {
                pRegistro = new Entity();
                pRegistro.dataid = rFila.Cells[0].Text;
                pRegistro.first_name = rFila.Cells[1].Text;
                pRegistro.un_list_type = rFila.Cells[2].Text;
                pRegistro.listed_on = rFila.Cells[3].Text;
                pRegistro.comments1 = rFila.Cells[4].Text;
                pRegistro.country = rFila.Cells[5].Text;
                pRegistro.city = rFila.Cells[6].Text;
                pRegistro.coincidencia = true;

                listaONU.Add(pRegistro);
            }
            else if (imageEquis.Visible)
            {
                pRegistro = new Entity();
                pRegistro.dataid = "";
                pRegistro.first_name = txtNombres.Text;
                pRegistro.un_list_type = "";
                pRegistro.listed_on = "";
                pRegistro.comments1 = rFila.Cells[4].Text;
                pRegistro.country = "";
                pRegistro.city = "";
                pRegistro.coincidencia = false;

                listaONU.Add(pRegistro);
            }
        }
        return listaONU;
    }
}