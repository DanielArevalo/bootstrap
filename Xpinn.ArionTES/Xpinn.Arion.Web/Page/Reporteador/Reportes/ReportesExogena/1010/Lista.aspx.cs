using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Reporteador.Services;
using Xpinn.Reporteador.Entities;
using System.Data;
using Xpinn.Util;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class Page_Reporteador_Reportes_ReportesExogena_1010_Lista : GlobalWeb
{
    ExogenaReportService objReporteService = new ExogenaReportService();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(objReporteService.CodigoProgramaReportelista1010, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoExportar += btnExportar_Click;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        try
        {
         

            DataTable dtResultado = objReporteService.FormatoAporte(Convert.ToInt32(txtAño.Text), (Usuario)Session["Usuario"]);

            if (dtResultado.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                GridView dg = new GridView();
                dg.AllowPaging = false;
                dg.DataSource = dtResultado;
                dg.DataBind();
                dg.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(dg);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + txtArchivo.Text + ".xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                //Response.End();
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
                VerError("No se encontrarón registros para exportar");
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }
}