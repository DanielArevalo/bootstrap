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
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Services;
using Xpinn.Util;
using System.Globalization;

public partial class EstadoCuentaCreditoMovimientoGeneral : GlobalWeb
{
    private Xpinn.Aportes.Services.AporteServices AporteServicio = new Xpinn.Aportes.Services.AporteServices();

    Aporte entityProducto;
    List<MovimientoAporte> lstConsulta = new List<MovimientoAporte>();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AporteServicio.CodigoProgramaMov, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoConsultar += btnConsular_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ImprimirGrilla();

            if (!IsPostBack)
            {
                Session["EstadoCuenta"] = "0";
                Rpview1.Visible = false;
                MostrarPeriodo(true);
                ucFechaInicial.ToDateTime = Convert.ToDateTime("01/01/0001");
                ucFechaFinal.ToDateTime = DateTime.Today;
                if (Request.UrlReferrer != null)
                {
                    if (Request.UrlReferrer.Segments[4].ToString() == "Movimiento/")
                    {
                        Site toolBar = (Site)this.Master;
                        toolBar.MostrarConsultar(true);
                        ucFechaInicial.ToDateTime = Convert.ToDateTime("01/01/0001");
                    }
                }

                if (Request.UrlReferrer.Segments[4].ToString() == "EstadoCuenta/")
                {
                    Site toolBar = (Site)this.Master;
                    MostrarPeriodoEstadoCuenta(true);
                    ucFechaInicial.ToDateTime = Convert.ToDateTime("01/01/0001");
                    Session["EstadoCuenta"] = "1";
                }

                if (Session[AporteServicio.CodigoProgramaMov + ".id"] != null)
                {
                    idObjeto = Session[AporteServicio.CodigoProgramaMov.ToString() + ".id"].ToString();
                    Actualizar(idObjeto);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.GetType().Name + "D", "Page_Load", ex);
        }
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["EstadoCuenta"] == "1")
            Navegar("~/Page/Asesores/EstadoCuenta/Detalle.aspx");
        else
            Navegar("~/Page/Aportes/Movimiento/Lista.aspx");

    }
    protected void gvMovGeneral_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvMovGeneral.PageIndex = e.NewPageIndex;
            Actualizar(idObjeto);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
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
    private void Actualizar(string pidObjeto)
    {

        try
        {
            Configuracion conf = new Configuracion();
            Rpview1.Visible = false;
            Aporte datosaporte = new Aporte();
            entityProducto = (Aporte)(Session["MOV_GRAL_APO_PRODUC"]);

            // Traer la informaciòn del producto junto con listado de movimientos
            datosaporte = AporteServicio.ConsultarAporte(Convert.ToInt64(pidObjeto), (Usuario)Session["usuario"]);
            if (!string.IsNullOrEmpty(datosaporte.nombre.ToString()))
                txtNombres.Text = Convert.ToString(datosaporte.nombre);
            if (!string.IsNullOrEmpty(datosaporte.identificacion.ToString()))
                txtNumDoc.Text = Convert.ToString(datosaporte.identificacion);
            if (!string.IsNullOrEmpty(datosaporte.estado_Linea.ToString()))
                TxtEstado.Text = Convert.ToString(datosaporte.estado_Linea);
            if (!string.IsNullOrEmpty(datosaporte.cod_persona.ToString()))
                txtCodCliente.Text = Convert.ToString(datosaporte.cod_persona);
            if (!string.IsNullOrEmpty(datosaporte.numero_aporte.ToString()))
                txtNoAporte.Text = Convert.ToString(datosaporte.numero_aporte);
            if (!string.IsNullOrEmpty(datosaporte.Saldo.ToString()))
                txtSaldo.Text = datosaporte.Saldo.ToString("c");
            if (!string.IsNullOrEmpty(datosaporte.nom_linea_aporte.ToString()))
                txtLinea.Text = Convert.ToString(datosaporte.nom_linea_aporte);
            if (!string.IsNullOrEmpty(datosaporte.cuota.ToString()))
                txtcuota.Text = datosaporte.cuota.ToString("n");
            // Mostrando fecha de apertura
            if (datosaporte.fecha_apertura.ToString(conf.ObtenerFormatoFecha()) == "01/01/0001")
                TxtFechaApertura.Text = "       ";
            else
                TxtFechaApertura.Text = datosaporte.fecha_apertura.ToString(conf.ObtenerFormatoFecha());
            // Mostrando fecha de último pago
            if (datosaporte.fecha_ultimo_pago.ToString(conf.ObtenerFormatoFecha()) == "01/01/0001")
                TxtFechaUltimoPago.Text = "       ";
            else
                TxtFechaUltimoPago.Text = datosaporte.fecha_ultimo_pago.ToString(conf.ObtenerFormatoFecha());
            // Mostrar fecha de próximo pago
            if (datosaporte.fecha_proximo_pago.ToString(conf.ObtenerFormatoFecha()) == "01/01/0001")
                TxtFechaProxPago.Text = "       ";
            else
                TxtFechaProxPago.Text = datosaporte.fecha_proximo_pago.ToString(conf.ObtenerFormatoFecha());
            // Mostrar otros datos
            if (!string.IsNullOrEmpty(datosaporte.direccion.ToString()))
                txtDireccion.Text = Convert.ToString(datosaporte.direccion);
            if (!string.IsNullOrEmpty(datosaporte.telefono.ToString()))
                txttelefono.Text = Convert.ToString(datosaporte.telefono);
            if (!string.IsNullOrEmpty(datosaporte.nom_periodicidad.ToString()))
                txtperiodicidad.Text = Convert.ToString(datosaporte.nom_periodicidad);

            // Consultar los datos dela porte
            DateTime? fechaInicial = null, fechaFinal = null;
            if (TxtFechaInicial.Text != "")
                fechaInicial = DateTime.ParseExact(TxtFechaInicial.Text, gFormatoFecha, null);
            if (TxtFechaFinal.Text != "")
                fechaFinal = DateTime.ParseExact(TxtFechaFinal.Text, gFormatoFecha, null);
            lstConsulta = AporteServicio.ListarMovAporte(Convert.ToInt64(txtNoAporte.Text), fechaInicial, fechaFinal, (Usuario)Session["usuario"]);

            //Mostrar listado de movimientos
            ActualizarMovGenearal(lstConsulta);
            Session.Add(AporteServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            //BOexcepcion.Throw(AporteServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private void ActualizarHistori(string pidObjeto)
    {

        try
        {
            Configuracion conf = new Configuracion();
            Rpview1.Visible = false;
            Aporte datosaporte = new Aporte();
            entityProducto = (Aporte)(Session["MOV_GRAL_APO_PRODUC"]);

            // Traer la informaciòn del producto junto con listado de movimientos
            datosaporte = AporteServicio.ConsultarAporte(Convert.ToInt64(pidObjeto), (Usuario)Session["usuario"]);
            if (!string.IsNullOrEmpty(datosaporte.nombre.ToString()))
                txtNombres.Text = Convert.ToString(datosaporte.nombre);
            if (!string.IsNullOrEmpty(datosaporte.identificacion.ToString()))
                txtNumDoc.Text = Convert.ToString(datosaporte.identificacion);
            if (!string.IsNullOrEmpty(datosaporte.estado_Linea.ToString()))
                TxtEstado.Text = Convert.ToString(datosaporte.estado_Linea);
            if (!string.IsNullOrEmpty(datosaporte.cod_persona.ToString()))
                txtCodCliente.Text = Convert.ToString(datosaporte.cod_persona);
            if (!string.IsNullOrEmpty(datosaporte.numero_aporte.ToString()))
                txtNoAporte.Text = Convert.ToString(datosaporte.numero_aporte);
            if (!string.IsNullOrEmpty(datosaporte.Saldo.ToString()))
                txtSaldo.Text = datosaporte.Saldo.ToString("c");
            if (!string.IsNullOrEmpty(datosaporte.nom_linea_aporte.ToString()))
                txtLinea.Text = Convert.ToString(datosaporte.nom_linea_aporte);
            if (!string.IsNullOrEmpty(datosaporte.cuota.ToString()))
                txtcuota.Text = datosaporte.cuota.ToString("n");
            // Mostrando fecha de apertura
            if (datosaporte.fecha_apertura.ToString(conf.ObtenerFormatoFecha()) == "01/01/0001")
                TxtFechaApertura.Text = "       ";
            else
                TxtFechaApertura.Text = datosaporte.fecha_apertura.ToString(conf.ObtenerFormatoFecha());
            // Mostrando fecha de último pago
            if (datosaporte.fecha_ultimo_pago.ToString(conf.ObtenerFormatoFecha()) == "01/01/0001")
                TxtFechaUltimoPago.Text = "       ";
            else
                TxtFechaUltimoPago.Text = datosaporte.fecha_ultimo_pago.ToString(conf.ObtenerFormatoFecha());
            // Mostrar fecha de próximo pago
            if (datosaporte.fecha_proximo_pago.ToString(conf.ObtenerFormatoFecha()) == "01/01/0001")
                TxtFechaProxPago.Text = "       ";
            else
                TxtFechaProxPago.Text = datosaporte.fecha_proximo_pago.ToString(conf.ObtenerFormatoFecha());
            // Mostrar otros datos
            if (!string.IsNullOrEmpty(datosaporte.direccion.ToString()))
                txtDireccion.Text = Convert.ToString(datosaporte.direccion);
            if (!string.IsNullOrEmpty(datosaporte.telefono.ToString()))
                txttelefono.Text = Convert.ToString(datosaporte.telefono);
            if (!string.IsNullOrEmpty(datosaporte.nom_periodicidad.ToString()))
                txtperiodicidad.Text = Convert.ToString(datosaporte.nom_periodicidad);

            // Consultar los datos dela porte
            DateTime? fechaInicial = null, fechaFinal = null;
            if (TxtFechaInicial.Text != "")
                fechaInicial = DateTime.ParseExact(TxtFechaInicial.Text, gFormatoFecha, null);
            if (TxtFechaFinal.Text != "")
                fechaFinal = DateTime.ParseExact(TxtFechaFinal.Text, gFormatoFecha, null);
            lstConsulta = AporteServicio.ListarMovAporte(Convert.ToInt64(txtNoAporte.Text), fechaInicial, fechaFinal, (Usuario)Session["usuario"]);

            //Mostrar listado de movimientos
            ActualizarMovGenearalHis(lstConsulta);
            Session.Add(AporteServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            //BOexcepcion.Throw(AporteServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }


    private void ActualizarMovGenearal(List<MovimientoAporte> plstConsulta)
    {
        Configuracion conf = new Configuracion();
        List<Object> lstMov = new List<Object>();
        // Determinar el período a consultar
        ucFechaFinal.ToDateTime = Convert.ToDateTime(ucFechaFinal.ToDateTime.ToString(conf.ObtenerFormatoFecha()));
        if (ucFechaInicial.ToDateTime == Convert.ToDateTime(TxtFechaApertura.Text) || Convert.ToString(ucFechaInicial.ToDate) == "01/01/0001")
            ucFechaInicial.ToDateTime = Convert.ToDateTime(TxtFechaApertura.Text);

        // Mostrar los movimientos dependiendo si es detallado o no
        if (PeriodoEsVisible() == true)
        {
            var movProd = from mp in plstConsulta
                          where Convert.ToDateTime(mp.FechaPago.ToString(conf.ObtenerFormatoFecha())) >= Convert.ToDateTime(ucFechaInicial.ToDateTime.ToString(conf.ObtenerFormatoFecha())) && Convert.ToDateTime(mp.FechaPago) <= Convert.ToDateTime(ucFechaFinal.ToDateTime.ToString(conf.ObtenerFormatoFecha()))
                          orderby mp.FechaPago, mp.FechaCuota, mp.CodOperacion, mp.TipoOperacion, mp.TipoMovimiento
                          select new
                          {
                              mp.num_comp,
                              tipo_comp = mp.tipo_comp,
                              nom_tipo_comp = mp.nom_tipo_comp,
                              FechaPago = mp.FechaPago.ToShortDateString(),
                              FechaCuota = mp.FechaCuota.ToShortDateString(),
                              TipoOperacion = mp.CodOperacion,
                              Transaccion = mp.TipoOperacion,
                              mp.TipoMovimiento,
                              Saldo = mp.Saldo,
                              Capital = mp.Capital,
                              totalpago = ((mp.TipoOperacion == "Pagos Caja") ? 0 : mp.Capital),
                              mp.numero_aporte,
                          };
            foreach (var node in movProd)
            {
                lstMov.Add(node);
            }
        }
        else
        {
            var movProd = from mp in plstConsulta
                          orderby mp.FechaPago, mp.FechaCuota, mp.CodOperacion, mp.TipoOperacion, mp.TipoMovimiento
                          select new
                          {
                              mp.num_comp,
                              tipo_comp = mp.tipo_comp,
                              nom_tipo_comp = mp.nom_tipo_comp,
                              FechaPago = mp.FechaPago.ToShortDateString(),
                              FechaCuota = mp.FechaCuota.ToShortDateString(),
                              TipoOperacion = mp.CodOperacion,
                              Transaccion = mp.TipoOperacion,
                              mp.TipoMovimiento,
                              Saldo = mp.Saldo,
                              Capital = mp.Capital,
                              totalpago = ((mp.TipoOperacion == "Pagos Caja") ? 0 : mp.Capital),
                              mp.numero_aporte,
                          };

            foreach (var node in movProd)
            {
                lstMov.Add(node);
            }
        }

        // LLenando la grilla con los movimientos
        gvMovGeneral.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
        gvMovGeneral.DataSource = lstMov;

        // Mostrar número de registros
        if (lstMov.Count() > 0)
        {
            gvMovGeneral.DataBind();
            gvMovGeneral.Visible = true;

            lblInfo.Visible = false;
            lblTotalReg.Visible = true;
            lblTotalReg.Text = "<br/> Registros encontrados " + lstMov.Count().ToString();
            this.gvMovGeneral.Columns[1].Visible = false;
            gvMovGeneral.DataBind();
            ValidarPermisosGrilla(gvMovGeneral);
        }
        else
        {
            gvMovGeneral.Visible = false;
            if (Request.UrlReferrer.Segments[4].ToString() != "Movimiento/")
            {
                lblInfo.Visible = true;
            }
            lblTotalReg.Visible = false;
        }

    }

    private void ActualizarMovGenearalHis(List<MovimientoAporte> plstConsulta)
    {
        Configuracion conf = new Configuracion();
        List<Object> lstMov = new List<Object>();
        // Determinar el período a consultar
        ucFechaFinal.ToDateTime = Convert.ToDateTime(ucFechaFinal.ToDateTime.ToString(conf.ObtenerFormatoFecha()));
        if (ucFechaInicial.ToDateTime == Convert.ToDateTime(TxtFechaApertura.Text) || Convert.ToString(ucFechaInicial.ToDate) == "01/01/0001")
            ucFechaInicial.ToDateTime = Convert.ToDateTime("01/01/0001");

        // Mostrar los movimientos dependiendo si es detallado o no
        if (PeriodoEsVisible() == true)
        {
            var movProd = from mp in plstConsulta
                          where Convert.ToDateTime(mp.FechaPago.ToString(conf.ObtenerFormatoFecha())) >= Convert.ToDateTime(ucFechaInicial.ToDateTime.ToString(conf.ObtenerFormatoFecha())) && Convert.ToDateTime(mp.FechaPago) <= Convert.ToDateTime(ucFechaFinal.ToDateTime.ToString(conf.ObtenerFormatoFecha()))
                          orderby mp.FechaPago, mp.FechaCuota, mp.CodOperacion, mp.TipoOperacion, mp.TipoMovimiento
                          select new
                          {
                              mp.num_comp,
                              tipo_comp = mp.tipo_comp,
                              nom_tipo_comp = mp.nom_tipo_comp,
                              FechaPago = mp.FechaPago.ToShortDateString(),
                              FechaCuota = mp.FechaCuota.ToShortDateString(),
                              TipoOperacion = mp.CodOperacion,
                              Transaccion = mp.TipoOperacion,
                              mp.TipoMovimiento,
                              Saldo = mp.Saldo,
                              Capital = mp.Capital,
                              totalpago = ((mp.TipoOperacion == "Pagos Caja") ? 0 : mp.Capital),
                              mp.numero_aporte,
                          };
            foreach (var node in movProd)
            {
                lstMov.Add(node);
            }
        }
        else
        {
            var movProd = from mp in plstConsulta
                          orderby mp.FechaPago, mp.FechaCuota, mp.CodOperacion, mp.TipoOperacion, mp.TipoMovimiento
                          select new
                          {
                              mp.num_comp,
                              tipo_comp = mp.tipo_comp,
                              nom_tipo_comp = mp.nom_tipo_comp,
                              FechaPago = mp.FechaPago.ToShortDateString(),
                              FechaCuota = mp.FechaCuota.ToShortDateString(),
                              TipoOperacion = mp.CodOperacion,
                              Transaccion = mp.TipoOperacion,
                              mp.TipoMovimiento,
                              Saldo = mp.Saldo,
                              Capital = mp.Capital,
                              totalpago = ((mp.TipoOperacion == "Pagos Caja") ? 0 : mp.Capital),
                              mp.numero_aporte,
                          };

            foreach (var node in movProd)
            {
                lstMov.Add(node);
            }
        }

        // LLenando la grilla con los movimientos
        gvMovGeneral.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
        gvMovGeneral.DataSource = lstMov;

        // Mostrar número de registros
        if (lstMov.Count() > 0)
        {
            gvMovGeneral.DataBind();
            gvMovGeneral.Visible = true;

            lblInfo.Visible = false;
            lblTotalReg.Visible = true;
            lblTotalReg.Text = "<br/> Registros encontrados " + lstMov.Count().ToString();
            this.gvMovGeneral.Columns[1].Visible = false;
            gvMovGeneral.DataBind();
            ValidarPermisosGrilla(gvMovGeneral);
        }
        else
        {
            gvMovGeneral.Visible = false;
            if (Request.UrlReferrer.Segments[4].ToString() != "Movimiento/")
            {
                lblInfo.Visible = true;
            }
            lblTotalReg.Visible = false;
        }

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
        VerError("");
        if (txtNoAporte.Enabled == true && txtNoAporte.Text != "")
        {
            Actualizar(txtNoAporte.Text);
            idObjeto = txtNoAporte.Text;
        }
        else
            Actualizar(idObjeto);
        txtNoAporte.Enabled = false;
        btnBusquedaMovimiento.Enabled = false;
        if (Request.UrlReferrer.Segments[4].ToString() == "MoviGralCredito/")
        {
            lblInfo.Visible = true;
        }
    }


    protected void btnExportarExcel_Click(object sender, EventArgs e)
    {
        if (gvMovGeneral.Rows.Count > 0)
        {
            gvMovGeneral.Visible = true;
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



    protected void cbDetallado_CheckedChanged(object sender, EventArgs e)
    {
        Actualizar(idObjeto);
    }

    protected void gvMovGeneral_SelectedIndexChanged(object sender, EventArgs e)
    {

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

    /// <summary>
    ///  Determina si el período es visible
    /// </summary>
    /// <returns></returns>
    private bool PeriodoEsVisible()
    {
        return ucFechaFinal.Enabled;
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {

        ReportParameter[] param = new ReportParameter[18];
        Usuario pUsu = (Usuario)Session["usuario"];
        string cRutaDeImagen;
        cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";

        param[0] = new ReportParameter("cod_cliente", " " + txtCodCliente.Text);
        param[1] = new ReportParameter("documento", " " + this.txtNumDoc.Text);
        param[2] = new ReportParameter("nombres", " " + txtNombres.Text);
        param[3] = new ReportParameter("direccion", " " + txtDireccion.Text);
        param[4] = new ReportParameter("telefono", " " + txttelefono.Text);
        param[5] = new ReportParameter("linea", " " + txtLinea.Text);
        param[6] = new ReportParameter("periodicidad", " " + this.txtperiodicidad.Text);
        param[7] = new ReportParameter("no_aporte", " " + this.txtNoAporte.Text);
        param[8] = new ReportParameter("saldo_aportes", " " + txtSaldo.Text);
        param[9] = new ReportParameter("Cuota", " " + this.txtcuota.Text);
        param[10] = new ReportParameter("estado_aporte", " " + this.TxtEstado.Text);
        param[11] = new ReportParameter("fecha_apertura", " " + TxtFechaApertura.Text);
        param[12] = new ReportParameter("fecha_ult_pago", " " + TxtFechaUltimoPago.Text);
        param[13] = new ReportParameter("fecha_prox_pago", " " + TxtFechaProxPago.Text);
        param[14] = new ReportParameter("creado", " " + DateTime.Today.ToString("dd/MM/yyyy"));
        param[15] = new ReportParameter("entidad", " " + pUsu.empresa);
        param[16] = new ReportParameter("nit", " " + pUsu.nitempresa);
        param[17] = new ReportParameter("ImagenReport", cRutaDeImagen);

        ReportDataSource rds = new ReportDataSource("DataSet1", CrearDataTable());



        Rpview1.LocalReport.EnableExternalImages = true;
        Rpview1.LocalReport.SetParameters(param);
        Rpview1.LocalReport.DataSources.Clear();
        Rpview1.LocalReport.DataSources.Add(rds);
        Rpview1.LocalReport.Refresh();
        Rpview1.Visible = true;
        gvMovGeneral.Visible = false;
    }

    public System.Data.DataTable CrearDataTable()
    {
        System.Data.DataTable table = new System.Data.DataTable();

        table.Columns.Add("NumeroAporte");
        table.Columns.Add("FechaCuota");
        table.Columns.Add("FechaPago");
        table.Columns.Add("Transaccion");
        table.Columns.Add("TipoMovimiento");
        table.Columns.Add("Valor");
        table.Columns.Add("Saldo");
        table.Columns.Add("totalpago");

        foreach (GridViewRow row in gvMovGeneral.Rows)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = " " + row.Cells[0].Text;
            datarw[1] = " " + row.Cells[1].Text;
            datarw[2] = " " + row.Cells[2].Text;
            datarw[3] = " " + row.Cells[3].Text;
            datarw[4] = " " + row.Cells[8].Text.Replace("&#233;", "é");
            datarw[5] = " " + row.Cells[9].Text;
            datarw[6] = " " + row.Cells[10].Text;
            datarw[7] = " " + row.Cells[9].Text;

            table.Rows.Add(datarw);

        }
        return table;
    }


    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarPanel(pConsulta);
        txtNoAporte.Enabled = true;
        btnBusquedaMovimiento.Enabled = true;
        txtNoAporte.Focus();
        gvMovGeneral.DataSource = null;
        gvMovGeneral.DataBind();
        lblTotalReg.Text = "<br/> Registros encontrados " + gvMovGeneral.Rows.Count;
    }

    protected void btnBusquedaMovimiento_Click(object sender, EventArgs e)
    {
        ctlBusquedaMovi.Motrar(true, "txtNumDoc", "txtNombres", "txtNoAporte");
    }

    protected void btnMovimi_Click(object sender, EventArgs e)
    {

        VerError("");
        if (txtNoAporte.Enabled == true && txtNoAporte.Text != "")
        {
            ActualizarHistori(txtNoAporte.Text);
            idObjeto = txtNoAporte.Text;
        }
        else
            ActualizarHistori(idObjeto);
        txtNoAporte.Enabled = false;
        btnBusquedaMovimiento.Enabled = false;
        if (Request.UrlReferrer.Segments[4].ToString() == "MoviGralCredito/")
        {
            lblInfo.Visible = true;
        }


    }
}