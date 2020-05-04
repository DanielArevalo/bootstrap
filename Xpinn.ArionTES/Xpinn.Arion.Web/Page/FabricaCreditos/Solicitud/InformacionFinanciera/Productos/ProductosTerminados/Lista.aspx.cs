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
    private Xpinn.FabricaCreditos.Services.ProductosTerminadosService ProductosTerminadosServicio = new Xpinn.FabricaCreditos.Services.ProductosTerminadosService();
    private Xpinn.FabricaCreditos.Services.InformacionFinancieraService InformacionFinancieraServicio = new Xpinn.FabricaCreditos.Services.InformacionFinancieraService();
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ProductosTerminadosServicio.CodigoPrograma, "L");

            Site2 toolBar = (Site2)this.Master;
            //toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            //toolBar.eventoLimpiar += btnLimpiar_Click;
            //toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProductosTerminadosServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, ProductosTerminadosServicio.CodigoPrograma);
                if (Session[ProductosTerminadosServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProductosTerminadosServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    //protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    //{
    //    GuardarValoresConsulta(pConsulta, ProductosTerminadosServicio.CodigoPrograma);
    //    //Navegar(Pagina.Nuevo);
    //    Borrar();
    //}

    private void Borrar()
    {
        //CargarListas();
        txtCantidad.Text = "";
        txtProducto.Text = "";
        txtVrunitario.Text = "";
      
        idObjeto = "";
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, ProductosTerminadosServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ProductosTerminadosServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProductosTerminadosServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        //String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        String id = gvLista.SelectedRow.Cells[0].Text;
        Session[ProductosTerminadosServicio.CodigoPrograma + ".id"] = id;
        //Navegar(Pagina.Detalle);
        Edicion();
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;//gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[ProductosTerminadosServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
          
            long id = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[0].Text);
            Int64 Cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
            Int64 Cod_InfFin = Convert.ToInt64(Session["Cod_InfFin"].ToString());


            ProductosTerminadosServicio.EliminarProductosTerminados(id, (Usuario)Session["usuario"], Cod_persona, Cod_InfFin);

            long COD_INFFIN = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[4].Text);
            InformacionFinancieraServicio.EliminarInformacionFinanciera(COD_INFFIN, (Usuario)Session["usuario"]);

            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProductosTerminadosServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(ProductosTerminadosServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.ProductosTerminados> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.ProductosTerminados>();
            lstConsulta = ProductosTerminadosServicio.ListarProductosTerminados(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            gvLista.EmptyDataText = "";
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                Int64 sumaTotales = lstConsulta.Sum(item => item.vrtotal);
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

            Session.Add(ProductosTerminadosServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProductosTerminadosServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.ProductosTerminados ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.ProductosTerminados vProductosTerminados = new Xpinn.FabricaCreditos.Entities.ProductosTerminados();
        vProductosTerminados.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        vProductosTerminados.cod_inffin = Convert.ToInt64(Session["Cod_InfFin"].ToString());
        if(txtCantidad.Text.Trim() != "")
            vProductosTerminados.cantidad = Convert.ToInt64(txtCantidad.Text.Trim().Replace(".",""));
        if(txtProducto.Text.Trim() != "")
            vProductosTerminados.producto = Convert.ToString(txtProducto.Text.Trim());
        if(txtVrunitario.Text.Trim() != "")
            vProductosTerminados.vrunitario = Convert.ToInt64(txtVrunitario.Text.Trim().Replace(".", ""));
        //if(txtVrtotal.Text.Trim() != "")
        //    vProductosTerminados.vrtotal = Convert.ToInt64(txtVrtotal.Text.Trim());

        return vProductosTerminados;
    }

    private void Edicion()
    {
        try
        {
            if (Session[ProductosTerminadosServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ProductosTerminadosServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(ProductosTerminadosServicio.CodigoPrograma, "A");

            //Page_Load
            if (Session[ProductosTerminadosServicio.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[ProductosTerminadosServicio.CodigoPrograma + ".id"].ToString();
                if (idObjeto != null)
                {
                    ObtenerDatos(idObjeto);
                    Session.Remove(ProductosTerminadosServicio.CodigoPrograma + ".id");
                }

            }
            //else
            //{
            //    CargarListas();
            //}
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProductosTerminadosServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.ProductosTerminados vProductosTerminados = new Xpinn.FabricaCreditos.Entities.ProductosTerminados();
            vProductosTerminados = ProductosTerminadosServicio.ConsultarProductosTerminados(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vProductosTerminados.cod_prodter != Int64.MinValue)
                txtCod_prodter.Text = HttpUtility.HtmlDecode(vProductosTerminados.cod_prodter.ToString().Trim());
           
            if (vProductosTerminados.cantidad != Int64.MinValue)
                txtCantidad.Text = HttpUtility.HtmlDecode(vProductosTerminados.cantidad.ToString().Trim());
            if (!string.IsNullOrEmpty(vProductosTerminados.producto))
                txtProducto.Text = HttpUtility.HtmlDecode(vProductosTerminados.producto.ToString().Trim());
            if (vProductosTerminados.vrunitario != Int64.MinValue)
                txtVrunitario.Text = HttpUtility.HtmlDecode(vProductosTerminados.vrunitario.ToString().Trim());
            //    if (vProductosTerminados.vrtotal != Int64.MinValue)
            //        txtVrtotal.Text = HttpUtility.HtmlDecode(vProductosTerminados.vrtotal.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProductosTerminadosServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.ProductosTerminados vProductosTerminados = new Xpinn.FabricaCreditos.Entities.ProductosTerminados();

            if (idObjeto != "")
                vProductosTerminados = ProductosTerminadosServicio.ConsultarProductosTerminados(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (txtCod_prodter.Text != "") vProductosTerminados.cod_prodter = Convert.ToInt64(txtCod_prodter.Text.Trim());
            vProductosTerminados.cod_inffin = Convert.ToInt64(Session["Cod_Inffin"].ToString());
            if (txtCantidad.Text != "") vProductosTerminados.cantidad = Convert.ToInt64(txtCantidad.Text.Trim().Replace(@".", ""));
            vProductosTerminados.producto = (txtProducto.Text != "") ? Convert.ToString(txtProducto.Text.Trim()) : String.Empty;
            if (txtVrunitario.Text != "") vProductosTerminados.vrunitario = Convert.ToInt64(txtVrunitario.Text.Trim().Replace(@".", ""));
            //if (txtVrtotal.Text != "") vProductosTerminados.vrtotal = Convert.ToInt64(txtVrtotal.Text.Trim());
            vProductosTerminados.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());

            //INFORMACION FINANCIERA:
            Xpinn.FabricaCreditos.Entities.InformacionFinanciera vInformacionFinanciera = new Xpinn.FabricaCreditos.Entities.InformacionFinanciera();
            vInformacionFinanciera.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
            vInformacionFinanciera.fecha = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));

            if (idObjeto != "")
            {
                vProductosTerminados.cod_prodter = Convert.ToInt64(idObjeto);
                ProductosTerminadosServicio.ModificarProductosTerminados(vProductosTerminados, (Usuario)Session["usuario"]);
            }
            else
            {
                //InformacionFinancieraServicio.CrearInformacionFinanciera(vInformacionFinanciera, (Usuario)Session["usuario"]);
                //vProductosTerminados.cod_inffin = vInformacionFinanciera.cod_inffin;

                vProductosTerminados = ProductosTerminadosServicio.CrearProductosTerminados(vProductosTerminados, (Usuario)Session["usuario"]);
                idObjeto = vProductosTerminados.cod_prodter.ToString();
            }

            Session[ProductosTerminadosServicio.CodigoPrograma + ".id"] = idObjeto;
            //Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProductosTerminadosServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
        Actualizar();
        Borrar();
    }
}