using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Data
{
    public class Empleado_FamiliarData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public Empleado_FamiliarData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public Empleado_Familiar CrearEmpleado_Familiar(Empleado_Familiar pEmpleado_Familiar, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pEmpleado_Familiar.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pEmpleado_Familiar.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pCodigoEmpleado = cmdTransaccionFactory.CreateParameter();
                        pCodigoEmpleado.ParameterName = "p_CodigoEmpleado";
                        pCodigoEmpleado.Value = pEmpleado_Familiar.codigoempleado;
                        pCodigoEmpleado.Direction = ParameterDirection.Input;
                        pCodigoEmpleado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pCodigoEmpleado);

                        DbParameter pparentezco = cmdTransaccionFactory.CreateParameter();
                        pparentezco.ParameterName = "p_parentezco";
                        if (pEmpleado_Familiar.parentezco == null)
                            pparentezco.Value = DBNull.Value;
                        else
                            pparentezco.Value = pEmpleado_Familiar.parentezco;
                        pparentezco.Direction = ParameterDirection.Input;
                        pparentezco.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pparentezco);

                        DbParameter pidentificacionfamiliar = cmdTransaccionFactory.CreateParameter();
                        pidentificacionfamiliar.ParameterName = "p_identificacionfamiliar";
                        if (pEmpleado_Familiar.identificacionfamiliar == null)
                            pidentificacionfamiliar.Value = DBNull.Value;
                        else
                            pidentificacionfamiliar.Value = pEmpleado_Familiar.identificacionfamiliar;
                        pidentificacionfamiliar.Direction = ParameterDirection.Input;
                        pidentificacionfamiliar.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacionfamiliar);

                        DbParameter ptipoidentificacion = cmdTransaccionFactory.CreateParameter();
                        ptipoidentificacion.ParameterName = "p_tipoidentificacion";
                        if (pEmpleado_Familiar.tipoidentificacion == null)
                            ptipoidentificacion.Value = DBNull.Value;
                        else
                            ptipoidentificacion.Value = pEmpleado_Familiar.tipoidentificacion;
                        ptipoidentificacion.Direction = ParameterDirection.Input;
                        ptipoidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipoidentificacion);

                        DbParameter pnombrefamiliar = cmdTransaccionFactory.CreateParameter();
                        pnombrefamiliar.ParameterName = "p_nombrefamiliar";
                        if (pEmpleado_Familiar.nombrefamiliar == null)
                            pnombrefamiliar.Value = DBNull.Value;
                        else
                            pnombrefamiliar.Value = pEmpleado_Familiar.nombrefamiliar;
                        pnombrefamiliar.Direction = ParameterDirection.Input;
                        pnombrefamiliar.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombrefamiliar);

                        DbParameter pprofesion = cmdTransaccionFactory.CreateParameter();
                        pprofesion.ParameterName = "p_profesion";
                        if (pEmpleado_Familiar.profesion == null)
                            pprofesion.Value = DBNull.Value;
                        else
                            pprofesion.Value = pEmpleado_Familiar.profesion;
                        pprofesion.Direction = ParameterDirection.Input;
                        pprofesion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprofesion);

                        DbParameter pconvivefamiliar = cmdTransaccionFactory.CreateParameter();
                        pconvivefamiliar.ParameterName = "p_convivefamiliar";
                        if (pEmpleado_Familiar.convivefamiliar == null)
                            pconvivefamiliar.Value = DBNull.Value;
                        else
                            pconvivefamiliar.Value = pEmpleado_Familiar.convivefamiliar;
                        pconvivefamiliar.Direction = ParameterDirection.Input;
                        pconvivefamiliar.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pconvivefamiliar);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_EMPLEAFAM_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEmpleado_Familiar;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Empleado_FamiliarData", "CrearEmpleado_Familiar", ex);
                        return null;
                    }
                }
            }
        }


        public Empleado_Familiar ModificarEmpleado_Familiar(Empleado_Familiar pEmpleado_Familiar, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pEmpleado_Familiar.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pEmpleado_Familiar.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pCodigoEmpleado = cmdTransaccionFactory.CreateParameter();
                        pCodigoEmpleado.ParameterName = "p_CodigoEmpleado";
                        pCodigoEmpleado.Value = pEmpleado_Familiar.codigoempleado;
                        pCodigoEmpleado.Direction = ParameterDirection.Input;
                        pCodigoEmpleado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pCodigoEmpleado);

                        DbParameter pparentezco = cmdTransaccionFactory.CreateParameter();
                        pparentezco.ParameterName = "p_parentezco";
                        if (pEmpleado_Familiar.parentezco == null)
                            pparentezco.Value = DBNull.Value;
                        else
                            pparentezco.Value = pEmpleado_Familiar.parentezco;
                        pparentezco.Direction = ParameterDirection.Input;
                        pparentezco.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pparentezco);

                        DbParameter pidentificacionfamiliar = cmdTransaccionFactory.CreateParameter();
                        pidentificacionfamiliar.ParameterName = "p_identificacionfamiliar";
                        if (pEmpleado_Familiar.identificacionfamiliar == null)
                            pidentificacionfamiliar.Value = DBNull.Value;
                        else
                            pidentificacionfamiliar.Value = pEmpleado_Familiar.identificacionfamiliar;
                        pidentificacionfamiliar.Direction = ParameterDirection.Input;
                        pidentificacionfamiliar.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacionfamiliar);

                        DbParameter ptipoidentificacion = cmdTransaccionFactory.CreateParameter();
                        ptipoidentificacion.ParameterName = "p_tipoidentificacion";
                        if (pEmpleado_Familiar.tipoidentificacion == null)
                            ptipoidentificacion.Value = DBNull.Value;
                        else
                            ptipoidentificacion.Value = pEmpleado_Familiar.tipoidentificacion;
                        ptipoidentificacion.Direction = ParameterDirection.Input;
                        ptipoidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipoidentificacion);

                        DbParameter pnombrefamiliar = cmdTransaccionFactory.CreateParameter();
                        pnombrefamiliar.ParameterName = "p_nombrefamiliar";
                        if (pEmpleado_Familiar.nombrefamiliar == null)
                            pnombrefamiliar.Value = DBNull.Value;
                        else
                            pnombrefamiliar.Value = pEmpleado_Familiar.nombrefamiliar;
                        pnombrefamiliar.Direction = ParameterDirection.Input;
                        pnombrefamiliar.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombrefamiliar);

                        DbParameter pprofesion = cmdTransaccionFactory.CreateParameter();
                        pprofesion.ParameterName = "p_profesion";
                        if (pEmpleado_Familiar.profesion == null)
                            pprofesion.Value = DBNull.Value;
                        else
                            pprofesion.Value = pEmpleado_Familiar.profesion;
                        pprofesion.Direction = ParameterDirection.Input;
                        pprofesion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprofesion);

                        DbParameter pconvivefamiliar = cmdTransaccionFactory.CreateParameter();
                        pconvivefamiliar.ParameterName = "p_convivefamiliar";
                        if (pEmpleado_Familiar.convivefamiliar == null)
                            pconvivefamiliar.Value = DBNull.Value;
                        else
                            pconvivefamiliar.Value = pEmpleado_Familiar.convivefamiliar;
                        pconvivefamiliar.Direction = ParameterDirection.Input;
                        pconvivefamiliar.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pconvivefamiliar);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_EMPLEAFAM_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEmpleado_Familiar;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Empleado_FamiliarData", "ModificarEmpleado_Familiar", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarEmpleado_Familiar(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pId;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_EMPLEAFAM_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Empleado_FamiliarData", "EliminarEmpleado_Familiar", ex);
                    }
                }
            }
        }


        public Empleado_Familiar ConsultarEmpleado_Familiar(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Empleado_Familiar entidad = new Empleado_Familiar();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM EMPLEADO_FAMILIAR WHERE CONSECUTIVO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt32(resultado["CONSECUTIVO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt32(resultado["COD_PERSONA"]);
                            if (resultado["CodigoEmpleado"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt32(resultado["CodigoEmpleado"]);
                            if (resultado["PARENTEZCO"] != DBNull.Value) entidad.parentezco = Convert.ToString(resultado["PARENTEZCO"]);
                            if (resultado["IDENTIFICACIONFAMILIAR"] != DBNull.Value) entidad.identificacionfamiliar = Convert.ToString(resultado["IDENTIFICACIONFAMILIAR"]);
                            if (resultado["TIPOIDENTIFICACION"] != DBNull.Value) entidad.tipoidentificacion = Convert.ToString(resultado["TIPOIDENTIFICACION"]);
                            if (resultado["NOMBREFAMILIAR"] != DBNull.Value) entidad.nombrefamiliar = Convert.ToString(resultado["NOMBREFAMILIAR"]);
                            if (resultado["PROFESION"] != DBNull.Value) entidad.profesion = Convert.ToString(resultado["PROFESION"]);
                            if (resultado["CONVIVEFAMILIAR"] != DBNull.Value) entidad.convivefamiliar = Convert.ToString(resultado["CONVIVEFAMILIAR"]);
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
                        BOExcepcion.Throw("Empleado_FamiliarData", "ConsultarEmpleado_Familiar", ex);
                        return null;
                    }
                }
            }
        }


        public List<Empleado_Familiar> ListarEmpleado_Familiar(Empleado_Familiar pEmpleado_Familiar, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Empleado_Familiar> lstEmpleado_Familiar = new List<Empleado_Familiar>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM EMPLEADO_FAMILIAR " + ObtenerFiltro(pEmpleado_Familiar) + " ORDER BY CONSECUTIVO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Empleado_Familiar entidad = new Empleado_Familiar();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt32(resultado["CONSECUTIVO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt32(resultado["COD_PERSONA"]);
                            if (resultado["CodigoEmpleado"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt32(resultado["CodigoEmpleado"]);
                            if (resultado["PARENTEZCO"] != DBNull.Value) entidad.parentezco = Convert.ToString(resultado["PARENTEZCO"]);
                            if (resultado["IDENTIFICACIONFAMILIAR"] != DBNull.Value) entidad.identificacionfamiliar = Convert.ToString(resultado["IDENTIFICACIONFAMILIAR"]);
                            if (resultado["TIPOIDENTIFICACION"] != DBNull.Value) entidad.tipoidentificacion = Convert.ToString(resultado["TIPOIDENTIFICACION"]);
                            if (resultado["NOMBREFAMILIAR"] != DBNull.Value) entidad.nombrefamiliar = Convert.ToString(resultado["NOMBREFAMILIAR"]);
                            if (resultado["PROFESION"] != DBNull.Value) entidad.profesion = Convert.ToString(resultado["PROFESION"]);
                            if (resultado["CONVIVEFAMILIAR"] != DBNull.Value) entidad.convivefamiliar = Convert.ToString(resultado["CONVIVEFAMILIAR"]);
                            lstEmpleado_Familiar.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEmpleado_Familiar;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Empleado_FamiliarData", "ListarEmpleado_Familiar", ex);
                        return null;
                    }
                }
            }
        }


    }
}
