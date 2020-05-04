using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.IO;
using System.Configuration;
//using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.Programado.Services;
using Xpinn.Programado.Entities;
using Xpinn.FabricaCreditos.Entities;

partial class Nuevo : GlobalWeb
{

    LineasProgramadoServices LineasPrograService = new LineasProgramadoServices();

    LineasProgramado vDatos = new LineasProgramado();
    decimal cuota_minima = 0;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[LineasPrograService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(LineasPrograService.CodigoPrograma, "E");
            else
                VisualizarOpciones(LineasPrograService.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasPrograService.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDropdown();
                mvAplicar.ActiveViewIndex = 0;
                chkEstado.Checked = true;
                Session["RequisitoTopes"] = null;
                Session["RangoTopes"] = null;
                Session["idrango"] = null;
                txtCuotaMinExtra.Visible = false;
                txtCuotaMaxExtra.Visible = false;
                lblcuotamaximaExtra.Visible = false;
                lblcuotaminimaExtra.Visible = false;

                if (Session[LineasPrograService.CodigoPrograma + ".id"] != null)
                {
                    txtCodigo.Enabled = false;
                    idObjeto = Session[LineasPrograService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(LineasPrograService.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    chkRetiroParcial_CheckedChanged(chkRetiroParcial, null);
                    chkCruza_CheckedChanged(chkCruza, null);
                    chkAplicaReten_CheckedChanged(chkAplicaReten, null);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasPrograService.GetType().Name + "L", "Page_Load", ex);
        }
    }




    private void CargarDropdown()
    {

        ddlMoneda.Inicializar();
        ddlMoneda.Requerido = false;

        PoblarLista("PERIODICIDAD", ddlPeriodicidad);
        //PoblarLista("TIPO_LIQPROGRAMADO", ddlTipoLiquidacion);

        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        try
        {
            ctlTasaInteres.Inicializar();
            ctlTasaInteresReno.Inicializar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasPrograService.GetType().Name + "L", "CargarDropdown", ex);
        }
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
            LineasProgramado vDatos = new LineasProgramado();
            List<LineaProgramado_Rango> lRango = new List<LineaProgramado_Rango>();

            vDatos = LineasPrograService.ConsultarLineasProgramado(Convert.ToInt32(pIdObjeto), ref lRango, (Usuario)Session["usuario"]);

            gvTasas.DataSource = lRango;
            gvTasas.DataBind();
            Session["RangoTopes"] = lRango;
            if (vDatos.cod_linea_programado != null)
                txtCodigo.Text = vDatos.cod_linea_programado.Trim();

            if (vDatos.nombre != null)
                if (vDatos.nombre != "")
                    txtNombre.Text = vDatos.nombre.ToString().Trim();

            if (vDatos.cod_moneda != 0)
                ddlMoneda.Value = vDatos.cod_moneda.ToString();

            chkEstado.Checked = vDatos.estado == 1 ? true : false;

            // if (vDatos.tipo_liquidacion != 0)
            // {
            // ddlTipoLiquidacion.SelectedValue = vDatos.tipo_liquidacion.ToString();
            if (vDatos.tipo_saldo_int != 0)
            {
                ddlTipoSaldoInt.SelectedValue = vDatos.tipo_saldo_int.ToString();
            }
            if (vDatos.periodicidad_int != 0)
            {
                ddlPeriodicidad.SelectedValue = vDatos.periodicidad_int.ToString();
            }
            //   }

            if (vDatos.cuota_minima != 0)

                txtCuotaMin.Text = vDatos.cuota_minima.ToString();

            if (vDatos.plazo_minimo != 0)
                txtPlazoMin.Text = vDatos.plazo_minimo.ToString();

            if (vDatos.saldo_minimo != 0)
                txtSaldoMin.Text = vDatos.saldo_minimo.ToString();

            if (vDatos.prioridad != 0)
                txtPrioridad.Text = vDatos.prioridad.ToString();

            ///////TASA

            try
            {
                if (!string.IsNullOrEmpty(vDatos.calculo_tasa.ToString()))
                    ctlTasaInteres.FormaTasa = HttpUtility.HtmlDecode(vDatos.calculo_tasa.ToString());
                if (!string.IsNullOrEmpty(vDatos.tipo_historico.ToString()))
                    ctlTasaInteres.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(vDatos.tipo_historico.ToString()));
                if (!string.IsNullOrEmpty(vDatos.desviacion.ToString()))
                    ctlTasaInteres.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(vDatos.desviacion.ToString()));
                if (!string.IsNullOrEmpty(vDatos.cod_tipo_tasa.ToString()))
                    ctlTasaInteres.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(vDatos.cod_tipo_tasa.ToString()));
                if (!string.IsNullOrEmpty(vDatos.tasa_interes.ToString()))
                    ctlTasaInteres.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(vDatos.tasa_interes.ToString()));
            }
            catch
            { }

            chkTasaRenovacion.Checked = vDatos.interes_renovacion == 1 ? true : false;
            ///////TASA RENOVACION

            try
            {
                if (!string.IsNullOrEmpty(vDatos.calculo_tasa_ren.ToString()))
                    ctlTasaInteresReno.FormaTasa = HttpUtility.HtmlDecode(vDatos.calculo_tasa_ren.ToString());
                if (!string.IsNullOrEmpty(vDatos.tipo_historico_ren.ToString()))
                    ctlTasaInteresReno.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(vDatos.tipo_historico_ren.ToString()));
                if (!string.IsNullOrEmpty(vDatos.desviacion_ren.ToString()))
                    ctlTasaInteresReno.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(vDatos.desviacion_ren.ToString()));
                if (!string.IsNullOrEmpty(vDatos.cod_tipo_tasa_ren.ToString()))
                    ctlTasaInteresReno.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(vDatos.cod_tipo_tasa_ren.ToString()));
                if (!string.IsNullOrEmpty(vDatos.tasa_interes_ren.ToString()))
                    ctlTasaInteresReno.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(vDatos.tasa_interes_ren.ToString()));
            }
            catch
            { }



            ///////

            chkRetiroParcial.Checked = vDatos.retiro_parcial == 1 ? true : false;
          


            if (vDatos.por_retiro_maximo != 0)
                txtPorcRetiro.Text = vDatos.por_retiro_maximo.ToString();

            chkCruza.Checked = vDatos.cruza == 1 ? true : false;
            if (vDatos.porcentaje_cruce != 0)
                txtCruce.Text = vDatos.porcentaje_cruce.ToString();

            chkAplicaReten.Checked = vDatos.aplica_retencion == 1 ? true : false;
            if (vDatos.retencion != 0)
                txtPorcRetencion.Text = vDatos.retencion.ToString();

            if (vDatos.cuota_nomina != 0)
                txtCtaNomina.Text = vDatos.cuota_nomina.ToString();

            if (vDatos.valor_maximo_retiro != 0)
                txtVrMaxRetiro.Text = vDatos.valor_maximo_retiro.ToString();

            if (vDatos.dias_gracia != 0)
                txtDiasGracia.Text = vDatos.dias_gracia.ToString();

            chkCuotasExtras.Checked = vDatos.maneja_cuota_extra == 1 ? true : false;

            if(vDatos.maneja_cuota_extra ==1)
            {
                txtCuotaMinExtra.Visible = true;
                txtCuotaMaxExtra.Visible = true;
                lblcuotaminimaExtra.Visible = true;
                lblcuotamaximaExtra.Visible = true;
                if (vDatos.cuota_extra_min != 0)
                    
                    txtCuotaMinExtra.Text = vDatos.cuota_extra_min.ToString();
                if (vDatos.cuota_extra_max != 0)
                  
                txtCuotaMaxExtra.Text = vDatos.cuota_extra_max.ToString();
            }




            cbInteresPorCuenta.Checked = ConvertirABoolean(HttpUtility.HtmlDecode(vDatos.interes_por_cuenta.ToString()));


            chkRetiroParcial_CheckedChanged(chkRetiroParcial, null);
            chkCruza_CheckedChanged(chkCruza, null);
            chkAplicaReten_CheckedChanged(chkAplicaReten, null);




        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasPrograService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
    private Boolean ConvertirABoolean(string sParametro)
    {
        if (sParametro == null)
            return false;
        if (sParametro.Trim() == "1")
            return true;
        return false;
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public Boolean ValidarDatos()
    {
        if (txtCodigo.Text == "")
        {
            VerError("Debe ingresar el código de la Linea");
            txtCodigo.Focus();

            return false;
        }
        //if (txtCodigo.Text != "")
        //{
        //    vDatos = LineasPrograService.ConsultarLineasProgramado(Convert.ToInt32(txtCodigo.Text), (Usuario)Session["usuario"]);

        //    if (vDatos.cod_linea_programado !=null)
        //    {
        //        VerError("Ya existe un registro con el código ingresado");
        //        return false;
        //    }
        //}

        if (txtNombre.Text == "")
        {
            VerError("Ingrese concepto");
            txtNombre.Focus();
            return false;
        }

        //if (ddlTipoLiquidacion.SelectedIndex == 0 || ddlTipoLiquidacion.SelectedItem == null)
        //{
        //    VerError("Seleccione un tipo de Liquidación");
        //    ddlTipoLiquidacion.Focus();
        //    return false;
        //}

        //if (ddlTipoSaldoInt.SelectedIndex == 0 || ddlTipoSaldoInt.SelectedItem == null)
        //{
        //    VerError("Seleccione un tipo de saldo para el interes");
        //    ddlTipoSaldoInt.Focus();
        //    return false;
        //}

        if (ddlPeriodicidad.SelectedIndex == 0 || ddlPeriodicidad.SelectedItem == null)
        {
            VerError("Seleccione un tipo de periodicidad para el interes");
            ddlPeriodicidad.Focus();
            return false;
        }

        if (txtCuotaMin.Text == "" || txtCuotaMin.Text == "0")
        {
            VerError("Ingrese la cuota mínima");
            txtCuotaMin.Focus();
            return false;
        }
        if (txtPlazoMin.Text == "" || txtPlazoMin.Text == "0")
        {
            VerError("Ingrese el Plazo mínimo");
            txtPlazoMin.Focus();
            return false;
        }

        if (gvTasas.Rows.Count == 0)
        {
            VerError("Debe al menos agregar un rango de tasa de interes");
            return false;
        }

        if (chkRetiroParcial.Checked)
        {
            if (txtPorcRetiro.Text == "")
            {
                VerError("Ingrese el porcentaje de retiro parcial");
                txtPorcRetiro.Focus();
                return false;
            }
        }
        if (chkCruza.Checked)
        {
            if (txtCruce.Text == "")
            {
                VerError("Ingrese el porcentaje de cruce");
                txtCruce.Focus();
                return false;
            }
        }
        if (chkAplicaReten.Checked)
        {
            if (txtPorcRetencion.Text == "")
            {
                VerError("Ingrese el porcentaje de retención");
                txtPorcRetencion.Focus();
                return false;
            }
        }

        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            string msj = idObjeto != "" ? "modificar" : "grabar";
            ctlMensaje.MostrarMensaje("Desea " + msj + " los datos ingresados?");
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            Usuario pUsu = (Usuario)Session["usuario"];
            LineasProgramado pVar = new LineasProgramado();

            List<LineaProgramado_Rango> ListaRango = new List<LineaProgramado_Rango>();

            if (txtCodigo.Text != "")
                pVar.cod_linea_programado = txtCodigo.Text;

            pVar.nombre = txtNombre.Text.Trim();
            pVar.estado = chkEstado.Checked ? 1 : 0;
            pVar.cod_moneda = Convert.ToInt32(ddlMoneda.Value);
            // pVar.tipo_liquidacion = Convert.ToInt32(ddlTipoLiquidacion.SelectedValue);
            pVar.tipo_saldo_int = Convert.ToInt32(ddlTipoSaldoInt.SelectedValue);
            pVar.periodicidad_int = Convert.ToInt32(ddlPeriodicidad.SelectedValue);
            pVar.cuota_minima = Convert.ToDecimal(txtCuotaMin.Text);
            pVar.plazo_minimo = Convert.ToInt32(txtPlazoMin.Text);
            pVar.saldo_minimo = txtSaldoMin.Text != "" ? Convert.ToDecimal(txtSaldoMin.Text) : 0;
            pVar.prioridad = txtPrioridad.Text != "" ? Convert.ToInt32(txtPrioridad.Text) : 0;

            //extras 
            pVar.maneja_cuota_extra = chkCuotasExtras.Checked ? 1 : 0;

            if (pVar.maneja_cuota_extra == 1)
            {
                pVar.cuota_extra_min = Convert.ToDecimal(txtCuotaMinExtra.Text);
                pVar.cuota_extra_max = Convert.ToDecimal(txtCuotaMaxExtra.Text);
            }
            if (pVar.maneja_cuota_extra == 0)
            {
                pVar.cuota_extra_min = 0;
                pVar.cuota_extra_max = 0;
            }
            //tasa

            pVar.interes_por_cuenta = Convert.ToInt32(cbInteresPorCuenta.Checked);
            pVar.calculo_tasa = Convert.ToInt32(ctlTasaInteres.FormaTasa);
            pVar.tipo_historico = Convert.ToInt32(ctlTasaInteres.TipoHistorico);
            pVar.desviacion = Convert.ToDecimal(ctlTasaInteres.Desviacion);
            pVar.cod_tipo_tasa = Convert.ToInt32(ctlTasaInteres.TipoTasa);
            try
            {
                pVar.tasa_interes = Convert.ToDecimal(ctlTasaInteres.Tasa);
            }
            catch
            {
                pVar.tasa_interes = null;
            }

            //tasa
            

            pVar.interes_renovacion = Convert.ToInt32(chkTasaRenovacion.Checked);
            pVar.calculo_tasa_ren = Convert.ToInt32(ctlTasaInteresReno.FormaTasa);
            pVar.tipo_historico_ren = Convert.ToInt32(ctlTasaInteresReno.TipoHistorico);
            pVar.desviacion_ren = Convert.ToDecimal(ctlTasaInteresReno.Desviacion);
            pVar.cod_tipo_tasa_ren = Convert.ToInt32(ctlTasaInteresReno.TipoTasa);
            try
            {
                pVar.tasa_interes_ren = Convert.ToDecimal(ctlTasaInteresReno.Tasa);
            }
            catch
            {
                pVar.tasa_interes_ren = null;
            }



            ListaRango = (List<LineaProgramado_Rango>)Session["RangoTopes"];

            if (chkRetiroParcial.Checked)
            {
                pVar.retiro_parcial = 1;
                if (Convert.ToDecimal(txtPorcRetiro.Text) > 100 || Convert.ToDecimal(txtPorcRetiro.Text) < 0)
                {
                    VerError("Ingrese un porcentaje de retiro parcial que este dentro del rango de 0 al 100%");
                    txtPorcRetiro.Focus();
                    return;
                }
                pVar.por_retiro_maximo = Convert.ToDecimal(txtPorcRetiro.Text);
            }
            else
            {
                pVar.retiro_parcial = 0;
                pVar.por_retiro_maximo = 0;
            }

            if (chkCruza.Checked)
            {
                pVar.cruza = 1;
                if (Convert.ToDecimal(txtCruce.Text) > 100 || Convert.ToDecimal(txtCruce.Text) < 0)
                {
                    VerError("Ingrese un porcentaje de cruce que este dentro del rango de 0 al 100%");
                    txtCruce.Focus();
                    return;
                }
                pVar.porcentaje_cruce = Convert.ToDecimal(txtCruce.Text);
            }
            else
            {
                pVar.cruza = 0;
                pVar.porcentaje_cruce = 0;
            }

            if (chkAplicaReten.Checked)
            {
                pVar.aplica_retencion = 1;
                if (Convert.ToDecimal(txtPorcRetencion.Text) > 100 || Convert.ToDecimal(txtPorcRetencion.Text) < 0)
                {
                    VerError("Ingrese un porcentaje de retención que este dentro del rango de 0 al 100%");
                    txtPorcRetencion.Focus();
                    return;
                }
                pVar.retencion = Convert.ToInt32(txtPorcRetencion.Text);
            }
            else
            {
                pVar.aplica_retencion = 0;
                pVar.retencion = 0;
            }


            pVar.cuota_nomina = txtCtaNomina.Text != "" ? Convert.ToDecimal(txtCtaNomina.Text) : 0;
            pVar.valor_maximo_retiro = txtVrMaxRetiro.Text != "" ? Convert.ToDecimal(txtVrMaxRetiro.Text) : 0;
            pVar.dias_gracia = txtDiasGracia.Text != "" ? Convert.ToInt32(txtDiasGracia.Text) : 0;

            //DATOS NULOS
            pVar.por_retiro_plazo = 0;
            pVar.opcion_saldo = null;
            pVar.por_retiro_minimo = 0;
            pVar.por_int_dism = 0;
            pVar.porpla_ret_t = 0;
            pVar.pormont_ret_t = 0;
            pVar.interes_por_cuenta = Convert.ToInt32(cbInteresPorCuenta.Checked);

            List<LineaProgramado_Rango> lRango = new List<LineaProgramado_Rango>();
            vDatos = LineasPrograService.ConsultarLineasProgramado(Convert.ToInt32(txtCodigo.Text), ref lRango, (Usuario)Session["usuario"]);
            if (idObjeto != "")
            {
                pVar.por_retiro_plazo = vDatos.por_retiro_plazo;
                pVar.opcion_saldo = vDatos.opcion_saldo;
                pVar.por_retiro_minimo = vDatos.por_retiro_minimo;
                pVar.por_int_dism = vDatos.por_int_dism;
                pVar.porpla_ret_t = vDatos.porpla_ret_t;
                pVar.pormont_ret_t = vDatos.pormont_ret_t;
                //MODIFICAR
                LineasPrograService.CrearMod_LineasProgramado(pVar, ListaRango, pUsu, 2);
            }
            else
            {
                if (vDatos.cod_linea_programado != null)
                {
                    VerError("Ya existe un registro con el código ingresado");
                    return;
                }
                //CREAR
                LineasPrograService.CrearMod_LineasProgramado(pVar, ListaRango, pUsu, 1);
            }
            mvAplicar.ActiveViewIndex = 1;
            Site ToolBar = (Site)Master;
            ToolBar.MostrarGuardar(false);
            lblmsj.Text = idObjeto != "" ? "modificada" : "grabada";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasPrograService.CodigoPrograma, "btnContinuar_Click", ex);
        }
    }


    protected void chkRetiroParcial_CheckedChanged(object sender, EventArgs e)
    {
        if (chkRetiroParcial.Checked)
            txtPorcRetiro.Enabled = true;
        else
            txtPorcRetiro.Enabled = false;
    }

    protected void chkCruza_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCruza.Checked)
            txtCruce.Enabled = true;
        else
            txtCruce.Enabled = false;
    }

    protected void chkAplicaReten_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAplicaReten.Checked)
            txtPorcRetencion.Enabled = true;
        else
            txtPorcRetencion.Enabled = false;
    }
    protected void txtCodigo_TextChanged(object sender, EventArgs e)
    {

    }

    protected void btnnuevatasa_Click(object sender, ImageClickEventArgs e)
    {
        if (txtCodigo.Text == "")
        {
            VerError("Debe ingresar el código de la Linea");
            txtCodigo.Focus();
            return;
        }
        else
        {
            VerError("");
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
            mvAplicar.ActiveViewIndex = 2;
            txtNombreGrupo.Text = "";
            ctlTasaInteres.Inicializar();
            ctlTasaInteresReno.Inicializar();
            lblCodLineaProgramado.Text = txtCodigo.Text;
            InicializarTopes();
        }
    }
    protected void InicializarTopes()
    {
        List<LineaProgramado_Requisito> lstTopes = new List<LineaProgramado_Requisito>();
        for (int i = gvTopes.Rows.Count; i < 4; i++)
        {
            LineaProgramado_Requisito eTope = new LineaProgramado_Requisito();
            eTope.idrequisito = 0;
            eTope.tipo_tope = 0;
            eTope.minimo = "";
            eTope.maximo = "";
            lstTopes.Add(eTope);
        }
        gvTopes.DataSource = lstTopes;
        gvTopes.DataBind();
        Session["RequisitoTopes"] = lstTopes;
    }



    protected void gvTopes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Xpinn.FabricaCreditos.Services.LineasCreditoService Topesservicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
            DropDownListGrid ddlDescrpTope = (DropDownListGrid)e.Row.FindControl("ddlDescrpTope");
            if (ddlDescrpTope != null)
            {
                //PENDIENTE CARGAR DROP
                RangosTopes tope = new RangosTopes();
                ddlDescrpTope.DataSource = Topesservicio.ListarTopes(tope, (Usuario)Session["usuario"]);
                ddlDescrpTope.DataTextField = "descripcion";
                ddlDescrpTope.DataValueField = "tipo_tope";
                ddlDescrpTope.Items.Insert(0, new ListItem("Seleccione un Item", "0"));
                ddlDescrpTope.SelectedIndex = 0;
                ddlDescrpTope.DataBind();

                Label lbldescripciontope = (Label)e.Row.FindControl("lbldescripciontope");
                if (lbldescripciontope != null)
                {
                    if (lbldescripciontope.Text.Trim() != "")
                        ddlDescrpTope.SelectedValue = lbldescripciontope.Text;
                }
            }
        }
    }

    protected void btnDetalleTopes_Click(object sender, EventArgs e)
    {

        VerError("");
        List<LineaProgramado_Requisito> lstTopes = new List<LineaProgramado_Requisito>();
        lstTopes = ObtenerListaTopes();
        if (gvTopes.Rows.Count >= 0)
        {
            if (Session["RequisitoTopes"] != null)
            {

                lstTopes = (List<LineaProgramado_Requisito>)Session["RequisitoTopes"];
                if (lstTopes.Count <= 8)
                {
                    for (int i = 1; i <= 1; i++)
                    {
                        LineaProgramado_Requisito eTope = new LineaProgramado_Requisito();
                        eTope.idrequisito = 0;
                        eTope.tipo_tope = 0;
                        eTope.minimo = "";
                        eTope.maximo = "";
                        lstTopes.Add(eTope);
                    }
                    gvTopes.PageIndex = gvTopes.PageCount;
                    gvTopes.DataSource = lstTopes;
                    gvTopes.DataBind();
                    Session["RequisitoTopes"] = lstTopes;
                    if (lstTopes.Count == 8)
                        btnDetalleTopes.Enabled = false;
                }
            }
            else
            {
                InicializarTopes();
            }
        }

    }

    protected List<LineaProgramado_Requisito> ObtenerListaTopes()
    {
        List<LineaProgramado_Requisito> lstRequisito = new List<LineaProgramado_Requisito>();
        List<LineaProgramado_Requisito> lista = new List<LineaProgramado_Requisito>();

        foreach (GridViewRow rfila in gvTopes.Rows)
        {
            LineaProgramado_Requisito etope = new LineaProgramado_Requisito();

            Label lbltope = (Label)rfila.FindControl("lbltope");
            if (lbltope.Text != "")
                etope.idrequisito = Convert.ToInt32(lbltope.Text);

            etope.cod_linea_programado = this.lblCodLineaProgramado.Text;
            if (lblCodRango.Text != "")
                etope.idrango = Convert.ToInt32(lblCodRango.Text);

            DropDownListGrid ddlDescrpTope = (DropDownListGrid)rfila.FindControl("ddlDescrpTope");
            if (ddlDescrpTope.SelectedValue != null || ddlDescrpTope.SelectedIndex != 0)
                etope.tipo_tope = Convert.ToInt32(ddlDescrpTope.SelectedValue);

            TextBox txttopeminimo = (TextBox)rfila.FindControl("txttopeminimo");
            if (txttopeminimo.Text != "")
                etope.minimo = Convert.ToString(txttopeminimo.Text);
            else
                etope.minimo = null;

            TextBox txttopemaximo = (TextBox)rfila.FindControl("txttopemaximo");
            if (txttopemaximo.Text != "")
                etope.maximo = Convert.ToString(txttopemaximo.Text);
            else
                etope.maximo = null;

            lista.Add(etope);
            Session["RequisitoTopes"] = lista;

            if (((etope.minimo != "" && etope.minimo != null) || (etope.maximo != "" && etope.maximo != null)) && etope.tipo_tope != 0)
                lstRequisito.Add(etope);
        }

        return lstRequisito;
    }

    protected void gvTopes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Int64 conseID = Convert.ToInt64(gvTopes.DataKeys[e.RowIndex].Values[0].ToString());

        // Obtener listado de topes
        ObtenerListaTopes();
        List<LineaProgramado_Requisito> LstTopes;
        LstTopes = (List<LineaProgramado_Requisito>)Session["RequisitoTopes"];

        // Borrar el tope del listado y si existe en la base de datos borrarlo
        try
        {
            if (conseID > 0)
            {
                foreach (LineaProgramado_Requisito topes in LstTopes)
                {
                    if (Convert.ToInt64(topes.idrequisito) == conseID)
                    {
                        LstTopes.RemoveAt(e.RowIndex);
                        break;
                    }
                }
            }
            else
                LstTopes.RemoveAt(e.RowIndex);
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }

        gvTopes.DataSourceID = null;
        gvTopes.DataBind();

        gvTopes.DataSource = LstTopes;
        gvTopes.DataBind();

        Session["RequisitoTopes"] = LstTopes;
        if (LstTopes.Count < 8)
            btnDetalleTopes.Enabled = true;

    }

    protected void btnGuardarTasa_Click(object sender, EventArgs e)
    {
        try
        {
            LineaProgramado_Rango vTasa = new LineaProgramado_Rango();

            if (validarDatos())
            {
                VerError("");
                vTasa.cod_linea_programado = lblCodLineaProgramado.Text;
                if (idObjeto != "" && lblCodRango.Text != "")
                    vTasa.idrango = Convert.ToInt32(lblCodRango.Text);
                else
                    vTasa.idrango = 0;
                vTasa.descripcion = txtNombreGrupo.Text; //NOMBRE DEL GRUPO

                //Obtener lista de rango de topes
                vTasa.ListaRequisitos = new List<LineaProgramado_Requisito>();
                vTasa.ListaRequisitos = ObtenerListaTopes();
                vTasa.LineaTasa = new LineaProgramado_Tasa();
                //if (lblCodRango.Text != "")
                //    vTasa.LineaTasa.idrango = Convert.ToInt32(lblCodRango.Text);

               





                vTasa.LineaTasa.tipo_interes = int.Parse(ctlTasaInteres.FormaTasa);
                if (ctlTasaInteres.VisibleHistorico)
                {
                    vTasa.LineaTasa.tipo_historico = ctlTasaInteres.TipoHistorico;
                    vTasa.LineaTasa.desviación = Convert.ToDecimal(ctlTasaInteres.Desviacion);
                    vTasa.LineaTasa.cod_tipo_tasa = null;
                    vTasa.LineaTasa.tasa = null;
                }
                else if (ctlTasaInteres.VisibleFijo)
                {
                    vTasa.LineaTasa.tipo_historico = null;
                    vTasa.LineaTasa.desviación = null;
                    vTasa.LineaTasa.cod_tipo_tasa = ctlTasaInteres.TipoTasa;
                    vTasa.LineaTasa.tasa = ctlTasaInteres.Tasa;
                }
                else
                {
                    vTasa.LineaTasa.tipo_historico = null;
                    vTasa.LineaTasa.desviación = null;
                    vTasa.LineaTasa.cod_tipo_tasa = null;
                    vTasa.LineaTasa.tasa = null;
                }
                if (vTasa.ListaRequisitos.Count == 0)
                {
                    VerError("No existen topes para el grupo actual, Ingrese al menos un registro");
                    return;
                }
                else
                {
                    VerError("");
                    List<LineaProgramado_Rango> listaRango = new List<LineaProgramado_Rango>();
                    if (Session["RangoTopes"] != null)
                    {
                        listaRango = (List<LineaProgramado_Rango>)Session["RangoTopes"];

                        if (Session["idrango"] != null)
                        {
                            if (listaRango[(int)Session["idrango"]].idrango > 0)
                                vTasa.idrango = listaRango[(int)Session["idrango"]].idrango;
                            vTasa.LineaTasa.idrango = listaRango[(int)Session["idrango"]].idrango;
                            vTasa.LineaTasa.idtasa = listaRango[(int)Session["idrango"]].LineaTasa.idtasa;
                            vTasa.LineaTasa.cod_linea_programado = listaRango[(int)Session["idrango"]].LineaTasa.cod_linea_programado;
                            listaRango[(int)Session["idrango"]] = vTasa;
                            Session["idrango"] = null;
                        }
                        else
                        {
                            listaRango.Add(vTasa);
                        }
                        gvTasas.EmptyDataText = null;
                        gvTasas.DataSource = listaRango;
                        gvTasas.DataBind();
                        Session["RangoTopes"] = listaRango;
                    }
                    else
                    {
                        listaRango.Add(vTasa);
                        gvTasas.EmptyDataText = null;
                        gvTasas.DataSource = listaRango;
                        gvTasas.DataBind();
                        Session["RangoTopes"] = listaRango;
                    }

                }
            }
            else
            {
                return;
            }
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
            toolBar.MostrarCancelar(true);
            mvAplicar.ActiveViewIndex = 0;
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasPrograService.CodigoPrograma, "btnGuardarTasa_Click", ex);
        }
    }

    protected void btnCancelarTasa_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarCancelar(true);
        mvAplicar.ActiveViewIndex = 0;
    }

    private Boolean validarDatos()
    {
        if (txtNombreGrupo.Text == "")
        {
            VerError("Ingrese la Descripcion del Grupo");
            return false;
        }

        if (chkTasaRenovacion.Checked)
        {
            if (ctlTasaInteresReno.FormaTasa == "0")
            {
                VerError("Debe ingresar una tasa para Renovación ");
                return false;
            }
        }



        if (ctlTasaInteres.VisibleHistorico)
        {
            if (ctlTasaInteres.TipoHistorico == 0)
            {
                VerError("Seleccione un tipo de Histórico");
                return false;
            }
            if (ctlTasaInteres.Desviacion == 0)
            {
                VerError("Ingrese el monto de desviación");
                return false;
            }
        }
        else if (ctlTasaInteres.VisibleFijo)
        {
            if (ctlTasaInteres.TipoTasa == 0)
            {
                VerError("Seleccione un tipo de Tasa");
                return false;
            }
            if (ctlTasaInteres.Tasa == 0)
            {
                VerError("Ingrese el valor de la tasa.");
                return false;
            }
        }
        return true;
    }

    protected void gvTasas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int conseID = Convert.ToInt32(gvTasas.DataKeys[e.RowIndex].Values[0].ToString());
            List<LineaProgramado_Rango> lstRangos = new List<LineaProgramado_Rango>();
            lstRangos = (List<LineaProgramado_Rango>)Session["RangoTopes"];

            lstRangos.RemoveAt(e.RowIndex);
            //    AtributosTasaServices.EliminarRangoTopes(conseID, (Usuario)Session["usuario"]);

            gvTasas.DataSourceID = null;
            gvTasas.DataBind();

            gvTasas.DataSource = lstRangos;
            gvTasas.DataBind();

            Session["RangoTopes"] = lstRangos;

        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void gvTasas_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        ctlTasaInteres.Inicializar();
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        toolBar.MostrarCancelar(false);
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        List<LineaProgramado_Rango> listaRango = new List<LineaProgramado_Rango>();
        LineaProgramado_Rango Rango = new LineaProgramado_Rango();



        Session["idrango"] = e.NewSelectedIndex;
        string id = gvTasas.DataKeys[e.NewSelectedIndex].Value.ToString();

        listaRango = (List<LineaProgramado_Rango>)Session["RangoTopes"];

        if (id == "0")
        {
            Rango = listaRango[e.NewSelectedIndex];
            gvTopes.EmptyDataText = null;
            gvTopes.DataSource = Rango.ListaRequisitos;
            gvTopes.DataBind();
            txtNombreGrupo.Text = Rango.descripcion;
            Session["RequisitoTopes"] = Rango.ListaRequisitos;

            ctlTasaInteres.FormaTasa = Rango.LineaTasa.tipo_interes.ToString();
            if (!string.IsNullOrEmpty(Rango.LineaTasa.tipo_historico.ToString()))
                ctlTasaInteres.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(Rango.LineaTasa.tipo_historico.ToString()));
            if (!string.IsNullOrEmpty(Rango.LineaTasa.desviación.ToString()))
                ctlTasaInteres.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(Rango.LineaTasa.desviación.ToString()));
            if (!string.IsNullOrEmpty(Rango.LineaTasa.cod_tipo_tasa.ToString()))
                ctlTasaInteres.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(Rango.LineaTasa.cod_tipo_tasa.ToString()));
            if (!string.IsNullOrEmpty(Rango.LineaTasa.tasa.ToString()))
                ctlTasaInteres.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(Rango.LineaTasa.tasa.ToString()));

            mvAplicar.ActiveViewIndex = 2;

        }
        else
        {

            Rango = listaRango.Where(x => x.idrango == int.Parse(id)).ToList()[0];


            if (Rango.ListaRequisitos == null)
            {
                Rango.ListaRequisitos = new List<LineaProgramado_Requisito>();
                Rango.ListaRequisitos = LineasPrograService.ListarLineasProgramado_Requisito(Rango.idrango, Rango.cod_linea_programado, pUsuario);
            }
            if (Rango.LineaTasa == null)
            {
                Rango.LineaTasa = new LineaProgramado_Tasa();
                Rango.LineaTasa = LineasPrograService.ConsultarLineaProgramado_tasa(Rango.idrango, Rango.cod_linea_programado, pUsuario);
            }
            lblCodLineaProgramado.Text = txtCodigo.Text;
            gvTopes.EmptyDataText = null;
            gvTopes.DataSource = Rango.ListaRequisitos;
            gvTopes.DataBind();
            txtNombreGrupo.Text = Rango.descripcion;
            Session["RequisitoTopes"] = Rango.ListaRequisitos;

            ctlTasaInteres.FormaTasa = Rango.LineaTasa.tipo_interes.ToString();
            if (!string.IsNullOrEmpty(Rango.LineaTasa.tipo_historico.ToString()))
                ctlTasaInteres.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(Rango.LineaTasa.tipo_historico.ToString()));
            if (!string.IsNullOrEmpty(Rango.LineaTasa.desviación.ToString()))
                ctlTasaInteres.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(Rango.LineaTasa.desviación.ToString()));
            if (!string.IsNullOrEmpty(Rango.LineaTasa.cod_tipo_tasa.ToString()))
                ctlTasaInteres.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(Rango.LineaTasa.cod_tipo_tasa.ToString()));
            if (!string.IsNullOrEmpty(Rango.LineaTasa.tasa.ToString()))
                ctlTasaInteres.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(Rango.LineaTasa.tasa.ToString()));

            mvAplicar.ActiveViewIndex = 2;

        }
    }

    protected void chkCuotasExtras_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCuotasExtras.Checked == true)
        {
            txtCuotaMinExtra.Visible = true;
            txtCuotaMaxExtra.Visible = true;
            lblcuotamaximaExtra.Visible = true;
            lblcuotaminimaExtra.Visible = true;
        }
        else
        {
            txtCuotaMinExtra.Visible = false;
            txtCuotaMaxExtra.Visible = false;
            lblcuotamaximaExtra.Visible = false;
            lblcuotaminimaExtra.Visible = false;

        }
    }
}
