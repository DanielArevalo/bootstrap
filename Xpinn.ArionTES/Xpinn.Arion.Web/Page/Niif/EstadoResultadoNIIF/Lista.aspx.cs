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
    private Xpinn.NIIF.Services.EstadoResultadoNIIFService EstadoResultadoServicio = new Xpinn.NIIF.Services.EstadoResultadoNIIFService();
    private static string pCodigo;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["niif"] != null)
            {
                VisualizarOpciones(EstadoResultadoServicio.CodigoProgramaNIIF, "L");
                pCodigo = EstadoResultadoServicio.CodigoProgramaNIIF;
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
        ddlNivel.Items.Insert(0, new ListItem("0-X CUENTA", "0"));
        ddlNivel.Items.Insert(1, new ListItem("1-X CONCEPTO", "1"));


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
        List<Xpinn.NIIF.Entities.EstadoResultadoNIIF> lstFechaCierre = new List<Xpinn.NIIF.Entities.EstadoResultadoNIIF>();
        Xpinn.Contabilidad.Services.BalanceGenCompService BalanceGenCompService = new Xpinn.Contabilidad.Services.BalanceGenCompService();
        Xpinn.NIIF.Entities.EstadoResultadoNIIF Estadoresultado= new Xpinn.NIIF.Entities.EstadoResultadoNIIF();
        lstFechaCierre = EstadoResultadoServicio.ListarFecha((Usuario)Session["Usuario"]);
        ddlFecha1.DataSource = lstFechaCierre;
        ddlFecha2.DataSource = lstFechaCierre;
        ddlFecha1.DataTextFormatString = "{0:" + GlobalWeb.gFormatoFecha + "}";
        ddlFecha2.DataTextFormatString = "{0:" + GlobalWeb.gFormatoFecha + "}";
        ddlFecha1.DataTextField = "fechaprimerper";
        ddlFecha2.DataTextField = "fechaprimerper";
        ddlFecha1.DataBind();
        ddlFecha2.DataBind();     
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
        ddlNivel.Enabled = false;
        ddlcentrocosto.Enabled = false;
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        mvEstadoResultados.ActiveViewIndex = 0;
        LimpiarValoresConsulta(pConsulta, pCodigo);
        gvLista.DataSource = null;
        gvLista.DataBind();
        Lblerror.Visible = false;
       
        ddlcentrocosto.Enabled = true;
        ddlFecha1.Enabled = true;
        ddlFecha2.Enabled = true;       
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
            Xpinn.NIIF.Entities.EstadoResultadoNIIF datosApp = new Xpinn.NIIF.Entities.EstadoResultadoNIIF();

            String format = GlobalWeb.gFormatoFecha;
            datosApp.fechaprimerper = DateTime.ParseExact(this.ddlFecha1.SelectedValue, format, CultureInfo.InvariantCulture);
            datosApp.fechasegunper = DateTime.ParseExact(this.ddlFecha2.SelectedValue, format, CultureInfo.InvariantCulture);

            
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
            datosApp.nivel = Convert.ToInt64(ddlNivel.SelectedValue);

            // Generar el libro de Balance comparativo
            List<Xpinn.NIIF.Entities.EstadoResultadoNIIF> lstConsultaEstadoResultadoNIIF = new List<Xpinn.NIIF.Entities.EstadoResultadoNIIF>();
            lstConsultaEstadoResultadoNIIF.Clear();
            int pOpcion = Request.QueryString["niif"] != null ? 2 : 1;
            lstConsultaEstadoResultadoNIIF = EstadoResultadoServicio.ListarEstadoResultado(datosApp, (Usuario)Session["usuario"], pOpcion);
            
            gvLista.EmptyDataText = emptyQuery;
            Session["DTEstadoResultadoNIIF"] = lstConsultaEstadoResultadoNIIF;

            gvLista.DataSource = lstConsultaEstadoResultadoNIIF;
            if (lstConsultaEstadoResultadoNIIF.Count > 0)
            {
                List<Xpinn.NIIF.Entities.EstadoResultadoNIIF> lstConsulta = new List<Xpinn.NIIF.Entities.EstadoResultadoNIIF>();
                gvLista.Visible = true;
                int indice = 0;
                Int64? nivel = null;
                foreach (Xpinn.NIIF.Entities.EstadoResultadoNIIF item in lstConsultaEstadoResultadoNIIF)
                {
                    Xpinn.NIIF.Entities.EstadoResultadoNIIF rNuevo1 = new Xpinn.NIIF.Entities.EstadoResultadoNIIF();


                    if (item.cod_cuenta == "999999")
                    {
                        lstConsulta.Add(rNuevo1);
                    }

                    if (nivel != null)
                    {
                        if (nivel != item.nivel)
                        {
                            if (item.nivel <= 2)
                            {
                                Xpinn.NIIF.Entities.EstadoResultadoNIIF rNuevo = new Xpinn.NIIF.Entities.EstadoResultadoNIIF();
                                if (item.balance1 > 0)
                                    lstConsulta.Add(rNuevo);
                            }
                        }
                    }
                  
                        lstConsulta.Add(item);
                        nivel = item.nivel;
                        indice += 1;

                   

                }


                btnInforme.Visible = true;
                btnExportar.Visible = true;

                mvEstadoResultados.ActiveViewIndex = 0;
                gvLista.Columns[2].HeaderText = datosApp.fechaprimerper.ToShortDateString();
                gvLista.Columns[3].HeaderText = datosApp.fechasegunper.ToShortDateString();

                Session["DTEstadoResultadoNIIF"] = lstConsulta;
                gvLista.DataSource = lstConsulta;

                gvLista.DataBind();
            }
            else
            {
                btnInforme.Visible = false;
                btnExportar.Visible = false;
                mvEstadoResultados.ActiveViewIndex = -1;
            }

            Session.Add(pCodigo + ".consulta", 1);
            ddlcentrocosto.Enabled = true;
            ddlFecha1.Enabled = true;
            ddlFecha2.Enabled = true;
          
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

  
    public DataTable CrearDataTableEstadoResultadosNIIF()
    {

        List<Xpinn.NIIF.Entities.EstadoResultadoNIIF> lstConsultaEstadoResultadoNIIF = new List<Xpinn.NIIF.Entities.EstadoResultadoNIIF>();
        lstConsultaEstadoResultadoNIIF = (List<Xpinn.NIIF.Entities.EstadoResultadoNIIF>)Session["DTEstadoResultadoNIIF"];

        // LLenar data table con los datos a recoger
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("CodCuenta");
        table.Columns.Add("NombreCuenta");
        table.Columns.Add("Balance1");
        table.Columns.Add("Porpart1");
        table.Columns.Add("Balance2");
        table.Columns.Add("Diferencia");
        table.Columns.Add("Dif1");      

       
        foreach (Xpinn.NIIF.Entities.EstadoResultadoNIIF item in lstConsultaEstadoResultadoNIIF)
        {
            CultureInfo ci = new CultureInfo("en-us");
            DataRow datarw;
            datarw = table.NewRow();


            if (ddlNivel.SelectedValue == "0")
                datarw[0] = item.cod_cuenta;
            else
                datarw[0] = "".ToString();

            if (item.cod_cuenta == "99999" || item.cod_cuenta == "999999")
                datarw[0] = "-1".ToString();

            datarw[1] = item.nombrecuenta;



            if (item.balance1 != 0)
                datarw[2] = item.balance1.ToString("##,##0");
            else
                datarw[2] = "".ToString();

            if (item.balance1 != 0)
                datarw[3] = item.porcpart1.ToString("##,##0");
            else
                datarw[3] = "".ToString();

            if (item.balance2 != 0)
                datarw[4] = item.balance2.ToString("##,##0");
            else
                datarw[4] = "".ToString();

            if (item.diferencia != 0)
                datarw[5] = item.diferencia.ToString("##,##0");
            else
                datarw[5] = "".ToString();

            if (item.diferencia != 0)

                datarw[6] = item.porcdif.ToString("#0.00%");
            else
                datarw[6] = "".ToString();

            

            table.Rows.Add(datarw);
        }
        return table;
    }

    protected void btnInforme_Click(object sender, EventArgs e)
    {
       
        Configuracion conf = new Configuracion();
        VerError("");
        if (Session["DTEstadoResultadoNIIF"] == null)
        {
            Lblerror.Text="No ha generado el Estado De Resultados para poder imprimir el reporte";
            return;
        }

        if (Session["DTEstadoResultadoNIIF"] != null)
        {

            List<Xpinn.NIIF.Entities.EstadoResultadoNIIF> lstEstadoResultadoNIIF = new List<Xpinn.NIIF.Entities.EstadoResultadoNIIF>();
            lstEstadoResultadoNIIF = (List<Xpinn.NIIF.Entities.EstadoResultadoNIIF>)Session["DTEstadoResultadoNIIF"];

            Usuario pUsuario = (Usuario)Session["Usuario"];

            ReportParameter[] param = new ReportParameter[14];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);
            param[2] = new ReportParameter("fecha", Convert.ToString(DateTime.Now.ToShortDateString()));
            // //if (ddlFechaa3.SelectedValue == "01/01/0001")
            //   param[3] = new ReportParameter("periodoter", "0");
            //else
            param[3] = new ReportParameter("periodoter", "1");
            param[4] = new ReportParameter("fecha1", ddlFecha1.SelectedValue);
            param[5] = new ReportParameter("fecha2", ddlFecha2.SelectedValue);
            //    param[6] = new ReportParameter("fecha3", ddlFechaa3.SelectedValue);
            param[6] = new ReportParameter("representante_legal", pUsuario.representante_legal);
            param[7] = new ReportParameter("contador", pUsuario.contador);
            param[8] = new ReportParameter("tarjeta_contador", pUsuario.tarjeta_contador);
            param[9] = new ReportParameter("ImagenReport", ImagenReporte());
            param[10] = new ReportParameter("RevisorFiscal", pUsuario.revisor_Fiscal);






            if (ddlNivel.SelectedValue == "1")
            {
                Session["OCULTAR"] = 1;
                param[11] = new ReportParameter("Ocultar", 1.ToString());
            }
            if (ddlNivel.SelectedValue == "0")
            {
                Session["OCULTAR"] = 0;
                param[11] = new ReportParameter("Ocultar", 0.ToString());

            }



            if (ddlcentrocosto.SelectedValue == "0")
            {
                param[12] = new ReportParameter("CentroCostoCons", "X".ToString());
                param[13] = new ReportParameter("CentroCostoInd", " ".ToString());
            }
            else
            {
                param[12] = new ReportParameter("CentroCostoCons", " ".ToString());
                param[13] = new ReportParameter("CentroCostoInd", "X".ToString());
            }



            mvEstadoResultados.Visible = true;
            RptReporte.LocalReport.EnableExternalImages = true;
            RptReporte.LocalReport.SetParameters(param);
            RptReporte.LocalReport.DataSources.Clear();
            RptReporte.LocalReport.Refresh();

            ReportDataSource rds = new ReportDataSource("DataSetEstadoResultados", CrearDataTableEstadoResultadosNIIF());
            RptReporte.LocalReport.DataSources.Add(rds);

            Site toolBar = (Site)Master;
            toolBar.MostrarConsultar(false);
            toolBar.MostrarLimpiar(false);
            frmPrint.Visible = false;
            RptReporte.Visible = true;
            mvEstadoResultados.ActiveViewIndex = 1;
        }
    
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTEstadoResultadoNIIF"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.DataSource = Session["DTEstadoResultadoNIIF"];
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
                Response.AddHeader("Content-Disposition", "attachment;filename=EstadoResultadoNIIF.xls");
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
        mvEstadoResultados.ActiveViewIndex = 0;
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



    protected void ChkOcultarCuenta_CheckedChanged(object sender, EventArgs e)
    {
        if(ChkOcultarCuenta.Checked==true)
        {
            Session["OCULTAR"]=1;
        }
        if (ChkOcultarCuenta.Checked == false)
        {
            Session["OCULTAR"] = null;
        }
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            String cuenta = Convert.ToString(e.Row.Cells[0].Text);
            String valor = Convert.ToString(e.Row.Cells[3].Text);


            if (cuenta == "" || cuenta == "&nbsp;" || cuenta == "0" || cuenta == "-1" || cuenta == " ")
            {
                e.Row.Cells[0].Text = " ";
                e.Row.Cells[0].Visible = true;
                gvLista.Columns[0].Visible = true;
                gvLista.Columns[1].Visible = true;
                e.Row.Cells[0].Text = "-";
                e.Row.Cells[0].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[1].Font.Bold = true;
                e.Row.Cells[1].Font.Size = 8;
                e.Row.Cells[2].Text = "";
                e.Row.Cells[2].Font.Size = 8;
                e.Row.Cells[3].Text = "";
                e.Row.Cells[3].Font.Size = 8;
                e.Row.Cells[4].Text = "";
                e.Row.Cells[4].Font.Size = 8;
                e.Row.Cells[5].Text = "";
                e.Row.Cells[5].Font.Size = 8;


            }


            if (cuenta == "99999")
            {
                e.Row.Cells[0].Text = " ";
                e.Row.Cells[0].Visible = true;

                gvLista.Columns[1].Visible = true;
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[1].Font.Size = 8;
                e.Row.Cells[1].Font.Bold = true;
                e.Row.Cells[2].Font.Bold = true;
                e.Row.Cells[2].Font.Size = 8;
                e.Row.Cells[3].Font.Bold = true;
                e.Row.Cells[3].Font.Size = 8;
                e.Row.Cells[4].Font.Bold = true;
                e.Row.Cells[4].Font.Size = 8;
                e.Row.Cells[5].Font.Bold = true;
                e.Row.Cells[5].Font.Size = 8;


            }
            if (cuenta == "999999")
            {
                e.Row.Cells[0].Text = " ";
                e.Row.Cells[0].Visible = true;
                e.Row.Cells[0].Text = "-";
                e.Row.Cells[0].ForeColor = System.Drawing.Color.White;

                gvLista.Columns[0].Visible = true;
                gvLista.Columns[1].Visible = true;
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[1].Font.Size = 9;
                e.Row.Cells[1].Font.Bold = true;
                e.Row.Cells[2].Font.Bold = true;
                e.Row.Cells[2].Font.Size = 9;
                e.Row.Cells[3].Font.Bold = true;
                e.Row.Cells[3].Font.Size = 9;
                e.Row.Cells[4].Font.Bold = true;
                e.Row.Cells[4].Font.Size = 9;
                e.Row.Cells[5].Font.Bold = true;
                e.Row.Cells[5].Font.Size = 9;

            }

            if (cuenta == "999999" && valor == "$ 0")
            {
                e.Row.Cells[0].Text = " ";
                e.Row.Cells[0].Visible = true;
                e.Row.Cells[0].Text = "-";
                e.Row.Cells[0].ForeColor = System.Drawing.Color.White;

                gvLista.Columns[0].Visible = true;
                gvLista.Columns[1].Visible = true;
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;

                e.Row.Cells[1].Font.Size = 9;
                e.Row.Cells[1].Font.Bold = true;
                e.Row.Cells[2].Font.Bold = true;
                e.Row.Cells[2].Font.Size = 9;
                e.Row.Cells[3].Font.Bold = true;
                e.Row.Cells[3].Font.Size = 9;
                e.Row.Cells[4].Font.Bold = true;
                e.Row.Cells[4].Font.Size = 9;
                e.Row.Cells[5].Font.Bold = true;
                e.Row.Cells[5].Font.Size = 9;
                
                e.Row.Cells[2].Text = "";
                e.Row.Cells[3].Text = "";
                e.Row.Cells[4].Text = "";
                e.Row.Cells[5].Text = "";


            }


            if (cuenta == "" || cuenta == "&nbsp;" && valor == "$ 0" )
            {
                e.Row.Cells[0].Text = " ";
                e.Row.Cells[0].Visible = true;
                e.Row.Cells[0].Text = "-";
                e.Row.Cells[0].ForeColor = System.Drawing.Color.White;

                gvLista.Columns[0].Visible = true;
                gvLista.Columns[1].Visible = true;
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;

                e.Row.Cells[1].Font.Size = 9;
                e.Row.Cells[1].Font.Bold = true;
                e.Row.Cells[2].Font.Bold = true;
                e.Row.Cells[2].Font.Size = 9;
                e.Row.Cells[3].Font.Bold = true;
                e.Row.Cells[3].Font.Size = 9;
                e.Row.Cells[4].Font.Bold = true;
                e.Row.Cells[4].Font.Size = 9;
                e.Row.Cells[5].Font.Bold = true;
                e.Row.Cells[5].Font.Size = 9;
             

                e.Row.Cells[2].Text = "";
                e.Row.Cells[3].Text = "";
                e.Row.Cells[4].Text = "";
                e.Row.Cells[5].Text = "";


            }
            if (cuenta == "" || cuenta == "&nbsp;")
            {
                e.Row.Cells[0].Text = " ";
                e.Row.Cells[0].Visible = true;
                e.Row.Cells[0].Text = "-";
                e.Row.Cells[0].ForeColor = System.Drawing.Color.White;

                gvLista.Columns[0].Visible = true;
                gvLista.Columns[1].Visible = true;
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;

                e.Row.Cells[1].Font.Size = 9;
                e.Row.Cells[1].Font.Bold = true;
                e.Row.Cells[2].Font.Bold = true;
                e.Row.Cells[2].Font.Size = 9;
                e.Row.Cells[3].Font.Bold = true;
                e.Row.Cells[3].Font.Size = 9;
                e.Row.Cells[4].Font.Bold = true;
                e.Row.Cells[4].Font.Size = 9;
                e.Row.Cells[5].Font.Bold = true;
                e.Row.Cells[5].Font.Size = 9;
             

                e.Row.Cells[2].Text = "-";
                e.Row.Cells[3].Text = "-";
                e.Row.Cells[4].Text = "-";
                e.Row.Cells[5].Text = "-";


            }




        }

        if (e.Row.RowType == DataControlRowType.Header)

        {
            if (ddlNivel.SelectedValue == "1")
            {
                gvLista.Columns[1].Visible = false;
                e.Row.Cells[0].Text = " ";
            }

        }

    }
}