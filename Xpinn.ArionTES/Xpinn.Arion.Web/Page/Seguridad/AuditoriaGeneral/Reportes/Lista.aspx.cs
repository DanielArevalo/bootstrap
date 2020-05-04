using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Seguridad.Services;
using Xpinn.Seguridad.Entities;
using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Configuration;

public partial class Page_Asesores_Colocacion_Lista : GlobalWeb
{

    private Xpinn.Seguridad.Services.Rep_AuditoriaService ReporteaudServicio = new Xpinn.Seguridad.Services.Rep_AuditoriaService();

    Usuario usuario = new Usuario();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ReporteaudServicio.CodigoPrograma, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteaudServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            this.Panel4.Visible = false;

            this.Panel5.Visible = false;

            this.Panel6.Visible = false;

            this.Panel7.Visible = false;

            if (!IsPostBack)
            {

                CargarValoresConsulta(pConsulta, ReporteaudServicio.CodigoPrograma);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                LlenarComboReporte();
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteaudServicio.CodigoPrograma, "Page_Load", ex);
        }
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
            GuardarValoresConsulta(pConsulta, ReporteaudServicio.CodigoPrograma);
            Actualizar();

        }
    }

    /// <summary>
    /// Méotodo para limpiar datos de la conuslta
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ReporteaudServicio.CodigoPrograma);
        gvReportePersonas.DataSource = null;
        GvpreporteCreditos.DataSource = null;

    }



    /// <summary>
    /// Método para traer los datos según las condiciones ingresadas
    /// </summary>
    private void Actualizar()
    {
        try
        {
            Usuario usuap = (Usuario)Session["usuario"];

            // Determinar el còdigo del usuario y determinar si el usuario es un ejecutivo
            int cod = Convert.ToInt32(usuap.codusuario);
            int oficinaus = Convert.ToInt32(usuap.cod_oficina);
            int perfil = Convert.ToInt32(usuap.codperfil);

            // Generar el reporte


            if (ddlConsultar.SelectedIndex == 1)
            {


                Panel4.Visible = true;
                Panel5.Visible = false;
                Panel6.Visible = false;
                Panel7.Visible = false;
                this.LabelCliente.Visible = true;
            }


            if (ddlConsultar.SelectedIndex == 2)
            {
                Panel4.Visible = false;
                Panel5.Visible = true;
                Panel6.Visible = false;
                Panel7.Visible = false;
                this.LabelRadicado.Visible = true;
            }
            if (ddlConsultar.SelectedIndex == 3)
            {
                Panel4.Visible = false;
                Panel5.Visible = false;
                Panel6.Visible = true;
                Panel7.Visible = false;
                this.LabelOperacion.Visible = true;
            }
            if (ddlConsultar.SelectedIndex == 4)
            {
                Panel4.Visible = false;
                Panel5.Visible = false;
                Panel6.Visible = false;
                Panel7.Visible = true;
                this.LabelComprobante.Visible = true;
            }

            int tipoReporte = Convert.ToInt32(ddlConsultar.SelectedValue);
            switch (tipoReporte)
            {
                case 1:

                    List<ReporteAuditoria> lstConsultaPersonas = new List<ReporteAuditoria>();

                    if (string.IsNullOrWhiteSpace(txtIdentificacion.Text))
                    {
                        VerError("Filtra por una identificacion");
                        return;
                    }

                    lstConsultaPersonas = ReporteaudServicio.ListarRep_aud_persona(Convert.ToInt64(txtIdentificacion.Text), (Usuario)Session["usuario"]);
                    gvReportePersonas.EmptyDataText = emptyQuery;
                    gvReportePersonas.DataSource = lstConsultaPersonas;
                    if (lstConsultaPersonas.Count > 0)
                    {
                        mvLista.SetActiveView(vGridReportePersonas);
                        lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaPersonas.Count.ToString();
                        gvReportePersonas.DataBind();
                        ValidarPermisosGrilla(gvReportePersonas);
                    }
                    else
                    {
                        mvLista.ActiveViewIndex = -1;
                    }

                    Session.Add(ReporteaudServicio.CodigoPrograma + ".consulta", 1);

                    break;

                case 2:

                    List<ReporteAuditoria> lstConsultareportecreditos = new List<ReporteAuditoria>();
                    long oficina = (usuap.cod_oficina);

                    if (string.IsNullOrWhiteSpace(txtRadicado.Text))
                    {
                        VerError("Filtra por un radicado");
                        return;
                    }

                    lstConsultareportecreditos = ReporteaudServicio.ListarRep_aud_credito(Convert.ToInt64(txtRadicado.Text), (Usuario)Session["usuario"]);
                    GvpreporteCreditos.EmptyDataText = emptyQuery;
                    GvpreporteCreditos.DataSource = lstConsultareportecreditos;
                    if (lstConsultareportecreditos.Count > 0)
                    {
                        mvLista.SetActiveView(VGreporteCreditos);
                        lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultareportecreditos.Count.ToString();
                        GvpreporteCreditos.DataBind();
                        ValidarPermisosGrilla(GvpreporteCreditos);
                    }
                    else
                    {
                        mvLista.ActiveViewIndex = -1;
                    }

                    break;

                case 3:

                    List<ReporteAuditoria> lstConsultareporteOperacion = new List<ReporteAuditoria>();

                    if (string.IsNullOrWhiteSpace(txtOperacion.Text))
                    {
                        VerError("Filtra por una operacion");
                        return;
                    }

                    lstConsultareporteOperacion = ReporteaudServicio.ListarRep_aud_operacion(Convert.ToInt64(txtOperacion.Text), (Usuario)Session["usuario"]);
                    gvReporteOperacion.EmptyDataText = emptyQuery;
                    gvReporteOperacion.DataSource = lstConsultareporteOperacion;
                    if (lstConsultareporteOperacion.Count > 0)
                    {
                        mvLista.SetActiveView(VGridReporteOperacion);
                        lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultareporteOperacion.Count.ToString();
                        gvReporteOperacion.DataBind();
                        ValidarPermisosGrilla(gvReporteOperacion);
                    }
                    else
                    {
                        mvLista.ActiveViewIndex = -1;
                    }

                    break;

                case 4:

                    List<ReporteAuditoria> lstConsultareportecomprobante = new List<ReporteAuditoria>();
                    string tipo = ddlTipoComprobante.SelectedValue;

                    if (string.IsNullOrWhiteSpace(txtComprobante.Text))
                    {
                        VerError("Filtra por un comprobante");
                        return;
                    }

                    lstConsultareportecomprobante = ReporteaudServicio.ListarRep_aud_comprobante(Convert.ToInt64(txtComprobante.Text), tipo, (Usuario)Session["usuario"]);
                    gvReporteComprobante.EmptyDataText = emptyQuery;
                    gvReporteComprobante.DataBind();
                    gvReporteComprobante.DataSource = null;
                    gvReporteComprobante.Columns[1].Visible = true;
                    gvReporteComprobante.Columns[5].Visible = true;
                    gvReporteComprobante.Columns[6].Visible = true;
                    gvReporteComprobante.Columns[7].Visible = true;
                    gvReporteComprobante.Columns[8].Visible = true;
                    gvReporteComprobante.Columns[15].Visible = true;
                    gvReporteComprobante.Columns[14].Visible = true;
                    gvReporteComprobante.DataSource = lstConsultareportecomprobante;
                    if (lstConsultareportecomprobante.Count > 0)
                    {
                        mvLista.SetActiveView(VGridReporteComprobante);
                        lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultareportecomprobante.Count.ToString();
                        gvReporteComprobante.DataBind();
                        ValidarPermisosGrilla(gvReporteComprobante);
                        if (tipo == "1")
                        {
                            gvReporteComprobante.Columns[15].Visible = false;
                            gvReporteComprobante.Columns[14].Visible = false;
                        }
                        else
                        {
                            gvReporteComprobante.Columns[15].Visible = true;
                            gvReporteComprobante.Columns[14].Visible = true;
                        }
                        if (tipo != "1" || tipo != "5")
                        {
                            gvReporteComprobante.Columns[8].Visible = false;
                            gvReporteComprobante.Columns[5].Visible = false;
                            gvReporteComprobante.Columns[6].Visible = false;
                            gvReporteComprobante.Columns[7].Visible = false;
                        }
                        else
                        {
                            gvReporteComprobante.Columns[8].Visible = true;
                            gvReporteComprobante.Columns[5].Visible = true;
                        }
                        if (tipo == "5")
                        {
                            gvReporteComprobante.Columns[6].Visible = false;
                            gvReporteComprobante.Columns[7].Visible = true;
                        }
                        else if (tipo == "1")
                        {
                            gvReporteComprobante.Columns[6].Visible = true;
                            gvReporteComprobante.Columns[7].Visible = false;
                        }
                        if (tipo == "1" || tipo == "5")
                            gvReporteComprobante.Columns[1].Visible = false;
                        else
                            gvReporteComprobante.Columns[1].Visible = true;
                    }
                    else
                    {
                        mvLista.ActiveViewIndex = -1;
                    }

                    break;
                case 5:
                    //Filta por tipo de auditoria 5 (Afiliaciones)
                    List<Auditoria> listaAuditoriaAfil = ReporteaudServicio.ListarAuditoriaDeTablaAuditoria(tipoReporte, Usuario);
                    gvAuditoriaUsuarios.DataSource = listaAuditoriaAfil;
                    gvAuditoriaUsuarios.DataBind();
                    mvLista.SetActiveView(viewAuditoriaUsuarios);
                    break;
                case 6:
                    //Filta por tipo de auditoria 5 (Cruce de cuentas)
                    List<Auditoria> listaAuditoriaCruce = ReporteaudServicio.ListarAuditoriaDeTablaAuditoria(tipoReporte, Usuario);
                    gvAuditoriaUsuarios.DataSource = listaAuditoriaCruce;
                    gvAuditoriaUsuarios.DataBind();
                    mvLista.SetActiveView(viewAuditoriaUsuarios);
                    break;
                case 7:
                    GenerarReporte("AUD_APORTE");
                    mvLista.SetActiveView(ViewDinamica);
                    break;
                case 8:
                    GenerarReporte("AUD_CREDITO");
                    mvLista.SetActiveView(ViewDinamica);
                    break;
                case 9:
                    GenerarReporte("AUD_AHORRO_VISTA");
                    mvLista.SetActiveView(ViewDinamica);
                    break;
                case 10:
                    GenerarReporte("AUD_CDAT");
                    mvLista.SetActiveView(ViewDinamica);
                    break;
                case 11:
                    GenerarReporte("AUD_PROGRAMADO");
                    mvLista.SetActiveView(ViewDinamica);
                    break;
                case 12:
                    GenerarReporte("AUD_SERVICIOS");
                    mvLista.SetActiveView(ViewDinamica);
                    break;
                case 13:
                    GenerarReporte("AUD_AUXILIOS");
                    mvLista.SetActiveView(ViewDinamica);
                    break;
                case 14:
                case 15:
                case 16:
                    GenerarReporte("AUD_GIRO");
                    mvLista.SetActiveView(ViewDinamica);
                    break;
                case 17:
                case 18:
                    List<Auditoria> listaAuditoria = ReporteaudServicio.ListarAuditoriaDeTablaAuditoria(tipoReporte, Usuario);
                    gvAuditoriaUsuarios.DataSource = listaAuditoria;
                    gvAuditoriaUsuarios.DataBind();
                    mvLista.SetActiveView(viewAuditoriaUsuarios);
                    break;
            }
        }

        catch (Exception ex)
        {
            VerError(ex.ToString());
        }
    }

    private Xpinn.Seguridad.Entities.ReporteAuditoria ObtenerValores()
    {
        Xpinn.Seguridad.Entities.ReporteAuditoria vReporte = new Xpinn.Seguridad.Entities.ReporteAuditoria();


        if (txtIdentificacion.Text.Trim() != "")
            vReporte.identificacion = Convert.ToString(txtIdentificacion.Text.Trim());

        return vReporte;
    }


    /// <summary>
    /// Método para llenar el combo de reporte 
    /// </summary>
    /// <param name="Ddloficinas"></param>
    protected void LlenarComboReporte()
    {

        Rep_AuditoriaService ReporteService = new Rep_AuditoriaService();
        ReporteAuditoria reporte = new ReporteAuditoria();
        ddlConsultar.DataSource = ReporteService.ConsultarReporte(reporte, (Usuario)Session["usuario"]);
        ddlConsultar.DataTextField = "descripcion";
        ddlConsultar.DataValueField = "idreporte";
        ddlConsultar.DataBind();
        ddlConsultar.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

        Xpinn.Contabilidad.Services.TipoComprobanteService TipoComprobanteService = new Xpinn.Contabilidad.Services.TipoComprobanteService();
        Xpinn.Contabilidad.Entities.TipoComprobante TipoComprobante = new Xpinn.Contabilidad.Entities.TipoComprobante();
        ddlTipoComprobante.DataSource = TipoComprobanteService.ListarTipoComprobante(TipoComprobante, "", (Usuario)Session["usuario"]);
        ddlTipoComprobante.DataTextField = "descripcion";
        ddlTipoComprobante.DataValueField = "tipo_comprobante";
        ddlTipoComprobante.DataBind();
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

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (gvReportePersonas.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();

            gvReportePersonas.EnableViewState = false;
            pagina.EnableEventValidation = false;

            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvReportePersonas);
            //gvReoirtemora.HeaderRow.Cells[1].Visible = false;
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=repaudper.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else if (gvReporteOperacion.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();

            gvReportePersonas.EnableViewState = false;
            pagina.EnableEventValidation = false;

            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvReporteOperacion);
            //gvReoirtemora.HeaderRow.Cells[1].Visible = false;
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=RepOperacion.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else if (gvReporteComprobante.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();

            gvReporteComprobante.EnableViewState = false;
            pagina.EnableEventValidation = false;

            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvReporteComprobante);
            //gvReoirtemora.HeaderRow.Cells[1].Visible = false;
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=RepComprobante.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else if (gvAuditoriaUsuarios.Rows.Count > 0)
        {
            ExportarGridViewEnExcel(gvAuditoriaUsuarios);
        }
        else if (gvDinamica.Rows.Count > 0)
        {
            ExportarGridViewEnExcel(gvDinamica);
        }
        else
            VerError("Se debe generar el reporte primero");
    }



    protected void ddlConsultar_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlConsultar.SelectedValue == "1")
        {


            Panel4.Visible = true;
            Panel5.Visible = false;
            Panel6.Visible = false;
            Panel7.Visible = false;
            this.LabelCliente.Visible = true;
        }


        if (ddlConsultar.SelectedValue == "2")
        {


            Panel4.Visible = false;
            Panel5.Visible = true;
            Panel6.Visible = false;
            Panel7.Visible = false;
            this.LabelRadicado.Visible = true;
        }
        if (ddlConsultar.SelectedValue == "3")
        {


            Panel4.Visible = false;
            Panel5.Visible = false;
            Panel6.Visible = true;
            Panel7.Visible = false;
            this.LabelOperacion.Visible = true;
        }
        if (ddlConsultar.SelectedValue == "4")
        {


            Panel4.Visible = false;
            Panel5.Visible = false;
            Panel6.Visible = false;
            Panel7.Visible = true;
            this.LabelComprobante.Visible = true;
        }


        mvLista.ActiveViewIndex = -1;
    }

    protected void ddlAsesores_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void Gvpreportepolizas_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }

    protected void Gvcolocacionejecutivo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    Int64 subtotalnumerocreditos = 0;
    decimal subtotalsaldocierre = 0;
    Int64 subtotalcolocacion_mes = 0;
    decimal subtotalmonto_colocacion_mes = 0;
    Int64 subtotalmora = 0;
    decimal subtotalsaldo_mora = 0;
    Int64 subtotalmora_menor_30 = 0;
    decimal subtotalmonto_menor_30 = 0;
    Int64 subtotalmora_mayor_30 = 0;
    decimal subtotalmonto_mayor_30 = 0;


    protected void GvCarteracierre_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            subtotalnumerocreditos += Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "numero_credito"));
            subtotalsaldocierre += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldo_cierre"));
            subtotalcolocacion_mes += Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "numero_colocacion_mes"));
            subtotalmonto_colocacion_mes += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "monto_colocacion_mes"));
            subtotalmora += Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "total_mora"));
            subtotalsaldo_mora += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldo_mora"));
            subtotalmora_menor_30 += Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "mora_menor_30"));
            subtotalmonto_menor_30 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "monto_menor_30"));
            subtotalmora_mayor_30 += Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "mora_mayor_30"));
            subtotalmonto_mayor_30 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "monto_mayor_30"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = "Total:";
            e.Row.Cells[5].Text = subtotalnumerocreditos.ToString("d");
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].Text = subtotalsaldocierre.ToString("c");
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[7].Text = subtotalcolocacion_mes.ToString("d");
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[8].Text = subtotalmonto_colocacion_mes.ToString("c");
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[9].Text = subtotalmora.ToString("d");
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[10].Text = subtotalsaldo_mora.ToString("c");
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[11].Text = subtotalmora_menor_30.ToString("d");
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[12].Text = subtotalmonto_menor_30.ToString("c");
            e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[13].Text = subtotalmora_mayor_30.ToString("d");
            e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[14].Text = subtotalmonto_mayor_30.ToString("c");
            e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Font.Bold = true;
        }
    }

    Int64 subtotalconteo = 0;
    decimal subtotalvalordesembolsado = 0;

    protected void Gvcolocacionejecutivo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            subtotalconteo += Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "cuenta"));
            subtotalvalordesembolsado += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "monto_pago"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = "Total:";
            e.Row.Cells[4].Text = subtotalvalordesembolsado.ToString("c");
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[5].Text = subtotalconteo.ToString("d");
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Font.Bold = true;
        }
    }

    decimal subtotalgarantiascomunitarias = 0;
    decimal subtotalsaldo_capital = 0;
    decimal subtotalvalor_cuota = 0;
    decimal subtotalpendite_cuota = 0;



    protected void gvReportePersonas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            subtotalgarantiascomunitarias += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "garantia_comunitaria"));
            subtotalsaldo_capital += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldo_capital"));
            subtotalvalor_cuota += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "valor_cuota"));
            subtotalpendite_cuota += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pendite_cuota"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = "Total:";

            e.Row.Cells[6].Text = subtotalvalor_cuota.ToString("c");
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[8].Text = subtotalsaldo_capital.ToString("c");
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[9].Text = subtotalgarantiascomunitarias.ToString("c");
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[10].Text = subtotalpendite_cuota.ToString("c");
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Font.Bold = true;
        }
    }
    protected void btnExportarpoliza_Click(object sender, EventArgs e)
    {
        if (GvpreporteCreditos.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();

            GvpreporteCreditos.EnableViewState = false;
            pagina.EnableEventValidation = false;

            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(GvpreporteCreditos);
            //gvReoirtemora.HeaderRow.Cells[1].Visible = false;
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=repaudcred.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }

    public void GenerarReporte(string tablaAuditoria)
    {
        gvDinamica.Columns.Clear();
        DataTable dtReporteDinamico = new DataTable();
        string[] aColumnas = new string[] { };
        System.Type[] aTipos = new System.Type[] { };
        int numerocolumnas = 0;
        dtReporteDinamico.Clear();
        string sError = "";
        try
        {
            dtReporteDinamico = ReporteaudServicio.GenerarReporte(tablaAuditoria, ref aColumnas, ref aTipos, ref numerocolumnas, ref sError, (Usuario)Session["Usuario"]);
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
                if (aTipos[i] == typeof(System.DateTime))
                {
                    TotalWith = TotalWith + largo;
                    ColumnBoundKAP[i].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (aTipos[i] == typeof(System.Decimal))
                {
                    TotalWith = TotalWith + largo;
                    ColumnBoundKAP[i].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                }
                if (aTipos[i] == typeof(System.Int32))
                {
                    TotalWith = TotalWith + largo;
                    ColumnBoundKAP[i].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                }
                if (aTipos[i] == typeof(System.String))
                {
                    TotalWith = TotalWith + 300;
                    ColumnBoundKAP[i].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                }
                gvDinamica.Columns.Add(ColumnBoundKAP[i]);
            }
        }
        Session["DTPREPORTE"] = dtReporteDinamico;
        gvDinamica.ShowFooter = false;
        TotalWith = TotalWith + 100;
        gvDinamica.Width = TotalWith;
        //gvDinamica.Height = 800;
        gvDinamica.DataSource = dtReporteDinamico;
        gvDinamica.DataBind();
        int colInicial = 0;        
    }
}
