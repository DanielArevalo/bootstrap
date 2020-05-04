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
    /// Objeto de acceso a datos para la tabla Viabilidad
    /// </summary>
    public class ViabilidadFinancieraData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Viabilidad
        /// </summary>
        public ViabilidadFinancieraData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla Viabilidad de la base de datos
        /// </summary>
        /// <param name="pViabilidadFinanciera">Entidad ViabilidadFinanciera</param>
        /// <returns>Entidad ViabilidadFinanciera creada</returns>
        public ViabilidadFinanciera CrearViabilidadFinanciera(ViabilidadFinanciera pViabilidadFinanciera, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_VIABILIDAD = cmdTransaccionFactory.CreateParameter();
                        pCOD_VIABILIDAD.ParameterName = "p_COD_VIABILIDAD";
                        pCOD_VIABILIDAD.Value = pViabilidadFinanciera.cod_viabilidad;
                        pCOD_VIABILIDAD.Direction = ParameterDirection.Output;

                        DbParameter pNUMEROSOLICITUD = cmdTransaccionFactory.CreateParameter();
                        pNUMEROSOLICITUD.ParameterName = "pNUMEROSOLICITUD";
                        pNUMEROSOLICITUD.Value = pViabilidadFinanciera.numeroSolicitud;
                        pNUMEROSOLICITUD.Direction = ParameterDirection.Input;

                        DbParameter pIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pIDENTIFICACION.ParameterName = "pIDENTIFICACION";
                        pIDENTIFICACION.Value = pViabilidadFinanciera.identificacion;
                        pIDENTIFICACION.Direction = ParameterDirection.Input;

                        DbParameter pPRUEBA = cmdTransaccionFactory.CreateParameter();
                        pPRUEBA.ParameterName = "pPRUEBA";
                        pPRUEBA.Value = pViabilidadFinanciera.endeudamiento;
                        pPRUEBA.Direction = ParameterDirection.Output;

                        DbParameter pENDEUDAMIENTO = cmdTransaccionFactory.CreateParameter();
                        pENDEUDAMIENTO.ParameterName = "pENDEUDAMIENTO";
                        pENDEUDAMIENTO.Value = pViabilidadFinanciera.endeudamiento;
                        pENDEUDAMIENTO.Direction = ParameterDirection.Output;

                        DbParameter pROTACIONCUENTAS = cmdTransaccionFactory.CreateParameter();
                        pROTACIONCUENTAS.ParameterName = "pROTACIONCUENTAS";
                        pROTACIONCUENTAS.Value = pViabilidadFinanciera.rotacioncuentas;
                        pROTACIONCUENTAS.Direction = ParameterDirection.Output;

                        DbParameter pGASTOS = cmdTransaccionFactory.CreateParameter();
                        pGASTOS.ParameterName = "pGASTOS";
                        pGASTOS.Value = pViabilidadFinanciera.gastos;
                        pGASTOS.Direction = ParameterDirection.Output;

                        DbParameter pROTACIONCUENTASPAGAR = cmdTransaccionFactory.CreateParameter();
                        pROTACIONCUENTASPAGAR.ParameterName = "pROTACIONCUENTASPAGAR";
                        pROTACIONCUENTASPAGAR.Value = pViabilidadFinanciera.rotacioncuentaspagar;
                        pROTACIONCUENTASPAGAR.Direction = ParameterDirection.Output;

                        DbParameter pROTACIONCAPITAL = cmdTransaccionFactory.CreateParameter();
                        pROTACIONCAPITAL.ParameterName = "pROTACIONCAPITAL";
                        pROTACIONCAPITAL.Value = pViabilidadFinanciera.rotacioncapital;
                        pROTACIONCAPITAL.Direction = ParameterDirection.Output;

                        DbParameter pROTACIONINVENTARIOS = cmdTransaccionFactory.CreateParameter();
                        pROTACIONINVENTARIOS.ParameterName = "pROTACIONINVENTARIOS";
                        pROTACIONINVENTARIOS.Value = pViabilidadFinanciera.rotacioninventarios;
                        pROTACIONINVENTARIOS.Direction = ParameterDirection.Output;

                        DbParameter pPUNTOEQUILIBRIO = cmdTransaccionFactory.CreateParameter();
                        pPUNTOEQUILIBRIO.ParameterName = "pPUNTOEQUILIBRIO";
                        pPUNTOEQUILIBRIO.Value = pViabilidadFinanciera.puntoequilibrio;
                        pPUNTOEQUILIBRIO.Direction = ParameterDirection.Output;

                        DbParameter pEF = cmdTransaccionFactory.CreateParameter();
                        pEF.ParameterName = "pEF";
                        pEF.Value = pViabilidadFinanciera.ef;
                        pEF.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(pCOD_VIABILIDAD);
                        cmdTransaccionFactory.Parameters.Add(pNUMEROSOLICITUD);
                        cmdTransaccionFactory.Parameters.Add(pIDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pPRUEBA);
                        cmdTransaccionFactory.Parameters.Add(pENDEUDAMIENTO);
                        cmdTransaccionFactory.Parameters.Add(pROTACIONCUENTAS);
                        cmdTransaccionFactory.Parameters.Add(pGASTOS);
                        cmdTransaccionFactory.Parameters.Add(pROTACIONCUENTASPAGAR);
                        cmdTransaccionFactory.Parameters.Add(pROTACIONCAPITAL);
                        cmdTransaccionFactory.Parameters.Add(pROTACIONINVENTARIOS);
                        cmdTransaccionFactory.Parameters.Add(pPUNTOEQUILIBRIO);
                        cmdTransaccionFactory.Parameters.Add(pEF);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_VIABI_CALCU";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pViabilidadFinanciera.cod_viabilidad = Convert.ToInt64(pCOD_VIABILIDAD.Value);
                        pViabilidadFinanciera.prueba = Convert.ToDouble(pPRUEBA.Value);
                        pViabilidadFinanciera.endeudamiento = Convert.ToDouble(pENDEUDAMIENTO.Value);
                        pViabilidadFinanciera.rotacioncuentas = Convert.ToDouble(pROTACIONCUENTAS.Value);
                        pViabilidadFinanciera.gastos = Convert.ToDouble(pGASTOS.Value);
                        pViabilidadFinanciera.rotacioncuentaspagar = Convert.ToDouble(pROTACIONCUENTASPAGAR.Value);
                        pViabilidadFinanciera.rotacioncapital = Convert.ToDouble(pROTACIONCAPITAL.Value);
                        pViabilidadFinanciera.rotacioninventarios = Convert.ToDouble(pROTACIONINVENTARIOS.Value);
                        pViabilidadFinanciera.puntoequilibrio = Convert.ToDouble(pPUNTOEQUILIBRIO.Value);
                        pViabilidadFinanciera.ef = Convert.ToDouble(pEF.Value);

                        connection.Close();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pViabilidadFinanciera, "VIABILIDAD", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        //pViabilidadFinanciera.cod_viabilidad = Convert.ToInt64(pCOD_VIABILIDAD.Value);
                        return pViabilidadFinanciera;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ViabilidadFinancieraData", "CrearViabilidadFinanciera", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla Viabilidad de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ViabilidadFinanciera modificada</returns>
        public ViabilidadFinanciera ModificarViabilidadFinanciera(ViabilidadFinanciera pViabilidadFinanciera, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_VIABILIDAD = cmdTransaccionFactory.CreateParameter();
                        pCOD_VIABILIDAD.ParameterName = "p_COD_VIABILIDAD";
                        pCOD_VIABILIDAD.Value = pViabilidadFinanciera.cod_viabilidad;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pViabilidadFinanciera.cod_persona;

                        DbParameter pDATACREDITO = cmdTransaccionFactory.CreateParameter();
                        pDATACREDITO.ParameterName = "p_DATACREDITO";
                        pDATACREDITO.Value = pViabilidadFinanciera.datacredito;

                        DbParameter pDISPONIBLE = cmdTransaccionFactory.CreateParameter();
                        pDISPONIBLE.ParameterName = "p_DISPONIBLE";
                        pDISPONIBLE.Value = pViabilidadFinanciera.disponible;

                        DbParameter pPRUEBA = cmdTransaccionFactory.CreateParameter();
                        pPRUEBA.ParameterName = "p_PRUEBA";
                        pPRUEBA.Value = pViabilidadFinanciera.prueba;

                        DbParameter pGASTOS = cmdTransaccionFactory.CreateParameter();
                        pGASTOS.ParameterName = "p_GASTOS";
                        pGASTOS.Value = pViabilidadFinanciera.gastos;

                        DbParameter pROTACIONCUENTAS = cmdTransaccionFactory.CreateParameter();
                        pROTACIONCUENTAS.ParameterName = "p_ROTACIONCUENTAS";
                        pROTACIONCUENTAS.Value = pViabilidadFinanciera.rotacioncuentas;

                        DbParameter pROTACIONCAPITAL = cmdTransaccionFactory.CreateParameter();
                        pROTACIONCAPITAL.ParameterName = "p_ROTACIONCAPITAL";
                        pROTACIONCAPITAL.Value = pViabilidadFinanciera.rotacioncapital;

                        DbParameter pROTACIONCUENTASPAGAR = cmdTransaccionFactory.CreateParameter();
                        pROTACIONCUENTASPAGAR.ParameterName = "p_ROTACIONCUENTASPAGAR";
                        pROTACIONCUENTASPAGAR.Value = pViabilidadFinanciera.rotacioncuentaspagar;

                        DbParameter pPUNTOEQUILIBRIO = cmdTransaccionFactory.CreateParameter();
                        pPUNTOEQUILIBRIO.ParameterName = "p_PUNTOEQUILIBRIO";
                        pPUNTOEQUILIBRIO.Value = pViabilidadFinanciera.puntoequilibrio;

                        DbParameter pROTACIONINVENTARIOS = cmdTransaccionFactory.CreateParameter();
                        pROTACIONINVENTARIOS.ParameterName = "p_ROTACIONINVENTARIOS";
                        pROTACIONINVENTARIOS.Value = pViabilidadFinanciera.rotacioninventarios;

                        DbParameter pEF = cmdTransaccionFactory.CreateParameter();
                        pEF.ParameterName = "p_EF";
                        pEF.Value = pViabilidadFinanciera.ef;

                        DbParameter pENDEUDAMIENTO = cmdTransaccionFactory.CreateParameter();
                        pENDEUDAMIENTO.ParameterName = "p_ENDEUDAMIENTO";
                        pENDEUDAMIENTO.Value = pViabilidadFinanciera.endeudamiento;

                        DbParameter pOBSERVACIONES = cmdTransaccionFactory.CreateParameter();
                        pOBSERVACIONES.ParameterName = "p_OBSERVACIONES";
                        pOBSERVACIONES.Value = pViabilidadFinanciera.observaciones;

                        cmdTransaccionFactory.Parameters.Add(pCOD_VIABILIDAD);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pDATACREDITO);
                        cmdTransaccionFactory.Parameters.Add(pDISPONIBLE);
                        cmdTransaccionFactory.Parameters.Add(pPRUEBA);
                        cmdTransaccionFactory.Parameters.Add(pGASTOS);
                        cmdTransaccionFactory.Parameters.Add(pROTACIONCUENTAS);
                        cmdTransaccionFactory.Parameters.Add(pROTACIONCAPITAL);
                        cmdTransaccionFactory.Parameters.Add(pROTACIONCUENTASPAGAR);
                        cmdTransaccionFactory.Parameters.Add(pPUNTOEQUILIBRIO);
                        cmdTransaccionFactory.Parameters.Add(pROTACIONINVENTARIOS);
                        cmdTransaccionFactory.Parameters.Add(pEF);
                        cmdTransaccionFactory.Parameters.Add(pENDEUDAMIENTO);
                        cmdTransaccionFactory.Parameters.Add(pOBSERVACIONES);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_VIABI_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pViabilidadFinanciera, "Viabilidad", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);
                        return pViabilidadFinanciera;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ViabilidadFinancieraData", "ModificarViabilidadFinanciera", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla Viabilidad de la base de datos
        /// </summary>
        /// <param name="pId">identificador de Viabilidad</param>
        public void EliminarViabilidadFinanciera(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ViabilidadFinanciera pViabilidadFinanciera = new ViabilidadFinanciera();

                        //if (pUsuario.programaGeneraLog)
                        //    pViabilidadFinanciera = ConsultarViabilidadFinanciera(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_VIABILIDAD = cmdTransaccionFactory.CreateParameter();
                        pCOD_VIABILIDAD.ParameterName = "p_COD_VIABILIDAD";
                        pCOD_VIABILIDAD.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_VIABILIDAD);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_VIABI_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pViabilidadFinanciera, "Viabilidad", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ViabilidadFinancieraData", "EliminarViabilidadFinanciera", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla Viabilidad de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Viabilidad</param>
        /// <returns>Entidad ViabilidadFinanciera consultado</returns>
        public ViabilidadFinanciera ConsultarViabilidadFinanciera(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            ViabilidadFinanciera entidad = new ViabilidadFinanciera();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  VIABILIDAD WHERE COD_VIABILIDAD = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_VIABILIDAD"] != DBNull.Value) entidad.cod_viabilidad = Convert.ToInt64(resultado["COD_VIABILIDAD"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["DATACREDITO"] != DBNull.Value) entidad.datacredito = Convert.ToDouble(resultado["DATACREDITO"]);
                            if (resultado["DISPONIBLE"] != DBNull.Value) entidad.disponible = Convert.ToDouble(resultado["DISPONIBLE"]);
                            if (resultado["PRUEBA"] != DBNull.Value) entidad.prueba = Convert.ToDouble(resultado["PRUEBA"]);
                            if (resultado["GASTOS"] != DBNull.Value) entidad.gastos = Convert.ToDouble(resultado["GASTOS"]);
                            if (resultado["ROTACIONCUENTAS"] != DBNull.Value) entidad.rotacioncuentas = Convert.ToDouble(resultado["ROTACIONCUENTAS"]);
                            if (resultado["ROTACIONCAPITAL"] != DBNull.Value) entidad.rotacioncapital = Convert.ToDouble(resultado["ROTACIONCAPITAL"]);
                            if (resultado["ROTACIONCUENTASPAGAR"] != DBNull.Value) entidad.rotacioncuentaspagar = Convert.ToDouble(resultado["ROTACIONCUENTASPAGAR"]);
                            if (resultado["PUNTOEQUILIBRIO"] != DBNull.Value) entidad.puntoequilibrio = Convert.ToDouble(resultado["PUNTOEQUILIBRIO"]);
                            if (resultado["ROTACIONINVENTARIOS"] != DBNull.Value) entidad.rotacioninventarios = Convert.ToDouble(resultado["ROTACIONINVENTARIOS"]);
                            if (resultado["EF"] != DBNull.Value) entidad.ef = Convert.ToDouble(resultado["EF"]);
                            if (resultado["ENDEUDAMIENTO"] != DBNull.Value) entidad.endeudamiento = Convert.ToDouble(resultado["ENDEUDAMIENTO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
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
                        BOExcepcion.Throw("ViabilidadFinancieraData", "ConsultarViabilidadFinanciera", ex);
                        return null;
                    }
                }
            }
        }/// <summary>
        /// Obtiene un registro en la tabla Viabilidad de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Viabilidad</param>
        /// <returns>Entidad ViabilidadFinanciera consultado</returns>
        public ViabilidadFinanciera ConsultarViabilidadFin_Control(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            ViabilidadFinanciera entidad = new ViabilidadFinanciera();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  VIABILIDAD WHERE numero_solicitud = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_VIABILIDAD"] != DBNull.Value) entidad.cod_viabilidad = Convert.ToInt64(resultado["COD_VIABILIDAD"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["DATACREDITO"] != DBNull.Value) entidad.datacredito = Convert.ToDouble(resultado["DATACREDITO"]);
                            if (resultado["DISPONIBLE"] != DBNull.Value) entidad.disponible = Convert.ToDouble(resultado["DISPONIBLE"]);
                            if (resultado["PRUEBA"] != DBNull.Value) entidad.prueba = Convert.ToDouble(resultado["PRUEBA"]);
                            if (resultado["GASTOS"] != DBNull.Value) entidad.gastos = Convert.ToDouble(resultado["GASTOS"]);
                            if (resultado["ROTACIONCUENTAS"] != DBNull.Value) entidad.rotacioncuentas = Convert.ToDouble(resultado["ROTACIONCUENTAS"]);
                            if (resultado["ROTACIONCAPITAL"] != DBNull.Value) entidad.rotacioncapital = Convert.ToDouble(resultado["ROTACIONCAPITAL"]);
                            if (resultado["ROTACIONCUENTASPAGAR"] != DBNull.Value) entidad.rotacioncuentaspagar = Convert.ToDouble(resultado["ROTACIONCUENTASPAGAR"]);
                            if (resultado["PUNTOEQUILIBRIO"] != DBNull.Value) entidad.puntoequilibrio = Convert.ToDouble(resultado["PUNTOEQUILIBRIO"]);
                            if (resultado["ROTACIONINVENTARIOS"] != DBNull.Value) entidad.rotacioninventarios = Convert.ToDouble(resultado["ROTACIONINVENTARIOS"]);
                            if (resultado["EF"] != DBNull.Value) entidad.ef = Convert.ToDouble(resultado["EF"]);
                            if (resultado["ENDEUDAMIENTO"] != DBNull.Value) entidad.endeudamiento = Convert.ToDouble(resultado["ENDEUDAMIENTO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                        }
                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ViabilidadFinancieraData", "ConsultarViabilidadFinanciera", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Viabilidad dados unos filtros
        /// </summary>
        /// <param name="pViabilidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ViabilidadFinanciera obtenidos</returns>
        public List<ViabilidadFinanciera> ListarViabilidadFinanciera(ViabilidadFinanciera pViabilidadFinanciera, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ViabilidadFinanciera> lstViabilidadFinanciera = new List<ViabilidadFinanciera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  VIABILIDAD " ;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ViabilidadFinanciera entidad = new ViabilidadFinanciera();

                            if (resultado["COD_VIABILIDAD"] != DBNull.Value) entidad.cod_viabilidad = Convert.ToInt64(resultado["COD_VIABILIDAD"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["DATACREDITO"] != DBNull.Value) entidad.datacredito = Convert.ToDouble(resultado["DATACREDITO"]);
                            if (resultado["DISPONIBLE"] != DBNull.Value) entidad.disponible = Convert.ToDouble(resultado["DISPONIBLE"]);
                            if (resultado["PRUEBA"] != DBNull.Value) entidad.prueba = Convert.ToDouble(resultado["PRUEBA"]);
                            if (resultado["GASTOS"] != DBNull.Value) entidad.gastos = Convert.ToDouble(resultado["GASTOS"]);
                            if (resultado["ROTACIONCUENTAS"] != DBNull.Value) entidad.rotacioncuentas = Convert.ToDouble(resultado["ROTACIONCUENTAS"]);
                            if (resultado["ROTACIONCAPITAL"] != DBNull.Value) entidad.rotacioncapital = Convert.ToDouble(resultado["ROTACIONCAPITAL"]);
                            if (resultado["ROTACIONCUENTASPAGAR"] != DBNull.Value) entidad.rotacioncuentaspagar = Convert.ToDouble(resultado["ROTACIONCUENTASPAGAR"]);
                            if (resultado["PUNTOEQUILIBRIO"] != DBNull.Value) entidad.puntoequilibrio = Convert.ToDouble(resultado["PUNTOEQUILIBRIO"]);
                            if (resultado["ROTACIONINVENTARIOS"] != DBNull.Value) entidad.rotacioninventarios = Convert.ToDouble(resultado["ROTACIONINVENTARIOS"]);
                            if (resultado["EF"] != DBNull.Value) entidad.ef = Convert.ToDouble(resultado["EF"]);
                            if (resultado["ENDEUDAMIENTO"] != DBNull.Value) entidad.endeudamiento = Convert.ToDouble(resultado["ENDEUDAMIENTO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);

                            lstViabilidadFinanciera.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstViabilidadFinanciera;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ViabilidadFinancieraData", "ListarViabilidadFinanciera", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Viabilidad dados unos filtros
        /// </summary>
        /// <param name="pViabilidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ViabilidadFinanciera obtenidos</returns>
        public List<ViabilidadFinanciera> ListarViabilidadFinancieraRepo(ViabilidadFinanciera pViabilidadFinanciera, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ViabilidadFinanciera> lstViabilidadFinanciera = new List<ViabilidadFinanciera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  VIABILIDAD where cod_persona = " + pViabilidadFinanciera.cod_persona;
                             

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ViabilidadFinanciera entidad = new ViabilidadFinanciera();

                            if (resultado["COD_VIABILIDAD"] != DBNull.Value) entidad.cod_viabilidad = Convert.ToInt64(resultado["COD_VIABILIDAD"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["DATACREDITO"] != DBNull.Value) entidad.datacredito = Convert.ToDouble(resultado["DATACREDITO"]);
                            if (resultado["DISPONIBLE"] != DBNull.Value) entidad.disponible = Convert.ToDouble(resultado["DISPONIBLE"]);
                            if (resultado["PRUEBA"] != DBNull.Value) entidad.prueba = Convert.ToDouble(resultado["PRUEBA"]);
                            if (resultado["GASTOS"] != DBNull.Value) entidad.gastos = Convert.ToDouble(resultado["GASTOS"]);
                            if (resultado["ROTACIONCUENTAS"] != DBNull.Value) entidad.rotacioncuentas = Convert.ToDouble(resultado["ROTACIONCUENTAS"]);
                            if (resultado["ROTACIONCAPITAL"] != DBNull.Value) entidad.rotacioncapital = Convert.ToDouble(resultado["ROTACIONCAPITAL"]);
                            if (resultado["ROTACIONCUENTASPAGAR"] != DBNull.Value) entidad.rotacioncuentaspagar = Convert.ToDouble(resultado["ROTACIONCUENTASPAGAR"]);
                            if (resultado["PUNTOEQUILIBRIO"] != DBNull.Value) entidad.puntoequilibrio = Convert.ToDouble(resultado["PUNTOEQUILIBRIO"]);
                            if (resultado["ROTACIONINVENTARIOS"] != DBNull.Value) entidad.rotacioninventarios = Convert.ToDouble(resultado["ROTACIONINVENTARIOS"]);
                            if (resultado["EF"] != DBNull.Value) entidad.ef = Convert.ToDouble(resultado["EF"]);
                            if (resultado["ENDEUDAMIENTO"] != DBNull.Value) entidad.endeudamiento = Convert.ToDouble(resultado["ENDEUDAMIENTO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);

                            lstViabilidadFinanciera.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstViabilidadFinanciera;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ViabilidadFinancieraData", "ListarViabilidadFinancieraRepo", ex);
                        return null;
                    }
                }
            }
        }

       
    }
}