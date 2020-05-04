using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Lista : GlobalWeb
{
    EstructuraRecaudoServices EstructuraRecaudo = new EstructuraRecaudoServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(EstructuraRecaudo.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstructuraRecaudo.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //if (Session[EstructuraRecaudo.GetType().Name + ".consulta"] != null)
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstructuraRecaudo.GetType().Name + "L", "Page_Load", ex);
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
            List<Estructura_Carga> lstConsulta = new List<Estructura_Carga>();
            String filtro = obtFiltro(ObtenerValores());
            lstConsulta = EstructuraRecaudo.ListarEstructuraRecaudo(ObtenerValores(), (Usuario)Session["usuario"],filtro,1);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            Session["filtro"] = filtro;
            Session["obtenciondatos"] = ObtenerValores();
            if (lstConsulta.Count > 0)
            {
                Session["ListaEstructura"] = lstConsulta;
                panelGrilla.Visible = true;
                //gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
            }
            else
            {
                Session["ListaEstructura"] = null;
                panelGrilla.Visible = false;
                //gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(EstructuraRecaudo.CodigoPrograma + ".consulta", 1);
        }
            catch (Exception ex)
        {
            BOexcepcion.Throw(EstructuraRecaudo.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Estructura_Carga ObtenerValores()
    {
        Estructura_Carga vEstruc = new Estructura_Carga();
        if (txtCodigo.Text.Trim() != "")
            vEstruc.cod_estructura_carga = Convert.ToInt32(txtCodigo.Text.Trim());
        if (txtNombre.Text.Trim() != "")
            vEstruc.descripcion = Convert.ToString(txtNombre.Text.Trim().ToUpper());

        return vEstruc;
    }

    private string obtFiltro(Estructura_Carga Estruc)
    {
        String filtro = String.Empty;
        if (txtCodigo.Text.Trim() != "")
            filtro += " and esc.cod_estructura_carga = " + Estruc.cod_estructura_carga;
        if (txtNombre.Text.Trim() != "")
            filtro += " and descripcion like '%" + Estruc.descripcion+"%'";
      
        //filtro += " and estado ='G'";
        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "where " + filtro;
        }
        return filtro;
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[EstructuraRecaudo.CodigoPrograma + ".id"] = id;
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
            BOexcepcion.Throw(EstructuraRecaudo.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }


    protected void btnFinal_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        Session["Index"] = conseID;
        mpeConfirmar.Show();
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        try
        {
            EstructuraRecaudo.EliminarEstructuraCarga(Convert.ToInt32(Session["Index"]), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EstructuraRecaudo.CodigoPrograma + "L", "gvLista_RowDeleting", ex);
        }
        
        mpeConfirmar.Hide();
    }
    protected void btnParar_Click(object sender, EventArgs e)
    {
        mpeConfirmar.Hide();
    }
}