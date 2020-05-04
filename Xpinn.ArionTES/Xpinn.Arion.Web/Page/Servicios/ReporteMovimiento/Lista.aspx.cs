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
    PoblarListas Poblar = new PoblarListas();
    AprobacionServiciosServices ExcluServicios = new AprobacionServiciosServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ExcluServicios.CodigoProgramaReporteMovimiento, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExcluServicios.CodigoProgramaReporteMovimiento, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarDropdown();
               
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExcluServicios.CodigoProgramaReporteMovimiento, "Page_Load", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ExcluServicios.CodigoProgramaReporteMovimiento);
        txtFecha.Text = "";
        Actualizar();
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            Actualizar();
        }
    }

    protected void cargarDropdown()
    {
        Poblar.PoblarListaDesplegable("LINEASSERVICIOS", ddlLinea, (Usuario)Session["usuario"]);

        List<Servicio> lstOficina = new List<Servicio>();
        Servicio Data = new Servicio();

        lstOficina = ExcluServicios.ListarOficinas(Data, (Usuario)Session["usuario"]);
        if (lstOficina.Count > 0)
        {
            ddlOficina.DataSource = lstOficina;
            ddlOficina.DataTextField = "nombre";
            ddlOficina.DataValueField = "cod_oficina";
            ddlOficina.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlOficina.SelectedIndex = 0;
            ddlOficina.DataBind();
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
            BOexcepcion.Throw(ExcluServicios.CodigoProgramaReporteMovimiento, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Session.Remove("Retorno");
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[ExcluServicios.CodigoProgramaReporteMovimiento + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }


    
    private void Actualizar()
    {
        try
        {
            List<Servicio> lstConsulta = new List<Servicio>();
            String filtro = obtFiltro(ObtenerValores());
            DateTime pFechaIni;
            pFechaIni = txtFecha.ToDateTime == null ? DateTime.MinValue : txtFecha.ToDateTime;

            lstConsulta = ExcluServicios.ListarServicios(filtro,"", pFechaIni, (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataBind();
                panelGrilla.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();                
                
            }
            else
            {
                panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }

            Session.Add(ExcluServicios.CodigoProgramaReporteMovimiento + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExcluServicios.CodigoProgramaReporteMovimiento, "Actualizar", ex);
        }
    }

    private Servicio ObtenerValores()
    {
        Servicio vCuentas = new Servicio();
        if (txtNumServ.Text.Trim() != "")
            vCuentas.numero_servicio = Convert.ToInt32(txtNumServ.Text.Trim());        
        if (ddlLinea.SelectedIndex != 0)
            vCuentas.cod_linea_servicio = ddlLinea.SelectedValue;
        if (txtIdentificacion.Text != "")
            vCuentas.identificacion = txtIdentificacion.Text;
        
        if (txtNombre.Text.Trim() != "")
            vCuentas.nombre = txtNombre.Text.Trim().ToUpper();


        return vCuentas;
    }



    private string obtFiltro(Servicio Cuentas)
    {
        String filtro = String.Empty;

        if (txtNumServ.Text.Trim() != "")
            filtro += " and s.numero_servicio = " + Cuentas.numero_servicio;
        if (txtIdentificacion.Text != "")
            filtro += " and p.identificacion like '%" + Cuentas.identificacion + "%'";
        if (txtCodigoNomina.Text != "")
            filtro += " and p.cod_nomina like '%" + txtCodigoNomina.Text + "%'";
        if (ddlLinea.SelectedIndex != 0)
            filtro += "AND s.cod_linea_servicio = '" + ddlLinea.SelectedValue + "'";

        return filtro;
    }


   
}