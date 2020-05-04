using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using System.Web;

namespace Xpinn.FabricaCreditos.Data
{
    public class HistoricoTasaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public HistoricoTasaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public HistoricoTasa obtenermod(string cod, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<HistoricoTasa> lstAnexos = new List<HistoricoTasa>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select historicotasa.tipo_historico, historicotasa.idhistorico, historicotasa.fecha_final,
                                    historicotasa.fecha_inicial, historicotasa.valor, tipotasahist.descripcion 
                                    From historicotasa inner join tipotasahist on tipotasahist.tipo_historico = historicotasa.tipo_historico 
                                    Where historicotasa.idhistorico = " + cod + @" Order by historicotasa.fecha_inicial, historicotasa.fecha_final";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        HistoricoTasa entidad = new HistoricoTasa();
                        if (resultado.Read())
                        {

                            //Asociar todos los valores a la entidad
                            if (resultado["tipo_historico"] != DBNull.Value) entidad.TIPO_HISTORICO = Convert.ToInt64(resultado["tipo_historico"]);
                            if (resultado["idhistorico"] != DBNull.Value) entidad.IDHISTORICO = Convert.ToInt64(resultado["idhistorico"]);
                            if (resultado["fecha_final"] != DBNull.Value) entidad.FECHA_FINAL = Convert.ToDateTime(resultado["fecha_final"]);
                            if (resultado["fecha_inicial"] != DBNull.Value) entidad.FECHA_INICIAL = Convert.ToDateTime(resultado["fecha_inicial"]);
                            if (resultado["valor"] != DBNull.Value) entidad.VALOR = Convert.ToDecimal(resultado["valor"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AnexoCreditorData", "ListarAnexos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Método para listar las tasas históricas grabadas
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public IList<HistoricoTasa> listarhistorico(string tipo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<HistoricoTasa> lstAnexos = new List<HistoricoTasa>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";

                        if (tipo == "0")
                        {
                            sql = @"Select historicotasa.idhistorico,historicotasa.fecha_final,historicotasa.fecha_inicial,historicotasa.valor,tipotasahist.descripcion From historicotasa inner join tipotasahist
                                        on tipotasahist.tipo_historico = historicotasa.tipo_historico
                                        order by tipotasahist.descripcion,historicotasa.fecha_inicial desc";
                        }
                        else
                        {
                            sql = @"Select historicotasa.tipo_historico,
                                    historicotasa.idhistorico,
                                    historicotasa.fecha_final,
                                    historicotasa.fecha_inicial,
                                    historicotasa.valor,
                                    tipotasahist.descripcion 
                                    From historicotasa inner join tipotasahist
                                    on tipotasahist.tipo_historico = historicotasa.tipo_historico where historicotasa.tipo_historico=" + tipo +
                                    "order by Historicotasa.Fecha_Inicial desc ";
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            HistoricoTasa entidad = new HistoricoTasa();
                            //Asociar todos los valores a la entidad
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.DESCRIPCION = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["idhistorico"] != DBNull.Value) entidad.IDHISTORICO = Convert.ToInt64(resultado["idhistorico"]);
                            if (resultado["fecha_final"] != DBNull.Value) entidad.FECHA_FINAL = Convert.ToDateTime(resultado["fecha_final"]);
                            if (resultado["fecha_inicial"] != DBNull.Value) entidad.FECHA_INICIAL = Convert.ToDateTime(resultado["fecha_inicial"]);
                            if (resultado["valor"] != DBNull.Value) entidad.VALOR = Convert.ToDecimal(resultado["valor"]);
                            lstAnexos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAnexos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AnexoCreditorData", "ListarAnexos", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Método para listar las tasas históricas grabadas
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public List<HistoricoTasa> ListarTasasHistoricas(HistoricoTasa pentidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<HistoricoTasa> lstAnexos = new List<HistoricoTasa>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";

                        // if (tipo == "0")
                        // {
                        sql = @"Select historicotasa.idhistorico,historicotasa.fecha_final,historicotasa.fecha_inicial,historicotasa.valor,tipotasahist.descripcion From historicotasa inner join tipotasahist
                                        on tipotasahist.tipo_historico = historicotasa.tipo_historico";
                        // }
                        //// else
                        // {
                        //     //sql = @"Select historicotasa.tipo_historico,
                        //        //      historicotasa.idhistorico,
                        //           /   historicotasa.fecha_final,
                        //              historicotasa.fecha_inicial,
                        //              historicotasa.valor,
                        //              tipotasahist.descripcion 
                        //              From historicotasa inner join tipotasahist
                        //              on tipotasahist.tipo_historico = historicotasa.tipo_historico where historicotasa.tipo_historico=" + tipo;
                        // }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            HistoricoTasa entidad = new HistoricoTasa();
                            //Asociar todos los valores a la entidad
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.DESCRIPCION = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["idhistorico"] != DBNull.Value) entidad.IDHISTORICO = Convert.ToInt64(resultado["idhistorico"]);
                            if (resultado["fecha_final"] != DBNull.Value) entidad.FECHA_FINAL = Convert.ToDateTime(resultado["fecha_final"]);
                            if (resultado["fecha_inicial"] != DBNull.Value) entidad.FECHA_INICIAL = Convert.ToDateTime(resultado["fecha_inicial"]);
                            if (resultado["valor"] != DBNull.Value) entidad.VALOR = Convert.ToDecimal(resultado["valor"]);
                            lstAnexos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAnexos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AnexoCreditorData", "ListarAnexos", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarHistorico(long cod, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<HistoricoTasa> lstAnexos = new List<HistoricoTasa>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"DELETE
                                    FROM HISTORICOTASA
                                    WHERE IDHISTORICO  =" + cod;
                        connection.Open();
                        GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);
                        dbConnectionFactory.CerrarConexion(connection);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AnexoCreditorData", "ListarAnexos", ex);

                    }
                }
            }
        }

