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
    private Xpinn.FabricaCreditos.Services.consultasdatacreditoService consultasdatacreditoServicio = new Xpinn.FabricaCreditos.Services.consultasdatacreditoService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(consultasdatacreditoServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
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
                AsignarEventoConfirmar();
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
            consultasdatacreditoServicio.Eliminarconsultasdatacredito(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(consultasdatacreditoServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[consultasdatacreditoServicio.CodigoPrograma + ".id"] = idObjeto;
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
            Xpinn.FabricaCreditos.Entities.consultasdatacredito vconsultasdatacredito = new Xpinn.FabricaCreditos.Entities.consultasdatacredito();
            vconsultasdatacredito = consultasdatacreditoServicio.Consultarconsultasdatacredito(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vconsultasdatacredito.numerofactura != Int64.MinValue)
                txtNumerofactura.Text = vconsultasdatacredito.numerofactura.ToString().Trim();
            if (vconsultasdatacredito.fechaconsulta != DateTime.MinValue)
                txtFechaconsulta.Text = vconsultasdatacredito.fechaconsulta.ToShortDateString();
            if (!string.IsNullOrEmpty(vconsultasdatacredito.cedulacliente))
                txtCedulacliente.Text = vconsultasdatacredito.cedulacliente.ToString().Trim();
            if (!string.IsNullOrEmpty(vconsultasdatacredito.usuario))
                txtUsuario.Text = vconsultasdatacredito.usuario.ToString().Trim();
            if (!string.IsNullOrEmpty(vconsultasdatacredito.ip))
                txtIp.Text = vconsultasdatacredito.ip.ToString().Trim();
            if (!string.IsNullOrEmpty(vconsultasdatacredito.oficina))
                txtOficina.Text = vconsultasdatacredito.oficina.ToString().Trim();
            if (vconsultasdatacredito.valorconsulta != Int64.MinValue)
                txtValorconsulta.Text = vconsultasdatacredito.valorconsulta.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(consultasdatacreditoServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }
}