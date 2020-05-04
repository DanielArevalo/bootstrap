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
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Services;

partial class Lista : GlobalWeb
{
    LineaServiciosServices LineaServicios = new LineaServiciosServices();
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(LineaServicios.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaServicios.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["usuario"];

            if (!IsPostBack)
            {
                cargarDropdown();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaServicios.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {

        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            Actualizar();
        }
    }


    void cargarDropdown()
    {
        ddlTipoServicio.Items.Insert(0,new ListItem("Seleccione un item","0"));
        ddlTipoServicio.Items.Insert(1, new ListItem("Medicina Prepagada", "1"));
        ddlTipoServicio.Items.Insert(2, new ListItem("Planes Exequiales", "2"));
        ddlTipoServicio.Items.Insert(3, new ListItem("Seguros", "3"));
        ddlTipoServicio.Items.Insert(4, new ListItem("Otros", "4"));
        ddlTipoServicio.SelectedIndex = 0;
        ddlTipoServicio.DataBind();
    }

   


    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove(LineaServicios.CodigoPrograma + ".id");
        Navegar(Pagina.Nuevo);
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
            BOexcepcion.Throw(LineaServicios.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[LineaServicios.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        Session["ID"] = id;
        ctlMensaje.MostrarMensaje("Desea realizar la eliminación del Servicio?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            LineaServicios.EliminarLineaServicio(Convert.ToInt32(Session["ID"]), _usuario);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaServicios.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }

    
    private void Actualizar()
    {
        try
        {
            List<LineaServicios> lstConsulta = new List<LineaServicios>();
            String filtro = obtFiltro(ObtenerValores());
            
            lstConsulta = LineaServicios.ListarLineaServicios(ObtenerValores(), _usuario, filtro);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;           

            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;                
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();                
                
            }
            else
            {
                panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
            }

            Session.Add(LineaServicios.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaServicios.CodigoPrograma, "Actualizar", ex);
        }
    }

    private LineaServicios ObtenerValores()
    {
        LineaServicios vCuentas = new LineaServicios();
        if (txtCodigo.Text.Trim() != "")
            vCuentas.cod_linea_servicio = txtCodigo.Text.Trim();        
        if (ddlTipoServicio.SelectedIndex != 0)
            vCuentas.tipo_servicio = Convert.ToInt32(ddlTipoServicio.SelectedValue);
        if (txtIdentificacion.Text != "")
            vCuentas.identificacion_proveedor = txtIdentificacion.Text;
        
        if (txtDescripcion.Text.Trim() != "")
            vCuentas.nombre = txtDescripcion.Text.Trim().ToUpper();

        return vCuentas;
    }



    private string obtFiltro(LineaServicios linea)
    {
        Configuracion conf = new Configuracion();

        String filtro = String.Empty;

        if (txtCodigo.Text.Trim() != "")
            filtro += " and l.cod_linea_servicio = " + linea.cod_linea_servicio;
        if (ddlTipoServicio.SelectedIndex != 0)
            filtro += " and l.tipo_servicio = " + linea.tipo_servicio;
        if (txtIdentificacion.Text != "")
            filtro += " and l.identificacion_proveedor like '%" + linea.identificacion_proveedor + "%'";
        if (txtDescripcion.Text.Trim() != "")
            filtro += " and l.nombre like '%" + linea.nombre + "%'";       

        return filtro;
    }


   
}