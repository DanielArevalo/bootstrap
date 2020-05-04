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
    public class RepEdadesData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla  cierre histórico
        /// </summary>
        public RepEdadesData()
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
                        BOExcepcion.Throw("RepEdadesData", "ListarFechaCierre", ex);
                        return null;
                    }
                }
            }
        }

        public List<EdadMora> ListarRangos(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<EdadMora> lstCredito = new List<EdadMora>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select idrango, descripcion, dias_minimo, dias_maximo From rango_edad_cartera Order by 1";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            EdadMora entidad = new EdadMora();
                            if (resultado["IDRANGO"] != DBNull.Value) entidad.idrango = Convert.ToInt64(resultado["IDRANGO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["DIAS_MINIMO"] != DBNull.Value) entidad.dias_minimo = Convert.ToInt64(resultado["DIAS_MINIMO"]);
                            if (resultado["DIAS_MAXIMO"] != DBNull.Value) entidad.dias_maximo = Convert.ToInt64(resultado["DIAS_MAXIMO"]);                            
                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RepEdadesData", "ListarRangos", ex);
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
        public List<RepEdades> ListarCredito(DateTime pFecha, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<RepEdades> lstCredito = new List<RepEdades>();

            List<EdadMora> lstRangos = new List<EdadMora>();
            lstRangos = ListarRangos(pUsuario);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (pFecha == System.DateTime.MinValue)
                        {
                            sql = @"Select c.cod_oficina, o.nombre As nom_oficina, c.cod_linea_credito, g.nombre As nom_linea, c.numero_radicacion, c.cod_deudor, p.identificacion, p.nombres, p.apellidos, c.saldo_capital, c.fecha_proximo_pago, c.cod_asesor_com, u.nombre As nom_asesor, c.monto_aprobado, c.valor_cuota, Nvl(FecDifDia(c.fecha_proximo_pago, TRUNC(SYSDATE), 2), 0) As dias_mora 
                                    From credito c Inner Join v_persona p On c.cod_deudor = p.cod_persona Inner Join oficina o On c.cod_oficina = o.cod_oficina Inner Join lineascredito g On c.cod_linea_credito = g.cod_linea_credito Left Join usuarios u On c.cod_asesor_com = u.codusuario
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
                            sql = @"Select c.cod_oficina, o.nombre As nom_oficina, c.cod_linea_credito, g.nombre As nom_linea, c.numero_radicacion, c.cod_deudor, p.identificacion, p.nombres, p.apellidos, h.saldo_capital, h.fecha_proximo_pago, c.cod_asesor_com, u.nombre As nom_asesor, c.monto_aprobado, c.valor_cuota, h.dias_mora 
                                    From historico_cre h Inner Join credito c On h.numero_radicacion = c.numero_radicacion Inner Join v_persona p On c.cod_deudor = p.cod_persona Inner Join oficina o On c.cod_oficina = o.cod_oficina Inner Join lineascredito g On c.cod_linea_credito = g.cod_linea_credito Left Join usuarios u On c.cod_asesor_com = u.codusuario 
                                    Where h.fecha_historico = To_Date('" + pFecha.ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "')" +
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
                            RepEdades entidad = new RepEdades();
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
                            if (resultado["DIAS_MORA"] != DBNull.Value) entidad.dias_mora = Convert.ToInt64(resultado["DIAS_MORA"]);

                            entidad.clasificacion_mora_1 = 0;
                            entidad.clasificacion_mora_2 = 0;
                            entidad.clasificacion_mora_3 = 0;
                            entidad.clasificacion_mora_4 = 0;
                            entidad.clasificacion_mora_5 = 0;
                            entidad.clasificacion_mora_6 = 0;
                            entidad.clasificacion_mora_7 = 0;
                            entidad.clasificacion_mora_8 = 0;

                            int RangoIdx = 1;
                            foreach (EdadMora eRango in lstRangos)
                            {
                                if (eRango.dias_minimo <= entidad.dias_mora && eRango.dias_maximo >= entidad.dias_mora)
                                    if (RangoIdx == 1)
                                        entidad.clasificacion_mora_1 = entidad.saldo_capital;                                
                                    else if (RangoIdx == 2)
                                        entidad.clasificacion_mora_2 = entidad.saldo_capital;
                                    else if (RangoIdx == 3)
                                        entidad.clasificacion_mora_3 = entidad.saldo_capital;
                                    else if (RangoIdx == 4)
                                        entidad.clasificacion_mora_4 = entidad.saldo_capital;
                                    else if (RangoIdx == 5)
                                        entidad.clasificacion_mora_5 = entidad.saldo_capital;
                                    else if (RangoIdx == 6)
                                        entidad.clasificacion_mora_6 = entidad.saldo_capital;
                                    else if (RangoIdx == 7)
                                        entidad.clasificacion_mora_7 = entidad.saldo_capital;
                                    else if (RangoIdx == 8)
                                        entidad.clasificacion_mora_8 = entidad.saldo_capital;
                                RangoIdx += 1;
                            }

                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RepEdadesData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }
        
    }
}
