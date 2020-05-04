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
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    private Xpinn.ActivosFijos.Services.ActivosFijoservices ActivosFijoservicio = new Xpinn.ActivosFijos.Services.ActivosFijoservices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ActivosFijoservicio.CodigoProgramaBaja, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaBaja, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, ActivosFijoservicio.CodigoProgramaBaja);
                if (Session[ActivosFijoservicio.CodigoProgramaBaja + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaBaja, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, ActivosFijoservicio.CodigoProgramaBaja);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ActivosFijoservicio.CodigoProgramaBaja);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnEliminar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaBaja + "L", "gvLista_RowDataBound", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[ActivosFijoservicio.CodigoProgramaBaja + ".id"] = id;
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
            BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaBaja, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.ActivosFijos.Entities.ActivoFijo> lstConsulta = new List<Xpinn.ActivosFijos.Entities.ActivoFijo>();
            lstConsulta = ActivosFijoservicio.ListarActivoFijo(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                Session["DTActivosFijos"] = lstConsulta;
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(ActivosFijoservicio.CodigoProgramaBaja + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaBaja, "Actualizar", ex);
        }
    }

    private Xpinn.ActivosFijos.Entities.ActivoFijo ObtenerValores()
    {
        Xpinn.ActivosFijos.Entities.ActivoFijo vActivoFijo = new Xpinn.ActivosFijos.Entities.ActivoFijo();
        vActivoFijo.estado = 1;
        if (txtCodigo.Text.Trim() != "")
            vActivoFijo.cod_act = Convert.ToInt64(txtCodigo.Text.Trim());
        return vActivoFijo;
    }


}