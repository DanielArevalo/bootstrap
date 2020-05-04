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

partial class Lista : GlobalWeb
{
    private Xpinn.Cartera.Services.RepEdadesService edadesServicio = new Xpinn.Cartera.Services.RepEdadesService();


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

        // Llenar rangos de clasificación de cartera
        List<EdadMora> lstRango = new List<EdadMora>();
        lstRango = edadesServicio.ListarRangos((Usuario)Session["usuario"]);
        cblRangos.DataTextField = "descripcion";
        cblRangos.DataValueField = "idrango";
        cblRangos.DataSource = lstRango;
        cblRangos.DataBind();
        foreach (System.Web.UI.WebControls.ListItem rItem in cblRangos.Items)
        {
            rItem.Selected = true;
        }
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
                filtro = "h.estado = 'C' And h.saldo_capital != 0";
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
            List<RepEdades> lstRepEdades = new List<RepEdades>();
            lstRepEdades.Clear();
            lstRepEdades = edadesServicio.ListarCredito(fecha, (Usuario)Session["usuario"], filtro);
            gvLista.EmptyDataText = emptyQuery;
            Session["DTEDADES"] = lstRepEdades;

            gvLista.DataSource = lstRepEdades;
            if (lstRepEdades.Count > 0)
            {
                mvEdades.ActiveViewIndex = 0;                
                // Colocar titulos a columnas                
                int RowIndex = 0;
                foreach (System.Web.UI.WebControls.ListItem rItem in cblRangos.Items)
                {
                    gvLista.Columns[17 + RowIndex].Visible = rItem.Selected;
                    gvLista.Columns[17 + RowIndex].HeaderText = rItem.Text;                    
                    RowIndex += 1;
                }
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
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


    protected void btnInforme_Click(object sender, EventArgs e)
    {

        Configuracion conf = new Configuracion();
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        // LLenar data table con los datos
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
        table.Columns.Add("fecha_proximo_pago", typeof(DateTime));
        table.Columns.Add("cod_asesor", typeof(Int64));
        table.Columns.Add("nom_asesor");
        table.Columns.Add("monto_aprobado", typeof(double));
        table.Columns.Add("cuota", typeof(double));
        table.Columns.Add("dias_mora", typeof(Int64));
        table.Columns.Add("clasificacion_mora_1", typeof(double));
        table.Columns.Add("clasificacion_mora_2", typeof(double));
        table.Columns.Add("clasificacion_mora_3", typeof(double));
        table.Columns.Add("clasificacion_mora_4", typeof(double));
        table.Columns.Add("clasificacion_mora_5", typeof(double));
        table.Columns.Add("clasificacion_mora_6", typeof(double));
        table.Columns.Add("clasificacion_mora_7", typeof(double));
        table.Columns.Add("clasificacion_mora_8", typeof(double));
        DataRow datarw;
        int RowIndex = 0;
        foreach (GridViewRow gfila in gvLista.Rows)
        {
            datarw = table.NewRow();
            for (int i = 0; i < 25; i++)
            {
                try
                {
                    if (table.Columns[i].DataType == typeof(Int64))
                        datarw[i] = gvLista.Rows[RowIndex].Cells[i].Text.Replace(".", "");
                    else if (table.Columns[i].DataType == typeof(double))
                    {
                        if (gvLista.Rows[RowIndex].Cells[i].Text == "")
                            datarw[i] = 0;
                        else
                            datarw[i] = gvLista.Rows[RowIndex].Cells[i].Text.Replace(".", "").Replace("$", "");
                    }
                    else
                        datarw[i] = gvLista.Rows[RowIndex].Cells[i].Text;
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
        ReportParameter[] param = new ReportParameter[10];

        param[0] = new ReportParameter("entidad", pUsuario.empresa);
        if (chkFecha.Checked == true)
            if (txtFechaActual.TieneDatos)
                param[1] = new ReportParameter("fecha", txtFechaActual.ToDate);
            else
                param[1] = new ReportParameter("fecha", System.DateTime.Now.ToShortDateString());
        else
            param[1] = new ReportParameter("fecha", ddlFechaCorte.SelectedItem.Value);
        param[2] = new ReportParameter("titulo1", gvLista.Columns[17].HeaderText);
        param[3] = new ReportParameter("titulo2", gvLista.Columns[18].HeaderText);
        param[4] = new ReportParameter("titulo3", gvLista.Columns[19].HeaderText);
        param[5] = new ReportParameter("titulo4", gvLista.Columns[20].HeaderText);
        param[6] = new ReportParameter("titulo5", gvLista.Columns[21].HeaderText);
        param[7] = new ReportParameter("titulo6", gvLista.Columns[22].HeaderText);
        param[8] = new ReportParameter("titulo7", gvLista.Columns[23].HeaderText);
        param[9] = new ReportParameter("titulo8", gvLista.Columns[24].HeaderText);

        rvEdades.LocalReport.SetParameters(param);

        ReportDataSource rds = new ReportDataSource("DataSet1", table);

        rvEdades.LocalReport.DataSources.Clear();
        rvEdades.LocalReport.DataSources.Add(rds);
        rvEdades.LocalReport.Refresh();

        // Mostrar el reporte en pantalla.
        mvEdades.ActiveViewIndex = 1;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTEDADES"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.DataSource = Session["DTEDADES"];
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
                Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
                Response.Charset = "UTF-8";
                Response.Write(sb.ToString());
                Response.End();
                gvLista.AllowPaging = true;
                gvLista.DataBind();
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

}