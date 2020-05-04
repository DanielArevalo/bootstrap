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
    Xpinn.NIIF.Services.BalanceNIIFService _balancePruebaSer = new Xpinn.NIIF.Services.BalanceNIIFService();
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_balancePruebaSer.CodigoProgramaBalanceNiif, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_balancePruebaSer.CodigoProgramaBalanceNiif, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["Usuario"];

            if (!IsPostBack)
            {
                LlenarCombos();
                btnExportar.Visible = false;
                btnInforme.Visible = false;
                VerificarComprobantesYCuentasNIIF();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_balancePruebaSer.CodigoProgramaBalanceNiif, "Page_Load", ex);
        }
    }

    void VerificarComprobantesYCuentasNIIF()
    {
        if (!string.IsNullOrWhiteSpace(ddlFechaCorte.SelectedValue))
        {
            DateTime dateCorte = Convert.ToDateTime(ddlFechaCorte.SelectedValue);

            if (dateCorte != DateTime.MinValue)
            {
                string mensajeDescuadre = _balancePruebaSer.VerificarComprobantesYCuentasNIIF(dateCorte, _usuario);

                if (!string.IsNullOrWhiteSpace(mensajeDescuadre))
                {
                    VerError(mensajeDescuadre);
                }
            }
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
        try
        {
            Configuracion conf = new Configuracion();
            List<Xpinn.NIIF.Entities.BalanceNIIF> lstFechaCierre = new List<Xpinn.NIIF.Entities.BalanceNIIF>();
            Xpinn.NIIF.Services.BalanceNIIFService BalancePruebaService = new Xpinn.NIIF.Services.BalanceNIIFService();
            lstFechaCierre = BalancePruebaService.ListarFechaCorte(_usuario);
            ddlFechaCorte.DataSource = lstFechaCierre;
            ddlFechaCorte.DataTextFormatString = "{0:" + conf.ObtenerFormatoFecha() + "}";
            ddlFechaCorte.DataTextField = "fecha";
            ddlFechaCorte.DataBind();
            if (ddlFechaCorte.SelectedIndex > 0)
                ddlFechaCorte_SelectedIndexChanged(ddlFechaCorte, null);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        GuardarValoresConsulta(pConsulta, _balancePruebaSer.CodigoProgramaBalanceNiif);
        Actualizar(idObjeto);
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        LimpiarValoresConsulta(pConsulta, _balancePruebaSer.CodigoProgramaBalanceNiif);
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
            BOexcepcion.Throw(_balancePruebaSer.CodigoProgramaBalanceNiif, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar(String pIdObjeto)
    {
        try
        {
            String emptyQuery = "Fila de datos vacia";
            Xpinn.NIIF.Entities.BalanceNIIF datosApp = new Xpinn.NIIF.Entities.BalanceNIIF();

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
                        datosApp.centro_costo = Convert.ToInt32(Session["CenIni"].ToString());
                        datosApp.centro_costo_fin = Convert.ToInt32(Session["CenFin"].ToString());
                    }
                    else
                    {
                        Xpinn.Contabilidad.Services.CentroCostoService CCSer = new Xpinn.Contabilidad.Services.CentroCostoService();
                        Int64 cenini = 0;
                        Int64 cenfin = 0;
                        CCSer.RangoCentroCosto(ref cenini, ref cenfin, _usuario);
                        datosApp.centro_costo = Convert.ToInt32(cenini);
                        datosApp.centro_costo_fin = Convert.ToInt32(cenfin);
                    }
                }
                else
                {
                    datosApp.centro_costo = Convert.ToInt32(ddlcentrocosto.SelectedValue);
                    datosApp.centro_costo_fin = Convert.ToInt32(ddlcentrocosto.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
                return;
            }

            // Determinar el nivel
            datosApp.nivel = Convert.ToInt32(ddlNivel.SelectedItem.Text);

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

            if (chkCompCentroCosto.Checked == true)
            {
                datosApp.comparativo = 1;
            }
            else
            {
                datosApp.comparativo = 0;
            }

            // Determina si genera mes13
            if (datosApp.fecha.Month == 12)
            {
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
            }
            else
            {
                datosApp.mostrarmovper13 = 0;
            }
            this.Lblmensaje.Text = "";
            datosApp.tipo_moneda = Convert.ToInt32(ddlMoneda.Value);

            // Limpiar columnas de la grilla para volver a generar
            gvLista.Columns.Clear();
            BoundField ColumnBoundCUENTA = new BoundField();
            ColumnBoundCUENTA.HeaderText = "Cod.Cuenta NIIF";
            ColumnBoundCUENTA.DataField = "cod_cuenta_niif";
            ColumnBoundCUENTA.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            gvLista.Columns.Add(ColumnBoundCUENTA);
            BoundField ColumnBoundNOMBRE = new BoundField();
            ColumnBoundNOMBRE.HeaderText = "Nombre Cuenta";
            ColumnBoundNOMBRE.DataField = "nombre";
            ColumnBoundNOMBRE.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            if (chkCompCentroCosto.Checked == true)
                ColumnBoundNOMBRE.ItemStyle.Width = 200;
            else
                ColumnBoundNOMBRE.ItemStyle.Width = 400;
            gvLista.Columns.Add(ColumnBoundNOMBRE);
            BoundField ColumnBoundVALOR = new BoundField();
            ColumnBoundVALOR.HeaderText = "Valor";
            ColumnBoundVALOR.DataField = "saldo";
            ColumnBoundVALOR.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            ColumnBoundVALOR.DataFormatString = "{0:N}";
            gvLista.Columns.Add(ColumnBoundVALOR);

            // Generar el balance de prueba                        
            if (chkCompCentroCosto.Checked == true)
            {
                List<Xpinn.Contabilidad.Entities.CentroCosto> lstCentrosCosto = new List<CentroCosto>();

                // Si es comparativo organizar los centros de costo en columnas
                List<Xpinn.NIIF.Entities.BalanceNIIF> lstConsulta = new List<Xpinn.NIIF.Entities.BalanceNIIF>();
                lstConsulta.Clear();
                lstConsulta = _balancePruebaSer.ListarBalance(datosApp, _usuario);

                // Crear Datatable para organizar los datos
                System.Data.DataTable table = new System.Data.DataTable();
                table.Columns.Add("cod_cuenta_niif");
                table.PrimaryKey = new DataColumn[] { table.Columns["cod_cuenta_niif"] };
                table.Columns.Add("nombre");
                table.Columns.Add("saldo", typeof(double));

                foreach (Xpinn.NIIF.Entities.BalanceNIIF item in lstConsulta)
                {
                    // Verificar si el centro de costo ya existe
                    if (item.centro_costo != null)
                    {
                        Boolean bExisteCentro = false;
                        foreach (CentroCosto cencos in lstCentrosCosto)
                        {
                            if (item.centro_costo == cencos.centro_costo)
                                bExisteCentro = true;
                        }
                        if (bExisteCentro == false)
                        {
                            Xpinn.Contabilidad.Services.CentroCostoService contabilServicio = new Xpinn.Contabilidad.Services.CentroCostoService();
                            CentroCosto rCenCos = new CentroCosto();
                            rCenCos.centro_costo = Convert.ToInt64(item.centro_costo);
                            rCenCos = contabilServicio.ConsultarCentroCosto(Convert.ToInt64(item.centro_costo), _usuario);
                            lstCentrosCosto.Add(rCenCos);
                            DataColumn dtCenCos = new DataColumn();
                            dtCenCos.ColumnName = "centro_costo_" + item.centro_costo.ToString().Trim();
                            dtCenCos.DataType = typeof(double);
                            dtCenCos.AllowDBNull = true;
                            table.Columns.Add(dtCenCos);
                            BoundField ColumnBoundKAP = new BoundField();
                            ColumnBoundKAP.HeaderText = rCenCos.descripcion;
                            ColumnBoundKAP.DataField = "centro_costo_" + item.centro_costo.ToString().Trim();
                            ColumnBoundKAP.DataFormatString = "{0:N}";
                            ColumnBoundKAP.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                            gvLista.Columns.Add(ColumnBoundKAP);
                        }
                    }
                    // Verificar si la cuenta contable existe
                    DataRow drReg;
                    drReg = table.Rows.Find(item.cod_cuenta_niif);
                    if (drReg == null)
                    {
                        DataRow datarw;
                        datarw = table.NewRow();
                        datarw["cod_cuenta_niif"] = item.cod_cuenta_niif;
                        datarw["nombre"] = item.nombre;
                        datarw["saldo"] = item.saldo;
                        datarw["centro_costo_" + item.centro_costo.ToString().Trim()] = item.saldo;
                        table.Rows.Add(datarw);
                    }
                    else
                    {
                        Double total = 0;
                        try
                        {
                            total = Convert.ToDouble(drReg["saldo"]);
                        }
                        catch { }
                        drReg["saldo"] = total + Convert.ToDouble(item.saldo);
                        Double valor = 0;
                        try
                        {
                            valor = Convert.ToDouble(drReg["centro_costo_" + item.centro_costo.ToString().Trim()]);
                        }
                        catch { }
                        drReg["centro_costo_" + item.centro_costo.ToString().Trim()] = valor + Convert.ToDouble(item.saldo);
                    }
                }
                Session["DTBALANCE"] = table;
                gvLista.DataSource = table;
                if (table.Rows.Count > 0)
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
            }
            else
            {
                List<Xpinn.NIIF.Entities.BalanceNIIF> lstConsultabalance = new List<Xpinn.NIIF.Entities.BalanceNIIF>();
                lstConsultabalance.Clear();
                lstConsultabalance = _balancePruebaSer.ListarBalance(datosApp, _usuario);
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
            }
            Session.Add(_balancePruebaSer.CodigoProgramaBalanceNiif + ".consulta", 1);

        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(_balancePruebaSer.CodigoProgramaBalanceNiif, "Actualizar", ex);
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
        if (chkCompCentroCosto.Checked == true)
        {
            table = (System.Data.DataTable)Session["DTBALANCE"];
        }
        else
        {
            List<Xpinn.NIIF.Entities.BalanceNIIF> lstConsultabalance = new List<Xpinn.NIIF.Entities.BalanceNIIF>();
            lstConsultabalance = (List<Xpinn.NIIF.Entities.BalanceNIIF>)Session["DTBALANCE"];

            // LLenar data table con los datos a recoger        
            table.Columns.Add("cod_cuenta_niif");
            table.Columns.Add("nombre");
            DataColumn cValor = new DataColumn();
            cValor.ColumnName = "saldo";
            cValor.AllowDBNull = true;
            cValor.DataType = typeof(decimal);
            table.Columns.Add(cValor);

            foreach (Xpinn.NIIF.Entities.BalanceNIIF item in lstConsultabalance)
            {
                DataRow datarw;
                datarw = table.NewRow();
                datarw[0] = item.cod_cuenta_niif;
                datarw[1] = item.nombre;
                datarw[2] = item.saldo.ToString("##,##0");
                table.Rows.Add(datarw);
            }
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
            Usuario pUsuario = _usuario;

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

            ReportDataSource rds = new ReportDataSource("DataSetBalanPrueba", CrearDataTablebalance());

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
                Response.AddHeader("Content-Disposition", "attachment;filename=ReporteSaldosNIIF.xls");
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
        this.Lblmensaje.Text = "";    
    }

    protected void ddlFechaCorte_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BalancePrueba mes13 = new BalancePrueba();
            mes13.fecha = DateTime.ParseExact(ddlFechaCorte.SelectedItem.Text, gFormatoFecha, null);
            mes13 = _balancePruebaSer.ConsultarBalanceMes13(mes13, (Usuario)Session["usuario"]);
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

}