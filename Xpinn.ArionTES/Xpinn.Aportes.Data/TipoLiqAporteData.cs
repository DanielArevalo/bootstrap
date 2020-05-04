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
    /// Objeto de acceso a datos para la tabla TipoLiqAporteS
    /// </summary>
    public class TipoLiqAporteData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla TipoLiqAporteS
        /// </summary>
        public TipoLiqAporteData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla TipoLiqAporteS de la base de datos
        /// </summary>
        /// <param name="pTipoLiqAporte">Entidad TipoLiqAporte</param>
        /// <returns>Entidad TipoLiqAporte creada</returns>
        public TipoLiqAporte CrearTipoLiqAporte(TipoLiqAporte pTipoLiqAporte, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_tipo_liquidacion = cmdTransaccionFactory.CreateParameter();
                        p_tipo_liquidacion.ParameterName = "p_tipo_liquidacion";
                        p_tipo_liquidacion.Value = pTipoLiqAporte.tipo_liquidacion;
                        p_tipo_liquidacion.Direction = ParameterDirection.Output;
                        p_tipo_liquidacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_liquidacion);

                        DbParameter p_nombre = cmdTransaccionFactory.CreateParameter();
                        p_nombre.ParameterName = "p_nombre";
                        p_nombre.Value = pTipoLiqAporte.nombre;
                        p_nombre.Direction = ParameterDirection.Input;
                        p_nombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_nombre);

                        DbParameter p_cod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        p_cod_periodicidad.ParameterName = "p_cod_periodicidad";
                        p_cod_periodicidad.Value = pTipoLiqAporte.cod_periodicidad;
                        p_cod_periodicidad.Direction = ParameterDirection.Input;
                        p_cod_periodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_periodicidad);

                        DbParameter ptipo_saldo_base = cmdTransaccionFactory.CreateParameter();
                        ptipo_saldo_base.ParameterName = "p_tipo_saldo_base";
                        ptipo_saldo_base.Value = pTipoLiqAporte.tipo_saldo_base;
                        ptipo_saldo_base.Direction = ParameterDirection.Input;
                        ptipo_saldo_base.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_saldo_base);

                        DbParameter p_lineaaporte_base = cmdTransaccionFactory.CreateParameter();
                        p_lineaaporte_base.ParameterName = "p_lineaaporte_base";
                        p_lineaaporte_base.Value = pTipoLiqAporte.lineaaporte_base;
                        p_lineaaporte_base.Direction = ParameterDirection.Input;
                        p_lineaaporte_base.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_lineaaporte_base);

                        DbParameter p_lineaaporte_afecta = cmdTransaccionFactory.CreateParameter();
                        p_lineaaporte_afecta.ParameterName = "p_lineaaporte_afecta";
                        p_lineaaporte_afecta.Value = pTipoLiqAporte.lineaaporte_afecta;
                        p_lineaaporte_afecta.Direction = ParameterDirection.Input;
                        p_lineaaporte_afecta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_lineaaporte_afecta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_TIPO_LIQAP_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTipoLiqAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoLiqAporteData", "CrearTipoLiqAporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla TipoLiqAporteS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad TipoLiqAporte modificada</returns>
        public TipoLiqAporte ModificarTipoLiqAporte(TipoLiqAporte pTipoLiqAporte, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                       
                        DbParameter p_tipo_liquidacion = cmdTransaccionFactory.CreateParameter();
                        p_tipo_liquidacion.ParameterName = "p_tipo_liquidacion";
                        p_tipo_liquidacion.Value = pTipoLiqAporte.tipo_liquidacion;
                        p_tipo_liquidacion.Direction = ParameterDirection.Input;
                        p_tipo_liquidacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_liquidacion);

                        DbParameter p_nombre = cmdTransaccionFactory.CreateParameter();
                        p_nombre.ParameterName = "p_nombre";
                        p_nombre.Value = pTipoLiqAporte.nombre;
                        p_nombre.Direction = ParameterDirection.Input;
                        p_nombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_nombre);

                        DbParameter p_cod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        p_cod_periodicidad.ParameterName = "p_cod_periodicidad";
                        p_cod_periodicidad.Value = pTipoLiqAporte.cod_periodicidad;
                        p_cod_periodicidad.Direction = ParameterDirection.Input;
                        p_cod_periodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_periodicidad);

                        DbParameter ptipo_saldo_base = cmdTransaccionFactory.CreateParameter();
                        ptipo_saldo_base.ParameterName = "p_tipo_saldo_base";
                        ptipo_saldo_base.Value = pTipoLiqAporte.tipo_saldo_base;
                        ptipo_saldo_base.Direction = ParameterDirection.Input;
                        ptipo_saldo_base.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_saldo_base);

                        DbParameter p_lineaaporte_base = cmdTransaccionFactory.CreateParameter();
                        p_lineaaporte_base.ParameterName = "p_lineaaporte_base";
                        p_lineaaporte_base.Value = pTipoLiqAporte.lineaaporte_base;
                        p_lineaaporte_base.Direction = ParameterDirection.Input;
                        p_lineaaporte_base.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_lineaaporte_base);

                        DbParameter p_lineaaporte_afecta = cmdTransaccionFactory.CreateParameter();
                        p_lineaaporte_afecta.ParameterName = "p_lineaaporte_afecta";
                        p_lineaaporte_afecta.Value = pTipoLiqAporte.lineaaporte_afecta;
                        p_lineaaporte_afecta.Direction = ParameterDirection.Input;
                        p_lineaaporte_afecta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_lineaaporte_afecta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_TIPO_LIQAP_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        return pTipoLiqAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoLiqAporteData", "ModificarTipoLiqAporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla TipoLiqAporteS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de TipoLiqAporteS</param>
        public void EliminarTipoLiqAporte(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        TipoLiqAporte pTipoLiqAporte = new TipoLiqAporte();
                        DbParameter p_tipo_liquidacion = cmdTransaccionFactory.CreateParameter();
                        p_tipo_liquidacion.ParameterName = "p_tipo_liquidacion";
                        p_tipo_liquidacion.Value = pId;
                        p_tipo_liquidacion.Direction = ParameterDirection.Input;
                        p_tipo_liquidacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_liquidacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_TIPO_LIQAP_ELIMI";
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
        /// Obtiene un registro en la tabla TipoLiqAporteS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla TipoLiqAporteS</param>
        /// <returns>Entidad TipoLiqAporte consultado</returns>
        public TipoLiqAporte ConsultarTipoLiqAporte(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            TipoLiqAporte entidad = new TipoLiqAporte();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM tipo_liqaporte WHERE TIPO_LIQUIDACION = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt32(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["TIPO_SALDO_BASE"] != DBNull.Value) entidad.tipo_saldo_base = Convert.ToInt32(resultado["TIPO_SALDO_BASE"]);
                            if (resultado["LINEAAPORTE_BASE"] != DBNull.Value) entidad.lineaaporte_base = Convert.ToInt32(resultado["LINEAAPORTE_BASE"]);
                            if (resultado["LINEAAPORTE_AFECTA"] != DBNull.Value) entidad.lineaaporte_afecta = Convert.ToInt32(resultado["LINEAAPORTE_AFECTA"]);
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
                        BOExcepcion.Throw("TipoLiqAporteData", "ConsultarTipoLiqAporte", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene un registro en la tabla TipoLiqAporteS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla TipoLiqAporteS</param>
        /// <returns>Entidad TipoLiqAporte consultado</returns>
        public TipoLiqAporte ConsultarMax( Usuario vUsuario)
        {
            DbDataReader resultado;
            TipoLiqAporte entidad = new TipoLiqAporte();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT max(tipo_liquidacion)  as LIQUIDACION FROM TIPO_LIQAPORTE";                    
                   
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt32(resultado["LIQUIDACION"]);
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
                        BOExcepcion.Throw("TipoLiqAporteData", "ConsultarMax", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TipoLiqAporteS dados unos filtros
        /// </summary>
        /// <param name="pTipoLiqAporteS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoLiqAporte obtenidos</returns>
        public List<TipoLiqAporte> ListarTipoLiqAporte(TipoLiqAporte pTipoLiqAporte, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TipoLiqAporte> lstTipoLiqAporte = new List<TipoLiqAporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Tipo_LiqAporte " + ObtenerFiltro(pTipoLiqAporte) + " ORDER BY TIPO_LIQUIDACION ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TipoLiqAporte entidad = new TipoLiqAporte();
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt32(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["TIPO_SALDO_BASE"] != DBNull.Value) entidad.tipo_saldo_base = Convert.ToInt32(resultado["TIPO_SALDO_BASE"]);
                            if (resultado["LINEAAPORTE_BASE"] != DBNull.Value) entidad.lineaaporte_base = Convert.ToInt32(resultado["LINEAAPORTE_BASE"]);
                            if (resultado["LINEAAPORTE_AFECTA"] != DBNull.Value) entidad.lineaaporte_afecta = Convert.ToInt32(resultado["LINEAAPORTE_AFECTA"]);
                            lstTipoLiqAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipoLiqAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoLiqAporteData", "ListarTipoLiqAporte", ex);
                        return null;
                    }
                }
            }
        }

       
    }
}