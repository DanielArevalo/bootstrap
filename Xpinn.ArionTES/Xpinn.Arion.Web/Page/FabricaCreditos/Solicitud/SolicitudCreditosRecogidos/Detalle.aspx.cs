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
    private Xpinn.FabricaCreditos.Services.SolicitudCreditosRecogidosService SolicitudCreditosRecogidosServicio = new Xpinn.FabricaCreditos.Services.SolicitudCreditosRecogidosService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(SolicitudCreditosRecogidosServicio.CodigoPrograma, "D");

            Site1 toolBar = (Site1)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicitudCreditosRecogidosServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[SolicitudCreditosRecogidosServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[SolicitudCreditosRecogidosServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(SolicitudCreditosRecogidosServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(SolicitudCreditosRecogidosServicio.CodigoPrograma, "Page_Load", ex);
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
            SolicitudCreditosRecogidosServicio.EliminarSolicitudCreditosRecogidos(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicitudCreditosRecogidosServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[SolicitudCreditosRecogidosServicio.CodigoPrograma + ".id"] = idObjeto;
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
            Xpinn.FabricaCreditos.Entities.SolicitudCreditosRecogidos vSolicitudCreditosRecogidos = new Xpinn.FabricaCreditos.Entities.SolicitudCreditosRecogidos();
            vSolicitudCreditosRecogidos = SolicitudCreditosRecogidosServicio.ConsultarSolicitudCreditosRecogidos(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vSolicitudCreditosRecogidos.idsolicitudrecoge != Int64.MinValue)
                txtIdsolicitudrecoge.Text = vSolicitudCreditosRecogidos.idsolicitudrecoge.ToString().Trim();
            if (vSolicitudCreditosRecogidos.numerosolicitud != Int64.MinValue)
                txtNumerosolicitud.Text = vSolicitudCreditosRecogidos.numerosolicitud.ToString().Trim();
            if (vSolicitudCreditosRecogidos.numero_recoge != Int64.MinValue)
                txtNumero_recoge.Text = vSolicitudCreditosRecogidos.numero_recoge.ToString().Trim();
            if (vSolicitudCreditosRecogidos.fecharecoge != DateTime.MinValue)
                txtFecharecoge.Text = vSolicitudCreditosRecogidos.fecharecoge.ToShortDateString();
            if (vSolicitudCreditosRecogidos.valorrecoge != Int64.MinValue)
                txtValorrecoge.Text = vSolicitudCreditosRecogidos.valorrecoge.ToString().Trim();
            if (vSolicitudCreditosRecogidos.fechapago != DateTime.MinValue)
                txtFechapago.Text = vSolicitudCreditosRecogidos.fechapago.ToShortDateString();
            if (vSolicitudCreditosRecogidos.saldocapital != Int64.MinValue)
                txtSaldocapital.Text = vSolicitudCreditosRecogidos.saldocapital.ToString().Trim();
            if (vSolicitudCreditosRecogidos.saldointcorr != Int64.MinValue)
                txtSaldointcorr.Text = vSolicitudCreditosRecogidos.saldointcorr.ToString().Trim();
            if (vSolicitudCreditosRecogidos.saldointmora != Int64.MinValue)
                txtSaldointmora.Text = vSolicitudCreditosRecogidos.saldointmora.ToString().Trim();
            if (vSolicitudCreditosRecogidos.saldomipyme != Int64.MinValue)
                txtSaldomipyme.Text = vSolicitudCreditosRecogidos.saldomipyme.ToString().Trim();
            if (vSolicitudCreditosRecogidos.saldoivamipyme != Int64.MinValue)
                txtSaldoivamipyme.Text = vSolicitudCreditosRecogidos.saldoivamipyme.ToString().Trim();
            if (vSolicitudCreditosRecogidos.saldootros != Int64.MinValue)
                txtSaldootros.Text = vSolicitudCreditosRecogidos.saldootros.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicitudCreditosRecogidosServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }
}