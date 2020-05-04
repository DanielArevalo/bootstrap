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
    private Xpinn.Ahorros.Services.ParametroGMFService AhorroVistaServicio = new Xpinn.Ahorros.Services.ParametroGMFService();
    PoblarListas lista = new PoblarListas();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AhorroVistaServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;

            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            
            toolBar.MostrarExportar(false);
     
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargar();
                CargarListar();
               
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void cargar()
    {

       

    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Session[AhorroVistaServicio.CodigoPrograma + ".id"] = null;
        GuardarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {

        ///mensaje al guardar
        ctlMensaje.MostrarMensaje("Desea guardar los datos de la parametrización?");
    }

   
    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();

    }

    private void CargarListar()
    {
        ///carga y lista las variables a la entidad linea ahorro services 
        Xpinn.Ahorros.Services.ParametroGMFService linahorroServicio = new Xpinn.Ahorros.Services.ParametroGMFService();
        Xpinn.Ahorros.Entities.ParametroGMF linahorroVista = new Xpinn.Ahorros.Entities.ParametroGMF();
        ddloperacion.DataTextField = "descripcion";
        ddloperacion.DataValueField = "TIPO_OPE";
        ddloperacion.DataSource = linahorroServicio.combooperacion((Usuario)Session["usuario"]);
        ddloperacion.DataBind();

        Xpinn.Aportes.Services.TipoProductoServices linahorroServicios = new Xpinn.Aportes.Services.TipoProductoServices();
        Xpinn.Aportes.Entities.TipoProducto linahorroVistas = new Xpinn.Aportes.Entities.TipoProducto();
        ddlproducto.DataTextField = "nombre";
        ddlproducto.DataValueField = "COD_TIPO_PROD";
        ddlproducto.DataSource = linahorroServicios.ListarTipoProducto(linahorroVistas, (Usuario)Session["usuario"]);
        
        ddlproducto.DataBind();
        ddlproducto.Items.Insert(0, new ListItem("seleccione un item"));

        
        PoblarLista("TIPO_TRAN", ddltransaccion);
      
    }

protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
{
    VerError("");
    ///pone en 0 los datos nulos cuando consulta y verifica las variables

        GuardarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        gvLista.Visible = false;
        lblTotalRegs.Visible = false;
        LimpiarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnEliminar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[AhorroVistaServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[AhorroVistaServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string id = Convert.ToString(e.Keys[0]);
            AhorroVistaServicio.EliminarParametroGMF(Convert.ToInt64(id), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
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
            BOexcepcion.Throw(AhorroVistaServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        VerError("");
        try
        {
            ///carga todo a la entidad provision_ahorro por la variable ahorro

            Xpinn.Ahorros.Entities.ParametroGMF ahorro = new Xpinn.Ahorros.Entities.ParametroGMF();

            string filtro = getFiltro();


            List<Xpinn.Ahorros.Entities.ParametroGMF> lstConsulta = new List<Xpinn.Ahorros.Entities.ParametroGMF>();
            DateTime pFechaIni;
            Xpinn.Ahorros.Entities.ParametroGMF pAhorroVista = new Xpinn.Ahorros.Entities.ParametroGMF();
            if(ddloperacion.SelectedIndex!=0)
            ahorro.tipo_ope = Convert.ToInt32(ddloperacion.SelectedValue);
            if (ddlproducto.SelectedIndex != 0)
            ahorro.tipo_producto = Convert.ToInt32(ddlproducto.SelectedValue);
            if (ddltransaccion.SelectedIndex != 0)
            ahorro.tipo_tran = Convert.ToInt32(ddltransaccion.SelectedValue);

            ///ingresa a la capa de bussines por listar provision
            lstConsulta = AhorroVistaServicio.ListarParametroGMF(filtro, ahorro, (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {

                Site toolBar = (Site)this.Master;
                toolBar.MostrarExportar(true);
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                Session["DTAhorroVista"] = lstConsulta;
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {

                Site toolBar = (Site)this.Master;
                toolBar.MostrarExportar(false);
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(AhorroVistaServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.Ahorros.Entities.provision_ahorro ObtenerValor()
    {
        return null;
    }

    public String getFiltro()
    {
        String filtro = " where 1=1 ";
        if (ddloperacion.SelectedValue != "0" && ddloperacion.SelectedValue != "")
            filtro += "and t.tipo_ope= " + ddloperacion.SelectedValue + "";
        if (ddlproducto.SelectedIndex != 0)
            filtro += "and p.tipo_producto =" + ddlproducto.SelectedValue + "";
        if (ddltransaccion.SelectedValue != "0" && ddltransaccion.SelectedValue != "")
            filtro += "and tt.tipo_tran = " + ddltransaccion.SelectedValue + "";

        return filtro;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvLista);
    }

    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=ParametrosGmf.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.Default;
        StringWriter sw = new StringWriter();
        ExpGrilla expGrilla = new ExpGrilla();

        sw = expGrilla.ObtenerGrilla(GridView1, null);

        Response.Write(expGrilla.style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

}