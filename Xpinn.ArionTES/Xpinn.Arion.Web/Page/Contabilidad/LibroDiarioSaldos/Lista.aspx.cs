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
using System.Threading;

partial class Lista : GlobalWeb
{
    private Xpinn.Contabilidad.Services.LibroDiarioService LibroDiarioSer = new Xpinn.Contabilidad.Services.LibroDiarioService();
    private static string pCod_Programa;

  
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
      
    }

    private void Actualizar()
    {
        VerError("");

        try
        {
            List<Xpinn.Contabilidad.Entities.LibroDiario> lstConsulta = new List<Xpinn.Contabilidad.Entities.LibroDiario>();
            Xpinn.Contabilidad.Entities.LibroDiario penditad = new Xpinn.Contabilidad.Entities.LibroDiario();
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
                        penditad.cenini = Convert.ToInt64(Session["CenIni"].ToString());
                        penditad.cenfin = Convert.ToInt64(Session["CenFin"].ToString());
                      
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
                    penditad.cenini = Convert.ToInt64(ddlCentroCosto.SelectedValue);
                    penditad.cenfin = Convert.ToInt64(ddlCentroCosto.SelectedValue);
                }
                catch (Exception ex)
                {
                    VerError(ex.Message);
                    return;
                }
            }

            // determinar los datos para generar el libro auxiliar
            penditad.fecha_ini = Convert.ToDateTime(txtFecIni.ToDate);
            penditad.fecha_fin = Convert.ToDateTime(txtFecFin.ToDate);
            penditad.nivelint= Convert.ToInt64(ddlNivel.SelectedItem.Text);
            FecIni = Convert.ToDateTime(txtFecIni.ToDate);
            FecFin = Convert.ToDateTime(txtFecFin.ToDate);

            // determinar si se genera por cuenta o por fecha
            Boolean bCuenta = true;
            if (chkCuenta.Checked == true)
                bCuenta = true;
            else
                bCuenta = false;

            double TotDeb = 0;
            double TotCre = 0;

            // Determinando la fecha Inicial y Final
            if (ViewState["COD_PROGRAMA"] != null)
            {
                if (ViewState["COD_PROGRAMA"].ToString() == "NIIF")
                    lstConsulta = LibroDiarioSer.ListarDiarioNiif(CenIni, CenFin, FecIni, FecFin, bCuenta, Convert.ToInt64(ddlMoneda.Value), (Usuario)Session["Usuario"]);
                else
                    lstConsulta = LibroDiarioSer.ListarLibroDiario(penditad, (Usuario)Session["Usuario"],ref TotDeb,ref TotCre);
            }
            else
                lstConsulta = LibroDiarioSer.ListarLibroDiario(penditad, (Usuario)Session["Usuario"], ref TotDeb, ref TotCre);



            gvLista.Visible = true; 
                gvLista.PageSize = pageSize;
                gvLista.EmptyDataText = emptyQuery;
                ViewState["DTLIBRODIARIO"] = lstConsulta;
                gvLista.DataSource = lstConsulta;
                Session["TOTDEB"] = TotDeb;
                Session["TOTCRE"] = TotCre;
            penditad.nombre_cuenta= "TOTALES";

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
              
                    gvLista.AllowPaging = false;
                    gvLista.DataSource = ViewState["DTLIBRODIARIO"];
                    gvLista.DataBind();
                    gvLista.EnableViewState = false;
          
               
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                if (chkFecha.Checked == true)
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
                if (chkFecha.Checked == true)
                {
                    gvLista.AllowPaging = true;
                    gvLista.DataBind();
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

        foreach (Xpinn.Contabilidad.Entities.LibroDiario item in lstConsulta)
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

        // ---------------------------------------------------------------------------------------------------------
        // Pasar datos al reporte
        // ---------------------------------------------------------------------------------------------------------


        //ReportParameter[] param = new ReportParameter[6];

        //Usuario pUsuario = new Usuario();
        //pUsuario = (Usuario)Session["Usuario"];

        //param[0] = new ReportParameter("entidad", pUsuario.empresa);
        //param[1] = new ReportParameter("nit", pUsuario.nitempresa);
        //param[2] = new ReportParameter("fecini", txtFecIni.ToDate);
        //param[3] = new ReportParameter("fecfin", txtFecFin.ToDate);
        //param[4] = new ReportParameter("centro_costo", ddlCentroCosto.SelectedItem.ToString());
        //param[5] = new ReportParameter("ImagenReport", ImagenReporte());

        //rvLibAux.LocalReport.EnableExternalImages = true;
        //rvLibAux.LocalReport.SetParameters(param);

        //ReportDataSource rds = new ReportDataSource("DataSet1", table);
        //rvLibAux.LocalReport.DataSources.Clear();
        //if (chkFecha.Checked == true)
        //    rvLibAux.LocalReport.ReportPath = "Page/Contabilidad/LibroDiario/ReporteLibroDiario.rdlc";
        //else
        //    rvLibAux.LocalReport.ReportPath = "Page/Contabilidad/LibroDiario/ReporteLibroDiarioCta.rdlc";
        //rvLibAux.LocalReport.DataSources.Add(rds);
        //rvLibAux.LocalReport.Refresh();

        //// Mostrar el reporte en pantalla.
        //mvLibroDiario.ActiveViewIndex = 1;

        Double TotDeb = 0;
        Double TotCre = 0;
        TotDeb = (Double)Session["TOTDEB"];
        TotCre = (Double)Session["TOTCRE"];

        

        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        ReportParameter[] param = new ReportParameter[7];
        param[0] = new ReportParameter("entidad", pUsuario.empresa);
        param[1] = new ReportParameter("nit", pUsuario.nitempresa);
        param[2] = new ReportParameter("fecha", txtFecIni.ToDate);
        if (ddlCentroCosto.SelectedValue == "0")
            param[3] = new ReportParameter("centro_costo", "CONSOLIDADO");
        else
            param[3] = new ReportParameter("centro_costo", ddlCentroCosto.SelectedValue);
        param[4] = new ReportParameter("TotDeb", TotDeb.ToString());
        param[5] = new ReportParameter("TotCre", TotCre.ToString());
        param[6] = new ReportParameter("ImagenReport", ImagenReporte());

      
        rvLibAux.LocalReport.EnableExternalImages = true;
        rvLibAux.LocalReport.SetParameters(param);
        rvLibAux.LocalReport.DataSources.Clear();
        rvLibAux.LocalReport.Refresh();

        ReportDataSource rds = new ReportDataSource("DataSet1", table);

        rvLibAux.LocalReport.DataSources.Add(rds);
        rvLibAux.Visible = true;
      
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
}