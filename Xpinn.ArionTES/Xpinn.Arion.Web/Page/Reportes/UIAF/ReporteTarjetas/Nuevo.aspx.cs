using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Reporteador.Services;
using Xpinn.Reporteador.Entities;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Nuevo : GlobalWeb
{

    private UIAFService ReporteService = new UIAFService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ReporteService.CodigoProgramaTarjeta + ".id"] != null)
                VisualizarOpciones(ReporteService.CodigoProgramaTarjeta, "E");
            else
                VisualizarOpciones(ReporteService.CodigoProgramaTarjeta, "A");

            Site toolBar = (Site)this.Master;          
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteService.CodigoProgramaTarjeta, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {           
            if (!IsPostBack)
            {
                mvCuentasxPagar.ActiveViewIndex = 0;
               
                if (Session[ReporteService.CodigoProgramaTarjeta + ".id"] != null)
                {
                    idObjeto = Session[ReporteService.CodigoProgramaTarjeta + ".id"].ToString();
                    Session.Remove(ReporteService.CodigoProgramaTarjeta + ".id");
                    ObtenerDatos(idObjeto);
                    txtFechaFin.Enabled = false;
                    txtFechaIni.Enabled = false;
                    gvProductos.Enabled = false;
                    Site toolBar = (Site)this.Master;
                    lblMsj.Text = " modificado ";
                }
                else
                {
                    lblMsj.Text = " grabado ";
                    txtFechaIni.Text = "";
                    txtFechaFin.Text = "";
                }               
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteService.CodigoProgramaTarjeta, "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ValidarDatos())
            {
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteService.GetType().Name + "L", "btnConsultar_Click", ex);
        }
    }


    private UIAFDetalle ObtenerValores()
    {
        UIAFDetalle vProducto = new UIAFDetalle();        
        return vProducto;
    }



    private string obtFiltro(UIAFDetalle Producto)
    {
        String filtro = String.Empty;
        return filtro;
    }


    private void Actualizar()
    {
        try
        {
            Configuracion conf = new Configuracion();
            List<UIAFTarjetas> lstConsulta = new List<UIAFTarjetas>();

            String filtro = obtFiltro(ObtenerValores());

            DateTime pFechaIni, pFechaFin;
            pFechaIni = txtFechaIni.ToDateTime == null ? DateTime.MinValue : txtFechaIni.ToDateTime;
            pFechaFin = txtFechaFin.ToDateTime == null ? DateTime.MinValue : txtFechaFin.ToDateTime;

            lstConsulta = ReporteService.ListarTransaccionesTarjeta(filtro, pFechaIni, pFechaFin, (Usuario)Session["usuario"]);
            Session["DTUIAF"] = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvProductos.Visible = true;
                gvProductos.DataSource = lstConsulta;
                gvProductos.DataBind();                
                lblInfo.Visible = false;
                lblTotalRegs1.Visible = true;
                lblTotalRegs1.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
            }
            else
            {
                lblInfo.Visible = false;
                lblTotalRegs1.Visible = true;
                lblTotalRegs1.Text = "<br/> No se encontraron registros para el período dado ";
            }
            Session.Add(ReporteService.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteService.GetType().Name + "L", "Actualizar", ex);
        }
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            int IdReporte ;
            IdReporte = Convert.ToInt32(idObjeto);
            UIAF vRepor = new UIAF();
            vRepor = ReporteService.ConsultarReporteUIAF(IdReporte, (Usuario)Session["usuario"]);
            if (vRepor.fecha_inicial != DateTime.MinValue)
                txtFechaIni.Text = vRepor.fecha_inicial.ToShortDateString();
            if (vRepor.fecha_final != DateTime.MinValue)
                txtFechaFin.Text = vRepor.fecha_final.ToShortDateString();

            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteService.CodigoProgramaTarjeta, "ObtenerDatos", ex);
        }
    }


    protected Boolean ValidarDatos()
    {
        VerError("");
        return true;
    }


    protected string ReemplazarTextos(String pTexto)
    {
        return pTexto.Replace("&#250", "u").Replace("&#237", "i").Replace("&#39", "").Replace(";", "").Replace("&#201", "E").Replace("&#193", "A").Replace("&#211", "O").Replace("&#243", "o").Replace("&#205", "I").Replace("&nbsp", "").Replace("&#209", "N");
    }

    protected void btnArchivo_Click(object sender, EventArgs e)
    {
        string nombreArchivo = "UIAFReporte.txt";
        if (gvProductos.Rows.Count > 0 && Session["DTUIAF"] != null)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("") + nombreArchivo, true);
            // Decargar titulos
            int titcount = gvProductos.HeaderRow.Cells.Count;
            string linea = "";
            for (int j = 1; j < titcount - 1; j++)
            {
                string texto = ReemplazarTextos(gvProductos.HeaderRow.Cells[j].Text);
                linea += texto + ";";
            }
            sw.WriteLine(linea);
            // Descargar el texto de cada fila
            int rowcount = gvProductos.Rows.Count;
            for (int i = 0; i < rowcount - 1; i++)
            {
                int celcount = gvProductos.Rows[i].Cells.Count;
                linea = "";
                for (int j = 1; j < celcount - 1; j++)
                {
                    string texto = ReemplazarTextos(gvProductos.Rows[i].Cells[j].Text);
                    linea += texto + ";";
                }
                sw.WriteLine(linea);
            }
            sw.Close();
        }
        if (File.Exists(Server.MapPath("") + nombreArchivo))
        {
            System.IO.StreamReader sr;
            sr = File.OpenText(Server.MapPath("") + nombreArchivo);
            string texto = sr.ReadToEnd();
            sr.Close();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "text/plain";
            HttpContext.Current.Response.Write(texto);
            HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + nombreArchivo);
            HttpContext.Current.Response.Flush();
            File.Delete(Server.MapPath("") + nombreArchivo);
            HttpContext.Current.Response.End();
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (gvProductos.Rows.Count > 0 && Session["DTUIAF"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            //gvProductos.AllowPaging = false;
            //gvProductos.DataSource = Session["DTUIAF"];
            //gvProductos.DataBind();
            for (int i = 0; i < gvProductos.Rows.Count; i++)
            {
                GridViewRow row = gvProductos.Rows[i];
                row.Cells[7].Attributes.Add("style", "mso-number-format:\\@");
            }
            gvProductos.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvProductos);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=UIAFTarjetas.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            string style = @"<style> 
                                .gridHeader { background-color: #359af2; font-weight: bold; color: White; border: 1px solid #d7e6e9; text-align: center; } 
                                .gridItem   { mso-number-format:\@; }  
                            </style>";
            Response.Write(style);            
            Response.Write(sb.ToString());
            Response.End();
            //gvProductos.AllowPaging = true;
            //gvProductos.DataBind();

        }
        else
            VerError("Se debe generar el reporte primero");
    }




    protected void gvProductos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
        Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
        pData = ConsultaData.ConsultarGeneral(9188, (Usuario)Session["usuario"]);
        Int64 valor = Convert.ToInt64(pData.valor);
        if (valor == 1)//si es 1 mostrar ciudad de oficina , si es 0 o null mostra ciudad de la persona
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (valor >= 1)
                {
                    e.Row.Cells[5].Controls.Clear();
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Controls.Clear();
                    e.Row.Cells[6].Visible = true;
                }

            }


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (valor >= 1)
                {
                    e.Row.Cells[5].Controls.Clear();
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Controls.Clear();
                    e.Row.Cells[6].Visible = true;
                }
            }


        }
        else
        {
            e.Row.Cells[5].Controls.Clear();
            e.Row.Cells[5].Visible = true;
            e.Row.Cells[6].Controls.Clear();
            e.Row.Cells[6].Visible = false;
        }
    }
}