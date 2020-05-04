<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Collections.Generic;
using System.Web;
using Xpinn.Util;

public class Handler : IHttpHandler
{
    private Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();


    public void ProcessRequest(HttpContext context)
    {
        // Determinar código de la imagen a mostrar
        List<Xpinn.FabricaCreditos.Entities.Imagenes> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Imagenes>();
        Xpinn.FabricaCreditos.Entities.Imagenes vImagenes = new Xpinn.FabricaCreditos.Entities.Imagenes();
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