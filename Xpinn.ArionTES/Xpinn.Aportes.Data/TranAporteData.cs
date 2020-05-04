using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Aportes.Entities;

namespace Xpinn.Aportes.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla TranAporteS
    /// </summary>
    public class TranAporteData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla TranAporteS
        /// </summary>
        public TranAporteData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla TranAporteS de la base de datos
        /// </summary>
        /// <param name="pTranAporte">Entidad TranAporte</param>
        /// <returns>Entidad TranAporte creada</returns>
        public TranAporte CrearTranAporte(TranAporte pTranAporte, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_transaccion = cmdTransaccionFactory.CreateParameter();
                        pnumero_transaccion.ParameterName = "p_numero_transaccion";
                        pnumero_transaccion.Value = pTranAporte.numero_transaccion;
                        pnumero_transaccion.Direction = ParameterDirection.Output;
                        pnumero_transaccion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_transaccion);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        pcod_ope.Value = pTranAporte.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pTranAporte.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pnumero_aporte = cmdTransaccionFactory.CreateParameter();
                        pnumero_aporte.ParameterName = "p_numero_aporte";
                        pnumero_aporte.Value = pTranAporte.numero_aporte;
                        pnumero_aporte.Direction = ParameterDirection.Input;
                        pnumero_aporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_aporte);

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        pcod_atr.Value = pTranAporte.cod_atr;
                        pcod_atr.Direction = ParameterDirection.Input;
                        pcod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter pcod_det_lis = cmdTransaccionFactory.CreateParameter();
                        pcod_det_lis.ParameterName = "p_cod_det_lis";
                        pcod_det_lis.Value = pTranAporte.cod_det_lis;
                        pcod_det_lis.Direction = ParameterDirection.Input;
                        pcod_det_lis.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_det_lis);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        ptipo_tran.Value = pTranAporte.tipo_tran;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pTranAporte.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pTranAporte.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_TRANAPORTE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTranAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TranAporteData", "CrearTranAporte", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Modifica un registro en la tabla TranAporteS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad TranAporte modificada</returns>
        public TranAporte ModificarTranAporte(TranAporte pTranAporte, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_transaccion = cmdTransaccionFactory.CreateParameter();
                        pnumero_transaccion.ParameterName = "p_numero_transaccion";
                        pnumero_transaccion.Value = pTranAporte.numero_transaccion;
                        pnumero_transaccion.Direction = ParameterDirection.Input;
                        pnumero_transaccion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_transaccion);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        pcod_ope.Value = pTranAporte.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pTranAporte.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pnumero_aporte = cmdTransaccionFactory.CreateParameter();
                        pnumero_aporte.ParameterName = "p_numero_aporte";
                        pnumero_aporte.Value = pTranAporte.numero_aporte;
                        pnumero_aporte.Direction = ParameterDirection.Input;
                        pnumero_aporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_aporte);

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        pcod_atr.Value = pTranAporte.cod_atr;
                        pcod_atr.Direction = ParameterDirection.Input;
                        pcod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter pcod_det_lis = cmdTransaccionFactory.CreateParameter();
                        pcod_det_lis.ParameterName = "p_cod_det_lis";
                        pcod_det_lis.Value = pTranAporte.cod_det_lis;
                        pcod_det_lis.Direction = ParameterDirection.Input;
                        pcod_det_lis.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_det_lis);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        ptipo_tran.Value = pTranAporte.tipo_tran;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pTranAporte.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pTranAporte.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_TRANAPORTE_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        return pTranAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TranAporteData", "ModificarTranAporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla TranAporteS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de TranAporteS</param>
        public void EliminarTranAporte(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        TranAporte pTranAporte = new TranAporte();
                        DbParameter pnumero_transaccion = cmdTransaccionFactory.CreateParameter();
                        pnumero_transaccion.ParameterName = "p_numero_transaccion";
                        pnumero_transaccion.Value = pTranAporte.numero_transaccion;
                        pnumero_transaccion.Direction = ParameterDirection.Input;
                        pnumero_transaccion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_transaccion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_TRANAPORTE_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoData", "EliminarConcepto", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla TranAporteS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla TranAporteS</param>
        /// <returns>Entidad TranAporte consultado</returns>
        public TranAporte ConsultarTranAporte(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            TranAporte entidad = new TranAporte();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Tran_Aporte WHERE NUMERO_TRANSACCION = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_TRANSACCION"] != DBNull.Value) entidad.numero_transaccion = Convert.ToInt64(resultado["NUMERO_TRANSACCION"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NUMERO_APORTE"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt32(resultado["NUMERO_APORTE"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["COD_DET_LIS"] != DBNull.Value) entidad.cod_det_lis = Convert.ToInt32(resultado["COD_DET_LIS"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt32(resultado["TIPO_TRAN"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
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
                        BOExcepcion.Throw("TranAporteData", "ConsultarTranAporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TranAporteS dados unos filtros
        /// </summary>
        /// <param name="pTranAporteS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TranAporte obtenidos</returns>
        public List<TranAporte> ListarTranAporte(TranAporte pTranAporte, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TranAporte> lstTranAporte = new List<TranAporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Tran_Aporte " + ObtenerFiltro(pTranAporte) + " ORDER BY NUMERO_TRANSACCION ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TranAporte entidad = new TranAporte();
                            if (resultado["NUMERO_TRANSACCION"] != DBNull.Value) entidad.numero_transaccion = Convert.ToInt64(resultado["NUMERO_TRANSACCION"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NUMERO_APORTE"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt32(resultado["NUMERO_APORTE"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["COD_DET_LIS"] != DBNull.Value) entidad.cod_det_lis = Convert.ToInt32(resultado["COD_DET_LIS"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt32(resultado["TIPO_TRAN"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            lstTranAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTranAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TranAporteData", "ListarTranAporte", ex);
                        return null;
                    }
                }
            }
        }


    }
}