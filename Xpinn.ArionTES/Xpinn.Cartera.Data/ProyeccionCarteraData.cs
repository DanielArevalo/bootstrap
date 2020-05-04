using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Cartera.Entities;

namespace Xpinn.Cartera.Data
{
    public class ProyeccionCarteraData : GlobalData
    {

         protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla  cierre histórico
        /// </summary>
         public ProyeccionCarteraData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

         /// <summary>
         /// Método para consultar la proyección de cartera
         /// </summary>
         /// <param name="fecha"></param>
         /// <returns></returns>
         public List<ProyeccionCartera> ListarProyeccionCartera(DateTime pfecha, DateTime pfechafinal, Usuario pUsuario)
         {
             DbDataReader resultado = default(DbDataReader);
             List<ProyeccionCartera> listarchivo = new List<ProyeccionCartera>();

             using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
             {
                 using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                 {
                     try
                     {
                         Configuracion config = new Configuracion();
                         string sql = "";
                         if (dbConnectionFactory.TipoConexion() == "ORACLE")
                             sql = "Select c.fecha_historico, c.numero_radicacion, 0 As pagare, c.cod_oficina As oficina, p.identificacion, p.nombre, c.cod_linea_credito, c.fecha_aprobacion, c.fecha_terminacion, c.dias_mora, c.monto, c.saldo_capital, c.valor_cuota, c.fecha_proximo_pago, a.fecha_cuota, a.cod_atr, a.valor " +
                                   "From Historico_Cre c, Historico_Amortiza a, V_Persona p Where c.fecha_historico = a.fecha_historico And c.numero_radicacion = a.numero_radicacion And c.cod_cliente = p.cod_persona And c.fecha_historico = to_date('" + pfecha.ToString(config.ObtenerFormatoFecha()) + "','" + config.ObtenerFormatoFecha() + "') And c.estado != 'B' And c.saldo_capital != 0 " +
                                   "And a.fecha_cuota <= To_Date('" + pfechafinal.ToString(config.ObtenerFormatoFecha()) + "', '" + config.ObtenerFormatoFecha() + "') " +
                                   "Order by 1, 2";
                         else
                             sql = "Select c.fecha_historico, c.numero_radicacion, 0 As pagare, c.cod_oficina As oficina, p.identificacion, p.nombre, c.cod_linea_credito, c.fecha_aprobacion, c.fecha_terminacion, c.dias_mora, c.monto, c.saldo_capital, c.valor_cuota, c.fecha_proximo_pago, a.fecha_cuota, a.cod_atr, a.valor " +
                                   "From Historico_Cre c, Historico_Amortiza a, V_Persona p Where c.fecha_historico = a.fecha_historico And c.numero_radicacion = a.numero_radicacion And c.cod_cliente = p.cod_persona And c.fecha_historico = '" + pfecha.ToString(config.ObtenerFormatoFecha()) + "' And c.estado != 'B' And c.saldo_capital != 0 " +
                                   "And a.fecha_cuota <= '" + pfechafinal.ToString(config.ObtenerFormatoFecha()) + "' " +
                                   "Order by 1, 2";
                         connection.Open();
                         cmdTransaccionFactory.Connection = connection;
                         cmdTransaccionFactory.CommandType = CommandType.Text;
                         cmdTransaccionFactory.CommandText = sql;
                         resultado = cmdTransaccionFactory.ExecuteReader();

                         while (resultado.Read())
                         {
                             ProyeccionCartera entidad = new ProyeccionCartera();
                             if (resultado["FECHA_HISTORICO"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA_HISTORICO"]);
                             if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                             if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToInt64(resultado["OFICINA"]);
                             if (resultado["PAGARE"] != DBNull.Value) entidad.pagare = Convert.ToString(resultado["PAGARE"]);
                             if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                             if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                             if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                             if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                             if (resultado["FECHA_TERMINACION"] != DBNull.Value) entidad.fecha_terminacion = Convert.ToDateTime(resultado["FECHA_TERMINACION"]);
                             if (resultado["DIAS_MORA"] != DBNull.Value) entidad.dias_mora = Convert.ToInt64(resultado["DIAS_MORA"]);
                             if (resultado["MONTO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO"]);
                             if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                             if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                             if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                             if (resultado["FECHA_CUOTA"] != DBNull.Value) entidad.fecha_cuota = Convert.ToDateTime(resultado["FECHA_CUOTA"]);
                             if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["COD_ATR"]);
                             if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDouble(resultado["VALOR"]);

                             listarchivo.Add(entidad);
                         }

                         return listarchivo;
                     }
                     catch (Exception ex)
                     {
                         BOExcepcion.Throw("ProyeccionCartera", "ListarProyeccionCartera", ex);
                         return null;
                     }
                 }
             }
         }

         public Int64 ValidarProyeccionCartera(DateTime pfecha, Usuario pUsuario)
         {
             Int64 numero = 0;
             DbDataReader resultado = default(DbDataReader);

             using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
             {
                 using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                 {
                     try
                     {
                         Configuracion config = new Configuracion();
                         string sql = "";
                         if (dbConnectionFactory.TipoConexion() == "ORACLE")
                             sql = "Select Count(*) As numero " +
                                   "From Historico_Amortiza a Where a.fecha_historico = to_date('" + pfecha.ToString(config.ObtenerFormatoFecha()) + "','" + config.ObtenerFormatoFecha() + "') ";
                         else
                             sql = "Select Count(*) As numero " +
                                   "From Historico_Amortiza a Where a.fecha_historico = '" + pfecha.ToString(config.ObtenerFormatoFecha()) + "' ";
                         connection.Open();
                         cmdTransaccionFactory.Connection = connection;
                         cmdTransaccionFactory.CommandType = CommandType.Text;
                         cmdTransaccionFactory.CommandText = sql;
                         resultado = cmdTransaccionFactory.ExecuteReader();

                         if (resultado.Read())
                         {
                             if (resultado["NUMERO"] != DBNull.Value) numero = Convert.ToInt64(resultado["NUMERO"]);
                         }

                         return numero;
                     }
                     catch (Exception ex)
                     {
                         BOExcepcion.Throw("ProyeccionCartera", "ValidarProyeccionCartera", ex);
                         return 0;
                     }
                 }
             }
         }


         public void Proyeccion(DateTime pfecha, Usuario pUsuario, ref string serror)
         {
             serror = "";

             using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
             {
                 using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                 {
                     try
                     {
                         DbParameter PFECHA = cmdTransaccionFactory.CreateParameter();
                         PFECHA.ParameterName = "PFECHA";
                         PFECHA.Value = pfecha;
                         PFECHA.DbType = DbType.Date;

                         cmdTransaccionFactory.Parameters.Add(PFECHA);

                         connection.Open();
                         cmdTransaccionFactory.Connection = connection;
                         cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                         cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_PROYECCIONCARTER";
                         cmdTransaccionFactory.ExecuteNonQuery();
                         dbConnectionFactory.CerrarConexion(connection);
                     }
                     catch (Exception ex)
                     {
                         connection.Close();
                         serror = ex.Message;
                         return;
                     }
                 }
             }
         }


    }
}
