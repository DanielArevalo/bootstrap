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
    private Xpinn.FabricaCreditos.Services.CuotasExtrasService CuotasExtrasServicio = new Xpinn.FabricaCreditos.Services.CuotasExtrasService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CuotasExtrasServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuotasExtrasServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[CuotasExtrasServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[CuotasExtrasServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(CuotasExtrasServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(CuotasExtrasServicio.CodigoPrograma, "Page_Load", ex);
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
            CuotasExtrasServicio.EliminarCuotasExtras(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuotasExtrasServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[CuotasExtrasServicio.CodigoPrograma + ".id"] = idObjeto;
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
            Xpinn.FabricaCreditos.Entities.CuotasExtras vCuotasExtras = new Xpinn.FabricaCreditos.Entities.CuotasExtras();
            vCuotasExtras = CuotasExtrasServicio.ConsultarCuotasExtras(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vCuotasExtras.cod_cuota != Int64.MinValue)
                txtCod_cuota.Text = vCuotasExtras.cod_cuota.ToString().Trim();
            if (vCuotasExtras.numero_radicacion != Int64.MinValue)
                txtNumero_radicacion.Text = vCuotasExtras.numero_radicacion.ToString().Trim();
            if (vCuotasExtras.fecha_pago != DateTime.MinValue)
                txtFecha_pago.Text = vCuotasExtras.fecha_pago.ToString();
            if (vCuotasExtras.valor != Int64.MinValue)
                txtValor.Text = vCuotasExtras.valor.ToString().Trim();
            if (vCuotasExtras.valor_capital != Int64.MinValue)
                txtValor_capital.Text = vCuotasExtras.valor_capital.ToString().Trim();
            if (vCuotasExtras.valor_interes != Int64.MinValue)
                txtValor_interes.Text = vCuotasExtras.valor_interes.ToString().Trim();
            if (vCuotasExtras.saldo_capital != Int64.MinValue)
                txtSaldo_capital.Text = vCuotasExtras.saldo_capital.ToString().Trim();
            if (vCuotasExtras.saldo_interes != Int64.MinValue)
                txtSaldo_interes.Text = vCuotasExtras.saldo_interes.ToString().Trim();
            if (!string.IsNullOrEmpty(vCuotasExtras.forma_pago))
                txtForma_pago.Text = vCuotasExtras.forma_pago.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuotasExtrasServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }
}