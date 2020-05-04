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
    private Xpinn.FabricaCreditos.Services.MargenVentasService MargenVentasServicio = new Xpinn.FabricaCreditos.Services.MargenVentasService();
    private Xpinn.FabricaCreditos.Services.VentasSemanalesService VentasSemanalesServicio = new Xpinn.FabricaCreditos.Services.VentasSemanalesService();
    Xpinn.FabricaCreditos.Entities.MargenVentas vMargenVentas = new Xpinn.FabricaCreditos.Entities.MargenVentas();
    List<Xpinn.FabricaCreditos.Entities.MargenVentas> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.MargenVentas>();
    private Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(MargenVentasServicio.CodigoPrograma, "L");

            Site1 toolBar = (Site1)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoAdelante += btnAdelante_Click;
            toolBar.eventoAtras += btnAtras_Click;

            ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();

            ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
            btnAdelante.ValidationGroup = "";
            btnAdelante.ImageUrl = "~/Images/btnInformacionFinanciera.jpg";
        
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MargenVentasServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, MargenVentasServicio.CodigoPrograma);
                //CargarListas();

                if (Session[MargenVentasServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MargenVentasServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    //protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    //{
    //    GuardarValoresConsulta(pConsulta, MargenVentasServicio.CodigoPrograma);
    //    Borrar();
      
    //}

    private void Borrar()
    {
        
        //txtTipoproduco.Text = "";
        txtNombreproducto.Text = "";
        txtUnivendida.Text = "";
        txtCostounidven.Text = "";
        txtPreciounidven.Text = "";
        idObjeto = "";
    }    

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, MargenVentasServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, MargenVentasServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnEliminar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MargenVentasServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[11].Text = e.Row.Cells[10].Text + "%";
            e.Row.Cells[12].Text = e.Row.Cells[12].Text + "%";     
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        //String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        String id = gvLista.SelectedRow.Cells[0].Text;
        Session[MargenVentasServicio.CodigoPrograma + ".id"] = id;
        //Navegar(Pagina.Detalle);
        Edicion();
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;//gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        //Session[MargenVentasServicio.CodigoPrograma + ".id"] = id;
        //Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long id = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[0].Text);//Convert.ToInt64(gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value);
            Int64 persona = Convert.ToInt64(Session["Cod_persona"].ToString());
            MargenVentasServicio.EliminarMargenVentas(id, persona, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MargenVentasServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(MargenVentasServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.MargenVentas entTotales = new Xpinn.FabricaCreditos.Entities.MargenVentas(); // Entidad donde se devuelven los calculos
            lstConsulta = MargenVentasServicio.ListarMargenVentas(ObtenerValores(), (Usuario)Session["usuario"]);

            //Dias laborados:
            List<Xpinn.FabricaCreditos.Entities.VentasSemanales> lstDiasLab = new List<Xpinn.FabricaCreditos.Entities.VentasSemanales>();
            Xpinn.FabricaCreditos.Entities.VentasSemanales entDiasLab = new Xpinn.FabricaCreditos.Entities.VentasSemanales(); // Entidad donde se devuelven los calculos
            lstDiasLab = VentasSemanalesServicio.ListarVentasSemanales(entDiasLab, (Usuario)Session["usuario"]);
            
            gvLista.PageSize = 15;
            gvLista.EmptyDataText = "No se encontraron registros";

            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                //Pasa lista a bussines para hacer calculos
                entTotales = MargenVentasServicio.CalculosMargenVentas(lstConsulta);

                lblCostoVenta.Text = Convert.ToString(entTotales.totalCostoVenta);
                lblVentaTotal.Text = Convert.ToString(entTotales.totalVentaTotal);
                lblPorcentajeCostoVenta.Text = Convert.ToString(entTotales.porcentajeCostoVenta) + "%";
                lblMargenVenta.Text = Convert.ToString(entTotales.porcentajeMargen) + "%";

                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
                          
                //Dias laborados:
                Int64[] sumaDias = new Int64[] { lstDiasLab.Sum(item => item.lunes), lstDiasLab.Sum(item => item.martes), lstDiasLab.Sum(item => item.miercoles), lstDiasLab.Sum(item => item.jueves), lstDiasLab.Sum(item => item.viernes), lstDiasLab.Sum(item => item.sabados), lstDiasLab.Sum(item => item.domingo) };
                int i =0;
                int diasLab = 0;
                while (i <= 6)
                {
                    if (sumaDias[i] == 0)
                        diasLab++; 
                    i++;
                }
                diasLab = 7 - diasLab;
                lblDiasLaborados.Text = Convert.ToString(diasLab.ToString());           

            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(MargenVentasServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MargenVentasServicio.CodigoPrograma, "Actualizar", ex);
        }
    }


   

    private Xpinn.FabricaCreditos.Entities.MargenVentas ObtenerValores()
    {
        
    if(txtCod_ventas.Text.Trim() != "")
        vMargenVentas.cod_ventas = Convert.ToInt64(txtCod_ventas.Text.Trim());
    if(txtTipoproduco.Text.Trim() != "")
        vMargenVentas.tipoproduco = Convert.ToString(txtTipoproduco.Text.Trim());
    if(txtNombreproducto.Text.Trim() != "")
        vMargenVentas.nombreproducto = Convert.ToString(txtNombreproducto.Text.Trim());
    if(txtUnivendida.Text.Trim() != "")
        vMargenVentas.univendida = Convert.ToInt64(txtUnivendida.Text.Trim());
    if(txtCostounidven.Text.Trim() != "")
        vMargenVentas.costounidven = Convert.ToInt64(txtCostounidven.Text.Trim());
    if(txtPreciounidven.Text.Trim() != "")
        vMargenVentas.preciounidven = Convert.ToInt64(txtPreciounidven.Text.Trim());

    vMargenVentas.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString()); ;
    //if(txtCostoventa.Text.Trim() != "")
    //    vMargenVentas.costoventa = Convert.ToInt64(txtCostoventa.Text.Trim());
    //if(txtVentatotal.Text.Trim() != "")
    //    vMargenVentas.ventatotal = Convert.ToInt64(txtVentatotal.Text.Trim());
    //if(txtMargen.Text.Trim() != "")
    //    vMargenVentas.margen = Convert.ToInt64(txtMargen.Text.Trim());

        return vMargenVentas;
    }
    protected void gvLista_DataBinding(object sender, EventArgs e)
    {
       
    }
    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument.ToString() == "TipoProd")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Session["IdMargen"] = Convert.ToString(gvLista.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);//Convert.ToInt64(gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value);
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/CosteoProductos/Lista.aspx");
        }
    }

    private void VariablesCosteo()
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[MargenVentasServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }
   
    protected void hlbTipoProducto_Click(object sender, EventArgs e)
    {
        //Get the button that raised the event
        Button btn = (Button)sender;

        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        //Get rowindex
        int rowindex = gvr.RowIndex;
        
        //long id = Convert.ToInt64(gvLista.Rows[e.].Cells[0].Text);//Convert.ToInt64(gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value);
        //MargenVentasServicio.EliminarMargenVentas(id, (Usuario)Session["usuario"]);
        //Actualizar();
    }

    private void Edicion()
    {
        try
        {
            if (Session[MargenVentasServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(MargenVentasServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(MargenVentasServicio.CodigoPrograma, "A");

            //Page_Load
            if (Session[MargenVentasServicio.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[MargenVentasServicio.CodigoPrograma + ".id"].ToString();
                if (idObjeto != null)
                {
                    ObtenerDatos(idObjeto);
                    Session.Remove(MargenVentasServicio.CodigoPrograma + ".id");
                }

            }
            //else
            //{
            //    CargarListas();
            //}
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MargenVentasServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
        
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.MargenVentas vMargenVentas = new Xpinn.FabricaCreditos.Entities.MargenVentas();
            vMargenVentas = MargenVentasServicio.ConsultarMargenVentas(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vMargenVentas.cod_ventas != Int64.MinValue)
                txtCod_ventas.Text = HttpUtility.HtmlDecode(vMargenVentas.cod_ventas.ToString().Trim());
            if (!string.IsNullOrEmpty(vMargenVentas.tipoproduco))
                txtTipoproduco.Text = HttpUtility.HtmlDecode(vMargenVentas.tipoproduco.ToString().Trim());
            if (!string.IsNullOrEmpty(vMargenVentas.nombreproducto))
                txtNombreproducto.Text = HttpUtility.HtmlDecode(vMargenVentas.nombreproducto.ToString().Trim());
            if (vMargenVentas.univendida != Int64.MinValue)
                txtUnivendida.Text = HttpUtility.HtmlDecode(vMargenVentas.univendida.ToString().Trim());
            if (vMargenVentas.costounidven != Int64.MinValue)
                txtCostounidven.Text = HttpUtility.HtmlDecode(vMargenVentas.costounidven.ToString().Trim());
            if (vMargenVentas.preciounidven != Int64.MinValue)
                txtPreciounidven.Text = HttpUtility.HtmlDecode(vMargenVentas.preciounidven.ToString().Trim());
            //if (vMargenVentas.costoventa != Int64.MinValue)
            //    txtCostoventa.Text = HttpUtility.HtmlDecode(vMargenVentas.costoventa.ToString().Trim());
            //if (vMargenVentas.ventatotal != Int64.MinValue)
            //    txtVentatotal.Text = HttpUtility.HtmlDecode(vMargenVentas.ventatotal.ToString().Trim());
            //if (vMargenVentas.margen != Int64.MinValue)
            //    txtMargen.Text = HttpUtility.HtmlDecode(vMargenVentas.margen.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MargenVentasServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.MargenVentas vMargenVentas = new Xpinn.FabricaCreditos.Entities.MargenVentas();

            if (idObjeto != "")
                vMargenVentas = MargenVentasServicio.ConsultarMargenVentas(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            //vMargenVentas.cod_ventas = Convert.ToInt64(txtCod_ventas.Text.Trim());
            vMargenVentas.tipoproduco = Convert.ToString(txtTipoproduco.Text.Trim());
            vMargenVentas.nombreproducto = Convert.ToString(txtNombreproducto.Text.Trim());
            vMargenVentas.univendida = Convert.ToInt64(txtUnivendida.Text.Trim().Replace(".", ""));
            vMargenVentas.costounidven = Convert.ToInt64(txtCostounidven.Text.Trim().Replace(".", ""));
            vMargenVentas.preciounidven = Convert.ToInt64(txtPreciounidven.Text.Trim().Replace(".", ""));
            vMargenVentas.costoventa = 0;
            vMargenVentas.ventatotal = 0;
            vMargenVentas.margen = 0;
            vMargenVentas.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());

            if (idObjeto != "")
            {
                vMargenVentas.cod_margen = Convert.ToInt64(idObjeto);
                MargenVentasServicio.ModificarMargenVentas(vMargenVentas, (Usuario)Session["usuario"]);
            }
            else
            {
                vMargenVentas = MargenVentasServicio.CrearMargenVentas(vMargenVentas, (Usuario)Session["usuario"]);
                idObjeto = vMargenVentas.cod_margen.ToString();
            }

            Session[MargenVentasServicio.CodigoPrograma + ".id"] = idObjeto;
            Borrar();
            //Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MargenVentasServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }

        Actualizar();
    }










    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/Default.aspx");
    }

    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/InformacionFinancieraNegocio/Default.aspx");
    }
}