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
    private Xpinn.Tesoreria.Services.SoporteCajService SoporteCajServicio = new Xpinn.Tesoreria.Services.SoporteCajService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(SoporteCajServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoporteCajServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, SoporteCajServicio.CodigoPrograma);
                if (Session[SoporteCajServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoporteCajServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove(SoporteCajServicio.CodigoPrograma + ".id");
        GuardarValoresConsulta(pConsulta, SoporteCajServicio.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        GuardarValoresConsulta(pConsulta, SoporteCajServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, SoporteCajServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnEliminar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoporteCajServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[SoporteCajServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[SoporteCajServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        VerError("");
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            Xpinn.Tesoreria.Entities.SoporteCaj vSoporte = new Xpinn.Tesoreria.Entities.SoporteCaj();
            vSoporte = SoporteCajServicio.ConsultarSoporteCaj(id, (Usuario)Session["usuario"]);
            if (vSoporte != null)
            {
                vSoporte.id_arqueo = vSoporte.id_arqueo == null ? 0 : vSoporte.id_arqueo;
                if (vSoporte.num_comp > 0 && vSoporte.tipo_comp > 0)
                    VerError("No se puede eliminar el soporte, se encuentra contabilizado");
                else if (vSoporte.id_arqueo != 0 )
                    VerError("No se puede eliminar el soporte, se encuentra registrado en un arqueo");
                else
                {
                    SoporteCajServicio.EliminarSoporteCaj(id, (Usuario)Session["usuario"]);
                    Actualizar();
                }
            }
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoporteCajServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(SoporteCajServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Tesoreria.Entities.SoporteCaj> lstConsulta = new List<Xpinn.Tesoreria.Entities.SoporteCaj>();
            lstConsulta = SoporteCajServicio.ListarSoporteCaj(ObtenerValores(), (Usuario)Session["usuario"]);

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

            Session.Add(SoporteCajServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoporteCajServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.Tesoreria.Entities.SoporteCaj ObtenerValores()
    {
        Xpinn.Tesoreria.Entities.SoporteCaj vSoporteCaj = new Xpinn.Tesoreria.Entities.SoporteCaj();

        if (txtCodigo.Text.Trim() != "")
            vSoporteCaj.idsoporte = Convert.ToInt32(txtCodigo.Text.Trim());
        if (txtDescripcion.Text.Trim() != "")
            vSoporteCaj.descripcion = Convert.ToString(txtDescripcion.Text.Trim());
        if (txtFecha.TieneDatos)
            vSoporteCaj.fecha = txtFecha.ToDateTime;
        return vSoporteCaj;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        gvLista.AllowPaging = false;
        Actualizar();

        ExportarGridViewEnExcel(gvLista, "Recibos");

        gvLista.AllowPaging = true;
        Actualizar();
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