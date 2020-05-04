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
    ReclamacionServiciosServicesService ServicioReclamacion = new ReclamacionServiciosServicesService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ExcluServicios.CodigoProgramaReclamacionServicios, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExcluServicios.CodigoProgramaReclamacionServicios, "Page_PreInit", ex);
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
            BOexcepcion.Throw(ExcluServicios.CodigoProgramaReclamacionServicios, "Page_Load", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ExcluServicios.CodigoProgramaReclamacionServicios);
        txtFechareclamacion.Text = "";
        txtIdentFallecido.Text = "";
        txtIdentificacion.Text = "";
        txtNombre.Text = "";
        txtNumServ.Text = "";
        ddlLinea.SelectedIndex = 0;
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

    void cargarDropdown()
    {
        Poblar.PoblarListaDesplegable("LINEASSERVICIOS", ddlLinea, (Usuario)Session["usuario"]);
    }
    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
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
            BOexcepcion.Throw(ExcluServicios.CodigoProgramaReclamacionServicios, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[ExcluServicios.CodigoProgramaReclamacionServicios + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[ExcluServicios.CodigoProgramaReclamacionServicios + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

  

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        List<ReclamacionServicios> LstDetalle = new List<ReclamacionServicios>();
        if (conseID > 0)
        {
            try
            {
            ServicioReclamacion.EliminarReclamacionServiciosServices(conseID, (Usuario)Session["usuario"]);
            }
            
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(ServicioReclamacion.CodigoPrograma, "gvLista_RowDeleting", ex);
            }
        }
        else
        {
            LstDetalle.RemoveAt((gvLista.PageIndex * gvLista.PageSize) + e.RowIndex);
        }

        Session["Beneficiario"] = LstDetalle;

        gvLista.DataSourceID = null;
        gvLista.DataBind();
        gvLista.DataSource = LstDetalle;
        gvLista.DataBind();
    }
    
    private void Actualizar()
    {
        try
        {
            List<ReclamacionServicios> lstConsulta = new List<ReclamacionServicios>();
            String filtro = obtFiltro(ObtenerValores());
            DateTime pFechaIni;
            pFechaIni = txtFechareclamacion.ToDateTime == null ? DateTime.MinValue : txtFechareclamacion.ToDateTime;

            lstConsulta = ServicioReclamacion.ListarReclamacionServiciosServices(filtro, "", pFechaIni, (Usuario)Session["usuario"]);


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

            Session.Add(ExcluServicios.CodigoProgramaReclamacionServicios + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ExcluServicios.CodigoProgramaReclamacionServicios, "Actualizar", ex);
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
        if (ddlLinea.SelectedIndex != 0)
            filtro += " and s.COD_LINEA_SERVICIO = " + Cuentas.cod_linea_servicio;
        if (txtIdentificacion.Text != "")
            filtro += " and p.identificacion like '%" + Cuentas.identificacion + "%'";
        if (txtNombre.Text.Trim() != "")
            filtro += " and p.primer_nombre ||' '|| p.segundo_nombre||' '|| p.primer_apellido||' '||p.segundo_apellido like '%" + Cuentas.nombre + "%'";
        if (txtCodigoNomina.Text != "")
            filtro += " and p.cod_nomina like '%" + txtCodigoNomina.Text + "%'";

        return filtro;
    }


   
}