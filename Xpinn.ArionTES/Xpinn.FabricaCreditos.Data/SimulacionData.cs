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
    public class SimulacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
        string FormatoFecha = " ";

        public SimulacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
            Xpinn.Util.Configuracion global = new Xpinn.Util.Configuracion();
            FormatoFecha = global.ObtenerValorConfig("FormatoFechaBase");
        }

        public Simulacion ConsultarSimulacionCuota(long monto, int plazo, int periodicidad, string cod_cred, int tipo_liquidacion, decimal tasa, decimal? Comision, decimal? Aporte, DateTime? FechaPrimerPago, List<CuotasExtras> lstCuotasExtras, ref string error, Usuario pUsuario, long cod_persona)
        {
            error = "";
            //string fecha = FechaPrimerPago.ToString().Substring(0,10);
            Simulacion valor = new Simulacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        // Limpiar la tabla temporal de cuotas extras
                        cmdTransaccionFactory.Parameters.Clear();
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "DELETE FROM TEMP_CUOTASEXTRAS";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // Cargar las cuotas extras
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (lstCuotasExtras != null)
                        {
                            foreach (CuotasExtras item in lstCuotasExtras)
                            {
                                cmdTransaccionFactory.Parameters.Clear();

                                DbParameter pIDCUOTAEXTRA = cmdTransaccionFactory.CreateParameter();
                                pIDCUOTAEXTRA.ParameterName = "p_IDCUOTAEXTRA";
                                pIDCUOTAEXTRA.Value = item.cod_cuota;
                                pIDCUOTAEXTRA.Direction = ParameterDirection.InputOutput;
                                pIDCUOTAEXTRA.DbType = DbType.Int64;

                                DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                                pFECHA.ParameterName = "p_FECHA";
                                pFECHA.Value = item.fecha_pago;
                                pFECHA.Direction = ParameterDirection.Input;
                                pFECHA.DbType = DbType.DateTime;

                                DbParameter pVALOR = cmdTransaccionFactory.CreateParameter();
                                pVALOR.ParameterName = "p_VALOR";
                                pVALOR.Value = item.valor;
                                pVALOR.Direction = ParameterDirection.Input;
                                pVALOR.DbType = DbType.Int64;

                                DbParameter pTIP_FOR_PAG = cmdTransaccionFactory.CreateParameter();
                                pTIP_FOR_PAG.ParameterName = "p_TIP_FOR_PAG";
                                pTIP_FOR_PAG.Value = item.forma_pago;
                                pTIP_FOR_PAG.Direction = ParameterDirection.Input;
                                pTIP_FOR_PAG.DbType = DbType.Int64;

                                DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                                pNUMERO_RADICACION.ParameterName = "p_NUMERO_RADICACION";
                                pNUMERO_RADICACION.Value = item.numero_radicacion;
                                pNUMERO_RADICACION.Direction = ParameterDirection.Input;
                                pNUMERO_RADICACION.DbType = DbType.Int64;

                                cmdTransaccionFactory.Parameters.Add(pIDCUOTAEXTRA);
                                cmdTransaccionFactory.Parameters.Add(pFECHA);
                                cmdTransaccionFactory.Parameters.Add(pVALOR);
                                cmdTransaccionFactory.Parameters.Add(pTIP_FOR_PAG);
                                cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);

                                cmdTransaccionFactory.Connection = connection;
                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_SIMUCUOEXT_CREAR";
                                cmdTransaccionFactory.ExecuteNonQuery();

                                string registrado = pIDCUOTAEXTRA.Value.ToString();
                            }
                        }

                        // Generar el calculo de la cuota
                        cmdTransaccionFactory.Parameters.Clear();
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pmonto = cmdTransaccionFactory.CreateParameter();
                        pmonto.ParameterName = "pmonto";
                        pmonto.Value = monto;
                        pmonto.DbType = DbType.Int64;
                        pmonto.Direction = ParameterDirection.Input;

                        DbParameter pn_cuota = cmdTransaccionFactory.CreateParameter();
                        pn_cuota.ParameterName = "pn_cuota";
                        pn_cuota.DbType = DbType.Decimal;
                        pn_cuota.Direction = ParameterDirection.Output;

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "PCOD_PERSONA";
                        pcod_persona.Value = cod_persona;
                        pcod_persona.DbType = DbType.Int32;
                        pcod_persona.Direction = ParameterDirection.Input;

                        DbParameter perror = cmdTransaccionFactory.CreateParameter();
                        perror.ParameterName = "PERROR";
                        perror.DbType = DbType.StringFixedLength;
                        perror.Size = 1000;
                        perror.Direction = ParameterDirection.Output;

                        DbParameter pplazo = cmdTransaccionFactory.CreateParameter();
                        pplazo.ParameterName = "pplazo";
                        pplazo.Value = plazo;
                        pplazo.DbType = DbType.Int32;
                        pplazo.Direction = ParameterDirection.Input;

                        DbParameter pperiodic = cmdTransaccionFactory.CreateParameter();
                        pperiodic.ParameterName = "pperiodic";
                        pperiodic.Value = periodicidad;
                        pperiodic.DbType = DbType.Int32;
                        pperiodic.Direction = ParameterDirection.Input;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "pfecha";
                        pfecha.Value = DateTime.Today.ToString(FormatoFecha);
                        pfecha.DbType = DbType.Date;
                        pfecha.Direction = ParameterDirection.Input;

                        DbParameter pfor_pag = cmdTransaccionFactory.CreateParameter();
                        pfor_pag.ParameterName = "pfor_pag";
                        pfor_pag.Value = 1;
                        pfor_pag.DbType = DbType.Int32;
                        pfor_pag.Direction = ParameterDirection.Input;

                        DbParameter pcod_credi = cmdTransaccionFactory.CreateParameter();
                        pcod_credi.ParameterName = "pcod_credi";
                        pcod_credi.Value = cod_cred;
                        pcod_credi.DbType = DbType.String;
                        pcod_credi.Direction = ParameterDirection.Input;

                        DbParameter ptipoliq = cmdTransaccionFactory.CreateParameter();
                        ptipoliq.ParameterName = "ptipo_liquidacion";
                        if (tipo_liquidacion != 0) ptipoliq.Value = tipo_liquidacion; else ptipoliq.Value = DBNull.Value;
                        ptipoliq.DbType = DbType.Int32;
                        ptipoliq.Direction = ParameterDirection.Input;

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "ptasa";
                        ptasa.Value = tasa;
                        ptasa.DbType = DbType.Decimal;
                        ptasa.Direction = ParameterDirection.Input;

                        DbParameter pcomision = cmdTransaccionFactory.CreateParameter();
                        pcomision.ParameterName = "pcomision";
                        if (Comision != 0) pcomision.Value = Comision; else pcomision.Value = DBNull.Value;
                        pcomision.DbType = DbType.Decimal;
                        pcomision.Direction = ParameterDirection.Input;

                        DbParameter paporte = cmdTransaccionFactory.CreateParameter();
                        paporte.ParameterName = "paporte";
                        if (Aporte != 0) paporte.Value = Aporte; else paporte.Value = DBNull.Value;
                        paporte.DbType = DbType.Decimal;
                        paporte.Direction = ParameterDirection.Input;

                        DbParameter pfechaprimerpago = cmdTransaccionFactory.CreateParameter();
                        pfechaprimerpago.ParameterName = "pfechaprimerpago";
                        if (FechaPrimerPago == null)
                            pfechaprimerpago.Value = DBNull.Value;
                        else
                        pfechaprimerpago.Value = Convert.ToDateTime(FechaPrimerPago);
                        pfechaprimerpago.DbType = DbType.Date;
                        pfechaprimerpago.Direction = ParameterDirection.Input;



                        cmdTransaccionFactory.Parameters.Add(pmonto);
                        cmdTransaccionFactory.Parameters.Add(pn_cuota);
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);
                        cmdTransaccionFactory.Parameters.Add(perror);
                        cmdTransaccionFactory.Parameters.Add(pplazo);
                        cmdTransaccionFactory.Parameters.Add(pperiodic);
                        cmdTransaccionFactory.Parameters.Add(pfecha);
                        cmdTransaccionFactory.Parameters.Add(pfor_pag);
                        cmdTransaccionFactory.Parameters.Add(pcod_credi);
                        cmdTransaccionFactory.Parameters.Add(ptipoliq);
                        cmdTransaccionFactory.Parameters.Add(ptasa);
                        cmdTransaccionFactory.Parameters.Add(pcomision);
                        cmdTransaccionFactory.Parameters.Add(paporte);
                        cmdTransaccionFactory.Parameters.Add(pfechaprimerpago);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_SIMULA_CALCUOTA";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pn_cuota.Value != null)
                        {
                            if (pn_cuota.Value != DBNull.Value)
                            {
                                try { valor.cuota = Convert.ToDecimal(pn_cuota.Value.ToString()); }
                                catch (Exception ex) { valor.cuota = 0; error = ex.Message; }
                            }
                        }
                        else
                        {
                            valor.cuota = 0;
                        }

                        if (perror.Value != null)
                            if (perror.Value != DBNull.Value)
                                error = perror.Value.ToString();

                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                        return null;
                    }
                }
            }
        }

        public Simulacion ConsultarSimulacionCuotaInterna(int monto, int plazo, int periodicidad, int tasa, ref string error, Usuario pUsuario, long cod_persona)
        {
            error = "";
            Simulacion valor = new Simulacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pmonto = cmdTransaccionFactory.CreateParameter();
                        pmonto.ParameterName = "pmonto";
                        pmonto.Value = monto;
                        pmonto.Direction = ParameterDirection.Input;

                        DbParameter pplazo = cmdTransaccionFactory.CreateParameter();
                        pplazo.ParameterName = "pplazo";
                        pplazo.Value = plazo;
                        pplazo.Direction = ParameterDirection.Input;

                        DbParameter pperiodic = cmdTransaccionFactory.CreateParameter();
                        pperiodic.ParameterName = "pperiodic";
                        pperiodic.Value = periodicidad;
                        pperiodic.Direction = ParameterDirection.Input;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "pfecha";
                        pfecha.Value = DateTime.Today.ToString(FormatoFecha);
                        pfecha.DbType = DbType.Date;
                        pfecha.Direction = ParameterDirection.Input;

                        DbParameter pfor_pag = cmdTransaccionFactory.CreateParameter();
                        pfor_pag.ParameterName = "pfor_pag";
                        pfor_pag.Value = 1;
                        pfor_pag.Direction = ParameterDirection.Input;

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "ptasa";
                        ptasa.Value = tasa;
                        ptasa.Direction = ParameterDirection.Input;

                        DbParameter pn_cuota = cmdTransaccionFactory.CreateParameter();
                        pn_cuota.ParameterName = "pn_cuota";
                        pn_cuota.DbType = DbType.Decimal;
                        pn_cuota.Direction = ParameterDirection.Output;

                        DbParameter PCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        PCOD_PERSONA.ParameterName = "PCOD_PERSONA";
                        PCOD_PERSONA.DbType = DbType.Int64;
                        PCOD_PERSONA.Value = cod_persona;
                        PCOD_PERSONA.Direction = ParameterDirection.Input;

                        DbParameter perror = cmdTransaccionFactory.CreateParameter();
                        perror.ParameterName = "PERROR";
                        perror.DbType = DbType.StringFixedLength;
                        perror.Size = 1000;
                        perror.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(pmonto);
                        cmdTransaccionFactory.Parameters.Add(pplazo);
                        cmdTransaccionFactory.Parameters.Add(pperiodic);
                        cmdTransaccionFactory.Parameters.Add(pfecha);
                        cmdTransaccionFactory.Parameters.Add(pfor_pag);
                        cmdTransaccionFactory.Parameters.Add(ptasa);
                        cmdTransaccionFactory.Parameters.Add(pn_cuota);
                        cmdTransaccionFactory.Parameters.Add(PCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(perror);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_SIMULA_CALCUOTA";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                        return null;
                    }
                }
            }
        }

        public List<DatosPlanPagos> SimularPlanPagos(Simulacion pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DatosPlanPagos> lstPlanPagos = new List<DatosPlanPagos>();
            DatosPlanPagos pEntidadPlan;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        // Limpiar la tabla temporal de cuotas extras
                        cmdTransaccionFactory.Parameters.Clear();
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "DELETE FROM TEMP_CUOTASEXTRAS";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // Cargar las cuotas extras                       
                        if (pEntidad.lstCuotasExtras != null)
                        {
                            foreach (CuotasExtras item in pEntidad.lstCuotasExtras)
                            {
                                cmdTransaccionFactory.Parameters.Clear();
                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                                DbParameter pIDCUOTAEXTRA = cmdTransaccionFactory.CreateParameter();
                                pIDCUOTAEXTRA.ParameterName = "p_IDCUOTAEXTRA";
                                pIDCUOTAEXTRA.Value = item.cod_cuota;
                                pIDCUOTAEXTRA.Direction = ParameterDirection.InputOutput;
                                pIDCUOTAEXTRA.DbType = DbType.Int64;

                                DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                                pFECHA.ParameterName = "p_FECHA";
                                pFECHA.Value = item.fecha_pago;
                                pFECHA.Direction = ParameterDirection.Input;
                                pFECHA.DbType = DbType.DateTime;

                                DbParameter pVALOR = cmdTransaccionFactory.CreateParameter();
                                pVALOR.ParameterName = "p_VALOR";
                                pVALOR.Value = item.valor;
                                pVALOR.Direction = ParameterDirection.Input;
                                pVALOR.DbType = DbType.Int64;

                                DbParameter pTIP_FOR_PAG = cmdTransaccionFactory.CreateParameter();
                                pTIP_FOR_PAG.ParameterName = "p_TIP_FOR_PAG";
                                pTIP_FOR_PAG.Value = item.forma_pago;
                                pTIP_FOR_PAG.Direction = ParameterDirection.Input;
                                pTIP_FOR_PAG.DbType = DbType.Int64;

                                DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                                pNUMERO_RADICACION.ParameterName = "p_NUMERO_RADICACION";
                                pNUMERO_RADICACION.Value = item.numero_radicacion;
                                pNUMERO_RADICACION.Direction = ParameterDirection.Input;
                                pNUMERO_RADICACION.DbType = DbType.Int64;

                                cmdTransaccionFactory.Parameters.Add(pIDCUOTAEXTRA);
                                cmdTransaccionFactory.Parameters.Add(pFECHA);
                                cmdTransaccionFactory.Parameters.Add(pVALOR);
                                cmdTransaccionFactory.Parameters.Add(pTIP_FOR_PAG);
                                cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);

                                cmdTransaccionFactory.Connection = connection;
                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_SIMUCUOEXT_CREAR";
                                cmdTransaccionFactory.ExecuteNonQuery();
                            }
                        }

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pn_monto = cmdTransaccionFactory.CreateParameter();
                        pn_monto.ParameterName = "pmonto";
                        pn_monto.Value = pEntidad.monto;
                        pn_monto.Direction = ParameterDirection.Input;

                        DbParameter pn_plazo = cmdTransaccionFactory.CreateParameter();
                        pn_plazo.ParameterName = "pplazo";
                        pn_plazo.Value = pEntidad.plazo;
                        pn_plazo.Direction = ParameterDirection.Input;

                        DbParameter pn_for_pla = cmdTransaccionFactory.CreateParameter();
                        pn_for_pla.ParameterName = "pfor_pla";
                        pn_for_pla.Value = pEntidad.periodic;
                        pn_for_pla.Direction = ParameterDirection.Input;

                        DbParameter pn_periodic = cmdTransaccionFactory.CreateParameter();
                        pn_periodic.ParameterName = "pperiodic";
                        pn_periodic.Value = pEntidad.periodic;
                        pn_periodic.Direction = ParameterDirection.Input;

                        DbParameter pn_fecha = cmdTransaccionFactory.CreateParameter();
                        pn_fecha.ParameterName = "pfecha";
                        pn_fecha.Value = pEntidad.fecha.ToString(FormatoFecha);
                        pn_fecha.DbType = DbType.Date;
                        pn_fecha.Direction = ParameterDirection.Input;

                        DbParameter pn_for_pag = cmdTransaccionFactory.CreateParameter();
                        pn_for_pag.ParameterName = "pfor_pag";
                        pn_for_pag.Value = pEntidad.for_pag;
                        pn_for_pag.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_credi = cmdTransaccionFactory.CreateParameter();
                        pn_cod_credi.ParameterName = "pcod_credi";
                        pn_cod_credi.Value = pEntidad.cod_credi;
                        pn_cod_credi.Direction = ParameterDirection.Input;

                        DbParameter ptipoliq = cmdTransaccionFactory.CreateParameter();
                        ptipoliq.ParameterName = "ptipo_liquidacion";
                        ptipoliq.Value = pEntidad.tipo_liquidacion;
                        ptipoliq.Direction = ParameterDirection.Input;

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "ptasa";
                        if (pEntidad.tasa == null)
                            ptasa.Value = DBNull.Value;
                        else
                            ptasa.Value = pEntidad.tasa;
                        ptasa.Direction = ParameterDirection.Input;

                        DbParameter pcomision = cmdTransaccionFactory.CreateParameter();
                        pcomision.ParameterName = "pcomision";
                        if (pEntidad.comision == null)
                            pcomision.Value = DBNull.Value;
                        else
                            pcomision.Value = pEntidad.comision;
                        pcomision.Direction = ParameterDirection.Input;

                        DbParameter paporte = cmdTransaccionFactory.CreateParameter();
                        paporte.ParameterName = "paporte";
                        if (pEntidad.aporte == null)
                            paporte.Value = DBNull.Value;
                        else
                            paporte.Value = pEntidad.aporte;
                        paporte.Direction = ParameterDirection.Input;

                        DbParameter pfechaprimerpago = cmdTransaccionFactory.CreateParameter();
                        pfechaprimerpago.ParameterName = "pfechaprimerpago";
                        if (pEntidad.fecha_primer_pago == null)
                            pfechaprimerpago.Value = DBNull.Value;
                        else
                            pfechaprimerpago.Value = pEntidad.fecha_primer_pago;
                        pfechaprimerpago.Direction = ParameterDirection.Input;

                        DbParameter PCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        PCOD_PERSONA.ParameterName = "PCOD_PERSONA";
                        PCOD_PERSONA.DbType = DbType.Int64;
                        PCOD_PERSONA.Value = pEntidad.cod_persona;
                        PCOD_PERSONA.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Clear();
                        cmdTransaccionFactory.Parameters.Add(pn_monto);
                        cmdTransaccionFactory.Parameters.Add(pn_plazo);
                        cmdTransaccionFactory.Parameters.Add(pn_for_pla);
                        cmdTransaccionFactory.Parameters.Add(pn_periodic);
                        cmdTransaccionFactory.Parameters.Add(pn_fecha);
                        cmdTransaccionFactory.Parameters.Add(pn_for_pag);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_credi);
                        cmdTransaccionFactory.Parameters.Add(ptipoliq);
                        cmdTransaccionFactory.Parameters.Add(ptasa);
                        cmdTransaccionFactory.Parameters.Add(pcomision);
                        cmdTransaccionFactory.Parameters.Add(paporte);
                        cmdTransaccionFactory.Parameters.Add(pfechaprimerpago);
                        cmdTransaccionFactory.Parameters.Add(PCOD_PERSONA);

                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_SIMULA_PLANPAGOS";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosPlanPagosData", "USP_XPINN_CRE_SIMULA_PLANPAGOS", ex);
                        return null;
                    }
                };

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = "";
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;


                        sql = "Select Cod_Atr, Nom_Atr, Val_Atr, Tipo_Atr From Genera_Descuentos_Plan";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DescuentosCredito pDescuentosEntidad = new DescuentosCredito();
                            pEntidadPlan = new DatosPlanPagos();
                            pEntidadPlan.lstDescuentos = new List<DescuentosCredito>();
                            // Asociar todos los valores a la entidad
                            if (resultado["Cod_Atr"] != DBNull.Value) pDescuentosEntidad.cod_atr = Convert.ToInt32(resultado["Cod_Atr"]);
                            if (resultado["Nom_Atr"] != DBNull.Value) pDescuentosEntidad.nom_atr = Convert.ToString(resultado["Nom_Atr"]);
                            if (resultado["Val_Atr"] != DBNull.Value) pDescuentosEntidad.val_atr = Convert.ToInt64(resultado["Val_Atr"]);
                            if (resultado["Tipo_Atr"] != DBNull.Value) pDescuentosEntidad.tipo_atr = Convert.ToInt32(resultado["Tipo_Atr"]);

                            pEntidadPlan.lstDescuentos.Add(pDescuentosEntidad);
                            lstPlanPagos.Add(pEntidadPlan);
                        }

                        sql = "Select c.numerocuota, c.fechacuota, c.sal_ini, c.capital, c.int_1, c.int_2, c.int_3, c.int_4, c.int_5, c.int_6, c.int_7, c.int_8, c.int_9, c.int_10, c.int_11, c.int_12, c.int_13, c.int_14, c.int_15, c.total, c.sal_fin From simula_plan_pagos c  WHERE c.total > 0 Order by 2";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            pEntidadPlan = new DatosPlanPagos();
                            // Asociar todos los valores a la entidad
                            if (resultado["numerocuota"] != DBNull.Value) pEntidadPlan.numerocuota = Convert.ToInt32(resultado["numerocuota"]);
                            if (resultado["fechacuota"] != DBNull.Value) pEntidadPlan.fechacuota = Convert.ToDateTime(resultado["fechacuota"]);
                            if (resultado["sal_ini"] != DBNull.Value) pEntidadPlan.sal_ini = Convert.ToInt64(resultado["sal_ini"]);
                            if (resultado["capital"] != DBNull.Value) pEntidadPlan.capital = Convert.ToInt64(resultado["capital"]);
                            if (resultado["int_1"] != DBNull.Value) pEntidadPlan.int_1 = Convert.ToInt64(resultado["int_1"]);
                            if (resultado["int_2"] != DBNull.Value) pEntidadPlan.int_2 = Convert.ToInt64(resultado["int_2"]);
                            if (resultado["int_3"] != DBNull.Value) pEntidadPlan.int_3 = Convert.ToInt64(resultado["int_3"]);
                            if (resultado["int_4"] != DBNull.Value) pEntidadPlan.int_4 = Convert.ToInt64(resultado["int_4"]);
                            if (resultado["int_5"] != DBNull.Value) pEntidadPlan.int_5 = Convert.ToInt64(resultado["int_5"]);
                            if (resultado["int_6"] != DBNull.Value) pEntidadPlan.int_6 = Convert.ToInt64(resultado["int_6"]);
                            if (resultado["int_7"] != DBNull.Value) pEntidadPlan.int_7 = Convert.ToInt64(resultado["int_7"]);
                            if (resultado["int_8"] != DBNull.Value) pEntidadPlan.int_8 = Convert.ToInt64(resultado["int_8"]);
                            if (resultado["int_9"] != DBNull.Value) pEntidadPlan.int_9 = Convert.ToInt64(resultado["int_9"]);
                            if (resultado["int_10"] != DBNull.Value) pEntidadPlan.int_10 = Convert.ToInt64(resultado["int_10"]);
                            if (resultado["int_11"] != DBNull.Value) pEntidadPlan.int_11 = Convert.ToInt64(resultado["int_11"]);
                            if (resultado["int_12"] != DBNull.Value) pEntidadPlan.int_12 = Convert.ToInt64(resultado["int_12"]);
                            if (resultado["int_13"] != DBNull.Value) pEntidadPlan.int_13 = Convert.ToInt64(resultado["int_13"]);
                            if (resultado["int_14"] != DBNull.Value) pEntidadPlan.int_14 = Convert.ToInt64(resultado["int_14"]);
                            if (resultado["int_15"] != DBNull.Value) pEntidadPlan.int_15 = Convert.ToInt64(resultado["int_15"]);
                            if (resultado["total"] != DBNull.Value) pEntidadPlan.total = Convert.ToInt64(resultado["total"]);
                            if (resultado["sal_fin"] != DBNull.Value) pEntidadPlan.sal_fin = Convert.ToInt64(resultado["sal_fin"]);
                            lstPlanPagos.Add(pEntidadPlan);

                        }


                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPlanPagos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosPlanPagosData", "SimularPlanPagos", ex);
                        return null;
                    }
                }
            }
        }

        public List<DatosPlanPagos> SimularPlanPagosInterno(Simulacion pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DatosPlanPagos> lstPlanPagos = new List<DatosPlanPagos>();
            DatosPlanPagos pEntidadPlan;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pn_monto = cmdTransaccionFactory.CreateParameter();
                        pn_monto.ParameterName = "pmonto";
                        pn_monto.Value = pEntidad.monto;
                        pn_monto.Direction = ParameterDirection.Input;

                        DbParameter pn_plazo = cmdTransaccionFactory.CreateParameter();
                        pn_plazo.ParameterName = "pplazo";
                        pn_plazo.Value = pEntidad.plazo;
                        pn_plazo.Direction = ParameterDirection.Input;

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "ptasa";
                        ptasa.Value = pEntidad.tasa;
                        ptasa.Direction = ParameterDirection.Input;

                        DbParameter pperiodicidad = cmdTransaccionFactory.CreateParameter();
                        pperiodicidad.ParameterName = "pperiodicidad";
                        pperiodicidad.Value = pEntidad.periodic;
                        pperiodicidad.Direction = ParameterDirection.Input;

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "pforma_pago";
                        pforma_pago.Value = pEntidad.for_pag;
                        pforma_pago.Direction = ParameterDirection.Input;

                        DbParameter ptasa_seguro = cmdTransaccionFactory.CreateParameter();
                        ptasa_seguro.ParameterName = "ptasa_seguro";
                        ptasa_seguro.Value = pEntidad.tasaseguro;
                        ptasa_seguro.Direction = ParameterDirection.Input;

                        DbParameter pn_fecha = cmdTransaccionFactory.CreateParameter();
                        pn_fecha.ParameterName = "pfecha";
                        pn_fecha.Value = pEntidad.fecha.ToString(FormatoFecha);
                        pn_fecha.DbType = DbType.DateTime;
                        pn_fecha.Direction = ParameterDirection.Input;

                        DbParameter pn_fecha_primer_pago = cmdTransaccionFactory.CreateParameter();
                        pn_fecha_primer_pago.ParameterName = "pfecha_primer_pago";
                        pn_fecha_primer_pago.Value = pEntidad.fecha.ToString(FormatoFecha);
                        pn_fecha_primer_pago.DbType = DbType.DateTime;
                        pn_fecha_primer_pago.Direction = ParameterDirection.Input;

                        DbParameter pcuota = cmdTransaccionFactory.CreateParameter();
                        pcuota.ParameterName = "pcuota";
                        pcuota.Value = pEntidad.cuota;
                        pcuota.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(pn_monto);
                        cmdTransaccionFactory.Parameters.Add(pn_plazo);
                        cmdTransaccionFactory.Parameters.Add(ptasa);
                        cmdTransaccionFactory.Parameters.Add(pperiodicidad);
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);
                        cmdTransaccionFactory.Parameters.Add(ptasa_seguro);
                        cmdTransaccionFactory.Parameters.Add(pn_fecha_primer_pago);
                        cmdTransaccionFactory.Parameters.Add(pn_fecha);
                        cmdTransaccionFactory.Parameters.Add(pcuota);

                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_SIMULACION";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SimulacionData", "SimularPlanPagosInterno", ex);
                        return null;
                    }
                };

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        const string sql = "Select c.fechacuota, c.numerocuota, c.total as cuota, c.capital, c.int_1 as interes, c.sal_fin, c.int_2 as seguro From simula_plan_pagos c Order by c.numerocuota asc";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            pEntidadPlan = new DatosPlanPagos();
                            // Asociar todos los valores a la entidad
                            if (resultado["numerocuota"] != DBNull.Value) pEntidadPlan.numerocuota = Convert.ToInt32(resultado["numerocuota"]);
                            if (resultado["cuota"] != DBNull.Value) pEntidadPlan.valorcuota = Convert.ToDecimal(resultado["cuota"]);
                            if (resultado["fechacuota"] != DBNull.Value) pEntidadPlan.fechacuota = Convert.ToDateTime(resultado["fechacuota"]);
                            if (resultado["capital"] != DBNull.Value) pEntidadPlan.capital = Convert.ToDecimal(resultado["capital"]);
                            if (resultado["interes"] != DBNull.Value) pEntidadPlan.interes = Convert.ToDecimal(resultado["interes"]);
                            if (resultado["sal_fin"] != DBNull.Value) pEntidadPlan.sal_pendiente = Convert.ToDecimal(resultado["sal_fin"]);
                            if (resultado["seguro"] != DBNull.Value) pEntidadPlan.seguro = Convert.ToDecimal(resultado["seguro"]);
                            lstPlanPagos.Add(pEntidadPlan);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPlanPagos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosPlanPagosData", "SimularPlanPagosInterno", ex);
                        return null;
                    }
                }
            }
        }


        public List<Atributos> SimularAtributosPlan(Usuario pUsuario)
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

                        while (resultado.Read())
                        {
                            pEntidadPlan = new Atributos();
                            // Asociar todos los valores a la entidad
                            if (resultado["cod_atr"] != DBNull.Value) pEntidadPlan.cod_atr = Convert.ToInt64(resultado["cod_atr"]);
                            if (resultado["nom_atr"] != DBNull.Value) pEntidadPlan.nom_atr = Convert.ToString(resultado["nom_atr"]);
                            lstPlanPagos.Add(pEntidadPlan);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPlanPagos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosPlanPagosData", "SimularAtributosPlan", ex);
                        return null;
                    }
                }
            }
        }

    }
}
