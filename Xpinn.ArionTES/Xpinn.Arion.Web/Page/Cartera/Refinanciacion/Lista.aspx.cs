using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Cartera.Services;

partial class Lista : GlobalWeb
{
    ReestructuracionService ReestructuracionServicio = new ReestructuracionService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ReestructuracionServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReestructuracionServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarListas();
                CargarValoresConsulta(pConsulta, ReestructuracionServicio.CodigoPrograma);
                if (Session[ReestructuracionServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReestructuracionServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    /// <summary>
    /// Método para consultar los datos del crédito seleccionado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();
        gvLista.Visible = true;

        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, ReestructuracionServicio.CodigoPrograma);
            Actualizar();
        }
    }

    /// <summary>
    /// Método para limpiar los datos en pantalla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        gvLista.DataSourceID = null;
        gvLista.DataBind();
        gvLista.Visible = false;
        txtNombre.Text = "";
        txtIdentificacion.Text = "";
        lblTotalRegs.Visible = false;
        LimpiarValoresConsulta(pConsulta, ReestructuracionServicio.CodigoPrograma);
    }

    /// <summary>
    /// Método para control de selección de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[ReestructuracionServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    /// <summary>
    /// Méotod para cuando se selecciona un registro de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;

        Session[ReestructuracionServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs evt)
    {
        Xpinn.Asesores.Entities.Producto producto = new Xpinn.Asesores.Entities.Producto();

        if (evt.CommandName == "Reestructuración")
        {
            producto.Persona.IdPersona = Convert.ToInt64(evt.CommandArgument.ToString());
            Session[MOV_GRAL_CRED_PRODUC] = producto;
            Navegar(Pagina.Detalle);
        }
    }

    /// <summary>
    /// Método para cambio de página en la grilla
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
            BOexcepcion.Throw(ReestructuracionServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Método para llenar la grilla.
    /// </summary>
    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.Credito> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Credito>();
            String filtro = obtFiltro();
            lstConsulta = ReestructuracionServicio.ListarPersonas((Usuario)Session["usuario"], filtro);

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
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(ReestructuracionServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReestructuracionServicio.CodigoPrograma, "Actualizar", ex);
        }
    }


    /// <summary>
    /// LLenar el dropdownlist que permite filtras por oficinas
    /// </summary>
    /// <param name="ddlOficinas"></param>
    protected void LlenarListas()
    {
        Xpinn.FabricaCreditos.Services.OficinaService oficinaService = new Xpinn.FabricaCreditos.Services.OficinaService();

        int consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas(Convert.ToInt32(Usuario.codusuario), Usuario);
        if (consulta >= 1)
        {
            LlenarListasDesplegables(TipoLista.Oficinas, ddlOficinas);
            ddlOficinas.Items.Insert(0, new ListItem("<Seleccione un Item>", ""));
        }
        else
        {
            ddlOficinas.Items.Insert(0, new ListItem("<Seleccione un Item>", ""));
            ddlOficinas.Items.Insert(1, new ListItem(Convert.ToString(Usuario.nombre_oficina), Convert.ToString(Usuario.cod_oficina)));
        }
    }

    /// <summary>
    /// Generar las condiciones de acuerdo a los filtros ingresados
    /// </summary>
    /// <param name="credito"></param>
    /// <returns></returns>
    string obtFiltro()
    {
        string filtro = string.Empty;

        filtro += " WHERE cod_persona In (Select c.cod_deudor From credito c Where c.cod_deudor = persona.cod_persona And c.estado = 'C' And c.saldo_capital > 0)";

        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
            filtro += " and identificacion like '%" + txtIdentificacion.Text.Trim() + "%'";
        if (!string.IsNullOrWhiteSpace(txtNombre.Text))
            filtro += " and nombres like '%" + txtNombre.Text.Trim() + "%'";
        if (!string.IsNullOrWhiteSpace(ddlOficinas.SelectedValue))
            filtro += " and cod_oficina = '" + ddlOficinas.SelectedValue + "'";
        if (txtCodigoNomina.Text != "")
            filtro += " and cod_nomina like '%" + txtCodigoNomina.Text + "%'";

        return filtro;
    }


}