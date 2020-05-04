using System;
using System.Collections.Generic;
using System.Web.UI;
using Xpinn.Util;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using System.Web.Script.Services;
using System.Web.Services;


public delegate void btnGuardar_ActionsDelegate(object sender, EventArgs e);
public delegate void imgGuardar_ActionsDelegate(object sender, ImageClickEventArgs e);

public partial class ctlValidarBiometria : System.Web.UI.UserControl
{
    public event btnGuardar_ActionsDelegate eventoGuardar;
    public event imgGuardar_ActionsDelegate eventoGuardarimg;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove("ValidarBiometria");
        }
    }

    public bool IniciarValidacion(Int32 CodigoPrograma, string pNumeroProducto, Int64 pCodPersona, DateTime pFecha, ref string sError)
    {
        sError = "";
        string validarBiometriaDesembolso = "";
        try
        {
            Xpinn.Seguridad.Services.OpcionService objeOpciones = new Xpinn.Seguridad.Services.OpcionService();
            Xpinn.Seguridad.Entities.Opcion opcion = new Xpinn.Seguridad.Entities.Opcion();
            opcion = objeOpciones.ConsultarOpcion(CodigoPrograma, (Usuario)Session["usuario"]);
            if (opcion.validar_Biometria == null)
                validarBiometriaDesembolso = "";
            else
            validarBiometriaDesembolso = opcion.validar_Biometria.ToString();
        }
        catch
        {
            validarBiometriaDesembolso = "";
        }
        if (validarBiometriaDesembolso != null)
        {
            if (validarBiometriaDesembolso == "1")
            {
                string validarBiometria = "";
                if (Session["ValidarBiometria"] != null)
                {
                    validarBiometria = Session["ValidarBiometria"].ToString();
                }
                if (validarBiometria == "")
                {                  
                    Int64? autorizacion = null;
                    Xpinn.FabricaCreditos.Services.PersonaBiometriaService personaBiometria = new Xpinn.FabricaCreditos.Services.PersonaBiometriaService();
                    Xpinn.FabricaCreditos.Entities.PersonaBiometria biometria = new Xpinn.FabricaCreditos.Entities.PersonaBiometria();
                    biometria = personaBiometria.ConsultarPersonaBiometria(pCodPersona, 0, (Usuario)Session["Usuario"]);
                    if (biometria != null && biometria.identificacion != null)
                    {
                        txtIdentificacion.Text = biometria.identificacion;
                        txtNombres.Text = biometria.nombres;
                        txtApellidos.Text = biometria.apellidos;
                        Xpinn.FabricaCreditos.Services.CreditoService creditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();
                        autorizacion = creditoServicio.AutorizarCredito(Convert.ToInt64(pNumeroProducto), pCodPersona, pFecha, ref sError, (Usuario)Session["usuario"]);
                        if (autorizacion != null && sError == "")
                        {
                            txtAutorizacion.Text = autorizacion.ToString();
                            Session["Biometria"] = autorizacion;
                            mpeMensaje.Show();
                            return true;
                        }
                        else
                        {
                            Session.Remove("Biometria");
                        }
                    }
                    else
                    {
                        if (biometria.identificacion == null && sError.Trim() == "")
                        {
                            sError = "La persona no tiene enrolada una huella";
                            return true;
                        }
                    }
                    if (sError.Trim() != "")
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    protected void btnContinuarBiometria_Click(object sender, EventArgs e)
    {
        lblMensajeBiometria.Text = "";
        if (Session["Biometria"] != null)
        {
            if (Session["Biometria"].ToString() != "")
            {
                Xpinn.FabricaCreditos.Services.CreditoService creditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();
                String sAutorizacion = Session["Biometria"].ToString();
                Int32 nValidar = 0;
                nValidar = creditoServicio.VerificarAutorizacion(sAutorizacion, (Usuario)Session["Usuario"]);
                if (nValidar == 2)
                {
                    Session["ValidarBiometria"] = "1";
                    mpeMensaje.Hide();
                    btnGuardar_Click(null, null);
                }
                else
                {
                    mpeMensaje.Show();
                    lblMensajeBiometria.Text = "No ha sido autorizado el desembolso en el módulo de biometría. #" + sAutorizacion;
                }
            }
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (eventoGuardar != null)
            eventoGuardar(sender, e);
        if (eventoGuardarimg != null)
            eventoGuardarimg(sender, null);
    }

    protected void btnCancelarBiometria_Click(object sender, EventArgs e)
    {
        Session.Remove("ValidarBiometria");
        mpeMensaje.Hide();
    }

    [WebMethod]
    public static double Sumar(string Valor1)
    {
        return Convert.ToDouble(2);
    }


    [WebMethod]
    public static double devolver(string Valor7, double Valor2)
    {
        return Convert.ToDouble(2);
    }


    protected void btn_llamar(object sender, System.EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
        Button2.Enabled = true;
    }

    protected void validar_persona(object sender, System.EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:validar();", true);
    }

}