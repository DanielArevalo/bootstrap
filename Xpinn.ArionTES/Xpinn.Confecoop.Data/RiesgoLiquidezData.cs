using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Confecoop.Entities;


namespace Xpinn.Confecoop.Data
{
    public class RiesgoLiquidezData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
        
        public RiesgoLiquidezData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public RiesgoLiquidez CrearRiesgoLiquidez(RiesgoLiquidez pRiesgoLiquidez, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pRiesgoLiquidez.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pfecha_corte = cmdTransaccionFactory.CreateParameter();
                        pfecha_corte.ParameterName = "p_fecha_corte";
                        if (pRiesgoLiquidez.fecha_corte == null)
                            pfecha_corte.Value = DBNull.Value;
                        else
                            pfecha_corte.Value = pRiesgoLiquidez.fecha_corte;
                        pfecha_corte.Direction = ParameterDirection.Input;
                        pfecha_corte.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_corte);

                        DbParameter prenglon = cmdTransaccionFactory.CreateParameter();
                        prenglon.ParameterName = "p_renglon";
                        if (pRiesgoLiquidez.renglon == null)
                            prenglon.Value = DBNull.Value;
                        else
                            prenglon.Value = pRiesgoLiquidez.renglon;
                        prenglon.Direction = ParameterDirection.Input;
                        prenglon.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(prenglon);

