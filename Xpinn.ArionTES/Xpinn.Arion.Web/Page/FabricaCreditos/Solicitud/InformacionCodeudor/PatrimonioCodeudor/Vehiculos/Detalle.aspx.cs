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
    private Xpinn.FabricaCreditos.Services.VehiculosService VehiculosServicio = new Xpinn.FabricaCreditos.Services.VehiculosService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(VehiculosServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(VehiculosServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[VehiculosServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[VehiculosServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(VehiculosServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(VehiculosServicio.CodigoPrograma, "Page_Load", ex);
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
            VehiculosServicio.EliminarVehiculos(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(VehiculosServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[VehiculosServicio.CodigoPrograma + ".id"] = idObjeto;
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
            Xpinn.FabricaCreditos.Entities.Vehiculos vVehiculos = new Xpinn.FabricaCreditos.Entities.Vehiculos();
            vVehiculos = VehiculosServicio.ConsultarVehiculos(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vVehiculos.cod_vehiculo != Int64.MinValue)
                txtCod_vehiculo.Text = vVehiculos.cod_vehiculo.ToString().Trim();
            if (vVehiculos.cod_persona != Int64.MinValue)
                txtCod_persona.Text = vVehiculos.cod_persona.ToString().Trim();
            if (vVehiculos.marca != null)
                txtMarca.Text = vVehiculos.marca.ToString().Trim();
            if (!string.IsNullOrEmpty(vVehiculos.placa))
                txtPlaca.Text = vVehiculos.placa.ToString().Trim();
            if (vVehiculos.modelo != Int64.MinValue)
                txtModelo.Text = vVehiculos.modelo.ToString().Trim();
            if (vVehiculos.valorcomercial != Int64.MinValue)
                txtValorcomercial.Text = vVehiculos.valorcomercial.ToString().Trim();
            if (vVehiculos.valorprenda != Int64.MinValue)
                txtValorprenda.Text = vVehiculos.valorprenda.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(VehiculosServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }
}