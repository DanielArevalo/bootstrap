using System;
using System.Data;
using System.Globalization;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Asesores.Services;
using Xpinn.Caja.Services;
using Xpinn.Caja.Entities;
using Xpinn.Asesores.Entities;
using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Configuration;

public partial class Page_CajaFin_Reportes_Lista : GlobalWeb
{
    EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
    ClienteService clienteServicio = new ClienteService();
    CreditoService creditoServicio = new CreditoService();
    CreditosService creditoServicios = new CreditosService();
    ExcelService excelServicio = new ExcelService();
    ReporteCajaService reporteServicio = new ReporteCajaService();
    Xpinn.FabricaCreditos.Services.UsuarioAtribucionesService UsuarioAtribucionesServicio = new Xpinn.FabricaCreditos.Services.UsuarioAtribucionesService();
    Xpinn.FabricaCreditos.Entities.UsuarioAtribuciones vUsuarioAtribuciones = new Xpinn.FabricaCreditos.Entities.UsuarioAtribuciones();
    TransaccionCajaService cajaServicio = new TransaccionCajaService();
    MovGralCreditoService movGrlService = new MovGralCreditoService();
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
            Panel1.Visible = false;
          //  Panel2.Visible = false;

            if (!IsPostBack)
            {
                Panel1.Visible = false;
                //Panel2.Visible = false;
                CargarValoresConsulta(pConsulta, reporteServicio.CodigoPrograma);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
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
            //CalcularTotal();
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

    }



    protected void gvReportemovdiario_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvReportemovdiario.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoPrograma, "gvReportemovdiario_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Método para traer los datos según las condiciones ingresadas
    /// </summary>
    private void Actualizar()
    {
        VerError("");
        try
        {
            Usuario usuap = (Usuario)Session["usuario"];

            // Determinar el còdigo del usuario y determinar si el usuario es un ejecutivo
            int cod = Convert.ToInt32(usuap.codusuario);

            // Generar el reporte
            switch ((int)Session["op1"])
            {
                case 1:
                    DateTime fechaini ; //, fechafinal;
                    if (txtFechaIni.Text == "")
                    {
                        Label3.Text = "falta fecha";
                        
                    }
                    else
                    {
                      
                       // fechafinal = txtFechaFin.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtFechaFin.Text);
                        fechaini = txtFechaIni.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtFechaIni.Text);

                        List<ReportesCaja> lstConsulta = new List<ReportesCaja>();
                        long oficina = (usuap.cod_oficina);
                        lstConsulta = reporteServicio.ListarReportemovdiario((Usuario)Session["usuario"], oficina, fechaini, fechaini);
                        gvReportemovdiario.EmptyDataText = emptyQuery;
                        gvReportemovdiario.DataSource = lstConsulta;
                        if (lstConsulta.Count > 0)
                        {
                            mvLista.ActiveViewIndex = 0;
                            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                            gvReportemovdiario.DataBind();
                            txtTotalvalorPago.Text = (from c in lstConsulta select (long)c.VALOR_PAGO).Sum().ToString(CultureInfo.InvariantCulture);
                            txtTotalcantidad.Text = (from c in lstConsulta select (long)c.CANTIDAD_PAGOS).Sum().ToString(CultureInfo.InvariantCulture);
                            txtTotalefectivo.Text = (from c in lstConsulta select (long)c.EFECTIVO).Sum().ToString(CultureInfo.InvariantCulture);
                            txtTotalcheques.Text = (from c in lstConsulta select (long)c.CHEQUE).Sum().ToString(CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            mvLista.ActiveViewIndex = -1;
                        }
                    }
                    break;

            }

        }
        catch (Exception ex)
        {
            VerError(ex.ToString());
        }
    }

    protected void CalcularTotal()
    {
     
        decimal total = 0;
        List<ReportesCaja> LstReportesCaja = new List<ReportesCaja>();
        LstReportesCaja = (List<ReportesCaja>)Session["ReportesCaja"];
     
        foreach (GridViewRow fila in gvReportemovdiario.Rows)
        {
            total = decimal.Parse(fila.Cells[50].Text);
          
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

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (gvReportemovdiario.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvReportemovdiario.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvReportemovdiario);
            Page.Form.Controls.Add(this.txtTotalcantidad);
          
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }

    protected void btnExportarpoliza_Click(object sender, EventArgs e)
    {
        if (gvReportemovdiario.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvReportemovdiario.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvReportemovdiario);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }




    protected void ddlConsultar_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlConsultar.SelectedIndex == 1)
        {
            Panel1.Visible = true;
          //  Panel2.Visible = true;
        }


        if (ddlConsultar.SelectedIndex == 6)
        {
            Panel1.Visible = true;
        }

        mvLista.ActiveViewIndex = -1;
    }


}
