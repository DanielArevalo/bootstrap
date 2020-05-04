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
    /// Objeto de acceso a datos para la tabla IngresosFamilia
    /// </summary>
    public class ModalidadtasaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Modalidad_Tasa
        /// </summary>
        public ModalidadtasaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
    
        public ModalidadTasa CrearModalidadTasa(ModalidadTasa pModalidadTasa, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_credito.ParameterName = "p_cod_linea_credito";
                        pcod_linea_credito.Value = pModalidadTasa.cod_linea_credito;
                        pcod_linea_credito.Direction = ParameterDirection.Input;
                        pcod_linea_credito.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_credito);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pModalidadTasa.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pModalidadTasa.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pcod_modalidad = cmdTransaccionFactory.CreateParameter();
                        pcod_modalidad.ParameterName = "p_cod_modalidad";
                        pcod_modalidad.Value = pModalidadTasa.cod_modalidad;
                        pcod_modalidad.Direction = ParameterDirection.Input;
                        pcod_modalidad.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcod_modalidad);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_ModalidadTasa_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pModalidadTasa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ModalidadTasaData", "CrearModalidadTasa", ex);
                        return null;
                    }
                }
            }
        }
        public void EliminarModalidadTasa(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ModalidadTasa pModalidadTasa = new ModalidadTasa();
                        pModalidadTasa = ConsultarModalidadTasa(pId, vUsuario);

                        DbParameter pcod_modalidad = cmdTransaccionFactory.CreateParameter();
                        pcod_modalidad.ParameterName = "p_cod_modalidad";
                        pcod_modalidad.Value = pModalidadTasa.cod_modalidad;
                        pcod_modalidad.Direction = ParameterDirection.Input;
                        pcod_modalidad.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcod_modalidad);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_ModalidadTasa_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ModalidadTasaData", "EliminarModalidadTasa", ex);
                    }
                }
            }
        }
        public ModalidadTasa ConsultarModalidadTasa(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ModalidadTasa entidad = new ModalidadTasa();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM MODALIDAD_TASA WHERE COD_MODALIDAD = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt32(resultado["VALOR"]);
                            if (resultado["COD_MODALIDAD"] != DBNull.Value) entidad.cod_modalidad = Convert.ToDecimal(resultado["COD_MODALIDAD"]);
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
                        BOExcepcion.Throw("ModalidadTasaData", "ConsultarModalidadTasa", ex);
                        return null;
                    }
                }
            }
        }
        public List<ModalidadTasa> ListarModalidadTasa(String pIdCodLinea, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ModalidadTasa> lstModalidadTasa = new List<ModalidadTasa>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM MODALIDAD_TASA where COD_LINEA_CREDITO= " + pIdCodLinea + " ORDER BY COD_MODALIDAD ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ModalidadTasa entidad = new ModalidadTasa();
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.valor = Convert.ToInt32(resultado["TASA"]);
                            if (resultado["COD_MODALIDAD"] != DBNull.Value) entidad.cod_modalidad = Convert.ToDecimal(resultado["COD_MODALIDAD"]);
                            lstModalidadTasa.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstModalidadTasa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ModalidadTasaData", "ListarModalidadTasa", ex);
                        return null;
                    }
                }
            }
        }
    }
}


