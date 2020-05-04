using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Reporteador.Services;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Configuration;
using Xpinn.Reporteador.Entities;
using System.IO;
using System.Diagnostics;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.Hosting;


public partial class Lista : GlobalWeb
{
    Xpinn.Reporteador.Services.ReporteService reporteServicio = new Xpinn.Reporteador.Services.ReporteService();
    Usuario usuario = new Usuario();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            try
            {
                if (Request.QueryString["CodOpcion"].Trim() != "")
                    reporteServicio.AsignarCodigoPrograma(Convert.ToString(Request.QueryString["CodOpcion"].Trim()));
                VisualizarOpciones(reporteServicio.CodigoPrograma, "L");
            }
            catch
            {
            }
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ddlConsultar.SelectedValue) == -1)
                mvParametros.ActiveViewIndex = 2;
            else
                mvParametros.ActiveViewIndex = 0;

            if (!IsPostBack)
            {
                mvParametros.ActiveViewIndex = 0;
                CargarDDList();
                CargarValoresConsulta(pConsulta, reporteServicio.CodigoPrograma);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (Request.QueryString["codigo"] != null)
                {
                    string pResult = Request.QueryString["codigo"].ToString();
                    if (pResult != "")
                    {
                        ddlConsultar.SelectedValue = pResult;
                        ddlConsultar_SelectedIndexChanged(ddlConsultar, null);
                    }
                }
                ddlConsultar_SelectedIndexChanged(null, null);
            }
            else
            {
                gvParametros.Visible = true;
                mvParametros.ActiveViewIndex = 1;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    private void CargarDDList()
    {
        ddlConsultar.DataSource =
            reporteServicio.ListarReporteUsuario((Usuario)Session["usuario"]).OrderBy(x => x.descripcion).ToList();
        ddlConsultar.DataTextField = "descripcion";
        ddlConsultar.DataValueField = "idReporte";
        ddlConsultar.DataBind();
    }

    /// <summary>
    /// Método que permite generar el reporte segùn las condiciones dadas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Page.Validate();
        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, reporteServicio.CodigoPrograma);
            Session["op1"] = ddlConsultar.SelectedIndex;
            Generar(Convert.ToInt32(ddlConsultar.SelectedValue));
        }
    }

    /// <summary>
    /// Méotodo para limpiar datos de la conuslta
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, reporteServicio.CodigoPrograma);
        gvReporte.DataSource = null;
        gvReporte.DataBind();
        lblTotalRegs.Text = "";
    }

    protected void gvReporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvReporte.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoPrograma, "gvListaCreditos_PageIndexChanging", ex);
        }
    }


    private void Generar(int pCodReporte)
    {
        VerError("");
        Int64 tipoReporte = 0;
        Xpinn.Reporteador.Entities.Reporte eReporte = new Xpinn.Reporteador.Entities.Reporte();
        if (pCodReporte > 0)
        {
            eReporte = reporteServicio.ConsultarReporte(pCodReporte, (Usuario)Session["usuario"]);
            tipoReporte = eReporte.tipo_reporte;
        }
        else
        {
            tipoReporte = 2;
        }

        if (tipoReporte == 3)
        {
            mvReporte.SetActiveView(vCrystalReport);
            //crvReporte.ReportSource = eReporte.url_crystal;
            return;
        }

        // Determinar fecha del histórico
        DateTime fechahistorico = DateTime.Now;

        // Determinar parametros de entrada
        List<Parametros> lstParametros = new List<Parametros>();
        foreach (GridViewRow rFila in gvParametros.Rows)
        {
            Parametros eParametros = new Parametros();
            Label lblidParametro = (Label)rFila.FindControl("lblidParametro");
            Label lblDescripcion = (Label)rFila.FindControl("lblDescripcion");
            Label lblTipo = (Label)rFila.FindControl("lblTipo");
            Label lblIdLista = (Label)rFila.FindControl("lblIdLista");
            TextBox txtValor = (TextBox)rFila.FindControl("txtValor");
            ctlListarCodigo ctlValores = (ctlListarCodigo)rFila.FindControl("ctlListarValores");

            eParametros.idparametro = Convert.ToInt64(lblidParametro.Text);
            eParametros.descripcion = lblDescripcion.Text;
            eParametros.tipo = Convert.ToInt64(lblTipo.Text);
            eParametros.valor = txtValor.Text;
            if (lblIdLista != null)
                if (lblIdLista.Text.Trim() != "")
                    if (!string.IsNullOrWhiteSpace(ctlValores.Codigo))
                        eParametros.valor = ctlValores.Codigo;
            lstParametros.Add(eParametros);
        }

        // Inicializar el datatable
        gvReporte.Columns.Clear();
        DataTable dtReporte = new DataTable();
        string[] aColumnas = new string[] { };
        System.Type[] aTipos = new System.Type[] { };
        int numerocolumnas = 0;
        dtReporte.Clear();
        string sError = "";
        try
        {
            dtReporte = reporteServicio.GenerarReporte(pCodReporte, fechahistorico, lstParametros, ref aColumnas,
                ref aTipos, ref numerocolumnas, ref sError, (Usuario)Session["usuario"]);
            if (sError.Trim() != "")
                VerError(sError);
        }
        catch (Exception ex)
        {
            VerError("Error al generar el reporte. " + ex.Message);
            return;
        }
        // Colocar columna numeracion
        Session["NUMERAR"] = eReporte.numerar.ToString();
        int colInicial = 0;
        if (eReporte.numerar == 1)
        {
            TemplateField tempDesc = new TemplateField();
            tempDesc.ItemTemplate = new GridViewItemTemplate("Num");
            gvReporte.Columns.Add(tempDesc);
            colInicial = 1;
        }
        // Generar columnas
        BoundField[] ColumnBoundKAP = new BoundField[] { };
        int TotalWith = 0;
        for (int i = colInicial; i < numerocolumnas + colInicial; i++)
        {
            if (aColumnas[i - colInicial] != null)
            {
                Array.Resize(ref ColumnBoundKAP, i + 1);
                ColumnBoundKAP[i] = new BoundField();
                ColumnBoundKAP[i].HeaderText = aColumnas[i - colInicial];
                ColumnBoundKAP[i].DataField = aColumnas[i - colInicial];
                int largo = aColumnas[i - colInicial].Length;
                if (largo < 10)
                    largo = 10;
                largo = largo * 8;
                //ColumnBoundKAP[i].ItemStyle.Width = largo;
                //ColumnBoundKAP[i].ControlStyle.Width = largo;
                //ColumnBoundKAP[i].HeaderStyle.Width = largo;
                if (aTipos[i - colInicial] == typeof(System.DateTime))
                {
                    //ColumnBoundKAP[i].DataFormatString = "{0:d}";                    
                    TotalWith = TotalWith + largo;
                    ColumnBoundKAP[i].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (aTipos[i - colInicial] == typeof(System.Decimal))
                {
                    //ColumnBoundKAP[i].DataFormatString = "{0:N}";
                    TotalWith = TotalWith + largo;
                    ColumnBoundKAP[i].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                }
                if (aTipos[i - colInicial] == typeof(System.Int32))
                {
                    //ColumnBoundKAP[i].DataFormatString = "{0:F}";
                    TotalWith = TotalWith + largo;
                    ColumnBoundKAP[i].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                }
                if (aTipos[i - colInicial] == typeof(System.String))
                {
                    //ColumnBoundKAP[i].ItemStyle.Width = 300;
                    //ColumnBoundKAP[i].ControlStyle.Width = 300;
                    //ColumnBoundKAP[i].HeaderStyle.Width = 300;
                    TotalWith = TotalWith + 300;
                    ColumnBoundKAP[i].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                }
                gvReporte.Columns.Add(ColumnBoundKAP[i]);
            }
        }
        Session["DTPREPORTE"] = dtReporte;
        TotalWith = TotalWith + 100;
        gvReporte.Width = TotalWith;
        gvReporte.DataSource = dtReporte;
        lblTitulo.Text = eReporte.encabezado;
        if (eReporte.lstColumnaReporte != null)
        {
            TotalWith = 0;
            int numcol = 0;
            foreach (ColumnaReporte eColumna in eReporte.lstColumnaReporte)
            {
                if (numcol <= gvReporte.Columns.Count)
                {
                    if (eColumna.titulo != null)
                    {
                        gvReporte.Columns[numcol + colInicial].HeaderText = eColumna.titulo;
                        if (eColumna.tipodato == "VARCHAR2")
                        {
                            gvReporte.Columns[numcol + colInicial].HeaderText = eColumna.titulo + "@";
                        }
                    }
                    gvReporte.Columns[numcol + colInicial].ItemStyle.Wrap = true;
                    if (eColumna.ancho > 0)
                    {
                        gvReporte.Columns[numcol + colInicial].HeaderStyle.Width = Convert.ToInt32(eColumna.ancho) * 6;
                        gvReporte.Columns[numcol + colInicial].ItemStyle.Width = Convert.ToInt32(eColumna.ancho) * 6;
                        TotalWith += Convert.ToInt32(eColumna.ancho);
                    }
                    if (eColumna.formato != null)
                    {
                        DataControlField columna = gvReporte.Columns[numcol + colInicial];
                        if (columna is BoundField)
                        {
                            BoundField boundField = columna as BoundField;
                            boundField.DataFormatString = eColumna.formato;
                        }
                    }
                    if (eColumna.alineacion != null)
                    {
                        if (eColumna.alineacion == "Izquierda")
                            gvReporte.Columns[numcol + colInicial].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                        if (eColumna.alineacion == "Centro")
                            gvReporte.Columns[numcol + colInicial].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        if (eColumna.alineacion == "Derecha")
                            gvReporte.Columns[numcol + colInicial].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    }
                }
                numcol += 1;
            }
            gvReporte.Width = TotalWith;
        }

        gvReporte.PageSize = 50;
        gvReporte.DataBind();
        // Mostrar totales
        if (eReporte.lstColumnaReporte != null)
        {
            int numcol = colInicial;
            foreach (ColumnaReporte eColumna in eReporte.lstColumnaReporte)
            {
                if (numcol <= gvReporte.Columns.Count)
                {
                    if (eColumna.total == true)
                    {
                        string nombreCol = "Sum(" + aColumnas[numcol - colInicial] + ")";
                        object sumaCol = dtReporte.Compute(nombreCol, "");
                        if (sumaCol != null && sumaCol.ToString().Trim() != "")
                        {
                            gvReporte.FooterRow.Cells[numcol].Text = Convert.ToDecimal(sumaCol).ToString("C2");
                            gvReporte.FooterRow.Cells[numcol].HorizontalAlign = HorizontalAlign.Right;
                        }
                    }
                }
                numcol += 1;
            }
        }
        // Mostrar registros
        gvReporte.EmptyDataText = emptyQuery;
        if (dtReporte.Rows.Count > 0)
        {
            mvReporte.SetActiveView(vGridReporte);
            lblTotalRegs.Text = "<br/> Registros encontrados: " + dtReporte.Rows.Count;
        }
        else
        {
            mvReporte.ActiveViewIndex = -1;
        }
    }

    /// <summary>
    /// Método para traer los datos según las condiciones ingresadas
    /// </summary>
    private void Actualizar()
    {
        Configuracion conf = new Configuracion();
        VerError("");
        try
        {
            // Generar el reporte
            if (Session["DTPREPORTE"] == null)
            {
                lblMensajeRep.Text = "No se han cargado los datos del reporte";
            }
            lblMensajeRep.Text = "";
            gvReporte.EmptyDataText = emptyQuery;
            DataTable dtReporte = new DataTable();
            dtReporte = (DataTable)Session["DTPREPORTE"];
            gvReporte.DataSource = dtReporte;
            if (dtReporte.Rows.Count > 0)
            {
                mvReporte.SetActiveView(vGridReporte);
                lblTotalRegs.Text = "<br/> Registros encontrados" + dtReporte.Rows.Count;
                gvReporte.DataBind();
            }
            else
            {
                mvReporte.ActiveViewIndex = -1;
            }
        }
        catch (Exception ex)
        {
            VerError(ex.ToString());
        }
    }



    /// <summary>
    /// Método para obtener datos del filtro
    /// </summary>
    /// <returns></returns>
    private string obtFiltro()
    {
        String filtro = String.Empty;
        return filtro;
    }


    protected void ddlConsultar_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlConsultar.SelectedValue) == -1)
            mvParametros.ActiveViewIndex = 2;
        else
            mvParametros.ActiveViewIndex = 0;
        ActualizarPar();
        // Esto es para limpiar la grilla
        DataTable dtReporte = new DataTable();
        gvReporte.DataSource = dtReporte;
        gvReporte.DataBind();
    }


    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (gvReporte.Rows.Count > 0 && Session["DTPREPORTE"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            GridView gvFuck = gvReporte;
            gvFuck.AllowPaging = false;
            gvFuck.DataSource = Session["DTPREPORTE"];
            gvFuck.DataBind();
            for (int numcol = 0; numcol < gvFuck.Columns.Count; numcol++)
            {
                if (gvFuck.HeaderRow.Cells[numcol].Text.Contains("@"))
                {
                    string aux = gvFuck.HeaderRow.Cells[numcol].Text;
                    gvFuck.HeaderRow.Cells[numcol].Text = aux.Substring(0, aux.Length - 1); 
                    aux = gvFuck.Columns[numcol].HeaderText;
                    gvFuck.Columns[numcol].HeaderText = aux.Substring(0, aux.Length-1);
                    for (int i = 0; i < gvFuck.Rows.Count; i++)
                    {
                        GridViewRow row = gvFuck.Rows[i];
                        row.Cells[numcol].Attributes.Add("style", "mso-number-format:\\@");
                    }
                }
            }
            gvFuck.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvFuck);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=Reporte.xls");
            Response.Charset = "UTF-16";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        ExportToPdf(gvReporte, PaginaPDF.Checked);
    }


    protected void gvParametros_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblIdLista = (Label)e.Row.FindControl("lblIdLista");
            if (lblIdLista != null)
            {
                if (lblIdLista.Text.Trim() != "")
                {
                    TextBox txtValor = (TextBox)e.Row.FindControl("txtValor");
                    RequiredFieldValidator rfvTxtValor = (RequiredFieldValidator)e.Row.FindControl("rfvTxtValor");
                    if (txtValor != null)
                        txtValor.Visible = false;
                    if (rfvTxtValor != null)
                        rfvTxtValor.Enabled = false;
                    Usuario pUsuario = new Usuario();
                    pUsuario = (Usuario)Session["usuario"];
                    Xpinn.Reporteador.Entities.Lista pLista = new Xpinn.Reporteador.Entities.Lista();
                    pLista = reporteServicio.ConsultarLista(Convert.ToInt64(lblIdLista.Text), pUsuario);
                    ctlListarCodigo ctlValores = (ctlListarCodigo)e.Row.FindControl("ctlListarValores");

                    if (ctlValores != null)
                    {
                        DataTable dtReporte = new DataTable();
                        dtReporte.Clear();
                        string sError = "";
                        dtReporte = reporteServicio.GenerarLista(pLista.sentencia, ref sError,
                            (Usuario)Session["usuario"]);
                        if (sError.Trim() == "")
                        {
                            var hola = (from reporteRow in dtReporte.Rows.OfType<DataRow>()
                                        select new
                                        {
                                            Descripcion = reporteRow.ItemArray[1],
                                            Codigo = reporteRow.ItemArray[0]
                                        }).ToList();

                            ctlValores.TextField = "Descripcion";
                            ctlValores.ValueField = "Codigo";
                            ctlValores.BindearControl(hola);
                        }
                    }
                }
                else
                {
                    // Al actualizar el gridview, cambia el codigo de la columna "Modelo" por la respectiva descripcion
                    TextBox txtValor = (TextBox)e.Row.FindControl("txtValor");
                    Label lblTipo = (Label)e.Row.FindControl("lblTipo");
                    AjaxControlToolkit.FilteredTextBoxExtender ftbetxtValor =
                        (AjaxControlToolkit.FilteredTextBoxExtender)e.Row.FindControl("ftbetxtValor");
                    AjaxControlToolkit.CalendarExtender ceTxtValor =
                        (AjaxControlToolkit.CalendarExtender)e.Row.FindControl("ceTxtValor");
                    RequiredFieldValidator rfvTxtValor = (RequiredFieldValidator)e.Row.FindControl("rfvTxtValor");
                    ctlListarCodigo ctlValores = (ctlListarCodigo)e.Row.FindControl("ctlListarValores");
                    ctlValores.Visible = false;
                    txtValor.Enabled = true;

                    if (lblTipo != null)
                    {
                        if (ftbetxtValor != null)
                            ftbetxtValor.Enabled = false;
                        if (ceTxtValor != null)
                            ceTxtValor.Enabled = false;
                        if (rfvTxtValor != null)
                            rfvTxtValor.Enabled = false;

                        switch (lblTipo.Text)
                        {
                            case "0":
                                break;
                            case "1":
                                break;
                            case "2":
                                if (ftbetxtValor != null)
                                    ftbetxtValor.Enabled = false;
                                if (ceTxtValor != null)
                                    ceTxtValor.Enabled = true;
                                if (rfvTxtValor != null)
                                    rfvTxtValor.Enabled = true;
                                break;
                            case "3":
                                if (ceTxtValor != null)
                                    ceTxtValor.Enabled = false;
                                if (rfvTxtValor != null)
                                    rfvTxtValor.Enabled = true;
                                break;
                            default:
                                lblTipo.Text = "";
                                break;

                        }
                    }
                }
            }
        }
    }

    public void ActualizarPar()
    {
        if (ddlConsultar.SelectedValue == "0")
            return;
        try
        {
            List<Parametros> lstConsulta = new List<Parametros>();
            Parametros pParametros = new Parametros();
            pParametros.idreporte = Convert.ToInt32(ddlConsultar.SelectedValue);
            lstConsulta = reporteServicio.ListarParametro(pParametros, (Usuario)Session["usuario"]);
            gvParametros.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            gvParametros.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvParametros.Visible = true;
                gvParametros.DataBind();
                mvParametros.ActiveViewIndex = 1;
            }
            else
            {
                //Permite visualizar el footer del gridview cuando no hay ningun registro para mostrar
                Parametros vParametros = new Parametros();
                vParametros.idparametro = 0;
                vParametros.idreporte = 0;
                vParametros.descripcion = "";
                vParametros.tipo = 0;
                lstConsulta.Add(vParametros);
                gvParametros.DataBind();
                gvParametros.Rows[0].Visible = false;
                if (Convert.ToInt32(ddlConsultar.SelectedValue) == -1)
                    mvParametros.ActiveViewIndex = 2;
                else
                    mvParametros.ActiveViewIndex = 0;
            }

            Session.Add(reporteServicio.CodigoPrograma + ".consulta", 1);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    protected void gvReporte_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Session["NUMERAR"].ToString() == "1")
            {
                Label lcDescripcion = (Label)e.Row.FindControl("lblNum");
                lcDescripcion.Text = ((e.Row.RowIndex + 1) + (gvReporte.PageIndex * gvReporte.PageSize)).ToString();
            }
        }
    }

    protected void btnCombinar_Click(object sender, EventArgs e)
    {
        Usuario pUsuario = (Usuario)Session["usuario"];

        // Limpiar instancia de libre office
        EliminarInstanciasDeLibreOffice();
        //System.Threading.Thread.Sleep(5000);

        // Determinar archivo de plantilla
        List<Plantilla> lstPlantilla = new List<Plantilla>();
        Plantilla plantilla = new Plantilla();
        plantilla.idreporte = Convert.ToInt64(ddlConsultar.SelectedValue);
        lstPlantilla = reporteServicio.ListarPlantilla(plantilla, pUsuario);
        if (lstPlantilla.Count <= 0)
        {
            VerError("El reporte seleccionado no tiene plantillas adjuntas");
            return;
        }

        // Generrar informe
        string filePathPlantilla = Path.Combine(HostingEnvironment.ApplicationPhysicalPath,
            @"Page\Reporteador\Reporte\Plantillas\" + lstPlantilla[0].archivo);
        string filePathFinal = Path.Combine(HostingEnvironment.ApplicationPhysicalPath,
            @"Page\Reporteador\Reporte\Plantillas\PLANTILLA_FINAL.docx");
        try
        {
            int control = 0;
            foreach (GridViewRow pRegistro in gvReporte.Rows)
            {
                CombinarCorrespondencia combCorresp = new CombinarCorrespondencia();
                if (control == 0)
                {
                    combCorresp.ReemplazarEnDocumentoDeWordGrid(filePathPlantilla.ToString(), filePathFinal.ToString(),
                        gvReporte, control);
                }
                else
                {
                    string filePathDes = Path.Combine(HostingEnvironment.ApplicationPhysicalPath,
                        @"Page\Reporteador\Reporte\Plantillas\PLANTILLA_TEMP.docx");
                    combCorresp.ReemplazarEnDocumentoDeWordGrid(filePathPlantilla.ToString(), filePathDes.ToString(),
                        gvReporte, control);
                    CombinarCorrespondencia unirCorresp = new CombinarCorrespondencia();
                    unirCorresp.UnirFicheros(filePathDes.ToString(), filePathFinal.ToString());
                }
                control += 1;
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }

        Response.Clear();
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Buffer = false;
        Response.AddHeader("Content-Disposition", @"attachment;filename=PLANTILLA_FINAL.docx");
        Response.ContentType = "application/octet-stream";
        Response.Flush();
        Response.WriteFile(filePathFinal);
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void EliminarInstanciasDeLibreOffice()
    {
        /* Por el error en libreoffice (https://bugs.freedesktop.org/show_bug.cgi?id=37531) */
        while (Process.GetProcessesByName("soffice.bin").Length > 0)
        {
            Process[] procs = Process.GetProcessesByName("soffice.bin");
            foreach (Process p in procs)
            {
                p.Kill();
            }
            System.Threading.Thread.Sleep(1000);
        }
        /* Por el error en libreoffice */
    }

    protected void ExportToPdf(GridView gvReport, bool landScape)
    {
        int noOfColumns = 0, noOfRows = 0;
        DataTable tbl = null;

        if (gvReport.AutoGenerateColumns)
        {
            tbl = gvReport.DataSource as DataTable; // Gets the DataSource of the GridView Control.
            noOfColumns = tbl.Columns.Count;
            noOfRows = tbl.Rows.Count;
        }
        else
        {
            noOfColumns = gvReport.Columns.Count;
            noOfRows = gvReport.Rows.Count;
        }

        float HeaderTextSize = 8;
        float ReportNameSize = 10;
        float ReportTextSize = 8;
        float ApplicationNameSize = 7;

        // Creates a PDF document
        Document document = null;
        if (landScape == true)
        {
            // Sets the document to A4 size and rotates it so that the orientation of the page is Landscape.
            document = new Document(PageSize.A4.Rotate(), 0, 0, 15, 5);
        }
        else
        {
            document = new Document(PageSize.A4, 0, 0, 15, 5);
        }

        // Creates a PdfPTable with column count of the table equal to no of columns of the gridview or gridview datasource.
        iTextSharp.text.pdf.PdfPTable mainTable = new iTextSharp.text.pdf.PdfPTable(noOfColumns);

        // Sets the first 4 rows of the table as the header rows which will be repeated in all the pages.
        mainTable.HeaderRows = 4;

        // Creates a PdfPTable with 2 columns to hold the header in the exported PDF.
        iTextSharp.text.pdf.PdfPTable headerTable = new iTextSharp.text.pdf.PdfPTable(2);

        // Creates a phrase to hold the application name at the left hand side of the header.
        iTextSharp.text.Image jpg =
            iTextSharp.text.Image.GetInstance(Server.MapPath("../../..") + "/Images/LogoEmpresa.jpg");

        //Resize image depend upon your need
        jpg.ScaleToFit(120f, 120f);

        // Creates a phrase to show the current date at the right hand side of the header.
        Phrase phDate = new Phrase(DateTime.Now.Date.ToString("dd/MM/yyyy"),
            FontFactory.GetFont("Arial", ApplicationNameSize, Font.NORMAL));

        // Creates a PdfPCell which accepts the date phrase as a parameter.
        PdfPCell clDate = new PdfPCell(phDate);
        // Sets the Horizontal Alignment of the PdfPCell to right.
        clDate.HorizontalAlignment = Element.ALIGN_RIGHT;
        // Sets the border of the cell to zero.
        clDate.Border = PdfPCell.NO_BORDER;

        // Adds the cell which holds the application name to the headerTable.
        var imageCell = new PdfPCell(jpg);
        imageCell.HorizontalAlignment = Element.ALIGN_LEFT;
        imageCell.Border = PdfPCell.NO_BORDER;
        headerTable.AddCell(imageCell);
        // Adds the cell which holds the date to the headerTable.
        headerTable.AddCell(clDate);
        // Sets the border of the headerTable to zero.
        headerTable.DefaultCell.Border = PdfPCell.NO_BORDER;

        // Creates a PdfPCell that accepts the headerTable as a parameter and then adds that cell to the main PdfPTable.
        PdfPCell cellHeader = new PdfPCell(headerTable);
        cellHeader.Border = PdfPCell.NO_BORDER;
        // Sets the column span of the header cell to noOfColumns.
        cellHeader.Colspan = noOfColumns;
        // Adds the above header cell to the table.
        mainTable.AddCell(cellHeader);

        // Creates a phrase which holds the file name.
        Phrase phHeader = new Phrase(ddlConsultar.SelectedItem.Text,
            FontFactory.GetFont("Arial", ReportNameSize, Font.BOLD));
        PdfPCell clHeader = new PdfPCell(phHeader);
        clHeader.Colspan = noOfColumns;
        clHeader.Border = PdfPCell.NO_BORDER;
        clHeader.HorizontalAlignment = Element.ALIGN_CENTER;
        mainTable.AddCell(clHeader);

        // Creates a phrase for a new line.
        Phrase phSpace = new Phrase("\n");
        PdfPCell clSpace = new PdfPCell(phSpace);
        clSpace.Border = PdfPCell.NO_BORDER;
        clSpace.Colspan = noOfColumns;
        mainTable.AddCell(clSpace);

        // Sets the gridview column names as table headers.
        for (int i = 0; i < noOfColumns; i++)
        {
            Phrase ph = null;

            if (gvReport.AutoGenerateColumns)
            {
                ph = new Phrase(tbl.Columns[i].ColumnName,
                    FontFactory.GetFont("Arial", HeaderTextSize, Font.BOLD));
            }
            else
            {
                ph = new Phrase(gvReport.Columns[i].HeaderText,
                    FontFactory.GetFont("Arial", HeaderTextSize, Font.BOLD));
            }

            mainTable.AddCell(ph);
        }

        // Reads the gridview rows and adds them to the mainTable
        for (int rowNo = 0; rowNo < noOfRows; rowNo++)
        {
            for (int columnNo = 0; columnNo < noOfColumns; columnNo++)
            {
                if (gvReport.AutoGenerateColumns)
                {
                    string s = gvReport.Rows[rowNo].Cells[columnNo].Text.Trim();
                    Phrase ph = new Phrase(s, FontFactory.GetFont("Arial", ReportTextSize, Font.NORMAL));
                    mainTable.AddCell(ph);
                }
                else
                {
                    if (gvReport.Columns[columnNo] is TemplateField)
                    {
                        DataBoundLiteralControl lc =
                            gvReport.Rows[rowNo].Cells[columnNo].Controls[0] as DataBoundLiteralControl;
                        string s = lc.Text.Trim();
                        Phrase ph = new Phrase(s,
                            FontFactory.GetFont("Arial", ReportTextSize, Font.NORMAL));
                        mainTable.AddCell(ph);
                    }
                    else
                    {
                        string s = gvReport.Rows[rowNo].Cells[columnNo].Text.Trim();
                        Phrase ph = new Phrase(s,
                            FontFactory.GetFont("Arial", ReportTextSize, Font.NORMAL));
                        mainTable.AddCell(ph);
                    }
                }
            }

            // Tells the mainTable to complete the row even if any cell is left incomplete.
            mainTable.CompleteRow();
        }

        // Gets the instance of the document created and writes it to the output stream of the Response object.
        PdfWriter.GetInstance(document, Response.OutputStream);
        // Opens the document.
        document.Open();
        // Adds the mainTable to the document.
        document.Add(mainTable);
        // Closes the document.
        document.Close();

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment; filename= SampleExport.pdf");
        Response.End();
    }
}

public class GridViewItemTemplate : ITemplate
{
    private string columnName;

    public GridViewItemTemplate(string columnName)
    {
        this.columnName = columnName;
    }

    public void InstantiateIn(System.Web.UI.Control container)
    {
        Label lc = new Label();
        lc.ID = string.Format("lbl{0}", columnName);
        container.Controls.Add(lc);
    }

    //void lc_DataBinding(object sender, EventArgs e)
    //{
    //    Label l = (Label)sender; 
    //    GridViewRow row = (GridViewRow)l.NamingContainer; 
    //}

}
