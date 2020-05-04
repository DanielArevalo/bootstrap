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
using Microsoft.Reporting.WebForms;
using Microsoft.CSharp;
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    private Xpinn.Contabilidad.Services.LibroDiarioService LibroDiarioSer = new Xpinn.Contabilidad.Services.LibroDiarioService();
    private static string pCod_Programa;
    Xpinn.Contabilidad.Services.LibroMayorService LibroMayorSer = new Xpinn.Contabilidad.Services.LibroMayorService();

    Usuario _usuario;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Request.UrlReferrer.ToString().Contains("niif"))
            {
                pCod_Programa = LibroDiarioSer.CodigoProgramaNiif;
                ViewState.Add("COD_PROGRAMA", "NIIF");
            }
            else
            {
                pCod_Programa = LibroDiarioSer.CodigoPrograma;
                ViewState.Add("COD_PROGRAMA", "LOCAL");
            }
            VisualizarOpciones(pCod_Programa, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pCod_Programa, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                chkCuenta.Checked = true;
                chkFecha.Checked = false;
                btnExportar.Visible = false;
                btnInforme.Visible = false;
                btnDatos.Visible = false;
                LlenarCombos();
                VerError("");
                Configuracion conf = new Configuracion();
                try
                {
                    Xpinn.Comun.Services.CiereaService cierreServicio = new Xpinn.Comun.Services.CiereaService();
                    DateTime pFecIni;
                    if (ViewState["COD_PROGRAMA"] != null && ViewState["COD_PROGRAMA"].ToString() == "LOCAL")
                        pFecIni = cierreServicio.FechaUltimoCierre("C", (Usuario)Session["Usuario"]).AddDays(1);
                    else
                        pFecIni = cierreServicio.FechaUltimoCierre("G", (Usuario)Session["Usuario"]).AddDays(1);
                    txtFecIni.ToDateTime = pFecIni;
                }
                catch
                {
                    VerError("No se pudo determinar fecha de cierre inicial");
                }
                txtFecFin.ToDateTime = System.DateTime.Now;
                CargarValoresConsulta(pConsulta, pCod_Programa);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pCod_Programa, "Page_Load", ex);
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
        ddlCentroCosto.DataSource = LstCentroCosto;
        ddlCentroCosto.DataTextField = "nom_centro";
        ddlCentroCosto.DataValueField = "centro_costo";
        ddlCentroCosto.DataBind();
        ddlCentroCosto.Items.Insert(0, new ListItem("CONSOLIDADO", "0"));

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

    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, pCod_Programa);
        Actualizar();
        ddlCentroCosto.Enabled = false;
        chkFecha.Enabled = false;
        chkCuenta.Enabled = false;
        txtFecIni.Enabled = false;
        txtFecFin.Enabled = false;
        btnExportar.Visible = true;
        btnInforme.Visible = true;
        btnDatos.Visible = true;
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, pCod_Programa);
        ddlCentroCosto.Enabled = true;
        chkFecha.Enabled = true;
        chkCuenta.Enabled = true;
        txtFecIni.Enabled = true;
        txtFecFin.Enabled = true;
        gvLista.DataSource = null;
        gvLista.DataBind();
        gvListaO.DataSource = null;
        gvListaO.DataBind();
        btnExportar.Visible = false;
        btnInforme.Visible = false;
        btnDatos.Visible = false;
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
            BOexcepcion.Throw(pCod_Programa, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void gvListaO_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvListaO.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pCod_Programa, "gvListaO_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        VerError("");

        try
        {
            List<Xpinn.Contabilidad.Entities.LibroDiario> lstConsulta = new List<Xpinn.Contabilidad.Entities.LibroDiario>();
            Int64 CenIni = Int64.MinValue;
            Int64 CenFin = Int64.MinValue;
            DateTime FecIni;
            DateTime FecFin;

            // Determinando el centro de costo
            if (ddlCentroCosto.SelectedValue.ToString() == "0")
            {
                try
                {
                    if (Session["CenIni"] != null && Session["CenFin"] != null)
                    {
                        CenIni = Convert.ToInt64(Session["CenIni"].ToString());
                        CenFin = Convert.ToInt64(Session["CenFin"].ToString());
                    }
                    else
                    {
                        Xpinn.Contabilidad.Services.CentroCostoService CCSer = new Xpinn.Contabilidad.Services.CentroCostoService();
                        CCSer.RangoCentroCosto(ref CenIni, ref CenFin, (Usuario)Session["Usuario"]);
                    }
                }
                catch (Exception ex)
                {
                    VerError(ex.Message);
                    return;
                }
            }
            else
            {
                try
                {
                    CenIni = Convert.ToInt64(ddlCentroCosto.SelectedValue);
                    CenFin = Convert.ToInt64(ddlCentroCosto.SelectedValue);
                }
                catch (Exception ex)
                {
                    VerError(ex.Message);
                    return;
                }
            }

            // determinar los datos para generar el libro auxiliar
            FecIni = Convert.ToDateTime(txtFecIni.ToDate);
            FecFin = Convert.ToDateTime(txtFecFin.ToDate);

            // determinar si se genera por cuenta o por fecha
            Boolean bCuenta = true;
            if (chkCuenta.Checked == true)
                bCuenta = true;
            else
                bCuenta = false;

            // Determinando la fecha Inicial y Final
            if (ViewState["COD_PROGRAMA"] != null)
            {
                if (ViewState["COD_PROGRAMA"].ToString() == "NIIF")
                    lstConsulta = LibroDiarioSer.ListarDiarioNiif(CenIni, CenFin, FecIni, FecFin, bCuenta, Convert.ToInt64(ddlMoneda.Value), (Usuario)Session["Usuario"]);
                else
                    lstConsulta = LibroDiarioSer.ListarDiario(CenIni, CenFin, FecIni, FecFin, bCuenta, Convert.ToInt64(ddlMoneda.Value), (Usuario)Session["Usuario"]);
            }
            else
                lstConsulta = LibroDiarioSer.ListarDiario(CenIni, CenFin, FecIni, FecFin, bCuenta, Convert.ToInt64(ddlMoneda.Value), (Usuario)Session["Usuario"]);

            if (chkFecha.Checked == true)
            {
                gvListaO.Visible = false;
                gvLista.Visible = true; 
                gvLista.PageSize = pageSize;
                gvLista.EmptyDataText = emptyQuery;
                ViewState["DTLIBRODIARIO"] = lstConsulta;
                gvLista.DataSource = lstConsulta;

                if (lstConsulta.Count > 0)
                {
                    gvLista.Visible = true;
                    lblTotalRegs.Visible = true;
                    lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                    gvLista.DataBind();                    
                }
                else
                {
                    gvLista.Visible = false;
                    lblTotalRegs.Visible = false;
                }
            }
            else
            {
                gvLista.Visible = false;
                gvListaO.Visible = true;                
                gvListaO.PageSize = pageSize;
                gvListaO.EmptyDataText = emptyQuery;
                ViewState["DTLIBRODIARIO"] = lstConsulta;
                gvListaO.DataSource = lstConsulta;

                if (lstConsulta.Count > 0)
                {
                    gvListaO.Visible = true;
                    lblTotalRegs.Visible = true;
                    lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                    gvListaO.DataBind();
                    ValidarPermisosGrilla(gvListaO);
                }
                else
                {
                    gvListaO.Visible = false;
                    lblTotalRegs.Visible = false;
                }
            }

            Session.Add(pCod_Programa + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pCod_Programa, "Actualizar", ex);
        }
    }


    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["DTLIBRODIARIO"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                if (chkFecha.Checked == true)
                {
                    gvLista.AllowPaging = false;
                    gvLista.DataSource = ViewState["DTLIBRODIARIO"];
                    gvLista.DataBind();
                    gvLista.EnableViewState = false;
                }
                else
                {
                    gvListaO.AllowPaging = false;
                    gvListaO.DataSource = ViewState["DTLIBRODIARIO"];
                    gvListaO.DataBind();
                    gvListaO.EnableViewState = false;
                }
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                if (chkFecha.Checked == true)
                    form.Controls.Add(gvLista);
                else
                    form.Controls.Add(gvListaO);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
                if (chkFecha.Checked == true)
                {
                    gvLista.AllowPaging = true;
                    gvLista.DataBind();
                }
                else
                {
                    gvListaO.AllowPaging = true;
                    gvListaO.DataBind();
                }
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
        Configuracion conf = new Configuracion();
        VerError("");
        if (ViewState["DTLIBRODIARIO"] == null)
        {
            VerError("No ha generado el libro diario para poder imprimir el reporte");
        }

        List<Xpinn.Contabilidad.Entities.LibroDiario> lstConsulta = new List<Xpinn.Contabilidad.Entities.LibroDiario>();
        lstConsulta = (List<Xpinn.Contabilidad.Entities.LibroDiario>)ViewState["DTLIBRODIARIO"];

        // LLenar data table con los datos a recoger
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("fecha");
        table.Columns.Add("cod_cuenta");
        table.Columns.Add("nombre");
        table.Columns.Add("debito", typeof(double));
        table.Columns.Add("credito", typeof(double));

        DataRow datarw;
        if (lstConsulta.Count == 0)
        {
            datarw = table.NewRow();
            for (int i = 0; i <= 22; i++)
            {
                datarw[i] = " ";
            }
            table.Rows.Add(datarw);
        }
        else
        {
            foreach (Xpinn.Contabilidad.Entities.LibroDiario refe in lstConsulta)
            {
                datarw = table.NewRow();
                if (refe.fecha == null)
                    datarw[0] = " ";
                else
                    datarw[0] = " " + refe.fecha.ToString(gFormatoFecha);
                datarw[1] = " " + refe.cod_cuenta;
                datarw[2] = " " + refe.nombrecuenta;
                datarw[3] = " " + refe.debito;
                datarw[4] = " " + refe.credito;
                table.Rows.Add(datarw);
            }
        }

      
        // ---------------------------------------------------------------------------------------------------------
        // Pasar datos al reporte
        // ---------------------------------------------------------------------------------------------------------
        ReportParameter[] param = new ReportParameter[10];

        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
     

        param[0] = new ReportParameter("entidad", pUsuario.empresa);
        param[1] = new ReportParameter("nit", pUsuario.nitempresa);
        param[2] = new ReportParameter("fecini", txtFecIni.ToDate);
        param[3] = new ReportParameter("fecfin", txtFecFin.ToDate);
        param[4] = new ReportParameter("centro_costo", ddlCentroCosto.SelectedItem.ToString());
        param[5] = new ReportParameter("ImagenReport", ImagenReporte());


        param[6] = new ReportParameter("representante_legal", pUsuario.representante_legal);
        param[7] = new ReportParameter("contador", pUsuario.contador);
        param[8] = new ReportParameter("tarjeta_contador", pUsuario.tarjeta_contador);

        param[9] = new ReportParameter("RevisorFiscal", pUsuario.revisor_Fiscal);


        rvLibAux.LocalReport.EnableExternalImages = true;
        rvLibAux.LocalReport.SetParameters(param);

        ReportDataSource rds = new ReportDataSource("DataSet1", table);
        rvLibAux.LocalReport.DataSources.Clear();
        if (chkFecha.Checked == true)
            rvLibAux.LocalReport.ReportPath = "Page/Contabilidad/LibroDiario/ReporteLibroDiario.rdlc";
        else
            rvLibAux.LocalReport.ReportPath = "Page/Contabilidad/LibroDiario/ReporteLibroDiarioCta.rdlc";
        rvLibAux.LocalReport.DataSources.Add(rds);
        rvLibAux.LocalReport.Refresh();

        // Mostrar el reporte en pantalla.
        mvLibroDiario.ActiveViewIndex = 1;

    }


    protected void btnDatos_Click(object sender, EventArgs e)
    {
        mvLibroDiario.ActiveViewIndex = 0;
    }

    protected void chkFecha_CheckedChanged(object sender, EventArgs e)
    {
        if (chkFecha.Checked == true)
        {
            chkCuenta.Checked = false;
        }
        else
        {
            chkCuenta.Checked = true;
        }
    }

    protected void chkCuenta_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCuenta.Checked == true)
        {
            chkFecha.Checked = false;
        }
        else
        {
            chkFecha.Checked = true;
        }
    }
}