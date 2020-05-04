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
    private static string pCodigo;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["niif"] != null)
            {
                VisualizarOpciones(objeEfectivo.CodigoProgramaNIIF, "L");
                pCodigo = objeEfectivo.CodigoProgramaNIIF;
            }
            else
            {
                VisualizarOpciones(objeEfectivo.CodigoPrograma, "L");
                pCodigo = objeEfectivo.CodigoPrograma;
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
            List<EstFlujoEfectivo> lstConsulta = new List<EstFlujoEfectivo>();
            DateTime fechaActual = Convert.ToDateTime(ddPeriodoAc.SelectedValue), fechaAnterio = Convert.ToDateTime(ddlPeriodoAn.SelectedValue);

            int pOpcion = Request.QueryString["niif"] != null ? 2 : 1;
            lstConsulta = objeEfectivo.getListaReporGridvServices((Usuario)Session["usuario"], fechaActual, fechaAnterio, Convert.ToInt16(ddlCentroCosto.SelectedValue), pOpcion);
            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista2.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            gvLista2.DataSource = lstConsulta;
            Session["DTPARAMETROS"] = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                gvLista2.DataBind();
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

    List<DataTable> crearDataTableFlujo()
    {
        List<DataTable> listaTabla = new List<DataTable>();
        List<EstFlujoEfectivo> lista = new List<EstFlujoEfectivo>();
        lista = (List<EstFlujoEfectivo>)Session["DTPARAMETROS"];

        DataTable tabla = new DataTable();
        tabla.Columns.Add("ActividadesOperativas");
        tabla.Columns.Add("Año2015");
        tabla.Columns.Add("Año2014");
        tabla.Columns.Add("Variacion");
        foreach (EstFlujoEfectivo item in lista)
        {
            DataRow datarw;
            datarw = tabla.NewRow();
            datarw[0] = item.descripcion;
            datarw[1] = item.valor1;
            datarw[2] = item.valor2;
            datarw[3] = item.variacion != 0 ? item.variacion.ToString("n0") : "0";
            tabla.Rows.Add(datarw);
        }
        listaTabla.Add(tabla);

        DataTable tabla2 = new DataTable();
        tabla2.Columns.Add("ConciliacionEfectivo");
        tabla2.Columns.Add("Caja");
        tabla2.Columns.Add("BancosComerciales");
        tabla2.Columns.Add("Total");

        foreach (EstFlujoEfectivo item in lista)
        {
            DataRow datarw;
            datarw = tabla2.NewRow();
            datarw[0] = item.Descripcion2;
            datarw[1] = item.caja;
            datarw[2] = item.bancos_Comerciales;
            datarw[3] = item.total;
            tabla2.Rows.Add(datarw);
        }
        listaTabla.Add(tabla2);
        return listaTabla;
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
                gvLista.DataSource = Session["DTPARAMETROS"];
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
                Response.AddHeader("Content-Disposition", "attachment;filename=FlujoEfectivo.xls");
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
        DataTable tabla = crearDataTableFlujo()[0];
        DataTable tabla2 = crearDataTableFlujo()[1];


        Configuracion conf = new Configuracion();
        VerError("");
        if (Session["DTPARAMETROS"] == null)
        {
            lblInfo.Text = "No ha generado el Libro Diario Columnario para poder imprimir el reporte";
        }
        if (Session["DTPARAMETROS"] != null)
        {

            List<EstFlujoEfectivo> lista = new List<EstFlujoEfectivo>();
            lista = (List<EstFlujoEfectivo>)Session["DTPARAMETROS"];

            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["usuario"];

            Microsoft.Reporting.WebForms.ReportParameter[] param = new Microsoft.Reporting.WebForms.ReportParameter[4];
            param[0] = new Microsoft.Reporting.WebForms.ReportParameter("entidad", pUsuario.empresa);
            param[1] = new Microsoft.Reporting.WebForms.ReportParameter("nit", pUsuario.nitempresa);
            param[2] = new Microsoft.Reporting.WebForms.ReportParameter("fecha", ddPeriodoAc.SelectedValue);
            param[3] = new Microsoft.Reporting.WebForms.ReportParameter("ImagenReport", ImagenReporte());

            // mvReporteFlujo.Visible = true;
            RptReporte.LocalReport.DataSources.Clear();
            RptReporte.LocalReport.EnableExternalImages = true;
            RptReporte.LocalReport.SetParameters(param);



            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", tabla);
            Microsoft.Reporting.WebForms.ReportDataSource rdss = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", tabla2);
            RptReporte.LocalReport.DataSources.Add(rds);
            RptReporte.LocalReport.DataSources.Add(rdss); 
            
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