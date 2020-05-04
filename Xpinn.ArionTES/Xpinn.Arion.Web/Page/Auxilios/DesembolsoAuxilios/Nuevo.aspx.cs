using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.Auxilios.Services;
using Xpinn.Auxilios.Entities;
using Xpinn.FabricaCreditos.Entities;

public partial class Nuevo : GlobalWeb
{

    DesembolsoAuxilioServices DesemServicios = new DesembolsoAuxilioServices();
    SolicitudAuxilioServices SolicAuxilios = new SolicitudAuxilioServices();
    Xpinn.Auxilios.Services.LineaAuxilioServices LineaAux = new Xpinn.Auxilios.Services.LineaAuxilioServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(DesemServicios.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DesemServicios.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                Session["Distribucion"] = null;
                Session["NumCred_Orden"] = null;
                Session["Beneficiario"] = null;
                CargarDropdown();
               
                mvAplicar.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;
                txtCupos.Enabled = false;
                txtMontoDisp.Enabled = false;
                txtFechaDesembolso.Text = DateTime.Now.ToString();
                if (Session[DesemServicios.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[DesemServicios.CodigoPrograma + ".id"].ToString();
                    Session.Remove(DesemServicios.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    Session["TEXTO"] = "modificar";
                }
                else
                {
                    Session["TEXTO"] = "grabar";
                    txtFecha.Text = DateTime.Now.ToShortDateString();
                    txtCodigo.Text = SolicAuxilios.ObtenerSiguienteCodigo((Usuario)Session["usuario"]).ToString();
                }
                InicializarDistribucion();
                chkDistribuir_CheckedChanged(chkDistribuir, null);
              //  ddlForma_Desem_SelectedIndexChanged(ddlForma_Desem, null);
            }
            else
                CalculaTotalXColumna();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DesemServicios.GetType().Name + "L", "Page_Load", ex);
        }
    }


    void CargarDropdown()
    {
        PoblarLista("lineasauxilios", ddlLinea);
        ctlGiro.Inicializar();


      //  ddlTipo_cuenta.Items.Insert(0, new ListItem("Ahorros", "0"));
        //ddlTipo_cuenta.Items.Insert(1, new ListItem("Corriente", "1"));
        //ddlTipo_cuenta.SelectedIndex = 0;
        //ddlTipo_cuenta.DataBind();

       // ddlForma_Desem.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        //ddlForma_Desem.Items.Insert(1, new ListItem("Efectivo", "1"));
        //ddlForma_Desem.Items.Insert(2, new ListItem("Cheque", "2"));
        //ddlForma_Desem.Items.Insert(3, new ListItem("Transferencia", "3"));
        //ddlForma_Desem.SelectedIndex = 0;
        //ddlForma_Desem.DataBind();

        //Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        //Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
        //ddlEntidad_giro.DataSource = bancoService.ListarBancosEntidad(banco, (Usuario)Session["usuario"]);
        //ddlEntidad_giro.DataTextField = "nombrebanco";
        //ddlEntidad_giro.DataValueField = "cod_banco";
        //ddlEntidad_giro.DataBind();
        //CargarCuentas();

        //ddlEntidad.DataSource = bancoService.ListarBancos(banco, (Usuario)Session["usuario"]);
        //ddlEntidad.DataTextField = "nombrebanco";
        //ddlEntidad.DataValueField = "cod_banco";
        //ddlEntidad.DataBind();
    }

    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodPersona", "txtIdPersona", "txtNomPersona");
    }


    
    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            SolicitudAuxilio vDetalle = new SolicitudAuxilio();

            vDetalle = DesemServicios.ConsultarAuxilioAprobado(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);

            if (vDetalle.oficina != null)
                txtoficina.Text = vDetalle.oficina;
            if (vDetalle.numero_auxilio != 0)
                txtCodigo.Text = vDetalle.numero_auxilio.ToString().Trim();
            if (vDetalle.fecha_solicitud != DateTime.MinValue)
                txtFecha.Text = vDetalle.fecha_solicitud.ToString(gFormatoFecha).Trim();
            if (vDetalle.cod_persona != 0)
            {
                txtCodPersona.Text = vDetalle.cod_persona.ToString().Trim();
                txtIdPersona.Text = vDetalle.identificacion.ToString().Trim();
                txtNomPersona.Text = vDetalle.nombre.ToString().Trim();
            }



            if (txtCodPersona.Text != null)
            {
                ctlGiro.cargarCuentasAhorro(Convert.ToInt64(txtCodPersona.Text));
            }


            if (vDetalle.cod_linea_auxilio != "")
                ddlLinea.SelectedValue = vDetalle.cod_linea_auxilio;
            ddlLinea_SelectedIndexChanged(ddlLinea, null);

            panelProveedor.Visible = false;
          
            if (ddlLinea.SelectedIndex != 0)
            {
                LineaAuxilio vDatosLinea = new LineaAuxilio();
                vDatosLinea = LineaAux.ConsultarLineaAUXILIO(ddlLinea.SelectedValue, (Usuario)Session["usuario"]);
                if (vDatosLinea.orden_servicio == 1)
                {
                    panelProveedor.Visible = true;
                   
                }
                if (panelProveedor.Visible == true)
                {
                    Auxilio_Orden_Servicio pEntidad = new Auxilio_Orden_Servicio();
                    if (txtCodigo.Text != "")
                    {
                        String pFiltro = "WHERE NUMERO_AUXILIO = " + txtCodigo.Text;
                        pEntidad = SolicAuxilios.ConsultarAUX_OrdenServicio(pFiltro, (Usuario)Session["usuario"]);
                        if (pEntidad.idproveedor != null && pEntidad.idordenservicio != 0)
                        {
                            txtIdentificacionprov.Text = pEntidad.idproveedor;
                            if (pEntidad.nomproveedor != null)
                                txtNombreProveedor.Text = pEntidad.nomproveedor;

                            Int64 pConsecutivo = 0;
                            pConsecutivo = SolicAuxilios.ObtenerNumeroPreImpreso((Usuario)Session["usuario"]);
                            txtPreImpreso.Text = pConsecutivo.ToString();
                        }
                        else
                        {
                            panelProveedor.Visible = false;
                           
                        }
                    }
                }
            }

            if (vDetalle.valor_solicitado != 0)
                txtValorSoli.Text = vDetalle.valor_solicitado.ToString();
            if (vDetalle.detalle != "")
                txtDetalle.Text = vDetalle.detalle;
           //Agregado
            if (vDetalle.fecha_aprobacion != DateTime.MinValue)
                txtFechaAprobacion.Text = vDetalle.fecha_aprobacion.ToShortDateString();

            if (vDetalle.valor_aprobado != 0)
                txtValorAproba.Text = vDetalle.valor_aprobado.ToString();
            if (vDetalle.observacion != "")
                txtObservacionAproba.Text = vDetalle.observacion;




            //agregado
           
           // txtNum_cuenta.Text = codigo.numero_cuenta;
           // ddlEntidad.SelectedValue = Convert.ToString(codigo.cod_banco);
           // ddlTipo_cuenta.SelectedValue = Convert.ToString(codigo.tipo_cuenta);
            


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DesemServicios.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public Boolean ValidarDatos()
    {
        bool pGenerarGiro = false;
        DesembolsoAuxilio pVar = new DesembolsoAuxilio();
        if (txtFechaDesembolso.Text == "")
        {
            VerError("Ingrese la fecha de Desembolso");
            return false;
        }
        if (Convert.ToDateTime(txtFechaDesembolso.Text) > DateTime.Now)
        {
            VerError("La fecha de Desembolso no puede ser superior a la fecha actual");
            return false;
        }
        /*if (panelGrilla.Visible == true)
        {
            if (ddlForma_Desem.SelectedIndex == 0)
            {
                VerError("Seleccione la forma de pago");
                return false;
            }
            if (ddlForma_Desem.SelectedItem.Text == "Transferencia")
            {
                if (txtNum_cuenta.Text == "")
                {
                    VerError("Ingrese el numero de cuenta");
                    return false;
                }
            }
        }
        */

        //Validando datos del control de Giro
        if (ctlGiro.IndiceFormaDesem == 0)
        {
            VerError("Seleccione una forma de desembolso");
            return false;
        }
        else
        {
            if (ctlGiro.IndiceFormaDesem == 2 || ctlGiro.IndiceFormaDesem == 3)
            {
                if (ctlGiro.IndiceEntidadOrigen == 0)
                {
                    VerError("Seleccione un Banco de donde se girará");
                    return false;
                }
                if (ctlGiro.IndiceFormaDesem == 3)
                {
                    if (ctlGiro.IndiceEntidadDest == 0)
                    {
                        VerError("Seleccione la Entidad de destino");
                        return false;
                    }
                    if (ctlGiro.TextNumCuenta == "")
                    {
                        VerError("Ingrese el número de la cuenta");
                        return false;
                    }
                }
              }
               
            
        }

        if (ctlGiro.IndiceFormaDesem == 4)
        {
            pGenerarGiro = false;

            pVar.numero_cuenta_ahorro_vista = !string.IsNullOrWhiteSpace(ctlGiro.ValueCuentaAhorro) ? Convert.ToInt64(ctlGiro.ValueCuentaAhorro) : default(long?);

            if (!pVar.numero_cuenta_ahorro_vista.HasValue)
            {
                VerError("No tiene una cuenta de ahorro a la vista asociada para esta forma de pago!.");
                return false;
            }

            if (ctlGiro.ValueCuentaAhorro == null)

            {
                VerError("No tiene una cuenta de ahorro a la vista asociada para esta forma de pago!.");
                return false;
            }
        }


        if (chkDistribuir.Checked)
        {
            List<Auxilios_Giros> lstGiros = new List<Auxilios_Giros>();
            lstGiros = ObtenerListaDistribucion();
            if (lstGiros.Count == 0)
            {
                VerError("Debe Ingresar algún beneficiario en la distribución de Giros.");
                return false;
            }
            else
            {
                CalculaTotalXColumna();
            }
        }

        // Validar que exista la parametrización contable por procesos
        if (ValidarProcesoContable(Convert.ToDateTime(txtFechaDesembolso.Text), 111) == false)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación 111 ");
            return false;
        }
        return true;
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            ctlMensaje.MostrarMensaje("Desea generar el Desembolso?");          
        }
    }

    protected void btnCancelarProceso_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarCancelar(true);
        panelGeneral.Visible = true;
        panelProceso.Visible = false;
    }

    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            panelGeneral.Visible = true;
            panelProceso.Visible = false;
            AplicarDatos();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        VerError("");
        Int64? rpta = 0;
        if (!panelProceso.Visible && panelGeneral.Visible)
        {
            rpta = ctlproceso.Inicializar(111, Convert.ToDateTime(txtFecha.Text), (Usuario)Session["Usuario"]);
            if (rpta > 1)
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarCancelar(false);
                panelGeneral.Visible = false;
                panelProceso.Visible = true;
            }
            else
            {
                AplicarDatos();
                Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
            }
        }
    }

    private bool AplicarDatos()
    {
        string separadorDecimal = System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
        Configuracion conf = new Configuracion();
        bool pGenerarGiro = false;
        Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
        Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
        try
        {
            //MODIFICAR TABLA AUXILIO A DESEMBOLSADO
            AprobacionAuxilio pVar = new AprobacionAuxilio();

            //----------------------------------
            pVar.numero_auxilios = Convert.ToInt64(txtCodigo.Text);
            pVar.fecha_desembolso = Convert.ToDateTime(txtFechaDesembolso.Text);

            Usuario usuap = (Usuario)Session["usuario"];
            if (idObjeto != "")
            {
                //MODIFICAR ESTADO DEL AUXILIO
                DesemServicios.DesembolsarAuxilios(pVar, (Usuario)Session["usuario"]);

                LineaAuxilio vDatosLinea = new LineaAuxilio();
                vDatosLinea = LineaAux.ConsultarLineaAUXILIO(ddlLinea.SelectedValue, (Usuario)Session["usuario"]);
                if (vDatosLinea.orden_servicio == 1 && panelProveedor.Visible == true)
                {
                    //VALIDAR SI ESTA ACTIVO LA OPCION ORDEN SERVICIO PARA LA LINEA.
                    Session["NumCred_Orden"] = txtCodigo.Text;

                    if (txtPreImpreso.Text == "")
                    {
                        VerError("Debe ingresar el número de Pre Impresión");
                        txtPreImpreso.Enabled = true;
                        txtPreImpreso.Focus();
                        return false;
                    }
                    Auxilio_Orden_Servicio pAux = new Auxilio_Orden_Servicio();
                    pAux.numero_auxilio = Convert.ToInt32(pVar.numero_auxilios);
                    pAux.numero_preimpreso = Convert.ToInt64(txtPreImpreso.Text);

                    Auxilio_Orden_Servicio pEntidad = new Auxilio_Orden_Servicio();
                    if (txtCodigo.Text != "")
                    {
                        String pFiltro = "WHERE NUMERO_PREIMPRESO = " + txtPreImpreso.Text;
                        pEntidad = SolicAuxilios.ConsultarAUX_OrdenServicio(pFiltro, (Usuario)Session["usuario"]);
                        if (pEntidad.idordenservicio > 0)
                        {
                            VerError("El número de Pre Impresión ya existe. Ingrese otro consecutivo");
                            txtPreImpreso.Focus();
                            return false;
                        }
                        else
                        {
                            pFiltro = "WHERE NUMERO_AUXILIO = " + txtCodigo.Text;
                            pEntidad = null;
                            pEntidad = SolicAuxilios.ConsultarAUX_OrdenServicio(pFiltro, (Usuario)Session["usuario"]);

                            if (pEntidad.idordenservicio != 0)
                            {
                                string pError = "";
                                //ACTUALIZAR LA TABLA AUXILIO_ORDEN_SERVICIO
                                SolicAuxilios.ModificarAuxilio_OrdenServ(pAux, ref pError, (Usuario)Session["usuario"]);
                                if (pError != "")
                                {
                                    VerError(pError.ToString());
                                    return false;
                                }
                            }
                        }
                    }
                }

                //GRABAR OPERACIÓN
                Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
                pOperacion.cod_ope = 0;
                pOperacion.tipo_ope = 111;
                pOperacion.cod_usu = usuap.codusuario;
                pOperacion.cod_ofi = usuap.cod_oficina;
                pOperacion.fecha_oper = DateTime.Parse(txtFechaDesembolso.Text);
                pOperacion.fecha_calc = DateTime.Now;
                pOperacion.num_lista = 0;

                //GRABAR GIRO               
                Xpinn.FabricaCreditos.Entities.Giro pGiro = new Xpinn.FabricaCreditos.Entities.Giro();
                 bool pOpcion = false;
                /* if (panelGrilla.Visible == true)
                {
                    pOpcion = true;

                    pGiro.idgiro = 0;
                    pGiro.cod_persona = Convert.ToInt64(txtCodPersona.Text);
                    pGiro.forma_pago = Convert.ToInt32(ddlForma_Desem.SelectedValue);
                    pGiro.tipo_acto = 6;
                    pGiro.fec_reg = DateTime.Now;
                    pGiro.fec_giro = DateTime.MinValue;
                    pGiro.numero_radicacion = Convert.ToInt64(txtCodigo.Text);// NO ENVIO EL NUMERO DE RADICACION SINO EL NUMERO DE DEVOLUCION
                    pGiro.usu_gen = usuap.nombre;
                    pGiro.usu_apli = null;
                    pGiro.estadogi = 0;
                    pGiro.usu_apro = null;
                    if (ddlForma_Desem.SelectedItem.Text == "Transferencia" || ddlForma_Desem.SelectedItem.Text == "Cheque")
                        CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ddlEntidad_giro.SelectedValue), ddlCuenta_Giro.SelectedItem.Text, (Usuario)Session["usuario"]);
                    Int64 idCta = CuentaBanc.idctabancaria;
                    //DATOS DE FORMA DE PAGO
                    if (ddlForma_Desem.SelectedItem.Text == "Transferencia")
                    {
                        pGiro.idctabancaria = idCta;
                        pGiro.cod_banco = Convert.ToInt32(ddlEntidad.SelectedValue);
                        pGiro.num_cuenta = txtNum_cuenta.Text;
                        pGiro.tipo_cuenta = Convert.ToInt32(ddlTipo_cuenta.SelectedValue);
                    }
                    else if (ddlForma_Desem.SelectedItem.Text == "Cheque")
                    {
                        pGiro.idctabancaria = idCta;
                        pGiro.cod_banco = 0; //NULO
                        pGiro.num_cuenta = null; //NULO
                        pGiro.tipo_cuenta = -1; //NULO
                    }
                    else
                    {
                        pGiro.idctabancaria = 0;
                        pGiro.cod_banco = 0;
                        pGiro.num_cuenta = null;
                        pGiro.tipo_cuenta = -1;
                    }
                    pGiro.fec_apro = DateTime.MinValue;
                    pGiro.cob_comision = 0;
                    pGiro.valor = Convert.ToInt64(txtValorAproba.Text.Replace(".", ""));
                }

    */


                //GRABACION DEL GIRO A REALIZAR
                Xpinn.FabricaCreditos.Services.AvanceService AvancServices = new Xpinn.FabricaCreditos.Services.AvanceService();
                Usuario pusu = (Usuario)Session["usuario"];
                Xpinn.FabricaCreditos.Entities.Giro pGiros = new Xpinn.FabricaCreditos.Entities.Giro();
                pGiro.idgiro = 0;
                pGiro.cod_persona = Convert.ToInt64(txtCodPersona.Text);
                pGiro.forma_pago = Convert.ToInt32(ctlGiro.ValueFormaDesem);
                pGiro.tipo_acto = 6; 
                pGiro.fec_reg = Convert.ToDateTime(txtFechaDesembolso.Texto);
                pGiro.fec_giro = DateTime.Now;
                pGiro.numero_radicacion = 0;
                pGiro.usu_gen = pusu.nombre;
                pGiro.usu_apli = null;
                pGiro.estadogi = 0;
                pGiro.usu_apro = null;


                if (ctlGiro.IndiceFormaDesem == 1) //"eFECTIVO"
                {
                    pGiro.idctabancaria = 0;
                    pGiro.cod_banco = 0;
                    pGiro.num_cuenta = null;
                    pGiro.tipo_cuenta = -1;
                    pGenerarGiro = true;

                }
                if (ctlGiro.IndiceFormaDesem != 1 && ctlGiro.IndiceFormaDesem != 4) //"eFECTIVO"
                {

                    //DETERMINAR LA IDENTIFICACIÓN DE LA CUENTA BANCARIA
                    CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ctlGiro.ValueEntidadOrigen), ctlGiro.TextCuentaOrigen, (Usuario)Session["usuario"]);
                    Int64 idCta = CuentaBanc.idctabancaria;
                    //DATOS DE FORMA DE PAGO
                    if (ctlGiro.IndiceFormaDesem == 3) //"Transferencia"
                    {
                        pGenerarGiro = true;
                        pGiro.idctabancaria = idCta;
                        pGiro.cod_banco = Convert.ToInt32(ctlGiro.ValueEntidadDest);
                        pGiro.num_cuenta = ctlGiro.TextNumCuenta;
                        pGiro.tipo_cuenta = Convert.ToInt32(ctlGiro.ValueTipoCta);
                   ;

                    }
                    else if (ctlGiro.IndiceFormaDesem == 2) //Cheque
                    {
                        pGenerarGiro = true;
                        pGiro.idctabancaria = idCta;
                        pGiro.cod_banco = 0;        //NULO
                        pGiro.num_cuenta = null;    //NULO
                        pGiro.tipo_cuenta = -1;      //NULO
                  
                    }
                    else if (ctlGiro.IndiceFormaDesem == 4)
                    {
                        pGenerarGiro = false;

                        pVar.numero_cuenta_ahorro_vista = !string.IsNullOrWhiteSpace(ctlGiro.ValueCuentaAhorro) ? Convert.ToInt64(ctlGiro.ValueCuentaAhorro) : default(long?);

                        if (!pVar.numero_cuenta_ahorro_vista.HasValue)
                        {
                            VerError("No tiene una cuenta de ahorro a la vista asociada para esta forma de pago!.");
                        }
                     
                    }
                    else
                    {
                        pGiro.idctabancaria = 0;
                        pGiro.cod_banco = 0;
                        pGiro.num_cuenta = null;
                        pGiro.tipo_cuenta = -1;
                 
                    }
                }


                pGiro.valor = Convert.ToInt64(txtValorAproba.Text.ToString().Replace(gSeparadorMiles, ""));
                //GRABAR TRAN_AUXILIO
                DesembolsoAuxilio pDesem = new DesembolsoAuxilio();
                pDesem.numero_transaccion = 0;
                pDesem.numero_auxilio = pVar.numero_auxilios;
                pDesem.cod_cliente = Convert.ToInt64(txtCodPersona.Text);
                pDesem.cod_linea_auxilio = ddlLinea.SelectedValue;
                pDesem.tipo_tran = 1000;
                pDesem.valor = Convert.ToDecimal(txtValorAproba.Text);
                pDesem.estado = 1;
                pDesem.num_tran_anula = 0; // NULL
                Int64 COD_OPE = 0;
                Int32 pIdGiro = 0;
                
                List<Auxilios_Giros> lstGiros = new List<Auxilios_Giros>();
                bool pOpcionGiro = chkDistribuir.Checked ? true : false;
                lstGiros = ObtenerListaDistribucion();

                if (ctlGiro.ValueCuentaAhorro != "" )
                    pDesem.numero_cuenta_ahorro_vista = Convert.ToInt64(ctlGiro.ValueCuentaAhorro);
                else pDesem.numero_cuenta_ahorro_vista = 0;
                DesemServicios.CrearTran_Auxilio(Convert.ToInt64(ctlGiro.IndiceFormaDesem),pOpcionGiro, lstGiros, ref COD_OPE, ref pIdGiro, pDesem, pOperacion, pGiro, pGenerarGiro, (Usuario)Session["usuario"]);

                //GENERAR EL COMPROBANTE
                if (COD_OPE != 0)
                {
                    Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                    Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = COD_OPE;
                    Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 111;
                    Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = txtCodPersona.Text; //"<Colocar Aquí el código de la persona del servicio>"
                    Session[ComprobanteServicio.CodigoPrograma + ".idgiro"] = pOpcion == true && pIdGiro > 0 ? pIdGiro.ToString() : null;
                }
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            return false;
        }
        return true;
    }

    

    protected void txtIdPersona_TextChanged(object sender, EventArgs e)
    {
        Xpinn.Seguridad.Services.UsuarioService UsuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
        Xpinn.FabricaCreditos.Entities.Persona1 DatosPersona = new Xpinn.FabricaCreditos.Entities.Persona1();

        DatosPersona = UsuarioServicio.ConsultarPersona1(txtIdPersona.Text, (Usuario)Session["usuario"]);

        if (DatosPersona.cod_persona != 0)
        {
            if (DatosPersona.cod_persona != 0)
                txtCodPersona.Text = DatosPersona.cod_persona.ToString();
            if (DatosPersona.identificacion != "")
                txtIdPersona.Text = DatosPersona.identificacion;
            if (DatosPersona.nombre != "")
                txtNomPersona.Text = DatosPersona.nombre;
        }
        else
        {
            txtNomPersona.Text = "";
            txtCodPersona.Text = "";
        }
    }

    protected void ddlLinea_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLinea.SelectedIndex != 0)
        {
            SolicitudAuxilio Datos = new SolicitudAuxilio();
            Datos = SolicAuxilios.ListarLineasDauxilios(Convert.ToInt32(ddlLinea.SelectedValue), (Usuario)Session["usuario"]);
            if (Datos.cupos != 0)
                txtCupos.Text = Datos.cupos.ToString();
            else
                txtCupos.Text = "";
            if (Datos.monto_maximo != 0)
                txtMontoDisp.Text = Datos.monto_maximo.ToString();
            else
                txtMontoDisp.Text = "";
        }
        else
        {
            txtCupos.Text = "";
            txtMontoDisp.Text = "";           
        }
    }

  



    #region DISTRIBUCION DE AUXILIOS

    protected void gvDistribucion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlTipo = (DropDownListGrid)e.Row.FindControl("ddlTipo");
            if (ddlTipo != null)
            {
                ddlTipo.Items.Insert(0, new ListItem("Asociado", "0"));
                ddlTipo.Items.Insert(1, new ListItem("Tercero", "1"));
                ddlTipo.SelectedIndex = 0;
                ddlTipo.DataBind();

                Label lblTipo = (Label)e.Row.FindControl("lblTipo");
                if (lblTipo.Text != "")
                    ddlTipo.SelectedValue = lblTipo.Text;
            }
        }
    }

    protected void gvDistribucion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvDistribucion.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaDistribucion();

        List<Auxilios_Giros> LstDetalle = new List<Auxilios_Giros>();
        LstDetalle = (List<Auxilios_Giros>)Session["Distribucion"];
        if (conseID > 0)
        {
            try
            {
                foreach (Auxilios_Giros acti in LstDetalle)
                {
                    if (acti.idgiro == conseID)
                    {
                        LstDetalle.Remove(acti);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
        }
        else
        {
            LstDetalle.RemoveAt((gvDistribucion.PageIndex * gvDistribucion.PageSize) + e.RowIndex);
        }
        Session["Distribucion"] = LstDetalle;

        gvDistribucion.DataSource = LstDetalle;
        gvDistribucion.DataBind();
        CalculaTotalXColumna();
    }

    protected void chkDistribuir_CheckedChanged(object sender, EventArgs e)
    {
        panelDistribucion.Visible = chkDistribuir.Checked ? true : false;
    }

    protected void btnAdicionarFila_Click(object sender, EventArgs e)
    {
        ObtenerListaDistribucion();

        List<Auxilios_Giros> LstPrograma = new List<Auxilios_Giros>();
        if (Session["Distribucion"] != null)
        {
            LstPrograma = (List<Auxilios_Giros>)Session["Distribucion"];

            for (int i = 1; i <= 1; i++)
            {
                Auxilios_Giros pDetalle = new Auxilios_Giros();
                pDetalle.idgiro = -1;
                pDetalle.cod_persona = null;
                pDetalle.identificacion = "";
                pDetalle.nombre = "";
                pDetalle.valor = null;
                pDetalle.tipo = 0;
                LstPrograma.Add(pDetalle);
            }
            gvDistribucion.DataSource = LstPrograma;
            gvDistribucion.DataBind();

            Session["Distribucion"] = LstPrograma;
            CalculaTotalXColumna();
        }
    }

    protected List<Auxilios_Giros> ObtenerListaDistribucion()
    {
        try
        {
            List<Auxilios_Giros> lstDetalle = new List<Auxilios_Giros>();
            List<Auxilios_Giros> lista = new List<Auxilios_Giros>();

            foreach (GridViewRow rfila in gvDistribucion.Rows)
            {
                Auxilios_Giros eBene = new Auxilios_Giros();

                Label lblCodigo = (Label)rfila.FindControl("lblCodigo");
                if (lblCodigo != null)
                    eBene.idgiro = Convert.ToInt32(lblCodigo.Text);
                else
                    eBene.idgiro = -1;

                Label lblCod_persona = (Label)rfila.FindControl("lblCod_persona");
                if (lblCod_persona.Text != "")
                    eBene.cod_persona = Convert.ToInt64(lblCod_persona.Text);

          

                TextBoxGrid txtIdentificacionD = (TextBoxGrid)rfila.FindControl("txtIdentificacionD");
                if (txtIdentificacionD.Text != "")
                    eBene.identificacion = txtIdentificacionD.Text;

                TextBoxGrid txtNombre = (TextBoxGrid)rfila.FindControl("txtNombre");
                if (txtNombre.Text != "")
                    eBene.nombre = txtNombre.Text;

                DropDownListGrid ddlTipo = (DropDownListGrid)rfila.FindControl("ddlTipo");
                eBene.tipo = Convert.ToInt32(ddlTipo.SelectedValue);

                decimalesGridRow txtValor = (decimalesGridRow)rfila.FindControl("txtValor");
                if (txtValor.Text.Trim() != "")
                    eBene.valor = Convert.ToDecimal(txtValor.Text);

                lista.Add(eBene);
                Session["Distribucion"] = lista;

                if (eBene.identificacion != null && eBene.nombre != null && eBene.valor != null)
                {
                    lstDetalle.Add(eBene);
                }
            }
            return lstDetalle;
        }
        catch
        {
            return null;
        }
    }


    protected void InicializarDistribucion()
    {

        List<Auxilios_Giros> lstDistribucion = new List<Auxilios_Giros>();
        for (int i = gvDistribucion.Rows.Count; i < 2; i++)
        {
            Auxilios_Giros pDetalle = new Auxilios_Giros();
            pDetalle.idgiro = -1;
            pDetalle.cod_persona = null;
            pDetalle.identificacion = "";
            pDetalle.nombre = "";
            pDetalle.valor = null;
            pDetalle.tipo = 0;
            lstDistribucion.Add(pDetalle);
        }
        gvDistribucion.DataSource = lstDistribucion;
        gvDistribucion.DataBind();

        Session["Distribucion"] = lstDistribucion;
    }

    void CalculaTotalXColumna()
    {
        lblErrorDist.Text = "";
        decimal Fvalor = 0, MontoAprobado = 0;
        Label lblTotalVr = (Label)gvDistribucion.FooterRow.FindControl("lblTotalVr");

        foreach (GridViewRow rfila in gvDistribucion.Rows)
        {
            decimalesGridRow txtValor = (decimalesGridRow)rfila.FindControl("txtValor");
            if (txtValor.Text != "")
                Fvalor += Convert.ToDecimal(txtValor.Text.Replace(gSeparadorMiles, ""));
        }
        if (lblTotalVr != null)
            lblTotalVr.Text = Fvalor.ToString("c0");
        MontoAprobado = txtValorAproba.Text != "" ? Convert.ToDecimal(txtValorAproba.Text.Replace(gSeparadorMiles, "")) : 0;
        if (Fvalor > MontoAprobado)
        {
            lblErrorDist.Text = "Error al ingresar los datos, El monto total de las distribuciones no puede superar el monto Aprobado";
            return;
        }
    }

    protected void txtIdentificacionD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Seguridad.Services.UsuarioService UsuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
            Xpinn.FabricaCreditos.Entities.Persona1 DatosPersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            TextBoxGrid txtIdentificacion = (TextBoxGrid)sender;
            int rowIndex = Convert.ToInt32(txtIdentificacion.CommandArgument);

            Label lblcod_persona = (Label)gvDistribucion.Rows[rowIndex].FindControl("lblcod_persona");
            TextBoxGrid txtNombre = (TextBoxGrid)gvDistribucion.Rows[rowIndex].FindControl("txtNombre");
            DropDownListGrid ddlTipo = (DropDownListGrid)gvDistribucion.Rows[rowIndex].FindControl("ddlTipo");
            if (txtIdentificacion.Text != "")
            {
                DatosPersona = UsuarioServicio.ConsultarPersona1(txtIdentificacion.Text, (Usuario)Session["usuario"]);

                if (DatosPersona.cod_persona != 0)
                {
                    if (lblcod_persona != null)
                        lblcod_persona.Text = DatosPersona.cod_persona.ToString();
                    if (DatosPersona.nombre != null)
                        txtNombre.Text = DatosPersona.nombre;
                    ddlTipo.SelectedIndex = 0;
                }
                else
                {
                    lblcod_persona.Text = "";
                    ddlTipo.SelectedIndex = 1;
                }
            }
            else
                lblcod_persona.Text = "";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DesemServicios.CodigoPrograma, "txtIdentificacionD_TextChanged", ex);
        }
    }

    #endregion
    
}
