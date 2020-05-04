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
using System.Globalization;

public partial class Page_Reporteador_Reportes_ReportesExogena_Lista : GlobalWeb
{
    ExogenaReportService objReporteService = new ExogenaReportService();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(objReporteService.CodigoProgramaInformesDian, "L");
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
        if (!IsPostBack)
        {
            DateTime fecha = DateTime.Now;
            txtAño.Text = (fecha.Year - 1).ToString();
            //rbTipoArchivo.SelectedIndex = 0;
            PoblarListaFromatosDIAN(ddlTipoFormato);
        }
    }
    private void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlTipoFormato.SelectedValue == "1")
        {
            Formato1019();
        }
        if (ddlTipoFormato.SelectedValue == "2")
        {
            Formato1020();
        }
        if (ddlTipoFormato.SelectedValue == "3")
        {
            Formato1010();
        }
        if (ddlTipoFormato.SelectedValue == "4")
        {
            Formato1008();
        }
        if (ddlTipoFormato.SelectedValue == "5")
        {
            Formato1001();
        }
        if (ddlTipoFormato.SelectedValue == "6")
        {
            Formato1007();
        }
        if (ddlTipoFormato.SelectedValue == "7")
        {
            Formato1009();
        }
        if (ddlTipoFormato.SelectedValue == "8")
        {
            Formato1026();
        }
    }

    public void Formato1019()
    {
        VerError("");
        try
        {
            string s = "";

            ExogenaReport pExogena = new ExogenaReport();

            // Determinando el nombre del archivo
            string fic = "";
            if (txtArchivo.Text != "")
            {
                fic = txtArchivo.Text.Trim().Contains(".xls") ? txtArchivo.Text : txtArchivo.Text + ".xls";
            }
            else
            {
                VerError("Ingrese el Nombre del archivo a Generar");
                return;
            }

            string texto = "";

            List<ExogenaReport> listaArchivo = new List<ExogenaReport>();
            string pError = "";


            Int32 Fila = 0;

            try
            {                
                // Si el archivo ya existe entonces borrarlo
                if (File.Exists(Server.MapPath("Archivos\\") + fic))
                    File.Delete(Server.MapPath("Archivos\\") + fic);

                using (XLWorkbook wb = new XLWorkbook())
                {
                    bool bTieneDatos = false;
                    DataTable dt = null;
                    HtmlTextWriter htw = null;
                    for (int i = 1; i <= 12; i++)
                    {
                        int totaldias = DateTime.DaysInMonth(Convert.ToInt32(txtAño.Text), i);
                        string inicial = "01/" + i + "/" + txtAño.Text;
                        string final = totaldias + "/" + i + "/" + txtAño.Text;
                        lblMensaje.Text = "1.Generando el archivo " + final;

                        try
                        {
                            listaArchivo = objReporteService.FormatoAhorros1019(pExogena, inicial, final, (Usuario)Session["Usuario"], ref pError, 1/* Convert.ToInt16(rdbCuantias.SelectedItem.Value)*/);
                            if (listaArchivo.Count == 0)
                            {
                                VerError("No se encontraron datos para este año.");
                                return;
                            }
                            if (pError != "")
                            {
                                VerError(pError);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            VerError("--->" + ex.Message);
                            s = ex.ToString();
                        }

                        try
                        {
                            try
                            {
                                string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Archivos\\"));
                                foreach (string ficheroActual in ficherosCarpeta)
                                    File.Delete(ficheroActual);
                            }
                            catch(Exception ex)
                            {
                                VerError(ex.Message);
                            }
                            lblMensaje.Text = "2.Escribir en el archivo " + listaArchivo.Count();
                            if (listaArchivo.Count > 0)
                            {
                                foreach (ExogenaReport item in listaArchivo)
                                {
                                    texto = item.linea;
                                    System.IO.StreamWriter sw1 = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                                    sw1.WriteLine(texto);
                                    sw1.Close();
                                    bTieneDatos = true;
                                }
                            }else
                            {
                                VerError("No se encontraron datos para este año.");
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            VerError("-->" + ex.Message);
                        }

                        if (bTieneDatos)
                        {
                            lblMensaje.Text = "3. Terminar el archivo " + Server.MapPath("Archivos\\") + fic;
                            ExpExcelConfecoop Exportar = new ExpExcelConfecoop();
                            StreamReader strReader = File.OpenText(Server.MapPath("Archivos\\") + fic);

                            lblMensaje.Text = "4. Generar descarga arhcivo ";
                            DataGrid gvLista = new DataGrid();
                            StringBuilder sb = new StringBuilder();
                            StringWriter sw = new StringWriter(sb);
                            htw = new HtmlTextWriter(sw);
                            Page pagina = new Page();
                            dynamic form = new HtmlForm();

                            lblMensaje.Text = "5. Crear la hoja " + Fila;
                            DateTime Fecha = Convert.ToDateTime(inicial);
                            string Nombre_mes = MonthName(Fecha.Month);
                            dt = new DataTable(i.ToString());
                       
                            lblMensaje.Text = "6. Exportar archivo " + Fila;
                            dt = (DataTable)Exportar.EjecutarExportacion(ref Fila, strReader, (Usuario)Session["usuario"]);
                            dt.TableName = Nombre_mes;
                            dt.Namespace = "textmode";

                            wb.Worksheets.Add(dt);
                            strReader.Close();
                        }
                        else
                        {
                            VerError("No hay datos para generar");
                        }
                    }


                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + txtArchivo.Text + ".xlsx");

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
                VerError("---->" + ex.Message);
            }
        }
        catch (Exception ex)
        {
            VerError("---->" + ex.Message);
        }
    }

    /// <summary>
    /// Generando formato 1020-INFORMACION INVERSIONES EN CDAT
    /// </summary>
    public void Formato1020()
    {
        VerError("");
        try
        {
            using (XLWorkbook wb = new XLWorkbook())
            {

                for (int i = 1; i <= 12; i++)
                {
                    int totaldias = DateTime.DaysInMonth(Convert.ToInt32(txtAño.Text), i);
                    string inicial = "01/" + i.ToString("00") + "/" + txtAño.Text;
                    string final = totaldias + "/" + i.ToString("00") + "/" + txtAño.Text;

                    // Cargando datos de los CDATS para el mes correspondiente
                    DataTable dtResultado = objReporteService.FormatoCDAT(inicial, final, (Usuario)Session["Usuario"]);
                  
                    wb.Worksheets.Add(dtResultado);

                }
                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + txtArchivo.Text + ".xlsx");
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

    public void Formato1010()
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
                dg.Attributes.Add("OnRowDataBound", "GridView1_RowDataBound");
              
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
    public void Formato1008()
    {
        VerError("");
        try
        {
            DataTable dtResultado = objReporteService.Formato1008(Convert.ToInt32(txtAño.Text), (Usuario)Session["Usuario"]);
            if (dtResultado == null)
            {
                VerError("No se pudieron generar los datos del reporte");
                return;
            }
            if (dtResultado.Columns != null)
                if (dtResultado.Columns.Count >= 11)
                    dtResultado.Columns[11].DataType = System.Type.GetType("System.String");
        
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

    public void Formato1001()
    {
        VerError("");
        try
        {
            
            DataTable dtResultado = objReporteService.Formato1001(Convert.ToInt32(txtAño.Text),1/* Convert.ToInt16(rdbCuantias.SelectedItem.Value)*/, (Usuario)Session["Usuario"]);
         
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
    public void Formato1026()
    {
        VerError("");
        try
        {


            DataTable dtResultado = objReporteService.Formato1026(Convert.ToInt32(txtAño.Text), (Usuario)Session["Usuario"]);


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

    /// <summary>
    /// Generar información del concepto 1007-PAGOS O ABONOS EN CUENTA Y RETENCIONES PRACTICADAS los conceptos son:
    /// 4001	Ingresos Brutos Operacionales					
    /// 4002	Ingresos No operacionales Diferentes de Interes y Rendimientos Financieros					
    /// 4003	Ingresos Por Intereses y Rendimientos Financieros
    /// Intereses Ingresos Cartera y demas cuentas del Codigo Contable 4 Base 500000 , maneja cuantias menores
    /// </summary>
    public void Formato1007()
    {
        VerError("");
        try
        {
            // Realizar la consulta y obtener los datos
            DataTable dtResultado = objReporteService.Formato1007(Convert.ToInt32(txtAño.Text), (Usuario)Session["Usuario"]);

            // Exportar los datos a EXCEL
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
    public void Formato1009()
    {
        VerError("");
        try
        {


            DataTable dtResultado = objReporteService.Formato1009(Convert.ToInt32(txtAño.Text), (Usuario)Session["Usuario"]);


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
    public string MonthName(int month)
    {
        DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
        return dtinfo.GetMonthName(month);
    }
    protected void ddlTipoFormato_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlTipoFormato.SelectedValue == "1" || ddlTipoFormato.SelectedValue == "5")
        //{
        //    rbTipoArchivo.Visible = true;
        //    rdbCuantias.Visible = true;
        //}
        //else
        //{
        //    rbTipoArchivo.Visible = false;
        //    rdbCuantias.Visible = false;
        //}
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[11].Attributes.Add("class", "textmode");
        }
    }
}