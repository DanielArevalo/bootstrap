using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;

public partial class ctlArchivoGiros : System.Web.UI.UserControl
{
    List<Xpinn.Tesoreria.Entities.Giro> lstGiros = new List<Xpinn.Tesoreria.Entities.Giro>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfCodigo.Value = null;
            panelArchivoGiros.Visible = false;
        }
    }

    protected void bntCerrar_Click(object sender, ImageClickEventArgs e)
    {
        panelArchivoGiros.Visible = false;
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {

    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        panelArchivoGiros.Visible = false;
    }

    protected void GenerarArchivoGiros()
    {
        Xpinn.Tesoreria.Services.ACHplantillaService estructuraServicio = new Xpinn.Tesoreria.Services.ACHplantillaService();
        Xpinn.Tesoreria.Entities.ACHplantilla estructura = new Xpinn.Tesoreria.Entities.ACHplantilla();


        //if (estructura.tipo_archivo == 0)
        //{
        //    //ARCHIVO EXCEL
        //    DataGrid dg = new DataGrid();
        //    dg.AllowPaging = false;
        //    dg.DataSource = lstConsulta;
        //    dg.DataBind();

        //    System.Web.HttpContext.Current.Response.Clear();
        //    System.Web.HttpContext.Current.Response.Buffer = true;
        //    System.Web.HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
        //    System.Web.HttpContext.Current.Response.Charset = "";
        //    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition",
        //      "attachment; filename=" + txtNombreArchivo.Text);
        //    System.Web.HttpContext.Current.Response.ContentType =
        //      "application/vnd.ms-excel";
        //    System.IO.StringWriter stringWriter = new System.IO.StringWriter();
        //    System.Web.UI.HtmlTextWriter htmlTextWriter =
        //      new System.Web.UI.HtmlTextWriter(stringWriter);
        //    dg.RenderControl(htmlTextWriter);
        //    System.Web.HttpContext.Current.Response.Write(stringWriter.ToString());
        //    System.Web.HttpContext.Current.Response.End();
        //}
        //else
        //{
        //    //GENERAR EL ARCHIVO PLANO
        //    string texto = "", fic = "";
        //    if (txtNombreArchivo.Text != "")
        //        if (txtNombreArchivo.Text.ToLower().Contains(".txt") == true)
        //            fic = txtNombreArchivo.Text;
        //        else
        //            fic = txtNombreArchivo.Text + ".txt";
        //    if (File.Exists(Server.MapPath("Archivos\\") + fic))
        //        File.Delete(Server.MapPath("Archivos\\") + fic);
        //    try
        //    {
        //        //Guarda los Datos a la ruta especificada
        //        foreach (string item in lstConsulta)
        //        {
        //            texto = item;
        //            System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
        //            sw.WriteLine(texto);
        //            sw.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMensj.Text = ex.Message;
        //        return;
        //    }

        //    if (lstConsulta.Count > 0 && File.Exists(Server.MapPath("Archivos\\") + fic))
        //    {
        //        System.IO.StreamReader sr;
        //        sr = File.OpenText(Server.MapPath("Archivos\\") + fic);
        //        texto = sr.ReadToEnd();
        //        sr.Close();
        //        HttpContext.Current.Response.ClearContent();
        //        HttpContext.Current.Response.ClearHeaders();
        //        HttpContext.Current.Response.ContentType = "text/plain";
        //        HttpContext.Current.Response.Write(texto);
        //        HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + fic);
        //        HttpContext.Current.Response.Flush();
        //        File.Delete(Server.MapPath("Archivos\\") + fic);
        //        HttpContext.Current.Response.End();
        //    }
        //    else
        //    {
        //        lblMensj.Text = "No se genero el archivo, Verifique los Datos";
        //    }
        //}
    }

}