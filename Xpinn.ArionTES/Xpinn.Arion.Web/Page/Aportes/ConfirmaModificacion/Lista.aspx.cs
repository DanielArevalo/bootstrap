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
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    AporteServices _aporteService = new AporteServices();
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_aporteService.CodigoProgramaConfirmaModificacion, "L");

            Site toolBar = (Site)Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlMensajeBorrar.eventoClick += btnContinuarBorrar_Click;

            toolBar.MostrarGuardar(false);
            toolBar.MostrarRegresar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_aporteService.CodigoProgramaConfirmaModificacion, "Page_PreInit", ex);
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
            BOexcepcion.Throw(_aporteService.CodigoProgramaConfirmaModificacion, "Page_Load", ex);
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

            List<Aporte> lstProductoAporte = _aporteService.ListarAportesNovedadesCambio(filtro, _usuario);
            Session[Usuario.codusuario + "DTSolicitud"] = lstProductoAporte;
            gvLista.PageSize = 15;
            gvLista.DataSource = lstProductoAporte;
            Site toolBar = (Site)Master;
            
            if (lstProductoAporte.Count > 0)
            {
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstProductoAporte.Count.ToString();
                toolBar.MostrarGuardar(true);
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


    string obtFiltro()
    {
        Configuracion conf = new Configuracion();

        string filtro = string.Empty;

        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and p.identificacion = '" + txtIdentificacion.Text + "' ";
        if (txtNombres.Text.Trim() != "")
            filtro += " and p.NOMBREYAPELLIDO like '%" + txtNombres.Text.Trim() + "%' ";
        if (txtCodigoNomina.Text.Trim() != "")
            filtro += " and p.cod_nomina = '" + txtCodigoNomina + "'";
        if (txtFechaNovedad.Text.Trim() != "")
            filtro += " and nov.fecha_novedad = To_Date('" + txtFechaNovedad.Text.Trim() + "', 'dd/MM/yyyy') ";

        if (ddlEstado.SelectedValue != "3")
        {
            filtro += " and nov.estado = '" + ddlEstado.SelectedValue + "'";
        }

        if (ddlTipoProducto.SelectedValue != "0")
        {
            filtro += " and nov.cod_tipo_producto = '" + ddlTipoProducto.SelectedValue + "'";
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

        if (ddlZona.SelectedValue != "")
        {
            if (ddlZona.SelectedValue == "0")
                filtro += " and p.cod_zona is null";
            else
                filtro += " and p.cod_zona = " + ddlZona.SelectedValue + " ";
        }

        return filtro;
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            VerError("");
            string id = gvLista.DataKeys[e.RowIndex].Values[0].ToString();
            ViewState.Add("IdRegistroRechazar", id);

            ctlMensajeBorrar.MostrarMensaje("Seguro que desea rechazar esta solicitud?");
        }
        catch (Exception ex)
        {
            VerError("Ocurrio un problema al intentar rechazar el registro, " + ex.Message);
        }
    }


    void btnContinuarBorrar_Click(object sender, EventArgs e)
    {
        try
        {
            Aporte aporte = new Aporte();
            aporte.id_novedad_cambio = Convert.ToInt64(ViewState["IdRegistroRechazar"]);
            aporte.estado_modificacion = "2"; // Rechazando Solicitud

            _aporteService.ModificarNovedadCuotaAporte(aporte, _usuario);

            VerError("Solicitud Rechazada Correctamente!.");
            Actualizar();
        }
        catch (Exception ex)
        {
            VerError("No se ha podido rechazar la solicitud, " + ex.Message);
        }
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


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            ctlMensaje.MostrarMensaje("Desea realizar la actualización de los registros seleccionados?");
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            //OBTENER LOS DATOS POR ACTUALIZAR
            List<Aporte> lstAporteActualizar = ObtenerListaActualizacion();

            foreach (var aporte in lstAporteActualizar)
            {
                _aporteService.ModificarNovedadCuotaAporte(aporte, _usuario);
            }

            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarLimpiar(false);
            toolBar.MostrarConsultar(false);
            toolBar.MostrarRegresar(true);
            mvPrincipal.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            VerError("Error al modificar los aportes, " + ex.Message);
        }
    }


    protected List<Aporte> ObtenerListaActualizacion()
    {
        List<Aporte> lstResultado = new List<Aporte>();
        StringHelper stringHelper = new StringHelper();
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
            if (cbSeleccionar != null)
            {
                if (cbSeleccionar.Checked)
                {
                    Aporte aporte = new Aporte();

                    aporte.id_novedad_cambio = Convert.ToInt64(rFila.Cells[2].Text.Replace("&nbsp;", "").Trim());
                    aporte.numero_aporte = Convert.ToInt64(rFila.Cells[4].Text.Replace("&nbsp;", "").Trim());
                    aporte.cuota = Convert.ToDecimal(stringHelper.DesformatearNumerosEnteros(rFila.Cells[10].Text.Replace("&nbsp;", "").Trim()));
                    aporte.nuevo_valor_cuota = Convert.ToDecimal(stringHelper.DesformatearNumerosEnteros(rFila.Cells[11].Text.Replace("&nbsp;", "").Trim()));
                    aporte.fecha_empieza_cambio = Convert.ToDateTime(rFila.Cells[12].Text.Replace("&nbsp;", "").Trim());
                    aporte.estado_modificacion = "1"; // Aprobando Solicitud
                    
                    lstResultado.Add(aporte);
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
            List<Aporte> lstTipos = _aporteService.ListarTipoProducto(usuario);
            ddlTipoProducto.DataTextField = "descripcion_tipo_prod";
            ddlTipoProducto.DataValueField = "cod_tipo_producto";
            ddlTipoProducto.DataSource = lstTipos;
            ddlTipoProducto.DataBind();            
        }
        catch (Exception ex)
        {
            VerError("Error al consutlar los datos, " + ex.Message);
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