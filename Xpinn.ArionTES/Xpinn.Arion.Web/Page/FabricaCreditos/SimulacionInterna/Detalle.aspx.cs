using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Microsoft.Reporting.WebForms;
using Microsoft.CSharp;
using Xpinn.FabricaCreditos.Data;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class Detalle : GlobalWeb
{
    SimulacionService SimulacionServicio = new SimulacionService();
    DatosPlanPagosService datosServicio = new DatosPlanPagosService();
    Configuracion global = new Configuracion();
    string FormatoFecha = " ";

    /// <summary>
    /// Cargar datos de la opción
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[SimulacionServicio.CodigoProgramaSimInterna + ".id"] != null)
                VisualizarOpciones(SimulacionServicio.CodigoProgramaSimInterna, "E");
            else
                VisualizarOpciones(SimulacionServicio.CodigoProgramaSimInterna, "A");
            Site toolBar = (Site)this.Master;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SimulacionServicio.CodigoProgramaSimInterna, "Page_PreInit", ex);
        }
    }

    /// <summary>
    /// Cargar datos de la página, llenar los combos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        FormatoFecha = global.ObtenerValorConfig("FormatoFecha");

        try
        {
            txtFecha.Text = DateTime.Today.ToString(FormatoFecha);
            if (!IsPostBack)
            {
                acordionPlanPagos.Visible = false;
                LlenarComboPeriodicidad(lstPeriodicidad);
                mvLista.ActiveViewIndex = 0;
                btnPlanPagos.Visible = true;
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SimulacionServicio.CodigoProgramaSimInterna, "Page_Load", ex);
        }
    }


    /// <summary>
    /// Llenar listado de líneas de crédito
    /// </summary>
    /// <param name="ddlLinea"></param>
    protected void LlenarComboLineasCredito(DropDownList ddlLinea)
    {
        LineasCreditoService lineaService = new LineasCreditoService();
        LineasCredito linea = new LineasCredito();
        ddlLinea.DataSource = lineaService.ListarLineasCredito(linea, (Usuario)Session["usuario"]);
        ddlLinea.DataTextField = "nombre";
        ddlLinea.DataValueField = "cod_linea_credito";
        ddlLinea.DataBind();
        ddlLinea.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    /// <summary>
    /// Llenar listado de periodicidades de pago
    /// </summary>
    /// <param name="ddlPeriodicidad"></param>
    protected void LlenarComboPeriodicidad(DropDownList ddlPeriodicidad)
    {
        PeriodicidadService periodicService = new PeriodicidadService();
        Periodicidad periodic = new Periodicidad();
        ddlPeriodicidad.DataSource = periodicService.ListarPeriodicidad(periodic, (Usuario)Session["usuario"]);
        ddlPeriodicidad.DataTextField = "descripcion";
        ddlPeriodicidad.DataValueField = "codigo";
        ddlPeriodicidad.DataBind();
        ddlPeriodicidad.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }


    protected void gvPlanPagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvPlanPagos.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SimulacionServicio.CodigoProgramaSimInterna, "gvPlanPagos_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        VerError("");
        btnPlanPagos.Visible = true;
        Simulacion datosApp = new Simulacion();
        List<DatosPlanPagos> lstConsulta = new List<DatosPlanPagos>();
        try
        {
            // Determinar separador de miles
            Configuracion global = new Configuracion();

            // Determinar el monto del crédito
            string texto1 = txtMonto.Text;
            string[] sCadena = new string[2];
            sCadena = texto1.Split(Convert.ToChar(global.ObtenerSeparadorDecimalConfig()));
            string texto = sCadena[0];
            texto = texto.Replace(".", "");
            datosApp.monto = Convert.ToInt32(texto);

            // Determinar condiciones deseadas del crédito
            datosApp.plazo = Convert.ToInt32(txtPlazo.Text);
            if (txttasa.Text != "")
                datosApp.tasa = Convert.ToDecimal(txttasa.Text);
            datosApp.periodic = Convert.ToInt32(lstPeriodicidad.Text);
            datosApp.for_pag = 1;
            datosApp.fecha = DateTime.Today;
            if (txtSeguro.Text != "")
                datosApp.tasaseguro = Convert.ToDecimal(txtSeguro.Text);
            else
                datosApp.tasaseguro = 0;
            datosApp.fecha_primer_pago = ConvertirStringToDate(txtFechaPrimerPago.Text);

            lstConsulta = SimulacionServicio.SimularPlanPagosInterno(datosApp, (Usuario)Session["usuario"]);
            Session["LSTCONSULTA"] = lstConsulta;

            gvPlanPagos.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                acordionPlanPagos.Visible = true;
                gvPlanPagos.Visible = true;
                gvPlanPagos.DataBind();
            }
            else
            {
                acordionPlanPagos.Visible = false;
                gvPlanPagos.Visible = false;
            }

            Session.Add(SimulacionServicio.CodigoProgramaSimInterna + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SimulacionServicio.CodigoProgramaSimInterna, "Actualizar", ex);
        }
    }


    protected void gvPlanPagos_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Llenar la tabla del plan de pagos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPlanPagos_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void ExportarExcelGrilla(GridView gvGrilla, string Archivo)
    {
        try
        {
            if (gvGrilla.Rows.Count > 0)
            {
                string style = "";
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                style = "<link href=\"../../Styles/Styles.css\" rel=\"stylesheet\" type=\"text/css\" />";
                gvGrilla.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvGrilla);
                pagina.RenderControl(htw);
                Response.Clear();
                style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + Archivo + ".xls");
                Response.Charset = "UTF-8";
                Response.Write(sb.ToString());
                Response.End();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex0)
        {
            VerError(ex0.Message);
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (Session["LSTCONSULTA"] != null)
        {
            GridView gvGrillaExcel = new GridView();
            gvGrillaExcel = gvPlanPagos;
            List<DatosPlanPagos> lstConsulta = new List<DatosPlanPagos>();
            lstConsulta = (List<DatosPlanPagos>)Session["LSTCONSULTA"];
            gvGrillaExcel.AllowPaging = false;
            gvPlanPagos.DataSource = lstConsulta;
            gvGrillaExcel.DataBind();
            ExportarExcelGrilla(gvGrillaExcel, "Simulacion");
        }
    }

    protected void lstLinea_SelectedIndexChanged(object sender, EventArgs e)
    {
        Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        LineasCredito eLinea = new LineasCredito();
        // eLinea = LineaCreditoServicio.ConsultarLineasCredito(lstLinea.SelectedValue.ToString(), (Usuario)Session["Usuario"]);
        //ddlTipoLiquidacion.SelectedValue = eLinea.tipo_liquidacion.ToString(); 
    }

    protected void txtSeguro_Unload(object sender, EventArgs e)
    {
        txtSeguro.Attributes.Add("onkeypress", "return ValidaDecimal(event);");
    }

}