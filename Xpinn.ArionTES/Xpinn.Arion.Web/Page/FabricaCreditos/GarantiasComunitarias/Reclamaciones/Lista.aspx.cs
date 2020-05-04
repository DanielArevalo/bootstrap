using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;


public partial class garantiascomunitarias : GlobalWeb
{
    GarantiasComunitariasService servicegrantias = new GarantiasComunitariasService(); 


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(servicegrantias.CodigoProgramaReclamaciones, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicegrantias.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {                               
                ucFecha.Visible = true;          
                ucFecha.ToDateTime = DateTime.Today;
                               
             }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicegrantias.GetType().Name + "D", "Page_Load", ex);
        }
    }

    protected void gvMovGeneral_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvMovGeneral.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicegrantias.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }


    /// <summary>
    /// Mostrar los datos del crèdito junto con los movimientos
    /// </summary>
    private void Actualizar()
    {
      
    }
    

    protected void btnExportarExcel_Click(object sender, EventArgs e)
    {
        if (gvMovGeneral.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvMovGeneral.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvMovGeneral);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");

    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        string fecha = "";
        if (ucFecha.TieneDatos)
            fecha = ucFecha.ToDateTime.ToString("dd/MM/yyyy");
        List<GarantiasComunitarias> listgarantias = new List<GarantiasComunitarias>();
        listgarantias = servicegrantias.consultargarantiasconsultarReclamacion(fecha, (Usuario)Session["Usuario"]);
        gvMovGeneral.DataSource = listgarantias;
        gvMovGeneral.DataBind();
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);      
    }



    protected void gvMovGeneral_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Session["numero_reclamacion"] = gvMovGeneral.Rows[e.NewEditIndex].Cells[1].Text;
        Session["fecha_reclamacion"] = gvMovGeneral.Rows[e.NewEditIndex].Cells[3].Text;
        Navegar(Pagina.Detalle);       
    }

}