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
using Xpinn.Programado.Entities;
using Xpinn.Programado.Services;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;

partial class Detalle : GlobalWeb
{
    MovimientoCuentasServices MovimientoCtaService = new MovimientoCuentasServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {       
            VisualizarOpciones(MovimientoCtaService.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarExportar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MovimientoCtaService.CodigoPrograma, "Page_PreInit", ex);
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
                if (Session[MovimientoCtaService.CodigoPrograma + ".id"] != null)
                {
                    if (Request.UrlReferrer.Segments[4].ToString() == "EstadoCuenta/")
                        Session["Retorno"] = "1";
                    idObjeto = Session[MovimientoCtaService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(MovimientoCtaService.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }                
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MovimientoCtaService.CodigoPrograma, "Page_Load", ex);
        }
    }


    private void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            CuentasProgramado pData = new CuentasProgramado();
            pData = MovimientoCtaService.ConsultarAhorrosProgramado(pIdObjeto,(Usuario)Session["usuario"]);

            if (pData.numero_programado != null)
                txtNumCta.Text = pData.numero_programado;
            if (pData.nomlinea != null)
                txtLinea.Text = pData.nomlinea.Trim();
            if (pData.saldo != null || pData.saldo != 0)
                txtSaldoTotal.Text = pData.saldo.ToString();
            if (pData.nom_estado != null)
                txtEstado.Text = pData.nom_estado.ToString();
            if (pData.fecha_apertura != DateTime.MinValue || pData.fecha_apertura != null)
                txtFechaApertura.Text = Convert.ToDateTime(pData.fecha_apertura).ToShortDateString();
            if (pData.fecha_ultimo_pago != null && pData.fecha_ultimo_pago != DateTime.MinValue)
                txtFechaUltMov.Text = Convert.ToDateTime(pData.fecha_ultimo_pago).ToShortDateString();
            if (pData.fecha_proximo_pago != null && pData.fecha_proximo_pago != DateTime.MinValue)
                txtProxPago.Text = Convert.ToDateTime(pData.fecha_proximo_pago).ToShortDateString();
            if (pData.nomforma_pago != null)
                txtFormaPago.Text = pData.nomforma_pago.Trim();
            if (pData.nom_periodicidad != null)
                txtPeriodicidad.Text = pData.nom_periodicidad.Trim();
            if (pData.valor_cuota != null)
                txtCuota.Text = pData.valor_cuota.ToString();

            if (pData.cod_persona != 0 || pData.cod_persona != null)
                txtCodPersona.Text = pData.cod_persona.ToString();
            if (pData.identificacion != null)
                txtIdentificacion.Text = pData.identificacion;
            if (pData.nombre != null)
                txtNombre.Text = pData.nombre;

            txtFechaIni.Text = txtFechaApertura.Text;
            txtFechaFin.Text = DateTime.Now.ToShortDateString();

            Actualizar();

