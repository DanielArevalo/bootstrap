using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Data
{
    public class NovedadPrimaData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public NovedadPrimaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public NovedadPrima CrearNovedadPrima(NovedadPrima pNovedadPrima, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pNovedadPrima.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pNovedadPrima.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter psemestre = cmdTransaccionFactory.CreateParameter();
                        psemestre.ParameterName = "p_semestre";
                        psemestre.Value = pNovedadPrima.semestre;
                        psemestre.Direction = ParameterDirection.Input;
                        psemestre.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(psemestre);

                        DbParameter pcodigotiponovedad = cmdTransaccionFactory.CreateParameter();
                        pcodigotiponovedad.ParameterName = "p_codigotiponovedad";
                        pcodigotiponovedad.Value = pNovedadPrima.codigotiponovedad;
                        pcodigotiponovedad.Direction = ParameterDirection.Input;
                        pcodigotiponovedad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigotiponovedad);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pNovedadPrima.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pnovedadfuepagada = cmdTransaccionFactory.CreateParameter();
                        pnovedadfuepagada.ParameterName = "p_novedadfuepagada";
                        pnovedadfuepagada.Value = pNovedadPrima.novedadfuepagada;
                        pnovedadfuepagada.Direction = ParameterDirection.Input;
                        pnovedadfuepagada.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnovedadfuepagada);

                        DbParameter panio = cmdTransaccionFactory.CreateParameter();
                        panio.ParameterName = "p_anio";
                        panio.Value = pNovedadPrima.anio;
                        panio.Direction = ParameterDirection.Input;
                        panio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(panio);

                        DbParameter pcodigonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigonomina.ParameterName = "p_codigonomina";
                        pcodigonomina.Value = pNovedadPrima.codigonomina;
                        pcodigonomina.Direction = ParameterDirection.Input;
                        pcodigonomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigonomina);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_NOVEDADPRI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pNovedadPrima.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pNovedadPrima;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("NovedadPrimaData", "CrearNovedadPrima", ex);
                        return null;
                    }
                }
            }
        }


        public NovedadPrima ModificarNovedadPrima(NovedadPrima pNovedadPrima, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pNovedadPrima.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pNovedadPrima.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter psemestre = cmdTransaccionFactory.CreateParameter();
                        psemestre.ParameterName = "p_semestre";
                        psemestre.Value = pNovedadPrima.semestre;
                        psemestre.Direction = ParameterDirection.Input;
                        psemestre.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(psemestre);

                        DbParameter pcodigotiponovedad = cmdTransaccionFactory.CreateParameter();
                        pcodigotiponovedad.ParameterName = "p_codigotiponovedad";
                        pcodigotiponovedad.Value = pNovedadPrima.codigotiponovedad;
                        pcodigotiponovedad.Direction = ParameterDirection.Input;
                        pcodigotiponovedad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigotiponovedad);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pNovedadPrima.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pnovedadfuepagada = cmdTransaccionFactory.CreateParameter();
                        pnovedadfuepagada.ParameterName = "p_novedadfuepagada";
                        pnovedadfuepagada.Value = pNovedadPrima.novedadfuepagada;
                        pnovedadfuepagada.Direction = ParameterDirection.Input;
                        pnovedadfuepagada.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnovedadfuepagada);

                        DbParameter panio = cmdTransaccionFactory.CreateParameter();
                        panio.ParameterName = "p_anio";
                        panio.Value = pNovedadPrima.anio;
                        panio.Direction = ParameterDirection.Input;
                        panio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(panio);

                        DbParameter pcodigonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigonomina.ParameterName = "p_codigonomina";
                        pcodigonomina.Value = pNovedadPrima.codigonomina;
                        pcodigonomina.Direction = ParameterDirection.Input;
                        pcodigonomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigonomina);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_NOVEDADPRI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pNovedadPrima;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("NovedadPrimaData", "ModificarNovedadPrima", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarNovedadPrima(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        NovedadPrima pNovedadPrima = new NovedadPrima();
                        pNovedadPrima = ConsultarNovedadPrima(pId, vUsuario);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pNovedadPrima.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_NOVEDADPRI_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("NovedadPrimaData", "EliminarNovedadPrima", ex);
                    }
                }
            }
        }


        public NovedadPrima ConsultarNovedadPrima(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            NovedadPrima entidad = new NovedadPrima();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select nov.*, per.identificacion, per.nombre, per.TIPO_IDENTIFICACION
                                        from NovedadPrima nov
                                        JOIN Empleados emp on nov.CODIGOEMPLEADO = emp.consecutivo
                                        JOIN V_PERSONA per on emp.COD_PERSONA = per.COD_PERSONA
                                        WHERE nov.CONSECUTIVO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["SEMESTRE"] != DBNull.Value) entidad.semestre = Convert.ToInt32(resultado["SEMESTRE"]);
                            if (resultado["CODIGOTIPONOVEDAD"] != DBNull.Value) entidad.codigotiponovedad = Convert.ToInt64(resultado["CODIGOTIPONOVEDAD"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["NOVEDADFUEPAGADA"] != DBNull.Value) entidad.novedadfuepagada = Convert.ToInt32(resultado["NOVEDADFUEPAGADA"]);
                            if (resultado["ANIO"] != DBNull.Value) entidad.anio = Convert.ToInt64(resultado["ANIO"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("NovedadPrimaData", "ConsultarNovedadPrima", ex);
                        return null;
                    }
                }
            }
        }


        public List<NovedadPrima> ListarNovedadPrima(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<NovedadPrima> lstNovedadPrima = new List<NovedadPrima>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select nov.*, per.identificacion, per.nombre, nom.DESCRIPCION as desc_nomina, tip.DESCRIPCION as desc_tipoNovedad,
                                        CASE nov.Semestre WHEN 1 THEN 'Primer Semestre' WHEN 2 THEN 'Segundo Semestre' END as desc_semestre,
                                        CASE nov.NOVEDADFUEPAGADA WHEN 1 THEN 'Si' WHEN 0 THEN 'No' END as desc_fuepagada 
                                        from NovedadPrima nov
                                        JOIN NOMINA_EMPLEADO nom on nov.CODIGONOMINA = nom.CONSECUTIVO
                                        JOIN concepto_nomina tip on nov.CODIGOTIPONOVEDAD = tip.CONSECUTIVO
                                        JOIN EMPLEADOS emp ON nov.CODIGOEMPLEADO = emp.CONSECUTIVO
                                        JOIN V_PERSONA per on emp.COD_PERSONA = per.COD_PERSONA " + filtro + " ORDER BY nov.CONSECUTIVO desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            NovedadPrima entidad = new NovedadPrima();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["SEMESTRE"] != DBNull.Value) entidad.semestre = Convert.ToInt32(resultado["SEMESTRE"]);
                            if (resultado["CODIGOTIPONOVEDAD"] != DBNull.Value) entidad.codigotiponovedad = Convert.ToInt64(resultado["CODIGOTIPONOVEDAD"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["NOVEDADFUEPAGADA"] != DBNull.Value) entidad.novedadfuepagada = Convert.ToInt32(resultado["NOVEDADFUEPAGADA"]);
                            if (resultado["ANIO"] != DBNull.Value) entidad.anio = Convert.ToInt64(resultado["ANIO"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["desc_nomina"] != DBNull.Value) entidad.desc_nomina = Convert.ToString(resultado["desc_nomina"]);
                            if (resultado["desc_tipoNovedad"] != DBNull.Value) entidad.desc_tipoNovedad = Convert.ToString(resultado["desc_tipoNovedad"]);
                            if (resultado["desc_semestre"] != DBNull.Value) entidad.desc_semestre = Convert.ToString(resultado["desc_semestre"]);
                            if (resultado["desc_fuepagada"] != DBNull.Value) entidad.desc_fuepagada = Convert.ToString(resultado["desc_fuepagada"]);

                            lstNovedadPrima.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstNovedadPrima;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("NovedadPrimaData", "ListarNovedadPrima", ex);
                        return null;
                    }
                }
            }
        }


    }
}