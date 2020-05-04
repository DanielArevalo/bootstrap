using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla USUARIOS
    /// </summary>
    public class UsuariosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla USUARIOS
        /// </summary>
        public UsuariosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla USUARIOS de la base de datos
        /// </summary>
        /// <param name="pUsuarios">Entidad Usuarios</param>
        /// <returns>Entidad Usuarios creada</returns>
        public Usuarios CrearUsuarios(Usuarios pUsuarios, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = param + "CODUSUARIO";
                        pCODUSUARIO.Value = pUsuarios.codusuario;

                        DbParameter pIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pIDENTIFICACION.ParameterName = param + "IDENTIFICACION";
                        pIDENTIFICACION.Value = pUsuarios.identificacion;

                        DbParameter pNOMBRE = cmdTransaccionFactory.CreateParameter();
                        pNOMBRE.ParameterName = param + "NOMBRE";
                        pNOMBRE.Value = pUsuarios.nombre;

                        DbParameter pDIRECCION = cmdTransaccionFactory.CreateParameter();
                        pDIRECCION.ParameterName = param + "DIRECCION";
                        pDIRECCION.Value = pUsuarios.direccion;

                        DbParameter pTELEFONO = cmdTransaccionFactory.CreateParameter();
                        pTELEFONO.ParameterName = param + "TELEFONO";
                        pTELEFONO.Value = pUsuarios.telefono;

                        DbParameter pLOGIN = cmdTransaccionFactory.CreateParameter();
                        pLOGIN.ParameterName = param + "LOGIN";
                        pLOGIN.Value = pUsuarios.login;

                        DbParameter pCODPERFIL = cmdTransaccionFactory.CreateParameter();
                        pCODPERFIL.ParameterName = param + "CODPERFIL";
                        pCODPERFIL.Value = pUsuarios.codperfil;

                        DbParameter pFECHACREACION = cmdTransaccionFactory.CreateParameter();
                        pFECHACREACION.ParameterName = param + "FECHACREACION";
                        pFECHACREACION.Value = pUsuarios.fechacreacion;

                        DbParameter pCOD_OFICINA = cmdTransaccionFactory.CreateParameter();
                        pCOD_OFICINA.ParameterName = param + "COD_OFICINA";
                        pCOD_OFICINA.Value = pUsuarios.cod_oficina;

                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.ParameterName = param + "ESTADO";
                        pESTADO.Value = pUsuarios.estado;


                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);
                        cmdTransaccionFactory.Parameters.Add(pIDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRE);
                        cmdTransaccionFactory.Parameters.Add(pDIRECCION);
                        cmdTransaccionFactory.Parameters.Add(pTELEFONO);
                        cmdTransaccionFactory.Parameters.Add(pLOGIN);
                        cmdTransaccionFactory.Parameters.Add(pCODPERFIL);
                        cmdTransaccionFactory.Parameters.Add(pFECHACREACION);
                        cmdTransaccionFactory.Parameters.Add(pCOD_OFICINA);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_Caja_USUARIOS_C";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pUsuarios, pUsuario, pUsuarios.codusuario,"USUARIOS", Accion.Crear.ToString(),connection,cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                        pUsuarios.codusuario = Convert.ToInt64(pCODUSUARIO.Value);
                        return pUsuarios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuariosData", "CrearUsuarios", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla USUARIOS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Usuarios modificada</returns>
        public Usuarios ModificarUsuarios(Usuarios pUsuarios, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = param + "CODUSUARIO";
                        pCODUSUARIO.Value = pUsuarios.codusuario;

                        DbParameter pIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pIDENTIFICACION.ParameterName = param + "IDENTIFICACION";
                        pIDENTIFICACION.Value = pUsuarios.identificacion;

                        DbParameter pNOMBRE = cmdTransaccionFactory.CreateParameter();
                        pNOMBRE.ParameterName = param + "NOMBRE";
                        pNOMBRE.Value = pUsuarios.nombre;

                        DbParameter pDIRECCION = cmdTransaccionFactory.CreateParameter();
                        pDIRECCION.ParameterName = param + "DIRECCION";
                        pDIRECCION.Value = pUsuarios.direccion;

                        DbParameter pTELEFONO = cmdTransaccionFactory.CreateParameter();
                        pTELEFONO.ParameterName = param + "TELEFONO";
                        pTELEFONO.Value = pUsuarios.telefono;

                        DbParameter pLOGIN = cmdTransaccionFactory.CreateParameter();
                        pLOGIN.ParameterName = param + "LOGIN";
                        pLOGIN.Value = pUsuarios.login;

                        DbParameter pCODPERFIL = cmdTransaccionFactory.CreateParameter();
                        pCODPERFIL.ParameterName = param + "CODPERFIL";
                        pCODPERFIL.Value = pUsuarios.codperfil;

                        DbParameter pFECHACREACION = cmdTransaccionFactory.CreateParameter();
                        pFECHACREACION.ParameterName = param + "FECHACREACION";
                        pFECHACREACION.Value = pUsuarios.fechacreacion;

                        DbParameter pCOD_OFICINA = cmdTransaccionFactory.CreateParameter();
                        pCOD_OFICINA.ParameterName = param + "COD_OFICINA";
                        pCOD_OFICINA.Value = pUsuarios.cod_oficina;

                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.ParameterName = param + "ESTADO";
                        pESTADO.Value = pUsuarios.estado;

                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);
                        cmdTransaccionFactory.Parameters.Add(pIDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRE);
                        cmdTransaccionFactory.Parameters.Add(pDIRECCION);
                        cmdTransaccionFactory.Parameters.Add(pTELEFONO);
                        cmdTransaccionFactory.Parameters.Add(pLOGIN);
                        cmdTransaccionFactory.Parameters.Add(pCODPERFIL);
                        cmdTransaccionFactory.Parameters.Add(pFECHACREACION);
                        cmdTransaccionFactory.Parameters.Add(pCOD_OFICINA);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_Caja_USUARIOS_U";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pUsuarios, pUsuario, pUsuarios.codusuario, "USUARIOS", Accion.Modificar.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                        return pUsuarios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuariosData", "ModificarUsuarios", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla USUARIOS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de USUARIOS</param>
        public void EliminarUsuarios(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Usuarios pUsuarios = new Usuarios();

                        if (pUsuario.programaGeneraLog)
                            pUsuarios = ConsultarUsuarios(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = param + "CODUSUARIO";
                        pCODUSUARIO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_Caja_USUARIOS_D";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pUsuarios, pUsuario, pId, "USUARIOS", Accion.Eliminar.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuariosData", "InsertarUsuarios", ex);
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene un registro en la tabla USUARIOS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla USUARIOS</param>
        /// <returns>Entidad Usuarios consultado</returns>
        public Usuarios ConsultarUsuarios(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Usuarios entidad = new Usuarios();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT CODUSUARIO,IDENTIFICACION,NOMBRE,DIRECCION,TELEFONO,LOGIN,CODPERFIL,FECHACREACION,COD_OFICINA,ESTADO, sysdate fecha_actual FROM USUARIOS Where codusuario=" + pId.ToString();
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
                    catch(Exception ex)
                    {
                        BOExcepcion.Throw("UsuariosData", "ConsultarUsuarios", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla USUARIOS dados unos filtros
        /// </summary>
        /// <param name="pUSUARIOS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuarios obtenidos</returns>
        public List<Usuarios> ListarUsuarios(Usuarios pUsuarios, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Usuarios> lstUsuarios = new List<Usuarios>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  USUARIOS " + ObtenerFiltro(pUsuarios);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Usuarios entidad = new Usuarios();

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

                            lstUsuarios.Add(entidad);
                        }

                        return lstUsuarios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuariosData", "ListarUsuarios", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla USUARIOS dados unos filtros
        /// </summary>
        /// <param name="pUSUARIOS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuarios obtenidos</returns>
        public List<Usuarios> ListarUsuariosXOficina(Usuarios pUsuarios, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Usuarios> lstUsuarios = new List<Usuarios>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  USUARIOS Where codusuario not in(select cod_persona from CAJERO) and cod_oficina=" + pUsuarios.cod_oficina;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Usuarios entidad = new Usuarios();

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

                            lstUsuarios.Add(entidad);
                        }

                        return lstUsuarios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuariosData", "ListarUsuariosXOficina", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene la lista de Usuarios dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuarios cajeros obtenidas</returns>
        public List<Usuarios> ListarComboCajero(Usuarios pEntidad, Int32? pEstado = null, Usuario pUsuario = null)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Usuarios> lstPersona = new List<Usuarios>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT * FROM CAJERO a, USUARIOS b WHERE a.cod_persona = b.codusuario ";
                        if (pEstado != null)
                            sql += " AND a.estado = " + pEstado;
                        sql += " ORDER BY a.cod_persona asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Usuarios entidad = new Usuarios();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_cajero"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["cod_cajero"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nom_cajero = Convert.ToString(resultado["nombre"]);
                            lstPersona.Add(entidad);
                        }

                        return lstPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "ListarComboCajero", ex);
                        return null;
                    }
                }
            }
        }

            
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla USUARIOS dados unos filtros
        /// </summary>
        /// <param name="pUSUARIOS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuarios obtenidos</returns>
        public List<Usuarios> ListarUsuariosXOficina2(Usuarios pUsuarios, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Usuarios> lstUsuarios = new List<Usuarios>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  USUARIOS Where cod_oficina=" + pUsuarios.cod_oficina;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Usuarios entidad = new Usuarios();

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

                            lstUsuarios.Add(entidad);
                        }

                        return lstUsuarios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuariosData", "ListarUsuariosXOficina", ex);
                        return null;
                    }
                }
            }
        }
    }
}