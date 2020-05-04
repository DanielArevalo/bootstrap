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

public partial class EstadoCuentaCreditoMovimientoGeneral : GlobalWeb
{

    EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
    MovGralCreditoService servicemovgeneral = new MovGralCreditoService();
    Producto entityProducto;
    List<DetalleProducto> lstConsulta = new List<DetalleProducto>();
    Int64 tipolinea = 0;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(servicemovgeneral.CodigoPrograma, "L");

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
                Session["Retorno"] = "0";
                Rpview1.Visible = false;
                MostrarPeriodo(false);
                ucFechaInicial.ToDateTime = Convert.ToDateTime("01/01/0001");
                ucFechaFinal.ToDateTime = DateTime.Today;
                if (Request.UrlReferrer != null)
                    if (Request.UrlReferrer.Segments[4].ToString() == "MoviGralCredito/")
                    {
                        btnConsultar.Visible = true;
                        MostrarPeriodo(true);
                        ucFechaInicial.ToDateTime = Convert.ToDateTime("01/01/0001");
                    }
                if (Request.UrlReferrer.Segments[4].ToString() == "EstadoCuenta/")
                {
                    btnConsultar.Visible = true;
                    //MostrarPeriodo(true);
                    MostrarPeriodoEstadoCuenta(true);
                    ucFechaInicial.ToDateTime = Convert.ToDateTime("01/01/0001");
                    Session["Retorno"] = "1";
                }

