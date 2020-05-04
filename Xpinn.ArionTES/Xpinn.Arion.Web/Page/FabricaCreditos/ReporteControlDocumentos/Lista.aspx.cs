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
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;


partial class Lista : GlobalWeb
{

    DocumentosAnexosService objDocumentos = new DocumentosAnexosService();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(objDocumentos.CodigoProgram, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objDocumentos.CodigoProgram, "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarComboOficinas(ddlOficina);
                CargarValoresConsulta(pConsulta, objDocumentos.CodigoProgram);
                if (Session[objDocumentos.CodigoProgram + ".consulta"] != null)
                {
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarExportar(false);
                    Actualizar();
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objDocumentos.CodigoProgram, "Page_Load", ex);
        }
    }

    public string getFiltro()
    {
        string filtro = string.Empty;
        
        if (ddlOficina.SelectedIndex > 0)
            filtro += "and c.cod_oficina =" + ddlOficina.SelectedValue;

        if (ddlLineaCredito.SelectedIndex > 0)
            filtro += " and c.cod_linea_credito ='" + ddlLineaCredito.SelectedValue+"'";

        if(txtidentificacion.Text!="")
            filtro += "and p.identificacion like '%" + txtidentificacion.Text +"%'";
        if(ddlestado.SelectedIndex != 0)
            filtro += "and d.estado =" + ddlestado.SelectedValue + "";
        if (txtCodigoNomina.Text.Trim() != "")
            filtro += " and p.cod_nomina = '" + txtCodigoNomina.Text + "'";

        return filtro;
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();

        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, objDocumentos.CodigoProgram);
            Actualizar();
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        gvLista.DataSourceID = null;
        gvLista.DataBind();
        gvLista.Visible = false;
        lblTotalRegs.Visible = false;
        LimpiarValoresConsulta(pConsulta, objDocumentos.CodigoProgram);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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
            BOexcepcion.Throw(objDocumentos.CodigoProgram, "gvLista_PageIndexChanging", ex);
        }
    }
    private void Actualizar()
    {
        try
        {
            DateTime pFechaAper;
            pFechaAper = txtFecha.ToDateTime == null ? DateTime.MinValue : txtFecha.ToDateTime;

            List<DocumentosAnexos> lstConsulta = objDocumentos.ListadoControlDocumentos(pFechaAper,getFiltro(), (Usuario)Session["usuario"]);
            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                Session["DatosGrid"] = lstConsulta;
                ValidarPermisosGrilla(gvLista);
                Site toolBar = (Site)this.Master;
                toolBar.MostrarExportar(false);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                lblTotalRegs.Text="No se Encontraron Registros";
                ClientScriptManager sc = Page.ClientScript;
                sc.RegisterStartupScript(this.GetType(), "key", "$('#lblError').css('color','#0066cc');", true);
            }

            foreach (GridViewRow row in gvLista.Rows)
            {

                if (row.Cells[14].Text == "01/01/0001")
                    row.Cells[14].Text = "";
            }
            
            

            Session.Add(objDocumentos.CodigoProgram + ".consulta", 1);
        }
        catch
        {
            lblTotalRegs.Text="No hay datos";
            lblTotalRegs.Visible = false;
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (gvLista.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvLista);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=ReporteControlDocumentos.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }

    protected void LlenarComboOficinas(DropDownList ddlOficinas)
    {
        OficinaService oficinaService = new OficinaService();
        Oficina oficina = new Oficina();
        ddlOficinas.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);
        ddlOficinas.DataTextField = "nombre";
        ddlOficinas.DataValueField = "codigo";
        ddlOficinas.DataBind();
        ddlOficinas.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        PoblarListas listasdespleglabe = new PoblarListas();
        listasdespleglabe.PoblarListaDesplegable("LINEASCREDITO", ddlLineaCredito, (Usuario)Session["usuario"]);
    }


    protected void btnInforme_Click(object sender, EventArgs e)
    {
        Configuracion conf = new Configuracion();
        VerError("");
        if (Session["DatosGrid"] == null)
        {
            VerError("No ha generado el Reporte para poder imprimir informacion");
        }
        else
        {
            List<DocumentosAnexos> lista = (List<DocumentosAnexos>)Session["DatosGrid"];

            System.Data.DataTable table = new System.Data.DataTable();

            table.Columns.Add("NunRadicacion");
            table.Columns.Add("Linea");
            table.Columns.Add("Identificacion");
            table.Columns.Add("Nombre");
            table.Columns.Add("FecSolicitud");
            table.Columns.Add("Fecha");
            table.Columns.Add("Monto");
            table.Columns.Add("Plazo");
            table.Columns.Add("Estado");
            table.Columns.Add("TipoDocumento");
            table.Columns.Add("FechaF");
            table.Columns.Add("Descripcion");
            table.Columns.Add("EstadoF");
            table.Columns.Add("FecEstimada");

            foreach (DocumentosAnexos item in lista)
            {
                DataRow datarw;
                datarw = table.NewRow();
                datarw[0] = item.numero_radicacion;
                datarw[1] = item.tipo;
                datarw[2] = item.iddocumento;
                datarw[3] = item.Nombre;
                datarw[4] = item.fecha_solicitud;
                datarw[5] = item.fechaentrega;
                datarw[6] = item.monto_solicitado;
                datarw[7] = item.Nun_Cuoatas;
                datarw[8] = item.estado_cre;
                datarw[9] = item.tipo_documento;
                datarw[10] = item.fechaanexo;
                datarw[11] = item.descripcion;
                datarw[12] = item.estados;
                datarw[13] = item.fec_estimada_entrga;

                table.Rows.Add(datarw);
            }

            Usuario pUsuario  = (Usuario)Session["Usuario"];

            ReportParameter[] param = new ReportParameter[5];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);
            param[2] = new ReportParameter("Usuario", pUsuario.nombre);
            param[3] = new ReportParameter("Oficina", pUsuario.nombre_oficina);
            param[4] = new ReportParameter("ImagenReport", ImagenReporte());


            rvReporte.LocalReport.EnableExternalImages = true;
            rvReporte.LocalReport.SetParameters(param);

            ReportDataSource rds = new ReportDataSource("DataSet1", table);
            rvReporte.LocalReport.DataSources.Clear();
            rvReporte.LocalReport.DataSources.Add(rds);
            rvReporte.LocalReport.Refresh();


            // Mostrar el reporte en pantalla.
            MvControl.ActiveViewIndex = 1;


        }
    }
}

