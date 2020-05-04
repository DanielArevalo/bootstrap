using System;
using System.Web;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;

namespace WebServices
{
    public class SecuredToken : System.Web.Services.Protocols.SoapHeader
    {
        public string UserIdent { get; set; }
        public string Password { get; set; }
        public int Tipo { get; set; }
        public string Entidad { get; set; }
        public string AuthenticationToken { get; set; }

        public bool IsUserCredentialsValid(string UserIdent, string Password, int Tipo, string Entidad = null)
        {
            if (Entidad != null)
            {
                bool isDefined = Enum.IsDefined(typeof(ClienteEnt), Entidad.ToUpper());
                if (!isDefined)
                    return false;
            }

            // TIPO 1 => USUARIOS
            // TIPO 2 => PERSONA ACCESO (ASOCIADOS)
            if (Tipo != 1 && Tipo != 2)
                return false;

            Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(Entidad);
            if (usuario == null)
                return false;

            try
            {
                if (Tipo == 1)
                {
                    usuario = usuarioServicio.ValidarUsuario(UserIdent, Password, "", "", usuario);
                    if (usuario != null)
                        return usuario.codusuario > 0 && usuario.identificacion != null ? true : false;
                    else
                        return false;
                }
                else
                {
                    Persona1 pPersona = usuarioServicio.ValidarPersonaUsuario(UserIdent, Password, usuario);
                    if (pPersona != null)
                        return pPersona.cod_persona > 0 && pPersona.identificacion != null ? true : false;
                    else
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }


        public bool IsUserCredentialsValid(SecuredToken SoapHeader)
        {
            if (SoapHeader == null)
                return false;

            if (!string.IsNullOrEmpty(SoapHeader.AuthenticationToken))
                return (HttpRuntime.Cache[SoapHeader.AuthenticationToken] != null);

            return false;
        }

    }
}