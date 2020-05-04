using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Riesgo.Entities;

namespace Xpinn.Riesgo.Data
{
    public class AlertasData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public AlertasData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<alertas_ries> Listaralertas(alertas_ries palertas, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<alertas_ries> lstalertas = new List<alertas_ries>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM GR_ALERTAS " + ObtenerFiltro(palertas) + " ORDER BY COD_ALERTA";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            alertas_ries entidad = new alertas_ries();

                            if (resultado["COD_ALERTA"] != DBNull.Value) entidad.Cod_Alerta = Convert.ToInt64(resultado["COD_ALERTA"]);
                            if (resultado["NOM_ALERTA"] != DBNull.Value) entidad.Nom_Alerta = Convert.ToString(resultado["NOM_ALERTA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["PERIOCIDAD"] != DBNull.Value) entidad.Periocidad = Convert.ToString(resultado["PERIOCIDAD"]);
                            if (resultado["SENTENCIA_SQL"] != DBNull.Value) entidad.Sentencia_Sql = Convert.ToString(resultado["SENTENCIA_SQL"]);
                            if (resultado["INDICADOR"] != DBNull.Value) entidad.Indicador = Convert.ToString(resultado["INDICADOR"]);
                            lstalertas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstalertas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("alertasData", "Listaralertas", ex);
                        return null;
                    }
                }
            }
        }
        public alertas_ries Crearalertas(alertas_ries palertas, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_alerta = cmdTransaccionFactory.CreateParameter();
                        p_cod_alerta.ParameterName = "p_cod_alerta";
                        p_cod_alerta.Value = palertas.Cod_Alerta;
                        p_cod_alerta.Direction = ParameterDirection.InputOutput;
                        p_cod_alerta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_alerta);

                        DbParameter p_nom_alerta = cmdTransaccionFactory.CreateParameter();
                        p_nom_alerta.ParameterName = "p_nom_alerta";
                        p_nom_alerta.Value = palertas.Nom_Alerta;
                        p_nom_alerta.Direction = ParameterDirection.Input;
                        p_nom_alerta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_nom_alerta);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = palertas.Descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_periocidad = cmdTransaccionFactory.CreateParameter();
                        p_periocidad.ParameterName = "p_periocidad";
                        p_periocidad.Value = palertas.Periocidad;
                        p_periocidad.Direction = ParameterDirection.Input;
                        p_periocidad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_periocidad);

                        DbParameter p_sentencia_sql = cmdTransaccionFactory.CreateParameter();
                        p_sentencia_sql.ParameterName = "p_sentecia_sql";
                        p_sentencia_sql.Value = palertas.Sentencia_Sql;
                        p_sentencia_sql.Direction = ParameterDirection.Input;
                        p_sentencia_sql.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_sentencia_sql);
                        
                        DbParameter p_indicador = cmdTransaccionFactory.CreateParameter();
                        p_indicador.ParameterName = "p_indicador";
                        p_indicador.Value = palertas.Indicador;
                        p_indicador.Direction = ParameterDirection.Input;
                        p_indicador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_indicador);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GR_GR_ALERTAS_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return palertas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("alertasData", "Crearalertas", ex);
                        return null;
                    }
                }
            }
        }
        public alertas_ries Modificaralertas(alertas_ries palertas, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_alerta = cmdTransaccionFactory.CreateParameter();
                        p_cod_alerta.ParameterName = "p_cod_alerta";
                        p_cod_alerta.Value = palertas.Cod_Alerta;
                        p_cod_alerta.Direction = ParameterDirection.InputOutput;
                        p_cod_alerta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_alerta);

                        DbParameter p_nom_alerta = cmdTransaccionFactory.CreateParameter();
                        p_nom_alerta.ParameterName = "p_nom_alerta";
                        p_nom_alerta.Value = palertas.Nom_Alerta;
                        p_nom_alerta.Direction = ParameterDirection.Input;
                        p_nom_alerta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_nom_alerta);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = palertas.Descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_periocidad = cmdTransaccionFactory.CreateParameter();
                        p_periocidad.ParameterName = "p_periocidad";
                        p_periocidad.Value = palertas.Periocidad;
                        p_periocidad.Direction = ParameterDirection.Input;
                        p_periocidad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_periocidad);

                        DbParameter p_sentencia_sql = cmdTransaccionFactory.CreateParameter();
                        p_sentencia_sql.ParameterName = "p_sentecia_sql";
                        p_sentencia_sql.Value = palertas.Sentencia_Sql;
                        p_sentencia_sql.Direction = ParameterDirection.Input;
                        p_sentencia_sql.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_sentencia_sql);

                        DbParameter p_indicador = cmdTransaccionFactory.CreateParameter();
                        p_indicador.ParameterName = "p_indicador";
                        p_indicador.Value = palertas.Indicador;
                        p_indicador.Direction = ParameterDirection.Input;
                        p_indicador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_indicador);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GR_GR_ALERTAS_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return palertas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("alertasData", "Modificaralertas", ex);
                        return null;
                    }
                }
            }
        }


        public void Eliminaralertas(alertas_ries palertas, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_alerta = cmdTransaccionFactory.CreateParameter();
                        p_cod_alerta.ParameterName = "p_cod_alerta";
                        p_cod_alerta.Value = palertas.Cod_Alerta;
                        p_cod_alerta.Direction = ParameterDirection.InputOutput;
                        p_cod_alerta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_alerta);

                        DbParameter p_nom_alerta = cmdTransaccionFactory.CreateParameter();
                        p_nom_alerta.ParameterName = "p_nom_alerta";
                        p_nom_alerta.Value = palertas.Nom_Alerta;
                        p_nom_alerta.Direction = ParameterDirection.Input;
                        p_nom_alerta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_nom_alerta);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = palertas.Descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_periocidad = cmdTransaccionFactory.CreateParameter();
                        p_periocidad.ParameterName = "p_periocidad";
                        p_periocidad.Value = palertas.Periocidad;
                        p_periocidad.Direction = ParameterDirection.Input;
                        p_periocidad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_periocidad);

                        DbParameter p_sentencia_sql = cmdTransaccionFactory.CreateParameter();
                        p_sentencia_sql.ParameterName = "p_sentecia_sql";
                        p_sentencia_sql.Value = palertas.Sentencia_Sql;
                        p_sentencia_sql.Direction = ParameterDirection.Input;
                        p_sentencia_sql.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_sentencia_sql);

                        DbParameter p_indicador = cmdTransaccionFactory.CreateParameter();
                        p_indicador.ParameterName = "p_indicador";
                        p_indicador.Value = palertas.Indicador;
                        p_indicador.Direction = ParameterDirection.Input;
                        p_indicador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_indicador);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GR_GR_ALERTAS_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("alertasData", "Eliminaralertas", ex);
                    }
                }
            }
        }
        public alertas_ries Consultaralertas(alertas_ries pAlertas, Usuario vUsuario)
        {
            DbDataReader resultado;
            alertas_ries entidad = new alertas_ries();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM GR_ALERTAS " + ObtenerFiltro(pAlertas);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {

                            if (resultado["Cod_Alerta"] != DBNull.Value) entidad.Cod_Alerta = Convert.ToInt64(resultado["Cod_Alerta"]);
                            if (resultado["Nom_Alerta"] != DBNull.Value) entidad.Nom_Alerta = Convert.ToString(resultado["Nom_Alerta"]);
                            if (resultado["Descripcion"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["Descripcion"]);
                            if (resultado["Periocidad"] != DBNull.Value) entidad.Periocidad = Convert.ToString(resultado["Periocidad"]);
                            if (resultado["Sentencia_Sql"] != DBNull.Value) entidad.Sentencia_Sql = Convert.ToString(resultado["Sentencia_Sql"]);
                            if (resultado["Indicador"] != DBNull.Value) entidad.Indicador = Convert.ToString(resultado["Indicador"]);

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
                        BOExcepcion.Throw("alertasData", "Consultaralertas", ex);
                        return null;
                    }
                }
            }
        }
    }
}