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

public partial class Lista : GlobalWeb
{
    IdentificacionServices identificacionServicio = new IdentificacionServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(identificacionServicio.CodigoProgramaCa, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarExportar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaCa, "Page_PreInit", ex);
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
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaCa, "Page_Load", ex);
        }
    }

    private void CargarListas()
    {
        PoblarListas poblar = new PoblarListas();
        poblar.PoblarListaDesplegable("GR_AREA_FUNCIONAL", "COD_AREA, DESCRIPCION", "", "1", ddlArea, (Usuario)Session["usuario"]);
        poblar.PoblarListaDesplegable("GR_CARGO_ORGANIGRAMA", "COD_CARGO, DESCRIPCION", "", "1", ddlPotencialResponsable, (Usuario)Session["usuario"]);
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Actualizar();
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarPanel(pConsulta);
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (gvCausaRiesgo.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=ReporteCausas.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            ExpGrilla expGrilla = new ExpGrilla();
            sw = expGrilla.ObtenerGrilla(gvCausaRiesgo, null);
            Response.Write("<div>" + expGrilla.style + "</div>");
            Response.Output.Write("<div>" + sw.ToString() + "</div>");
            Response.Flush();
            Response.End();
        }
    }

    private void Actualizar()
    {
        List<Identificacion> lstCausas = new List<Identificacion>();
        
        lstCausas = identificacionServicio.ListarCausas(ObtenerFiltro(), "", (Usuario)Session["usuario"]);
        if (lstCausas.Count > 0)
        {
            panelGrilla.Visible = true;
            gvCausaRiesgo.DataSource = lstCausas;
            gvCausaRiesgo.DataBind();
            lblTotalRegs.Text = "Registros encontrados: " + lstCausas.Count;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarExportar(true);
        }
        else
        {
            panelGrilla.Visible = false;
            lblTotalRegs.Text = "La consulta no obtuvo resultado";
        }

    }

    private Identificacion ObtenerFiltro()
    {
        Identificacion pCausa = new Identificacion();
        if (txtCodigoCausa.Text != "")
            pCausa.cod_causa = Convert.ToInt64(txtCodigoCausa.Text);
        if (txtDescripcionCausa.Text != "")
            pCausa.descripcion = Convert.ToString(txtDescripcionCausa.Text);
        if (ddlArea.SelectedValue != "")
            pCausa.cod_area = Convert.ToInt64(ddlArea.SelectedValue);
        if (ddlPotencialResponsable.SelectedValue != "")
            pCausa.cod_cargo = Convert.ToInt64(ddlPotencialResponsable.SelectedValue);

        return pCausa;
    }

    protected void gvCausaRiesgo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Identificacion pCausa = new Identificacion();
            MatrizServices matrizServicio = new MatrizServices();
            List<Matriz> lstMatriz = new List<Matriz>();

            pCausa.cod_causa = Convert.ToInt64(gvCausaRiesgo.DataKeys[e.RowIndex].Values[0]);
            string filtro = " WHERE COD_CAUSA = " + pCausa.cod_causa + " AND CALIFICACION_RIESGO > 0";

            lstMatriz = matrizServicio.ListarMatriz(0, filtro, (Usuario)Session["usuario"]);
            if (lstMatriz.Count == 0)
            {
                identificacionServicio.EliminarCausa(pCausa, (Usuario)Session["usuario"]);
                Actualizar();
            }
            else
            {
                VerError("No se puede eliminar la causa, se encuentra registrada en una matriz de riesgo inherente");
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaCa, "gvCausaRiesgo_RowDeleting", ex);
        }
    }

    protected void gvCausaRiesgo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string id = gvCausaRiesgo.DataKeys[e.NewEditIndex].Value.ToString();
        Session[identificacionServicio.CodigoProgramaCa + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvCausaRiesgo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvCausaRiesgo.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaCa, "gvCausaRiesgo_PageIndexChanging", ex);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
}