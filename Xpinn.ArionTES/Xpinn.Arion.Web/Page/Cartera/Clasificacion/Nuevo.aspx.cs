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
    private Xpinn.Cartera.Services.ClasificacionService clasificacionServicio = new Xpinn.Cartera.Services.ClasificacionService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[clasificacionServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(clasificacionServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(clasificacionServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clasificacionServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                PoblarListas poblar = new PoblarListas();
                poblar.PoblarListaDesplegable("TIPOTASAHIST", ddlTipoHistorico, (Usuario)Session["Usuario"]);
                if (Session[clasificacionServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[clasificacionServicio.CodigoPrograma + ".id"].ToString();
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
            BOexcepcion.Throw(clasificacionServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Cartera.Entities.Clasificacion vClasif = new Xpinn.Cartera.Entities.Clasificacion();

            if (idObjeto != "")
                vClasif = clasificacionServicio.ConsultarClasificacion(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vClasif.cod_clasifica = Convert.ToInt32(txtCodigo.Text.Trim());
            vClasif.descripcion = Convert.ToString(txtDescripcion.Text.Trim());
            if (ddlTipoHistorico.SelectedValue != "")
                vClasif.tipo_historico = Convert.ToInt32(ConvertirStringToInt(ddlTipoHistorico.SelectedValue));
            vClasif.estado = Convert.ToInt32(ConvertirStringToInt(txtEstado.Text));
            if (cbLocal.Checked == true)
                vClasif.aportes_garantia = 1;
            else
                vClasif.aportes_garantia = 0;

            if (cbAporteClasif.Checked)
                vClasif.aportes_gar_clasificacion = 1;
            else
                vClasif.aportes_gar_clasificacion = 0;

            if (idObjeto != "")
            {
                vClasif.cod_clasifica = Convert.ToInt32(idObjeto);
                clasificacionServicio.ModificarClasificacion(vClasif, (Usuario)Session["usuario"]);
            }
            else
            {
                vClasif = clasificacionServicio.CrearClasificacion(vClasif, (Usuario)Session["usuario"]);
                idObjeto = vClasif.cod_clasifica.ToString();
            }

            Session[clasificacionServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clasificacionServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Cartera.Entities.Clasificacion vClasif = new Xpinn.Cartera.Entities.Clasificacion();
            vClasif = clasificacionServicio.ConsultarClasificacion(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            cbLocal.Checked = false;

            if (!string.IsNullOrEmpty(vClasif.cod_clasifica.ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(vClasif.cod_clasifica.ToString().Trim());
            if (!string.IsNullOrEmpty(vClasif.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vClasif.descripcion.ToString().Trim());
            if (!string.IsNullOrEmpty(vClasif.tipo_historico.ToString()))
                ddlTipoHistorico.SelectedValue = HttpUtility.HtmlDecode(vClasif.tipo_historico.ToString().Trim());
            if (!string.IsNullOrEmpty(vClasif.estado.ToString()))
                txtEstado.Text = HttpUtility.HtmlDecode(vClasif.estado.ToString().Trim());
            cbLocal.Checked = false;    
            if (!string.IsNullOrEmpty(vClasif.aportes_garantia.ToString().Trim()))
                if (vClasif.aportes_garantia == 1)
                    cbLocal.Checked = true;
            if (!string.IsNullOrEmpty(vClasif.aportes_gar_clasificacion.ToString().Trim()))
                if (vClasif.aportes_gar_clasificacion == 1)
                    cbAporteClasif.Checked = true;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clasificacionServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    
 
}