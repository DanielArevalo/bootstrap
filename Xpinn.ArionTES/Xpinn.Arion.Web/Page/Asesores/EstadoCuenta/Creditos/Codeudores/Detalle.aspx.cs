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
using System.Data;

public partial class EstadoCuentaCodeudoresDetalle : GlobalWeb
{
    EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
    CodeudorService serviceCodeudores = new CodeudorService();
    Producto entityProducto;
    List<DetalleProducto> lstConsulta = new List<DetalleProducto>();
    private static string NAME_CACHE = "Codeudores";

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(serviceCodeudores.CodigoPrograma, "L");

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
        try{
            if (!IsPostBack){
                ObtenerCodeudores();
            }
        }
        catch (ExceptionBusiness ex){
            VerError(ex.Message);
        }
        catch (Exception ex){
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "D", "Page_Load", ex);
        }
    }
    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {

        Navegar("~/Page/Asesores/EstadoCuenta/Detalle.aspx");


    }
    private void ObtenerCodeudores()
    {
        if (Session[MOV_GRAL_CRED_PRODUC] != null)
        {
            entityProducto = (Producto)(Session[MOV_GRAL_CRED_PRODUC]);

            String nameCache = NAME_CACHE + entityProducto.CodRadicacion;
            object cacheValue = System.Web.HttpRuntime.Cache.Get(nameCache);
            DateTime timeExpiration = DateTime.Now.AddSeconds(45);

            if (cacheValue == null){
                lstConsulta = serviceEstadoCuenta.ListarDetalleProductos(entityProducto, (Usuario)Session["usuario"],1);
                System.Web.HttpRuntime.Cache.Add(nameCache, lstConsulta, null, timeExpiration, TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
            }
            else{
                lstConsulta = (List<DetalleProducto>)System.Web.HttpRuntime.Cache.Get(nameCache);
            }
            Actualizar();
        }
    }

    protected void gvCodeudores_PageIndexChanging(object sender, GridViewPageEventArgs evt)
    {
        try{
            gvCodeudores.PageIndex = evt.NewPageIndex;
            ObtenerCodeudores();
        }
        catch (Exception ex){
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            var detalle = lstConsulta.First(s => s.Producto.CodLineaCredito == entityProducto.CodLineaCredito);

            txtNoCredito.Text   = detalle.NumeroRadicacion.ToString();
            txtEstaCredito.Text = detalle.EstadoCredito.ToString();
            txtNombLinea.Text   = detalle.Producto.Linea;
            txtNombres.Text     = detalle.Producto.Persona.PrimerNombre + (string.IsNullOrEmpty(detalle.Producto.Persona.SegundoNombre) ? " " : detalle.Producto.Persona.SegundoNombre ) +" "+ detalle.Producto.Persona.PrimerApellido + (string.IsNullOrEmpty(detalle.Producto.Persona.SegundoApellido) ? " " : detalle.Producto.Persona.SegundoApellido);
            txtMonto.Text       = detalle.Producto.ValorTotalAPagar.ToString();
            txtSaldo.Text       = detalle.Producto.SaldoCapital.ToString();
            txtCouta.Text       = detalle.Producto.ValorCuota.ToString();
            txtProPago.Text     = detalle.FechaProximoPago.ToShortDateString();

            ActualizarCodeudores(detalle);

            Session.Add(serviceEstadoCuenta.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex){
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private void ActualizarCodeudores(DetalleProducto detalle)
    {
        var lstCodeudores = from c in detalle.Producto.Codeudores
                            select new
                            {
                                Nombres = c.Persona.PrimerNombre + c.Persona.SegundoNombre,
                                Apellidos = c.Persona.PrimerApellido + c.Persona.SegundoApellido,
                                Direccion = c.Persona.Direccion,
                                Telefono = c.Persona.Telefono,
                                c.Persona.NumeroDocumento
                            };

        lstCodeudores = lstCodeudores.OrderBy(c => c.Nombres).ToList();

        gvCodeudores.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
        gvCodeudores.DataSource = lstCodeudores;

        if (lstCodeudores.Count() > 0)
        {
            gvCodeudores.Visible = true;
            lblInfo.Visible = false;
            lblTotalReg.Visible = true;
            lblTotalReg.Text = "<br/> Registros encontrados " + lstCodeudores.Count().ToString();
            gvCodeudores.DataBind();
            ValidarPermisosGrilla(gvCodeudores);
        }
        else
        {
            gvCodeudores.Visible = false;
        }

    }

    protected void ucImprimir_Click(object sender, ImageClickEventArgs evt)
    {
        Session.Remove("imprimirCtrl");
        Session["imprimirCtrl"] = gvCodeudores;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", Imprimir.JSCRIPT_PRINT);
    }

    protected void imgBtnVolverHandler(object sender, ImageClickEventArgs e)
    {
        Navegar("~/Page/Asesores/EstadoCuenta/Detalle.aspx");
    }
}