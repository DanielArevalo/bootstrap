using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Reporteador.Services;
using Xpinn.Reporteador.Entities;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using System.Linq;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Globalization;
public partial class Lista : GlobalWeb
{
    Xpinn.Reporteador.Services.UIAFService reporteServicio = new Xpinn.Reporteador.Services.UIAFService();
    AporteServices AportesServices = new AporteServices();
    PoblarListas poblar = new PoblarListas();
    List<LiquidacionInteres> listagr;
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(AportesServices.CodigoProgramaLiqAportes, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarExportar(false);
            toolBar.MostrarLimpiar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AportesServices.CodigoProgramaLiqAportes + "L", "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {

                var fechain = DateTime.Now;
                DateTime fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                txtFechaIni.Text = fecha.ToString();
                poblar.PoblarListaDesplegable("lineaaporte", ddlLinea, (Usuario)Session["usuario"]);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AportesServices.CodigoProgramaLiqAportes + "L", "Page_Load", ex);
        }
    }

    // carga la entidad de operacion y la retorna llena 
    Xpinn.Tesoreria.Entities.Operacion cargarOperacion()
    {
        try
        {
            Xpinn.Tesoreria.Entities.Operacion operacion = new Xpinn.Tesoreria.Entities.Operacion();
            operacion.cod_ope = 0;
            operacion.tipo_ope = 113;
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
                listagr = AportesServices.getAportesLiquidarServices(fechaini, ddlLinea.SelectedValue.ToString(), (Usuario)Session["usuario"]);

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
            BOexcepcion.Throw(AportesServices.CodigoProgramaLiqAportes + "L", "btnConsultar_Click", ex);
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
            Response.AddHeader("Content-Disposition", "attachment;filename=InteresAportes.xls");
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

    private void cargarListaddl()
    {
        Xpinn.Asesores.Data.OficinaData listaOficina = new Xpinn.Asesores.Data.OficinaData();
        Xpinn.Asesores.Entities.Oficina oficina = new Xpinn.Asesores.Entities.Oficina();
        oficina.Estado = 1;
        var lista = listaOficina.ListarOficina(oficina, (Usuario)Session["usuario"]);

        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];

        Xpinn.Aportes.Services.LineaAporteServices linaporteServicio = new Xpinn.Aportes.Services.LineaAporteServices();
        Xpinn.Aportes.Entities.LineaAporte linaporteVista = new Xpinn.Aportes.Entities.LineaAporte();
        var listaAporte = linaporteServicio.ListarLineaAporte(linaporteVista, pUsuario);
        if (listaAporte != null)
        {
            listaAporte.Insert(0, new Xpinn.Aportes.Entities.LineaAporte { nombre = "Seleccione Un Item", cod_linea_aporte = 0 });

            ddlLinea.DataSource = listaAporte;
            ddlLinea.DataTextField = "descripcion";
            ddlLinea.DataValueField = "cod_linea_ahorro";
            ddlLinea.DataBind();
        }
    }

    private List<LiquidacionInteres> ObtenerListaGrilla()
    {
        List<LiquidacionInteres> lstLista = new List<LiquidacionInteres>();                
        gvLista.AllowPaging = false;
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            LiquidacionInteres vData = new LiquidacionInteres();
            if (rFila.Cells[0].Text != "" && rFila.Cells[0].Text != "&nbsp;")//numero_cuenta
                vData.NumeroCuenta = Convert.ToString(rFila.Cells[0].Text);
            else
                vData.NumeroCuenta = "";
            if (rFila.Cells[2].Text != "" && rFila.Cells[2].Text != "&nbsp;")//IDENTIFICACION
                vData.Identificacion = Convert.ToString(rFila.Cells[2].Text);

            if (rFila.Cells[6].Text != "" && rFila.Cells[6].Text != "&nbsp;")//VALOR
                vData.Saldo = Convert.ToDecimal(rFila.Cells[6].Text);

            if (rFila.Cells[8].Text != "" && rFila.Cells[8].Text != "&nbsp;")//TASA INTERES
                vData.Tasa_interes = Convert.ToDecimal(rFila.Cells[8].Text);
            else
                vData.Tasa_interes = 0;

            if (rFila.Cells[9].Text != "" && rFila.Cells[9].Text != "&nbsp;")//INTERES
                vData.Interes = Convert.ToDecimal(rFila.Cells[9].Text);
            else
                vData.Interes = 0;

            if (rFila.Cells[10].Text != "" && rFila.Cells[10].Text != "&nbsp;")//RETENCION
                vData.Retefuente = Convert.ToDecimal(rFila.Cells[10].Text);
            else
                vData.Retefuente = 0;

            vData.fecha_liquidacion = Convert.ToDateTime(txtFechaIni.Text);

            if (vData.NumeroCuenta != "" && vData.NumeroCuenta != null && vData.NumeroCuenta != "0")
            {
                lstLista.Add(vData);
            }
        }
        return lstLista;
    }


    public void btnGuardar_Click(object sender, EventArgs e)
    {
        var entidad = (List<LiquidacionInteres>)Session["ListaData"];// si la lista no viene cargada
        List<Tran_Aportes> listaDatoInteres = new List<Tran_Aportes>();
        List<Tran_Aportes> listaDatoRetaFuente = new List<Tran_Aportes>();
        var cod_operacion = 0L;        
        // SI LA LISTA NO SE CRAGA TRAER EL METODO EJECUTAR NUEVAMENTE EL SP
        foreach (var item in entidad)
        {
            Tran_Aportes tranCod_Inters = new Tran_Aportes
            {
                NUMERO_APORTE = item.numero_cuenta,
                COD_CLIENTE = item.Cod_Usuario,
                TIPO_TRAN = 103,
                VALOR = item.Interes,
                ESTADO = 1,
                Fecha_Interes = Convert.ToDateTime(txtFechaIni.Text),

            };
            Tran_Aportes TranCod_Retafuente = new Tran_Aportes
            {
                NUMERO_APORTE = item.numero_cuenta,
                COD_CLIENTE = item.Cod_Usuario,
                TIPO_TRAN = 104,
                VALOR = item.Retefuente,
                ESTADO = 1
            };
            listaDatoInteres.Add(tranCod_Inters);
            listaDatoRetaFuente.Add(TranCod_Retafuente);
         
        }
       
        LiquidacionInteres pLiqui = new LiquidacionInteres();
      
        pLiqui.lstLista = new List<LiquidacionInteres>();
        pLiqui.lstLista = ObtenerListaGrilla();



        String estado = "";
        DateTime fechacierrehistorico;
        String formato = gFormatoFecha;
        DateTime Fechaliquidacion = DateTime.ParseExact(txtFechaIni.Text, formato, CultureInfo.InvariantCulture);

        Xpinn.Aportes.Entities.Aporte vaportes = new Xpinn.Aportes.Entities.Aporte();
        vaportes = AportesServices.ConsultarCierreAportes((Usuario)Session["usuario"]);
        estado = vaportes.estadocierre;
        fechacierrehistorico = Convert.ToDateTime(vaportes.fecha_cierre.ToString());

        if (estado == "D" && Fechaliquidacion <= fechacierrehistorico)
        {
            VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO A,'APORTES'");
            return;
        }
        else
        {


            //Guardar en liquidacion_Aportes
            if (pLiqui.lstLista.Count > 0)
            {
                AportesServices.CrearLiquidacionAportes(pLiqui, (Usuario)Session["usuario"]);
            }

            //if (txtInteres.Text != "0")
            // {
            DateTime fechaoperacion = Convert.ToDateTime(txtFechaIni.Text);
            AportesServices.guardarDatosLiquidacionServices(listaDatoInteres, listaDatoRetaFuente, cargarOperacion(), (Usuario)Session["usuario"], ref cod_operacion, fechaoperacion);
            cod_operacion = listaDatoInteres[0].COD_OPE;


            //  LiquidacionInteres pLiqui = new LiquidacionInteres();

            if (cod_operacion != 0)
            {
                var usu = (Usuario)Session["usuario"];
                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = cod_operacion;
                Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 113;
                Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = usu.codusuario; //"<Colocar Aquí el código de la persona del servicio>"
                Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");


                //  LiquidacionInteres pLiqui = new LiquidacionInteres();

                Session[AportesServices.CodigoProgramaLiqAportes + ".id"] = idObjeto;
            }


            if (cod_operacion != null)
            {
                var usu = (Usuario)Session["usuario"];
                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = cod_operacion;
                Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 113;
                Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = usu.codusuario; //"<Colocar Aquí el código de la persona del servicio>"
                Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");

                Session[AportesServices.CodigoProgramaLiqAportes + ".id"] = idObjeto;
            }
            //   }

            // pLiqui.lstLista = new List<LiquidacionInteres>();
            //pLiqui.lstLista = ObtenerListaGrilla();

            //Guardar en liquidacion_Aportes
            //if (pLiqui.lstLista.Count > 0)
            // {
            //AportesServices.CrearLiquidacionAportes(pLiqui, (Usuario)Session["usuario"]);
            //  }   


            if (cod_operacion != null)
            {
                var usu = (Usuario)Session["usuario"];
                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = cod_operacion;
                Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 113;
                Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = usu.codusuario; //"<Colocar Aquí el código de la persona del servicio>"
                Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");

                Session[AportesServices.CodigoProgramaLiqAportes + ".id"] = idObjeto;
            }
            //   }
        }
    }
 
}
