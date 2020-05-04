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

partial class Detalle : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.LineasCreditoService LineasCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(LineasCreditoServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[LineasCreditoServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[LineasCreditoServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(LineasCreditoServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            LineasCreditoServicio.EliminarLineasCredito(idObjeto, (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[LineasCreditoServicio.CodigoPrograma + ".id"] = idObjeto;
        Navegar(Pagina.Nuevo);
    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.LineasCredito vLineasCredito = new Xpinn.FabricaCreditos.Entities.LineasCredito();
            vLineasCredito = LineasCreditoServicio.ConsultarLineasCredito(pIdObjeto, (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vLineasCredito.cod_linea_credito))
                txtCod_linea_credito.Text = vLineasCredito.cod_linea_credito.ToString().Trim();
            if (!string.IsNullOrEmpty(vLineasCredito.nombre))
                txtNombre.Text = vLineasCredito.nombre.ToString().Trim();
            if (vLineasCredito.tipo_linea != Int64.MinValue)
                txtTipo_linea.Text = vLineasCredito.tipo_linea.ToString().Trim();
            if (vLineasCredito.tipo_liquidacion != Int64.MinValue)
                txtTipo_liquidacion.Text = vLineasCredito.tipo_liquidacion.ToString().Trim();
            if (vLineasCredito.tipo_cupo != Int64.MinValue)
                txtTipo_cupo.Text = vLineasCredito.tipo_cupo.ToString().Trim();
            if (vLineasCredito.recoge_saldos != Int64.MinValue)
                txtRecoge_saldos.Text = vLineasCredito.recoge_saldos.ToString().Trim();
            if (vLineasCredito.cobra_mora != Int64.MinValue)
                txtCobra_mora.Text = vLineasCredito.cobra_mora.ToString().Trim();
            if (vLineasCredito.tipo_refinancia != Int64.MinValue)
                txtTipo_refinancia.Text = vLineasCredito.tipo_refinancia.ToString().Trim();
            if (vLineasCredito.minimo_refinancia != Int64.MinValue)
                txtMinimo_refinancia.Text = vLineasCredito.minimo_refinancia.ToString().Trim();
            if (vLineasCredito.maximo_refinancia != Int64.MinValue)
                txtMaximo_refinancia.Text = vLineasCredito.maximo_refinancia.ToString().Trim();
            if (!string.IsNullOrEmpty(vLineasCredito.maneja_pergracia))
                txtManeja_pergracia.Text = vLineasCredito.maneja_pergracia.ToString().Trim();
            if (vLineasCredito.periodo_gracia != Int64.MinValue)
                txtPeriodo_gracia.Text = vLineasCredito.periodo_gracia.ToString().Trim();
            if (!string.IsNullOrEmpty(vLineasCredito.tipo_periodic_gracia))
                txtTipo_periodic_gracia.Text = vLineasCredito.tipo_periodic_gracia.ToString().Trim();
            if (!string.IsNullOrEmpty(vLineasCredito.modifica_datos))
                txtModifica_datos.Text = vLineasCredito.modifica_datos.ToString().Trim();
            if (!string.IsNullOrEmpty(vLineasCredito.modifica_fecha_pago))
                txtModifica_fecha_pago.Text = vLineasCredito.modifica_fecha_pago.ToString().Trim();
            if (!string.IsNullOrEmpty(vLineasCredito.garantia_requerida))
                txtGarantia_requerida.Text = vLineasCredito.garantia_requerida.ToString().Trim();
            if (vLineasCredito.tipo_capitalizacion != Int64.MinValue)
                txtTipo_capitalizacion.Text = vLineasCredito.tipo_capitalizacion.ToString().Trim();
            if (vLineasCredito.cuotas_extras != Int64.MinValue)
                txtCuotas_extras.Text = vLineasCredito.cuotas_extras.ToString().Trim();
            if (vLineasCredito.cod_clasifica != Int64.MinValue)
                txtCod_clasifica.Text = vLineasCredito.cod_clasifica.ToString().Trim();
            if (vLineasCredito.numero_codeudores != Int64.MinValue)
                txtNumero_codeudores.Text = vLineasCredito.numero_codeudores.ToString().Trim();
            if (vLineasCredito.cod_moneda != Int64.MinValue)
                txtCod_moneda.Text = vLineasCredito.cod_moneda.ToString().Trim();
            if (vLineasCredito.porc_corto != Int64.MinValue)
                txtPorc_corto.Text = vLineasCredito.porc_corto.ToString().Trim();
            if (vLineasCredito.tipo_amortiza != Int64.MinValue)
                txtTipo_amortiza.Text = vLineasCredito.tipo_amortiza.ToString().Trim();
            if (vLineasCredito.estado != Int64.MinValue)
                txtEstado.Text = vLineasCredito.estado.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }
}