using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;



partial class Nuevo : GlobalWeb
{
    List<Xpinn.FabricaCreditos.Entities.ControlTiempos> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.ControlTiempos>();  //Lista de los menus desplegables
    private Xpinn.FabricaCreditos.Services.ControlTiemposService ControlProcesosServicio = new Xpinn.FabricaCreditos.Services.ControlTiemposService();
   
    private Xpinn.FabricaCreditos.Services.DocumentosAnexosService DocumentosAnexosServicio = new Xpinn.FabricaCreditos.Services.DocumentosAnexosService();
    String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    Xpinn.FabricaCreditos.Services.ViabilidadFinancieraService ViabilidadFinancieraServicio = new Xpinn.FabricaCreditos.Services.ViabilidadFinancieraService();
    private Xpinn.FabricaCreditos.Services.ControlCreditosService ControlCreditosServicio = new Xpinn.FabricaCreditos.Services.ControlCreditosService(); 
    private Xpinn.FabricaCreditos.Services.CreditoService CreditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();

    CreditoPlanService creditoPlanServicio = new CreditoPlanService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
          
            VisualizarOpciones(DocumentosAnexosServicio.CodigoPrograma, "L");

            Site1 toolBar = (Site1)this.Master;
            toolBar.eventoAtras += btnAtras_Click;
            toolBar.eventoAdelante += btnAdelante_Click;

            ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();

            ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
            btnAdelante.ValidationGroup = "vgGuardar";
            toolBar.eventoAdelante += btnAdelante_Click;
            btnAdelante.ImageUrl = "~/Images/btnFin.jpg";

            Xpinn.FabricaCreditos.Entities.Credito vCredito = new Xpinn.FabricaCreditos.Entities.Credito();
            Int64 vNumeroSolicitud = Convert.ToInt64(Session["NumeroSolicitud"].ToString());
            try
            {
                vCredito = CreditoServicio.ConsultarCreditoPorObligacion(vNumeroSolicitud, (Usuario)Session["usuario"]);
            }
            catch
            {
                vCredito = CreditoServicio.ConsultarCreditoSolicitud(vNumeroSolicitud, (Usuario)Session["usuario"]);
            }
            
