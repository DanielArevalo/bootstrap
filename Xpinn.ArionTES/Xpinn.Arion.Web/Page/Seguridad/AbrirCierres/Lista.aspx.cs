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
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Services;
using System.Text;
using System.IO;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


partial class Nuevo : GlobalWeb
{
    private Xpinn.Seguridad.Services.CierresService CierresService = new Xpinn.Seguridad.Services.CierresService();    

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[CierresService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(CierresService.CodigoPrograma, "E");
            else
                VisualizarOpciones(CierresService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CierresService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                
                UpdatePanel3.Visible = true;
                gvConsolidado1.Visible = true;
                List<ClasificacionCartera> listadetalle = new List<ClasificacionCartera>();
                List<ClasificacionCartera> listacalifica = new List<ClasificacionCartera>();
                Xpinn.Cartera.Services.ClasificacionCarteraService clasificacionServicio = new Xpinn.Cartera.Services.ClasificacionCarteraService();
                listadetalle = clasificacionServicio.ListarDetalleAbrirCierre(txtFechaIni.Text, DropDownList1.SelectedValue, (Usuario)Session["Usuario"]);
                UpdatePanel3.Visible = true;
                gvConsolidado1.DataSource = listadetalle;
                gvConsolidado1.DataBind();
                DataTable dataTable = new DataTable();         
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CierresService.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        List<ClasificacionCartera> listadetalle = new List<ClasificacionCartera>();
        List<ClasificacionCartera> listacalifica = new List<ClasificacionCartera>();
        Xpinn.Cartera.Services.ClasificacionCarteraService clasificacionServicio = new Xpinn.Cartera.Services.ClasificacionCarteraService();
        listadetalle = clasificacionServicio.ListarDetalleAbrirCierre(txtFechaIni.Text, DropDownList1.SelectedValue, (Usuario)Session["Usuario"]);
        UpdatePanel3.Visible = true;
        gvConsolidado1.DataSource = listadetalle;
        gvConsolidado1.DataBind();
    }

    protected void gvdeducciones_RowDelete(object sender, GridViewDeleteEventArgs e)
    {
        Label1.Text = gvConsolidado1.Rows[e.RowIndex].Cells[1].Text;
        Label2.Text = gvConsolidado1.Rows[e.RowIndex].Cells[2].Text;
        Label3.Text = gvConsolidado1.Rows[e.RowIndex].Cells[3].Text;
        CheckBox cbAnulaComprobante = (CheckBox)gvConsolidado1.Rows[e.RowIndex].Cells[4].FindControl("cbAnulaComprobante");
        Label4.Text = cbAnulaComprobante.Checked ? "1" : "0";
        mpeNuevo.Show(); 
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        mpeNuevo.Hide();
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        if (Label1.Text == "&nbsp;")
            Label1.Text = "";

        Label2.Text = DropDownList1.SelectedValue;
        if (Label2.Text == "&nbsp;")
            Label2.Text = "";

        if (Label3.Text == "Definitivo")
            Label3.Text = "D";
        if (Label3.Text == "Pruebas")
            Label3.Text = "P";        
        if (Label3.Text == "&nbsp;")
            Label3.Text = "";

        Xpinn.Cartera.Services.ClasificacionCarteraService clasificacionServicio = new Xpinn.Cartera.Services.ClasificacionCarteraService();
        string error = "";
        error = clasificacionServicio.EliminarRegistroAbrirCierres(Label1.Text, Label2.Text, Label3.Text, Convert.ToInt32(Label4.Text), (Usuario)Session["Usuario"]);
        List<ClasificacionCartera> listadetalle = new List<ClasificacionCartera>();
        List<ClasificacionCartera> listacalifica = new List<ClasificacionCartera>();
        listadetalle = clasificacionServicio.ListarDetalleAbrirCierre(txtFechaIni.Text, DropDownList1.SelectedValue, (Usuario)Session["Usuario"]);
        UpdatePanel3.Visible = true;
        gvConsolidado1.DataSource = listadetalle;
        gvConsolidado1.DataBind();
        mpeNuevo.Hide();
        try { VerError(error); }
        catch { }
    }
    
    protected void gvConsolidado1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox cbAnulaComprobante = (CheckBox)e.Row.FindControl("cbAnulaComprobante");
            string cierre = e.Row.Cells[2].Text;
            if(cierre == "Provision General" || cierre == "Causacion" || cierre == "Provision" || cierre == "Cierre Cartera y Clasificacion")
            {
                cbAnulaComprobante.Enabled = true;
            }
        }
    }
}