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
    public class IndicadoresAhorrosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public IndicadoresAhorrosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public List<IndicadoresAhorros> consultarfecha(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadoresAhorros> lstComponenteAdicional = new List<IndicadoresAhorros>();
            Configuracion conf = new Configuracion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select fecha from cierea where tipo = 'H' and estado ='D' order by 1 desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadoresAhorros entidad = new IndicadoresAhorros();
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

        public List<IndicadoresAhorros> ListarTipoProducto(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadoresAhorros> lstTipoOpe = new List<IndicadoresAhorros>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select tp.cod_tipo_producto, tp.descripcion From tipoproducto tp where tp.cod_tipo_producto in(3,5,9) Order by tp.cod_tipo_producto ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadoresAhorros entidad = new IndicadoresAhorros();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_tipo_producto"] != DBNull.Value) entidad.tipo_producto = long.Parse(resultado["cod_tipo_producto"].ToString());
                            if (resultado["descripcion"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["descripcion"].ToString());
                            lstTipoOpe.Add(entidad);
                        }
                        return lstTipoOpe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IndicadoresAhorrosData", "ListarTipoProducto", ex);
                        return null;
                    }
                }
            }
        }


        public List<IndicadoresAhorros> ListarLineaAhorro(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadoresAhorros> lstTipoOpe = new List<IndicadoresAhorros>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from lineaahorro tp  Order by tp.cod_linea_ahorro ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadoresAhorros entidad = new IndicadoresAhorros();
                            //Asociar todos los valores a la entidad
                            if (resultado["COD_LINEA_AHORRO"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["COD_LINEA_AHORRO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                           lstTipoOpe.Add(entidad);
                        }
                        return lstTipoOpe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IndicadoresAhorrosData", "ListarLineaAhorro", ex);
                        return null;
                    }
                }
            }
        }
        public List<IndicadoresAhorros> ListarLineaProgramado(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadoresAhorros> lstTipoOpe = new List<IndicadoresAhorros>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from lineaprogramado tp Order by tp.cod_linea_programado ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadoresAhorros entidad = new IndicadoresAhorros();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_linea_programado"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["cod_linea_programado"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["nombre"]);
                            lstTipoOpe.Add(entidad);
                        }
                        return lstTipoOpe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IndicadoresAhorrosData", "ListarLineaAhorro", ex);
                        return null;
                    }
                }
            }
        }
        public List<IndicadoresAhorros> ListarLineaCdat(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadoresAhorros> lstTipoOpe = new List<IndicadoresAhorros>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from lineacdat tp Order by tp.cod_lineacdat";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadoresAhorros entidad = new IndicadoresAhorros();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_lineacdat"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["cod_lineacdat"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstTipoOpe.Add(entidad);
                        }
                        return lstTipoOpe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IndicadoresAhorrosData", "ListarLineaAhorro", ex);
                        return null;
                    }
                }
            }
        }


        public List<IndicadoresAhorros> consultarAhorros(string fechaini, string fechafin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadoresAhorros> lstComponenteAdicional = new List<IndicadoresAhorros>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select fecha_historico, descripcion,
                                        total_ahorros AS  total,  NUMERO_AHORROS AS numero_cuentas
                                        From V_IND_EVOLUCIONAHORROS Where fecha_historico between to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') and to_date ('" + fechafin + "', '" + conf.ObtenerFormatoFecha() + "')  " +
                                        " Order by fecha_historico";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadoresAhorros entidad = new IndicadoresAhorros();
                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToDateTime(resultado["fecha_historico"]).ToString(conf.ObtenerFormatoFecha());
                            if (resultado["total"] != DBNull.Value) entidad.total = Convert.ToDecimal(resultado["total"]);
                            if (resultado["numero_cuentas"] != DBNull.Value) entidad.numero_cuentas = Convert.ToDecimal(resultado["numero_cuentas"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);

                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IndicadoresAhorrosData", "consultarAhorros", ex);
                        return null;
                    }
                }
            }
        }

        public List<IndicadoresAhorros> consultarProgramado(string fechaini, string fechafin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadoresAhorros> lstComponenteAdicional = new List<IndicadoresAhorros>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select fecha_historico, descripcion,
                                        TOTAL_PROGRAMADO AS  total,  NUMERO_PROGRAMADO AS numero_cuentas
                                        From V_IND_EVOLUCIONPROGRAMADO Where fecha_historico between to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') and to_date ('" + fechafin + "', '" + conf.ObtenerFormatoFecha() + "')  " +
                                        " Order by fecha_historico";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadoresAhorros entidad = new IndicadoresAhorros();
                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToDateTime(resultado["fecha_historico"]).ToString(conf.ObtenerFormatoFecha());
                            if (resultado["total"] != DBNull.Value) entidad.total = Convert.ToDecimal(resultado["total"]);
                            if (resultado["numero_cuentas"] != DBNull.Value) entidad.numero_cuentas = Convert.ToDecimal(resultado["numero_cuentas"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);

                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IndicadoresAhorrosData", "consultarProgramado", ex);
                        return null;
                    }
                }
            }
        }
        public List<IndicadoresAhorros> consultarCdat(string fechaini, string fechafin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadoresAhorros> lstComponenteAdicional = new List<IndicadoresAhorros>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select fecha_historico, descripcion,
                                        sum(TOTAL_CDAT) AS  total,  sum(NUMERO_CDAT) AS numero_cuentas
                                        From V_IND_EVOLUCIONCDAT Where fecha_historico between to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') and to_date ('" + fechafin + "', '" + conf.ObtenerFormatoFecha() + "')  " +
                                        " group by fecha_historico,descripcion  Order by fecha_historico";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadoresAhorros entidad = new IndicadoresAhorros();
                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToDateTime(resultado["fecha_historico"]).ToString(conf.ObtenerFormatoFecha());
                            if (resultado["total"] != DBNull.Value) entidad.total = Convert.ToDecimal(resultado["total"]);
                            if (resultado["numero_cuentas"] != DBNull.Value) entidad.numero_cuentas = Convert.ToDecimal(resultado["numero_cuentas"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);

                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IndicadoresAhorrosData", "consultarCdat", ex);
                        return null;
                    }
                }
            }
        }
        public List<IndicadoresAhorros> consultarAhorrosVariacion(string fechaini, string fechafin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadoresAhorros> lstComponenteAdicional = new List<IndicadoresAhorros>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from V_IND_VARIACIONCARTERA where fecha_historico = (select max(fecha_historico) from V_IND_VARIACIONCARTERA where fecha_historico <= to_date ('" + fechafin + "', 'dd/mm/yyyy')) order by fecha_historico";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadoresAhorros entidad = new IndicadoresAhorros();
                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToString(resultado["fecha_historico"]);
                            if (resultado["variacion_valor"] != DBNull.Value) entidad.variacion_valor = (Convert.ToDecimal(resultado["variacion_valor"]));
                            if (resultado["variacion_numero"] != DBNull.Value) entidad.variacion_numero = Convert.ToDecimal(resultado["variacion_numero"]);                            

                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IndicadoresAhorrosData", "consultarAhorrosVariacion", ex);
                        return null;
                    }
                }
            }
        }
    }
}




