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
    /// Objeto de acceso a datos para la tabla MODULO
    /// </summary>
    public class ModuloData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla MODULO
        /// </summary>
        public ModuloData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla MODULO de la base de datos
        /// </summary>
        /// <param name="pModulo">Entidad Modulo</param>
        /// <returns>Entidad Modulo creada</returns>
        public Modulo CrearModulo(Modulo pModulo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_MODULO = cmdTransaccionFactory.CreateParameter();
                        pCOD_MODULO.ParameterName = "p_cod_modulo";
                        pCOD_MODULO.Value = pModulo.cod_modulo;
                        pCOD_MODULO.Direction = ParameterDirection.InputOutput;

                        DbParameter pNOM_MODULO = cmdTransaccionFactory.CreateParameter();
                        pNOM_MODULO.ParameterName = "p_nom_modulo";
                        pNOM_MODULO.Value = pModulo.nom_modulo;

                        cmdTransaccionFactory.Parameters.Add(pCOD_MODULO);
                        cmdTransaccionFactory.Parameters.Add(pNOM_MODULO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_MODULO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pModulo, "MODULO",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pModulo.cod_modulo = Convert.ToInt64(pCOD_MODULO.Value);
                        return pModulo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ModuloData", "CrearModulo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla MODULO de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Modulo modificada</returns>
        public Modulo ModificarModulo(Modulo pModulo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_MODULO = cmdTransaccionFactory.CreateParameter();
                        pCOD_MODULO.ParameterName = "p_cod_modulo";
                        pCOD_MODULO.Value = pModulo.cod_modulo;

                        DbParameter pNOM_MODULO = cmdTransaccionFactory.CreateParameter();
                        pNOM_MODULO.ParameterName = "p_nom_modulo";
                        pNOM_MODULO.Value = pModulo.nom_modulo;

                        cmdTransaccionFactory.Parameters.Add(pCOD_MODULO);
                        cmdTransaccionFactory.Parameters.Add(pNOM_MODULO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_MODULO_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pModulo, "MODULO",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pModulo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ModuloData", "ModificarModulo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla MODULO de la base de datos
        /// </summary>
        /// <param name="pId">identificador de MODULO</param>
        public void EliminarModulo(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Modulo pModulo = new Modulo();

                        if (pUsuario.programaGeneraLog)
                            pModulo = ConsultarModulo(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_MODULO = cmdTransaccionFactory.CreateParameter();
                        pCOD_MODULO.ParameterName = "p_cod_modulo";
                        pCOD_MODULO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_MODULO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_MODULO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pModulo, "MODULO", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ModuloData", "InsertarModulo", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla MODULO de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla MODULO</param>
        /// <returns>Entidad Modulo consultado</returns>
        public Modulo ConsultarModulo(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Modulo entidad = new Modulo();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  MODULO WHERE cod_modulo = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_MODULO"] != DBNull.Value) entidad.cod_modulo = Convert.ToInt64(resultado["COD_MODULO"]);
                            if (resultado["NOM_MODULO"] != DBNull.Value) entidad.nom_modulo = Convert.ToString(resultado["NOM_MODULO"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch(Exception ex)
                    {
                        BOExcepcion.Throw("ModuloData", "ConsultarModulo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla MODULO dados unos filtros
        /// </summary>
        /// <param name="pMODULO">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Modulo obtenidos</returns>
        public List<Modulo> ListarModulo(Modulo pModulo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Modulo> lstModulo = new List<Modulo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  MODULO ORDER BY COD_MODULO" + ObtenerFiltro(pModulo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Modulo entidad = new Modulo();

                            if (resultado["COD_MODULO"] != DBNull.Value) entidad.cod_modulo = Convert.ToInt64(resultado["COD_MODULO"]);
                            if (resultado["NOM_MODULO"] != DBNull.Value) entidad.nom_modulo = Convert.ToString(resultado["NOM_MODULO"]);

                            lstModulo.Add(entidad);
                        }

                        return lstModulo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ModuloData", "ListarModulo", ex);
                        return null;
                    }
                }
            }
        }

    }
}