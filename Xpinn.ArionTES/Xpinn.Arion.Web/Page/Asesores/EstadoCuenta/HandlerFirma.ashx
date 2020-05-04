<%@ WebHandler Language="C#" Class="HandlerFirma" %>

using System;
using System.Web;
using Xpinn.Util;
using System.Configuration;
using System.Collections.Generic;

public class HandlerFirma : IHttpHandler {

    private Xpinn.Ahorros.Services.AhorroVistaServices ahorroServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();

    public void ProcessRequest (HttpContext context)
    {
        // Determinar datos de la persona
        Int64 numero_cuenta = Convert.ToInt64(context.Request.QueryString["id"]);
        Int64 idimagen = 0;

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

        // Cargar datos de la imagen
        byte[] imagen = ahorroServicio.ImagenTarjeta(numero_cuenta, ref idimagen, pUsuario);
        if (imagen != null)
        {
            context.Response.ContentType = "jpg";
            context.Response.BinaryWrite((byte[])imagen);
        }

    }

    public bool IsReusable {
        get {
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