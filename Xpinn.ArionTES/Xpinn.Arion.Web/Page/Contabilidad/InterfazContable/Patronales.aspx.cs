using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.Util;

partial class Lista : GlobalWeb
{
    private Xpinn.Contabilidad.Services.InterfazNominaService InterfazNominaServicio = new Xpinn.Contabilidad.Services.InterfazNominaService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(InterfazNominaServicio.CodigoPrograma, "L");

            pProceso.Visible = false;
            pFinal.Visible = false;
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarExportar(false);
            txtFecha.Text = DateTime.Now.ToString(gFormatoFecha);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InterfazNominaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarListas();
                CargarValoresConsulta(pConsulta, InterfazNominaServicio.CodigoPrograma);
                if (Session[InterfazNominaServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InterfazNominaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void CargarListas()
    {
        List<Xpinn.Contabilidad.Entities.InterfazNomina> lstConsulta = new List<Xpinn.Contabilidad.Entities.InterfazNomina>();
        lstConsulta = InterfazNominaServicio.ListarPeriodosSaludPension((Usuario)Session["usuario"]);
        ddlPeriodo.DataTextField = "nom_periodo";
        ddlPeriodo.DataValueField = "iden_periodo";
        ddlPeriodo.DataSource = lstConsulta;
        ddlPeriodo.DataBind();
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Actualizar();
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarExportar(true);
        toolBar.MostrarConsultar(false);
        ddlPeriodo.Enabled = false;
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        LimpiarValoresConsulta(pConsulta, InterfazNominaServicio.CodigoPrograma);
        gvLista.Visible = false;
        ddlPeriodo.Enabled = true;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        toolBar.MostrarExportar(false);
        toolBar.MostrarConsultar(true);
        lblTotalRegs.Text = "";
        txtFecha.Text = DateTime.Now.ToString(gFormatoFecha);
        pConsulta.Visible = true;
        pProceso.Visible = false;
        pFinal.Visible = false;
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        ctlMensaje.MostrarMensaje("Desea generar el comprobante de la nomina?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count <= 0)
        {
            VerError("No hay registros para contabilizar");
            return;
        }
        // Validar la lista
        List<Xpinn.Contabilidad.Entities.InterfazNomina> lstConsulta = new List<Xpinn.Contabilidad.Entities.InterfazNomina>();
        foreach (GridViewRow rfila in gvLista.Rows)
        {
            Xpinn.Contabilidad.Entities.InterfazNomina entidad = new Xpinn.Contabilidad.Entities.InterfazNomina();
            entidad.identificacion = rfila.Cells[3].Text;
            entidad.nombre1 = rfila.Cells[4].Text;
            entidad.apellido1 = rfila.Cells[5].Text;
            entidad.iden_concepto = rfila.Cells[6].Text;
            entidad.nombre = rfila.Cells[7].Text;
            entidad.total = ConvertirStringToDecimal(rfila.Cells[8].Text);
            entidad.totalempleador = ConvertirStringToDecimal(rfila.Cells[9].Text);           
            entidad.centrocosto = rfila.Cells[11].Text;
            lstConsulta.Add(entidad);
        }
        // Grabar los datos
        // Determinar código de proceso contable para generar el comprobante
        Int64? rpta = 0;
        if (!pProceso.Visible && pConsulta.Visible)
        {
            rpta = ctlproceso.Inicializar(123, Convert.ToDateTime(txtFecha.Text), (Usuario)Session["Usuario"]);
            if (rpta <= 0)
            {
                VerError("No se encontró parametrización contable por procesos para el tipo de operación 123 = Aportes Patronales");
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
                if (GenerarComprobante(ctlproceso.cod_proceso))
                { 
                    pConsulta.Visible = false;
                    pFinal.Visible = true;                    
                    Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
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
        pFinal.Visible = false;
    }

    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            pConsulta.Visible = true;
            pProceso.Visible = false;
            if (GenerarComprobante(ctlproceso.cod_proceso))
            {
                pConsulta.Visible = false;
                pFinal.Visible = true;
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[InterfazNominaServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
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
            BOexcepcion.Throw(InterfazNominaServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        VerError("");
        try
        {
            string filtro = "";
            if (ddlPeriodo.SelectedItem == null)
            {
                VerError("Debe seleccionar un período");
                return;
            }
            if (ddlPeriodo.SelectedIndex == 0)
            {
                VerError("Debe seleccionar un período");
                return;
            }
            List<Xpinn.Contabilidad.Entities.InterfazNomina> lstConsulta = new List<Xpinn.Contabilidad.Entities.InterfazNomina>();
            lstConsulta = InterfazNominaServicio.ListarSaludPension(Convert.ToInt64(ddlPeriodo.SelectedItem.Value), filtro, (Usuario)Session["usuario"]);

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

            Session.Add(InterfazNominaServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InterfazNominaServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Determinar identificación y código de la persona
            string identificacion = e.Row.Cells[3].Text;
            Label lblCodPersona = (Label)e.Row.FindControl("lblCodPersona");
            if (lblCodPersona != null)
                lblCodPersona.Text = Convert.ToString(InterfazNominaServicio.CodigoDelEmpleado(identificacion, (Usuario)Session["Usuario"]));            
            // Determinar la cuenta contable
            string concepto = e.Row.Cells[6].Text;
            if (concepto == "&nbsp;")
                concepto = "1";
            List<Xpinn.Contabilidad.Entities.InterfazNomina> lstCuentas = new List<Xpinn.Contabilidad.Entities.InterfazNomina>();
            lstCuentas = InterfazNominaServicio.ListarCuentasConcepto(concepto, (Usuario)Session["Usuario"]);
            DropDownListGrid ddlCodCuenta = (DropDownListGrid)e.Row.FindControl("ddlCodCuenta");
            if (ddlCodCuenta != null)
            {
                ddlCodCuenta.DataSource = lstCuentas;
                ddlCodCuenta.DataTextField = "nom_cuenta";
                ddlCodCuenta.DataValueField = "cod_cuenta";
                ddlCodCuenta.DataBind();
                if (lstCuentas.Count >= 1)
                {
                    ListItem selectedListItem = ddlCodCuenta.Items.FindByValue(Convert.ToString(lstCuentas[0].cod_cuenta));
                    if (selectedListItem != null)
                        selectedListItem.Selected = true;
                }
            }
            lstCuentas = InterfazNominaServicio.ListarCuentasContraConcepto(concepto, (Usuario)Session["Usuario"]);
            DropDownListGrid ddlCodCuentaGasto = (DropDownListGrid)e.Row.FindControl("ddlCodCuentaGasto");
            if (ddlCodCuentaGasto != null)
            {
                ddlCodCuentaGasto.DataSource = lstCuentas;
                ddlCodCuentaGasto.DataTextField = "nom_cuenta";
                ddlCodCuentaGasto.DataValueField = "cod_cuenta";
                ddlCodCuentaGasto.DataBind();
                if (lstCuentas.Count >= 1)
                {
                    ListItem selectedListItem = ddlCodCuentaGasto.Items.FindByValue(Convert.ToString(lstCuentas[0].cod_cuenta));
                    if (selectedListItem != null)
                        selectedListItem.Selected = true;
                }
            }
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                GridView gvExportar = gvLista;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvExportar);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=InterfazNomina.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
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

    protected bool GenerarComprobante(Int64? pCodProceso)
    {
        VerError("");        
        // Cargar la gridView en un list
        List<Xpinn.Contabilidad.Entities.InterfazNomina> lstConceptos = new List<Xpinn.Contabilidad.Entities.InterfazNomina>();
        foreach (GridViewRow rfila in gvLista.Rows)
        {
            Xpinn.Contabilidad.Entities.InterfazNomina entidad = new Xpinn.Contabilidad.Entities.InterfazNomina();
            entidad.iden = rfila.Cells[0].Text;
            entidad.identificacion = rfila.Cells[3].Text;
            entidad.nombre1 = rfila.Cells[4].Text;
            entidad.apellido1 = rfila.Cells[5].Text;
            entidad.iden_concepto = rfila.Cells[6].Text;
            entidad.nombre = rfila.Cells[7].Text;
            entidad.total = ConvertirStringToDecimal(rfila.Cells[8].Text);
            entidad.tercero = rfila.Cells[10].Text.Trim() == "&nbsp;" ? "" : rfila.Cells[9].Text.Trim();
            entidad.centrocosto = rfila.Cells[10].Text;
            Label lblCodPersona = (Label)rfila.FindControl("lblCodPersona");
            if (lblCodPersona != null)
                if (lblCodPersona.Text.Trim() != "")
                    entidad.cod_cliente = Convert.ToInt64(lblCodPersona.Text);
            DropDownListGrid ddlCodCuenta = (DropDownListGrid)rfila.FindControl("ddlCodCuenta");
            if (ddlCodCuenta != null)
                if (ddlCodCuenta.SelectedValue != "")
                    entidad.cod_cuenta = ddlCodCuenta.SelectedValue;
            DropDownListGrid ddlCodCuentaGasto = (DropDownListGrid)rfila.FindControl("ddlCodCuentaGasto");
            if (ddlCodCuentaGasto != null)
                if (ddlCodCuentaGasto.SelectedValue != "")
                    entidad.cod_cuenta_gasto = ddlCodCuentaGasto.SelectedValue;
            lstConceptos.Add(entidad);
        }
        // Grabar los datos
        string error = "";
        Int64 cod_ope = 0;
        DateTime fechaAplicacion = Convert.ToDateTime(txtFecha.Text);
        if (!InterfazNominaServicio.GenerarComprobanteSaludPension(Convert.ToInt64(ddlPeriodo.SelectedItem.Value), fechaAplicacion, Convert.ToInt64(pCodProceso), lstConceptos, (Usuario)Session["Usuario"], ref error, ref cod_ope))
        {
            VerError("No se pudo generar el comprobante." + error);
            return false;
        }
        if (error.Trim() != "")
        {
            VerError("Error al generar el comprobante. Error:" + error);
            return false;
        }
        ctlproceso.CargarVariables(cod_ope, 123, ((Usuario)Session["Usuario"]).cod_persona, (Usuario)Session["Usuario"]);
        Site toolBar = (Site)this.Master;        
        toolBar.MostrarGuardar(false);
        toolBar.MostrarExportar(false);
        return true;
    }

}