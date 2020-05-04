using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Reporteador.Services;
using Xpinn.Reporteador.Entities;
using Xpinn.Programado.Services;
using Xpinn.Programado.Entities;
using System.Linq;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Lista : GlobalWeb
{
    Xpinn.Reporteador.Services.UIAFService reporteServicio = new Xpinn.Reporteador.Services.UIAFService();
    CuentasProgramadoServices cuentaProgramadoServices = new CuentasProgramadoServices();
    PoblarListas poblar = new PoblarListas();
    List<ELiquidacionInteres> listagr;
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(cuentaProgramadoServices.CodigoProgramaLiqInteres, "L");

            Site toolBar = (Site)this.Master;
            //toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            txtFechaIni.eventoCambiar += txtFecha_TextChanged;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarExportar(false);
            toolBar.MostrarLimpiar(false);
            
            //ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cuentaProgramadoServices.CodigoProgramaLiqInteres + "L", "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {

                var fechain = DateTime.Now;
                //DateTime fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
               // DateTime fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);
                txtFechaIni.Text = fecha.ToString();
                // cargarListaddl();
                poblar.PoblarListaDesplegable("lineaprogramado", "", "estado=1", "1", ddlLinea, (Usuario)Session["usuario"]);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cuentaProgramadoServices.CodigoProgramaLiqInteres + "L", "Page_Load", ex);
        }
    }

    // carga la entidad de operacion y la retorna llena 
    Xpinn.Tesoreria.Entities.Operacion cargarOperacion()
    {
        try
        {
            Xpinn.Tesoreria.Entities.Operacion operacion = new Xpinn.Tesoreria.Entities.Operacion();
            operacion.cod_ope = 0;
            operacion.tipo_ope = 46;
            operacion.cod_caja = 0;
            operacion.cod_cajero = 0;
            operacion.observacion = "Grabacion de operacion- LiquidacionInteres";
            operacion.cod_proceso = null;
            operacion.fecha_oper = DateTime.Now;
            operacion.fecha_calc = DateTime.Now;

            return operacion;
        }
        catch (Exception ex)
        {
            // BOexcepcion.Throw(cuentasProgramado.GetType().Name + "L", "Page_PreInit", ex);
            return null;
        }
    }

    private void Actualizar()
    {
        try
        {
            Site toolBar = (Site)this.Master;

            if (ddlLinea.SelectedIndex <= 0 || txtFechaIni.Text.Length < 0)
            {
                VerError("Seleccione las Opciones");
            }
            else
            {
                var fechaini = Convert.ToDateTime(txtFechaIni.Text);
                listagr = cuentaProgramadoServices.getCuentasLiquidarServices(fechaini, ddlLinea.SelectedValue.ToString(), (Usuario)Session["usuario"]);

                Session["ListaData"] = listagr;
                Session["linea"] = ddlLinea.SelectedValue.ToString();
                if (listagr.Count > 0)
                {
                    var totalInteres = (from t in listagr select t.Interes).Sum();
                    var totalRetencion = (from r in listagr select r.Retefuente).Sum();
                    var totalNeto = (from n in listagr select n.valor_Neto).Sum();

                    gvLista.Visible = true;
                    lblInfo.Text = "<br/> Registros encontrados " + listagr.Count.ToString();
                    lblInfo.Visible = true;
                    lblTotalRegs.Visible = true;
                    toolBar.MostrarExportar(true);
                    toolBar.MostrarLimpiar(true);
                    toolBar.MostrarGuardar(true);
                    gvLista.DataSource = listagr;
                    gvLista.DataBind();

                    txtInteres.Text = totalInteres.ToString();
                    txtTotalRetencion.Text = totalRetencion.ToString();
                    txtNeto.Text = totalNeto.ToString();

                    lblInteres.Visible = true;
                    lblRetencion.Visible = true;
                    lblNeto.Visible = true;
                    txtInteres.Visible = true;
                    txtTotalRetencion.Visible = true;
                    txtNeto.Visible = true;
                }
                else
                {
                    gvLista.Visible = false;
                    lblTotalRegs.Visible = false;
                    lblInfo.Text = "<br/> Registros encontrados " + listagr.Count.ToString();
                    lblInfo.Visible = true;
                    toolBar.MostrarExportar(false);
                    toolBar.MostrarLimpiar(true);
                    txtInteres.Visible = false;
                    txtTotalRetencion.Visible = false;
                    txtNeto.Visible = false;

                    lblInteres.Visible = false;
                    lblTotalRegs.Visible = false;
                    lblNeto.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cuentaProgramadoServices.CodigoProgramaLiqInteres + "L", "btnConsultar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        toolBar.MostrarExportar(false);
        txtFechaIni.Text = "";
        txtInteres.Text = "";
        txtTotalRetencion.Text = "";
        txtNeto.Text = "";
        gvLista.Visible = false;
        lblInfo.Visible = false;
        ddlLinea.ClearSelection();
        DateTime fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        txtFechaIni.Text = fecha.ToString();
        txtInteres.Visible = false;
        txtTotalRetencion.Visible = false;
        txtNeto.Visible = false;
        lblInteres.Visible = false;
        lblRetencion.Visible = false;
        lblNeto.Visible = false;
    }


 

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0 && Session["ListaData"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.AllowPaging = false;
            gvLista.DataSource = Session["ListaData"];
            gvLista.DataBind();
            gvLista.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvLista);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=InteresesProgramado.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
        {
            VerError("No existen datos, genere la consulta");
        }
    }

    void cargarListaddl()
    {
        Xpinn.Asesores.Data.OficinaData listaOficina = new Xpinn.Asesores.Data.OficinaData();
        Xpinn.Asesores.Entities.Oficina oficina = new Xpinn.Asesores.Entities.Oficina();
        oficina.Estado = 1;
        var lista = listaOficina.ListarOficina(oficina, (Usuario)Session["usuario"]);

        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];

        Xpinn.Ahorros.Services.LineaAhorroServices linahorroServicio = new Xpinn.Ahorros.Services.LineaAhorroServices();
        Xpinn.Ahorros.Entities.LineaAhorro linahorroVista = new Xpinn.Ahorros.Entities.LineaAhorro();
        linahorroVista.estado = 1;
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

    public void btnGuardar_Click(object sender, EventArgs e)
    {
        var entidad = (List<ELiquidacionInteres>)Session["ListaData"];// si la lista no viene cargada
        List<Etran_Programado> listaDatoInteres = new List<Etran_Programado>();
        List<Etran_Programado> listaDatoRetaFuente = new List<Etran_Programado>();
        var cod_operacion = 0L;
        // SI LA LISTA NO SE CRAGA TRAER EL METODO EJECUTAR NUEVAMENTE EL SP
        foreach (var item in entidad)
        {
            Etran_Programado tranCod_Inters = new Etran_Programado
            {
                NUMERO_PROGRAMADO = item.NumeroCuenta,
                COD_CLIENTE = item.Cod_Usuario,
                TIPO_TRAN = 353,
                VALOR = item.Interes,
                ESTADO = 1,
                Fecha_Interes=Convert.ToDateTime(txtFechaIni.Text),
                
            };
            Etran_Programado TranCod_Retafuente = new Etran_Programado
            {
                NUMERO_PROGRAMADO = item.NumeroCuenta,
                COD_CLIENTE = item.Cod_Usuario,
                TIPO_TRAN = 354,
                VALOR = item.Retefuente,
                ESTADO = 1
            };
            listaDatoInteres.Add(tranCod_Inters);
            listaDatoRetaFuente.Add(TranCod_Retafuente);
        }
        if (txtInteres.Text != "0")
        {


            String estado = "";
            DateTime fechacierrehistorico;
            DateTime fecharetiro = Convert.ToDateTime(txtFechaIni.Texto);
            Xpinn.Programado.Entities.CuentasProgramado vAhorroProgramado = new Xpinn.Programado.Entities.CuentasProgramado();
            vAhorroProgramado = cuentaProgramadoServices.ConsultarCierreAhorroProgramado((Usuario)Session["usuario"]);
            estado = vAhorroProgramado.estadocierre;
            fechacierrehistorico = Convert.ToDateTime(vAhorroProgramado.fecha_cierre.ToString());

            if (estado == "D" && fecharetiro <= fechacierrehistorico)
            {
                VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO L,'AH. PROGRAMADO'");
            }

            else
            {

                DateTime fechaoperacion = Convert.ToDateTime(txtFechaIni.Text);

                cuentaProgramadoServices.guardarDatosLiquidacionServices(listaDatoInteres, listaDatoRetaFuente, cargarOperacion(), (Usuario)Session["usuario"], ref cod_operacion, fechaoperacion);

                cod_operacion = listaDatoInteres[0].COD_OPE;

                ELiquidacionInteres pLiqui = new ELiquidacionInteres();

                pLiqui.lstLista = new List<ELiquidacionInteres>();
                pLiqui.lstLista = ObtenerListaGrilla();

                //Guardar en liquidacion_Programado
                if (pLiqui.lstLista.Count > 0)
                {
                    cuentaProgramadoServices.CrearLiquidacionProgramado(pLiqui, (Usuario)Session["usuario"]);
                }


                if (cod_operacion != null)
                {
                    var usu = (Usuario)Session["usuario"];
                    Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                    Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = cod_operacion;
                    Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 46;
                    Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = usu.cod_persona; //"<Colocar Aquí el código de la persona del servicio>"
                    Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");

                    Session[cuentaProgramadoServices.CodigoProgramaCierreCuenta + ".id"] = idObjeto;
                }
            }

        }

    }
    private List<ELiquidacionInteres> ObtenerListaGrilla()
    {
        List<ELiquidacionInteres> lstLista = new List<ELiquidacionInteres>();

        foreach (GridViewRow rFila in gvLista.Rows)
        {
            ELiquidacionInteres vData = new ELiquidacionInteres();
            if (rFila.Cells[0].Text != "" && rFila.Cells[0].Text != "&nbsp;")//numero_cuenta
                vData.NumeroCuenta = Convert.ToString(rFila.Cells[0].Text);
            else
                vData.NumeroCuenta = "";

            if (rFila.Cells[2].Text != "" && rFila.Cells[2].Text != "&nbsp;")//IDENTIFICACION
                vData.Identificacion = Convert.ToString(rFila.Cells[2].Text);

            if (rFila.Cells[3].Text != "" && rFila.Cells[3].Text != "&nbsp;")//NOMBRE TITULAR
                vData.Nombre = rFila.Cells[3].Text;

            if (rFila.Cells[6].Text != "" && rFila.Cells[6].Text != "&nbsp;")//VALOR
            { 
            vData.Saldo = Convert.ToDecimal(rFila.Cells[6].Text.ToString().Replace(".", ""));
                vData.valor_Neto = Convert.ToDecimal(rFila.Cells[6].Text.ToString().Replace(".", ""));
            }

            if (rFila.Cells[7].Text != "" && rFila.Cells[7].Text != "&nbsp;")//TASA
                vData.Tasa_interes = Convert.ToDecimal(rFila.Cells[7].Text);

            vData.fecha_int = Convert.ToDateTime(this.txtFechaIni .Texto);
            vData.fecha_liquidacion = Convert.ToDateTime(txtFechaIni.Texto);


            if (rFila.Cells[9].Text != "" && rFila.Cells[9].Text != "&nbsp;")//INTERES
                vData.Interes = Convert.ToDecimal(rFila.Cells[9].Text.ToString().Replace(".", ""));
            else
                vData.Interes = 0;

            if (rFila.Cells[10].Text != "" && rFila.Cells[10].Text != "&nbsp;")//RETENCION
                vData.Retefuente = Convert.ToDecimal(rFila.Cells[10].Text.ToString().Replace(".", ""));
            else
                vData.Retefuente = 0;

            vData.valor_gmf = 0;

            vData.valor_Neto = 0;

            vData.interes_capitalizado = 0;

            vData.retencion_causado = 0;

            vData.forma_pago = null;

            vData.cta_ahorros = null;

            if (vData.NumeroCuenta != "" && vData.NumeroCuenta != null && vData.NumeroCuenta != "0")
            {
                lstLista.Add(vData);
            }
        }
        return lstLista;
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
            BOexcepcion.Throw(reporteServicio.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }
    public void txtFecha_TextChanged(object sender, EventArgs e)
    {
        VerError("");       
        DateTime fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
        if (Convert.ToDateTime(txtFechaIni.Text) > fecha)
        {
            VerError("La fecha de liquidación no puede ser mayor al periodo de corte");
            txtFechaIni.Text = fecha.ToShortDateString();
        }
    }


    protected void gvLista_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void txtInteres_TextChanged(object sender, EventArgs e)
    {

    }
}
