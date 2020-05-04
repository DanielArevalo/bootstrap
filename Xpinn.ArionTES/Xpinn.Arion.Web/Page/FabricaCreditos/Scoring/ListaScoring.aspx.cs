using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;


public partial class ListaScoring : GlobalWeb
{
    ScoringService _scoringService = new ScoringService();
    Usuario _usuario;


    #region  Eventos Carga Inicial


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_scoringService.CodigoPrograma, "L");

            Site toolBar = (Site)Master;

            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_scoringService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["Usuario"];
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_scoringService.CodigoPrograma, "Page_Load", ex);
        }
    }


    #endregion


    #region EventoBotones


    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("../../../General/Global/inicio.aspx");
    }


    private void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        VerError("");

        LlenarScoringRealizados();
    }


    #endregion


    #region Eventos Grilla


    protected void gvScoring_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string idScoring = gvScoring.DataKeys[e.NewEditIndex].Value.ToString();
        Session[_scoringService + ".id"] = idScoring;

        Navegar(Pagina.Detalle);
    }


    protected void gvScoring_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvScoring.PageIndex = e.NewPageIndex;
        LlenarScoringRealizados();
    }


    #endregion


    #region Metodos de Llenado


    // Obtengo el filtro y hago el query de los Creditos CON GARANTIA
    void LlenarScoringRealizados()
    {
        try
        {
            string filtro = ObtenerFiltroToQuery();
            List<Scoring> lstConsulta = _scoringService.ListarScoresRealizados(filtro, _usuario);

            if (lstConsulta.Count == 0)
            {
                lblAvisoNoResultadoGrilla.Visible = true;
            }
            else
            {
                lblAvisoNoResultadoGrilla.Visible = false;
                lblNumeroRegistros.Text = "Número de registros encontrados: " + lstConsulta.Count.ToString();
            }

            gvScoring.DataSource = lstConsulta;
            gvScoring.DataBind();
        }
        catch (Exception ex)
        {
            VerError("LlenarCreditosConGarantia:  " + ex.Message);
        }
    }


    #endregion


    #region Metodo para obtener filtro de acuerdo a la información suministrada


    // Dependiendo de lo escrito en los campos armo el filtro para filtrar el query a realizar
    string ObtenerFiltroToQuery()
    {
        string filtro = string.Empty;
        string cod_persona = txtCodPersona.Text.Trim().ToUpperInvariant();
        string identificacion = txtIdentificacion.Text.Trim();
        string montoInicial = txtMontoIni.Text.Trim();
        string montoMax = txtMontoMax.Text.Trim();
        string plazoInicial = txtPlazoIni.Text.Trim();
        string plazoMax = txtPlazoMax.Text.Trim();
        string apellido = txtApellido.Text.Trim();
        string nombre = txtNombrePersona.Text.Trim();

        //Filtro cod_persona
        if (!string.IsNullOrWhiteSpace(cod_persona))
        {
            filtro += " and PER.COD_PERSONA = " + cod_persona;
        }

        // Filtro identificacion
        if (!string.IsNullOrWhiteSpace(identificacion))
        {
            filtro += " and PER.IDENTIFICACION like '%" + identificacion + "%'";
        }

        // Filtro nombre
        if (!string.IsNullOrWhiteSpace(nombre))
        {
            filtro += " and PER.PRIMER_NOMBRE like '%" + nombre + "%'";
        }

        // Filtro apellido
        if (!string.IsNullOrWhiteSpace(apellido))
        {
            filtro += " and PER.PRIMER_APELLIDO like '%" + apellido + "%'";
        }

        //Filtro código de nómina
        if (txtCodigoNomina.Text.Trim() != "")
            filtro += " and PER.COD_NOMINA = '" + txtCodigoNomina.Text + "'";


        // Filtro Monto
        if (!string.IsNullOrWhiteSpace(montoInicial) && !string.IsNullOrWhiteSpace(montoMax))
        {
            filtro += " and PRE.MONTO_SOLICITADO BETWEEN " + montoInicial + " AND " + montoMax;
        }
        else if (!string.IsNullOrWhiteSpace(montoInicial))
        {
            filtro += " and PRE.MONTO_SOLICITADO > " + montoInicial;
        }
        else if (!string.IsNullOrWhiteSpace(montoMax))
        {
            filtro += " and PRE.MONTO_SOLICITADO < " + montoMax;
        }


        // Filtro Plazo
        if (!string.IsNullOrWhiteSpace(plazoInicial) && !string.IsNullOrWhiteSpace(plazoMax))
        {
            filtro += " and PRE.PLAZO_SOLICITADO BETWEEN " + plazoInicial + " AND " + plazoMax;
        }
        else if (!string.IsNullOrWhiteSpace(plazoInicial))
        {
            filtro += " and PRE.PLAZO_SOLICITADO > " + plazoInicial;
        }
        else if (!string.IsNullOrWhiteSpace(plazoMax))
        {
            filtro += " and PRE.PLAZO_SOLICITADO < " + plazoMax;
        }

        return filtro;
    }


    #endregion


}