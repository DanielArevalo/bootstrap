using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Data
{
    public class AumentoSueldoData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public AumentoSueldoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public AumentoSueldo CrearAumentoSueldo(AumentoSueldo pAumentoSueldo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pAumentoSueldo.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigopersona = cmdTransaccionFactory.CreateParameter();
                        pcodigopersona.ParameterName = "p_codigopersona";
                        pcodigopersona.Value = pAumentoSueldo.codigopersona;
                        pcodigopersona.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcodigopersona);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pAumentoSueldo.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pnuevosueldo = cmdTransaccionFactory.CreateParameter();
                        pnuevosueldo.ParameterName = "p_nuevosueldo";
                        if (pAumentoSueldo.nuevosueldo == null)
                            pnuevosueldo.Value = DBNull.Value;
                        else
                            pnuevosueldo.Value = pAumentoSueldo.nuevosueldo;
                        pnuevosueldo.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pnuevosueldo);

                        DbParameter pvalorparaaumentar = cmdTransaccionFactory.CreateParameter();
                        pvalorparaaumentar.ParameterName = "p_valorparaaumentar";
                        if (pAumentoSueldo.valorparaaumentar == null)
                            pvalorparaaumentar.Value = DBNull.Value;
                        else
                            pvalorparaaumentar.Value = pAumentoSueldo.valorparaaumentar;
                        pvalorparaaumentar.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pvalorparaaumentar);

                        DbParameter pporcentajeaumentar = cmdTransaccionFactory.CreateParameter();
                        pporcentajeaumentar.ParameterName = "p_porcentajeaumentar";
                        if (pAumentoSueldo.porcentajeaumentar == null)
                            pporcentajeaumentar.Value = DBNull.Value;
                        else
                            pporcentajeaumentar.Value = pAumentoSueldo.porcentajeaumentar;
                        pporcentajeaumentar.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pporcentajeaumentar);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        if (pAumentoSueldo.fecha == null)
                            pfecha.Value = DBNull.Value;
                        else
                            pfecha.Value = pAumentoSueldo.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter p_sueldoanterior = cmdTransaccionFactory.CreateParameter();
                        p_sueldoanterior.ParameterName = "p_sueldoanterior";
                        if (pAumentoSueldo.sueldoanterior == null)
                            p_sueldoanterior.Value = DBNull.Value;
                        else
                            p_sueldoanterior.Value = pAumentoSueldo.sueldoanterior;
                        p_sueldoanterior.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_sueldoanterior);

                        DbParameter p_codigonomina = cmdTransaccionFactory.CreateParameter();
                        p_codigonomina.ParameterName = "p_codigonomina";
                        p_codigonomina.Value = pAumentoSueldo.codigonomina;
                        p_codigonomina.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_codigonomina);

                        DbParameter p_codigotipocontrato = cmdTransaccionFactory.CreateParameter();
                        p_codigotipocontrato.ParameterName = "p_codigotipocontrato";
                        p_codigotipocontrato.Value = pAumentoSueldo.codigotipocontrato;
                        p_codigotipocontrato.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_codigotipocontrato);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_AUMENTOSUE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pAumentoSueldo.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pAumentoSueldo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AumentoSueldoData", "CrearAumentoSueldo", ex);
                        return null;
                    }
                }
            }
        }

        public AumentoSueldo CrearAumentoSueldomasivo(AumentoSueldo pAumentoSueldo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pAumentoSueldo.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigopersona = cmdTransaccionFactory.CreateParameter();
                        pcodigopersona.ParameterName = "p_codigopersona";
                        pcodigopersona.Value = pAumentoSueldo.codigopersona;
                        pcodigopersona.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcodigopersona);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pAumentoSueldo.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pnuevosueldo = cmdTransaccionFactory.CreateParameter();
                        pnuevosueldo.ParameterName = "p_nuevosueldo";
                        if (pAumentoSueldo.nuevosueldo == null)
                            pnuevosueldo.Value = DBNull.Value;
                        else
                            pnuevosueldo.Value = pAumentoSueldo.nuevosueldo;
                        pnuevosueldo.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pnuevosueldo);

                        DbParameter pvalorparaaumentar = cmdTransaccionFactory.CreateParameter();
                        pvalorparaaumentar.ParameterName = "p_valorparaaumentar";
                        if (pAumentoSueldo.valorparaaumentar == null)
                            pvalorparaaumentar.Value = DBNull.Value;
                        else
                            pvalorparaaumentar.Value = pAumentoSueldo.valorparaaumentar;
                        pvalorparaaumentar.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pvalorparaaumentar);

                        DbParameter pporcentajeaumentar = cmdTransaccionFactory.CreateParameter();
                        pporcentajeaumentar.ParameterName = "p_porcentajeaumentar";
                        if (pAumentoSueldo.porcentajeaumentar == null)
                            pporcentajeaumentar.Value = DBNull.Value;
                        else
                            pporcentajeaumentar.Value = pAumentoSueldo.porcentajeaumentar;
                        pporcentajeaumentar.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pporcentajeaumentar);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        if (pAumentoSueldo.fecha == null)
                            pfecha.Value = DBNull.Value;
                        else
                            pfecha.Value = pAumentoSueldo.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter p_sueldoanterior = cmdTransaccionFactory.CreateParameter();
                        p_sueldoanterior.ParameterName = "p_sueldoanterior";
                        if (pAumentoSueldo.sueldoanterior == null)
                            p_sueldoanterior.Value = DBNull.Value;
                        else
                            p_sueldoanterior.Value = pAumentoSueldo.sueldoanterior;
                        p_sueldoanterior.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_sueldoanterior);

                        DbParameter p_codigonomina = cmdTransaccionFactory.CreateParameter();
                        p_codigonomina.ParameterName = "p_codigonomina";
                        p_codigonomina.Value = pAumentoSueldo.codigonomina;
                        p_codigonomina.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_codigonomina);

                        DbParameter p_codigotipocontrato = cmdTransaccionFactory.CreateParameter();
                        p_codigotipocontrato.ParameterName = "p_codigotipocontrato";
                        p_codigotipocontrato.Value = pAumentoSueldo.codigotipocontrato;
                        p_codigotipocontrato.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_codigotipocontrato);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_AUMENTOSUE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pAumentoSueldo.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pAumentoSueldo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AumentoSueldoData", "CrearAumentoSueldo", ex);
                        return null;
                    }
                }
            }
        }

        public AumentoSueldo ModificarAumentoSueldo(AumentoSueldo pAumentoSueldo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pAumentoSueldo.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigopersona = cmdTransaccionFactory.CreateParameter();
                        pcodigopersona.ParameterName = "p_codigopersona";
                        pcodigopersona.Value = pAumentoSueldo.codigopersona;
                        pcodigopersona.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcodigopersona);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pAumentoSueldo.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pnuevosueldo = cmdTransaccionFactory.CreateParameter();
                        pnuevosueldo.ParameterName = "p_nuevosueldo";
                        if (pAumentoSueldo.nuevosueldo == null)
                            pnuevosueldo.Value = DBNull.Value;
                        else
                            pnuevosueldo.Value = pAumentoSueldo.nuevosueldo;
                        pnuevosueldo.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pnuevosueldo);

                        DbParameter pvalorparaaumentar = cmdTransaccionFactory.CreateParameter();
                        pvalorparaaumentar.ParameterName = "p_valorparaaumentar";
                        if (pAumentoSueldo.valorparaaumentar == null)
                            pvalorparaaumentar.Value = DBNull.Value;
                        else
                            pvalorparaaumentar.Value = pAumentoSueldo.valorparaaumentar;
                        pvalorparaaumentar.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pvalorparaaumentar);

                        DbParameter pporcentajeaumentar = cmdTransaccionFactory.CreateParameter();
                        pporcentajeaumentar.ParameterName = "p_porcentajeaumentar";
                        if (pAumentoSueldo.porcentajeaumentar == null)
                            pporcentajeaumentar.Value = DBNull.Value;
                        else
                            pporcentajeaumentar.Value = pAumentoSueldo.porcentajeaumentar;
                        pporcentajeaumentar.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pporcentajeaumentar);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        if (pAumentoSueldo.fecha == null)
                            pfecha.Value = DBNull.Value;
                        else
                            pfecha.Value = pAumentoSueldo.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter p_sueldoanterior = cmdTransaccionFactory.CreateParameter();
                        p_sueldoanterior.ParameterName = "p_sueldoanterior";
                        if (pAumentoSueldo.sueldoanterior == null)
                            p_sueldoanterior.Value = DBNull.Value;
                        else
                            p_sueldoanterior.Value = pAumentoSueldo.sueldoanterior;
                        p_sueldoanterior.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_sueldoanterior);

                        DbParameter p_codigonomina = cmdTransaccionFactory.CreateParameter();
                        p_codigonomina.ParameterName = "p_codigonomina";
                        p_codigonomina.Value = pAumentoSueldo.codigonomina;
                        p_codigonomina.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_codigonomina);

                        DbParameter p_codigotipocontrato = cmdTransaccionFactory.CreateParameter();
                        p_codigotipocontrato.ParameterName = "p_codigotipocontrato";
                        p_codigotipocontrato.Value = pAumentoSueldo.codigotipocontrato;
                        p_codigotipocontrato.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_codigotipocontrato);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_AUMENTOSUE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pAumentoSueldo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AumentoSueldoData", "ModificarAumentoSueldo", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarAumentoSueldo(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        AumentoSueldo pAumentoSueldo = new AumentoSueldo();
                        pAumentoSueldo = ConsultarAumentoSueldo(pId, vUsuario);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pAumentoSueldo.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_AUMENTOSUE_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AumentoSueldoData", "EliminarAumentoSueldo", ex);
                    }
                }
            }
        }


        public AumentoSueldo ConsultarAumentoSueldo(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            AumentoSueldo entidad = new AumentoSueldo();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT aum.*, per.IDENTIFICACION, per.TIPO_IDENTIFICACION, per.NOMBRE
                                        FROM AumentoSueldo aum 
                                        JOIN v_persona per on aum.codigopersona = per.cod_persona
                                        WHERE aum.CONSECUTIVO = " + pId.ToString();

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
                            if (resultado["NUEVOSUELDO"] != DBNull.Value) entidad.nuevosueldo = Convert.ToDecimal(resultado["NUEVOSUELDO"]);
                            if (resultado["SueldoAnterior"] != DBNull.Value) entidad.sueldoanterior = Convert.ToDecimal(resultado["SueldoAnterior"]);
                            if (resultado["VALORPARAAUMENTAR"] != DBNull.Value) entidad.valorparaaumentar = Convert.ToDecimal(resultado["VALORPARAAUMENTAR"]);
                            if (resultado["PORCENTAJEAUMENTAR"] != DBNull.Value) entidad.porcentajeaumentar = Convert.ToDecimal(resultado["PORCENTAJEAUMENTAR"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
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
                        BOExcepcion.Throw("AumentoSueldoData", "ConsultarAumentoSueldo", ex);
                        return null;
                    }
                }
            }
        }


        public AumentoSueldo ConsultarAumentoSueldoXEmpleado(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            AumentoSueldo entidad = new AumentoSueldo();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT aum.*, per.IDENTIFICACION, per.TIPO_IDENTIFICACION, per.NOMBRE,i.salario as sueldo
                                        FROM INGRESOPERSONAL i
                                        JOIN v_persona per on i.codigopersona=per.cod_persona
                                        left join AumentoSueldo aum  on aum.codigopersona = per.cod_persona
                                        WHERE  per.COD_PERSONA = " + pId.ToString();

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
                            if (resultado["NUEVOSUELDO"] != DBNull.Value) entidad.nuevosueldo = Convert.ToDecimal(resultado["NUEVOSUELDO"]);
                            if (resultado["SueldoAnterior"] != DBNull.Value) entidad.sueldoanterior = Convert.ToDecimal(resultado["SueldoAnterior"]);
                            if (resultado["VALORPARAAUMENTAR"] != DBNull.Value) entidad.valorparaaumentar = Convert.ToDecimal(resultado["VALORPARAAUMENTAR"]);
                            if (resultado["PORCENTAJEAUMENTAR"] != DBNull.Value) entidad.porcentajeaumentar = Convert.ToDecimal(resultado["PORCENTAJEAUMENTAR"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["tipo_identificacion"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["tipo_identificacion"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["sueldo"] != DBNull.Value) entidad.sueldo = Convert.ToDecimal(resultado["sueldo"]);

                        }


                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AumentoSueldoData", "ConsultarAumentoSueldo", ex);
                        return null;
                    }
                }
            }
        }

        public List<AumentoSueldo> ListarAumentoSueldo(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<AumentoSueldo> lstAumentoSueldo = new List<AumentoSueldo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT aum.*, per.identificacion, per.nombre, emp.consecutivo as consecutivo_empleada
                                        FROM AumentoSueldo aum
                                        JOIN v_persona per on aum.CODIGOPERSONA = per.COD_PERSONA
                                        JOIN EMPLEADOS emp on emp.COD_PERSONA = per.COD_PERSONA  and emp.consecutivo=aum.CODIGOEMPLEADO " + filtro + " ORDER BY aum.CONSECUTIVO desc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AumentoSueldo entidad = new AumentoSueldo();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["consecutivo_empleada"] != DBNull.Value) entidad.consecutivo_empleado = Convert.ToInt64(resultado["consecutivo_empleada"]);
                            if (resultado["CODIGOPERSONA"] != DBNull.Value) entidad.codigopersona = Convert.ToInt64(resultado["CODIGOPERSONA"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["NUEVOSUELDO"] != DBNull.Value) entidad.nuevosueldo = Convert.ToDecimal(resultado["NUEVOSUELDO"]);
                            if (resultado["SueldoAnterior"] != DBNull.Value) entidad.sueldoanterior = Convert.ToDecimal(resultado["SueldoAnterior"]);
                            if (resultado["VALORPARAAUMENTAR"] != DBNull.Value) entidad.valorparaaumentar = Convert.ToDecimal(resultado["VALORPARAAUMENTAR"]);
                            if (resultado["PORCENTAJEAUMENTAR"] != DBNull.Value) entidad.porcentajeaumentar = Convert.ToDecimal(resultado["PORCENTAJEAUMENTAR"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);

                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["nombre"]);

                            lstAumentoSueldo.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAumentoSueldo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AumentoSueldoData", "ListarAumentoSueldo", ex);
                        return null;
                    }
                }
            }
        }


    }
}