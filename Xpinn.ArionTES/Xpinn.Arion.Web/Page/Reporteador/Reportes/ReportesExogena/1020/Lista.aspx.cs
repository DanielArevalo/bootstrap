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
using ClosedXML.Excel;

public partial class Page_Reporteador_Reportes_ReportesExogena_1020_Lista : GlobalWeb
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
            using (XLWorkbook wb = new XLWorkbook())
            {

                for (int i = 1; i <= 12; i++)
            {
                    int totaldias = DateTime.DaysInMonth(Convert.ToInt32(txtAño.Text),i);
                string inicial = "01/" + i + "/" + txtAño.Text;
                string final = totaldias+"/" + i + "/" + txtAño.Text;


                DataTable dtResultado = objReporteService.FormatoCDAT(inicial,final, (Usuario)Session["Usuario"]);

                    

             

                    //Add DataTable as Worksheet.
                    wb.Worksheets.Add(dtResultado);


                  
                
            }
            //Export the Excel file.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename="+ txtArchivo.Text + ".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
   
}
}