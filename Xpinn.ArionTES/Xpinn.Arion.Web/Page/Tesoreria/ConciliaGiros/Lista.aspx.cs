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
using System.Globalization;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    private Xpinn.Tesoreria.Services.GiroServices GiroServicio = new Xpinn.Tesoreria.Services.GiroServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(GiroServicio.CodigoProgramaConcilia, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GiroServicio.CodigoProgramaConcilia, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarListas();
                CargarValoresConsulta(pConsulta, GiroServicio.CodigoProgramaConcilia);
                if (Session[GiroServicio.CodigoProgramaConcilia + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GiroServicio.CodigoProgramaConcilia, "Page_Load", ex);
        }
    }

    protected void CargarListas()
    {
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, GiroServicio.CodigoProgramaConcilia);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, GiroServicio.CodigoProgramaConcilia);
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
            BOexcepcion.Throw(GiroServicio.CodigoProgramaConcilia, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Tesoreria.Entities.Giro> lstConsulta = new List<Xpinn.Tesoreria.Entities.Giro>();
            string pFiltro = "";
            lstConsulta = GiroServicio.ConciliarGiro(ObtenerValores(), pFiltro, (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(GiroServicio.CodigoProgramaConcilia + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GiroServicio.CodigoProgramaConcilia, "Actualizar", ex);
        }
    }

    private Xpinn.Tesoreria.Entities.Giro ObtenerValores()
    {
        Xpinn.Tesoreria.Entities.Giro vGiro = new Xpinn.Tesoreria.Entities.Giro();

        if (txtCodigo.Text.Trim() != "")
            vGiro.idgiro = Convert.ToInt32(txtCodigo.Text.Trim());
        if (txtIdentific.Text.Trim() != "")
            vGiro.identificacion = Convert.ToString(txtIdentific.Text.Trim());
        if (txtNumRadic.Text.Trim() != "")
            vGiro.numero_radicacion = Convert.ToInt64(txtNumRadic.Text.Trim());
        if (txtFechaRegistro.TieneDatos)
            vGiro.fec_reg = txtFechaRegistro.ToDateTime;
        if (txtFechaGiro.TieneDatos)
            vGiro.fec_giro = txtFechaGiro.ToDateTime;
        
        return vGiro;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvLista);
    }

    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=RecibosCajaMenor.xls");
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

}