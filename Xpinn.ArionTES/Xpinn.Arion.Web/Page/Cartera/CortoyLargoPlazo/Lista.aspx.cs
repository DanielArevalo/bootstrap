using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Cartera.Entities;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using Microsoft.CSharp;
using System.Text;
using System.IO;
using Xpinn.Asesores.Services;
using System.Collections.Specialized;

partial class Lista : GlobalWeb
{
    private Xpinn.Cartera.Services.CortoyLargoPlazoService edadesServicio = new Xpinn.Cartera.Services.CortoyLargoPlazoService();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(edadesServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(edadesServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarCombos();
                
                CargarValoresConsulta(pConsulta, edadesServicio.CodigoPrograma);
                if (Session[edadesServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar(idObjeto);
                lqs.Selecting += LinqDS_Selecting;
                gvConsolidado.DataBind();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(edadesServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    /// <summary>
    /// Método para llenar los DDLs requeridos para las consultas
    /// </summary>
    protected void LlenarCombos()
    {
        // Llenar el DDL de la fecha de corte 
        List<Xpinn.Comun.Entities.Cierea> lstFechaCierre = new List<Xpinn.Comun.Entities.Cierea>();
        lstFechaCierre = edadesServicio.ListarFechaCierre((Usuario)Session["Usuario"]);
        ddlFechaCorte.DataSource = lstFechaCierre;
        ddlFechaCorte.DataTextFormatString = "{0:dd/MM/yyyy}";
        ddlFechaCorte.DataTextField = "fecha";
        ddlFechaCorte.DataBind();

        // Llenar lista de oficinas
        Usuario usuap = (Usuario)Session["usuario"];
        int cod = Convert.ToInt32(usuap.codusuario);
        Xpinn.FabricaCreditos.Services.OficinaService oficinaService = new Xpinn.FabricaCreditos.Services.OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
        ddlOficina.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);
        ddlOficina.DataTextField = "Nombre";
        ddlOficina.DataValueField = "Codigo";
        ddlOficina.DataBind();
        ddlOficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }

    protected void ddlOficina_SelectedIndexChanged(object sender, EventArgs e)
    {
        Usuario usuap = (Usuario)Session["usuario"];
        int cod = Convert.ToInt32(usuap.codusuario);

        Xpinn.Asesores.Services.EjecutivoService serviceEjecutivo = new Xpinn.Asesores.Services.EjecutivoService();
        Xpinn.Asesores.Entities.Ejecutivo ejec = new Xpinn.Asesores.Entities.Ejecutivo();
        ejec.IOficina = Convert.ToInt64(ddlOficina.SelectedItem.Value);
        ddlAsesores.DataSource = serviceEjecutivo.ListarAsesores(ejec, (Usuario)Session["usuario"]);
        ddlAsesores.DataTextField = "NombreCompleto";
        ddlAsesores.DataValueField = "IdEjecutivo";
        ddlAsesores.DataBind();
        ddlAsesores.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, edadesServicio.CodigoPrograma);
        Actualizar(idObjeto);
        Lblerror.Visible = false;
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, edadesServicio.CodigoPrograma);
        gvLista.DataSource = null;
        gvLista.DataBind();
        Lblerror.Visible = false;
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(edadesServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[edadesServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[edadesServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar(idObjeto);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(edadesServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar(String pIdObjeto)
    {

        try
        {
            String emptyQuery = "Fila de datos vacia";
            String filtro = "";
            DateTime fecha = System.DateTime.MinValue;
          
            String format = "dd/MM/yyyy";
            if (chkFecha.Checked == false)
            {
                filtro = "h.saldo_capital != 0";
                fecha = DateTime.ParseExact(ddlFechaCorte.SelectedValue, format, CultureInfo.InvariantCulture);
            }
            else
            {
                filtro = "c.estado = 'C' And c.saldo_capital != 0";
                fecha = System.DateTime.MinValue;
            }

            // Filtro por Oficina
            if (ddlOficina.SelectedItem.Value != "0")
                filtro += " And c.cod_oficina = " + ddlOficina.SelectedItem.Value;
            if (ddlAsesores.SelectedItem.Value != "0")
                filtro += " And c.cod_asesor_com = " + ddlAsesores.SelectedItem.Value;
           
            // Generar el Reporte de Cartera por Edades
            List<CortoyLargoPlazo> lstCortoyLargoPlazo = new List<CortoyLargoPlazo>();
            lstCortoyLargoPlazo.Clear();
            lstCortoyLargoPlazo = edadesServicio.ListarCredito(fecha, (Usuario)Session["usuario"], filtro);
            gvLista.EmptyDataText = emptyQuery;
            Session["DTCORTOYLARGOPLAZO"] = lstCortoyLargoPlazo;
            
            gvLista.DataSource = lstCortoyLargoPlazo;
            if (lstCortoyLargoPlazo.Count > 0)
            {
                mvEdades.ActiveViewIndex = 0;
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
                lqs.Selecting += LinqDS_Selecting;
                gvConsolidado.DataBind();
            }
            Session.Add(edadesServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(edadesServicio.CodigoPrograma, "Actualizar", ex);
        }

    }

    private Xpinn.Util.Usuario ObtenerValores()
    {
        Xpinn.Util.Usuario vUsuario = new Xpinn.Util.Usuario();

        return vUsuario;
    }

    protected void gvConsolidado_Sorting(object sender, GridViewSortEventArgs e)
    {
      
    }
    protected void btnInforme_Click(object sender, EventArgs e)
    {

        Configuracion conf = new Configuracion();
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        // LLenar data table con los datos
        if (chkConsolidado.Checked==true)
        {
            System.Data.DataTable tablecon = new System.Data.DataTable();
         
            tablecon.Columns.Add("cod_linea");
            tablecon.Columns.Add("nom_linea");
            tablecon.Columns.Add("saldo_capital", typeof(double));
            tablecon.Columns.Add("corto_plazo", typeof(double));
            tablecon.Columns.Add("largo_plazo", typeof(double));
            DataRow datarw1;
            int RowIndex1 = 0;
            foreach (GridViewRow gfila in gvConsolidado.Rows)
            {
                datarw1 = tablecon.NewRow();
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        if (tablecon.Columns[i].DataType == typeof(Int64))
                        {
                            if (gvConsolidado.Rows[RowIndex1].Cells[i].Text == "")
                                datarw1[i] = 0;
                            else
                                datarw1[i] = gvConsolidado.Rows[RowIndex1].Cells[i].Text.Replace(".", "");
                        }
                        else if (tablecon.Columns[i].DataType == typeof(double))
                        {
                            if (gvConsolidado.Rows[RowIndex1].Cells[i].Text == "")
                                datarw1[i] = 0;
                            else
                                datarw1[i] = gvConsolidado.Rows[RowIndex1].Cells[i].Text.Replace(".", "").Replace("$", "");
                        }
                        else
                        {
                            if (gvConsolidado.Rows[RowIndex1].Cells[i].Text == "")
                                datarw1[i] = DBNull.Value;
                            else
                                datarw1[i] = gvConsolidado.Rows[RowIndex1].Cells[i].Text;
                        }
                    }
                    catch (Exception ex)
                    {
                        VerError(ex.Message);
                    }
                }
                tablecon.Rows.Add(datarw1);
                RowIndex1 += 1;
            }
            // ---------------------------------------------------------------------------------------------------------
            // Pasar datos al reporte
            // ---------------------------------------------------------------------------------------------------------
            ReportParameter[] param1 = new ReportParameter[2];

            param1[0] = new ReportParameter("entidad", pUsuario.empresa);
            if (chkFecha.Checked == true)
                if (txtFechaActual.TieneDatos)
                    param1[1] = new ReportParameter("fecha", txtFechaActual.ToDate);
                else
                    param1[1] = new ReportParameter("fecha", System.DateTime.Now.ToShortDateString());
            else
                param1[1] = new ReportParameter("fecha", ddlFechaCorte.SelectedItem.Value);

            ReportViewer1.LocalReport.SetParameters(param1);

            ReportDataSource rds1 = new ReportDataSource("DataSet1", tablecon);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds1);
            ReportViewer1.LocalReport.Refresh();

            // Mostrar el reporte en pantalla.
            mvEdades.ActiveViewIndex = 2;
        }
        else { 
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("fecha", typeof(DateTime));
        table.Columns.Add("cod_oficina", typeof(Int64));
        table.Columns.Add("nom_oficina");
        table.Columns.Add("cod_linea");
        table.Columns.Add("nom_linea");
        table.Columns.Add("numero_radicacion", typeof(Int64));
        table.Columns.Add("cod_deudor", typeof(Int64));
        table.Columns.Add("identificacion");
        table.Columns.Add("nombres");
        table.Columns.Add("apellidos");
        table.Columns.Add("saldo_capital", typeof(double));
        DataColumn fproxpago = new DataColumn();
        fproxpago.AllowDBNull = true;
        fproxpago.ColumnName = "fecha_proximo_pago";
        fproxpago.DataType = typeof(DateTime);
        table.Columns.Add(fproxpago);
        table.Columns.Add("cod_asesor", typeof(Int64));
        table.Columns.Add("nom_asesor");
        table.Columns.Add("monto_aprobado", typeof(double));
        table.Columns.Add("cuota", typeof(double));
        table.Columns.Add("dias_mora", typeof(Int64));
        table.Columns.Add("corto_plazo", typeof(double));
        table.Columns.Add("largo_plazo", typeof(double));
        DataRow datarw;
        int RowIndex = 0;
        foreach (GridViewRow gfila in gvLista.Rows)
        {
            datarw = table.NewRow();
            for (int i = 0; i < 19; i++)
            {
                try
                {
                    if (table.Columns[i].DataType == typeof(Int64))
                    {
                        if (gvLista.Rows[RowIndex].Cells[i].Text == "")
                            datarw[i] = 0;
                        else
                            datarw[i] = gvLista.Rows[RowIndex].Cells[i].Text.Replace(".", "");
                    }
                    else if (table.Columns[i].DataType == typeof(double))
                    {
                        if (gvLista.Rows[RowIndex].Cells[i].Text == "")
                            datarw[i] = 0;
                        else
                            datarw[i] = gvLista.Rows[RowIndex].Cells[i].Text.Replace(".", "").Replace("$", "");
                    }
                    else
                    {
                        if (gvLista.Rows[RowIndex].Cells[i].Text == "")
                            datarw[i] = DBNull.Value;
                        else
                            datarw[i] = gvLista.Rows[RowIndex].Cells[i].Text;
                    }
                }
                catch (Exception ex)
                {
                    VerError(ex.Message);
                }
            }
            table.Rows.Add(datarw);
            RowIndex += 1;
        }

        // ---------------------------------------------------------------------------------------------------------
        // Pasar datos al reporte
        // ---------------------------------------------------------------------------------------------------------
        ReportParameter[] param = new ReportParameter[2];

        param[0] = new ReportParameter("entidad", pUsuario.empresa);
        if (chkFecha.Checked == true)
            if (txtFechaActual.TieneDatos)
                param[1] = new ReportParameter("fecha", txtFechaActual.ToDate);
            else
                param[1] = new ReportParameter("fecha", System.DateTime.Now.ToShortDateString());
        else
            param[1] = new ReportParameter("fecha", ddlFechaCorte.SelectedItem.Value);

        rvCortoyLargoPlazo.LocalReport.SetParameters(param);

        ReportDataSource rds = new ReportDataSource("DataSet1", table);

        rvCortoyLargoPlazo.LocalReport.DataSources.Clear();
        rvCortoyLargoPlazo.LocalReport.DataSources.Add(rds);
        rvCortoyLargoPlazo.LocalReport.Refresh();

        // Mostrar el reporte en pantalla.
        mvEdades.ActiveViewIndex = 1;
        }

    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkConsolidado.Checked==true)
            {
                if (gvConsolidado.Rows.Count > 0 && Session["DTCORTOYLARGOPLAZOCON"] != null)
                {
                    StringBuilder sb = new StringBuilder();
                    StringWriter sw = new StringWriter(sb);
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    Page pagina = new Page();
                    dynamic form = new HtmlForm();
                    
                    gvConsolidado.EnableViewState = false;
                    pagina.EnableEventValidation = false;
                    pagina.DesignerInitialize();
                    pagina.Controls.Add(form);
                    form.Controls.Add(gvConsolidado);
                    pagina.RenderControl(htw);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment;filename=RepCortoyLargoPlazo.xls");
                    Response.Charset = "UTF-8";
                    Response.Write(sb.ToString());
                    Response.End();
                  
                }
            }
            else { 
            if (gvLista.Rows.Count > 0 && Session["DTCORTOYLARGOPLAZO"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.DataSource = Session["DTCORTOYLARGOPLAZO"];
                gvLista.DataBind();
                gvLista.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvLista);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=RepCortoyLargoPlazo.xls");
                Response.Charset = "UTF-8";
                Response.Write(sb.ToString());
                Response.End();
                gvLista.AllowPaging = true;
                gvLista.DataBind();
            }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch
        {
        }
    }

    protected void chkFecha_CheckedChanged(object sender, EventArgs e)
    {
        if (chkFecha.Checked == true)
        {
            txtFechaActual.Visible = true;
            txtFechaActual.ToDateTime = System.DateTime.Now;
            txtFechaActual.Enabled = false;
            ddlFechaCorte.Enabled = false;
            ddlFechaCorte.Visible = false;
        }
        else
        {
            txtFechaActual.Visible = false;
            ddlFechaCorte.Enabled = true;
            ddlFechaCorte.Visible = true;
        }
    }
    protected void chkConsolidado_CheckedChanged(object sender, EventArgs e)
    {
        if (chkConsolidado.Checked==true)
        {
            gvLista.Visible = false;
            gvConsolidado.Visible = true;
        }
        else
        {
            gvLista.Visible = true;
            gvConsolidado.Visible = false;
        }
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        mvEdades.ActiveViewIndex = 0;
    }

    protected void LinqDS_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {
        List<CortoyLargoPlazo> lst = new List<CortoyLargoPlazo>();
        String filtro = "";
        DateTime fecha = System.DateTime.MinValue;

        String format = "dd/MM/yyyy";
        if (chkFecha.Checked == false)
        {
            filtro = "h.saldo_capital != 0";
            fecha = DateTime.ParseExact(ddlFechaCorte.SelectedValue, format, CultureInfo.InvariantCulture);
        }
        else
        {
            filtro = "c.estado = 'C' And c.saldo_capital != 0";
            fecha = System.DateTime.MinValue;
        }

        // Filtro por Oficina
        if (ddlOficina.SelectedItem.Value != "0")
            filtro += " And c.cod_oficina = " + ddlOficina.SelectedItem.Value;
        if (ddlAsesores.SelectedItem.Value != "0")
            filtro += " And c.cod_asesor_com = " + ddlAsesores.SelectedItem.Value;
        lst = edadesServicio.ListarCredito(fecha, (Usuario)Session["usuario"], filtro); 
        e.Result = lst;
        Session["DTCORTOYLARGOPLAZOCON"] = lst;





    }
  
   
}