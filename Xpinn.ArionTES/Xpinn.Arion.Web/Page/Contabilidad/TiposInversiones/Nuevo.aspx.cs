using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;
using Xpinn.Util;

public partial class Nuevo : GlobalWeb
{
    InversionesService inversionesService = new InversionesService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[inversionesService.CodigoProgramaTipoInv + ".id"] != null)
                VisualizarOpciones(inversionesService.CodigoProgramaTipoInv, "E");
            else
                VisualizarOpciones(inversionesService.CodigoProgramaTipoInv, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(inversionesService.CodigoProgramaTipoInv, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtCodigo.Enabled = false;
            if (Session[inversionesService.CodigoProgramaTipoInv + ".id"] != null)
            {
                idObjeto = Session[inversionesService.CodigoProgramaTipoInv + ".id"].ToString();
                Session.Remove(inversionesService.CodigoProgramaTipoInv + ".id");
                ObtenerDatos(idObjeto);
            }
        }
    }

    private void ObtenerDatos(string idObjeto)
    {
        try
        {
            TipoInversiones TipoInversion = new TipoInversiones();
            string pFiltro = " WHERE COD_TIPO = " + idObjeto;
            TipoInversion = inversionesService.ConsultarTipoInversiones(pFiltro, Usuario);
            if (TipoInversion != null)
            {
                if (TipoInversion.cod_tipo != 0)
                    txtCodigo.Text = TipoInversion.cod_tipo.ToString();
                if (!string.IsNullOrEmpty(TipoInversion.descripcion))
                    txtDescripcion.Text = TipoInversion.descripcion;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(inversionesService.CodigoProgramaTipoInv, "ObtenerDatos", ex);
        }
    }

    private void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    private bool validarDatos()
    {
        if (!string.IsNullOrEmpty(idObjeto))
        {
            if (txtCodigo.Text.Trim() == "")
            {
                VerError("Ingrese la descripcion del tipo de inversión");
                return false;
            }
        }
        if (txtDescripcion.Text.Trim() == "")
        {
            VerError("Ingrese la descripcion del tipo de inversión");
            return false;
        }
        return true;
    }

    private void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Page.Validate();
        if (Page.IsValid)
        {
            if (validarDatos())
                ctlMensaje.MostrarMensaje("Desea realizar la grabación del Tipo de Inversión?");
        }
    }

    private void btnContinuarMen_Click(object sender, EventArgs e)
    {
        VerError("");
        try
        {
            TipoInversiones pEntidad = new TipoInversiones();
            pEntidad.descripcion = txtDescripcion.Text.Trim();
            string msj = string.Empty;
            if (idObjeto != "")
            {
                pEntidad.cod_tipo = Convert.ToInt32(txtCodigo.Text.Trim());
                pEntidad = inversionesService.CrearTipoInversion(pEntidad, 2, Usuario);
                msj = "modificaron";
            }
            else
            {
                pEntidad = inversionesService.CrearTipoInversion(pEntidad, 1, Usuario);
                msj = "grabaron";
            }

            lblMsj.Text = msj;
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            mvPrincipal.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

}