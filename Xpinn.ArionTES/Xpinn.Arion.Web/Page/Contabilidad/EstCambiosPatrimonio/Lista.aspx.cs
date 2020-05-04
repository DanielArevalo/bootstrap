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
using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;
using System.Globalization;
using System.Web.UI.HtmlControls;

public partial class Lista : GlobalWeb
{

    EstFlujoEfectivoServices objeEfectivo = new EstFlujoEfectivoServices();
    CambioPatrimonioService objePatrimonio = new CambioPatrimonioService();
    private static string pCodigo;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["niif"] != null)
            {
                VisualizarOpciones(objePatrimonio.CodigoProgramaNIIF, "L");
                pCodigo = objePatrimonio.CodigoProgramaNIIF;
            }
            else
            {
                VisualizarOpciones(objePatrimonio.CodigoPrograma, "L");
                pCodigo = objePatrimonio.CodigoPrograma;
            }

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pCodigo, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarCombos();
                Site toolBar = (Site)this.Master;
                toolBar.MostrarImprimir(false);
                toolBar.MostrarExportar(false);
                btnInforme.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pCodigo, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        if (validaCombos())
        {
            Actualizar();   
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        gvLista.DataBind();
        lblTotalRegs.Text = "";
        ddlCentroCosto.ClearSelection();
        ddlPeriodoAn.ClearSelection();
        ddPeriodoAc.ClearSelection();
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pCodigo + "L", "gvLista_RowDataBound", ex);
        }
    }

    Boolean validaCombos() 
    {
        if (ddPeriodoAc.SelectedIndex<=0)
        {
            VerError("seleccione el Periodo Actual");
            return false;
        }
        if (ddlPeriodoAn.SelectedIndex<=0)
        {
            VerError("seleccione el periodo anterior");
            return false;
        }
        return true;
    }

    String getFiltro()
    {
        String Filtro = "";

        if (ddPeriodoAc.SelectedIndex > 0)
            Filtro += " and c.codciudad = " + ddPeriodoAc.SelectedValue;
        if (ddlPeriodoAn.SelectedIndex > 0)
            Filtro += " and o.cod_oficina = " + ddlPeriodoAn.SelectedValue;
        return Filtro;
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pCodigo, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {

        try
        {
            List<CambioPatrimonio> lstConsulta = new List<CambioPatrimonio>();
            DateTime fechaActual = Convert.ToDateTime(ddPeriodoAc.SelectedValue), fechaAnterio = Convert.ToDateTime(ddlPeriodoAn.SelectedValue);

            int pOpcion = Request.QueryString["niif"] != null ? 2 : 1;
            lstConsulta = objePatrimonio.getListaCambioPatrimonioServices((Usuario)Session["usuario"], pOpcion);
            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            Session["DTPATRIMONIOS"] = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
                Site toolBar = (Site)this.Master;
                toolBar.MostrarExportar(true);
                toolBar.MostrarImprimir(true);
                btnInforme.Visible = true;
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }
            Session.Add(pCodigo + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pCodigo, "Actualizar", ex);
        }

    }

    void cargarCombos()
    {
        List<EstFlujoEfectivo> Listddl = new List<EstFlujoEfectivo>();
        Listddl = objeEfectivo.ListarDdllServices((Usuario)Session["usuario"], 2);
        if (Listddl.Count > 0)
        {
            ddPeriodoAc.Items.Insert(0, new ListItem("Seleccione"));
            ddlPeriodoAn.Items.Insert(0, new ListItem("Seleccione"));
            ddPeriodoAc.Width = 152;
            foreach (var item in Listddl)
            {
                ddPeriodoAc.Items.Add(item.fecha.ToShortDateString());
                ddPeriodoAc.Items.FindByText(item.fecha.ToShortDateString()).Value = item.fecha.ToShortDateString();

                ddlPeriodoAn.Items.Add(item.fecha.ToShortDateString());
                ddlPeriodoAn.Items.FindByText(item.fecha.ToShortDateString()).Value = item.fecha.ToShortDateString();
            }
        }
        Listddl = null;
        Listddl = objeEfectivo.ListarDdllServices((Usuario)Session["usuario"], 1);
        if (Listddl.Count > 0)
        {
            Listddl.Insert(0, new EstFlujoEfectivo { descripcion = "Seleccione", valor1 = -1 });
            foreach (var item in Listddl)
            {
                ddlCentroCosto.Items.Add(item.descripcion.ToString());
                ddlCentroCosto.Items.FindByText(item.descripcion).Value = item.valor1.ToString();
            }
        }
    }

    DataTable crearDataTableFlujo()
    {
        List<CambioPatrimonio> lista = new List<CambioPatrimonio>();
        lista = (List<CambioPatrimonio>)Session["DTPATRIMONIOS"];

        DataTable tabla = new DataTable();
        tabla.Columns.Add("DescripcionMovimiento");
        tabla.Columns.Add("AportesSociales");
        tabla.Columns.Add("Reservas");
        tabla.Columns.Add("FondosDestinacion");
        tabla.Columns.Add("Valorizacion");
        tabla.Columns.Add("ExedentesNetos");
        tabla.Columns.Add("Totales");

        foreach (CambioPatrimonio item in lista)
        {
            DataRow datarw;
            datarw = tabla.NewRow();
            datarw[0] = item.Descripcion_Moviminto;
            datarw[1] = item.aporte_Sociales;
            datarw[2] = item.reservas;
            datarw[3] = item.fondos_Destinacion_especificas.ToString("n0");
            datarw[4] = item.valorizacion;
            datarw[5] = item.excedentes_netos;
            datarw[6] = item.totales;
            tabla.Rows.Add(datarw);
        }
        return tabla;
     }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0)
            {

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                System.IO.StringWriter sw = new System.IO.StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.DataSource = Session["DTPATRIMONIOS"];
                gvLista.DataBind();
                gvLista.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvLista);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename= CambioPatrimonio.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = System.Text.Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
                gvLista.AllowPaging = true;
                gvLista.DataBind();
            }
            else
            {
                VerError("No existen Datos");
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch
        {
        }
    }

    protected void btnInforme_Click(object sender, EventArgs e) 
    {
        DataTable tabla = crearDataTableFlujo();

        Configuracion conf = new Configuracion();
        VerError("");
        if (Session["DTPATRIMONIOS"] == null)
        {
            lblInfo.Text = "No ha generado el Libro Diario Columnario para poder imprimir el reporte";
        }
        if (Session["DTPATRIMONIOS"] != null)
        {

            List<CambioPatrimonio> lista = new List<CambioPatrimonio>();
            lista = (List<CambioPatrimonio>)Session["DTPATRIMONIOS"];

            Usuario pUsuario = (Usuario)Session["usuario"];

            Microsoft.Reporting.WebForms.ReportParameter[] param = new Microsoft.Reporting.WebForms.ReportParameter[6];

            param[0] = new Microsoft.Reporting.WebForms.ReportParameter("entidad", pUsuario.empresa);
            param[1] = new Microsoft.Reporting.WebForms.ReportParameter("fecha",DateTime.Now.ToShortDateString());
            param[2] = new Microsoft.Reporting.WebForms.ReportParameter("ImagenReport", ImagenReporte());
            param[3] = new Microsoft.Reporting.WebForms.ReportParameter("gerenteGeneral", pUsuario.representante_legal);
            param[4] = new Microsoft.Reporting.WebForms.ReportParameter("revisorFiscal", pUsuario.revisor_Fiscal);
            param[5] = new Microsoft.Reporting.WebForms.ReportParameter("contador", pUsuario.contador);

            // mvReporteFlujo.Visible = true;
            RptReporte.LocalReport.DataSources.Clear();
            RptReporte.LocalReport.EnableExternalImages = true;
            RptReporte.LocalReport.SetParameters(param);

            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", tabla);
            RptReporte.LocalReport.DataSources.Add(rds);
            
            RptReporte.LocalReport.Refresh();
            RptReporte.Visible = true;
            mvReporteFlujo.ActiveViewIndex = 1;
        }
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {
    }

    protected void btnDatos_Click(object sender, EventArgs e)
    {
        mvReporteFlujo.ActiveViewIndex = 0;
    }
}