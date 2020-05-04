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
using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;


partial class Lista : GlobalWeb
{
    //Instancia de objetos necesarios
    CruceCtaAProductoServices _cruceService = new CruceCtaAProductoServices();
    AhorroVistaServices _ahorrosService = new AhorroVistaServices();
    private Xpinn.Ahorros.Services.AhorroVistaServices AhorroVistaServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
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
            VisualizarOpciones(_ahorrosService.CodigoProgramaConfirmarCruceAhorrosProductos, "L");            

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
            BOexcepcion.Throw(_ahorrosService.CodigoProgramaConfirmarCruceAhorrosProductos, "Page_PreInit", ex);
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
            BOexcepcion.Throw(_ahorrosService.CodigoProgramaConfirmarCruceAhorrosProductos, "Page_Load", ex);
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
            List<Solicitud_cruce_ahorro> lstSolicitud = new List<Solicitud_cruce_ahorro>();
            lstSolicitud = _cruceService.ListarSolicitud_cruce(filtro, _usuario);

            gvLista.PageSize = 15;
            gvLista.DataSource = lstSolicitud;
            Site toolBar = (Site)Master;

            if (lstSolicitud.Count > 0)
            {
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstSolicitud.Count.ToString();
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
            filtro += " and P.IDENTIFICACION = '" + txtIdentificacion.Text + "' ";
        if (txtNombres.Text.Trim() != "")
            filtro += " and P.NOMBRE like '%" + txtNombres.Text.Trim() + "%' ";
        if (txtFechaNovedad.Text.Trim() != "")
            filtro += " and S.FECHA_PAGO = To_Date('" + txtFechaNovedad.Text.Trim() + "', 'dd/MM/yyyy') ";
        if (ddlEstado.SelectedValue != "3")
        {
            filtro += " and S.ESTADO = '" + ddlEstado.SelectedValue + "'";
        }

        if (chkSinAsesor.Checked)
        {
            filtro += " and (P.Cod_Asesor = " + _usuario.codusuario + " or P.Cod_Asesor is null)";
        }
        else
        {
            filtro += " and P.Cod_Asesor = " + _usuario.codusuario + " ";
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
            ViewState.Add("IdRegistroRechazar", id);

            ctlMensaje.MostrarMensaje("Seguro aprobar esta solicitud?");
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
            Solicitud_cruce_ahorro sol = new Solicitud_cruce_ahorro();
            
            sol.idcruceahorro = Convert.ToInt32(ViewState["IdRegistroRechazar"]);
            sol.nom_estado = "2"; // Rechazando Solicitud

            _cruceService.CambiarEstado_Solicitud_Cruce_ahorro(sol, (Usuario)Session["Usuario"]);

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
            //OBTENER LOS DATOS PARA ACTUALIZAR LAS CUENTAS
            int id_solicitud = Convert.ToInt32(ViewState["IdRegistroRechazar"]);
            _usuario = (Usuario)Session["Usuario"];
            string filtro = string.Empty;
            filtro += " and S.IDCRUCEAHORRO = " + id_solicitud;
            List<Solicitud_cruce_ahorro> lstSolic = _cruceService.ListarSolicitud_cruce(filtro, _usuario);
            Solicitud_cruce_ahorro solicitud = lstSolic.ElementAt(0);
            List<Xpinn.Tesoreria.Entities.Operacion> op = new List<Xpinn.Tesoreria.Entities.Operacion>();
            string Error = "";
            ProcesoContable pc = new ProcesoContable();
            ProcesoContableService _servicepc = new ProcesoContableService();
            pc = _servicepc.ConsultarProcesoContableOperacion(125, _usuario);
            if(pc != null)
            {
                long proceso = pc.cod_proceso;
                bool salida = _cruceService.AplicarCruceProducto(lstSolic, _usuario, proceso, ref op, ref Error);
                if (salida)
                {
                    lblMensajeGrabar.Visible = true;
                    Actualizar();
                }
                else
                {
                    VerError(Error +"- La solicitud fue rechazada");
                    Actualizar();
                }
            }
        }
        catch (Exception ex)
        {
            VerError("Error al intentar aprobar el pago, " + ex.Message);
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