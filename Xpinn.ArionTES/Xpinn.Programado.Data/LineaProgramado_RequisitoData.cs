using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Programado.Entities;

namespace Xpinn.Programado.Data
{
    public class LineaProgramado_RequisitoData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public LineaProgramado_RequisitoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public LineaProgramado_Requisito CrearLineaProgramado_Requisito(LineaProgramado_Requisito pLineaProgramado_Requisito, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidrequisito = cmdTransaccionFactory.CreateParameter();
                        pidrequisito.ParameterName = "p_idrequisito";
                        pidrequisito.Value = pLineaProgramado_Requisito.idrequisito;
                        pidrequisito.Direction = ParameterDirection.Output;
                        pidrequisito.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidrequisito);

                        DbParameter pidrango = cmdTransaccionFactory.CreateParameter();
                        pidrango.ParameterName = "p_idrango";
                        pidrango.Value = pLineaProgramado_Requisito.idrango;
                        pidrango.Direction = ParameterDirection.Input;
                        pidrango.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidrango);

                        DbParameter pcod_linea_programado = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_programado.ParameterName = "p_cod_linea_programado";
                        pcod_linea_programado.Value = pLineaProgramado_Requisito.cod_linea_programado;
                        pcod_linea_programado.Direction = ParameterDirection.Input;
                        pcod_linea_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_programado);

                        DbParameter ptipo_tope = cmdTransaccionFactory.CreateParameter();
                        ptipo_tope.ParameterName = "p_tipo_tope";
                        ptipo_tope.Value = pLineaProgramado_Requisito.tipo_tope;
                        ptipo_tope.Direction = ParameterDirection.Input;
                        ptipo_tope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tope);

                        DbParameter pminimo = cmdTransaccionFactory.CreateParameter();
                        pminimo.ParameterName = "p_minimo";
                        if (pLineaProgramado_Requisito.minimo == null)
                            pminimo.Value = DBNull.Value;
                        else
                            pminimo.Value = pLineaProgramado_Requisito.minimo;
                        pminimo.Direction = ParameterDirection.Input;
                        pminimo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pminimo);

                        DbParameter pmaximo = cmdTransaccionFactory.CreateParameter();
                        pmaximo.ParameterName = "p_maximo";
                        if (pLineaProgramado_Requisito.maximo == null)
                            pmaximo.Value = DBNull.Value;
                        else
                            pmaximo.Value = pLineaProgramado_Requisito.maximo;
                        pmaximo.Direction = ParameterDirection.Input;
                        pmaximo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmaximo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_LINEAPR_RE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        pLineaProgramado_Requisito.idrequisito = Convert.ToInt64(pidrequisito.Value);
                        return pLineaProgramado_Requisito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaProgramado_RequisitoData", "CrearLineaProgramado_Requisito", ex);
                        return null;
                    }
                }
            }
        }


        public LineaProgramado_Requisito ModificarLineaProgramado_Requisito(LineaProgramado_Requisito pLineaProgramado_Requisito, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidrequisito = cmdTransaccionFactory.CreateParameter();
                        pidrequisito.ParameterName = "p_idrequisito";
                        pidrequisito.Value = pLineaProgramado_Requisito.idrequisito;
                        pidrequisito.Direction = ParameterDirection.Input;
                        pidrequisito.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidrequisito);

                        DbParameter pidrango = cmdTransaccionFactory.CreateParameter();
                        pidrango.ParameterName = "p_idrango";
                        pidrango.Value = pLineaProgramado_Requisito.idrango;
                        pidrango.Direction = ParameterDirection.Input;
                        pidrango.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidrango);

                        DbParameter pcod_linea_programado = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_programado.ParameterName = "p_cod_linea_programado";
                        pcod_linea_programado.Value = pLineaProgramado_Requisito.cod_linea_programado;
                        pcod_linea_programado.Direction = ParameterDirection.Input;
                        pcod_linea_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_programado);

                        DbParameter ptipo_tope = cmdTransaccionFactory.CreateParameter();
                        ptipo_tope.ParameterName = "p_tipo_tope";
                        ptipo_tope.Value = pLineaProgramado_Requisito.tipo_tope;
                        ptipo_tope.Direction = ParameterDirection.Input;
                        ptipo_tope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tope);

                        DbParameter pminimo = cmdTransaccionFactory.CreateParameter();
                        pminimo.ParameterName = "p_minimo";
                        if (pLineaProgramado_Requisito.minimo == null)
                            pminimo.Value = DBNull.Value;
                        else
                            pminimo.Value = pLineaProgramado_Requisito.minimo;
                        pminimo.Direction = ParameterDirection.Input;
                        pminimo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pminimo);

                        DbParameter pmaximo = cmdTransaccionFactory.CreateParameter();
                        pmaximo.ParameterName = "p_maximo";
                        if (pLineaProgramado_Requisito.maximo == null)
                            pmaximo.Value = DBNull.Value;
                        else
                            pmaximo.Value = pLineaProgramado_Requisito.maximo;
                        pmaximo.Direction = ParameterDirection.Input;
                        pmaximo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmaximo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_LINEAPR_RE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLineaProgramado_Requisito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaProgramado_RequisitoData", "ModificarLineaProgramado_Requisito", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarLineaProgramado_Requisito(Int64 pId , Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        LineaProgramado_Requisito pLineaProgramado_Requisito = new LineaProgramado_Requisito();
                        pLineaProgramado_Requisito = ConsultarLineaProgramado_Requisito(pId, vUsuario);

                        DbParameter pidrequisito = cmdTransaccionFactory.CreateParameter();
                        pidrequisito.ParameterName = "p_idrequisito";
                        pidrequisito.Value = pLineaProgramado_Requisito.idrequisito;
                        pidrequisito.Direction = ParameterDirection.Input;
                        pidrequisito.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidrequisito);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_LINEAPROGR_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaProgramado_RequisitoData", "EliminarLineaProgramado_Requisito", ex);
                    }
                }
            }
        }


        public void EliminarLineaProgramado_Requisitos(String pId, Int64 pId_rango,String pId_linea, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                                            
                        DbParameter pidrequisitos = cmdTransaccionFactory.CreateParameter();
                        pidrequisitos.ParameterName = "p_idrequisitos";
                        pidrequisitos.Value = pId;
                        pidrequisitos.Direction = ParameterDirection.Input;
                        pidrequisitos.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidrequisitos);

                        DbParameter p_idrango = cmdTransaccionFactory.CreateParameter();
                        p_idrango.ParameterName = "p_idrango";
                        p_idrango.Value = pId_rango;
                        p_idrango.Direction = ParameterDirection.Input;
                        p_idrango.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_idrango);

                        DbParameter p_idlinea = cmdTransaccionFactory.CreateParameter();
                        p_idlinea.ParameterName = "p_cod_linea_programado";
                        p_idlinea.Value = pId_linea;
                        p_idlinea.Direction = ParameterDirection.Input;
                        p_idlinea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_idlinea);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_LINEAPR_RE_DEL";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaProgramado_RequisitoData", "EliminarLineaProgramado_Requisito", ex);
                    }
                }
            }
        }


        public LineaProgramado_Requisito ConsultarLineaProgramado_Requisito(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            LineaProgramado_Requisito entidad = new LineaProgramado_Requisito();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM LINEAPROGRAMADO_REQUISITO WHERE IDREQUISITO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDREQUISITO"] != DBNull.Value) entidad.idrequisito = Convert.ToInt32(resultado["IDREQUISITO"]);
                            if (resultado["IDRANGO"] != DBNull.Value) entidad.idrango = Convert.ToInt32(resultado["IDRANGO"]);
                            if (resultado["COD_LINEA_PROGRAMADO"] != DBNull.Value) entidad.cod_linea_programado = Convert.ToString(resultado["COD_LINEA_PROGRAMADO"]);
                            if (resultado["TIPO_TOPE"] != DBNull.Value) entidad.tipo_tope = Convert.ToInt32(resultado["TIPO_TOPE"]);
                            if (resultado["MINIMO"] != DBNull.Value) entidad.minimo = Convert.ToString(resultado["MINIMO"]);
                            if (resultado["MAXIMO"] != DBNull.Value) entidad.maximo = Convert.ToString(resultado["MAXIMO"]);
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
                        BOExcepcion.Throw("LineaProgramado_RequisitoData", "ConsultarLineaProgramado_Requisito", ex);
                        return null;
                    }
                }
            }
        }


        public List<LineaProgramado_Requisito> ListarLineaProgramado_Requisito(Int64 pId_rango, string p_idlinea, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LineaProgramado_Requisito> lstLineaProgramado_Requisito = new List<LineaProgramado_Requisito>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM LINEAPROGRAMADO_REQUISITO WHERE IDRANGO= "+ pId_rango + " AND COD_LINEA_PROGRAMADO='"+ p_idlinea + "'  ORDER BY IDREQUISITO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LineaProgramado_Requisito entidad = new LineaProgramado_Requisito();
                            if (resultado["IDREQUISITO"] != DBNull.Value) entidad.idrequisito = Convert.ToInt32(resultado["IDREQUISITO"]);
                            if (resultado["IDRANGO"] != DBNull.Value) entidad.idrango = Convert.ToInt32(resultado["IDRANGO"]);
                            if (resultado["COD_LINEA_PROGRAMADO"] != DBNull.Value) entidad.cod_linea_programado = Convert.ToString(resultado["COD_LINEA_PROGRAMADO"]);
                            if (resultado["TIPO_TOPE"] != DBNull.Value) entidad.tipo_tope = Convert.ToInt32(resultado["TIPO_TOPE"]);
                            if (resultado["MINIMO"] != DBNull.Value) entidad.minimo = Convert.ToString(resultado["MINIMO"]);
                            if (resultado["MAXIMO"] != DBNull.Value) entidad.maximo = Convert.ToString(resultado["MAXIMO"]);
                            lstLineaProgramado_Requisito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineaProgramado_Requisito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaProgramado_RequisitoData", "ListarLineaProgramado_Requisito", ex);
                        return null;
                    }
                }
            }
        }



        public LineaProgramado_Requisito ConsultarLineaProgramadoRango(Int64 pId_plazo, string p_idlinea, Usuario vUsuario)
        {
            DbDataReader resultado;
            Int64 pplazo = pId_plazo;
           LineaProgramado_Requisito entidad = new LineaProgramado_Requisito();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM LINEAPROGRAMADO_REQUISITO WHERE  tipo_tope=2 and  minimo <=" + pId_plazo + " and MAXIMO>= " + pId_plazo + "AND COD_LINEA_PROGRAMADO='" + p_idlinea + "'  ORDER BY IDREQUISITO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                           
                            if (resultado["IDREQUISITO"] != DBNull.Value) entidad.idrequisito = Convert.ToInt32(resultado["IDREQUISITO"]);
                            if (resultado["IDRANGO"] != DBNull.Value) entidad.idrango = Convert.ToInt32(resultado["IDRANGO"]);
                            if (resultado["COD_LINEA_PROGRAMADO"] != DBNull.Value) entidad.cod_linea_programado = Convert.ToString(resultado["COD_LINEA_PROGRAMADO"]);
                            if (resultado["TIPO_TOPE"] != DBNull.Value) entidad.tipo_tope = Convert.ToInt32(resultado["TIPO_TOPE"]);
                            if (resultado["MINIMO"] != DBNull.Value) entidad.minimo = Convert.ToString(resultado["MINIMO"]);
                            if (resultado["MAXIMO"] != DBNull.Value) entidad.maximo = Convert.ToString(resultado["MAXIMO"]);
                          
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaProgramado_RequisitoData", "ConsultarLineaProgramadoRango", ex);
                        return null;
                    }
                }
            }
        }

    }
}