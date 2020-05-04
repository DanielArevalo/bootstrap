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
using Xpinn.Ahorros.Services;
using Xpinn.Ahorros.Entities;
using Xpinn.CDATS.Entities;
using Xpinn.CDATS.Services;
using Xpinn.Programado.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    //Instancia de objetos necesarios
    AhorroVistaServices _ahorrosService = new AhorroVistaServices();
    private Xpinn.Ahorros.Services.AhorroVistaServices AhorroVistaServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
    CuentasProgramadoServices cuentasProgramado = new CuentasProgramadoServices();

    Usuario _usuario;

    //requiere cambio
    /// <summary>
    /// Pre cargado de la página
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_ahorrosService.CodigoProgramaConfirmarRetiroAhorros, "L");            

            Site toolBar = (Site)Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlMensajeBorrar.eventoClick += btnContinuarBorrar_Click;

            toolBar.MostrarGuardar(false);
            toolBar.MostrarRegresar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_ahorrosService.CodigoProgramaConfirmarRetiroAhorros, "Page_PreInit", ex);
        }
    }

    //requiere cambio
    /// <summary>
    /// Carga de la página
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["solicitud"] = null;
            //Obtiene la sesión
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
            BOexcepcion.Throw(_ahorrosService.CodigoProgramaConfirmarRetiroAhorros, "Page_Load", ex);
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

    //Evento del boton consultar
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

    //evento del boton limpiar
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarData();
    }

    //Vuelve al menú principal
    void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        mvPrincipal.ActiveViewIndex = 0;
    }

    //Limpia los filtros
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

    //actualiza la grid de datos
    void Actualizar()
    {
        try
        {
            string filtro = obtFiltro();

            List<AhorroVista> lstAhorroVista = _ahorrosService.ListarSolicitudRetiro(filtro, _usuario);

            gvLista.PageSize = 15;
            gvLista.DataSource = lstAhorroVista;
            Site toolBar = (Site)Master;
            Session[Usuario.codusuario + "DTSolicitud"] = lstAhorroVista;
            if (lstAhorroVista.Count > 0)
            {
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstAhorroVista.Count.ToString();
                toolBar.MostrarGuardar(false);          
            }
            else
            {
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
                toolBar.MostrarGuardar(false);
            }

            gvLista.DataBind();
        }
        catch (Exception ex)
        {
            VerError("Error al consutlar los datos, " + ex.Message);
        }
    }

    //Genera filtro para la consulta
    string obtFiltro()
    {
        Configuracion conf = new Configuracion();

        string filtro = string.Empty;

        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and p.identificacion = '" + txtIdentificacion.Text + "' ";
        if (txtNombres.Text.Trim() != "")
            filtro += " and l.DESCRIPCION like '%" + txtNombres.Text.Trim() + "%' ";
        if (txtCodigoProducto.Text.Trim() != "")
            filtro += " and s.NUMERO_CUENTA = '" + txtCodigoProducto + "'";
        if (txtFechaNovedad.Text.Trim() != "")
            filtro += " and s.FECHA_SOLICITUD = To_Date('" + txtFechaNovedad.Text.Trim() + "', 'dd/MM/yyyy') ";
        if (ddlEstado.SelectedValue != "3")
        {
            filtro += " and s.ESTADO = '" + ddlEstado.SelectedValue + "'";
        }
        filtro += " and s.ESTADO <> '4'";
        if (ddlTipoProducto.SelectedValue != "0")
            filtro += " and s.tipo_producto = '" + ddlTipoProducto.SelectedValue + "'";

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


    #region EVENTOS GRIDVIEW

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {                
        try
        {
            VerError("");
            string id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
            ViewState.Add("IdRegistroRechazar", id);
            ViewState.Add("cod_persona", gvLista.Rows[e.NewEditIndex].Cells[3].Text);
            ViewState.Add("producto", gvLista.Rows[e.NewEditIndex].Cells[10].Text); 
            e.NewEditIndex = -1;
            ctlMensajeBorrar.MostrarMensaje("Seguro que desea rechazar esta solicitud?");
        }
        catch (Exception ex)
        {
            VerError("Ocurrio un problema al intentar rechazar el registro, " + ex.Message);
        }

    }


    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string id = gvLista.Rows[gvLista.SelectedRow.RowIndex].Cells[2].Text;
            ViewState.Add("IdRegistroAprobar", id);

            ctlMensaje.MostrarMensaje("Seguro desea continuar el proceso para esta solicitud?");
        }
        catch (Exception ex)
        {
            VerError("Ocurrio un problema al intentar continuar con el proceso, " + ex.Message);
        }
        
        
    }

    #endregion



    //Finaliza eliminación
    void btnContinuarBorrar_Click(object sender, EventArgs e)
    {
        try
        {
            AhorroVista vista = new AhorroVista();
            
            vista.id_solicitud = Convert.ToInt32(ViewState["IdRegistroRechazar"]);
            vista.nom_estado = "2"; // Rechazando Solicitud

            _ahorrosService.ModificarEstadoSolicitud(vista, _usuario);

            Xpinn.Comun.Services.Formato_NotificacionService COServices = new Xpinn.Comun.Services.Formato_NotificacionService();
            Xpinn.Comun.Entities.Formato_Notificacion noti = new Xpinn.Comun.Entities.Formato_Notificacion(Convert.ToInt32(ViewState["cod_persona"]), 18, "nombreProducto;"+Convert.ToString(ViewState["producto"]));
            COServices.SendEmailPerson(noti, (Usuario)Session["usuario"]);

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
            AhorroVista vista = new AhorroVista();
            //OBTENER LOS DATOS PARA PASAR AL PROCESO DE CREACION
            int id_solicitud = Convert.ToInt32(ViewState["IdRegistroAprobar"]);

            vista.id_solicitud = id_solicitud;
            vista.nom_estado = "4"; // Pre Aprobando Solicitud

            _ahorrosService.ModificarEstadoSolicitud(vista, _usuario);

            VerError("Solicitud aprobada correctamente!.");
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
            VerError("Error al cambiar de pagina en la tabla, " + ex.Message);
        }
    }


    protected void cbSeleccionarEncabezado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbSeleccionarEncabezado = (CheckBox)sender;
        if (cbSeleccionarEncabezado != null && cbSeleccionarEncabezado.Enabled != false)
        {
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
                cbSeleccionar.Checked = cbSeleccionarEncabezado.Checked;
            }
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