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
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Drawing.Imaging;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Tesoreria.Services;
using System.Drawing;
using Ghostscript.NET;


partial class Nuevo : GlobalWeb
{
    private Xpinn.Ahorros.Services.AhorroVistaServices ahorrosServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();

    CuentasExentasServices ExentaServicio = new CuentasExentasServices();
    PoblarListas Poblar = new PoblarListas();
    Xpinn.CDATS.Services.AperturaCDATService AperturaService = new Xpinn.CDATS.Services.AperturaCDATService();

    NumeracionCuentas BONumeracionCuentaCDAT = new NumeracionCuentas();

    private Xpinn.FabricaCreditos.Services.BeneficiarioService BeneficiarioServicio = new Xpinn.FabricaCreditos.Services.BeneficiarioService();
    private object GhostscriptPngDeviceType;

    public object GhostscriptImageDeviceAlphaBits { get; private set; }

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ahorrosServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ahorrosServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(ahorrosServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.MostrarConsultar(false);
            toolBar.eventoConsultar += btnConsultar_Click;
            //ctlBusquedaPersonas.eventoEditar += gvListaTitulares_SelectedIndexChanged;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ddlLineaAhorro.eventoSelectedIndexChanged += ddlLineaAhorro_SelectedIndexChanged;

            if (!IsPostBack)
            {
                //   panelPersona.Enabled = false;
                CrearDetalleInicial();

                cbInteresCuenta.Enabled = false;

                cbRetencion.Enabled = false;
                mvAhorroVista.ActiveViewIndex = 1;
                txtNumeroCuenta.Enabled = false;
                CargarListas();
                if (Session[ahorrosServicio.CodigoPrograma + ".id"] != null)
                {
                    mvAhorroVista.ActiveViewIndex = 1;
                    pDatos.Visible = false;
                    idObjeto = Session[ahorrosServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ahorrosServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarConsultar(true);
                    toolBar.MostrarGuardar(true);
                    mvAhorroVista.ActiveViewIndex = 1;
                    Usuario pusuario = (Usuario)Session["Usuario"];
                    if (ahorrosServicio.GeneraNumeroCuenta(pusuario) == false)
                        txtNumeroCuenta.Enabled = true;
                    // Inicializar variables
                    txtNumeroCuenta.Text = "";
                    ddlEstado.SelectedValue = "0";
                    txtSaldoTotal.Text = "0";
                    txtSaldoCanje.Text = "0";
                    txtSaldoInteres.Text = "0";
                    pDatos.Visible = true;
                    txtFechaApertura.ToDateTime = System.DateTime.Now;
                    txtFechaInteres.ToDateTime = System.DateTime.Now;
                    ddlOficina.SelectedValue = pusuario.cod_oficina.ToString();
                    ddlModalidad.SelectedValue = "1";
                    // Ocultar datos
                    pDatos.Visible = false;
                    lblEstado.Visible = false;
                    ddlEstado.Visible = false;

                    PanelLibreta.Visible = false;

                    Xpinn.CDATS.Entities.Cdat Data = new Xpinn.CDATS.Entities.Cdat();
                    Data = AperturaService.ConsultarNumeracionCDATS(Data, (Usuario)Session["usuario"]);

                    if (Data.valor == 1)
                    {
                        txtNumeroCuenta.Visible = false;
                        lblNumAuto.Visible = true;
                    }
                    else
                    {
                        txtNumeroCuenta.Visible = true;
                        lblNumAuto.Visible = false;
                    }
                }
                if (Session["solicitudProducto"] != null)
                {
                    cargarDatosSolicitud();
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        VerError("");
        ctlBusquedaPersonas.Filtro = "";
        ctlBusquedaPersonas.Actualizar(0);
    }

    protected Boolean ValidarDatos()
    {
        if (txtNumeroCuenta.Visible == true)
        {
            if (txtNumeroCuenta.Text == "")
            {
                VerError("Ingrese el número de la cuenta");
                txtNumeroCuenta.Focus();
                return false;
            }
        }
        if (txtFechaApertura.Texto == "")
        {
            VerError("Ingrese la Fecha de Apertura");
            txtFechaApertura.Focus();
            return false;
        }
        if (ddlLineaAhorro.Indice == 0)
        {
            VerError("Seleccione la Linea de Ahorros");
            return false;
        }
        if (ddlOficina.SelectedIndex == 0)
        {
            VerError("Seleccione la Oficina");
            ddlOficina.Focus();
            return false;
        }
        if (ddlModalidad.SelectedValue == "0")
        {
            VerError("Seleccione la Modalidad");
            ddlModalidad.Focus();
            return false;
        }

        if (ddlModalidad.SelectedValue == "2")
        {
            int ContTol = 0;
            string pIdentificacion = string.Empty;
            Int64 pCodpersona = 0;
            foreach (GridViewRow rFila in gvDetalle.Rows)
            {
                TextBox txtCod_persona = (TextBox)rFila.FindControl("lblcodigo");
                if (txtCod_persona != null)
                    if (txtCod_persona.Text != "")
                        pCodpersona = Convert.ToInt64(txtCod_persona.Text.Trim());

                TextBoxGrid txtIdentificacion = (TextBoxGrid)rFila.FindControl("txtIdentificacion");
                if (txtIdentificacion != null)
                    if (txtIdentificacion.Text != "")
                        pIdentificacion = txtIdentificacion.Text.Trim();
                if (pCodpersona != 0 && pIdentificacion != "")
                {
                    ContTol += 1;
                }
                pIdentificacion = "";
                pCodpersona = 0;
            }
            if (ContTol <= 1)
            {
                VerError("Debe tener mas de un titular para la Modalidad CONJUNTA");
                return false;
            }
        }

        if (ddlEstado.SelectedIndex == 0)
        {
            VerError("Seleccione el estado");
        }

        List<CuentaHabientes> LstDetalle = new List<CuentaHabientes>();
        LstDetalle = ObtenerListaDetalle();

        if (LstDetalle.Count == 0)
        {
            VerError("Debe Ingresar un Titular Principal");
            return false;
        }
        int cont = 0;
        if (ddlModalidad.SelectedItem.Text == "INDIVIDUAL")
        {
            if (LstDetalle.Count > 1)
            {
                VerError("Solo debe ingresar un Titular para la Modalidad INDIVIDUAL");
                return false;
            }

            foreach (CuentaHabientes deta in LstDetalle)
            {
                if (deta.principal == 1)
                {
                    cont++;
                }
            }
            if (cont != 1)
            {
                VerError("Debe selecciona un titular principal");
                return false;
            }
        }
        else
        {
            if (LstDetalle.Count > 0)
            {
                foreach (CuentaHabientes deta in LstDetalle)
                {
                    if (deta.principal == 1)
                    {
                        cont++;
                    }
                }
                if (cont != 1)
                {
                    VerError("Debe selecciona un titular principal");
                    return false;
                }
            }
        }

        return true;
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (ValidarDatos())
            ctlMensaje.MostrarMensaje("Desea guardar los datos de la cuenta de ahorros?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        //try
        //{
            Usuario vUsuario = new Usuario();
            vUsuario = (Usuario)Session["Usuario"];

            Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();

            Usuario pUsu = (Usuario)Session["usuario"];
            //OBTENER LOS DATOS DEL TITULAR PRINCIPAL
            string pIdentificacion1 = string.Empty;
            Int64 pCodpersona1 = 0;
            foreach (GridViewRow rFila in gvDetalle.Rows)
            {
                CheckBoxGrid chkPrincipal = (CheckBoxGrid)rFila.FindControl("chkPrincipal");
                if (chkPrincipal != null)
                {
                    if (chkPrincipal.Checked)
                    {
                        TextBox txtCod_persona = (TextBox)rFila.FindControl("lblcodigo");
                        if (txtCod_persona != null)
                            if (txtCod_persona.Text != "")
                                pCodpersona1 = Convert.ToInt64(txtCod_persona.Text.Trim());

                        TextBoxGrid txtIdentificacion = (TextBoxGrid)rFila.FindControl("txtIdentificacion");
                        if (txtIdentificacion != null)
                            if (txtIdentificacion.Text != "")
                                pIdentificacion1 = txtIdentificacion.Text.Trim();
                        break;
                    }
                }
            }

            Xpinn.CDATS.Entities.Cdat Data = new Xpinn.CDATS.Entities.Cdat();
            Data = AperturaService.ConsultarNumeracionCDATS(Data, (Usuario)Session["usuario"]);

            if (idObjeto == "")
            {
                if (Data.valor == 1)
                {
                    string pError = "";
                    string autogenerado = BONumeracionCuentaCDAT.ObtenerCodigoParametrizado(1, pIdentificacion1, pCodpersona1, ddlLineaAhorro.Value, ref pError, pUsu);

                    if (pError != "")
                    {
                        VerError(pError);
                        return;
                    }
                    if (autogenerado == "ErrorGeneracion")
                    {
                        VerError("Se generó un error al construir el consecutivo CDAT");
                        return;
                    }
                    vAhorroVista.numero_cuenta = autogenerado;
                }
                else
                {
                    vAhorroVista.numero_cuenta = txtNumeroCuenta.Text;
                }
            }
            else
            {
                vAhorroVista.numero_cuenta = txtNumeroCuenta.Text;
            }

            vAhorroVista.cod_linea_ahorro = ddlLineaAhorro.Value;

            vAhorroVista.cod_oficina = Convert.ToInt32(ddlOficina.SelectedValue);
            if (ddlDestinacion.Value != "0")
                vAhorroVista.cod_destino = Convert.ToInt32(ddlDestinacion.Value);
            vAhorroVista.observaciones = txtObservaciones.Text != "" ? txtObservaciones.Text : null;
            vAhorroVista.modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);

            //OBTENER LOS DATOS DEL TITULAR PRINCIPAL
            string pIdentificacion = string.Empty;
            Int64 pCodpersona = 0;
            foreach (GridViewRow rFila in gvDetalle.Rows)
            {
                CheckBoxGrid chkPrincipal = (CheckBoxGrid)rFila.FindControl("chkPrincipal");
                if (chkPrincipal != null)
                {
                    if (chkPrincipal.Checked)
                    {
                        TextBox txtCod_persona = (TextBox)rFila.FindControl("lblcodigo");
                        if (txtCod_persona != null)
                            if (txtCod_persona.Text != "")
                                pCodpersona = Convert.ToInt64(txtCod_persona.Text.Trim());

                        vAhorroVista.cod_persona = pCodpersona;
                        TextBoxGrid txtIdentificacion = (TextBoxGrid)rFila.FindControl("txtIdentificacion");
                        if (txtIdentificacion != null)
                            if (txtIdentificacion.Text != "")
                                pIdentificacion = txtIdentificacion.Text.Trim();
                        break;
                    }
                }
            }

            if (txtFechaApertura.TieneDatos == true)
                vAhorroVista.fecha_apertura = txtFechaApertura.ToDateTime;

            if (txtFechaCierre.TieneDatos == true)
                vAhorroVista.fecha_cierre = txtFechaCierre.ToDateTime;
            vAhorroVista.saldo_total = txtSaldoTotal.Text != null ? Convert.ToDecimal(txtSaldoTotal.Text) : 0;
            vAhorroVista.saldo_canje = txtSaldoCanje.Text != null ? Convert.ToDecimal(txtSaldoCanje.Text) : 0;

            vAhorroVista.forma_tasa = Convert.ToInt32(ctlTasaInteres.FormaTasa);
            if (ctlTasaInteres.Indice == 0)//NIGUNA
            {
                vAhorroVista.tipo_historico = null;
                vAhorroVista.desviacion = 0;
                vAhorroVista.tasa = 0;
                vAhorroVista.tipo_tasa = 0;
            }
            else if (ctlTasaInteres.Indice == 1)//FIJO
            {
                vAhorroVista.tipo_historico = null;
                vAhorroVista.desviacion = 0;
                if (ctlTasaInteres.Tasa != 0)
                    vAhorroVista.tasa = ctlTasaInteres.Tasa;
                vAhorroVista.tipo_tasa = ctlTasaInteres.TipoTasa;
            }
            else // HISTORICO
            {
                vAhorroVista.tipo_tasa = 0;
                vAhorroVista.tipo_historico = ctlTasaInteres.TipoHistorico;
                if (ctlTasaInteres.Desviacion != 0)
                    vAhorroVista.desviacion = ctlTasaInteres.Desviacion;
            }


            if (txtFechaInteres.TieneDatos == true)
                vAhorroVista.fecha_interes = txtFechaInteres.ToDateTime;
            else
                vAhorroVista.fecha_interes = System.DateTime.Now;
            vAhorroVista.saldo_intereses = txtSaldoInteres.Text != null && txtSaldoInteres.Text != "" ? Convert.ToDecimal(txtSaldoInteres.Text) : 0;
            vAhorroVista.retencion = Convert.ToInt32(cbRetencion.Checked);
            vAhorroVista.cod_forma_pago = Convert.ToInt32(ddlFormaPago.SelectedValue);

            if (ddlFormaPago.SelectedValue.ToEnum<TipoFormaPago>() == TipoFormaPago.Nomina)
            {
                if (!string.IsNullOrWhiteSpace(ddlEmpresaRecaudo.SelectedValue))
                {
                    vAhorroVista.cod_empresa_reca = Convert.ToInt32(ddlEmpresaRecaudo.SelectedValue);
                }
                else
                {
                    VerError("No puedes grabar con forma de pago 'Nomina' si no tienes asignada ninguna empresa recaudo");
                    return;
                }
            }

            if (txtProximoPago.TieneDatos == true)
                vAhorroVista.fecha_proximo_pago = txtProximoPago.ToDateTime;
            vAhorroVista.valor_cuota = txtCuota.Text != null ? Convert.ToDecimal(txtCuota.Text) : 0;
            vAhorroVista.cod_periodicidad = ddlPeriodicidad.cod_periodicidad;

            if (!string.IsNullOrWhiteSpace(ddlAsesor.SelectedValue))
            {
                vAhorroVista.cod_asesor = Convert.ToInt64(ddlAsesor.SelectedValue);
            }


            // Cargar los cuentahabientes
            vAhorroVista.LstCuentaHabientes = new List<CuentaHabientes>();
            foreach (GridViewRow rfila in gvDetalle.Rows)
            {
                CuentaHabientes detalle = new CuentaHabientes();
                detalle.numero_cuenta = vAhorroVista.numero_cuenta;
                TextBox txtCod_persona = (TextBox)rfila.FindControl("lblcodigo");
                TextBox lblcod_cuentahabiente = (TextBox)rfila.FindControl("lblcod_cuentahabiente");

                if (lblcod_cuentahabiente != null)
                    if (lblcod_cuentahabiente.Text.Trim() != "")
                        detalle.idcuenta_habiente = Convert.ToInt64(lblcod_cuentahabiente.Text);
                    else
                        detalle.idcuenta_habiente = 0;

                if (txtCod_persona != null)
                    if (txtCod_persona.Text.Trim() != "")
                        detalle.cod_persona = Convert.ToInt64(txtCod_persona.Text);
                    else
                        detalle.cod_persona = null;
                else
                    detalle.cod_persona = null;
                TextBox txtIdentificD = (TextBox)rfila.FindControl("txtIdentificacion");
                if (txtIdentificD != null)
                    detalle.identificacion = txtIdentificD.Text;
                else
                    detalle.identificacion = "";
                CheckBox chkPrincipal = (CheckBox)rfila.FindControl("chkPrincipal");
                if (chkPrincipal.Checked)
                    detalle.tipo_firma = 1;
                else
                    detalle.tipo_firma = 0;
                vAhorroVista.LstCuentaHabientes.Add(detalle);
            }
            //vAhorroVista.LstCuentaHabientes = new List<CuentaHabientes>();
            //vAhorroVista.LstCuentaHabientes = ObtenerListaDetalle();

            // para cargar tarjeta de firmas
            if (hdFileName.Value != null)
            {
                try
                {
                    Stream stream = null;
                    /*Se guarda la imagen en un stream para despues colocarla en un objeto para que la imagen no quede abierta en el servidor*/
                    stream = File.OpenRead(Server.MapPath("Images\\") + Path.GetFileName(this.hdFileName.Value));
                    this.Response.Clear();
                    if (stream.Length > 5000000)
                    {
                        VerError("La imagen excede el tamaño máximo que es de " + 5000000);
                        return;
                    }
                    using (BinaryReader br = new BinaryReader(stream))
                    {
                        vAhorroVista.foto = br.ReadBytes(Convert.ToInt32(stream.Length));
                    }
                }
                catch
                {
                    vAhorroVista.foto = null;
                }
            }
            vAhorroVista.lstBeneficiarios = new List<Beneficiario>();
            if (chkBeneficiario.Checked)
            {
                vAhorroVista.lstBeneficiarios = ObtenerListaBeneficiariosAhorroVista();
            }

            //verificar cierre historico 
            String estado = "";
            DateTime fechacierrehistorico;
            DateTime fechaapertura = txtFechaApertura.ToDateTime;
            Xpinn.Ahorros.Entities.AhorroVista vAhorro = new Xpinn.Ahorros.Entities.AhorroVista();
            vAhorro = ahorrosServicio.ConsultarCierreAhorroVista((Usuario)Session["usuario"]);
            if (vAhorro != null)
            { 
                estado = vAhorro.estadocierre;
                fechacierrehistorico = Convert.ToDateTime(vAhorro.fecha_cierre.ToString());
            }
            else
            {
                estado = "D";
                fechacierrehistorico = new DateTime(DateTime.Now.Year, 1, 1);
            }


            //  vAhorroVista.numero_cuenta = Convert.ToString(idObjeto);
            if (idObjeto != "")
            {
                if(vAhorroVista.tasa == null)                 
                {
                   vAhorroVista.tasa = 0;
                }
                vAhorroVista.estado = Convert.ToInt32(ddlEstado.SelectedValue);
                // vAhorroVista.numero_cuenta = Convert.ToString(idObjeto);
                vAhorroVista = ahorrosServicio.ModificarAhorroVista(vAhorroVista, (Usuario)Session["usuario"]);
                lblMsj.Text = "Modificada";
                lblgenerado.Text = vAhorroVista.numero_cuenta;
            }
            else
            {
                if (vAhorro != null)
                {
                    if (estado == "D" && fechaapertura <= fechacierrehistorico)
                    {
                        VerError("NO PUEDE INGRESAR APERTURAS EN PERIODOS YA CERRADOS, TIPO H,'AHORROS'");
                        return;
                    }
                }

                vAhorroVista.estado = 0; //Apertura
                vAhorroVista = vAhorroVista = ahorrosServicio.CrearAhorroVista(vAhorroVista, (Usuario)Session["usuario"]);
                idObjeto = Convert.ToString(vAhorroVista.numero_cuenta);
                lblMsj.Text = "Creada";

                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarCancelar(false);
                toolBar.MostrarConsultar(false);
                toolBar.MostrarImprimir(false);

                lblgenerado.Text = vAhorroVista.numero_cuenta;
            }

            //CREAR  SI ES EXENTA
            if (ChkExentaGmf.Checked)
            {
                CuentasExenta eExenta = new CuentasExenta();

                eExenta.idexenta = IdExcenta.Text != "" ? Convert.ToInt64(IdExcenta.Text) : 0;
                eExenta.numero_cuenta = vAhorroVista.numero_cuenta;
                //Codigo de tipo Cuenta Ahorros Vista 3 
                eExenta.tipo_cuenta = 3;
                eExenta.fecha_exenta = DateTime.Now;
                eExenta.monto = Convert.ToDecimal(999999999999);
                eExenta.cod_persona = Convert.ToInt64(vAhorroVista.cod_persona);
                eExenta.fecha = null;
                if (IdExcenta.Text != "")
                {
                    if (!string.IsNullOrEmpty(FechaExcenta.Text))
                        eExenta.fecha_exenta = Convert.ToDateTime(FechaExcenta.Text);
                    else
                        eExenta.fecha_exenta = DateTime.Now;

                    ExentaServicio.CrearCuentaExentApertura(eExenta, (Usuario)Session["usuario"], 2);
                    actualizarSolicitud(pCodpersona);
                }
                else
                {
                    ExentaServicio.CrearCuentaExentApertura(eExenta, (Usuario)Session["usuario"], 1);
                    actualizarSolicitud(pCodpersona);
                }

            }
            else
            {
                CuentasExenta eExenta = new CuentasExenta();
                eExenta.idexenta = IdExcenta.Text != "" ? Convert.ToInt64(IdExcenta.Text) : 0;
                eExenta.numero_cuenta = vAhorroVista.numero_cuenta;
                //Codigo de tipo Cuenta Ahorros Vista 3 
                eExenta.tipo_cuenta = 3;
                if (!string.IsNullOrEmpty(FechaExcenta.Text))
                    eExenta.fecha_exenta = Convert.ToDateTime(FechaExcenta.Text);
                else
                    eExenta.fecha_exenta = DateTime.Now;
                eExenta.monto = Convert.ToDecimal(999999999999);
                eExenta.cod_persona = Convert.ToInt64(vAhorroVista.cod_persona);
                eExenta.fecha = DateTime.Now;
                if (IdExcenta.Text != null)
                {
                    ExentaServicio.CrearCuentaExentApertura(eExenta, (Usuario)Session["usuario"], 2);
                    actualizarSolicitud(pCodpersona);
                }

            }
            mvAhorroVista.ActiveViewIndex = 2;
        //}
        //catch (ExceptionBusiness ex)
        //{
        //    VerError(ex.Message);
        //}
        //catch (Exception ex)
        //{
        //    BOexcepcion.Throw(ahorrosServicio.CodigoPrograma, "btnGuardar_Click", ex);
        //}


    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (Session["solicitudProducto"] != null)
        {            
            Session["solicitudProducto"] = null;
            Response.Redirect("../../Aportes/ConfirmarProductoAprobado/Lista.aspx", false);
        }
        else
        {
            Navegar(Pagina.Lista);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();
            vAhorroVista = ahorrosServicio.ConsultarAhorroVista(pIdObjeto, (Usuario)Session["usuario"]);
            Usuario lusuario = (Usuario)Session["usuario"];

            txtNumeroCuenta.Text = HttpUtility.HtmlDecode(vAhorroVista.numero_cuenta.ToString().Trim());
            //txtDigVer.Text = txtNumeroCuenta.Text != null ? CalcularDigitoVerificacion(txtNumeroCuenta.Text) : "0";
            if (!string.IsNullOrEmpty(vAhorroVista.cod_linea_ahorro.ToString()))
                ddlLineaAhorro.Value = HttpUtility.HtmlDecode(vAhorroVista.cod_linea_ahorro.ToString().Trim());

            if (!string.IsNullOrEmpty(vAhorroVista.cod_oficina.ToString()))
                ddlOficina.SelectedValue = HttpUtility.HtmlDecode(vAhorroVista.cod_oficina.ToString().Trim());
            if (!string.IsNullOrEmpty(vAhorroVista.cod_destino.ToString()))
                ddlDestinacion.Value = HttpUtility.HtmlDecode(vAhorroVista.cod_destino.ToString().Trim());
            if (vAhorroVista.observaciones != null)
                txtObservaciones.Text = HttpUtility.HtmlDecode(vAhorroVista.observaciones.ToString().Trim());
            if (!string.IsNullOrEmpty(vAhorroVista.modalidad.ToString()))
                ddlModalidad.SelectedValue = HttpUtility.HtmlDecode(vAhorroVista.modalidad.ToString().Trim());
            if (ddlModalidad.SelectedValue == "1")
            {
                gvDetalle.Visible = false;
            }

            if (!string.IsNullOrEmpty(vAhorroVista.estado.ToString()))
                ddlEstado.SelectedValue = HttpUtility.HtmlDecode(vAhorroVista.estado.ToString().Trim());
            if (!string.IsNullOrEmpty(vAhorroVista.fecha_apertura.ToString()))
                txtFechaApertura.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vAhorroVista.fecha_apertura.ToString().Trim()));
            if (!string.IsNullOrEmpty(vAhorroVista.fecha_cierre.ToString()))
                txtFechaCierre.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vAhorroVista.fecha_cierre.ToString().Trim()));
            if (!string.IsNullOrEmpty(vAhorroVista.saldo_total.ToString()))
                txtSaldoTotal.Text = HttpUtility.HtmlDecode(vAhorroVista.saldo_total.ToString().Trim());
            if (!string.IsNullOrEmpty(vAhorroVista.saldo_canje.ToString()))
                txtSaldoCanje.Text = HttpUtility.HtmlDecode(vAhorroVista.saldo_canje.ToString().Trim());

            if (!string.IsNullOrEmpty(vAhorroVista.fecha_interes.ToString()))
                txtFechaInteres.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vAhorroVista.fecha_interes.ToString().Trim()));
            if (!string.IsNullOrEmpty(vAhorroVista.saldo_intereses.ToString()))
                txtSaldoInteres.Text = HttpUtility.HtmlDecode(vAhorroVista.saldo_intereses.ToString().Trim());
            if (!string.IsNullOrEmpty(vAhorroVista.retencion.ToString()))
            {
                if (HttpUtility.HtmlDecode(vAhorroVista.retencion.ToString().Trim()) == "1")
                    cbRetencion.Checked = true;
                else
                    cbRetencion.Checked = false;
            }

            if (vAhorroVista.exenta == null)
            {
                ChkExentaGmf.Checked = false;
            }
            else
            {
                ChkExentaGmf.Checked = true;
                IdExcenta.Text = vAhorroVista.exenta;
            }


            if (vAhorroVista.fechaNoExcenta != DateTime.MinValue)
            {
                IdExcenta.Text = vAhorroVista.exenta == null ? vAhorroVista.exenta : "";
                FechaExcenta.Text = null;          

            }
            else
            {
              
                FechaExcenta.Text = vAhorroVista.fecha_exenta.ToString();
            }

            if (!string.IsNullOrEmpty(vAhorroVista.cod_forma_pago.ToString()))
                ddlFormaPago.SelectedValue = HttpUtility.HtmlDecode(vAhorroVista.cod_forma_pago.ToString().Trim());
            ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
            if (vAhorroVista.cod_empresa_reca != null)
                ddlEmpresaRecaudo.SelectedValue = vAhorroVista.cod_empresa_reca.ToString();

            if (!string.IsNullOrEmpty(vAhorroVista.fecha_proximo_pago.ToString()))
                txtProximoPago.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vAhorroVista.fecha_proximo_pago.ToString().Trim()));
            //if (!string.IsNullOrEmpty(vAhorroVista.fecha_ultimo_pago.ToString()))
            //    txtUltimoPago.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vAhorroVista.fecha_ultimo_pago.ToString().Trim()));
            if (!string.IsNullOrEmpty(vAhorroVista.valor_cuota.ToString()))
                txtCuota.Text = HttpUtility.HtmlDecode(vAhorroVista.valor_cuota.ToString().Trim());
            if (!string.IsNullOrEmpty(vAhorroVista.cod_periodicidad.ToString()))
                ddlPeriodicidad.cod_periodicidad = Convert.ToInt32(vAhorroVista.cod_periodicidad);
            if (vAhorroVista.cod_asesor.HasValue)
            {
                ddlAsesor.SelectedValue = vAhorroVista.cod_asesor.ToString();
            }

            //RECUPERAR GRILLA DETALLE 
            List<CuentaHabientes> lstDetalle = new List<CuentaHabientes>();

            lstDetalle = ahorrosServicio.ListarDetalleTitulares(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            int EnteTerri = 0;


            //Si tiene datos de solicitud los carga primero
            if (Session["solicitudProducto"] != null)
            {
                AhorroVista solicitud = new AhorroVista();
                solicitud = Session["solicitudProducto"] as AhorroVista;

                Xpinn.FabricaCreditos.Services.Persona1Service personaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
                Xpinn.FabricaCreditos.Entities.Persona1 persona = new Xpinn.FabricaCreditos.Entities.Persona1();
                persona.cod_persona = Convert.ToInt64(solicitud.cod_persona);
                persona.identificacion = solicitud.identificacion;
                List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = personaService.ListadoPersonas1(persona, (Usuario)Session["usuario"]);
                persona = lstConsulta.ElementAt(0);

                CuentaHabientes eDeta = new CuentaHabientes();

                eDeta.identificacion = persona.identificacion;
                eDeta.cod_persona = persona.cod_persona;
                eDeta.nombres = persona.nombres;
                eDeta.apellidos = persona.apellidos;
                eDeta.ciudad = persona.nomciudad_resid;
                eDeta.direccion = persona.direccion;
                eDeta.telefono = persona.telefono;
                eDeta.principal = 1;
                eDeta.conjuncion = null;
                lstDetalle.Add(eDeta);
            }


            if (lstDetalle.Count > 0)
            {
                gvDetalle.Visible = true;
                gvDetalle.DataSource = lstDetalle;
                gvDetalle.DataBind();

                string identificacion = lstDetalle.Where(x => x.principal == 1).Select(x => x.identificacion).FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(identificacion))
                {
                    ConsultarEmpresaRecaudoPersona(identificacion);
                }

                Int64? cod_per = lstDetalle.Where(x => x.principal == 1).Select(x => x.cod_persona).FirstOrDefault();
                if (cod_per != 0)
                {
                    Xpinn.Contabilidad.Entities.Tercero VPersona = new Xpinn.Contabilidad.Entities.Tercero();
                    Xpinn.Contabilidad.Services.TerceroService TerceroServicio = new Xpinn.Contabilidad.Services.TerceroService();
                    VPersona = TerceroServicio.ConsultarTercero(Convert.ToInt64(cod_per), null, (Usuario)Session["usuario"]);

                    EnteTerri = EnteTerri + VPersona.EnteTerritorial;
                }

                //Indica si es una entidad territorial
                if (EnteTerri > 0)
                {
                    lblEntTerritorial.Visible = true;
                    lblEntTerritorial.Text = "Ente Territorial";
                    ChkExentaGmf.Checked = true;
                    ChkExentaGmf.Enabled = false;
                }
                else
                {
                    lblEntTerritorial.Text = "";
                    //ChkExentaGmf.Checked = false;
                    ChkExentaGmf.Enabled = true;
                }

            }
            else
            {
                InicializarDetalle();
            }
            ddlModalidad_SelectedIndexChanged(ddlModalidad, null);

            // Mostrar imagenes de la persona
            if (vAhorroVista.foto != null)
            {
                try
                {
                    imgFoto.ImageUrl = Bytes_A_Archivo(pIdObjeto, vAhorroVista.foto);
                    imgFoto.ImageUrl = string.Format("Handler.ashx?id={0}", vAhorroVista.idimagen + "&Us=" + lusuario.identificacion + "&Pw=" + System.Web.HttpUtility.UrlEncode(lusuario.clave));

                }
                catch // (Exception ex)
                {
                    // VerError("No pudo abrir archivo con imagen de la persona " + ex.Message);
                }
            }


            List<Beneficiario> LstBeneficiario = new List<Beneficiario>();
            LstBeneficiario = BeneficiarioServicio.ConsultarBeneficiarioAhorroVista(pIdObjeto, (Usuario)Session["usuario"]);
            if (LstBeneficiario.Count > 0)
            {
                if ((LstBeneficiario != null) || (LstBeneficiario.Count != 0))
                {
                    //ValidarPermisosGrilla(gvBeneficiarios);
                    gvBeneficiarios.DataSource = LstBeneficiario;
                    gvBeneficiarios.DataBind();
                }
                Session["DatosBene"] = LstBeneficiario;
                chkBeneficiario.Checked = true;
                upBeneficiarios.Visible = true;

            }


            //Generar Consulta de la Linea Seleccionada
            LineaAhorro vLineaAhorro = new LineaAhorro();
            LineaAhorroServices linahorroServicio = new LineaAhorroServices();
            vLineaAhorro = linahorroServicio.ConsultarLineaAhorro(Convert.ToInt64(vAhorroVista.cod_linea_ahorro), (Usuario)Session["usuario"]);

            if (vLineaAhorro.interes_por_cuenta == 0)
            {
                cbInteresCuenta.Enabled = false;
                panelTasa.Enabled = false;
            }
            else
            {
                cbInteresCuenta.Enabled = true;

                if (vLineaAhorro.retencion_por_cuenta == 1)
                {
                    cbRetencion.Enabled = false;

                }
                else
                {
                    cbRetencion.Enabled = true;
                    cbRetencion.Checked = true;
                    panelTasa.Enabled = true;
                    cbInteresCuenta.Checked = true;

                }
            }
            if (vAhorroVista.forma_tasa != null)
            {
                if (!string.IsNullOrEmpty(vAhorroVista.forma_tasa.ToString()))
                    ctlTasaInteres.FormaTasa = HttpUtility.HtmlDecode(vAhorroVista.forma_tasa.ToString().Trim());
                if (!string.IsNullOrEmpty(vAhorroVista.tipo_historico.ToString()))
                    ctlTasaInteres.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(vAhorroVista.tipo_historico.ToString().Trim()));
                if (!string.IsNullOrEmpty(vAhorroVista.desviacion.ToString()))
                    ctlTasaInteres.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(vAhorroVista.desviacion.ToString().Trim()));
                if (!string.IsNullOrEmpty(vAhorroVista.tipo_tasa.ToString()))
                    ctlTasaInteres.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(vAhorroVista.tipo_tasa.ToString().Trim()));
                if (!string.IsNullOrEmpty(vAhorroVista.tasa.ToString()))
                    ctlTasaInteres.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(vAhorroVista.tasa.ToString().Trim()));
            }

            if (vLineaAhorro.requiere_libreta != 1)
            {
                txtNumLibreta.Enabled = false;
                txtDesprendibleDesde.Enabled = false;
                txtDesprendibleHasta.Enabled = false;
            }
            else
            {
                txtNumLibreta.Enabled = false;
                txtDesprendibleDesde.Enabled = false;
                txtDesprendibleHasta.Enabled = false;
            }

            // Consuktar libreta
            String numero_cuenta = Convert.ToString(vAhorroVista.numero_cuenta);
            ELibretas vLibreta = new ELibretas();
            vLibreta = ahorrosServicio.getLibretaByNumeroCuentaService(numero_cuenta, (Usuario)Session["usuario"]);
            Int64 codigo = vLibreta.id_Libreta;
            vLibreta = ahorrosServicio.getLibretaByIdLibretaServices(codigo, (Usuario)Session["usuario"]);

            txtNumLibreta.Text = Convert.ToString(vLibreta.numero_libreta);

            txtDesprendibleDesde.Text = Convert.ToString(vLibreta.desde);
            txtDesprendibleHasta.Text = Convert.ToString(vLibreta.hasta);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    void CargarListas()
    {
        Usuario pUsuario = Usuario;
        try
        {
            ctlTasaInteres.Inicializar();

            LlenarListasDesplegables(TipoLista.Asesor, ddlAsesor);
            LlenarListasDesplegables(TipoLista.LineaAhorro, ddlLineaFiltro);
            ddlLineaFiltro.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

            ddlModalidad.Items.Insert(0, new ListItem("Seleccione un Item", "0"));
            ddlModalidad.Items.Insert(1, new ListItem("INDIVIDUAL", "1"));
            ddlModalidad.Items.Insert(2, new ListItem("CONJUNTA", "2"));
            ddlModalidad.Items.Insert(3, new ListItem("ALTERNA", "3"));
            ddlModalidad.SelectedIndex = 0;
            ddlModalidad.DataBind();

            ddlFormaPago.Items.Insert(0, new ListItem("Caja", "1"));
            ddlFormaPago.Items.Insert(1, new ListItem("Nomina", "2"));
            ddlFormaPago.DataBind();

            ddlPeriodicidad.Inicializar();
            Poblar.PoblarListaDesplegable("OFICINA", " COD_OFICINA,NOMBRE ", " ESTADO = 1 ", " 1 ", ddlOficina, Usuario);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    protected void RegsPag_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList _DropDownList = (DropDownList)sender;
        this.gvDetalle.PageSize = int.Parse(_DropDownList.SelectedValue);
    }

    protected void FechaNacimiento_Changed(object sender, EventArgs e)
    {
        TextBox fechaNacimiento = sender as TextBox;
        TextBox edadBeneficiaro = fechaNacimiento.NamingContainer.NamingContainer.FindControl("txtEdadBen") as TextBox;

        if (fechaNacimiento != null && edadBeneficiaro != null && !string.IsNullOrWhiteSpace(fechaNacimiento.Text))
        {
            DateTimeHelper dateHelper = new DateTimeHelper();
            edadBeneficiaro.Text = dateHelper.DiferenciaEntreDosFechasAños(DateTime.Today, Convert.ToDateTime(fechaNacimiento.Text)).ToString();
        }
    }


    protected void IraPag(object sender, EventArgs e)
    {
        TextBox _IraPag = (TextBox)sender;
        int _NumPag = 0;

        if (int.TryParse(_IraPag.Text.Trim(), out _NumPag) && _NumPag > 0 && _NumPag <= this.gvDetalle.PageCount)
        {
            if (int.TryParse(_IraPag.Text.Trim(), out _NumPag) && _NumPag > 0 && _NumPag <= this.gvDetalle.PageCount)
            {
                this.gvDetalle.PageIndex = _NumPag - 1;
            }
            else
            {
                this.gvDetalle.PageIndex = 0;
            }
        }
        this.gvDetalle.SelectedIndex = -1;
    }
    protected void chkPrincipal_CheckedChanged(object sender, EventArgs e)
    {
        VerError("");
        CheckBoxGrid chkPrincipal = (CheckBoxGrid)sender;
        int rowIndex = Convert.ToInt32(chkPrincipal.CommandArgument);
        string Cod_persona = ((TextBox)gvDetalle.Rows[rowIndex].FindControl("lblcodigo")).Text;

        int EnteTerri = 0;

        if (chkPrincipal != null)
        {
            foreach (GridViewRow rFila in gvDetalle.Rows)
            {
                CheckBoxGrid check = (CheckBoxGrid)rFila.FindControl("chkPrincipal");
                if (rFila.RowIndex == rowIndex)
                {
                    if (string.IsNullOrWhiteSpace(Cod_persona))
                    {
                        VerError("Persona Inexistente");
                        check.Checked = false;
                    }
                    else
                    {
                        // check.Checked = true;
                        AhorroVista vpersona = new AhorroVista();
                        vpersona = ahorrosServicio.ConsultarAfiliacion(Convert.ToString(Cod_persona), (Usuario)Session["usuario"]);
                        String Estado = vpersona.estadopersona;
                        Int64 Persona = Convert.ToInt64(vpersona.cod_persona);
                        if (Persona == 0)
                        {
                            VerError("La persona no se encuentra afiliada");
                            check.Checked = false;
                        }
                        if (Persona > 0 && Estado != "A")
                        {
                            VerError("La persona no es un afiliado activo");
                            check.Checked = false;
                        }

                        string identificacion = ((TextBoxGrid)rFila.FindControl("txtIdentificacion")).Text;
                        ConsultarEmpresaRecaudoPersona(identificacion);
                    }
                }

                if (chkPrincipal != null && check.Checked == true)
                {
                    string cod_per = ((TextBox)rFila.FindControl("lblcodigo")).Text;
                    Xpinn.Contabilidad.Entities.Tercero VPersona = new Xpinn.Contabilidad.Entities.Tercero();
                    Xpinn.Contabilidad.Services.TerceroService TerceroServicio = new Xpinn.Contabilidad.Services.TerceroService();
                    VPersona = TerceroServicio.ConsultarTercero(Convert.ToInt64(cod_per), null, (Usuario)Session["usuario"]);
                    if (VPersona.EnteTerritorial == 1 && check.Checked == true)
                        EnteTerri = EnteTerri + VPersona.EnteTerritorial;
                }
            }

            //Indica si es una entidad territorial
            if (EnteTerri > 0)
            {
                lblEntTerritorial.Visible = true;
                lblEntTerritorial.Text = "Ente Territorial";
                ChkExentaGmf.Checked = true;
                ChkExentaGmf.Enabled = false;
            }
            else
            {
                lblEntTerritorial.Text = "";
                //ChkExentaGmf.Checked = false;
                ChkExentaGmf.Enabled = true;
            }
        }
    }

    void ConsultarEmpresaRecaudoPersona(string identificacion)
    {
        CambioAProductoServices cambioProducService = new CambioAProductoServices();
        List<PersonaEmpresaRecaudo> lstConsulta = cambioProducService.ListarPersonaEmpresaRecaudo(new PersonaEmpresaRecaudo() { identificacion = identificacion }, Usuario);
        ddlEmpresaRecaudo.DataSource = lstConsulta;
        ddlEmpresaRecaudo.DataTextField = "nom_empresa";
        ddlEmpresaRecaudo.DataValueField = "cod_empresa";
        ddlEmpresaRecaudo.DataBind();
    }

    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        ObtenerListaDetalle();

        List<CuentaHabientes> lstDetalle = new List<CuentaHabientes>();

        if (Session["DatosDetalle"] != null)
        {
            lstDetalle = (List<CuentaHabientes>)Session["DatosDetalle"];

            for (int i = 1; i <= 1; i++)
            {
                CuentaHabientes eApert = new CuentaHabientes();
                eApert.idcuenta_habiente = 0;
                eApert.cod_persona = null;
                eApert.principal = null;
                eApert.tipo_firma = 0;
                lstDetalle.Add(eApert);
            }
            gvDetalle.PageIndex = gvDetalle.PageCount;
            gvDetalle.DataSource = lstDetalle;
            gvDetalle.DataBind();

            Session["DatosDetalle"] = lstDetalle;
        }
        else
        {
            InicializarDetalle();
        }
    }

    protected void InicializarDetalle()
    {
        //Creo la lista
        List<CuentaHabientes> lstDetalle = new List<CuentaHabientes>();        

        for (int i = gvDetalle.Rows.Count; i < 3; i++)
        {
            CuentaHabientes eApert = new CuentaHabientes();
            eApert.cod_usuario_ahorro = -1;
            eApert.cod_persona = null;
            eApert.principal = null;
            eApert.conjuncion = "";
            lstDetalle.Add(eApert);
        }
        gvDetalle.DataSource = lstDetalle;
        gvDetalle.DataBind();
        Session["DatosDetalle"] = lstDetalle;
    }

    protected void gvDetalle_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {
        VerError("");
        TextBoxGrid txtIdentificacion = (TextBoxGrid)sender;
        int rowIndex = Convert.ToInt32(txtIdentificacion.CommandArgument);

        TextBox lblcod_persona = (TextBox)gvDetalle.Rows[rowIndex].FindControl("lblcodigo");
        TextBox lblNombre = (TextBox)gvDetalle.Rows[rowIndex].FindControl("lblNombre");
        TextBox lblApellidos = (TextBox)gvDetalle.Rows[rowIndex].FindControl("lblApellidos");
        TextBox lblCiudad = (TextBox)gvDetalle.Rows[rowIndex].FindControl("lblCiudad");
        TextBox lblDireccion = (TextBox)gvDetalle.Rows[rowIndex].FindControl("lblDireccion");
        TextBox lbltelefono = (TextBox)gvDetalle.Rows[rowIndex].FindControl("lbltelefono");
        CheckBox chkPrincipal = (CheckBox)gvDetalle.Rows[rowIndex].FindControl("chkPrincipal");

        Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Xpinn.FabricaCreditos.Entities.Persona1 DataPersona = new Xpinn.FabricaCreditos.Entities.Persona1();
        DataPersona.identificacion = txtIdentificacion.Text;
        DataPersona = Persona1Servicio.ConsultaDatosPersona(Convert.ToString(txtIdentificacion.Text), (Usuario)Session["usuario"]);

        if (DataPersona.cod_persona != 0 && DataPersona.cod_persona != null)
        {
            if (DataPersona.cod_persona != 0 && DataPersona.cod_persona != null)
                lblcod_persona.Text = DataPersona.cod_persona.ToString();

            if (DataPersona.nombres != "" && DataPersona.nombres != null)
                lblNombre.Text = DataPersona.nombres;

            if (DataPersona.apellidos != "" && DataPersona.apellidos != null)
                lblApellidos.Text = DataPersona.apellidos;

            if (DataPersona.nomciudad_resid != "" && DataPersona.nomciudad_resid != null)
                lblCiudad.Text = DataPersona.nomciudad_resid;

            if (DataPersona.direccion != "" && DataPersona.direccion != null)
                lblDireccion.Text = DataPersona.direccion;

            if (DataPersona.telefono != "" && DataPersona.telefono != null)
                lbltelefono.Text = DataPersona.telefono;
        }
        else
        {
            lblcod_persona.Text = ""; lblNombre.Text = ""; lblApellidos.Text = "";
            lblCiudad.Text = ""; lblDireccion.Text = ""; lbltelefono.Text = "";
            chkPrincipal.Checked = false;
        }
    }
    protected void gvDetalle_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvDetalle.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaDetalle();

        List<CuentaHabientes> LstDeta;
        LstDeta = (List<CuentaHabientes>)Session["DatosDetalle"];

        if (conseID > 0)
        {
            try
            {
                foreach (CuentaHabientes Deta in LstDeta)
                {
                    if (Deta.idcuenta_habiente == conseID)
                    {
                        ahorrosServicio.EliminarCtaHabiente(conseID, (Usuario)Session["usuario"]);
                        LstDeta.Remove(Deta);
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
            LstDeta.RemoveAt((gvDetalle.PageIndex * gvDetalle.PageSize) + e.RowIndex);
        }

        gvDetalle.DataSourceID = null;
        gvDetalle.DataBind();

        gvDetalle.DataSource = LstDeta;
        gvDetalle.DataBind();

        Session["DatosDetalle"] = LstDeta;
    }


    protected List<CuentaHabientes> ObtenerListaDetalle()
    {        
        List<CuentaHabientes> lstDetalle = new List<CuentaHabientes>();
        List<CuentaHabientes> lista = new List<CuentaHabientes>();        

        foreach (GridViewRow rfila in gvDetalle.Rows)
        {
            CuentaHabientes eDeta = new CuentaHabientes();

            TextBox lblcod_cuentahabiente = (TextBox)rfila.FindControl("lblcod_cuentahabiente");
            if (lblcod_cuentahabiente != null)
                eDeta.idcuenta_habiente = Convert.ToInt64(lblcod_cuentahabiente.Text);


            TextBoxGrid txtIdentificacion = (TextBoxGrid)rfila.FindControl("txtIdentificacion");
            if (txtIdentificacion != null)
                eDeta.identificacion = txtIdentificacion.Text;

            TextBox lblcod_persona = (TextBox)rfila.FindControl("lblcodigo");
            if (lblcod_persona.Text != "")
                eDeta.cod_persona = Convert.ToInt64(lblcod_persona.Text);

            TextBox lblNombre = (TextBox)rfila.FindControl("lblNombre");
            if (lblNombre.Text != "")
                eDeta.nombres = lblNombre.Text;

            TextBox lblApellidos = (TextBox)rfila.FindControl("lblApellidos");
            if (lblApellidos.Text != "")
                eDeta.apellidos = lblApellidos.Text;

            TextBox lblCiudad = (TextBox)rfila.FindControl("lblCiudad");
            if (lblCiudad.Text != "")
                eDeta.ciudad = lblCiudad.Text;

            TextBox lblDireccion = (TextBox)rfila.FindControl("lblDireccion");
            if (lblDireccion.Text != "")
                eDeta.direccion = lblDireccion.Text;

            TextBox lbltelefono = (TextBox)rfila.FindControl("lbltelefono");
            if (lbltelefono.Text != "")
                eDeta.telefono = lbltelefono.Text;

            CheckBoxGrid chkPrincipal = (CheckBoxGrid)rfila.FindControl("chkPrincipal");
            if (chkPrincipal.Checked)
                eDeta.principal = 1;
            else
                eDeta.principal = 0;

            if (ddlModalidad.Text == "CONJUNTA")
            {
                DropDownListGrid ddlConjuncion = (DropDownListGrid)rfila.FindControl("ddlConjuncion");
                if (ddlConjuncion.SelectedIndex != 0)
                    eDeta.conjuncion = ddlConjuncion.SelectedValue;
                else
                    eDeta.conjuncion = null;
            }
            else
                eDeta.conjuncion = null;

            lista.Add(eDeta);
            Session["DatosDetalle"] = lista;

            if (eDeta.cod_persona != 0 && eDeta.cod_persona != null)
            {
                lstDetalle.Add(eDeta);
                Session["DTAPERTURA"] = lstDetalle; // CAPTURA DATOS PARA IMPRESION
            }
        }

        return lstDetalle;
    }

    #region Titulares

    /// <summary>
    /// Método para instar un detalle en blanco para cuando la grilla no tiene datos
    /// </summary>
    /// <param name="consecutivo"></param>
    private void CrearDetalleInicial()
    {
        List<CuentaHabientes> LstCuentaHabientes = new List<CuentaHabientes>();
        for (int i = 1; i <= 2; i++)
        {
            CuentaHabientes pCuentaHabientes = new CuentaHabientes();
            pCuentaHabientes.cod_persona = null;
            pCuentaHabientes.identificacion = "";
            pCuentaHabientes.nombre = "";
            pCuentaHabientes.tipo_firma = null;

            LstCuentaHabientes.Add(pCuentaHabientes);
        }
        gvDetalle.DataSource = LstCuentaHabientes;
        gvDetalle.DataBind();

        Session["CuentaHabientes"] = LstCuentaHabientes;

    }

    /// <summary>
    /// Método para cambio de página
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDetMovs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //ObtenerDetalleComprobante(false);
            gvDetalle.PageIndex = e.NewPageIndex;
            ActualizarDetalle();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoPrograma, "gvDetMovs_PageIndexChanging", ex);
        }
    }

    protected void ActualizarDetalle()
    {
        //List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
        //LstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];
        //gvDetalle.DataSource = LstDetalleComprobante;
        //gvDetalle.DataBind();
    }

    protected void gvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlConjuncion = (DropDownListGrid)e.Row.FindControl("ddlConjuncion");
            if (ddlConjuncion != null)
            {
                ddlConjuncion.Items.Insert(0, new ListItem("Seleccione un item", "0"));
                ddlConjuncion.Items.Insert(1, new ListItem("Y", "Y"));
                ddlConjuncion.Items.Insert(2, new ListItem("O", "O"));
            }

            Label lblConjuncion = (Label)e.Row.FindControl("lblConjuncion");
            if (lblConjuncion != null)
                ddlConjuncion.SelectedValue = lblConjuncion.Text;

            TextBoxGrid txtIdentificacion = (TextBoxGrid)e.Row.FindControl("txtIdentificacion");
            if (txtIdentificacion != null)
                txtIdentificacion.TextChanged += txtIdentificacion_TextChanged;

            BusquedaRapida ctlListadoPersonas = (BusquedaRapida)e.Row.FindControl("ctlListadoPersonas");
            if (ctlListadoPersonas != null)
                ctlListadoPersonas.eventotxtIdentificacion_TextChanged += txtIdentificacion_TextChanged;
        }
    }
    protected void btnListadoPersona_Click(object sender, EventArgs e)
    {
        ButtonGrid btnListadoPersonas = (ButtonGrid)sender;
        if (btnListadoPersonas != null)
        {
            int rowIndex = Convert.ToInt32(btnListadoPersonas.CommandArgument);
            BusquedaRapida ctlListadoPer = (BusquedaRapida)gvDetalle.Rows[rowIndex].FindControl("ctlListadoPersonas");
            ctlListadoPer.Motrar(true, "lblcodigo", "txtIdentificacion", "", "lblNombre", "lblApellidos", "lblDireccion", "lbltelefono", "lblCiudad");
        }
    }

    #endregion  


    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFormaPago.SelectedValue.ToEnum<TipoFormaPago>() == TipoFormaPago.Nomina)
        {
            pnlEmpresaRecaudo.Visible = true;
        }
        else
        {
            pnlEmpresaRecaudo.Visible = false;
        }
    }
    protected void ddlLineaAhorro_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Generar Consulta de la Linea Seleccionada
        if (!string.IsNullOrWhiteSpace(ddlLineaAhorro.Value) || Session["solicitudProducto"] != null)
        {
            Int64 linea = 0;
            AhorroVista solicitud = new AhorroVista();
            if(Session["solicitudProducto"] != null)
            {
                solicitud = Session["solicitudProducto"] as AhorroVista;
                linea = Convert.ToInt64(solicitud.cod_linea_ahorro);
            }
            if (!string.IsNullOrWhiteSpace(ddlLineaAhorro.Value))
                linea = Convert.ToInt64(ddlLineaAhorro.Value);
             
            LineaAhorro vLineaAhorro = new LineaAhorro();
            LineaAhorroServices linahorroServicio = new LineaAhorroServices();
            vLineaAhorro = linahorroServicio.ConsultarLineaAhorro(linea, Usuario);

            if (vLineaAhorro.interes_por_cuenta == 0)
            {
                cbInteresCuenta.Enabled = false;
                panelTasa.Enabled = false;
            }
            else
            {
                cbInteresCuenta.Enabled = true;

                if (vLineaAhorro.retencion_por_cuenta == 1)
                {
                    cbRetencion.Enabled = false;

                }
                else
                {
                    cbRetencion.Enabled = true;
                    cbRetencion.Checked = true;
                    panelTasa.Enabled = true;
                    cbInteresCuenta.Checked = true;
                }
            }
            if (vLineaAhorro.forma_tasa != null)
            {
                //ctlTasaInteres.Inicializar();
                if (!string.IsNullOrEmpty(vLineaAhorro.forma_tasa.ToString()))
                    ctlTasaInteres.FormaTasa = HttpUtility.HtmlDecode(vLineaAhorro.forma_tasa.ToString().Trim());
                if (!string.IsNullOrEmpty(vLineaAhorro.tipo_historico.ToString()))
                    ctlTasaInteres.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(vLineaAhorro.tipo_historico.ToString().Trim()));
                if (!string.IsNullOrEmpty(vLineaAhorro.desviacion.ToString()))
                    ctlTasaInteres.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(vLineaAhorro.desviacion.ToString().Trim()));
                if (!string.IsNullOrEmpty(vLineaAhorro.tipo_tasa.ToString()))
                    ctlTasaInteres.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(vLineaAhorro.tipo_tasa.ToString().Trim()));
                if (!string.IsNullOrEmpty(vLineaAhorro.tasa.ToString()))
                    ctlTasaInteres.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(vLineaAhorro.tasa.ToString().Trim()));
            }
        }
    }

    protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {

        //if (ddlModalidad.SelectedItem.Text == "1")
        //{
        //    for (int i = 0; i < gvDetalle.Rows.Count; i++)
        //    {
        //        gvDetalle.Columns[10].Visible = true;
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < gvDetalle.Rows.Count; i++)
        //    {
        //        gvDetalle.Columns[10].Visible = false;
        //    }
        //}
        btnAddRow.Visible = true;
        if (ddlModalidad.SelectedItem.Text == "INDIVIDUAL")
            btnAddRow.Visible = false;

        //if (ddlModalidad.SelectedValue == "1")
        //{
        //    gvDetalle.Visible = false;
        //    btnAddRow.Visible = false;
        //    LblTitulares.Visible = false;
        //    panelPersona.Visible = true;
        //}
        //else
        //{
        //    gvDetalle.Visible = true;
        //    btnAddRow.Visible = true;
        //    LblTitulares.Visible = true;
        //    panelPersona.Visible = false;
        //}
    }

    //protected void gvListaTitulares_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    VerError("");
    //    if (ddlLineaFiltro.SelectedIndex != 0)
    //    {
    //        // Determinar la identificacion
    //        GridView gvListaAFiliados = (GridView)sender;
    //        Int64  cod_persona = Convert.ToInt64(gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[1].Text);
    //        String tipo_persona = gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[2].Text;
    //        String identificacion = gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[3].Text;
    //        String nombre = "";
    //        Int32 TipoIdent = Convert.ToInt32(gvListaAFiliados.DataKeys[gvListaAFiliados.SelectedRow.RowIndex].Values[1].ToString());

    //        if (tipo_persona == "Natural")
    //        {
    //            nombre = gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[5].Text + " " +
    //                     gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[6].Text + " " +
    //                     gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[7].Text + " " +
    //                     gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[8].Text;                
    //        }
    //        else
    //        {
    //            nombre = gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[9].Text;
    //        }
    //        ctlPersona.AdicionarPersona(identificacion, cod_persona, nombre, TipoIdent);
    //        ddlLineaAhorro.Value = ddlLineaFiltro.SelectedValue;
    //        ddlLineaAhorro.Habilitado = false;

    //        //Generar Consulta de la Linea Seleccionada
    //        LineaAhorro vLineaAhorro = new LineaAhorro();
    //        LineaAhorroServices linahorroServicio = new LineaAhorroServices();
    //        vLineaAhorro = linahorroServicio.ConsultarLineaAhorro(Convert.ToInt64(ddlLineaAhorro.Value), (Usuario)Session["usuario"]);

    //        if (vLineaAhorro.interes_por_cuenta != 1)
    //        {
    //            panelTasa.Enabled = false;
    //        }
    //        else
    //        {
    //            panelTasa.Enabled = true;
    //            cbInteresCuenta.Checked = true;
    //            if (vLineaAhorro.forma_tasa != null)
    //            {
    //                ctlTasaInteres.FormaTasa = vLineaAhorro.forma_tasa.ToString();
    //                if (vLineaAhorro.tipo_historico != null)
    //                    ctlTasaInteres.TipoHistorico = Convert.ToInt32(vLineaAhorro.tipo_historico);
    //                if (vLineaAhorro.desviacion != null)
    //                    ctlTasaInteres.Desviacion = Convert.ToDecimal(vLineaAhorro.desviacion);
    //                if (vLineaAhorro.tipo_tasa != null)
    //                    ctlTasaInteres.TipoTasa = Convert.ToInt32(vLineaAhorro.tipo_tasa);
    //                if (vLineaAhorro.tasa != null)
    //                    ctlTasaInteres.Tasa = Convert.ToDecimal(vLineaAhorro.tasa);
    //            }
    //        }

    //        if (vLineaAhorro.requiere_libreta != 1)
    //        {
    //            txtNumLibreta.Enabled = false;
    //            txtDesprendibleDesde.Enabled = false;
    //            txtDesprendibleHasta.Enabled = false;
    //        }
    //        else
    //        {
    //            txtNumLibreta.Enabled = true;
    //            txtDesprendibleDesde.Enabled = false;
    //            txtDesprendibleHasta.Enabled = false;
    //        }

    //        if (ctlPersona.DatosPersona() == true)
    //        {                
    //            // Habilitar la barra de herramientas
    //            Site toolBar = (Site)this.Master; 
    //            toolBar.MostrarGuardar(true);
    //            toolBar.MostrarConsultar(false);
    //            // Ir a la siguiente página
    //            mvAhorroVista.ActiveViewIndex = 1;
    //        }
    //        else
    //        {
    //            VerError("No se encontraron datos de las persona");
    //        }
    //    }
    //    else
    //    {
    //        VerError("Seleccion una Linea");
    //    }
    //}

    protected void txtNumeroCuenta_TextChanged(object sender, EventArgs e)
    {
        /*
        if (txtNumeroCuenta.Text != "")
        {
            txtDigVer.Text = CalcularDigitoVerificacion(txtNumeroCuenta.Text);
        }
        else
        {
            txtDigVer.Text = "0";
        }
         * */
    }

    protected void btnCargarImagen_Click(object sender, EventArgs e)
    {
        Boolean FileOK = false;
        Boolean FileSaved = false;

        if (fuFoto.HasFile)
        {
            Session["WorkingImage"] = fuFoto.FileName;
            String FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();

            String[] allowedExtensions = { ".png", ".jpeg", ".jpg", ".gif", ".bmp", ".pdf" };

            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (FileExtension == allowedExtensions[i])
                {
                    FileOK = true;
                }
            }

        }


        if (FileOK)
        {
            try
            {

                if (Path.GetExtension(Session["WorkingImage"].ToString()).ToLower() == ".pdf")
                {
                    fuFoto.PostedFile.SaveAs(Server.MapPath("Pdf\\") + Session["WorkingImage"]);
                    String nombre = Path.GetFileNameWithoutExtension(Session["WorkingImage"].ToString());
                    String Folder = System.Web.Hosting.HostingEnvironment.MapPath("~/Page/AhorrosVista/CuentasAhorro/Images/" + nombre + ".png");
                    var info = new System.IO.FileInfo(Folder);
                    if (info.Exists.Equals(false))
                    {
                        GhostscriptPngDevice img = new GhostscriptPngDevice(Ghostscript.NET.GhostscriptPngDeviceType.Png16m);
                        img.GraphicsAlphaBits = Ghostscript.NET.GhostscriptImageDeviceAlphaBits.V_4;
                        img.TextAlphaBits = Ghostscript.NET.GhostscriptImageDeviceAlphaBits.V_4;
                        img.ResolutionXY = new Ghostscript.NET.GhostscriptImageDeviceResolution(200, 200);
                        img.InputFiles.Add(Server.MapPath("Pdf\\") + Session["WorkingImage"]);
                        img.Pdf.FirstPage = 1;
                        img.Pdf.LastPage = 1;
                        img.PostScript = string.Empty;
                        img.OutputPath = Folder;
                        img.Process();

                    }
                    Session["WorkingImage"] = nombre + ".png";
                }
                else
                {
                    fuFoto.PostedFile.SaveAs(Server.MapPath("Images\\") + Session["WorkingImage"]);
                }

                FileSaved = true;
                hdFileName.Value = Session["WorkingImage"].ToString();
            }
            catch (Exception ex)
            {

                FileSaved = false;
            }

        }
        else
        {


        }

        if (FileSaved)
        {

            imgFoto.ImageUrl = "Images/" + Session["WorkingImage"].ToString();

            imgFoto.Visible = true;



        }
    }


    private void cargarFotografia()
    {
        /*Obtenemos el nombre y la extension del archivo*/
        String fileName = Path.GetFileName(this.fuFoto.PostedFile.FileName);
        String extension = Path.GetExtension(this.fuFoto.PostedFile.FileName).ToLower();
        try
        {
            if (extension != ".png" && extension != ".jpg" && extension != ".bmp")
            {
                VerError("El archivo ingresado no es una imagen");
            }
            else
            {
                /*Se guarda la imagen en el servidor*/
                fuFoto.PostedFile.SaveAs(Server.MapPath("Images\\") + fileName);
                /*Obtenemos el nombre temporal de la imagen con la siguiente funcion*/
                String nombreImgServer = getNombreImagenServidor(extension);
                hdFileName.Value = nombreImgServer;
                /*Cambiamos el nombre de la imagen por el nuevo*/
                File.Move(Server.MapPath("Images\\") + fileName, Server.MapPath("Images\\" + nombreImgServer));
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }
    private void mostrarImagen()
    {
        /*Muestra la imagen como un thumbnail*/
        System.Drawing.Image objImage = null, objThumbnail = null;
        Int32 width, height;
        String fileName = Server.MapPath("Images\\") + Path.GetFileName(this.hdFileName.Value);
        Stream stream = null;
        try
        {
            /*Se guarda la imagen en un stream para despues colocarla en un objeto para que la imagen no quede abierta en el servidor*/
            stream = File.OpenRead(fileName);
            objImage = System.Drawing.Image.FromStream(stream);
            width = 100;
            height = objImage.Height / (objImage.Width / width);
            this.Response.Clear();
            /*Se crea el thumbnail y se muestra en la imagen*/

            objThumbnail = objImage.GetThumbnailImage(width, height, null, IntPtr.Zero);
            objThumbnail.Save(Server.MapPath("Images\\") + "thumb_" + this.hdFileName.Value, ImageFormat.Jpeg);
            imgFoto.Visible = true;
            String nombreImgThumb = "thumb_" + this.hdFileName.Value;
            this.hdFileNameThumb.Value = nombreImgThumb;

            imgFoto.ImageUrl = "Images\\" + nombreImgThumb;

        }
        catch (Exception ex)
        {
            VerError("No pudro abrir archivo con imagen de la persona " + ex.Message);
        }
        finally
        {
            /*Limpiamos los objetos*/
            objImage.Dispose();
            objThumbnail.Dispose();
            stream.Dispose();
            objImage = null;
            objThumbnail = null;
            stream = null;
        }
    }

    public String getNombreImagenServidor(String extension)
    {
        /*Devuelve el nombre temporal de la imagen*/
        Random nRandom = new Random();
        String nr = Convert.ToString(nRandom.Next(0, 32000));
        String nombre = nr + "_" + DateTime.Today.ToString("ddMMyyyy") + extension;
        nRandom = null;
        return nombre;
    }
    protected void linkBt_Click(object sender, EventArgs e)
    {
        try
        {
            /*Este evento se generara desde la funcion javascript asignarRutaFoto*/
            if (fuFoto.HasFile == true)
            {
                cargarFotografia();
                mostrarImagen();
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }
    public string Bytes_A_Archivo(string Cuenta, Byte[] ImgBytes)
    {
        Stream stream = null;
        string fileName = Server.MapPath("Images\\") + Path.GetFileName(Cuenta + ".jpg");
        if (ImgBytes != null)
        {
            try
            {
                // Guardar imagen en un archivo
                stream = File.OpenWrite(fileName);
                foreach (byte b in ImgBytes)
                {
                    stream.WriteByte(b);
                }
                stream.Close();
                this.hdFileName.Value = Path.GetFileName(Cuenta + ".jpg");
                mostrarImagen();
            }
            finally
            {
                /*Limpiamos los objetos*/
                stream.Dispose();
                stream = null;
            }
        }
        return fileName;
    }



    protected void chkBeneficiario_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkbeneficiario = (CheckBox)sender;
        if (chkbeneficiario.Checked)
            upBeneficiarios.Visible = true;
        else
            upBeneficiarios.Visible = false;
    }
    protected string FormatoFecha()
    {
        Configuracion conf = new Configuracion();
        return conf.ObtenerFormatoFecha();
    }

    protected void gvBeneficiarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvBeneficiarios.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaBeneficiariosAhorroVista();

        List<Beneficiario> LstBene;
        LstBene = (List<Beneficiario>)Session["DatosBene"];

        if (conseID > 0)
        {
            try
            {
                foreach (Beneficiario bene in LstBene)
                {
                    if (bene.idbeneficiario == conseID)
                    {
                        BeneficiarioServicio.EliminarBeneficiario(conseID, (Usuario)Session["usuario"]);
                        LstBene.Remove(bene);
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
            LstBene.RemoveAt((gvBeneficiarios.PageIndex * gvBeneficiarios.PageSize) + e.RowIndex);
        }

        gvBeneficiarios.DataSource = LstBene;
        gvBeneficiarios.DataBind();

        Session["DatosBene"] = LstBene;
    }

    protected void gvBeneficiarios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlSexo = (DropDownList)e.Row.FindControl("ddlsexo");
            DropDownList ddlParentezco = (DropDownList)e.Row.FindControl("ddlParentezco");
            if (ddlParentezco != null)
            {
                Beneficiario Ben = new Beneficiario();
                ddlParentezco.DataSource = BeneficiarioServicio.ListarParentesco(Ben, (Usuario)Session["usuario"]);
                ddlParentezco.DataTextField = "DESCRIPCION";
                ddlParentezco.DataValueField = "CODPARENTESCO";
                ddlParentezco.Items.Insert(0, new ListItem("<Seleccione un item>", "0"));
                ddlParentezco.DataBind();

            }

            Label lblParentezco = (Label)e.Row.FindControl("lblParentezco");
            if (lblParentezco.Text != null)
                ddlParentezco.SelectedValue = lblParentezco.Text;

            Label lblSexo = (Label)e.Row.FindControl("lblSexo");
            if (lblSexo.Text != null)
                ddlSexo.SelectedValue = lblSexo.Text;
        }
    }

    protected List<Beneficiario> ObtenerListaBeneficiariosAhorroVista()
    {
        List<Beneficiario> lstBeneficiarios = new List<Beneficiario>();
        List<Beneficiario> lista = new List<Beneficiario>();

        foreach (GridViewRow rfila in gvBeneficiarios.Rows)
        {
            Beneficiario eBenef = new Beneficiario();
            Label lblidbeneficiario = (Label)rfila.FindControl("lblidbeneficiario");
            if (lblidbeneficiario != null)
                eBenef.idbeneficiario = Convert.ToInt64(lblidbeneficiario.Text);

            DropDownListGrid ddlParentezco = (DropDownListGrid)rfila.FindControl("ddlParentezco");
            if (ddlParentezco.SelectedValue != null || ddlParentezco.SelectedIndex != 0)
                eBenef.parentesco = Convert.ToInt32(ddlParentezco.SelectedValue);

            DropDownList ddlSexo = (DropDownList)rfila.FindControl("ddlsexo");
            if (ddlSexo.SelectedValue != null)
                eBenef.sexo = Convert.ToString(ddlSexo.SelectedValue);

            TextBox txtIdentificacion = (TextBox)rfila.FindControl("txtIdentificacion");
            if (txtIdentificacion != null)
                eBenef.identificacion_ben = Convert.ToString(txtIdentificacion.Text);

            TextBox txtEdadBen = (TextBox)rfila.FindControl("txtEdadBen");
            if (txtEdadBen != null)
            {
                if (txtEdadBen.Text != "")
                {
                    eBenef.edad = Convert.ToInt32(txtEdadBen.Text);
                }
            }
            TextBox txtNombres = (TextBox)rfila.FindControl("txtNombres");
            if (txtNombres != null)
                eBenef.nombre_ben = Convert.ToString(txtNombres.Text);

            fechaeditable txtFechaNacimientoBen = (fechaeditable)rfila.FindControl("txtFechaNacimientoBen");
            if (txtFechaNacimientoBen != null)
                if (txtFechaNacimientoBen.Texto != "")
                    eBenef.fecha_nacimiento_ben = txtFechaNacimientoBen.ToDateTime;
                else
                    eBenef.fecha_nacimiento_ben = null;
            else
                eBenef.fecha_nacimiento_ben = null;
            decimalesGridRow txtPorcentaje = (decimalesGridRow)rfila.FindControl("txtPorcentaje");
            if (txtPorcentaje != null)
                eBenef.porcentaje_ben = Convert.ToDecimal(txtPorcentaje.Text);

            lista.Add(eBenef);
            Session["DatosBene"] = lista;

            if (eBenef.identificacion_ben.Trim() != "" && eBenef.nombre_ben.Trim() != null)
            {
                lstBeneficiarios.Add(eBenef);
            }
        }
        return lstBeneficiarios;
    }

    protected void btnAddRowBeneficio_Click(object sender, EventArgs e)
    {
        Session["DatosBene"] = null;
        ObtenerListaBeneficiariosAhorroVista();

        List<Beneficiario> lstBene = new List<Beneficiario>();

        if (Session["DatosBene"] != null)
        {
            lstBene = (List<Beneficiario>)Session["DatosBene"];

            for (int i = 1; i <= 1; i++)
            {
                Beneficiario eBenef = new Beneficiario();
                eBenef.idbeneficiario = -1;
                eBenef.nombre = "";
                eBenef.identificacion_ben = "";
                eBenef.tipo_identificacion_ben = null;
                eBenef.nombre_ben = "";
                eBenef.fecha_nacimiento_ben = null;
                eBenef.parentesco = null;
                eBenef.porcentaje_ben = null;
                eBenef.edad = null;
                eBenef.sexo = null;
                lstBene.Add(eBenef);
            }
            gvBeneficiarios.DataSource = lstBene;
            gvBeneficiarios.DataBind();

            Session["DatosBene"] = lstBene;
        }
        else if (Session["solicitudProducto"] != null)
        {
            AhorroVista solicitud = new AhorroVista();
            solicitud = Session["solicitudProducto"] as AhorroVista;
            lstBene = new List<Beneficiario>();

            for (int i = 1; i <= 1; i++)
            {
                Beneficiario eBenef = new Beneficiario();
                eBenef.idbeneficiario = -1;
                eBenef.nombre = solicitud.nombres_ben;
                eBenef.identificacion_ben = solicitud.identificacion_ben;
                eBenef.tipo_identificacion_ben = null;
                eBenef.nombre_ben = solicitud.nombres_ben;
                eBenef.fecha_nacimiento_ben = solicitud.fecha_nacimiento_ben;
                eBenef.parentesco = solicitud.parentesco_ben;
                eBenef.porcentaje_ben = 100;
                DateTimeHelper dateHelper = new DateTimeHelper();
                eBenef.edad = Convert.ToInt32(dateHelper.DiferenciaEntreDosFechasAños(DateTime.Today, solicitud.fecha_nacimiento_ben).ToString());
                eBenef.sexo = null;
                lstBene.Add(eBenef);
            }
            gvBeneficiarios.DataSource = lstBene;
            gvBeneficiarios.DataBind();

            Session["DatosBene"] = lstBene;
        }
        else{
            lstBene = new List<Beneficiario>(); ;

            for (int i = 1; i <= 1; i++)
            {
                Beneficiario eBenef = new Beneficiario();
                eBenef.idbeneficiario = -1;
                eBenef.nombre = "";
                eBenef.identificacion_ben = "";
                eBenef.tipo_identificacion_ben = null;
                eBenef.nombre_ben = "";
                eBenef.fecha_nacimiento_ben = null;
                eBenef.parentesco = null;
                eBenef.porcentaje_ben = null;
                eBenef.edad = null;
                eBenef.sexo = null;
                lstBene.Add(eBenef);
            }
            gvBeneficiarios.DataSource = lstBene;
            gvBeneficiarios.DataBind();

            Session["DatosBene"] = lstBene;
        }
    }

    public void cargarDatosSolicitud()
    {
        //Creo la lista
        List<CuentaHabientes> lstDetalle = new List<CuentaHabientes>();
        int EnteTerri = 0;
        //Si tiene datos de solicitud los carga primero
        if (Session["solicitudProducto"] != null)
        {
            AhorroVista solicitud = new AhorroVista();
            solicitud = Session["solicitudProducto"] as AhorroVista;

            Xpinn.FabricaCreditos.Services.Persona1Service personaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 persona = new Xpinn.FabricaCreditos.Entities.Persona1();
            persona.cod_persona = Convert.ToInt64(solicitud.cod_persona);
            persona.identificacion = solicitud.identificacion;
            List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = personaService.ListadoPersonas1(persona, (Usuario)Session["usuario"]);
            persona = lstConsulta.ElementAt(0);

            CuentaHabientes eDeta = new CuentaHabientes();

            eDeta.identificacion = persona.identificacion;
            eDeta.cod_persona = persona.cod_persona;
            eDeta.nombres = persona.nombres;
            eDeta.apellidos = persona.apellidos;
            eDeta.ciudad = persona.nomciudad_resid;
            eDeta.direccion = persona.direccion;
            eDeta.telefono = persona.telefono;
            eDeta.principal = 1;
            eDeta.conjuncion = null;
            lstDetalle.Add(eDeta);

            //Carga los demás datos
            try
            {
                ddlLineaFiltro.SelectedValue = Convert.ToString(solicitud.cod_linea_ahorro);
                ddlLineaAhorro.Value = solicitud.cod_linea_ahorro.ToString();
                ddlFormaPago.SelectedValue = Convert.ToString(solicitud.cod_forma_pago);
                txtCuota.Text = Convert.ToString(solicitud.valor_cuota).Replace(".","");
                ddlPeriodicidad.cod_periodicidad = solicitud.cod_periodicidad;
                                    
                if (!string.IsNullOrEmpty(solicitud.identificacion_ben))
                {
                    chkBeneficiario.Checked = true;
                    btnAddRowBeneficio_Click(new object(), new EventArgs());
                    upBeneficiarios.Visible = true;
                }
                ddlLineaAhorro_SelectedIndexChanged(new object(), new EventArgs());
                ddlDestinacion.Value = "1";

            }
            catch (Exception ex)
            {
            }
        }

        if (lstDetalle.Count > 0)
        {
            gvDetalle.Visible = true;
            gvDetalle.DataSource = lstDetalle;
            gvDetalle.DataBind();
            Session["DatosDetalle"] = lstDetalle;

            string identificacion = lstDetalle.Where(x => x.principal == 1).Select(x => x.identificacion).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(identificacion))
            {
                ConsultarEmpresaRecaudoPersona(identificacion);
            }

            Int64? cod_per = lstDetalle.Where(x => x.principal == 1).Select(x => x.cod_persona).FirstOrDefault();
            if (cod_per != 0)
            {
                Xpinn.Contabilidad.Entities.Tercero VPersona = new Xpinn.Contabilidad.Entities.Tercero();
                Xpinn.Contabilidad.Services.TerceroService TerceroServicio = new Xpinn.Contabilidad.Services.TerceroService();
                VPersona = TerceroServicio.ConsultarTercero(Convert.ToInt64(cod_per), null, (Usuario)Session["usuario"]);

                EnteTerri = EnteTerri + VPersona.EnteTerritorial;
            }

            //Indica si es una entidad territorial
            if (EnteTerri > 0)
            {
                lblEntTerritorial.Visible = true;
                lblEntTerritorial.Text = "Ente Territorial";
                ChkExentaGmf.Checked = true;
                ChkExentaGmf.Enabled = false;
            }
            else
            {
                lblEntTerritorial.Text = "";
                //ChkExentaGmf.Checked = false;
                ChkExentaGmf.Enabled = true;
            }
            
        }
    }

    public void actualizarSolicitud(long cod_persona)
    {
        if (Session["solicitudProducto"] != null)
        {
            AhorroVista solicitud = Session["solicitudProducto"] as AhorroVista;
            solicitud.estado_modificacion = "1"; // aprobando solicitud                    
            ahorrosServicio.ModificarEstadoSolicitudProducto(solicitud, (Usuario)Session["usuario"]);
            Session["solicitudProducto"] = null;

            Xpinn.Comun.Services.Formato_NotificacionService COServices = new Xpinn.Comun.Services.Formato_NotificacionService();
            Xpinn.Comun.Entities.Formato_Notificacion noti = new Xpinn.Comun.Entities.Formato_Notificacion(Convert.ToInt32(cod_persona), 17, "nombreProducto;Ahorro a la vista");
            COServices.SendEmailPerson(noti, (Usuario)Session["usuario"]);
        }
    }

    private class GhostscriptImageDeviceResolution
    {
        private int v1;
        private int v2;

        public GhostscriptImageDeviceResolution(int v1, int v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
    }
}