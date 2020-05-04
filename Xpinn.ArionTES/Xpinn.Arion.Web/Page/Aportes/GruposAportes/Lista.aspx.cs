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
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Services;

partial class Lista : GlobalWeb
{
    private Xpinn.Aportes.Services.GrupoLineaAporteServices GrupoAporteServicio = new Xpinn.Aportes.Services.GrupoLineaAporteServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(GrupoAporteServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GrupoAporteServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Actualizar();         
                CargarValoresConsulta(pConsulta, GrupoAporteServicio.CodigoPrograma);
                if (Session[GrupoAporteServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GrupoAporteServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, GrupoAporteServicio.CodigoPrograma);
        Navegar(Pagina.Nuevo);
      Session["operacion"] = "N";
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, GrupoAporteServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, GrupoAporteServicio.CodigoPrograma);
        txtCodLinea.Text = "";          
        gvLista.DataBind();
   
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GrupoAporteServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[GrupoAporteServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);

    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
     
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[GrupoAporteServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
        Session["operacion"] = "";
     
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
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
            BOexcepcion.Throw(GrupoAporteServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

   

    private void Actualizar()
    {      
        try
        {
            List<Xpinn.Aportes.Entities.GrupoLineaAporte> lstConsulta = new List<Xpinn.Aportes.Entities.GrupoLineaAporte>();
            lstConsulta = GrupoAporteServicio.ListarGrupoAporte(ObtenerValores(), (Usuario)Session["usuario"]);
        
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

            Session.Add(GrupoAporteServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GrupoAporteServicio.CodigoPrograma, "Actualizar", ex);
        }
        
    }

    private Xpinn.Aportes.Entities.GrupoLineaAporte ObtenerValores()
    {
        Xpinn.Aportes.Entities.GrupoLineaAporte vAporte = new Xpinn.Aportes.Entities.GrupoLineaAporte();
        if (txtCodLinea.Text.Trim() != "")
            vAporte.cod_linea_aporte = Convert.ToInt32(txtCodLinea.Text.Trim());
     

            return vAporte;
    }

    

    protected void btnInfo_Click(object sender, ImageClickEventArgs e)
    {

    }



    protected void DdlOrdenadorpor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Actualizar();
    }
}