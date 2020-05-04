using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Obligaciones.Services;
using Xpinn.Obligaciones.Entities;
using Xpinn.Caja.Services;
using Xpinn.Caja.Entities;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.IO;
using System.Web.Script.Services;

public partial class Detalle : GlobalWeb
{
    private Xpinn.Obligaciones.Services.SolicitudService SolicitudServicio = new Xpinn.Obligaciones.Services.SolicitudService();
    private Xpinn.Caja.Services.UsuariosService UsuarioServicio = new Xpinn.Caja.Services.UsuariosService();
    private Xpinn.Caja.Entities.Usuarios user2 = new Xpinn.Caja.Entities.Usuarios();

    private Xpinn.Obligaciones.Services.ObligacionCreditoService ObligacionCreditoServicio = new Xpinn.Obligaciones.Services.ObligacionCreditoService();

    PeriodicidadService periodicidadServicio = new PeriodicidadService();
    ComponenteService componenteServicio = new ComponenteService();
    Usuario user = new Usuario();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[SolicitudServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ObligacionCreditoServicio.CodigoPrograma6, "E");
            else
                VisualizarOpciones(ObligacionCreditoServicio.CodigoPrograma6, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;

            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma6, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                user = (Usuario)Session["usuario"];
                user2.codusuario = user.codusuario;
                user2 = UsuarioServicio.ConsultarUsuarios(user2.codusuario, (Usuario)Session["usuario"]);

                LlenarComboEntidades(ddlEntidad);
                LlenarComboMonedas(ddlTipoMoneda);
                LlenarComboTipoCuota(ddlTipoCuota);
                LlenarComboPeriodicidadCuota(ddlPeriodCuotas);
                LlenarComboTipoTasa(ddlTipoTasa, true);
                LlenarLineaObligacion(ddlLineaObligacion);

                AsignarEventoConfirmar();

                txtCajero.Text = user2.nombre;
                txtFechaSolicitud.Text = user2.fecha_actual.ToShortDateString();
                txtFechaTransaccion.Text = user2.fecha_actual.ToString();

                if (Session[ObligacionCreditoServicio.CodigoPrograma6 + ".id"] != null)
                {
                    idObjeto = Session[ObligacionCreditoServicio.CodigoPrograma6 + ".id"].ToString();
                    Session.Remove(ObligacionCreditoServicio.CodigoPrograma6 + ".id");
                    ObtenerDatos(idObjeto);
                    Actualizar();
                    CrearComponenteInicial();
                    CrearPagosExtInicial();
                }
                else
                {
                    Navegar(Pagina.Lista);
                }

                mvSolicitud.ActiveViewIndex = 0;
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma6, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Obligaciones.Entities.Solicitud vSolicitud = new Xpinn.Obligaciones.Entities.Solicitud();

            DataTable dtComp = new DataTable();
            dtComp = (DataTable)Session["Componentes"];

            DataTable dtPagos = new DataTable();
            dtPagos = (DataTable)Session["PagosExt"];

            if (mvSolicitud.ActiveViewIndex == 0)
            {
                if (ddlTipoGracia.SelectedValue.ToString() == "0")
                {
                    txtGracia.Text = "0";
                }
                if (txtGracia.Text == "")
                {
                    VerError("Diligenciar  el plazo de gracia de la obligación");
                }
                if (Convert.ToInt64(txtPlazo.Text) <= Convert.ToInt64(txtGracia.Text))
                {
                    VerError("El plazo de gracia no puede ser mayor o igual al plazo");
                }

                if (txtFechaDesembolso.Text == "" || txtValorTasa.Text == "")
                {
                    VerError("Diligenciar la fecha de desembolso y tasa");
                }

                else
                {
                    VerError("");
                    vSolicitud.codobligacion = long.Parse(txtCodigo.Text);
                    vSolicitud.montosolicitado = txtMontoSol.Text == "" ? 0 : decimal.Parse(txtMontoSol.Text);
                    vSolicitud.montoaprobado = txtMontoApro.Text == "" ? 0 : decimal.Parse(txtMontoApro.Text.Trim());
                    vSolicitud.saldocapital = txtMontoApro.Text == "" ? 0 : decimal.Parse(txtMontoApro.Text.Trim());
                    vSolicitud.tipomoneda = long.Parse(ddlTipoMoneda.SelectedValue.ToString());
                    vSolicitud.fechasolicitud = Convert.ToDateTime(txtFechaSolicitud.Text.Trim());
                    vSolicitud.fecha_aprobacion = Convert.ToDateTime(txtFechaDesembolso.Text.Trim());
                    vSolicitud.fechaultimopago = Convert.ToDateTime(txtFechaDesembolso.Text.Trim());
                    vSolicitud.fechaproximopago = Convert.ToDateTime(txtFechaDesembolso.Text.Trim());
                    vSolicitud.tipoliquidacion = long.Parse(ddlTipoCuota.SelectedValue.ToString());
                    vSolicitud.plazo = Convert.ToInt64(txtPlazo.Text.Trim());
                    vSolicitud.codperiodicidad = long.Parse(ddlPeriodCuotas.SelectedValue.ToString());
                    vSolicitud.estadoobligacion = ddlEstado.SelectedValue;
                    vSolicitud.numeropagare = txtPagare.Text == "" ? 0 : Convert.ToInt64(txtPagare.Text.Trim());
                    vSolicitud.codentidad = long.Parse(ddlEntidad.SelectedValue.ToString());
                    vSolicitud.codlineaobligacion = long.Parse(ddlLineaObligacion.SelectedValue);
                    vSolicitud.calculocomponente = rbCalculoTasa.SelectedIndex + 1;
                    if (rbCalculoTasa.SelectedIndex == 1)
                    {
                        vSolicitud.tipo_historico = long.Parse(ddlTipoTasa.SelectedValue);
                        vSolicitud.cod_tipo_tasa = 0;
                    }
                    else
                    {
                        vSolicitud.tipo_historico = 0;
                        vSolicitud.cod_tipo_tasa = long.Parse(ddlTipoTasa.SelectedValue);
                    }
                    vSolicitud.tasa = txtValorTasa.Text == "" ? 0 : decimal.Parse(txtValorTasa.Text);
                    vSolicitud.spread = txtPuntosads.Text == "" ? 0 : decimal.Parse(txtPuntosads.Text);
                    vSolicitud.cod_tipo_tasa = long.Parse(ddlTipoTasa.SelectedValue);
                    Actualizar();
                }

                //DATOS DE LA OPERACION
                Xpinn.Obligaciones.Entities.ObligacionCredito vOpe = new Xpinn.Obligaciones.Entities.ObligacionCredito();
                vOpe.cod_ope = 0;
                vOpe.cod_tipo_ope = 41;
                vOpe.codobligacion = long.Parse(txtCodigo.Text);
                vOpe.fechacuota = DateTime.Now;

                //se crea el desembolso de la obligacion
                if (vSolicitud.montoaprobado != 0)
                {
                    vSolicitud = SolicitudServicio.ModificarSolicitud(vOpe, vSolicitud, dtComp, dtPagos, (Usuario)Session["usuario"]);

                    idObjeto = vSolicitud.codobligacion.ToString();
                    Session[ObligacionCreditoServicio.CodigoPrograma6 + ".id"] = idObjeto;
                    Session["cod_ope"] = Convert.ToInt64(vSolicitud.cod_ope);
                    lblNroObligacion.Text = idObjeto;
                    txtValSol.Text = txtMontoSol.Text;
                    txtValDesem.Text = txtMontoApro.Text;

                    //se inactivan los campos para que no se pueda modificar
                    mvSolicitud.ActiveViewIndex = 1;
                    LlenarListBoxDescuentos(lbxValDescon);

                }
                else
                {
                    VerError("Debe digitar el valor a ser aprobado, fecha de desembolso o tasa");
                }
            }
            else
            {
                VerError("");
                mvSolicitud.ActiveViewIndex = 2;
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
                // Validar que exista la parametrización contable por procesos
                if (ValidarProcesoContable(Convert.ToDateTime(txtFechaTransaccion.Text), 41) == false)
                {
                    VerError("No se encontró parametrización contable por procesos para el tipo de operación 41");
                    return;
                }

                // Determinar código de proceso contable para generar el comprobante
                Int64? rpta = 0;
                if (!panelProceso.Visible && panelGeneral.Visible)
                {
                    rpta = ctlproceso.Inicializar(41, Convert.ToDateTime(txtFechaTransaccion.Text), (Usuario)Session["Usuario"]);
                    if (rpta > 1)
                    {
                        toolBar.MostrarGuardar(false);
                        // Activar demás botones que se requieran
                        panelGeneral.Visible = false;
                        panelProceso.Visible = true;
                    }
                    else
                    {
                        // Crear la tarea de ejecución del proceso                
                        if (AplicarDatos())
                            Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                        else
                            VerError("Se presentó error");
                    }
                }
            }

        }

        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma6, "btnGuardar_Click", ex);
        }

    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Navegar("Lista.aspx");
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Obligaciones.Entities.Solicitud vSolicitud = new Xpinn.Obligaciones.Entities.Solicitud();

            vSolicitud = SolicitudServicio.ConsultarSolicitud(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            //OBCREDITO
            if (!string.IsNullOrEmpty(vSolicitud.codobligacion.ToString()))
                txtCodigo.Text = vSolicitud.codobligacion.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.codentidad.ToString()))
                ddlEntidad.SelectedValue = vSolicitud.codentidad.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.montosolicitado.ToString()))
                txtMontoSol.Text = vSolicitud.montosolicitado.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.montoaprobado.ToString()))
                txtMontoApro.Text = vSolicitud.montoaprobado.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.tipomoneda.ToString()))
                ddlTipoMoneda.SelectedValue = vSolicitud.tipomoneda.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.fechasolicitud.ToString()))
                txtFechaSolicitud.Text = vSolicitud.fechasolicitud.ToShortDateString();
            if (!string.IsNullOrEmpty(vSolicitud.fecha_aprobacion.ToString()))
                txtFechaDesembolso.Text = vSolicitud.fecha_aprobacion.ToString(gFormatoFecha);
            if (!string.IsNullOrEmpty(vSolicitud.plazo.ToString()))
                txtPlazo.Text = vSolicitud.plazo.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.gracia.ToString()))
                txtGracia.Text = vSolicitud.gracia.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.tipo_gracia.ToString()))
                ddlTipoGracia.SelectedValue = vSolicitud.tipo_gracia.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.codperiodicidad.ToString()))
                ddlPeriodCuotas.SelectedValue = vSolicitud.codperiodicidad.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.tipoliquidacion.ToString()))
                ddlLineaObligacion.SelectedValue = vSolicitud.tipoliquidacion.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.tipoliquidacion.ToString()))
                ddlTipoCuota.SelectedValue = vSolicitud.tipoliquidacion.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.estadoobligacion))
                ddlEstado.SelectedValue = vSolicitud.estadoobligacion;
            if (!string.IsNullOrEmpty(vSolicitud.numeropagare.ToString()))
                txtPagare.Text = vSolicitud.numeropagare.ToString();

            //OBCOMPONENTECREDITO
            if (!string.IsNullOrEmpty(vSolicitud.calculocomponente.ToString()))
                rbCalculoTasa.SelectedValue = vSolicitud.calculocomponente.ToString();
            if (vSolicitud.calculocomponente == 2)
            {
                LlenarComboTipoTasa(ddlTipoTasa, false);
                if (!string.IsNullOrEmpty(vSolicitud.tipo_historico.ToString()))
                    ddlTipoTasa.SelectedValue = vSolicitud.tipo_historico.ToString();
            }
            else
            {
                LlenarComboTipoTasa(ddlTipoTasa, true);
                if (!string.IsNullOrEmpty(vSolicitud.cod_tipo_tasa.ToString()))
                    ddlTipoTasa.SelectedValue = vSolicitud.cod_tipo_tasa.ToString();
            }
            
            if (!string.IsNullOrEmpty(vSolicitud.tasa.ToString()))
                txtValorTasa.Text = vSolicitud.tasa.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.spread.ToString()))
                txtPuntosads.Text = vSolicitud.spread.ToString();

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma6, "ObtenerDatos", ex);
        }
    }

    protected void LlenarComboEntidades(DropDownList ddlEntidades)
    {
        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
        ddlEntidades.DataSource = bancoService.ListarBancos(banco, (Usuario)Session["usuario"]);
        ddlEntidades.DataTextField = "nombrebanco";
        ddlEntidades.DataValueField = "cod_banco";
        ddlEntidades.DataBind();
    }

    protected void LlenarComboMonedas(DropDownList ddlMonedas)
    {

        Xpinn.Caja.Services.TipoMonedaService monedaService = new Xpinn.Caja.Services.TipoMonedaService();
        Xpinn.Caja.Entities.TipoMoneda moneda = new Xpinn.Caja.Entities.TipoMoneda();
        ddlMonedas.DataSource = monedaService.ListarTipoMoneda(moneda, (Usuario)Session["usuario"]);
        ddlMonedas.DataTextField = "descripcion";
        ddlMonedas.DataValueField = "cod_moneda";
        ddlMonedas.DataBind();
    }

    protected void LlenarComboTipoCuota(DropDownList ddlTipoCuotas)
    {
        Xpinn.Obligaciones.Services.TipoLiquidacionService tipoLiqService = new Xpinn.Obligaciones.Services.TipoLiquidacionService();
        Xpinn.Obligaciones.Entities.TipoLiquidacion tipoLiq = new Xpinn.Obligaciones.Entities.TipoLiquidacion();
        ddlTipoCuotas.DataSource = tipoLiqService.ListarTipoLiquidacion(tipoLiq, (Usuario)Session["usuario"]);
        ddlTipoCuotas.DataTextField = "descripcion";
        ddlTipoCuotas.DataValueField = "codtipoliquidacion";
        ddlTipoCuotas.DataBind();
    }

    protected void LlenarComboPeriodicidadCuota(DropDownList ddlPeriodicidadCuotas)
    {
        Xpinn.Obligaciones.Services.PeriodicidadCuotaService PeriodicidadCuotaService = new Xpinn.Obligaciones.Services.PeriodicidadCuotaService();
        Xpinn.Obligaciones.Entities.PeriodicidadCuota PeriodicidadCuota = new Xpinn.Obligaciones.Entities.PeriodicidadCuota();
        ddlPeriodicidadCuotas.DataSource = PeriodicidadCuotaService.ListarPeriodicidadCuota(PeriodicidadCuota, (Usuario)Session["usuario"]);
        ddlPeriodicidadCuotas.DataTextField = "DESCRIPCION";
        ddlPeriodicidadCuotas.DataValueField = "COD_PERIODICIDAD";
        ddlPeriodicidadCuotas.DataBind();
    }

    protected void LlenarComboTipoTasa(DropDownList ddlTipoTasa, Boolean tipo)
    {
        Xpinn.Obligaciones.Services.TipoTasaService tasaService = new Xpinn.Obligaciones.Services.TipoTasaService();
        Xpinn.Obligaciones.Entities.TipoTasa tasa = new Xpinn.Obligaciones.Entities.TipoTasa();
        if (tipo == true)
        {
            ddlTipoTasa.DataSource = tasaService.ListarTipoTasa(tasa, (Usuario)Session["usuario"]);
            txtPuntosads.Text = "0";
            txtPuntosads.Enabled = false;
        }
        else
        {
            ddlTipoTasa.DataSource = tasaService.ListarTipoHistorico(tasa, (Usuario)Session["usuario"]);
            txtValorTasa.Enabled = false;
            txtPuntosads.Enabled = true;
        }
        ddlTipoTasa.DataTextField = "NOMBRE";
        ddlTipoTasa.DataValueField = "COD_TIPO_TASA";
        ddlTipoTasa.DataBind();
    }

    protected void LlenarLineaObligacion(DropDownList ddLineaObligacion)
    {
        Xpinn.Obligaciones.Services.LineaObligacionService lineaObService = new Xpinn.Obligaciones.Services.LineaObligacionService();
        Xpinn.Obligaciones.Entities.LineaObligacion lineaOb = new Xpinn.Obligaciones.Entities.LineaObligacion();
        ddLineaObligacion.DataSource = lineaObService.ListarLineaObligacion(lineaOb, (Usuario)Session["usuario"]);
        ddLineaObligacion.DataTextField = "NOMBRELINEA";
        ddLineaObligacion.DataValueField = "CODLINEAOBLIGACION";
        ddLineaObligacion.DataBind();
    }

    protected void gvPagosExt_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Control ctrl = e.Row.FindControl("ddPeriodo");
            if (ctrl != null)
            {
                DropDownList dd = ctrl as DropDownList;

                this.LlenarComboddPeriodo(dd);
            }
        }

    }


    protected void gvComponente_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Control ctrl = e.Row.FindControl("ddlComponente");
            if (ctrl != null)
            {
                DropDownList dd = ctrl as DropDownList;
                this.LlenarComboComponente(dd);
                DataTable dtAgre = new DataTable();
            }
        }
    }

    protected void LlenarComboddPeriodo(DropDownList ddlPeriodo)
    {
        Periodicidad periodo = new Periodicidad();
        List<Periodicidad> LstPeriodo = new List<Periodicidad>();
        LstPeriodo = periodicidadServicio.ListarPeriodicidad(periodo, (Usuario)Session["usuario"]);
        ddlPeriodo.DataSource = LstPeriodo;
        ddlPeriodo.DataTextField = "Descripcion";
        ddlPeriodo.DataValueField = "Codigo";
        ddlPeriodo.DataBind();
    }


    protected void LlenarComboComponente(DropDownList ddlComponente)
    {
        Componente component = new Componente();
        List<Componente> LstComponent = new List<Componente>();
        LstComponent = componenteServicio.ListarComponentes(component, (Usuario)Session["usuario"]);
        ddlComponente.DataSource = LstComponent;
        ddlComponente.DataTextField = "NOMBRE";
        ddlComponente.DataValueField = "CODCOMPONENTE";
        ddlComponente.DataBind();
    }

    private void CrearcompoenteadicionalInicial(int consecutivo, String nombresession)
    {
        ComponenteAdicional pcomponente = new ComponenteAdicional();
        List<ComponenteAdicional> LstComponente = new List<ComponenteAdicional>();

        pcomponente.NOMCOMPONENTE = "";
        pcomponente.FORMULA = -99;
        pcomponente.VALOR = -99;
        pcomponente.FINANCIADO = -1;
        pcomponente.DESCRIPCION = "";
        pcomponente.CODOBLIGACION = -99;
        pcomponente.NOMFORMULA = "";
        pcomponente.CODCOMPONENTE = 1;

        LstComponente.Add(pcomponente);

        Session[nombresession] = LstComponente;

    }


    protected void CrearComponenteInicial()
    {
        try
        {

            List<Xpinn.Obligaciones.Entities.ComponenteAdicional> lstConsulta = new List<Xpinn.Obligaciones.Entities.ComponenteAdicional>();
            Xpinn.Obligaciones.Services.ComponenteAdicionalService obCompAdiService = new Xpinn.Obligaciones.Services.ComponenteAdicionalService();

            lstConsulta = obCompAdiService.ListarComponenteAdicional(long.Parse(idObjeto), (Usuario)Session["usuario"]); ;

            DataTable dt = new DataTable();
            dt.Columns.Add("componente");
            dt.Columns.Add("formula");
            dt.Columns.Add("valor");
            dt.Columns.Add("chkFin", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("nomcomponente");
            dt.Columns.Add("nomformula");
            dt.Columns.Add("codcomponente");

            if (lstConsulta.Count > 0)
            {
                foreach (ComponenteAdicional fil in lstConsulta)
                {
                    DataRow fila = dt.NewRow();

                    fila[0] = fil.CODOBLIGACION;
                    fila[1] = fil.FORMULA;
                    fila[2] = fil.VALOR;
                    CheckBox chi = (CheckBox)(gvComponente.FindControl("chkFinanciado"));
                    try
                    {
                        if (chi != null)
                            chi.Checked = false;
                    }
                    catch
                    {
                    }
                    fila[3] = false.ToString();
                    fila[4] = fil.NOMCOMPONENTE;// nombre del componente
                    fila[5] = fil.NOMFORMULA;// nombre de la formula
                    fila[6] = fil.CODCOMPONENTE;// codigo del componente

                    dt.Rows.Add(fila);
                }

                Session["Componentes"] = dt;
                gvComponente.DataSource = dt;
                gvComponente.DataBind();
                gvComponente.Visible = true;

            }
            else
            {
                DataRow fila = dt.NewRow();
                fila[0] = null;
                fila[1] = null;
                fila[2] = null;
                fila[3] = false;
                fila[4] = null;
                fila[5] = null;
                dt.Rows.Add(fila);
                Session["Componentes"] = dt;
                gvComponente.DataSource = dt;
                gvComponente.DataBind();
                gvComponente.Rows[0].Visible = false;
            }
        }
        catch
        {
        }

    }


    protected void CrearPagosExtInicial()
    {
        List<Xpinn.Obligaciones.Entities.PagoExtraord> lstConsulta = new List<Xpinn.Obligaciones.Entities.PagoExtraord>();
        Xpinn.Obligaciones.Services.PagoExtraordService PagoExtraService = new Xpinn.Obligaciones.Services.PagoExtraordService();

        lstConsulta = PagoExtraService.ListarPagoExtraord(long.Parse(idObjeto), (Usuario)Session["usuario"]); ;

        DataTable dt = new DataTable();
        dt.Columns.Add("periodo");
        dt.Columns.Add("valor");
        dt.Columns.Add("nomperiodo");

        if (lstConsulta.Count > 0)
        {
            foreach (PagoExtraord fil in lstConsulta)
            {
                DataRow fila = dt.NewRow();

                fila[0] = fil.COD_PERIODICIDAD;
                fila[1] = fil.VALOR;
                fila[2] = fil.NOM_PERIODICIDAD;

                dt.Rows.Add(fila);
            }

            Session["PagosExt"] = dt;
            gvPagosExt.DataSource = dt;
            gvPagosExt.DataBind();
            gvPagosExt.Visible = true;
        }
        else
        {
            dt.Rows.Add();
            Session["PagosExt"] = dt;
            gvPagosExt.DataSource = dt;
            gvPagosExt.DataBind();
            gvPagosExt.Rows[0].Visible = false;
        }
    }



    protected void gvComponente_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            DropDownList ddlComponentes = (DropDownList)gvComponente.FooterRow.FindControl("ddlComponente");
            DropDownList ddlTipoFormula = (DropDownList)gvComponente.FooterRow.FindControl("ddlFormula");
            TextBox txtnewvalore = (TextBox)gvComponente.FooterRow.FindControl("txtnewvalor");
            CheckBox chkFinanciado = (CheckBox)gvComponente.FooterRow.FindControl("chkFinanciado");
            int optChkFin = 0;
            if (chkFinanciado != null)
                optChkFin = chkFinanciado.Checked == true ? 1 : 0;

            DataTable dtAgre = new DataTable();
            dtAgre = (DataTable)Session["Componentes"];

            if (dtAgre.Rows[0][0] == null || dtAgre.Rows[0][0].ToString() == "")
            {
                dtAgre.Rows[0].Delete();
            }


            DataRow fila = dtAgre.NewRow();

            fila[0] = ddlComponentes.SelectedValue;
            fila[1] = ddlTipoFormula.SelectedValue;
            fila[2] = txtnewvalore.Text;
            if (chkFinanciado.Checked)
            {
                fila[3] = true;
            }
            else
            {
                fila[3] = false;
            }

            //fila[3] = optChkFin;
            fila[4] = ddlComponentes.SelectedItem.Text;// nombre del componente
            fila[5] = ddlTipoFormula.SelectedItem.Text;// nombre de la formula

            dtAgre.Rows.Add(fila);
            gvComponente.DataSource = dtAgre;
            gvComponente.DataBind();
            Session["Componentes"] = dtAgre;
        }
    }


    protected void gvPagosExt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            DropDownList ddlPeriodo = (DropDownList)gvPagosExt.FooterRow.FindControl("ddPeriodo");
            TextBox txtnewvalore = (TextBox)gvPagosExt.FooterRow.FindControl("txtnewvalor");

            DataTable dtAgre = new DataTable();
            dtAgre = (DataTable)Session["PagosExt"];

            if (dtAgre.Rows[0][0] == null || dtAgre.Rows[0][0].ToString() == "")
            {
                dtAgre.Rows[0].Delete();
            }


            DataRow fila = dtAgre.NewRow();

            fila[0] = ddlPeriodo.SelectedValue;
            fila[1] = txtnewvalore.Text;
            fila[2] = ddlPeriodo.SelectedItem.Text;

            dtAgre.Rows.Add(fila);
            gvPagosExt.DataSource = dtAgre;
            gvPagosExt.DataBind();
            Session["PagosExt"] = dtAgre;
        }
    }


    protected void gvComponente_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        try
        {

            DataTable table = new DataTable();
            table = (DataTable)Session["Componentes"];//se pilla los componentes          

            List<Xpinn.Obligaciones.Entities.ComponenteAdicional> lstConsulta = new List<Xpinn.Obligaciones.Entities.ComponenteAdicional>();
            Xpinn.Obligaciones.Services.ComponenteAdicionalService obCompAdiService = new Xpinn.Obligaciones.Services.ComponenteAdicionalService();

            lstConsulta = obCompAdiService.ListarComponenteAdicional(long.Parse(idObjeto), (Usuario)Session["usuario"]); ;

            DataTable dt = new DataTable();

            if ((e.RowIndex == 0) && (table.Rows[0][0] != null || table.Rows[0][0].ToString() == "") && (table.Rows.Count == 1))
            {
                DataRow fila = table.NewRow();
                fila[0] = null;
                fila[1] = null;
                fila[2] = null;
                fila[3] = false;
                fila[4] = null;
                fila[5] = null;
                table.Rows.Add(fila);
            }

            table.Rows[e.RowIndex].Delete();
            gvComponente.DataSource = table;
            gvComponente.DataBind();
            Session["Componentes"] = table;

            if ((e.RowIndex == 0) && (table.Rows[0][0] == null || table.Rows[0][0].ToString() == "") && (table.Rows.Count == 1))
                gvComponente.Rows[0].Visible = false;

        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(componenteServicio.GetType().Name + "L", "gvComponente_RowDeleting", ex);
        }

    }


    protected void gvPagosExt_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable table = new DataTable();
            table = (DataTable)Session["PagosExt"];//se pilla los Pagos Extraordinarios

            if ((e.RowIndex == 0) && (table.Rows[0][0] != null || table.Rows[0][0].ToString() == "") && (table.Rows.Count == 1))
            {
                table.Rows.Add();
            }

            table.Rows[e.RowIndex].Delete();
            gvPagosExt.DataSource = table;
            gvPagosExt.DataBind();
            Session["PagosExt"] = table;

            if ((e.RowIndex == 0) && (table.Rows[0][0] == null || table.Rows[0][0].ToString() == "") && (table.Rows.Count == 1))
                gvPagosExt.Rows[0].Visible = false;
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(componenteServicio.GetType().Name + "L", "gvComponente_RowDeleting", ex);
        }

    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnGuardar"), "Desea grabar la información de la Obligación?");
    }


    private void Actualizar()
    {
        VerError("");
        try
        {
            List<Xpinn.Obligaciones.Entities.ObPlanPagos> lstConsulta = new List<Xpinn.Obligaciones.Entities.ObPlanPagos>();
            Xpinn.Obligaciones.Entities.ObPlanPagos obPlan = new Xpinn.Obligaciones.Entities.ObPlanPagos();
            Xpinn.Obligaciones.Services.ObPlanPagosService obPlanService = new Xpinn.Obligaciones.Services.ObPlanPagosService();

            obPlan.cod_obligacion = long.Parse(txtCodigo.Text);
            obPlan.tasa_efectiva = (ddlTipoTasa.SelectedValue == "" ? 0 : long.Parse(ddlTipoTasa.SelectedValue));

            obPlan.tipo_tasa = lblTasaIntPer.Text;

            // obPlan.cuota = (lblCuota.Text == "" ? 0 : Convert.ToString(lblCuota.Text));
            // obPlan.cuota = Convert.ToInt64(lblCuota.Text);

            lstConsulta = obPlanService.ListarObPlanPagos(obPlan, (Usuario)Session["usuario"]);

            gvObPlan.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvObPlan.Visible = true;
                gvObPlan.DataBind();

            }

            ObPlanPagosService planpagosservicio = new ObPlanPagosService();

            ObPlanPagos planpagos = new ObPlanPagos();
            planpagos = planpagosservicio.ConsultarObcomponente(long.Parse(idObjeto), (Usuario)Session["usuario"]);


            if (!string.IsNullOrEmpty(planpagos.tasa.ToString()))
                lblTasaIntPer.Text = Convert.ToString(planpagos.tasa.ToString());

            if (!string.IsNullOrEmpty(planpagos.cuota.ToString()))
                lblCuota.Text = planpagos.cuota.ToString("##,##0");

            if (!string.IsNullOrEmpty(planpagos.tipo_tasa.ToString()))
            {
                lblTasaEfectiva.Text = Convert.ToString(planpagos.tipo_tasa.ToString());
            }
            else
            {
                gvObPlan.Visible = false;
            }

            Session.Add(ObligacionCreditoServicio.CodigoPrograma6 + ".consulta", 1);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma6, "Actualizar", ex);
        }
    }

    private void Consultar()
    {
        VerError("");
        try
        {
            List<Xpinn.Obligaciones.Entities.ObPlanPagos> lstConsulta = new List<Xpinn.Obligaciones.Entities.ObPlanPagos>();
            Xpinn.Obligaciones.Entities.ObPlanPagos obPlan = new Xpinn.Obligaciones.Entities.ObPlanPagos();
            Xpinn.Obligaciones.Services.ObPlanPagosService obPlanService = new Xpinn.Obligaciones.Services.ObPlanPagosService();

            obPlan.cod_obligacion = long.Parse(txtCodigo.Text);
            obPlan.tasa_efectiva = (ddlTipoTasa.SelectedValue == "" ? 0 : long.Parse(ddlTipoTasa.SelectedValue));

            lstConsulta = obPlanService.ConsultarObPlanPagos(obPlan, (Usuario)Session["usuario"]);

            gvObPlan.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvObPlan.Visible = true;
                gvObPlan.DataBind();

            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma6, "Actualizar", ex);
        }
    }

    private void actualizarcomponentes(String pIdObjeto)
    {
        //  Actualizar();
        ObPlanPagosService planpagosservicio = new ObPlanPagosService();
        idObjeto = Session[ObligacionCreditoServicio.CodigoPrograma6 + ".id"].ToString();

        ObPlanPagos planpagos = new ObPlanPagos();
        planpagos = planpagosservicio.ConsultarObcomponente(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);


        if (!string.IsNullOrEmpty(planpagos.tasa.ToString()))
            lblTasaIntPer.Text = Convert.ToString(planpagos.tasa.ToString("##,##0"));


        if (!string.IsNullOrEmpty(planpagos.cuota.ToString()))
            lblCuota.Text = Convert.ToString(planpagos.cuota.ToString("##,##0"));

        if (!string.IsNullOrEmpty(planpagos.cuota.ToString()))
            lblTasaEfectiva.Text = Convert.ToString(planpagos.tipo_tasa.ToString());
    }

    protected void LlenarListBoxDescuentos(ListBox LstBoxDescon)
    {
        List<Xpinn.Obligaciones.Entities.ComponenteAdicional> lstConsulta = new List<Xpinn.Obligaciones.Entities.ComponenteAdicional>();
        Xpinn.Obligaciones.Services.ComponenteAdicionalService obCompAdService = new Xpinn.Obligaciones.Services.ComponenteAdicionalService();
        Usuario usuario = new Usuario();
        lstConsulta = obCompAdService.ListarComponenteAdicional(long.Parse(idObjeto), (Usuario)Session["usuario"]);
        LstBoxDescon.DataSource = lstConsulta;
        LstBoxDescon.DataTextField = "DESCRIPCION";
        LstBoxDescon.DataBind();
    }



    protected void gvObPlan_RowCommand(object sender, GridViewCommandEventArgs evt)
    {

        if (evt.CommandName == "DetallePago")
        {

            int index = Convert.ToInt32(evt.CommandArgument);

            GridViewRow gvObPlanRow = gvObPlan.Rows[index];

            txtFechaCuota.Text = gvObPlan.Rows[index].Cells[2].Text;
            txtNroCuota.Text = gvObPlan.Rows[index].Cells[1].Text;
            txtCapital.Text = gvObPlan.Rows[index].Cells[3].Text;
            txtIntCorr.Text = gvObPlan.Rows[index].Cells[4].Text;
            txtIntMora.Text = "0";
            txtSeguro.Text = "0";
            mpeRegObPlanPago.Show();
        }
    }


    protected void AceptarButton_Click(object sender, EventArgs e)
    {
        Xpinn.Obligaciones.Services.ObPlanPagosService obPLanService = new Xpinn.Obligaciones.Services.ObPlanPagosService();
        Xpinn.Obligaciones.Entities.ObPlanPagos obPlan = new Xpinn.Obligaciones.Entities.ObPlanPagos();

        obPlan.cod_obligacion = long.Parse(lblNroObligacion.Text);
        obPlan.nrocuota = long.Parse(txtNroCuota.Text);
        obPlan.fecha = Convert.ToDateTime(txtFechaCuota.Text);
        obPlan.amort_cap = txtCapital.Text == "" ? 0 : decimal.Parse(txtCapital.Text);
        obPlan.interes_corriente = txtIntCorr.Text == "" ? 0 : decimal.Parse(txtIntCorr.Text);
        obPlan.interes_mora = txtIntMora.Text == "" ? 0 : decimal.Parse(txtIntMora.Text);
        obPlan.seguro = txtSeguro.Text == "" ? 0 : decimal.Parse(txtSeguro.Text);

        obPlan = obPLanService.ModificarPlanPagos(obPlan, (Usuario)Session["usuario"]);

        mpeRegObPlanPago.Hide();
        Consultar();
    }

    protected void chkFinanciado_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void gvComponente_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void chkFinanciado_CheckedChanged1(object sender, EventArgs e)
    {
        CheckBox chkFinanciado = (CheckBox)gvComponente.FindControl("chkFinanciado");

        foreach (GridViewRow gv in gvComponente.Rows)

            if (chkFinanciado.Checked)
            {
                int optChkFin = chkFinanciado.Checked == true ? 1 : 0;

            }
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        idObjeto = "";
        Navegar("../../../General/Global/inicio.aspx");
        Session.Remove(SolicitudServicio.CodigoPrograma + ".id");
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        mpeRegObPlanPago.Hide();
    }

    protected void rbCalculoTasa_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbCalculoTasa.SelectedIndex == 1)
            LlenarComboTipoTasa(ddlTipoTasa, false);
        else
            LlenarComboTipoTasa(ddlTipoTasa, true);
    }

    protected void ddlTipoTasa_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbCalculoTasa.SelectedIndex == 1)
        {
            Xpinn.Obligaciones.Services.TipoTasaService tasaService = new Xpinn.Obligaciones.Services.TipoTasaService();
            Xpinn.Obligaciones.Entities.TipoTasa tasa = new Xpinn.Obligaciones.Entities.TipoTasa();
            try
            {
                txtValorTasa.Text = Convert.ToString(tasaService.ConsultaTasaHistorica(Convert.ToInt64(ddlTipoTasa.SelectedValue.ToString()), Convert.ToDateTime(txtFechaSolicitud.Text), (Usuario)Session["usuario"]));
            }
            catch (Exception ex)
            {
                VerError("Error al determinar el valor de la tasa histórica " + ex.Message);
            }
        }
    }


    protected void btnCancelarProceso_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarCancelar(true);
        toolBar.MostrarConsultar(true);
        panelGeneral.Visible = true;
        panelProceso.Visible = false;
    }

    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            panelGeneral.Visible = true;
            panelProceso.Visible = false;
            // Aquí va la función que hace lo que se requiera grabar en la funcionalidad
            AplicarDatos();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected bool AplicarDatos()
    {
        string Cod_Tercero = SolicitudServicio.Consultar_Tercero(long.Parse(idObjeto), (Usuario)Session["usuario"]);
        Int64? CodOpe = Convert.ToInt64(Session["cod_ope"]);
        Int32? TipoOpe = 41;
        Int64? CodPersona = Convert.ToInt64(Cod_Tercero);
        Usuario usuario = (Usuario)Session["usuario"];

        if (CodOpe != 0 && TipoOpe != 0 && CodPersona != 0 && usuario != null)
        {
            ctlproceso.CargarVariables(CodOpe, TipoOpe, CodPersona, usuario);
            return true;
        }
        else
        {
            return false;
        }

    }



}
