﻿using System;
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
    private Xpinn.Caja.Services.TipoPagoService TipoPagoServicio = new Xpinn.Caja.Services.TipoPagoService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[TipoPagoServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(TipoPagoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(TipoPagoServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoPagoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[TipoPagoServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[TipoPagoServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(TipoPagoServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(TipoPagoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Caja.Entities.TipoPago vTipoPago = new Xpinn.Caja.Entities.TipoPago();

            if (idObjeto != "")
                vTipoPago = TipoPagoServicio.ConsultarTipoPago(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vTipoPago.cod_tipo_pago = Convert.ToInt64(txtTipoPago.Text.Trim());
            vTipoPago.descripcion = Convert.ToString(txtDescripcion.Text.Trim());
            vTipoPago.caja = Convert.ToString(ddlCaja.SelectedValue);

            if (idObjeto != "")
            {
                vTipoPago.cod_tipo_pago = Convert.ToInt64(idObjeto);
                TipoPagoServicio.ModificarTipoPago(vTipoPago, (Usuario)Session["usuario"]);
            }
            else
            {
                vTipoPago = TipoPagoServicio.CrearTipoPago(vTipoPago, (Usuario)Session["usuario"]);
                idObjeto = vTipoPago.cod_tipo_pago.ToString();
            }

            Session[TipoPagoServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoPagoServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Caja.Entities.TipoPago vTipoComp = new Xpinn.Caja.Entities.TipoPago();
            vTipoComp = TipoPagoServicio.ConsultarTipoPago(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vTipoComp.cod_tipo_pago.ToString()))
                txtTipoPago.Text = HttpUtility.HtmlDecode(vTipoComp.cod_tipo_pago.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoComp.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vTipoComp.descripcion.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoComp.caja))
                ddlCaja.SelectedValue = HttpUtility.HtmlDecode(vTipoComp.caja.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoPagoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


}