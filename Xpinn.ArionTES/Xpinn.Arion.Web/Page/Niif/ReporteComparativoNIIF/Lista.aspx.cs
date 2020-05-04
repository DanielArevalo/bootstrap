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
using Xpinn.Contabilidad.Entities;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using Microsoft.CSharp;
using System.Text;
using System.IO;

partial class Lista : GlobalWeb
{
    private Xpinn.NIIF.Services.PlanCuentasNIIFService BOReportePlan = new Xpinn.NIIF.Services.PlanCuentasNIIFService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(BOReportePlan.CodigoProgramaRepComp, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BOReportePlan.CodigoProgramaRepComp, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarCombos();
                btnExportar.Visible = false;
                btnInforme.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BOReportePlan.CodigoProgramaRepComp, "Page_Load", ex);
        }
    }

    /// <summary>
    /// Método para llenar los DDLs requeridos para las consultas
    /// </summary>
    protected void LlenarCombos()
    {
        // LLenar el DDl de centro de costo
        Xpinn.Contabilidad.Services.CentroCostoService CentroCostoService = new Xpinn.Contabilidad.Services.CentroCostoService();
        List<Xpinn.Contabilidad.Entities.CentroCosto> LstCentroCosto = new List<Xpinn.Contabilidad.Entities.CentroCosto>();
        string sFiltro = "";
        LstCentroCosto = CentroCostoService.ListarCentroCosto((Usuario)Session["Usuario"], sFiltro);
        ddlcentrocosto.DataSource = LstCentroCosto;
        ddlcentrocosto.DataTextField = "nom_centro";
        ddlcentrocosto.DataValueField = "centro_costo";
        ddlcentrocosto.DataBind();
        ddlcentrocosto.Items.Insert(0, new ListItem("CONSOLIDADO", "0"));
        // Determinando el centro de costo inicial y final
        Int64 CenIni = Int64.MinValue;
        Int64 CenFin = Int64.MinValue;
        foreach (Xpinn.Contabilidad.Entities.CentroCosto ItemCC in LstCentroCosto)
        {
            if (CenIni == Int64.MinValue)
                CenIni = ItemCC.centro_costo;
            if (CenFin == Int64.MinValue)
                CenFin = ItemCC.centro_costo;
            if (CenIni > ItemCC.centro_costo)
                CenIni = ItemCC.centro_costo;
            if (CenFin < ItemCC.centro_costo)
                CenFin = ItemCC.centro_costo;
        }
        Session["CenIni"] = CenIni;
        Session["CenFin"] = CenFin;

        // Llenar el DDL de la fecha de corte 
        try
        {
            Configuracion conf = new Configuracion();
            List<Xpinn.NIIF.Entities.BalanceNIIF> lstFechaCierre = new List<Xpinn.NIIF.Entities.BalanceNIIF>();
            Xpinn.NIIF.Services.BalanceNIIFService BOReportePlanvice = new Xpinn.NIIF.Services.BalanceNIIFService();
            lstFechaCierre = BOReportePlanvice.ListarFechaCorte((Usuario)Session["Usuario"]);
            ddlFechaCorte.DataSource = lstFechaCierre;
            ddlFechaCorte.DataTextFormatString = "{0:" + conf.ObtenerFormatoFecha() + "}";
            ddlFechaCorte.DataTextField = "fecha";
            ddlFechaCorte.DataBind();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, BOReportePlan.CodigoProgramaRepComp);
        Actualizar(idObjeto);
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, BOReportePlan.CodigoProgramaRepComp);
        gvLista.DataSource = null;
        gvLista.DataBind();
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar(idObjeto);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BOReportePlan.CodigoProgramaRepComp, "gvLista_PageIndexChanging", ex);
        }
    }

    protected string obtFiltro()
    {
        string pFiltro = string.Empty;
        Configuracion conf = new Configuracion();
        ConnectionDataBase conexion = new ConnectionDataBase();

        if (ddlFechaCorte.SelectedItem != null)
        {
            if (conexion.TipoConexion() == "ORACLE")
                pFiltro += " AND BN.FECHA = TO_DATE('" + ddlFechaCorte.SelectedItem.Text + "','" + conf.ObtenerFormatoFecha() + "')";
            else
                pFiltro += " AND BN.FECHA = '" + ddlFechaCorte.SelectedItem.Text + "'";
        }
        // Determinar el rango de centros de costo
        try
        {
            if (ddlcentrocosto.SelectedValue.ToString() == "0")
            {
                if (Session["CenIni"] != null && Session["CenFin"] != null)
                {
                    pFiltro += " AND BN.CENTRO_COSTO BETWEEN " + Convert.ToInt32(Session["CenIni"].ToString()) + " AND " + Convert.ToInt32(Session["CenFin"].ToString());
                }
                else
                {
                    Xpinn.Contabilidad.Services.CentroCostoService CCSer = new Xpinn.Contabilidad.Services.CentroCostoService();
                    Int64 cenini = 0;
                    Int64 cenfin = 0;
                    CCSer.RangoCentroCosto(ref cenini, ref cenfin, (Usuario)Session["Usuario"]);
                    pFiltro += " AND BN.CENTRO_COSTO BETWEEN " + cenini + " AND " + cenfin;
                }
            }
            else
            {
                pFiltro += " AND BN.CENTRO_COSTO BETWEEN " + ddlcentrocosto.SelectedValue + " AND " + ddlcentrocosto.SelectedValue;
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            return null;
        }

        pFiltro += " AND P.NIVEL <= " + ddlNivel.SelectedItem.Text;
        pFiltro += " AND P.COD_MONEDA = " + ddlMoneda.Value;

        if (!string.IsNullOrEmpty(pFiltro))
        {
            pFiltro = pFiltro.Substring(4);
            pFiltro = "WHERE " + pFiltro;
        }
        return pFiltro;
    }


    private void Actualizar(String pIdObjeto)
    {
        try
        {
            String emptyQuery = "Fila de datos vacia";
            string pFiltro = obtFiltro();

            List<Xpinn.NIIF.Entities.PlanCuentasNIIF> lstRpteComp = new List<Xpinn.NIIF.Entities.PlanCuentasNIIF>();
            lstRpteComp.Clear();
            lstRpteComp = BOReportePlan.ListarReporteComparativoNIIF(pFiltro, (Usuario)Session["usuario"]);
            // Mostrar los datos
            gvLista.EmptyDataText = emptyQuery;
            Session["DTBALANCE"] = lstRpteComp;
            gvLista.DataSource = lstRpteComp;
            if (lstRpteComp.Count > 0)
            {
                gvLista.DataBind();
                btnExportar.Visible = true;
                btnInforme.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstRpteComp.Count;
                lblInfo.Visible = false;
            }
            else
            {
                btnExportar.Visible = false;
                btnInforme.Visible = false;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }
            Session.Add(BOReportePlan.CodigoProgramaRepComp + ".consulta", 1);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BOReportePlan.CodigoProgramaRepComp, "Actualizar", ex);
        }
    }


    public DataTable CrearDataTablebalance()
    {
        System.Data.DataTable table = new System.Data.DataTable();

        List<Xpinn.NIIF.Entities.PlanCuentasNIIF> lstConsultabalance = new List<Xpinn.NIIF.Entities.PlanCuentasNIIF>();
        lstConsultabalance = (List<Xpinn.NIIF.Entities.PlanCuentasNIIF>)Session["DTBALANCE"];

        // LLenar data table con los datos a recoger        
        table.Columns.Add("cod_cuenta_niif");
        table.Columns.Add("nombre_niif");
        table.Columns.Add("tipo");
        table.Columns.Add("saldo");
        table.Columns.Add("cod_cuenta");
        table.Columns.Add("nombre_local");
        table.Columns.Add("saldo_local");
        table.Columns.Add("diferencia");

        foreach (Xpinn.NIIF.Entities.PlanCuentasNIIF item in lstConsultabalance)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.cod_cuenta_niif;
            datarw[1] = item.nombre;
            datarw[2] = item.tipo;
            datarw[3] = item.saldo.ToString("##,##0");
            datarw[4] = item.cod_cuenta;
            datarw[5] = item.nombre_local;
            datarw[6] = item.saldo_local.ToString("##,##0");
            datarw[7] = item.diferencia.ToString("##,##0");
            table.Rows.Add(datarw);
        }

        return table;
    }

    protected void btnInforme_Click(object sender, EventArgs e)
    {

        Configuracion conf = new Configuracion();
        VerError("");
        if (Session["DTBALANCE"] == null)
        {
            VerError("No ha generado el  reporte para poder imprimir");
            return;
        }
        if (Session["DTBALANCE"] != null)
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            ReportParameter[] param = new ReportParameter[4];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);
            param[2] = new ReportParameter("fecha", ddlFechaCorte.SelectedValue);
            param[3] = new ReportParameter("ImagenReport", ImagenReporte());

            RptReporte.LocalReport.EnableExternalImages = true;
            mvBalance.Visible = true;
            RptReporte.LocalReport.SetParameters(param);
            RptReporte.LocalReport.DataSources.Clear();
            RptReporte.LocalReport.Refresh();

            ReportDataSource rds = new ReportDataSource("DataSet1", CrearDataTablebalance());

            RptReporte.LocalReport.DataSources.Add(rds);
            RptReporte.Visible = true;
            mvBalance.ActiveViewIndex = 1;
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTBALANCE"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.DataSource = Session["DTBALANCE"];
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
                Response.AddHeader("Content-Disposition", "attachment;filename=ReporteComparativoNIIF.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
                gvLista.AllowPaging = true;
                gvLista.DataBind();
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

    protected void btnDatos_Click(object sender, EventArgs e)
    {
        mvBalance.ActiveViewIndex = 0;
    }


}