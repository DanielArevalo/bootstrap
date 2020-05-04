using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reporte : GlobalWeb
{
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Request.QueryString["r"] != null)
                VisualizarOpciones(Request.QueryString["r"].ToString().Substring(13), "L");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("report", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["r"] != null)
            {

                Uri reportServer = new Uri(ConfigurationManager.AppSettings["reportServer"].ToString());
                rvVisor.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                rvVisor.ServerReport.ReportPath = Request.QueryString["r"].ToString();
                rvVisor.ServerReport.ReportServerUrl = reportServer;
            }
        }
    }
}