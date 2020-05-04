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
    private Xpinn.FabricaCreditos.Services.CosteoProductosService CosteoProductosServicio = new Xpinn.FabricaCreditos.Services.CosteoProductosService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CosteoProductosServicio.CodigoPrograma, "L");

            Site1 toolBar = (Site1)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            //toolBar.eventoLimpiar += btnLimpiar_Click;

            ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString(); 
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CosteoProductosServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, CosteoProductosServicio.CodigoPrograma);
                if (Session[CosteoProductosServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CosteoProductosServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, CosteoProductosServicio.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, CosteoProductosServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, CosteoProductosServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnEliminar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CosteoProductosServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[CosteoProductosServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;//gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[CosteoProductosServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long id = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[0].Text);
            CosteoProductosServicio.EliminarCosteoProductos(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CosteoProductosServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(CosteoProductosServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.CosteoProductos> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.CosteoProductos>();
            lstConsulta = CosteoProductosServicio.ListarCosteoProductos(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            gvLista.EmptyDataText = "No se encontraron registros";
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                txtCostoTotalMateriales.Text = lstConsulta.Sum(item => item.costo).ToString();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                txtCostoTotalMateriales.Text = "0";
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(CosteoProductosServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CosteoProductosServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.CosteoProductos ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.CosteoProductos vCosteoProductos = new Xpinn.FabricaCreditos.Entities.CosteoProductos();

    if(txtCod_margen.Text.Trim() != "")
        vCosteoProductos.cod_margen = Convert.ToInt64(txtCod_margen.Text.Trim());
    if(txtMateriaprima.Text.Trim() != "")
        vCosteoProductos.materiaprima = Convert.ToString(txtMateriaprima.Text.Trim());
    if(txtUnidadcompra.Text.Trim() != "")
        vCosteoProductos.unidadcompra = Convert.ToString(txtUnidadcompra.Text.Trim());
    if(txtCostounidad.Text.Trim() != "")
        vCosteoProductos.costounidad = Convert.ToInt64(txtCostounidad.Text.Trim());
    if(txtCantidad.Text.Trim() != "")
        vCosteoProductos.cantidad = Convert.ToInt64(txtCantidad.Text.Trim());
    if(txtCosto.Text.Trim() != "")
        vCosteoProductos.costo = Convert.ToInt64(txtCosto.Text.Trim());

        return vCosteoProductos;
    }
    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/MargenVentas/Lista.aspx");
   
    }
    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/ComposicionPasivo/Lista.aspx");
   
    }
}