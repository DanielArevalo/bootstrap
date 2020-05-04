using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Obligaciones.Entities;

namespace Xpinn.Obligaciones.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla OBCREDITO
    /// </summary>
    public class SolicitudData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla OBCREDITO
        /// </summary>
        public SolicitudData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla OBCREDITO de la base de datos
        /// </summary>
        /// <param name="pSolicitud">Entidad Solicitud</param>
        /// <returns>Entidad Solicitud creada</returns>
        public Solicitud CrearSolicitud(Solicitud pSolicitud, DataTable dtComp, DataTable dtPagos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        DbParameter pCODOBLIGACION = cmdTransaccionFactory.CreateParameter();
                        pCODOBLIGACION.ParameterName = "p_codobligacion";
                        pCODOBLIGACION.Value = pSolicitud.codobligacion;
                        pCODOBLIGACION.Direction = ParameterDirection.InputOutput;

                        DbParameter pCODLINEAOBLIGACION = cmdTransaccionFactory.CreateParameter();
                        pCODLINEAOBLIGACION.ParameterName = "p_codlineaobligacion";
                        pCODLINEAOBLIGACION.Value = pSolicitud.codlineaobligacion;

                        DbParameter pCODENTIDAD = cmdTransaccionFactory.CreateParameter();
                        pCODENTIDAD.ParameterName = "p_codentidad";
                        pCODENTIDAD.Value = pSolicitud.codentidad;

                        DbParameter pMONTOSOLICITADO = cmdTransaccionFactory.CreateParameter();
                        pMONTOSOLICITADO.ParameterName = "p_montosolicitado";
                        pMONTOSOLICITADO.Value = pSolicitud.montosolicitado;

                        DbParameter pMONTOAPROBADO = cmdTransaccionFactory.CreateParameter();
                        pMONTOAPROBADO.ParameterName = "p_montoaprobado";
                        pMONTOAPROBADO.Value = pSolicitud.montoaprobado;

                        DbParameter pTIPOMONEDA = cmdTransaccionFactory.CreateParameter();
                        pTIPOMONEDA.ParameterName = "p_tipomoneda";
                        pTIPOMONEDA.Value = pSolicitud.tipomoneda;

                        DbParameter pSALDOCAPITAL = cmdTransaccionFactory.CreateParameter();
                        pSALDOCAPITAL.ParameterName = "p_saldocapital";
                        pSALDOCAPITAL.Value = pSolicitud.saldocapital;

                        DbParameter pFECHASOLICITUD = cmdTransaccionFactory.CreateParameter();
                        pFECHASOLICITUD.ParameterName = "p_fechasolicitud";
                        pFECHASOLICITUD.Value = pSolicitud.fechasolicitud;

                        DbParameter pFECHA_APROBACION = cmdTransaccionFactory.CreateParameter();
                        pFECHA_APROBACION.ParameterName = "p_fecha_aprobacion";
                        pFECHA_APROBACION.Value = pSolicitud.fecha_aprobacion;

                        DbParameter pFECHA_TERMINACION = cmdTransaccionFactory.CreateParameter();
                        pFECHA_TERMINACION.ParameterName = "p_fecha_terminacion";
                        pFECHA_TERMINACION.Value = pSolicitud.fecha_terminacion;

                        DbParameter pCUOTA = cmdTransaccionFactory.CreateParameter();
                        pCUOTA.ParameterName = "p_cuota";
                        pCUOTA.Value = pSolicitud.cuota;

                        DbParameter pFECHAPROXIMOPAGO = cmdTransaccionFactory.CreateParameter();
                        pFECHAPROXIMOPAGO.ParameterName = "p_fechaproximopago";
                        pFECHAPROXIMOPAGO.Value = pSolicitud.fechaproximopago;

                        DbParameter pFECHAULTIMOPAGO = cmdTransaccionFactory.CreateParameter();
                        pFECHAULTIMOPAGO.ParameterName = "p_fechaultimopago";
                        pFECHAULTIMOPAGO.Value = pSolicitud.fechaultimopago;

                        DbParameter pPLAZO = cmdTransaccionFactory.CreateParameter();
                        pPLAZO.ParameterName = "p_plazo";
                        pPLAZO.Value = pSolicitud.plazo;

                        DbParameter pGRACIA = cmdTransaccionFactory.CreateParameter();
                        pGRACIA.ParameterName = "p_gracia";
                        pGRACIA.Value = pSolicitud.gracia;

                        DbParameter pTIPO_GRACIA = cmdTransaccionFactory.CreateParameter();
                        pTIPO_GRACIA.ParameterName = "p_tipo_gracia";
                        pTIPO_GRACIA.Value = pSolicitud.tipo_gracia;

                        DbParameter pTIPOLIQUIDACION = cmdTransaccionFactory.CreateParameter();
                        pTIPOLIQUIDACION.ParameterName = "p_tipoliquidacion";
                        pTIPOLIQUIDACION.Value = pSolicitud.tipoliquidacion;

                        DbParameter pCODPERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        pCODPERIODICIDAD.ParameterName = "p_codperiodicidad";
                        pCODPERIODICIDAD.Value = pSolicitud.codperiodicidad;

                        DbParameter pESTADOOBLIGACION = cmdTransaccionFactory.CreateParameter();
                        pESTADOOBLIGACION.ParameterName = "p_estadoobligacion";
                        pESTADOOBLIGACION.Value = pSolicitud.estadoobligacion;

                        DbParameter pNUMEROPAGARE = cmdTransaccionFactory.CreateParameter();
                        pNUMEROPAGARE.ParameterName = "p_numeropagare";
                        pNUMEROPAGARE.Value = pSolicitud.numeropagare;

                        DbParameter p_cuenta = cmdTransaccionFactory.CreateParameter();
                        p_cuenta.ParameterName = "p_cuenta";
                        p_cuenta.Value = pSolicitud.cuenta;

                        cmdTransaccionFactory.Parameters.Add(pCODOBLIGACION);
                        cmdTransaccionFactory.Parameters.Add(pCODLINEAOBLIGACION);
                        cmdTransaccionFactory.Parameters.Add(pCODENTIDAD);
                        cmdTransaccionFactory.Parameters.Add(pMONTOSOLICITADO);
                        cmdTransaccionFactory.Parameters.Add(pMONTOAPROBADO);
                        cmdTransaccionFactory.Parameters.Add(pTIPOMONEDA);
                        cmdTransaccionFactory.Parameters.Add(pSALDOCAPITAL);
                        cmdTransaccionFactory.Parameters.Add(pFECHASOLICITUD);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_APROBACION);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_TERMINACION);
                        cmdTransaccionFactory.Parameters.Add(pCUOTA);
                        cmdTransaccionFactory.Parameters.Add(pFECHAPROXIMOPAGO);
                        cmdTransaccionFactory.Parameters.Add(pFECHAULTIMOPAGO);
                        cmdTransaccionFactory.Parameters.Add(pPLAZO);
                        cmdTransaccionFactory.Parameters.Add(pGRACIA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_GRACIA);
                        cmdTransaccionFactory.Parameters.Add(pTIPOLIQUIDACION);
                        cmdTransaccionFactory.Parameters.Add(pCODPERIODICIDAD);
                        cmdTransaccionFactory.Parameters.Add(pESTADOOBLIGACION);
                        cmdTransaccionFactory.Parameters.Add(pNUMEROPAGARE);
                        cmdTransaccionFactory.Parameters.Add(p_cuenta);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_SOLICITUD_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pSolicitud, "OBCREDITO", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pSolicitud.codobligacion = Convert.ToInt64(pCODOBLIGACION.Value);

                        //se guardan los datos en OBCOMPONENTECREDITO

                        cmdTransaccionFactory.Parameters.Clear();
                        DbParameter pCOD_OBLIGACION = cmdTransaccionFactory.CreateParameter();
                        pCOD_OBLIGACION.ParameterName = "p_codobligacion";
                        pCOD_OBLIGACION.Value = pSolicitud.codobligacion;

                        DbParameter PCODCOMPONENTE = cmdTransaccionFactory.CreateParameter();
                        PCODCOMPONENTE.ParameterName = "p_codcomponente";
                        PCODCOMPONENTE.Value = 2;

                        DbParameter pCALCULO_COMPONENTE = cmdTransaccionFactory.CreateParameter();
                        pCALCULO_COMPONENTE.ParameterName = "p_calculocomponente";
                        pCALCULO_COMPONENTE.Value = pSolicitud.calculocomponente;

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        ptipo_historico.Value = pSolicitud.tipo_historico;

                        DbParameter pcodtipotasa = cmdTransaccionFactory.CreateParameter();
                        pcodtipotasa.ParameterName = "p_codtipotasa";
                        pcodtipotasa.Value = pSolicitud.cod_tipo_tasa;
                        pcodtipotasa.Direction = ParameterDirection.InputOutput;

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "p_tasa";
                        ptasa.Value = pSolicitud.tasa;
                        ptasa.DbType = DbType.Decimal;

                        DbParameter spread = cmdTransaccionFactory.CreateParameter();
                        spread.ParameterName = "p_spread";
                        spread.Value = pSolicitud.spread;

                        cmdTransaccionFactory.Parameters.Add(pCOD_OBLIGACION);
                        cmdTransaccionFactory.Parameters.Add(PCODCOMPONENTE);
                        cmdTransaccionFactory.Parameters.Add(pCALCULO_COMPONENTE);
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);
                        cmdTransaccionFactory.Parameters.Add(pcodtipotasa);
                        cmdTransaccionFactory.Parameters.Add(ptasa);
                        cmdTransaccionFactory.Parameters.Add(spread);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_COMP_CRED_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // se envia a la base de datos los Componentes Adicionales
                        if (dtComp.Rows.Count > 0)
                        {
                            foreach (DataRow fila in dtComp.Rows)
                            {
                                if (dtComp.Rows[0][0].ToString() != "")
                                {
                                    cmdTransaccionFactory.Parameters.Clear();
                                    DbParameter pCODe_OBLIGACION = cmdTransaccionFactory.CreateParameter();
                                    pCODe_OBLIGACION.ParameterName = "pcodigoobligacion";
                                    pCODe_OBLIGACION.Value = pSolicitud.codobligacion;

                                    DbParameter PCODeCOMPONENTE = cmdTransaccionFactory.CreateParameter();
                                    PCODeCOMPONENTE.ParameterName = "pcodigocomponente";
                                    PCODeCOMPONENTE.Value = fila[0];

                                    DbParameter pformula = cmdTransaccionFactory.CreateParameter();
                                    pformula.ParameterName = "pformula";
                                    pformula.Value = fila[1];

                                    DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                                    pvalor.ParameterName = "pvalor";
                                    pvalor.Value = fila[2];
                                    DbParameter pfinanciado = cmdTransaccionFactory.CreateParameter();
                                    if (fila[3].ToString() == "True")
                                    {
                                        pfinanciado.ParameterName = "pfinanciado";
                                        pfinanciado.Value = "1";
                                    }
                                    else
                                    {
                                        pfinanciado.ParameterName = "pfinanciado";
                                        pfinanciado.Value = "0";
                                    }

                                    cmdTransaccionFactory.Parameters.Add(pCODe_OBLIGACION);
                                    cmdTransaccionFactory.Parameters.Add(PCODeCOMPONENTE);
                                    cmdTransaccionFactory.Parameters.Add(pformula);
                                    cmdTransaccionFactory.Parameters.Add(pvalor);
                                    cmdTransaccionFactory.Parameters.Add(pfinanciado);

                                    cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                    cmdTransaccionFactory.CommandText = "USP_XPINN_OB_COMPONENT_CREAR";
                                    cmdTransaccionFactory.ExecuteNonQuery();
                                }

                            }
                        }

                        // Se envia a la base de datos los Pagos Extraordinarios
                        if (dtPagos.Rows.Count > 0)
                        {
                            foreach (DataRow fila in dtPagos.Rows)
                            {
                                if (dtPagos.Rows[0][0].ToString() != "")
                                {
                                    cmdTransaccionFactory.Parameters.Clear();
                                    DbParameter pCODex_OBLIGACION = cmdTransaccionFactory.CreateParameter();
                                    pCODex_OBLIGACION.ParameterName = "pcodigoobligacion";
                                    pCODex_OBLIGACION.Value = pSolicitud.codobligacion;

                                    DbParameter pcodeperiodicidad = cmdTransaccionFactory.CreateParameter();
                                    pcodeperiodicidad.ParameterName = "pcodigoperiodicidad";
                                    pcodeperiodicidad.Value = fila[0];

                                    DbParameter pvalore = cmdTransaccionFactory.CreateParameter();
                                    pvalore.ParameterName = "pvalor";
                                    pvalore.Value = fila[1];

                                    cmdTransaccionFactory.Parameters.Add(pCODex_OBLIGACION);
                                    cmdTransaccionFactory.Parameters.Add(pcodeperiodicidad);
                                    cmdTransaccionFactory.Parameters.Add(pvalore);

                                    cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                    cmdTransaccionFactory.CommandText = "USP_XPINN_OB_PAGEXT_C";
                                    cmdTransaccionFactory.ExecuteNonQuery();
                                }
                            }
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return pSolicitud;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudData", "CrearSolicitud", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla OBCREDITO de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Solicitud modificada</returns>
        public Solicitud ModificarSolicitud(Xpinn.Obligaciones.Entities.ObligacionCredito pOperacion, Solicitud pSolicitud, DataTable dtComp, DataTable dtPagos, Usuario pUsuario)
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
                        pcode_opera.ParameterName = "PCODIGOOPER";
                        pcode_opera.Value = pOperacion.cod_ope;
                        pcode_opera.Direction = ParameterDirection.Output;

                        DbParameter pcode_tope = cmdTransaccionFactory.CreateParameter();
                        pcode_tope.ParameterName = "PCODIGOTIPOOPE";
                        pcode_tope.Value = pOperacion.cod_tipo_ope;

                        DbParameter pcode_usuari = cmdTransaccionFactory.CreateParameter();
                        pcode_usuari.ParameterName = "PCODIGOUSUARIO";
                        pcode_usuari.Value = pUsuario.codusuario;

                        DbParameter pcode_oficina = cmdTransaccionFactory.CreateParameter();
                        pcode_oficina.ParameterName = "PCODIGOOFICINA";
                        pcode_oficina.Value = pUsuario.cod_oficina;

                        DbParameter pfechaoper = cmdTransaccionFactory.CreateParameter();
                        pfechaoper.ParameterName = "PFECHAOPER";
                        pfechaoper.Value = pOperacion.fechacuota;

                        DbParameter pfechacalc = cmdTransaccionFactory.CreateParameter();
                        pfechacalc.ParameterName = "PFECHACALC";
                        pfechacalc.Value = pOperacion.fechacuota;


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

                        //Insertamos las transacciones pertinentes a las obligacion

                        cmdTransaccionFactory.Parameters.Clear();
                        DbParameter pCODOBLIGACION = cmdTransaccionFactory.CreateParameter();
                        pCODOBLIGACION.ParameterName = "p_codobligacion";
                        pCODOBLIGACION.Value = pSolicitud.codobligacion;
                        pCODOBLIGACION.Direction = ParameterDirection.InputOutput;

                        DbParameter pCODLINEAOBLIGACION = cmdTransaccionFactory.CreateParameter();
                        pCODLINEAOBLIGACION.ParameterName = "p_codlineaobligacion";
                        pCODLINEAOBLIGACION.Value = pSolicitud.codlineaobligacion;

                        DbParameter pCODENTIDAD = cmdTransaccionFactory.CreateParameter();
                        pCODENTIDAD.ParameterName = "p_codentidad";
                        pCODENTIDAD.Value = pSolicitud.codentidad;

                        DbParameter pMONTOSOLICITADO = cmdTransaccionFactory.CreateParameter();
                        pMONTOSOLICITADO.ParameterName = "p_montosolicitado";
                        pMONTOSOLICITADO.Value = pSolicitud.montosolicitado;

                        DbParameter pMONTOAPROBADO = cmdTransaccionFactory.CreateParameter();
                        pMONTOAPROBADO.ParameterName = "p_montoaprobado";
                        pMONTOAPROBADO.Value = pSolicitud.montoaprobado;

                        DbParameter pTIPOMONEDA = cmdTransaccionFactory.CreateParameter();
                        pTIPOMONEDA.ParameterName = "p_tipomoneda";
                        pTIPOMONEDA.Value = pSolicitud.tipomoneda;

                        DbParameter pSALDOCAPITAL = cmdTransaccionFactory.CreateParameter();
                        pSALDOCAPITAL.ParameterName = "p_saldocapital";
                        pSALDOCAPITAL.Value = pSolicitud.saldocapital;

                        DbParameter pFECHASOLICITUD = cmdTransaccionFactory.CreateParameter();
                        pFECHASOLICITUD.ParameterName = "p_fechasolicitud";
                        pFECHASOLICITUD.Value = pSolicitud.fechasolicitud;

                        DbParameter pFECHA_APROBACION = cmdTransaccionFactory.CreateParameter();
                        pFECHA_APROBACION.ParameterName = "p_fecha_aprobacion";
                        pFECHA_APROBACION.Value = pSolicitud.fecha_aprobacion;

                        DbParameter pFECHA_TERMINACION = cmdTransaccionFactory.CreateParameter();
                        pFECHA_TERMINACION.ParameterName = "p_fecha_terminacion";
                        pFECHA_TERMINACION.Value = pSolicitud.fecha_terminacion;

                        DbParameter pCUOTA = cmdTransaccionFactory.CreateParameter();
                        pCUOTA.ParameterName = "p_cuota";
                        pCUOTA.Value = pSolicitud.cuota;

                        DbParameter pFECHAPROXIMOPAGO = cmdTransaccionFactory.CreateParameter();
                        pFECHAPROXIMOPAGO.ParameterName = "p_fechaproximopago";
                        pFECHAPROXIMOPAGO.Value = pSolicitud.fechaproximopago;

                        DbParameter pFECHAULTIMOPAGO = cmdTransaccionFactory.CreateParameter();
                        pFECHAULTIMOPAGO.ParameterName = "p_fechaultimopago";
                        pFECHAULTIMOPAGO.Value = pSolicitud.fechaultimopago;

                        DbParameter pPLAZO = cmdTransaccionFactory.CreateParameter();
                        pPLAZO.ParameterName = "p_plazo";
                        pPLAZO.Value = pSolicitud.plazo;

                        DbParameter pGRACIA = cmdTransaccionFactory.CreateParameter();
                        pGRACIA.ParameterName = "p_gracia";
                        pGRACIA.Value = pSolicitud.gracia;

                        DbParameter pTIPO_GRACIA = cmdTransaccionFactory.CreateParameter();
                        pTIPO_GRACIA.ParameterName = "p_tipo_gracia";
                        pTIPO_GRACIA.Value = pSolicitud.tipo_gracia;

                        DbParameter pTIPOLIQUIDACION = cmdTransaccionFactory.CreateParameter();
                        pTIPOLIQUIDACION.ParameterName = "p_tipoliquidacion";
                        pTIPOLIQUIDACION.Value = pSolicitud.tipoliquidacion;

                        DbParameter pCODPERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        pCODPERIODICIDAD.ParameterName = "p_codperiodicidad";
                        pCODPERIODICIDAD.Value = pSolicitud.codperiodicidad;

                        DbParameter pESTADOOBLIGACION = cmdTransaccionFactory.CreateParameter();
                        pESTADOOBLIGACION.ParameterName = "p_estadoobligacion";
                        pESTADOOBLIGACION.Value = pSolicitud.estadoobligacion;

                        DbParameter pNUMEROPAGARE = cmdTransaccionFactory.CreateParameter();
                        pNUMEROPAGARE.ParameterName = "p_numeropagare";
                        pNUMEROPAGARE.Value = pSolicitud.numeropagare;

                        DbParameter p_cod_ope = cmdTransaccionFactory.CreateParameter();
                        p_cod_ope.ParameterName = "p_cod_ope";
                        p_cod_ope.Value = pcode_opera.Value;


                        cmdTransaccionFactory.Parameters.Add(pCODOBLIGACION);
                        cmdTransaccionFactory.Parameters.Add(pCODLINEAOBLIGACION);
                        cmdTransaccionFactory.Parameters.Add(pCODENTIDAD);
                        cmdTransaccionFactory.Parameters.Add(pMONTOSOLICITADO);
                        cmdTransaccionFactory.Parameters.Add(pMONTOAPROBADO);
                        cmdTransaccionFactory.Parameters.Add(pTIPOMONEDA);
                        cmdTransaccionFactory.Parameters.Add(pSALDOCAPITAL);
                        cmdTransaccionFactory.Parameters.Add(pFECHASOLICITUD);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_APROBACION);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_TERMINACION);
                        cmdTransaccionFactory.Parameters.Add(pCUOTA);
                        cmdTransaccionFactory.Parameters.Add(pFECHAPROXIMOPAGO);
                        cmdTransaccionFactory.Parameters.Add(pFECHAULTIMOPAGO);
                        cmdTransaccionFactory.Parameters.Add(pPLAZO);
                        cmdTransaccionFactory.Parameters.Add(pGRACIA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_GRACIA);
                        cmdTransaccionFactory.Parameters.Add(pTIPOLIQUIDACION);
                        cmdTransaccionFactory.Parameters.Add(pCODPERIODICIDAD);
                        cmdTransaccionFactory.Parameters.Add(pESTADOOBLIGACION);
                        cmdTransaccionFactory.Parameters.Add(pNUMEROPAGARE);
                        cmdTransaccionFactory.Parameters.Add(p_cod_ope);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_SOLICITUD_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pSolicitud, "OBCREDITO", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        pSolicitud.codobligacion = Convert.ToInt64(pCODOBLIGACION.Value);

                        //se guardan los datos en OBCOMPONENTECREDITO

                        cmdTransaccionFactory.Parameters.Clear();
                        DbParameter pCOD_OBLIGACION = cmdTransaccionFactory.CreateParameter();
                        pCOD_OBLIGACION.ParameterName = "p_codobligacion";
                        pCOD_OBLIGACION.Value = pSolicitud.codobligacion;

                        DbParameter PCODCOMPONENTE = cmdTransaccionFactory.CreateParameter();
                        PCODCOMPONENTE.ParameterName = "p_codcomponente";
                        PCODCOMPONENTE.Value = 2;

                        DbParameter pCALCULO_COMPONENTE = cmdTransaccionFactory.CreateParameter();
                        pCALCULO_COMPONENTE.ParameterName = "p_calculocomponente";
                        pCALCULO_COMPONENTE.Value = pSolicitud.calculocomponente;

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        ptipo_historico.Value = pSolicitud.tipo_historico;

                        DbParameter pcodtipotasa = cmdTransaccionFactory.CreateParameter();
                        pcodtipotasa.ParameterName = "p_codtipotasa";
                        pcodtipotasa.Value = pSolicitud.cod_tipo_tasa;

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "p_tasa";
                        ptasa.Value = pSolicitud.tasa;

                        DbParameter spread = cmdTransaccionFactory.CreateParameter();
                        spread.ParameterName = "p_spread";
                        spread.Value = pSolicitud.spread;

                        cmdTransaccionFactory.Parameters.Add(pCOD_OBLIGACION);
                        cmdTransaccionFactory.Parameters.Add(PCODCOMPONENTE);
                        cmdTransaccionFactory.Parameters.Add(pCALCULO_COMPONENTE);
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);
                        cmdTransaccionFactory.Parameters.Add(pcodtipotasa);
                        cmdTransaccionFactory.Parameters.Add(ptasa);
                        cmdTransaccionFactory.Parameters.Add(spread);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_COMP_CRED_U";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // se envia a la base de datos los Componentes Adicionales
                        if (dtComp.Rows.Count > 0)
                        {
                            //se elimina la informacion para volverla a insertar a la tabla componente adicional

                            cmdTransaccionFactory.Parameters.Clear();
                            DbParameter pCODx_OBLIGACION = cmdTransaccionFactory.CreateParameter();
                            pCODx_OBLIGACION.ParameterName = "pcodigoobligacion";
                            pCODx_OBLIGACION.Value = pSolicitud.codobligacion;

                            cmdTransaccionFactory.Parameters.Add(pCODx_OBLIGACION);

                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_OB_COMPONENT_BORRAR";
                            cmdTransaccionFactory.ExecuteNonQuery();


                            foreach (DataRow fila in dtComp.Rows)
                            {
                                if (dtComp.Rows[0][0].ToString() != "")
                                {
                                    cmdTransaccionFactory.Parameters.Clear();
                                    DbParameter pCODe_OBLIGACION = cmdTransaccionFactory.CreateParameter();
                                    pCODe_OBLIGACION.ParameterName = "pcodigoobligacion";
                                    pCODe_OBLIGACION.Value = pSolicitud.codobligacion;

                                    DbParameter PCODeCOMPONENTE = cmdTransaccionFactory.CreateParameter();
                                    PCODeCOMPONENTE.ParameterName = "pcodigocomponente";
                                    PCODeCOMPONENTE.Value = fila[6];

                                    DbParameter pformula = cmdTransaccionFactory.CreateParameter();
                                    pformula.ParameterName = "pformula";
                                    pformula.Value = fila[1];

                                    DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                                    pvalor.ParameterName = "pvalor";
                                    pvalor.Value = fila[2];

                                    DbParameter pfinanciado = cmdTransaccionFactory.CreateParameter();
                                    pfinanciado.ParameterName = "pfinanciado";
                                    if (fila[3].ToString() == "True")
                                    {
                                        pfinanciado.ParameterName = "pfinanciado";
                                        pfinanciado.Value = "1";
                                    }
                                    else
                                    {
                                        pfinanciado.ParameterName = "pfinanciado";
                                        pfinanciado.Value = "0";
                                    }
                                    cmdTransaccionFactory.Parameters.Add(pCODe_OBLIGACION);
                                    cmdTransaccionFactory.Parameters.Add(PCODeCOMPONENTE);
                                    cmdTransaccionFactory.Parameters.Add(pformula);
                                    cmdTransaccionFactory.Parameters.Add(pvalor);
                                    cmdTransaccionFactory.Parameters.Add(pfinanciado);

                                    cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                    cmdTransaccionFactory.CommandText = "USP_XPINN_OB_COMPONENT_CREAR";
                                    cmdTransaccionFactory.ExecuteNonQuery();
                                }

                            }
                        }


                        // se envia a la base de datos los Pagos Extraordinarios
                        if (dtPagos.Rows.Count > 0)
                        {

                            cmdTransaccionFactory.Parameters.Clear();
                            DbParameter pCODexi_OBLIGACION = cmdTransaccionFactory.CreateParameter();
                            pCODexi_OBLIGACION.ParameterName = "pcodigoobligacion";
                            pCODexi_OBLIGACION.Value = pSolicitud.codobligacion;

                            cmdTransaccionFactory.Parameters.Add(pCODexi_OBLIGACION);

                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_OB_PAGEXT_D";
                            cmdTransaccionFactory.ExecuteNonQuery();


                            foreach (DataRow fila in dtPagos.Rows)
                            {
                                if (dtPagos.Rows[0][0].ToString() != "")
                                {
                                    cmdTransaccionFactory.Parameters.Clear();
                                    DbParameter pCODex_OBLIGACION = cmdTransaccionFactory.CreateParameter();
                                    pCODex_OBLIGACION.ParameterName = "pcodigoobligacion";
                                    pCODex_OBLIGACION.Value = pSolicitud.codobligacion;

                                    DbParameter pcodeperiodicidad = cmdTransaccionFactory.CreateParameter();
                                    pcodeperiodicidad.ParameterName = "pcodigoperiodicidad";
                                    pcodeperiodicidad.Value = fila[0];

                                    DbParameter pvalore = cmdTransaccionFactory.CreateParameter();
                                    pvalore.ParameterName = "pvalor";
                                    pvalore.Value = fila[1];

                                    cmdTransaccionFactory.Parameters.Add(pCODex_OBLIGACION);
                                    cmdTransaccionFactory.Parameters.Add(pcodeperiodicidad);
                                    cmdTransaccionFactory.Parameters.Add(pvalore);

                                    cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                    cmdTransaccionFactory.CommandText = "USP_XPINN_OB_PAGEXT_C";
                                    cmdTransaccionFactory.ExecuteNonQuery();
                                }
                            }
                        }

                        pSolicitud.cod_ope = long.Parse(pcode_opera.Value.ToString());

                        dbConnectionFactory.CerrarConexion(connection);
                        return pSolicitud;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudData", "ModificarSolicitud", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla OBCREDITO de la base de datos
        /// </summary>
        /// <param name="pId">identificador de OBCREDITO</param>
        public void EliminarSolicitud(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Solicitud pSolicitud = new Solicitud();

                        if (pUsuario.programaGeneraLog)
                            pSolicitud = ConsultarSolicitud(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCODOBLIGACION = cmdTransaccionFactory.CreateParameter();
                        pCODOBLIGACION.ParameterName = "p_codobligacion";
                        pCODOBLIGACION.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCODOBLIGACION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_Obligaciones_OBCREDITO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pSolicitud, "OBCREDITO", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudData", "EliminarSolicitud", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla OBCREDITO de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla OBCREDITO</param>
        /// <returns>Entidad Solicitud consultado</returns>
        public Solicitud ConsultarSolicitud(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Solicitud entidad = new Solicitud();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT CODOBLIGACION,CODLINEAOBLIGACION,CODENTIDAD,MONTOSOLICITADO,MONTOAPROBADO,TIPOMONEDA,SALDOCAPITAL,FECHA_SOLICITUD, " +
                                     " FECHA_APROBACION,FECHA_TERMINACION,VALOR_CUOTA,FECHAPROXIMOPAGO,FECHAULTIMOPAGO,PLAZO,TIPOLIQUIDACION, CODPERIODICIDAD, " +
                                     " ESTADOOBLIGACION,NUMEROPAGARE, GRACIA, TIPO_GRACIA, " +
                                     " (select b.calculocomponente from OBCOMPONENTECREDITO b where a.codobligacion=b.codobligacion And b.codcomponente = 2) calculocomponente, " +
                                     " (select b.tipo_historico from OBCOMPONENTECREDITO b where a.codobligacion=b.codobligacion And b.codcomponente = 2) tipo_historico, " +
                                     " (select b.cod_tipo_tasa from OBCOMPONENTECREDITO b where a.codobligacion=b.codobligacion And b.codcomponente = 2) codtipotasa, " +
                                     " (select b.tasa from OBCOMPONENTECREDITO b where a.codobligacion=b.codobligacion And b.codcomponente = 2) tasa, " +
                                     " (select b.spread from OBCOMPONENTECREDITO b where a.codobligacion=b.codobligacion And b.codcomponente = 2) spread " +
                                     " FROM  OBCREDITO a WHERE codobligacion = " + pId.ToString();


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //OBCREDITO
                            if (resultado["CODOBLIGACION"] != DBNull.Value) entidad.codobligacion = Convert.ToInt64(resultado["CODOBLIGACION"]);
                            if (resultado["CODLINEAOBLIGACION"] != DBNull.Value) entidad.codlineaobligacion = Convert.ToInt64(resultado["CODLINEAOBLIGACION"]);
                            if (resultado["CODENTIDAD"] != DBNull.Value) entidad.codentidad = Convert.ToInt64(resultado["CODENTIDAD"]);
                            if (resultado["MONTOSOLICITADO"] != DBNull.Value) entidad.montosolicitado = Convert.ToInt64(resultado["MONTOSOLICITADO"]);
                            if (resultado["MONTOAPROBADO"] != DBNull.Value) entidad.montoaprobado = Convert.ToInt64(resultado["MONTOAPROBADO"]);
                            if (resultado["TIPOMONEDA"] != DBNull.Value) entidad.tipomoneda = Convert.ToInt64(resultado["TIPOMONEDA"]);
                            if (resultado["SALDOCAPITAL"] != DBNull.Value) entidad.saldocapital = Convert.ToInt64(resultado["SALDOCAPITAL"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fechasolicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["FECHA_TERMINACION"] != DBNull.Value) entidad.fecha_terminacion = Convert.ToDateTime(resultado["FECHA_TERMINACION"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FECHAPROXIMOPAGO"] != DBNull.Value) entidad.fechaproximopago = Convert.ToDateTime(resultado["FECHAPROXIMOPAGO"]);
                            if (resultado["FECHAULTIMOPAGO"] != DBNull.Value) entidad.fechaultimopago = Convert.ToDateTime(resultado["FECHAULTIMOPAGO"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["GRACIA"] != DBNull.Value) entidad.gracia = Convert.ToInt64(resultado["GRACIA"]);
                            if (resultado["TIPO_GRACIA"] != DBNull.Value) entidad.tipo_gracia = Convert.ToInt64(resultado["TIPO_GRACIA"]);
                            if (resultado["TIPOLIQUIDACION"] != DBNull.Value) entidad.tipoliquidacion = Convert.ToInt64(resultado["TIPOLIQUIDACION"]);
                            if (resultado["CODPERIODICIDAD"] != DBNull.Value) entidad.codperiodicidad = Convert.ToInt64(resultado["CODPERIODICIDAD"]);
                            if (resultado["ESTADOOBLIGACION"] != DBNull.Value) entidad.estadoobligacion = Convert.ToString(resultado["ESTADOOBLIGACION"]);
                            if (resultado["NUMEROPAGARE"] != DBNull.Value) entidad.numeropagare = Convert.ToInt64(resultado["NUMEROPAGARE"]);
                            //OBCOMPONENTECREDITO
                            if (resultado["calculocomponente"] != DBNull.Value) entidad.calculocomponente = Convert.ToInt64(resultado["calculocomponente"]);
                            if (resultado["tipo_historico"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt64(resultado["tipo_historico"]);
                            if (resultado["codtipotasa"] != DBNull.Value) entidad.cod_tipo_tasa = Convert.ToInt64(resultado["codtipotasa"]);
                            if (resultado["tasa"] != DBNull.Value) entidad.tasa = decimal.Parse(resultado["tasa"].ToString());
                            if (resultado["spread"] != DBNull.Value) entidad.spread = decimal.Parse(resultado["spread"].ToString());
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudData", "ConsultarSolicitud", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla OBCREDITO dados unos filtros
        /// </summary>
        /// <param name="pOBCREDITO">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Solicitud obtenidos</returns>
        public List<Solicitud> ListarSolicitud(Solicitud pSolicitud, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Solicitud> lstSolicitud = new List<Solicitud>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  OBCREDITO  " + ObtenerFiltro(pSolicitud);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Solicitud entidad = new Solicitud();

                            if (resultado["CODOBLIGACION"] != DBNull.Value) entidad.codobligacion = Convert.ToInt64(resultado["CODOBLIGACION"]);
                            if (resultado["CODLINEAOBLIGACION"] != DBNull.Value) entidad.codlineaobligacion = Convert.ToInt64(resultado["CODLINEAOBLIGACION"]);
                            if (resultado["CODENTIDAD"] != DBNull.Value) entidad.codentidad = Convert.ToInt64(resultado["CODENTIDAD"]);
                            if (resultado["MONTOSOLICITADO"] != DBNull.Value) entidad.montosolicitado = Convert.ToInt64(resultado["MONTOSOLICITADO"]);
                            if (resultado["MONTOAPROBADO"] != DBNull.Value) entidad.montoaprobado = Convert.ToInt64(resultado["MONTOAPROBADO"]);
                            if (resultado["TIPOMONEDA"] != DBNull.Value) entidad.tipomoneda = Convert.ToInt64(resultado["TIPOMONEDA"]);
                            if (resultado["SALDOCAPITAL"] != DBNull.Value) entidad.saldocapital = Convert.ToInt64(resultado["SALDOCAPITAL"]);
                            if (resultado["FECHASOLICITUD"] != DBNull.Value) entidad.fechasolicitud = Convert.ToDateTime(resultado["FECHASOLICITUD"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["FECHA_TERMINACION"] != DBNull.Value) entidad.fecha_terminacion = Convert.ToDateTime(resultado["FECHA_TERMINACION"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FECHAPROXIMOPAGO"] != DBNull.Value) entidad.fechaproximopago = Convert.ToDateTime(resultado["FECHAPROXIMOPAGO"]);
                            if (resultado["FECHAULTIMOPAGO"] != DBNull.Value) entidad.fechaultimopago = Convert.ToDateTime(resultado["FECHAULTIMOPAGO"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["GRACIA"] != DBNull.Value) entidad.gracia = Convert.ToInt64(resultado["GRACIA"]);
                            if (resultado["TIPO_GRACIA"] != DBNull.Value) entidad.tipo_gracia = Convert.ToInt64(resultado["TIPO_GRACIA"]);
                            if (resultado["TIPOLIQUIDACION"] != DBNull.Value) entidad.tipoliquidacion = Convert.ToInt64(resultado["TIPOLIQUIDACION"]);
                            if (resultado["CODPERIODICIDAD"] != DBNull.Value) entidad.codperiodicidad = Convert.ToInt64(resultado["CODPERIODICIDAD"]);
                            if (resultado["ESTADOOBLIGACION"] != DBNull.Value) entidad.estadoobligacion = Convert.ToString(resultado["ESTADOOBLIGACION"]);
                            if (resultado["NUMEROPAGARE"] != DBNull.Value) entidad.numeropagare = Convert.ToInt64(resultado["NUMEROPAGARE"]);

                            lstSolicitud.Add(entidad);
                        }

                        return lstSolicitud;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudData", "ListarSolicitud", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene la informcion del Estado de Cuenta
        /// </summary>
        /// <param name="pId">identificador de registro </param>
        /// <returns>Entidad ObligacionCredito consultado</returns>
        public Solicitud ConsultarEstadoCuenta(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Solicitud entidad = new Solicitud();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT a.CODOBLIGACION COD_OBLIGACION, a.NUMEROPAGARE NUMERO_PAGARE,a.CODENTIDAD COD_ENTIDAD,a.FECHA_APROBACION FECHAAPROBACION, a.FECHA_SOLICITUD FECHASOLICITUD, " +
                                     " a.CODPERIODICIDAD COD_PERIODICIDAD,a.MONTOAPROBADO MONTO_APROBADO, a.MONTOSOLICITADO MONTO_SOLICITADO, a.SALDOCAPITAL SALDO_CAPITAL, " +
                                     " a.VALOR_CUOTA CUOT, a.PLAZO PLAZ, a.GRACIA, a.TIPO_GRACIA, a.TIPOLIQUIDACION TIPO_LIQUIDACION,a.FECHAPROXIMOPAGO FECHA_PROXIMOPAGO, " +
                                     " decode(a.ESTADOOBLIGACION,'S', 'Solicitado',decode(a.ESTADOOBLIGACION,'D', 'Desembolsado',decode(a.ESTADOOBLIGACION,'C', 'Cancelada',decode(a.ESTADOOBLIGACION,'P','Pendiente Por Solicitud','Anulada')))) ESTADO_OBLIGACION, " +
                                     " a.TIPOMONEDA TIPO_MONEDA, b.CALCULOCOMPONENTE, b.TIPO_HISTORICO, b.TASA TAS, b.SPREAD SPREA, b.COD_TIPO_TASA CODTIPOTASA, a.cuotas_pagadas CUOTASPAGADAS, a.FECHA_INICIO FECHAINICIO,a.FECHA_TERMINACION FECHATERMINACION,a.FECHAULTIMOPAGO FECHA_ULTIMOPAGO, a.FECHAPROXIMOPAGO FECHA_PROXIMOPAGO " +
                                     " FROM OBCREDITO a LEFT JOIN OBCOMPONENTECREDITO b ON a.codobligacion = b.codobligacion WHERE a.codobligacion = " + pId.ToString() + " ORDER BY B.CODCOMPONENTE";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_OBLIGACION"] != DBNull.Value) entidad.codobligacion = Convert.ToInt64(resultado["COD_OBLIGACION"]);
                            if (resultado["NUMERO_PAGARE"] != DBNull.Value) entidad.numeropagare = Convert.ToInt64(resultado["NUMERO_PAGARE"]);
                            if (resultado["COD_ENTIDAD"] != DBNull.Value) entidad.codentidad = Convert.ToInt64(resultado["COD_ENTIDAD"]);
                            if (resultado["FECHAAPROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHAAPROBACION"]);
                            if (resultado["FECHASOLICITUD"] != DBNull.Value) entidad.fechasolicitud = Convert.ToDateTime(resultado["FECHASOLICITUD"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.codperiodicidad = Convert.ToInt64(resultado["COD_PERIODICIDAD"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.montoaprobado = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.montosolicitado = Convert.ToInt64(resultado["MONTO_SOLICITADO"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldocapital = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["CUOT"] != DBNull.Value) entidad.cuota = Convert.ToInt64(resultado["CUOT"]);
                            if (resultado["PLAZ"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZ"]);
                            if (resultado["GRACIA"] != DBNull.Value) entidad.gracia = Convert.ToInt64(resultado["GRACIA"]);
                            if (resultado["TIPO_GRACIA"] != DBNull.Value) entidad.tipo_gracia = Convert.ToInt64(resultado["TIPO_GRACIA"]);
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipoliquidacion = Convert.ToInt64(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["FECHA_PROXIMOPAGO"] != DBNull.Value) entidad.fechaproximopago = Convert.ToDateTime(resultado["FECHA_PROXIMOPAGO"]);
                            if (resultado["ESTADO_OBLIGACION"] != DBNull.Value) entidad.estadoobligacion = Convert.ToString(resultado["ESTADO_OBLIGACION"]);
                            if (resultado["TIPO_MONEDA"] != DBNull.Value) entidad.tipomoneda = Convert.ToInt64(resultado["TIPO_MONEDA"]);
                            if (resultado["CALCULOCOMPONENTE"] != DBNull.Value) entidad.calculocomponente = Convert.ToInt64(resultado["CALCULOCOMPONENTE"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt64(resultado["TIPO_HISTORICO"]);
                            if (resultado["CODTIPOTASA"] != DBNull.Value) entidad.cod_tipo_tasa = Convert.ToInt64(resultado["CODTIPOTASA"]);
                            if (resultado["TAS"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TAS"]);
                            if (resultado["SPREA"] != DBNull.Value) entidad.spread = Convert.ToDecimal(resultado["SPREA"]);
                            if (resultado["FECHATERMINACION"] != DBNull.Value) entidad.fecha_terminacion = Convert.ToDateTime(resultado["FECHATERMINACION"]);
                            if (resultado["FECHA_ULTIMOPAGO"] != DBNull.Value) entidad.fechaultimopago = Convert.ToDateTime(resultado["FECHA_ULTIMOPAGO"]);
                            if (resultado["FECHA_PROXIMOPAGO"] != DBNull.Value) entidad.fechaproximopago = Convert.ToDateTime(resultado["FECHA_PROXIMOPAGO"]);
                            if (resultado["FECHAINICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHAINICIO"]);
                            if (resultado["CUOTASPAGADAS"] != DBNull.Value) entidad.cuotaspagadas = Convert.ToInt64(resultado["CUOTASPAGADAS"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudData", "ConsultarEstadoCuenta", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene la informcion del Estado de Cuenta
        /// </summary>
        /// <param name="pId">Identificador de registro del tercero </param>
        /// <returns>Entidad ObligacionCredito consultado</returns>
        public string Consultar_Tercero(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            string cod_tercero = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select p.COD_PERSONA 
                                    from OBCREDITO obc
                                    inner join BANCOS b on obc.CODENTIDAD = b.COD_BANCO
                                    left outer join PERSONA p on b.COD_PERSONA = p.COD_PERSONA
                                    where obc.CODOBLIGACION =" + pId;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PERSONA"] != DBNull.Value) cod_tercero = Convert.ToString(resultado["COD_PERSONA"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }

                        return cod_tercero;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudData", "Consultar_Tercero", ex);
                        return null;
                    }
                }
            }
        }

    }
}