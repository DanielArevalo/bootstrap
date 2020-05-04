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
    private Xpinn.Contabilidad.Services.CentroCostoService CentroCostoServicio = new Xpinn.Contabilidad.Services.CentroCostoService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[CentroCostoServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(CentroCostoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(CentroCostoServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CentroCostoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[CentroCostoServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[CentroCostoServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(CentroCostoServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(CentroCostoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Contabilidad.Entities.CentroCosto vCentroCosto = new Xpinn.Contabilidad.Entities.CentroCosto();

            if (idObjeto != "")
                vCentroCosto = CentroCostoServicio.ConsultarCentroCosto(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vCentroCosto.centro_costo = Convert.ToInt64(txtCentroCosto.Text.Trim());
            vCentroCosto.nom_centro = Convert.ToString(txtNombre.Text.Trim());
            vCentroCosto.descripcion = Convert.ToString(txtDescripcion.Text.Trim());
            if (chbPrincipal.Checked == true)
                vCentroCosto.principal = 1;
            else
                vCentroCosto.principal = 0;

            if (idObjeto != "")
            {
                vCentroCosto.centro_costo = Convert.ToInt64(idObjeto);
                CentroCostoServicio.ModificarCentroCosto(vCentroCosto, (Usuario)Session["usuario"]);
            }
            else
            {
                vCentroCosto = CentroCostoServicio.CrearCentroCosto(vCentroCosto, (Usuario)Session["usuario"]);
                idObjeto = vCentroCosto.centro_costo.ToString();
            }

            Session[CentroCostoServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CentroCostoServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Xpinn.Contabilidad.Entities.CentroCosto vCentroCosto = new Xpinn.Contabilidad.Entities.CentroCosto();
            vCentroCosto = CentroCostoServicio.ConsultarCentroCosto(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            txtCentroCosto.Text = HttpUtility.HtmlDecode(vCentroCosto.centro_costo.ToString().Trim());
            if (!string.IsNullOrEmpty(vCentroCosto.nom_centro))
                txtNombre.Text = HttpUtility.HtmlDecode(vCentroCosto.nom_centro.ToString().Trim());
            if (!string.IsNullOrEmpty(vCentroCosto.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vCentroCosto.descripcion.ToString().Trim());
            if (!string.IsNullOrEmpty(vCentroCosto.principal.ToString()))
                if (vCentroCosto.principal == 1)
                    chbPrincipal.Checked = true;
                else
                    chbPrincipal.Checked = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CentroCostoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


}