using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;
using Xpinn.Confecoop.Services;
using Xpinn.Confecoop.Entities;
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;

partial class Lista : GlobalWeb
{
    RiesgoLiquidezServices _riesgoLiquidezServices = new RiesgoLiquidezServices();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_riesgoLiquidezServices.CodigoProgramaRiesgoLiquidez, "L");

            Site toolBar = (Site)Master;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_riesgoLiquidezServices.CodigoProgramaRiesgoLiquidez, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarCombos();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_riesgoLiquidezServices.CodigoProgramaRiesgoLiquidez, "Page_Load", ex);
        }
    }

    /// <summary>
    /// Método para llenar los DDLs requeridos para las consultas
    /// </summary>
    protected void LlenarCombos()
    {
        LlenarListasDesplegables(TipoLista.ClasificacionCreditos, ddlClasificacionCartera);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (string.IsNullOrWhiteSpace(txtFechaCorte.Text))
        {
            VerError("Debes seleccionar una fecha de corte!.");
        }
        else
        {
            Actualizar();
        }
    }

    void Actualizar()
    {
        try
        {
            TipoProyeccionRiesgoLiquidez tipoProyeccion = ddlTipoReporte.SelectedValue.ToEnum<TipoProyeccionRiesgoLiquidez>();
            RiesgoLiquidez riesgo = new RiesgoLiquidez { fecha_corte = Convert.ToDateTime(txtFechaCorte.Text) };
            riesgo.clasificacion_cartera = ddlClasificacionCartera.SelectedValue.ToEnum<ClasificacionCredito>();

            List<RiesgoLiquidez> lstProyeccion = _riesgoLiquidezServices.ListarProyeccionRiesgoLiquidez(riesgo, tipoProyeccion, Usuario);

            if (lstProyeccion.Count > 0)
            {
                panelReporte.Visible = true;

                DataTable table = lstProyeccion.ToDataTable();

                Tuple<string,string> configuracion = ConfigurarReporte(tipoProyeccion);

                // Limpiar DataSource de Reporte
                ReportViewerPlan.Reset();
                ReportViewerPlan.LocalReport.DataSources.Clear();

                ReportDataSource rds1 = new ReportDataSource(configuracion.Item1, table);
                ReportViewerPlan.LocalReport.DataSources.Add(rds1);

                ReportViewerPlan.LocalReport.ReportPath = configuracion.Item2;
                ReportViewerPlan.LocalReport.Refresh();
            }
            else
            {
                panelReporte.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_riesgoLiquidezServices.CodigoProgramaRiesgoLiquidez, "Actualizar", ex);
        }

    }

    Tuple<string,string> ConfigurarReporte(TipoProyeccionRiesgoLiquidez tipoProyeccion)
    {
        string dataSetName = string.Empty;
        string reportPath = string.Empty;

        switch (tipoProyeccion)
        {
            case TipoProyeccionRiesgoLiquidez.Disponible:
                dataSetName = "ProyeccionRiesgoLiquidezDataSet";
                reportPath = @"Page\Confecoop\ProyeccionRiesgoLiquidez\ReportProyeccion.rdlc";
                break;
            case TipoProyeccionRiesgoLiquidez.AhorroPermanente:
                dataSetName = "ProyeccionRiesgoLiquidezDataSet";
                reportPath = @"Page\Confecoop\ProyeccionRiesgoLiquidez\ReportProyeccion.rdlc";
                break;
            case TipoProyeccionRiesgoLiquidez.Cartera:
                dataSetName = "ProyeccionCarteraRiesgoLiquidez";
                reportPath = @"Page\Confecoop\ProyeccionRiesgoLiquidez\ReportProyeccionCartera.rdlc";
                break;
            case TipoProyeccionRiesgoLiquidez.Aporte:
                dataSetName = "ProyeccionAporteRiesgoLiquidez";
                reportPath = @"Page\Confecoop\ProyeccionRiesgoLiquidez\ReportProyeccionAporte.rdlc";
                break;
            default:
                throw new NotSupportedException("Tipo de reporte no soportado");
        }
        
        return Tuple.Create(dataSetName, reportPath);
    }

    protected void ddlTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTipoReporte.SelectedValue.ToEnum<TipoProyeccionRiesgoLiquidez>() == TipoProyeccionRiesgoLiquidez.Cartera)
        {
            pnlClasificacion.Visible = true;
        }
        else
        {
            pnlClasificacion.Visible = false;
        }
    }
}