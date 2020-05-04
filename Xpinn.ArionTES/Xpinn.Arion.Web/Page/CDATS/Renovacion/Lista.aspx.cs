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
using Xpinn.CDATS.Entities;
using Xpinn.CDATS.Services;

partial class Lista : GlobalWeb
{
    AperturaCDATService AperturaService = new AperturaCDATService();
    AdministracionCDATService AdmService = new AdministracionCDATService();
   
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AperturaService.codigoprogramarenovacioncdat, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.codigoprogramarenovacioncdat, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtFechaVencimiento.ToDateTime = DateTime.Now;
                cargarDropdown();
                
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.codigoprogramarenovacioncdat, "Page_Load", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtFecha.Text = string.Empty;
        txtFechaVencimiento.Text = string.Empty;
         txtCodigo.Text = string.Empty;
         txtidentificacion.Text = string.Empty;
         txtnombre.Text = string.Empty;
         ddlModalidad.ClearSelection();
         ddlOficina.ClearSelection();
         ddlasesor.ClearSelection();
        gvLista.DataSource = null;
        gvLista.DataBind();      
        //LimpiarValoresConsulta(pConsulta, AperturaService.codigoprogramarenovacioncdat);

    }
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
            Actualizar();        
    }


    protected void cargarDropdown()
    {     
        List<Cdat> lstLineaDcat = new List<Cdat>();
        Cdat Data3 = new Cdat();

        lstLineaDcat = AperturaService.ListarTipoLineaCDAT(Data3, (Usuario)Session["usuario"]);
        if (lstLineaDcat.Count > 0)
        {
            ddlModalidad.DataSource = lstLineaDcat;
            ddlModalidad.DataTextField = "nombre";
            ddlModalidad.DataValueField = "cod_oficina";
            ddlModalidad.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlModalidad.SelectedIndex = 0;
            ddlModalidad.DataBind();
        }

        List<Cdat> lstUsuarios = new List<Cdat>();
        Cdat Data2 = new Cdat();

        lstUsuarios = AperturaService.ListarUsuariosAsesores(Data2, (Usuario)Session["usuario"]);
        if (lstUsuarios.Count > 0)
        {
            ddlasesor.DataSource = lstUsuarios;
            ddlasesor.DataTextField = "nombre";
            ddlasesor.DataValueField = "cod_oficina";
            ddlasesor.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlasesor.SelectedIndex = 0;
            ddlasesor.DataBind();
        }
 
     
        List<Cdat> lstOficina = new List<Cdat>();
        Cdat Data = new Cdat();

        lstOficina = AperturaService.ListarOficinas(Data, (Usuario)Session["usuario"]);
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
            BOexcepcion.Throw(AperturaService.codigoprogramarenovacioncdat, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[1].Text;
        Session[AperturaService.codigoprogramarenovacioncdat + ".id"] = id;
        Session["Cdat"] = id;
        Session["ADMI"] = "";
        Session["RETURNO"] = "";
        Navegar(Pagina.Nuevo);
    }


   
    private void Actualizar()
    {
        try
        {
            List<Cdat> lstConsulta = new List<Cdat>();
            String filtro = obtFiltro(ObtenerValores());
            DateTime FechaApe, FechaVencimiento;

            FechaApe = txtFecha.ToDateTime == null ? DateTime.MinValue : txtFecha.ToDateTime;
            FechaVencimiento = txtFechaVencimiento.ToDateTime == null ? DateTime.MinValue : txtFechaVencimiento.ToDateTime;

            lstConsulta = this.AperturaService.ListarCdat(filtro, FechaApe, (Usuario)Session["usuario"], FechaVencimiento);


            
            
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

            Session.Add(AperturaService.codigoprogramarenovacioncdat + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.codigoprogramarenovacioncdat, "Actualizar", ex);
        }
    }

    private Cdat ObtenerValores()
    {
        Cdat vApertu = new Cdat();
        if (txtCodigo.Text.Trim() != "")
            vApertu.numero_cdat = Convert.ToString(txtCodigo.Text);
        if (ddlModalidad.SelectedIndex != 0)
            vApertu.modalidad = ddlModalidad.SelectedValue;
        if (ddlOficina.SelectedIndex != 0)
            vApertu.cod_oficina = Convert.ToInt32(ddlOficina.SelectedValue);
        
        return vApertu;
    }



    private string obtFiltro(Cdat vApertu)
    {
        String filtro = String.Empty;

        if (txtCodigo.Text.Trim() != "")
            filtro += " and  C.numero_cdat = '" + vApertu.numero_cdat + "'";
        if (ddlModalidad.SelectedIndex != 0)
            filtro += " and  C.modalidad = '" + vApertu.modalidad +"'";
        if (ddlOficina.SelectedIndex != 0)
            filtro += " and C.cod_oficina = " + vApertu.cod_oficina;
        if (txtCodigoNomina.Text.Trim() != "")
            filtro += " and  p.cod_nomina  = '" + txtCodigoNomina.Text + "'";

        filtro += " and c.estado = 2";

        return filtro;
    }


   
}