            if (vCredito.numero_radicacion != Int64.MinValue)
                txtRadicacion.Text = HttpUtility.HtmlDecode(vCredito.numero_radicacion.ToString().Trim());
            if (vCredito.monto != Int64.MinValue)
                txtMonto.Text = HttpUtility.HtmlDecode(vCredito.monto.ToString().Trim());
            if (vCredito.valor_cuota != Int64.MinValue)
                txtCuota.Text = HttpUtility.HtmlDecode(vCredito.valor_cuota.ToString().Trim());
            if (vCredito.plazo != Int64.MinValue)
                txtPlazo.Text = HttpUtility.HtmlDecode(vCredito.numero_cuotas.ToString().Trim());     

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMensaje.Visible = false;
        try
        {
            if (!IsPostBack)
            {
                
              
                  Xpinn.FabricaCreditos.Entities.ViabilidadFinanciera vViabilidadFinanciera = new Xpinn.FabricaCreditos.Entities.ViabilidadFinanciera();
                vViabilidadFinanciera.numeroSolicitud = Convert.ToInt64(Session["NumeroSolicitud"].ToString());
                vViabilidadFinanciera.identificacion = Session["Identificacion"].ToString();
                vViabilidadFinanciera.prueba = 0;
                vViabilidadFinanciera.endeudamiento = 0;
                vViabilidadFinanciera.rotacioncuentas = 0;
                vViabilidadFinanciera.gastos = 0;
                vViabilidadFinanciera.rotacioncuentaspagar = 0;
                vViabilidadFinanciera.rotacioncapital = 0;
                vViabilidadFinanciera.rotacioninventarios = 0;
                vViabilidadFinanciera.puntoequilibrio = 0;
                vViabilidadFinanciera.ef = 0;
                vViabilidadFinanciera.observaciones = txtObservaciones.Text;
                vViabilidadFinanciera = ViabilidadFinancieraServicio.CrearViabilidadFinanciera(vViabilidadFinanciera, (Usuario)Session["usuario"]);// DatosSolicitudServicio.ViabilidadFinanciera(vViabilidadFinanciera);                    

                lblCodigo.Text = vViabilidadFinanciera.cod_viabilidad.ToString();
                txtPruebaAcida.Text = vViabilidadFinanciera.prueba.ToString();
                txtEndeudamientoTot.Text = vViabilidadFinanciera.endeudamiento.ToString();
                txtRotacionCC.Text = vViabilidadFinanciera.rotacioncuentas.ToString();
                txtGastosFamiliares.Text = vViabilidadFinanciera.gastos.ToString();
                txtRotacionCP.Text = vViabilidadFinanciera.rotacioncuentaspagar.ToString();
                txtRotacionCap.Text = vViabilidadFinanciera.rotacioncapital.ToString();
                txtRotacionInv.Text = vViabilidadFinanciera.rotacioninventarios.ToString();
                txtPuntoEquilibrio.Text = vViabilidadFinanciera.puntoequilibrio.ToString();
                txtEF.Text = vViabilidadFinanciera.ef.ToString();
               
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        
        try
        {
            CreditoPlan credito = new CreditoPlan();

            if (pIdObjeto != null)
            {
                credito.Numero_radicacion = Int32.Parse(pIdObjeto);
                try
                {
                    credito = creditoPlanServicio.ConsultarCredito(credito.Numero_radicacion, false, (Usuario)Session["usuario"]);
                }
                catch
                {
                }

                if (!string.IsNullOrEmpty(credito.Numero_radicacion.ToString()))
                    txtRadicacion.Text = HttpUtility.HtmlDecode(credito.Numero_radicacion.ToString());
                if (!string.IsNullOrEmpty(credito.Cuota.ToString()))
                {
                    txtCuota.Text = HttpUtility.HtmlDecode(credito.Cuota.ToString());
                    txtCuota.Text = String.Format("{0:C}", Convert.ToInt64(txtCuota.Text));
                };
                if (!string.IsNullOrEmpty(credito.Monto.ToString()))
                {
                    txtMonto.Text = HttpUtility.HtmlDecode(credito.Monto.ToString());
                    txtMonto.Text = String.Format("{0:C}", Convert.ToInt64(txtMonto.Text));
                };
                if (!string.IsNullOrEmpty(credito.Plazo.ToString()))
                    txtPlazo.Text = HttpUtility.HtmlDecode(credito.Plazo.ToString());
              
            }
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/CapturaDocumentos/DocumentosAnexos/Lista.aspx");
    }

    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        ////Boolean result = true;

        ////if (Session["Observaciones"] == "" || Session["Observaciones"] == null || txtObservaciones.Text == "")
        ////    {
                
                           
               
        ////}
        ////else
        ////{
        ////this.lblMensaje2.Visible = false;
        ////GuardarObservacion();
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/PlanPagos/Lista.aspx"); //Finaliza la solicitud de microcredito         
        //}
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarObservacion();
    }

    private void GuardarObservacion()
    {
        Usuario pUsuario = (Usuario)Session["usuario"];
        try
        {
            if (Session["Numero_Radicacion"] != null)
            {


                Xpinn.FabricaCreditos.Entities.ViabilidadFinanciera vViabilidadFinanciera = new Xpinn.FabricaCreditos.Entities.ViabilidadFinanciera();
                if (Session["Cod_persona"] != null)
                    vViabilidadFinanciera.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
                vViabilidadFinanciera.cod_viabilidad = Convert.ToInt64(lblCodigo.Text);
                vViabilidadFinanciera.identificacion = Session["Identificacion"].ToString();
                vViabilidadFinanciera.prueba = Convert.ToDouble(txtPruebaAcida.Text);
                vViabilidadFinanciera.endeudamiento = Convert.ToDouble(txtEndeudamientoTot.Text);
                vViabilidadFinanciera.rotacioncuentas = Convert.ToDouble(txtRotacionCC.Text);
                vViabilidadFinanciera.gastos = Convert.ToDouble(txtGastosFamiliares.Text);
                vViabilidadFinanciera.rotacioncuentaspagar = Convert.ToDouble(txtRotacionCP.Text);
                vViabilidadFinanciera.rotacioncapital = Convert.ToDouble(txtRotacionCap.Text);
                vViabilidadFinanciera.rotacioninventarios = Convert.ToDouble(txtRotacionInv.Text);
                vViabilidadFinanciera.puntoequilibrio = Convert.ToDouble(txtPuntoEquilibrio.Text);
                vViabilidadFinanciera.ef = Convert.ToDouble(txtEF.Text);
                vViabilidadFinanciera.observaciones = txtObservaciones.Text;
                
                vViabilidadFinanciera = ViabilidadFinancieraServicio.ModificarViabilidadFinanciera(vViabilidadFinanciera, pUsuario);
              Session["Observaciones"] = vViabilidadFinanciera.observaciones;
            }
            //if (Session["Numero_Radicacion"] != null)
            //{
            //    Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();
            //    vControlCreditos.numero_radicacion = Convert.ToInt64(Session["Numero_Radicacion"].ToString());
            //    vControlCreditos.codtipoproceso = "1";
            //    vControlCreditos.fechaproceso = Convert.ToDateTime(DateTime.Now);
            //    vControlCreditos.cod_persona = pUsuario.codusuario;
            //    vControlCreditos.cod_motivo = 0;
            //    //if (txtObservaciones.Text != "")
            //    //{
            //    //    vControlCreditos.observaciones = txtObservaciones.Text.Length >= 250 ? txtObservaciones.Text.Substring(0, 249) : txtObservaciones.Text.Substring(0, txtObservaciones.Text.Length);
            //    //}
            //    //if (txtObservaciones.Text == "" || txtObservaciones.Text == null ||  Session["Observaciones"] == null || vControlCreditos.observaciones == "")
            //    //{
            //    //    txtObservaciones.Text = "0";
            //    //}    
            //    //ControlCreditos.observaciones= Convert.ToString(Session["Observaciones"]);
            //    //if (txtObservaciones.Text == "")
            //    //{
            //    //    vControlCreditos.observaciones = "0";
            //    //}
            //    //else
            //    //{
            //    //    vControlCreditos.observaciones = Convert.ToString(Session["Observaciones"]);
            //    //}
            //    //vControlCreditos.anexos = "";
            //    //vControlCreditos.nivel = 0;
               
            //    //vControlCreditos = ControlCreditosServicio.CrearControlCreditos(vControlCreditos, (Usuario)Session["usuario"]);
               
            //}
            Session["Observaciones"] = txtObservaciones.Text;
            lblMensaje.Visible = true;
            lblMensaje2.Visible = false;
            btnGuardar.Enabled = false;
        }
        
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }
   
   
}