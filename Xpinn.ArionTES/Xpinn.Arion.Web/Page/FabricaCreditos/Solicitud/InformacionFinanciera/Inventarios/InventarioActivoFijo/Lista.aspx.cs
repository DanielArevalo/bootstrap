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
    private Xpinn.FabricaCreditos.Services.InventarioActivoFijoService InventarioActivoFijoServicio = new Xpinn.FabricaCreditos.Services.InventarioActivoFijoService();
    private Xpinn.FabricaCreditos.Services.InformacionFinancieraService InformacionFinancieraServicio = new Xpinn.FabricaCreditos.Services.InformacionFinancieraService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(InventarioActivoFijoServicio.CodigoPrograma, "L");

            Site2 toolBar = (Site2)this.Master;
            //toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            //toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            //toolBar.eventoAdelante += btnAdelante_Click;
            //toolBar.eventoAtras += btnAtras_Click;

            //((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            //((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();
            
            //ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
            //btnAdelante.ValidationGroup = "";
            //btnAdelante.ImageUrl = "~/Images/btnInformacionFinanciera.jpg";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventarioActivoFijoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, InventarioActivoFijoServicio.CodigoPrograma);
                //CargarListas();

                if (Session[InventarioActivoFijoServicio.GetType().Name + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventarioActivoFijoServicio.GetType().Name, "Page_Load", ex);
        }
    }

    //protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    //{
    //    GuardarValoresConsulta(pConsulta, InventarioActivoFijoServicio.CodigoPrograma);
    //    Borrar();
    //    //Navegar(Pagina.Nuevo);
    //}

    private void Borrar()
    {
        //CargarListas();
        txtDescripcion.Text = "";
        txtMarca.Text = "";
        txtValor.Text = "";       
        idObjeto = "";
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, InventarioActivoFijoServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, InventarioActivoFijoServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventarioActivoFijoServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        //String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        String id = gvLista.SelectedRow.Cells[0].Text;
        Session[InventarioActivoFijoServicio.CodigoPrograma + ".id"] = id;
        //Navegar(Pagina.Detalle);
        Edicion();
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;//gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[InventarioActivoFijoServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 Cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
            Int64 Cod_InfFin = Convert.ToInt64(Session["Cod_InfFin"].ToString());
            long id = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[0].Text);//Convert.ToInt64(gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value);
            InventarioActivoFijoServicio.EliminarInventarioActivoFijo(id, (Usuario)Session["usuario"], Cod_persona, Cod_InfFin);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventarioActivoFijoServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(InventarioActivoFijoServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.InventarioActivoFijo> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.InventarioActivoFijo>();
            lstConsulta = InventarioActivoFijoServicio.ListarInventarioActivoFijo(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            gvLista.EmptyDataText = "No se encontraron registros";
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                Int64 sumaTotal = Convert.ToInt64(lstConsulta.Sum(item => item.valor).ToString());
                txtTotalFijo.Text = sumaTotal.ToString();

                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                txtTotalFijo.Text = "0";
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(InventarioActivoFijoServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventarioActivoFijoServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.InventarioActivoFijo ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.InventarioActivoFijo vInventarioActivoFijo = new Xpinn.FabricaCreditos.Entities.InventarioActivoFijo();

        vInventarioActivoFijo.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        vInventarioActivoFijo.cod_inffin = Convert.ToInt64(Session["Cod_InfFin"].ToString());

        if(txtCod_inffin.Text.Trim() != "")
            vInventarioActivoFijo.cod_inffin = Convert.ToInt64(txtCod_inffin.Text.Trim().Replace(@".",""));
        if(txtDescripcion.Text.Trim() != "")
            vInventarioActivoFijo.descripcion = Convert.ToString(txtDescripcion.Text.Trim());
        if(txtMarca.Text.Trim() != "")
            vInventarioActivoFijo.marca = Convert.ToString(txtMarca.Text.Trim());
        if(txtValor.Text.Trim() != "")
        vInventarioActivoFijo.valor = Convert.ToInt64(txtValor.Text.Trim().Replace(@".",""));

        return vInventarioActivoFijo;
    }
    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/InformacionFinancieraNegocio/Lista.aspx");
    }
    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/Productos/Default.aspx");
    }


    private void Edicion()
    {
        try
        {
            if (Session[InventarioActivoFijoServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(InventarioActivoFijoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(InventarioActivoFijoServicio.CodigoPrograma, "A");

            //Page_Load
            if (Session[InventarioActivoFijoServicio.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[InventarioActivoFijoServicio.CodigoPrograma + ".id"].ToString();
                if (idObjeto != null)
                {
                    ObtenerDatos(idObjeto);
                    Session.Remove(InventarioActivoFijoServicio.CodigoPrograma + ".id");
                }

            }
            //else
            //{
            //    CargarListas();
            //}
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventarioActivoFijoServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.InventarioActivoFijo vInventarioActivoFijo = new Xpinn.FabricaCreditos.Entities.InventarioActivoFijo();

            if (idObjeto != "")
                vInventarioActivoFijo = InventarioActivoFijoServicio.ConsultarInventarioActivoFijo(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (txtCod_activo.Text != "") vInventarioActivoFijo.cod_activo = Convert.ToInt64(txtCod_activo.Text.Trim());
            if (txtCod_inffin.Text != "") vInventarioActivoFijo.cod_inffin = Convert.ToInt64(txtCod_inffin.Text.Trim());
            vInventarioActivoFijo.descripcion = (txtDescripcion.Text != "") ? Convert.ToString(txtDescripcion.Text.Trim()) : String.Empty;
            vInventarioActivoFijo.marca = (txtMarca.Text != "") ? Convert.ToString(txtMarca.Text.Trim()) : String.Empty;
            if (txtValor.Text != "") vInventarioActivoFijo.valor = Convert.ToInt64(txtValor.Text.Trim().Replace(@".", ""));
            vInventarioActivoFijo.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
            vInventarioActivoFijo.cod_inffin = Convert.ToInt64(Session["Cod_InfFin"].ToString());
            
            ////INFORMACION FINANCIERA:
            //Xpinn.FabricaCreditos.Entities.InformacionFinanciera vInformacionFinanciera = new Xpinn.FabricaCreditos.Entities.InformacionFinanciera();
            //vInformacionFinanciera.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
            //vInformacionFinanciera.fecha = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));

            if (idObjeto != "")
            {
                vInventarioActivoFijo.cod_activo = Convert.ToInt64(idObjeto);
                InventarioActivoFijoServicio.ModificarInventarioActivoFijo(vInventarioActivoFijo, (Usuario)Session["usuario"]);
            }
            else
            {
                vInventarioActivoFijo = InventarioActivoFijoServicio.CrearInventarioActivoFijo(vInventarioActivoFijo, (Usuario)Session["usuario"]);
                idObjeto = vInventarioActivoFijo.cod_activo.ToString();
            }

            Session[InventarioActivoFijoServicio.CodigoPrograma + ".id"] = idObjeto;
            //Navegar(Pagina.Detalle);
            Borrar();
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventarioActivoFijoServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
        Actualizar();
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.InventarioActivoFijo vInventarioActivoFijo = new Xpinn.FabricaCreditos.Entities.InventarioActivoFijo();
            vInventarioActivoFijo = InventarioActivoFijoServicio.ConsultarInventarioActivoFijo(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vInventarioActivoFijo.cod_activo != Int64.MinValue)
                txtCod_activo.Text = HttpUtility.HtmlDecode(vInventarioActivoFijo.cod_activo.ToString().Trim());
            if (vInventarioActivoFijo.cod_inffin != Int64.MinValue)
                txtCod_inffin.Text = HttpUtility.HtmlDecode(vInventarioActivoFijo.cod_inffin.ToString().Trim());
            if (!string.IsNullOrEmpty(vInventarioActivoFijo.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vInventarioActivoFijo.descripcion.ToString().Trim());
            if (!string.IsNullOrEmpty(vInventarioActivoFijo.marca))
                txtMarca.Text = HttpUtility.HtmlDecode(vInventarioActivoFijo.marca.ToString().Trim());
            if (vInventarioActivoFijo.valor != Int64.MinValue)
                txtValor.Text = HttpUtility.HtmlDecode(vInventarioActivoFijo.valor.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InventarioActivoFijoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


}