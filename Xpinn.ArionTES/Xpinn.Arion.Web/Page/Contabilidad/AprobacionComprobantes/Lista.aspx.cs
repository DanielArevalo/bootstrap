using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;

public partial class Lista : GlobalWeb
{
    Xpinn.Contabilidad.Services.AprobacionComprobantesService ComprobanteServicio = new Xpinn.Contabilidad.Services.AprobacionComprobantesService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(ComprobanteServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvAprobarComprobante.ActiveViewIndex = 0;
                CargarDDList();
                LlenarCombos();
                CargarValoresConsulta(pConsulta, ComprobanteServicio.GetType().Name);
                Session["GENERAL"] = null;
                btnAprobarTodos.Visible = true;
                //CONSULTAR TABLA GENERAL
                Xpinn.Comun.Data.GeneralData ComunData = new Xpinn.Comun.Data.GeneralData();
                Xpinn.Comun.Entities.General General = new Xpinn.Comun.Entities.General();
                try
                {
                    General = ComunData.ConsultarGeneral(8, (Usuario)Session["usuario"]);
                }
                catch {
                    General.valor = "0";
                }
                if (General.valor == "1")
                {
                    btnAprobarTodos.Visible = false;
                    Session["GENERAL"] = 1;    
                }                
                if (Session[ComprobanteServicio.GetType().Name + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "Page_Load", ex);
        }
    }

    private void CargarDDList()
    {
        Xpinn.Contabilidad.Services.TipoComprobanteService TipoComprobanteService = new Xpinn.Contabilidad.Services.TipoComprobanteService();
        Xpinn.Contabilidad.Entities.TipoComprobante TipoComprobante = new Xpinn.Contabilidad.Entities.TipoComprobante();
        ddlTipoComprobante.DataSource = TipoComprobanteService.ListarTipoComprobante(TipoComprobante, "", (Usuario)Session["Usuario"]);
        ddlTipoComprobante.DataTextField = "descripcion";
        ddlTipoComprobante.DataValueField = "tipo_comprobante";
        ddlTipoComprobante.DataBind();



        Xpinn.Contabilidad.Entities.Comprobante TipoComprobantes = new Xpinn.Contabilidad.Entities.Comprobante();
        ddlMotivoAnulacion.DataSource = ComprobanteServicio.ListarComprobanteTipoMotivoAnulacion(TipoComprobantes, (Usuario)Session["Usuario"]);
        ddlMotivoAnulacion.DataTextField = "DESCRIPCION";
        ddlMotivoAnulacion.DataValueField = "TIPO_MOTIVO";
        ddlMotivoAnulacion.DataBind();
        ddlMotivoAnulacion.Items.Insert(0, new ListItem("Motivo Anulación: ", "0"));
        ddlMotivoAnulacion.SelectedIndex = 0;
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        lblMensaje.Text = "";
        Int64 ventanActiva = mvAprobarComprobante.ActiveViewIndex;
        if (ventanActiva == 0)
        {
            try
            {
                GuardarValoresConsulta(pConsulta, ComprobanteServicio.GetType().Name);
                Actualizar();
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "btnConsultar_Click", ex);
            }
        }
        if (ventanActiva == 1)
        {
            Site toolBar = (Site)this.Master;
            toolBar.MostrarLimpiar(true);
            mvAprobarComprobante.ActiveViewIndex = 0;
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        gvDetMovs.Visible = false;
        gvLista.Visible = false;
        txtNumComp.Text = "";
        ddlTipoComprobante.SelectedIndex = 0;
        txtFechaIni.Text = "";
        txtFechaFin.Text = "";
        lblMensajeVerificar.Text = "";
        lblMensaje.Text = "";
        lblTotalRegs.Text = "";
        LimpiarValoresConsulta(pConsulta, ComprobanteServicio.GetType().Name);
    }

