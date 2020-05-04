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
    /// Objeto de acceso a datos para la tabla COMPOSICIONPASIVO
    /// </summary>
    public class ComposicionPasivoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla COMPOSICIONPASIVO
        /// </summary>
        public ComposicionPasivoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla COMPOSICIONPASIVO de la base de datos
        /// </summary>
        /// <param name="pComposicionPasivo">Entidad ComposicionPasivo</param>
        /// <returns>Entidad ComposicionPasivo creada</returns>
        public ComposicionPasivo CrearComposicionPasivo(ComposicionPasivo pComposicionPasivo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDPASIVO = cmdTransaccionFactory.CreateParameter();
                        pIDPASIVO.ParameterName = "p_IDPASIVO";
                        pIDPASIVO.Value = 0;
                        pIDPASIVO.Direction = ParameterDirection.InputOutput;

                        DbParameter pCOD_INFFIN = cmdTransaccionFactory.CreateParameter();
                        pCOD_INFFIN.ParameterName = "p_COD_INFFIN";
                        pCOD_INFFIN.Value = pComposicionPasivo.cod_inffin;

                        DbParameter pACREEDOR = cmdTransaccionFactory.CreateParameter();
                        pACREEDOR.ParameterName = "p_ACREEDOR";
                        pACREEDOR.Value = pComposicionPasivo.acreedor;

                        DbParameter pMONTO_OTORGADO = cmdTransaccionFactory.CreateParameter();
                        pMONTO_OTORGADO.ParameterName = "p_MONTO_OTORGADO";
                        pMONTO_OTORGADO.Value = pComposicionPasivo.monto_otorgado;

                        DbParameter pVALOR_CUOTA = cmdTransaccionFactory.CreateParameter();
                        pVALOR_CUOTA.ParameterName = "p_VALOR_CUOTA";
                        pVALOR_CUOTA.Value = pComposicionPasivo.valor_cuota;

                        DbParameter pPERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        pPERIODICIDAD.ParameterName = "p_PERIODICIDAD";
                        pPERIODICIDAD.Value = pComposicionPasivo.periodicidad;

                        DbParameter pCUOTA = cmdTransaccionFactory.CreateParameter();
                        pCUOTA.ParameterName = "p_CUOTA";
                        pCUOTA.Value = pComposicionPasivo.cuota;

                        DbParameter pPLAZO = cmdTransaccionFactory.CreateParameter();
                        pPLAZO.ParameterName = "p_PLAZO";
                        pPLAZO.Value = pComposicionPasivo.plazo;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pComposicionPasivo.cod_persona;

                        cmdTransaccionFactory.Parameters.Add(pIDPASIVO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_INFFIN);
                        cmdTransaccionFactory.Parameters.Add(pACREEDOR);
                        cmdTransaccionFactory.Parameters.Add(pMONTO_OTORGADO);
                        cmdTransaccionFactory.Parameters.Add(pVALOR_CUOTA);
                        cmdTransaccionFactory.Parameters.Add(pPERIODICIDAD);
                        cmdTransaccionFactory.Parameters.Add(pCUOTA);
                        cmdTransaccionFactory.Parameters.Add(pPLAZO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_COMPA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // ACTUALIZA LOS ESTADOS FINANCIEROS                       

                        DbCommand cmdTransaccionFactory2 = dbConnectionFactory.dbProveedorFactory.CreateCommand();

                        DbParameter pCOD_INFFIN1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_INFFIN1.ParameterName = "p_COD_INFFIN1";
                        pCOD_INFFIN1.Value = pComposicionPasivo.cod_inffin;

                        DbParameter pCOD_PERSONA1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_PERSONA1.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA1.Value = pComposicionPasivo.cod_persona;

                        cmdTransaccionFactory2.Parameters.Add(pCOD_INFFIN1);
                        cmdTransaccionFactory2.Parameters.Add(pCOD_PERSONA1);

                        cmdTransaccionFactory2.Connection = connection;
                        cmdTransaccionFactory2.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory2.CommandText = "USP_XPINN_SOLICRED_ESFIN_MODIF";
                        cmdTransaccionFactory2.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pComposicionPasivo, "COMPOSICIONPASIVO",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pComposicionPasivo.idpasivo = Convert.ToInt64(pIDPASIVO.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pComposicionPasivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComposicionPasivoData", "CrearComposicionPasivo", ex);
                        return null;
                    }
                }
            }
        }
        public ComposicionPasivo creaobligacion(ComposicionPasivo pComposicionPasivo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                  
                        DbParameter pCOD_INFFIN = cmdTransaccionFactory.CreateParameter();
                        pCOD_INFFIN.ParameterName = "pCOD_INFFIN";
                        pCOD_INFFIN.Value = pComposicionPasivo.cod_inffin;

                        DbParameter pENTIDAD = cmdTransaccionFactory.CreateParameter();
                        pENTIDAD.ParameterName = "pENTIDAD";
                        pENTIDAD.Value = pComposicionPasivo.entidad;

                         DbParameter pCUPO = cmdTransaccionFactory.CreateParameter();
                        pCUPO.ParameterName = "pCUPO";
                        pCUPO.Value = pComposicionPasivo.cupo;

                         DbParameter pSALDO = cmdTransaccionFactory.CreateParameter();
                        pSALDO.ParameterName = "pSALDO";
                        pSALDO.Value = pComposicionPasivo.saldo;

                        DbParameter pVALOR_CUOTA = cmdTransaccionFactory.CreateParameter();
                        pVALOR_CUOTA.ParameterName = "pVALOR_CUOTA";
                        pVALOR_CUOTA.Value = pComposicionPasivo.cuota;

                        cmdTransaccionFactory.Parameters.Add(pVALOR_CUOTA);
                        cmdTransaccionFactory.Parameters.Add(pSALDO);
                        cmdTransaccionFactory.Parameters.Add(pENTIDAD);
                        cmdTransaccionFactory.Parameters.Add(pCUPO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_INFFIN);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_OBLIGAS_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComposicionPasivoData", "CrearComposicionPasivo", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Modifica un registro en la tabla COMPOSICIONPASIVO de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ComposicionPasivo modificada</returns>
        public ComposicionPasivo ModificarComposicionPasivo(ComposicionPasivo pComposicionPasivo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDPASIVO = cmdTransaccionFactory.CreateParameter();
                        pIDPASIVO.ParameterName = "p_IDPASIVO";
                        pIDPASIVO.Value = pComposicionPasivo.idpasivo;

                        DbParameter pCOD_INFFIN = cmdTransaccionFactory.CreateParameter();
                        pCOD_INFFIN.ParameterName = "p_COD_INFFIN";
                        pCOD_INFFIN.Value = pComposicionPasivo.cod_inffin;

                        DbParameter pACREEDOR = cmdTransaccionFactory.CreateParameter();
                        pACREEDOR.ParameterName = "p_ACREEDOR";
                        pACREEDOR.Value = pComposicionPasivo.acreedor;

                        DbParameter pMONTO_OTORGADO = cmdTransaccionFactory.CreateParameter();
                        pMONTO_OTORGADO.ParameterName = "p_MONTO_OTORGADO";
                        pMONTO_OTORGADO.Value = pComposicionPasivo.monto_otorgado;

                        DbParameter pVALOR_CUOTA = cmdTransaccionFactory.CreateParameter();
                        pVALOR_CUOTA.ParameterName = "p_VALOR_CUOTA";
                        pVALOR_CUOTA.Value = pComposicionPasivo.valor_cuota;

                        DbParameter pPERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        pPERIODICIDAD.ParameterName = "p_PERIODICIDAD";
                        pPERIODICIDAD.Value = pComposicionPasivo.periodicidad;

                        DbParameter pCUOTA = cmdTransaccionFactory.CreateParameter();
                        pCUOTA.ParameterName = "p_CUOTA";
                        pCUOTA.Value = pComposicionPasivo.cuota;

                        DbParameter pPLAZO = cmdTransaccionFactory.CreateParameter();
                        pPLAZO.ParameterName = "p_PLAZO";
                        pPLAZO.Value = pComposicionPasivo.plazo;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pComposicionPasivo.cod_persona;

                        cmdTransaccionFactory.Parameters.Add(pIDPASIVO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_INFFIN);
                        cmdTransaccionFactory.Parameters.Add(pACREEDOR);
                        cmdTransaccionFactory.Parameters.Add(pMONTO_OTORGADO);
                        cmdTransaccionFactory.Parameters.Add(pVALOR_CUOTA);
                        cmdTransaccionFactory.Parameters.Add(pPERIODICIDAD);
                        cmdTransaccionFactory.Parameters.Add(pCUOTA);
                        cmdTransaccionFactory.Parameters.Add(pPLAZO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_COMPA_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // ACTUALIZA LOS ESTADOS FINANCIEROS                       

                        DbCommand cmdTransaccionFactory2 = dbConnectionFactory.dbProveedorFactory.CreateCommand();

                        DbParameter pCOD_INFFIN1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_INFFIN1.ParameterName = "p_COD_INFFIN1";
                        pCOD_INFFIN1.Value = pComposicionPasivo.cod_inffin;

                        DbParameter pCOD_PERSONA1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_PERSONA1.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA1.Value = pComposicionPasivo.cod_persona;

                        cmdTransaccionFactory2.Parameters.Add(pCOD_INFFIN1);
                        cmdTransaccionFactory2.Parameters.Add(pCOD_PERSONA1);

                        cmdTransaccionFactory2.Connection = connection;
                        cmdTransaccionFactory2.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory2.CommandText = "USP_XPINN_SOLICRED_ESFIN_MODIF";
                        cmdTransaccionFactory2.ExecuteNonQuery();


                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pComposicionPasivo, "COMPOSICIONPASIVO",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pComposicionPasivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComposicionPasivoData", "ModificarComposicionPasivo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla COMPOSICIONPASIVO de la base de datos
        /// </summary>
        /// <param name="pId">identificador de COMPOSICIONPASIVO</param>
        public void EliminarComposicionPasivo(Int64 pId, Usuario pUsuario, Int64 Cod_persona, Int64 Cod_InfFin)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ComposicionPasivo pComposicionPasivo = new ComposicionPasivo();

                        //if (pUsuario.programaGeneraLog)
                        //    pComposicionPasivo = ConsultarComposicionPasivo(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pIDPASIVO = cmdTransaccionFactory.CreateParameter();
                        pIDPASIVO.ParameterName = "p_IDPASIVO";
                        pIDPASIVO.Value = pId;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = Cod_persona;

                        cmdTransaccionFactory.Parameters.Add(pIDPASIVO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_COMPA_ELIMI";
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

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pComposicionPasivo, "COMPOSICIONPASIVO", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComposicionPasivoData", "EliminarComposicionPasivo", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla COMPOSICIONPASIVO de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla COMPOSICIONPASIVO</param>
        /// <returns>Entidad ComposicionPasivo consultado</returns>
        public ComposicionPasivo ConsultarComposicionPasivo(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            ComposicionPasivo entidad = new ComposicionPasivo();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  COMPOSICIONPASIVO WHERE IDPASIVO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDPASIVO"] != DBNull.Value) entidad.idpasivo = Convert.ToInt64(resultado["IDPASIVO"]);
                            if (resultado["COD_INFFIN"] != DBNull.Value) entidad.cod_inffin = Convert.ToInt64(resultado["COD_INFFIN"]);
                            if (resultado["ACREEDOR"] != DBNull.Value) entidad.acreedor = Convert.ToString(resultado["ACREEDOR"]);
                            if (resultado["MONTO_OTORGADO"] != DBNull.Value) entidad.monto_otorgado = Convert.ToInt64(resultado["MONTO_OTORGADO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt64(resultado["CUOTA"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);
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
                        BOExcepcion.Throw("ComposicionPasivoData", "ConsultarComposicionPasivo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla COMPOSICIONPASIVO dados unos filtros
        /// </summary>
        /// <param name="pCOMPOSICIONPASIVO">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ComposicionPasivo obtenidos</returns>
        public List<ComposicionPasivo> ListarComposicionPasivo(ComposicionPasivo pComposicionPasivo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ComposicionPasivo> lstComposicionPasivo = new List<ComposicionPasivo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //string sql = "SELECT * FROM  COMPOSICIONPASIVO ";

                        string sql = @"SELECT COMPOSICIONPASIVO.* FROM INFORMACIONFINANCIERA, COMPOSICIONPASIVO 
                                       WHERE 
                                       COMPOSICIONPASIVO.COD_INFFIN = INFORMACIONFINANCIERA.COD_INFFIN AND
                                       INFORMACIONFINANCIERA.COD_PERSONA = " + pComposicionPasivo.cod_persona +
                                       " and INFORMACIONFINANCIERA.COD_INFFIN = " + pComposicionPasivo.cod_inffin;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ComposicionPasivo entidad = new ComposicionPasivo();

                            if (resultado["IDPASIVO"] != DBNull.Value) entidad.idpasivo = Convert.ToInt64(resultado["IDPASIVO"]);
                            if (resultado["COD_INFFIN"] != DBNull.Value) entidad.cod_inffin = Convert.ToInt64(resultado["COD_INFFIN"]);
                            if (resultado["ACREEDOR"] != DBNull.Value) entidad.acreedor = Convert.ToString(resultado["ACREEDOR"]);
                            if (resultado["MONTO_OTORGADO"] != DBNull.Value) entidad.monto_otorgado = Convert.ToInt64(resultado["MONTO_OTORGADO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt64(resultado["CUOTA"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);

                            lstComposicionPasivo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstComposicionPasivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComposicionPasivoData", "ListarComposicionPasivo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla COMPOSICIONPASIVO dados unos filtros
        /// </summary>
        /// <param name="pCOMPOSICIONPASIVO">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ComposicionPasivo obtenidos</returns>
        public List<ComposicionPasivo> ListarComposicionPasivoRepo(ComposicionPasivo pComposicionPasivo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ComposicionPasivo> lstComposicionPasivo = new List<ComposicionPasivo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //string sql = "SELECT * FROM  COMPOSICIONPASIVO ";

                        string sql = @"SELECT COMPOSICIONPASIVO.* FROM INFORMACIONFINANCIERA, COMPOSICIONPASIVO 
                                       WHERE COMPOSICIONPASIVO.COD_INFFIN = INFORMACIONFINANCIERA.COD_INFFIN AND
                                       INFORMACIONFINANCIERA.COD_PERSONA = " + pComposicionPasivo.cod_persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ComposicionPasivo entidad = new ComposicionPasivo();

                            if (resultado["IDPASIVO"] != DBNull.Value) entidad.idpasivo = Convert.ToInt64(resultado["IDPASIVO"]);
                            if (resultado["COD_INFFIN"] != DBNull.Value) entidad.cod_inffin = Convert.ToInt64(resultado["COD_INFFIN"]);
                            if (resultado["ACREEDOR"] != DBNull.Value) entidad.acreedor = Convert.ToString(resultado["ACREEDOR"]);
                            if (resultado["MONTO_OTORGADO"] != DBNull.Value) entidad.monto_otorgado = Convert.ToInt64(resultado["MONTO_OTORGADO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt64(resultado["CUOTA"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);

                            lstComposicionPasivo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstComposicionPasivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComposicionPasivoData", "ListarComposicionPasivoRepo", ex);
                        return null;
                    }
                }
            }
        }

        public List<ComposicionPasivo> Listarobligacion(long infin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ComposicionPasivo> lstComposicionPasivo = new List<ComposicionPasivo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //string sql = "SELECT * FROM  COMPOSICIONPASIVO ";

                        string sql = "SELECT  ENTIDAD,  CUPO,  SALDO,  VALOR_CUOTA FROM OBLIGACIONPERSONAS where cod_inffin = " + infin;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ComposicionPasivo entidad = new ComposicionPasivo();

                            if (resultado["ENTIDAD"] != DBNull.Value) entidad.entidad = Convert.ToInt64(resultado["ENTIDAD"]);
                            if (resultado["CUPO"] != DBNull.Value) entidad.cupo = Convert.ToInt64(resultado["CUPO"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["SALDO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                         
                            lstComposicionPasivo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstComposicionPasivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComposicionPasivoData", "ListarComposicionPasivo", ex);
                        return null;
                    }
                }
            }
        }
    }
}