using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Servicios.Entities;

namespace Xpinn.Servicios.Data
{
    public class DesembolsoServicioData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public DesembolsoServicioData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public DesembosoServicios CrearTransaccionDesembolso(DesembosoServicios pDesembolso, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_servicio = cmdTransaccionFactory.CreateParameter();
                        pnumero_servicio.ParameterName = "pnumero_servicio";
                        pnumero_servicio.Value = pDesembolso.numero_servicio;
                        pnumero_servicio.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pnumero_servicio);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        pestado.Value = "C";
                        pestado.Direction = ParameterDirection.Input;
                        pestado.Size = 1;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pusuario = cmdTransaccionFactory.CreateParameter();
                        pusuario.ParameterName = "pusuario";
                        pusuario.Value = vUsuario.codusuario;
                        pusuario.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pusuario);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "pcod_ope";
                        pcod_ope.Value = pDesembolso.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pnumero_preimpreso = cmdTransaccionFactory.CreateParameter();
                        pnumero_preimpreso.ParameterName = "pnumero_preimpreso";
                        if (pDesembolso.numero_preimpreso != null)
                            pnumero_preimpreso.Value = pDesembolso.numero_preimpreso;
                        else
                            pnumero_preimpreso.Value = DBNull.Value;
                        pnumero_preimpreso.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pnumero_preimpreso);

                        DbParameter perror = cmdTransaccionFactory.CreateParameter();
                        perror.ParameterName = "perror";
                        perror.Direction = ParameterDirection.Input;
                        perror.DbType = DbType.AnsiStringFixedLength;
                        perror.Size = 200;
                        cmdTransaccionFactory.Parameters.Add(perror);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_DESEMBOLSO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (perror != null)
                            if (perror.Value != null)
                                pDesembolso.error = perror.Value.ToString();

                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DesembolsoServicioData", "CrearTransaccionDesembolso", ex);
                        return null;
                    }
                }
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_transaccion = cmdTransaccionFactory.CreateParameter();
                        pnumero_transaccion.ParameterName = "p_numero_transaccion";
                        pnumero_transaccion.Value = pDesembolso.numero_transaccion;
                        pnumero_transaccion.Direction = ParameterDirection.Output;
                        pnumero_transaccion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_transaccion);

                        DbParameter pnumero_servicio = cmdTransaccionFactory.CreateParameter();
                        pnumero_servicio.ParameterName = "p_numero_servicio";
                        pnumero_servicio.Value = pDesembolso.numero_servicio;
                        pnumero_servicio.Direction = ParameterDirection.Input;
                        pnumero_servicio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_servicio);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        pcod_ope.Value = pDesembolso.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pcod_cliente = cmdTransaccionFactory.CreateParameter();
                        pcod_cliente.ParameterName = "p_cod_cliente";
                        pcod_cliente.Value = pDesembolso.cod_cliente;
                        pcod_cliente.Direction = ParameterDirection.Input;
                        pcod_cliente.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_cliente);

                        DbParameter pcod_linea_servicio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_servicio.ParameterName = "p_cod_linea_servicio";
                        pcod_linea_servicio.Value = pDesembolso.cod_linea_servicio;
                        pcod_linea_servicio.Direction = ParameterDirection.Input;
                        pcod_linea_servicio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_servicio);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        ptipo_tran.Value = pDesembolso.tipo_tran;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                        DbParameter pcod_det_lis = cmdTransaccionFactory.CreateParameter();
                        pcod_det_lis.ParameterName = "p_cod_det_lis";
                        if (pDesembolso.cod_det_lis != 0) pcod_det_lis.Value = pDesembolso.cod_det_lis; else pcod_det_lis.Value = DBNull.Value;
                        pcod_det_lis.Direction = ParameterDirection.Input;
                        pcod_det_lis.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_det_lis);

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        if (pDesembolso.cod_atr != 0) pcod_atr.Value = pDesembolso.cod_atr; else pcod_atr.Value = DBNull.Value;
                        pcod_atr.Direction = ParameterDirection.Input;
                        pcod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pDesembolso.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pDesembolso.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pnum_tran_anula = cmdTransaccionFactory.CreateParameter();
                        pnum_tran_anula.ParameterName = "p_num_tran_anula";
                        if (pDesembolso.num_tran_anula != 0) pnum_tran_anula.Value = pDesembolso.num_tran_anula; else pnum_tran_anula.Value = DBNull.Value;
                        pnum_tran_anula.Direction = ParameterDirection.Input;
                        pnum_tran_anula.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnum_tran_anula);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_TRANSERVIC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pDesembolso.numero_transaccion = Convert.ToInt64(pnumero_transaccion.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDesembolso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DesembolsoServicioData", "CrearTransaccionDesembolso", ex);
                        return null;
                    }
                }
            }
        }




        public Int64 ObtenerUltimoCodigo(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select max(cod_plan_servicio) from planservicios";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        try
                        {
                            resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());
                        }
                        catch
                        {
                            resultado = 1;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DesembolsoServicioData", "ObtenerSiguienteCodigo", ex);
                        return -1;
                    }
                }
            }
        }


        public DateTime? FechaInicioServicioNomina(DateTime pFechaActual, Int64 pCodEmpresa, Usuario vUsuario)
        {
            DateTime? pFechaInicio =  null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pFecha = cmdTransaccionFactory.CreateParameter();
                        pFecha.ParameterName = "pFecha";
                        pFecha.Value = pFechaActual;
                        pFecha.Direction = ParameterDirection.Input;
                        pFecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pFecha);

                        DbParameter p_CodEmpresa = cmdTransaccionFactory.CreateParameter();
                        p_CodEmpresa.ParameterName = "pCodEmpresa";
                        p_CodEmpresa.Value = pCodEmpresa;
                        p_CodEmpresa.Direction = ParameterDirection.Input;
                        p_CodEmpresa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_CodEmpresa);

                        DbParameter pTipoProducto = cmdTransaccionFactory.CreateParameter();
                        pTipoProducto.ParameterName = "pTipoProducto";
                        pTipoProducto.Value = 4;
                        pTipoProducto.Direction = ParameterDirection.Input;
                        pTipoProducto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pTipoProducto);

                        DbParameter pfecha_inicio = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicio.ParameterName = "pFechaInicio";
                        pfecha_inicio.Value = DBNull.Value;
                        pfecha_inicio.Direction = ParameterDirection.Output;
                        pfecha_inicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicio);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_FECHAINICIO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if(pfecha_inicio.Value != null)
                            pFechaInicio = Convert.ToDateTime(pfecha_inicio.Value);

                        return pFechaInicio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DesembolsoServicioData", "FechaInicioServicioNomina", ex);
                        return null;
                    }
                }
            }
        }


        public DateTime? FechaInicioServicioCaja(DateTime pFechaActual, string pCod_Periodicidad, Usuario vUsuario)
        {
            DbDataReader resultado;
            DateTime? pFechaInicio = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT FECSUMDIA(to_date('" + pFechaActual + "','" + conf.ObtenerFormatoFecha() + @"'), NUMERO_DIAS, TIPO_CALENDARIO) AS FECHA_PRIMER
                                    FROM PERIODICIDAD WHERE COD_PERIODICIDAD = " + pCod_Periodicidad;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["FECHA_PRIMER"] != DBNull.Value) pFechaInicio = Convert.ToDateTime(resultado["FECHA_PRIMER"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        
                        return pFechaInicio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DesembolsoServicioData", "FechaInicioServicioCaja", ex);
                        return null;
                    }
                }
            }
        }


    }
}
