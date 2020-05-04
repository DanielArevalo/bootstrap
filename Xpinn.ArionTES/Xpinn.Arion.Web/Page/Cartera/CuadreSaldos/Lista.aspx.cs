using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.Util;
using Xpinn.Cartera.Services;
using Xpinn.Cartera.Entities;
using System.Linq;

public partial class CuadreSaldos : GlobalWeb
{
    ReporteService reporteServicio = new ReporteService();
        
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(reporteServicio.CodigoProgCuadreSaldos, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoProgCuadreSaldos, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {                      
            if (!IsPostBack)
            {           
                CargarValoresConsulta(pConsulta, reporteServicio.CodigoProgCuadreSaldos);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                ddlConsultar_SelectedIndexChanged(this, EventArgs.Empty);
                CargarDDL();
            }            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoProgCuadreSaldos, "Page_Load", ex);
        }
    }

    private void CargarDDL()
    {
        PoblarListas poblar = new PoblarListas();
        poblar.PoblarListaDesplegable("TIPO_COMP", ddlTipoComp,(Usuario)Session["usuario"]);
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
            GuardarValoresConsulta(pConsulta, reporteServicio.CodigoProgCuadreSaldos);           
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

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");        
        ctlMensaje.MostrarMensaje("Desea generar ajustar contablemente las diferencias?");
        
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
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
            if (!ValidarCierreContable())
                return;

            // Determinar el código del usuario y determinar si el usuario es un ejecutivo      
            Usuario usuap = (Usuario)Session["usuario"];

            // Generar el reporte
            VerError("");
            fecha = ddlCierreFecha.fecha_cierre.ToString() == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(ddlCierreFecha.fecha_cierre.ToString());
            reporteServicio.GuardarCuadreSaldos(fecha, Convert.ToInt32(ddlConsultar.SelectedValue), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Exception ex)
        {
            VerError(ex.ToString());
        }
    }

    private bool ValidarCierreContable()
    {
        Xpinn.Comun.Entities.Cierea pCierea = new Xpinn.Comun.Entities.Cierea();
        List<Xpinn.Comun.Entities.Cierea> lstCierre = new List<Xpinn.Comun.Entities.Cierea>();
        Xpinn.Comun.Services.CiereaService CiereaServicio = new Xpinn.Comun.Services.CiereaService();

        pCierea.fecha = Convert.ToDateTime(ddlCierreFecha.fecha_cierre);
        pCierea.tipo = "C";
        pCierea.estado = "D";
        lstCierre = CiereaServicio.ListarCierea(pCierea, (Usuario)Session["usuario"]);
        pCierea = lstCierre.FirstOrDefault();

        if (pCierea != null)
        {
            VerError("No se puede ejecutar el proceso, ya se realizó el cierre contable definitivo para la fecha");
            return false;
        }
        return true;
    }

    /// <summary>
    /// Méotodo para limpiar datos de la conuslta
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, reporteServicio.CodigoProgCuadreSaldos);
        gvReportecierre.DataSource = null;       
        gvReportecierre.DataBind();
        ddlCierreFecha.Limpiar();
        ddlCierreFecha.Inicializar("", "");
        lblTotalRegs.Text = "";
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

            // Determinar el còdigo del usuario y determinar si el usuario es un ejecutivo      
            Usuario usuap = (Usuario)Session["usuario"];
            gvReportecierre.Visible = false;

            // Generar el reporte
            bool bGuardar = false;
            gvReportecierre.Visible = true;
            VerError("");
            fecha = ddlCierreFecha.fecha_cierre.ToString() == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(ddlCierreFecha.fecha_cierre.ToString());
            List<Reporte> lstConsultaMora = new List<Reporte>();
            lstConsultaMora = reporteServicio.CuadreSaldos(fecha, Convert.ToInt32(ddlConsultar.SelectedValue), sFiltro, (Usuario)Session["usuario"]);
            gvReportecierre.EmptyDataText = emptyQuery;
            gvReportecierre.DataSource = lstConsultaMora;
            if (lstConsultaMora.Count > 0)
            {
                mvLista.SetActiveView(vGridReporteCierre);
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaMora.Count;
                gvReportecierre.DataBind();
                bGuardar = true;
                ConsultarComprobante();
            }
            else
            {
                mvLista.ActiveViewIndex = -1;               
            }
            Session.Add(reporteServicio.CodigoProgCuadreSaldos + ".consulta", 1);
            if (bGuardar)
            {
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(true);
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
   
    decimal suboperativo = 0;
    decimal subcontable = 0;
    decimal diferencia = 0;
    protected void gvReportecierre_RowDataBound(object sender, GridViewRowEventArgs e)
   {
       if (e.Row.RowType == DataControlRowType.DataRow)
       {
            suboperativo += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldo_operativo"));
            subcontable += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldo_contable"));            
        }
       if (e.Row.RowType == DataControlRowType.Footer)
       {
            diferencia = (suboperativo - subcontable);
            e.Row.Cells[3].Text = "Total:";
            e.Row.Cells[4].Text = suboperativo.ToString("c");
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[5].Text = subcontable.ToString("c");
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[6].Text = diferencia.ToString("c");
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
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
   }


    protected void ddlConsultar_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        ddlCierreFecha.Limpiar();
        VerError("");
        if (ddlConsultar.SelectedValue != "0")
        {
            ddlCierreFecha.Inicializar(TipoCierre(), "D");
        }
    }

    private string TipoCierre()
    {
        string Tipo = "";
        if (ddlConsultar.SelectedValue == "1") Tipo = "R";
        if (ddlConsultar.SelectedValue == "2") Tipo = "U";
        if (ddlConsultar.SelectedValue == "3") Tipo = "R";
        if (ddlConsultar.SelectedValue == "4") Tipo = "S";
        if (ddlConsultar.SelectedValue == "5") Tipo = "A";
        if (ddlConsultar.SelectedValue == "6") Tipo = "A";
        if (ddlConsultar.SelectedValue == "7") Tipo = "H";
        if (ddlConsultar.SelectedValue == "8") Tipo = "L";
        if (ddlConsultar.SelectedValue == "9") Tipo = "M";
        if (ddlConsultar.SelectedValue == "10") Tipo = "J";
        return Tipo;
    }

    private bool ConsultarComprobante()
    {
        Xpinn.Comun.Entities.Cierea pCierea = new Xpinn.Comun.Entities.Cierea();
        List<Xpinn.Comun.Entities.Cierea> lstCierre = new List<Xpinn.Comun.Entities.Cierea>();
        Xpinn.Comun.Services.CiereaService CiereaServicio = new Xpinn.Comun.Services.CiereaService();

        pCierea.fecha = Convert.ToDateTime(ddlCierreFecha.fecha_cierre);
        pCierea.tipo = TipoCierre();
        pCierea.estado = "D";
        lstCierre = CiereaServicio.ListarCierea(pCierea, (Usuario)Session["usuario"]);
        pCierea = lstCierre.FirstOrDefault();
        if (!String.IsNullOrWhiteSpace(pCierea.campo1) && !String.IsNullOrWhiteSpace(pCierea.campo2))
        {
            //Verificar el estado del comprobante
            Xpinn.Contabilidad.Services.ComprobanteService compServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            Xpinn.Contabilidad.Entities.Comprobante vComprobante = new Xpinn.Contabilidad.Entities.Comprobante();
            List<Xpinn.Contabilidad.Entities.DetalleComprobante> lstComp = new List<Xpinn.Contabilidad.Entities.DetalleComprobante>();
            compServicio.ConsultarComprobante(Convert.ToInt64(pCierea.campo1), Convert.ToInt64(pCierea.campo2), ref vComprobante, ref lstComp, (Usuario)Session["usuario"]);

            if (vComprobante.estado != "N")
            {
                txtNumComp.Text = pCierea.campo1;
                ddlTipoComp.SelectedValue = pCierea.campo2;
                txtNumComp.Visible = true;
                ddlTipoComp.Visible = true;
                lblNum.Visible = true;
                lblTipo.Visible = true;
                return true;
            }
            else
            {
                txtNumComp.Visible = false;
                ddlTipoComp.Visible = false;
                lblNum.Visible = false;
                lblTipo.Visible = false;
                return false;
            }
        }
        else
        {
            txtNumComp.Visible = false;
            ddlTipoComp.Visible = false;
            lblNum.Visible = false;
            lblTipo.Visible = false;
            return false;
        }
    }
}
