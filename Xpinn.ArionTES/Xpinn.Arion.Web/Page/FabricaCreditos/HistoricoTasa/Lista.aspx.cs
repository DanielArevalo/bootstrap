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
    private Xpinn.FabricaCreditos.Services.HistoricoTasaService HistoricoTasaService = new Xpinn.FabricaCreditos.Services.HistoricoTasaService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(HistoricoTasaService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
     
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(HistoricoTasaService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {               
                llenarcombo();                        
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(HistoricoTasaService.CodigoPrograma, "Page_Load", ex);
        }
    }

    public void llenarcombo()
    {
        ddlHistorico.DataSource = HistoricoTasaService.tipohistorico((Usuario)Session["Usuario"]);
        ddlHistorico.DataTextField = "DESCRIPCION";
        ddlHistorico.DataValueField = "TIPODEHISTORICO";
        ddlHistorico.DataBind();
        ddlHistorico.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));        
    }

    /// <summary>
    /// Método para crear una nueva tasa
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session[HistoricoTasaService.CodigoPrograma + ".id"] = null;
        GuardarValoresConsulta(pConsulta, HistoricoTasaService.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }

    /// <summary>
    /// Método para evento consultar de la MasterPage
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, HistoricoTasaService.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, HistoricoTasaService.CodigoPrograma);
    }

    /// <summary>
    /// Método para confirmar eliminar una tasa histçorica
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(HistoricoTasaService.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    /// <summary>
    /// Método para modificar una tasa
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[HistoricoTasaService.CodigoPrograma + ".id"] = id;
        Response.Redirect("~/Page/FabricaCreditos/HistoricoTasa/Nuevo.aspx");
    }

    /// <summary>
    /// Método para borrar una tasa
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            HistoricoTasaService.EliminarHistorico(id, (Usuario)Session["Usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(HistoricoTasaService.CodigoPrograma, "gvLista_RowDeleting", ex);
        }
    }

    /// <summary>
    ///  Método para cuando cambia de página
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(HistoricoTasaService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Método para actualizar los datos
    /// </summary>
    private void Actualizar()
    {
        gvLista.DataSource = HistoricoTasaService.listarhistorico(ddlHistorico.SelectedValue, (Usuario)Session["Usuario"]);
        gvLista.DataBind();
    }

}