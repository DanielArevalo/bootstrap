using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Scoring.Services;
using Xpinn.Scoring.Entities;
using Xpinn.FabricaCreditos.Services;
using Microsoft.Reporting.WebForms;
using System.Linq;

public partial class Lista : GlobalWeb
{
    
    ScoringCreditosService ScoringCreditosServicio = new ScoringCreditosService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(ScoringCreditosServicio.CodigoProgramaProvRiesgo, "L");
            Site toolBar = (Site)this.Master;
            toolBar.MostrarImprimir(false);
            toolBar.MostrarExportar(false);
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ScoringCreditosServicio.CodigoProgramaProvRiesgo, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session.Remove("ocultarMenu");
            if (!IsPostBack)
            {
                InicializarPagina();
            }
        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(ScoringCreditosServicio.CodigoProgramaProvRiesgo, "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {
        List<RiesgoCredito> lstConsulta = ScoringCreditosServicio.ListarFechaCierreYaHechas("Z", "D", Usuario);

        ddlFechaCierre.DataSource = lstConsulta;
        ddlFechaCierre.DataValueField = "fecha_corte";
        ddlFechaCierre.DataTextField = "fecha_corte";
        Configuracion conf = new Configuracion();
        ddlFechaCierre.DataTextFormatString = "{0:" + conf.ObtenerFormatoFecha() + "}";
        ddlFechaCierre.DataBind();
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ddlFechaCierre.SelectedItem.Value.Trim() == "")
        {
            VerError("Debe ingresar la fecha de corte");
            return;
        }
        Page.Validate();
        if (Page.IsValid)
        {               
            Actualizar();
        }
    }


    /// <summary>
    /// Evento para cargar valores a la grilla.
    /// </summary>
    private void Actualizar()
    {
        try
        {
            List<RiesgoCredito> lstConsulta = new List<RiesgoCredito>();
            String filtro = obtFiltro(ObtenerValores());
            lstConsulta = ScoringCreditosServicio.ListarRiesgoCreditoProvision(ConvertirStringToDate(ddlFechaCierre.SelectedItem.Text), filtro, (Usuario)Session["usuario"]);

            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                Site toolBar = (Site)this.Master;
                toolBar.MostrarExportar(true);
            }
            else
            {
                gvLista.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
                Site toolBar = (Site)this.Master;
                toolBar.MostrarExportar(false);
            }
            Session["DTLista"] = lstConsulta;
            Session.Add(ScoringCreditosServicio.CodigoProgramaProvRiesgo + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ScoringCreditosServicio.CodigoProgramaProvRiesgo, "Actualizar", ex);
        }
    }


 
    /// <summary>
    /// Esta función actualiza la grilla de créditos al ir a la siguiente página de datos de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ScoringCreditosServicio.CodigoProgramaProvRiesgo, "gvLista_PageIndexChanging", ex);
        }
    }


    /// <summary>
    /// Evento para obtener los filtros ingresados por el usuario para realizar la consulta
    /// </summary>
    /// <param name="credito">Clase que tiene los datos del filtro</param>
    /// <returns>Retorna los filtros a aplicar</returns>
    private string obtFiltro(ScoringCreditos credito)
    {
        String filtro = String.Empty;
        filtro += " And hc.saldo_capital != 0 ";
        return filtro;
    }

    private ScoringCreditos ObtenerValores()
    {
        ScoringCreditos credito = new ScoringCreditos();
        return credito;
    }


    //-------------------------------------------------------------------------------------------------------------
    //------------------------------------------------   Reporte   ------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0)
        {
            List<RiesgoCredito> lstConsulta = new List<RiesgoCredito>();
            lstConsulta = (List<RiesgoCredito>)Session["DTLista"];

            GridView gvExportar = gvLista;
            gvExportar.AllowPaging = false;
            gvExportar.DataSource = lstConsulta;
            gvExportar.DataBind();
            ExportarGridCSVDirecto(gvLista, "SegmentacionCreditos_" + ddlFechaCierre.SelectedItem.Text, ';');
        }
    }



}