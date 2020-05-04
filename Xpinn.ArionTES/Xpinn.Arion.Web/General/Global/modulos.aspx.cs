using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;


public partial class modulos : System.Web.UI.Page
{
    protected override void InitializeCulture()
    {

        string Temp = "";
        if (Session["temp"] != null)
        {
            Temp = Session["temp"].ToString();
            Thread.CurrentThread.CurrentCulture = new CultureInfo(Temp);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Temp);
        }

    }
    private void Page_PreInit(object sender, EventArgs e)
    {
        base.InitializeCulture();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)

        {
         

        if (Session["usuario"] != null)
            {
                Configuracion conf = new Configuracion();
                if (conf.ObtenerValorConfig("Modulo") != null)
                {
                    if (conf.ObtenerValorConfig("Modulo").ToString().Trim() != "")
                    {
                        Session["modulo"] = conf.ObtenerValorConfig("Modulo");
                        Response.Redirect("~/General/Global/inicio.aspx");
                        return;
                    }
                }            
                lblDireccionIP.Text = (string)Session["ipusuario"] + "-" + (string)Session["macusuario"];
                if (((Usuario)Session["usuario"]).codperfil == 1)
                {
                    hlkSeguridad.Visible = true;
                }
                else
                {
                    Xpinn.Seguridad.Services.PerfilService perfilServicio = new Xpinn.Seguridad.Services.PerfilService();
                    List<Xpinn.Seguridad.Entities.Acceso> lstAcceso = new List<Xpinn.Seguridad.Entities.Acceso>();
                    Usuario pUsuario = (Usuario)Session["usuario"];

                    lstAcceso = perfilServicio.ListarOpciones(pUsuario.codperfil, 1, pUsuario);
                    Int64? seg = 0;
                    Xpinn.Seguridad.Entities.Acceso pAcceso = lstAcceso.Where(x => x.cod_proceso == 901 && (x.consultar == 1 || x.borrar == 1 || x.insertar == 1 || x.modificar == 1)).FirstOrDefault();
                    if (lstAcceso.Count > 0 && pAcceso != null)
                    {
                        hlkSeguridad.Visible = true;
                    }
                    else
                    {
                        pAcceso = lstAcceso.Where(x => x.cod_proceso == 902 && (x.consultar == 1 || x.borrar == 1 || x.insertar == 1 || x.modificar == 1)).FirstOrDefault();
                        if(pAcceso != null)
                        {
                            hlkSeguridad.Visible = true;
                            hlkSeguridad.Text = "Auditoria";
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("~/General/Global/FinSesion.htm");
            }
        }
    }

    protected void btnFabrica_Click(object sender, ImageClickEventArgs e)
    {
        Session["modulo"] = "10";
        Response.Redirect("~/General/Global/inicio.aspx");
    }

    protected void btnAsesores_Click(object sender, ImageClickEventArgs e)
    {
        Session["modulo"] = "11";
        Response.Redirect("~/General/Global/inicio.aspx");
    }

    protected void btnScoring_Click(object sender, ImageClickEventArgs e)
    {
        Session["modulo"] = "16";
        Response.Redirect("~/General/Global/inicio.aspx");
    }

    protected void btnObligaciones_Click(object sender, ImageClickEventArgs e)
    {
        Session["modulo"] = "13";
        Response.Redirect("~/General/Global/inicio.aspx");
    }

    protected void btnIndicadores_Click(object sender, ImageClickEventArgs e)
    {
        Session["modulo"] = "14";
        Response.Redirect("~/General/Global/inicio.aspx");
    }

    protected void btnCaja_Click(object sender, ImageClickEventArgs e)
    {
        Session["modulo"] = "12";
        Response.Redirect("~/General/Global/inicio.aspx");
    }

    protected void btnContabilidad_Click(object sender, ImageClickEventArgs e)
    {
        Session["modulo"] = "3";
        Response.Redirect("~/General/Global/inicio.aspx");
    }

    protected void btnCartera_Click(object sender, ImageClickEventArgs e)
    {
        Session["modulo"] = "6";
        Response.Redirect("~/General/Global/inicio.aspx");
    }

    protected void btnTesoreria_Click(object sender, ImageClickEventArgs e)
    {
        Session["modulo"] = "4";
        Response.Redirect("~/General/Global/inicio.aspx");
    }

    protected void hlkCambio_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/General/Account/CambiarClave.aspx");
    }

    protected void hlkSeguridad_Click(object sender, EventArgs e)
    {
        Session["modulo"] = "1";
        Response.Redirect("~/General/Global/inicio.aspx");
    }

    protected void btnRecaudos_Click(object sender, ImageClickEventArgs e)
    {
        Session["modulo"] = "18";
        Response.Redirect("~/General/Global/inicio.aspx");
    }

    protected void btnAportes_Click(object sender, ImageClickEventArgs e)
    {
        Session["modulo"] = "17";
        Response.Redirect("~/General/Global/inicio.aspx");
    }

    protected void btnNIIF_Click(object sender, ImageClickEventArgs e)
    {
        Session["modulo"] = "21";
        Response.Redirect("~/General/Global/inicio.aspx");
    }

    protected void btnReportes_Click(object sender, ImageClickEventArgs e)
    {
        Session["modulo"] = "20";
        Response.Redirect("~/General/Global/inicio.aspx");
    }

    protected void btnActivos_Click(object sender, ImageClickEventArgs e)
    {
        Session["modulo"] = "5";
        Response.Redirect("~/General/Global/inicio.aspx");
    }

    protected void btnDepositos_Click(object sender, ImageClickEventArgs e)
    {
        Session["modulo"] = "22";
        Response.Redirect("~/General/Global/inicio.aspx");
    }

    protected void btnNomina_Click(object sender, ImageClickEventArgs e)
    {
        Session["modulo"] = "25";
        Response.Redirect("~/General/Global/inicio.aspx");
    }

    protected void btnRiesgo_Click(object sender, ImageClickEventArgs e)
    {
        Session["modulo"] = "27";
        Response.Redirect("~/General/Global/inicio.aspx");
    }

    protected void btnTransferencia_Click(object sender, ImageClickEventArgs e)
    {
        Session["modulo"] = "28";
        Response.Redirect("~/General/Global/inicio.aspx");
    }
}