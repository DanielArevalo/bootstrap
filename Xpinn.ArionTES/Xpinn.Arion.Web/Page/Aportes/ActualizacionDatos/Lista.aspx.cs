using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using System.IO;
partial class Lista : GlobalWeb
{
    Persona1Service PersonaService = new Persona1Service();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {       
            VisualizarOpciones(PersonaService.CodigoProgramaActualizacionDatos, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoImportar += btnImportar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.MostrarConsultar(true);
            toolBar.MostrarGuardar(false);
            panelGrilla.ScrollBars = ScrollBars.Vertical;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PersonaService.CodigoProgramaActualizacionDatos, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarDropdown();
                CargaFormatosFecha();
                txtfecha.ToDateTime = DateTime.Now;
                panelGrilla.Visible = false;
                Session["lstPersona"] = null;
                mvPrincipal.ActiveViewIndex = 0;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PersonaService.CodigoProgramaActualizacionDatos, "Page_Load", ex);
        }
    }

    
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtCodIni.Text = "";
        txtCodFin.Text = "";
        ddlEmpresa.SelectedIndex = 0;
        ddlAsesor.SelectedIndex = 0;
        ddlAsesorNuevo.SelectedIndex = 0;
        ddlAsesorNuevo.Enabled = false;
        ddlZona.SelectedIndex = 0;
        ddlZonaNuevo.SelectedIndex = 0;
        ddlZonaNuevo.Enabled = false;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        panelGrilla.Visible = false;
        gvLista.DataSource = null;
        lblInfo.Visible = false;
        lblTotalRegs.Visible = false;
        LimpiarDataImportacion();
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {

        Page.Validate();
        if (Page.IsValid)
        {
            Actualizar();
        }
    }

    
    void cargarDropdown()
    {
        String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();


        ListaSolicitada = "Empresa";
        lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
        ddlEmpresa.DataSource = lstDatosSolicitud;
        ddlEmpresa.DataTextField = "ListaDescripcion";
        ddlEmpresa.DataValueField = "ListaId";
        ddlEmpresa.DataBind();
        ddlEmpresa.Items.Insert(0, new ListItem("Seleccione un item"));

        ListaSolicitada = "Asesor";
        lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
        ddlAsesor.DataSource = lstDatosSolicitud;
        ddlAsesor.DataTextField = "ListaDescripcion";
        ddlAsesor.DataValueField = "ListaId";
        ddlAsesor.DataBind();
        ddlAsesor.Items.Insert(0, new ListItem("Seleccione un item"));

        ddlAsesorNuevo.DataSource = lstDatosSolicitud;
        ddlAsesorNuevo.DataTextField = "ListaDescripcion";
        ddlAsesorNuevo.DataValueField = "ListaId";
        ddlAsesorNuevo.DataBind();
        ddlAsesorNuevo.Items.Insert(0, new ListItem("Seleccione un item"));



        ListaSolicitada = "Zona";
        lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
        ddlZona.DataSource = lstDatosSolicitud;
        ddlZona.DataTextField = "ListaDescripcion";
        ddlZona.DataValueField = "ListaId";
        ddlZona.DataBind();
        ddlZona.Items.Insert(0, new ListItem("Seleccione un item"));


        ddlZonaNuevo.DataSource = lstDatosSolicitud;
        ddlZonaNuevo.DataTextField = "ListaDescripcion";
        ddlZonaNuevo.DataValueField = "ListaId";
        ddlZonaNuevo.DataBind();
        ddlZonaNuevo.Items.Insert(0, new ListItem("Seleccione un item"));

    }


    private void Actualizar()
    {
        try
        {
            VerError("");
            if (txtCodIni.Text=="" && txtCodFin.Text == "" && ddlEmpresa.SelectedIndex==0 && ddlAsesor.SelectedIndex == 0 && ddlZona.SelectedIndex == 0)
            {
                VerError("Debe utilizar cualquier filtro para realizar la consulta");
                return;
            }

            List<Persona1> lstConsulta = new List<Persona1>();

            lstConsulta = PersonaService.ListarPersonasAporte(txtCodIni.Text.Trim(), txtCodFin.Text.Trim(),ddlEmpresa.SelectedValue,ddlAsesor.SelectedValue,ddlZona.SelectedValue, (Usuario)Session["Usuario"]);
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            Site toolBar = (Site)this.Master;
            if (lstConsulta.Count > 0)
            {
                toolBar.MostrarGuardar(true);
                ddlZonaNuevo.Enabled = true;
                ddlAsesorNuevo.Enabled = true;
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;                
                lblTotalRegs.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();                
               
            }
            else
            {
                toolBar.MostrarGuardar(false);
                ddlZonaNuevo.Enabled = false;
                ddlAsesorNuevo.Enabled = false;
                panelGrilla.Visible = false;
                gvLista.DataSource = null;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }

            Session.Add(PersonaService.CodigoProgramaActualizacionDatos + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PersonaService.CodigoProgramaActualizacionDatos, "Actualizar", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (Session["lstPersona"]==null)
        {
            int cant = (from GridViewRow n in gvLista.Rows
                        where ((CheckBox)n.FindControl("chkpersona")).Checked == true
                        select n).Count();
            if (cant > 0)
                ctlMensaje.MostrarMensaje("Desea realizar la grabación de los datos");
            else
            {
                VerError("seleccione la(s) persona(s) para realizar los cambios");
                return;
            }
        }
        else
        {
            ctlMensaje.MostrarMensaje("Desea realizar la grabación de los datos");
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        List<Persona1> listaPersona = new List<Persona1>();
        if (Session["lstPersona"] == null)
        {
           
            listaPersona = (from GridViewRow x in gvLista.Rows
                            where ((CheckBox)x.FindControl("chkpersona")).Checked == true
                            select new Persona1
                            {
                                cod_persona = Convert.ToInt64(x.Cells[1].Text),
                                cod_asesor = ((Label)x.FindControl("lblAsesor")).Text != "" ? Convert.ToInt64(((Label)x.FindControl("lblAsesor")).Text) : 0,
                                zona = (((Label)x.FindControl("lblZona")).Text != "" ? Convert.ToInt64(((Label)x.FindControl("lblZona")).Text) : 0)
                            }).ToList();
            VerError("No se pudo actualizar la cuota del aporte ya que tiene el porcentaje en 0");
        }
        else
        {
            listaPersona = (List<Persona1>)Session["lstPersona"];
        }
        listaPersona = PersonaService.ModificarPersonasAporte(listaPersona, (Usuario)Session["Usuario"]);
        Navegar(Pagina.Lista);
    }

    private List<Xpinn.FabricaCreditos.Entities.Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = persona1Servicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
        return lstDatosSolicitud;
    }


    protected void ddlAsesorNuevo_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow n in gvLista.Rows)
        {
            if (ddlAsesorNuevo.SelectedIndex != 0)
                ((Label)n.FindControl("lblAsesor")).Text = ddlAsesorNuevo.SelectedValue;
            else
                ((Label)n.FindControl("lblAsesor")).Text ="";
        }
    }

    protected void ddlZonaNuevo_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        foreach (GridViewRow n in gvLista.Rows)
        {
            if (ddlZonaNuevo.SelectedIndex != 0)
                ((Label)n.FindControl("lblZona")).Text = ddlZonaNuevo.SelectedValue;
            else
                ((Label)n.FindControl("lblZona")).Text = "";
        }
    }

    protected void btnImportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        mvPrincipal.ActiveViewIndex = 1;
        Site toolBar = (Site)Master;
        toolBar.MostrarLimpiar(true);
        toolBar.MostrarImportar(false);
        toolBar.MostrarConsultar(false);
        LimpiarDataImportacion();
    }

    void LimpiarDataImportacion()
    {
        pErrores.Visible = false;
        panelPersonas.Visible = false;
        gvpersonas.DataSource = null;
        gvpersonas.DataBind();
        gvErrores.DataSource = null;
        gvErrores.DataBind();
        panelPersonas.Visible = false;
        ucFecha.Text = DateTime.Now.ToShortDateString();
        ddlFormatoFecha.SelectedIndex = 0;
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(false);
    }
    protected void btnCargarPersonas_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            string error = "";
            if (ddlFormatoFecha.SelectedIndex == 0)
            {
                VerError("Seleccione el tipo de fecha que se carga en el archivo.");
                ddlFormatoFecha.Focus();
                return;
            }
            if (ucFecha.Text == "")
            {
                VerError("Ingrese la fecha de carga");
                ucFecha.Focus();
                return;
            }
            if (fupArchivoPersona.HasFile)
            {
                Stream stream = fupArchivoPersona.FileContent;
                List<Xpinn.FabricaCreditos.Entities.Persona1> lstPersonas = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
                List<Xpinn.FabricaCreditos.Entities.ErroresCarga> plstErrores = new List<Xpinn.FabricaCreditos.Entities.ErroresCarga>();

                PersonaService.CargarPersonasDatos(ucFecha.ToDateTime, ddlFormatoFecha.SelectedValue, stream, ref error, ref lstPersonas, ref plstErrores, (Usuario)Session["usuario"]);

                if (error.Trim() != "")
                {
                    VerError(error);
                    return;
                }
                if (plstErrores.Count() > 0)
                {
                    pErrores.Visible = true;
                    gvErrores.DataSource = plstErrores;
                    gvErrores.DataBind();
                    cpeDemo1.CollapsedText = "(Click Aqui para ver " + plstErrores.Count() + " errores...)";
                    cpeDemo1.ExpandedText = "(Click Aqui para ocultar listado de errores...)";
                }
                panelPersonas.Visible = false;


                if (lstPersonas.Count > 0)
                {
                    Session["lstPersona"] = lstPersonas;
                    panelPersonas.Visible = true;
                    //CARGAR DATOS A GRILLA DE NATURALES
                    gvpersonas.DataSource = lstPersonas;
                    gvpersonas.DataBind();
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarGuardar(true);
                }
            }
            else
            {
                VerError("Seleccione el archivo a cargar, verifique los datos.");
                return;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PersonaService.CodigoProgramaActualizacionDatos, "btnCargarPersonas_Click", ex);
        }
    }
    void CargaFormatosFecha()
    {
        ddlFormatoFecha.Items.Insert(0, new ListItem("Seleccione un Item", "0"));
        ddlFormatoFecha.Items.Insert(1, new ListItem("dd/MM/yyyy", "dd/MM/yyyy"));
        ddlFormatoFecha.Items.Insert(2, new ListItem("yyyy/MM/dd", "yyyy/MM/dd"));
        ddlFormatoFecha.Items.Insert(3, new ListItem("MM/dd/yyyy", "MM/dd/yyyy"));
        ddlFormatoFecha.Items.Insert(4, new ListItem("ddMMyyyy", "ddMMyyyy"));
        ddlFormatoFecha.Items.Insert(5, new ListItem("yyyyMMdd", "yyyyMMdd"));
        ddlFormatoFecha.Items.Insert(6, new ListItem("MMddyyyy", "MMddyyyy"));
        ddlFormatoFecha.SelectedIndex = 0;
        ddlFormatoFecha.DataBind();
    }
}