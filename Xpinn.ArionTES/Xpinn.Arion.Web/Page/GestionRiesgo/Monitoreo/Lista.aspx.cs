using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Riesgo.Services;
using Xpinn.Riesgo.Entities;
using Xpinn.Util;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;


public partial class Lista : GlobalWeb
{
    SeguimientoServices seguimientoServicio = new SeguimientoServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(seguimientoServicio.CodigoProgramaM, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarExportar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(seguimientoServicio.CodigoProgramaM, "Page_PreInit", ex);
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
            BOexcepcion.Throw(seguimientoServicio.CodigoProgramaM, "Page_Load", ex);
        }
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
        if (gvMonitoreo.Rows.Count > 0)
        {

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=ReporteMonitoreo.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            ExpGrilla expGrilla = new ExpGrilla();
            sw = expGrilla.ObtenerGrilla(gvMonitoreo, null);
            Response.Write("<div>" + expGrilla.style + "</div>");
            Response.Output.Write("<div>" + sw.ToString() + "</div>");
            Response.Flush();
            Response.End();
        }
    }

    private void CargarListas()
    {
        PoblarListas poblar = new PoblarListas();
        poblar.PoblarListaDesplegable("GR_AREA_FUNCIONAL", "COD_AREA, DESCRIPCION", "", "1", ddlAreaEjecucion, (Usuario)Session["usuario"]);
        poblar.PoblarListaDesplegable("GR_CARGO_ORGANIGRAMA", "COD_CARGO, DESCRIPCION", "", "1", ddlResponsableEjecucion, (Usuario)Session["usuario"]);
        poblar.PoblarListaDesplegable("GR_PERIODICIDAD", "COD_PERIODICIDAD, DESCRIPCION", "", "1", ddlPeriodicidad, (Usuario)Session["usuario"]);

        ddlEstadoAlerta.Items.Insert(0, new ListItem("Seleccione un item", ""));
        ddlEstadoAlerta.Items.Insert(1, new ListItem("Pasiva", "1"));
        ddlEstadoAlerta.Items.Insert(2, new ListItem("Pendiente", "2"));
        ddlEstadoAlerta.Items.Insert(3, new ListItem("Activa", "3"));
        ddlEstadoAlerta.DataBind();
    }

    private Seguimiento ObtenerFiltro()
    {
        Seguimiento pMonitoreo = new Seguimiento();
        if (!string.IsNullOrWhiteSpace(txtCodigoMonitoreo.Text))
            pMonitoreo.cod_monitoreo = Convert.ToInt64(txtCodigoMonitoreo.Text);
        if (ddlAreaEjecucion.SelectedValue != "")
            pMonitoreo.cod_area = Convert.ToInt64(ddlAreaEjecucion.SelectedValue);
        if (ddlEstadoAlerta.SelectedValue != "")
            pMonitoreo.cod_alerta = Convert.ToInt32(ddlEstadoAlerta.SelectedValue);
        if (ddlResponsableEjecucion.SelectedValue != "")
            pMonitoreo.cod_cargo = Convert.ToInt64(ddlResponsableEjecucion.SelectedValue);
        if (ddlPeriodicidad.SelectedValue != "")
            pMonitoreo.periodicidad = Convert.ToInt32(ddlPeriodicidad.SelectedValue);

        return pMonitoreo;
    }

    private void Actualizar()
    {
        List<Seguimiento> lstTipoMonitoreo = new List<Seguimiento>();

        lstTipoMonitoreo = seguimientoServicio.ListarTiposMonitoreo(ObtenerFiltro(), (Usuario)Session["usuario"]);
        if (lstTipoMonitoreo.Count > 0)
        {
            panelGrilla.Visible = true;
            gvMonitoreo.DataSource = lstTipoMonitoreo;
            gvMonitoreo.DataBind();
            lblTotalRegs.Text = "Registros encontrados: " + lstTipoMonitoreo.Count;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarExportar(true);
        }
        else
        {
            panelGrilla.Visible = false;
            lblTotalRegs.Text = "La consulta no obtuvo resultado";
        }

    }

    protected void gvMonitoreo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            VerError("");
            Seguimiento pMonitoreo = new Seguimiento();
            MatrizServices matrizServicio = new MatrizServices();
            List<Matriz> lstMatriz = new List<Matriz>();

            pMonitoreo.cod_monitoreo = Convert.ToInt64(gvMonitoreo.DataKeys[e.RowIndex].Values[0]);

            string filtro = " WHERE R.COD_MONITOREO = " + pMonitoreo.cod_monitoreo + " AND R.VALOR_RRESIDUAL > 0";

            lstMatriz = matrizServicio.ListarMatrizMonitoreo(0, filtro, (Usuario)Session["usuario"]);
            
            if (lstMatriz.Count == 0)
            {
                seguimientoServicio.EliminarTipoMonitoreo(pMonitoreo, (Usuario)Session["usuario"]);
                Actualizar();
            }
            else
            {
                VerError("No se puede eliminar el tipo de monitoreo, se encuentra registrado en la matriz de monitoreo");
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(seguimientoServicio.CodigoProgramaM, "gvMonitoreo_RowDeleting", ex);
        }
    }

    protected void gvMonitoreo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvMonitoreo.DataKeys[e.NewEditIndex].Values[0].ToString();
        Session[seguimientoServicio.CodigoProgramaM + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvMonitoreo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvMonitoreo.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(seguimientoServicio.CodigoProgramaM, "gvMonitoreo_PageIndexChanging", ex);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
}