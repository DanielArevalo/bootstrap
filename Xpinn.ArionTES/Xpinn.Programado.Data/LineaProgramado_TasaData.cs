using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Programado.Entities;

namespace Xpinn.Programado.Data
{
   public class LineaProgramado_TasaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public LineaProgramado_TasaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public LineaProgramado_Tasa CrearLineaProgramado_tasa(LineaProgramado_Tasa pLineaProgramado_tasa, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidtasa = cmdTransaccionFactory.CreateParameter();
                        pidtasa.ParameterName = "p_idtasa";
                        pidtasa.Value = pLineaProgramado_tasa.idtasa;
                        pidtasa.Direction = ParameterDirection.Output;
                        pidtasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidtasa);

                        DbParameter pidrango = cmdTransaccionFactory.CreateParameter();
                        pidrango.ParameterName = "p_idrango";
                        pidrango.Value = pLineaProgramado_tasa.idrango;
                        pidrango.Direction = ParameterDirection.Input;
                        pidrango.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidrango);

                        DbParameter pcod_linea_programado = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_programado.ParameterName = "p_cod_linea_programado";
                        pcod_linea_programado.Value = pLineaProgramado_tasa.cod_linea_programado;
                        pcod_linea_programado.Direction = ParameterDirection.Input;
                        pcod_linea_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_programado);

                        DbParameter ptipo_interes = cmdTransaccionFactory.CreateParameter();
                        ptipo_interes.ParameterName = "p_tipo_interes";
                        ptipo_interes.Value = pLineaProgramado_tasa.tipo_interes;
                        ptipo_interes.Direction = ParameterDirection.Input;
                        ptipo_interes.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_interes);

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "p_tasa";
                        if (pLineaProgramado_tasa.tasa == null)
                            ptasa.Value = DBNull.Value;
                        else
                            ptasa.Value = pLineaProgramado_tasa.tasa;
                        ptasa.Direction = ParameterDirection.Input;
                        ptasa.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa);

                        DbParameter pcod_tipo_tasa = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_tasa.ParameterName = "p_cod_tipo_tasa";
                        if (pLineaProgramado_tasa.cod_tipo_tasa == null)
                            pcod_tipo_tasa.Value = DBNull.Value;
                        else
                            pcod_tipo_tasa.Value = pLineaProgramado_tasa.cod_tipo_tasa;
                        pcod_tipo_tasa.Direction = ParameterDirection.Input;
                        pcod_tipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_tasa);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pLineaProgramado_tasa.tipo_historico == null)
                            ptipo_historico.Value = DBNull.Value;
                        else
                            ptipo_historico.Value = pLineaProgramado_tasa.tipo_historico;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviación = cmdTransaccionFactory.CreateParameter();
                        pdesviación.ParameterName = "p_desviacion";
                        if (pLineaProgramado_tasa.desviación == null)
                            pdesviación.Value = DBNull.Value;
                        else
                            pdesviación.Value = pLineaProgramado_tasa.desviación;
                        pdesviación.Direction = ParameterDirection.Input;
                        pdesviación.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviación);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_LINEAPRO_T_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        pLineaProgramado_tasa.idtasa = int.Parse(pidtasa.Value.ToString());
                        return pLineaProgramado_tasa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaProgramado_tasaData", "CrearLineaProgramado_tasa", ex);
                        return null;
                    }
                }
            }
        }


        public LineaProgramado_Tasa ModificarLineaProgramado_tasa(LineaProgramado_Tasa pLineaProgramado_tasa, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidtasa = cmdTransaccionFactory.CreateParameter();
                        pidtasa.ParameterName = "p_idtasa";
                        pidtasa.Value = pLineaProgramado_tasa.idtasa;
                        pidtasa.Direction = ParameterDirection.Input;
                        pidtasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidtasa);

                        DbParameter pidrango = cmdTransaccionFactory.CreateParameter();
                        pidrango.ParameterName = "p_idrango";
                        pidrango.Value = pLineaProgramado_tasa.idrango;
                        pidrango.Direction = ParameterDirection.Input;
                        pidrango.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidrango);

                        DbParameter pcod_linea_programado = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_programado.ParameterName = "p_cod_linea_programado";
                        pcod_linea_programado.Value = pLineaProgramado_tasa.cod_linea_programado;
                        pcod_linea_programado.Direction = ParameterDirection.Input;
                        pcod_linea_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_programado);

                        DbParameter ptipo_interes = cmdTransaccionFactory.CreateParameter();
                        ptipo_interes.ParameterName = "p_tipo_interes";
                        ptipo_interes.Value = pLineaProgramado_tasa.tipo_interes;
                        ptipo_interes.Direction = ParameterDirection.Input;
                        ptipo_interes.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_interes);

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "p_tasa";
                        if (pLineaProgramado_tasa.tasa == null)
                            ptasa.Value = DBNull.Value;
                        else
                            ptasa.Value = pLineaProgramado_tasa.tasa;
                        ptasa.Direction = ParameterDirection.Input;
                        ptasa.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa);

                        DbParameter pcod_tipo_tasa = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_tasa.ParameterName = "p_cod_tipo_tasa";
                        if (pLineaProgramado_tasa.cod_tipo_tasa == null)
                            pcod_tipo_tasa.Value = DBNull.Value;
                        else
                            pcod_tipo_tasa.Value = pLineaProgramado_tasa.cod_tipo_tasa;
                        pcod_tipo_tasa.Direction = ParameterDirection.Input;
                        pcod_tipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_tasa);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pLineaProgramado_tasa.tipo_historico == null)
                            ptipo_historico.Value = DBNull.Value;
                        else
                            ptipo_historico.Value = pLineaProgramado_tasa.tipo_historico;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviación = cmdTransaccionFactory.CreateParameter();
                        pdesviación.ParameterName = "p_desviacion";
                        if (pLineaProgramado_tasa.desviación == null)
                            pdesviación.Value = DBNull.Value;
                        else
                            pdesviación.Value = pLineaProgramado_tasa.desviación;
                        pdesviación.Direction = ParameterDirection.Input;
                        pdesviación.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviación);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_LINEAPRO_T_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLineaProgramado_tasa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaProgramado_tasaData", "ModificarLineaProgramado_tasa", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarLineaProgramado_tasa(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        LineaProgramado_Tasa pLineaProgramado_tasa = new LineaProgramado_Tasa();
                       
                        DbParameter pidtasa = cmdTransaccionFactory.CreateParameter();
                        pidtasa.ParameterName = "p_idtasa";
                        pidtasa.Value = pLineaProgramado_tasa.idtasa;
                        pidtasa.Direction = ParameterDirection.Input;
                        pidtasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidtasa);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_LINEAPROGR_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaProgramado_tasaData", "EliminarLineaProgramado_tasa", ex);
                    }
                }
            }
        }


        public LineaProgramado_Tasa ConsultarLineaProgramado_tasa(Int64 pId_rango, string p_idlinea, Usuario vUsuario)
        {
            DbDataReader resultado;
            LineaProgramado_Tasa entidad = new LineaProgramado_Tasa();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM LINEAPROGRAMADO_TASA WHERE IDRANGO = " + pId_rango.ToString() + " AND COD_LINEA_PROGRAMADO='"+ p_idlinea + "'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDTASA"] != DBNull.Value) entidad.idtasa = Convert.ToInt32(resultado["IDTASA"]);
                            if (resultado["IDRANGO"] != DBNull.Value) entidad.idrango = Convert.ToInt32(resultado["IDRANGO"]);
                            if (resultado["COD_LINEA_PROGRAMADO"] != DBNull.Value) entidad.cod_linea_programado = Convert.ToString(resultado["COD_LINEA_PROGRAMADO"]);
                            if (resultado["TIPO_INTERES"] != DBNull.Value) entidad.tipo_interes = Convert.ToInt32(resultado["TIPO_INTERES"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["COD_TIPO_TASA"] != DBNull.Value) entidad.cod_tipo_tasa = Convert.ToInt32(resultado["COD_TIPO_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado[7]!= DBNull.Value) entidad.desviación = Convert.ToDecimal(resultado[7]);
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
                        BOExcepcion.Throw("LineaProgramado_tasaData", "ConsultarLineaProgramado_tasa", ex);
                        return null;
                    }
                }
            }
        }


        public List<LineaProgramado_Tasa> ListarLineaProgramado_tasa(LineaProgramado_Tasa pLineaProgramado_tasa, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LineaProgramado_Tasa> lstLineaProgramado_tasa = new List<LineaProgramado_Tasa>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM LINEAPROGRAMADO_TASA " + ObtenerFiltro(pLineaProgramado_tasa) + " ORDER BY IDTASA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LineaProgramado_Tasa entidad = new LineaProgramado_Tasa();
                            if (resultado["IDTASA"] != DBNull.Value) entidad.idtasa = Convert.ToInt32(resultado["IDTASA"]);
                            if (resultado["IDRANGO"] != DBNull.Value) entidad.idrango = Convert.ToInt32(resultado["IDRANGO"]);
                            if (resultado["COD_LINEA_PROGRAMADO"] != DBNull.Value) entidad.cod_linea_programado = Convert.ToString(resultado["COD_LINEA_PROGRAMADO"]);
                            if (resultado["TIPO_INTERES"] != DBNull.Value) entidad.tipo_interes = Convert.ToInt32(resultado["TIPO_INTERES"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["COD_TIPO_TASA"] != DBNull.Value) entidad.cod_tipo_tasa = Convert.ToInt32(resultado["COD_TIPO_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACIÓN"] != DBNull.Value) entidad.desviación = Convert.ToDecimal(resultado["DESVIACIÓN"]);
                            lstLineaProgramado_tasa.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineaProgramado_tasa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaProgramado_tasaData", "ListarLineaProgramado_tasa", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarLineaProgramado_tasas(String pId_tasas, String pIdLinea, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                      
                        DbParameter p_idtasas = cmdTransaccionFactory.CreateParameter();
                        p_idtasas.ParameterName = "p_idrangos";
                        p_idtasas.Value = pId_tasas;
                        p_idtasas.Direction = ParameterDirection.Input;
                        p_idtasas.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_idtasas);

                        DbParameter p_cod_linea_programado = cmdTransaccionFactory.CreateParameter();
                        p_cod_linea_programado.ParameterName = "p_cod_linea_programado";
                        p_cod_linea_programado.Value = pIdLinea;
                        p_cod_linea_programado.Direction = ParameterDirection.Input;
                        p_cod_linea_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cod_linea_programado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_LINEAPR_T_DEL";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaProgramado_tasaData", "EliminarLineaProgramado_tasa", ex);
                    }
                }
            }
        }
    }
}
