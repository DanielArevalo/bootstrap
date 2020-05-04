using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Text;
using System.IO;

partial class Lista : GlobalWeb
{
    private Xpinn.Cartera.Services.RefinanciacionService RefinanciacionServicio = new Xpinn.Cartera.Services.RefinanciacionService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(RefinanciacionServicio.CodigoPrograma, "L");

            Site toolBar = (Site)Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarExportar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RefinanciacionServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                InicializarListas(ddlOficinas);
                txtFecha.ToDateTime = DateTime.Now;
                CargarValoresConsulta(pConsulta, RefinanciacionServicio.CodigoPrograma);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RefinanciacionServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    /// <summary>
    /// Método para consultar los datos del crédito seleccionado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Page.Validate();
        gvLista.Visible = true;
        if (!txtFecha.TieneDatos)
        {
            VerError("Debe ingresar la fecha de refinanciación");
            return;
        }
        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, RefinanciacionServicio.CodigoPrograma);
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
        ddllineacredito.SelectedIndex = 0;
        lblTotalRegs.Visible = false;
        Site toolBar = (Site)Master;
        toolBar.MostrarExportar(false);
        LimpiarValoresConsulta(pConsulta, RefinanciacionServicio.CodigoPrograma);
    }

    /// <summary>
    /// Método para control de selección de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[RefinanciacionServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    /// <summary>
    /// Méotod para cuando se selecciona un registro de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;

        Session[RefinanciacionServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
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
            BOexcepcion.Throw(RefinanciacionServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Método para llenar la grilla.
    /// </summary>
    private void Actualizar()
    {
        try
        {
            string filtro = obtFiltro();

            // debo filtrar por algo la consulta dura mucho
            if (string.IsNullOrWhiteSpace(filtro))
            {
                return;
            }

            DateTime? pFecha = null;
            if (txtFecha.TieneDatos)
                pFecha = txtFecha.ToDateTime;
            List<Xpinn.FabricaCreditos.Entities.Credito> lstConsulta = RefinanciacionServicio.ListarCredito(pFecha, filtro, Usuario);

            gvLista.PageSize = 15;
            gvLista.EmptyDataText = "Fila de datos vacía";
            gvLista.DataSource = lstConsulta;

            Site toolBar = (Site)Master;
            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                gvLista.DataBind();
                toolBar.MostrarExportar(true);
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                toolBar.MostrarExportar(false);
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }

            Session.Add(RefinanciacionServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RefinanciacionServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    protected void InicializarListas(DropDownList ddlOficinas)
    {
        Xpinn.FabricaCreditos.Services.OficinaService oficinaService = new Xpinn.FabricaCreditos.Services.OficinaService();

        LlenarListasDesplegables(TipoLista.LineasCredito, ddllineacredito);

        int consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas(Convert.ToInt32(Usuario.codusuario), Usuario);
        if (consulta >= 1)
        {
            LlenarListasDesplegables(TipoLista.Oficinas, ddlOficinas);
            ddlOficinas.Items.Insert(0, new ListItem("<Seleccione un Item>", " "));
        }
        else
        {
            ddlOficinas.Items.Insert(0, new ListItem(Convert.ToString(Usuario.nombre_oficina), Convert.ToString(Usuario.cod_oficina)));
            ddlOficinas.DataBind();
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

        if (chkMoras.Checked)
            filtro += " and FECHA_PROXIMO_PAGO >= ( SYSDATE - 30 ) "; // Mora 30 días desde la fecha actual
        if (!string.IsNullOrWhiteSpace(txtNumero_radicacion.Text))
            filtro += " and v_creditos.numero_radicacion = " + txtNumero_radicacion.Text.Trim();
        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
            filtro += " and v_creditos.identificacion like '%" + txtIdentificacion.Text.Trim() + "%'";
        if (!string.IsNullOrWhiteSpace(txtNombre.Text))
            filtro += " and v_creditos.nombres like '%" + txtNombre.Text.Trim() + "%'";
        if (!string.IsNullOrWhiteSpace(ddllineacredito.SelectedValue))
            filtro += " and v_creditos.cod_linea_credito = '" + ddllineacredito.SelectedValue + "'";
        if (!string.IsNullOrWhiteSpace(ddlOficinas.SelectedValue))
            filtro += " and v_creditos.cod_oficina = '" + ddlOficinas.SelectedValue + "'";
        if (txtCodigoNomina.Text != "")
            filtro += " and cod_nomina like '%" + txtCodigoNomina.Text + "%'";

        // Con esto me aseguro que si no filtra por nada devolvera un string vacio
        // Se debe obligar a filtrar por algo porque la consulta dura mucho
        if (!string.IsNullOrWhiteSpace(filtro))
        {
           filtro = filtro.Insert(0, " Where v_creditos.estado = 'C' "); 
        }
        else
        {
            VerError("Debes filtrar por algún valor, o marcar el check box para créditos con 30- días de mora!.");
        }

        return filtro;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvLista);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=refinanciacion.xls");
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
}