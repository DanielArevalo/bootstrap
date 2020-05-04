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
    AhorroVistaServices _ahorrosService = new AhorroVistaServices();
    EjecutivoService serviceEjecutivo = new EjecutivoService();
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_ahorrosService.CodigoProgramaConfirmarSolicitudProducto, "L");

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
            BOexcepcion.Throw(_ahorrosService.CodigoProgramaConfirmarSolicitudProducto, "Page_PreInit", ex);
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
                cargarTiposProductoConNovedad(_usuario);
                cargarCombos();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_ahorrosService.CodigoProgramaConfirmarSolicitudProducto, "Page_Load", ex);
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

            List<AhorroVista> lstSolicitudes = _ahorrosService.ListarSolicitudProducto(filtro, _usuario);           
            gvLista.PageSize = 15;
            gvLista.DataSource = lstSolicitudes;
            Site toolBar = (Site)Master;
            Session[Usuario.codusuario + "DTSolicitud"] = lstSolicitudes;
            if (lstSolicitudes.Count > 0)
            {
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstSolicitudes.Count.ToString();
                toolBar.MostrarGuardar(true);
            }
            else
            {
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
                toolBar.MostrarGuardar(false);
            }

            gvLista.DataBind();
            //id_solicitud
            //cod_persona
            //tipo_producto
            //cod_linea_ahorro
            //plazo
            //num_cuotas
            //valor_cuota
            //cod_periodicidad
            //od_forma_pago
            //cod_destino
            //fecha
            //estado_modificacion1
            //nom_estado
            //nombres
            //identificacion
            //nombre_producto
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
            filtro += " and p.identificacion = '" + txtIdentificacion.Text + "' ";
        if (txtNombres.Text.Trim() != "")
            filtro += " and upper(p.PRIMER_NOMBRE||' '||p.SEGUNDO_NOMBRE||' '||p.PRIMER_APELLIDO||' '||p.SEGUNDO_APELLIDO) like upper('%"+txtNombres.Text+"%')";
        if (txtFechaNovedad.Text.Trim() != "")
            filtro += " and s.fecha_solicitud = To_Date('" + txtFechaNovedad.Text.Trim() + "', 'dd/MM/yyyy') ";
        if (ddlEstado.SelectedValue != "3")        
            filtro += " and s.estado = '" + ddlEstado.SelectedValue + "'";
        filtro += " and s.estado <> 4 ";
        if (ddlTipoProducto.SelectedValue != "0")       
            filtro += " and s.cod_tipo_producto = '" + ddlTipoProducto.SelectedValue + "'";

        if (ddlZona.SelectedValue != "")
        {
            if (ddlZona.SelectedValue == "0")
                filtro += " and p.cod_zona is null";
            else
                filtro += " and p.cod_zona = " + ddlZona.SelectedValue + " ";
        }

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

        return filtro;
    }
    

    protected Boolean ValidarDatos()
    {
        if (gvLista.Rows.Count == 0)
        {
            VerError("No existen datos por registrar, verifique los datos.");
            return false;
        }
        int cont = 0;
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
            if (cbSeleccionar != null)
            {
                if (cbSeleccionar.Checked)
                    cont++;
                    //break;
            }
        }
        if (cont == 0)
        {
            VerError("No existen datos seleccionados para modificar, verifique los datos.");
            return false;
        }
        return true;
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


    /// <summary>
    /// Llena el combo de tipo producto con los tipos de producto que tengan al menos una novedad
    /// </summary>
    protected void cargarTiposProductoConNovedad(Usuario usuario)
    {
        try
        {            
            List<AhorroVista> lstTipos = _ahorrosService.ListarTipoProductoConSolicitud(usuario);
            ddlTipoProducto.DataTextField = "descripcion";
            ddlTipoProducto.DataValueField = "tipo_producto";
            ddlTipoProducto.DataSource = lstTipos;
            ddlTipoProducto.DataBind();            
        }
        catch (Exception ex)
        {
            VerError("Error al consutlar los datos, " + ex.Message);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string idSol = gvLista.Rows[gvLista.SelectedRow.RowIndex].Cells[2].Text;
            ViewState.Add("IdRegistroAprobar", idSol);

            ctlMensaje.MostrarMensaje("¿Seguro desea continuar el proceso para esta solicitud?");
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
            ViewState.Add("cod_persona", gvLista.Rows[e.NewEditIndex].Cells[5].Text);
            ViewState.Add("producto", gvLista.Rows[e.NewEditIndex].Cells[4].Text);
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
            AhorroVista solicitud = new AhorroVista();            

            solicitud.id_solicitud = Convert.ToInt32(ViewState["IdRegistroRechazar"]);
            solicitud.estado_modificacion = "2"; // Rechazando Solicitud
            _ahorrosService.ModificarEstadoSolicitudProducto(solicitud, (Usuario)Session["usuario"]);


            Xpinn.Comun.Services.Formato_NotificacionService COServices = new Xpinn.Comun.Services.Formato_NotificacionService();
            Xpinn.Comun.Entities.Formato_Notificacion noti = new Xpinn.Comun.Entities.Formato_Notificacion(Convert.ToInt32(ViewState["cod_persona"]),18, "nombreProducto;"+Convert.ToString(ViewState["producto"]));
            COServices.SendEmailPerson(noti, (Usuario)Session["usuario"]);

            VerError("Solicitud Rechazada Correctamente!.");
            Actualizar();
        }
        catch (Exception ex)
        {
            VerError("No se ha podido rechazar la solicitud, " + ex.Message);
        }
    }
    
    /// <summary>
    /// Lleva al detalle para ver documentos anexos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //OBTENER LOS DATOS PARA MOSTRAR EN DETALLE
            string id_solicitud = gvLista.Rows[e.RowIndex].Cells[2].Text;            
            string filtro = string.Empty;
            filtro += " and s.ID_SOL_PRODUCTO = '" + id_solicitud + "' ";

            List<AhorroVista> lstSolRetiro = _ahorrosService.ListarSolicitudProducto(filtro, (Usuario)Session["usuario"]);
            AhorroVista solicitud = lstSolRetiro.ElementAt(0);
            Session["solicitudProducto"] = solicitud;
            Response.Redirect("Nuevo.aspx", false);
        }
        catch (Exception ex)
        {
            VerError("Se presentó un problema, intentelo nuevamente");
        }
    }


    //Continua el proceso de aprobación en RetiroAhorros
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            AhorroVista solicitud = new AhorroVista();
            //OBTENER LOS DATOS PARA PASAR AL PROCESO DE CREACION
            int id_solicitud = Convert.ToInt32(ViewState["IdRegistroAprobar"]);            

            solicitud.id_solicitud = id_solicitud;
            solicitud.estado_modificacion = "4"; // Pre Aprobando solicitud
            _ahorrosService.ModificarEstadoSolicitudProducto(solicitud, (Usuario)Session["usuario"]);
            
            VerError("Solicitud confirmada correctamente!.");
            Actualizar();
        }
        catch (Exception ex)
        {
            VerError("Error al intentar generar la confirmación, " + ex.Message);
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