            Session.Add(MovimientoCtaService.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MovimientoCtaService.CodigoPrograma, "Actualizar", ex);
        }
    }


    private void Actualizar()
    {
        try
        {
          Configuracion conf = new Configuracion();
           List<Xpinn.Ahorros.Entities.ReporteMovimiento> lstConsulta = new List<Xpinn.Ahorros.Entities.ReporteMovimiento>();
           // DateTime pFechaIni, pFechaFin;
           DateTime? pFechaIni = null, pFechaFin = null;
        
            pFechaIni = txtFechaIni.ToDateTime == null ? DateTime.MinValue : txtFechaIni.ToDateTime;
            pFechaFin = txtFechaFin.ToDateTime == null ? DateTime.MinValue : txtFechaFin.ToDateTime;
            if (ValidarFechas())
            {
                if (idObjeto != "")
                    lstConsulta = MovimientoCtaService.ListarDetalleMovimiento(Convert.ToString(idObjeto),Convert.ToDateTime(txtFechaIni.Text), Convert.ToDateTime(txtFechaFin.Text), (Usuario)Session["usuario"]);

                gvLista.PageSize = 20;
                String emptyQuery = "Fila de datos vacia";
                gvLista.EmptyDataText = emptyQuery;

                Site toolBar = (Site)this.Master;
                if (lstConsulta.Count > 0)
                {
                    gvLista.DataSource = lstConsulta;
                    gvLista.DataBind();
                    panelGrilla.Visible = true;
                    lblInfo.Visible = false;
                    Session["DTREPORTE"] = lstConsulta;
                    toolBar.MostrarExportar(true);
                }
                else
                {
                    Session["DTREPORTE"] = null;
                    panelGrilla.Visible = false;
                    lblInfo.Visible = true;
                    toolBar.MostrarExportar(false);
                }

                Session.Add(MovimientoCtaService.CodigoPrograma + ".consulta", 1);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MovimientoCtaService.CodigoPrograma, "Actualizar", ex);
        }
    }


    bool ValidarFechas()
    {
        if (txtFechaIni.Text == "")
        {
            VerError("Ingrese la fecha inicial");
            txtFechaIni.Focus();
            return false;
        }
        if (txtFechaFin.Text == "")
        {
            VerError("Ingrese la fecha final");
            txtFechaFin.Focus();
            return false;
        }
        if (txtFechaIni.Text != "" && txtFechaFin.Text != "")
        {
            if (Convert.ToDateTime(txtFechaIni.Text) > Convert.ToDateTime(txtFechaFin.Text))
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
        if (Session["Retorno"] == "1")
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

            List<Xpinn.Ahorros.Entities.ReporteMovimiento> lstConsulta = new List<Xpinn.Ahorros.Entities.ReporteMovimiento>();
            lstConsulta = (List<Xpinn.Ahorros.Entities.ReporteMovimiento>)Session["DTREPORTE"];
            //CARGANDO LA TABLA
            if (Session["DTREPORTE"] != null)
            {
                foreach (Xpinn.Ahorros.Entities.ReporteMovimiento rMovim in lstConsulta)
                {
                    DataRow dr;
                    dr = table.NewRow();
                    dr[0] = rMovim.fecha.ToShortDateString();
                    dr[1] = rMovim.cod_ope;
                    dr[2] = rMovim.descripcion;
                    dr[3] = rMovim.num_comp;
                    dr[4] = rMovim.tipo_comp;
                    dr[5] = rMovim.tipo_mov;
                    dr[6] = rMovim.valor.ToString("n0");
                    dr[7] = rMovim.saldo.ToString("n0");
                    table.Rows.Add(dr);
                }
            }

            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            ReportParameter[] param = new ReportParameter[17];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);
            param[2] = new ReportParameter("ImagenReport", ImagenReporte());
            param[3] = new ReportParameter("numCuenta", txtNumCta.Text.Trim());
            param[4] = new ReportParameter("linea", txtLinea.Text.Trim());
            param[5] = new ReportParameter("saldoTotal", Convert.ToDecimal(txtSaldoTotal.Text).ToString("n"));
            param[6] = new ReportParameter("estado", txtEstado.Text.Trim());
            param[7] = new ReportParameter("fechaApertura", txtFechaApertura.Text);
            param[8] = new ReportParameter("fechaUltMov", txtFechaUltMov.Text);
            param[9] = new ReportParameter("fechaProxPago", txtProxPago.Text);
            param[10] = new ReportParameter("formaPago", txtFormaPago.Text.Trim());
            param[11] = new ReportParameter("Periodicidad", txtPeriodicidad.Text);
            param[12] = new ReportParameter("Cuota", Convert.ToDecimal(txtCuota.Text).ToString("n"));
            param[13] = new ReportParameter("codigo", txtCodPersona.Text.Trim());
            param[14] = new ReportParameter("identificacion", txtIdentificacion.Text.Trim());
            param[15] = new ReportParameter("nombre", txtNombre.Text.Trim());
            string rpta = "true";
            if (table.Rows.Count > 0)
                rpta = "false";
            param[16] = new ReportParameter("MostrarGrilla", rpta);

            rvReportMov.LocalReport.DataSources.Clear();
            rvReportMov.LocalReport.EnableExternalImages = true;
            rvReportMov.LocalReport.SetParameters(param);

            ReportDataSource rds = new ReportDataSource("DataSet1", table);
            rvReportMov.LocalReport.DataSources.Add(rds);
            rvReportMov.LocalReport.Refresh();

            frmPrint.Visible = false;
            rvReportMov.Visible = true;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarImprimir(false);

            mvPrincipal.ActiveViewIndex = 1;
        }
        catch(Exception ex)
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
            Response.AddHeader("Content-Disposition", "attachment;filename=MovimientoCuentas.xls");
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
            BOexcepcion.Throw(MovimientoCtaService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void btnDatos_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)this.Master;
        toolBar.MostrarImprimir(true);
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
        FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output.pdf"),
        FileMode.Create);
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();
        Session["Archivo" + Usuario.codusuario] = Server.MapPath("output.pdf");
        frmPrint.Visible = true;
        rvReportMov.Visible = false;
    }

    protected void btnConsultar_Click1(object sender, ImageClickEventArgs e)
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
}