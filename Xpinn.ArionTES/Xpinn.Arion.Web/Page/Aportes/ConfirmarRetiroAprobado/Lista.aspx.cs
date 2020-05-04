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
using System.Text;
using System.Web.UI.HtmlControls;
using System.IO;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Services;

partial class Lista : GlobalWeb
{
    private Xpinn.Aportes.Services.AporteServices AporteServicio = new Xpinn.Aportes.Services.AporteServices();
    PoblarListas Poblar = new PoblarListas();
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AporteServicio.CodigoProgramaConfirmarRetiroaprobado, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlMensajeBorrar.eventoClick += btnContinuarBorrar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.CodigoProgramaConfirmarRetiroaprobado, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["Usuario"];
            if (!IsPostBack)
            {
                Session["solicitudRetiro"] = null;
                Actualizar();
                CargarDropDown();
                cargarCombos();                            
                if (Session[AporteServicio.ProgramaCruce + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.CodigoProgramaConfirmarRetiroaprobado, "Page_Load", ex);
        }
    }

    private void cargarCombos()
    {
        //Lista de zonas por ejecutivo
        List<Ejecutivo> lstZonas = new List<Ejecutivo>();
        EjecutivoService serviceEjecutivo = new EjecutivoService();
        lstZonas = serviceEjecutivo.ListarZonasDeEjecutivo(_usuario.codusuario, (Usuario)Session["usuario"]);       
    }

    protected void CargarDropDown()
    {
        Poblar.PoblarListaDesplegable("TIPOIDENTIFICACION", "*","","1", ddlTipoIdentificacion, (Usuario)Session["usuario"]);
        Poblar.PoblarListaDesplegable("MOTIVO_RETIRO", "*", "", "1", DdlMotRetiro, (Usuario)Session["usuario"]);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, AporteServicio.ProgramaCruce);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, AporteServicio.ProgramaCruce);        
        txtFechaSolicitud.Text = "";
        txtIdentificacion.Text = "";
        txtNombres.Text = "";
        txtApellidos.Text = "";
        gvLista.DataSource = null;
        panelGrilla.Visible = false;
        CargarDropDown();
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string idSol = gvLista.Rows[gvLista.SelectedRow.RowIndex].Cells[2].Text;
            ViewState.Add("IdRegistroRechazar", idSol);

            ctlMensaje.MostrarMensaje("Seguro desea continuar el proceso para esta solicitud?");
        }
        catch (Exception ex)
        {
            VerError("Ocurrio un problema al intentar continuar con el proceso, " + ex.Message);
        }        
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            VerError("");
            string idSol = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
            ViewState.Add("IdRegistroRechazar", idSol);
            e.NewEditIndex = -1;
            ctlMensajeBorrar.MostrarMensaje("Seguro que desea rechazar esta solicitud?");
        }
        catch (Exception ex)
        {
            VerError("Ocurrio un problema al intentar rechazar el registro, " + ex.Message);
        }
    }    

    //Finaliza eliminación
    void btnContinuarBorrar_Click(object sender, EventArgs e)
    {
        try
        {
            Aporte solicitud = new Aporte();

            solicitud.idretiro = Convert.ToInt32(ViewState["IdRegistroRechazar"]);
            solicitud.estado_modificacion = "2"; // Rechazando Solicitud
            AporteServicio.ModificarEstadoSolicitud(solicitud, (Usuario)Session["usuario"]);

            VerError("Solicitud Rechazada Correctamente!.");
            Actualizar();
        }
        catch (Exception ex)
        {
            VerError("No se ha podido rechazar la solicitud, " + ex.Message);
        }
    }

    //Continua el proceso de aprobación en RetiroAhorros
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            //OBTENER LOS DATOS PARA PASAR AL PROCESO DE RETIRO
            int id_solicitud = Convert.ToInt32(ViewState["IdRegistroRechazar"]);            
            string filtro = string.Empty;
            filtro += " and s.ID_SOL_RETIRO = '" + id_solicitud + "' ";

            List<Aporte> lstSolRetiro = AporteServicio.ListarSolicitudRetiro(filtro, (Usuario)Session["usuario"]);
            Aporte solicitud = lstSolRetiro.ElementAt(0);                              
            Session["solicitudRetiro"] = solicitud;
            Session[AporteServicio.ProgramaCruce + ".id"] = solicitud.cod_persona;
            Session[AporteServicio.ProgramaCruce + ".consulta"] = 0;
            Response.Redirect("../../Aportes/CruceCuentas/Nuevo.aspx", false);
        }
        catch (Exception ex)
        {
            VerError("Error al intentar generar el retiro, " + ex.Message);
        }
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
            BOexcepcion.Throw(AporteServicio.ProgramaCruce, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {      
        try
        {
            List<Xpinn.Aportes.Entities.Aporte> lstConsulta = new List<Xpinn.Aportes.Entities.Aporte>();
            string pFiltro = obtFiltro();
            //Consulta la lista de solicitudes según el filtro            
            lstConsulta = AporteServicio.ListarSolicitudRetiro(pFiltro, (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            Session["DTDETALLE"] = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                panelGrilla.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                lblInfo.Visible = false;
            }
            else
            {
                panelGrilla.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            }
           
            Session.Add(AporteServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.CodigoPrograma, "Actualizar", ex);
        }        
    }

    public string obtFiltro()
    {
        string filtro = string.Empty;

        if(txtIdentificacion.Text != "")
            filtro += " AND p.IDENTIFICACION = '" + txtIdentificacion.Text.Trim() + "'";
        if (ddlTipoIdentificacion.SelectedIndex > 0)
            filtro += " AND p.TIPO_IDENTIFICACION = "+ ddlTipoIdentificacion.SelectedValue;
        if(txtNombres.Text != "")
            filtro += " AND p.NOMBRES LIKE '%" + txtNombres.Text.Trim() + "%'";
        if(txtApellidos.Text != "")
            filtro += " AND p.APELLIDOS LIKE '%" + txtApellidos.Text.Trim() + "%'";
        if (DdlMotRetiro.SelectedIndex > 0)
            filtro += " AND s.COD_MOTIVO = " + DdlMotRetiro.SelectedValue ;
        if (txtFechaSolicitud.Text.Trim() != "")        
            filtro += " and s.FECHA_SOLICITUD = To_Date('" + txtFechaSolicitud.Text.Trim() + "', 'dd/MM/yyyy') ";

        filtro += " and s.ESTADO = '4' ";        
        return filtro;
    }
}