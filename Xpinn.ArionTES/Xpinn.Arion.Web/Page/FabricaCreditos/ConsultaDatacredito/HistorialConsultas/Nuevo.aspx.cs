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

partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.consultasdatacreditoService consultasdatacreditoServicio = new Xpinn.FabricaCreditos.Services.consultasdatacreditoService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[consultasdatacreditoServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(consultasdatacreditoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(consultasdatacreditoServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(consultasdatacreditoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[consultasdatacreditoServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[consultasdatacreditoServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(consultasdatacreditoServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(consultasdatacreditoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.consultasdatacredito vconsultasdatacredito = new Xpinn.FabricaCreditos.Entities.consultasdatacredito();

            if (idObjeto != "")
                vconsultasdatacredito = consultasdatacreditoServicio.Consultarconsultasdatacredito(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vconsultasdatacredito.numerofactura = Convert.ToInt64(txtNumerofactura.Text.Trim());
            vconsultasdatacredito.fechaconsulta = Convert.ToDateTime(txtFechaconsulta.Text.Trim());
            vconsultasdatacredito.cedulacliente = Convert.ToString(txtCedulacliente.Text.Trim());
            vconsultasdatacredito.usuario = Convert.ToString(txtUsuario.Text.Trim());
            vconsultasdatacredito.ip = Convert.ToString(txtIp.Text.Trim());
            vconsultasdatacredito.oficina = Convert.ToString(txtOficina.Text.Trim());
            vconsultasdatacredito.valorconsulta = Convert.ToInt64(txtValorconsulta.Text.Trim());

            if (idObjeto != "")
            {
                vconsultasdatacredito.numerofactura = Convert.ToInt64(idObjeto);
                consultasdatacreditoServicio.Modificarconsultasdatacredito(vconsultasdatacredito, (Usuario)Session["usuario"]);
            }
            else
            {
                vconsultasdatacredito = consultasdatacreditoServicio.Crearconsultasdatacredito(vconsultasdatacredito, (Usuario)Session["usuario"]);
                idObjeto = vconsultasdatacredito.numerofactura.ToString();
            }

            Session[consultasdatacreditoServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(consultasdatacreditoServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Session[consultasdatacreditoServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.consultasdatacredito vconsultasdatacredito = new Xpinn.FabricaCreditos.Entities.consultasdatacredito();
            vconsultasdatacredito = consultasdatacreditoServicio.Consultarconsultasdatacredito(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vconsultasdatacredito.numerofactura != Int64.MinValue)
                txtNumerofactura.Text = HttpUtility.HtmlDecode(vconsultasdatacredito.numerofactura.ToString().Trim());
            if (vconsultasdatacredito.fechaconsulta != DateTime.MinValue)
                txtFechaconsulta.Text = HttpUtility.HtmlDecode(vconsultasdatacredito.fechaconsulta.ToShortDateString());
            if (!string.IsNullOrEmpty(vconsultasdatacredito.cedulacliente))
                txtCedulacliente.Text = HttpUtility.HtmlDecode(vconsultasdatacredito.cedulacliente.ToString().Trim());
            if (!string.IsNullOrEmpty(vconsultasdatacredito.usuario))
                txtUsuario.Text = HttpUtility.HtmlDecode(vconsultasdatacredito.usuario.ToString().Trim());
            if (!string.IsNullOrEmpty(vconsultasdatacredito.ip))
                txtIp.Text = HttpUtility.HtmlDecode(vconsultasdatacredito.ip.ToString().Trim());
            if (!string.IsNullOrEmpty(vconsultasdatacredito.oficina))
                txtOficina.Text = HttpUtility.HtmlDecode(vconsultasdatacredito.oficina.ToString().Trim());
            if (vconsultasdatacredito.valorconsulta != Int64.MinValue)
                txtValorconsulta.Text = HttpUtility.HtmlDecode(vconsultasdatacredito.valorconsulta.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(consultasdatacreditoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
}