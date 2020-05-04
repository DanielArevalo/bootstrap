using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla parentescos
    /// </summary>
    public class ParentescoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla parentescos
        /// </summary>
        public ParentescoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla parentescos de la base de datos
        /// </summary>
        /// <param name="pParentesco">Entidad Parentesco</param>
        /// <returns>Entidad Parentesco creada</returns>
        public Parentesco CrearParentesco(Parentesco pParentesco, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODPARENTESCO = cmdTransaccionFactory.CreateParameter();
                        pCODPARENTESCO.ParameterName = param + "CODPARENTESCO";
                        pCODPARENTESCO.Value = 0;
                        pCODPARENTESCO.Direction = ParameterDirection.Output;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = param + "DESCRIPCION";
                        pDESCRIPCION.Value = pParentesco.descripcion;

                        cmdTransaccionFactory.Parameters.Add(pCODPARENTESCO);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_FabricaCreditos_parentescos_C";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pParentesco, "parentescos", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pParentesco.codparentesco = Convert.ToInt64(pCODPARENTESCO.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pParentesco;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParentescoData", "CrearParentesco", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla parentescos de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Parentesco modificada</returns>
        public Parentesco ModificarParentesco(Parentesco pParentesco, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODPARENTESCO = cmdTransaccionFactory.CreateParameter();
                        pCODPARENTESCO.ParameterName = param + "CODPARENTESCO";
                        pCODPARENTESCO.Value = pParentesco.codparentesco;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = param + "DESCRIPCION";
                        pDESCRIPCION.Value = pParentesco.descripcion;

                        cmdTransaccionFactory.Parameters.Add(pCODPARENTESCO);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_FabricaCreditos_parentescos_U";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pParentesco, "parentescos", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pParentesco;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParentescoData", "ModificarParentesco", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla parentescos de la base de datos
        /// </summary>
        /// <param name="pId">identificador de parentescos</param>
        public void EliminarParentesco(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Parentesco pParentesco = new Parentesco();

                        if (pUsuario.programaGeneraLog)
                            pParentesco = ConsultarParentesco(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCODPARENTESCO = cmdTransaccionFactory.CreateParameter();
                        pCODPARENTESCO.ParameterName = param + "CODPARENTESCO";
                        pCODPARENTESCO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCODPARENTESCO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_FabricaCreditos_parentescos_D";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pParentesco, "parentescos", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParentescoData", "InsertarParentesco", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla parentescos de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla parentescos</param>
        /// <returns>Entidad Parentesco consultado</returns>
        public Parentesco ConsultarParentesco(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Parentesco entidad = new Parentesco();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  PARENTESCOS WHERE codparentesco = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODPARENTESCO"] == DBNull.Value) entidad.codparentesco = Convert.ToInt64(resultado["CODPARENTESCO"]);
                            if (resultado["DESCRIPCION"] == DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParentescoData", "ConsultarParentesco", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla parentescos dados unos filtros
        /// </summary>
        /// <param name="pparentescos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Parentesco obtenidos</returns>
        public List<Parentesco> ListarParentesco(Parentesco pParentesco, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Parentesco> lstParentesco = new List<Parentesco>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  PARENTESCOS ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Parentesco entidad = new Parentesco();

                            if (resultado["CODPARENTESCO"] != DBNull.Value) entidad.codparentesco = Convert.ToInt64(resultado["CODPARENTESCO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstParentesco.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstParentesco;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParentescoData", "ListarParentesco", ex);
                        return null;
                    }
                }
            }
        }

    }
}