using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Obligaciones.Entities;
using System.Web;
using System.Web.UI.WebControls;

namespace Xpinn.Obligaciones.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla OBCREDITO
    /// </summary>
    public class ObligacionCreditoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla OBCREDITO
        /// </summary>
        public ObligacionCreditoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla OBCREDITO de la base de datos
        /// </summary>
        /// <param name="pObligacionCredito">Entidad ObligacionCredito</param>
        /// <returns>Entidad ObligacionCredito creada</returns>
        public ObligacionCredito CrearObligacionCredito(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODOBLIGACION = cmdTransaccionFactory.CreateParameter();
                        pCODOBLIGACION.ParameterName = "p_codobligacion";
                        pCODOBLIGACION.Value = pObligacionCredito.codobligacion;
                        pCODOBLIGACION.Direction = ParameterDirection.InputOutput;

                        DbParameter pNUMEROPAGARE = cmdTransaccionFactory.CreateParameter();
                        pNUMEROPAGARE.ParameterName = "p_numeropagare";
                        pNUMEROPAGARE.Value = pObligacionCredito.numeropagare;

                        DbParameter pCODENTIDAD = cmdTransaccionFactory.CreateParameter();
                        pCODENTIDAD.ParameterName = "p_codentidad";
                        pCODENTIDAD.Value = pObligacionCredito.codentidad;

                        DbParameter pFECHA_APROBACION = cmdTransaccionFactory.CreateParameter();
                        pFECHA_APROBACION.ParameterName = "p_fecha_aprobacion";
                        pFECHA_APROBACION.Value = pObligacionCredito.fecha_aprobacion;

                        DbParameter pCODPERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        pCODPERIODICIDAD.ParameterName = "p_codperiodicidad";
                        pCODPERIODICIDAD.Value = pObligacionCredito.codperiodicidad;

                        DbParameter pMONTOAPROBADO = cmdTransaccionFactory.CreateParameter();
                        pMONTOAPROBADO.ParameterName = "p_montoaprobado";
                        pMONTOAPROBADO.Value = pObligacionCredito.montoaprobado;

                        DbParameter pSALDOCAPITAL = cmdTransaccionFactory.CreateParameter();
                        pSALDOCAPITAL.ParameterName = "p_saldocapital";
                        pSALDOCAPITAL.Value = pObligacionCredito.saldocapital;

                        DbParameter pCUOTA = cmdTransaccionFactory.CreateParameter();
                        pCUOTA.ParameterName = "p_cuota";
                        pCUOTA.Value = pObligacionCredito.cuota;

                        DbParameter pFECHAPROXIMOPAGO = cmdTransaccionFactory.CreateParameter();
                        pFECHAPROXIMOPAGO.ParameterName = "p_fechaproximopago";
                        pFECHAPROXIMOPAGO.Value = pObligacionCredito.fechaproximopago;

                        DbParameter pESTADOOBLIGACION = cmdTransaccionFactory.CreateParameter();
                        pESTADOOBLIGACION.ParameterName = "p_estadoobligacion";
                        pESTADOOBLIGACION.Value = pObligacionCredito.estadoobligacion;


                        cmdTransaccionFactory.Parameters.Add(pCODOBLIGACION);
                        cmdTransaccionFactory.Parameters.Add(pNUMEROPAGARE);
                        cmdTransaccionFactory.Parameters.Add(pCODENTIDAD);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_APROBACION);
                        cmdTransaccionFactory.Parameters.Add(pCODPERIODICIDAD);
                        cmdTransaccionFactory.Parameters.Add(pMONTOAPROBADO);
                        cmdTransaccionFactory.Parameters.Add(pSALDOCAPITAL);
                        cmdTransaccionFactory.Parameters.Add(pCUOTA);
                        cmdTransaccionFactory.Parameters.Add(pFECHAPROXIMOPAGO);
                        cmdTransaccionFactory.Parameters.Add(pESTADOOBLIGACION);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_OBCREDITO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pObligacionCredito, "OBCREDITO", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pObligacionCredito.codobligacion = Convert.ToInt64(pCODOBLIGACION.Value);
                        return pObligacionCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObligacionCreditoData", "CrearObligacionCredito", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla OBCREDITO de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ObligacionCredito modificada</returns>
        public ObligacionCredito ModificarObligacionCredito(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODOBLIGACION = cmdTransaccionFactory.CreateParameter();
                        pCODOBLIGACION.ParameterName = "p_CODOBLIGACION";
                        pCODOBLIGACION.Value = pObligacionCredito.codobligacion;

                        DbParameter pNUMEROPAGARE = cmdTransaccionFactory.CreateParameter();
                        pNUMEROPAGARE.ParameterName = "p_NUMEROPAGARE";
                        pNUMEROPAGARE.Value = pObligacionCredito.numeropagare;

                        DbParameter pCODENTIDAD = cmdTransaccionFactory.CreateParameter();
                        pCODENTIDAD.ParameterName = "p_CODENTIDAD";
                        pCODENTIDAD.Value = pObligacionCredito.codentidad;

                        DbParameter pFECHA_APROBACION = cmdTransaccionFactory.CreateParameter();
                        pFECHA_APROBACION.ParameterName = "p_FECHA_APROBACION";
                        pFECHA_APROBACION.Value = pObligacionCredito.fecha_aprobacion;

                        DbParameter pCODPERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        pCODPERIODICIDAD.ParameterName = "p_CODPERIODICIDAD";
                        pCODPERIODICIDAD.Value = pObligacionCredito.codperiodicidad;

                        DbParameter pMONTOAPROBADO = cmdTransaccionFactory.CreateParameter();
                        pMONTOAPROBADO.ParameterName = "p_MONTOAPROBADO";
                        pMONTOAPROBADO.Value = pObligacionCredito.montoaprobado;

                        DbParameter pSALDOCAPITAL = cmdTransaccionFactory.CreateParameter();
                        pSALDOCAPITAL.ParameterName = "p_SALDOCAPITAL";
                        pSALDOCAPITAL.Value = pObligacionCredito.saldocapital;

                        DbParameter pCUOTA = cmdTransaccionFactory.CreateParameter();
                        pCUOTA.ParameterName = "p_CUOTA";
                        pCUOTA.Value = pObligacionCredito.cuota;

                        DbParameter pFECHAPROXIMOPAGO = cmdTransaccionFactory.CreateParameter();
                        pFECHAPROXIMOPAGO.ParameterName = "p_FECHAPROXIMOPAGO";
                        pFECHAPROXIMOPAGO.Value = pObligacionCredito.fechaproximopago;

                        DbParameter pESTADOOBLIGACION = cmdTransaccionFactory.CreateParameter();
                        pESTADOOBLIGACION.ParameterName = "p_ESTADOOBLIGACION";
                        pESTADOOBLIGACION.Value = pObligacionCredito.estadoobligacion;

                        cmdTransaccionFactory.Parameters.Add(pCODOBLIGACION);
                        cmdTransaccionFactory.Parameters.Add(pNUMEROPAGARE);
                        cmdTransaccionFactory.Parameters.Add(pCODENTIDAD);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_APROBACION);
                        cmdTransaccionFactory.Parameters.Add(pCODPERIODICIDAD);
                        cmdTransaccionFactory.Parameters.Add(pMONTOAPROBADO);
                        cmdTransaccionFactory.Parameters.Add(pSALDOCAPITAL);
                        cmdTransaccionFactory.Parameters.Add(pCUOTA);
                        cmdTransaccionFactory.Parameters.Add(pFECHAPROXIMOPAGO);
                        cmdTransaccionFactory.Parameters.Add(pESTADOOBLIGACION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_OBCREDITO_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pObligacionCredito, "OBCREDITO", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pObligacionCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObligacionCreditoData", "ModificarObligacionCredito", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla OBCREDITO de la base de datos
        /// </summary>
        /// <param name="pId">identificador de OBCREDITO</param>
        public void EliminarObligacionCredito(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ObligacionCredito pObligacionCredito = new ObligacionCredito();

                        if (pUsuario.programaGeneraLog)
                            pObligacionCredito = ConsultarObligacionCredito(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCODOBLIGACION = cmdTransaccionFactory.CreateParameter();
                        pCODOBLIGACION.ParameterName = "p_codobligacion";
                        pCODOBLIGACION.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCODOBLIGACION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_OBCREDITO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pObligacionCredito, "OBCREDITO", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObligacionCreditoData", "EliminarObligacionCredito", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla OBCREDITO de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla OBCREDITO</param>
        /// <returns>Entidad ObligacionCredito consultado</returns>
        public ObligacionCredito ConsultarObligacionCredito(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            ObligacionCredito entidad = new ObligacionCredito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  OBCREDITO WHERE codobligacion = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODOBLIGACION"] != DBNull.Value) entidad.codobligacion = Convert.ToInt64(resultado["CODOBLIGACION"]);
                            if (resultado["NUMEROPAGARE"] != DBNull.Value) entidad.numeropagare = Convert.ToInt64(resultado["NUMEROPAGARE"]);
                            if (resultado["CODENTIDAD"] != DBNull.Value) entidad.codentidad = Convert.ToInt64(resultado["CODENTIDAD"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToString(resultado["FECHA_APROBACION"]);
                            if (resultado["CODPERIODICIDAD"] != DBNull.Value) entidad.codperiodicidad = Convert.ToInt64(resultado["CODPERIODICIDAD"]);
                            if (resultado["MONTOAPROBADO"] != DBNull.Value) entidad.montoaprobado = Convert.ToInt64(resultado["MONTOAPROBADO"]);
                            if (resultado["SALDOCAPITAL"] != DBNull.Value) entidad.saldocapital = Convert.ToInt64(resultado["SALDOCAPITAL"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FECHAPROXIMOPAGO"] != DBNull.Value) entidad.fechaproximopago = Convert.ToString(resultado["FECHAPROXIMOPAGO"]);
                            if (resultado["ESTADOOBLIGACION"] != DBNull.Value) entidad.estadoobligacion = Convert.ToString(resultado["ESTADOOBLIGACION"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObligacionCreditoData", "ConsultarObligacionCredito", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla OBCREDITO dados unos filtros
        /// </summary>
        /// <param name="pOBCREDITO">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ObligacionCredito obtenidos</returns>
        public List<ObligacionCredito> ListarObligacionCredito(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ObligacionCredito> lstObligacionCredito = new List<ObligacionCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                       
                        string param1 = pObligacionCredito.codobligacion == 0 ? " " : " and codobligacion=" + pObligacionCredito.codobligacion;
                        
                        string param2 ="";

                        if( pObligacionCredito.fechaproximopago != "01/01/1900")
                            param2 = " and to_char(FECHAPROXIMOPAGO,'dd/MM/yyyy') = '" + pObligacionCredito.fechaproximopago + "' ";

                        string param3 = pObligacionCredito.codentidad == 0 ? " " : " and codentidad=" + pObligacionCredito.codentidad;

                        string param4 = pObligacionCredito.estadoobligacion == "T" ? " " : " and estadoobligacion='" + pObligacionCredito.estadoobligacion + "' ";

                        string sql = " SELECT CODOBLIGACION,NUMEROPAGARE,(select b.nombrebanco from bancos b where b.cod_banco=o.codentidad) NOMENTIDAD, " +
                                     " (select l.nombrelinea from OBLINEAOBLIGACION l where l.codlineaobligacion = o.codlineaobligacion) NOMLINEAOB," +
                                     " to_char(FECHA_APROBACION,'dd/MM/yyyy') FECHAAPROBACION, (select b.DESCRIPCION from periodicidad b where b.COD_PERIODICIDAD=o.CODPERIODICIDAD) NOMPERIODICIDAD, " +
                                     " MONTOAPROBADO,SALDOCAPITAL,VALOR_CUOTA,to_char(FECHAPROXIMOPAGO,'dd/MM/yyyy') FECHA_PROXIMOPAGO,decode(ESTADOOBLIGACION,'S', 'Solicitado',decode(ESTADOOBLIGACION,'D', 'Desembolsado',decode(ESTADOOBLIGACION,'C', 'Cancelada',decode(ESTADOOBLIGACION,'P','Pendiente por Solicitud','Anulada')))) ESTADO " +
                                     " FROM OBCREDITO o " +
                                     " where 1=1 " + param1 + param2 + param3 + param4 +
                                     " order by " + pObligacionCredito.codfiltroorderuno + " asc ," + pObligacionCredito.codfiltroorderdos + " asc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ObligacionCredito entidad = new ObligacionCredito();

                            if (resultado["CODOBLIGACION"] != DBNull.Value) entidad.codobligacion = Convert.ToInt64(resultado["CODOBLIGACION"]);
                            if (resultado["NUMEROPAGARE"] != DBNull.Value) entidad.numeropagare = Convert.ToInt64(resultado["NUMEROPAGARE"]);
                            if (resultado["NOMENTIDAD"] != DBNull.Value) entidad.nomentidad = Convert.ToString(resultado["NOMENTIDAD"]);
                            if (resultado["NOMLINEAOB"] != DBNull.Value) entidad.nomlineaobligacion = Convert.ToString(resultado["NOMLINEAOB"]);
                            
                            if (resultado["FECHAAPROBACION"].ToString() != "01/01/0001") entidad.fecha_aprobacion = Convert.ToString(resultado["FECHAAPROBACION"]);
                            else
                                entidad.fecha_aprobacion = "";

                            if (resultado["NOMPERIODICIDAD"] != DBNull.Value) entidad.nomperiodicidad = Convert.ToString(resultado["NOMPERIODICIDAD"]);
                            if (resultado["MONTOAPROBADO"] != DBNull.Value) entidad.montoaprobado = Convert.ToInt64(resultado["MONTOAPROBADO"]);
                            if (resultado["SALDOCAPITAL"] != DBNull.Value) entidad.saldocapital = Convert.ToInt64(resultado["SALDOCAPITAL"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);

                            if (resultado["FECHA_PROXIMOPAGO"].ToString() != "01/01/0001") entidad.fechaproximopago = Convert.ToString(resultado["FECHA_PROXIMOPAGO"]);
                            else
                                entidad.fechaproximopago = "";

                            if (resultado["ESTADO"] != DBNull.Value) entidad.estadoobligacion = Convert.ToString(resultado["ESTADO"]);

                            lstObligacionCredito.Add(entidad);
                        }

                        return lstObligacionCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObligacionCreditoData", "ListarObligacionCredito", ex);
                        return null;
                    }
                }
            }
        }


        /// <returns>Conjunto de ObligacionCredito obtenidos</returns>
        public List<ObligacionCredito> ListarObligaciones(String filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ObligacionCredito> lstObligacionCredito = new List<ObligacionCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                    
                        string sql = " SELECT CODOBLIGACION,NUMEROPAGARE,(select b.nombrebanco from bancos b where b.cod_banco=o.codentidad) NOMENTIDAD, " +
                                     " (select l.nombrelinea from OBLINEAOBLIGACION l where l.codlineaobligacion = o.codlineaobligacion) NOMLINEAOB," +
                                     " to_char(FECHA_APROBACION,'dd/MM/yyyy') FECHAAPROBACION, to_char(FECHAULTIMOPAGO,'dd/MM/yyyy') FECHAULTIMOPAGO,(select b.DESCRIPCION from periodicidad b where b.COD_PERIODICIDAD=o.CODPERIODICIDAD) NOMPERIODICIDAD, " +
                                     " MONTOAPROBADO,SALDOCAPITAL,VALOR_CUOTA,to_char(FECHAPROXIMOPAGO,'dd/MM/yyyy') FECHA_PROXIMOPAGO,decode(ESTADOOBLIGACION,'S', 'Solicitado',decode(ESTADOOBLIGACION,'D', 'Desembolsado',decode(ESTADOOBLIGACION,'C', 'Cancelada',decode(ESTADOOBLIGACION,'P','Pendiente por Solicitud','Anulada')))) ESTADO " +
                                     " FROM OBCREDITO o   LEFT JOIN bancos ON bancos.COD_BANCO=o.CODENTIDAD LEFT JOIN PERSONA P ON P.COD_PERSONA = bancos.COD_PERSONA" +
                                     " where 1=1 " + filtro +
                                     " order by   CODOBLIGACION  asc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ObligacionCredito entidad = new ObligacionCredito();

                            if (resultado["CODOBLIGACION"] != DBNull.Value) entidad.codobligacion = Convert.ToInt64(resultado["CODOBLIGACION"]);
                            if (resultado["NUMEROPAGARE"] != DBNull.Value) entidad.numeropagare = Convert.ToInt64(resultado["NUMEROPAGARE"]);
                            if (resultado["NOMENTIDAD"] != DBNull.Value) entidad.nomentidad = Convert.ToString(resultado["NOMENTIDAD"]);
                            if (resultado["NOMLINEAOB"] != DBNull.Value) entidad.nomlineaobligacion = Convert.ToString(resultado["NOMLINEAOB"]);

                            if (resultado["FECHAAPROBACION"].ToString() != "01/01/0001") entidad.fecha_aprobacion = Convert.ToString(resultado["FECHAAPROBACION"]);                           else
                                entidad.fecha_aprobacion = "";
                            if (resultado["FECHAULTIMOPAGO"].ToString() != "01/01/0001") entidad.fecha_ultpago = Convert.ToDateTime(resultado["FECHAULTIMOPAGO"]);
                           

                            if (resultado["NOMPERIODICIDAD"] != DBNull.Value) entidad.nomperiodicidad = Convert.ToString(resultado["NOMPERIODICIDAD"]);
                            if (resultado["MONTOAPROBADO"] != DBNull.Value) entidad.montoaprobado = Convert.ToInt64(resultado["MONTOAPROBADO"]);
                            if (resultado["SALDOCAPITAL"] != DBNull.Value) entidad.saldocapital = Convert.ToInt64(resultado["SALDOCAPITAL"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);

                            if (resultado["FECHA_PROXIMOPAGO"].ToString() != "01/01/0001") entidad.fechaproximopago = Convert.ToString(resultado["FECHA_PROXIMOPAGO"]);
                            else
                                entidad.fechaproximopago = "";

                            if (resultado["ESTADO"] != DBNull.Value) entidad.estadoobligacion = Convert.ToString(resultado["ESTADO"]);

                            lstObligacionCredito.Add(entidad);
                        }

                        return lstObligacionCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObligacionCreditoData", "ListarObligacionCredito", ex);
                        return null;
                    }
                }
            }
        }

        public List<ObligacionCredito> ListarProvicionCredito(ObligacionCredito pObligacionCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ObligacionCredito> lstObligacionCredito = new List<ObligacionCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                                              
                        string param3 = pObligacionCredito.codentidad == 0 ? " " : " and o.codentidad=" + pObligacionCredito.codentidad;

                        string sql = "SELECT o.plazo, o.codobligacion, o.numeropagare, to_char(o.fecha_aprobacion,'dd/MM/yyyy') fecha_aprobacion, (select b.nombrebanco from bancos b where b.cod_banco = o.codentidad) NOMENTIDAD," +
                        "o.montoaprobado, o.saldocapital, c.tasa tasa, c.spread  puntos_adicionales, b.descripcion nomperiodicidad," +
                        "(select t.nombre from tipotasa t where c.cod_tipo_tasa = t.cod_tipo_tasa) nombre_tasa" +
                        " FROM obcredito o LEFT JOIN obcomponentecredito c ON c.codobligacion = o.codobligacion LEFT JOIN periodicidad b ON b.cod_periodicidad = o.codperiodicidad " +
                        " WHERE 0 = 0 "  + param3 +
                        " ORDER BY " + pObligacionCredito.codfiltroorderuno + " asc ," + pObligacionCredito.codfiltroorderdos + " asc ," + pObligacionCredito.codfiltroordertres + " asc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ObligacionCredito entidad = new ObligacionCredito();

                            if (resultado["CODOBLIGACION"] != DBNull.Value) entidad.codobligacion = Convert.ToInt64(resultado["CODOBLIGACION"]);
                            if (resultado["NUMEROPAGARE"] != DBNull.Value) entidad.numeropagare = Convert.ToInt64(resultado["NUMEROPAGARE"]);
                            if (resultado["FECHA_APROBACION"].ToString() != "01/01/0001") 
                                entidad.fecha_aprobacion = Convert.ToString(resultado["FECHA_APROBACION"]);
                            else
                                entidad.fecha_aprobacion = "";
                            if (resultado["NOMENTIDAD"] != DBNull.Value) entidad.entidad = Convert.ToString(resultado["NOMENTIDAD"]);
                            if (resultado["MONTOAPROBADO"] != DBNull.Value) entidad.montoaprobado = Convert.ToInt64(resultado["MONTOAPROBADO"]);
                            if (resultado["SALDOCAPITAL"] != DBNull.Value) entidad.saldocapital = Convert.ToInt64(resultado["SALDOCAPITAL"]);
                            if (resultado["TASA"]!= DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["PUNTOS_ADICIONALES"] != DBNull.Value) entidad.puntos_adicionales = Convert.ToDecimal(resultado["PUNTOS_ADICIONALES"]);
                            if (resultado["NOMPERIODICIDAD"] != DBNull.Value) entidad.nomperiodicidad = Convert.ToString(resultado["NOMPERIODICIDAD"]);
                            if (resultado["NOMBRE_TASA"] != DBNull.Value) entidad.nombre_tasa = Convert.ToString(resultado["NOMBRE_TASA"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);                                              

                            lstObligacionCredito.Add(entidad);
                        }

                        return lstObligacionCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObligacionCreditoData", "ListarObligacionCredito", ex);
                        return null;
                    }
                }
            }
        }

        public ObligacionCredito ModificarProvision(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter PFECHA_CORTE = cmdTransaccionFactory.CreateParameter();
                        PFECHA_CORTE.ParameterName = "PFECHA_CORTE";
                        PFECHA_CORTE.DbType = DbType.Date;
                        PFECHA_CORTE.Value = pObligacionCredito.fecha_corte;

                        DbParameter PCOD_OBLIGACION = cmdTransaccionFactory.CreateParameter();
                        PCOD_OBLIGACION.ParameterName = "PCOD_OBLIGACION";
                        PCOD_OBLIGACION.Value = pObligacionCredito.codobligacion;

                        DbParameter PNUM_OBLIGACION = cmdTransaccionFactory.CreateParameter();
                        PNUM_OBLIGACION.ParameterName = "PNUM_OBLIGACION";
                        PNUM_OBLIGACION.Value = pObligacionCredito.codobligacion;

                        DbParameter PINTERESES = cmdTransaccionFactory.CreateParameter();
                        PINTERESES.ParameterName = "PINTERESES";
                        PINTERESES.Value = pObligacionCredito.intereses;

                        DbParameter PDIASCAUSADOS = cmdTransaccionFactory.CreateParameter();
                        PDIASCAUSADOS.ParameterName = "PDIASCAUSADOS";
                        PDIASCAUSADOS.Value = pObligacionCredito.dias_causados;

                        cmdTransaccionFactory.Parameters.Add(PFECHA_CORTE);
                        cmdTransaccionFactory.Parameters.Add(PCOD_OBLIGACION);
                        cmdTransaccionFactory.Parameters.Add(PINTERESES);
                        cmdTransaccionFactory.Parameters.Add(PDIASCAUSADOS);
                           
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = " USP_XPINN_OB_PROVISION_CREAR ";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        return pObligacionCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObligacionCreditoData", "ModificarProvision", ex);
                        return null;
                    }
                }
            }
        }


        public void ControlProvision(DateTime pfechacorte, string pestado, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter PFECHA_CORTE = cmdTransaccionFactory.CreateParameter();
                        PFECHA_CORTE.ParameterName = "PFECHA_CORTE";
                        PFECHA_CORTE.DbType = DbType.Date;
                        PFECHA_CORTE.Value = pfechacorte;

                        DbParameter PESTADO = cmdTransaccionFactory.CreateParameter();
                        PESTADO.ParameterName = "PESTADO";
                        PESTADO.Value = pestado;

                        DbParameter PCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        PCODUSUARIO.ParameterName = "PCODUSUARIO";
                        PCODUSUARIO.Value = pUsuario.codusuario;

                        cmdTransaccionFactory.Parameters.Add(PFECHA_CORTE);
                        cmdTransaccionFactory.Parameters.Add(PESTADO);
                        cmdTransaccionFactory.Parameters.Add(PCODUSUARIO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_PROVISION_CONTROL";
                        cmdTransaccionFactory.ExecuteNonQuery(); 

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObligacionCreditoData", "ControlProvision", ex);
                        return;
                    }
                }
            }
        }

        public DateTime FechaUltimoCierre(string pTipo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = "Select Max(fecha) as fecha From cierea Where tipo = '" + pTipo + "' And estado = 'D' ";
                                                                      
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            ObligacionCredito entidad = new ObligacionCredito();
                            if (resultado["FECHA"].ToString() != "01/01/0001" && resultado["FECHA"].ToString() != "") 
                                entidad.fecha_corte = Convert.ToDateTime(resultado["FECHA"]);                            
                            else
                                entidad.fecha_corte = new DateTime(DateTime.Now.Year, DateTime.Now.Month-1, 1).AddDays(-1);
                            return entidad.fecha_corte;
                        }

                        return DateTime.MinValue;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObligacionCreditoData", "FechaUltimoCierre", ex);
                        return DateTime.MinValue;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla OBCREDITO dados unos filtros
        /// </summary>
        /// <param name="pOBCREDITO">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ObligacionCredito obtenidos</returns>
        public List<ObligacionCredito> ListarObligacionPendPagar(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ObligacionCredito> lstObligacionCredito = new List<ObligacionCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string param1 = pObligacionCredito.codobligacion == 0 ? " " : " and codobligacion=" + pObligacionCredito.codobligacion;

                        string param2 = "";

                        if (pObligacionCredito.fechaproximopago != "01/01/1900")
                            param2 = " and to_char(FECHAPROXIMOPAGO,'dd/MM/yyyy') = '" + pObligacionCredito.fechaproximopago + "' ";

                        string param3 = pObligacionCredito.codentidad == 0 ? " " : " and codentidad=" + pObligacionCredito.codentidad;

                        string sql = " SELECT CODOBLIGACION,NUMEROPAGARE,(select b.nombrebanco from bancos b where b.cod_banco=o.codentidad) NOMENTIDAD, " +
                                     " to_char(FECHA_APROBACION,'dd/MM/yyyy') FECHAAPROBACION, (select b.DESCRIPCION from periodicidad b where b.COD_PERIODICIDAD=o.CODPERIODICIDAD) NOMPERIODICIDAD, " +
                                     " MONTOAPROBADO,SALDOCAPITAL,VALOR_CUOTA,to_char(FECHAPROXIMOPAGO,'dd/MM/yyyy') FECHA_PROXIMOPAGO ,decode(ESTADOOBLIGACION,'S', 'Solicitado',decode(ESTADOOBLIGACION,'D', 'Desembolsado',decode(ESTADOOBLIGACION,'C', 'Cancelada',decode(ESTADOOBLIGACION,'P','Pendiente por Solicitud','Anulada')))) ESTADO " +
                                     " FROM OBCREDITO o " +
                                     " where 1=1 And Estadoobligacion='D'" + param1 + param2 + param3 +
                                     " order by CODOBLIGACION asc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ObligacionCredito entidad = new ObligacionCredito();

                            if (resultado["CODOBLIGACION"] != DBNull.Value) entidad.codobligacion = Convert.ToInt64(resultado["CODOBLIGACION"]);
                            if (resultado["NUMEROPAGARE"] != DBNull.Value) entidad.numeropagare = Convert.ToInt64(resultado["NUMEROPAGARE"]);
                            if (resultado["NOMENTIDAD"] != DBNull.Value) entidad.nomentidad = Convert.ToString(resultado["NOMENTIDAD"]);

                            if (resultado["FECHA_PROXIMOPAGO"].ToString() != "01/01/0001") entidad.fechaproximopago = Convert.ToString(resultado["FECHA_PROXIMOPAGO"]);
                            else
                                entidad.fechaproximopago = "";

                            if (resultado["MONTOAPROBADO"] != DBNull.Value) entidad.montoaprobado = Convert.ToInt64(resultado["MONTOAPROBADO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["SALDOCAPITAL"] != DBNull.Value) entidad.saldocapital = Convert.ToInt64(resultado["SALDOCAPITAL"]);

                          
                            lstObligacionCredito.Add(entidad);
                        }

                        return lstObligacionCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObligacionCreditoData", "ListarObligacionPendPagar", ex);
                        return null;
                    }
                }
            }
        }

         
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla OBCREDITO - Datos Solicitud dados unos filtros
        /// </summary>
        /// <param name="pOBCREDITO">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ObligacionCredito obtenidos</returns>
        public List<ObligacionCredito> ListarDatosSolicitud(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ObligacionCredito> lstObligacionCredito = new List<ObligacionCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string param1 = pObligacionCredito.codobligacion == 0 ? " " : " and codobligacion=" + pObligacionCredito.codobligacion;

                        string param2 = pObligacionCredito.codentidad == 0 ? " " : " and codentidad=" + pObligacionCredito.codentidad;

                        string param3 = pObligacionCredito.montosolicitud == 0 ? " " : " and montosolicitado=" + pObligacionCredito.montosolicitud;

                        string sql = " SELECT CODOBLIGACION,(select b.nombrebanco from bancos b where b.cod_banco=o.codentidad) NOMENTIDAD, " +
                                     " (select l.nombrelinea from OBLINEAOBLIGACION l where l.codlineaobligacion = o.codlineaobligacion) NOMLINEAOB," +
                                     " FECHA_SOLICITUD, " +
                                     " MONTOSOLICITADO,decode(ESTADOOBLIGACION,'S', 'Solicitado',decode(ESTADOOBLIGACION,'D', 'Desembolsado',decode(ESTADOOBLIGACION,'C', 'Cancelada',decode(ESTADOOBLIGACION,'P','Pendiente Por Solicitud','Anulada')))) ESTADO " +
                                     " FROM OBCREDITO o " +
                                     " where 1=1 " + param1 + param2 + param3 +
                                     " order by codobligacion asc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ObligacionCredito entidad = new ObligacionCredito();

                            if (resultado["CODOBLIGACION"] != DBNull.Value) entidad.codobligacion = Convert.ToInt64(resultado["CODOBLIGACION"]);
                            if (resultado["NOMENTIDAD"] != DBNull.Value) entidad.nomentidad = Convert.ToString(resultado["NOMENTIDAD"]);
                            if (resultado["NOMLINEAOB"] != DBNull.Value) entidad.nomlineaobligacion = Convert.ToString(resultado["NOMLINEAOB"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["MONTOSOLICITADO"] != DBNull.Value) entidad.montosolicitud = Convert.ToInt64(resultado["MONTOSOLICITADO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estadoobligacion = Convert.ToString(resultado["ESTADO"]);

                            lstObligacionCredito.Add(entidad);
                        }

                        return lstObligacionCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObligacionCreditoData", "ListarDatosSolicitud", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Relacion - Datos Solicitud dados unos filtros
        /// </summary>
        /// <param name="pOBCREDITO">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ObligacionCredito obtenidos</returns>
        public List<ObligacionCredito> ListarPlanObligacion(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ObligacionCredito> lstObligacionCredito = new List<ObligacionCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = "SELECT * FROM VOBLIGACIONESPLANPAGOS WHERE codobligacion = " + pObligacionCredito.codobligacion.ToString() + " ORDER BY FECHACUOTA ASC, NUMCUOTA ASC ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ObligacionCredito pEntidad = new ObligacionCredito();

                            if (resultado["numcuota"] != DBNull.Value) pEntidad.nrocuota = Convert.ToInt64(resultado["numcuota"]);
                            if (resultado["fechacuota"] != DBNull.Value) pEntidad.fecha = Convert.ToDateTime(resultado["fechacuota"]);
                            if (resultado["capital"] != DBNull.Value) pEntidad.amort_cap = Convert.ToInt64(resultado["capital"]);
                            if (resultado["int_corriente"] != DBNull.Value) pEntidad.interes_corriente = Convert.ToInt64(resultado["int_corriente"]);
                            if (resultado["int_mora"] != DBNull.Value) pEntidad.interes_mora = Convert.ToInt64(resultado["int_mora"]);
                            if (resultado["seguro"] != DBNull.Value) pEntidad.seguro = Convert.ToInt64(resultado["seguro"]);
                            if (resultado["saldo"] != DBNull.Value)  pEntidad.saldo = Convert.ToInt64(resultado["saldo"]);

                            lstObligacionCredito.Add(pEntidad);
                        }

                        return lstObligacionCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObligacionCreditoData", "ListarMovsObligacion", ex);
                        return null;
                    }
                }
            }
        }

        public List<ObligacionCredito> ListarMovsObligacion(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ObligacionCredito> lstObligacionCredito = new List<ObligacionCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                            string sql = " SELECT VOBLIGACIONESPAGOS.*,o.num_comp,o.tipo_comp FROM VOBLIGACIONESPAGOS left join operacion o on o.cod_ope = VOBLIGACIONESPAGOS.CODOPE WHERE codobligacion = " + pObligacionCredito.codobligacion.ToString() + " ORDER BY FPAGO ASC, NUMCUOTA ASC ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ObligacionCredito pEntidad = new ObligacionCredito();

                            if (resultado["numcuota"] != DBNull.Value) pEntidad.nrocuota = Convert.ToInt64(resultado["numcuota"]);
                            if (resultado["fechacuota"] != DBNull.Value) pEntidad.fecha = Convert.ToDateTime(resultado["fechacuota"]);
                            if (resultado["fpago"] != DBNull.Value) pEntidad.fechapago = Convert.ToDateTime(resultado["fpago"]);
                            if (resultado["codope"] != DBNull.Value) pEntidad.cod_ope = Convert.ToInt64(resultado["codope"]);
                            if (resultado["tipoope"] != DBNull.Value) pEntidad.tipo_ope = Convert.ToString(resultado["tipoope"]);
                            if (resultado["tipomov"] != DBNull.Value) pEntidad.tipo_mov = Convert.ToString(resultado["tipomov"]);
                            if (resultado["capital"] != DBNull.Value) pEntidad.amort_cap = Convert.ToInt64(resultado["capital"]);
                            if (resultado["int_corriente"] != DBNull.Value) pEntidad.interes_corriente = Convert.ToInt64(resultado["int_corriente"]);
                            if (resultado["int_mora"] != DBNull.Value) pEntidad.interes_mora = Convert.ToInt64(resultado["int_mora"]);
                            if (resultado["seguro"] != DBNull.Value) pEntidad.seguro = Convert.ToInt64(resultado["seguro"]);
                            if (resultado["saldo"] != DBNull.Value) pEntidad.saldo = Convert.ToInt64(resultado["saldo"]);
                            if (resultado["num_comp"] != DBNull.Value) pEntidad.num_comp = Convert.ToInt64(resultado["num_comp"]);
                            if (resultado["tipo_comp"] != DBNull.Value) pEntidad.tipo_comp = Convert.ToInt64(resultado["tipo_comp"]);
                            if (resultado["TIPOTRAN"] != DBNull.Value) pEntidad.tipo_tran = Convert.ToInt64(resultado["TIPOTRAN"]);
                            lstObligacionCredito.Add(pEntidad);
                        }

                        return lstObligacionCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObligacionCreditoData", "ListarMovsObligacion", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla OBCREDITO dados unos filtros
        /// </summary>
        /// <param name="pOBCREDITO">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ObligacionCredito obtenidos</returns>
        public List<ObligacionCredito> ListarObligacionCreditoVencido(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ObligacionCredito> lstObligacionCredito = new List<ObligacionCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        string param1 = pObligacionCredito.codentidad == 0 ? " " : " and codentidad=" + pObligacionCredito.codentidad;

                        string param2 = "";

                        if (pObligacionCredito.fecha_inicio != "01/01/1900")
                            param2 = " and to_char(FECHAPROXIMOPAGO,'dd/MM/yyyy') >= '" + pObligacionCredito.fecha_inicio + "' ";

                        string param3 = "";

                        if (pObligacionCredito.fecha_final != "01/01/1900")
                            param3 = " and to_char(FECHAPROXIMOPAGO,'dd/MM/yyyy') <= '" + pObligacionCredito.fecha_final + "' ";


                        string sql = " SELECT CODOBLIGACION,NUMEROPAGARE,(select b.nombrebanco from bancos b where b.cod_banco=o.codentidad) NOMENTIDAD, " +
                                     " to_char(FECHA_APROBACION,'dd/MM/yyyy') FECHAAPROBACION, (select b.DESCRIPCION from periodicidad b where b.COD_PERIODICIDAD=o.CODPERIODICIDAD) NOMPERIODICIDAD, " +
                                     " MONTOAPROBADO,SALDOCAPITAL,VALOR_CUOTA,to_char(FECHAPROXIMOPAGO,'dd/MM/yyyy') FECHA_PROXIMOPAGO,decode(ESTADOOBLIGACION,'S', 'Solicitado',decode(ESTADOOBLIGACION,'D', 'Desembolsado',decode(ESTADOOBLIGACION,'C', 'Cancelada',decode(ESTADOOBLIGACION,'P','Pendiente por Solicitud','Anulada')))) ESTADO " +
                                     " FROM OBCREDITO o " +
                                     " where 1=1 " + param1 + param2 + param3 + " and ESTADOOBLIGACION='D' "+ 
                                     " order by " + pObligacionCredito.codfiltroorderuno + " asc ," + pObligacionCredito.codfiltroorderuno + " asc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ObligacionCredito entidad = new ObligacionCredito();

                            if (resultado["CODOBLIGACION"] != DBNull.Value) entidad.codobligacion = Convert.ToInt64(resultado["CODOBLIGACION"]);
                            if (resultado["NUMEROPAGARE"] != DBNull.Value) entidad.numeropagare = Convert.ToInt64(resultado["NUMEROPAGARE"]);
                            if (resultado["NOMENTIDAD"] != DBNull.Value) entidad.nomentidad = Convert.ToString(resultado["NOMENTIDAD"]);
                            if (resultado["FECHAAPROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToString(resultado["FECHAAPROBACION"]);
                            if (resultado["NOMPERIODICIDAD"] != DBNull.Value) entidad.nomperiodicidad = Convert.ToString(resultado["NOMPERIODICIDAD"]);
                            if (resultado["MONTOAPROBADO"] != DBNull.Value) entidad.montoaprobado = Convert.ToInt64(resultado["MONTOAPROBADO"]);
                            if (resultado["SALDOCAPITAL"] != DBNull.Value) entidad.saldocapital = Convert.ToInt64(resultado["SALDOCAPITAL"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FECHA_PROXIMOPAGO"] != DBNull.Value) entidad.fechaproximopago = Convert.ToString(resultado["FECHA_PROXIMOPAGO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estadoobligacion = Convert.ToString(resultado["ESTADO"]);

                            lstObligacionCredito.Add(entidad);
                        }

                        return lstObligacionCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObligacionCreditoData", "ListarObligacionCreditoVencido", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Relacion - Datos Solicitud dados unos filtros
        /// </summary>
        /// <param name="pOBCREDITO">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ObligacionCredito obtenidos</returns>
        public List<ObligacionCredito> ListarDistribPagosPendCuotas(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ObligacionCredito> lstObligacionCredito = new List<ObligacionCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " Select * from VObligacionesDetalle " +
                        " where codobligacion=" + pObligacionCredito.codobligacion.ToString() + " and to_char(fechacuota,'dd/MM/yyyy') >='" + pObligacionCredito.fechaproximopago + "'  and to_char(sysdate,'dd/MM/yyyy') >= to_char(fechacuota,'dd/MM/yyyy') " +
                        " ORDER BY FECHACUOTA ASC, NUMCUOTA ASC ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ObligacionCredito pEntidad = new ObligacionCredito();

                            if (resultado["numcuota"] != DBNull.Value) pEntidad.nrocuota = Convert.ToInt64(resultado["numcuota"]);
                            if (resultado["fechacuota"] != DBNull.Value) pEntidad.fecha = Convert.ToDateTime(resultado["fechacuota"]);
                            if (resultado["capital"] != DBNull.Value) pEntidad.amort_cap = Convert.ToInt64(resultado["capital"]);
                            if (resultado["int_corriente"] != DBNull.Value) pEntidad.interes_corriente = Convert.ToInt64(resultado["int_corriente"]);
                            if (resultado["int_mora"] != DBNull.Value) pEntidad.interes_mora = Convert.ToInt64(resultado["int_mora"]);
                            if (resultado["seguro"] != DBNull.Value) pEntidad.seguro = Convert.ToInt64(resultado["seguro"]);
                            if (resultado["saldo"] != DBNull.Value) pEntidad.saldo = Convert.ToInt64(resultado["saldo"]);

                            lstObligacionCredito.Add(pEntidad);
                        }

                        return lstObligacionCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObligacionCreditoData", "ListarDistribPagosPendCuotas", ex);
                        return null;
                    }
                }
            }
        }


        // <summary>
        /// Crea un registro en la tabla CONSIGNACION de la base de datos
        /// </summary>
        /// <param name="pConsignacion">Entidad Consignacion</param>
        /// <returns>Entidad Consignacion creada</returns>
        public ObligacionCredito CrearTransacOpePagoOb(ObligacionCredito pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        // en esta porcion de codigo se inserta primero la operacion realizada con el fin de ir alimentar la operacion cod_ope
                        // en la tabla OPERACIONES
                        cmdTransaccionFactory.Parameters.Clear();
                        DbParameter pcode_opera = cmdTransaccionFactory.CreateParameter();
                        pcode_opera.ParameterName = "pcodigooper";
                        pcode_opera.Value = pEntidad.cod_ope;
                        pcode_opera.Direction = ParameterDirection.Output;

                        DbParameter pcode_tope = cmdTransaccionFactory.CreateParameter();
                        pcode_tope.ParameterName = "pcodigotipoope";
                        pcode_tope.Value = pEntidad.cod_tipo_ope;
                        
                        DbParameter pcode_usuari = cmdTransaccionFactory.CreateParameter();
                        pcode_usuari.ParameterName = "pcodigousuario";
                        pcode_usuari.Value = pUsuario.codusuario;

                        DbParameter pcode_oficina = cmdTransaccionFactory.CreateParameter();
                        pcode_oficina.ParameterName = "pcodigooficina";
                        pcode_oficina.Value = pUsuario.cod_oficina;

                        DbParameter pfechaoper = cmdTransaccionFactory.CreateParameter();
                        pfechaoper.ParameterName = "pfechaoper";
                        pfechaoper.Value = pEntidad.fechapago;

                        DbParameter pfechacalc = cmdTransaccionFactory.CreateParameter();
                        pfechacalc.ParameterName = "pfechacalc";
                        pfechacalc.Value = pEntidad.fechapago;

                        DbParameter P_IP = cmdTransaccionFactory.CreateParameter();
                        P_IP.ParameterName = "P_IP";
                        P_IP.Value = pUsuario.IP;

                        cmdTransaccionFactory.Parameters.Add(pcode_opera);
                        cmdTransaccionFactory.Parameters.Add(pcode_tope);
                        cmdTransaccionFactory.Parameters.Add(pcode_usuari);
                        cmdTransaccionFactory.Parameters.Add(pcode_oficina);
                        cmdTransaccionFactory.Parameters.Add(pfechaoper);
                        cmdTransaccionFactory.Parameters.Add(pfechacalc);
                        cmdTransaccionFactory.Parameters.Add(P_IP);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_OPERACION";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        //// se llama a la funcion PAGAR_CREDITOS para cotabilizar la transaccion OB

                        cmdTransaccionFactory.Parameters.Clear();
                        DbParameter pn_cod_obligacion = cmdTransaccionFactory.CreateParameter();
                        pn_cod_obligacion.ParameterName = "pn_cod_obligacion";
                        pn_cod_obligacion.Value = pEntidad.codobligacion;
                        
                        DbParameter pn_cod_ope = cmdTransaccionFactory.CreateParameter();
                        pn_cod_ope.ParameterName = "pn_cod_ope";
                        pn_cod_ope.Value = pcode_opera.Value;

                        DbParameter pf_fecha_ope = cmdTransaccionFactory.CreateParameter();
                        pf_fecha_ope.ParameterName = "pf_fecha_ope";
                        pf_fecha_ope.Value = pEntidad.fechapago;

                        DbParameter pn_cuota = cmdTransaccionFactory.CreateParameter();
                        pn_cuota.ParameterName = "pn_cuota";
                        pn_cuota.Value = pEntidad.nrocuota;

                        DbParameter pf_fecha_cuota = cmdTransaccionFactory.CreateParameter();
                        pf_fecha_cuota.ParameterName = "pf_fecha_cuota";
                        pf_fecha_cuota.Value = pEntidad.fechacuota;

                        DbParameter pn_valor_capital = cmdTransaccionFactory.CreateParameter();
                        pn_valor_capital.ParameterName = "pn_valor_capital";
                        pn_valor_capital.Value = pEntidad.amort_cap;

                        DbParameter pn_valor_int_corr = cmdTransaccionFactory.CreateParameter();
                        pn_valor_int_corr.ParameterName = "pn_valor_int_corr";
                        pn_valor_int_corr.Value = pEntidad.interes_corriente;

                        DbParameter pn_valor_int_mora = cmdTransaccionFactory.CreateParameter();
                        pn_valor_int_mora.ParameterName = "pn_valor_int_mora";
                        pn_valor_int_mora.Value = pEntidad.interes_mora;                        

                        DbParameter pn_valor_seguro = cmdTransaccionFactory.CreateParameter();
                        pn_valor_seguro.ParameterName = "pn_valor_seguro";
                        pn_valor_seguro.Value = pEntidad.seguro;                       

                        DbParameter pn_cod_usu = cmdTransaccionFactory.CreateParameter();
                        pn_cod_usu.ParameterName = "pn_cod_usu";
                        pn_cod_usu.Value = pUsuario.codusuario;

                        cmdTransaccionFactory.Parameters.Add(pn_cod_obligacion);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_ope);
                        cmdTransaccionFactory.Parameters.Add(pf_fecha_ope);
                        cmdTransaccionFactory.Parameters.Add(pn_cuota);
                        cmdTransaccionFactory.Parameters.Add(pf_fecha_cuota);
                        cmdTransaccionFactory.Parameters.Add(pn_valor_capital);
                        cmdTransaccionFactory.Parameters.Add(pn_valor_int_corr);
                        cmdTransaccionFactory.Parameters.Add(pn_valor_int_mora);
                        cmdTransaccionFactory.Parameters.Add(pn_valor_seguro);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_usu);                        

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_PAGO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pEntidad.cod_ope = long.Parse(pcode_opera.Value.ToString());

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObligacionCreditoData", "CrearTransacOpePagoOb", ex);
                        return null;
                    }
                }

            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TransaccionesCaja dados unos filtros
        /// Transacciones de Caja Pendientes por pasar a contabilidad
        /// </summary>
        /// <param name="pTransaccionesCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ObligacionCredito obtenidos</returns>
        public List<ObligacionCredito> ListarOperaciones(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ObligacionCredito> lstObligacionCredito = new List<ObligacionCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " Select distinct b.codobligacion cod_obligacion, a.cod_ope codope, a.fecha_oper, (select s.descripcion from tipo_ope s where s.tipo_ope = a.tipo_ope) tip_ope, " +
                                     " a.fecha_real,(select distinct z.nombrebanco from bancos z where z.cod_banco = b.codentidad) entidad, "  +
                                     " d.numcuota, " +
                                     " d.fechacuota, " +
                                     " d.capital, " +
                                     " d.int_corriente, " +
                                     " d.int_mora, " +
                                     " d.seguro, " +
                                     " d.total " +  
                                     " From operacion a, obcredito b, VObligacionesDetallePago d " +
                                     " Where d.cod_ope = a.cod_ope And d.codobligacion = b.codobligacion And a.tipo_ope In (42, 43, 44) And to_char(a.fecha_oper,'dd/MM/yyyy') = '" + pObligacionCredito.fecha.ToShortDateString() + "' And a.estado = 1  " +
                                     " Order By a.cod_ope asc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ObligacionCredito entidad = new ObligacionCredito();

                            if (resultado["cod_obligacion"] != DBNull.Value) entidad.codobligacion = Convert.ToInt64(resultado["cod_obligacion"]);
                            if (resultado["codope"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["codope"]);
                            if (resultado["fecha_oper"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["fecha_oper"]);
                            if (resultado["tip_ope"] != DBNull.Value) entidad.tipo_ope  = Convert.ToString(resultado["tip_ope"]);
                            if (resultado["fecha_real"] != DBNull.Value) entidad.fechareal = Convert.ToDateTime(resultado["fecha_real"]);
                            if (resultado["entidad"] != DBNull.Value) entidad.entidad = Convert.ToString(resultado["entidad"]);
                            if (resultado["numcuota"] != DBNull.Value) entidad.nrocuota = Convert.ToInt64(resultado["numcuota"]);
                            if (resultado["fechacuota"] != DBNull.Value) entidad.fechacuota = Convert.ToDateTime(resultado["fechacuota"]);
                            if (resultado["capital"] != DBNull.Value) entidad.amort_cap = Convert.ToInt64(resultado["capital"]);
                            if (resultado["int_corriente"] != DBNull.Value) entidad.interes_corriente = Convert.ToInt64(resultado["int_corriente"]);
                            if (resultado["int_mora"] != DBNull.Value) entidad.interes_mora = Convert.ToInt64(resultado["int_mora"]);
                            if (resultado["seguro"] != DBNull.Value) entidad.seguro = Convert.ToInt64(resultado["seguro"]);
                            if (resultado["total"] != DBNull.Value) entidad.cuotatotal = Convert.ToInt64(resultado["total"]);


                            lstObligacionCredito.Add(entidad);
                        }

                        return lstObligacionCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObligacionCreditoData", "ListarOperaciones", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla OBCREDITO - Datos Solicitud a aprobar dados unos filtros
        /// </summary>
        /// <param name="pOBCREDITO">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ObligacionCredito obtenidos</returns>
        public List<ObligacionCredito> ListarDatosSolicitudAprobacion(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ObligacionCredito> lstObligacionCredito = new List<ObligacionCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string param1 = pObligacionCredito.codobligacion == 0 ? " " : " and codobligacion=" + pObligacionCredito.codobligacion;

                        string param2 = pObligacionCredito.codentidad == 0 ? " " : " and codentidad=" + pObligacionCredito.codentidad;

                        string param3 = pObligacionCredito.montosolicitud == 0 ? " " : " and montosolicitado=" + pObligacionCredito.montosolicitud;

                        string sql = " SELECT CODOBLIGACION,(select b.nombrebanco from bancos b where b.cod_banco=o.codentidad) NOMENTIDAD, " +
                                     " (select l.nombrelinea from OBLINEAOBLIGACION l where l.codlineaobligacion = o.codlineaobligacion) NOMLINEAOB," +
                                     " FECHA_SOLICITUD, " +
                                     " MONTOSOLICITADO,decode(ESTADOOBLIGACION,'S', 'Solicitado',decode(ESTADOOBLIGACION,'D', 'Desembolsado',decode(ESTADOOBLIGACION,'C', 'Cancelada',decode(ESTADOOBLIGACION,'P','Pendiente Por Solicitud','Anulada')))) ESTADO " +
                                     " FROM OBCREDITO o " +
                                     " where 1=1 and ESTADOOBLIGACION='S' " + param1 + param2 + param3 +
                                     " order by codobligacion asc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ObligacionCredito entidad = new ObligacionCredito();

                            if (resultado["CODOBLIGACION"] != DBNull.Value) entidad.codobligacion = Convert.ToInt64(resultado["CODOBLIGACION"]);
                            if (resultado["NOMENTIDAD"] != DBNull.Value) entidad.nomentidad = Convert.ToString(resultado["NOMENTIDAD"]);
                            if (resultado["NOMLINEAOB"] != DBNull.Value) entidad.nomlineaobligacion = Convert.ToString(resultado["NOMLINEAOB"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["MONTOSOLICITADO"] != DBNull.Value) entidad.montosolicitud = Convert.ToInt64(resultado["MONTOSOLICITADO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estadoobligacion = Convert.ToString(resultado["ESTADO"]);

                            lstObligacionCredito.Add(entidad);
                        }

                        return lstObligacionCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObligacionCreditoData", "ListarDatosSolicitudAprobacion", ex);
                        return null;
                    }
                }
            }
        }


        public ObligacionCredito ProvisionCredito(ObligacionCredito pObligacion, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter PFECHA_CORTE = cmdTransaccionFactory.CreateParameter();
                        PFECHA_CORTE.ParameterName = "PFECHA_CORTE";
                        PFECHA_CORTE.DbType = DbType.Date;
                        PFECHA_CORTE.Direction = ParameterDirection.Input;
                        PFECHA_CORTE.Value = pObligacion.fecha_corte;

                        DbParameter pCODOBLIGACION = cmdTransaccionFactory.CreateParameter();
                        pCODOBLIGACION.ParameterName = "PCOD_OBLIGACION";
                        pCODOBLIGACION.Direction = ParameterDirection.Input;
                        pCODOBLIGACION.Value = pObligacion.codobligacion;

                        DbParameter PINTERESES = cmdTransaccionFactory.CreateParameter();
                        PINTERESES.ParameterName = "PINTERESES";
                        PINTERESES.Direction = ParameterDirection.Output;
                        PINTERESES.Value = 0;

                        DbParameter PDIASCAUSADOS = cmdTransaccionFactory.CreateParameter();
                        PDIASCAUSADOS.ParameterName = "PDIASCAUSADOS";
                        PDIASCAUSADOS.Direction = ParameterDirection.Output;
                        PDIASCAUSADOS.Value = 0;

                        cmdTransaccionFactory.Parameters.Add(PFECHA_CORTE);
                        cmdTransaccionFactory.Parameters.Add(pCODOBLIGACION);
                        cmdTransaccionFactory.Parameters.Add(PINTERESES);
                        cmdTransaccionFactory.Parameters.Add(PDIASCAUSADOS);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_PROVISION";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pObligacion, "OBCREDITO", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA

                        pObligacion.intereses = Convert.ToDecimal(PINTERESES.Value);
                        pObligacion.dias_causados = Convert.ToInt64(PDIASCAUSADOS.Value);

                        return pObligacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObligacionCreditoData", "ProvisionCredito", ex);
                        return null;
                    }
                }
            }
        }


    }
}