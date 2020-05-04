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
    private Xpinn.ActivosFijos.Services.Articuloservices Articuloservices = new Xpinn.ActivosFijos.Services.Articuloservices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(Articuloservices.CodigoPrograma, "L");
            
            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Articuloservices.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           
            if (!IsPostBack)
            {
                txtTipoArticulo.Text = "";
                btnExportar.Visible = false;
                CargarValoresConsulta(pConsulta, Articuloservices.CodigoPrograma);
                if (Session[Articuloservices.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Articuloservices.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session[Articuloservices.CodigoPrograma + ".id"] = null;
        GuardarValoresConsulta(pConsulta, Articuloservices.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, Articuloservices.CodigoPrograma);
        Actualizar();
        txtTipoArticulo.Text = "";

    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        LimpiarValoresConsulta(pConsulta, Articuloservices.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnEliminar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Articuloservices .CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[Articuloservices.CodigoPrograma + ".id"] = id;
        txtTipoArticulo.Text = "";

        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[Articuloservices.CodigoPrograma + ".id"] = id;
        txtTipoArticulo.Text = "";

        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            Articuloservices.EliminarArticulo  (id, (Usuario)Session["usuario"]);
            Actualizar();
            txtTipoArticulo.Text = "";
         
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(ActivosFijoservicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(Articuloservices.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        VerError("");
        try
        {
            List<Xpinn.ActivosFijos.Entities.Articulo  > lstConsulta = new List<Xpinn.ActivosFijos.Entities.Articulo>();
            lstConsulta = Articuloservices.ListarArticulo(ObtenerValores(), (Usuario)Session["usuario"]);
            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                btnExportar.Visible = true;
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                Session["DTActivosFijos"] = lstConsulta;
                gvLista.DataBind();
                //ValidarPermisosGrilla(gvLista);
                Site toolbar = (Site)Master;
                toolbar.MostrarCancelar(true);
            }
            else
            {
                btnExportar.Visible = false;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(Articuloservices.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Articuloservices.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.ActivosFijos.Entities.Articulo   ObtenerValores()
    {
        Xpinn.ActivosFijos.Entities.Articulo vArticulo = new Xpinn.ActivosFijos.Entities.Articulo();
        if (txtTipoArticulo.Text.Trim() != "")
            vArticulo.idarticulo    = Convert.ToInt64(txtTipoArticulo.Text.Trim());
        return vArticulo;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvLista);  
    }

    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=ActivosFijos.xls");
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