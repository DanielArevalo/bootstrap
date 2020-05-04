using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using Xpinn.Util;
using Xpinn.Seguridad.Entities;

namespace WebServices
{
    /// <summary>
    /// Descripción breve de WSlogin
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class WSlogin : System.Web.Services.WebService
    {
        //public SecuredToken SHSecurity;

        [WebMethod]
        public bool Login(string pIdentificacion, string pClave)
        {            
            // Determinando datos para conexión a base de datos.
            string connectionString = ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString;
            string[] sParametros = new string[3] { "", "", "" };
            sParametros = connectionString.Split(';');
            string[] sTexto = new string[3] { "", "", "" };
            sTexto = sParametros[1].Split('=');
            string sUsuario = sTexto[1];
            sTexto = sParametros[2].Split('=');
            string sClave = sTexto[1];
            // Definición de entidades y servicios
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            Xpinn.Seguridad.Services.AccesoService accesoServicio = new Xpinn.Seguridad.Services.AccesoService();
            Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
            List<Xpinn.Seguridad.Entities.Acceso> lstAccesos = new List<Xpinn.Seguridad.Entities.Acceso>();
            // Validar usuario y obtener accesos
            usuario.identificacion = sUsuario;
            usuario.clave_sinencriptar = sClave;
            usuario = usuarioServicio.ValidarUsuario(usuario.identificacion, usuario.clave_sinencriptar, "", "", usuario);
            lstAccesos = accesoServicio.ListarAcceso(usuario.codperfil, usuario,"");
            // Validar la conexión del usuario
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            bool rta =  personaServicio.ConsultaClavePersona(pIdentificacion, pClave, usuario);
            return rta;
        }
        /*
        [WebMethod, TraceExtension(Filename = "F:\\WebServiceTrace.Log")]
        [System.Web.Services.Protocols.SoapHeader("SHSecurity")]

        //[System.Web.Services.Protocols.SoapHeader("SHSecurity", Direction = System.Web.Services.Protocols.SoapHeaderDirection.InOut)]
        public string SayHello(string Message)
        {
            string rtnValue = string.Empty;
            if (SHSecurity == null)
                return "Por favor proporcionar el usuario y la contraseña";

            if (string.IsNullOrEmpty(SHSecurity.UserIdent) || string.IsNullOrEmpty(SHSecurity.Password))
                return "Por favor proporcionar el usuario y la contraseña";

            if (!SHSecurity.IsUserCredentialsValid(SHSecurity))
                return "Por favor llame el método de autentificación primero.";

            return rtnValue;
        }
        
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SHSecurity")]
        public string AuthenticationUser()
        {
            if (SHSecurity == null)
                return "Por favor proporcionar el usuario y la contraseña";

            if (string.IsNullOrEmpty(SHSecurity.UserIdent) || string.IsNullOrEmpty(SHSecurity.Password))
                return "Por favor proporcionar el usuario y la contraseña";
            
            if (!SHSecurity.IsUserCredentialsValid(SHSecurity.UserIdent, SHSecurity.Password, SHSecurity.Tipo, SHSecurity.Entidad))
                return "Credenciales de usuario incorrecto";

            string token = Guid.NewGuid().ToString();
            HttpRuntime.Cache.Add(
                token, SHSecurity.UserIdent,
                null,
                System.Web.Caching.Cache.NoAbsoluteExpiration,
                TimeSpan.FromMinutes(15),
                System.Web.Caching.CacheItemPriority.NotRemovable,
                null);

            return token;
        }
        */

        [WebMethod]
        public bool Logeo(string pEntidad, string pIdentificacion, string pClave, int pTipoUsuario)
        {            
            if (string.IsNullOrEmpty(pIdentificacion) || string.IsNullOrEmpty(pClave) || string.IsNullOrEmpty(pEntidad))
                return false;

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(pEntidad);
            if (usuario == null)
                return false;

            if (pTipoUsuario != 2 && pTipoUsuario != 1)
                return false;
            Xpinn.FabricaCreditos.Entities.Persona1 pPersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
            try
            {
                if (pTipoUsuario == 2)
                {
                    pPersona = usuarioServicio.ValidarPersonaUsuario(pIdentificacion, pClave, usuario);
                    if (pPersona != null)
                    {
                        return pPersona.cod_persona > 0 && pPersona.identificacion != null ? true : false;
                    }
                    else
                        return false;
                }
                else
                {
                    usuario = usuarioServicio.ValidarUsuario(pIdentificacion, pClave, "", "", usuario);
                    if (usuario != null)
                    {
                        return usuario.codusuario > 0 && usuario.identificacion != null ? true : false;
                    }
                    else
                        return false;
                }
            }
            catch
            {
                return false;
            }
            //string pUsuario = "User:" + HttpRuntime.Cache[SoapHeader.AuthenticationToken];
            //return true;
        }

        [WebMethod]
        public Xpinn.FabricaCreditos.Entities.Persona1 LoginApp(string pIdentificacion, string pClave, string pDireccionIp, string sec)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString;

            // Definición de entidades y servicios            
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec); 
            if (usuario == null)
                return null;

