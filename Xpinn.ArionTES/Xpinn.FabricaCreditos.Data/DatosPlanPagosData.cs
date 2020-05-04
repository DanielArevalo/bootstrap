using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    public class DatosPlanPagosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public DatosPlanPagosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene la informacion de los creditos solicitados
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Datos del credito solicitado</returns>
        public List<DatosPlanPagos> ListarDatosPlanPagos(Credito pCredito, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DatosPlanPagos> lstPlanPagos = new List<DatosPlanPagos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        DbParameter pn_num_radic = cmdTransaccionFactory.CreateParameter();
                        pn_num_radic.ParameterName = "pn_num_radic";
                        pn_num_radic.Value = pCredito.numero_radicacion;
                        pn_num_radic.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pn_num_radic);
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_GENERAPLANPAGOS";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        string sql = "Select c.cod_atr, c.nom_atr, c.val_atr, c.tipo_atr From genera_descuentos_plan c Where c.numero_radicacion = " + pCredito.numero_radicacion + " Order by c.cod_atr, c.tipo_atr";
                        cmdTransaccionFactory.Parameters.Clear();
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        DatosPlanPagos pEntidadT = new DatosPlanPagos();
                        pEntidadT.lstDescuentos = new List<DescuentosCredito>();
                        pEntidadT.lstSumados = new List<DescuentosCredito>();
                        int contador = 0;
                        while (resultado.Read())
                        {                            
                            DescuentosCredito pEntidad = new DescuentosCredito();
                            // Asociar todos los valores a la entidad
                            if (resultado["cod_atr"] != DBNull.Value) pEntidad.cod_atr = Convert.ToInt32(resultado["cod_atr"]);
                            if (resultado["nom_atr"] != DBNull.Value) pEntidad.nom_atr = Convert.ToString(resultado["nom_atr"]);
                            if (resultado["val_atr"] != DBNull.Value) pEntidad.val_atr = Convert.ToInt64(resultado["val_atr"]);
                            if (resultado["tipo_atr"] != DBNull.Value) pEntidad.tipo_atr = Convert.ToInt32(resultado["tipo_atr"]);
                            pEntidadT.numerocuota = 0;
                            if (pEntidad.tipo_atr == null || pEntidad.tipo_atr == 1)
                                pEntidadT.lstDescuentos.Add(pEntidad);
                            else
                                pEntidadT.lstSumados.Add(pEntidad);
                            contador += 1;
                        }                        
                        lstPlanPagos.Add(pEntidadT);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosPlanPagosData", "ListarDatosPlanPagos", ex);
                        return null;
                    }
                }

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        string sql = "Select c.numerocuota, c.fechacuota, c.sal_ini, c.capital, c.int_1, c.int_2, c.int_3, c.int_4, c.int_5, c.int_6, c.int_7, c.int_8, c.int_9, c.int_10, c.int_11, c.int_12, c.int_13, c.int_14, c.int_15, c.total, c.sal_fin From genera_plan_pagos c Where c.numero_radicacion = " + pCredito.numero_radicacion + " Order by c.numerocuota, c.fechacuota, c.tipo desc";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DatosPlanPagos pEntidad = new DatosPlanPagos();
                            // Asociar todos los valores a la entidad
                            if (resultado["numerocuota"] != DBNull.Value) pEntidad.numerocuota = Convert.ToInt32(resultado["numerocuota"]);
                            if (resultado["fechacuota"] != DBNull.Value) pEntidad.fechacuota = Convert.ToDateTime(resultado["fechacuota"]);
                            if (resultado["sal_ini"] != DBNull.Value) pEntidad.sal_ini = Convert.ToInt64(resultado["sal_ini"]);
                            if (resultado["capital"] != DBNull.Value) pEntidad.capital = Convert.ToInt64(resultado["capital"]);
                            if (resultado["int_1"] != DBNull.Value) pEntidad.int_1 = Convert.ToInt64(resultado["int_1"]);
                            if (resultado["int_2"] != DBNull.Value) pEntidad.int_2 = Convert.ToInt64(resultado["int_2"]);
                            if (resultado["int_3"] != DBNull.Value) pEntidad.int_3 = Convert.ToInt64(resultado["int_3"]);
                            if (resultado["int_4"] != DBNull.Value) pEntidad.int_4 = Convert.ToInt64(resultado["int_4"]);
                            if (resultado["int_5"] != DBNull.Value) pEntidad.int_5 = Convert.ToInt64(resultado["int_5"]);
                            if (resultado["int_6"] != DBNull.Value) pEntidad.int_6 = Convert.ToInt64(resultado["int_6"]);
                            if (resultado["int_7"] != DBNull.Value) pEntidad.int_7 = Convert.ToInt64(resultado["int_7"]);
                            if (resultado["int_8"] != DBNull.Value) pEntidad.int_8 = Convert.ToInt64(resultado["int_8"]);
                            if (resultado["int_9"] != DBNull.Value) pEntidad.int_9 = Convert.ToInt64(resultado["int_9"]);
                            if (resultado["int_10"] != DBNull.Value) pEntidad.int_10 = Convert.ToInt64(resultado["int_10"]);
                            if (resultado["int_11"] != DBNull.Value) pEntidad.int_11 = Convert.ToInt64(resultado["int_11"]);
                            if (resultado["int_12"] != DBNull.Value) pEntidad.int_12 = Convert.ToInt64(resultado["int_12"]);
                            if (resultado["int_13"] != DBNull.Value) pEntidad.int_13 = Convert.ToInt64(resultado["int_13"]);
                            if (resultado["int_14"] != DBNull.Value) pEntidad.int_14 = Convert.ToInt64(resultado["int_14"]);
                            if (resultado["int_15"] != DBNull.Value) pEntidad.int_15 = Convert.ToInt64(resultado["int_15"]);
                            if (resultado["total"] != DBNull.Value) pEntidad.total = Convert.ToInt64(resultado["total"]);
                            if (resultado["sal_fin"] != DBNull.Value) pEntidad.sal_fin = Convert.ToInt64(resultado["sal_fin"]);
                            lstPlanPagos.Add(pEntidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);                        
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosPlanPagosData", "ListarDatosPlanPagos", ex);
                        return null;
                    }                    
                }                

                return lstPlanPagos; 
            }
        }

        public List<DatosPlanPagos> ListarDatosPlanPagosOriginal(Credito pCredito, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DatosPlanPagos> lstPlanPagos = new List<DatosPlanPagos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pn_num_radic = cmdTransaccionFactory.CreateParameter();
                        pn_num_radic.ParameterName = "p_numero_radicacion";
                        pn_num_radic.Value = pCredito.numero_radicacion;
                        pn_num_radic.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pn_num_radic);
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_PLANPAGOORIG";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ListarDatosPlanPagosOriginal", "USP_XPINN_CRE_GENERAPLANPAGOS", ex);
                        return null;
                    }
                };

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = "Select c.numerocuota, c.fechacuota, c.sal_ini, c.capital, c.int_1, c.int_2, c.int_3, c.int_4, c.int_5, c.int_6, c.int_7, c.int_8, c.int_9, c.int_10, c.int_11, c.int_12, c.int_13, c.int_14, c.int_15, c.total, c.sal_fin From genera_plan_pagos c Where c.numero_radicacion = " + pCredito.numero_radicacion + " Order by c.numerocuota";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DatosPlanPagos pEntidad = new DatosPlanPagos();
                            // Asociar todos los valores a la entidad
                            if (resultado["numerocuota"] != DBNull.Value) pEntidad.numerocuota = Convert.ToInt32(resultado["numerocuota"]);
                            if (resultado["fechacuota"] != DBNull.Value) pEntidad.fechacuota = Convert.ToDateTime(resultado["fechacuota"]);
                            if (resultado["sal_ini"] != DBNull.Value) pEntidad.sal_ini = Convert.ToInt64(resultado["sal_ini"]);
                            if (resultado["capital"] != DBNull.Value) pEntidad.capital = Convert.ToInt64(resultado["capital"]);
                            if (resultado["int_1"] != DBNull.Value) pEntidad.int_1 = Convert.ToInt64(resultado["int_1"]);
                            if (resultado["int_2"] != DBNull.Value) pEntidad.int_2 = Convert.ToInt64(resultado["int_2"]);
                            if (resultado["int_3"] != DBNull.Value) pEntidad.int_3 = Convert.ToInt64(resultado["int_3"]);
                            if (resultado["int_4"] != DBNull.Value) pEntidad.int_4 = Convert.ToInt64(resultado["int_4"]);
                            if (resultado["int_5"] != DBNull.Value) pEntidad.int_5 = Convert.ToInt64(resultado["int_5"]);
                            if (resultado["int_6"] != DBNull.Value) pEntidad.int_6 = Convert.ToInt64(resultado["int_6"]);
                            if (resultado["int_7"] != DBNull.Value) pEntidad.int_7 = Convert.ToInt64(resultado["int_7"]);
                            if (resultado["int_8"] != DBNull.Value) pEntidad.int_8 = Convert.ToInt64(resultado["int_8"]);
                            if (resultado["int_9"] != DBNull.Value) pEntidad.int_9 = Convert.ToInt64(resultado["int_9"]);
                            if (resultado["int_10"] != DBNull.Value) pEntidad.int_10 = Convert.ToInt64(resultado["int_10"]);
                            if (resultado["int_11"] != DBNull.Value) pEntidad.int_11 = Convert.ToInt64(resultado["int_11"]);
                            if (resultado["int_12"] != DBNull.Value) pEntidad.int_12 = Convert.ToInt64(resultado["int_12"]);
                            if (resultado["int_13"] != DBNull.Value) pEntidad.int_13 = Convert.ToInt64(resultado["int_13"]);
                            if (resultado["int_14"] != DBNull.Value) pEntidad.int_14 = Convert.ToInt64(resultado["int_14"]);
                            if (resultado["int_15"] != DBNull.Value) pEntidad.int_15 = Convert.ToInt64(resultado["int_15"]);
                            if (resultado["total"] != DBNull.Value) pEntidad.total = Convert.ToInt64(resultado["total"]);
                            if (resultado["sal_fin"] != DBNull.Value) pEntidad.sal_fin = Convert.ToInt64(resultado["sal_fin"]);
                            lstPlanPagos.Add(pEntidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);

                        return lstPlanPagos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosPlanPagosData", "ListarDatosPlanPagosOriginal", ex);
                        return null;
                    }
                }
            }
        }

        public List<Atributos> GenerarAtributosPlan(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Atributos> lstPlanPagos = new List<Atributos>();
            Atributos pEntidadPlan;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        const string sql = "Select c.cod_atr, c.nom_atr From atributos_plan_pagos c Order by c.orden";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        int contador = 0;

                        while (resultado.Read())
                        {
                            pEntidadPlan = new Atributos();
                            // Asociar todos los valores a la entidad
                            if (resultado["cod_atr"] != DBNull.Value) pEntidadPlan.cod_atr = Convert.ToInt64(resultado["cod_atr"]);
                            if (resultado["nom_atr"] != DBNull.Value) pEntidadPlan.nom_atr = Convert.ToString(resultado["nom_atr"]);
                            contador += 1;
                            lstPlanPagos.Add(pEntidadPlan);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPlanPagos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosPlanPagosData", "GenerarAtributosPlan", ex);
                        return null;
                    }
                }
            }
        }


        public List<DatosPlanPagos> ListarDatosPlanPagosNue(Credito pCredito, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DatosPlanPagos> lstPlanPagos = new List<DatosPlanPagos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        DbParameter pf_fecha = cmdTransaccionFactory.CreateParameter();
                        pf_fecha.ParameterName = "pf_fecha";
                        pf_fecha.Value = pCredito.fecha_aprobacion;
                        pf_fecha.Direction = ParameterDirection.Input;
                        pf_fecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pf_fecha);

                        DbParameter pn_num_radic = cmdTransaccionFactory.CreateParameter();
                        pn_num_radic.ParameterName = "pn_num_radic";
                        pn_num_radic.Value = pCredito.numero_radicacion;
                        pn_num_radic.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pn_num_radic);

                        DbParameter pn_monto = cmdTransaccionFactory.CreateParameter();
                        pn_monto.ParameterName = "pn_monto";
                        pn_monto.Value = pCredito.monto;
                        pn_monto.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pn_monto);

                        DbParameter pn_plazo = cmdTransaccionFactory.CreateParameter();
                        pn_plazo.ParameterName = "pn_plazo";
                        pn_plazo.Value = pCredito.plazo;
                        pn_plazo.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pn_plazo);

                        DbParameter pf_fecha_proximo_pago = cmdTransaccionFactory.CreateParameter();
                        pf_fecha_proximo_pago.ParameterName = "pf_fecha_proximo_pago";
                        pf_fecha_proximo_pago.Value = pCredito.fecha_prox_pago;
                        pf_fecha_proximo_pago.Direction = ParameterDirection.Input;
                        pf_fecha_proximo_pago.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pf_fecha_proximo_pago);

                        DbParameter pn_cuota = cmdTransaccionFactory.CreateParameter();
                        pn_cuota.ParameterName = "pn_cuota";
                        pn_cuota.Value = pCredito.valor_cuota;
                        pn_cuota.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(pn_cuota);

                        DbParameter pn_periodic = cmdTransaccionFactory.CreateParameter();
                        pn_periodic.ParameterName = "pn_periodic";
                        pn_periodic.Value = pCredito.periodicidad;
                        pn_periodic.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pn_periodic);

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "ptasa";
                        ptasa.Value = pCredito.tasa;
                        ptasa.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(ptasa);   

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "ptipo";
                        ptipo.Value = pCredito.tipo_plan;
                        ptipo.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(ptipo);


                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_GENERANUEPLANPAG";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pCredito.valor_cuota = Convert.ToInt64(pn_cuota.Value);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosPlanPagosData", "USP_XPINN_CRE_GENERANUEPLANPAG", ex);
                        return null;
                    }
                };

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = "Select c.numerocuota, c.fechacuota, c.sal_ini, c.capital, c.int_1, c.int_2, c.int_3, c.int_4, c.int_5, c.int_6, c.int_7, c.int_8, c.int_9, c.int_10, c.int_11, c.int_12, c.int_13, c.int_14, c.int_15, c.total, c.sal_fin From genera_plan_pagos c Where c.numero_radicacion = " + pCredito.numero_radicacion + " Order by c.numerocuota";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DatosPlanPagos pEntidad = new DatosPlanPagos();
                            // Asociar todos los valores a la entidad
                            if (resultado["numerocuota"] != DBNull.Value) pEntidad.numerocuota = Convert.ToInt32(resultado["numerocuota"]);
                            if (resultado["fechacuota"] != DBNull.Value) pEntidad.fechacuota = Convert.ToDateTime(resultado["fechacuota"]);
                            if (resultado["sal_ini"] != DBNull.Value) pEntidad.sal_ini = Convert.ToInt64(resultado["sal_ini"]);
                            if (resultado["capital"] != DBNull.Value) pEntidad.capital = Convert.ToInt64(resultado["capital"]);
                            if (resultado["int_1"] != DBNull.Value) pEntidad.int_1 = Convert.ToInt64(resultado["int_1"]);
                            if (resultado["int_2"] != DBNull.Value) pEntidad.int_2 = Convert.ToInt64(resultado["int_2"]);
                            if (resultado["int_3"] != DBNull.Value) pEntidad.int_3 = Convert.ToInt64(resultado["int_3"]);
                            if (resultado["int_4"] != DBNull.Value) pEntidad.int_4 = Convert.ToInt64(resultado["int_4"]);
                            if (resultado["int_5"] != DBNull.Value) pEntidad.int_5 = Convert.ToInt64(resultado["int_5"]);
                            if (resultado["int_6"] != DBNull.Value) pEntidad.int_6 = Convert.ToInt64(resultado["int_6"]);
                            if (resultado["int_7"] != DBNull.Value) pEntidad.int_7 = Convert.ToInt64(resultado["int_7"]);
                            if (resultado["int_8"] != DBNull.Value) pEntidad.int_8 = Convert.ToInt64(resultado["int_8"]);
                            if (resultado["int_9"] != DBNull.Value) pEntidad.int_9 = Convert.ToInt64(resultado["int_9"]);
                            if (resultado["int_10"] != DBNull.Value) pEntidad.int_10 = Convert.ToInt64(resultado["int_10"]);
                            if (resultado["int_11"] != DBNull.Value) pEntidad.int_11 = Convert.ToInt64(resultado["int_11"]);
                            if (resultado["int_12"] != DBNull.Value) pEntidad.int_12 = Convert.ToInt64(resultado["int_12"]);
                            if (resultado["int_13"] != DBNull.Value) pEntidad.int_13 = Convert.ToInt64(resultado["int_13"]);
                            if (resultado["int_14"] != DBNull.Value) pEntidad.int_14 = Convert.ToInt64(resultado["int_14"]);
                            if (resultado["int_15"] != DBNull.Value) pEntidad.int_15 = Convert.ToInt64(resultado["int_15"]);
                            if (resultado["total"] != DBNull.Value) pEntidad.total = Convert.ToInt64(resultado["total"]);
                            if (resultado["sal_fin"] != DBNull.Value) pEntidad.sal_fin = Convert.ToInt64(resultado["sal_fin"]);
                            lstPlanPagos.Add(pEntidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPlanPagos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosPlanPagosData", "ListarDatosPlanPagos", ex);
                        return null;
                    }
                }
            }
        }



    }
}
