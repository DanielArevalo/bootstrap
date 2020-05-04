using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;


namespace Xpinn.FabricaCreditos.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla TipoCalificacionRef
    /// </summary>
    public class CalificacionReferenciasData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla TipoCalificacionRef
        /// </summary>
        public CalificacionReferenciasData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla TipoCalificacionRef de la base de datos
        /// </summary>
        /// <param name="pCalificacionReferencias">Entidad CalificacionReferencias</param>
        /// <returns>Entidad CalificacionReferencias creada</returns>
        public CalificacionReferencias CrearCalificacionReferencias(CalificacionReferencias pCalificacionReferencias, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pTIPOCALIFICACIONREF = cmdTransaccionFactory.CreateParameter();
                        pTIPOCALIFICACIONREF.ParameterName = "p_TipoCalificacionRef";
                        pTIPOCALIFICACIONREF.Value = pCalificacionReferencias.tipocalificacionref;
                        pTIPOCALIFICACIONREF.Direction = ParameterDirection.InputOutput;

                        DbParameter pNOMBRE = cmdTransaccionFactory.CreateParameter();
                        pNOMBRE.ParameterName = "p_Nombre";
                        pNOMBRE.Value = pCalificacionReferencias.nombre;


                        cmdTransaccionFactory.Parameters.Add(pTIPOCALIFICACIONREF);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRE);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_REF_CAL_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pCalificacionReferencias, "TipoCalificacionRef",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pCalificacionReferencias.tipocalificacionref = Convert.ToInt64(pTIPOCALIFICACIONREF.Value);
                        return pCalificacionReferencias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CalificacionReferenciasData", "CrearCalificacionReferencias", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla TipoCalificacionRef de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad CalificacionReferencias modificada</returns>
        public CalificacionReferencias ModificarCalificacionReferencias(CalificacionReferencias pCalificacionReferencias, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pTIPOCALIFICACIONREF = cmdTransaccionFactory.CreateParameter();
                        pTIPOCALIFICACIONREF.ParameterName = "p_TIPOCALIFICACIONREF";
                        pTIPOCALIFICACIONREF.Value = pCalificacionReferencias.tipocalificacionref;

                        DbParameter pNOMBRE = cmdTransaccionFactory.CreateParameter();
                        pNOMBRE.ParameterName = "p_NOMBRE";
                        pNOMBRE.Value = pCalificacionReferencias.nombre;

                        cmdTransaccionFactory.Parameters.Add(pTIPOCALIFICACIONREF);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRE);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_REF_CAL_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pCalificacionReferencias, "TipoCalificacionRef",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pCalificacionReferencias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CalificacionReferenciasData", "ModificarCalificacionReferencias", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla TipoCalificacionRef de la base de datos
        /// </summary>
        /// <param name="pId">identificador de TipoCalificacionRef</param>
        public void EliminarCalificacionReferencias(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        CalificacionReferencias pCalificacionReferencias = new CalificacionReferencias();

                        //if (pUsuario.programaGeneraLog)
                        //    pCalificacionReferencias = ConsultarCalificacionReferencias(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pTIPOCALIFICACIONREF = cmdTransaccionFactory.CreateParameter();
                        pTIPOCALIFICACIONREF.ParameterName = "p_TipoCalificacionRef";
                        pTIPOCALIFICACIONREF.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pTIPOCALIFICACIONREF);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_REF_CAL_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pCalificacionReferencias, "TipoCalificacionRef", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CalificacionReferenciasData", "EliminarCalificacionReferencias", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla TipoCalificacionRef de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla TipoCalificacionRef</param>
        /// <returns>Entidad CalificacionReferencias consultado</returns>
        public CalificacionReferencias ConsultarCalificacionReferencias(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            CalificacionReferencias entidad = new CalificacionReferencias();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TIPOCALIFICACIONREF WHERE TipoCalificacionRef = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["TIPOCALIFICACIONREF"] != DBNull.Value) entidad.tipocalificacionref = Convert.ToInt64(resultado["TIPOCALIFICACIONREF"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch(Exception ex)
                    {
                        BOExcepcion.Throw("CalificacionReferenciasData", "ConsultarCalificacionReferencias", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TipoCalificacionRef dados unos filtros
        /// </summary>
        /// <param name="pTipoCalificacionRef">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CalificacionReferencias obtenidos</returns>
        public List<CalificacionReferencias> ListarCalificacionReferencias(CalificacionReferencias pCalificacionReferencias, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CalificacionReferencias> lstCalificacionReferencias = new List<CalificacionReferencias>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TIPOCALIFICACIONREF " + ObtenerFiltro(pCalificacionReferencias);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CalificacionReferencias entidad = new CalificacionReferencias();

                            if (resultado["TIPOCALIFICACIONREF"] != DBNull.Value) entidad.tipocalificacionref = Convert.ToInt64(resultado["TIPOCALIFICACIONREF"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);

                            lstCalificacionReferencias.Add(entidad);
                        }

                        return lstCalificacionReferencias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CalificacionReferenciasData", "ListarCalificacionReferencias", ex);
                        return null;
                    }
                }
            }
        }

    }
}