﻿using System;
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
using Xpinn.Confecoop.Entities;
using Xpinn.Confecoop.Services;

public partial class Lista : GlobalWeb
{
    ConfecoopService cofecoopService = new ConfecoopService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(cofecoopService.CodigoProgramaTerceros, "L");
            Site toolBar = (Site)Master;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cofecoopService.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvActivosFijos.ActiveViewIndex = 0;
                CargarFecha("C");
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cofecoopService.GetType().Name + "D", "Page_Load", ex);
        }
    }


    void CargarFecha(string pTipo)
    {
        string pFecAnterior = ddlFecha.SelectedItem != null ? ddlFecha.SelectedValue : null;
        PUC pPuc = new PUC();
        string tipo, estado;
        tipo = pTipo;
        estado = "D";
        ddlFecha.DataSource = cofecoopService.ListarFechaCierreGLOBAL(tipo,estado, (Usuario)Session["usuario"]);
        ddlFecha.DataTextField = "fecha";
        ddlFecha.DataTextFormatString = "{0:" + gFormatoFecha + "}";
        ddlFecha.DataValueField = "fecha";        
        ddlFecha.DataBind();
        try
        {
            if (pFecAnterior != null)
                ddlFecha.SelectedValue = pFecAnterior;
        }
        catch
        {
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (ddlFecha.SelectedItem == null)
        {
            VerError("No existen fechas para seleccionar, verifique los datos");
            return;
        }
        if (rbTipoArchivo.SelectedItem != null)
        {
            PUC pPuc = new PUC();
            // Determinando el tipo de archivo y el Separador
            if (rbTipoArchivo.SelectedIndex == 0)
                pPuc.separador = ";";
            else if(rbTipoArchivo.SelectedIndex == 1)
                pPuc.separador = "  ";
            else if (rbTipoArchivo.SelectedIndex == 2)
                pPuc.separador = "|";

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

            List<PUC> listaArchivo = new List<PUC>();

            pPuc.fecha = Convert.ToDateTime(ddlFecha.SelectedValue);
            string pError = "";
            try
            {
                listaArchivo = cofecoopService.ListarTEMP_SUPERSOLI_TERCEROS(pPuc, ref pError, (Usuario)Session["Usuario"]);
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
                foreach (PUC item in listaArchivo)
                {
                    texto = item.linea.Replace(".",",");
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                    sw.WriteLine(texto);
                    sw.Close();
                }
            }
            catch
            {
                
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
                        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fic);
                        HttpContext.Current.Response.Flush();
                        File.Delete(Server.MapPath("Archivos\\") + fic);
                        HttpContext.Current.Response.End();
                    }
                    else
                    {
                        ExpExcelConfecoop Exportar = new ExpExcelConfecoop();
                        StreamReader strReader = File.OpenText(Server.MapPath("Archivos\\") + fic);
                        /*
                        // Abrir el archivo para lectura
                        string readLine;
                        StreamReader strReader = File.OpenText(Server.MapPath("Archivos\\") + fic);
                        // Leer línea de títulos
                        readLine = strReader.ReadLine();
                        string[] arrayTitulos = readLine.Split('|');
                        System.Data.DataTable table = new System.Data.DataTable();
                        for (int i=0; i < arrayTitulos.Length; i++)
                        {
                            table.Columns.Add(arrayTitulos[i].ToString());                        
                        }
                        while (strReader.Peek() >= 0)
                        {
                            readLine = strReader.ReadLine();
                            string[] arrayline = readLine.Split('|');

                            int cont = 0;
                            System.Data.DataRow dr = table.NewRow();
                            foreach (string item in arrayline)
                            {                                                        
                                dr[cont] = " " + item.Trim();
                                cont++;
                            }
                            table.Rows.Add(dr);
                        }*/

                        DataGrid gvLista = new DataGrid();
                        StringBuilder sb = new StringBuilder();
                        StringWriter sw = new StringWriter(sb);
                        HtmlTextWriter htw = new HtmlTextWriter(sw);
                        Page pagina = new Page();
                        dynamic form = new HtmlForm();

                        gvLista.AllowPaging = false;
                        gvLista.DataSource = Exportar.EjecutarExportacion(ref Fila,strReader, (Usuario)Session["usuario"]);
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

                    mvActivosFijos.ActiveViewIndex = 1;
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