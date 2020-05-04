using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Usuarios
    /// </summary>
    public class UsuarioAseData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Usuarios
        /// </summary>
        public UsuarioAseData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla Usuarios de la base de datos
        /// </summary>
        /// <param name="pUsuario">Entidad Usuario</param>
        /// <returns>Entidad Usuario creada</returns>
        public UsuarioAse CrearUsuario(UsuarioAse pUsuarioAse, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = "p_codusuario";
                        pCODUSUARIO.Value = pUsuarioAse.codusuario;
                        pCODUSUARIO.Direction = ParameterDirection.InputOutput;

                        DbParameter pNOMBRE = cmdTransaccionFactory.CreateParameter();
                        pNOMBRE.ParameterName = "p_nombre";
                        pNOMBRE.Value = pUsuarioAse.nombre;


                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRE);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_Asesores_Usuarios_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pUsuario, "Usuarios",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pUsuarioAse.codusuario = Convert.ToInt64(pCODUSUARIO.Value);
                        return pUsuarioAse;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "CrearUsuario", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla Usuarios de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Usuario modificada</returns>
        public UsuarioAse ModificarUsuario(UsuarioAse pUsuarioAse, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = "p_CODUSUARIO";
                        pCODUSUARIO.Value = pUsuarioAse.codusuario;

                        DbParameter pNOMBRE = cmdTransaccionFactory.CreateParameter();
                        pNOMBRE.ParameterName = "p_NOMBRE";
                        pNOMBRE.Value = pUsuarioAse.nombre;

                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRE);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_Asesores_Usuarios_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pUsuario, "Usuarios",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pUsuarioAse;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "ModificarUsuario", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla Usuarios de la base de datos
        /// </summary>
        /// <param name="pId">identificador de Usuarios</param>
        public void EliminarUsuario(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Usuario pUsuarioAse = new Usuario();

                        //if (pUsuario.programaGeneraLog)
                        //    pUsuarioAse = ConsultarUsuario(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = "p_codusuario";
                        pCODUSUARIO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_Asesores_Usuarios_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pUsuario, "Usuarios", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "EliminarUsuario", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla Usuarios de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Usuarios</param>
        /// <returns>Entidad Usuario consultado</returns>
        public UsuarioAse ConsultarUsuario(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            UsuarioAse entidad = new UsuarioAse();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  USUARIOS WHERE codusuario = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "ConsultarUsuario", ex);
                        return null;
                    }
                }
            }
        }
        public UsuarioAse ConsultarUsuarios(String pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            UsuarioAse entidad = new UsuarioAse();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT CODUSUARIO,IDENTIFICACION,NOMBRE,DIRECCION,TELEFONO,LOGIN,CODPERFIL,FECHACREACION,COD_OFICINA,ESTADO, sysdate fecha_actual FROM USUARIOS Where IDENTIFICACION='" + pId+"'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["LOGIN"] != DBNull.Value) entidad.login = Convert.ToString(resultado["LOGIN"]);
                            if (resultado["CODPERFIL"] != DBNull.Value) entidad.codperfil = Convert.ToInt64(resultado["CODPERFIL"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);
                            if (resultado["fecha_actual"] != DBNull.Value) entidad.fecha_actual = Convert.ToDateTime(resultado["fecha_actual"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuariosData", "ConsultarUsuarios", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Usuarios dados unos filtros
        /// </summary>
        /// <param name="pUsuarios">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuario obtenidos</returns>
        public List<UsuarioAse> ListarUsuario(UsuarioAse pUsuarioAse, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<UsuarioAse> lstUsuario = new List<UsuarioAse>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT codusuario, nombre FROM  USUARIOS " + ObtenerFiltro(pUsuarioAse);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            UsuarioAse entidad = new UsuarioAse();

                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);

                            lstUsuario.Add(entidad);
                        }

                        return lstUsuario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "ListarUsuario", ex);
                        return null;
                    }
                }
            }
        }

        public List<UsuarioAse> ListartodosUsuarios(UsuarioAse pUsuarioAse, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<UsuarioAse> lstUsuario = new List<UsuarioAse>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT u.codusuario, u.nombre, u.cod_oficina FROM USUARIOS u
                                        INNER JOIN asejecutivos ase on u.identificacion = ase.sidentificacion
                                        WHERE u.estado = 1 and cod_oficina = "+pUsuario.cod_oficina +" order by u.nombre";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            UsuarioAse entidad = new UsuarioAse();

                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);

                            lstUsuario.Add(entidad);
                        }

                        return lstUsuario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "ListarUsuario", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Usuarios-abogados dados unos filtros
        /// </summary>
        /// <param name="pUsuarios">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuario-Abogados obtenidos</returns>
        public List<UsuarioAse> ListarUsuarioAbogados(UsuarioAse pUsuarioAse, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<UsuarioAse> lstUsuario = new List<UsuarioAse>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select Distinct C.Abogado_Encargado, U.Nombre From Cobros_Credito C Inner Join Usuarios U  on  c.abogado_encargado=U.Codusuario " + ObtenerFiltro(pUsuarioAse);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            UsuarioAse entidad = new UsuarioAse();

                            if (resultado["Abogado_Encargado"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["Abogado_Encargado"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);

                            lstUsuario.Add(entidad);
                        }

                        return lstUsuario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "ListarUsuarioAbogados", ex);
                        return null;
                    }
                }
            }
        }

       

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Usuarios dados unos filtros
        /// </summary>
        /// <param name="pUsuarios">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuario obtenidos</returns>
        public List<UsuarioAse> ListarUsuariosPerfilAbogado(UsuarioAse pUsuarioAse, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<UsuarioAse> lstUsuario = new List<UsuarioAse>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT codusuario, nombre FROM  USUARIOS where codperfil= " + pUsuarioAse.codusuario;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            UsuarioAse entidad = new UsuarioAse();

                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);

                            lstUsuario.Add(entidad);
                        }

                        return lstUsuario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "ListarUsuariosPerfilAbogado", ex);
                        return null;
                    }
                }
            }
        }
    }
}