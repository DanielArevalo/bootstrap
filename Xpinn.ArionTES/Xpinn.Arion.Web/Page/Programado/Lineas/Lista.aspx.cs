using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Programado.Services;
using Xpinn.Programado.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Lista : GlobalWeb
{
    LineasProgramadoServices LineasPrograService = new LineasProgramadoServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(LineasPrograService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlMensaje.eventoClick += btnContinuar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasPrograService.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                panelGrilla.Visible = false;
                CargarValoresConsulta(pConsulta, LineasPrograService.CodigoPrograma);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasPrograService.GetType().Name + "L", "Page_Load", ex);
        }

    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, LineasPrograService.CodigoPrograma);
        gvLista.DataSource = null;
        panelGrilla.Visible = false;
        lblTotalRegs.Visible = false;
        lblInfo.Visible = false;
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            Actualizar();
        }
    }

    private void Actualizar()
    {
        try
        {
            List<LineasProgramado> lstConsulta = new List<LineasProgramado>();
            String filtro = obtFiltro();
            lstConsulta = LineasPrograService.ListarLineasProgramado(filtro, (Usuario)Session["usuario"]);

            gvLista.PageSize = 20;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                panelGrilla.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                lblInfo.Visible = false;
                gvLista.DataBind();
            }
            else
            {
                panelGrilla.Visible = false;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }
            Session.Add(LineasPrograService.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasPrograService.CodigoPrograma, "Actualizar", ex);
        }
    }

    private string obtFiltro()
    {
        String filtro = String.Empty;
        if (txtCodigo.Text.Trim() != "")
            filtro += " and L.COD_LINEA_PROGRAMADO = " + txtCodigo.Text.Trim(); 
        if (txtDescripcion.Text.Trim() != "")
            filtro += " and L.NOMBRE like '%" + txtDescripcion.Text.Trim() + "%'";

        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "where " + filtro;
        }

        return filtro;
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();
        Session[LineasPrograService.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
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
            BOexcepcion.Throw(LineasPrograService.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }



    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {        
        int conseID = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        Session["Index"] = conseID;
        ctlMensaje.MostrarMensaje("Desea eliminar el registro seleccionado?");
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        try
        {
            LineasPrograService.EliminarLineaProgramado(Convert.ToInt64(Session["Index"]), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }

}