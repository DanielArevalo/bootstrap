using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Aportes.Entities;

namespace Xpinn.Aportes.Data
{
    public class DirectivoData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public DirectivoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public Directivo CrearDirectivo(Directivo pDirectivo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pDirectivo.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.InputOutput;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        pidentificacion.Value = pDirectivo.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pcalidad = cmdTransaccionFactory.CreateParameter();
                        pcalidad.ParameterName = "p_calidad";
                        pcalidad.Value = pDirectivo.calidad;
                        pcalidad.Direction = ParameterDirection.Input;
                        pcalidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcalidad);

                        DbParameter pfecha_nombramiento = cmdTransaccionFactory.CreateParameter();
                        pfecha_nombramiento.ParameterName = "p_fecha_nombramiento";
                        if (pDirectivo.fecha_nombramiento == null)
                            pfecha_nombramiento.Value = DBNull.Value;
                        else
                            pfecha_nombramiento.Value = pDirectivo.fecha_nombramiento;
                        pfecha_nombramiento.Direction = ParameterDirection.Input;
                        pfecha_nombramiento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_nombramiento);

                        DbParameter pparientes = cmdTransaccionFactory.CreateParameter();
                        pparientes.ParameterName = "p_parientes";
                        if (pDirectivo.parientes == null)
                            pparientes.Value = DBNull.Value;
                        else
                            pparientes.Value = pDirectivo.parientes;
                        pparientes.Direction = ParameterDirection.Input;
                        pparientes.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pparientes);

                        DbParameter pvinculos_organiza = cmdTransaccionFactory.CreateParameter();
                        pvinculos_organiza.ParameterName = "p_vinculos_organiza";
                        if (pDirectivo.vinculos_organiza == null)
                            pvinculos_organiza.Value = DBNull.Value;
                        else
                            pvinculos_organiza.Value = pDirectivo.vinculos_organiza;
                        pvinculos_organiza.Direction = ParameterDirection.Input;
                        pvinculos_organiza.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvinculos_organiza);

                        DbParameter ptipo_directivo = cmdTransaccionFactory.CreateParameter();
                        ptipo_directivo.ParameterName = "p_tipo_directivo";
                        if (pDirectivo.tipo_directivo == null)
                            ptipo_directivo.Value = DBNull.Value;
                        else
                            ptipo_directivo.Value = pDirectivo.tipo_directivo;
                        ptipo_directivo.Direction = ParameterDirection.Input;
                        ptipo_directivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_directivo);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pDirectivo.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pDirectivo.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pvigencia_final = cmdTransaccionFactory.CreateParameter();
                        pvigencia_final.ParameterName = "p_vigencia_final";
                        if (pDirectivo.vigencia_final == null)
                            pvigencia_final.Value = DBNull.Value;
                        else
                            pvigencia_final.Value = pDirectivo.vigencia_final;
                        pvigencia_final.Direction = ParameterDirection.Input;
                        pvigencia_final.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pvigencia_final);

                        DbParameter pfecha_posesion = cmdTransaccionFactory.CreateParameter();
                        pfecha_posesion.ParameterName = "p_fecha_posesion";
                        if (pDirectivo.fecha_posesion == null)
                            pfecha_posesion.Value = DBNull.Value;
                        else
                            pfecha_posesion.Value = pDirectivo.fecha_posesion;
                        pfecha_posesion.Direction = ParameterDirection.Input;
                        pfecha_posesion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_posesion);

                        DbParameter pempresa = cmdTransaccionFactory.CreateParameter();
                        pempresa.ParameterName = "p_empresa";
                        if (pDirectivo.empresa == null)
                            pempresa.Value = DBNull.Value;
                        else
                            pempresa.Value = pDirectivo.empresa;
                        pempresa.Direction = ParameterDirection.Input;
                        pempresa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pempresa);

                        DbParameter pvigencia_inicio = cmdTransaccionFactory.CreateParameter();
                        pvigencia_inicio.ParameterName = "p_vigencia_inicio";
                        if (pDirectivo.vigencia_inicio == null)
                            pvigencia_inicio.Value = DBNull.Value;
                        else
                            pvigencia_inicio.Value = pDirectivo.vigencia_inicio;
                        pvigencia_inicio.Direction = ParameterDirection.Input;
                        pvigencia_inicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pvigencia_inicio);

                        DbParameter p_email_inst = cmdTransaccionFactory.CreateParameter();
                        p_email_inst.ParameterName = "p_email_inst";
                        if (pDirectivo.email == null)
                            p_email_inst.Value = DBNull.Value;
                        else
                            p_email_inst.Value = pDirectivo.email;
                        p_email_inst.Direction = ParameterDirection.Input;
                        p_email_inst.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_email_inst);

                        DbParameter p_num_radi_pose = cmdTransaccionFactory.CreateParameter();
                        p_num_radi_pose.ParameterName = "p_num_radi_pose";
                        if (pDirectivo.num_radi_pose == null)
                            p_num_radi_pose.Value = DBNull.Value;
                        else
                            p_num_radi_pose.Value = pDirectivo.num_radi_pose;
                        p_num_radi_pose.Direction = ParameterDirection.Input;
                        p_num_radi_pose.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_num_radi_pose);

                        DbParameter p_t_p_revisor = cmdTransaccionFactory.CreateParameter();
                        p_t_p_revisor.ParameterName = "p_t_p_revisor";
                        if (pDirectivo.tarj_rev_fiscar == null)
                            p_t_p_revisor.Value = DBNull.Value;
                        else
                            p_t_p_revisor.Value = pDirectivo.tarj_rev_fiscar;
                        p_t_p_revisor.Direction = ParameterDirection.Input;
                        p_t_p_revisor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_t_p_revisor);

                        pDirectivo.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_DIRECTIVO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDirectivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DirectivoData", "CrearDirectivo", ex);
                        return null;
                    }
                }
            }
        }

        public bool ValidarPersonaNoSeaDirectivoYa(string identificacion, Usuario pusuario)
        {
            DbDataReader resultado;
            bool existe = false;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT *
                                        FROM DIRECTIVO
                                        WHERE IDENTIFICACION = '" + identificacion + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            existe = true;
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return existe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DirectivoData", "ValidarPersonaNoSeaDirectivoYa", ex);
                        return false;
                    }
                }
            }
        }

        public Directivo ModificarDirectivo(Directivo pDirectivo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pDirectivo.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        pidentificacion.Value = pDirectivo.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pcalidad = cmdTransaccionFactory.CreateParameter();
                        pcalidad.ParameterName = "p_calidad";
                        pcalidad.Value = pDirectivo.calidad;
                        pcalidad.Direction = ParameterDirection.Input;
                        pcalidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcalidad);

                        DbParameter pfecha_nombramiento = cmdTransaccionFactory.CreateParameter();
                        pfecha_nombramiento.ParameterName = "p_fecha_nombramiento";
                        if (pDirectivo.fecha_nombramiento == null)
                            pfecha_nombramiento.Value = DBNull.Value;
                        else
                            pfecha_nombramiento.Value = pDirectivo.fecha_nombramiento;
                        pfecha_nombramiento.Direction = ParameterDirection.Input;
                        pfecha_nombramiento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_nombramiento);

                        DbParameter pparientes = cmdTransaccionFactory.CreateParameter();
                        pparientes.ParameterName = "p_parientes";
                        if (pDirectivo.parientes == null)
                            pparientes.Value = DBNull.Value;
                        else
                            pparientes.Value = pDirectivo.parientes;
                        pparientes.Direction = ParameterDirection.Input;
                        pparientes.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pparientes);

                        DbParameter pvinculos_organiza = cmdTransaccionFactory.CreateParameter();
                        pvinculos_organiza.ParameterName = "p_vinculos_organiza";
                        if (pDirectivo.vinculos_organiza == null)
                            pvinculos_organiza.Value = DBNull.Value;
                        else
                            pvinculos_organiza.Value = pDirectivo.vinculos_organiza;
                        pvinculos_organiza.Direction = ParameterDirection.Input;
                        pvinculos_organiza.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvinculos_organiza);

                        DbParameter ptipo_directivo = cmdTransaccionFactory.CreateParameter();
                        ptipo_directivo.ParameterName = "p_tipo_directivo";
                        if (pDirectivo.tipo_directivo == null)
                            ptipo_directivo.Value = DBNull.Value;
                        else
                            ptipo_directivo.Value = pDirectivo.tipo_directivo;
                        ptipo_directivo.Direction = ParameterDirection.Input;
                        ptipo_directivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_directivo);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pDirectivo.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pDirectivo.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pvigencia_final = cmdTransaccionFactory.CreateParameter();
                        pvigencia_final.ParameterName = "p_vigencia_final";
                        if (pDirectivo.vigencia_final == null)
                            pvigencia_final.Value = DBNull.Value;
                        else
                            pvigencia_final.Value = pDirectivo.vigencia_final;
                        pvigencia_final.Direction = ParameterDirection.Input;
                        pvigencia_final.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pvigencia_final);

                        DbParameter pfecha_posesion = cmdTransaccionFactory.CreateParameter();
                        pfecha_posesion.ParameterName = "p_fecha_posesion";
                        if (pDirectivo.fecha_posesion == null)
                            pfecha_posesion.Value = DBNull.Value;
                        else
                            pfecha_posesion.Value = pDirectivo.fecha_posesion;
                        pfecha_posesion.Direction = ParameterDirection.Input;
                        pfecha_posesion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_posesion);

                        DbParameter pempresa = cmdTransaccionFactory.CreateParameter();
                        pempresa.ParameterName = "p_empresa";
                        if (pDirectivo.empresa == null)
                            pempresa.Value = DBNull.Value;
                        else
                            pempresa.Value = pDirectivo.empresa;
                        pempresa.Direction = ParameterDirection.Input;
                        pempresa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pempresa);

                        DbParameter pvigencia_inicio = cmdTransaccionFactory.CreateParameter();
                        pvigencia_inicio.ParameterName = "p_vigencia_inicio";
                        if (pDirectivo.vigencia_inicio == null)
                            pvigencia_inicio.Value = DBNull.Value;
                        else
                            pvigencia_inicio.Value = pDirectivo.vigencia_inicio;
                        pvigencia_inicio.Direction = ParameterDirection.Input;
                        pvigencia_inicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pvigencia_inicio);

                        DbParameter p_email_inst = cmdTransaccionFactory.CreateParameter();
                        p_email_inst.ParameterName = "p_email_inst";
                        if (pDirectivo.email == null)
                            p_email_inst.Value = DBNull.Value;
                        else
                            p_email_inst.Value = pDirectivo.email;
                        p_email_inst.Direction = ParameterDirection.Input;
                        p_email_inst.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_email_inst);

                        DbParameter p_num_radi_pose = cmdTransaccionFactory.CreateParameter();
                        p_num_radi_pose.ParameterName = "p_num_radi_pose";
                        if (pDirectivo.num_radi_pose == null)
                            p_num_radi_pose.Value = DBNull.Value;
                        else
                            p_num_radi_pose.Value = pDirectivo.num_radi_pose;
                        p_num_radi_pose.Direction = ParameterDirection.Input;
                        p_num_radi_pose.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_num_radi_pose);

                        DbParameter p_t_p_revisor = cmdTransaccionFactory.CreateParameter();
                        p_t_p_revisor.ParameterName = "p_t_p_revisor";
                        if (pDirectivo.tarj_rev_fiscar == null)
                            p_t_p_revisor.Value = DBNull.Value;
                        else
                            p_t_p_revisor.Value = pDirectivo.tarj_rev_fiscar;
                        p_t_p_revisor.Direction = ParameterDirection.Input;
                        p_t_p_revisor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_t_p_revisor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_DIRECTIVO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDirectivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DirectivoData", "ModificarDirectivo", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarDirectivo(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Directivo pDirectivo = new Directivo();
                        pDirectivo = ConsultarDirectivo(pId, vUsuario);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pDirectivo.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_DIRECTIVO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DirectivoData", "EliminarDirectivo", ex);
                    }
                }
            }
        }


        public Directivo ConsultarDirectivo(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Directivo entidad = new Directivo();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT dir.*, 
                                        per.PRIMER_NOMBRE | | ' ' | |  per.SEGUNDO_NOMBRE | | ' ' | | per.PRIMER_APELLIDO | | ' ' | | per.SEGUNDO_APELLIDO as Nombres,
                                        per.COD_PERSONA
                                        FROM DIRECTIVO dir
                                        JOIN PERSONA per ON per.IDENTIFICACION = dir.IDENTIFICACION 
                                        WHERE dir.CONSECUTIVO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["Nombres"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["Nombres"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["CALIDAD"] != DBNull.Value) entidad.calidad = Convert.ToInt32(resultado["CALIDAD"]);
                            if (resultado["FECHA_NOMBRAMIENTO"] != DBNull.Value) entidad.fecha_nombramiento = Convert.ToDateTime(resultado["FECHA_NOMBRAMIENTO"]);
                            if (resultado["PARIENTES"] != DBNull.Value) entidad.parientes = Convert.ToString(resultado["PARIENTES"]);
                            if (resultado["VINCULOS_ORGANIZA"] != DBNull.Value) entidad.vinculos_organiza = Convert.ToString(resultado["VINCULOS_ORGANIZA"]);
                            if (resultado["TIPO_DIRECTIVO"] != DBNull.Value) entidad.tipo_directivo = Convert.ToInt32(resultado["TIPO_DIRECTIVO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["VIGENCIA_FINAL"] != DBNull.Value) entidad.vigencia_final = Convert.ToDateTime(resultado["VIGENCIA_FINAL"]);
                            if (resultado["FECHA_POSESION"] != DBNull.Value) entidad.fecha_posesion = Convert.ToDateTime(resultado["FECHA_POSESION"]);
                            if (resultado["EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["EMPRESA"]);
                            if (resultado["VIGENCIA_INICIO"] != DBNull.Value) entidad.vigencia_inicio = Convert.ToDateTime(resultado["VIGENCIA_INICIO"]);
                            if (resultado["EMAIL_INST"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL_INST"]);
                            if (resultado["NUM_RADI_POSE"] != DBNull.Value) entidad.num_radi_pose = Convert.ToString(resultado["NUM_RADI_POSE"]);
                            if (resultado["T_P_REVISOR"] != DBNull.Value) entidad.tarj_rev_fiscar = Convert.ToString(resultado["T_P_REVISOR"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DirectivoData", "ConsultarDirectivo", ex);
                        return null;
                    }
                }
            }
        }


        public List<Directivo> ListarDirectivo(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Directivo> lstDirectivo = new List<Directivo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select dir.consecutivo, dir.identificacion, dir.VIGENCIA_INICIO, dir.VIGENCIA_FINAL, dir.FECHA_NOMBRAMIENTO ,
                                         CASE dir.calidad WHEN 0 THEN 'Principal' WHEN 1 THEN 'Suplente' END as desc_Calidad,
                                         CASE dir.estado WHEN '0' THEN 'Nombrado' WHEN '1' THEN 'Retirado' WHEN '2' THEN 'EXCLUIDO' END as desc_estado,
                                          per.PRIMER_NOMBRE | | ' ' | | per.PRIMER_APELLIDO as Nombre,
                                          tip.DESCRIPCION as desc_tipo_dir
                                        from directivo dir
                                        JOIN TIPO_DIRECTIVO tip on tip.CONSECUTIVO = dir.TIPO_DIRECTIVO
                                        JOIN PERSONA per on per.IDENTIFICACION = dir.IDENTIFICACION "
                                        + filtro +                        
                                        " ORDER BY 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Directivo entidad = new Directivo();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["desc_Calidad"] != DBNull.Value) entidad.desc_calidad = Convert.ToString(resultado["desc_Calidad"]);
                            if (resultado["FECHA_NOMBRAMIENTO"] != DBNull.Value) entidad.fecha_nombramiento = Convert.ToDateTime(resultado["FECHA_NOMBRAMIENTO"]);
                            if (resultado["Nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["Nombre"]);
                            if (resultado["desc_tipo_dir"] != DBNull.Value) entidad.desc_tipo_directivo = Convert.ToString(resultado["desc_tipo_dir"]);
                            if (resultado["desc_estado"] != DBNull.Value) entidad.desc_estado = Convert.ToString(resultado["desc_estado"]);
                            if (resultado["VIGENCIA_FINAL"] != DBNull.Value) entidad.vigencia_final = Convert.ToDateTime(resultado["VIGENCIA_FINAL"]);
                            if (resultado["VIGENCIA_INICIO"] != DBNull.Value) entidad.vigencia_inicio = Convert.ToDateTime(resultado["VIGENCIA_INICIO"]);

                            lstDirectivo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDirectivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DirectivoData", "ListarDirectivo", ex);
                        return null;
                    }
                }
            }
        }


    }
}