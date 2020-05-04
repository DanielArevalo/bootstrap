using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Text;
using System.IO;
using System.Globalization;
using Xpinn.Contabilidad.Entities;
using Xpinn.Comun.Entities;

partial class Lista : GlobalWeb
{
    private Xpinn.Ahorros.Services.AhorroVistaServices AhorroVistaServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
    private Xpinn.Contabilidad.Services.Par_Cue_OtrosService objeParametros = new Xpinn.Contabilidad.Services.Par_Cue_OtrosService();
    
    PoblarListas poblar = new PoblarListas();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(objeParametros.codigoProgramaParametro, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objeParametros.codigoProgramaParametro, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarddl();
                CargarValoresConsulta(pConsulta, objeParametros.codigoProgramaParametro);
                if (Session[objeParametros.codigoProgramaParametro + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objeParametros.codigoProgramaParametro, "Page_Load", ex);
        }
    }

    private void cargarddl()
    {
        //poblar.PoblarListaDesplegable("lineacdat", ddlLineaAhorro, (Usuario)Session["usuario"]);
        ddlTipoAhorro.Items.Insert(0, new ListItem("Seleccione Un Item ", "0"));
        ddlTipoAhorro.Items.Insert(1, new ListItem("Ahorro A la Vista ", "3"));
        ddlTipoAhorro.Items.Insert(2, new ListItem("Ahorro Programado ", "9"));
        ddlTipoAhorro.Items.Insert(3, new ListItem("CDAT", "5"));
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session[objeParametros.codigoProgramaParametro + ".id"] = null;
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {        
        Actualizar();
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
          //  ConfirmarEliminarFila(e, "btnEliminar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objeParametros.codigoProgramaParametro + "L", "gvLista_RowDataBound", ex);
        }
    }

    public String getFiltro() 
    {
        String filtro = "";
        if (txtCodigo.Text.Trim() != "")
            filtro += " and pl.idparametro = " + txtCodigo.Text;

        if (ddlTipoAhorro.SelectedIndex > 0)
            filtro += " and pl.tipo_ahorro = " + ddlTipoAhorro.SelectedValue;

        if (!string.IsNullOrWhiteSpace(ctlListarCodigoAhorro.Codigo))
            filtro += " and pl.cod_linea = " + ctlListarCodigoAhorro.Codigo;

        if (!string.IsNullOrWhiteSpace(ctlListarCodigoTransaccion.Codigo))
            filtro += " and t.TIPO_TRAN = " + ctlListarCodigoTransaccion.Codigo;

        return filtro;
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[objeParametros.codigoProgramaParametro + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 ide = Convert.ToInt64(gvLista.DataKeys[e.RowIndex].Value.ToString());
            Session["idParametro"] = ide;
            ctlMensaje.MostrarMensaje("El registro se eliminará desa continuar?");
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
            BOexcepcion.Throw(objeParametros.codigoProgramaParametro, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        VerError("");
        try
        {

            List<Xpinn.Contabilidad.Entities.Par_Cue_LinAho> lista = new List<Xpinn.Contabilidad.Entities.Par_Cue_LinAho>();

            lista = objeParametros.getListParametrosServices((Usuario)Session["usuario"], getFiltro());
            
            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lista;
            if (lista.Count > 0)
            {
                gvLista.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lista.Count.ToString();
                gvLista.DataBind();
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }

            Session.Add(objeParametros.codigoProgramaParametro + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objeParametros.codigoProgramaParametro, "Actualizar", ex);
        }
    }

    // para cargar el dropdaw de tipo
    protected void ddlTipoAhorro_SelectedIndexChanged(object sender, EventArgs e)
    {
        String tabla = "";
        ctlListarCodigoAhorro.LimpiarControl();
        ctlListarCodigoTransaccion.LimpiarControl();

        if(ddlTipoAhorro.SelectedIndex!=0)
        {
            String codigo = ddlTipoAhorro.SelectedValue;

            switch (codigo)
            {
                case "3":
                    tabla = "lineaahorro";
                    break;
                case "9": tabla = "lineaprogramado";
                    break;
                case "5": tabla = "lineacdat";
                    break;
            }

            List<ListaDesplegable> plista = new List<ListaDesplegable>();
            Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

            plista = pservicio.ListarListaDesplegable(new ListaDesplegable(), tabla, "", "", "", (Usuario)Session["usuario"]);

            if (plista.Count > 0)
            {
                ctlListarCodigoAhorro.TextField = "descripcion";
                ctlListarCodigoAhorro.ValueField = "idconsecutivo";
                ctlListarCodigoAhorro.BindearControl(plista);
            }

            Xpinn.Contabilidad.Data.ParametroCtasOtrosData objeparam = new Xpinn.Contabilidad.Data.ParametroCtasOtrosData();
            List<Par_Cue_LinAho> lista = new Xpinn.Contabilidad.Data.ParametroCtasOtrosData().llenarLista((Usuario)Session["usuario"], ddlTipoAhorro.SelectedValue);
            
            if (lista.Count > 0)
            {
                ctlListarCodigoTransaccion.ValueField = "tipoTrasaccion";
                ctlListarCodigoTransaccion.TextField = "NombreCuenta";
                ctlListarCodigoTransaccion.BindearControl(lista);
            }

            if (plista.Count > 0 || lista.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
            }
        }
        else
        {
            List<ListaDesplegable> plista = new List<ListaDesplegable>();
            ctlListarCodigoAhorro.TextField = "descripcion";
            ctlListarCodigoAhorro.ValueField = "idconsecutivo";

            ctlListarCodigoAhorro.BindearControl(plista);

            List<Par_Cue_LinAho> lista = new List<Par_Cue_LinAho>();
            ctlListarCodigoTransaccion.ValueField = "tipoTrasaccion";
            ctlListarCodigoTransaccion.TextField = "NombreCuenta";

            ctlListarCodigoTransaccion.BindearControl(lista);

            ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
        }
    }

    void eliminarparametro(Usuario usuario, Int64 id)
    {
        try
        {
            objeParametros.EliminarParametroServices(usuario, id);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objeParametros.codigoProgramaParametro, "eliminarparametro", ex);
        }
    }

    public void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            eliminarparametro((Usuario)Session["usuario"], (Int64)Session["idParametro"]);
            Session.Remove("idParametro");
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objeParametros.codigoProgramaParametro, "btnContinuarMen_Click", ex);
        }
    }
}