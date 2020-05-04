using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Comun.Entities;

namespace Xpinn.Comun.Data
{
    public class ListaDesplegableData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public ListaDesplegableData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

       
        /// <summary>
        /// Obtiene la lista de ListaDesplegablees
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ListaDesplegablees obtenidos</returns>
        public List<ListaDesplegable> ListarListaDesplegable(ListaDesplegable pListaDesplegable, string pTabla, Usuario pUsuario)
        {
            return ListarListaDesplegable(pListaDesplegable, pTabla, "", "", "", pUsuario);
        }

        public List<ListaDesplegable> ListarListaDesplegable(ListaDesplegable pListaDesplegable, string pTabla, string pColumnas, string pCondicion, string pOrden, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ListaDesplegable> lstListaDesplegable = new List<ListaDesplegable>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                        
                        string sql = @"SELECT " + (pColumnas.Trim() == "" ? "*" : pColumnas) + " FROM " + pTabla + " " + ObtenerFiltro(pListaDesplegable);
                        if (pCondicion.Trim() != "")
                            sql += (sql.ToUpper().Contains("WHERE") ? " AND " : " WHERE ") + pCondicion;                        
                        if (pOrden.Trim() != "")
                            sql += " ORDER BY " + pOrden;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ListaDesplegable entidad = new ListaDesplegable();

                            if (resultado[0] != DBNull.Value) entidad.idconsecutivo = Convert.ToString(resultado[0]);
                            if (resultado[1] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado[1]);
                            lstListaDesplegable.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstListaDesplegable;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ListaDesplegableData", "ListarListaDesplegable", ex);
                        return null;
                    }
                }
            }
        }

        public List<ListaDesplegable> ListarListaDesplegable2(ListaDesplegable pListaDesplegable, string IdLinea, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ListaDesplegable> lstListaDesplegable = new List<ListaDesplegable>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select 
                                    D.DESCRIPCION, LD.COD_DESTINO
                                    from LINEACRED_DESTINACION LD
                                    left join DESTINACION D on LD.COD_DESTINO = D.COD_DESTINO
                                    where COD_LINEA_CREDITO =" + IdLinea  +  " order by 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ListaDesplegable entidad = new ListaDesplegable();

                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.idconsecutivo = Convert.ToString(resultado["COD_DESTINO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstListaDesplegable.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstListaDesplegable;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ListaDesplegableData", "ListarListaDesplegable", ex);
                        return null;
                    }
                }
            }
        }


        public List<ListaDesplegable> ListarListaDesplegable3(ListaDesplegable pListaDesplegable, string IdLinea, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ListaDesplegable> lstListaDesplegable = new List<ListaDesplegable>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select D.DESCRIPCION, LD.COD_DESTINO
                                    from LINEASER_DESTINACION LD
                                    left join DESTINACION D on LD.COD_DESTINO = D.COD_DESTINO
                                    where COD_LINEA_SERVICIO =" + IdLinea + " order by 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ListaDesplegable entidad = new ListaDesplegable();

                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.idconsecutivo = Convert.ToString(resultado["COD_DESTINO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstListaDesplegable.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstListaDesplegable;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ListaDesplegableData", "ListarListaDesplegable", ex);
                        return null;
                    }
                }
            }
        }

        public List<ListaDesplegable> ListarListaDesplegableEmpresaaportes(ListaDesplegable pListaDesplegable, string pTabla, string pColumnas, string pCondicion, string pOrden, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ListaDesplegable> lstListaDesplegable = new List<ListaDesplegable>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT " + (pColumnas.Trim() == "" ? "*" : pColumnas) + " FROM " + pTabla + " " + ObtenerFiltro(pListaDesplegable);
                        if (pCondicion.Trim() != "")
                            sql += (sql.ToUpper().Contains("WHERE") ? " AND " : " WHERE ") + pCondicion;
                        if (pOrden.Trim() != "")
                            sql += " ORDER BY " + pOrden;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ListaDesplegable entidad = new ListaDesplegable();

                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.idconsecutivo = Convert.ToString(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["NOM_EMPRESA"]);
                            lstListaDesplegable.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstListaDesplegable;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ListaDesplegableData", "ListarListaDesplegable", ex);
                        return null;
                    }
                }
            }
        }

        public List<ListaDesplegable> ListarPeriodicidad(string pCodPeriodicidad, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ListaDesplegable> lstListaDesplegable = new List<ListaDesplegable>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select cod_periodicidad, descripcion, numero_dias, numero_meses, periodos_anuales, tipo_calendario From periodicidad " + (pCodPeriodicidad.Trim() != "" ? " Where cod_periodicidad = '" + pCodPeriodicidad + "' ": "") + " Order by cod_periodicidad";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ListaDesplegable entidad = new ListaDesplegable();

                            if (resultado["NUMERO_DIAS"] != DBNull.Value) entidad.idconsecutivo = Convert.ToString(resultado["NUMERO_DIAS"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstListaDesplegable.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstListaDesplegable;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ListaDesplegableData", "ConsultarPeriodicidad", ex);
                        return null;
                    }
                }
            }
        }


    }
}
