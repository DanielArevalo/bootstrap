using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.IO;
using System.Drawing.Imaging;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Services;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
//using Subgurim.Controles;
using System.Text.RegularExpressions;
using Xpinn.Asesores.Entities;
using Xpinn.Caja.Entities;
using Xpinn.Caja.Services;
using Xpinn.ActivosFijos.Services;
using Xpinn.ActivosFijos.Entities;
using Xpinn.Comun.Entities;
using System.Configuration;
using System.Globalization;


public partial class NuevoPersona : GlobalWeb
{
    private Xpinn.Contabilidad.Services.TerceroService DatosClienteServicio = new Xpinn.Contabilidad.Services.TerceroService();
    private Xpinn.Aportes.Services.AfiliacionServices _afiliacionServicio = new Xpinn.Aportes.Services.AfiliacionServices();
    private ActividadesServices ActiServices = new ActividadesServices();
    private BeneficiarioService BeneficiarioServicio = new BeneficiarioService();
    private Xpinn.FabricaCreditos.Entities.Georeferencia pGeo = new Xpinn.FabricaCreditos.Entities.Georeferencia();
    private Xpinn.FabricaCreditos.Services.GeoreferenciaService Georeferencia = new Xpinn.FabricaCreditos.Services.GeoreferenciaService();
    private FormatoDocumentoServices DocumentoService = new FormatoDocumentoServices();
    private ImagenesService ImagenSERVICE = new ImagenesService();
    Persona1Service ServicePersona = new Persona1Service();
    Usuario pUsuario = new Usuario();
    UsuarioAtribuciones atrusuarios = new UsuarioAtribuciones();
    UsuarioAtribucionesService atribuciones = new UsuarioAtribucionesService();
    EstadosFinancierosService EstadosFinancierosServicio = new EstadosFinancierosService();
    String Operacion;
    string codigo;

