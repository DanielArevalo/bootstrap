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
using Xpinn.Tesoreria.Entities;

partial class Lista : GlobalWeb
{
    Xpinn.Contabilidad.Services.ProcesoContableService ProcesoContableServicio = new Xpinn.Contabilidad.Services.ProcesoContableService();
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ProcesoContableServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesoContableServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["Usuario"];
            if (!IsPostBack)
            {
                LlenarCombos();
                CargarValoresConsulta(pConsulta, ProcesoContableServicio.CodigoPrograma);
                if (Session[ProcesoContableServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesoContableServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void LlenarCombos()
    {
        Xpinn.Tesoreria.Services.OperacionServices anulacionservices = new Xpinn.Tesoreria.Services.OperacionServices();

        var lstComprobantes = anulacionservices.cobocomprobantes(_usuario);

        ctllistar.ValueField = "TIPO_COMP"; 
        ctllistar.TextField = "DESCRIPCION";
        ctllistar.BindearControl(lstComprobantes);

        Xpinn.Caja.Services.TipoOperacionService TipOpeService = new Xpinn.Caja.Services.TipoOperacionService();
        Xpinn.Caja.Entities.TipoOperacion TipOpe = new Xpinn.Caja.Entities.TipoOperacion();
        ddloperacion.DataSource = TipOpeService.ListarTipoOpe(_usuario);
        ddloperacion.DataTextField = "nom_tipo_operacion";
        ddloperacion.DataValueField = "cod_operacion";
        ddloperacion.DataBind();

        //ddloperacion.DataSource = anulacionservices.combooperacion((Usuario)Session["Usuario"]);
        //ddloperacion.DataTextField = "TIPO_OPE";
        //ddloperacion.DataValueField = "TIPO_OPE";
        //ddloperacion.DataBind();
        ddloperacion.Items.Insert(0, new ListItem("", "0"));
    }


    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove(ProcesoContableServicio.CodigoPrograma + ".id"); 
        GuardarValoresConsulta(pConsulta, ProcesoContableServicio.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, ProcesoContableServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ProcesoContableServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesoContableServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[ProcesoContableServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[3].Text;
        Session[ProcesoContableServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            //ProcesoContableServicio.EliminarProcesoContable(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesoContableServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(ProcesoContableServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Contabilidad.Entities.ProcesoContable> lstConsulta = new List<Xpinn.Contabilidad.Entities.ProcesoContable>();
            lstConsulta = ProcesoContableServicio.ListarProcesoContable(ObtenerValores(), _usuario);

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

            Session.Add(ProcesoContableServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesoContableServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.Contabilidad.Entities.ProcesoContable ObtenerValores()
    {
        Xpinn.Contabilidad.Entities.ProcesoContable vProcesoContable = new Xpinn.Contabilidad.Entities.ProcesoContable();
        if (txtCodigo.Text.Trim() != "")
            vProcesoContable.cod_proceso = Convert.ToInt64(txtCodigo.Text.Trim());
        if (ddloperacion.SelectedValue.Trim() != "")
            vProcesoContable.tipo_ope = Convert.ToInt64(ddloperacion.SelectedValue.Trim());
        if (!string.IsNullOrWhiteSpace(ctllistar.Codigo))
            vProcesoContable.tipo_comp = Convert.ToInt64(ctllistar.Codigo);

        return vProcesoContable;
    }


}