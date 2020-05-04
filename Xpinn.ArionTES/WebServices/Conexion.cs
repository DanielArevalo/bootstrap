using System;
using System.Configuration;
using Xpinn.Util;

namespace WebServices
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
                string sClave = sTexto[2];
                // Definición de entidades y servicios
                Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
                Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
                // Validar usuario y obtener accesos
                usuario.identificacion = sUsuario;
                usuario.clave_sinencriptar = sClave;
                usuario = usuarioServicio.ValidarUsuario(usuario.identificacion, sClave, "", "", usuario);

                usuario.conexionBD = pName;

                return usuario;
            }
            catch
            {
                return null;
            }
        }

        public string DeterminarNombreUsuario(string pEntidad = "DataBase")
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
                    if (pEntidad.ToUpper() == ClienteEnt.COOTREGUA.ToString() || pEntidad.ToUpper() == ClienteEnt.FONSODI.ToString())
                        pUsuBD = "XPINNADM";
                }
                else
                {
                    if (sUsuario.ToUpper() == ClienteEnt.FECEM.ToString() || sUsuario.ToUpper() == ClienteEnt.COOPECOL.ToString()
                        || sUsuario.ToUpper() == ClienteEnt.PRUEBAS.ToString() || sUsuario.ToUpper() == ClienteEnt.FONSODI.ToString()
                        || sUsuario.ToUpper() == ClienteEnt.FONDECOLP.ToString())
                    {
                        pUsuBD = "XPINNADM";
                    }
                    else
                    {
                        pUsuBD = "XPINNADM";
                    }
                }

                sTexto = sParametros[2].Split('=');
                string sClave = sTexto[1];
                // Definición de entidades y servicios
                Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
                Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
                // recuperando usuario por default
                string UsuarioDefault = ConfigurationManager.AppSettings["usuarioDefault"] != null ?
                    ConfigurationManager.AppSettings["usuarioDefault"].ToString() : null;
                // Validar usuario y obtener accesos
                usuario.identificacion = sUsuario;
                usuario.clave_sinencriptar = sClave;
                if (string.IsNullOrEmpty(UsuarioDefault))
                    pUsuBD = !string.IsNullOrEmpty(pUsuBD) ? pUsuBD : sUsuario.ToUpper();
                else
                    pUsuBD = UsuarioDefault;

                return usuario.identificacion + " " + usuario.clave_sinencriptar;
            }
            catch
            {
                return null;
            }
        }

        public Xpinn.Util.Usuario DeterminarUsuarioSinClave(string pEntidad = null)
        {
            string error = "";
            return DeterminarUsuarioSinClave(ref error, pEntidad);
        }

        public Xpinn.Util.Usuario DeterminarUsuarioSinClave(ref string error, string pEntidad = null)
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
                    if (pEntidad.ToUpper() == ClienteEnt.COOTREGUA.ToString() || pEntidad.ToUpper() == ClienteEnt.FONSODI.ToString())
                        pUsuBD = "XPINNADM";
                }
                else
                {
                    if (sUsuario.ToUpper() == ClienteEnt.FECEM.ToString() || sUsuario.ToUpper() == ClienteEnt.COOPECOL.ToString()
                        || sUsuario.ToUpper() == ClienteEnt.PRUEBAS.ToString() || sUsuario.ToUpper() == ClienteEnt.FONSODI.ToString()
                        || sUsuario.ToUpper() == ClienteEnt.FONDECOLP.ToString())
                    {
                        pUsuBD = "XPINNADM";
                    }
                    else
                    {
                        pUsuBD = "XPINNADM";
                    }
                }

                sTexto = sParametros[2].Split('=');
                string sClave = sTexto[1];
                // Definición de entidades y servicios
                Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
                Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
                // recuperando usuario por default
                string UsuarioDefault = ConfigurationManager.AppSettings["usuarioDefault"] != null ?
                    ConfigurationManager.AppSettings["usuarioDefault"].ToString() : null;
                // Validar usuario y obtener accesos
                usuario.identificacion = sUsuario;
                usuario.clave_sinencriptar = sClave;
                if (string.IsNullOrEmpty(UsuarioDefault))
                    pUsuBD = !string.IsNullOrEmpty(pUsuBD) ? pUsuBD : sUsuario.ToUpper();
                else
                    pUsuBD = UsuarioDefault;
                usuario = usuarioServicio.ValidarUsuarioSinClave(pUsuBD, "", ref error, usuario);
                // Se asigna la clave porque al validar usuario la limpia
                usuario.clave_sinencriptar = sClave;
                return usuario;
            }
            catch (Exception ex)
            {
                error += ex.Message;
                return null;
            }
        }

        public Xpinn.Util.Usuario DeterminarUsuarioOficina(string sec)
        {
            string _err = "";
            return DeterminarUsuarioOficina(sec, ref _err);
        }

        public Xpinn.Util.Usuario DeterminarUsuarioOficina(string sec, ref string error)
        {
            error = "";
            try
            {
                /////////////////////////////////////////////////////////////////////////////////////////////
                /// Obtener datos del USUARIO y CLAVE del webconfig
                /////////////////////////////////////////////////////////////////////////////////////////////
                //Obtiene cadena de conexión
                string connectionString = ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString;
                //Separa la cadena de conexión
                string[] sParametros = new string[3] { "", "", "" };
                //Obtiene segmento de conexión a bese de datos
                sParametros = connectionString.Split(';');
                //Separa datos de acceso usuario y contraseña bd
                string[] sTexto = new string[3] { "", "", "" };
                //ASIGNA USUARIO DE BASE DE DATOS
                sTexto = sParametros[1].Split('=');
                string sUsuario = sTexto[1];
                //ASIGNA CLAVE DE LA BASE DE DATOS
                sTexto = sParametros[2].Split('=');
                string sClave = sTexto[1];               
                // CREA EL OBJETO DE USUARIO DE BASE DE DATOS
                Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
                usuario.identificacion = sUsuario;
                usuario.clave_sinencriptar = sClave;                
                //Asigna a esta variable el usuario de base de datos para validar acceso de seguridad
                string pUsuBD = sUsuario;

                /////////////////////////////////////////////////////////////////////////////////////////////
                /// Obtener datos del USUARIO y CLAVE del token para poder compararlos
                /////////////////////////////////////////////////////////////////////////////////////////////
                //desencripta token
                CifradoBusiness cifrado = new CifradoBusiness();
                sec = cifrado.Desencriptar(sec);
                //Descompone token
                string[] sSecurity = new string[3] { "", "", "" };
                sSecurity = sec.Split(';');
                string pClaveBD = sSecurity[2];

                /////////////////////////////////////////////////////////////////////////////////////////////
                //VALIDA DATOS SIMETRICOS
                /////////////////////////////////////////////////////////////////////////////////////////////
                if (sSecurity[1] == sUsuario && sSecurity[2] == sClave)
                {
                    //CAMBIA EL USUARIO POR EL REGISTRADO EN TABLA USUARIOS PARA OFICINA VIRTUAL
                    pUsuBD = "XPINNOFFICE";
                    //Define service para validaciones internas
                    Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
                    usuario = usuarioServicio.ValidarUsuarioOficina(pUsuBD, pClaveBD, sSecurity[0], usuario);
                    if (usuario == null)
                    {
                        error = "no de pudo determinar el usuario";
                    }
                    return usuario;
                }
                else
                {
                    error = "usuario y/o clave no corresponden";
                    return null;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        public string TestDeterminarUsuarioOficina(string sec)
        {
            string salida = "";
            try
            {
                //Obtiene cadena de conexión
                string connectionString = ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString;
                //Separa la cadena de conexión
                string[] sParametros = new string[3] { "", "", "" };
                //Obtiene segmento de conexión a bese de datos
                sParametros = connectionString.Split(';');
                //Separa datos de acceso usuario y contraseña bd
                string[] sTexto = new string[3] { "", "", "" };
                //ASIGNA USUARIO DE BASE DE DATOS
                sTexto = sParametros[1].Split('=');
                string sUsuario = sTexto[1];
                //ASIGNA CLAVE DE LA BASE DE DATOS
                sTexto = sParametros[2].Split('=');
                string sClave = sTexto[1];

                // CREA EL OBJETO DE USUARIO DE BASE DE DATOS
                Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
                usuario.identificacion = sUsuario;
                usuario.clave_sinencriptar = sClave;

                //Asigna a esta variable el usuario de base de datos para validar acceso de seguridad
                string pUsuBD = sUsuario;

                //desencripta token
                CifradoBusiness cifrado = new CifradoBusiness();
                sec = cifrado.Desencriptar(sec);

                //Descompone token
                string[] sSecurity = new string[3] { "", "", "" };
                sSecurity = sec.Split(';');
                string pClaveBD = sSecurity[2];

                //VALIDA DATOS SIMETRICOS
                salida += ";" + sSecurity[1] + ";" + sUsuario + ";" + sSecurity[2] + ";" + sClave+";"+ sSecurity[0] + ";";                
                if (sSecurity[1] == sUsuario && sSecurity[2] == sClave)
                {
                    //CAMBIA EL USUARIO POR EL REGISTRADO EN TABLA USUARIOS PARA OFICINA VIRTUAL
                    pUsuBD = "XPINNOFFICE";
                    salida += pUsuBD + ";";

                    //Define service para validaciones internas
                    Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
                    usuario = usuarioServicio.ValidarUsuarioOficina(pUsuBD, pClaveBD, sSecurity[0], usuario);
                    if (usuario != null)
                        salida += usuario.codusuario + ";" + usuario.nombre + ";";
                    else
                        salida += "sin usuario;";
                    return salida;
                }
                else
                {
                    salida += "desiguales;";
                    return salida;
                }
            }
            catch (Exception ex)
            {
                //salida = ex.Message;
                return salida + ";ex: " + ex.Message;
            }
        }

    }
}