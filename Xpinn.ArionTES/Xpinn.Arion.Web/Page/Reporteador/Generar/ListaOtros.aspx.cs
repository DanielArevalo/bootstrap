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
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    private void CargarDDList()
    {
        ddlConsultar.DataSource = reporteServicio.ListarReporteUsuario((Usuario)Session["Usuario"]);
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
        txtFecha.Text = "";
        lblTotalRegs.Text = "";
    }

    protected void gvReporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
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
            eReporte = reporteServicio.ConsultarReporte(pCodReporte, (Usuario)Session["Usuario"]);
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

        // Valida datos del reporte
        switch (pCodReporte)
        {
            case -1:
                if (txtFecha.Text == "")
                {
                    lblMensajeRep.Text = "falta fecha Cierre";
                    return;
                }
                Session.Add(reporteServicio.CodigoPrograma + ".consulta", 1);
                break;
        }

        // Determinar fecha del histórico
        DateTime fechahistorico = DateTime.Now;
        if (Convert.ToInt32(ddlConsultar.SelectedValue) == -1)
        {
            fechahistorico = new DateTime(2013, 8, 31);
            try
            {
                fechahistorico = Convert.ToDateTime(txtFecha.Text);
            }
            catch
            {
                VerError("Error al determinar la fecha del reporte");
                return;
            }
        }

        // Determinar parametros de entrada
        List<Parametros> lstParametros = new List<Parametros>();
        foreach (GridViewRow rFila in gvParametros.Rows)
        {
            Parametros eParametros = new Parametros();
            Label lblidParametro = (Label)rFila.FindControl("lblidParametro");
            Label lblDescripcion = (Label)rFila.FindControl("lblDescripcion");
            Label lblTipo = (Label)rFila.FindControl("lblTipo");
            TextBox txtValor = (TextBox)rFila.FindControl("txtValor");
            eParametros.idparametro = Convert.ToInt64(lblidParametro.Text);
            eParametros.descripcion = lblDescripcion.Text;
            eParametros.tipo = Convert.ToInt64(lblTipo.Text);
            eParametros.valor = txtValor.Text;
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
            dtReporte = reporteServicio.GenerarReporte(pCodReporte, fechahistorico, lstParametros, ref aColumnas, ref aTipos, ref numerocolumnas, ref sError, (Usuario)Session["Usuario"]);
            if (sError.Trim() != "")
                VerError(sError);
        }
        catch (Exception ex)
        {
            VerError("Error al generar el reporte. " + ex.Message);
            return;
        }
        BoundField[] ColumnBoundKAP = new BoundField[] { };
        int TotalWith = 0;
        for (int i = 0; i < numerocolumnas; i++)
        {
            if (aColumnas[i] != null)
            {
                Array.Resize(ref ColumnBoundKAP, i + 1);
                ColumnBoundKAP[i] = new BoundField();
                ColumnBoundKAP[i].HeaderText = aColumnas[i];
                ColumnBoundKAP[i].DataField = aColumnas[i];
                int largo = aColumnas[i].Length;
                if (largo < 10)
                    largo = 10;
                largo = largo * 8;
                //ColumnBoundKAP[i].ItemStyle.Width = largo;
                //ColumnBoundKAP[i].ControlStyle.Width = largo;
                //ColumnBoundKAP[i].HeaderStyle.Width = largo;
                if (aTipos[i] == typeof(System.DateTime))
                {
                    //ColumnBoundKAP[i].DataFormatString = "{0:d}";                    
                    TotalWith = TotalWith + largo;
                    ColumnBoundKAP[i].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (aTipos[i] == typeof(System.Decimal))
                {
                    //ColumnBoundKAP[i].DataFormatString = "{0:N}";
                    TotalWith = TotalWith + largo;
                    ColumnBoundKAP[i].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                }
                if (aTipos[i] == typeof(System.Int32))
                {
                    //ColumnBoundKAP[i].DataFormatString = "{0:F}";
                    TotalWith = TotalWith + largo;
                    ColumnBoundKAP[i].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                }
                if (aTipos[i] == typeof(System.String))
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
        gvReporte.ShowFooter = true;
        TotalWith = TotalWith + 100;
        gvReporte.Width = TotalWith;
        gvReporte.DataSource = dtReporte;
        lblTitulo.Text = eReporte.encabezado;
        int colInicial = 0;
        if (eReporte.lstColumnaReporte != null)
        {
            TotalWith = 0;
            int numcol = 0;
            foreach (ColumnaReporte eColumna in eReporte.lstColumnaReporte)
            {
                if (numcol <= gvReporte.Columns.Count)
                {
                    if (eColumna.titulo != null)
                        gvReporte.Columns[numcol + colInicial].HeaderText = eColumna.titulo;
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
        gvReporte.DataBind();
        // Mostrar totales
        if (eReporte.lstColumnaReporte != null)
        {
            int numcol = 0;
            foreach (ColumnaReporte eColumna in eReporte.lstColumnaReporte)
            {
                if (numcol <= gvReporte.Columns.Count)
                {
                    if (eColumna.total != null)
                    {
                        if (eColumna.total == true)
                        {
                            string nombreCol = "Sum(" + aColumnas[numcol] + ")";
                            object sumaCol = dtReporte.Compute(nombreCol, "");
                            gvReporte.FooterRow.Cells[numcol + colInicial].Text = Convert.ToDecimal(sumaCol).ToString("C2");
                            gvReporte.FooterRow.Cells[numcol + colInicial].HorizontalAlign = HorizontalAlign.Right;
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
            lblTotalRegs.Text = "<br/> Registros encontrados";
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
            if (Session["DTREPORTE"] == null)
            {
                lblMensajeRep.Text = "No se han cargado los datos del reporte";
            }
            lblMensajeRep.Text = "";
            gvReporte.EmptyDataText = emptyQuery;
            DataTable dtReporte = new DataTable();
            dtReporte = (DataTable)Session["DTREPORTE"];
            gvReporte.DataSource = dtReporte;
            if (dtReporte.Rows.Count > 0)
            {
                mvReporte.SetActiveView(vGridReporte);
                lblTotalRegs.Text = "<br/> Registros encontrados";
                gvReporte.DataBind();
                ValidarPermisosGrilla(gvReporte);
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
        if (gvReporte.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvReporte.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvReporte);
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
        //Creo el documento PDF y el archivo a escribir.
        //string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
        string fileName = Server.MapPath("Documentos\\" + Guid.NewGuid().ToString() + ".pdf");
        VerError(fileName);
        Document document = new Document(PageSize.LETTER, 50, 50, 25, 25);
        PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
        document.Open();
        document.AddHeader("EMPRENDER", "FUNDACION EMPRENDER");

        //PARRAFO
        Paragraph unParrafo = GenerarParrafo(lblTitulo.Text);
        document.Add(unParrafo);

        //GENERO Y AGREGO SALTO DE LINEA
        document.Add(new Paragraph(" "));

        //GENERO Y AGREGO SALTO DE LINEA
        document.Add(new Paragraph(" "));

        //TABLA
        PdfPTable unaTabla = GenerarTabla();
        document.Add(unaTabla);

        //CIERRO DOCUMENTO
        document.Close();

        //LO EJECUTO
        Process prc = new System.Diagnostics.Process();
        prc.StartInfo.FileName = fileName;

        prc.Start();

        try
        {
            //FileStream fs = File.OpenRead(fileName);
            //byte[] data = new byte[fs.Length];
            //fs.Read(data, 0, (int)fs.Length);
            //fs.Close();
            //// Determinar configuraciòn del tipo de archivo
            //Response.Buffer = true;
            //Response.Clear();
            //Response.ContentType = "application/pdf";
            //// Mostrar el archivo
            //Response.BinaryWrite(data);
            //Response.End();
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = false;
            Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            Response.ContentType = "application/octet-stream";
            Response.Flush();
            Response.WriteFile(fileName);
            Response.End();
        }
        catch
        {
            VerError("Error al generar PDF");
        }
    }

    public Paragraph GenerarParrafo(string pTexto)
    {

        //Creamos un nuevo parrafo, por ejemplo el titulo de nuestro reporte
        //Y le damos algunas propiedades.
        Paragraph parrafo = new Paragraph();

        parrafo.Alignment = Element.ALIGN_CENTER;
        parrafo.Font = FontFactory.GetFont("Arial", 8);
        parrafo.Font.SetStyle(Font.BOLD);
        parrafo.Font.SetStyle(Font.UNDERLINE);
        parrafo.Add(pTexto);

        return parrafo;
    }

    public PdfPTable GenerarTabla()
    {
        int numCol = gvReporte.Columns.Count;
        int numFil = gvReporte.Rows.Count;
        float[] tamCol = new float[numCol];

        for (int i = 1; i <= numCol; i++)
        {
            tamCol[i - 1] = Convert.ToInt64(gvReporte.Columns[i - 1].ItemStyle.Width.Value);
        }

        PdfPTable unaTabla = new PdfPTable(numCol);
        unaTabla.SetWidths(tamCol);

        //Headers
        for (int i = 1; i <= numCol; i++)
        {
            unaTabla.AddCell(new Paragraph(gvReporte.HeaderRow.Cells[i - 1].Text, FontFactory.GetFont("Arial", 6)));
        }

        //¿Le damos un poco de formato?        
        //foreach (PdfPCell celda in unaTabla.Rows[0].GetCells())
        //{
        //    celda.BackgroundColor = BaseColor.LIGHT_GRAY;
        //    celda.HorizontalAlignment = 1;
        //    celda.Padding = 3;
        //}

        for (int i = 1; i <= numFil; i++)
        {
            for (int j = 1; j <= numCol; j++)
            {
                PdfPCell celda = new PdfPCell(new Paragraph(gvReporte.Rows[i - 1].Cells[j - 1].Text, FontFactory.GetFont("Arial", 6)));
                unaTabla.AddCell(celda);
            }
        }

        return unaTabla;

    }

    protected void gvParametros_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Al actualizar el gridview, cambia el codigo de la columna "Modelo" por la respectiva descripcion
            TextBox txtValor = (TextBox)e.Row.FindControl("txtValor");
            Label lblTipo = (Label)e.Row.FindControl("lblTipo");
            AjaxControlToolkit.FilteredTextBoxExtender ftbetxtValor = (AjaxControlToolkit.FilteredTextBoxExtender)e.Row.FindControl("ftbetxtValor");
            AjaxControlToolkit.CalendarExtender ceTxtValor = (AjaxControlToolkit.CalendarExtender)e.Row.FindControl("ceTxtValor");
            RequiredFieldValidator rfvTxtValor = (RequiredFieldValidator)e.Row.FindControl("rfvTxtValor");
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

    public void ActualizarPar()
    {
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
            {   //Permite visualizar el footer del gridview cuando no hay ningun registro para mostrar
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

}
