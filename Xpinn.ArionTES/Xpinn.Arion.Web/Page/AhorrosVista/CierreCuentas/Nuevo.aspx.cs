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

partial class Nuevo : GlobalWeb
{
    private Xpinn.Ahorros.Services.AhorroVistaServices ahorrosServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();


    PoblarListas Poblar = new PoblarListas();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ahorrosServicio.CodigoProgramaCie + ".id"] != null)
                VisualizarOpciones(ahorrosServicio.CodigoProgramaCie, "E");
            else
                VisualizarOpciones(ahorrosServicio.CodigoProgramaCie, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.MostrarConsultar(false);
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            txtFechaCierre.eventoCambiar += txtFecha_textchange;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaCie, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            //CrearDetalleInicial();
            Session["DatosDetalle"] = null;
            if (!IsPostBack)
            {
                mvAhorroVista.ActiveViewIndex = 0;
                txtNumeroCuenta.Enabled = false;
                CargarListas();
                txtFechaCierre.Text = Convert.ToString(DateTime.Now);
                if (Session[ahorrosServicio.CodigoProgramaCie + ".id"] != null)
                {
                    idObjeto = Session[ahorrosServicio.CodigoProgramaCie + ".id"].ToString();
                    Session.Remove(ahorrosServicio.CodigoProgramaCie + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtNumeroCuenta.Text = "";
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaCie, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }
    Boolean ValidarDatos()
    {
        if (txtFechaCierre.Texto == "")
        {
            VerError("Ingrese la fecha de Cierre");
            return false;
        }

        if (txtNumeroCuenta.Visible == true)
        {
            if (txtNumeroCuenta.Text == "")
            {
                VerError("Ingrese el numero de Ahorro");
                return false;
            }
        }


        List<CuentaHabientes> LstDetalle = new List<CuentaHabientes>();
        LstDetalle = ObtenerListaDetalle();
        int cont = 0;

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

        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
        DateTime dtUltCierre;
        try
        {
            dtUltCierre = Convert.ToDateTime(ComprobanteServicio.Consultafecha((Usuario)Session["Usuario"]));
        }
        catch
        {
            VerError("No se encontro la fecha del último cierre contable");
            return false;
        }

        if (Convert.ToDateTime(txtFechaCierre.Texto) <= dtUltCierre)
        {
            VerError("La fecha de Cierre ingresada debe ser mayor a la fecha del último Cierre generado ('" + dtUltCierre.ToShortDateString() + "')");
            return false;
        }

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

        //  Int64 COD = Buscar_Titular();
        //if (COD == 0)
        //{
        //  VerError("Error al realizar la búsqueda, No se ubico al titular");
        // return false;
        //}
        return true;
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            if (ValidarDatos())
            {
                ctlMensaje.MostrarMensaje("Desea generar el cierre?");
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaCie, "btnGuardar_Click", ex);
        }

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

            //if (ddlModalidad.Text == "CONJUNTA")
            //{
            //    DropDownListGrid ddlConjuncion = (DropDownListGrid)rfila.FindControl("ddlConjuncion");
            //    if (ddlConjuncion.SelectedIndex != 0)
            //        eDeta.conjuncion = ddlConjuncion.SelectedValue;
            //    else
            //        eDeta.conjuncion = null;
            //}
            //else
            //    eDeta.conjuncion = null;

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


    protected Int64 Buscar_Titular()
    {
        Int64 codigo = 0;
        int cont = 0;
        ObtenerListaDetalle();
        foreach (GridViewRow rFila in gvDetalle.Rows)
        {
            CheckBoxGrid chkPrincipal = (CheckBoxGrid)rFila.FindControl("chkPrincipal");
            if (chkPrincipal.Checked)
                cont++;

            if (cont == 1)
            {
                string cod = "";
                cod = rFila.Cells[2].Text.Replace("&nbsp;", "");
                if (cod != "")
                    codigo = Convert.ToInt64(cod);
                break;
            }
        }
        return codigo;
    }
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        string sError = "";
        VerError("");
        Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
        Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
        try
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["usuario"];

            //DATOS DE LA OPERACION
            Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
            vOpe.cod_ope = 0;
            vOpe.tipo_ope = 12;
            vOpe.cod_caja = 0;
            vOpe.cod_cajero = 0;
            vOpe.observacion = "Operacion-Cierre Cuentas Ahorros";
            vOpe.cod_proceso = null;
            vOpe.fecha_oper = Convert.ToDateTime(txtFechaCierre.Texto);
            vOpe.fecha_calc = Convert.ToDateTime(txtFechaCierre.Texto);
            vOpe.cod_ofi = pUsuario.cod_oficina;

            Int64 COD_OPE = 0, COD_PERSONA = 0;

            COD_PERSONA = Convert.ToInt64(txtCodPersona.Text);

            //GRABACION DEL GIRO A REALIZAR
            Xpinn.FabricaCreditos.Services.AvanceService AvancServices = new Xpinn.FabricaCreditos.Services.AvanceService();
            Usuario pusu = (Usuario)Session["usuario"];
            Xpinn.FabricaCreditos.Entities.Giro pGiro = new Xpinn.FabricaCreditos.Entities.Giro();
            pGiro.idgiro = 0;
            pGiro.cod_persona = COD_PERSONA;
            pGiro.forma_pago = Convert.ToInt32(ctlGiro.ValueFormaDesem);
            pGiro.tipo_acto = 5;
            pGiro.fec_reg = Convert.ToDateTime(txtFechaCierre.Texto);
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
            }
            if (ctlGiro.IndiceFormaDesem != 1) //"eFECTIVO"
            {
                TipoFormaDesembolso formaPago = ctlGiro.ValueFormaDesem.ToEnum<TipoFormaDesembolso>();
                //DETERMINAR LA IDENTIFICACIÓN DE LA CUENTA BANCARIA
                CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ctlGiro.ValueEntidadOrigen), ctlGiro.TextCuentaOrigen, (Usuario)Session["usuario"]);
                Int64 idCta = CuentaBanc.idctabancaria;
                //DATOS DE FORMA DE PAGO
                if (formaPago == TipoFormaDesembolso.Transferencia)
                {
                    pGiro.idctabancaria = idCta;
                    pGiro.cod_banco = Convert.ToInt32(ctlGiro.ValueEntidadDest);
                    pGiro.num_cuenta = ctlGiro.TextNumCuenta;
                    pGiro.tipo_cuenta = Convert.ToInt32(ctlGiro.ValueTipoCta);
                }
                else if (formaPago == TipoFormaDesembolso.Cheque)
                {
                    pGiro.idctabancaria = idCta;
                    pGiro.cod_banco = 0;        //NULO
                    pGiro.num_cuenta = null;    //NULO
                    pGiro.tipo_cuenta = -1;      //NULO
                }
                else if (formaPago == TipoFormaDesembolso.TranferenciaAhorroVistaInterna)
                {
                    pGiro.num_cuenta = !string.IsNullOrWhiteSpace(ctlGiro.ValueCuentaAhorro) ? ctlGiro.ValueCuentaAhorro : null;
                }
                else
                {
                    pGiro.idctabancaria = 0;
                    pGiro.cod_banco = 0;
                    pGiro.num_cuenta = null;
                    pGiro.tipo_cuenta = -1;
                }
            }

            pGiro.fec_apro = DateTime.MinValue;
            pGiro.cob_comision = 0;
            pGiro.valor = Convert.ToInt64(txtTotal.Text.Replace(".", "").Replace(",", ""));

            //DATOS DE CIERRE LIQUIDACION
            ELiquidacionInteres pLiqui = new ELiquidacionInteres();
            pLiqui.fecha_liquidacion = Convert.ToDateTime(txtFechaCierre.Texto); //FECHA DE CIERRE
            pLiqui.numero_cuenta = txtNumeroCuenta.Text;
            pLiqui.Saldo = Convert.ToDecimal(txtSaldoTotalLiq.Text.Replace(".", ""));
            pLiqui.interes_capitalizado = Convert.ToDecimal(txtInteresescapitalizable.Text.Replace(".", ""));
            pLiqui.Interes = Convert.ToDecimal(txtIntereses.Text.Replace(".", ""));
            pLiqui.Interescausado = Convert.ToDecimal(txtInteresesCausado.Text.Replace(".", ""));
            pLiqui.Retefuente = Convert.ToDecimal(txtRetencion.Text.Replace(".", ""));
            pLiqui.valor_gmf = Convert.ToDecimal(txtGMF.Text.Replace(".", ""));
            pLiqui.valor_pagar = Convert.ToDecimal(txtTotal.Text.Replace(".", ""));
            pLiqui.retencion_causado = Convert.ToDecimal(txtRetencionCausada.Text.Replace(".", ""));
            

            //verificar cierre historico 
            String estado = "";
            DateTime fechacierrehistorico;
            DateTime fechaliquidacion = pLiqui.fecha_liquidacion;
            Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();
            vAhorroVista = ahorrosServicio.ConsultarCierreAhorroVista((Usuario)Session["usuario"]);
            estado = vAhorroVista.estadocierre;
            fechacierrehistorico = Convert.ToDateTime(vAhorroVista.fecha_cierre.ToString());

            if (estado == "D" && fechaliquidacion <= fechacierrehistorico)
            {
                VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO H,'AHORROS'");
            }
            else
            {
                string pError = "";
                pLiqui = ahorrosServicio.CierreLiquidacionAhorro(ref pError, ref COD_OPE, vOpe, pGiro, pLiqui, (Usuario)Session["usuario"]);


                if (pLiqui == null)
                {
                    VerError(pError.Substring(0, 90));
                    return;
                }

                //GENERAR EL COMPROBANTE
                if (COD_OPE != 0)
                {
                    Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                    Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = COD_OPE;
                    Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 12;
                    Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = DateTime.ParseExact(txtFechaCierre.Texto, gFormatoFecha, null);
                    Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = COD_PERSONA;
                    Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                }

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaCie, "btnContinuarMen_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();
            vAhorroVista = ahorrosServicio.ConsultarAhorroVista(Convert.ToString(pIdObjeto), (Usuario)Session["usuario"]);

            txtNumeroCuenta.Text = HttpUtility.HtmlDecode(vAhorroVista.numero_cuenta.ToString().Trim());
            if (!string.IsNullOrEmpty(vAhorroVista.cod_linea_ahorro.ToString()))
                txtLineaAhorros.Text = HttpUtility.HtmlDecode(vAhorroVista.nom_linea.ToString().Trim());

            if (!string.IsNullOrEmpty(vAhorroVista.cod_oficina.ToString()))
                ddlOficina.SelectedValue = HttpUtility.HtmlDecode(vAhorroVista.cod_oficina.ToString().Trim());
            if (!string.IsNullOrEmpty(vAhorroVista.modalidad.ToString()))
                ddlModalidad.SelectedValue = HttpUtility.HtmlDecode(vAhorroVista.modalidad.ToString().Trim());

            if (!string.IsNullOrEmpty(vAhorroVista.fecha_apertura.ToString()))
                txtFechaApertura.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vAhorroVista.fecha_apertura.ToString().Trim()));

            if (!string.IsNullOrEmpty(vAhorroVista.fecha_ultimo_pago.ToString()))
                txtFecUltMov.Text = Convert.ToString(HttpUtility.HtmlDecode(vAhorroVista.fecha_ultimo_pago.ToString().Trim()));

            if (!string.IsNullOrEmpty(vAhorroVista.fecha_ultimo_pago.ToString()))
                txtFecUltLiq.Text = Convert.ToString(HttpUtility.HtmlDecode(vAhorroVista.fecha_interes.ToString().Trim()));    

            if (!string.IsNullOrEmpty(vAhorroVista.cod_persona.ToString()))
                txtCodPersona.Text = Convert.ToString(HttpUtility.HtmlDecode(vAhorroVista.cod_persona.ToString().Trim()));

            //LISTAR TARJETAS ASOCIADAS
            List<AhorroVista> lstTarjetas = new List<AhorroVista>();
            lstTarjetas = ahorrosServicio.ListarTarjetas(Convert.ToString(pIdObjeto), (Usuario)Session["usuario"]);
            gvLista.DataSource = lstTarjetas;
            if (lstTarjetas.Count > 0)
            {
                gvLista.DataBind();

                tarjeta.Visible = false;
            }
            else
            {
                gvLista.DataSource = null;
                gvLista.DataBind();
                tarjeta.Visible = true;
            }

            //RECUPERAR GRILLA DETALLE 
            List<CuentaHabientes> lstDetalle = new List<CuentaHabientes>();

            lstDetalle = ahorrosServicio.ListarDetalleTitulares(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (lstDetalle.Count > 0)
            {
                gvDetalle.Visible = true;
                gvDetalle.DataSource = lstDetalle;
                gvDetalle.DataBind();
                PanelTitulares.Enabled = false;
            }
            else
            {
                gvDetalle.Visible = false;
                gvDetalle.DataSource = null;
            }
            LiquidarAhorro();

        }



        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaCie, "ObtenerDatos", ex);
        }
    }

    protected void LiquidarAhorro()
    {
        ELiquidacionInteres pLiqui = new ELiquidacionInteres();
        ELiquidacionInteres entidad = new ELiquidacionInteres();

        if (txtFechaCierre.Texto == "")
        {
            VerError("Ingrese la fecha de Cierre para realizar la Liquidación.");
            return;
        }
        pLiqui.fecha_liquidacion = Convert.ToDateTime(txtFechaCierre.Texto);
        pLiqui.numero_cuenta = this.txtNumeroCuenta.Text;
        pLiqui.Saldo = 0;
        pLiqui.Interes = 0;
        pLiqui.Retefuente = 0;
        pLiqui.valor_gmf = 0;
        pLiqui.valor_pagar = 0;

        entidad = ahorrosServicio.CalculoLiquidacionaHORRO(pLiqui, (Usuario)Session["usuario"]);

        if (entidad.Saldo != 0)
            txtSaldoTotalLiq.Text = entidad.Saldo.ToString();

        if (entidad.Interes != 0)
            txtIntereses.Text = entidad.Interes.ToString();
        if (entidad.Retefuente != 0)
            txtRetencion.Text = entidad.Retefuente.ToString();
        if (entidad.valor_gmf != 0)
            this.txtGMF.Text = entidad.valor_gmf.ToString();
        if (entidad.valor_pagar != 0)
            txtTotal.Text = entidad.valor_pagar.ToString();

        if (entidad.interes_capitalizado != 0)
            txtInteresescapitalizable.Text = entidad.interes_capitalizado.ToString();

        if (entidad.Interescausado != 0)
            txtInteresesCausado.Text = entidad.Interescausado.ToString();
        if (entidad.retencion_causado != 0)
            txtRetencionCausada.Text = entidad.retencion_causado.ToString();

    }



    private void CargarListas()
    {

        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        try
        {
            ctlGiro.Inicializar();
            ddlModalidad.Items.Insert(0, new ListItem("Seleccione un Item", "0"));
            ddlModalidad.Items.Insert(1, new ListItem("INDIVIDUAL", "1"));
            ddlModalidad.Items.Insert(2, new ListItem("CONJUNTA", "2"));
            ddlModalidad.Items.Insert(3, new ListItem("ALTERNA", "3"));
            ddlModalidad.SelectedIndex = 0;
            ddlModalidad.DataBind();

            Poblar.PoblarListaDesplegable("OFICINA", " COD_OFICINA,NOMBRE ", " ESTADO = 1 ", " 1 ", ddlOficina, (Usuario)Session["usuario"]);


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }



    protected void IraPag(object sender, EventArgs e)
    {
        TextBox _IraPag = (TextBox)sender;

    }

    protected void txtIdentificD_TextChanged(object sender, EventArgs e)
    {
        TextBoxGrid txtIdentificD = (TextBoxGrid)sender;

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
    protected void gvDetalle_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //ObtenerDetalleComprobante(false);
            gvDetalle.PageIndex = e.NewPageIndex;
            ActualizarDetalle();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaCie, "gvDetMovs_PageIndexChanging", ex);
        }
    }

    protected void ActualizarDetalle()
    {
        //List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
        //LstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];
        //gvDetMovs.DataSource = LstDetalleComprobante;
        //gvDetMovs.DataBind();
    }


    /// <summary>
    /// Método para borrar un registro de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDetalle_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvDetalle.DataKeys[e.RowIndex].Values[0].ToString());

        if (conseID != 0)
        {
            try
            {
                ////List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
                ////LstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];

                ////LstDetalleComprobante.RemoveAt((gvDetMovs.PageIndex * gvDetMovs.PageSize) + e.RowIndex);
                ////Session["DetalleComprobante"] = LstDetalleComprobante;

                ////gvDetMovs.DataSourceID = null;
                ////gvDetMovs.DataBind();
                ////gvDetMovs.DataSource = LstDetalleComprobante;
                ////gvDetMovs.DataBind();

                ////CalcularTotal();
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
        }
        else
        {
            e.Cancel = true;
        }
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
    #endregion

    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnListadoPersona_Click(object sender, EventArgs e)
    {

    }
    protected void chkPrincipal_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxGrid chkPrincipal = (CheckBoxGrid)sender;
        int rowIndex = Convert.ToInt32(chkPrincipal.CommandArgument);

        if (chkPrincipal != null)
        {
            foreach (GridViewRow rFila in gvDetalle.Rows)
            {
                CheckBoxGrid check = (CheckBoxGrid)rFila.FindControl("chkPrincipal");
                check.Checked = false;
                if (rFila.RowIndex == rowIndex)
                {
                    check.Checked = true;
                }
            }
        }
    }

    protected void gvDetalle_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnConsular_Click(object sender, ImageClickEventArgs e)
    {
        LiquidarAhorro();
    }

    // actualiza el metodo cargar campo por si cambio la fecha de liquidacion
    public void txtFecha_textchange(object sender, EventArgs e)
    {
        LiquidarAhorro();
    }


}