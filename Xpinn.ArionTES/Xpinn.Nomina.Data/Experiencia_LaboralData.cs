using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Data
{
    public class Experiencia_LaboralData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public Experiencia_LaboralData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public Experiencia_Laboral CrearExperiencia_Laboral(Experiencia_Laboral pExperiencia_Laboral, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo_empleado = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo_empleado.ParameterName = "p_consecutivo_empleado";
                        pconsecutivo_empleado.Value = pExperiencia_Laboral.consecutivo_empleado;
                        pconsecutivo_empleado.Direction = ParameterDirection.Input;
                        pconsecutivo_empleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo_empleado);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pExperiencia_Laboral.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pExperiencia_Laboral.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pExperiencia_Laboral.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pnombre_empresa = cmdTransaccionFactory.CreateParameter();
                        pnombre_empresa.ParameterName = "p_nombre_empresa";
                        if (pExperiencia_Laboral.nombre_empresa == null)
                            pnombre_empresa.Value = DBNull.Value;
                        else
                            pnombre_empresa.Value = pExperiencia_Laboral.nombre_empresa;
                        pnombre_empresa.Direction = ParameterDirection.Input;
                        pnombre_empresa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre_empresa);

                        DbParameter pcodcargo = cmdTransaccionFactory.CreateParameter();
                        pcodcargo.ParameterName = "p_codcargo";
                        if (pExperiencia_Laboral.codcargo == null)
                            pcodcargo.Value = DBNull.Value;
                        else
                            pcodcargo.Value = pExperiencia_Laboral.codcargo;
                        pcodcargo.Direction = ParameterDirection.Input;
                        pcodcargo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodcargo);

                        DbParameter pfecha_ingreso = cmdTransaccionFactory.CreateParameter();
                        pfecha_ingreso.ParameterName = "p_fecha_ingreso";
                        if (pExperiencia_Laboral.fecha_ingreso == null)
                            pfecha_ingreso.Value = DBNull.Value;
                        else
                            pfecha_ingreso.Value = pExperiencia_Laboral.fecha_ingreso;
                        pfecha_ingreso.Direction = ParameterDirection.Input;
                        pfecha_ingreso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_ingreso);

                        DbParameter pmotivo_retiro = cmdTransaccionFactory.CreateParameter();
                        pmotivo_retiro.ParameterName = "p_motivo_retiro";
                        if (pExperiencia_Laboral.motivo_retiro == null)
                            pmotivo_retiro.Value = DBNull.Value;
                        else
                            pmotivo_retiro.Value = pExperiencia_Laboral.motivo_retiro;
                        pmotivo_retiro.Direction = ParameterDirection.Input;
                        pmotivo_retiro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmotivo_retiro);

                        DbParameter pfecha_retiro = cmdTransaccionFactory.CreateParameter();
                        pfecha_retiro.ParameterName = "p_fecha_retiro";
                        if (pExperiencia_Laboral.fecha_retiro == null)
                            pfecha_retiro.Value = DBNull.Value;
                        else
                            pfecha_retiro.Value = pExperiencia_Laboral.fecha_retiro;
                        pfecha_retiro.Direction = ParameterDirection.Input;
                        pfecha_retiro.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_retiro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_EXPERIENCI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pExperiencia_Laboral;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Experiencia_LaboralData", "CrearExperiencia_Laboral", ex);
                        return null;
                    }
                }
            }
        }


        public Experiencia_Laboral ModificarExperiencia_Laboral(Experiencia_Laboral pExperiencia_Laboral, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo_empleado = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo_empleado.ParameterName = "p_consecutivo_empleado";
                        pconsecutivo_empleado.Value = pExperiencia_Laboral.consecutivo_empleado;
                        pconsecutivo_empleado.Direction = ParameterDirection.Input;
                        pconsecutivo_empleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo_empleado);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pExperiencia_Laboral.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pExperiencia_Laboral.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pExperiencia_Laboral.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pnombre_empresa = cmdTransaccionFactory.CreateParameter();
                        pnombre_empresa.ParameterName = "p_nombre_empresa";
                        if (pExperiencia_Laboral.nombre_empresa == null)
                            pnombre_empresa.Value = DBNull.Value;
                        else
                            pnombre_empresa.Value = pExperiencia_Laboral.nombre_empresa;
                        pnombre_empresa.Direction = ParameterDirection.Input;
                        pnombre_empresa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre_empresa);

                        DbParameter pcodcargo = cmdTransaccionFactory.CreateParameter();
                        pcodcargo.ParameterName = "p_codcargo";
                        if (pExperiencia_Laboral.codcargo == null)
                            pcodcargo.Value = DBNull.Value;
                        else
                            pcodcargo.Value = pExperiencia_Laboral.codcargo;
                        pcodcargo.Direction = ParameterDirection.Input;
                        pcodcargo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodcargo);

                        DbParameter pfecha_ingreso = cmdTransaccionFactory.CreateParameter();
                        pfecha_ingreso.ParameterName = "p_fecha_ingreso";
                        if (pExperiencia_Laboral.fecha_ingreso == null)
                            pfecha_ingreso.Value = DBNull.Value;
                        else
                            pfecha_ingreso.Value = pExperiencia_Laboral.fecha_ingreso;
                        pfecha_ingreso.Direction = ParameterDirection.Input;
                        pfecha_ingreso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_ingreso);

                        DbParameter pmotivo_retiro = cmdTransaccionFactory.CreateParameter();
                        pmotivo_retiro.ParameterName = "p_motivo_retiro";
                        if (pExperiencia_Laboral.motivo_retiro == null)
                            pmotivo_retiro.Value = DBNull.Value;
                        else
                            pmotivo_retiro.Value = pExperiencia_Laboral.motivo_retiro;
                        pmotivo_retiro.Direction = ParameterDirection.Input;
                        pmotivo_retiro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmotivo_retiro);

                        DbParameter pfecha_retiro = cmdTransaccionFactory.CreateParameter();
                        pfecha_retiro.ParameterName = "p_fecha_retiro";
                        if (pExperiencia_Laboral.fecha_retiro == null)
                            pfecha_retiro.Value = DBNull.Value;
                        else
                            pfecha_retiro.Value = pExperiencia_Laboral.fecha_retiro;
                        pfecha_retiro.Direction = ParameterDirection.Input;
                        pfecha_retiro.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_retiro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_EXPERIENCI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pExperiencia_Laboral;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Experiencia_LaboralData", "ModificarExperiencia_Laboral", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarExperiencia_Laboral(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo_empleado = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo_empleado.ParameterName = "p_consecutivo";
                        pconsecutivo_empleado.Value = pId;
                        pconsecutivo_empleado.Direction = ParameterDirection.Input;
                        pconsecutivo_empleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo_empleado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_EXPERIENCI_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Experiencia_LaboralData", "EliminarExperiencia_Laboral", ex);
                    }
                }
            }
        }


        public Experiencia_Laboral ConsultarExperiencia_Laboral(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Experiencia_Laboral entidad = new Experiencia_Laboral();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Experiencia_Laboral WHERE CONSECUTIVO_EMPLEADO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO_EMPLEADO"] != DBNull.Value) entidad.consecutivo_empleado = Convert.ToInt64(resultado["CONSECUTIVO_EMPLEADO"]);
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NOMBRE_EMPRESA"] != DBNull.Value) entidad.nombre_empresa = Convert.ToString(resultado["NOMBRE_EMPRESA"]);
                            if (resultado["CODCARGO"] != DBNull.Value) entidad.codcargo = Convert.ToInt32(resultado["CODCARGO"]);
                            if (resultado["FECHA_INGRESO"] != DBNull.Value) entidad.fecha_ingreso = Convert.ToDateTime(resultado["FECHA_INGRESO"]);
                            if (resultado["MOTIVO_RETIRO"] != DBNull.Value) entidad.motivo_retiro = Convert.ToString(resultado["MOTIVO_RETIRO"]);
                            if (resultado["FECHA_RETIRO"] != DBNull.Value) entidad.fecha_retiro = Convert.ToDateTime(resultado["FECHA_RETIRO"]);
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
                        BOExcepcion.Throw("Experiencia_LaboralData", "ConsultarExperiencia_Laboral", ex);
                        return null;
                    }
                }
            }
        }


        public List<Experiencia_Laboral> ListarExperiencia_Laboral(long cod_persona, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Experiencia_Laboral> lstExperiencia_Laboral = new List<Experiencia_Laboral>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Experiencia_Laboral WHERE COD_PERSONA = " + cod_persona + " ORDER BY CONSECUTIVO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Experiencia_Laboral entidad = new Experiencia_Laboral();
                            if (resultado["CONSECUTIVO_EMPLEADO"] != DBNull.Value) entidad.consecutivo_empleado = Convert.ToInt64(resultado["CONSECUTIVO_EMPLEADO"]);
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NOMBRE_EMPRESA"] != DBNull.Value) entidad.nombre_empresa = Convert.ToString(resultado["NOMBRE_EMPRESA"]);
                            if (resultado["CODCARGO"] != DBNull.Value) entidad.codcargo = Convert.ToInt32(resultado["CODCARGO"]);
                            if (resultado["FECHA_INGRESO"] != DBNull.Value) entidad.fecha_ingreso = Convert.ToDateTime(resultado["FECHA_INGRESO"]);
                            if (resultado["MOTIVO_RETIRO"] != DBNull.Value) entidad.motivo_retiro = Convert.ToString(resultado["MOTIVO_RETIRO"]);
                            if (resultado["FECHA_RETIRO"] != DBNull.Value) entidad.fecha_retiro = Convert.ToDateTime(resultado["FECHA_RETIRO"]);
                            lstExperiencia_Laboral.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstExperiencia_Laboral;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Experiencia_LaboralData", "ListarExperiencia_Laboral", ex);
                        return null;
                    }
                }
            }
        }


    }
}
