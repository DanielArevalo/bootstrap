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
    private Xpinn.FabricaCreditos.Services.ProductosProcesoService ProductosProcesoServicio = new Xpinn.FabricaCreditos.Services.ProductosProcesoService();
   private Xpinn.FabricaCreditos.Services.InformacionFinancieraService InformacionFinancieraServicio = new Xpinn.FabricaCreditos.Services.InformacionFinancieraService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ProductosProcesoServicio.CodigoPrograma, "L");

            Site2 toolBar = (Site2)this.Master;
            //toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            //toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProductosProcesoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, ProductosProcesoServicio.CodigoPrograma);
                //CargarListas();
                if (Session[ProductosProcesoServicio.GetType().Name + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProductosProcesoServicio.GetType().Name, "Page_Load", ex);
        }
    }

    //protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    //{
    //    GuardarValoresConsulta(pConsulta, ProductosProcesoServicio.CodigoPrograma);
    //    //Navegar(Pagina.Nuevo);
    //    Borrar();
    //}

    private void  Borrar()
    {
        txtCantidad.Text = "";
        txtProducto.Text = "";
        txtPorcpd.Text = "";
        txtValunitario.Text = "";
        idObjeto = "";
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, ProductosProcesoServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ProductosProcesoServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProductosProcesoServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        //String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        String id = gvLista.SelectedRow.Cells[0].Text;
        Session[ProductosProcesoServicio.CodigoPrograma + ".id"] = id;
        Edicion();
        //Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;//gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[ProductosProcesoServicio.CodigoPrograma + ".id"] = id;
        //Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 Cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
            long id = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[0].Text);

            Int64 Cod_InfFin = Convert.ToInt64(Session["Cod_InfFin"].ToString());

            ProductosProcesoServicio.EliminarProductosProceso(id, (Usuario)Session["usuario"], Cod_persona, Cod_InfFin);

            //long COD_INFFIN = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[4].Text);
            //InformacionFinancieraServicio.EliminarInformacionFinanciera(COD_INFFIN, (Usuario)Session["usuario"]);

            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProductosProcesoServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(ProductosProcesoServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.ProductosProceso> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.ProductosProceso>();
            lstConsulta = ProductosProcesoServicio.ListarProductosProceso(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            gvLista.EmptyDataText = "";
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                Int64 sumaTotales = lstConsulta.Sum(item => item.valortotal);
                txtTotal.Text = sumaTotales.ToString();
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                txtTotal.Text = "0";
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(ProductosProcesoServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProductosProcesoServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.ProductosProceso ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.ProductosProceso vProductosProceso = new Xpinn.FabricaCreditos.Entities.ProductosProceso();

        vProductosProceso.cod_persona =  Convert.ToInt64(Session["Cod_persona"].ToString());
        vProductosProceso.cod_balance = Convert.ToInt64(Session["Cod_InfFin"].ToString());
        if(txtCantidad.Text.Trim() != "")
            vProductosProceso.cantidad = Convert.ToInt64(txtCantidad.Text.Trim().Replace(@".", ""));
        if(txtProducto.Text.Trim() != "")
            vProductosProceso.producto = Convert.ToString(txtProducto.Text.Trim());
        if(txtPorcpd.Text.Trim() != "")
            vProductosProceso.porcpd = Convert.ToInt64(txtPorcpd.Text.Trim().Replace(@".", ""));
        if(txtValunitario.Text.Trim() != "")
            vProductosProceso.valunitario = Convert.ToInt64(txtValunitario.Text.Trim().Replace(@".", ""));
        //if(txtValortotal.Text.Trim() != "")
        //    vProductosProceso.valortotal = Convert.ToInt64(txtValortotal.Text.Trim());

        return vProductosProceso;
    }










    private void Edicion()
    {

        try
        {
            if (Session[ProductosProcesoServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ProductosProcesoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(ProductosProcesoServicio.CodigoPrograma, "A");

            //Page_Load
            if (Session[ProductosProcesoServicio.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[ProductosProcesoServicio.CodigoPrograma + ".id"].ToString();
                if (idObjeto != null)
                {
                    ObtenerDatos(idObjeto);
                    Session.Remove(ProductosProcesoServicio.CodigoPrograma + ".id");
                }
            }
            //else
            //{
            //    CargarListas();
            //}
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProductosProcesoServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.ProductosProceso vProductosProceso = new Xpinn.FabricaCreditos.Entities.ProductosProceso();

            if (idObjeto != "")
                vProductosProceso = ProductosProcesoServicio.ConsultarProductosProceso(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (txtCod_prodproc.Text != "") vProductosProceso.cod_prodproc = Convert.ToInt64(txtCod_prodproc.Text.Trim());
            vProductosProceso.cod_balance = Convert.ToInt64(Session["Cod_InfFin"].ToString());
            if (txtCantidad.Text != "") vProductosProceso.cantidad = Convert.ToInt64(txtCantidad.Text.Trim().Replace(@".", ""));
            vProductosProceso.producto = (txtProducto.Text != "") ? Convert.ToString(txtProducto.Text.Trim()) : String.Empty;
            if (txtPorcpd.Text != "") vProductosProceso.porcpd = Convert.ToInt64(txtPorcpd.Text.Trim());
            if (txtValunitario.Text != "") vProductosProceso.valunitario = Convert.ToInt64(txtValunitario.Text.Trim().Replace(@".", ""));
            //if (txtValortotal.Text != "") vProductosProceso.valortotal = Convert.ToInt64(txtValortotal.Text.Trim());
            vProductosProceso.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());

            ////INFORMACION FINANCIERA:
            //Xpinn.FabricaCreditos.Entities.InformacionFinanciera vInformacionFinanciera = new Xpinn.FabricaCreditos.Entities.InformacionFinanciera();
            //vInformacionFinanciera.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
            //vInformacionFinanciera.fecha = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));

            if (idObjeto != "")
            {
                vProductosProceso.cod_prodproc = Convert.ToInt64(idObjeto);
                ProductosProcesoServicio.ModificarProductosProceso(vProductosProceso, (Usuario)Session["usuario"]);
            }
            else
            {
                //InformacionFinancieraServicio.CrearInformacionFinanciera(vInformacionFinanciera, (Usuario)Session["usuario"]);
                //vProductosProceso.cod_balance = vInformacionFinanciera.cod_inffin;

                vProductosProceso = ProductosProcesoServicio.CrearProductosProceso(vProductosProceso, (Usuario)Session["usuario"]);
                idObjeto = vProductosProceso.cod_prodproc.ToString();
            }

            Session[ProductosProcesoServicio.CodigoPrograma + ".id"] = idObjeto;
            //Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProductosProcesoServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
        Actualizar();
        Borrar();
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.ProductosProceso vProductosProceso = new Xpinn.FabricaCreditos.Entities.ProductosProceso();
            vProductosProceso = ProductosProcesoServicio.ConsultarProductosProceso(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vProductosProceso.cod_prodproc != Int64.MinValue)
                txtCod_prodproc.Text = HttpUtility.HtmlDecode(vProductosProceso.cod_prodproc.ToString().Trim());
            
            if (vProductosProceso.cantidad != Int64.MinValue)
                txtCantidad.Text = HttpUtility.HtmlDecode(vProductosProceso.cantidad.ToString().Trim());
            if (!string.IsNullOrEmpty(vProductosProceso.producto))
                txtProducto.Text = HttpUtility.HtmlDecode(vProductosProceso.producto.ToString().Trim());
            if (vProductosProceso.porcpd != Int64.MinValue)
                txtPorcpd.Text = HttpUtility.HtmlDecode(vProductosProceso.porcpd.ToString().Trim());
            if (vProductosProceso.valunitario != Int64.MinValue)
                txtValunitario.Text = HttpUtility.HtmlDecode(vProductosProceso.valunitario.ToString().Trim());
            //if (vProductosProceso.valortotal != Int64.MinValue)
            //    txtValortotal.Text = HttpUtility.HtmlDecode(vProductosProceso.valortotal.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProductosProcesoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
    
    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/InventarioActivoFijo/Lista.aspx");
       
    }
    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/BalanceGeneralMicroempresa/Default.aspx");
       
    }
}