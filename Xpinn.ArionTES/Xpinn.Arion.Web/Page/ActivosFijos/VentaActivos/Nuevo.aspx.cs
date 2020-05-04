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
using Xpinn.ActivosFijos.Entities;
using Xpinn.ActivosFijos.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Nuevo : GlobalWeb
{
    private Xpinn.ActivosFijos.Services.ActivosFijoservices activosServicio = new Xpinn.ActivosFijos.Services.ActivosFijoservices();
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[activosServicio.CodigoProgramaVenta + ".id"] != null)
                VisualizarOpciones(activosServicio.CodigoProgramaVenta, "E");
            else
                VisualizarOpciones(activosServicio.CodigoProgramaVenta, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.MostrarConsultar(false);
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(activosServicio.CodigoProgramaVenta, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvActivosFijos.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;
                CargarListas();
                txtFechaVenta.ToDateTime = System.DateTime.Now;
                if (Session[activosServicio.CodigoProgramaVenta + ".id"] != null)
                {
                    idObjeto = Session[activosServicio.CodigoProgramaVenta + ".id"].ToString();
                    Session.Remove(activosServicio.CodigoProgramaVenta + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtCodigo.Text = activosServicio.ObtenerSiguienteCodigo((Usuario)Session["Usuario"]).ToString();
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(activosServicio.CodigoProgramaVenta, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        ctlMensaje.MostrarMensaje("Desea guardar los datos de la venta del activo fijo?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario vUsuario = new Usuario();
            vUsuario = (Usuario)Session["Usuario"];
            Xpinn.ActivosFijos.Entities.ActivoFijo vActivoFijo = new Xpinn.ActivosFijos.Entities.ActivoFijo();
            idObjeto = lblConsecutivo.Text;
            Int64 pCodOpe = 0;
            string pError = "";
            vActivoFijo = activosServicio.ConsultarActivoFijo(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            vActivoFijo.fecha_venta = txtFechaVenta.ToDateTime;
            vActivoFijo.valor_venta = Convert.ToDecimal(txtValorVenta.Text.Replace(".", ""));
            vActivoFijo.cod_comprador = Convert.ToInt64(ddlComprador.SelectedItem.Value);
            if (activosServicio.VentaActivoFijo(vActivoFijo, ref pCodOpe, ref pError, (Usuario)Session["usuario"]) == false)
                return;
            if (pError != "")
            {
                VerError(pError);
                return;
            }
            Session[activosServicio.CodigoProgramaVenta + ".id"] = idObjeto;
            //mvActivosFijos.ActiveViewIndex = 1;
            Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = pCodOpe;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 22;
            Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = Convert.ToInt64(ddlComprador.SelectedItem.Value);
            Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(activosServicio.CodigoProgramaVenta, "btnGuardar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Configuracion conf = new Configuracion();
            Xpinn.ActivosFijos.Entities.ActivoFijo vActivoFijo = new Xpinn.ActivosFijos.Entities.ActivoFijo();
            vActivoFijo = activosServicio.ConsultarActivoFijo(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            lblConsecutivo.Text = HttpUtility.HtmlDecode(vActivoFijo.consecutivo.ToString().Trim());
            txtCodigo.Text = HttpUtility.HtmlDecode(vActivoFijo.cod_act.ToString().Trim());
            if (!string.IsNullOrEmpty(vActivoFijo.clase.ToString()))
                ddlClase.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.clase.ToString().Trim());
            if (!string.IsNullOrEmpty(vActivoFijo.tipo.ToString()))
                ddlTipo.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.tipo.ToString().Trim());
            if (!string.IsNullOrEmpty(vActivoFijo.cod_ubica.ToString()))
                ddlUbicacion.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.cod_ubica.ToString().Trim());
            if (!string.IsNullOrEmpty(vActivoFijo.cod_costo.ToString()))
                ddlCentroCosto.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.cod_costo.ToString().Trim());
            if (!string.IsNullOrEmpty(vActivoFijo.nombre))
                txtNombre.Text = HttpUtility.HtmlDecode(vActivoFijo.nombre.ToString());
            if (vActivoFijo.anos_util.ToString() != "")
                txtVidaUtil.Text = HttpUtility.HtmlDecode(vActivoFijo.anos_util.ToString().Trim());
            if (!string.IsNullOrEmpty(vActivoFijo.estado.ToString()))
                lblEstado.Text = HttpUtility.HtmlDecode(vActivoFijo.estado.ToString().Trim());
            if (!string.IsNullOrEmpty(vActivoFijo.nomestado))
                txtEstado.Text = HttpUtility.HtmlDecode(vActivoFijo.nomestado);
            if (!string.IsNullOrEmpty(vActivoFijo.serial))
                txtSerial.Text = HttpUtility.HtmlDecode(vActivoFijo.serial);
            if (!string.IsNullOrEmpty(vActivoFijo.cod_encargado.ToString()))
                ddlEncargado.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.cod_encargado.ToString().Trim());
            if (!string.IsNullOrEmpty(vActivoFijo.cod_proveedor.ToString()))
                ddlProveedor.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.cod_proveedor.ToString().Trim());
            if (!string.IsNullOrEmpty(vActivoFijo.fecha_compra.ToString()))
                txtFechaCompra.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vActivoFijo.fecha_compra.ToString()));
            if (!string.IsNullOrEmpty(vActivoFijo.valor_compra.ToString()))
                txtValorCompra.Text = HttpUtility.HtmlDecode(vActivoFijo.valor_compra.ToString());
            if (!string.IsNullOrEmpty(vActivoFijo.valor_avaluo.ToString()))
                txtValorAvaluo.Text = HttpUtility.HtmlDecode(vActivoFijo.valor_avaluo.ToString());
            if (!string.IsNullOrEmpty(vActivoFijo.valor_salvamen.ToString()))
                txtValorSalvamento.Text = HttpUtility.HtmlDecode(vActivoFijo.valor_salvamen.ToString());
            if (!string.IsNullOrEmpty(vActivoFijo.num_factura.ToString()))
                txtFactura.Text = HttpUtility.HtmlDecode(vActivoFijo.num_factura.ToString().Trim());
            if (!string.IsNullOrEmpty(vActivoFijo.observaciones))
                txtObservacion.Text = HttpUtility.HtmlDecode(vActivoFijo.observaciones);
            if (!string.IsNullOrEmpty(vActivoFijo.cod_oficina.ToString()))
                ddlOficina.SelectedValue = HttpUtility.HtmlDecode(vActivoFijo.cod_oficina.ToString().Trim());
            if (!string.IsNullOrEmpty(vActivoFijo.fecha_ult_depre.ToString()))
                txtFecUltDep.Text = HttpUtility.HtmlDecode(vActivoFijo.fecha_ult_depre.ToString(conf.ObtenerFormatoFecha()).Trim());
            if (!string.IsNullOrEmpty(vActivoFijo.acumulado_depreciacion.ToString()))
                txtDepAcum.Text = HttpUtility.HtmlDecode(vActivoFijo.acumulado_depreciacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vActivoFijo.saldo_por_depreciar.ToString()))
                txtSaldoDep.Text = HttpUtility.HtmlDecode(vActivoFijo.saldo_por_depreciar.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(activosServicio.CodigoProgramaVenta, "ObtenerDatos", ex);
        }
    }

    private void CargarListas()
    {
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        try
        {
            ClaseActivoservices claseServicio = new ClaseActivoservices();
            ClaseActivo claseAct = new ClaseActivo();
            ddlClase.DataTextField = "nombre";
            ddlClase.DataValueField = "clase";
            ddlClase.DataSource = claseServicio.ListarClaseActivo(claseAct, pUsuario);
            ddlClase.DataBind();

            TipoActivoservices tipoServicio = new TipoActivoservices();
            TipoActivo tipoAct = new TipoActivo();
            ddlTipo.DataTextField = "nombre";
            ddlTipo.DataValueField = "tipo";
            ddlTipo.DataSource = tipoServicio.ListarTipoActivo(tipoAct, pUsuario);
            ddlTipo.DataBind();

            UbicacionActivoservices ubicacionServicio = new UbicacionActivoservices();
            UbicacionActivo ubicacionAct = new UbicacionActivo();
            ddlUbicacion.DataTextField = "nombre";
            ddlUbicacion.DataValueField = "cod_ubica";
            ddlUbicacion.DataSource = ubicacionServicio.ListarUbicacionActivo(ubicacionAct, pUsuario);
            ddlUbicacion.DataBind();

            Xpinn.Contabilidad.Services.CentroCostoService centroServicio = new Xpinn.Contabilidad.Services.CentroCostoService();
            Xpinn.Contabilidad.Entities.CentroCosto centroCosto = new Xpinn.Contabilidad.Entities.CentroCosto();
            ddlCentroCosto.DataTextField = "nom_centro";
            ddlCentroCosto.DataValueField = "centro_costo";
            ddlCentroCosto.DataSource = centroServicio.ListarCentroCosto(centroCosto, pUsuario);
            ddlCentroCosto.DataBind();

            Xpinn.Caja.Services.OficinaService oficinaServicio = new Xpinn.Caja.Services.OficinaService();
            Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();
            ddlOficina.DataTextField = "nombre";
            ddlOficina.DataValueField = "cod_oficina";
            ddlOficina.DataSource = oficinaServicio.ListarOficina(oficina, pUsuario);
            ddlOficina.DataBind();

            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 persona = new Xpinn.FabricaCreditos.Entities.Persona1();
            ddlEncargado.DataTextField = "nombre";
            ddlEncargado.DataValueField = "cod_persona";
            ddlEncargado.DataSource = personaServicio.ListadoPersonas1(persona, pUsuario);
            ddlEncargado.DataBind();

            ddlProveedor.DataTextField = "nombre";
            ddlProveedor.DataValueField = "cod_persona";
            ddlProveedor.DataSource = personaServicio.ListadoPersonas1(persona, pUsuario);
            ddlProveedor.DataBind();

            ddlComprador.DataTextField = "nombre";
            ddlComprador.DataValueField = "cod_persona";
            ddlComprador.DataSource = personaServicio.ListadoPersonas1(persona, pUsuario);
            ddlComprador.DataBind();

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(activosServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

}