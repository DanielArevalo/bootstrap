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
using Xpinn.Seguridad.Entities;
using Xpinn.Seguridad.Services;

partial class Lista : GlobalWeb
{
    private Xpinn.Seguridad.Services.ConsecutivoOficinasService ConsecutivoOficinaServicio = new Xpinn.Seguridad.Services.ConsecutivoOficinasService();
    private Xpinn.Seguridad.Entities.ConsecutivoOficinas consecutivooficinas = new Xpinn.Seguridad.Entities.ConsecutivoOficinas();
    PoblarListas poblar = new PoblarListas();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ConsecutivoOficinaServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConsecutivoOficinaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDllOficinas();
                CargarValoresConsulta(pConsulta, ConsecutivoOficinaServicio.CodigoPrograma);
                if (Session[ConsecutivoOficinaServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConsecutivoOficinaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        //GuardarValoresConsulta(pConsulta, ConsecutivoOficinaServicio.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, ConsecutivoOficinaServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ConsecutivoOficinaServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConsecutivoOficinaServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[ConsecutivoOficinaServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[ConsecutivoOficinaServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            ConsecutivoOficinaServicio.EliminarConsecutivoOficinas(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConsecutivoOficinaServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(ConsecutivoOficinaServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }
    
    private void Actualizar()
    {
        String filtro = obtFiltro(ObtenerValores());
            
        try
        {
            List<Xpinn.Seguridad.Entities.ConsecutivoOficinas> lstConsulta = new List<Xpinn.Seguridad.Entities.ConsecutivoOficinas>();
            lstConsulta = ConsecutivoOficinaServicio.ListarConsecutivoOficinas(filtro, (Usuario)Session["usuario"]);

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

            Session.Add(ConsecutivoOficinaServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConsecutivoOficinaServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.Seguridad.Entities.ConsecutivoOficinas ObtenerValores()
    {
        Xpinn.Seguridad.Entities.ConsecutivoOficinas vOpcion = new Xpinn.Seguridad.Entities.ConsecutivoOficinas();

        if (ddltabla.Text.Trim() != "") 
            vOpcion.tabla = Convert.ToString(ddltabla.Text.Trim());
        if (ddlOficina.Text.Trim() != "")
            vOpcion.cod_oficina = Convert.ToInt32(ddlOficina.Text.Trim());

        return vOpcion;
    }
    private string obtFiltro(ConsecutivoOficinas consecutivooficina)
    {
        String filtro = String.Empty;

        if (ddltabla.SelectedIndex != 0)
            filtro += " and c.tabla like '" + consecutivooficina.tabla + "'";
        else
            filtro += " and UPPER(TRIM(c.tabla)) like '%COMPROBANTE%' or UPPER(TRIM(c.tabla)) like '%CREDITO%' or UPPER(TRIM(c.tabla)) like '%PERSONA' or UPPER(TRIM(c.tabla)) like 'APORTE'";
        if ((ddlOficina.Text.Trim() != "") && ((ddlOficina.Text.Trim() != "0")))

            filtro += " and c.cod_oficina = " + consecutivooficina.cod_oficina + "";    
         
       
        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "  where " + filtro;
        }
        return filtro;
    }

    

    // carga ddl
    void CargarDllOficinas()
    {
        poblar.PoblarListaDesplegable("lineaahorro", ddlOficina, (Usuario)Session["usuario"]);

        Xpinn.Asesores.Data.OficinaData listaOficina = new Xpinn.Asesores.Data.OficinaData();
        Xpinn.Asesores.Entities.Oficina oficina = new Xpinn.Asesores.Entities.Oficina();
        oficina.Estado = 1;
        var lista = listaOficina.ListarOficina(oficina, (Usuario)Session["usuario"]);

        if (lista != null)
        {
            lista.Insert(0, new Xpinn.Asesores.Entities.Oficina { NombreOficina = "Seleccione un Item", IdOficina = 0 });
            ddlOficina.DataSource = lista;
            ddlOficina.DataTextField = "NombreOficina";
            ddlOficina.DataValueField = "IdOficina";
            ddlOficina.DataBind();
        }

        ddltabla.SelectedIndex = 0;       
    }

}