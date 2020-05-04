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
using Xpinn.Obligaciones.Entities;
using Xpinn.Obligaciones.Services;

partial class Lista : GlobalWeb
{

    ObligacionCreditoService anulacionservicio = new ObligacionCreditoService();



    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(anulacionservicio.CodigoPrograma5, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(anulacionservicio.CodigoPrograma5, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[anulacionservicio.CodigoPrograma5 + ".id"] != null)
                {
                    txtFechaAnulacion.ToDateTime = DateTime.Now;
                    LlenarComboMotivosAnu(ddlMotivoAnulacion);
                    ObtenerValores(Session[anulacionservicio.CodigoPrograma5 + ".id"].ToString());
                }

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(anulacionservicio.CodigoPrograma5, "Page_Load", ex);
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
        Session[anulacionservicio.CodigoPrograma5 + ".id"] = id;
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
            BOexcepcion.Throw(anulacionservicio.CodigoPrograma5, "gvLista_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Método para actualziación de datos
    /// </summary>
    private void Actualizar()
    {
        try
        {
            List<Xpinn.Tesoreria.Entities.AnulacionOperaciones> lstConsulta = new List<Xpinn.Tesoreria.Entities.AnulacionOperaciones>();

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

            Session.Add(anulacionservicio.CodigoPrograma5 + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(anulacionservicio.CodigoPrograma5, "Actualizar", ex);
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
        ddlMotivoAnu.DataSource = motivoAnuService.ListarTipoMotivoAnu(motivoAnu, (Usuario)Session["usuario"]);
        ddlMotivoAnu.DataTextField = "descripcion";
        ddlMotivoAnu.DataValueField = "tipo_motivo";
        ddlMotivoAnu.DataBind();
    }

    /// <summary>
    /// Método para traer los valores de la operación
    /// </summary>
    /// <param name="id"></param>
    protected void ObtenerValores(string id)
    {
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        Xpinn.Tesoreria.Services.OperacionServices anulacionservices = new Xpinn.Tesoreria.Services.OperacionServices();
        gvLista.DataSource = anulacionservices.listaranulacionesNuevo(id, pUsuario);
        gvLista.DataBind();

        Xpinn.Tesoreria.Entities.AnulacionOperaciones lisanula = new Xpinn.Tesoreria.Entities.AnulacionOperaciones();
        lisanula = anulacionservices.listaranulacionesentidadnuevo(id, pUsuario);

        try
        {
            txtoperacion.Text = lisanula.COD_OPE.ToString();
            txttipooperacion.Text = lisanula.TIPO_OPE;
            txtcomprobante.Text = lisanula.NUM_COMP.ToString();
            txttipocomprobante.Text = lisanula.TIPO_COMP;
            txtusuario.Text = lisanula.IDEN_USUARIO;
            if (lisanula.NOM_USUARIO != null)
                txtNombre.Text = lisanula.NOM_USUARIO.ToString();
            txtoficina.Text = lisanula.COD_OFICINA.ToString();
            txtestado.Text = lisanula.ESTADO.ToString();
            if (lisanula.ESTADO.ToString() == "1")
                txtestado.Text = "Activa";
            if (lisanula.ESTADO.ToString() == "2")
                txtestado.Text = "Anulada";
            txtnumlista.Text = lisanula.NUM_LISTA.ToString();
            txtfechaope.Text = lisanula.FECHA_OPER;
            txtfechareal.Text = lisanula.FECHA_REAL;
        }
        catch (Exception ex)
        {
            VerError("Se presento error: " + ex.Message);
        }

        lblAnulacion.Text = "";
        Xpinn.Tesoreria.Entities.AnulacionOperaciones OperacionAnula = new Xpinn.Tesoreria.Entities.AnulacionOperaciones();
        OperacionAnula = anulacionservices.ListarOperacionAnula(id, pUsuario);
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
        // Realizar la anulación
        Xpinn.Tesoreria.Services.OperacionServices anulacionservices = new Xpinn.Tesoreria.Services.OperacionServices();
        Int64 pcod_proceso = Convert.ToInt64(LstProcesoContable[0].cod_proceso);
        Int64 pnum_comp = 0;
        Int64 ptipo_comp = 0;        
        string sError = "";
        int resultado = anulacionservices.RealizarAnulacion(Convert.ToInt64(txtoperacion.Text), Convert.ToDateTime(txtFechaAnulacion.ToDate), Convert.ToString(usuap.codusuario), usuap.cod_oficina, long.Parse(ddlMotivoAnulacion.SelectedValue), pcod_proceso, ref pnum_comp, ref ptipo_comp, ref sError, usuap);
        if (resultado == 1)
        {
            // Generar comprobante de la anulación            
            //Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = txtoperacion.Text;
            //Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 7;
            //Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = txtFechaAnulacion.ToDate;
            //Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
            // Modificar el comprobante            
            Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = pnum_comp;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = ptipo_comp;
            Session[ComprobanteServicio.CodigoPrograma + ".modificar"] = "1";
        }
        else
        {
            VerError("No fue posible anular la operación. " + sError);
        }

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
}

