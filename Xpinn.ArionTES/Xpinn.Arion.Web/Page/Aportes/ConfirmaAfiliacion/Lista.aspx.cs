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
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;

partial class Lista : GlobalWeb
{

    AfiliacionServices AfiliacionServicio = new AfiliacionServices();
    Xpinn.FabricaCreditos.Services.Persona1Service BOPersona = new Xpinn.FabricaCreditos.Services.Persona1Service();
    AfiliacionServices BOActualizar = new AfiliacionServices();
    PoblarListas poblarLista = new PoblarListas();
    ParametrizacionProcesoAfilicacionService _paramProceso = new ParametrizacionProcesoAfilicacionService();
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {

            VisualizarOpciones(AfiliacionServicio.codigoprogramaConfirmarAfili, "L");
            
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramaConfirmarAfili, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["Usuario"];
            if (!Page.IsPostBack)
            {
                Session["ID"] = null;
                mvPrincipal.ActiveViewIndex = 0;
                CargarDropDown();
                LimpiarData();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramaConfirmarAfili, "Page_Load", ex);
        }
    }

    protected void LimpiarData()
    {
        VerError("");
        pDatos.Visible = false;
        lblTotalRegs.Visible = false;
        lblInfo.Visible = false;
        chkSinAsesor.Checked = false;
        gvLista.DataSource = null;        
        gvLista.DataBind();
        LimpiarValoresConsulta(pConsulta, AfiliacionServicio.codigoprogramaConfirmarAfili);
        Site toolBar = (Site)this.Master;
        toolBar.MostrarLimpiar(true);
        mvPrincipal.ActiveViewIndex = 0;
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (mvPrincipal.ActiveViewIndex == 0)
        {
            Actualizar();
        }
        else
        {
            LimpiarData();
        }        
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarData();
    }

    protected void CargarDropDown()
    {
        poblarLista.PoblarListaDesplegable("CIUDADES", "CODCIUDAD,NOMCIUDAD", "tipo = 3", "2", ddlCiudad, (Usuario)Session["usuario"]);
    }


    private void Actualizar()
    {
        try
        {
            List<SolicitudPersonaAfi> lstConsulta = new List<SolicitudPersonaAfi>();
            String filtro = obtFiltro();
            lstConsulta = AfiliacionServicio.ListarDataSolicitudAfiliacion(filtro, (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            Site toolBar = (Site)this.Master;
            
            if (lstConsulta.Count > 0)
            {
                pDatos.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
            }
            else
            {
                pDatos.Visible = false;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramaConfirmarAfili, "Actualizar", ex);
        }
    }


    private string obtFiltro()
    {
        ConnectionDataBase conexion = new ConnectionDataBase();
        Configuracion conf = new Configuracion();

        String filtro = String.Empty;
        
        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and S.identificacion = '" + txtIdentificacion.Text + "'";
        if (txtNombres.Text.Trim() != "")
            filtro += " and TRIM(S.PRIMER_NOMBRE) || ' ' || TRIM(S.SEGUNDO_NOMBRE) like '%" + txtNombres.Text.Trim() + "%'";
        if (txtApellidos.Text.Trim() != "")
            filtro += " and TRIM(S.PRIMER_APELLIDO) || ' ' || TRIM(S.SEGUNDO_APELLIDO) like '%" + txtApellidos.Text.Trim() + "%'";
        if (ddlCiudad.SelectedItem != null && ddlCiudad.SelectedIndex > 0)
            filtro += " and S.CIUDAD = " + ddlCiudad.SelectedValue;

        UsuarioAtribuciones atrusuarios = new UsuarioAtribuciones();
        UsuarioAtribucionesService atribuciones = new UsuarioAtribucionesService();
        atrusuarios.codusuario = _usuario.codusuario;
        atrusuarios.tipoatribucion = 1;
        atrusuarios.activo = 1;
        List<UsuarioAtribuciones> atrusuario = atribuciones.ListarUsuarioAtribuciones(atrusuarios, _usuario);
        if (atrusuarios != null && atrusuario.Count > 0)
        {

        }
        else if (chkSinAsesor.Checked)
        {
            filtro += " and (S.Cod_Asesor = " + _usuario.codusuario + " or S.Cod_Asesor is null)";
        }
        else
        {
            filtro += " and S.Cod_Asesor = " + _usuario.codusuario + " ";
        }

        filtro += " and to_char(S.estado) In ('0', 'S') ";

        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = " where " + filtro;
        }
        return filtro;
    }
    
    protected List<SolicitudPersonaAfi> ObtenerListaAfiliaciones()
    {
        List<SolicitudPersonaAfi> lstResultado = new List<SolicitudPersonaAfi>();
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
            if (cbSeleccionar != null)
            {
                if (cbSeleccionar.Checked)
                {
                    SolicitudPersonaAfi pEntidad = new SolicitudPersonaAfi();
                    Int64 pId = Convert.ToInt64(gvLista.DataKeys[rFila.RowIndex].Values[0].ToString());
                    pEntidad.id_persona = pId;
                    pEntidad.identificacion = Convert.ToString(gvLista.DataKeys[rFila.RowIndex].Values[1].ToString());
                    lstResultado.Add(pEntidad);
                }
            }
        }
        return lstResultado;
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
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramaConfirmarAfili, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void cbSeleccionarEncabezado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbSeleccionarEncabezado = (CheckBox)sender;
        if (cbSeleccionarEncabezado != null)
        {
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
                cbSeleccionar.Checked = cbSeleccionarEncabezado.Checked;
            }
        }
    }
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        Int64 idELiminar = 0;
        if (Session["ID"] != null)
        {
            idELiminar = Convert.ToInt64(Session["ID"].ToString());
            Session.Remove("ID");
            //llamar metodo de eliminación
            BOActualizar.EliminarSolicitudAfiliacion(idELiminar, (Usuario)Session["usuario"]);
            Actualizar();
        }
    }
    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string id = gvLista.DataKeys[e.RowIndex].Values[0].ToString();
            Session["ID"] = id; 
            ctlMensaje.MostrarMensaje("Seguro que desea eliminar este registro?");
        }
        catch
        {
            VerError("no puede borar el escalafon por que ya hay personas con este escalafon salarial");
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();
        Session[AfiliacionServicio.codigoprogramaConfirmarAfili + ".id"] = id;
        Navegar(Pagina.Detalle);        
    }
}