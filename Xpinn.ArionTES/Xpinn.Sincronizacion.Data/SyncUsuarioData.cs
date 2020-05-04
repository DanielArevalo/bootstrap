using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Util;

namespace Xpinn.Sincronizacion.Data
{
    public class SyncUsuarioData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
        public SyncUsuarioData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<SyncUsuario> ListarSyncUsuarios(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<SyncUsuario> lstUsuario = null;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM USUARIOS " + pFiltro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.HasRows)
                        {
                            lstUsuario = new List<SyncUsuario>();
                            SyncUsuario entidad;
                            while (resultado.Read())
                            {
                                entidad = new SyncUsuario();
                                if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);
                                if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                                if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                                if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                                if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                                if (resultado["LOGIN"] != DBNull.Value) entidad.login = Convert.ToString(resultado["LOGIN"]);
                                if (resultado["LOGIN"] != DBNull.Value) entidad.clave = Convert.ToString(resultado["LOGIN"]);
                                if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                                if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);
                                if (resultado["CODPERFIL"] != DBNull.Value) entidad.codperfil = Convert.ToInt64(resultado["CODPERFIL"]);
                                if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                                lstUsuario.Add(entidad);
                            }
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstUsuario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncUsuarioData", "ListarSyncUsuarios", ex);
                        return null;
                    }
                }
            }
        }

        public List<string> ListarIPUsuario(SyncUsuario pUsuario, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<string> lstIpAcceso= null;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT direccionip FROM usuario_ipacceso WHERE codusuario = " + pUsuario.codusuario;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.HasRows)
                        {
                            lstIpAcceso = new List<string>();
                            string entidad = string.Empty;
                            while (resultado.Read())
                            {
                                entidad = "";
                                if (resultado["direccionip"] != DBNull.Value) entidad = Convert.ToString(resultado["direccionip"]);
                                lstIpAcceso.Add(entidad);
                            }
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstIpAcceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncUsuarioData", "ListarIPUsuario", ex);
                        return lstIpAcceso;
                    }
                }
            }
        }

        public List<int> ListarAtribucionesUsuario(SyncUsuario pUsuario, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<int> lstAtribUsuario= new List<int>();
            for (int i = 0; i <= 5; i += 1)
            {
                if (i >= lstAtribUsuario.Count)
                    lstAtribUsuario.Add(0);
                lstAtribUsuario[i] = 0;
            }

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT DISTINCT tipoatribucion, activo FROM  usuario_atribuciones WHERE codusuario = " + pUsuario.codusuario + " ORDER BY tipoatribucion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            int tipoatribucion = 0;
                            int activo = 0;
                            if (resultado["tipoatribucion"] != DBNull.Value) tipoatribucion = Convert.ToInt32(resultado["tipoatribucion"]);
                            if (resultado["activo"] != DBNull.Value) activo = Convert.ToInt32(resultado["activo"]);
                            lstAtribUsuario[tipoatribucion] = 1;
                            if (activo != 1)
                                lstAtribUsuario[tipoatribucion] = 0;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAtribUsuario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncUsuarioData", "ListarAtribuciones", ex);
                        return null;
                    }
                }
            }
        }


        public List<SyncPerfil> ListarSyncPerfilUsuario(string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<SyncPerfil> lstPerfil = null;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  PERFIL_USUARIO " + pFiltro + " ORDER BY CODPERFIL";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.HasRows)
                        {
                            lstPerfil = new List<SyncPerfil>();
                            SyncPerfil entidad;
                            while (resultado.Read())
                            {
                                entidad = new SyncPerfil();
                                if (resultado["CODPERFIL"] != DBNull.Value) entidad.codperfil = Convert.ToInt64(resultado["CODPERFIL"]);
                                if (resultado["NOMBREPERFIL"] != DBNull.Value) entidad.nombreperfil = Convert.ToString(resultado["NOMBREPERFIL"]);
                                if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                                lstPerfil.Add(entidad);
                            }
                        }
                        return lstPerfil;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncUsuarioData", "ListarSyncPerfilUsuario", ex);
                        return null;
                    }
                }
            }
        }

        public List<SyncAcceso> ListarSyncPerfilAcceso(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<SyncAcceso> lstPerfilAcce = null;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT codacceso, codperfil, cod_opcion, consultar, insertar, modificar, borrar FROM Perfil_Acceso " + pFiltro + " order by 1,2";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.HasRows)
                        {
                            lstPerfilAcce = new List<SyncAcceso>();
                            SyncAcceso entidad;
                            while (resultado.Read())
                            {
                                entidad = new SyncAcceso();
                                if (resultado["CODACCESO"] != DBNull.Value) entidad.codacceso = Convert.ToInt64(resultado["CODACCESO"]);
                                if (resultado["CODPERFIL"] != DBNull.Value) entidad.codperfil = Convert.ToInt64(resultado["CODPERFIL"]);
                                if (resultado["COD_OPCION"] != DBNull.Value) entidad.cod_opcion = Convert.ToInt64(resultado["COD_OPCION"]);
                                if (resultado["CONSULTAR"] != DBNull.Value) entidad.consultar = Convert.ToInt32(resultado["CONSULTAR"]);
                                if (resultado["INSERTAR"] != DBNull.Value) entidad.insertar = Convert.ToInt32(resultado["INSERTAR"]);
                                if (resultado["MODIFICAR"] != DBNull.Value) entidad.modificar = Convert.ToInt32(resultado["MODIFICAR"]);
                                if (resultado["BORRAR"] != DBNull.Value) entidad.borrar = Convert.ToInt32(resultado["BORRAR"]);
                                lstPerfilAcce.Add(entidad);
                            }
                        }
                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPerfilAcce;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncUsuarioData", "ListarSyncPerfilAcceso", ex);
                        return null;
                    }
                }
            }
        }


    }
}
