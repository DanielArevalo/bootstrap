using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Programado.Entities;

namespace Xpinn.Programado.Data
{
    public class LineaProgramado_RangoData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public LineaProgramado_RangoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public LineaProgramado_Rango CrearLineaProgramado_Rango(LineaProgramado_Rango pLineaProgramado_Rango, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidrango = cmdTransaccionFactory.CreateParameter();
                        pidrango.ParameterName = "p_idrango";
                        pidrango.Value = pLineaProgramado_Rango.idrango;
                        pidrango.Direction = ParameterDirection.Output;
                        pidrango.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidrango);

                        DbParameter pcod_linea_programado = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_programado.ParameterName = "p_cod_linea_programado";
                        pcod_linea_programado.Value = pLineaProgramado_Rango.cod_linea_programado;
                        pcod_linea_programado.Direction = ParameterDirection.Input;
                        pcod_linea_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_programado);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pLineaProgramado_Rango.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_LINEAPRO_R_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        pLineaProgramado_Rango.idrango = int.Parse(pidrango.Value.ToString());
                        return pLineaProgramado_Rango;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaProgramado_RangoData", "CrearLineaProgramado_Rango", ex);
                        return null;
                    }
                }
            }
        }


        public LineaProgramado_Rango ModificarLineaProgramado_Rango(LineaProgramado_Rango pLineaProgramado_Rango, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidrango = cmdTransaccionFactory.CreateParameter();
                        pidrango.ParameterName = "p_idrango";
                        pidrango.Value = pLineaProgramado_Rango.idrango;
                        pidrango.Direction = ParameterDirection.Input;
                        pidrango.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidrango);

                        DbParameter pcod_linea_programado = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_programado.ParameterName = "p_cod_linea_programado";
                        pcod_linea_programado.Value = pLineaProgramado_Rango.cod_linea_programado;
                        pcod_linea_programado.Direction = ParameterDirection.Input;
                        pcod_linea_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_programado);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pLineaProgramado_Rango.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_LINEAPRO_R_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLineaProgramado_Rango;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaProgramado_RangoData", "ModificarLineaProgramado_Rango", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarLineaProgramado_Rango(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        LineaProgramado_Rango pLineaProgramado_Rango = new LineaProgramado_Rango();
                        pLineaProgramado_Rango = ConsultarLineaProgramado_Rango(pId, vUsuario);

                        DbParameter pidrango = cmdTransaccionFactory.CreateParameter();
                        pidrango.ParameterName = "p_idrango";
                        pidrango.Value = pLineaProgramado_Rango.idrango;
                        pidrango.Direction = ParameterDirection.Input;
                        pidrango.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidrango);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_LINEAPROGR_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaProgramado_RangoData", "EliminarLineaProgramado_Rango", ex);
                    }
                }
            }
        }


        public LineaProgramado_Rango ConsultarLineaProgramado_Rango(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            LineaProgramado_Rango entidad = new LineaProgramado_Rango();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM LINEAPROGRAMADO_RANGO WHERE IDRANGO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDRANGO"] != DBNull.Value) entidad.idrango = Convert.ToInt32(resultado["IDRANGO"]);
                            if (resultado["COD_LINEA_PROGRAMADO"] != DBNull.Value) entidad.cod_linea_programado = Convert.ToString(resultado["COD_LINEA_PROGRAMADO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
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
                        BOExcepcion.Throw("LineaProgramado_RangoData", "ConsultarLineaProgramado_Rango", ex);
                        return null;
                    }
                }
            }
        }


        public List<LineaProgramado_Rango> ListarLineaProgramado_Rango(Int64 id_linea,Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LineaProgramado_Rango> lstLineaProgramado_Rango = new List<LineaProgramado_Rango>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM LINEAPROGRAMADO_RANGO WHERE COD_LINEA_PROGRAMADO = '"+ id_linea + "' ORDER BY IDRANGO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LineaProgramado_Rango entidad = new LineaProgramado_Rango();
                            if (resultado["IDRANGO"] != DBNull.Value) entidad.idrango = Convert.ToInt32(resultado["IDRANGO"]);
                            if (resultado["COD_LINEA_PROGRAMADO"] != DBNull.Value) entidad.cod_linea_programado = Convert.ToString(resultado["COD_LINEA_PROGRAMADO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstLineaProgramado_Rango.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineaProgramado_Rango;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaProgramado_RangoData", "ListarLineaProgramado_Rango", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarLineaProgramado_Rangos(String pId_rangos, String pIdLinea , Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                       

                        DbParameter pidrangos = cmdTransaccionFactory.CreateParameter();
                        pidrangos.ParameterName = "p_idrangos";
                        pidrangos.Value = pId_rangos;
                        pidrangos.Direction = ParameterDirection.Input;
                        pidrangos.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidrangos);


                        DbParameter p_cod_linea_programado = cmdTransaccionFactory.CreateParameter();
                        p_cod_linea_programado.ParameterName = "p_cod_linea_programado";
                        p_cod_linea_programado.Value = pIdLinea;
                        p_cod_linea_programado.Direction = ParameterDirection.Input;
                        p_cod_linea_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cod_linea_programado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_LINEAPR_R_DEL";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaProgramado_RangoData", "EliminarLineaProgramado_Rango", ex);
                    }
                }
            }
        }
    }
}