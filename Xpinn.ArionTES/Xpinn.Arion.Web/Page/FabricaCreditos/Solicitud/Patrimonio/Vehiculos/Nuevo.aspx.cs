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
    private Xpinn.FabricaCreditos.Services.VehiculosService VehiculosServicio = new Xpinn.FabricaCreditos.Services.VehiculosService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[VehiculosServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(VehiculosServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(VehiculosServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
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

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Vehiculos vVehiculos = new Xpinn.FabricaCreditos.Entities.Vehiculos();

            if (idObjeto != "")
                vVehiculos = VehiculosServicio.ConsultarVehiculos(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (txtCod_vehiculo.Text != "") vVehiculos.cod_vehiculo = Convert.ToInt64(txtCod_vehiculo.Text.Trim());
            if (txtCod_persona.Text != "") vVehiculos.cod_persona = Convert.ToInt64(txtCod_persona.Text.Trim());
            if (txtMarca.Text != "") vVehiculos.marca = Convert.ToString(txtMarca.Text.Trim());
            //vVehiculos.placa = Convert.ToString(txtPlaca.Text.Trim());
            vVehiculos.placa = (txtPlaca.Text != "") ? Convert.ToString(txtPlaca.Text.Trim()) : String.Empty;
            if (txtModelo.Text != "") vVehiculos.modelo = Convert.ToInt64(txtModelo.Text.Trim());
            if (txtValorcomercial.Text != "") vVehiculos.valorcomercial = Convert.ToInt64(txtValorcomercial.Text.Trim());
            if (txtValorprenda.Text != "") vVehiculos.valorprenda = Convert.ToInt64(txtValorprenda.Text.Trim());

            if (idObjeto != "")
            {
                vVehiculos.cod_vehiculo = Convert.ToInt64(idObjeto);
                VehiculosServicio.ModificarVehiculos(vVehiculos, (Usuario)Session["usuario"]);
            }
            else
            {
                vVehiculos = VehiculosServicio.CrearVehiculos(vVehiculos, (Usuario)Session["usuario"]);
                idObjeto = vVehiculos.cod_vehiculo.ToString();
            }

            Session[VehiculosServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(VehiculosServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Session[VehiculosServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Vehiculos vVehiculos = new Xpinn.FabricaCreditos.Entities.Vehiculos();
            vVehiculos = VehiculosServicio.ConsultarVehiculos(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vVehiculos.cod_vehiculo != Int64.MinValue)
                txtCod_vehiculo.Text = HttpUtility.HtmlDecode(vVehiculos.cod_vehiculo.ToString().Trim());
            if (vVehiculos.cod_persona != Int64.MinValue)
                txtCod_persona.Text = HttpUtility.HtmlDecode(vVehiculos.cod_persona.ToString().Trim());
            if (vVehiculos.marca != null)
                txtMarca.Text = HttpUtility.HtmlDecode(vVehiculos.marca.ToString().Trim());
            if (!string.IsNullOrEmpty(vVehiculos.placa))
                txtPlaca.Text = HttpUtility.HtmlDecode(vVehiculos.placa.ToString().Trim());
            if (vVehiculos.modelo != Int64.MinValue)
                txtModelo.Text = HttpUtility.HtmlDecode(vVehiculos.modelo.ToString().Trim());
            if (vVehiculos.valorcomercial != Int64.MinValue)
                txtValorcomercial.Text = HttpUtility.HtmlDecode(vVehiculos.valorcomercial.ToString().Trim());
            if (vVehiculos.valorprenda != Int64.MinValue)
                txtValorprenda.Text = HttpUtility.HtmlDecode(vVehiculos.valorprenda.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(VehiculosServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
}