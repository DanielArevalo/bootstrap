using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Configuration;

public partial class Lista : GlobalWeb
{
    AporteServices aporteServicio = new AporteServices();

        
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(aporteServicio.CodigoProgramaRepCierreAportes, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(aporteServicio.CodigoProgramaRepCierreAportes, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {                      
            if (!IsPostBack)
            {
                ddlCierreFecha.Inicializar("A","D");
            }            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(aporteServicio.CodigoProgramaRepCierreAportes, "Page_Load", ex);
        }
    }


    /// <summary>
    /// Método que permite generar el reporte segùn las condiciones dadas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlCierreFecha.fecha_cierre != null)
        {
            VerError("");
            Actualizar();
        }
        else
            VerError("Debe seleccionar la fecha de corte");


    }

    /// <summary>
    /// Méotodo para limpiar datos de la conuslta
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        gvReportecierreAporte.DataSource = null;
        gvReportecierreAporte.DataBind(); 
        lblTotalRegs.Text = "";
        ddlCierreFecha.Limpiar();
        ddlCierreFecha.Inicializar("A", "D");
        
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
            
             // Determinar el còdigo del usuario y determinar si el usuario es un ejecutivo      
             Usuario usuap = (Usuario)Session["usuario"];
             gvReportecierreAporte.Visible = false;
             gvReportecierreAporte.Visible = false;

            // Generar el reporte

             gvReportecierreAporte.Visible = true;
             fecha = ddlCierreFecha.fecha_cierre.ToString() == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(ddlCierreFecha.fecha_cierre.ToString());
             List<Aporte> lstConsultaCierre = new List<Aporte>();
             lstConsultaCierre = aporteServicio.ListarAporteReporteCierre(fecha, (Usuario)Session["usuario"]);
             gvReportecierreAporte.EmptyDataText = emptyQuery;
             gvReportecierreAporte.DataSource = lstConsultaCierre;
             if (lstConsultaCierre.Count > 0)
             {
                 mvLista.SetActiveView(vGridReporteCierre);
                 lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaCierre.Count;
                 gvReportecierreAporte.DataBind();
             }
             else
             {
                mvLista.ActiveViewIndex = -1;
                gvReportecierreAporte.Visible= false;
             }
             Session.Add(aporteServicio.CodigoProgramaRepCierreAportes + ".consulta", 1);    
        }
        catch (Exception ex)
        {
            VerError(ex.ToString());
        }
    }
   

   protected void btnExportar_Click(object sender, EventArgs e)
   {
       
           if (gvReportecierreAporte.Rows.Count > 0)
           {
               StringBuilder sb = new StringBuilder();
               StringWriter sw = new StringWriter(sb);
               HtmlTextWriter htw = new HtmlTextWriter(sw);
               Page pagina = new Page();
               dynamic form = new HtmlForm();
               gvReportecierreAporte.EnableViewState = false;
               pagina.EnableEventValidation = false;
               pagina.DesignerInitialize();
               pagina.Controls.Add(form);
               form.Controls.Add(gvReportecierreAporte);
               pagina.RenderControl(htw);
               Response.Clear();
               Response.Buffer = true;
               Response.ContentType = "application/vnd.ms-excel";
               Response.AddHeader("Content-Disposition", "attachment;filename=ReporteCierreAporte.xls");
               Response.Charset = "UTF-16";
               Response.ContentEncoding = Encoding.Default;
               Response.Write(sb.ToString());
               Response.End();
           }
           else
               VerError("Se debe generar el reporte primero");
       

      
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
