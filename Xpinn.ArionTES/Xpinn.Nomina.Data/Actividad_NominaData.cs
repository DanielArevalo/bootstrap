using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Data
{
    public class Actividad_NominaNominaData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public Actividad_NominaNominaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public Actividad_Nomina CrearActividada_NominaEntities(Actividad_Nomina pActividada_NominaEntities, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pActividada_NominaEntities.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pnombre_actividad = cmdTransaccionFactory.CreateParameter();
                        pnombre_actividad.ParameterName = "p_nombre_actividad";
                        if (pActividada_NominaEntities.nombre_actividad == null)
                            pnombre_actividad.Value = DBNull.Value;
                        else
                            pnombre_actividad.Value = pActividada_NominaEntities.nombre_actividad;
                        pnombre_actividad.Direction = ParameterDirection.Input;
                        pnombre_actividad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre_actividad);

                        DbParameter pobjetivo = cmdTransaccionFactory.CreateParameter();
                        pobjetivo.ParameterName = "p_objetivo";
                        if (pActividada_NominaEntities.objetivo == null)
                            pobjetivo.Value = DBNull.Value;
                        else
                            pobjetivo.Value = pActividada_NominaEntities.objetivo;
                        pobjetivo.Direction = ParameterDirection.Input;
                        pobjetivo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobjetivo);

                        DbParameter pfecha_inicio = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicio.ParameterName = "p_fecha_inicio";
                        if (pActividada_NominaEntities.fecha_inicio == null)
                            pfecha_inicio.Value = DBNull.Value;
                        else
                            pfecha_inicio.Value = pActividada_NominaEntities.fecha_inicio;
                        pfecha_inicio.Direction = ParameterDirection.Input;
                        pfecha_inicio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicio);

                        DbParameter pfecha_terminacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_terminacion.ParameterName = "p_fecha_terminacion";
                        if (pActividada_NominaEntities.fecha_terminacion == null)
                            pfecha_terminacion.Value = DBNull.Value;
                        else
                            pfecha_terminacion.Value = pActividada_NominaEntities.fecha_terminacion;
                        pfecha_terminacion.Direction = ParameterDirection.Input;
                        pfecha_terminacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pfecha_terminacion);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pActividada_NominaEntities.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pActividada_NominaEntities.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pcentro_costo = cmdTransaccionFactory.CreateParameter();
                        pcentro_costo.ParameterName = "p_centro_costo";
                        if (pActividada_NominaEntities.centro_costo == null)
                            pcentro_costo.Value = DBNull.Value;
                        else
                            pcentro_costo.Value = pActividada_NominaEntities.centro_costo;
                        pcentro_costo.Direction = ParameterDirection.Input;
                        pcentro_costo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcentro_costo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_ACTIVIDAD__CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pActividada_NominaEntities;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Actividada_NominaEntitiesData", "CrearActividada_NominaEntities", ex);
                        return null;
                    }
                }
            }
        }


        public Actividad_Nomina ModificarActividada_NominaEntities(Actividad_Nomina pActividada_NominaEntities, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pActividada_NominaEntities.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pnombre_actividad = cmdTransaccionFactory.CreateParameter();
                        pnombre_actividad.ParameterName = "p_nombre_actividad";
                        if (pActividada_NominaEntities.nombre_actividad == null)
                            pnombre_actividad.Value = DBNull.Value;
                        else
                            pnombre_actividad.Value = pActividada_NominaEntities.nombre_actividad;
                        pnombre_actividad.Direction = ParameterDirection.Input;
                        pnombre_actividad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre_actividad);

                        DbParameter pobjetivo = cmdTransaccionFactory.CreateParameter();
                        pobjetivo.ParameterName = "p_objetivo";
                        if (pActividada_NominaEntities.objetivo == null)
                            pobjetivo.Value = DBNull.Value;
                        else
                            pobjetivo.Value = pActividada_NominaEntities.objetivo;
                        pobjetivo.Direction = ParameterDirection.Input;
                        pobjetivo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobjetivo);

                        DbParameter pfecha_inicio = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicio.ParameterName = "p_fecha_inicio";
                        if (pActividada_NominaEntities.fecha_inicio == null)
                            pfecha_inicio.Value = DBNull.Value;
                        else
                            pfecha_inicio.Value = pActividada_NominaEntities.fecha_inicio;
                        pfecha_inicio.Direction = ParameterDirection.Input;
                        pfecha_inicio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicio);

                        DbParameter pfecha_terminacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_terminacion.ParameterName = "p_fecha_terminacion";
                        if (pActividada_NominaEntities.fecha_terminacion == null)
                            pfecha_terminacion.Value = DBNull.Value;
                        else
                            pfecha_terminacion.Value = pActividada_NominaEntities.fecha_terminacion;
                        pfecha_terminacion.Direction = ParameterDirection.Input;
                        pfecha_terminacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pfecha_terminacion);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pActividada_NominaEntities.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pActividada_NominaEntities.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pcentro_costo = cmdTransaccionFactory.CreateParameter();
                        pcentro_costo.ParameterName = "p_centro_costo";
                        if (pActividada_NominaEntities.centro_costo == null)
                            pcentro_costo.Value = DBNull.Value;
                        else
                            pcentro_costo.Value = pActividada_NominaEntities.centro_costo;
                        pcentro_costo.Direction = ParameterDirection.Input;
                        pcentro_costo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcentro_costo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_ACTIVIDAD__MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pActividada_NominaEntities;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Actividada_NominaEntitiesData", "ModificarActividada_NominaEntities", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarActividada_NominaEntities(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Actividad_Nomina pActividada_NominaEntities = new Actividad_Nomina();
                        pActividada_NominaEntities = ConsultarActividada_NominaEntities(pId, vUsuario);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pActividada_NominaEntities.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_ACTIVIDAD__ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Actividada_NominaEntitiesData", "EliminarActividada_NominaEntities", ex);
                    }
                }
            }
        }


        public Actividad_Nomina ConsultarActividada_NominaEntities(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Actividad_Nomina entidad = new Actividad_Nomina();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Actividad_Nomina WHERE CONSECUTIVO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["NOMBRE_ACTIVIDAD"] != DBNull.Value) entidad.nombre_actividad = Convert.ToString(resultado["NOMBRE_ACTIVIDAD"]);
                            if (resultado["OBJETIVO"] != DBNull.Value) entidad.objetivo = Convert.ToString(resultado["OBJETIVO"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToString(resultado["FECHA_INICIO"]);
                            if (resultado["FECHA_TERMINACION"] != DBNull.Value) entidad.fecha_terminacion = Convert.ToString(resultado["FECHA_TERMINACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToString(resultado["COD_PERSONA"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToString(resultado["CENTRO_COSTO"]);
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
                        BOExcepcion.Throw("Actividada_NominaEntitiesData", "ConsultarActividada_NominaEntities", ex);
                        return null;
                    }
                }
            }
        }


        public List<Actividad_Nomina> ListarActividada_NominaEntities(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Actividad_Nomina> lstActividada_NominaEntities = new List<Actividad_Nomina>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT Actividad_Nomina.consecutivo,
                            Actividad_Nomina.nombre_actividad,
                            Actividad_Nomina.objetivo,
                            Actividad_Nomina.fecha_inicio,
                            Actividad_Nomina.fecha_terminacion,
                            Actividad_Nomina.cod_persona,
                            Actividad_Nomina.centro_costo
                        FROM Actividad_Nomina " + filtro.ToString();




                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            
                            Actividad_Nomina entidad = new Actividad_Nomina();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["NOMBRE_ACTIVIDAD"] != DBNull.Value) entidad.nombre_actividad = Convert.ToString(resultado["NOMBRE_ACTIVIDAD"]);
                            if (resultado["OBJETIVO"] != DBNull.Value) entidad.objetivo = Convert.ToString(resultado["OBJETIVO"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToString(resultado["FECHA_INICIO"]);
                            if (resultado["FECHA_TERMINACION"] != DBNull.Value) entidad.fecha_terminacion = Convert.ToString(resultado["FECHA_TERMINACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToString(resultado["COD_PERSONA"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToString(resultado["CENTRO_COSTO"]);
                            lstActividada_NominaEntities.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstActividada_NominaEntities;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Actividada_NominaEntitiesData", "ListarActividada_NominaEntities", ex);
                        return null;
                    }
                }
            }
        }


    }
}