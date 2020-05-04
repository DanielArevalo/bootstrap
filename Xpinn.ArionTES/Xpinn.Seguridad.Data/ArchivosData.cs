using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using Xpinn.Util;
using Xpinn.Seguridad.Entities;

namespace Xpinn.Seguridad.Data
{
    /// <summary>
    /// Summary description for TablaData
    /// </summary>
    public class ArchivosData
    {

        //esta variable está en la clase global... aquí se usa pero en el proyecto real no.
        protected ExcepcionBusiness BOExcepcion = new ExcepcionBusiness();

        protected ConnectionDataBase dbConnectionFactory;

        public ArchivosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public string consultarusuariobd(Usuario pUsuario)
        {
            String resultado = "";
            DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario);
            resultado = connection.ConnectionString.Split(';')[1].Split('=')[1].ToUpper();

            return resultado;

        }
        public BaseDatosEntidad infoEsquemasBaseDatos(BaseDatosEntidad baseDatosEntidad, Usuario pUsuario)
        {

            DbDataReader resultado = default(DbDataReader);
            BaseDatosEntidad basedatos = new BaseDatosEntidad();
            basedatos.TipoProveedor = baseDatosEntidad.TipoProveedor;
            basedatos.Nombre = baseDatosEntidad.Nombre;

            List<TablaEntidad> lstTablas = new List<TablaEntidad>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sqlTabla = null;
                        string sqlColumna = null;
                        if (baseDatosEntidad.TipoProveedor == TipoProveedorBaseDatos.SqlServer)
                        {
                            sqlTabla = "SELECT NOMBRE  FROM V_ESQUEMAS ";
                        }
                        if (baseDatosEntidad.TipoProveedor == TipoProveedorBaseDatos.Oracle)
                        {
                            // sqlTabla = "select TABLE_NAME NombreTabla from user_tables";
                            sqlTabla = " SELECT NOMBRE  FROM V_ESQUEMAS   ";

                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sqlTabla;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TablaEntidad tablaEntidad = new TablaEntidad();

                            if (resultado["NOMBRE"] != DBNull.Value) tablaEntidad.Nombre = Convert.ToString(resultado["NOMBRE"]);

                            lstTablas.Add(tablaEntidad);
                        }
                        resultado.Close();

                        dbConnectionFactory.CerrarConexion(connection);
                        basedatos.Tablas = lstTablas;
                        return basedatos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ArchivosData", "infoEsquemasBaseDatos", ex);
                        return null;
                    }
                }
            }
        }
        //---20161227

        public BaseDatosEntidad infoBaseDatosColumnas(BaseDatosEntidad baseDatosEntidad, String nombreTabla, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            BaseDatosEntidad basedatos = new BaseDatosEntidad();
            basedatos.TipoProveedor = baseDatosEntidad.TipoProveedor;
            basedatos.Nombre = baseDatosEntidad.Nombre;

            List<TablaEntidad> lstTablas = new List<TablaEntidad>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sqlTabla = null;
                        string sqlColumna = null;
                        if (baseDatosEntidad.TipoProveedor == TipoProveedorBaseDatos.SqlServer)
                        {
                            sqlColumna = "SELECT so.name AS Tabla,sc.name AS Columna,st.name AS Tipo,sc.max_length AS Longitud FROM sys.objects so INNER JOIN sys.columns sc ON so.object_id = sc.object_id INNER JOIN sys.types st ON st.system_type_id = sc.system_type_id AND st.name != 'sysname' WHERE so.type = 'U' and so.name = '@NombreTabla' ORDER BY so.name, sc.name ";
                        }
                        if (baseDatosEntidad.TipoProveedor == TipoProveedorBaseDatos.Oracle)
                        {
                            sqlColumna = "SELECT col.column_name Columna, col.data_type Tipo,col.data_length Longitud FROM all_tab_columns col, all_col_comments com WHERE col.table_name = com.table_name AND col.column_name = com.column_name AND col.owner=com.owner AND  col.table_name = '@NombreTabla'  and col.owner = '@OWNER' ORDER BY col.table_name, col.column_id ";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        /*cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sqlTabla;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TablaEntidad tablaEntidad = new TablaEntidad();

                            if (resultado["NombreTabla"] != DBNull.Value) tablaEntidad.Nombre = Convert.ToString(resultado["NombreTabla"]);

                            lstTablas.Add(tablaEntidad);
                        }
                        resultado.Close();*/
                        String sql = "";
                        //foreach (TablaEntidad tabla in lstTablas)
                        foreach (TablaEntidad tabla in baseDatosEntidad.Tablas)
                        {
                            if (tabla.Nombre.Equals(nombreTabla))
                            {

                            
                                sql = sqlColumna.Replace("@NombreTabla", tabla.Nombre);
                                sql = sql.Replace("@OWNER", baseDatosEntidad.Owner.ToUpper());
                                cmdTransaccionFactory.CommandType = CommandType.Text;
                                cmdTransaccionFactory.CommandText = sql;
                                resultado = cmdTransaccionFactory.ExecuteReader();
                                while (resultado.Read())
                                {
                                    ColumnaEntidad columna = new ColumnaEntidad();
                                    if (resultado["Columna"] != DBNull.Value) columna.Nombre = Convert.ToString(resultado["Columna"]);
                                    if (resultado["Tipo"] != DBNull.Value) columna.Tipo = Convert.ToString(resultado["Tipo"]);
                                    if (resultado["Longitud"] != DBNull.Value) columna.Longitud = Convert.ToInt32(resultado["Longitud"]);
                                    tabla.ColumnasEntidad.Add(columna);
                                }
                                break;
                                resultado.Close();
                            }
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        basedatos.Tablas = lstTablas;
                        return baseDatosEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ArchivosData", "infoBaseDatos", ex);
                        return null;
                    }
                }
            }
        }


        //--20161227

        public BaseDatosEntidad infoBaseDatos(BaseDatosEntidad baseDatosEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            BaseDatosEntidad basedatos = new BaseDatosEntidad();
            basedatos.TipoProveedor = baseDatosEntidad.TipoProveedor;
            basedatos.Nombre = baseDatosEntidad.Nombre;

            List<TablaEntidad> lstTablas = new List<TablaEntidad>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sqlTabla = null;
                        string sqlColumna = null;
                        if (baseDatosEntidad.TipoProveedor == TipoProveedorBaseDatos.SqlServer)
                        {
                            sqlTabla = "SELECT SO.NAME NombreTabla FROM sys.objects SO INNER JOIN sys.columns SC ON SO.OBJECT_ID = SC.OBJECT_ID WHERE SO.TYPE = 'U' GROUP BY SO.NAME ORDER BY SO.NAME";
                            sqlColumna = "SELECT so.name AS Tabla,sc.name AS Columna,st.name AS Tipo,sc.max_length AS Longitud FROM sys.objects so INNER JOIN sys.columns sc ON so.object_id = sc.object_id INNER JOIN sys.types st ON st.system_type_id = sc.system_type_id AND st.name != 'sysname' WHERE so.type = 'U' and so.name = '@NombreTabla' ORDER BY so.name, sc.name ";
                        }
                        if (baseDatosEntidad.TipoProveedor == TipoProveedorBaseDatos.Oracle)
                        {
                            // sqlTabla = "select TABLE_NAME NombreTabla from user_tables";
                            sqlTabla = " SELECT nombre NombreTabla  FROM V_TABLAS_MIGRACIONES   WHERE ESQUEMA = " + "'" + baseDatosEntidad.Owner.ToUpper() + "' order by 1";
                            sqlColumna = "SELECT col.column_name Columna, col.data_type Tipo,col.data_length Longitud FROM all_tab_columns col, all_col_comments com WHERE col.table_name = com.table_name AND col.column_name = com.column_name AND col.owner=com.owner AND  col.table_name = '@NombreTabla'  and col.owner = '@OWNER' ORDER BY col.table_name, col.column_id ";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sqlTabla;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TablaEntidad tablaEntidad = new TablaEntidad();

                            if (resultado["NombreTabla"] != DBNull.Value) tablaEntidad.Nombre = Convert.ToString(resultado["NombreTabla"]);

                            lstTablas.Add(tablaEntidad);
                        }
                        resultado.Close();
                        String sql = "";
                        /* foreach (TablaEntidad tabla in lstTablas)
                         {
                             sql = sqlColumna.Replace("@NombreTabla", tabla.Nombre);
                             sql = sql.Replace("@OWNER", baseDatosEntidad.Owner.ToUpper());
                             cmdTransaccionFactory.CommandType = CommandType.Text;
                             cmdTransaccionFactory.CommandText = sql;
                             resultado = cmdTransaccionFactory.ExecuteReader();
                             while (resultado.Read())
                             {
                                 ColumnaEntidad columna = new ColumnaEntidad();
                                 if (resultado["Columna"] != DBNull.Value) columna.Nombre = Convert.ToString(resultado["Columna"]);
                                 if (resultado["Tipo"] != DBNull.Value) columna.Tipo = Convert.ToString(resultado["Tipo"]);
                                 if (resultado["Longitud"] != DBNull.Value) columna.Longitud = Convert.ToInt32(resultado["Longitud"]);
                                 tabla.ColumnasEntidad.Add(columna);
                             }
                             resultado.Close();
                         }
                          */
                        dbConnectionFactory.CerrarConexion(connection);
                        basedatos.Tablas = lstTablas;
                        return basedatos;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ArchivosData", "infoBaseDatos", ex);
                        return null;
                    }
                }
            }
        }


        public BaseDatosEntidad ingresarDatosBaseDatos(Seguridad.Entities.TablaEntidadExt tablaentidadex, System.Data.DataTable tablaorigen, Usuario pUsuario, BaseDatosEntidad basedatosin)
        {
            DbDataReader resultado = default(DbDataReader);
            BaseDatosEntidad basedatos = new BaseDatosEntidad();
            String prefijo = "";
            if (tablaentidadex.Tipoprovedorbd == TipoProveedorBaseDatos.Oracle)
            {
                prefijo = ":";
            }
            if (tablaentidadex.Tipoprovedorbd == TipoProveedorBaseDatos.SqlServer)
            {
                prefijo = "@";
            }
            String sql = "INSERT INTO " + basedatosin.Owner + "." + tablaentidadex.NombreTablaDestino + "";
            String columnas = "";
            foreach (Seguridad.Entities.ColumnaEntidadExt column in tablaentidadex.ColumnasEntidadExt)
            {
                if (!column.NombreOrigen.Equals("No Importar"))
                {
                    if (columnas.Trim().Length > 0)
                    {
                        columnas = columnas + ",";
                    }
                    columnas = columnas + column.NombreDestino;
                }
            }
            sql = sql + "(" + columnas + ") values ";
            columnas = "";
            foreach (Seguridad.Entities.ColumnaEntidadExt column in tablaentidadex.ColumnasEntidadExt)
            {
                if (!column.NombreOrigen.Equals("No Importar"))
                {
                    if (columnas.Trim().Length > 0)
                    {
                        columnas = columnas + ",";
                    }
                    columnas = columnas + prefijo + column.NombreDestino;
                }
            }
            sql = sql + "(" + columnas + ")";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sqlTabla = null;
                        string sqlColumna = null;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;

                        foreach (DataRow row in tablaorigen.Rows)
                        {
                            cmdTransaccionFactory.Parameters.Clear();
                            Seguridad.Entities.RegistroEntidad registroentidad = new RegistroEntidad();

                            foreach (Seguridad.Entities.ColumnaEntidadExt column in tablaentidadex.ColumnasEntidadExt)
                            {
                                if (column.NombreOrigen != "No Importar")
                                {
                                    DbParameter pidout = cmdTransaccionFactory.CreateParameter();
                                    pidout.ParameterName = prefijo + column.NombreDestino;
                                    pidout.Value = row[column.NombreOrigen];
                                    pidout.Direction = ParameterDirection.Input;
                                    //pidout.DbType = DbType.Int64;
                                    if (registroentidad.Registro.Trim().Length > 0)
                                    {
                                        registroentidad.Registro = registroentidad.Registro + "|";
                                    }
                                    registroentidad.Registro = registroentidad.Registro + pidout.Value;
                                    cmdTransaccionFactory.Parameters.Add(pidout);
                                }

                            }
                            try
                            {
                                cmdTransaccionFactory.ExecuteNonQuery();
                                basedatos.Registros.RegistrosExitosos.Add(registroentidad);
                            }
                            catch (Exception ex)
                            {
                                registroentidad.Mensaje = ex.Message;
                                basedatos.Registros.RegistrosFallidos.Add(registroentidad);
                            }
                        }

                        //basedatos.Tablas = lstTablas;


                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ArchivosData", "ingresarDatosBaseDatos", ex);
                        return null;
                    }
                    dbConnectionFactory.CerrarConexion(connection);
                    return basedatos;
                }
            }
        }


    }
}