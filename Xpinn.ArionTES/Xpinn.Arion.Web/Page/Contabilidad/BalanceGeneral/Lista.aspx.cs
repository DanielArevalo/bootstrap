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
    Xpinn.Contabilidad.Services.BalanceGeneralService BalanceGeneralSer = new Xpinn.Contabilidad.Services.BalanceGeneralService();
    static string pCodigo = null;
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["niif"] != null)
            {
                VisualizarOpciones(BalanceGeneralSer.CodigoProgramaNIIF, "L");
                pCodigo = BalanceGeneralSer.CodigoProgramaNIIF;
            }
            else
            {
                VisualizarOpciones(BalanceGeneralSer.CodigoPrograma, "L");
                pCodigo = BalanceGeneralSer.CodigoPrograma;
            }
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BalanceGeneralSer.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["usuario"];

            if (!IsPostBack)
            {
                LlenarCombos();
                btnExportar.Visible = false;
                btnInforme.Visible = false;
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

        //// LLenar el DDl de TipoMoneda
        //Xpinn.Contabilidad.Services.TipoMonedaService TipoMonedaService = new Xpinn.Contabilidad.Services.TipoMonedaService();
        //List<Xpinn.Contabilidad.Entities.TipoMoneda> LstTipoMoneda = new List<Xpinn.Contabilidad.Entities.TipoMoneda>();
        //LstTipoMoneda = TipoMonedaService.ListarTipoMoneda(_usuario);
        //ddlMoneda.DataSource = LstTipoMoneda;
        //ddlMoneda.DataTextField = "descripcion";
        //ddlMoneda.DataValueField = "tipo_moneda";
        //ddlMoneda.DataBind();

        // Llenar el DDL de la fecha de corte 
        Configuracion conf = new Configuracion();
        List<Xpinn.Contabilidad.Entities.BalanceGeneral> lstFechaCierre = new List<Xpinn.Contabilidad.Entities.BalanceGeneral>();
        Xpinn.Contabilidad.Services.BalanceGeneralService BalancePruebaService = new Xpinn.Contabilidad.Services.BalanceGeneralService();
        Xpinn.Contabilidad.Entities.BalanceGeneral BalancePrueba = new Xpinn.Contabilidad.Entities.BalanceGeneral();
        lstFechaCierre = BalancePruebaService.ListarFechaCorte(_usuario);
        ddlFechaCorte.DataSource = lstFechaCierre;
        ddlFechaCorte.DataTextFormatString = "{0:" + conf.ObtenerFormatoFecha() + "}";
        ddlFechaCorte.DataTextField = "fecha";
        ddlFechaCorte.DataBind();

    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {

        GuardarValoresConsulta(pConsulta, pCodigo);

        Actualizar(idObjeto);

        Lblerror.Visible = false;

    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, pCodigo);
        gvLista.DataSource = null;
        gvLista.DataBind();
        Lblerror.Visible = false;

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
        VerError("");
        try
        {
            String emptyQuery = "Fila de datos vacia";
            BalanceGeneral datosApp = new BalanceGeneral();

            Configuracion conf = new Configuracion();
            String format = conf.ObtenerFormatoFecha();
            datosApp.fecha = DateTime.ParseExact(ddlFechaCorte.SelectedValue, format, CultureInfo.InvariantCulture);

            // Determinar la moneda
            datosApp.moneda = Convert.ToInt64(ddlMoneda.Value);

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
                        CCSer.RangoCentroCosto(ref cenini, ref cenfin, _usuario);
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

            datosApp.comparativo = 0;

            // Determina  si se muestran cuentas de orden
            if (chkCuentasOrden.Checked == true)
            {
                datosApp.cuentasorden = 1;
            }
            else
            {
                datosApp.cuentasorden = 0;
            }

            // Generar el Balance General
            List<BalanceGeneral> lstConsultabalance = new List<BalanceGeneral>();
            lstConsultabalance.Clear();
            int pOpcion = Request.QueryString["niif"] != null ? 2 : 1;
            lstConsultabalance = BalanceGeneralSer.ListarBalance(datosApp, _usuario, pOpcion);

            gvLista.EmptyDataText = emptyQuery;            

            // Mostrando los datos            
            if (lstConsultabalance.Count > 0)
            {
                List<BalanceGeneral> lstConsulta = new List<BalanceGeneral>();
                gvLista.Visible = true;                
                int indice = 0;
                Int64? nivel = null;
                foreach (BalanceGeneral item in lstConsultabalance)
                {
                    if (nivel != null)
                    {
                        if (nivel != item.nivel)
                        {
                            if (item.nivel <= 2)
                            {
                                BalanceGeneral rNuevo = new BalanceGeneral();
                                lstConsulta.Add(rNuevo);
                            }
                        }
                    }
                    lstConsulta.Add(item);
                    nivel = item.nivel;
                    indice += 1;
                }
                Session["DTBALANCEGENERAL"] = lstConsulta;
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                mvBalance.ActiveViewIndex = 0;
                btnExportar.Visible = true;
                btnInforme.Visible = true;

                if (datosApp.fecha.HasValue)
                {
                    string error = BalanceGeneralSer.VerificarComprobantesYCuentas(datosApp.fecha.Value, _usuario);

                    if (!string.IsNullOrWhiteSpace(error))
                    {
                        VerError(error);
                    }
                }
            }
            else
            {
                Session["DTBALANCEGENERAL"] = null;
                gvLista.Visible = false;
                btnExportar.Visible = false;
                btnInforme.Visible = false;
            }

            Session.Add(pCodigo + ".consulta", 1);
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


    protected void chkCuentasCero_CheckedChanged(object sender, EventArgs e)
    {        
    }

    public DataTable CrearDataTablebalance()
    {
        List<Xpinn.Contabilidad.Entities.BalanceGeneral> lstConsultabalance = new List<Xpinn.Contabilidad.Entities.BalanceGeneral>();
        lstConsultabalance = (List<Xpinn.Contabilidad.Entities.BalanceGeneral>)Session["DTBALANCEGENERAL"];

        // LLenar data table con los datos a recoger
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("CodCuenta");
        table.Columns.Add("NombreCuenta");
        DataColumn dtValor = new DataColumn();
        dtValor.ColumnName = "Valor";
        dtValor.AllowDBNull = true;
        dtValor.DataType = typeof(decimal);
        table.Columns.Add(dtValor);


        foreach (BalanceGeneral item in lstConsultabalance)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.cod_cuenta;
            datarw[1] = item.nombrecuenta;
            if (item.valor == null)
                datarw[2] = DBNull.Value;
            else
                datarw[2] = Convert.ToDouble(item.valor).ToString("N2");
            table.Rows.Add(datarw);            
        }
        return table;
    }

    protected void btnInforme_Click(object sender, EventArgs e)
    {

        Configuracion conf = new Configuracion();
        VerError("");
        if (Session["DTBALANCEGENERAL"] == null)
        {
            Lblerror.Text = "No ha generado el Balance General para poder imprimir el reporte";

        }
        if (Session["DTBALANCEGENERAL"] != null)
        {

            List<Xpinn.Contabilidad.Entities.BalanceGeneral> lstConsultabalance = new List<Xpinn.Contabilidad.Entities.BalanceGeneral>();
            lstConsultabalance = (List<Xpinn.Contabilidad.Entities.BalanceGeneral>)Session["DTBALANCEGENERAL"];

            ReportParameter[] param = new ReportParameter[11];
            param[0] = new ReportParameter("entidad", _usuario.empresa);
            param[1] = new ReportParameter("nit", _usuario.nitempresa);            
            param[2] = new ReportParameter("fecha", ddlFechaCorte.SelectedValue);
            if (ddlcentrocosto.SelectedValue == "0")
                param[3] = new ReportParameter("centro_costo", "");
            else
                param[3] = new ReportParameter("centro_costo", ddlcentrocosto.SelectedValue);
            param[4] = new ReportParameter("representante_legal", _usuario.representante_legal);
            param[5] = new ReportParameter("contador", _usuario.contador);
            param[6] = new ReportParameter("tarjeta_contador", _usuario.tarjeta_contador);
            param[7] = new ReportParameter("ImagenReport", ImagenReporte());
            param[8] = new ReportParameter("RevisorFiscal", _usuario.revisor_Fiscal);

            param[9] = new ReportParameter("usuario_genera", _usuario.nombre);
            param[10] = new ReportParameter("nivel_cuentas", ddlNivel.SelectedValue);
            mvBalance.Visible = true;
            RptReporte.LocalReport.EnableExternalImages = true;
            RptReporte.LocalReport.SetParameters(param);
            RptReporte.LocalReport.DataSources.Clear();
            RptReporte.LocalReport.Refresh();

            ReportDataSource rds = new ReportDataSource("DataSetBalanceGeneral", CrearDataTablebalance());
            RptReporte.LocalReport.DataSources.Add(rds);

            Site toolBar = (Site)Master;
            toolBar.MostrarConsultar(false);
            toolBar.MostrarLimpiar(false);
            frmPrint.Visible = false;
            RptReporte.Visible = true;
            mvBalance.ActiveViewIndex = 1;
        }

    }
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTBALANCEGENERAL"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.DataSource = Session["DTBALANCEGENERAL"];
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

    protected void btnDatos_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarConsultar(true);
        toolBar.MostrarLimpiar(true);
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

}