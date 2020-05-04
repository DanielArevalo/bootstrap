using System;
using System.Collections.Generic;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Services;
using Microsoft.Reporting.WebForms;
using Xpinn.Util;
using System.Globalization;
using System.Data;

partial class Lista : GlobalWeb
{
    HistoricoSegPerfilService _HistoricoSegPerfil = new HistoricoSegPerfilService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_HistoricoSegPerfil.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.MostrarRegresar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_HistoricoSegPerfil.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, _HistoricoSegPerfil.CodigoPrograma);
                cargarlist();

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_HistoricoSegPerfil.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, _HistoricoSegPerfil.CodigoPrograma);

        Actualizar();
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Site toolBar = (Site)this.Master;
        toolBar.MostrarExportar(true);
        toolBar.MostrarConsultar(true);
        toolBar.MostrarLimpiar(true);
        toolBar.MostrarRegresar(false);

        mvHistorico.ActiveViewIndex = 0;
    }

    private bool ValidarDatos()
    {

        if (ddlFechaCierre.SelectedValue == null || ddlFechaCierre.SelectedValue == " ")
        {
            VerError("Seleccione una  fecha de cierre");
            return false;
        }
        return true;
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, _HistoricoSegPerfil.CodigoPrograma);
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[_HistoricoSegPerfil.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[_HistoricoSegPerfil.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
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
            BOexcepcion.Throw(_HistoricoSegPerfil.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {

            if (ValidarDatos())
            {
                List<HistoricoSegPersona> lstConsulta = _HistoricoSegPerfil.ListarPersonaHistorico(ObtenerValores(), Convert.ToString(txtNompe.Text.Trim()), Convert.ToString(txtApe.Text.Trim()), Convert.ToString(txtIden.Text.Trim()), Convert.ToString(ddlPerfilRiesgo.SelectedValue), Convert.ToString(ddlSegmentoRiesgo.SelectedValue), Usuario);

                gvLista.PageSize = pageSize;
                gvLista.EmptyDataText = emptyQuery;
                gvLista.DataSource = lstConsulta;

                if (lstConsulta.Count > 0)
                {
                    gvLista.Visible = true;
                    lblTotalRegs.Visible = true;
                    lblInfo.Visible = false;
                    lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                    gvLista.DataBind();
                }
                else
                {
                    gvLista.Visible = false;
                    lblTotalRegs.Visible = false;
                    lblInfo.Visible = true;
                }
            }
            Session.Add(_HistoricoSegPerfil.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_HistoricoSegPerfil.CodigoPrograma, "Actualizar", ex);
        }
    }

    HistoricoSegPersona ObtenerValores()
    {
        HistoricoSegPersona vHistoricoSegPersona = new HistoricoSegPersona();
        
        if (ddlFechaCierre.SelectedValue != "")
            vHistoricoSegPersona.FECHACIERRE = Convert.ToDateTime(ddlFechaCierre.SelectedValue);
     
        return vHistoricoSegPersona;
    }

    private void cargarlist()
    {

        Xpinn.Riesgo.Services.HistoricoSegmentacionService consultafechas = new HistoricoSegmentacionService();

        List<HistoricoSegmentacion> listaFechasCerras = consultafechas.ListarFechaCierreYaHechas("", Usuario);
        LlenarListasDesplegables(TipoLista.Segmentos, ddlSegmentoRiesgo);
        ddlFechaCierre.DataSource = listaFechasCerras;
        ddlFechaCierre.DataValueField = "fechacierre";
        ddlFechaCierre.DataTextField = "fechacierre";
        Configuracion conf = new Configuracion();
        ddlFechaCierre.DataTextFormatString = "{0:" + conf.ObtenerFormatoFecha() + "}";
        ddlFechaCierre.DataBind();


    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int32 id = Convert.ToInt32(e.Keys[0]);

            Session["ID"] = id;
            ctlMensaje.MostrarMensaje("Desea eliminar el registro seleccionado?");
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }

    protected void gvlista_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Imprimir")
        {
            VerError("");
            Label lblCod = (Label)gvLista.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblCod");
            Int64 cod_persona = 0;

            string Fecha_cierre = Convert.ToString(ddlFechaCierre.SelectedItem);
            cod_persona = Convert.ToInt64(gvLista.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0]);
            //Agregar parametros al reporte
            System.Data.DataTable table = new DataTable();
            System.Data.DataTable tableH = new DataTable();
            ReportParameter[] param = LlenarParametrosReporte(cod_persona, Fecha_cierre, ref table, ref tableH);
            if (param != null)
            {
                ReportViewerFactor.LocalReport.ReportPath = "Page\\GestionRiesgo\\ReporteHistoricoPersona\\ReporteHistoricopersona.rdlc";
                ReportViewerFactor.LocalReport.EnableExternalImages = true;
                ReportViewerFactor.LocalReport.SetParameters(param);
                ReportDataSource rds = new ReportDataSource("dsAnalisisComparativo", table);
                ReportDataSource hds = new ReportDataSource("dsHistorialSegmentos", tableH);
                ReportViewerFactor.LocalReport.DataSources.Clear();
                ReportViewerFactor.LocalReport.DataSources.Add(rds);
                ReportViewerFactor.LocalReport.DataSources.Add(hds);
                ReportViewerFactor.LocalReport.Refresh();
                ReportViewerFactor.Visible = true;

                Site toolBar = (Site)this.Master;
                toolBar.MostrarExportar(false);
                toolBar.MostrarConsultar(false);
                toolBar.MostrarLimpiar(false);
                toolBar.MostrarRegresar(true);

                mvHistorico.ActiveViewIndex = 1;
            }
        }
    }

    public int CalcularMesesDeDiferencia(DateTime fechaDesde, DateTime fechaHasta)
    {
        return Math.Abs((fechaDesde.Month - fechaHasta.Month) + 12 * (fechaDesde.Year - fechaHasta.Year));
    }

    private ReportParameter[] LlenarParametrosReporte(Int64 cod_persona, string Fecha_cierre, ref System.Data.DataTable pAnalisisComparativo, ref System.Data.DataTable pHistoricoSegmentacion)
    {
        DateTime fecha = DateTime.Today;
        HistoricoSegPersona RptHistoricoSegPersona = new HistoricoSegPersona();
        RptHistoricoSegPersona = _HistoricoSegPerfil.ConsultarPersonaHistorico(cod_persona, Fecha_cierre, Usuario);
        int antiguedad = CalcularMesesDeDiferencia(RptHistoricoSegPersona.fecha_historico, fecha);

        HistoricoSegPersona cP = _HistoricoSegPerfil.Captaciones(cod_persona, Fecha_cierre, Usuario);
        HistoricoSegPersona cL = _HistoricoSegPerfil.Colocaciones(cod_persona, Fecha_cierre, Usuario);

        if (RptHistoricoSegPersona.cod_persona != 0)
        {
            // LLenar data table con los datos a recoger
            pAnalisisComparativo = new System.Data.DataTable();
            pAnalisisComparativo.Columns.Add("fecha", typeof(DateTime));
            pAnalisisComparativo.Columns.Add("captacion", typeof(Decimal));
            pAnalisisComparativo.Columns.Add("colocacion", typeof(Decimal));

            string separadorDecimal = System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;

            DataRow datarw;
            List <HistoricoSegPersona> lstConsulta = new List<HistoricoSegPersona>();
            lstConsulta = _HistoricoSegPerfil.ListarPersonaAnalisis(cod_persona, fecha.ToString(gFormatoFecha), Usuario);
            if (lstConsulta.Count == 0)
            {
                datarw = pAnalisisComparativo.NewRow();
                for (int i = 0; i <= 2; i++)
                {
                    if (i == 0)
                        datarw[i] = DateTime.Now;
                    else
                        datarw[i] = 0;
                }
                pAnalisisComparativo.Rows.Add(datarw);
            }
            else
            {
                foreach (HistoricoSegPersona refe in lstConsulta)
                {
                    datarw = pAnalisisComparativo.NewRow();
                    if (refe.fecha_historico == null)
                        datarw[0] = " ";
                    else
                        datarw[0] = refe.fecha_historico.ToString(GlobalWeb.gFormatoFecha);
                    datarw[1] = refe.captaciones;
                    datarw[2] = refe.colocaciones;
                    pAnalisisComparativo.Rows.Add(datarw);
                }
            }
            // LLenar data table cde historico de segmentacion
            pHistoricoSegmentacion = new System.Data.DataTable();
            pHistoricoSegmentacion.Columns.Add("fecha_segmentacion", typeof(DateTime));
            pHistoricoSegmentacion.Columns.Add("segmento_riesgo", typeof(string));

            DataRow datarwHistorico;
            List<HistoricoSegPersona> lstConsultaH = new List<HistoricoSegPersona>();
            lstConsultaH = _HistoricoSegPerfil.ListarHistorialSegementacion(cod_persona, Usuario);
            if (lstConsultaH.Count == 0)
            {
                datarwHistorico = pHistoricoSegmentacion.NewRow();
                for (int i = 0; i <= 2; i++)
                {
                    if (i == 0)
                        datarwHistorico[i] = DateTime.Now;
                    else
                        datarwHistorico[i] = 0;
                }
                pHistoricoSegmentacion.Rows.Add(datarwHistorico);
            }
            else
            {
                foreach (HistoricoSegPersona refe in lstConsultaH)
                {
                    datarwHistorico = pHistoricoSegmentacion.NewRow();
                    if (refe.fecha_segemento == null)
                        datarwHistorico[0] = " ";
                    else
                        datarwHistorico[0] = refe.fecha_segemento.ToString(GlobalWeb.gFormatoFecha);
                    datarwHistorico[1] = refe.calificacion;
                    pHistoricoSegmentacion.Rows.Add(datarwHistorico);
                }
            }
            ReportParameter[] param = new ReportParameter[31];

            param[0] = new ReportParameter("Nombre", RptHistoricoSegPersona.primer_nombre);
            param[1] = new ReportParameter("Apellido", RptHistoricoSegPersona.primer_apellido);
            param[2] = new ReportParameter("Edad", Convert.ToString(RptHistoricoSegPersona.Edad));
            param[3] = new ReportParameter("Antiguedad", Convert.ToString(antiguedad));
            param[4] = new ReportParameter("Nom_Ofi", RptHistoricoSegPersona.Nom_ofi);
            param[5] = new ReportParameter("Dir_Ofi", RptHistoricoSegPersona.Dir_ofi);
            param[6] = new ReportParameter("Tel_Ofi", RptHistoricoSegPersona.Tel_ofi);
            param[7] = new ReportParameter("Direccion", RptHistoricoSegPersona.Direccion);
            param[8] = new ReportParameter("Celular", Convert.ToString(RptHistoricoSegPersona.Celular));
            param[9] = new ReportParameter("Actividad_Eco", RptHistoricoSegPersona.Actividad_Eco);
            param[10] = new ReportParameter("Estrato", Convert.ToString(RptHistoricoSegPersona.Estrato));
            param[11] = new ReportParameter("Ingresos", Convert.ToString(RptHistoricoSegPersona.Ingresosmensuales));
            param[12] = new ReportParameter("Perfil_Riesgo", RptHistoricoSegPersona.Perfil_riesgo);
            param[13] = new ReportParameter("Num_Ope", Convert.ToString(RptHistoricoSegPersona.Numero_operacionMes));
            param[14] = new ReportParameter("Total_Ope", Convert.ToString(RptHistoricoSegPersona.Valor_operacionMes));
            param[15] = new ReportParameter("ImagenReport", ImagenReporte());
            param[16] = new ReportParameter("Fecha", Convert.ToString(fecha));
            param[17] = new ReportParameter("Identificacion", RptHistoricoSegPersona.identificacion);
            param[18] = new ReportParameter("Empresa", ((Usuario)Session["Usuario"]).empresa);
            param[19] = new ReportParameter("Usuario", ((Usuario)Session["Usuario"]).identificacion);
            param[20] = new ReportParameter("Email", RptHistoricoSegPersona.Email);
            /******Nueva sección********/
            param[21] = new ReportParameter("MontoCPD", Convert.ToString(cP.MontoCPD));
            param[22] = new ReportParameter("MontoCPC", Convert.ToString(cP.MontoCPC));
            param[23] = new ReportParameter("MontoCLD", Convert.ToString(cL.MontoCLD));
            param[24] = new ReportParameter("MontoCLC", Convert.ToString(cL.MontoCLC));
            param[25] = new ReportParameter("NumCPD", Convert.ToString(cP.NumCPD));
            param[26] = new ReportParameter("NumCPC", Convert.ToString(cP.NumCPC));
            param[27] = new ReportParameter("NumCLD", Convert.ToString(cL.NumCLD));
            param[28] = new ReportParameter("NumCLC", Convert.ToString(cL.NumCLC));
            param[29] = new ReportParameter("segmentoRiesgo", Convert.ToString(RptHistoricoSegPersona.segmentoActual));
            if (RptHistoricoSegPersona.analisisCumplimiento == null) RptHistoricoSegPersona.analisisCumplimiento = "";
            param[30] = new ReportParameter("observaciones", Convert.ToString(RptHistoricoSegPersona.analisisCumplimiento));

            return param;
        }
        else
        {
            VerError("No se encuentra  registro  en  el  historico de segmentacion");
            return null;
        }
    }



}