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
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Obligaciones.Services;
using Xpinn.Obligaciones.Entities;


public partial class DetallePagosPendientes : GlobalWeb
{
    private Xpinn.Obligaciones.Services.SolicitudService SolicitudServicio = new Xpinn.Obligaciones.Services.SolicitudService();
    private Xpinn.Obligaciones.Services.ObligacionCreditoService ObligacionCreditoServicio = new Xpinn.Obligaciones.Services.ObligacionCreditoService();
    private Xpinn.Obligaciones.Entities.ObligacionCredito ObligaCred = new Xpinn.Obligaciones.Entities.ObligacionCredito();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ObligacionCreditoServicio.CodigoPrograma2 + ".id"] != null)
                VisualizarOpciones(ObligacionCreditoServicio.CodigoPrograma2, "E");
            else
                VisualizarOpciones(ObligacionCreditoServicio.CodigoPrograma2, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += btnRegresar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma2, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ImprimirGrilla();

                LlenarComboEntidades(ddlEntidad);
                
                if (Session[ObligacionCreditoServicio.CodigoPrograma2 + ".id"] != null)
                {
                    idObjeto = Session[ObligacionCreditoServicio.CodigoPrograma2 + ".id"].ToString();
                    Session.Remove(ObligacionCreditoServicio.CodigoPrograma2 + ".id");
                    ObtenerDatos(idObjeto);
                    ActualizarDistribPagosPendientesCuotas();
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma2, "Page_Load", ex);
        }
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Session[ObligacionCreditoServicio.CodigoPrograma2 + ".id"] = idObjeto;
        Navegar("../EstadoCuenta/Detalle.aspx");
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Obligaciones.Entities.Solicitud vSolicitud = new Xpinn.Obligaciones.Entities.Solicitud();
            vSolicitud = SolicitudServicio.ConsultarEstadoCuenta(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vSolicitud.codobligacion.ToString()))
                txtNroObligacion.Text = vSolicitud.codobligacion.ToString();

            if (!string.IsNullOrEmpty(vSolicitud.estadoobligacion.ToString()))
                txtEstado.Text = vSolicitud.estadoobligacion.ToString();

            if (!string.IsNullOrEmpty(vSolicitud.codentidad.ToString()))
                ddlEntidad.SelectedValue = vSolicitud.codentidad.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.montoaprobado.ToString()))
                txtMontoApro.Text = vSolicitud.montoaprobado.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.fecha_aprobacion.ToShortDateString()))
                txtFechaDesembolso.Text = vSolicitud.fecha_aprobacion.ToShortDateString();
            if (!string.IsNullOrEmpty(vSolicitud.montoaprobado.ToString()))
                txtMontoApro.Text = vSolicitud.montoaprobado.ToString();
            if (!string.IsNullOrEmpty(vSolicitud.numeropagare.ToString()))
                txtPagare.Text = vSolicitud.numeropagare.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma2, "ObtenerDatos", ex);
        }
    }


    protected void LlenarComboEntidades(DropDownList ddlEntidades)
    {
        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
        ddlEntidades.DataSource = bancoService.ListarBancos(banco, (Usuario)Session["usuario"]);
        ddlEntidades.DataTextField = "nombrebanco";
        ddlEntidades.DataValueField = "cod_banco";
        ddlEntidades.DataBind();
    }


    protected void ImprimirGrilla()
    {
        string printScript =
                    @"function PrintGridView()
         {
     
            div = document.getElementById('DivButtons');
            div.style.display='none';

            var gridInsideDiv = document.getElementById('gvDiv');
            var printWindow = window.open('gview.htm','PrintWindow','letf=0,top=0,width=150,height=300,toolbar=1,scrollbars=1,status=1');
            printWindow.document.write(gridInsideDiv.innerHTML);
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
            printWindow.close();}";

        this.ClientScript.RegisterStartupScript(Page.GetType(), "PrintGridView", printScript.ToString(), true);

        btnImprimir.Attributes.Add("onclick", "PrintGridView();");

    }


    private void ActualizarDistribPagosPendientesCuotas()
    {
        try
        {
            List<Xpinn.Obligaciones.Entities.ObligacionCredito> lstConsulta = new List<Xpinn.Obligaciones.Entities.ObligacionCredito>();
            ObligaCred.codobligacion = long.Parse(idObjeto);
            ObligaCred.fechaproximopago = Convert.ToString(Session["FechaProxPago"]);
            lstConsulta = ObligacionCreditoServicio.ListarDistribPagosPendCuotas(ObligaCred, (Usuario)Session["usuario"]);

            gvDistribPagPenCuo.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvDistribPagPenCuo.Visible = true;
                gvDistribPagPenCuo.DataBind();
            }
            else
            {
                gvDistribPagPenCuo.Visible = false;
            }

            Session.Add(ObligacionCreditoServicio.CodigoPrograma2 + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObligacionCreditoServicio.CodigoPrograma2, "Actualizar", ex);
        }
    }

}