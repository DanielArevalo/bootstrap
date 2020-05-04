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
    private Xpinn.Contabilidad.Services.PyGCompService PyGCompSer = new Xpinn.Contabilidad.Services.PyGCompService();
    private static string pCodigo;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["niif"] != null)
            {
                VisualizarOpciones(PyGCompSer.CodigoProgramaNIIF, "L");
                pCodigo = PyGCompSer.CodigoProgramaNIIF;
            }
            else
            {
                VisualizarOpciones(PyGCompSer.CodigoPrograma, "L");
                pCodigo = PyGCompSer.CodigoPrograma;
            }

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
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
                btnInforme.Visible = false;
                btnExportar.Visible = false;
                LlenarCombos();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pCodigo, "Page_Load", ex);
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

        // Llenar el DDL de la fecha 1
        List<Xpinn.Contabilidad.Entities.PyGComp> lstFechaCierre = new List<Xpinn.Contabilidad.Entities.PyGComp>();
        Xpinn.Contabilidad.Services.PyGCompService PyGCompService = new Xpinn.Contabilidad.Services.PyGCompService();
        Xpinn.Contabilidad.Entities.PyGComp PyGComp = new Xpinn.Contabilidad.Entities.PyGComp();
        lstFechaCierre = PyGCompService.ListarFecha((Usuario)Session["Usuario"]);
        ddlFecha1.DataSource = lstFechaCierre;
        ddlFecha2.DataSource = lstFechaCierre;
        ddlFechaa3.DataSource = lstFechaCierre;
        ddlFecha1.DataTextFormatString = "{0:" + GlobalWeb.gFormatoFecha + "}";
        ddlFecha2.DataTextFormatString = "{0:" + GlobalWeb.gFormatoFecha + "}";
        ddlFechaa3.DataTextFormatString = "{0:" + GlobalWeb.gFormatoFecha + "}";
        ddlFecha1.DataTextField = "fechaprimerper";
        ddlFecha2.DataTextField = "fechaprimerper";
        ddlFechaa3.DataTextField = "fechaprimerper";
        ddlFecha1.DataBind();
        ddlFecha2.DataBind();
        ddlFechaa3.DataBind();
        ddlFechaa3.Items.Insert(0, new ListItem("", "01/01/0001"));

    }


    /// <summary>
    /// Método para el botón de consulta que permite generar el reporte
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, pCodigo);
        Actualizar(idObjeto);
        Lblerror.Visible = false;
        ddlFecha1.Enabled = false;
        ddlFecha2.Enabled = false;
        ddlFechaa3.Enabled = false;
        ddlNivel.Enabled = false;
        ddlcentrocosto.Enabled = false;
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        mvBalanceComp.ActiveViewIndex = 0;
        LimpiarValoresConsulta(pConsulta, pCodigo);
        gvLista.DataSource = null;
        gvLista.DataBind();
        Lblerror.Visible = false;

        ddlcentrocosto.Enabled = true;
        ddlFecha1.Enabled = true;
        ddlFecha2.Enabled = true;
        ddlFechaa3.Enabled = true;
        ddlNivel.Enabled = true;
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
            BOexcepcion.Throw(pCodigo, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar(String pIdObjeto)
    {

        try
        {
            String emptyQuery = "Fila de datos vacia";
            PyGComp datosApp = new PyGComp();

            String format = GlobalWeb.gFormatoFecha;
            datosApp.fechaprimerper = DateTime.ParseExact(this.ddlFecha1.SelectedValue, format, CultureInfo.InvariantCulture);
            datosApp.fechasegunper = DateTime.ParseExact(this.ddlFecha2.SelectedValue, format, CultureInfo.InvariantCulture);

            // Determinar fecha vacia 

            if (ddlFechaa3.SelectedValue == "01/01/0001")
            {

                datosApp.fechatercerper = DateTime.ParseExact(this.ddlFechaa3.SelectedValue, format, CultureInfo.InvariantCulture);
            }
            else
            {

                datosApp.fechatercerper = DateTime.ParseExact(this.ddlFechaa3.SelectedValue, format, CultureInfo.InvariantCulture);
            }
            datosApp.cod_moneda = Convert.ToInt64(ddlMoneda.Value);
            // Determinar el rango de centros de costo
            try
            {
                if (ddlcentrocosto.SelectedValue.ToString() == "0")
                {
                    if (Session["CenIni"] != null && Session["CenFin"] != null)
                    {
                        datosApp.centro_costoini = Convert.ToInt64(Session["CenIni"].ToString());
                        datosApp.centro_costofin = Convert.ToInt64(Session["CenFin"].ToString());
                    }
                    else
                    {
                        Xpinn.Contabilidad.Services.CentroCostoService CCSer = new Xpinn.Contabilidad.Services.CentroCostoService();
                        Int64 cenini = 0;
                        Int64 cenfin = 0;
                        CCSer.RangoCentroCosto(ref cenini, ref cenfin, (Usuario)Session["Usuario"]);
                        datosApp.centro_costoini = cenini;
                        datosApp.centro_costofin = cenfin;
                    }
                }
                else
                {
                    datosApp.centro_costoini = Convert.ToInt64(ddlcentrocosto.SelectedValue);
                    datosApp.centro_costofin = Convert.ToInt64(ddlcentrocosto.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
                return;
            }

            // Determinar el nivel
            datosApp.nivel = Convert.ToInt64(ddlNivel.SelectedItem.Text);

            // Generar el libro de Balance comparativo
            List<PyGComp> lstConsultapygcomp = new List<PyGComp>();
            lstConsultapygcomp.Clear();
            int pOpcion = Request.QueryString["niif"] != null ? 2 : 1;
            lstConsultapygcomp = PyGCompSer.ListarPyGComparativo(datosApp, (Usuario)Session["usuario"], pOpcion);
            gvLista.EmptyDataText = emptyQuery;
            Session["DTPYGCOMP"] = lstConsultapygcomp;

            gvLista.DataSource = lstConsultapygcomp;
            if (lstConsultapygcomp.Count > 0)
            {
                btnInforme.Visible = true;
                btnExportar.Visible = true;
                if (ddlFechaa3.SelectedValue == "01/01/0001")
                {
                    gvLista.Columns[8].Visible = false;
                    gvLista.Columns[9].Visible = false;
                    gvLista.Columns[10].Visible = false;
                    gvLista.Columns[11].Visible = false;
                }
                mvBalanceComp.ActiveViewIndex = 0;                
                gvLista.Columns[2].HeaderText = datosApp.fechaprimerper.ToShortDateString();
                gvLista.Columns[4].HeaderText = datosApp.fechasegunper.ToShortDateString();
                gvLista.Columns[8].HeaderText = Convert.ToDateTime(datosApp.fechatercerper).ToShortDateString();
                gvLista.DataBind();
            }
            else
            {
                btnInforme.Visible = false;
                btnExportar.Visible = false;
                mvBalanceComp.ActiveViewIndex = -1;
            }

            Session.Add(PyGCompSer.CodigoPrograma + ".consulta", 1);
            ddlcentrocosto.Enabled = true;
            ddlFecha1.Enabled = true;
            ddlFecha2.Enabled = true;
            ddlFechaa3.Enabled = true;
        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(pCodigo, "Actualizar", ex);
        }


    }

    private Xpinn.Util.Usuario ObtenerValores()
    {
        Xpinn.Util.Usuario vUsuario = new Xpinn.Util.Usuario();

        return vUsuario;
    }


    public DataTable CrearDataTablebalancecomp()
    {

        List<Xpinn.Contabilidad.Entities.PyGComp> lstPyGComp = new List<Xpinn.Contabilidad.Entities.PyGComp>();
        lstPyGComp = (List<Xpinn.Contabilidad.Entities.PyGComp>)Session["DTPYGCOMP"];

        // LLenar data table con los datos a recoger
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("CodCuenta");
        table.Columns.Add("NombreCuenta");
        table.Columns.Add("Balance1");
        table.Columns.Add("Porpart1", typeof(Decimal));
        table.Columns.Add("Balance2");
        table.Columns.Add("Porpart2", typeof(Decimal));
        table.Columns.Add("Diferencia");
        table.Columns.Add("Dif1", typeof(Decimal));
        table.Columns.Add("Balance3");
        table.Columns.Add("Porpart3", typeof(Decimal));
        table.Columns.Add("Diferencia2");
        table.Columns.Add("Dif2");


        foreach (PyGComp item in lstPyGComp)
        {
            CultureInfo ci = new CultureInfo("en-us");
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.cod_cuenta;
            datarw[1] = item.nombrecuenta;
            datarw[2] = item.balance1.ToString("##,##0");
            datarw[3] = item.porcpart1;
            datarw[4] = item.balance2.ToString("##,##0");
            datarw[5] = item.porcpart2;
            datarw[6] = item.diferencia.ToString("##,##0");
            datarw[7] = item.porcdif;
            datarw[8] = item.balance3.ToString("##,##0");
            datarw[9] = item.porcpart3;
            datarw[10] = item.diferencia2.ToString("##,##0");
            datarw[11] = item.porcdif2.ToString("%#0.00");
            table.Rows.Add(datarw);
        }
        return table;
    }

    protected void btnInforme_Click(object sender, EventArgs e)
    {

        Configuracion conf = new Configuracion();
        VerError("");
        if (Session["DTPYGCOMP"] == null)
        {
            Lblerror.Text = "No ha generado el PyG para poder imprimir el reporte";
            return;
        }
        if (Session["DTPYGCOMP"] != null)
        {

            List<Xpinn.Contabilidad.Entities.PyGComp> lstPyGComp = new List<Xpinn.Contabilidad.Entities.PyGComp>();
            lstPyGComp = (List<Xpinn.Contabilidad.Entities.PyGComp>)Session["DTPYGCOMP"];

            Usuario pUsuario = (Usuario)Session["Usuario"];

            ReportParameter[] param = new ReportParameter[12];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);  
            param[2] = new ReportParameter("fecha", Convert.ToString(DateTime.Now.ToShortDateString()));
            if (ddlFechaa3.SelectedValue == "01/01/0001")
                param[3] = new ReportParameter("periodoter", "0");
            else
                param[3] = new ReportParameter("periodoter", "1");
            param[4] = new ReportParameter("fecha1", ddlFecha1.SelectedValue);
            param[5] = new ReportParameter("fecha2", ddlFecha2.SelectedValue);
            param[6] = new ReportParameter("fecha3", ddlFechaa3.SelectedValue);
            param[7] = new ReportParameter("representante_legal", pUsuario.representante_legal);
            param[8] = new ReportParameter("contador", pUsuario.contador);
            param[9] = new ReportParameter("tarjeta_contador", pUsuario.tarjeta_contador);
            param[10] = new ReportParameter("ImagenReport", ImagenReporte());
            param[11] = new ReportParameter("RevisorFiscal", pUsuario.revisor_Fiscal);

            mvBalanceComp.Visible = true;
            RptReporte.LocalReport.EnableExternalImages = true;
            RptReporte.LocalReport.SetParameters(param);
            RptReporte.LocalReport.DataSources.Clear();
            RptReporte.LocalReport.Refresh();

            ReportDataSource rds = new ReportDataSource("DataSetPyGComp", CrearDataTablebalancecomp());

            RptReporte.LocalReport.DataSources.Add(rds);
            RptReporte.Visible = true;
            mvBalanceComp.ActiveViewIndex = 1;
        }

    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTPYGCOMP"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.DataSource = Session["DTPYGCOMP"];
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
                Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
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


    protected void ddlFechaa3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFechaa3.SelectedValue == "01/01/0001")
        {
            gvLista.Columns[8].Visible = false;
            gvLista.Columns[9].Visible = false;
            gvLista.Columns[10].Visible = false;
            gvLista.Columns[11].Visible = false;
        }
        else
        {
            gvLista.Columns[8].Visible = true;
            gvLista.Columns[9].Visible = true;
            gvLista.Columns[10].Visible = true;
            gvLista.Columns[11].Visible = true;
        }
    }

    protected void btnDatos_Click(object sender, EventArgs e)
    {        
        mvBalanceComp.ActiveViewIndex = 0;
    }

}