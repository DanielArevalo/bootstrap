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
    /// Objeto de acceso a datos para la tabla PresupuestoEmpresarial
    /// </summary>
    public class PresupuestoEmpresarialData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PresupuestoEmpresarial
        /// </summary>
        public PresupuestoEmpresarialData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla PresupuestoEmpresarial de la base de datos
        /// </summary>
        /// <param name="pPresupuestoEmpresarial">Entidad PresupuestoEmpresarial</param>
        /// <returns>Entidad PresupuestoEmpresarial creada</returns>
        public PresupuestoEmpresarial CrearPresupuestoEmpresarial(PresupuestoEmpresarial pPresupuestoEmpresarial, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_PRESUPUESTO = cmdTransaccionFactory.CreateParameter();
                        pCOD_PRESUPUESTO.ParameterName = "p_COD_PRESUPUESTO";
                        pCOD_PRESUPUESTO.Value = pPresupuestoEmpresarial.cod_presupuesto;
                        pCOD_PRESUPUESTO.Direction = ParameterDirection.InputOutput;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pPresupuestoEmpresarial.cod_persona;

                        DbParameter pTOTALACTIVO = cmdTransaccionFactory.CreateParameter();
                        pTOTALACTIVO.ParameterName = "p_TOTALACTIVO";
                        pTOTALACTIVO.Value = pPresupuestoEmpresarial.totalactivo;

                        DbParameter pTOTALPASIVO = cmdTransaccionFactory.CreateParameter();
                        pTOTALPASIVO.ParameterName = "p_TOTALPASIVO";
                        pTOTALPASIVO.Value = pPresupuestoEmpresarial.totalpasivo;

                        DbParameter pTOTALPATRIMONIO = cmdTransaccionFactory.CreateParameter();
                        pTOTALPATRIMONIO.ParameterName = "p_TOTALPATRIMONIO";
                        pTOTALPATRIMONIO.Value = pPresupuestoEmpresarial.totalpatrimonio;

                        DbParameter pVENTAMENSUAL = cmdTransaccionFactory.CreateParameter();
                        pVENTAMENSUAL.ParameterName = "p_VENTAMENSUAL";
                        pVENTAMENSUAL.Value = pPresupuestoEmpresarial.ventamensual;

                        DbParameter pCOSTOTOTAL = cmdTransaccionFactory.CreateParameter();
                        pCOSTOTOTAL.ParameterName = "p_COSTOTOTAL";
                        pCOSTOTOTAL.Value = pPresupuestoEmpresarial.costototal;

                        DbParameter pUTILIDAD = cmdTransaccionFactory.CreateParameter();
                        pUTILIDAD.ParameterName = "p_UTILIDAD";
                        pUTILIDAD.Value = pPresupuestoEmpresarial.utilidad;


                        cmdTransaccionFactory.Parameters.Add(pCOD_PRESUPUESTO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pTOTALACTIVO);
                        cmdTransaccionFactory.Parameters.Add(pTOTALPASIVO);
                        cmdTransaccionFactory.Parameters.Add(pTOTALPATRIMONIO);
                        cmdTransaccionFactory.Parameters.Add(pVENTAMENSUAL);
                        cmdTransaccionFactory.Parameters.Add(pCOSTOTOTAL);
                        cmdTransaccionFactory.Parameters.Add(pUTILIDAD);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_PRESE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pPresupuestoEmpresarial, "PresupuestoEmpresarial",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pPresupuestoEmpresarial.cod_presupuesto = Convert.ToInt64(pCOD_PRESUPUESTO.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPresupuestoEmpresarial;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoEmpresarialData", "CrearPresupuestoEmpresarial", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla PresupuestoEmpresarial de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad PresupuestoEmpresarial modificada</returns>
        public PresupuestoEmpresarial ModificarPresupuestoEmpresarial(PresupuestoEmpresarial pPresupuestoEmpresarial, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_PRESUPUESTO = cmdTransaccionFactory.CreateParameter();
                        pCOD_PRESUPUESTO.ParameterName = "p_COD_PRESUPUESTO";
                        pCOD_PRESUPUESTO.Value = pPresupuestoEmpresarial.cod_presupuesto;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pPresupuestoEmpresarial.cod_persona;

                        DbParameter pTOTALACTIVO = cmdTransaccionFactory.CreateParameter();
                        pTOTALACTIVO.ParameterName = "p_TOTALACTIVO";
                        pTOTALACTIVO.Value = pPresupuestoEmpresarial.totalactivo;

                        DbParameter pTOTALPASIVO = cmdTransaccionFactory.CreateParameter();
                        pTOTALPASIVO.ParameterName = "p_TOTALPASIVO";
                        pTOTALPASIVO.Value = pPresupuestoEmpresarial.totalpasivo;

                        DbParameter pTOTALPATRIMONIO = cmdTransaccionFactory.CreateParameter();
                        pTOTALPATRIMONIO.ParameterName = "p_TOTALPATRIMONIO";
                        pTOTALPATRIMONIO.Value = pPresupuestoEmpresarial.totalpatrimonio;

                        DbParameter pVENTAMENSUAL = cmdTransaccionFactory.CreateParameter();
                        pVENTAMENSUAL.ParameterName = "p_VENTAMENSUAL";
                        pVENTAMENSUAL.Value = pPresupuestoEmpresarial.ventamensual;

                        DbParameter pCOSTOTOTAL = cmdTransaccionFactory.CreateParameter();
                        pCOSTOTOTAL.ParameterName = "p_COSTOTOTAL";
                        pCOSTOTOTAL.Value = pPresupuestoEmpresarial.costototal;

                        DbParameter pUTILIDAD = cmdTransaccionFactory.CreateParameter();
                        pUTILIDAD.ParameterName = "p_UTILIDAD";
                        pUTILIDAD.Value = pPresupuestoEmpresarial.utilidad;

                        cmdTransaccionFactory.Parameters.Add(pCOD_PRESUPUESTO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pTOTALACTIVO);
                        cmdTransaccionFactory.Parameters.Add(pTOTALPASIVO);
                        cmdTransaccionFactory.Parameters.Add(pTOTALPATRIMONIO);
                        cmdTransaccionFactory.Parameters.Add(pVENTAMENSUAL);
                        cmdTransaccionFactory.Parameters.Add(pCOSTOTOTAL);
                        cmdTransaccionFactory.Parameters.Add(pUTILIDAD);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_PRESE_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pPresupuestoEmpresarial, "PresupuestoEmpresarial",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPresupuestoEmpresarial;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoEmpresarialData", "ModificarPresupuestoEmpresarial", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla PresupuestoEmpresarial de la base de datos
        /// </summary>
        /// <param name="pId">identificador de PresupuestoEmpresarial</param>
        public void EliminarPresupuestoEmpresarial(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        PresupuestoEmpresarial pPresupuestoEmpresarial = new PresupuestoEmpresarial();

                        //if (pUsuario.programaGeneraLog)
                        //    pPresupuestoEmpresarial = ConsultarPresupuestoEmpresarial(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_PRESUPUESTO = cmdTransaccionFactory.CreateParameter();
                        pCOD_PRESUPUESTO.ParameterName = "p_COD_PRESUPUESTO";
                        pCOD_PRESUPUESTO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_PRESUPUESTO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_PRESE_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pPresupuestoEmpresarial, "PresupuestoEmpresarial", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoEmpresarialData", "EliminarPresupuestoEmpresarial", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla PresupuestoEmpresarial de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla PresupuestoEmpresarial</param>
        /// <returns>Entidad PresupuestoEmpresarial consultado</returns>
        public PresupuestoEmpresarial ConsultarPresupuestoEmpresarial(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            PresupuestoEmpresarial entidad = new PresupuestoEmpresarial();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  PresupuestoEmpresarial WHERE COD_PRESUPUESTO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PRESUPUESTO"] != DBNull.Value) entidad.cod_presupuesto = Convert.ToInt64(resultado["COD_PRESUPUESTO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TOTALACTIVO"] != DBNull.Value) entidad.totalactivo = Convert.ToInt64(resultado["TOTALACTIVO"]);
                            if (resultado["TOTALPASIVO"] != DBNull.Value) entidad.totalpasivo = Convert.ToInt64(resultado["TOTALPASIVO"]);
                            if (resultado["TOTALPATRIMONIO"] != DBNull.Value) entidad.totalpatrimonio = Convert.ToInt64(resultado["TOTALPATRIMONIO"]);
                            if (resultado["VENTAMENSUAL"] != DBNull.Value) entidad.ventamensual = Convert.ToInt64(resultado["VENTAMENSUAL"]);
                            if (resultado["COSTOTOTAL"] != DBNull.Value) entidad.costototal = Convert.ToInt64(resultado["COSTOTOTAL"]);
                            if (resultado["UTILIDAD"] != DBNull.Value) entidad.utilidad = Convert.ToInt64(resultado["UTILIDAD"]);
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
                        BOExcepcion.Throw("PresupuestoEmpresarialData", "ConsultarPresupuestoEmpresarial", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla PresupuestoEmpresarial dados unos filtros
        /// </summary>
        /// <param name="pPresupuestoEmpresarial">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PresupuestoEmpresarial obtenidos</returns>
        public List<PresupuestoEmpresarial> ListarPresupuestoEmpresarial(PresupuestoEmpresarial pPresupuestoEmpresarial, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PresupuestoEmpresarial> lstPresupuestoEmpresarial = new List<PresupuestoEmpresarial>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  PresupuestoEmpresarial " ;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PresupuestoEmpresarial entidad = new PresupuestoEmpresarial();

                            if (resultado["COD_PRESUPUESTO"] != DBNull.Value) entidad.cod_presupuesto = Convert.ToInt64(resultado["COD_PRESUPUESTO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TOTALACTIVO"] != DBNull.Value) entidad.totalactivo = Convert.ToInt64(resultado["TOTALACTIVO"]);
                            if (resultado["TOTALPASIVO"] != DBNull.Value) entidad.totalpasivo = Convert.ToInt64(resultado["TOTALPASIVO"]);
                            if (resultado["TOTALPATRIMONIO"] != DBNull.Value) entidad.totalpatrimonio = Convert.ToInt64(resultado["TOTALPATRIMONIO"]);
                            if (resultado["VENTAMENSUAL"] != DBNull.Value) entidad.ventamensual = Convert.ToInt64(resultado["VENTAMENSUAL"]);
                            if (resultado["COSTOTOTAL"] != DBNull.Value) entidad.costototal = Convert.ToInt64(resultado["COSTOTOTAL"]);
                            if (resultado["UTILIDAD"] != DBNull.Value) entidad.utilidad = Convert.ToInt64(resultado["UTILIDAD"]);

                            lstPresupuestoEmpresarial.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPresupuestoEmpresarial;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoEmpresarialData", "ListarPresupuestoEmpresarial", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla PresupuestoEmpresarial dados unos filtros
        /// </summary>
        /// <param name="pPresupuestoEmpresarial">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PresupuestoEmpresarial obtenidos</returns>
        public List<PresupuestoEmpresarial> ListarPresupuestoEmpresarialREPO(PresupuestoEmpresarial pPresupuestoEmpresarial, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PresupuestoEmpresarial> lstPresupuestoEmpresarial = new List<PresupuestoEmpresarial>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  PresupuestoEmpresarial where COD_PERSONA = " + pPresupuestoEmpresarial.cod_persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PresupuestoEmpresarial entidad = new PresupuestoEmpresarial();

                            if (resultado["COD_PRESUPUESTO"] != DBNull.Value) entidad.cod_presupuesto = Convert.ToInt64(resultado["COD_PRESUPUESTO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TOTALACTIVO"] != DBNull.Value) entidad.totalactivo = Convert.ToInt64(resultado["TOTALACTIVO"]);
                            if (resultado["TOTALPASIVO"] != DBNull.Value) entidad.totalpasivo = Convert.ToInt64(resultado["TOTALPASIVO"]);
                            if (resultado["TOTALPATRIMONIO"] != DBNull.Value) entidad.totalpatrimonio = Convert.ToInt64(resultado["TOTALPATRIMONIO"]);
                            if (resultado["VENTAMENSUAL"] != DBNull.Value) entidad.ventamensual = Convert.ToInt64(resultado["VENTAMENSUAL"]);
                            if (resultado["COSTOTOTAL"] != DBNull.Value) entidad.costototal = Convert.ToInt64(resultado["COSTOTOTAL"]);
                            if (resultado["UTILIDAD"] != DBNull.Value) entidad.utilidad = Convert.ToInt64(resultado["UTILIDAD"]);

                            lstPresupuestoEmpresarial.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPresupuestoEmpresarial;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoEmpresarialData", "ListarPresupuestoEmpresarialREPO", ex);
                        return null;
                    }
                }
            }
        }

    }
}