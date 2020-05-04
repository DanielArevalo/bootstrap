using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Microsoft.Reporting.WebForms;


partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    PreAnalisisService PreAnalisisServ = new PreAnalisisService();
    PoblarListas Poblar = new PoblarListas();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[personaServicio.CodigoProgramaPreAnalisis + ".id"] != null)
                VisualizarOpciones(personaServicio.CodigoProgramaPreAnalisis, "E");
            else
                VisualizarOpciones(personaServicio.CodigoProgramaPreAnalisis, "A");
            frmPrint.Visible = false;
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.MostrarCancelar(false);
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.MostrarImprimir(false);
            ctlBusquedaPersonas.eventoEditar += gvListaTitulares_SelectedIndexChanged;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(personaServicio.CodigoProgramaPreAnalisis, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDropDown();
                panelGeneral.Enabled = true;
                if (Session[personaServicio.CodigoProgramaPreAnalisis + ".id"] != null)
                {
                    mvPreAnalisis.ActiveViewIndex = 1;
                    idObjeto = Session[personaServicio.CodigoProgramaPreAnalisis + ".id"].ToString();
                    Session.Remove(personaServicio.CodigoProgramaPreAnalisis + ".id");
                    lblTitulo.Visible = false;
                    ObtenerDatos(idObjeto);
                    calcular();
                }
                else
                {
                    Session.Remove("DatosGrid");
                    mvPreAnalisis.ActiveViewIndex = 0;
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarConsultar(true);
                    toolBar.MostrarGuardar(false);
                    toolBar.MostrarImprimir(false);
                }
            }
            calcular();
            
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(personaServicio.CodigoProgramaPreAnalisis, "Page_Load", ex);
        }
    }

    private void CargarDropDown()
    {
        Poblar.PoblarListaDesplegable("PERIODICIDAD", "","", " 1 ",ddlPeriodicidad, (Usuario)Session["usuario"]);
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (mvPreAnalisis.ActiveViewIndex == 3)
            mvPreAnalisis.ActiveViewIndex = 0;

        VerError("");
        ctlBusquedaPersonas.Filtro = "";
        ctlBusquedaPersonas.Actualizar(0);      
    }


    protected void calcular()
    {
        decimal suma = 0;
        if (txtSaldoDisponible.Text != "")
            suma += Convert.ToDecimal(txtSaldoDisponible.Text.Replace(".", ""));
        if (txtCuotaCreditoCancelado.Text != "")
            suma += Convert.ToDecimal(txtCuotaCreditoCancelado.Text.Replace(".", ""));
        if (txtCuotaServicios.Text != "")
            suma += Convert.ToDecimal(txtCuotaServicios.Text.Replace(".", ""));
        if (txtPagoTerceros.Text != "")
            suma += Convert.ToDecimal(txtPagoTerceros.Text.Replace(".", ""));
        if (txtCuotaOtros.Text != "")
            suma += Convert.ToDecimal(txtCuotaOtros.Text.Replace(".", ""));
        if (txtIngresosAdicionales.Text != "")
            suma -= Convert.ToDecimal(txtIngresosAdicionales.Text.Replace(".", ""));
        txtSubTotal.Text = suma.ToString().Trim();
        CalcularMenosSMLMV();
        decimal deduccion = 0;
        if (txtMenosSMLMV.Text != "")
            deduccion = Convert.ToInt64(txtMenosSMLMV.Text.Replace(".", ""));
        txtDisponible.Text = Convert.ToString(suma - deduccion);

    }

    protected Boolean CalcularMenosSMLMV()
    {
        if (txtnumeroSMLMV.SelectedValue == "3")
        {
            txtMenosSMLMV.Enabled = true;
            return true;
        }
        else
        {
            txtMenosSMLMV.Enabled = false;
        }
        // Determinar el valor del salrio mínimo
        Xpinn.Comun.Entities.General salarios = new Xpinn.Comun.Entities.General();
        salarios = personaServicio.consultarsalariominimo(10, (Usuario)Session["usuario"]);
        Session["Salariominimo"] = salarios;
        // Determinar el valor de la deducción
        decimal valorDeduccion = 0;
        try
        {
            if (txtnumeroSMLMV.SelectedValue == "1")
                valorDeduccion = Convert.ToInt64(txtnumeroSMLMV.SelectedItem.Value.Replace(".", "")) * Convert.ToInt64(salarios.valor.Replace(".", ""));
            else
                valorDeduccion = Math.Round(Convert.ToDecimal(txtSubTotal.Text.Replace(".", "")) / 2);
        }
        catch
        {
            return false;
        }
        txtMenosSMLMV.Text = Convert.ToString(valorDeduccion);     
        return true;
    }

    protected Boolean ValidarDatos()
    {
        if (txtMonto.Text.Trim() == "" || txtMonto.Text.Trim() == "0")
        {
            VerError("Ingrese el monto solicitado.");
            txtMonto.Focus();
            return false;
        }
        if (txtPlazo.Text.Trim() == "" || txtPlazo.Text.Trim() == "0")
        {
            VerError("Ingrese el plazo solicitado.");
            txtPlazo.Focus();
            return false;
        }
        int cont = 0;
        foreach (GridViewRow rFila in gvCreditos.Rows)
        {
            CheckBoxGrid chkSeleccionar = (CheckBoxGrid)rFila.FindControl("chkSeleccionar");
            if (chkSeleccionar != null)
            {
                if (chkSeleccionar.Checked)
                    cont++;
            }
        }
        if (cont == 0)
        {
            VerError("No existe seleccionado un crédito, verifique los datos.");
            return false;
        }
        else if (cont > 1)
        {
            VerError("No puede seleccionar mas de un crédito, verifique los datos.");
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (ValidarDatos())
            ctlMensaje.MostrarMensaje("Desea guardar los datos del preanálisis?");
    }
    
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario vUsuario = new Usuario();
            vUsuario = (Usuario)Session["Usuario"];

            Xpinn.FabricaCreditos.Services.CreditoService creditoServicio = new CreditoService();
            Credito crearcreditos = new Credito();

            crearcreditos.idpreanalisis = 0;
            if(lblCodPersona.Text!="")
                crearcreditos.cod_persona = Convert.ToInt64(lblCodPersona.Text);
            crearcreditos.fecha = DateTime.Now;
            if (txtSaldoDisponible.Text != "")
                crearcreditos.saldo_disponible = Convert.ToDecimal(txtSaldoDisponible.Text);
            if (txtCuotaCreditoCancelado.Text != "")
                crearcreditos.cuota_credito = Convert.ToDecimal(txtCuotaCreditoCancelado.Text);
            if (txtCuotaServicios.Text != "")
                crearcreditos.cuota_servicios = Convert.ToDecimal(txtCuotaServicios.Text);
            if (txtPagoTerceros.Text != "")
                crearcreditos.pago_terceros = Convert.ToDecimal(txtPagoTerceros.Text);
            if (txtCuotaOtros.Text != "")
                crearcreditos.cuota_otros = Convert.ToDecimal(txtCuotaOtros.Text);
            if (txtIngresosAdicionales.Text != "")
                crearcreditos.ingresos_adicionales = Convert.ToDecimal(txtIngresosAdicionales.Text);
            if (txtMenosSMLMV.Text != "")
                crearcreditos.menos_smlmv = Convert.ToDecimal(txtMenosSMLMV.Text);
            if (txtDisponible.Text != "")
                crearcreditos.total_disponible = Convert.ToDecimal(txtDisponible.Text);
            if (txtAportes.Text != "")
                crearcreditos.aportes = Convert.ToDecimal(txtAportes.Text);
            if (txtCreditos.Text != "")
                crearcreditos.creditos = Convert.ToDecimal(txtCreditos.Text);
            if (txtCapitaliza.Text != "")
                crearcreditos.capitalizacion = Convert.ToDecimal(txtCapitaliza.Text);
            if (vUsuario != null)
                crearcreditos.cod_usuario = Convert.ToInt32(vUsuario.codusuario);
            if (txtMonto.Text != "")
                crearcreditos.monto = Convert.ToDecimal(txtMonto.Text.Replace(".",""));
            if (txtPlazo.Text != "")
                crearcreditos.plazo = Convert.ToInt64(txtPlazo.Text);
            //CAPTURA DEL CODIGO DE CREDITO
            List<Credito> lstCredito = new List<Credito>();
            string cod_linea = "";
            foreach (GridViewRow rFila in gvCreditos.Rows)
            {
                CheckBoxGrid chkSeleccionar = (CheckBoxGrid)rFila.FindControl("chkSeleccionar");
                if (chkSeleccionar != null)
                {
                    if (chkSeleccionar.Checked)
                    {
                        Credito pEntidad = new Credito();

                        pEntidad.idpreanalisis = 1;
                        Label lblcod_Linea = (Label)rFila.FindControl("lblinea_credito");
                        if (lblcod_Linea != null && lblcod_Linea.Text != "")
                        {
                            cod_linea = lblcod_Linea.Text;
                            pEntidad.cod_linea_credito = cod_linea;
                        }
                        Label lblnom_linea = (Label)rFila.FindControl("lbnomlinea_credito");
                        if (lblnom_linea != null && lblnom_linea.Text != "")
                            pEntidad.nom_linea_credito = lblnom_linea.Text;

                        Label lbmontomaximo = (Label)rFila.FindControl("lbmontomaximo");
                        if (lbmontomaximo != null && lbmontomaximo.Text != "")
                            pEntidad.monto_maximo = Convert.ToDecimal(lbmontomaximo.Text.Replace(".", ""));

                        Label lblmonto = (Label)rFila.FindControl("lbmonto");
                        if (lblmonto != null && lblmonto.Text != "")
                            pEntidad.monto = Convert.ToDecimal(lblmonto.Text);

                        Label lblCuota = (Label)rFila.FindControl("Cuota");
                        if (lblCuota != null && lblCuota.Text != "")
                            pEntidad.cuota_credito = Convert.ToDecimal(lblCuota.Text);
                        
                        Label lblPlazo = (Label)rFila.FindControl("Plazo");
                        if (lblPlazo != null && lblPlazo.Text != "")
                            pEntidad.plazo = Convert.ToInt64(lblPlazo.Text);

                        Label lblTasa = (Label)rFila.FindControl("Tasa");
                        if (lblTasa != null && lblTasa.Text != "")
                            pEntidad.tasa = Convert.ToDecimal(lblTasa.Text);

                        Label lblReciprocidad = (Label)rFila.FindControl("lblreciprocidad");
                        if (lblReciprocidad != null && lblReciprocidad.Text != "")
                            pEntidad.reciprocidad = Convert.ToInt32(lblReciprocidad.Text);

                        pEntidad.educativo = Convert.ToInt32(cbeducativo.Checked);

                        Label lblrefinancia = (Label)rFila.FindControl("lblrefinancia");
                        if (lblrefinancia != null && lblrefinancia.Text != "")
                            pEntidad.check = Convert.ToInt32(lblrefinancia.Text);

                        Label lblSaldo = (Label)rFila.FindControl("saldo");
                        if (lblSaldo != null && lblSaldo.Text != "")
                            pEntidad.saldo_capital = Convert.ToDecimal(lblSaldo.Text.Replace(".", ""));

                        Label lblporcentajeauxilio = (Label)rFila.FindControl("lblporcentajeauxilio");
                        if (lblporcentajeauxilio != null && lblporcentajeauxilio.Text != "")
                            pEntidad.porcentaje_auxilio = Convert.ToDecimal(lblporcentajeauxilio.Text);

                        Label lblvalorauxilio = (Label)rFila.FindControl("lblvalorauxilio");
                        if (lblvalorauxilio != null && lblvalorauxilio.Text != "")
                            pEntidad.valor_auxilio = Convert.ToDecimal(lblvalorauxilio.Text);

                        Label lblmanejaauxilio = (Label)rFila.FindControl("lblmanejaauxilio");
                        if (lblmanejaauxilio != null && lblmanejaauxilio.Text != "")
                            pEntidad.maneja_auxilio = Convert.ToInt32(lblmanejaauxilio.Text);

                        lstCredito.Add(pEntidad);                        
                        break;
                    }
                }
            }
            if (cod_linea != null && cod_linea != "")
                crearcreditos.cod_linea_credito = cod_linea;


            // Validar biometria
            String codigoPrograma = personaServicio.CodigoProgramaPreAnalisis;
            string sError = "";
            string Cod_persona = lblCodPersona.Text.Trim();
            if (ctlValidarBiometria.IniciarValidacion(Convert.ToInt32(codigoPrograma), personaServicio.CodigoProgramaPreAnalisis, Convert.ToInt64(Cod_persona), DateTime.Now, ref sError))
            {
                VerError(sError);
                return;
            }

            creditoServicio.CREARCREDITOANALISIS(crearcreditos, (Usuario)Session["usuario"]);

            if (lstCredito.Count > 0)
            {
                Session["DatosGrid"] = lstCredito;
                gvCreditos.DataSource = null;
                gvCreditos.DataSource = lstCredito;
                gvCreditos.DataBind();
            }
            lblMsj.Text = "Datos Grabados Correctamente, si desea puede realizar la Impresión.";
            panelGeneral.Enabled = false;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarConsultar(false);
            toolBar.MostrarGuardar(false);
            toolBar.MostrarImprimir(true);
            btnCalcularCupos.Visible = true;
            btnSolicitar.Visible = true;
            btnSolicitar.Enabled = true;       
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)this.Master;
        toolBar.MostrarCancelar(false);
        toolBar.MostrarGuardar(false);
        toolBar.MostrarImprimir(false);
        toolBar.MostrarConsultar(true);
        mvPreAnalisis.ActiveViewIndex = 0;
        Session.Remove(personaServicio.CodigoProgramaPreAnalisis + ".id");
        Session.Remove("DatosGrid");
        Session.Remove("Origen");
        Session.Remove("Salariominimo");
        panelGeneral.Enabled = true;
        lblMsj.Text = "";
        btnSolicitar.Visible = false;
        btnCalcularCupos.Visible = true;
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            VerError("");
            
            Xpinn.FabricaCreditos.Entities.Persona1 vTercero = new Xpinn.FabricaCreditos.Entities.Persona1();
            vTercero = personaServicio.ConsultarPersona(Convert.ToInt64(pIdObjeto), null, (Usuario)Session["usuario"]);
            Session["DatosPersona"] = vTercero;
            if (!string.IsNullOrEmpty(vTercero.cod_persona.ToString()))
                lblCodPersona.Text = HttpUtility.HtmlDecode(vTercero.cod_persona.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.identificacion))
                txtIdentificacion.Text = HttpUtility.HtmlDecode(vTercero.identificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.nomtipo_identificacion))
                txtTipoIdentificacion.Text = HttpUtility.HtmlDecode(vTercero.nomtipo_identificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.primer_apellido))
                txtApellidos.Text = HttpUtility.HtmlDecode(vTercero.primer_apellido.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.primer_nombre))
                txtNombres.Text = HttpUtility.HtmlDecode(vTercero.primer_nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.direccion))
                txtDireccion.Text = HttpUtility.HtmlDecode(vTercero.direccion.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.telefono))
                txtTelefono.Text = HttpUtility.HtmlDecode(vTercero.telefono.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.oficina))
                txtOficina.Text = HttpUtility.HtmlDecode(vTercero.oficina.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.nomestado))
                txtEstado.Text = HttpUtility.HtmlDecode(vTercero.nomestado.ToString().Trim());

            CalcularMenosSMLMV();
            decimal valortotal = 0;
            if (txtSubTotal.Text != "")
            {
                valortotal = Convert.ToInt64(txtMenosSMLMV.Text.Replace(".", "")) - Convert.ToInt64(txtSubTotal.Text.Replace(".", ""));
            }
            txtDisponible.Text = Convert.ToString(valortotal);
            

            Xpinn.FabricaCreditos.Entities.Persona1 aportes = new Xpinn.FabricaCreditos.Entities.Persona1();
            aportes = personaServicio.consultaraportes(Convert.ToInt64(idObjeto), null, (Usuario)Session["usuario"]);
            if(aportes.SALDOaportes!=null)
                txtAportes.Text = Convert.ToString(aportes.SALDOaportes);
            Xpinn.FabricaCreditos.Entities.Persona1 creditos = new Xpinn.FabricaCreditos.Entities.Persona1();

            creditos = personaServicio.consultarcreditos(Convert.ToInt64(idObjeto), null, (Usuario)Session["usuario"]);
            if (creditos.saldocreditos != null)
                txtCreditos.Text = Convert.ToString(creditos.saldocreditos);

            //consultar si tiene registro en la tabla CREDITO_EMPRESA_RECAUDO
            if (lblCodPersona.Text != "")
            {
                Credito pEntidad = new Credito();
                pEntidad.cod_persona = Convert.ToInt64(lblCodPersona.Text);
                pEntidad = PreAnalisisServ.ConsultarPreAnalisis_credito(pEntidad, (Usuario)Session["usuario"]);
                if (pEntidad != null && pEntidad.idpreanalisis != 0)
                {
                    if (pEntidad.saldo_disponible != 0)
                        txtSaldoDisponible.Text = pEntidad.saldo_disponible.ToString();
                    if (pEntidad.cuota_credito != 0)
                        txtCuotaCreditoCancelado.Text = pEntidad.cuota_credito.ToString();
                    if (pEntidad.cuota_servicios != 0)
                        txtCuotaServicios.Text = pEntidad.cuota_servicios.ToString();
                    if (pEntidad.pago_terceros != 0)
                        txtPagoTerceros.Text = pEntidad.pago_terceros.ToString();
                    if (pEntidad.cuota_otros != 0)
                        txtCuotaOtros.Text = pEntidad.cuota_otros.ToString();
                    if (pEntidad.ingresos_adicionales != 0)
                        txtIngresosAdicionales.Text = pEntidad.ingresos_adicionales.ToString();                   
                }
            }

            List<CreditoRecoger> lstRecoger = new List<CreditoRecoger>();
            CreditoRecogerService recogerServicio = new CreditoRecogerService();
            lstRecoger = recogerServicio.ListarCreditoARecoger(lblCodPersona.Text, DateTime.Now, (Usuario)Session["Usuario"]);
            gvRecoger.DataSource = lstRecoger;
            gvRecoger.DataBind();

            lblTitulo.Visible = false;
            gvCreditos.Visible = false;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarImprimir(false);
            toolBar.MostrarGuardar(true);
            toolBar.MostrarCancelar(true);
            toolBar.MostrarConsultar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(personaServicio.CodigoProgramaPreAnalisis, "ObtenerDatos", ex);
        }
    }

    protected void cbListado_SelectedIndexChanged(object sender, EventArgs e)
    {
        string radicacion = "";
        decimal valor = 0;
        foreach (GridViewRow rfila in gvRecoger.Rows)
        {
            CheckBoxGrid cbListado = (CheckBoxGrid)rfila.FindControl("cbListado");
            if (cbListado != null)
            {
                if (cbListado.Checked)
                {
                    valor += ConvertirStringToDecimal(rfila.Cells[2].Text);
                }
            }
        }
        hfValue.Value = radicacion;
        txtRecoger.Text = valor.ToString("N");
    }

     protected void txtSalariominimo_TextChanged(object sender,EventArgs e)
     {
        CalcularMenosSMLMV();
        decimal suma = Convert.ToDecimal(txtSubTotal.Text.Replace(".", ""));
        decimal deduccion = 0;
        if (txtMenosSMLMV.Text != "")
            deduccion = Convert.ToInt64(txtMenosSMLMV.Text.Replace(".", ""));
        txtDisponible.Text = Convert.ToString(suma - deduccion);
     }

    protected void gvListaTitulares_SelectedIndexChanged(object sender, EventArgs e)
    {
        VerError("");
        GridView gvLista = (GridView)sender;
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        mvPreAnalisis.ActiveViewIndex = 1;
        idObjeto = id;
        ObtenerDatos(idObjeto);
        calcular();
    }

    protected void gvCreditos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        VerError("");
        if (e.CommandName == "Solicitar")
        {            
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument.ToString());
            }
            catch (Exception ex)
            {
                VerError("Error al seleccionar la línea." + ex.Message);
            }            
        }
    }

    protected void btnImprime_Click(object sender, EventArgs e)
    {
        if (rvReporte.Visible == true)
        {
            //MOSTRAR REPORTE EN PANTALLA
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            try
            {
                byte[] bytes = rvReporte.LocalReport.Render("PDF", null, out mimeType,
                               out encoding, out extension, out streamids, out warnings);
                FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output.pdf"),
                FileMode.Create);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
            }
            catch
            {
                VerError("No se pudo imprimir el reporte");
                return;
            }
            Session["Archivo" + Usuario.codusuario] = Server.MapPath("output.pdf");
            frmPrint.Visible = true;
            rvReporte.Visible = false;

        }
    }

    public void btnImprimir_Click(object sender, EventArgs e)
    {
        //codigo de la persona y el otro es codigo de la Oficina
        Configuracion conf = new Configuracion();
        string porcentaje_auxilio = "";
        string valor_auxilio = "";
        string valor_desembolso = "";
        string maneja_auxilio = "";
        VerError("");

        List<Credito> lista = Session["DatosGrid"] != null ? (List<Credito>)Session["DatosGrid"] : new List<Credito>();
        
        DataTable tabla = new DataTable();
        tabla.Columns.Add("LineaCredito");
        tabla.Columns.Add("PréstamoAprobadoPor");
        tabla.Columns.Add("CuotaEstimada");
        tabla.Columns.Add("Plazo");
        tabla.Columns.Add("TasaIntcte");

        foreach (Credito rows in lista)
        {
            DataRow dtrows;
            dtrows = tabla.NewRow();
            dtrows[0] = rows.cod_linea_credito + " " + rows.nom_linea_credito;
            dtrows[1] = rows.monto.ToString("N2");
            dtrows[2] = rows.cuota_credito.ToString("N2");
            dtrows[3] = rows.plazo;
            dtrows[4] = rows.tasa.ToString("N2");
            porcentaje_auxilio = rows.porcentaje_auxilio.ToString("N2");
            valor_auxilio = rows.valor_auxilio.ToString("N0");
            valor_desembolso = (Convert.ToDecimal(txtMonto.Text) - rows.valor_auxilio).ToString("N0");
            maneja_auxilio = rows.maneja_auxilio.ToString("N0");
            tabla.Rows.Add(dtrows);
        }
        Usuario pUsuario = (Usuario)Session["usuario"];
        ReportParameter[] param = new ReportParameter[30];
        Xpinn.FabricaCreditos.Entities.Persona1 vTercero = Session["DatosPersona"] != null ? (Xpinn.FabricaCreditos.Entities.Persona1)Session["DatosPersona"] : null;
        param[0] = new ReportParameter("fecha", DateTime.Now.ToLongDateString());
        param[1] = new ReportParameter("oficina", pUsuario.nombre_oficina);
        param[2] = new ReportParameter("codigo", pUsuario.codusuario.ToString());
        param[3] = new ReportParameter("cedula", vTercero.identificacion);
        param[4] = new ReportParameter("apellidos", vTercero.primer_apellido +" "+ vTercero.segundo_apellido);
        param[5] = new ReportParameter("Nombres", vTercero.primer_nombre + " " + vTercero.segundo_nombre);
        param[6] = new ReportParameter("saldodispo", txtSaldoDisponible.Text);
        param[7] = new ReportParameter("estado", vTercero.estado =="A" ? "Activo": "Inactivo");
        param[8] = new ReportParameter("agencia", pUsuario.nombre_oficina);
        param[9] = new ReportParameter("cuotaCRCancel", txtCuotaCreditoCancelado.Text);
        param[10] = new ReportParameter("cuotaServ", txtCuotaServicios.Text);
        param[11] = new ReportParameter("aporteFech",txtAportes.Text);
        param[12] = new ReportParameter("pagotercer", txtPagoTerceros.Text);
        param[13] = new ReportParameter("creditofecha", txtCreditos.Text);
        param[14] = new ReportParameter("cuotadeotro", txtCuotaOtros.Text);
        param[15] = new ReportParameter("capitalizava", txtCapitaliza.Text);
        param[16] = new ReportParameter("ingresosAdcn", txtIngresosAdicionales.Text);
        decimal pSubTotal = 0,pTotal = 0,smlv = 0;
        if (txtSubTotal.Text != "")
            pSubTotal = Convert.ToDecimal(txtSubTotal.Text);
        param[17] = new ReportParameter("subtotal", pSubTotal.ToString("N2"));
        param[18] = new ReportParameter("desmbolRecipro", "0");
        param[19] = new ReportParameter("entidad", pUsuario.empresa);
        param[20] = new ReportParameter("usuario", pUsuario.nombre);
        if (txtDisponible.Text != "")
            pTotal = Convert.ToDecimal(txtDisponible.Text);
        param[21] = new ReportParameter("totaldisponible", pTotal.ToString("N2"));
        param[22] = new ReportParameter("monto_solicitud", txtMonto.Text);
        param[23] = new ReportParameter("plazo_solicitud", txtPlazo.Text);
        if(txtMenosSMLMV.Text != "")
            smlv = Convert.ToDecimal(txtMenosSMLMV.Text);
        param[24] = new ReportParameter("menos_smlv", smlv.ToString("N2"));
        param[25] = new ReportParameter("porcentaje_auxilio", porcentaje_auxilio);
        param[26] = new ReportParameter("valor_auxilio", valor_auxilio);
        param[27] = new ReportParameter("valor_desembolso", valor_desembolso);
        param[28] = new ReportParameter("esEducativo", cbeducativo.Checked ? "True" : "False");
        param[29] = new ReportParameter("maneja_auxilio", maneja_auxilio);

        rvReporte.LocalReport.SetParameters(param);

        ReportDataSource rds = new ReportDataSource("DataSet1", tabla);
        rvReporte.LocalReport.DataSources.Clear();
        rvReporte.LocalReport.DataSources.Add(rds);
        rvReporte.LocalReport.Refresh();
        mvPreAnalisis.ActiveViewIndex = 2;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarConsultar(false);
        toolBar.MostrarGuardar(false);
        toolBar.MostrarImprimir(false);
    }

    protected void btnDatos_click(object sender, EventArgs e) 
    {
        mvPreAnalisis.ActiveViewIndex =1;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarConsultar(false);
        toolBar.MostrarGuardar(false);
        toolBar.MostrarImprimir(true);
    }

    protected void CalcularCupos()
    {
        if (lblCodPersona.Text.Trim() == "")
            return;
        try
        {
            // Datos del pre-análisis
            Int64 pCodPersona = Convert.ToInt64(lblCodPersona.Text);
            decimal pDisponible = ConvertirStringToDecimal(txtDisponible.Text);
            decimal pMontoSolicitado = ConvertirStringToDecimal(txtMonto.Text);
            Int64 pNumeroCuotas = 0;
            if (txtPlazo.Text.Trim() != "")
                pNumeroCuotas = Convert.ToInt64(txtPlazo.Text);
            Xpinn.FabricaCreditos.Services.CreditoService creditoServicio = new CreditoService();
            List<Credito> lstCredito = new List<Credito>();
            lstCredito = creditoServicio.RealizarPreAnalisis(true,DateTime.Now, pCodPersona, pDisponible, pNumeroCuotas, pMontoSolicitado, Convert.ToInt32(ddlPeriodicidad.SelectedValue), cbeducativo.Checked, (Usuario)Session["usuario"]);
            if (lstCredito.Count <= 0)
            {
                Credito credito = new Credito();
                credito.cod_linea_credito = "";
                credito.cod_persona = 0;
                credito.monto = 0;
                lstCredito.Add(credito);
            }
            lblTitulo.Visible = true;
            gvCreditos.PageIndex = 0;
            gvCreditos.Visible = true;
            gvCreditos.DataSource = lstCredito;
            gvCreditos.DataBind();
            for (int i = 0; i < gvCreditos.Columns.Count; i++)
            {
                if (gvCreditos.Columns[i].HeaderText.Contains("Auxilio"))
                {
                    if (cbeducativo.Checked)
                        gvCreditos.Columns[i].Visible = true;
                    else
                        gvCreditos.Columns[i].Visible = false;
                }
            }
            Session["DatosGrid"] = lstCredito;            
            Site toolBar = (Site)this.Master;
            toolBar.MostrarImprimir(false);
            toolBar.MostrarGuardar(true);
            toolBar.MostrarCancelar(true);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(personaServicio.CodigoProgramaPreAnalisis, "ObtenerDatos", ex);
        }
    }

    protected void btnCalcularCupos_Click(object sender, EventArgs e)
    {
        VerError("");
        if (txtMonto.Text.Trim() == "" || txtMonto.Text.Trim() == "0")
        {
            VerError("Ingrese el monto solicitado.");
            txtMonto.Focus();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
            return;            
        }
        if (txtPlazo.Text.Trim() == "" || txtPlazo.Text.Trim() == "0")
        {
            VerError("Ingrese el plazo solicitado.");
            txtPlazo.Focus();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
            return;            
        }
        if (ddlPeriodicidad.SelectedItem == null || ddlPeriodicidad.SelectedIndex == 0)
        {
            VerError("Seleccione la periodicidad.");
            ddlPeriodicidad.Focus();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
            return;
        }
        CalcularCupos();
    }

    protected void gvCreditos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (e.NewPageIndex >= 0)
            {
                gvCreditos.PageIndex = e.NewPageIndex;
                if (Session["DatosGrid"] != null)
                {
                    List<Credito> lstCredito = new List<Credito>();
                    lstCredito = (List<Credito>)Session["DatosGrid"];
                    gvCreditos.DataSource = lstCredito;
                    gvCreditos.DataBind();
                }
                else
                {
                    CalcularCupos();
                }
            }            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(personaServicio.CodigoPrograma, "gvCreditos_PageIndexChanging", ex);
        }
    }

    protected void gvCreditos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbmonto = (Label)e.Row.FindControl("lbmonto");
            if (lbmonto != null)
            {
                string sMonto = lbmonto.Text.Replace(gSeparadorMiles, "");
                if (sMonto == "0,00")
                {
                    CheckBoxGrid chkSeleccionar = (CheckBoxGrid)e.Row.FindControl("chkSeleccionar");
                    if (chkSeleccionar != null)
                        chkSeleccionar.Visible = false;
                }
            }
            Label lblmanejaauxilio = (Label)e.Row.FindControl("lblmanejaauxilio");
            if (lblmanejaauxilio != null)
            {
                if (lblmanejaauxilio.Text != "1")
                {
                    Label lblporcentajeauxilio = (Label)e.Row.FindControl("lblporcentajeauxilio");
                    if (lblporcentajeauxilio != null)
                        lblporcentajeauxilio.Visible = false;
                    Label lblvalorauxilio = (Label)e.Row.FindControl("lblvalorauxilio");
                    if (lblvalorauxilio != null)
                        lblvalorauxilio.Visible = false;
                    Label lblsimbolo = (Label)e.Row.FindControl("lblsimbolo");
                    if (lblsimbolo != null)
                        lblsimbolo.Visible = false;
                }
            }
        }
    }


    protected void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBoxGrid chkSeleccionar = (CheckBoxGrid)sender;
            int rowIndex = Convert.ToInt32(chkSeleccionar.CommandArgument);
            if (chkSeleccionar != null)
            {
                foreach (GridViewRow rFila in gvCreditos.Rows)
                {
                    CheckBoxGrid chkSelec = (CheckBoxGrid)rFila.FindControl("chkSeleccionar");
                    if (chkSelec != null)
                    {
                        chkSelec.Checked = false;
                        if (rFila.RowIndex == rowIndex)
                            chkSelec.Checked = true;
                    }
                }
            }            
        }
        catch
        {        
        }
    }

    protected void btnSolicitar_Click(object sender, EventArgs e)
    {
        // Cargando los datos en variables de sesión para poder generar la solicitud
        GridViewRow rfila = (GridViewRow)gvCreditos.Rows[0];
        if (rfila != null)
        {
            if (cbeducativo.Checked == true)
            {
                Session[personaServicio.CodigoProgramaCreditoE + ".id"] = txtIdentificacion.Text;
                Session["Origen"] = "CEL";
            }
            if (cbeducativo.Checked == false)
            {
                Session[personaServicio.CodigoPrograma + ".id"] = txtIdentificacion.Text;
                Session["Origen"] = "SDCL";                
            }
            // Cargando la línea
            Label lblinea_credito = (Label)rfila.FindControl("lblinea_credito");
            if (lblinea_credito != null)
            {
                Session[personaServicio.CodigoPrograma + ".linea"] = lblinea_credito.Text;
            }
            // Cargando el monto
            Label lbmonto = (Label)rfila.FindControl("lbmonto");
            if (lbmonto != null)
            {
                string sMonto = txtMonto.Text.Replace(gSeparadorMiles, "");
                Session[personaServicio.CodigoPrograma + ".monto"] = Convert.ToInt64(sMonto.Replace(".", ""));  // Se convierte a entero para quitarle los décimales.
            }
            // Cargando el plazo
            if (txtPlazo.Text == "")
            {
                Label Plazo = (Label)rfila.FindControl("Plazo");
                if (Plazo != null)
                {
                    Session[personaServicio.CodigoPrograma + ".plazo"] = Convert.ToInt64(Plazo.Text);
                }
            }
            else
            {
                Session[personaServicio.CodigoPrograma + ".plazo"] = Convert.ToInt64(txtPlazo.Text);
            }
            // Cargando la cuota
            Label Cuota = (Label)rfila.FindControl("Cuota");
            if (Cuota != null)
            {
                string sCuota = Cuota.Text.Replace(gSeparadorMiles, "");
                Session[personaServicio.CodigoPrograma + ".cuota"] = Convert.ToInt64(sCuota.Remove(sCuota.Length - 3, 3));  // Se convierte a entero para quitarle los décimales.
            }
            // Cargando la periodicidad
            if (ddlPeriodicidad.SelectedItem != null && ddlPeriodicidad.SelectedIndex != 0)
            {
                string sPeriodicidad = ddlPeriodicidad.SelectedValue;
                Session[personaServicio.CodigoPrograma + ".periodicidad"] = sPeriodicidad;  // Se convierte a entero para quitarle los décimales.
            }
            // Determinar el tipo de crédito
            ClasificacionCredito clasificacion = new ClasificacionCredito();
            LineasCreditoService lineasser = new LineasCreditoService();
            LineasCredito linea = new LineasCredito();
            linea = lineasser.ConsultaLineaCredito(lblinea_credito.Text, (Usuario)Session["Usuario"]);

            switch (linea.cod_clasifica)
            {
                case 0:
                    clasificacion = ClasificacionCredito.Consumo;
                    break;
                case 1:
                    clasificacion = ClasificacionCredito.MicroCredito;
                    break;
                case 2:
                    clasificacion = ClasificacionCredito.Vivienda;
                    break;
                case 3:
                    clasificacion = ClasificacionCredito.Comercial;
                    break;
            }
            Session["TipoCredito"] = clasificacion;
        }

        Session.Remove(personaServicio.CodigoProgramaPreAnalisis + ".id");
        if (Session["Origen"] == "CEL")
            Navegar("~/Page/FabricaCreditos/DatosCliente/DatosCliente.aspx");
        if (Session["Origen"] == "SDCL")
            Navegar("~/Page/FabricaCreditos/DatosCliente/DatosCliente.aspx");
    }

    void focoEdicion(TextBox txt)
    {
        System.Web.UI.ScriptManager scriptManager = System.Web.UI.ScriptManager.GetCurrent(this.Page);
        scriptManager.SetFocus(txt);
    }

    protected void txtSaldoDisponible_TextChanged(object sender, EventArgs e)
    {
        try
        {
            focoEdicion(txtCuotaCreditoCancelado);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void txtCuotaCreditoCancelado_TextChanged(object sender, EventArgs e)
    {
        try
        {
            focoEdicion(txtCuotaServicios);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }
    protected void txtCuotaServicios_TextChanged(object sender, EventArgs e)
    {
        try
        {
            focoEdicion(txtPagoTerceros);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void txtPagoTerceros_TextChanged(object sender, EventArgs e)
    {
        try
        {
            focoEdicion(txtCuotaOtros);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void txtCuotaOtros_TextChanged(object sender, EventArgs e)
    {
        try
        {
            focoEdicion(txtIngresosAdicionales);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void txtIngresosAdicionales_TextChanged(object sender, EventArgs e)
    {
        try
        {
            focoEdicion(txtIngresosAdicionales);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void txtMenosSMLMV_TextChanged(object sender, EventArgs e)
    {
        try
        {
            focoEdicion(txtMenosSMLMV);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void txtMonto_TextChanged(object sender, EventArgs e)
    {
        try
        {
            focoEdicion(txtPlazo);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }
    protected void txtPlazo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ddlPeriodicidad.Focus();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void txtSaldoDisponible_PreRender(object sender, EventArgs e)
    {
        Pre_render(txtSaldoDisponible);
    }

    protected void txtCuotaCreditoCancelado_PreRender(object sender, EventArgs e)
    {
        Pre_render(txtCuotaCreditoCancelado);
    }

    protected void txtCuotaServicios_PreRender(object sender, EventArgs e)
    {
        Pre_render(txtCuotaServicios);
    }

    protected void txtPagoTerceros_PreRender(object sender, EventArgs e)
    {
        Pre_render(txtPagoTerceros);
    }

    protected void txtCuotaOtros_PreRender(object sender, EventArgs e)
    {
        Pre_render(txtCuotaOtros);
    }

    protected void txtIngresosAdicionales_PreRender(object sender, EventArgs e)
    {
        Pre_render(txtIngresosAdicionales);
    }

    protected void txtMenosSMLMV_PreRender(object sender, EventArgs e)
    {
        Pre_render(txtMenosSMLMV);
    }

    protected void txtMonto_PreRender(object sender, EventArgs e)
    {
        Pre_render(txtMonto);
    }

    protected void txtPlazo_PreRender(object sender, EventArgs e)
    {
        Pre_render(txtPlazo);
    }

    protected void Pre_render(TextBox txtValor)
    {     
        string str = txtValor.Text ;
        string formateado = "";

        string s = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        if (s == ".")
            str = str.Replace(",", "");
        else
        {
            str = str.Replace(".", "");
            str = str.Replace(",", ".");
        }

        try
        {
            if (str != "" &&  str.ToLower() != "null" && Convert.ToInt64(str) > 0)
            {

                var strI = Convert.ToInt64(str);  //Convierte a entero y luego a string para quitar ceros a la izquierda
                str = strI.ToString();

                if (str.Length > 12)
                { str = str.Substring(0, 12); }

                int longi = str.Length;
                string milmill = "";
                string mill = "";
                string mil = "";
                string cen = "";


                if (longi > 0 && longi <= 3)
                {
                    cen = str.Substring(0, longi);
                    formateado = Convert.ToInt64(cen).ToString();
                }
                else if (longi > 3 && longi <= 6)
                {
                    mil = str.Substring(0, longi - 3);
                    cen = str.Substring(longi - 3, 3);
                    formateado = Convert.ToInt64(mil) + "." + cen;
                }
                else if (longi > 6 && longi <= 9)
                {
                    mill = str.Substring(0, longi - 6);
                    mil = str.Substring(longi - 6, 3);
                    cen = str.Substring(longi - 3, 3);
                    formateado = Convert.ToInt64(mill) + "." + mil + "." + cen;
                }
                else if (longi > 9 && longi <= 12)
                {
                    milmill = str.Substring(0, longi - 9);
                    mill = str.Substring(longi - 9, 3);
                    mil = str.Substring(longi - 6, 3);
                    cen = str.Substring(longi - 3, 3);
                    formateado = Convert.ToInt64(milmill) + "." + mill + "." + mil + "." + cen;
                }
                else
                { formateado = "0"; }
            }
            else { if (str.ToLower() != "null") formateado = "0"; else formateado = "0"; }

            txtValor.Text = formateado.ToString();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }    
    }

    protected void cbeducativo_CheckedChanged(object sender, EventArgs e)
    {
        // Actualizar la gridView si ya se generaron datos
        if (gvCreditos.Visible == true && gvCreditos.Rows.Count > 0)
        {
            btnCalcularCupos_Click(btnCalcularCupos, e);
        }
    }
}