using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Programado.Entities;
using System.Data.Common;
using System.Data;

namespace Xpinn.Programado.Data
{
    public class CierreCuentaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public CierreCuentaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<CuentasProgramado> ListarProgramadoReporteCierre(DateTime pFecha, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<CuentasProgramado> lstProgramado = new List<CuentasProgramado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT h.cod_oficina,p.cod_persona,p.Identificacion,p.apellidos,p.nombres,h.numero_programado,h.cod_linea_programado,l.nombre as nom_linea_programado,"
                                    + "a.fecha_apertura,h.valor_cuota,h.saldo_total,h.fecha_proximo_pago,h.fecha_ultimo_pago,d.descripcion periodicidad,a.tasa_interes,a.fecha_interes,"
                                    + "h.interes_causado,h.estado "
                                    + "from historico_programado h inner join ahorro_programado a on h.numero_programado=a.numero_programado "
                                    + "inner join v_persona p on a.cod_persona = p.cod_persona "
                                    + "Left join lineaprogramado l on h.cod_linea_programado=l.cod_linea_programado "
                                    + "Left join periodicidad d on h.cod_periodicidad_int = d.cod_periodicidad ";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = sql + " where h.fecha_historico = to_date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "')";
                        else
                            sql = sql + " where h.fecha_historico = '" + pFecha + "'";

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CuentasProgramado entidad = new CuentasProgramado();
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["NUMERO_PROGRAMADO"] != DBNull.Value) entidad.numero_programado = Convert.ToString(resultado["NUMERO_PROGRAMADO"]);
                            if (resultado["COD_LINEA_PROGRAMADO"] != DBNull.Value) entidad.cod_linea_programado = Convert.ToString(resultado["COD_LINEA_PROGRAMADO"]);
                            if (resultado["NOM_LINEA_PROGRAMADO"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA_PROGRAMADO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.nom_periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["INTERES_CAUSADO"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["INTERES_CAUSADO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            lstProgramado.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProgramado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreCuentaData", "ListarProgramadoReporteCierre", ex);
                        return null;
                    }
                }
            }
        }

    }
}