                if (Session[MOV_GRAL_CRED_PRODUC] != null)
                {
                    Actualizar();
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
            Configuracion conf = new Configuracion();
            int detallado = 0;
            if (cbDetallado.Checked)
                detallado = 2;
            else
                detallado = 1;

            Rpview1.Visible = false;

            entityProducto = (Producto)(Session[MOV_GRAL_CRED_PRODUC]);
            entityProducto.noconsultaTodo = 1;

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
            if (tipolinea == 2)
            {
                txtCuotasPagas.Visible = false;
                Lblcuotaspagas.Visible = false;
              //  this.gvMovGeneral.Columns[16].Visible = false;
                this.gvMovGeneral.Columns[17].Visible = false;
                this.gvMovGeneral.Columns[18].Visible = false;
            }
          
            // Mostrar listado de movimientos
            ActualizarMovGenearal(lstConsulta);
            CreditosTerminados(txtCodCliente.Text);
            Session.Add(serviceEstadoCuenta.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "L", "Actualizar", ex);
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
                    var movProd = from mp in dproducto.MovimientosProducto
                                  where Convert.ToDateTime(mp.FechaPago.ToString(conf.ObtenerFormatoFecha())) >= Convert.ToDateTime(ucFechaInicial.ToDateTime.ToString(conf.ObtenerFormatoFecha())) && Convert.ToDateTime(mp.FechaPago) <= Convert.ToDateTime(ucFechaFinal.ToDateTime.ToString(conf.ObtenerFormatoFecha()))
                                  orderby mp.consecutivo, mp.FechaPago, mp.FechaCuota, mp.CodOperacion
                                  select new
                                  {
                                      mp.num_comp,
                                      TIPO_COMP = mp.TIPO_COMP,
                                      mp.NumCuota,
                                      FechaPago = mp.FechaPago.ToShortDateString(),
                                      FechaCuota = mp.FechaCuota.ToShortDateString(),
                                      mp.DiasMora,
                                      TipoOperacion = mp.CodOperacion,
                                      Transaccion = mp.TipoOperacion,
                                      mp.TipoMovimiento,
                                      Saldo = mp.Saldo,
                                      Capital = mp.Capital,
                                      IntCte = mp.IntCte,
                                      IntMora = mp.IntMora,
                                      LeyMiPyme = mp.LeyMiPyme,
                                      ivaMiPyme = mp.ivaMiPyme,
                                      Poliza = mp.Poliza,
                                      Otros = mp.Otros,
                                      Prejuridico = mp.Prejuridico,
                                      totalpago = ((mp.TipoOperacion == "Desembolsos") ? 0 : mp.Capital + mp.IntCte + mp.IntMora + mp.LeyMiPyme + mp.ivaMiPyme + mp.Poliza + mp.Otros + mp.Seguros + mp.Prejuridico),
                                      mp.Calificacion,
                                      Seguros = mp.Seguros,
                                      mp.NumeroRadicacion,
                                      idavance = mp.idavance,
                                      plazo_avance = mp.plazo_avance,
                                      tipo_tran =mp.tipo_tran,
                                      desc_tran = mp.desc_tran,
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
                    var movProd = from mp in dproducto.MovimientosProducto
                                  orderby mp.consecutivo, mp.FechaPago, mp.FechaCuota, mp.CodOperacion
                                  select new
                                  {
                                      mp.num_comp,
                                      TIPO_COMP = mp.TIPO_COMP,
                                      mp.NumCuota,
                                      FechaPago = mp.FechaPago.ToShortDateString(),
                                      FechaCuota = mp.FechaCuota.ToShortDateString(),
                                      mp.DiasMora,
                                      TipoOperacion = mp.CodOperacion,
                                      Transaccion = mp.TipoOperacion,
                                      mp.TipoMovimiento,
                                      Saldo = mp.Saldo,
                                      Capital = mp.Capital,
                                      IntCte = mp.IntCte,
                                      IntMora = mp.IntMora,
                                      LeyMiPyme = mp.LeyMiPyme,
                                      ivaMiPyme = mp.ivaMiPyme,
                                      Poliza = mp.Poliza,
                                      Otros = mp.Otros,
                                      Prejuridico = mp.Prejuridico,
                                      totalpago = ((mp.TipoOperacion == "Desembolsos") ? 0 : mp.Capital + mp.IntCte + mp.IntMora + mp.LeyMiPyme + mp.ivaMiPyme + mp.Poliza + mp.Otros + mp.Seguros + mp.Prejuridico),
                                      mp.Calificacion,
                                      Seguros = mp.Seguros,
                                      idavance = mp.idavance,
                                      plazo_avance = mp.plazo_avance,
                                      tipo_tran = mp.tipo_tran,
                                      desc_tran = mp.desc_tran
                                  };

                    foreach (var node in movProd)
                    {
                        lstMov.Add(node);
                    }
                }
            }
        }
        gvMovGeneral.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
        gvMovGeneral.DataSource = lstMov;

        if (lstMov.Count() > 0)
        {
            gvMovGeneral.DataBind();
            gvMovGeneral.Visible = true;
            lblInfo.Visible = false;
            lblTotalReg.Visible = true;
            lblTotalReg.Text = "<br/> Registros encontrados " + lstMov.Count().ToString();

            if (cbDetallado.Checked==true && tipolinea == 1)
            {
                this.gvMovGeneral.Columns[1].Visible = false;
                this.gvMovGeneral.Columns[2].Visible = true;
                this.gvMovGeneral.Columns[3].Visible = false;
                this.gvMovGeneral.Columns[4].Visible = false;
                this.gvMovGeneral.Columns[5].Visible = true;
                this.gvMovGeneral.Columns[6].Visible = true;
            }
          
            if (cbDetallado.Checked && tipolinea == 2)
            { 
                this.gvMovGeneral.Columns[1].Visible = true;
                this.gvMovGeneral.Columns[2].Visible = true;
                this.gvMovGeneral.Columns[3].Visible = true;
                this.gvMovGeneral.Columns[4].Visible = true;
                this.gvMovGeneral.Columns[17].Visible = false;
                this.gvMovGeneral.Columns[18].Visible = false;

                txtCuotasPagas.Visible = false;
                Lblcuotaspagas.Visible = false;
            }
            if (cbDetallado.Checked == false && tipolinea == 1)
            {
                this.gvMovGeneral.Columns[1].Visible = false;
                this.gvMovGeneral.Columns[2].Visible = false;
                this.gvMovGeneral.Columns[3].Visible = false;
                this.gvMovGeneral.Columns[4].Visible = false;
                this.gvMovGeneral.Columns[6].Visible = false;
            }
            if (cbDetallado.Checked == false && tipolinea == 2)
            {
                this.gvMovGeneral.Columns[1].Visible = false;
                this.gvMovGeneral.Columns[2].Visible = false;
                this.gvMovGeneral.Columns[3].Visible = false;
                this.gvMovGeneral.Columns[4].Visible = false;
                this.gvMovGeneral.Columns[6].Visible = false;
            }

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

        table.Columns.Add("NumeroRadicacion");
        table.Columns.Add("FechaCuota");
        table.Columns.Add("FechaPago");
        table.Columns.Add("DiasMora");
        table.Columns.Add("TipoOperacion");
        table.Columns.Add("num_comp");
        table.Columns.Add("TIPO_COMP");
        table.Columns.Add("Transaccion");
        table.Columns.Add("TipoMovimiento");
        table.Columns.Add("Capital");
        table.Columns.Add("IntCte");
        table.Columns.Add("IntMora");
        table.Columns.Add("Seguros");
        table.Columns.Add("LeyMiPyme");
        table.Columns.Add("ivaMiPyme");
        table.Columns.Add("otros");
        table.Columns.Add("Prejuridico");
        table.Columns.Add("totalpago");
        table.Columns.Add("Saldo");
        table.Columns.Add("Calificacion");
        table.Columns.Add("Poliza");
        

        foreach (GridViewRow row in gvMovGeneral.Rows)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = " " + row.Cells[0].Text;
            datarw[1] = " " + row.Cells[2].Text;
            datarw[2] = " " + row.Cells[5].Text;
            datarw[3] = " " + row.Cells[6].Text;
            datarw[4] = " " + row.Cells[7].Text;
            datarw[5] = " " + row.Cells[8].Text;
            datarw[6] = " " + row.Cells[9].Text;
            datarw[7] = " " + row.Cells[10].Text;
            datarw[8] = " " + row.Cells[11].Text.Replace("&#233;", "é");
            datarw[9] = " " + row.Cells[12].Text;
            datarw[10] = " " + row.Cells[13].Text;
            datarw[11] = " " + row.Cells[14].Text;
            datarw[12] = " " + row.Cells[16].Text;
            datarw[13] = " " + row.Cells[17].Text;
            datarw[14] = " " + row.Cells[18].Text;
            datarw[15] = " " + row.Cells[19].Text;
            datarw[16] = " " + row.Cells[20].Text;
            datarw[17] = " " + row.Cells[21].Text;
            datarw[18] = " " + row.Cells[22].Text;
            datarw[19] = " " + row.Cells[23].Text;
            datarw[20] = " " + row.Cells[15].Text;
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
        if (Request.UrlReferrer.Segments[4].ToString() == "MoviGralCredito/")
        {
            lblInfo.Visible = true;
        }
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
        Usuario pUsu = new Usuario();
        pUsu = (Usuario)Session["usuario"];
        ReportParameter[] param = new ReportParameter[26];
        param[0] = new ReportParameter("cod_cliente", " " + txtCodCliente.Text);
        param[1] = new ReportParameter("documento", " " + txtTipoDoc.Text);
        param[2] = new ReportParameter("nombres", " " + txtNombres.Text);
        param[3] = new ReportParameter("nocreditos", " " + txtcreditosterminados.Text);
        param[4] = new ReportParameter("direccion", " " + txtDireccion.Text);
        param[5] = new ReportParameter("telefono", " " + txttelefono.Text);
        param[6] = new ReportParameter("estado_cobro", " " + txtcreditosterminados.Text);
        param[7] = new ReportParameter("no_credito", " " + txtNoCredito.Text);
        param[8] = new ReportParameter("saldo_capital", " " + Convert.ToDecimal(txtSaldoCapital.Text).ToString("###,###,##0"));
        param[9] = new ReportParameter("linea", " " + txtLinea.Text);
        param[10] = new ReportParameter("estado_credito", " " + TxtEstado.Text);
        param[11] = new ReportParameter("fecha_desembolso", " " + TxtFechaDesembolso.Text);
        param[12] = new ReportParameter("fecha_ult_pago", " " + TxtFechaUltimoPago.Text);
        param[13] = new ReportParameter("fecha_prox_pago", " " + TxtFechaCuota.Text);
        param[14] = new ReportParameter("plazo", " " + txtPlazo.Text);
        param[15] = new ReportParameter("cuota", " " + Convert.ToDecimal(txtCuota.Text).ToString("###,###,##0"));
        param[16] = new ReportParameter("calificacion", " " + txtCalifPromedio.Text);
        param[17] = new ReportParameter("periodicidad", " " + txtperiodicidad.Text);
        param[18] = new ReportParameter("monto", " " + Convert.ToDecimal(txtMontoSolicitado.Text).ToString("###,###,##0"));
        param[19] = new ReportParameter("creado", " " + DateTime.Today.ToString("dd/MM/yyyy"));
        param[20] = new ReportParameter("montoaprobado", " " + Convert.ToDecimal(txtMontoAprobado.Text).ToString("###,###,##0"));
        param[21] = new ReportParameter("tasa", " " + txtTasaInteres.Text);
        param[22] = new ReportParameter("CuotasPagas", " " + txtCuotasPagas.Text);
        param[23] = new ReportParameter("entidad", " " + pUsu.empresa);
        param[24] = new ReportParameter("nit", " " + pUsu.nitempresa);
        param[25] = new ReportParameter("ImagenReport", ImagenReporte());
        Rpview1.LocalReport.EnableExternalImages = true;

        ReportDataSource rds = new ReportDataSource("DataSet1", CrearDataTable());

        Rpview1.LocalReport.SetParameters(param);
        Rpview1.LocalReport.DataSources.Clear();
        Rpview1.LocalReport.DataSources.Add(rds);
        Rpview1.LocalReport.Refresh();
        Rpview1.Visible = true;
        gvMovGeneral.Visible = false;
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
        try
        {
            Producto credito = new Producto();
            String pIdObjeto2 = txtCodCliente.Text;
            if (pIdObjeto2 != null)
            {
                credito = serviceEstadoCuenta.ConsultarCreditosTerminados(Convert.ToInt64(pIdObjeto2), (Usuario)Session["usuario"]);
                if (credito.numerocreditos != Int64.MinValue)
                    txtcreditosterminados.Text = credito.numerocreditos.ToString().Trim();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name, "CreditosTerminados", ex);
        }
    }

    private void MostrarPeriodo(bool bMostrar)
    {
        ucFechaInicial.Visible = bMostrar;
        ucFechaFinal.Visible = bMostrar;

        TxtFechaFinal.Visible = !bMostrar;
        TxtFechaInicial.Visible = !bMostrar;
    }
    private void MostrarPeriodoEstadoCuenta(bool bMostrar)
    {
        ucFechaInicial.Visible = bMostrar;
        ucFechaFinal.Visible = bMostrar;
        ucFechaInicial.Enabled = true;
        ucFechaFinal.Enabled = true;
        TxtFechaFinal.Visible = !bMostrar;
        TxtFechaInicial.Visible = !bMostrar;
        TxtFechaFinal.Enabled = true;
        TxtFechaInicial.Enabled = true;

    }
    private bool PeriodoEsVisible()
    {
        return ucFechaFinal.Visible;
    }

}