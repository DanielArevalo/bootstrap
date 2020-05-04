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
    private Xpinn.FabricaCreditos.Services.DocumentosAnexosService DocumentosAnexosServicio = new Xpinn.FabricaCreditos.Services.DocumentosAnexosService();


    public void ProcessRequest(HttpContext context)
    {
        List<Xpinn.FabricaCreditos.Entities.DocumentosAnexos> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.DocumentosAnexos>();        
        Xpinn.FabricaCreditos.Entities.DocumentosAnexos vDocumentosAnexos = new Xpinn.FabricaCreditos.Entities.DocumentosAnexos();
        
        vDocumentosAnexos.iddocumento = Convert.ToInt32(context.Request.QueryString["id"]);

        Usuario pUsuario = new Usuario();

        lstConsulta = DocumentosAnexosServicio.Handler(vDocumentosAnexos, pUsuario);       
        
        context.Response.ContentType = "png";
        context.Response.BinaryWrite((byte[])lstConsulta[0].imagen);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    //private Xpinn.FabricaCreditos.Entities.DocumentosAnexos ObtenerValores()
    //{
    //    Xpinn.FabricaCreditos.Entities.DocumentosAnexos vDocumentosAnexos = new Xpinn.FabricaCreditos.Entities.DocumentosAnexos();
    //    vDocumentosAnexos.iddocumento = Convert.ToInt32(context.Request.QueryString["id"]);
    //    return vDocumentosAnexos;
    //}

}