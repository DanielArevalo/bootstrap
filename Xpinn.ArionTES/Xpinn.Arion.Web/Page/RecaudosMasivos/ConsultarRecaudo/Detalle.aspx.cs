using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Data;
using Microsoft.Reporting.WebForms;


public partial class Detalle : GlobalWeb
{
    public int ancho = 1200;
    Usuario usuario = new Usuario();    
    Xpinn.Tesoreria.Services.RecaudosMasivosService RecaudosMasivosServicio = new Xpinn.Tesoreria.Services.RecaudosMasivosService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(RecaudosMasivosServicio.CodigoProgramaConsulta, "L");

            Site toolBar = (Site)this.Master;            
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ancho = cbDetallado.Checked ? 1200 : 900;
            if (!IsPostBack)
            {
                CargarEmpresaRecaudo();
                mvAplicar.ActiveViewIndex = 0;
                if (Session[RecaudosMasivosServicio.CodigoProgramaConsulta + ".id"] != null)
                {
                    idObjeto = Session[RecaudosMasivosServicio.CodigoProgramaConsulta + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                }
            }
            msg.Text = "";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.GetType().Name + "L", "Page_Load", ex);
        }

    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.CodigoProgramaConsulta + "L", "gvLista_RowDeleting", ex);
        }
    }

    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs evt)
    {
        if (evt.CommandName == "Editar")
        {
            String[] tmp = evt.CommandArgument.ToString().Split('|');
            RecaudosMasivos ejeMeta = new RecaudosMasivos();
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            ancho = cbDetallado.Checked ? 1200 : 800;
            gvLista.Columns[7].Visible = cbDetallado.Checked;
            gvLista.Columns[8].Visible = cbDetallado.Checked;
            gvLista.Columns[9].Visible = cbDetallado.Checked;
            gvLista.Columns[10].Visible = cbDetallado.Checked;
            gvLista.Columns[11].Visible = cbDetallado.Checked;
            gvLista.Columns[12].Visible = cbDetallado.Checked;
            gvLista.Columns[13].Visible = cbDetallado.Checked;
            gvLista.Columns[14].Visible = cbDetallado.Checked;
            List<RecaudosMasivos> lstConsulta = new List<RecaudosMasivos>();
            lstConsulta = RecaudosMasivosServicio.ListarDetalleRecaudoConsulta(Convert.ToInt32(txtNumeroLista.Text), cbDetallado.Checked, (Usuario)Session["Usuario"]);
            lblTotalReg.Visible = true;
            lblTotalReg.Text = "Se encontraron " + lstConsulta.Count() + " registros";
            gvLista.DataSource = lstConsulta;
            gvLista.DataBind();
            Session["DatosGrilla"] = lstConsulta;
            TotalizarGridView(lstConsulta);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    protected void TotalizarGridView(List<RecaudosMasivos> lstInfo)
    {
        decimal totalRev = 0;
        if (lstInfo != null && lstInfo.Count > 0)
        {
            totalRev = lstInfo.Sum(x => x.valor);;
        }
        lblTotalRecaudo.Text = totalRev.ToString("n2");
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            RecaudosMasivos vRecaudos = new RecaudosMasivos();
            vRecaudos = RecaudosMasivosServicio.ConsultarRecaudo(pIdObjeto, (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vRecaudos.numero_recaudo.ToString()))
                txtNumeroLista.Text = HttpUtility.HtmlDecode(vRecaudos.numero_recaudo.ToString().Trim());
            if (!string.IsNullOrEmpty(vRecaudos.fecha_aplicacion.ToString()))
                ucFechaAplicacion.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vRecaudos.fecha_aplicacion.ToString()));
            if (!string.IsNullOrEmpty(vRecaudos.cod_empresa.ToString()))
                ddlEntidad.SelectedValue = HttpUtility.HtmlDecode(vRecaudos.cod_empresa.ToString());
            if (!string.IsNullOrEmpty(vRecaudos.numero_novedad.ToString()))
                txtNumeroNovedad.Text = HttpUtility.HtmlDecode(vRecaudos.numero_novedad.ToString().Trim());
            if (!string.IsNullOrEmpty(vRecaudos.periodo_corte.ToString()))
                txtPeriodo.Text = HttpUtility.HtmlDecode(vRecaudos.periodo_corte.ToString().Trim());
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.CodigoProgramaConsulta, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

   
    protected void CargarEmpresaRecaudo()
    {
        try
        {
            Xpinn.Tesoreria.Services.RecaudosMasivosService recaudoServicio = new Xpinn.Tesoreria.Services.RecaudosMasivosService();
            List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstModulo = new List<Xpinn.Tesoreria.Entities.EmpresaRecaudo>();

            lstModulo = recaudoServicio.ListarEmpresaRecaudo(null, (Usuario)Session["usuario"]);

            ddlEntidad.DataSource = lstModulo;
            ddlEntidad.DataTextField = "nom_empresa";
            ddlEntidad.DataValueField = "cod_empresa";
            ddlEntidad.DataBind();

            ddlEntidad.Items.Insert(0, "");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.CodigoProgramaConsulta, "CargarEmpresaRecaudo", ex);
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvLista);
    }

    

    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=AplicacionRecaudosMasivos.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        ExpGrilla expGrilla = new ExpGrilla();

        sw = expGrilla.ObtenerGrilla(GridView1, null);

        Response.Write(expGrilla.style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }



    protected void btnDatos_Click(object sender, EventArgs e)
    {
        mvAplicar.ActiveViewIndex = 0;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarCancelar(true);
    }

    protected void btnReporte_Click(object sender, EventArgs e)
    {
        VerError("");

        List<RecaudosMasivos> lstConsulta = new List<RecaudosMasivos>();
        lstConsulta = (List<RecaudosMasivos>)Session["DatosGrilla"];

        if (gvLista.Rows.Count > 0 && lstConsulta.Count > 0)
        {
            //CREACION DE LA TABLA
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("cedula");
            table.Columns.Add("nombre");
            table.Columns.Add("tipo_producto");
            table.Columns.Add("num_producto");
            table.Columns.Add("tipo_aplicacion");
            table.Columns.Add("nro_cuotas");
            table.Columns.Add("valor");
            table.Columns.Add("capital");
            table.Columns.Add("int_cte");
            table.Columns.Add("int_mora");
            table.Columns.Add("seguro");
            table.Columns.Add("ley_mypime");
            table.Columns.Add("ivaley_mypime");
            table.Columns.Add("devolucion");

            //LLENAR LAS TABLAS CON LOS DATOS CORRESPONDIENTES

            foreach (RecaudosMasivos rFila in lstConsulta)
            {
                DataRow dr;
                dr = table.NewRow();
                dr[0] = " " + rFila.identificacion;
                dr[1] = " " + rFila.nombre;
                dr[2] = " " + rFila.tipo_producto;
                dr[3] = " " + rFila.numero_producto;
                dr[4] = " " + rFila.tipo_aplicacion;
                dr[5] = " " + rFila.num_cuotas;
                dr[6] = " " + rFila.valor.ToString("n");
                dr[7] = " " + rFila.capital;
                dr[8] = " " + rFila.intcte;
                dr[9] = " " + rFila.intmora;
                dr[10] = " " + rFila.seguro;
                dr[11] = " " + rFila.leymipyme;
                dr[12] = " " + rFila.ivaleymipyme;
                dr[13] = " " + rFila.devolucion.ToString("n");

                table.Rows.Add(dr);
            }
            
            //PASAR LOS DATOS AL REPORTE
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            ReportParameter[] param = new ReportParameter[3];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);
            param[2] = new ReportParameter("fecha", DateTime.Now.ToShortDateString());
            rvReporte.LocalReport.SetParameters(param);

            ReportDataSource rds = new ReportDataSource("DataSet1", table);

            rvReporte.LocalReport.DataSources.Clear();
            rvReporte.LocalReport.DataSources.Add(rds);
            rvReporte.LocalReport.Refresh();

            Site toolBar = (Site)this.Master;
            toolBar.MostrarCancelar(false);

            mvAplicar.ActiveViewIndex = 1;
            rvConsultaRecaudo.Visible = false;
            rvReporte.Visible = true;
            rvConsolidado.Visible = false;
        }
        else
        {
            VerError("No existen Datos");
        }
    }


    protected void btnRptConsolidado_Click(object sender, EventArgs e)
    {
        VerError("");

        if (txtNumeroLista.Text == "")
        {
            VerError("Error al generar el reporte consolidado, Verifique los datos.");
            return;
        }

        List<RecaudosMasivos> lstData = new List<RecaudosMasivos>();
        RecaudosMasivos pEntidad = new RecaudosMasivos();
        pEntidad.numero_recaudo = Convert.ToInt64(txtNumeroLista.Text);

        string pError = "";
        lstData = RecaudosMasivosServicio.ListarTEMP_Consolidado(pEntidad, ref pError, (Usuario)Session["usuario"]);
        if (pError != "")
        {
            VerError(pError);
            return;
        }

        DataTable table = new DataTable();
        table.Columns.Add("Producto");
        table.Columns.Add("Concepto");
        table.Columns.Add("NombreConc");
        table.Columns.Add("Atributo");
        table.Columns.Add("Oficina");
        table.Columns.Add("Valor");

        if (lstData.Count > 0)
        {
            //LLENANDO EL DATATABLE
            foreach (RecaudosMasivos pData in lstData)
            {
                DataRow dr;
                dr = table.NewRow();
                dr[0] = " " + pData.nom_tipo_producto;
                dr[1] = " " + pData.nombre;
                dr[2] = " " + pData.nombres;
                dr[3] = " " + pData.nom_atr;
                dr[4] = " " + pData.nom_oficina;
                dr[5] = pData.valor.ToString("n0");
                table.Rows.Add(dr);
            }

            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            ReportParameter[] param = new ReportParameter[8];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);
            param[2] = new ReportParameter("ImagenReport", ImagenReporte());
            param[3] = new ReportParameter("NroLista", " " + txtNumeroLista.Text);
            param[4] = new ReportParameter("Periodo", " " + txtPeriodo.Text.Substring(0, 10));
            if (ddlEntidad.SelectedItem != null)
                param[5] = new ReportParameter("Pagaduria", " " + ddlEntidad.SelectedItem.Text);
            else
                param[5] = new ReportParameter("Pagaduria", " ");
            param[6] = new ReportParameter("FechaAplicacion", " " + ucFechaAplicacion.Text);
            param[7] = new ReportParameter("nomUsuario", " " + pUsuario.nombre);

            rvConsolidado.LocalReport.DataSources.Clear();
            rvConsolidado.LocalReport.EnableExternalImages = true;
            rvConsolidado.LocalReport.SetParameters(param);
            ReportDataSource rds = new ReportDataSource("DataSet1", table);
            rvConsolidado.LocalReport.DataSources.Add(rds);
            rvConsolidado.LocalReport.Refresh();

            Site toolBar = (Site)this.Master;
            toolBar.MostrarCancelar(false);

            mvAplicar.ActiveViewIndex = 1;
            rvConsultaRecaudo.Visible = false;
            rvReporte.Visible = false;
            rvConsolidado.Visible = true;
        }
        else
        {
            VerError("No existen Datos");
        }
    }

    protected void btnConciliacion_Click(object sender, EventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0)
        {
            //RECUPERAR DATOS

            List<RecaudosMasivos> lstDetalle = new List<RecaudosMasivos>();
            if (txtNumeroNovedad.Text == "")
                txtNumeroNovedad.Text = "0";
            lstDetalle = RecaudosMasivosServicio.ListarDetalleReporte(Convert.ToInt32(txtNumeroLista.Text), Convert.ToInt32(txtNumeroNovedad.Text), (Usuario)Session["usuario"]);

            //CREACION DE LA TABLA ENCABEZADO
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("cedula");
            table.Columns.Add("nombre");
            table.Columns.Add("tipo_producto");
            table.Columns.Add("num_producto");
            table.Columns.Add("valor_aplicado");
            table.Columns.Add("valor_novedad");
            table.Columns.Add("diferencia");

            //LLENAR LAS TABLAS CON LOS DATOS CORRESPONDIENTES
            decimal totalpagado = 0,totalCobrado = 0,TotalDiferencia= 0;
            if (lstDetalle.Count > 0)
            {
                foreach (RecaudosMasivos rFila in lstDetalle)
                {
                    DataRow dr;
                    dr = table.NewRow();
                    dr[0] = " " + rFila.identificacion;
                    dr[1] = " " + rFila.nombre;
                    dr[2] = " " + rFila.tipo_producto;
                    dr[3] = " " + rFila.numero_producto;
                    dr[4] = " " + rFila.valor_aplicado.ToString("n");
                    dr[5] = " " + rFila.valor_novedad.ToString("n");
                    dr[6] = " " + (rFila.valor_novedad - rFila.valor_aplicado).ToString("n");
                    totalpagado += rFila.valor_aplicado;
                    totalCobrado += rFila.valor_novedad;
                    TotalDiferencia += rFila.valor_novedad - rFila.valor_aplicado;
                    table.Rows.Add(dr);
                }
            }


            //PASAR LOS DATOS AL REPORTE
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            ReportParameter[] param = new ReportParameter[6];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);
            param[2] = new ReportParameter("fecha", DateTime.Now.ToShortDateString());
            param[3] = new ReportParameter("TotalPagado", totalpagado.ToString("N2"));
            param[4] = new ReportParameter("TotalCobrado", totalCobrado.ToString("N2"));
            param[5] = new ReportParameter("TotalDiferencia", TotalDiferencia.ToString("N2"));
            rvConsultaRecaudo.LocalReport.SetParameters(param);

            ReportDataSource rds = new ReportDataSource("DataSet1", table);

            rvConsultaRecaudo.LocalReport.DataSources.Clear();
            rvConsultaRecaudo.LocalReport.DataSources.Add(rds);
            rvConsultaRecaudo.LocalReport.Refresh();

            Site toolBar = (Site)this.Master;
            toolBar.MostrarCancelar(false);

            mvAplicar.ActiveViewIndex = 1;
            rvConsultaRecaudo.Visible = true;
            rvReporte.Visible = false;
            rvConsolidado.Visible = false;
        }
        else
        {
            VerError("No existen Datos");
        }
    }

    protected void cbDetallado_CheckedChanged(object sender, EventArgs e)
    {
        Actualizar();
    }
}