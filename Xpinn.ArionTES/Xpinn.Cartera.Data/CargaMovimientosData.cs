using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Xpinn.Caja.Entities;
using Xpinn.Cartera.Entities;
using Xpinn.Util;

namespace Xpinn.Cartera.Data
{
    public class CargaMovimientosData
    {
        private ExcepcionBusiness BOExcepcion = new ExcepcionBusiness();
        protected ConnectionDataBase dbConnectionFactory = new ConnectionDataBase();
        public Boolean CargaMasivoMovimientos(CargaMovimientos carga, Usuario pUsuario, ref string Error)
        {
            try
            {
                using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
                {
                    using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                    {
                        try
                        {
                            DbParameter PN_NUM_PRODUCTO = cmdTransaccionFactory.CreateParameter();
                            PN_NUM_PRODUCTO.ParameterName = "PN_NUM_PRODUCTO";
                            PN_NUM_PRODUCTO.Value = carga.NumeroProducto;
                            PN_NUM_PRODUCTO.DbType = DbType.Int32;

                            DbParameter PN_COD_CLIENTE = cmdTransaccionFactory.CreateParameter();
                            PN_COD_CLIENTE.ParameterName = "PN_COD_CLIENTE";
                            PN_COD_CLIENTE.Value = carga.CodPersona;
                            PN_COD_CLIENTE.DbType = DbType.Int32;

                            DbParameter PN_COD_OPE = cmdTransaccionFactory.CreateParameter();
                            PN_COD_OPE.DbType = DbType.Int32; PN_COD_OPE.ParameterName = "PN_COD_OPE";
                            PN_COD_OPE.Value = carga.CodOperacion;
                            PN_COD_OPE.DbType = DbType.Int32;

                            DbParameter PN_VALOR_PAGO = cmdTransaccionFactory.CreateParameter();
                            PN_VALOR_PAGO.ParameterName = "PN_VALOR_PAGO";
                            PN_VALOR_PAGO.Value = carga.Valor;
                            PN_VALOR_PAGO.DbType = DbType.Int32;

                            DbParameter PF_FECHA_PAGO = cmdTransaccionFactory.CreateParameter();
                            PF_FECHA_PAGO.ParameterName = "PF_FECHA_PAGO";
                            PF_FECHA_PAGO.Value = DateTime.Today;
                            PF_FECHA_PAGO.DbType = DbType.DateTime;

                            DbParameter PN_TIPO_PROD = cmdTransaccionFactory.CreateParameter();
                            PN_TIPO_PROD.ParameterName = "PN_TIPO_PROD";
                            PN_TIPO_PROD.Value = carga.TipoProducto;
                            PN_TIPO_PROD.DbType = DbType.Int32;

                            DbParameter PN_COD_USU = cmdTransaccionFactory.CreateParameter();
                            PN_COD_USU.ParameterName = "PN_COD_USU";
                            PN_COD_USU.Value = Convert.ToInt32(pUsuario.cod_persona);
                            PN_COD_USU.DbType = DbType.Int32;

                            DbParameter RN_SOBRANTE = cmdTransaccionFactory.CreateParameter();
                            RN_SOBRANTE.ParameterName = "RN_SOBRANTE";
                            RN_SOBRANTE.Value = 0;
                            RN_SOBRANTE.DbType = DbType.Int32;

                            DbParameter PN_ERROR = cmdTransaccionFactory.CreateParameter();
                            PN_ERROR.ParameterName = "PN_ERROR";
                            PN_ERROR.Value = DBNull.Value;
                            PN_ERROR.DbType = DbType.String;

                            DbParameter PN_TIPONOTA = cmdTransaccionFactory.CreateParameter();
                            PN_TIPONOTA.ParameterName = "PN_TIPONOTA";
                            PN_TIPONOTA.Value = carga.TipoNota;
                            PN_TIPONOTA.DbType = DbType.Int32;

                            DbParameter PN_TIPO_MOVIMIENTO = cmdTransaccionFactory.CreateParameter();
                            PN_TIPO_MOVIMIENTO.ParameterName = "PN_TIPO_MOVIMIENTO";
                            PN_TIPO_MOVIMIENTO.Value = Convert.ToInt32(carga.TipoMovimiento);
                            PN_TIPO_MOVIMIENTO.DbType = DbType.Int32;

                            cmdTransaccionFactory.Parameters.Add(PN_NUM_PRODUCTO);
                            cmdTransaccionFactory.Parameters.Add(PN_COD_CLIENTE);
                            cmdTransaccionFactory.Parameters.Add(PN_COD_OPE);
                            cmdTransaccionFactory.Parameters.Add(PF_FECHA_PAGO);
                            cmdTransaccionFactory.Parameters.Add(PN_VALOR_PAGO);
                            cmdTransaccionFactory.Parameters.Add(PN_TIPO_PROD);
                            cmdTransaccionFactory.Parameters.Add(PN_COD_USU);
                            cmdTransaccionFactory.Parameters.Add(RN_SOBRANTE);
                            cmdTransaccionFactory.Parameters.Add(PN_TIPONOTA);
                            cmdTransaccionFactory.Parameters.Add(PN_ERROR);
                            cmdTransaccionFactory.Parameters.Add(PN_TIPO_MOVIMIENTO);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CARGA_MOV";
                            cmdTransaccionFactory.ExecuteNonQuery();
                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        catch (Exception ex)
                        {
                            BOExcepcion.Throw("CargaMovimientos", "CargaMovimientos", ex);
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                BOExcepcion.Throw("CargaMovimientos", "CargaMovimientosBusiness", e);
                return false;
            }

        }

        public Entities.TipoProducto ConsultarProducto(string tipo_producto, Usuario pUsuario)
        {

            DbDataReader resultado = default(DbDataReader);
            Entities.TipoProducto entidad = new Entities.TipoProducto();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select cod_tipo_producto, descripcion from tipoproducto where cod_tipo_producto = '" + tipo_producto + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (resultado["COD_TIPO_PRODUCTO"] != DBNull.Value) entidad.CodProducto = Convert.ToInt32(resultado["COD_TIPO_PRODUCTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CargaMovimientos", "ConsultarProducto", ex);
                        return null;
                    }
                }
            }
        }

        public Entities.TipoProducto ConsultaNProducto(string Query, Usuario pUsuario)
        {

            DbDataReader resultado = default(DbDataReader);
            Entities.TipoProducto entidad = new Entities.TipoProducto();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = Query;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (resultado["COD_PRODUCTO"] != DBNull.Value) entidad.CodProducto = Convert.ToInt32(resultado["COD_PRODUCTO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CargaMovimientos", "ConsultarProducto", ex);
                        return null;
                    }
                }
            }
        }

        public Entities.TipoProducto ConsultaSaldo(Int64 numero_radicacion, Usuario pUsuario)
        {

            DbDataReader resultado = default(DbDataReader);
            Entities.TipoProducto entidad = new Entities.TipoProducto();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string Query = @"select saldo from credito where numero_radicacio = " + numero_radicacion;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = Query;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (resultado["SALDO"] != DBNull.Value) entidad.CodProducto = Convert.ToInt32(resultado["SALDO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CargaMovimientos", "ConsultaSaldo", ex);
                        return null;
                    }
                }
            }
        }
    }
}
