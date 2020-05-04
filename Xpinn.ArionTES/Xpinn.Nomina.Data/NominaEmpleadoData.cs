using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Data
{
    public class NominaEmpleadoData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public NominaEmpleadoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public NominaEmpleado CrearNominaEmpleado(NominaEmpleado pNominaEmpleado, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pNominaEmpleado.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pNominaEmpleado.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pNominaEmpleado.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pcodigotipocontrato = cmdTransaccionFactory.CreateParameter();
                        pcodigotipocontrato.ParameterName = "p_codigotipocontrato";
                        if (pNominaEmpleado.codigotipocontrato == null)
                            pcodigotipocontrato.Value = DBNull.Value;
                        else
                            pcodigotipocontrato.Value = pNominaEmpleado.codigotipocontrato;
                        pcodigotipocontrato.Direction = ParameterDirection.Input;
                        pcodigotipocontrato.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigotipocontrato);

                        DbParameter ptiponomina = cmdTransaccionFactory.CreateParameter();
                        ptiponomina.ParameterName = "p_tiponomina";
                        if (pNominaEmpleado.tiponomina == null)
                            ptiponomina.Value = DBNull.Value;
                        else
                            ptiponomina.Value = pNominaEmpleado.tiponomina;
                        ptiponomina.Direction = ParameterDirection.Input;
                        ptiponomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptiponomina);

                        DbParameter pcodigooficina = cmdTransaccionFactory.CreateParameter();
                        pcodigooficina.ParameterName = "p_codigooficina";
                        if (pNominaEmpleado.codigooficina == null)
                            pcodigooficina.Value = DBNull.Value;
                        else
                            pcodigooficina.Value = pNominaEmpleado.codigooficina;
                        pcodigooficina.Direction = ParameterDirection.Input;
                        pcodigooficina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigooficina);


                        DbParameter p_permite_anticipos = cmdTransaccionFactory.CreateParameter();
                        p_permite_anticipos.ParameterName = "p_permite_anticipos";
                        p_permite_anticipos.Value = pNominaEmpleado.permite_anticipos;
                        p_permite_anticipos.Direction = ParameterDirection.Input;
                        p_permite_anticipos.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_permite_anticipos);


                        DbParameter p_porcentaje_anticipos = cmdTransaccionFactory.CreateParameter();
                        p_porcentaje_anticipos.ParameterName = "p_porcentaje_anticipos";
                        if (pNominaEmpleado.porcentaje_anticipos == null )
                            p_porcentaje_anticipos.Value = 0;
                        else
                            p_porcentaje_anticipos.Value = pNominaEmpleado.porcentaje_anticipos;

                        p_porcentaje_anticipos.Direction = ParameterDirection.Input;
                        p_porcentaje_anticipos.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_porcentaje_anticipos);



                        DbParameter p_permite_anticipo_sub_transporte = cmdTransaccionFactory.CreateParameter();
                        p_permite_anticipo_sub_transporte.ParameterName = "p_permite_anti_sub_transp";
                        p_permite_anticipo_sub_transporte.Value = pNominaEmpleado.permite_anticipos_sub_trans;
                        p_permite_anticipo_sub_transporte.Direction = ParameterDirection.Input;
                        p_permite_anticipo_sub_transporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_permite_anticipo_sub_transporte);


                        DbParameter p_porcentaje_anti_sub_transp = cmdTransaccionFactory.CreateParameter();
                        p_porcentaje_anti_sub_transp.ParameterName = "p_porcentaje_anti_sub_transp";
                        if (pNominaEmpleado.porcentaje_anticipos_sub_trans == null )
                            p_porcentaje_anti_sub_transp.Value = 0;
                        else
                            p_porcentaje_anti_sub_transp.Value = pNominaEmpleado.porcentaje_anticipos_sub_trans;

                        p_porcentaje_anti_sub_transp.Direction = ParameterDirection.Input;
                        p_porcentaje_anti_sub_transp.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_porcentaje_anti_sub_transp);



                        DbParameter p_periodicidad_anticipos = cmdTransaccionFactory.CreateParameter();
                        p_periodicidad_anticipos.ParameterName = "p_periodicidad_anticipos";
                        if (pNominaEmpleado.periodicidad_anticipos == null)
                            p_periodicidad_anticipos.Value = 0;
                        else
                            p_periodicidad_anticipos.Value = pNominaEmpleado.periodicidad_anticipos;

                        p_periodicidad_anticipos.Direction = ParameterDirection.Input;
                        p_periodicidad_anticipos.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_periodicidad_anticipos);








                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_NOMINA_EMP_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pNominaEmpleado.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt32(pconsecutivo.Value) : 0;

                        return pNominaEmpleado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("NominaEmpleadoData", "CrearNominaEmpleado", ex);
                        return null;
                    }
                }
            }
        }


        public NominaEmpleado ModificarNominaEmpleado(NominaEmpleado pNominaEmpleado, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pNominaEmpleado.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pNominaEmpleado.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pNominaEmpleado.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pcodigotipocontrato = cmdTransaccionFactory.CreateParameter();
                        pcodigotipocontrato.ParameterName = "p_codigotipocontrato";
                        if (pNominaEmpleado.codigotipocontrato == null)
                            pcodigotipocontrato.Value = DBNull.Value;
                        else
                            pcodigotipocontrato.Value = pNominaEmpleado.codigotipocontrato;
                        pcodigotipocontrato.Direction = ParameterDirection.Input;
                        pcodigotipocontrato.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigotipocontrato);

                        DbParameter ptiponomina = cmdTransaccionFactory.CreateParameter();
                        ptiponomina.ParameterName = "p_tiponomina";
                        if (pNominaEmpleado.tiponomina == null)
                            ptiponomina.Value = DBNull.Value;
                        else
                            ptiponomina.Value = pNominaEmpleado.tiponomina;
                        ptiponomina.Direction = ParameterDirection.Input;
                        ptiponomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptiponomina);

                        DbParameter pcodigooficina = cmdTransaccionFactory.CreateParameter();
                        pcodigooficina.ParameterName = "p_codigooficina";
                        if (pNominaEmpleado.codigooficina == null)
                            pcodigooficina.Value = DBNull.Value;
                        else
                            pcodigooficina.Value = pNominaEmpleado.codigooficina;
                        pcodigooficina.Direction = ParameterDirection.Input;
                        pcodigooficina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigooficina);

                        DbParameter p_permite_anticipos = cmdTransaccionFactory.CreateParameter();
                        p_permite_anticipos.ParameterName = "p_permite_anticipos";
                        p_permite_anticipos.Value = pNominaEmpleado.permite_anticipos;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        p_permite_anticipos.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_permite_anticipos);



                        DbParameter p_porcentaje_anticipos = cmdTransaccionFactory.CreateParameter();
                        p_porcentaje_anticipos.ParameterName = "p_porcentaje_anticipos";
                        if (pNominaEmpleado.porcentaje_anticipos == null)
                            p_porcentaje_anticipos.Value = 0;
                        else
                            p_porcentaje_anticipos.Value = pNominaEmpleado.porcentaje_anticipos;

                        p_porcentaje_anticipos.Direction = ParameterDirection.Input;
                        p_porcentaje_anticipos.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_porcentaje_anticipos);





                        DbParameter p_permite_anticipo_sub_transporte = cmdTransaccionFactory.CreateParameter();
                        p_permite_anticipo_sub_transporte.ParameterName = "p_permite_anti_sub_transp";
                        p_permite_anticipo_sub_transporte.Value = pNominaEmpleado.permite_anticipos_sub_trans;
                        p_permite_anticipo_sub_transporte.Direction = ParameterDirection.Input;
                        p_permite_anticipo_sub_transporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_permite_anticipo_sub_transporte);


                        DbParameter p_porcentaje_anti_sub_transp = cmdTransaccionFactory.CreateParameter();
                        p_porcentaje_anti_sub_transp.ParameterName = "p_porcentaje_anti_sub_transp";
                        if (pNominaEmpleado.porcentaje_anticipos_sub_trans == null)
                            p_porcentaje_anti_sub_transp.Value = 0;
                        else
                            p_porcentaje_anti_sub_transp.Value = pNominaEmpleado.porcentaje_anticipos_sub_trans;

                        p_porcentaje_anti_sub_transp.Direction = ParameterDirection.Input;
                        p_porcentaje_anti_sub_transp.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_porcentaje_anti_sub_transp);


                        DbParameter p_periodicidad_anticipos = cmdTransaccionFactory.CreateParameter();
                        p_periodicidad_anticipos.ParameterName = "p_periodicidad_anticipos";
                        if (pNominaEmpleado.periodicidad_anticipos == null)
                            p_periodicidad_anticipos.Value = 0;
                        else
                            p_periodicidad_anticipos.Value = pNominaEmpleado.periodicidad_anticipos;

                        p_periodicidad_anticipos.Direction = ParameterDirection.Input;
                        p_periodicidad_anticipos.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_periodicidad_anticipos);




                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_NOMINA_EMP_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pNominaEmpleado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("NominaEmpleadoData", "ModificarNominaEmpleado", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarNominaEmpleado(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        NominaEmpleado pNominaEmpleado = new NominaEmpleado();
                        pNominaEmpleado = ConsultarNominaEmpleado(pId, vUsuario);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pNominaEmpleado.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_NOMINA_EMP_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("NominaEmpleadoData", "EliminarNominaEmpleado", ex);
                    }
                }
            }
        }


        public NominaEmpleado ConsultarNominaEmpleado(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            NominaEmpleado entidad = new NominaEmpleado();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT NOM.*, OFI.COD_CIUDAD, OFI.DIRECCION
                                        FROM NOMINA_EMPLEADO NOM
                                        JOIN OFICINA OFI ON OFI.COD_OFICINA = NOM.CODIGOOFICINA
                                        WHERE NOM.CONSECUTIVO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt32(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CODIGOTIPOCONTRATO"] != DBNull.Value) entidad.codigotipocontrato = Convert.ToInt64(resultado["CODIGOTIPOCONTRATO"]);
                            if (resultado["TIPONOMINA"] != DBNull.Value) entidad.tiponomina = Convert.ToInt64(resultado["TIPONOMINA"]);
                            if (resultado["CODIGOOFICINA"] != DBNull.Value) entidad.codigooficina = Convert.ToInt64(resultado["CODIGOOFICINA"]);
                            if (resultado["COD_CIUDAD"] != DBNull.Value) entidad.codigociudad = Convert.ToInt64(resultado["COD_CIUDAD"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion_oficina = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["PERMITE_ANTICIPOS"] != DBNull.Value) entidad.permite_anticipos = Convert.ToInt32(resultado["PERMITE_ANTICIPOS"]);
                            if (resultado["PORCENTAJE_ANTICIPOS"] != DBNull.Value) entidad.porcentaje_anticipos = Convert.ToInt32(resultado["PORCENTAJE_ANTICIPOS"]);
                            if (resultado["PERMITE_ANTI_SUB_TRANSP"] != DBNull.Value) entidad.permite_anticipos_sub_trans = Convert.ToInt32(resultado["PERMITE_ANTI_SUB_TRANSP"]);
                            if (resultado["PORCENTAJE_ANTI_SUB_TRANSP"] != DBNull.Value) entidad.porcentaje_anticipos_sub_trans = Convert.ToInt32(resultado["PORCENTAJE_ANTI_SUB_TRANSP"]);
                            if (resultado["FECHA_ULT_LIQUIDACION"] != DBNull.Value) entidad.fecha_ult_liquidacion = Convert.ToDateTime(resultado["FECHA_ULT_LIQUIDACION"]);
                            if (resultado["COD_PERIODICIDAD_ANTI"] != DBNull.Value) entidad.periodicidad_anticipos = Convert.ToInt32(resultado["COD_PERIODICIDAD_ANTI"]);

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
                        BOExcepcion.Throw("NominaEmpleadoData", "ConsultarNominaEmpleado", ex);
                        return null;
                    }
                }
            }
        }


        public List<NominaEmpleado> ListarNominaEmpleado(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<NominaEmpleado> lstNominaEmpleado = new List<NominaEmpleado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT NOM.*, OFI.NOMBRE as desc_oficina, TIP.TIPO_CONTRATO as desc_tipo_contrato,
                                        CASE NOM.TIPONOMINA WHEN 1 THEN 'Mensual' WHEN 2 THEN 'Quincenal' WHEN 3 THEN '1er Periodo' WHEN 4 THEN '2do Periodo' WHEN 5 THEN '3er Periodo' WHEN 6 THEN '4to Periodo' WHEN 7 THEN 'Todos los Periodos' END as desc_tipo_nomina
                                        FROM NOMINA_EMPLEADO NOM
                                        JOIN OFICINA OFI ON OFI.COD_OFICINA = NOM.CODIGOOFICINA
                                        JOIN CONTRATACION TIP ON TIP.COD_CONTRATACION = NOM.CODIGOTIPOCONTRATO " + filtro + " ORDER BY NOM.CONSECUTIVO ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            NominaEmpleado entidad = new NominaEmpleado();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt32(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CODIGOTIPOCONTRATO"] != DBNull.Value) entidad.codigotipocontrato = Convert.ToInt64(resultado["CODIGOTIPOCONTRATO"]);
                         
                            if (resultado["TIPONOMINA"] != DBNull.Value) entidad.tiponomina = Convert.ToInt64(resultado["TIPONOMINA"]);
                            if (resultado["CODIGOOFICINA"] != DBNull.Value) entidad.codigooficina = Convert.ToInt64(resultado["CODIGOOFICINA"]);

                            if (resultado["desc_oficina"] != DBNull.Value) entidad.desc_oficina = Convert.ToString(resultado["desc_oficina"]);
                            if (resultado["desc_tipo_contrato"] != DBNull.Value) entidad.desc_tipo_contrato = Convert.ToString(resultado["desc_tipo_contrato"]);
                            if (resultado["desc_tipo_nomina"] != DBNull.Value) entidad.desc_tipo_nomina = Convert.ToString(resultado["desc_tipo_nomina"]);
                            if (resultado["PERMITE_ANTICIPOS"] != DBNull.Value) entidad.permite_anticipos = Convert.ToInt32(resultado["PERMITE_ANTICIPOS"]);
                            if (entidad.permite_anticipos == 0)
                            {
                                entidad.permite_anticipos_mostrar = "NO";
                            }
                            else
                                entidad.permite_anticipos_mostrar = "SI";

                            if (resultado["PORCENTAJE_ANTICIPOS"] != DBNull.Value) entidad.porcentaje_anticipos = Convert.ToInt32(resultado["PORCENTAJE_ANTICIPOS"]);
                            


                            if (resultado["PERMITE_ANTI_SUB_TRANSP"] != DBNull.Value) entidad.permite_anticipos_sub_trans = Convert.ToInt32(resultado["PERMITE_ANTI_SUB_TRANSP"]);
                            if (entidad.permite_anticipos_sub_trans == 0)
                            {
                                entidad.permite_anticipos_sub_mostrar = "NO";
                            }
                            else
                                entidad.permite_anticipos_sub_mostrar = "SI";

                            if (resultado["PORCENTAJE_ANTI_SUB_TRANSP"] != DBNull.Value) entidad.porcentaje_anticipos_sub_trans = Convert.ToInt32(resultado["PORCENTAJE_ANTI_SUB_TRANSP"]);


                            if (resultado["FECHA_ULT_LIQUIDACION"] != DBNull.Value) entidad.fecha_ult_liquidacion = Convert.ToDateTime(resultado["FECHA_ULT_LIQUIDACION"]);

                            if (resultado["COD_PERIODICIDAD_ANTI"] != DBNull.Value) entidad.periodicidad_anticipos = Convert.ToInt32(resultado["COD_PERIODICIDAD_ANTI"]);


                            lstNominaEmpleado.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstNominaEmpleado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("NominaEmpleadoData", "ListarNominaEmpleado", ex);
                        return null;
                    }
                }
            }
        }


    }
}