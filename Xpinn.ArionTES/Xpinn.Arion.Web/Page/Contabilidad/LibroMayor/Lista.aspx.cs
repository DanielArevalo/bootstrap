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
    Xpinn.Contabilidad.Services.LibroMayorService LibroMayorSer = new Xpinn.Contabilidad.Services.LibroMayorService();
    Xpinn.Contabilidad.Services.BalancePruebaService BalancePruebaSer = new Xpinn.Contabilidad.Services.BalancePruebaService();
    Xpinn.Contabilidad.Services.BalanceGeneralService BalanceGeneralSer = new Xpinn.Contabilidad.Services.BalanceGeneralService();
    private Xpinn.NIIF.Services.BalanceNIIFService BalancenNiifSer = new Xpinn.NIIF.Services.BalanceNIIFService();
    Usuario _usuario;
    private static string pCod_Programa;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Request.UrlReferrer.ToString().Contains("niif"))
            {
                pCod_Programa = LibroMayorSer.CodigoProgramaNiif;
                ViewState.Add("COD_PROGRAMA", "NIIF");
            }
            else
            {
                pCod_Programa = LibroMayorSer.CodigoPrograma;
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
            _usuario = (Usuario)Session["usuario"];

            if (!IsPostBack)
            {
                gvLista.Visible = false;
                btnInforme.Visible = false;
                btnExportar.Visible = false;
                LlenarCombos();
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
        LstCentroCosto = CentroCostoService.ListarCentroCosto(_usuario, sFiltro);
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
        List<Xpinn.Contabilidad.Entities.LibroMayor> lstFechaCierre = new List<Xpinn.Contabilidad.Entities.LibroMayor>();
        Xpinn.Contabilidad.Services.LibroMayorService LibroMayorService = new Xpinn.Contabilidad.Services.LibroMayorService();
        Xpinn.Contabilidad.Entities.LibroMayor LibroMayor = new Xpinn.Contabilidad.Entities.LibroMayor();
        bool rpta = ViewState["COD_PROGRAMA"] != null && ViewState["COD_PROGRAMA"].ToString() == "NIIF" ? true : false; 
        lstFechaCierre = LibroMayorService.ListarFechaCorte(_usuario, rpta);
        ddlFechaCorte.DataSource = lstFechaCierre;
        ddlFechaCorte.DataTextFormatString = "{0:dd/MM/yyyy}";
        ddlFechaCorte.DataTextField = "fecha";
        ddlFechaCorte.DataBind();

    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, pCod_Programa);
        Actualizar(idObjeto);
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, pCod_Programa);
        gvLista.DataSource = null;
        gvLista.DataBind();
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pCod_Programa + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[pCod_Programa + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[pCod_Programa + ".id"] = id;
        Navegar(Pagina.Editar);
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
            BOexcepcion.Throw(pCod_Programa, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar(String pIdObjeto)
    {
        VerError("");
        try
        {
            String emptyQuery = "Fila de datos vacia";
            LibroMayor datosApp = new LibroMayor();

            String format = "dd/MM/yyyy";
            datosApp.fecha_corte = DateTime.ParseExact(ddlFechaCorte.SelectedValue, format, CultureInfo.InvariantCulture);

            // Determinar el rango de centros de costo
            try
            {
                if (ddlcentrocosto.SelectedValue.ToString() == "0")
                {
                    if (Session["CenIni"] != null && Session["CenFin"] != null)
                    {
                        datosApp.cenini = Convert.ToInt64(Session["CenIni"].ToString());
                        datosApp.cenfin = Convert.ToInt64(Session["CenFin"].ToString());
                    }
                    else
                    {
                        Xpinn.Contabilidad.Services.CentroCostoService CCSer = new Xpinn.Contabilidad.Services.CentroCostoService();
                        Int64 cenini = 0;
                        Int64 cenfin = 0;
                        CCSer.RangoCentroCosto(ref cenini, ref cenfin, _usuario);
                        datosApp.cenfin = cenini;
                        datosApp.cenfin = cenfin;
                    }
                }
                else
                {
                    datosApp.cenini = Convert.ToInt64(ddlcentrocosto.SelectedValue);
                    datosApp.cenfin = Convert.ToInt64(ddlcentrocosto.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
                return;
            }

            // Determinar el nivel
            datosApp.nivel = Convert.ToInt64(ddlNivel.SelectedItem.Text);

            // determinar si se muestran cuentas en ceros
            if (chkCuentasCero.Checked == true)
            {
                datosApp.mostrarceros = 1;
            }
            else
            {
                datosApp.mostrarceros = 0;
            }

            // Determinar si se genera por terceros
            if (chkTerceros.Checked == true)
            {
                datosApp.generarterceros = 1;
            }
            else
            {
                datosApp.generarterceros = 0;
            }

            // Determinar si se muestra solo nivel seleccionado
            if (chkNivel.Checked == true)
            {
                datosApp.solonivel = 1;
            }
            else
            {
                datosApp.solonivel = 0;
            }

            // Determinar si se muestra excedentes
            if (chkExcedentes.Checked == true)
            {
                datosApp.excedentes = 1;
            }
            else
            {
                datosApp.excedentes = 0;
            }
            datosApp.cod_moneda = Convert.ToInt64(ddlMoneda.Value);

            // Generar el balance de prueba
            double TotDeb = 0;
            double TotCre = 0;
            List<LibroMayor> lstConsultabalance = new List<LibroMayor>();
            lstConsultabalance.Clear();
            try
            {
                if (chkmes13.Checked == true)
                {
                    datosApp.mostrarmovper13 = 1;
                 }
                else
                {
                    datosApp.mostrarmovper13 = 0;
                }
                bool isNiif = ViewState["COD_PROGRAMA"] != null && ViewState["COD_PROGRAMA"].ToString() == "NIIF" ? true : false;
                lstConsultabalance = LibroMayorSer.ListarLibroMayor(datosApp, ref TotDeb, ref TotCre, _usuario, isNiif);
                if (lstConsultabalance.Count > 0)
                {
                    LibroMayor rTotales = new LibroMayor();
                    rTotales.nombre_cuenta = "TOTALES";
                    rTotales.debito = TotDeb;
                    rTotales.credito = TotCre;
                    lstConsultabalance.Add(rTotales);
                }
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
                gvLista.Visible = false;
                btnInforme.Visible = false;
                btnExportar.Visible = false;
                return;
            }
            gvLista.EmptyDataText = emptyQuery;
            Session["DTBALANCE"] = lstConsultabalance;
            Session["TOTDEB"] = TotDeb;
            Session["TOTCRE"] = TotCre;
            gvLista.DataSource = lstConsultabalance;
            if (lstConsultabalance.Count > 0)
            {
                mvBalance.ActiveViewIndex = 0;
                gvLista.DataBind();
                gvLista.Visible = true;
                btnInforme.Visible = true;
                btnExportar.Visible = true;

                ValidarBalance(datosApp.fecha_corte);
            }
            else
            {
                gvLista.Visible = false;
                btnInforme.Visible = false;
                btnExportar.Visible = false;
            }
            Session.Add(pCod_Programa + ".consulta", 1);
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pCod_Programa, "Actualizar", ex);
        }
    }

    private Xpinn.Util.Usuario ObtenerValores()
    {
        Xpinn.Util.Usuario vUsuario = new Xpinn.Util.Usuario();

        return vUsuario;
    }


    void ValidarBalance(DateTime fechaCorte)
    {
        if (fechaCorte != null && fechaCorte != DateTime.MinValue)
        {
            string error = BalanceGeneralSer.VerificarComprobantesYCuentas(fechaCorte, _usuario);

            if (!string.IsNullOrWhiteSpace(error))
            {
                VerError(error);
            }
        }
    }


    public DataTable CrearDataTablebalance()
    {
        Configuracion conf = new Configuracion();
        VerError("");
        if (Session["DTBALANCE"] == null)
        {
            VerError("No ha generado el balance de prueba para poder imprimir el reporte");
        }

        List<Xpinn.Contabilidad.Entities.LibroMayor> lstConsultabalance = new List<Xpinn.Contabilidad.Entities.LibroMayor>();
        lstConsultabalance = (List<Xpinn.Contabilidad.Entities.LibroMayor>)Session["DTBALANCE"];

        // LLenar data table con los datos a recoger
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("cod_cuenta");
        table.Columns.Add("nombre_cuenta");
        table.Columns.Add("cod_tercero");
        table.Columns.Add("iden_tercero");
        table.Columns.Add("nom_tercero");
        table.Columns.Add("saldo_inicial_debito", typeof(double));
        table.Columns.Add("saldo_inicial_credito", typeof(double));
        table.Columns.Add("debito", typeof(double));
        table.Columns.Add("credito", typeof(double));
        table.Columns.Add("saldo_final_debito", typeof(double));
        table.Columns.Add("saldo_final_credito", typeof(double));

        foreach (LibroMayor item in lstConsultabalance)
        {
            if (item.nombre_cuenta != "TOTALES")
            {
                DataRow datarw;
                datarw = table.NewRow();
                datarw[0] = item.cod_cuenta;
                datarw[1] = item.nombre_cuenta;
                datarw[2] = item.cod_tercero;
                datarw[3] = item.iden_tercero;
                datarw[4] = item.nom_tercero;
                datarw[5] = Convert.ToDouble(item.saldo_inicial_debito).ToString("##,##0.00");
                datarw[6] = Convert.ToDouble(item.saldo_inicial_credito).ToString("##,##0.00"); ;
                datarw[7] = Convert.ToDouble(item.debito).ToString("##,##0.00");
                datarw[8] = Convert.ToDouble(item.credito).ToString("##,##0.00");
                datarw[9] = Convert.ToDouble(item.saldo_final_debito).ToString("##,##0.00");
                datarw[10] = Convert.ToDouble(item.saldo_final_credito).ToString("##,##0.00");
                table.Rows.Add(datarw);
            }
        }
        return table;
    }

    /// <summary>
    /// Método para generar el informe pasando los parámetros correspondientes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInforme_Click(object sender, EventArgs e)
    {
        string empresa = "", nit = "";
        LibroMayorSer.DatosEmpresa(ref empresa, ref nit, _usuario);

        Double TotDeb = 0;
        Double TotCre = 0;
        TotDeb = (Double)Session["TOTDEB"];
        TotCre = (Double)Session["TOTCRE"];

        ReportParameter[] param = new ReportParameter[12];
        param[0] = new ReportParameter("entidad", empresa);
        param[1] = new ReportParameter("nit", nit);
        param[2] = new ReportParameter("fecha", ddlFechaCorte.SelectedValue);
        if (ddlcentrocosto.SelectedValue == "0")
            param[3] = new ReportParameter("centro_costo", "CONSOLIDADO");
        else
            param[3] = new ReportParameter("centro_costo", ddlcentrocosto.SelectedValue);
        param[4] = new ReportParameter("TotDeb", TotDeb.ToString());
        param[5] = new ReportParameter("TotCre", TotCre.ToString());
        param[6] = new ReportParameter("ImagenReport", ImagenReporte());
        param[7] = new ReportParameter("representante_legal", _usuario.representante_legal);
        param[8] = new ReportParameter("contador", _usuario.contador);
        param[9] = new ReportParameter("tarjeta_contador", _usuario.tarjeta_contador);        
        param[10] = new ReportParameter("RevisorFiscal", _usuario.revisor_Fiscal);
        param[11] = new ReportParameter("consecutivo", ConvertirStringToInt(txtConsecutivo.Text).ToString());

        mvBalance.Visible = true;
        RptReporte.LocalReport.EnableExternalImages = true;
        RptReporte.LocalReport.SetParameters(param);
        RptReporte.LocalReport.DataSources.Clear();
        RptReporte.LocalReport.Refresh();

        ReportDataSource rds = new ReportDataSource("DataSet1", CrearDataTablebalance());

        RptReporte.LocalReport.DataSources.Add(rds);
        RptReporte.Visible = true;
        frmPrint.Visible = false;
        mvBalance.ActiveViewIndex = 1;
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
                GridView gvExportar = CopiarGridViewParaExportar(gvLista, "DTBALANCE");             
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvExportar);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=LibroMayor.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
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


    protected void btnImprime_Click(object sender, EventArgs e)
    {
        if (RptReporte.Visible == true)
        {
            //MOSTRAR REPORTE EN PANTALLA
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            byte[] bytes = RptReporte.LocalReport.Render("PDF", null, out mimeType,
                           out encoding, out extension, out streamids, out warnings);
            Usuario pUsuario = new Usuario();
            string pNomUsuario = pUsuario.nombre != "" && pUsuario.nombre != null ? "_" + pUsuario.nombre : "";
            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output" + pNomUsuario + ".pdf"),
            FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            Session["Archivo" + Usuario.codusuario] = Server.MapPath("output" + pNomUsuario + ".pdf");
            frmPrint.Visible = true;
            RptReporte.Visible = false;
        }
    }

    protected void chkmes13_CheckedChanged(object sender, EventArgs e)
    {
        chkExcedentes.Enabled = true;
        if (chkmes13.Checked)
        {
            chkExcedentes.Enabled = false;
            chkExcedentes.Checked = true;

        }
    }

    protected void  ddlFechaCorte_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkmes13.Visible = false;
        chkmes13.Checked = false;
        // Consulta si esa fecha es de mes 13 
        if (ddlFechaCorte.SelectedItem != null)
        {
            if (ddlFechaCorte.SelectedItem.Text != "")
            {
                BalancePrueba mes13 = new BalancePrueba();
                mes13.fecha = ConvertirStringToDate(ddlFechaCorte.SelectedItem.Text);
                bool isNiif = ViewState["COD_PROGRAMA"] != null && ViewState["COD_PROGRAMA"].ToString() == "NIIF" ? true : false;
                if (isNiif)
                    mes13 = BalancenNiifSer.ConsultarBalanceMes13(mes13, Usuario);
                else
                    mes13 = BalancePruebaSer.ConsultarBalanceMes13(mes13, Usuario);
                if (mes13 != null)
                {
                    if (mes13.fecha != null)
                    {
                        chkmes13.Visible = true;
                        chkmes13.Checked = false;
                    }
                }
            }
        }
    }


}