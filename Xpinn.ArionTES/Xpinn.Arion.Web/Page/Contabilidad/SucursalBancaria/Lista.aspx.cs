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

partial class Lista : GlobalWeb
{
    private Xpinn.Contabilidad.Services.SucursalBancariaService SucursalBancariaServicio = new Xpinn.Contabilidad.Services.SucursalBancariaService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(SucursalBancariaServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SucursalBancariaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, SucursalBancariaServicio.CodigoPrograma);
                if (Session[SucursalBancariaServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SucursalBancariaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, SucursalBancariaServicio.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, SucursalBancariaServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, SucursalBancariaServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SucursalBancariaServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[SucursalBancariaServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[3].Text;
        Session[SucursalBancariaServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            SucursalBancariaServicio.EliminarSucursalBancaria(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SucursalBancariaServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(SucursalBancariaServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Contabilidad.Entities.SucursalBancaria> lstConsulta = new List<Xpinn.Contabilidad.Entities.SucursalBancaria>();
            lstConsulta = SucursalBancariaServicio.ListarSucursalBancaria(ObtenerValores(), (Usuario)Session["usuario"]);

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

            Session.Add(SucursalBancariaServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SucursalBancariaServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.Contabilidad.Entities.SucursalBancaria ObtenerValores()
    {
        Xpinn.Contabilidad.Entities.SucursalBancaria vSucursalBancaria = new Xpinn.Contabilidad.Entities.SucursalBancaria();
        //if (txtCodCuenta.Text.Trim() != "")
        //    vPlanCuentas.cod_cuenta = Convert.ToString(txtCodCuenta.Text.Trim());
        //if (txtNombre.Text.Trim() != "")
        //    vPlanCuentas.nombre = Convert.ToString(txtNombre.Text.Trim());

        return vSucursalBancaria;
    }


}