                        DbParameter punidad_captura = cmdTransaccionFactory.CreateParameter();
                        punidad_captura.ParameterName = "p_unidad_captura";
                        if (pRiesgoLiquidez.unidad_captura == null)
                            punidad_captura.Value = DBNull.Value;
                        else
                            punidad_captura.Value = pRiesgoLiquidez.unidad_captura;
                        punidad_captura.Direction = ParameterDirection.Input;
                        punidad_captura.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(punidad_captura);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pRiesgoLiquidez.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pRiesgoLiquidez.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter psaldo_actual = cmdTransaccionFactory.CreateParameter();
                        psaldo_actual.ParameterName = "p_saldo_actual";
                        if (pRiesgoLiquidez.saldo_actual == null)
                            psaldo_actual.Value = DBNull.Value;
                        else
                            psaldo_actual.Value = pRiesgoLiquidez.saldo_actual;
                        psaldo_actual.Direction = ParameterDirection.Input;
                        psaldo_actual.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_actual);

                        DbParameter pvr_brecha1 = cmdTransaccionFactory.CreateParameter();
                        pvr_brecha1.ParameterName = "p_vr_brecha1";
                        if (pRiesgoLiquidez.vr_brecha1 == null)
                            pvr_brecha1.Value = DBNull.Value;
                        else
                            pvr_brecha1.Value = pRiesgoLiquidez.vr_brecha1;
                        pvr_brecha1.Direction = ParameterDirection.Input;
                        pvr_brecha1.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvr_brecha1);

                        DbParameter pvr_brecha2 = cmdTransaccionFactory.CreateParameter();
                        pvr_brecha2.ParameterName = "p_vr_brecha2";
                        if (pRiesgoLiquidez.vr_brecha2 == null)
                            pvr_brecha2.Value = DBNull.Value;
                        else
                            pvr_brecha2.Value = pRiesgoLiquidez.vr_brecha2;
                        pvr_brecha2.Direction = ParameterDirection.Input;
                        pvr_brecha2.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvr_brecha2);

                        DbParameter pvr_brecha3 = cmdTransaccionFactory.CreateParameter();
                        pvr_brecha3.ParameterName = "p_vr_brecha3";
                        if (pRiesgoLiquidez.vr_brecha3 == null)
                            pvr_brecha3.Value = DBNull.Value;
                        else
                            pvr_brecha3.Value = pRiesgoLiquidez.vr_brecha3;
                        pvr_brecha3.Direction = ParameterDirection.Input;
                        pvr_brecha3.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvr_brecha3);

                        DbParameter pvr_brecha4 = cmdTransaccionFactory.CreateParameter();
                        pvr_brecha4.ParameterName = "p_vr_brecha4";
                        if (pRiesgoLiquidez.vr_brecha4 == null)
                            pvr_brecha4.Value = DBNull.Value;
                        else
                            pvr_brecha4.Value = pRiesgoLiquidez.vr_brecha4;
                        pvr_brecha4.Direction = ParameterDirection.Input;
                        pvr_brecha4.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvr_brecha4);

                        DbParameter pvr_brecha5 = cmdTransaccionFactory.CreateParameter();
                        pvr_brecha5.ParameterName = "p_vr_brecha5";
                        if (pRiesgoLiquidez.vr_brecha5 == null)
                            pvr_brecha5.Value = DBNull.Value;
                        else
                            pvr_brecha5.Value = pRiesgoLiquidez.vr_brecha5;
                        pvr_brecha5.Direction = ParameterDirection.Input;
                        pvr_brecha5.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvr_brecha5);

                        DbParameter pvr_brecha6 = cmdTransaccionFactory.CreateParameter();
                        pvr_brecha6.ParameterName = "p_vr_brecha6";
                        if (pRiesgoLiquidez.vr_brecha6 == null)
                            pvr_brecha6.Value = DBNull.Value;
                        else
                            pvr_brecha6.Value = pRiesgoLiquidez.vr_brecha6;
                        pvr_brecha6.Direction = ParameterDirection.Input;
                        pvr_brecha6.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvr_brecha6);

                        DbParameter pvr_brecha7 = cmdTransaccionFactory.CreateParameter();
                        pvr_brecha7.ParameterName = "p_vr_brecha7";
                        if (pRiesgoLiquidez.vr_brecha7 == null)
                            pvr_brecha7.Value = DBNull.Value;
                        else
                            pvr_brecha7.Value = pRiesgoLiquidez.vr_brecha7;
                        pvr_brecha7.Direction = ParameterDirection.Input;
                        pvr_brecha7.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvr_brecha7);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pRiesgoLiquidez.consecutivo = Convert.ToInt64(pconsecutivo.Value);

                        return pRiesgoLiquidez;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RiesgoLiquidezData", "CrearRiesgoLiquidez", ex);
                        return null;
                    }
                }
            }
        }

        public List<RiesgoLiquidez> ListarProyeccionAporte(RiesgoLiquidez riesgo, Usuario usuario)
        {
            List<RiesgoLiquidez> listaProyeccion = new List<RiesgoLiquidez>();
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DateTimeHelper dateHelper = new DateTimeHelper();

                        string sql = @"SELECT DISTINCT per.identificacion, per.NOMBRE, apo.cuota, apo.saldo,
                                            Nvl((Select x.CUOTA From HISTORICO_APORTE x Where x.NUMERO_APORTE = h.NUMERO_APORTE And Trunc(x.FECHA_HISTORICO) Between to_Date('" + dateHelper.PrimerDiaDelMes(riesgo.fecha_corte.Value.AddYears(-1).AddMonths(1)).ToShortDateString() + @"', 'dd/MM/yyyy') And to_Date('" + dateHelper.UltimoDiaDelMes(riesgo.fecha_corte.Value.AddYears(-1).AddMonths(1)).ToShortDateString() + @"', 'dd/MM/yyyy')), 0) As brecha1,
                                            Nvl((Select x.CUOTA From HISTORICO_APORTE x Where x.NUMERO_APORTE = h.NUMERO_APORTE And Trunc(x.FECHA_HISTORICO) Between to_Date('" + dateHelper.PrimerDiaDelMes(riesgo.fecha_corte.Value.AddYears(-1).AddMonths(2)).ToShortDateString() + @"', 'dd/MM/yyyy') And to_Date('" + dateHelper.UltimoDiaDelMes(riesgo.fecha_corte.Value.AddYears(-1).AddMonths(2)).ToShortDateString() + @"', 'dd/MM/yyyy')), 0) As brecha2,
                                            Nvl((Select x.CUOTA From HISTORICO_APORTE x Where x.NUMERO_APORTE = h.NUMERO_APORTE And Trunc(x.FECHA_HISTORICO) Between to_Date('" + dateHelper.PrimerDiaDelMes(riesgo.fecha_corte.Value.AddYears(-1).AddMonths(3)).ToShortDateString() + @"', 'dd/MM/yyyy') And  to_Date('" + dateHelper.UltimoDiaDelMes(riesgo.fecha_corte.Value.AddYears(-1).AddMonths(3)).ToShortDateString() + @"', 'dd/MM/yyyy')), 0) As brecha3,
                                            Nvl((Select Sum(x.CUOTA) From HISTORICO_APORTE x Where x.NUMERO_APORTE = h.NUMERO_APORTE And Trunc(x.FECHA_HISTORICO) Between to_Date('" + dateHelper.PrimerDiaDelMes(riesgo.fecha_corte.Value.AddYears(-1).AddMonths(4)).ToShortDateString() + @"', 'dd/MM/yyyy') And  to_Date('" + dateHelper.UltimoDiaDelMes(riesgo.fecha_corte.Value.AddYears(-1).AddMonths(6)).ToShortDateString() + @"', 'dd/MM/yyyy')), 0) As brecha4,
                                            Nvl((Select Sum(x.CUOTA) From HISTORICO_APORTE x Where x.NUMERO_APORTE = h.NUMERO_APORTE And Trunc(x.FECHA_HISTORICO) Between to_Date('" + dateHelper.PrimerDiaDelMes(riesgo.fecha_corte.Value.AddYears(-1).AddMonths(7)).ToShortDateString() + @"', 'dd/MM/yyyy') And  to_Date('" + dateHelper.UltimoDiaDelMes(riesgo.fecha_corte.Value.AddYears(-1).AddMonths(9)).ToShortDateString() + @"', 'dd/MM/yyyy')), 0) As brecha5,
                                            Nvl((Select Sum(x.CUOTA) From HISTORICO_APORTE x Where x.NUMERO_APORTE = h.NUMERO_APORTE And Trunc(x.FECHA_HISTORICO) Between to_Date('" + dateHelper.PrimerDiaDelMes(riesgo.fecha_corte.Value.AddYears(-1).AddMonths(10)).ToShortDateString() + @"', 'dd/MM/yyyy') And  to_Date('" + dateHelper.UltimoDiaDelMes(riesgo.fecha_corte.Value).ToShortDateString() + @"', 'dd/MM/yyyy')), 0) As brecha6
                                        FROM HISTORICO_APORTE h
                                        JOIN APORTE apo on h.NUMERO_APORTE = apo.NUMERO_APORTE
                                        JOIN V_PERSONA per on per.COD_PERSONA = apo.COD_PERSONA
                                        WHERE h.FECHA_HISTORICO 
                                        between to_Date('" + dateHelper.PrimerDiaDelMes(riesgo.fecha_corte.Value.AddYears(-1).AddMonths(1)).ToShortDateString() + "', 'dd/MM/yyyy') and to_Date('" + dateHelper.UltimoDiaDelMes(riesgo.fecha_corte.Value).ToShortDateString() + "', 'dd/MM/yyyy') " +
                                        " order by 1 ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RiesgoLiquidez entidad = new RiesgoLiquidez();

                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["cuota"] != DBNull.Value) entidad.cuota_actual = Convert.ToDecimal(resultado["cuota"]);
                            if (resultado["SALDO"] != DBNull.Value)
                            {
                                entidad.saldo_actual = Convert.ToDecimal(resultado["SALDO"]);
                            }
                            else
                            {
                                entidad.saldo_actual = 0;
                            }

                            if (resultado["brecha1"] != DBNull.Value) entidad.vr_brecha1 = Convert.ToDecimal(resultado["brecha1"]);
                            if (resultado["brecha2"] != DBNull.Value) entidad.vr_brecha2 = Convert.ToDecimal(resultado["brecha2"]);
                            if (resultado["brecha3"] != DBNull.Value) entidad.vr_brecha3 = Convert.ToDecimal(resultado["brecha3"]);
                            if (resultado["brecha4"] != DBNull.Value) entidad.vr_brecha4 = Convert.ToDecimal(resultado["brecha4"]);
                            if (resultado["brecha5"] != DBNull.Value) entidad.vr_brecha5 = Convert.ToDecimal(resultado["brecha5"]);
                            if (resultado["brecha6"] != DBNull.Value) entidad.vr_brecha6 = Convert.ToDecimal(resultado["brecha6"]);

                            listaProyeccion.Add(entidad);
                        }

                        return listaProyeccion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RiesgoLiquidezData", "ListarProyeccionAporte", ex);
                        return null;
                    }
                }
            }
        }

        public List<RiesgoLiquidez> ListarProyeccionCartera(RiesgoLiquidez riesgo, Usuario usuario)
        {
            List<RiesgoLiquidez> listaProyeccion = new List<RiesgoLiquidez>();
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DateTimeHelper dateHelper = new DateTimeHelper();

                        string sql = @"Select h.fecha_historico, c.descripcion, p.identificacion, p.nombre, h.numero_radicacion, h.saldo_capital, h.cod_categoria, h.cod_categoria_cli,
                                            Nvl((Select Sum(x.valor) From historico_amortiza x Where x.fecha_historico = h.fecha_historico And x.numero_radicacion = h.numero_radicacion And x.cod_atr = 1 And Trunc(x.fecha_cuota) Between to_Date('" + dateHelper.PrimerDiaDelMes(riesgo.fecha_corte.Value.AddMonths(1)).ToShortDateString() + @"', 'dd/MM/yyyy') And to_Date('" + dateHelper.UltimoDiaDelMes(riesgo.fecha_corte.Value.AddMonths(1)).ToShortDateString() + @"', 'dd/MM/yyyy')), 0) As brecha1,
                                            Nvl((Select Sum(x.valor) From historico_amortiza x Where x.fecha_historico = h.fecha_historico And x.numero_radicacion = h.numero_radicacion And x.cod_atr = 1 And Trunc(x.fecha_cuota) Between to_Date('" + dateHelper.PrimerDiaDelMes(riesgo.fecha_corte.Value.AddMonths(2)).ToShortDateString() + @"', 'dd/MM/yyyy') And to_Date('" + dateHelper.UltimoDiaDelMes(riesgo.fecha_corte.Value.AddMonths(2)).ToShortDateString() + @"', 'dd/MM/yyyy')), 0) As brecha2,
                                            Nvl((Select Sum(x.valor) From historico_amortiza x Where x.fecha_historico = h.fecha_historico And x.numero_radicacion = h.numero_radicacion And x.cod_atr = 1 And Trunc(x.fecha_cuota) Between to_Date('" + dateHelper.PrimerDiaDelMes(riesgo.fecha_corte.Value.AddMonths(3)).ToShortDateString() + @"', 'dd/MM/yyyy') And  to_Date('" + dateHelper.UltimoDiaDelMes(riesgo.fecha_corte.Value.AddMonths(3)).ToShortDateString() + @"', 'dd/MM/yyyy')), 0) As brecha3,
                                            Nvl((Select Sum(x.valor) From historico_amortiza x Where x.fecha_historico = h.fecha_historico And x.numero_radicacion = h.numero_radicacion And x.cod_atr = 1 And Trunc(x.fecha_cuota) Between to_Date('" + dateHelper.PrimerDiaDelMes(riesgo.fecha_corte.Value.AddMonths(4)).ToShortDateString() + @"', 'dd/MM/yyyy') And  to_Date('" + dateHelper.UltimoDiaDelMes(riesgo.fecha_corte.Value.AddMonths(6)).ToShortDateString() + @"', 'dd/MM/yyyy')), 0) As brecha4,
                                            Nvl((Select Sum(x.valor) From historico_amortiza x Where x.fecha_historico = h.fecha_historico And x.numero_radicacion = h.numero_radicacion And x.cod_atr = 1 And Trunc(x.fecha_cuota) Between to_Date('" + dateHelper.PrimerDiaDelMes(riesgo.fecha_corte.Value.AddMonths(7)).ToShortDateString() + @"', 'dd/MM/yyyy') And  to_Date('" + dateHelper.UltimoDiaDelMes(riesgo.fecha_corte.Value.AddMonths(9)).ToShortDateString() + @"', 'dd/MM/yyyy')), 0) As brecha5,
                                            Nvl((Select Sum(x.valor) From historico_amortiza x Where x.fecha_historico = h.fecha_historico And x.numero_radicacion = h.numero_radicacion And x.cod_atr = 1 And Trunc(x.fecha_cuota) Between to_Date('" + dateHelper.PrimerDiaDelMes(riesgo.fecha_corte.Value.AddMonths(10)).ToShortDateString() + @"', 'dd/MM/yyyy') And  to_Date('" + dateHelper.UltimoDiaDelMes(riesgo.fecha_corte.Value.AddMonths(12)).ToShortDateString() + @"', 'dd/MM/yyyy')), 0) As brecha6,
                                            Nvl((Select Sum(x.valor) From historico_amortiza x Where x.fecha_historico = h.fecha_historico And x.numero_radicacion = h.numero_radicacion And x.cod_atr = 1 And Trunc(x.fecha_cuota) > to_Date('" + dateHelper.PrimerDiaDelMes(riesgo.fecha_corte.Value.AddYears(1).AddMonths(1)).ToShortDateString() + @"', 'dd/MM/yyyy')), 0) As brecha7
                                        From historico_cre h 
                                        Left Join v_persona p On h.cod_cliente = p.cod_persona 
                                        Left Join clasificacion c On h.cod_clasifica = c.cod_clasifica
                                        Where Trunc(h.fecha_historico) = to_Date('" + riesgo.fecha_corte.Value.ToShortDateString() + @"', 'dd/MM/yyyy')
                                        and h.COD_CATEGORIA = 'A'
                                        and h.COD_CLASIFICA = " + (int)riesgo.clasificacion_cartera;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RiesgoLiquidez entidad = new RiesgoLiquidez();

                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["fecha_historico"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToString(resultado["numero_radicacion"]);
                            if (resultado["cod_categoria"] != DBNull.Value) entidad.cod_categoria = Convert.ToString(resultado["cod_categoria"]);
                            if (resultado["saldo_capital"] != DBNull.Value)
                            {
                                entidad.saldo_actual = Convert.ToDecimal(resultado["saldo_capital"]);
                            }
                            else
                            {
                                entidad.saldo_actual = 0;
                            }

                            if (resultado["brecha1"] != DBNull.Value) entidad.vr_brecha1 = Convert.ToDecimal(resultado["brecha1"]);
                            if (resultado["brecha2"] != DBNull.Value) entidad.vr_brecha2 = Convert.ToDecimal(resultado["brecha2"]);
                            if (resultado["brecha3"] != DBNull.Value) entidad.vr_brecha3 = Convert.ToDecimal(resultado["brecha3"]);
                            if (resultado["brecha4"] != DBNull.Value) entidad.vr_brecha4 = Convert.ToDecimal(resultado["brecha4"]);
                            if (resultado["brecha5"] != DBNull.Value) entidad.vr_brecha5 = Convert.ToDecimal(resultado["brecha5"]);
                            if (resultado["brecha6"] != DBNull.Value) entidad.vr_brecha6 = Convert.ToDecimal(resultado["brecha6"]);
                            if (resultado["brecha7"] != DBNull.Value) entidad.vr_brecha7 = Convert.ToDecimal(resultado["brecha7"]);

                            listaProyeccion.Add(entidad);
                        }

                        return listaProyeccion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RiesgoLiquidezData", "ListarProyeccionCartera", ex);
                        return null;
                    }
                }
            }
        }

        public List<RiesgoLiquidez> ListarProyeccionAhorro(RiesgoLiquidez riesgo, Usuario usuario)
        {
            List<RiesgoLiquidez> listaProyeccion = new List<RiesgoLiquidez>();
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DateTimeHelper dateHelper = new DateTimeHelper();

                        string sql = @"select cod_cuenta, fecha, saldo
                                        from riesgo_liquidez_saldos ries
                                        where ries.cod_cuenta In ('2130', '2105') 
                                        and ries.fecha
                                        between to_Date('" + dateHelper.PrimerDiaDelMes(riesgo.fecha_corte.Value.AddYears(-1).AddMonths(1)).ToShortDateString() + "', 'dd/MM/yyyy') and to_Date('" + dateHelper.UltimoDiaDelMes(riesgo.fecha_corte.Value).ToShortDateString() + "', 'dd/MM/yyyy') ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RiesgoLiquidez entidad = new RiesgoLiquidez();

                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToInt64(resultado["COD_CUENTA"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["SALDO"] != DBNull.Value)
                            {
                                entidad.saldo_madurar = Convert.ToDecimal(resultado["SALDO"]);
                            }
                            else
                            {
                                entidad.saldo_madurar = 0;
                            }

                            listaProyeccion.Add(entidad);
                        }

                        return listaProyeccion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RiesgoLiquidezData", "ListarProyeccionAhorro", ex);
                        return null;
                    }
                }
            }
        }

        public List<RiesgoLiquidez> ListarProyeccionDisponible(RiesgoLiquidez riesgo, Usuario usuario)
        {
            List<RiesgoLiquidez> listaProyeccion = new List<RiesgoLiquidez>();
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DateTimeHelper dateHelper = new DateTimeHelper();

                        string sql = @"select cod_cuenta, fecha, saldo
                                        from riesgo_liquidez_saldos ries
                                        where ries.cod_cuenta IN ('11', '1120')
                                        and ries.fecha
                                        between to_Date('" + dateHelper.PrimerDiaDelMes(riesgo.fecha_corte.Value.AddYears(-1).AddMonths(1)).ToShortDateString() + "', 'dd/MM/yyyy') and to_Date('" + dateHelper.UltimoDiaDelMes(riesgo.fecha_corte.Value).ToShortDateString() + "', 'dd/MM/yyyy') ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RiesgoLiquidez entidad = new RiesgoLiquidez();

                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToInt64(resultado["COD_CUENTA"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["SALDO"] != DBNull.Value)
                            {
                                entidad.saldo_madurar = Convert.ToDecimal(resultado["SALDO"]);
                            }
                            else
                            {
                                entidad.saldo_madurar = 0;
                            }

                            listaProyeccion.Add(entidad);
                        }

                        return listaProyeccion;
                    }
                    catch(Exception ex)
                    {
                        BOExcepcion.Throw("RiesgoLiquidezData", "ListarProyeccionDisponible", ex);
                        return null;
                    }
                }
            }
        }

        public decimal CalculoSaldoContable(DateTime pFecha, string pCod_cuenta, bool bNiif, Usuario vUsuario)
        {
            decimal SaldoContable = 0;
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = string.Empty;

                        if (bNiif)
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql = "select SALDOCUENTACONTABLE_NIIF(to_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "') ,'" + pCod_cuenta + "') as SALDO from dual";
                            else
                                sql = "select SALDOCUENTACONTABLE_NIIF('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "' , '" + pCod_cuenta + "') as SALDO";
                        else
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql = "select SALDOCUENTACONTABLE(to_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "') ,'" + pCod_cuenta + "') as SALDO from dual";
                            else
                                sql = "select SALDOCUENTACONTABLE('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "' , '" + pCod_cuenta + "') as SALDO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["SALDO"] != DBNull.Value) SaldoContable = Convert.ToDecimal(resultado["SALDO"]);                            
                        }

                        return SaldoContable;
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
        }

        public decimal CalculoMovimientoContable(DateTime pFecha, string pCod_cuenta, bool pNiif,  Usuario vUsuario)
        {
            decimal MovContable = 0;
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = string.Empty;

                        if (pNiif)
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql = @"SELECT SUM(CASE D.TIPO WHEN D.NATURALEZA THEN D.VALOR ELSE -D.VALOR END) AS MOVIMIENTO
                                    FROM E_COMPROBANTE E INNER JOIN D_COMPROBANTE D ON E.NUM_COMP = D.NUM_COMP AND E.TIPO_COMP = D.TIPO_COMP 
                                    WHERE TRUNC(E.FECHA) = to_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "') AND D.COD_CUENTA_NIIF LIKE '" + pCod_cuenta + "' ";
                            else
                                sql = @"SELECT SUM(CASE D.TIPO WHEN D.NATURALEZA THEN D.VALOR ELSE -D.VALOR END) AS MOVIMIENTO
                                    FROM E_COMPROBANTE E INNER JOIN D_COMPROBANTE D ON E.NUM_COMP = D.NUM_COMP AND E.TIPO_COMP = D.TIPO_COMP 
                                    WHERE TRUNC(E.FECHA) = '" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "' AND D.COD_CUENTA_NIIF LIKE '" + pCod_cuenta + "' ";
                        else
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql = @"SELECT SUM(CASE D.TIPO WHEN D.NATURALEZA THEN D.VALOR ELSE -D.VALOR END) AS MOVIMIENTO
                                        FROM E_COMPROBANTE E INNER JOIN D_COMPROBANTE D ON E.NUM_COMP = D.NUM_COMP AND E.TIPO_COMP = D.TIPO_COMP 
                                        WHERE TRUNC(E.FECHA) = to_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "') AND D.COD_CUENTA LIKE '" + pCod_cuenta + "' ";
                            else
                                sql = @"SELECT SUM(CASE D.TIPO WHEN D.NATURALEZA THEN D.VALOR ELSE -D.VALOR END) AS MOVIMIENTO
                                        FROM E_COMPROBANTE E INNER JOIN D_COMPROBANTE D ON E.NUM_COMP = D.NUM_COMP AND E.TIPO_COMP = D.TIPO_COMP 
                                        WHERE TRUNC(E.FECHA) = '" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "' AND D.COD_CUENTA LIKE '" + pCod_cuenta + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["MOVIMIENTO"] != DBNull.Value) MovContable = Convert.ToDecimal(resultado["MOVIMIENTO"]);
                        }

                        return MovContable;
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
        }

        //METODO DE CONSULTA A LA TABLA RIESGO_LIQUIDEZ_SALDOS
        public decimal CalculoMovimientoSaldos(DateTime pFecha, string pCod_cuenta, bool pNiif, Usuario vUsuario)
        {
            decimal MovContable = 0;
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = string.Empty;

                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = @"SELECT SUM(R.CREDITO - R.DEBITO) 
                                    FROM RIESGO_LIQUIDEZ_SALDOS R 
                                    WHERE TRUNC(R.FECHA) > to_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha()
                                   + @"') AND R.COD_CUENTA LIKE '" + pCod_cuenta + "' ";
                        else
                            sql = @"SELECT SUM(R.CREDITO - R.DEBITO) 
                                    FROM RIESGO_LIQUIDEZ_SALDOS R 
                                    WHERE TRUNC(R.FECHA) > '" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "' AND R.COD_CUENTA LIKE '" + pCod_cuenta + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["MOVIMIENTO"] != DBNull.Value) MovContable = Convert.ToDecimal(resultado["MOVIMIENTO"]);
                        }

                        return MovContable;
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
        }


        public decimal CarteraSaldoActualEmpleados(DateTime pFecha, Usuario vUsuario)
        {
            decimal SaldoContable = 0;
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = string.Empty;

                        if (!manejaLineasCreditoEmpleados(vUsuario))
                            return 0;

                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                        {
                            sql = @"SELECT SUM(H.SALDO_CAPITAL) as SALDO FROM HISTORICO_CRE H LEFT JOIN PERSONA P ON H.COD_CLIENTE = P.COD_PERSONA WHERE H.FECHA_HISTORICO = to_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"') " 
                                + " AND P.EMPLEADO_ENTIDAD = 1 ";
                        }
                        else
                        {
                            sql = @"SELECT SUM(H.SALDO_CAPITAL) as SALDO FROM HISTORICO_CRE H LEFT JOIN PERSONA P ON H.COD_CLIENTE = P.COD_PERSONA WHERE H.FECHA_HISTORICO = '" + pFecha.ToString(conf.ObtenerFormatoFecha()) 
                                + " AND P.EMPLEADO_ENTIDAD = 1 ";
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["SALDO"] != DBNull.Value) SaldoContable = Convert.ToDecimal(resultado["SALDO"]);
                        }

                        return SaldoContable;
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
        }


        public decimal CarteraSaldoActual(DateTime pFecha, int pCod_Clasifica, Usuario vUsuario)
        {
            decimal SaldoContable = 0;
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = string.Empty;

                        if (manejaLineasCreditoEmpleados(vUsuario))
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            {
                                sql = @"SELECT SUM(H.SALDO_CAPITAL) as SALDO FROM HISTORICO_CRE H LEFT JOIN PERSONA P ON H.COD_CLIENTE = P.COD_PERSONA WHERE H.FECHA_HISTORICO = to_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"') AND H.COD_CLASIFICA = " + pCod_Clasifica
                                    + " AND (P.EMPLEADO_ENTIDAD IS NULL OR P.EMPLEADO_ENTIDAD != 1)";
                            }
                            else
                            {
                                sql = @"SELECT SUM(H.SALDO_CAPITAL) as SALDO FROM HISTORICO_CRE H LEFT JOIN PERSONA P ON H.COD_CLIENTE = P.COD_PERSONA WHERE H.FECHA_HISTORICO = '" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "' AND H.COD_CLASIFICA = " + pCod_Clasifica
                                    + " AND (P.EMPLEADO_ENTIDAD IS NULL OR P.EMPLEADO_ENTIDAD != 1)";
                            }
                        else
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            {
                                sql = @"SELECT SUM(H.SALDO_CAPITAL) as SALDO FROM HISTORICO_CRE H WHERE H.FECHA_HISTORICO = to_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"') AND H.COD_CLASIFICA = " + pCod_Clasifica
                                    + " ";
                            }
                            else
                            {
                                sql = @"SELECT SUM(H.SALDO_CAPITAL) as SALDO FROM HISTORICO_CRE H WHERE H.FECHA_HISTORICO = '" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "' AND H.COD_CLASIFICA = " + pCod_Clasifica
                                    + " ";
                            }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["SALDO"] != DBNull.Value) SaldoContable = Convert.ToDecimal(resultado["SALDO"]);
                        }

                        return SaldoContable;
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
        }


        public decimal CarteraMaduracionBandas(DateTime pFecha, DateTime pFechaIni, DateTime pFechaFin, string pFiltro, Usuario vUsuario)
        {
            decimal SaldoContable = 0;
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = string.Empty;
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                        {
                            sql = @"SELECT NVL(SUM(H.VALOR),0) AS SALDO FROM HISTORICO_CRE C LEFT JOIN PERSONA P ON C.COD_CLIENTE = P.COD_PERSONA INNER JOIN HISTORICO_AMORTIZA H ON H.FECHA_HISTORICO = C.FECHA_HISTORICO AND C.NUMERO_RADICACION = H.NUMERO_RADICACION 
                                    WHERE C.COD_CATEGORIA = 'A' 
                                    AND H.FECHA_HISTORICO = TO_DATE('" + pFecha.ToString(conf.ObtenerFormatoFecha())+ "','" + conf.ObtenerFormatoFecha()+ @"')  AND H.COD_ATR IN (SELECT cod_atr FROM atributos WHERE proyecta_riesgoliq = 1 OR cod_atr = 1)
                                    AND H.FECHA_CUOTA BETWEEN TO_DATE('" + pFechaIni.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "') AND TO_DATE('" + pFechaFin.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "')"
                                    + (pFiltro == null || pFiltro == "" ? "" : (" AND " + pFiltro));
                        }
                        else
                        {
                            sql = @"SELECT NVL(SUM(H.VALOR),0) AS SALDO FROM HISTORICO_CRE C LEFT JOIN PERSONA P ON C.COD_CLIENTE = P.COD_PERSONA INNER JOIN HISTORICO_AMORTIZA H ON H.FECHA_HISTORICO = C.FECHA_HISTORICO AND C.NUMERO_RADICACION = H.NUMERO_RADICACION 
                                    WHERE C.COD_CATEGORIA = 'A' 
                                    AND H.FECHA_HISTORICO = '" + pFecha.ToString(conf.ObtenerFormatoFecha()) + @"' AND H.COD_ATR IN (SELECT cod_atr FROM atributos WHERE proyecta_riesgoliq = 1 OR cod_atr = 1)
                                    AND H.FECHA_CUOTA BETWEEN '" + pFechaIni.ToString(conf.ObtenerFormatoFecha()) + "' AND '" + pFechaFin.ToString(conf.ObtenerFormatoFecha()) + "'"
                                    + (pFiltro == null || pFiltro == "" ? "" : (" AND " + pFiltro));
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["SALDO"] != DBNull.Value) SaldoContable = Convert.ToDecimal(resultado["SALDO"]);
                        }

                        return SaldoContable;
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
        }

        public bool SaldoDiario(DateTime pFecha, string pCod_cuenta, ref decimal saldoDIA, bool pNiif, Usuario vUsuario)
        {
            saldoDIA = 0;
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = string.Empty;

                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = @"SELECT E.SALDO FROM RIESGO_LIQUIDEZ_SALDOS E WHERE TRUNC(E.FECHA) = to_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "') AND E.COD_CUENTA = '" + pCod_cuenta + "' ";
                        else
                            sql = @"SELECT E.SALDO FROM RIESGO_LIQUIDEZ_SALDOS E WHERE E.FECHA = '" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "' AND E.COD_CUENTA LIKE '" + pCod_cuenta + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["SALDO"] != DBNull.Value) saldoDIA = Convert.ToDecimal(resultado["SALDO"]);
                            return true;
                        }

                        return false;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public decimal CausacionMaduracion(DateTime pFecha, DateTime pFechaIni, DateTime pFechaFin, string pFiltro, Usuario vUsuario)
        {
            decimal SaldoContable = 0;
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = string.Empty;
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                        {
                            sql = @"SELECT NVL(SUM(C.SALDO_CAUSADO),0) AS SALDO FROM CAUSACION C INNER JOIN HISTORICO_CRE H ON C.NUMERO_RADICACION = H.NUMERO_RADICACION AND C.FECHA_CORTE = H.FECHA_HISTORICO
                                    WHERE H.COD_CATEGORIA = 'A' 
                                    AND H.FECHA_CUOTA BETWEEN TO_DATE('" + pFechaIni.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "') AND TO_DATE('" + pFechaFin.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + @"')
                                    AND C.FECHA_CORTE = TO_DATE('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + @"') AND C.COD_ATR IN (2, 3) "
                                    + (pFiltro == null || pFiltro == "" ? "" : (" AND " + pFiltro)); ;
                        }
                        else
                        {
                            sql = @"SELECT NVL(SUM(C.SALDO_CAUSADO),0) AS SALDO FROM CAUSACION C INNER JOIN HISTORICO_CRE H ON C.NUMERO_RADICACION = H.NUMERO_RADICACION AND C.FECHA_CORTE = H.FECHA_HISTORICO
                                    WHERE H.COD_CATEGORIA = 'A'
                                    AND H.FECHA_CUOTA BETWEEN '" + pFechaIni.ToString(conf.ObtenerFormatoFecha()) + "' AND '" + pFechaFin.ToString(conf.ObtenerFormatoFecha()) + @"')
                                    AND H.FECHA_CORTE = '" + pFecha.ToString(conf.ObtenerFormatoFecha()) + @"' AND C.COD_ATR IN (2, 3) "
                                    + pFiltro;
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["SALDO"] != DBNull.Value) SaldoContable = Convert.ToDecimal(resultado["SALDO"]);
                        }

                        return SaldoContable;
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
        }


        public decimal CausacionSaldoActual(DateTime pFecha, int pCod_Clasifica, Usuario vUsuario)
        {
            decimal SaldoContable = 0;
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = string.Empty;

                        if (manejaLineasCreditoEmpleados(vUsuario))
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            {
                                sql = @"SELECT NVL(SUM(C.SALDO_CAUSADO),0) AS SALDO FROM CAUSACION C INNER JOIN HISTORICO_CRE H ON C.NUMERO_RADICACION = H.NUMERO_RADICACION AND C.FECHA_CORTE = H.FECHA_HISTORICO LEFT JOIN PERSONA P ON H.COD_CLIENTE = P.COD_PERSONA "
                                    + " WHERE H.FECHA_HISTORICO = to_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"') AND H.COD_CLASIFICA = " + pCod_Clasifica
                                    + " AND (P.EMPLEADO_ENTIDAD IS NULL OR P.EMPLEADO_ENTIDAD != 1) ";
                            }
                            else
                            {
                                sql = @"SELECT NVL(SUM(C.SALDO_CAUSADO),0) AS SALDO FROM CAUSACION C INNER JOIN HISTORICO_CRE H ON C.NUMERO_RADICACION = H.NUMERO_RADICACION AND C.FECHA_CORTE = H.FECHA_HISTORICO LEFT JOIN PERSONA P ON H.COD_CLIENTE = P.COD_PERSONA "
                                    + " WHERE H.FECHA_HISTORICO = '" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "' AND H.COD_CLASIFICA = " + pCod_Clasifica
                                    + " AND (P.EMPLEADO_ENTIDAD IS NULL OR P.EMPLEADO_ENTIDAD != 1) ";
                            }
                        else
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            {
                                sql = @"SELECT NVL(SUM(C.SALDO_CAUSADO),0) AS SALDO FROM CAUSACION C INNER JOIN HISTORICO_CRE H ON C.NUMERO_RADICACION = H.NUMERO_RADICACION AND C.FECHA_CORTE = H.FECHA_HISTORICO "
                                    + " WHERE H.FECHA_HISTORICO = to_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"') AND H.COD_CLASIFICA = " + pCod_Clasifica
                                    + " ";
                            }
                            else
                            {
                                sql = @"SELECT NVL(SUM(C.SALDO_CAUSADO),0) AS SALDO FROM CAUSACION C INNER JOIN HISTORICO_CRE H ON C.NUMERO_RADICACION = H.NUMERO_RADICACION AND C.FECHA_CORTE = H.FECHA_HISTORICO "
                                    + " WHERE H.FECHA_HISTORICO = '" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "' AND H.COD_CLASIFICA = " + pCod_Clasifica
                                    + " ";
                            }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["SALDO"] != DBNull.Value) SaldoContable = Convert.ToDecimal(resultado["SALDO"]);
                        }

                        return SaldoContable;
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
        }

        public decimal CausacionSaldoActualEmpleados(DateTime pFecha, Usuario vUsuario)
        {
            decimal SaldoContable = 0;
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = string.Empty;

                        if (manejaLineasCreditoEmpleados(vUsuario))
                        {
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            {
                                sql = @"SELECT NVL(SUM(C.SALDO_CAUSADO),0) AS SALDO FROM CAUSACION C INNER JOIN HISTORICO_CRE H ON C.NUMERO_RADICACION = H.NUMERO_RADICACION AND C.FECHA_CORTE = H.FECHA_HISTORICO LEFT JOIN PERSONA P ON H.COD_DEUDOR = P.COD_PERSONA "
                                    + " WHERE H.FECHA_HISTORICO = to_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"') " 
                                    + " AND P.EMPLEADO_ENTIDAD = 1 ";
                            }
                            else
                            {
                                sql = @"SELECT NVL(SUM(C.SALDO_CAUSADO),0) AS SALDO FROM CAUSACION C INNER JOIN HISTORICO_CRE H ON C.NUMERO_RADICACION = H.NUMERO_RADICACION AND C.FECHA_CORTE = H.FECHA_HISTORICO LEFT JOIN PERSONA P ON H.COD_DEUDOR = P.COD_PERSONA "
                                    + " WHERE H.FECHA_HISTORICO = '" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "' "
                                    + " AND P.EMPLEADO_ENTIDAD = 1 ";
                            }
                        }
                        else
                        {
                            return 0;
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["SALDO"] != DBNull.Value) SaldoContable = Convert.ToDecimal(resultado["SALDO"]);
                        }

                        return SaldoContable;
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
        }


        public List<MadurarCdats> ConsultaCDATS(DateTime pFecha, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<MadurarCdats> lstdatos = new List<MadurarCdats>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = string.Empty;
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                        {
                            sql = @"Select h.codigo_cdat, h.numero_cdat, h.fecha_inicio, h.fecha_vencimiento, h.valor, h.plazo, h.interes_causado, h.tasa_interes_nom, h.fecha_intereses, h.estado, 
                                        (Select Count(*) From prorroga_cdat p Left Join operacion o On o.cod_ope = p.cod_ope Join cdat c On p.codigo_cdat = c.codigo_cdat
                                        Where Trunc(Nvl(o.fecha_oper, p.fecha_inicio)) Between DatePrimerDiaDelMes(FecResDia(h.fecha_historico, 360, 1)) And h.fecha_historico And p.codigo_cdat = h.codigo_cdat) As prorrogas	
                                        From historico_cdat h
                                        Where h.fecha_historico = To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') And h.estado Not In ('3', '4')";
                        }
                        else
                        {
                            sql = @"Select h.codigo_cdat, h.numero_cdat, h.fecha_inicio, h.fecha_vencimiento, h.valor, h.plazo, h.interes_causado, h.tasa_interes_nom, h.fecha_intereses, h.estado, 
                                        (Select Count(*) From prorroga_cdat p Left Join operacion o On o.cod_ope = p.cod_ope Join cdat c On p.codigo_cdat = c.codigo_cdat
                                        Where Trunc(Nvl(o.fecha_oper, p.fecha_inicio)) Between DatePrimerDiaDelMes(FecResDia(h.fecha_historico, 360, 1)) And h.fecha_historico And p.codigo_cdat = h.codigo_cdat) As prorrogas	
                                        From historico_cdat h
                                        Where h.fecha_historico = '" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "' And h.estado Not In ('3', '4')";
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            MadurarCdats entidad = new MadurarCdats();
                            if (resultado["NUMERO_CDAT"] != DBNull.Value) entidad.numero_cdat = Convert.ToString(resultado["NUMERO_CDAT"]);
                            if (resultado["PRORROGAS"] != DBNull.Value) entidad.prorrogas = Convert.ToInt32(resultado["PRORROGAS"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            lstdatos.Add(entidad);
                        }

                        return lstdatos;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public bool manejaLineasCreditoEmpleados(Usuario pUsuario)
        {
            return false;
        }


    }
}
