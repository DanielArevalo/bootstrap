using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Data
{
    public class EstFlujoEfectivoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public EstFlujoEfectivoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<EstFlujoEfectivo> ListarDdll(Usuario pUsuario, int opcion)
        {
            DbDataReader resultado = default(DbDataReader);
            List<EstFlujoEfectivo> lstEstFlujo = new List<EstFlujoEfectivo>();
            string sql = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        switch (opcion)
                        {
                            case 1:
                                sql = "select * from centro_costo";
                                connection.Open();
                                cmdTransaccionFactory.Connection = connection;
                                cmdTransaccionFactory.CommandType = CommandType.Text;
                                cmdTransaccionFactory.CommandText = sql;
                                resultado = cmdTransaccionFactory.ExecuteReader();

                                while (resultado.Read())
                                {
                                    EstFlujoEfectivo entidad = new EstFlujoEfectivo();
                                    if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.valor1 = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                                    if (resultado["NOM_CENTRO"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["NOM_CENTRO"]);
                                    lstEstFlujo.Add(entidad);
                                }
                                dbConnectionFactory.CerrarConexion(connection);
                                break;
                            case 2:
                                sql = "select * from cierea where tipo='C'and  estado ='D' order by fecha desc";
                                connection.Open();
                                cmdTransaccionFactory.Connection = connection;
                                cmdTransaccionFactory.CommandType = CommandType.Text;
                                cmdTransaccionFactory.CommandText = sql;
                                resultado = cmdTransaccionFactory.ExecuteReader();

                                while (resultado.Read())
                                {
                                    EstFlujoEfectivo entidad = new EstFlujoEfectivo();
                                    if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                                    lstEstFlujo.Add(entidad);
                                }
                                dbConnectionFactory.CerrarConexion(connection);
                                break;
                        }

                        return lstEstFlujo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstFlujoEfectivo", "ListarDdll", ex);
                        return null;
                    }
                }
            }
        }


        public List<EstFlujoEfectivo> getListaReporGridv(Usuario pUsuario, DateTime fechaActual, DateTime fechaAnterior, int costoid,int pOpcion)
        {

            DbDataReader resultado = default(DbDataReader);
            List<EstFlujoEfectivo> lstEstFlujo = new List<EstFlujoEfectivo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pFechaActual = cmdTransaccionFactory.CreateParameter();
                        pFechaActual.ParameterName = "pFechaActual";
                        pFechaActual.Value = fechaActual;
                        pFechaActual.Direction = ParameterDirection.Input;

                        DbParameter pFechaAnterior = cmdTransaccionFactory.CreateParameter();
                        pFechaAnterior.ParameterName = "pFechaAnterior";
                        pFechaAnterior.Value = fechaAnterior;
                        pFechaActual.Direction = ParameterDirection.Input;

                        DbParameter pCentroCosto = cmdTransaccionFactory.CreateParameter();
                        pCentroCosto.ParameterName = "pCentroCosto";
                        pCentroCosto.Value = costoid;
                        pFechaActual.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pFechaActual);
                        cmdTransaccionFactory.Parameters.Add(pFechaAnterior);
                        cmdTransaccionFactory.Parameters.Add(pCentroCosto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (pOpcion == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_FLUJOEFECTIVO";
                        else //NIIF PENDIENTE POR AJUSTAR PL
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_FLUJOEFECT_NIIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoData", "CrearConcepto", ex);
                        return null;
                    }
                }
            }

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM temp_flujoefectivo";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            EstFlujoEfectivo entidad = new EstFlujoEfectivo();

                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR1"] != DBNull.Value) entidad.valor1 = Convert.ToDecimal(resultado["VALOR1"]);
                            if (resultado["VALOR2"] != DBNull.Value) entidad.valor2 = Convert.ToDecimal(resultado["VALOR2"]);
                            if (resultado["VARIACION"] != DBNull.Value) entidad.variacion = Convert.ToDecimal(resultado["VARIACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.Descripcion2 = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.caja = Convert.ToDecimal(resultado["CENTRO_COSTO"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.bancos_Comerciales = Convert.ToDecimal(resultado["TIPO"]);
                            if (resultado["TOTAL"] != DBNull.Value) entidad.total = Convert.ToDecimal(resultado["TOTAL"]);

                            lstEstFlujo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEstFlujo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstFlujoEfectivo", "ListarDdll", ex);
                        return null;
                    }
                }
            }


        }

    }
}
