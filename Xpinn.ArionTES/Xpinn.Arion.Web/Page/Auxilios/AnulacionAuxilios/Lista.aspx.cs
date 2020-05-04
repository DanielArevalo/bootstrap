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
    ReporteAuxilioService AnulacionServicios = new ReporteAuxilioService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AnulacionServicios.CodigoProgramaAnulacion, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.MostrarExportar(false);
            toolBar.MostrarImprimir(false);

            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AnulacionServicios.CodigoProgramaAnulacion, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                mvPrincipal.ActiveViewIndex = 0;
                CargarDropdown();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AnulacionServicios.CodigoProgramaAnulacion, "Page_Load", ex);
        }
    }

    void Limpiar()
    {
        LimpiarPanel(pConsulta);
        gvLista.DataSource = null;
        gvLista.Visible = false;
        lblTotalRegs.Visible = false;
        lblNro.Text = "";
        Site toolBar = (Site)this.Master;
        toolBar.MostrarLimpiar(true);
        mvPrincipal.ActiveViewIndex = 0;
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        Limpiar();
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        if (mvPrincipal.ActiveViewIndex == 0)
        {
            Page.Validate();
            gvLista.Visible = true;
            if (Page.IsValid)
            {
                Actualizar();
            }
        }
        else
            Limpiar();
    }

    protected void CargarDropdown()
    {
        PoblarListas PoblarLista = new PoblarListas();
        PoblarLista.PoblarListaDesplegable("lineasauxilios", "", " ESTADO = 1 ", "2", ddllineaAuxilios,(Usuario)Session["usuario"]);
        
        ddlestado.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlestado.Items.Insert(1, new ListItem("Solicitado", "S"));
        ddlestado.Items.Insert(2, new ListItem("Aprobado", "A"));
        ddlestado.SelectedIndex = 0;
        ddlestado.DataBind();        
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

            lstConsulta = AnulacionServicios.ListarAuxilioPorAnular(obtFiltro(), (Usuario)Session["usuario"]);
            
            gvLista.PageSize = 20;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
            }
            else
            {
                panelGrilla.Visible = false;
                lblTotalRegs.Visible = false;
            }
            
            foreach (GridViewRow rfila in gvLista.Rows)
            {
                if (rfila.Cells[4].Text == "01/01/0001")
                    rfila.Cells[4].Text = "";
            }
            
            Session.Add(AnulacionServicios.CodigoProgramaAnulacion + ".consulta", 1);
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AnulacionServicios.CodigoProgramaAnulacion, "Actualizar", ex);
        }
    }

    /*
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvImpresion);
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

        sw = expGrilla.ObtenerGrilla(GridView1, null);

        Response.Write(expGrilla.style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }
    */

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (!ValidarAccionesGrilla("DELETE"))
            return;
        int id = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        Session["ID"] = id;
        ctlMensaje.MostrarMensaje("Está seguro que desea anular el Auxilio seleccionado?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            ReporteAuxilio pEntidad = new ReporteAuxilio();
            pEntidad.numero_auxilio = Convert.ToInt32(Session["ID"]);
            pEntidad.estado = "N";
            AnulacionServicios.GenerarAnulacionAuxilio(pEntidad, (Usuario)Session["usuario"]);
            Actualizar();
            Site toolBar = (Site)this.Master;
            toolBar.MostrarLimpiar(false);
            lblNro.Text = pEntidad.numero_auxilio.ToString();
            mvPrincipal.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AnulacionServicios.CodigoProgramaAnulacion, "btnContinuarMen_Click", ex);
        }
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
            BOexcepcion.Throw(AnulacionServicios.CodigoProgramaAnulacion, "gvLista_PageIndexChanging", ex);
        }
    }

    private string obtFiltro()
    {

        String filtros = String.Empty;
        filtros = "";
        if (ddlestado.SelectedIndex != 0)
        {
            filtros += " and auxilios.estado ='" + ddlestado.SelectedValue + "'";
        }
        else
            filtros += " AND AUXILIOS.ESTADO IN ('S','A')";
        if (txtFecha.Text != "")
        {
            Configuracion conf = new Configuracion();
            filtros += " and AUXILIOS.FECHA_SOLICITUD = To_Date('" + Convert.ToDateTime(txtFecha.Text).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
        }

        if (txtidentificacion.Text != "")
        {
            filtros += " and V_PERSONA.identificacion = '" + txtidentificacion.Text + "'";
        }
        if (txtNombre.Text != "")
        {
            filtros += " and UPPER(V_PERSONA.NOMBRE) Like '" + txtNombre.Text.Trim().ToUpper() + "%'";
        }
        if (txtCodigoNomina.Text != "")
            filtros += " and V_PERSONA.cod_nomina like '%" + txtCodigoNomina.Text + "%'";

        if (ddllineaAuxilios.SelectedIndex != 0)
            filtros += " and LINEASAUXILIOS.cod_linea_auxilio = '" + ddllineaAuxilios.SelectedValue + "'";

        return filtros;
    }

    // pendiente por usar
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[AnulacionServicios.CodigoProgramaAnulacion + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

 }