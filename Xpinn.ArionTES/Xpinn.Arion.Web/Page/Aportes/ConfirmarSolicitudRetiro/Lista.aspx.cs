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
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;

partial class Lista : GlobalWeb
{
    private Xpinn.Aportes.Services.AporteServices AporteServicio = new Xpinn.Aportes.Services.AporteServices();
    PoblarListas Poblar = new PoblarListas();
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AporteServicio.CodigoProgramaConfirmarRetiroAsociado, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlMensajeBorrar.eventoClick += btnContinuarBorrar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.CodigoProgramaConfirmarRetiroAsociado, "Page_PreInit", ex);
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
            BOexcepcion.Throw(AporteServicio.CodigoProgramaConfirmarRetiroAsociado, "Page_Load", ex);
        }
    }

    private void cargarCombos()
    {
        List<Ejecutivo> lstZonas = new List<Ejecutivo>();
        EjecutivoService serviceEjecutivo = new EjecutivoService();

        UsuarioAtribuciones atrusuarios = new UsuarioAtribuciones();
        UsuarioAtribucionesService atribuciones = new UsuarioAtribucionesService();
        atrusuarios.codusuario = _usuario.codusuario;
        atrusuarios.tipoatribucion = 1;
        atrusuarios.activo = 1;
        List<UsuarioAtribuciones> atrusuario = atribuciones.ListarUsuarioAtribuciones(atrusuarios, _usuario);
        if (atrusuarios != null && atrusuario.Count > 0)
        {
            //Lista de zonas en total
            lstZonas = serviceEjecutivo.ListarZonasDeEjecutivo(0, (Usuario)Session["usuario"]);
            List<Ejecutivo> LstEjecutivos = serviceEjecutivo.ListarEjecutivo((Usuario)Session["usuario"]);
            pnlAse.Visible = true;
            if (LstEjecutivos != null && LstEjecutivos.Count > 0)
            {
                ddlAsesores.DataSource = LstEjecutivos;
                ddlAsesores.DataValueField = "Codigo";
                ddlAsesores.DataTextField = "PrimerNombre";
                ddlAsesores.DataBind();
                ddlAsesores.Items.Insert(0, new ListItem("Seleccione", ""));
                ddlAsesores.SelectedValue = "";
            }
        }
        else
        {
            //Lista de zonas por ejecutivo
            lstZonas = serviceEjecutivo.ListarZonasDeEjecutivo(_usuario.codusuario, (Usuario)Session["usuario"]);
        }
        if (lstZonas.Count > 0)
        {
            ddlZona.DataSource = lstZonas;
            ddlZona.DataValueField = "icodciudad";
            ddlZona.DataTextField = "nomciudad";
            ddlZona.DataBind();
        }
        ddlZona.Items.Insert(0, new ListItem("Seleccione", ""));
        ddlZona.SelectedValue = "";
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
        ddlEstado.SelectedValue = "3";
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
            Aporte solicitud = new Aporte();
            //OBTENER LOS DATOS PARA PASAR AL PROCESO DE RETIRO          
            solicitud.idretiro = Convert.ToInt32(ViewState["IdRegistroRechazar"]);
            solicitud.estado_modificacion = "4"; // dejando solicitud en estado de aprobación para el siguiente paso 
            AporteServicio.ModificarEstadoSolicitud(solicitud, (Usuario)Session["usuario"]);

            VerError("Solicitud aprobada Correctamente!.");
            Actualizar();
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
            Session[Usuario.codusuario + "DTSolicitud"] = lstConsulta;
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
        if (ddlEstado.SelectedValue != "3")
            filtro += " and s.ESTADO = '" + ddlEstado.SelectedValue + "'";


        UsuarioAtribuciones atrusuarios = new UsuarioAtribuciones();
        UsuarioAtribucionesService atribuciones = new UsuarioAtribucionesService();
        atrusuarios.codusuario = _usuario.codusuario;
        atrusuarios.tipoatribucion = 1;
        atrusuarios.activo = 1;
        List<UsuarioAtribuciones> atrusuario = atribuciones.ListarUsuarioAtribuciones(atrusuarios, _usuario);
        if (atrusuarios != null && atrusuario.Count > 0)
        {
            if (ddlAsesores.SelectedValue != "")
            {
                if (chkSinAsesor.Checked)
                    filtro += " and (p.Cod_Asesor = " + ddlAsesores.SelectedValue + " or p.Cod_Asesor is null or p.Cod_Asesor = '' or p.Cod_asesor not in (select icodigo from Asejecutivos where Iestado = 1))";
                else
                    filtro += " and p.Cod_Asesor = " + ddlAsesores.SelectedValue + " ";
            }
        }
        else if (chkSinAsesor.Checked)
            filtro += " and (p.Cod_Asesor = " + _usuario.codusuario + " or p.Cod_Asesor is null or p.Cod_Asesor = '' or p.Cod_asesor not in (select icodigo from Asejecutivos where Iestado = 1))";
        else
            filtro += " and p.Cod_Asesor = " + _usuario.codusuario + " ";

        if (ddlZona.SelectedValue != "")
        {
            if (ddlZona.SelectedValue == "0")
                filtro += " and p.cod_zona is null";
            else
                filtro += " and p.cod_zona = " + ddlZona.SelectedValue + " ";
        }

        return filtro;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.Columns[0].Visible = false;
            gvLista.Columns[1].Visible = false;
            gvLista.Columns[2].Visible = false;
            gvLista.AllowPaging = false;
            gvLista.DataSource = Session[Usuario.codusuario + "DTSolicitud"];
            gvLista.DataBind();
            gvLista.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvLista);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=ListaPersonas.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
        {
            VerError("No existen datos, genere la consulta");
        }
    }
}