using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using Xpinn.Util;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using System.Linq;

partial class Nuevo : GlobalWeb
{
    TiposDocCobranzasServices _tipoDocumentoServicio = new TiposDocCobranzasServices();
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_tipoDocumentoServicio.CodigoProgramaFormatoCorreo, "E");

            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_tipoDocumentoServicio.CodigoProgramaFormatoCorreo, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["usuario"];
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_tipoDocumentoServicio.CodigoProgramaFormatoCorreo, "Page_Load", ex);
        }
    }

    protected void ddltipo_documento_OnselectedIndexChanged(object sender, EventArgs e)
    {
        TipoDocumentoCorreo paginaCorreo = ddlTipoDocumento.SelectedValue.ToEnum<TipoDocumentoCorreo>();

        LlenarParametrosFormatoSeleccionado(paginaCorreo);

        if (paginaCorreo != TipoDocumentoCorreo.Ninguno)
        {
            TiposDocCobranzas modificardocumneto = _tipoDocumentoServicio.ConsultarFormatoDocumentoCorreo(Convert.ToInt64(ddlTipoDocumento.SelectedValue), _usuario);

            if (modificardocumneto.texto == null)
            {
                FreeTextBox1.Text = "Ponga su carta en este sitio";
            }
            else
            {
                FreeTextBox1.Text = modificardocumneto.texto;
            }
        }
    }

    private void LlenarParametrosFormatoSeleccionado(TipoDocumentoCorreo tipoDocumentoCorreo)
    {
        List<ListItem> items = new List<ListItem>();
        items.Add(new ListItem(ParametroCorreo.NombreCompletoPersona.ToString(), ((int)ParametroCorreo.NombreCompletoPersona).ToString()));

        if (tipoDocumentoCorreo == TipoDocumentoCorreo.FormatoCumpleaños)
        {
            items.Add(new ListItem(ParametroCorreo.PrimerNombre.ToString(), ((int)ParametroCorreo.PrimerNombre).ToString()));
            items.Add(new ListItem(ParametroCorreo.SegundoNombre.ToString(), ((int)ParametroCorreo.SegundoNombre).ToString()));
            items.Add(new ListItem(ParametroCorreo.PrimerApellido.ToString(), ((int)ParametroCorreo.PrimerApellido).ToString()));
            items.Add(new ListItem(ParametroCorreo.SegundoApellido.ToString(), ((int)ParametroCorreo.SegundoApellido).ToString()));
            items.Add(new ListItem(ParametroCorreo.RazonSocial.ToString(), ((int)ParametroCorreo.RazonSocial).ToString()));
        }      
        else if (tipoDocumentoCorreo == TipoDocumentoCorreo.ControlDocumentos || tipoDocumentoCorreo == TipoDocumentoCorreo.CreditoAplazado || tipoDocumentoCorreo == TipoDocumentoCorreo.CreditoAprobado || tipoDocumentoCorreo == TipoDocumentoCorreo.CreditoNegado || tipoDocumentoCorreo == TipoDocumentoCorreo.HojaRuta)
        {
            items.Add(new ListItem(ParametroCorreo.NumeroRadicacion.ToString(), ((int)ParametroCorreo.NumeroRadicacion).ToString()));
            items.Add(new ListItem(ParametroCorreo.Identificacion.ToString(), ((int)ParametroCorreo.Identificacion).ToString()));
            items.Add(new ListItem(ParametroCorreo.LineaCredito.ToString(), ((int)ParametroCorreo.LineaCredito).ToString()));
            items.Add(new ListItem(ParametroCorreo.FechaCredito.ToString(), ((int)ParametroCorreo.FechaCredito).ToString()));
            items.Add(new ListItem(ParametroCorreo.MontoCredito.ToString(), ((int)ParametroCorreo.MontoCredito).ToString()));
            items.Add(new ListItem(ParametroCorreo.PlazoCredito.ToString(), ((int)ParametroCorreo.PlazoCredito).ToString()));
        }
        else if (tipoDocumentoCorreo == TipoDocumentoCorreo.ControlDocumentos)
        {
            items.Add(new ListItem(ParametroCorreo.DocumentosPendientes.ToString(), ((int)ParametroCorreo.DocumentosPendientes).ToString()));
        }
        else if (tipoDocumentoCorreo == TipoDocumentoCorreo.IcetexAprobado || tipoDocumentoCorreo == TipoDocumentoCorreo.IcetexNegado || tipoDocumentoCorreo == TipoDocumentoCorreo.IcetexAplazado
            || tipoDocumentoCorreo == TipoDocumentoCorreo.IcetexAprobadoInscrito || tipoDocumentoCorreo == TipoDocumentoCorreo.IcetexNegadoInscrito || tipoDocumentoCorreo == TipoDocumentoCorreo.IcetexAplazadoInscrito)
        {
            items.Add(new ListItem(ParametroCorreo.Identificacion.ToString(), ((int)ParametroCorreo.Identificacion).ToString()));
            items.Add(new ListItem(ParametroCorreo.NumeroIcetex.ToString(), ((int)ParametroCorreo.NumeroIcetex).ToString()));
            items.Add(new ListItem(ParametroCorreo.CodConvocatoria.ToString(), ((int)ParametroCorreo.CodConvocatoria).ToString()));
            items.Add(new ListItem(ParametroCorreo.FechaIcetex.ToString(), ((int)ParametroCorreo.FechaIcetex).ToString()));
            items.Add(new ListItem(ParametroCorreo.MontoIcetex.ToString(), ((int)ParametroCorreo.MontoIcetex).ToString()));
            items.Add(new ListItem(ParametroCorreo.ObservacionIcetex.ToString(), ((int)ParametroCorreo.ObservacionIcetex).ToString()));
        }
        else if (tipoDocumentoCorreo == TipoDocumentoCorreo.SolicitudCreditoAtencionWeb)
        {
            items.Add(new ListItem(ParametroCorreo.NumeroSolicitud.ToString(), ((int)ParametroCorreo.NumeroRadicacion).ToString()));
            items.Add(new ListItem(ParametroCorreo.Identificacion.ToString(), ((int)ParametroCorreo.Identificacion).ToString()));
            items.Add(new ListItem(ParametroCorreo.LineaCredito.ToString(), ((int)ParametroCorreo.LineaCredito).ToString()));
            items.Add(new ListItem(ParametroCorreo.FechaCredito.ToString(), ((int)ParametroCorreo.FechaCredito).ToString()));
            items.Add(new ListItem(ParametroCorreo.MontoCredito.ToString(), ((int)ParametroCorreo.MontoCredito).ToString()));
            items.Add(new ListItem(ParametroCorreo.PlazoCredito.ToString(), ((int)ParametroCorreo.PlazoCredito).ToString()));
        }
        else if (tipoDocumentoCorreo == TipoDocumentoCorreo.PagosPorVentanilla)
        {
            items.Add(new ListItem(ParametroCorreo.Identificacion.ToString(), ((int)ParametroCorreo.Identificacion).ToString()));
            items.Add(new ListItem(ParametroCorreo.FechaPago.ToString(), ((int)ParametroCorreo.FechaPago).ToString()));
            items.Add(new ListItem(ParametroCorreo.OficinaPago.ToString(), ((int)ParametroCorreo.OficinaPago).ToString()));
        }
        else if (tipoDocumentoCorreo == TipoDocumentoCorreo.ExtractoAhorros)
        {
        }
        else if (tipoDocumentoCorreo == TipoDocumentoCorreo.ConfirmaciónProductoWeb)
        {
            items.Add(new ListItem(ParametroCorreo.nombreProducto.ToString(), ((int)ParametroCorreo.nombreProducto).ToString()));
        }
        else if (tipoDocumentoCorreo == TipoDocumentoCorreo.RechazoProductoWeb)
        {
            items.Add(new ListItem(ParametroCorreo.nombreProducto.ToString(), ((int)ParametroCorreo.nombreProducto).ToString()));
        }

        ddlParametroFormato.Items.Clear();
        ddlParametroFormato.Items.AddRange(items.ToArray());
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (ddlTipoDocumento.SelectedValue == "0")
        {
            VerError("Seleccione un tipo de documento");
            return;
        }

        try
        {
            // Esta consulta me trae el ID si el ID es diferente a 0 Modifico, si es 0 Creo
            TiposDocCobranzas tipoDocumento = _tipoDocumentoServicio.ConsultarFormatoDocumentoCorreo(Convert.ToInt64(ddlTipoDocumento.SelectedValue), _usuario, verificarSoloSiExiste: true);
            tipoDocumento.texto = FreeTextBox1.Text;
            tipoDocumento.tipo = ddlTipoDocumento.SelectedValue;
            tipoDocumento.descripcion = ddlTipoDocumento.SelectedItem.Text;

            if (tipoDocumento.id == 0)
            {
                tipoDocumento = _tipoDocumentoServicio.CrearFormatoDocumentoCorreo(tipoDocumento, _usuario);
            }
            else
            {
                tipoDocumento = _tipoDocumentoServicio.ModificarFormatoDocumentoCorreo(tipoDocumento, _usuario);
            }

            // Si el guardado fue exitoso sigo adelante
            if (tipoDocumento != null)
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarCancelar(false);

                mvTipoDopcumento.ActiveViewIndex = 1;
            }
            else
            {
                VerError("Error al guardar el documento");
            }
        }
        catch(Exception ex)
        {
            VerError("Fallo al guardar el documento, " + ex.Message);
            return;
        }
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarCancelar(true);

        mvTipoDopcumento.ActiveViewIndex = 0;
    }
}