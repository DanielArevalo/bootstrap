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
using System.Text;
using System.IO;
using System.Drawing;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    private Xpinn.Obligaciones.Services.ObligacionCreditoService ObligacionCreditoServicio = new Xpinn.Obligaciones.Services.ObligacionCreditoService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ObligacionCreditoServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;            
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ObligacionCreditoServicio.CodigoPrograma);
        Navegar("Lista.aspx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ImprimirGrilla();
            if (!IsPostBack)
            {
                LlenarComboEntidades(ddlEntidad);
                btnExpObligacion.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Actualizar();
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Obligaciones.Entities.ObligacionCredito> lstConsulta = new List<Xpinn.Obligaciones.Entities.ObligacionCredito>();
            lstConsulta = ObligacionCreditoServicio.ListarObligacionCredito(ObtenerValores(), (Usuario)Session["usuario"]);

            gvObCredito.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvObCredito.Visible = true;
                gvObCredito.DataBind();
                btnExpObligacion.Visible = true;
            }
            else
            {
                btnExpObligacion.Visible = false;
                gvObCredito.Visible = false;
            }

            Session.Add(ObligacionCreditoServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.Obligaciones.Entities.ObligacionCredito ObtenerValores()
    {
        Xpinn.Obligaciones.Entities.ObligacionCredito vObligacionCredito = new Xpinn.Obligaciones.Entities.ObligacionCredito();

        if (txtNumeObl.Text.Trim() != "")
            vObligacionCredito.codobligacion = Convert.ToInt64(txtNumeObl.Text.Trim());
        
        vObligacionCredito.codentidad = Convert.ToInt64(ddlEntidad.SelectedValue);

        if (txtFechaProxPago.Text.Trim() != "")
            vObligacionCredito.fechaproximopago = Convert.ToString(txtFechaProxPago.Text.Trim());
        else
            vObligacionCredito.fechaproximopago = Convert.ToString("01/01/1900");

        vObligacionCredito.estadoobligacion = Convert.ToString(ddlEstadoObl.SelectedValue);
        vObligacionCredito.codfiltroorderuno = Convert.ToString(ddlFiltro1.SelectedValue);
        vObligacionCredito.codfiltroorderdos = Convert.ToString(ddlFiltro2.SelectedValue);

        return vObligacionCredito;
    }

    protected void LlenarComboEntidades(DropDownList ddlEntidades)
    {
        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
        ddlEntidades.DataSource = bancoService.ListarBancos(banco, (Usuario)Session["usuario"]);
        ddlEntidades.DataTextField = "nombrebanco";
        ddlEntidades.DataValueField = "cod_banco";
        ddlEntidades.DataBind();
        ddlEntidades.Items.Insert(0, new ListItem("Todos", "0"));
    }

    protected void ImprimirGrilla()
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
        btnImprimir.Attributes.Add("onclick", "PrintGridView();");
    }

    protected void ddlFiltro1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnExpObligacion_Click(object sender, EventArgs e)
    {
        ExportarExcelGrilla(gvObCredito, "ReporteObligaciones");
    }

    protected void ExportarExcelGrilla(GridView gvGrilla, string Archivo)
    {
        try
        {
            if (gvGrilla.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvGrilla.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvGrilla);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + Archivo + ".xls");
                Response.Charset = "UTF-8";
                Response.Write(sb.ToString());
                Response.End();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex0)
        {
            VerError(ex0.Message);
        }
    }


}