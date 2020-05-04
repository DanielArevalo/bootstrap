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

partial class Lista : GlobalWeb
{
    private Xpinn.Tesoreria.Services.AreasCajService AreasCajServicio = new Xpinn.Tesoreria.Services.AreasCajService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AreasCajServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AreasCajServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, AreasCajServicio.CodigoPrograma);
                if (Session[AreasCajServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AreasCajServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove(AreasCajServicio.CodigoPrograma + ".id");
        GuardarValoresConsulta(pConsulta, AreasCajServicio.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, AreasCajServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, AreasCajServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnEliminar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AreasCajServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[AreasCajServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[AreasCajServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            VerError("");
            Int64 id = Convert.ToInt64(e.Keys[0]);
            Xpinn.Tesoreria.Services.SoporteCajService soporteServicio = new Xpinn.Tesoreria.Services.SoporteCajService();
            List<Xpinn.Tesoreria.Entities.SoporteCaj> lstSoportes = new List<Xpinn.Tesoreria.Entities.SoporteCaj>();
            Xpinn.Tesoreria.Entities.SoporteCaj pSoporteCaj = new Xpinn.Tesoreria.Entities.SoporteCaj();
            pSoporteCaj.idarea = Convert.ToInt32(id);
            lstSoportes = soporteServicio.ListarSoporteCaj(pSoporteCaj, (Usuario)Session["usuario"]);
            if (lstSoportes.Count > 0)
                VerError("No se puede eliminar el area de caja menor, tiene recibos registrados");
            else
            {
                AreasCajServicio.EliminarAreasCaj(id, (Usuario)Session["usuario"]);
                Actualizar();
            }
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AreasCajServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(AreasCajServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Tesoreria.Entities.AreasCaj> lstConsulta = new List<Xpinn.Tesoreria.Entities.AreasCaj>();
            lstConsulta = AreasCajServicio.ListarAreasCaj(ObtenerValores(), (Usuario)Session["usuario"]);

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

            Session.Add(AreasCajServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AreasCajServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.Tesoreria.Entities.AreasCaj ObtenerValores()
    {
        Xpinn.Tesoreria.Entities.AreasCaj vAreasCaj = new Xpinn.Tesoreria.Entities.AreasCaj();

        if (txtCodigo.Text.Trim() != "")
            vAreasCaj.idarea = Convert.ToInt32(txtCodigo.Text.Trim());
        return vAreasCaj;
    }

}