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
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
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
                OficinaService oficinaService = new OficinaService();
                Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
                ddlOficina.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);
                ddlOficina.DataTextField = "Nombre";
                ddlOficina.DataValueField = "Codigo";
                ddlOficina.DataBind();
                ddlOficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));       
              
                Label3.Visible = true;
                ucFecha.Visible = true;
                      
                ucFecha.ToDateTime = DateTime.Today;
                ucFecha0.ToDateTime = DateTime.Today;

                btnExportarExcel.Visible = false;
            
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
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicegrantias.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
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
            Response.AddHeader("Content-Disposition", "attachment;filename=Reclamaciones.xls");
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
        List<GarantiasComunitarias> listgarantias = new List<GarantiasComunitarias>();

        listgarantias = servicegrantias.consultargarantiascomunitariasReclamaciones(ucFecha.ToDateTime.ToString("dd/MM/yyyy"), ucFecha0.ToDateTime.ToString("dd/MM/yyyy"), Convert.ToInt32(ddlOficina.SelectedValue), (Usuario)Session["Usuario"]);
        gvMovGeneral.DataSource = listgarantias;
        gvMovGeneral.DataBind();
        btnExportarExcel.Visible = true;
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        int encabezado = 0;

        GarantiasComunitarias reclamaciones = new GarantiasComunitarias();

        foreach (GridViewRow fila in gvMovGeneral.Rows)
        {         
            System.Web.UI.WebControls.DropDownList ddl_Resultado = (System.Web.UI.WebControls.DropDownList)fila.Cells[9].FindControl("ddlreclamacion");
            if (ddl_Resultado.SelectedValue == "1" || ddl_Resultado.SelectedValue == "2")
            {
                reclamaciones.RECLAMACION = ddl_Resultado.SelectedValue;
                reclamaciones.NUMERO_RADICACION = fila.Cells[0].Text;
                reclamaciones.NITENTIDAD = fila.Cells[1].Text;
                reclamaciones.IDENTIFICACION = fila.Cells[2].Text;
                reclamaciones.FECHAPROXPAGO = fila.Cells[3].Text;
                reclamaciones.DIASMORA = fila.Cells[4].Text;
                reclamaciones.CAPITAL = Convert.ToDouble(fila.Cells[5].Text);
                reclamaciones.INT_CORRIENTES = Convert.ToDouble(fila.Cells[6].Text);
                reclamaciones.INT_MORA = Convert.ToDouble(fila.Cells[7].Text);
                reclamaciones.CUOTAS_RECLAMAR = fila.Cells[8].Text;
                servicegrantias.CrearReclamacion(reclamaciones, (Usuario)Session["usuario"], ucFecha.ToDateTime.ToString("dd/MM/yyyy"), encabezado);
                encabezado = encabezado + 1;
                ddl_Resultado.Enabled = false;                
            }
        }
        Navegar(Pagina.Lista);
    }

}