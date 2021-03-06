﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Afiliacion_Default : System.Web.UI.Page
{

    xpinnWSEstadoCuenta.SolicitudPersonaAfi pEntidad = new xpinnWSEstadoCuenta.SolicitudPersonaAfi();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            TextBox identificacion = Master.FindControl("IDENTIFICACION") as TextBox;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        btnContinuar.Click += btnContinuar_Click;
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        Response.Redirect("R10_Temas.aspx");
    }
}