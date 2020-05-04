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

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AperturaService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.CodigoPrograma, "Page_PreInit", ex);
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
            BOexcepcion.Throw(AperturaService.CodigoPrograma, "Page_Load", ex);
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
        ddlModalidad.Items.Insert(0, new ListItem("Seleccione un Item", "0"));
        ddlModalidad.Items.Insert(1, new ListItem("INDIVIDUAL", "IND"));
        ddlModalidad.Items.Insert(2, new ListItem("CONJUNTA", "CON"));
        ddlModalidad.Items.Insert(3, new ListItem("ALTERNA", "ALT"));
        ddlModalidad.SelectedIndex = 0;
        ddlModalidad.DataBind();


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
            BOexcepcion.Throw(AperturaService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[AperturaService.CodigoPrograma + ".id"] = id;
        Session["ADMI"] = "";
        Session["RETURNO"] = "";
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        Session["ID"] = id;
        ctlMensaje.MostrarMensaje("Desea realizar la eliminación?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            AperturaService.EliminarAperturaCdat(Convert.ToInt64(Session["ID"]), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Cdat> lstConsulta = new List<Cdat>();
            String filtro = obtFiltro(ObtenerValores());
            DateTime FechaApe;

            FechaApe = txtFecha.ToDateTime == null ? DateTime.MinValue : txtFecha.ToDateTime;

            lstConsulta = AperturaService.ListarCdats(filtro,FechaApe, (Usuario)Session["usuario"]);

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

            Session.Add(AperturaService.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Cdat ObtenerValores()
    {
        Cdat vApertu = new Cdat();
        if (txtCodigo.Text.Trim() != "")
            vApertu.codigo_cdat = Convert.ToInt64(txtCodigo.Text);
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
            filtro += " and  C.codigo_cdat = " + vApertu.codigo_cdat;
        if (ddlModalidad.SelectedIndex != 0)
            filtro += " and  C.modalidad = '" + vApertu.modalidad +"'";
        if (ddlOficina.SelectedIndex != 0)
            filtro += " and C.cod_oficina = " + vApertu.cod_oficina;

        filtro += " and C.estado = 1";

        return filtro;
    }


   
}