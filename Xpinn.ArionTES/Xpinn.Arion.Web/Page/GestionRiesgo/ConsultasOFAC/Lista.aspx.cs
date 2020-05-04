using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Contabilidad.Services;
using Xpinn.Interfaces.Entities;
using Xpinn.Interfaces.Services;
using Xpinn.Riesgo.Services;
using Xpinn.Util;
using System.Linq;
using Xpinn.Riesgo.Entities;
using System.Threading;
using System.IO;
using System.Web.Script.Serialization;
using System.Web;
using System.Threading.Tasks;

public partial class Lista : GlobalWeb
{
    private HistoricoSegmentacionService _historicoService = new HistoricoSegmentacionService();
    private TradeUSServices _tradeService = new TradeUSServices();
    private Thread tareaEjecucion;
    public static int result;
    private List<TradeUSSearchEntity> listaOFAQ = new List<TradeUSSearchEntity>();
    private Individuals listaONU = new Individuals();
    private int tipoRol = 1;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_historicoService.CodigoPrograma4, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
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
                CargarListas();
                //Actualizar(0);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_historicoService.CodigoPrograma4, "Page_Load", ex);
        }
    }

    void CargarListas()
    {
        try
        {
            cbUltima.Checked = true;
            /*// Llenar las listas que tienen que ver con ciudades
            ddlCiudad.DataTextField = "ListaDescripcion";
            ddlCiudad.DataValueField = "ListaIdStr";
            ddlCiudad.DataSource = TraerResultadosLista("Ciudades");
            ddlCiudad.DataBind();
            */
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_historicoService.CodigoPrograma4, "CargarListas", ex);
        }
    }
    
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        lblSalida.Visible = false;
        VerError("");
        Actualizar(0);
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session[_historicoService.CodigoPrograma + ".id"] = null;
        VerError("");
        Navegar(Pagina.Nuevo);
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        LimpiarPanel(pBusqueda);
        txtFecha.Text = "";
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar(0);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_historicoService.CodigoPrograma4, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SarlaftAlerta dataItem = e.Row.DataItem as SarlaftAlerta;
                
                Image imageCheckF = e.Row.FindControl("btnCheckF") as Image;
                Image imageEquisF = e.Row.FindControl("btnEquisF") as Image;
                imageCheckF.Visible = dataItem.coincidencia;
                imageEquisF.Visible = !dataItem.coincidencia;
                
                Image imageCheckO = e.Row.FindControl("btnCheckO") as Image;
                Image imageEquisO = e.Row.FindControl("btnEquisO") as Image;
                imageCheckO.Visible = dataItem.coincidencia2;
                imageEquisO.Visible = !dataItem.coincidencia2;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_historicoService.CodigoPrograma4, "gvLista_RowDataBound", ex);
        }
    }

    private List<Xpinn.FabricaCreditos.Entities.Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
        return lstDatosSolicitud;
    }

    private string ObtenerFiltro()
    {
        string filtro = "";
        if(txtNumeIdentificacion.Text != "")
            filtro += " AND P.IDENTIFICACION LIKE '%" + txtNumeIdentificacion.Text + "%'";
        Configuracion conf = new Configuracion();
        if (ddlTipoPersona.SelectedValue != "")
            filtro += " AND P.TIPO_PERSONA = UPPER('" + ddlTipoPersona.SelectedItem.Text+"')";
        if (txtCodigo.Text != "")
            filtro += " AND P.COD_PERSONA = " + txtCodigo.Text; 
        if(txtFecha.Text != "")
            filtro += " AND TRUNC(C.FECHA_CONSULTA) >= TO_DATE('" + txtFecha.Text + "', 'dd/MM/yyyy')";
        if (txtFechaFinal.Text != "")
            filtro += " AND TRUNC(C.FECHA_CONSULTA) <= TO_DATE('" + txtFechaFinal.Text + "', 'dd/MM/yyyy')";
        if (txtNombres.Text != "")
            filtro += " AND P.NOMBRE LIKE '%" + txtNombres.Text + "%' ";
        if (txtApellidos.Text != "")
            filtro += " AND P.NOMBRE LIKE '%" + txtApellidos.Text + "%' ";
        if (ddlTipoRol.SelectedValue != "")
            if (ddlTipoRol.SelectedValue == "T")
                filtro += " AND P.IDAFILIACION IS NULL AND P.ESTADO = '" + ddlTipoRol.SelectedValue + "' ";
            else
                filtro += " AND P.IDAFILIACION IS NOT NULL AND P.ESTADO = '" + ddlTipoRol.SelectedValue + "' ";
        return filtro;
    }

    public void Actualizar(int filtro = 0)
    {
        pnlFinal.Visible = false;
        VerError("");
        string Filtro = "";
        Filtro = ObtenerFiltro();
        SarlaftAlertaServices _sarlaftServicio = new SarlaftAlertaServices();
        List<SarlaftAlerta> lstPersonas = new List<SarlaftAlerta>();
        lstPersonas = _sarlaftServicio.ListarPersonasConsultadas(Filtro, cbUltima.Checked, (Usuario)Session["usuario"]);

        //Filtar por resultados positivos
        if (lstPersonas.Count > 0)
        {
            if (checkOFAC.Checked)
            {
                lstPersonas = (from p in lstPersonas
                              where (p.coincidencia == true)
                              select p).ToList();
            }
            if (checkONU.Checked)
            {
                lstPersonas = (from p in lstPersonas
                               where (p.coincidencia2 == true)
                               select p).ToList();
            }
            // Agrupar por listas
            if (checkOFAC.Checked && checkONU.Checked)
            {
                lstPersonas = (from p in lstPersonas
                              where (p.coincidencia == true || p.coincidencia2 == true)
                              select p).ToList();
            }
        }


        if (lstPersonas.Count > 0)
        { 
            gvLista.Visible = true;
            lblTotalRegs.Visible = true;
            lblTotalRegs.Text = "<br/> Registros encontrados " + lstPersonas.Count.ToString();
            gvLista.DataSource = lstPersonas;
            gvLista.DataBind();
            lblInfo.Visible = false;
            Session["DTPERSONAS"] = lstPersonas;
        }
        else
        {
            gvLista.Visible = false;
            lblTotalRegs.Visible = false;
            lblInfo.Visible = true;
            Session["DTPERSONAS"] = null;
        }
    }



    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id;
        id = gvLista.SelectedRow.Cells[4].Text;
        Session[_historicoService.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }


    #region Consulta Masiva

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        VerError("");
        lblError.Text = "";
        IniciarProceso();
        EjecutaProceso();
        ctlMensaje.EnableViewState = false;
        ctlMensaje.Dispose();       
    }

    protected void btnConsultaMasiva_Click(object sender, EventArgs e)
    {
        tipoRol = 1;
        VerError("");
        ctlMensaje.MostrarMensaje("Desea Generar la Consulta Masiva para Asociados?");
    }

    protected void btnConsultaMasivaOtros_Click(object sender, EventArgs e)
    {
        tipoRol = 2;
        VerError("");
        ctlMensaje.MostrarMensaje("Desea Generar la Consulta Masiva Terceros?");
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        if (Session["Proceso"] != null)
            if (Session["Proceso"].ToString() == "FINAL")
                TerminarProceso();
            else
                mpeProcesando.Show();
        else
            mpeProcesando.Hide();
    }

    public void IniciarProceso()
    {
        mpeProcesando.Show();
        Image1.Visible = true;
        Session["Proceso"] = "INICIO";
        Timer1.Enabled = true;
    }

    public void EjecutaProceso()
    {
        try
        {
            // Traer la lista OFAQ
            listaOFAQ = _tradeService.ListaConsolidadOFAQ();
            listaONU = _tradeService.ListaConsolidadaCSONU();
            
            // Traer el listado de personas a consultar
            DateTime tod = DateTime.Now;
            string Filtro = "";
            if (tipoRol == 1)
                Filtro = " And p.identificacion not like '%-%' And p.idafiliacion Is Not Null And p.estado != 'R' ";
            else
                Filtro = " And p.identificacion not like '%-%' And p.idafiliacion Is Null ";

            SarlaftAlertaServices _sarlaftServicio = new SarlaftAlertaServices();
            List<SarlaftAlerta> lstPersonas = new List<SarlaftAlerta>();
            int contador = 0;
            lstPersonas = _sarlaftServicio.ListarPersonasParaConsultar(Filtro, (Usuario)Session["usuario"]);

            // Realizar la consulta por cada persona
            foreach (SarlaftAlerta item in lstPersonas)
            {
                contador++;
                string resultado = "";
                List<TradeUSSearchInd> lstOFAC = new List<TradeUSSearchInd>();
                List<Individual> lstONUInd = new List<Individual>();
                List<Entity> lstONUEnt = new List<Entity>();
                ConsultaOFAC(item.identificacion, item.nombre, ref resultado, ref lstOFAC);
                ConsultaONU(item.descripcion, item.identificacion, item.nombre, ref resultado, ref lstONUInd, ref lstONUEnt);

                SarlaftAlertaServices salarftServicio = new SarlaftAlertaServices();
                salarftServicio.CrearRegistroConsultaLista(lstOFAC, lstONUInd, lstONUEnt, Convert.ToInt64(item.cod_persona), true, Convert.ToBoolean(null), Convert.ToBoolean(null), Convert.ToBoolean(null), Convert.ToBoolean(null), Convert.ToBoolean(null), (Usuario)Session["usuario"]);
            }

            DateTime hora = DateTime.Now;
        }
        catch (Exception ex)
        {
            Session["Error"] = ex.Message;
        }
        Session["Proceso"] = "FINAL";
    }


    public void TerminarProceso()
    {
        mpeProcesando.Hide();
        Image1.Visible = false;
        Session.Remove("Proceso");
        Timer1.Enabled = false;
        if (Session["Error"] != null)
        {
            if (Session["Error"].ToString().Trim() != "")
                lblError.Text = Session["Error"].ToString();
            Session.Remove("Error");
        }
        //Forzar el postback para que actualize los links creados

    }

    private  bool ConsultaOFAC(string pIdentificacion, string pNombres, ref string pResultado, ref List<TradeUSSearchInd> lstOFAC)
    {
        try
        {
            InterfazRIESGO intRiesgo = new InterfazRIESGO();
            TradeUSSearchResults tradeResult;
            lstOFAC = new List<TradeUSSearchInd>();
            TradeUSSearchInd pObjeto;

            tradeResult = new TradeUSSearchResults();
            tradeResult.search_performed_at = "";
            tradeResult.results = (from i in listaOFAQ where i.name == pNombres select i).ToList();
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
                    pObjeto.id_persona = pIdentificacion;
                    pObjeto.nombre_persona = pNombres;
                    lstOFAC.Add(pObjeto);
                }
            }
            else
            {
                pObjeto = new TradeUSSearchInd();
                pObjeto.id = pIdentificacion;
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
                pObjeto.id_persona = pIdentificacion;
                pObjeto.nombre_persona = pNombres;
                lstOFAC.Add(pObjeto);
            }

            if (lstOFAC.Count > 0)
                pResultado = "Registros encontrados: " + lstOFAC.Count();
            else
                pResultado = "La persona no tiene registro en listas OFAC";
            return true;
        }
        catch (Exception ex)
        {
            VerError("Error al generar la consulta de lista OFAC " + ex.Message);
            return false;
        }
    }

    private bool ConsultaONU(string pTipoPersona, string pIdentificacion, string pNombres, ref string pResultado, ref List<Individual> lstIndividual, ref List<Entity> lstEntidad)
    {
        try
        {
            InterfazRIESGO intRiesgo = new InterfazRIESGO();
            Individual PersonaIndividual = new Individual();
            lstIndividual = new List<Individual>();
            Entity PersonaEntidad = new Entity();
            lstEntidad = new List<Entity>();

            string URL = Request.Url.ToString();
            string tipo_persona = pTipoPersona == "NATURAL" ? "N" : pTipoPersona == "JURIDICA" ? "J" : "";
            string nombre = "";
            //intRiesgo.ConsultarPersonaCSONU(pIdentificacion, pNombres, tipo_persona, ref PersonaIndividual, ref PersonaEntidad);
            try
            {
                PersonaIndividual = (from item in listaONU.individual
                                     let nombres = item.first_name.Trim() + " " + item.second_name.Trim() + " " + item.third_name.Trim()
                                     where (nombres.Trim() == pNombres.Trim()) && item != null
                                     select item).FirstOrDefault();
            }
            catch { }

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
                PersonaIndividual.nombre_persona = pNombres;
                PersonaIndividual.id_persona = pIdentificacion;

                lstIndividual.Add(PersonaIndividual);
                pResultado = "Registros encontrados: " + lstIndividual.Count();
            }
            else if (tipo_persona == "N" && PersonaIndividual.first_name == null || PersonaIndividual.second_name == null || PersonaIndividual.third_name == null)
            {
                PersonaIndividual.dataid = pIdentificacion;
                PersonaIndividual.first_name = txtNombres.Text;
                PersonaIndividual.comments1 = "No se encuentra registro en las listas";
                PersonaIndividual.coincidencia = false;
                PersonaIndividual.nombre_persona = pNombres;
                PersonaIndividual.id_persona = pIdentificacion;

                lstIndividual.Add(PersonaIndividual);
                pResultado = "La persona no tiene registro en listas CS/NU";
            }
            else if (tipo_persona == "J" && PersonaEntidad != null)
            {
                PersonaEntidad.coincidencia = true;
                PersonaEntidad.nombre = pNombres;
                PersonaEntidad.identificacion = pIdentificacion;
                lstEntidad.Add(PersonaEntidad);
                pResultado = "Registros encontrados: " + lstEntidad.Count();
            }
            else if (tipo_persona == "J" && PersonaEntidad.dataid == null)
            {
                PersonaEntidad.first_name = txtNombres.Text;
                PersonaEntidad.comments1 = "No se encuentra registro en las listas";
                PersonaEntidad.un_list_type = DateTime.Now.ToShortDateString();
                PersonaEntidad.coincidencia = false;
                PersonaEntidad.nombre = pNombres;
                PersonaEntidad.identificacion = pIdentificacion;

                lstEntidad.Add(PersonaEntidad);
                pResultado = "La persona no tiene registro en listas CS/NU";
            }
            else
            {
                pResultado = "La persona no tiene registro en listas CS/NU";
            }
            return true;
        }
        catch (Exception ex)
        {
            VerError("Error al generar la consulta de lista CS/NU " + ex.Message);
            return false;
        }
    }

    #endregion    
}