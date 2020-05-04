using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Seguridad.Entities;

namespace Xpinn.Seguridad.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla PROCESO
    /// </summary>
    public class UsuarioIngresoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PROCESO
        /// </summary>
        public UsuarioIngresoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

      

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla PROCESO dados unos filtros
        /// </summary>
        /// <param name="pPROCESO">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Proceso obtenidos</returns>
        public List<Ingresos> ListarIngresos(string filtro,DateTime pFechaIni,DateTime pFechaFin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Ingresos> lstProceso = new List<Ingresos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select I.Cod_Ingreso,U.Nombre,I.Fecha_Horaingreso,I.Fecha_Horasalida,I.Direccionip "
                                      + "from Usuario_Ingreso I left join usuarios U on I.Codusuario = U.Codusuario where 1 = 1 " + filtro;

                        if (pFechaIni != null && pFechaIni != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " TRUNC(I.Fecha_Horaingreso) >= To_Date('" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " TRUNC(I.Fecha_Horaingreso) >= '" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        if (pFechaFin != null && pFechaFin != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " TRUNC(I.Fecha_Horasalida) <= To_Date('" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " TRUNC(I.Fecha_Horasalida) <= '" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }
                        sql += " Order by 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Ingresos entidad = new Ingresos();

                            if (resultado["COD_INGRESO"] != DBNull.Value) entidad.cod_ingreso = Convert.ToInt32(resultado["COD_INGRESO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["FECHA_HORAINGRESO"] != DBNull.Value) entidad.fecha_horaingreso = Convert.ToDateTime(resultado["FECHA_HORAINGRESO"]);
                            if (resultado["FECHA_HORASALIDA"] != DBNull.Value) entidad.fecha_horasalida = Convert.ToDateTime(resultado["FECHA_HORASALIDA"]);
                            if (resultado["DIRECCIONIP"] != DBNull.Value) entidad.direccionip = Convert.ToString(resultado["DIRECCIONIP"]);
                            lstProceso.Add(entidad);
                        }

                        return lstProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioIngresoData", "ListarIngresos", ex);
                        return null;
                    }
                }
            }
        }




    }
}