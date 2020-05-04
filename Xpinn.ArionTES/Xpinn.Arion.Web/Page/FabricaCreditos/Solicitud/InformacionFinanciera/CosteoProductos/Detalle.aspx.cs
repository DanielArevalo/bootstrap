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

partial class Detalle : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.CosteoProductosService CosteoProductosServicio = new Xpinn.FabricaCreditos.Services.CosteoProductosService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CosteoProductosServicio.CodigoPrograma, "D");

            Site1 toolBar = (Site1)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;

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
                AsignarEventoConfirmar();
                if (Session[CosteoProductosServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[CosteoProductosServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(CosteoProductosServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CosteoProductosServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            CosteoProductosServicio.EliminarCosteoProductos(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CosteoProductosServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[CosteoProductosServicio.CodigoPrograma + ".id"] = idObjeto;
        Navegar(Pagina.Nuevo);
    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.CosteoProductos vCosteoProductos = new Xpinn.FabricaCreditos.Entities.CosteoProductos();
            vCosteoProductos = CosteoProductosServicio.ConsultarCosteoProductos(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vCosteoProductos.cod_margen != Int64.MinValue)
                txtCod_margen.Text = vCosteoProductos.cod_margen.ToString().Trim();
            if (!string.IsNullOrEmpty(vCosteoProductos.materiaprima))
                txtMateriaprima.Text = vCosteoProductos.materiaprima.ToString().Trim();
            if (!string.IsNullOrEmpty(vCosteoProductos.unidadcompra))
                txtUnidadcompra.Text = vCosteoProductos.unidadcompra.ToString().Trim();
            if (vCosteoProductos.costounidad != Int64.MinValue)
                txtCostounidad.Text = vCosteoProductos.costounidad.ToString().Trim();
            if (vCosteoProductos.cantidad != Int64.MinValue)
                txtCantidad.Text = vCosteoProductos.cantidad.ToString().Trim();
            if (vCosteoProductos.costo != Int64.MinValue)
                txtCosto.Text = vCosteoProductos.costo.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CosteoProductosServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }
}