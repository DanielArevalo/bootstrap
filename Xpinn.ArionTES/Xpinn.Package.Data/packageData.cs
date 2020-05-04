using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Package.Entities;
using System.Threading.Tasks;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Package.Data
{
    public class packageData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public packageData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public DbDataReader EjecutarSQL(string psql, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = psql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public string NombreAtributo(int? pCodAtr, Usuario pusuario)
        {
            if (pCodAtr == null)
                return "";
            string nom_atr = "";
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select nombre From atributos Where cod_atr = " + pCodAtr;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["nombre"] != DBNull.Value) nom_atr = Convert.ToString(resultado["nombre"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return nom_atr;
                    }
                    catch
                    {
                        return nom_atr;
                    }
                }
            }
        }

        public decimal? SumaDeudas(Int64? pn_cod_cliente, Usuario pusuario)
        {
            if (pn_cod_cliente == null)
                return 0;
            decimal? n_deuda = null;
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select sum(d.saldo_capital) As deuda From credito d, lineascredito p Where d.cod_linea_credito = p.cod_linea_credito And d.cod_deudor = " + pn_cod_cliente + " And d.estado = 'C' = ";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["deuda"] != DBNull.Value) n_deuda = Convert.ToDecimal(resultado["deuda"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return n_deuda;
                    }
                    catch
                    {
                        return n_deuda;
                    }
                }
            }
        }

        public bool ConTipoTasa(int? pn_tip_tas, ref string s_efe_nom, ref int? n_per, ref string s_mod, ref int? n_mod_per, Usuario pusuario)
        {
            if (pn_tip_tas == null)
                return false;
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select efectiva_nomina, cod_periodicidad, modalidad, cod_periodicidad_cap From tipotasa Where cod_tipo_tasa = " + pn_tip_tas;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["efectiva_nomina"] != DBNull.Value) s_efe_nom = Convert.ToString(resultado["efectiva_nomina"]);
                            if (resultado["cod_periodicidad"] != DBNull.Value) n_per = Convert.ToInt32(resultado["cod_periodicidad"]);
                            if (resultado["modalidad"] != DBNull.Value) s_mod = Convert.ToString(resultado["modalidad"]);
                            if (resultado["cod_periodicidad_cap"] != DBNull.Value) n_mod_per = Convert.ToInt32(resultado["cod_periodicidad_cap"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public decimal ConPeriodicidadNumDia(decimal? pper_pro, Usuario pusuario)
        {
            if (pper_pro == null)
                return 0;
            decimal num_dias = 0;
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select numero_dias From periodicidad Where cod_periodicidad = " + pper_pro;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["numero_dias"] != DBNull.Value) num_dias = Convert.ToDecimal(resultado["numero_dias"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return num_dias;
                    }
                    catch
                    {
                        return num_dias;
                    }
                }
            }
        }

        public List<Tuple<DateTime?, string, string>> ConsultarHistoricoCambiosCredito(long? n_radic, DateTime? rf_f_prox_pago, Usuario pusuario)
        {
            var lista = new List<Tuple<DateTime?, string, string>>();

            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = @"Select fechacambio, valor_ant, valor_nue 
                                                              From histcambios_cre 
                                                              Where tipo_cambio = 313 
                                                              And numero_radicacion = " + n_radic +
                                                              " and fechacambio > " + rf_f_prox_pago + " Order By 1";

                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DateTime? f_fecha_cambio = null;
                            string s_val_ant = string.Empty;
                            string s_val_nue = string.Empty;

                            if (resultado["fechacambio"] != DBNull.Value) f_fecha_cambio = Convert.ToDateTime(resultado["fechacambio"]);
                            if (resultado["valor_ant"] != DBNull.Value) s_val_ant = Convert.ToString(resultado["valor_ant"]);
                            if (resultado["valor_nue"] != DBNull.Value) s_val_nue = Convert.ToString(resultado["valor_nue"]);

                            lista.Add(Tuple.Create(f_fecha_cambio, s_val_ant, s_val_nue));
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lista;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public bool ConsultarDependeDeAtributos(int? n_cod_atr, ref int? n_atr_depende, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select depende from atributos where cod_atr = " + n_cod_atr;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["depende"] != DBNull.Value) n_atr_depende = Convert.ToInt32(resultado["depende"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public bool ConsultarValorPrejuridicoPagado(long? n_radic, DateTime? rf_f_prox_pago, int? n_atr_PREJUR, ref decimal? n_valor_prejuridico_pagado, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = @"Select Sum(Decode(r.tipo_mov, 1, -d.valor, 2, d.valor)) as valor_prejuridico         
                                    From det_tran_cred d 
                                    Inner Join tran_cred t On d.num_tran = t.num_tran 
                                    And d.numero_radicacion = t.numero_radicacion 
                                    Inner Join tipo_tran r On r.tipo_tran = t.tipo_tran           
                                    Where d.numero_radicacion = " + n_radic +
                                    @" And d.fecha_cuota = " + rf_f_prox_pago +
                                    @" And d.cod_atr = " + n_atr_PREJUR +
                                    @" And d.cod_ope Not In(Select a.cod_ope From operacion_anulada a Where a.cod_ope = d.cod_ope)           
                                    And d.cod_ope Not In(Select a.cod_ope_anula From operacion_anulada a Where a.cod_ope_anula = d.cod_ope) ";

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor_prejuridico"] != DBNull.Value) n_valor_prejuridico_pagado = Convert.ToDecimal(resultado["valor_prejuridico"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public bool ConsultarCuentaPorCobrarAtributosCredito(long? n_radic, ref decimal? n_atributos, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = @"Select Sum(saldo) as saldo From cuenta_porcobrar_cre Where numero_radicacion = " + n_radic;

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["saldo"] != DBNull.Value) n_atributos = Convert.ToDecimal(resultado["saldo"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public bool ConsultarRangoFechasHistoricoTasa(int? n_tip_his, DateTime? fechaRango, ref DateTime? f_fec_fin, ref DateTime? f_fec_ini, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = @"Select fecha_final, fecha_inicial From historicotasa Where tipo_historico = " + n_tip_his + " and " + fechaRango + " between fecha_inicial and fecha_final";

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["fecha_final"] != DBNull.Value) f_fec_fin = Convert.ToDateTime(resultado["fecha_final"]);
                            if (resultado["fecha_inicial"] != DBNull.Value) f_fec_ini = Convert.ToDateTime(resultado["fecha_inicial"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public bool ConsultarPrioridadLinea(string s_cod_credi, ref int? n_cod_atr, ref int n_num, Usuario pusuario)
        {
            DbDataReader resultado;
            bool encontradaPrioridad = false;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = @"Select Distinct cod_atr, numero From prioridad_lin Where cod_linea_credito = " + s_cod_credi +
                                   @" Union
                                   Select cod_atr, 999 From atributos Where cod_atr Not In(Select cod_atr From prioridad_lin Where cod_linea_credito = " + s_cod_credi + " ) Order by 2 ";

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_atr"] != DBNull.Value) n_cod_atr = Convert.ToInt32(resultado["cod_atr"]);
                            if (resultado["numero"] != DBNull.Value) n_num = Convert.ToInt32(resultado["numero"]);
                            encontradaPrioridad = true;
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return encontradaPrioridad;
                    }
                    catch
                    {
                        return encontradaPrioridad;
                    }
                }
            }
        }

        public int? ConPeriodicidadTipoCal(int? pper_pro, Usuario pusuario)
        {
            if (pper_pro == null)
                return 0;
            int? tipo_calendario = 0;
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select tipo_calendario From periodicidad Where cod_periodicidad = " + pper_pro;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["tipo_calendario"] != DBNull.Value) tipo_calendario = Convert.ToInt32(resultado["tipo_calendario"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return tipo_calendario;
                    }
                    catch
                    {
                        return tipo_calendario;
                    }
                }
            }
        }

        public decimal ConPeriodicidadPerAnu(int? pper_pro, Usuario pusuario)
        {
            if (pper_pro == null)
                return 0;
            decimal numeroPeriodos = 0;
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select periodos_anuales From periodicidad Where cod_periodicidad = " + pper_pro;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["periodos_anuales"] != DBNull.Value) numeroPeriodos = Convert.ToDecimal(resultado["periodos_anuales"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return numeroPeriodos;
                    }
                    catch
                    {
                        return numeroPeriodos;
                    }
                }
            }
        }

        public bool DeterminarFechaInicioYFinTasaHistorica(int? n_tip_his, DateTime? f_fec_prox, ref DateTime? f_fec_ini, ref DateTime? f_fec_fin, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select fecha_inicial, fecha_final From historicotasa Where tipo_historico = " + n_tip_his + " And " + f_fec_prox + " between fecha_inicial and fecha_final";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["fecha_inicial"] != DBNull.Value) f_fec_ini = Convert.ToDateTime(resultado["fecha_inicial"]);
                            if (resultado["fecha_final"] != DBNull.Value) f_fec_fin = Convert.ToDateTime(resultado["fecha_final"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public bool DeterminarValorYTipoTasaHistorica(decimal? tipoHistorico, DateTime? g_f_usura, ref decimal? n_tasa_usura, ref int? n_tipo_tasa, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = @"Select h.valor, t.tipo_tasa 
                                                              From historicotasa h , tipotasahist  t   
                                                              Where " + g_f_usura + @" between h.fecha_inicial And h.fecha_final 
                                                              And h.tipo_historico = " + tipoHistorico +
                                                              @" And h.tipo_historico = t.tipo_historico";

                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) n_tasa_usura = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["tipo_tasa"] != DBNull.Value) n_tipo_tasa = Convert.ToInt32(resultado["tipo_tasa"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public decimal ConPeriodicidadNumeroMeses(int? pper_pro, Usuario pusuario)
        {
            if (pper_pro == null)
                return 0;
            decimal numero_meses = 0;
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select numero_meses From periodicidad Where cod_periodicidad = " + pper_pro;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["numero_meses"] != DBNull.Value) numero_meses = Convert.ToDecimal(resultado["numero_meses"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return numero_meses;
                    }
                    catch
                    {
                        return numero_meses;
                    }
                }
            }
        }

        public bool ConTipoTasaDelHist(DateTime? f_fec_ini, DateTime? pf_fec_apro, ref int? rtip_tas, ref decimal? rtasa, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            cmdTransaccionFactory.CommandText =
                                @"Select t.tipo_tasa, avg (h.valor) As valor 
                                    From tipotasahist t, historicotasa h Where t.tipo_historico = h.tipo_historico
                                    And t.tipo_historico = ptip_his And h.fecha_inicial <= To_Date('" + Convert.ToDateTime(f_fec_ini).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') And h.fecha_final >= To_Date('" + Convert.ToDateTime(pf_fec_apro).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') Group By t.tipo_tasa";
                        else
                            cmdTransaccionFactory.CommandText =
                                @"Select t.tipo_tasa, avg (h.valor) As valor
                                    From tipotasahist t, historicotasa h Where t.tipo_historico = h.tipo_historico
                                    And t.tipo_historico = ptip_his And h.fecha_inicial <= '" + f_fec_ini + "' And h.fecha_final >= '" + pf_fec_apro + "' Group By t.tipo_tasa";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["tipo_tasa"] != DBNull.Value) rtip_tas = Convert.ToInt32(resultado["tipo_tasa"]);
                            if (resultado["valor"] != DBNull.Value) rtasa = Convert.ToDecimal(resultado["valor"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }


        public bool ConHistoricoTasa(DateTime? pf_fec_apro, ref int? rtip_tas, ref decimal? rtasa, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            cmdTransaccionFactory.CommandText =
                                @"Select t.tipo_tasa, h.valor
                                    From tipotasahist t, historicotasa h Where t.tipo_historico = h.tipo_historico
                                    And t.tipo_historico = ptip_his And h.fecha_inicial <= To_Date('" + Convert.ToDateTime(pf_fec_apro).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') And h.fecha_final >= To_Date('" + Convert.ToDateTime(pf_fec_apro).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            cmdTransaccionFactory.CommandText =
                                @"Select t.tipo_tasa, h.valor
                                    From tipotasahist t, historicotasa h Where t.tipo_historico = h.tipo_historico
                                    And t.tipo_historico = ptip_his And h.fecha_inicial <= '" + pf_fec_apro + "' And h.fecha_final >= '" + pf_fec_apro + "' ";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["tipo_tasa"] != DBNull.Value) rtip_tas = Convert.ToInt32(resultado["tipo_tasa"]);
                            if (resultado["valor"] != DBNull.Value) rtasa = Convert.ToDecimal(resultado["valor"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public int? ConTasaHist(int? ptip_his, Usuario pusuario)
        {
            if (ptip_his == null)
                return 0;
            int? rtip_tas = 0;
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select t.tipo_tasa From tipotasahist t Where t.tipo_historico = " + ptip_his;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["tipo_tasa"] != DBNull.Value) rtip_tas = Convert.ToInt32(resultado["tipo_tasa"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return rtip_tas;
                    }
                    catch
                    {
                        return rtip_tas;
                    }
                }
            }
        }

        public string ConGeneral(int? pcodigo, Usuario pusuario)
        {
            if (pcodigo == null)
                return "";
            string valor = "";
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select valor From general Where codigo = " + pcodigo;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) valor = Convert.ToString(resultado["valor"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch
                    {
                        return valor;
                    }
                }
            }
        }


        public List<RanValAtributo> ConRanValAtributo(int? n_cod_atr, ref int? n_tipo_tope, ref decimal? n_desde, ref decimal? n_hasta, ref decimal? n_valor_tope, Usuario pusuario)
        {
            List<RanValAtributo> listado = new List<RanValAtributo>();
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = @"Select tipo_tope, desde, hasta, valor From ranval_atributo Where cod_atr = " + n_cod_atr + " Order by 1, 2, 3";

                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RanValAtributo item = new RanValAtributo();
                            if (resultado["tipo_tope"] != DBNull.Value) item.n_tipo_tope = Convert.ToInt32(resultado["tipo_tope"]);
                            if (resultado["desde"] != DBNull.Value) item.n_desde = Convert.ToDecimal(resultado["desde"]);
                            if (resultado["hasta"] != DBNull.Value) item.n_hasta = Convert.ToDecimal(resultado["hasta"]);
                            if (resultado["valor"] != DBNull.Value) item.n_valor_tope = Convert.ToDecimal(resultado["valor"]);
                            listado.Add(item);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listado;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public int? ConPeriodicidadDiaria(int? pTipoCal, Usuario pusuario)
        {
            if (pTipoCal == null)
                return null;
            int? valor = null;
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select cod_periodicidad From periodicidad Where numero_dias = 1 And tipo_calendario = " + pTipoCal + " Order by 1";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_periodicidad"] != DBNull.Value) valor = Convert.ToInt32(resultado["cod_periodicidad"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch
                    {
                        return valor;
                    }
                }
            }
        }

        public bool ConsultarCredito(Int64 pId, ref Package.Entities.Credito entidad, Usuario pUsuario)
        {
            DbDataReader resultado;
            if (entidad == null)
                entidad = new Package.Entities.Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CREDITO WHERE NUMERO_RADICACION = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["NUMERO_OBLIGACION"] != DBNull.Value) entidad.numero_obligacion = Convert.ToString(resultado["NUMERO_OBLIGACION"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.monto_solicitado = Convert.ToDecimal(resultado["MONTO_SOLICITADO"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto_aprobado = Convert.ToDecimal(resultado["MONTO_APROBADO"]);
                            if (resultado["MONTO_DESEMBOLSADO"] != DBNull.Value) entidad.monto_desembolsado = Convert.ToDecimal(resultado["MONTO_DESEMBOLSADO"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt32(resultado["COD_MONEDA"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["FECHA_DESEMBOLSO"] != DBNull.Value) entidad.fecha_desembolso = Convert.ToDateTime(resultado["FECHA_DESEMBOLSO"]);
                            if (resultado["FECHA_PRIMERPAGO"] != DBNull.Value) entidad.fecha_primerpago = Convert.ToDateTime(resultado["FECHA_PRIMERPAGO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToDecimal(resultado["NUMERO_CUOTAS"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToDecimal(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["CUOTAS_PENDIENTES"] != DBNull.Value) entidad.cuotas_pendientes = Convert.ToDecimal(resultado["CUOTAS_PENDIENTES"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToString(resultado["COD_PERIODICIDAD"]);
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt32(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["TIPO_GRACIA"] != DBNull.Value) entidad.tipo_gracia = Convert.ToInt32(resultado["TIPO_GRACIA"]);
                            if (resultado["COD_ATR_GRA"] != DBNull.Value) entidad.cod_atr_gra = Convert.ToInt64(resultado["COD_ATR_GRA"]);
                            if (resultado["PERIODO_GRACIA"] != DBNull.Value) entidad.periodo_gracia = Convert.ToDecimal(resultado["PERIODO_GRACIA"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt32(resultado["COD_CLASIFICA"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["SALDO_CAPITAL"]);
                            if (resultado["OTROS_SALDOS"] != DBNull.Value) entidad.otros_saldos = Convert.ToDecimal(resultado["OTROS_SALDOS"]);
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.cod_asesor_com = Convert.ToInt64(resultado["COD_ASESOR_COM"]);
                            if (resultado["TIPO_CREDITO"] != DBNull.Value) entidad.tipo_credito = Convert.ToString(resultado["TIPO_CREDITO"]);
                            if (resultado["NUM_RADIC_ORIGEN"] != DBNull.Value) entidad.num_radic_origen = Convert.ToInt64(resultado["NUM_RADIC_ORIGEN"]);
                            if (resultado["VRREESTRUCTURADO"] != DBNull.Value) entidad.vrreestructurado = Convert.ToDecimal(resultado["VRREESTRUCTURADO"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["COD_PAGADURIA"] != DBNull.Value) entidad.cod_pagaduria = Convert.ToInt32(resultado["COD_PAGADURIA"]);
                            if (resultado["GRADIENTE"] != DBNull.Value) entidad.gradiente = Convert.ToDecimal(resultado["GRADIENTE"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["DIAS_AJUSTE"] != DBNull.Value) entidad.dias_ajuste = Convert.ToInt32(resultado["DIAS_AJUSTE"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                            if (resultado["TIR"] != DBNull.Value) entidad.tir = Convert.ToDecimal(resultado["TIR"]);
                            if (resultado["PAGO_ESPECIAL"] != DBNull.Value) entidad.pago_especial = Convert.ToInt32(resultado["PAGO_ESPECIAL"]);
                            if (resultado["UNIVERSIDAD"] != DBNull.Value) entidad.universidad = Convert.ToString(resultado["UNIVERSIDAD"]);
                            if (resultado["SEMESTRE"] != DBNull.Value) entidad.semestre = Convert.ToString(resultado["SEMESTRE"]);
                            if (resultado["FECREESTRUCTURADO"] != DBNull.Value) entidad.fecreestructurado = Convert.ToDateTime(resultado["FECREESTRUCTURADO"]);
                            if (resultado["REESTRUCTURADO"] != DBNull.Value) entidad.reestructurado = Convert.ToInt32(resultado["REESTRUCTURADO"]);
                        }
                        else
                        {
                            return false;
                        }

                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    finally
                    {
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                }
            }
        }

        public decimal ConsultaPeriodicidad(int? pper_pro, ref decimal? n_dias_per, ref int? n_tip_cal, Usuario pusuario)
        {
            if (pper_pro == null)
                return 0;
            decimal num_dias = 0;
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select numero_dias, tipo_calendario From periodicidad Where cod_periodicidad = " + pper_pro;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["numero_dias"] != DBNull.Value) n_dias_per = Convert.ToDecimal(resultado["numero_dias"]);
                            if (resultado["tipo_calendario"] != DBNull.Value) n_tip_cal = Convert.ToInt32(resultado["tipo_calendario"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return num_dias;
                    }
                    catch
                    {
                        return num_dias;
                    }
                }
            }
        }

        public bool ConsultaEmpresa(Int64? pcod_empresa, string pcod_linea_credito, ref decimal? ldias_novedad, ref int? lcod_periodicidad, ref decimal? n_dia_per, ref DateTime? pfecha_inicio, ref string s_tipo_ini, ref Int64? lcod_empresa, ref int? n_tipo_lista, Usuario pusuario)
        {
            if (pcod_empresa == null)
                return false;
            ldias_novedad = null;
            lcod_periodicidad = null;
            n_dia_per = 0;
            s_tipo_ini = "1";
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText =
                            @"Select p.dias_novedad, t.cod_periodicidad, 0, t.fecha_inicio, 1, p.cod_empresa, t.tipo_lista
                              From empresa_recaudo p
                              Inner Join empresa_recaudo_programacion t On p.cod_empresa = t.cod_empresa
                              Inner Join tipo_lista_recaudo_detalle d On t.tipo_lista = d.idtipo_lista
                              Where p.cod_empresa = " + pcod_empresa + " And d.tipo_producto = '2' And d.cod_linea = '" + pcod_linea_credito + "' ";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["dias_novedad"] != DBNull.Value) ldias_novedad = Convert.ToDecimal(resultado["dias_novedad"]);
                            if (resultado["cod_periodicidad"] != DBNull.Value) lcod_periodicidad = Convert.ToInt32(resultado["cod_periodicidad"]);
                            if (resultado["fecha_inicio"] != DBNull.Value) pfecha_inicio = Convert.ToDateTime(resultado["fecha_inicio"]);
                            if (resultado["cod_empresa"] != DBNull.Value) lcod_empresa = Convert.ToInt64(resultado["cod_empresa"]);
                            if (resultado["tipo_lista"] != DBNull.Value) n_tipo_lista = Convert.ToInt32(resultado["tipo_lista"]);
                            dbConnectionFactory.CerrarConexion(connection);
                            return true;
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return false;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public DateTime? ConsultaPeriodo(int? n_tipo_lista, Usuario pusuario)
        {
            if (n_tipo_lista == null)
                return null;
            DateTime? f_fecha_lista = null;
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select Max(l.periodo_corte) As periodo_corte From empresa_novedad l Where l.estado != '5' and l.cod_empresa = pcod_empresa and l.tipo_lista = " + n_tipo_lista;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["periodo_corte"] != DBNull.Value) f_fecha_lista = Convert.ToDateTime(resultado["periodo_corte"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return f_fecha_lista;
                    }
                    catch
                    {
                        return f_fecha_lista;
                    }
                }
            }
        }

        public string BuscarCuenta(int? PCODIGO, Usuario pusuario)
        {
            if (PCODIGO == null)
                return null;
            string valor = null;
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select c.cod_cuenta From cuentas c Where c.codigo = " + PCODIGO;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_cuenta"] != DBNull.Value) valor = Convert.ToString(resultado["cod_cuenta"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch
                    {
                        return valor;
                    }
                }
            }
        }

        public lineascredito Consultarlineascredito(string pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            lineascredito entidad = new lineascredito();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM lineascredito WHERE COD_LINEA_CREDITO = '" + pId.ToString() + "' ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_LINEA"] != DBNull.Value) entidad.tipo_linea = Convert.ToInt32(resultado["TIPO_LINEA"]);
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt32(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["TIPO_CUPO"] != DBNull.Value) entidad.tipo_cupo = Convert.ToInt32(resultado["TIPO_CUPO"]);
                            if (resultado["RECOGE_SALDOS"] != DBNull.Value) entidad.recoge_saldos = Convert.ToInt32(resultado["RECOGE_SALDOS"]);
                            if (resultado["COBRA_MORA"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt32(resultado["COBRA_MORA"]);
                            if (resultado["TIPO_REFINANCIA"] != DBNull.Value) entidad.tipo_refinancia = Convert.ToInt32(resultado["TIPO_REFINANCIA"]);
                            if (resultado["MINIMO_REFINANCIA"] != DBNull.Value) entidad.minimo_refinancia = Convert.ToDecimal(resultado["MINIMO_REFINANCIA"]);
                            if (resultado["MAXIMO_REFINANCIA"] != DBNull.Value) entidad.maximo_refinancia = Convert.ToDecimal(resultado["MAXIMO_REFINANCIA"]);
                            if (resultado["MANEJA_PERGRACIA"] != DBNull.Value) entidad.maneja_pergracia = Convert.ToString(resultado["MANEJA_PERGRACIA"]);
                            if (resultado["PERIODO_GRACIA"] != DBNull.Value) entidad.periodo_gracia = Convert.ToInt32(resultado["PERIODO_GRACIA"]);
                            if (resultado["TIPO_PERIODIC_GRACIA"] != DBNull.Value) entidad.tipo_periodic_gracia = Convert.ToString(resultado["TIPO_PERIODIC_GRACIA"]);
                            if (resultado["MODIFICA_DATOS"] != DBNull.Value) entidad.modifica_datos = Convert.ToString(resultado["MODIFICA_DATOS"]);
                            if (resultado["MODIFICA_FECHA_PAGO"] != DBNull.Value) entidad.modifica_fecha_pago = Convert.ToString(resultado["MODIFICA_FECHA_PAGO"]);
                            if (resultado["GARANTIA_REQUERIDA"] != DBNull.Value) entidad.garantia_requerida = Convert.ToString(resultado["GARANTIA_REQUERIDA"]);
                            if (resultado["TIPO_CAPITALIZACION"] != DBNull.Value) entidad.tipo_capitalizacion = Convert.ToInt32(resultado["TIPO_CAPITALIZACION"]);
                            if (resultado["CUOTAS_EXTRAS"] != DBNull.Value) entidad.cuotas_extras = Convert.ToInt32(resultado["CUOTAS_EXTRAS"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt32(resultado["COD_CLASIFICA"]);
                            if (resultado["NUMERO_CODEUDORES"] != DBNull.Value) entidad.numero_codeudores = Convert.ToInt32(resultado["NUMERO_CODEUDORES"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt32(resultado["COD_MONEDA"]);
                            if (resultado["PORC_CORTO"] != DBNull.Value) entidad.porc_corto = Convert.ToDecimal(resultado["PORC_CORTO"]);
                            if (resultado["TIPO_AMORTIZA"] != DBNull.Value) entidad.tipo_amortiza = Convert.ToInt32(resultado["TIPO_AMORTIZA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["APROBAR_AVANCES"] != DBNull.Value) entidad.aprobar_avances = Convert.ToInt32(resultado["APROBAR_AVANCES"]);
                            if (resultado["DESEMBOLSAR_A_AHORROS"] != DBNull.Value) entidad.desembolsar_a_ahorros = Convert.ToInt32(resultado["DESEMBOLSAR_A_AHORROS"]);
                            if (resultado["PLAZO_A_DIFERIR"] != DBNull.Value) entidad.plazo_a_diferir = Convert.ToInt32(resultado["PLAZO_A_DIFERIR"]);
                            if (resultado["APLICA_TERCERO"] != DBNull.Value) entidad.aplica_tercero = Convert.ToInt32(resultado["APLICA_TERCERO"]);
                            if (resultado["APLICA_ASOCIADO"] != DBNull.Value) entidad.aplica_asociado = Convert.ToInt32(resultado["APLICA_ASOCIADO"]);
                            if (resultado["APLICA_EMPLEADO"] != DBNull.Value) entidad.aplica_empleado = Convert.ToInt32(resultado["APLICA_EMPLEADO"]);
                            if (resultado["MANEJA_EXCEPCION"] != DBNull.Value) entidad.maneja_excepcion = Convert.ToString(resultado["MANEJA_EXCEPCION"]);
                            if (resultado["CUOTAS_INTAJUSTE"] != DBNull.Value) entidad.cuotas_intajuste = Convert.ToInt32(resultado["CUOTAS_INTAJUSTE"]);
                            if (resultado["CREDITO_GERENCIAL"] != DBNull.Value) entidad.credito_gerencial = Convert.ToInt32(resultado["CREDITO_GERENCIAL"]);
                            if (resultado["ORDEN_SERVICIO"] != DBNull.Value) entidad.orden_servicio = Convert.ToInt32(resultado["ORDEN_SERVICIO"]);
                            if (resultado["EDUCATIVO"] != DBNull.Value) entidad.educativo = Convert.ToInt32(resultado["EDUCATIVO"]);
                            if (resultado["CREDITO_X_LINEA"] != DBNull.Value) entidad.credito_x_linea = Convert.ToInt32(resultado["CREDITO_X_LINEA"]);
                            if (resultado["MANEJA_AUXILIO"] != DBNull.Value) entidad.maneja_auxilio = Convert.ToInt32(resultado["MANEJA_AUXILIO"]);
                            if (resultado["PRIORIDAD"] != DBNull.Value) entidad.prioridad = Convert.ToInt32(resultado["PRIORIDAD"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public tipoliquidacion Consultartipoliquidacion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            tipoliquidacion entidad = new tipoliquidacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM tipoliquidacion WHERE TIPO_LIQUIDACION = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt32(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_CUOTA"] != DBNull.Value) entidad.tipo_cuota = Convert.ToInt32(resultado["TIPO_CUOTA"]);
                            if (resultado["TIPO_PAGO"] != DBNull.Value) entidad.tipo_pago = Convert.ToInt32(resultado["TIPO_PAGO"]);
                            if (resultado["TIPO_INTERES"] != DBNull.Value) entidad.tipo_interes = Convert.ToInt32(resultado["TIPO_INTERES"]);
                            if (resultado["TIPO_INTANT"] != DBNull.Value) entidad.tipo_intant = Convert.ToInt32(resultado["TIPO_INTANT"]);
                            if (resultado["VALOR_GRADIENTE"] != DBNull.Value) entidad.valor_gradiente = Convert.ToDecimal(resultado["VALOR_GRADIENTE"]);
                            if (resultado["TIP_GRA"] != DBNull.Value) entidad.tip_gra = Convert.ToInt32(resultado["TIP_GRA"]);
                            if (resultado["TIP_AMO"] != DBNull.Value) entidad.tip_amo = Convert.ToInt32(resultado["TIP_AMO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public List<atributoscredito> Listaratributoscredito(Int64 pnumero_radicacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<atributoscredito> lstatributoscredito = new List<atributoscredito>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM atributoscredito WHERE numero_radicacion = " + pnumero_radicacion + " ORDER BY COD_ATR ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            atributoscredito entidad = new atributoscredito();
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt32(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["CALCULO_ATR"] != DBNull.Value) entidad.calculo_atr = Convert.ToString(resultado["CALCULO_ATR"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["COBRA_MORA"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt32(resultado["COBRA_MORA"]);
                            if (resultado["SALDO_ATRIBUTO"] != DBNull.Value) entidad.saldo_atributo = Convert.ToDecimal(resultado["SALDO_ATRIBUTO"]);
                            if (resultado["CAUSADO_ATRIBUTO"] != DBNull.Value) entidad.causado_atributo = Convert.ToDecimal(resultado["CAUSADO_ATRIBUTO"]);
                            if (resultado["ORDEN_ATRIBUTO"] != DBNull.Value) entidad.orden_atributo = Convert.ToDecimal(resultado["ORDEN_ATRIBUTO"]);
                            lstatributoscredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstatributoscredito;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public int ConsultarNumeroCodeudores(Int64 pnumero_radicacion, Usuario pusuario)
        {
            int numero_codeudores = 0;
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select count (*) As num_codeu from codeudores Where numero_radicacion = " + pnumero_radicacion;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["num_codeu"] != DBNull.Value) numero_codeudores = Convert.ToInt32(resultado["num_codeu"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return numero_codeudores;
                    }
                    catch
                    {
                        return numero_codeudores;
                    }
                }
            }
        }

        public List<descuentoscredito> Listardescuentoscredito(Int64 pnumero_radicacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<descuentoscredito> lstdescuentoscredito = new List<descuentoscredito>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM descuentoscredito WHERE numero_radicacion = " + pnumero_radicacion + " ORDER BY COD_ATR ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            descuentoscredito entidad = new descuentoscredito();
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["TIPO_DESCUENTO"] != DBNull.Value) entidad.tipo_descuento = Convert.ToInt32(resultado["TIPO_DESCUENTO"]);
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt32(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["COBRA_MORA"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt32(resultado["COBRA_MORA"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToDecimal(resultado["NUMERO_CUOTAS"]);
                            if (resultado["FORMA_DESCUENTO"] != DBNull.Value) entidad.forma_descuento = Convert.ToInt32(resultado["FORMA_DESCUENTO"]);
                            if (resultado["TIPO_IMPUESTO"] != DBNull.Value) entidad.tipo_impuesto = Convert.ToInt32(resultado["TIPO_IMPUESTO"]);
                            lstdescuentoscredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstdescuentoscredito;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public int ConsultarNumeroCodeudores(int pn_cod_atr, decimal pn_monto, Usuario pusuario)
        {
            DbDataReader resultado;
            int valida = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select Count(*) As valida From ranval_atributo r Where r.cod_atr = " + pn_cod_atr + " And " + pn_monto + " Between r.desde And r.hasta And r.tipo_tope = 0";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valida"] != DBNull.Value) valida = Convert.ToInt32(resultado["valida"]);
                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return valida;
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
        }

        public List<atributo_depende> Listaratributo_depende(int pcod_atr, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<atributo_depende> lstatributo_depende = new List<atributo_depende>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM atributo_depende WHERE cod_atr " + pcod_atr + " ORDER BY DEPENDE ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            atributo_depende entidad = new atributo_depende();
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["DEPENDE"] != DBNull.Value) entidad.depende = Convert.ToInt32(resultado["DEPENDE"]);
                            lstatributo_depende.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstatributo_depende;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public List<Tuple<int?, DateTime?, decimal?, decimal?, int?>> ListarAmortizaCreAuditoriaDeUnaOperacionYCredito(long? pn_cod_ope, long? lnumero_radicacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            var listaAmortizaCreAuditoria = new List<Tuple<int?, DateTime?, decimal?, decimal?, int?>>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select d.cod_atr, d.fecha_cuota, d.valor, d.saldo, d.estado 
                                       From amortiza_cre_aud d
                                       Where d.cod_ope = " + pn_cod_ope + " And d.numero_radicacion = " + lnumero_radicacion +
                                       " Order by d.fecha_cuota, d.cod_atr";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            int? codigoAtributo = null;
                            DateTime? fechaCuota = null;
                            decimal? valor = null;
                            decimal? saldo = null;
                            int? estado = null;

                            if (resultado["COD_ATR"] != DBNull.Value) codigoAtributo = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["fecha_cuota"] != DBNull.Value) fechaCuota = Convert.ToDateTime(resultado["fecha_cuota"]);
                            if (resultado["valor"] != DBNull.Value) valor = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["saldo"] != DBNull.Value) saldo = Convert.ToDecimal(resultado["saldo"]);
                            if (resultado["estado"] != DBNull.Value) estado = Convert.ToInt32(resultado["estado"]);

                            listaAmortizaCreAuditoria.Add(Tuple.Create(codigoAtributo, fechaCuota, valor, saldo, estado));
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return listaAmortizaCreAuditoria;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public List<descuentosvalores> Listardescuentosvalores(Int64 numero_radicacion, int pcod_atr, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<descuentosvalores> lstdescuentosvalores = new List<descuentosvalores>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM descuentosvalores WHERE numero_radicacion = " + numero_radicacion + " AND cod_atr = " + pcod_atr + " ORDER BY  ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            descuentosvalores entidad = new descuentosvalores();
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["NUM_CUOTA"] != DBNull.Value) entidad.num_cuota = Convert.ToInt64(resultado["NUM_CUOTA"]);
                            if (resultado["VALOR_PRESENTE"] != DBNull.Value) entidad.valor_presente = Convert.ToDecimal(resultado["VALOR_PRESENTE"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            lstdescuentosvalores.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstdescuentosvalores;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public List<Tuple<int?, DateTime?, decimal?>> ListarDetalleTransaccionCreditoTotalizado(long? pn_cod_ope, long? lnumero_radicacion, long? lnum_tran, Usuario vUsuario)
        {
            DbDataReader resultado;
            var listaDetalleTransaccion = new List<Tuple<int?, DateTime?, decimal?>>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select d.cod_atr, d.fecha_cuota, Sum(d.valor) as valor 
                                        From det_tran_cred d
                                        Where d.cod_ope = " + pn_cod_ope + " And d.numero_radicacion = " + lnumero_radicacion + " And d.num_tran = " + lnum_tran +
                                        " Group by d.cod_atr, d.fecha_cuota Order by d.cod_atr, d.fecha_cuota ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            int? cod_atr = null;
                            DateTime? fecha_cuota = null;
                            decimal? valor = null;

                            if (resultado["cod_atr"] != DBNull.Value) cod_atr = Convert.ToInt32(resultado["cod_atr"]);
                            if (resultado["fecha_cuota"] != DBNull.Value) fecha_cuota = Convert.ToDateTime(resultado["fecha_cuota"]);
                            if (resultado["valor"] != DBNull.Value) valor = Convert.ToDecimal(resultado["valor"]);

                            listaDetalleTransaccion.Add(Tuple.Create(cod_atr, fecha_cuota, valor));
                        }
                        dbConnectionFactory.CerrarConexion(connection);

                        return listaDetalleTransaccion;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public List<Tuple<int?, int?>> ListarPrioridadAtributoLineaCredito(string s_cod_credi, Usuario vUsuario)
        {
            DbDataReader resultado;
            var listaDetalleTransaccion = new List<Tuple<int?, int?>>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select Distinct cod_atr, numero From prioridad_lin Where cod_linea_credito = " + s_cod_credi +
                                        @" Union
                                        Select cod_atr, 999 From atributos Where cod_atr Not In(Select cod_atr From prioridad_lin Where cod_linea_credito = " + s_cod_credi + " ) Order by 2 ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            int? cod_atr = null;
                            int? numero = null;

                            if (resultado["cod_atr"] != DBNull.Value) cod_atr = Convert.ToInt32(resultado["cod_atr"]);
                            if (resultado["numero"] != DBNull.Value) numero = Convert.ToInt32(resultado["numero"]);

                            listaDetalleTransaccion.Add(Tuple.Create(cod_atr, numero));
                        }
                        dbConnectionFactory.CerrarConexion(connection);

                        return listaDetalleTransaccion;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public List<cuotasextras> Listarcuotasextras(Int64 pnumero_radicacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<cuotasextras> lstcuotasextras = new List<cuotasextras>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM cuotasextras WHERE numero_radicacion = " + pnumero_radicacion + " ORDER BY COD_CUOTA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            cuotasextras entidad = new cuotasextras();
                            if (resultado["COD_CUOTA"] != DBNull.Value) entidad.cod_cuota = Convert.ToInt32(resultado["COD_CUOTA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt32(resultado["NUMERO_RADICACION"]);
                            if (resultado["FECHA_PAGO"] != DBNull.Value) entidad.fecha_pago = Convert.ToDateTime(resultado["FECHA_PAGO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["VALOR_CAPITAL"] != DBNull.Value) entidad.valor_capital = Convert.ToDecimal(resultado["VALOR_CAPITAL"]);
                            if (resultado["VALOR_INTERES"] != DBNull.Value) entidad.valor_interes = Convert.ToDecimal(resultado["VALOR_INTERES"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["SALDO_CAPITAL"]);
                            if (resultado["SALDO_INTERES"] != DBNull.Value) entidad.saldo_interes = Convert.ToDecimal(resultado["SALDO_INTERES"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            lstcuotasextras.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstcuotasextras;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public List<rangosatributos> Listarrangosatributos(string pcod_linea_credito, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<rangosatributos> lstrangosatributos = new List<rangosatributos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM rangosatributos '" + pcod_linea_credito + "' ORDER BY COD_RANGO_ATR ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            rangosatributos entidad = new rangosatributos();
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["COD_RANGO_ATR"] != DBNull.Value) entidad.cod_rango_atr = Convert.ToInt32(resultado["COD_RANGO_ATR"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            lstrangosatributos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstrangosatributos;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public List<rangostopes> Listarrangostopes(string pcod_linea_credito, int pcod_rango, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<rangostopes> lstrangostopes = new List<rangostopes>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM rangostopes '" + pcod_linea_credito + "' AND cod_rango_atr = " + pcod_rango + " ORDER BY IDTOPE ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            rangostopes entidad = new rangostopes();
                            if (resultado["IDTOPE"] != DBNull.Value) entidad.idtope = Convert.ToInt32(resultado["IDTOPE"]);
                            if (resultado["COD_RANGO_ATR"] != DBNull.Value) entidad.cod_rango_atr = Convert.ToInt32(resultado["COD_RANGO_ATR"]);
                            if (resultado["MINIMO"] != DBNull.Value) entidad.minimo = Convert.ToString(resultado["MINIMO"]);
                            if (resultado["MAXIMO"] != DBNull.Value) entidad.maximo = Convert.ToString(resultado["MAXIMO"]);
                            if (resultado["TIPO_TOPE"] != DBNull.Value) entidad.tipo_tope = Convert.ToInt32(resultado["TIPO_TOPE"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            lstrangostopes.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstrangostopes;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public DateTime? ConsultarFechaAfiliacion(Int64 pn_cod_deudor, Usuario pusuario)
        {
            DbDataReader resultado;
            DateTime? fecha_afiliacion = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select Max(fecha_afiliacion) As fecha_afiliacion From persona_afiliacion Where cod_persona = " + pn_cod_deudor + " And estado = 'A'";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["fecha_afiliacion"] != DBNull.Value) fecha_afiliacion = Convert.ToDateTime(resultado["fecha_afiliacion"]);
                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return fecha_afiliacion;
                    }
                    catch
                    {
                        return fecha_afiliacion;
                    }
                }
            }
        }

        public decimal? ConsultarSaldoAportes(Int64 pn_cod_deudor, Usuario pusuario)
        {
            DbDataReader resultado;
            decimal? saldo = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select Sum(saldo) As saldo From aporte Where cod_persona = " + pn_cod_deudor + " And estado = '1'";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["saldo"] != DBNull.Value) saldo = Convert.ToDecimal(resultado["saldo"]);
                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return saldo;
                    }
                    catch
                    {
                        return saldo;
                    }
                }
            }
        }

        public List<atributoslinea> Listaratributoslinea(string s_cod_credi, int cod_rango, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<atributoslinea> lstatributoslinea = new List<atributoslinea>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select cod_atr, calculo_atr, tasa, tipo_tasa, tipo_historico, desviacion,  1, 1, 0, cobra_mora  
                                        From atributoslinea Where cod_linea_credito = '" + s_cod_credi + "' And cod_rango_atr = " + cod_rango + " Order By 1";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            atributoslinea entidad = new atributoslinea();
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["COD_RANGO_ATR"] != DBNull.Value) entidad.cod_rango_atr = Convert.ToInt32(resultado["COD_RANGO_ATR"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["CALCULO_ATR"] != DBNull.Value) entidad.calculo_atr = Convert.ToString(resultado["CALCULO_ATR"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["COBRA_MORA"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt32(resultado["COBRA_MORA"]);
                            lstatributoslinea.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstatributoslinea;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public List<descuentoslinea> Listardescuentoslinea(string s_cod_credi, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<descuentoslinea> lstdescuentoslinea = new List<descuentoslinea>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select cod_atr, tipo_descuento, valor, tipo_liquidacion, forma_descuento, cobra_mora, numero_cuotas  
                                        From descuentoslinea Where cod_linea_credito = '" + s_cod_credi + "' Order By 1";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            descuentoslinea entidad = new descuentoslinea();
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt32(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["COBRA_MORA"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt32(resultado["COBRA_MORA"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt32(resultado["NUMERO_CUOTAS"]);
                            if (resultado["FORMA_DESCUENTO"] != DBNull.Value) entidad.forma_descuento = Convert.ToString(resultado["FORMA_DESCUENTO"]);
                            if (resultado["TIPO_IMPUESTO"] != DBNull.Value) entidad.tipo_impuesto = Convert.ToInt32(resultado["TIPO_IMPUESTO"]);
                            if (resultado["TIPO_DESCUENTO"] != DBNull.Value) entidad.tipo_descuento = Convert.ToInt32(resultado["TIPO_DESCUENTO"]);
                            if (resultado["MODIFICA"] != DBNull.Value) entidad.modifica = Convert.ToInt32(resultado["MODIFICA"]);
                            lstdescuentoslinea.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstdescuentoslinea;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public int? ConsultarHistoricoClasifica(int? n_clasificacion, Usuario pusuario)
        {
            if (n_clasificacion == null)
                return null;
            DbDataReader resultado;
            int? tipo_historico = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select tipo_historico From clasificacion Where cod_clasifica = " + n_clasificacion;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["tipo_historico"] != DBNull.Value) tipo_historico = Convert.ToInt32(resultado["tipo_historico"]);
                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return tipo_historico;
                    }
                    catch
                    {
                        return tipo_historico;
                    }
                }
            }
        }

        public bool ConsultarTasaUsura(DateTime? f_fec_apro, int? n_tipo_tasa_usura, ref decimal? valor, ref int? tipo_tasa, Usuario pusuario)
        {
            if (f_fec_apro == null)
                return false;
            DbDataReader resultado;
            valor = null;
            tipo_tasa = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            cmdTransaccionFactory.CommandText = @"Select h.valor, t.tipo_tasa from historicotasa h, tipotasahist  t 
                                                                Where To_Date('" + Convert.ToDateTime(f_fec_apro).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') between h.fecha_inicial and h.fecha_final and h.tipo_historico = " + n_tipo_tasa_usura + " and h.tipo_historico = t.tipo_historico";
                        else
                            cmdTransaccionFactory.CommandText = @"Select h.valor, t.tipo_tasa from historicotasa h, tipotasahist  t 
                                                                Where '" + f_fec_apro + "' between h.fecha_inicial and h.fecha_final and h.tipo_historico = " + n_tipo_tasa_usura + " and h.tipo_historico = t.tipo_historico";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) valor = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["tipo_tasa"] != DBNull.Value) tipo_tasa = Convert.ToInt32(resultado["tipo_tasa"]);
                            dbConnectionFactory.CerrarConexion(connection);
                            return true;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return false;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public int? ConsultarAtributoDepende(int? pn_cod_atr, Usuario pusuario)
        {
            if (pn_cod_atr == null)
                return null;
            DbDataReader resultado;
            int? depende = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select depende from atributos where cod_atr = " + pn_cod_atr;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["depende"] != DBNull.Value) depende = Convert.ToInt32(resultado["depende"]);
                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return depende;
                    }
                    catch
                    {
                        return depende;
                    }
                }
            }
        }

        public bool ConsultaCobroPrejuridico(int? pn_dias_ven, ref decimal? n_formaCatg, ref decimal? n_valorCatg, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select forma_cobro, Case forma_cobro When 0 ) { porcentaje } else { valor End As n_valorCatg From parametro_cobro_prejuridico Where " + pn_dias_ven + " between StrToNumber(minimo)and StrToNumber(maximo) and tipo_cobro = 0";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["forma_cobro"] != DBNull.Value) n_formaCatg = Convert.ToInt32(resultado["forma_cobro"]);
                            if (resultado["n_valorCatg"] != DBNull.Value) n_valorCatg = Convert.ToDecimal(resultado["n_valorCatg"]);
                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public bool ConsultarAmortizaCreditoDeUnaCuotaYAtributo(long? lnumero_radicacion, int? lcod_atr_cuota, DateTime? lfecha_cuota, ref decimal? lsaldo_ac, ref decimal? ltotal_ac, ref long? lnumreg_ac, Usuario usuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = @"Select Sum(a.saldo) as Saldo , Sum(a.valor) as valor, Count(*) as contador
                                   From amortiza_cre a Where numero_radicacion = " + lnumero_radicacion + " And cod_atr = " + lcod_atr_cuota + " And fecha_cuota = " + lfecha_cuota;

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["Saldo"] != DBNull.Value) lsaldo_ac = Convert.ToDecimal(resultado["Saldo"]);
                            if (resultado["valor"] != DBNull.Value) ltotal_ac = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["contador"] != DBNull.Value) lnumreg_ac = Convert.ToInt64(resultado["contador"]);

                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }


        //Revisar------------------------------

        public bool AtributoGracia(int? n_atrib_gra, ref string s_nom_atr, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select nombre from atributos where cod_atr =" + n_atrib_gra;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["nombre"] != DBNull.Value) s_nom_atr = Convert.ToString(resultado["nombre"]);
                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public bool CreditoRestructurado(string s_cod_credi, ref string sAux, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select cod_linea_credito From parametros_linea Where cod_linea_credito = " + s_cod_credi + " And cod_parametro = 230 And valor = '1'";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_linea_credito"] != DBNull.Value) sAux = Convert.ToString(resultado["cod_linea_credito"]);
                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public bool CreditoRestructuradosuma(Int64? n_radic, ref decimal? sn_val_atr_rees, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select Sum (valorrecoge - saldocapital) as valor From creditosrecogidos Where numero_radicacion = " + n_radic;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) sn_val_atr_rees = Convert.ToDecimal(resultado["valor"]);
                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public bool tipo_Hist_Tasa(DateTime? g_f_usura, decimal? n_usura, ref decimal? n_tasa_usura, ref int? n_tipo_tasa, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select h.valor as valor, t.tipo_tasa as tipo_tasa From historicotasa h, tipotasahist t Where " + g_f_usura + " between h.fecha_inicial and h.fecha_final And h.tipo_historico = " + n_usura + " And h.tipo_historico = t.tipo_historico";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) n_tasa_usura = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["tipo_tasa"] != DBNull.Value) n_tipo_tasa = Convert.ToInt32(resultado["tipo_tasa"]);

                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public bool SaldoCapital(Int64? n_radic, DateTime? rf_f_prox_pago, ref decimal? n_sum_tf, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select sum(saldo_capital)as Saldo From cuotasextras Where numero_radicacion = " + n_radic + " And fecha_pago < " + rf_f_prox_pago;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["Saldo"] != DBNull.Value) n_sum_tf = Convert.ToDecimal(resultado["Saldo"]);


                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public bool SaldoCapitalRangoFechaProximoPago(Int64? n_radic, DateTime? f_fec_ant, DateTime? rf_f_prox_pago, ref decimal? n_sum_tf, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select sum(saldo_capital)as Saldo From cuotasextras Where numero_radicacion = " + n_radic + " And fecha_pago < " + f_fec_ant + " And fecha_pago < " + rf_f_prox_pago;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["Saldo"] != DBNull.Value) n_sum_tf = Convert.ToDecimal(resultado["Saldo"]);


                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public bool ConsultarValorTransaccionCredito(Int64? n_radic, DateTime? f_fecha_pago, ref decimal? n_sum_tf, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select sum (valor) From tran_cred t, operacion o Where t.cod_ope = o.cod_ope And t.numero_radicacion = " + n_radic + " And t.tipo_tran = 12 And t.cod_atr = 1 And o.fecha_oper > " + f_fecha_pago;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["n_sum_tf"] != DBNull.Value) n_sum_tf = Convert.ToDecimal(resultado["n_sum_tf"]);

                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public bool LineaRestructurada(string s_cod_credi, ref string sLinea, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select cod_linea_credito from parametros_linea where cod_linea_credito = '" + s_cod_credi + "' And cod_parametro = 230 And valor = '1'";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_linea_credito"] != DBNull.Value) sLinea = Convert.ToString(resultado["cod_linea_credito"]);
                            return true;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return false;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public DateTime? DatosFechaCierre(Int64? n_radic, DateTime? f_fecha_pago, Usuario pusuario)
        {
            DbDataReader resultado;
            DateTime? f_ultcie = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select max(fecha_historico) as Fecha_Hist From historico_cre Where numero_radicacion = " + n_radic + " And fecha_historico <=" + f_fecha_pago;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["Fecha_Hist"] != DBNull.Value) f_ultcie = Convert.ToDateTime(resultado["Fecha_Hist"]);
                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return f_ultcie;
                    }
                    catch
                    {
                        return f_ultcie;
                    }
                }
            }
        }

        public bool Proximo_Pago(Int64? n_radic, DateTime? f_UltCie, ref DateTime? f_prox_pag, ref decimal? n_saldo, ref DateTime? f_ult_pago, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select fecha_proximo_pago, saldo_capital, fecha_ultimo_pago From historico_cre Where numero_radicacion = " + n_radic + " And fecha_historico = " + f_UltCie;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) f_prox_pag = Convert.ToDateTime(resultado["fecha_proximo_pago"]);
                            if (resultado["saldo_capital"] != DBNull.Value) n_saldo = Convert.ToDecimal(resultado["saldo_capital"]);
                            if (resultado["fecha_ultimo_pago"] != DBNull.Value) f_ult_pago = Convert.ToDateTime(resultado["fecha_ultimo_pago"]);

                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public bool Dias_Habiles(int? n_mes_pago, int? n_ano_pago, int? n_dia_pago, ref string sLinea, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select mes from dias_no_habiles where mes = " + n_mes_pago + " and ano = " + n_ano_pago + " and dia_festivo =" + n_dia_pago;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["mes"] != DBNull.Value) sLinea = Convert.ToString(resultado["mes"]);

                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public DateTime? DeterminaCobroPrejuridico(ref DateTime? f_fecha_pago, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select Max(fecha)as Fecha From cierea Where tipo = 'R' And estado = 'D'";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["Fecha"] != DBNull.Value) f_fecha_pago = Convert.ToDateTime(resultado["Fecha"]);
                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return f_fecha_pago;
                    }
                    catch
                    {
                        return f_fecha_pago;
                    }
                }
            }
        }

        public int? DiasMora(DateTime? f_fecha_ult_cierre, Int64? n_radic, Usuario pusuario)
        {
            DbDataReader resultado;
            int? n_dias_mora_cierre = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select h.dias_mora as DiasMora From historico_cre h Where h.fecha_historico = " + f_fecha_ult_cierre + " And h.numero_radicacion =" + n_radic;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["DiasMora"] != DBNull.Value) n_dias_mora_cierre = Convert.ToInt32(resultado["DiasMora"]);
                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return n_dias_mora_cierre;
                    }
                    catch
                    {
                        return n_dias_mora_cierre;
                    }
                }
            }
        }

        public bool TipoHistorico(int? n_clasificacion, ref int? n_tipo_tasa_usura, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select tipo_historico From clasificacion Where cod_clasifica =" + n_clasificacion;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["tipo_historico"] != DBNull.Value) n_tipo_tasa_usura = Convert.ToInt32(resultado["tipo_historico"]);
                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public bool FechaSaldosPend(Int64? n_radic, DateTime? rf_f_prox_pago, ref DateTime? fecha_pendiente, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select Min(fecha_cuota) as FechaMin From amortiza_cre Where numero_radicacion = " + n_radic + " And fecha_cuota >= " + rf_f_prox_pago + " And saldo != 0";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["FechaMin"] != DBNull.Value) fecha_pendiente = Convert.ToDateTime(resultado["FechaMin"]);
                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public List<Tuple<DateTime?, int?, decimal?, decimal?, string>> InfCargaAmortiza(Int64? n_radic, DateTime? pf_fecha_cuota, ref DateTime? f_fecha_cuota, ref int? n_cod_atr, ref decimal? n_valor, ref decimal? n_saldo, ref string s_estado, Usuario pusuario)
        {
            var listaAmortiza = new List<Tuple<DateTime?, int?, decimal?, decimal?, string>>();

            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = " Select fecha_cuota, cod_atr, valor, saldo, estado From amortiza_cre Where numero_radicacion =" + n_radic + " And fecha_cuota >=" + pf_fecha_cuota + " Order by fecha_cuota, cod_atr";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (resultado["fecha_cuota"] != DBNull.Value) f_fecha_cuota = Convert.ToDateTime(resultado["fecha_cuota"]);
                            if (resultado["cod_atr"] != DBNull.Value) n_cod_atr = Convert.ToInt32(resultado["cod_atr"]);
                            if (resultado["valor"] != DBNull.Value) n_valor = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["saldo"] != DBNull.Value) n_saldo = Convert.ToDecimal(resultado["saldo"]);
                            if (resultado["estado"] != DBNull.Value) s_estado = Convert.ToString(resultado["estado"]);

                            listaAmortiza.Add(Tuple.Create(f_fecha_cuota, n_cod_atr, n_valor, n_saldo, s_estado));
                        }

                        return listaAmortiza;
                    }
                    catch
                    {
                        return null;
                    }
                    finally
                    {
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                }
            }
        }

        public List<DetalleMoraCredito> ListarDetalleMoraCredito(Int64? n_radic, DateTime? pf_fecha_cuota, Usuario pusuario)
        {
            var listaDetalleMora = new List<DetalleMoraCredito>();

            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = @"Select fecha_cuota, cod_atr, valor, saldo, fecha_ini, fecha_fin, estado, dias_mora
                                   From det_mora_cre
                                   Where numero_radicacion = " + n_radic + @" and fecha_cuota >= " + pf_fecha_cuota + @" and estado = '1'
                                   Order by fecha_cuota, cod_atr, fecha_ini ";

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = " Select fecha_cuota, cod_atr, valor, saldo, estado From amortiza_cre Where numero_radicacion =" + n_radic + " And fecha_cuota >=" + pf_fecha_cuota + " Order by fecha_cuota, cod_atr";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DetalleMoraCredito detalle = new DetalleMoraCredito();

                            if (resultado["fecha_cuota"] != DBNull.Value) detalle.f_fecha_cuota = Convert.ToDateTime(resultado["fecha_cuota"]);
                            if (resultado["cod_atr"] != DBNull.Value) detalle.n_cod_atr = Convert.ToInt32(resultado["cod_atr"]);
                            if (resultado["valor"] != DBNull.Value) detalle.n_valor = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["saldo"] != DBNull.Value) detalle.n_saldo = Convert.ToDecimal(resultado["saldo"]);
                            if (resultado["fecha_ini"] != DBNull.Value) detalle.f_fecha_ini = Convert.ToDateTime(resultado["fecha_ini"]);
                            if (resultado["fecha_fin"] != DBNull.Value) detalle.f_fecha_fin = Convert.ToDateTime(resultado["fecha_fin"]);
                            if (resultado["estado"] != DBNull.Value) detalle.s_estado = Convert.ToString(resultado["estado"]);
                            if (resultado["dias_mora"] != DBNull.Value) detalle.dias_mora = Convert.ToInt32(resultado["dias_mora"]);

                            listaDetalleMora.Add(detalle);
                        }

                        return listaDetalleMora;
                    }
                    catch
                    {
                        return null;
                    }
                    finally
                    {
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                }
            }
        }

        public decimal? ValorAmortiza(Int64? n_radic, DateTime? f_fecha_cuota, int? n_cod_atr, DateTime? pf_fecha_pago, Usuario pusuario)
        {
            DbDataReader resultado;
            decimal? n_valor_pago = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select sum(r.valor) as valor
                                      from det_tran_cred r , operacion o
                                      Where r.cod_ope = o.cod_ope
                                      and r.numero_radicacion = " + n_radic +
                                      @"And r.fecha_cuota = " + f_fecha_cuota +
                                      "and r.cod_atr = " + n_cod_atr +
                                      "and o.fecha_oper > " + pf_fecha_pago +
                                      "And o.cod_ope Not In (Select a.cod_ope_anula from operacion_anulada a, operacion b where a.cod_ope = b.cod_ope And a.cod_ope_anula = o.cod_ope and BOFunciones.Trunc(b.fecha_oper) <= " + pf_fecha_pago + ")";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) n_valor_pago = Convert.ToDecimal(resultado["valor"]);


                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return n_valor_pago;
                    }
                    catch
                    {
                        return n_valor_pago;
                    }
                }
            }
        }


        public List<cuotasextras> ListarTEMPTerminosFijos(Usuario vUsuario)
        {
            DbDataReader resultado;
            List<cuotasextras> lstcuotasextras = new List<cuotasextras>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select  FECHA as fecha_pago, VALOR, VALOR as valor_capital, VALOR as saldo_capital, 0 as valor_interes, 0 as saldo_interes, TIP_FOR_PAG as forma_pago, IDCUOTAEXTRA
                                        from TEMP_CUOTASEXTRAS Order by FECHA asc ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            // f_ter, n_ter, n_valcap_ter, n_saldo_ter, n_valint_ter, n_salint_ter, n_forpag_ter, n_num_ter
                            cuotasextras entidad = new cuotasextras();
                            if (resultado["FECHA_PAGO"] != DBNull.Value) entidad.fecha_pago = Convert.ToDateTime(resultado["FECHA_PAGO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["VALOR_CAPITAL"] != DBNull.Value) entidad.valor_capital = Convert.ToDecimal(resultado["VALOR_CAPITAL"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["SALDO_CAPITAL"]);
                            if (resultado["VALOR_INTERES"] != DBNull.Value) entidad.valor_interes = Convert.ToDecimal(resultado["VALOR_INTERES"]);
                            if (resultado["SALDO_INTERES"] != DBNull.Value) entidad.saldo_interes = Convert.ToDecimal(resultado["SALDO_INTERES"]);
                            if (resultado["TIP_FOR_PAG"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["TIP_FOR_PAG"]);
                            if (resultado["IDCUOTAEXTRA"] != DBNull.Value) entidad.cod_cuota = Convert.ToInt32(resultado["IDCUOTAEXTRA"]);
                            lstcuotasextras.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstcuotasextras;
                    }
                    catch
                    {
                        return lstcuotasextras;
                    }
                }
            }
        }

        public void ConsultarValorPagarCredito(long? numeroRadicacion, ref decimal? pcapital, ref decimal? pintcte, ref decimal? pintmora, ref decimal? potros, ref decimal? ptotal, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = @"Select Sum(Case ac.cod_atr When 1 Then ac.valor Else 0 End) capital,
                                    Sum(Case ac.cod_atr When 2 Then ac.valor Else 0 End) intcte,
                                    Sum(Case ac.cod_atr When 3 Then ac.valor Else 0 End) intmora,
                                    Sum(Case ac.cod_atr When 1  Then 0  When 2  Then 0  When 3  Then 0 Else ac.valor End) otros,
                                    Sum(ac.valor) total
                                    from temp_pagar ac
                                    where numero_radicacion = " + numeroRadicacion;

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["capital"] != DBNull.Value) pcapital = Convert.ToDecimal(resultado["capital"]);
                            if (resultado["intcte"] != DBNull.Value) pintcte = Convert.ToDecimal(resultado["intcte"]);
                            if (resultado["intmora"] != DBNull.Value) pintmora = Convert.ToDecimal(resultado["intmora"]);
                            if (resultado["otros"] != DBNull.Value) potros = Convert.ToDecimal(resultado["otros"]);
                            if (resultado["total"] != DBNull.Value) ptotal = Convert.ToInt32(resultado["total"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public DateTime? ConsultarFechaUltimoCierre(Usuario pusuario)
        {
            DateTime? fechaUltimoCierre = null;

            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = @"Select Max(fecha) as fecha_cierre From cierea Where tipo = 'R' and estado = 'D' ";

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["fecha_cierre"] != DBNull.Value) fechaUltimoCierre = Convert.ToDateTime(resultado["fecha_cierre"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return fechaUltimoCierre;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public int ConsultarDiasDeUnaPeriodicidad(int? codigoPeriodicidad, Usuario pusuario)
        {
            int numeroDeDias = 0;

            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = @"Select numero_dias From periodicidad Where cod_periodicidad = " + codigoPeriodicidad;

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["numero_dias"] != DBNull.Value) numeroDeDias = Convert.ToInt32(resultado["numero_dias"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return numeroDeDias;
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
        }

        public int ConsultarSiguienteValorDetalleMoraSecuencia(Usuario pusuario)
        {
            int consecutivo = 0;

            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = @"Select sq_det_mora_cre.nextval as consecutivo From dual";

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["consecutivo"] != DBNull.Value) consecutivo = Convert.ToInt32(resultado["consecutivo"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return consecutivo;
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
        }

        public void ConsultarCaracteristicaOperacion(long? codigoOperacion, ref string codigoEstado, ref int? tipoOperacion, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = @"Select o.estado, o.tipo_ope From operacion o Where o.cod_ope = " + codigoOperacion;

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["estado"] != DBNull.Value) codigoEstado = Convert.ToString(resultado["estado"]);
                            if (resultado["tipo_ope"] != DBNull.Value) tipoOperacion = Convert.ToInt32(resultado["tipo_ope"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void ConsultarValorCuotaExtra(long? n_radic, long? n_num_ter, ref decimal? n_tot_abono, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = @"Select Sum(valor) as valor from Amortiza_CuotaExtra where numero_radicacion = " + n_radic + " and cod_cuota = " + n_num_ter;

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) n_tot_abono = Convert.ToDecimal(resultado["valor"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void ConsultarDetalleMora(long? n_radic, long? n_num_ter, ref decimal? n_tot_abono, Usuario pusuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = @"Select Sum(valor) as valor from Amortiza_CuotaExtra where numero_radicacion = " + n_radic + " and cod_cuota = " + n_num_ter;

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) n_tot_abono = Convert.ToDecimal(resultado["valor"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public List<Tuple<long?, decimal?, DateTime?>> ListarCuotasExtras(long? n_radic, Usuario pusuario)
        {
            var listaCuotasExtras = new List<Tuple<long?, decimal?, DateTime?>>();
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = @"Select cod_cuota, saldo_capital, fecha_pago From cuotasextras Where numero_radicacion = " + n_radic + " And saldo_capital > 0 Order By fecha_pago";

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            long? n_num_ter = null;
                            decimal? n_val_ter = null;
                            DateTime? f_pago = null;

                            if (resultado["cod_cuota"] != DBNull.Value) n_num_ter = Convert.ToInt64(resultado["cod_cuota"]);
                            if (resultado["saldo_capital"] != DBNull.Value) n_val_ter = Convert.ToDecimal(resultado["saldo_capital"]);
                            if (resultado["fecha_pago"] != DBNull.Value) f_pago = Convert.ToDateTime(resultado["fecha_pago"]);

                            listaCuotasExtras.Add(Tuple.Create(n_num_ter, n_val_ter, f_pago));
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return listaCuotasExtras;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public void ControlError(long? pn_radic, long? pn_cod_ope, string pError, int? pTipo, Usuario pusuario)
        {
            if (!pTipo.HasValue || pTipo == 0)
            {
                throw new Exception(pError + " Radic: " + pn_radic);
            }
            else
            {
                using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
                {
                    using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                    {
                        string sql = string.Format(@"Insert Into Temp_ErrorRecaudo Values({0}, '1', {1}, {2}, {3})", pn_cod_ope, pn_radic, pError, pTipo);

                        try
                        {
                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql;
                            cmdTransaccionFactory.ExecuteNonQuery();

                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }

        public void InsertarHistoricoAmortizaCre(DateTime? f_fecha_pago, int tipoCuota, long? n_radic, DateTime? f_fec_act, decimal? rn_cod_atributos, decimal? n_atributos, Usuario pusuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Insert Into Historico_Amortiza values(sq_Historico_Amortiza.nextval, {0}, {1}, {2}, {3}, {4}, {5}) ", f_fecha_pago, tipoCuota, n_radic, f_fec_act, rn_cod_atributos, n_atributos);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void ActualizarAmortizaCre(long? n_radic, DateTime? f_fecha_cuota, int? n_cod_atr, decimal? n_saldo, Usuario pusuario, bool aumenta = false)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Empty;

                    if (aumenta)
                    {
                        sql = string.Format(@"Update amortiza_cre Set amortiza_cre.saldo = amortiza_cre.saldo + {0} Where amortiza_cre.numero_radicacion = {1} and amortiza_cre.fecha_cuota = {2} and amortiza_cre.cod_atr = {3}", n_saldo, n_radic, f_fecha_cuota, n_cod_atr);
                    }
                    else
                    {
                        sql = string.Format(@"Update amortiza_cre Set amortiza_cre.saldo = {0} Where amortiza_cre.numero_radicacion = {1} and amortiza_cre.fecha_cuota = {2} and amortiza_cre.cod_atr = {3}", n_saldo, n_radic, f_fecha_cuota, n_cod_atr);
                    }

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void BorrarAmortizaCre(long? n_radic, DateTime? f_fecha_cuota, int? n_cod_atr, Usuario pusuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Delete From amortiza_cre Where numero_radicacion = {0} And fecha_cuota = {1} And cod_atr = {2}", n_radic, f_fecha_cuota, n_cod_atr);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void ActualizarEstadoTransaccionAnulada(long? numeroTransaccionParaActualizar, long? numeroTransaccionAnulacion, Usuario pusuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Update tran_cred Set estado = 2, num_tran_anula = {1} Where num_tran = {0}", numeroTransaccionParaActualizar, numeroTransaccionAnulacion);
                    
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void BorrarDetalleTransaccionCreditoPorNumeroTransaccion(long? lnum_tran, Usuario pusuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Delete from det_tran_cred Where num_tran = {0} ", lnum_tran);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void BorrarTransaccionCredito(long? lnum_tran, Usuario pusuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Delete from tran_cred Where num_tran = {0} ", lnum_tran);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void BorrarTodaAmortizacionDeUnCredito(long? n_radic, Usuario pusuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Delete From amortiza_cre Where numero_radicacion = {0}", n_radic);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void InsertarDetalleMoraCredito(int n_consecutivo, long? n_radic, DateTime? f_fecha_cuota, int? n_cod_atr, decimal? n_valor, decimal? n_saldo, decimal? n_tasa_int,
                                               decimal? n_valor_base, DateTime? f_fecha_ini, DateTime? f_fecha_fin, int? n_dias_mora, string s_estado, Usuario pusuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Insert Into det_mora_cre values({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10} ",
                                               n_consecutivo, n_radic, n_cod_atr, n_valor, n_saldo, n_tasa_int, n_valor_base, f_fecha_ini, f_fecha_fin, n_dias_mora, s_estado);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void ActualizarDetalleMoraCredito(long? n_radic, DateTime? f_fecha_cuota, int? n_cod_atr, decimal? n_saldo,
                                                 DateTime? f_fecha_ini, DateTime? f_fecha_fin, string s_estado, Usuario pusuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Update det_mora_cre Set saldo = {0}, estado = {1}, 
                                                where numero_radicacion = {2}, and fecha_cuota = {3}, and cod_atr = {4}, 
                                                and fecha_ini = {5}, and fecha_fin = {6} ",
                                                n_saldo, s_estado, n_radic, f_fecha_cuota, n_cod_atr, f_fecha_ini, f_fecha_fin);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void InsertarCreditoAuditoria(long? n_radic, long? pn_cod_ope, Usuario pusuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Insert Into credito_aud
                                                 Select sq_credito_aud.nextval, {0}, c.numero_radicacion, c.saldo_capital, c.otros_saldos, c.cuotas_pagadas, c.fecha_proximo_pago, c.fecha_ultimo_pago, c.estado, c.cuotas_pendientes
                                                 From credito c Where c.numero_radicacion = {1}", pn_cod_ope, n_radic);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void InsertarTransaccionAnulacion(long? lidtran, long? pn_cod_ope, long? lnum_tran, long? lnumero_radicacion, long? lcod_cliente, string lcod_linea_credito, int? ltipo_tran, int? lcod_atr, decimal? lvalor, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string manejoConsecutivo = "sq_tran_cred.nextval";

                    if (lidtran.HasValue && lidtran > 0)
                    {
                        manejoConsecutivo = lidtran.ToString();
                    }

                    string sql = string.Format(@"Insert Into tran_cred_anula Values({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})",
                                                manejoConsecutivo, pn_cod_ope, lnum_tran, lnumero_radicacion, lcod_cliente, lcod_linea_credito, ltipo_tran, lcod_atr, lvalor);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void InsertarAmortizaCreditoAuditoria(long? n_radic, long pn_cod_ope, Usuario pusuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Insert Into Amortiza_cre_aud
                                                 Select sq_amortiza_cre_aud.nextval, {0}, a.numero_radicacion, a.cod_atr, a.fecha_cuota, a.valor, a.saldo, a.estado
                                                 From amortiza_cre a Where a.numero_radicacion = {1}", pn_cod_ope, n_radic);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void InsertarAmortizaCredito(long? n_consecutivo, long? n_radic, int? n_cod_atr, DateTime? f_fecha_cuota, decimal? n_valor, decimal? n_saldo, decimal? n_saldo_base, decimal? n_tasa_int, int dias_base, string s_estado, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    // Evita tener que tener el siguiente consecutivo primero a la mano
                    string manejoConsecutivo = "sq_amortiza_cre.nextval";

                    if (n_consecutivo.HasValue && n_consecutivo > 0)
                    {
                        manejoConsecutivo = n_consecutivo.ToString();
                    }

                    string sql = string.Format(@"Insert Into amortiza_cre values({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9})",
                                                manejoConsecutivo, n_radic, n_cod_atr, f_fecha_cuota, n_valor, n_saldo, n_saldo_base, n_tasa_int, dias_base, s_estado);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void ActualizarCredito(int n_num_cuo, DateTime? f_proximo_pago, DateTime pf_fecha_pago, long? n_radic, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Update Credito Set Credito.cuotas_pagadas = Credito.cuotas_pagadas + {0}, Credito.fecha_proximo_pago = {1}, Credito.fecha_ultimo_pago = {2},  
                                                 Credito.cuotas_pendientes = Credito.cuotas_pendientes - {0}
                                                 Where Credito.numero_radicacion = {3} ",
                                                 n_num_cuo, f_proximo_pago, pf_fecha_pago, n_radic);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void ActualizarCredito(decimal? lsaldo_capital, decimal? lotros_saldos, int? lcuotas_pagadas, int? lcuotas_pendientes, DateTime? lfecha_proximo_pago, DateTime? lfecha_ultimo_pago, string lestadoc, long? lnumero_radicacion, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Update credito Set credito.saldo_capital = {0}, credito.otros_saldos = {1}, credito.cuotas_pagadas = {2},
                                                 credito.cuotas_pendientes = {3}, credito.fecha_proximo_pago = {4}, credito.fecha_ultimo_pago = {5},
                                                 credito.estado = {6}
                                                 Where credito.numero_radicacion = {7} ",
                                                 lsaldo_capital, lotros_saldos, lcuotas_pagadas, lcuotas_pendientes, lfecha_proximo_pago, lfecha_ultimo_pago, lestadoc, lnumero_radicacion);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void ActualizarSaldoCapitalCredito(decimal? lvalor, long? lnumero_radicacion, bool disminuye, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Empty;

                    if (disminuye)
                    {
                        sql = string.Format(@"Update credito Set credito.saldo_capital = credito.saldo_capital - {0}
                                             Where credito.numero_radicacion = {1} ",
                                             lvalor, lnumero_radicacion);
                    }
                    else
                    {
                        sql = string.Format(@"Update credito Set credito.saldo_capital = credito.saldo_capital + {0}
                                             Where credito.numero_radicacion = {1} ",
                                             lvalor, lnumero_radicacion);
                    }

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void ActualizarFechaUltimoPagoCredito(DateTime pf_fecha_pago, long? n_radic, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Update Credito Set Credito.fecha_ultimo_pago = {0} Where credito.numero_radicacion = {1}",
                                                 pf_fecha_pago, n_radic);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void InsertarTemporalErrorRecaudo(long? pn_cod_ope, string tipoProducto, long? pn_radic, string pError, int? pTipo, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Insert Into Temp_ErrorRecaudo Values({0}, {1}, {2}, {3}, {4})",
                                                 pn_cod_ope, tipoProducto, pn_radic, pError, pTipo);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void ActualizarEstadoCredito(string estado, long? n_radic, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Update credito Set credito.estado = {0} Where credito.numero_radicacion = {1} ",
                                                 estado, n_radic);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void ActualizarEstadoPersona(string estado, long? cod_persona, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Update persona Set persona.estado = {0} Where persona.cod_persona = {1}",
                                                 estado, cod_persona);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void InsertarNovedadCredito(long? n_nov_cre, int tipoNovedad, long pn_cod_usu, long? n_radic, DateTime? f_fec_act, DateTime? f_hor_act, string descripcion, string estado, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Insert Into novedad_cre Values({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}) ",
                                                 n_nov_cre, tipoNovedad, pn_cod_usu, n_radic, f_fec_act, f_hor_act, descripcion, estado);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void ActualizarValorCuotaCredito(decimal? l_cuota, long? n_radic, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Update credito Set credito.valor_cuota = {0} Where credito.numero_radicacion = {1}",
                                                 l_cuota, n_radic);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void ActualizarSaldoEstadoYFechaCredito(DateTime pf_fecha_pago, decimal? n_saldo, string s_estado, long? n_radic, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Update Credito Set fecha_ultimo_pago = {0}, saldo_capital = {1}, estado = {2} Where numero_radicacion = {3}",
                                                 pf_fecha_pago, n_saldo, s_estado, n_radic);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public TRAN_CRED InsertarTransaccionCredito(TRAN_CRED pTransac, Usuario vUsuario)
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
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
        }

        public void ActualizarTemporalRecogerAtributoCorriente(decimal? rn_tot_atributos, long? n_radic, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Update temp_recoger Set total_recoger = total_recoger + {0}, interes_corriente = interes_corriente + {0} Where numero_radicacion = {1})",
                                                 rn_tot_atributos, n_radic);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void ActualizarTemporalRecogerAtributoMora(decimal? rn_tot_atributos, long? n_radic, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Update temp_recoger Set total_recoger = total_recoger + {0}, interes_mora = interes_mora + {0} Where numero_radicacion = {1})",
                                                 rn_tot_atributos, n_radic);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void ActualizarTemporalRecogerAtributoSeguro(decimal? rn_tot_atributos, long? n_radic, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Update temp_recoger Set total_recoger = total_recoger + {0}, seguro = seguro + {0} Where numero_radicacion = {1})",
                                                 rn_tot_atributos, n_radic);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void ActualizarTemporalRecogerAtributoLeyPime(decimal? rn_tot_atributos, long? n_radic, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Update temp_recoger Set total_recoger = total_recoger + {0}, leymipyme = leymipyme + {0} Where numero_radicacion = {1})",
                                                 rn_tot_atributos, n_radic);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void ActualizarTemporalRecogerAtributoIvaLeyPime(decimal? rn_tot_atributos, long? n_radic, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Update temp_recoger Set total_recoger = total_recoger + {0}, iva_leymipyme = iva_leymipyme + {0} Where numero_radicacion = {1})",
                                                 rn_tot_atributos, n_radic);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void ActualizarTemporalRecogerAtributoOtros(decimal? rn_tot_atributos, long? n_radic, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Update temp_recoger Set total_recoger = total_recoger + {0}, otros = otros + {0} Where numero_radicacion = {1})",
                                                 rn_tot_atributos, n_radic);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void InsertarCuotasExtrasAuditoria(long? pn_cod_ope, DateTime? pf_fecha_pago, long? n_radic, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Insert Into cuotasextras_aud
                                                 Select sq_cuotasextras_aud.nextval, {0}, cod_cuota, numero_radicacion, {1}, saldo_capital, saldo_interes
                                                 From cuotasextras Where numero_radicacion = {2}",
                                                 pn_cod_ope, pf_fecha_pago, n_radic);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void InsertarAmortizaCuotasExtras(long? n_radic, DateTime pf_fecha_pago, long? n_num_ter, decimal? n_mora_tf, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Insert into Amortiza_cuotaextra 
                                                 Values(sq_Amortiza_cuotaextra.nextval, {0}, {1}, {2}, {3}) ",
                                                 n_radic, pf_fecha_pago, n_num_ter, n_mora_tf);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void ActualizarSaldoDeUnaCuotaExtra(decimal? n_pagado, long? n_num_ter, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Update cuotasextras set saldo_capital = {0} Where cod_cuota = {1} ", n_pagado, n_num_ter);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void InsertarTemporalPagar(long? n_radic, int? n_cod_atr, int? n_num_pago, DateTime? f_fecha_tran, decimal? n_valor_pago, decimal? n_valor_saldo, decimal? dias_mora, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Format(@"Insert Into temp_pagar Values({0}, {1}, {2}, {3}, {4}, {5}, {6})",
                                                n_radic, n_cod_atr, n_num_pago, f_fecha_tran, n_valor_pago, n_valor_saldo, dias_mora);

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void BorrarTodaTemporalPagar(Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = @"Delete from temp_pagar";

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public List<TransaccionCredito> ListarTransaccionesCredito(long? pn_cod_ope, Usuario usuario)
        {
            List<TransaccionCredito> listaTransacciones = new List<TransaccionCredito>();
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = @"Select t.num_tran, t.numero_radicacion, t.cod_cliente, t.cod_linea_credito, t.tipo_tran, t.cod_atr, t.valor, t.estado, t.num_tran_anula, r.tipo_mov
                                   From tran_cred t Left Join tipo_tran r On t.tipo_tran = r.tipo_tran Where t.cod_ope = " + pn_cod_ope;

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;

                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TransaccionCredito tranCredito = new TransaccionCredito();

                            if (resultado["num_tran"] != DBNull.Value) tranCredito.num_tran = Convert.ToInt64(resultado["num_tran"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) tranCredito.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["cod_cliente"] != DBNull.Value) tranCredito.cod_cliente = Convert.ToInt64(resultado["cod_cliente"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) tranCredito.cod_linea_credito = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["tipo_tran"] != DBNull.Value) tranCredito.tipo_tran = Convert.ToInt32(resultado["tipo_tran"]);
                            if (resultado["cod_atr"] != DBNull.Value) tranCredito.cod_atr = Convert.ToInt32(resultado["cod_atr"]);
                            if (resultado["valor"] != DBNull.Value) tranCredito.valor = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["estado"] != DBNull.Value) tranCredito.estado = Convert.ToInt64(resultado["estado"]);
                            if (resultado["num_tran_anula"] != DBNull.Value) tranCredito.num_tran_anula = Convert.ToInt64(resultado["num_tran_anula"]);
                            if (resultado["tipo_mov"] != DBNull.Value) tranCredito.tipo_mov = Convert.ToInt64(resultado["tipo_mov"]);

                            listaTransacciones.Add(tranCredito);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaTransacciones;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public void ConsultarRegistroAuditoriaCredito(long? pn_cod_ope, long? lnumero_radicacion, ref decimal? lsaldo_capital, ref decimal? lotros_saldos, ref int? lcuotas_pagadas, ref DateTime? lfecha_proximo_pago, ref DateTime? lfecha_ultimo_pago, ref string lestadoc, ref int? lcuotas_pendientes, Usuario usuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = @"Select c.saldo_capital, c.otros_saldos, c.cuotas_pagadas, c.fecha_proximo_pago, c.fecha_ultimo_pago, c.estado, c.cuotas_pendientes
                                  From credito_aud c
                                  Where c.cod_ope = " + pn_cod_ope + " And c.numero_radicacion = " + lnumero_radicacion + " Order by c.idauditoria";

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;

                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["saldo_capital"] != DBNull.Value) lsaldo_capital = Convert.ToDecimal(resultado["saldo_capital"]);
                            if (resultado["otros_saldos"] != DBNull.Value) lotros_saldos = Convert.ToDecimal(resultado["otros_saldos"]);
                            if (resultado["cuotas_pagadas"] != DBNull.Value) lcuotas_pagadas = Convert.ToInt32(resultado["cuotas_pagadas"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) lfecha_proximo_pago = Convert.ToDateTime(resultado["fecha_proximo_pago"]);
                            if (resultado["fecha_ultimo_pago"] != DBNull.Value) lfecha_ultimo_pago = Convert.ToDateTime(resultado["fecha_ultimo_pago"]);
                            if (resultado["estado"] != DBNull.Value) lestadoc = Convert.ToString(resultado["estado"]);
                            if (resultado["cuotas_pendientes"] != DBNull.Value) lcuotas_pendientes = Convert.ToInt32(resultado["cuotas_pendientes"]);
                        }
                    }
                    catch
                    {

                    }
                    finally
                    {
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                }
            }
        }

        public void ConsultarAtributoCreditoCorrienteDeUnCredito(long? n_radic, ref string lcalculo_atr, ref int? ltipo_tasa, ref decimal? ltasa, ref int? ltipo_historico, ref decimal? ldesviacion, Usuario usuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = @"Select calculo_atr, tipo_tasa, tasa, tipo_historico, desviacion
                                   From atributoscredito
                                   Where numero_radicacion = " + n_radic + " and cod_atr = AtrCorriente() ";

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;

                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["calculo_atr"] != DBNull.Value) lcalculo_atr = Convert.ToString(resultado["calculo_atr"]);
                            if (resultado["ltipo_tasa"] != DBNull.Value) ltipo_tasa = Convert.ToInt32(resultado["ltipo_tasa"]);
                            if (resultado["ltasa"] != DBNull.Value) ltasa = Convert.ToDecimal(resultado["ltasa"]);
                            if (resultado["ltipo_historico"] != DBNull.Value) ltipo_historico = Convert.ToInt32(resultado["ltipo_historico"]);
                            if (resultado["ldesviacion"] != DBNull.Value) ldesviacion = Convert.ToDecimal(resultado["ldesviacion"]);
                        }
                    }
                    catch
                    {

                    }
                    finally
                    {
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                }
            }
        }

        public void ActualizarSaldoAtributoCredito(decimal? lvalor, long? lnumero_radicacion, bool disminuye, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Empty;

                    if (disminuye)
                    {
                        sql = string.Format(@"Update atributoscredito Set saldo_atributo = saldo_atributo - {0}
                                              Where atributoscredito.numero_radicacion = {1} ", lvalor, lnumero_radicacion);
                    }
                    else
                    {
                        sql = string.Format(@"Update atributoscredito Set saldo_atributo = saldo_atributo + {0}
                                              Where atributoscredito.numero_radicacion = {1} ", lvalor, lnumero_radicacion);
                    }

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void ActualizarSaldoCuentasPorCobrar(decimal? lvalor_aplica, long? lidcuenta, bool aumenta, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = string.Empty;

                    if (aumenta)
                    {
                        sql = string.Format(@"Update cuenta_porcobrar_cre Set saldo = saldo + {0} Where idcuenta = {1} ", lvalor_aplica, lidcuenta);
                    }
                    else
                    {
                        sql = string.Format(@"Update cuenta_porcobrar_cre Set saldo = {0} Where idcuenta = {1} ", lvalor_aplica, lidcuenta);
                    }

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public List<Tuple<long?, decimal?>> ListarCuentaPorCobrarCredito(long? n_radic, Usuario usuario)
        {
            var listaTransacciones = new List<Tuple<long?, decimal?>>();

            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = @"Select idcuenta, total From cuenta_porcobrar_cre Where numero_radicacion = " + n_radic + " Order by idcuenta desc";

                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;

                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            long? idcuenta = null;
                            decimal? total = null;

                            if (resultado["idcuenta"] != DBNull.Value) idcuenta = Convert.ToInt64(resultado["idcuenta"]);
                            if (resultado["total"] != DBNull.Value) total = Convert.ToDecimal(resultado["total"]);

                            listaTransacciones.Add(Tuple.Create(idcuenta, total));
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaTransacciones;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }
    }
}