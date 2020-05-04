<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using Xpinn.Util;
using System.Collections.Generic;

public class Handler : IHttpHandler {

    private Xpinn.Aportes.Services.ImagenesService ImagenServicio = new Xpinn.Aportes.Services.ImagenesService();

    public void ProcessRequest (HttpContext context) {

        try
        {



            Int64 codPersona = Convert.ToInt64(context.Request.QueryString["id"]);
            Int64 dedo = 1;
            Int64 idimagen = 0;
            Usuario pUsuario = new Usuario();
            Xpinn.FabricaCreditos.Entities.Imagenes imagen=new Xpinn.FabricaCreditos.Entities.Imagenes();
            imagen = ImagenServicio.DocumentosAnexos(codPersona, ref idimagen, pUsuario);
            if (imagen.imagenEsPDF != true)
            {
                context.Response.ContentType = "jpg";
                context.Response.BinaryWrite((byte[])imagen.imagen);
            }
            else { 
            context.Response.Buffer = true;
            context.Response.Charset = "";

            context.Response.AppendHeader("Content-Disposition","inline;filename=Imprimir.pdf");

            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.ContentType = "application/pdf";
            context.Response.BinaryWrite(imagen.imagen);
            context.Response.Flush();
            context.Response.End();
                    }
        }
        catch (Exception ex)
        {

            throw;
        }

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}