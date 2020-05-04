<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Collections.Generic;
using System.Web;
using Xpinn.Util;
using System.Configuration;

public class Handler : IHttpHandler
{
    private Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();


    public void ProcessRequest(HttpContext context)
    {
        // Determinar código de la imagen a mostrar
        List<Xpinn.FabricaCreditos.Entities.Imagenes> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Imagenes>();
        Xpinn.FabricaCreditos.Entities.Imagenes vImagenes = new Xpinn.FabricaCreditos.Entities.Imagenes();
        vImagenes.idimagen = Convert.ToInt64(context.Request.QueryString["id"]);
        // Determinar el usuario
        CifradoBusiness cifrar = new CifradoBusiness();
        Usuario pUsuario = new Usuario();
        try
        {
            pUsuario.identificacion = Convert.ToString(context.Request.QueryString["Us"]);
            pUsuario.clave_sinencriptar = Convert.ToString(context.Request.QueryString["Pw"]);
            pUsuario.clave = cifrar.Encriptar(pUsuario.clave_sinencriptar);
        }
        catch
        {
            pUsuario = null;
        }
        try
        {
            pUsuario.clave_sinencriptar = cifrar.Desencriptar(pUsuario.clave);
        }
        catch
        {
            pUsuario = DeterminarUsuario("DataBase");
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

    public Xpinn.Util.Usuario DeterminarUsuario(string pName = "DataBase")
    {
        try
        {
            // Determinar parámetros de conexión del webconfig
            string connectionString = ConfigurationManager.ConnectionStrings[pName].ConnectionString;
            string[] sParametros = new string[3] { "", "", "" };
            sParametros = connectionString.Split(';');
            string[] sTexto = new string[3] { "", "", "" };
            sTexto = sParametros[1].Split('=');
            string sUsuario = sTexto[1];
            sTexto = sParametros[2].Split('=');
            string sClave = sTexto[1];
            // Definición de entidades y servicios
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
            // Validar usuario y obtener accesos
            usuario.identificacion = sUsuario;
            usuario.clave_sinencriptar = sClave;
            usuario = usuarioServicio.ValidarUsuario(usuario.identificacion, sClave, "","", usuario);

            usuario.conexionBD = pName;

            return usuario;
        }
        catch
        {
            return null;
        }
    }

}