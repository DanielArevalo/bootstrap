using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.ServiceModel;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;


public partial class Page_FabricaCreditos_Solicitud_InformacionFinanciera_BalanceGeneralMicroempresa_Default : System.Web.UI.Page
{
    Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
        ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();
        mvBalanceGeneralMicroempres.ActiveViewIndex = 0;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
            
    }

    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/Inventarios/Default.aspx");

    }


    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/ComposicionPasivo/Lista.aspx");
    }

    private void VerError(string p)
    {
        throw new NotImplementedException();
    }


    protected void btnNo_Click(object sender, EventArgs e)
    {
        mvBalanceGeneralMicroempres.ActiveViewIndex = 2;
    }
    protected void btnNoGr_Click(object sender, EventArgs e)
    {
        //Pregunta si el cliente tiene menos de 65 años
        vPersona1.identificacion = Session["Identificacion"].ToString();
        vPersona1.seleccionar = "Identificacion";
        vPersona1.noTraerHuella = 1;
        vPersona1 = DatosClienteServicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);   //Consulta fecha nacimiento
        int edadCliente = GetAge(Convert.ToDateTime(vPersona1.fechanacimiento.ToString()));

        if (edadCliente < 65)
            mvBalanceGeneralMicroempres.ActiveViewIndex = 3;

    }

    public static int GetAge(DateTime birthDate)
    {
        return (int)Math.Floor((DateTime.Now - birthDate).TotalDays / 365.242199);
    }
    protected void btnSiGr_Click(object sender, EventArgs e)
    {
        //Abre formulario de garantias
        mvBalanceGeneralMicroempres.ActiveViewIndex = 5;
    }
    protected void btnNoMicro_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/Georeferenciacion/Lista.aspx");
    }
    protected void btnSiMicro_Click(object sender, EventArgs e)
    {
        mvBalanceGeneralMicroempres.ActiveViewIndex = 4;
    }
    protected void btnAdelante0_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/Georeferenciacion/Lista.aspx");
    }

    protected void btnAdelanteGarantias_Click(object sender, ImageClickEventArgs e)
    {
        mvBalanceGeneralMicroempres.ActiveViewIndex = 3;
    }
    protected void btnAtrasGarantias_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnSi_Click(object sender, EventArgs e)
    {
        Session["EstadoCodeudor"] = "0";  // Variables de sesion para saber estado civil y codigo del codeudor
        Session["CodCodeudor"] = "0";
        //Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionCodeudor/Default.aspx");
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionCodeudor/Codeudor/Lista.aspx");
    }
    protected void btnAtras0_Click(object sender, ImageClickEventArgs e)
    {
        mvBalanceGeneralMicroempres.ActiveViewIndex = 0;
    }
}