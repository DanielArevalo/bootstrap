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
    
    private Xpinn.Contabilidad.Services.BalanceTercerosService BalanceTercerosSer = new Xpinn.Contabilidad.Services.BalanceTercerosService();
    private static string pCod_Programa;
    private Xpinn.NIIF.Services.BalanceNIIFService BalancenNiifSer = new Xpinn.NIIF.Services.BalanceNIIFService();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Request.UrlReferrer.ToString().Contains("niif"))
            {
                VisualizarOpciones(BalanceTercerosSer.CodigoProgramaNIIF, "L");
                pCod_Programa = BalanceTercerosSer.CodigoProgramaNIIF;
                ViewState.Add("COD_PROGRAMA", "NIIF");
            }
            else
            {
                VisualizarOpciones(BalanceTercerosSer.CodigoPrograma, "L");
                pCod_Programa = BalanceTercerosSer.CodigoPrograma;
                ViewState.Add("COD_PROGRAMA", "LOCAL");
            }
            

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoImprimir += btnInforme_Click;
            toolBar.MostrarExportar(false);
            toolBar.MostrarImprimir(false);
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
                LlenarCombos();
                btnExportar.Visible = false;
                btnInforme.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pCod_Programa, "Page_Load", ex);
        }
    }

    private bool EsNiif()
    {
        if (ViewState["COD_PROGRAMA"] != null)
            if (ViewState["COD_PROGRAMA"].ToString() == "NIIF")
                return true;
        return false;
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
            List<Xpinn.Contabilidad.Entities.BalanceTerceros> lstFechaCierre = new List<Xpinn.Contabilidad.Entities.BalanceTerceros>();
            Xpinn.Contabilidad.Services.BalanceTercerosService BalanceTercerosService = new Xpinn.Contabilidad.Services.BalanceTercerosService();
            Xpinn.Contabilidad.Entities.BalanceTerceros BalanceTerceros = new Xpinn.Contabilidad.Entities.BalanceTerceros();
            lstFechaCierre = BalanceTercerosService.ListarFechaCierreDefinitivo((EsNiif() ? "G" : "C"), (Usuario)Session["Usuario"]);
            ddlFechaCorte.DataSource = lstFechaCierre;
            ddlFechaCorte.DataTextFormatString = "{0:" + conf.ObtenerFormatoFecha() + "}";
            ddlFechaCorte.DataTextField = "fecha";
            ddlFechaCorte.DataBind();
            if (ddlFechaCorte.SelectedIndex != int.MinValue)
                ddlFechaCorte_SelectedIndexChanged(ddlFechaCorte, null);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
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
        try
        {
            //String emptyQuery = "Fila de datos vacia";
            BalanceTerceros datosApp = new BalanceTerceros();

            Configuracion conf = new Configuracion();
            String format = conf.ObtenerFormatoFecha();
            datosApp.fecha = DateTime.ParseExact(ddlFechaCorte.SelectedValue, format, CultureInfo.InvariantCulture);
            datosApp.centro_costo = Convert.ToInt64(ddlcentrocosto.SelectedValue);
            datosApp.centro_costo_fin = 99999;
            datosApp.mostrarmovper13 = 0;
            datosApp.nivel = 99;
            datosApp.esniif = EsNiif();


            // Determina si genera mes13
            if (chkmes13.Checked == true)
            {
                datosApp.mostrarmovper13 = 1;
                this.Lblmensaje.Text = "";
            }
            else
            {
                datosApp.mostrarmovper13 = 0;
                this.Lblmensaje.Text = "";
            }


            // Determinar el rango de centros de costo          
            List<Xpinn.Contabilidad.Entities.CentroCosto> lstCentrosCosto = new List<CentroCosto>();            
            List<BalanceTerceros> lstConsulta = new List<BalanceTerceros>();
            lstConsulta.Clear();
            
            lstConsulta = BalanceTercerosSer.ListarBalance(datosApp, (Usuario)Session["usuario"]);



            if (lstConsulta.Count > 0)
            {
                if (txtCodCuenta.Text != "" || ddlTercero.Text != "")
                {
                    List<BalanceTerceros> lstConsultaFiltra = new List<BalanceTerceros>();
                    lstConsultaFiltra = lstConsulta;
                    if (txtCodCuenta.Text != "")
                        lstConsultaFiltra = lstConsultaFiltra.Where(x => x.cod_cuenta.StartsWith(txtCodCuenta.Text)).ToList();
                    if (ddlTercero.Text != "")
                        lstConsultaFiltra = lstConsultaFiltra.Where(x => x.tercero == ddlTercero.Text).ToList();
                    gvLista.DataSource = lstConsultaFiltra;
                    gvLista.DataBind();
                    Session["DTBALANCE"] = lstConsultaFiltra;
                }
                else
                {
                    gvLista.DataSource = lstConsulta;
                    gvLista.DataBind();
                    Session["DTBALANCE"] = lstConsulta;
                }
                Site toolBar = (Site)this.Master;
                toolBar.MostrarExportar(true);
                toolBar.MostrarImprimir(true);
            }
            else
            {
                Site toolBar = (Site)this.Master;
                toolBar.MostrarExportar(false);
                toolBar.MostrarImprimir(false);
                Session["DTBALANCE"] = null;
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


    protected void chkCuentasCero_CheckedChanged(object sender, EventArgs e)
    {
        //if (Session["DTBALANCE"] != null)
        //Actualizar(idObjeto); 
    }

    protected void chkCompCentroCosto_CheckedChanged(object sender, EventArgs e)
    {
        //if (Session["DTBALANCE"] != null)
        // Actualizar(idObjeto); 
    }

    /// <summary>
    /// Crear DATATABLE con el listado de cuentas para poder generar el reporte
    /// </summary>
    /// <returns></returns>
    public DataTable CrearDataTablebalance()
    {
        System.Data.DataTable table = new System.Data.DataTable();
        //if (chkCompCentroCosto.Checked == true)
        //{            
        //    table = (System.Data.DataTable)Session["DTBALANCE"];
        //}
        //else
        //{
            List<Xpinn.Contabilidad.Entities.BalanceTerceros> lstConsultabalance = new List<Xpinn.Contabilidad.Entities.BalanceTerceros>();
            lstConsultabalance = (List<Xpinn.Contabilidad.Entities.BalanceTerceros>)Session["DTBALANCE"];

            // LLenar data table con los datos a recoger
            table.Columns.Add("CodCuenta");
            table.Columns.Add("NombreCuenta");
            DataColumn cValor = new DataColumn();
            cValor.ColumnName = "Valor";
            cValor.AllowDBNull = true;
            cValor.DataType = typeof(decimal);
            table.Columns.Add(cValor);
            table.Columns.Add("Tercero");
            table.Columns.Add("Nombre_Tercero");

            foreach (BalanceTerceros item in lstConsultabalance)
            {
                DataRow datarw;
                datarw = table.NewRow();
                datarw[0] = item.cod_cuenta;
                datarw[1] = item.nombrecuenta;
                datarw[2] = item.SaldoFin.ToString("###,##0");
                datarw[3] = item.tercero;
                datarw[4] = item.nombreTercero;
                table.Rows.Add(datarw);
        }
        //}
        return table;
    }

    protected void btnInforme_Click(object sender, EventArgs e)
    {

        Configuracion conf = new Configuracion();
        VerError("");
        if (Session["DTBALANCE"] == null)
        {
            VerError("No ha generado el  balance Terceros para poder imprimir el reporte");
            return;
        }
        if (Session["DTBALANCE"] != null)
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            ReportParameter[] param = new ReportParameter[8];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);
            param[2] = new ReportParameter("fecha", ddlFechaCorte.SelectedValue);
            if (ddlcentrocosto.SelectedValue == "0")
                param[3] = new ReportParameter("centro_costo", ".");
            else
                param[3] = new ReportParameter("centro_costo", ddlcentrocosto.SelectedItem.Text);
            param[4] = new ReportParameter("representante_legal", pUsuario.representante_legal);
            param[5] = new ReportParameter("contador", pUsuario.contador);
            param[6] = new ReportParameter("tarjeta_contador", pUsuario.tarjeta_contador);
            param[7] = new ReportParameter("ImagenReport", ImagenReporte());

            RptReporte.LocalReport.EnableExternalImages = true;
            mvBalance.Visible = true;
            RptReporte.LocalReport.SetParameters(param);
            RptReporte.LocalReport.DataSources.Clear();
            RptReporte.LocalReport.Refresh();

            ReportDataSource rds = new ReportDataSource("DataSetBalanceTer", CrearDataTablebalance());

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
                Response.AddHeader("Content-Disposition", "attachment;filename=BalanceTerceros.xls");
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

    protected void chkmes13_CheckedChanged(object sender, EventArgs e)
    {
        //this.Lblmensaje.Text = "";    
    }

    protected void ddlFechaCorte_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            BalanceTerceros mes13 = new BalanceTerceros();
            mes13.fecha = DateTime.ParseExact(ddlFechaCorte.SelectedItem.Text, gFormatoFecha, null);
            bool rpta = ViewState["COD_PROGRAMA"] != null && ViewState["COD_PROGRAMA"].ToString() == "NIIF" ? true : false;
            if (rpta)
                mes13 =  BalanceTercerosSer.ConsultarBalanceMes13(mes13, Usuario);
            else
                mes13 = BalanceTercerosSer.ConsultarBalanceMes13(mes13, Usuario);
            if (mes13.fecha != null)
            {
                chkmes13.Visible = true;
                String Mensaje = "Desea generar con fecha de mes 13";
                this.Lblmensaje.Text = Mensaje;
                mes13.fecha = mes13.fechames13cons;
            }
            else
            {
                chkmes13.Checked = false;
                chkmes13.Visible = false;
                Lblmensaje.Text = "";
            }
        }
        catch
        {
        }



       
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string identificacion = e.Row.Cells[2].Text.ToString();
            if (identificacion == " ")
            {
                e.Row.Font.Bold = true; 
            }
        }
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

    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true, "txtCodCuenta", "txtNomCuenta");
    }

    protected void btnListadoPlan1_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true, "txtCodCuentaFin", "txtNomCuenta1");
    }

    protected void btnTercero_Click(object sender, EventArgs e)
    {
        ctlBusquedaTercero.Motrar(true, "ddlTercero", "");
    }

}