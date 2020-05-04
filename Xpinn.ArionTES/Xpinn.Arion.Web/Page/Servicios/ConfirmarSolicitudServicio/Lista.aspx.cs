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
using Xpinn.Servicios.Services;
using Xpinn.Servicios.Entities;
using Xpinn.Ahorros.Services;
using Xpinn.Ahorros.Entities;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    SolicitudServiciosServices _solService = new SolicitudServiciosServices();              
    
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_solService.CodigoProgramaConfirmarSolicitudServicio, "L");

            Site toolBar = (Site)Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlMensajeBorrar.eventoClick += btnContinuarBorrar_Click;
            
            toolBar.MostrarRegresar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_solService.CodigoProgramaConfirmarSolicitudServicio, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["Usuario"];

            if (!Page.IsPostBack)
            {
                mvPrincipal.ActiveViewIndex = 0;
                txtFechaNovedad.Attributes.Add("readonly", "readonly");
                cargarCombos();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_solService.CodigoProgramaConfirmarSolicitudServicio, "Page_Load", ex);
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


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");

        //if (string.IsNullOrWhiteSpace(txtFechaNovedad.Text))
        //{
        //    VerError("Ingrese la fecha de modificación.");
        //    return;
        //}

        Actualizar();
    }


    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarData();
    }


    void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        mvPrincipal.ActiveViewIndex = 0;
    }


    protected void LimpiarData()
    {
        VerError("");

        txtFechaNovedad.Text = string.Empty;
        txtIdentificacion.Text = string.Empty;
        txtNombres.Text = string.Empty;
        ddlEstado.SelectedValue = "0";

        lblTotalRegs.Visible = false;
        lblInfo.Visible = false;
        gvLista.DataSource = null;
        gvLista.DataBind();
    }

    void Actualizar()
    {
        try
        {
            string filtro = obtFiltro();

            List<Servicio> lstSolicitudes = _solService.ListarSolicitudServicio(filtro, _usuario);
            gvLista.PageSize = 15;
            gvLista.DataSource = lstSolicitudes;
            Session[Usuario.codusuario + "DTSolicitud"] = lstSolicitudes;
            if (lstSolicitudes.Count > 0)
            {
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstSolicitudes.Count.ToString();
            }
            else
            {
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }

            gvLista.DataBind();
            //numero_servicio
            //cod_linea_servicio
            //fecha_solicitud
            //valor_total
            //fecha_primera_cuota
            //numero_cuotas
            //estado
            //fechacreacion
            //cod_persona
            //nom_linea
            //nombre_titular
            //identificacion
            //descripcion_estado
        }
        catch (Exception ex)
        {
            VerError("Error al consutlar los datos, " + ex.Message);
        }
    }

    string obtFiltro()
    {
        Configuracion conf = new Configuracion();

        string filtro = string.Empty;

        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and v.identificacion = '" + txtIdentificacion.Text + "' ";
        if (txtNombres.Text.Trim() != "")
            filtro += " and upper(v.nombreyapellido) like upper('%" + txtNombres.Text + "%')";
        if (txtFechaNovedad.Text.Trim() != "")
            filtro += " and l.fecha_solicitud = To_Date('" + txtFechaNovedad.Text.Trim() + "', 'dd/MM/yyyy') ";
        if (ddlEstado.SelectedValue != "3")
            filtro += " and l.estado = '" + ddlEstado.SelectedValue + "'";


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
                    filtro += " and (v.Cod_Asesor = " + ddlAsesores.SelectedValue + " or v.Cod_Asesor is null or v.Cod_Asesor = '' or v.Cod_asesor not in (select icodigo from Asejecutivos where Iestado = 1))";
                else
                    filtro += " and v.Cod_Asesor = " + ddlAsesores.SelectedValue + " ";
            }
        }
        else if (chkSinAsesor.Checked)
            filtro += " and (v.Cod_Asesor = " + _usuario.codusuario + " or v.Cod_Asesor is null or v.Cod_Asesor = '' or v.Cod_asesor not in (select icodigo from Asejecutivos where Iestado = 1))";
        else
            filtro += " and v.Cod_Asesor = " + _usuario.codusuario + " ";

        if (ddlZona.SelectedValue != "")
        {
            if (ddlZona.SelectedValue == "0")
                filtro += " and p.cod_zona is null";
            else
                filtro += " and p.cod_zona = " + ddlZona.SelectedValue + " ";
        }

        return filtro;
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
            VerError("Error al cambiar de pagina en la tabla, " + ex.Message);
        }
    }


    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string idSol = gvLista.Rows[gvLista.SelectedRow.RowIndex].Cells[2].Text;
            ViewState.Add("IdRegistroAprobar", idSol);

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
            ViewState.Add("cod_persona", gvLista.Rows[e.NewEditIndex].Cells[3].Text);
            ViewState.Add("producto", gvLista.Rows[e.NewEditIndex].Cells[6].Text);
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
            Servicio solicitud = new Servicio();

            solicitud.numero_servicio = Convert.ToInt32(ViewState["IdRegistroRechazar"]);
            solicitud.estado = "2"; // Rechazando Solicitud
            _solService.ModificarEstadoSolicitudServicio(solicitud, (Usuario)Session["usuario"]);
            VerError("Solicitud Rechazada Correctamente!.");
            Xpinn.Comun.Services.Formato_NotificacionService COServices = new Xpinn.Comun.Services.Formato_NotificacionService();
            Xpinn.Comun.Entities.Formato_Notificacion noti = new Xpinn.Comun.Entities.Formato_Notificacion(Convert.ToInt32(ViewState["cod_persona"]), 18, "nombreProducto;"+Convert.ToString(ViewState["producto"]));
            COServices.SendEmailPerson(noti, (Usuario)Session["usuario"]);
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
            //OBTENER LOS DATOS PARA PASAR AL PROCESO DE CREACION
            int id_solicitud = Convert.ToInt32(ViewState["IdRegistroAprobar"]);
            string filtro = string.Empty;
            filtro += " and L.id_sol_servicio = '" + id_solicitud + "' ";

            List<Servicio> lstSol = _solService.ListarSolicitudServicio(filtro, (Usuario)Session["usuario"]);
            Servicio solicitud = lstSol.ElementAt(0);
            Session["solicitudServWeb"] = solicitud;
            Response.Redirect("../../Servicios/SolicitudServicios/Nuevo.aspx", false);
        }
        catch (Exception ex)
        {
            VerError("Error al intentar generar el retiro, " + ex.Message);
        }
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
