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
            VisualizarOpciones(ScoringCreditosServicio.CodigoProgramaRiesgo, "L");
            Site toolBar = (Site)this.Master;
            toolBar.MostrarImprimir(false);
            toolBar.MostrarExportar(false);
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ScoringCreditosServicio.CodigoProgramaRiesgo, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session.Remove("ocultarMenu");
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, ScoringCreditosServicio.GetType().Name);
                InicializarPagina();
                CargarListas();
                mvScoringCreditos.ActiveViewIndex = 0;
            }
        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(ScoringCreditosServicio.CodigoProgramaRiesgo, "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.Oficinas, ddloficina);

        List<RiesgoCredito> lstConsulta = ScoringCreditosServicio.ListarFechaCierreYaHechas("Z", "D", Usuario);

        ddlFechaCierre.DataSource = lstConsulta;
        ddlFechaCierre.DataValueField = "fecha_corte";
        ddlFechaCierre.DataTextField = "fecha_corte";
        Configuracion conf = new Configuracion();
        ddlFechaCierre.DataTextFormatString = "{0:" + conf.ObtenerFormatoFecha() + "}";
        ddlFechaCierre.DataBind();

        lstConsulta = ScoringCreditosServicio.ListarCalificaciones("", Usuario);
        ddlSegmentoActual.DataSource = lstConsulta;
        ddlSegmentoActual.DataValueField = "segmento";
        ddlSegmentoActual.DataTextField = "segmento";
        ddlSegmentoActual.DataBind();
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
            GuardarValoresConsulta(pConsulta, ScoringCreditosServicio.CodigoProgramaRiesgo);
            if (mvScoringCreditos.ActiveViewIndex != 0)
            {
                mvScoringCreditos.ActiveViewIndex = 0;
                ViewState["id"] = null;
            }                   

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
            lstConsulta = ScoringCreditosServicio.ListarRiesgoCredito(ConvertirStringToDate(ddlFechaCierre.SelectedItem.Text), filtro, (Usuario)Session["usuario"]);

            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                Site toolBar = (Site)this.Master;
                toolBar.MostrarImprimir(true);
                toolBar.MostrarExportar(true);
            }
            else
            {
                gvLista.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
                Site toolBar = (Site)this.Master;
                toolBar.MostrarImprimir(false);
                toolBar.MostrarExportar(false);
            }
            Session["DTLista"] = lstConsulta;
            Session.Add(ScoringCreditosServicio.CodigoProgramaRiesgo + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ScoringCreditosServicio.CodigoProgramaRiesgo, "Actualizar", ex);
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
            BOexcepcion.Throw(ScoringCreditosServicio.CodigoProgramaRiesgo, "gvLista_PageIndexChanging", ex);
        }
    }

    public MatrizColor[] ColorSegmento = new MatrizColor[4]
    {
        new MatrizColor { calificacion = "1", color = "Green" },
        new MatrizColor { calificacion = "2", color = "Yellow" },
        new MatrizColor { calificacion = "3", color = "Orange" },
        new MatrizColor { calificacion = "4", color = "Red" },
    };

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbSegmento = (Label)e.Row.FindControl("lbSegmento");
                Label lbCalificacion = (Label)e.Row.FindControl("lbCalificacion");
                MatrizColor m = ColorSegmento.Where(x => x.calificacion == lbCalificacion.Text).FirstOrDefault();
                if (m != null)
                {
                    DataControlFieldCell celda = (DataControlFieldCell)lbSegmento.Parent;
                    celda.BackColor = System.Drawing.Color.FromName(m.color);
                }
            }
        }
        catch { }
    }

    /// <summary>
    /// Evento para obtener los filtros ingresados por el usuario para realizar la consulta
    /// </summary>
    /// <param name="credito">Clase que tiene los datos del filtro</param>
    /// <returns>Retorna los filtros a aplicar</returns>
    private string obtFiltro(ScoringCreditos credito)
    {
        String filtro = String.Empty;

        if (ddloficina.SelectedIndex > 0)
            filtro += " And hc.nombre_oficina = " + "'" + ddloficina.SelectedItem.Text + "'";
        if (ddlSegmentoActual.SelectedIndex > 0)
            filtro += " And hc.segmento = " + "'" + ddlSegmentoActual.SelectedItem.Text + "'";
        if (txtCredito.Text.Trim() != "")
            filtro += " And hc.numero_radicacion = " + txtCredito.Text.Trim();
        if (txtCliente.Text.Trim() != "")
            filtro += " And hc.identificacion Like " + "'" + txtCliente.Text.Trim() + "%'";
        if (ddlTipoCredito.SelectedIndex > 0)
            filtro += " And hc.cod_linea_credito = " + "'" + ddlTipoCredito.SelectedItem.Value + "'";
        filtro += " And hc.saldo_capital != 0 ";
        return filtro;
    }

    private ScoringCreditos ObtenerValores()
    {
        ScoringCreditos credito = new ScoringCreditos();
        if (txtCredito.Text.Trim() != "")
            credito.Numero_radicacion = Convert.ToInt32(txtCredito.Text.Trim());
        return credito;
    }

    /// <summary>
    /// Cargar información de las listas desplegables
    /// </summary>
    private void CargarListas()
    {
        try
        {
            Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
            String ListaSolicitada = null;
            ListaSolicitada = "LineasCredito";
            lstDatosSolicitud.Clear();
            lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
            ddlTipoCredito.DataSource = lstDatosSolicitud;            
            ddlTipoCredito.DataTextField = "ListaDescripcion";
            ddlTipoCredito.DataValueField = "ListaIdStr";            
            ddlTipoCredito.DataBind();
            ddlTipoCredito.Items.Add(new ListItem("", ""));
            ddlTipoCredito.SelectedItem.Text = "";

        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    //-------------------------------------------------------------------------------------------------------------
    //------------------------------------------------   Reporte   ------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //BindReportViewer();
            mvScoringCreditos.ActiveViewIndex = 2;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        mvScoringCreditos.ActiveViewIndex = 1;
    }

    protected void btnParar_Click(object sender, EventArgs e)
    {
        
    }

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