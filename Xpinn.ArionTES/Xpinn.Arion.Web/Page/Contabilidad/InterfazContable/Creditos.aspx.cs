using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Creditos : GlobalWeb
{
    private Xpinn.Contabilidad.Services.InterfazNominaService InterfazNominaServicio = new Xpinn.Contabilidad.Services.InterfazNominaService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(InterfazNominaServicio.CodigoProgramaCredito, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            pProceso.Visible = false;
            txtFecha.Text = DateTime.Now.ToString(gFormatoFecha);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InterfazNominaServicio.CodigoProgramaCredito, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarListas();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InterfazNominaServicio.CodigoProgramaCredito, "Page_Load", ex);
        }
    }

    protected void CargarListas()
    {
        List<Xpinn.Contabilidad.Entities.InterfazNomina> lstConsulta = new List<Xpinn.Contabilidad.Entities.InterfazNomina>();
        lstConsulta = InterfazNominaServicio.ListarPeriodos("B", (Usuario)Session["usuario"]);
        ddlPeriodo.DataTextField = "nom_periodo";
        ddlPeriodo.DataValueField = "iden_periodo";
        ddlPeriodo.DataSource = lstConsulta;
        ddlPeriodo.DataBind();
        if (lstConsulta.Count >= 1)
        {
            ddlPeriodo.SelectedIndex = 1;
            ddlPeriodo.Enabled = false;
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarConsultar(false);
        ddlPeriodo.Enabled = false;
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, InterfazNominaServicio.CodigoProgramaCredito);
        gvLista.Visible = false;
        ddlPeriodo.Enabled = true;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        toolBar.MostrarConsultar(true);
        lblTotalRegs.Text = "";
        txtFecha.Text = DateTime.Now.ToString(gFormatoFecha);
        CargarListas();
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        ctlMensaje.MostrarMensaje("Desea realizar la aplicación de los crèditos?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count <= 0)
        {
            VerError("No hay registros para contabilizar");
            return;
        }
        // Cargar la lista
        List<Xpinn.Contabilidad.Entities.InterfazNomina> lstConsulta = new List<Xpinn.Contabilidad.Entities.InterfazNomina>();
        foreach (GridViewRow rfila in gvLista.Rows)
        {
            DropDownListGrid ddlNumeroRadicacion = (DropDownListGrid)rfila.FindControl("ddlNumeroRadicacion");
            if (ddlNumeroRadicacion == null)
            {
                VerError("Debe seleccionar el crédito para el empleado " + rfila.Cells[3].Text + " " + rfila.Cells[4].Text + " " + rfila.Cells[5].Text);
                return;
            }
        }
        // Determinar código de proceso contable para generar el comprobante
        Int64? rpta = 0;
        if (!pProceso.Visible && pConsulta.Visible)
        {
            rpta = ctlproceso.Inicializar(141, Convert.ToDateTime(txtFecha.Text), (Usuario)Session["Usuario"]);
            if (rpta == 0)
            {
                VerError("No se encontró parametrización contable por procesos para el tipo de operación 141 = Aplicación Recaudos Masivos");
                return;
            }
            else if (rpta > 1)
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarCancelar(false);
                toolBar.MostrarConsultar(false);
                pConsulta.Visible = false;
                pProceso.Visible = true;
            }
            else
            {
                if (AplicarPagos())
                {                    
                    Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                }
            }
        }
    }
    

    protected bool AplicarPagos()
    {
        // Cargar la gridView en un list
        List<Xpinn.Contabilidad.Entities.InterfazNomina> lstConsulta = new List<Xpinn.Contabilidad.Entities.InterfazNomina>();
        foreach (GridViewRow rfila in gvLista.Rows)
        {
            Xpinn.Contabilidad.Entities.InterfazNomina entidad = new Xpinn.Contabilidad.Entities.InterfazNomina();
            entidad.iden = rfila.Cells[0].Text;
            entidad.iden_prestamo = rfila.Cells[1].Text;            
            entidad.identificacion = rfila.Cells[2].Text;
            entidad.nombre1 = rfila.Cells[3].Text;
            entidad.apellido1 = rfila.Cells[4].Text;
            entidad.iden_concepto = rfila.Cells[5].Text;
            entidad.nombre = rfila.Cells[6].Text;
            entidad.total = ConvertirStringToDecimal(rfila.Cells[7].Text);            
            entidad.centrocosto = rfila.Cells[8].Text;
            Label lblCodPersona = (Label)rfila.FindControl("lblCodPersona");
            if (lblCodPersona != null)
                if (lblCodPersona.Text.Trim() != "")
                    entidad.cod_cliente = Convert.ToInt64(lblCodPersona.Text);
            DropDownListGrid ddlNumeroRadicacion = (DropDownListGrid)rfila.FindControl("ddlNumeroRadicacion");
            if (ddlNumeroRadicacion != null)
            {
                if (ddlNumeroRadicacion.SelectedValue.Trim() != "")
                    entidad.numero_radicacion = Convert.ToInt64(ddlNumeroRadicacion.SelectedValue);
            }
            if (entidad.numero_radicacion == null || entidad.numero_radicacion == 0)
            {
                VerError("Debe seleccionar el crédito para el empleado " + entidad.identificacion + " " + entidad.nombre1 + " " + entidad.apellido1);
                return false;
            }
            lstConsulta.Add(entidad);
        }
        // Grabar los datos
        string error = "";
        Int64 codOpe = 0;
        DateTime fechaAplicacion = Convert.ToDateTime(txtFecha.Text);
        if (!InterfazNominaServicio.AplicarCreditos(Convert.ToInt64(ddlPeriodo.SelectedItem.Value), fechaAplicacion, lstConsulta, (Usuario)Session["Usuario"], ref error, ref codOpe))
        {
            VerError("No se pudieron aplicar los pagos de créditos." + error);
            return false;
        }
        if (error.Trim() != "")
        {
            VerError("Error al aplicar los créditos. Error:" + error);
            return false;
        }
        Usuario usu = (Usuario)Session["Usuario"];
        ctlproceso.CargarVariables(codOpe, 141, usu.cod_persona, usu);
        return true;
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
            BOexcepcion.Throw(InterfazNominaServicio.CodigoProgramaCredito, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        VerError("");
        try
        {
            if (ddlPeriodo.SelectedItem == null)
            {
                VerError("Debe seleccionar un período");
                return;
            }
            if (ddlPeriodo.SelectedItem.Value == null)
            {
                VerError("Debe seleccionar un período");
                return;
            }

            if (ddlPeriodo.SelectedItem.Value == "")
            {
                VerError("Debe seleccionar un período");
                return;
            }
            List<Xpinn.Contabilidad.Entities.InterfazNomina> lstConsulta = new List<Xpinn.Contabilidad.Entities.InterfazNomina>();
            lstConsulta = InterfazNominaServicio.ListarCreditos(Convert.ToInt64(ddlPeriodo.SelectedItem.Value), (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

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
            decimal total = 0;
            foreach (Xpinn.Contabilidad.Entities.InterfazNomina efila in lstConsulta)
            {
                total += efila.total;
            }
            txtTotal.Text = total.ToString("n2");
            Session["DTPRESTAMOSEMPLEADOS"] = lstConsulta;
            Session.Add(InterfazNominaServicio.CodigoProgramaCredito + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InterfazNominaServicio.CodigoProgramaCredito, "Actualizar", ex);
        }
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Determinar identificación y código de la persona
            string identificacion = e.Row.Cells[2].Text;
            Label lblCodPersona = (Label)e.Row.FindControl("lblCodPersona");
            if (lblCodPersona != null)
                lblCodPersona.Text = Convert.ToString(InterfazNominaServicio.CodigoDelEmpleado(identificacion, (Usuario)Session["Usuario"]));
            // Determinar número de radicación del crédito
            string concepto = e.Row.Cells[5].Text;
            string cod_linea = "";
            cod_linea += (concepto == "47" ? ((cod_linea.Trim() != "" ? ",": "") + " '8'") : "");
            cod_linea += (concepto == "48" ? ((cod_linea.Trim() != "" ? ",": "") + "'12'") : "");
            List<Xpinn.Contabilidad.Entities.InterfazNomina> lstCreditos = new List<Xpinn.Contabilidad.Entities.InterfazNomina>();
            lstCreditos = InterfazNominaServicio.ListarCreditosDelEmpleado(identificacion, cod_linea, (Usuario)Session["Usuario"]);
            DropDownListGrid ddlNumeroRadicacion = (DropDownListGrid)e.Row.FindControl("ddlNumeroRadicacion");
            if (ddlNumeroRadicacion != null)
            {
                ddlNumeroRadicacion.DataSource = lstCreditos;
                ddlNumeroRadicacion.DataTextField = "descripcion";
                ddlNumeroRadicacion.DataValueField = "numero_radicacion";
                ddlNumeroRadicacion.DataBind();
                if (lstCreditos.Count == 1)
                {
                    try
                    {
                        ListItem selectedListItem = ddlNumeroRadicacion.Items.FindByValue(Convert.ToString(lstCreditos[0].numero_radicacion));
                        if (selectedListItem != null)
                            selectedListItem.Selected = true;
                    }
                    catch { }
                }
                else
                {
                    decimal monto = ConvertirStringToDecimal(e.Row.Cells[9].Text);
                    decimal valorcuota = ConvertirStringToDecimal(e.Row.Cells[7].Text);                    
                    foreach (Xpinn.Contabilidad.Entities.InterfazNomina eFila in lstCreditos)
                    {                        
                        if (Math.Round(eFila.valor_cuota) == Math.Round(valorcuota))
                        {
                            try
                            {                                
                                ListItem selectedItem  = ddlNumeroRadicacion.SelectedItem;
                                selectedItem.Selected = false;
                                //if (ddlNumeroRadicacion.SelectedItem == null)
                                //{
                                    ListItem selectedListItem = ddlNumeroRadicacion.Items.FindByValue(Convert.ToString(eFila.numero_radicacion));
                                    if (selectedListItem != null)
                                        selectedListItem.Selected = true;
                                //}
                            }
                            catch { VerError("Rad:" + eFila.numero_radicacion); }
                        }
                    }
                }
            }
        }
    }

    protected void btnCancelarProceso_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarCancelar(true);
        toolBar.MostrarConsultar(true);
        pConsulta.Visible = true;
        pProceso.Visible = false;
    }

    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            pConsulta.Visible = true;
            pProceso.Visible = false;
            AplicarPagos();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }        
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTPRESTAMOSEMPLEADOS"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                GridView gvExportar = CopiarGridViewParaExportar(gvLista, "DTPRESTAMOSEMPLEADOS");
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvExportar);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=PrestamosEmpleados.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
            }

        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

}