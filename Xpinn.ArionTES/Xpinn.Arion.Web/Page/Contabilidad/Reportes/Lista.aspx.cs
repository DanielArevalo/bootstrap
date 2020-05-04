using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;
using Xpinn.Asesores.Services;
using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Configuration;

public partial class Page_Contabilidad_Reportes : GlobalWeb
{

    ExcelService excelServicio = new ExcelService();
    ReportesService reporteServicio = new ReportesService();
    Xpinn.FabricaCreditos.Services.UsuarioAtribucionesService UsuarioAtribucionesServicio = new Xpinn.FabricaCreditos.Services.UsuarioAtribucionesService();
    Xpinn.FabricaCreditos.Entities.UsuarioAtribuciones vUsuarioAtribuciones = new Xpinn.FabricaCreditos.Entities.UsuarioAtribuciones();
    Usuario usuario = new Usuario();
    
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(reporteServicio.CodigoPrograma, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {           
           
            if (!IsPostBack)
            {
                Panel4.Visible = true;               
                CargarValoresConsulta(pConsulta, reporteServicio.CodigoPrograma);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                if (Session[reporteServicio.CodigoPrograma + ".consulta"] != null)
                {
                    
                    
                }
            }
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoPrograma, "Page_Load", ex);
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
           GuardarValoresConsulta(pConsulta, reporteServicio.CodigoPrograma);
            
            switch (ddlConsultar.SelectedIndex)
            {
                case 1: Session["op1"] = 1;
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
        LimpiarValoresConsulta(pConsulta, reporteServicio.CodigoPrograma);
        gvReportecierre.DataSource = null;       
        gvReportecierre.DataBind();
    
      
        txtFechaIni.Text = "";
       
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
            BOexcepcion.Throw(reporteServicio.CodigoPrograma, "gvListaCreditos_PageIndexChanging", ex);
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
            BOexcepcion.Throw(reporteServicio.CodigoPrograma, "gvListaClientes_PageIndexChanging", ex);
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
            Usuario usuap = (Usuario)Session["usuario"];

            // Determinar el còdigo del usuario y determinar si el usuario es un ejecutivo      

          
            // Generar el reporte
            switch ((int)Session["op1"])
            {

                case 1:
                     DateTime fechaini;
                     DateTime fechafin;
                     if (txtFechaIni.Text == "" )
                     {
                         Label4.Text = "falta fecha Cierre";
                        
                     }
                     else
                     {
                         Label4.Text = "";

                      fechaini = txtFechaIni.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtFechaIni.Text);
                      fechafin = txtFechaFin.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtFechaFin.Text);

                        List<Reporte> lstConsultaMora = new List<Reporte>();
                        lstConsultaMora = reporteServicio.ListarReporteInteresPagados((Usuario)Session["usuario"],fechaini,fechafin);


                        gvReportecierre.EmptyDataText = emptyQuery;
                        gvReportecierre.DataSource = lstConsultaMora;
                        if (lstConsultaMora.Count > 0)
                        {
                            mvLista.SetActiveView(vGridReporte);
                            lblTotalRegs.Text = "<br/> Registros encontrados   " + lstConsultaMora.Count.ToString();
                            gvReportecierre.DataBind();
                            ValidarPermisosGrilla(gvReportecierre);
                        }
                        else
                        {
                            mvLista.ActiveViewIndex = -1;
                        }
                    }                
             
                    Session.Add(reporteServicio.CodigoPrograma + ".consulta", 1);

                    break;              

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
    private string obtFiltro()
    {

        String filtro = String.Empty;

        return filtro;
    }
   


   protected void ddlAsesores_SelectedIndexChanged(object sender, EventArgs e)
   {

   }
   protected void mvLista_ActiveViewChanged(object sender, EventArgs e)
   {

   }
   protected void ddlConsultar_SelectedIndexChanged(object sender, EventArgs e)
   {

   }
   decimal subtotalcapital = 0;

   protected void gvReportecierre_RowDataBound(object sender, GridViewRowEventArgs e)
   {
       if (e.Row.RowType == DataControlRowType.DataRow)
       {
           subtotalcapital += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "valor"));
          
       }
       if (e.Row.RowType == DataControlRowType.Footer)
       {
           e.Row.Cells[3].Text = "Total:";
           e.Row.Cells[4].Text = subtotalcapital.ToString("N");
           e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
         

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
           Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
           Response.Charset = "UTF-16";
           Response.ContentEncoding = Encoding.Default;
           Response.Write(sb.ToString());
           Response.End();
       }
       else
           VerError("Se debe generar el reporte primero");
   }

   protected void gvReportecierre_SelectedIndexChanged(object sender, EventArgs e)
   {

   }
}