    private void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[_afiliacionServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(_afiliacionServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(_afiliacionServicio.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            //lblempresas.Visible = false;
            //txtsueldo_soli.eventoCambiar += txtsueldoSoli_TextChanged;            
        }
        catch //(Exception ex)
        {
            //BOexcepcion.Throw(AfiliacionServicio.CodigoPrograma + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //ctlFormatos.Inicializar("1");
            cargarListas();

            if (Session[_afiliacionServicio.CodigoPrograma + ".id"] != null)
            {
                string id = Session[_afiliacionServicio.CodigoPrograma + ".id"].ToString();
                Session[Usuario.codusuario + "cod_per"] = id;
                cargar_cabecera(id);
            }
            else
            {
                Session[Usuario.codusuario + "cod_per"] = null;
            }

            if (Session[Usuario.codusuario + "cod_per"] != null)
            {
                codigo = Session[Usuario.codusuario + "cod_per"].ToString();
                cargar_datos();
            }
        }
    }


    protected void cargar_cabecera(string cod_persona)
    {
        if (cod_persona != null && cod_persona != "")
        {
            Int64 cod_per = Convert.ToInt64(cod_persona);
            Persona1 Entidad = new Persona1();
            Entidad.cod_persona = cod_per;
            Entidad.seleccionar = "Cod_persona";
            Entidad.soloPersona = 1;
            Entidad = ServicePersona.ConsultarPersona1Param(Entidad, (Usuario)Session["usuario"]);

            //carga la informacion
            lblcodpersona.Text += Entidad.cod_persona;
            lblidentificacion.Text += Entidad.identificacion;
            lblnombre.Text += Entidad.nombreCompleto;
            lblcodpersona.Visible = true;
            lblidentificacion.Visible = true;
            lblnombre.Visible = true;
        }
        else
        {
            lblcodpersona.Visible = false;
            lblidentificacion.Visible = false;
            lblnombre.Visible = false;
        }
    }


    #region eventos tab
    protected void btnTab1_Click(object sender, EventArgs e)
    {
            Response.Redirect("Persona.aspx");
    }    

    protected void btnTab2_Click(object sender, EventArgs e)
    {
            Response.Redirect("Laboral.aspx");
    }
    protected void btnTab3_Click(object sender, EventArgs e)
    {
            Response.Redirect("Beneficiarios.aspx");
    }
    protected void btnTab4_Click(object sender, EventArgs e)
    {
        if (almacenarDatos())
            Response.Redirect("Economica.aspx");
    }
    protected void btnTab5_Click(object sender, EventArgs e)
    {
        if (almacenarDatos())
            Response.Redirect("Adicional.aspx");
    }
    #endregion

    #region metodos de carga de datos
    public void cargar_datos()
    {
        Usuario pUsuario = (Usuario)Session["usuario"];

        Persona1 Entidad = new Persona1();
        if (Session[Usuario.codusuario + "cod_per"] != null)
        {

            string id = (string)Session[Usuario.codusuario + "cod_per"];

            //if (Session["Persona"] != null)
            //{
            //    Entidad = (Persona1)Session["Persona"];
            //}
            //else
            //{
            Entidad.cod_persona = Convert.ToInt64(id);
            Entidad.seleccionar = "Cod_persona";
            Entidad.soloPersona = 1;
            Entidad = ServicePersona.ConsultarPersona1Param(Entidad, (Usuario)Session["usuario"]);
            Session["Persona"] = Entidad;
            //}
            if (Entidad != null)
            {
                EstadosFinancieros InformacionFinanciera = new EstadosFinancieros();

                InformacionFinanciera = EstadosFinancierosServicio.listarperosnainfofin(Convert.ToInt64(id), (Usuario)Session["usuario"]);
                if (InformacionFinanciera != null)
                {
                    if (InformacionFinanciera.cod_persona != Int64.MinValue)
                        cod_per.Text = HttpUtility.HtmlDecode(InformacionFinanciera.cod_persona.ToString().Trim());
                    if (InformacionFinanciera.cod_personaconyuge != Int64.MinValue)
                        cod_cony.Text = HttpUtility.HtmlDecode(InformacionFinanciera.cod_personaconyuge.ToString().Trim());
                    if (InformacionFinanciera.sueldo != Int64.MinValue)
                        txtsueldo_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.sueldo.ToString().Trim());
                    if (InformacionFinanciera.sueldoconyuge != Int64.MinValue)
                        txtsueldo_cony.Text = HttpUtility.HtmlDecode(InformacionFinanciera.sueldoconyuge.ToString().Trim());
                    if (InformacionFinanciera.honorarios != Int64.MinValue)
                        txthonorario_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.honorarios.ToString().Trim());
                    if (InformacionFinanciera.honorariosconyuge != Int64.MinValue)
                        txthonorario_cony.Text = HttpUtility.HtmlDecode(InformacionFinanciera.honorariosconyuge.ToString().Trim());
                    if (InformacionFinanciera.arrendamientos != Int64.MinValue)
                        txtarrenda_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.arrendamientos.ToString().Trim());
                    if (InformacionFinanciera.arrendamientosconyuge != Int64.MinValue)
                        txtarrenda_cony.Text = HttpUtility.HtmlDecode(InformacionFinanciera.arrendamientosconyuge.ToString().Trim());
                    if (InformacionFinanciera.otrosingresos != Int64.MinValue)
                        txtotrosIng_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.otrosingresos.ToString().Trim());
                    if (InformacionFinanciera.otrosingresosconyuge != Int64.MinValue)
                        txtotrosIng_cony.Text = HttpUtility.HtmlDecode(InformacionFinanciera.otrosingresosconyuge.ToString().Trim());

                    txtConceptoOtros_soli.Text = InformacionFinanciera.conceptootros;
                    txtConceptoOtros_cony.Text = InformacionFinanciera.conceptootrosconyuge;

                    // TOTALIZANDO VALORES DE INGRESOS
                    decimal pTotalSoli = InformacionFinanciera.sueldo + InformacionFinanciera.honorarios + InformacionFinanciera.arrendamientos
                                        + InformacionFinanciera.otrosingresos;
                    decimal pTotalCony = InformacionFinanciera.sueldoconyuge + InformacionFinanciera.honorariosconyuge +
                                        InformacionFinanciera.arrendamientosconyuge + InformacionFinanciera.otrosingresosconyuge;

                    if (InformacionFinanciera.totalingreso != Int64.MinValue)
                    {
                        txttotalING_soli.Text = pTotalSoli == InformacionFinanciera.totalingreso ? HttpUtility.HtmlDecode(InformacionFinanciera.totalingreso.ToString().Trim()) : pTotalSoli.ToString();
                        hdtotalING_soli.Value = pTotalSoli == InformacionFinanciera.totalingreso ? InformacionFinanciera.totalingreso.ToString() : pTotalSoli.ToString();
                    }
                    if (InformacionFinanciera.totalingresoconyuge != Int64.MinValue)
                    {
                        txttotalING_cony.Text = pTotalCony == InformacionFinanciera.totalingreso ? HttpUtility.HtmlDecode(InformacionFinanciera.totalingresoconyuge.ToString().Trim()) : pTotalCony.ToString();
                        hdtotalING_cony.Value = pTotalCony == InformacionFinanciera.totalingreso ? InformacionFinanciera.totalingresoconyuge.ToString() : pTotalCony.ToString();
                    }

                    if (InformacionFinanciera.hipoteca != Int64.MinValue)
                        txthipoteca_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.hipoteca.ToString().Trim());
                    if (InformacionFinanciera.hipotecaconyuge != Int64.MinValue)
                        txthipoteca_cony.Text = HttpUtility.HtmlDecode(InformacionFinanciera.hipotecaconyuge.ToString().Trim());
                    if (InformacionFinanciera.targeta_credito != Int64.MinValue)
                        txttarjeta_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.targeta_credito.ToString().Trim());
                    if (InformacionFinanciera.targeta_creditoconyuge != Int64.MinValue)
                        txttarjeta_cony.Text = HttpUtility.HtmlDecode(InformacionFinanciera.targeta_creditoconyuge.ToString().Trim());
                    if (InformacionFinanciera.otrosprestamos != Int64.MinValue)
                        txtotrosPres_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.otrosprestamos.ToString().Trim());
                    if (InformacionFinanciera.otrosprestamosconyuge != Int64.MinValue)
                        txtotrosPres_cony.Text = HttpUtility.HtmlDecode(InformacionFinanciera.otrosprestamosconyuge.ToString().Trim());
                    if (InformacionFinanciera.gastofamiliar != Int64.MinValue)
                        txtgastosFam_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.gastofamiliar.ToString().Trim());
                    if (InformacionFinanciera.gastofamiliarconyuge != Int64.MinValue)
                        txtgastosFam_cony.Text = HttpUtility.HtmlDecode(InformacionFinanciera.gastofamiliarconyuge.ToString().Trim());
                    if (InformacionFinanciera.decunomina != Int64.MinValue)
                        txtnomina_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.decunomina.ToString().Trim());
                    if (InformacionFinanciera.decunominaconyuge != Int64.MinValue)
                        txtnomina_cony.Text = HttpUtility.HtmlDecode(InformacionFinanciera.decunominaconyuge.ToString().Trim());

                    // TOTALIZANDO VALORES DE EGRESOS
                    pTotalSoli = InformacionFinanciera.hipoteca + InformacionFinanciera.targeta_credito + InformacionFinanciera.otrosprestamos
                                        + InformacionFinanciera.gastofamiliar + InformacionFinanciera.decunomina;
                    pTotalCony = InformacionFinanciera.hipotecaconyuge + InformacionFinanciera.targeta_creditoconyuge + InformacionFinanciera.otrosprestamosconyuge
                                        + InformacionFinanciera.gastofamiliarconyuge + InformacionFinanciera.decunominaconyuge;

                    if (InformacionFinanciera.totalegresos != Int64.MinValue)
                    {
                        txttotalEGR_soli.Text = pTotalSoli == InformacionFinanciera.totalegresos ? HttpUtility.HtmlDecode(InformacionFinanciera.totalegresos.ToString().Trim()) : pTotalSoli.ToString();
                        hdtotalEGR_soli.Value = pTotalSoli == InformacionFinanciera.totalegresos ? InformacionFinanciera.totalegresos.ToString() : pTotalSoli.ToString();
                    }
                    if (InformacionFinanciera.totalegresosconyuge != Int64.MinValue)
                    {
                        txttotalEGR_cony.Text = pTotalCony == InformacionFinanciera.totalegresosconyuge ? HttpUtility.HtmlDecode(InformacionFinanciera.totalegresosconyuge.ToString().Trim()) : pTotalCony.ToString();
                        hdtotalEGR_cony.Value = pTotalCony == InformacionFinanciera.totalegresosconyuge ? InformacionFinanciera.totalegresosconyuge.ToString() : pTotalCony.ToString();
                    }

                    //Agregado para listar información de activos, pasivos y patrimonio
                    if (InformacionFinanciera.TotAct != Int64.MinValue)
                        txtactivos_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.TotAct.ToString().Trim());
                    if (InformacionFinanciera.TotActConyuge != Int64.MinValue)
                        txtactivos_conyuge.Text = HttpUtility.HtmlDecode(InformacionFinanciera.TotActConyuge.ToString().Trim());
                    if (InformacionFinanciera.TotPas != Int64.MinValue)
                        txtpasivos_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.TotPas.ToString().Trim());
                    if (InformacionFinanciera.TotPasConyuge != Int64.MinValue)
                        txtpasivos_conyuge.Text = HttpUtility.HtmlDecode(InformacionFinanciera.TotPasConyuge.ToString().Trim());
                    if (InformacionFinanciera.TotPat != Int64.MinValue)
                        txtpatrimonio_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.TotPat.ToString().Trim());
                    if (InformacionFinanciera.TotPatConyuge != Int64.MinValue)
                        txtpatrimonio_conyuge.Text = HttpUtility.HtmlDecode(InformacionFinanciera.TotPatConyuge.ToString().Trim());
                }

                //Lista de Cuentas Bancarias
                try
                {
                    List<CuentasBancarias> LstCuentasBanc = new List<CuentasBancarias>();

                    LstCuentasBanc = ActiServices.ConsultarCuentasBancarias(Convert.ToInt64(id), "", (Usuario)Session["usuario"]);
                    if (LstCuentasBanc.Count > 0)
                    {
                        if ((LstCuentasBanc != null) || (LstCuentasBanc.Count != 0))
                        {
                            gvCuentasBancarias.DataSource = LstCuentasBanc;
                            gvCuentasBancarias.DataBind();
                        }
                        Session[pUsuario.codusuario + "DatosCuentaBanc"] = LstCuentasBanc;

                    }
                }
                catch (Exception ex)
                {
                    lblerror.Text = ("Error al consultar la lista de Cuentas, error:" + ex.Message);
                }

                try
                {
                    List<EstadosFinancieros> LstMonedaExt = new List<EstadosFinancieros>();
                    List<EstadosFinancieros> LstTransaccionesExt = new List<EstadosFinancieros>();
                    List<EstadosFinancieros> LstProductosExt = new List<EstadosFinancieros>();
                    LstMonedaExt = EstadosFinancierosServicio.ListarCuentasMonedaExtranjera(Convert.ToInt64(id), (Usuario)Session["usuario"]);
                    LstTransaccionesExt = LstMonedaExt.Where(x => x.tipo_producto == "" || x.tipo_producto == null).ToList();
                    LstProductosExt = LstMonedaExt.Where(x => x.tipo_producto != "" && x.tipo_producto != null).ToList();

                    if (LstTransaccionesExt.Count > 0)
                    {
                        if ((LstTransaccionesExt != null) || (LstTransaccionesExt.Count != 0))
                        {
                            chkMonedaExtranjera.Checked = true;
                            panelMonedaExtranjera.Visible = true;
                            gvMonedaExtranjera.DataSource = LstTransaccionesExt;
                            gvMonedaExtranjera.DataBind();
                        }
                        else
                        {
                            chkMonedaExtranjera.Checked = false;
                            panelMonedaExtranjera.Visible = false;
                        }
                        Session["DatosCuentaExtranjera"] = LstTransaccionesExt;
                    }

                    if (LstProductosExt.Count > 0)
                    {
                        if ((LstProductosExt != null) || (LstProductosExt.Count != 0))
                        {
                            chkTransaccionExterior.Checked = true;
                            pProductosExt.Visible = true;
                            gvProductosExterior.DataSource = LstProductosExt;
                            gvProductosExterior.DataBind();
                        }
                        else
                        {
                            chkTransaccionExterior.Checked = false;
                            pProductosExt.Visible = false;
                        }
                        Session["DatosProductosFinExt"] = LstProductosExt;
                    }
                }
                catch (Exception ex)
                {
                    lblerror.Text = ("Error al consultar la lista de Transacciones y/o productos en el exterior, error:" + ex.Message);
                }

                LlenarGVActivoFijos(id);


                //Validación de tab laboral 

                if (Entidad.ocupacionApo > 0)
                {
                    string script = @"  function Alertando(valor) {
                                            valor = " + Entidad.ocupacionApo.ToString() + @"
                                            if (valor == 1 || valor == 2 || valor == 3) 
                                            { window.parent.$('#lilaboral').removeClass('clsInactivos'); }
                                            else
                                            {

                                                window.parent.$('#lilaboral').removeClass('clsInactivos').addClass('clsInactivos');
                                            }                                                                                    
                                    }";

                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Ocupacion", script, true);
                }
            }
        }
    }
    public void cargarListas()
    {
        LlenarDDLTipoIdentificacion();
        LlenarDDLTipoActivo();
        InicializarBienesActivosFijos();
        InicializarCuentasBan();
        LineaAporteObliga();
    }
    private void InicializarBienesActivosFijos()
    {
        List<Garantia> lstBienes = new List<Garantia>(1) { new Garantia() };
        gvBienesActivos.DataSource = lstBienes;
        gvBienesActivos.DataBind();
    }
    protected void btnAgregarFila_Click(object sender, EventArgs e)
    {
        Usuario pUsuario = (Usuario)Session["usuario"];

        ObtenerListaCuentaBanc();

        List<CuentasBancarias> lstBene = new List<CuentasBancarias>();

        if (Session[pUsuario.codusuario + "DatosCuentaBanc"] != null)
        {
            lstBene = (List<CuentasBancarias>)Session[pUsuario.codusuario + "DatosCuentaBanc"];

            for (int i = 1; i <= 1; i++)
            {
                CuentasBancarias eCuenta = new CuentasBancarias();
                eCuenta.idcuentabancaria = -1;
                eCuenta.tipo_cuenta = null;
                eCuenta.numero_cuenta = "";
                eCuenta.cod_banco = null;
                eCuenta.sucursal = "";
                eCuenta.fecha_apertura = null;
                eCuenta.principal = null;
                lstBene.Add(eCuenta);
            }
            gvCuentasBancarias.DataSource = lstBene;
            gvCuentasBancarias.DataBind();

            Session[pUsuario.codusuario + "DatosCuentaBanc"] = lstBene;
        }
    }
    protected string FormatoFecha()
    {
        //Configuracion conf = new Configuracion();
        return ""; // conf.ObtenerFormatoFecha();
    }
    protected void cbSeleccionar_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxGrid cbSeleccionar = (CheckBoxGrid)sender;
        int nItem = Convert.ToInt32(cbSeleccionar.CommandArgument);

        foreach (GridViewRow rFila in gvCuentasBancarias.Rows)
        {
            CheckBoxGrid check = (CheckBoxGrid)rFila.FindControl("cbSeleccionar");
            check.Checked = false;
            if (rFila.RowIndex == nItem)
            {
                check.Checked = true;
            }
        }
    }
    protected void InicializarCuentasBan()
    {
        Usuario pUsuario = (Usuario)Session["usuario"];
        List<CuentasBancarias> lstCuentasBan = new List<CuentasBancarias>();
        for (int i = gvCuentasBancarias.Rows.Count; i < 5; i++)
        {
            CuentasBancarias eCuenta = new CuentasBancarias();
            eCuenta.idcuentabancaria = -1;
            eCuenta.tipo_cuenta = null;
            eCuenta.numero_cuenta = "";
            eCuenta.cod_banco = null;
            eCuenta.sucursal = "";
            eCuenta.fecha_apertura = null;
            eCuenta.principal = null;
            lstCuentasBan.Add(eCuenta);
        }
        gvCuentasBancarias.DataSource = lstCuentasBan;
        gvCuentasBancarias.DataBind();

        Session[pUsuario.codusuario + "DatosCuentaBanc"] = lstCuentasBan;
    }
    protected void gvCuentasBancarias_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DropDownList ddltipoProducto = (DropDownList)e.Row.FindControl("ddltipoProducto");
        DropDownList ddlentidad = (DropDownList)e.Row.FindControl("ddlentidad");

        if (ddltipoProducto != null)
        {
            ddltipoProducto.Items.Insert(0, new ListItem("AHORROS", "0"));
            ddltipoProducto.Items.Insert(1, new ListItem("CORRIENTE", "1"));
            ddltipoProducto.SelectedIndex = 0;
            ddltipoProducto.DataBind();
        }

        if (ddlentidad != null)
        {
            BancosService bancoService = new BancosService();
            Bancos banco = new Bancos();
            ddlentidad.DataSource = bancoService.ListarBancos(banco, (Usuario)Session["usuario"]);
            ddlentidad.DataTextField = "nombrebanco";
            ddlentidad.DataValueField = "cod_banco";
            ddlentidad.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            ddlentidad.SelectedIndex = 0;
            ddlentidad.DataBind();
        }

        Label lbltipoProducto = (Label)e.Row.FindControl("lbltipoProducto");
        if (lbltipoProducto != null)
            ddltipoProducto.SelectedValue = lbltipoProducto.Text;

        Label lblentidad = (Label)e.Row.FindControl("lblentidad");
        if (lblentidad != null)
            ddlentidad.SelectedValue = lblentidad.Text;

    }
    protected void gvCuentasBancarias_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Usuario pUsuario = (Usuario)Session["usuario"];
        int conseID = Convert.ToInt32(gvCuentasBancarias.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaCuentaBanc();

        List<CuentasBancarias> LstCuentas;
        LstCuentas = (List<CuentasBancarias>)Session[pUsuario.codusuario + "DatosCuentaBanc"];

        if (conseID > 0)
        {
            try
            {
                foreach (CuentasBancarias acti in LstCuentas)
                {
                    if (acti.idcuentabancaria == conseID)
                    {
                        ActiServices.EliminarCuentasBancarias(conseID, (Usuario)Session["usuario"]);
                        LstCuentas.Remove(acti);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                lblerror.Text = (ex.Message);
            }
        }
        else
        {
            LstCuentas.RemoveAt((gvCuentasBancarias.PageIndex * gvCuentasBancarias.PageSize) + e.RowIndex);
        }
        gvCuentasBancarias.DataSourceID = null;
        gvCuentasBancarias.DataBind();

        gvCuentasBancarias.DataSource = LstCuentas;
        gvCuentasBancarias.DataBind();

        Session[pUsuario.codusuario + "DatosCuentaBanc"] = LstCuentas;
    }
    protected void chkMonedaExtranjera_CheckedChanged(object sender, EventArgs e)
    {
        if (chkMonedaExtranjera.Checked)
        {
            panelMonedaExtranjera.Visible = true;
        }
        else
        {
            panelMonedaExtranjera.Visible = false;
        }
    }
    protected void btnAgregarFilaM_Click(object sender, EventArgs e)
    {
        ObtenerListaMonedaExt();
        List<EstadosFinancieros> lsMonedaExtranjera = new List<EstadosFinancieros>();
        if (Session["DatosCuentaExtranjera"] != null)
        {
            lsMonedaExtranjera = (List<EstadosFinancieros>)Session["DatosCuentaExtranjera"];
            for (int i = 1; i <= 1; i++)
            {
                EstadosFinancieros eMonedaExtranjera = new EstadosFinancieros();
                eMonedaExtranjera.cod_moneda_ext = 0;
                eMonedaExtranjera.num_cuenta_ext = "";
                eMonedaExtranjera.banco_ext = null;
                eMonedaExtranjera.pais = null;
                eMonedaExtranjera.ciudad = null;
                eMonedaExtranjera.moneda = null;
                eMonedaExtranjera.desc_operacion = null;
                lsMonedaExtranjera.Add(eMonedaExtranjera);
            }
            gvMonedaExtranjera.DataSource = lsMonedaExtranjera;
            gvMonedaExtranjera.DataBind();
            Session["DatosCuentaExtranjera"] = lsMonedaExtranjera;
        }
    }
    protected void gvMonedaExtranjera_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int cod = Convert.ToInt32(gvMonedaExtranjera.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaMonedaExt();

        List<EstadosFinancieros> LstCuentas;
        LstCuentas = (List<EstadosFinancieros>)Session["DatosCuentaExtranjera"];

        if (cod > 0)
        {
            try
            {
                foreach (EstadosFinancieros eMoneda in LstCuentas)
                {
                    if (eMoneda.cod_moneda_ext == cod)
                    {
                        EstadosFinancierosServicio.EliminarCuentasMonedaExtranjera(cod, (Usuario)Session["usuario"]);
                        LstCuentas.Remove(eMoneda);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                lblerror.Text = (ex.Message);
            }
        }
        else
        {
            LstCuentas.RemoveAt((gvMonedaExtranjera.PageIndex * gvMonedaExtranjera.PageSize) + e.RowIndex);
        }
        gvMonedaExtranjera.DataSourceID = null;
        gvMonedaExtranjera.DataBind();

        gvMonedaExtranjera.DataSource = LstCuentas;
        gvMonedaExtranjera.DataBind();

        Session["DatosCuentaExtranjera"] = LstCuentas;
    }
    protected void chkTransaccionExterior_CheckedChanged(object sender, EventArgs e)
    {
        if (chkTransaccionExterior.Checked)
        {
            pProductosExt.Visible = true;
        }
        else
        {
            pProductosExt.Visible = false;
        }
    }
    protected void btnProductoExt_Click(object sender, EventArgs e)
    {
        ObtenerListaProductosExt();
        List<EstadosFinancieros> lstProductosExt = new List<EstadosFinancieros>();
        if (Session["DatosProductosFinExt"] != null)
        {
            lstProductosExt = (List<EstadosFinancieros>)Session["DatosProductosFinExt"];
            for (int i = 1; i <= 1; i++)
            {
                EstadosFinancieros eProducto = new EstadosFinancieros();
                eProducto.cod_moneda_ext = 0;
                eProducto.tipo_producto = "";
                eProducto.num_cuenta_ext = "";
                eProducto.pais = null;
                eProducto.ciudad = null;
                eProducto.moneda = null;
                lstProductosExt.Add(eProducto);
            }
            gvProductosExterior.DataSource = lstProductosExt;
            gvProductosExterior.DataBind();
            Session["DatosProductosFinExt"] = lstProductosExt;
        }
    }
    protected void gvProductosExterior_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int cod = Convert.ToInt32(gvProductosExterior.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaProductosExt();

        List<EstadosFinancieros> LstProductos;
        LstProductos = (List<EstadosFinancieros>)Session["DatosProductosFinExt"];

        if (cod > 0)
        {
            try
            {
                foreach (EstadosFinancieros eMoneda in LstProductos)
                {
                    if (eMoneda.cod_moneda_ext == cod)
                    {
                        EstadosFinancierosServicio.EliminarCuentasMonedaExtranjera(cod, (Usuario)Session["usuario"]);
                        LstProductos.Remove(eMoneda);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                lblerror.Text = (ex.Message);
            }
        }
        else
        {
            LstProductos.RemoveAt((gvProductosExterior.PageIndex * gvMonedaExtranjera.PageSize) + e.RowIndex);
        }
        gvProductosExterior.DataSourceID = null;
        gvProductosExterior.DataBind();

        gvProductosExterior.DataSource = LstProductos;
        gvProductosExterior.DataBind();
        Session["DatosProductosFinExt"] = LstProductos;
    }
    protected void InicializarModal(object sender, EventArgs e)
    {

        Persona1 Entidad = new Persona1();
        if (Session[Usuario.codusuario + "cod_per"] != null)
        {
            string id = Convert.ToString(Session[Usuario.codusuario + "cod_per"]);
            Entidad.cod_persona = Convert.ToInt64(id);
            Entidad.seleccionar = "Cod_persona";
            Entidad.soloPersona = 1;

            if (Session["Persona"] != null)
            {
                Entidad = (Persona1)Session["Persona"];
            }
            else
            {
                Entidad = ServicePersona.ConsultarPersona1Param(Entidad, (Usuario)Session["usuario"]);
            }

            lblTipoProceso.Text = "";
            txtModalNombres.Text = (Entidad.primer_nombre != null ? Entidad.primer_nombre.ToString() : "") + " " + (Entidad.segundo_nombre != null ? Entidad.segundo_nombre.ToString() : "") + " " + (Entidad.primer_apellido != null ? Entidad.primer_apellido.ToString() : "") + " " + (Entidad.segundo_apellido != null ? Entidad.segundo_apellido.ToString() : "");
            txtModalIdentificacion.Text = Entidad.identificacion.ToString();
            ddlModalIdentificacion.SelectedValue = Entidad.tipo_identificacion.ToString();
            //Simulo evento para llenar ddl "Rango Vivienda"
            ddlModalVIS_SelectedIndexChanged(this, EventArgs.Empty);
            //Simulo evento para llenar ddl "Tipo Activo"
            ddlModalTipoActivo.SelectedIndex = 0;
            ddlModalTipoActivo_SelectedIndexChanged(this, EventArgs.Empty);
            lblErrorModal.Text = "";
            lblTipoProceso.Text = "";
            ddlEstadoModal.SelectedValue = "1";
            ddlEstadoModal.Enabled = false;
        }
    }
    protected void ddlModalTipoActivo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] tipoActivoSeleccionado = ddlModalTipoActivo.SelectedItem.Value.Split('-');

        if (tipoActivoSeleccionado[0] == ((char)TipoActivoFijo.Inmueble).ToString())
        {
            panelTipoActivoInmueble.Visible = true;
            pnlTipoActivoMaquinaria.Visible = false;
        }
        else if (tipoActivoSeleccionado[0] == ((char)TipoActivoFijo.Vehiculo).ToString())
        {
            panelTipoActivoInmueble.Visible = false;
            pnlTipoActivoMaquinaria.Visible = true;
        }
        else
        {
            panelTipoActivoInmueble.Visible = false;
            pnlTipoActivoMaquinaria.Visible = false;
        }
    }
    protected void ddlModalVIS_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dataConVIS = new[]
        {
             new { Valor = 1, Descripcion = "Tipo 1: Cuyo valor de la vivienda sea menor o igual a 50 SMML"} ,
             new { Valor = 2, Descripcion = "Tipo 2: Cuyo valor de la vivienda sea mayor a 50 SMML y menor o igual a 70 SMML"} ,
             new { Valor = 3, Descripcion = "Tipo 3: Cuyo valor de la vivienda sea mayor a 70 SMML y menor o igual a 100 SMML"} ,
             new { Valor = 4, Descripcion = "Tipo 4: Cuyo valor de la vivienda sea mayor a 100 SMML y menor o igual a 135 SMML"} ,
        };

        var dataSinVIS = new[]
        {
             new { Valor = 5, Descripcion = "Rango 1: Cuyo monto sea mayor a VIS y menor o igual a 643.100 UVR"} ,
             new { Valor = 6, Descripcion = "Rango 2: Cuyo monto sea mayor a 643.100 UVR y menor o igual a 2’411.625 UVR"} ,
             new { Valor = 7, Descripcion = "Rango 3: Cuyo valor sea mayor a 2’411.625 UVR"} ,
        };


        int tieneVIS = Convert.ToInt32(ddlModalVIS.SelectedValue);

        if (tieneVIS == (int)Tiene.Si)
        {
            ddlModalRangoVivienda.DataSource = dataConVIS;
        }
        else
        {
            ddlModalRangoVivienda.DataSource = dataSinVIS;
        }

        ddlModalRangoVivienda.DataTextField = "Descripcion";
        ddlModalRangoVivienda.DataValueField = "Valor";
        ddlModalRangoVivienda.DataBind();
    }
    protected void gvBienesActivos_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Delete")
            {
                if (!btnBienesActivos.Visible)
                {
                    return;
                }

                GarantiaService garantiaService = new GarantiaService();
                List<Garantia> lstReferencia = RecorresGrillaReferencias();
                int index = Convert.ToInt32(e.CommandArgument);
                Garantia garantia = lstReferencia[index];

                lstReferencia.RemoveAt(index);
                string pError = string.Empty;
                garantiaService.EliminarActivoFijo(garantia.IdActivo, 0, ref pError, (Usuario)Session["usuario"]);

                if (lstReferencia.Count == 0)
                {
                    lstReferencia.Add(new Garantia());
                }
                gvBienesActivos.DataSource = lstReferencia;
                gvBienesActivos.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblerror.Text = ("gvBienesActivos_OnRowCommand, " + ex.Message);
        }
    }
    private List<Garantia> RecorresGrillaReferencias()
    {
        List<Garantia> lstBienes = new List<Garantia>();

        foreach (GridViewRow gFila in gvBienesActivos.Rows)
        {
            Garantia garantia = new Garantia()
            {
                IdActivo = Convert.ToInt64(gvBienesActivos.DataKeys[gFila.RowIndex].Value),
                descripcion_activo = gFila.Cells[3].Text,
                Descripcion = gFila.Cells[4].Text,
                Fecha_adquisicionactivo = !string.IsNullOrWhiteSpace(gFila.Cells[5].Text) ? Convert.ToDateTime(gFila.Cells[5].Text) : default(DateTime?),
                valor_comercial = !string.IsNullOrWhiteSpace(gFila.Cells[6].Text) ? Convert.ToInt64(gFila.Cells[6].Text.Replace(".", "")) : default(long?),
                estado_descripcion = gFila.Cells[6].Text
            };

            lstBienes.Add(garantia);
        }

        return lstBienes;

    }
    protected void gvBienesActivos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Int64 conseID = Convert.ToInt64(gvBienesActivos.DataKeys[e.NewEditIndex].Values[0].ToString());
        lblErrorModal.Text = "";
        if (conseID != 0)
        {
            gvBienesActivos.EditIndex = e.NewEditIndex;
            ObtenerBienesActivos(conseID);
            mpeNuevoActividad.Show();
        }
        else
        {
            e.Cancel = true;
        }
        e.NewEditIndex = -1;
    }
    protected List<EstadosFinancieros> ObtenerListaProductosExt()
    {
        List<EstadosFinancieros> lstProductos = new List<EstadosFinancieros>();
        List<EstadosFinancieros> lista = new List<EstadosFinancieros>();

        foreach (GridViewRow filaMoneda in gvProductosExterior.Rows)
        {
            EstadosFinancieros Producto = new EstadosFinancieros();

            Label lblCodProducto = (Label)filaMoneda.FindControl("lblCodProducto");
            if (lblCodProducto != null)
                Producto.cod_moneda_ext = Convert.ToInt64(lblCodProducto.Text);

            TextBox txtTipoProducto = (TextBox)filaMoneda.FindControl("txtTipoProducto");
            if (txtTipoProducto != null)
                Producto.tipo_producto = Convert.ToString(txtTipoProducto.Text);

            TextBox txtNumProducto = (TextBox)filaMoneda.FindControl("txtNumProducto");
            if (txtNumProducto != null)
                Producto.num_cuenta_ext = txtNumProducto.Text != null && txtNumProducto.Text != "" ? txtNumProducto.Text : "";

            TextBox txtNomPais = (TextBox)filaMoneda.FindControl("txtNomPais");
            if (txtNomPais != null)
                Producto.pais = Convert.ToString(txtNomPais.Text);

            TextBox txtNomCiudad = (TextBox)filaMoneda.FindControl("txtNomCiudad");
            if (txtNomCiudad != null)
                Producto.ciudad = Convert.ToString(txtNomCiudad.Text);

            TextBox txtNomMoneda = (TextBox)filaMoneda.FindControl("txtNomMoneda");
            if (txtNomMoneda != null)
                Producto.moneda = Convert.ToString(txtNomMoneda.Text);

            Producto.banco_ext = "N.A";
            Producto.desc_operacion = "N.A";

            lista.Add(Producto);

            if (Producto.num_cuenta_ext != "" && Producto.num_cuenta_ext != null)
            {
                lstProductos.Add(Producto);
            }
        }
        Session["DatosProductosFinExt"] = lista;
        return lstProductos;
    }
    protected List<CuentasBancarias> ObtenerListaCuentaBanc()
    {
        Usuario pUsuario = (Usuario)Session["usuario"];
        List<CuentasBancarias> lstListaCuenta = new List<CuentasBancarias>();
        List<CuentasBancarias> lista = new List<CuentasBancarias>();

        foreach (GridViewRow rfila in gvCuentasBancarias.Rows)
        {
            CuentasBancarias eCuenta = new CuentasBancarias();
            Label lblidcuentabancaria = (Label)rfila.FindControl("lblidcuentabancaria");
            if (lblidcuentabancaria != null)
                eCuenta.idcuentabancaria = Convert.ToInt32(lblidcuentabancaria.Text);

            DropDownListGrid ddltipoProducto = (DropDownListGrid)rfila.FindControl("ddltipoProducto");
            if (ddltipoProducto.SelectedValue != null)
                eCuenta.tipo_cuenta = Convert.ToInt32(ddltipoProducto.SelectedValue);

            TextBox txtnum_Producto = (TextBox)rfila.FindControl("txtnum_Producto");
            if (txtnum_Producto != null)
                eCuenta.numero_cuenta = Convert.ToString(txtnum_Producto.Text.Trim());

            DropDownListGrid ddlentidad = (DropDownListGrid)rfila.FindControl("ddlentidad");
            if (ddlentidad.SelectedValue != null)
                eCuenta.cod_banco = Convert.ToInt32(ddlentidad.SelectedValue);

            TextBox txtsucursal = (TextBox)rfila.FindControl("txtsucursal");
            if (txtsucursal != null)
                if (txtsucursal.Text != "")
                    eCuenta.sucursal = Convert.ToString(txtsucursal.Text.ToUpper());
                else
                    eCuenta.sucursal = null;

            fecha txtfecha = (fecha)rfila.FindControl("txtfecha");
            if (txtfecha != null)
                if (txtfecha.Text != "")
                    eCuenta.fecha_apertura = txtfecha.ToDateTime;
                else
                    eCuenta.fecha_apertura = null;
            else
                eCuenta.fecha_apertura = null;
            CheckBoxGrid cbSeleccionar = (CheckBoxGrid)rfila.FindControl("cbSeleccionar");
            if (cbSeleccionar.Checked == true)
                eCuenta.principal = 1;
            else
                eCuenta.principal = 0;

            lista.Add(eCuenta);

            if (eCuenta.numero_cuenta != "" && eCuenta.cod_banco.Value != 0)
            {
                lstListaCuenta.Add(eCuenta);
            }
        }
        Session[pUsuario.codusuario + "DatosCuentaBanc"] = lista;
        return lstListaCuenta;
    }
    protected List<EstadosFinancieros> ObtenerListaMonedaExt()
    {
        List<EstadosFinancieros> lstListaMoneda = new List<EstadosFinancieros>();
        List<EstadosFinancieros> lista = new List<EstadosFinancieros>();

        foreach (GridViewRow filaMoneda in gvMonedaExtranjera.Rows)
        {
            EstadosFinancieros Moneda = new EstadosFinancieros();

            Label lblCodMoneda = (Label)filaMoneda.FindControl("lblCodMoneda");
            if (lblCodMoneda != null)
                Moneda.cod_moneda_ext = Convert.ToInt64(lblCodMoneda.Text);

            TextBox txtNumCuentaExt = (TextBox)filaMoneda.FindControl("txtNumCuentaExt");
            if (txtNumCuentaExt != null)
                Moneda.num_cuenta_ext = txtNumCuentaExt.Text != null && txtNumCuentaExt.Text != "" ? txtNumCuentaExt.Text : "";

            TextBox txtNomBancoExt = (TextBox)filaMoneda.FindControl("txtNomBancoExt");
            if (txtNomBancoExt != null)
                Moneda.banco_ext = txtNomBancoExt.Text != null && txtNomBancoExt.Text != "" ? Convert.ToString(txtNomBancoExt.Text) : "";

            TextBox txtNomPais = (TextBox)filaMoneda.FindControl("txtNomPais");
            if (txtNomPais != null)
                Moneda.pais = Convert.ToString(txtNomPais.Text);

            TextBox txtNomCiudad = (TextBox)filaMoneda.FindControl("txtNomCiudad");
            if (txtNomCiudad != null)
                Moneda.ciudad = Convert.ToString(txtNomCiudad.Text);

            TextBox txtNomMoneda = (TextBox)filaMoneda.FindControl("txtNomMoneda");
            if (txtNomMoneda != null)
                Moneda.moneda = Convert.ToString(txtNomMoneda.Text);

            TextBox txtOperacion = (TextBox)filaMoneda.FindControl("txtOperacion");
            if (txtOperacion != null)
                Moneda.desc_operacion = Convert.ToString(txtOperacion.Text);

            Moneda.tipo_producto = "";

            lista.Add(Moneda);

            if (Moneda.num_cuenta_ext != "" && Moneda.num_cuenta_ext != null)
            {
                lstListaMoneda.Add(Moneda);
            }
        }
        Session["DatosCuentaExtranjera"] = lista;
        return lstListaMoneda;
    }
    protected void ObtenerBienesActivos(Int64 IdGarantia)
    {
        GarantiaService garantiasservicio = new GarantiaService();

        lblerror.Text = ("");
        try
        {
            Persona1 Entidad = (Persona1)Session["Persona"];

            lblTipoProceso.Text = IdGarantia.ToString();
            ddlEstadoModal.Enabled = true;
            txtModalNombres.Text = (Entidad.primer_nombre != null ? Entidad.primer_nombre.ToString() : "") + " " + (Entidad.segundo_nombre != null ? Entidad.segundo_nombre.ToString() : "") + " " + (Entidad.primer_apellido != null ? Entidad.primer_apellido.ToString() : "") + " " + (Entidad.segundo_apellido != null ? Entidad.segundo_apellido.ToString() : "");
            txtModalIdentificacion.Text = Entidad.identificacion.ToString();
            ddlModalIdentificacion.SelectedValue = Entidad.tipo_identificacion.ToString();

            ActivoFijo pActivo = new ActivoFijo();
            pActivo = garantiasservicio.ConsultarActivoFijoPersonal(IdGarantia, (Usuario)Session["usuario"]);
            if (pActivo != null)
            {
                if (pActivo.cod_tipo_activo_per != null)
                {
                    string value = pActivo.str_clase + "-" + pActivo.cod_tipo_activo_per;
                    ddlModalTipoActivo.SelectedValue = value;
                    ddlModalTipoActivo_SelectedIndexChanged(this, EventArgs.Empty);
                    ddlEstadoModal.SelectedValue = pActivo.estado.ToString();
                    txtModalDescripcion.Text = pActivo.descripcion != null ? pActivo.descripcion : string.Empty;
                    txtModalFechaIni.Text = pActivo.fecha_compra != null && pActivo.fecha_compra != DateTime.MinValue ? pActivo.fecha_compra.ToString() : string.Empty;
                    txtModalValorComercial.Text = pActivo.valor_compra.ToString();
                    txtModalValorComprometido.Text = pActivo.valor_comprometido.ToString();

                    if (pActivo.str_clase == "H")
                    {
                        if (!string.IsNullOrEmpty(pActivo.direccion))
                            txtModalDireccion.Text = HttpUtility.HtmlDecode(pActivo.direccion);
                        if (!string.IsNullOrEmpty(pActivo.localizacion))
                            txtModalLocalizacion.Text = HttpUtility.HtmlDecode(pActivo.localizacion);
                        if (pActivo.SENALVIS != null)
                            ddlModalVIS.SelectedValue = pActivo.SENALVIS.ToString();
                        if (!string.IsNullOrEmpty(pActivo.matricula))
                            txtModalNoMatricula.Text = pActivo.matricula;
                        if (!string.IsNullOrEmpty(pActivo.escritura))
                            txtModalEscritura.Text = pActivo.escritura;
                        if (!string.IsNullOrEmpty(pActivo.notaria))
                            txtModalNotaria.Text = pActivo.notaria;
                        if (!string.IsNullOrEmpty(pActivo.entidad_redescuento))
                            ddlModalEntidadReDesc.SelectedValue = pActivo.entidad_redescuento;
                        if (!string.IsNullOrEmpty(pActivo.margen_redescuento))
                            txtModalmargenReDesc.Text = pActivo.margen_redescuento;
                        if (!string.IsNullOrEmpty(pActivo.tipo_vivienda))
                            ddlModalTipoVivienda.SelectedValue = pActivo.tipo_vivienda;
                        if (!string.IsNullOrEmpty(pActivo.desembolso))
                            ddlModalDesembolso.SelectedValue = pActivo.desembolso;
                        if (!string.IsNullOrEmpty(pActivo.desembolso_directo))
                            txtModalDesembolsoDirecto.Text = pActivo.desembolso_directo;
                        ddlModalVIS_SelectedIndexChanged(this, EventArgs.Empty);
                        if (!string.IsNullOrEmpty(pActivo.rango_vivienda))
                            ddlModalRangoVivienda.SelectedValue = pActivo.rango_vivienda;
                        if (pActivo.hipoteca != null && pActivo.hipoteca != 0)
                            chkHipoteca.Checked = true;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(pActivo.marca))
                            txtModalMarca.Text = pActivo.marca;
                        if (!string.IsNullOrEmpty(pActivo.referencia))
                            txtModalReferencia.Text = pActivo.referencia;
                        if (!string.IsNullOrEmpty(pActivo.modelo))
                            txtModalModelo.Text = pActivo.modelo;
                        if (pActivo.cod_uso != null)
                            ddlModalUso.SelectedValue = pActivo.cod_uso.ToString();
                        if (!string.IsNullOrEmpty(pActivo.num_chasis))
                            txtModalNoChasis.Text = pActivo.num_chasis;
                        if (!string.IsNullOrEmpty(pActivo.capacidad))
                            txtModalCapacidad.Text = pActivo.capacidad;
                        if (!string.IsNullOrEmpty(pActivo.num_motor))
                            txtModalNoSerieMotor.Text = pActivo.num_motor;
                        if (!string.IsNullOrEmpty(pActivo.placa))
                            txtModalPlaca.Text = pActivo.placa;
                        if (!string.IsNullOrEmpty(pActivo.color))
                            txtModalColor.Text = pActivo.color;
                        if (!string.IsNullOrEmpty(pActivo.documentos_importacion))
                            txtModalDocImportacion.Text = pActivo.documentos_importacion;
                        if (pActivo.fecha_importacion != null && pActivo.fecha_importacion != DateTime.MinValue)
                            txtModalFechaImportacion.Text = pActivo.fecha_importacion.ToString();
                        if (pActivo.porcentaje_pignorado != null && pActivo.porcentaje_pignorado != 0 && pActivo.porcentaje_pignorado > 0)
                        {
                            chkPignorado.Checked = true;
                            txtPorcPignorado.Text = pActivo.porcentaje_pignorado.ToString();
                            txtPorcPignorado.Visible = true;
                            lblPorcPignorado.Visible = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblerror.Text = (ex.Message);
        }
    }
    protected void btnCancelarModal_click(object sender, EventArgs e)
    {
        mpeNuevoActividad.Hide();
        VaciarFormularioActivoFijo(upReclasificacion);
    }
    public void VaciarFormularioActivoFijo(Control pControl)
    {
        foreach (var controlhij in pControl.Controls)
        {
            if (controlhij is TextBox)
            {
                var texbox = controlhij as TextBox;
                if (!texbox.ID.Contains(txtModalNombres.ID) || !texbox.ID.Contains(txtModalIdentificacion.ID))
                {
                    texbox.Text = "";
                }
            }
            else
            {
                VaciarFormularioActivoFijo((Control)controlhij);
            }
        }
    }
    protected void btnGuardarModal_click(object sender, EventArgs e)
    {
        try
        {
            lblErrorModal.Text = "";
            string error = string.Empty;

            // Valido Campos Obligatorios y lleno entidad
            ActivoFijo activoFijo = ValidarCamposActivoFijo(out error);

            // Si hay algun error notifico y retorno
            if (!string.IsNullOrWhiteSpace(error))
            {
                lblErrorModal.Text = error;
                return;
            }

            // Lleno el resto de la entidad segun tipo Activo seleccionado en el DDL y procedo a guardar
            // Si tengo un tipo activo invalido retorno
            bool tipoActivoSeleccionadoCorrecto = LlenarEntidadActivoFijoGuardar(activoFijo);

            if (!tipoActivoSeleccionadoCorrecto)
            {
                return;
            }
            GarantiaService garantiasservicio = new GarantiaService();
            if (string.IsNullOrEmpty(lblTipoProceso.Text))
                garantiasservicio.CrearActivoFijoPersonal(activoFijo, (Usuario)Session["usuario"]);
            else
                garantiasservicio.ModificarActivoFijoPersonal(activoFijo, (Usuario)Session["usuario"]);
            LlenarGVActivoFijos(Session[Usuario.codusuario + "cod_per"].ToString());
            mpeNuevoActividad.Hide();

            VaciarFormularioActivoFijo(upReclasificacion);
            InicializarModal(this, EventArgs.Empty);

            RegistrarPostBack();
        }
        catch (Exception ex)
        {
            lblErrorModal.Text = "btnGuardarModal_click: " + ex.Message;
        }
    }
    private void LlenarGVActivoFijos(string objeto)
    {
        List<Garantia> lstConsultas = new List<Garantia>(1);
        GarantiaService garantiasservicio = new GarantiaService();

        try
        {
            lstConsultas = garantiasservicio.Listaractivos(objeto, (Usuario)Session["usuario"]);
        }
        catch (Exception ex)
        {
            lblerror.Text = ("LlenarGVActivoFijos: " + ex.Message);
            return;
        }

        if (lstConsultas.Count == 0)
        {
            lstConsultas.Add(new Garantia());
        }

        gvBienesActivos.DataSource = lstConsultas;
        gvBienesActivos.DataBind();
    }
    private bool LlenarEntidadActivoFijoGuardar(ActivoFijo activoFijo)
    {
        string[] tipoActivoSeleccionado = ddlModalTipoActivo.SelectedItem.Value.Split('-');
        activoFijo.cod_tipo_activo_per = Convert.ToInt64(tipoActivoSeleccionado[1]);

        if (tipoActivoSeleccionado[0] == ((char)TipoActivoFijo.Inmueble).ToString())
        {
            LlenarEntidadActivoFijoInmueble(activoFijo);
        }
        else if (tipoActivoSeleccionado[0] == ((char)TipoActivoFijo.Vehiculo).ToString())
        {
            LlenarEntidadActivoFijoVehiculo(activoFijo);
        }
        else
        {
            return false;
        }
        return true;
    }
    private void LlenarEntidadActivoFijoInmueble(ActivoFijo activoFijo)
    {
        activoFijo.direccion = txtModalDireccion.Text;
        activoFijo.localizacion = txtModalLocalizacion.Text;
        activoFijo.matricula = txtModalNotaria.Text;
        activoFijo.escritura = txtModalEscritura.Text;
        activoFijo.notaria = txtModalNotaria.Text;
        activoFijo.SENALVIS = Convert.ToInt32(ddlModalVIS.SelectedValue);
        activoFijo.tipo_vivienda = ddlModalTipoVivienda.SelectedValue;
        activoFijo.rango_vivienda = ddlModalRangoVivienda.SelectedValue;
        activoFijo.entidad_redescuento = ddlModalEntidadReDesc.SelectedValue;
        activoFijo.margen_redescuento = txtModalmargenReDesc.Text == "" ? null : txtModalmargenReDesc.Text;
        activoFijo.desembolso_directo = txtModalDesembolsoDirecto.Text == "" ? null : txtModalDesembolsoDirecto.Text;
        activoFijo.desembolso = ddlModalDesembolso.SelectedValue;
        activoFijo.hipoteca = chkHipoteca.Checked ? 1 : 0;
    }
    private void LlenarEntidadActivoFijoVehiculo(ActivoFijo activoFijo)
    {
        string fechaImportacion = txtModalFechaImportacion.Text;

        if (!string.IsNullOrWhiteSpace(fechaImportacion))
        {
            activoFijo.fecha_importacion = Convert.ToDateTime(txtModalFechaImportacion.Text);
        }

        activoFijo.marca = txtModalMarca.Text;
        activoFijo.referencia = txtModalReferencia.Text;
        activoFijo.modelo = txtModalModelo.Text;
        activoFijo.cod_uso = Convert.ToInt32(ddlModalUso.SelectedValue);
        activoFijo.capacidad = txtModalCapacidad.Text;
        activoFijo.num_chasis = txtModalNoChasis.Text;
        activoFijo.num_motor = txtModalNoSerieMotor.Text;
        activoFijo.placa = txtModalPlaca.Text;
        activoFijo.color = txtModalColor.Text;
        activoFijo.documentos_importacion = txtModalDocImportacion.Text;
        if (chkPignorado.Checked)
        {
            if (string.IsNullOrWhiteSpace(txtPorcPignorado.Text))
            {
                lblerror.Text = ("El vehiculo se encuentra marcado como pignorado, registre el porcentaje");
                return;
            }
            else
                activoFijo.porcentaje_pignorado = Convert.ToInt32(txtPorcPignorado.Text);
        }
        activoFijo.porcentaje_pignorado = 0;
    }
    private ActivoFijo ValidarCamposActivoFijo(out string error)
    {
        ActivoFijo activoFijo = new ActivoFijo();
        string fechaCompra = txtModalFechaIni.Text;
        string valor_avaluo = txtModalValorComercial.Text;
        string valor_comprometido = txtModalValorComprometido.Text;
        error = string.Empty;

        /*if (string.IsNullOrWhiteSpace(valor_avaluo))
        {
            error += " Valor Comercial debe tener un valor valido, ";
            return activoFijo;
        }*/
        if (string.IsNullOrWhiteSpace(fechaCompra))
        {
            error += " Fecha de Adquisición debe ser llenada, ";
            return activoFijo;
        }

        if (string.IsNullOrEmpty(lblTipoProceso.Text))
        {
            foreach (GridViewRow row in gvBienesActivos.Rows)
            {
                string pCod = row.Cells[2].Text;
                if (!string.IsNullOrEmpty(pCod) && pCod != "0")
                {
                    Decimal a = Convert.ToDecimal(row.Cells[6].Text);
                    DateTime b = Convert.ToDateTime(row.Cells[5].Text);
                    string c = Convert.ToString(row.Cells[4].Text);
                    if (c == Convert.ToString(txtModalDescripcion.Text))
                    {
                        if (b == Convert.ToDateTime(fechaCompra))
                        {
                            if (a == Convert.ToDecimal(valor_avaluo))
                            {
                                error += "Valores repetidos con los anteriores Activos Fijos";
                                return activoFijo;
                            }
                        }
                    }
                }
            }
        }


        activoFijo.idActivo = string.IsNullOrEmpty(lblTipoProceso.Text) ? 0 : Convert.ToInt32(lblTipoProceso.Text);
        activoFijo.fecha_compra = Convert.ToDateTime(fechaCompra);
        activoFijo.valor_comprometido = string.IsNullOrWhiteSpace(valor_comprometido) ? 0 : Convert.ToDecimal(valor_comprometido);
        activoFijo.valor_compra = string.IsNullOrWhiteSpace(valor_avaluo) ? 0 : Convert.ToDecimal(valor_avaluo);
        activoFijo.cod_persona = Convert.ToInt64(Session[Usuario.codusuario + "cod_per"]);   //Convert.ToInt64(Session[_afiliacionServicio.CodigoPrograma + ".id"]);
        activoFijo.descripcion = txtModalDescripcion.Text;
        activoFijo.estado = ddlEstadoModal.SelectedIndex;
        return activoFijo;
    }
    protected void chkPignorado_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPignorado.Checked)
        {
            txtPorcPignorado.Visible = true;
            lblPorcPignorado.Visible = true;
        }
        else
        {
            txtPorcPignorado.Visible = false;
            lblPorcPignorado.Visible = false;
        }
    }
    protected void LlenarDDLTipoActivo()
    {
        ActivosFijoservices activoService = new ActivosFijoservices();

        var lstActivoDataSource = from lista in activoService.ListarTipoActivoFijo((Usuario)Session["usuario"])
                                  select new
                                  {
                                      Descripcion = lista.nomclase,
                                      Value = lista.str_clase.ToString() + "-" + lista.cod_act.ToString()
                                  };

        ddlModalTipoActivo.DataSource = lstActivoDataSource;
        ddlModalTipoActivo.DataTextField = "Descripcion";
        ddlModalTipoActivo.DataValueField = "Value";
        ddlModalTipoActivo.DataBind();

        // Necesario tener el "-" para que no explote en el Split en el SelectIndex Event
        ddlModalTipoActivo.Items.Insert(0, new ListItem("Seleccione un Tipo", "0-0"));
    }
    protected void LlenarDDLTipoIdentificacion()
    {
        TipoIdenService IdenService = new TipoIdenService();
        List<TipoIden> lstTipoIden = new List<TipoIden>(1);

        try
        {
            lstTipoIden = IdenService.ListarTipoIden(new TipoIden(), (Usuario)Session["usuario"]);
        }
        catch (Exception ex)
        {
            lblerror.Text = ("LlenarDDLTipoIdentificacion" + ex.Message);
            return;
        }
        if (lstTipoIden.Count == 0)
        {
            lstTipoIden.Add(new TipoIden());
        }
        // Lleno ddlIdentificacion de la View de Registro de Activos
        ddlModalIdentificacion.DataSource = lstTipoIden;
        ddlModalIdentificacion.DataTextField = "descripcion";
        ddlModalIdentificacion.DataValueField = "codtipoidentificacion";
        ddlModalIdentificacion.DataBind();
    }
    protected void LineaAporteObliga()
    {
        string script = "";
        LineaAporte inflineaaport = new LineaAporte();
        inflineaaport = _afiliacionServicio.ConsultarLineaObligatoria(Usuario);
        if (inflineaaport.cod_linea_aporte != 0 && (inflineaaport.tipo_cuota == 4 || inflineaaport.tipo_cuota == 5))
        {
            txtCodLinApor.Text = inflineaaport.cod_linea_aporte.ToString();
            txtTipCuoApor.Text = inflineaaport.tipo_cuota.ToString();
            //script = "document.getElementById(" + "txtsueldo_soli" + ").className =document.getElementById(" + "txtsueldo_soli" + ").className.replace( '' , 'required ' );";
        }
        else
        {
            // script = "document.getElementById('"+"txtsueldo_soli"+"').className =document.getElementById('"+"txtsueldo_soli"+"').className.replace( 'required' , '' );";
        }
        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "ToggleScript3", script, true);

    }
    #endregion

    private bool almacenarDatos()
    {
        Usuario pUsuario = (Usuario)Session["usuario"];
        Persona1 Entidad = new Persona1();

        #region GRABADO DE INFORMACIÓN ECONÓMICA 
        // RegistrarPostBack();
        EstadosFinancieros InformacionFinanciera = new EstadosFinancieros();
        if (txtsueldo_soli.Text != null && txtsueldo_soli.Text != "")
            InformacionFinanciera.sueldo = Convert.ToDecimal(txtsueldo_soli.Text);
        if (txtsueldo_cony.Text != null && txtsueldo_cony.Text != "")
            InformacionFinanciera.sueldoconyuge = Convert.ToDecimal(txtsueldo_cony.Text);
        if (txthonorario_soli.Text != null && txthonorario_soli.Text != "")
            InformacionFinanciera.honorarios = Convert.ToDecimal(txthonorario_soli.Text);
        if (txthonorario_cony.Text != null && txthonorario_cony.Text != "")
            InformacionFinanciera.honorariosconyuge = Convert.ToDecimal(txthonorario_cony.Text);
        if (txtarrenda_soli.Text != null && txtarrenda_soli.Text != "")
            InformacionFinanciera.arrendamientos = Convert.ToDecimal(txtarrenda_soli.Text);
        if (txtarrenda_cony.Text != null && txtarrenda_cony.Text != "")
            InformacionFinanciera.arrendamientosconyuge = Convert.ToDecimal(txtarrenda_cony.Text);
        if (txtotrosIng_soli.Text != null && txtotrosIng_soli.Text != "")
            InformacionFinanciera.otrosingresos = Convert.ToDecimal(txtotrosIng_soli.Text);
        if (txtotrosIng_cony.Text != null && txtotrosIng_cony.Text != "")
            InformacionFinanciera.otrosingresosconyuge = Convert.ToDecimal(txtotrosIng_cony.Text);
        if (hdtotalING_soli.Value != null && hdtotalING_soli.Value != "")
            InformacionFinanciera.totalingreso = Convert.ToDecimal(hdtotalING_soli.Value);
        if (hdtotalING_cony.Value != null && hdtotalING_cony.Value != "")
            InformacionFinanciera.totalingresoconyuge = Convert.ToDecimal(hdtotalING_cony.Value);
        if (txthipoteca_soli.Text != null && txthipoteca_soli.Text != "")
            InformacionFinanciera.hipoteca = Convert.ToDecimal(txthipoteca_soli.Text);
        if (txthipoteca_cony.Text != null && txthipoteca_cony.Text != "")
            InformacionFinanciera.hipotecaconyuge = Convert.ToDecimal(txthipoteca_cony.Text);
        if (txttarjeta_soli.Text != null && txttarjeta_soli.Text != "")
            InformacionFinanciera.targeta_credito = Convert.ToDecimal(txttarjeta_soli.Text);
        if (txttarjeta_cony.Text != null && txttarjeta_cony.Text != "")
            InformacionFinanciera.targeta_creditoconyuge = Convert.ToDecimal(txttarjeta_cony.Text);
        if (txtotrosPres_soli.Text != null && txtotrosPres_soli.Text != "")
            InformacionFinanciera.otrosprestamos = Convert.ToDecimal(txtotrosPres_soli.Text);
        if (txtotrosPres_cony.Text != null && txtotrosPres_cony.Text != "")
            InformacionFinanciera.otrosprestamosconyuge = Convert.ToDecimal(txtotrosPres_cony.Text);
        if (txtgastosFam_soli.Text != null && txtgastosFam_soli.Text != "")
            InformacionFinanciera.gastofamiliar = Convert.ToDecimal(txtgastosFam_soli.Text);
        if (txtgastosFam_cony.Text != null && txtgastosFam_cony.Text != "")
            InformacionFinanciera.gastofamiliarconyuge = Convert.ToDecimal(txtgastosFam_cony.Text);
        if (txtnomina_soli.Text == "")
            InformacionFinanciera.decunomina = 0;
        else
            InformacionFinanciera.decunomina = Convert.ToDecimal(txtnomina_soli.Text);
        if (txtnomina_cony.Text != null && txtnomina_cony.Text != "")
            InformacionFinanciera.decunominaconyuge = Convert.ToDecimal(txtnomina_cony.Text);
        if (hdtotalEGR_soli.Value != null && hdtotalEGR_soli.Value != "")
            InformacionFinanciera.totalegresos = Convert.ToDecimal(hdtotalEGR_soli.Value);
        if (hdtotalEGR_cony.Value != null && hdtotalEGR_cony.Value != "")
            InformacionFinanciera.totalegresosconyuge = Convert.ToDecimal(hdtotalEGR_cony.Value);
        //Agregado para guardar información de activos, pasivos y patrimonio
        if (txtactivos_soli.Text != null && txtactivos_soli.Text != "")
            InformacionFinanciera.TotAct = Convert.ToInt64(txtactivos_soli.Text);
        if (txtactivos_conyuge.Text != null && txtactivos_conyuge.Text != "")
            InformacionFinanciera.TotActConyuge = Convert.ToInt64(txtactivos_conyuge.Text);
        if (txtpasivos_soli.Text != null && txtpasivos_soli.Text != "")
            InformacionFinanciera.TotPas = Convert.ToInt64(txtpasivos_soli.Text);
        if (txtpasivos_conyuge.Text != null && txtpasivos_conyuge.Text != "")
            InformacionFinanciera.TotPasConyuge = Convert.ToInt64(txtpasivos_conyuge.Text);
        if (txtpatrimonio_soli.Text != null && txtpatrimonio_soli.Text != "")
            InformacionFinanciera.TotPat = Convert.ToInt64(txtpatrimonio_soli.Text);
        if (txtpatrimonio_conyuge.Text != null && txtpatrimonio_conyuge.Text != "")
            InformacionFinanciera.TotPatConyuge = Convert.ToInt64(txtpatrimonio_conyuge.Text);

        InformacionFinanciera.conceptootros = txtConceptoOtros_soli.Text;
        InformacionFinanciera.conceptootrosconyuge = txtConceptoOtros_cony.Text;

        if (Session[Usuario.codusuario + "cod_per"] != null)
            InformacionFinanciera.cod_persona = Convert.ToInt64(Session[Usuario.codusuario + "cod_per"].ToString());

        if (Session[pUsuario + "Cod_persona_conyuge"] != null)
            InformacionFinanciera.cod_personaconyuge = Convert.ToInt64(Session[pUsuario + "Cod_persona_conyuge"].ToString());

        if (InformacionFinanciera.cod_persona != 0)
        {
            EstadosFinancierosServicio.guardarIngreEgre(InformacionFinanciera, (Usuario)Session["Usuario"]);
        }
        else
        {
            lblerror.Text = ("No fue posible guardar la información Economica");
            return false;
        }
        #endregion

        //Listado de Cuentas
        Entidad.lstCuentasBan = new List<CuentasBancarias>();
        Entidad.lstCuentasBan = ObtenerListaCuentaBanc();

        //Listado de Moneda Extranjera
        Entidad.lstMonedaExt = new List<EstadosFinancieros>();
        Entidad.lstMonedaExt = ObtenerListaMonedaExt();

        //Listado de productos financieros
        Entidad.lstProductosFinExt = new List<EstadosFinancieros>();
        Entidad.lstProductosFinExt = ObtenerListaProductosExt();


        if (Session[Usuario.codusuario + "cod_per"] != null)
        {

            Entidad.fecultmod = Convert.ToDateTime(DateTime.Now.ToString(GlobalWeb.gFormatoFecha));
            Entidad.usuultmod = pUsuario.nombre;
            try
            {
                Entidad.cod_persona = Convert.ToInt64(Session[Usuario.codusuario + "cod_per"].ToString());
                Persona1 pResult = ServicePersona.TabEconomica(Entidad, pUsuario);
                if (pResult != null)
                {
                    // RECARGANDO GRIDVIEW
                    if (pResult.lstCuentasBan != null)
                    {
                        if (pResult.lstCuentasBan.Count > 0)
                        {
                            gvCuentasBancarias.DataSource = pResult.lstCuentasBan;
                            gvCuentasBancarias.DataBind();
                        }
                    }

                    if (pResult.lstMonedaExt != null)
                    {
                        if (pResult.lstMonedaExt.Count > 0)
                        {
                            gvMonedaExtranjera.DataSource = pResult.lstMonedaExt;
                            gvMonedaExtranjera.DataBind();
                        }
                    }

                    if (pResult.lstProductosFinExt != null)
                    {
                        if (pResult.lstProductosFinExt.Count > 0)
                        {
                            gvProductosExterior.DataSource = pResult.lstProductosFinExt;
                            gvProductosExterior.DataBind();
                        }
                    }
                }
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Guardo", "alert('Guardo la Información Económica.')", true);
                return true;
            }
            catch (Exception ex)
            {
                lblerror.Text = ("Error al modificar los datos de la información Económica. " + ex.Message);
                return false;
            }
        }
        else
        {
            lblerror.Text = ("No se pudo realizar el registro o actualizacion de la información Económica. ");
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Error2", "alert('No se pudo realizar el registro o actualizacion de la información Económica.')", true);
            return false;
        }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        /*******QUITAR VARIABLES DE SESION DE JHOJA DE RUTA*************/
        ImagenesService imagenService = new ImagenesService();
        Session.Remove(imagenService.CodigoPrograma.ToString() + ".cod_persona");
        Session.Remove(_afiliacionServicio.CodigoPrograma + "last");
        Session.Remove(_afiliacionServicio.CodigoPrograma + "next");
        Session.Remove(_afiliacionServicio.CodigoPrograma + ".id");
        Session.Remove(_afiliacionServicio.CodigoPrograma + ".modificar");
        Session.Remove("lstParametros");

        EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
        if (Session["ocupacion"] != null) { Session.Remove("ocupacion"); }
        if (Session[serviceEstadoCuenta.CodigoPrograma + ".id"] != null)
        {
            Session.Remove(serviceEstadoCuenta.CodigoPrograma + ".id");
            Session.Remove("Persona");
            Session.Remove(Usuario.codusuario + "cod_per");
            Navegar("../../../Asesores/EstadoCuenta/Detalle.aspx");
        }
        else
        {
            Session.Remove("Persona");
            Session.Remove(Usuario.codusuario + "cod_per");
            Navegar("../../Afiliaciones/Lista.aspx");
        }

    }

}