using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Entities;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Tesoreria.Services.TrasladoPagaduriasServices TrasladoPagaduriasServicio = new Xpinn.Tesoreria.Services.TrasladoPagaduriasServices();
    private PoblarListas poblarLista = new PoblarListas();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            Site toolBar = (Site)this.Master;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlBusquedaPersonas.eventoEditar += OnrowEdit_Click;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TrasladoPagaduriasServicio.CodigoPrograma, "Page_PreInit", ex);
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
            if (ValidarDatos())
                ctlMensaje.MostrarMensaje("Desea realizar la grabación de datos?");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TrasladoPagaduriasServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }
   
    
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Tesoreria.Entities.TrasladoPagadurias TrasladoPagadurias = new Xpinn.Tesoreria.Entities.TrasladoPagadurias();



            Usuario vUsuario = new Usuario();
            vUsuario = (Usuario)Session["Usuario"];

                 TrasladoPagadurias.Lista_Producto = (from GridViewRow x in gvProductos.Rows
                                                     select new Productos_Persona {
                                                         cod_tipo_producto = (x.Cells[0].Text == "CREDITOS") ? 2 : (x.Cells[0].Text == "APORTES") ? 1 :  (x.Cells[0].Text == "AHORROS") ? 3  :  (x.Cells[0].Text == "SERVICIOS") ? 4 :   (x.Cells[0].Text == "AH.PROGRAMADO") ? 9: 6,
                                                         num_producto= Convert.ToInt64(x.Cells[1].Text),
                                                         cod_linea = Convert.ToInt64(((Label)x.FindControl("lblLinea")).Text),
                                                         forma_pago = ((DropDownList)x.FindControl("ddlFormaPagoPro")).SelectedValue,
                                                         cod_empresa = (((DropDownList)x.FindControl("ddlEmpresaPro")).SelectedValue != "") ?  Convert.ToInt64(((DropDownList)x.FindControl("ddlEmpresaPro")).SelectedValue) : 0,
                                                         porcentaje = Convert.ToInt64(((TextBox)x.FindControl("txtporcentaje")).Text)
                                                     }).ToList();
                TrasladoPagadurias.cod_persona = Convert.ToInt64(txtCodigo.Text);

                TrasladoPagadurias =TrasladoPagaduriasServicio.ModificarTrasladoPagadurias(TrasladoPagadurias, vUsuario);
                Navegar(Pagina.Nuevo);
            
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
        pEmpresa.Visible = false;
        mvPrincipal.ActiveViewIndex = 1;
        ddlEmpresa.Items.Clear();
        ObtenerDatos(cod_persona);

    }

    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFormaPago.SelectedValue == "2")
            pEmpresa.Visible = true;
        else
            pEmpresa.Visible = false;
    }

    public void ObtenerDatos(Int64 cod_persona)
    {
        Xpinn.Tesoreria.Entities.TrasladoPagadurias TrasladoPagadurias = new Xpinn.Tesoreria.Entities.TrasladoPagadurias();
        TrasladoPagadurias = TrasladoPagaduriasServicio.ConsultarTrasladoPagadurias(cod_persona, (Usuario)Session["usuario"]);
        txtNumeIdentificacion.Text = TrasladoPagadurias.identificacion;
        txtNombres.Text = TrasladoPagadurias.nombre;
        if (TrasladoPagadurias.tipo_persona=="N") 
            poblarLista.PoblarListaDesplegable("V_PERSONA_EMPRESA_RECAUDO", "COD_EMPRESA,NOM_EMPRESA", "COD_PERSONA = "+ cod_persona + "", "2", ddlEmpresa, (Usuario)Session["usuario"]);
        else
            poblarLista.PoblarListaDesplegable("EMPRESA_RECAUDO", "", "ESTADO = 1", "2", ddlEmpresa, (Usuario)Session["usuario"]);

        ddlFormaPago.SelectedValue = "1";
        pEmpresa.Visible = false;
        Site toolBar = (Site)this.Master;
        Session["Productos"] = TrasladoPagadurias;
        gvProductos.EmptyDataText = emptyQuery;
        gvProductos.DataSource = TrasladoPagadurias.Lista_Producto;
        gvProductos.DataBind();
        if (TrasladoPagadurias.Lista_Producto.Count>0)
        {
            toolBar.MostrarGuardar(true);
            toolBar.MostrarCancelar(true);
        }
        else
        {
           toolBar.MostrarCancelar(true);
        }
    }

    public bool ValidarDatos()
    {
        foreach (GridViewRow n in gvProductos.Rows)
        {
            DropDownList ddlFormaPagoPro = (DropDownList)n.FindControl("ddlFormaPagoPro");
            DropDownList ddlEmpresaPro = (DropDownList)n.FindControl("ddlEmpresaPro");
            TextBox txtporcentaje = (TextBox)n.FindControl("txtporcentaje");
            if (ddlFormaPagoPro.SelectedValue == "2" && ddlEmpresaPro.SelectedValue == "")
            {
                VerError("Debe seleccionar la empresa correspondiente al producto");
                return false;
            }
            else if (txtporcentaje.Text == "")
            {
                VerError("Debe digitar el porcentajecorrespondiente al producto");
                return false;
            }
            
        }
        return true;
    }
    protected void gvProductos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Xpinn.Tesoreria.Entities.TrasladoPagadurias TrasladoPagadurias = new Xpinn.Tesoreria.Entities.TrasladoPagadurias();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            TrasladoPagadurias = (Xpinn.Tesoreria.Entities.TrasladoPagadurias)Session["Productos"];
            if (TrasladoPagadurias!=null && TrasladoPagadurias.Lista_Producto.Count()>0) {
                DropDownList ddlFormaPagoPro = (DropDownList)e.Row.FindControl("ddlFormaPagoPro");
                DropDownList ddlEmpresaPro = (DropDownList)e.Row.FindControl("ddlEmpresaPro");
                TextBox txtporcentaje = (TextBox)e.Row.FindControl("txtporcentaje");
                Label lblLinea = (Label)e.Row.FindControl("lblLinea");
                if (TrasladoPagadurias.tipo_persona == "N")
                    poblarLista.PoblarListaDesplegable("V_PERSONA_EMPRESA_RECAUDO", "COD_EMPRESA,NOM_EMPRESA", "COD_PERSONA = " + TrasladoPagadurias.cod_persona + "", "2", ddlEmpresaPro, (Usuario)Session["usuario"]);
                else
                    poblarLista.PoblarListaDesplegable("EMPRESA_RECAUDO", "", "ESTADO = 1", "2", ddlEmpresaPro, (Usuario)Session["usuario"]);

                if (TrasladoPagadurias.Lista_Producto[e.Row.DataItemIndex].forma_pago == "1" || TrasladoPagadurias.Lista_Producto[e.Row.DataItemIndex].forma_pago == "C")
                    ddlFormaPagoPro.SelectedValue = "1";
                else
                {
                    ddlFormaPagoPro.SelectedValue = "2";
                    ddlEmpresaPro.SelectedValue = TrasladoPagadurias.Lista_Producto[e.Row.DataItemIndex].cod_empresa.ToString();
                }
                lblLinea.Text = TrasladoPagadurias.Lista_Producto[e.Row.DataItemIndex].cod_linea.ToString();
                txtporcentaje.Text = TrasladoPagadurias.Lista_Producto[e.Row.DataItemIndex].porcentaje.ToString();
            if (TrasladoPagadurias.Lista_Producto[e.Row.DataItemIndex].nom_tipo_producto == "CREDITOS") 
              txtporcentaje.Enabled = true;
            else
              txtporcentaje.Enabled = false;
             }
        }
    }
}
