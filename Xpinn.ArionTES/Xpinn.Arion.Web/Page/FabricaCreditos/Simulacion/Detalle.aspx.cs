using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Microsoft.CSharp;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data;
using System.Globalization;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Services;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;

public partial class Detalle : GlobalWeb
{
    SimulacionService SimulacionServicio = new SimulacionService();
    DatosPlanPagosService datosServicio = new DatosPlanPagosService();
    Configuracion global = new Configuracion();
    string FormatoFecha = " ";
    Persona1Service persona1Service = new Persona1Service();
    Persona1 persona = new Persona1();
    List<Producto> lstConsulta = new List<Producto>();

    /// <summary>
    /// Cargar datos de la opción
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[SimulacionServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(SimulacionServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(SimulacionServicio.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoImprimir += btnImprimir_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SimulacionServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    /// <summary>
    /// Cargar datos de la página, llenar los combos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        FormatoFecha = global.ObtenerValorConfig("FormatoFecha");

        try
        {
            if (!IsPostBack)
            {
                txtFecha.Text = DateTime.Today.ToString(FormatoFecha);
                Accordion1.Visible = false;
                LlenarComboLineasCredito(lstLinea);
                LlenarComboPeriodicidad(lstPeriodicidad);
                LlenarComboPeriodicidadCtExt(ddlPeriodicidadCuotaExt);
                mvLista.ActiveViewIndex = 0;
                btnPlanPagos.Visible = false;
                HabilitarComision(false);
                HabilitarAporte(false);
                HabilitarSeguro(false);
                Session["COD_LINEA"] = lstLinea.SelectedValue;
                if (Session["COD_LINEA"] != null)
                {
                    //DETERMINAR SI SE VISUALIZARA EL CAMPO DE APORTE Y COMISION

                    OficinaService oficinaService = new OficinaService();
                    bool comision = false;
                    bool aporte = false;
                    bool seguro = false;
                    oficinaService.ValidarComisionAporte(Session["COD_LINEA"].ToString(), ref comision, ref aporte, ref seguro, (Usuario)Session["usuario"]);
                    HabilitarComision(comision);
                    HabilitarAporte(aporte);
                    HabilitarSeguro(seguro);
                    Session.Remove("COD_LINEA");
                }
                InicialCuoExt();
                Session["TotalCuoExt"] = 0;
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SimulacionServicio.CodigoPrograma, "Page_Load", ex);
        }
    }
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Session[SimulacionServicio.CodigoPrograma + ".id"] = idObjeto;
    }
    /// <summary>
    /// Llenar listado de líneas de crédito
    /// </summary>
    /// <param name="ddlLinea"></param>

    protected void LlenarComboLineasCredito(DropDownList ddlLinea)
    {
        LineasCreditoService lineaService = new LineasCreditoService();
        LineasCredito linea = new LineasCredito();
        ddlLinea.DataSource = lineaService.ListarLineasCredito(linea, (Usuario)Session["usuario"]);
        ddlLinea.DataTextField = "nombre";
        ddlLinea.DataValueField = "cod_linea_credito";
        ddlLinea.DataBind();
        ddlLinea.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

        TipoLiquidacionService tipoLiqService = new TipoLiquidacionService();
        TipoLiquidacion tipoliq = new TipoLiquidacion();
        ddlTipoLiquidacion.DataSource = tipoLiqService.ListarTipoLiquidacion(tipoliq, (Usuario)Session["usuario"]);
        ddlTipoLiquidacion.DataTextField = "descripcion";
        ddlTipoLiquidacion.DataValueField = "tipo_liquidacion";
        ddlTipoLiquidacion.DataBind();
        ddlTipoLiquidacion.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    /// <summary>
    /// Llenar listado de periodicidades de pago
    /// </summary>
    /// <param name="ddlPeriodicidad"></param>
    protected void LlenarComboPeriodicidad(DropDownList ddlPeriodicidad)
    {
        PeriodicidadService periodicService = new PeriodicidadService();
        Periodicidad periodic = new Periodicidad();
        ddlPeriodicidad.DataSource = periodicService.ListarPeriodicidad(periodic, (Usuario)Session["usuario"]);
        ddlPeriodicidad.DataTextField = "descripcion";
        ddlPeriodicidad.DataValueField = "codigo";
        ddlPeriodicidad.DataBind();
        ddlPeriodicidad.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    protected void LlenarComboPeriodicidadCtExt(DropDownList ddlPeriodicidadCuotaExt)
    {
        PeriodicidadService periodicService = new PeriodicidadService();
        Periodicidad periodic = new Periodicidad();
        ddlPeriodicidadCuotaExt.DataSource = periodicService.ListarPeriodicidad(periodic, (Usuario)Session["usuario"]);
        ddlPeriodicidadCuotaExt.DataTextField = "descripcion";
        ddlPeriodicidadCuotaExt.DataValueField = "codigo";
        ddlPeriodicidadCuotaExt.DataBind();
        ddlPeriodicidadCuotaExt.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

        ddlCuotaExtTipo.DataSource = TraerResultadosLista("TipoCuotaExtra");
        ddlCuotaExtTipo.DataTextField = "ListaDescripcion";
        ddlCuotaExtTipo.DataValueField = "ListaIdStr";
        ddlCuotaExtTipo.DataBind();
    }


    private List<Xpinn.FabricaCreditos.Entities.Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["usuario"]);
        return lstDatosSolicitud;
    }


    /// <summary>
    /// Calcular el valor de la cuota
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCalcular_Click(object sender, ImageClickEventArgs e)
    {

        Configuracion conf = new Configuracion();
        VerError("");
        string texto1 = txtMonto.Text;
        texto1 = texto1.Replace(conf.ObtenerSeparadorMilesConfig(), "");
        string texto = texto1;
        texto = texto.Replace(",", "");
        string error = "";

        decimal comision = 0, Aporte = 0, seguro = 0;
        try
        {
            if (txtComision.Text != "")
                comision = Decimal.Parse(txtComision.Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
        }
        catch
        {
            comision = 0;
        }

        try
        {
            if (txtAporte.Text != "")
                Aporte = Convert.ToDecimal(txtAporte.Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
            else
                Aporte = 0;
        }
        catch
        {
            Aporte = 0;
        }

        try
        {
            if (txtSeguro.Text != "")
                seguro = Convert.ToDecimal(txtSeguro.Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
            else
                seguro = 0;
        }
        catch
        {
            seguro = 0;
        }

        Simulacion Datos = new Simulacion();

        try
        {
            int tipo_liquidacion = Convert.ToInt32(ddlTipoLiquidacion.SelectedValue);
            long monto = Convert.ToInt64(texto.Replace(".", "").Replace(",", ""));
            txttasa.Text = txttasa.Text.Replace(".", "");
            decimal tasa = this.txttasa.Text == "" ? 0 : Convert.ToDecimal(txttasa.Text);
            DateTime? fechaprimerpago = null;
            if (txtFechaPrimerPago.TieneDatos)
                fechaprimerpago = txtFechaPrimerPago.ToDateTime;

            if (string.IsNullOrWhiteSpace(txtIdentificacionPersona.Text))
            {
                VerError("Debes digitar la identificacion de la persona a  simular!.");
                return;
            }

            // Consultar código de la persona
            Persona1Service personaService = new Persona1Service();
            long cod_persona = personaService.ConsultarCodigopersona(txtIdentificacionPersona.Text, Usuario);

            // Guardar datos de Cuotas Extras
            List<CuotasExtras> lstCuotasExtras = ObtenerListaCuotasExtras();

            // Calcular la cuota
            Datos = SimulacionServicio.ConsultarSimulacionCuota(monto, Convert.ToInt32(txtPlazo.Text.Replace(".", "").Replace(",", "")), Convert.ToInt32(lstPeriodicidad.Text), lstLinea.Text, tipo_liquidacion, tasa, comision, Aporte, fechaprimerpago, ref error, (Usuario)Session["Usuario"], cod_persona, lstCuotasExtras);
            if (error.Trim() != "")
                VerError(error);
            if (Datos != null)
                txtCuota.Text = Datos.cuota.ToString();
            else
                txtCuota.Text = "0";
            validarCuotasExtras();
            gvPlanPagos.DataSource = null;
            gvPlanPagos.DataBind();
            btnPlanPagos.Visible = true;
        }
        catch (Exception ex)
        {
            VerError("No se pudo calcular la cuota. " + ex.Message);
        }

    }

    protected void gvPlanPagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //gvPlanPagos.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SimulacionServicio.CodigoPrograma, "gvPlanPagos_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        VerError("");

        Int32 anchocolumna = 100;
        Int32 longitud = 0;

        Simulacion datosApp = new Simulacion();
        List<DatosPlanPagos> lstConsulta = new List<DatosPlanPagos>();
        List<DatosPlanPagos> lstConsultaajus = new List<DatosPlanPagos>();
        try
        {
            // Determinar separador de miles
            Configuracion global = new Configuracion();

            // Determinar el monto del crédito
            string texto;
            texto = txtMonto.Text.Replace(".", "");
            datosApp.monto = Convert.ToInt32(texto);

            // Determinar condiciones deseadas del crédito
            datosApp.plazo = Convert.ToInt32(txtPlazo.Text.Replace(".", "").Replace(",", ""));
            datosApp.periodic = Convert.ToInt32(lstPeriodicidad.Text);
            datosApp.for_pag = 1;
            datosApp.cod_credi = lstLinea.Text;
            datosApp.tipo_liquidacion = Convert.ToInt32(ddlTipoLiquidacion.SelectedValue);
            datosApp.fecha = txtFecha.ToDateTime;
            try
            {
                if (txtComision.Text != "")
                    datosApp.comision = Decimal.Parse(txtComision.Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
            }
            catch
            {
                datosApp.comision = 0;
            }

            try
            {
                if (txtAporte.Text != "")
                    datosApp.aporte = Convert.ToDecimal(txtAporte.Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
            }
            catch
            {
                datosApp.aporte = 0;
            }

            try
            {
                if (txtSeguro.Text != "")
                    datosApp.seguro = Convert.ToDecimal(txtSeguro.Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
            }
            catch
            {
                datosApp.seguro = 0;
            }
            if (txtFechaPrimerPago.TieneDatos)
                datosApp.fecha_primer_pago = txtFechaPrimerPago.ToDateTime;
            datosApp.tasa = this.txttasa.Text == "" ? 0 : Convert.ToDecimal(this.txttasa.Text);

            // Consultar código de la persona
            Persona1Service personaService = new Persona1Service();
            datosApp.cod_persona = personaService.ConsultarCodigopersona(txtIdentificacionPersona.Text, Usuario);

            // Guardar datos de Cuotas Extras
            datosApp.lstCuotasExtras = ObtenerListaCuotasExtras();

            // Realizar la simulación de créditos
            lstConsulta = SimulacionServicio.SimularPlanPagos(datosApp, (Usuario)Session["usuario"]);

            foreach (DatosPlanPagos pagos in lstConsulta)
            {
                if (pagos.sal_ini <= 0) continue;
                lstConsultaajus.Add(pagos);
            }

            Session["LSTCONSULTA"] = lstConsultaajus;

            gvPlanPagos.DataSource = lstConsultaajus;
            if (lstConsultaajus.Count > 0)
            {
                gvPlanPagos.DataBind();
                gvPlanPagos.Visible = true;
                gvPlanPagos.Columns[1].ItemStyle.Width = 90;
                // Ocultando las columnas que no deben mostrarse
                List<Atributos> lstAtr = new List<Atributos>();
                lstAtr = SimulacionServicio.SimularAtributosPlan((Usuario)Session["usuario"]);
                Session["AtributosPlanPagos"] = lstAtr;
                for (int i = 4; i <= 18; i++)
                {
                    gvPlanPagos.Columns[i].Visible = false;
                    int j = 0;
                    foreach (Atributos item in lstAtr)
                    {
                        if (j == i - 4)
                            gvPlanPagos.Columns[i].HeaderText = item.nom_atr;
                        j = j + 1;
                    }
                }
                // Establecer el ancho de las columnas de valores
                for (int i = 2; i < 20; i++)
                {
                    gvPlanPagos.Columns[i].ItemStyle.Width = anchocolumna;
                }
                // Ajustando el tamaño de la grilla
                longitud = 0;
                for (int i = 0; i < 20; i++)
                {
                    longitud = longitud + Convert.ToInt32(gvPlanPagos.Columns[i].ItemStyle.Width.Value);
                }
                if (longitud / 2 > 500)
                    gvPlanPagos.Width = longitud/2 + 100;
                else
                    gvPlanPagos.Width = 500;
                foreach (DatosPlanPagos ItemPlanPagos in lstConsultaajus)
                {
                    if (ItemPlanPagos.int_1 != 0) { gvPlanPagos.Columns[4].Visible = true; }
                    if (ItemPlanPagos.int_2 != 0) { gvPlanPagos.Columns[5].Visible = true; }
                    if (ItemPlanPagos.int_3 != 0) { gvPlanPagos.Columns[6].Visible = true; }
                    if (ItemPlanPagos.int_4 != 0) { gvPlanPagos.Columns[7].Visible = true; }
                    if (ItemPlanPagos.int_5 != 0) { gvPlanPagos.Columns[8].Visible = true; }
                    if (ItemPlanPagos.int_6 != 0) { gvPlanPagos.Columns[9].Visible = true; }
                    if (ItemPlanPagos.int_7 != 0) { gvPlanPagos.Columns[10].Visible = true; }
                    if (ItemPlanPagos.int_8 != 0) { gvPlanPagos.Columns[11].Visible = true; }
                    if (ItemPlanPagos.int_9 != 0) { gvPlanPagos.Columns[12].Visible = true; }
                    if (ItemPlanPagos.int_10 != 0) { gvPlanPagos.Columns[13].Visible = true; }
                    if (ItemPlanPagos.int_11 != 0) { gvPlanPagos.Columns[14].Visible = true; }
                    if (ItemPlanPagos.int_12 != 0) { gvPlanPagos.Columns[15].Visible = true; }
                    if (ItemPlanPagos.int_13 != 0) { gvPlanPagos.Columns[16].Visible = true; }
                    if (ItemPlanPagos.int_14 != 0) { gvPlanPagos.Columns[17].Visible = true; }
                    if (ItemPlanPagos.int_15 != 0) { gvPlanPagos.Columns[18].Visible = true; }
                }
                gvPlanPagos.DataBind();
                Session["PlanPagos"] = lstConsultaajus;
                Accordion1.Visible = true;
                ContenedorAtributos.Visible = false;
                List<DescuentosCredito> lstDescuentosCreditos = (from item in lstConsulta where item.lstDescuentos != null from items in item.lstDescuentos where items.val_atr > 0 select items).ToList();
                if (lstDescuentosCreditos != null)
                {
                    if (lstDescuentosCreditos.Count >= 1)
                    {
                        List<DescuentosCredito> descuentosCredito = new List<DescuentosCredito>();
                        Configuracion conf = new Configuracion();
                        decimal? vrMonto = 0;
                        string sMonto = txtMonto.Text.Replace("$", "").Replace(gSeparadorMiles, "");
                        sMonto =
                            sMonto.Replace(
                                System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator,
                                conf.ObtenerSeparadorDecimalConfig());
                        vrMonto = Convert.ToDecimal(sMonto);

                        foreach (DescuentosCredito drFila in lstDescuentosCreditos)
                        {
                            DescuentosCredito vrDescontados = new DescuentosCredito();
                            if (drFila.val_atr > 0)
                            {
                                vrDescontados.nom_atr = drFila.nom_atr;
                                vrDescontados.val_atr = vrDescontados.val_atr == null
                                    ? drFila.val_atr
                                    : drFila.val_atr + vrDescontados.val_atr;
                                descuentosCredito.Add(vrDescontados);
                            }
                        }
                        List<DescuentosCredito> lstdescuento = descuentosCredito;
                        foreach (DescuentosCredito item in lstdescuento)
                        {
                            vrMonto = vrMonto - item.val_atr;
                        }
                        txtVrDesembolsado.Text = Convert.ToDecimal(vrMonto - Convert.ToInt32(string.IsNullOrEmpty(txtActividadCIIU.Text) ? "0" : txtActividadCIIU.Text)).ToString("C");
                        lbDescontados.DataSource = lstdescuento;
                        Session["DescuentosPlan"] = lstdescuento;
                        lbDescontados.DataBind();
                        ContenedorAtributos.Visible = true;
                    }
                }
            }
            else
            {
                VerError("No se genero el plan de pagos");
            }

        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }

    }

    protected void gvPlanPagos_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Llenar la tabla del plan de pagos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPlanPagos_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void ExportarExcelGrilla(GridView gvGrilla, string Archivo)
    {
        try
        {
            if (gvGrilla.Rows.Count > 0)
            {
                string style = "";
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                style = "<link href=\"../../Styles/Styles.css\" rel=\"stylesheet\" type=\"text/css\" />";
                gvGrilla.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvGrilla);
                pagina.RenderControl(htw);
                Response.Clear();
                style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + Archivo + ".xls");
                Response.Charset = "UTF-8";
                Response.Write(sb.ToString());
                Response.End();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex0)
        {
            VerError(ex0.Message);
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (Session["LSTCONSULTA"] != null)
        {
            GridView gvGrillaExcel = new GridView();
            gvGrillaExcel = gvPlanPagos;
            List<DatosPlanPagos> lstConsulta = new List<DatosPlanPagos>();
            lstConsulta = (List<DatosPlanPagos>)Session["LSTCONSULTA"];
            gvGrillaExcel.AllowPaging = false;
            gvPlanPagos.DataSource = lstConsulta;
            gvGrillaExcel.DataBind();
            ExportarExcelGrilla(gvGrillaExcel, "Simulacion");
        }
    }

    protected void lstLinea_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            validarCuotasExtras();
            string lineaSeleccionada = lstLinea.SelectedValue.ToString();
            Usuario usuario = (Usuario)Session["Usuario"];
            LineasCreditoService LineaCreditoServicio = new LineasCreditoService();
            LineasCredito eLinea = LineaCreditoServicio.ConsultarLineasCredito(lineaSeleccionada, usuario);

            ddlTipoLiquidacion.SelectedValue = eLinea.tipo_liquidacion.ToString();
            Session["COD_LINEA"] = lstLinea.SelectedValue;

            if (Session["COD_LINEA"] != null)
            {
                //DETERMINAR SI SE VISUALIZARA EL CAMPO DE APORTE Y COMISION
                OficinaService oficinaService = new OficinaService();
                bool comision = false;
                bool aporte = false;
                bool seguro = false;
                oficinaService.ValidarComisionAporte(Session["COD_LINEA"].ToString(), ref comision, ref aporte, ref seguro, (Usuario)Session["usuario"]);
                HabilitarComision(comision);
                HabilitarAporte(aporte);
                HabilitarSeguro(seguro);
                
                Session.Remove("COD_LINEA");
                
                // Calcular el cupo del crédito
                Calcular_Cupo();

            }

            eLinea = LineaCreditoServicio.ConsultarTasaInteresLineaCredito(lineaSeleccionada, usuario);
            txttasa.Text = eLinea.tasa.ToString();
            lblTipoTasa.Text = eLinea.tipotasa.ToString() + "-" + eLinea.descripcion_tipo_tasa;
        }
        catch (Exception ex)
        {
            VerError("lstLinea_SelectedIndexChanged, " + ex.Message);
        }
    }

    private void Calcular_Cupo()
    {
        Xpinn.FabricaCreditos.Entities.LineasCredito DatosLinea = new Xpinn.FabricaCreditos.Entities.LineasCredito();
        Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCredito = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        try
        {
            DatosLinea = LineaCredito.Calcular_Cupo(lstLinea.SelectedValue.ToString(), 0, DateTime.Today, (Usuario)Session["usuario"]);
            txtPlazoMaximo.Text = DatosLinea.Plazo_Maximo.ToString();
            txtMontoMaximo.Text = String.Format("{0:C}", DatosLinea.Monto_Maximo);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        VerError("");
        if (Session["PlanPagos"] == null)
        {
            VerError("No se ha generado el plan de pagos");
            return;
        }
        try
        {
            persona = persona1Service.ConsultarPersona(null, txtIdentificacionPersona.Text, (Usuario)Session["Usuario"]);
        }
        catch (Exception exception)
        {
            VerError("Error al consultar identificación: " + exception.Message);
            return;
        }

        // ---------------------------------------------------------------------------------------------------------
        // Pasar datos al reporte
        // ---------------------------------------------------------------------------------------------------------
        string cRutaDeImagen;
        cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";

        Usuario User = (Usuario)Session["usuario"];
        ReportParameter[] param = new ReportParameter[54];

        param[0] = new ReportParameter("Entidad", User.empresa);
        param[1] = new ReportParameter("numero_radicacion", " ");
        param[2] = new ReportParameter("cod_linea_credito", lstLinea.SelectedItem == null ? "" : lstLinea.SelectedItem.Value);
        param[3] = new ReportParameter("linea", lstLinea.SelectedItem == null ? "" : lstLinea.SelectedItem.Text);
        param[4] = new ReportParameter("nombre", persona.nombreYApellido);
        param[5] = new ReportParameter("identificacion", persona.identificacion);
        param[6] = new ReportParameter("direccion", " ");
        param[7] = new ReportParameter("ciudad", " ");
        param[8] = new ReportParameter("fecha_inico", " ");
        param[9] = new ReportParameter("fecha_primer", txtFechaPrimerPago.Text);
        param[10] = new ReportParameter("palzo", txtPlazo.Text);
        param[11] = new ReportParameter("fecha_generacion", txtFecha.Text);
        param[12] = new ReportParameter("periocidad", lstPeriodicidad.SelectedItem == null ? " " : lstPeriodicidad.SelectedItem.Text);
        param[13] = new ReportParameter("cuotas", txtPlazo.Text);
        param[14] = new ReportParameter("valor_cuota", string.Format(txtCuota.Text, "{0:n}"));
        param[15] = new ReportParameter("forma_pago", " ");
        param[16] = new ReportParameter("tasa_nominal", txttasa.Text);
        param[17] = new ReportParameter("tasa_efectiva", txttasa.Text);
        param[18] = new ReportParameter("desembolso", txtMonto.Text);
        param[19] = new ReportParameter("pagare", " ");

        List<Atributos> lstAtr = new List<Atributos>();
        lstAtr = (List<Atributos>)Session["AtributosPlanPagos"];
        int j = 0;
        if (lstAtr != null)
        {
            foreach (Atributos item in lstAtr)
            {
                param[20 + j] = new ReportParameter("titulo" + j, item.nom_atr);
                j = j + 1;
            }
        }
        for (int i = j; i < 15; i++)
        {
            param[20 + i] = new ReportParameter("titulo" + i, " ");
        }
        List<DatosPlanPagos> lstPlan = new List<DatosPlanPagos>();
        lstPlan = (List<DatosPlanPagos>)Session["PlanPagos"];
        Boolean[] bVisible = new Boolean[16];
        for (int i = 1; i <= 15; i++)
        {
            bVisible[i] = false;
            i = i + 1;
        }
        foreach (DatosPlanPagos ItemPlanPagos in lstPlan)
        {
            if (ItemPlanPagos.int_1 != 0) { bVisible[1] = true; }
            if (ItemPlanPagos.int_2 != 0) { bVisible[2] = true; }
            if (ItemPlanPagos.int_3 != 0) { bVisible[3] = true; }
            if (ItemPlanPagos.int_4 != 0) { bVisible[4] = true; }
            if (ItemPlanPagos.int_5 != 0) { bVisible[5] = true; }
            if (ItemPlanPagos.int_6 != 0) { bVisible[6] = true; }
            if (ItemPlanPagos.int_7 != 0) { bVisible[7] = true; }
            if (ItemPlanPagos.int_8 != 0) { bVisible[8] = true; }
            if (ItemPlanPagos.int_9 != 0) { bVisible[9] = true; }
            if (ItemPlanPagos.int_10 != 0) { bVisible[10] = true; }
            if (ItemPlanPagos.int_11 != 0) { bVisible[11] = true; }
            if (ItemPlanPagos.int_12 != 0) { bVisible[12] = true; }
            if (ItemPlanPagos.int_13 != 0) { bVisible[13] = true; }
            if (ItemPlanPagos.int_14 != 0) { bVisible[14] = true; }
            if (ItemPlanPagos.int_15 != 0) { bVisible[15] = true; }
        }
        for (int i = 0; i < 15; i++)
        {
            param[35 + i] = new ReportParameter("visible" + i, bVisible[i + 1].ToString());
        }
        param[50] = new ReportParameter("nomUsuario", User.nombre);
        param[51] = new ReportParameter("ImagenReport", cRutaDeImagen);
        param[52] = new ReportParameter("fecha_solicitud", txtFecha.Text);
        param[53] = new ReportParameter("fecha_desembolso", txtFecha.Text);
        ReportViewerPlan.LocalReport.EnableExternalImages = true;
        ReportViewerPlan.LocalReport.SetParameters(param);

        ReportDataSource rds = new ReportDataSource("DataSetPlanPagos", CrearDataTable(idObjeto));

        ReportViewerPlan.LocalReport.DataSources.Clear();
        ReportViewerPlan.LocalReport.DataSources.Add(rds);
        ReportViewerPlan.LocalReport.Refresh();
        // Mostrar el reporte en pantalla.
        mvLista.ActiveViewIndex = 1;
        Iframe1.Visible = false;
        ReportViewerPlan.Visible = true;
    }

    public DataTable CrearDataTable(String pIdObjeto)
    {
        if (Session["PlanPagos"] == null)
            return null;

        System.Data.DataTable table = new System.Data.DataTable();

        DatosPlanPagos datosApp = new DatosPlanPagos();
        datosApp.numero_radicacion = 0;
        List<DatosPlanPagos> lstPlanPagos = new List<DatosPlanPagos>();
        lstPlanPagos = (List<DatosPlanPagos>)Session["PlanPagos"];
        List<Atributos> lstAtr = new List<Atributos>();
        lstAtr = (List<Atributos>)Session["AtributosPlanPagos"];

        table.Columns.Add("numerocuota");
        table.Columns.Add("fechacuota");
        table.Columns.Add("sal_ini");
        table.Columns.Add("capital");
        table.Columns.Add("int_1");
        table.Columns.Add("int_2");
        table.Columns.Add("int_3");
        table.Columns.Add("int_4");
        table.Columns.Add("int_5");
        table.Columns.Add("int_6");
        table.Columns.Add("int_7");
        table.Columns.Add("int_8");
        table.Columns.Add("int_9");
        table.Columns.Add("int_10");
        table.Columns.Add("int_11");
        table.Columns.Add("int_12");
        table.Columns.Add("int_13");
        table.Columns.Add("int_14");
        table.Columns.Add("int_15");
        table.Columns.Add("total");
        table.Columns.Add("sal_fin");

        foreach (DatosPlanPagos item in lstPlanPagos)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.numerocuota;
            if (item.fechacuota != null && item.total != 0)
            {
                datarw[1] = item.fechacuota.Value.ToShortDateString();
                datarw[2] = item.sal_ini.ToString();
                datarw[3] = item.capital.ToString();
                datarw[4] = item.int_1.ToString();
                datarw[5] = item.int_2.ToString();
                datarw[6] = item.int_3.ToString();
                datarw[7] = item.int_4.ToString();
                datarw[8] = item.int_5.ToString();
                datarw[9] = item.int_6.ToString();
                datarw[10] = item.int_7.ToString();
                datarw[11] = item.int_8.ToString();
                datarw[12] = item.int_9.ToString();
                datarw[13] = item.int_10.ToString();
                datarw[14] = item.int_11.ToString();
                datarw[15] = item.int_12.ToString();
                datarw[16] = item.int_13.ToString();
                datarw[17] = item.int_14.ToString();
                datarw[18] = item.int_15.ToString();
                datarw[19] = item.total.ToString();
                datarw[20] = item.sal_fin.ToString();
                table.Rows.Add(datarw);
            }
        }

        return table;
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        mvLista.ActiveViewIndex = 0;
    }

    protected void btnImprimiendose_Click(object sender, EventArgs e)
    {
        if (ReportViewerPlan.Visible == true)
        {
            //MOSTRAR REPORTE EN PANTALLA
            var bytes = ReportViewerPlan.LocalReport.Render("PDF");
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "inline;attachment; filename=Simulacion.pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.Clear();
        }
    }

    protected void HabilitarComision(bool pComision)
    {
        panelComision.Visible = pComision;
        panelComision.Height = pComision == true ? 28 : 0;
    }

    protected void HabilitarAporte(bool pAporte)
    {
        panelAporte.Visible = pAporte;
        panelAporte.Height = pAporte == true ? 28 : 0;
    }

    protected void HabilitarSeguro(bool pSeguro)
    {
        panelSeguro.Visible = pSeguro;
        panelSeguro.Height = pSeguro == true ? 28 : 0;
    }
    protected void gvCuoExt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Control ctrl = e.Row.FindControl("ddlformapago");
            if (ctrl != null)
            {
                DropDownList ddlformapago = ctrl as DropDownList;
            }
        }


        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Control ctrl = e.Row.FindControl("ddltipocuotagv");
            if (ctrl != null)
            {
                DropDownList ddltipocuota = ctrl as DropDownList;

                ddltipocuota.DataSource = TraerResultadosLista("TipoCuotaExtra"); ;
                ddltipocuota.DataTextField = "ListaDescripcion";
                ddltipocuota.DataValueField = "ListaIdStr";
                ddltipocuota.DataBind();

            }
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
            if (eCuoExt.valor != 0 || eCuoExt.valor == null)
            {
                decimal total = Convert.ToDecimal(Session["TotalCuoExt"].ToString());
                if (total != 0)
                {
                    total = total - Convert.ToDecimal(eCuoExt.valor);
                    Session["TotalCuoExt"] = total;
                }
                lstCuoExt.Remove(eCuoExt);
            }
        }
        if (lstCuoExt.Count == 0)
        {
            InicialCuoExt();
            gvCuoExt.DataSource = lstCuoExt;
            gvCuoExt.DataBind();
            Session["CuoExt"] = lstCuoExt;
        }
        else
        {
            gvCuoExt.DataSource = lstCuoExt;
            gvCuoExt.DataBind();
            Session["CuoExt"] = lstCuoExt;
        }
    }
    protected void InicialCuoExt()
    {
        List<Xpinn.FabricaCreditos.Entities.CuotasExtras> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.CuotasExtras>();
        Xpinn.FabricaCreditos.Entities.CuotasExtras eCuoExt = new Xpinn.FabricaCreditos.Entities.CuotasExtras();
        lstConsulta.Add(eCuoExt);
        Session["CuoExt"] = lstConsulta;
        gvCuoExt.DataSource = lstConsulta;
        gvCuoExt.DataBind();
        gvCuoExt.Visible = false;
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
            decimal total = Convert.ToDecimal(Session["TotalCuoExt"].ToString());
            total = total + Convert.ToDecimal(gItemNew.valor);
            Session["TotalCuoExt"] = total;
            gvCuoExt.DataSource = lstCuoExt;
            gvCuoExt.DataBind();
            Session["CuoExt"] = lstCuoExt;
        }
    }
    protected void gvCuoExt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvCuoExt.PageIndex = e.NewPageIndex;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(/*DatosSolicitudServicio.CodigoPrograma*/"", "gvLista_PageIndexChanging", ex);
        }
    }
    protected void btnGenerarCuotaext_Click(object sender, EventArgs e)
    {
        List<CuotasExtras> lstCuoExt = new List<CuotasExtras>();
        lstCuoExt = (List<CuotasExtras>)Session["CuoExt"];
        if (lstCuoExt.Count == 1)
        {
            CuotasExtras gItem = new CuotasExtras();
            gItem = lstCuoExt[0];
            if (gItem.valor == 0 || gItem.valor == null)
                lstCuoExt.Remove(gItem);
        }
        if (txtPorcentaje.Text == "")
        {
            lblError.Text = "Debe digitar el porcentaje de cuota extra";
            return;
        }
        else if (txtNumeroCuotaExt.Text == "")
        {
            lblError.Text = "Debe digitar el numero de cuotas extras";
            return;
        }
        else if (txtFechaCuotaExt.Text == "")
        {
            lblError.Text = "Debe digitar la fecha inicial de cuota extra";
            return;
        }
        else if (txtValorCuotaExt.Text == "")
        {
            lblError.Text = "Debe digitar el valor de cuota extra";
            return;
        }
        if (lstPeriodicidad.SelectedIndex <= 0)
        {
            lblError.Text = "Seleccione la periodicidad dentro de las condiciones del crédito";
            return;
        }
        if (ddlPeriodicidadCuotaExt.SelectedIndex <= 0)
        {
            lblError.Text = "Seleccione la periodicidad de la cuota extra";
            return;
        }
        // Determinar dias de la periodicidad
        PeriodicidadService periodServicio = new PeriodicidadService();
        Periodicidad periodo = new Periodicidad();
        periodo = periodServicio.ConsultarPeriodicidad(Convert.ToInt32(lstPeriodicidad.SelectedValue), (Usuario)Session["Usuario"]);
        // Calcular plazo cuotas extras
        DropDownList ddlperiod = new DropDownList();
        ddlperiod = ddlPeriodicidadCuotaExt;
        int valor_diasPeriodicidad = Convert.ToInt32(periodo.numero_dias);
        int plazo_CuotaExtra = (Convert.ToInt32(ddlPeriodicidadCuotaExt.SelectedValue) * Convert.ToInt32(txtNumeroCuotaExt.Text)) / valor_diasPeriodicidad;
        if (plazo_CuotaExtra > Convert.ToInt32(txtPlazo.Text))
        {
            lblError.Text = "El numero de cuotas por la periodicidad Excede el plazo";
            return;
        }
        Decimal valor_limite = (Convert.ToDecimal(txtMonto.Text) * Convert.ToDecimal(txtPorcentaje.Text)) / 100;
        Decimal valor_cuota = Convert.ToDecimal(txtValorCuotaExt.Text) * Convert.ToDecimal(txtNumeroCuotaExt.Text);
        if (valor_cuota > valor_limite)
        {
            lblError.Text = "La cantidad de cuotas extras por el valor excede el porcentaje del monto";
            return;
        }

        periodo = periodServicio.ConsultarPeriodicidad(Convert.ToInt32(ddlPeriodicidadCuotaExt.SelectedValue), (Usuario)Session["Usuario"]);
        lblError.Text = "";
        int total_cuota = Convert.ToInt32(txtNumeroCuotaExt.Text);
        int dias_inclemento = 0;
        decimal total = 0;
        DateTime? FechaCuotaExt = Convert.ToDateTime(txtFechaCuotaExt.Text);
        for (int i = 1; i <= total_cuota; i++)
        {
            CuotasExtras gItemNew = new CuotasExtras();
            gItemNew.fecha_pago = FechaCuotaExt;
            if (i > 1)
            {
                dias_inclemento = Convert.ToInt32(periodo.numero_dias);
                Xpinn.Comun.Services.FechasService fechaServicio = new Xpinn.Comun.Services.FechasService();
                gItemNew.fecha_pago = fechaServicio.FecSumDia(Convert.ToDateTime(gItemNew.fecha_pago), dias_inclemento, Convert.ToInt32(periodo.tipo_calendario));
                FechaCuotaExt = gItemNew.fecha_pago;
            }
            gItemNew.forma_pago = ddlFormaPagoCuotaExt.SelectedValue.ToString();
            gItemNew.des_forma_pago = ddlFormaPagoCuotaExt.SelectedItem.ToString();
            gItemNew.valor = Convert.ToInt64(txtValorCuotaExt.Text);
            gItemNew.des_tipo_cuota = ddlCuotaExtTipo.SelectedValue.ToString() + "-" + ddlCuotaExtTipo.SelectedItem.ToString();

            lstCuoExt.Add(gItemNew);
            total = total + Convert.ToDecimal(gItemNew.valor);
        }
        Session["TotalCuoExt"] = total;
        if (lstCuoExt.Count > 0)
            gvCuoExt.Visible = true;
        gvCuoExt.DataSource = lstCuoExt;
        gvCuoExt.DataBind();
        Session["CuoExt"] = lstCuoExt;
    }
    protected void btnLimpiarCuotaext_Click(object sender, EventArgs e)
    {
        List<CuotasExtras> lstCuoExt = new List<CuotasExtras>();
        gvCuoExt.DataSource = lstCuoExt;
        gvCuoExt.DataBind();
        Session["CuoExt"] = lstCuoExt;
        Session["TotalCuoExt"] = 0;
        InicialCuoExt();
    }
    protected void validarCuotasExtras()
    {
        Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();

        if (lstLinea.SelectedValue != "")
        {
            LineasCredito eLinea = new LineasCredito();
            eLinea = LineaCreditoServicio.ConsultarLineasCredito(lstLinea.SelectedValue.ToString(), (Usuario)Session["Usuario"]);

            if (eLinea.cuotas_extras == 1)
            {
                tblCtsExt.Visible = true;
                gvCuoExt.Visible = true;
                CuotasExt.Visible = true;
            }
            else
            {
                tblCtsExt.Visible = false;
                gvCuoExt.Visible = false;
                CuotasExt.Visible = false;
            }

        }
    }


    protected List<CuotasExtras> ObtenerListaCuotasExtras()
    {
        int num_ter = 0;
        /////////////////////////////////////////////////////////////////////////////////////////////
        // Guardar datos de Cuotas Extras
        /////////////////////////////////////////////////////////////////////////////////////////////
        List<CuotasExtras> lstCuotasExtras = new List<CuotasExtras>();
        foreach (GridViewRow rFila in gvCuoExt.Rows)
        {
            Xpinn.FabricaCreditos.Entities.CuotasExtras vCuotaExtra = new Xpinn.FabricaCreditos.Entities.CuotasExtras();
            vCuotaExtra.numero_radicacion = Convert.ToInt64(lstLinea.Text);
            Label lblfechapago = rFila.FindControl("lblfechapago") as Label;
            if (lblfechapago.Text == "")
                break;
            Label lblformapago = rFila.FindControl("lblformapago") as Label;
            Label lblvalor = rFila.FindControl("lblvalor") as Label;
            vCuotaExtra.fecha_pago = Convert.ToDateTime(lblfechapago.Text);
            if (lblformapago.Text == "Caja")
                vCuotaExtra.forma_pago = "1";
            else if (lblformapago.Text == "Nomina")
                vCuotaExtra.forma_pago = "2";
            vCuotaExtra.valor = ConvertirStringToDecimal(lblvalor.Text.Replace("$", "").Replace(gSeparadorMiles, ""));
            num_ter += 1;
            vCuotaExtra.cod_cuota = num_ter;
            lstCuotasExtras.Add(vCuotaExtra);
        }
        return lstCuotasExtras;
    }

    #region Mostrar Saldos 

    void CargarListas()
    {
        VerError("");
        // Mostrar información de los créditos
        try
        {
            persona = persona1Service.ConsultarPersona(null, txtIdentificacionPersona.Text.Trim(), (Usuario)Session["Usuario"]);
            if (persona == null)
            {
                VerError("El asociado no existe");
                return;
            }
        }
        catch (Exception e)
        {
            VerError("El asociado no existe");
            return;
        }
        if (persona.cod_persona != 0)
        {
            Producto producto = new Producto();
            producto.Persona.IdPersona = persona.cod_persona;


            EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
            String FiltroEstados = " 'DESEMBOLSADO' ";

            // Aplicar el filtro si se seleccionó el estado

            String FiltroFinal = " (estado Like 'ATRASADO%' Or estado = 'ESTA AL DIA' Or estado In (" + FiltroEstados + "))";
            lstConsulta = serviceEstadoCuenta.ListarProductosPorEstados(producto, (Usuario)Session["usuario"], FiltroFinal);
            var lstconsultacre = (from p in lstConsulta
                                  select new
                                  {
                                      p.NumRadicion,
                                      SaldoCapital = p.SaldoCapital.ToString("##,##0", CultureInfo.InvariantCulture),
                                      ValorTotalAPagar = p.ValorTotalAPagar.ToString("##,##0", CultureInfo.InvariantCulture)
                                  });
            gvCreditosSaldos.DataSource = lstconsultacre;
            gvCreditosSaldos.DataBind();
        }
        else
        {
            VerError("El asociado no existe");
        }

    }

    #endregion

    protected void txtIdentificacionPersona_OnTextChanged(object sender, EventArgs e)
    {
        CargarListas();
    }


    protected void chkPrincipal_OnCheckedChanged(object sender, EventArgs e)
    {
        int total = 0;
        foreach (GridViewRow row in gvCreditosSaldos.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkPrincipal");
            Label valorapagar = (Label)row.FindControl("lbl_ValorApagar");
            if (chk.Checked)
            {
                total += Convert.ToInt32(valorapagar.Text.Replace(",", ""));
            }
            else
            {
                if (!string.IsNullOrEmpty(txtActividadCIIU.Text) && total > 0 && Convert.ToInt32(txtActividadCIIU.Text) > 0)
                {
                    total -= Convert.ToInt32(valorapagar.Text.Replace(",", ""));
                }
                else
                {
                    total += 0;
                }
            }
        }
        txtActividadCIIU.Text = total.ToString();
    }
}
