using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.Data;
using System.Data.Common;


namespace Xpinn.FabricaCreditos.Data
{
    public class ScoringData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public ScoringData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public Scoring ConsultarDatosScoring(Scoring pScoring, Usuario pUsuario)
        {
            DbDataReader resultado;
            Scoring entidad = new Scoring() { cod_persona = pScoring.cod_persona };

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //Consulta modificada para traer la suma de las valor de las cuotas que tenga el asociado en servicios
                        //Se modificó la suma de ahorros voluntarios ya que se hacia un calculo incosistente
                        //Se valido que el valor de la cuota de aporte solo sea de tipo aporte 1
                        //Modificación para sumar en el valor del saldo de aporte el valor del ahorro permanente
                        string sql = @"select per.cod_nomina, per.identificacion, per.TIPO_IDENTIFICACION, tipoi.descripcion as tipo_identificacion_desc, per.primer_nombre, per.segundo_nombre, per.primer_apellido, per.segundo_apellido,
                                          afi.idafiliacion as cod_afiliacion, NVL(est.SUELDO_PERSONA, 0) as salario, per.empresa, cargo.descripcion as descripcion_cargo, per.direccionempresa,
                                          per.fecha_ingresoempresa, afi.fecha_afiliacion,  NVL(est.honorarios, 0) as honorarios, NVL(est.otros_ingresos, 0) as otros_ingresos, sum(NVL(apo.cuota, 0)) as Valor_cuota_aporte,
                                          (NVL((select sum(valor_cuota) from ahorro_vista where estado = 1 and cod_persona = " + pScoring.cod_persona + @"), 0)) + (NVL((select sum(valor_cuota) 
                                          from ahorro_programado where estado = 1 and cod_persona =" + pScoring.cod_persona + @"), 0)) as Ahorro_Voluntario,
                                          NVL((select SUM(saldo) from aporte where cod_linea_aporte in (1,2) and cod_persona = " + pScoring.cod_persona + @"),0) as  Saldo_Aporte, (select sum(valor_cuota) from servicios where COD_PERSONA =  " + pScoring.cod_persona + @" and estado = 'C') as Servicios
                                          from persona per
                                          left join persona_afiliacion afi on per.cod_persona = afi.cod_persona
                                          left join tipoidentificacion tipoi on per.tipo_identificacion = tipoi.codtipoidentificacion  
                                          left join Informacion_Ingre_Egre est on est.cod_persona = afi.cod_persona 
                                          left join cargo on per.codcargo = cargo.codcargo
                                          left join aporte apo on apo.cod_persona = afi.cod_persona 
                                          left join lineaaporte linapo on linapo.cod_linea_aporte = apo.cod_linea_aporte --and linapo.tipo_aporte = 1
                                          where per.cod_persona = " + pScoring.cod_persona +
                                          @" and linapo.tipo_aporte = 1
                                          Group by per.cod_nomina, per.identificacion, tipoi.descripcion, per.primer_nombre, per.segundo_nombre, per.primer_apellido, per.segundo_apellido,
                                          afi.idafiliacion , est.SUELDO_PERSONA, per.empresa, cargo.descripcion, per.direccionempresa,
                                          per.fecha_ingresoempresa, afi.fecha_afiliacion, est.honorarios, est.otros_ingresos, per.tipo_identificacion ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["cod_nomina"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["cod_nomina"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["tipo_identificacion"] != DBNull.Value) entidad.cod_tipo_identificacion = Convert.ToString(resultado["tipo_identificacion"]); 
                            if (resultado["tipo_identificacion_desc"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["tipo_identificacion_desc"]); 
                            if (resultado["primer_nombre"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["primer_nombre"]);
                            if (resultado["segundo_nombre"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["segundo_nombre"]);
                            if (resultado["primer_apellido"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["primer_apellido"]);
                            if (resultado["segundo_apellido"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["segundo_apellido"]);
                            if (resultado["cod_afiliacion"] != DBNull.Value) entidad.cod_afiliacion = Convert.ToInt64(resultado["cod_afiliacion"]);
                            if (resultado["salario"] != DBNull.Value) entidad.salario = Convert.ToDecimal(resultado["salario"]);
                            if (resultado["empresa"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["empresa"]);
                            if (resultado["descripcion_cargo"] != DBNull.Value) entidad.descripcion_cargo = Convert.ToString(resultado["descripcion_cargo"]);
                            if (resultado["direccionempresa"] != DBNull.Value) entidad.direccionempresa = Convert.ToString(resultado["direccionempresa"]);
                            if (resultado["fecha_ingresoempresa"] != DBNull.Value) entidad.fecha_ingresoempresa = Convert.ToDateTime(resultado["fecha_ingresoempresa"]);
                            if (resultado["fecha_afiliacion"] != DBNull.Value) entidad.fecha_afiliacion = Convert.ToDateTime(resultado["fecha_afiliacion"]);
                            if (resultado["honorarios"] != DBNull.Value) entidad.honorarios = Convert.ToDecimal(resultado["honorarios"]);
                            if (resultado["otros_ingresos"] != DBNull.Value) entidad.otros_ingresos = Convert.ToDecimal(resultado["otros_ingresos"]);
                            if (resultado["Saldo_Aporte"] != DBNull.Value) entidad.saldo_aporte = Convert.ToDecimal(resultado["Saldo_Aporte"]);
                            if (resultado["Valor_cuota_aporte"] != DBNull.Value) entidad.valor_cuota_aporte = Convert.ToDecimal(resultado["Valor_cuota_aporte"]);
                            if (resultado["Ahorro_Voluntario"] != DBNull.Value) entidad.ahorro_voluntario = Convert.ToDecimal(resultado["Ahorro_Voluntario"]);
                            if (resultado["Servicios"] != DBNull.Value) entidad.valor_servicios = Convert.ToDecimal(resultado["Servicios"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScoringData", "ConsultarDatosScoring", ex);
                        return null;
                    }
                }
            }
        }


        public decimal CalcularParafiscales(long ingresosTotales, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        decimal valorParafiscal = 0;

                        DbParameter p_ingresosTotales = cmdTransaccionFactory.CreateParameter();
                        p_ingresosTotales.ParameterName = "p_ingresosTotales";
                        p_ingresosTotales.Value = ingresosTotales;
                        p_ingresosTotales.Direction = ParameterDirection.Input;
                        p_ingresosTotales.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_ingresosTotales);

                        DbParameter p_Parafiscales = cmdTransaccionFactory.CreateParameter();
                        p_Parafiscales.ParameterName = "p_Parafiscales";
                        p_Parafiscales.Value = DBNull.Value;
                        p_Parafiscales.Direction = ParameterDirection.Output;
                        p_Parafiscales.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_Parafiscales);

                        connection.Open();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CALCULO_FAC_PEN";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        valorParafiscal = p_Parafiscales.Value != DBNull.Value ? Convert.ToDecimal(p_Parafiscales.Value.ToString()) : 0;

                        dbConnectionFactory.CerrarConexion(connection);

                        return valorParafiscal;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScoringData", "ConsultarFactorAntiguedad", ex);
                        return 0;
                    }
                }
            }
        }


