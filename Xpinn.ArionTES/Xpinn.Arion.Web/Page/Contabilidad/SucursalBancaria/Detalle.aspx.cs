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
    private Xpinn.Contabilidad.Services.SucursalBancariaService SucursalBancariaServicio = new Xpinn.Contabilidad.Services.SucursalBancariaService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(SucursalBancariaServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SucursalBancariaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarListas();
                if (Session[SucursalBancariaServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[SucursalBancariaServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(SucursalBancariaServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtCodigo.Text = Convert.ToString(SucursalBancariaServicio.ObtenerSiguienteCodigo((Usuario)Session["Usuario"]));
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SucursalBancariaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void LlenarListas()
    {
        Usuario usuario = new Usuario();
        usuario = (Usuario)Session["Usuario"];
        Xpinn.Caja.Services.BancosService BancosService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos Bancos = new Xpinn.Caja.Entities.Bancos();
        ddlBanco.DataSource = BancosService.ListarBancos(Bancos, usuario);
        ddlBanco.DataTextField = "nombrebanco";
        ddlBanco.DataValueField = "cod_banco";
        ddlBanco.DataBind();

        Xpinn.Caja.Services.CiudadService CiudadService = new Xpinn.Caja.Services.CiudadService();
        Xpinn.Caja.Entities.Ciudad Ciudad = new Xpinn.Caja.Entities.Ciudad();
        ddlCiudad.DataSource = CiudadService.ListadoCiudad(Ciudad, usuario);
        ddlCiudad.DataTextField = "nom_ciudad";
        ddlCiudad.DataValueField = "cod_ciudad";
        ddlCiudad.DataBind();
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Contabilidad.Entities.SucursalBancaria vSucursalBancaria = new Xpinn.Contabilidad.Entities.SucursalBancaria();
            vSucursalBancaria = SucursalBancariaServicio.ConsultarSucursalBancaria(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            txtConsecutivo.Text = HttpUtility.HtmlDecode(vSucursalBancaria.idsucursal.ToString().Trim());
            txtCodigo.Text = HttpUtility.HtmlDecode(vSucursalBancaria.cod_suc.ToString().Trim());
            if (!string.IsNullOrEmpty(vSucursalBancaria.direccion))
                txtDireccion.Text = HttpUtility.HtmlDecode(vSucursalBancaria.direccion.ToString().Trim());
            if (!string.IsNullOrEmpty(vSucursalBancaria.nom_suc))
                txtNombre.Text = HttpUtility.HtmlDecode(vSucursalBancaria.nom_suc.ToString().Trim());
            if (!string.IsNullOrEmpty(vSucursalBancaria.cod_banco.ToString().Trim()))
                ddlBanco.SelectedValue = HttpUtility.HtmlDecode(vSucursalBancaria.cod_banco.ToString().Trim());
            if (!string.IsNullOrEmpty(vSucursalBancaria.codciudad.ToString().Trim()))
                ddlCiudad.SelectedValue = HttpUtility.HtmlDecode(vSucursalBancaria.codciudad.ToString().Trim());
            if (!string.IsNullOrEmpty(vSucursalBancaria.cod_int.ToString().Trim()))
                txtCodInt.Text = HttpUtility.HtmlDecode(vSucursalBancaria.cod_int.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SucursalBancariaServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


}