using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Data
{
    public class HorasExtrasEmpleadosData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public HorasExtrasEmpleadosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public HorasExtrasEmpleados CrearHorasExtrasEmpleados(HorasExtrasEmpleados pHorasExtrasEmpleados, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pHorasExtrasEmpleados.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pHorasExtrasEmpleados.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pcodigopersona = cmdTransaccionFactory.CreateParameter();
                        pcodigopersona.ParameterName = "p_codigopersona";
                        pcodigopersona.Value = pHorasExtrasEmpleados.codigopersona;
                        pcodigopersona.Direction = ParameterDirection.Input;
                        pcodigopersona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigopersona);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pHorasExtrasEmpleados.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pcantidadhoras = cmdTransaccionFactory.CreateParameter();
                        pcantidadhoras.ParameterName = "p_cantidadhoras";
                        pcantidadhoras.Value = pHorasExtrasEmpleados.cantidadhoras;
                        pcantidadhoras.Direction = ParameterDirection.Input;
                        pcantidadhoras.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcantidadhoras);

                        DbParameter pcodigoconceptohoras = cmdTransaccionFactory.CreateParameter();
                        pcodigoconceptohoras.ParameterName = "p_codigoconceptohoras";
                        pcodigoconceptohoras.Value = pHorasExtrasEmpleados.codigoconceptohoras;
                        pcodigoconceptohoras.Direction = ParameterDirection.Input;
                        pcodigoconceptohoras.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoconceptohoras);

                        DbParameter pcodigonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigonomina.ParameterName = "p_codigonomina";
                        pcodigonomina.Value = pHorasExtrasEmpleados.codigonomina;
                        pcodigonomina.Direction = ParameterDirection.Input;
                        pcodigonomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigonomina);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_HORASEXTRA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pHorasExtrasEmpleados.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pHorasExtrasEmpleados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HorasExtrasEmpleadosData", "CrearHorasExtrasEmpleados", ex);
                        return null;
                    }
                }
            }
        }


        public HorasExtrasEmpleados ModificarHorasExtrasEmpleados(HorasExtrasEmpleados pHorasExtrasEmpleados, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pHorasExtrasEmpleados.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pHorasExtrasEmpleados.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pcodigopersona = cmdTransaccionFactory.CreateParameter();
                        pcodigopersona.ParameterName = "p_codigopersona";
                        pcodigopersona.Value = pHorasExtrasEmpleados.codigopersona;
                        pcodigopersona.Direction = ParameterDirection.Input;
                        pcodigopersona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigopersona);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pHorasExtrasEmpleados.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pcantidadhoras = cmdTransaccionFactory.CreateParameter();
                        pcantidadhoras.ParameterName = "p_cantidadhoras";
                        pcantidadhoras.Value = pHorasExtrasEmpleados.cantidadhoras;
                        pcantidadhoras.Direction = ParameterDirection.Input;
                        pcantidadhoras.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcantidadhoras);

                        DbParameter pcodigoconceptohoras = cmdTransaccionFactory.CreateParameter();
                        pcodigoconceptohoras.ParameterName = "p_codigoconceptohoras";
                        pcodigoconceptohoras.Value = pHorasExtrasEmpleados.codigoconceptohoras;
                        pcodigoconceptohoras.Direction = ParameterDirection.Input;
                        pcodigoconceptohoras.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoconceptohoras);

                        DbParameter pcodigonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigonomina.ParameterName = "p_codigonomina";
                        pcodigonomina.Value = pHorasExtrasEmpleados.codigonomina;
                        pcodigonomina.Direction = ParameterDirection.Input;
                        pcodigonomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigonomina);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_HORASEXTRA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pHorasExtrasEmpleados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HorasExtrasEmpleadosData", "ModificarHorasExtrasEmpleados", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarHorasExtrasEmpleados(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        HorasExtrasEmpleados pHorasExtrasEmpleados = new HorasExtrasEmpleados();
                        pHorasExtrasEmpleados = ConsultarHorasExtrasEmpleados(pId, vUsuario);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pHorasExtrasEmpleados.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_HORASEXTRA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HorasExtrasEmpleadosData", "EliminarHorasExtrasEmpleados", ex);
                    }
                }
            }
        }


        public HorasExtrasEmpleados ConsultarHorasExtrasEmpleados(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            HorasExtrasEmpleados entidad = new HorasExtrasEmpleados();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT hor.*, per.nombre, per.identificacion, per.TIPO_IDENTIFICACION
                                        FROM HorasExtrasEmpleados hor
                                        JOIN EMPLEADOS emp on emp.consecutivo = hor.codigoempleado
                                        JOIN v_persona per on emp.COD_PERSONA = per.COD_PERSONA WHERE hor.CONSECUTIVO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGOPERSONA"] != DBNull.Value) entidad.codigopersona = Convert.ToInt64(resultado["CODIGOPERSONA"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["CANTIDADHORAS"] != DBNull.Value) entidad.cantidadhoras = Convert.ToDecimal(resultado["CANTIDADHORAS"]);
                            if (resultado["CODIGOCONCEPTOHORAS"] != DBNull.Value) entidad.codigoconceptohoras = Convert.ToInt64(resultado["CODIGOCONCEPTOHORAS"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);

                            if (resultado["nombre"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["nombre"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion_empleado = Convert.ToString(resultado["identificacion"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["HORASFUERONPAGADAS"] != DBNull.Value) entidad.pagadas = Convert.ToInt64(resultado["HORASFUERONPAGADAS"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HorasExtrasEmpleadosData", "ConsultarHorasExtrasEmpleados", ex);
                        return null;
                    }
                }
            }
        }


        public List<HorasExtrasEmpleados> ListarHorasExtrasEmpleados(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<HorasExtrasEmpleados> lstHorasExtrasEmpleados = new List<HorasExtrasEmpleados>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT hor.*, per.nombre, per.identificacion, con.DESCRIPCION as desc_concepto_hora, nomi.DESCRIPCION as desc_nomina
                                        FROM HorasExtrasEmpleados hor
                                        JOIN CONCEPTO_NOMINA con on con.CONSECUTIVO = hor.CODIGOCONCEPTOHORAS
                                        JOIN NOMINA_EMPLEADO nomi on nomi.CONSECUTIVO = hor.CODIGONOMINA
                                        JOIN EMPLEADOS emp on emp.consecutivo = hor.codigoempleado
                                        JOIN v_persona per on emp.COD_PERSONA = per.COD_PERSONA " + filtro + " ORDER BY hor.CONSECUTIVO desc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            HorasExtrasEmpleados entidad = new HorasExtrasEmpleados();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGOPERSONA"] != DBNull.Value) entidad.codigopersona = Convert.ToInt64(resultado["CODIGOPERSONA"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["CANTIDADHORAS"] != DBNull.Value) entidad.cantidadhoras = Convert.ToDecimal(resultado["CANTIDADHORAS"]);
                            if (resultado["CODIGOCONCEPTOHORAS"] != DBNull.Value) entidad.codigoconceptohoras = Convert.ToInt64(resultado["CODIGOCONCEPTOHORAS"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);

                            if (resultado["nombre"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["nombre"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion_empleado = Convert.ToString(resultado["identificacion"]);
                            if (resultado["desc_concepto_hora"] != DBNull.Value) entidad.desc_concepto_hora = Convert.ToString(resultado["desc_concepto_hora"]);
                            if (resultado["desc_nomina"] != DBNull.Value) entidad.desc_nomina = Convert.ToString(resultado["desc_nomina"]);
                            if (resultado["HORASFUERONPAGADAS"] != DBNull.Value) entidad.pagadas = Convert.ToInt64(resultado["HORASFUERONPAGADAS"]);


                            lstHorasExtrasEmpleados.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstHorasExtrasEmpleados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HorasExtrasEmpleadosData", "ListarHorasExtrasEmpleados", ex);
                        return null;
                    }
                }
            }
        }


    }
}