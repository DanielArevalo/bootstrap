using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace Xpinn.Util
{
    /// <summary>
    /// Objeto para definicion de caracteristicas globales para la capa de acceso a datos
    /// </summary>
    public abstract class GlobalData
    {
        protected ExcepcionBusiness BOExcepcion = new ExcepcionBusiness();
        protected AuditoryData DAauditoria;
        protected string param = ":";
        protected enum Accion { Crear = 1, Listar = 2, Detalle = 3, Modificar = 4, Eliminar = 5 };

        /// <summary>
        /// Constructor del objeto global de capa de acceso a datos
        /// </summary>
        public GlobalData()
        {
            BOExcepcion = new ExcepcionBusiness();
            DAauditoria = new AuditoryData();
        }

        public string ObtenerFiltro(object pEntidad)
        {
            return ObtenerFiltro(pEntidad, "");
        }

        /// <summary>
        /// Constructor del objeto global de capa de acceso a datos
        /// </summary>
        public string ObtenerFiltro2(object pEntidad)
        {
            return ObtenerFiltroIII(pEntidad, "");
        }
        /// <summary>
        /// Obtiene la sentencia SQL para filtrar los datos
        /// </summary>
        public string ObtenerFiltro(object pEntidad, string pTabla)
        {
            try
            {
                string iniSql = " WHERE ";
                string str = "";
                int num = 0, valores = 0;
                PropertyInfo[] propertyInfos;

                if (pEntidad != null)
                {
                    propertyInfos = pEntidad.GetType().GetProperties();
                    string sTipoBase = "";

                    foreach (PropertyInfo property in propertyInfos)
                        if (property.GetValue(pEntidad, null) != null)
                            if (property.CanWrite && !string.IsNullOrWhiteSpace(property.GetValue(pEntidad, null).ToString()) && !property.GetValue(pEntidad, null).ToString().Contains("01/01/0001") && !property.GetValue(pEntidad, null).ToString().Contains("1/1/0001") && !property.GetValue(pEntidad, null).ToString().Contains("1/01/0001") && property.GetValue(pEntidad, null).ToString() != "0" && property.GetValue(pEntidad, null).ToString() != "False")
                            {
                                sTipoBase = property.PropertyType.Namespace.ToLower();
                                if (sTipoBase == "system")
                                    valores++;
                            }

                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        if (!propertyInfo.CanWrite) continue;

                        num++;
                        if (propertyInfo.GetValue(pEntidad, null) != null)
                        {
                            if (!string.IsNullOrWhiteSpace(propertyInfo.GetValue(pEntidad, null).ToString()) && !propertyInfo.GetValue(pEntidad, null).ToString().Contains("01/01/0001") && propertyInfo.GetValue(pEntidad, null).ToString() != "0")
                            {
                                if ((propertyInfo.PropertyType.Name == "DateTime" || propertyInfo.PropertyType.FullName.StartsWith("System.Nullable`1[[System.DateTime")) && !propertyInfo.GetValue(pEntidad, null).ToString().Contains("01/01/0001") && !propertyInfo.GetValue(pEntidad, null).ToString().Contains("1/1/0001") && !propertyInfo.GetValue(pEntidad, null).ToString().Contains("1/01/0001"))
                                {
                                    DateTime date = new DateTime();
                                    date = Convert.ToDateTime(propertyInfo.GetValue(pEntidad, null));
                                    str += "extract (YEAR from " + pTabla + propertyInfo.Name + ") = '" + date.Year + "'" + " AND extract (MONTH from " + pTabla + propertyInfo.Name + ") = '" + date.Month + "'" + " AND  extract (DAY from " + pTabla + propertyInfo.Name + ") = '" + date.Day + "'";
                                }
                                else if (propertyInfo.PropertyType.Name == "String")
                                {
                                    //Al dejar el (=) no realiza bien el filtro..no quedaria mejor con like
                                    str += pTabla + propertyInfo.Name + " like '%" + propertyInfo.GetValue(pEntidad, null) + "%' ";
                                }
                                else if (propertyInfo.PropertyType.Name.Contains("Nullable"))
                                {
                                    str += pTabla + propertyInfo.Name + " = " + propertyInfo.GetValue(pEntidad, null);
                                }
                                else if (propertyInfo.PropertyType.Name.Contains("Int"))
                                {
                                    str += pTabla + propertyInfo.Name + " = " + propertyInfo.GetValue(pEntidad, null);
                                }
                                else
                                {
                                    str += "";
                                }

                                if (valores != 0 && valores - 1 != 0 && str != "" && str != null)
                                {
                                    str += " AND ";
                                    valores--;
                                }
                            }
                        }
                    }
                }

                if (str != "")
                    return iniSql + str;
                else
                    return "";
            }
            catch (Exception ex)
            {
                throw new Exception("Logger.ObtenerFiltro: " + ex.Message);
            }
        }


        /// <summary>
        /// Obtiene la sentencia SQL para filtrar los datos
        /// </summary>
        public string ObtenerFiltroII(object pEntidad, string pTabla)
        {
            try
            {
                string iniSql = " WHERE ";
                string str = "";
                int num = 0, valores = 0;
                PropertyInfo[] propertyInfos;

                if (pEntidad != null)
                {
                    propertyInfos = pEntidad.GetType().GetProperties();
                    string sTipoBase = "";

                    foreach (PropertyInfo property in propertyInfos)
                        if (property.GetValue(pEntidad, null) != null)
                            if (property.CanWrite && !string.IsNullOrWhiteSpace(property.GetValue(pEntidad, null).ToString()) && !property.GetValue(pEntidad, null).ToString().Contains("01/01/0001") && !property.GetValue(pEntidad, null).ToString().Contains("1/1/0001") && !property.GetValue(pEntidad, null).ToString().Contains("1/01/0001") && property.GetValue(pEntidad, null).ToString() != "0" && property.GetValue(pEntidad, null).ToString() != "False")
                            {
                                sTipoBase = property.PropertyType.Namespace.ToLower();
                                if (sTipoBase == "system")
                                    valores++;
                            }

                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        if (!propertyInfo.CanWrite) continue;

                        num++;
                        if (propertyInfo.GetValue(pEntidad, null) != null)
                        {
                            if (!string.IsNullOrWhiteSpace(propertyInfo.GetValue(pEntidad, null).ToString()) && !propertyInfo.GetValue(pEntidad, null).ToString().Contains("01/01/0001") && propertyInfo.GetValue(pEntidad, null).ToString() != "0")
                            {
                                if ((propertyInfo.PropertyType.Name == "DateTime" || propertyInfo.PropertyType.FullName.StartsWith("System.Nullable`1[[System.DateTime")) && !propertyInfo.GetValue(pEntidad, null).ToString().Contains("01/01/0001") && !propertyInfo.GetValue(pEntidad, null).ToString().Contains("1/1/0001") && !propertyInfo.GetValue(pEntidad, null).ToString().Contains("1/01/0001"))
                                {
                                    DateTime date = new DateTime();
                                    date = Convert.ToDateTime(propertyInfo.GetValue(pEntidad, null));
                                    str += "extract (YEAR from " + pTabla + propertyInfo.Name + ") = '" + date.Year + "'" + " AND extract (MONTH from " + pTabla + propertyInfo.Name + ") = '" + date.Month + "'" + " AND  extract (DAY from " + pTabla + propertyInfo.Name + ") = '" + date.Day + "'";
                                }
                                else if (propertyInfo.PropertyType.Name == "String")
                                {
                                    str += pTabla + propertyInfo.Name + " = '" + propertyInfo.GetValue(pEntidad, null) + "' ";
                                }
                                else if (propertyInfo.PropertyType.Name.Contains("Nullable"))
                                {
                                    str += pTabla + propertyInfo.Name + " = " + propertyInfo.GetValue(pEntidad, null);
                                }
                                else if (propertyInfo.PropertyType.Name.Contains("Int"))
                                {
                                    str += pTabla + propertyInfo.Name + " = " + propertyInfo.GetValue(pEntidad, null);
                                }
                                else
                                {
                                    str += "";
                                }

                                if (valores != 0 && valores - 1 != 0 && str != "" && str != null)
                                {
                                    str += " AND ";
                                    valores--;
                                }
                            }
                        }
                    }
                }

                if (str != "")
                    return iniSql + str;
                else
                    return "";
            }
            catch (Exception ex)
            {
                throw new Exception("Logger.ObtenerFiltro: " + ex.Message);
            }
        }


        //agrega Filtros  "AND" cuando el SQL ya contiene  un WHERE
        public string ObtenerFiltroIII(object pEntidad, string pTabla)
        {
            try
            {
                string iniSql = " AND ";
                string str = "";
                int num = 0, valores = 0;
                PropertyInfo[] propertyInfos;

                if (pEntidad != null)
                {
                    propertyInfos = pEntidad.GetType().GetProperties();
                    string sTipoBase = "";

                    foreach (PropertyInfo property in propertyInfos)
                        if (property.GetValue(pEntidad, null) != null)
                            if (property.CanWrite && !string.IsNullOrWhiteSpace(property.GetValue(pEntidad, null).ToString()) && !property.GetValue(pEntidad, null).ToString().Contains("01/01/0001") && !property.GetValue(pEntidad, null).ToString().Contains("1/1/0001") && !property.GetValue(pEntidad, null).ToString().Contains("1/01/0001") && property.GetValue(pEntidad, null).ToString() != "0" && property.GetValue(pEntidad, null).ToString() != "False")
                            {
                                sTipoBase = property.PropertyType.Namespace.ToLower();
                                if (sTipoBase == "system")
                                    valores++;
                            }

                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        if (!propertyInfo.CanWrite) continue;

                        num++;
                        if (propertyInfo.GetValue(pEntidad, null) != null)
                        {
                            if (!string.IsNullOrWhiteSpace(propertyInfo.GetValue(pEntidad, null).ToString()) && !propertyInfo.GetValue(pEntidad, null).ToString().Contains("01/01/0001") && propertyInfo.GetValue(pEntidad, null).ToString() != "0")
                            {
                                if ((propertyInfo.PropertyType.Name == "DateTime" || propertyInfo.PropertyType.FullName.StartsWith("System.Nullable`1[[System.DateTime")) && !propertyInfo.GetValue(pEntidad, null).ToString().Contains("01/01/0001") && !propertyInfo.GetValue(pEntidad, null).ToString().Contains("1/1/0001") && !propertyInfo.GetValue(pEntidad, null).ToString().Contains("1/01/0001"))
                                {
                                    DateTime date = new DateTime();
                                    date = Convert.ToDateTime(propertyInfo.GetValue(pEntidad, null));
                                    str += "extract (YEAR from " + pTabla + propertyInfo.Name + ") = '" + date.Year + "'" + " AND extract (MONTH from " + pTabla + propertyInfo.Name + ") = '" + date.Month + "'" + " AND  extract (DAY from " + pTabla + propertyInfo.Name + ") = '" + date.Day + "'";
                                }
                                else if (propertyInfo.PropertyType.Name == "String")
                                {
                                    str += pTabla + propertyInfo.Name + " = '" + propertyInfo.GetValue(pEntidad, null) + "' ";
                                }
                                else if (propertyInfo.PropertyType.Name.Contains("Nullable"))
                                {
                                    str += pTabla + propertyInfo.Name + " = " + propertyInfo.GetValue(pEntidad, null);
                                }
                                else if (propertyInfo.PropertyType.Name.Contains("Int"))
                                {
                                    str += pTabla + propertyInfo.Name + " = " + propertyInfo.GetValue(pEntidad, null);
                                }
                                else
                                {
                                    str += "";
                                }

                                if (valores != 0 && valores - 1 != 0 && str != "" && str != null)
                                {
                                    str += " AND ";
                                    valores--;
                                }
                            }
                        }
                    }
                }

                if (str != "")
                    return iniSql + str;
                else
                    return "";
            }
            catch (Exception ex)
            {
                throw new Exception("Logger.ObtenerFiltro: " + ex.Message);
            }
        }

        //Agregado para consultas con valores decimales
        public string ObtenerFiltro(object pEntidad, string pTabla, bool pDecimal)
        {
            return ObtenerFiltro(pEntidad, pTabla, pDecimal, ".");
        }

        //Agregado para consultas con valores decimales
        public string ObtenerFiltro(object pEntidad, string pTabla, bool pDecimal, string pseparador_decimal)
        {
            try
            {
                string iniSql = "";
                string str = "";
                if (pDecimal == true)
                {
                    iniSql = " WHERE ";
                    int num = 0, valores = 0;
                    PropertyInfo[] propertyInfos;

                    if (pEntidad != null)
                    {
                        propertyInfos = pEntidad.GetType().GetProperties();
                        string sTipoBase = "";

                        foreach (PropertyInfo property in propertyInfos)
                            if (property.GetValue(pEntidad, null) != null)
                                if (property.CanWrite && !string.IsNullOrWhiteSpace(property.GetValue(pEntidad, null).ToString()) && !property.GetValue(pEntidad, null).ToString().Contains("01/01/0001") && !property.GetValue(pEntidad, null).ToString().Contains("1/01/0001") && property.GetValue(pEntidad, null).ToString() != "0" && property.GetValue(pEntidad, null).ToString() != "False")
                                {
                                    sTipoBase = property.PropertyType.Namespace.ToLower();
                                    if (sTipoBase == "system")
                                        valores++;
                                }

                        foreach (PropertyInfo propertyInfo in propertyInfos)
                        {
                            if (!propertyInfo.CanWrite) continue;

                            num++;
                            if (propertyInfo.GetValue(pEntidad, null) != null)
                            {
                                if (!string.IsNullOrWhiteSpace(propertyInfo.GetValue(pEntidad, null).ToString()) && !propertyInfo.GetValue(pEntidad, null).ToString().Contains("01/01/0001") && propertyInfo.GetValue(pEntidad, null).ToString() != "0")
                                {
                                    if ((propertyInfo.PropertyType.Name == "DateTime" || propertyInfo.PropertyType.FullName.StartsWith("System.Nullable`1[[System.DateTime")) && !propertyInfo.GetValue(pEntidad, null).ToString().Contains("01/01/0001") && !propertyInfo.GetValue(pEntidad, null).ToString().Contains("1/01/0001"))
                                    {
                                        DateTime date = new DateTime();
                                        date = Convert.ToDateTime(propertyInfo.GetValue(pEntidad, null));
                                        str += "extract (YEAR from " + pTabla + propertyInfo.Name + ") = '" + date.Year + "'" + " AND extract (MONTH from " + pTabla + propertyInfo.Name + ") = '" + date.Month + "'" + " AND  extract (DAY from " + pTabla + propertyInfo.Name + ") = '" + date.Day + "'";
                                    }
                                    else if (propertyInfo.PropertyType.Name == "String")
                                    {
                                        //Al dejar el (=) no realiza bien el filtro..no quedaria mejor con like
                                        str += pTabla + propertyInfo.Name + " like '%" + propertyInfo.GetValue(pEntidad, null) + "%' ";
                                    }
                                    else if (propertyInfo.PropertyType.Name.Contains("Nullable") && !propertyInfo.PropertyType.FullName.Contains("Decimal"))
                                    {
                                        str += pTabla + propertyInfo.Name + " = " + propertyInfo.GetValue(pEntidad, null);
                                    }
                                    else if (propertyInfo.PropertyType.Name.Contains("Int"))
                                    {
                                        str += pTabla + propertyInfo.Name + " = " + propertyInfo.GetValue(pEntidad, null);
                                    }
                                    else if (propertyInfo.PropertyType.Name.Contains("Nullable") && propertyInfo.PropertyType.FullName.Contains("Decimal"))
                                    {
                                        string valor = propertyInfo.GetValue(pEntidad, null).ToString();
                                        string separador_decimal = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                                        if (separador_decimal == pseparador_decimal)
                                        {
                                            // Quitar el separador de miles
                                            string separador_miles = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator;
                                            valor = valor.Replace(separador_miles, "");
                                        }
                                        else
                                        {
                                            valor = valor.Replace(".", "");
                                            valor = valor.Replace(",", pseparador_decimal);
                                        }

                                        str += pTabla + propertyInfo.Name + " = '" + valor + "'";
                                    }
                                    else
                                    {
                                        str += "";
                                    }

                                    if (valores != 0 && valores - 1 != 0 && str != "")
                                    {
                                        str += " AND ";
                                        valores--;
                                    }
                                }
                            }
                        }
                    }
                }
                if (str != "")
                    return iniSql + str;
                else
                    return "";

            }
            catch (Exception ex)
            {
                throw new Exception("Logger.ObtenerFiltro: " + ex.Message);
            }
        }
        public bool GuardarConexion(DbCommand cmdTransaccionFactorys, DbCommand cmdTransaccionFactory, DbConnection connection, long codUsuario)
        {
            if (cmdTransaccionFactorys.Parameters.Count <= 0) return false;
            try
            {
                DbParameter psession_id = cmdTransaccionFactory.CreateParameter();
                psession_id.ParameterName = "psession_id";
                psession_id.Value = 0;
                psession_id.Direction = ParameterDirection.Output;
                psession_id.DbType = DbType.Int32;
                cmdTransaccionFactory.Parameters.Add(psession_id);

                DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                pcod_usuario.ParameterName = "pcod_usuario";
                pcod_usuario.Value = codUsuario;
                pcod_usuario.Direction = ParameterDirection.Input;
                pcod_usuario.DbType = DbType.Int32;
                cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                cmdTransaccionFactory.Connection = connection;
                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                cmdTransaccionFactory.CommandText = "Usp_Xpinn_Glob_Conexion_Cre";
                cmdTransaccionFactory.ExecuteNonQuery();
                return true;
            }
            catch 
            {
                return false;
            }

        }


        public DbConnection AbrirConexion(ConnectionDataBase dbConnectionFactory, DbCommand cmdTransaccionFactory, DbConnection connection, Usuario pUsuario)
        {
            connection.Open(); 
            GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); 
            dbConnectionFactory.CerrarConexion(connection); 
            connection.Open();
            return connection;
        }



    }

}
