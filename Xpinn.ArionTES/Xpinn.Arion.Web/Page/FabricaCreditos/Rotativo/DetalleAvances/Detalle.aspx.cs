using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Util;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Data;
using Microsoft.Reporting.WebForms;

public partial class EstadoCuentaCreditoAvances : GlobalWeb
{

    EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
    MovGralCreditoService servicemovgeneral = new MovGralCreditoService();
    Producto entityProducto;
    List<DetalleProducto> lstConsulta = new List<DetalleProducto>();
    List<ConsultaAvance> lstAvances = new List<ConsultaAvance>();
    Int64 tipolinea = 0;


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(servicemovgeneral.CodigoProgramaAvances, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += btnRegresar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ImprimirGrilla();

            if (!IsPostBack)
            {
                if (Session["Retorno"] != null)
                    if (Session["Retorno"].ToString() == "A")
                        Session["Retorno"] = "A";
                    else
                        Session["Retorno"] = "0";
                else
                    Session["Retorno"] = "0";
                Rpview1.Visible = false;
                MostrarPeriodo(false);
                ucFechaInicial.ToDateTime = Convert.ToDateTime("01/01/0001");
                ucFechaFinal.ToDateTime = DateTime.Today;

                if (Session[MOV_GRAL_CRED_PRODUC] != null)
                {
                    Actualizar();
                    btnConsultar.Visible = true;
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "D", "Page_Load", ex);
        }
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["Retorno"].ToString() == "0")
            Navegar("~/Page/Asesores/MoviGralCredito/Lista.aspx");
        else if (Session["Retorno"].ToString() == "A")
            Navegar("~/Page/FabricaCreditos/Rotativo/DetalleAvances/Lista.aspx");
        else
            Navegar("~/Page/Asesores/EstadoCuenta/Detalle.aspx");


    }
    protected void gvMovGeneral_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvMovGeneral.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }


    protected void ImprimirGrilla()
    {
        string printScript =
           @"function PrintGridView()
                {         
                div = document.getElementById('DivButtons');
                div.style.display='none';
                var gridInsideDiv = document.getElementById('gvDiv');
                var printWindow = window.open('gview.htm','PrintWindow','letf=0,top=0,width=150,height=300,toolbar=1,scrollbars=1,status=1');
                printWindow.document.write(gridInsideDiv.innerHTML);
                printWindow.document.close();
                printWindow.focus();
                printWindow.print();
                printWindow.close();}";
        this.ClientScript.RegisterStartupScript(Page.GetType(), "PrintGridView", printScript.ToString(), true);

    }

    /// <summary>
    /// Mostrar los datos del crèdito junto con los movimientos
    /// </summary>
    private void Actualizar()
    {
        try
        {
            try
            {
                Configuracion conf = new Configuracion();
                int detallado = 0;

                detallado = 1;

                Rpview1.Visible = false;

                entityProducto = (Producto)(Session[MOV_GRAL_CRED_PRODUC]);
                entityProducto.noconsultaTodo = 1;
                Int64 radicado = Convert.ToInt64(Request["radicado"]);
                entityProducto.CodRadicacion = radicado.ToString();

                // Traer la informaciòn del crèdito junto con listado de movimientos
                lstConsulta = serviceEstadoCuenta.ListarDetalleProductos(entityProducto, (Usuario)Session["usuario"], detallado);

                // Mostrar los datos consultados
                var detalle = lstConsulta.First(s => s.NumeroRadicacion == Convert.ToInt64(entityProducto.CodRadicacion));
                txtCodCliente.Text = detalle.Producto.Persona.IdPersona.ToString();
                txtTipoDoc.Text = detalle.Producto.Persona.TipoIdentificacion.NombreTipoIdentificacion.Substring(0, 3) + "  " + detalle.Producto.Persona.NumeroDocumento.ToString();
                txtNombres.Text = detalle.Producto.Persona.PrimerNombre + "  " + detalle.Producto.Persona.SegundoNombre + "  " + detalle.Producto.Persona.PrimerApellido + "  " + detalle.Producto.Persona.SegundoApellido;
                TxtEstado.Text = detalle.Producto.Estado;
                if (detalle.FechaUltimoPago.ToString(conf.ObtenerFormatoFecha()) == "01/01/0001")
                    TxtFechaUltimoPago.Text = "       ";
                else
                    TxtFechaUltimoPago.Text = detalle.FechaUltimoPago.ToString(conf.ObtenerFormatoFecha());

                if (detalle.FechaProximoPago.ToString(conf.ObtenerFormatoFecha()) == "01/01/0001")
                    TxtFechaCuota.Text = "       ";
                else
                    TxtFechaCuota.Text = detalle.FechaProximoPago.ToString(conf.ObtenerFormatoFecha());

                if (detalle.FechaAprobacion.ToString(conf.ObtenerFormatoFecha()) == "01/01/0001")
                    TxtFechaInicial.Text = "       ";
                else
                    TxtFechaInicial.Text = detalle.FechaAprobacion.ToString(conf.ObtenerFormatoFecha());
                TxtFechaFinal.Text = DateTime.Now.ToString(conf.ObtenerFormatoFecha());

                if (detalle.Producto.Persona.Telefono != null)
                    txttelefono.Text = detalle.Producto.Persona.Telefono.ToString();
                txtDireccion.Text = detalle.Producto.Persona.Direccion.ToString();
                txtNoCredito.Text = detalle.NumeroRadicacion.ToString();
                txtLinea.Text = detalle.Producto.CodLineaCredito.ToString() + "  " + detalle.Producto.Linea;

                if (detalle.FechaDesembolso.ToString(conf.ObtenerFormatoFecha()) == "01/01/0001")
                    TxtFechaDesembolso.Text = "       ";
                else
                    TxtFechaDesembolso.Text = detalle.FechaDesembolso.ToString(conf.ObtenerFormatoFecha());
                txtCalifPromedio.Text = detalle.Producto.CalifPromedio.ToString();
                txtMontoSolicitado.Text = detalle.MontoSolicitado.ToString();
                txtPlazo.Text = detalle.Producto.Plazo.ToString();
                txtSaldoCapital.Text = detalle.Producto.SaldoCapital.ToString();
                txtCuota.Text = detalle.Producto.Cuota.ToString();
                txtperiodicidad.Text = detalle.periodicidad;
                txtMontoAprobado.Text = detalle.Producto.MontoAprobado.ToString();
                txtTasaInteres.Text = detalle.TasaNM.ToString();
                txtCuotasPagas.Text = detalle.Producto.CuotasPagadas.ToString();
                txttipolinea.Text = detalle.Producto.TipoLinea.ToString();
                tipolinea = Convert.ToInt64(txttipolinea.Text);

                // Mostrar listado de movimientos
                ActualizarMovGenearal(lstConsulta);
                Session.Add(serviceEstadoCuenta.GetType().Name + ".consulta", 1);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "L", "Actualizar", ex);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    private void ActualizarMovGenearal(List<DetalleProducto> plstConsulta)
    {
        Configuracion conf = new Configuracion();
        List<Object> lstMov = new List<Object>();
        ucFechaFinal.ToDateTime = Convert.ToDateTime(ucFechaFinal.ToDateTime.ToString(conf.ObtenerFormatoFecha()));

        if (!string.IsNullOrWhiteSpace(TxtFechaDesembolso.Text))
        {
            if (ucFechaInicial.ToDateTime == Convert.ToDateTime(TxtFechaDesembolso.Text) || Convert.ToString(ucFechaInicial.ToDate) == "01/01/0001")
                ucFechaInicial.ToDateTime = Convert.ToDateTime(TxtFechaDesembolso.Text);
        }

        int cont = 0;
        if (PeriodoEsVisible() == true)
        {
            foreach (DetalleProducto dproducto in plstConsulta)
            {
                // Se ajusto para que no traiga datos duplicados
                if (cont == 0)
                {
                    var movProd = from mp in dproducto.ConsultaAvance
                                  orderby mp.NumAvance
                                  select new
                                  {
                                      mp.NumAvance,
                                      Fecha = mp.FechaDesembolsi.ToShortDateString(),
                                      FechaProx = mp.FechaProxPago.ToShortDateString(),
                                      FechaUlti = mp.FechaUltiPago.ToShortDateString(),
                                      mp.ValDesembolso,
                                      mp.Plazo,
                                      mp.CuotasPagadas,
                                      mp.CuotasPendi,
                                      mp.SaldoAvance,
                                      mp.Tasa

                                  };
                    foreach (var node in movProd)
                    {
                        lstMov.Add(node);
                    }
                    cont += 1;
                }
            }
        }
        else
        {
            foreach (DetalleProducto dproducto in plstConsulta)
            {
                if (cont == 0)
                {
                    var movProd = from mp in dproducto.ConsultaAvance
                                  orderby mp.NumAvance
                                  select new
                                  {
                                      mp.NumAvance,
                                      mp.FechaDesembolsi,
                                      mp.ValDesembolso,
                                      mp.Plazo,
                                      mp.CuotasPagadas,
                                      mp.CuotasPendi,
                                      mp.SaldoAvance

                                  };

                    foreach (var node in movProd)
                    {
                        lstMov.Add(node);
                    }
                }
            }
        }
        Int64 radicado = Convert.ToInt64(Request["radicado"]);
        entityProducto.CodRadicacion = radicado.ToString();
        lstAvances = serviceEstadoCuenta.ListarAvances(radicado, (Usuario)Session["usuario"]);
        gvMovGeneral.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
        gvMovGeneral.DataSource = lstAvances;

        if (lstAvances.Count() > 0)
        {
            gvMovGeneral.DataBind();
            gvMovGeneral.Visible = true;
            lblInfo.Visible = false;
            lblTotalReg.Visible = true;
            lblTotalReg.Text = "<br/> Registros encontrados " + lstAvances.Count().ToString();

            //if (cbDetallado.Checked == true && tipolinea == 1)
            //{
            //    this.gvMovGeneral.Columns[1].Visible = false;
            //    this.gvMovGeneral.Columns[2].Visible = true;
            //    this.gvMovGeneral.Columns[3].Visible = false;
            //    this.gvMovGeneral.Columns[4].Visible = false;
            //    this.gvMovGeneral.Columns[5].Visible = true;
            //    this.gvMovGeneral.Columns[6].Visible = true;
            //}

            //if (cbDetallado.Checked && tipolinea == 2)
            //{
            //    this.gvMovGeneral.Columns[1].Visible = true;
            //    this.gvMovGeneral.Columns[2].Visible = true;
            //    this.gvMovGeneral.Columns[3].Visible = true;
            //    this.gvMovGeneral.Columns[4].Visible = true;
            //    this.gvMovGeneral.Columns[17].Visible = false;
            //    this.gvMovGeneral.Columns[18].Visible = false;

            //    txtCuotasPagas.Visible = false;
            //    Lblcuotaspagas.Visible = false;
            //}
            //if (cbDetallado.Checked == false && tipolinea == 1)
            //{
            //    this.gvMovGeneral.Columns[1].Visible = false;
            //    this.gvMovGeneral.Columns[2].Visible = false;
            //    this.gvMovGeneral.Columns[3].Visible = false;
            //    this.gvMovGeneral.Columns[4].Visible = false;
            //    this.gvMovGeneral.Columns[6].Visible = false;
            //}
            //if (cbDetallado.Checked == false && tipolinea == 2)
            //{
            //    this.gvMovGeneral.Columns[1].Visible = false;
            //    this.gvMovGeneral.Columns[2].Visible = false;
            //    this.gvMovGeneral.Columns[3].Visible = false;
            //    this.gvMovGeneral.Columns[4].Visible = false;
            //    this.gvMovGeneral.Columns[6].Visible = false;
            //}

            gvMovGeneral.DataBind();
        }
        else
        {
            gvMovGeneral.Visible = false;
            if (Request.UrlReferrer.Segments[4].ToString() != "MoviGralCredito/")
            {
                lblInfo.Visible = true;
            }
            lblTotalReg.Visible = false;
        }
    }


    public System.Data.DataTable CrearDataTable()
    {
        System.Data.DataTable table = new System.Data.DataTable();

        table.Columns.Add("NumAvance");
        table.Columns.Add("Estado");
        table.Columns.Add("FecDesembolso");
        table.Columns.Add("FecProxPago");
        table.Columns.Add("FecUltiPago");
        table.Columns.Add("ValDesembolso");
        table.Columns.Add("ValorCuotas");
        table.Columns.Add("Plazo");
        table.Columns.Add("CuotasPagadas");
        table.Columns.Add("CuotasPendientes");
        table.Columns.Add("SaldoAvance");
        table.Columns.Add("Tasa");


        foreach (GridViewRow row in gvMovGeneral.Rows)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = " " + row.Cells[0].Text;
            datarw[1] = " " + row.Cells[1].Text;
            datarw[2] = " " + row.Cells[2].Text;
            datarw[3] = " " + row.Cells[3].Text;
            datarw[4] = " " + row.Cells[4].Text;
            datarw[5] = " " + row.Cells[5].Text;
            datarw[6] = " " + row.Cells[6].Text;
            datarw[7] = " " + row.Cells[7].Text;
            datarw[8] = " " + row.Cells[8].Text;
            datarw[9] = " " + row.Cells[9].Text;
            datarw[10] = " " + row.Cells[10].Text;
            datarw[11] = " " + row.Cells[11].Text;

            table.Rows.Add(datarw);

        }
        return table;

    }

    protected void ucImprimir_Click(object sender, ImageClickEventArgs evt)
    {
        Session.Remove("imprimirCtrl");
        Session["imprimirCtrl"] = gvMovGeneral;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", Imprimir.JSCRIPT_PRINT);
    }

    protected void imgBtnVolverHandler(object sender, ImageClickEventArgs e)
    {
        Navegar("~/Page/Asesores/EstadoCuenta/Detalle.aspx");
    }

    protected void btnConsular_Click(object sender, EventArgs e)
    {
        Actualizar();

    }


    protected void btnExportarExcel_Click(object sender, EventArgs e)
    {
        if (gvMovGeneral.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvMovGeneral.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvMovGeneral);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=Movimiento.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        try
        {


            Usuario pUsu = new Usuario();
            pUsu = (Usuario)Session["usuario"];
            ReportParameter[] param = new ReportParameter[25];
            param[0] = new ReportParameter("entidad", " " + pUsu.empresa);
            param[1] = new ReportParameter("nit", " " + pUsu.nitempresa);
            param[2] = new ReportParameter("cod_cliente", " " + txtCodCliente.Text);
            param[3] = new ReportParameter("documento", " " + txtTipoDoc.Text);
            param[4] = new ReportParameter("nombres", " " + txtNombres.Text);
            param[5] = new ReportParameter("direccion", " " + txtDireccion.Text);
            param[6] = new ReportParameter("telefono", " " + txttelefono.Text);
            param[7] = new ReportParameter("linea", " " + txtLinea.Text);
            param[8] = new ReportParameter("no_credito", " " + txtNoCredito.Text);
            param[9] = new ReportParameter("monto", " " + Convert.ToDecimal(txtMontoSolicitado.Text).ToString("###,###,##0"));
            param[10] = new ReportParameter("nocreditos", " " + txtcreditosterminados.Text);
            param[11] = new ReportParameter("saldo_capital", " " + Convert.ToDecimal(txtSaldoCapital.Text).ToString("###,###,##0"));
            param[12] = new ReportParameter("estado_credito", " " + TxtEstado.Text);
            param[13] = new ReportParameter("CuotasPagas", " " + txtCuotasPagas.Text);
            param[14] = new ReportParameter("montoaprobado", " " + Convert.ToDecimal(txtMontoAprobado.Text).ToString("###,###,##0"));
            param[15] = new ReportParameter("tasa", " " + txtTasaInteres.Text);
            param[16] = new ReportParameter("plazo", " " + txtPlazo.Text);
            param[17] = new ReportParameter("cuota", " " + Convert.ToDecimal(txtCuota.Text).ToString("###,###,##0"));
            param[18] = new ReportParameter("calificacion", " " + txtCalifPromedio.Text);
            param[19] = new ReportParameter("periodicidad", " " + txtperiodicidad.Text);
            param[20] = new ReportParameter("fecha_desembolso", " " + TxtFechaDesembolso.Text);
            param[21] = new ReportParameter("fecha_ult_pago", " " + TxtFechaUltimoPago.Text);
            param[22] = new ReportParameter("fecha_prox_pago", " " + TxtFechaCuota.Text);
            param[23] = new ReportParameter("ImagenReport", ImagenReporte());
            param[24] = new ReportParameter("creado", " " + DateTime.Today.ToString("dd/MM/yyyy"));


            Rpview1.LocalReport.EnableExternalImages = true;

            ReportDataSource rds = new ReportDataSource("DataSet1", CrearDataTable());

            Rpview1.LocalReport.SetParameters(param);
            Rpview1.LocalReport.DataSources.Clear();
            Rpview1.LocalReport.DataSources.Add(rds);
            Rpview1.LocalReport.Refresh();
            Rpview1.Visible = true;
            gvMovGeneral.Visible = false;
        }
        catch (Exception)
        {

            VerError("No hay ningun avance");
        }
    }

    protected void cbDetallado_CheckedChanged(object sender, EventArgs e)
    {
        Actualizar();
    }

    protected void gvMovGeneral_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void CreditosTerminados(String pIdObjeto)
    {

    }

    private void MostrarPeriodo(bool bMostrar)
    {

    }
    private void MostrarPeriodoEstadoCuenta(bool bMostrar)
    {


    }
    private bool PeriodoEsVisible()
    {
        return ucFechaFinal.Visible;
    }

}