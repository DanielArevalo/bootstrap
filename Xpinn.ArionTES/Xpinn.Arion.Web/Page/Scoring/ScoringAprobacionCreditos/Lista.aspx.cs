using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Scoring.Services;
using Xpinn.Scoring.Entities;
using Xpinn.FabricaCreditos.Services;
using Microsoft.Reporting.WebForms;

public partial class Lista : GlobalWeb
{
    
    ScoringCreditosService ScoringCreditosServicio = new ScoringCreditosService();
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables
    List<ScoringCreditos> lstConsulta;
    CreditoPlanService creditoPlanServicio = new CreditoPlanService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(ScoringCreditosServicio.CodigoProgramaApr, "L");
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarImprimir(false);
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ScoringCreditosServicio.CodigoProgramaApr, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session.Remove("ocultarMenu");
            if (!IsPostBack)
            {
                PanelSolicitud.Visible = false;
                CargarValoresConsulta(pConsulta, ScoringCreditosServicio.GetType().Name);
                if (Session[ScoringCreditosServicio.CodigoProgramaApr + ".consulta"] != null)
                    CargarListas();
                    Actualizar();
                    mvScoringCreditos.ActiveViewIndex = 0;
            }
        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(ScoringCreditosServicio.CodigoProgramaApr, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        
        {
            Page.Validate();

            if (Page.IsValid)
            {
                PanelSolicitud.Visible = false;
                GuardarValoresConsulta(pConsulta, ScoringCreditosServicio.CodigoProgramaApr);
                if (mvScoringCreditos.ActiveViewIndex != 0)
                {
                    mvScoringCreditos.ActiveViewIndex = 0;
                    ViewState["id"] = null;
                }                   

                Actualizar();
            }
            
        }       
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        mpeNuevo.Show();
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
       //Guarda el resultado del scoring en la tabla "scscoring_credito"
        ScScoringCredito vScScoringCredito = new ScScoringCredito();

        vScScoringCredito.idscorecre = 0;
        vScScoringCredito.numero_radicacion = lblNumero.Text.Trim() != "" ? Convert.ToInt64(lblNumero.Text.Trim()) : 0;
        vScScoringCredito.idscore = 0;
        vScScoringCredito.fecha_scoring = DateTime.Now;
        vScScoringCredito.resultado = lblResultadoScoring.Text.Trim() != "" ? Convert.ToDouble(lblResultadoScoring.Text.Trim()) : 0; 
        vScScoringCredito.calificacion = lblCalificacionScoring.Text;
        vScScoringCredito.observacion = txtObservacion.Text;
        if (lblTipoScoring.Text == "Negación")
        {
            vScScoringCredito.estado = 3;
            vScScoringCredito.tipo = 1;
        }
        if (lblTipoScoring.Text == "Reevaluación")
        {
            vScScoringCredito.estado = 2;
            vScScoringCredito.tipo = 2;
        }
        if (lblTipoScoring.Text == "Aprobación")
        {
            vScScoringCredito.estado = 1;
            vScScoringCredito.tipo = 3;
        }        
        vScScoringCredito.fechacreacion = DateTime.Now; 
        vScScoringCredito.fecultmod = DateTime.MinValue;
        vScScoringCredito = ScoringCreditosServicio.ValidarScoringCredito(vScScoringCredito, (Usuario)Session["usuario"]);
        if (vScScoringCredito.error != "")
        {
            VerError(vScScoringCredito.error);
            return;
        }
        ScoringCreditosServicio.CrearScScoringCredito(vScScoringCredito, (Usuario)Session["usuario"]);
        ViewState["id"] = null;
        mvScoringCreditos.ActiveViewIndex = 0;

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
            lstConsulta = ScoringCreditosServicio.ListarScoringCredito(credito, "'S', 'V', 'A', 'G'", (Usuario)Session["usuario"], filtro);

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

            Session.Add(ScoringCreditosServicio.CodigoProgramaApr + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ScoringCreditosServicio.CodigoProgramaApr, "Actualizar", ex);
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
        lblCodigo.Text = lstConsulta[0].Cod_Cliente.ToString();

        lblNumero.Text = lstConsulta[0].Numero_radicacion.ToString();
        lblLinea.Text = lstConsulta[0].Linea;
        lblOficina.Text = lstConsulta[0].Oficina;
        lblEstado.Text = lstConsulta[0].Estado;
        txtMonto.Text = lstConsulta[0].Monto.ToString();
        lblEjecutivo.Text = lstConsulta[0].Ejecutivo;
        txtCuota.Text = lstConsulta[0].Cuota.ToString();
        lblPlazo.Text = lstConsulta[0].Plazo.ToString();

        ActualizarCreditosRecogidos();
        ActualizarCodeudores();
        CalcularScoring();
        mvScoringCreditos.ActiveViewIndex = 1;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarImprimir(true);
        Session[creditoPlanServicio.CodigoPrograma + ".id"] = lblNumero.Text;
        Session[creditoPlanServicio.CodigoPrograma + ".solicitud"] = "1";        
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
            BOexcepcion.Throw(ScoringCreditosServicio.CodigoProgramaApr, "gvLista_PageIndexChanging", ex);
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
        
        if (txtCredito.Text.Trim() != "")
            filtro += " And numero_radicacion = " + txtCredito.Text.Trim();
        else if (ViewState["id"] != null)
            filtro += " And numero_radicacion = " + ViewState["id"].ToString();
        if (txtCliente.Text.Trim() != "")
            filtro += " And nombres LIKE " + "'" + txtCliente.Text.Trim() + "%'";
        if (ddlTipoCredito.SelectedIndex > 0)
            filtro += " And linea = " + "'" + ddlTipoCredito.SelectedItem + "'";
        if (ddlEstado.SelectedValue != "")
            filtro += " And estado = " + "'" + ddlEstado.SelectedValue + "'";

        if (filtro.StartsWith(" And"))
            filtro = " Where " + filtro.Substring(4, filtro.Length - 4);
            
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

            gvCreditosRecogidos.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvCreditosRecogidos.Visible = true;
                lblInfo0.Visible = false;
                lblTotalRegs0.Visible = true;
                lblTotalRegs0.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvCreditosRecogidos.DataBind();
                // ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvCreditosRecogidos.Visible = false;
                lblInfo0.Visible = true;
                lblTotalRegs0.Visible = false;
            }

