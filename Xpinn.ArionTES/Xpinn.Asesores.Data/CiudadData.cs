using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    //Se cambia de acuerdo a la entidad
    /// <summary>
    /// Objeto de acceso a datos para la tabla Ciudad
    /// </summary>
    public class CiudadData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Ciudad
        /// </summary>
        public CiudadData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla Ciudad de la base de datos
        /// </summary>
        /// <param name="pCiudad">Entidad Ciudad</param>
        /// <returns>Entidad Ciudad creada</returns>
        public Ciudad CrearCiudad(Ciudad pCiudad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODCIUDAD = cmdTransaccionFactory.CreateParameter();
                        pCODCIUDAD.ParameterName = param + "CODCIUDAD";
                        pCODCIUDAD.Value = 0;
                        pCODCIUDAD.Direction = ParameterDirection.Output;

                        DbParameter pNOMCIUDAD = cmdTransaccionFactory.CreateParameter();
                        pNOMCIUDAD.ParameterName = param + "NOMCIUDAD";
                        pNOMCIUDAD.Value = pCiudad.nomciudad;

                        DbParameter pTIPO = cmdTransaccionFactory.CreateParameter();
                        pTIPO.ParameterName = param + "TIPO";
                        pTIPO.Value = pCiudad.tipo;


                        cmdTransaccionFactory.Parameters.Add(pCODCIUDAD);
                        cmdTransaccionFactory.Parameters.Add(pNOMCIUDAD);
                        cmdTransaccionFactory.Parameters.Add(pTIPO);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_Asesores_Ciudad_C";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pCiudad, "Ciudad", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pCiudad.codciudad = Convert.ToInt64(pCODCIUDAD.Value);
                        return pCiudad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiudadData", "CrearCiudad", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla Ciudad de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Ciudad modificada</returns>
        public Ciudad ModificarCiudad(Ciudad pCiudad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODCIUDAD = cmdTransaccionFactory.CreateParameter();
                        pCODCIUDAD.ParameterName = param + "CODCIUDAD";
                        pCODCIUDAD.Value = pCiudad.codciudad;

                        DbParameter pNOMCIUDAD = cmdTransaccionFactory.CreateParameter();
                        pNOMCIUDAD.ParameterName = param + "NOMCIUDAD";
                        pNOMCIUDAD.Value = pCiudad.nomciudad;

                        DbParameter pTIPO = cmdTransaccionFactory.CreateParameter();
                        pTIPO.ParameterName = param + "TIPO";
                        pTIPO.Value = pCiudad.tipo;

                        cmdTransaccionFactory.Parameters.Add(pCODCIUDAD);
                        cmdTransaccionFactory.Parameters.Add(pNOMCIUDAD);
                        cmdTransaccionFactory.Parameters.Add(pTIPO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_Asesores_Ciudad_U";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pCiudad, "Ciudad", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pCiudad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiudadData", "ModificarCiudad", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla Ciudad de la base de datos
        /// </summary>
        /// <param name="pId">identificador de Ciudad</param>
        public void EliminarCiudad(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Ciudad pCiudad = new Ciudad();

                        if (pUsuario.programaGeneraLog)
                            pCiudad = ConsultarCiudad(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCODCIUDAD = cmdTransaccionFactory.CreateParameter();
                        pCODCIUDAD.ParameterName = param + "CODCIUDAD";
                        pCODCIUDAD.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCODCIUDAD);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_Asesores_Ciudad_D";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pCiudad, "Ciudad", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiudadData", "EliminarCiudad", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla Ciudad de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Ciudad</param>
        /// <returns>Entidad Ciudad consultado</returns>
        public Ciudad ConsultarCiudad(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Ciudad entidad = new Ciudad();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  CIUDADES WHERE codciudad = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODCIUDAD"] != DBNull.Value) entidad.codciudad = Convert.ToInt64(resultado["CODCIUDAD"]);
                            if (resultado["NOMCIUDAD"] != DBNull.Value) entidad.nomciudad = Convert.ToString(resultado["NOMCIUDAD"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt64(resultado["TIPO"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiudadData", "ConsultarCiudad", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Ciudad dados unos filtros
        /// </summary>
        /// <param name="pCiudad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Ciudad obtenidos</returns>
        public List<Ciudad> ListarCiudad(Ciudad pCiudad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Ciudad> lstCiudad = new List<Ciudad>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  CIUDADES WHERE TIPO= " + pCiudad.tipo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Ciudad entidad = new Ciudad();

                            if (resultado["CODCIUDAD"] != DBNull.Value) entidad.codciudad = Convert.ToInt64(resultado["CODCIUDAD"]);
                            if (resultado["NOMCIUDAD"] != DBNull.Value) entidad.nomciudad = Convert.ToString(resultado["NOMCIUDAD"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt64(resultado["TIPO"]);

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
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Ciudad dados unos filtros
        /// </summary>
        /// <param name="pCiudad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Ciudad obtenidos</returns>
        public List<Ciudad> ListarCiudadRecuperaciones(Ciudad pCiudad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Ciudad> lstCiudad = new List<Ciudad>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  CIUDADES" + ObtenerFiltro(pCiudad);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Ciudad entidad = new Ciudad();

                            if (resultado["CODCIUDAD"] != DBNull.Value) entidad.codciudad = Convert.ToInt64(resultado["CODCIUDAD"]);
                            if (resultado["NOMCIUDAD"] != DBNull.Value) entidad.nomciudad = Convert.ToString(resultado["NOMCIUDAD"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt64(resultado["TIPO"]);

                            lstCiudad.Add(entidad);
                        }

                        return lstCiudad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiudadData", "ListarCiudadRecuperaciones", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla CIUDADES_JUZGADOS  dados unos filtros
        /// </summary>
        /// <param name="pCiudad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Ciudad obtenidos</returns>
        public List<Ciudad> ListarCiudadjuzgados(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Ciudad> lstCiudad = new List<Ciudad>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  CIUDADES_JUZGADOS";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Ciudad entidad = new Ciudad();

                            if (resultado["CODCIUDAD"] != DBNull.Value) entidad.codciudad = Convert.ToInt64(resultado["CODCIUDAD"]);
                            if (resultado["NOMCIUDAD"] != DBNull.Value) entidad.nomciudad = Convert.ToString(resultado["NOMCIUDAD"]);
                         
                            lstCiudad.Add(entidad);
                        }

                        return lstCiudad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiudadData", "ListarCiudadjuzgados", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Ciudad para recuperaciones por asesor dados unos filtros
        /// </summary>
        /// <param name="pCiudad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Ciudad obtenidos</returns>
        public List<Ciudad> ListarZonaRecupXAsesor(Ciudad pCiudad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Ciudad> lstCiudad = new List<Ciudad>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT a.codciudad,a.nomciudad FROM  CIUDADES a inner join asejecutivos b  on a.codciudad=b.icodzona";
                        if (pUsuario.codusuario.ToString().Trim().Length >= 0)
                        {
                            sql = sql + " where iusuario  =  " + pUsuario.codusuario.ToString();
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Ciudad entidad = new Ciudad();

                            if (resultado["CODCIUDAD"] != DBNull.Value) entidad.codciudad = Convert.ToInt64(resultado["CODCIUDAD"]);
                            if (resultado["NOMCIUDAD"] != DBNull.Value) entidad.nomciudad = Convert.ToString(resultado["NOMCIUDAD"]);


                            lstCiudad.Add(entidad);
                        }

                        return lstCiudad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiudadData", "ListarCiudadRecupXAsesor", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Ciudad dados unos filtros
        /// </summary>
        /// <param name="pCiudad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Ciudad obtenidos</returns>
        public List<Ciudad> ListarZonasxOficina(Int64 idOficina, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Ciudad> lstCiudad = new List<Ciudad>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT c.*, o.idoficina FROM  ciudades c, oficinazona o WHERE c.codciudad = o.idzona AND tipo = 5 AND idoficina = " + idOficina;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Ciudad entidad = new Ciudad();

                            if (resultado["CODCIUDAD"] != DBNull.Value) entidad.codciudad = Convert.ToInt64(resultado["CODCIUDAD"]);
                            if (resultado["NOMCIUDAD"] != DBNull.Value) entidad.nomciudad = Convert.ToString(resultado["NOMCIUDAD"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt64(resultado["TIPO"]);

                            lstCiudad.Add(entidad);
                        }

                        return lstCiudad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiudadData", "ListarZonasxOficina", ex);
                        return null;
                    }
                }
            }
        }


        /// <returns>Conjunto de Ciudad obtenidos</returns>
        public List<Ciudad> ListarZonas(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Ciudad> lstCiudad = new List<Ciudad>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  ZONAS order by 1 asc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Ciudad entidad = new Ciudad();

                            if (resultado["COD_ZONA"] != DBNull.Value) entidad.codciudad = Convert.ToInt64(resultado["COD_ZONA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nomciudad = Convert.ToString(resultado["DESCRIPCION"]);
                           
                            lstCiudad.Add(entidad);
                        }

                        return lstCiudad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiudadData", "ListarZonas", ex);
                        return null;
                    }
                }
            }
        }

    }
}