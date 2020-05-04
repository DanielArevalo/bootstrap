using System.Configuration;
using Xpinn.Seguridad.Business;
using Xpinn.Util;

namespace Xpinn.Synchronization
{
    public class Conexion
    {
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

        public Xpinn.Util.Usuario DeterminarUsuarioSinClave(string pEntidad = null)
        {
            try
            {
                // Determinar parámetros de conexión del webconfig
                string connectionString = ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString;
                string[] sParametros = new string[3] { "", "", "" };
                sParametros = connectionString.Split(';');
                string[] sTexto = new string[3] { "", "", "" };
                sTexto = sParametros[1].Split('=');
                string sUsuario = sTexto[1];
                string pUsuBD = string.Empty;
                if (pEntidad != null)
                {
                    if (pEntidad.ToUpper() == "COOTREGUA")
                        pUsuBD = "XPINNADM";
                }
                else
                {
                    if (sUsuario.ToUpper() == "FECEM" || sUsuario.ToUpper() == "COOPECOL" || sUsuario.ToUpper() == "PRUEBAS")
                        pUsuBD = "XPINNADM";
                }

                sTexto = sParametros[2].Split('=');
                string sClave = sTexto[1];
                // Definición de entidades y servicios
                Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
                Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
                // Validar usuario y obtener accesos
                usuario.identificacion = sUsuario;
                usuario.clave_sinencriptar = sClave;
                pUsuBD = !string.IsNullOrEmpty(pUsuBD) ? pUsuBD : sUsuario;
                usuario = usuarioServicio.ValidarUsuarioSinClave(pUsuBD, "", usuario);

                return usuario;
            }
            catch
            {
                return null;
            }
        }

    }
}