using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.CosteoProductosService CosteoProductosServicio = new Xpinn.FabricaCreditos.Services.CosteoProductosService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[CosteoProductosServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(CosteoProductosServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(CosteoProductosServicio.CodigoPrograma, "A");

            Site1 toolBar = (Site1)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;

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

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.CosteoProductos vCosteoProductos = new Xpinn.FabricaCreditos.Entities.CosteoProductos();

            if (idObjeto != "")
                vCosteoProductos = CosteoProductosServicio.ConsultarCosteoProductos(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (txtCod_margen.Text != "") vCosteoProductos.cod_margen = Convert.ToInt64(txtCod_margen.Text.Trim());
            if (txtMateriaprima.Text != "") vCosteoProductos.materiaprima = Convert.ToString(txtMateriaprima.Text.Trim());
            if (txtUnidadcompra.Text != "") vCosteoProductos.unidadcompra = Convert.ToString(txtUnidadcompra.Text.Trim());
            if (txtCostounidad.Text != "") vCosteoProductos.costounidad = Convert.ToInt64(txtCostounidad.Text.Trim());
            if (txtCantidad.Text != "") vCosteoProductos.cantidad = Convert.ToInt64(txtCantidad.Text.Trim());
            if (txtCosto.Text != "") vCosteoProductos.costo = Convert.ToInt64(txtCosto.Text.Trim());

            if (idObjeto != "")
            {
                vCosteoProductos.cod_costeo = Convert.ToInt64(idObjeto);
                CosteoProductosServicio.ModificarCosteoProductos(vCosteoProductos, (Usuario)Session["usuario"]);
            }
            else
            {
                vCosteoProductos = CosteoProductosServicio.CrearCosteoProductos(vCosteoProductos, (Usuario)Session["usuario"]);
                idObjeto = vCosteoProductos.cod_costeo.ToString();
            }

            Session[CosteoProductosServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CosteoProductosServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[CosteoProductosServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.CosteoProductos vCosteoProductos = new Xpinn.FabricaCreditos.Entities.CosteoProductos();
            vCosteoProductos = CosteoProductosServicio.ConsultarCosteoProductos(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vCosteoProductos.cod_margen != Int64.MinValue)
                txtCod_margen.Text = HttpUtility.HtmlDecode(vCosteoProductos.cod_margen.ToString().Trim());
            if (!string.IsNullOrEmpty(vCosteoProductos.materiaprima))
                txtMateriaprima.Text = HttpUtility.HtmlDecode(vCosteoProductos.materiaprima.ToString().Trim());
            if (!string.IsNullOrEmpty(vCosteoProductos.unidadcompra))
                txtUnidadcompra.Text = HttpUtility.HtmlDecode(vCosteoProductos.unidadcompra.ToString().Trim());
            if (vCosteoProductos.costounidad != Int64.MinValue)
                txtCostounidad.Text = HttpUtility.HtmlDecode(vCosteoProductos.costounidad.ToString().Trim());
            if (vCosteoProductos.cantidad != Int64.MinValue)
                txtCantidad.Text = HttpUtility.HtmlDecode(vCosteoProductos.cantidad.ToString().Trim());
            if (vCosteoProductos.costo != Int64.MinValue)
                txtCosto.Text = HttpUtility.HtmlDecode(vCosteoProductos.costo.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CosteoProductosServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
}