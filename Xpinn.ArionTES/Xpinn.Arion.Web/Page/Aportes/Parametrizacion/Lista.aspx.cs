using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;

public partial class Lista : GlobalWeb
{
    InformacionAdicionalServices InformacionService = new InformacionAdicionalServices();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(InformacionService.CodigoProgramaParametros, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionService.CodigoProgramaParametros, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionService.CodigoProgramaParametros, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Navegar(Pagina.Nuevo);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionService.CodigoProgramaParametros, "btnNuevo_Click", ex);
        }

    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionService.CodigoProgramaParametros, "btnConsultar_Click", ex);
        }
    }

  

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.SelectedRow.Cells[3].Text;
        Session[InformacionService.CodigoProgramaParametros + ".id"] = id;
        Session[InformacionService.CodigoProgramaParametros + ".from"] = "l";
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[InformacionService.CodigoProgramaParametros + ".id"] = id;
        
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int32 id = Convert.ToInt32(gvLista.Rows[e.RowIndex].Cells[2].Text);
            try
            {
                InformacionService.EliminarInformacionAdicional(Convert.ToInt64(id), (Usuario)Session["Usuario"]);
            }
            catch
            {
                VerError("No se puede Eliminar el elemento seleccionado");
            }
            //InformacionService.(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionService.CodigoProgramaParametros, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(InformacionService.CodigoProgramaParametros, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<InformacionAdicional> lstConsulta = new List<InformacionAdicional>();
            lstConsulta = InformacionService.ListarInformacionAdicionalGeneral(ObtenerValores(), (Usuario)Session["usuario"]);
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

            Session.Add(InformacionService.CodigoProgramaParametros + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionService.CodigoProgramaParametros, "Actualizar", ex);
        }
    }

    private InformacionAdicional ObtenerValores()
    {
        InformacionAdicional pInfo = new InformacionAdicional();
        return pInfo;
    }

    void CargaGrillaPorFiltro(string tipo)
    {
        string filtro = tipo;
        List<InformacionAdicional> lstConsulta = new List<InformacionAdicional>();
        lstConsulta = InformacionService.ListarInformacionAdicional(ObtenerValores(), filtro, (Usuario)Session["usuario"]);
        if (lstConsulta.Count > 0)
        {
            gvLista.DataSource = lstConsulta;
            gvLista.DataBind();
        }
        else
        {
            gvLista.DataSource = null;
            gvLista.DataBind();            
        }
        lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
    }

    protected void btnEditaNatural_Click(object sender, EventArgs e)
    {
        CargaGrillaPorFiltro("N");       
    }
    protected void btnEditaJuridico_Click(object sender, EventArgs e)
    {
        CargaGrillaPorFiltro("J");
    }
    protected void btnEditaMenores_Click(object sender, EventArgs e)
    {
        CargaGrillaPorFiltro("M");
    }
    protected void btnEditaGeneral_Click(object sender, EventArgs e)
    {
        Actualizar();
    }
    protected void btnVerNatural_Click(object sender, ImageClickEventArgs e)
    {
        Session[InformacionService.CodigoProgramaParametros + ".id"] = "N";
        Navegar(Pagina.Nuevo);
    }
    protected void btnVerJuridica_Click(object sender, ImageClickEventArgs e)
    {
        Session[InformacionService.CodigoProgramaParametros + ".id"] = "J";
        Navegar(Pagina.Nuevo);
    }
    protected void btnVerMenores_Click(object sender, ImageClickEventArgs e)
    {
        Session[InformacionService.CodigoProgramaParametros + ".id"] = "M";
        Navegar(Pagina.Nuevo);
    }

}