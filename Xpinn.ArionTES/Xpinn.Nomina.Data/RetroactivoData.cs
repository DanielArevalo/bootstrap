using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Data
{
    public class RetroactivoData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public RetroactivoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Retroactivo CrearRetroactivo(Retroactivo pRetroactivo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pRetroactivo.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigopersona = cmdTransaccionFactory.CreateParameter();
                        pcodigopersona.ParameterName = "p_codigopersona";
                        pcodigopersona.Value = pRetroactivo.codigopersona;
                        pcodigopersona.Direction = ParameterDirection.Input;
                        pcodigopersona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigopersona);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pRetroactivo.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pRetroactivo.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pRetroactivo.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pcodigocentrocosto = cmdTransaccionFactory.CreateParameter();
                        pcodigocentrocosto.ParameterName = "p_codigocentrocosto";
                        if (pRetroactivo.codigocentrocosto == null)
                            pcodigocentrocosto.Value = DBNull.Value;
                        else
                            pcodigocentrocosto.Value = pRetroactivo.codigocentrocosto;
                        pcodigocentrocosto.Direction = ParameterDirection.Input;
                        pcodigocentrocosto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigocentrocosto);

                        DbParameter pfechainicio = cmdTransaccionFactory.CreateParameter();
                        pfechainicio.ParameterName = "p_fechainicio";
                        if (pRetroactivo.fechainicio == null)
                            pfechainicio.Value = DBNull.Value;
                        else
                            pfechainicio.Value = pRetroactivo.fechainicio;
                        pfechainicio.Direction = ParameterDirection.Input;
                        pfechainicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechainicio);

                        DbParameter pfechafinal = cmdTransaccionFactory.CreateParameter();
                        pfechafinal.ParameterName = "p_fechafinal";
                        if (pRetroactivo.fechafinal == null)
                            pfechafinal.Value = DBNull.Value;
                        else
                            pfechafinal.Value = pRetroactivo.fechafinal;
                        pfechafinal.Direction = ParameterDirection.Input;
                        pfechafinal.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechafinal);

                        DbParameter pfechapago = cmdTransaccionFactory.CreateParameter();
                        pfechapago.ParameterName = "p_fechapago";
                        if (pRetroactivo.fechapago == null)
                            pfechapago.Value = DBNull.Value;
                        else
                            pfechapago.Value = pRetroactivo.fechapago;
                        pfechapago.Direction = ParameterDirection.Input;
                        pfechapago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechapago);

                        DbParameter pnumeropagos = cmdTransaccionFactory.CreateParameter();
                        pnumeropagos.ParameterName = "p_numeropagos";
                        if (pRetroactivo.numeropagos == null)
                            pnumeropagos.Value = DBNull.Value;
                        else
                            pnumeropagos.Value = pRetroactivo.numeropagos;
                        pnumeropagos.Direction = ParameterDirection.Input;
                        pnumeropagos.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumeropagos);

                        DbParameter pperiodicidad = cmdTransaccionFactory.CreateParameter();
                        pperiodicidad.ParameterName = "p_periodicidad";
                        if (pRetroactivo.periodicidad == null)
                            pperiodicidad.Value = DBNull.Value;
                        else
                            pperiodicidad.Value = pRetroactivo.periodicidad;
                        pperiodicidad.Direction = ParameterDirection.Input;
                        pperiodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pperiodicidad);

                        DbParameter pconceptopagoretroactivo = cmdTransaccionFactory.CreateParameter();
                        pconceptopagoretroactivo.ParameterName = "p_conceptopagoretroactivo";
                        if (pRetroactivo.conceptopagoretroactivo == null)
                            pconceptopagoretroactivo.Value = DBNull.Value;
                        else
                            pconceptopagoretroactivo.Value = pRetroactivo.conceptopagoretroactivo;
                        pconceptopagoretroactivo.Direction = ParameterDirection.Input;
                        pconceptopagoretroactivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pconceptopagoretroactivo);

                        DbParameter pCodigoNomina = cmdTransaccionFactory.CreateParameter();
                        pCodigoNomina.ParameterName = "pCodigoNomina";
                        if (pRetroactivo.codigo_tipo_nomina == null)
                            pCodigoNomina.Value = DBNull.Value;
                        else
                            pCodigoNomina.Value = pRetroactivo.codigo_tipo_nomina;
                        pCodigoNomina.Direction = ParameterDirection.Input;
                        pCodigoNomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCodigoNomina);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_RETROACTIV_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pRetroactivo.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pRetroactivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RetroactivoData", "CrearRetroactivo", ex);
                        return null;
                    }
                }
            }
        }


        public Retroactivo ModificarRetroactivo(Retroactivo pRetroactivo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pRetroactivo.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigopersona = cmdTransaccionFactory.CreateParameter();
                        pcodigopersona.ParameterName = "p_codigopersona";
                        pcodigopersona.Value = pRetroactivo.codigopersona;
                        pcodigopersona.Direction = ParameterDirection.Input;
                        pcodigopersona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigopersona);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pRetroactivo.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pRetroactivo.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pRetroactivo.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pcodigocentrocosto = cmdTransaccionFactory.CreateParameter();
                        pcodigocentrocosto.ParameterName = "p_codigocentrocosto";
                        if (pRetroactivo.codigocentrocosto == null)
                            pcodigocentrocosto.Value = DBNull.Value;
                        else
                            pcodigocentrocosto.Value = pRetroactivo.codigocentrocosto;
                        pcodigocentrocosto.Direction = ParameterDirection.Input;
                        pcodigocentrocosto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigocentrocosto);

                        DbParameter pfechainicio = cmdTransaccionFactory.CreateParameter();
                        pfechainicio.ParameterName = "p_fechainicio";
                        if (pRetroactivo.fechainicio == null)
                            pfechainicio.Value = DBNull.Value;
                        else
                            pfechainicio.Value = pRetroactivo.fechainicio;
                        pfechainicio.Direction = ParameterDirection.Input;
                        pfechainicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechainicio);

                        DbParameter pfechafinal = cmdTransaccionFactory.CreateParameter();
                        pfechafinal.ParameterName = "p_fechafinal";
                        if (pRetroactivo.fechafinal == null)
                            pfechafinal.Value = DBNull.Value;
                        else
                            pfechafinal.Value = pRetroactivo.fechafinal;
                        pfechafinal.Direction = ParameterDirection.Input;
                        pfechafinal.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechafinal);

                        DbParameter pfechapago = cmdTransaccionFactory.CreateParameter();
                        pfechapago.ParameterName = "p_fechapago";
                        if (pRetroactivo.fechapago == null)
                            pfechapago.Value = DBNull.Value;
                        else
                            pfechapago.Value = pRetroactivo.fechapago;
                        pfechapago.Direction = ParameterDirection.Input;
                        pfechapago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechapago);

                        DbParameter pnumeropagos = cmdTransaccionFactory.CreateParameter();
                        pnumeropagos.ParameterName = "p_numeropagos";
                        if (pRetroactivo.numeropagos == null)
                            pnumeropagos.Value = DBNull.Value;
                        else
                            pnumeropagos.Value = pRetroactivo.numeropagos;
                        pnumeropagos.Direction = ParameterDirection.Input;
                        pnumeropagos.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumeropagos);

                        DbParameter pperiodicidad = cmdTransaccionFactory.CreateParameter();
                        pperiodicidad.ParameterName = "p_periodicidad";
                        if (pRetroactivo.periodicidad == null)
                            pperiodicidad.Value = DBNull.Value;
                        else
                            pperiodicidad.Value = pRetroactivo.periodicidad;
                        pperiodicidad.Direction = ParameterDirection.Input;
                        pperiodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pperiodicidad);

                        DbParameter pconceptopagoretroactivo = cmdTransaccionFactory.CreateParameter();
                        pconceptopagoretroactivo.ParameterName = "p_conceptopagoretroactivo";
                        if (pRetroactivo.conceptopagoretroactivo == null)
                            pconceptopagoretroactivo.Value = DBNull.Value;
                        else
                            pconceptopagoretroactivo.Value = pRetroactivo.conceptopagoretroactivo;
                        pconceptopagoretroactivo.Direction = ParameterDirection.Input;
                        pconceptopagoretroactivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pconceptopagoretroactivo);

                        DbParameter pCodigoNomina = cmdTransaccionFactory.CreateParameter();
                        pCodigoNomina.ParameterName = "pCodigoNomina";
                        if (pRetroactivo.codigo_tipo_nomina == null)
                            pCodigoNomina.Value = DBNull.Value;
                        else
                            pCodigoNomina.Value = pRetroactivo.codigo_tipo_nomina;
                        pCodigoNomina.Direction = ParameterDirection.Input;
                        pCodigoNomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCodigoNomina);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_RETROACTIV_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pRetroactivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RetroactivoData", "ModificarRetroactivo", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarRetroactivo(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Retroactivo pRetroactivo = new Retroactivo();
                        pRetroactivo = ConsultarRetroactivo(pId, vUsuario);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pRetroactivo.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_RETROACTIV_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RetroactivoData", "EliminarRetroactivo", ex);
                    }
                }
            }
        }


        public Retroactivo ConsultarRetroactivo(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Retroactivo entidad = new Retroactivo();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT ret.*, per.IDENTIFICACION, per.TIPO_IDENTIFICACION, per.NOMBRE
                                        FROM Retroactivo ret 
                                        JOIN v_persona per on ret.codigopersona = per.cod_persona
                                        WHERE ret.CONSECUTIVO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOPERSONA"] != DBNull.Value) entidad.codigopersona = Convert.ToInt64(resultado["CODIGOPERSONA"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);
                            if (resultado["FECHAINICIO"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["FECHAINICIO"]);
                            if (resultado["FECHAFINAL"] != DBNull.Value) entidad.fechafinal = Convert.ToDateTime(resultado["FECHAFINAL"]);
                            if (resultado["FECHAPAGO"] != DBNull.Value) entidad.fechapago = Convert.ToDateTime(resultado["FECHAPAGO"]);
                            if (resultado["NUMEROPAGOS"] != DBNull.Value) entidad.numeropagos = Convert.ToInt32(resultado["NUMEROPAGOS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToInt32(resultado["PERIODICIDAD"]);
                            if (resultado["CONCEPTOPAGORETROACTIVO"] != DBNull.Value) entidad.conceptopagoretroactivo = Convert.ToInt32(resultado["CONCEPTOPAGORETROACTIVO"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigo_tipo_nomina = Convert.ToInt64(resultado["CODIGONOMINA"]);

                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["tipo_identificacion"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["tipo_identificacion"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["NOMBRE"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RetroactivoData", "ConsultarRetroactivo", ex);
                        return null;
                    }
                }
            }
        }


        public List<Retroactivo> ListarRetroactivo(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Retroactivo> lstRetroactivo = new List<Retroactivo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select ret.*, per.NOMBRE, per.identificacion,
                                        CASE ret.periodicidad WHEN 1 THEN '1er Periodo' WHEN 2 THEN '2do Periodo' WHEN 3 THEN '3er Periodo' WHEN 4 THEN 'Ultimo Periodo' WHEN 5 THEN 'Todos los Periodos' END as desc_periodo,
                                        con.descripcion as desc_concepto
                                        from retroactivo ret
                                        JOIN v_persona per on ret.CODIGOPERSONA = per.COD_PERSONA
                                        JOIN EMPLEADOS emp on per.COD_PERSONA = emp.COD_PERSONA
                                        JOIN CONCEPTO_NOMINA con on con.consecutivo = ret.CONCEPTOPAGORETROACTIVO " + filtro + " ORDER BY emp.CONSECUTIVO desc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Retroactivo entidad = new Retroactivo();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOPERSONA"] != DBNull.Value) entidad.codigopersona = Convert.ToInt64(resultado["CODIGOPERSONA"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);
                            if (resultado["FECHAINICIO"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["FECHAINICIO"]);
                            if (resultado["FECHAFINAL"] != DBNull.Value) entidad.fechafinal = Convert.ToDateTime(resultado["FECHAFINAL"]);
                            if (resultado["FECHAPAGO"] != DBNull.Value) entidad.fechapago = Convert.ToDateTime(resultado["FECHAPAGO"]);
                            if (resultado["NUMEROPAGOS"] != DBNull.Value) entidad.numeropagos = Convert.ToInt32(resultado["NUMEROPAGOS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToInt32(resultado["PERIODICIDAD"]);
                            if (resultado["CONCEPTOPAGORETROACTIVO"] != DBNull.Value) entidad.conceptopagoretroactivo = Convert.ToInt32(resultado["CONCEPTOPAGORETROACTIVO"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigo_tipo_nomina = Convert.ToInt64(resultado["CODIGONOMINA"]);

                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["nombre"]);
                            if (resultado["desc_periodo"] != DBNull.Value) entidad.desc_periodo = Convert.ToString(resultado["desc_periodo"]);
                            if (resultado["desc_concepto"] != DBNull.Value) entidad.desc_concepto = Convert.ToString(resultado["desc_concepto"]);
                            
                            lstRetroactivo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstRetroactivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RetroactivoData", "ListarRetroactivo", ex);
                        return null;
                    }
                }
            }
        }


    }
}
