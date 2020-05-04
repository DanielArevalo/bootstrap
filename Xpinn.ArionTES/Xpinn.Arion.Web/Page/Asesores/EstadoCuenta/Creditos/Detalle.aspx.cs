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
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;

public partial class EstadoCuentaCreditoDetalle : GlobalWeb
{

    EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
    DetalleProductoService serviceDetalleProducto = new DetalleProductoService();
    GarantiaService serviceGarantia = new GarantiaService();
    Producto entityProducto;
    List<DetalleProducto> lstConsulta = new List<DetalleProducto>();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(serviceDetalleProducto.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            ucImprimir.PrintCustomEvent += ucImprimir_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
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
                if (Session[MOV_GRAL_CRED_PRODUC] != null)
                {
                    entityProducto = (Producto)(Session[MOV_GRAL_CRED_PRODUC]);
                    lstConsulta = serviceEstadoCuenta.ListarDetalleProductos(entityProducto, (Usuario)Session["usuario"], 1);
                    Actualizar(Convert.ToInt64(entityProducto.CodRadicacion));
                    if (!string.IsNullOrEmpty(entityProducto.Persona.PrimerApellido)) txtCliente.Text = entityProducto.Persona.PrimerNombre.Trim().ToString() + " " + entityProducto.Persona.PrimerApellido.Trim().ToString();

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


    Xpinn.FabricaCreditos.Data.consultasdatacreditoData vData = new Xpinn.FabricaCreditos.Data.consultasdatacreditoData();
    private void Actualizar(Int64 codRadicaion)
    {
        try
        {
            var detalle = lstConsulta.First(s => Convert.ToInt64(s.Producto.CodRadicacion) == codRadicaion);

            txtEstado.Text = detalle.Producto.Estado;
            txtNoCredito.Text = detalle.NumeroRadicacion.ToString();
            txtPagare.Text = detalle.NumeroObligacion.ToString();
            txtDestinacion.Text = detalle.Destinacion;
            txtEstadoCredito.Text = detalle.EstadoCredito;
            txtLinea.Text = detalle.Producto.Linea;
            txtEstado.Text = detalle.Producto.Estado;
            txtOficina.Text = detalle.Producto.Oficina.NombreOficina;
            txtFormaPago.Text = detalle.FormaPago;
            txtCuotasPagas.Text = detalle.Producto.CuotasPagadas.ToString();
            txtPlazo.Text = detalle.Producto.Plazo.ToString();
            txtFormatoPago.Text = detalle.FormaPago;
            txtPeriocidad.Text = detalle.periodicidad;
            txtTasaNM.Text = detalle.TasaNM.ToString();
            txtCalifPromedio.Text = detalle.Producto.CalifPromedio.ToString();
            txtMontoSolicitado.Text = detalle.MontoSolicitado.ToString();
            txtMontoAprobado.Text = detalle.Producto.MontoAprobado.ToString();
            txtSaldoCapital.Text = detalle.Producto.SaldoCapital.ToString();
            txtCuotas.Text = detalle.Producto.Cuota.ToString();
            txtAtributos.Text = detalle.Producto.Atributos.ToString();
            txtSaldoPendiente.Text = detalle.SaldoPendiente.ToString();
            txtVlrAPagar.Text = detalle.Producto.ValorAPagar.ToString();
            txtVlrTotalAPagar.Text = detalle.Producto.ValorTotalAPagar.ToString();

            if (detalle.Producto.FechaSolicitud.ToString(GlobalWeb.gFormatoFecha) == "01/01/0001")
                TxtFechaSolicitud.Text = "      ";
            else
                TxtFechaSolicitud.Text = detalle.Producto.FechaSolicitud.ToString(GlobalWeb.gFormatoFecha);

            if (detalle.FechaAprobacion.ToString(GlobalWeb.gFormatoFecha) == "01/01/0001")
                TxtFechaAprobacion.Text = "      ";
            else
                TxtFechaAprobacion.Text = detalle.FechaAprobacion.ToString(GlobalWeb.gFormatoFecha);

            if (detalle.FechaDesembolso.ToString(GlobalWeb.gFormatoFecha) == "01/01/0001")
                TxtFechaDesembolso.Text = "      ";
            else
                TxtFechaDesembolso.Text = detalle.FechaDesembolso.ToString(GlobalWeb.gFormatoFecha);

            if (detalle.FechaTerminacion.ToString(GlobalWeb.gFormatoFecha) == "01/01/0001")
                TxtFechaTerminacion.Text = "      ";
            else
                TxtFechaTerminacion.Text = detalle.FechaTerminacion.ToString(GlobalWeb.gFormatoFecha);

            if (detalle.FechaTerminacion.ToString(GlobalWeb.gFormatoFecha) == "01/01/0001")
                TxtFechaTerminacion.Text = "      ";
            else
                TxtFechaTerminacion.Text = detalle.FechaTerminacion.ToString(GlobalWeb.gFormatoFecha);
            if (detalle.FechaUltimoPago.ToString(GlobalWeb.gFormatoFecha) == "01/01/0001")
                TxtFechaUltimoPago.Text = "      ";
            else
                TxtFechaUltimoPago.Text = detalle.FechaUltimoPago.ToString(GlobalWeb.gFormatoFecha);
            if (detalle.FechaProximoPago.ToString(GlobalWeb.gFormatoFecha) == "01/01/0001")
                TxtFechaProximoPago.Text = "      ";
            else
                TxtFechaProximoPago.Text = detalle.FechaProximoPago.ToString(GlobalWeb.gFormatoFecha);

            ActualizarCodeudores(detalle);
            ActualizarDocumentos(detalle);
            ActualizarGarantias(detalle);
            ActualizarReferencias(detalle.NumeroRadicacion);

            //RECUPERANDO DATOS DE EMPRESAS DE RECAUDO            
            List<Xpinn.FabricaCreditos.Entities.CreditoEmpresaRecaudo> lstEmpresas = new List<Xpinn.FabricaCreditos.Entities.CreditoEmpresaRecaudo>();
            lstEmpresas = vData.ListarCreditoEmpresa_Recaudo(codRadicaion, (Usuario)Session["usuario"]);
            gvEmpresaRecaudora.DataSource = lstEmpresas;
            if (lstEmpresas.Count > 0)
            {
                gvEmpresaRecaudora.Visible = true;
                gvEmpresaRecaudora.DataBind();
                lblInfoEmpresas.Visible = false;
                lblTotalEmpresas.Visible = true;
                lblTotalEmpresas.Text = "<br/> Registros encontrados " + lstEmpresas.Count().ToString();
            }
            else
            {
                gvEmpresaRecaudora.Visible = false;
                lblInfoEmpresas.Visible = true;
                lblTotalEmpresas.Visible = false;
            }

            // Traer los datos de las cuotas extras
            // ---------------------------------------------------------------------------------------------------------
            List<CuotasExtras> lstConsultas = new List<CuotasExtras>();
            CuotasExtras eCuoExt = new CuotasExtras();
            CuotasExtrasService CuoExtServicio = new CuotasExtrasService();
            eCuoExt.numero_radicacion = Convert.ToInt64(codRadicaion);
            lstConsultas = CuoExtServicio.ListarCuotasExtras(eCuoExt, (Usuario)Session["usuario"]);
            gvCuotas.DataSource = lstConsultas;
            Session["lstCuotasExtras"] = lstConsultas;
            if (lstConsultas.Count > 0)
            {
                gvCuotas.Visible = true;
                gvCuotas.DataBind();
            }
            else
            {
                gvCuotas.Visible = false;

            }

            //RECUPERAR DATOS DE OBSERVACIONES
            if (Convert.ToInt64(entityProducto.CodRadicacion) != 0 && entityProducto.CodRadicacion != null)
                ActualizarObservaciones();

            Session.Add(serviceEstadoCuenta.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "L", "Actualizar", ex);
        }
    }

    protected void gvObservaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvObservaciones.PageIndex = e.NewPageIndex;
        ActualizarObservaciones();
    }

    protected void gvCuotas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCuotas.PageIndex = e.NewPageIndex;
        List<CuotasExtras> lstConsultas = new List<CuotasExtras>();
        if (Session["lstCuotasExtras"] != null)
            lstConsultas = (List<CuotasExtras>)Session["lstCuotasExtras"];
    }

