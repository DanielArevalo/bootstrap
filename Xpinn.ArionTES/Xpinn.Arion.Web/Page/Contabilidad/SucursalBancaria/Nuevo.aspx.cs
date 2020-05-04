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
            if (Session[SucursalBancariaServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(SucursalBancariaServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(SucursalBancariaServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
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

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Contabilidad.Entities.SucursalBancaria vSucursalBancaria = new Xpinn.Contabilidad.Entities.SucursalBancaria();

            if (idObjeto != "")
                vSucursalBancaria = SucursalBancariaServicio.ConsultarSucursalBancaria(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vSucursalBancaria.idsucursal = Convert.ToInt32(txtConsecutivo.Text);
            vSucursalBancaria.cod_suc = Convert.ToInt32(txtCodigo.Text.Trim());
            vSucursalBancaria.cod_banco = Convert.ToInt32(ddlBanco.SelectedValue);
            vSucursalBancaria.nom_suc = Convert.ToString(txtNombre.Text.Trim());
            vSucursalBancaria.codciudad = Convert.ToInt32(ddlCiudad.SelectedValue);
            vSucursalBancaria.direccion = txtDireccion.Text;
            if (txtCodInt.Text.Trim() != "")
                vSucursalBancaria.cod_int = Convert.ToInt32(txtCodInt.Text);

            if (idObjeto != "")
            {
                vSucursalBancaria.idsucursal = Convert.ToInt32(idObjeto);
                SucursalBancariaServicio.ModificarSucursalBancaria(vSucursalBancaria, (Usuario)Session["usuario"]);
            }
            else
            {
                vSucursalBancaria = SucursalBancariaServicio.CrearSucursalBancaria(vSucursalBancaria, (Usuario)Session["usuario"]);
                idObjeto = vSucursalBancaria.idsucursal.ToString();
            }

            Session[SucursalBancariaServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SucursalBancariaServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Session[SucursalBancariaServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
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