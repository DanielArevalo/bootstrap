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
using System.Reflection;

partial class Lista : GlobalWeb
{
    private Xpinn.Contabilidad.Services.BalancePruebaService BalancePruebaSer = new Xpinn.Contabilidad.Services.BalancePruebaService();
    private Xpinn.NIIF.Services.BalanceNIIFService BalancenNiifSer = new Xpinn.NIIF.Services.BalanceNIIFService();

    private static string pCod_Programa;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Request.UrlReferrer.ToString().Contains("niif"))
            {
                pCod_Programa = BalancenNiifSer.CodigoProgramaCompTerNiif;
                ViewState.Add("COD_PROGRAMA", "NIIF");
            }
            else
            {
                pCod_Programa = BalancePruebaSer.CodigoProgramaCompTer;
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
            List<Xpinn.Contabilidad.Entities.LibroMayor> lstFechaCierre = new List<Xpinn.Contabilidad.Entities.LibroMayor>();
            Xpinn.Contabilidad.Services.LibroMayorService LibroMayorService = new Xpinn.Contabilidad.Services.LibroMayorService();
            bool rpta = ViewState["COD_PROGRAMA"] != null && ViewState["COD_PROGRAMA"].ToString() == "NIIF" ? true : false;
            lstFechaCierre = LibroMayorService.ListarFechaCorte(Usuario, rpta);
            ddlFechaCorte.DataSource = lstFechaCierre;
            ddlFechaCorte.DataTextFormatString = "{0:" + conf.ObtenerFormatoFecha() + "}";
            ddlFechaCorte.DataTextField = "fecha";
            ddlFechaCorte.DataBind();
            if (ddlFechaCorte.SelectedIndex != null)
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
            if (Session["DTBALANCE"] != null)
            {
                List<BalancePrueba> lstConsultabalance = new List<BalancePrueba>();
                lstConsultabalance = (List<BalancePrueba>)Session["DTBALANCE"];
                gvLista.DataSource = lstConsultabalance;
                gvLista.DataBind();
            }
            else
            { 
                Actualizar(idObjeto);
            }
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
            String emptyQuery = "Fila de datos vacia";
            BalancePrueba datosApp = new BalancePrueba();

            Configuracion conf = new Configuracion();
            String format = conf.ObtenerFormatoFecha();
            datosApp.fecha = DateTime.ParseExact(ddlFechaCorte.SelectedValue, format, CultureInfo.InvariantCulture);

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

            // Determinar el nivel
            datosApp.nivel = 9999;

            // Determina si se muestran las cuentas que tienen saldo cero
            if (chkCuentasCero.Checked == true)
            {
                datosApp.cuentascero = 1;
            }
            else
            {
                datosApp.cuentascero = 0;
            }

            // Determina si se hace comparativo por centro de costo
            datosApp.comparativo = 0;

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
            datosApp.cod_moneda = Convert.ToInt64(ddlMoneda.Value);

            // Generar el balance de prueba 
            List<BalancePrueba> lstConsultabalance = new List<BalancePrueba>();
            lstConsultabalance.Clear();
            bool rpta = ViewState["COD_PROGRAMA"] != null && ViewState["COD_PROGRAMA"].ToString() == "NIIF" ? true : false;
            if (rpta)
                lstConsultabalance = BalancenNiifSer.ListarBalanceComprobacionTerNiif(datosApp, Usuario);
            else
                lstConsultabalance = BalancePruebaSer.ListarBalanceComprobacionTer(datosApp, Usuario);
            // Mostrar los datos
            gvLista.EmptyDataText = emptyQuery;
            Session["DTBALANCE"] = lstConsultabalance;
            gvLista.DataSource = lstConsultabalance;
            if (lstConsultabalance.Count > 0)
            {
                mvBalance.ActiveViewIndex = 0;
                gvLista.DataBind();
                btnExportar.Visible = true;
                btnInforme.Visible = true;
            }
            else
            {
                mvBalance.ActiveViewIndex = -1;
                btnExportar.Visible = false;
                btnInforme.Visible = false;
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
        List<Xpinn.Contabilidad.Entities.BalancePrueba> lstConsultabalance = new List<Xpinn.Contabilidad.Entities.BalancePrueba>();
        lstConsultabalance = (List<Xpinn.Contabilidad.Entities.BalancePrueba>)Session["DTBALANCE"];

        // LLenar data table con los datos a recoger
        table.Columns.Add("CodCuenta");
        table.Columns.Add("NombreCuenta");
        DataColumn cSaldoInicial = new DataColumn();
        cSaldoInicial.ColumnName = "Saldo_inicial";
        cSaldoInicial.AllowDBNull = true;
        cSaldoInicial.DataType = typeof(decimal);
        table.Columns.Add(cSaldoInicial);
        DataColumn cDebitos = new DataColumn();
        cDebitos.ColumnName = "Debitos";
        cDebitos.AllowDBNull = true;
        cDebitos.DataType = typeof(decimal);
        table.Columns.Add(cDebitos);
        DataColumn cCreditos = new DataColumn();
        cCreditos.ColumnName = "Creditos";
        cCreditos.AllowDBNull = true;
        cCreditos.DataType = typeof(decimal);
        table.Columns.Add(cCreditos);
        DataColumn cSaldoFinal = new DataColumn();
        cSaldoFinal.ColumnName = "Saldo_final";
        cSaldoFinal.AllowDBNull = true;
        cSaldoFinal.DataType = typeof(decimal);
        table.Columns.Add(cSaldoFinal);

        foreach (BalancePrueba item in lstConsultabalance)
        {
            DataRow datarw;
            datarw = table.NewRow();
            if (item.tercero == "-")
            { 
                datarw[0] = item.cod_cuenta;
                datarw[1] = item.nombrecuenta;
            }
            else
            {
                datarw[0] = item.tercero;
                datarw[1] = item.nombreTercero;
            }
            datarw[2] = item.saldo_inicial.ToString("N2");
            datarw[3] = item.debitos.ToString("N2");
            datarw[4] = item.creditos.ToString("N2");
            datarw[5] = item.saldo_final.ToString("N2");
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
            VerError("No ha generado el  balance de prueba para poder imprimir el reporte");
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

            ReportDataSource rds = new ReportDataSource("DataSetBalanceComprobacion", CrearDataTablebalance());

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
                List<BalancePrueba> lstListado = new List<BalancePrueba>();
                lstListado = (List<BalancePrueba>)Session["DTBALANCE"];
                var lstConsulta = (from s in lstListado select new {s.cod_cuenta, s.nombrecuenta, s.tercero, s.nombreTercero, s.saldo_inicial, s.debitos, s.creditos, s.saldo_final }).ToList();
                string fic = "Balance_Comprobacion_Tercero.csv";
                try
                {
                    File.Delete(fic);
                }
                catch
                {
                }
                // Generar el archivo
                bool bTitulos = false;
                System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("") + fic, true);
                foreach (var item in lstConsulta)
                {
                    string texto = "";
                    FieldInfo[] propiedades = item.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                    if (!bTitulos)
                    {
                        foreach (FieldInfo f in propiedades)
                        {
                            try
                            {
                                texto += f.Name.Split('>').First().Replace("<", "") + ";";
                            }
                            catch { texto += ";"; };
                        }
                        sw.WriteLine(texto);
                        bTitulos = true;
                    }
                    texto = "";
                    int i = 0;
                    foreach (FieldInfo f in propiedades)
                    {
                        i += 1;
                        object valorObject = f.GetValue(item);
                        // Si no soy nulo
                        if (valorObject != null)
                        {
                            string valorString = valorObject.ToString();
                            if (valorObject is DateTime)
                            {
                                DateTime? fechaValidar = valorObject as DateTime?;
                                if (fechaValidar.Value != DateTime.MinValue)
                                {
                                    texto += f.GetValue(item) + ";";
                                }
                                else
                                {
                                    texto += "" + ";";
                                }
                            }
                            else
                            {
                                texto += f.GetValue(item) + ";";
                                texto.Replace("\r", "").Replace(";", "");
                            }
                        }
                        else
                        {
                            texto += "" + ";";
                        }
                    }
                    sw.WriteLine(texto);
                }
                sw.Close();
                System.IO.StreamReader sr;
                sr = File.OpenText(Server.MapPath("") + fic);
                string texo = sr.ReadToEnd();
                sr.Close();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.ContentType = "text/plain";
                HttpContext.Current.Response.Write(texo);
                HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + fic);
                HttpContext.Current.Response.Flush();
                File.Delete(Server.MapPath("") + fic);
                HttpContext.Current.Response.End();
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
        this.Lblmensaje.Text = "";
    }



    protected void ddlFechaCorte_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BalancePrueba mes13 = new BalancePrueba();
            mes13.fecha = DateTime.ParseExact(ddlFechaCorte.SelectedItem.Text, gFormatoFecha, null);
            bool rpta = ViewState["COD_PROGRAMA"] != null && ViewState["COD_PROGRAMA"].ToString() == "NIIF" ? true : false;
            if (rpta)
                mes13 = BalancenNiifSer.ConsultarBalanceMes13(mes13, Usuario);
            else
                mes13 = BalancePruebaSer.ConsultarBalanceMes13(mes13, Usuario);
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
            if (identificacion == "-") 
            {
                e.Row.Font.Bold = true;
            }
        }
    }

}