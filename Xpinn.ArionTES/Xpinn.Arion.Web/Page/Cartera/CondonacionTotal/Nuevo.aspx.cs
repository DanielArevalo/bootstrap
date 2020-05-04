using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using System.Linq;
using System.IO;
using System.Xml;
using System.Design;
using System.Threading;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using System.Configuration;
using System.Globalization;
using System.Web.UI;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Cartera.Services.CondonacionTotalService CondonaciontotalServicio = new Xpinn.Cartera.Services.CondonacionTotalService();
 
    CreditoService CreditoServicio = new CreditoService();
    DocumentoService documentoServicio = new DocumentoService();
    DatosDeDocumentoService datosDeDocumentoServicio = new DatosDeDocumentoService();
    List<DetalleProducto> lstConsulta = new List<DetalleProducto>();
    EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
    
     ///<summary>
    /// Mostrar la barra de herramientas al ingresar a la funcionalidad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
           
            if (Session[CondonaciontotalServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(CondonaciontotalServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(CondonaciontotalServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar+= btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CondonaciontotalServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    /// <summary>
    /// Cargar datos de la funcionalidad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[CondonaciontotalServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[CondonaciontotalServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(CondonaciontotalServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);                    
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CondonaciontotalServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    /// <summary>
    ///  Evento para guardar 
    /// </summary>
    protected void Guardar()
    {
        VerError("");
        List<Xpinn.Cartera.Entities.CondonacionTotal> lstConsulta = new List<Xpinn.Cartera.Entities.CondonacionTotal>();

        Usuario usuap = (Usuario)Session["usuario"];
        int cod = Convert.ToInt32(usuap.codusuario);
        int oficina = Convert.ToInt32(usuap.cod_oficina);
        // int interescondonar = Convert.ToInt32(Lblinfointeres.Text);
        // int moracondonar = Convert.ToInt32(Lblinfomora.Text);

        if (txtFechaCondonacion.Text == "")
        {
            VerError("Por favor ingresar una fecha de condonación");
        }

        else
        {
            try
            {
                Xpinn.Cartera.Entities.CondonacionTotal vCondonacion = new Xpinn.Cartera.Entities.CondonacionTotal();
                vCondonacion.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text.Trim());
                vCondonacion.fecha_condonacion = txtFechaCondonacion.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtFechaCondonacion.Text);
                //vCondonacion.valorcond_intcte = Convert.ToInt64(txtvalinterescte.Text.Trim());
                //vCondonacion.valorcond_mora = Convert.ToInt64(txtvalinteresmora.Text.Trim());
                vCondonacion.codigo_usuario = cod;
                vCondonacion.codigo_oficina = oficina;

                               
                    try
                    {
                        CondonaciontotalServicio.CrearCondonacion(vCondonacion, (Usuario)Session["usuario"]);
                        // Genera el comprobante contable

                        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new
                        Xpinn.Contabilidad.Services.ComprobanteService();

                        Usuario pUsuario = new Usuario();
                        pUsuario = (Usuario)Session["Usuario"];
                        Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = vCondonacion.codigo.ToString();
                        Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 35;
                        Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = DateTime.Now;
                        Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] = vCondonacion.codigo_oficina;
                        Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = pUsuario.codusuario;
                        Session["OrigenComprobante"] = "../../Cartera/CondonacionTotales/Nuevo.aspx";
                        //  Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                    }

                    catch 
                    {
                        VerError("La condonación no se puedo efectuar");
                    }
                }

            

            catch (Exception ex)
            {
                BOexcepcion.Throw(CondonaciontotalServicio.CodigoPrograma, "btnGuardar_Click", ex);
            }
        }

    }
    
    
    /// <summary>
    /// Evento para consultar los datos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }
    // <summary>
    /// Evento para guardar los datos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
              Guardar();
               
         }

          


    /// <summary>
    /// Evento para cancelar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[CondonaciontotalServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }


    /// <summary>
    /// Mostrar los datos del crédito
    /// </summary>
    /// <param name="pIdObjeto"></param>
    protected void ObtenerDatos(String pIdObjeto)
    {
      
        Int64 credito = 0;
        try
        {
            Credito vCredito = new Credito();
            vCredito = CreditoServicio.ConsultarCredito(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vCredito.numero_radicacion != Int64.MinValue)
                txtNumero_radicacion.Text = vCredito.numero_radicacion.ToString().Trim();
            credito = Convert.ToInt64(txtNumero_radicacion.Text);
            if (vCredito.identificacion != string.Empty)
                txtIdentificacion.Text = HttpUtility.HtmlDecode(vCredito.identificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.tipo_identificacion))
                txtTipo_identificacion.Text = HttpUtility.HtmlDecode(vCredito.tipo_identificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.nombre))
                txtNombre.Text = HttpUtility.HtmlDecode(vCredito.nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.linea_credito))
                txtLinea_credito.Text = HttpUtility.HtmlDecode(vCredito.linea_credito.ToString().Trim());
            if (vCredito.monto != Int64.MinValue)
                txtMonto.Text = HttpUtility.HtmlDecode(vCredito.monto.ToString().Trim());
            if (vCredito.plazo != Int64.MinValue)
                txtPlazo.Text = HttpUtility.HtmlDecode(vCredito.plazo.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.periodicidad))
                txtPeriodicidad.Text = HttpUtility.HtmlDecode(vCredito.periodicidad.ToString().Trim());
            if (vCredito.valor_cuota != Int64.MinValue)
                txtValor_cuota.Text = HttpUtility.HtmlDecode(vCredito.valor_cuota.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.forma_pago))
                txtForma_pago.Text = HttpUtility.HtmlDecode(vCredito.forma_pago.ToString().Trim());
            if (vCredito.saldo_capital != Int64.MinValue)
                this.txtSaldoCapital.Text = HttpUtility.HtmlDecode(vCredito.saldo_capital.ToString().Trim());
            if (vCredito.fecha_prox_pago != null)
                this.txtFechaProxPago.Text = HttpUtility.HtmlDecode(vCredito.fecha_prox_pago.ToShortDateString().ToString());
            if (vCredito.fecha_prox_pago != null)
                this.txtFechaUltPago.Text = HttpUtility.HtmlDecode(vCredito.fecha_ultimo_pago.ToShortDateString().ToString());
         
            if (!string.IsNullOrEmpty(vCredito.estado))
                if (vCredito.estado == "A")
                    txtEstado.Text = "Aprobado";
                else if ((vCredito.estado == "G"))
                    txtEstado.Text = "Generado";
                else if ((vCredito.estado == "C"))
                    txtEstado.Text = "Desembolsado";
                else
                    txtEstado.Text = vCredito.estado;
     
            lstConsulta = serviceEstadoCuenta.ListarValoresAdeudados(credito, (Usuario)Session["usuario"]);

            var detalle = lstConsulta.First(s => Convert.ToInt64(s.Producto.CodRadicacion) == vCredito.numero_radicacion);

            ActualizarPendientesCuotas(detalle);



        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CondonaciontotalServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    

    private void ActualizarPendientesCuotas(DetalleProducto detalle)
    {
        var pendCoutas = from pc in detalle.DetallePagos
                         select new
                         {
                             NumCuota = pc.NumCuota.ToString("#0", CultureInfo.InvariantCulture),
                             FechaCuota = pc.FechaCuota.ToShortDateString().ToString(),
                             Capital = pc.Capital.ToString(),
                             IntCte = pc.IntCte.ToString(),                             
                             IntMora = pc.IntMora.ToString(),
                             LeyMiPyme = pc.LeyMiPyme.ToString(),
                             iva_leymipyme = pc.ivaLeyMiPyme.ToString(),
                             Poliza = pc.Poliza.ToString(),
                             otros = pc.Otros.ToString(),
                             total = (pc.Capital + pc.IntCte + pc.IntMora + pc.LeyMiPyme + pc.ivaLeyMiPyme + pc.Poliza + pc.Otros).ToString(),
                             Cobranzas = pc.Cobranzas.ToString(),
                             totalconhonorarios = (pc.Capital + pc.IntCte + pc.IntMora + pc.LeyMiPyme + pc.ivaLeyMiPyme + pc.Poliza + pc.Otros + pc.Cobranzas).ToString()

                         };

        pendCoutas = pendCoutas.ToList();

        gvDistPagosPendCuotas.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
        gvDistPagosPendCuotas.DataSource = pendCoutas;
      
         if (pendCoutas.Count() > 0)
        {
            gvDistPagosPendCuotas.Visible = true;
            lblInfoPendCuotas.Visible = false;
            lblTotalRegPendCuotas.Visible = true;
            lblTotalRegPendCuotas.Text = "<br/> Registros encontrados " + pendCoutas.Count().ToString();
            gvDistPagosPendCuotas.DataBind();
            ValidarPermisosGrilla(gvDistPagosPendCuotas);
        }
        else
        {
            gvDistPagosPendCuotas.Visible = false;
        }
    }

    /// <summary>
    /// Actualizar los datos del crédito
    /// </summary>
    /// <param name="codigo"></param>
    /// <param name="gvLista"></param>
    /// <param name="opcion"></param>
    private void Actualizar(string codigo, GridView gvLista, int opcion)
    {
        try
        {
            String detalle = txtNumero_radicacion.Text;        
           
        }


        catch (Exception ex)
        {
            BOexcepcion.Throw(CondonaciontotalServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

   
    Decimal subtotalinterescte = 0;
    Decimal subtotalinteresmora = 0;
    Decimal subtotalcapital= 0;
    Decimal subtotalley = 0;
    Decimal subtotaliva = 0;
    Decimal subtotalpolizas = 0;
    Decimal subtotalotros = 0;
    Decimal subtotalvalorsincobranzas = 0;

    protected void gvDistPagosPendCuotas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            subtotalinterescte += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "IntCte"));
            subtotalinteresmora += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "IntMora"));
            subtotalcapital += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Capital"));
            subtotalley += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "LeyMiPyme"));
            subtotaliva += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "iva_leymipyme"));
            subtotalpolizas += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Poliza"));
            subtotalotros += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "otros"));
            subtotalvalorsincobranzas += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "total"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Total:";
            e.Row.Cells[2].Text = subtotalcapital.ToString("c");
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[3].Text = subtotalinterescte.ToString("c");           
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[4].Text = subtotalinteresmora.ToString("c");
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[5].Text = subtotalley.ToString("c");
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].Text = subtotaliva.ToString("c");
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[7].Text = subtotalpolizas.ToString("c");
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[8].Text = subtotalotros.ToString("c");
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[9].Text = subtotalvalorsincobranzas.ToString("c");
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Font.Bold = true;
           
        }

        txttotal.Text = subtotalvalorsincobranzas.ToString("c");
    }
}