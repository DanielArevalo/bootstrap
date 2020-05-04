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

public partial class EstadoCuentaAcodeudadoDetalle : GlobalWeb
{
    EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
    AcodeudadoService serviceacodeudado = new AcodeudadoService();
   
    List<DetalleProducto> lstConsulta = new List<DetalleProducto>();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try{
            VisualizarOpciones(serviceacodeudado.CodigoPrograma, "L");
           
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
            if (!IsPostBack){
                Actualizar();
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
    protected void gvAcodeudados_PageIndexChanging(object sender, GridViewPageEventArgs evt)
    {
        try
        {
            gvAcodeudados.PageIndex = evt.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        if (Request.QueryString["codigoCliente"] != null)
            ActualizarCodeudores(serviceEstadoCuenta.ListarAcodeudados(new Cliente() { IdCliente = Convert.ToInt64(Request.QueryString["codigoCliente"]) }, (Usuario)Session["usuario"]));
    }
    
    private void ActualizarCodeudores(List<Acodeudados> pAcodeudados)
    {
        var acodeudados = from c in pAcodeudados
                          select new
                          {
                              c.CodPersona,
                              Cuota = c.Cuota.ToString("##,##0", CultureInfo.InvariantCulture),
                              c.Estado,
                              FechaProxPago = c.FechaProxPago.ToShortDateString(),
                              c.Linea,
                              Monto = c.Monto.ToString("##,##0", CultureInfo.InvariantCulture),
                              c.Nombres,
                              c.NumRadicacion,
                              Saldo = c.Saldo.ToString("##,##0", CultureInfo.InvariantCulture)
                          };

        acodeudados = acodeudados.OrderBy(c => c.NumRadicacion).ToList();

        gvAcodeudados.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
        gvAcodeudados.DataSource = acodeudados;

        if (acodeudados.Count() > 0)
        {
            gvAcodeudados.Visible = true;
            lblInfo.Visible = false;
            lblTotalReg.Visible = true;
            lblTotalReg.Text = "<br/> Registros encontrados " + acodeudados.Count().ToString();
            gvAcodeudados.DataBind();
            ValidarPermisosGrilla(gvAcodeudados);
        }
        else
        {
            gvAcodeudados.Visible = false;
        }

    }

    protected void ucImprimir_Click(object sender, ImageClickEventArgs evt)
    {
        Session.Remove("imprimirCtrl");
        Session["imprimirCtrl"] = gvAcodeudados;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", Imprimir.JSCRIPT_PRINT);
    }

    protected void imgBtnVolverHandler(object sender, ImageClickEventArgs e)
    {
        Navegar("~/Page/Asesores/EstadoCuenta/Detalle.aspx");
    }
}