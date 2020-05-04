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
using System.Globalization;

public partial class Lista : GlobalWeb
{
    Oficina_ciudadService objOficina = new Oficina_ciudadService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(objOficina.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objOficina.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarCombos();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objOficina.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, objOficina.CodigoPrograma);
        Session[objOficina.CodigoPrograma + ".id"] = null;
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        gvLista.DataBind();
        lblTotalRegs.Text = "";
        ddlCiudad.ClearSelection();
        ddlOficina.ClearSelection();

    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objOficina.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[objOficina.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);

    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[objOficina.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Int64 id = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        Session["ID"] = id;
        ctlMensaje.MostrarMensaje("Desea realizar la eliminación?");
    }

    String getFiltro()
    {
        String Filtro = "";

        if (ddlCiudad.SelectedIndex > 0)
            Filtro += " and c.codciudad = " + ddlCiudad.SelectedValue;
        if (ddlOficina.SelectedIndex > 0)
            Filtro += " and o.cod_oficina = " + ddlOficina.SelectedValue;
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
            BOexcepcion.Throw(objOficina.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {

        try
        {
            List<Oficina_ciudad> lstConsulta = new List<Oficina_ciudad>();
            lstConsulta = objOficina.listaOficinaCiudadServices((Usuario)Session["usuario"], getFiltro());
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
            Session.Add(objOficina.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objOficina.CodigoPrograma, "Actualizar", ex);
        }

    }

    void cargarCombos()
    {
        PoblarListas poblar = new PoblarListas();
        List<Oficina_ciudad> Listddl = new List<Oficina_ciudad>();
        Oficina_ciudad obj = new Oficina_ciudad();
        Listddl = objOficina.ListarOficina_ciudad(obj, (Usuario)Session["usuario"]);

        if (Listddl.Count > 0)
        {
            Listddl.Insert(0, new Oficina_ciudad { Nombre_Oficina = "Seleccione", cod_oficina = -1 });
            foreach (var item in Listddl)
            {
                ddlOficina.Items.Add(item.Nombre_Oficina.ToString());
                ddlOficina.Items.FindByText(item.Nombre_Oficina.ToString()).Value = item.cod_oficina.ToString();
            }
        }
        poblar.PoblarListaDesplegable("ciudades", ddlCiudad, (Usuario)Session["usuario"]);
    }

    private void btnContinuar_Click(object sender, EventArgs e)
    {
        if (Session["ID"] != null)
        {
            objOficina.EliminarOficina_ciudad((Int64)Session["ID"], (Usuario)Session["usuario"]);
            Actualizar();
        }
    }
}