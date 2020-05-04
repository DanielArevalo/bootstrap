<%@ WebHandler Language="C#" Class="HandlerFoto" %>

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


public class HandlerFoto : IHttpHandler {

    private Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();

    public void ProcessRequest(HttpContext context)
    {
        Xpinn.FabricaCreditos.Entities.Imagenes imagen = new Xpinn.FabricaCreditos.Entities.Imagenes();
        string Identificacion = Convert.ToString(context.Request.QueryString["id"]);
        Usuario pUsuario = new Usuario();
        imagen = personaServicio.ConsultarImagenesPersonaIdentificacion(Identificacion, 1, pUsuario);
        if (imagen != null)
        {
            context.Response.ContentType = "jpg";
            context.Response.BinaryWrite((byte[])imagen.foto);
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