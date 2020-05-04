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
using Xpinn.NIIF.Entities;
using Xpinn.NIIF.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{

    ObligacionesNIFServices ObligaService = new ObligacionesNIFServices();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ObligaService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;             
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            
            toolBar.MostrarGuardar(false);
            toolBar.MostrarExportar(false);            
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligaService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                mvPrincipal.ActiveViewIndex = 0;
                txtFecha.Text = DateTime.Now.ToShortDateString();
                panelGrilla.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligaService.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            if (txtFecha.Text != "")
            {
                Page.Validate();
                ObligacionesNIF obliga = new ObligacionesNIF();
                obliga.fecha = Convert.ToDateTime(txtFecha.Text);
                ObligaService.GENERAR_ObligacionesNIF(obliga, (Usuario)Session["usuario"]);
                if (ObligaService.ConsultarFECHAIngresada(Convert.ToDateTime(txtFecha.Text), (Usuario)Session["usuario"]) == true)
                {
                    if (Page.IsValid)
                        Actualizar();
                }
                else
                    VerError("La fecha Ingresada no es valida");
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
     }


    private void Actualizar()
    {
        try
        {
            List<ObligacionesNIF> lstConsulta = new List<ObligacionesNIF>();
            ObligacionesNIF vData = new ObligacionesNIF();

            DateTime FechaApe;

            FechaApe = txtFecha.ToDateTime == null ? DateTime.MinValue : txtFecha.ToDateTime;

            lstConsulta = ObligaService.Listar_TEMP_CostoAMortizado(FechaApe, (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;

            Site toolbar = (Site)Master;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;
                lblTotalRegs.Visible = true;
                Label2.Visible = false;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                Session["DTCOSTO"] = lstConsulta;

                toolbar.MostrarGuardar(true);
                toolbar.MostrarExportar(true);
            }
            else
            {
                toolbar.MostrarGuardar(false);
                toolbar.MostrarExportar(false);

                Label2.Visible = true;
                Session["DTCOSTO"] = null;
                panelGrilla.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(ObligaService.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligaService.CodigoPrograma, "Actualizar", ex);
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            if (gvLista.Rows.Count > 0 && Session["DTCOSTO"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.DataSource = Session["DTCOSTO"];
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

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligaService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    Boolean ValidarDatos() 
    {
        if (gvLista.Rows.Count == 0)
        {
            VerError("No existen datos a grabar");
            return false;
        }
        if (txtFecha.Text == "")
        {
            VerError("Ingrese la fecha Correspondiente");
            return false;
        }
        return true;
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        try
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
            Site toolBar = (Site)Master;
            toolBar.MostrarExportar(false);
            toolBar.MostrarGuardar(false);
            toolBar.MostrarConsultar(true);
            panelGrilla.Visible = false;
            lblTotalRegs.Text = "";
            mvPrincipal.ActiveViewIndex = 0;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligaService.CodigoPrograma, "btnRegresar_Click", ex);
        }
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            ctlMensaje.MostrarMensaje("Desea guardar los datos?");
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            ObligacionesNIF pObliga = new ObligacionesNIF();

            pObliga.fecha = Convert.ToDateTime(txtFecha.Text);
            pObliga.tipo = "M";
            pObliga.estado = "D";

            ObligaService.ModificarFechaCTOAMORTIZACION_NIF(pObliga, (Usuario)Session["usuario"]);

            Site toolBar = (Site)Master;
            toolBar.MostrarExportar(false);
            toolBar.MostrarGuardar(false);
            toolBar.MostrarConsultar(false);
            toolBar.MostrarConsultar(false);
            mvPrincipal.ActiveViewIndex = 1;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligaService.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }

    decimal sumValorPresente = 0;
    decimal sumAjuste = 0;
    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            sumValorPresente += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "valor_presente"));
            sumAjuste += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "valor_ajuste"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[9].Text = "Totales:";
            e.Row.Cells[10].Text = sumValorPresente.ToString("n");
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[11].Text = sumAjuste.ToString("n");
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Font.Bold = true;
        }
    }
}