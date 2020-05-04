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
using Xpinn.Auxilios.Entities;
using Xpinn.Auxilios.Services;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;

partial class Lista : GlobalWeb
{
    ReporteAuxilioService SoliServicios = new ReporteAuxilioService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(SoliServicios.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;

            toolBar.MostrarCancelar(false);
            toolBar.MostrarExportar(false);
            toolBar.MostrarImprimir(false);

         ///   ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDropdown();

               
               
                }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoPrograma, "Page_Load", ex);
        }
    }



    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        Site toolBar = (Site)this.Master;
        ddlestado.SelectedIndex = 0;
        ddllineaAuxilios.SelectedIndex = 0;
        txtidentificacion.Text = "";
        txtMora1.Text = "";
        txtMora2.Text = "";
        txtNombre.Text = "";
        gvImpresion.Visible = false;
        gvLista.Visible = false;
        rvExtracto.Visible = false;
        lblTotalRegs.Visible = false;
        toolBar.MostrarCancelar(false);
        toolBar.MostrarExportar(false);
        toolBar.MostrarImprimir(false);

    }
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
     
        Page.Validate();
        gvLista.Visible = true;
        gvImpresion.Visible = true;
        if (Page.IsValid)
        {
            
            Actualizar();
        }
    }
    void CargarDropdown()
    {
        PoblarListas PoblarLista = new PoblarListas();
        PoblarLista.PoblarListaDesplegable("lineasauxilios", "", " ESTADO = 1 ", "2", ddllineaAuxilios,(Usuario)Session["usuario"]);
        
        ddlestado.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlestado.Items.Insert(1, new ListItem("Solicitado", "S"));
        ddlestado.Items.Insert(2, new ListItem("Aprobado", "A"));
        ddlestado.Items.Insert(3, new ListItem("Desembolsado", "D"));
        ddlestado.Items.Insert(4, new ListItem("Anulado", "N"));
        ddlestado.SelectedIndex = 0;
        ddlestado.DataBind();
        
    }

    
    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvImpresion.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        String ids = gvImpresion.DataKeys[gvImpresion.SelectedRow.RowIndex].Value.ToString();
        Session[SoliServicios.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }


    protected void ImprimirGrilla(object sender, EventArgs e)
    {

        string printScript =
            @"function PrintGridView()
                {         
                div = document.getElementById('DivButtons');
                div.style.display='none';
                var gridInsideDiv = document.getElementById('gvDiv');
                var printWindow = window.open('gview.htm','PrintWindow','letf=0,top=0,width=150,height=300,toolbar=1,scrollbars=1,status=1');
                printWindow.document.write(gridInsideDiv.innerHTML);
                printWindow.document.close();
                printWindow.focus();
                printWindow.print();
                printWindow.close();}";
        this.ClientScript.RegisterStartupScript(Page.GetType(), "PrintGridView", printScript.ToString(), true);

    }

    private void Actualizar()
    {
        try
        {
            List<ReporteAuxilio> lstConsulta = new List<ReporteAuxilio>();
            
            ReporteAuxilio filtro = new ReporteAuxilio();
            obtFiltro();

            DateTime pFechaIni;
            pFechaIni = txtMora1.ToDateTime;
            DateTime pFechaFin;
            pFechaFin = txtMora2.ToDateTime;
            lstConsulta = SoliServicios.ListarAuxilio(obtFiltro(),pFechaIni,pFechaFin, (Usuario)Session["usuario"]);
            
            gvLista.PageSize = 15;
            gvImpresion.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvImpresion.EmptyDataText = emptyQuery;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                gvImpresion.DataSource = lstConsulta;
                gvImpresion.DataBind();
                panelimpresion.Visible = false;
                panelGrilla.Visible = true;                
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                Site toolBar = (Site)this.Master;
                toolBar.MostrarCancelar(false);
                toolBar.MostrarExportar(true);
                toolBar.MostrarImprimir(true);
            }
            else
            {
                panelimpresion.Visible = false;
                panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
                Site toolBar = (Site)this.Master;
                toolBar.MostrarCancelar(false);
                toolBar.MostrarExportar(false);
                toolBar.MostrarImprimir(false);
            }
            Session["LSTCONSULTA"] = lstConsulta;
            Session.Add(SoliServicios.CodigoPrograma + ".consulta", 1);



            foreach (GridViewRow rfila in gvLista.Rows)
            {
                if (rfila.Cells[3].Text == "01/01/0001")
                    rfila.Cells[3].Text = "";
                if (rfila.Cells[4].Text == "01/01/0001")
                    rfila.Cells[4].Text = "";
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoPrograma, "Actualizar", ex);
        }
    }

      protected void btnImprimir_Click(object sender, EventArgs e)
      {
          List<ReporteAuxilio> lstConsulta = new List<ReporteAuxilio>();
          if (Session["LSTCONSULTA"] == null)
              return;
          lstConsulta = (List<ReporteAuxilio>)Session["LSTCONSULTA"];

          DataTable table = new DataTable();
          table.Columns.Add("numero_auxilio");
          table.Columns.Add("descripcion");
          table.Columns.Add("fecha_solicitud");
          table.Columns.Add("fecha_aprobacion");
          table.Columns.Add("fecha_desembolso");
          table.Columns.Add("identificacion");
          table.Columns.Add("nombres");
          table.Columns.Add("valor_solicitado", typeof(decimal));
          table.Columns.Add("valor_aprobado", typeof(decimal));
          table.Columns.Add("valor_matricula", typeof(decimal));
          table.Columns.Add("estado");
          table.Columns.Add("identificacionPersona");
          table.Columns.Add("nombre");
          table.Columns.Add("cod_parentesco");
          table.Columns.Add("descripciones");

            foreach (ReporteAuxilio fila in lstConsulta)
            {
                DataRow datarw;
                datarw = table.NewRow();
                datarw[0] = fila.numero_auxilio.ToString();
                datarw[1] = fila.descripcion;
                datarw[2] = fila.fecha_solicitud.ToShortDateString();
                datarw[3] = fila.fecha_aprobacion.ToShortDateString();
                if (fila.fecha_desembolso.ToShortDateString() == null)
                {
                    datarw[4] = fila.fecha_desembolso.ToString();                   
                }
                else
                    datarw[4] = fila.fecha_desembolso.ToShortDateString();
                datarw[5] = fila.identificacion;
                datarw[6] = fila.nombres;
                datarw[7] = fila.valor_solicitado.ToString();
                datarw[8] = fila.valor_aprobado.ToString();
                datarw[9] = fila.valor_matricula.ToString();
                datarw[10] = fila.estado;
                datarw[11] = fila.identificacionPersona;
                datarw[12] = fila.nombre;
                datarw[13] = fila.cod_parentesco;
                datarw[14] = fila.descripciones;

                table.Rows.Add(datarw);
                  
            }

           Usuario pUsu = (Usuario)Session["usuario"];

            rvExtracto.LocalReport.DataSources.Clear();
            ReportParameter[] param = new ReportParameter[3];
            param[0] = new ReportParameter("entidad", pUsu.empresa);
            param[1] = new ReportParameter("nit", pUsu.nitempresa);
            param[2] = new ReportParameter("ImagenReport", ImagenReporte());

            rvExtracto.LocalReport.EnableExternalImages = true;
            rvExtracto.LocalReport.SetParameters(param);

            ReportDataSource rds1 = new ReportDataSource("DataSet1", table);
            rvExtracto.LocalReport.DataSources.Add(rds1);
            rvExtracto.LocalReport.Refresh();
            rvExtracto.Visible = true;

            Site toolBar = (Site)Master;
           
            rvExtracto.Visible = true;
            mvPrincipal.ActiveViewIndex = 1;
            toolBar.MostrarCancelar(true);
            toolBar.MostrarExportar(false);
            toolBar.MostrarImprimir(false);
            toolBar.MostrarLimpiar(false);
            toolBar.MostrarConsultar(false);
                
            
      }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvImpresion);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        rvExtracto.Visible = false;
        mvPrincipal.ActiveViewIndex = 0;
        toolBar.MostrarConsultar(true);
        toolBar.MostrarExportar(true);
        toolBar.MostrarImprimir(true);
        toolBar.MostrarLimpiar(true);
        toolBar.MostrarCancelar(false);
    }


    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=ReporteAuxilios.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.Default;
        StringWriter sw = new StringWriter();
        ExpGrilla expGrilla = new ExpGrilla();
        sw = expGrilla.ObtenerGrilla(GridView1, (List<ReporteAuxilio>)Session["LSTCONSULTA"]);

        Response.Write(expGrilla.style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    { 
    }

    private string obtFiltro()
    {

        String filtros = String.Empty;
        ReporteAuxilio hola = new ReporteAuxilio();
        filtros = "";
        if (ddlestado.SelectedIndex != 0)
        {
            filtros += "and auxilios.estado='" + ddlestado.SelectedValue + "'";
         
        }
        if (txtFechaReporte.Text != "")
        {
            Configuracion conf = new Configuracion();
            filtros += " and AUXILIOS.fecha_desembolso = To_Date('" + Convert.ToDateTime(txtFechaReporte.Text).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
        }

        if (txtidentificacion.Text != "") 
        { 
            filtros+="and persona.identificacion= '"+txtidentificacion.Text+"'";
        }
        if(txtNombre.Text!="")
        {
            filtros+="and persona.primer_nombre||' '||persona.primer_apellido Like '" + txtNombre.Text +"%'";
        }

        if (ddllineaAuxilios.SelectedIndex != 0)
            filtros += "and LINEASAUXILIOS.cod_linea_auxilio= '" + ddllineaAuxilios.SelectedValue+"'";

        if (txtCodigoNomina.Text != "")
            filtros += " and p.cod_nomina like '%" + txtCodigoNomina.Text + "%'";

        return filtros;
    }
    
 }