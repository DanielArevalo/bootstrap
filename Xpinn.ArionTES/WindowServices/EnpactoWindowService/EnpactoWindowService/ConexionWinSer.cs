using System;
using System.Configuration;
using Xpinn.Util;
using Xpinn.Seguridad.Services;

namespace EnpactoWindowService
{
    public class ConexionWinSer
    {
   
        public Xpinn.Util.Usuario DeterminarUsuario(ref string pError, string pName = "DataBase")
        {
            try
            {
                // Determinar parámetros de conexión del webconfig
                //string connectionString = System.Configuration.ConfigurationSettings.AppSettings[pName].ToString();
                //string[] sParametros = new string[3] { "", "", "" };
                //sParametros = connectionString.Split(';');
                //string[] sTexto = new string[3] { "", "", "" };
                //sTexto = sParametros[1].Split('=');
                //string sUsuario = sTexto[1];
                //sTexto = sParametros[2].Split('=');
                //string sClave = sTexto[1];
                // Definición de entidades y servicios
                Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
                Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
                // Validar usuario y obtener accesos. COOTRAEMCALI
                //usuario.identificacion = "XPINNADM";
                //usuario.clave_sinencriptar = "Lingalit";
                // Validar usuario y obtener accesos. OTRAS ENTIDADES.
                usuario.identificacion = "TARJETAD";
                usuario.clave_sinencriptar = "enpacto2018"; 
                usuario = usuarioServicio.ValidarUsuario(usuario.identificacion, usuario.clave_sinencriptar, "","", usuario);

                usuario.conexionBD = pName;

                return usuario;
            }
            catch (Exception ex)
            {
                pError = pError + " " + ex.Message;
                return null;
            }
        }       

    }
}