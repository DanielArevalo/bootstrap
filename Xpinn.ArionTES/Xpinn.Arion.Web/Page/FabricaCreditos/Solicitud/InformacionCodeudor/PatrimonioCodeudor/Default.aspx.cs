﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Page_FabricaCreditos_Solicitud_InformacionCodeudor_PatrimonioCodeudor_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
           Site1 toolBar = (Site1)this.Master;
           
            toolBar.eventoAdelante += btnAdelante_Click;
            toolBar.eventoAtras += btnAtras_Click;

            ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();
            
            ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
            btnAdelante.ValidationGroup = "";
            btnAdelante.ImageUrl = "~/Images/btnPresupuestoCodeudor.jpg";
       
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
    /// <summary>
    /// Método para control del evento de click sobre el botón de ir atrás
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionCodeudor/Codeudor/Lista.aspx");
    }

    /// <summary>
    /// Método para control del evento de click sobre el botón de ir adelante
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionCodeudor/Presupuesto/Default.aspx");
    }
}