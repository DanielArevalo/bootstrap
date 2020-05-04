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

    public class AvanceData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;


        public AvanceData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public Avance CrearCreditoAvance(Avance pAvance, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidavance = cmdTransaccionFactory.CreateParameter();
                        pidavance.ParameterName = "p_idavance";
                        pidavance.Value = pAvance.idavance;
                        pidavance.Direction = ParameterDirection.Output;
                        pidavance.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidavance);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pAvance.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pfecha_solicitud = cmdTransaccionFactory.CreateParameter();
                        pfecha_solicitud.ParameterName = "p_fecha_solicitud";
                        pfecha_solicitud.Value = pAvance.fecha_solicitud;
                        pfecha_solicitud.Direction = ParameterDirection.Input;
                        pfecha_solicitud.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_solicitud);

                        DbParameter pfecha_aprobacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_aprobacion.ParameterName = "p_fecha_aprobacion";
                        if (pAvance.fecha_aprobacion != DateTime.MinValue) pfecha_aprobacion.Value = pAvance.fecha_aprobacion; else pfecha_aprobacion.Value = DBNull.Value;
                        pfecha_aprobacion.Direction = ParameterDirection.Input;
                        pfecha_aprobacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_aprobacion);

                        DbParameter pfecha_desembolso = cmdTransaccionFactory.CreateParameter();
                        pfecha_desembolso.ParameterName = "p_fecha_desembolso";
                        if (pAvance.fecha_desembolso != DateTime.MinValue) pfecha_desembolso.Value = pAvance.fecha_desembolso; else pfecha_desembolso.Value = DBNull.Value;
                        pfecha_desembolso.Direction = ParameterDirection.Input;
                        pfecha_desembolso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_desembolso);

                        DbParameter pvalor_solicitado = cmdTransaccionFactory.CreateParameter();
                        pvalor_solicitado.ParameterName = "p_valor_solicitado";
                        pvalor_solicitado.Value = pAvance.valor_solicitado;
                        pvalor_solicitado.Direction = ParameterDirection.Input;
                        pvalor_solicitado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_solicitado);

                        DbParameter pvalor_aprobado = cmdTransaccionFactory.CreateParameter();
                        pvalor_aprobado.ParameterName = "p_valor_aprobado";
                        if (pAvance.valor_aprobado != 0) pvalor_aprobado.Value = pAvance.valor_aprobado; else pvalor_aprobado.Value = DBNull.Value;
                        pvalor_aprobado.Direction = ParameterDirection.Input;
                        pvalor_aprobado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_aprobado);

                        DbParameter pvalor_desembolsado = cmdTransaccionFactory.CreateParameter();
                        pvalor_desembolsado.ParameterName = "p_valor_desembolsado";
                        if (pAvance.valor_desembolsado != 0) pvalor_desembolsado.Value = pAvance.valor_desembolsado; else pvalor_desembolsado.Value = DBNull.Value;
                        pvalor_desembolsado.Direction = ParameterDirection.Input;
                        pvalor_desembolsado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_desembolsado);

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "p_forma_pago";
                        pforma_pago.Value = pAvance.forma_pago;
                        pforma_pago.Direction = ParameterDirection.Input;
                        pforma_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pAvance.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pobservacion = cmdTransaccionFactory.CreateParameter();
                        pobservacion.ParameterName = "p_observacion";
                        if (pAvance.observacion != null) pobservacion.Value = pAvance.observacion; else pobservacion.Value = DBNull.Value;
                        pobservacion.Direction = ParameterDirection.Input;
                        pobservacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservacion);


                        DbParameter P_VALOR_CUOTA = cmdTransaccionFactory.CreateParameter();
                        P_VALOR_CUOTA.ParameterName = "P_VALOR_CUOTA";
                        if (pAvance.valor_cuota != 0) P_VALOR_CUOTA.Value = pAvance.valor_cuota; else P_VALOR_CUOTA.Value = DBNull.Value;
                        P_VALOR_CUOTA.Direction = ParameterDirection.Input;
                        P_VALOR_CUOTA.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_VALOR_CUOTA);


                        DbParameter P_PLAZO = cmdTransaccionFactory.CreateParameter();
                        P_PLAZO.ParameterName = "P_PLAZO";
                        if (pAvance.plazo_diferir != null) P_PLAZO.Value = pAvance.plazo_diferir; else P_VALOR_CUOTA.Value = DBNull.Value;
                        P_PLAZO.Direction = ParameterDirection.Input;
                        P_PLAZO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_PLAZO);


                        DbParameter P_FORMA_TASA = cmdTransaccionFactory.CreateParameter();
                        P_FORMA_TASA.ParameterName = "P_FORMA_TASA";
                        if (pAvance.forma_tasa != 0 && pAvance.forma_tasa != null)
                            P_FORMA_TASA.Value = pAvance.forma_tasa;
                        else
                            P_FORMA_TASA.Value = DBNull.Value;
                        P_FORMA_TASA.Direction = ParameterDirection.Input;
                        P_FORMA_TASA.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_FORMA_TASA);

                        DbParameter P_TIPO_HISTORICO = cmdTransaccionFactory.CreateParameter();
                        P_TIPO_HISTORICO.ParameterName = "P_TIPO_HISTORICO";
                        if (pAvance.tipo_historico != 0 && pAvance.tipo_historico != null)
                            P_TIPO_HISTORICO.Value = pAvance.tipo_historico;

                        else
                            P_TIPO_HISTORICO.Value = DBNull.Value;
                        P_TIPO_HISTORICO.Direction = ParameterDirection.Input;
                        P_TIPO_HISTORICO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_TIPO_HISTORICO);

                        DbParameter P_DESVIACION = cmdTransaccionFactory.CreateParameter();
                        P_DESVIACION.ParameterName = "P_DESVIACION";
                        if (pAvance.desviacion != 0 && pAvance.desviacion != null)
                            P_DESVIACION.Value = pAvance.desviacion;
                        else
                            P_DESVIACION.Value = DBNull.Value;
                        P_DESVIACION.Direction = ParameterDirection.Input;
                        P_DESVIACION.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_DESVIACION);


                        DbParameter P_TIPO_TASA = cmdTransaccionFactory.CreateParameter();
                        P_TIPO_TASA.ParameterName = "P_TIPO_TASA";
                        if (pAvance.tipo_tasa != 0 && pAvance.tipo_tasa != null)
                            P_TIPO_TASA.Value = pAvance.tipo_tasa;
                        else
                            P_TIPO_TASA.Value = DBNull.Value;
                        P_TIPO_TASA.Direction = ParameterDirection.Input;
                        P_TIPO_TASA.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_TIPO_TASA);

                        DbParameter P_TASA_INTERES = cmdTransaccionFactory.CreateParameter();
                        P_TASA_INTERES.ParameterName = "P_TASA_INTERES";
                        if (pAvance.tasa != 0 && pAvance.tasa != null)
                            P_TASA_INTERES.Value = pAvance.tasa;
                        else
                            P_TASA_INTERES.Value = DBNull.Value;
                        P_TASA_INTERES.Direction = ParameterDirection.Input;
                        P_TASA_INTERES.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_TASA_INTERES);

                        DbParameter P_SALDO_AVANCE = cmdTransaccionFactory.CreateParameter();
                        P_SALDO_AVANCE.ParameterName = "P_SALDO_AVANCE";
                        if (pAvance.saldo_avance != 0) P_SALDO_AVANCE.Value = pAvance.saldo_avance; else P_SALDO_AVANCE.Value = DBNull.Value;
                        P_SALDO_AVANCE.Direction = ParameterDirection.Input;
                        P_SALDO_AVANCE.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_SALDO_AVANCE);


                        DbParameter P_FECHA_PROXIMO_PAGO = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_PROXIMO_PAGO.ParameterName = "P_FECHA_PROXIMO_PAGO";
                        P_FECHA_PROXIMO_PAGO.Value = pAvance.fecha_solicitud;
                        P_FECHA_PROXIMO_PAGO.Direction = ParameterDirection.Input;
                        P_FECHA_PROXIMO_PAGO.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_PROXIMO_PAGO);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CREDAVANCE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);


                        pAvance.idavance = Convert.ToInt32(pidavance.Value);
                        return pAvance;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "CrearCreditoAvance", ex);
                        return null;
                    }
                }
            }
        }
        public void EliminarCreditoAvance(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pidavance = cmdTransaccionFactory.CreateParameter();
                        pidavance.ParameterName = "p_idavance";
                        pidavance.Value = pId;
                        pidavance.Direction = ParameterDirection.Input;
                        pidavance.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidavance);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "EliminarCreditoAvance", ex);
                    }
                }
            }
        }

        public List<Avance> ListarCreditoRotativos(Avance pRotativo, DateTime pFecha, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Avance> lstCredito = new List<Avance>();
            Configuracion conf = new Configuracion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select c.cod_deudor, c.numero_radicacion, l.nombre as nomlinea,c.fecha_aprobacion,p.identificacion, p.cod_nomina, "
                                        + "p.primer_nombre||' '||p.segundo_nombre||' '||p.primer_apellido||' '||p.segundo_apellido as Nombre, "
                                        + "c.monto_aprobado as cupototal ,c.monto_aprobado -c.saldo_capital as cupodisponible, o.nombre as nomoficina,c.valor_cuota  "
                                        + "from credito c inner join lineascredito l on l.cod_linea_credito = c.cod_linea_credito "
                                        + "inner join persona p on p.cod_persona = c.cod_deudor "
                                        + "inner join oficina o on c.cod_oficina = o.cod_oficina " + (filtro.ToUpper().Contains("WHERE") ? "" : " Where c.numero_radicacion != 0 ") +  filtro;


                        if (pFecha != null && pFecha != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " c.fecha_aprobacion = To_Date('" + Convert.ToDateTime(pFecha
                                    ).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " c.fecha_aprobacion = '" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        sql += "Order by c.numero_radicacion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Avance entidad = new Avance();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["NOMLINEA"] != DBNull.Value) entidad.nomlinea = Convert.ToString(resultado["NOMLINEA"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["CUPOTOTAL"] != DBNull.Value) entidad.cupototal = Convert.ToDecimal(resultado["CUPOTOTAL"]);
                            if (resultado["CUPODISPONIBLE"] != DBNull.Value) entidad.cupodisponible = Convert.ToDecimal(resultado["CUPODISPONIBLE"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOMOFICINA"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["valor_cuota"]);
                            if (resultado["cod_deudor"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["cod_deudor"]);

                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "ListarCreditoRotativos", ex);
                        return null;
                    }
                }
            }
        }



        public Avance ConsultarCreditoRotativo(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Avance entidad = new Avance();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select c.numero_radicacion,c.cod_deudor, l.nombre as nomlinea,c.fecha_aprobacion,c.monto_aprobado,c.monto_desembolsado, p.identificacion, "
                                       + "p.primer_nombre||' '||p.segundo_nombre||' '||p.primer_apellido||' '||p.segundo_apellido as Nombre, "
                                       + "c.monto_aprobado as cupototal , c.monto_aprobado -c.saldo_capital as cupodisponible, o.nombre as nomoficina, "
                                       + "case c.forma_pago when 'l' then 'Caja' when '2' then 'Nomina' when 'C' then 'Caja' when 'N' then 'Nomina' end as forma_pago "
                                       + "from credito c inner join lineascredito l on l.cod_linea_credito = c.cod_linea_credito "
                                       + "inner join persona p on p.cod_persona = c.cod_deudor "
                                       + "inner join oficina o on c.cod_oficina = o.cod_oficina where NUMERO_RADICACION = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["NOMLINEA"] != DBNull.Value) entidad.nomlinea = Convert.ToString(resultado["NOMLINEA"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["CUPOTOTAL"] != DBNull.Value) entidad.cupototal = Convert.ToDecimal(resultado["CUPOTOTAL"]);
                            if (resultado["CUPODISPONIBLE"] != DBNull.Value) entidad.cupodisponible = Convert.ToDecimal(resultado["CUPODISPONIBLE"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOMOFICINA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.descforma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "ConsultarCreditoRotativo", ex);
                        return null;
                    }
                }
            }
        }
        public Avance ConsultarCuotaCredito(Avance avance, Usuario pUsuario)
        {
            DbDataReader resultado;
            Avance entidad = new Avance();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select sum(valor) as valor_cuota  from planpagos where NUMERO_RADICACION = " + avance.numero_radicacion.ToString() + " and numerocuota = " + avance.cuotas_pagadas;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["valor_cuota"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "ConsultarCreditoRotativo", ex);
                        return null;
                    }
                }
            }
        }

        public Avance ConsultarCredRotativoXaprobarXCredito(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Avance entidad = new Avance();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select c.saldo_capital,c.fecha_proximo_pago,  c.cuotas_pagadas, c.cod_linea_credito, c.numero_radicacion,c.cod_deudor, l.nombre as nomlinea,c.fecha_aprobacion, p.identificacion, "
                                       + "p.primer_nombre||' '||p.segundo_nombre||' '||p.primer_apellido||' '||p.segundo_apellido as Nombre, "
                                       + "c.monto_aprobado as cupototal , c.monto_aprobado -c.saldo_capital as cupodisponible, o.nombre as nomoficina, "
                                       + "case c.forma_pago when 'l' then 'NOSE' when 'C' then 'Caja' when 'N' then 'Nomina' end as forma_pago, "
                                       + "a.fecha_solicitud,a.valor_solicitado,a.idavance,a.valor_aprobado,a.observacion,l.aprobar_avances,a.plazo"
                                       + "  from credito c inner join lineascredito l on l.cod_linea_credito = c.cod_linea_credito "
                                       + "inner join persona p on p.cod_persona = c.cod_deudor "
                                       + "inner join oficina o on c.cod_oficina = o.cod_oficina "
                                       + "inner join credito_avance a on a.numero_radicacion = c.numero_radicacion where a.numero_radicacion = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["NOMLINEA"] != DBNull.Value) entidad.nomlinea = Convert.ToString(resultado["NOMLINEA"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["CUPOTOTAL"] != DBNull.Value) entidad.cupototal = Convert.ToDecimal(resultado["CUPOTOTAL"]);
                            if (resultado["CUPODISPONIBLE"] != DBNull.Value) entidad.cupodisponible = Convert.ToDecimal(resultado["CUPODISPONIBLE"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOMOFICINA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.descforma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["VALOR_SOLICITADO"] != DBNull.Value) entidad.valor_solicitado = Convert.ToDecimal(resultado["VALOR_SOLICITADO"]);
                            if (resultado["IDAVANCE"] != DBNull.Value) entidad.idavance = Convert.ToInt32(resultado["IDAVANCE"]);
                            if (resultado["VALOR_APROBADO"] != DBNull.Value) entidad.valor_aprobado = Convert.ToDecimal(resultado["VALOR_APROBADO"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["APROBAR_AVANCES"] != DBNull.Value) entidad.aprobar_avances = Convert.ToInt64(resultado["APROBAR_AVANCES"]);
                            if (resultado["APROBAR_AVANCES"] != DBNull.Value) entidad.aprobar_avances = Convert.ToInt64(resultado["APROBAR_AVANCES"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo_diferir = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["fecha_proximo_pago"]);
                            if (resultado["saldo_capital"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["saldo_capital"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "ConsultarCredRotativoXaprobar", ex);
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
                        string sql = "select max(idavance)+1 from CREDITO_AVANCE ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());
                        if (resultado == 0)
                        {
                            resultado = 1;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "ObtenerSiguienteCodigo", ex);
                        return -1;
                    }
                }
            }
        }



        //LISTAR CREDITOS POR APROBAR

        public Avance ModificarCreditoAvance(Avance pAvance, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidavance = cmdTransaccionFactory.CreateParameter();
                        pidavance.ParameterName = "p_idavance";
                        pidavance.Value = pAvance.idavance;
                        pidavance.Direction = ParameterDirection.Input;
                        pidavance.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidavance);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pAvance.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pfecha_solicitud = cmdTransaccionFactory.CreateParameter();
                        pfecha_solicitud.ParameterName = "p_fecha_solicitud";
                        pfecha_solicitud.Value = pAvance.fecha_solicitud;
                        pfecha_solicitud.Direction = ParameterDirection.Input;
                        pfecha_solicitud.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_solicitud);

                        DbParameter pfecha_aprobacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_aprobacion.ParameterName = "p_fecha_aprobacion";
                        if (pAvance.fecha_aprobacion != DateTime.MinValue) pfecha_aprobacion.Value = pAvance.fecha_aprobacion; else pfecha_aprobacion.Value = DBNull.Value;
                        pfecha_aprobacion.Direction = ParameterDirection.Input;
                        pfecha_aprobacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_aprobacion);

                        DbParameter pfecha_desembolso = cmdTransaccionFactory.CreateParameter();
                        pfecha_desembolso.ParameterName = "p_fecha_desembolso";
                        if (pAvance.fecha_desembolso != DateTime.MinValue) pfecha_desembolso.Value = pAvance.fecha_desembolso; else pfecha_desembolso.Value = DBNull.Value;
                        pfecha_desembolso.Direction = ParameterDirection.Input;
                        pfecha_desembolso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_desembolso);

                        DbParameter pvalor_solicitado = cmdTransaccionFactory.CreateParameter();
                        pvalor_solicitado.ParameterName = "p_valor_solicitado";
                        pvalor_solicitado.Value = pAvance.valor_solicitado;
                        pvalor_solicitado.Direction = ParameterDirection.Input;
                        pvalor_solicitado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_solicitado);

                        DbParameter pvalor_aprobado = cmdTransaccionFactory.CreateParameter();
                        pvalor_aprobado.ParameterName = "p_valor_aprobado";
                        if (pAvance.valor_aprobado != 0) pvalor_aprobado.Value = pAvance.valor_aprobado; else pvalor_aprobado.Value = DBNull.Value;
                        pvalor_aprobado.Direction = ParameterDirection.Input;
                        pvalor_aprobado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_aprobado);

                        DbParameter pvalor_desembolsado = cmdTransaccionFactory.CreateParameter();
                        pvalor_desembolsado.ParameterName = "p_valor_desembolsado";
                        if (pAvance.valor_desembolsado != 0) pvalor_desembolsado.Value = pAvance.valor_desembolsado; else pvalor_desembolsado.Value = DBNull.Value;
                        pvalor_desembolsado.Direction = ParameterDirection.Input;
                        pvalor_desembolsado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_desembolsado);

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "p_forma_pago";
                        pforma_pago.Value = pAvance.forma_pago;
                        pforma_pago.Direction = ParameterDirection.Input;
                        pforma_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pAvance.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pobservacion = cmdTransaccionFactory.CreateParameter();
                        pobservacion.ParameterName = "p_observacion";
                        if (pAvance.observacion != null) pobservacion.Value = pAvance.observacion; else pobservacion.Value = DBNull.Value;
                        pobservacion.Direction = ParameterDirection.Input;
                        pobservacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CREDAVANCE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);


                        return pAvance;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "ModificarCreditoAvance", ex);
                        return null;
                    }
                }
            }
        }


        public Avance GrabarAprobacionDavance(Avance pAvance, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidavance = cmdTransaccionFactory.CreateParameter();
                        pidavance.ParameterName = "p_idavance";
                        pidavance.Value = pAvance.idavance;
                        pidavance.Direction = ParameterDirection.Input;
                        pidavance.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidavance);

                        DbParameter pfecha_aprobacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_aprobacion.ParameterName = "p_fecha_aprobacion";
                        if (pAvance.fecha_aprobacion != DateTime.MinValue) pfecha_aprobacion.Value = pAvance.fecha_aprobacion; else pfecha_aprobacion.Value = DBNull.Value;
                        pfecha_aprobacion.Direction = ParameterDirection.Input;
                        pfecha_aprobacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_aprobacion);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pAvance.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_APROAVANC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pAvance;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "GrabarAprobacionDavance", ex);
                        return null;
                    }
                }
            }
        }


        public List<Avance> ListarCreditoXaprobar(Avance pRotativo, DateTime pFecha, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Avance> lstCredito = new List<Avance>();
            Configuracion conf = new Configuracion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select c.cod_deudor,a.numero_radicacion, l.nombre as nomlinea,p.identificacion, p.primer_nombre||' '||p.segundo_nombre||' '||p.primer_apellido||' '||p.segundo_apellido as Nombre, "
                                        + "p.cod_nomina, c.monto_aprobado as cupototal , c.monto_aprobado -c.saldo_capital as cupodisponible, "
                                        + "a.idavance,a.fecha_solicitud,a.valor_solicitado,a.observacion,a.fecha_aprobacion, a.valor_aprobado,l.aprobar_avances "
                                        + "from credito c inner join lineascredito l on l.cod_linea_credito = c.cod_linea_credito "
                                        + "inner join persona p on p.cod_persona = c.cod_deudor "
                                        + "inner join credito_avance a on a.numero_radicacion = c.numero_radicacion " + filtro;

                        if (pFecha != null && pFecha != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " a.fecha_solicitud = To_Date('" + Convert.ToDateTime(pFecha
                                    ).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " a.fecha_solicitud = '" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        sql += "Order by c.numero_radicacion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Avance entidad = new Avance();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["NOMLINEA"] != DBNull.Value) entidad.nomlinea = Convert.ToString(resultado["NOMLINEA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["CUPOTOTAL"] != DBNull.Value) entidad.cupototal = Convert.ToDecimal(resultado["CUPOTOTAL"]);
                            if (resultado["CUPODISPONIBLE"] != DBNull.Value) entidad.cupodisponible = Convert.ToDecimal(resultado["CUPODISPONIBLE"]);
                            if (resultado["IDAVANCE"] != DBNull.Value) entidad.idavance = Convert.ToInt32(resultado["IDAVANCE"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["VALOR_SOLICITADO"] != DBNull.Value) entidad.valor_solicitado = Convert.ToDecimal(resultado["VALOR_SOLICITADO"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["VALOR_APROBADO"] != DBNull.Value) entidad.valor_aprobado = Convert.ToDecimal(resultado["VALOR_APROBADO"]);
                            if (resultado["cod_deudor"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["cod_deudor"]);
                            if (resultado["APROBAR_AVANCES"] != DBNull.Value) entidad.aprobar_avances = Convert.ToInt64(resultado["APROBAR_AVANCES"]);
                            if (entidad.aprobar_avances == 1)
                            {
                                entidad.aprobar_avance = "No necesita aprobación";
                            }
                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "ListarCreditoXaprobar", ex);
                        return null;
                    }
                }
            }
        }

        public Avance ConsultarCredRotativoXaprobar(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Avance entidad = new Avance();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select c.fecha_proximo_pago,  c.cuotas_pagadas, c.cod_linea_credito, c.numero_radicacion,c.cod_deudor, l.nombre as nomlinea,c.fecha_aprobacion, p.identificacion, "
                                       + "p.primer_nombre||' '||p.segundo_nombre||' '||p.primer_apellido||' '||p.segundo_apellido as Nombre, "
                                       + "c.monto_aprobado as cupototal , c.monto_aprobado -c.saldo_capital as cupodisponible, o.nombre as nomoficina, "
                                       + "case c.forma_pago when 'l' then 'NOSE' when 'C' then 'Caja' when 'N' then 'Nomina' end as forma_pago, "
                                       + "a.fecha_solicitud,a.valor_solicitado,a.idavance,a.valor_aprobado,a.observacion,l.aprobar_avances,a.plazo, c.cod_deudor "
                                       + "  from credito c inner join lineascredito l on l.cod_linea_credito = c.cod_linea_credito "
                                       + "inner join persona p on p.cod_persona = c.cod_deudor "
                                       + "inner join oficina o on c.cod_oficina = o.cod_oficina "
                                       + "inner join credito_avance a on a.numero_radicacion = c.numero_radicacion where a.idavance = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["NOMLINEA"] != DBNull.Value) entidad.nomlinea = Convert.ToString(resultado["NOMLINEA"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["CUPOTOTAL"] != DBNull.Value) entidad.cupototal = Convert.ToDecimal(resultado["CUPOTOTAL"]);
                            if (resultado["CUPODISPONIBLE"] != DBNull.Value) entidad.cupodisponible = Convert.ToDecimal(resultado["CUPODISPONIBLE"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOMOFICINA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.descforma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["VALOR_SOLICITADO"] != DBNull.Value) entidad.valor_solicitado = Convert.ToDecimal(resultado["VALOR_SOLICITADO"]);
                            if (resultado["IDAVANCE"] != DBNull.Value) entidad.idavance = Convert.ToInt32(resultado["IDAVANCE"]);
                            if (resultado["VALOR_APROBADO"] != DBNull.Value) entidad.valor_aprobado = Convert.ToDecimal(resultado["VALOR_APROBADO"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["APROBAR_AVANCES"] != DBNull.Value) entidad.aprobar_avances = Convert.ToInt64(resultado["APROBAR_AVANCES"]);
                            if (resultado["APROBAR_AVANCES"] != DBNull.Value) entidad.aprobar_avances = Convert.ToInt64(resultado["APROBAR_AVANCES"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo_diferir = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["fecha_proximo_pago"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
        
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "ConsultarCredRotativoXaprobar", ex);
                        return null;
                    }
                }
            }
        }
        public Avance fecha_ult_avance(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Avance entidad = new Avance();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select max(fecha_desembolso) as fecha_desembolso  from credito_avance where estado='C' and NUMERO_RADICACION = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["fecha_desembolso"] != DBNull.Value) entidad.fecha_ult_avance = Convert.ToDateTime(resultado["fecha_desembolso"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "fecha_ult_avance", ex);
                        return null;
                    }
                }
            }
        }


        public Avance ValidarNumeroRadicacion(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Avance entidad = new Avance();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from credito_avance where numero_radicacion = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "ValidarNumeroRadicacion", ex);
                        return null;
                    }
                }
            }
        }



        public ControlCreditos CrearControlCreditos(ControlCreditos pControl, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcontrol = cmdTransaccionFactory.CreateParameter();
                        pidcontrol.ParameterName = "p_idcontrol";
                        pidcontrol.Value = pControl.idcontrol;
                        pidcontrol.Direction = ParameterDirection.Output;
                        pidcontrol.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcontrol);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pControl.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pcodtipoproceso = cmdTransaccionFactory.CreateParameter();
                        pcodtipoproceso.ParameterName = "p_codtipoproceso";
                        pcodtipoproceso.Value = pControl.codtipoproceso;
                        pcodtipoproceso.Direction = ParameterDirection.Input;
                        pcodtipoproceso.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcodtipoproceso);

                        DbParameter pfechaproceso = cmdTransaccionFactory.CreateParameter();
                        pfechaproceso.ParameterName = "p_fechaproceso";
                        pfechaproceso.Value = pControl.fechaproceso;
                        pfechaproceso.Direction = ParameterDirection.Input;
                        pfechaproceso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaproceso);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = vUsuario.codusuario;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pcod_motivo = cmdTransaccionFactory.CreateParameter();
                        pcod_motivo.ParameterName = "p_cod_motivo";
                        if (pControl.cod_motivo != 0) pcod_motivo.Value = pControl.cod_motivo; else pcod_motivo.Value = DBNull.Value;
                        pcod_motivo.Direction = ParameterDirection.Input;
                        pcod_motivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_motivo);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        if (pControl.observaciones != null) pobservaciones.Value = pControl.observaciones; else pobservaciones.Value = DBNull.Value;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        DbParameter panexos = cmdTransaccionFactory.CreateParameter();
                        panexos.ParameterName = "p_anexos";
                        if (pControl.anexos != null) panexos.Value = pControl.anexos; else panexos.Value = DBNull.Value;
                        panexos.Direction = ParameterDirection.Input;
                        panexos.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(panexos);

                        DbParameter pnivel = cmdTransaccionFactory.CreateParameter();
                        pnivel.ParameterName = "p_nivel";
                        if (pControl.nivel != 0) pnivel.Value = pControl.nivel; else pnivel.Value = DBNull.Value;
                        pnivel.Direction = ParameterDirection.Input;
                        pnivel.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnivel);

                        DbParameter pfecha_consulta_dat = cmdTransaccionFactory.CreateParameter();
                        pfecha_consulta_dat.ParameterName = "p_fecha_consulta_dat";
                        if (pControl.fechaconsulta_dat != DateTime.MinValue) pfecha_consulta_dat.Value = pControl.fechaconsulta_dat; else pfecha_consulta_dat.Value = DBNull.Value;
                        pfecha_consulta_dat.Direction = ParameterDirection.Input;
                        pfecha_consulta_dat.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_consulta_dat);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CONTROLCRE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pControl.idcontrol = Convert.ToInt32(pidcontrol.Value);
                        return pControl;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "CrearControlCreditos", ex);
                        return null;
                    }
                }
            }
        }
        public ControlCreditos ModificarControlCreditos(ControlCreditos pControl, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcontrol = cmdTransaccionFactory.CreateParameter();
                        pidcontrol.ParameterName = "p_idcontrol";
                        pidcontrol.Value = pControl.idcontrol;
                        pidcontrol.Direction = ParameterDirection.Input;
                        pidcontrol.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcontrol);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pControl.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pcodtipoproceso = cmdTransaccionFactory.CreateParameter();
                        pcodtipoproceso.ParameterName = "p_codtipoproceso";
                        pcodtipoproceso.Value = pControl.codtipoproceso;
                        pcodtipoproceso.Direction = ParameterDirection.Input;
                        pcodtipoproceso.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcodtipoproceso);

                        DbParameter pfechaproceso = cmdTransaccionFactory.CreateParameter();
                        pfechaproceso.ParameterName = "p_fechaproceso";
                        pfechaproceso.Value = pControl.fechaproceso;
                        pfechaproceso.Direction = ParameterDirection.Input;
                        pfechaproceso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaproceso);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = vUsuario.codusuario;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pcod_motivo = cmdTransaccionFactory.CreateParameter();
                        pcod_motivo.ParameterName = "p_cod_motivo";
                        if (pControl.cod_motivo != 0) pcod_motivo.Value = pControl.cod_motivo; else pcod_motivo.Value = DBNull.Value;
                        pcod_motivo.Direction = ParameterDirection.Input;
                        pcod_motivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_motivo);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        if (pControl.observaciones != null) pobservaciones.Value = pControl.observaciones; else pobservaciones.Value = DBNull.Value;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        DbParameter panexos = cmdTransaccionFactory.CreateParameter();
                        panexos.ParameterName = "p_anexos";
                        if (pControl.anexos != null) panexos.Value = pControl.anexos; else panexos.Value = DBNull.Value;
                        panexos.Direction = ParameterDirection.Input;
                        panexos.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(panexos);

                        DbParameter pnivel = cmdTransaccionFactory.CreateParameter();
                        pnivel.ParameterName = "p_nivel";
                        if (pControl.nivel != 0) pnivel.Value = pControl.nivel; else pnivel.Value = DBNull.Value;
                        pnivel.Direction = ParameterDirection.Input;
                        pnivel.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnivel);

                        DbParameter pfecha_consulta_dat = cmdTransaccionFactory.CreateParameter();
                        pfecha_consulta_dat.ParameterName = "p_fecha_consulta_dat";
                        if (pControl.fechaconsulta_dat != DateTime.MinValue) pfecha_consulta_dat.Value = pControl.fechaconsulta_dat; else pfecha_consulta_dat.Value = DBNull.Value;
                        pfecha_consulta_dat.Direction = ParameterDirection.Input;
                        pfecha_consulta_dat.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_consulta_dat);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CONTROLCRE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pControl;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "ModificarControlCreditos", ex);
                        return null;
                    }
                }
            }
        }

        public Giro CrearGiro(Giro pGiro, Usuario vUsuario, int opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidgiro = cmdTransaccionFactory.CreateParameter();
                        pidgiro.ParameterName = "p_idgiro";
                        pidgiro.Value = pGiro.idgiro;
                        if (opcion == 1)
                            pidgiro.Direction = ParameterDirection.Output;
                        else
                            pidgiro.Direction = ParameterDirection.Input;
                        pidgiro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidgiro);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pGiro.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "p_forma_pago";
                        pforma_pago.Value = pGiro.forma_pago;
                        pforma_pago.Direction = ParameterDirection.Input;
                        pforma_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);

                        DbParameter ptipo_acto = cmdTransaccionFactory.CreateParameter();
                        ptipo_acto.ParameterName = "p_tipo_acto";
                        ptipo_acto.Value = pGiro.tipo_acto;
                        ptipo_acto.Direction = ParameterDirection.Input;
                        ptipo_acto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_acto);

                        DbParameter pfec_reg = cmdTransaccionFactory.CreateParameter();
                        pfec_reg.ParameterName = "p_fec_reg";
                        pfec_reg.Value = pGiro.fec_reg;
                        pfec_reg.Direction = ParameterDirection.Input;
                        pfec_reg.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfec_reg);

                        DbParameter pfec_giro = cmdTransaccionFactory.CreateParameter();
                        pfec_giro.ParameterName = "p_fec_giro";
                        if (pGiro.fec_giro != DateTime.MinValue) pfec_giro.Value = pGiro.fec_giro; else pfec_giro.Value = DBNull.Value;
                        pfec_giro.Direction = ParameterDirection.Input;
                        pfec_giro.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfec_giro);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        if (pGiro.numero_radicacion != 0) pnumero_radicacion.Value = pGiro.numero_radicacion; else pnumero_radicacion.Value = DBNull.Value;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        if (pGiro.cod_ope != 0 && pGiro.cod_ope != null) pcod_ope.Value = pGiro.cod_ope; else pcod_ope.Value = DBNull.Value;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pnum_comp = cmdTransaccionFactory.CreateParameter();
                        pnum_comp.ParameterName = "p_num_comp";
                        pnum_comp.Value = -1;
                        pnum_comp.Direction = ParameterDirection.Input;
                        pnum_comp.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_comp);

                        DbParameter ptipo_comp = cmdTransaccionFactory.CreateParameter();
                        ptipo_comp.ParameterName = "p_tipo_comp";
                        if (pGiro.tipo_comp != 0) ptipo_comp.Value = pGiro.tipo_comp; else ptipo_comp.Value = DBNull.Value;
                        ptipo_comp.Direction = ParameterDirection.Input;
                        ptipo_comp.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_comp);

                        DbParameter pusu_gen = cmdTransaccionFactory.CreateParameter();
                        pusu_gen.ParameterName = "p_usu_gen";
                        pusu_gen.Value = pGiro.usu_gen;
                        pusu_gen.Direction = ParameterDirection.Input;
                        pusu_gen.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusu_gen);

                        DbParameter pusu_apli = cmdTransaccionFactory.CreateParameter();
                        pusu_apli.ParameterName = "p_usu_apli";
                        if (pGiro.usu_apli != null) pusu_apli.Value = pGiro.usu_apli; else pusu_apli.Value = DBNull.Value;
                        pusu_apli.Direction = ParameterDirection.Input;
                        pusu_apli.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusu_apli);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pGiro.estadogi;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pusu_apro = cmdTransaccionFactory.CreateParameter();
                        pusu_apro.ParameterName = "p_usu_apro";
                        if (pGiro.usu_apro != null) pusu_apro.Value = pGiro.usu_apro; else pusu_apro.Value = DBNull.Value;
                        pusu_apro.Direction = ParameterDirection.Input;
                        pusu_apro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusu_apro);

                        DbParameter pidctabancaria = cmdTransaccionFactory.CreateParameter();
                        pidctabancaria.ParameterName = "p_idctabancaria";
                        if (pGiro.idctabancaria != 0) pidctabancaria.Value = pGiro.idctabancaria; else pidctabancaria.Value = DBNull.Value;
                        pidctabancaria.Direction = ParameterDirection.Input;
                        pidctabancaria.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidctabancaria);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        if (pGiro.cod_banco != 0) pcod_banco.Value = pGiro.cod_banco; else pcod_banco.Value = DBNull.Value;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        DbParameter pnum_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnum_cuenta.ParameterName = "p_num_cuenta";
                        if (pGiro.num_cuenta != null) pnum_cuenta.Value = pGiro.num_cuenta; else pnum_cuenta.Value = DBNull.Value;
                        pnum_cuenta.Direction = ParameterDirection.Input;
                        pnum_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnum_cuenta);

                        DbParameter ptipo_cuenta = cmdTransaccionFactory.CreateParameter();
                        ptipo_cuenta.ParameterName = "p_tipo_cuenta";
                        if (pGiro.tipo_cuenta != -1) ptipo_cuenta.Value = pGiro.tipo_cuenta; else ptipo_cuenta.Value = DBNull.Value;
                        ptipo_cuenta.Direction = ParameterDirection.Input;
                        ptipo_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cuenta);

                        DbParameter pfec_apro = cmdTransaccionFactory.CreateParameter();
                        pfec_apro.ParameterName = "p_fec_apro";
                        if (pGiro.fec_apro != DateTime.MinValue) pfec_apro.Value = pGiro.fec_apro; else pfec_apro.Value = DBNull.Value;
                        pfec_apro.Direction = ParameterDirection.Input;
                        pfec_apro.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfec_apro);

                        DbParameter pcob_comision = cmdTransaccionFactory.CreateParameter();
                        pcob_comision.ParameterName = "p_cob_comision";
                        if (pGiro.cob_comision != 0) pcob_comision.Value = pGiro.cob_comision; else pcob_comision.Value = DBNull.Value;
                        pcob_comision.Direction = ParameterDirection.Input;
                        pcob_comision.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcob_comision);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pGiro.valor != 0) pvalor.Value = pGiro.valor; else pvalor.Value = DBNull.Value;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (opcion == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_GIRO_CREAR";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_GIRO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        if (opcion == 1)
                            pGiro.idgiro = Convert.ToInt32(pidgiro.Value);
                        return pGiro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "CrearGiro", ex);
                        return null;
                    }
                }
            }
        }


        public DateTime ObtenerUltimaFecha(Usuario pUsuario)
        {
            DateTime resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select max(fecha_solicitud) from credito_avance;";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToDateTime(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "ObtenerUltimaFecha", ex);
                        return DateTime.MinValue;
                    }
                }
            }
        }


        public Giro ConsultarFormaDesembolso(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Giro entidad = new Giro();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from giro where Numero_Radicacion =" + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDGIRO"] != DBNull.Value) entidad.idgiro = Convert.ToInt32(resultado["IDGIRO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (resultado["TIPO_ACTO"] != DBNull.Value) entidad.tipo_acto = Convert.ToInt32(resultado["TIPO_ACTO"]);
                            if (resultado["FEC_REG"] != DBNull.Value) entidad.fec_reg = Convert.ToDateTime(resultado["FEC_REG"]);
                            if (resultado["FEC_GIRO"] != DBNull.Value) entidad.fec_giro = Convert.ToDateTime(resultado["FEC_GIRO"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt32(resultado["COD_OPE"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt32(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt32(resultado["TIPO_COMP"]);
                            if (resultado["USU_GEN"] != DBNull.Value) entidad.usu_gen = Convert.ToString(resultado["USU_GEN"]);
                            if (resultado["USU_APLI"] != DBNull.Value) entidad.usu_apli = Convert.ToString(resultado["USU_APLI"]);
                            if (resultado["USU_APRO"] != DBNull.Value) entidad.usu_apro = Convert.ToString(resultado["USU_APRO"]);
                            if (resultado["FEC_APRO"] != DBNull.Value) entidad.fec_apro = Convert.ToDateTime(resultado["FEC_APRO"]);
                            if (resultado["IDCTABANCARIA"] != DBNull.Value) entidad.idctabancaria = Convert.ToInt64(resultado["IDCTABANCARIA"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["COD_BANCO"]);
                            if (resultado["NUM_CUENTA"] != DBNull.Value) entidad.num_cuenta = Convert.ToString(resultado["NUM_CUENTA"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToInt32(resultado["TIPO_CUENTA"]);
                            if (resultado["COB_COMISION"] != DBNull.Value) entidad.cob_comision = Convert.ToInt32(resultado["COB_COMISION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "ConsultarFormaDesembolso", ex);
                        return null;
                    }
                }
            }
        }



        public ControlCreditos ConsultarCodigoPersonaXBanco(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ControlCreditos entidad = new ControlCreditos();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT cod_persona FROM Bancos WHERE COD_BANCO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "ConsultarCodigoPersonaXBanco", ex);
                        return null;
                    }
                }
            }
        }


        public Avance ModificarDesembolsoAvance(Avance pAvance, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        Configuracion conf = new Configuracion();
                        sql = "update credito_avance set fecha_desembolso = To_Date('" + pAvance.fecha_desembolso.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "'), estado = '" + pAvance.estado + "', valor_desembolsado = " + pAvance.valor_desembolsado + " where idavance = " + pAvance.idavance;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pAvance;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "ModificarDesembolsoAvance", ex);
                        return null;
                    }
                }
            }
        }

        public Avance ModificarCuota(Avance pAvance, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        Configuracion conf = new Configuracion();
                        sql = "update credito  set  valor_cuota  = " + pAvance.valor_cuota + " where numero_radicacion = " + pAvance.numero_radicacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pAvance;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "ModificarCuota", ex);
                        return null;
                    }
                }
            }
        }


        public TRAN_CRED CrearTransaCred(TRAN_CRED pTransac, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnum_tran = cmdTransaccionFactory.CreateParameter();
                        pnum_tran.ParameterName = "p_num_tran";
                        pnum_tran.Value = pTransac.num_tran;
                        pnum_tran.Direction = ParameterDirection.Output;
                        pnum_tran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_tran);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        pcod_ope.Value = pTransac.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pTransac.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pcod_cliente = cmdTransaccionFactory.CreateParameter();
                        pcod_cliente.ParameterName = "p_cod_cliente";
                        pcod_cliente.Value = pTransac.cod_cliente;
                        pcod_cliente.Direction = ParameterDirection.Input;
                        pcod_cliente.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_cliente);

                        DbParameter pcod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_credito.ParameterName = "p_cod_linea_credito";
                        pcod_linea_credito.Value = pTransac.cod_linea_credito;
                        pcod_linea_credito.Direction = ParameterDirection.Input;
                        pcod_linea_credito.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_credito);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        ptipo_tran.Value = pTransac.tipo_tran;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                        DbParameter pcod_det_lis = cmdTransaccionFactory.CreateParameter();
                        pcod_det_lis.ParameterName = "p_cod_det_lis";
                        if (pTransac.cod_det_lis != 0) pcod_det_lis.Value = pTransac.cod_det_lis; else pcod_det_lis.Value = DBNull.Value;
                        pcod_det_lis.Direction = ParameterDirection.Input;
                        pcod_det_lis.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_det_lis);

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        pcod_atr.Value = pTransac.cod_atr;
                        pcod_atr.Direction = ParameterDirection.Input;
                        pcod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pTransac.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pvalor_mes = cmdTransaccionFactory.CreateParameter();
                        pvalor_mes.ParameterName = "p_valor_mes";
                        pvalor_mes.Value = pTransac.valor_mes;
                        pvalor_mes.Direction = ParameterDirection.Input;
                        pvalor_mes.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_mes);

                        DbParameter pvalor_causa = cmdTransaccionFactory.CreateParameter();
                        pvalor_causa.ParameterName = "p_valor_causa";
                        pvalor_causa.Value = pTransac.valor_causa;
                        pvalor_causa.Direction = ParameterDirection.Input;
                        pvalor_causa.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_causa);

                        DbParameter pvalor_orden = cmdTransaccionFactory.CreateParameter();
                        pvalor_orden.ParameterName = "p_valor_orden";
                        pvalor_orden.Value = pTransac.valor_orden;
                        pvalor_orden.Direction = ParameterDirection.Input;
                        pvalor_orden.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_orden);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pTransac.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pnum_tran_anula = cmdTransaccionFactory.CreateParameter();
                        pnum_tran_anula.ParameterName = "p_num_tran_anula";
                        if (pTransac.num_tran_anula != 0) pnum_tran_anula.Value = pTransac.num_tran_anula; else pnum_tran_anula.Value = DBNull.Value;
                        pnum_tran_anula.Direction = ParameterDirection.Input;
                        pnum_tran_anula.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_tran_anula);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_TRAN_CRED_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pTransac.num_tran = Convert.ToInt32(pnum_tran.Value);
                        return pTransac;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "CrearTransaCred", ex);
                        return null;
                    }
                }
            }
        }



        public Giro ModificarGiro(Giro pAvance, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        Configuracion conf = new Configuracion();
                        sql = "update giro set fec_apro = To_Date('" + pAvance.fec_apro.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ,"
                        + "usu_apro = '" + pAvance.usu_apro + "', valor = " + pAvance.valor + " , estado = " + pAvance.estadogi
                        + " where idgiro = " + pAvance.idgiro + " and numero_radicacion = " + pAvance.numero_radicacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pAvance;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "ModificarGiro", ex);
                        return null;
                    }
                }
            }
        }

        public List<DescuentosCredito> ListarDescuentosCredito(DescuentosCredito pDescuentosCredito, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<DescuentosCredito> lstDescuentosCredito = new List<DescuentosCredito>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //string sql = @"SELECT d.*, a.nombre FROM DescuentosCredito d LEFT JOIN atributos a ON d.cod_atr = a.cod_atr" + ObtenerFiltro(pDescuentosCredito, "d.") + " ORDER BY d.cod_atr ";
                        //  string sql = @"SELECT d.*, a.nombre,(Select x.modifica From descuentoslinea x where x.cod_atr = d.cod_atr  and x.modifica=1  and x.cod_linea_credito = + '" + pDescuentosCredito.cod_linea + "')" + " As modifica From descuentoscredito d left join atributos a on a.cod_atr=d.cod_atr left join descuentoslinea  x on  x.cod_atr = d.cod_atr where x.modifica=1" + "and d.numero_radicacion= " + pDescuentosCredito.numero_radicacion + " ORDER BY d.cod_atr ";

                        string sql = @"SELECT d.*, a.nombre, x.modifica FROM descuentoscredito d LEFT JOIN atributos a ON a.cod_atr = d.cod_atr "
                                                        + " LEFT JOIN descuentoslinea x ON x.cod_linea_credito = + '" + pDescuentosCredito.cod_linea + "'"
                                                        + " AND x.cod_atr = d.cod_atr   where x.modifica = 1 and  d.numero_radicacion = " + pDescuentosCredito.numero_radicacion + " ORDER BY d.cod_atr ";



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            DescuentosCredito entidad = new DescuentosCredito();
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nom_atr = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_DESCUENTO"] != DBNull.Value) entidad.tipo_descuento = Convert.ToInt32(resultado["TIPO_DESCUENTO"]);
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt32(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.val_atr = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["COBRA_MORA"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt32(resultado["COBRA_MORA"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToDecimal(resultado["NUMERO_CUOTAS"]);
                            if (resultado["FORMA_DESCUENTO"] != DBNull.Value) entidad.forma_descuento = Convert.ToInt32(resultado["FORMA_DESCUENTO"]);
                            if (resultado["TIPO_IMPUESTO"] != DBNull.Value) entidad.tipo_impuesto = Convert.ToInt32(resultado["TIPO_IMPUESTO"]);
                            lstDescuentosCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDescuentosCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "ListarDescuentosCredito", ex);
                        return null;
                    }
                }
            }
        }

        public Avance ConsultarTasaCreditoTotativo(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Avance entidad = new Avance();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from atributoscredito where cod_atr=2 and  NUMERO_RADICACION = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["CALCULO_ATR"] != DBNull.Value) entidad.calculo_atr = Convert.ToInt32(resultado["CALCULO_ATR"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToInt32(resultado["DESVIACION"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "ConsultarTasaCreditoTotativo", ex);
                        return null;
                    }
                }
            }
        }

        public Avance ConsultarPlazoCreditoTotativo(String pNombre, Usuario pUsuario)
        {
            DbDataReader resultado;
            Avance entidad = new Avance();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select diferir ,PLAZO_A_DIFERIR from lineascredito  where  nombre = " + "'" + pNombre.ToString() + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["PLAZO_A_DIFERIR"] != DBNull.Value) entidad.plazo_diferir = Convert.ToInt64(resultado["PLAZO_A_DIFERIR"]);
                            if (resultado["DIFERIR"] != DBNull.Value) entidad.diferir = Convert.ToInt32(resultado["DIFERIR"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "ConsultarPlazoCreditoTotativo", ex);
                        return null;
                    }
                }
            }
        }


        public Avance ConsultarPlazoMaximoCredito(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Avance entidad = new Avance();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select numero_cuotas from  credito   where numero_radicacion = " + pId;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["numero_cuotas"] != DBNull.Value) entidad.plazo_maximo = Convert.ToInt64(resultado["numero_cuotas"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "ConsultarPlazoMaximoCredito", ex);
                        return null;
                    }
                }
            }
        }


        public Avance CrearCreditoAvanceTarjeta(Avance pAvance, String numero_radicacion, Int64 codcliente, Int64 codoperacion, DateTime fechaavance, Int64 valoravance, Int64 plazo, ref string pError, Usuario pUsuario)
        {
            pError = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_ip = cmdTransaccionFactory.CreateParameter();
                        p_ip.ParameterName = "p_ip";
                        if (pUsuario.IP == "0" || pUsuario.IP == "")
                            p_ip.Value = 0;
                        else
                            p_ip.Value = pUsuario.IP;
                        p_ip.DbType = DbType.Date;
                        p_ip.Direction = ParameterDirection.Input;



                        DbParameter p_numero_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_numero_radicacion.ParameterName = "p_numero_radicacion";
                        p_numero_radicacion.Value = numero_radicacion;
                        p_numero_radicacion.Direction = ParameterDirection.Input;


                        DbParameter p_fecha_prim_pago = cmdTransaccionFactory.CreateParameter();
                        p_fecha_prim_pago.ParameterName = "p_fecha_prim_pago";
                        if (fechaavance == null)
                            p_fecha_prim_pago.Value = DBNull.Value;
                        else
                            p_fecha_prim_pago.Value = fechaavance;
                        p_fecha_prim_pago.DbType = DbType.Date;
                        p_fecha_prim_pago.Direction = ParameterDirection.Input;


                        DbParameter p_idavance = cmdTransaccionFactory.CreateParameter();
                        p_idavance.ParameterName = "p_idavance";
                        p_idavance.Value = pAvance.idavance;
                        p_idavance.DbType = DbType.AnsiString;
                        p_idavance.Direction = ParameterDirection.InputOutput;
                        // p_idavance.Size = 1;

                        DbParameter p_fecha_desembolso = cmdTransaccionFactory.CreateParameter();
                        p_fecha_desembolso.ParameterName = "p_fecha_desembolso";
                        p_fecha_desembolso.Value = fechaavance;
                        p_fecha_desembolso.Direction = ParameterDirection.Input;

                        DbParameter p_Usuario = cmdTransaccionFactory.CreateParameter();
                        p_Usuario.ParameterName = "p_Usuario";
                        p_Usuario.Value = pUsuario.codusuario;
                        // p_Usuario.DbType = DbType.AnsiString;
                        p_Usuario.Direction = ParameterDirection.Input;
                        p_Usuario.Size = 50;

                        DbParameter p_Cod_Ope = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Ope.ParameterName = "p_cod_ope";
                        p_Cod_Ope.Value = codoperacion;
                        p_Cod_Ope.Direction = ParameterDirection.Input;

                        DbParameter p_valor_desembolsado = cmdTransaccionFactory.CreateParameter();
                        p_valor_desembolsado.ParameterName = "p_valor_desembolsado";
                        p_valor_desembolsado.Value = valoravance;
                        p_valor_desembolsado.Direction = ParameterDirection.Input;

                        DbParameter p_plazo = cmdTransaccionFactory.CreateParameter();
                        p_plazo.ParameterName = "p_plazo";
                        p_plazo.Value = plazo;
                        p_plazo.Direction = ParameterDirection.Input;

                        DbParameter p_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        p_tipo_tran.ParameterName = "p_tipo_tran";
                        p_tipo_tran.Value = 982;
                        p_tipo_tran.Direction = ParameterDirection.Input;

                        DbParameter p_operacion_tarjeta = cmdTransaccionFactory.CreateParameter();
                        p_operacion_tarjeta.ParameterName = "p_operacion_tarjeta";
                        p_operacion_tarjeta.Value = DBNull.Value;
                        p_operacion_tarjeta.DbType = DbType.String;
                        p_operacion_tarjeta.Direction = ParameterDirection.Input;

                        DbParameter p_num_tran_tarjeta = cmdTransaccionFactory.CreateParameter();
                        p_num_tran_tarjeta.ParameterName = "p_num_tran_tarjeta";
                        p_num_tran_tarjeta.Value = DBNull.Value;
                        p_num_tran_tarjeta.DbType = DbType.Int64;
                        p_num_tran_tarjeta.Direction = ParameterDirection.Input;

                        DbParameter p_error = cmdTransaccionFactory.CreateParameter();
                        p_error.ParameterName = "p_error";
                        string s_error = " ";
                        for (int i = 0; i < 200; i++) s_error += " ";  // Se asigna espacios para efectos de poder ejecutar el PL
                        p_error.Value = s_error;
                        p_error.DbType = DbType.String;
                        p_error.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(p_numero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(p_idavance);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_prim_pago);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_desembolso);
                        cmdTransaccionFactory.Parameters.Add(p_Usuario);
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Ope);
                        cmdTransaccionFactory.Parameters.Add(p_ip);
                        cmdTransaccionFactory.Parameters.Add(p_valor_desembolsado);
                        cmdTransaccionFactory.Parameters.Add(p_plazo);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_tran);
                        cmdTransaccionFactory.Parameters.Add(p_operacion_tarjeta);
                        cmdTransaccionFactory.Parameters.Add(p_num_tran_tarjeta);
                        cmdTransaccionFactory.Parameters.Add(p_error);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DESAVANCE";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);

                        pAvance.idavance = Convert.ToInt32(p_idavance.Value);
                        if (p_error.Value != null)
                            pError = p_error.Value.ToString();

                        return pAvance;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "ModificarCredito", ex);
                        return null;
                    }
                }
            }
        }


        public Avance CrearPagoAvanceTarjeta(Avance pAvance, String numero_radicacion, Int64 codcliente, Int64 codoperacion, DateTime fechapago, Int64 valorapagar, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        DbParameter p_fecha_pago = cmdTransaccionFactory.CreateParameter();
                        p_fecha_pago.ParameterName = "pf_fecha";
                        p_fecha_pago.Value = fechapago;

                        DbParameter p_numero_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_numero_radicacion.ParameterName = "p_radicacion";
                        p_numero_radicacion.Value = numero_radicacion;

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "ptipo";
                        ptipo.Value = 2;
                        ptipo.DbType = DbType.Int16;
                        ptipo.Direction = ParameterDirection.Input;

                        DbParameter pvalorapagar = cmdTransaccionFactory.CreateParameter();
                        pvalorapagar.ParameterName = "pvalor";
                        pvalorapagar.Value = valorapagar;
                        pvalorapagar.Direction = ParameterDirection.Input;

                        DbParameter p_Usuario = cmdTransaccionFactory.CreateParameter();
                        p_Usuario.ParameterName = "p_Usuario";
                        p_Usuario.Value = pUsuario.codusuario;
                        p_Usuario.Direction = ParameterDirection.Input;
                        p_Usuario.Size = 50;

                        DbParameter p_Cod_Ope = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Ope.ParameterName = "p_cod_ope";
                        p_Cod_Ope.Value = codoperacion;
                        p_Cod_Ope.Direction = ParameterDirection.Input;

                        DbParameter p_cod_det_lis = cmdTransaccionFactory.CreateParameter();
                        p_cod_det_lis.ParameterName = "pn_cod_det_lis";
                        p_cod_det_lis.Value = DBNull.Value;
                        p_cod_det_lis.Direction = ParameterDirection.Input;

                        DbParameter p_sobrante = cmdTransaccionFactory.CreateParameter();
                        p_sobrante.ParameterName = "p_sobrante";
                        p_sobrante.Value = 0;
                        p_sobrante.Direction = ParameterDirection.Output;

                        DbParameter pmensaje = cmdTransaccionFactory.CreateParameter();
                        pmensaje.ParameterName = "pmensaje";
                        pmensaje.Value = pAvance.pagoavance;
                        pmensaje.Direction = ParameterDirection.Output;


                        cmdTransaccionFactory.Parameters.Add(p_fecha_pago);
                        cmdTransaccionFactory.Parameters.Add(p_numero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(ptipo);
                        cmdTransaccionFactory.Parameters.Add(pvalorapagar);
                        cmdTransaccionFactory.Parameters.Add(p_Usuario);
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Ope);
                        cmdTransaccionFactory.Parameters.Add(p_cod_det_lis);
                        cmdTransaccionFactory.Parameters.Add(p_sobrante);
                        cmdTransaccionFactory.Parameters.Add(pmensaje);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_PAGO_AVANCE";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);


                        pAvance.pagoavance = Convert.ToBoolean(pmensaje.Value);
                        return pAvance;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "CrearPagoAvanceTarjeta", ex);
                        return null;
                    }
                }
            }
        }


        public Avance NegarAvances(Avance pEntidad, Motivo motivo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_login = cmdTransaccionFactory.CreateParameter();
                        p_login.ParameterName = "p_login";
                        p_login.Value = pUsuario.codusuario;
                        p_login.Direction = ParameterDirection.Input;

                        DbParameter p_idavance = cmdTransaccionFactory.CreateParameter();
                        p_idavance.ParameterName = "p_idavance";
                        p_idavance.Value = pEntidad.idavance;
                        p_idavance.Direction = ParameterDirection.Input;

                        DbParameter p_observaciones = cmdTransaccionFactory.CreateParameter();
                        p_observaciones.ParameterName = "p_observaciones";
                        p_observaciones.Value = (pEntidad.observacion);
                        p_observaciones.Direction = ParameterDirection.Input;

                        DbParameter p_motivo = cmdTransaccionFactory.CreateParameter();
                        p_motivo.ParameterName = "p_motivo";
                        p_motivo.Value = motivo.Codigo;
                        p_motivo.Direction = ParameterDirection.Input;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "pfecha";
                        pfecha.Value = DateTime.Now;
                        pfecha.DbType = DbType.DateTime;
                        pfecha.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_login);
                        cmdTransaccionFactory.Parameters.Add(p_idavance);
                        cmdTransaccionFactory.Parameters.Add(p_observaciones);
                        cmdTransaccionFactory.Parameters.Add(p_motivo);
                        cmdTransaccionFactory.Parameters.Add(pfecha);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_AVANCES_NEG";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "NegarAvances", ex);
                        return null;
                    }
                }
            }
        }


        public string AlertaTarjeta(Int64 cod_persona, Usuario pUsuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                        
                        int laux = 0;
                        string result = "";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        //Alerta estado Tarjeta 0=Pendiente, 1=Activa, 2=Inactiva, 3=Bloqueda
                        laux = 0;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = @"select T.ESTADO as NUMERO from TARJETA T where T.COD_PERSONA = " + cod_persona + "and T.TIPO_CUENTA = 2";
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO"] != DBNull.Value) laux = Convert.ToInt32(resultado["NUMERO"]);
                            if (laux == 3)
                                result += "Tarjeta Bloqueada-";
                        }

                        //Alerta estado cupo Crédito 0 = activo, 1 = bloqueado
                        laux = 0;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = @"SELECT ESTADO_SALDO as NUMERO from TARJETA T INNER JOIN CREDITO C ON T.NUMERO_CUENTA = C.NUMERO_RADICACION WHERE T.COD_PERSONA = " + cod_persona + " AND T.ESTADO != 2 AND C.ESTADO = 'C'";
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO"] != DBNull.Value) laux = Convert.ToInt32(resultado["NUMERO"]);
                            if (laux > 0)
                                result += "Cupo Bloqueado (Crédito)";
                        }

                        connection.Close();
                        return result;
                    }                    
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "AlertaTarjeta", ex);
                        return null;
                    }
                }
            }
        }

        public List<Avance> ListarCreditoXaprobar(Avance pRotativo, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Avance> lstCredito = new List<Avance>();
            Configuracion conf = new Configuracion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select Distinct c.cod_deudor, c.numero_radicacion, l.nombre as nomlinea, p.identificacion, p.primer_nombre || ' ' || p.segundo_nombre || ' ' || p.primer_apellido || ' ' || p.segundo_apellido as Nombre, "
                                        + "p.cod_nomina, c.monto_aprobado as cupototal, c.monto_aprobado - c.saldo_capital as cupodisponible, "
                                        + "0 As idavance, c.fecha_solicitud, c.monto_aprobado, ' ' As observacion, c.fecha_aprobacion, c.monto_aprobado, 0 As aprobar_avances "
                                        + "From credito c Inner Join lineascredito l on l.cod_linea_credito = c.cod_linea_credito "
                                        + "Inner join credito_avance a On a.numero_radicacion = c.numero_radicacion "
                                        + "Inner join persona p on p.cod_persona = c.cod_deudor " + filtro + " " 
                                        + "Order by c.numero_radicacion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Avance entidad = new Avance();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["NOMLINEA"] != DBNull.Value) entidad.nomlinea = Convert.ToString(resultado["NOMLINEA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["CUPOTOTAL"] != DBNull.Value) entidad.cupototal = Convert.ToDecimal(resultado["CUPOTOTAL"]);
                            if (resultado["CUPODISPONIBLE"] != DBNull.Value) entidad.cupodisponible = Convert.ToDecimal(resultado["CUPODISPONIBLE"]);
                            if (resultado["IDAVANCE"] != DBNull.Value) entidad.idavance = Convert.ToInt32(resultado["IDAVANCE"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.valor_solicitado = Convert.ToDecimal(resultado["MONTO_APROBADO"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.valor_aprobado = Convert.ToDecimal(resultado["MONTO_APROBADO"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "ListarCreditoXaprobar", ex);
                        return null;
                    }
                }
            }
        }



    }
}


