using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;


partial class Detalle : GlobalWeb
{

    AvanceService AvancServices = new AvanceService();
    CreditoService CreditoServicio = new CreditoService();
    DatosPlanPagosService datosServicio = new DatosPlanPagosService();
  
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AvancServices.CodigoProgramaAprobacion, "E");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AvancServices.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Permite modificar cierta información cuando es perfil ADMINISTRADOR.
            bool bHabilitar = false;
            if (((Usuario)Session["usuario"]).codperfil == 1)
                bHabilitar = true;

            gvDeducciones.Enabled = bHabilitar;
            if (!IsPostBack)
            {               

                txtFechaAproSoli.Text = Convert.ToString(DateTime.Now);
                CargarDropdown();
                mvAplicar.ActiveViewIndex = 0;
                txtNumero.Enabled = false;
                panelDatos.Enabled = false;
                txtFechaSoli.Enabled = false;
                txtValorSoli.Enabled = false;
                panel1.Enabled = false;


                if (Session[AvancServices.CodigoProgramaAprobacion + ".id"] != null)
                {
                    idObjeto = Session[AvancServices.CodigoProgramaAprobacion + ".id"].ToString();
                    Session.Remove(AvancServices.CodigoProgramaAprobacion + ".id");
                    ObtenerDatos(idObjeto);                   
                }

               // ActivarDesembolso(idObjeto);// ddlForma_Desem_SelectedIndexChanged(ddlForma_Desem, null);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AvancServices.GetType().Name + "L", "Page_Load", ex);
        }

    }


    protected void ddlForma_Desem_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  ActivarDesembolso(idObjeto);
        Session.Remove("numavanace");
        Session.Remove("numcredito");
       
    }


    //void ActivarDesembolso(String pIdObjeto)
    //{
    //    //Recuperar Forma de Pago
    //    Giro vGiro = new Giro();
    //    vGiro = AvancServices.ConsultarFormaDesembolso(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

    //  //  ddlForma_Desem.SelectedValue = vGiro.forma_pago.ToString();
    //    if (vGiro.idctabancaria != 0)
    //    {
    //        CuentaBanc = bancoService.ConsultarCuentasBancarias(Convert.ToInt32(vGiro.idctabancaria), (Usuario)Session["usuario"]);
    //        if (CuentaBanc.cod_banco != 0)
    //        {
    //            ddlEntidad_giro.SelectedValue = CuentaBanc.cod_banco.ToString();
    //            ddlEntidad_giro_SelectedIndexChanged(ddlEntidad_giro, null);
    //            ddlCuenta_Giro.SelectedValue = vGiro.idctabancaria.ToString();
    //        }
    //    }
    //    if (vGiro.cod_banco != 0)
    //        ddlEntidad.SelectedValue = vGiro.cod_banco.ToString();
    //    if (vGiro.num_cuenta != "")
    //        txtNum_cuenta.Text = vGiro.num_cuenta;
    //    if (vGiro.tipo_cuenta != null)
    //        ddlTipo_cuenta.SelectedValue = vGiro.tipo_cuenta.ToString();


           
    //    ///

    //    if (ddlForma_Desem.SelectedItem.Text == "Transferencia")
    //    {

    //        ///

    //        ActividadesServices ActividadServicio = new ActividadesServices();
    //        List<CuentasBancarias> LstCuentasBanc = new List<CuentasBancarias>();
    //        Int64 cod = Convert.ToInt64(Session["codigocliente"]);
    //        string filtro = " and Principal = 1";
    //        LstCuentasBanc = ActividadServicio.ConsultarCuentasBancarias(cod, filtro, (Usuario)Session["usuario"]);

    //        if (LstCuentasBanc.Count > 0 && LstCuentasBanc.Count == 1)
    //        {

    //            panelFormaPago.Visible = true;
    //            lblEntidadGiro.Visible = true;
    //            ddlEntidad_giro.Visible = true;
    //            ddlCuenta_Giro.Visible = true;
    //            lblCuenta_Giro.Visible = true;
    //            txtNum_cuenta.Visible = true;
    //            lblNum_cuenta.Visible = true;
    //            ddlEntidad.Visible = true;
    //            lblEntidad.Visible = true;
    //            ddlTipo_cuenta.Visible = true;
    //            lblTipo_Cuenta.Visible = true;

    //            if (Session["LISTA"] != null)
    //            {
    //                List<CuentasBancarias> lstCuenta = (List<CuentasBancarias>)Session["LISTA"];
    //                if (lstCuenta[0].cod_banco != null && lstCuenta[0].cod_banco != 0)
    //                  ddlEntidad.SelectedValue = lstCuenta[0].cod_banco.ToString();
    //                if (lstCuenta[0].numero_cuenta != null && lstCuenta[0].numero_cuenta != "")
    //                 txtNum_cuenta.Text = lstCuenta[0].numero_cuenta;
    //                if (lstCuenta[0].tipo_cuenta != null && lstCuenta[0].tipo_cuenta != 0)
    //                {
    //                    try
    //                    {
    //                        ddlTipo_cuenta.SelectedValue = lstCuenta[0].tipo_cuenta.ToString();
    //                    }
    //                    catch { }
    //                }
    //            }
    //        }
    //    }

    //    else if (ddlForma_Desem.SelectedItem.Text == "Efectivo")
    //    {
    //        panelFormaPago.Visible = false;
    //    }
    //    else if (ddlForma_Desem.SelectedItem.Text == "Cheque")
    //    {
    //        panelFormaPago.Visible = true;
    //        lblEntidadGiro.Visible = true;
    //        ddlEntidad_giro.Visible = true;
    //        ddlCuenta_Giro.Visible = true;
    //        lblCuenta_Giro.Visible = true;
    //        txtNum_cuenta.Visible = false;
    //        lblNum_cuenta.Visible = false;
    //        ddlEntidad.Visible = false;
    //        lblEntidad.Visible = false;
    //        ddlTipo_cuenta.Visible = false;
    //        lblTipo_Cuenta.Visible = false;
    //    }
    //    else
    //    {
    //        panelFormaPago.Visible = false;
    //    }
    //}

    void CargarDropdown()
    {

        ctlTasaInteres.Inicializar();


       // ddlForma_Desem.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
       //ddlForma_Desem.Items.Insert(1, new ListItem("Efectivo", "1"));
        //ddlForma_Desem.Items.Insert(2, new ListItem("Cheque", "2"));
        //ddlForma_Desem.Items.Insert(3, new ListItem("Transferencia", "3"));
        //ddlForma_Desem.SelectedIndex = 0;
        //ddlForma_Desem.DataBind();
        
        ddlTipo_cuenta.Items.Insert(0, new ListItem("Ahorros", "0"));
        ddlTipo_cuenta.Items.Insert(1, new ListItem("Corriente", "1"));
        ddlTipo_cuenta.SelectedIndex = 1;
        ddlTipo_cuenta.DataBind();

        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
        ddlEntidad_giro.DataSource = bancoService.ListarBancosEntidad(banco, (Usuario)Session["usuario"]);
        ddlEntidad_giro.DataTextField = "nombrebanco";
        ddlEntidad_giro.DataValueField = "cod_banco";
        ddlEntidad_giro.DataBind();

        PoblarLista("bancos", ddlEntidad);
        CargarCuentas();
       

    }


    void CargarCuentas()
    {
        Int64 codbanco = 0;
        try
        {
            codbanco = Convert.ToInt64(ddlEntidad_giro.SelectedValue);
        }
        catch
        {
        }
        if (codbanco != 0)
        {
            Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
            Usuario usuario = (Usuario)Session["usuario"];
            ddlCuenta_Giro.DataSource = bancoService.ListarCuentaBancos(codbanco, usuario);
            ddlCuenta_Giro.DataTextField = "num_cuenta";
            ddlCuenta_Giro.DataValueField = "idctabancaria";
            ddlCuenta_Giro.DataBind();
        }
    }


    protected void ddlEntidad_giro_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarCuentas();
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
   
    

    protected void ObtenerDatos(String pIdObjeto2)
    {
        try
        {
            Avance vDetalle = new Avance();
            Avance vDetalle1 = new Avance();
            Avance vDetalleTasa = new Avance();
            Avance vDetallePlazo = new Avance();
            Avance vDetallePlazoMax = new Avance();

            Credito vCredito = new Credito();
            pIdObjeto2 =Convert.ToString(Session["numavanace"]);
            String pIdCredito = Convert.ToString(Session["numcredito"]);
            vDetalle = AvancServices.ConsultarCredRotativoXaprobar(Convert.ToInt64(pIdObjeto2), (Usuario)Session["usuario"]);
            
            vDetalleTasa = AvancServices.ConsultarTasaCreditoTotativo(Convert.ToInt64(pIdCredito), (Usuario)Session["usuario"]);


            
            // Mostrar los datos de descuentos del crédito
            gvDeducciones.DataSource = vDetalle.lstDescuentosCredito;
            gvDeducciones.DataBind();


            if (vDetalle.nomlinea != "")
                txtNomLinea.Text = vDetalle.nomlinea;
            if(vDetalle.numero_radicacion != 0)
                txtNumCredito.Text = vDetalle.numero_radicacion.ToString().Trim();
            if (vDetalle.cupototal != 0)// valor o monto aprobado
                txtCupoTotal.Text = vDetalle.cupototal.ToString();
            if (vDetalle.cupodisponible != 0)
                txtCupoDisp.Text = vDetalle.cupodisponible.ToString();
            if (vDetalle.nomoficina !="")
                txtOficina.Text = vDetalle.nomoficina;
            if (vDetalle.fecha_aprobacion != DateTime.MinValue)
                txtFechaApro.Text = vDetalle.fecha_aprobacion.ToShortDateString();
            String idObjeto2 = pIdCredito;
            vDetalle1 = AvancServices.fecha_ult_avance(Convert.ToInt64(idObjeto2), (Usuario)Session["usuario"]);

                if (vDetalle1.fecha_ult_avance != DateTime.MinValue)
                 txtFechaUlt.Text = vDetalle1.fecha_ult_avance.ToShortDateString();

            if (vDetalle.descforma_pago != "")
                txtFormaPago.Text = vDetalle.descforma_pago;
            if (txtFormaPago.Text == "Caja")
                txtnumFormPago.Text = "1";
            else if (txtFormaPago.Text == "Nomina")
                txtnumFormPago.Text = "2";
            if (vDetalle.cuotas_pagadas == 0 || vDetalle.cuotas_pagadas > 0)
                this.txtCuotasPagas.Text = Convert.ToString(vDetalle.cuotas_pagadas);


            if (vDetalle.identificacion != "")
                txtIdentificacion.Text = vDetalle.identificacion;
            if (vDetalle.nombre != "")
                txtNombre.Text = vDetalle.nombre;
            
            if (vDetalle.fecha_solicitud != DateTime.MinValue)
                txtFechaSoli.Text = vDetalle.fecha_solicitud.ToShortDateString();
            if (vDetalle.valor_solicitado != 0)
                txtValorSoli.Text = vDetalle.valor_solicitado.ToString();
            if (vDetalle.idavance != 0)
                txtNumero.Text = vDetalle.idavance.ToString();
            txtValorApro.Text = vDetalle.valor_solicitado.ToString();

            //Recuperar Forma de Pago
            Giro vGiro = new Giro();
            vGiro = AvancServices.ConsultarFormaDesembolso(Convert.ToInt64(pIdCredito), (Usuario)Session["usuario"]);

          //  ddlForma_Desem.SelectedValue = vDetalle.forma_pago.ToString();
            if (vGiro.idctabancaria != 0)
            {
                CuentaBanc = bancoService.ConsultarCuentasBancarias(Convert.ToInt32(vGiro.idctabancaria), (Usuario)Session["usuario"]);
                if (CuentaBanc.cod_banco != 0)
                {
                    ddlEntidad_giro.SelectedValue = CuentaBanc.cod_banco.ToString();
                    ddlEntidad_giro_SelectedIndexChanged(ddlEntidad_giro, null);
                    ddlCuenta_Giro.SelectedValue = vGiro.idctabancaria.ToString();
                }
            }
            if (vGiro.cod_banco != 0)
                ddlEntidad.SelectedValue = vGiro.cod_banco.ToString();
            if (vGiro.num_cuenta != "")
                txtNum_cuenta.Text = vGiro.num_cuenta;
            if (vGiro.tipo_cuenta != null)
                ddlTipo_cuenta.SelectedValue = vGiro.tipo_cuenta.ToString();


            vDetallePlazo = AvancServices.ConsultarPlazoCreditoTotativo(Convert.ToString(txtNomLinea.Text), (Usuario)Session["usuario"]);
            vDetallePlazoMax = AvancServices.ConsultarPlazoMaximoCredito(Convert.ToInt64(pIdCredito), (Usuario)Session["usuario"]);



            if (vDetallePlazo.diferir == 1)
            {
                txtPlazo.Enabled = true;
                txtPlazo.Text = Convert.ToString(vDetalle.plazo_diferir);

            }

            if (vDetallePlazo.diferir == 0 && vDetalle.plazo_diferir == 0)
            {
                txtPlazo.Enabled = false;
                txtPlazo.Text = vDetallePlazoMax.plazo_maximo.ToString().Trim();
            }
            if (vDetallePlazo.diferir == 0 || vDetalle.plazo_diferir > 0)
            {
                txtPlazo.Text= Convert.ToString(vDetalle.plazo_diferir);

            }


            // para la tasa


            if (vDetalleTasa.calculo_atr != null)
            {
                if (!string.IsNullOrEmpty(vDetalleTasa.calculo_atr.ToString()))
                    ctlTasaInteres.FormaTasa = HttpUtility.HtmlDecode(vDetalleTasa.calculo_atr.ToString().Trim());
                if (!string.IsNullOrEmpty(vDetalleTasa.tipo_historico.ToString()))
                    ctlTasaInteres.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(vDetalleTasa.tipo_historico.ToString().Trim()));
                if (!string.IsNullOrEmpty(vDetalleTasa.desviacion.ToString()))
                    ctlTasaInteres.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(vDetalleTasa.desviacion.ToString().Trim()));
                if (!string.IsNullOrEmpty(vDetalleTasa.tipo_tasa.ToString()))
                    ctlTasaInteres.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(vDetalleTasa.tipo_tasa.ToString().Trim()));
                if (!string.IsNullOrEmpty(vDetalleTasa.tasa.ToString()))
                    ctlTasaInteres.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(vDetalleTasa.tasa.ToString().Trim()));
            }



        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AvancServices.CodigoProgramaAprobacion, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    public Boolean ValidarDatos()
    {
        
        if (chkAprobar.Checked == false && chkNegar.Checked == false)
        {
            VerError("Seleccione una opción de la Aprobación");
            return false;
        }
        if (txtFechaApro.Text == "")
        {
            VerError("Ingrese fecha de Aprobación");
            return false;
        }
        if (txtValorApro.Text == "")
        {
            VerError("Ingrese el valor Aprobado");
            return false;
        }
        if (Convert.ToDateTime(txtFechaAproSoli.Text) < Convert.ToDateTime(txtFechaSoli.Text))
        {
            VerError("No puede generar la aprobación en una fecha menor de la que fue solicitada");
            return false;
        }
        //if (ddlForma_Desem.SelectedIndex == 0)
        //{
           // VerError("Seleccione la forma de desembolso");
           // return false;
      //  }

        decimal avance = Convert.ToDecimal(txtValorApro.Text);
        decimal valorsolicitado = Convert.ToDecimal(txtValorSoli.Text);
        decimal cupodisponible = Convert.ToDecimal(txtCupoDisp.Text);
        if (avance > cupodisponible)
        {
            VerError("El valor Aprobado del avance supera el cupo total o disponible");
            return false;
        }

        if (avance > valorsolicitado)
        {
            VerError("El valor Aprobado del avance supera el Valor solicitado");
            return false;
        }
        if (chkAprobar.Checked == true && txtValorApro.Text == "0")
        {
            VerError("Digite un monto a aprobar");
            return false;
        }

         return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");       
        if (ValidarDatos())
        {
            string msj="";
            if(chkAprobar.Checked == true)
                msj = "Aprobar";
            if (chkNegar.Checked == true)
                msj = "Anular";
            lblmsj.Text = "Modificada";
            ctlMensaje.MostrarMensaje("Desea "+msj+" la solicitud de Crédito de Avance?");          
        }
    }


    Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
    Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
           
             // Cargar atributos descontados/financiados a modificar del crédito
            Avance vCredito = new Avance();
            vCredito.lstDescuentosCredito = new List<DescuentosCredito>();
            foreach (GridViewRow gFila in gvDeducciones.Rows)
            {
                try
                {
                    DescuentosCredito eDescuento = new DescuentosCredito();
                    eDescuento.numero_radicacion = vCredito.numero_radicacion;
                    eDescuento.cod_atr = ValorSeleccionado((Label)gFila.FindControl("lblCodAtr"));
                    eDescuento.nom_atr = Convert.ToString(gFila.Cells[1].ToString());
                    eDescuento.tipo_descuento = ValorSeleccionado((DropDownList)gFila.FindControl("ddlTipoDescuento"));
                    eDescuento.tipo_liquidacion = ValorSeleccionado((DropDownList)gFila.FindControl("ddlTipoLiquidacion"));
                    eDescuento.forma_descuento = Convert.ToInt32(ValorSeleccionado((DropDownList)gFila.FindControl("ddlFormaDescuento")));
                    eDescuento.val_atr = ValorSeleccionado((TextBox)gFila.FindControl("txtvalor"));
                    eDescuento.numero_cuotas = ValorSeleccionado((TextBox)gFila.FindControl("txtnumerocuotas"));
                    eDescuento.cobra_mora = ValorSeleccionado((CheckBox)gFila.FindControl("cbCobraMora"));
                    vCredito.lstDescuentosCredito.Add(eDescuento);
                    eDescuento.numero_radicacion = Convert.ToInt64(txtNumCredito.Text);
                    vCredito = CreditoServicio.ModificarDescuentos(vCredito, (Usuario)Session["usuario"]);
                 
                }
                catch (Exception ex)
                {
                    VerError("Se presentaron errores al cargar atributos descontados/financiados a modificar del crédito. Error:" + ex.Message);
                    return;
                }
                
            }
            GenerarAprobacion();
      
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AvancServices.CodigoProgramaAprobacion, "btnContinuar_Click", ex);
        }
      
        
    }
    protected void GenerarAprobacion()
    {
        Credito vCredito = new Credito();
        //Int64 cuota = 0;
        Credito credito = new Credito();
        //credito.numero_radicacion = Convert.ToInt64(txtNumCredito.Text);
        Avance pVar = new Avance();
        //// MODIFICAR EL VALOR DE LA CUOTA 
            
        vCredito.numero_radicacion = Convert.ToInt64(txtNumCredito.Text);
        List<DatosPlanPagos> lstConsulta = new List<DatosPlanPagos>();
        lstConsulta = datosServicio.ListarDatosPlanPagos(vCredito, (Usuario)Session["usuario"]);
        decimal cCampo;
        foreach (DatosPlanPagos dFila in lstConsulta)
        {
            try
            {
                cCampo = Convert.ToDecimal(dFila.total.ToString().Trim());
                Session["valor"] = cCampo;
            }
            catch
            {
            }           
        }
        pVar.numero_radicacion = Convert.ToInt64(txtNumCredito.Text);
        pVar.valor_cuota = Convert.ToDecimal(Session["valor"]);
        pVar = AvancServices.ModificarCuota(pVar, (Usuario)Session["usuario"]);
        
        
        // aprobar avance
          if (txtNumero.Text != "")
            pVar.idavance = Convert.ToInt32(txtNumero.Text);
        else
            pVar.idavance = 0;

        pVar.numero_radicacion = Convert.ToInt64(txtNumCredito.Text);
        pVar.fecha_solicitud = Convert.ToDateTime(txtFechaSoli.Text);
        pVar.fecha_aprobacion = Convert.ToDateTime(txtFechaAproSoli.Text);
        pVar.fecha_desembolso = DateTime.MinValue;
        pVar.valor_solicitado = Convert.ToDecimal(txtValorSoli.Text);
        pVar.valor_aprobado = Convert.ToDecimal(txtValorApro.Text);
        pVar.valor_desembolsado = 0;
        pVar.forma_pago = Convert.ToInt32(txtnumFormPago.Text);

        if (chkAprobar.Checked)
            pVar.estado = "A";
        else if (chkNegar.Checked)
            pVar.estado = "N";

        if (txtObserva.Text != "")
            pVar.observacion = txtObserva.Text;
        else
            pVar.observacion = null;


      
        if (idObjeto != "")
        {
            //MODIFICAR
            AvancServices.ModificarCreditoAvance(pVar, (Usuario)Session["usuario"]);
        }


        //Giro vDetalle = new Giro();
        //vDetalle = AvancServices.ConsultarFormaDesembolso(pVar.numero_radicacion, (Usuario)Session["usuario"]);
        ////GUARDANDO GIRO
        //Usuario pusu = (Usuario)Session["usuario"];
        //Giro pGiro = new Giro();
        //if (vDetalle.idgiro != 0) pGiro.idgiro = vDetalle.idgiro;
        //if (vDetalle.cod_persona != 0) pGiro.cod_persona = vDetalle.cod_persona;
        //if (vDetalle.forma_pago != 0) pGiro.forma_pago = vDetalle.forma_pago;
        //if (vDetalle.tipo_acto != 0) pGiro.tipo_acto = vDetalle.tipo_acto;
        //if (vDetalle.fec_reg != DateTime.MinValue) pGiro.fec_reg = DateTime.Now;
        //pGiro.fec_giro = DateTime.MinValue;

        //if (vDetalle.numero_radicacion != 0) pGiro.numero_radicacion = Convert.ToInt64(txtNumCredito.Text);
        //if (vDetalle.usu_gen != "" && vDetalle.usu_gen != null) pGiro.usu_gen = vDetalle.usu_gen;
        //pGiro.usu_apli = null;
        //pGiro.estadogi = 1;
        //pGiro.usu_apro = pusu.nombre;
        //pGiro.fec_apro = DateTime.Now;

        //CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ddlEntidad_giro.SelectedValue), ddlCuenta_Giro.SelectedItem.Text, (Usuario)Session["usuario"]);
        //Int64 idCta = CuentaBanc.idctabancaria;

        //if (ddlForma_Desem.SelectedItem.Text == "Transferencia")
        //{
        //    pGiro.idctabancaria = idCta;
        //    pGiro.cod_banco = Convert.ToInt32(ddlEntidad.SelectedValue);
        //    pGiro.num_cuenta = txtNum_cuenta.Text;
        //    pGiro.tipo_cuenta = Convert.ToInt32(ddlTipo_cuenta.SelectedValue);
        //}
        //else if (ddlForma_Desem.SelectedItem.Text == "Cheque")
        //{
        //    pGiro.idctabancaria = idCta;
        //    pGiro.cod_banco = 0; //NULO
        //    pGiro.num_cuenta = null; //NULO
        //    pGiro.tipo_cuenta = -1; //NULO
        //}
        //else
        //{
        //    pGiro.idctabancaria = 0;
        //    pGiro.cod_banco = 0;
        //    pGiro.num_cuenta = null;
        //    pGiro.tipo_cuenta = -1;
        //}
        //pGiro.cob_comision = 0;
        //pGiro.valor = Convert.ToInt64(txtValorApro.Text.Replace(".", ""));

       // AvancServices.CrearGiro(pGiro, (Usuario)Session["usuario"], 2); //MODIFICAR


        //GRABACION DE CONTROL DE CREDITOS
        ControlCreditos pcont = new ControlCreditos();
        pcont.numero_radicacion = Convert.ToInt64(txtNumCredito.Text);
        pcont.codtipoproceso = "2";
        pcont.fechaproceso = Convert.ToDateTime(txtFechaApro.Text);
        //pControl.cod_persona = 0;
        pcont.cod_motivo = 0;
        if (txtObserva.Text != "")
            pcont.observaciones = txtObserva.Text;
        else
            pcont.observaciones = null;

        pcont.anexos = null;
        pcont.nivel = 0;
        pcont.fechaconsulta_dat = DateTime.MinValue;
        AvancServices.CrearControlCreditos(pcont, (Usuario)Session["usuario"]);

        Session[AvancServices.CodigoProgramaAprobacion + ".id"] = idObjeto;
        mvAplicar.ActiveViewIndex = 1;
       

    }


    protected Int32? ValorSeleccionado(DropDownList ddlControl)
    {
        if (ddlControl != null)
            if (ddlControl.SelectedValue != null)
                if (ddlControl.SelectedValue != "")
                    return Convert.ToInt32(ddlControl.SelectedValue);
        return null;
    }

    protected decimal? ValorSeleccionado(TextBox txtControl)
    {
        if (txtControl != null)
            if (txtControl.Text != null)
                if (txtControl.Text != "")
                    return ConvertirStringToDecimal(txtControl.Text);
        return null;
    }

    protected Int32? ValorSeleccionado(Label txtControl)
    {
        if (txtControl != null)
            if (txtControl.Text != null)
                if (txtControl.Text != "")
                    return Convert.ToInt32(txtControl.Text);
        return null;
    }

    protected int ValorSeleccionado(CheckBox txtControl)
    {
        if (txtControl != null)
            if (txtControl.Checked != null)
                return Convert.ToInt32(txtControl.Checked);
        return 0;
    }


    protected void chkNegar_CheckedChanged(object sender, EventArgs e)
    {
        if (chkNegar.Checked)
            chkAprobar.Checked = false;
    }


    protected void chkAprobar_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAprobar.Checked)
            chkNegar.Checked = false;
    }
    protected void ddlForma_Desem_TextChanged(object sender, EventArgs e)
    {
        //ActivarDesembolso(idObjeto);
    }

    protected List<Xpinn.Comun.Entities.ListasFijas> ListarTiposdeDecuento()
    {
        GlobalWeb glob = new GlobalWeb();
        return glob.ListaCreditoTipoDeDescuento();
    }

    protected List<Xpinn.Comun.Entities.ListasFijas> ListaCreditoTipoDeLiquidacion()
    {
        GlobalWeb glob = new GlobalWeb();
        return glob.ListaCreditoTipoDeLiquidacion();
    }

    protected List<Xpinn.Comun.Entities.ListasFijas> ListaCreditoFormadeDescuento()
    {
        GlobalWeb glob = new GlobalWeb();
        return glob.ListaCreditoFormadeDescuento();
    }

    protected List<Xpinn.Comun.Entities.ListasFijas> ListaImpuestos()
    {
        List<Xpinn.Comun.Entities.ListasFijas> lstImpuestos = new List<Xpinn.Comun.Entities.ListasFijas>();
        lstImpuestos.Add(new Xpinn.Comun.Entities.ListasFijas { codigo = "", descripcion = "" });
        lstImpuestos.Add(new Xpinn.Comun.Entities.ListasFijas { codigo = "0", descripcion = "" });
        return lstImpuestos;
    }

}
