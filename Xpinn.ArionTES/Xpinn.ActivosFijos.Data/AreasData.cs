using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.ActivosFijos.Entities;

namespace Xpinn.ActivosFijos.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla ActivoFijos
    /// </summary>
    public class AreasData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ACTIVOS_FIJOS
        /// </summary>
        public AreasData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Areas CrearAreas(Areas pAreas, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIdArea = cmdTransaccionFactory.CreateParameter();
                        pIdArea.ParameterName = "p_IdArea";
                        pIdArea.Value = pAreas.IdArea;
                        pIdArea.Direction = ParameterDirection.Input;
                        pIdArea.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pIdArea);

                        DbParameter pDescripcionArea = cmdTransaccionFactory.CreateParameter();
                        pDescripcionArea.ParameterName = "p_DescripcionArea";
                        pDescripcionArea.Value = pAreas.DescripcionArea;
                        pDescripcionArea.Direction = ParameterDirection.Input;
                        pDescripcionArea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pDescripcionArea);

                        DbParameter pIdCentroCosto = cmdTransaccionFactory.CreateParameter();
                        pIdCentroCosto.ParameterName = "p_IdCentroCosto";
                        pIdCentroCosto.Value = pAreas.IdCentroCosto;
                        pIdCentroCosto.Direction = ParameterDirection.Input;
                        pIdCentroCosto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pIdCentroCosto);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_COM_AREAS_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);


                        //pActivoFijo.consecutivo = Convert.ToInt64(pconsecutivo.Value);

                        return pAreas ;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "CrearActivoFijo", ex);
                        return null;
                    }
                }
            }
        }




        /// <summary>
        /// Modifica un registro en la tabla ActivoFijo de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ActivoFijo modificada</returns>
        public Areas ModificarAreas(Areas pAreas, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIdArea = cmdTransaccionFactory.CreateParameter();
                        pIdArea.ParameterName = "p_IdArea";
                        pIdArea.Value = pAreas.IdArea ;
                        pIdArea.Direction = ParameterDirection.Input;
                        pIdArea.DbType = DbType.Int32 ;
                        cmdTransaccionFactory.Parameters.Add(pIdArea);

                        DbParameter pDescripcionArea = cmdTransaccionFactory.CreateParameter();
                        pDescripcionArea.ParameterName = "p_DescripcionArea";
                        pDescripcionArea.Value = pAreas.DescripcionArea ;
                        pDescripcionArea.Direction = ParameterDirection.Input;
                        pDescripcionArea.DbType = DbType.String ;
                        cmdTransaccionFactory.Parameters.Add(pDescripcionArea);

                        DbParameter pIdCentroCosto = cmdTransaccionFactory.CreateParameter();
                        pIdCentroCosto.ParameterName = "p_IdCentroCosto";
                        pIdCentroCosto.Value = pAreas.IdCentroCosto ;
                        pIdCentroCosto.Direction = ParameterDirection.Input;
                        pIdCentroCosto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pIdCentroCosto);

                        

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_COM_AREAS_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pAreas ;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AreasData", "ModificarAreas", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla ActivoFijoS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de ActivoFijoS</param>
        public void EliminarAreas(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ActivoFijo pActivoFijo = new ActivoFijo();

                        DbParameter pIdAreas = cmdTransaccionFactory.CreateParameter();
                        pIdAreas.ParameterName = "p_IdArea";
                        pIdAreas.Value = Convert.ToInt32(pId);
                        pIdAreas.Direction = ParameterDirection.Input;
                        pIdAreas.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pIdAreas);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_COM_AREAS_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoData", "EliminarConcepto", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla ActivoFijoS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla ActivoFijoS</param>
        /// <returns>Entidad ActivoFijo consultado</returns>
        public Areas ConsultarAreas(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Areas entidad = new Areas();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT IdArea,Descripcion,Centro_Costo FROM cm_area
                                            WHERE IdArea = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IdArea"] != DBNull.Value) entidad.IdArea = Convert.ToInt64(resultado["IdArea"]);
                            if (resultado["Descripcion"] != DBNull.Value) entidad.DescripcionArea  = resultado["Descripcion"].ToString();
                            if (resultado["Centro_Costo"] != DBNull.Value) entidad.IdCentroCosto  = Convert.ToInt64(resultado["Centro_Costo"]);
                           
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AreasData", "ConsultarAreas", ex);
                        return null;
                    }
                }
            }
        }

       

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla ActivoFijoS dados unos filtros
        /// </summary>
        /// <param name="pActivoFijoS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ActivoFijo obtenidos</returns>
        public List<Areas> ListarAreas(Areas pAreas, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Areas> lstAreas = new List<Areas>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT *
                                            FROM cm_area                                             
                                      " + ObtenerFiltro(pAreas , "Areas.") + " ORDER BY IdArea";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Areas entidad = new Areas();
                            if (resultado["IdArea"] != DBNull.Value) entidad.IdArea  = Convert.ToInt64(resultado["IdArea"]);
                            if (resultado["Descripcion"] != DBNull.Value) entidad.DescripcionArea  = resultado["Descripcion"].ToString();
                            if (resultado["Centro_Costo"] != DBNull.Value) entidad.IdCentroCosto  = Convert.ToInt32(resultado["Centro_Costo"]);

                            lstAreas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAreas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "ListarActivoFijo", ex);
                        return null;
                    }
                }
            }
        }

    

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(cod_act) + 1 FROM ACTIVOS_FIJOS ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch 
                    {
                        return 1;
                    }
                }
            }
        }



    }
}
