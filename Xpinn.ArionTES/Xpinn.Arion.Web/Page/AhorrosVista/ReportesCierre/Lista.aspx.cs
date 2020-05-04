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
using Xpinn.Ahorros.Services;
using Xpinn.Ahorros.Entities;
using Xpinn.Programado.Services;
using Xpinn.Programado.Entities;
using Xpinn.CDATS.Services;
using Xpinn.CDATS.Entities;
using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Configuration;

public partial class Lista : GlobalWeb
{
    AhorroVistaServices reporteServicio = new AhorroVistaServices();
    CierreCuentasServices programadoServicio = new CierreCuentasServices();
    RepCierreCDATServices cdatServicio = new RepCierreCDATServices();
        
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
                case 2: Session["op1"] = 2;
                        break;
                case 3: Session["op1"] = 3;
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
        gvReportecierreAhorroVista.DataSource = null;
        gvReportecierreAhorroVista.DataBind();
        gvReportecierreAhorroVista.Visible = true;
        gvReportecierreAhorroProgramado.DataSource = null;
        gvReportecierreAhorroProgramado.DataBind();
        gvReportecierreAhorroProgramado.Visible = false;
        gvReportecierreCdat.DataSource = null;
        gvReportecierreCdat.DataBind();
        gvReportecierreCdat.Visible = false;
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
            gvReportecierreAhorroVista.Visible = false;
            gvReportecierreAhorroProgramado.Visible = false;
            gvReportecierreCdat.Visible = false;

