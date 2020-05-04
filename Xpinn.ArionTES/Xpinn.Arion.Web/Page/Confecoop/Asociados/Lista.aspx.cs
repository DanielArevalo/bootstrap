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
using System.Web.Script.Serialization;


public partial class Lista : GlobalWeb
{
    ConfecoopService pucService = new ConfecoopService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(pucService.CodigoProgramaAsoci, "L");       
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pucService.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {       
            if (!IsPostBack)
            {
                mvActivosFijos.ActiveViewIndex = 0;
                  
                CargarFecha();
                rbTipoArchivo.SelectedIndex = 0;
             }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pucService.GetType().Name + "D", "Page_Load", ex);
        }
    }


    protected void CargarFecha()
    { 
        PUC pPuc = new PUC();
        string tipo, estado;
        tipo = "P";
        estado = "D";
        ddlFecha.DataSource = pucService.ListarFechaCierreGLOBAL(tipo,estado, (Usuario)Session["usuario"]);
        ddlFecha.DataTextField = "fecha";
        ddlFecha.DataTextFormatString = "{0:" + gFormatoFecha + "}";
        ddlFecha.DataValueField = "fecha";        
        ddlFecha.DataBind();
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
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
            else if (rbTipoArchivo.SelectedIndex == 1)
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
                listaArchivo = pucService.ListarTEMP_SUPER_ASOCIADOS(pPuc, ref pError, (Usuario)Session["usuario"]);
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
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
                
            try
            {
                foreach (PUC item in listaArchivo)
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


            Int32 FILA = 0;
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
                        // File.Delete(Server.MapPath("Archivos\\") + fic);
                        HttpContext.Current.Response.End();
                    }
                    else
                    {
                        string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                        ExpExcelConfecoop Exportar = new ExpExcelConfecoop();
                        StreamReader strReader = File.OpenText(Server.MapPath("Archivos\\") + fic);
                        
                        GridView1.DataSource = Exportar.EjecutarExportacion(ref FILA, strReader, (Usuario)Session["usuario"]);
                        GridView1.DataBind();
                        
                        Response.ClearContent();
                        Response.ContentType = "application/vnd.ms-excel";
                        Response.AddHeader("Content-Disposition", "attachment;filename=" + fic);
                        StringWriter sw = new StringWriter();
                        HtmlTextWriter htw = new HtmlTextWriter(sw);

                        GridView1.RenderControl(htw);
                        Response.Write(style);
                        Response.Write(sw.ToString());
                        Response.End();
                    }
                    mvActivosFijos.ActiveViewIndex = 1;
                }
                else
                {
                    VerError("No se genero el archivo para la fecha solicitado, Verifique los Datos");
                }
            }
            catch(Exception ec)
            {
                VerError("Se generó un error al realizar el archivo. En la Fila " + FILA+ec.Message);
            }
        }
        else
        {
            VerError("Seleccione el Tipo de Archivo");
        }
        //if (Response.Buffer)
        //{
        //    VerError("archivo cargado correctamente");
        //    return;  
        //}

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("class", "textmode");
            e.Row.Cells[6].Attributes.Add("class", "textmode");
            e.Row.Cells[11].Attributes.Add("class","textmode");
            e.Row.Cells[27].Attributes.Add("class", "textmode");
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

}