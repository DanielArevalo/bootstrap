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
    /// Objeto de acceso a datos para la tabla atributos
    /// </summary>
    public class AtributoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla atributos
        /// </summary>
        public AtributoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla atributos de la base de datos
        /// </summary>
        /// <param name="pAtributo">Entidad Atributo</param>
        /// <returns>Entidad Atributo creada</returns>
        public Atributo CrearAtributo(Atributo pAtributo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_ATR = cmdTransaccionFactory.CreateParameter();
                        pCOD_ATR.ParameterName = "p_cod_atr";
                        pCOD_ATR.Value = pAtributo.cod_atr;
                        pCOD_ATR.Direction = ParameterDirection.InputOutput;

                        DbParameter pNOMBRE = cmdTransaccionFactory.CreateParameter();
                        pNOMBRE.ParameterName = "p_nombre";
                        pNOMBRE.Value = pAtributo.nombre;

                        DbParameter pSALDO_ATRIBUTO = cmdTransaccionFactory.CreateParameter();
                        pSALDO_ATRIBUTO.ParameterName = "p_saldo_atributo";
                        pSALDO_ATRIBUTO.Value = pAtributo.saldo_atributo;


                        cmdTransaccionFactory.Parameters.Add(pCOD_ATR);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRE);
                        cmdTransaccionFactory.Parameters.Add(pSALDO_ATRIBUTO);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_Asesores_atributos_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pAtributo, "atributos", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pAtributo.cod_atr = Convert.ToInt64(pCOD_ATR.Value);
                        return pAtributo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AtributoData", "CrearAtributo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla atributos de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Atributo modificada</returns>
        public Atributo ModificarAtributo(Atributo pAtributo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_ATR = cmdTransaccionFactory.CreateParameter();
                        pCOD_ATR.ParameterName = "p_COD_ATR";
                        pCOD_ATR.Value = pAtributo.cod_atr;

                        DbParameter pNOMBRE = cmdTransaccionFactory.CreateParameter();
                        pNOMBRE.ParameterName = "p_NOMBRE";
                        pNOMBRE.Value = pAtributo.nombre;

                        DbParameter pSALDO_ATRIBUTO = cmdTransaccionFactory.CreateParameter();
                        pSALDO_ATRIBUTO.ParameterName = "p_SALDO_ATRIBUTO";
                        pSALDO_ATRIBUTO.Value = pAtributo.saldo_atributo;

                        cmdTransaccionFactory.Parameters.Add(pCOD_ATR);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRE);
                        cmdTransaccionFactory.Parameters.Add(pSALDO_ATRIBUTO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_Asesores_atributos_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pAtributo, "atributos", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pAtributo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AtributoData", "ModificarAtributo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla atributos de la base de datos
        /// </summary>
        /// <param name="pId">identificador de atributos</param>
        public void EliminarAtributo(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Atributo pAtributo = new Atributo();

                        if (pUsuario.programaGeneraLog)
                            pAtributo = ConsultarAtributo(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_ATR = cmdTransaccionFactory.CreateParameter();
                        pCOD_ATR.ParameterName = "p_cod_atr";
                        pCOD_ATR.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_ATR);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_Asesores_atributos_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pAtributo, "atributos", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AtributoData", "EliminarAtributo", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla atributos de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla atributos</param>
        /// <returns>Entidad Atributo consultado</returns>
        public Atributo ConsultarAtributo(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Atributo entidad = new Atributo();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  ATRIBUTOS WHERE cod_atr = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["COD_ATR"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["SALDO_ATRIBUTO"] != DBNull.Value) entidad.saldo_atributo = Convert.ToInt64(resultado["SALDO_ATRIBUTO"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AtributoData", "ConsultarAtributo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla atributos dados unos filtros
        /// </summary>
        /// <param name="patributos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Atributo obtenidos</returns>
        public List<Atributo> ListarAtributo(Int64 numero_radicacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Atributo> lstAtributo = new List<Atributo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select a.cod_atr, a.nombre, ac.saldo_atributo from atributoscredito ac, atributos a where ac.cod_atr=a.cod_atr and numero_radicacion= " + numero_radicacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Atributo entidad = new Atributo();

                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["COD_ATR"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["SALDO_ATRIBUTO"] != DBNull.Value) entidad.saldo_atributo = Convert.ToInt64(resultado["SALDO_ATRIBUTO"]);

                            lstAtributo.Add(entidad);
                        }

                        return lstAtributo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AtributoData", "ListarAtributo", ex);
                        return null;
                    }
                }
            }
        }
    }
}