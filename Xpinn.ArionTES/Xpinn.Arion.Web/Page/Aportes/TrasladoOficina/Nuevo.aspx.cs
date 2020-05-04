using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.WebControls;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.Persona1Service PersonaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
    private PoblarListas poblarLista = new PoblarListas();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            Site toolBar = (Site)this.Master;
            VisualizarOpciones(PersonaServicio.CodigoProgramaTrasladoOficina, "N");
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlBusquedaPersonas.eventoEditar += OnrowEdit_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
            pProceso.Visible = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PersonaServicio.CodigoProgramaTrasladoOficina, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
            VerError("");
            ctlBusquedaPersonas.Filtro = "";
       }
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            
                ctlMensaje.MostrarMensaje("Desea realizar la grabación de datos?");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PersonaServicio.CodigoProgramaTrasladoOficina, "btnGuardar_Click", ex);
        }
    }
   
    
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Persona1 Persona1TrasladoOficina = new Xpinn.FabricaCreditos.Entities.Persona1();



            Usuario vUsuario = new Usuario();
            vUsuario = (Usuario)Session["Usuario"];

            Persona1TrasladoOficina.Lista_Producto = (from GridViewRow x in gvProductos.Rows
                                                    select new Productos_Persona {
                                                        cod_tipo_producto = (x.Cells[0].Text == "CREDITOS") ? 2 : (x.Cells[0].Text == "APORTES") ? 1 : 6,
                                                        num_producto= Convert.ToInt64(x.Cells[1].Text),
                                                        cod_linea = Convert.ToInt64(((Label)x.FindControl("lblLinea")).Text),
                                                        cod_oficina= (((DropDownList)x.FindControl("ddlOficinaPro")).SelectedValue != "") ?  Convert.ToInt64(((DropDownList)x.FindControl("ddlOficinaPro")).SelectedValue) : 0
                                                     }).ToList();
            Persona1TrasladoOficina.cod_persona = Convert.ToInt64(txtCodigo.Text);

            Persona1TrasladoOficina = PersonaServicio.ModificarTrasladoOficinas(Persona1TrasladoOficina, vUsuario);

            Int64? rpta = 0;
            if (!pProceso.Visible && pConsulta.Visible)
            {
                rpta = ctlproceso.Inicializar(130, Convert.ToDateTime(DateTime.Now), (Usuario)Session["Usuario"]);
                if (rpta <= 0)
                {
                    VerError("No se encontró parametrización contable por procesos para el tipo de operación 130 = Traslado de Oficina");
                    return;
                }
                else if (rpta > 1)
                {
                    Site toolBar = (Site)Master;
                    toolBar.MostrarGuardar(false);
                    toolBar.MostrarCancelar(false);
                    toolBar.MostrarConsultar(false);
                    pConsulta.Visible = false;
                    pProceso.Visible = true;
                }
                else
                {
                    if (GenerarComprobante(ctlproceso.cod_proceso))
                    {
                        Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                    }
                }
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)this.Master;
        mvPrincipal.ActiveViewIndex = 0;
        toolBar.MostrarGuardar(false);
        toolBar.MostrarCancelar(false);
        toolBar.MostrarConsultar(true);
        gvProductos.DataSource = null;
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        VerError("");
        ctlBusquedaPersonas.Filtro = "";
        ctlBusquedaPersonas.Actualizar(0);
    }

    protected void OnrowEdit_Click(object sender, EventArgs e)
    {
        VerError("");
        Site toolBar = (Site)this.Master;
        toolBar.MostrarConsultar(false);
        ctlBusquedaPersonas.Filtro = "";
        GridView gv_lista = (GridView)sender;
        Int64 cod_persona = Convert.ToInt64(gv_lista.SelectedValue.ToString());
        txtCodigo.Text = cod_persona.ToString();
        mvPrincipal.ActiveViewIndex = 1;
        ddlOficina.Items.Clear();
        ObtenerDatos(cod_persona);

    }

    public void ObtenerDatos(Int64 cod_persona)
    {
        Xpinn.FabricaCreditos.Entities.Persona1 Persona1TrasladoOficina = new Xpinn.FabricaCreditos.Entities.Persona1();
        Persona1TrasladoOficina = PersonaServicio.ConsultarPersona1TrasladoOficina(cod_persona, (Usuario)Session["usuario"]);
        txtNumeIdentificacion.Text = Persona1TrasladoOficina.identificacion;
        txtNombres.Text = Persona1TrasladoOficina.nombre;
        poblarLista.PoblarListaDesplegable("OFICINA", "COD_OFICINA,NOMBRE", "", "2", ddlOficina, (Usuario)Session["usuario"]);
        Site toolBar = (Site)this.Master;
        Session["Productos"] = Persona1TrasladoOficina;
        gvProductos.EmptyDataText = emptyQuery;
        gvProductos.DataSource = Persona1TrasladoOficina.Lista_Producto;
        gvProductos.DataBind();
        if (Persona1TrasladoOficina.Lista_Producto.Count>0)
        {
            toolBar.MostrarGuardar(true);
            toolBar.MostrarCancelar(true);
        }
        else
        {
           toolBar.MostrarCancelar(true);
        }
    }

    protected void gvProductos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Xpinn.FabricaCreditos.Entities.Persona1 Persona1TrasladoOficina = new Xpinn.FabricaCreditos.Entities.Persona1();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Persona1TrasladoOficina = (Xpinn.FabricaCreditos.Entities.Persona1)Session["Productos"];
            if (Persona1TrasladoOficina != null && Persona1TrasladoOficina.Lista_Producto.Count()>0) {
                DropDownList ddlOficinaPro = (DropDownList)e.Row.FindControl("ddlOficinaPro");
                Label lblLinea = (Label)e.Row.FindControl("lblLinea");
                poblarLista.PoblarListaDesplegable("OFICINA", "COD_OFICINA,NOMBRE", "", "2", ddlOficinaPro, (Usuario)Session["usuario"]);
                ddlOficinaPro.SelectedValue = Persona1TrasladoOficina.Lista_Producto[e.Row.DataItemIndex].cod_oficina.ToString();
                lblLinea.Text = Persona1TrasladoOficina.Lista_Producto[e.Row.DataItemIndex].cod_linea.ToString();
             }
             
        }
    }

    protected void btnCancelarProceso_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarCancelar(true);
        toolBar.MostrarConsultar(true);
        pConsulta.Visible = true;
        pProceso.Visible = false;
    }

    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            pConsulta.Visible = true;
            pProceso.Visible = false;
            if (GenerarComprobante(ctlproceso.cod_proceso))
                pConsulta.Visible = false;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected bool GenerarComprobante(Int64? pCodProceso)
    {
        VerError("");
        // Grabar los datos
        Int64 codOpe = 0;
        string error = "";
        Int64 num_comp = 0;
        Int64 tip_comp = 0;
        Usuario usu = (Usuario)Session["Usuario"];
        DateTime fechaAplicacion = Convert.ToDateTime(DateTime.Now);
        if (!ComprobanteServicio.GenerarComprobante(codOpe,130, fechaAplicacion, Convert.ToInt64(txtCodigo.Text), Convert.ToInt64(usu.cod_persona), Convert.ToInt64(pCodProceso),ref num_comp, ref tip_comp, ref error, (Usuario)Session["Usuario"]))
        {
            VerError("No se pudo generar el comprobante de la traslacion de oficina." + error);
            return false;
        }
        if (error.Trim() != "")
        {
            VerError("Error al generar el  comprobante de la traslacion de oficina. Error:" + error);
            return false;
        }
        
        ctlproceso.CargarVariables(codOpe, 130, usu.cod_persona, usu);
        return true;
    }
}
