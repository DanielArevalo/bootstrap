using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Indicadores.Entities;
using System.Web;

namespace Xpinn.Indicadores.Data
{
    public class IndicadoresLiquidezData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public IndicadoresLiquidezData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public List<IndicadoresLiquidez> consultarfecha(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadoresLiquidez> lstComponenteAdicional = new List<IndicadoresLiquidez>();
            Configuracion conf = new Configuracion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select fecha from cierea where tipo = 'C' and estado ='D' order by 1 desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadoresLiquidez entidad = new IndicadoresLiquidez();
                            if (resultado["fecha"] != DBNull.Value) entidad.fecha_corte = (Convert.ToDateTime(resultado["fecha"]).ToString(conf.ObtenerFormatoFecha()));
                            lstComponenteAdicional.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraOficinasData", "consultarfecha", ex);
                        return null;
                    }
                }
            }
        }
        public List<IndicadoresLiquidez> consultarFondoLiquidez(string fechaini, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadoresLiquidez> lstComponenteAdicional = new List<IndicadoresLiquidez>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select descripcion,fecha, cod_cuenta,
                                        total_valor AS total                                       
                                        From V_IND_FONDO_LIQUIDEZ2 Where fecha = to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "')  " +
                                        " Order by fecha";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadoresLiquidez entidad = new IndicadoresLiquidez();
                            if (resultado["fecha"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["fecha"]).ToString(conf.ObtenerFormatoFecha());
                            if (resultado["total"] != DBNull.Value) entidad.total = Convert.ToDecimal(resultado["total"]);
                            if (resultado["cod_cuenta"] != DBNull.Value) entidad.cod_cuenta = Convert.ToDecimal(resultado["cod_cuenta"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);

                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IndicadoreLiquidezData", "consultarFondoLiquidez", ex);
                        return null;
                    }
                }
            }
        }
        public List<IndicadoresLiquidez> consultarDepositosLiquidez(string fechaini, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadoresLiquidez> lstComponenteAdicional = new List<IndicadoresLiquidez>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select descripcion,fecha, cod_cuenta,
                                        total_valor AS total                                       
                                        From V_IND_FONDO_LIQUIDEZ Where fecha = to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "')  " +
                                        " Order by fecha";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadoresLiquidez entidad = new IndicadoresLiquidez();
                            if (resultado["fecha"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["fecha"]).ToString(conf.ObtenerFormatoFecha());
                            if (resultado["total"] != DBNull.Value) entidad.total = Convert.ToDecimal(resultado["total"]);
                            if (resultado["cod_cuenta"] != DBNull.Value) entidad.cod_cuenta = Convert.ToDecimal(resultado["cod_cuenta"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);

                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IndicadoreLiquidezData", "consultarDepositosLiquidez", ex);
                        return null;
                    }
                }
            }
        }
        public List<IndicadoresLiquidez> consultarDisponible(string fechaini, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadoresLiquidez> lstComponenteAdicional = new List<IndicadoresLiquidez>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select descripcion,fecha, cod_cuenta,
                                        total_valor AS total                                       
                                        From V_IND_DISPONIBLE Where fecha = to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "')  " +
                                        " Order by fecha";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadoresLiquidez entidad = new IndicadoresLiquidez();
                            if (resultado["fecha"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["fecha"]).ToString(conf.ObtenerFormatoFecha());
                            if (resultado["total"] != DBNull.Value) entidad.total = Convert.ToDecimal(resultado["total"]);
                            if (resultado["cod_cuenta"] != DBNull.Value) entidad.cod_cuenta = Convert.ToDecimal(resultado["cod_cuenta"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);

                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IndicadoreLiquidezData", "consultarDisponible", ex);
                        return null;
                    }
                }
            }
        }



    }
}




