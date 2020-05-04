using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Configuration;

public partial class Page_Asesores_Colocacion_Lista : GlobalWeb
{
    ReporteService reporteServicio = new ReporteService();
        
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(reporteServicio.CodigoProgReportesCierre, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoProgReportesCierre, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {                      
            if (!IsPostBack)
            {
                CargarDropDown();
                              
                CargarValoresConsulta(pConsulta, reporteServicio.CodigoProgReportesCierre);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ddlConsultar_SelectedIndexChanged(this, EventArgs.Empty);
            }            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoProgReportesCierre, "Page_Load", ex);
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
            GuardarValoresConsulta(pConsulta, reporteServicio.CodigoProgReportesCierre);
            
            switch (ddlConsultar.SelectedIndex)
            {
                case 1: Session["op1"] = 1;
                        break;
                case 2: Session["op1"] = 2;
                        break;
                case 3: Session["op1"] = 3;
                        break;
            }
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
        LimpiarValoresConsulta(pConsulta, reporteServicio.CodigoProgReportesCierre);
        gvReportecierre.DataSource = null;       
        gvReportecierre.DataBind();
        ddlCierreFecha.Limpiar();
        ddlCierreFecha.Inicializar("", "");
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
            BOexcepcion.Throw(reporteServicio.CodigoProgReportesCierre, "gvListaCreditos_PageIndexChanging", ex);
        }
    }

    protected void gvReoirtemora_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvReportecierre.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoProgReportesCierre, "gvListaClientes_PageIndexChanging", ex);
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
            if (ddlCierreFecha.fecha_cierre == null)
            {
                VerError("Seleccione la fecha Cierre");
                return;
            }

            // Determinar los filtros
            string sFiltro = "";
            if (ddlLinea.SelectedText != "")
            {
                sFiltro += " and cod_linea_Credito In (";
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
                sFiltro += " and cod_oficina In ( " + ddloficina.SelectedValue + ")";
            if (ddlCategoria.SelectedText != "")
            {
                sFiltro += " and cod_categoria_cli In (";
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
            if (txtIdentificacion.Text.Trim() != "")
            {;
                sFiltro += " and identificacion = '" + txtIdentificacion.Text.Trim() + "' ";
            }

            // Determinar el còdigo del usuario y determinar si el usuario es un ejecutivo      
            Usuario usuap = (Usuario)Session["usuario"];
            gvReportecierre.Visible = false;
            gvReportecausacion.Visible = false;
            gvReporteprovision.Visible = false;

            // Generar el reporte
            switch ((int)Session["op1"])
            {
                case 1:
                    {
                        gvReportecierre.Visible = true;
                        VerError("");
                        fecha = ddlCierreFecha.fecha_cierre.ToString() == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(ddlCierreFecha.fecha_cierre.ToString());
                        List<Reporte> lstConsultaMora = new List<Reporte>();
                        lstConsultaMora = reporteServicio.ListarRepCierreDetallado(fecha, sFiltro, (Usuario)Session["usuario"]);
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
                        Session.Add(reporteServicio.CodigoProgReportesCierre + ".consulta", 1);
                        break;
                    }
                case 2:
                    {
                        gvReportecausacion.Visible = true;
                        VerError("");
                        fecha = ddlCierreFecha.fecha_cierre.ToString() == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(ddlCierreFecha.fecha_cierre.ToString());
                        List<Reporte> lstConsultaMora = new List<Reporte>();
                        lstConsultaMora = reporteServicio.ListarRepCausacionDetallado(fecha, sFiltro, (Usuario)Session["usuario"]);
                        gvReportecausacion.EmptyDataText = emptyQuery;
                        gvReportecausacion.DataSource = lstConsultaMora;
                        if (lstConsultaMora.Count > 0)
                        {
                            mvLista.SetActiveView(vGridReporteCierre);
                            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaMora.Count;
                            gvReportecausacion.DataBind();
                        }
                        else
                        {
                            mvLista.ActiveViewIndex = -1;
                        }
                        Session.Add(reporteServicio.CodigoProgReportesCierre + ".consulta", 2);
                        break;
                    }
                case 3:
                    {
                        if (!string.IsNullOrWhiteSpace(ddlAtributos.SelectedText))
                        {
                            sFiltro += " and COD_ATR In (";

                            string[] sCodAtributo = ddlAtributos.SelectedValue.Split(',');
                            for (int i = 0; i < sCodAtributo.Count(); i++)
                            {
                                string[] sDato = sCodAtributo[i].ToString().Split('-');
                                if (i > 0)
                                    sFiltro += ", ";
                                sFiltro += "'" + sDato[0].Trim() + "'";
                            }
                            sFiltro += ") ";
                        }

                        gvReporteprovision.Visible = true;
                        VerError("");
                        fecha = ddlCierreFecha.fecha_cierre.ToString() == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(ddlCierreFecha.fecha_cierre.ToString());
                        List<Reporte> lstConsultaMora = new List<Reporte>();
                        lstConsultaMora = reporteServicio.ListarRepProvisionDetallado(fecha, sFiltro, (Usuario)Session["usuario"]);
                        gvReporteprovision.EmptyDataText = emptyQuery;
                        gvReporteprovision.DataSource = lstConsultaMora;
                        if (lstConsultaMora.Count > 0)
                        {
                            mvLista.SetActiveView(vGridReporteCierre);
                            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaMora.Count;
                            gvReporteprovision.DataBind();
                        }
                        else
                        {
                            mvLista.ActiveViewIndex = -1;
                        }
                        Session.Add(reporteServicio.CodigoProgReportesCierre + ".consulta", 2);
                        break;
                    }
            }
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
    private string actobtFiltro()
    {
        String filtro = String.Empty;
        return filtro;
    }
   
   decimal subtotalcapital = 0;
   protected void gvReportecierre_RowDataBound(object sender, GridViewRowEventArgs e)
   {
       if (e.Row.RowType == DataControlRowType.DataRow)
       {
           subtotalcapital += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldo"));          
       }
       if (e.Row.RowType == DataControlRowType.Footer)
       {
           e.Row.Cells[10].Text = "Total:";
           e.Row.Cells[11].Text = subtotalcapital.ToString("c");
           e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Left;        
           e.Row.Font.Bold = true;
       }
   }

   decimal subtotalcausacion = 0;
   protected void gvReportecausacion_RowDataBound(object sender, GridViewRowEventArgs e)
   {
       if (e.Row.RowType == DataControlRowType.DataRow)
       {
           subtotalcausacion += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldo_causado"));
       }
       if (e.Row.RowType == DataControlRowType.Footer)
       {
           e.Row.Cells[10].Text = "Total:";
           e.Row.Cells[11].Text = subtotalcausacion.ToString("c");
           e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Left;
           e.Row.Font.Bold = true;
       }
   }

   decimal subtotalprovision = 0;
   protected void gvReporteprovision_RowDataBound(object sender, GridViewRowEventArgs e)
   {
       if (e.Row.RowType == DataControlRowType.DataRow)
       {
           subtotalprovision += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldo_causado"));
       }
       if (e.Row.RowType == DataControlRowType.Footer)
       {
           e.Row.Cells[10].Text = "Total:";
           e.Row.Cells[11].Text = subtotalprovision.ToString("c");
           e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Left;
           e.Row.Font.Bold = true;
       }
   }


   protected void btnExportar_Click(object sender, EventArgs e)
   {
       if (gvReportecierre.Visible == true)
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
               Response.AddHeader("Content-Disposition", "attachment;filename=ReporteCartera.xls");
               Response.Charset = "UTF-16";
               Response.ContentEncoding = Encoding.Default;
               Response.Write(sb.ToString());
               Response.End();
           }
           else
               VerError("Se debe generar el reporte primero");
       }

       if (gvReportecausacion.Visible == true)
       {
           if (gvReportecausacion.Rows.Count > 0)
           {
               StringBuilder sb = new StringBuilder();
               StringWriter sw = new StringWriter(sb);
               HtmlTextWriter htw = new HtmlTextWriter(sw);
               Page pagina = new Page();
               dynamic form = new HtmlForm();
               gvReportecausacion.EnableViewState = false;
               pagina.EnableEventValidation = false;
               pagina.DesignerInitialize();
               pagina.Controls.Add(form);
               form.Controls.Add(gvReportecausacion);
               pagina.RenderControl(htw);
               Response.Clear();
               Response.Buffer = true;
               Response.ContentType = "application/vnd.ms-excel";
               Response.AddHeader("Content-Disposition", "attachment;filename=ReporteCausacion.xls");
               Response.Charset = "UTF-16";
               Response.ContentEncoding = Encoding.Default;
               Response.Write(sb.ToString());
               Response.End();
           }
           else
               VerError("Se debe generar el reporte primero");
       }

       if (gvReporteprovision.Visible == true)
       {
           if (gvReporteprovision.Rows.Count > 0)
           {
               StringBuilder sb = new StringBuilder();
               StringWriter sw = new StringWriter(sb);
               HtmlTextWriter htw = new HtmlTextWriter(sw);
               Page pagina = new Page();
               dynamic form = new HtmlForm();
               gvReporteprovision.EnableViewState = false;
               pagina.EnableEventValidation = false;
               pagina.DesignerInitialize();
               pagina.Controls.Add(form);
               form.Controls.Add(gvReporteprovision);
               pagina.RenderControl(htw);
               Response.Clear();
               Response.Buffer = true;
               Response.ContentType = "application/vnd.ms-excel";
               Response.AddHeader("Content-Disposition", "attachment;filename=ReporteProvision.xls");
               Response.Charset = "UTF-16";
               Response.ContentEncoding = Encoding.Default;
               Response.Write(sb.ToString());
               Response.End();
           }
           else
               VerError("Se debe generar el reporte primero");
       }
   }

   protected void CargarDropDown()
   {
       Xpinn.FabricaCreditos.Services.OficinaService oficinaServicio = new Xpinn.FabricaCreditos.Services.OficinaService();
       Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
       ddloficina.DataTextField = "nombre";
       ddloficina.DataValueField = "codigo";
       ddloficina.DataSource = oficinaServicio.ListarOficinas(oficina, (Usuario)Session["usuario"]);
       ddloficina.DataBind();

       Xpinn.FabricaCreditos.Services.LineasCreditoService BOLinea = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
       List<Xpinn.FabricaCreditos.Entities.LineasCredito> lstLineas = new List<Xpinn.FabricaCreditos.Entities.LineasCredito>();
       Xpinn.FabricaCreditos.Entities.LineasCredito pEntidad = new Xpinn.FabricaCreditos.Entities.LineasCredito();
       lstLineas = BOLinea.ListarLineasCredito(pEntidad, (Usuario)Session["usuario"]);
       if (lstLineas.Count > 0)
       {
           ddlLinea.DataSource = lstLineas;
           ddlLinea.DataTextField = "nom_linea_credito";
           ddlLinea.DataValueField = "cod_linea_credito";
           ddlLinea.DataBind();
       }

        List<Atributos> lstAtributos = new List<Atributos>();
        lstAtributos = BOLinea.ListarAtributos(new Atributos(), (Usuario)Session["usuario"]);
        if (lstLineas.Count > 0)
        {
            ddlAtributos.DataSource = lstAtributos;
            ddlAtributos.DataTextField = "descripcion";
            ddlAtributos.DataValueField = "cod_atr";
            ddlAtributos.DataBind();
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


    protected void ddlConsultar_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        ddlCierreFecha.Limpiar();
        VerError("");
        if (ddlConsultar.SelectedValue == "0")
        {
            lblAtributos.Visible = false;
            ddlAtributos.Visible = false;
        }
        else
        {
            if (ddlConsultar.SelectedValue == "S")
            {
                lblAtributos.Visible = true;
                ddlAtributos.Visible = true;
            }
            else
            {
                lblAtributos.Visible = false;
                ddlAtributos.Visible = false;
            }
            ddlCierreFecha.Inicializar(ddlConsultar.SelectedValue, "D");
            //chkOrden.Enabled = true;
            //chkOrden.Checked = false;
        }
    }

    protected void chkOrden_CheckedChanged(object sender, EventArgs e)
    {
        ddlCierreFecha.Limpiar();
        CheckBox chkordenar = (CheckBox)sender;
        if (chkordenar.Checked)
            ddlCierreFecha.ordenar("D");
        else
            ddlCierreFecha.ordenar("A");
    }
}
