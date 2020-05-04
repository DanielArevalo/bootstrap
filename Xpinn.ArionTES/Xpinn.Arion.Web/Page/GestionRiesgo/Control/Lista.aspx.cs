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
            VisualizarOpciones(seguimientoServicio.CodigoProgramaC, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarExportar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(seguimientoServicio.CodigoProgramaC, "Page_PreInit", ex);
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
            BOexcepcion.Throw(seguimientoServicio.CodigoProgramaC, "Page_Load", ex);
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
        if (gvControl.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=ReporteControl.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            ExpGrilla expGrilla = new ExpGrilla();
            sw = expGrilla.ObtenerGrilla(gvControl, null);
            Response.Write("<div>" + expGrilla.style + "</div>");
            Response.Output.Write("<div>" + sw.ToString() + "</div>");
            Response.Flush();
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }

    private void CargarListas()
    {
        PoblarListas poblar = new PoblarListas();
        poblar.PoblarListaDesplegable("GR_AREA_FUNCIONAL", "COD_AREA, DESCRIPCION", "", "1", ddlAreaEjecucion, (Usuario)Session["usuario"]);
        poblar.PoblarListaDesplegable("GR_CARGO_ORGANIGRAMA", "COD_CARGO, DESCRIPCION", "", "1", ddlResponsable, (Usuario)Session["usuario"]);

        ddlClase.Items.Insert(0, new ListItem("Seleccione un item", ""));
        ddlClase.Items.Insert(1, new ListItem("Preventivo", "1"));
        ddlClase.Items.Insert(2, new ListItem("Detectivo", "2"));
        ddlClase.Items.Insert(3, new ListItem("Correctivo", "3"));
        ddlClase.DataBind();

        ddlGradoAceptacion.Items.Insert(0, new ListItem("Seleccione un item", ""));
        ddlGradoAceptacion.Items.Insert(1, new ListItem("10%", "10"));
        ddlGradoAceptacion.Items.Insert(2, new ListItem("20%", "20"));
        ddlGradoAceptacion.Items.Insert(3, new ListItem("30%", "30"));
        ddlGradoAceptacion.Items.Insert(4, new ListItem("40%", "40"));
        ddlGradoAceptacion.Items.Insert(5, new ListItem("50%", "50"));
        ddlGradoAceptacion.Items.Insert(6, new ListItem("60%", "60"));
        ddlGradoAceptacion.Items.Insert(7, new ListItem("70%", "70"));
        ddlGradoAceptacion.Items.Insert(8, new ListItem("80%", "70"));
        ddlGradoAceptacion.Items.Insert(9, new ListItem("90%", "90"));
        ddlGradoAceptacion.Items.Insert(10, new ListItem("100%", "100"));
        ddlGradoAceptacion.DataBind();
    }

    private Seguimiento ObtenerFiltro()
    {
        Seguimiento pControl = new Seguimiento();
        if (!string.IsNullOrWhiteSpace(txtCodigoControl.Text))
            pControl.cod_control = Convert.ToInt64(txtCodigoControl.Text);
        if (!string.IsNullOrWhiteSpace(txtDescripcionControl.Text))
            pControl.descripcion = txtDescripcionControl.Text;
        if(ddlAreaEjecucion.SelectedValue != "")
            pControl.cod_area = Convert.ToInt64(ddlAreaEjecucion.SelectedValue);
        if (ddlClase.SelectedValue != "")
            pControl.clase = Convert.ToInt64(ddlClase.SelectedValue);
        if (ddlResponsable.SelectedValue != "")
            pControl.cod_cargo = Convert.ToInt64(ddlResponsable.SelectedValue);
        if (ddlGradoAceptacion.SelectedValue != "")
            pControl.grado_aceptacion = Convert.ToInt32(ddlGradoAceptacion.SelectedValue);

        return pControl;
    }

    private void Actualizar()
    {
        List<Seguimiento> lstFormaControl = new List<Seguimiento>();
        
        lstFormaControl = seguimientoServicio.ListarFormasControl(ObtenerFiltro(), (Usuario)Session["usuario"]);
        if (lstFormaControl.Count > 0)
        {
            panelGrilla.Visible = true;
            gvControl.DataSource = lstFormaControl;
            gvControl.DataBind();
            lblTotalRegs.Text = "Registros encontrados: " + lstFormaControl.Count;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarExportar(true);
        }
        else
        {
            panelGrilla.Visible = false;
            lblTotalRegs.Text = "La consulta no obtuvo resultado";
        }

    }

    protected void gvControl_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            VerError("");
            Seguimiento pControl = new Seguimiento();
            MatrizServices matrizServicio = new MatrizServices();
            List<Matriz> lstMatriz = new List<Matriz>();

            pControl.cod_control = Convert.ToInt64(gvControl.DataKeys[e.RowIndex].Values[0]);
            string filtro = " WHERE COD_CONTROL = " + pControl.cod_control + " AND VALOR_RRESIDUAL > 0";

            lstMatriz = matrizServicio.ListarMatrizRResidual(0, filtro, (Usuario)Session["usuario"]);
            
            if (lstMatriz.Count == 0)
            {
                seguimientoServicio.EliminarFormaControl(pControl, (Usuario)Session["usuario"]);
                Actualizar();
            }
            else
            {
                VerError("No se puede eliminar el control, se encuentra registrado en la matriz de riesgo residual");
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(seguimientoServicio.CodigoProgramaC, "gvControl_RowDeleting", ex);
        }
    }

    protected void gvControl_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvControl.DataKeys[e.NewEditIndex].Values[0].ToString();
        Session[seguimientoServicio.CodigoProgramaC + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvControl_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvControl.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(seguimientoServicio.CodigoProgramaC, "gvControl_PageIndexChanging", ex);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
}