using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.CuotasExtrasService CuotasExtrasServicio = new Xpinn.FabricaCreditos.Services.CuotasExtrasService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[CuotasExtrasServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(CuotasExtrasServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(CuotasExtrasServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
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

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.CuotasExtras vCuotasExtras = new Xpinn.FabricaCreditos.Entities.CuotasExtras();

            if (idObjeto != "")
                vCuotasExtras = CuotasExtrasServicio.ConsultarCuotasExtras(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (txtCod_cuota.Text != "") vCuotasExtras.cod_cuota = Convert.ToInt64(txtCod_cuota.Text.Trim());
            if (txtNumero_radicacion.Text != "") vCuotasExtras.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text.Trim());
            if (txtFecha_pago.Text != "") vCuotasExtras.fecha_pago = Convert.ToDateTime(txtFecha_pago.Text.Trim());
            if (txtValor.Text != "") vCuotasExtras.valor = Convert.ToInt64(txtValor.Text.Trim());
            if (txtValor_capital.Text != "") vCuotasExtras.valor_capital = Convert.ToInt64(txtValor_capital.Text.Trim());
            if (txtValor_interes.Text != "") vCuotasExtras.valor_interes = Convert.ToInt64(txtValor_interes.Text.Trim());
            if (txtSaldo_capital.Text != "") vCuotasExtras.saldo_capital = Convert.ToInt64(txtSaldo_capital.Text.Trim());
            if (txtSaldo_interes.Text != "") vCuotasExtras.saldo_interes = Convert.ToInt64(txtSaldo_interes.Text.Trim());
            //if (txtForma_pago.Text != "") vCuotasExtras.forma_pago = Convert.ToString(txtForma_pago.Text.Trim());
            vCuotasExtras.forma_pago = (txtForma_pago.Text != "") ? Convert.ToString(txtForma_pago.Text.Trim()) : String.Empty;

            if (idObjeto != "")
            {
                vCuotasExtras.cod_cuota = Convert.ToInt64(idObjeto);
                CuotasExtrasServicio.ModificarCuotasExtras(vCuotasExtras, (Usuario)Session["usuario"]);
            }
            else
            {
                vCuotasExtras = CuotasExtrasServicio.CrearCuotasExtras(vCuotasExtras, (Usuario)Session["usuario"]);
                idObjeto = vCuotasExtras.cod_cuota.ToString();
            }

            Session[CuotasExtrasServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuotasExtrasServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[CuotasExtrasServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.CuotasExtras vCuotasExtras = new Xpinn.FabricaCreditos.Entities.CuotasExtras();
            vCuotasExtras = CuotasExtrasServicio.ConsultarCuotasExtras(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vCuotasExtras.cod_cuota != Int64.MinValue)
                txtCod_cuota.Text = HttpUtility.HtmlDecode(vCuotasExtras.cod_cuota.ToString().Trim());
            if (vCuotasExtras.numero_radicacion != Int64.MinValue)
                txtNumero_radicacion.Text = HttpUtility.HtmlDecode(vCuotasExtras.numero_radicacion.ToString().Trim());
            if (vCuotasExtras.fecha_pago != DateTime.MinValue)
                txtFecha_pago.Text = HttpUtility.HtmlDecode(vCuotasExtras.fecha_pago.ToString());
            if (vCuotasExtras.valor != Int64.MinValue)
                txtValor.Text = HttpUtility.HtmlDecode(vCuotasExtras.valor.ToString().Trim());
            if (vCuotasExtras.valor_capital != Int64.MinValue)
                txtValor_capital.Text = HttpUtility.HtmlDecode(vCuotasExtras.valor_capital.ToString().Trim());
            if (vCuotasExtras.valor_interes != Int64.MinValue)
                txtValor_interes.Text = HttpUtility.HtmlDecode(vCuotasExtras.valor_interes.ToString().Trim());
            if (vCuotasExtras.saldo_capital != Int64.MinValue)
                txtSaldo_capital.Text = HttpUtility.HtmlDecode(vCuotasExtras.saldo_capital.ToString().Trim());
            if (vCuotasExtras.saldo_interes != Int64.MinValue)
                txtSaldo_interes.Text = HttpUtility.HtmlDecode(vCuotasExtras.saldo_interes.ToString().Trim());
            if (!string.IsNullOrEmpty(vCuotasExtras.forma_pago))
                txtForma_pago.Text = HttpUtility.HtmlDecode(vCuotasExtras.forma_pago.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuotasExtrasServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
}