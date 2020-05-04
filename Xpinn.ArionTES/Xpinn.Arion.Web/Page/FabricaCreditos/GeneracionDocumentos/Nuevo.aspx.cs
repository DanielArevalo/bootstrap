using System;
using System.Diagnostics;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Design;
using System.Threading;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using Xpinn.Interfaces.Entities;
using Xpinn.Interfaces.Services;
using Xpinn.Comun.Entities;

partial class Nuevo : GlobalWeb
{

    private CreditoService CreditoServicio = new CreditoService();
    string cDocsSubDir = "Documentos";
    private CreditoSolicitadoService creditoServicio = new CreditoSolicitadoService();
    Usuario _usuario;
    private Xpinn.FabricaCreditos.Services.ControlCreditosService ControlCreditosServicio = new Xpinn.FabricaCreditos.Services.ControlCreditosService();
    String ListaSolicitada = null;
    private List<Xpinn.FabricaCreditos.Entities.ControlTiempos> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.ControlTiempos>();  //Lista de los menus desplegables
    private Xpinn.FabricaCreditos.Services.ControlTiemposService ControlProcesosServicio = new Xpinn.FabricaCreditos.Services.ControlTiemposService();

    PdfTemplate total;
    BaseFont helv;
    string error = "";
    /// <summary>
    /// Mostrar la barra de herramientas al ingresar a la funcionalidad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            Session.Remove("Generado");
            if (Session[CreditoServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(CreditoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(CreditoServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoPrograma, "Page_PreInit", ex);
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
            if (!IsPostBack)
            {
                Session["HojaDeRuta"] = null;
                CargarListas();
                if (Session[CreditoServicio.CodigoPrograma + ".id"] != null)
                {
                    mvDocumentos.ActiveViewIndex = 0;
                    if (Request.UrlReferrer != null)
                        if (Request.UrlReferrer.Segments[4].ToString() == "HojaDeRuta/")
                            Session["HojaDeRuta"] = "1";
                    idObjeto = Session[CreditoServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove("Generado");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }
    /// <summary>
    ///  Evento para guardar 
    /// </summary>
    protected Boolean GuardarDocumentos()
    {
        try
        {
            // Verificar datos del crédito   
            Credito vCredito = new Credito();
            vCredito = CreditoServicio.ConsultarCredito(Convert.ToInt64(txtNumero_radicacion.Text), (Usuario)Session["usuario"]);
            ControlTiempos proceso = new ControlTiempos();
            CreditoSolicitadoService credito = new CreditoSolicitadoService();
            if (txtEstado.Text == "Aprobado")
            {
                proceso.estado = "A";
            }
            else
            {
                string estado = "Documentos Generados";
                proceso = credito.ConsultarProcesoAnterior(estado, (Usuario)Session["usuario"]);
                if (proceso.estado == "" || proceso.estado == null)
                    proceso.estado = vCredito.estado;
            }

            if (vCredito.estado != proceso.estado)
            {
                string sEstado = "";
                if (!string.IsNullOrEmpty(vCredito.estado))
                    sEstado = EstadoCredito(vCredito.estado);
                if (sEstado != "" && sEstado != txtEstado.Text)
                    ObtenerDatos(idObjeto);
                return false;
            }
            if (idObjeto == "")
            {
                return false;
            }

            // Grabar los documentos del crédito
            DocumentoService documentoServicio = new DocumentoService();
            int numero = 0;
            foreach (GridViewRow row in gvLista.Rows)
            {
                System.Web.UI.WebControls.CheckBox chkSeleccionar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("cbx"));
                if (chkSeleccionar.Checked)
                {
                    Documento document = new Documento() { iddocumento = null, numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text), cod_linea_credito = txtLinea_credito.Text };
                    try
                    {
                        string dato = GetCellByName(row, "Tipo documento").Text;
                        document.tipo_documento = Convert.ToInt64(dato);
                        dato = GetCellByName(row, "Descripción").Text;
                        document.descripcion_documento = dato;
                        dato = GetCellByName(row, "Requerido").Text;
                        document.requerido = dato;
                        System.Web.UI.WebControls.TextBox txtReferencia = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtReferencia"));
                        dato = txtReferencia.Text;
                        document.referencia = dato;
                        dato = GetCellByName(row, "Ruta").Text;
                        document.ruta = dato;
                        documentoServicio.CrearDocumentoGenerado(document, Convert.ToInt64(txtNumero_radicacion.Text), (Usuario)Session["usuario"]);
                        numero += 1;
                    }
                    catch (Exception ex)
                    {
                        lblInfo.Text = String.Format("Ocurrio un error al generar los documentos. \r\n({0})", ex.Message);
                    }
                }
            }
            if (numero == 0)
            {
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "No se seleccionaron documentos para generar";
                return false;
            }

            // Cargar datos del crédito
            vCredito.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text);
            vCredito.identificacion = Convert.ToString(txtIdentificacion.Text.Trim());
            vCredito.tipo_identificacion = Convert.ToString(txtTipo_identificacion.Text.Trim());
            vCredito.nombre = Convert.ToString(txtNombre.Text.Trim());
            vCredito.linea_credito = Convert.ToString(txtLinea_credito.Text.Trim());
            vCredito.monto = Convert.ToInt64(txtMonto.Text.Trim().Replace(".", ""));
            vCredito.plazo = Convert.ToInt64(txtPlazo.Text.Trim());
            vCredito.periodicidad = Convert.ToString(txtPeriodicidad.Text.Trim());
            vCredito.valor_cuota = Convert.ToInt64(txtValor_cuota.Text.Trim().Replace(".", ""));
            vCredito.forma_pago = Convert.ToString(txtForma_pago.Text.Trim());
            vCredito.estado = "G";
            CreditoServicio.ModificarCredito(vCredito, (Usuario)Session["usuario"]);
            consultarControl();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }

        return true;
    }
    /// <summary>
    /// Evento para cancelar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (Session["HojaDeRuta"] == "1")
        {
            Response.Redirect("~/Page/FabricaCreditos/HojaDeRuta/Lista.aspx");
        }
        else
        {
            Session.Remove(CreditoServicio.CodigoPrograma + ".consulta");
            Navegar(Pagina.Lista);
        }
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
            vCredito = CreditoServicio.ConsultarCredito(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vCredito.numero_radicacion != Int64.MinValue)
                txtNumero_radicacion.Text = vCredito.numero_radicacion.ToString().Trim();
            if (vCredito.identificacion != string.Empty)
                txtIdentificacion.Text = HttpUtility.HtmlDecode(vCredito.identificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.tipo_identificacion))
                txtTipo_identificacion.Text = HttpUtility.HtmlDecode(vCredito.tipo_identificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.nombre))
                txtNombre.Text = HttpUtility.HtmlDecode(vCredito.nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.linea_credito))
                txtLinea_credito.Text = HttpUtility.HtmlDecode(vCredito.linea_credito.ToString().Trim());
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
            if (!string.IsNullOrEmpty(vCredito.estado))
                txtEstado.Text = EstadoCredito(vCredito.estado);
            if (!string.IsNullOrEmpty(vCredito.idenprov))
                txtIdenProv.Text = HttpUtility.HtmlDecode(vCredito.idenprov.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.nomprov))
                txtNomProv.Text = HttpUtility.HtmlDecode(vCredito.nomprov.ToString().Trim());
            validarGrid(vCredito);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
    private string EstadoCredito(string pEstado)
    {
        CreditoSolicitadoService creditoServicio = new CreditoSolicitadoService();
        List<CreditoSolicitado> lstCreditos = new List<CreditoSolicitado>();
        lstCreditos = creditoServicio.ListarEstadosCredito((Usuario)Session["usuario"]);
        if (lstCreditos.Count > 0)
        {
            foreach (CreditoSolicitado credito in lstCreditos)
            {
                if (credito.estado == pEstado)
                {
                    return credito.descripcion;
                }
            }
            return pEstado;
        }
        else
        {
            if (pEstado == "A")
                return "Aprobado";
            else if ((pEstado == "G"))
                return "Generado";
            else if ((pEstado == "C"))
                return "Desembolsado";
            else if ((pEstado == "T"))
                return "Terminado";
            else
                return pEstado;
        }


    }
    /// <summary>
    /// Actualizar los datos del crédito
    /// </summary>
    /// <param name="codigo"></param>
    /// <param name="gvLista"></param>
    /// <param name="opcion"></param>
    private void Actualizar(string codigo, GridView gvLista, int opcion)
    {
        try
        {
            DocumentoService documentoServicio = new DocumentoService();
            List<Documento> lstConsulta = new List<Documento>();
            Documento doc = new Documento();
            doc.cod_linea_credito = codigo;
            // Dependiendo del estado del crédito se muestran los documentos
            switch (opcion)
            {
                case 1:
                    lstConsulta = documentoServicio.ListarDocumentoAGenerar(doc, (Usuario)Session["usuario"]);
                    break;
                case 2:
                    doc.numero_radicacion = Convert.ToInt64(idObjeto);
                    lstConsulta = documentoServicio.ListarDocumentoGenerado(doc, (Usuario)Session["usuario"]);
                    break;
            }
            gvLista.PageSize = 10;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();


                // Si no se han generado documentos entonces validar.
                if (opcion == 1)
                {
                    foreach (GridViewRow row in gvLista.Rows)
                    {
                        DocumentoService documentoServicio1 = new DocumentoService();
                        string dato = GetCellByName(row, "Requerido").Text;
                        string tipo = GetCellByName(row, "Tipo documento").Text;
                        string espagare = GetCellByName(row, "Descripción").Text;

                        // Determinar si se puede digitar el número del documento 
                        Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
                        Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
                        pData = ConsultaData.ConsultarGeneral(90169, (Usuario)Session["usuario"]);
                        Int16 Digitannumero = Convert.ToInt16(pData.valor);
                        if (Digitannumero == null)
                        {
                            Digitannumero = 0;
                        }

                        // Si el documento es requerido habilita lo marca como tal
                        if (dato == "Si")
                        {
                            System.Web.UI.WebControls.TextBox txtReferencia = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtReferencia"));
                            System.Web.UI.WebControls.CheckBox chkSeleccionar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("cbx"));
                            chkSeleccionar.Checked = true;
                        }

                        if (dato == "Si" && Digitannumero == 0)
                        {
                            System.Web.UI.WebControls.TextBox txtReferencia = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtReferencia"));
                            System.Web.UI.WebControls.CheckBox chkSeleccionar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("cbx"));
                            if (txtReferencia.Text == "0" || txtReferencia.Text == "")
                            {
                                txtReferencia.Text = txtNumero_radicacion.Text;
                                txtReferencia.Enabled = true;
                            }

                            if (dato == "Si" && espagare.Contains("Paga"))
                            {
                                chkSeleccionar.Checked = true;
                                txtReferencia.Enabled = false;
                                txtReferencia.Text = txtNumero_radicacion.Text;
                            }
                        }
                        else
                        {
                            if (dato == "Si" && espagare.Contains("Paga"))
                            {
                                System.Web.UI.WebControls.CheckBox chkSeleccionar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("cbx"));
                                chkSeleccionar.Checked = true;

                                System.Web.UI.WebControls.TextBox txtReferencia = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtReferencia"));
                                //txtReferencia.Enabled = false;
                                txtReferencia.Text = generar(tipo);
                            }
                            else
                            {
                                System.Web.UI.WebControls.TextBox txtReferencia = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtReferencia"));
                                if (dato == "Si" && Digitannumero == 1)
                                {
                                    System.Web.UI.WebControls.CheckBox chkSeleccionar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("cbx"));
                                    chkSeleccionar.Checked = true;
                                }
                                if (txtReferencia.Text == "0" || txtReferencia.Text == "")
                                {
                                    txtReferencia.Text = txtNumero_radicacion.Text;
                                    txtReferencia.Enabled = true;
                                }
                                if (dato == "Si" && Digitannumero != 1)
                                {
                                    txtReferencia.Enabled = false;
                                    txtReferencia.Text = generar(tipo);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                lbDocumentos.Visible = false;
                lblInfo.Visible = false;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(CreditoServicio.CodigoPrograma + ".consulta", 1);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoPrograma, "Actualizar", ex);
        }
    }
    public string generar(string svar)
    {
        DocumentoService documentoServicio = new DocumentoService();
        return documentoServicio.Listarconsecutivo(svar, (Usuario)Session["Usuario"]);
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
            gvLista.PageIndex = e.NewPageIndex;
            ObtenerDatos(idObjeto);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }
    /// <summary>
    /// Dependiendo del estado del crédito se activan las grillas.
    /// </summary>
    /// <param name="vCredito"></param>
    private void validarGrid(Credito vCredito)
    {
        //Agregado para consultar créditos previos a la generación de documentos
        CreditoSolicitadoService CreditoSolicitadoServicio = new CreditoSolicitadoService();
        ControlTiempos control = new ControlTiempos();
        control = CreditoSolicitadoServicio.ConsultarProcesoAnterior("Documentos Generados", (Usuario)Session["usuario"]);
        if (vCredito.estado == "A" || vCredito.estado == control.estado)
        {
            Actualizar(vCredito.cod_linea_credito, gvLista, 1);
            lbDocumentos.Text += " A GENERAR:";
            gvLista2.Visible = false;
        }
        else
        {
            Actualizar(vCredito.cod_linea_credito, gvLista2, 2);
            gvLista.Visible = false;
            lbDocumentos.Text += " GENERADOS:";
            btnGenerar.Visible = true;
        }
    }
    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        VerError("");
        var conteo = gvLista2.Rows.Count;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Si el crédito esta aprobado entonces grabar los documentos si no solamente mostrarlos
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Agregado para permitir créditos previos a la generación de documentos
        CreditoSolicitadoService CreditoSolicitadoServicio = new CreditoSolicitadoService();
        ControlTiempos control = new ControlTiempos();
        control = CreditoSolicitadoServicio.ConsultarProcesoAnterior("Documentos Generados", (Usuario)Session["usuario"]);

        if (txtEstado.Text == "Aprobado" || txtEstado.Text == control.nom_proceso)
        {
            // Guardar los documentos del crédito
            if (GuardarDocumentos() == false)
                return;
            gvLista.Visible = false;
            Actualizar(txtLinea_credito.Text, gvLista2, 2);
            gvLista2.Visible = true;
            lbDocumentos.Text += " GENERADOS:";
        }
        else
        {
            if (gvLista2.Visible == false)
                ObtenerDatos(idObjeto);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Proceso de generación de los PDFs de los documentos
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        Int64 fTest = 0;
        if (!Int64.TryParse(txtNumero_radicacion.Text, out fTest)) return;

        // Solicitando la información que debe ser mostrada en el documento
        DatosDeDocumentoService datosDeDocumentoServicio = new DatosDeDocumentoService();
        List<DatosDeDocumento> lstDatosDeDocumento = new List<DatosDeDocumento>();
        lstDatosDeDocumento = datosDeDocumentoServicio.ListarDatosDeDocumentoGenerado(Convert.ToInt64(txtNumero_radicacion.Text), (Usuario)Session["usuario"]);

        // Recorrer cada documento y generarlo
        TiposDocumentoService tipoDOCServicio = new TiposDocumentoService();
        lblInfo.Text = "No se ha seleccionado nada para procesar.";
        Boolean bGenerado = false;
        foreach (GridViewRow row in gvLista2.Rows)
        {
            Documento document = new Documento();
            System.Web.UI.WebControls.CheckBox chkGenerar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("chkGenerar"));
            if (chkGenerar.Checked)
            {
                try
                {
                    string html;
                    string cTipoDocumento = GetCellByName(row, "Tipo documento").Text;
                    string cNombreDocumento = GetCellByName(row, "Descripción").Text;
                    string cDocumentoGenerados = Server.MapPath("~/Page/FabricaCreditos/GeneracionDocumentos/" + cDocsSubDir);
                    string cDocumentoGenerado = Server.MapPath("~/Page/FabricaCreditos/GeneracionDocumentos/" + cDocsSubDir + "/" + txtNumero_radicacion.Text.Trim() + "_" + cTipoDocumento + '.' + 'p' + 'd' + 'f');
                    //Si no existe el Directorio lo crea
                    if (!File.Exists(cDocumentoGenerados))
                        Directory.CreateDirectory(cDocumentoGenerados);
                    TiposDocumento tipoDOC = new TiposDocumento();
                    tipoDOC = tipoDOCServicio.ConsultarParametroTipoDocumento((Usuario)Session["usuario"]);
                    if (tipoDOC.tipo_documento == Convert.ToInt64(cTipoDocumento))
                        lstDatosDeDocumento = datosDeDocumentoServicio.ListarDatosDeDocumentoFormato(Convert.ToInt64(txtNumero_radicacion.Text), Convert.ToInt64(cTipoDocumento), (Usuario)Session["usuario"]);
                    tipoDOC = tipoDOCServicio.ConsultarTiposDocumento(Convert.ToInt32(cTipoDocumento), (Usuario)Session["Usuario"]);
                    if (tipoDOC != null)
                    {
                        try
                        {
                            if (tipoDOC.Textos != null)
                            {
                                html = Encoding.ASCII.GetString(tipoDOC.Textos);
                                ProcessesHTML.ReemplazarEnDocumentoDeWordYGuardarPdf(html, lstDatosDeDocumento,
                                    cDocumentoGenerado, ref error);
                            }
                            else
                            {
                                ProcessesHTML.ReemplazarEnDocumentoDeWordYGuardarPdf(tipoDOC.texto, lstDatosDeDocumento,
                                    cDocumentoGenerado, ref error);
                            }
                            if (!string.IsNullOrEmpty(error))
                                VerError(error);
                        }
                        catch (Exception ex)
                        {
                            LinkButton btnImprimir = (LinkButton)row.FindControl("btnImprimir");
                            if (btnImprimir != null)
                                btnImprimir.Visible = false;
                            VerError("Se generó un error al crear el documento : " + cNombreDocumento + " " + ex.Message);
                        }
                    }
                    Boolean bExiste = System.IO.File.Exists(cDocumentoGenerado);
                    if (bExiste)
                    {
                        chkGenerar.Checked = false;
                        bGenerado = true;
                    }
                }
                catch (Exception ex)
                {
                    lblInfo.Text = "Ocurrio un error al generar los documentos. \r\n(" + ex.Message + ")";
                }
            }
        }

        Session.Remove("Generado");

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Si se generaron los documentos correctamente entonces mostrar mensaje;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (bGenerado == true)
        {
            Session["Generado"] = true;
            ObtenerDatos(txtNumero_radicacion.Text);
            lblInfo.Text = "Los DocumentosAnexos del Credito fueron generados, dar click en el icono de impresora de cada documento para visualizarlo";
            Response.Write("<script>window.__doPostBack('','');</script>");
            // Usuario pUsuarios = _usuario;
            CreditoSolicitado eproceso = new CreditoSolicitado();
            eproceso.estado = "Documentos Generados";
            _usuario = (Usuario)Session["Usuario"];
            // Consultar el proceso para aprobacion
            eproceso = creditoServicio.ConsultarCodigodelProceso(eproceso, _usuario);
            ddlProceso.SelectedValue = Convert.ToString(eproceso.Codigoproceso);
            ddlProceso.Visible = true;
            consultarControlGeneracion();
        }
        else
        {
            lblInfo.Text = "No se pudieron procesar los documentos del crédito";
        }

    }
    private void consultarControlGeneracion()
    {
        try
        {
            Usuario pUsuarios = _usuario;
            Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();
            String radicado = Convert.ToString(this.txtNumero_radicacion.Text);
            if (ddlProceso.SelectedValue != null)
            {
                if (ddlProceso.SelectedValue != "")
                {
                    vControlCreditos = ControlCreditosServicio.ConsultarControl_Procesos(Convert.ToInt64(ddlProceso.SelectedValue), radicado, pUsuarios);
                    if (vControlCreditos != null)
                    {
                        String Controlexiste = "";
                        Controlexiste = Convert.ToString(vControlCreditos.codtipoproceso);
                        if (Controlexiste == null)
                        {
                            GuardarControl();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            VerError("No se pudieron grabar datos del control de créditos. Error:" + ex.Message);
        }

    }
    /// <summary>
    /// Borrar los archivos .DOC generados y utilizados para generar los PDFS
    /// </summary>
    public void EliminarArchivosTemporales()
    {
        foreach (string cArchivoABorrar in Directory.GetFiles(Server.MapPath(cDocsSubDir + "\\"), "*.docx"))
        {
            string ArchivoPDF = cArchivoABorrar.Substring(0, cArchivoABorrar.LastIndexOf(".")) + ".pdf";
            if (File.Exists(ArchivoPDF))
                File.Delete(cArchivoABorrar);
        };
    }
    static public DataControlFieldCell GetCellByName(GridViewRow Row, String CellName)
    {
        foreach (DataControlFieldCell Cell in Row.Cells)
        {
            if (Cell.ContainingField.ToString().ToUpper() == CellName.ToUpper())
                return Cell;
        }
        return null;
    }
    /// <summary>
    /// Mostrar un documento PDF y descargarlo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnImprimir_Click(object sender, EventArgs eventArgs)
    {
        VerError("");
        LinkButton btnImprimir = sender as LinkButton;
        String sID = txtNumero_radicacion.Text;
        GridViewRow row = (GridViewRow)btnImprimir.NamingContainer;
        Usuario _user = (Usuario)Session["usuario"];

        string cTipoDocumento = GetCellByName(row, "Tipo documento").Text;
        string cNombreDeArchivo = txtNumero_radicacion.Text.Trim() + "_" + cTipoDocumento + ".pdf";
        string cRutaLocalDeArchivoPDF = Server.MapPath("Documentos\\" + cNombreDeArchivo);

        if (GlobalWeb.bMostrarPDF == true)
        {
            // Copiar el archivo a una ruta local
            try
            {
                using (FileStream fs = File.OpenRead(cRutaLocalDeArchivoPDF))
                {
                    if (fs.Length <= 0)
                        VerError(cRutaLocalDeArchivoPDF);
                    byte[] data = new byte[fs.Length];
                    fs.Read(data, 0, (int)fs.Length);
                    fs.Close();
                    Session["Archivo" + _user.codusuario] = cRutaLocalDeArchivoPDF;
                    frmPrint.ResolveUrl("../../Reportes/Reporte.aspx?Archivo=" + cRutaLocalDeArchivoPDF);
                    mvDocumentos.ActiveViewIndex = 1;
                }
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
                Session[CreditoServicio.CodigoPrograma + ".id"] = sID;
                ObtenerDatos(sID);
            }
        }
        else
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = false;
            Response.AddHeader("Content-Disposition", "attachment;filename=" + cNombreDeArchivo);
            Response.ContentType = "application/octet-stream";
            Response.Flush();
            Response.WriteFile(cRutaLocalDeArchivoPDF);
            Response.End();
        }

    }
    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        _usuario = (Usuario)Session["Usuario"];

        lstDatosSolicitud = ControlProcesosServicio.ListasDesplegables(ListaSolicitada, _usuario);
    }
    private void CargarListas()
    {
        try
        {

            ListaSolicitada = "EstadoProceso";
            TraerResultadosLista();
            ddlProceso.DataSource = lstDatosSolicitud;
            ddlProceso.DataTextField = "ListaDescripcion";
            ddlProceso.DataValueField = "ListaId";
            ddlProceso.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }
    private void GuardarControl()
    {
        Xpinn.FabricaCreditos.Services.ControlCreditosService ControlCreditosServicio = new Xpinn.FabricaCreditos.Services.ControlCreditosService();
        String FechaDatcaredito = "";
        Usuario pUsuario = (Usuario)Session["usuario"];
        Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();
        vControlCreditos.numero_radicacion = Convert.ToInt64(this.txtNumero_radicacion.Text);
        vControlCreditos.numero_radicacion = Convert.ToInt64(this.txtNumero_radicacion.Text);
        vControlCreditos.codtipoproceso = ddlProceso.SelectedItem != null ? ddlProceso.SelectedValue : null;
        vControlCreditos.fechaproceso = Convert.ToDateTime(DateTime.Now);
        vControlCreditos.cod_persona = pUsuario.codusuario;
        vControlCreditos.cod_motivo = 0;
        vControlCreditos.observaciones = "CREDITO CON DOCUMENTOS GENERADOS";
        vControlCreditos.anexos = null;
        vControlCreditos.nivel = 0;
        if (Session["Datacredito"] == null)
        {
            if (FechaDatcaredito == "" || FechaDatcaredito == null || Session["Datacredito"] == "" || Session["Datacredito"].ToString() == "" || Session["Datacredito"] == null)
            {
                vControlCreditos.fechaconsulta_dat = FechaDatcaredito == "" ? DateTime.MinValue : Convert.ToDateTime(FechaDatcaredito.Trim());
            }
            else
            {
                if (FechaDatcaredito != null || FechaDatcaredito != "" || Session["Datacredito"] != null)
                {
                    FechaDatcaredito = Convert.ToString(Session["Datacredito"].ToString());
                    vControlCreditos.fechaconsulta_dat = Convert.ToDateTime(FechaDatcaredito);
                }
            }
        }
        else
        {
            //if (FechaDatcaredito != null || FechaDatcaredito != "" || Session["Datacredito"] != null)
            //{
            //    FechaDatcaredito = Convert.ToString(Session["Datacredito"].ToString());
            //    vControlCreditos.fechaconsulta_dat = Convert.ToDateTime(FechaDatcaredito);
            //}
        }
        vControlCreditos = ControlCreditosServicio.CrearControlCreditos(vControlCreditos, (Usuario)Session["usuario"]);
    }
    private void consultarControl()
    {
        ControlCreditos vControlCreditos = new ControlCreditos();
        string radicado = Convert.ToString(txtNumero_radicacion.Text);

        CreditoSolicitado proceso = new CreditoSolicitado
        {
            estado = "Documentos Generados"
        };

        _usuario = (Usuario)Session["Usuario"];
        // Consultar el proceso para aprobacion
        CreditoSolicitadoService creditoServicio = new CreditoSolicitadoService();
        proceso = creditoServicio.ConsultarCodigodelProceso(proceso, _usuario);

        if (proceso.Codigoproceso > 0)
        {
            ddlProceso.SelectedValue = Convert.ToString(proceso.Codigoproceso);

            try
            {
                ControlCreditosService ControlCreditosServicio = new ControlCreditosService();
                vControlCreditos = ControlCreditosServicio.ConsultarControl_Procesos(Convert.ToInt64(ddlProceso.SelectedValue), radicado, Usuario);

                Int64 Controlexiste = 0;
                Controlexiste = Convert.ToInt64(vControlCreditos.codtipoproceso);
                if (Controlexiste <= 0)
                {
                    GuardarControl();
                }
            }
            catch
            {

            }
        }
    }
    protected void gvLista2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string cTipoDocumento = GetCellByName(e.Row, "Tipo documento").Text;
            string sDocumento = Server.MapPath("~/Page/FabricaCreditos/GeneracionDocumentos/Documentos/" + txtNumero_radicacion.Text.Trim() + "_" + cTipoDocumento + ".pdf");
            Boolean bExiste = System.IO.File.Exists(sDocumento);
            ((System.Web.UI.WebControls.LinkButton)e.Row.FindControl("btnImprimir")).Visible = bExiste;
        }
    }
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        mvDocumentos.ActiveViewIndex = 0;
    }
}