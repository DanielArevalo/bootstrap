using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;

partial class Lista : GlobalWeb
{
    OperacionServices anulacionservicio = new OperacionServices();
    string pNomUsuario;
    bool existe;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(anulacionservicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoImprimir += btnImprimir_eventoImprimir;
            toolBar.MostrarImprimir(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(anulacionservicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    private void btnImprimir_eventoImprimir(object sender, ImageClickEventArgs e)
    {
        Session[Usuario.codusuario + "codOpe"] = txtoperacion.Text;
        Session["vengoDeTesoreria"] = true;
        Navegar("../../CajaFin/OperacionCaja/Factura.aspx");
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
                    Generar_Declaracion();
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
        //Usuario usuap = (Usuario)Session["usuario"];
        // usuap = (Usuario)Session["Usuario"]

        //AnulacionOperacionesServices anulacionservices = new AnulacionOperacionesServices();
        //int resultado = anulacionservices.RealizarAnulacion(txtoperacion.Text, Convert.ToString(usuap.codusuario));

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
            ObtenerValores(Session[anulacionservicio.CodigoPrograma + ".id"].ToString());
            
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
    /// Método para traer los valores de la operación
    /// </summary>
    /// <param name="id"></param>
    protected void ObtenerValores(string id)
    {
        OperacionServices anulacionservices = new OperacionServices();
        gvLista.DataSource = anulacionservices.listaranulacionesNuevo(id, (Usuario)Session["Usuario"]);
        gvLista.DataBind();

        AnulacionOperaciones lisanula = new AnulacionOperaciones();
        lisanula = anulacionservices.listaranulacionesentidadnuevo(id, (Usuario)Session["Usuario"]);

        if (lisanula.TIPO_OPE.Contains("120-"))
        {
            Site toolBar = (Site)this.Master;
            toolBar.MostrarImprimir(true);
            toolBar.MostrarEliminar(false);
        }

        txtoperacion.Text = lisanula.COD_OPE.ToString();
        txttipooperacion.Text = lisanula.TIPO_OPE;
        txtcomprobante.Text = lisanula.NUM_COMP.ToString();
        txttipocomprobante.Text = lisanula.TIPO_COMP;
        txtusuario.Text = lisanula.IDEN_USUARIO;
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

        lblAnulacion.Text = "";
        AnulacionOperaciones OperacionAnula = new AnulacionOperaciones();
        OperacionAnula = anulacionservices.ListarOperacionAnula(id, (Usuario)Session["Usuario"]);
        if (OperacionAnula.COD_OPE>0)
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
        OperacionServices anulacionservices = new OperacionServices();
        Int64 pcod_proceso = Convert.ToInt64(LstProcesoContable[0].cod_proceso);
        Int64 pnum_comp = 0;
        Int64 ptipo_comp = 0;
        string sError = "";
        Int64 motivoAnulacion = ddlMotivoAnulacion.SelectedValue == "" ? 0 : long.Parse(ddlMotivoAnulacion.SelectedValue);
        int resultado = anulacionservices.RealizarAnulacion(Convert.ToInt64(txtoperacion.Text), DateTime.ParseExact(txtFechaAnulacion.ToDate, gFormatoFecha, null), Convert.ToString(usuap.codusuario), usuap.cod_oficina, motivoAnulacion, pcod_proceso, ref pnum_comp, ref ptipo_comp, ref sError, (Usuario)Session["Usuario"]);

        if (resultado == 1)
        {
            // Modificar el comprobante            
            Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = pnum_comp;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = ptipo_comp;
            Session[ComprobanteServicio.CodigoPrograma + ".modificar"] = "~/Page/Tesoreria/AnulacionOperaciones/Lista.aspx";
            Response.Redirect("~/Page/Contabilidad/Comprobante/Nuevo.aspx", false);
        }
        else if (resultado == -1)
        {
            VerError("No fue posible anular la operación o generar el comprobante de la anulación " + sError);
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

    //Consultar Documento De Declaracion 
    void Generar_Declaracion()
    {
        try
        {
            pNomUsuario = "output_Declaracion_" + txtoperacion.Text;
            var url = HttpContext.Current.Server.MapPath("../PagosVentanilla/Archivos/" + pNomUsuario + ".pdf");

            var ficherosCarpeta = Directory.GetFiles(Server.MapPath("..\\PagosVentanilla\\Archivos\\"));
            foreach (string ficheroActual in ficherosCarpeta)
            {
                if (ficheroActual.Contains(url))
                    existe = true;
            }
            if (existe)
            {
                ConsultarDocumentos.Controls.Add(new LiteralControl("<a id=\"download\" class=\"btn btn - default navbar- btn\" href=\"../PagosVentanilla/Archivos/" + pNomUsuario + ".pdf\" download=\"" + pNomUsuario + "\">Certificado Declaracion (Pagos Efectivo)</a>"));
            }
        }
        catch (Exception d)
        {
            // ignored
        }
    }
}

