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
            VisualizarOpciones(InterfazNominaServicio.CodigoProgramaProvision, "L");

            pProceso.Visible = false;
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
            BOexcepcion.Throw(InterfazNominaServicio.CodigoProgramaProvision, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarListas();
                CargarValoresConsulta(pConsulta, InterfazNominaServicio.CodigoProgramaProvision);
                if (Session[InterfazNominaServicio.CodigoProgramaProvision + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InterfazNominaServicio.CodigoProgramaProvision, "Page_Load", ex);
        }
    }

    protected void CargarListas()
    {
        List<Xpinn.Contabilidad.Entities.InterfazNomina> lstConsulta = new List<Xpinn.Contabilidad.Entities.InterfazNomina>();
        lstConsulta = InterfazNominaServicio.ListarPeriodosProvision((Usuario)Session["usuario"]);
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
        LimpiarValoresConsulta(pConsulta, InterfazNominaServicio.CodigoProgramaProvision);
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
        CargarListas();
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
            Label lblCodPersona = (Label)rfila.FindControl("lblCodPersona");
            if (lblCodPersona != null)
                if (lblCodPersona.Text.Trim() != "")
                    entidad.cod_cliente = Convert.ToInt64(lblCodPersona.Text);
            DropDownListGrid ddlCodCuenta = (DropDownListGrid)rfila.FindControl("ddlCodCuenta");
            if (ddlCodCuenta != null)
            {
                if (ddlCodCuenta.SelectedValue.Trim() != "")
                    entidad.cod_cuenta = Convert.ToString(ddlCodCuenta.SelectedValue);
            }
            if (entidad.cod_cuenta == null || entidad.cod_cuenta.Trim() == "")
            {
                VerError("Debe seleccionar la cuenta contable para el empleado " + entidad.identificacion + " " + entidad.nombre1 + " " + entidad.apellido1);
                return;
            }  
            DropDownListGrid ddlCodCuentaCre = (DropDownListGrid)rfila.FindControl("ddlCodCuentaCre");
            if (ddlCodCuentaCre != null)
            {
                if (ddlCodCuentaCre.SelectedValue.Trim() != "")
                    entidad.cod_cuenta_gasto = Convert.ToString(ddlCodCuentaCre.SelectedValue);
            }
            if (entidad.cod_cuenta_gasto == null || entidad.cod_cuenta_gasto.Trim() == "")
            {
                VerError("Debe seleccionar la cuenta contable contra para el empleado " + entidad.identificacion + " " + entidad.nombre1 + " " + entidad.apellido1);
                return;
            }
            entidad.total = -ConvertirStringToDecimal(rfila.Cells[8].Text);
            lstConsulta.Add(entidad);
        }
        // Grabar los datos
        // Determinar código de proceso contable para generar el comprobante
        Int64? rpta = 0;
        if (!pProceso.Visible && pConsulta.Visible)
        {
            rpta = ctlproceso.Inicializar(122, Convert.ToDateTime(txtFecha.Text), (Usuario)Session["Usuario"]);
            if (rpta <= 0)
            {
                VerError("No se encontró parametrización contable por procesos para el tipo de operación 122 = Aplicación Recaudos Masivos");
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
    }

    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            pConsulta.Visible = true;
            pProceso.Visible = false;
            if (GenerarComprobante(ctlproceso.cod_proceso))
                pConsulta.Visible = false;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[InterfazNominaServicio.CodigoProgramaProvision + ".id"] = id;
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
            BOexcepcion.Throw(InterfazNominaServicio.CodigoProgramaProvision, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        VerError("");
        try
        {
            string filtro = " And a.iden_periodo = " + ddlPeriodo.SelectedValue.ToString();
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
            if (ddlPeriodo.Text.Trim() == "")
            {
                VerError("Debe seleccionar un período");
                return;
            }
            List<Xpinn.Contabilidad.Entities.InterfazNomina> lstConsulta = new List<Xpinn.Contabilidad.Entities.InterfazNomina>();
            DateTime pFecha = ConvertirStringToDate(ddlPeriodo.SelectedItem.Text);
            int pAño, pMes;
            pAño = pFecha.Year;
            pMes = pFecha.Month;
            lstConsulta = InterfazNominaServicio.ListarNominaProvision(pAño, pMes, filtro, (Usuario)Session["usuario"]);

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

            Session.Add(InterfazNominaServicio.CodigoProgramaProvision + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InterfazNominaServicio.CodigoProgramaProvision, "Actualizar", ex);
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
            // Determinar la cuenta contable
            string concepto = e.Row.Cells[5].Text;
            if (concepto == "&nbsp;")
                concepto = "1";
            List<Xpinn.Contabilidad.Entities.InterfazNomina> lstCuentas = new List<Xpinn.Contabilidad.Entities.InterfazNomina>();
            lstCuentas = InterfazNominaServicio.ListarCuentasConcepto(concepto, (Usuario)Session["Usuario"]);
            foreach (Xpinn.Contabilidad.Entities.InterfazNomina eRegistro in lstCuentas)
            {
                DropDownListGrid ddlCodCuenta;
                if (eRegistro.tipomov.Trim() == "1")
                    ddlCodCuenta = (DropDownListGrid)e.Row.FindControl("ddlCodCuenta");
                else
                    ddlCodCuenta = (DropDownListGrid)e.Row.FindControl("ddlCodCuentaCre");
                if (ddlCodCuenta != null)
                {
                    ddlCodCuenta.DataSource = lstCuentas;
                    ddlCodCuenta.DataTextField = "nom_cuenta";
                    ddlCodCuenta.DataValueField = "cod_cuenta";
                    ddlCodCuenta.DataBind();
                    ListItem selectedListItem = ddlCodCuenta.Items.FindByValue(Convert.ToString(eRegistro.cod_cuenta));
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
        List<Xpinn.Contabilidad.Entities.InterfazNomina> lstConsulta = new List<Xpinn.Contabilidad.Entities.InterfazNomina>();
        foreach (GridViewRow rfila in gvLista.Rows)
        {
            // Registrar la cuenta contable débito
            Xpinn.Contabilidad.Entities.InterfazNomina entidad = new Xpinn.Contabilidad.Entities.InterfazNomina();
            entidad.iden = rfila.Cells[0].Text;
            entidad.iden_prestamo = rfila.Cells[1].Text;
            entidad.identificacion = rfila.Cells[2].Text;
            entidad.nombre1 = rfila.Cells[3].Text;
            entidad.apellido1 = rfila.Cells[4].Text;
            entidad.iden_concepto = rfila.Cells[5].Text;
            entidad.nombre = rfila.Cells[6].Text;
            entidad.total = ConvertirStringToDecimal(rfila.Cells[7].Text);            
            entidad.tercero = rfila.Cells[8].Text.Trim() == "&nbsp;" ? "" : rfila.Cells[8].Text.Trim();
            entidad.centrocosto = rfila.Cells[9].Text;
            Label lblCodPersona = (Label)rfila.FindControl("lblCodPersona");
            if (lblCodPersona != null)
                if (lblCodPersona.Text.Trim() != "")
                    entidad.cod_cliente = Convert.ToInt64(lblCodPersona.Text); 
            DropDownListGrid ddlCodCuenta = (DropDownListGrid)rfila.FindControl("ddlCodCuenta");
            if (ddlCodCuenta != null)
            {
                if (ddlCodCuenta.SelectedValue.Trim() != "")
                    entidad.cod_cuenta = Convert.ToString(ddlCodCuenta.SelectedValue);
            }
            if (entidad.cod_cuenta == null || entidad.cod_cuenta == "")
            {
                VerError("Debe seleccionar la cuenta contable para el empleado " + entidad.identificacion + " " + entidad.nombre1 + " " + entidad.apellido1);
                return false;
            }
            lstConsulta.Add(entidad);
            // Registrar la cuenta contable crédito. Se genera en una nueva entidad.
            Xpinn.Contabilidad.Entities.InterfazNomina entidadcre = new Xpinn.Contabilidad.Entities.InterfazNomina();
            entidadcre.iden = rfila.Cells[0].Text;
            entidadcre.iden_prestamo = rfila.Cells[1].Text;
            entidadcre.identificacion = rfila.Cells[2].Text;
            entidadcre.nombre1 = rfila.Cells[3].Text;
            entidadcre.apellido1 = rfila.Cells[4].Text;
            entidadcre.iden_concepto = rfila.Cells[5].Text;
            entidadcre.nombre = rfila.Cells[6].Text;
            entidadcre.total = ConvertirStringToDecimal(rfila.Cells[7].Text);
            entidadcre.tercero = rfila.Cells[8].Text.Trim() == "&nbsp;" ? "" : rfila.Cells[8].Text.Trim();
            entidadcre.centrocosto = rfila.Cells[9].Text;
            if (lblCodPersona != null)
                if (lblCodPersona.Text.Trim() != "")
                    entidadcre.cod_cliente = Convert.ToInt64(lblCodPersona.Text); 
            DropDownListGrid ddlCodCuentaCre = (DropDownListGrid)rfila.FindControl("ddlCodCuentaCre");
            if (ddlCodCuentaCre != null)
            {
                if (ddlCodCuentaCre.SelectedValue.Trim() != "")
                    entidadcre.cod_cuenta = Convert.ToString(ddlCodCuentaCre.SelectedValue);
            }
            if (entidadcre.cod_cuenta == null || entidadcre.cod_cuenta.Trim() == "")
            {
                VerError("Debe seleccionar la cuenta contable contra para el empleado " + entidadcre.identificacion + " " + entidadcre.nombre1 + " " + entidadcre.apellido1);
                return false;
            }
            entidadcre.total = -ConvertirStringToDecimal(rfila.Cells[7].Text);  
            lstConsulta.Add(entidadcre);
        }
        // Grabar los datos
        string error = "";
        Int64 codOpe = 0;
        DateTime fechaAplicacion = Convert.ToDateTime(txtFecha.Text);
        if (!InterfazNominaServicio.GenerarComprobante(Convert.ToInt64(ddlPeriodo.SelectedItem.Value), fechaAplicacion, Convert.ToInt64(pCodProceso), lstConsulta, (Usuario)Session["Usuario"], ref error, ref codOpe))
        {
            VerError("No se pudo generar el comprobante de la nomina." + error);
            return false;
        }
        if (error.Trim() != "")
        {
            VerError("Error al generar el comprobante de la nomina. Error:" + error);
            return false;
        }
        Usuario usu = (Usuario)Session["Usuario"];
        ctlproceso.CargarVariables(codOpe, 122, usu.cod_persona, usu);
        return true;
    }

}