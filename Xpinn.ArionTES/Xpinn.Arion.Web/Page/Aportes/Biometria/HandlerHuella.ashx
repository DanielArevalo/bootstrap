<%@ WebHandler Language="C#" Class="HandlerHuella" %>

using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Drawing;
using System.Data.Common;

public class HandlerHuella : IHttpHandler {

    private Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();

    public void ProcessRequest(HttpContext context)
    {        
        Int64 codPersona = Convert.ToInt64(context.Request.QueryString["id"]);
        Int64 dedo = 1;
        Int64 idimagen = 0;
        Usuario pUsuario = new Usuario();
        byte[] imagen = personaServicio.ConsultarImagenHuellaPersona(codPersona, dedo, ref idimagen, pUsuario);
        if (imagen != null)
        {
            context.Response.ContentType = "jpg";
            context.Response.BinaryWrite((byte[])imagen);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}