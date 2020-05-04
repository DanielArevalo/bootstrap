using System;
using System.Diagnostics;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Design;
using System.Threading;
using System.Collections.Generic;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Services;
using Xpinn.Comun.Entities;
using Xpinn.Interfaces.Entities;
using Xpinn.Interfaces.Services;

partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.DocumentosRequeridosService documentoservicio = new Xpinn.FabricaCreditos.Services.DocumentosRequeridosService();
    private Xpinn.FabricaCreditos.Services.CreditoService creditoservicio = new Xpinn.FabricaCreditos.Services.CreditoService();
    //private Xpinn.FabricaCreditos.Services.DocumentosRequeridosService DocumentoRequeridoServicio = new Xpinn.FabricaCreditos.Services.DocumentosRequeridosService();
    private Xpinn.FabricaCreditos.Entities.documentosrequeridos documentos = new Xpinn.FabricaCreditos.Entities.documentosrequeridos();

    private Xpinn.FabricaCreditos.Entities.DocumentosAnexos documentos2 = new Xpinn.FabricaCreditos.Entities.DocumentosAnexos();
    private Xpinn.FabricaCreditos.Services.DocumentosAnexosService documentoanexosservicio = new Xpinn.FabricaCreditos.Services.DocumentosAnexosService();


    PoblarListas poblar = new PoblarListas();

    List<Xpinn.FabricaCreditos.Entities.ControlTiempos> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.ControlTiempos>();  //Lista de los menus desplegables
    Usuario _usuario;



    /// <summary>
    /// Mostrar la barra de herramientas al ingresar a la funcionalidad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[documentoservicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(documentoservicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(documentoservicio.CodigoPrograma, "A");

            Site toolBar = (Site)Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(documentoservicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    /// <summary>
    /// Cargar datos de la funcionalidad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {                      
            _usuario = (Usuario)Session["usuario"];
            string radicado = Convert.ToString(Request.QueryString["radicado"]);
            //lista documentos incluyendo documentos de garantías
            if (radicado != null)
            {
                Session[documentoservicio.CodigoPrograma + ".id"] = radicado;                
            }
            if (!IsPostBack)
            {
                CargarDllLineas();
                if (Session[documentoservicio.CodigoPrograma + ".id"] != null)
                {
                    if (Request.UrlReferrer != null)

                        idObjeto = Session[documentoservicio.CodigoPrograma + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                    ChkEntregado_CheckedChanged(this, EventArgs.Empty);
                    ChkEntregadoNuevo_CheckedChanged(this, EventArgs.Empty);
                }
            }            
        }        
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(documentoservicio.CodigoPrograma, "Page_Load", ex);
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        ctlMensaje.MostrarMensaje("Desea grabar los datos de los documentos requeridos");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        if (Session["Documentos"] != "E")
        {
            Guardar();
        }
        if (Session["Documentos"] == "E")
        {
            Modificar();
        }
        string radicado = Convert.ToString(Request.QueryString["radicado"]);
        //lista documentos incluyendo documentos de garantías
        if (radicado != null)
        {
            GuardarGarantias();
        }
    }

    private void GuardarGarantias()
    {
        try
        {
            CheckBox chkentregado;
            TextBox txtobservaciones;
            Label lbltipodocumento;
            TextBox txtfechaentrega;
            TextBox txtfechanexo;
            //Label lblmensaje;
            Label Lblid;
            DocumentosAnexos datos = new DocumentosAnexos();
            Configuracion conf = new Configuracion();
            String format = conf.ObtenerFormatoFecha();

            foreach (GridViewRow wrow in gvListaNuevo.Rows)
            {
                chkentregado = (CheckBox)wrow.FindControl("ChkEntregadoNuevo");
                txtobservaciones = (TextBox)wrow.FindControl("txtobservaciones");
                txtfechaentrega = (TextBox)wrow.FindControl("txtfechaentrega");
                txtfechanexo = (TextBox)wrow.FindControl("txtfechaanexo");
                lbltipodocumento = (Label)wrow.FindControl("Lbldocumento");
                //   lblmensaje = (Label)wrow.FindControl("lblmensaje");
                Lblid = (Label)wrow.FindControl("Lblid");

                if (txtNumero_solicitud.Text != null)
                {
                    datos.numerosolicitud = 0;
                }
                else
                {
                    datos.numerosolicitud = Convert.ToInt32(this.txtNumero_solicitud.Text);

                }
                datos.numero_radicacion = Convert.ToInt32(this.txtNumero_radicacion.Text);
                datos.tipo_documento = Convert.ToInt32(lbltipodocumento.Text);
                Int64 doc = datos.tipo_documento;
                // if (chkentregado.Checked || txtobservaciones.Text != null || txtfechaentrega.Text != null)
                //{
                DocumentosAnexos lstConsultadocumentos = new DocumentosAnexos();

                try
                {
                    if (chkentregado.Checked == true)
                    {
                        if (string.IsNullOrWhiteSpace(txtfechanexo.Text))
                        {
                            VerError("Debe ingresar una fecha de anexo");
                            return;
                        }

                        else
                        {
                            datos.estado = 1;
                            txtfechaentrega.Visible = false;
                            datos.fechaentrega = null;
                            datos.descripcion = txtobservaciones.Text;
                            datos.fechaanexo = Convert.ToDateTime(txtfechanexo.Text);
                        }
                    }
                    if (chkentregado.Checked == false)
                    {
                        if (string.IsNullOrWhiteSpace(txtfechaentrega.Text))
                        {
                            VerError("Debe ingresar una fecha de posible entrega");
                            return;
                        }
                        else
                        {
                            var descripcionDocumentoFaltante = wrow.Cells[2].Text;

                            if (ViewState["DocumentosFaltantes"] == null)
                            {
                                ViewState.Add("DocumentosFaltantes", descripcionDocumentoFaltante);
                            }
                            else
                            {
                                ViewState["DocumentosFaltantes"] += ", " + descripcionDocumentoFaltante;
                            }

                            datos.fechaanexo = null;
                            datos.estado = 0;
                            txtfechaentrega.Visible = true;
                            datos.descripcion = txtobservaciones.Text;
                            datos.fechaentrega = Convert.ToDateTime(txtfechaentrega.Text);
                        }
                    }


                    lstConsultadocumentos = documentoanexosservicio.CrearDocAnexos(datos, (Usuario)Session["usuario"]);
                }
                catch (Exception ex)
                {
                    VerError("Error al guardar los datos, " + ex.Message);
                }
            }
            string radicado = Convert.ToString(Request.QueryString["radicado"]);
            //lista documentos incluyendo documentos de garantías
            if (radicado != null)
            {
                GuardarControl(radicado);
            }
        }
        catch (Exception ex)
        {
            VerError("Error al guardar los datos, " + ex.Message);
        }
    }

        private void Guardar()
    {
        try
        {
            CheckBox chkentregado;
            TextBox txtobservaciones;
            Label lbltipodocumento;
            TextBox txtfechaentrega;
            TextBox txtfechanexo;
            //Label lblmensaje;
            Label Lblid;
            DocumentosAnexos datos = new DocumentosAnexos();
            Configuracion conf = new Configuracion();
            String format = conf.ObtenerFormatoFecha();

            foreach (GridViewRow wrow in gvListaNuevo.Rows)
            {
                chkentregado = (CheckBox)wrow.FindControl("ChkEntregadoNuevo");
                txtobservaciones = (TextBox)wrow.FindControl("txtobservaciones");
                txtfechaentrega = (TextBox)wrow.FindControl("txtfechaentrega");
                txtfechanexo = (TextBox)wrow.FindControl("txtfechaanexo");
                lbltipodocumento = (Label)wrow.FindControl("Lbldocumento");
                //   lblmensaje = (Label)wrow.FindControl("lblmensaje");
                Lblid = (Label)wrow.FindControl("Lblid");

                if (txtNumero_solicitud.Text != null)
                {
                    datos.numerosolicitud = 0;
                }
                else
                {
                    datos.numerosolicitud = Convert.ToInt32(this.txtNumero_solicitud.Text);

                }
                datos.numero_radicacion = Convert.ToInt32(this.txtNumero_radicacion.Text);
                datos.tipo_documento = Convert.ToInt32(lbltipodocumento.Text);
                Int64 doc = datos.tipo_documento;
                // if (chkentregado.Checked || txtobservaciones.Text != null || txtfechaentrega.Text != null)
                //{
                DocumentosAnexos lstConsultadocumentos = new DocumentosAnexos();

                try
                {
                    if (chkentregado.Checked == true)
                    {
                        if (string.IsNullOrWhiteSpace(txtfechanexo.Text))
                        {
                            VerError("Debe ingresar una fecha de anexo");
                            return;
                        }

                        else
                        {
                            datos.estado = 1;
                            txtfechaentrega.Visible = false;
                            datos.fechaentrega = null;
                            datos.descripcion = txtobservaciones.Text;
                            datos.fechaanexo = Convert.ToDateTime(txtfechanexo.Text);
                        }
                    }
                    if (chkentregado.Checked == false)
                    {
                        if (string.IsNullOrWhiteSpace(txtfechaentrega.Text))
                        {
                            VerError("Debe ingresar una fecha de posible entrega");
                            return;
                        }
                        else
                        {
                            var descripcionDocumentoFaltante = wrow.Cells[2].Text;

                            if (ViewState["DocumentosFaltantes"] == null)
                            {
                                ViewState.Add("DocumentosFaltantes", descripcionDocumentoFaltante);
                            }
                            else
                            {
                                ViewState["DocumentosFaltantes"] += ", " + descripcionDocumentoFaltante;
                            }

                            datos.fechaanexo = null;
                            datos.estado = 0;
                            txtfechaentrega.Visible = true;
                            datos.descripcion = txtobservaciones.Text;
                            datos.fechaentrega = Convert.ToDateTime(txtfechaentrega.Text);
                        }
                    }

                    if (Session["Documentos"] == "E")
                    {
                        Int64 Iddocumento = Convert.ToInt64(Lblid.Text);

                        lstConsultadocumentos = documentoanexosservicio.ModificarDocAnexos(datos, Iddocumento, (Usuario)Session["usuario"]);
                    }
                    if (Session["Documentos"] != "E")
                    {
                        lstConsultadocumentos = documentoanexosservicio.CrearDocAnexos(datos, (Usuario)Session["usuario"]);
                    }

                    string radicado = Convert.ToString(Request.QueryString["radicado"]);
                    //lista documentos incluyendo documentos de garantías
                    if (radicado != null)
                    {
                        GuardarControl(radicado);
                    }

                }
                catch (Exception ex)
                {
                    VerError("Error al guardar los datos, " + ex.Message);
                }
            }

            Session[documentoservicio.CodigoPrograma + ".id"] = idObjeto;

            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
            toolBar.MostrarConsultar(false);

            mvNuevo.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(documentoservicio.CodigoPrograma, "Actualizar", ex);
        }
    }


    private void GuardarControl(string radicado)
    {
        try
        { 
            Xpinn.FabricaCreditos.Services.ControlCreditosService ControlCreditosServicio = new Xpinn.FabricaCreditos.Services.ControlCreditosService();
            Usuario pUsuario = _usuario;

            Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();
            vControlCreditos.numero_radicacion = Convert.ToInt64(radicado);
            string proceso = ControlCreditosServicio.obtenerCodTipoProceso("D", _usuario);
            if (!string.IsNullOrWhiteSpace(proceso))
            {
                vControlCreditos.codtipoproceso = proceso;
                vControlCreditos.fechaproceso = Convert.ToDateTime(DateTime.Now);
                vControlCreditos.cod_persona = pUsuario.codusuario;
                vControlCreditos.cod_motivo = 0;
                vControlCreditos.observaciones = "Documentos entregados";
                vControlCreditos.anexos = null;
                vControlCreditos.nivel = -1;
                vControlCreditos = ControlCreditosServicio.CrearControlCreditos(vControlCreditos, _usuario);
            }
            else
            {
                VerError("Ocurrio un error al actualizar el workflow");
            }
        }
        catch (Exception ex)
        {
            VerError("Ocurrio un error al actualizar el workflow" + ex.Message);
            return;
        }
    }



    private void Modificar()
    {
        try
        {
            CheckBox chkentregado;
            TextBox txtobservaciones;
            Label lbltipodocumento;
            TextBox txtfechaentrega;
            TextBox txtfechanexo;
            // Label lblmensaje;
            Label Lblid;
            DocumentosAnexos datos = new DocumentosAnexos();
            Configuracion conf = new Configuracion();
            String format = conf.ObtenerFormatoFecha();

            foreach (GridViewRow wrow in gvLista.Rows)
            {
                chkentregado = (CheckBox)wrow.FindControl("ChkEntregado");
                txtobservaciones = (TextBox)wrow.FindControl("txtobservaciones");
                txtfechaentrega = (TextBox)wrow.FindControl("txtfechaentrega");
                txtfechanexo = (TextBox)wrow.FindControl("txtfechaanexo");
                lbltipodocumento = (Label)wrow.FindControl("Lbldocumento");

                //  lblmensaje = (Label)wrow.FindControl("lblmensaje");
                Lblid = (Label)wrow.FindControl("Lblid");

                if (txtNumero_solicitud.Text != null)
                {
                    datos.numerosolicitud = 0;
                }
                else
                {
                    datos.numerosolicitud = Convert.ToInt32(this.txtNumero_solicitud.Text);

                }
                datos.numero_radicacion = Convert.ToInt32(this.txtNumero_radicacion.Text);
                datos.tipo_documento = Convert.ToInt32(lbltipodocumento.Text);
                Int64 doc = datos.tipo_documento;
                // if (chkentregado.Checked || txtobservaciones.Text != null || txtfechaentrega.Text != null)
                //{
                DocumentosAnexos lstConsultadocumentos = new DocumentosAnexos();

                try
                {
                    if (chkentregado.Checked == true)
                    {
                        if (string.IsNullOrWhiteSpace(txtfechanexo.Text))
                        {
                            VerError("Debe ingresar una fecha de anexo");
                            return;
                        }

                        datos.estado = 1;
                        txtfechaentrega.Visible = false;
                        datos.fechaentrega = null;
                        datos.descripcion = txtobservaciones.Text;
                        datos.fechaanexo = Convert.ToDateTime(txtfechanexo.Text);
                    }
                    if (chkentregado.Checked == false)
                    {
                        if (string.IsNullOrWhiteSpace(txtfechaentrega.Text))
                        {
                            VerError("Debe ingresar una fecha de posible entrega");
                            return;
                        }

                        var descripcionDocumentoFaltante = wrow.Cells[2].Text;

                        if (ViewState["DocumentosFaltantes"] == null)
                        {
                            ViewState.Add("DocumentosFaltantes", descripcionDocumentoFaltante);
                        }
                        else
                        {
                            ViewState["DocumentosFaltantes"] += ", " + descripcionDocumentoFaltante;
                        }

                        datos.fechaanexo = null;
                        datos.estado = 0;
                        txtfechaentrega.Visible = true;
                        datos.descripcion = txtobservaciones.Text;
                        datos.fechaentrega = !string.IsNullOrWhiteSpace(txtfechaentrega.Text) ? Convert.ToDateTime(txtfechaentrega.Text) : default(DateTime?);
                    }

                    if (Session["Documentos"] == "E")
                    {
                        Int64 Iddocumento = Convert.ToInt64(Lblid.Text);

                        lstConsultadocumentos = documentoanexosservicio.ModificarDocAnexos(datos, Iddocumento, (Usuario)Session["usuario"]);
                    }
                }
                catch (Exception ex)
                {
                    VerError("Ocurrio un error al modificar los datos, " + ex.Message);
                    return;
                }
            }

            Session[documentoservicio.CodigoPrograma + ".id"] = idObjeto;

            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
            toolBar.MostrarConsultar(false);

            mvNuevo.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(documentoservicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    /// <summary>
    /// Evento para cancelar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    /// <summary>
    /// Mostrar los datos del crédito
    /// </summary>
    /// <param name="pIdObjeto"></param>
    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Credito vCredito = new Credito();
            vCredito = creditoservicio.ConsultarCredito(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            hiddenCodDeudor.Value = vCredito.cod_deudor.ToString();
            if (vCredito.numero_radicacion != Int64.MinValue)
                txtNumero_radicacion.Text = vCredito.numero_radicacion.ToString().Trim();
            if (vCredito.identificacion != string.Empty)
                txtIdentificacion.Text = HttpUtility.HtmlDecode(vCredito.identificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.tipo_identificacion))
                txtTipo_identificacion.Text = HttpUtility.HtmlDecode(vCredito.tipo_identificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.nombre))
                txtNombre.Text = HttpUtility.HtmlDecode(vCredito.nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.cod_linea_credito))
                this.ddlLineas.SelectedValue = HttpUtility.HtmlDecode(vCredito.cod_linea_credito.ToString().Trim());
            if (vCredito.monto != Int64.MinValue)
                txtMonto.Text = HttpUtility.HtmlDecode(vCredito.monto.ToString().Trim());
            if (vCredito.plazo != Int64.MinValue)
                txtPlazo.Text = HttpUtility.HtmlDecode(vCredito.plazo.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.periodicidad))
                txtPeriodicidad.Text = HttpUtility.HtmlDecode(vCredito.periodicidad.ToString().Trim());
            if (vCredito.valor_cuota != Int64.MinValue)
                txtValor_cuota.Text = HttpUtility.HtmlDecode(vCredito.valor_cuota.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.forma_pago))
                txtForma_pago.Text = HttpUtility.HtmlDecode(vCredito.forma_pago.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.nomestado))
                txtEstado.Text = HttpUtility.HtmlDecode(vCredito.nomestado);

            if (vCredito.fecha_solicitud != DateTime.MinValue)
                txtFechaSolicitud.Text = vCredito.fecha_solicitud.ToString().Trim();
            if (!string.IsNullOrEmpty(vCredito.numero_obligacion))
                this.txtNumero_solicitud.Text = HttpUtility.HtmlDecode(vCredito.numero_obligacion.ToString().Trim());



        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(documentoservicio.CodigoPrograma, "ObtenerDatos", ex);
        }
        ActualizarDocumentos();

    }

    /// <summary>
    /// Es para cuando se cambia a la siguiente página
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //gvLista.PageIndex = e.NewPageIndex;
            ObtenerDatos(idObjeto);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(documentoservicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    void CargarDllLineas()
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
    private void Actualizar()
    {
        try
        {
            List<documentosrequeridos> lstConsulta = new List<documentosrequeridos>();
            documentos.cod_linea_credio = ddlLineas.SelectedValue;
            string radicado = Convert.ToString(Request.QueryString["radicado"]);
            //lista documentos incluyendo documentos de garantías
            if (radicado != null)
            {
                lstConsulta = documentoservicio.ListarDocumentosCredito(radicado, (Usuario)Session["usuario"]);
            }
            else
            {
                lstConsulta = documentoservicio.ListarDocumentosRequeridos(documentos, (Usuario)Session["usuario"]);
            }
            
            gvListaNuevo.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvListaNuevo.EmptyDataText = emptyQuery;
            gvListaNuevo.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvListaNuevo.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvListaNuevo.DataBind();
                //  ValidarPermisosGrilla(gvLista);
            }
            else
            {

                gvListaNuevo.Visible = false;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(documentoservicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(documentoservicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private void ActualizarDocumentos()
    {
        Int64 pId = 0;
        Int32 cod_linea_credito = Convert.ToInt32(ddlLineas.SelectedValue);
        try
        {
            List<DocumentosAnexos> lstConsulta = new List<DocumentosAnexos>();

            documentos2.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);
            lstConsulta = documentoanexosservicio.ListarDocAnexos(documentos2, cod_linea_credito, (Usuario)Session["usuario"]);
            if (lstConsulta != null)
            {
                gvLista.PageSize = 15;
                String emptyQuery = "Fila de datos vacia";
                gvLista.EmptyDataText = emptyQuery;
                gvLista.DataSource = lstConsulta;

                if (lstConsulta.Count > 0)
                {
                    Session["Documentos"] = "E";
                    gvLista.Visible = true;
                    lblTotalRegs.Visible = true;
                    lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                    gvLista.DataBind();
                    //  ValidarPermisosGrilla(gvLista);                    
                    string radicado = Convert.ToString(Request.QueryString["radicado"]);
                    if (radicado != null)
                    {
                        Actualizar();
                    }
                }
                else
                {
                    Actualizar();
                    // gvListaNuevo.Visible = false;
                    lblTotalRegs.Visible = false;
                }

                Session.Add(documentoservicio.CodigoPrograma + ".consulta", 1);
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(documentoservicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    protected void txtPlazo_TextChanged(object sender, EventArgs e)
    {

    }
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ChkEntregado_CheckedChanged(object sender, EventArgs e)
    {

        documentosrequeridos documentosrequeridos = new documentosrequeridos();
        TextBox txtfechaentrega;
        TextBox txtfechaanexo;
        CheckBox ChkEntregado;
        foreach (GridViewRow wrow in gvLista.Rows)
        {
            txtfechaentrega = (TextBox)wrow.FindControl("txtfechaentrega");
            txtfechaanexo = (TextBox)wrow.FindControl("txtfechaanexo");

            ChkEntregado = (CheckBox)wrow.FindControl("ChkEntregado");


            if (ChkEntregado.Checked == true)
            {
                txtfechaentrega.Visible = false;
            }
            else
            {
                txtfechaentrega.Visible = true;
            }

            if (ChkEntregado.Checked == false)
            {
                txtfechaanexo.Visible = false;
            }
            else
            {
                txtfechaanexo.Visible = true;
            }
        }


    }
    protected void ChkEntregado1_CheckedChanged(object sender, EventArgs e)
    {

    }


    protected void ChkEntregadoNuevo_CheckedChanged(object sender, EventArgs e)
    {

        DocumentosAnexos documentosanexos = new DocumentosAnexos();
        TextBox txtfechaentrega;
        TextBox txtfechaanexo;
        CheckBox ChkEntregadoNuevo;
        foreach (GridViewRow wrow in gvListaNuevo.Rows)
        {
            txtfechaentrega = (TextBox)wrow.FindControl("txtfechaentrega");
            txtfechaanexo = (TextBox)wrow.FindControl("txtfechaanexo");

            ChkEntregadoNuevo = (CheckBox)wrow.FindControl("ChkEntregadoNuevo");


            if (ChkEntregadoNuevo.Checked == true)
            {
                txtfechaentrega.Visible = false;
            }
            else
            {
                txtfechaentrega.Visible = true;
            }

            if (ChkEntregadoNuevo.Checked == false)
            {
                txtfechaanexo.Visible = false;
            }
            else
            {
                txtfechaanexo.Visible = true;
            }
        }


    }

    protected void btnCorreo_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");

            TiposDocCobranzasServices _tipoDocumentoServicio = new TiposDocCobranzasServices();

            Empresa empresa = _tipoDocumentoServicio.ConsultarCorreoEmpresa(_usuario.idEmpresa, _usuario);

            Xpinn.Asesores.Entities.Persona persona = _tipoDocumentoServicio.ConsultarCorreoPersona(Convert.ToInt64(hiddenCodDeudor.Value.ToString()), _usuario);

            TiposDocCobranzas modificardocumento = _tipoDocumentoServicio.ConsultarFormatoDocumentoCorreo((int)TipoDocumentoCorreo.ControlDocumentos, _usuario);

            if (string.IsNullOrWhiteSpace(persona.Email))
            {
                VerError("El asociado no tiene email registrado");
                return;
            }
            else if (string.IsNullOrWhiteSpace(empresa.e_mail) || string.IsNullOrWhiteSpace(empresa.clave_e_mail))
            {
                VerError("La empresa no tiene configurado un email para enviar el correo");
                return;
            }
            else if (string.IsNullOrWhiteSpace(modificardocumento.texto))
            {
                VerError("No esta parametrizado el formato del correo a enviar");
                return;
            }

            LlenarDiccionarioGlobalWebParaCorreo();

            modificardocumento.texto = ReemplazarParametrosEnElMensajeCorreo(modificardocumento.texto);

            CorreoHelper correoHelper = new CorreoHelper(persona.Email, empresa.e_mail, empresa.clave_e_mail);
            bool exitoso = correoHelper.EnviarCorreoConHTML(modificardocumento.texto, Correo.Gmail, modificardocumento.descripcion, _usuario.empresa);

            if (!exitoso)
            {
                VerError("Error al enviar el correo");
                return;
            }

            btnCorreo.Text = "Envio Satisfactorio";
            btnCorreo.Enabled = false;
        }
        catch (Exception ex)
        {
            VerError("Error al enviar el correo, " + ex.Message);
        }
    }

    private void LlenarDiccionarioGlobalWebParaCorreo()
    {
        parametrosFormatoCorreo = new Dictionary<ParametroCorreo, string>();

        parametrosFormatoCorreo.Add(ParametroCorreo.NumeroRadicacion, txtNumero_radicacion.Text);
        parametrosFormatoCorreo.Add(ParametroCorreo.Identificacion, txtIdentificacion.Text);
        parametrosFormatoCorreo.Add(ParametroCorreo.NombreCompletoPersona, txtNombre.Text);
        parametrosFormatoCorreo.Add(ParametroCorreo.LineaCredito, ddlLineas.SelectedItem.Text);
        parametrosFormatoCorreo.Add(ParametroCorreo.FechaCredito, txtFechaSolicitud.Text);
        parametrosFormatoCorreo.Add(ParametroCorreo.PlazoCredito, txtPlazo.Text);
        parametrosFormatoCorreo.Add(ParametroCorreo.MontoCredito, txtMonto.Text);

        if (ViewState["DocumentosFaltantes"] != null)
        {
            parametrosFormatoCorreo.Add(ParametroCorreo.DocumentosPendientes, (string)ViewState["DocumentosFaltantes"]);
        }
    }
}