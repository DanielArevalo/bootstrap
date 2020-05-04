using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;


namespace Xpinn.Util
{
    public class ConnectionDataBase
    {
        public DbExpinnProviderFactory dbProveedorFactory;
        public static Func<IAuditableServices> ObtenerInstanciaAuditoria;
        String _connectionString;
        String _proveedor;
        DbConnection _conexionFactory;
        ExcepcionBusiness _BOExcepcion;
        IAuditableServices _auditableService;
        Usuario _usuario;
        bool _seAudita;

        public ConnectionDataBase()
        {
            try
            {
                _BOExcepcion = new ExcepcionBusiness();
                _connectionString = ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString;
                _proveedor = ConfigurationManager.ConnectionStrings["DataBase"].ProviderName;

                try
                {
                    string hayAuditoria = ConfigurationManager.AppSettings["SeAuditaLaEjecucionDeLosSP"];
                    _seAudita = hayAuditoria == "1";
                }
                catch (Exception)
                {
                    // No se quiere que explote todo el app si no se consigue ese parametro
                }
            }
            catch (Exception ex)
            {
                _BOExcepcion.Throw("ConnectionDataBase", "ConnectionDB", ex);
            }
        }

        public ConnectionDataBase(Usuario pUsuario, bool seAudita = true)
        {
            try
            {
                _BOExcepcion = new ExcepcionBusiness();
                CadenaConexion(pUsuario);

                bool seAuditaEnEstaConexion = _seAudita && seAudita && ObtenerInstanciaAuditoria != null;
                DbExpinnProviderFactory provider = new DbExpinnProviderFactory(DbProviderFactories.GetFactory(_proveedor), seAuditaEnEstaConexion);

                if (seAuditaEnEstaConexion)
                {
                    provider.SpEjecutado += Provider_SpEjecutado;
                }

                dbProveedorFactory = provider;

                _usuario = pUsuario;

                if (ObtenerInstanciaAuditoria != null)
                {
                    _auditableService = ObtenerInstanciaAuditoria();
                }

                _conexionFactory = dbProveedorFactory.CreateConnection();
                _conexionFactory.ConnectionString = _connectionString;
            }
            catch (Exception ex)
            {
                _BOExcepcion.Throw("ConnectionDataBase", "ConnectionDB", ex);
            }
        }

