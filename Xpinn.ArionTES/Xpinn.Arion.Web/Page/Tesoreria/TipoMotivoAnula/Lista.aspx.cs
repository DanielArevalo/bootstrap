using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Seguridad.Services;
using Xpinn.Seguridad.Entities;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Caja.Services.TipoMotivoAnuService perfilServicio = new Xpinn.Caja.Services.TipoMotivoAnuService();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[perfilServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(perfilServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(perfilServicio.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_CLick;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;

            mvPrincipal.ActiveViewIndex = 0;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
            }


        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }




    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        
        Navegar(Pagina.Nuevo);
    }

    /// <summary>
    /// Crear los datos del perfil
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>



    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[perfilServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();
        String descripcion = gvLista.DataKeys[e.NewEditIndex].Values[1].ToString(); ;
        Session["descripcion"] = descripcion;
        Session[perfilServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Int64 id = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        Session["ID"] = id;
        ctlMensaje.MostrarMensaje("Desea realizar la eliminación?");
        perfilServicio.EliminarTipoMotivoAnus(id, (Usuario)Session["usuario"]);
        Actualizar();
    }


    protected String getFiltro()
    {
        String Filtro = " where 1=1";

        if (txtCod_opcion.Text != "")
            Filtro += " and tipo_motivo = " + txtCod_opcion.Text.Trim();
        if (txtdescripcion.Text.Trim() != "")
            Filtro += " and descripcion like '%" + txtdescripcion.Text.Trim() + "%'";
        return Filtro;
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
            BOexcepcion.Throw(perfilServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }



    private void Actualizar()
    {
        VerError("");
        try
        {
            Xpinn.Caja.Entities.TipoMotivoAnu entidad = new Xpinn.Caja.Entities.TipoMotivoAnu();
            List<Xpinn.Caja.Entities.TipoMotivoAnu> lstConsulta = new List<Xpinn.Caja.Entities.TipoMotivoAnu>();
            lstConsulta = perfilServicio.ListarTipoMotivoAnus(entidad,(Usuario)Session["usuario"], getFiltro());
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
            Session.Add(perfilServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoPrograma, "Actualizar", ex);
        }

    }//40205


    protected void btnConsultar_CLick(object sender, EventArgs e)
    {
        Actualizar();
    }
    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        gvLista.Visible = false;
        txtCod_opcion.Text = "";
        txtcodigo.Text="";
        txtdescripcion.Text = "";
        lblInfo.Text = "";
        lblTotalRegs.Text = "";
    }
    
    

}