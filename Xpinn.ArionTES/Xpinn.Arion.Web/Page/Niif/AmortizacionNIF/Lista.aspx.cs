using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Xpinn.Util;
using Xpinn.Comun.Services;
using Xpinn.NIIF.Services;
using Xpinn.NIIF.Entities;
using System.IO;
using System.Text;


partial class Lista : GlobalWeb
{
    private Xpinn.NIIF.Services.AmortizacionNIFService AmortizacionServicio = new Xpinn.NIIF.Services.AmortizacionNIFService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {

            VisualizarOpciones(AmortizacionServicio.CodigoProgramaoriginal, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;           
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AmortizacionServicio.CodigoProgramaoriginal, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {          

            if (!IsPostBack)
            {
                btnExportar.Visible = false;
                txtFecha.Text = DateTime.Today.ToShortDateString();
                CargarValoresConsulta(pConsulta, AmortizacionServicio.CodigoProgramaoriginal);
                pDatos.Visible = false;
                txtFecha.Text = DateTime.Today.ToShortDateString();
               
                
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AmortizacionServicio.CodigoProgramaoriginal, "Page_Load", ex);
        }
    }


   
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Page.Validate();
            gvLista.Visible = true;
            if (Page.IsValid)
            {
                GuardarValoresConsulta(pConsulta, AmortizacionServicio.CodigoProgramaoriginal);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            gvLista.DataSource = null;
            gvLista.DataBind();
            VerError("Error." + ex.Message);
        }
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {       
        ctlMensaje.MostrarMensaje("Desea realizar la amortización de los créditos seleccionados?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            txtFecha.Enabled = false;
            AmortizacionServicio.ModificarEstadoFechaNIIF(Convert.ToDateTime(Session["Fecha"].ToString()), (Usuario)Session["usuario"]);

            VerError("Datos Grabados Correctamente");
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            btnExportar.Visible = false;
            gvLista.DataSource = null;
            gvLista.Visible = false;
            lblTotalRegs.Visible = false;

            Session["FechaComprobante"] = txtFecha.Text;
            Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = 0;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 115;
            Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = Convert.ToDateTime(txtFecha.Text);
            Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = -1;
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] = Session["Oficina"];
            Session["OrigenComprobante"] = "../../Niif/AmortizacionNIF/Lista.aspx";
            Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                      
        }
        catch
        { 
        
        }
    }
       
    
    private void Actualizar()
    {
        VerError("");
        try
        {
            List<Xpinn.NIIF.Entities.AmortizacionNIF> lstConsulta = new List<Xpinn.NIIF.Entities.AmortizacionNIF>();
            DateTime pFecha;
            pFecha = txtFecha.ToDateTime == null ? DateTime.MinValue : txtFecha.ToDateTime;
            Session["Fecha"] = pFecha;

            lstConsulta = AmortizacionServicio.ListarAmortizacionNIIF(pFecha, (Usuario)Session["usuario"]);
                        
            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            

            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                Site toolBar = (Site)this.Master;   
                toolBar.MostrarGuardar(true);
                btnExportar.Visible = true;                

                pDatos.Visible = true;
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
                Session["DTCOSTOAMORTIZADO"] = lstConsulta;
                
                VerError("");
            }
            else
            {
                Session["DTCOSTOAMORTIZADO"] = null;
                pDatos.Visible = false;
                gvLista.DataSource = null;
                gvLista.Visible = false;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                btnExportar.Visible = false;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                VerError("No existen Datos para la fecha seleccionada");
            }
                       

            Session.Add(AmortizacionServicio.CodigoProgramaoriginal + ".consulta", 1);

        }
        catch (Exception ex)
        {
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            btnExportar.Visible = false;
            gvLista.DataSource = null;
            gvLista.Visible = false;
            lblTotalRegs.Text = "<br/>No se encontraron Registros ";
            VerError(ex.Message);
        }
    }


    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[AmortizacionServicio.CodigoProgramaoriginal + ".id"] = id;
        Navegar(Pagina.Detalle);
    }
    
    protected void gvLista_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        gvLista.PageIndex = e.NewPageIndex;
        Actualizar();
    }
    
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTCOSTOAMORTIZADO"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.DataSource = Session["DTCOSTOAMORTIZADO"];
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
                Response.AddHeader("Content-Disposition", "attachment;filename=CostoAmortizado.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
                gvLista.AllowPaging = true;
                gvLista.DataBind();
            }
            else 
            {
                VerError("No existen Datos");
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }

    decimal sumValor_presente = 0;
    decimal sumValor_ajuste = 0;

    protected void gvLista_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            sumValor_presente += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "valor_presente"));
            sumValor_ajuste += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "valor_ajuste"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[11].Text = "Totales:";
            e.Row.Cells[12].Text = sumValor_presente.ToString("c");
            e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[13].Text = sumValor_ajuste.ToString("c");
            e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Font.Bold = true;
        }

    }


    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs evt)
    {

        if (evt.CommandName == "Detalle")
        {
            DateTime pFecha;
            pFecha = txtFecha.ToDateTime == null ? DateTime.MinValue : txtFecha.ToDateTime;
            int index = Convert.ToInt32(evt.CommandArgument);
            GridViewRow gvTransaccionesRow = gvLista.Rows[index];
            String snumpro = gvLista.Rows[index].Cells[1].Text;
            if (snumpro != "")
            {
                List<DetalleAmortizacionNIIF> lstDetalle = new List<DetalleAmortizacionNIIF>();
                try
                {
                    lstDetalle = AmortizacionServicio.ListarDetalleAmortizacionNIIF(pFecha, Convert.ToInt64(snumpro), (Usuario)Session["Usuario"]);
                    gvDetalle.DataSource = lstDetalle;
                    gvDetalle.DataBind();
                    MpeDetalle.Show();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
        }
    }


    protected void btnCloseAct_Click(object sender, EventArgs e)
    {
        MpeDetalle.Hide();
    }

}