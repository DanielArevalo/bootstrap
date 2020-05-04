using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Services;

public partial class Nuevo : GlobalWeb
{
    TipoAsociadoServices _TipoAsociado = new TipoAsociadoServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[_TipoAsociado.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(_TipoAsociado.CodigoPrograma, "E");
            else
                VisualizarOpciones(_TipoAsociado.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_TipoAsociado.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //CargarListas();
                if (Session[_TipoAsociado.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[_TipoAsociado.CodigoPrograma + ".id"].ToString();
                    Session.Remove(_TipoAsociado.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_TipoAsociado.CodigoPrograma, "Page_Load", ex);
        }
    }

    //private void CargarListas()
    //{

    //}

    private bool ValidarDatos()
    {

        if (txtDescripccion.Text == null || txtDescripccion.Text == "")
        {
            VerError("Ingrese una Descripccion para el Perfil de Riesgo");
            return false;
        }
        if (ddlValor.SelectedValue == "0" || ddlValor.SelectedValue == null)
        {
            VerError("Ingrese una valoracion para  el Perfil de Riesgo");
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidarDatos())
            {
                TipoAsociado pTipoAsociado = new TipoAsociado();
                pTipoAsociado.Cod_tipoasociado = Convert.ToInt64(hdCodperfil.Value.Trim());
                pTipoAsociado.Descripcion = txtDescripccion.Text;
                pTipoAsociado.valoracion = Convert.ToString(ddlValor.SelectedValue.Trim());

                if (hdCodperfil.Value == "")
                    pTipoAsociado = _TipoAsociado.CrearTipoAsociado(pTipoAsociado, (Usuario)Session["usuario"]);
                else
                {
                    pTipoAsociado.Cod_tipoasociado = Convert.ToInt64(hdCodperfil.Value.Trim());
                    pTipoAsociado = _TipoAsociado.ModificarTipoAsociado(pTipoAsociado, (Usuario)Session["usuario"]);
                }
                Navegar(Pagina.Lista);
            }
        }
        catch (Exception ex)
        {
            VerError("Error al guardar los datos " + ex.Message);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session.Remove(_TipoAsociado.CodigoPrograma + ".id");
        Navegar(Pagina.Lista);
    }

    private void ObtenerDatos(string idObjeto)
    {
        TipoAsociado pTipoAsociado = new TipoAsociado();
        pTipoAsociado.Cod_tipoasociado = Convert.ToInt64(idObjeto);

        pTipoAsociado = _TipoAsociado.ConsultarTipoAsociado(pTipoAsociado, (Usuario)Session["usuario"]);

        if (pTipoAsociado != null)
        {
            if (!string.IsNullOrWhiteSpace(pTipoAsociado.Cod_tipoasociado.ToString()))
                hdCodperfil.Value = pTipoAsociado.Cod_tipoasociado.ToString();
            if (!string.IsNullOrWhiteSpace(pTipoAsociado.Descripcion.ToString()))
                txtDescripccion.Text = pTipoAsociado.Descripcion.ToString();
            if (!string.IsNullOrWhiteSpace(pTipoAsociado.valoracion.ToString()))
                ddlValor.SelectedValue = pTipoAsociado.valoracion.ToString();
        }
    }

}