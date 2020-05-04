using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;
using System.Web;

namespace Xpinn.Tesoreria.Data
{
    public class TrasladoDevolucionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public TrasladoDevolucionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public TrasladoDevolucion Crear_AplicacionDevolucion(TrasladoDevolucion pTraslado, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                // Actualizando el saldo y el estado de la devolución
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "PN_COD_CLIENTE";
                        pcod_persona.Value = pTraslado.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "PN_COD_OPE";
                        pcod_ope.Value = pTraslado.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pnum_devolucion = cmdTransaccionFactory.CreateParameter();
                        pnum_devolucion.ParameterName = "PN_NUM_DEVO";
                        pnum_devolucion.Value = pTraslado.num_devolucion;
                        pnum_devolucion.Direction = ParameterDirection.Input;
                        pnum_devolucion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnum_devolucion);

                        DbParameter PN_COD_USU = cmdTransaccionFactory.CreateParameter();
                        PN_COD_USU.ParameterName = "PN_COD_USU";
                        PN_COD_USU.Value =vUsuario.codusuario;
                        PN_COD_USU.Direction = ParameterDirection.Input;
                        PN_COD_USU.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PN_COD_USU);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_APLICADEVO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }   
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TrasladoDevolucionData", "Crear_Mod_TrasladoDevolucion", ex);
                        return null;
                    }
                }
            }
            return null;
        }

        public TrasladoDevolucion Crear_TrasladoDevolucion(TrasladoDevolucion pTraslado, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                // Actualizando el saldo y el estado de la devolución
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnum_devolucion = cmdTransaccionFactory.CreateParameter();
                        pnum_devolucion.ParameterName = "p_num_devolucion";
                        pnum_devolucion.Value = pTraslado.num_devolucion;
                        pnum_devolucion.Direction = ParameterDirection.Input;
                        pnum_devolucion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_devolucion);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pTraslado.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pTraslado.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        pcod_ope.Value = pTraslado.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_DEVOLUCION_TRAS";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TrasladoDevolucionData", "Crear_TrasladoDevolucion", ex);
                        return null;
                    }
                }

                // Creando la transacción de la devolución
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_transaccion = cmdTransaccionFactory.CreateParameter();
                        pnumero_transaccion.ParameterName = "p_numero_transaccion";
                        pnumero_transaccion.Value = pTraslado.numero_transaccion;
                        pnumero_transaccion.Direction = ParameterDirection.Output;
                        pnumero_transaccion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_transaccion);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        pcod_ope.Value = pTraslado.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pnum_devolucion = cmdTransaccionFactory.CreateParameter();
                        pnum_devolucion.ParameterName = "p_num_devolucion";
                        pnum_devolucion.Value = pTraslado.num_devolucion;
                        pnum_devolucion.Direction = ParameterDirection.Input;
                        pnum_devolucion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_devolucion);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        ptipo_tran.Value = pTraslado.tipo_tran;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pTraslado.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = "1";
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_TRASLA_DEV_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pTraslado.numero_transaccion = Convert.ToInt64(pnumero_transaccion.Value);
                        return pTraslado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TrasladoDevolucionData", "Crear_Mod_TrasladoDevolucion", ex);
                        return null;
                    }
                }
            }
        }

          
        public List<TrasladoDevolucion> ListarTrasladoDevolucion(String orden,string filtro,Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TrasladoDevolucion> lstEmpresaRecaudo = new List<TrasladoDevolucion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT d.NUM_DEVOLUCION,d.FECHA_DEVOLUCION,V.IDENTIFICACION,D.CONCEPTO,D.COD_PERSONA, "
                                        + "V.PRIMER_NOMBRE ||' '|| V.SEGUNDO_NOMBRE ||' '||V.PRIMER_APELLIDO ||' '||V.SEGUNDO_APELLIDO AS NOMBRE ,V.COD_NOMINA, D.SALDO,D.VALOR, "
                                        + "CASE D.Estado WHEN '1' THEN 'PENDIENTE' WHEN '2' THEN 'PAGADA' WHEN '3' THEN 'ANULADA' END AS ESTADO "
                                        + "FROM DEVOLUCION D LEFT JOIN V_PERSONA V ON D.COD_PERSONA = V.COD_PERSONA "
                                        + "WHERE TRIM(D.ESTADO) = 1 AND D.SALDO > 0 " + filtro + orden;
                       
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TrasladoDevolucion entidad = new TrasladoDevolucion();
                            if (resultado["NUM_DEVOLUCION"] != DBNull.Value) entidad.num_devolucion = Convert.ToInt32(resultado["NUM_DEVOLUCION"]);
                            if (resultado["FECHA_DEVOLUCION"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA_DEVOLUCION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["CONCEPTO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.nomestado = Convert.ToString(resultado["ESTADO"]);
                            lstEmpresaRecaudo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEmpresaRecaudo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TrasladoDevolucionData", "ListarTrasladoDevolucion", ex);
                        return null;
                    }
                }
            }
        }



        public List<TrasladoDevolucion> ConsultarTrasladoDevolucion(Int64 cod_persona, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TrasladoDevolucion> lstEmpresaRecaudo = new List<TrasladoDevolucion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT D.NUM_DEVOLUCION,D.FECHA_DEVOLUCION,D.CONCEPTO,D.VALOR,D.SALDO,D.COD_PERSONA,V.IDENTIFICACION, "
                            +"V.PRIMER_NOMBRE ||' '|| V.SEGUNDO_NOMBRE ||' '||V.PRIMER_APELLIDO ||' '||V.SEGUNDO_APELLIDO AS NOMBRE "            
                            +"FROM DEVOLUCION D INNER JOIN PERSONA V ON D.COD_PERSONA = V.COD_PERSONA "
                                        +"WHERE D.ESTADO  In (0, 1) AND D.SALDO != 0 AND D.COD_PERSONA = " + cod_persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TrasladoDevolucion entidad = new TrasladoDevolucion();
                            if (resultado["NUM_DEVOLUCION"] != DBNull.Value) entidad.num_devolucion = Convert.ToInt32(resultado["NUM_DEVOLUCION"]);
                            if (resultado["FECHA_DEVOLUCION"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA_DEVOLUCION"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["CONCEPTO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            lstEmpresaRecaudo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEmpresaRecaudo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TrasladoDevolucionData", "ConsultarTrasladoDevolucion", ex);
                        return null;
                    }
                }
            }
        }



        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(NUM_DEVOLUCION) + 1 FROM devolucion ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "ObtenerSiguienteCodigo", ex);
                        return -1;
                    }
                }
            }
        }


        public List<TrasladoDevolucion> ListarDevolucionesMenores(String orden, string filtro, decimal valor_menor, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TrasladoDevolucion> lstEmpresaRecaudo = new List<TrasladoDevolucion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT V.IDENTIFICACION, D.COD_PERSONA, V.PRIMER_NOMBRE ||' '|| V.SEGUNDO_NOMBRE ||' '||V.PRIMER_APELLIDO ||' '||V.SEGUNDO_APELLIDO AS NOMBRE, SUM(D.SALDO) AS SALDO, SUM(D.VALOR) AS VALOR "
                                        + "FROM DEVOLUCION D LEFT JOIN V_PERSONA V ON D.COD_PERSONA = V.COD_PERSONA "
                                        + "WHERE TRIM(D.ESTADO) = 1 AND D.SALDO > 0 " + filtro + orden + " " 
                                        + "GROUP BY V.IDENTIFICACION, D.COD_PERSONA, V.PRIMER_NOMBRE ||' '|| V.SEGUNDO_NOMBRE ||' '||V.PRIMER_APELLIDO ||' '||V.SEGUNDO_APELLIDO "
                                        + "HAVING SUM(D.SALDO) < " + valor_menor;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TrasladoDevolucion entidad = new TrasladoDevolucion();                            
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);                            
                            lstEmpresaRecaudo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEmpresaRecaudo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TrasladoDevolucionData", "ListarTrasladoDevolucion", ex);
                        return null;
                    }
                }
            }
        }


        public List<TrasladoDevolucion> ListarDevolucionesMasivas(String orden, string filtro,  Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TrasladoDevolucion> lstEmpresaRecaudo = new List<TrasladoDevolucion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select d.num_devolucion, d.cod_persona, d.identificacion, rm.fecha_recaudo, rm.numero_novedad, p.nombre, d.valor, d.saldo from devolucion d "
                                        + " Inner join recaudo_masivo  rm  on d.num_recaudo = rm.numero_recaudo"
                                        + " Inner join v_persona p on p.cod_persona = d.cod_persona "
                                        + " Where TRIM(d.estado) = 1 And d.saldo > 0 " + filtro + orden + " order by d.cod_persona,d.valor desc";
                                        

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TrasladoDevolucion entidad = new TrasladoDevolucion();
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["NUM_DEVOLUCION"] != DBNull.Value) entidad.num_devolucion = Convert.ToInt64(resultado["NUM_DEVOLUCION"]);
                            if (resultado["FECHA_RECAUDO"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA_RECAUDO"]);
                            lstEmpresaRecaudo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEmpresaRecaudo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TrasladoDevolucionData", "ListarTrasladoDevolucion", ex);
                        return null;
                    }
                }
            }
        }

        public List<TrasladoDevolucion> ListarDevolucionesPersona(String pIdentificacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TrasladoDevolucion> lstDevoluciones = new List<TrasladoDevolucion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT D.NUM_DEVOLUCION, V.IDENTIFICACION, D.COD_PERSONA, V.PRIMER_NOMBRE ||' '|| V.SEGUNDO_NOMBRE ||' '|| V.PRIMER_APELLIDO ||' '|| V.SEGUNDO_APELLIDO AS NOMBRE, D.SALDO, D.VALOR "
                                        + "FROM DEVOLUCION D LEFT JOIN V_PERSONA V ON D.COD_PERSONA = V.COD_PERSONA "
                                        + "WHERE TRIM(D.ESTADO) = 1 AND D.SALDO > 0 AND V.IDENTIFICACION = '" + pIdentificacion + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TrasladoDevolucion entidad = new TrasladoDevolucion();
                            if (resultado["NUM_DEVOLUCION"] != DBNull.Value) entidad.num_devolucion = Convert.ToInt64(resultado["NUM_DEVOLUCION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            lstDevoluciones.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDevoluciones;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TrasladoDevolucionData", "ListarDevolucionesPersona", ex);
                        return null;
                    }
                }
            }
        }



    }
}




