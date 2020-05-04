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
    /// Objeto de acceso a datos para la tabla INVENTARIOACTIVO
    /// </summary>
    public class InventarioActivoFijoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla INVENTARIOACTIVO
        /// </summary>
        public InventarioActivoFijoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla INVENTARIOACTIVO de la base de datos
        /// </summary>
        /// <param name="pInventarioActivoFijo">Entidad InventarioActivoFijo</param>
        /// <returns>Entidad InventarioActivoFijo creada</returns>
        public InventarioActivoFijo CrearInventarioActivoFijo(InventarioActivoFijo pInventarioActivoFijo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_ACTIVO = cmdTransaccionFactory.CreateParameter();
                        pCOD_ACTIVO.ParameterName = "p_COD_ACTIVO";
                        pCOD_ACTIVO.Value = 0;
                        pCOD_ACTIVO.Direction = ParameterDirection.InputOutput;

                        DbParameter pCOD_INFFIN = cmdTransaccionFactory.CreateParameter();
                        pCOD_INFFIN.ParameterName = "p_COD_INFFIN";
                        pCOD_INFFIN.Value = pInventarioActivoFijo.cod_inffin;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_DESCRIPCION";
                        pDESCRIPCION.Value = pInventarioActivoFijo.descripcion;

                        DbParameter pMARCA = cmdTransaccionFactory.CreateParameter();
                        pMARCA.ParameterName = "p_MARCA";
                        pMARCA.Value = pInventarioActivoFijo.marca;

                        DbParameter pVALOR = cmdTransaccionFactory.CreateParameter();
                        pVALOR.ParameterName = "p_VALOR";
                        pVALOR.Value = pInventarioActivoFijo.valor;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pInventarioActivoFijo.cod_persona;

                        cmdTransaccionFactory.Parameters.Add(pCOD_ACTIVO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_INFFIN);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);
                        cmdTransaccionFactory.Parameters.Add(pMARCA);
                        cmdTransaccionFactory.Parameters.Add(pVALOR);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_INVAC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();                        
                        
                        // ACTUALIZA LOS ESTADOS FINANCIEROS                       

                        DbCommand cmdTransaccionFactory2 = dbConnectionFactory.dbProveedorFactory.CreateCommand();

                        DbParameter pCOD_INFFIN1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_INFFIN1.ParameterName = "p_COD_INFFIN1";
                        pCOD_INFFIN1.Value = pInventarioActivoFijo.cod_inffin;

                        DbParameter pCOD_PERSONA1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_PERSONA1.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA1.Value = pInventarioActivoFijo.cod_persona;

                        cmdTransaccionFactory2.Parameters.Add(pCOD_INFFIN1);
                        cmdTransaccionFactory2.Parameters.Add(pCOD_PERSONA1);

                        cmdTransaccionFactory2.Connection = connection;
                        cmdTransaccionFactory2.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory2.CommandText = "USP_XPINN_SOLICRED_ESFIN_MODIF";
                        cmdTransaccionFactory2.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pInventarioActivoFijo, "INVENTARIOACTIVO",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pInventarioActivoFijo.cod_activo = Convert.ToInt64(pCOD_ACTIVO.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pInventarioActivoFijo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventarioActivoFijoData", "CrearInventarioActivoFijo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla INVENTARIOACTIVO de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad InventarioActivoFijo modificada</returns>
        public InventarioActivoFijo ModificarInventarioActivoFijo(InventarioActivoFijo pInventarioActivoFijo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_ACTIVO = cmdTransaccionFactory.CreateParameter();
                        pCOD_ACTIVO.ParameterName = "p_COD_ACTIVO";
                        pCOD_ACTIVO.Value = pInventarioActivoFijo.cod_activo;

                        DbParameter pCOD_INFFIN = cmdTransaccionFactory.CreateParameter();
                        pCOD_INFFIN.ParameterName = "p_COD_INFFIN";
                        pCOD_INFFIN.Value = pInventarioActivoFijo.cod_inffin;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_DESCRIPCION";
                        pDESCRIPCION.Value = pInventarioActivoFijo.descripcion;

                        DbParameter pMARCA = cmdTransaccionFactory.CreateParameter();
                        pMARCA.ParameterName = "p_MARCA";
                        pMARCA.Value = pInventarioActivoFijo.marca;

                        DbParameter pVALOR = cmdTransaccionFactory.CreateParameter();
                        pVALOR.ParameterName = "p_VALOR";
                        pVALOR.Value = pInventarioActivoFijo.valor;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pInventarioActivoFijo.cod_persona;

                        cmdTransaccionFactory.Parameters.Add(pCOD_ACTIVO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_INFFIN);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);
                        cmdTransaccionFactory.Parameters.Add(pMARCA);
                        cmdTransaccionFactory.Parameters.Add(pVALOR);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_INVAC_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // ACTUALIZA LOS ESTADOS FINANCIEROS                       

                        DbCommand cmdTransaccionFactory2 = dbConnectionFactory.dbProveedorFactory.CreateCommand();

                        DbParameter pCOD_INFFIN1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_INFFIN1.ParameterName = "p_COD_INFFIN1";
                        pCOD_INFFIN1.Value = pInventarioActivoFijo.cod_inffin;

                        DbParameter pCOD_PERSONA1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_PERSONA1.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA1.Value = pInventarioActivoFijo.cod_persona;

                        cmdTransaccionFactory2.Parameters.Add(pCOD_INFFIN1);
                        cmdTransaccionFactory2.Parameters.Add(pCOD_PERSONA1);

                        cmdTransaccionFactory2.Connection = connection;
                        cmdTransaccionFactory2.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory2.CommandText = "USP_XPINN_SOLICRED_ESFIN_MODIF";
                        cmdTransaccionFactory2.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pInventarioActivoFijo, "INVENTARIOACTIVO",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pInventarioActivoFijo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventarioActivoFijoData", "ModificarInventarioActivoFijo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla INVENTARIOACTIVO de la base de datos
        /// </summary>
        /// <param name="pId">identificador de INVENTARIOACTIVO</param>
        public void EliminarInventarioActivoFijo(Int64 pId, Usuario pUsuario, Int64 Cod_persona, Int64 Cod_InfFin)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        InventarioActivoFijo pInventarioActivoFijo = new InventarioActivoFijo();

                        DbParameter pCOD_ACTIVO = cmdTransaccionFactory.CreateParameter();
                        pCOD_ACTIVO.ParameterName = "p_COD_ACTIVO";
                        pCOD_ACTIVO.Value = pId;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = Cod_persona;

                        cmdTransaccionFactory.Parameters.Add(pCOD_ACTIVO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_INVAC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // ACTUALIZA LOS ESTADOS FINANCIEROS                       

                        DbCommand cmdTransaccionFactory2 = dbConnectionFactory.dbProveedorFactory.CreateCommand();

                        DbParameter pCOD_INFFIN1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_INFFIN1.ParameterName = "p_COD_INFFIN1";
                        pCOD_INFFIN1.Value = Cod_InfFin;

                        DbParameter pCOD_PERSONA1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_PERSONA1.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA1.Value = Cod_persona;

                        cmdTransaccionFactory2.Parameters.Add(pCOD_INFFIN1);
                        cmdTransaccionFactory2.Parameters.Add(pCOD_PERSONA1);

                        cmdTransaccionFactory2.Connection = connection;
                        cmdTransaccionFactory2.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory2.CommandText = "USP_XPINN_SOLICRED_ESFIN_MODIF";
                        cmdTransaccionFactory2.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pInventarioActivoFijo, "INVENTARIOACTIVO", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventarioActivoFijoData", "EliminarInventarioActivoFijo", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla INVENTARIOACTIVO de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla INVENTARIOACTIVO</param>
        /// <returns>Entidad InventarioActivoFijo consultado</returns>
        public InventarioActivoFijo ConsultarInventarioActivoFijo(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            InventarioActivoFijo entidad = new InventarioActivoFijo();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  INVENTARIOACTIVO WHERE COD_ACTIVO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_ACTIVO"] != DBNull.Value) entidad.cod_activo = Convert.ToInt64(resultado["COD_ACTIVO"]);
                            if (resultado["COD_INFFIN"] != DBNull.Value) entidad.cod_inffin = Convert.ToInt64(resultado["COD_INFFIN"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["MARCA"] != DBNull.Value) entidad.marca = Convert.ToString(resultado["MARCA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch(Exception ex)
                    {
                        BOExcepcion.Throw("InventarioActivoFijoData", "ConsultarInventarioActivoFijo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla INVENTARIOACTIVO dados unos filtros
        /// </summary>
        /// <param name="pINVENTARIOACTIVO">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InventarioActivoFijo obtenidos</returns>
        public List<InventarioActivoFijo> ListarInventarioActivoFijo(InventarioActivoFijo pInventarioActivoFijo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<InventarioActivoFijo> lstInventarioActivoFijo = new List<InventarioActivoFijo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT INVENTARIOACTIVO.* FROM INFORMACIONFINANCIERA, INVENTARIOACTIVO 
                                       WHERE
                                       INVENTARIOACTIVO.COD_INFFIN = INFORMACIONFINANCIERA.COD_INFFIN AND
                                       INFORMACIONFINANCIERA.COD_PERSONA = " + pInventarioActivoFijo.cod_persona +
                                       " and INVENTARIOACTIVO.COD_INFFIN = " + pInventarioActivoFijo.cod_inffin;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InventarioActivoFijo entidad = new InventarioActivoFijo();

                            if (resultado["COD_ACTIVO"] != DBNull.Value) entidad.cod_activo = Convert.ToInt64(resultado["COD_ACTIVO"]);
                            if (resultado["COD_INFFIN"] != DBNull.Value) entidad.cod_inffin = Convert.ToInt64(resultado["COD_INFFIN"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["MARCA"] != DBNull.Value) entidad.marca = Convert.ToString(resultado["MARCA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);

                            lstInventarioActivoFijo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstInventarioActivoFijo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventarioActivoFijoData", "ListarInventarioActivoFijo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla INVENTARIOACTIVO dados unos filtros
        /// </summary>
        /// <param name="pINVENTARIOACTIVO">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InventarioActivoFijo obtenidos</returns>
        public List<InventarioActivoFijo> ListarInventarioActivoFijoRepo(InventarioActivoFijo pInventarioActivoFijo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<InventarioActivoFijo> lstInventarioActivoFijo = new List<InventarioActivoFijo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT INVENTARIOACTIVO.* FROM INFORMACIONFINANCIERA, INVENTARIOACTIVO 
                                       WHERE
                                       INVENTARIOACTIVO.COD_INFFIN = INFORMACIONFINANCIERA.COD_INFFIN AND
                                       INFORMACIONFINANCIERA.COD_PERSONA = " + pInventarioActivoFijo.cod_persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InventarioActivoFijo entidad = new InventarioActivoFijo();

                            if (resultado["COD_ACTIVO"] != DBNull.Value) entidad.cod_activo = Convert.ToInt64(resultado["COD_ACTIVO"]);
                            if (resultado["COD_INFFIN"] != DBNull.Value) entidad.cod_inffin = Convert.ToInt64(resultado["COD_INFFIN"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["MARCA"] != DBNull.Value) entidad.marca = Convert.ToString(resultado["MARCA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);

                            lstInventarioActivoFijo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstInventarioActivoFijo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventarioActivoFijoData", "ListarInventarioActivoFijoRepo", ex);
                        return null;
                    }
                }
            }
        }


    }
}