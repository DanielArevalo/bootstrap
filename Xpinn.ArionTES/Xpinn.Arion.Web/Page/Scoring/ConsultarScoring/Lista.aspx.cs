using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Scoring.Services;
using Xpinn.Scoring.Entities;
using Microsoft.Reporting.WebForms;

public partial class Lista : GlobalWeb
{
    
    ScoringCreditosService ScoringCreditosServicio = new ScoringCreditosService();
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables
    List<ScoringCreditos> lstConsulta;
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(ScoringCreditosServicio.CodigoPrograma, "L");
            Site toolBar = (Site)this.Master;
            toolBar.MostrarImprimir(false);
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ScoringCreditosServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, ScoringCreditosServicio.GetType().Name);
                if (Session[ScoringCreditosServicio.CodigoPrograma + ".consulta"] != null)
                    CargarListas();
                    Actualizar();
                    mvScoringCreditos.ActiveViewIndex = 0;
            }
        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(ScoringCreditosServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {      
        {
            Page.Validate();

            if (Page.IsValid)
            {
                GuardarValoresConsulta(pConsulta, ScoringCreditosServicio.CodigoPrograma);
                if (mvScoringCreditos.ActiveViewIndex != 0)
                {
                    mvScoringCreditos.ActiveViewIndex = 0;
                    ViewState["id"] = null;
                }                   

                Actualizar();
            }
            
        }       
    }

    /// <summary>
    /// Evento para cargar valores a la grilla.
    /// </summary>
    private void Actualizar()
    {
        try
        {
            lstConsulta = new List<ScoringCreditos>();
            ScoringCreditos credito = new ScoringCreditos();
            String filtro = obtFiltro(ObtenerValores());
            lstConsulta = ScoringCreditosServicio.ListarScoringCredito(credito, "'S', 'V', 'A', 'G', 'C'", (Usuario)Session["usuario"], filtro);

            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
               // ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            }

            Session.Add(ScoringCreditosServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ScoringCreditosServicio.CodigoPrograma, "Actualizar", ex);
        }
    }


    /// <summary>
    /// Esta función permite mostrar el plan de pagos del crédito seleccionado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Muestra el multiview con el detalle de la selecci
        ViewState["id"] = gvLista.SelectedRow.Cells[1].Text;
        Actualizar();
      
       // Detalle del registro seleccionado en el gridview
        lblTipoIdentificacion.Text = lstConsulta[0].DescripcionIdentificacion;
        lblIdentificacion.Text = lstConsulta[0].Identificacion;
        lblNombre.Text = lstConsulta[0].Nombres;
        lblDireccion.Text = lstConsulta[0].Direccion;
        lblTelefono.Text = lstConsulta[0].Telefono;

        lblNumero.Text = lstConsulta[0].Numero_radicacion.ToString();
        lblLinea.Text = lstConsulta[0].Linea;
        lblOficina.Text = lstConsulta[0].Oficina;
        lblEstado.Text = lstConsulta[0].Estado;
        txtMonto.Text = lstConsulta[0].Monto.ToString();
        lblEjecutivo.Text = lstConsulta[0].Ejecutivo;
        txtCuota.Text = lstConsulta[0].Cuota.ToString();
        lblPlazo.Text = lstConsulta[0].Plazo.ToString();
       
        ActualizarCodeudores();
        ActualizarResultadoScoring();
        ActualizarSegumientoScoring();
        mvScoringCreditos.ActiveViewIndex = 1;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarImprimir(true);

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
            BOexcepcion.Throw(ScoringCreditosServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Evento para obtener los filtros ingresados por el usuario para realizar la consulta
    /// </summary>
    /// <param name="credito">Clase que tiene los datos del filtro</param>
    /// <returns>Retorna los filtros a aplicar</returns>
    private string obtFiltro(ScoringCreditos credito)
    {
        String filtro = " where numero_radicacion In (Select x.numero_radicacion From scscoring_credito x)";
        
        if (txtCredito.Text.Trim() != "")
            filtro += " and numero_radicacion=" + txtCredito.Text.Trim();
        else if (ViewState["id"] != null)
            filtro += " and numero_radicacion=" + ViewState["id"].ToString();
        if (txtCliente.Text.Trim() != "")
            filtro += " and nombres LIKE " + "'" + txtCliente.Text.Trim() + "%'";
        if (ddlTipoCredito.SelectedIndex > 0)
            filtro += " and linea=" + "'" + ddlTipoCredito.SelectedItem + "'";
        if (ddlEstado.SelectedValue != "")
            filtro += " and estado=" + "'" + ddlEstado.SelectedValue + "'";
        if (filtro != "")
        {
            string filtro1 = filtro.Substring(7, filtro.Length - 7).Replace(" where ", " and ");
            filtro = " where " + filtro1;
        }
            
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
            
            ListaSolicitada = "TipoCredito";
            TraerResultadosLista();
            ddlTipoCredito.DataSource = lstDatosSolicitud;            
            ddlTipoCredito.DataTextField = "ListaDescripcion";
            ddlTipoCredito.DataValueField = "ListaIdStr";            
            ddlTipoCredito.DataBind();
            ddlTipoCredito.Items.Add(new ListItem("", ""));
            ddlTipoCredito.SelectedItem.Text = "";

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);

    }


    /// <summary>
    /// Evento para cargar valores a la grilla.
    /// </summary>
    private void ActualizarCreditosRecogidos()
    {
        try
        {
            lstConsulta = new List<ScoringCreditos>();
            ScoringCreditos credito = new ScoringCreditos();
            String filtro = obtFiltroCreRec();
            lstConsulta = ScoringCreditosServicio.ListarCreditosRecogidos(credito, (Usuario)Session["usuario"], filtro);

            gvSeguimientoScoring.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvSeguimientoScoring.Visible = true;
                lblInfo0.Visible = false;
                lblTotalRegs0.Visible = true;
                lblTotalRegs0.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvSeguimientoScoring.DataBind();
                // ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvSeguimientoScoring.Visible = false;
                lblInfo0.Visible = true;
                lblTotalRegs0.Visible = false;
            }

            Session.Add(ScoringCreditosServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ScoringCreditosServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private string obtFiltroCreRec()
    {
        String filtro = String.Empty;      
            filtro = " where numero_radicacion=" + ViewState["id"].ToString();
        return filtro;
    }



    /// <summary>
    /// Evento para cargar valores a la grilla.
    /// </summary>
    private void ActualizarCodeudores()
    {
        try
        {
            lstConsulta = new List<ScoringCreditos>();
            ScoringCreditos credito = new ScoringCreditos();
            String filtro = obtFiltroCodeudores();
            lstConsulta = ScoringCreditosServicio.ListarCodeudores(credito, (Usuario)Session["usuario"], filtro);

            gvCodeudores.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvCodeudores.Visible = true;
                lblInfo1.Visible = false;
                lblTotalRegs1.Visible = true;
                lblTotalRegs1.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvCodeudores.DataBind();
                // ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvCodeudores.Visible = false;
                lblInfo1.Visible = true;
                lblTotalRegs1.Visible = false;
            }

            Session.Add(ScoringCreditosServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ScoringCreditosServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private string obtFiltroCodeudores()
    {
        String filtro = String.Empty;
        filtro = " where numerosolicitud=" + ViewState["id"].ToString();
        return filtro;
    }
   


    //-------------------------------------------------------------------------------------------------------------
    //------------------------------------------------   Reporte   ------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BindReportViewer();
            mvScoringCreditos.ActiveViewIndex = 2;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindReportViewer()
    {
        rvScoringCreditos.Visible = true;

        //Parametros
        ReportParameter[] param = new ReportParameter[15];
        param[0] = new ReportParameter("lblTipoIdentificacion", lblTipoIdentificacion.Text);
        param[1] = new ReportParameter("lblIdentificacion", lblIdentificacion.Text);
        param[2] = new ReportParameter("lblNombre", lblNombre.Text);
        param[3] = new ReportParameter("lblDireccion", lblDireccion.Text);
        param[4] = new ReportParameter("lblNumero", lblNumero.Text);
        param[5] = new ReportParameter("lblLinea", lblLinea.Text);
        param[6] = new ReportParameter("lblOficina", lblOficina.Text);
        param[7] = new ReportParameter("lblEstado", lblEstado.Text);
        param[8] = new ReportParameter("txtMonto", txtMonto.Text);
        param[9] = new ReportParameter("lblEjecutivo", lblEjecutivo.Text);
        param[10] = new ReportParameter("txtCuota", txtCuota.Text);
        param[11] = new ReportParameter("lblPlazo", lblPlazo.Text);
        param[12] = new ReportParameter("lblTelefono", lblTelefono.Text);
        param[13] = new ReportParameter("lblTelefono", lblTelefono.Text);
        param[14] = new ReportParameter("lblResultadoScoring", lblResultadoScoring.Text);
        rvScoringCreditos.LocalReport.SetParameters(param);

        //Lista de Codeudores
        IList<ScoringCreditos> CodeudoresList = new List<ScoringCreditos>();
        ScoringCreditos credito = new ScoringCreditos();
        String filtro = obtFiltroCodeudores();
        CodeudoresList = ScoringCreditosServicio.ListarCodeudores(credito, (Usuario)Session["usuario"], filtro);

        ReportDataSource rds = new ReportDataSource("DataSet1", CodeudoresList);

        //Lista de Seguimiento Scoring
        
        IList<SeguimientoScoring> lstConsulta = new List<SeguimientoScoring>(); //Se utilizan las entidades AprobacionScoringNegados, dode se definio la tabla "controlcreditos"
        SeguimientoScoring credito1 = new SeguimientoScoring();
        String filtro1 = obtFiltroSegumientoScoring();
        lstConsulta = ScoringCreditosServicio.ListarSegumientoScoring(credito1, (Usuario)Session["usuario"], filtro1);

        ReportDataSource rdsVar = new ReportDataSource("DataSet2", lstConsulta);

        rvScoringCreditos.LocalReport.DataSources.Clear();
        rvScoringCreditos.LocalReport.DataSources.Add(rds);
        rvScoringCreditos.LocalReport.DataSources.Add(rdsVar);
        rvScoringCreditos.LocalReport.Refresh();
    }


    /// <summary>
    /// ------------------------------------------------------------------------------------------------------------------------------------
    /// --------------------------------------------------   Consulta Resultado Scoring   -------------------------------------------------- 
    /// ------------------------------------------------------------------------------------------------------------------------------------
    /// </summary>
    
    private void ActualizarResultadoScoring()
    {
        try
        {
            List<ScScoringCredito> lstConsulta = new List<ScScoringCredito>();
            ScScoringCredito credito = new ScScoringCredito();
            String filtro = obtFiltroResultadoScoring();
            lstConsulta = ScoringCreditosServicio.ListarScScoringCredito(credito, (Usuario)Session["usuario"], filtro);

            if (lstConsulta.Count > 0)
            {
                lblResultadoScoring.Text = lstConsulta[0].resultado.ToString();
                lblCalificacionScoring.Text = lstConsulta[0].calificacion.ToString();
                lblObservacion.Text = lstConsulta[0].observacion.ToString();
            }
            else
            {
                lblResultadoScoring.Text = "No existen resultados de Scoring para el credito " + ViewState["id"].ToString();
                lblCalificacionScoring.Text = "";
                lblObservacion.Text = "";
            }

            Session.Add(ScoringCreditosServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ScoringCreditosServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private string obtFiltroResultadoScoring()
    {
        String filtro = String.Empty;
        filtro = " where numero_radicacion=" + ViewState["id"].ToString() + " and rownum = 1 order by FECHACREACION desc";
        return filtro;
    }



    

    /// <summary>   
    ///------------------------------------------------------------------------------------------------------------------
    ///---------------------------------------   Actualizar Segumiento Scoring   ----------------------------------------
    ///------------------------------------------------------------------------------------------------------------------
    /// </summary>
     


    private void ActualizarSegumientoScoring()
    {
        try
        {
            List<SeguimientoScoring> lstConsulta = new List<SeguimientoScoring>(); //Se utilizan las entidades AprobacionScoringNegados, dode se definio la tabla "controlcreditos"
            SeguimientoScoring credito = new SeguimientoScoring();
            String filtro = obtFiltroSegumientoScoring();
            lstConsulta = ScoringCreditosServicio.ListarSegumientoScoring(credito, (Usuario)Session["usuario"], filtro);

            gvSeguimientoScoring.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvSeguimientoScoring.Visible = true;
                lblInfo0.Visible = false;
                lblTotalRegs0.Visible = true;
                lblTotalRegs0.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvSeguimientoScoring.DataBind();
                // ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvSeguimientoScoring.Visible = false;
                lblInfo0.Visible = true;
                lblTotalRegs0.Visible = false;
            }

            Session.Add(ScoringCreditosServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ScoringCreditosServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private string obtFiltroSegumientoScoring()
    {
        String filtro = String.Empty;
        filtro = " Where numero_radicacion=" + ViewState["id"].ToString();
        return filtro;
    }
   
}