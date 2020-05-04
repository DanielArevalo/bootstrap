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
    /// Objeto de acceso a datos para la tabla Tipo_Pago
    /// </summary>
    public class TipoPagoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Tipo_Pago
        /// </summary>
        public TipoPagoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla Tipo_Pago de la base de datos
        /// </summary>
        /// <param name="pTipoPago">Entidad TipoPago</param>
        /// <returns>Entidad TipoPago creada</returns>
        public TipoPago CrearTipoPago(TipoPago pTipoPago, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_TIPO_PAGO = cmdTransaccionFactory.CreateParameter();
                        pCOD_TIPO_PAGO.ParameterName = "p_cod_tipo_pago";
                        pCOD_TIPO_PAGO.Value = pTipoPago.cod_tipo_pago;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_descripcion";
                        pDESCRIPCION.Value = pTipoPago.descripcion;

                        DbParameter pCAJA = cmdTransaccionFactory.CreateParameter();
                        pCAJA.ParameterName = "p_caja";
                        pCAJA.Value = pTipoPago.caja;

                        cmdTransaccionFactory.Parameters.Add(pCOD_TIPO_PAGO);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);
                        cmdTransaccionFactory.Parameters.Add(pCAJA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_Caja_Tipo_Pago_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTipoPago, "Tipo_Pago",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        return pTipoPago;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoPagoData", "CrearTipoPago", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla Tipo_Pago de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad TipoPago modificada</returns>
        public TipoPago ModificarTipoPago(TipoPago pTipoPago, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_TIPO_PAGO = cmdTransaccionFactory.CreateParameter();
                        pCOD_TIPO_PAGO.ParameterName = "p_COD_TIPO_PAGO";
                        pCOD_TIPO_PAGO.Value = pTipoPago.cod_tipo_pago;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_DESCRIPCION";
                        pDESCRIPCION.Value = pTipoPago.descripcion;

                        DbParameter pCAJA = cmdTransaccionFactory.CreateParameter();
                        pCAJA.ParameterName = "p_caja";
                        pCAJA.Value = pTipoPago.caja;

                        cmdTransaccionFactory.Parameters.Add(pCOD_TIPO_PAGO);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);
                        cmdTransaccionFactory.Parameters.Add(pCAJA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_Caja_Tipo_Pago_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTipoPago, "Tipo_Pago",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pTipoPago;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoPagoData", "ModificarTipoPago", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla Tipo_Pago de la base de datos
        /// </summary>
        /// <param name="pId">identificador de Tipo_Pago</param>
        public void EliminarTipoPago(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        TipoPago pTipoPago = new TipoPago();

                        if (pUsuario.programaGeneraLog)
                            pTipoPago = ConsultarTipoPago(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_TIPO_PAGO = cmdTransaccionFactory.CreateParameter();
                        pCOD_TIPO_PAGO.ParameterName = "p_cod_tipo_pago";
                        pCOD_TIPO_PAGO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_TIPO_PAGO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_Caja_Tipo_Pago_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTipoPago, "Tipo_Pago", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoPagoData", "EliminarTipoPago", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla Tipo_Pago de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Tipo_Pago</param>
        /// <returns>Entidad TipoPago consultado</returns>
        public TipoPago ConsultarTipoPago(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            TipoPago entidad = new TipoPago();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TIPO_PAGO WHERE cod_tipo_pago = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_TIPO_PAGO"] != DBNull.Value) entidad.cod_tipo_pago = Convert.ToInt64(resultado["COD_TIPO_PAGO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CAJA"] != DBNull.Value) entidad.caja = Convert.ToString(resultado["CAJA"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch(Exception ex)
                    {
                        BOExcepcion.Throw("TipoPagoData", "ConsultarTipoPago", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Tipo_Pago dados unos filtros
        /// </summary>
        /// <param name="pTipo_Pago">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoPago obtenidos</returns>
        public List<TipoPago> ListarTipoPago(TipoPago pTipoPago, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoPago> lstTipoPago = new List<TipoPago>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TIPO_PAGO " + ObtenerFiltro(pTipoPago) + " Order By descripcion asc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (string.Equals(resultado["CAJA"], "S"))
                            {
                                TipoPago entidad = new TipoPago();

                                if (resultado["COD_TIPO_PAGO"] != DBNull.Value) entidad.cod_tipo_pago = Convert.ToInt64(resultado["COD_TIPO_PAGO"]);
                                if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                                lstTipoPago.Add(entidad);
                            }
                        }

                        return lstTipoPago;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoPagoData", "ListarTipoPago", ex);
                        return null;
                    }
                }
            }
        }


        public List<TipoPago> ListarTipoPagoCon(TipoPago pTipoPago, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoPago> lstTipoPago = new List<TipoPago>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TIPO_PAGO " + ObtenerFiltro(pTipoPago) + " Order By 1 asc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoPago entidad = new TipoPago();

                            if (resultado["COD_TIPO_PAGO"] != DBNull.Value) entidad.cod_tipo_pago = Convert.ToInt64(resultado["COD_TIPO_PAGO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CAJA"] != DBNull.Value) entidad.caja = Convert.ToString(resultado["CAJA"]);

                            lstTipoPago.Add(entidad);
                        }

                        return lstTipoPago;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoPagoData", "ListarTipoPago", ex);
                        return null;
                    }
                }
            }
        }


    }
}