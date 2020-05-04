using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Caja.Entities;


namespace Xpinn.Caja.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Ciudad
    /// </summary>
    public class CiudadData:GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
        
        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public CiudadData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene la lista de Ciudades dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Ciudades obtenidas</returns>
        public List<Ciudad> ListarCiudad(Ciudad pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Ciudad> lstCiudad = new List<Ciudad>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT * FROM CIUDADES Where tipo=3 Order by nomciudad asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Ciudad entidad = new Ciudad();
                            //Asociar todos los valores a la entidad
                            if (resultado["codciudad"] != DBNull.Value) entidad.cod_ciudad = Convert.ToInt64(resultado["codciudad"]);
                            if (resultado["nomciudad"] != DBNull.Value) entidad.nom_ciudad = Convert.ToString(resultado["nomciudad"]);
                            lstCiudad.Add(entidad);
                        }

                        return lstCiudad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiudadData", "ListarCiudad", ex);
                        return null;
                    }
                }
            }
        }


        public Ciudad ConsultarCiudadXNombre(Ciudad pCiudad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Ciudad entidad = new Ciudad();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM CIUDADES Where tipo=3 AND TRIM(NOMCIUDAD) = '" + pCiudad.nom_ciudad + "'";
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["codciudad"] != DBNull.Value) entidad.cod_ciudad = Convert.ToInt64(resultado["codciudad"]);
                            if (resultado["nomciudad"] != DBNull.Value) entidad.nom_ciudad = Convert.ToString(resultado["nomciudad"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiudadData", "ListarCiudad", ex);
                        return null;
                    }
                }
            }
        }

        public List<Ciudad> ListadoCiudad(Ciudad pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Ciudad> lstCiudad = new List<Ciudad>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT * FROM CIUDADES Order by nomciudad asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Ciudad entidad = new Ciudad();
                            //Asociar todos los valores a la entidad
                            if (resultado["codciudad"] != DBNull.Value) entidad.cod_ciudad = Convert.ToInt64(resultado["codciudad"]);
                            if (resultado["nomciudad"] != DBNull.Value) entidad.nom_ciudad = Convert.ToString(resultado["nomciudad"]);
                            lstCiudad.Add(entidad);
                        }

                        return lstCiudad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiudadData", "ListarCiudad", ex);
                        return null;
                    }
                }
            }
        }

        public Ciudad CiudadTran(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Ciudad entidad = new Ciudad();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select c.NOMCIUDAD from OFICINA o inner join ciudades c on o.COD_CIUDAD = c.CODCIUDAD where o.COD_OFICINA = " + pUsuario.cod_oficina;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["NOMCIUDAD"] != DBNull.Value) entidad.nom_ciudad = Convert.ToString(resultado["NOMCIUDAD"]);
                        }
                        
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Ciudad", "Ciudad", ex);
                        return null;
                    }
                }
            }
        }
    }
}
