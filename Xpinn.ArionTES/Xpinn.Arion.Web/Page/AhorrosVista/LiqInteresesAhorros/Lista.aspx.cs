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
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;

partial class Lista : GlobalWeb
{
    private Xpinn.Ahorros.Services.AhorroVistaServices Ahorroservicio = new Xpinn.Ahorros.Services.AhorroVistaServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(Ahorroservicio.CodigoProgramaLiq, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Ahorroservicio.CodigoProgramaLiq, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ctlGiro.Visible = false;
        try
        {
            if (!IsPostBack)
            {
                txtFecha.Text = DateTime.Now.ToString(gFormatoFecha);
                cargarListaddl();
                LimpiarValoresConsulta(pConsulta, Ahorroservicio.CodigoProgramaLiq);
                btnExportar.Visible = false;
                CargarValoresConsulta(pConsulta, Ahorroservicio.CodigoProgramaLiq);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Ahorroservicio.CodigoProgramaLiq, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, Ahorroservicio.CodigoProgramaLiq);
        Actualizar();
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        ctlMensaje.MostrarMensaje("Desea guardar los datos de la liquidación de interes?");
    }

    private List<ELiquidacionInteres> ObtenerListaGrilla()
    {
        List<ELiquidacionInteres> lstLista = new List<ELiquidacionInteres>();

        foreach (GridViewRow rFila in gvLista.Rows)
        {
            ELiquidacionInteres vData = new ELiquidacionInteres();
            if (rFila.Cells[0].Text != "" && rFila.Cells[0].Text != "&nbsp;")//numero_cuenta
                vData.numero_cuenta = Convert.ToString(rFila.Cells[0].Text);
            else
                vData.numero_cuenta = "";



            if (rFila.Cells[1].Text != "" && rFila.Cells[1].Text != "&nbsp;")//cod_persona
                vData.Cod_Usuario = Convert.ToInt64(rFila.Cells[1].Text);
            Session["Identificacion"] = vData.Cod_Usuario;

            if (rFila.Cells[2].Text != "" && rFila.Cells[2].Text != "&nbsp;")//IDENTIFICACION
                vData.Identificacion = Convert.ToString(rFila.Cells[2].Text);



            if (rFila.Cells[3].Text != "" && rFila.Cells[3].Text != "&nbsp;")//NOMBRE TITULAR
                vData.Nombre = rFila.Cells[3].Text;

            if (rFila.Cells[4].Text != "" && rFila.Cells[4].Text != "&nbsp;")//VALOR
            {
                vData.Saldo = Convert.ToDecimal(rFila.Cells[4].Text);
                vData.valor_Neto = Convert.ToDecimal(rFila.Cells[4].Text);
            }

            if (rFila.Cells[5].Text != "" && rFila.Cells[5].Text != "&nbsp;")//TASA
                vData.Tasa_interes = Convert.ToDecimal(rFila.Cells[5].Text);

            vData.fecha_int = Convert.ToDateTime(txtFecha.Texto);
            vData.fecha_liquidacion = Convert.ToDateTime(txtFecha.Texto);

            if (rFila.Cells[7].Text != "" && rFila.Cells[7].Text != "&nbsp;")//INTERES
                vData.Interes = Convert.ToDecimal(rFila.Cells[7].Text);
            else
                vData.Interes = 0;

            if (rFila.Cells[8].Text != "" && rFila.Cells[8].Text != "&nbsp;")//RETENCION
                vData.Retefuente = Convert.ToDecimal(rFila.Cells[8].Text);
            else
                vData.Retefuente = 0;

            if (rFila.Cells[9].Text != "" && rFila.Cells[9].Text != "&nbsp;")//INTERES causada
                vData.Interescausado = Convert.ToDecimal(rFila.Cells[9].Text);
            else
                vData.Interescausado = 0;


            if (rFila.Cells[10].Text != "" && rFila.Cells[10].Text != "&nbsp;")//RETENCION causada
                vData.retencion_causado = Convert.ToDecimal(rFila.Cells[10].Text);
            else
                vData.retencion_causado = 0;

            vData.valor_gmf = 0;

            vData.valor_Neto = 0;

            vData.interes_capitalizado = 0;

            vData.forma_pago = "0";

            vData.cta_ahorros = "0";

            Session["Valor"] = vData.Interes + vData.Interescausado - vData.Retefuente - vData.retencion_causado;


            if (vData.numero_cuenta != "" && vData.numero_cuenta != null && vData.numero_cuenta != "0")
            {
                lstLista.Add(vData);
            }
        }
        return lstLista;
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
        Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();


        try
        {
            Int64 COD_OPE = 0;

            ELiquidacionInteres pLiqui = new ELiquidacionInteres();

            pLiqui.lstLista = new List<ELiquidacionInteres>();
            pLiqui.lstLista = ObtenerListaGrilla();


            String estado = "";
            DateTime fechacierrehistorico;
            DateTime fechaLiquidacion = txtFecha.ToDateTime; 
            Xpinn.Ahorros.Entities.AhorroVista vAhorroVistaCierre = new Xpinn.Ahorros.Entities.AhorroVista();
            vAhorroVistaCierre = Ahorroservicio.ConsultarCierreAhorroVista((Usuario)Session["usuario"]);
            estado = vAhorroVistaCierre.estadocierre;
            fechacierrehistorico = Convert.ToDateTime(vAhorroVistaCierre.fecha_cierre.ToString());

            if (estado == "D" && fechaLiquidacion <= fechacierrehistorico)
            {
                VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO H,'AHORROS'");

            }
            else
            {

                //Guardar en liquidacion_Ahorro
                if (pLiqui.lstLista.Count > 0)
                {
                    Ahorroservicio.CrearLiquidacionAhorro(pLiqui, (Usuario)Session["usuario"]);
                }

                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                Usuario pUsuario = new Usuario();
                pUsuario = (Usuario)Session["Usuario"];

                // Valida que exista parametrización contable para la operación        
                List<Xpinn.Contabilidad.Entities.ProcesoContable> LstProcesoContable;
                LstProcesoContable = ComprobanteServicio.ConsultaProceso(0, 14, txtFecha.ToDateTime, pUsuario);
                if (LstProcesoContable.Count() == 0)
                {
                    VerError("No existen comprobantes parametrizados para esta operación (Tipo 14=Liquidacion Intereses de Ahorros)");
                    return;
                }

                Int64 pcod_proceso = Convert.ToInt64(LstProcesoContable[0].cod_proceso);

                //GRABACION DE LA OPERACION
                Xpinn.Tesoreria.Services.OperacionServices xTesoreria = new Xpinn.Tesoreria.Services.OperacionServices();
                Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
                vOpe.cod_ope = 0;
                vOpe.tipo_ope = 14;
                //COD USUARIO EN CAPA DATOS
                //COD OFICINA EN CAPA DATOS
                vOpe.cod_caja = 0;
                vOpe.cod_cajero = 0;
                vOpe.observacion = null;
                vOpe.cod_proceso = null;
                vOpe.fecha_oper = txtFecha.ToDateTime;
                vOpe.fecha_calc = DateTime.Now;
                
                string pError = "";
                // Cargar listado de depreciación
                if (pLiqui.lstLista.Count > 0)
                {                   
                    Ahorroservicio.GuardarLiquidacionAhorro(ref pError, ref COD_OPE, vOpe, pLiqui, (Usuario)Session["usuario"]);                   
                }

                //if (txtCodigo.Text != null || txtCodigo.Text != "")
                //{
                //    //GRABACION DEL GIRO A REALIZAR
                //    Xpinn.FabricaCreditos.Services.AvanceService AvancServices = new Xpinn.FabricaCreditos.Services.AvanceService();
                //    Usuario pusu = (Usuario)Session["usuario"];
                //    Xpinn.FabricaCreditos.Entities.Giro pGiro = new Xpinn.FabricaCreditos.Entities.Giro();
                //    pGiro.idgiro = 0;
                //    pGiro.cod_persona = Convert.ToInt64(Session["Identificacion"]);
                //    pGiro.forma_pago = Convert.ToInt32(ctlGiro.ValueFormaDesem);
                //    pGiro.tipo_acto = 7;
                //    pGiro.fec_reg = Convert.ToDateTime(txtFecha.Texto);
                //    pGiro.fec_giro = DateTime.Now;
                //    pGiro.numero_radicacion = 0;
                //    pGiro.usu_gen = pusu.nombre;
                //    pGiro.usu_apli = null;
                //    pGiro.estadogi = 0;
                //    pGiro.usu_apro = null;
                //    if (ctlGiro.IndiceFormaDesem == 1) //"eFECTIVO"
                //    {
                //        pGiro.idctabancaria = 0;
                //        pGiro.cod_banco = 0;
                //        pGiro.num_cuenta = null;
                //        pGiro.tipo_cuenta = -1;
                //    }
                //    if (ctlGiro.IndiceFormaDesem != 1) //"eFECTIVO"
                //    {

                //        //DETERMINAR LA IDENTIFICACIÓN DE LA CUENTA BANCARIA
                //        CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ctlGiro.ValueEntidadOrigen), ctlGiro.TextCuentaOrigen, (Usuario)Session["usuario"]);
                //        Int64 idCta = CuentaBanc.idctabancaria;
                //        //DATOS DE FORMA DE PAGO
                //        if (ctlGiro.IndiceFormaDesem == 3) //"Transferencia"
                //        {
                //            pGiro.idctabancaria = idCta;
                //            pGiro.cod_banco = Convert.ToInt32(ctlGiro.ValueEntidadDest);
                //            pGiro.num_cuenta = ctlGiro.TextNumCuenta;
                //            pGiro.tipo_cuenta = Convert.ToInt32(ctlGiro.ValueTipoCta);
                //        }
                //        else if (ctlGiro.IndiceFormaDesem == 2) //Cheque
                //        {
                //            pGiro.idctabancaria = idCta;
                //            pGiro.cod_banco = 0;        //NULO
                //            pGiro.num_cuenta = null;    //NULO
                //            pGiro.tipo_cuenta = -1;      //NULO
                //        }
                //        else
                //        {
                //            pGiro.idctabancaria = 0;
                //            pGiro.cod_banco = 0;
                //            pGiro.num_cuenta = null;
                //            pGiro.tipo_cuenta = -1;
                //        }
                //    }

                //    pGiro.fec_apro = DateTime.MinValue;
                //    pGiro.cob_comision = 0;
                //    pGiro.valor = Convert.ToInt64(Session["Valor"]);

                //}
                // Grabar comporbante 

                if (pError != null && pError != "")
                {
                    VerError(pError);
                }
                if (pError == null || pError == "")
                {

                    if (vOpe.cod_ope != 0)
                    {
                        var usu = (Usuario)Session["usuario"];
                        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicios = new Xpinn.Contabilidad.Services.ComprobanteService();
                        Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = vOpe.cod_ope;
                        Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 14;
                        Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = usu.codusuario; //"<Colocar Aquí el código de la persona del servicio>"
                        Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");

                        Session[Ahorroservicio.CodigoProgramaLiq + ".id"] = idObjeto;
                    }
                    Session["Valor"] = null;
                    Site toolBar = (Site)Master;
                    toolBar.eventoRegresar += btnRegresar_Click;

                    toolBar.MostrarExportar(false);
                    toolBar.MostrarGuardar(false);

                    mvPrincipal.ActiveViewIndex = 1;

                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Ahorroservicio.CodigoProgramaLiq, "btnContinuarMen_Click", ex);
        }
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        try
        {
            txtFecha.Text = DateTime.Now.ToString(gFormatoFecha);
            Site toolBar = (Site)Master;
            toolBar.MostrarExportar(false);
            toolBar.MostrarGuardar(false);
            panelGrilla.Visible = false;
            mvPrincipal.ActiveViewIndex = 0;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Ahorroservicio.CodigoProgramaLiq, "btnRegresar_Click", ex);
        }
    }



    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, Ahorroservicio.CodigoProgramaLiq);
        List<Xpinn.Ahorros.Entities.AhorroVista> lstConsulta = new List<Xpinn.Ahorros.Entities.AhorroVista>();
        gvLista.DataSource = lstConsulta;
        gvLista.DataBind();
        btnExportar.Visible = false;
        gvLista.Visible = false;
        lblTotalRegs.Visible = false;
    }


    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[Ahorroservicio.CodigoProgramaLiq + ".id"] = id;
        Navegar(Pagina.Detalle);
    }


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Ahorroservicio.CodigoProgramaLiq, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void cargarListaddl()
    {
        //Xpinn.Asesores.Data.OficinaData listaOficina = new Xpinn.Asesores.Data.OficinaData();
        //Xpinn.Asesores.Entities.Oficina oficina = new Xpinn.Asesores.Entities.Oficina();
        //oficina.Estado = 1;
        //var lista = listaOficina.ListarOficina(oficina, (Usuario)Session["usuario"]);
        ctlGiro.Inicializar();
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];

        Xpinn.Ahorros.Services.LineaAhorroServices linahorroServicio = new Xpinn.Ahorros.Services.LineaAhorroServices();
        Xpinn.Ahorros.Entities.LineaAhorro linahorroVista = new Xpinn.Ahorros.Entities.LineaAhorro();
        var listaAhorro = linahorroServicio.ListarLineaAhorro(linahorroVista, pUsuario);
        if (listaAhorro != null)
        {
            listaAhorro.Insert(0, new Xpinn.Ahorros.Entities.LineaAhorro { descripcion = "Seleccione Un Item", cod_linea_ahorro = "" });

            ddlLinea.DataSource = listaAhorro;
            ddlLinea.DataTextField = "descripcion";
            ddlLinea.DataValueField = "cod_linea_ahorro";
            ddlLinea.DataBind();
        }
    }

    private void Actualizar()
    {
        if (txtFecha.TieneDatos == false)
        {
            VerError("Debe ingresar la fecha de liquidación");
            return;
        }
        //try
        //{
        DateTime pFechaLiquidacion = new DateTime();
        pFechaLiquidacion = txtFecha.ToDateTime;
        DateTime fechaini = ConvertirStringToDate(txtFecha.Text);
        String cuenta = this.txtCodigo.Text == null ? "" : this.txtCodigo.Text;
        if (txtCodigo.Text == "" || txtFecha.Text.Length < 0)
        {
            VerError("Seleccione las Opciones");
        }
        else
            VerError(" ");

        if (ddlLinea.SelectedValue == null)
        {
            VerError("Seleccione la linea de Ahorro");
        }
        else
            VerError(" ");

        if (ddlLinea.SelectedIndex <= 0)
        {
            VerError("Seleccione la linea de Ahorro");
        }
        else
            VerError(" ");

        List<Xpinn.Ahorros.Entities.ELiquidacionInteres> lstConsulta = new List<Xpinn.Ahorros.Entities.ELiquidacionInteres>();
        lstConsulta = Ahorroservicio.getCuentasLiquidarServices(fechaini, ddlLinea.SelectedValue.ToString(), (Usuario)Session["usuario"], cuenta);

        gvLista.PageSize = pageSize;
        gvLista.EmptyDataText = emptyQuery;
        //  gvLista.DataSource = lstConsulta;
        Site toolbar = (Site)Master;
        if (lstConsulta != null && lstConsulta.Count > 0)
        {
            btnExportar.Visible = true;
            gvLista.Visible = true;
            lblTotalRegs.Visible = true;
            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
            Session["DTLIQUIDACION"] = lstConsulta;
            gvLista.DataSource = lstConsulta;

            gvLista.DataBind();
            //ValidarPermisosGrilla(gvLista);
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
        }
        else
        {
            btnExportar.Visible = false;
            gvLista.Visible = false;
            lblTotalRegs.Visible = false;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
        }

        Session.Add(Ahorroservicio.CodigoProgramaLiq + ".consulta", 1);
        //}
        //catch (Exception ex)
        //{
        //    BOexcepcion.Throw(Ahorroservicio.CodigoProgramaLiq, "Actualizar", ex);
        //}
    }

    private Xpinn.Ahorros.Entities.AhorroVista ObtenerValores()
    {
        Xpinn.Ahorros.Entities.AhorroVista vActivoFijo = new Xpinn.Ahorros.Entities.AhorroVista();
        return vActivoFijo;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvLista);
    }

    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=LiqIntAhorros.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.Default;
        StringWriter sw = new StringWriter();
        ExpGrilla expGrilla = new ExpGrilla();

        sw = expGrilla.ObtenerGrilla(GridView1, null);

        Response.Write(expGrilla.style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }


    protected void txtCodigo_TextChanged(object sender, EventArgs e)
    {
        if (txtCodigo.Text != "" || txtCodigo.Text != null)
        {
            this.ctlGiro.Visible = false;
        }
        else
        {
            this.ctlGiro.Visible = false;
        }
    }
}