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
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;

partial class Lista : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.CreditoService CreditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();    
    PoblarListas poblar = new PoblarListas();
    /// <summary>
    /// Ingrsesar a la funcionalidad y mostrar la tool bar con botón de consulta y limpiar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CreditoServicio.CodigoProgramaCancelacion, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaCancelacion, "Page_PreInit", ex);
        }
    }

    /// <summary>
    /// Cargar la página, llenar combos de listado de oficinas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
       
        try
        {
            if (!IsPostBack)
            {
                Session["talonario"] = 0;
                CargarDllOficinas();
                CargarDllLineas();
                CargarValoresConsulta(pConsulta, CreditoServicio.CodigoProgramaCancelacion);
                if (Session[CreditoServicio.CodigoProgramaCancelacion + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaCancelacion, "Page_Load", ex);
        }
    }

    /// <summary>
    /// Inhabilitar botón de nuevo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove(CreditoServicio.CodigoProgramaCancelacion + ".id");
    }

    /// <summary>
    /// Botón de consulta de los datos del crédito según los filtros realizados.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();

        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, CreditoServicio.CodigoProgramaCancelacion);
            Actualizar();
        }
    }

    /// <summary>
    /// Boton para limpiar los campos de la pantalla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, CreditoServicio.CodigoProgramaCancelacion);
    }

    /// <summary>
    /// Inhabilitar botón de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    /// <summary>
    /// Acción para cuando se pasa a la siguiente página en la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[CreditoServicio.CodigoProgramaCancelacion + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    /// <summary>
    /// Evento cuando se selecciona un crédito de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[CreditoServicio.CodigoProgramaCancelacion + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    /// <summary>
    /// Evento para borrar datos que en esta funcionalidad no aplica
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    /// <summary>
    /// Esto es cuando se da click para cambio de página de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaCancelacion, "gvLista_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Esto es para actualizar la grilla
    /// </summary>
    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.Credito> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Credito>();
            String filtro = obtFiltro(ObtenerValores());
            lstConsulta = CreditoServicio.ListarCreditoDocumRequeridos(ObtenerValores(), (Usuario)Session["usuario"], filtro);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(CreditoServicio.CodigoProgramaCancelacion + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaCancelacion, "Actualizar", ex);
        }
    }

    /// <summary>
    ///  Esto es para cargar los valores de los filtros en la entidad
    /// </summary>
    /// <returns></returns>
    private Xpinn.FabricaCreditos.Entities.Credito ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.Credito vCredito = new Xpinn.FabricaCreditos.Entities.Credito();
        if (txtNumero_radicacion.Text.Trim() != "")
            vCredito.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text.Trim());
        if(txtIdentificacion.Text.Trim() != string.Empty)
            vCredito.identificacion = Convert.ToString(txtIdentificacion.Text.Trim());
        if(txtNombre.Text.Trim() != "")
            vCredito.nombre = Convert.ToString(txtNombre.Text.Trim());
        if (ddlOficinas.SelectedIndex != 0)
            vCredito.oficina = ddlOficinas.SelectedValue;
        if (ddlLineas.SelectedIndex != 0)
            vCredito.cod_linea_credito = ddlLineas.SelectedValue;
        if (ddlEstado.SelectedItem != null)
            if (ddlEstado.SelectedItem.Value != "")
                vCredito.estado = ddlEstado.SelectedItem.Value;

        return vCredito;
    }

    // carga ddl
    private void CargarDllOficinas()
    {
        poblar.PoblarListaDesplegable("oficina", ddlOficinas, (Usuario)Session["usuario"]);

        Xpinn.FabricaCreditos.Data.OficinaData listaOficina = new Xpinn.FabricaCreditos.Data.OficinaData();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
        //oficina..Estado = 1;
        var lista = listaOficina.ListarOficinas(oficina, (Usuario)Session["usuario"]);

        if (lista != null)
        {
            lista.Insert(0, new Xpinn.FabricaCreditos.Entities.Oficina { Nombre = "Seleccione un Item", Codigo = 0 });
            ddlOficinas.DataSource = lista;
            ddlOficinas.DataTextField = "Nombre";
            ddlOficinas.DataValueField = "Codigo";
            ddlOficinas.DataBind();
        }
    }

    private void CargarDllLineas()
    {
        poblar.PoblarListaDesplegable("lineascredito", ddlLineas, (Usuario)Session["usuario"]);

        Xpinn.FabricaCreditos.Data.LineasCreditoData listaLinea = new Xpinn.FabricaCreditos.Data.LineasCreditoData();
        Xpinn.FabricaCreditos.Entities.LineasCredito linea = new Xpinn.FabricaCreditos.Entities.LineasCredito();
        
        var lista = listaLinea.ListarLineasCredito(linea, (Usuario)Session["usuario"]);

        if (lista != null)
        {
            lista.Insert(0, new Xpinn.FabricaCreditos.Entities.LineasCredito { nom_linea_credito = "Seleccione un Item", cod_lineacredito = 0 });
            this.ddlLineas.DataSource = lista;
            ddlLineas.DataTextField = "nom_linea_credito";
            ddlLineas.DataValueField = "Codigo";
            ddlLineas.DataBind();
        }

    }


    /// <summary>
    /// Obtener el filtro según los criterios ingresados
    /// </summary>
    /// <param name="credito"></param>
    /// <returns></returns>
    private string obtFiltro(Credito credito)
    {
        String filtro = String.Empty;
        if (txtNumero_radicacion.Text.Trim() != "")
            filtro += " and numero_radicacion = " + credito.numero_radicacion;
        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and identificacion like '%" + credito.identificacion + "%'";
        if (txtNombre.Text.Trim() != "")
            filtro += " and nombres like '%" + credito.nombre + "%'";
        if (this.ddlLineas.SelectedIndex != 0)
            filtro += " and cod_linea_credito = '" + credito.cod_linea_credito + "'";
        if (ddlOficinas.SelectedIndex != 0)
            filtro += " and cod_oficina = '" + credito.oficina + "'";
        if (ddlEstado.SelectedItem != null)
            if (ddlEstado.SelectedItem.Value != "")
                filtro += " and estado = '" + ddlEstado.SelectedItem.Value + "' ";
        if (txtCodigoNomina.Text != "")
            filtro += " and cod_nomina like '%" + txtCodigoNomina.Text + "%'";

        filtro += " and (estado In ('S', 'L', 'A', 'G', 'V', 'H','Z','W') Or estado Is Null) ";
        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "Where " + filtro;
        }
        return filtro;
    }

}