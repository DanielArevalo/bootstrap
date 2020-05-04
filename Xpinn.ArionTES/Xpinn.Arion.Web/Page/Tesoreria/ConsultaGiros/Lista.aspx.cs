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
            VisualizarOpciones(GiroServicio.CodigoProgramaConsulta, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GiroServicio.CodigoProgramaConsulta, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                VisualizarOpciones(GiroServicio.CodigoProgramaConsulta, "L");
                CargarListas();
                CargarValoresConsulta(pConsulta, GiroServicio.CodigoProgramaConsulta);
                if (Session[GiroServicio.CodigoProgramaConsulta + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GiroServicio.CodigoProgramaConsulta, "Page_Load", ex);
        }
    }

    protected void CargarListas()
    {
        ddlEstado.DataSource = ListaEstadosGiro();
        ddlEstado.DataValueField = "codigo";
        ddlEstado.DataTextField = "descripcion";
        ddlEstado.AppendDataBoundItems = true;
        ddlEstado.Items.Insert(0, new ListItem("Seleccione un item", "-1"));
        ddlEstado.SelectedIndex = 0;
        ddlEstado.DataBind();
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove(GiroServicio.CodigoProgramaConsulta + ".id");
        GuardarValoresConsulta(pConsulta, GiroServicio.CodigoProgramaConsulta);
        Navegar(Pagina.Nuevo);
    }
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, GiroServicio.CodigoProgramaConsulta);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtFechaAprobacion.Text = "";
        LimpiarValoresConsulta(pConsulta, GiroServicio.CodigoProgramaConsulta);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnEliminar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GiroServicio.CodigoProgramaConsulta + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[GiroServicio.CodigoProgramaConsulta + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[GiroServicio.CodigoProgramaConsulta + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            GiroServicio.AnularGiro(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GiroServicio.CodigoProgramaConsulta, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(GiroServicio.CodigoProgramaConsulta, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Tesoreria.Entities.Giro> lstConsulta = new List<Xpinn.Tesoreria.Entities.Giro>();
            string pFiltro = "";
            if (ddlEstado.SelectedIndex != 0)
                pFiltro = " g.ESTADO = " + ddlEstado.SelectedValue;

            lstConsulta = GiroServicio.ListarGiroConsulta(ObtenerValores(), pFiltro, ddlOrdenar.SelectedValue,(Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta != null)
            {
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
            }

            Session.Add(GiroServicio.CodigoProgramaConsulta + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GiroServicio.CodigoProgramaConsulta, "Actualizar", ex);
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
        if (txtFechaAprobacion.TieneDatos)
            vGiro.fec_apro = txtFechaAprobacion.ToDateTime;

        if (txtPrimerNombre.Text.Trim() != "")
            vGiro.primer_nombre = Convert.ToString(txtPrimerNombre.Text.Trim());
        if (txtSegundoNombre.Text.Trim() != "")
            vGiro.segundo_nombre = Convert.ToString(txtSegundoNombre.Text.Trim());
        if (txtPrimerApellido.Text.Trim() != "")
            vGiro.primer_apellido = Convert.ToString(txtPrimerApellido.Text.Trim());
        if (txtSegundoApellido.Text.Trim() != "")
            vGiro.segundo_apellido = Convert.ToString(txtSegundoApellido.Text.Trim());

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