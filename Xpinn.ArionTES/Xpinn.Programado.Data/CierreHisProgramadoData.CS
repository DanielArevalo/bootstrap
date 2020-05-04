using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Comun.Entities;
using Xpinn.Programado.Entities;

namespace Xpinn.Programado.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla cierre histórico
    /// </summary>
    public class CierreHisProgramdoData : GlobalData
    {      
         
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla  cierre histórico
        /// </summary>
        public CierreHisProgramdoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public CierreHistorico CierreHistorico(CierreHistorico pentidad,string valor, DateTime fechas, int cod_usuario, ref string serror, Usuario pUsuario)
        {
            serror = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "pfecha";
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.Value = fechas.ToShortDateString();
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        pestado.Direction = ParameterDirection.Input;
                        pestado.Value = valor;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pusuario = cmdTransaccionFactory.CreateParameter();
                        pusuario.ParameterName = "pusuario";
                        pusuario.Direction = ParameterDirection.Input;
                        pusuario.Value = pUsuario.codusuario;
                        pusuario.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuario);

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_CIERREHISTORICO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        return pentidad;


                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreHisProgramado", "CierreHistorico", ex);

                        serror = ex.Message;
                        return null;
                    }
                }
            }
        }


        public Xpinn.Comun.Entities.Cierea FechaUltimoCierre(Xpinn.Comun.Entities.Cierea pCierea, String filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);           

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from cierea" + ObtenerFiltro(pCierea) + " Order by fecha desc";

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            Xpinn.Comun.Entities.Cierea entidad = new Xpinn.Comun.Entities.Cierea();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["CAMPO1"] != DBNull.Value) entidad.campo1 = Convert.ToString(resultado["CAMPO1"]);
                            if (resultado["CAMPO2"] != DBNull.Value) entidad.campo2 = Convert.ToString(resultado["CAMPO2"]);
                            if (resultado["FECREA"] != DBNull.Value) entidad.fecrea = Convert.ToDateTime(resultado["FECREA"]);
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);

                            dbConnectionFactory.CerrarConexion(connection);

                            return entidad;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreHistoricoData", "FechaUltimoCierre", ex);
                        return null;
                    }
                }
            }
        }


        public void PeriodicidadCierre(ref int dias_cierre, ref int tipo_calendario, Usuario pUsuario)
        {
            dias_cierre = 30;
            tipo_calendario = 1;
            int periodicidad = 0;
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string valor = "";
                        string sql = "Select valor From general Where codigo = 1911 ";
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) valor = Convert.ToString(resultado["VALOR"].ToString().Trim());
                        }
                        try
                        {
                            periodicidad = Convert.ToInt16(valor);
                        }
                        catch
                        {
                            return;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreHistoricoData", "PeriodicidadCierre", ex);
                        return;
                    }
                }
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select numero_dias, tipo_calendario From periodicidad Where cod_periodicidad = " + periodicidad;
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_DIAS"] != DBNull.Value) dias_cierre = Convert.ToInt16(resultado["NUMERO_DIAS"].ToString());
                            if (resultado["TIPO_CALENDARIO"] != DBNull.Value) tipo_calendario = Convert.ToInt16(resultado["TIPO_CALENDARIO"].ToString());
                        }
                        return;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreHistoricoData", "PeriodicidadCierre", ex);
                        return;
                    }
                }

            }
        }

    }
}