        /// <summary>
        /// Método para actualizra datos de una tasa histórica
        /// </summary>
        /// <param name="historico"></param>
        public void ModHistorico(HistoricoTasa historico, Usuario pUsuario)
        {
            var error = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        

                        DbParameter PIDHISTORICO = cmdTransaccionFactory.CreateParameter();
                        PIDHISTORICO.ParameterName = "PIDHISTORICO";
                        PIDHISTORICO.Value = historico.IDHISTORICO;

                        DbParameter PTIPO_HISTORICO = cmdTransaccionFactory.CreateParameter();
                        PTIPO_HISTORICO.ParameterName = "PTIPO_HISTORICO";
                        PTIPO_HISTORICO.Value = historico.TIPO_HISTORICO;

                        DbParameter PFECHA_INICIAL = cmdTransaccionFactory.CreateParameter();
                        PFECHA_INICIAL.ParameterName = "PFECHA_INICIAL";
                        PFECHA_INICIAL.Value = historico.FECHA_INICIAL;
                        PFECHA_INICIAL.DbType = DbType.Date;

                        DbParameter PFECHA_FINAL = cmdTransaccionFactory.CreateParameter();
                        PFECHA_FINAL.ParameterName = "PFECHA_FINAL";
                        PFECHA_FINAL.Value = historico.FECHA_FINAL;
                        PFECHA_FINAL.DbType = DbType.Date;

                        DbParameter PVALOR = cmdTransaccionFactory.CreateParameter();
                        PVALOR.ParameterName = "PVALOR";
                        PVALOR.Value = historico.VALOR;

                        cmdTransaccionFactory.Parameters.Add(PIDHISTORICO);
                        cmdTransaccionFactory.Parameters.Add(PTIPO_HISTORICO);
                        cmdTransaccionFactory.Parameters.Add(PFECHA_INICIAL);
                        cmdTransaccionFactory.Parameters.Add(PFECHA_FINAL);
                        cmdTransaccionFactory.Parameters.Add(PVALOR);

                        connection.Open();
                        GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);
                        dbConnectionFactory.CerrarConexion(connection);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_HISTORICOTAS_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pUsuario, "HISTORICOTASA", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        dbConnectionFactory.CerrarConexion(connection);
                        BOExcepcion.Throw("HistoricoTasaData", "ModHistorico", ex);
                        return;
                    }
                }
            }
        }


        public void CrearHistorico(HistoricoTasa historico, Usuario pUsuario)
        {

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        DbParameter PIDHISTORICO = cmdTransaccionFactory.CreateParameter();
                        PIDHISTORICO.ParameterName = "PIDHISTORICO";
                        PIDHISTORICO.Value = historico.IDHISTORICO;

                        DbParameter PTIPO_HISTORICO = cmdTransaccionFactory.CreateParameter();
                        PTIPO_HISTORICO.ParameterName = "PTIPO_HISTORICO";
                        PTIPO_HISTORICO.Value = historico.TIPO_HISTORICO;

                        DbParameter PFECHA_INICIAL = cmdTransaccionFactory.CreateParameter();
                        PFECHA_INICIAL.ParameterName = "PFECHA_INICIAL";
                        PFECHA_INICIAL.Value = historico.FECHA_INICIAL;

                        DbParameter PFECHA_FINAL = cmdTransaccionFactory.CreateParameter();
                        PFECHA_FINAL.ParameterName = "PFECHA_FINAL";
                        PFECHA_FINAL.Value = historico.FECHA_FINAL;

                        DbParameter PVALOR = cmdTransaccionFactory.CreateParameter();
                        PVALOR.ParameterName = "PVALOR";
                        PVALOR.Value = historico.VALOR;

                        cmdTransaccionFactory.Parameters.Add(PIDHISTORICO);
                        cmdTransaccionFactory.Parameters.Add(PTIPO_HISTORICO);
                        cmdTransaccionFactory.Parameters.Add(PFECHA_INICIAL);
                        cmdTransaccionFactory.Parameters.Add(PFECHA_FINAL);
                        cmdTransaccionFactory.Parameters.Add(PVALOR);

                        connection.Open();
                        GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_HISTORICOTASA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "InsertarPersona1", ex);
                    }
                }
            }
        }

        public IList<HistoricoTasa> tipohistorico(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<HistoricoTasa> lstAnexos = new List<HistoricoTasa>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * From tipotasahist";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            HistoricoTasa entidad = new HistoricoTasa();
                            //Asociar todos los valores a la entidad
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.DESCRIPCION = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.TIPODEHISTORICO = Convert.ToInt64(resultado["TIPO_HISTORICO"]);
                            lstAnexos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAnexos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AnexoCreditorData", "ListarAnexos", ex);
                        return null;
                    }
                }
            }
        }
    }
}
