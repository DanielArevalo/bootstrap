using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Reporteador.Services;
using Xpinn.Reporteador.Entities;
using System.Data;
using Xpinn.Util;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using ClosedXML.Excel;
using System.Globalization;

public partial class Page_Contabilidad_Conceptos_DIAN_Nuevo : GlobalWeb
{
    ExogenaReportService objReporteService = new ExogenaReportService();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[objReporteService.CodigoTiposConcepto + ".id"] != null)
                VisualizarOpciones(objReporteService.CodigoTiposConcepto, "E");
            else
                VisualizarOpciones(objReporteService.CodigoTiposConcepto, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objReporteService.CodigoTiposConcepto, "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtCodconvenio.Enabled = false;
            if (Session[objReporteService.CodigoTiposConcepto + ".id"] != null)
            {
                idObjeto = Session[objReporteService.CodigoTiposConcepto + ".id"].ToString();
                Session.Remove(objReporteService.CodigoTiposConcepto + ".id");
                ObtenerDatos(idObjeto);
            }
        }
    }
    private void ObtenerDatos(string idObjeto)
    {
        try
        {
            ExogenaReport CONCEPTOSDIAN = new ExogenaReport();
            string pFiltro = " WHERE CODCONCEPTO = " + idObjeto;
            CONCEPTOSDIAN = objReporteService.ConsultarConceptosDian(pFiltro, Usuario);
            if (CONCEPTOSDIAN != null)
            {
                if (CONCEPTOSDIAN.codconcepto != 0)
                    txtCodconvenio.Text = CONCEPTOSDIAN.codconcepto.ToString();
                if (!string.IsNullOrEmpty(CONCEPTOSDIAN.nombre))
                    txtnomconcepto.Text = CONCEPTOSDIAN.nombre;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objReporteService.CodigoTiposConcepto, "ObtenerDatos", ex);
        }
    }
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Page.Validate();
        if (Page.IsValid)
        {
            if (validarDatos())
                ctlMensaje.MostrarMensaje("Desea realizar la grabación del Tipo de Concepto?");
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {

        VerError("");
        try
        {
            ExogenaReport pEntidad = new ExogenaReport();
        
            pEntidad.nombre = txtnomconcepto.Text.Trim();
            string msj = string.Empty;
            if (idObjeto != "")
            {
                pEntidad.codconcepto = Convert.ToInt32(txtCodconvenio.Text.Trim());
                pEntidad = objReporteService.CrearTiposConceptos(pEntidad, Usuario,2);
                msj = "modificaron";
            }
            else
            {
                pEntidad = objReporteService.CrearTiposConceptos(pEntidad, Usuario,1);
                msj = "grabaron";
            }

            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            mvAplicar.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }
    private bool validarDatos()
    {
        if (!string.IsNullOrEmpty(idObjeto))
        {
            if (txtCodconvenio.Text.Trim() == "")
            {
                VerError("Ingrese el código de convenio");
                return false;
            }
        }
        if (txtnomconcepto.Text.Trim() == "")
        {
            VerError("Ingrese el nombre del convenio");
            return false;
        }
        return true;
    }
}
