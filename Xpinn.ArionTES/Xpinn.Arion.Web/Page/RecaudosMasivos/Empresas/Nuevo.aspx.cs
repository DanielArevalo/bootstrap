using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Nuevo : GlobalWeb
{

    EmpresaRecaudoServices recaudoEmpresaRecaudo = new EmpresaRecaudoServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(recaudoEmpresaRecaudo.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(recaudoEmpresaRecaudo.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["Programacion"] = null;
                Session["Concepto"] = null;
                mvAplicar.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;
                if (Session[recaudoEmpresaRecaudo.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[recaudoEmpresaRecaudo.CodigoPrograma + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                    Session["TEXTO"] = "modificar";
                    lblmsj.Text = "modificada";
                }
                else
                {
                    Session["TEXTO"] = "grabar";
                    lblmsj.Text = "grabada";
                    InicializargvProgramacion();
                    InicializargvConcepto();
                    InicializargvEstructura();
                    InicializarEmpresaRecaudo();
                    txtIdentificacion.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(recaudoEmpresaRecaudo.GetType().Name + "L", "Page_Load", ex);
        }

    }

    protected void InicializarEmpresaRecaudo()
    {
        PersonaEmpresaRecaudoServices perempresaServicio = new PersonaEmpresaRecaudoServices();
        List<PersonaEmpresaRecaudo> lstEmpresaRecaudo = new List<PersonaEmpresaRecaudo>();
        lstEmpresaRecaudo = perempresaServicio.ListarEmpresaRecaudo(false, (Usuario)Session["Usuario"]);
        gvEmpresaExcluyente.DataSource = lstEmpresaRecaudo;
        gvEmpresaExcluyente.DataBind();
        Session["EmpresaRecaudo"] = lstEmpresaRecaudo;
    }


    protected void InicializargvProgramacion()
    {
        List<EmpresaRecaudo_Programacion> lstProgra = new List<EmpresaRecaudo_Programacion>();
        for (int i = gvProgramacion.Rows.Count; i < 5; i++)
        {
            EmpresaRecaudo_Programacion eCuenta = new EmpresaRecaudo_Programacion();
            eCuenta.idprogramacion = -1;
            eCuenta.tipo_lista = null;
            eCuenta.cod_periodicidad = null;
            eCuenta.fecha_inicio = null;
            eCuenta.principal = null;

            lstProgra.Add(eCuenta);
        }
        gvProgramacion.DataSource = lstProgra;
        gvProgramacion.DataBind();

        Session["Programacion"] = lstProgra;
    }

    protected void InicializargvConcepto()
    {
        List<EMPRESARECAUDO_CONCEPTO> lstConcepto = new List<EMPRESARECAUDO_CONCEPTO>();
        for (int i = gvConcepto.Rows.Count; i < 5; i++)
        {
            EMPRESARECAUDO_CONCEPTO eCuenta = new EMPRESARECAUDO_CONCEPTO();
            eCuenta.idempconcepto = -1;
            eCuenta.tipo_producto = null;
            eCuenta.cod_linea = "";
            eCuenta.cod_concepto = "";
            lstConcepto.Add(eCuenta);
        }
        gvConcepto.DataSource = lstConcepto;
        gvConcepto.DataBind();

        Session["Concepto"] = lstConcepto;
    }

    protected void gvProgramacion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlTipo = (DropDownListGrid)e.Row.FindControl("ddlTipo");
            if (ddlTipo != null) PoblarLista("tipo_lista_recaudo", ddlTipo);

            DropDownListGrid ddlPeriodicidad = (DropDownListGrid)e.Row.FindControl("ddlPeriodicidad");
            if (ddlPeriodicidad != null) PoblarLista("periodicidad", ddlPeriodicidad);

            Label lbltipo = (Label)e.Row.FindControl("lbltipo");
            if (lbltipo != null)
                ddlTipo.SelectedValue = lbltipo.Text;
            Label lblPeriodicidad = (Label)e.Row.FindControl("lblPeriodicidad");
            if (lblPeriodicidad != null)
                ddlPeriodicidad.SelectedValue = lblPeriodicidad.Text;

        }
    }


    protected void gvConcepto_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlTipoProd = (DropDownListGrid)e.Row.FindControl("ddlTipoProd");
            if (ddlTipoProd != null)
            {
                ddlTipoProd.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
                ddlTipoProd.Items.Insert(1, new ListItem("APORTES", "1"));
                ddlTipoProd.Items.Insert(2, new ListItem("CRÉDITOS", "2"));
                ddlTipoProd.Items.Insert(3, new ListItem("DEPÓSITOS", "3"));
                ddlTipoProd.Items.Insert(4, new ListItem("AFILIACION", "6"));
                ddlTipoProd.Items.Insert(5, new ListItem("SERVICIOS", "4"));
                ddlTipoProd.Items.Insert(6, new ListItem("CDAT", "5"));
                ddlTipoProd.Items.Insert(7, new ListItem("AHORRO PROGRAMADO", "9"));
                ddlTipoProd.Items.Insert(8, new ListItem("CRÉDITOS-CUOTAS EXTRAS", "10"));
                ddlTipoProd.Items.Insert(9, new ListItem("INT.AHORRO PERMANENTE", "11"));
                ddlTipoProd.SelectedValue = "0";
                ddlTipoProd.DataBind();
            }


            Label lbltipoProd = (Label)e.Row.FindControl("lbltipoProd");
            if (lbltipoProd != null)
            {
                ddlTipoProd.SelectedValue = lbltipoProd.Text;
            }

            DropDownListGrid ddlLineaProd = (DropDownListGrid)e.Row.FindControl("ddlLineaProd");
            if (ddlLineaProd == null)
            {
                VerError("No pudo encontrar el control");
                return;
            }
            Label lblLineaProd = (Label)e.Row.FindControl("lblLineaProd");
            if (lblLineaProd != null)
            {
                if (ddlTipoProd.SelectedValue == "1")
                {
                    PoblarLista("lineaaporte", ddlLineaProd);
                    ddlLineaProd.SelectedValue = lblLineaProd.Text;
                }
                else if (ddlTipoProd.SelectedValue == "2")
                {
                    PoblarLista("lineascredito", ddlLineaProd);
                    ddlLineaProd.SelectedValue = lblLineaProd.Text;
                }
                else if (ddlTipoProd.SelectedValue == "3")
                {
                    PoblarLista("lineaahorro", ddlLineaProd);
                    ddlLineaProd.SelectedValue = lblLineaProd.Text;
                }
                else if (ddlTipoProd.SelectedValue == "0")
                {
                    ddlLineaProd.DataSource = null;
                    ddlLineaProd.DataBind();
                }
                else if (ddlTipoProd.SelectedValue == "4")
                {
                    PoblarLista("LINEASSERVICIOS", ddlLineaProd);
                    ddlLineaProd.SelectedValue = lblLineaProd.Text;
                }
                else if (ddlTipoProd.SelectedValue == "5")
                {
                    PoblarLista("lineacdat", ddlLineaProd);
                    ddlLineaProd.SelectedValue = lblLineaProd.Text;
                }
                else if (ddlTipoProd.SelectedValue == "9")
                {
                    PoblarLista("lineaprogramado", ddlLineaProd);
                    ddlLineaProd.SelectedValue = lblLineaProd.Text;
                }
                else if (ddlTipoProd.SelectedValue == "10")
                {
                    PoblarLista("lineascredito", ddlLineaProd);
                    ddlLineaProd.SelectedValue = lblLineaProd.Text;
                }
            }
        }
    }

    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        VerError("");
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        try
        {
            plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        }
        catch (Exception ex)
        {
            VerError("Error:" + ex.Message + "-" + pentidad + "-" + pTabla + "-");
        }
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
            EmpresaRecaudo vRecaudos = new EmpresaRecaudo();
            vRecaudos.cod_empresa = Convert.ToInt32(pIdObjeto);
            vRecaudos = recaudoEmpresaRecaudo.ConsultarEMPRESARECAUDO(vRecaudos, (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vRecaudos.cod_empresa.ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(vRecaudos.cod_empresa.ToString().Trim());
            if (!string.IsNullOrEmpty(vRecaudos.nom_empresa.ToString()))
                txtNombre.Text = HttpUtility.HtmlDecode(vRecaudos.nom_empresa.ToString().Trim());
            if (!string.IsNullOrEmpty(vRecaudos.numero_planilla.ToString()))
                txtNumeroPlanilla.Text = HttpUtility.HtmlDecode(vRecaudos.numero_planilla.ToString().Trim());
            if (!string.IsNullOrEmpty(vRecaudos.estado.ToString()))
                rblEstado.SelectedValue = HttpUtility.HtmlDecode(vRecaudos.estado.ToString().Trim());
            if (vRecaudos.direccion != null)
                txtDirec.Text = HttpUtility.HtmlDecode(vRecaudos.direccion.ToString().Trim());
            if (vRecaudos.telefono != null)
                txtTelefono.Text = HttpUtility.HtmlDecode(vRecaudos.telefono.ToString().Trim());
            if (vRecaudos.responsable != null)
                txtResponsable.Text = HttpUtility.HtmlDecode(vRecaudos.responsable.ToString().Trim());
            if (vRecaudos.tipo_novedad != null)
                rblTipo_nove.SelectedValue = HttpUtility.HtmlDecode(vRecaudos.tipo_novedad.ToString().Trim());
            if (vRecaudos.tipo_recaudo != null)
                rbTipoRecaudo.SelectedValue = HttpUtility.HtmlDecode(vRecaudos.tipo_recaudo.ToString().Trim());
            if (vRecaudos.dias_novedad != 0)
                txtDias_nove.Text = HttpUtility.HtmlDecode(vRecaudos.dias_novedad.ToString().Trim());
            if (!string.IsNullOrEmpty(vRecaudos.fecha_convenio.ToString()))
                txtFecha.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vRecaudos.fecha_convenio.ToString()));
            if (!string.IsNullOrEmpty(vRecaudos.aplica_novedades.ToString()))
                cbAplicar.Checked = Convert.ToInt32(HttpUtility.HtmlDecode(vRecaudos.aplica_novedades.ToString())) == 1 ? true : false;
            if (!string.IsNullOrEmpty(vRecaudos.deshabilitar_desc_inhabiles.ToString()))
                cbDeshabilitarDesc.Checked = vRecaudos.deshabilitar_desc_inhabiles;
            if (!string.IsNullOrEmpty(vRecaudos.descuento_retiro.ToString()))
                chkDescuentosRetirados.Checked = Convert.ToBoolean(vRecaudos.descuento_retiro);
            if (!string.IsNullOrEmpty(vRecaudos.mantener_condicion_inicial.ToString()))
                chkCondicionInicial.Checked = Convert.ToBoolean(vRecaudos.mantener_condicion_inicial);
            if (!string.IsNullOrEmpty(vRecaudos.mayores_valores.ToString()))
                ddlMayoresValores.SelectedValue = HttpUtility.HtmlDecode(vRecaudos.mayores_valores.ToString());
            if (!string.IsNullOrEmpty(vRecaudos.aplica_refinanciados.ToString()))
                cbAplicarRefinancidos.Checked = Convert.ToInt32(HttpUtility.HtmlDecode(vRecaudos.aplica_refinanciados.ToString())) == 1 ? true : false;
            if (!string.IsNullOrEmpty(vRecaudos.forma_cobro.ToString()))
                ddlFormaCobro.SelectedValue = HttpUtility.HtmlDecode(vRecaudos.forma_cobro.ToString());
            if (!string.IsNullOrEmpty(vRecaudos.aplica_mora.ToString()))
                chkMora.Checked = Convert.ToBoolean(vRecaudos.aplica_mora);
            if (vRecaudos.cod_cuenta != null)
                if (!string.IsNullOrEmpty(vRecaudos.cod_cuenta.ToString()))
                    txtCodCuenta.Text = HttpUtility.HtmlDecode(vRecaudos.cod_cuenta.ToString());

            chkAplicarXoficina.Checked = vRecaudos.aplicar_poroficina == 1 ? true : false;
            chkDebitoAutomatico.Checked = vRecaudos.debito_automatico == 1 ? true : false;
            chkDebitoAutomaticoSem.Checked = vRecaudos.debito_automatico_sem == 1 ? true : false;

            if (!string.IsNullOrEmpty(vRecaudos.cod_persona.ToString()))
                txtCodEmpresa.Text = HttpUtility.HtmlDecode(vRecaudos.cod_persona.ToString());
            if (!string.IsNullOrEmpty(vRecaudos.identificacion.ToString()))
                txtIdentificacion.Text = HttpUtility.HtmlDecode(vRecaudos.identificacion.ToString());

            txtCodigoRecaudoEstructura.Text = vRecaudos.codigo_recaudo_estructura;
            chkManejaAtributos.Checked = Convert.ToBoolean(vRecaudos.maneja_atributos);
            chkAporteVacaciones.Checked = Convert.ToBoolean(vRecaudos.aporte_vacaciones);

            if (!string.IsNullOrEmpty(vRecaudos.descuentos_productos_inact.ToString()))
                cbProductosInactivos.Checked = vRecaudos.descuentos_productos_inact;


            //RECUPERAR DATOS - GRILLA PROGRAMACION-CONCEPTO
            List <EmpresaRecaudo_Programacion> LstProgramacion = new List<EmpresaRecaudo_Programacion>();
            EmpresaRecaudo_Programacion pProgra = new EmpresaRecaudo_Programacion();
            pProgra.cod_empresa = vRecaudos.cod_empresa;
            LstProgramacion = recaudoEmpresaRecaudo.ListarEMPRESAPROGRAMACION(pProgra, (Usuario)Session["usuario"]);
            if (LstProgramacion.Count > 0)
            {
                if ((LstProgramacion != null) || (LstProgramacion.Count != 0))
                {
                    gvProgramacion.DataSource = LstProgramacion;
                    gvProgramacion.DataBind();
                }
                Session["Programacion"] = LstProgramacion;
            }
            else
            {
                InicializargvProgramacion();
            }

            //RECUPERAR DATOS - GRILLA CONCEPTO
            List<EMPRESARECAUDO_CONCEPTO> LstConcepto = new List<EMPRESARECAUDO_CONCEPTO>();
            EMPRESARECAUDO_CONCEPTO pConcep = new EMPRESARECAUDO_CONCEPTO();
            pConcep.cod_empresa = vRecaudos.cod_empresa;
            LstConcepto = recaudoEmpresaRecaudo.ListarEMPRESACONCEPTO(pConcep, (Usuario)Session["usuario"]);
            if (LstConcepto.Count > 0)
            {
                if ((LstConcepto != null) || (LstConcepto.Count != 0))
                {
                    gvConcepto.DataSource = LstConcepto;
                    gvConcepto.DataBind();
                }
                Session["Concepto"] = LstConcepto;
            }
            else
            {
                InicializargvConcepto();
            }

            //RECUPERAR DATOS - GRILLA ESTRUCTURAS
            List<EmpresaEstructuraCarga> LstEstructura = new List<EmpresaEstructuraCarga>();
            EmpresaEstructuraCarga pEstructura = new EmpresaEstructuraCarga();
            pEstructura.cod_empresa = vRecaudos.cod_empresa;
            Xpinn.Tesoreria.Services.EmpresaEstructuraCargaServices estructuraEmpresaRecaudo = new EmpresaEstructuraCargaServices();
            LstEstructura = estructuraEmpresaRecaudo.ListarEmpresaEstructuraCarga(pEstructura, (Usuario)Session["usuario"]);
            if (LstEstructura.Count > 0)
            {
                if ((LstEstructura != null) || (LstEstructura.Count != 0))
                {
                    gvEstructura.DataSource = LstEstructura;
                    gvEstructura.DataBind();
                }
                Session["Estructura"] = LstEstructura;
            }
            else
            {
                InicializargvEstructura();
            }

            List<EmpresaExcluyente> lstExcluyente = new List<EmpresaExcluyente>();
            EmpresaExcluyenteServices EmpresaExclu = new EmpresaExcluyenteServices();
            lstExcluyente = EmpresaExclu.ListarEmpresaExcluyente(Convert.ToInt32(txtCodigo.Text), (Usuario)Session["usuario"]);
            if (lstExcluyente.Count > 0)
            {
                gvEmpresaExcluyente.DataSource = lstExcluyente;
                gvEmpresaExcluyente.DataBind();
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(recaudoEmpresaRecaudo.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void btnFinal_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar("~/Page/RecaudosMasivos/Empresas/Lista.aspx");
    }

    public Boolean ValidarDatos()
    {
        List<EmpresaRecaudo_Programacion> LstProgramacion = new List<EmpresaRecaudo_Programacion>();
        if (Session["Programacion"] != null)
        {
            LstProgramacion = (List<EmpresaRecaudo_Programacion>)Session["Programacion"];
        }
        if (rblEstado.SelectedItem == null)
        {
            VerError("Seleccione un Estado");
            return false;
        }
        if (rblTipo_nove.SelectedItem == null)
        {
            VerError("Seleccione una Opción de tipo de Novedad");
            return false;
        }

        for (int i = 0; i < LstProgramacion.Count; i++)
        {
            try
            {

            }
            catch (Exception ex)
            {
                VerError(ex.Message);
                return false;
            }
        }
        if(txtIdentificacion.Text == "")
        {
            VerError("Ingrese la identificación de la pagaduria");
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            ctlMensaje.MostrarMensaje("Esta Seguro de " + Session["TEXTO"].ToString() + " los Datos Ingresados?");
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            EmpresaRecaudo eEmpre = new EmpresaRecaudo();
            if (txtCodigo.Text != "")
                eEmpre.cod_empresa = Convert.ToInt64(txtCodigo.Text);
            else
                eEmpre.cod_empresa = 0;
            eEmpre.nom_empresa = txtNombre.Text;
            if (txtNumeroPlanilla.Text != "")
                eEmpre.numero_planilla = Convert.ToInt32(txtNumeroPlanilla.Text);
            else
                eEmpre.numero_planilla = null;
            eEmpre.estado = Convert.ToInt32(rblEstado.SelectedValue);
            if (txtDirec.Text != "")
                eEmpre.direccion = txtDirec.Text;
            else
                eEmpre.direccion = null;
            if (txtTelefono.Text != "")
                eEmpre.telefono = txtTelefono.Text;
            else
                eEmpre.telefono = null;
            if (txtResponsable.Text != "")
                eEmpre.responsable = txtResponsable.Text;
            else
                eEmpre.responsable = null;
            eEmpre.tipo_novedad = Convert.ToInt32(rblTipo_nove.SelectedValue);
            if (rbTipoRecaudo.SelectedValue != null)
                if (rbTipoRecaudo.SelectedValue != "")
                    eEmpre.tipo_recaudo = Convert.ToInt32(rbTipoRecaudo.SelectedValue);
            if (txtDias_nove.Text != "")
                eEmpre.dias_novedad = Convert.ToInt32(txtDias_nove.Text);
            else
                eEmpre.dias_novedad = 0;
            if (DateTime.Parse(txtFecha.ToDate) != DateTime.MinValue)
                eEmpre.fecha_convenio = Convert.ToDateTime(txtFecha.Text);
            else
                eEmpre.fecha_convenio = DateTime.MinValue;
            if (cbAplicar.Checked == true)
                eEmpre.aplica_novedades = 1;
            else
                eEmpre.aplica_novedades = 0;

            eEmpre.deshabilitar_desc_inhabiles = cbDeshabilitarDesc.Checked;

            if (ddlMayoresValores.SelectedValue != null)
                if (ddlMayoresValores.SelectedValue != "")
                    eEmpre.mayores_valores = Convert.ToInt32(ddlMayoresValores.SelectedValue);
            if (cbAplicarRefinancidos.Checked == true)
                eEmpre.aplica_refinanciados = 1;
            else
                eEmpre.aplica_refinanciados = 0;
            if (ddlFormaCobro.SelectedValue != null)
                if (ddlFormaCobro.SelectedValue != "")
                    eEmpre.forma_cobro = Convert.ToInt32(ddlFormaCobro.SelectedValue);

            eEmpre.codigo_recaudo_estructura = txtCodigoRecaudoEstructura.Text;

            eEmpre.aplicar_poroficina = chkAplicarXoficina.Checked ? 1 : 0;

            eEmpre.debito_automatico = chkDebitoAutomatico.Checked ? 1 : 0;

            eEmpre.debito_automatico_sem = chkDebitoAutomaticoSem.Checked ? 1 : 0;


            eEmpre.maneja_atributos = chkManejaAtributos.Checked ? 1 : 0;
            eEmpre.descuento_retiro = chkDescuentosRetirados.Checked ? 1 : 0;
            eEmpre.mantener_condicion_inicial = chkCondicionInicial.Checked ? 1 : 0;
            eEmpre.aplica_mora = chkMora.Checked ? 1 : 0;
            eEmpre.aporte_vacaciones = chkAporteVacaciones.Checked ? 1 : 0; 

            eEmpre.lstProgramacion = new List<EmpresaRecaudo_Programacion>();
            eEmpre.lstProgramacion = ObtenerListaProgramacion();

            eEmpre.lstConcepto = new List<EMPRESARECAUDO_CONCEPTO>();
            eEmpre.lstConcepto = ObtenerListaConcepto();

            eEmpre.lstEstructura = new List<EmpresaEstructuraCarga>();
            eEmpre.lstEstructura = ObtenerListaEstructura();

            eEmpre.lstExcluyente = new List<EmpresaExcluyente>();
            eEmpre.lstExcluyente = ObtenerListaEmpresaExcluyente();

            eEmpre.cod_persona = Convert.ToInt64(!string.IsNullOrEmpty(txtCodEmpresa.Text) ? txtCodEmpresa.Text : "0");


            eEmpre.descuentos_productos_inact = cbProductosInactivos.Checked;



            if (idObjeto != "")
            {
                //MODIFICAR
                recaudoEmpresaRecaudo.ModificarEmpresaRecaudo(eEmpre, (Usuario)Session["usuario"]);
            }
            else
            {
                //CREAR
                recaudoEmpresaRecaudo.CrearEmpresaRecaudo(eEmpre, (Usuario)Session["usuario"]);
            }
            Session[recaudoEmpresaRecaudo.CodigoPrograma + ".id"] = idObjeto;

            mvAplicar.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(recaudoEmpresaRecaudo.CodigoPrograma, "btnContinuar_Click", ex);
        }

    }


    protected List<EmpresaExcluyente> ObtenerListaEmpresaExcluyente()
    {
        try
        {
            List<EmpresaExcluyente> lstEmpresa = new List<EmpresaExcluyente>();

            foreach (GridViewRow rFila in gvEmpresaExcluyente.Rows)
            {
                EmpresaExcluyente eEmpresa = new EmpresaExcluyente();

                CheckBoxGrid chkSeleccione = (CheckBoxGrid)rFila.FindControl("chkSeleccione");
                if (chkSeleccione != null)
                    if (chkSeleccione.Checked)
                    {
                        Label lblcodempresa = (Label)rFila.FindControl("lblcodempresa");
                        if (lblcodempresa.Text != "")
                            eEmpresa.cod_empresa_excluye = Convert.ToInt32(lblcodempresa.Text);
                    }

                if (chkSeleccione.Checked)
                {
                    lstEmpresa.Add(eEmpresa);
                }
            }
            return lstEmpresa;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(recaudoEmpresaRecaudo.CodigoPrograma, "ObtenerListaEmpresaExcluyente", ex);
            return null;
        }
    }


    protected List<EmpresaRecaudo_Programacion> ObtenerListaProgramacion()
    {
        try
        {
            List<EmpresaRecaudo_Programacion> lstProgramacion = new List<EmpresaRecaudo_Programacion>();
            //lista para adicionar filas sin perder datos
            List<EmpresaRecaudo_Programacion> lista = new List<EmpresaRecaudo_Programacion>();

            foreach (GridViewRow rfila in gvProgramacion.Rows)
            {
                EmpresaRecaudo_Programacion ePogra = new EmpresaRecaudo_Programacion();

                Label idprogramacion = (Label)rfila.FindControl("idprogramacion");
                if (idprogramacion != null)
                    ePogra.idprogramacion = Convert.ToInt64(idprogramacion.Text);
                else
                    ePogra.idprogramacion = -1;

                DropDownListGrid ddlTipo = (DropDownListGrid)rfila.FindControl("ddlTipo");
                if (ddlTipo.SelectedValue != null)
                    if (ddlTipo.SelectedValue != "")
                        ePogra.tipo_lista = Convert.ToInt32(ddlTipo.SelectedValue);

                CheckBoxGrid chkPrincipal = (CheckBoxGrid)rfila.FindControl("chkPrincipal");
                if (chkPrincipal != null)
                    if (chkPrincipal.Checked)
                        ePogra.principal = 1;
                    else
                        ePogra.principal = 0;

                DropDownListGrid ddlPeriodicidad = (DropDownListGrid)rfila.FindControl("ddlPeriodicidad");
                if (ddlPeriodicidad.SelectedValue != null)
                    if (ddlPeriodicidad.SelectedValue != "")
                        ePogra.cod_periodicidad = Convert.ToInt32(ddlPeriodicidad.SelectedValue);

                fecha txtfechainicio = (fecha)rfila.FindControl("txtfechainicio");
                if (txtfechainicio != null)
                    if (txtfechainicio.Text != "")
                        ePogra.fecha_inicio = txtfechainicio.ToDateTime;

                lista.Add(ePogra);
                Session["Programacion"] = lista;

                if (ePogra.tipo_lista != null && ePogra.cod_periodicidad != null && ePogra.fecha_inicio != DateTime.MinValue)
                {
                    lstProgramacion.Add(ePogra);
                }
            }
            return lstProgramacion;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(recaudoEmpresaRecaudo.CodigoPrograma, "ObtenerListaProgramacion", ex);
            return null;
        }
    }


    protected List<EMPRESARECAUDO_CONCEPTO> ObtenerListaConcepto()//Int64 cod
    {
        List<EMPRESARECAUDO_CONCEPTO> lstConcepto = new List<EMPRESARECAUDO_CONCEPTO>();
        //lista para adicionar filas sin perder datos
        List<EMPRESARECAUDO_CONCEPTO> lista = new List<EMPRESARECAUDO_CONCEPTO>();

        foreach (GridViewRow rfila in gvConcepto.Rows)
        {
            EMPRESARECAUDO_CONCEPTO eConcep = new EMPRESARECAUDO_CONCEPTO();

            Label idempconcepto = (Label)rfila.FindControl("idempconcepto");
            if (idempconcepto != null)
                eConcep.idempconcepto = Convert.ToInt64(idempconcepto.Text);
            else
                eConcep.idempconcepto = -1;

            DropDownListGrid ddlTipoProd = (DropDownListGrid)rfila.FindControl("ddlTipoProd");
            if (ddlTipoProd.SelectedValue != null)
                if (ddlTipoProd.SelectedValue != "")
                    eConcep.tipo_producto = Convert.ToInt32(ddlTipoProd.SelectedValue);

            DropDownListGrid ddlLineaProd = (DropDownListGrid)rfila.FindControl("ddlLineaProd");
            if (ddlLineaProd.SelectedValue != null)
                if (ddlLineaProd.SelectedValue != "")
                    eConcep.cod_linea = ddlLineaProd.SelectedValue;

            TextBox txtConcepto = (TextBox)rfila.FindControl("txtConcepto");
            if (txtConcepto != null)
                eConcep.cod_concepto = txtConcepto.Text;

            TextBox txtPrioridad = (TextBox)rfila.FindControl("txtPrioridad");
            if (txtPrioridad != null)
                if (txtPrioridad.Text != "")
                    eConcep.prioridad = Convert.ToInt32(txtPrioridad.Text);

            lista.Add(eConcep);
            Session["Concepto"] = lista;

            if (eConcep.tipo_producto.Value != 0 && eConcep.cod_linea != "" && eConcep.cod_concepto != "")
            {
                lstConcepto.Add(eConcep);
            }
        }
        return lstConcepto;
    }




    protected void gvProgramacion_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void gvConcepto_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlTipoProd_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListGrid ddlTipoProd = (DropDownListGrid)sender;
        int nItem = Convert.ToInt32(ddlTipoProd.CommandArgument);

        DropDownListGrid ddlLineaProd = (DropDownListGrid)gvConcepto.Rows[nItem].FindControl("ddlLineaProd");

        if (ddlLineaProd != null)
        {
            if (ddlTipoProd.SelectedValue == "1")
                PoblarLista("lineaaporte", ddlLineaProd);
            else if (ddlTipoProd.SelectedValue == "2")
                PoblarLista("lineascredito", ddlLineaProd);
            else if (ddlTipoProd.SelectedValue == "3")
                PoblarLista("lineaahorro", ddlLineaProd);
            else if (ddlTipoProd.SelectedValue == "0")
            {
                ddlLineaProd.DataSource = null;
                ddlLineaProd.DataBind();
            }
            else if (ddlTipoProd.SelectedValue == "4")
            {
                PoblarLista("LINEASSERVICIOS", ddlLineaProd);
            }
            else if (ddlTipoProd.SelectedValue == "5")
            {
                PoblarLista("lineacdat", ddlLineaProd);
            }
            else if (ddlTipoProd.SelectedValue == "9")
            {
                PoblarLista("lineaprogramado", ddlLineaProd);
            }
            else if (ddlTipoProd.SelectedValue == "10")
                PoblarLista("lineascredito", ddlLineaProd);
            else
            {
                ddlLineaProd.DataSource = null;
                ddlLineaProd.DataBind();
            }
        }
    }

    protected void gvConcepto_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvConcepto.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaConcepto();

        List<EMPRESARECAUDO_CONCEPTO> LstDetalle = new List<EMPRESARECAUDO_CONCEPTO>();
        LstDetalle = (List<EMPRESARECAUDO_CONCEPTO>)Session["Concepto"];
        if (conseID > 0)
        {
            try
            {
                foreach (EMPRESARECAUDO_CONCEPTO acti in LstDetalle)
                {
                    if (acti.idempconcepto == conseID)
                    {
                        recaudoEmpresaRecaudo.EliminarEmpresaConcepto(conseID, (Usuario)Session["usuario"]);
                        LstDetalle.Remove(acti);
                        break;
                    }
                }
                Session["Concepto"] = LstDetalle;

                gvConcepto.DataSourceID = null;
                gvConcepto.DataBind();
                gvConcepto.DataSource = LstDetalle;
                gvConcepto.DataBind();


            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(recaudoEmpresaRecaudo.CodigoPrograma, "gvProgramacion_RowDeleting", ex);
            }
        }
        else
        {
            foreach (EMPRESARECAUDO_CONCEPTO acti in LstDetalle)
            {
                if (acti.idempconcepto == conseID)
                {
                    LstDetalle.Remove(acti);
                    break;
                }
            }
            Session["Concepto"] = LstDetalle;

            gvConcepto.DataSourceID = null;
            gvConcepto.DataBind();
            gvConcepto.DataSource = LstDetalle;
            gvConcepto.DataBind();
        }
    }

    protected void gvProgramacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvProgramacion.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaProgramacion();

        List<EmpresaRecaudo_Programacion> LstDetalle = new List<EmpresaRecaudo_Programacion>();
        LstDetalle = (List<EmpresaRecaudo_Programacion>)Session["Programacion"];
        if (conseID > 0)
        {
            try
            {
                foreach (EmpresaRecaudo_Programacion acti in LstDetalle)
                {
                    if (acti.idprogramacion == conseID)
                    {
                        recaudoEmpresaRecaudo.EliminarEmpresaPrograma(conseID, (Usuario)Session["usuario"]);
                        LstDetalle.Remove(acti);
                        break;
                    }
                }
                Session["Programacion"] = LstDetalle;

                gvProgramacion.DataSourceID = null;
                gvProgramacion.DataBind();
                gvProgramacion.DataSource = LstDetalle;
                gvProgramacion.DataBind();

            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(recaudoEmpresaRecaudo.CodigoPrograma, "gvProgramacion_RowDeleting", ex);
            }
        }
        else
        {
            foreach (EmpresaRecaudo_Programacion acti in LstDetalle)
            {
                if (acti.idprogramacion == conseID)
                {
                    LstDetalle.Remove(acti);
                    break;
                }
            }
            Session["Programacion"] = LstDetalle;

            gvProgramacion.DataSourceID = null;
            gvProgramacion.DataBind();
            gvProgramacion.DataSource = LstDetalle;
            gvProgramacion.DataBind();
            //e.Cancel = true;
        }
    }


    protected void btnProgramacion_Click(object sender, EventArgs e)
    {
        ObtenerListaProgramacion();
        List<EmpresaRecaudo_Programacion> LstPrograma = new List<EmpresaRecaudo_Programacion>();
        if (Session["Programacion"] != null)
        {
            LstPrograma = (List<EmpresaRecaudo_Programacion>)Session["Programacion"];

            for (int i = 1; i <= 1; i++)
            {
                EmpresaRecaudo_Programacion pDetalle = new EmpresaRecaudo_Programacion();
                pDetalle.idprogramacion = -1;
                pDetalle.cod_empresa = -1;
                pDetalle.tipo_lista = null;
                pDetalle.cod_periodicidad = null;
                pDetalle.fecha_inicio = null;
                pDetalle.principal = null;
                LstPrograma.Add(pDetalle);
            }
            gvProgramacion.PageIndex = gvProgramacion.PageCount;
            gvProgramacion.DataSource = LstPrograma;
            gvProgramacion.DataBind();

            Session["Programacion"] = LstPrograma;
        }

    }

    protected void btnConcepto_Click(object sender, EventArgs e)
    {
        ObtenerListaConcepto();
        List<EMPRESARECAUDO_CONCEPTO> LstConcepto = new List<EMPRESARECAUDO_CONCEPTO>();
        if (Session["Concepto"] != null)
        {
            LstConcepto = (List<EMPRESARECAUDO_CONCEPTO>)Session["Concepto"];

            for (int i = 1; i <= 1; i++)
            {
                EMPRESARECAUDO_CONCEPTO pDetalle = new EMPRESARECAUDO_CONCEPTO();
                pDetalle.idempconcepto = -1;
                pDetalle.cod_empresa = -1;
                pDetalle.tipo_producto = null;
                pDetalle.cod_linea = "";
                pDetalle.cod_concepto = "";
                LstConcepto.Add(pDetalle);
            }
            gvConcepto.PageIndex = gvConcepto.PageCount;
            gvConcepto.DataSource = LstConcepto;
            gvConcepto.DataBind();

            Session["Concepto"] = LstConcepto;
        }
    }

    protected void InicializargvEstructura()
    {
        List<EmpresaEstructuraCarga> lstEstructura = new List<EmpresaEstructuraCarga>();
        for (int i = gvEstructura.Rows.Count; i < 2; i++)
        {
            EmpresaEstructuraCarga eCuenta = new EmpresaEstructuraCarga();
            lstEstructura.Add(eCuenta);
        }
        gvEstructura.DataSource = lstEstructura;
        gvEstructura.DataBind();

        Session["Estructura"] = lstEstructura;
    }


    protected void gvEstructura_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlEstructura = (DropDownListGrid)e.Row.FindControl("ddlEstructura");
            if (ddlEstructura != null)
            {
                PoblarLista("Estructura_Carga", ddlEstructura);
                Label lblcod_estructura_carga = (Label)e.Row.FindControl("lblcod_estructura_carga");
                if (lblcod_estructura_carga != null)
                {
                    ddlEstructura.SelectedValue = lblcod_estructura_carga.Text;
                }
            }
        }
    }

    protected void gvEstructura_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Int64 conseID = Convert.ToInt64(gvEstructura.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaEstructura();

        List<EmpresaEstructuraCarga> LstDetalle = new List<EmpresaEstructuraCarga>();
        LstDetalle = (List<EmpresaEstructuraCarga>)Session["Estructura"];
        if (conseID > 0)
        {
            try
            {
                foreach (EmpresaEstructuraCarga acti in LstDetalle)
                {
                    if (acti.codemparchivo == conseID)
                    {
                        Xpinn.Tesoreria.Services.EmpresaEstructuraCargaServices recaudoEmpresaRecaudo = new Xpinn.Tesoreria.Services.EmpresaEstructuraCargaServices();
                        recaudoEmpresaRecaudo.EliminarEmpresaEstructuraCarga(conseID, (Usuario)Session["usuario"]);
                        LstDetalle.Remove(acti);
                        break;
                    }
                }
                Session["Estructura"] = LstDetalle;

                gvEstructura.DataSourceID = null;
                gvEstructura.DataBind();
                gvEstructura.DataSource = LstDetalle;
                gvEstructura.DataBind();

            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(recaudoEmpresaRecaudo.CodigoPrograma, "gvEstructura_RowDeleting", ex);
            }
        }
        else
        {
            foreach (EmpresaEstructuraCarga acti in LstDetalle)
            {
                if (acti.codemparchivo == conseID)
                {
                    LstDetalle.Remove(acti);
                    break;
                }
            }
            Session["Estructura"] = LstDetalle;

            gvEstructura.DataSourceID = null;
            gvEstructura.DataBind();
            gvEstructura.DataSource = LstDetalle;
            gvEstructura.DataBind();
        }
    }

    protected void gvEstructura_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnEstructura_Click(object sender, EventArgs e)
    {
        ObtenerListaEstructura();
        List<EmpresaEstructuraCarga> LstConcepto = new List<EmpresaEstructuraCarga>();
        if (Session["Estructura"] != null)
        {
            LstConcepto = (List<EmpresaEstructuraCarga>)Session["Estructura"];

            for (int i = 1; i <= 1; i++)
            {
                EmpresaEstructuraCarga pDetalle = new EmpresaEstructuraCarga();
                LstConcepto.Add(pDetalle);
            }
            gvEstructura.PageIndex = gvEstructura.PageCount;
            gvEstructura.DataSource = LstConcepto;
            gvEstructura.DataBind();

            Session["Estructura"] = LstConcepto;
        }
    }


    protected List<EmpresaEstructuraCarga> ObtenerListaEstructura()
    {
        List<EmpresaEstructuraCarga> lstConcepto = new List<EmpresaEstructuraCarga>();
        //lista para adicionar filas sin perder datos
        List<EmpresaEstructuraCarga> lista = new List<EmpresaEstructuraCarga>();

        foreach (GridViewRow rfila in gvEstructura.Rows)
        {
            EmpresaEstructuraCarga eConcep = new EmpresaEstructuraCarga();

            Label lblcodemparchivo = (Label)rfila.FindControl("lblcodemparchivo");
            if (lblcodemparchivo != null)
                if (lblcodemparchivo.Text.Trim() != "")
                    eConcep.codemparchivo = Convert.ToInt64(lblcodemparchivo.Text);
                else
                    eConcep.codemparchivo = -1;

            DropDownListGrid ddlEstructura = (DropDownListGrid)rfila.FindControl("ddlEstructura");
            if (ddlEstructura != null)
                if (ddlEstructura.SelectedValue != null)
                    if (ddlEstructura.SelectedValue != "")
                        eConcep.cod_estructura_carga = Convert.ToInt32(ddlEstructura.SelectedValue);

            lista.Add(eConcep);
            Session["Estructura"] = lista;

            if (eConcep.cod_estructura_carga != null)
            {
                if (eConcep.cod_estructura_carga.Value != 0)
                {
                    lstConcepto.Add(eConcep);
                }
            }
        }
        return lstConcepto;
    }

    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodEmpresa", "txtIdentificacion", "");
    }
    
    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {
        Xpinn.Seguridad.Services.UsuarioService UsuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
        Xpinn.FabricaCreditos.Entities.Persona1 DatosPersona = new Xpinn.FabricaCreditos.Entities.Persona1();

        DatosPersona = UsuarioServicio.ConsultarPersona1(txtIdentificacion.Text, (Usuario)Session["usuario"]);

        if (DatosPersona.cod_persona != 0)
        {
            txtCodEmpresa.Text = DatosPersona.cod_persona.ToString();
            txtIdentificacion.Text = DatosPersona.identificacion != "" ? DatosPersona.identificacion : "";
            txtNombre.Text = DatosPersona.nombre != "" ? DatosPersona.nombre : "";
        }
    }
}
