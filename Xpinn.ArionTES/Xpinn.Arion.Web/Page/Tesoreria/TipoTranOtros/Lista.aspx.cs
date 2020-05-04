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
using System.Globalization;
using Xpinn.Caja.Entities;
using Xpinn.Caja.Services;

public partial class Lista : GlobalWeb
{
    TipoOperacionService objOpercion = new TipoOperacionService();
    TipoOperacion entiti = new TipoOperacion();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(objOpercion.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objOpercion.CodigoPrograma, "Page_PreInit", ex);
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
            BOexcepcion.Throw(objOpercion.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, objOpercion.CodigoPrograma);
        Session[objOpercion.CodigoPrograma + ".id"] = null;
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        gvLista.DataBind();
        lblTotalRegs.Text = "";
        txtCodigo.Text = "";
        txtDescripcion.Text = "";
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objOpercion.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[objOpercion.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();
        String descripcion = gvLista.DataKeys[e.NewEditIndex].Values[1].ToString();
        String tipo_movimiento = gvLista.Rows[e.NewEditIndex].Cells[4].Text;        

        Session["descripcion"] = descripcion;
        Session["tipo_movimiento"] = tipo_movimiento;
        Session[objOpercion.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Int64 id = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        Session["ID"] = id;
        ctlMensaje.MostrarMensaje("Desea realizar la eliminación?");
    }

    protected String getFiltro()
    {
        String Filtro = "";

        if (txtCodigo.Text.Trim() != "")
            Filtro += " and t.tipo_tran = " + txtCodigo.Text.Trim();
        if (txtDescripcion.Text.Trim() != "")
            Filtro += " and t.descripcion = '" + txtDescripcion.Text.Trim()+"'";
         Filtro += " and t.tipo_producto = " + 7;
       
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
            BOexcepcion.Throw(objOpercion.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    private void Actualizar()
    {
        VerError("");
        try
        {
            List<TipoOperacion> lstConsulta = new List<TipoOperacion>();
            lstConsulta = objOpercion.ListarTipoOpGridServices((Usuario)Session["usuario"], getFiltro());
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
            Session.Add(objOpercion.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objOpercion.CodigoPrograma, "Actualizar", ex);
        }

    }//40205


    private void btnContinuar_Click(object sender, EventArgs e)
    {
        if (Session["ID"] != null)
        {
            try
            {
                objOpercion.EliminarTipoOpeServices((Int64)Session["ID"], (Usuario)Session["usuario"]);
                Actualizar();
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
        }
    }
}