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
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Business;

partial class Lista : GlobalWeb
{
    //private Xpinn.FabricaCreditos.Services.LineasCreditoService LineasCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
    String operacion = "";
    Xpinn.FabricaCreditos.Services.CreditoSolicitadoService creditoServicio = new Xpinn.FabricaCreditos.Services.CreditoSolicitadoService();
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables
    List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitudEstado = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables

    // List<Xpinn.FabricaCreditos.Entities.ControlTiempos> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.ControlTiempos>();  //Lista de los menus desplegables
    private Xpinn.FabricaCreditos.Services.ControlTiemposService ControlProcesosServicio = new Xpinn.FabricaCreditos.Services.ControlTiemposService();
    String ListaSolicitada = null;
    String ListaSolicitada1 = null;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(creditoServicio.CodigoProgramaRotativoReporte, "L");

            Site toolBar = (Site)this.Master;
            //  toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoProgramaRotativoReporte, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarListas(); CargarEstado();
                CargarValoresConsulta(pConsulta, creditoServicio.CodigoProgramaRotativoReporte);
                if (Session[creditoServicio.CodigoProgramaRotativoReporte + ".consulta"] != null)
                    Actualizar();
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoProgramaRotativoReporte, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        //  GuardarValoresConsulta(pConsulta, LineasCreditoServicio.CodigoProgramaRotativo);
        Session["operacion"] = null;
        Session["operacion"] = "N";
        Navegar(Pagina.Nuevo);
        LimpiarFormulario();
        LimpiarValoresConsulta(pConsulta, creditoServicio.CodigoProgramaRotativoReporte);

    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, creditoServicio.CodigoProgramaRotativoReporte);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, creditoServicio.CodigoProgramaRotativoReporte);
        txtFecha.Text = "";
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoProgramaRotativoReporte + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[creditoServicio.CodigoProgramaRotativo + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session["id"] = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session["identificacion"] = gvLista.Rows[e.NewEditIndex].Cells[6].Text;
        Session["nombre"] = gvLista.Rows[e.NewEditIndex].Cells[6].Text;

        Session[creditoServicio.CodigoProgramaRotativo + ".id"] = id;
        Session["operacion"] = "E";
        Navegar("~/Page/FabricaCreditos/Rotativo/Solicitud/Nuevo.aspx");

        //Navegar(Pagina.Nuevo);

    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string id = Convert.ToString(e.Keys[0]);
            //LineasCreditoServicio.EliminarLineasCredito(Convert.ToString(id), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoProgramaRotativoReporte, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(creditoServicio.CodigoProgramaRotativoReporte, "gvLista_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Cargar información de las listas desplegables
    /// </summary>
    private void CargarListas()
    {
        try
        {

            Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
            LineasCredito eLinea = new LineasCredito();
            eLinea.tipo_linea = 2;
            eLinea.estado = 1;
            ddlLinea.DataSource = LineaCreditoServicio.ListarLineasCredito(eLinea, (Usuario)Session["usuario"]);
            ddlLinea.DataTextField = "nom_linea_credito";
            ddlLinea.DataValueField = "Codigo";
            ddlLinea.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlLinea.SelectedIndex = 0;
            ddlLinea.DataBind();


            ListaSolicitada = "Oficinas";
            TraerResultadosLista();
            ddlOficina.DataSource = lstDatosSolicitud;
            ddlOficina.DataTextField = "ListaDescripcion";
            ddlOficina.DataValueField = "ListaId";
            ddlOficina.DataBind();
            ddlOficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));



        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    private void CargarEstado()
    {
        try
        {

            ddlEstado.DataSource = creditoServicio.ListarEstadosCredito((Usuario)Session["usuario"]);
            ddlEstado.DataTextField = "Descripcion";
            ddlEstado.DataValueField = "estado";
            ddlEstado.DataBind();
            ddlEstado.Items.Insert(0, new ListItem("Seleccione un item", "0"));

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.GetType().Name + "L", "CargarEstado", ex);
        }
    }
    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
    }



    private void Actualizar()
    {
        try
        {
            List<CreditoSolicitado> lstConsulta = new List<CreditoSolicitado>();
            CreditoSolicitado credito = new CreditoSolicitado();
            String filtro = obtFiltro(ObtenerValores());
            DateTime pFecha;
            pFecha = txtFecha.ToDateTime == null ? DateTime.MinValue : txtFecha.ToDateTime;

            lstConsulta = creditoServicio.ListarCreditosRotativos(credito, pFecha, (Usuario)Session["usuario"], filtro);

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

            Session.Add(creditoServicio.CodigoProgramaRotativoReporte + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoProgramaRotativoReporte, "Actualizar", ex);
        }
    }

    private CreditoSolicitado ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.CreditoSolicitado vLineasCredito = new Xpinn.FabricaCreditos.Entities.CreditoSolicitado();


        if (ddlLinea.SelectedIndex != 0 && !string.IsNullOrEmpty(ddlLinea.SelectedIndex.ToString()))
            vLineasCredito.cod_linea_credito = Convert.ToString(ddlLinea.SelectedValue);

        if (ddlOficina.SelectedIndex != 0 && !string.IsNullOrEmpty(ddlOficina.SelectedValue))
            vLineasCredito.cod_oficina = Convert.ToInt32(ddlOficina.SelectedValue);


        if (ddlEstado.SelectedIndex != 0 && !string.IsNullOrEmpty(ddlEstado.SelectedValue))
            vLineasCredito.estado = Convert.ToString(ddlEstado.SelectedValue);

        if (txtIdentificacion.Text.Trim() != "")
            vLineasCredito.Identificacion = Convert.ToString(txtIdentificacion.Text.Trim());

        if (txtNumero_radicacion.Text.Trim() != "")
            vLineasCredito.NumeroCredito = Convert.ToInt32(txtNumero_radicacion.Text.Trim());


        if (txtNombre.Text.Trim() != "")
            vLineasCredito.Nombres = txtNombre.Text.Trim().ToUpper();


        return vLineasCredito;
    }


    private string obtFiltro(CreditoSolicitado credito)
    {
        String filtro = String.Empty;

        if (txtNumero_radicacion.Text.Trim() != "")
            filtro += " and numero_radicacion=" + credito.NumeroCredito;

        if (txtNombre.Text.Trim() != "")
            filtro += " and Nombres like '%" + credito.Nombres + "%'";

        if (txtCodigoNomina.Text != "")
            filtro += " and cod_nomina like '%" + txtCodigoNomina.Text + "%'";

        if (ddlLinea.SelectedIndex != 0 && !string.IsNullOrEmpty(ddlLinea.SelectedIndex.ToString()) && ddlLinea.SelectedIndex > 0)
            filtro += " and cod_linea_credito = " + credito.cod_linea_credito;



        if (ddlEstado.SelectedIndex != 0 && !string.IsNullOrEmpty(ddlEstado.SelectedIndex.ToString()))
            filtro += " and estado = " + "'" + credito.estado + "'";


        if (ddlOficina.SelectedIndex != 0 && !string.IsNullOrEmpty(ddlOficina.SelectedIndex.ToString()))
            filtro += " and cod_oficina = " + credito.cod_oficina;


        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and identificacion like '%" + credito.Identificacion + "%'";
        filtro += " and estadolinea=1";
        filtro += " and tipo_linea=2 ";
        return filtro;
    }
}