            Session.Add(ScoringCreditosServicio.CodigoProgramaApr + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ScoringCreditosServicio.CodigoProgramaApr, "Actualizar", ex);
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

            Session.Add(ScoringCreditosServicio.CodigoProgramaApr + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ScoringCreditosServicio.CodigoProgramaApr, "Actualizar", ex);
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
        ReportParameter[] param = new ReportParameter[12];
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
        rvScoringCreditos.LocalReport.SetParameters(param);
        rvScoringCreditos.LocalReport.DataSources.Clear();
        rvScoringCreditos.LocalReport.Refresh();
    }


    private void CalcularScoring()
    {
        ScScoringCredito eScoringCredito = new ScScoringCredito();
        eScoringCredito.idscorecre = 0;
        eScoringCredito.numero_radicacion = lblNumero.Text.Trim() != "" ? Convert.ToInt64(lblNumero.Text.Trim()) : 0;
        eScoringCredito.cod_cliente = lblCodigo.Text.Trim() != "" ? Convert.ToInt64(lblCodigo.Text.Trim()) : 0;
        eScoringCredito.idscore = 0;
        eScoringCredito.modelo = 0;
        eScoringCredito.fecha_scoring = DateTime.Now;
        eScoringCredito.clase_scoring = 1;
        eScoringCredito.calificacion = " ";
        eScoringCredito.estado = 0;
        eScoringCredito.fechacreacion = DateTime.Now;
        eScoringCredito.fecultmod = DateTime.MinValue;
        eScoringCredito.observacion = " ";
        eScoringCredito = ScoringCreditosServicio.CalculaScScoringCredito(eScoringCredito, (Usuario)Session["Usuario"]);
        lblResultadoScoring.Text = eScoringCredito.resultado.ToString();
        lblCalificacionCliente.Text = eScoringCredito.calificacion_cliente.ToString();
        lblCalificacionScoring.Text = eScoringCredito.calificacion;
        if (eScoringCredito.modelo == 2)
            gvParametros.Columns[3].HeaderText = "Beta";
        else
            gvParametros.Columns[3].HeaderText = "Puntaje";
        if (eScoringCredito.tipo == 1)
            lblTipoScoring.Text = "Negación";
        if (eScoringCredito.tipo == 2)
            lblTipoScoring.Text = "Reevaluación";
        if (eScoringCredito.tipo == 3)
            lblTipoScoring.Text = "Aprobación";
        txtObservacion.Text = eScoringCredito.observacion;
        
        List<ScScoringCreditoDetalle> eLstDetalle = new List<ScScoringCreditoDetalle>();
        eLstDetalle = ScoringCreditosServicio.ListarScoringCreditoDetalle(eScoringCredito, (Usuario)Session["Usuario"], "");
        double total = 0;
        foreach (ScScoringCreditoDetalle rFila in eLstDetalle)
        {
            total += rFila.puntaje;
        }
        gvParametros.DataSource = eLstDetalle;        
        gvParametros.DataBind();
        lblTotal.Text = "Total: " + total.ToString();
        if (eLstDetalle.Count > 0)
            lblTotal.Visible = true;
        else
            lblTotal.Visible = false;
    }

    protected void btnInformeSolicitud_Click(object sender, EventArgs e)
    {
        Session[creditoPlanServicio.CodigoPrograma + ".id"] = lblNumero.Text;
        Session[creditoPlanServicio.CodigoPrograma + ".solicitud"] = "1";   
        mvScoringCreditos.ActiveViewIndex = 3;
        PanelSolicitud.Visible = true;
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        mvScoringCreditos.ActiveViewIndex = 1;
    }

    protected void btnParar_Click(object sender, EventArgs e)
    {
        mpeNuevo.Hide();
    }


}