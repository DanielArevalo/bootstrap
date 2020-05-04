  using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using System.Configuration;

public partial class Detalle : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.consultasdatacreditoService consultasdatacreditoServicio = new Xpinn.FabricaCreditos.Services.consultasdatacreditoService();
    private Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private Xpinn.FabricaCreditos.Services.SolicitudCreditosRecogidosService SolicitudCreditosRecogidosServicio = new Xpinn.FabricaCreditos.Services.SolicitudCreditosRecogidosService();
    private Xpinn.FabricaCreditos.Services.ReferenciaService ReferenciaServicio = new Xpinn.FabricaCreditos.Services.ReferenciaService();
    private Xpinn.FabricaCreditos.Services.DocumentosAnexosService DocumentosAnexosServicio = new Xpinn.FabricaCreditos.Services.DocumentosAnexosService();
    private Xpinn.FabricaCreditos.Services.codeudoresService CodeudorServicio = new Xpinn.FabricaCreditos.Services.codeudoresService();
    private Xpinn.FabricaCreditos.Services.ControlCreditosService ControlCreditosServicio = new Xpinn.FabricaCreditos.Services.ControlCreditosService();
    private List<Xpinn.FabricaCreditos.Entities.ControlTiempos> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.ControlTiempos>();  //Lista de los menus desplegables
    private Xpinn.FabricaCreditos.Services.ControlTiemposService ControlProcesosServicio = new Xpinn.FabricaCreditos.Services.ControlTiemposService();
    Xpinn.FabricaCreditos.Services.DatosSolicitudService DatosSolicitudServicio = new Xpinn.FabricaCreditos.Services.DatosSolicitudService();
    private CreditoRecogerService creditoRecogerServicio = new CreditoRecogerService();
    private ModalidadTasaService ModalidadTasaServicio = new ModalidadTasaService();
    private CreditoSolicitadoService creditoServicio = new CreditoSolicitadoService();
    private DatosAprobadorService datosServicio = new DatosAprobadorService();
    private AnexoCreditoService anexoServicio = new AnexoCreditoService();
    private PeriodicidadService periodicidadServicio = new PeriodicidadService();    
    Xpinn.FabricaCreditos.Data.DocumentoData DADocumentos = new Xpinn.FabricaCreditos.Data.DocumentoData();
    Xpinn.FabricaCreditos.Entities.Documento cDocumentos = new Xpinn.FabricaCreditos.Entities.Documento();  
      
    String FechaDatcaredito = "";
    String pIdCodLinea = "";
    String tasa = "0";
    String ListaSolicitada = null;
    Int64 tipoempresa = 0;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[creditoServicio.CodigoProgramaRotativoAprobar + ".id"] != null)
                VisualizarOpciones(creditoServicio.CodigoProgramaRotativoAprobar, "E");
            else
                VisualizarOpciones(creditoServicio.CodigoProgramaRotativoAprobar, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;                  
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoProgramaRotativoAprobar, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Permite modificar cierta información cuando es perfil ADMINISTRADOR.
            bool bHabilitar = false;
            if (((Usuario)Session["usuario"]).codperfil == 1)
                bHabilitar = true;

            if (!IsPostBack)
            {
                mvAprobacion.ActiveViewIndex = 0;
                Session["Distribucion"] = null;
                Session["ES_EMPLEADO"] = null;
                gvDeducciones.Enabled = bHabilitar; 
                rbCalculoTasa.Visible = false;
                PanelFija.Visible = false;
                PanelHistorico.Visible = false;
                // Determinar si el usuario puede modificar la tasa
                OficinaService oficinaService = new OficinaService();
                Usuario usuap = (Usuario)Session["usuario"];
                int cod = Convert.ToInt32(usuap.codusuario);
                int consulta = oficinaService.UsuarioPuedecambiartasas(cod, (Usuario)Session["usuario"]);
                if (consulta == 0) // Si no tiene atribuciones                 
                    chkTasa.Visible = false;
                else
                    chkTasa.Visible = true;


                // Determinar como se modifican los datos
                rbTipoAprobacion.SelectedIndex = 0;
                rbTipoAprobacion_SelectedIndexChanged(rbTipoAprobacion, null);
                tipoempresa = Convert.ToInt64(usuap.tipo);
                //if (tipoempresa == 2)
                //{
                //    lbledad.Visible = false;
                //    txtEdad.Visible = false;
                //    mvAprobacion.ActiveViewIndex = 0;
                //    Lblaprob.Text = "APROBACIÓN DE CRÉDITOS";
                //    panelAprobacion.Visible = false;
                //}
               // if (tipoempresa == 1)
               // {
                 //   lbledad.Visible = true;
                    mvAprobacion.ActiveViewIndex = 0;
                    Lblaprob.Text = "APROBACIÓN DE CRÉDITOS MODIFICANDO CONDICIONES";
              //  }
                Session["COD_LINEA"] = null;
                Session["HojaDeRuta"] = null;
                CargarListas();
                   if (Session[creditoServicio.CodigoProgramaRotativoAprobar + ".id"] != null)
                {
                    // Determinar si el llamado se hizo desde la hoja de ruta
                    if (Request.UrlReferrer != null)
                        if (Request.UrlReferrer.Segments[4].ToString() == "HojaDeRuta/")
                            Session["HojaDeRuta"] = "1";
                    // Mostrar los datos del crédito
                    idObjeto = Session[creditoServicio.CodigoProgramaRotativoAprobar + ".id"].ToString();
                    ObtenerDatos(idObjeto);


                    //DETERMINAR SI SE VISUALIZARA EL CAMPO DE APORTE Y COMISION
                    bool comision = false;
                    bool aporte = false;
                    bool seguro = false;
                    oficinaService.ValidarComisionAporte(Session["COD_LINEA"].ToString(), ref comision, ref aporte, ref seguro, (Usuario)Session["usuario"]);
                    //txtComision.Visible = comision;
                    //lblComision.Visible = comision;
                    //txtAporte.Visible = aporte;
                    //lblAporte.Visible = aporte;
                    //txtSeguro.Visible = seguro;
                    //lblSeguro.Visible = seguro;
                    Session.Remove("COD_LINEA");


                    ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
                }
               
            }                            
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoProgramaRotativoAprobar, "Page_Load", ex);
        }
    }
    protected List<Xpinn.Comun.Entities.ListasFijas> ListarTiposdeDecuento()
    {
        GlobalWeb glob = new GlobalWeb();
        return glob.ListaCreditoTipoDeDescuento();
    }

    protected List<Xpinn.Comun.Entities.ListasFijas> ListaCreditoTipoDeLiquidacion()
    {
        GlobalWeb glob = new GlobalWeb();
        return glob.ListaCreditoTipoDeLiquidacion();
    }

    protected List<Xpinn.Comun.Entities.ListasFijas> ListaCreditoFormadeDescuento()
    {
        GlobalWeb glob = new GlobalWeb();
        return glob.ListaCreditoFormadeDescuento();
    }

    protected List<Xpinn.Comun.Entities.ListasFijas> ListaImpuestos()
    {
        List<Xpinn.Comun.Entities.ListasFijas> lstImpuestos = new List<Xpinn.Comun.Entities.ListasFijas>();
        lstImpuestos.Add(new Xpinn.Comun.Entities.ListasFijas { codigo = "", descripcion = "" });
        lstImpuestos.Add(new Xpinn.Comun.Entities.ListasFijas { codigo = "0", descripcion = "" });
        return lstImpuestos;
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (Session["HojaDeRuta"] == "1")
            Response.Redirect("~/Page/FabricaCreditos/HojaDeRuta/Lista.aspx");
        else
        {
            Navegar(Pagina.Lista);
        }
    }
 
    /// <summary>
    /// Método para filtrado de los datos
    /// </summary>
    /// <returns></returns>
    private Xpinn.FabricaCreditos.Entities.consultasdatacredito ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.consultasdatacredito vconsultasdatacredito = new Xpinn.FabricaCreditos.Entities.consultasdatacredito();

        if (txtId.Text != "")
            vconsultasdatacredito.cedulacliente = txtId.Text;

        return vconsultasdatacredito;
    }


    /// <summary>
    /// Método que muestra los datos del crédito a aprobar
    /// </summary>
    /// <param name="pIdObjeto"></param>
    protected void ObtenerDatos(String pIdObjeto)
    {      
        try
        {
            CreditoSolicitado credito = new CreditoSolicitado();

            if (pIdObjeto != null)
            {
                credito.NumeroCredito = Int64.Parse(pIdObjeto);
                credito = creditoServicio.ConsultarCredito(credito, (Usuario)Session["usuario"]);
                if (credito.LineaCredito != null)  //Se muestra el detalle sólo si este existe
                {
                    if (credito.estado != "S" && credito.estado != "V" && credito.estado != "W")
                    {
                        VerError("El crédito no esta en el estado correspondiente para poder ser aprobado. " + credito.estado);
                        Site toolBar = (Site)this.Master;
                        toolBar.MostrarConsultar(false);
                        btnAcpAprobar.Visible = false;
                        btnAcpAproModif.Visible = false;
                        btnAcpAplazar.Visible = false;
                        btnAcpNegar.Visible = false;
                        btnCncAprobar.Visible = false;
                        btnCncNegar.Visible = false;
                        btnCncAproModif.Visible = false;
                       
                        txtObsApr.Enabled = false;
                    }
                    Validarestado(credito.CodigoCliente, credito.Identificacion);
                    Session["TipoCredito"] = credito.cod_clasifica;
                    DropLineaCredito();
                    if (!string.IsNullOrEmpty(credito.NumeroCredito.ToString()))
                        txtCredito.Text = HttpUtility.HtmlDecode(credito.NumeroCredito.ToString());
                    if (!string.IsNullOrEmpty(credito.Asesor))
                        txtAsesor.Text = HttpUtility.HtmlDecode(credito.Asesor.ToString());
                    if (!string.IsNullOrEmpty(credito.Calificacion.ToString()))
                        txtCalificacion.Text = HttpUtility.HtmlDecode(credito.Calificacion.ToString());
                    if (!string.IsNullOrEmpty(credito.CodigoCliente.ToString()))
                        txtCodigo.Text = HttpUtility.HtmlDecode(credito.CodigoCliente.ToString());
                      if (!string.IsNullOrEmpty(credito.Cuota.ToString()))
                        txtCuota.Text = HttpUtility.HtmlDecode(credito.Cuota.ToString());
                    if (!string.IsNullOrEmpty(credito.Disponible.ToString()))
                        txtDisp.Text = HttpUtility.HtmlDecode(credito.Disponible.ToString());
                    if(!string.IsNullOrEmpty(credito.forma_pago.ToString()))
                        txtFormaPago.Text=HttpUtility.HtmlDecode(credito.forma_pago.ToString());
                    if (!string.IsNullOrEmpty(credito.fecha_primer_pago.ToString()))
                    ucFechaPrimerPago.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(credito.fecha_primer_pago.ToString()));
                    if (credito.fecha_primer_pago != null)
                        ucFechaPrimerPago.ToDateTime = Convert.ToDateTime(credito.fecha_primer_pago);
                    else
                    {
                        try
                        {
                            CreditoService CreditoServicio = new CreditoService();
                            DateTime? FechaInicio = CreditoServicio.FechaInicioDESEMBOLSO(credito.NumeroCredito, (Usuario)Session["Usuario"]);
                            if (FechaInicio != null)
                                ucFechaPrimerPago.Text = Convert.ToDateTime(FechaInicio).ToShortDateString();
                        }
                        catch
                        { }
                    }
                                         
                    if (!string.IsNullOrEmpty(credito.Identificacion))
                        txtId.Text = HttpUtility.HtmlDecode(credito.Identificacion.ToString());
                    if (!string.IsNullOrEmpty(credito.LineaCredito))
                        txtLinea.Text = HttpUtility.HtmlDecode(credito.LineaCredito.ToString());
                    if (credito.cod_linea_credito != null)
                        Session["COD_LINEA"] = credito.cod_linea_credito; 
                    if (!string.IsNullOrEmpty(credito.Monto.ToString()))
                    {
                        txtMonto.Text = HttpUtility.HtmlDecode(credito.Monto.ToString());
                        txtMonto2.Text = HttpUtility.HtmlDecode(credito.Monto.ToString());
                    }
                    if (!string.IsNullOrEmpty(credito.Nombres))                    
                        txtNombres.Text = HttpUtility.HtmlDecode(credito.Nombres.ToString());
                    if (!string.IsNullOrEmpty(credito.Cod_Periodicidad.ToString()))
                    {
                        ddlPlazo.SelectedValue = HttpUtility.HtmlDecode(credito.Cod_Periodicidad.ToString());
                    }
                    if (!string.IsNullOrEmpty(credito.Periodicidad))
                    {
                        txtPeriodicidad.Text = HttpUtility.HtmlDecode(credito.Periodicidad);
                    }
                  //  ctlBusquedaProveedor.VisibleCtl = false;
                    if (!string.IsNullOrEmpty(credito.cod_linea_credito))
                    {
                        string linea = HttpUtility.HtmlDecode(credito.cod_linea_credito.ToString());
                        ListItemCollection lista = ddlLineaCredito.Items;
                        ListItem listaitem = lista.FindByValue(linea);
                        if (listaitem == null)
                        {
                            listaitem = new ListItem();
                            listaitem.Value = linea;
                            listaitem.Text = credito.LineaCredito;
                            ddlLineaCredito.Items.Add(listaitem);
                        }
                        ddlLineaCredito.SelectedValue = linea;
                        if (credito.cod_linea_credito != null)
                        {
                            Xpinn.FabricaCreditos.Services.LineasCreditoService LineasCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
                            Xpinn.FabricaCreditos.Entities.LineasCredito vLineasCredito = new Xpinn.FabricaCreditos.Entities.LineasCredito();
                           //Rotativo
                            Int64 tipocredito = 2;
                            vLineasCredito = LineasCreditoServicio.ConsultarLineasCreditoRotativo(Convert.ToInt64(tipocredito), (Usuario)Session["usuario"]);
                                                    
                        }
                    }
                    txtCredito2.Text = HttpUtility.HtmlDecode(credito.NumeroCredito.ToString());
                    txtCredito3.Text = HttpUtility.HtmlDecode(credito.NumeroCredito.ToString());
                    txtCredito4.Text = HttpUtility.HtmlDecode(credito.NumeroCredito.ToString());
                    txtCredito5.Text = HttpUtility.HtmlDecode(credito.NumeroCredito.ToString());
                   
                    if (!string.IsNullOrEmpty(credito.Plazo))
                    {
                        txtPlazo.Text = HttpUtility.HtmlDecode(credito.Plazo.ToString());
                        txtPlazo2.Text = HttpUtility.HtmlDecode(credito.Plazo.ToString());
                    }
                                
                    if (!string.IsNullOrEmpty(credito.cod_linea_credito.ToString()))
                        pIdCodLinea = HttpUtility.HtmlDecode(credito.cod_linea_credito.ToString());
                    if (!string.IsNullOrEmpty(credito.Disponible.ToString()))
                    {
                        try
                        {
                            ucFechaPrimerPago.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(credito.fecha_primer_pago.ToString()));
                        }
                        catch {}
                    }
                    if (credito.forma_pago != null && credito.forma_pago != "")
                        ddlFormaPago.SelectedValue = credito.forma_pago;

                    if (txtFormaPago.Text == "1" || txtFormaPago.Text == "C" || txtFormaPago.Text == "c")
                        txtFormaPago.Text = "Caja";
                    if (txtFormaPago.Text == "2" || txtFormaPago.Text == "N" || txtFormaPago.Text == "n")
                        txtFormaPago.Text = "Nomina";

                    if (credito.forma_pago == "1")
                        ddlFormaPago.SelectedValue = "C";
                    if (credito.forma_pago == "2")
                        ddlFormaPago.SelectedValue = "N";
                 
                    // LLenar los dropdownlist para aplazar el crédito o negar
                    DropAplazar();
                    DropNegar();
                    // Cargar las diferentes tablas del sistema
                    TablaCodeudores();

                    // Mostrar los datos de descuentos del crédito
                    gvDeducciones.DataSource = credito.lstDescuentosCredito;
                    gvDeducciones.DataBind();


                }

                
  }           
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoProgramaRotativoAprobar, "ObtenerDatos", ex);
        }
    }

    protected Boolean Validarestado(Int64 pCodPersona, String pIdentificacion)
    {
        Session["ES_EMPLEADO"] = "false";
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Boolean resultado = true;
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        vPersona1.seleccionar = "Identificacion";
        vPersona1.identificacion = pIdentificacion;
        vPersona1 = DatosClienteServicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);
        if (vPersona1.estado == null)
        {
            vPersona1 = DatosClienteServicio.ConsultarPersona1(pCodPersona, (Usuario)Session["usuario"]);
            if (vPersona1.estado != null)
            {
                if (vPersona1.estado != Convert.ToString('A'))
                {
                    VerError("Su estado no esta activo");
                    resultado = false;
                }
                else
                    Session["ES_EMPLEADO"] = "true";
            }
        }
        else
        {
            if (vPersona1.estado != Convert.ToString('A'))
            {
                VerError("Su estado no esta activo");
                resultado = false;
            }
        }
        return resultado;
    }

    private Xpinn.FabricaCreditos.Entities.Persona1 ObtenerValoresCodeudores()
    {
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();

        if (idObjeto != "")
            vPersona1.numeroRadicacion = Convert.ToInt64(idObjeto.ToString());

        vPersona1.seleccionar = "CD"; //Bandera para ejecuta el select del CODEUDOR

        return vPersona1;
    }

 
  

    ///// <summary>
    ///// Método para mostrar los conceptos de los aprobadores
    ///// </summary>
    ///// <param name="pIdObjeto"></param>
    //private void TablaAprobadores(String pIdObjeto)
    //{
    //    DatosAprobador datosApp = new DatosAprobador();
    //    datosApp.Radicacion = Int64.Parse(pIdObjeto);
    //    List<DatosAprobador> lstConsulta = new List<DatosAprobador>();
    //    lstConsulta = datosServicio.ListarDatosAprobador(datosApp, (Usuario)Session["usuario"]);

    //    gvAprobacion.DataSource = lstConsulta;
    //    if (lstConsulta.Count > 0)
    //    {
    //        gvAprobacion.Visible = true;
    //        TabPanel1.Visible = true;
    //        lblInfo.Visible = false;
    //        lblTotalRegs.Visible = true;
    //        lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
    //        gvAprobacion.DataBind();
    //        try { ValidarPermisosGrilla(gvAprobacion); }
    //        catch { }
    //    }
    //    else
    //    {
    //        gvAprobacion.Visible = false;
    //        TabPanel1.Visible=false;
    //        lblInfo.Visible = true;
    //        lblTotalRegs.Visible = true;
    //        lblTotalRegs.Text = "No se encontraron registros de aprobadores del crédito";
    //    }
    //}

  


    /// <summary>
    /// Método para llenar el DDL de los motivos de aplazamiento del crédito
    /// </summary>
    private void DropAplazar()
    {
        MotivoService motivoServicio = new MotivoService();
        Motivo motivo = new Motivo();
        ddlAplazar.DataSource = motivoServicio.ListarMotivosFiltro(motivo, (Usuario)Session["usuario"], 1);
        ddlAplazar.DataTextField = "Descripcion";
        ddlAplazar.DataValueField = "Codigo";
        ddlAplazar.DataBind();
        ddlAplazar.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    /// <summary>
    /// Método para mostrar los motivos de negación del crédito
    /// </summary>
    private void DropNegar()
    {
        MotivoService motivoServicio = new MotivoService();
        Motivo motivo = new Motivo();
        ddlNegar.DataSource = motivoServicio.ListarMotivosFiltro(motivo, (Usuario)Session["usuario"], 2);
        ddlNegar.DataTextField = "Descripcion";
        ddlNegar.DataValueField = "Codigo";
        ddlNegar.DataBind();
        ddlNegar.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public void MensajeFinal(string pmensaje)
    {
        mvAprobacion.ActiveViewIndex = 1;
        lblMensajeGrabar.Text = pmensaje;
    }


    /// <summary>
    /// Mètodo para cuando se aprueba el crèdito
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAcpAprobar_Click(object sender, EventArgs e)
    {
        string sError ="";
        VerError("");
        Validate("vgEdad");
        if (Page.IsValid)
        {
            try
            { 
                CreditoSolicitado credito = new CreditoSolicitado();
                CreditoService CreditoServicio = new CreditoService();
                credito.ObservacionesAprobacion = txtObsApr.Text;
                credito.NumeroCredito = Int64.Parse(txtCredito2.Text);
                Label lblUsuario = (Label)this.Master.FindControl("header1").FindControl("lblUser");
                credito.Nombres = lblUsuario.Text;
                credito.fecha = DateTime.Now;
                credito.lstCodeudores = new List<Persona1>();
                credito.lstCodeudores = (List<Persona1>)Session["Codeudores"];
                credito.lstCuoExt = new List<CuotasExtras>();
                credito.lstCuoExt = (List<CuotasExtras>)Session["CuoExt"];
            

                creditoServicio.AprobarCredito(credito, (Usuario)Session["usuario"], ref sError);
               
                if (sError.Trim() != "")
                {
                    VerError(sError);
                    return;
                }                

                MensajeFinal("Crédito " + txtCredito2.Text  + " Aprobado Correctamente");
                if (ddlProceso.Items.Count > 4)
                    ddlProceso.SelectedIndex = 4;
                consultarControlAprobados();
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(creditoServicio.GetType().Name + "L", "AprobarCredito", ex);
            }
           
        }
    }

    protected void btnCncAprobar_Click(object sender, EventArgs e)
    {
        txtObsApr.Text = "";
    }
    
    /// <summary>
    /// Método para aprobar crèditos modificando condiciones
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAcpAproModif_Click(object sender, EventArgs e)
    {
        VerError("");
        lblError.Text = "";       
         if (ValidarAprobacion())
        {
            long codigoCliente = Convert.ToInt64(txtCodigo.Text);
            string tipoCredito = ddlLineaCredito.SelectedValue;
            Usuario usuario = (Usuario)Session["usuario"];

            if (!ValidarNumCreditosPorLinea(tipoCredito, null, codigoCliente, usuario))
                return;
            
            string sError = "";
           
                CreditoService CreditoServicio = new CreditoService();
                CreditoSolicitado credito = new CreditoSolicitado();
                Periodicidad perio = new Periodicidad();

                try
                {
                    Label lblUsuario = (Label)this.Master.FindControl("header1").FindControl("lblUser");

                    credito.Nombres = lblUsuario.Text;
                    credito.ObservacionesAprobacion = txtObs3.Text;
                    credito.NumeroCredito = Int64.Parse(txtCredito3.Text);
                    credito.Monto = Decimal.Parse(txtMonto2.Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    credito.Plazo = txtPlazo2.Text;
                    credito.fecha = DateTime.Now;
                    credito.cod_Periodicidad = Int32.Parse(ddlPlazo.SelectedValue);    
                    credito.cod_linea_credito = ddlLineaCredito.SelectedValue;
                    credito.forma_pago = ddlFormaPago.SelectedValue;
                    if (ucFechaPrimerPago.TieneDatos)
                        credito.fecha_primer_pago = ucFechaPrimerPago.ToDateTime;

                  //  credito.tasa = txtTasa.Text;
                   // credito.tipotasa = ddlTipoTasa.SelectedValue;
                    credito.lstCodeudores = new List<Persona1>();
                    credito.lstCodeudores = (List<Persona1>)Session["Codeudores"];
                  
                    if (chekpago.Checked)
                        credito.condicion_especial = 1;
                    else
                        credito.condicion_especial = 0;
                  

                    // Guardar las empresas de recaudo
                    if (ddlFormaPago.SelectedIndex == 1)// FORMA DE PAGO POR NOMINA
                    {
                        List<CreditoEmpresaRecaudo> lstDetalle = new List<CreditoEmpresaRecaudo>();
                         lstDetalle = ObtenerListaEmpresaRecaudadora();
                        foreach (CreditoEmpresaRecaudo rEmpre in lstDetalle)
                        {
                            if (rEmpre.porcentaje != 0)
                            {
                                rEmpre.numero_radicacion = Convert.ToInt64(credito.NumeroCredito);
                                CreditoServicio.CrearModEmpresa_Recaudo(rEmpre,(Usuario)Session["usuario"], 1);
                            }
                        }
                    }
                 


                    int opcion = 0;
                    // Realizar la inserción para generar documentos     
                    List<Xpinn.FabricaCreditos.Entities.Documento> lstConsultaDoc;
                    Int64 numero_radicacion = Convert.ToInt64(txtCredito.Text);
                    cDocumentos.cod_linea_credito = ddlLineaCredito.SelectedValue;
                    lstConsultaDoc = DADocumentos.ListarDocumentoAGenerar(cDocumentos,(Usuario)Session["usuario"]);
                   // Session["Documentos"] = lstConsultaDoc;
                    if (lstConsultaDoc != null)
                    {
                        foreach (Xpinn.FabricaCreditos.Entities.Documento cDocumento in lstConsultaDoc)
                        {
                            cDocumento.numero_radicacion = numero_radicacion;
                            cDocumento.referencia = "";
                            cDocumento.ruta = "";
                            DADocumentos.CrearDocumentoGenerado(cDocumento, numero_radicacion, (Usuario)Session["usuario"]);
                        }
                    }


                    // Cargar atributos descontados/financiados a modificar del crédito
                    credito.lstDescuentosCredito = new List<DescuentosCredito>();
                    foreach (GridViewRow gFila in gvDeducciones.Rows)
                    {
                        try
                        {
                            DescuentosCredito eDescuento = new DescuentosCredito();
                            eDescuento.numero_radicacion = credito.NumeroCredito;
                            eDescuento.cod_atr = ValorSeleccionado((Label)gFila.FindControl("lblCodAtr"));
                            eDescuento.nom_atr = Convert.ToString(gFila.Cells[1].ToString());
                            eDescuento.tipo_descuento = ValorSeleccionado((DropDownList)gFila.FindControl("ddlTipoDescuento"));
                            eDescuento.tipo_liquidacion = ValorSeleccionado((DropDownList)gFila.FindControl("ddlTipoLiquidacion"));
                            eDescuento.forma_descuento = Convert.ToInt32(ValorSeleccionado((DropDownList)gFila.FindControl("ddlFormaDescuento")));
                            eDescuento.val_atr = ValorSeleccionado((TextBox)gFila.FindControl("txtvalor"));
                            eDescuento.numero_cuotas = ValorSeleccionado((TextBox)gFila.FindControl("txtnumerocuotas"));
                            eDescuento.cobra_mora = ValorSeleccionado((CheckBox)gFila.FindControl("cbCobraMora"));
                          //  eDescuento.tipo_impuesto = Convert.ToInt32((DropDownList)gFila.FindControl("ddlimpuestos"));
                            credito.lstDescuentosCredito.Add(eDescuento);
                        }
                        catch (Exception ex)
                        {
                            VerError("Se presentaron errores al cargar atributos descontados/financiados a modificar del crédito. Error:" + ex.Message);
                            return;
                        }
                    }

                   
                    // Realizar los cambios al crédito
                    creditoServicio.AprobarCreditoRotativo(credito, (Usuario)Session["usuario"], ref sError);
                   
                    if (sError.Trim() != "")
                    {
                        VerError(sError);
                        return;
                    }

                    // Despues de aprobado el credito se cambia el estado a C
                    Credito creditomod = new Credito();
                    creditomod.estado = "C";
                    creditomod.numero_radicacion = Convert.ToInt64(txtCredito.Text);
                    creditomod.numero_credito = creditomod.numero_radicacion;
                    creditomod.fecha_inicio = ucFechaPrimerPago.ToDateTime; ;
                    creditomod.fecha_desembolso = DateTime.Now;
               //     creditomod.saldo_capital = Decimal.Parse(txtMonto2.Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
               
                    CreditoServicio.ModificarCredito(creditomod, (Usuario)Session["usuario"]);
                    //DateTime fechadesembolso = Convert.ToDateTime(DateTime.Today.ToShortDateString());
                  
                    txtFecha.Text = DateTime.Today.ToShortDateString();
                    DateTime fechadesembolso = Convert.ToDateTime(txtFecha.Text);
                    CreditoServicio.ModificarFechaDesembolsoCredito(fechadesembolso,creditomod, (Usuario)Session["usuario"]);


                    // Guardar la tasa
                    if (chkTasa.Checked == true)
                    {
                        string tasa, tipotasa, desviacion, tipohisto;
                        if (PanelFija.Visible == true)
                        {

                            tasa = txtTasa.Text != "0" ? txtTasa.Text : "";
                            //Decimal.Parse(txtMonto2.Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                            tasa=Convert.ToString(txtTasa.Text.Trim().Replace(",","."));
                            tipotasa = ddlTipoTasa.SelectedValue != "" && ddlTipoTasa.SelectedItem != null ? ddlTipoTasa.SelectedValue : "";
                        }
                        else
                        {
                            tasa = ""; tipotasa = "";
                        }
                        if (PanelHistorico.Visible == true)
                        {
                            desviacion = txtDesviacion.Text != "0" ? txtDesviacion.Text : "";
                            tipohisto = ddlHistorico.SelectedValue != "" && ddlHistorico.SelectedItem != null ? ddlHistorico.SelectedValue : "";
                        }
                        else
                        {
                            desviacion = "";
                            tipohisto = "";
                        }

                   // CreditoServicio.cambiotasa(creditomod.numero_radicacion.ToString(), "", rbCalculoTasa.SelectedValue, tasa, tipotasa, desviacion, tipohisto, (Usuario)Session["usuario"], "2");                    
                   //es lo mismo indicado por Duvan 
                        CreditoServicio.cambiotasa(creditomod.numero_radicacion.ToString(), rbCalculoTasa.SelectedValue, tasa, tipotasa, desviacion, tipohisto, "1", (Usuario)Session["usuario"], "0");

                    }

                    // Finalizar el proceso 
                    MensajeFinal("Crédito " + txtCredito2.Text + " Aprobado Correctamente Con las Nuevas Condiciones");
                    if (ddlProceso.Items.Count > 4)
                        ddlProceso.SelectedIndex = 4;
                    consultarControlAprobados();

                    mvAprobacion.ActiveViewIndex = 1; 
                    
                }
                catch (Exception ex)
                {
                    VerError(ex.Message);
                }
        }

    }

    protected Int32? ValorSeleccionado(DropDownList ddlControl)
    {
        if (ddlControl != null)
            if (ddlControl.SelectedValue != null)
                if (ddlControl.SelectedValue != "")
                    return Convert.ToInt32(ddlControl.SelectedValue);
        return null;
    }

    protected decimal? ValorSeleccionado(TextBox txtControl)
    {
        if (txtControl != null)
            if (txtControl.Text != null)
                if (txtControl.Text != "")
                    return ConvertirStringToDecimal(txtControl.Text);
        return null;
    }

    protected Int32? ValorSeleccionado(Label txtControl)
    {
        if (txtControl != null)
            if (txtControl.Text != null)
                if (txtControl.Text != "")
                    return Convert.ToInt32(txtControl.Text);
        return null;
    }

    protected int ValorSeleccionado(CheckBox txtControl)
    {
        if (txtControl != null)
            if (txtControl.Checked != null)
                return Convert.ToInt32(txtControl.Checked);
        return 0;
    }



    protected void btnCncAproModif_Click(object sender, EventArgs e)
    {
        txtObs3.Text = "";
        txtMonto2.Text = "";
        txtPlazo2.Text = "";
        ddlPlazo.SelectedIndex = 0;
    }

    /// <summary>
    /// Método para aplazar el crédito
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAcpAplazar_Click(object sender, EventArgs e)
    {
        Validate("vgEdad");
        if (Page.IsValid)
        {
            CreditoSolicitado credito = new CreditoSolicitado();
            Motivo motivo = new Motivo();
            Label lblUsuario = (Label)this.Master.FindControl("header1").FindControl("lblUser");
            credito.Nombres = lblUsuario.Text;
            motivo.Codigo = Int16.Parse(ddlAplazar.SelectedValue);
            credito.ObservacionesAprobacion = txtObs4.Text;
            credito.NumeroCredito = Int64.Parse(txtCredito4.Text);

          
            creditoServicio.AplazarCredito(credito, motivo, (Usuario)Session["usuario"]);
            MensajeFinal("Crédito " + txtCredito2.Text + " Aplazado");
            if (ddlProceso.Items.Count > 16)
                ddlProceso.SelectedIndex = 16;
            consultarControlAplazados();
        }
    }

    protected void btnCncAplaz_Click(object sender, EventArgs e)
    {
        txtObs4.Text = "";
        ddlAplazar.SelectedIndex = 0;
 
    }

    /// <summary>
    /// Método para negar el crédito
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAcpNegar_Click(object sender, EventArgs e)
    {
        Validate("vgEdad");
        if (Page.IsValid)
        {
            CreditoSolicitado credito = new CreditoSolicitado();
            Motivo motivo = new Motivo();
            Label lblUsuario = (Label)this.Master.FindControl("header1").FindControl("lblUser");
            credito.Nombres = lblUsuario.Text;
            motivo.Codigo = Int16.Parse(ddlNegar.SelectedValue);
            credito.ObservacionesAprobacion = txtObs5.Text;
            credito.NumeroCredito = Int64.Parse(txtCredito5.Text);
           
            creditoServicio.NegarCredito(credito, motivo, (Usuario)Session["usuario"]);
            MensajeFinal("Crédito " + txtCredito2.Text + " Negado");
            if (ddlProceso.Items.Count > 15)
                ddlProceso.SelectedIndex = 15;

            ///Session["Negado"] = lstCodeudores;
            consultarControlNegados();
        }
    }

    protected void btnCncNegar_Click(object sender, EventArgs e)
    {
        txtObs5.Text = "";
        ddlNegar.SelectedIndex = 0;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    //  Métodos para control de la grilla de centrales de riesgo
    //
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
           // gvLista.PageIndex = e.NewPageIndex;
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(consultasdatacreditoServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        //String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        //Session[consultasdatacreditoServicio.CodigoPrograma + ".id"] = id;
        //Navegar(Pagina.Detalle);
    }
      



   

    ///// <summary>
    ///// Mètodo para controlar el cambio de pàgina en la grilla de los aprobadores
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void gvAprobacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    try
    //    {
    //        gvAprobacion.PageIndex = e.NewPageIndex;
    //        TablaAprobadores(txtCredito.Text);
    //    }
    //    catch (Exception ex)
    //    {
    //        BOexcepcion.Throw(creditoServicio.CodigoProgramaRotativoAprobar, "gvAprobacion_PageIndexChanging", ex);
    //    }
    
  

    /// <summary>
    /// Mètodo para botòn de continuar el cual lleva a la lista de consulta
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnContinuar_Click(object sender, EventArgs e)
    {

        Navegar(Pagina.Lista);
    }

  

    protected void registro_Click(object sender, EventArgs e)
    {
     
    }

   

    private void DropLineaCredito()
    {
        string ListaSolicitada = "";
        try
        {
            if (Session["ES_EMPLEADO"] == "true")
            {
                ListaSolicitada = "TipoCreditoEmple";                
            }
            else
            {
                if (Session["TipoCredito"].ToString() == "3")
                    ListaSolicitada = "TipoCreditoMicro";
                else if (Session["TipoCredito"].ToString() == "1")
                    ListaSolicitada = "TipoCreditoConsumo";
                else
                    ListaSolicitada = "TipoCreditoMicro";
            }
            Session.Remove("ES_EMPLEADO");
            //ddlLineaCredito.DataSource = TraerResultadosLista(ListaSolicitada); ;
            //ddlLineaCredito.DataTextField = "ListaDescripcion";
            //ddlLineaCredito.DataValueField = "ListaIdStr";
            //ddlLineaCredito.DataBind();
        }
        catch { }
    }

    private List<Xpinn.FabricaCreditos.Entities.Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["usuario"]);
        return lstDatosSolicitud;
    }

    protected void ddlLineaCredito_TextChanged(object sender, EventArgs e)
    {
        Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        LineasCredito eLinea = new LineasCredito();
        Int64 tipocredito = 2;
        eLinea = LineaCreditoServicio.ConsultarLineasCreditoRotativo(tipocredito, (Usuario)Session["usuario"]);        
    }

    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = ControlProcesosServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["usuario"]);
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


            Periodicidad perio = new Periodicidad();
            ddlPlazo.DataSource = periodicidadServicio.ListarPeriodicidad(perio, (Usuario)Session["usuario"]);
            ddlPlazo.DataTextField = "Descripcion";
            ddlPlazo.DataValueField = "Codigo";
            ddlPlazo.DataBind();
            ddlPlazo.Text = txtPeriodicidad.Text;

            List<TipoTasaHist> lstTipoTasaHist = new List<TipoTasaHist>();
            TipoTasaHistService TipoTasaHistServicios = new TipoTasaHistService();
            TipoTasaHist vTipoTasaHist = new TipoTasaHist();
            lstTipoTasaHist = TipoTasaHistServicios.ListarTipoTasaHist(vTipoTasaHist, (Usuario)Session["usuario"]);
            ddlHistorico.DataSource = lstTipoTasaHist;
            ddlHistorico.DataTextField = "descripcion";
            ddlHistorico.DataValueField = "tipo_historico";
            ddlHistorico.DataBind();


            // Llena el DDL de los tipos de tasas
            List<TipoTasa> lstTipoTasa = new List<TipoTasa>();
            TipoTasaService TipoTasaServicios = new TipoTasaService();
            TipoTasa vTipoTasa = new TipoTasa();
            lstTipoTasa = TipoTasaServicios.ListarTipoTasa(vTipoTasa, (Usuario)Session["usuario"]);
            ddlTipoTasa.DataSource = lstTipoTasa;
            ddlTipoTasa.DataTextField = "nombre";
            ddlTipoTasa.DataValueField = "cod_tipo_tasa";
            ddlTipoTasa.DataBind();

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReferenciaServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }
    private void GuardarControl()
    {
        Usuario pUsuario = (Usuario)Session["usuario"];


        Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();
        vControlCreditos.numero_radicacion = Convert.ToInt64(this.txtCredito.Text);
        vControlCreditos.codtipoproceso = ddlProceso.SelectedItem != null ? ddlProceso.SelectedValue : null;
        vControlCreditos.fechaproceso = Convert.ToDateTime(DateTime.Now);
        vControlCreditos.cod_persona = pUsuario.codusuario;
        vControlCreditos.cod_motivo = 0;
        if (txtObsApr.Text != "" || txtObs3.Text != "" || txtObs4.Text != "" || txtObs5.Text != "")
        {
            if (txtObsApr.Text != "")
            {
                vControlCreditos.observaciones = this.txtObsApr.Text.Length >= 250 ? txtObsApr.Text.Substring(0, 249) : txtObsApr.Text.Substring(0, txtObsApr.Text.Length).ToUpper();

            }

            if (txtObs3.Text != "")
            {
                vControlCreditos.observaciones = this.txtObs3.Text.Length >= 250 ? txtObs3.Text.Substring(0, 249) : txtObs3.Text.Substring(0, txtObs3.Text.Length).ToUpper();
            }
            if (txtObs4.Text != "")
            {
                vControlCreditos.observaciones = this.txtObs4.Text.Length >= 250 ? txtObs4.Text.Substring(0, 249) : txtObs4.Text.Substring(0, txtObs4.Text.Length).ToUpper();
            }
            if (txtObs5.Text != "")
            {
                vControlCreditos.observaciones = this.txtObs5.Text.Length >= 250 ? txtObs5.Text.Substring(0, 249) : txtObs5.Text.Substring(0, txtObs5.Text.Length).ToUpper();
            }

        }
        else
        {
            vControlCreditos.observaciones = "0";
        }
        
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
            FechaDatcaredito = Convert.ToString(Session["Datacredito"].ToString());
            vControlCreditos.fechaconsulta_dat = Convert.ToDateTime(FechaDatcaredito);
 
        }
        
        
        vControlCreditos = ControlCreditosServicio.CrearControlCreditos(vControlCreditos, (Usuario)Session["usuario"]);
    
    }
    private void consultarControlAprobados()
    {
        Usuario pUsuario = (Usuario)Session["usuario"];


        Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();

        String radicado = Convert.ToString(this.txtCredito.Text);
        vControlCreditos = ControlCreditosServicio.ConsultarControl_Procesos(5, radicado, (Usuario)Session["usuario"]);
        Int64 Controlexiste = 0;
        Controlexiste = Convert.ToInt64(vControlCreditos.codtipoproceso);
        if (Controlexiste <= 0)
        {
            GuardarControl();
        }


    }
    private void consultarControlNegados()
    {
        Usuario pUsuario = (Usuario)Session["usuario"];


        Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();

        String radicado = Convert.ToString(this.txtCredito.Text);
        vControlCreditos = ControlCreditosServicio.ConsultarControl_Procesos(16, radicado, (Usuario)Session["usuario"]);
        Int64 Controlexiste = 0;
        Controlexiste = Convert.ToInt64(vControlCreditos.codtipoproceso);
        if (Controlexiste <= 0)
        {
            GuardarControl();
        }


    }
    private void consultarControlAplazados()
    {
        Usuario pUsuario = (Usuario)Session["usuario"];


        Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();

        String radicado = Convert.ToString(this.txtCredito.Text);
        vControlCreditos = ControlCreditosServicio.ConsultarControl_Procesos(17, radicado, (Usuario)Session["usuario"]);
        Int64 Controlexiste = 0;
        Controlexiste = Convert.ToInt64(vControlCreditos.codtipoproceso);
        if (Controlexiste <= 0)
        {
            GuardarControl();
        }


    }

    protected void gvListaModalidadTasas_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ModalidadTasa modalidad = new ModalidadTasa();
                modalidad = (ModalidadTasa)e.Row.DataItem;
                CheckBox chkModalidadTasa = new CheckBox();
                chkModalidadTasa = (CheckBox)e.Row.Cells[3].FindControl("chkSeleccionarTasa");


                if (chkModalidadTasa.Checked == true)
                {
                   // chkModalidadTasa.Checked = true;
                    //chkTasa.Enabled = false;
                    
                }
                else 
                {
                    //chkTasa.Enabled = true;
                }

            }
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ModalidadTasaServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }



    protected void rbTipoAprobacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        panelAprobacion.Visible = false;
        panelAprobacionCom.Visible = false;
        panelAplazamiento.Visible = false;
        panelNegar.Visible = false;
        if (rbTipoAprobacion.SelectedItem.Value == "1")
        {
            Usuario usuap = (Usuario)Session["usuario"];
            if (usuap.tipo == 2)
            {
                //lbledad.Visible = false;
            //    txtEdad.Visible = false;
                mvAprobacion.ActiveViewIndex = 0;
                Lblaprob.Text = "APROBACIÓN DE CRÉDITOS";
                panelAprobacion.Visible = false;
            }
            if (tipoempresa == 1)
            {
               // lbledad.Visible = true;
                mvAprobacion.ActiveViewIndex = 0;
                Lblaprob.Text = "APROBACIÓN DE CRÉDITOS MODIFICANDO CONDICIONES";
            }
            panelAprobacionCom.Visible = true;
        }
        else if (rbTipoAprobacion.SelectedItem.Value == "2")
        {
            panelAplazamiento.Visible = true;
        }
        else if (rbTipoAprobacion.SelectedItem.Value == "3")
        {
            panelNegar.Visible = true;
        }
    }

    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        panelEmpresaRec.Visible = false;
        if (ddlFormaPago.SelectedIndex != 0)
        {
            List<CreditoEmpresaRecaudo> lstConsulta = new List<CreditoEmpresaRecaudo>();
            lstConsulta = consultasdatacreditoServicio.ListarPersona_Empresa_Recaudo(Convert.ToInt64(txtCodigo.Text), (Usuario)Session["usuario"]);
            if (lstConsulta.Count > 0)
            {
                panelEmpresaRec.Visible = true;
                gvEmpresaRecaudora.DataSource = lstConsulta;
                gvEmpresaRecaudora.DataBind();
            }
        }
    }
    protected void txtPorcentaje_OnTextChanged(object sender, EventArgs e)
    {
        decimal numero = 0;
        TextBoxGrid txtporcentaje = (TextBoxGrid)sender;
        if (txtporcentaje != null)
        {
            if (txtporcentaje.Text != null && txtporcentaje.Text != "")
            {
                numero = Convert.ToDecimal(txtporcentaje.Text);
                decimal numerodividio = numero;
                numero = numerodividio / 100;
            }
            if (txtCuota.Text != null && txtCuota.Text != "")
            {
                numero = Math.Round(numero * Convert.ToInt64(txtCuota.Text.Replace(".", "")));
            }

            int rowIndex = Convert.ToInt32(txtporcentaje.CommandArgument);
            TextBox txtValor = (TextBox)gvEmpresaRecaudora.Rows[rowIndex].FindControl("txtValor");
            txtValor.Text = "";
            if (numero != 0)
            {
                try
                {
                    if (txtValor != null)
                    {
                        txtValor.Text = numero.ToString("##,##");
                    }
                }
                catch { }
            }
        }
    }

    protected List<CreditoEmpresaRecaudo> ObtenerListaEmpresaRecaudadora()
    {
        List<CreditoEmpresaRecaudo> lstLista = new List<CreditoEmpresaRecaudo>();
        int cantRow = 0;
        decimal vrtotal = 0, vrReemplazo = 0;
        if (gvEmpresaRecaudora.Rows.Count > 0)
            cantRow = gvEmpresaRecaudora.Rows.Count - 1;
        foreach (GridViewRow rFila in gvEmpresaRecaudora.Rows)
        {
            CreditoEmpresaRecaudo vData = new CreditoEmpresaRecaudo();

            Label lblCodigo = (Label)rFila.FindControl("lblCodigo"); //CODIGO DE LA EMPRESA
            if (lblCodigo != null && lblCodigo.Text != "")
                vData.cod_empresa = Convert.ToInt32(lblCodigo.Text);

            Label lblNombre = (Label)rFila.FindControl("lblNombre");
            if (lblNombre != null && lblNombre.Text != "")
                vData.nom_empresa = lblNombre.Text;

            TextBoxGrid txtPorcentaje = (TextBoxGrid)rFila.FindControl("txtPorcentaje");
            if (txtPorcentaje.Text != "")
                vData.porcentaje = Convert.ToDecimal(txtPorcentaje.Text);

            vData.valor = 0;
            TextBox txtValor = (TextBox)rFila.FindControl("txtValor");
            if (txtValor.Text != "")
                vData.valor = Convert.ToDecimal(txtValor.Text.Replace(".", ""));

            vrtotal += vData.valor;
            if (cantRow == rFila.RowIndex)
            {
                //---validando que cuadre el valor dividido con el monto de cuota.
                if (txtCuota.Text != "" && txtCuota != null)
                {
                    int cuota = Convert.ToInt32(txtCuota.Text.Replace(".", ""));
                    if (vrtotal > cuota)
                    {
                        vrReemplazo = vrtotal - cuota;
                        vData.valor = vData.valor - vrReemplazo;
                    }
                    else if (vrtotal < cuota)
                    {
                        vrReemplazo = cuota - vrtotal;
                        vData.valor = vData.valor + vrReemplazo;
                    }
                }
            }

            if (vData.porcentaje != 0 && vData.porcentaje != null)
                lstLista.Add(vData);
        }

        return lstLista;
    }

    Boolean ValidarAprobacion()
    {
      
        lblError.Visible = false;
        lblError.Text = "";
        if (ddlFormaPago.SelectedIndex == 1 && gvEmpresaRecaudora.Rows.Count > 0)// si la forma de pago es por NOMINA
        {
            List<CreditoEmpresaRecaudo> lstDatos = new List<CreditoEmpresaRecaudo>();
            lstDatos = ObtenerListaEmpresaRecaudadora();

            decimal porcentaje = 0;
            int cont = 0;
            foreach (CreditoEmpresaRecaudo rCred in lstDatos)
            {
                if (lstDatos[cont].porcentaje != 0)
                    porcentaje += lstDatos[cont].porcentaje;
                cont++;
            }
            if (porcentaje > 100)
            {
                lblError.Visible = true;
                lblError.Text = "El valor total del Porcentaje no puede ser mayor al 100%";
                return false;
            }
            else if (porcentaje < 100)
            {
                lblError.Visible = true;
                lblError.Text = "El valor total del porcentaje debe ser el 100%";
                return false;
            }
            else if (porcentaje == 0)
            {
                lblError.Visible = true;
                lblError.Text = "Debe Ingresar el valor de porcentaje";
                return false;
            }
        }


        return true;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    //  Métodos para control de la grilla de codeudores
    //
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    protected void gvListaCodeudores_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        long ID = Convert.ToInt32(gvListaCodeudores.DataKeys[e.RowIndex].Values[0].ToString());
        if (ID != 0)
        {
            //ReporteServicio.EliminarParametro(Convert.ToInt64(txtCodigo.Text), ID, (Usuario)Session["usuario"]);
            TablaCodeudores();
        }
    }
    protected void gvListaCodeudores_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtidentificacion = (TextBox)gvListaCodeudores.FooterRow.FindControl("txtidentificacion");
            if (txtidentificacion.Text.Trim() == "")
            {
                VerError("Ingrese la Identificación del Codeudor por favor.");
                return;
            }
            Xpinn.Comun.Data.GeneralData DAGeneral = new Xpinn.Comun.Data.GeneralData();
            Xpinn.Comun.Entities.General pEntidad = new Xpinn.Comun.Entities.General();
            pEntidad = DAGeneral.ConsultarGeneral(480, (Usuario)Session["usuario"]);
            try
            {
                if (pEntidad.valor != null)
                {
                    if (Convert.ToInt32(pEntidad.valor) > 0)
                    {
                        int paramCantidad = 0, cantReg = 0;
                        paramCantidad = Convert.ToInt32(pEntidad.valor);
                        Xpinn.FabricaCreditos.Entities.codeudores pCodeu = new Xpinn.FabricaCreditos.Entities.codeudores();
                        pCodeu = CodeudorServicio.ConsultarCantidadCodeudores(txtidentificacion.Text, (Usuario)Session["usuario"]);
                        if (pCodeu.cantidad != null)
                        {
                            cantReg = Convert.ToInt32(pCodeu.cantidad);
                            if (cantReg >= paramCantidad)
                            {
                                VerError("No puede adicionar esta persona debido a que ya mantiene el límite de veces como codeudor.");
                                return;
                            }
                        }
                    }
                }
            }
            catch { }

            List<Persona1> lstCodeudor = new List<Persona1>();
            lstCodeudor = (List<Persona1>)Session["Codeudores"];

            if (lstCodeudor.Count == 1)
            {
                Persona1 gItem = new Persona1();
                gItem = lstCodeudor[0];
                if (gItem.cod_persona == 0)
                    lstCodeudor.Remove(gItem);
            }

            Xpinn.FabricaCreditos.Services.codeudoresService codeudorServicio = new Xpinn.FabricaCreditos.Services.codeudoresService();
            Xpinn.FabricaCreditos.Entities.codeudores vcodeudor = new Xpinn.FabricaCreditos.Entities.codeudores();
            vcodeudor = codeudorServicio.ConsultarDatosCodeudor(txtidentificacion.Text, (Usuario)Session["usuario"]);
            Persona1 gItemNew = new Persona1();
            gItemNew.cod_persona = vcodeudor.codpersona;
            gItemNew.identificacion = vcodeudor.identificacion;
            gItemNew.primer_nombre = vcodeudor.primer_nombre;
            gItemNew.segundo_nombre = vcodeudor.segundo_nombre;
            gItemNew.primer_apellido = vcodeudor.primer_apellido;
            gItemNew.segundo_apellido = vcodeudor.segundo_apellido;
            gItemNew.direccion = vcodeudor.direccion;
            gItemNew.telefono = vcodeudor.telefono;
            gItemNew.idcodeudor = vcodeudor.idcodeud;
            lstCodeudor.Add(gItemNew);
            gvListaCodeudores.DataSource = lstCodeudor;
            gvListaCodeudores.DataBind();
            Session["Codeudores"] = lstCodeudor;
        }
        if (e.CommandName.Equals("Delete"))
        {
            List<Persona1> lstCodeudores = new List<Persona1>();
            lstCodeudores = (List<Persona1>)Session["Codeudores"];
            if (lstCodeudores.Count >= 1)
            {
                Persona1 eCodeudor = new Persona1();
                int index = Convert.ToInt32(e.CommandArgument);
                eCodeudor = lstCodeudores[index];
                if (eCodeudor.cod_persona != 0)
                {
                    if (eCodeudor.idcodeudor != null)
                    {

                        lstCodeudores.Remove(eCodeudor);
                        CodeudorServicio.EliminarcodeudoresCred(eCodeudor.idcodeudor, Convert.ToInt64(txtCredito.Text), (Usuario)Session["usuario"]);
                    }
                }
                Session["Codeudores"] = lstCodeudores;
            }
            if (lstCodeudores.Count == 0)
            {
                InicialCodeudores();
            }
            else
            {
                gvListaCodeudores.DataSource = lstCodeudores;
                gvListaCodeudores.DataBind();
                Session["Codeudores"] = lstCodeudores;
            }
        }
    }

    protected void gvListaCodeudores_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvListaCodeudores.PageIndex = e.NewPageIndex;
            TablaCodeudores();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoProgramaCodeudor, "gvLista_PageIndexChanging", ex);
        }
    }


    /// <summary>
    ///  Método para insertar un registro en la grilla cuando no hay codeudores
    /// </summary>
    protected void InicialCodeudores()
    {
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        Xpinn.FabricaCreditos.Entities.Persona1 eCodeudor = new Xpinn.FabricaCreditos.Entities.Persona1();
        lstConsulta.Add(eCodeudor);
        Session["Codeudores"] = lstConsulta;
        gvListaCodeudores.DataSource = lstConsulta;
        gvListaCodeudores.DataBind();
        gvListaCodeudores.Visible = true;
    }

    /// <summary>
    /// Método para control al digitar la identificación del codeudor
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtidentificacion_TextChanged(object sender, EventArgs e)
    {
        Control ctrl = gvListaCodeudores.FooterRow.FindControl("txtidentificacion");
        if (ctrl != null)
        {
            TextBox txtidentificacion = (TextBox)ctrl;
            Xpinn.FabricaCreditos.Services.codeudoresService codeudorServicio = new Xpinn.FabricaCreditos.Services.codeudoresService();
            Xpinn.FabricaCreditos.Entities.codeudores vcodeudor = new Xpinn.FabricaCreditos.Entities.codeudores();
            vcodeudor = codeudorServicio.ConsultarDatosCodeudor(txtidentificacion.Text, (Usuario)Session["usuario"]);
            gvListaCodeudores.FooterRow.Cells[2].Text = vcodeudor.codpersona.ToString();
            gvListaCodeudores.FooterRow.Cells[4].Text = vcodeudor.primer_nombre;
            gvListaCodeudores.FooterRow.Cells[5].Text = vcodeudor.segundo_nombre;
            gvListaCodeudores.FooterRow.Cells[6].Text = vcodeudor.primer_apellido;
            gvListaCodeudores.FooterRow.Cells[7].Text = vcodeudor.segundo_apellido;
            gvListaCodeudores.FooterRow.Cells[8].Text = vcodeudor.direccion;
            gvListaCodeudores.FooterRow.Cells[9].Text = vcodeudor.telefono;
        }
    }

    /// <summary>
    /// Método para consultar los codeudores del crédito
    /// </summary>
    private void TablaCodeudores()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
            lstConsulta = Persona1Servicio.ListarPersona1(ObtenerValoresCodeudores(), (Usuario)Session["usuario"]);

            gvListaCodeudores.PageSize = 5;
            gvListaCodeudores.EmptyDataText = "No se encontraron registros";
            gvListaCodeudores.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvListaCodeudores.Visible = true;
                lblTotalRegsCodeudores.Visible = false;
                lblTotalRegsCodeudores.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvListaCodeudores.DataBind();
                ValidarPermisosGrilla(gvListaCodeudores);
                Session["Codeudores"] = lstConsulta;

            }
            else
            {
                idObjeto = "";
                gvListaCodeudores.Visible = false;
                lblTotalRegsCodeudores.Text = "No hay codeudores para este crédito";
                lblTotalRegsCodeudores.Visible = true;
                InicialCodeudores();
            }

            Session.Add(Persona1Servicio.CodigoProgramaCodeudor + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoProgramaCodeudor, "Actualizar", ex);
        }

    }

    protected void chkTasa_CheckedChanged(object sender, EventArgs e)
    {
        if (chkTasa.Checked)
        {
            rbCalculoTasa.Visible = true;
            rbCalculoTasa.SelectedIndex = 0;
            rbCalculoTasa_SelectedIndexChanged(rbCalculoTasa, null);
        }
        else
        {
            rbCalculoTasa.Visible = false;
            PanelFija.Visible = false;
            PanelHistorico.Visible = false;
        }
    }
    protected void rbCalculoTasa_SelectedIndexChanged(object sender, EventArgs e)
    {
        PanelFija.Visible = false;
        PanelHistorico.Visible = false;
        if (rbCalculoTasa.SelectedIndex == 0)
            PanelFija.Visible = true;
        if (rbCalculoTasa.SelectedIndex == 1)
            PanelHistorico.Visible = true;
        if (rbCalculoTasa.SelectedIndex == 2)
            PanelHistorico.Visible = true;
    }

}

