using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Business;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{

    #region Variables Globales
    private Xpinn.FabricaCreditos.Services.LineasCreditoService LineasCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
    //Listas:
    //List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables
    //String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    private Xpinn.FabricaCreditos.Services.DestinacionService destinServicio = new Xpinn.FabricaCreditos.Services.DestinacionService();
    #endregion

    #region  Clases
    public class meses
    {
        public meses(int pmes, string pnombre) { mes = pmes; nombre = pnombre; }
        public int mes { get; set; }
        public string nombre { get; set; }
    }

    #endregion

    #region Metodos Iniciales
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[LineasCreditoServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(LineasCreditoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(LineasCreditoServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {
                if (ddlTipoLinea.SelectedValue == "2")
                {
                    txtplazodiferir.Visible = false;
                    Lblplazodiferir.Visible = false;
                    chkAvancesAprob.Visible = true;
                    ChkDeseAhorros.Visible = true;
                    chkDiferir.Visible = true;
                    txtFechaCorte.Visible = true;
                    lblfechacorte.Visible = true;
                }
                else
                {
                    txtplazodiferir.Visible = false;
                    Lblplazodiferir.Visible = false;
                    chkAvancesAprob.Visible = false;
                    ChkDeseAhorros.Visible = false;
                    chkDiferir.Visible = false;
                    txtFechaCorte.Visible = false;
                    lblfechacorte.Visible = false;
                }
                Session["DatosDetalle"] = null;
                Session["LSTTIPODOCUMENTOS"] = null;
                iniciarGarantiaDocumentoLinea();
                CargarListas();
                btnnuevoatributos.Visible = false;
                btnnuevodeduccion.Visible = false;


                ddlTipoRefinanciacion_SelectedIndexChanged(ddlTipoRefinanciacion, null);
                if (Session[LineasCreditoServicio.CodigoPrograma + ".id"] != null)
                {
                    txtCod_linea_credito.Enabled = false;
                    idObjeto = Session[LineasCreditoServicio.CodigoPrograma + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                    btnnuevoatributos.Visible = true;
                    btnnuevodeduccion.Visible = true;

                    if (ddlTipoLinea.SelectedValue == "2")
                    {
                        txtplazodiferir.Visible = false;
                        Lblplazodiferir.Visible = false;
                        chkAvancesAprob.Visible = true;
                        ChkDeseAhorros.Visible = true;
                        chkDiferir.Visible = true;
                        txtFechaCorte.Visible = true;
                        lblfechacorte.Visible = true;
                    }
                    else
                    {
                        txtplazodiferir.Visible = false;
                        Lblplazodiferir.Visible = false;
                        chkAvancesAprob.Visible = false;
                        ChkDeseAhorros.Visible = false;
                        chkDiferir.Visible = false;
                        txtFechaCorte.Visible = false;
                        lblfechacorte.Visible = false;
                    }
                }
                else
                {
                    CargarDocumentosYProcesos();
                    ValidarManejaPeriodo();
                }
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    #endregion

    #region Metodos Externos
    private void CargarListas()
    {
        try
        {
            Xpinn.FabricaCreditos.Services.TipoCupoService tipoCupoServicio = new Xpinn.FabricaCreditos.Services.TipoCupoService();
            Xpinn.FabricaCreditos.Entities.TipoCupo eTipoCupo = new TipoCupo();
            txtTipo_cupo.DataSource = tipoCupoServicio.ListarTipoCupo(eTipoCupo, (Usuario)Session["Usuario"]);
            txtTipo_cupo.DataTextField = "descripcion";
            txtTipo_cupo.DataValueField = "tipo_cupo";
            txtTipo_cupo.DataBind();

            ddlTipoLiquidacion.DataSource = TraerResultadosLista("TipoLiquidacion");
            ddlTipoLiquidacion.DataTextField = "ListaDescripcion";
            ddlTipoLiquidacion.DataValueField = "ListaId";
            ddlTipoLiquidacion.DataBind();
            ddlTipoLiquidacion.Enabled = true;

            ddlPeriodicidadGracia.DataSource = TraerResultadosLista("PeriodicidadGracia"); ;
            ddlPeriodicidadGracia.DataTextField = "ListaDescripcion";
            ddlPeriodicidadGracia.DataValueField = "ListaIdStr";
            ddlPeriodicidadGracia.DataBind();

            ddlCod_clasifica.DataSource = TraerResultadosLista("Cod_clasifica");
            ddlCod_clasifica.DataTextField = "ListaDescripcion";
            ddlCod_clasifica.DataValueField = "ListaId";
            ddlCod_clasifica.DataBind();

            ddlCod_moneda.DataSource = TraerResultadosLista("Cod_moneda");
            ddlCod_moneda.DataTextField = "ListaDescripcion";
            ddlCod_moneda.DataValueField = "ListaId";
            ddlCod_moneda.DataBind();

            //TRAER DATOS DE LA DESTINACION
            List<Xpinn.FabricaCreditos.Entities.Destinacion> lstConsultaD = new List<Xpinn.FabricaCreditos.Entities.Destinacion>();
            lstConsultaD = destinServicio.ListarDestinacion(ObtenerValores(), (Usuario)Session["usuario"]);
            gvRecoger.PageSize = pageSize;
            gvRecoger.EmptyDataText = emptyQuery;
            gvRecoger.DataSource = lstConsultaD;
            if (lstConsultaD.Count > 0)
            {
                gvRecoger.Visible = true;
                gvRecoger.DataBind();
            }
            else
            {
                gvRecoger.Visible = false;
            }

            //TRAER MESES
            List<meses> lstmeses = new List<meses>();
            lstmeses.Add(new meses(1, "Enero"));
            lstmeses.Add(new meses(2, "Febrero"));
            lstmeses.Add(new meses(3, "Marzo"));
            lstmeses.Add(new meses(4, "Abril"));
            lstmeses.Add(new meses(5, "Mayo"));
            lstmeses.Add(new meses(6, "Junio"));
            lstmeses.Add(new meses(7, "Julio"));
            lstmeses.Add(new meses(8, "Agosto"));
            lstmeses.Add(new meses(9, "Septiembre"));
            lstmeses.Add(new meses(10, "Octubre"));
            lstmeses.Add(new meses(11, "Noviembre"));
            lstmeses.Add(new meses(12, "Diciembre"));
            gvMeses.DataSource = lstmeses;
            gvMeses.DataBind();

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }
    private void CargarListaTipoLiqRotativo()
    {
        try
        {

            ddlTipoLiquidacion.DataSource = TraerResultadosLista("TipoLiquidacionRot");
            ddlTipoLiquidacion.DataTextField = "ListaDescripcion";
            ddlTipoLiquidacion.DataValueField = "ListaId";
            ddlTipoLiquidacion.DataBind();
            ddlTipoLiquidacion.SelectedIndex = 1;
            ddlTipoLiquidacion.Enabled = false;


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }
    private List<Xpinn.FabricaCreditos.Entities.Persona1> TraerResultadosLista(string pListaSolicitada)
    {
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = LineasCreditoServicio.ListasDesplegables(pListaSolicitada, (Usuario)Session["Usuario"]);
        return lstDatosSolicitud;
    }
    private void CargarDocumentosYProcesos()
    {
        List<LineasCredito> lstDocumentos = new List<LineasCredito>();
        LineasCredito vDoc = new LineasCredito();

        lstDocumentos = LineasCreditoServicio.ListarDocumentos(vDoc, "LISTAR", (Usuario)Session["usuario"]);

        if (lstDocumentos.Count > 0)
        {
            gvRequeridoDoc.DataSource = lstDocumentos;
            gvRequeridoDoc.DataBind();
        }

        List<ProcesoLineaCredito> lstProcesos = new List<ProcesoLineaCredito>();
        ProcesoLineaCredito vProc = new ProcesoLineaCredito();

        lstProcesos = LineasCreditoServicio.ListarProcesos(vProc, "LISTAR", (Usuario)Session["usuario"]);
        if (lstProcesos.Count > 0)
        {
            gvProcesos.DataSource = lstProcesos;
            gvProcesos.DataBind();
        }
    }
    private void iniciarGarantiaDocumentoLinea()
    {
        List<LineasCredito> lstDetalle = new List<LineasCredito>();
        for (int i = 0; i < 5; i++)
        {
            LineasCredito vDeta = new LineasCredito();
            vDeta.tipo_documento = null;
            vDeta.requerido = null;
            vDeta.plantilla = "";
            lstDetalle.Add(vDeta);
        }

        gvGarantiaDoc.DataSource = lstDetalle;
        gvGarantiaDoc.DataBind();
        Session["DatosDetalle"] = lstDetalle;
    }
    #endregion

    #region Validaciones
    protected Boolean ValidarDatos()
    {
        if (gvGarantiaDoc.Rows.Count > 0)
        {
            List<LineasCredito> LstDeta;
            LstDeta = obtenerListaGarantiaDocumentos();

            foreach (LineasCredito rLinea in LstDeta)
            {
                int tipo = Convert.ToInt32(rLinea.tipo_documento);
                int cont = 0;
                foreach (LineasCredito rVal in LstDeta)
                {
                    if (tipo == rVal.tipo_documento)
                    {
                        cont++;
                    }
                    if (cont > 1)
                    {
                        VerError("Error en Documentos- No puede Ingresar el mismo tipo de Documento mas de una vez.");
                        return false;
                    }
                }
            }
            if (ddlTipoRefinanciacion.SelectedItem.Text == "% Saldo")
            {
                if (txtPorcent_Mini.Text != "")
                    if (Convert.ToDecimal(txtPorcent_Mini.Text) < 0 || Convert.ToDecimal(txtPorcent_Mini.Text) > 100)
                    {
                        VerError("Error en el ingreso del porcentaje mínimo");
                        return false;
                    }
                if (txtPorcent_Maxi.Text != "")
                    if (Convert.ToDecimal(txtPorcent_Maxi.Text) <= 0 || Convert.ToDecimal(txtPorcent_Maxi.Text) > 100)
                    {
                        VerError("Error en el ingreso del porcentaje máximo");
                        return false;
                    }
            }


            // if (chkDiferir.Checked == true && txtplazodiferir.Text == "0" ||  txtplazodiferir.Text == "")
            //{
            //  VerError("Digitar un plazo a diferir");
            //return false;

            // if (chkDiferir.Visible == true)
            //{
            //  if (chkDiferir.Checked == true && txtplazodiferir.Text == "0" || txtplazodiferir.Text == "")
            //{
            //  VerError("Digitar un plazo a diferir");
            //return false;

            //  if (chkDiferir.Checked == true && txtplazodiferir.Text == "0" || txtplazodiferir.Text == "")
            //{
            //  VerError("Digitar un plazo a diferir");
            //return false;

            //}

            //  }
            //}
        }
        return true;
    }
    private void ValidarManejaPeriodo()
    {
        try
        {
            if (ddlTipoGracia.SelectedValue == null)
                ddlTipoGracia.SelectedValue = "0";

            lblMeses.Visible = false;
            upPeriodicidad.Visible = false;

            if (ddlTipoGracia.SelectedValue == "1")
            {
                txtPeriodo_gracia.Enabled = true;
                ddlPeriodicidadGracia.Enabled = true;
            }
            else if (ddlTipoGracia.SelectedValue == "3")
            {
                lblMeses.Visible = true;
                upPeriodicidad.Visible = true;
            }
            else
            {
                txtPeriodo_gracia.Enabled = false;
                ddlPeriodicidadGracia.Enabled = false;
                ddlPeriodicidadGracia.SelectedValue = "0";
                txtPeriodo_gracia.Text = "";
            }
        }
        catch
        {
            VerError("Error al seleccionar período de gracia");
        }
    }
    #endregion

    #region Metodos Botonoes y eventos
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (ValidarDatos())
            ctlMensaje.MostrarMensaje("Desea grabar los datos de la línea de crédito?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        //try
        //{

        Xpinn.FabricaCreditos.Entities.LineasCredito vLineasCredito = new Xpinn.FabricaCreditos.Entities.LineasCredito();

        if (idObjeto != "")
            vLineasCredito = LineasCreditoServicio.ConsultarLineasCredito(Convert.ToString(idObjeto),
                (Usuario)Session["usuario"]);

        vLineasCredito.cod_linea_credito = Convert.ToString(txtCod_linea_credito.Text.Trim());
        vLineasCredito.nombre = Convert.ToString(txtNombre.Text.Trim());
        vLineasCredito.tipo_linea = Convert.ToInt64(ddlTipoLinea.SelectedValue);
        vLineasCredito.tipo_liquidacion = Convert.ToInt64(ddlTipoLiquidacion.Text.Trim());
        vLineasCredito.tipo_cupo = txtTipo_cupo.Text == "" ? 0 : Convert.ToInt64(txtTipo_cupo.Text.Trim());
        vLineasCredito.recoge_saldos = chkRecogerSaldos.Checked == true ? 1 : 0;
        vLineasCredito.cobra_mora = chkCobraMora.Checked == true ? 1 : 0;

        vLineasCredito.tipo_refinancia = Convert.ToInt64(ddlTipoRefinanciacion.SelectedValue);
        if (ddlTipoRefinanciacion.SelectedItem.Text == "% Saldo" ||
            ddlTipoRefinanciacion.SelectedItem.Text == "% Cuotas Pagas")
        {
            vLineasCredito.minimo_refinancia = txtPorcent_Mini.Text == ""
                ? 0
                : Convert.ToDecimal(txtPorcent_Mini.Text.Trim());
            vLineasCredito.maximo_refinancia = txtPorcent_Maxi.Text == ""
                ? 0
                : Convert.ToDecimal(txtPorcent_Maxi.Text.Trim());
        }
        else
        {
            vLineasCredito.minimo_refinancia = txtMinimo_refinancia.Text == ""
                ? 0
                : Convert.ToDecimal(txtMinimo_refinancia.Text.Trim().Replace(".", ""));
            vLineasCredito.maximo_refinancia = txtMaximo_refinancia.Text == ""
                ? 0
                : Convert.ToDecimal(txtMaximo_refinancia.Text.Trim().Replace(".", ""));
        }
        vLineasCredito.maneja_pergracia = ddlTipoGracia.SelectedValue == null ? "0" : ddlTipoGracia.SelectedValue;
        vLineasCredito.periodo_gracia = txtPeriodo_gracia.Text == ""
            ? 0
            : Convert.ToInt64(txtPeriodo_gracia.Text.Trim());
        vLineasCredito.tipo_periodic_gracia = Convert.ToString(ddlPeriodicidadGracia.SelectedValue);
        vLineasCredito.modifica_datos = chkModificaDatos.Checked == true ? "1" : "0";
        vLineasCredito.modifica_fecha_pago = chkModifica_fecha_pago.Checked == true ? "1" : "0";
        vLineasCredito.garantia_requerida = Convert.ToString(ddlGarantia_requerida.SelectedValue);
        vLineasCredito.tipo_capitalizacion = Convert.ToInt64(ddlTipo_capitalizacion.SelectedValue);
        vLineasCredito.cuotas_extras = chkCuotas_extras.Checked == true ? 1 : 0;
        vLineasCredito.cod_clasifica = Convert.ToInt64(ddlCod_clasifica.SelectedValue);
        vLineasCredito.numero_codeudores = txtNumero_codeudores.Text == ""
            ? 0
            : Convert.ToInt64(txtNumero_codeudores.Text.Trim());
        vLineasCredito.cod_moneda = Convert.ToInt64(ddlCod_moneda.Text.Trim());
        vLineasCredito.porc_corto = txtPorc_corto.Text == "" ? 0 : Convert.ToInt64(txtPorc_corto.Text.Trim());
        vLineasCredito.tipo_amortiza = Convert.ToInt64(ddlTipo_amortiza.SelectedValue);
        vLineasCredito.estado = chkEstado.Checked == true ? 1 : 0;
        vLineasCredito.maneja_excepcion = chkManejaExcepcion.Checked == true ? "1" : "0";
        vLineasCredito.aporte_garantia = chkAporteGarantia.Checked ? 1 : 0;
        vLineasCredito.meses_gracia = txtMeses.Text;

        if (txtCuotaIntAjuste.Text != "")
            vLineasCredito.cuota_intajuste = Convert.ToInt32(txtCuotaIntAjuste.Text);
        else
            vLineasCredito.cuota_intajuste = 0;
        vLineasCredito.orden_servicio = cbOrdenSErvicio.Checked == true ? 1 : 0;
        vLineasCredito.credito_x_linea = txtCredXlinea.Text != "" ? Convert.ToInt32(txtCredXlinea.Text) : 0;

        //credito rotativo           
        if (txtFechaCorte.TieneDatos)
            vLineasCredito.fecha_corte = txtFechaCorte.ToDateTime;

        vLineasCredito.diferir = chkDiferir.Checked == true ? 1 : 0;
        if (chkDiferir.Checked == true && chkDiferir.Visible == true)
        {
            vLineasCredito.plazo_diferir = txtplazodiferir.Text == "" ? 0 : Convert.ToInt64(txtplazodiferir.Text.Trim());
        }
        else
        {
            vLineasCredito.plazo_diferir = 0;
        }

        vLineasCredito.cantidad_comision = txtcantidadcomision.Text != ""
            ? Convert.ToInt64(txtcantidadcomision.Text)
            : 0;
        vLineasCredito.valor_comision = TxtValorComision.Text != "" ? Convert.ToInt64(TxtValorComision.Text) : 0;
        vLineasCredito.signo_comision = ddlSigno.SelectedValue != "" ? Convert.ToInt64(ddlSigno.SelectedValue) : 0;
        vLineasCredito.avances_aprob = chkAvancesAprob.Checked == true ? 1 : 0;
        vLineasCredito.desem_ahorros = ChkDeseAhorros.Checked == true ? 1 : 0;
        //Tipo Persona 
        vLineasCredito.aplica_tercero = chktercero.Checked == true ? 1 : 0;
        vLineasCredito.aplica_asociado = chkasociado.Checked == true ? 1 : 0;
        vLineasCredito.aplica_empleado = chkempleado.Checked == true ? 1 : 0;
        vLineasCredito.credito_gerencial = chkCredGenerencial.Checked == true ? 1 : 0;
        vLineasCredito.credito_educativo = chkEducativo.Checked ? 1 : 0;
        if (chkEducativo.Checked)
            vLineasCredito.maneja_auxilio = chkManejaAuxilio.Checked ? 1 : 0;
        else
            vLineasCredito.maneja_auxilio = 0;
        vLineasCredito.prioridad = txtPrioridad.Text == ""
            ? 0
            : Convert.ToInt32(ConvertirStringToInt(txtPrioridad.Text));

        //RECUPERAR LOS DATOS DE DOCUMENTOS
        vLineasCredito.lstDocumentos = new List<LineasCredito>();
        vLineasCredito.lstDocumentos = obtenerListaDocumentos();

        //RECUPERAR LOS DATOS GARANTIA DE DOCUMENTOS
        vLineasCredito.lstGarantiaDoc = new List<LineasCredito>();
        vLineasCredito.lstGarantiaDoc = obtenerListaGarantiaDocumentos();

        //RECUPERAR LOS DATOS DE PROCESOS
        vLineasCredito.lstProcesoLinea = new List<ProcesoLineaCredito>();
        vLineasCredito.lstProcesoLinea = ObtenerListaProcesos();

        //RECUPERAR LOS DATOS DE PRIORIDAD POR ATRIBUTOS DE LA LINEA
        vLineasCredito.lstPrioridad = new List<LineasCredito>();
        vLineasCredito.lstPrioridad = ObtenerListaPrioridades();

        //Cargar datos destinacion por linea de credito
        vLineasCredito.lstdestinacion = new List<LineasCredito>();
        vLineasCredito.lstdestinacion = ObtenerListaDestinación();

        //Guarda los Datos de parametros de la linea
        vLineasCredito.LstParametrosLinea = new List<ProcesoLineaCredito>();
        vLineasCredito.LstParametrosLinea = ObtenerParametrosLinea();


        if

        (idObjeto != "")
        {
            vLineasCredito.cod_linea_credito = Convert.ToString(idObjeto);
            LineasCreditoServicio.ModificarLineasCredito(vLineasCredito, (Usuario)Session["usuario"]);
        }
        else
        {
            if (ddlTipoLinea.SelectedValue == "2")
            {
                CargarListaTipoLiqRotativo();
            }
            vLineasCredito = LineasCreditoServicio.CrearLineasCredito(vLineasCredito, (Usuario)Session["usuario"]);
            idObjeto = vLineasCredito.cod_linea_credito.ToString();
        }

        Session[LineasCreditoServicio.CodigoPrograma + ".id"] = idObjeto;
        mvLineaCredito.ActiveViewIndex = 1;

        //}
        //catch (ExceptionBusiness ex)
        //{
        //    VerError(ex.Message);
        //}
        //catch (Exception ex)
        //{
        //    BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma, "btnGuardar_Click", ex);
        //}
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }
    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        Session[LineasCreditoServicio.CodigoPrograma + ".id"] = txtCod_linea_credito.Text;
        idObjeto = txtCod_linea_credito.Text;
        btnnuevoatributos.Visible = true;
        btnnuevodeduccion.Visible = true;
        Navegar(Pagina.Lista);
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            //  Session[LineasCreditoServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
    }
    protected void ddlTipoGracia_SelectedIndexChanged(object sender, EventArgs e)
    {
        ValidarManejaPeriodo();
    }
    protected void ddlTipoLinea_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTipoLinea.SelectedValue == "2")
        {
            //txtplazodiferir.Visible = true;
            Lblplazodiferir.Visible = false;
            txtcantidadcomision.Visible = true;
            chkAvancesAprob.Visible = true;
            ChkDeseAhorros.Visible = true;
            CargarListaTipoLiqRotativo();
            chkDiferir.Visible = true;
            txtFechaCorte.Visible = true;
            lblfechacorte.Visible = true;
            Lblcantidadcomision.Visible = true;
            TxtValorComision.Visible = true;
            LblValorComision.Visible = true;
            txtFechaCorte.Visible = true;
            ddlSigno.Visible = true;
        }

        else
        {
            Lblcantidadcomision.Visible = false;
            TxtValorComision.Visible = false;
            LblValorComision.Visible = false;
            ddlSigno.Visible = false;
            txtcantidadcomision.Visible = false;
            ddlTipoLiquidacion.Enabled = true;
            txtplazodiferir.Visible = false;
            Lblplazodiferir.Visible = false;
            chkAvancesAprob.Visible = false;

            ChkDeseAhorros.Visible = false;
            chkDiferir.Visible = false;
            txtFechaCorte.Visible = false;
            lblfechacorte.Visible = false;

            ChkDeseAhorros.Visible = false;

            ChkDeseAhorros.Visible = false;

            if (idObjeto == "")
                CargarListas();
        }
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        obtenerListaGarantiaDocumentos();

        List<LineasCredito> lstDetalle = new List<LineasCredito>();

        if (Session["DatosDetalle"] != null)
        {
            lstDetalle = (List<LineasCredito>)Session["DatosDetalle"];

            for (int i = 1; i <= 1; i++)
            {
                LineasCredito vDeta = new LineasCredito();
                vDeta.tipo_documento = null;
                vDeta.requerido = null;
                vDeta.plantilla = "";
                lstDetalle.Add(vDeta);
            }
            gvGarantiaDoc.PageIndex = gvGarantiaDoc.PageCount;
            gvGarantiaDoc.DataSource = lstDetalle;
            gvGarantiaDoc.DataBind();

            Session["DatosDetalle"] = lstDetalle;
        }
    }
    protected void btnnuevoatributos_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (txtCod_linea_credito.Text != "")
        {
            Session["Operacion"] = "N";
            Session[LineasCreditoServicio.CodigoPrograma + ".LineaCredito"] = txtCod_linea_credito.Text;
            Session[LineasCreditoServicio.CodigoPrograma + ".CodRango"] = "";
            Session[LineasCreditoServicio.CodigoPrograma + ".NombreGrupo"] = "";
            Session[LineasCreditoServicio.CodigoPrograma + ".id"] = txtCod_linea_credito.Text;
            Response.Redirect("~/Page/FabricaCreditos/LineasCreditos/Atributos.aspx");
        }
        else
        { VerError("Ingrese el Codigo de la Linea"); }
    }
    protected void btnnuevodeduccion_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (txtCod_linea_credito.Text != "")
        {
            Session["Operacion"] = "N";
            Session[LineasCreditoServicio.CodigoPrograma + ".LineaCredito"] = txtCod_linea_credito.Text;
            Session[LineasCreditoServicio.CodigoPrograma + ".CodAtr"] = null;
            Response.Redirect("~/Page/FabricaCreditos/LineasCreditos/Deducciones.aspx");
        }
    }
    protected void ddlTipoRefinanciacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTipoRefinanciacion.SelectedItem.Text == "% Saldo" || ddlTipoRefinanciacion.SelectedItem.Text == "% Cuotas Pagas")
        {
            panelPorcentaje.Visible = true;
            panelValores.Visible = false;
        }
        else
        {
            panelPorcentaje.Visible = false;
            panelValores.Visible = true;
        }
    }
    protected void chkEducativo_CheckedChanged(object sender, EventArgs e)
    {
        chkManejaAuxilio.Visible = false;
        if (chkEducativo.Checked)
        {
            chkManejaAuxilio.Visible = true;
            chkManejaAuxilio.Checked = true;
        }
    }
    protected void chkDiferir_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDiferir.Checked == true)
        {
            txtplazodiferir.Enabled = true;
        }
    }
    protected void cbOrdenSErvicio_CheckedChanged(object sender, EventArgs e)
    {
        bool result = cbOrdenSErvicio.Checked;

        obtenerListaGarantiaDocumentos();
        if (Session["DTDocumentos"] != null)
        {
            gvGarantiaDoc.DataSource = Session["DTDocumentos"];
            gvGarantiaDoc.DataBind();
        }
    }
    protected void cbListado_CheckedChanged(object sender, EventArgs e)
    {
        txtMeses.Text = "";
        foreach (GridViewRow item in gvMeses.Rows)
        {
            CheckBox cbListado = (CheckBox)item.FindControl("cbListado");
            if (cbListado != null)
            {
                if (cbListado.Checked)
                {
                    Label lbl_mes = (Label)item.FindControl("lbl_mes");
                    if (lbl_mes != null)
                    {
                        txtMeses.Text += lbl_mes.Text + ";";
                    }
                }
            }
        }
    }
    protected void gvAtributos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvAtributos.PageIndex = e.NewPageIndex;

            gvAtributos.DataSource = LineasCreditoServicio.ConsultarLineas_Creditoatributo(txtCod_linea_credito.Text, (Usuario)Session["Usuario"]);
            gvAtributos.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }
    protected void btnParamContable_Click(object sender, EventArgs e)
    {
        VerError("");
        if (txtCod_linea_credito.Text == null || txtCod_linea_credito.Text == "")
        {
            VerError("La línea no se encuentra registrada para realizar la parametrización contable");
            Session["cod_linea_credito"] = null;
        }
        else
        {
            Session["cod_linea_credito"] = txtCod_linea_credito.Text;
            Navegar("../../Contabilidad/ParametrosCtasLineas/Detalle.aspx");
        }
    }
    #endregion

    #region Obtencion de Datos

    protected void ObtenerDatos(String pIdObjeto)
    {

        try
        {
            Xpinn.FabricaCreditos.Entities.LineasCredito vLineasCredito = new Xpinn.FabricaCreditos.Entities.LineasCredito();
            vLineasCredito = LineasCreditoServicio.ConsultarLineasCredito(Convert.ToString(pIdObjeto), (Usuario)Session["usuario"]);
            //credito rotativo
            chkDiferir.Checked = vLineasCredito.diferir == 1 ? true : false;
            if (vLineasCredito.diferir == 1)
            {
                txtplazodiferir.Enabled = true;
            }
            if (vLineasCredito.plazo_diferir != Int64.MinValue)
                txtplazodiferir.Text = HttpUtility.HtmlDecode(vLineasCredito.plazo_diferir.ToString().Trim());

            if (vLineasCredito.cantidad_comision != Int64.MinValue)
                txtcantidadcomision.Text = HttpUtility.HtmlDecode(vLineasCredito.cantidad_comision.ToString().Trim());
            else
                txtcantidadcomision.Text = "0";

            if (vLineasCredito.valor_comision != Int64.MinValue)
                TxtValorComision.Text = HttpUtility.HtmlDecode(vLineasCredito.valor_comision.ToString().Trim());
            else
                TxtValorComision.Text = "0";

            if (vLineasCredito.signo_comision != Int64.MinValue)
                ddlSigno.SelectedValue = HttpUtility.HtmlDecode(vLineasCredito.signo_comision.ToString().Trim());


            Int64 linea = Convert.ToInt64(vLineasCredito.tipo_linea);
            if (linea == 2)
            {
                Lblcantidadcomision.Visible = true;
                TxtValorComision.Visible = true;
                LblValorComision.Visible = true;
                txtFechaCorte.Visible = true;
                ddlSigno.Visible = true;
                txtcantidadcomision.Visible = true;
                if (!string.IsNullOrEmpty(vLineasCredito.fecha_corte.ToString()))
                    txtFechaCorte.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vLineasCredito.fecha_corte.ToString()));
                CargarListaTipoLiqRotativo();
            }
            else
            {
                CargarListas();
                txtFechaCorte.Visible = false;
            }

            if (!string.IsNullOrEmpty(vLineasCredito.cod_linea_credito))
                txtCod_linea_credito.Text = HttpUtility.HtmlDecode(vLineasCredito.cod_linea_credito.ToString().Trim());
            if (!string.IsNullOrEmpty(vLineasCredito.nombre))
                txtNombre.Text = HttpUtility.HtmlDecode(vLineasCredito.nombre.ToString().Trim());
            if (vLineasCredito.tipo_linea != Int64.MinValue)
                ddlTipoLinea.SelectedValue = HttpUtility.HtmlDecode(vLineasCredito.tipo_linea.ToString().Trim());
            if (vLineasCredito.tipo_liquidacion != Int64.MinValue)
                ddlTipoLiquidacion.SelectedValue = HttpUtility.HtmlDecode(vLineasCredito.tipo_liquidacion.ToString().Trim());
            if (vLineasCredito.tipo_cupo != Int64.MinValue)
                txtTipo_cupo.Text = HttpUtility.HtmlDecode(vLineasCredito.tipo_cupo.ToString().Trim());
            chkRecogerSaldos.Checked = vLineasCredito.recoge_saldos == 1 ? true : false;
            chkCobraMora.Checked = vLineasCredito.cobra_mora == 1 ? true : false;
            if (vLineasCredito.tipo_refinancia != Int64.MinValue)
                ddlTipoRefinanciacion.SelectedValue = HttpUtility.HtmlDecode(vLineasCredito.tipo_refinancia.ToString().Trim());
            ddlTipoRefinanciacion_SelectedIndexChanged(ddlTipoRefinanciacion, null);

            if (ddlTipoRefinanciacion.SelectedValue == "2" || ddlTipoRefinanciacion.SelectedValue == "3") // si es % Saldo
            {
                if (vLineasCredito.minimo_refinancia != Decimal.MinValue)
                    txtPorcent_Mini.Text = HttpUtility.HtmlDecode(vLineasCredito.minimo_refinancia.ToString().Trim());
                if (vLineasCredito.maximo_refinancia != Decimal.MinValue)
                    txtPorcent_Maxi.Text = HttpUtility.HtmlDecode(vLineasCredito.maximo_refinancia.ToString().Trim());
            }
            else
            {
                if (vLineasCredito.minimo_refinancia != Decimal.MinValue)
                    txtMinimo_refinancia.Text = HttpUtility.HtmlDecode(vLineasCredito.minimo_refinancia.ToString().Trim());
                if (vLineasCredito.maximo_refinancia != Decimal.MinValue)
                    txtMaximo_refinancia.Text = HttpUtility.HtmlDecode(vLineasCredito.maximo_refinancia.ToString().Trim());
            }
            ddlTipoGracia.SelectedValue = vLineasCredito.maneja_pergracia;
            if (vLineasCredito.periodo_gracia != Int64.MinValue)
                txtPeriodo_gracia.Text = HttpUtility.HtmlDecode(vLineasCredito.periodo_gracia.ToString().Trim());
            if (!string.IsNullOrEmpty(vLineasCredito.tipo_periodic_gracia))
                ddlPeriodicidadGracia.SelectedValue = HttpUtility.HtmlDecode(vLineasCredito.tipo_periodic_gracia.ToString());
            chkModificaDatos.Checked = vLineasCredito.modifica_datos == "1" ? true : false;
            chkModifica_fecha_pago.Checked = vLineasCredito.modifica_fecha_pago == "1" ? true : false;
            if (!string.IsNullOrEmpty(vLineasCredito.garantia_requerida))
                ddlGarantia_requerida.SelectedValue = HttpUtility.HtmlDecode(vLineasCredito.garantia_requerida.ToString().Trim());
            if (vLineasCredito.tipo_capitalizacion != Int64.MinValue)
                ddlTipo_capitalizacion.SelectedValue = HttpUtility.HtmlDecode(vLineasCredito.tipo_capitalizacion.ToString().Trim());
            chkCuotas_extras.Checked = vLineasCredito.cuotas_extras == 1 ? true : false;
            if (vLineasCredito.cod_clasifica != Int64.MinValue)
                ddlCod_clasifica.SelectedValue = HttpUtility.HtmlDecode(vLineasCredito.cod_clasifica.ToString().Trim());
            if (vLineasCredito.numero_codeudores != Int64.MinValue)
                txtNumero_codeudores.Text = HttpUtility.HtmlDecode(vLineasCredito.numero_codeudores.ToString().Trim());
            if (vLineasCredito.cod_moneda != Int64.MinValue)
                ddlCod_moneda.SelectedValue = HttpUtility.HtmlDecode(vLineasCredito.cod_moneda.ToString().Trim());
            if (vLineasCredito.porc_corto != Int64.MinValue)
                txtPorc_corto.Text = HttpUtility.HtmlDecode(vLineasCredito.porc_corto.ToString().Trim());
            if (vLineasCredito.tipo_amortiza != Int64.MinValue)
                ddlTipo_amortiza.SelectedValue = HttpUtility.HtmlDecode(vLineasCredito.tipo_amortiza.ToString().Trim());

            chkAporteGarantia.Checked = vLineasCredito.aporte_garantia == 1 ? true : false;
            chkEstado.Checked = vLineasCredito.estado == 1 ? true : false;
            chkManejaExcepcion.Checked = vLineasCredito.maneja_excepcion == "1" ? true : false;
            txtCuotaIntAjuste.Text = vLineasCredito.cuota_intajuste != 0 ? vLineasCredito.cuota_intajuste.ToString() : "";
            cbOrdenSErvicio.Checked = vLineasCredito.orden_servicio == 1 ? true : false;
            chkAvancesAprob.Checked = vLineasCredito.avances_aprob == 1 ? true : false;
            ChkDeseAhorros.Checked = vLineasCredito.desem_ahorros == 1 ? true : false;

            //Tipo persona aplica
            chktercero.Checked = vLineasCredito.aplica_tercero == 1 ? true : false;
            chkasociado.Checked = vLineasCredito.aplica_asociado == 1 ? true : false;
            chkempleado.Checked = vLineasCredito.aplica_empleado == 1 ? true : false;
            chkCredGenerencial.Checked = vLineasCredito.credito_gerencial == 1 ? true : false;
            chkEducativo.Checked = vLineasCredito.credito_educativo == 1 ? true : false;
            chkEducativo_CheckedChanged(chkEducativo, null);
            if (vLineasCredito.credito_x_linea != 0)
                txtCredXlinea.Text = vLineasCredito.credito_x_linea.ToString();
            if (vLineasCredito.prioridad != Int32.MinValue)
                txtPrioridad.Text = HttpUtility.HtmlDecode(vLineasCredito.prioridad.ToString().Trim());
            chkManejaAuxilio.Checked = chkManejaAuxilio.Visible == true && vLineasCredito.maneja_auxilio == 1 ? true : false;

            //CARGAR MESES DE GRACIA
            if (ddlTipoGracia.SelectedValue == "3")
            {
                if (vLineasCredito.meses_gracia != null)
                {
                    if (vLineasCredito.meses_gracia != null)
                    {
                        string[] smeses = vLineasCredito.meses_gracia.Split(';');
                        txtMeses.Text = vLineasCredito.meses_gracia;
                        for (int i = 0; i < smeses.Count(); i += 1)
                        {
                            if (smeses[i].Trim() != "")
                            { 
                                try
                                { 
                                    int mes = ConvertirStringToInt32(smeses[i]);
                                    CheckBox cbListado = (CheckBox)gvMeses.Rows[mes-1].FindControl("cbListado");
                                    if (cbListado != null)
                                    {
                                        cbListado.Checked = true;
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                }
            }

            ValidarManejaPeriodo();

            gvAtributos.DataSource = LineasCreditoServicio.ConsultarLineas_Creditoatributo(txtCod_linea_credito.Text, (Usuario)Session["Usuario"]);
            gvAtributos.DataBind();

            List<LineasCredito> lstDeduccion = new List<LineasCredito>();
            lstDeduccion = LineasCreditoServicio.ConsultarLineasCreditodeducciones(txtCod_linea_credito.Text, (Usuario)Session["Usuario"]);
            if (lstDeduccion.Count > 0)
            {
                gvdeducciones.DataSource = lstDeduccion;
                gvdeducciones.DataBind();
            }

            //RECUPERAR DATOS DE DOCUMENTOS REQUERIDOS
            List<LineasCredito> lstData = new List<LineasCredito>();
            LineasCredito vdata = new LineasCredito();
            vdata.cod_linea_credito = pIdObjeto;
            lstData = LineasCreditoServicio.ListarDocumentos(vdata, "RECUPERA_DATOS", (Usuario)Session["usuario"]);
            if (lstData.Count > 0)
            {
                gvRequeridoDoc.DataSource = lstData;
                gvRequeridoDoc.DataBind();
            }

            //RECUPERAR DATOS DE DOCUMENTOS GARANTIA
            lstData = LineasCreditoServicio.ConsultarGarantiaDocumento(pIdObjeto, (Usuario)Session["usuario"]);
            if (lstData.Count > 0)
            {
                try
                {
                    gvGarantiaDoc.DataSource = lstData;
                    gvGarantiaDoc.DataBind();
                }
                catch { }
            }

            //RECUPERAR DATOS PROCESOS DE LINEA
            List<ProcesoLineaCredito> lstProceso = new List<ProcesoLineaCredito>();
            ProcesoLineaCredito vProc = new ProcesoLineaCredito();
            vProc.cod_lineacredito = pIdObjeto;
            lstProceso = LineasCreditoServicio.ListarProcesos(vProc, "RECUPERA_DATOS", (Usuario)Session["usuario"]);
            if (lstProceso.Count > 0)
            {
                gvProcesos.DataSource = lstProceso;
                gvProcesos.DataBind();
            }

            //RECUPERAR DATOS DE PRIORIDADES
            List<LineasCredito> lstPrioridad = new List<LineasCredito>();
            lstPrioridad = LineasCreditoServicio.ConsultarPrioridad_Linea(txtCod_linea_credito.Text, (Usuario)Session["Usuario"]);
            if (lstPrioridad.Count > 0) //CONSULTAR EN LA TABLA PRIORIDAD_LIN 
            {
                gvPrioridad.DataSource = lstPrioridad;
                gvPrioridad.DataBind();
            }

            //RECUPERAR DATOS DE DESTINACIÓN
            List<LineasCredito> lstDestinos = new List<LineasCredito>();
            lstDestinos = LineasCreditoServicio.ConsultarDestinacion_Linea(txtCod_linea_credito.Text, (Usuario)Session["usuario"]);
            if (lstDestinos.Count > 0)
            {
                foreach (var item in lstDestinos)
                {
                    foreach (GridViewRow rFila in gvRecoger.Rows)
                    {
                        CheckBoxGrid chkSeleccione = rFila.FindControl("cbListado") as CheckBoxGrid;
                        Label lblcodDest = (Label)rFila.FindControl("lbl_destino");
                        if (item.cod_destino == Convert.ToInt32(lblcodDest.Text))
                        {
                            chkSeleccione.Checked = true;
                        }
                    }

                }
            }

            // CARGAR LAS PAGADURIAS PARA LOS DOCUMENTOS
            try
            {
                foreach (GridViewRow rFila in gvGarantiaDoc.Rows)
                {
                    Xpinn.Tesoreria.Services.EmpresaRecaudoServices serviciosempresarecaudo = new Xpinn.Tesoreria.Services.EmpresaRecaudoServices();
                    List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstConsulta = new List<Xpinn.Tesoreria.Entities.EmpresaRecaudo>();
                    Xpinn.Tesoreria.Entities.EmpresaRecaudo pData = new Xpinn.Tesoreria.Entities.EmpresaRecaudo();
                    lstConsulta = serviciosempresarecaudo.ListarEmpresaRecaudo(pData, (Usuario)Session["usuario"]);
                    DropDownListGrid ddlpagaduria = (DropDownListGrid)rFila.FindControl("ddlpagaduria");
                    if (lstConsulta.Count > 0)
                    {
                        ddlpagaduria.DataSource = lstConsulta;
                        ddlpagaduria.DataTextField = "NOM_EMPRESA";
                        ddlpagaduria.DataValueField = "COD_EMPRESA";
                        ddlpagaduria.AppendDataBoundItems = true;
                        ddlpagaduria.Items.Insert(0, new ListItem("Seleccione un item", "0"));
                        ddlpagaduria.DataBind();
                    }
                }
            }
            catch { }

            //CARGA LOS PARAMETROS DE LA LINEA
            List<ProcesoLineaCredito> procesoLineaCreditos = new List<ProcesoLineaCredito>();
            procesoLineaCreditos = LineasCreditoServicio.ListarParametrosLinea(Convert.ToString(pIdObjeto),
                (Usuario)Session["usuario"]);
            if (procesoLineaCreditos != null)
            {
                gvParamtros.DataSource = procesoLineaCreditos;
                gvParamtros.DataBind();
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
    protected List<LineasCredito> ObtenerListaDeducciones()
    {
        gvdeducciones.DataSource = LineasCreditoServicio.ConsultarLineasCreditodeducciones(txtCod_linea_credito.Text, (Usuario)Session["Usuario"]);
        List<Xpinn.FabricaCreditos.Entities.LineasCredito> lista = new List<Xpinn.FabricaCreditos.Entities.LineasCredito>();
        Xpinn.FabricaCreditos.Entities.LineasCredito lista1 = new Xpinn.FabricaCreditos.Entities.LineasCredito();
        gvdeducciones.DataBind();
        int i = 0;
        lista = LineasCreditoServicio.ConsultarLineasCreditodeducciones(txtCod_linea_credito.Text, (Usuario)Session["Usuario"]);

        foreach (GridViewRow row in gvdeducciones.Rows)
        {

            lista1 = lista[i];

            System.Web.UI.WebControls.CheckBox chkSeleccionar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("cbx"));

            System.Web.UI.WebControls.CheckBox chkGenerar = ((System.Web.UI.WebControls.CheckBox)row.FindControl("chkGenerar"));
            if (lista1.cobra_mora == 0)
                chkGenerar.Checked = false;
            else
                chkGenerar.Checked = true;

            System.Web.UI.WebControls.CheckBox ChkModifica = ((System.Web.UI.WebControls.CheckBox)row.FindControl("ChkModifica"));
            if (lista1.modifica == 0)
                ChkModifica.Checked = false;
            else
                ChkModifica.Checked = true;

            i = i + 1;
        }

        Session["Deducciones"] = lista;

        return lista;

        //return lista1;
    }
    public List<LineasCredito> ObtenerListaPrioridades()
    {
        List<LineasCredito> lstPrioridad = new List<LineasCredito>();
        List<LineasCredito> lista = new List<LineasCredito>();

        foreach (GridViewRow rFila in gvPrioridad.Rows)
        {
            LineasCredito pPriori = new LineasCredito();

            if (rFila.Cells[0].Text != null)
                pPriori.cod_atr = Convert.ToInt64(rFila.Cells[0].Text);

            TextBox txtPrioridad = (TextBox)rFila.FindControl("txtPrioridad");
            if (txtPrioridad.Text != "")
                pPriori.numero = Convert.ToInt32(txtPrioridad.Text);
            else
                pPriori.numero = 0;

            if (pPriori.numero != 0 && pPriori.numero != null)
                lstPrioridad.Add(pPriori);
        }
        return lstPrioridad;
    }
    public List<LineasCredito> obtenerListaDocumentos()
    {
        List<LineasCredito> lstDocumentos = new List<LineasCredito>();
        List<LineasCredito> lista = new List<LineasCredito>();
        foreach (GridViewRow rFila in gvRequeridoDoc.Rows)
        {
            LineasCredito pDocu = new LineasCredito();

            if (rFila.Cells[0].Text != "")
                pDocu.tipo_documento = Convert.ToInt32(rFila.Cells[0].Text); //TIPO DE DOCUMENTO
            if (rFila.Cells[1].Text != "")
                pDocu.descripcion = rFila.Cells[1].Text;
            CheckBox chkAplica = (CheckBox)rFila.FindControl("chkAplicaCod");
            if (chkAplica != null && chkAplica.Checked)
                pDocu.aplica_codeudor = "1";
            else
                pDocu.aplica_codeudor = "0";
            CheckBox chkSeleccion = (CheckBox)rFila.FindControl("chkAplica");
            if (chkSeleccion != null)
            {
                pDocu.checkbox = chkSeleccion.Checked == false ? 0 : 1;
            }
            lista.Add(pDocu);
            if (pDocu.tipo_documento != 0 && pDocu.checkbox == 1)
            {
                lstDocumentos.Add(pDocu);
            }
        }
        Session["DTDocumentos"] = lista;

        return lstDocumentos;
    }
    public List<LineasCredito> obtenerListaGarantiaDocumentos()
    {
        List<LineasCredito> lstGaran = new List<LineasCredito>();
        List<LineasCredito> lista = new List<LineasCredito>();

        foreach (GridViewRow rFila in gvGarantiaDoc.Rows)
        {
            LineasCredito pDocu = new LineasCredito();

            pDocu.consecutivo = rFila.RowIndex;

            DropDownListGrid ddlpagaduria = (DropDownListGrid)rFila.FindControl("ddlpagaduria");
            if (ddlpagaduria.SelectedIndex != 0 && ddlpagaduria.SelectedValue != null && ddlpagaduria.SelectedValue != "")
                pDocu.cod_empresa = Convert.ToInt64(ddlpagaduria.SelectedValue);

            DropDownListGrid ddlTipoDoc = (DropDownListGrid)rFila.FindControl("ddlTipoDoc");
            if (ddlTipoDoc.SelectedIndex != 0 && ddlTipoDoc.SelectedValue != null && ddlTipoDoc.SelectedValue != "")
                pDocu.tipo_documento = Convert.ToInt64(ddlTipoDoc.SelectedValue);

            CheckBox chkRequerido = (CheckBox)rFila.FindControl("chkRequerido");
            if (chkRequerido != null && chkRequerido.Checked)
                pDocu.requerido = 1;
            else
                pDocu.requerido = 0;

            TextBox txtPlantilla = (TextBox)rFila.FindControl("txtPlantilla");
            if (txtPlantilla.Text != "")
                pDocu.plantilla = txtPlantilla.Text;

            /*FileUploadGrid fupCarga = (FileUploadGrid)rFila.FindControl("fupCarga");
            if (fupCarga.HasFile)
            {
                String fileName = System.IO.Path.GetFileName(fupCarga.PostedFile.FileName);
                String RutaOrigen = System.IO.Path.GetDirectoryName(fupCarga.PostedFile.FileName);
                String extension = System.IO.Path.GetExtension(fupCarga.PostedFile.FileName).ToLower();
                string RutaDefault = "C:\inetpub\Publica";                 
                if(ConfigurationManager.AppSettings["ruta_doc_garantia_linea"] != null)
                    RutaDefault = ConfigurationManager.AppSettings["ruta_doc_garantia_linea"].ToString();
            }*/

            lista.Add(pDocu);
            Session["DatosDetalle"] = lista;
            Session["DTDocumentos"] = lista;
            if (pDocu.tipo_documento != 0 && pDocu.tipo_documento != null && pDocu.plantilla != "" && pDocu.plantilla != null)
            {
                lstGaran.Add(pDocu);
                return lstGaran;
            }
        }
        return lista;
    }
    public List<ProcesoLineaCredito> ObtenerListaProcesos()
    {
        List<ProcesoLineaCredito> lstProceso = new List<ProcesoLineaCredito>();

        foreach (GridViewRow rFila in gvProcesos.Rows)
        {
            ProcesoLineaCredito pProc = new ProcesoLineaCredito();

            if (rFila.Cells[0].Text != "")
                pProc.cod_procesolinea = Convert.ToInt32(rFila.Cells[0].Text);

            CheckBox chkSeleccion = (CheckBox)rFila.FindControl("chkSeleccion");
            if (chkSeleccion != null && chkSeleccion.Checked)
                pProc.checkbox = 1;
            else
                pProc.checkbox = 0;

            if (pProc.cod_procesolinea != 0 && pProc.checkbox == 1)
            {
                lstProceso.Add(pProc);
            }
        }
        return lstProceso;
    }
    public List<ProcesoLineaCredito> ObtenerParametrosLinea()
    {
        List<ProcesoLineaCredito> lstProceso = new List<ProcesoLineaCredito>();

        foreach (GridViewRow rFila in gvParamtros.Rows)
        {
            ProcesoLineaCredito pProc = new ProcesoLineaCredito();

            pProc.codtipoproceso = Convert.ToInt32(rFila.Cells[0].Text);
            pProc.cod_lineacredito = Convert.ToString(txtCod_linea_credito.Text);
            TextBox TxtValor = (TextBox)rFila.FindControl("txtValor");
            if (TxtValor != null)
            {
                pProc.Valor = Convert.ToString(TxtValor.Text);
            }

            lstProceso.Add(pProc);
        }
        return lstProceso;
    }
    protected List<LineasCredito> ObtenerListaDestinación()
    {
        try
        {
            List<LineasCredito> lstdestinacion = new List<LineasCredito>();

            foreach (GridViewRow rFila in gvRecoger.Rows)
            {
                LineasCredito destinacion_linea = new LineasCredito();

                CheckBoxGrid chkSeleccione = rFila.FindControl("cbListado") as CheckBoxGrid;

                if (chkSeleccione != null)
                    if (chkSeleccione.Checked)
                    {
                        Label lblcodDest = (Label)rFila.FindControl("lbl_destino");
                        if (lblcodDest.Text != "")
                            destinacion_linea.cod_destino = Convert.ToInt32(lblcodDest.Text);
                    }

                if (chkSeleccione.Checked)
                {
                    lstdestinacion.Add(destinacion_linea);
                }
            }
            return lstdestinacion;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("", "ObtenerListaDestinación", ex);
            return null;
        }
    }
    private Xpinn.FabricaCreditos.Entities.Destinacion ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.Destinacion vDestinacion = new Xpinn.FabricaCreditos.Entities.Destinacion();
        return vDestinacion;
    }
    #endregion

    #region Eventos Tablas

    protected void gvAtributos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string id = gvAtributos.Rows[e.NewEditIndex].Cells[2].Text;
        Session["Operacion"] = "E";
        Session[LineasCreditoServicio.CodigoPrograma + ".CodRango"] = id;
        Session[LineasCreditoServicio.CodigoPrograma + ".NombreGrupo"] = gvAtributos.Rows[e.NewEditIndex].Cells[3].Text;
        Session[LineasCreditoServicio.CodigoPrograma + ".LineaCredito"] = txtCod_linea_credito.Text;
        Session[LineasCreditoServicio.CodigoPrograma + ".id"] = txtCod_linea_credito.Text;
        Response.Redirect("~/Page/FabricaCreditos/LineasCreditos/Atributos.aspx");
    }
    protected void gvAtributos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int conseID = Convert.ToInt32(gvAtributos.DataKeys[e.RowIndex].Values[0].ToString());
            LineasCredito vLinea = new LineasCredito();

            vLinea.cod_linea_credito = txtCod_linea_credito.Text;
            vLinea.cod_rango_atr = Convert.ToInt64(conseID);

            LineasCreditoServicio.EliminarTodoElAtributo(vLinea, (Usuario)Session["usuario"]);

            gvAtributos.DataSource = LineasCreditoServicio.ConsultarLineas_Creditoatributo(txtCod_linea_credito.Text, (Usuario)Session["Usuario"]);
            gvAtributos.DataBind();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }
    protected void gvdeducciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvdeducciones.DataKeys[e.RowIndex].Values[0].ToString());


        ObtenerListaDeducciones();
        List<LineasCredito> LstDeducciones;
        LineasCredito erango = new LineasCredito();
        LstDeducciones = (List<LineasCredito>)Session["Deducciones"];

        if (conseID > 0)
        {
            try
            {
                foreach (LineasCredito deducciones in LstDeducciones)
                {
                    if (Convert.ToInt64(deducciones.cod_atr) == conseID)
                    {
                        deducciones.cod_atr = Convert.ToInt64(conseID);
                        deducciones.cod_linea_credito = Convert.ToString(this.txtCod_linea_credito.Text);
                        LineasCreditoServicio.EliminarDeducciones(deducciones, (Usuario)Session["usuario"]);
                        LstDeducciones.Remove(deducciones);

                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
        }

        gvdeducciones.DataSourceID = null;
        gvdeducciones.DataBind();

        gvdeducciones.DataSource = LstDeducciones;
        gvdeducciones.DataBind();

        Session["Deducciones"] = LstDeducciones;
    }
    protected void gvGarantiaDoc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlTipoDoc = (DropDownListGrid)e.Row.FindControl("ddlTipoDoc");
            if (ddlTipoDoc != null)
            {
                List<LineasCredito> lstConsulta = new List<LineasCredito>();
                LineasCredito pData = new LineasCredito();

                if (Session["LSTTIPODOCUMENTOS"] != null)
                {
                    lstConsulta = (List<LineasCredito>)Session["LSTTIPODOCUMENTOS"];
                }
                else
                {
                    if (idObjeto != "")
                        pData = LineasCreditoServicio.ConsultarLineasCredito(Convert.ToString(idObjeto), (Usuario)Session["usuario"]);
                    lstConsulta = LineasCreditoServicio.ListarComboTipoDocumentos(pData, (Usuario)Session["usuario"]);
                    Session["LSTTIPODOCUMENTOS"] = lstConsulta;
                }

                List<LineasCredito> lstFiltrado = new List<LineasCredito>();

                foreach (LineasCredito pinfo in lstConsulta)
                {
                    LineasCredito datosdocumento = new LineasCredito();

                    if (cbOrdenSErvicio.Checked == true)
                    {
                        lstFiltrado.Add(pinfo);
                    }
                    else
                    {
                        if (pinfo.orden_servicio == 0)
                        {
                            lstFiltrado.Add(pinfo);
                        }
                    }
                }
                if (lstConsulta.Count > 0)
                {
                    ddlTipoDoc.DataSource = lstFiltrado;
                    ddlTipoDoc.DataTextField = "descripcion";
                    ddlTipoDoc.DataValueField = "tipo_documento";
                    ddlTipoDoc.AppendDataBoundItems = true;
                    ddlTipoDoc.Items.Insert(0, new ListItem("Seleccione un item", "0"));
                    ddlTipoDoc.SelectedIndex = 0;
                    ddlTipoDoc.DataBind();
                }

            }

            Label lblTipoDoc = (Label)e.Row.FindControl("lblTipoDoc");
            if (lblTipoDoc != null)
                ddlTipoDoc.SelectedValue = lblTipoDoc.Text;

            DropDownListGrid ddlpagaduria = (DropDownListGrid)e.Row.FindControl("ddlpagaduria");
            if (ddlTipoDoc != null)
            {
                Xpinn.Tesoreria.Services.EmpresaRecaudoServices serviciosempresarecaudo = new Xpinn.Tesoreria.Services.EmpresaRecaudoServices();
                List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstConsulta = new List<Xpinn.Tesoreria.Entities.EmpresaRecaudo>();
                Xpinn.Tesoreria.Entities.EmpresaRecaudo pData = new Xpinn.Tesoreria.Entities.EmpresaRecaudo();
                lstConsulta = serviciosempresarecaudo.ListarEmpresaRecaudo(pData, (Usuario)Session["usuario"]);
                if (lstConsulta.Count > 0)
                {
                    ddlpagaduria.DataSource = lstConsulta;
                    ddlpagaduria.DataTextField = "NOM_EMPRESA";
                    ddlpagaduria.DataValueField = "COD_EMPRESA";
                    ddlpagaduria.AppendDataBoundItems = true;
                    ddlpagaduria.Items.Insert(0, new ListItem("Seleccione un item", ""));
                    ddlpagaduria.SelectedIndex = 0;
                    ddlpagaduria.DataBind();
                }
            }

            try
            {
                Label lbltipoPagaduria = (Label)e.Row.FindControl("lblTipoDoc");
                if (lbltipoPagaduria != null)
                    ddlpagaduria.SelectedValue = lbltipoPagaduria.Text;
            }
            catch { }
        }
    }
    protected void gvGarantiaDoc_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Delete")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            obtenerListaGarantiaDocumentos();

            List<LineasCredito> LstDeta;
            LstDeta = (List<LineasCredito>)Session["DatosDetalle"];
            foreach (GridViewRow rfila in gvGarantiaDoc.Rows)
            {
                foreach (LineasCredito Deta in LstDeta)
                {
                    DropDownListGrid tipodoc = (DropDownListGrid)rfila.FindControl("ddlTipoDoc");
                    if (Deta.consecutivo == id)
                    {
                        LstDeta.Remove(Deta);
                        LineasCreditoServicio.Eliminardocumentosdegarantia(Convert.ToString(tipodoc.SelectedValue), txtCod_linea_credito.Text, (Usuario)Session["usuario"]);
                        break;
                    }
                }
            }
            gvGarantiaDoc.DataSource = null;
            gvGarantiaDoc.DataBind();
            //RECUPERAR DATOS DE DOCUMENTOS GARANTIA
            LstDeta = LineasCreditoServicio.ConsultarGarantiaDocumento(txtCod_linea_credito.Text, (Usuario)Session["usuario"]);
            if (LstDeta.Count > 0)
            {

                gvGarantiaDoc.DataSource = LstDeta;
                gvGarantiaDoc.DataBind();
                Session["DatosDetalle"] = LstDeta;
                ObtenerDatos(txtCod_linea_credito.Text);
                Response.Redirect("Nuevo.aspx");
            }
            else
            {
                gvGarantiaDoc.DataSource = null;
                gvGarantiaDoc.DataBind();
                ObtenerDatos(txtCod_linea_credito.Text);
                Response.Redirect("Nuevo.aspx");
            }


        }
    }
    protected void gvProcesos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbldescripcion = (Label)e.Row.FindControl("lbldescripcion");
            CheckBox chkSeleccion = (CheckBox)e.Row.FindControl("chkSeleccion");

            if (lbldescripcion.Text != "")
                if (lbldescripcion.Text == "Solicitado")
                {
                    chkSeleccion.Checked = true;
                    chkSeleccion.Enabled = false;
                }
        }
    }
    protected void gvdeducciones_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string id = gvdeducciones.Rows[e.NewEditIndex].Cells[2].Text;
        Session[LineasCreditoServicio.CodigoPrograma + ".LineaCredito"] = txtCod_linea_credito.Text;
        Session[LineasCreditoServicio.CodigoPrograma + ".CodAtr"] = id;
        Session[LineasCreditoServicio.CodigoPrograma + ".id"] = txtCod_linea_credito.Text;
        Response.Redirect("~/Page/FabricaCreditos/LineasCreditos/Deducciones.aspx");
    }

    #endregion

}