    private void ActualizarObservaciones()
    {
        List<Xpinn.FabricaCreditos.Entities.ControlCreditos> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.ControlCreditos>();
        entityProducto = (Producto)(Session[MOV_GRAL_CRED_PRODUC]);
        lstConsulta = vData.ListarObservacionesCreditos(Convert.ToInt64(entityProducto.CodRadicacion), (Usuario)Session["usuario"]);
        if (lstConsulta.Count > 0)
        {
            gvObservaciones.Visible = true;
            gvObservaciones.DataSource = lstConsulta;
            gvObservaciones.DataBind();
            lblInfoObser.Visible = false;
            lblTotalRegObser.Visible = true;
            lblTotalRegObser.Text = "<br/> Registros encontrados " + lstConsulta.Count().ToString();
        }
        else
        {
            gvObservaciones.Visible = false;
            lblInfoObser.Visible = true;
            lblTotalRegObser.Visible = false;
        }
    }

    private void ActualizarDocumentos(DetalleProducto detalle)
    {
        var docs = from d in detalle.Documentos select new { d.NumeroRadicacion, d.Referencia, d.Descripcion };

        docs = docs.OrderBy(d => d.NumeroRadicacion).ToList();

        gvDocumentos.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
        gvDocumentos.DataSource = docs;

        if (docs.Count() > 0)
        {
            gvDocumentos.Visible = true;
            lblInfoDocs.Visible = false;
            lblTotalDocs.Visible = true;
            lblTotalDocs.Text = "<br/> Registros encontrados " + docs.Count().ToString();
            gvDocumentos.DataBind();
            ValidarPermisosGrilla(gvCodeudores);
        }
        else
        {
            gvDocumentos.Visible = false;
            lblInfoDocs.Visible = true;
            lblTotalDocs.Visible = false;
        }
    }

