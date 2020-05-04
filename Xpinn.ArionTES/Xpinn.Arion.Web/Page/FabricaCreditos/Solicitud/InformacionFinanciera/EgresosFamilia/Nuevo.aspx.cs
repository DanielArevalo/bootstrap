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
    private Xpinn.FabricaCreditos.Services.EgresosFamiliaService EgresosFamiliaServicio = new Xpinn.FabricaCreditos.Services.EgresosFamiliaService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[EgresosFamiliaServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(EgresosFamiliaServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(EgresosFamiliaServicio.CodigoPrograma, "A");

            Site2 toolBar = (Site2)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EgresosFamiliaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtCod_persona.Text = Session["Cod_persona"].ToString(); 
            if (!IsPostBack)
            {
                if (Session[EgresosFamiliaServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[EgresosFamiliaServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(EgresosFamiliaServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(EgresosFamiliaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.EgresosFamilia vEgresosFamilia = new Xpinn.FabricaCreditos.Entities.EgresosFamilia();

            if (idObjeto != "")
                vEgresosFamilia = EgresosFamiliaServicio.ConsultarEgresosFamilia(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (txtCod_egreso.Text != "") vEgresosFamilia.cod_egreso = Convert.ToInt64(txtCod_egreso.Text.Trim());
            if (txtCod_persona.Text != "") vEgresosFamilia.cod_persona = Convert.ToInt64(txtCod_persona.Text.Trim());
            if (txtEgresos.Text != "") vEgresosFamilia.egresos = Convert.ToInt64(txtEgresos.Text.Trim());
            if (txtAlimentacion.Text != "") vEgresosFamilia.alimentacion = Convert.ToInt64(txtAlimentacion.Text.Trim().Replace(@".", ""));
            if (txtVivienda.Text != "") vEgresosFamilia.vivienda = Convert.ToInt64(txtVivienda.Text.Trim().Replace(@".", ""));
            if (txtEducacion.Text != "") vEgresosFamilia.educacion = Convert.ToInt64(txtEducacion.Text.Trim().Replace(@".", ""));
            if (txtServiciospublicos.Text != "") vEgresosFamilia.serviciospublicos = Convert.ToInt64(txtServiciospublicos.Text.Trim().Replace(@".", ""));
            if (txtTransporte.Text != "") vEgresosFamilia.transporte = Convert.ToInt64(txtTransporte.Text.Trim().Replace(@".", ""));
            if (txtPagodeudas.Text != "") vEgresosFamilia.pagodeudas = Convert.ToInt64(txtPagodeudas.Text.Trim().Replace(@".", ""));
            if (txtOtros.Text != "") vEgresosFamilia.otros = Convert.ToInt64(txtOtros.Text.Trim().Replace(@".", ""));

            if (idObjeto != "")
            {
                vEgresosFamilia.cod_egreso = Convert.ToInt64(idObjeto);
                EgresosFamiliaServicio.ModificarEgresosFamilia(vEgresosFamilia, (Usuario)Session["usuario"]);
            }
            else
            {
                vEgresosFamilia = EgresosFamiliaServicio.CrearEgresosFamilia(vEgresosFamilia, (Usuario)Session["usuario"]);
                idObjeto = vEgresosFamilia.cod_egreso.ToString();
            }

            Session[EgresosFamiliaServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EgresosFamiliaServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Session[EgresosFamiliaServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.EgresosFamilia vEgresosFamilia = new Xpinn.FabricaCreditos.Entities.EgresosFamilia();
            vEgresosFamilia = EgresosFamiliaServicio.ConsultarEgresosFamilia(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vEgresosFamilia.cod_egreso != Int64.MinValue)
                txtCod_egreso.Text = HttpUtility.HtmlDecode(vEgresosFamilia.cod_egreso.ToString().Trim());
            if (vEgresosFamilia.cod_persona != Int64.MinValue)
                txtCod_persona.Text = HttpUtility.HtmlDecode(vEgresosFamilia.cod_persona.ToString().Trim());
            if (vEgresosFamilia.egresos != Int64.MinValue)
                txtEgresos.Text = HttpUtility.HtmlDecode(vEgresosFamilia.egresos.ToString().Trim());
            if (vEgresosFamilia.alimentacion != Int64.MinValue)
                txtAlimentacion.Text = HttpUtility.HtmlDecode(vEgresosFamilia.alimentacion.ToString().Trim());
            if (vEgresosFamilia.vivienda != Int64.MinValue)
                txtVivienda.Text = HttpUtility.HtmlDecode(vEgresosFamilia.vivienda.ToString().Trim());
            if (vEgresosFamilia.educacion != Int64.MinValue)
                txtEducacion.Text = HttpUtility.HtmlDecode(vEgresosFamilia.educacion.ToString().Trim());
            if (vEgresosFamilia.serviciospublicos != Int64.MinValue)
                txtServiciospublicos.Text = HttpUtility.HtmlDecode(vEgresosFamilia.serviciospublicos.ToString().Trim());
            if (vEgresosFamilia.transporte != Int64.MinValue)
                txtTransporte.Text = HttpUtility.HtmlDecode(vEgresosFamilia.transporte.ToString().Trim());
            if (vEgresosFamilia.pagodeudas != Int64.MinValue)
                txtPagodeudas.Text = HttpUtility.HtmlDecode(vEgresosFamilia.pagodeudas.ToString().Trim());
            if (vEgresosFamilia.otros != Int64.MinValue)
                txtOtros.Text = HttpUtility.HtmlDecode(vEgresosFamilia.otros.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(EgresosFamiliaServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
}