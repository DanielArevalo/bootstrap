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
    private Xpinn.Tesoreria.Services.AreasCajService AreasCajServicio = new Xpinn.Tesoreria.Services.AreasCajService();
    public StringHelper _stringHelper = new StringHelper();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[AreasCajServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(AreasCajServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(AreasCajServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AreasCajServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[AreasCajServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[AreasCajServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(AreasCajServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(AreasCajServicio.CodigoPrograma, "Page_Load", ex);
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
            Xpinn.Tesoreria.Entities.AreasCaj areasCaja = new Xpinn.Tesoreria.Entities.AreasCaj();
            areasCaja = AreasCajServicio.ConsultarAreasCaj(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(areasCaja.idarea.ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(areasCaja.idarea.ToString().Trim());
            if (!string.IsNullOrEmpty(areasCaja.fecha_constitucion.ToString()))
                txtfechaConst.Text = HttpUtility.HtmlDecode(areasCaja.fecha_constitucion.ToString().Trim());
            if (!string.IsNullOrEmpty(areasCaja.base_valor.ToString()))
                txtValor.Text = HttpUtility.HtmlDecode(areasCaja.base_valor.ToString().Trim());
            if (!string.IsNullOrEmpty(areasCaja.cod_usuario.ToString()))
                txtUsuario.Text = HttpUtility.HtmlDecode(areasCaja.cod_usuario.ToString().Trim());
            if (!string.IsNullOrEmpty(areasCaja.nombre.ToString()))
                txtDescripcion.Text = HttpUtility.HtmlDecode(areasCaja.nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(areasCaja.nombre.ToString()))
                txtVMinimo.Text = HttpUtility.HtmlDecode(areasCaja.valor_minimo.ToString().Trim());
        }
        catch (Exception ex)        
        {
            BOexcepcion.Throw(AreasCajServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }



    protected void txtValor_TextChanged(object sender, EventArgs e)
    {
        txtValor.Text = _stringHelper.FormatearNumerosComoCurrency(txtValor.Text);
    }

    protected void txtVMinimo_TextChanged(object sender, EventArgs e)
    {
        txtVMinimo.Text = _stringHelper.FormatearNumerosComoCurrency(txtVMinimo.Text);
    }
}