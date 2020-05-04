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
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;

partial class Detalle : GlobalWeb
{
    ReporteMovimientoServices ReporteMovService = new ReporteMovimientoServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ReporteMovService.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.eventoExportar += btnExportar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteMovService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["Retorno"] = "0";
                mvPrincipal.ActiveViewIndex = 0;
                if (Session[ReporteMovService.CodigoPrograma + ".id"] != null)
                {
                    if (Request.UrlReferrer.Segments[4].ToString() == "EstadoCuenta/")
                        Session["Retorno"] = "1";
                    idObjeto = Session[ReporteMovService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ReporteMovService.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteMovService.CodigoPrograma, "Page_Load", ex);
        }
    }


    private void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            AhorroVista pData = new AhorroVista();
            pData = ReporteMovService.ConsultarAhorroVista(pIdObjeto, (Usuario)Session["usuario"]);

            if (pData.numero_cuenta != null)
                txtNumCta.Text = pData.numero_cuenta;
            if (pData.nom_linea != null)
                txtLinea.Text = pData.nom_linea.Trim();            
            if (!string.IsNullOrEmpty(pData.saldo_total.ToString()))
                txtSaldoTotal.Text = pData.saldo_total.ToString("n");
            if (pData.saldo_canje != null && pData.saldo_canje != 0)
                txtSaldoCanje.Text = Convert.ToDecimal(pData.saldo_canje).ToString("n0");
            if (pData.nom_estado != null)
                txtEstado.Text = pData.nom_estado.ToString();
            if (pData.fecha_apertura != DateTime.MinValue || pData.fecha_apertura != null)
                txtFechaApertura.Text = Convert.ToDateTime(pData.fecha_apertura).ToShortDateString();
            if (pData.fecha_ultimo_pago != null)
                txtFechaUltMov.Text = Convert.ToDateTime(pData.fecha_ultimo_pago).ToShortDateString();
            if (pData.fecha_proximo_pago != null)
                txtProxPago.Text = Convert.ToDateTime(pData.fecha_proximo_pago).ToShortDateString();
            if (pData.nom_formapago != null)
                txtFormaPago.Text = pData.nom_formapago.Trim();
            if (pData.nom_periodicidad != null)
                txtPeriodicidad.Text = pData.nom_periodicidad.Trim();
            if (pData.valor_cuota != null)
                txtCuota.Text = pData.valor_cuota.ToString();
            if (pData.cod_persona != 0 || pData.cod_persona != null)
                txtCodPersona.Text = pData.cod_persona.ToString();
            if (pData.identificacion != null)
                txtIdentificacion.Text = pData.identificacion;
            if (pData.nombres != null)
                txtNombre.Text = pData.nombres;

            //ucFechaInicial.Text = txtFechaApertura.Text;
            Configuracion conf = new Configuracion();
            ucFechaInicial.Text = (new DateTime(DateTime.Now.Year, 1, 1)).ToString(conf.ObtenerFormatoFecha());
            ucFechaFinal.Text = DateTime.Now.ToShortDateString();

            Actualizar();

            Session.Add(ReporteMovService.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteMovService.CodigoPrograma, "Actualizar", ex);
        }
    }


    private void Actualizar()
    {
        try
        {
            Configuracion conf = new Configuracion();
            List<ReporteMovimiento> lstConsulta = new List<ReporteMovimiento>();
           // DateTime pFechaIni, pFechaFin;
          

            // Consultar los datos dela porte
            //DateTime? fechaInicial = null, fechaFinal = null;
            if (ucFechaInicial.Text != "")
               // fechaInicial = DateTime.ParseExact(ucFechaInicial.ToDateTime.ToShortDateString(), gFormatoFecha, null);

            //fechaInicial = Convert.ToDateTime(ucFechaInicial.Text);
            //if (ucFechaFinal.Text != "")
            //    fechaFinal = DateTime.ParseExact(ucFechaInicial.ToDateTime.ToShortDateString(), gFormatoFecha, null);
            

           //ReporteMovService.EliminarAhorroVista(Convert.ToString(idObjeto), (Usuario)Session["usuario"]);


            if (ValidarFechas())
            {
                if (idObjeto != "")
                    lstConsulta = ReporteMovService.ListarReporteMovimiento(Convert.ToInt64(idObjeto), Convert.ToDateTime(ucFechaInicial.Text), Convert.ToDateTime(ucFechaFinal.Text), (Usuario)Session["usuario"]);

                gvLista.PageSize = 20;
                String emptyQuery = "Fila de datos vacia";
                gvLista.EmptyDataText = emptyQuery;

                if (lstConsulta.Count > 0)
                {
                    gvLista.DataSource = lstConsulta;
                    gvLista.DataBind();
                    panelGrilla.Visible = true;
                    lblInfo.Visible = false;
                    Session["DTREPORTE"] = lstConsulta;
                }
                else
                {
                    Session["DTREPORTE"] = null;
                    panelGrilla.Visible = false;
                    lblInfo.Visible = true;
                }

                Session.Add(ReporteMovService.CodigoPrograma + ".consulta", 1);
            }
        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteMovService.CodigoPrograma, "Actualizar", ex);
        }
    }


    bool ValidarFechas()
    {
        if (ucFechaInicial.Text == "")
        {
            VerError("Ingrese la fecha inicial");
            ucFechaInicial.Focus();
            return false;
        }
        if (ucFechaFinal.Text == "")
        {
            VerError("Ingrese la fecha final");
            ucFechaFinal.Focus();
            return false;
        }
        if (ucFechaInicial.Text != "" && ucFechaFinal.Text != "")
        {
            if (Convert.ToDateTime(ucFechaInicial.Text) > Convert.ToDateTime(ucFechaInicial.Text))
            {
                VerError("Datos erroneos en el rango de fechas ingresadas");
                return false;
            }
        }
        return true;
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarFechas())
        {
            Page.Validate();
            gvLista.Visible = true;
            if (Page.IsValid)
            {
                Actualizar();
            }
        }
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        if ((string)Session["Retorno"] == "1")
            Navegar("~/Page/Asesores/EstadoCuenta/Detalle.aspx");
        else
            Navegar(Pagina.Lista);
    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //CREACION DE LA TABLA
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("fecha");
            table.Columns.Add("nro_operacion");
            table.Columns.Add("descripcion");
            table.Columns.Add("num_comp");
            table.Columns.Add("tipo_comp");
            table.Columns.Add("tipo_mov");
            table.Columns.Add("valor");
            table.Columns.Add("saldo");

            List<ReporteMovimiento> lstConsulta = new List<ReporteMovimiento>();
            lstConsulta = (List<ReporteMovimiento>)Session["DTREPORTE"];
            //CARGANDO LA TABLA
            if (Session["DTREPORTE"] != null)
            {
                foreach (ReporteMovimiento rMovim in lstConsulta)
                {
                    DataRow dr;
                    dr = table.NewRow();
                    dr[0] = rMovim.fecha.ToShortDateString();
                    dr[1] = rMovim.cod_ope;
                    dr[2] = rMovim.descripcion == null ? rMovim.tipo_ope : rMovim.descripcion;
                    dr[3] = rMovim.num_comp;
                    dr[4] = rMovim.tipo_comp;
                    dr[5] = rMovim.tipo_mov;
                    dr[6] = rMovim.valor.ToString("n");
                    dr[7] = rMovim.saldo.ToString("n");
                    table.Rows.Add(dr);
                }
            }

            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            ReportParameter[] param = new ReportParameter[18];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);
            param[2] = new ReportParameter("ImagenReport", ImagenReporte());
            param[3] = new ReportParameter("numCuenta", txtNumCta.Text.Trim());
            param[4] = new ReportParameter("linea", txtLinea.Text.Trim());
            param[5] = new ReportParameter("saldoTotal", Convert.ToDecimal(txtSaldoTotal.Text).ToString("n"));
            param[6] = new ReportParameter("SaldoCanje", Convert.ToDecimal(txtSaldoCanje.Text.Trim() == "" ? "0" : txtSaldoCanje.Text).ToString("n"));
            param[7] = new ReportParameter("estado", txtEstado.Text.Trim());
            param[8] = new ReportParameter("fechaApertura", txtFechaApertura.Text);
            param[9] = new ReportParameter("fechaUltMov", " " + txtFechaUltMov.Text);
            param[10] = new ReportParameter("fechaProxPago", txtProxPago.Text);
            param[11] = new ReportParameter("formaPago", txtFormaPago.Text.Trim());
            param[12] = new ReportParameter("Periodicidad", txtPeriodicidad.Text);
            param[13] = new ReportParameter("Cuota", Convert.ToDecimal(txtCuota.Text).ToString("n"));
            param[14] = new ReportParameter("codigo", txtCodPersona.Text.Trim());
            param[15] = new ReportParameter("identificacion", txtIdentificacion.Text.Trim());
            param[16] = new ReportParameter("nombre", txtNombre.Text.Trim());
            string rpta = "true";
            if (table.Rows.Count > 0)
                rpta = "false";
            param[17] = new ReportParameter("MostrarGrilla", rpta);

            rvReportMov.LocalReport.EnableExternalImages = true;
            rvReportMov.LocalReport.SetParameters(param);

            rvReportMov.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DataSet1", table);
            rvReportMov.LocalReport.DataSources.Add(rds);
            rvReportMov.LocalReport.Refresh();

            frmPrint.Visible = false;
            rvReportMov.Visible = true;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarImprimir(false);
            toolBar.MostrarConsultar(false);
            toolBar.MostrarExportar(false);
            mvPrincipal.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0 && Session["DTREPORTE"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.AllowPaging = false;
            gvLista.DataSource = Session["DTREPORTE"];
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
            Response.AddHeader("Content-Disposition", "attachment;filename=ReporteMovimiento.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
        {
            VerError("No existen datos, genere la consulta");
        }
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
            BOexcepcion.Throw(ReporteMovService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void btnDatos_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)this.Master;
        toolBar.MostrarImprimir(true);
        toolBar.MostrarConsultar(true);
        toolBar.MostrarExportar(true);
        mvPrincipal.ActiveViewIndex = 0;
    }

    protected void btnImpresion_Click(object sender, EventArgs e)
    {
        // MOSTRAR REPORTE EN PANTALLA
        Warning[] warnings;
        string[] streamids;
        string mimeType;
        string encoding;
        string extension;
        byte[] bytes = rvReportMov.LocalReport.Render("PDF", null, out mimeType,
                       out encoding, out extension, out streamids, out warnings);
        Xpinn.Util.Usuario pUsuario = (Xpinn.Util.Usuario)Session["usuario"];
        string pNomUsuario = pUsuario.nombre != "" && pUsuario.nombre != null ? "_" + pUsuario.nombre : "";
        FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output" + pNomUsuario + ".pdf"),
        FileMode.Create);
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();
        Session["Archivo" + Usuario.codusuario] = Server.MapPath("output" + pNomUsuario + ".pdf");
        frmPrint.Visible = true;
        rvReportMov.Visible = false;
    }

    protected void btnConsular_Click(object sender, ImageClickEventArgs e)
    {
        // gvLista.Visible = false; 
        gvLista.DataSource = null;
        gvLista.DataBind();
   //    ReporteMovService.EliminarAhorroVista(Convert.ToString(idObjeto), (Usuario)Session["usuario"]);

        Actualizar();
        // gvLista.Visible = true;
    }

}