    private void Actualizar()
    {
        try
        {
            Configuracion conf = new Configuracion();
            List<Xpinn.Contabilidad.Entities.Comprobante> lstConsulta = new List<Xpinn.Contabilidad.Entities.Comprobante>();
            string sFiltro = " ";
            if (txtFechaIni.TieneDatos)
                if (txtFechaIni.ToDate.Trim() != "")
                    sFiltro = sFiltro + " And fecha >= To_Date('" + txtFechaIni.ToDate.Trim() + "', '" + conf.ObtenerFormatoFecha() + "')";
            if (txtFechaFin.TieneDatos)
                if (txtFechaFin.ToDate.Trim() != "")
                    sFiltro = sFiltro + " And fecha <= To_Date('" + txtFechaFin.ToDate.Trim() + "', '" + conf.ObtenerFormatoFecha() + "')"; ;
            lstConsulta = ComprobanteServicio.ListarComprobanteParaAprobar(ObtenerValores(), (Usuario)Session["usuario"], sFiltro);

            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            }

            Session.Add(ComprobanteServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private Xpinn.Contabilidad.Entities.Comprobante ObtenerValores()
    {
        Xpinn.Contabilidad.Entities.Comprobante Comprobante = new Xpinn.Contabilidad.Entities.Comprobante();

        try
        {
            if (txtNumComp.Text.Trim() != "")
                Comprobante.num_comp = Convert.ToInt64(txtNumComp.Text.Trim());
            if (ddlTipoComprobante.SelectedValue != null && ddlTipoComprobante.SelectedIndex != 0)
                Comprobante.tipo_comp = Convert.ToInt64(ddlTipoComprobante.SelectedValue);
            if (Session["GENERAL"] != null && Session["GENERAL"].ToString() == "1")
                Comprobante.rptaLista = true;
            else
                Comprobante.rptaLista = false;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "ObtenerValores", ex);
        }

        return Comprobante;
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnEliminar");
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        VerError("");
        String id = gvLista.SelectedRow.Cells[1].Text;
        Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = id;
        id = gvLista.SelectedRow.Cells[2].Text;
        Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = id;
        string estado = gvLista.DataKeys[gvLista.SelectedIndex].Values[0].ToString();
        btnAprobar.Visible = true;
        if (estado == "A")
            btnAprobar.Visible = false;
        DateTime dtUltCierre = Convert.ToDateTime(ComprobanteServicio.Consultafecha((Usuario)Session["Usuario"]));
        DateTime fechaComp = new DateTime();
        fechaComp = Convert.ToDateTime(gvLista.SelectedRow.Cells[3].Text);
        if (fechaComp <= dtUltCierre)
        {
            VerError("No puede modificar comprobantes en períodos ya cerrados. Fecha Ultimo Cierre:" + dtUltCierre.ToShortDateString());
            return;
        }

        ObtenerDatos(Session[ComprobanteServicio.CodigoPrograma + ".num_comp"].ToString(), Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"].ToString());
        mvAprobarComprobante.ActiveViewIndex = 1;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarLimpiar(false);
        Session[ComprobanteServicio.CodigoPrograma + ".seleccion"] = gvLista.SelectedIndex;
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
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id;
        id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = id;
        id = gvLista.Rows[e.NewEditIndex].Cells[3].Text;
        Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = id;

        string fecha = gvLista.Rows[e.NewEditIndex].Cells[4].Text;

        if (Convert.ToDateTime(fecha) > Convert.ToDateTime(ComprobanteServicio.Consultafecha((Usuario)Session["Usuario"])))
            Navegar(Pagina.Nuevo);
    }


    protected void btnAnterior_Click(object sender, EventArgs e)
    {
        VerError("");
        lblMensaje.Text = "";
        if (gvLista.SelectedIndex >= 1)
        {
            gvLista.SelectedIndex -= 1;
            String id = gvLista.SelectedRow.Cells[1].Text;
            Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = id;
            id = gvLista.SelectedRow.Cells[2].Text;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = id;

            string estado = gvLista.DataKeys[gvLista.SelectedIndex].Values[0].ToString();
            btnAprobar.Visible = true;
            if (estado == "A")
                btnAprobar.Visible = false;

            ObtenerDatos(Session[ComprobanteServicio.CodigoPrograma + ".num_comp"].ToString(), Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"].ToString());
            Session[ComprobanteServicio.CodigoPrograma + ".seleccion"] = gvLista.SelectedIndex;
        }
    }

    protected void btnSiguiente_Click(object sender, EventArgs e)
    {
        VerError("");
        lblMensaje.Text = "";
        if (gvLista.SelectedIndex + 1 < gvLista.Rows.Count)
        {
            gvLista.SelectedIndex += 1;
            String id = gvLista.SelectedRow.Cells[1].Text;
            Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = id;
            id = gvLista.SelectedRow.Cells[2].Text;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = id;
            
            string estado = gvLista.DataKeys[gvLista.SelectedIndex].Values[0].ToString();
            btnAprobar.Visible = true;
            if (estado == "A")
                btnAprobar.Visible = false;

            ObtenerDatos(Session[ComprobanteServicio.CodigoPrograma + ".num_comp"].ToString(), Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"].ToString());
            Session[ComprobanteServicio.CodigoPrograma + ".seleccion"] = gvLista.SelectedIndex;
        }
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {        
        VerError("");
        if (Session[ComprobanteServicio + "Estado"] != null)
        {
            string estado = "";
            string Error = "";
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            Xpinn.Contabilidad.Entities.Comprobante vComprobante = new Xpinn.Contabilidad.Entities.Comprobante(); 
            // Determinar el número del comprobante
            if (txtNumeroComp.Text != "")
                vComprobante.num_comp = Convert.ToInt64(txtNumeroComp.Text);
            // Determinar el tipo del comprobante
            vComprobante.tipo_comp = Convert.ToInt64(ddlTipoComp.SelectedValue);
            vComprobante.cod_aprobo = pUsuario.codusuario;
            vComprobante.tipo_motivo = Convert.ToInt32(ddlMotivoAnulacion.SelectedValue);
            vComprobante.estado = Session[ComprobanteServicio + "Estado"].ToString();
            if (vComprobante.estado == "A")
                estado = " Aprobado";
            else
                estado = " Anulado";
            if (ComprobanteServicio.AprobarAnularComprobante(vComprobante, ref Error, pUsuario) == true)
            {
                lblMensaje.Text = "Comprobante " + vComprobante.num_comp + "-" + ddlTipoComp.SelectedItem.ToString() + estado + " correctamente";
                Actualizar();
            }
            else
            {
                VerError(Error);
            }
        }
    }

    protected void btnParar_Click(object sender, EventArgs e)
    {
        mpeVerificar.Hide();
    }

    protected void btnAprobar_Click(object sender, EventArgs e)
    {
        if (gvLista.SelectedIndex >= 0)
        {
            lblAnulación.Visible = false;
            ddlMotivoAnulacion.Visible = false;
            Session[ComprobanteServicio + "Estado"] = "A";
            lblMensajeVerificar.Text = "Esta Seguro de APROBAR el comprobante ?";
            mpeVerificar.Show();
        }
    }

    protected void btnAprobarTodos_Click(object sender, EventArgs e)
    {
        ctlMensaje.MostrarMensaje("Desea aprobar todos los comprobantes?");   
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        VerError("");
        string Error = "";
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            Xpinn.Contabilidad.Entities.Comprobante vComprobante = new Xpinn.Contabilidad.Entities.Comprobante();
            vComprobante.num_comp = Convert.ToInt64(rFila.Cells[1].Text);
            vComprobante.tipo_comp = Convert.ToInt64(rFila.Cells[2].Text);
            vComprobante.cod_aprobo = pUsuario.codusuario;
            vComprobante.estado = "A";
            if (ComprobanteServicio.AprobarAnularComprobante(vComprobante, ref Error, pUsuario) == false)
            {
                VerError(Error);
                return;
            }
        }
        lblMensaje.Text = "Comprobantes Aprobados Correctamente";
        Actualizar();
    }

    protected void btnAnular_Click(object sender, EventArgs e)
    {
        if (gvLista.SelectedIndex >= 0)
        {
            lblAnulación.Visible = true;
            Session[ComprobanteServicio + "Estado"] = "N";
            ddlMotivoAnulacion.Visible = true;
            lblMensajeVerificar.Text = "Esta Seguro de ANULAR el comprobante ?";
            mpeVerificar.Show();

        }
    }


    protected void ObtenerDatos(String pIdNComp, String pIdTComp)
    {
        try
        {
            Xpinn.Contabilidad.Entities.Comprobante vComprobante = new Xpinn.Contabilidad.Entities.Comprobante();
            Xpinn.Contabilidad.Entities.DetalleComprobante vDetalleComprobante = new Xpinn.Contabilidad.Entities.DetalleComprobante();

            List<Xpinn.Contabilidad.Entities.DetalleComprobante> LstDetalleComprobante = new List<Xpinn.Contabilidad.Entities.DetalleComprobante>();
            if (ComprobanteServicio.ConsultarComprobante(Convert.ToInt64(pIdNComp), Convert.ToInt64(pIdTComp), ref vComprobante, ref LstDetalleComprobante, (Usuario)Session["Usuario"]))
            {
                // Mostrar datos del encabezado
                txtNumeroComp.Text = HttpUtility.HtmlDecode(vComprobante.num_comp.ToString().Trim());
                txtFecha.Text = vComprobante.fecha.ToShortDateString();
                ddlTipoComp.SelectedValue = vComprobante.tipo_comp.ToString();
                ddlCiudad.SelectedValue = Convert.ToString(vComprobante.ciudad);
                ddlConcepto.SelectedValue = vComprobante.concepto.ToString();

                Usuario usuap = (Usuario)Session["usuario"];
                txtElaboradoPor.Text = Convert.ToString(usuap.nombre);
                txtCodElabora.Text = vComprobante.cod_elaboro.ToString();
                txtCodAprobo.Text = vComprobante.cod_aprobo.ToString();
                txtEstado.Text = vComprobante.estado;
                if (!string.IsNullOrEmpty(vComprobante.iden_benef))
                    txtIdentificacion.Text = HttpUtility.HtmlDecode(vComprobante.iden_benef.ToString().Trim());
                if (!string.IsNullOrEmpty(vComprobante.tipo_identificacion))
                    ddlTipoIdentificacion.SelectedValue = vComprobante.tipo_identificacion.ToString();
                txtNombres.Text = vComprobante.nombre;
                tbxObservaciones.Text = vComprobante.observaciones;

                // Determinar el tipo de pago del comprobante
                if (vComprobante.tipo_pago != null)
                    try
                    {
                        ddlFormaPago.SelectedValue = vComprobante.tipo_pago.ToString();
                    }
                    catch
                    {
                    }

                // Determinar la entidad bancaria y la cuenta bancaria
                ddlEntidad.SelectedValue = vComprobante.entidad.ToString();
                try
                {
                    if (ddlTipoComp.SelectedValue == "5")
                        LlenarCuenta();
                }
                catch (Exception ex)
                {
                    VerError(ex.ToString());
                }
                if (vComprobante.cod_ope != 0 && vComprobante.cod_ope != null)
                {
                    txtCod_Ope.Text = vComprobante.cod_ope.ToString();
                    if (txtCod_Ope.Text != "" || (vComprobante.estado == "E" && txtCod_Ope.Text != "") || (vComprobante.estado == "A" && txtCod_Ope.Text != ""))
                        btnAnular.Visible = false;
                }
                else
                {
                    Xpinn.Tesoreria.Entities.AnulacionOperaciones pAnulacion = new Xpinn.Tesoreria.Entities.AnulacionOperaciones();
                    Xpinn.Tesoreria.Services.OperacionServices anulacionservices = new Xpinn.Tesoreria.Services.OperacionServices();
                    string pFiltro = " (select Max(x.cod_ope) from OPERACION x where x.NUM_COMP = " + pIdNComp.ToString() + " and x.TIPO_COMP = " + pIdTComp.ToString() + ") ";
                    pAnulacion = anulacionservices.listaranulacionesentidadnuevo(pFiltro, (Usuario)Session["usuario"]);
                    if (pAnulacion.COD_OPE != 0)
                    {
                        txtCod_Ope.Text = pAnulacion.COD_OPE.ToString();
                        if (txtCod_Ope.Text != "" || (vComprobante.estado == "E" && txtCod_Ope.Text != "") || (vComprobante.estado == "A" && txtCod_Ope.Text != ""))
                            btnAnular.Visible = false;
                    }
                    else
                        txtCod_Ope.Text = "";
                }
                if (vComprobante.tipo_comp == 1)
                    txtNumSop.Text = vComprobante.num_consig;
                if (vComprobante.tipo_comp == 5)
                    txtNumSop.Text = vComprobante.n_documento;

                // Mostrar datos del detalle
                if ((LstDetalleComprobante == null) || (LstDetalleComprobante.Count == 0))
                {
                    CrearDetalleInicial(0);
                }
                else
                {
                    Session["DetalleComprobante"] = LstDetalleComprobante;
                };

                CalcularTotal();

                gvDetMovs.DataSource = LstDetalleComprobante;
                gvDetMovs.DataBind();

                Activar(vComprobante.tipo_comp);

            }
            ddlTipoComp.SelectedValue = Convert.ToString(vComprobante.tipo_comp);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    private void LlenarCuenta()
    {
        try
        {
            Xpinn.Caja.Services.BancosService BancosService = new Xpinn.Caja.Services.BancosService();
            Xpinn.Caja.Entities.Bancos Bancos = new Xpinn.Caja.Entities.Bancos();
            ddlCuenta.DataTextField = "num_cuenta";
            ddlCuenta.DataValueField = "num_cuenta";
            ddlCuenta.DataSource = BancosService.ListarBancosegrecuentas(ddlEntidad.SelectedValue, (Usuario)Session["Usuario"]);
            ddlCuenta.DataBind();
            ddlCuenta.Items.Insert(0, new ListItem("Sin Cuenta", "0"));
            ddlEntidad.SelectedValue = ddlEntidad.SelectedValue;
        }
        catch
        {
            VerError("...");
        }
    }

    public void CalcularTotal()
    {
        decimal? totdeb = 0.00m;
        decimal? totcre = 0.00m;
        List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
        if (Session["DetalleComprobante"] != null)
        {
            LstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];
            for (int i = 0; i < LstDetalleComprobante.Count; i++)
            {
                if (LstDetalleComprobante[i].valor != null)
                {
                    if (LstDetalleComprobante[i].tipo == "D" | LstDetalleComprobante[i].tipo == "d")
                        totdeb = totdeb + LstDetalleComprobante[i].valor;
                    else
                        totcre = totcre + LstDetalleComprobante[i].valor;
                }
            }
            string sDeb = Convert.ToString(totdeb);
            tbxTotalDebitos.Text = sDeb;
            string sCre = Convert.ToString(totcre);
            tbxTotalCreditos.Text = sCre;
        } 
    }

    private void CrearDetalleInicial(int consecutivo)
    {
        DetalleComprobante pDetalleComprobante = new DetalleComprobante();
        List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();

        pDetalleComprobante.codigo = -1;
        if (txtNumComp.Text != "")
            pDetalleComprobante.num_comp = Convert.ToInt64(txtNumComp.Text);
        if (ddlTipoComp.SelectedValue != null)
            pDetalleComprobante.tipo_comp = Convert.ToInt64(ddlTipoComp.SelectedValue.ToString());
        pDetalleComprobante.cod_cuenta = "";
        pDetalleComprobante.nombre_cuenta = "";
        pDetalleComprobante.centro_costo = null;
        pDetalleComprobante.valor = null;
        pDetalleComprobante.tercero = null;

        LstDetalleComprobante.Add(pDetalleComprobante);
        gvDetMovs.DataSource = LstDetalleComprobante;
        gvDetMovs.DataBind();

        Session["DetalleComprobante"] = LstDetalleComprobante;

    }

    void Activar(Int64 tipo_comprobante)
    {
        if (tipo_comprobante == 1)
        {
            ddlEntidad.Visible = true;
            ddlFormaPago.Visible = true;
            lblEntidad.Visible = true;
            lblFormaPago.Visible = true;
            txtNumSop.Visible = true;
            lblCuenta.Visible = false;
            ddlCuenta.Visible = false;
            lblSoporte.Visible = true;
        }
        else
        {
            if (tipo_comprobante == 5)
            {
                ddlEntidad.Visible = true;
                ddlFormaPago.Visible = true;
                lblEntidad.Visible = true;
                lblFormaPago.Visible = true;
                txtNumSop.Visible = true;
                lblSoporte.Visible = true;
            }
            else
            {
                ddlEntidad.Visible = false;
                ddlFormaPago.Visible = false;
                lblEntidad.Visible = false;
                lblFormaPago.Visible = false;
                lblSoporte.Visible = false;
                txtNumSop.Visible = false;
                lblCuenta.Visible = false;
                ddlCuenta.Visible = false;
            }
        }
    }

    protected void LlenarCombos()
    {
        Usuario usuario = new Usuario();
        usuario = (Usuario)Session["Usuario"];

        Xpinn.Caja.Services.TipoIdenService IdenService = new Xpinn.Caja.Services.TipoIdenService();
        Xpinn.Caja.Entities.TipoIden identi = new Xpinn.Caja.Entities.TipoIden();
        ddlTipoIdentificacion.DataSource = IdenService.ListarTipoIden(identi, usuario);
        ddlTipoIdentificacion.DataTextField = "descripcion";
        ddlTipoIdentificacion.DataValueField = "codtipoidentificacion";
        ddlTipoIdentificacion.DataBind();

        Xpinn.Contabilidad.Services.TipoComprobanteService TipoComprobanteService = new Xpinn.Contabilidad.Services.TipoComprobanteService();
        Xpinn.Contabilidad.Entities.TipoComprobante TipoComprobante = new Xpinn.Contabilidad.Entities.TipoComprobante();

        ddlTipoComp.DataSource = TipoComprobanteService.ListarTipoComprobante(TipoComprobante, "", (Usuario)Session["Usuario"]);
        ddlTipoComp.DataTextField = "descripcion";
        ddlTipoComp.DataValueField = "tipo_comprobante";
        ddlTipoComp.DataBind();

        Xpinn.Caja.Services.TipoPagoService TipoPagoService = new Xpinn.Caja.Services.TipoPagoService();
        Xpinn.Caja.Entities.TipoPago TipoPago = new Xpinn.Caja.Entities.TipoPago();
        ddlFormaPago.DataSource = TipoPagoService.ListarTipoPago(TipoPago, usuario);
        ddlFormaPago.DataTextField = "descripcion";
        ddlFormaPago.DataValueField = "cod_tipo_pago";
        ddlFormaPago.DataBind();

        Xpinn.Caja.Services.BancosService BancosService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos Bancos = new Xpinn.Caja.Entities.Bancos();
        ddlEntidad.DataSource = BancosService.ListarBancos(Bancos, usuario);
        ddlEntidad.DataTextField = "nombrebanco";
        ddlEntidad.DataValueField = "cod_banco";
        ddlEntidad.DataBind();
        ddlEntidad.Items.Insert(0, new ListItem("", ""));

        Xpinn.Caja.Services.CiudadService CiudadService = new Xpinn.Caja.Services.CiudadService();
        Xpinn.Caja.Entities.Ciudad Ciudad = new Xpinn.Caja.Entities.Ciudad();
        ddlCiudad.DataSource = CiudadService.ListadoCiudad(Ciudad, usuario);
        ddlCiudad.DataTextField = "nom_ciudad";
        ddlCiudad.DataValueField = "cod_ciudad";
        ddlCiudad.DataBind();

        Xpinn.Contabilidad.Services.ConceptoService ConceptoService = new Xpinn.Contabilidad.Services.ConceptoService();
        Xpinn.Contabilidad.Entities.Concepto Concepto = new Xpinn.Contabilidad.Entities.Concepto();
        ddlConcepto.DataSource = ConceptoService.ListarConcepto(Concepto, usuario);
        ddlConcepto.DataTextField = "descripcion";
        ddlConcepto.DataValueField = "concepto";
        ddlConcepto.DataBind();

    }

    protected void gvDetMovs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetMovs.PageIndex = e.NewPageIndex;
        gvDetMovs.DataSource = Session["DetalleComprobante"];
        gvDetMovs.DataBind();
    }
    
}