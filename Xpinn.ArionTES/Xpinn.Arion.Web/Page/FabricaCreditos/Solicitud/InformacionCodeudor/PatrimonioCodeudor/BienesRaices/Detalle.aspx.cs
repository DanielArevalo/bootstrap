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
    private Xpinn.FabricaCreditos.Services.BienesRaicesService BienesRaicesServicio = new Xpinn.FabricaCreditos.Services.BienesRaicesService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(BienesRaicesServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BienesRaicesServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[BienesRaicesServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[BienesRaicesServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(BienesRaicesServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(BienesRaicesServicio.CodigoPrograma, "Page_Load", ex);
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
            BienesRaicesServicio.EliminarBienesRaices(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BienesRaicesServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[BienesRaicesServicio.CodigoPrograma + ".id"] = idObjeto;
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
            Xpinn.FabricaCreditos.Entities.BienesRaices vBienesRaices = new Xpinn.FabricaCreditos.Entities.BienesRaices();
            vBienesRaices = BienesRaicesServicio.ConsultarBienesRaices(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vBienesRaices.cod_bien != Int64.MinValue)
                txtCod_bien.Text = vBienesRaices.cod_bien.ToString().Trim();
            if (vBienesRaices.cod_persona != Int64.MinValue)
                txtCod_persona.Text = vBienesRaices.cod_persona.ToString().Trim();
            if (vBienesRaices.tipo != null)
                txtTipo.Text = vBienesRaices.tipo.ToString().Trim();
            if (!string.IsNullOrEmpty(vBienesRaices.matricula))
                txtMatricula.Text = vBienesRaices.matricula.ToString().Trim();
            if (vBienesRaices.valorcomercial != Int64.MinValue)
                txtValorcomercial.Text = vBienesRaices.valorcomercial.ToString().Trim();
            if (vBienesRaices.valorhipoteca != Int64.MinValue)
                txtValorhipoteca.Text = vBienesRaices.valorhipoteca.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BienesRaicesServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }
}