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
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;


partial class Lista : GlobalWeb
{
    CuentasBancariasServices CuentaService = new CuentasBancariasServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CuentaService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarDropdown();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoPrograma, "Page_Load", ex);
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

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    protected void cargarDropdown()
    {
        List<CuentasBancarias> lstConsulta = new List<CuentasBancarias>();
        lstConsulta = CuentaService.ListarBancos((Usuario)Session["usuario"]);

        if (lstConsulta.Count > 0)
        {
            ddlBanco.DataSource = lstConsulta;
            ddlBanco.DataTextField = "nombrebanco";
            ddlBanco.DataValueField = "cod_banco";
            ddlBanco.AppendDataBoundItems = true;
            ddlBanco.Items.Insert(0, new ListItem("Selecione un item","0"));
            ddlBanco.SelectedIndex = 0;
            ddlBanco.DataBind();
        }

        try
        {
            lstConsulta = CuentaService.ListarNumeroCuentas((Usuario)Session["usuario"]);
            if (lstConsulta.Count > 0)
            {
                ddlNumeroCuenta.DataSource = lstConsulta;
                ddlNumeroCuenta.DataTextField = "nombrecuenta";
                ddlNumeroCuenta.DataValueField = "num_cuenta";
                ddlNumeroCuenta.AppendDataBoundItems = true;
                ddlNumeroCuenta.Items.Insert(0, new ListItem("Selecione un item", "0"));
                ddlNumeroCuenta.SelectedIndex = 0;
                ddlNumeroCuenta.DataBind();
            }
        }
        catch { }

        ddlEstado.Items.Insert(0, new ListItem("Seleccione un item","0"));
        ddlEstado.Items.Insert(1, new ListItem("Activo", "1"));
        ddlEstado.Items.Insert(2, new ListItem("Inactivo", "2"));
        ddlEstado.SelectedIndex = 0;
        ddlEstado.DataBind();
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
            BOexcepcion.Throw(CuentaService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;         
        Session[CuentaService.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        Session["ID"] = id;
        ctlMensaje.MostrarMensaje("Desea realizar la eliminación de la cuenta?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            CuentaService.EliminarCuentasBancarias(Convert.ToInt32(Session["ID"]), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }

    
    private void Actualizar()
    {
        try
        {
            List<CuentasBancarias> lstConsulta = new List<CuentasBancarias>();
            string filtro = obtFiltro();
           
            lstConsulta = CuentaService.ListarCuentasBancarias(filtro,(Usuario)Session["usuario"]);

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

            Session.Add(CuentaService.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoPrograma, "Actualizar", ex);
        }
    }
      
   
    private string obtFiltro()
    {
        Configuracion conf = new Configuracion();

        String filtro = String.Empty;

        if (txtCodigo.Text.Trim() != "")
            filtro += " and c.idctabancaria = " + txtCodigo.Text;
        if (ddlBanco.SelectedIndex != 0 && ddlBanco.SelectedItem != null)
            filtro += " and c.cod_banco = " + ddlBanco.SelectedValue;       
        if (ddlNumeroCuenta.SelectedIndex != 0 && ddlNumeroCuenta.SelectedItem != null)
            filtro += " and c.num_cuenta = '" + ddlNumeroCuenta.SelectedValue +"'";
        if (ddlEstado.SelectedIndex != 0)
            filtro += " and c.estado = '" + ddlEstado.SelectedValue+"'";
        
        return filtro;
    }

   
   
}