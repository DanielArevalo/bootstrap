using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WSChatBot : System.Web.Services.WebService
    {
        [WebMethod(Description = "Determinar los datos de la conexión")]
        public string ProbarConexion(string entidadBD)
        {
            if (string.IsNullOrWhiteSpace(entidadBD)) entidadBD = "DataBase";

            // Definición de entidades y servicios
            Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
            // Realizar conexión a la base de datos
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            string error = "";
            usuario = conexion.DeterminarUsuarioSinClave(ref error, entidadBD);
            if (usuario == null)
            {
                usuario = new Xpinn.Util.Usuario();
                string aux = usuarioServicio.ProbarConexión(usuario);
                return "No pudo determinar el usuario => Conexión:" + entidadBD + " String:" + aux + " Usuario:" + conexion.DeterminarNombreUsuario(entidadBD) + " Error:" + error;
            }
            // Validar usuario y obtener accesos
            if (usuarioServicio.ProbarConexión(usuario) == null)
                return "No pudo realizar conexión => Conexión:" + entidadBD + " Usuario:" + conexion.DeterminarNombreUsuario(entidadBD) + " Clave:" + usuarioServicio.ProbarConexión(usuario);
            usuario.clave = usuario.clave_sinencriptar;
            usuario = usuarioServicio.ValidarUsuario(usuario.identificacion, usuario.clave, "", "", usuario);
            if (usuario == null)
            {
                return "No pudo identificar el usuario => Conexión:" + entidadBD + " Usuario:" + conexion.DeterminarNombreUsuario(entidadBD) + " Estado:" + usuarioServicio.ProbarConexión(usuario);
            }
            // Devolver listado de productos
            return "Usuario: " + usuario.nombre;
        }

        [WebMethod(Description = "OBTENCION DE DATOS EMPRESA")]
        public Xpinn.Seguridad.Entities.Empresa DatosEmpresa(string entidadBD)
        {
            if (string.IsNullOrWhiteSpace(entidadBD)) entidadBD = "DataBase";
            Conexion conexion = new Conexion();
            string error = "";
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(ref error, entidadBD);
            if (usuario == null)
                return null;

            Xpinn.Seguridad.Services.EmpresaService empresaService = new Xpinn.Seguridad.Services.EmpresaService();
            Xpinn.Seguridad.Entities.Empresa EmpresaDatos = empresaService.ConsultarEmpresa(1, usuario);
            return EmpresaDatos;
        }
    }
}
