using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Collections.Generic;

partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.BienesRaicesService BienesRaicesServicio = new Xpinn.FabricaCreditos.Services.BienesRaicesService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[BienesRaicesServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(BienesRaicesServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(BienesRaicesServicio.CodigoPrograma, "A");

            Site2 toolBar = (Site2)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
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

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.BienesRaices vBienesRaices = new Xpinn.FabricaCreditos.Entities.BienesRaices();

            if (idObjeto != "")
                vBienesRaices = BienesRaicesServicio.ConsultarBienesRaices(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (txtCod_bien.Text != "") vBienesRaices.cod_bien = Convert.ToInt64(txtCod_bien.Text.Trim());
            if (txtCod_persona.Text != "") vBienesRaices.cod_persona = Convert.ToInt64(txtCod_persona.Text.Trim());
            if (txtTipo.Text != "") vBienesRaices.tipo = Convert.ToString(txtTipo.Text.Trim());
            //if (txtMatricula.Text != "") vBienesRaices.matricula = Convert.ToString(txtMatricula.Text.Trim());
            vBienesRaices.matricula = (txtMatricula.Text != "") ? Convert.ToString(txtMatricula.Text.Trim()) : String.Empty;
            if (txtValorcomercial.Text != "") vBienesRaices.valorcomercial = Convert.ToInt64(txtValorcomercial.Text.Trim());
            if (txtValorhipoteca.Text != "") vBienesRaices.valorhipoteca = Convert.ToInt64(txtValorhipoteca.Text.Trim());

            if (idObjeto != "")
            {
                vBienesRaices.cod_bien = Convert.ToInt64(idObjeto);
                BienesRaicesServicio.ModificarBienesRaices(vBienesRaices, (Usuario)Session["usuario"]);
            }
            else
            {
                vBienesRaices = BienesRaicesServicio.CrearBienesRaices(vBienesRaices, (Usuario)Session["usuario"]);
                idObjeto = vBienesRaices.cod_bien.ToString();
            }

            Session[BienesRaicesServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BienesRaicesServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Session[BienesRaicesServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.BienesRaices vBienesRaices = new Xpinn.FabricaCreditos.Entities.BienesRaices();
            vBienesRaices = BienesRaicesServicio.ConsultarBienesRaices(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vBienesRaices.cod_bien != Int64.MinValue)
                txtCod_bien.Text = HttpUtility.HtmlDecode(vBienesRaices.cod_bien.ToString().Trim());
            if (vBienesRaices.cod_persona != Int64.MinValue)
                txtCod_persona.Text = HttpUtility.HtmlDecode(vBienesRaices.cod_persona.ToString().Trim());
            if (vBienesRaices.tipo != null)
                txtTipo.Text = HttpUtility.HtmlDecode(vBienesRaices.tipo.ToString().Trim());
            if (!string.IsNullOrEmpty(vBienesRaices.matricula))
                txtMatricula.Text = HttpUtility.HtmlDecode(vBienesRaices.matricula.ToString().Trim());
            if (vBienesRaices.valorcomercial != Int64.MinValue)
                txtValorcomercial.Text = HttpUtility.HtmlDecode(vBienesRaices.valorcomercial.ToString().Trim());
            if (vBienesRaices.valorhipoteca != Int64.MinValue)
                txtValorhipoteca.Text = HttpUtility.HtmlDecode(vBienesRaices.valorhipoteca.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BienesRaicesServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
}