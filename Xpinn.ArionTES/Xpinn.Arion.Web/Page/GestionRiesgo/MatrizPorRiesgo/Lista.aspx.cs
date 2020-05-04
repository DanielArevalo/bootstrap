using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Services;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using System.Data;

public partial class Lista : GlobalWeb
{
    MatrizServices _matrizService = new MatrizServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_matrizService.CodigoProgramaMR, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.MostrarExportar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_matrizService.CodigoProgramaMR, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarListas();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_matrizService.CodigoProgramaMR, "Page_Load", ex);
        }
    }

    private void CargarListas()
    {
        PoblarListas poblar = new PoblarListas();
        poblar.PoblarListaDesplegable("GR_FACTOR_RIESGO", "COD_FACTOR, DESCRIPCION", "", "COD_FACTOR", ddlFactorRiesgo, (Usuario)Session["usuario"]);
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Actualizar();
    }

    private void Actualizar()
    {
        List<Identificacion> lstFactores = new List<Identificacion>();
        Int32 f = 0;
        if (ddlFactorRiesgo.SelectedValue == "") { f = 0; }else { f = Convert.ToInt32(ddlFactorRiesgo.SelectedValue); }
        lstFactores = _matrizService.ListarPonderadoFactores(f, (Usuario)Session["usuario"]);
        if (lstFactores.Count > 0)
        {
            panelGrilla.Visible = true;
            gvFactorRiesgo.DataSource = lstFactores;
            gvFactorRiesgo.DataBind();
            lblTotalRegs.Text = "Registros encontrados: " + lstFactores.Count;
            Site toolBar = (Site)this.Master;
        }
        else
        {
            panelGrilla.Visible = false;
            lblTotalRegs.Text = "La consulta no obtuvo resultado";
        }

    }

    protected void gvFactor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtPuntajeI = (TextBox)e.Row.FindControl("txtPuntajeI");
            TextBox puntajeI = (TextBox)e.Row.FindControl("puntajeI");
            if (txtPuntajeI.Text == "") { txtPuntajeI.Text = Convert.ToString(0); }
            //Int32 colorI = _matrizService.calcularValoracionRango(Convert.ToInt32(txtPuntajeI.Text), (Usuario)Session["usuario"]);
            Int32 colorI = Convert.ToInt32(txtPuntajeI.Text);
            puntajeI.Text = _matrizService.ColoresRInherente[colorI].descripcion;
            puntajeI.BackColor = System.Drawing.Color.FromName(_matrizService.ColoresRInherente[colorI].nivel);

            TextBox txtPuntajeR = (TextBox)e.Row.FindControl("txtPuntajeR");
            if (txtPuntajeR.Text == "") { txtPuntajeR.Text = Convert.ToString(0); }
            //Int32 colorR = _matrizService.calcularValoracionRango(Convert.ToInt32(txtPuntajeR.Text), (Usuario)Session["usuario"]);
            Int32 colorR = Convert.ToInt32(txtPuntajeR.Text);
            TextBox puntajeR = (TextBox)e.Row.FindControl("puntajeR");
            puntajeR.Text = _matrizService.ColoresRResidual[colorR].descripcion;
            puntajeR.BackColor = System.Drawing.Color.FromName(_matrizService.ColoresRResidual[colorR].nivel);
        }
    }
    protected void gvFactorRiesgo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvFactorRiesgo.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_matrizService.CodigoProgramaMR, "gvFactorRiesgo_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Evento para realizar impresión del reporte de evaluación del factor de riesgo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 

    protected void gvFactorRiesgo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

}