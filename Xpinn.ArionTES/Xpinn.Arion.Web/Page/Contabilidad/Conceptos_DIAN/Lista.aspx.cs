using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Reporteador.Services;
using Xpinn.Reporteador.Entities;
using System.Data;
using Xpinn.Util;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using ClosedXML.Excel;
using System.Globalization;

public partial class Page_Contabilidad_Conceptos_DIAN_Lista : GlobalWeb
{
    ExogenaReportService objReporteService = new ExogenaReportService();
    String operacion = "";
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(objReporteService.CodigoTiposConcepto, "D");

            Site toolBar = (Site)Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoImportar += btnImportar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;


        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(_datosClienteServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private void Actualizar()
    {
        VerError("");
        try
        {
            List<ExogenaReport> lstConsulta = new List<ExogenaReport>();
            lstConsulta = objReporteService.TiposConceptos((Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
             
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                Session["DTAhorros"] = lstConsulta;
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
              
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

         
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objReporteService.CodigoPrograma, "Actualizar", ex);
        }
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Actualizar();
    }

    protected void btnImportar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Detalle);
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        operacion = "N";
        Navegar(Pagina.Nuevo);
    }
    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[objReporteService.CodigoTiposConcepto + ".id"] = id;
        Navegar(Pagina.Nuevo);
        e.NewEditIndex = -1;
    }
    private void btnContinuarMen_Click(object sender, EventArgs e)
    {
        VerError("");
        try
        {
            if (ViewState["ID"] != null)
            {
                Int32 pId = Convert.ToInt32(ViewState["ID"]);
                objReporteService.EliminarCodConcepto(pId, Usuario);
                Actualizar();
                ViewState["ID"] = null;
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }
    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            if (ViewState["DTAhorros"] != null)
            {
                List<ExogenaReport> lstConsulta = new List<ExogenaReport>();
                lstConsulta = (List<ExogenaReport>)ViewState["DTAhorros"];
                if (lstConsulta.Count > 0)
                {
                    gvLista.Visible = true;
                    gvLista.DataSource = lstConsulta;
                    gvLista.DataBind();
                    lblTotalRegs.Visible = true;
                    lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                    lblInfo.Visible = false;
                }
                else
                {
                    gvLista.Visible = false;
                    lblTotalRegs.Visible = false;
                    lblInfo.Visible = true;
                }
            }
            else
                Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objReporteService.CodigoTiposConcepto, "gvLista_PageIndexChanging", ex);
        }
    }
    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Int32 id = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Value.ToString());
        try
        {
            ViewState["ID"] = id;
            ctlMensaje.MostrarMensaje("Está seguro de eliminar el registro seleccionado?");
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

}