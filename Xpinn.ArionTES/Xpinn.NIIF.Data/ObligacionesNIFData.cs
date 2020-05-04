using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.NIIF.Entities;

namespace Xpinn.NIIF.Data
{
    public class ObligacionesNIFData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public ObligacionesNIFData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public Boolean ConsultarFECHAIngresada(DateTime pFecha, Usuario vUsuario)
        {
            DbDataReader resultado;
            ObligacionesNIF entidad = new ObligacionesNIF();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT * FROM Cierea WHERE TIPO = 'O' and ESTADO = 'D'";

                        if (sql.ToUpper().Contains("WHERE"))
                            sql += " And ";
                        else
                            sql += " Where ";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql += " FECHA = To_Date('" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                        else
                            sql += " FECHA = '" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);                           
                        }
                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObligacionesNIFData", "ConsultarFECHAIngresada", ex);
                        return false;
                    }
                }
            }
        }


        public void GENERAR_ObligacionesNIF(ObligacionesNIF pObli, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha_liquidacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_liquidacion.ParameterName = "PFECHA";
                        pfecha_liquidacion.Value = pObli.fecha;
                        pfecha_liquidacion.Direction = ParameterDirection.Input;
                        pfecha_liquidacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_liquidacion);

                        DbParameter pusuario = cmdTransaccionFactory.CreateParameter();
                        pusuario.ParameterName = "PUSUARIO";
                        pusuario.Value = vUsuario.codusuario;
                        pusuario.Direction = ParameterDirection.Input;
                        pusuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pusuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_COSTOAMORTIZADO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch 
                    {
                        //BOExcepcion.Throw("ObligacionesNIFData", "GENERAR_ObligacionesNIF", ex);
                    }                    
                }
            }
        }

        public List<ObligacionesNIF> Listar_TEMP_CostoAMortizado(DateTime pFecha, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ObligacionesNIF> lstCosto = new List<ObligacionesNIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select Obcosto_Amortizado.*,Bancos.Nombrebanco as nomentidad "
                                        + "From Obcosto_Amortizado Inner Join Bancos "
                                        + "on Bancos.Cod_Banco = Obcosto_Amortizado.Entidad ";

                        if (pFecha != null && pFecha != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " FECHA = To_Date('" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " FECHA = '" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        sql += "Order By Codcostoamortizado";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ObligacionesNIF entidad = new ObligacionesNIF();
                            if (resultado["CODCOSTOAMORTIZADO"] != DBNull.Value) entidad.codcostoamortizado = Convert.ToInt64(resultado["CODCOSTOAMORTIZADO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["CODOBLIGACION"] != DBNull.Value) entidad.codobligacion = Convert.ToInt64(resultado["CODOBLIGACION"]);
                            if (resultado["ENTIDAD"] != DBNull.Value) entidad.entidad = Convert.ToString(resultado["ENTIDAD"]);
                            if (resultado["MONTO"] != DBNull.Value) entidad.monto = Convert.ToDecimal(resultado["MONTO"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["PLAZO_FALTANTE"] != DBNull.Value) entidad.plazo_faltante = Convert.ToInt32(resultado["PLAZO_FALTANTE"]);
                            if (resultado["TASA_MERCADO"] != DBNull.Value) entidad.tasa_mercado = Convert.ToDecimal(resultado["TASA_MERCADO"]);
                            if (resultado["TIR"] != DBNull.Value) entidad.tir = Convert.ToDecimal(resultado["TIR"]);
                            if (resultado["VALOR_PRESENTE"] != DBNull.Value) entidad.valor_presente = Convert.ToDecimal(resultado["VALOR_PRESENTE"]);
                            if (resultado["VALOR_AJUSTE"] != DBNull.Value) entidad.valor_ajuste = Convert.ToDecimal(resultado["VALOR_AJUSTE"]);
                            if (resultado["NOMENTIDAD"] != DBNull.Value) entidad.nomentidad = Convert.ToString(resultado["NOMENTIDAD"]);
                            lstCosto.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCosto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObligacionesNIFData", "Listar_TEMP_CostoAMortizado", ex);
                        return null;
                    }
                }
            }
        }


        public ObligacionesNIF ModificarFechaCTOAMORTIZACION_NIF(ObligacionesNIF pCosto, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pCosto.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.Value = pCosto.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pCosto.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_AMORTIFECHA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCosto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObligacionesNIFData", "ModificarFechaCTOAMORTIZACION_NIF", ex);
                        return null;
                    }
                }
            }
        }

        


    }
}
