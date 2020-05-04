using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.NIIF.Entities;

namespace Xpinn.NIIF.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla PlanCuentasNIIFS
    /// </summary>
    public class CarteraNIFData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PlanCuentasNIIFS
        /// </summary>
        public CarteraNIFData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        

        public void EliminarCarteraNIIF(CarteraNIF pCarteraNIIF, DateTime vFecha, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pCarteraNIIF.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = vFecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_COSTORIESGO_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraNIIFData", "EliminarCarteraNIIF", ex);
                    }
                }
            }
        }


        public void ModificarEstadoFechaNIIF(DateTime vFecha, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = vFecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_FECHACARTERA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                      
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AmortizacionNIIFData", "ModificarEstadoFechaNIIF", ex);
                     
                    }
                }
            }
        }




        public void ConsultarCarteraNIIF(DateTime vFecha, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = vFecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pusuario = cmdTransaccionFactory.CreateParameter();
                        pusuario.ParameterName = "PUSUARIO";
                        pusuario.Value = vUsuario.codusuario;
                        pusuario.Direction = ParameterDirection.Input;
                        pusuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pusuario);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_COSTORIESGO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraNIIFData", "ListarCarteraNIIF", ex);
                    }
                }
            }
        }


          
        public List<CarteraNIF> ListarCarteraNIIF(DateTime vFecha,string Orden, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CarteraNIF> lstCarteraNIIF = new List<CarteraNIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {  
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        

                        Configuracion conf = new Configuracion();
                        string sql = "";    
                        if (Orden != "SIN_DATA")
                        {
                            sql = "Select * from CREDITO_COSTORIESGO Where fecha = To_Date('" + Convert.ToDateTime(vFecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') order by "+Orden;
                        }else
                        {
                            sql = "Select * from CREDITO_COSTORIESGO Where fecha = To_Date('" + Convert.ToDateTime(vFecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') order by numero_radicacion";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;  
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CarteraNIF entidad = new CarteraNIF();                            

                            //if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt32(resultado["CONSECUTIVO"]);
                            //if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["MONTO"] != DBNull.Value) entidad.monto = Convert.ToDecimal(resultado["MONTO"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToDecimal(resultado["CUOTA"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["DIAS_MORA"] != DBNull.Value) entidad.dias_mora = Convert.ToInt32(resultado["DIAS_MORA"]);
                            if (resultado["VALOR_TOTAL"] != DBNull.Value) entidad.valor_total = Convert.ToDecimal(resultado["VALOR_TOTAL"]);
                            if (resultado["VALOR_EXP"] != DBNull.Value) entidad.valor_exp = Convert.ToDecimal(resultado["VALOR_EXP"]);
                            if (resultado["PROBABILIDAD_INCUMP"] != DBNull.Value) entidad.probabilidad_incump = Convert.ToDecimal(resultado["PROBABILIDAD_INCUMP"]);
                            if (resultado["PERDIDA_ESPERADA"] != DBNull.Value) entidad.perdida_esperada = Convert.ToDecimal(resultado["PERDIDA_ESPERADA"]);
                            if (resultado["GARANTIA"] != DBNull.Value) entidad.garantia = Convert.ToString(resultado["GARANTIA"]);
                            if (resultado["PORCENTAJE_PDI"] != DBNull.Value) entidad.porcentaje_pdi = Convert.ToDecimal(resultado["PORCENTAJE_PDI"]);
                            if (resultado["TOTAL_AJUSTE"] != DBNull.Value) entidad.total_ajuste = Convert.ToDecimal(resultado["TOTAL_AJUSTE"]);
                            
                            lstCarteraNIIF.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCarteraNIIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraNIIFData", "ListarCarteraNIIF", ex);
                        return null;
                    }
                }

            }
        }



    }
}