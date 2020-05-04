using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Cartera.Services;
using Xpinn.Util;

partial class Lista : GlobalWeb
{
   ReliquidacionService ReliquidacionServicio = new ReliquidacionService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ReliquidacionServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReliquidacionServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarListas();
                CargarValoresConsulta(pConsulta, ReliquidacionServicio.CodigoPrograma);
                if (Session[ReliquidacionServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReliquidacionServicio.CodigoPrograma, "Page_Load", ex);
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
            GuardarValoresConsulta(pConsulta, ReliquidacionServicio.CodigoPrograma);
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
        txtNumero_radicacion.Text = "";
        ddlLineasCredito.SelectedIndex = 0;
        lblTotalRegs.Visible = false;
        LimpiarValoresConsulta(pConsulta, ReliquidacionServicio.CodigoPrograma);
    }
    
    /// <summary>
    /// Método para control de selección de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[ReliquidacionServicio.CodigoPrograma + ".id"] = id;
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

        Session[ReliquidacionServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
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
            BOexcepcion.Throw(ReliquidacionServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
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
            string filtro = obtFiltro();
            lstConsulta = ReliquidacionServicio.ListarCredito(Usuario, filtro);
            gvLista.PageSize = 15;
            gvLista.EmptyDataText = "Fila de datos vacía";
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                gvLista.DataBind();
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(ReliquidacionServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReliquidacionServicio.CodigoPrograma, "Actualizar", ex);
        }
    }


    /// <summary>
    /// LLenar el dropdownlist que permite filtras por oficinas
    /// </summary>
    /// <param name="ddlOficinas"></param>
    protected void LlenarListas()
    {
        Xpinn.FabricaCreditos.Services.OficinaService oficinaService = new Xpinn.FabricaCreditos.Services.OficinaService();

        LlenarListasDesplegables(TipoLista.LineasCredito, ddlLineasCredito);

        int consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas(Convert.ToInt32(Usuario.codusuario), Usuario);
        if (consulta >= 1)
        {
            LlenarListasDesplegables(TipoLista.Oficinas, ddlOficinas);
            ddlOficinas.Items.Insert(0, new ListItem("<Seleccione un Item>", ""));
        }
        else
        {
            ddlOficinas.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
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

        filtro += " Where estado = 'C' and cod_linea_credito Not In (Select pl.cod_linea_credito from parametros_linea pl where pl.cod_parametro = 320)";

        if (!string.IsNullOrWhiteSpace(txtNumero_radicacion.Text))
            filtro += " and numero_radicacion = " + txtNumero_radicacion.Text.Trim();
        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
            filtro += " and identificacion like '%" + txtIdentificacion.Text.Trim() + "%'";
        if (!string.IsNullOrWhiteSpace(txtNombre.Text))
            filtro += " and nombres like '%" + txtNombre.Text.Trim() + "%'";
        if (!string.IsNullOrWhiteSpace(ddlLineasCredito.SelectedValue))
            filtro += " and cod_linea_credito = '" + ddlLineasCredito.SelectedValue + "'";
        if (ddlOficinas.SelectedIndex > 0)
            filtro += " and cod_oficina = '" + ddlOficinas.SelectedValue + "'";
        if (txtCodigoNomina.Text != "")
            filtro += " and cod_nomina like '%" + txtCodigoNomina.Text + "%'";

        return filtro;
    }

    
}