        public DbConnection ObtenerConexion(bool seAudita = true)
        {
            try
            {
                try
                {
                    bool seAuditaEnEstaConexion = _seAudita && seAudita && ObtenerInstanciaAuditoria != null;
                    DbExpinnProviderFactory provider = new DbExpinnProviderFactory(DbProviderFactories.GetFactory(_proveedor), seAuditaEnEstaConexion);

                    if (seAuditaEnEstaConexion)
                    {
                        provider.SpEjecutado += Provider_SpEjecutado;
                    }

                    dbProveedorFactory = provider;

                    if (ObtenerInstanciaAuditoria != null)
                    {
                        _auditableService = ObtenerInstanciaAuditoria();
                    }

                    _conexionFactory = dbProveedorFactory.CreateConnection();
                    _conexionFactory.ConnectionString = _connectionString;

                    return _conexionFactory;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error de conexion a la Base de Datos. " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                _BOExcepcion.Throw("ConnectionDataBase", "ObtenerConexion", ex);
                return null;
            }
        }

        public DbConnection ObtenerConexion(Usuario pUsuario, bool seAudita = true)
        {
            try
            {
                try
                {
                    CadenaConexion(pUsuario);

                    bool seAuditaEnEstaConexion = _seAudita && seAudita && ObtenerInstanciaAuditoria != null;
                    DbExpinnProviderFactory provider = new DbExpinnProviderFactory(DbProviderFactories.GetFactory(_proveedor), seAuditaEnEstaConexion);

                    if (seAuditaEnEstaConexion)
                    {
                        provider.SpEjecutado += Provider_SpEjecutado;
                    }

                    dbProveedorFactory = provider;

                    _usuario = pUsuario;

                    if (ObtenerInstanciaAuditoria != null)
                    {
                        _auditableService = ObtenerInstanciaAuditoria();
                    }

                    _conexionFactory = dbProveedorFactory.CreateConnection();
                    _conexionFactory.ConnectionString = _connectionString;

                    return _conexionFactory;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error de conexion a la Base de Datos. " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                _BOExcepcion.Throw("ConnectionDataBase", "ObtenerConexion", ex);
                return null;
            }
        }

        public void CadenaConexion(Usuario eUsuario)
        {
            // Determinar parámetros de conexión del WEBCONFIG 

            if (!string.IsNullOrWhiteSpace(eUsuario.conexionBD))
            {
                _connectionString = ConfigurationManager.ConnectionStrings[eUsuario.conexionBD].ConnectionString;
            }
            else
            {
                _connectionString = ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString;
            }

            string[] sParametros = new string[3] { "", "", "" };
            sParametros = _connectionString.Split(';');
            string[] sTexto = new string[3] { "", "", "" };
            sTexto = sParametros[1].Split('=');
            string sUsuario = sTexto[1];
            sTexto = sParametros[2].Split('=');
            string sClave = sTexto[1];

            if (!string.IsNullOrWhiteSpace(eUsuario.conexionBD))
            {
                _proveedor = ConfigurationManager.ConnectionStrings[eUsuario.conexionBD].ProviderName;
            }
            else
            {
                _proveedor = ConfigurationManager.ConnectionStrings["DataBase"].ProviderName;
            }


            // Determinar datos del usuario
            string pUsuario = "";
            string pClave = "";
            if (eUsuario == null || eUsuario.identificacion == null || eUsuario.clave_sinencriptar == null)
            {
                pUsuario = sUsuario;
                pClave = sClave;
            }
            else
            {
                pUsuario = eUsuario.identificacion;
                pClave = eUsuario.clave_sinencriptar;
            }

            // Determinar si se manejan usuarios
            string manejoUsuarios = ConfigurationManager.AppSettings["ManejoUsuarios"];
            if (manejoUsuarios != "1")
                return;

            // Detrminar si contiene el usuario. Cuanto tengo que conectarme a la base de datos con el usuario que se este logueando. 
            if (_connectionString.Contains("User") == false)
                _connectionString = _connectionString + "User Id=" + pUsuario + ";";
            if (_connectionString.Contains("Password") == false)
                _connectionString = _connectionString + "Password=" + pClave + ";";

            if (sParametros[0].Substring(sParametros[0].Length - 1, 1) != ";")
                sParametros[0] = sParametros[0] + ";";
            // Todos los usuarios en la base de datos llevan una U al inicio porque oracle obliga a que inicien con letras. De esto se excluyen los usuarios de administrador
            // el de tarjeta débito, biometría y atención al cliente.
            if (pUsuario.ToLower() != "xpinnadm" && pUsuario.ToLower() != "tarjetad" && pUsuario.ToLower() != "biometria" && pUsuario.ToLower() != "atencion")
                pUsuario = "U" + pUsuario;
            _connectionString = sParametros[0] + "User Id=" + pUsuario + ";" + "Password=" + pClave + ";";

        }

        public string TipoConexion()
        {
            if (_proveedor == "System.Data.OracleClient")
                return "ORACLE";
            return "";
        }

        public void CerrarConexion(DbConnection pConexionFactory)
        {
            try
            {
                pConexionFactory.Close();
            }
            catch (Exception ex)
            {
                _BOExcepcion.Throw("ConnectionDataBase", "CerrarConexion", ex);
            }
        }

        // Se ejecutara la auditoria si y solo si, la aplicacion cliente establecio un Func para la clase Service que audita y tengo habilitado la auditoria en el config
        void Provider_SpEjecutado(object sender, AuditoriaStoredProceduresArgs e)
        {
            Usuario usuarioParaAuditar = _usuario;

            if (_seAudita && _auditableService != null)
            {
                if (usuarioParaAuditar == null)
                {
                    usuarioParaAuditar = new Usuario();
                }

                e.AuditoriaEntity.codigousuario = Convert.ToInt32(usuarioParaAuditar.codusuario);
                e.AuditoriaEntity.nombreusuario = usuarioParaAuditar.nombre;
                e.AuditoriaEntity.codigoOpcion = usuarioParaAuditar.codOpcionActual;

                try
                {
                    _auditableService.CrearAuditoriaStoredProcedures(e.AuditoriaEntity, usuarioParaAuditar);
                }
                catch (Exception)
                {
                    // No se ve pertinente parar una operacion si un log de auditoria falla
                }
            }
        }

        void Provider_SpSessionID(object sender, AuditoriaStoredProceduresArgs e)
        {
            Usuario usuarioParaAuditar = _usuario;
            e.AuditoriaEntity.codigousuario = Convert.ToInt32(usuarioParaAuditar.codusuario);
            e.AuditoriaEntity.nombreusuario = usuarioParaAuditar.nombre;
            //_auditableService.CrearAuditoriaStoredProcedures(e.AuditoriaEntity, usuarioParaAuditar);
        }

        public string ObtenerStringConexion(Usuario pUsuario)
        {
            try
            {
                CadenaConexion(pUsuario);
                return _connectionString;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #region ConexionInterfazNomina
        public void CadenaConexionInterfazNomina(Usuario eUsuario)
        {
            // Determinar parametros de conexión del WEBCONFIG            
            _connectionString = ConfigurationManager.ConnectionStrings["InterfazNomina"].ConnectionString;
            _proveedor = ConfigurationManager.ConnectionStrings["InterfazNomina"].ProviderName;
        }

        public DbConnection ObtenerConexionInterfazNomina(Usuario pUsuario, bool seAudita = true)
        {
            try
            {
                try
                {
                    CadenaConexionInterfazNomina(pUsuario);

                    bool seAuditaEnEstaConexion = _seAudita && seAudita && ObtenerInstanciaAuditoria != null;
                    DbExpinnProviderFactory provider = new DbExpinnProviderFactory(DbProviderFactories.GetFactory(_proveedor), seAuditaEnEstaConexion);

                    if (seAuditaEnEstaConexion)
                    {
                        provider.SpEjecutado += Provider_SpEjecutado;
                    }

                    dbProveedorFactory = provider;

                    _usuario = pUsuario;

                    if (ObtenerInstanciaAuditoria != null)
                    {
                        _auditableService = ObtenerInstanciaAuditoria();
                    }

                    _conexionFactory = dbProveedorFactory.CreateConnection();
                    _conexionFactory.ConnectionString = _connectionString;

                    return _conexionFactory;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error de conexion a la Base de Datos de Interfaz Nomina. " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                _BOExcepcion.Throw("ConnectionDataBase", "ObtenerConexionInterfazNomina", ex);
                return null;
            }
        }

        #endregion

        #region ConexionInventarios
        public void CadenaConexionInventarios(Usuario eUsuario)
        {
            // Determinar parametros de conexión del WEBCONFIG            
            _connectionString = ConfigurationManager.ConnectionStrings["Inventarios"].ConnectionString;
            _proveedor = ConfigurationManager.ConnectionStrings["Inventarios"].ProviderName;
        }

        public DbConnection ObtenerConexionInventarios(Usuario pUsuario, bool seAudita = true)
        {
            try
            {
                try
                {
                    CadenaConexionInventarios(pUsuario);

                    bool seAuditaEnEstaConexion = _seAudita && seAudita && ObtenerInstanciaAuditoria != null;
                    DbExpinnProviderFactory provider = new DbExpinnProviderFactory(DbProviderFactories.GetFactory(_proveedor), seAuditaEnEstaConexion);

                    if (seAuditaEnEstaConexion)
                    {
                        provider.SpEjecutado += Provider_SpEjecutado;
                    }

                    dbProveedorFactory = provider;

                    _usuario = pUsuario;

                    if (ObtenerInstanciaAuditoria != null)
                    {
                        _auditableService = ObtenerInstanciaAuditoria();
                    }

                    _conexionFactory = dbProveedorFactory.CreateConnection();
                    _conexionFactory.ConnectionString = _connectionString;

                    return _conexionFactory;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error de conexion a la Base de Datos de Inventarios. " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                _BOExcepcion.Throw("ConnectionDataBase", "ObtenerConexionInventarios", ex);
                return null;
            }
        }

        #endregion


    }
}