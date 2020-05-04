using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Entities;

public partial class Lista : GlobalWeb
{
    private Xpinn.Asesores.Services.TiposDocCobranzasServices tipoDocumentoServicio = new Xpinn.Asesores.Services.TiposDocCobranzasServices();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(tipoDocumentoServicio.CodigoPrograma, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoDocumentoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, tipoDocumentoServicio.CodigoPrograma);
                if (Session[tipoDocumentoServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoDocumentoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GuardarValoresConsulta(pConsulta, tipoDocumentoServicio.CodigoPrograma);
            Navegar(Pagina.Nuevo);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoDocumentoServicio.CodigoPrograma, "btnNuevo_Click", ex);
        }

    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GuardarValoresConsulta(pConsulta, tipoDocumentoServicio.CodigoPrograma);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoDocumentoServicio.CodigoPrograma, "btnConsultar_Click", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, tipoDocumentoServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnEliminar");
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.SelectedRow.Cells[3].Text;
        Session[tipoDocumentoServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[3].Text;
        Session[tipoDocumentoServicio.CodigoPrograma + ".id"] = id;
        String tipo = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[tipo] = tipo; 
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int32 id = Convert.ToInt32(gvLista.Rows[e.RowIndex].Cells[3].Text);
            tipoDocumentoServicio.EliminarTiposDocumento(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoDocumentoServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(tipoDocumentoServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<TiposDocCobranzas> lstConsulta = new List<TiposDocCobranzas>();
            lstConsulta = tipoDocumentoServicio.ListarTiposDocumentoCobranzas(ObtenerValores(), (Usuario)Session["usuario"]);
            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            }

            Session.Add(tipoDocumentoServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoDocumentoServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private TiposDocCobranzas ObtenerValores()
    {
        TiposDocCobranzas TipoDoc = new TiposDocCobranzas();
        return TipoDoc;
    }
}