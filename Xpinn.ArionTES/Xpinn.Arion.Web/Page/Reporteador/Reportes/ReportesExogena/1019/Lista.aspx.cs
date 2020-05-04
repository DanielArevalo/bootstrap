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

public partial class Page_Reporteador_Reportes_ReportesExogena_1019_Lista : GlobalWeb
{
    ExogenaReportService objReporteService = new ExogenaReportService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(objReporteService.CodigoProgramaReportelista1019, "L");
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
            string s = "";

            if (rbTipoArchivo.SelectedItem != null)
            {

                ExogenaReport pExogena = new ExogenaReport();
                // Determinando el tipo de archivo y el Separador
                if (rbTipoArchivo.SelectedIndex == 0)
                    pExogena.separador = ";";
                else if (rbTipoArchivo.SelectedIndex == 1)
                    pExogena.separador = "  ";
                else if (rbTipoArchivo.SelectedIndex == 2)
                    pExogena.separador = "|";

                // Determinando el nombre del archivo
                string fic = "";
                if (txtArchivo.Text != "")
                {
                    if (rbTipoArchivo.SelectedIndex == 0)
                    {
                        fic = txtArchivo.Text.Trim().Contains(".csv") ? txtArchivo.Text : txtArchivo.Text + ".csv";
                    }
                    else if (rbTipoArchivo.SelectedIndex == 1)
                    {
                        fic = txtArchivo.Text.Trim().Contains(".txt") ? txtArchivo.Text : txtArchivo.Text + ".txt";
                    }
                    else if (rbTipoArchivo.SelectedIndex == 2)
                    {
                        fic = txtArchivo.Text.Trim().Contains(".xls") ? txtArchivo.Text : txtArchivo.Text + ".xls";
                    }
                }
                else
                {
                    VerError("Ingrese el Nombre del archivo a Generar");
                    return;
                }

                string texto = "";

                List<ExogenaReport> listaArchivo = new List<ExogenaReport>();
                //DataTable dtResultado = objReporteService.FormatoAhorros(Convert.ToInt32(txtAño.Text), (Usuario)Session["Usuario"]);
                string pError = "";
                try
                {
                    listaArchivo = objReporteService.FormatoAhorros(pExogena, Convert.ToInt32(txtAño.Text), (Usuario)Session["Usuario"], ref pError, 0);
                    if (pError != "")
                    {
                        VerError(pError);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    VerError(ex.Message);
                    s = ex.ToString();
                }




                //Pendiente

                //string numero_cuenta = "";

                //for (int index = 0; index < dtResultado.Rows.Count - 1; index++)
                //{


                //    numero_cuenta  = Convert.ToString(dtResultado.Rows[index]["NUMERO_CUENTA"]);
                //    int resultado = objReporteService.Validar_Saldo_Mensual(numero_cuenta, Convert.ToInt32(txtAño.Text), (Usuario)Session["Usuario"]);
                //    Int64 Saldo_Total = objReporteService.Saldo_Total(numero_cuenta, Convert.ToInt32(txtAño.Text), (Usuario)Session["Usuario"]);
                //    if (resultado==0 || Saldo_Total<1000000)
                //    {
                //        dtResultado.Rows.RemoveAt(index);
                //    }

                //}
              
                
                try
                {
                    string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Archivos\\"));
                    foreach (string ficheroActual in ficherosCarpeta)
                        File.Delete(ficheroActual);
                }
                catch
                { }

                try
                {
                    foreach (ExogenaReport item in listaArchivo)
                    {
                        texto = item.linea;
                        System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                        sw.WriteLine(texto);
                        sw.Close();
                    }
                }
                catch (Exception ex)
                {
                    VerError(ex.Message);
                }

                
                Int32 Fila = 0;
                try
                {
                    // Copiar el archivo al cliente        
                    if (File.Exists(Server.MapPath("Archivos\\") + fic))
                    {
                        if (rbTipoArchivo.SelectedItem.Text == "CSV" || rbTipoArchivo.SelectedItem.Text == "TEXTO") // TEXTO O CSV
                        {
                            System.IO.StreamReader sr;
                            sr = File.OpenText(Server.MapPath("Archivos\\") + fic);
                            texto = sr.ReadToEnd();
                            sr.Close();
                            HttpContext.Current.Response.ClearContent();
                            HttpContext.Current.Response.ClearHeaders();
                            HttpContext.Current.Response.ContentType = "text/plain";
                            HttpContext.Current.Response.Write(texto);
                            HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + fic);
                            HttpContext.Current.Response.Flush();
                            File.Delete(Server.MapPath("Archivos\\") + fic);
                            HttpContext.Current.Response.End();
                        }
                        else
                        {
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                for (int i = 1; i <= 12; i++)
                                {
                                    int totaldias = DateTime.DaysInMonth(Convert.ToInt32(txtAño.Text), i);
                                    string inicial = "01/" + i + "/" + txtAño.Text;
                                    string final = totaldias + "/" + i + "/" + txtAño.Text;

                                    try
                                    {
                                        listaArchivo = objReporteService.FormatoAhorros1019(pExogena, inicial, final, (Usuario)Session["Usuario"], ref pError, 0);
                                        if (pError != "")
                                        {
                                            VerError(pError);
                                            return;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        VerError(ex.Message);
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
                                        catch (Exception ex)
                                        { }

                                        foreach (ExogenaReport item in listaArchivo)
                                        {
                                            texto = item.linea;
                                            System.IO.StreamWriter sw1 = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                                            sw1.WriteLine(texto);
                                            sw1.Close();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        VerError(ex.Message);
                                    }

                                    ExpExcelConfecoop Exportar = new ExpExcelConfecoop();
                                    StreamReader strReader = File.OpenText(Server.MapPath("Archivos\\") + fic);

                                    DataGrid gvLista = new DataGrid();
                                    StringBuilder sb = new StringBuilder();
                                    StringWriter sw = new StringWriter(sb);
                                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                                    Page pagina = new Page();
                                    dynamic form = new HtmlForm();

                                    //gvLista.AllowPaging = false;
                                    //gvLista.DataSource = Exportar.EjecutarExportacion(ref Fila, strReader, (Usuario)Session["usuario"]);
                                    //gvLista.DataBind();
                                    //gvLista.EnableViewState = false;

                                    //pagina.EnableEventValidation = false;
                                    //pagina.DesignerInitialize();
                                    //pagina.Controls.Add(form);
                                    //form.Controls.Add(gvLista);
                                    //pagina.RenderControl(htw);
                                    //Response.Clear();
                                    //Response.Buffer = true;
                                    //Response.ContentType = "application/vnd.ms-excel";
                                    //Response.AddHeader("Content-Disposition", "attachment;filename=" + fic);
                                    //Response.Charset = "UTF-8";
                                    //Response.ContentEncoding = Encoding.Default;
                                    //Response.Write(sb.ToString());
                                    //Response.End();

                                    DateTime Fecha = Convert.ToDateTime(inicial);
                                    string Nombre_mes = MonthName(Fecha.Month);
                                    DataTable dt = new DataTable(i.ToString());
                                 
                                    dt= (DataTable)Exportar.EjecutarExportacion(ref Fila, strReader, (Usuario)Session["usuario"]);
                                    dt.TableName = Nombre_mes;

                                    wb.Worksheets.Add(dt);
                                    strReader.Close();
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
                    }
                        //mvConfAporte.ActiveViewIndex = 1;
                
                    else
                    {
                        VerError("No se genero el archivo para la fecha solicitado, Verifique los Datos"+s);
                    }
                }
                catch (Exception es)
                {
                    VerError("Se generó un error al realizar el archivo. En la Fila " + Fila + "  ..."+es);
                }
            }
            else
            {
                VerError("Seleccione el Tipo de Archivo");
            }
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
}