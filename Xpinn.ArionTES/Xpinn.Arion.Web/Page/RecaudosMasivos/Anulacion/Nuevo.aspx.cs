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
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;

partial class Lista : GlobalWeb
{

    OperacionServices operacionService = new OperacionServices();
    AnulacionServices anulacionservicio = new AnulacionServices();
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(anulacionservicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(anulacionservicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[anulacionservicio.CodigoPrograma + ".id"] != null)
                {
                    txtFechaAnulacion.ToDateTime = DateTime.Now;
                    LlenarComboMotivosAnu(ddlMotivoAnulacion);
                    ObtenerValores(Session[anulacionservicio.CodigoPrograma + ".id"].ToString());
                    LlenarComboTipoCampo(ddlcampo);
                }

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(anulacionservicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    /// <summary>
    /// Método para realizar el proceso de anulación
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        mpeNuevo.Show();
    }

    /// <summary>
    /// Método para el evento de cancelar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    /// <summary>
    /// Método para control de paginación de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[anulacionservicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    /// <summary>
    /// Método para control de edición de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        String cod_radica = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        String nombre = gvLista.Rows[e.NewEditIndex].Cells[4].Text;
        String identificacion = gvLista.Rows[e.NewEditIndex].Cells[3].Text;
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
            BOexcepcion.Throw(anulacionservicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Método para actualziación de datos
    /// </summary>
    private void Actualizar()
    {
        try
        {
            List<AnulacionOperaciones> lstConsulta = new List<AnulacionOperaciones>();

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

            Session.Add(anulacionservicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(anulacionservicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    /// <summary>
    /// Método para llenar datos al DLL del motivo de anulación
    /// </summary>
    /// <param name="ddlMotivoAnu"></param>
    protected void LlenarComboMotivosAnu(DropDownList ddlMotivoAnu)
    {

        Xpinn.Caja.Services.TipoMotivoAnuService motivoAnuService = new Xpinn.Caja.Services.TipoMotivoAnuService();
        Xpinn.Caja.Entities.TipoMotivoAnu motivoAnu = new Xpinn.Caja.Entities.TipoMotivoAnu();
        ddlMotivoAnu.DataSource = motivoAnuService.ListarTipoMotivoAnu(motivoAnu, (Usuario)Session["Usuario"]);
        ddlMotivoAnu.DataTextField = "descripcion";
        ddlMotivoAnu.DataValueField = "tipo_motivo";
        ddlMotivoAnu.DataBind();
    }


    /// <summary>
    /// Método para llenar datos al DLL de los campos de la tabla operacion
    /// </summary>
    /// <param name="ddlMotivoAnu"></param>
    protected void LlenarComboTipoCampo(DropDownList ddlcampo)
    {
        ddlcampo.Items.Insert(0, new ListItem { Text="Seleccionar Item",Value= "" });
        ddlcampo.Items.Insert(1, new ListItem { Text="Radicación",Value= "Numero_Radicacion" });
        ddlcampo.Items.Insert(2, new ListItem { Text = "Cod Línea", Value = "Cod_Linea_Credito" });
        ddlcampo.Items.Insert(3, new ListItem { Text = "Nombre Línea", Value = "Nombre_Linea" });
        ddlcampo.Items.Insert(4, new ListItem { Text = "Cliente", Value = "Cliente" });
        ddlcampo.Items.Insert(5, new ListItem { Text = "Tipo Transacción", Value = "Tipo_Tran" });
        ddlcampo.Items.Insert(6, new ListItem { Text = "Tipo Movimiento", Value = "Tipo_Mov" });
        ddlcampo.Items.Insert(7, new ListItem { Text = "Valor", Value = "valor" });
    }

    /// <summary>
    /// Método para traer los valores de la operación
    /// </summary>
    /// <param name="id"></param>
    protected void ObtenerValores(string id)
    {
        string[] filtros = new string[4] ;
        filtros[0] = id;
        filtros[1] = filtros[2] = filtros[3] = "";
        if (txtRadicacion.Text != "")
            filtros[1] = txtRadicacion.Text;
        if (txtCliente.Text != "")
            filtros[2] = txtCliente.Text;
        if (ddlcampo.SelectedValue != "")
            filtros[3] = ddlcampo.SelectedValue;
        gvLista.DataSource = anulacionservicio.listaranulacionesNuevo(filtros, (Usuario)Session["Usuario"]);
        gvLista.DataBind();

        AnulacionOperaciones vAnula = new AnulacionOperaciones();
        vAnula = anulacionservicio.listaranulacionesentidadnuevo(id, (Usuario)Session["Usuario"]);

        txtoperacion.Text = vAnula.COD_OPE.ToString();
        txttipooperacion.Text = vAnula.TIPO_OPE;
        txtcomprobante.Text = vAnula.NUM_COMP.ToString();
        txttipocomprobante.Text = vAnula.TIPO_COMP;
        txtusuario.Text = vAnula.IDEN_USUARIO;
        if (vAnula.NOM_USUARIO != null)
            txtNombre.Text = vAnula.NOM_USUARIO.ToString();
        txtoficina.Text = vAnula.COD_OFICINA.ToString();
        txtestado.Text = vAnula.ESTADO.ToString();
        if (vAnula.ESTADO.ToString() == "1")
            txtestado.Text = "Activa";
        if (vAnula.ESTADO.ToString() == "2")
            txtestado.Text = "Anulada";
        txtnumlista.Text = vAnula.NUM_LISTA.ToString();
        txtfechaope.Text = vAnula.FECHA_OPER;
        txtfechareal.Text = vAnula.FECHA_REAL;

        lblAnulacion.Text = "";
        AnulacionOperaciones OperacionAnula = new AnulacionOperaciones();
        OperacionAnula = operacionService.ListarOperacionAnula(id, (Usuario)Session["Usuario"]);
        if (OperacionAnula != null)
            lblAnulacion.Text = "Anulada con Op." + OperacionAnula.COD_OPE + " Comp." + OperacionAnula.NUM_COMP + "-" + OperacionAnula.TIPO_COMP + " Usuario:" + OperacionAnula.NOM_USUARIO;
    }


    /// <summary>
    /// Método para grabar la anulación y generar el comprobante
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        VerError("");

        Usuario usuap = (Usuario)Session["usuario"];
        // Valida que exista parametrización contable para la operación
        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
        List<Xpinn.Contabilidad.Entities.ProcesoContable> LstProcesoContable;
        LstProcesoContable = ComprobanteServicio.ConsultaProceso(Convert.ToInt64(txtoperacion.Text), 7, txtFechaAnulacion.ToDateTime, usuap);
        if (LstProcesoContable.Count() == 0)
        {
            VerError("No existen comprobantes parametrizados para esta operación (Tipo 7=Anulación)");
            return;
        }
        int resultado = 0;
        Int64 pnum_comp = 0;
        Int64 ptipo_comp = 0;
        string sError = "";

        List<AnulacionOperaciones> lstTransacciones = new List<AnulacionOperaciones>();
        lstTransacciones = (from GridViewRow rw in gvLista.Rows
                           where ((CheckBoxGrid)rw.FindControl("cbSeleccionar")).Checked
                           select new AnulacionOperaciones { NUMERO_RADICACION = rw.Cells[2].Text,TIPO_PRODUCTO= rw.Cells[7].Text ,COD_OPE=Convert.ToInt64(txtoperacion.Text)}).ToList();

        resultado = 0;
        Int64 num_recaudo = 0;
        num_recaudo =  0;
        // Realizar la anulación
        OperacionServices anulacionservices = new OperacionServices();
        Int64 pcod_proceso = Convert.ToInt64(LstProcesoContable[0].cod_proceso);
        Int64 motivoAnulacion = ddlMotivoAnulacion.SelectedValue == "" ? 0 : long.Parse(ddlMotivoAnulacion.SelectedValue);
        resultado = anulacionservicio.RealizarAnulacion(DateTime.ParseExact(txtFechaAnulacion.ToDate, gFormatoFecha, null), lstTransacciones, pcod_proceso, ref pnum_comp, ref ptipo_comp, ref sError, (Usuario)Session["Usuario"]);
        if (resultado == -1)
        {
            VerError("No fue posible anular la operación o generar el comprobante de la anulación " + sError);
            return;
        }
        else if(resultado == 0)
        {
            VerError("No fue posible anular la operación. " + sError);
            return;
        }
        // Modificar el comprobante            
        Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = pnum_comp;
        Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = ptipo_comp;
        Session[ComprobanteServicio.CodigoPrograma + ".modificar"] = "~/Page/Tesoreria/AnulacionOperaciones/Lista.aspx";
        Response.Redirect("~/Page/Contabilidad/Comprobante/Nuevo.aspx", false);
        mpeNuevo.Hide();

    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        mpeNuevo.Hide();
    }


    protected void btnSeguir_Click(object sender, EventArgs e)
    {
        mvOperacion.ActiveViewIndex = 0;
    }


    protected void cbSeleccionarEncabezado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxGrid cbSeleccionarEncabezado = (CheckBoxGrid)sender;
        if (cbSeleccionarEncabezado != null)
        {
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBoxGrid cbSeleccionar = (CheckBoxGrid)rFila.FindControl("cbSeleccionar");
                cbSeleccionar.Checked = cbSeleccionarEncabezado.Checked;
            }
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        
        ObtenerValores(Session[anulacionservicio.CodigoPrograma + ".id"].ToString());
    }
}

