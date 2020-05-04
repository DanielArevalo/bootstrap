using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Riesgo.Entities;

namespace Xpinn.Riesgo.Data
{
    public class RangoPerfilData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public RangoPerfilData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<RangoPerfil> ListarRangosPerfil(RangoPerfil rangos, Usuario usuario)
        {
            DbDataReader resultado;
            List<RangoPerfil> listaRangosPerfiles = new List<RangoPerfil>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        sql = @"SELECT * FROM GR_RANGO_PERFIL ORDER BY COD_RANGO_PERFIL";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RangoPerfil rango = new RangoPerfil();
                            if (resultado["COD_RANGO_PERFIL"] != DBNull.Value) rango.cod_rango_perfil = Convert.ToInt64(resultado["COD_RANGO_PERFIL"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) rango.calificacion = Convert.ToInt64(resultado["CALIFICACION"]);
                            if (resultado["RANGO_MINIMO"] != DBNull.Value) rango.rango_minimo = Convert.ToInt32(resultado["RANGO_MINIMO"]);
                            if (resultado["RANGO_MAXIMO"] != DBNull.Value) rango.rango_maximo = Convert.ToInt32(resultado["RANGO_MAXIMO"]);
                            if (resultado["COD_MONITOREO"] != DBNull.Value) rango.cod_monitoreo = Convert.ToInt32(resultado["COD_MONITOREO"]);
                            listaRangosPerfiles.Add(rango);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaRangosPerfiles;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RangoPerfilData", "ListarRangosPerfil", ex);
                        return null;
                    }
                }
            }
        }
        public RangoPerfil ModificarRangoPerfil(RangoPerfil eachRango, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_calificacion = cmdTransaccionFactory.CreateParameter();
                        p_calificacion.ParameterName = "P_CALIFICACION";
                        p_calificacion.Value = eachRango.calificacion;
                        p_calificacion.Direction = ParameterDirection.Input;
                        p_calificacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_calificacion);

                        DbParameter p_cod_rango_perfil = cmdTransaccionFactory.CreateParameter();
                        p_cod_rango_perfil.ParameterName = "P_COD_RANGO_PERFIL";
                        p_cod_rango_perfil.Value = eachRango.cod_rango_perfil;
                        p_cod_rango_perfil.Direction = ParameterDirection.Input;
                        p_cod_rango_perfil.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_rango_perfil);

                        DbParameter p_rango_minimo = cmdTransaccionFactory.CreateParameter();
                        p_rango_minimo.ParameterName = "P_RANGO_MINIMO";
                        p_rango_minimo.Value = eachRango.rango_minimo;
                        p_rango_minimo.Direction = ParameterDirection.Input;
                        p_rango_minimo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_rango_minimo);

                        DbParameter p_rango_maximo = cmdTransaccionFactory.CreateParameter();
                        p_rango_maximo.ParameterName = "P_RANGO_MAXIMO";
                        p_rango_maximo.Value = eachRango.rango_maximo;
                        p_rango_maximo.Direction = ParameterDirection.Input;
                        p_rango_maximo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_rango_maximo);

                        DbParameter p_cod_monitoreo = cmdTransaccionFactory.CreateParameter();
                        p_cod_monitoreo.ParameterName = "P_COD_MONITOREO";
                        p_cod_monitoreo.Value = eachRango.cod_monitoreo;
                        p_cod_monitoreo.Direction = ParameterDirection.Input;
                        p_cod_monitoreo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_monitoreo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GR_RANGO_RANGO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        
                        return eachRango;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RangoPerfilData", "ModificarRangoPerfil", ex);
                        return null;
                    }
                }
            }
        }
    }
}
