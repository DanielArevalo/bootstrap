using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;

using Xpinn.ConciliacionBancaria.Services;
using Xpinn.ConciliacionBancaria.Entities;
using Xpinn.Util;


public partial class Lista : GlobalWeb
{
    EstructuraExtractoServices EstructuraService = new EstructuraExtractoServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(EstructuraService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstructuraService.GetType().Name + "L", "Page_PreInit", ex);
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
            BOexcepcion.Throw(EstructuraService.GetType().Name + "L", "Page_Load", ex);
        }

    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
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
            List<EstructuraExtracto> lstConsulta = new List<EstructuraExtracto>();
            String filtro = obtFiltro();
            lstConsulta = EstructuraService.ListarEstructuraExtracto(filtro, (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                panelGrilla.Visible = true;
                lblTotalRegs.Visible = true;
                Label2.Visible = false;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
            }
            else
            {
                panelGrilla.Visible = false;
                lblTotalRegs.Visible = false;
                Label2.Visible = true;
            }

            Session.Add(EstructuraService.CodigoPrograma + ".consulta", 1);
        }
            catch (Exception ex)
        {
            BOexcepcion.Throw(EstructuraService.CodigoPrograma, "Actualizar", ex);
        }
    }

    
    private string obtFiltro()
    {
        String filtro = String.Empty;
        if (txtCodigo.Text.Trim() != "")
            filtro += " and e.idestructuraextracto = " + txtCodigo.Text;
        if (txtNombre.Text.Trim() != "")
            filtro += " and e.nombre like '%" + txtNombre.Text + "%'";
      
        return filtro;
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[EstructuraService.CodigoPrograma + ".id"] = id;
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
            BOexcepcion.Throw(EstructuraService.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        Session["Index"] = conseID;
        ctlMensaje.MostrarMensaje("Desea realizar la eliminacion del registro seleccionado?");
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            EstructuraService.EliminarEstructuraCarga(Convert.ToInt32(Session["Index"]), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstructuraService.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }

}