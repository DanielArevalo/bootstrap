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
    /// Objeto de acceso a datos para la tabla BalanceFamilia
    /// </summary>
    public class BalanceFamiliaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla BalanceFamilia
        /// </summary>
        public BalanceFamiliaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla BalanceFamilia de la base de datos
        /// </summary>
        /// <param name="pBalanceFamilia">Entidad BalanceFamilia</param>
        /// <returns>Entidad BalanceFamilia creada</returns>
        public BalanceFamilia CrearBalanceFamilia(BalanceFamilia pBalanceFamilia, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_BALANCE = cmdTransaccionFactory.CreateParameter();
                        pCOD_BALANCE.ParameterName = "p_COD_BALANCE";
                        pCOD_BALANCE.Value = 0;
                        pCOD_BALANCE.Direction = ParameterDirection.Output;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pBalanceFamilia.cod_persona;

                        DbParameter pTERRENOSYEDIFICIOS = cmdTransaccionFactory.CreateParameter();
                        pTERRENOSYEDIFICIOS.ParameterName = "p_TERRENOSYEDIFICIOS";
                        pTERRENOSYEDIFICIOS.Value = pBalanceFamilia.terrenosyedificios;

                        DbParameter pOTROS = cmdTransaccionFactory.CreateParameter();
                        pOTROS.ParameterName = "p_OTROS";
                        pOTROS.Value = pBalanceFamilia.otros;

                        DbParameter pTOTALACTIVO = cmdTransaccionFactory.CreateParameter();
                        pTOTALACTIVO.ParameterName = "p_TOTALACTIVO";
                        pTOTALACTIVO.Value = pBalanceFamilia.totalactivo;

                        DbParameter pCORRIENTE = cmdTransaccionFactory.CreateParameter();
                        pCORRIENTE.ParameterName = "p_CORRIENTE";
                        pCORRIENTE.Value = pBalanceFamilia.corriente;

                        DbParameter pLARGOPLAZO = cmdTransaccionFactory.CreateParameter();
                        pLARGOPLAZO.ParameterName = "p_LARGOPLAZO";
                        pLARGOPLAZO.Value = pBalanceFamilia.largoplazo;

                        DbParameter pTOTALPASIVO = cmdTransaccionFactory.CreateParameter();
                        pTOTALPASIVO.ParameterName = "p_TOTALPASIVO";
                        pTOTALPASIVO.Value = pBalanceFamilia.totalpasivo;

                        DbParameter pPATRIMONIO = cmdTransaccionFactory.CreateParameter();
                        pPATRIMONIO.ParameterName = "p_PATRIMONIO";
                        pPATRIMONIO.Value = pBalanceFamilia.patrimonio;

                        DbParameter pTOTALPASIVOYPATRIMONIO = cmdTransaccionFactory.CreateParameter();
                        pTOTALPASIVOYPATRIMONIO.ParameterName = "p_TOTALPASIVOYPATRIMONIO";
                        pTOTALPASIVOYPATRIMONIO.Value = pBalanceFamilia.totalpasivoypatrimonio;

                        DbParameter pCOD_INFFIN = cmdTransaccionFactory.CreateParameter();
                        pCOD_INFFIN.ParameterName = "p_COD_INFFIN";
                        pCOD_INFFIN.Value = pBalanceFamilia.cod_inffin;

                        cmdTransaccionFactory.Parameters.Add(pCOD_BALANCE);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pTERRENOSYEDIFICIOS);
                        cmdTransaccionFactory.Parameters.Add(pOTROS);
                        cmdTransaccionFactory.Parameters.Add(pTOTALACTIVO);
                        cmdTransaccionFactory.Parameters.Add(pCORRIENTE);
                        cmdTransaccionFactory.Parameters.Add(pLARGOPLAZO);
                        cmdTransaccionFactory.Parameters.Add(pTOTALPASIVO);
                        cmdTransaccionFactory.Parameters.Add(pPATRIMONIO);
                        cmdTransaccionFactory.Parameters.Add(pTOTALPASIVOYPATRIMONIO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_INFFIN);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_BALFA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // ACTUALIZA LOS ESTADOS FINANCIEROS                       

                        DbCommand cmdTransaccionFactory2 = dbConnectionFactory.dbProveedorFactory.CreateCommand();

                        DbParameter pCOD_INFFIN1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_INFFIN1.ParameterName = "p_COD_INFFIN1";
                        pCOD_INFFIN1.Value = pBalanceFamilia.cod_inffin;

                        DbParameter pCOD_PERSONA1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_PERSONA1.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA1.Value = pBalanceFamilia.cod_persona;

                        cmdTransaccionFactory2.Parameters.Add(pCOD_INFFIN1);
                        cmdTransaccionFactory2.Parameters.Add(pCOD_PERSONA1);

                        cmdTransaccionFactory2.Connection = connection;
                        cmdTransaccionFactory2.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory2.CommandText = "USP_XPINN_SOLICRED_ESFIN_MODIF";
                        cmdTransaccionFactory2.ExecuteNonQuery();
                        
                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pBalanceFamilia, "BalanceFamilia",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pBalanceFamilia.cod_balance = Convert.ToInt64(pCOD_BALANCE.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pBalanceFamilia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceFamiliaData", "CrearBalanceFamilia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla BalanceFamilia de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad BalanceFamilia modificada</returns>
        public BalanceFamilia ModificarBalanceFamilia(BalanceFamilia pBalanceFamilia, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_BALANCE = cmdTransaccionFactory.CreateParameter();
                        pCOD_BALANCE.ParameterName = "p_COD_BALANCE";
                        pCOD_BALANCE.Value = pBalanceFamilia.cod_balance;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pBalanceFamilia.cod_persona;

                        DbParameter pTERRENOSYEDIFICIOS = cmdTransaccionFactory.CreateParameter();
                        pTERRENOSYEDIFICIOS.ParameterName = "p_TERRENOSYEDIFICIOS";
                        pTERRENOSYEDIFICIOS.Value = pBalanceFamilia.terrenosyedificios;

                        DbParameter pOTROS = cmdTransaccionFactory.CreateParameter();
                        pOTROS.ParameterName = "p_OTROS";
                        pOTROS.Value = pBalanceFamilia.otros;

                        DbParameter pTOTALACTIVO = cmdTransaccionFactory.CreateParameter();
                        pTOTALACTIVO.ParameterName = "p_TOTALACTIVO";
                        pTOTALACTIVO.Value = pBalanceFamilia.totalactivo;

                        DbParameter pCORRIENTE = cmdTransaccionFactory.CreateParameter();
                        pCORRIENTE.ParameterName = "p_CORRIENTE";
                        pCORRIENTE.Value = pBalanceFamilia.corriente;

                        DbParameter pLARGOPLAZO = cmdTransaccionFactory.CreateParameter();
                        pLARGOPLAZO.ParameterName = "p_LARGOPLAZO";
                        pLARGOPLAZO.Value = pBalanceFamilia.largoplazo;

                        DbParameter pTOTALPASIVO = cmdTransaccionFactory.CreateParameter();
                        pTOTALPASIVO.ParameterName = "p_TOTALPASIVO";
                        pTOTALPASIVO.Value = pBalanceFamilia.totalpasivo;

                        DbParameter pPATRIMONIO = cmdTransaccionFactory.CreateParameter();
                        pPATRIMONIO.ParameterName = "p_PATRIMONIO";
                        pPATRIMONIO.Value = pBalanceFamilia.patrimonio;

                        DbParameter pTOTALPASIVOYPATRIMONIO = cmdTransaccionFactory.CreateParameter();
                        pTOTALPASIVOYPATRIMONIO.ParameterName = "p_TOTALPASIVOYPATRIMONIO";
                        pTOTALPASIVOYPATRIMONIO.Value = pBalanceFamilia.totalpasivoypatrimonio;

                        DbParameter pCOD_INFFIN = cmdTransaccionFactory.CreateParameter();
                        pCOD_INFFIN.ParameterName = "p_COD_INFFIN";
                        pCOD_INFFIN.Value = pBalanceFamilia.cod_inffin;

                        cmdTransaccionFactory.Parameters.Add(pCOD_BALANCE);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pTERRENOSYEDIFICIOS);
                        cmdTransaccionFactory.Parameters.Add(pOTROS);
                        cmdTransaccionFactory.Parameters.Add(pTOTALACTIVO);
                        cmdTransaccionFactory.Parameters.Add(pCORRIENTE);
                        cmdTransaccionFactory.Parameters.Add(pLARGOPLAZO);
                        cmdTransaccionFactory.Parameters.Add(pTOTALPASIVO);
                        cmdTransaccionFactory.Parameters.Add(pPATRIMONIO);
                        cmdTransaccionFactory.Parameters.Add(pTOTALPASIVOYPATRIMONIO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_INFFIN);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_BALFA_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // ACTUALIZA LOS ESTADOS FINANCIEROS                       

                        DbCommand cmdTransaccionFactory2 = dbConnectionFactory.dbProveedorFactory.CreateCommand();

                        DbParameter pCOD_INFFIN1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_INFFIN1.ParameterName = "p_COD_INFFIN1";
                        pCOD_INFFIN1.Value = pBalanceFamilia.cod_inffin;

                        DbParameter pCOD_PERSONA1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_PERSONA1.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA1.Value = pBalanceFamilia.cod_persona;

                        cmdTransaccionFactory2.Parameters.Add(pCOD_INFFIN1);
                        cmdTransaccionFactory2.Parameters.Add(pCOD_PERSONA1);

                        cmdTransaccionFactory2.Connection = connection;
                        cmdTransaccionFactory2.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory2.CommandText = "USP_XPINN_SOLICRED_ESFIN_MODIF";
                        cmdTransaccionFactory2.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pBalanceFamilia, "BalanceFamilia",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pBalanceFamilia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceFamiliaData", "ModificarBalanceFamilia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla BalanceFamilia de la base de datos
        /// </summary>
        /// <param name="pId">identificador de BalanceFamilia</param>
        public void EliminarBalanceFamilia(Int64 pId, Usuario pUsuario, Int64 Cod_persona, Int64 Cod_InfFin)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        BalanceFamilia pBalanceFamilia = new BalanceFamilia();

                        //if (pUsuario.programaGeneraLog)
                        //    pBalanceFamilia = ConsultarBalanceFamilia(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_BALANCE = cmdTransaccionFactory.CreateParameter();
                        pCOD_BALANCE.ParameterName = "p_COD_BALANCE";
                        pCOD_BALANCE.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_BALANCE);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_BALFA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // ACTUALIZA LOS ESTADOS FINANCIEROS                       

                        DbCommand cmdTransaccionFactory2 = dbConnectionFactory.dbProveedorFactory.CreateCommand();

                        DbParameter pCOD_INFFIN1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_INFFIN1.ParameterName = "p_COD_INFFIN1";
                        pCOD_INFFIN1.Value = Cod_InfFin;

                        DbParameter pCOD_PERSONA1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_PERSONA1.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA1.Value = Cod_persona;

                        cmdTransaccionFactory2.Parameters.Add(pCOD_INFFIN1);
                        cmdTransaccionFactory2.Parameters.Add(pCOD_PERSONA1);

                        cmdTransaccionFactory2.Connection = connection;
                        cmdTransaccionFactory2.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory2.CommandText = "USP_XPINN_SOLICRED_ESFIN_MODIF";
                        cmdTransaccionFactory2.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pBalanceFamilia, "BalanceFamilia", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceFamiliaData", "EliminarBalanceFamilia", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla BalanceFamilia de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla BalanceFamilia</param>
        /// <returns>Entidad BalanceFamilia consultado</returns>
        public BalanceFamilia ConsultarBalanceFamilia(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            BalanceFamilia entidad = new BalanceFamilia();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  BALANCEFAMILIA WHERE COD_BALANCE = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_BALANCE"] != DBNull.Value) entidad.cod_balance = Convert.ToInt64(resultado["COD_BALANCE"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TERRENOSYEDIFICIOS"] != DBNull.Value) entidad.terrenosyedificios = Convert.ToInt64(resultado["TERRENOSYEDIFICIOS"]);
                            if (resultado["OTROS"] != DBNull.Value) entidad.otros = Convert.ToInt64(resultado["OTROS"]);
                            if (resultado["TOTALACTIVO"] != DBNull.Value) entidad.totalactivo = Convert.ToInt64(resultado["TOTALACTIVO"]);
                            if (resultado["CORRIENTE"] != DBNull.Value) entidad.corriente = Convert.ToInt64(resultado["CORRIENTE"]);
                            if (resultado["LARGOPLAZO"] != DBNull.Value) entidad.largoplazo = Convert.ToInt64(resultado["LARGOPLAZO"]);
                            if (resultado["TOTALPASIVO"] != DBNull.Value) entidad.totalpasivo = Convert.ToInt64(resultado["TOTALPASIVO"]);
                            if (resultado["PATRIMONIO"] != DBNull.Value) entidad.patrimonio = Convert.ToInt64(resultado["PATRIMONIO"]);
                            if (resultado["TOTALPASIVOYPATRIMONIO"] != DBNull.Value) entidad.totalpasivoypatrimonio = Convert.ToInt64(resultado["TOTALPASIVOYPATRIMONIO"]);
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
                        BOExcepcion.Throw("BalanceFamiliaData", "ConsultarBalanceFamilia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla BalanceFamilia dados unos filtros
        /// </summary>
        /// <param name="pBalanceFamilia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de BalanceFamilia obtenidos</returns>
        public List<BalanceFamilia> ListarBalanceFamilia(BalanceFamilia pBalanceFamilia, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<BalanceFamilia> lstBalanceFamilia = new List<BalanceFamilia>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                       // string sql = "SELECT * FROM  BALANCEFAMILIA " ;

                        string sql = @"SELECT BALANCEFAMILIA.* FROM INFORMACIONFINANCIERA, BALANCEFAMILIA 
                                       WHERE 
                                       BALANCEFAMILIA.COD_INFFIN = INFORMACIONFINANCIERA.COD_INFFIN AND
                                       INFORMACIONFINANCIERA.COD_PERSONA = " + pBalanceFamilia.cod_persona +
                                       " and BALANCEFAMILIA.COD_INFFIN = " + pBalanceFamilia.cod_inffin;


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            BalanceFamilia entidad = new BalanceFamilia();

                            if (resultado["COD_BALANCE"] != DBNull.Value) entidad.cod_balance = Convert.ToInt64(resultado["COD_BALANCE"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TERRENOSYEDIFICIOS"] != DBNull.Value) entidad.terrenosyedificios = Convert.ToInt64(resultado["TERRENOSYEDIFICIOS"]);
                            if (resultado["OTROS"] != DBNull.Value) entidad.otros = Convert.ToInt64(resultado["OTROS"]);
                            if (resultado["TOTALACTIVO"] != DBNull.Value) entidad.totalactivo = Convert.ToInt64(resultado["TOTALACTIVO"]);
                            if (resultado["CORRIENTE"] != DBNull.Value) entidad.corriente = Convert.ToInt64(resultado["CORRIENTE"]);
                            if (resultado["LARGOPLAZO"] != DBNull.Value) entidad.largoplazo = Convert.ToInt64(resultado["LARGOPLAZO"]);
                            if (resultado["TOTALPASIVO"] != DBNull.Value) entidad.totalpasivo = Convert.ToInt64(resultado["TOTALPASIVO"]);
                            if (resultado["PATRIMONIO"] != DBNull.Value) entidad.patrimonio = Convert.ToInt64(resultado["PATRIMONIO"]);
                            if (resultado["TOTALPASIVOYPATRIMONIO"] != DBNull.Value) entidad.totalpasivoypatrimonio = Convert.ToInt64(resultado["TOTALPASIVOYPATRIMONIO"]);

                            lstBalanceFamilia.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstBalanceFamilia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceFamiliaData", "ListarBalanceFamilia", ex);
                        return null;
                    }
                }
            }
        }

    }
}