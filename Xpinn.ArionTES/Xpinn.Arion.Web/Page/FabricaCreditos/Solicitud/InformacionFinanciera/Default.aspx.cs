using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Page_FabricaCreditos_Solicitud_InformacionFinanciera_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {

            Site1 toolBar = (Site1)this.Master;
            //toolBar.eventoNuevo += btnNuevo_Click;
            //toolBar.eventoConsultar += btnConsultar_Click;
            ////toolBar.eventoLimpiar += btnLimpiar_Click;
            //toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoAdelante += btnAdelante_Click;
            toolBar.eventoAtras += btnAtras_Click;

            ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();

        
            ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
            btnAdelante.ValidationGroup = "";
            btnAdelante.ImageUrl = "~/Images/btnMargenVentas.jpg";
      
      
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private void variablesSesion()
    {
      
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        variablesSesion();
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/EstacionalidadSemanal/Lista.aspx");
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        variablesSesion();
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/EstacionalidadMensual/Lista.aspx");
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        variablesSesion();
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/MargenVentas/Lista.aspx");
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        variablesSesion();
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/ComposicionPasivo/Lista.aspx");
   
    }
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        
        variablesSesion();
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/InventarioActivoFijo/Lista.aspx");
  
    }
    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {
        variablesSesion();
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/InventarioMateriaPrima/Lista.aspx");
  
    }
    protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
    {
        variablesSesion();
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/ProductosEnProceso/Lista.aspx");
  
    }
    protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
    {
        variablesSesion();
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/ProductosTerminados/Lista.aspx");
 
    }
    protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
    {
        variablesSesion();
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/IngresosFamilia/Lista.aspx");
 
    }
    protected void ImageButton10_Click(object sender, ImageClickEventArgs e)
    {
        variablesSesion();
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/EgresosFamilia/Lista.aspx");
 
    }
    protected void ImageButton11_Click(object sender, ImageClickEventArgs e)
    {
        variablesSesion();
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/BalanceGeneralFamilia/Lista.aspx");
 
    }
    protected void ImageButton12_Click(object sender, ImageClickEventArgs e)
    {
        variablesSesion();
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/InformacionFinancieraNegocio/Lista.aspx"); 
    }





    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/Referencias/Lista.aspx");
    }
    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/MargenVentas/Lista.aspx");
    }
}