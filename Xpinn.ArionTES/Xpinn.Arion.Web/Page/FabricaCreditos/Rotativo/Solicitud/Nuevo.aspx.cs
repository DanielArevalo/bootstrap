using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Globalization;
using System.IO;
using Xpinn.Util;
using System.Web.UI.HtmlControls;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;
using Xpinn.Comun.Services;
using Xpinn.Comun.Entities;
using System.Data.Common;

public partial class Nuevo : GlobalWeb
{
    Xpinn.FabricaCreditos.Services.CreditoSolicitadoService creditoServicio = new Xpinn.FabricaCreditos.Services.CreditoSolicitadoService();
    Xpinn.Tesoreria.Entities.EmpresaRecaudo empresa = new Xpinn.Tesoreria.Entities.EmpresaRecaudo();
    Xpinn.Tesoreria.Services.EmpresaRecaudoServices empresaServicio = new Xpinn.Tesoreria.Services.EmpresaRecaudoServices();
    Avance vDetallePlazo = new Avance();
    Avance vDetallePlazoMax = new Avance();
    AvanceService AvancServices = new AvanceService();

    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables
    String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    Xpinn.FabricaCreditos.Services.DatosSolicitudService DatosSolicitudServicio = new Xpinn.FabricaCreditos.Services.DatosSolicitudService();
    Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
    String operacion;
    decimal montomaximo = 0;
    decimal montomosolitado = 0;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(creditoServicio.CodigoProgramaRotativo, "E");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.GetType().Name + "E", "Page_PreInit", ex);
        }


    }
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (ValidarDatos())
        {
            VerError("");

            // Guarda los datos de los datos de la solicitud
            if (txtMonto.Text != "0" && txtPlazo.Text != "0" && txtMonto.Text != "" && txtPlazo.Text != "")
            {
                long codigoCliente = Convert.ToInt64(Session["codigocliente"]);
                string tipoCredito = ddlTipoCredito.SelectedValue;
                Usuario usuario = (Usuario)Session["usuario"];

                if (!ValidarNumCreditosPorLinea(tipoCredito, null, codigoCliente, usuario))
                    return;

                Guardar();
            }
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

                // Llenar datos del cliente 

                lblNumeroCreditoRad.Visible = false;
                lblcredito.Visible = false;
                ddlEmpresa.Visible = false;
                lblEmpresa.Visible = false;
                // Llenar información de los combos o listas desplegables
                CargarListas();
                Calcular_Cupo();
                // Determinar datos de encabezado de la solicitud

                // Cargar el código de la persona si se paso desde la consulta de datacredito
                if (Session[creditoServicio.CodigoProgramaRotativo + ".id"] != null)
                {
                    idObjeto = Session[creditoServicio.CodigoProgramaRotativo + ".id"].ToString();
                }
                operacion = (String)Session["operacion"];
                if (operacion == "E")
                {
                    MvAfiliados.ActiveViewIndex = 0;
                    txtidentificacion.Text = Convert.ToString(Session["identificacion"]);
                    lblNumeroCreditoRad.Visible = true;
                    lblNumeroCreditoRad.Text = Convert.ToString(Session["id"]);
                    txtCodigoCliente.Text = Convert.ToString(Session["codigocliente"]);
                    lblcredito.Visible = true;
                    txtMonto.Enabled = false;
                    txtPlazo.Enabled = false;
                    ddlPeriodicidad.Enabled = false;
                    txtConcepto.Enabled = false;
                    Ddlusuarios.Enabled = false;
                    ddlTipoLiquidacion.Enabled = false;
                    ddlFormaPago.Enabled = false;
                    ddlEmpresa.Enabled = false;
                    this.ddlTipoCredito.Enabled = false;
                    ObtenerDatos(lblNumeroCreditoRad.Text);
                    Site toolBar1 = (Site)this.Master;
                    toolBar1.MostrarGuardar(false);
                }
                if (operacion == "N")
                {
                    if (Session[creditoServicio.CodigoProgramaRotativo + ".id"] != null)
                    {
                        idObjeto = Session[creditoServicio.CodigoProgramaRotativo + ".id"].ToString();
                    }
                    txtCodigoCliente.Text = Convert.ToString(Session["codigocliente"]);
                    MvAfiliados.ActiveViewIndex = 0;
                    txtidentificacion.Text = Convert.ToString(Session["Identificacion"]);
                    txtCodigoCliente.Text = idObjeto;
                    txtNombreCLiente.Text = Convert.ToString(Session["Nombre"]);
                }
                if (operacion == null)
                {

                    lblNumeroCreditoRad.Visible = false;
                }

                Usuario usuap = (Usuario)Session["usuario"];
                lblOficina.Text = usuap.nombre_oficina;
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosSolicitudServicio.GetType().Name + "D", "Page_Load", ex);
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
            CreditoSolicitadoService lstConsulta = new CreditoSolicitadoService();
            CreditoSolicitado credito = new CreditoSolicitado();
            DateTime pFecha;
            Int64 numsolicitud = 0;
            Int64 codcliente = 0;

            credito.NumeroCredito = Convert.ToInt32(Session["id"]);

            credito = creditoServicio.ConsultarCreditosRotativos(credito, (Usuario)Session["usuario"]);
            if (credito.CodigoCliente != 0)
                codcliente = credito.Cod_deudor;
          //  Session["codigocliente"] = codcliente;
            if (credito.numsolicitud != 0)
                numsolicitud = credito.numsolicitud;
            Session["solicitud"] = numsolicitud;
            if (credito.fechasolicitud != "")
                this.txfechasolicitud.Texto = Convert.ToDateTime(credito.fecha).ToShortDateString();
            if (credito.cod_oficina != 0)
                lblOficina.Text = Convert.ToString(credito.cod_oficina);
            if (credito.Monto != 0)
                txtMonto.Text = Convert.ToString(credito.Monto);

          



            if (credito.Plazo != "")
                txtPlazo.Text = Convert.ToString(credito.Plazo);

            if (credito.cod_Periodicidad != 0)
                ddlPeriodicidad.SelectedValue = Convert.ToString(credito.cod_Periodicidad);

            txtNombreCLiente.Text = Convert.ToString(Session["nombre"]);

            if (credito.Cod_asesor != 0)
                Ddlusuarios.SelectedValue = Convert.ToString(credito.Cod_asesor);

            if (credito.forma_pago != "")
                ddlFormaPago.SelectedValue = Convert.ToString(credito.forma_pago);
            if (ddlFormaPago.SelectedItem.Text != "Caja")
            {
                lblEmpresa.Visible = true;
                ddlEmpresa.Visible = true;
            }
            else
            {
                lblEmpresa.Visible = false;
                ddlEmpresa.Visible = false;
            }
            if (credito.cod_empresa != 0)
                ddlEmpresa.SelectedValue = Convert.ToString(credito.cod_empresa);

            if (credito.Tipo_liqu != 0)
                ddlTipoLiquidacion.SelectedValue = Convert.ToString(credito.Tipo_liqu);

            if (credito.observaciones != "")
                this.txtConcepto.Text = credito.observaciones;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosSolicitudServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }


    /// <summary>
    /// Cargar información de las listas desplegables
    /// </summary>
    private void CargarListas()
    {
        try
        {
            ListaSolicitada = "CreditoRotativo";
            TraerResultadosLista();
            ddlTipoCredito.DataSource = lstDatosSolicitud;
            ddlTipoCredito.DataTextField = "ListaDescripcion";
            ddlTipoCredito.DataValueField = "ListaId";
            ddlTipoCredito.DataBind();

            ListaSolicitada = "Periodicidad";
            TraerResultadosLista();
            ddlPeriodicidad.DataSource = lstDatosSolicitud;
            ddlPeriodicidad.DataTextField = "ListaDescripcion";
            ddlPeriodicidad.DataValueField = "ListaIdStr";
            ddlPeriodicidad.DataBind();

            ListaSolicitada = "TipoLiquidacion";
            TraerResultadosLista();
            ddlTipoLiquidacion.DataSource = lstDatosSolicitud;
            ddlTipoLiquidacion.DataTextField = "ListaDescripcion";
            ddlTipoLiquidacion.DataValueField = "ListaId";
            ddlTipoLiquidacion.DataBind();

            ListItem selectedListItem2 = ddlPeriodicidad.Items.FindByValue("1"); //Selecciona mensual por defecto
            if (selectedListItem2 != null)
                selectedListItem2.Selected = true;

            ListItem selectedTipoLiq = ddlTipoLiquidacion.Items.FindByValue("3"); // Seleccionar tipo de liquidación por defecto
            if (selectedTipoLiq != null)
                selectedTipoLiq.Selected = true;

            string ddlusuarios = ConfigurationManager.AppSettings["ddlusuarios"].ToString();
            if (ddlusuarios == "1")
            {
                VerError("");
                // Validando la lìnea
                if (ddlTipoCredito.SelectedValue.Trim() == "")
                {
                    lblMensaje2.Text = "Debe seleccionar la línea de crédito";
                    return;
                }
                if (Ddlusuarios.SelectedValue.Trim() == "0")
                {
                    VerError("Debe seleccionar un asesor");
                    return;
                }

                // Guarda los datos de los datos de la solicitud
                if (txtMonto.Text != "0" && txtPlazo.Text != "0" && txtMonto.Text != "" && txtPlazo.Text != "")
                    Guardar();
                asesores();
            }
            Calcular_Cupo();

            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["usuario"];
            List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstEmpresas = new List<Xpinn.Tesoreria.Entities.EmpresaRecaudo>();
          //  ddlEmpresa.DataSource = empresaServicio.ListarEmpresaRecaudo(empresa, pUsuario);
            //ddlEmpresa.DataTextField = "nom_empresa";
            //ddlEmpresa.DataValueField = "cod_empresa";
            //ddlEmpresa.DataBind();
            ddlFormaPago_SelectedIndexChanged(null, null);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }


    private void asesores()
    {

        UsuarioAseService serviceEjecutivo = new UsuarioAseService();
        UsuarioAse ejec = new UsuarioAse();
        Ddlusuarios.DataSource = serviceEjecutivo.ListartodosUsuarios(ejec, (Usuario)Session["usuario"]);
        Ddlusuarios.DataTextField = "nombre";
        Ddlusuarios.DataValueField = "codusuario";
        Ddlusuarios.DataBind();
        Ddlusuarios.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["usuario"]);
    }

    /// <summary>
    ///  Deshabilita los campos una vez grabados
    /// </summary>
    private void deshabilitaText()
    {
        txtMonto.Enabled = false;
        txtPlazo.Enabled = false;
        ddlTipoCredito.Enabled = false;
        ddlPeriodicidad.Enabled = false;
        //txtCliente.Enabled = false;
    }

    /// <summary>
    /// Guardar información de la solicitud de crédito
    /// </summary>
    private void Guardar()
    {
       

        VerError("");
        string error = "";
        if (operacion == null)
        {
            this.txtidentificacion.Text = Convert.ToString(Session["identificacion"]);
            this.txtNombreCLiente.Text = Convert.ToString(Session["nombre"]);
            this.txtCodigoCliente.Text = Convert.ToString(Session["codigocliente"]);
        }

        Xpinn.FabricaCreditos.Entities.DatosSolicitud datosSolicitud = new Xpinn.FabricaCreditos.Entities.DatosSolicitud();
        Xpinn.FabricaCreditos.Entities.DatosSolicitud datosSCredito = new Xpinn.FabricaCreditos.Entities.DatosSolicitud();

        try
        {
            // Determinando datos de la solicitud
            datosSolicitud.identificacion = txtidentificacion.Text;
            datosSolicitud.fechasolicitud = Convert.ToDateTime(txfechasolicitud.ToDate);
            datosSolicitud.cod_cliente = txtCodigoCliente.Text;
            datosSolicitud.montosolicitado = txtMonto.Text != "" ? Convert.ToInt64(txtMonto.Text.Trim().Replace(".", "")) : 0;
            datosSolicitud.plazosolicitado = txtPlazo.Text != "" ? Convert.ToInt64(txtPlazo.Text.Trim().Replace(".", "")) : 0;
            datosSolicitud.tipocrdito = Convert.ToString(ddlTipoCredito.SelectedValue);
            if (ddlPeriodicidad.Text != "")
                datosSolicitud.periodicidad = Convert.ToInt64(ddlPeriodicidad.SelectedValue);
            datosSolicitud.concepto = txtConcepto.Text.Trim();
            datosSolicitud.forma_pago = Convert.ToInt64(ddlFormaPago.SelectedValue);
            datosSolicitud.tipo_liquidacion = Convert.ToInt64(ddlTipoLiquidacion.SelectedValue);
            datosSolicitud.forma_pago = Convert.ToInt64(ddlFormaPago.SelectedValue);
            if (datosSolicitud.forma_pago == 2)
                datosSolicitud.empresa_recaudo = Convert.ToInt64(ddlEmpresa.SelectedValue);
            else
                datosSolicitud.empresa_recaudo = null;
            // datos en 0
            datosSolicitud.cuotasolicitada = 0;
            datosSolicitud.medio = 0;
            datosSolicitud.otro = null;
            datosSolicitud.garantia = 0;
            datosSolicitud.garantia_comunitaria = 0;
            datosSolicitud.poliza = 0;
            datosSolicitud.identificacionprov = null;
            datosSolicitud.nombreprov = null;
            datosSolicitud.destino = 0;
            datosSolicitud.linea = Convert.ToString(ddlTipoCredito.SelectedValue);
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

            // Validar los datos de la solicitud
            string sError = "";
            datosSolicitud = DatosSolicitudServicio.ValidarSolicitudRotativo(datosSolicitud, (Usuario)Session["usuario"], ref sError);
            if (sError.Trim() != "")
            {
                VerError("No se pudieron validar datos de la solicitud. Error:" + sError);
                return;
            }

            if (lblNumeroCreditoRad.Text == "")
            {
                Session.Add(DatosSolicitudServicio.CodigoProgramaRotativo + ".insertar", 1);
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(true);


                datosSolicitud.numerosolicitud = 0;
                // Crear solicitud
                datosSolicitud = DatosSolicitudServicio.CrearSolicitud(datosSolicitud, (Usuario)Session["usuario"]);


                lblMensaje2.Text = "Su solicitud de credito se ha creado correctamente ";
            }

            if (sError.Trim() != "")
            {
                VerError("No se pudieron validar datos de la solicitud. Error:" + sError);
                return;
            }
            // Crear credito 
            datosSCredito = DatosSolicitudServicio.CrearRadicadoRotativo(datosSolicitud, (Usuario)Session["usuario"]);
            if (sError.Trim() != "")
            {
                VerError("No se pudieron validar datos de la solicitud. Error:" + sError);
                return;
            }
           
          

            Session.Add(DatosSolicitudServicio.CodigoProgramaRotativo + ".insertar", 1);
            Site toolBar1 = (Site)this.Master;
            toolBar1.MostrarGuardar(false);
            MvAfiliados.ActiveViewIndex = 1;
            // Mostrando informaciòn del nùmero de solicitud grabada
            lblNumeroSolicitud.Text = datosSolicitud.numerosolicitud.ToString();
            lblNumeroCredito.Text = datosSCredito.numeroradicado.ToString();

            deshabilitaText();
            lblMensaje2.Text = "Su solicitud de credito se ha registrado con el número " + lblNumeroCredito.Text;
            Session["NumeroSolicitud"] = lblNumeroSolicitud.Text;
            Session["Solicitud"] = lblNumeroSolicitud.Text.ToString().Trim();
            Session["numero_radicacion"] = lblNumeroCredito.Text.ToString().Trim();
            Session["Nombre"] = "";
            Session.Remove("Identificacion");
            Session.Remove("Nombre");
            Session.Remove("operacion");


        }

        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosSolicitudServicio.GetType().Name + "A", "btnGuardar_Click", ex);
        }


    }
    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        operacion = (String)Session["operacion"];
        if (operacion == "E")
            Navegar("~/Page/FabricaCreditos/Rotativo/Reporte/Lista.aspx");
        else
            Navegar(Pagina.Lista);
    }
    protected void ddlTipoCredito_TextChanged(object sender, EventArgs e)
    {
        Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();

        LineasCredito eLinea = new LineasCredito();
        eLinea = LineaCreditoServicio.ConsultarLineasCredito(ddlTipoCredito.SelectedValue.ToString(), (Usuario)Session["usuario"]);
        ddlTipoLiquidacion.SelectedValue = eLinea.tipo_liquidacion.ToString();
        Calcular_Cupo();
    }
    private void Calcular_Cupo()
    {
        Xpinn.FabricaCreditos.Entities.LineasCredito DatosLinea = new Xpinn.FabricaCreditos.Entities.LineasCredito();
        Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();

        Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCredito = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        try
        {
            if (!string.Equals(txtCodigoCliente.Text, "") && !string.Equals(this.txfechasolicitud.Texto, ""))
                DatosLinea = LineaCredito.Calcular_Cupo(ddlTipoCredito.SelectedValue.ToString(), Convert.ToInt64(txtCodigoCliente.Text), Convert.ToDateTime(txfechasolicitud.Texto), (Usuario)Session["usuario"]);
            else
                DatosLinea = LineaCredito.Calcular_Cupo(ddlTipoCredito.SelectedValue.ToString(), 0, DateTime.Today, (Usuario)Session["usuario"]);
            txtPlazoMaximo.Text = DatosLinea.Plazo_Maximo.ToString();
            txtMontoMaximo.Text = String.Format("{0:C}", DatosLinea.Monto_Maximo);
            txtMontomax.Text = (DatosLinea.Monto_Maximo.ToString());           
            txfechasolicitud.Texto = DateTime.Today.ToShortDateString();

            // calcular tipo de liquidacion
            LineasCredito eLinea = new LineasCredito();
            eLinea = LineaCreditoServicio.ConsultarLineasCredito(ddlTipoCredito.SelectedValue.ToString(), (Usuario)Session["usuario"]);
            ddlTipoLiquidacion.SelectedValue = eLinea.tipo_liquidacion.ToString();


            vDetallePlazo = AvancServices.ConsultarPlazoCreditoTotativo(Convert.ToString(ddlTipoCredito.SelectedItem.Text), (Usuario)Session["usuario"]);
            // vDetallePlazoMax = AvancServices.ConsultarPlazoMaximoCredito(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);



            if (vDetallePlazo.diferir == 1)
            {
                txtPlazo.Enabled = true;
            
            }

            if (vDetallePlazo.diferir == 0)
            {
                txtPlazo.Enabled = false;
                txtPlazo.Text = txtPlazoMaximo.Text;
                // txtPlazo.Text = Convert.ToString(vDetallePlazo.plazo_maximo);
            }



        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }
    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFormaPago.SelectedItem.Text == "Caja")
        {
            lblEmpresa.Visible = false;
            ddlEmpresa.Visible = false;
        }
        if (ddlFormaPago.SelectedItem.Text == "Nomina")
        {
            lblEmpresa.Visible = false;
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["usuario"];
            Int64 Cod_persona = Convert.ToInt64(Session["codigocliente"].ToString());
            ddlEmpresa.DataSource = empresaServicio.ListarEmpresaRecaudoPersona(Cod_persona, pUsuario);
            ddlEmpresa.DataTextField = "nom_empresa";
            ddlEmpresa.DataValueField = "cod_empresa";
            ddlEmpresa.AppendDataBoundItems = true;
            //ddlEmpresa.Items.Insert(0, new ListItem("Seleccione un item", "0"));
           // ddlEmpresa.SelectedIndex = 0;
            ddlEmpresa.DataBind();

            ddlEmpresa.Visible = true;

        }

    }
    protected void mvDatosSolicitud_ActiveViewChanged(object sender, EventArgs e)
    {

    }
    protected void ddlPeriodicidad_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    protected void btnConsultarafiliado_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, DatosSolicitudServicio.CodigoProgramaRotativo);

    }
    public Boolean ValidarDatos()
    {
        Int64 Plazomax = Convert.ToInt64(txtPlazoMaximo.Text);
        Int64 plazo = Convert.ToInt64(txtPlazo.Text);
        montomosolitado = Convert.ToDecimal(txtMonto.Text.ToString());
        montomaximo = Convert.ToDecimal(txtMontomax.Text);

        if (txtMonto.Text == "" || txtMonto.Text == "0")
        {
            VerError("Ingrese el Monto Solicitadoo");
            return false;
        }
        if (txtPlazo.Text == "" || txtPlazo.Text == "0")
        {
            VerError("Ingrese el Plazo Solicitadoo");
            return false;
        }
        if (txfechasolicitud.Texto == "")
        {
            VerError("Ingrese una fecha de Solicitud");
            return false;
        }
   
        if (montomosolitado>montomaximo)
        {
            VerError("El monto solicitado supera el máximo de la linea");
            return false;
        }
        if (plazo > Plazomax)
        {
            VerError("El plazo solicitado supera el máximo de la linea");
            return false;
        }
        if (this.Ddlusuarios.SelectedIndex == 0)
        {
            VerError("Seleccione un Asesor Comercial");
            return false;
        }

        return true;
    }

    protected void btnEditar_Click(object sender, EventArgs e)
    {
        MvAfiliados.ActiveViewIndex = -1;

    }
    protected void txtNombreCLiente_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnPlanPagosClick(object sender, EventArgs e)
    {
        if (Session["numero_radicacion"] != null)
        {
            Xpinn.FabricaCreditos.Services.CreditoPlanService creditoPlanServicio = new Xpinn.FabricaCreditos.Services.CreditoPlanService();
            Session[creditoPlanServicio.CodigoPrograma + ".id"] = Session["numero_radicacion"];
            Navegar("~/Page/FabricaCreditos/PlanPagos/Detalle.aspx");
        }
    }
    protected void btnAprobacionClick(object sender, EventArgs e)
    {
        if (Session["numero_radicacion"] != null)
        {
            Xpinn.FabricaCreditos.Services.CreditoSolicitadoService creditoServicio = new Xpinn.FabricaCreditos.Services.CreditoSolicitadoService();
            Session[creditoServicio.CodigoPrograma + ".id"] = Session["numero_radicacion"];
            Navegar("~/Page/FabricaCreditos/Rotativo/Aprobacion/Lista.aspx");
        }
    }
}