using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Data
{
    public class Empleado_EstudiosData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public Empleado_EstudiosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public Empleado_Estudios CrearEmpleado_Estudios(Empleado_Estudios pEmpleado_Estudios, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo_empleado = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo_empleado.ParameterName = "p_consecutivo_empleado";
                        pconsecutivo_empleado.Value = pEmpleado_Estudios.consecutivo_empleado;
                        pconsecutivo_empleado.Direction = ParameterDirection.Input;
                        pconsecutivo_empleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo_empleado);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pEmpleado_Estudios.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pEmpleado_Estudios.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter psemestre = cmdTransaccionFactory.CreateParameter();
                        psemestre.ParameterName = "p_semestre";
                        if (pEmpleado_Estudios.semestre == null)
                            psemestre.Value = DBNull.Value;
                        else
                            psemestre.Value = pEmpleado_Estudios.semestre;
                        psemestre.Direction = ParameterDirection.Input;
                        psemestre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psemestre);

                        DbParameter pprofesion = cmdTransaccionFactory.CreateParameter();
                        pprofesion.ParameterName = "p_profesion";
                        if (pEmpleado_Estudios.profesion == null)
                            pprofesion.Value = DBNull.Value;
                        else
                            pprofesion.Value = pEmpleado_Estudios.profesion;
                        pprofesion.Direction = ParameterDirection.Input;
                        pprofesion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprofesion);

                        DbParameter phorario_estudio = cmdTransaccionFactory.CreateParameter();
                        phorario_estudio.ParameterName = "p_horario_estudio";
                        if (pEmpleado_Estudios.horario_estudio == null)
                            phorario_estudio.Value = DBNull.Value;
                        else
                            phorario_estudio.Value = pEmpleado_Estudios.horario_estudio;
                        phorario_estudio.Direction = ParameterDirection.Input;
                        phorario_estudio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(phorario_estudio);

                        DbParameter pfecha_inicio = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicio.ParameterName = "p_fecha_inicio";
                        if (pEmpleado_Estudios.fecha_inicio == null)
                            pfecha_inicio.Value = DBNull.Value;
                        else
                            pfecha_inicio.Value = pEmpleado_Estudios.fecha_inicio;
                        pfecha_inicio.Direction = ParameterDirection.Input;
                        pfecha_inicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicio);

                        DbParameter ptitulo_obtenido = cmdTransaccionFactory.CreateParameter();
                        ptitulo_obtenido.ParameterName = "p_titulo_obtenido";
                        if (pEmpleado_Estudios.titulo_obtenido == null)
                            ptitulo_obtenido.Value = DBNull.Value;
                        else
                            ptitulo_obtenido.Value = pEmpleado_Estudios.titulo_obtenido;
                        ptitulo_obtenido.Direction = ParameterDirection.Input;
                        ptitulo_obtenido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptitulo_obtenido);

                        DbParameter pestablecimiento = cmdTransaccionFactory.CreateParameter();
                        pestablecimiento.ParameterName = "p_establecimiento";
                        if (pEmpleado_Estudios.establecimiento == null)
                            pestablecimiento.Value = DBNull.Value;
                        else
                            pestablecimiento.Value = pEmpleado_Estudios.establecimiento;
                        pestablecimiento.Direction = ParameterDirection.Input;
                        pestablecimiento.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestablecimiento);

                        DbParameter pfecha_terminacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_terminacion.ParameterName = "p_fecha_terminacion";
                        if (pEmpleado_Estudios.fecha_terminacion == null)
                            pfecha_terminacion.Value = DBNull.Value;
                        else
                            pfecha_terminacion.Value = pEmpleado_Estudios.fecha_terminacion;
                        pfecha_terminacion.Direction = ParameterDirection.Input;
                        pfecha_terminacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_terminacion);

                        DbParameter phorario_titulo = cmdTransaccionFactory.CreateParameter();
                        phorario_titulo.ParameterName = "p_horario_titulo";
                        if (pEmpleado_Estudios.horario_titulo == null)
                            phorario_titulo.Value = DBNull.Value;
                        else
                            phorario_titulo.Value = pEmpleado_Estudios.horario_titulo;
                        phorario_titulo.Direction = ParameterDirection.Input;
                        phorario_titulo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(phorario_titulo);

                        DbParameter pestudia = cmdTransaccionFactory.CreateParameter();
                        pestudia.ParameterName = "p_estudia";
                        if (pEmpleado_Estudios.estudia == null)
                            pestudia.Value = DBNull.Value;
                        else
                            pestudia.Value = pEmpleado_Estudios.estudia;
                        pestudia.Direction = ParameterDirection.Input;
                        pestudia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestudia);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_EMPLEADO_E_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEmpleado_Estudios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Empleado_EstudiosData", "CrearEmpleado_Estudios", ex);
                        return null;
                    }
                }
            }
        }


        public Empleado_Estudios ModificarEmpleado_Estudios(Empleado_Estudios pEmpleado_Estudios, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo_empleado = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo_empleado.ParameterName = "p_consecutivo_empleado";
                        pconsecutivo_empleado.Value = pEmpleado_Estudios.consecutivo_empleado;
                        pconsecutivo_empleado.Direction = ParameterDirection.Input;
                        pconsecutivo_empleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo_empleado);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pEmpleado_Estudios.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pEmpleado_Estudios.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter psemestre = cmdTransaccionFactory.CreateParameter();
                        psemestre.ParameterName = "p_semestre";
                        if (pEmpleado_Estudios.semestre == null)
                            psemestre.Value = DBNull.Value;
                        else
                            psemestre.Value = pEmpleado_Estudios.semestre;
                        psemestre.Direction = ParameterDirection.Input;
                        psemestre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psemestre);

                        DbParameter pprofesion = cmdTransaccionFactory.CreateParameter();
                        pprofesion.ParameterName = "p_profesion";
                        if (pEmpleado_Estudios.profesion == null)
                            pprofesion.Value = DBNull.Value;
                        else
                            pprofesion.Value = pEmpleado_Estudios.profesion;
                        pprofesion.Direction = ParameterDirection.Input;
                        pprofesion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprofesion);

                        DbParameter phorario_estudio = cmdTransaccionFactory.CreateParameter();
                        phorario_estudio.ParameterName = "p_horario_estudio";
                        if (pEmpleado_Estudios.horario_estudio == null)
                            phorario_estudio.Value = DBNull.Value;
                        else
                            phorario_estudio.Value = pEmpleado_Estudios.horario_estudio;
                        phorario_estudio.Direction = ParameterDirection.Input;
                        phorario_estudio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(phorario_estudio);

                        DbParameter pfecha_inicio = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicio.ParameterName = "p_fecha_inicio";
                        if (pEmpleado_Estudios.fecha_inicio == null)
                            pfecha_inicio.Value = DBNull.Value;
                        else
                            pfecha_inicio.Value = pEmpleado_Estudios.fecha_inicio;
                        pfecha_inicio.Direction = ParameterDirection.Input;
                        pfecha_inicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicio);

                        DbParameter ptitulo_obtenido = cmdTransaccionFactory.CreateParameter();
                        ptitulo_obtenido.ParameterName = "p_titulo_obtenido";
                        if (pEmpleado_Estudios.titulo_obtenido == null)
                            ptitulo_obtenido.Value = DBNull.Value;
                        else
                            ptitulo_obtenido.Value = pEmpleado_Estudios.titulo_obtenido;
                        ptitulo_obtenido.Direction = ParameterDirection.Input;
                        ptitulo_obtenido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptitulo_obtenido);

                        DbParameter pestablecimiento = cmdTransaccionFactory.CreateParameter();
                        pestablecimiento.ParameterName = "p_establecimiento";
                        if (pEmpleado_Estudios.establecimiento == null)
                            pestablecimiento.Value = DBNull.Value;
                        else
                            pestablecimiento.Value = pEmpleado_Estudios.establecimiento;
                        pestablecimiento.Direction = ParameterDirection.Input;
                        pestablecimiento.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestablecimiento);

                        DbParameter pfecha_terminacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_terminacion.ParameterName = "p_fecha_terminacion";
                        if (pEmpleado_Estudios.fecha_terminacion == null)
                            pfecha_terminacion.Value = DBNull.Value;
                        else
                            pfecha_terminacion.Value = pEmpleado_Estudios.fecha_terminacion;
                        pfecha_terminacion.Direction = ParameterDirection.Input;
                        pfecha_terminacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_terminacion);

                        DbParameter phorario_titulo = cmdTransaccionFactory.CreateParameter();
                        phorario_titulo.ParameterName = "p_horario_titulo";
                        if (pEmpleado_Estudios.horario_titulo == null)
                            phorario_titulo.Value = DBNull.Value;
                        else
                            phorario_titulo.Value = pEmpleado_Estudios.horario_titulo;
                        phorario_titulo.Direction = ParameterDirection.Input;
                        phorario_titulo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(phorario_titulo);

                        DbParameter pestudia = cmdTransaccionFactory.CreateParameter();
                        pestudia.ParameterName = "p_estudia";
                        if (pEmpleado_Estudios.estudia == null)
                            pestudia.Value = DBNull.Value;
                        else
                            pestudia.Value = pEmpleado_Estudios.estudia;
                        pestudia.Direction = ParameterDirection.Input;
                        pestudia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestudia);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_EMPLEADO_E_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEmpleado_Estudios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Empleado_EstudiosData", "ModificarEmpleado_Estudios", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarEmpleado_Estudios(Int64 pId, Usuario vUsuario)
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
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_EMPLEADO_E_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Empleado_EstudiosData", "EliminarEmpleado_Estudios", ex);
                    }
                }
            }
        }


        public Empleado_Estudios ConsultarEmpleado_Estudios(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Empleado_Estudios entidad = new Empleado_Estudios();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Empleado_Estudios WHERE CONSECUTIVO_EMPLEADO = " + pId.ToString();
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
                            if (resultado["SEMESTRE"] != DBNull.Value) entidad.semestre = Convert.ToString(resultado["SEMESTRE"]);
                            if (resultado["PROFESION"] != DBNull.Value) entidad.profesion = Convert.ToString(resultado["PROFESION"]);
                            if (resultado["HORARIO_ESTUDIO"] != DBNull.Value) entidad.horario_estudio = Convert.ToInt64(resultado["HORARIO_ESTUDIO"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["TITULO_OBTENIDO"] != DBNull.Value) entidad.titulo_obtenido = Convert.ToString(resultado["TITULO_OBTENIDO"]);
                            if (resultado["ESTABLECIMIENTO"] != DBNull.Value) entidad.establecimiento = Convert.ToString(resultado["ESTABLECIMIENTO"]);
                            if (resultado["FECHA_TERMINACION"] != DBNull.Value) entidad.fecha_terminacion = Convert.ToDateTime(resultado["FECHA_TERMINACION"]);
                            if (resultado["HORARIO_TITULO"] != DBNull.Value) entidad.horario_titulo = Convert.ToInt64(resultado["HORARIO_TITULO"]);
                            if (resultado["ESTUDIA"] != DBNull.Value) entidad.estudia = Convert.ToInt32(resultado["ESTUDIA"]);
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
                        BOExcepcion.Throw("Empleado_EstudiosData", "ConsultarEmpleado_Estudios", ex);
                        return null;
                    }
                }
            }
        }


        public List<Empleado_Estudios> ListarEmpleado_Estudios(long cod_persona, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Empleado_Estudios> lstEmpleado_Estudios = new List<Empleado_Estudios>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Empleado_Estudios WHERE COD_PERSONA = " + cod_persona + " ORDER BY CONSECUTIVO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Empleado_Estudios entidad = new Empleado_Estudios();
                            if (resultado["CONSECUTIVO_EMPLEADO"] != DBNull.Value) entidad.consecutivo_empleado = Convert.ToInt64(resultado["CONSECUTIVO_EMPLEADO"]);
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["SEMESTRE"] != DBNull.Value) entidad.semestre = Convert.ToString(resultado["SEMESTRE"]);
                            if (resultado["PROFESION"] != DBNull.Value) entidad.profesion = Convert.ToString(resultado["PROFESION"]);
                            if (resultado["HORARIO_ESTUDIO"] != DBNull.Value) entidad.horario_estudio = Convert.ToInt64(resultado["HORARIO_ESTUDIO"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["TITULO_OBTENIDO"] != DBNull.Value) entidad.titulo_obtenido = Convert.ToString(resultado["TITULO_OBTENIDO"]);
                            if (resultado["ESTABLECIMIENTO"] != DBNull.Value) entidad.establecimiento = Convert.ToString(resultado["ESTABLECIMIENTO"]);
                            if (resultado["FECHA_TERMINACION"] != DBNull.Value) entidad.fecha_terminacion = Convert.ToDateTime(resultado["FECHA_TERMINACION"]);
                            if (resultado["HORARIO_TITULO"] != DBNull.Value) entidad.horario_titulo = Convert.ToInt64(resultado["HORARIO_TITULO"]);
                            if (resultado["ESTUDIA"] != DBNull.Value) entidad.estudia = Convert.ToInt32(resultado["ESTUDIA"]);
                            lstEmpleado_Estudios.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEmpleado_Estudios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Empleado_EstudiosData", "ListarEmpleado_Estudios", ex);
                        return null;
                    }
                }
            }
        }


    }
}