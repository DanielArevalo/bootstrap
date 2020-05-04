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
    private Xpinn.Contabilidad.Services.BalancePruebaService BalancePruebaSer = new Xpinn.Contabilidad.Services.BalancePruebaService();
    decimal Activo;
    decimal Pasivo;
    decimal Patrimonio;
    decimal Utilidad;
    decimal Diferencia;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(BalancePruebaSer.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BalancePruebaSer.CodigoPrograma, "Page_PreInit", ex);
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
                Alert.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BalancePruebaSer.CodigoPrograma, "Page_Load", ex);
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
            List<Xpinn.Contabilidad.Entities.BalancePrueba> lstFechaCierre = new List<Xpinn.Contabilidad.Entities.BalancePrueba>();
            Xpinn.Contabilidad.Services.BalancePruebaService BalancePruebaService = new Xpinn.Contabilidad.Services.BalancePruebaService();
            Xpinn.Contabilidad.Entities.BalancePrueba BalancePrueba = new Xpinn.Contabilidad.Entities.BalancePrueba();
            lstFechaCierre = BalancePruebaService.ListarFechaCorte((Usuario)Session["Usuario"]);
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
        GuardarValoresConsulta(pConsulta, BalancePruebaSer.CodigoPrograma);
        Actualizar(idObjeto);
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        lblTotalRegs.Text = "";
        LimpiarValoresConsulta(pConsulta, BalancePruebaSer.CodigoPrograma);
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
            BOexcepcion.Throw(BalancePruebaSer.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar(String pIdObjeto)
    {
        lblTotalRegs.Text = "";
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
            datosApp.nivel = Convert.ToInt64(ddlNivel.SelectedItem.Text);

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

            // Limpiar columnas de la grilla para volver a generar
            gvLista.Columns.Clear();
            BoundField ColumnBoundCUENTA = new BoundField();
            ColumnBoundCUENTA.HeaderText = "Cod.Cuenta";
            ColumnBoundCUENTA.DataField = "cod_cuenta";
            ColumnBoundCUENTA.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            gvLista.Columns.Add(ColumnBoundCUENTA);
            BoundField ColumnBoundNOMBRE = new BoundField();
            ColumnBoundNOMBRE.HeaderText = "Nombre Cuenta";
            ColumnBoundNOMBRE.DataField = "nombrecuenta";
            ColumnBoundNOMBRE.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            if (chkCompCentroCosto.Checked == true)
                ColumnBoundNOMBRE.ItemStyle.Width = 200;
            else
                ColumnBoundNOMBRE.ItemStyle.Width = 400;
            gvLista.Columns.Add(ColumnBoundNOMBRE);
            BoundField ColumnBoundVALOR = new BoundField();
            ColumnBoundVALOR.HeaderText = "Valor";
            ColumnBoundVALOR.DataField = "valor";
            ColumnBoundVALOR.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            ColumnBoundVALOR.DataFormatString = "{0:N}";
            gvLista.Columns.Add(ColumnBoundVALOR);

            // Generar el balance de prueba                        
            if (chkCompCentroCosto.Checked == true)
            {
                List<Xpinn.Contabilidad.Entities.CentroCosto> lstCentrosCosto = new List<CentroCosto>();

                // Si es comparativo organizar los centros de costo en columnas
                List<BalancePrueba> lstConsulta = new List<BalancePrueba>();
                List<BalancePrueba> lstBalAlert = new List<BalancePrueba>();
                lstConsulta.Clear();              
                lstConsulta = BalancePruebaSer.ListarBalance(datosApp, (Usuario)Session["usuario"]);            

                // Crear Datatable para organizar los datos
                System.Data.DataTable table = new System.Data.DataTable();
                table.Columns.Add("cod_cuenta");
                table.PrimaryKey = new DataColumn[] { table.Columns["cod_cuenta"] };
                table.Columns.Add("nombrecuenta");
                table.Columns.Add("valor", typeof(double));

                foreach (BalancePrueba item in lstConsulta)
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
                            rCenCos = contabilServicio.ConsultarCentroCosto(Convert.ToInt64(item.centro_costo), (Usuario)Session["usuario"]);
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
                    drReg = table.Rows.Find(item.cod_cuenta);
                    if (drReg == null)
                    {
                        DataRow datarw;
                        datarw = table.NewRow();
                        datarw["cod_cuenta"] = item.cod_cuenta;
                        datarw["nombrecuenta"] = item.nombrecuenta;
                        datarw["valor"] = item.valor;
                        datarw["centro_costo_" + item.centro_costo.ToString().Trim()] = item.valor;
                        table.Rows.Add(datarw);
                    }
                    else
                    {
                        Double total = 0;
                        try
                        {
                            total = Convert.ToDouble(drReg["valor"]);
                        }
                        catch { }
                        drReg["valor"] = total + item.valor;
                        Double valor = 0;
                        try
                        {
                            valor = Convert.ToDouble(drReg["centro_costo_" + item.centro_costo.ToString().Trim()]);
                        }
                        catch { }
                        drReg["centro_costo_" + item.centro_costo.ToString().Trim()] = valor + item.valor;
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
                    lblTotalRegs.Text = "No se encontraron datos";
                }
            }
            else
            {
                List<BalancePrueba> lstConsultabalance = new List<BalancePrueba>();               
                lstConsultabalance.Clear();
                lstConsultabalance = BalancePruebaSer.ListarBalance(datosApp, (Usuario)Session["usuario"]);    
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
                    lblTotalRegs.Text = "No se encontraron datos";
                }
            }
            Session.Add(BalancePruebaSer.CodigoPrograma + ".consulta", 1);

            //Alerta descuadre balance
            AlertaBalance(Convert.ToDateTime(ddlFechaCorte.SelectedValue));

        }

        catch (Exception ex)
        {
            //BOexcepcion.Throw(BalancePruebaSer.CodigoPrograma, "Actualizar", ex);
            VerError(ex.Message);
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
            List<Xpinn.Contabilidad.Entities.BalancePrueba> lstConsultabalance = new List<Xpinn.Contabilidad.Entities.BalancePrueba>();
            lstConsultabalance = (List<Xpinn.Contabilidad.Entities.BalancePrueba>)Session["DTBALANCE"];

            // LLenar data table con los datos a recoger
            table.Columns.Add("CodCuenta");
            table.Columns.Add("NombreCuenta");
            DataColumn cValor = new DataColumn();
            cValor.ColumnName = "Valor";
            cValor.AllowDBNull = true;
            cValor.DataType = typeof(decimal);
            table.Columns.Add(cValor);

            foreach (BalancePrueba item in lstConsultabalance)
            {
                DataRow datarw;
                datarw = table.NewRow();
                datarw[0] = item.cod_cuenta;
                datarw[1] = item.nombrecuenta;
                datarw[2] = item.valor.ToString("N2");
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
                Response.AddHeader("Content-Disposition", "attachment;filename=BalancePrueba.xls");
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
            mes13 = BalancePruebaSer.ConsultarBalanceMes13(mes13, (Usuario)Session["usuario"]);
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
    void AlertaBalance(DateTime pfecha)
    {  
        BalancePruebaSer.AlertaBalance(pfecha, ref Activo, ref Pasivo, ref Patrimonio, ref Utilidad, ref Diferencia, (Usuario)Session["Usuario"]);

        if (Diferencia != 0)
        {
            Alert.Visible = true;
            txtAlertas.Text = @"Se presenta un descuadre en el balance con la Ecuación Patrimonial:" + "\n" +
                                "Activo: " + Activo.ToString() + "\n" +
                                "Pasivo: " + Pasivo.ToString() + "\n" +
                                "Patrimonio: " + Patrimonio.ToString() + "\n" +
                                "Utilidad: " + Utilidad.ToString() + "\n" +
                                "Diferencia: " + Diferencia.ToString();
        }
        else
        {
            Alert.Visible = false;
            txtAlertas.Text = "";
        }

    }
}