            // Generar el reporte
            switch ((int)Session["op1"])
            {
                case 1:
                    {
                        gvReportecierreAhorroVista.Visible = true;
                        
                        fecha = ddlCierreFecha.fecha_cierre.ToString() == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(ddlCierreFecha.fecha_cierre.ToString());
                        List<AhorroVista> lstConsultaCierre = new List<AhorroVista>();
                        lstConsultaCierre = reporteServicio.ListarAhorroVistaReporteCierre(fecha, (Usuario)Session["usuario"]);
                        gvReportecierreAhorroVista.EmptyDataText = emptyQuery;
                        gvReportecierreAhorroVista.DataSource = lstConsultaCierre;
                        if (lstConsultaCierre.Count > 0)
                        {
                            mvLista.SetActiveView(vGridReporteCierre);
                            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaCierre.Count;
                            gvReportecierreAhorroVista.DataBind();
                        }
                        else
                        {
                            mvLista.ActiveViewIndex = -1;
                        }
                        Session.Add(reporteServicio.CodigoProgramaReporteCierre + ".consulta", 1);
                        break;
                    }
                case 2:
                    {
                        gvReportecierreAhorroProgramado.Visible = true;
                        fecha = ddlCierreFecha.fecha_cierre.ToString() == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(ddlCierreFecha.fecha_cierre.ToString());
                        List<CuentasProgramado> lstProgramadoCierre = new List<CuentasProgramado>();
                        lstProgramadoCierre = programadoServicio.ListarProgramadoReporteCierre(fecha, (Usuario)Session["usuario"]);
                        gvReportecierreAhorroProgramado.EmptyDataText = emptyQuery;
                        gvReportecierreAhorroProgramado.DataSource = lstProgramadoCierre;
                        if (lstProgramadoCierre.Count > 0)
                        {
                            mvLista.SetActiveView(vGridReporteCierre);
                            lblTotalRegs.Text = "<br/> Registros encontrados " + lstProgramadoCierre.Count;
                            gvReportecierreAhorroProgramado.DataBind();
                        }
                        else
                        {
                            mvLista.ActiveViewIndex = -1;
                        }
                        Session.Add(reporteServicio.CodigoProgramaReporteCierre + ".consulta", 2);
                        break;
                    }
                case 3:
                    {
                        
                        gvReportecierreCdat.Visible = true;
                        fecha = ddlCierreFecha.fecha_cierre.ToString() == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(ddlCierreFecha.fecha_cierre.ToString());
                        List<AdministracionCDAT> lstCdatCierre = new List<AdministracionCDAT>();
                        lstCdatCierre = cdatServicio.ListarCdatReporteCierre(fecha, (Usuario)Session["usuario"]);
                        gvReportecierreCdat.EmptyDataText = emptyQuery;
                        gvReportecierreCdat.DataSource = lstCdatCierre;
                        if (lstCdatCierre.Count > 0)
                        {
                            mvLista.SetActiveView(vGridReporteCierre);
                            lblTotalRegs.Text = "<br/> Registros encontrados " + lstCdatCierre.Count;
                            gvReportecierreCdat.DataBind();
                        }
                        else
                        {
                            mvLista.ActiveViewIndex = -1;
                        }
                        Session.Add(reporteServicio.CodigoProgramaReporteCierre + ".consulta", 2);
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
       if (gvReportecierreAhorroVista.Visible == true)
       {
           if (gvReportecierreAhorroVista.Rows.Count > 0)
           {
               StringBuilder sb = new StringBuilder();
               StringWriter sw = new StringWriter(sb);
               HtmlTextWriter htw = new HtmlTextWriter(sw);
               Page pagina = new Page();
               dynamic form = new HtmlForm();
                gvReportecierreAhorroVista.EnableViewState = false;
               pagina.EnableEventValidation = false;
               pagina.DesignerInitialize();
               pagina.Controls.Add(form);
               form.Controls.Add(gvReportecierreAhorroVista);
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

       if (gvReportecierreAhorroProgramado.Visible == true)
       {
           if (gvReportecierreAhorroProgramado.Rows.Count > 0)
           {
               StringBuilder sb = new StringBuilder();
               StringWriter sw = new StringWriter(sb);
               HtmlTextWriter htw = new HtmlTextWriter(sw);
               Page pagina = new Page();
               dynamic form = new HtmlForm();
                gvReportecierreAhorroProgramado.EnableViewState = false;
               pagina.EnableEventValidation = false;
               pagina.DesignerInitialize();
               pagina.Controls.Add(form);
               form.Controls.Add(gvReportecierreAhorroProgramado);
               pagina.RenderControl(htw);
               Response.Clear();
               Response.Buffer = true;
               Response.ContentType = "application/vnd.ms-excel";
               Response.AddHeader("Content-Disposition", "attachment;filename=ReporteCierreAhorroProgramado.xls");
               Response.Charset = "UTF-16";
               Response.ContentEncoding = Encoding.Default;
               Response.Write(sb.ToString());
               Response.End();
           }
           else
               VerError("Se debe generar el reporte primero");
       }

       if (gvReportecierreCdat.Visible == true)
       {
           if (gvReportecierreCdat.Rows.Count > 0)
           {
               StringBuilder sb = new StringBuilder();
               StringWriter sw = new StringWriter(sb);
               HtmlTextWriter htw = new HtmlTextWriter(sw);
               Page pagina = new Page();
               dynamic form = new HtmlForm();
               gvReportecierreCdat.EnableViewState = false;
               pagina.EnableEventValidation = false;
               pagina.DesignerInitialize();
               pagina.Controls.Add(form);
               form.Controls.Add(gvReportecierreCdat);
               pagina.RenderControl(htw);
               Response.Clear();
               Response.Buffer = true;
               Response.ContentType = "application/vnd.ms-excel";
               Response.AddHeader("Content-Disposition", "attachment;filename=ReporteCierreCdat.xls");
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
            gvReportecierreAhorroVista.Visible = false;
            gvReportecierreAhorroProgramado.Visible = false;
            gvReportecierreAhorroProgramado.Visible = false;
            chkOrden.Enabled = false;
            chkOrden.Checked = false;
        }
        else 
        {
            gvReportecierreAhorroVista.Visible = false;
            gvReportecierreAhorroProgramado.Visible = false;
            gvReportecierreAhorroProgramado.Visible = false;
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
