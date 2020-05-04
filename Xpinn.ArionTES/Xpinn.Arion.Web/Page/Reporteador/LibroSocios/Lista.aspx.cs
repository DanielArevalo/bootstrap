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

    Xpinn.Reporteador.Services.ReporteService librosociosService = new Xpinn.Reporteador.Services.ReporteService();
    Usuario _usuario;
    private static string pCod_Programa;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
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

        //Llenar fecha de cierre historico
        List<Xpinn.Reporteador.Entities.TransaccionEfectivo> lstFechaHistorico = new List<Xpinn.Reporteador.Entities.TransaccionEfectivo>();
        lstFechaHistorico = librosociosService.ListarfechaCierrHist(_usuario);
        ddlFechaCorte.DataSource = lstFechaHistorico;
        ddlFechaCorte.DataTextFormatString = "{0:dd/MM/yyyy}";
        ddlFechaCorte.DataTextField = "fecha_tran";
        ddlFechaCorte.DataBind();
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
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
            // gvLista.PageIndex = e.NewPageIndex;
            // Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pCod_Programa, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        VerError("");
        try
        {
            Xpinn.Aportes.Services.AporteServices APlibrosociosService = new Xpinn.Aportes.Services.AporteServices();
            List<Xpinn.Aportes.Entities.Aporte> LSTAPORTES = new List<Xpinn.Aportes.Entities.Aporte>();

            bool incluye_retirados = false;
            incluye_retirados = chkincluyeRetirados.Checked;
            LSTAPORTES = APlibrosociosService.RptLibroSocios(ddlFechaCorte.SelectedItem.ToString(), incluye_retirados, _usuario);


            gvLista.DataSource = LSTAPORTES;
            ViewState["DTLISTA"] = LSTAPORTES;


            if (LSTAPORTES.Count > 0)
            {
                btnExportar.Visible = true;
                btnInforme.Visible = true;
                divgrid.Visible = true;
                Session["DTREPORTE"] = LSTAPORTES;
                gvLista.Visible = true;
                decimal a = 0;
                a = Convert.ToDecimal(LSTAPORTES.Where(x => x.cod_nomina != null).Sum(x => 1));


                /*foreach (Xpinn.Aportes.Entities.Aporte aporte in LSTAPORTES)
                {
                    // a = aporte.cod_nomina != null ? Convert.ToInt32(aporte.cod_nomina) : 0 ;
                    a = aporte.cod_nomina != null ? 1 : 0;
                    a = a + a;
                }*/
                if (a == 0)
                {
                    gvLista.Columns[0].Visible = false;
                }
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados: " + LSTAPORTES.Count.ToString();
                gvLista.DataBind();

                if (chkincluyeRetirados.Checked)
                {
                    gvLista.Columns[10].Visible = true;
                    gvLista.Columns[11].Visible = true;
                }
                else
                {
                    {
                        gvLista.Columns[10].Visible = true;
                        gvLista.Columns[11].Visible = true;
                    }
                }
                Site toolBar = (Site)this.Master;
                toolBar.MostrarExportar(true);
            }
            else
            {
                divgrid.Visible = false;
                btnExportar.Visible = false;
                btnInforme.Visible = false;
                Session["DTREPORTE"] = null;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                Site toolBar = (Site)this.Master;
                toolBar.MostrarExportar(true);
            }



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



    /// <summary>
    /// Método para generar el informe pasando los parámetros correspondientes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInforme_Click(object sender, EventArgs e)
    {
        string separadorDecimal = System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
        Configuracion conf = new Configuracion();

        System.Data.DataTable table = new System.Data.DataTable();
        if (gvLista.Rows.Count > 0)
        {
            //Crear encabezado   
            table.Columns.Add("cod_nomina");
            table.Columns.Add("identificacion");
            table.Columns.Add("nombre");
            table.Columns.Add("fecha_naci");
            table.Columns.Add("direccion");
            table.Columns.Add("fecha_ingreso");
            table.Columns.Add("ahorro_permanente");
            table.Columns.Add("aporte_social");
            table.Columns.Add("total_ahorro_aporte");
            table.Columns.Add("cartera");
            table.Columns.Add("estado");
            table.Columns.Add("telefono");
            table.Columns.Add("sexo");
            table.Columns.Add("cod_empresa");
            table.Columns.Add("nom_empresa");
            //table.Columns.Add("fecha_retiro");

            List<Xpinn.Aportes.Entities.Aporte> lstDetalle = new List<Xpinn.Aportes.Entities.Aporte>();
            lstDetalle = (List<Xpinn.Aportes.Entities.Aporte>)Session["DTREPORTE"];

            if (lstDetalle.Count > 0)
            {
                foreach (Xpinn.Aportes.Entities.Aporte rFila in lstDetalle)
                {
                    DataRow dr;
                    dr = table.NewRow();
                    dr[0] = " " + rFila.cod_nomina;
                    dr[1] = " " + rFila.identificacion;
                    dr[2] = " " + rFila.nombre;
                    dr[3] = " " + rFila.fecha_apertura.ToShortDateString();
                    dr[4] = " " + rFila.direccion;
                    dr[5] = " " + rFila.fecha_crea.ToShortDateString();
                    dr[6] = " " + Math.Round(rFila.otros).ToString().Replace(gSeparadorDecimal, separadorDecimal);
                    dr[7] = " " + Math.Round((double)rFila.valor_acumulado).ToString().Replace(gSeparadorDecimal, separadorDecimal);
                    dr[8] = " " + Math.Round(rFila.total).ToString().Replace(gSeparadorDecimal, separadorDecimal);
                    dr[9] = " " + Math.Round(rFila.cartera).ToString().Replace(gSeparadorDecimal, separadorDecimal);
                    //dr[9] = " " + rFila.estado_modificacion;
                    // dr[10] = " " + rFila.fecha_ultima_mod;
                    table.Rows.Add(dr);
                }
            }
        }

        //Pasar Valores al reporte
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];

        ReportParameter[] param = new ReportParameter[4];
        param[0] = new ReportParameter("entidad", pUsuario.empresa);
        param[1] = new ReportParameter("nit", pUsuario.nitempresa);
        param[2] = new ReportParameter("fecha", ddlFechaCorte.SelectedValue);
        param[3] = new ReportParameter("ImagenReport", ImagenReporte());

        RptReporte.LocalReport.DataSources.Clear();
        RptReporte.LocalReport.EnableExternalImages = true;
        RptReporte.LocalReport.SetParameters(param);
        ReportDataSource rds = new ReportDataSource("DataSet1", table);
        RptReporte.LocalReport.DataSources.Add(rds);
        RptReporte.LocalReport.Refresh();

        mvBalance.ActiveViewIndex = 1;
        RptReporte.Visible = true;
        frmPrint.Visible = false;

    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0 && ViewState["DTLISTA"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.AllowPaging = false;
            gvLista.DataSource = ViewState["DTLISTA"];
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
            Response.AddHeader("Content-Disposition", "attachment;filename=ReporteSocios.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            gvLista.AllowPaging = true;
            gvLista.DataSource = ViewState["DTLISTA"];
            gvLista.DataBind();
            Response.End();
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

}