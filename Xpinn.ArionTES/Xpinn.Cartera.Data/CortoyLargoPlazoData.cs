using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Cartera.Entities;
using Xpinn.Comun.Entities;

namespace Xpinn.Cartera.Data
{
    public class CortoyLargoPlazoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla  cierre histórico
        /// </summary>
        public CortoyLargoPlazoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public List<Cierea> ListarFechaCierre(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Cierea> lstFechaCierre = new List<Cierea>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select Distinct fecha From cierea Where tipo = 'R' Order by fecha desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cierea entidad = new Cierea();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"].ToString());
                            lstFechaCierre.Add(entidad);
                        }

                        return lstFechaCierre;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CortoyLargoPlazoData", "ListarFechaCierre", ex);
                        return null;
                    }
                }
            }
        }

                /// <summary>
        /// Listar créditos a refinanciar
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<CortoyLargoPlazo> ListarCredito(DateTime pFecha, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CortoyLargoPlazo> lstCredito = new List<CortoyLargoPlazo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (pFecha == System.DateTime.MinValue)
                        {
                            sql = @"Select c.cod_oficina, o.nombre As nom_oficina, c.cod_linea_credito, g.nombre As nom_linea, c.numero_radicacion, c.cod_deudor, p.identificacion, p.nombres, p.apellidos, c.saldo_capital, c.fecha_proximo_pago, c.cod_asesor_com, u.nombre As nom_asesor, c.monto_aprobado, c.valor_cuota, c.numero_cuotas, c.cod_periodicidad, TasaCredito(c.numero_radicacion) As tasa_interes, p.numero_dias, p.numero_meses 
                                    From credito c Inner Join v_persona p On c.cod_deudor = p.cod_persona Inner Join oficina o On c.cod_oficina = o.cod_oficina Inner Join lineascredito g On c.cod_linea_credito = g.cod_linea_credito Left Join usuarios u On c.cod_asesor_com = u.codusuario Left Join periodicidad p On c.cod_periodicidad = p.cod_periodicidad
                                    Where c.cod_linea_credito Not In (Select pl.cod_linea_credito from parametros_linea pl where pl.cod_parametro = 320)";

                            if (filtro.Trim() != "")
                            {
                                if (sql.ToLower().Contains("where"))
                                    sql += " And " + filtro;
                                else
                                    sql += " Where " + filtro;
                            }
                            sql += " Order by c.cod_oficina, c.cod_linea_credito, c.numero_radicacion";

                        }
                        else
                        {
                            Configuracion conf = new Configuracion();
                            sql = @"Select c.cod_oficina, o.nombre As nom_oficina, c.cod_linea_credito, g.nombre As nom_linea, c.numero_radicacion, c.cod_deudor, p.identificacion, p.nombres, p.apellidos, round(h.saldo_capital) SALDO_CAPITAL, h.fecha_proximo_pago, c.cod_asesor_com, u.nombre As nom_asesor, c.monto_aprobado,c.valor_cuota, c.numero_cuotas, c.cod_periodicidad, TasaCredito(c.numero_radicacion) as tasa_interes, p.numero_dias, p.numero_meses 
                                    From historico_cre h Inner Join credito c On h.numero_radicacion = c.numero_radicacion Inner Join v_persona p On c.cod_deudor = p.cod_persona Inner Join oficina o On c.cod_oficina = o.cod_oficina Inner Join lineascredito g On c.cod_linea_credito = g.cod_linea_credito Left Join usuarios u On c.cod_asesor_com = u.codusuario Left Join periodicidad p On c.cod_periodicidad = p.cod_periodicidad
                                    Where   h.fecha_historico = To_Date('" + pFecha.ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "')" +
                                   "And h.cod_linea_credito Not In (Select pl.cod_linea_credito from parametros_linea pl where pl.cod_parametro = 320)";
                            if (filtro.Trim() != "")
                            {
                                if (sql.ToLower().Contains("where"))
                                    sql += " And " + filtro;
                                else
                                    sql += " Where " + filtro;
                            }
                            sql += " Order by c.cod_oficina, c.cod_linea_credito, c.numero_radicacion";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CortoyLargoPlazo entidad = new CortoyLargoPlazo();
                            entidad.fecha = System.DateTime.Now;
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOM_LINEA"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToDouble(resultado["SALDO_CAPITAL"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.cod_asesor = Convert.ToInt64(resultado["COD_ASESOR_COM"]);
                            if (resultado["NOM_ASESOR"] != DBNull.Value) entidad.nom_asesor = Convert.ToString(resultado["NOM_ASESOR"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto_aprobado = Convert.ToDouble(resultado["MONTO_APROBADO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToDouble(resultado["VALOR_CUOTA"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt64(resultado["COD_PERIODICIDAD"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDouble(resultado["TASA_INTERES"]);
                            if (resultado["NUMERO_DIAS"] != DBNull.Value) entidad.numero_dias = Convert.ToDouble(resultado["NUMERO_DIAS"]);
                            if (resultado["NUMERO_MESES"] != DBNull.Value) entidad.numero_meses = Convert.ToDouble(resultado["NUMERO_MESES"]);

                            entidad.corto_plazo = 0;
                            entidad.largo_plazo = 0;

                            double n_meses_cred = entidad.plazo * entidad.numero_meses;
                            if (n_meses_cred <= 12)
                            {
                                entidad.corto_plazo = entidad.saldo_capital;
                                entidad.largo_plazo = 0;
                            }
                            else
                            {
                                Int64 cuotas = 0;
                                if (entidad.numero_meses != 0)
                                {
                                    double saldo_capital = entidad.saldo_capital;
                                    double interes = 0;
                                    double capital = 0;
                                    cuotas = Convert.ToInt64(12 / entidad.numero_meses);
                                    for (Int64 i = 1; i <= cuotas; i++)
                                    {
                                         interes = 0;
                                         capital = 0;

                                        interes = saldo_capital * (entidad.tasa_interes/100);
                                        capital = entidad.cuota - interes;
                                        if (saldo_capital < capital)
                                            capital = saldo_capital;
                                        entidad.corto_plazo += capital > 0 ? capital : 0 ; 
                                        saldo_capital = saldo_capital - capital;
                                        if (saldo_capital <= 0)
                                            i = cuotas + 1;
                                    }
                                    if (entidad.corto_plazo < entidad.saldo_capital)
                                        entidad.largo_plazo = entidad.saldo_capital - entidad.corto_plazo;
                                }
                            }

                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CortoyLargoPlazoData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }

    }
}
