using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Linq;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;


public partial class Page_Asesores_Colocacion_Lista : GlobalWeb
{
    ReporteService reporteServicio = new ReporteService();
        
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(reporteServicio.CodigoProgCierreDiario, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoProgCierreDiario, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {                      
            if (!IsPostBack)
            {
                CargarDropDown();             
                CargarValoresConsulta(pConsulta, reporteServicio.CodigoProgCierreDiario);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
            }            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoProgCierreDiario, "Page_Load", ex);
        }
    }


    /// <summary>
    /// Método que permite generar el reporte segùn las condiciones dadas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, reporteServicio.CodigoProgCierreDiario);
            Actualizar();            
        }
    }

    /// <summary>
    /// Méotodo para limpiar datos de la conuslta
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, reporteServicio.CodigoProgCierreDiario);
        gvReportecierre.DataSource = null;       
        gvReportecierre.DataBind();          
        lblTotalRegs.Text = "";
    }
    
    protected void gvListaCreditos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {           
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoProgCierreDiario, "gvListaCreditos_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Método para traer los datos según las condiciones ingresadas
    /// </summary>
    private void Actualizar()
    {
        Configuracion conf = new Configuracion();
        VerError("");
        try
        {
            DateTime fecha;
            //if (txtFechaIni.Text == "")
            //{
                //VerError("falta fecha Cierre");
                //return;
            //}

            // Determinar los filtros
            string sFiltro = "";
            if (ddlLinea.SelectedText != "")
            {
                sFiltro += " and informe_creditos.cod_linea_Credito In (";
                string[] sLineas = ddlLinea.SelectedText.Split(',');
                for (int i = 0; i < sLineas.Count(); i++)
                {
                    string[] sDato = sLineas[i].ToString().Split('-');
                    if (i > 0)
                        sFiltro += ", ";
                    sFiltro += "'" + sDato[0].Trim() + "'";
                }
                sFiltro += ") ";
            }
            if (ddloficina.SelectedText != "")
                sFiltro += " and informe_creditos.cod_oficina In ( " + ddloficina.SelectedValue + ")";
            if (ddlCategoria.SelectedText != "")
            {
                sFiltro += " and informe_creditos.cod_categoria_cli In (";
                string[] sCateg = ddlCategoria.SelectedValue.Split(',');
                for (int i = 0; i < sCateg.Count(); i++)
                {
                    string[] sDato = sCateg[i].ToString().Split('-');
                    if (i > 0)
                        sFiltro += ", ";
                    sFiltro += "'" + sDato[0].Trim() + "'";
                }
                sFiltro += ") ";
            }

            // Determinar el còdigo del usuario y determinar si el usuario es un ejecutivo      
            Usuario usuap = (Usuario)Session["usuario"];

            // Generar el reporte
            gvReportecierre.Visible = true;
            fecha = ddlFechaCorte.Text == "" ? DateTime.Now : Convert.ToDateTime(ddlFechaCorte.Text);
            List<Reporte> lstConsultaMora = new List<Reporte>();
            lstConsultaMora = reporteServicio.ListarRepCierreDiarios(fecha, sFiltro, (Usuario)Session["usuario"]);
            gvReportecierre.EmptyDataText = emptyQuery;
            gvReportecierre.DataSource = lstConsultaMora;
            if (lstConsultaMora.Count > 0)
            {
                mvLista.SetActiveView(vGridReporteCierre);
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaMora.Count;
                gvReportecierre.DataBind();
            }
            else
            {
                mvLista.ActiveViewIndex = -1;
            }
            Session.Add(reporteServicio.CodigoProgCierreDiario + ".consulta", 1);
        }
        catch (Exception ex)
        {
            VerError(ex.ToString());
        }
    }

   
    /// <summary>
    /// Método para obtener datos del filtro
    /// </summary>
    /// <returns></returns>
    private string obtFiltro()
    {
        String filtro = String.Empty;
        return filtro;
    }
   
   decimal subtotalcapital = 0;
   decimal subtotalapagar = 0;
   protected void gvReportecierre_RowDataBound(object sender, GridViewRowEventArgs e)
   {
       if (e.Row.RowType == DataControlRowType.DataRow)
       {
           subtotalcapital += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldo"));
           subtotalapagar += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "valor_a_pagar"));          
       }
       if (e.Row.RowType == DataControlRowType.Footer)
       {
           e.Row.Cells[12].Text = "Total:";
           e.Row.Cells[13].Text = subtotalcapital.ToString("c");
           e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Left;
           e.Row.Cells[17].Text = subtotalapagar.ToString("c");
           e.Row.Cells[17].HorizontalAlign = HorizontalAlign.Left;        
           e.Row.Font.Bold = true;
       }
   }

   protected void btnExportar_Click(object sender, EventArgs e)
   {
        if (gvReportecierre.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvReportecierre.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvReportecierre);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=ReporteDiario.xls");
            Response.Charset = "UTF-16";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
   }

   protected void CargarDropDown()
   {
       Configuracion conf = new Configuracion();
       List<Reporte> lstFechaCierre = new List<Reporte>();
       lstFechaCierre = reporteServicio.ListarFechaCorte((Usuario)Session["Usuario"]);
       ddlFechaCorte.DataSource = lstFechaCierre;
       ddlFechaCorte.DataTextFormatString = "{0:" + conf.ObtenerFormatoFecha() + "}";
       ddlFechaCorte.DataTextField = "fechacierre";
       ddlFechaCorte.DataBind();

       Xpinn.FabricaCreditos.Services.OficinaService oficinaServicio = new Xpinn.FabricaCreditos.Services.OficinaService();
       Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
       ddloficina.DataTextField = "nombre";
       ddloficina.DataValueField = "codigo";
       ddloficina.DataSource = oficinaServicio.ListarOficinas(oficina, (Usuario)Session["usuario"]);
       ddloficina.DataBind();

       Xpinn.FabricaCreditos.Services.LineasCreditoService BOLinea = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
       List<Xpinn.FabricaCreditos.Entities.LineasCredito> lstLineas = new List<Xpinn.FabricaCreditos.Entities.LineasCredito>();
       Xpinn.FabricaCreditos.Entities.LineasCredito pEntidad = new Xpinn.FabricaCreditos.Entities.LineasCredito();
       pEntidad.estado = 1;
       lstLineas = BOLinea.ListarLineasCredito(pEntidad, (Usuario)Session["usuario"]);
       if (lstLineas.Count > 0)
       {
           ddlLinea.DataSource = lstLineas;
           ddlLinea.DataTextField = "nom_linea_credito";
           ddlLinea.DataValueField = "cod_linea_credito";
           ddlLinea.DataBind();
       }

       Xpinn.Cartera.Services.ClasificacionCarteraService BOCartera = new Xpinn.Cartera.Services.ClasificacionCarteraService();
       List<Xpinn.Cartera.Entities.ClasificacionCartera> lstCategoria = new List<Xpinn.Cartera.Entities.ClasificacionCartera>();
       lstCategoria = BOCartera.ListarCategorias((Usuario)Session["usuario"]);
       if (lstCategoria.Count > 0)
       {
           ddlCategoria.DataSource = lstCategoria;
           ddlCategoria.DataTextField = "categoria";
           ddlCategoria.DataValueField = "categoria";
           ddlCategoria.DataBind();
       }
   }
   
}
