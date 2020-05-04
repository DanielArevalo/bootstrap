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
    PerfilRiesgoServices _PerfilRiesgo = new PerfilRiesgoServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[_PerfilRiesgo.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(_PerfilRiesgo.CodigoPrograma, "E");
            else
                VisualizarOpciones(_PerfilRiesgo.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_PerfilRiesgo.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //CargarListas();
                if (Session[_PerfilRiesgo.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[_PerfilRiesgo.CodigoPrograma + ".id"].ToString();
                    Session.Remove(_PerfilRiesgo.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_PerfilRiesgo.CodigoPrograma, "Page_Load", ex);
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
                PerfilRiesgo pPerfilRiesgo = new PerfilRiesgo();
                pPerfilRiesgo.Cod_perfil = Convert.ToInt64(hdCodperfil.Value.Trim());
                pPerfilRiesgo.Descripcion = txtDescripccion.Text;
                pPerfilRiesgo.valoracion = Convert.ToString(ddlValor.SelectedValue.Trim());
                pPerfilRiesgo.tipoPersona = ddlTipoPersona.SelectedValue;

                if (hdCodperfil.Value == "")
                    pPerfilRiesgo = _PerfilRiesgo.CrearPerfilRiesgo(pPerfilRiesgo, (Usuario)Session["usuario"]);
                else
                {
                    pPerfilRiesgo.Cod_perfil = Convert.ToInt64(hdCodperfil.Value.Trim());
                    pPerfilRiesgo = _PerfilRiesgo.ModificarPerfilRiesgo(pPerfilRiesgo, (Usuario)Session["usuario"]);
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
        Session.Remove(_PerfilRiesgo.CodigoPrograma + ".id");
        Navegar(Pagina.Lista);
    }

    private void ObtenerDatos(string idObjeto)
    {
        PerfilRiesgo pPerfilRiesgo = new PerfilRiesgo();
        pPerfilRiesgo.Cod_perfil = Convert.ToInt64(idObjeto);

        pPerfilRiesgo = _PerfilRiesgo.ConsultarFactorRiesgo(pPerfilRiesgo, (Usuario)Session["usuario"]);

        if (pPerfilRiesgo != null)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(pPerfilRiesgo.Cod_perfil.ToString()))
                    hdCodperfil.Value = pPerfilRiesgo.Cod_perfil.ToString();
                if (!string.IsNullOrWhiteSpace(pPerfilRiesgo.Descripcion.ToString()))
                    txtDescripccion.Text = pPerfilRiesgo.Descripcion.ToString();
                if (!string.IsNullOrWhiteSpace(pPerfilRiesgo.valoracion.ToString()))
                    ddlValor.SelectedValue = pPerfilRiesgo.valoracion.ToString();
                if (pPerfilRiesgo.tipoPersona != null)
                    if (!string.IsNullOrWhiteSpace(pPerfilRiesgo.tipoPersona.ToString()))
                        ddlTipoPersona.SelectedValue = pPerfilRiesgo.tipoPersona.ToString();
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
        }
    }

    //protected void ddlGrupoact_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    PoblarListas poblar = new PoblarListas();
    //    string Cod_activ =Convert.ToString(ddlGrupoact.SelectedValue);
    //    poblar.PoblarListaDesplegable("ACTIVIDAD", "CODACTIVIDAD, DESCRIPCION", "CODACTIVIDAD like '" + Cod_activ + "%'", "", ddlActividad, (Usuario)Session["usuario"]);
        
    //}
}