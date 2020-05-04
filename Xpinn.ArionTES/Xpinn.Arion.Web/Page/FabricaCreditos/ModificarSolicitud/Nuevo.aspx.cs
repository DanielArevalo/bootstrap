using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Xpinn.Util;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;
using Control = System.Web.UI.Control;
using Label = System.Web.UI.WebControls.Label;
using TextBox = System.Web.UI.WebControls.TextBox;

public partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables    
    private Xpinn.FabricaCreditos.Services.DatosSolicitudService DatosSolicitudServicio = new Xpinn.FabricaCreditos.Services.DatosSolicitudService();
    private Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
    private CreditoRecogerService creditoRecogerServicio = new CreditoRecogerService();
    private CreditoPlanService creditoPlanServicio = new CreditoPlanService();
    private Xpinn.FabricaCreditos.Services.SolicitudCreditosRecogidosService SolicitudCreditosRecogidosServicio = new Xpinn.FabricaCreditos.Services.SolicitudCreditosRecogidosService();
    CreditoService CreditoServicio = new CreditoService();
    CuotasExtrasService CuoExtServicio = new CuotasExtrasService();
    private Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private codeudoresService CodeudorServicio = new codeudoresService();
    private long _codPersona;

    String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar

    private void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(DatosClienteServicio.CodigoProgramaModificacion, "D");

            Site1 toolBar = (Site1)this.Master;
            toolBar.eventoGuardar += btnGuardarDatosSolicitud_Click;
            toolBar.eventoCancelar += btnAtr0_Click;

            ((Label)Master.FindControl("lblNombresApellidos")).Text = "";
            ((Label)Master.FindControl("lblIdCliente")).Text = "";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.CodigoProgramaModificacion + "A", "Page_PreInit", ex);
        }


    }

    /// <summary>
    /// Cargar información apenas se ingresa a la opción
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                Session["NumeroSolicitud"] = null;
                if (Session["TipoCredito"] == null)
                    Session["TipoCredito"] = "C";
                Session["Numero_radicado"] = null;
                // Llenar información de los combos o listas desplegables
                CargarListas();
                if (Session[CreditoServicio.CodigoProgramaModificacion + ".id"] != null)
                {
                    idObjeto = Session[CreditoServicio.CodigoProgramaModificacion + ".id"].ToString();
                    Session["NumeroSolicitud"] = idObjeto;
                    ObtenerDatos(idObjeto);
                    idObjeto = Convert.ToString(Session["NumeroSolicitud"]);
                    TablaCuoExt(idObjeto);
                }
                Site1 toolBar = (Site1)this.Master;
                toolBar.eventoGuardar += btnGuardarDatosSolicitud_Click;
                toolBar.eventoCancelar += btnAtr0_Click;

                // Inicializar datos para orden de servicio
                lblTitOrden.Visible = false;
                lblTitIdenProveedor.Visible = false;
                txtIdentificacionprov.Visible = false;
                lblTitNomProveedor.Visible = false;
                txtNombreProveedor.Visible = false;
                btnListadoPersona.Visible = false;
                //RECUPERAR LA FORMA DE PAGO POR DEFECTO
                string FormaPago = ConfigurationManager.AppSettings["ddlFormaPago"].ToString();
                if (FormaPago != null && FormaPago != "")
                {
                    ddlFormaPago.SelectedValue = FormaPago;
                }
                ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
                if (ddlTipoCredito.Items.Count > 0)
                    ddlTipoCredito_SelectedIndexChanged(ddlTipoCredito, null);
                validarMedio();
                // Determinar datos de encabezado de la solicitud
                lblFecha.Text = DateTime.Today.ToShortDateString();
                // Cargar el código de la persona si se paso desde la consulta de datacredito
                if (Session[DatosClienteServicio.CodigoProgramaModificacion + ".id"] != null)
                {
                    idObjeto = Session[DatosClienteServicio.CodigoProgramaModificacion + ".id"].ToString();

                }
                // Cargar el número de solicitud
                if (Session["Solicitud"] != null)
                {
                    lblNumero.Text = Session["Solicitud"].ToString();
                }
                // Deshabilitar campos que dependen de la línea seleccionada
                txtPeriodo.Enabled = false;
                Usuario usuap = (Usuario)Session["usuario"];
                lblOficina.Text = usuap.nombre_oficina;

                if (Checkgarantia_comunitaria.Checked == true)
                {
                    txt_ValorGaran.Visible = true;
                    //consultar Valor Garantia

                    string valorgaran = DatosSolicitudServicio.ConsultaValorGarantia(lblradicacion.Text, (Usuario)Session["usuario"]);
                    txt_ValorGaran.Text = valorgaran;

                }

            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.GetType().Name + "D", "Page_Load", ex);
        }
    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.DatosSolicitud pSolicitud = new Xpinn.FabricaCreditos.Entities.DatosSolicitud();

            pSolicitud = DatosSolicitudServicio.ConsultarSolicitudCreditos(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            if (pSolicitud.numerosolicitud != 0)
                lblNumero.Text = Convert.ToString(pSolicitud.numerosolicitud);
            if (pSolicitud.cod_persona != 0)
            {
                txtCliente.Text = pSolicitud.cod_persona.ToString().Trim();
                if (pSolicitud.identificacion != null && pSolicitud.nombre != null)
                {
                    ((Label)Master.FindControl("lblNombresApellidos")).Text = pSolicitud.nombre;
                    ((Label)Master.FindControl("lblIdCliente")).Text = pSolicitud.identificacion;
                }
            }
            if (pSolicitud.numeroradicado != 0)
                lblradicacion.Text = Convert.ToString(pSolicitud.numeroradicado);
            Session["Numero_radicado"] = pSolicitud.numeroradicado;
            if (pSolicitud.fechasolicitud != DateTime.MinValue)
                lblFecha.Text = pSolicitud.fechasolicitud.ToShortDateString();

            if (pSolicitud.nomoficina != null)
                lblOficina.Text = pSolicitud.nomoficina;
            if (pSolicitud.tipocrdito != null)
            {
                LineasCreditoService LineasCreditoServicio = new LineasCreditoService();
                LineasCredito vLineasCredito = new LineasCredito();
                vLineasCredito = LineasCreditoServicio.ConsultarLineasCredito(pSolicitud.tipocrdito, (Usuario)Session["usuario"]);
                if (vLineasCredito.cod_clasifica == 1)
                    Session["TipoCredito"] = "C";
                else
                    Session["TipoCredito"] = "M";

                if (Session["TipoCredito"].ToString() == "M")
                    ListaSolicitada = "STipoCreditoMicro";
                else
                    if (Session["TipoCredito"].ToString() == "C")
                    ListaSolicitada = "STipoCreditoConsumo";
                else
                    ListaSolicitada = "STipoCreditoConsumo";
                TraerResultadosLista();
                ddlTipoCredito.DataSource = lstDatosSolicitud;
                ddlTipoCredito.DataTextField = "ListaDescripcion";
                ddlTipoCredito.DataValueField = "ListaIdStr";
                ddlTipoCredito.DataBind();

                ddlTipoCredito.SelectedValue = pSolicitud.tipocrdito;
                ddlTipoCredito_SelectedIndexChanged(ddlTipoCredito, null);
            }

            if (pSolicitud.montosolicitado != 0)
                txtMonto.Text = Convert.ToString(pSolicitud.montosolicitado);
            if (pSolicitud.plazosolicitado != 0)
                txtPlazo.Text = Convert.ToString(pSolicitud.plazosolicitado);
            if (pSolicitud.periodicidad != 0)
                ddlPeriodicidad.SelectedValue = pSolicitud.periodicidad.ToString();
            if (pSolicitud.cuotasolicitada != 0)
                txtCuota.Text = Convert.ToString(pSolicitud.cuotasolicitada);
            if (pSolicitud.medio != 0 && ddlMedio.SelectedItem != null)
                ddlMedio.SelectedValue = pSolicitud.medio.ToString();
            if (pSolicitud.concepto != null)
                txtConcepto.Text = pSolicitud.concepto;
            if (pSolicitud.otro != null)
                txtOtro.Text = pSolicitud.otro;
            Checkgarantia_real.Checked = pSolicitud.garantia == 1 ? true : false;
            Checkgarantia_comunitaria.Checked = pSolicitud.garantia_comunitaria == 1 ? true : false;
            Checkpoliza.Checked = pSolicitud.poliza == 1 ? true : false;

            if (pSolicitud.cod_usuario != 0 && Ddlusuarios.SelectedItem != null)
                Ddlusuarios.SelectedValue = pSolicitud.cod_usuario.ToString();
            if (pSolicitud.forma_pago != 0 && ddlFormaPago.SelectedItem != null)
                ddlFormaPago.SelectedValue = pSolicitud.forma_pago.ToString();
            if (pSolicitud.empresa_recaudo != null && ddlEmpresa.SelectedItem != null)
                ddlEmpresa.SelectedValue = pSolicitud.empresa_recaudo.ToString();
            if (pSolicitud.tipo_liquidacion != 0 && ddlTipoLiquidacion.SelectedItem != null)
                ddlTipoLiquidacion.SelectedValue = pSolicitud.tipo_liquidacion.ToString();

            if (pSolicitud.nombre != null)
                txtNombreProveedor.Text = pSolicitud.nombre;
            if (pSolicitud.identificacion != null)
                txtIdentificacionprov.Text = pSolicitud.identificacion;
            InicialCodeudores();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }


    /// <summary>
    /// Cargar información de las listas desplegables
    /// </summary>
    private void CargarListas()
    {
        try
        {
            if (Session["TipoCredito"].ToString() == "M")
                ListaSolicitada = "STipoCreditoMicro";
            else
                if (Session["TipoCredito"].ToString() == "C")
                ListaSolicitada = "STipoCreditoConsumo";
            else
                ListaSolicitada = "STipoCreditoConsumo";
            TraerResultadosLista();
            ddlTipoCredito.DataSource = lstDatosSolicitud;
            ddlTipoCredito.DataTextField = "ListaDescripcion";
            ddlTipoCredito.DataValueField = "ListaIdStr";
            ddlTipoCredito.DataBind();

            ListaSolicitada = "Periodicidad";
            TraerResultadosLista();
            ddlPeriodicidad.DataSource = lstDatosSolicitud;
            ddlPeriodicidad.DataTextField = "ListaDescripcion";
            ddlPeriodicidad.DataValueField = "ListaIdStr";
            ddlPeriodicidad.DataBind();

            ListaSolicitada = "Medio";
            TraerResultadosLista();
            ddlMedio.DataSource = lstDatosSolicitud;
            ddlMedio.DataTextField = "ListaDescripcion";
            ddlMedio.DataValueField = "ListaIdStr";
            ddlMedio.DataBind();

            ListaSolicitada = "TipoLiquidacion";
            TraerResultadosLista();
            ddlTipoLiquidacion.DataSource = lstDatosSolicitud;
            ddlTipoLiquidacion.DataTextField = "ListaDescripcion";
            ddlTipoLiquidacion.DataValueField = "ListaId";
            ddlTipoLiquidacion.DataBind();

            ListItem selectedListItem2 = ddlPeriodicidad.Items.FindByValue("1"); //Selecciona mensual por defecto
            if (selectedListItem2 != null)
                selectedListItem2.Selected = true;

            if (Session["TipoCredito"].ToString() == "M")
            {
                ListItem selectedListItem = ddlTipoCredito.Items.FindByValue("301"); // Selección microcrédito por defecto
                if (selectedListItem != null)
                    selectedListItem.Selected = true;
            }

            if (Session["TipoCredito"].ToString() == "C")
            {
                ListItem selectedListItem = ddlTipoCredito.Items.FindByValue("103"); // Selección microcrédito por defecto
                if (selectedListItem != null)
                    selectedListItem.Selected = true;
            }
            ListItem selectedTipoLiq = ddlTipoLiquidacion.Items.FindByValue("2"); // Seleccionar tipo de liquidación por defecto
            if (selectedTipoLiq != null)
                selectedTipoLiq.Selected = true;

            asesores();
            Calcular_Cupo();

            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstEmpresas = new List<Xpinn.Tesoreria.Entities.EmpresaRecaudo>();
            Xpinn.Tesoreria.Entities.EmpresaRecaudo empresa = new Xpinn.Tesoreria.Entities.EmpresaRecaudo();
            Xpinn.Tesoreria.Services.EmpresaRecaudoServices empresaServicio = new Xpinn.Tesoreria.Services.EmpresaRecaudoServices();
            if (Session["Cod_persona"] == null)
            {
                ddlEmpresa.DataSource = empresaServicio.ListarEmpresaRecaudo(empresa, pUsuario);
            }
            else
            {
                try
                {
                    Int64 Cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
                    ddlEmpresa.DataSource = empresaServicio.ListarEmpresaRecaudoPersona(Cod_persona, pUsuario);
                }
                catch
                {
                    ddlEmpresa.DataSource = empresaServicio.ListarEmpresaRecaudo(empresa, pUsuario);
                }
            }
            ddlEmpresa.DataTextField = "nom_empresa";
            ddlEmpresa.DataValueField = "cod_empresa";
            ddlEmpresa.AppendDataBoundItems = true;
            ddlEmpresa.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlEmpresa.SelectedIndex = 0;
            ddlEmpresa.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    /// <summary>
    /// Cargar listado de asesores comerciales
    /// </summary>
    private void asesores()
    {
        string ddlusuarios = ConfigurationManager.AppSettings["ddlusuarios"].ToString();
        if (ddlusuarios == "1")
        {
            // Cargar los asesores ejecutivos
            UsuarioAseService serviceEjecutivo = new UsuarioAseService();
            UsuarioAse ejec = new UsuarioAse();
            List<UsuarioAse> lstAsesores = new List<UsuarioAse>();
            lstAsesores = serviceEjecutivo.ListartodosUsuarios(ejec, (Usuario)Session["usuario"]);
            if (lstAsesores.Count > 0)
            {
                Ddlusuarios.DataSource = lstAsesores;
                Ddlusuarios.DataTextField = "nombre";
                Ddlusuarios.DataValueField = "codusuario";
                Ddlusuarios.DataBind();
                Ddlusuarios.Items.Insert(0, new ListItem("<Seleccione un Item>", "-1"));
            }
            else
            {
                ddlusuarios = "0";
            }
        }
        if (ddlusuarios != "1")
        {
            // Cargar usuarios cuando no se manejan asesores
            Xpinn.Seguridad.Services.UsuarioService serviceEjecutivo = new Xpinn.Seguridad.Services.UsuarioService();
            Xpinn.Util.Usuario usu = new Xpinn.Util.Usuario();
            usu.estado = 1;
            Ddlusuarios.DataSource = serviceEjecutivo.ListarUsuario(usu, (Usuario)Session["usuario"]);
            Ddlusuarios.DataTextField = "nombre";
            Ddlusuarios.DataValueField = "codusuario";
            Ddlusuarios.DataBind();
            Ddlusuarios.Items.Insert(0, new ListItem("<Seleccione un Item>", "-1"));
        }
    }

    private void TraerResultadosLista()
    {
        if (ListaSolicitada == null)
            return;
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
    }

    /// <summary>
    ///  Deshabilita los campos una vez grabados
    /// </summary>
    private void deshabilitaText()
    {
        txtMonto.Enabled = false;
        txtPlazo.Enabled = false;
        txtCuota.Enabled = false;
        ddlTipoCredito.Enabled = false;
        ddlPeriodicidad.Enabled = false;
        txtPeriodo.Enabled = false;
        ddlMedio.Enabled = false;
        txtOtro.Enabled = false;
        txtCliente.Enabled = false;
    }



    /// <summary>
    /// Guardar información de la solicitud de crédito
    /// </summary>
    private void Guardar()
    {
        CuotasExtras eCuoExt = new CuotasExtras();
        lblMensajeValidacion.Text = "";
        string NumeroSolicitud = "";
        if (Session["NumeroSolicitud"] != null)
            NumeroSolicitud = Session["NumeroSolicitud"].ToString();


        if (NumeroSolicitud != "") // Valida que el numero de solicitud no este nulo o vacio 
        {
            Xpinn.FabricaCreditos.Entities.DatosSolicitud datosSolicitud = new Xpinn.FabricaCreditos.Entities.DatosSolicitud();
            try
            {
                // Determinando datos de la solicitud
                datosSolicitud.numerosolicitud = lblNumero.Text != "" ? Convert.ToInt64(lblNumero.Text) : 0;
                datosSolicitud.fechasolicitud = DateTime.Now;
                datosSolicitud.cod_persona = Convert.ToInt64(txtCliente.Text);
                datosSolicitud.montosolicitado = txtMonto.Text != "" ? Convert.ToInt64(txtMonto.Text.Trim().Replace(".", "")) : 0;
                datosSolicitud.plazosolicitado = txtPlazo.Text != "" ? Convert.ToInt64(txtPlazo.Text.Trim().Replace(".", "")) : 0;
                datosSolicitud.cuotasolicitada = txtCuota.Text != "" ? Convert.ToInt64(txtCuota.Text.Trim().Replace(".", "")) : 0;
                datosSolicitud.tipocrdito = Convert.ToString(ddlTipoCredito.SelectedValue);
                if (ddlPeriodicidad.Text != "") datosSolicitud.periodicidad = Convert.ToInt64(ddlPeriodicidad.SelectedValue);
                if (ddlMedio.SelectedValue != null)
                    if (ddlMedio.SelectedValue != "") datosSolicitud.medio = Convert.ToInt64(ddlMedio.SelectedValue);
                datosSolicitud.otro = txtOtro.Text.Trim();
                datosSolicitud.concepto = txtConcepto.Text.Trim();
                datosSolicitud.forma_pago = Convert.ToInt64(ddlFormaPago.SelectedValue);
                if (Checkgarantia_real.Checked == true) datosSolicitud.garantia = 1;
                if (Checkgarantia_comunitaria.Checked == true) datosSolicitud.garantia_comunitaria = 1;
                if (Checkpoliza.Checked == true) datosSolicitud.poliza = 1;
                datosSolicitud.tipo_liquidacion = Convert.ToInt64(ddlTipoLiquidacion.SelectedValue);
                datosSolicitud.forma_pago = Convert.ToInt64(ddlFormaPago.SelectedValue);
                if (datosSolicitud.forma_pago == 2)
                    datosSolicitud.empresa_recaudo = Convert.ToInt64(ddlEmpresa.SelectedValue);
                else
                    datosSolicitud.empresa_recaudo = null;

                // Determinando datos de la oficina y el usuario
                Usuario user = (Usuario)Session["usuario"];

                datosSolicitud.cod_oficina = user.cod_oficina;

                string ddlusuarios = ConfigurationManager.AppSettings["ddlusuarios"].ToString();
                if (ddlusuarios == "1")
                {
                    datosSolicitud.cod_usuario = Convert.ToInt64(Ddlusuarios.SelectedValue);
                }
                else
                {
                    datosSolicitud.cod_usuario = user.codusuario;
                }

                // Registrando datos del proveedor para la orden
                if (txtIdentificacionprov.Text.Trim() != "")
                    datosSolicitud.identificacionprov = Convert.ToString(txtIdentificacionprov.Text);
                datosSolicitud.nombreprov = txtNombreProveedor.Text;

                // Modificar datos de la solicitud
                datosSolicitud = DatosSolicitudServicio.ModificarSolicitudes(datosSolicitud, (Usuario)Session["usuario"]);

                // Mostrando informaciòn del nùmero de solicitud grabada
                lblMensaje.Text = "Su solicitud de credito se ha modificado con el número " + lblNumero.Text;
                Session["NumeroSolicitud"] = lblNumero.Text;

                Int64 tipoempresa = 0;
                Usuario usuap = (Usuario)Session["usuario"];
                tipoempresa = Convert.ToInt64(usuap.tipo);
                mvDatosSolicitud.ActiveViewIndex = 2;

                if (tipoempresa == 2)
                {
                    // Pasar a pregunta de si requiere recoger creditos 
                    mvDatosSolicitud.ActiveViewIndex = 2;

                }
                // Pasar a pregunta de si requiere codeudores
                if (tipoempresa == 1)
                {
                    mvDatosSolicitud.ActiveViewIndex = 2;
                    Session["Solicitud"] = lblNumero.Text.ToString().Trim();
                }

                //Agregado para Enviar el valor de la garantía del atributo #14
                if (Checkgarantia_comunitaria.Checked == true)
                {
                    if (txt_ValorGaran.Text != "" && Convert.ToDecimal(txt_ValorGaran.Text) > 0)
                    {
                        LineasCredito valoresgarantias = new LineasCredito();
                        Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
                        // Determinar valores de la linea para el atributo 14 fondo de  garantias 
                        valoresgarantias = LineaCreditoServicio.ConsultarDeducciones(ddlTipoCredito.SelectedValue.ToString(), 14, (Usuario)Session["Usuario"]);

                        CreditoSolicitadoService servicedescuentogarantia = new CreditoSolicitadoService();
                        DescuentosCredito garantia = new DescuentosCredito();

                        garantia.numero_radicacion = Convert.ToInt64(lblradicacion.Text.ToString());
                        garantia.cod_atr = Convert.ToInt32(valoresgarantias.cod_atr);
                        garantia.tipo_liquidacion = Convert.ToInt32(valoresgarantias.tipo_liquidacion);
                        garantia.val_atr = Convert.ToDecimal(txt_ValorGaran.Text);
                        garantia.numero_cuotas = valoresgarantias.numero_cuotas1;
                        garantia.forma_descuento = Convert.ToInt32(valoresgarantias.Forma_descuento);
                        garantia.tipo_impuesto = Convert.ToInt32(valoresgarantias.tipoimpuesto);
                        garantia.tipo_descuento = Convert.ToInt32(valoresgarantias.tipo_descuento);

                        garantia = servicedescuentogarantia.modificardeduccionesCredito(garantia, (Usuario)Session["Usuario"]);
                    }
                }


                //GRABA CUOTAS EXTRAS 
                List<CuotasExtras> lstCuoExt = new List<CuotasExtras>();
                CuoExtServicio.EliminarCuotasExtras(Convert.ToInt64(idObjeto), (Usuario)Session["Usuario"]);
                foreach (GridViewRow row in gvCuoExt.Rows)
                {
                    eCuoExt.numero_radicacion = Convert.ToInt64(lblradicacion.Text);
                    if (((TextBox)row.FindControl("lblfechapago")).Text == "") continue;

                    eCuoExt.fecha_pago = Convert.ToDateTime(((TextBox)row.FindControl("lblfechapago")).Text);
                    eCuoExt.forma_pago = ((DropDownList)row.FindControl("ddlformapago")).Text == "Caja" ? "1" : "2";
                    eCuoExt.valor = Convert.ToDecimal(((TextBox)row.FindControl("lblvalor")).Text);
                    eCuoExt.des_tipo_cuota = Convert.ToString(((DropDownList)row.FindControl("ddltipocuotagv")).Text);
                    eCuoExt.valor_capital = Convert.ToDecimal(((TextBox)row.FindControl("lblvalor")).Text);
                    eCuoExt.valor_interes = 0;
                    eCuoExt.saldo_capital = 0;
                    eCuoExt.saldo_interes = 0;

                    CuoExtServicio.CrearCuotasExtras(eCuoExt, (Usuario)Session["Usuario"]);
                }

                // ADICIONANDO LISTA DE CODEUDORES
                List<codeudores> lstCodeudores = ObtenerListaCodeudores();
                if (lstCodeudores.Count > 0)
                {
                    foreach (codeudores item in lstCodeudores)
                    {
                        CodeudorServicio.EliminarcodeudoresCred(item.idcodeud, item.numero_radicacion, (Usuario)Session["Usuario"]);
                    }
                    foreach (GridViewRow item in gvListaCodeudores.Rows)
                    {
                        codeudores cod = new codeudores();
                        cod.numero_radicacion = Convert.ToInt64(idObjeto);
                        cod.codpersona = Convert.ToInt64(((Label)item.FindControl("lblCodPersona")).Text);
                        cod.tipo_codeudor = "C";
                        cod.parentesco = 0;
                        cod.opinion = "B";
                        cod.orden = Convert.ToInt32(((Label)item.FindControl("lblOrdenRow")).Text);
                        CodeudorServicio.CrearCodeudoresDesdeFuncionImportacion(cod, (Usuario)Session["Usuario"]);
                    }

                }
                Navegar(Pagina.Lista);
            }
            catch (ExceptionBusiness ex)
            {
                lblMensajeValidacion.Text = ex.Message;
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(DatosSolicitudServicio.GetType().Name + "A", "btnGuardar_Click", ex);
            }

        }
        else
        {

        }
    }
    /// <summary>
    ///  Realizar la liquidación de una solicitud de crédito
    /// </summary>



    /// <summary>
    /// Guardar informaciòn de los crèditos a recoger
    /// </summary>
    private void GuardarRecoger()
    {
        Xpinn.FabricaCreditos.Entities.SolicitudCreditosRecogidos vSolicitudCreditosRecogidos = new Xpinn.FabricaCreditos.Entities.SolicitudCreditosRecogidos();

        foreach (GridViewRow row in gvListaSolicitudCreditosRecogidos.Rows)
        {
            if (((CheckBoxGrid)row.Cells[8].FindControl("chkRecoger")).Checked)
            {
                if (idObjeto != "")
                {
                    vSolicitudCreditosRecogidos.idsolicitudrecoge = 0;
                    vSolicitudCreditosRecogidos.numerosolicitud = Convert.ToInt64(lblNumero.Text.Trim());
                    vSolicitudCreditosRecogidos.numero_recoge = Convert.ToInt64(row.Cells[1].Text.Trim());
                    vSolicitudCreditosRecogidos.fecharecoge = Convert.ToDateTime(lblFecha.Text.Trim());
                    vSolicitudCreditosRecogidos.fechapago = DateTime.Now;
                    vSolicitudCreditosRecogidos.saldocapital = Convert.ToInt64(row.Cells[4].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    vSolicitudCreditosRecogidos.saldointcorr = Convert.ToInt64(row.Cells[5].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    vSolicitudCreditosRecogidos.saldointmora = Convert.ToInt64(row.Cells[6].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    vSolicitudCreditosRecogidos.saldootros = Convert.ToInt64(row.Cells[7].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    vSolicitudCreditosRecogidos.saldomipyme = Convert.ToInt64(row.Cells[8].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    vSolicitudCreditosRecogidos.saldoivamipyme = Convert.ToInt64(row.Cells[9].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                }

                vSolicitudCreditosRecogidos = SolicitudCreditosRecogidosServicio.CrearSolicitudCreditosRecogidos(vSolicitudCreditosRecogidos, (Usuario)Session["usuario"]);
                //GRABAR EN TABLA CREDITO RECOGIDOS
                if (Session["Numero_Radicacion"] != null)
                {
                    CreditoRecoger vCreditoRecoger = new CreditoRecoger();
                    vCreditoRecoger.numero_radicacion = Convert.ToInt64(Session["Numero_Radicacion"].ToString());
                    vCreditoRecoger.numero_credito = Convert.ToInt64(row.Cells[1].Text.Trim());
                    Int64 valor_recoge = 0;
                    Int64 valor;
                    valor = Convert.ToInt64(row.Cells[4].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    valor_recoge = valor_recoge + valor;
                    valor = Convert.ToInt64(row.Cells[5].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    valor_recoge = valor_recoge + valor;
                    valor = Convert.ToInt64(row.Cells[6].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    valor_recoge = valor_recoge + valor;
                    valor = Convert.ToInt64(row.Cells[7].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    valor_recoge = valor_recoge + valor;
                    vCreditoRecoger.valor_recoge = valor_recoge;
                    vCreditoRecoger.fecha_pago = DateTime.Now;
                    vCreditoRecoger.saldo_capital = Convert.ToInt64(row.Cells[4].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    creditoRecogerServicio.CrearCreditoRecoger(vCreditoRecoger, (Usuario)Session["usuario"]);

                    idObjeto = vSolicitudCreditosRecogidos.idsolicitudrecoge.ToString();
                }
            }
        }
    }


    protected void btnAtr_Click(object sender, ImageClickEventArgs e)
    {
        mvDatosSolicitud.ActiveViewIndex = 0;
    }

    protected void btnAtr0_Click(object sender, ImageClickEventArgs e)
    {
        Int64 tipoempresa = 0;
        Usuario usuap = (Usuario)Session["usuario"];
        tipoempresa = Convert.ToInt64(usuap.tipo);

        if (tipoempresa == 2)
        {
            Session[DatosClienteServicio.CodigoProgramaModificacion + ".NumDoc"] = ((Label)Master.FindControl("lblIdCliente")).Text;
            Response.Redirect("~/Page/FabricaCreditos/ModificarSolicitud/Lista.aspx");
        }
        if (tipoempresa == 1)
        {
            Response.Redirect("~/Page/FabricaCreditos/ModificarSolicitud/Lista.aspx");
        }
    }

    protected void btnGuardarDatosSolicitud_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        // Validando la lìnea
        if (ddlTipoCredito.SelectedValue.Trim() == "")
        {
            lblMensaje.Text = "Debe seleccionar la línea de crédito";
            return;
        }
        if (Ddlusuarios.SelectedValue.Trim() == "-1")
        {
            VerError("Debe seleccionar un asesor");
            return;
        }

        if (Checkgarantia_comunitaria.Checked == true)
        {
            if (txt_ValorGaran.Text == "")
            {
                VerError("Debe Ingresar el valor de la garantía");
                return;
            }
        }

        if (txtIdentificacionprov.Visible == true)
        {
            Persona1 pData = new Persona1();
            Persona1Service PersonaService = new Persona1Service();
            if (txtIdentificacionprov.Text != "")
            {
                pData.seleccionar = "Identificacion";
                pData.noTraerHuella = 1;
                pData.identificacion = txtIdentificacionprov.Text;
                pData = PersonaService.ConsultarPersona1Param(pData, (Usuario)Session["usuario"]);
                if (pData.nombres == null && pData.apellidos == null || txtNombreProveedor.Text == "")
                {
                    VerError("Ingrese una identificación valida del Proveedor");
                    return;
                }
            }
            else
            {
                VerError("Ingrese una identificación valida del Proveedor");
                return;
            }
        }
        // Guarda los datos de los datos de la solicitud
        if (txtMonto.Text != "0" && txtPlazo.Text != "0" && txtMonto.Text != "" && txtPlazo.Text != "")
        {
            Guardar();
            VerError("Solicitud guardada Con Exito");
            Navegar("Lista.aspx");
        }
    }


    protected void ddlTipoCredito_SelectedIndexChanged(object sender, EventArgs e)
    {
        Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        // Determinar si la línea es de Garantías Comunitarias
        if (LineaCreditoServicio.LineaEsFondoGarantiasComunitarias(ddlTipoCredito.SelectedValue.ToString(), (Usuario)Session["Usuario"]))
        {
            Checkgarantia_comunitaria.Checked = true;
            txt_ValorGaran.Visible = true;
        }
        else
        {
            Checkgarantia_comunitaria.Checked = false;
        }
        // Determinar datos de la línea
        LineasCredito eLinea = new LineasCredito();
        eLinea = LineaCreditoServicio.ConsultarLineasCredito(ddlTipoCredito.SelectedValue.ToString(), (Usuario)Session["Usuario"]);
        ddlTipoLiquidacion.SelectedValue = eLinea.tipo_liquidacion.ToString();
        // Determinar si la línea maneja periodo de gracia
        if (string.Equals(eLinea.maneja_pergracia, "1"))
            txtPeriodo.Enabled = true;
        else
            txtPeriodo.Enabled = false;
        // Determinar si es orden de servicio
        if (eLinea.orden_servicio == 1)
        {
            lblTitOrden.Visible = true;
            lblTitIdenProveedor.Visible = true;
            txtIdentificacionprov.Visible = true;
            lblTitNomProveedor.Visible = true;
            txtNombreProveedor.Visible = true;
            btnListadoPersona.Visible = true;
        }
        else
        {
            lblTitOrden.Visible = false;
            lblTitIdenProveedor.Visible = false;
            txtIdentificacionprov.Visible = false;
            lblTitNomProveedor.Visible = false;
            txtNombreProveedor.Visible = false;
            btnListadoPersona.Visible = false;
        }
        // Calcular el cupo del crédito
        Calcular_Cupo();
    }

    private void Calcular_Cupo()
    {
        Xpinn.FabricaCreditos.Entities.LineasCredito DatosLinea = new Xpinn.FabricaCreditos.Entities.LineasCredito();
        Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCredito = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        try
        {
            if (!string.Equals(txtCliente.Text, "") && !string.Equals(lblFecha.Text, ""))
                DatosLinea = LineaCredito.Calcular_Cupo(ddlTipoCredito.SelectedValue.ToString(), Convert.ToInt64(txtCliente.Text), Convert.ToDateTime(lblFecha.Text), (Usuario)Session["usuario"]);
            else
                DatosLinea = LineaCredito.Calcular_Cupo(ddlTipoCredito.SelectedValue.ToString(), 0, DateTime.Today, (Usuario)Session["usuario"]);
            txtPlazoMaximo.Text = DatosLinea.Plazo_Maximo.ToString();
            txtMontoMaximo.Text = String.Format("{0:C}", DatosLinea.Monto_Maximo);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void ddlMedio_TextChanged(object sender, EventArgs e)
    {
        if (string.Equals(ddlMedio.SelectedItem.ToString().ToLower(), "otro"))
        {
            txtOtro.Enabled = true;
        }
        else
        {
            txtOtro.Text = "";
            txtOtro.Enabled = false;
        }
    }



    protected void ddlMedio_SelectedIndexChanged(object sender, EventArgs e)
    {
        validarMedio();
    }

    /// <summary>
    /// Mètodo para calcular la edad de la persona
    /// </summary>
    /// <param name="birthDate"></param>
    /// <returns></returns>
    public static int GetAge(DateTime birthDate)
    {
        return (int)Math.Floor((DateTime.Now - birthDate).TotalDays / 365.242199);
    }

    /// <summary>
    /// Método para validar el tipo de medio por el cual se enteró de la entidad
    /// </summary>
    private void validarMedio()
    {
        txtOtro.Visible = ddlMedio.SelectedValue == "0" ? true : false;
        lblCual.Visible = ddlMedio.SelectedValue == "0" ? true : false;
    }


    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFormaPago.SelectedItem.Value == "2")
        {
            lblEmpresa.Visible = true;
            ddlEmpresa.Visible = true;
        }
        else
        {
            lblEmpresa.Visible = false;
            ddlEmpresa.Visible = false;
        }
    }

    /// <summary>
    /// Método para mostrar los créditos del deudor para recoger
    /// </summary>
    private void TablaCreditosRecogidos(Int64 Credito)
    {
        try
        {
            List<CreditoRecoger> lstConsulta = new List<CreditoRecoger>();
            CreditoRecoger creditoRecoger = new CreditoRecoger();

            // Cargar los créditos a recoger
            creditoRecoger.numero_radicacion = Convert.ToInt64(Credito);
            lstConsulta = creditoRecogerServicio.ListarCreditoRecoger(creditoRecoger, (Usuario)Session["usuario"]);

            gvListaSolicitudCreditosRecogidos.PageSize = pageSize;
            gvListaSolicitudCreditosRecogidos.EmptyDataText = emptyQuery;

            foreach (CreditoRecoger variable in lstConsulta)
            {
                variable.valor_total = variable.saldo_capital + variable.interes_mora + variable.interes_corriente + variable.otros + variable.leymipyme + variable.iva_leymipyme;
            }

            gvListaSolicitudCreditosRecogidos.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvListaSolicitudCreditosRecogidos.Visible = false;
                lblTotalRegsSolicitudCreditosRecogidos.Visible = false;
                lblTotalRegsSolicitudCreditosRecogidos.Text = "<br/> Registros encontrados: " + lstConsulta.Count.ToString();
                gvListaSolicitudCreditosRecogidos.DataBind();

            }
            else
            {
                gvListaSolicitudCreditosRecogidos.Visible = false;
                lblTotalRegsSolicitudCreditosRecogidos.Visible = false;
                lblTotalRegsSolicitudCreditosRecogidos.Text = "No se encontraron créditos recogidos para este crédito";
            }

            Session.Add(DatosSolicitudServicio.CodigoProgramaModificacion + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosSolicitudServicio.CodigoProgramaModificacion, "Page_PreInit", ex);
        }

        // Según parametro del WEBCONFIG no marcar los créditos a recoger
        if (GlobalWeb.gMarcarRecogerDesembolso == "1")
        {
            foreach (GridViewRow row in gvListaSolicitudCreditosRecogidos.Rows)
            {
                ((CheckBoxGrid)row.Cells[8].FindControl("chkRecoger")).Checked = false;
            }
        }

    }


    protected void chkRecoger_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            //VALIDACION SI CUMPLE EL MINIMO Y MAXIMO DE REFINANCIACION
            LineasCredito pDatosLinea = new LineasCredito();
            Xpinn.FabricaCreditos.Data.LineasCreditoData vData = new Xpinn.FabricaCreditos.Data.LineasCreditoData();

            CheckBoxGrid chkRecoger = (CheckBoxGrid)sender;
            int nItem = Convert.ToInt32(chkRecoger.CommandArgument);
            if (chkRecoger.Checked)
            {
                foreach (GridViewRow rFila in gvListaSolicitudCreditosRecogidos.Rows)
                {
                    if (rFila.RowIndex == nItem)
                    {
                        //CAPTURAR EL CODIGO DE LA LINEA
                        string Linea = rFila.Cells[2].Text;
                        string[] sDatos = Linea.ToString().Split('-');
                        string cod_linea = sDatos[0].ToString();
                        if (cod_linea != "")
                        {
                            pDatosLinea = vData.ConsultaLineaCredito(cod_linea, (Usuario)Session["usuario"]);
                            //VARIABLES A VALIDAR
                            decimal minimo = 0, maximo = 0;
                            minimo = Convert.ToDecimal(pDatosLinea.minimo_refinancia);
                            maximo = Convert.ToDecimal(pDatosLinea.maximo_refinancia);
                            if (pDatosLinea.tipo_refinancia == 0) //SI ES POR RANGO DE SALDO
                            {
                                //RECUPERAR SALDO CAPITAL
                                decimal saldoCapital = 0;
                                if (rFila.Cells[4].Text != "" && rFila.Cells[4].Text != "&nbsp;")
                                    saldoCapital = Convert.ToDecimal(rFila.Cells[4].Text);
                                if (saldoCapital < minimo || saldoCapital > maximo)
                                {
                                    chkRecoger.Checked = false;
                                    VerError("No puede recoger este credito ya que el Saldo Capital esta fuera del Rango establecido");
                                }
                            }
                            else if (pDatosLinea.tipo_refinancia == 2) //SI ES POR % SALDO
                            {
                                //RECUPERAR SALDO CAPITAL/MONTO
                                decimal saldoCapital = 0, monto = 0, valor = 0;
                                if (rFila.Cells[4].Text != "" && rFila.Cells[4].Text != "&nbsp;")
                                    saldoCapital = Convert.ToDecimal(rFila.Cells[4].Text);
                                if (rFila.Cells[3].Text != "" && rFila.Cells[3].Text != "&nbsp;")
                                    monto = Convert.ToDecimal(rFila.Cells[3].Text);
                                valor = saldoCapital / monto;
                                if (valor < minimo || valor > maximo)
                                {
                                    chkRecoger.Checked = false;
                                    VerError("No puede recoger este credito ya que el valor calculado esta fuera del Rango establecido");
                                }
                            }
                            else if (pDatosLinea.tipo_refinancia == 3) // SI ES POR % CUOTAS PAGAS
                            {
                                //RECUPERAR LAS CUOTAS PAGADAS
                                decimal cuotas = 0;
                                if (rFila.Cells[11].Text != "" && rFila.Cells[11].Text != "&nbsp;")
                                    cuotas = Convert.ToDecimal(rFila.Cells[11].Text);
                                if (cuotas < minimo || cuotas > maximo)
                                {
                                    chkRecoger.Checked = false;
                                    VerError("No puede recoger este credito ya que las cuotas Pagadas estan fuera del Rango establecido");
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void gvListaSolicitudCreditosRecogidos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CreditoRecoger cRec = new CreditoRecoger();
                cRec = (CreditoRecoger)e.Row.DataItem;
                CheckBoxGrid chkRecoger = new CheckBoxGrid();
                chkRecoger = (CheckBoxGrid)e.Row.Cells[8].FindControl("chkRecoger");
                if (cRec.recoger == true)
                    chkRecoger.Checked = true;
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosSolicitudServicio.CodigoProgramaModificacion + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvListaSolicitudCreditosRecogidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvListaSolicitudCreditosRecogidos.PageIndex = e.NewPageIndex;
            TablaCreditosRecogidos(Convert.ToInt64(Session["Numero_Radicacion"]));
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosSolicitudServicio.CodigoProgramaModificacion, "gvListaSolicitudCreditosRecogidos_PageIndexChanging", ex);
        }
    }

    protected void gvListaSolicitudCreditosRecogidos_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvListaSolicitudCreditosRecogidos.DataKeys[gvListaSolicitudCreditosRecogidos.SelectedRow.RowIndex].Value.ToString();
        Session[SolicitudCreditosRecogidosServicio.CodigoProgramaModificacion + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvListaSolicitudCreditosRecogidos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string a = Convert.ToString(e.ToString());
    }

    protected void gvListaSolicitudCreditosRecogidos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvListaSolicitudCreditosRecogidos.Rows[e.NewEditIndex].Cells[0].Text;
        Session[SolicitudCreditosRecogidosServicio.CodigoProgramaModificacion + ".id"] = id;
    }

    protected void gvListaSolicitudCreditosRecogidos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            SolicitudCreditosRecogidosServicio.EliminarSolicitudCreditosRecogidos(id, (Usuario)Session["usuario"]);
            TablaCreditosRecogidos(Convert.ToInt64(Session["Numero_Radicacion"]));
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosSolicitudServicio.CodigoProgramaModificacion, "gvLista_RowDeleting", ex);
        }
    }

    protected void txtIdentificacionprov_TextChanged(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            Persona1 pData = new Persona1();
            Persona1Service PersonaService = new Persona1Service();
            if (txtIdentificacionprov.Text != "")
            {
                pData.seleccionar = "Identificacion";
                pData.noTraerHuella = 1;
                pData.identificacion = txtIdentificacionprov.Text;
                pData = PersonaService.ConsultarPersona1Param(pData, (Usuario)Session["usuario"]);
                if (pData.nombres != null || pData.apellidos != null)
                {
                    string nombre = "", apellidos = "";
                    nombre = pData.nombres != null ? pData.nombres.Trim() : "";
                    apellidos = pData.apellidos != null ? pData.apellidos.Trim() : "";
                    txtNombreProveedor.Text = (nombre + " " + apellidos).Trim();
                }
                else
                {
                    txtNombreProveedor.Text = "";
                    VerError("Debe ingresar una identificación existente.");
                }
            }
            else
            {
                txtNombreProveedor.Text = "";
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void btnListadoPersona_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtIdentificacionprov", "txtNombreProveedor");
    }


    //cuotas extras
    protected void gvCuoExt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvCuoExt.PageIndex = e.NewPageIndex;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoProgramaCodeudor, "gvLista_PageIndexChanging", ex);
        }
    }
    protected void gvCuoExt_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<CuotasExtras> lstCuoExt = new List<CuotasExtras>();
        lstCuoExt = (List<CuotasExtras>)Session["CuoExt"];
        if (lstCuoExt.Count >= 1)
        {
            CuotasExtras eCuoExt = new CuotasExtras();
            int index = Convert.ToInt32(e.RowIndex);
            eCuoExt = lstCuoExt[index];
            CuoExtServicio.EliminarCuotasExtras(lstCuoExt[index].cod_cuota, (Usuario)Session["Usuario"]);
            if (eCuoExt.valor != 0 || eCuoExt.valor == null)
                lstCuoExt.Remove(eCuoExt);
        }
        if (lstCuoExt.Count == 0)
        {
            InicialCuoExt();
        }
        else
        {
            gvCuoExt.DataSource = lstCuoExt;
            gvCuoExt.DataBind();
            Session["CuoExt"] = lstCuoExt;
        }
    }

    protected void gvCuoExt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Control ctrl;
        List<CuotasExtras> lstCuoExt = new List<CuotasExtras>();

        if (e.Row.RowType != DataControlRowType.Footer)
        {
            ctrl = e.Row.FindControl("ddlformapago");
            lstCuoExt = (List<CuotasExtras>)Session["CuoExt"];
            if (ctrl != null)
            {
                foreach (GridViewRow row in gvCuoExt.Rows)
                {
                    Control ctls = row.FindControl("ddlFormaPago");
                    DropDownList ddlFormaPago = ctls as DropDownList;
                    foreach (var item in lstCuoExt)
                    {
                        ddlFormaPago.SelectedValue = item.forma_pago;
                    }
                }

            }
        }

        ctrl = e.Row.FindControl("ddltipocuotagv");
        if (ctrl != null)
        {
            DropDownList ddltipocuota = ctrl as DropDownList;
            ListaSolicitada = "TipoCuotaExtra";
            TraerResultadosListas();
            ddltipocuota.DataSource = lstDatosSolicitud;
            ddltipocuota.DataTextField = "ListaDescripcion";
            ddltipocuota.DataValueField = "ListaIdStr";
            ddltipocuota.DataBind();
        }

    }
    protected void InicialCuoExt()
    {
        List<Xpinn.FabricaCreditos.Entities.CuotasExtras> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.CuotasExtras>();
        Xpinn.FabricaCreditos.Entities.CuotasExtras eCuoExt = new Xpinn.FabricaCreditos.Entities.CuotasExtras();
        lstConsulta.Add(eCuoExt);
        gvCuoExt.DataSource = lstConsulta;
        gvCuoExt.DataBind();
        gvCuoExt.Visible = true;
    }

    private void TraerResultadosListas()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = Persona1Servicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
    }
    private void TablaCuoExt(String pIdObjeto)
    {
        try
        {
            List<CuotasExtras> lstConsulta = new List<CuotasExtras>();
            CuotasExtras eCuoExt = new CuotasExtras();
            lstConsulta = CuoExtServicio.ListarCuotasExtrasId(Convert.ToInt64(pIdObjeto), (Usuario)Session["Usuario"]);

            gvCuoExt.PageSize = 5;
            gvCuoExt.EmptyDataText = "No se encontraron registros";
            gvCuoExt.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {

                gvCuoExt.Visible = true;
                gvCuoExt.DataBind();
                ValidarPermisosGrilla(gvCuoExt);
                Session["CuoExt"] = lstConsulta;
                foreach (GridViewRow row in gvCuoExt.Rows)
                {
                    Control ctrl = row.FindControl("ddltipocuotagv");
                    DropDownList ddltipocuota = ctrl as DropDownList;
                    foreach (var item in lstConsulta)
                    {
                        string[] number = item.des_tipo_cuota.Split('-');
                        ddltipocuota.SelectedValue = number[0];
                    }
                    Control ctls = row.FindControl("ddlFormaPago");
                    DropDownList ddlFormaPago = ctls as DropDownList;
                    foreach (var item in lstConsulta)
                    {
                        ddlFormaPago.SelectedValue = item.forma_pago;
                    }
                }

            }
            else
            {
                Session["CuoExt"] = lstConsulta;
                gvCuoExt.Visible = false;
                InicialCuoExt();
            }

            Session.Add(DatosSolicitudServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosSolicitudServicio.CodigoPrograma, "Actualizar", ex);
        }

    }
    protected void gvCuoExt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtfechapago = (TextBox)gvCuoExt.FooterRow.FindControl("txtfechapago");
            DropDownList ddlformapago = (DropDownList)gvCuoExt.FooterRow.FindControl("ddlformapago");
            TextBox txtvalor = (TextBox)gvCuoExt.FooterRow.FindControl("txtvalor");
            DropDownList dlltipocuota = (DropDownList)gvCuoExt.FooterRow.FindControl("ddltipocuotagv");

            List<CuotasExtras> lstCuoExt = new List<CuotasExtras>();
            lstCuoExt = (List<CuotasExtras>)Session["CuoExt"];

            if (lstCuoExt.Count == 1)
            {
                CuotasExtras gItem = new CuotasExtras();
                gItem = lstCuoExt[0];
                if (gItem.valor == 0 || gItem.valor == null)
                    lstCuoExt.Remove(gItem);
            }

            CuotasExtras gItemNew = new CuotasExtras();
            if (txtfechapago.Text.Trim() == "" || txtvalor.Text.Trim() == "")
            {
                return;
            }
            gItemNew.fecha_pago = Convert.ToDateTime(txtfechapago.Text);
            gItemNew.forma_pago = ddlformapago.SelectedValue.ToString();
            gItemNew.des_forma_pago = ddlformapago.SelectedItem.ToString();
            gItemNew.valor = Convert.ToInt64(txtvalor.Text);
            gItemNew.des_tipo_cuota = dlltipocuota.SelectedValue.ToString() + "-" + dlltipocuota.SelectedItem.ToString();
            lstCuoExt.Add(gItemNew);
            // decimal total = Convert.ToDecimal(Session["TotalCuoExt"].ToString());
            // total = total + Convert.ToDecimal(gItemNew.valor);
            // Session["TotalCuoExt"] = total;
            gvCuoExt.DataSource = lstCuoExt;
            gvCuoExt.DataBind();
            Session["CuoExt"] = lstCuoExt;
        }
    }

    //MODIFICACION CODEUDORES
    protected void gvListaCodeudores_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<Persona1> lstCodeudores = new List<Persona1>();
        lstCodeudores = (List<Persona1>)Session[Usuario.codusuario + "Codeudores"];
        if (lstCodeudores.Count >= 1)
        {
            Persona1 eCodeudor = new Persona1();
            int index = Convert.ToInt32(e.RowIndex);
            eCodeudor = lstCodeudores[index];
            if (eCodeudor.cod_persona != 0)
            {
                lstCodeudores.Remove(eCodeudor); //PENDIENTE
                                                 //CodeudorServicio.EliminarcodeudoresCred(eCodeudor.cod_persona, Convert.ToInt64(txtCredito.Text), (Usuario)Session["usuario"]);
            }
            Session[Usuario.codusuario + "Codeudores"] = lstCodeudores;
        }
        if (lstCodeudores.Count == 0)
        {
            lblTotReg.Visible = false;
            lblTotalRegsCodeudores.Visible = true;
            InicialCodeudores();
        }
        else
        {
            lblTotReg.Visible = true;
            lblTotReg.Text = "<br/> Codeudores a registrar : " + lstCodeudores.Count.ToString();
            lblTotalRegsCodeudores.Visible = false;
            gvListaCodeudores.DataSource = lstCodeudores;
            gvListaCodeudores.DataBind();
            Session[Usuario.codusuario + "Codeudores"] = lstCodeudores;
            ObtenerSiguienteOrden();
        }
        BorrarReferenciaCodeudorBorradoGVReferencias(lstCodeudores);
    }

    protected void gvListaCodeudores_RowEditing(object sender, GridViewEditEventArgs e)
    {
        // GENERAR EDICION
        gvListaCodeudores.EditIndex = e.NewEditIndex;
        string id = gvListaCodeudores.DataKeys[e.NewEditIndex].Value.ToString();
        if (Session[Usuario.codusuario + "Codeudores"] != null)
        {
            gvListaCodeudores.DataSource = Session[Usuario.codusuario + "Codeudores"];
            gvListaCodeudores.DataBind();
            ObtenerSiguienteOrden();
        }
        else
            InicialCodeudores();

        OcultarGridFooter(gvListaCodeudores, false);
    }

    protected void gvListaCodeudores_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        // GENERAR REVERSION
        gvListaCodeudores.EditIndex = -1;
        if (Session[Usuario.codusuario + "Codeudores"] != null)
        {
            gvListaCodeudores.DataSource = Session[Usuario.codusuario + "Codeudores"];
            gvListaCodeudores.DataBind();
            ObtenerSiguienteOrden();
        }
        else
            InicialCodeudores();
        OcultarGridFooter(gvListaCodeudores, true);
    }

    protected void gvListaCodeudores_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        VerError("");
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtidentificacion = (TextBox)gvListaCodeudores.FooterRow.FindControl("txtidentificacion");
            TextBox txtOdenFooter = (TextBox)gvListaCodeudores.FooterRow.FindControl("txtOdenFooter");
            if (txtidentificacion.Text.Trim() == "")
            {
                VerError("Ingrese la Identificación del Codeudor a Agregar por favor.");
                return;
            }
            if (string.IsNullOrEmpty(txtOdenFooter.Text))
            {
                VerError("Ingrese el orden del codeudor por favor.");
                return;
            }
            string IdentifSolic = ((Label)Master.FindControl("lblIdCliente")).Text;
            if (IdentifSolic.Trim() == txtidentificacion.Text.Trim())
            {
                VerError("No puede ingresar como codeudor a la persona solicitante.");
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
            if (Session[Usuario.codusuario + "Codeudores"] != null)
            {
                lstCodeudor = (List<Persona1>)Session[Usuario.codusuario + "Codeudores"];

                if (lstCodeudor.Count == 1)
                {
                    // si no se adicionón ningún codeudor entonces quita el que se creo para inicializar la gridView porque es vacio
                    Persona1 gItem = new Persona1();
                    gItem = lstCodeudor[0];
                    if (gItem.cod_persona == 0)
                        lstCodeudor.Remove(gItem);
                }
            }

            Xpinn.FabricaCreditos.Entities.codeudores vcodeudor = new Xpinn.FabricaCreditos.Entities.codeudores();
            vcodeudor = CodeudorServicio.ConsultarDatosCodeudor(txtidentificacion.Text, (Usuario)Session["usuario"]);
            Persona1 gItemNew = new Persona1();
            gItemNew.cod_persona = vcodeudor.codpersona;
            gItemNew.identificacion = vcodeudor.identificacion;
            gItemNew.primer_nombre = vcodeudor.primer_nombre;
            gItemNew.segundo_nombre = vcodeudor.segundo_nombre;
            gItemNew.primer_apellido = vcodeudor.primer_apellido;
            gItemNew.segundo_apellido = vcodeudor.segundo_apellido;
            gItemNew.direccion = vcodeudor.direccion;
            gItemNew.telefono = vcodeudor.telefono;
            gItemNew.orden = Convert.ToInt32(txtOdenFooter.Text);

            // validar que no existe el mismo codeudor en la gridview
            // PENDIENTE VALIDAR
            bool isValid = gvListaCodeudores.Rows.OfType<GridViewRow>().Where(x => ((Label)x.FindControl("lblCodPersona")).Text == gItemNew.cod_persona.ToString()).Any();
            if (!isValid)
                lstCodeudor.Add(gItemNew);

            gvListaCodeudores.DataSource = lstCodeudor;
            gvListaCodeudores.DataBind();
            Session[Usuario.codusuario + "Codeudores"] = lstCodeudor;
            if (lstCodeudor.Count > 0)
            {
                lblTotReg.Visible = true;
                lblTotReg.Text = "<br/> Codeudores a registrar : " + lstCodeudor.Count.ToString();
                lblTotalRegsCodeudores.Visible = false;
            }
            else
            {
                lblTotReg.Visible = false;
                lblTotalRegsCodeudores.Visible = true;
            }

            LlenarDDLQuienReferenciaGVReferencias(lstCodeudor);
            ObtenerSiguienteOrden();
        }
    }
    protected void InicialCodeudores()
    {
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        Xpinn.FabricaCreditos.Entities.Persona1 eCodeudor = new Xpinn.FabricaCreditos.Entities.Persona1();
        lstConsulta.Add(eCodeudor);
        Session[Usuario.codusuario + "Codeudores"] = lstConsulta;
        gvListaCodeudores.DataSource = lstConsulta;
        gvListaCodeudores.DataBind();
        TablaCodeudores();
        ObtenerSiguienteOrden();
    }

    private Xpinn.FabricaCreditos.Entities.Persona1 ObtenerValoresCodeudores()
    {
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        var Numero_Radicacion = Session["Numero_radicado"];
        if (idObjeto != "")
            vPersona1.numeroRadicacion = Convert.ToInt64(Numero_Radicacion);

        vPersona1.seleccionar = "CD"; //Bandera para ejecuta el select del CODEUDOR

        return vPersona1;
    }
    private void TablaCodeudores()
    {
        try
        {
            Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
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
                Session[Usuario.codusuario + "Codeudores"] = lstConsulta;
                ObtenerSiguienteOrden();
            }
            else
            {
                idObjeto = "";
                gvListaCodeudores.Visible = false;
                lblTotalRegsCodeudores.Text = "No hay codeudores para este crédito";
                lblTotalRegsCodeudores.Visible = true;
                // InicialCodeudores();
            }

            Session.Add(Persona1Servicio.CodigoProgramaCodeudor + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoProgramaModifi, "Actualizar", ex);
        }

    }
    protected void ObtenerSiguienteOrden()
    {
        var maxValue = 0;
        if (Session[Usuario.codusuario + "Codeudores"] != null)
        {
            List<Persona1> lstCodeudor = (List<Persona1>)Session[Usuario.codusuario + "Codeudores"];
            maxValue = lstCodeudor.Max(x => x.orden);
        }
       ((TextBox)gvListaCodeudores.FooterRow.FindControl("txtOdenFooter")).Text = (maxValue + 1).ToString();
    }

    private List<Referncias> RecorresGrillaReferencias()
    {
        List<Referncias> lstReferencia = new List<Referncias>();

        foreach (GridViewRow gFila in gvReferencias.Rows)
        {
            Referncias referencia = new Referncias()
            {
                cod_persona_quien_referencia = Convert.ToInt64(((DropDownList)gFila.FindControl("ddlQuienReferencia")).SelectedValue),
                tiporeferencia = Convert.ToInt64(((DropDownList)gFila.FindControl("ddlTipoReferencia")).SelectedValue),
                nombres = ((TextBox)gFila.FindControl("txtNombres")).Text,
                codparentesco = Convert.ToInt64(((DropDownList)gFila.FindControl("ddlParentesco")).SelectedValue),
                direccion = ((TextBox)gFila.FindControl("txtDireccion")).Text,
                telefono = ((TextBox)gFila.FindControl("txtTelefono")).Text,
                teloficina = ((TextBox)gFila.FindControl("txtTelOficina")).Text,
                celular = ((TextBox)gFila.FindControl("txtCelular")).Text
            };

            lstReferencia.Add(referencia);
        }

        return lstReferencia;
    }
    private void BorrarReferenciaCodeudorBorradoGVReferencias(List<Persona1> lstCodeudores)
    {
        List<long> lstIDCodeudores = lstCodeudores.Select(x => x.cod_persona).ToList();
        lstIDCodeudores.Add(_codPersona);

        List<Referncias> lstReferencia = RecorresGrillaReferencias();

        lstReferencia = lstReferencia.Where(x => lstIDCodeudores.Contains(x.cod_persona_quien_referencia)).ToList();
        long[] idSeleccionado = lstReferencia.Select(x => x.cod_persona_quien_referencia).ToArray();

        if (lstReferencia.Count == 0)
        {
            lstReferencia.Add(new Referncias());
        }

        gvReferencias.DataSource = lstReferencia;
        //gvReferencias.DataBind();

        LlenarDDLQuienReferenciaGVReferencias(lstCodeudores, idSeleccionado);
    }

    private void LlenarDDLQuienReferenciaGVReferencias(List<Persona1> lstCodeudor, long[] idSeleccionado = null)
    {
        int contador = 0;
        var listaABindearDDL = (from codeudor in lstCodeudor
                                where codeudor.cod_persona != 0
                                select codeudor).ToList();

        Persona1 deudor = new Persona1() { primer_nombre = "Solicitante", cod_persona = _codPersona };
        listaABindearDDL.Add(deudor);

        foreach (GridViewRow row in gvReferencias.Rows)
        {
            DropDownList ddlQuienReferencia = row.Cells[1].FindControl("ddlQuienReferencia") as DropDownList;

            if (ddlQuienReferencia != null)
            {
                var valueSeleccionadoEnDDL = ddlQuienReferencia.SelectedValue;

                ddlQuienReferencia.DataSource = listaABindearDDL;
                ddlQuienReferencia.DataTextField = "nombreYApellido";
                ddlQuienReferencia.DataValueField = "cod_persona";
                ddlQuienReferencia.DataBind();

                if (idSeleccionado != null)
                {
                    ddlQuienReferencia.SelectedValue = idSeleccionado[contador].ToString();
                    contador += 1;
                }
                else
                {
                    ddlQuienReferencia.SelectedValue = valueSeleccionadoEnDDL;
                }
            }
            else
            {
                VerError("Ocurrio un error al agregar la referencia del codeudor, LlenarDDLQuienReferenciaGVReferencias");
                return;
            }
        }
    }

    protected void gvListaCodeudores_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        gvListaCodeudores.EditIndex = -1;
        if (Session[Usuario.codusuario + "Codeudores"] != null)
        {
            TextBox txtOrdenRow = (TextBox)gvListaCodeudores.Rows[e.RowIndex].FindControl("txtOrdenRow");
            if (string.IsNullOrEmpty(txtOrdenRow.Text))
            {
                VerError("Ingrese el orden al que pertenece el codeudor.");
                return;
            }
            List<Persona1> lstCodeudores = (List<Persona1>)Session[Usuario.codusuario + "Codeudores"];
            lstCodeudores[e.RowIndex].orden = Convert.ToInt32(txtOrdenRow.Text);
            gvListaCodeudores.DataSource = lstCodeudores;
            gvListaCodeudores.DataBind();
            Session[Usuario.codusuario + "Codeudores"] = lstCodeudores;
            ObtenerSiguienteOrden();
        }
        else
            InicialCodeudores();
        OcultarGridFooter(gvListaCodeudores, true);
    }

    protected void txtidentificacion_TextChanged(object sender, EventArgs e)
    {
        Control ctrl = gvListaCodeudores.FooterRow.FindControl("txtidentificacion");
        if (ctrl != null)
        {
            TextBox txtidentificacion = (TextBox)ctrl;
            if (txtidentificacion.Text != "")
            {
                Xpinn.FabricaCreditos.Services.codeudoresService codeudorServicio = new Xpinn.FabricaCreditos.Services.codeudoresService();
                Xpinn.FabricaCreditos.Entities.codeudores vcodeudor = new Xpinn.FabricaCreditos.Entities.codeudores();
                vcodeudor = codeudorServicio.ConsultarDatosCodeudor(txtidentificacion.Text, (Usuario)Session["usuario"]);
                if (vcodeudor.codpersona != 0)
                {
                    ((Label)gvListaCodeudores.FooterRow.FindControl("lblCodPersonaFooter")).Text = vcodeudor.codpersona.ToString();
                    gvListaCodeudores.FooterRow.Cells[4].Text = vcodeudor.primer_nombre;
                    gvListaCodeudores.FooterRow.Cells[5].Text = vcodeudor.segundo_nombre;
                    gvListaCodeudores.FooterRow.Cells[6].Text = vcodeudor.primer_apellido;
                    gvListaCodeudores.FooterRow.Cells[7].Text = vcodeudor.segundo_apellido;
                    gvListaCodeudores.FooterRow.Cells[8].Text = vcodeudor.direccion;
                    gvListaCodeudores.FooterRow.Cells[9].Text = vcodeudor.telefono;
                }
                else
                {
                    ((Label)gvListaCodeudores.FooterRow.FindControl("lblCodPersonaFooter")).ForeColor = System.Drawing.Color.Red;
                    string pagina = "";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:validar();", true);
                }
            }
            else
            {
                ((Label)gvListaCodeudores.FooterRow.FindControl("lblCodPersonaFooter")).Text = "";
                gvListaCodeudores.FooterRow.Cells[4].Text = "";
                gvListaCodeudores.FooterRow.Cells[5].Text = "";
                gvListaCodeudores.FooterRow.Cells[6].Text = "";
                gvListaCodeudores.FooterRow.Cells[7].Text = "";
                gvListaCodeudores.FooterRow.Cells[8].Text = "";
                gvListaCodeudores.FooterRow.Cells[9].Text = "";
            }
        }
    }

    protected List<codeudores> ObtenerListaCodeudores()
    {
        List<codeudores> lstCodeudores = null;
        List<Persona1> lstRegCodeu = (List<Persona1>)Session[Usuario.codusuario + "Codeudores"];

        if (lstRegCodeu != null)
        {
            lstCodeudores = (from codeudor in lstRegCodeu select codeudor)
                        .Select(x => new codeudores()
                        {
                            idcodeud = 0,
                            numero_radicacion = long.Parse(lblradicacion.Text),
                            codpersona = x.cod_persona,
                            tipo_codeudor = "C",
                            parentesco = 1,
                            opinion = "B",
                            responsabilidad = "S",
                            orden = x.orden
                        }).ToList();
        }

        return lstCodeudores;
    }
    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        BusquedaRapida ctlBusquedaPersonas = (BusquedaRapida)gvListaCodeudores.FooterRow.FindControl("ctlBusquedaPersonas");
        ctlBusquedaPersonas.Motrar(true, "txtidentificacion", "");
    }

    //REFERENCIAS MODIFICAR 

    protected void btnAgregarReferencia_Click(object sender, EventArgs e)
    {
        List<Referncias> lstReferencia = RecorresGrillaReferencias();

        lstReferencia.Insert(0, new Referncias() { tiporeferencia = 1, cod_persona_quien_referencia = _codPersona });
        var idSeleccionado = lstReferencia.Select(x => x.cod_persona_quien_referencia).ToArray();

        gvReferencias.DataSource = lstReferencia;
        gvReferencias.DataBind();

        LlenarDDLQuienReferenciaGVReferencias(((List<Persona1>)Session[Usuario.codusuario + "Codeudores"]), idSeleccionado);
    }

    protected void gvReferencia_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        List<Referncias> lstReferencia = RecorresGrillaReferencias();

        lstReferencia.RemoveAt(Convert.ToInt32(e.CommandArgument));
        var idSeleccionado = lstReferencia.Select(x => x.cod_persona_quien_referencia).ToArray();

        gvReferencias.DataSource = lstReferencia;
        gvReferencias.DataBind();

        LlenarDDLQuienReferenciaGVReferencias(((List<Persona1>)Session[Usuario.codusuario + "Codeudores"]), idSeleccionado);
    }

    protected void gvReferencias_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void ddlTipoReferencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        var ddlTipoReferenciaEvent = sender as DropDownListGrid;
        int rowIndex = Convert.ToInt32(ddlTipoReferenciaEvent.CommandArgument);

        var ddlParentesco = gvReferencias.Rows[rowIndex].FindControl("ddlParentesco") as DropDownList;

        var selectedValue = Convert.ToInt32(ddlTipoReferenciaEvent.SelectedValue);

        if (ddlTipoReferenciaEvent != null && selectedValue != (int)TipoReferencia.Familiar)
        {
            ddlParentesco.SelectedValue = "0";
            ddlParentesco.Enabled = false;
        }
        else
        {
            ddlParentesco.Enabled = true;
        }
    }

    protected List<Referncias> ListarParentesco()
    {
        RefernciasService lineasServicio = new RefernciasService();
        List<Referncias> lstAtributos = lineasServicio.ListasDesplegables("Parentesco", (Usuario)Session["Usuario"]);

        return lstAtributos;
    }
}