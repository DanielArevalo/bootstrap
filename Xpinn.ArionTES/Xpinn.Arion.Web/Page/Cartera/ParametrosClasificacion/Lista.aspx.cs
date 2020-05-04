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
using Xpinn.Cartera.Services;
using Xpinn.Cartera.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;


partial class Lista : GlobalWeb
{
    private Xpinn.Cartera.Services.ClasificacionCarteraService ClasificacioncarteraServicio = new Xpinn.Cartera.Services.ClasificacionCarteraService();
  
    /// <summary>
    /// Ingrsesar a la funcionalidad y mostrar la tool bar con botón de consulta y limpiar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ClasificacioncarteraServicio.CodigoProgramaParametros, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ClasificacioncarteraServicio.CodigoProgramaParametros, "Page_PreInit", ex);
        }
    }

    /// <summary>
    /// Cargar la página, llenar combos de listado de oficinas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
       
        try
        {
            if (!IsPostBack)
            {
                Actualizar();
                CargarValoresConsulta(pConsulta, ClasificacioncarteraServicio.CodigoProgramaParametros);
                if (Session[ClasificacioncarteraServicio.CodigoProgramaParametros + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ClasificacioncarteraServicio.CodigoProgramaParametros, "Page_Load", ex);
        }
    }

    /// <summary>
    /// Inhabilitar botón de nuevo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {

    }

    /// <summary>
    /// Botón de consulta de los datos del crédito según los filtros realizados.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();

        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, ClasificacioncarteraServicio.CodigoProgramaParametros);
            Actualizar();
        }    
    }

    /// <summary>
    /// Boton para limpiar los campos de la pantalla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ClasificacioncarteraServicio.CodigoProgramaParametros);
    }

    /// <summary>
    /// Inhabilitar botón de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    /// <summary>
    /// Acción para cuando se pasa a la siguiente página en la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[ClasificacioncarteraServicio.CodigoProgramaParametros + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    /// <summary>
    /// Evento cuando se selecciona un crédito de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[1].Text;

        Session[ClasificacioncarteraServicio.CodigoProgramaParametros + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    /// <summary>
    /// Evento para borrar datos que en esta funcionalidad no aplica
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    /// <summary>
    /// Esto es cuando se da click para cambio de página de la grilla
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
            BOexcepcion.Throw(ClasificacioncarteraServicio.CodigoProgramaParametros, "gvLista_PageIndexChanging", ex);
        }
    }
   
    /// <summary>
    /// Esto es para actualizar la grilla
    /// </summary>
    private void Actualizar()
    {
        try
        {
            List<Xpinn.Cartera.Entities.ClasificacionCartera> lstConsulta = new List<Xpinn.Cartera.Entities.ClasificacionCartera>();
            lstConsulta = ClasificacioncarteraServicio.ListarClasificacion((Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                //ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(ClasificacioncarteraServicio.CodigoProgramaParametros + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ClasificacioncarteraServicio.CodigoProgramaParametros, "Actualizar", ex);
        }
    }

   

}