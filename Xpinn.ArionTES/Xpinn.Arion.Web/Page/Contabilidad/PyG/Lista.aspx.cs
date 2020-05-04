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
    Xpinn.Contabilidad.Services.PyGService PyGSer = new Xpinn.Contabilidad.Services.PyGService();
    Xpinn.Contabilidad.Services.BalanceGeneralService BalanceGeneralSer = new Xpinn.Contabilidad.Services.BalanceGeneralService();
    static string pCodigo;
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["niif"] != null)
            {
                VisualizarOpciones(PyGSer.CodigoProgramaNIIF, "L");
                pCodigo = PyGSer.CodigoProgramaNIIF;
            }
            else
            {
                VisualizarOpciones(PyGSer.CodigoPrograma, "L");
                pCodigo = PyGSer.CodigoPrograma;
            }
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PyGSer.CodigoPrograma, "Page_PreInit", ex);
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
                btnInforme.Visible = false;
                btnExportar.Visible = false;
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
        List<Xpinn.Contabilidad.Entities.BalancePrueba> lstFechaCierre = new List<Xpinn.Contabilidad.Entities.BalancePrueba>();
        Xpinn.Contabilidad.Services.BalancePruebaService BalancePruebaService = new Xpinn.Contabilidad.Services.BalancePruebaService();
        Xpinn.Contabilidad.Entities.BalancePrueba BalancePrueba = new Xpinn.Contabilidad.Entities.BalancePrueba();
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
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, pCodigo);
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
            BOexcepcion.Throw(pCodigo, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar(String pIdObjeto)
    {
        try
        {
            VerError("");
            String emptyQuery = "Fila de datos vacia";
            PyG datosApp = new PyG();

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

            // Determina si se genera del periodo
            if (chkPeriodo.Checked == true)
            {
                datosApp.saldosperiodo = 1;
            }
            else
            {
                datosApp.saldosperiodo = 0;
            }

            datosApp.moneda = Convert.ToInt64(ddlMoneda.Value);

            // Limpiar columnas de la grilla para volver a generar
            gvLista.Columns.Clear();
            BoundField ColumnBoundCUENTA = new BoundField();
            ColumnBoundCUENTA.HeaderText = "Cod.Cuenta";
            if (chkCompCentroCosto.Checked == true)
                ColumnBoundCUENTA.HeaderStyle.Font.Size = 8;
            else
                ColumnBoundCUENTA.HeaderStyle.Font.Size = 10;
            ColumnBoundCUENTA.DataField = "cod_cuenta";
            ColumnBoundCUENTA.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            if (chkCompCentroCosto.Checked == true)
                ColumnBoundCUENTA.ItemStyle.Font.Size = 8;
            else
                ColumnBoundCUENTA.ItemStyle.Font.Size = 10;
            gvLista.Columns.Add(ColumnBoundCUENTA);
            BoundField ColumnBoundNOMBRE = new BoundField();
            ColumnBoundNOMBRE.HeaderText = "Nombre Cuenta";
            if (chkCompCentroCosto.Checked == true)
                ColumnBoundNOMBRE.HeaderStyle.Font.Size = 8;
            else
                ColumnBoundNOMBRE.HeaderStyle.Font.Size = 10;
            ColumnBoundNOMBRE.DataField = "nombrecuenta";
            ColumnBoundNOMBRE.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            if (chkCompCentroCosto.Checked == true)
                ColumnBoundNOMBRE.ItemStyle.Font.Size = 8;
            else
                ColumnBoundNOMBRE.ItemStyle.Font.Size = 10;
            if (chkCompCentroCosto.Checked == true)
                ColumnBoundNOMBRE.ItemStyle.Width = 200;
            else
                ColumnBoundNOMBRE.ItemStyle.Width = 400;
            gvLista.Columns.Add(ColumnBoundNOMBRE);
            BoundField ColumnBoundVALOR = new BoundField();
            ColumnBoundVALOR.HeaderText = "Valor";
            if (chkCompCentroCosto.Checked == true)
                ColumnBoundVALOR.HeaderStyle.Font.Size = 8;
            else
                ColumnBoundVALOR.HeaderStyle.Font.Size = 10;
            ColumnBoundVALOR.DataField = "valor";
            ColumnBoundVALOR.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            if (chkCompCentroCosto.Checked == true)
                ColumnBoundVALOR.ItemStyle.Font.Size = 8;
            else
                ColumnBoundVALOR.ItemStyle.Font.Size = 10;
            ColumnBoundVALOR.DataFormatString = "{0:c}";
            gvLista.Columns.Add(ColumnBoundVALOR);

            // Generar el balance de prueba                        
            if (chkCompCentroCosto.Checked == true)
            {
                List<Xpinn.Contabilidad.Entities.CentroCosto> lstCentrosCosto = new List<CentroCosto>();

                // Si es comparativo organizar los centros de costo en columnas
                List<PyG> lstDatos = new List<PyG>();
                List<PyG> lstConsulta = new List<PyG>();
                lstConsulta.Clear();
                int pOpcion = Request.QueryString["niif"] != null ? 2 : 1;
                datosApp.orden = 1;
                lstDatos = PyGSer.ListarPyG(datosApp, _usuario, pOpcion);
                lstConsulta = lstDatos.OrderBy(p => p.cod_cuenta).ToList();
                 
                // Crear Datatable para organizar los datos
                System.Data.DataTable table = new System.Data.DataTable();
                table.Columns.Add("cod_cuenta");
                table.PrimaryKey = new DataColumn[] { table.Columns["cod_cuenta"] };
                table.Columns.Add("nombrecuenta");
                table.Columns.Add("valor", typeof(double));

                foreach (PyG item in lstConsulta)
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
                            if (chkCompCentroCosto.Checked == true)
                                ColumnBoundKAP.HeaderStyle.Font.Size = 8;
                            else
                                ColumnBoundKAP.HeaderStyle.Font.Size = 10;
                            ColumnBoundKAP.DataField = "centro_costo_" + item.centro_costo.ToString().Trim();
                            ColumnBoundKAP.DataFormatString = "{0:c}";
                            ColumnBoundKAP.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                            if (chkCompCentroCosto.Checked == true)
                                ColumnBoundKAP.ItemStyle.Font.Size = 8;
                            else
                                ColumnBoundKAP.ItemStyle.Font.Size = 10;
                            gvLista.Columns.Add(ColumnBoundKAP);
                        }
                    }
                    if (item.cod_cuenta == "999")
                        item.cod_cuenta = "   ";
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

                    ValidarBalance(datosApp.fecha);
                }
                else
                {
                    mvBalance.ActiveViewIndex = -1;
                    btnExportar.Visible = false;
                }
                btnInforme.Visible = false;
            }
            else
            {
                List<PyG> lstConsultabalance = new List<PyG>();
                lstConsultabalance.Clear();
                int pOpcion = Request.QueryString["niif"] != null ? 2 : 1;
                lstConsultabalance = PyGSer.ListarPyG(datosApp, _usuario, pOpcion);

                gvLista.EmptyDataText = emptyQuery;
                if (lstConsultabalance.Count > 0)
                {
                    List<PyG> lstConsulta = new List<PyG>();
                    gvLista.Visible = true;
                    int indice = 0;
                    Int64? nivel = null;
                    foreach (PyG item in lstConsultabalance)
                    {
                        if (nivel != null)
                        {
                            if (nivel != item.nivel)
                            {
                                if (item.nivel <= 2)
                                {
                                    PyG rNuevo = new PyG();
                                    lstConsulta.Add(rNuevo);
                                }
                            }
                        }
                        lstConsulta.Add(item);
                        nivel = item.nivel;
                        indice += 1;
                    }
                    gvLista.DataSource = lstConsulta;
                    Session["DTBALANCE"] = lstConsulta;
                    mvBalance.ActiveViewIndex = 0;
                    gvLista.DataBind();
                    btnInforme.Visible = true;
                    btnExportar.Visible = true;

                    ValidarBalance(datosApp.fecha);
                }
                else
                {
                    Session["DTBALANCE"] = null;
                    gvLista.Visible = false;
                    btnInforme.Visible = false;
                    btnExportar.Visible = false;
                }
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

        List<Xpinn.Contabilidad.Entities.PyG> lstConsultabalance = new List<Xpinn.Contabilidad.Entities.PyG>();
        lstConsultabalance = (List<Xpinn.Contabilidad.Entities.PyG>)Session["DTBALANCE"];

        // LLenar data table con los datos a recoger
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("CodCuenta");
        table.Columns.Add("NombreCuenta");
        DataColumn dtValor = new DataColumn();
        dtValor.ColumnName = "Valor";
        dtValor.AllowDBNull = true;
        table.Columns.Add(dtValor);


        foreach (PyG item in lstConsultabalance)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.cod_cuenta;
            datarw[1] = item.nombrecuenta;
            if (item.valor == null)
                datarw[2] = DBNull.Value;
            else
                datarw[2] = Convert.ToDouble(item.valor).ToString("##,##0");
            table.Rows.Add(datarw);
        }
        return table;
    }


    protected void btnInforme_Click(object sender, EventArgs e)
    {
        Usuario pUsuario = _usuario;

        ReportParameter[] param = new ReportParameter[9];
        param[0] = new ReportParameter("entidad", pUsuario.empresa);
        param[1] = new ReportParameter("nit", pUsuario.nitempresa);
        param[2] = new ReportParameter("fecha", ddlFechaCorte.SelectedValue);
        if (ddlcentrocosto.SelectedValue == "0")
            param[3] = new ReportParameter("centro_costo", "");
        else
            param[3] = new ReportParameter("centro_costo", ddlcentrocosto.SelectedValue);
        param[4] = new ReportParameter("representante_legal", pUsuario.representante_legal);
        param[5] = new ReportParameter("contador", pUsuario.contador);
        param[6] = new ReportParameter("tarjeta_contador", pUsuario.tarjeta_contador);
        param[7] = new ReportParameter("ImagenReport", ImagenReporte());
        param[8] = new ReportParameter("RevisorFiscal", pUsuario.revisor_Fiscal);

        mvBalance.Visible = true;
        RptReporte.LocalReport.EnableExternalImages = true;
        RptReporte.LocalReport.SetParameters(param);
        RptReporte.LocalReport.DataSources.Clear();
        RptReporte.LocalReport.Refresh();

        ReportDataSource rds = new ReportDataSource("DataSet1", CrearDataTablebalance());
        RptReporte.LocalReport.DataSources.Add(rds);

        Site toolBar = (Site)Master;
        toolBar.MostrarConsultar(false);
        toolBar.MostrarLimpiar(false);
        frmPrint.Visible = false;
        RptReporte.Visible = true;
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
                Response.AddHeader("Content-Disposition", "attachment;filename=PyG.xls");
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