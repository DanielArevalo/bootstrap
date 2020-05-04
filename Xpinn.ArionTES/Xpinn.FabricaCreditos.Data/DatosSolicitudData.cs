using System;
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
    /// Objeto de acceso a datos para la tabla Programa
    /// </summary>
    public class DatosSolicitudData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para solicitud
        /// </summary>
        public DatosSolicitudData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea una entidad Solicitud en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Solicitud</param>
        /// <returns>Entidad creada</returns>
        public DatosSolicitud CrearSolicitud(DatosSolicitud pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUMEROSOLICITUD = cmdTransaccionFactory.CreateParameter();
                        pNUMEROSOLICITUD.ParameterName = "pNUMEROSOLICITUD";
                        pNUMEROSOLICITUD.Value = 0;
                        pNUMEROSOLICITUD.Direction = ParameterDirection.InputOutput;

                        DbParameter pFECHASOLICITUD = cmdTransaccionFactory.CreateParameter();
                        pFECHASOLICITUD.ParameterName = "pFECHASOLICITUD";
                        pFECHASOLICITUD.Value = pEntidad.fechasolicitud;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "pCOD_PERSONA";
                        pCOD_PERSONA.Value = pEntidad.cod_cliente;

                        DbParameter pMONTOSOLICITADO = cmdTransaccionFactory.CreateParameter();
                        pMONTOSOLICITADO.ParameterName = "pMONTOSOLICITADO";
                        pMONTOSOLICITADO.Value = pEntidad.montosolicitado;

                        DbParameter pPLAZOSOLICITADO = cmdTransaccionFactory.CreateParameter();
                        pPLAZOSOLICITADO.ParameterName = "pPLAZOSOLICITADO";
                        pPLAZOSOLICITADO.Value = pEntidad.plazosolicitado;

                        DbParameter pCUOTASOLICITADA = cmdTransaccionFactory.CreateParameter();
                        pCUOTASOLICITADA.ParameterName = "pCUOTASOLICITADA";
                        pCUOTASOLICITADA.Value = pEntidad.cuotasolicitada;

                        DbParameter pTIPOCREDITO = cmdTransaccionFactory.CreateParameter();
                        pTIPOCREDITO.ParameterName = "pTIPOCREDITO";
                        pTIPOCREDITO.Value = pEntidad.tipocrdito;

                        DbParameter pPERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        pPERIODICIDAD.ParameterName = "pPERIODICIDAD";
                        pPERIODICIDAD.Value = pEntidad.periodicidad;

                    
                        DbParameter pPrimerPago = cmdTransaccionFactory.CreateParameter();
                        pPrimerPago.ParameterName = "pPrimerPago";
                        if (pEntidad.fecha_primer_pago != null) pPrimerPago.Value = pEntidad.fecha_primer_pago; else pPrimerPago.Value = DBNull.Value;
                        pPrimerPago.Value = pEntidad.fecha_primer_pago;


                        DbParameter pMEDIO = cmdTransaccionFactory.CreateParameter();
                        pMEDIO.ParameterName = "pMEDIO";
                        pMEDIO.Value = pEntidad.medio;

                        DbParameter pOTRO = cmdTransaccionFactory.CreateParameter();
                        pOTRO.ParameterName = "pOTRO";
                        if (pEntidad.otro != null) pOTRO.Value = pEntidad.otro; else pOTRO.Value = DBNull.Value;

                        DbParameter pCONCEPTO = cmdTransaccionFactory.CreateParameter();
                        pCONCEPTO.ParameterName = "pCONCEPTO";
                        pCONCEPTO.Value = pEntidad.concepto;

                        DbParameter pOFICINA = cmdTransaccionFactory.CreateParameter();
                        pOFICINA.ParameterName = "pOFICINA";
                        pOFICINA.Value = Convert.ToString(pEntidad.cod_oficina);

                        DbParameter pUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pUSUARIO.ParameterName = "pUSUARIO";
                        pUSUARIO.Value = Convert.ToString(pEntidad.cod_usuario);

                        DbParameter pGARANTIA = cmdTransaccionFactory.CreateParameter();
                        pGARANTIA.ParameterName = "pGARANTIA";
                        pGARANTIA.Value = Convert.ToString(pEntidad.garantia);

                        DbParameter pGARANTIACOMUNITARIA = cmdTransaccionFactory.CreateParameter();
                        pGARANTIACOMUNITARIA.ParameterName = "pGARANTIACOMUNITARIA";
                        pGARANTIACOMUNITARIA.Value = Convert.ToString(pEntidad.garantia_comunitaria);

                        DbParameter pPOLIZA = cmdTransaccionFactory.CreateParameter();
                        pPOLIZA.ParameterName = "pPOLIZA";
                        pPOLIZA.Value = Convert.ToString(pEntidad.poliza);

                        DbParameter pTIPO_LIQUIDACION = cmdTransaccionFactory.CreateParameter();
                        pTIPO_LIQUIDACION.ParameterName = "pTIPO_LIQUIDACION";
                        pTIPO_LIQUIDACION.DbType = DbType.Int64;
                        pTIPO_LIQUIDACION.Value = pEntidad.tipo_liquidacion;

                        DbParameter pEMPRESA_RECAUDO = cmdTransaccionFactory.CreateParameter();
                        pEMPRESA_RECAUDO.ParameterName = "pEMPRESA_RECAUDO";
                        pEMPRESA_RECAUDO.DbType = DbType.Int64;
                        if (pEntidad.empresa_recaudo == null)
                            pEMPRESA_RECAUDO.Value = DBNull.Value;
                        else
                            pEMPRESA_RECAUDO.Value = pEntidad.empresa_recaudo;

                        DbParameter pID_PROVEEDOR = cmdTransaccionFactory.CreateParameter();
                        pID_PROVEEDOR.ParameterName = "pID_PROVEEDOR";
                        if (pEntidad.identificacionprov == null || pEntidad.identificacionprov == "")
                            pID_PROVEEDOR.Value = DBNull.Value;
                        else
                            pID_PROVEEDOR.Value = pEntidad.identificacionprov;

                        DbParameter pNOM_PROVEEDOR = cmdTransaccionFactory.CreateParameter();
                        pNOM_PROVEEDOR.ParameterName = "pNOM_PROVEEDOR";
                        if (pEntidad.nombreprov == null)
                            pNOM_PROVEEDOR.Value = DBNull.Value;
                        else
                            pNOM_PROVEEDOR.Value = pEntidad.nombreprov;

                        DbParameter pDESTINO = cmdTransaccionFactory.CreateParameter();
                        pDESTINO.ParameterName = "pDESTINO";
                        if (pEntidad.destino == null || pEntidad.destino == 0)
                            pDESTINO.Value = DBNull.Value;
                        else
                            pDESTINO.Value = pEntidad.destino;

                        DbParameter pFORMA_PAGO = cmdTransaccionFactory.CreateParameter();
                        pFORMA_PAGO.ParameterName = "pFORMA_PAGO";
                        pFORMA_PAGO.DbType = DbType.Int64;
                        pFORMA_PAGO.Value = pEntidad.forma_pago;

                        DbParameter pCOD_BANCO = cmdTransaccionFactory.CreateParameter();
                        pCOD_BANCO.ParameterName = "pCOD_BANCO";
                        if (pEntidad.cod_banco == 0)
                            pCOD_BANCO.Value = DBNull.Value;
                        else
                            pCOD_BANCO.Value = pEntidad.cod_banco;

                        DbParameter pNUM_CUENTA = cmdTransaccionFactory.CreateParameter();
                        pNUM_CUENTA.ParameterName = "pNUM_CUENTA";
                        if (string.IsNullOrWhiteSpace(pEntidad.numero_cuenta))
                            pNUM_CUENTA.Value = DBNull.Value;
                        else
                            pNUM_CUENTA.Value = pEntidad.numero_cuenta;

                        DbParameter pTIPOCUENTA = cmdTransaccionFactory.CreateParameter();
                        pTIPOCUENTA.ParameterName = "pTIPOCUENTA";
                        if (string.IsNullOrWhiteSpace(pEntidad.tipo_cuenta))
                            pTIPOCUENTA.Value = DBNull.Value;
                        else
                            pTIPOCUENTA.Value = pEntidad.tipo_cuenta;

                        DbParameter pFORMA_DESEMBOLSO = cmdTransaccionFactory.CreateParameter();
                        pFORMA_DESEMBOLSO.ParameterName = "pFORMA_DESEMBOLSO";
                        if (string.IsNullOrWhiteSpace(pEntidad.forma_desembolso))
                            pFORMA_DESEMBOLSO.Value = DBNull.Value;
                        else
                            pFORMA_DESEMBOLSO.Value = pEntidad.forma_desembolso;

                        DbParameter PREQAFIANCOL = cmdTransaccionFactory.CreateParameter();
                        PREQAFIANCOL.ParameterName = "PREQAFIANCOL";
                        if (string.IsNullOrWhiteSpace(pEntidad.forma_desembolso))
                            PREQAFIANCOL.Value = DBNull.Value;
                        else
                            PREQAFIANCOL.Value = pEntidad.Afiancol;

                        cmdTransaccionFactory.Parameters.Add(pNUMEROSOLICITUD);
                        cmdTransaccionFactory.Parameters.Add(pFECHASOLICITUD);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pMONTOSOLICITADO);
                        cmdTransaccionFactory.Parameters.Add(pPLAZOSOLICITADO);
                        cmdTransaccionFactory.Parameters.Add(pCUOTASOLICITADA);
                        cmdTransaccionFactory.Parameters.Add(pTIPOCREDITO);
                        cmdTransaccionFactory.Parameters.Add(pPERIODICIDAD);
                        cmdTransaccionFactory.Parameters.Add(pMEDIO);
                        cmdTransaccionFactory.Parameters.Add(pOTRO);
                        cmdTransaccionFactory.Parameters.Add(pCONCEPTO);
                        cmdTransaccionFactory.Parameters.Add(pOFICINA);
                        cmdTransaccionFactory.Parameters.Add(pUSUARIO);
                        cmdTransaccionFactory.Parameters.Add(pGARANTIA);
                        cmdTransaccionFactory.Parameters.Add(pGARANTIACOMUNITARIA);
                        cmdTransaccionFactory.Parameters.Add(pPOLIZA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_LIQUIDACION);
                        cmdTransaccionFactory.Parameters.Add(pEMPRESA_RECAUDO);
                        cmdTransaccionFactory.Parameters.Add(pID_PROVEEDOR);
                        cmdTransaccionFactory.Parameters.Add(pNOM_PROVEEDOR);
                        cmdTransaccionFactory.Parameters.Add(pDESTINO);
                        cmdTransaccionFactory.Parameters.Add(pFORMA_PAGO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_BANCO);
                        cmdTransaccionFactory.Parameters.Add(pNUM_CUENTA);
                        cmdTransaccionFactory.Parameters.Add(pTIPOCUENTA);
                        cmdTransaccionFactory.Parameters.Add(pFORMA_DESEMBOLSO);
                        cmdTransaccionFactory.Parameters.Add(pPrimerPago);
                        cmdTransaccionFactory.Parameters.Add(PREQAFIANCOL);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_DATOS_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pEntidad.numerosolicitud = Convert.ToInt64(pNUMEROSOLICITUD.Value);

                        DAauditoria.InsertarLog(pEntidad, "SOLICITUDCRED", pUsuario, Accion.Crear.ToString(), TipoAuditoria.Creditos, "Creacion de solicitud de credito con numero de solicitud " + pEntidad.numerosolicitud); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosData", "CrearSolicitud", ex);
                        return null;
                    }
                }
            }
        }


        public DatosSolicitud ModificarSolicitud(DatosSolicitud pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUMEROSOLICITUD = cmdTransaccionFactory.CreateParameter();
                        pNUMEROSOLICITUD.ParameterName = "pNUMEROSOLICITUD";
                        pNUMEROSOLICITUD.Value = 0;
                        pNUMEROSOLICITUD.Direction = ParameterDirection.InputOutput;

                        DbParameter pFECHASOLICITUD = cmdTransaccionFactory.CreateParameter();
                        pFECHASOLICITUD.ParameterName = "pFECHASOLICITUD";
                        pFECHASOLICITUD.Value = pEntidad.fechasolicitud;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "pCOD_PERSONA";
                        pCOD_PERSONA.Value = pEntidad.cod_cliente;

                        DbParameter pMONTOSOLICITADO = cmdTransaccionFactory.CreateParameter();
                        pMONTOSOLICITADO.ParameterName = "pMONTOSOLICITADO";
                        pMONTOSOLICITADO.Value = pEntidad.montosolicitado;

                        DbParameter pPLAZOSOLICITADO = cmdTransaccionFactory.CreateParameter();
                        pPLAZOSOLICITADO.ParameterName = "pPLAZOSOLICITADO";
                        pPLAZOSOLICITADO.Value = pEntidad.plazosolicitado;

                        DbParameter pCUOTASOLICITADA = cmdTransaccionFactory.CreateParameter();
                        pCUOTASOLICITADA.ParameterName = "pCUOTASOLICITADA";
                        pCUOTASOLICITADA.Value = pEntidad.cuotasolicitada;

                        DbParameter pTIPOCREDITO = cmdTransaccionFactory.CreateParameter();
                        pTIPOCREDITO.ParameterName = "pTIPOCREDITO";
                        pTIPOCREDITO.Value = pEntidad.tipocrdito;

                        DbParameter pPERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        pPERIODICIDAD.ParameterName = "pPERIODICIDAD";
                        pPERIODICIDAD.Value = pEntidad.periodicidad;

                        DbParameter pMEDIO = cmdTransaccionFactory.CreateParameter();
                        pMEDIO.ParameterName = "pMEDIO";
                        pMEDIO.Value = pEntidad.medio;

                        DbParameter pOTRO = cmdTransaccionFactory.CreateParameter();
                        pOTRO.ParameterName = "pOTRO";
                        if (pEntidad.otro != null) pOTRO.Value = pEntidad.otro; else pOTRO.Value = DBNull.Value;

                        DbParameter pCONCEPTO = cmdTransaccionFactory.CreateParameter();
                        pCONCEPTO.ParameterName = "pCONCEPTO";
                        pCONCEPTO.Value = pEntidad.concepto;

                        DbParameter pOFICINA = cmdTransaccionFactory.CreateParameter();
                        pOFICINA.ParameterName = "pOFICINA";
                        pOFICINA.Value = Convert.ToString(pEntidad.cod_oficina);

                        DbParameter pCOD_ASESOR_COM = cmdTransaccionFactory.CreateParameter();
                        pCOD_ASESOR_COM.ParameterName = "pCOD_ASESOR_COM";
                        pCOD_ASESOR_COM.Value = Convert.ToString(pEntidad.cod_asesor_com);

                        DbParameter pUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pUSUARIO.ParameterName = "pUSUARIO";
                        pUSUARIO.Value = Convert.ToString(pEntidad.cod_usuario);

                        DbParameter pGARANTIA = cmdTransaccionFactory.CreateParameter();
                        pGARANTIA.ParameterName = "pGARANTIA";
                        pGARANTIA.Value = Convert.ToString(pEntidad.garantia);

                        DbParameter pGARANTIACOMUNITARIA = cmdTransaccionFactory.CreateParameter();
                        pGARANTIACOMUNITARIA.ParameterName = "pGARANTIACOMUNITARIA";
                        pGARANTIACOMUNITARIA.Value = Convert.ToString(pEntidad.garantia_comunitaria);

                        DbParameter pPOLIZA = cmdTransaccionFactory.CreateParameter();
                        pPOLIZA.ParameterName = "pPOLIZA";
                        pPOLIZA.Value = Convert.ToString(pEntidad.poliza);

                        DbParameter pTIPO_LIQUIDACION = cmdTransaccionFactory.CreateParameter();
                        pTIPO_LIQUIDACION.ParameterName = "pTIPO_LIQUIDACION";
                        pTIPO_LIQUIDACION.DbType = DbType.Int64;
                        pTIPO_LIQUIDACION.Value = pEntidad.tipo_liquidacion;

                        DbParameter pFORMA_PAGO = cmdTransaccionFactory.CreateParameter();
                        pFORMA_PAGO.ParameterName = "pFORMA_PAGO";
                        pFORMA_PAGO.DbType = DbType.Int64;
                        pFORMA_PAGO.Value = pEntidad.forma_pago;

                        DbParameter pEMPRESA_RECAUDO = cmdTransaccionFactory.CreateParameter();
                        pEMPRESA_RECAUDO.ParameterName = "pEMPRESA_RECAUDO";
                        pEMPRESA_RECAUDO.DbType = DbType.Int64;
                        if (pEntidad.empresa_recaudo == null)
                            pEMPRESA_RECAUDO.Value = DBNull.Value;
                        else
                            pEMPRESA_RECAUDO.Value = pEntidad.empresa_recaudo;

                        DbParameter pID_PROVEEDOR = cmdTransaccionFactory.CreateParameter();
                        pID_PROVEEDOR.ParameterName = "pID_PROVEEDOR";
                        if (pEntidad.identificacionprov == null || pEntidad.identificacionprov == "")
                            pID_PROVEEDOR.Value = DBNull.Value;
                        else
                            pID_PROVEEDOR.Value = pEntidad.identificacionprov;

                        DbParameter pNOM_PROVEEDOR = cmdTransaccionFactory.CreateParameter();
                        pNOM_PROVEEDOR.ParameterName = "pNOM_PROVEEDOR";
                        if (pEntidad.nombreprov == null)
                            pNOM_PROVEEDOR.Value = DBNull.Value;
                        else
                            pNOM_PROVEEDOR.Value = pEntidad.nombreprov;

                        cmdTransaccionFactory.Parameters.Add(pNUMEROSOLICITUD);
                        cmdTransaccionFactory.Parameters.Add(pFECHASOLICITUD);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pMONTOSOLICITADO);
                        cmdTransaccionFactory.Parameters.Add(pPLAZOSOLICITADO);
                        cmdTransaccionFactory.Parameters.Add(pCUOTASOLICITADA);
                        cmdTransaccionFactory.Parameters.Add(pTIPOCREDITO);
                        cmdTransaccionFactory.Parameters.Add(pPERIODICIDAD);
                        cmdTransaccionFactory.Parameters.Add(pMEDIO);
                        cmdTransaccionFactory.Parameters.Add(pOTRO);
                        cmdTransaccionFactory.Parameters.Add(pCONCEPTO);
                        cmdTransaccionFactory.Parameters.Add(pOFICINA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_ASESOR_COM);
                        cmdTransaccionFactory.Parameters.Add(pUSUARIO);
                        cmdTransaccionFactory.Parameters.Add(pGARANTIA);
                        cmdTransaccionFactory.Parameters.Add(pGARANTIACOMUNITARIA);
                        cmdTransaccionFactory.Parameters.Add(pPOLIZA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_LIQUIDACION);
                        cmdTransaccionFactory.Parameters.Add(pFORMA_PAGO);
                        cmdTransaccionFactory.Parameters.Add(pEMPRESA_RECAUDO);
                        cmdTransaccionFactory.Parameters.Add(pID_PROVEEDOR);
                        cmdTransaccionFactory.Parameters.Add(pNOM_PROVEEDOR);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_DATOS_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pEntidad, "SOLICITUDCRED", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pEntidad.numerosolicitud = Convert.ToInt64(pNUMEROSOLICITUD.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosData", "ModificarSolicitud", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Crea una entidad Solicitud en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Solicitud</param>
        /// <returns>Entidad creada</returns>
        public DatosSolicitud CrearRadicadoRotativo(DatosSolicitud pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUMEROSOLICITUD = cmdTransaccionFactory.CreateParameter();
                        pNUMEROSOLICITUD.ParameterName = "pNUMEROSOLICITUD";
                        pNUMEROSOLICITUD.Value = pEntidad.numerosolicitud;
                        pNUMEROSOLICITUD.Direction = ParameterDirection.Input;

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "pnumero_radicacion";
                        pnumero_radicacion.Value = pEntidad.numeroradicado;
                        pnumero_radicacion.Direction = ParameterDirection.InputOutput;

                        cmdTransaccionFactory.Parameters.Add(pNUMEROSOLICITUD);
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_LIQUIDAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // if (pUsuario.programaGeneraLog)
                        //   DAauditoria.InsertarLog(pEntidad, "SOLICITUDCRED", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pEntidad.numeroradicado = Convert.ToInt64(pnumero_radicacion.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosData", "CrearRadicadoRotativo", ex);
                        return null;
                    }
                }
            }
        }


        public DatosSolicitud ModificarSolicitudes(DatosSolicitud pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DatosSolicitud solicitudAnterior = ConsultarSolicitudCreditos(pEntidad.numerosolicitud, pUsuario);

                        DbParameter pNUMEROSOLICITUD = cmdTransaccionFactory.CreateParameter();
                        pNUMEROSOLICITUD.ParameterName = "pNUMEROSOLICITUD";
                        pNUMEROSOLICITUD.Value = pEntidad.numerosolicitud;
                        pNUMEROSOLICITUD.Direction = ParameterDirection.Input;

                        DbParameter pFECHASOLICITUD = cmdTransaccionFactory.CreateParameter();
                        pFECHASOLICITUD.ParameterName = "pFECHASOLICITUD";
                        pFECHASOLICITUD.Value = pEntidad.fechasolicitud;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "pCOD_PERSONA";
                        pCOD_PERSONA.Value = pEntidad.cod_persona;

                        DbParameter pMONTOSOLICITADO = cmdTransaccionFactory.CreateParameter();
                        pMONTOSOLICITADO.ParameterName = "pMONTOSOLICITADO";
                        pMONTOSOLICITADO.Value = pEntidad.montosolicitado;

                        DbParameter pPLAZOSOLICITADO = cmdTransaccionFactory.CreateParameter();
                        pPLAZOSOLICITADO.ParameterName = "pPLAZOSOLICITADO";
                        pPLAZOSOLICITADO.Value = pEntidad.plazosolicitado;

                        DbParameter pCUOTASOLICITADA = cmdTransaccionFactory.CreateParameter();
                        pCUOTASOLICITADA.ParameterName = "pCUOTASOLICITADA";
                        pCUOTASOLICITADA.Value = pEntidad.cuotasolicitada;

                        DbParameter pTIPOCREDITO = cmdTransaccionFactory.CreateParameter();
                        pTIPOCREDITO.ParameterName = "pTIPOCREDITO";
                        pTIPOCREDITO.Value = pEntidad.tipocrdito;

                        DbParameter pPERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        pPERIODICIDAD.ParameterName = "pPERIODICIDAD";
                        pPERIODICIDAD.Value = pEntidad.periodicidad;

                        DbParameter pMEDIO = cmdTransaccionFactory.CreateParameter();
                        pMEDIO.ParameterName = "pMEDIO";
                        pMEDIO.Value = pEntidad.medio;

                        DbParameter pOTRO = cmdTransaccionFactory.CreateParameter();
                        pOTRO.ParameterName = "pOTRO";
                        pOTRO.Value = pEntidad.otro;

                        DbParameter pCONCEPTO = cmdTransaccionFactory.CreateParameter();
                        pCONCEPTO.ParameterName = "pCONCEPTO";
                        pCONCEPTO.Value = pEntidad.concepto;

                        DbParameter pOFICINA = cmdTransaccionFactory.CreateParameter();
                        pOFICINA.ParameterName = "pOFICINA";
                        pOFICINA.Value = Convert.ToString(pEntidad.cod_oficina);

                        DbParameter pCOD_ASESOR_COM = cmdTransaccionFactory.CreateParameter();
                        pCOD_ASESOR_COM.ParameterName = "pCOD_ASESOR_COM";
                        pCOD_ASESOR_COM.Value = Convert.ToString(pEntidad.cod_asesor_com);

                        DbParameter pUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pUSUARIO.ParameterName = "pUSUARIO";
                        pUSUARIO.Value = Convert.ToString(pEntidad.cod_usuario);

                        DbParameter pGARANTIA = cmdTransaccionFactory.CreateParameter();
                        pGARANTIA.ParameterName = "pGARANTIA";
                        pGARANTIA.Value = Convert.ToString(pEntidad.garantia);

                        DbParameter pGARANTIACOMUNITARIA = cmdTransaccionFactory.CreateParameter();
                        pGARANTIACOMUNITARIA.ParameterName = "pGARANTIACOMUNITARIA";
                        pGARANTIACOMUNITARIA.Value = Convert.ToString(pEntidad.garantia_comunitaria);

                        DbParameter pPOLIZA = cmdTransaccionFactory.CreateParameter();
                        pPOLIZA.ParameterName = "pPOLIZA";
                        pPOLIZA.Value = Convert.ToString(pEntidad.poliza);

                        DbParameter pTIPO_LIQUIDACION = cmdTransaccionFactory.CreateParameter();
                        pTIPO_LIQUIDACION.ParameterName = "pTIPO_LIQUIDACION";
                        pTIPO_LIQUIDACION.DbType = DbType.Int64;
                        pTIPO_LIQUIDACION.Value = pEntidad.tipo_liquidacion;

                        DbParameter pFORMA_PAGO = cmdTransaccionFactory.CreateParameter();
                        pFORMA_PAGO.ParameterName = "pFORMA_PAGO";
                        pFORMA_PAGO.DbType = DbType.Int64;
                        pFORMA_PAGO.Value = pEntidad.forma_pago;

                        DbParameter pEMPRESA_RECAUDO = cmdTransaccionFactory.CreateParameter();
                        pEMPRESA_RECAUDO.ParameterName = "pEMPRESA_RECAUDO";
                        if (pEntidad.empresa_recaudo == null)
                            pEMPRESA_RECAUDO.Value = DBNull.Value;
                        else
                            pEMPRESA_RECAUDO.Value = pEntidad.empresa_recaudo;

                        DbParameter pID_PROVEEDOR = cmdTransaccionFactory.CreateParameter();
                        pID_PROVEEDOR.ParameterName = "pID_PROVEEDOR";
                        if (pEntidad.identificacionprov == null)
                            pID_PROVEEDOR.Value = DBNull.Value;
                        else
                            pID_PROVEEDOR.Value = pEntidad.identificacionprov;

                        DbParameter pNOM_PROVEEDOR = cmdTransaccionFactory.CreateParameter();
                        pNOM_PROVEEDOR.ParameterName = "pNOM_PROVEEDOR";
                        if (pEntidad.nombreprov == null)
                            pNOM_PROVEEDOR.Value = DBNull.Value;
                        else
                            pNOM_PROVEEDOR.Value = pEntidad.nombreprov;

                        cmdTransaccionFactory.Parameters.Add(pNUMEROSOLICITUD);
                        cmdTransaccionFactory.Parameters.Add(pFECHASOLICITUD);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pMONTOSOLICITADO);
                        cmdTransaccionFactory.Parameters.Add(pPLAZOSOLICITADO);
                        cmdTransaccionFactory.Parameters.Add(pCUOTASOLICITADA);
                        cmdTransaccionFactory.Parameters.Add(pTIPOCREDITO);
                        cmdTransaccionFactory.Parameters.Add(pPERIODICIDAD);
                        cmdTransaccionFactory.Parameters.Add(pMEDIO);
                        cmdTransaccionFactory.Parameters.Add(pOTRO);
                        cmdTransaccionFactory.Parameters.Add(pCONCEPTO);
                        cmdTransaccionFactory.Parameters.Add(pOFICINA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_ASESOR_COM);
                        cmdTransaccionFactory.Parameters.Add(pUSUARIO);
                        cmdTransaccionFactory.Parameters.Add(pGARANTIA);
                        cmdTransaccionFactory.Parameters.Add(pGARANTIACOMUNITARIA);
                        cmdTransaccionFactory.Parameters.Add(pPOLIZA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_LIQUIDACION);
                        cmdTransaccionFactory.Parameters.Add(pFORMA_PAGO);
                        cmdTransaccionFactory.Parameters.Add(pEMPRESA_RECAUDO);
                        cmdTransaccionFactory.Parameters.Add(pID_PROVEEDOR);
                        cmdTransaccionFactory.Parameters.Add(pNOM_PROVEEDOR);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_DATOS_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        DAauditoria.InsertarLog(pEntidad, "SOLICITUDCRED", pUsuario, Accion.Modificar.ToString(), TipoAuditoria.Creditos, "Modificacion de solicitud de credito con numero de radicacion " + pEntidad.numerosolicitud, solicitudAnterior); //REGISTRO DE AUDITORIA

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosData", "ModificarSolicitud", ex);
                        return null;
                    }
                }
            }
        }


        public DatosSolicitud ModificarSolicitudRotativo(DatosSolicitud pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUMEROSOLICITUD = cmdTransaccionFactory.CreateParameter();
                        pNUMEROSOLICITUD.ParameterName = "pNUMEROSOLICITUD";
                        pNUMEROSOLICITUD.Value = pEntidad.numerosolicitud;
                        pNUMEROSOLICITUD.Direction = ParameterDirection.Input;

                        DbParameter pFECHASOLICITUD = cmdTransaccionFactory.CreateParameter();
                        pFECHASOLICITUD.ParameterName = "pFECHASOLICITUD";
                        pFECHASOLICITUD.Value = pEntidad.fechasolicitud;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "pCOD_PERSONA";
                        pCOD_PERSONA.Value = pEntidad.cod_cliente;

                        DbParameter pMONTOSOLICITADO = cmdTransaccionFactory.CreateParameter();
                        pMONTOSOLICITADO.ParameterName = "pMONTOSOLICITADO";
                        pMONTOSOLICITADO.Value = pEntidad.montosolicitado;

                        DbParameter pPLAZOSOLICITADO = cmdTransaccionFactory.CreateParameter();
                        pPLAZOSOLICITADO.ParameterName = "pPLAZOSOLICITADO";
                        pPLAZOSOLICITADO.Value = pEntidad.plazosolicitado;

                        DbParameter pCUOTASOLICITADA = cmdTransaccionFactory.CreateParameter();
                        pCUOTASOLICITADA.ParameterName = "pCUOTASOLICITADA";
                        pCUOTASOLICITADA.Value = pEntidad.cuotasolicitada;

                        DbParameter pTIPOCREDITO = cmdTransaccionFactory.CreateParameter();
                        pTIPOCREDITO.ParameterName = "pTIPOCREDITO";
                        pTIPOCREDITO.Value = pEntidad.tipocrdito;

                        DbParameter pPERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        pPERIODICIDAD.ParameterName = "pPERIODICIDAD";
                        pPERIODICIDAD.Value = pEntidad.periodicidad;

                        DbParameter pMEDIO = cmdTransaccionFactory.CreateParameter();
                        pMEDIO.ParameterName = "pMEDIO";
                        pMEDIO.Value = pEntidad.medio;

                        DbParameter pOTRO = cmdTransaccionFactory.CreateParameter();
                        pOTRO.ParameterName = "pOTRO";
                        pOTRO.Value = pEntidad.otro;

                        DbParameter pCONCEPTO = cmdTransaccionFactory.CreateParameter();
                        pCONCEPTO.ParameterName = "pCONCEPTO";
                        pCONCEPTO.Value = pEntidad.concepto;

                        DbParameter pOFICINA = cmdTransaccionFactory.CreateParameter();
                        pOFICINA.ParameterName = "pOFICINA";
                        pOFICINA.Value = Convert.ToString(pEntidad.cod_oficina);

                        DbParameter pCOD_ASESOR_COM = cmdTransaccionFactory.CreateParameter();
                        pCOD_ASESOR_COM.ParameterName = "pCOD_ASESOR_COM";
                        pCOD_ASESOR_COM.Value = Convert.ToString(pEntidad.cod_asesor_com);

                        DbParameter pUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pUSUARIO.ParameterName = "pUSUARIO";
                        pUSUARIO.Value = Convert.ToString(pEntidad.cod_usuario);

                        DbParameter pGARANTIA = cmdTransaccionFactory.CreateParameter();
                        pGARANTIA.ParameterName = "pGARANTIA";
                        pGARANTIA.Value = Convert.ToString(pEntidad.garantia);

                        DbParameter pGARANTIACOMUNITARIA = cmdTransaccionFactory.CreateParameter();
                        pGARANTIACOMUNITARIA.ParameterName = "pGARANTIACOMUNITARIA";
                        pGARANTIACOMUNITARIA.Value = Convert.ToString(pEntidad.garantia_comunitaria);

                        DbParameter pPOLIZA = cmdTransaccionFactory.CreateParameter();
                        pPOLIZA.ParameterName = "pPOLIZA";
                        pPOLIZA.Value = Convert.ToString(pEntidad.poliza);

                        DbParameter pTIPO_LIQUIDACION = cmdTransaccionFactory.CreateParameter();
                        pTIPO_LIQUIDACION.ParameterName = "pTIPO_LIQUIDACION";
                        pTIPO_LIQUIDACION.DbType = DbType.Int64;
                        pTIPO_LIQUIDACION.Value = pEntidad.tipo_liquidacion;

                        DbParameter pFORMA_PAGO = cmdTransaccionFactory.CreateParameter();
                        pFORMA_PAGO.ParameterName = "pFORMA_PAGO";
                        pFORMA_PAGO.DbType = DbType.Int64;
                        pFORMA_PAGO.Value = pEntidad.forma_pago;

                        DbParameter pEMPRESA_RECAUDO = cmdTransaccionFactory.CreateParameter();
                        pEMPRESA_RECAUDO.ParameterName = "pEMPRESA_RECAUDO";
                        if (pEntidad.empresa_recaudo == null)
                            pEMPRESA_RECAUDO.Value = DBNull.Value;
                        else
                            pEMPRESA_RECAUDO.Value = pEntidad.empresa_recaudo;
                        pEMPRESA_RECAUDO.Value = pEntidad.empresa_recaudo;


                        DbParameter PID_PROVEEDOR = cmdTransaccionFactory.CreateParameter();
                        PID_PROVEEDOR.ParameterName = "PID_PROVEEDOR";
                        if (pEntidad.identificacionprov == null)
                            PID_PROVEEDOR.Value = DBNull.Value;
                        else
                            PID_PROVEEDOR.Value = pEntidad.identificacionprov;


                        DbParameter PNOM_PROVEEDOR = cmdTransaccionFactory.CreateParameter();
                        PNOM_PROVEEDOR.ParameterName = "PNOM_PROVEEDOR";
                        if (pEntidad.nombreprov == null)
                            PNOM_PROVEEDOR.Value = DBNull.Value;
                        else
                            PNOM_PROVEEDOR.Value = pEntidad.nombreprov;

                        cmdTransaccionFactory.Parameters.Add(pNUMEROSOLICITUD);
                        cmdTransaccionFactory.Parameters.Add(pFECHASOLICITUD);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pMONTOSOLICITADO);
                        cmdTransaccionFactory.Parameters.Add(pPLAZOSOLICITADO);
                        cmdTransaccionFactory.Parameters.Add(pCUOTASOLICITADA);
                        cmdTransaccionFactory.Parameters.Add(pTIPOCREDITO);
                        cmdTransaccionFactory.Parameters.Add(pPERIODICIDAD);
                        cmdTransaccionFactory.Parameters.Add(pMEDIO);
                        cmdTransaccionFactory.Parameters.Add(pOTRO);
                        cmdTransaccionFactory.Parameters.Add(pCONCEPTO);
                        cmdTransaccionFactory.Parameters.Add(pOFICINA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_ASESOR_COM);
                        cmdTransaccionFactory.Parameters.Add(pUSUARIO);
                        cmdTransaccionFactory.Parameters.Add(pGARANTIA);
                        cmdTransaccionFactory.Parameters.Add(pGARANTIACOMUNITARIA);
                        cmdTransaccionFactory.Parameters.Add(pPOLIZA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_LIQUIDACION);
                        cmdTransaccionFactory.Parameters.Add(pFORMA_PAGO);
                        cmdTransaccionFactory.Parameters.Add(pEMPRESA_RECAUDO);
                        cmdTransaccionFactory.Parameters.Add(PID_PROVEEDOR);
                        cmdTransaccionFactory.Parameters.Add(PNOM_PROVEEDOR);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_DATOS_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pEntidad, "SOLICITUDCRED", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return null;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosData", "ModificarSolicitud", ex);
                        return null;
                    }
                }
            }
        }


        public DatosSolicitud ModificarConceptoSolicitud(DatosSolicitud pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUMEROSOLICITUD = cmdTransaccionFactory.CreateParameter();
                        pNUMEROSOLICITUD.ParameterName = "pNUMEROSOLICITUD";
                        pNUMEROSOLICITUD.Value = 0;
                        pNUMEROSOLICITUD.Direction = ParameterDirection.Input;

                        DbParameter pCONCEPTO = cmdTransaccionFactory.CreateParameter();
                        pCONCEPTO.ParameterName = "pCONCEPTO";
                        pCONCEPTO.Value = pEntidad.concepto;

                        DbParameter pUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pUSUARIO.ParameterName = "pUSUARIO";
                        pUSUARIO.Value = Convert.ToString(pEntidad.cod_usuario);

                        cmdTransaccionFactory.Parameters.Add(pNUMEROSOLICITUD);
                        cmdTransaccionFactory.Parameters.Add(pCONCEPTO);
                        cmdTransaccionFactory.Parameters.Add(pUSUARIO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_CONC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pEntidad, "SOLICITUDCRED", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return null;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosData", "CrearSolicitud", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un Solicitud en la base de datos
        /// </summary>
        /// <param name="pId">Identificador del Solicitud</param>
        public void EliminarSolicitud(Int64 pId, Usuario pUsuario)
        {
            //using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            //{
            //    using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
            //    {
            //        try
            //        {
            //            DatosSolicitud pEntidad = new DatosSolicitud();

            //            if (pUsuario.programaGeneraLog)
            //                pEntidad = ConsultarSolicitud(pId, pUsuario); //REGISTRO DE AUDITORIA

            //            DbParameter pIdSolicitud = cmdTransaccionFactory.CreateParameter();
            //            pIdSolicitud.ParameterName = "pIdc";
            //            pIdSolicitud.Value = pId;

            //            cmdTransaccionFactory.Parameters.Add(pIdCliente);

            //            connection.Open();
            //            cmdTransaccionFactory.Connection = connection;
            //            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
            //            cmdTransaccionFactory.CommandText = "usp_xpinn_pre_DatosClie_Elimi";
            //            cmdTransaccionFactory.ExecuteNonQuery();

            //            if (pUsuario.programaGeneraLog)
            //                DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
            //        }
            //        catch (Exception ex)
            //        {
            //            BOExcepcion.Throw("DatosSolicitudData", "ElimiCliente", ex);
            //        }
            //    }
            //}
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla EgresosFamilia dados unos filtros
        /// </summary>
        /// <param name="pEgresosFamilia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de EgresosFamilia obtenidos</returns>
        public DatosSolicitud ListarDatosSolicitud(DatosSolicitud pDatosSolicitud, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  SOLICITUDCRED where NUMEROSOLICITUD = " + pDatosSolicitud.numerosolicitud;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        DatosSolicitud entidad = new DatosSolicitud();

                        while (resultado.Read())
                        {
                            if (resultado["NUMEROSOLICITUD"] != DBNull.Value) entidad.numerosolicitud = Convert.ToInt64(resultado["NUMEROSOLICITUD"]);
                            if (resultado["FECHASOLICITUD"] != DBNull.Value) entidad.fechasolicitud = Convert.ToDateTime(resultado["FECHASOLICITUD"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["MONTOSOLICITADO"] != DBNull.Value) entidad.montosolicitado = Convert.ToInt64(resultado["MONTOSOLICITADO"]);
                            if (resultado["PLAZOSOLICITADO"] != DBNull.Value) entidad.plazosolicitado = Convert.ToInt64(resultado["PLAZOSOLICITADO"]);
                            if (resultado["CUOTASOLICITADA"] != DBNull.Value) entidad.cuotasolicitada = Convert.ToInt64(resultado["CUOTASOLICITADA"]);
                            if (resultado["TIPOCREDITO"] != DBNull.Value) entidad.tipocrdito = Convert.ToString(resultado["TIPOCREDITO"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToInt64(resultado["PERIODICIDAD"]);
                            if (resultado["MEDIO"] != DBNull.Value) entidad.medio = Convert.ToInt64(resultado["MEDIO"]);
                            if (resultado["concepto"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["concepto"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt64(resultado["FORMA_PAGO"]);
                            if (resultado["EMPRESA_RECAUDO"] != DBNull.Value) entidad.empresa_recaudo = Convert.ToInt64(resultado["EMPRESA_RECAUDO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosSolicitudData", "ListarDatosSolicitud", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene una lista de Entidades de la tabla EgresosFamilia dados unos filtros
        /// </summary>
        /// <param name="pEgresosFamilia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de EgresosFamilia obtenidos</returns>
        public List<DatosSolicitud> ListarLineasCredito(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DatosSolicitud> lstConsulta = new List<DatosSolicitud>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select *  From  clasificacion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DatosSolicitud entidad = new DatosSolicitud();
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt64(resultado["COD_CLASIFICA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);
                            lstConsulta.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosSolicitudData", "ListarLineasCredito", ex);
                        return null;
                    }
                }
            }
        }




        /// <summary>
        /// Obtiene una lista de Entidades de la tabla EgresosFamilia dados unos filtros
        /// </summary>
        /// <param name="pEgresosFamilia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de EgresosFamilia obtenidos</returns>
        public DatosSolicitud ListarSolicitudCrtlTiempos(DatosSolicitud pDatosSolicitud, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select cr.*, lc.nombre,u.nombre AS ASESOR FROM  credito cr, lineascredito lc,usuarios u  where  u.codusuario=cr.cod_asesor_com and lc.cod_linea_credito = cr.cod_linea_credito and cr.numero_radicacion = " + pDatosSolicitud.numerosolicitud;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        DatosSolicitud entidad = new DatosSolicitud();

                        while (resultado.Read())
                        {
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.montosolicitado = Convert.ToInt64(resultado["MONTO_SOLICITADO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numerocuotas = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valorcuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fechasolicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.cod_asesor_com = Convert.ToInt64(resultado["COD_ASESOR_COM"]);
                            if (resultado["ASESOR"] != DBNull.Value) entidad.asesor = Convert.ToString(resultado["ASESOR"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosSolicitudData", "ListarDatosSolicitud", ex);
                        return null;
                    }
                }
            }
        }


        public DatosSolicitud ValidarSolicitud(DatosSolicitud pEntidad, Usuario pUsuario, ref string sError)
        {
            sError = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pFECHASOLICITUD = cmdTransaccionFactory.CreateParameter();
                        pFECHASOLICITUD.ParameterName = "pFECHASOLICITUD";
                        pFECHASOLICITUD.Value = pEntidad.fechasolicitud;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "pCOD_PERSONA";
                        pCOD_PERSONA.Value = pEntidad.cod_persona;

                        DbParameter pMONTOSOLICITADO = cmdTransaccionFactory.CreateParameter();
                        pMONTOSOLICITADO.ParameterName = "pMONTOSOLICITADO";
                        pMONTOSOLICITADO.Value = pEntidad.montosolicitado;

                        DbParameter pPLAZOSOLICITADO = cmdTransaccionFactory.CreateParameter();
                        pPLAZOSOLICITADO.ParameterName = "pPLAZOSOLICITADO";
                        pPLAZOSOLICITADO.Value = pEntidad.plazosolicitado;

                        DbParameter pTIPOCREDITO = cmdTransaccionFactory.CreateParameter();
                        pTIPOCREDITO.ParameterName = "pTIPOCREDITO";
                        pTIPOCREDITO.Value = pEntidad.tipocrdito;

                        DbParameter pPERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        pPERIODICIDAD.ParameterName = "pPERIODICIDAD";
                        pPERIODICIDAD.Value = pEntidad.periodicidad;

                        DbParameter pFORMA_PAGO = cmdTransaccionFactory.CreateParameter();
                        pFORMA_PAGO.ParameterName = "pFORMA_PAGO";
                        pFORMA_PAGO.Value = Convert.ToString(pEntidad.forma_pago);

                        DbParameter pCOD_ASESOR_COM = cmdTransaccionFactory.CreateParameter();
                        pCOD_ASESOR_COM.ParameterName = "pCOD_ASESOR_COM";
                        pCOD_ASESOR_COM.Value = Convert.ToString(pEntidad.cod_asesor_com);

                        DbParameter pUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pUSUARIO.ParameterName = "pUSUARIO";
                        pUSUARIO.Value = Convert.ToString(pEntidad.cod_usuario);

                        DbParameter pOFICINA = cmdTransaccionFactory.CreateParameter();
                        pOFICINA.ParameterName = "pOFICINA";
                        pOFICINA.Value = Convert.ToString(pEntidad.cod_oficina);

                        DbParameter pEMPRESARECAUDO = cmdTransaccionFactory.CreateParameter();
                        pEMPRESARECAUDO.ParameterName = "pEMPRESARECAUDO";
                        if (pEntidad.empresa_recaudo == null)
                            pEMPRESARECAUDO.Value = DBNull.Value;
                        else
                            pEMPRESARECAUDO.Value = Convert.ToString(pEntidad.empresa_recaudo);

                        DbParameter pMENSAJE = cmdTransaccionFactory.CreateParameter();
                        pMENSAJE.ParameterName = "pMENSAJE";
                        pMENSAJE.DbType = DbType.AnsiStringFixedLength;
                        pMENSAJE.Size = 200;
                        pMENSAJE.Direction = ParameterDirection.Output;
                        pMENSAJE.Value = pEntidad.mensaje;

                        cmdTransaccionFactory.Parameters.Add(pFECHASOLICITUD);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pMONTOSOLICITADO);
                        cmdTransaccionFactory.Parameters.Add(pPLAZOSOLICITADO);
                        cmdTransaccionFactory.Parameters.Add(pTIPOCREDITO);
                        cmdTransaccionFactory.Parameters.Add(pPERIODICIDAD);
                        cmdTransaccionFactory.Parameters.Add(pFORMA_PAGO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_ASESOR_COM);
                        cmdTransaccionFactory.Parameters.Add(pUSUARIO);
                        cmdTransaccionFactory.Parameters.Add(pOFICINA);
                        cmdTransaccionFactory.Parameters.Add(pEMPRESARECAUDO);
                        cmdTransaccionFactory.Parameters.Add(pMENSAJE);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_VALIDAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pEntidad.mensaje = pMENSAJE.Value.ToString();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;

                    }
                    catch (Exception ex)
                    {
                        sError = ex.Message;
                        return null;
                    }
                }
            }
        }

        public DatosSolicitud ConsultarCliente(String pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            DatosSolicitud entidad = new DatosSolicitud();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT cod_persona, identificacion,tipo_identificacion,PRIMER_NOMBRE  || ' ' || SEGUNDO_nombre  || ' ' || primer_apellido  || ' ' || segundo_apellido as nombre FROM PERSONA WHERE IDENTIFICACION = " + "'" + pId.ToString() + "'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            //if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                        }
                        //  else
                        //   {
                        //  throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        //  }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosSolicitudData", "ConsultarAporte", ex);
                        return null;
                    }
                }
            }
        }
        public DatosSolicitud ConsultarParametroEdadMinima(Usuario vUsuario)
        {
            DbDataReader resultado;
            DatosSolicitud entidad = new DatosSolicitud();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select * from general where codigo=5";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {

                            if (resultado["VALOR"] != DBNull.Value) entidad.edadminima = Convert.ToInt64(resultado["VALOR"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosSolicitudData", "ConsultarParametroEdadMinima", ex);
                        return null;
                    }
                }
            }
        }
        public DatosSolicitud ConsultarParametroEdadMaxima(Usuario vUsuario)
        {
            DbDataReader resultado;
            DatosSolicitud entidad = new DatosSolicitud();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select * from general where codigo=6";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {

                            if (resultado["VALOR"] != DBNull.Value) entidad.edadmaxima = Convert.ToInt64(resultado["VALOR"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosSolicitudData", "ConsultarParametroEdadMinima", ex);
                        return null;
                    }
                }
            }
        }


        public DatosSolicitud ValidarSolicitudRotativo(DatosSolicitud pEntidad, Usuario pUsuario, ref string sError)
        {
            sError = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pFECHASOLICITUD = cmdTransaccionFactory.CreateParameter();
                        pFECHASOLICITUD.ParameterName = "pfechasolicitud";
                        pFECHASOLICITUD.Value = pEntidad.fechasolicitud;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "pcod_persona";
                        pCOD_PERSONA.Value = pEntidad.cod_cliente;

                        DbParameter pIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pIDENTIFICACION.ParameterName = "pidentificacion";
                        pIDENTIFICACION.Value = pEntidad.identificacion;

                        DbParameter pCOD_LINEA_CREDITO = cmdTransaccionFactory.CreateParameter();
                        pCOD_LINEA_CREDITO.ParameterName = "pcod_linea_credito";
                        pCOD_LINEA_CREDITO.Value = pEntidad.linea;

                        DbParameter pMENSAJE = cmdTransaccionFactory.CreateParameter();
                        pMENSAJE.ParameterName = "pmensaje";
                        pMENSAJE.Size = 200;
                        pMENSAJE.Direction = ParameterDirection.Output;
                        pMENSAJE.Value = pEntidad.mensaje;

                        cmdTransaccionFactory.Parameters.Add(pFECHASOLICITUD);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pIDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pCOD_LINEA_CREDITO);
                        cmdTransaccionFactory.Parameters.Add(pMENSAJE);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_VALIDACLI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pEntidad.mensaje = pMENSAJE.Value.ToString();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;

                    }
                    catch (Exception ex)
                    {
                        sError = ex.Message;
                        return null;
                    }
                }
            }
        }


        public DatosSolicitud ValidarCliente(DatosSolicitud pEntidad, Usuario pUsuario, ref string sError)
        {
            sError = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pFECHASOLICITUD = cmdTransaccionFactory.CreateParameter();
                        pFECHASOLICITUD.ParameterName = "pfechasolicitud";
                        pFECHASOLICITUD.DbType = DbType.Date;
                        pFECHASOLICITUD.Value = pEntidad.fechasolicitud;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "pcod_persona";
                        pCOD_PERSONA.Direction = ParameterDirection.InputOutput;
                        pCOD_PERSONA.Value = pEntidad.cod_persona;

                        DbParameter pIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pIDENTIFICACION.ParameterName = "pidentificacion";
                        pIDENTIFICACION.Value = pEntidad.identificacion;

                        DbParameter pMENSAJE = cmdTransaccionFactory.CreateParameter();
                        pMENSAJE.ParameterName = "pmensaje";
                        pMENSAJE.Size = 200;
                        pMENSAJE.Direction = ParameterDirection.Output;
                        pMENSAJE.Value = pEntidad.mensaje;

                        cmdTransaccionFactory.Parameters.Add(pFECHASOLICITUD);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pIDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pMENSAJE);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_VALIDACLI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pEntidad.cod_persona == 0)
                            pEntidad.cod_persona = Convert.ToInt64(pCOD_PERSONA.Value.ToString());

                        pEntidad.mensaje = pMENSAJE.Value.ToString();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;

                    }
                    catch (Exception ex)
                    {
                        sError = ex.Message;
                        return pEntidad;
                    }
                }
            }
        }


        public DatosSolicitud ConsultarSolicitudCreditos(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            DatosSolicitud entidad = new DatosSolicitud();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT S.*,O.NOMBRE AS NOMOFICINA, C.NUMERO_RADICACION,V.IDENTIFICACION,V.NOMBRE FROM SOLICITUDCRED S LEFT JOIN OFICINA O ON O.COD_OFICINA = S.OFICINA "
                        + "LEFT JOIN CREDITO C ON S.NUMEROSOLICITUD = C.NUMERO_OBLIGACION LEFT JOIN V_PERSONA V ON V.COD_PERSONA = S.COD_PERSONA WHERE S.NUMEROSOLICITUD = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMEROSOLICITUD"] != DBNull.Value) entidad.numerosolicitud = Convert.ToInt32(resultado["NUMEROSOLICITUD"]);
                            if (resultado["FECHASOLICITUD"] != DBNull.Value) entidad.fechasolicitud = Convert.ToDateTime(resultado["FECHASOLICITUD"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt32(resultado["COD_PERSONA"]);
                            if (resultado["MONTOSOLICITADO"] != DBNull.Value) entidad.montosolicitado = Convert.ToInt64(resultado["MONTOSOLICITADO"]);
                            if (resultado["PLAZOSOLICITADO"] != DBNull.Value) entidad.plazosolicitado = Convert.ToInt32(resultado["PLAZOSOLICITADO"]);
                            if (resultado["CUOTASOLICITADA"] != DBNull.Value) entidad.cuotasolicitada = Convert.ToInt64(resultado["CUOTASOLICITADA"]);
                            if (resultado["TIPOCREDITO"] != DBNull.Value) entidad.tipocrdito = Convert.ToString(resultado["TIPOCREDITO"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToInt32(resultado["PERIODICIDAD"]);
                            if (resultado["MEDIO"] != DBNull.Value) entidad.medio = Convert.ToInt32(resultado["MEDIO"]);
                            if (resultado["REQPOLIZA"] != DBNull.Value) entidad.poliza = Convert.ToInt64(resultado["REQPOLIZA"]);
                            if (resultado["OTROMEDIO"] != DBNull.Value) entidad.otro = Convert.ToString(resultado["OTROMEDIO"]);
                            if (resultado["USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt64(resultado["USUARIO"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["OFICINA"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["CONCEPTO"]);
                            if (resultado["GARANTIA"] != DBNull.Value) entidad.garantia = Convert.ToInt32(resultado["GARANTIA"]);
                            if (resultado["GARANTIA_COMUNITARIA"] != DBNull.Value) entidad.garantia_comunitaria = Convert.ToInt32(resultado["GARANTIA_COMUNITARIA"]);
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt32(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (resultado["EMPRESA_RECAUDO"] != DBNull.Value) entidad.empresa_recaudo = Convert.ToInt32(resultado["EMPRESA_RECAUDO"]);
                            if (resultado["IDPROVEEDOR"] != DBNull.Value) entidad.identificacionprov = Convert.ToString(resultado["IDPROVEEDOR"]);
                            if (resultado["NOMPROVEEDOR"] != DBNull.Value) entidad.nombreprov = Convert.ToString(resultado["NOMPROVEEDOR"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOMOFICINA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numeroradicado = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudCreditosData", "ConsultarSolicitudCreditos", ex);
                        return null;
                    }
                }
            }
        }

        public int ConsultarCreditosActivosXLinea(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            int numeroCreditoActivoPersona = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT COUNT(*) As Total from CREDITO " + pFiltro.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["Total"] != DBNull.Value) numeroCreditoActivoPersona = Convert.ToInt32(resultado["Total"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return numeroCreditoActivoPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosSolicitudData", "ConsultarCreditosActivosXLinea", ex);
                        return 0;
                    }
                }
            }
        }

        public int? ConsultarCreditosPermitidosXLinea(string cod_linea, Usuario vUsuario)
        {
            DbDataReader resultado;
            int? numeroCreditoActivoPersona = null;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT CREDITO_X_LINEA AS NUMEROLINEA from LINEASCREDITO Where COD_LINEA_CREDITO = '" + cod_linea + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMEROLINEA"] != DBNull.Value) numeroCreditoActivoPersona = Convert.ToInt32(resultado["NUMEROLINEA"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return numeroCreditoActivoPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosSolicitudData", "ConsultarCreditosPermitidosXLinea", ex);
                        return null;
                    }
                }
            }
        }


        public string ConsultaValorGarantia(string num_radica, Usuario vUsuario)
        {
            DbDataReader resultado;
            string valorgarantia = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select valor from DESCUENTOSCREDITO where NUMERO_RADICACION = '" + num_radica + "' and COD_ATR = 14";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) valorgarantia = Convert.ToString(resultado["valor"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return valorgarantia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosSolicitudData", "ConsultaValorGarantia", ex);
                        return null;
                    }
                }
            }
        }


    }
}


