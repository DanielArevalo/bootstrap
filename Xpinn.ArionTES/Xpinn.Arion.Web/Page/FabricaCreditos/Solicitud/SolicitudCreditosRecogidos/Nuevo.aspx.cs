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
    private Xpinn.FabricaCreditos.Services.SolicitudCreditosRecogidosService SolicitudCreditosRecogidosServicio = new Xpinn.FabricaCreditos.Services.SolicitudCreditosRecogidosService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[SolicitudCreditosRecogidosServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(SolicitudCreditosRecogidosServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(SolicitudCreditosRecogidosServicio.CodigoPrograma, "A");

            Site1 toolBar = (Site1)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
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

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.SolicitudCreditosRecogidos vSolicitudCreditosRecogidos = new Xpinn.FabricaCreditos.Entities.SolicitudCreditosRecogidos();

            if (idObjeto != "")
                vSolicitudCreditosRecogidos = SolicitudCreditosRecogidosServicio.ConsultarSolicitudCreditosRecogidos(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vSolicitudCreditosRecogidos.idsolicitudrecoge = Convert.ToInt64(txtIdsolicitudrecoge.Text.Trim());
            vSolicitudCreditosRecogidos.numerosolicitud = Convert.ToInt64(txtNumerosolicitud.Text.Trim());
            vSolicitudCreditosRecogidos.numero_recoge = Convert.ToInt64(txtNumero_recoge.Text.Trim());
            vSolicitudCreditosRecogidos.fecharecoge = Convert.ToDateTime(txtFecharecoge.Text.Trim());
            vSolicitudCreditosRecogidos.valorrecoge = Convert.ToInt64(txtValorrecoge.Text.Trim());
            vSolicitudCreditosRecogidos.fechapago = Convert.ToDateTime(txtFechapago.Text.Trim());
            vSolicitudCreditosRecogidos.saldocapital = Convert.ToInt64(txtSaldocapital.Text.Trim());
            vSolicitudCreditosRecogidos.saldointcorr = Convert.ToInt64(txtSaldointcorr.Text.Trim());
            vSolicitudCreditosRecogidos.saldointmora = Convert.ToInt64(txtSaldointmora.Text.Trim());
            vSolicitudCreditosRecogidos.saldomipyme = Convert.ToInt64(txtSaldomipyme.Text.Trim());
            vSolicitudCreditosRecogidos.saldoivamipyme = Convert.ToInt64(txtSaldoivamipyme.Text.Trim());
            vSolicitudCreditosRecogidos.saldootros = Convert.ToInt64(txtSaldootros.Text.Trim());

            if (idObjeto != "")
            {
                vSolicitudCreditosRecogidos.idsolicitudrecoge = Convert.ToInt64(idObjeto);
                SolicitudCreditosRecogidosServicio.ModificarSolicitudCreditosRecogidos(vSolicitudCreditosRecogidos, (Usuario)Session["usuario"]);
            }
            else
            {
                vSolicitudCreditosRecogidos = SolicitudCreditosRecogidosServicio.CrearSolicitudCreditosRecogidos(vSolicitudCreditosRecogidos, (Usuario)Session["usuario"]);
                idObjeto = vSolicitudCreditosRecogidos.idsolicitudrecoge.ToString();
            }

            Session[SolicitudCreditosRecogidosServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicitudCreditosRecogidosServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Session[SolicitudCreditosRecogidosServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.SolicitudCreditosRecogidos vSolicitudCreditosRecogidos = new Xpinn.FabricaCreditos.Entities.SolicitudCreditosRecogidos();
            vSolicitudCreditosRecogidos = SolicitudCreditosRecogidosServicio.ConsultarSolicitudCreditosRecogidos(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vSolicitudCreditosRecogidos.idsolicitudrecoge != Int64.MinValue)
                txtIdsolicitudrecoge.Text = HttpUtility.HtmlDecode(vSolicitudCreditosRecogidos.idsolicitudrecoge.ToString().Trim());
            if (vSolicitudCreditosRecogidos.numerosolicitud != Int64.MinValue)
                txtNumerosolicitud.Text = HttpUtility.HtmlDecode(vSolicitudCreditosRecogidos.numerosolicitud.ToString().Trim());
            if (vSolicitudCreditosRecogidos.numero_recoge != Int64.MinValue)
                txtNumero_recoge.Text = HttpUtility.HtmlDecode(vSolicitudCreditosRecogidos.numero_recoge.ToString().Trim());
            if (vSolicitudCreditosRecogidos.fecharecoge != DateTime.MinValue)
                txtFecharecoge.Text = HttpUtility.HtmlDecode(vSolicitudCreditosRecogidos.fecharecoge.ToShortDateString());
            if (vSolicitudCreditosRecogidos.valorrecoge != Int64.MinValue)
                txtValorrecoge.Text = HttpUtility.HtmlDecode(vSolicitudCreditosRecogidos.valorrecoge.ToString().Trim());
            if (vSolicitudCreditosRecogidos.fechapago != DateTime.MinValue)
                txtFechapago.Text = HttpUtility.HtmlDecode(vSolicitudCreditosRecogidos.fechapago.ToShortDateString());
            if (vSolicitudCreditosRecogidos.saldocapital != Int64.MinValue)
                txtSaldocapital.Text = HttpUtility.HtmlDecode(vSolicitudCreditosRecogidos.saldocapital.ToString().Trim());
            if (vSolicitudCreditosRecogidos.saldointcorr != Int64.MinValue)
                txtSaldointcorr.Text = HttpUtility.HtmlDecode(vSolicitudCreditosRecogidos.saldointcorr.ToString().Trim());
            if (vSolicitudCreditosRecogidos.saldointmora != Int64.MinValue)
                txtSaldointmora.Text = HttpUtility.HtmlDecode(vSolicitudCreditosRecogidos.saldointmora.ToString().Trim());
            if (vSolicitudCreditosRecogidos.saldomipyme != Int64.MinValue)
                txtSaldomipyme.Text = HttpUtility.HtmlDecode(vSolicitudCreditosRecogidos.saldomipyme.ToString().Trim());
            if (vSolicitudCreditosRecogidos.saldoivamipyme != Int64.MinValue)
                txtSaldoivamipyme.Text = HttpUtility.HtmlDecode(vSolicitudCreditosRecogidos.saldoivamipyme.ToString().Trim());
            if (vSolicitudCreditosRecogidos.saldootros != Int64.MinValue)
                txtSaldootros.Text = HttpUtility.HtmlDecode(vSolicitudCreditosRecogidos.saldootros.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicitudCreditosRecogidosServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
}