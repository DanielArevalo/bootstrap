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
    /// Objeto de acceso a datos para la tabla IngresosFamilia
    /// </summary>
    public class IngresosFamiliaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla IngresosFamilia
        /// </summary>
        public IngresosFamiliaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla IngresosFamilia de la base de datos
        /// </summary>
        /// <param name="pIngresosFamilia">Entidad IngresosFamilia</param>
        /// <returns>Entidad IngresosFamilia creada</returns>
        public IngresosFamilia CrearIngresosFamilia(IngresosFamilia pIngresosFamilia, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_INGRESO = cmdTransaccionFactory.CreateParameter();
                        pCOD_INGRESO.ParameterName = "p_COD_INGRESO";
                        pCOD_INGRESO.Value = 0;
                        pCOD_INGRESO.Direction = ParameterDirection.InputOutput;

                        DbParameter pINGRESOS = cmdTransaccionFactory.CreateParameter();
                        pINGRESOS.ParameterName = "p_INGRESOS";
                        pINGRESOS.Value = pIngresosFamilia.ingresos;

                        DbParameter pNEGOCIO = cmdTransaccionFactory.CreateParameter();
                        pNEGOCIO.ParameterName = "p_NEGOCIO";
                        pNEGOCIO.Value = pIngresosFamilia.negocio;

                        DbParameter pCONYUGE = cmdTransaccionFactory.CreateParameter();
                        pCONYUGE.ParameterName = "p_CONYUGE";
                        pCONYUGE.Value = pIngresosFamilia.conyuge;

                        DbParameter pHIJOS = cmdTransaccionFactory.CreateParameter();
                        pHIJOS.ParameterName = "p_HIJOS";
                        pHIJOS.Value = pIngresosFamilia.hijos;

                        DbParameter pARRIENDOS = cmdTransaccionFactory.CreateParameter();
                        pARRIENDOS.ParameterName = "p_ARRIENDOS";
                        pARRIENDOS.Value = pIngresosFamilia.arriendos;

                        DbParameter pPENSION = cmdTransaccionFactory.CreateParameter();
                        pPENSION.ParameterName = "p_PENSION";
                        pPENSION.Value = pIngresosFamilia.pension;

                        DbParameter pOTROS = cmdTransaccionFactory.CreateParameter();
                        pOTROS.ParameterName = "p_OTROS";
                        pOTROS.Value = pIngresosFamilia.otros;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pIngresosFamilia.cod_persona;


                        cmdTransaccionFactory.Parameters.Add(pCOD_INGRESO);
                        cmdTransaccionFactory.Parameters.Add(pINGRESOS);
                        cmdTransaccionFactory.Parameters.Add(pNEGOCIO);
                        cmdTransaccionFactory.Parameters.Add(pCONYUGE);
                        cmdTransaccionFactory.Parameters.Add(pHIJOS);
                        cmdTransaccionFactory.Parameters.Add(pARRIENDOS);
                        cmdTransaccionFactory.Parameters.Add(pPENSION);
                        cmdTransaccionFactory.Parameters.Add(pOTROS);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_INGFA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pIngresosFamilia, "IngresosFamilia",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pIngresosFamilia.cod_ingreso = Convert.ToInt64(pCOD_INGRESO.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pIngresosFamilia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IngresosFamiliaData", "CrearIngresosFamilia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla IngresosFamilia de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad IngresosFamilia modificada</returns>
        public IngresosFamilia ModificarIngresosFamilia(IngresosFamilia pIngresosFamilia, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_INGRESO = cmdTransaccionFactory.CreateParameter();
                        pCOD_INGRESO.ParameterName = "p_COD_INGRESO";
                        pCOD_INGRESO.Value = pIngresosFamilia.cod_ingreso;

                        DbParameter pINGRESOS = cmdTransaccionFactory.CreateParameter();
                        pINGRESOS.ParameterName = "p_INGRESOS";
                        pINGRESOS.Value = pIngresosFamilia.ingresos;

                        DbParameter pNEGOCIO = cmdTransaccionFactory.CreateParameter();
                        pNEGOCIO.ParameterName = "p_NEGOCIO";
                        pNEGOCIO.Value = pIngresosFamilia.negocio;

                        DbParameter pCONYUGE = cmdTransaccionFactory.CreateParameter();
                        pCONYUGE.ParameterName = "p_CONYUGE";
                        pCONYUGE.Value = pIngresosFamilia.conyuge;

                        DbParameter pHIJOS = cmdTransaccionFactory.CreateParameter();
                        pHIJOS.ParameterName = "p_HIJOS";
                        pHIJOS.Value = pIngresosFamilia.hijos;

                        DbParameter pARRIENDOS = cmdTransaccionFactory.CreateParameter();
                        pARRIENDOS.ParameterName = "p_ARRIENDOS";
                        pARRIENDOS.Value = pIngresosFamilia.arriendos;

                        DbParameter pPENSION = cmdTransaccionFactory.CreateParameter();
                        pPENSION.ParameterName = "p_PENSION";
                        pPENSION.Value = pIngresosFamilia.pension;

                        DbParameter pOTROS = cmdTransaccionFactory.CreateParameter();
                        pOTROS.ParameterName = "p_OTROS";
                        pOTROS.Value = pIngresosFamilia.otros;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pIngresosFamilia.cod_persona;

                        cmdTransaccionFactory.Parameters.Add(pCOD_INGRESO);
                        cmdTransaccionFactory.Parameters.Add(pINGRESOS);
                        cmdTransaccionFactory.Parameters.Add(pNEGOCIO);
                        cmdTransaccionFactory.Parameters.Add(pCONYUGE);
                        cmdTransaccionFactory.Parameters.Add(pHIJOS);
                        cmdTransaccionFactory.Parameters.Add(pARRIENDOS);
                        cmdTransaccionFactory.Parameters.Add(pPENSION);
                        cmdTransaccionFactory.Parameters.Add(pOTROS);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_INGFA_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pIngresosFamilia, "IngresosFamilia",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pIngresosFamilia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IngresosFamiliaData", "ModificarIngresosFamilia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla IngresosFamilia de la base de datos
        /// </summary>
        /// <param name="pId">identificador de IngresosFamilia</param>
        public void EliminarIngresosFamilia(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        IngresosFamilia pIngresosFamilia = new IngresosFamilia();

                        //if (pUsuario.programaGeneraLog)
                        //    pIngresosFamilia = ConsultarIngresosFamilia(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_INGRESO = cmdTransaccionFactory.CreateParameter();
                        pCOD_INGRESO.ParameterName = "p_COD_INGRESO";
                        pCOD_INGRESO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_INGRESO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_INGFA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pIngresosFamilia, "IngresosFamilia", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IngresosFamiliaData", "EliminarIngresosFamilia", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla IngresosFamilia de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla IngresosFamilia</param>
        /// <returns>Entidad IngresosFamilia consultado</returns>
        public IngresosFamilia ConsultarIngresosFamilia(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            IngresosFamilia entidad = new IngresosFamilia();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  INGRESOSFAMILIA WHERE COD_INGRESO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_INGRESO"] != DBNull.Value) entidad.cod_ingreso = Convert.ToInt64(resultado["COD_INGRESO"]);
                            if (resultado["INGRESOS"] != DBNull.Value) entidad.ingresos = Convert.ToInt64(resultado["INGRESOS"]);
                            if (resultado["NEGOCIO"] != DBNull.Value) entidad.negocio = Convert.ToInt64(resultado["NEGOCIO"]);
                            if (resultado["CONYUGE"] != DBNull.Value) entidad.conyuge = Convert.ToInt64(resultado["CONYUGE"]);
                            if (resultado["HIJOS"] != DBNull.Value) entidad.hijos = Convert.ToInt64(resultado["HIJOS"]);
                            if (resultado["ARRIENDOS"] != DBNull.Value) entidad.arriendos = Convert.ToInt64(resultado["ARRIENDOS"]);
                            if (resultado["PENSION"] != DBNull.Value) entidad.pension = Convert.ToInt64(resultado["PENSION"]);
                            if (resultado["OTROS"] != DBNull.Value) entidad.otros = Convert.ToInt64(resultado["OTROS"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch(Exception ex)
                    {
                        BOExcepcion.Throw("IngresosFamiliaData", "ConsultarIngresosFamilia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla IngresosFamilia dados unos filtros
        /// </summary>
        /// <param name="pIngresosFamilia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de IngresosFamilia obtenidos</returns>
        public List<IngresosFamilia> ListarIngresosFamilia(IngresosFamilia pIngresosFamilia, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IngresosFamilia> lstIngresosFamilia = new List<IngresosFamilia>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  INGRESOSFAMILIA ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IngresosFamilia entidad = new IngresosFamilia();

                            if (resultado["COD_INGRESO"] != DBNull.Value) entidad.cod_ingreso = Convert.ToInt64(resultado["COD_INGRESO"]);
                            if (resultado["INGRESOS"] != DBNull.Value) entidad.ingresos = Convert.ToInt64(resultado["INGRESOS"]);
                            if (resultado["NEGOCIO"] != DBNull.Value) entidad.negocio = Convert.ToInt64(resultado["NEGOCIO"]);
                            if (resultado["CONYUGE"] != DBNull.Value) entidad.conyuge = Convert.ToInt64(resultado["CONYUGE"]);
                            if (resultado["HIJOS"] != DBNull.Value) entidad.hijos = Convert.ToInt64(resultado["HIJOS"]);
                            if (resultado["ARRIENDOS"] != DBNull.Value) entidad.arriendos = Convert.ToInt64(resultado["ARRIENDOS"]);
                            if (resultado["PENSION"] != DBNull.Value) entidad.pension = Convert.ToInt64(resultado["PENSION"]);
                            if (resultado["OTROS"] != DBNull.Value) entidad.otros = Convert.ToInt64(resultado["OTROS"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);

                            lstIngresosFamilia.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstIngresosFamilia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IngresosFamiliaData", "ListarIngresosFamilia", ex);
                        return null;
                    }
                }
            }
        }

    }
}