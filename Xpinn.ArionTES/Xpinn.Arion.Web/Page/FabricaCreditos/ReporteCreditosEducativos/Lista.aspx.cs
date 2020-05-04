using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Entities;
using Xpinn.Util;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.CreditoService creditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(creditoServicio.CodigoProgramaRepEdu, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoProgramaRepEdu, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, creditoServicio.CodigoProgramaRepEdu);
                if (Session[creditoServicio.CodigoProgramaRepEdu + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoProgramaRepEdu, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, creditoServicio.CodigoProgramaRepEdu);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtFechaCorte.Enabled = true;
        txtTasa.Habilitado = true;
        gvLista.Visible = false;
        lblTotalRegs.Visible = false;
        LimpiarValoresConsulta(pConsulta, creditoServicio.CodigoProgramaRepEdu);
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
            BOexcepcion.Throw(creditoServicio.CodigoProgramaRepEdu, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            if (!txtFechaCorte.TieneDatos)
                txtFechaCorte.ToDateTime = DateTime.Now;
            decimal tasa = ConvertirStringToDecimal(txtTasa.Text);
            decimal saldoTotal = 0;
            decimal intcteTotal = 0;
            List<Xpinn.FabricaCreditos.Entities.Credito> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Credito>();
            lstConsulta = creditoServicio.ListarCreditosEducativos(txtFechaCorte.ToDateTime, "", (Usuario)Session["usuario"]);
            foreach (Xpinn.FabricaCreditos.Entities.Credito cred in lstConsulta)
            {
                saldoTotal += cred.saldo_capital;  
                if (cred.maneja_auxilio == 1)
                    cred.intcoriente = Math.Round(cred.saldo_capital * tasa / 100);
                else
                    cred.intcoriente = Math.Round(cred.saldo_capital * (tasa/2) / 100);
                intcteTotal += cred.intcoriente;
            }
            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString() + " Saldo Total: " + saldoTotal.ToString("c") + " Int.Cte Total: " + intcteTotal.ToString("c");
                gvLista.DataBind();                
                txtFechaCorte.Enabled = false;
                txtTasa.Habilitado = false;
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(creditoServicio.CodigoProgramaRepEdu + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoProgramaRepEdu, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.Credito ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.Credito vCredito = new Xpinn.FabricaCreditos.Entities.Credito();
        return vCredito;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
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
                Response.AddHeader("Content-Disposition", "attachment;filename=RepCreditosEducativos.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
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

}