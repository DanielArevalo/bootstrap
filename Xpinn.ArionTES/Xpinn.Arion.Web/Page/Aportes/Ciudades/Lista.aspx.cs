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
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Services;


partial class Lista : GlobalWeb
{
    CiudadServices CiudadService = new CiudadServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {       
            VisualizarOpciones(CiudadService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CiudadService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarDropdown();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CiudadService.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
            Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarPanel(pConsulta);
        panelGrilla.Visible = false;
        lblTotalRegs.Visible = false;
        lblInfo.Visible = false;
    }


    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    void cargarDropdown()
    {
        ddlTipo.Items.Insert(0, new ListItem("Seleccione un item","0"));
        ddlTipo.Items.Insert(1, new ListItem("País", "1"));
        ddlTipo.Items.Insert(2, new ListItem("Departamento/Estado", "2"));
        ddlTipo.Items.Insert(3, new ListItem("Ciudad", "3"));
        ddlTipo.Items.Insert(4, new ListItem("Municipio", "4"));
        ddlTipo.Items.Insert(5, new ListItem("Zona", "5"));
        ddlTipo.Items.Insert(5, new ListItem("Barrio", "6"));
        ddlTipo.SelectedIndex = 0;
        ddlTipo.DataBind();

        PoblarLista("CIUDADES", ddlDepende);
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


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CiudadService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();
         
        Session[CiudadService.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        Session["ID"] = id;
        ctlMensaje.MostrarMensaje("Desea realizar la eliminación de la cuenta?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            CiudadService.EliminarCiudad(Convert.ToInt64(Session["ID"]), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CiudadService.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }

    
    private void Actualizar()
    {
        try
        {
            List<Ciudad> lstConsulta = new List<Ciudad>();
            String filtro = obtFiltro();
           
            lstConsulta = CiudadService.ListarCiudad(filtro,(Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;           

            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
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
            Session.Add(CiudadService.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CiudadService.CodigoPrograma, "Actualizar", ex);
        }
    }

    
   
    private string obtFiltro()
    {
        Configuracion conf = new Configuracion();

        String filtro = String.Empty;

        if (txtCodigo.Text.Trim() != "")
            filtro += " and Ciudades.Codciudad = " + txtCodigo.Text;
        if (txtNombre.Text.Trim() != "")
            filtro += " and Ciudades.Nomciudad like '%" + txtNombre.Text + "%'";       
        if (ddlTipo.SelectedIndex != 0)
            filtro += " and Ciudades.Tipo = " + ddlTipo.SelectedValue;
        if (ddlDepende.SelectedIndex != 0)
            filtro += " and Ciudades.Depende_De = " + ddlDepende.SelectedValue;
        
        return filtro;
    }


   
}