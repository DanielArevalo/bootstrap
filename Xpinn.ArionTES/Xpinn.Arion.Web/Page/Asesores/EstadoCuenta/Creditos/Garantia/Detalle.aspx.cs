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

public partial class EstadoCuentaGarantiaDetalle : GlobalWeb
{
    EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
    GarantiaService serviceGarantia = new GarantiaService();

    Producto entityProducto;
    List<DetalleProducto> lstConsulta = new List<DetalleProducto>();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(serviceGarantia.CodigoPrograma, "L");

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
                if (Session[MOV_GRAL_CRED_PRODUC] != null){
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

    protected void gvGtias_PageIndexChanging(object sender, GridViewPageEventArgs evt)
    {
        try{
            gvGtias.PageIndex = evt.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            entityProducto = (Producto)(Session[MOV_GRAL_CRED_PRODUC]);
            lstConsulta = serviceEstadoCuenta.ListarDetalleProductos(entityProducto, (Usuario)Session["usuario"],1);

            var lstDetalle = (from l in lstConsulta
                          where l.Producto.CodLineaCredito == entityProducto.CodLineaCredito && l.Producto.CodRadicacion == entityProducto.CodRadicacion
                          select l).ToList();
                    
            var detalle = lstDetalle.First(s => s.Producto.CodLineaCredito == entityProducto.CodLineaCredito);//CodRadicacion
            
            txtTipoDoc.Text             = detalle.Producto.Persona.TipoIdentificacion.NombreTipoIdentificacion;
            txtNumeIdentificacion.Text  = detalle.Producto.Persona.NumeroDocumento.ToString();
            txtNombres.Text = detalle.Producto.Persona.PrimerNombre + detalle.Producto.Persona.SegundoNombre + detalle.Producto.Persona.PrimerApellido + detalle.Producto.Persona.SegundoApellido;
             txtNoCredito.Text           = detalle.NumeroRadicacion.ToString();
            //txtCodLinea.Text            = detalle.Producto.CodLineaCredito.ToString();
            txtNombLinea.Text           = detalle.Producto.Linea;
            txtEstaCredito.Text         = detalle.Producto.Estado;

            ActualizarGarantias(detalle);

            Session.Add(serviceEstadoCuenta.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private void ActualizarGarantias(DetalleProducto detalle)
    {
        var gtias = from g in detalle.Garantias
                    select new
                    {
                        NoGtia = g.NumeroRadicacion,
                        g.Tipo,
                        FechaGarantia = g.FechaGarantia.ToShortDateString().ToString(),
                        g.Descripcion,
                        EstadoGtia = g.Estado
                    };

        gtias = gtias.OrderBy(g => g.Tipo).ToList();


        gvGtias.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
        gvGtias.DataSource = gtias;

        if (gtias.Count() > 0)
        {
            gvGtias.Visible = true;
            lblInfo.Visible = false;
            lblTotalReg.Visible = true;
            lblTotalReg.Text = "<br/> Registros encontrados " + gtias.Count().ToString();
            gvGtias.DataBind();
            ValidarPermisosGrilla(gvGtias);
        }
        else
        {
            gvGtias.Visible = false;
        }
    }

    protected void ucImprimir_Click(object sender, ImageClickEventArgs evt)
    {
        Session.Remove("imprimirCtrl");
        Session["imprimirCtrl"] = gvGtias;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", Imprimir.JSCRIPT_PRINT);
    }

    protected void imgBtnVolverHandler(object sender, ImageClickEventArgs e)
    {
        Navegar("~/Page/Asesores/EstadoCuenta/Detalle.aspx");
    }

}