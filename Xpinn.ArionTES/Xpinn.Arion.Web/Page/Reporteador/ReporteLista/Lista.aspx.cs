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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Reporteador.Services;
using Xpinn.Reporteador.Entities;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    Xpinn.Reporteador.Services.ReporteService ParametroService = new ReporteService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoExportar += btnExportar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParametroService.CodigoProgramaReportelista, "Page_PreInit", ex);
        }
    }


   

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParametroService.CodigoProgramaReportelista, "Page_Load", ex);
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

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParametroService.CodigoProgramaReportelista, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();

        Session[ParametroService.CodigoProgramaReportelista + ".id"] = id;       
        Navegar("~/Page/Reporteador/ReporteLista/Nuevo.aspx");
    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string id = gvLista.DataKeys[e.RowIndex].Values[0].ToString();
            Session["ID"] = id;
           ParametroService.EliminarReporteLista(Convert.ToInt64(id), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch 
        {
            VerError("no puede borar el Reporte por que ya hay personas con este Reporte");
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParametroService.CodigoProgramaReportelista, "btnContinuarMen_Click", ex);
        }
    }

    
    private void Actualizar()
    {
        try
        {

            List<Xpinn.Reporteador.Entities.Lista> lstConsulta = new List<Xpinn.Reporteador.Entities.Lista>();
            string filtro = obtFiltro();
            Xpinn.Reporteador.Entities.Lista orden = new Xpinn.Reporteador.Entities.Lista();


            lstConsulta = ParametroService.ListarReporteLista(filtro, orden, (Usuario)Session["usuario"]);

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
                Session["DTPARAMETRO"] = lstConsulta;
            }
            else
            {
                panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
                Session["DTPARAMETRO"] = null;
            }
            Session.Add(ParametroService.CodigoProgramaReportelista + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParametroService.CodigoProgramaReportelista, "Actualizar", ex);
        }
    }



    private string obtFiltro()
    {
        String filtro = String.Empty;
        filtro = " where 1=1 ";
        if (txtCodigo.Text.Trim() != "")
            filtro += " and idlista = " + txtCodigo.Text;       
        if (txtDescripcion.Text.Trim() != "")
            filtro += " and descripcion like '%" + txtDescripcion.Text.Trim() + "%'";
        
        return filtro;
    }

    private void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0 && Session["DTPARAMETRO"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.Columns[0].Visible = false;
            gvLista.Columns[1].Visible = false;            
            gvLista.AllowPaging = false;
            gvLista.DataSource = Session["DTPARAMETRO"];
            gvLista.DataBind();
            gvLista.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvLista);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=Reportelista.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
        {
            VerError("No existen datos, genere la consulta");
        }
    }

}