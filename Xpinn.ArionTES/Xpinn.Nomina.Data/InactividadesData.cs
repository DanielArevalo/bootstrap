using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Data
{
    public class InactividadesData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public InactividadesData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public Inactividades CrearInactividades(Inactividades pInactividades, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pInactividades.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pInactividades.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pcodigopersona = cmdTransaccionFactory.CreateParameter();
                        pcodigopersona.ParameterName = "p_codigopersona";
                        pcodigopersona.Value = pInactividades.codigopersona;
                        pcodigopersona.Direction = ParameterDirection.Input;
                        pcodigopersona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigopersona);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pInactividades.tipo == null)
                            ptipo.Value = DBNull.Value;
                        else
                            ptipo.Value = pInactividades.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pcod_concepto = cmdTransaccionFactory.CreateParameter();
                        pcod_concepto.ParameterName = "pcod_concepto";
                        if (pInactividades.cod_concepto == null)
                            pcod_concepto.Value = DBNull.Value;
                        else
                            pcod_concepto.Value = pInactividades.cod_concepto;
                        pcod_concepto.Direction = ParameterDirection.Input;
                        pcod_concepto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_concepto);

                        DbParameter pfechainicio = cmdTransaccionFactory.CreateParameter();
                        pfechainicio.ParameterName = "p_fechainicio";
                        if (pInactividades.fechainicio == null)
                            pfechainicio.Value = DBNull.Value;
                        else
                            pfechainicio.Value = pInactividades.fechainicio;
                        pfechainicio.Direction = ParameterDirection.Input;
                        pfechainicio.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfechainicio);

                        DbParameter pfechaterminacion = cmdTransaccionFactory.CreateParameter();
                        pfechaterminacion.ParameterName = "p_fechaterminacion";
                        if (pInactividades.fechaterminacion == null)
                            pfechaterminacion.Value = DBNull.Value;
                        else
                            pfechaterminacion.Value = pInactividades.fechaterminacion;
                        pfechaterminacion.Direction = ParameterDirection.Input;
                        pfechaterminacion.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfechaterminacion);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pInactividades.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pInactividades.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter premunerada = cmdTransaccionFactory.CreateParameter();
                        premunerada.ParameterName = "p_remunerada";
                        if (pInactividades.remunerada == null)
                            premunerada.Value = DBNull.Value;
                        else
                            premunerada.Value = pInactividades.remunerada;
                        premunerada.Direction = ParameterDirection.Input;
                        premunerada.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(premunerada);

                        DbParameter pCodigoNomina = cmdTransaccionFactory.CreateParameter();
                        pCodigoNomina.ParameterName = "pCodigoNomina";
                        if (pInactividades.codigo_tipo_nomina == null)
                            pCodigoNomina.Value = DBNull.Value;
                        else
                            pCodigoNomina.Value = pInactividades.codigo_tipo_nomina;
                        pCodigoNomina.Direction = ParameterDirection.Input;
                        pCodigoNomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCodigoNomina);

                        DbParameter pCodigoTipoContrato = cmdTransaccionFactory.CreateParameter();
                        pCodigoTipoContrato.ParameterName = "pCodigoTipoContrato";
                        if (pInactividades.codigotipocontrato == null)
                            pCodigoTipoContrato.Value = DBNull.Value;
                        else
                            pCodigoTipoContrato.Value = pInactividades.codigotipocontrato;
                        pCodigoTipoContrato.Direction = ParameterDirection.Input;
                        pCodigoTipoContrato.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCodigoTipoContrato);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_INACTIVIDA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pInactividades.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pInactividades;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InactividadesData", "CrearInactividades", ex);
                        return null;
                    }
                }
            }
        }


        public Inactividades ModificarInactividades(Inactividades pInactividades, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pInactividades.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pInactividades.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pcodigopersona = cmdTransaccionFactory.CreateParameter();
                        pcodigopersona.ParameterName = "p_codigopersona";
                        pcodigopersona.Value = pInactividades.codigopersona;
                        pcodigopersona.Direction = ParameterDirection.Input;
                        pcodigopersona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigopersona);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pInactividades.tipo == null)
                            ptipo.Value = DBNull.Value;
                        else
                            ptipo.Value = pInactividades.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                      /*DbParameter pclase = cmdTransaccionFactory.CreateParameter();
                        pclase.ParameterName = "p_clase";
                        if (pInactividades.clase == null)
                            pclase.Value = DBNull.Value;
                        else
                            pclase.Value = pInactividades.clase;
                        pclase.Direction = ParameterDirection.Input;
                        pclase.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pclase);
                        *
                        */
                        DbParameter pcod_concepto = cmdTransaccionFactory.CreateParameter();
                        pcod_concepto.ParameterName = "pcod_concepto";
                        if (pInactividades.cod_concepto == null)
                            pcod_concepto.Value = DBNull.Value;
                        else
                            pcod_concepto.Value = pInactividades.cod_concepto;
                        pcod_concepto.Direction = ParameterDirection.Input;
                        pcod_concepto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_concepto);


                        DbParameter pfechainicio = cmdTransaccionFactory.CreateParameter();
                        pfechainicio.ParameterName = "p_fechainicio";
                        if (pInactividades.fechainicio == null)
                            pfechainicio.Value = DBNull.Value;
                        else
                            pfechainicio.Value = pInactividades.fechainicio;
                        pfechainicio.Direction = ParameterDirection.Input;
                        pfechainicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechainicio);

                        DbParameter pfechaterminacion = cmdTransaccionFactory.CreateParameter();
                        pfechaterminacion.ParameterName = "p_fechaterminacion";
                        if (pInactividades.fechaterminacion == null)
                            pfechaterminacion.Value = DBNull.Value;
                        else
                            pfechaterminacion.Value = pInactividades.fechaterminacion;
                        pfechaterminacion.Direction = ParameterDirection.Input;
                        pfechaterminacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaterminacion);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pInactividades.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pInactividades.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter premunerada = cmdTransaccionFactory.CreateParameter();
                        premunerada.ParameterName = "p_remunerada";
                        if (pInactividades.remunerada == null)
                            premunerada.Value = DBNull.Value;
                        else
                            premunerada.Value = pInactividades.remunerada;
                        premunerada.Direction = ParameterDirection.Input;
                        premunerada.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(premunerada);

                        DbParameter pCodigoNomina = cmdTransaccionFactory.CreateParameter();
                        pCodigoNomina.ParameterName = "pCodigoNomina";
                        if (pInactividades.codigo_tipo_nomina == null)
                            pCodigoNomina.Value = DBNull.Value;
                        else
                            pCodigoNomina.Value = pInactividades.codigo_tipo_nomina;
                        pCodigoNomina.Direction = ParameterDirection.Input;
                        pCodigoNomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCodigoNomina);

                        DbParameter pCodigoTipoContrato = cmdTransaccionFactory.CreateParameter();
                        pCodigoTipoContrato.ParameterName = "pCodigoTipoContrato";
                        if (pInactividades.codigotipocontrato == null)
                            pCodigoTipoContrato.Value = DBNull.Value;
                        else
                            pCodigoTipoContrato.Value = pInactividades.codigotipocontrato;
                        pCodigoTipoContrato.Direction = ParameterDirection.Input;
                        pCodigoTipoContrato.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCodigoTipoContrato);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_INACTIVIDA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pInactividades;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InactividadesData", "ModificarInactividades", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarInactividades(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Inactividades pInactividades = new Inactividades();
                        pInactividades = ConsultarInactividades(pId, vUsuario);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pInactividades.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_INACTIVIDA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InactividadesData", "EliminarInactividades", ex);
                    }
                }
            }
        }


        public Inactividades ConsultarInactividades(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Inactividades entidad = new Inactividades();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT ina.*,cn.consecutivo as Cod_concepto, per.NOMBRE, per.identificacion, per.tipo_identificacion,per.cod_persona 
                                        FROM Inactividades ina
                                        JOIN v_persona per on per.cod_persona = ina.CodigoPersona
                                        JOIN empleados emp on emp.consecutivo = ina.CODIGOEMPLEADO
                                        JOIN CONCEPTO_NOMINA cn on cn.consecutivo=ina.CODIGOCONCEPTO
                                        WHERE ina.CONSECUTIVO = " + pId.ToString();
                      
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
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["CODIGOCONCEPTO"] != DBNull.Value) entidad.cod_concepto = Convert.ToInt32(resultado["COD_CONCEPTO"]);
                            if (resultado["FECHAINICIO"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["FECHAINICIO"]);
                            if (resultado["FECHATERMINACION"] != DBNull.Value) entidad.fechaterminacion = Convert.ToDateTime(resultado["FECHATERMINACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["REMUNERADA"] != DBNull.Value) entidad.remunerada = Convert.ToInt32(resultado["REMUNERADA"]);
                            if (resultado["CodigoTipoContrato"] != DBNull.Value) entidad.codigotipocontrato = Convert.ToInt64(resultado["CodigoTipoContrato"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["tipo_identificacion"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["tipo_identificacion"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["CodigoNomina"] != DBNull.Value) entidad.codigo_tipo_nomina = Convert.ToInt64(resultado["CodigoNomina"]);
                            if (resultado["APLICADA"] != DBNull.Value) entidad.aplicada = Convert.ToInt32(resultado["APLICADA"]);

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
                        BOExcepcion.Throw("InactividadesData", "ConsultarInactividades", ex);
                        return null;
                    }
                }
            }
        }


        public List<Inactividades> ListarInactividades(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Inactividades> lstInactividades = new List<Inactividades>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT ina.*, cn.descripcion as desc_clase,per.NOMBRE, per.identificacion, nomi.DESCRIPCION as desc_nomina, tip.DESCRIPCION as desc_tipo_contrato,
                                        CASE ina.REMUNERADA WHEN 1 THEN 'Si' WHEN 0 THEN 'NO' END as desc_inactividad_remunerada,
                                        --CASE ina.CLASE WHEN 1 THEN 'Licencia' WHEN 2 THEN 'Inactividad EPS' WHEN 3 THEN 'Accidente de Trabajo' WHEN 4 THEN 'Maternidad' WHEN 5 THEN 'Suspencion' WHEN 6 THEN 'Enfermadad Laboral' WHEN 7 THEN 'Permiso' END as desc_clase,
                                        CASE ina.tipo WHEN 1 THEN 'Paga dos dias iniciales' WHEN 2 THEN 'Utiliza porcentaje dos dias' WHEN 3 THEN 'Utiliza porcentaje el resto de dias' WHEN 4 THEN 'Porcentaje 50%' WHEN 5 THEN 'Prorroga' WHEN 6 THEN '> mas 180 dias' END as desc_tipos
                                        FROM Inactividades ina
                                        JOIN v_persona per on per.cod_persona = ina.CodigoPersona
                                        JOIN empleados emp on emp.consecutivo = ina.CODIGOEMPLEADO
                                        LEFT JOIN NOMINA_EMPLEADO nomi on nomi.consecutivo = ina.CodigoNomina
                                        JOIN CONCEPTO_NOMINA cn on cn.consecutivo=ina.CODIGOCONCEPTO
                                        LEFT JOIN TIPOCONTRATO tip on tip.CODTIPOCONTRATO = ina.CodigoTipoContrato 
                                        " + filtro + " ORDER BY ina.CONSECUTIVO desc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Inactividades entidad = new Inactividades();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGOPERSONA"] != DBNull.Value) entidad.codigopersona = Convert.ToInt64(resultado["CODIGOPERSONA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            // if (resultado["CLASE"] != DBNull.Value) entidad.clase = Convert.ToInt32(resultado["CLASE"]);
                            if (resultado["CODIGOCONCEPTO"] != DBNull.Value) entidad.cod_concepto = Convert.ToInt32(resultado["CODIGOCONCEPTO"]);
                            if (resultado["FECHAINICIO"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["FECHAINICIO"]);
                            if (resultado["FECHATERMINACION"] != DBNull.Value) entidad.fechaterminacion = Convert.ToDateTime(resultado["FECHATERMINACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["REMUNERADA"] != DBNull.Value) entidad.remunerada = Convert.ToInt32(resultado["REMUNERADA"]);
                            if (resultado["CodigoTipoContrato"] != DBNull.Value) entidad.codigotipocontrato = Convert.ToInt64(resultado["CodigoTipoContrato"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["CodigoNomina"] != DBNull.Value) entidad.codigo_tipo_nomina = Convert.ToInt64(resultado["CodigoNomina"]);
                            if (resultado["desc_nomina"] != DBNull.Value) entidad.desc_nomina = Convert.ToString(resultado["desc_nomina"]);
                            if (resultado["desc_tipo_contrato"] != DBNull.Value) entidad.desc_contrato = Convert.ToString(resultado["desc_tipo_contrato"]);
                            if (resultado["desc_inactividad_remunerada"] != DBNull.Value) entidad.desc_inactividad_remunerada = Convert.ToString(resultado["desc_inactividad_remunerada"]);
                            if (resultado["desc_clase"] != DBNull.Value) entidad.desc_clase = Convert.ToString(resultado["desc_clase"]);
                            if (resultado["desc_tipos"] != DBNull.Value) entidad.desc_tipo = Convert.ToString(resultado["desc_tipos"]);
                            if (resultado["APLICADA"] != DBNull.Value) entidad.aplicada = Convert.ToInt32(resultado["APLICADA"]);

                            lstInactividades.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstInactividades;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InactividadesData", "ListarInactividades", ex);
                        return null;
                    }
                }
            }
        }


    }
}