using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.Common;
using System.Text;
using System.IO;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    Xpinn.Tesoreria.Services.InventariosServices InventariosServicio = new Xpinn.Tesoreria.Services.InventariosServices();
    Xpinn.Tesoreria.Entities.ivmovimiento operacion = new Xpinn.Tesoreria.Entities.ivmovimiento();
    private decimal? _total = 0;
    private decimal? _total_impuesto = 0;
    private int _tipo_ope = 149;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(InventariosServicio.CodigoProgramaComprobante, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoExportar += btnExportarExcel_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarExportar(false);
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventariosServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDropDown();
                mvComprobante.ActiveViewIndex = 0;
                ObtenerDatos();
                Actualizar();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventariosServicio.GetType().Name + "A", "Page_Load", ex);
        }
    }

    protected void CargarDropDown()
    {
        Xpinn.Tesoreria.Entities.ivalmacen _almacen = new Xpinn.Tesoreria.Entities.ivalmacen();
        ddlAlmacen.DataSource = InventariosServicio.ListarAlmacen(_almacen, (Usuario)Session["Usuario"]);
        ddlAlmacen.DataTextField = "almacenname";
        ddlAlmacen.DataValueField = "almacenid";
        ddlAlmacen.DataBind();
    }

    protected void Actualizar()
    {
        _total = 0;
        _total_impuesto = 0;
        try
        {
            List<Xpinn.Tesoreria.Entities.ivmovimiento> lstConsulta = new List<Xpinn.Tesoreria.Entities.ivmovimiento>();
            Xpinn.Tesoreria.Entities.ivmovimiento movimiento = new Xpinn.Tesoreria.Entities.ivmovimiento();
            if (ddlAlmacen.SelectedIndex != 0)
                movimiento.id_almacen = Convert.ToInt32(ddlAlmacen.SelectedValue);
            Int32 año = Convert.ToInt32(DateTime.Now.Year);
            String diames = "01/01/";
            String fecha = diames + año;
            // Listado de operaciones sin contabilizar
            lstConsulta = InventariosServicio.ListarMovimiento(movimiento, " Trunc(o.fecha_movimiento) = " + "'" + fecha + "'", (Usuario)Session["usuario"]);

            ViewState.Add("lstConsulta", lstConsulta);

            Site toolBar = (Site)this.Master;
            toolBar.MostrarExportar(false);
            toolBar.MostrarGuardar(false);
            if (lstConsulta.Count > 0)
            {
                toolBar.MostrarExportar(true);
                toolBar.MostrarGuardar(true);
                gvMovimiento.DataSource = lstConsulta;
                gvMovimiento.Visible = true;
                gvMovimiento.DataBind();
                lblTotalReg.Visible = true;
                lblTotalReg.Text = "<br/>Registros encontrados " + lstConsulta.Count;
                lblInfo.Visible = false;
                Session["Movimientos"] = lstConsulta;
            }
            else
            {
                lblTotalReg.Visible = false;
                lblInfo.Visible = true;
                gvMovimiento.Visible = false;
                Session["Movimientos"] = null;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventariosServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("Lista.aspx");
        Session["operacion"] = null;
    }

    protected void ObtenerDatos()
    {
        try
        {
            Usuario usuario = new Usuario();
            usuario = (Usuario)Session["Usuario"];
            txtFecha.Text = System.DateTime.Now.ToShortDateString();
            Session["FechaArqueo"] = System.DateTime.Now;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventariosServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Actualizar();
    }

    /// <summary>
    /// Método para generar el comprobante de caja
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        mpeNuevo.Show();
    }

    protected void btnParar_Click(object sender, EventArgs e)
    {
        mpeNuevo.Hide();
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        List<Xpinn.Tesoreria.Entities.ivmovimiento> lstConsulta = new List<Xpinn.Tesoreria.Entities.ivmovimiento>();
        int RowIndex = 0;
        foreach (GridViewRow rFila in gvMovimiento.Rows)
        {
            Xpinn.Tesoreria.Entities.ivmovimiento voperacion = new Xpinn.Tesoreria.Entities.ivmovimiento();
            voperacion.id_movimiento = Convert.ToInt64(rFila.Cells[0].Text);
            voperacion.fecha_movimiento = Convert.ToDateTime(rFila.Cells[1].Text);
            voperacion.tipo_movimiento = Convert.ToInt32(rFila.Cells[2].Text);
            voperacion.nom_tipo_movimiento = Convert.ToString(rFila.Cells[3].Text);
            voperacion.id_almacen = Convert.ToInt32(rFila.Cells[4].Text);
            voperacion.almacenname = Convert.ToString(rFila.Cells[5].Text);
            voperacion.cod_persona = ConvertirStringToIntN(rFila.Cells[6].Text);
            voperacion.id_persona = Convert.ToString(rFila.Cells[7].Text);
            voperacion.nombre_persona = Convert.ToString(rFila.Cells[8].Text);
            voperacion.total = ConvertirStringToDecimal(rFila.Cells[9].Text);
            voperacion.total_impuesto = ConvertirStringToDecimal(rFila.Cells[10].Text);
            voperacion.numero_factura = rFila.Cells[17].Text;
            ctlListarCodigo ctllistar = (ctlListarCodigo)rFila.FindControl("ddlProceso");
            if (!string.IsNullOrWhiteSpace(ctllistar.Codigo))
                voperacion.cod_proceso = Convert.ToInt64(ctllistar.Codigo);
            CheckBox chkseleccionar = (CheckBox)rFila.Cells[10].FindControl("chkseleccionar");
            voperacion.seleccionar = false;
            if (chkseleccionar != null)
            {
                voperacion.seleccionar = chkseleccionar.Checked;
                if (chkseleccionar.Checked == true)
                {
                    GridView gvRetencion = (GridView)rFila.FindControl("gvRetencion");
                    if (gvRetencion != null)
                    {
                        voperacion.LstRetencion = new List<Xpinn.Tesoreria.Entities.ivordenconcepto>();
                        foreach (GridViewRow item in gvRetencion.Rows)
                        {
                            Xpinn.Tesoreria.Entities.ivordenconcepto entidad = new Xpinn.Tesoreria.Entities.ivordenconcepto();
                            entidad.nombre = item.Cells[0].Text;
                            entidad.porcentaje_calculo = Convert.ToDouble(ConvertirStringToDecimal(item.Cells[1].Text));
                            entidad.valor = Convert.ToDouble(ConvertirStringToDecimal(item.Cells[2].Text));
                            if (item.Cells[3].Text != "&nbsp;")
                                entidad.cod_cuenta = item.Cells[3].Text;
                            voperacion.LstRetencion.Add(entidad);
                        }
                    }
                    voperacion.LstDatosPago = new List<Xpinn.Tesoreria.Entities.ivdatospago>();
                    voperacion.LstDatosPago = InventariosServicio.ListarDatosPago(Convert.ToInt64(voperacion.cod_proceso), voperacion.id_movimiento, (Usuario)Session["Usuario"]);
                    if (voperacion.LstDatosPago == null)
                        voperacion.LstDatosPago = new List<Xpinn.Tesoreria.Entities.ivdatospago>();
                    if (voperacion.LstDatosPago.Count() <= 0)
                    {
                        Xpinn.Tesoreria.Entities.ivdatospago entidad = new Xpinn.Tesoreria.Entities.ivdatospago();
                        entidad.id_venta_realizada = voperacion.id_movimiento;
                        entidad.id_forma_pago = 0;                        
                        entidad.valor = Convert.ToDouble(voperacion.total);
                        voperacion.LstDatosPago.Add(entidad);
                    }
                    lstConsulta.Add(voperacion);
                }
            }
            RowIndex += 1;
        }
        if (RowIndex > 0)
        {
            if (lstConsulta.Count > 0)
            {
                string Error = "";
                lstConsulta = InventariosServicio.ContabilizarOperacion(lstConsulta, ref Error, (Usuario)Session["Usuario"]);
                if (Error.Trim() == "")
                {
                    gvOperacion.DataSource = lstConsulta;
                    gvOperacion.DataBind();
                    mvComprobante.ActiveViewIndex = 1;
                }
                else
                {
                    VerError(Error);
                }
            }
        }
        else
        {
            VerError("No hay datos de transacciones para generar el comprobante");
        }
    }

    protected void gvMovimiento_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvMovimiento.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventariosServicio.CodigoPrograma, "gvMovimiento_PageIndexChanging", ex);
        }
    }

    protected void btnExportarExcel_Click(object sender, EventArgs e)
    {
        if (gvMovimiento.Rows.Count > 0 && Session["Movimientos"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            HtmlForm form = new HtmlForm();
            GridView gvExportar = new GridView();
            gvExportar.DataSource = ViewState["lstConsulta"];
            gvExportar.DataBind();

            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);

            form.Controls.Add(gvExportar);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }

    protected void gvOperacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }

    protected void chkseleccionarHeader_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkseleccionarHeader = (CheckBox)sender;
        if (chkseleccionarHeader != null)
        {
            foreach (GridViewRow rFila in gvMovimiento.Rows)
            {
                CheckBox chkseleccionar = (CheckBox)rFila.FindControl("chkseleccionar");
                chkseleccionar.Checked = chkseleccionarHeader.Checked;
            }
        }
    }

    protected void gvMovimiento_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Xpinn.Tesoreria.Entities.ivmovimiento edato = (Xpinn.Tesoreria.Entities.ivmovimiento)e.Row.DataItem;
            if (edato != null)
            {
                if (edato.cod_persona == null)
                {
                    e.Row.ForeColor = System.Drawing.Color.Red;
                    e.Row.Enabled = false;
                }
                ctlListarCodigo ctllistar = (ctlListarCodigo)e.Row.FindControl("ddlProceso");
                if (ctllistar != null)
                {
                    ctllistar.ValueField = "cod_proceso";
                    ctllistar.TextField = "nom_tipo_comp";
                    Xpinn.Contabilidad.Services.ProcesoContableService procesoContable = new Xpinn.Contabilidad.Services.ProcesoContableService();
                    Xpinn.Contabilidad.Entities.ProcesoContable eproceso = new Xpinn.Contabilidad.Entities.ProcesoContable();
                    try
                    {   
                        if (edato.tipo_movimiento == 20)
                            eproceso.tipo_ope = _tipo_ope;
                        else
                            eproceso.tipo_ope = _tipo_ope;
                        List<Xpinn.Contabilidad.Entities.ProcesoContable> lstDatos = procesoContable.ListarProcesoContable(eproceso, (Usuario)Session["Usuario"]);
                        ctllistar.BindearControl(lstDatos);
                        if (edato.cod_proceso != null)
                            ctllistar.SelectedValue(edato.cod_proceso.ToString());
                        else
                            if (lstDatos.Count == 1)
                                ctllistar.SelectedValue(lstDatos[0].cod_proceso.ToString());                    
                    }
                    catch { }
                }
                if (edato.tipo_movimiento == 20)
                { 
                    Xpinn.Tesoreria.Services.InventariosServices inventarioservicio = new Xpinn.Tesoreria.Services.InventariosServices();
                    List<Xpinn.Tesoreria.Entities.ivordenconcepto> lstRetencion = new List<Xpinn.Tesoreria.Entities.ivordenconcepto>();
                    lstRetencion = inventarioservicio.ListarOrdenConceptos(edato.id_movimiento, Convert.ToDouble(edato.total), (Usuario)Session["Usuario"]);
                    GridView gvRetencion = (GridView)e.Row.FindControl("gvRetencion");
                    if (gvRetencion != null)
                    {
                        gvRetencion.DataSource = lstRetencion;
                        gvRetencion.DataBind();
                    }
                }
            }
            decimal? _item_total = ConvertirStringToDecimal(e.Row.Cells[9].Text.ToString());
            _total += _item_total;
            decimal? _item_total_impuesto = ConvertirStringToDecimal(e.Row.Cells[10].Text.ToString());
            _total_impuesto += _item_total_impuesto;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[8].Text = "TOTAL";
            e.Row.Cells[9].Text = Convert.ToDecimal(_total).ToString("N2");
            e.Row.Cells[10].Text = Convert.ToDecimal(_total_impuesto).ToString("N2");
        }
    }

    protected void gvOperacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
        Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = gvOperacion.SelectedRow.Cells[2].Text;
        String id = gvOperacion.SelectedRow.Cells[3].Text;
        Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = id;
        Session[ComprobanteServicio.CodigoPrograma + ".detalle"] = id;
        Response.Redirect("../../../Contabilidad/Comprobante/Nuevo.aspx", false);
    }


}