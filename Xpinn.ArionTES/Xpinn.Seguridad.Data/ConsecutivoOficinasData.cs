using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Seguridad.Entities;

namespace Xpinn.Seguridad.Data
{
    public class ConsecutivoOficinasData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public ConsecutivoOficinasData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public ConsecutivoOficinas CrearConsecutivoOficinas(ConsecutivoOficinas pOficina, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pidconsecutivo.ParameterName = "p_idconsecutivo";
                        pidconsecutivo.Value = pOficina.idconsecutivo;
                        pidconsecutivo.Direction = ParameterDirection.Output;
                        pidconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidconsecutivo);

                        DbParameter ptabla = cmdTransaccionFactory.CreateParameter();
                        ptabla.ParameterName = "p_tabla";
                        if (pOficina.tabla == null)
                            ptabla.Value = DBNull.Value;
                        else
                            ptabla.Value = pOficina.tabla;
                        ptabla.Direction = ParameterDirection.Input;
                        ptabla.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptabla);

                        DbParameter pcolumna = cmdTransaccionFactory.CreateParameter();
                        pcolumna.ParameterName = "p_columna";
                        if (pOficina.columna == null)
                            pcolumna.Value = DBNull.Value;
                        else
                            pcolumna.Value = pOficina.columna;
                        pcolumna.Direction = ParameterDirection.Input;
                        pcolumna.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcolumna);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        if (pOficina.cod_oficina == null)
                            pcod_oficina.Value = DBNull.Value;
                        else
                            pcod_oficina.Value = pOficina.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        DbParameter ptipo_consecutivo = cmdTransaccionFactory.CreateParameter();
                        ptipo_consecutivo.ParameterName = "p_tipo_consecutivo";
                        if (pOficina.tipo_consecutivo == null)
                            ptipo_consecutivo.Value = DBNull.Value;
                        else
                            ptipo_consecutivo.Value = pOficina.tipo_consecutivo;
                        ptipo_consecutivo.Direction = ParameterDirection.Input;
                        ptipo_consecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_consecutivo);

                        DbParameter prango_inicial = cmdTransaccionFactory.CreateParameter();
                        prango_inicial.ParameterName = "p_rango_inicial";
                        if (pOficina.rango_inicial == null)
                            prango_inicial.Value = DBNull.Value;
                        else
                            prango_inicial.Value = pOficina.rango_inicial;
                        prango_inicial.Direction = ParameterDirection.Input;
                        prango_inicial.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(prango_inicial);

                        DbParameter prango_final = cmdTransaccionFactory.CreateParameter();
                        prango_final.ParameterName = "p_rango_final";
                        if (pOficina.rango_final == null)
                            prango_final.Value = DBNull.Value;
                        else
                            prango_final.Value = pOficina.rango_final;
                        prango_final.Direction = ParameterDirection.Input;
                        prango_final.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(prango_final);

                        DbParameter pfechacreacion = cmdTransaccionFactory.CreateParameter();
                        pfechacreacion.ParameterName = "p_fechacreacion";
                        if (pOficina.fechacreacion == null)
                            pfechacreacion.Value = DBNull.Value;
                        else
                            pfechacreacion.Value = pOficina.fechacreacion;
                        pfechacreacion.Direction = ParameterDirection.Input;
                        pfechacreacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacreacion);

                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "p_usuariocreacion";
                        if (pOficina.usuariocreacion == null)
                            pusuariocreacion.Value = DBNull.Value;
                        else
                            pusuariocreacion.Value = pOficina.usuariocreacion;
                        pusuariocreacion.Direction = ParameterDirection.Input;
                        pusuariocreacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);

                        DbParameter pfecultmod = cmdTransaccionFactory.CreateParameter();
                        pfecultmod.ParameterName = "p_fecultmod";
                        if (pOficina.fecultmod == null)
                            pfecultmod.Value = DBNull.Value;
                        else
                            pfecultmod.Value = pOficina.fecultmod;
                        pfecultmod.Direction = ParameterDirection.Input;
                        pfecultmod.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecultmod);

                        DbParameter pusuarioultmod = cmdTransaccionFactory.CreateParameter();
                        pusuarioultmod.ParameterName = "p_usuarioultmod";
                        if (pOficina.usuarioultmod == null)
                            pusuarioultmod.Value = DBNull.Value;
                        else
                            pusuarioultmod.Value = pOficina.usuarioultmod;
                        pusuarioultmod.Direction = ParameterDirection.Input;
                        pusuarioultmod.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuarioultmod);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_CONSE_OF_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pOficina.idconsecutivo = Convert.ToInt32(pidconsecutivo.Value);
                        return pOficina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConsecutivoOficinaData", "CrearConsecutivoOficinas", ex);
                        return null;
                    }
                }
            }
        }


        public ConsecutivoOficinas ModificarConsecutivoOficinas(ConsecutivoOficinas pOficina, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pidconsecutivo.ParameterName = "p_idconsecutivo";
                        pidconsecutivo.Value = pOficina.idconsecutivo;
                        pidconsecutivo.Direction = ParameterDirection.Input;
                        pidconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidconsecutivo);

                        DbParameter ptabla = cmdTransaccionFactory.CreateParameter();
                        ptabla.ParameterName = "p_tabla";
                        if (pOficina.tabla == null)
                            ptabla.Value = DBNull.Value;
                        else
                            ptabla.Value = pOficina.tabla;
                        ptabla.Direction = ParameterDirection.Input;
                        ptabla.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptabla);

                        DbParameter pcolumna = cmdTransaccionFactory.CreateParameter();
                        pcolumna.ParameterName = "p_columna";
                        if (pOficina.columna == null)
                            pcolumna.Value = DBNull.Value;
                        else
                            pcolumna.Value = pOficina.columna;
                        pcolumna.Direction = ParameterDirection.Input;
                        pcolumna.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcolumna);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        if (pOficina.cod_oficina == null)
                            pcod_oficina.Value = DBNull.Value;
                        else
                            pcod_oficina.Value = pOficina.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        DbParameter ptipo_consecutivo = cmdTransaccionFactory.CreateParameter();
                        ptipo_consecutivo.ParameterName = "p_tipo_consecutivo";
                        if (pOficina.tipo_consecutivo == null)
                            ptipo_consecutivo.Value = DBNull.Value;
                        else
                            ptipo_consecutivo.Value = pOficina.tipo_consecutivo;
                        ptipo_consecutivo.Direction = ParameterDirection.Input;
                        ptipo_consecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_consecutivo);

                        DbParameter prango_inicial = cmdTransaccionFactory.CreateParameter();
                        prango_inicial.ParameterName = "p_rango_inicial";
                        if (pOficina.rango_inicial == null)
                            prango_inicial.Value = DBNull.Value;
                        else
                            prango_inicial.Value = pOficina.rango_inicial;
                        prango_inicial.Direction = ParameterDirection.Input;
                        prango_inicial.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(prango_inicial);

                        DbParameter prango_final = cmdTransaccionFactory.CreateParameter();
                        prango_final.ParameterName = "p_rango_final";
                        if (pOficina.rango_final == null)
                            prango_final.Value = DBNull.Value;
                        else
                            prango_final.Value = pOficina.rango_final;
                        prango_final.Direction = ParameterDirection.Input;
                        prango_final.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(prango_final);

                        DbParameter pfechacreacion = cmdTransaccionFactory.CreateParameter();
                        pfechacreacion.ParameterName = "p_fechacreacion";
                        if (pOficina.fechacreacion == null)
                            pfechacreacion.Value = DBNull.Value;
                        else
                            pfechacreacion.Value = pOficina.fechacreacion;
                        pfechacreacion.Direction = ParameterDirection.Input;
                        pfechacreacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacreacion);

                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "p_usuariocreacion";
                        if (pOficina.usuariocreacion == null)
                            pusuariocreacion.Value = DBNull.Value;
                        else
                            pusuariocreacion.Value = pOficina.usuariocreacion;
                        pusuariocreacion.Direction = ParameterDirection.Input;
                        pusuariocreacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);

                        DbParameter pfecultmod = cmdTransaccionFactory.CreateParameter();
                        pfecultmod.ParameterName = "p_fecultmod";
                        if (pOficina.fecultmod == null)
                            pfecultmod.Value = DBNull.Value;
                        else
                            pfecultmod.Value = pOficina.fecultmod;
                        pfecultmod.Direction = ParameterDirection.Input;
                        pfecultmod.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecultmod);

                        DbParameter pusuarioultmod = cmdTransaccionFactory.CreateParameter();
                        pusuarioultmod.ParameterName = "p_usuarioultmod";
                        if (pOficina.usuarioultmod == null)
                            pusuarioultmod.Value = DBNull.Value;
                        else
                            pusuarioultmod.Value = pOficina.usuarioultmod;
                        pusuarioultmod.Direction = ParameterDirection.Input;
                        pusuarioultmod.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuarioultmod);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_CONSE_OF_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pOficina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConsecutivoOficinaData", "ModificarConsecutivoOficinas", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarConsecutivoOficinas(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ConsecutivoOficinas pOficina = new ConsecutivoOficinas();
                        pOficina = ConsultarConsecutivoOficinas(pId, vUsuario);

                        DbParameter pidconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pidconsecutivo.ParameterName = "p_idconsecutivo";
                        pidconsecutivo.Value = pOficina.idconsecutivo;
                        pidconsecutivo.Direction = ParameterDirection.Input;
                        pidconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ADM_OFICONSEC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConsecutivoOficinaData", "EliminarConsecutivoOficinas", ex);
                    }
                }
            }
        }


        public ConsecutivoOficinas ConsultarConsecutivoOficinas(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ConsecutivoOficinas entidad = new ConsecutivoOficinas();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CONSECUTIVO_OFICINAS WHERE IDCONSECUTIVO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDCONSECUTIVO"] != DBNull.Value) entidad.idconsecutivo = Convert.ToInt32(resultado["IDCONSECUTIVO"]);
                            if (resultado["TABLA"] != DBNull.Value) entidad.tabla = Convert.ToString(resultado["TABLA"]);
                            if (resultado["COLUMNA"] != DBNull.Value) entidad.columna = Convert.ToString(resultado["COLUMNA"]);

                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["TIPO_CONSECUTIVO"] != DBNull.Value) entidad.tipo_consecutivo = Convert.ToInt32(resultado["TIPO_CONSECUTIVO"]);
                            if (resultado["RANGO_INICIAL"] != DBNull.Value) entidad.rango_inicial = Convert.ToInt64(resultado["RANGO_INICIAL"]);
                            if (resultado["RANGO_FINAL"] != DBNull.Value) entidad.rango_final = Convert.ToInt64(resultado["RANGO_FINAL"]);

                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["TIPO_CONSECUTIVO"] != DBNull.Value) entidad.tipo_consecutivo = Convert.ToInt64(resultado["TIPO_CONSECUTIVO"]);
                            if (resultado["RANGO_INICIAL"] != DBNull.Value) entidad.rango_inicial = Convert.ToInt64(resultado["RANGO_INICIAL"]);
                            if (resultado["RANGO_FINAL"] != DBNull.Value) entidad.rango_final = Convert.ToInt64(resultado["RANGO_FINAL"]);

                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUARIOULTMOD"] != DBNull.Value) entidad.usuarioultmod = Convert.ToString(resultado["USUARIOULTMOD"]);
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
                        BOExcepcion.Throw("ConsecutivoOficinaData", "ConsultarConsecutivoOficinas", ex);
                        return null;
                    }
                }
            }
        }
        public ConsecutivoOficinas ConsultarConsOfiXOfyTabla(String pIdTabla, Int64 pIdOficina, Int64 prangoin, Int64 prangfin,Usuario vUsuario)
        {
            DbDataReader resultado;
            ConsecutivoOficinas entidad = new ConsecutivoOficinas();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CONSECUTIVO_OFICINAS WHERE TABLA  = '" + pIdTabla.ToString() + "'" + " and COD_OFICINA=" + pIdOficina.ToString() + " and RANGO_FINAL >=  " + prangoin.ToString() + "  and RANGO_INICIAL  <=  " + prangfin.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDCONSECUTIVO"] != DBNull.Value) entidad.idconsecutivo = Convert.ToInt32(resultado["IDCONSECUTIVO"]);
                            if (resultado["TABLA"] != DBNull.Value) entidad.tabla = Convert.ToString(resultado["TABLA"]);
                            if (resultado["COLUMNA"] != DBNull.Value) entidad.columna = Convert.ToString(resultado["COLUMNA"]);

                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["TIPO_CONSECUTIVO"] != DBNull.Value) entidad.tipo_consecutivo = Convert.ToInt32(resultado["TIPO_CONSECUTIVO"]);
                            if (resultado["RANGO_INICIAL"] != DBNull.Value) entidad.rango_inicial = Convert.ToInt64(resultado["RANGO_INICIAL"]);
                            if (resultado["RANGO_FINAL"] != DBNull.Value) entidad.rango_final = Convert.ToInt64(resultado["RANGO_FINAL"]);

                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["TIPO_CONSECUTIVO"] != DBNull.Value) entidad.tipo_consecutivo = Convert.ToInt64(resultado["TIPO_CONSECUTIVO"]);
                            if (resultado["RANGO_INICIAL"] != DBNull.Value) entidad.rango_inicial = Convert.ToInt64(resultado["RANGO_INICIAL"]);
                            if (resultado["RANGO_FINAL"] != DBNull.Value) entidad.rango_final = Convert.ToInt64(resultado["RANGO_FINAL"]);

                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUARIOULTMOD"] != DBNull.Value) entidad.usuarioultmod = Convert.ToString(resultado["USUARIOULTMOD"]);
                        }
                      
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConsecutivoOficinaData", "ConsultarConsOfiXOfyTabla", ex);
                        return null;
                    }
                }
            }
        }


        public List<ConsecutivoOficinas> ListarConsecutivoOficinas(String filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ConsecutivoOficinas> lstOficina = new List<ConsecutivoOficinas>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECt c.*,o.nombre as NOMOFICINA FROM CONSECUTIVO_OFICINAS c  inner join oficina o on c.cod_oficina=o.cod_oficina" + filtro + " ORDER BY IDCONSECUTIVO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ConsecutivoOficinas entidad = new ConsecutivoOficinas();
                            if (resultado["IDCONSECUTIVO"] != DBNull.Value) entidad.idconsecutivo = Convert.ToInt32(resultado["IDCONSECUTIVO"]);
                            if (resultado["TABLA"] != DBNull.Value) entidad.tabla = Convert.ToString(resultado["TABLA"]);
                            if (resultado["COLUMNA"] != DBNull.Value) entidad.columna = Convert.ToString(resultado["COLUMNA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOMOFICINA"]);

                            if (resultado["TIPO_CONSECUTIVO"] != DBNull.Value) entidad.tipo_consecutivo = Convert.ToInt32(resultado["TIPO_CONSECUTIVO"]);
                            if (resultado["RANGO_INICIAL"] != DBNull.Value) entidad.rango_inicial = Convert.ToInt64(resultado["RANGO_INICIAL"]);
                            if (resultado["RANGO_FINAL"] != DBNull.Value) entidad.rango_final = Convert.ToInt64(resultado["RANGO_FINAL"]);

                            if (resultado["TIPO_CONSECUTIVO"] != DBNull.Value) entidad.tipo_consecutivo = Convert.ToInt64(resultado["TIPO_CONSECUTIVO"]);
                            if (resultado["RANGO_INICIAL"] != DBNull.Value) entidad.rango_inicial = Convert.ToInt64(resultado["RANGO_INICIAL"]);
                            if (resultado["RANGO_FINAL"] != DBNull.Value) entidad.rango_final = Convert.ToInt64(resultado["RANGO_FINAL"]);

                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUARIOULTMOD"] != DBNull.Value) entidad.usuarioultmod = Convert.ToString(resultado["USUARIOULTMOD"]);
                            lstOficina.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstOficina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConsecutivoOficinaData", "ListarConsecutivoOficinas", ex);
                        return null;
                    }
                }
            }
        }


    }
}