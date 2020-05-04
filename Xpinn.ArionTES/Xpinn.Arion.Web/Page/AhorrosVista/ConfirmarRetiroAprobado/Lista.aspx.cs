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
            VisualizarOpciones(_ahorrosService.CodigoProgramaConfirmarRetiroAprobado, "L");            

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
            BOexcepcion.Throw(_ahorrosService.CodigoProgramaConfirmarRetiroAprobado, "Page_PreInit", ex);
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
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_ahorrosService.CodigoProgramaConfirmarRetiroAprobado, "Page_Load", ex);
        }
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

        filtro += " and s.ESTADO = '4'";
        if (ddlTipoProducto.SelectedValue != "0")
            filtro += " and s.tipo_producto = '" + ddlTipoProducto.SelectedValue + "'";
      
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
            //OBTENER LOS DATOS PARA PASAR AL PROCESO DE CREACION
            int id_solicitud = Convert.ToInt32(ViewState["IdRegistroAprobar"]);
            string filtro = string.Empty;
            filtro += " and s.ID_retiro_ahorros = '" + id_solicitud + "' ";
            _usuario = (Usuario)Session["Usuario"];
            List<AhorroVista> lstSolRetiro = _ahorrosService.ListarSolicitudRetiro(filtro, _usuario);
            AhorroVista solicitud = lstSolRetiro.ElementAt(0);            
            Session["cod_persona"] = solicitud.cod_persona;
            Session["solicitud"] = solicitud;
            string tipo = Convert.ToString(solicitud.tipo_producto);
            switch (tipo)
            {
                case "3": //Redirige a creación de retiro Ahorro                    
                    Session[AhorroVistaServicio.CodigoProgramaRet + ".id"] = solicitud.numero_cuenta;
                    Response.Redirect("../../AhorrosVista/RetiroAhorros/Nuevo.aspx", false);
                    break;
                case "5": //Redirige a creación de CDAT
                    AperturaCDATService AperturaService = new AperturaCDATService();
                    Session[AperturaService.CodigoProgramaCierre + ".id"] = solicitud.numero_cuenta;
                    Session[AperturaService.CodigoProgramaCierre + ".ov"] = 1;
                    // Session[AperturaService.CodigoProgramaCierre + ".id"].ToString();
                    Response.Redirect("../../CDATS/Cierre/Nuevo.aspx", false);
                    break;
                case "9": //Redirige a creación de Aporte
                    Session[cuentasProgramado.CodigoProgramaCierreCuenta + ".id"] = solicitud.numero_cuenta;
                    Response.Redirect("../../Programado/CierreCuentas/Nuevo.aspx", false);
                    break;
                default:
                    break;
            }                 
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
    
}