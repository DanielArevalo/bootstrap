using System;
using System.IO;
using System.Web.UI;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using Xpinn.Aportes.Services;
using Xpinn.Util;
using System.Web;
using Xpinn.Reporteador.Services;

public partial class Lista : GlobalWeb
{
    ReporteService objReporteService = new ReporteService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(objReporteService.CodigoProgramaReporteClientesGarantias, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoExportar += btnExportar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objReporteService.CodigoProgramaReporteClientesGarantias, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

        }
    }

    private void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        try
        {
            if (ucFechaCorte.Text == "")
                throw new Exception("Ingrese la fecha de corte");
            
            DataTable dtAfiliaciones = objReporteService.ConsultarAfiliados_GarantiasComunitarias(DateTime.Parse(ucFechaCorte.Text), (Usuario)Session["Usuario"]);
            if (dtAfiliaciones.Rows.Count > 0)
            {
                dtAfiliaciones.Columns["MUNICIPIO_RESIDENCIA"].DataType = System.Type.GetType("System.String");

                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                GridView dg = new GridView();
                dg.AllowPaging = false;
                dg.DataSource = dtAfiliaciones;
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
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                //Response.End();
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