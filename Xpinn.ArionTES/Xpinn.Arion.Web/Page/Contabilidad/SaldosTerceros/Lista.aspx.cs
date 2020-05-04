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
    private Xpinn.Contabilidad.Services.SaldosTercerosService SaldosTercerosSer = new Xpinn.Contabilidad.Services.SaldosTercerosService();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(SaldosTercerosSer.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            LimpiarValoresConsulta(pConsulta, SaldosTercerosSer.CodigoPrograma);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SaldosTercerosSer.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarCombos();
                Configuracion conf = new Configuracion();
                txtFechaIni.Text = System.DateTime.Now.ToString(conf.ObtenerFormatoFecha());
                txtFechaFin.Text = System.DateTime.Now.ToString(conf.ObtenerFormatoFecha());
                btnInforme.Visible = false;
                btnExportar.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SaldosTercerosSer.CodigoPrograma, "Page_Load", ex);
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
        ddlcentrocosto.Items.Insert(0, new ListItem("TODOS", "0"));

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

    /// <summary>
    /// Consultar los datos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {        
        Actualizar(idObjeto);
        Lblerror.Visible = false;                
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, SaldosTercerosSer.CodigoPrograma);
        gvLista.DataSource = null;
        gvLista.DataBind();
        Lblerror.Visible = false;

        txtCodCuenta.Enabled = true;
        ddlcentrocosto.Enabled = true;
        txtFechaIni.Enabled = true;
        txtFechaFin.Enabled = true;
        chkConsolidado.Enabled = true;    
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
            BOexcepcion.Throw(SaldosTercerosSer.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Generar el de saldos de terceros
    /// </summary>
    /// <param name="pIdObjeto"></param>
    private void Actualizar(String pIdObjeto)
    {
        if (txtFechaIni.Text == "")
        {
            String Error1 = "Fecha no valida";
            this.Lblerrorfechain.Text = Error1;
        }
        if (txtFechaFin.Text == "")
        {
            String Error1 = "Fecha no valida";
            this.Lblerrorfechafin.Text = Error1;
        }
        else
        {
            this.Lblerrorfechain.Text = "";
            this.Lblerrorfechafin.Text = "";
            try
            {
                String emptyQuery = "Fila de datos vacia";
                SaldosTerceros datosApp = new SaldosTerceros();

                Configuracion conf = new Configuracion();
                String format = conf.ObtenerFormatoFecha();
                datosApp.fechaini = DateTime.ParseExact(txtFechaIni.Text, format, CultureInfo.InvariantCulture);
                datosApp.fechafin = DateTime.ParseExact(txtFechaFin.Text, format, CultureInfo.InvariantCulture);
                datosApp.cod_cuenta = Convert.ToString(txtCodCuenta.Text);

                // Determinar el rango de centros de costo
                try
                {
                    if (ddlcentrocosto.SelectedValue.ToString() == "0")
                    {
                        if (Session["CenIni"] != null && Session["CenFin"] != null)
                        {
                            datosApp.centro_costo = Convert.ToInt64(Session["CenIni"].ToString());
                            datosApp.centro_costo_fin = Convert.ToInt64(Session["CenFin"].ToString());
                        }
                        else
                        {
                            Xpinn.Contabilidad.Services.CentroCostoService CCSer = new Xpinn.Contabilidad.Services.CentroCostoService();
                            Int64 cenini = 0;
                            Int64 cenfin = 0;
                            CCSer.RangoCentroCosto(ref cenini, ref cenfin, (Usuario)Session["Usuario"]);
                            datosApp.centro_costo = cenini;
                            datosApp.centro_costo_fin = cenfin;
                        }
                    }
                    else
                    {
                        datosApp.centro_costo = Convert.ToInt64(ddlcentrocosto.SelectedValue);
                        datosApp.centro_costo_fin = Convert.ToInt64(ddlcentrocosto.SelectedValue);
                    }
                }
                catch (Exception ex)
                {
                    VerError(ex.Message);
                    return;
                }
                datosApp.cod_moneda = Convert.ToInt64(ddlMoneda.Value);

                // Determina si se hace consolidado por centro de costo
                Double SalIni = 0;
                Double TotDeb = 0;
                Double TotCre = 0;
                Double SalFin = 0;

                if (chkConsolidado.Checked == true)
                {
                    // Generar el libro de Terceros consolidado
                    List<SaldosTerceros> lstConsultasaldos = new List<SaldosTerceros>();
                    lstConsultasaldos.Clear();
                    lstConsultasaldos = SaldosTercerosSer.ListarSaldoConsolidado(datosApp, ref SalIni, ref TotDeb, ref TotCre, ref SalFin, (Usuario)Session["usuario"]);
                    gvLista.EmptyDataText = emptyQuery;
                    Session["DTSALDOSTER"] = lstConsultasaldos;

                    gvLista.DataSource = lstConsultasaldos;
                    if (lstConsultasaldos.Count > 0)
                    {
                        SaldosTerceros rTotales = new SaldosTerceros();
                        rTotales.nombrecuenta = "TOTALES";
                        rTotales.saldoinicial = SalIni;
                        rTotales.debito = TotDeb;
                        rTotales.credito = TotCre;
                        rTotales.saldofinal = SalFin;
                        lstConsultasaldos.Add(rTotales);

                        lblInfo.Visible = false;
                        gvLista.Visible = true;
                        mvSaldosTer.ActiveViewIndex = 0;
                        gvLista.DataBind();
                        btnInforme.Visible = true;
                        btnExportar.Visible = true;
                    }
                    else
                    {
                        lblInfo.Visible = true;
                        lblInfo.Text = "No se encontraron registros";
                        btnInforme.Visible = false;
                        btnExportar.Visible = false;
                        gvLista.Visible = false;
                        lblTotalRegs.Visible = false;
                    }
                }

                if (chkConsolidado.Checked == false)
                {
                    // Generar el libro de Terceros consolidado
                    List<SaldosTerceros> lstConsultasaldoster = new List<SaldosTerceros>();
                    lstConsultasaldoster.Clear();
                    lstConsultasaldoster = SaldosTercerosSer.ListarSaldosTerceros(datosApp, ref SalIni, ref TotDeb, ref TotCre, ref SalFin, (Usuario)Session["usuario"]);
                    gvLista.EmptyDataText = emptyQuery;
                    Session["DTSALDOSTER"] = lstConsultasaldoster;

                    gvLista.DataSource = lstConsultasaldoster;
                    if (lstConsultasaldoster.Count > 0)
                    {
                        SaldosTerceros rTotales = new SaldosTerceros();
                        rTotales.nombrecuenta = "TOTALES";
                        rTotales.saldoinicial = SalIni;
                        rTotales.debito = TotDeb;
                        rTotales.credito = TotCre;
                        rTotales.saldofinal = SalFin;
                        lstConsultasaldoster.Add(rTotales);

                        lblInfo.Visible = false;
                        gvLista.Visible = true;
                        lblTotalRegs.Visible = true;
                        lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultasaldoster.Count.ToString();
                        gvLista.DataBind();
                        btnInforme.Visible = true;
                        btnExportar.Visible = true;
                    }
                    else
                    {
                        lblInfo.Visible = true;
                        lblInfo.Text = "No se encontraron registros";
                        gvLista.Visible = false;                        
                        lblTotalRegs.Visible = false;
                        btnInforme.Visible = false;
                        btnExportar.Visible = false;
                    }

                    Session.Add(SaldosTercerosSer.CodigoPrograma + ".consulta", 1);
                }
                txtCodCuenta.Enabled = false;
                ddlcentrocosto.Enabled = false;
                txtFechaIni.Enabled = false;
                txtFechaFin.Enabled = false;
                chkConsolidado.Enabled = false;

            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(SaldosTercerosSer.CodigoPrograma, "Actualizar", ex);
            }
        }        
    }

    /// <summary>
    /// Crear datatable con datos del libro
    /// </summary>
    /// <returns></returns>  
    public DataTable CrearDataTablesaldos()
    {

        List<Xpinn.Contabilidad.Entities.SaldosTerceros> lstConsultasaldos = new List<Xpinn.Contabilidad.Entities.SaldosTerceros>();
        lstConsultasaldos = (List<Xpinn.Contabilidad.Entities.SaldosTerceros>)Session["DTSALDOSTER"];

        // LLenar data table con los datos a recoger
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("CodCuenta");
        table.Columns.Add("NombreCuenta");
        table.Columns.Add("CodTercero");
        table.Columns.Add("IdentTercero");
        table.Columns.Add("NomTercero");
        table.Columns.Add("SaldoInicial", typeof(double));
        table.Columns.Add("Debito", typeof(double));
        table.Columns.Add("Credito", typeof(double));
        table.Columns.Add("SaldoFinal", typeof(double));

        foreach (SaldosTerceros item in lstConsultasaldos)
        {
            if (item.nombrecuenta != "TOTALES")
            {
                DataRow datarw;
                datarw = table.NewRow();
                datarw[0] = item.cod_cuenta;
                datarw[1] = item.nombrecuenta;
                datarw[2] = item.codtercero;
                datarw[3] = item.identercero;
                datarw[4] = item.nombretercero;
                datarw[5] = item.saldoinicial.ToString();
                datarw[6] = item.debito.ToString();
                datarw[7] = item.credito.ToString();
                datarw[8] = item.saldofinal.ToString();
                table.Rows.Add(datarw);
            }
        }

        return table;
    }

    /// <summary>
    /// Generar el informe
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInforme_Click(object sender, EventArgs e)
    {
       
        Configuracion conf = new Configuracion();
        VerError("");
        if (Session["DTSALDOSTER"] == null)
        {
            Lblerror.Text="No ha generado el Saldos Terceros para poder imprimir el reporte";  
        }
        if (Session["DTSALDOSTER"] != null)
        {
            List<Xpinn.Contabilidad.Entities.SaldosTerceros> lstConsultasaldos = new List<Xpinn.Contabilidad.Entities.SaldosTerceros>();
            lstConsultasaldos = (List<Xpinn.Contabilidad.Entities.SaldosTerceros>)Session["DTSALDOSTER"];
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            ReportParameter[] param = new ReportParameter[5];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);
            param[2] = new ReportParameter("fecha_inicial", txtFechaIni.Text);
            param[3] = new ReportParameter("fecha_final", txtFechaFin.Text);
            param[4] = new ReportParameter("ImagenReport", ImagenReporte());

            RptReporte.LocalReport.EnableExternalImages = true;
            mvSaldosTer.Visible = true;
            RptReporte.LocalReport.SetParameters(param);
            RptReporte.LocalReport.DataSources.Clear();
            RptReporte.LocalReport.Refresh();

            ReportDataSource rds = new ReportDataSource("DataSetSaldosterc", CrearDataTablesaldos());

            RptReporte.LocalReport.DataSources.Add(rds);
            frmPrint.Visible = false;
            RptReporte.Visible = true;
            mvSaldosTer.ActiveViewIndex = 1;
        }    
    }

    /// <summary>
    /// Exportar el libro a excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTSALDOSTER"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.DataSource = Session["DTSALDOSTER"];
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
                Response.AddHeader("Content-Disposition", "attachment;filename=SaldosTerceros.xls");
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
    }

    /// <summary>
    /// Determinar si se genera consolidado para todas las cuentas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkConsolidado_CheckedChanged(object sender, EventArgs e)
    {
        if (chkConsolidado.Checked == true)
            txtCodCuenta.Enabled = false;
        else
            txtCodCuenta.Enabled = true;
    }
    
    /// <summary>
    /// Devolverse al tab de datos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDatos_Click(object sender, EventArgs e)
    {
        mvSaldosTer.ActiveViewIndex = 0;
    }

    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true, "txtCodCuenta", "txtNomCuenta");
    }

    protected void txtCodCuenta_TextChanged(object sender, EventArgs e)
    {
        if (txtCodCuenta.Text != "")
        {
            // Determinar los datos de la cuenta contable
            Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
            Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta.Text, (Usuario)Session["usuario"]);
            //int rowIndex = Convert.ToInt32(txtCodCuenta.CommandArgument);

            // Mostrar el nombre de la cuenta            
            if (txtNomCuenta != null)
                txtNomCuenta.Text = PlanCuentas.nombre;
        }
        else
        {
            txtNomCuenta.Text = "";
        }
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


}