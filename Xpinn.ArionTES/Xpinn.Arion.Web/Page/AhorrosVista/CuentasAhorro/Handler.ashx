<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Collections.Generic;
using System.Web;
using Xpinn.Util;

public class Handler : IHttpHandler
{
    private Xpinn.Ahorros.Services.AhorroVistaServices ahorroServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();


    public void ProcessRequest(HttpContext context)
    {
        // Determinar código de la imagen a mostrar
        List<Xpinn.Ahorros.Entities.Imagenes> lstConsulta = new List<Xpinn.Ahorros.Entities.Imagenes>();
        Xpinn.Ahorros.Entities.Imagenes vImagenes = new Xpinn.Ahorros.Entities.Imagenes();
        vImagenes.idimagen = Convert.ToInt32(context.Request.QueryString["id"]);
        // Determinar el usuario  
        Usuario pUsuario = new Usuario();
        try
        {            
            pUsuario.identificacion = Convert.ToString(context.Request.QueryString["us"]);
            pUsuario.clave = Convert.ToString(context.Request.QueryString["pw"]);
            CifradoBusiness cifrar = new CifradoBusiness();
            pUsuario.clave_sinencriptar = cifrar.Desencriptar(pUsuario.clave);
        }
        catch
        {
            pUsuario = null;
        }
        // Traer datos de la imagen
        lstConsulta = ahorroServicio.Handler(vImagenes, pUsuario);
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