        public byte[] ConsultarDocumentoScoring(long idAnalisis, Usuario usuario)
        {
            DbDataReader resultado;
            byte[] byteDocumento = null;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT PRE.IMAGEN
                                        FROM PREANALISIS_CREDITO PRE where PRE.IDPREANALISIS = " + idAnalisis;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IMAGEN"] != DBNull.Value) byteDocumento = (byte[])resultado["IMAGEN"];
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return byteDocumento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScoringData", "ConsultarDocumentoScoring", ex);
                        return null;
                    }
                }
            }
        }


        public List<Scoring> ListarScoresRealizados(string filtro, Usuario usuario)
        {
            DbDataReader resultado;
            List<Scoring> lstScoring = new List<Scoring>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select PRE.IDPREANALISIS, PRE.COD_PERSONA,PRE.FECHA, PRE.MONTO_SOLICITADO, PRE.PLAZO_SOLICITADO,
                                           PER.PRIMER_NOMBRE | | ' ' | | PER.SEGUNDO_NOMBRE | | ' ' | | PER.PRIMER_APELLIDO | | ' ' | | PER.SEGUNDO_APELLIDO AS NOMBRE,
                                           PER.IDENTIFICACION , PER.COD_NOMINA
                                        from PREANALISIS_CREDITO PRE
                                        JOIN PERSONA PER ON PRE.COD_PERSONA = PER.COD_PERSONA
                                        WHERE IMAGEN IS NOT NULL " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Scoring entidad = new Scoring();
                            if (resultado["IDPREANALISIS"] != DBNull.Value) entidad.idpreanalisis = Convert.ToInt64(resultado["IDPREANALISIS"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha_score = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.monto = Convert.ToDecimal(resultado["MONTO_SOLICITADO"]);
                            if (resultado["PLAZO_SOLICITADO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO_SOLICITADO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_completo = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);

                            lstScoring.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstScoring;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScoringData", "ListarScoresRealizados", ex);
                        return null;
                    }
                }
            }
        }


        public Scoring ConsultarFactorAntiguedad(long diferenciaFechas, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcheck = cmdTransaccionFactory.CreateParameter();
                        pidcheck.ParameterName = "p_dias_antiguedad";
                        pidcheck.Value = diferenciaFechas;
                        pidcheck.Direction = ParameterDirection.Input;
                        pidcheck.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidcheck);

                        DbParameter p_intercepto = cmdTransaccionFactory.CreateParameter();
                        p_intercepto.ParameterName = "p_intercepto";
                        p_intercepto.Value = DBNull.Value;
                        p_intercepto.Direction = ParameterDirection.Output;
                        p_intercepto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_intercepto);

                        DbParameter p_pendiente = cmdTransaccionFactory.CreateParameter();
                        p_pendiente.ParameterName = "p_pendiente";
                        p_pendiente.Value = DBNull.Value;
                        p_pendiente.Direction = ParameterDirection.Output;
                        p_pendiente.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_pendiente);

                        connection.Open();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CALCULO_FAC_ANT";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        Scoring factor = new Scoring();

                        factor.factor_intercepto = p_intercepto.Value != DBNull.Value ? Convert.ToDecimal(p_intercepto.Value.ToString()) : 0;
                        factor.factor_pendiente= p_pendiente.Value != DBNull.Value ? Convert.ToDecimal(p_pendiente.Value.ToString()) : 0;
                        factor.fecha_meses_antiguedad = diferenciaFechas;

                        dbConnectionFactory.CerrarConexion(connection);

                        return factor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScoringData", "ConsultarFactorAntiguedad", ex);
                        return null;
                    }
                }
            }
        }
    }
}
