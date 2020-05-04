using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Util;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;

public partial class EstadoCuentaCreditoPagoDetalle : GlobalWeb
{
    EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
    CuotasExtrasService CuoExtServicio = new CuotasExtrasService();
    List<CuotasExtras> lstConsulta = new List<CuotasExtras>();
    ProductoService serviceProducto = new ProductoService();

    Producto entityProducto;
    List<DetalleProducto> _lstConsulta = new List<DetalleProducto>();
    string _colorToKeep = "#359af2";
    string _colorToChange = "#E8254C";
    static int itemTab;


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(serviceProducto.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            ucImprimir.PrintCustomEvent += ucImprimir_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "L", "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session[Usuario.codusuario + "DTCuotasPend"] = null;
                if (Session[MOV_GRAL_CRED_PRODUC] != null)
                {

                    txtFechapago.Text = DateTime.Now.ToString(gFormatoFecha);
                    Actualizar();
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "D", "Page_Load", ex);
        }
    }


    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("~/Page/Asesores/EstadoCuenta/Detalle.aspx");
    }


    private void Actualizar()
    {
        try
        {
            DetalleProducto detalle = ListarProductos(itemTab);

            txtTipoDoc.Text = detalle.Producto.Persona.TipoIdentificacion.NombreTipoIdentificacion;
            txtNumeIdentificacion.Text = detalle.Producto.Persona.NumeroDocumento.ToString();
            txtNombres.Text = detalle.Producto.Persona.PrimerNombre + " " + detalle.Producto.Persona.SegundoNombre + " " + detalle.Producto.Persona.PrimerApellido + " " + detalle.Producto.Persona.SegundoApellido;
            txtNoCredito.Text = detalle.NumeroRadicacion.ToString();
            txtNombLinea.Text = detalle.Producto.Linea;
            txtEstaCredito.Text = detalle.Producto.Estado;
            txtEstado.Text = detalle.EstadoCredito;
            txtFechaProximoPago.Text = detalle.FechaProximoPago.ToShortDateString();

            ActualizarPendientesCuotas(detalle);
            CambiarColorControles(btnCalcular);

            Session.Add(serviceEstadoCuenta.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "L", "Actualizar", ex);
        }
    }


    private void ActualizarPendientesCuotas(DetalleProducto detalle)
    {
        txtTotCuotasExtras.Visible = false;
        lblTotCuotasExtras.Visible = false;
        var pendCoutas = (from pc in detalle.DetallePagos
                          select new
                          {
                              numero_Radicacion = pc.NumeroRadicacion.ToString(),
                              NumCuota = pc.NumCuota.ToString("#0", CultureInfo.InvariantCulture),
                              IdAvance = pc.idavance.ToString(),
                              FechaCuota = pc.FechaCuota.ToShortDateString().ToString(),
                              Capital = pc.Capital.ToString("n"),
                              IntCte = pc.IntCte.ToString("###,##0", CultureInfo.InvariantCulture),
                              IntMora = pc.IntMora.ToString("###,##0", CultureInfo.InvariantCulture),
                              LeyMiPyme = pc.LeyMiPyme.ToString("###,##0", CultureInfo.InvariantCulture),
                              iva_leymipyme = pc.ivaLeyMiPyme.ToString("###,##0", CultureInfo.InvariantCulture),
                              Poliza = pc.Poliza.ToString("###,##0", CultureInfo.InvariantCulture),
                              garantias_comunitarias = pc.Garantias_Comunitarias.ToString("###,##0", CultureInfo.InvariantCulture),
                              Cobranzas = pc.Cobranzas.ToString("###,##0", CultureInfo.InvariantCulture),
                              Otros = pc.Otros.ToString("###,##0", CultureInfo.InvariantCulture),
                              totalconhonorarios = (pc.Capital + pc.IntCte + pc.IntMora + pc.LeyMiPyme + pc.ivaLeyMiPyme + pc.Poliza + pc.Garantias_Comunitarias + pc.Cobranzas + pc.Otros).ToString("###,##0", CultureInfo.InvariantCulture)

                          });

        if (ChkCuotasExtras.Checked)
        {
            detalle.DetallePagos = ListarConsultaConCuotasExtras(detalle);
            pendCoutas = (from pc in detalle.DetallePagos
                          select new
                          {
                              numero_Radicacion = pc.NumeroRadicacion.ToString(),
                              NumCuota = pc.NumCuota.ToString("#0", CultureInfo.InvariantCulture),
                              IdAvance = pc.idavance.ToString(),
                              FechaCuota = pc.FechaCuota.ToShortDateString().ToString(),
                              Capital = pc.Capital.ToString("n"),
                              IntCte = pc.IntCte.ToString("###,##0", CultureInfo.InvariantCulture),
                              IntMora = pc.IntMora.ToString("###,##0", CultureInfo.InvariantCulture),
                              LeyMiPyme = pc.LeyMiPyme.ToString("###,##0", CultureInfo.InvariantCulture),
                              iva_leymipyme = pc.ivaLeyMiPyme.ToString("###,##0", CultureInfo.InvariantCulture),
                              Poliza = pc.Poliza.ToString("###,##0", CultureInfo.InvariantCulture),
                              garantias_comunitarias = pc.Garantias_Comunitarias.ToString("###,##0", CultureInfo.InvariantCulture),
                              Cobranzas = pc.Cobranzas.ToString("###,##0", CultureInfo.InvariantCulture),
                              Otros = pc.Otros.ToString("###,##0", CultureInfo.InvariantCulture),
                              totalconhonorarios = (pc.Capital + pc.IntCte + pc.IntMora + pc.LeyMiPyme + pc.ivaLeyMiPyme + pc.Poliza + pc.Garantias_Comunitarias + pc.Cobranzas + pc.Otros).ToString("###,##0", CultureInfo.InvariantCulture),
                              
                          });
            txtTotCuotasExtras.Visible = true;
            lblTotCuotasExtras.Visible = true;
        }

        pendCoutas = pendCoutas.ToList();

        gvDistPagosPendCuotas.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
        Session[Usuario.codusuario + "DTCuotasPend"] = pendCoutas;
        gvDistPagosPendCuotas.DataSource = pendCoutas;

        Label1.Text = (from pc in detalle.DetallePagos select (long)pc.Capital).Sum().ToString("###,##0", CultureInfo.InvariantCulture);
        Label2.Text = (from pc in detalle.DetallePagos select (long)pc.IntCte).Sum().ToString("###,##0", CultureInfo.InvariantCulture);
        Label3.Text = (from pc in detalle.DetallePagos select (long)pc.IntMora).Sum().ToString("###,##0", CultureInfo.InvariantCulture);
        Label4.Text = (from pc in detalle.DetallePagos select (long)pc.LeyMiPyme).Sum().ToString("###,##0", CultureInfo.InvariantCulture);
        Label5.Text = (from pc in detalle.DetallePagos select (long)pc.ivaLeyMiPyme).Sum().ToString("###,##0", CultureInfo.InvariantCulture);
        Label6.Text = (from pc in detalle.DetallePagos select (long)pc.Poliza).Sum().ToString("###,##0", CultureInfo.InvariantCulture);
        Label7.Text = (from pc in detalle.DetallePagos select (long)pc.Cobranzas).Sum().ToString("###,##0", CultureInfo.InvariantCulture);
        Label8.Text = (from pc in detalle.DetallePagos select (long)pc.Capital + pc.IntCte + pc.IntMora + pc.LeyMiPyme + pc.ivaLeyMiPyme + pc.Poliza + pc.Garantias_Comunitarias + pc.Cobranzas + pc.Otros).Sum().ToString("###,##0", CultureInfo.InvariantCulture);
        Label9.Text = (from pc in detalle.DetallePagos select (long)pc.Garantias_Comunitarias).Sum().ToString("###,##0", CultureInfo.InvariantCulture);
        Label10.Text = (from pc in detalle.DetallePagos select (long)pc.Otros).Sum().ToString("###,##0", CultureInfo.InvariantCulture);
        Label11.Text = (from pc in detalle.DetallePagos where pc.CuotaExtra == 1 select (long)pc.Capital).Sum().ToString("###,##0", CultureInfo.InvariantCulture);

        if (pendCoutas.Count() > 0)
        {
            gvDistPagosPendCuotas.Visible = true;
            lblInfoPendCuotas.Visible = false;
            lblTotalRegPendCuotas.Visible = true;
            lblTotalRegPendCuotas.Text = "<br/> Registros encontrados " + pendCoutas.Count().ToString();
            gvDistPagosPendCuotas.DataBind();
        }
        else
        {
            gvDistPagosPendCuotas.Visible = false;
        }
    }

    protected void gvDistPagosPendCuotas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvDistPagosPendCuotas.PageIndex = e.NewPageIndex;
            Actualizar();
            switch (itemTab)
            {
                case 0:
                    CambiarColorControles((Control)btnCalcular);
                    break;
                case 1:
                    CambiarColorControles((Control)btnCalcularPagoTotal);
                    break;
                case 2:
                    CambiarColorControles((Control)btnProyeccionTotal);
                    break;
                case 3:
                    CambiarColorControles((Control)btnProximaCuota);
                    break;
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "L", "gvDistPagosPendCuotas_PageIndexChanging", ex);
        }
    }

    protected void ucImprimir_Click(object sender, ImageClickEventArgs evt)
    {
        Session.Remove("imprimirCtrl");
        GridView dtImpresion = new GridView();
        dtImpresion = gvDistPagosPendCuotas;
        dtImpresion.AllowPaging = false;
        dtImpresion.DataSource = Session[Usuario.codusuario + "DTCuotasPend"];
        dtImpresion.DataBind();
        Session["imprimirCtrl"] = dtImpresion;
        ObtenerHeader();
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", Imprimir.JSCRIPT_PRINT);
    }

    protected void ObtenerHeader()
    {
        string pContent = "<table style='width:100%;font-size:11px;font-family:Arial'><tr><td style='width:100%; text-align: center'>" + txtTipoDoc.Text + " - " + txtNumeIdentificacion.Text + " - " + txtNombres.Text;
        pContent += "</td></tr><tr><td style='width:100%; text-align: center;'>Credito : ";
        pContent += txtNoCredito.Text + "</td><tr></table><br/>";
        Session["Header" + Usuario.codusuario] = pContent;
    }

    protected void imgBtnVolverHandler(object sender, ImageClickEventArgs e)
    {
        Navegar("~/Page/Asesores/EstadoCuenta/Detalle.aspx");
    }


    protected void btnCalcular_Click(object sender, EventArgs e)
    {
        DetalleProducto detalle = ListarProductos(calcularTotal: 0);

        ActualizarPendientesCuotas(detalle);
        CambiarColorControles((Control)sender);
    }


    protected void btnCalcularPagoTotal_Click(object sender, EventArgs e)
    {
        DetalleProducto detalle = ListarProductos(calcularTotal: 1);
        ActualizarPendientesCuotas(detalle);
        CambiarColorControles((Control)sender);
    }


    protected void btnProyeccionTotal_Click(object sender, EventArgs e)
    {
        DetalleProducto detalle = ListarProductos(calcularTotal: 2);

        ActualizarPendientesCuotas(detalle);
        CambiarColorControles((Control)sender);
    }


    protected void btnProximaCuota_Click(object sender, EventArgs e)
    {
        DetalleProducto detalle = ListarProductos(calcularTotal: 3);

        ActualizarPendientesCuotas(detalle);
        CambiarColorControles((Control)sender);
    }


    private DetalleProducto ListarProductos(int calcularTotal = 0)
    {
        itemTab = calcularTotal;
        entityProducto = (Producto)(Session[MOV_GRAL_CRED_PRODUC]);
        entityProducto.FechaPago = Convert.ToDateTime(txtFechapago.Text);
        _lstConsulta = serviceEstadoCuenta.ListarDetalleProductos(entityProducto, (Usuario)Session["usuario"], 1, calcularTotal);

        return _lstConsulta.First(s => s.Producto.CodRadicacion == entityProducto.CodRadicacion);
    }

    void CambiarColorControles(Control controlToChange)
    {
        btnCalcular.BackColor = btnCalcular != controlToChange ? System.Drawing.ColorTranslator.FromHtml(_colorToKeep) : System.Drawing.ColorTranslator.FromHtml(_colorToChange);
        btnCalcularPagoTotal.BackColor = btnCalcularPagoTotal != controlToChange ? System.Drawing.ColorTranslator.FromHtml(_colorToKeep) : System.Drawing.ColorTranslator.FromHtml(_colorToChange);
        btnProyeccionTotal.BackColor = btnProyeccionTotal != controlToChange ? System.Drawing.ColorTranslator.FromHtml(_colorToKeep) : System.Drawing.ColorTranslator.FromHtml(_colorToChange);
        btnProximaCuota.BackColor = btnProximaCuota != controlToChange ? System.Drawing.ColorTranslator.FromHtml(_colorToKeep) : System.Drawing.ColorTranslator.FromHtml(_colorToChange);
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvDistPagosPendCuotas.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                GridView gvExportar = new GridView();
                gvExportar = gvDistPagosPendCuotas;
                gvExportar.AllowPaging = false;
                gvExportar.DataSource = Session[Usuario.codusuario + "DTCuotasPend"];
                gvExportar.DataBind();
                gvExportar.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvExportar);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=ProyeccionPagos-" + txtNoCredito.Text + ".xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();

            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch
        {
        }
    }


    protected List<DetallePago> ListarConsultaConCuotasExtras(DetalleProducto detalle)
    {
        DateTime? f_fecha_ant = null;
        Int64? n_numero_cuota = null;
        List<DetallePago> lstdetallepagocuotaex = new List<DetallePago>();
        CuotasExtras eCuoExt = new CuotasExtras();
        eCuoExt.numero_radicacion = Convert.ToInt64(txtNoCredito.Text);
        lstConsulta = CuoExtServicio.ListarCuotasExtras(eCuoExt, (Usuario)Session["Usuario"]);
        foreach (var item in detalle.DetallePagos)
        {
            foreach (var items in lstConsulta.Where(x => x.saldo_capital > 0).OrderBy(x => x.fecha_pago))
            {
                if ((items.fecha_pago >= f_fecha_ant || f_fecha_ant == null) && items.fecha_pago < item.FechaCuota)
                {
                    DetallePago det = new DetallePago();
                    det.NumCuota = item.NumCuota;
                    det.Capital = items.saldo_capital;
                    det.FechaCuota = Convert.ToDateTime(items.fecha_pago);
                    det.NumeroRadicacion = items.numero_radicacion;
                    det.CuotaExtra = 1;
                    lstdetallepagocuotaex.Add(det);
                }
            }
            lstdetallepagocuotaex.Add(item);
            n_numero_cuota = item.NumCuota;
            f_fecha_ant = item.FechaCuota;
        }
        foreach (var items in lstConsulta.Where(x => x.saldo_capital > 0).OrderBy(x => x.fecha_pago))
        {
            if (items.fecha_pago >= f_fecha_ant)
            {
                DetallePago det = new DetallePago();
                det.NumCuota = Convert.ToInt64(n_numero_cuota);
                det.Capital = items.saldo_capital;
                det.FechaCuota = Convert.ToDateTime(items.fecha_pago);
                det.NumeroRadicacion = items.numero_radicacion;
                det.CuotaExtra = 1;
                lstdetallepagocuotaex.Add(det);
            }
        }

        return lstdetallepagocuotaex;
    }

    protected void ChkCuotasExtras_Click(object sender, EventArgs e)
    {
        try
        {
            Actualizar();
            switch (itemTab)
            {
                case 0:
                    CambiarColorControles((Control)btnCalcular);
                    break;
                case 1:
                    CambiarColorControles((Control)btnCalcularPagoTotal);
                    break;
                case 2:
                    CambiarColorControles((Control)btnProyeccionTotal);
                    break;
                case 3:
                    CambiarColorControles((Control)btnProximaCuota);
                    break;
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "L", "gvDistPagosPendCuotas_PageIndexChanging", ex);
        }
    }


}