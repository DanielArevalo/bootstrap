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
using Xpinn.Auxilios.Entities;
using Xpinn.Auxilios.Services;

partial class Lista : GlobalWeb
{
    LineaAuxilioServices LineaAux = new LineaAuxilioServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(LineaAux.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAux.CodigoPrograma, "Page_PreInit", ex);
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
            BOexcepcion.Throw(LineaAux.CodigoPrograma, "Page_Load", ex);
        }
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



    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
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
            BOexcepcion.Throw(LineaAux.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (!ValidarAccionesGrilla("UPDATE"))
        {
            e.NewEditIndex = -1;
            return;
        }
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[LineaAux.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (!ValidarAccionesGrilla("DELETE"))
            return;
        int id = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        Session["ID"] = id;
        ctlMensaje.MostrarMensaje("Desea realizar la eliminación del Servicio?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            LineaAux.EliminarLineaAuxilio(Convert.ToInt32(Session["ID"]), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAux.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }

    
    private void Actualizar()
    {
        try
        {
            List<LineaAuxilio> lstConsulta = new List<LineaAuxilio>();
            String filtro = obtFiltro(ObtenerValores());

            lstConsulta = LineaAux.ListarLineaAuxilio(ObtenerValores(), (Usuario)Session["usuario"], filtro);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;           

            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;                
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();                
                
            }
            else
            {
                panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
            }

            Session.Add(LineaAux.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAux.CodigoPrograma, "Actualizar", ex);
        }
    }

    private LineaAuxilio ObtenerValores()
    {
        LineaAuxilio vAuxi = new LineaAuxilio();
        if (txtCodigo.Text.Trim() != "")
            vAuxi.cod_linea_auxilio = txtCodigo.Text.Trim();
        if (txtDescripcion.Text != "")
            vAuxi.descripcion = txtDescripcion.Text;
        
        return vAuxi;
    }



    private string obtFiltro(LineaAuxilio linea)
    {
        String filtro = String.Empty;

        if (txtCodigo.Text.Trim() != "")
            filtro += " and l.cod_linea_auxilio = " + linea.cod_linea_auxilio;       
        if (txtDescripcion.Text.Trim() != "")
            filtro += " and l.descripcion like '%" + linea.descripcion + "%'";       

        return filtro;
    }


   
}