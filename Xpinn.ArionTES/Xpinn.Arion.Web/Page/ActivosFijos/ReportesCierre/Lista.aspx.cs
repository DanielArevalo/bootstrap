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
using Xpinn.ActivosFijos.Services;
using Xpinn.ActivosFijos.Entities;

using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Configuration;

public partial class Lista : GlobalWeb
{
  ActivosFijoservices reporteServicio = new ActivosFijoservices();
        
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(reporteServicio.CodigoProgramaReporteCierre, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoProgramaReporteCierre, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {                      
            if (!IsPostBack)
            {
                ddlTipo.SelectedIndex = 0;
                ddlCierreFecha.Inicializar("","");
            }            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoProgramaReporteCierre, "Page_Load", ex);
        }
    }


    /// <summary>
    /// Método que permite generar el reporte segùn las condiciones dadas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {

        if (ddlTipo.SelectedIndex == 0)
        {
            VerError("Debe seleccionar el tipo de producto");
            return;
        }
        else if (ddlCierreFecha.fecha_cierre == null)
        {
            VerError("Debe seleccionar la fecha de cierre");
            return;
        }


        switch (ddlTipo.SelectedIndex)
            {
                case 1: Session["op1"] = 1;
                        break;
               
            }
            Actualizar();            
        
    }

    /// <summary>
    /// Méotodo para limpiar datos de la conuslta
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        gvReportecierre.DataSource = null;
        gvReportecierre.DataBind();
        gvReportecierre.Visible = true;
     
        lblTotalRegs.Text = "";
        ddlCierreFecha.Limpiar();
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
            gvReportecierre.Visible = false;
          
            // Generar el reporte
            switch ((int)Session["op1"])
            {
                case 1:
                    {
                        gvReportecierre.Visible = true;
                        
                        fecha = ddlCierreFecha.fecha_cierre.ToString() == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(ddlCierreFecha.fecha_cierre.ToString());
                        List<ActivoFijo> lstConsultaCierre = new List<ActivoFijo>();
                        lstConsultaCierre = reporteServicio.ListarActivoFijoReporteCierre(fecha, (Usuario)Session["usuario"]);
                        gvReportecierre.EmptyDataText = emptyQuery;
                        gvReportecierre.DataSource = lstConsultaCierre;
                        if (lstConsultaCierre.Count > 0)
                        {
                            mvLista.SetActiveView(vGridReporteCierre);
                            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaCierre.Count;
                            gvReportecierre.DataBind();
                        }
                        else
                        {
                            mvLista.ActiveViewIndex = -1;
                        }
                        Session.Add(reporteServicio.CodigoProgramaReporteCierre + ".consulta", 1);
                        break;
                    }
             
            }
        }
        catch (Exception ex)
        {
            VerError(ex.ToString());
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
               Response.AddHeader("Content-Disposition", "attachment;filename=ReporteCierreAhorroVista.xls");
               Response.Charset = "UTF-16";
               Response.ContentEncoding = Encoding.Default;
               Response.Write(sb.ToString());
               Response.End();
           }
           else
               VerError("Se debe generar el reporte primero");
       }

      
   }


    protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCierreFecha.Limpiar();
        VerError("");
        if (ddlTipo.SelectedValue == "0")
        {
            gvReportecierre.Visible = false;
           
            chkOrden.Enabled = false;
            chkOrden.Checked = false;
        }
        else 
        {
            gvReportecierre.Visible = false;
           
            ddlCierreFecha.Inicializar(ddlTipo.SelectedValue, "D");
            chkOrden.Enabled = true;
            chkOrden.Checked = false;
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
