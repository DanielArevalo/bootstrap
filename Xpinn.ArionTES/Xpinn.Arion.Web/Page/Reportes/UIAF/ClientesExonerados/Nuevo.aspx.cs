using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.Reporteador.Entities;
using Xpinn.Reporteador.Services;

public partial class Nuevo : GlobalWeb
{

    
    TransaccionEfectivoService TransaccionEfectivoServicio = new TransaccionEfectivoService();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(TransaccionEfectivoServicio.CodigoPrograma2, "N");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TransaccionEfectivoServicio.GetType().Name + "N", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvConfAporte.ActiveViewIndex = 0;
                ucFecha.ToDateTime = DateTime.Now;
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }

    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Configuracion conf = new Configuracion();
        VerError("");
        if (rbTipoArchivo.SelectedItem != null)
        {
            TransaccionEfectivo pTran = new TransaccionEfectivo();
            // Determinando el tipo de archivo y el Separador
            if (rbTipoArchivo.SelectedIndex == 0)
                pTran.separador = ",";
            else if (rbTipoArchivo.SelectedIndex == 1)
                pTran.separador = "  ";
            else if (rbTipoArchivo.SelectedIndex == 2)
                pTran.separador = "|";

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

            // Validar si ya se ejecuto el proceso

            List<UIAF_Exonerados> listaArchivo = new List<UIAF_Exonerados>();
            string pError = "";
            try
            {
                listaArchivo = TransaccionEfectivoServicio.ListaUIAFExoneradosDate(ucFecha.ToDateTime, (Usuario)Session["Usuario"]);
                if (listaArchivo.Count == 0)
                {
                    VerError("No hay datos a partir de la fecha establecida");
                    return;
                }
                else
                    VerError("");

                if (pError != "")
                {
                    VerError(pError);
                    return;
                }
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }

            // ELIMINANDO ARCHIVOS GENERADOS
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
                    texto = "Fecha de corte final" + pTran.separador  + pTran.separador + pTran.separador + pTran.separador + pTran.separador +
                             pTran.separador + pTran.separador + pTran.separador +"";
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                    sw.WriteLine(texto);
                    sw.Close();

                    texto = "" + ucFecha.ToDateTime.ToString("yyyy-MM-dd") + "" + pTran.separador + pTran.separador + pTran.separador + pTran.separador + pTran.separador +
                     pTran.separador + pTran.separador + pTran.separador + "";
                    sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                    sw.WriteLine(texto);
                    sw.Close();

                    texto = "Subcuenta" + pTran.separador + " Fecha de Exoneración" + pTran.separador + "Tipo Identificación del Cliente" + pTran.separador + "Nro.  Identificación del Cliente" + pTran.separador + "1er. Apellido del Cliente" + pTran.separador +
                    "2do. Apellido del Cliente" + pTran.separador + "1er. Nombre del Cliente" + pTran.separador + "Otros Nombres del Cliente" + pTran.separador + "Razón Social del Cliente";
                    sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                    sw.WriteLine(texto);
                    sw.Close();
                    int consecutivo = 1;
                    foreach (UIAF_Exonerados item in listaArchivo)
                    {
                        texto = "" + consecutivo + "" + pTran.separador + "" + item.fecha_exoneracion.ToString("dd/MM/yyyy") + "" + pTran.separador + "" +  item.tipo_identificacion + "" + pTran.separador + "" + item.identificacion + "" + pTran.separador + "" + item.primer_apellido + "" + pTran.separador +
                                "" + item.segundo_apellido + "" + pTran.separador + "" + item.primer_nombre + "" + pTran.separador + "" + item.segundo_nombre + "" + pTran.separador + "" + item.razon_social ;
                        sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                        sw.WriteLine(texto);
                        sw.Close();
                        consecutivo += 1;
                    }
                texto = "Consecutivo" + pTran.separador + "Total de Registros" + pTran.separador;
                texto = texto + "" + pTran.separador + "" + pTran.separador + "" + pTran.separador + "" + pTran.separador + "" + pTran.separador + "" + pTran.separador + "";
                sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                sw.WriteLine(texto);
                sw.Close();

                
                texto = "0" + pTran.separador + "" + listaArchivo.Count + "" + pTran.separador + "";
                texto = texto + "" + pTran.separador + "" + pTran.separador + "" + pTran.separador + "" + pTran.separador + "" + pTran.separador + "" + pTran.separador + "";
                sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                sw.WriteLine(texto);
                sw.Close();

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
                        ExpExcelConfecoop Exportar = new ExpExcelConfecoop();
                        StreamReader strReader = File.OpenText(Server.MapPath("Archivos\\") + fic);

                        DataGrid gvLista = new DataGrid();
                        StringBuilder sb = new StringBuilder();
                        StringWriter sw = new StringWriter(sb);
                        HtmlTextWriter htw = new HtmlTextWriter(sw);
                        Page pagina = new Page();
                        dynamic form = new HtmlForm();

                        gvLista.AllowPaging = false;
                        gvLista.DataSource = Exportar.EjecutarExportacion(ref Fila, strReader, (Usuario)Session["usuario"]);
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
                        Response.AddHeader("Content-Disposition", "attachment;filename=" + fic);
                        Response.Charset = "UTF-8";
                        Response.ContentEncoding = Encoding.Default;
                        Response.Write(sb.ToString());
                        Response.End();
                    }
                    mvConfAporte.ActiveViewIndex = 1;
                }
                else
                {
                    VerError("No se genero el archivo para la fecha solicitado, Verifique los Datos");
                }
            }
            catch
            {
                VerError("Se generó un error al realizar el archivo. En la Fila " + Fila);
            }
        }
        else
        {
            VerError("Seleccione el Tipo de Archivo");
        }
    }



}