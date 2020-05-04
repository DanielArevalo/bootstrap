<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Drawing;
using System.Data.Common;

public class Handler : IHttpHandler
{
    private Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();


    public void ProcessRequest(HttpContext context)
    {
        List<Xpinn.FabricaCreditos.Entities.Imagenes> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Imagenes>();
        Xpinn.FabricaCreditos.Entities.Imagenes vImagenes = new Xpinn.FabricaCreditos.Entities.Imagenes();
        vImagenes.idimagen = Convert.ToInt32(context.Request.QueryString["id"]);
        Usuario pUsuario = new Usuario();
        lstConsulta = personaServicio.Handler(vImagenes, pUsuario);
        if (lstConsulta.Count > 0)
        {
            context.Response.ContentType = "jpg";
            if (lstConsulta[0].imagen != null)
                context.Response.BinaryWrite((byte[])lstConsulta[0].imagen);
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