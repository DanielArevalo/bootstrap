using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.CDATS.Services;
using Xpinn.CDATS.Entities;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;


partial class Nuevo : GlobalWeb
{
    LiquidacionCDATService LiquiService = new LiquidacionCDATService();
    AperturaCDATService AperturaService = new AperturaCDATService();
    PoblarListas Poblar = new PoblarListas();
    LineaCDAT vLineaCdat = new LineaCDAT();
    LineaCDATService lineacdatServicio = new LineaCDATService();
    private Xpinn.CDATS.Services.LineaCDATService lineaCDATServicio = new Xpinn.CDATS.Services.LineaCDATService();


    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[AperturaService.CodigoProgramaCierre + ".id"] != null)
                VisualizarOpciones(AperturaService.CodigoProgramaCierre, "E");
            else
                VisualizarOpciones(AperturaService.CodigoProgramaCierre, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarImprimir(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            txtFechaCierre.eventoCambiar += txtFechaCierre_TextChanged;
            ctlTasa.eventoCambiar += txtTasa_TextChanged;
            ctlTasa.Inicializar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.CodigoProgramaCierre, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["DatosDetalle"] = null;
           
            if (!Page.IsPostBack)
            {
                mvPrincipal.ActiveViewIndex = 0;
                cargarDropdown();

                Usuario vUsu = (Usuario)Session["usuario"];
                ddlOficina.SelectedValue = vUsu.cod_oficina.ToString();
                txtFechaCierre.Texto = DateTime.Now.ToShortDateString();

                if (Session[AperturaService.CodigoProgramaCierre + ".id"] != null)
                {
                    idObjeto = Session[AperturaService.CodigoProgramaCierre + ".id"].ToString();
                    Session.Remove(AperturaService.CodigoProgramaCierre + ".id");
                    ObtenerDatos(idObjeto);
                  //  pnlCuentaAhorroVista.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.CodigoProgramaCierre, "Page_Load", ex);
        }
    }



    void cargarDropdown()
    {
         try
        {
        ctlGiro.Inicializar();
        Cdat Data = new Cdat();
        List<Cdat> lstTipoLinea = new List<Cdat>();

        lstTipoLinea = AperturaService.ListarTipoLineaCDAT(Data,(Usuario)Session["usuario"]);
        if (lstTipoLinea.Count > 0)
        {
       
            ddlTipoLinea.DataSource = lstTipoLinea;
            ddlTipoLinea.DataTextField = "NOMBRE";
            ddlTipoLinea.DataValueField = "COD_LINEACDAT";
            ddlTipoLinea.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlTipoLinea.SelectedIndex = 0;
            ddlTipoLinea.DataBind();
        }
        
        Poblar.PoblarListaDesplegable("Tipomoneda", ddlTipoMoneda,(Usuario)Session["usuario"]);

        ddlTipoCalendario.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlTipoCalendario.Items.Insert(1, new ListItem("Comercial", "1"));
        ddlTipoCalendario.Items.Insert(2, new ListItem("Calendario", "2"));
        ddlTipoCalendario.SelectedIndex = 0;
        ddlTipoCalendario.DataBind();

      
       
        Poblar.PoblarListaDesplegable("OFICINA","COD_OFICINA,NOMBRE","ESTADO = 1","1",ddlOficina,(Usuario)Session["usuario"]);        
        Poblar.PoblarListaDesplegable("PERIODICIDAD", "COD_PERIODICIDAD,DESCRIPCION", "", "to_number(cod_periodicidad)", ddlPeriodicidad, (Usuario)Session["usuario"]);
        }
         catch (Exception ex)
         {
             BOexcepcion.Throw(AperturaService.GetType().Name + "L", "CargarListas", ex);
         }
       
    }


    protected void chkPrincipal_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxGrid chkPrincipal = (CheckBoxGrid)sender;
        int rowIndex = Convert.ToInt32(chkPrincipal.CommandArgument);

        if (chkPrincipal != null)
        {
            foreach (GridViewRow rFila in gvDetalle.Rows)
            {
                CheckBoxGrid check = (CheckBoxGrid)rFila.FindControl("chkPrincipal");
                check.Checked = false;
                if (rFila.RowIndex == rowIndex)
                {
                    check.Checked = true;
                }
            }
        }
    }

    protected List<Detalle_CDAT> ObtenerListaDetalle()
    {
        List<Detalle_CDAT> lstDetalle = new List<Detalle_CDAT>();
        List<Detalle_CDAT> lista = new List<Detalle_CDAT>();

        foreach (GridViewRow rfila in gvDetalle.Rows)
        {
            Detalle_CDAT eDeta = new Detalle_CDAT();

            string pCodigo = rfila.Cells[0].Text;
            if (pCodigo != null && pCodigo != "&nbsp;")
                eDeta.cod_usuario_cdat = Convert.ToInt64(pCodigo);

            string pIdentificacion = rfila.Cells[1].Text;
            if (pIdentificacion != null && pIdentificacion != "&nbnsp;")
                eDeta.identificacion = pIdentificacion;

            string pCod_persona = rfila.Cells[2].Text;
            if (pCod_persona != null && pCod_persona != "&nbsp;")
                eDeta.cod_persona = Convert.ToInt64(pCod_persona);

            CheckBoxGrid chkPrincipal = (CheckBoxGrid)rfila.FindControl("chkPrincipal");
            if (chkPrincipal.Checked)
                eDeta.principal = 1;
            else
                eDeta.principal = 0;

            lista.Add(eDeta);
            Session["DatosDetalle"] = lista;

            if (eDeta.cod_persona != 0 && eDeta.cod_persona != null)
            {
                lstDetalle.Add(eDeta);
            }
        }

        return lstDetalle;
    }


  
    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Cdat vApe = new Cdat();

            if (Session[AperturaService.CodigoProgramaCierre + ".ov"]!=null)
            {
                vApe = AperturaService.ConsultarApertuXnumcdat(Convert.ToString(idObjeto), (Usuario)Session["usuario"]);
            }
           else
            {
            vApe = AperturaService.ConsultarApertu(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            }


            if (vApe.codigo_cdat != 0) txtCodigo.Text = vApe.codigo_cdat.ToString();

            if (vApe.numero_cdat != "")
            {
                txtNumCDAT.Text = vApe.numero_cdat;
                // txtDigVerif.Text = CalcularDigitoVerificacion(txtNumCDAT.Text);
            }

            if (vApe.fecha_apertura != DateTime.MinValue) txtFechaApertura.Text = vApe.fecha_apertura.ToShortDateString();

            if (vApe.cod_lineacdat != null) ddlTipoLinea.SelectedValue = vApe.cod_lineacdat;

            if (vApe.cod_oficina != 0) ddlOficina.SelectedValue = vApe.cod_oficina.ToString();

            if (vApe.valor != 0) txtValor.Text = vApe.valor.ToString();

            if (vApe.cod_moneda != 0) ddlTipoMoneda.SelectedValue = vApe.cod_moneda.ToString();

            if (vApe.plazo != 0) txtPlazo.Text = vApe.plazo.ToString();

            if (vApe.tipo_calendario != 0) ddlTipoCalendario.SelectedValue = vApe.tipo_calendario.ToString();

            if (vApe.fecha_intereses != null) txtfechaInt.Text = vApe.fecha_intereses.ToShortDateString();

            // panelTasa.Enabled = false;
            //PanelGiro.Enabled = true;
            //RECUPERAR GRILLA DETALLE 
            List<Detalle_CDAT> lstDetalle = new List<Detalle_CDAT>();

      

            lstDetalle = AperturaService.ListarDetalleTitulares(Convert.ToInt64(vApe.codigo_cdat), (Usuario)Session["usuario"]);

            if (lstDetalle.Count > 0)
            {
                gvDetalle.Visible = true;
                gvDetalle.DataSource = lstDetalle;
                gvDetalle.DataBind();
               // panelTitualres.Enabled = false;
            }
            else
            {
                gvDetalle.Visible = false;
                gvDetalle.DataSource = null;
            }

            if (vApe.tipo_interes != null)
            {
                ctlTasa.FormaTasa = vApe.tipo_interes;
                if (ctlTasa.Indice == 0)//NIGUNA
                {
                }
                else if (ctlTasa.Indice == 1)//FIJO
                {
                    if (vApe.tasa_interes != 0)
                        ctlTasa.Tasa = vApe.tasa_interes;
                    if (vApe.cod_tipo_tasa != 0)
                        ctlTasa.TipoTasa = vApe.cod_tipo_tasa;
                }
                else // HISTORICO
                {
                    if (vApe.tipo_historico != 0)
                        ctlTasa.TipoHistorico = Convert.ToInt32(vApe.tipo_historico);
                    if (vApe.desviacion != 0)
                        ctlTasa.Desviacion = Convert.ToInt32(vApe.desviacion);
                }
            }
            // ctlTasa.EnableViewState = false;

            if (vApe.modalidad_int != 0)
                rblModalidadInt.SelectedValue = vApe.modalidad_int.ToString();

            // Consultar si la periodicidad no es de la cuenta si no de la linea        
            // vLineaCdat = lineacdatServicio.ConsultarLineaCDAT(Convert.ToString(ddlTipoLinea.SelectedValue), (Usuario)Session["usuario"]);

            if (vApe.cod_periodicidad_int != 0)
                ddlPeriodicidad.SelectedValue = vApe.cod_periodicidad_int.ToString();
            else
                lblperiodicidad.Visible = false;
            ddlPeriodicidad.Visible = false;


            Int64 COD_PERSONA = 0;

            COD_PERSONA = Buscar_Titular();
            ctlGiro.cargarCuentasAhorro(COD_PERSONA);

           
            if (vApe.fecha_vencimiento > Convert.ToDateTime(txtFechaCierre.Text))
            {
                VerError("Fecha Cierre menor a fecha vencimiento , Puede modificar Tasa Interes.");
                txtTasaNew.BackColor = System.Drawing.Color.AliceBlue;
                LblNewTasa.Visible = true;
                txtTasaNew.Visible = true;
                ctlTasa.Enabled_Tasa(true);
                txtTasaNew.Focus();
                Xpinn.CDATS.Entities.LineaCDAT vLineaCDAT = new Xpinn.CDATS.Entities.LineaCDAT();
                vLineaCDAT = lineaCDATServicio.ConsultarLineaCDAT(Convert.ToString(ddlTipoLinea.SelectedValue), (Usuario)Session["usuario"]);
                ctlTasa.Tasa = vLineaCDAT.tasa_ven != null ? Decimal.Parse(vLineaCDAT.tasa_ven.ToString()) : 0;
            }
            else
            {
                ctlTasa.Enabled_Tasa(false);
                txtTasaNew.BackColor = System.Drawing.Color.AliceBlue;
                LblNewTasa.Visible = false;
                txtTasaNew.Visible = false;
                txtTasaNew.Focus();


            }

            /* pnlCuentaAhorroVista.Visible = false;
             AhorroVistaServices ahorroServices = new AhorroVistaServices();
             List<AhorroVista> lstAhorros = ahorroServices.ListarCuentaAhorroVista(Convert.ToInt64(COD_PERSONA), Usuario);
             ddlCuentaAhorroVista.DataSource = lstAhorros;
             ddlCuentaAhorroVista.DataTextField = "numero_cuenta";
             ddlCuentaAhorroVista.DataValueField = "numero_cuenta";
             ddlCuentaAhorroVista.DataBind();
             */

            txtValorLiquidacion.Text = "";
            txtIntCapitaliza.Text = "";
            txtMasInteres.Text = "";
            txtMenosRetencion.Text = "";
            txtMenosGMF.Text = "";
            txtTotalPagar.Text = "";

            LiquidarCDAT();


            if (Session["solicitud"] != null)
            {
                AhorroVista solicitud = new AhorroVista();
                solicitud = Session["solicitud"] as AhorroVista;
                
                ctlGiro.ValueFormaDesem = solicitud.forma_giro.ToString();
                if (solicitud.forma_giro == 3)
                {
                    ctlGiro.ValueEntidadDest = solicitud.cod_banco.ToString();
                    ctlGiro.TextNumCuenta = solicitud.numero_cuenta_final.ToString();
                    ctlGiro.ValueTipoCta = solicitud.tipo_cuenta.ToString();
                }
            }


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.CodigoProgramaCierre, "ObtenerDatos", ex);
        }
    }


    protected void LiquidarCDAT()
    {
        LiquidacionCDAT pLiqui = new LiquidacionCDAT();
        LiquidacionCDAT entidad = new LiquidacionCDAT();
        //PanelGiro.Enabled = true;
        if(txtFechaCierre.Texto == "")
        {
            VerError("Ingrese la fecha de Cierre para realizar la Liquidación.");
            return;
        }
        pLiqui.fecha_liquidacion = Convert.ToDateTime(txtFechaCierre.Texto);
        pLiqui.numero_cdat = txtNumCDAT.Text;
        pLiqui.valor = 0;
        pLiqui.interes_causado = 0;      
        pLiqui.retencion = 0;
        pLiqui.valor_gmf = 0;
        pLiqui.valor_pagar = 0;
        txtValorLiquidacion.Text = "";
        txtIntCapitaliza.Text = "";
        txtMasInteres.Text = "";
        txtMenosRetencion.Text = "";
        txtMenosGMF.Text = "";
        txtTotalPagar.Text = "";
        //cierre de cdats 
        pLiqui.origen = 1;

        entidad = LiquiService.CalculoLiquidacionCDAT(pLiqui, (Usuario)Session["usuario"]);
       

        if (entidad.valor != 0)
            txtValorLiquidacion.Text = entidad.valor.ToString();
        if (entidad.interes_causado != 0)
            txtIntCapitaliza.Text = entidad.interes_causado.ToString();
        if (entidad.interes != 0)
            txtMasInteres.Text = entidad.interes.ToString();
        if (entidad.retencion != 0)
            txtMenosRetencion.Text = entidad.retencion.ToString();
        if (entidad.valor_gmf != 0)
            txtMenosGMF.Text = entidad.valor_gmf.ToString();
        if (entidad.valor_pagar != 0)
            txtTotalPagar.Text = entidad.valor_pagar.ToString();
    }

    protected void txtFechaCierre_TextChanged(object sender, EventArgs e)
    {
        try
        {
            LiquidarCDAT();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("Cierre", "txtFechaCierre_TextChanged", ex);
        }
    }

    protected void txtTasa_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //ctlTasa.Tasa = Convert.ToDecimal(txtTasaNew.Text);
            LiquidarCDAT();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("Cierre", "txtFechaCierre_TextChanged", ex);
        }
    }



    protected void txtTasaNEW_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ctlTasa.Tasa = Convert.ToDecimal(txtTasaNew.Text);
            Xpinn.CDATS.Entities.LineaCDAT vLineaCDAT = new Xpinn.CDATS.Entities.LineaCDAT();
            vLineaCDAT = lineaCDATServicio.ConsultarLineaCDAT(Convert.ToString(ddlTipoLinea.SelectedValue), (Usuario)Session["usuario"]);
            vLineaCDAT.tasa_ven = ctlTasa.Tasa;
            lineaCDATServicio.ModificarLineaCDAT(vLineaCDAT, (Usuario)Session["usuario"]);
            LiquidarCDAT();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("Cierre", "txtFechaCierre_TextChanged", ex);
        }
    }


    Boolean ValidarDatos()
    {
        if (txtFechaCierre.Texto == "")
        {
            VerError("Ingrese la fecha de Cierre");
            return false;
        }

        if (txtNumCDAT.Visible == true)
        {
            if (txtNumCDAT.Text == "")
            {
                VerError("Ingrese el numero de CDAT");
                return false;
            }
        }
        if (txtPlazo.Text == "")
        {
            VerError("Ingrese el Plazo correspondiente");
            return false;
        }
        if (ddlTipoCalendario.SelectedIndex == 0)
        {
            VerError("Seleccione el Tipo de Calendario");
            return false;
        }
        //if (ddlOficina.SelectedIndex == 0)
        //{
        //    VerError("Seleccione la Oficina perteneciente al Asesor");
        //    return false;
        //}
        //if (ddlPeriodicidad.SelectedIndex == 0)
        //{
        //    VerError("Seleccione la Periodicidad correspondiente");
        //    return false;
        //}
        bool pGenerarGiro = false;
        List<Detalle_CDAT> LstDetalle = new List<Detalle_CDAT>();
        LstDetalle = ObtenerListaDetalle();
        int cont = 0;
        Detalle_CDAT pVar = new Detalle_CDAT();
        if (LstDetalle.Count > 0)
        {
            foreach (Detalle_CDAT deta in LstDetalle)
            {
                if (deta.principal == 1)
                {
                    cont++;
                }
            }
            if (cont != 1)
            {
                VerError("Debe selecciona un titular principal");
                return false;
            }
        }

        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
        DateTime dtUltCierre;
        try
        {
            dtUltCierre = Convert.ToDateTime(ComprobanteServicio.Consultafecha((Usuario)Session["Usuario"]));
        }
        catch
        {
            VerError("No se encontro la fecha del último cierre contable");
            return false;
        }

        if (Convert.ToDateTime(txtFechaCierre.Texto) <= dtUltCierre)
        {
            VerError("La fecha de Cierre ingresada debe ser mayor a la fecha del último Cierre generado ('"+ dtUltCierre.ToShortDateString() +"')");
            return false;
        }

        //Validando datos del control de Giro
        if (ctlGiro.IndiceFormaDesem == 0)
        {
            VerError("Seleccione una forma de desembolso");
            return false;
        }
        else
        {
            if (ctlGiro.IndiceFormaDesem == 2 || ctlGiro.IndiceFormaDesem == 3)
            {
                if (ctlGiro.IndiceEntidadOrigen == 0)
                {
                    VerError("Seleccione un Banco de donde se girará");
                    return false;
                }
                if (ctlGiro.IndiceFormaDesem == 3)
                {
                    if (ctlGiro.IndiceEntidadDest == 0)
                    {
                        VerError("Seleccione la Entidad de destino");
                        return false;
                    }
                    if (ctlGiro.TextNumCuenta == "")
                    {
                        VerError("Ingrese el número de la cuenta");
                        return false;
                    }
                }
                if (ctlGiro.IndiceFormaDesem == 4)
                {
                    pGenerarGiro = false;

                    pVar.numero_cuenta_ahorro_vista = !string.IsNullOrWhiteSpace(ctlGiro.ValueCuentaAhorro) ? Convert.ToInt64(ctlGiro.ValueCuentaAhorro) : default(long?);

                    if (!pVar.numero_cuenta_ahorro_vista.HasValue)
                    {
                        VerError("No tiene una cuenta de ahorro a la vista asociada para esta forma de pago!.");

                    }
                }
            }
        }

        Int64 COD = Buscar_Titular();
        if (COD == 0)
        {
            VerError("Error al realizar la búsqueda, No se ubico al titular");
            return false;
        }
        return true;
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            if (ValidarDatos())
            {
                ctlMensaje.MostrarMensaje("Desea generar el cierre?");
            }
        }       
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.CodigoProgramaCierre, "btnGuardar_Click", ex);
        }
    }

    protected Int64 Buscar_Titular()
    {
        Int64 codigo = 0;
        int cont = 0;
        foreach (GridViewRow rFila in gvDetalle.Rows)
        {
            CheckBoxGrid chkPrincipal = (CheckBoxGrid)rFila.FindControl("chkPrincipal");
            if (chkPrincipal.Checked)
                cont++;

            if (cont == 1)
            {
                string cod = "";
                cod = rFila.Cells[2].Text.Replace("&nbsp;","");
                if (cod != "")
                    codigo = Convert.ToInt64(cod);
                Session["Titular"] = codigo;
                break;
            }
        }
        return codigo;
    }

    
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
        Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
        Detalle_CDAT pVar = new Detalle_CDAT();
        bool pGenerarGiro = false;
        try
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["usuario"];

            //CONSULTAR CIERRE HISTORICO
            String estado = "";
            DateTime fechacierrehistorico;
            DateTime fechaliquidacion = Convert.ToDateTime(txtFechaCierre.Texto);
            Xpinn.CDATS.Entities.LiquidacionCDAT vliquidacioncdat = new Xpinn.CDATS.Entities.LiquidacionCDAT();
            vliquidacioncdat = LiquiService.ConsultarCierreCdats((Usuario)Session["usuario"]);
            estado = vliquidacioncdat.estadocierre;
            fechacierrehistorico = Convert.ToDateTime(vliquidacioncdat.fecha_cierre.ToString());

            if (estado == "D" && fechaliquidacion <= fechacierrehistorico)
            {
                VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO M,'CDAT'S'");
                return;
            }

            //DATOS DE LA OPERACION
            Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
            vOpe.cod_ope = 0;
            vOpe.tipo_ope = 11;
            vOpe.cod_caja = 0;
            vOpe.cod_cajero = 0;
            vOpe.observacion = "Operacion-Cierre CDAT";
            vOpe.cod_proceso = null;
            vOpe.fecha_oper = Convert.ToDateTime(txtFechaCierre.Texto);
            vOpe.fecha_calc = DateTime.Now;
            vOpe.cod_ofi = pUsuario.cod_oficina;

            //DATOS DEL TITULAR
            Int64 COD_OPE = 0, COD_PERSONA = 0;
            COD_PERSONA = Buscar_Titular();

            //GRABACION DEL GIRO A REALIZAR
            Xpinn.FabricaCreditos.Services.AvanceService AvancServices = new Xpinn.FabricaCreditos.Services.AvanceService();
            Usuario pusu = (Usuario)Session["usuario"];
            Xpinn.FabricaCreditos.Entities.Giro pGiro = new Xpinn.FabricaCreditos.Entities.Giro();
            pGiro.idgiro = 0;
            pGiro.cod_persona = COD_PERSONA;
            pGiro.forma_pago = Convert.ToInt32(ctlGiro.ValueFormaDesem);
            pGiro.tipo_acto = 7;
            pGiro.fec_reg = Convert.ToDateTime(txtFechaCierre.Texto);
            pGiro.fec_giro = DateTime.Now;
            pGiro.numero_radicacion = 0;
            pGiro.usu_gen = pusu.nombre;
            pGiro.usu_apli = null;
            pGiro.estadogi = 0;
            pGiro.usu_apro = null;          
            if (ctlGiro.IndiceFormaDesem == 1) //"EFECTIVO"
            {
                pGiro.idctabancaria = 0;
                pGiro.cod_banco = 0;
                pGiro.num_cuenta = null;
                pGiro.tipo_cuenta = -1;
                pGenerarGiro = true;
            }
            if (ctlGiro.IndiceFormaDesem != 1 && ctlGiro.IndiceFormaDesem != 4) //"EFECTIVO"
            {
                //DETERMINAR LA IDENTIFICACIÓN DE LA CUENTA BANCARIA
                CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ctlGiro.ValueEntidadOrigen), ctlGiro.TextCuentaOrigen, (Usuario)Session["usuario"]);
                Int64 idCta = CuentaBanc.idctabancaria;
                //DATOS DE FORMA DE PAGO
                if (ctlGiro.IndiceFormaDesem == 3) //"Transferencia"
                {
                    pGiro.idctabancaria = idCta;
                    pGiro.cod_banco = Convert.ToInt32(ctlGiro.ValueEntidadDest);
                    pGiro.num_cuenta = ctlGiro.TextNumCuenta;
                    pGiro.tipo_cuenta = Convert.ToInt32(ctlGiro.ValueTipoCta);
                    pGenerarGiro = true;
                }
                else if (ctlGiro.IndiceFormaDesem == 2) //Cheque
                {
                    pGiro.idctabancaria = idCta;
                    pGiro.cod_banco = 0;        //NULO
                    pGiro.num_cuenta = null;    //NULO
                    pGiro.tipo_cuenta = -1;      //NULO
                    pGenerarGiro = true;
                }
                else if(ctlGiro.IndiceFormaDesem ==4)
                {
                    pGenerarGiro = false;
                    pVar.numero_cuenta_ahorro_vista = !string.IsNullOrWhiteSpace(ctlGiro.ValueCuentaAhorro) ? Convert.ToInt64(ctlGiro.ValueCuentaAhorro) : default(long?);
                    if (!pVar.numero_cuenta_ahorro_vista.HasValue)
                    {
                        VerError("No tiene una cuenta de ahorro a la vista asociada para esta forma de pago!.");
                    }
                }
                else
                {
                    pGiro.idctabancaria = 0;
                    pGiro.cod_banco = 0;
                    pGiro.num_cuenta = null;
                    pGiro.tipo_cuenta = -1;
                }
            }
            pGiro.fec_apro = DateTime.MinValue;
            pGiro.cob_comision = 0;
            pGiro.valor = Convert.ToInt64(this.txtTotalPagar.Text.Replace(".", ""));

            //DATOS DE CIERRE LIQUIDACION
            LiquidacionCDAT pLiqui = new LiquidacionCDAT();
            pLiqui.fecha_liquidacion = Convert.ToDateTime(txtFechaCierre.Texto); //FECHA DE CIERRE
            pLiqui.numero_cdat = txtNumCDAT.Text;
            pLiqui.valor = Convert.ToDecimal(txtValorLiquidacion.Text.Replace(".",""));
            //pLiqui.interes_causado = Convert.ToDecimal(txtIntCapitaliza.Text.Replace(".",""));
            pLiqui.interes = Convert.ToDecimal(txtMasInteres.Text.Replace(".",""));
            pLiqui.retencion = Convert.ToDecimal(txtMenosRetencion.Text.Replace(".",""));
            pLiqui.valor_gmf = Convert.ToDecimal(txtMenosGMF.Text.Replace(".",""));
            pLiqui.valor_pagar = Convert.ToInt32(txtTotalPagar.Text.Replace(".", ""));
            pLiqui.cod_deudor = Convert.ToInt64(COD_PERSONA);

            //CUENTA DE AHORROS PARA GIRO
            if (ctlGiro.ValueCuentaAhorro != "")
            {
                pLiqui.numero_cuenta_ahorro_vista = Convert.ToInt64(ctlGiro.ValueCuentaAhorro);
            }

            string pError = string.Empty;

            pLiqui.capitalizar_int = 0;
            // pLiqui.valor_pagar = Convert.ToDecimal(pLiqui.valor + pLiqui.interes_causado + pLiqui.interes + pLiqui.retencion);
            LiquiService.CierreLiquidacionCDAT(ref COD_OPE, ref pError, vOpe, pGenerarGiro, Convert.ToInt64(ctlGiro.IndiceFormaDesem), pGiro, pLiqui, (Usuario)Session["usuario"]);
            if (!string.IsNullOrEmpty(pError))
            {
                VerError(pError);
                return;
            }
            //GENERAR EL COMPROBANTE
            if (COD_OPE != 0)
            {
                if (Session["solicitud"] != null)
                {
                    AhorroVista solicitud = new AhorroVista();
                    solicitud = Session["solicitud"] as AhorroVista;

                    if (string.IsNullOrWhiteSpace(solicitud.id_solicitud.ToString()))
                    {
                        solicitud.nom_estado = "1"; // Aprobar Solicitud
                        AhorroVistaServices _ahorrosService = new AhorroVistaServices();
                        _ahorrosService.ModificarEstadoSolicitud(solicitud, (Usuario)Session["usuario"]);
                    }
                }
                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = COD_OPE;
                Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 11;
                Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = DateTime.ParseExact(txtFechaCierre.Texto, gFormatoFecha, null);
                Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = COD_PERSONA;
                Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.CodigoProgramaCierre, "btnContinuarMen_Click", ex);
        }    
    }



    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        if (Session["solicitud"] != null)
        {
            Session["solicitud"] = null;
            Response.Redirect("../../AhorrosVista/ConfirmarRetiroAprobado/Lista.aspx", false);
        }else
            Navegar(Pagina.Lista);
    }

   
}