            Xpinn.FabricaCreditos.Entities.Persona1 PersonaUsuario = new Xpinn.FabricaCreditos.Entities.Persona1();
            Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();

            // Validar usuario y obtener accesos
            Xpinn.Util.Usuario pUsuario = new Xpinn.Util.Usuario();
            pUsuario.identificacion = pIdentificacion;
            pUsuario.clave_sinencriptar = pClave;
            PersonaUsuario = usuarioServicio.ValidarPersonaUsuario(pUsuario.identificacion, pUsuario.clave_sinencriptar, usuario);
            string direccionIP = "";
            direccionIP = pDireccionIp;

            if(PersonaUsuario.acceso_oficina == 1)
            {
                //VALIDA_ACTUALIZACION_DATOS
                string fecha = ConfigurationManager.AppSettings["fecha_actualizacion"] as string;
                if (!string.IsNullOrEmpty(fecha))
                {
                    PersonaUsuario.Observaciones = usuarioServicio.ValidarActualizacion(PersonaUsuario.cod_persona, fecha,usuario) ? null : "PENDIENTE DE ACTUALIZACION";
                }
                else
                {
                    PersonaUsuario.Observaciones = null;
                }

                //INSERTANDO EL HISTORIAL DE INGRESO
                if (PersonaUsuario.rptaingreso == true && PersonaUsuario.identificacion != null && PersonaUsuario.cod_persona != 0)
                {
                    Xpinn.Seguridad.Entities.Ingresos pIngreso = new Xpinn.Seguridad.Entities.Ingresos();
                    pIngreso.cod_ingreso = 0;
                    pIngreso.fecha_horaingreso = DateTime.Now;
                    pIngreso.fecha_horasalida = DateTime.MinValue;
                    pIngreso.direccionip = direccionIP;
                    pIngreso.cod_persona = Convert.ToInt64(PersonaUsuario.cod_persona);
                    pIngreso = usuarioServicio.CrearUsuarioIngreso(pIngreso, usuario);
                    PersonaUsuario.codigo = Convert.ToInt64(pIngreso.cod_ingreso);
                }
            }
            return PersonaUsuario;
        }

        [WebMethod]
        public Boolean ModificarUsuarioIngreso(Int32? pCod_ingreso, string sec)
        {
            Xpinn.Seguridad.Services.UsuarioService serviceUsuario = new Xpinn.Seguridad.Services.UsuarioService();
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return false;
            if (pCod_ingreso != null && pCod_ingreso != 0)
            {
                Xpinn.Seguridad.Entities.Ingresos pIngresos = new Xpinn.Seguridad.Entities.Ingresos();
                pIngresos.cod_ingreso = Convert.ToInt32(pCod_ingreso);
                pIngresos.fecha_horasalida = DateTime.Now;
                try
                {
                    serviceUsuario.ModificarUsuarioIngreso(pIngresos, (Usuario)Session["usuario"]);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;
        }        


        [WebMethod]
        public List<Xpinn.Seguridad.Entities.Acceso> ListarAccesoAAC(Int64 pIdModulo, string pIdentificacion, string pClave, string sec)
        {
            Xpinn.Seguridad.Services.AccesoService BOAcceso = new Xpinn.Seguridad.Services.AccesoService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            return BOAcceso.ListarAccesoAAC(pIdModulo, usuario);
        }


        [WebMethod]
        public List<Xpinn.Seguridad.Entities.Contenido> ListarContenido(Xpinn.Seguridad.Entities.Contenido pContenido, string sec)
        {
            Xpinn.Seguridad.Services.ContenidoService BOContenido = new Xpinn.Seguridad.Services.ContenidoService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            return BOContenido.ListarContenido(pContenido, usuario);
        }

        [WebMethod]
        public string testService(string sec)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString;
                // Definición de entidades y servicios            
                Conexion conexion = new Conexion();
                return conexion.TestDeterminarUsuarioOficina(sec);                
            }
            catch(Exception ex)
            {
                return "Error en servidor: " + ex;
            }
        }


        [WebMethod]
        public Xpinn.Seguridad.Entities.Perfil consultarPerfil(string sec)
        {
            Xpinn.Seguridad.Services.PerfilService PerfilServicio = new Xpinn.Seguridad.Services.PerfilService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            return PerfilServicio.ConsultarPerfil(usuario.codperfil, usuario);
        }

        [WebMethod]
        public List<Acceso> ListarAccesosApp(string sec)
        {
            CifradoBusiness cifrar = new CifradoBusiness();
            string clave = cifrar.Desencriptar("B3u85n7Si/VBaoQEElC+bd1elaoH/2m8O/gmeGI3p5w=");
//            sec = cifrar.Encriptar("sec");

            Xpinn.Seguridad.Services.AccesoService BOAcceso = new Xpinn.Seguridad.Services.AccesoService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            return BOAcceso.ListarAccesoApp(usuario);
        }
    }
}