    private void ActualizarCodeudores(DetalleProducto detalle)
    {
        var codeudores = from c in detalle.Producto.Codeudores
                         select new
                         {
                             c.IdCodeudor,
                             NombreCodeudor = (c.Persona.PrimerNombre + c.Persona.SegundoNombre + c.Persona.PrimerApellido + c.Persona.SegundoApellido),
                             DireccionCodeudor = c.Persona.Direccion,
                             TelefonoCodeudor = c.Persona.Telefono,
                             c.Persona.NumeroDocumento,
                             c.orden
                         };

        //codeudores = codeudores.OrderBy(c => c.NombreCodeudor).ToList();

        codeudores = codeudores.OrderBy(c => c.orden).ToList();

        gvCodeudores.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
        gvCodeudores.DataSource = codeudores;


        if (codeudores.Count() > 0)
        {
            gvCodeudores.Visible = true;
            lblInfoCodeudores.Visible = false;
            lblTotalRegCodeudores.Visible = true;
            lblTotalRegCodeudores.Text = "<br/> Registros encontrados " + codeudores.Count().ToString();
            gvCodeudores.DataBind();
            ValidarPermisosGrilla(gvCodeudores);
        }
        else
        {
            gvCodeudores.Visible = false;
            lblInfoCodeudores.Visible = true;
            lblTotalRegCodeudores.Visible = false;
        }

    }

    private void ActualizarGarantias(DetalleProducto detalle)
    {

        if (detalle.Garantias.Find(x => x.Estado != "3") != null)
        {
            var gtia = from g in detalle.Garantias
                       select new { g.NumeroRadicacion, g.Tipo, FechaGarantia = g.FechaGarantia.ToShortDateString().ToString(), g.Valor, g.Descripcion, g.Estado };

            gtia = gtia.OrderBy(g => g.NumeroRadicacion).ToList();

            gvGarantias.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            gvGarantias.DataSource = gtia;

            if (gtia.Count() > 0)
            {
                gvGarantias.Visible = true;
                lblInfoGtia.Visible = false;
                lblTotalRegGtia.Visible = true;
                lblTotalRegGtia.Text = "<br/> Registros encontrados " + gtia.Count().ToString();
                gvGarantias.DataBind();
                ValidarPermisosGrilla(gvGarantias);
            }
            else
            {
                gvGarantias.Visible = false;
                lblInfoGtia.Visible = true;
                lblTotalRegGtia.Visible = false;
            }

        }
    }

    private void ActualizarReferencias(Int64 pnumero_radicacion)
    {
        Xpinn.FabricaCreditos.Services.ReferenciaService ReferenciaServicio = new Xpinn.FabricaCreditos.Services.ReferenciaService();
        List<Xpinn.FabricaCreditos.Entities.Referencia> lstReferencias = new List<Xpinn.FabricaCreditos.Entities.Referencia>();
        lstReferencias = ReferenciaServicio.ConsultarReferenciacionCredito(pnumero_radicacion, (Usuario)Session["Usuario"]);
        gvReferencias.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
        gvReferencias.DataSource = lstReferencias;

        if (lstReferencias.Count() > 0)
        {
            gvReferencias.Visible = true;
            lblInfoReferencias.Visible = false;
            lblTotalRegReferencias.Visible = true;
            lblTotalRegReferencias.Text = "<br/> Registros encontrados " + lstReferencias.Count().ToString();
            gvReferencias.DataBind();
            ValidarPermisosGrilla(gvReferencias);
        }
        else
        {
            gvReferencias.Visible = false;
            lblInfoReferencias.Visible = true;
            lblTotalRegReferencias.Visible = false;
        }

    }


    protected void ucImprimir_Click(object sender, ImageClickEventArgs evt)
    {
        //Ignore
    }


}