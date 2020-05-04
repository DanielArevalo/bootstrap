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
    Xpinn.Tesoreria.Services.OperacionServices operacionServicio = new Xpinn.Tesoreria.Services.OperacionServices();
    Xpinn.Tesoreria.Entities.Operacion operacion = new Xpinn.Tesoreria.Entities.Operacion();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(operacionServicio.CodigoProgramaComprobante, "L");
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
            BOexcepcion.Throw(operacionServicio.GetType().Name + "A", "Page_PreInit", ex);
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
            BOexcepcion.Throw(operacionServicio.GetType().Name + "A", "Page_Load", ex);
        }
    }

    protected void CargarDropDown()
    {
        Xpinn.Caja.Services.TipoOperacionService TipOpeService = new Xpinn.Caja.Services.TipoOperacionService();
        Xpinn.Caja.Entities.TipoOperacion TipOpe = new Xpinn.Caja.Entities.TipoOperacion();
        ddlTipoOpe.DataSource = TipOpeService.ListarTipoOpe((Usuario)Session["Usuario"]);
        ddlTipoOpe.DataTextField = "nom_tipo_operacion";
        ddlTipoOpe.DataValueField = "cod_operacion";
        ddlTipoOpe.DataBind();
    }

    protected void Actualizar()
    {
        try
        {
            List<Xpinn.Tesoreria.Entities.Operacion> lstConsulta = new List<Xpinn.Tesoreria.Entities.Operacion>();
            Xpinn.Tesoreria.Entities.Operacion operacion = new Xpinn.Tesoreria.Entities.Operacion();
            if (ddlTipoOpe.SelectedIndex != 0)
                operacion.tipo_ope = Convert.ToInt64(ddlTipoOpe.SelectedValue);
            if (txtCod_Ope.Text != "")
                operacion.cod_ope = Convert.ToInt64(txtCod_Ope.Text);
            Int32 año = Convert.ToInt32(DateTime.Now.Year);
            String diames = "01/01/";
            String fecha = diames + año;
            // Listado de operaciones sin contabilizar, no mostrar tipo de operación 103=Fusión de Giros
            lstConsulta = operacionServicio.ListarOperacion(operacion, " o.tipo_ope Not In (34, 103, 129)  And o.num_comp In (-2) And Trunc(o.fecha_oper) > To_Date(" + "'" + fecha + "', '" + gFormatoFecha + "') ", (Usuario)Session["usuario"]);

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
            BOexcepcion.Throw(operacionServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("../../../General/Global/inicio.aspx");
        Session["operacion"] = null;
    }

    private Xpinn.Caja.Entities.TransaccionCaja ObtenerValores()
    {
        Xpinn.Caja.Entities.TransaccionCaja transaccion = new Xpinn.Caja.Entities.TransaccionCaja();

        transaccion.fecha_cierre = Convert.ToDateTime(Session["FechaArqueo"].ToString());
        transaccion.cod_oficina = long.Parse(Session["Oficina"].ToString());
        return transaccion;
    }

    protected void ObtenerDatos()
    {
        try
        {
            Usuario usuario = new Usuario();
            usuario = (Usuario)Session["Usuario"];
            txtFecha.Text = System.DateTime.Now.ToShortDateString();
            txtOficina.Text = usuario.nombre_oficina;
            txtCod_Ope.Text = Session["operacion"] != null ? Session["operacion"].ToString() : "";

            Session["Oficina"] = usuario.cod_oficina;
            Session["FechaArqueo"] = System.DateTime.Now;


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(operacionServicio.GetType().Name + "A", "ObtenerDatos", ex);
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
        List<Xpinn.Tesoreria.Entities.Operacion> lstConsulta = new List<Xpinn.Tesoreria.Entities.Operacion>();
        int RowIndex = 0;
        foreach (GridViewRow rFila in gvMovimiento.Rows)
        {
            Xpinn.Tesoreria.Entities.Operacion voperacion = new Xpinn.Tesoreria.Entities.Operacion();
            voperacion.cod_ope = Convert.ToInt64(rFila.Cells[0].Text);
            voperacion.tipo_ope = Convert.ToInt64(rFila.Cells[2].Text);
            voperacion.fecha_oper = Convert.ToDateTime(rFila.Cells[1].Text);
            voperacion.cod_cliente = Convert.ToInt64(rFila.Cells[4].Text);
            voperacion.cod_ofi = Convert.ToInt64(rFila.Cells[7].Text);
            //if (rFila.Cells[12].Text.Trim() == "" || rFila.Cells[12].Text.Trim() == "&nbsp;")
            //    voperacion.cod_proceso = null;
            //else
            //    voperacion.cod_proceso = Convert.ToInt64(rFila.Cells[12].Text);
            ctlListarCodigo ctllistar = (ctlListarCodigo)rFila.FindControl("ddlProceso");
            if (!string.IsNullOrWhiteSpace(ctllistar.Codigo))
                voperacion.cod_proceso = Convert.ToInt64(ctllistar.Codigo);
            voperacion.cod_cuenta = Convert.ToString(rFila.Cells[13].Text);
            CheckBox chkseleccionar = (CheckBox)rFila.Cells[10].FindControl("chkseleccionar");
            voperacion.seleccionar = false;
            if (chkseleccionar != null)
            {
                voperacion.seleccionar = chkseleccionar.Checked;
                if (chkseleccionar.Checked == true)
                    lstConsulta.Add(voperacion);
            }
            RowIndex += 1;
        }
        if (RowIndex > 0)
        {
            if (lstConsulta.Count > 0)
            {
                string Error = "";
                lstConsulta = operacionServicio.ContabilizarOperacion(lstConsulta, ref Error, (Usuario)Session["Usuario"]);
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
            BOexcepcion.Throw(operacionServicio.CodigoPrograma, "gvMovimiento_PageIndexChanging", ex);
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
            ctlListarCodigo ctllistar = (ctlListarCodigo)e.Row.FindControl("ddlProceso");
            if (ctllistar != null)
            {
                ctllistar.ValueField = "cod_proceso";
                ctllistar.TextField = "nom_tipo_comp";
                Xpinn.Contabilidad.Services.ProcesoContableService procesoContable = new Xpinn.Contabilidad.Services.ProcesoContableService();
                Xpinn.Contabilidad.Entities.ProcesoContable eproceso = new Xpinn.Contabilidad.Entities.ProcesoContable();
                try
                {
                    Xpinn.Tesoreria.Entities.Operacion edato = (Xpinn.Tesoreria.Entities.Operacion)e.Row.DataItem;
                    if (edato != null)
                    {
                        eproceso.tipo_ope = edato.tipo_ope;
                        List<Xpinn.Contabilidad.Entities.ProcesoContable> lstDatos = procesoContable.ListarProcesoContable(eproceso, (Usuario)Session["Usuario"]);
                        ctllistar.BindearControl(lstDatos);
                        if (edato.cod_proceso != null)
                            ctllistar.SelectedValue(edato.cod_proceso.ToString());
                        else
                            if (lstDatos.Count == 1)
                            ctllistar.SelectedValue(lstDatos[0].cod_proceso.ToString());
                    }
                }
                catch { }
            }
        }
    }
}