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
    /// Objeto de acceso a datos para la tabla SolicitudCreditosRecogidos
    /// </summary>
    public class SolicitudCreditosRecogidosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla SolicitudCreditosRecogidos
        /// </summary>
        public SolicitudCreditosRecogidosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla SolicitudCreditosRecogidos de la base de datos
        /// </summary>
        /// <param name="pSolicitudCreditosRecogidos">Entidad SolicitudCreditosRecogidos</param>
        /// <returns>Entidad SolicitudCreditosRecogidos creada</returns>
        public SolicitudRecogidoAvance CrearSolicitudCreditosRecogidos(SolicitudRecogidoAvance pSolicitudCreditosRecogidos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                       

                        DbParameter pNUMEROSOLICITUD = cmdTransaccionFactory.CreateParameter();
                        pNUMEROSOLICITUD.ParameterName = "p_NUMERO_RECOGE";
                        pNUMEROSOLICITUD.Value = pSolicitudCreditosRecogidos.Radicado;
                                             

                        DbParameter pVALORRECOGE = cmdTransaccionFactory.CreateParameter();
                        pVALORRECOGE.ParameterName = "p_VALORRECOGE";
                        pVALORRECOGE.Value = pSolicitudCreditosRecogidos.ValorTotal;

                        DbParameter pFECHAPAGO = cmdTransaccionFactory.CreateParameter();
                        pFECHAPAGO.ParameterName = "p_FECHAPAGO";
                        pFECHAPAGO.Value = pSolicitudCreditosRecogidos.FechaDesembolsi;

                        DbParameter pSALDOCAPITAL = cmdTransaccionFactory.CreateParameter();
                        pSALDOCAPITAL.ParameterName = "p_SALDOCAPITAL";
                        pSALDOCAPITAL.Value = pSolicitudCreditosRecogidos.SaldoAvance;

                        DbParameter pSALDOINTCORR = cmdTransaccionFactory.CreateParameter();
                        pSALDOINTCORR.ParameterName = "p_SALDOINTCORR";
                        pSALDOINTCORR.Value = pSolicitudCreditosRecogidos.Intereses;

                        DbParameter pAvance = cmdTransaccionFactory.CreateParameter();
                        pAvance.ParameterName = "p_avance";
                        pAvance.Value = pSolicitudCreditosRecogidos.NumAvance;



                        cmdTransaccionFactory.Parameters.Add(pNUMEROSOLICITUD);                       
                        cmdTransaccionFactory.Parameters.Add(pVALORRECOGE);
                        cmdTransaccionFactory.Parameters.Add(pFECHAPAGO);
                        cmdTransaccionFactory.Parameters.Add(pSALDOCAPITAL);
                        cmdTransaccionFactory.Parameters.Add(pSALDOINTCORR);
                        cmdTransaccionFactory.Parameters.Add(pAvance);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_AVAN_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pSolicitudCreditosRecogidos, "SolicitudCreditosRecogidosAvance",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                     
                        dbConnectionFactory.CerrarConexion(connection);
                        return pSolicitudCreditosRecogidos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudCreditosRecogidosData", "CrearSolicitudCreditosRecogidosAvance", ex);
                        return null;
                    }
                }
            }
        }



        public SolicitudCreditosRecogidos CrearSolicitudCreditosRecogidos(SolicitudCreditosRecogidos pSolicitudCreditosRecogidos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        DbParameter pIDSOLICITUDRECOGE = cmdTransaccionFactory.CreateParameter();
                        pIDSOLICITUDRECOGE.ParameterName = "p_IDSOLICITUDRECOGE";
                        pIDSOLICITUDRECOGE.Value = pSolicitudCreditosRecogidos.idsolicitudrecoge;
                        pIDSOLICITUDRECOGE.Direction = ParameterDirection.InputOutput;

                        DbParameter pNUMEROSOLICITUD = cmdTransaccionFactory.CreateParameter();
                        pNUMEROSOLICITUD.ParameterName = "p_NUMEROSOLICITUD";
                        pNUMEROSOLICITUD.Value = pSolicitudCreditosRecogidos.numerosolicitud;

                        DbParameter pNUMERO_RECOGE = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RECOGE.ParameterName = "p_NUMERO_RECOGE";
                        pNUMERO_RECOGE.Value = pSolicitudCreditosRecogidos.numero_recoge;

                        DbParameter pFECHARECOGE = cmdTransaccionFactory.CreateParameter();
                        pFECHARECOGE.ParameterName = "p_FECHARECOGE";
                        pFECHARECOGE.Value = pSolicitudCreditosRecogidos.fecharecoge;

                        DbParameter pVALORRECOGE = cmdTransaccionFactory.CreateParameter();
                        pVALORRECOGE.ParameterName = "p_VALORRECOGE";
                        pVALORRECOGE.Value = pSolicitudCreditosRecogidos.valorrecoge;

                        DbParameter pFECHAPAGO = cmdTransaccionFactory.CreateParameter();
                        pFECHAPAGO.ParameterName = "p_FECHAPAGO";
                        pFECHAPAGO.Value = pSolicitudCreditosRecogidos.fechapago;

                        DbParameter pSALDOCAPITAL = cmdTransaccionFactory.CreateParameter();
                        pSALDOCAPITAL.ParameterName = "p_SALDOCAPITAL";
                        pSALDOCAPITAL.Value = pSolicitudCreditosRecogidos.saldocapital;

                        DbParameter pSALDOINTCORR = cmdTransaccionFactory.CreateParameter();
                        pSALDOINTCORR.ParameterName = "p_SALDOINTCORR";
                        pSALDOINTCORR.Value = pSolicitudCreditosRecogidos.saldointcorr;

                        DbParameter pSALDOINTMORA = cmdTransaccionFactory.CreateParameter();
                        pSALDOINTMORA.ParameterName = "p_SALDOINTMORA";
                        pSALDOINTMORA.Value = pSolicitudCreditosRecogidos.saldointmora;

                        DbParameter pSALDOMIPYME = cmdTransaccionFactory.CreateParameter();
                        pSALDOMIPYME.ParameterName = "p_SALDOMIPYME";
                        pSALDOMIPYME.Value = pSolicitudCreditosRecogidos.saldomipyme;

                        DbParameter pSALDOIVAMIPYME = cmdTransaccionFactory.CreateParameter();
                        pSALDOIVAMIPYME.ParameterName = "p_SALDOIVAMIPYME";
                        pSALDOIVAMIPYME.Value = pSolicitudCreditosRecogidos.saldoivamipyme;

                        DbParameter pSALDOOTROS = cmdTransaccionFactory.CreateParameter();
                        pSALDOOTROS.ParameterName = "p_SALDOOTROS";
                        pSALDOOTROS.Value = pSolicitudCreditosRecogidos.saldootros;

                        DbParameter pVALORNOMINAS = cmdTransaccionFactory.CreateParameter();
                        pVALORNOMINAS.ParameterName = "p_VALOR_NOMINAS";
                        pVALORNOMINAS.Value = pSolicitudCreditosRecogidos.valor_nominas;


                        cmdTransaccionFactory.Parameters.Add(pIDSOLICITUDRECOGE);
                        cmdTransaccionFactory.Parameters.Add(pNUMEROSOLICITUD);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RECOGE);
                        cmdTransaccionFactory.Parameters.Add(pFECHARECOGE);
                        cmdTransaccionFactory.Parameters.Add(pVALORRECOGE);
                        cmdTransaccionFactory.Parameters.Add(pFECHAPAGO);
                        cmdTransaccionFactory.Parameters.Add(pSALDOCAPITAL);
                        cmdTransaccionFactory.Parameters.Add(pSALDOINTCORR);
                        cmdTransaccionFactory.Parameters.Add(pSALDOINTMORA);
                        cmdTransaccionFactory.Parameters.Add(pSALDOMIPYME);
                        cmdTransaccionFactory.Parameters.Add(pSALDOIVAMIPYME);
                        cmdTransaccionFactory.Parameters.Add(pSALDOOTROS);
                        cmdTransaccionFactory.Parameters.Add(pVALORNOMINAS);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_CRERE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pSolicitudCreditosRecogidos, "SolicitudCreditosRecogidos", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pSolicitudCreditosRecogidos.idsolicitudrecoge = Convert.ToInt64(pIDSOLICITUDRECOGE.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pSolicitudCreditosRecogidos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudCreditosRecogidosData", "CrearSolicitudCreditosRecogidos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla SolicitudCreditosRecogidos de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad SolicitudCreditosRecogidos modificada</returns>
        public SolicitudCreditosRecogidos ModificarSolicitudCreditosRecogidos(SolicitudCreditosRecogidos pSolicitudCreditosRecogidos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDSOLICITUDRECOGE = cmdTransaccionFactory.CreateParameter();
                        pIDSOLICITUDRECOGE.ParameterName = "p_IDSOLICITUDRECOGE";
                        pIDSOLICITUDRECOGE.Value = pSolicitudCreditosRecogidos.idsolicitudrecoge;

                        DbParameter pNUMEROSOLICITUD = cmdTransaccionFactory.CreateParameter();
                        pNUMEROSOLICITUD.ParameterName = "p_NUMEROSOLICITUD";
                        pNUMEROSOLICITUD.Value = pSolicitudCreditosRecogidos.numerosolicitud;

                        DbParameter pNUMERO_RECOGE = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RECOGE.ParameterName = "p_NUMERO_RECOGE";
                        pNUMERO_RECOGE.Value = pSolicitudCreditosRecogidos.numero_recoge;

                        DbParameter pFECHARECOGE = cmdTransaccionFactory.CreateParameter();
                        pFECHARECOGE.ParameterName = "p_FECHARECOGE";
                        pFECHARECOGE.Value = pSolicitudCreditosRecogidos.fecharecoge;

                        DbParameter pVALORRECOGE = cmdTransaccionFactory.CreateParameter();
                        pVALORRECOGE.ParameterName = "p_VALORRECOGE";
                        pVALORRECOGE.Value = pSolicitudCreditosRecogidos.valorrecoge;

                        DbParameter pFECHAPAGO = cmdTransaccionFactory.CreateParameter();
                        pFECHAPAGO.ParameterName = "p_FECHAPAGO";
                        pFECHAPAGO.Value = pSolicitudCreditosRecogidos.fechapago;

                        DbParameter pSALDOCAPITAL = cmdTransaccionFactory.CreateParameter();
                        pSALDOCAPITAL.ParameterName = "p_SALDOCAPITAL";
                        pSALDOCAPITAL.Value = pSolicitudCreditosRecogidos.saldocapital;

                        DbParameter pSALDOINTCORR = cmdTransaccionFactory.CreateParameter();
                        pSALDOINTCORR.ParameterName = "p_SALDOINTCORR";
                        pSALDOINTCORR.Value = pSolicitudCreditosRecogidos.saldointcorr;

                        DbParameter pSALDOINTMORA = cmdTransaccionFactory.CreateParameter();
                        pSALDOINTMORA.ParameterName = "p_SALDOINTMORA";
                        pSALDOINTMORA.Value = pSolicitudCreditosRecogidos.saldointmora;

                        DbParameter pSALDOMIPYME = cmdTransaccionFactory.CreateParameter();
                        pSALDOMIPYME.ParameterName = "p_SALDOMIPYME";
                        pSALDOMIPYME.Value = pSolicitudCreditosRecogidos.saldomipyme;

                        DbParameter pSALDOIVAMIPYME = cmdTransaccionFactory.CreateParameter();
                        pSALDOIVAMIPYME.ParameterName = "p_SALDOIVAMIPYME";
                        pSALDOIVAMIPYME.Value = pSolicitudCreditosRecogidos.saldoivamipyme;

                        DbParameter pSALDOOTROS = cmdTransaccionFactory.CreateParameter();
                        pSALDOOTROS.ParameterName = "p_SALDOOTROS";
                        pSALDOOTROS.Value = pSolicitudCreditosRecogidos.saldootros;

                        cmdTransaccionFactory.Parameters.Add(pIDSOLICITUDRECOGE);
                        cmdTransaccionFactory.Parameters.Add(pNUMEROSOLICITUD);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RECOGE);
                        cmdTransaccionFactory.Parameters.Add(pFECHARECOGE);
                        cmdTransaccionFactory.Parameters.Add(pVALORRECOGE);
                        cmdTransaccionFactory.Parameters.Add(pFECHAPAGO);
                        cmdTransaccionFactory.Parameters.Add(pSALDOCAPITAL);
                        cmdTransaccionFactory.Parameters.Add(pSALDOINTCORR);
                        cmdTransaccionFactory.Parameters.Add(pSALDOINTMORA);
                        cmdTransaccionFactory.Parameters.Add(pSALDOMIPYME);
                        cmdTransaccionFactory.Parameters.Add(pSALDOIVAMIPYME);
                        cmdTransaccionFactory.Parameters.Add(pSALDOOTROS);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_CRERE_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pSolicitudCreditosRecogidos, "SolicitudCreditosRecogidos",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pSolicitudCreditosRecogidos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudCreditosRecogidosData", "ModificarSolicitudCreditosRecogidos", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Modifica un registro en la tabla SolicitudCreditosRecogidos de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad SolicitudCreditosRecogidos modificada</returns>
        public SolicitudCreditosRecogidos ParametrosSolicredRecoger(SolicitudCreditosRecogidos pSolicitudCreditosRecogidos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pIDENTIFICACION.ParameterName = "pidentificacion";
                        pIDENTIFICACION.Value = pSolicitudCreditosRecogidos.identificacion;

                        DbParameter pFECHA_PAGO = cmdTransaccionFactory.CreateParameter();
                        pFECHA_PAGO.ParameterName = "pfecha_pago";
                        pFECHA_PAGO.Value = pSolicitudCreditosRecogidos.fecha_pago;
                        
                        cmdTransaccionFactory.Parameters.Add(pIDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_PAGO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "usp_xpinn_solicred_recoger";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pSolicitudCreditosRecogidos, "SolicitudCreditosRecogidos", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pSolicitudCreditosRecogidos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudCreditosRecogidosData", "ModificarSolicitudCreditosRecogidos", ex);
                        return null;
                    }
                }
            }
        }







        /// <summary>
        /// Elimina un registro en la tabla SolicitudCreditosRecogidos de la base de datos
        /// </summary>
        /// <param name="pId">identificador de SolicitudCreditosRecogidos</param>
        public void EliminarSolicitudCreditosRecogidos(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        SolicitudCreditosRecogidos pSolicitudCreditosRecogidos = new SolicitudCreditosRecogidos();

                        if (pUsuario.programaGeneraLog)
                            pSolicitudCreditosRecogidos = ConsultarSolicitudCreditosRecogidos(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pIDSOLICITUDRECOGE = cmdTransaccionFactory.CreateParameter();
                        pIDSOLICITUDRECOGE.ParameterName = "p_IDSOLICITUDRECOGE";
                        pIDSOLICITUDRECOGE.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pIDSOLICITUDRECOGE);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_CRERE_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pSolicitudCreditosRecogidos, "SolicitudCreditosRecogidos", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudCreditosRecogidosData", "EliminarSolicitudCreditosRecogidos", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla SolicitudCreditosRecogidos de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla SolicitudCreditosRecogidos</param>
        /// <returns>Entidad SolicitudCreditosRecogidos consultado</returns>
        public SolicitudCreditosRecogidos ConsultarSolicitudCreditosRecogidos(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            SolicitudCreditosRecogidos entidad = new SolicitudCreditosRecogidos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  SOLICITUDCREDSRECOGIDOS WHERE IDSOLICITUDRECOGE = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDSOLICITUDRECOGE"] != DBNull.Value) entidad.idsolicitudrecoge = Convert.ToInt64(resultado["IDSOLICITUDRECOGE"]);
                            if (resultado["NUMEROSOLICITUD"] != DBNull.Value) entidad.numerosolicitud = Convert.ToInt64(resultado["NUMEROSOLICITUD"]);
                            if (resultado["NUMERO_RECOGE"] != DBNull.Value) entidad.numero_recoge = Convert.ToInt64(resultado["NUMERO_RECOGE"]);
                            if (resultado["FECHARECOGE"] != DBNull.Value) entidad.fecharecoge = Convert.ToDateTime(resultado["FECHARECOGE"]);
                            if (resultado["VALORRECOGE"] != DBNull.Value) entidad.valorrecoge = Convert.ToInt64(resultado["VALORRECOGE"]);
                            if (resultado["FECHAPAGO"] != DBNull.Value) entidad.fechapago = Convert.ToDateTime(resultado["FECHAPAGO"]);
                            if (resultado["SALDOCAPITAL"] != DBNull.Value) entidad.saldocapital = Convert.ToInt64(resultado["SALDOCAPITAL"]);
                            if (resultado["SALDOINTCORR"] != DBNull.Value) entidad.saldointcorr = Convert.ToInt64(resultado["SALDOINTCORR"]);
                            if (resultado["SALDOINTMORA"] != DBNull.Value) entidad.saldointmora = Convert.ToInt64(resultado["SALDOINTMORA"]);
                            if (resultado["SALDOMIPYME"] != DBNull.Value) entidad.saldomipyme = Convert.ToInt64(resultado["SALDOMIPYME"]);
                            if (resultado["SALDOIVAMIPYME"] != DBNull.Value) entidad.saldoivamipyme = Convert.ToInt64(resultado["SALDOIVAMIPYME"]);
                            if (resultado["SALDOOTROS"] != DBNull.Value) entidad.saldootros = Convert.ToInt64(resultado["SALDOOTROS"]);
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
                        BOExcepcion.Throw("SolicitudCreditosRecogidosData", "ConsultarSolicitudCreditosRecogidos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla SolicitudCreditosRecogidos dados unos filtros
        /// </summary>
        /// <param name="pSolicitudCreditosRecogidos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de SolicitudCreditosRecogidos obtenidos</returns>
        public List<SolicitudCreditosRecogidos> ListarSolicitudCreditosRecogidos(SolicitudCreditosRecogidos pSolicitudCreditosRecogidos, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<SolicitudCreditosRecogidos> lstSolicitudCreditosRecogidos = new List<SolicitudCreditosRecogidos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  SOLICITUDCREDSRECOGIDOS " + ObtenerFiltro(pSolicitudCreditosRecogidos);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            SolicitudCreditosRecogidos entidad = new SolicitudCreditosRecogidos();

                            if (resultado["IDSOLICITUDRECOGE"] != DBNull.Value) entidad.idsolicitudrecoge = Convert.ToInt64(resultado["IDSOLICITUDRECOGE"]);
                            if (resultado["NUMEROSOLICITUD"] != DBNull.Value) entidad.numerosolicitud = Convert.ToInt64(resultado["NUMEROSOLICITUD"]);
                            if (resultado["NUMERO_RECOGE"] != DBNull.Value) entidad.numero_recoge = Convert.ToInt64(resultado["NUMERO_RECOGE"]);
                            if (resultado["FECHARECOGE"] != DBNull.Value) entidad.fecharecoge = Convert.ToDateTime(resultado["FECHARECOGE"]);
                            if (resultado["VALORRECOGE"] != DBNull.Value) entidad.valorrecoge = Convert.ToInt64(resultado["VALORRECOGE"]);
                            if (resultado["FECHAPAGO"] != DBNull.Value) entidad.fechapago = Convert.ToDateTime(resultado["FECHAPAGO"]);
                            if (resultado["SALDOCAPITAL"] != DBNull.Value) entidad.saldocapital = Convert.ToInt64(resultado["SALDOCAPITAL"]);
                            if (resultado["SALDOINTCORR"] != DBNull.Value) entidad.saldointcorr = Convert.ToInt64(resultado["SALDOINTCORR"]);
                            if (resultado["SALDOINTMORA"] != DBNull.Value) entidad.saldointmora = Convert.ToInt64(resultado["SALDOINTMORA"]);
                            if (resultado["SALDOMIPYME"] != DBNull.Value) entidad.saldomipyme = Convert.ToInt64(resultado["SALDOMIPYME"]);
                            if (resultado["SALDOIVAMIPYME"] != DBNull.Value) entidad.saldoivamipyme = Convert.ToInt64(resultado["SALDOIVAMIPYME"]);
                            if (resultado["SALDOOTROS"] != DBNull.Value) entidad.saldootros = Convert.ToInt64(resultado["SALDOOTROS"]);

                            lstSolicitudCreditosRecogidos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstSolicitudCreditosRecogidos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudCreditosRecogidosData", "ListarSolicitudCreditosRecogidos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla SolicitudCreditosRecogidos dados unos filtros
        /// </summary>
        /// <param name="pSolicitudCreditosRecogidos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de SolicitudCreditosRecogidos obtenidos</returns>
        public List<SolicitudCreditosRecogidos> ListarSolicitudCreditosRecogidos(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<SolicitudCreditosRecogidos> lstSolicitudCreditosRecogidos = new List<SolicitudCreditosRecogidos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  SOLICITUDCREDSRECOGIDOS ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            SolicitudCreditosRecogidos entidad = new SolicitudCreditosRecogidos();

                            if (resultado["IDSOLICITUDRECOGE"] != DBNull.Value) entidad.idsolicitudrecoge = Convert.ToInt64(resultado["IDSOLICITUDRECOGE"]);
                            if (resultado["NUMEROSOLICITUD"] != DBNull.Value) entidad.numerosolicitud = Convert.ToInt64(resultado["NUMEROSOLICITUD"]);
                            if (resultado["NUMERO_RECOGE"] != DBNull.Value) entidad.numero_recoge = Convert.ToInt64(resultado["NUMERO_RECOGE"]);
                            if (resultado["FECHARECOGE"] != DBNull.Value) entidad.fecharecoge = Convert.ToDateTime(resultado["FECHARECOGE"]);
                            if (resultado["VALORRECOGE"] != DBNull.Value) entidad.valorrecoge = Convert.ToInt64(resultado["VALORRECOGE"]);
                            if (resultado["FECHAPAGO"] != DBNull.Value) entidad.fechapago = Convert.ToDateTime(resultado["FECHAPAGO"]);
                            if (resultado["SALDOCAPITAL"] != DBNull.Value) entidad.saldocapital = Convert.ToInt64(resultado["SALDOCAPITAL"]);
                            if (resultado["SALDOINTCORR"] != DBNull.Value) entidad.saldointcorr = Convert.ToInt64(resultado["SALDOINTCORR"]);
                            if (resultado["SALDOINTMORA"] != DBNull.Value) entidad.saldointmora = Convert.ToInt64(resultado["SALDOINTMORA"]);
                            if (resultado["SALDOMIPYME"] != DBNull.Value) entidad.saldomipyme = Convert.ToInt64(resultado["SALDOMIPYME"]);
                            if (resultado["SALDOIVAMIPYME"] != DBNull.Value) entidad.saldoivamipyme = Convert.ToInt64(resultado["SALDOIVAMIPYME"]);
                            if (resultado["SALDOOTROS"] != DBNull.Value) entidad.saldootros = Convert.ToInt64(resultado["SALDOOTROS"]);

                            lstSolicitudCreditosRecogidos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstSolicitudCreditosRecogidos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudCreditosRecogidosData", "ListarSolicitudCreditosRecogidos", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Mètodo para cargar listado de crèditos a recoger de un crèdito
        /// </summary>
        /// <param name="NumeroRadicacion"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<SolicitudCreditosRecogidos> ListarCreditosRecogidos(Int64 pNumeroRadicacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<SolicitudCreditosRecogidos> lstSolicitudCreditosRecogidos = new List<SolicitudCreditosRecogidos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  CREDITOSRECOGIDOS WHERE NUMERO_RADICACION = " + pNumeroRadicacion.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            SolicitudCreditosRecogidos entidad = new SolicitudCreditosRecogidos();

                            if (resultado["IDRECOGE"] != DBNull.Value) entidad.idsolicitudrecoge = Convert.ToInt64(resultado["IDRECOGE"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numerosolicitud = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["NUMERO_RECOGE"] != DBNull.Value) entidad.numero_recoge = Convert.ToInt64(resultado["NUMERO_RECOGE"]);
                            if (resultado["FECHARECOGE"] != DBNull.Value) entidad.fecharecoge = Convert.ToDateTime(resultado["FECHARECOGE"]);
                            if (resultado["VALORRECOGE"] != DBNull.Value) entidad.valorrecoge = Convert.ToInt64(resultado["VALORRECOGE"]);
                            if (resultado["FECHAPAGO"] != DBNull.Value) entidad.fechapago = Convert.ToDateTime(resultado["FECHAPAGO"]);
                            if (resultado["SALDOCAPITAL"] != DBNull.Value) entidad.saldocapital = Convert.ToInt64(resultado["SALDOCAPITAL"]);
                            lstSolicitudCreditosRecogidos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstSolicitudCreditosRecogidos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudCreditosRecogidosData", "ListarCreditosRecogidos", ex);
                        return null;
                    }
                }
            }
        }




        /// <summary>
        /// Obtiene una lista de Entidades de la tabla SolicitudCreditosRecogidos dados unos filtros
        /// </summary>
        /// <param name="pSolicitudCreditosRecogidos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de SolicitudCreditosRecogidos obtenidos</returns>
        public List<SolicitudCreditosRecogidos> ListarTemp_recoger(SolicitudCreditosRecogidos pSolicitudCreditosRecogidos, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<SolicitudCreditosRecogidos> lstSolicitudCreditosRecogidos = new List<SolicitudCreditosRecogidos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TEMP_RECOGER "; //+ ObtenerFiltro(pSolicitudCreditosRecogidos);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            SolicitudCreditosRecogidos entidad = new SolicitudCreditosRecogidos();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numeroRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);
                            if (resultado["MONTO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldocapitalTemp = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["INTERES_CORRIENTE"] != DBNull.Value) entidad.interescorriente = Convert.ToInt64(resultado["INTERES_CORRIENTE"]);
                            if (resultado["INTERES_MORA"] != DBNull.Value) entidad.interesmora = Convert.ToInt64(resultado["INTERES_MORA"]);
                            if (resultado["SEGURO"] != DBNull.Value) entidad.seguro = Convert.ToInt64(resultado["SEGURO"]);
                            if (resultado["LEYMIPYME"] != DBNull.Value) entidad.leymipyme = Convert.ToInt64(resultado["LEYMIPYME"]);
                            if (resultado["IVA_LEYMIPYME"] != DBNull.Value) entidad.ivaLeymipyme = Convert.ToInt64(resultado["IVA_LEYMIPYME"]);
                            if (resultado["OTROS"] != DBNull.Value) entidad.otros = Convert.ToInt64(resultado["OTROS"]);
                            if (resultado["TOTAL_RECOGER"] != DBNull.Value) entidad.totalRecoger = Convert.ToInt64(resultado["TOTAL_RECOGER"]);
                            
                            lstSolicitudCreditosRecogidos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstSolicitudCreditosRecogidos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudCreditosRecogidosData", "ListarSolicitudCreditosRecogidos", ex);
                        return null;
                    }
                }
            }
        }



        public List<SolicitudCreditoAAC> ListarSolicitudCreditoAAC(string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<SolicitudCreditoAAC> lstSolicitudCreditosRecogidos = new List<SolicitudCreditoAAC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT S.*,P.DESCRIPCION,L.NOMBRE,
                                        CASE S.COD_PERSONA WHEN NULL THEN A.IDENTIFICACION  ELSE V.IDENTIFICACION END AS IDENTIFICACION,
                                        CASE S.COD_PERSONA WHEN NULL THEN (SELECT DESCRIPCION FROM TIPOIDENTIFICACION X WHERE X.CODTIPOIDENTIFICACION = A.TIPO_IDENTIFICACION)  
                                        ELSE (SELECT DESCRIPCION FROM TIPOIDENTIFICACION X WHERE X.CODTIPOIDENTIFICACION = V.TIPO_IDENTIFICACION) END AS TIPO_IDENTIFICACION,
                                        CASE S.COD_PERSONA WHEN NULL THEN TRIM(SUBSTR(A.PRIMER_NOMBRE || ' ' || A.SEGUNDO_NOMBRE || ' ' ||  A.PRIMER_APELLIDO || ' ' || A.SEGUNDO_APELLIDO, 0, 240)) 
                                        ELSE V.NOMBRE END AS NOM_PERSONA
                                        ,aj.snombre1||' '||aj.Sapellido1||' '||aj.Sapellido2 as NOMBREEJE
                                        FROM SOLICITUDCRED S LEFT JOIN PERIODICIDAD P ON P.COD_PERIODICIDAD = S.PERIODICIDAD
                                        LEFT JOIN LINEASCREDITO L ON L.COD_LINEA_CREDITO = S.TIPOCREDITO
                                        LEFT JOIN SOLICITUD_PERSONA_AFI A ON A.ID_PERSONA = S.ID_PERSONA
                                        LEFT JOIN V_PERSONA V ON V.COD_PERSONA = S.COD_PERSONA
                                        left join Asejecutivos aj on Aj.Icodigo = v.Cod_Asesor " + pFiltro + " ORDER BY 1"; 

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            SolicitudCreditoAAC entidad = new SolicitudCreditoAAC();
                            if (resultado["NUMEROSOLICITUD"] != DBNull.Value) entidad.numerosolicitud = Convert.ToInt32(resultado["NUMEROSOLICITUD"]);
                            if (resultado["FECHASOLICITUD"] != DBNull.Value) entidad.fechasolicitud = Convert.ToDateTime(resultado["FECHASOLICITUD"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt32(resultado["COD_PERSONA"]);
                            if (resultado["MONTOSOLICITADO"] != DBNull.Value) entidad.montosolicitado = Convert.ToDecimal(resultado["MONTOSOLICITADO"]);
                            if (resultado["PLAZOSOLICITADO"] != DBNull.Value) entidad.plazosolicitado = Convert.ToInt32(resultado["PLAZOSOLICITADO"]);
                            if (resultado["CUOTASOLICITADA"] != DBNull.Value) entidad.cuotasolicitada = Convert.ToDecimal(resultado["CUOTASOLICITADA"]);
                            if (resultado["TIPOCREDITO"] != DBNull.Value) entidad.tipocredito = Convert.ToInt32(resultado["TIPOCREDITO"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToInt32(resultado["PERIODICIDAD"]);
                            if (resultado["MEDIO"] != DBNull.Value) entidad.medio = Convert.ToInt32(resultado["MEDIO"]);
                            if (resultado["REQPOLIZA"] != DBNull.Value) entidad.reqpoliza = Convert.ToString(resultado["REQPOLIZA"]);
                            if (resultado["OTROMEDIO"] != DBNull.Value) entidad.otromedio = Convert.ToString(resultado["OTROMEDIO"]);
                            if (resultado["USUARIO"] != DBNull.Value) entidad.usuario = Convert.ToString(resultado["USUARIO"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["CONCEPTO"]);
                            if (resultado["GARANTIA"] != DBNull.Value) entidad.garantia = Convert.ToInt32(resultado["GARANTIA"]);
                            if (resultado["GARANTIA_COMUNITARIA"] != DBNull.Value) entidad.garantia_comunitaria = Convert.ToInt32(resultado["GARANTIA_COMUNITARIA"]);
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt32(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (resultado["EMPRESA_RECAUDO"] != DBNull.Value) entidad.empresa_recaudo = Convert.ToInt32(resultado["EMPRESA_RECAUDO"]);
                            if (resultado["DESTINO"] != DBNull.Value) entidad.destino = Convert.ToInt32(resultado["DESTINO"]);
                            if (resultado["ID_PERSONA"] != DBNull.Value) entidad.id_persona = Convert.ToInt64(resultado["ID_PERSONA"]);
                            if (resultado["NUM_CUENTA"] != DBNull.Value) entidad.num_cuenta = Convert.ToString(resultado["NUM_CUENTA"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["COD_BANCO"]);
                            if (resultado["CODCIUDAD_PROPIEDAD"] != DBNull.Value) entidad.codciudad_propiedad = Convert.ToInt64(resultado["CODCIUDAD_PROPIEDAD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nom_periodicidad = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOM_PERSONA"] != DBNull.Value) entidad.nom_persona = Convert.ToString(resultado["NOM_PERSONA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["NOMBREEJE"] != DBNull.Value) entidad.otro_propiedad = Convert.ToString(resultado["NOMBREEJE"]);
                            lstSolicitudCreditosRecogidos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstSolicitudCreditosRecogidos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudCreditosRecogidosData", "ListarSolicitudCreditoAAC", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarSolicitudCreditoAAC(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumerosolicitud = cmdTransaccionFactory.CreateParameter();
                        pnumerosolicitud.ParameterName = "p_numerosolicitud";
                        pnumerosolicitud.Value = pId;
                        pnumerosolicitud.Direction = ParameterDirection.Input;
                        pnumerosolicitud.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumerosolicitud);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_SOLICITUDC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudCreditosRecogidosData", "EliminarSolicitudCreditoAAC", ex);
                    }
                }
            }
        }


        public Int64? ConfirmacionSolicitudCredito(SolicitudCreditoAAC pSolicitud,ref string pError, Usuario vUsuario)
        {
            Int64? pNumero_Radicacion = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumerosolicitud = cmdTransaccionFactory.CreateParameter();
                        pnumerosolicitud.ParameterName = "p_numerosolicitud";
                        pnumerosolicitud.Value = pSolicitud.numerosolicitud;
                        pnumerosolicitud.Direction = ParameterDirection.Input;
                        pnumerosolicitud.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumerosolicitud);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pSolicitud.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pusuario = cmdTransaccionFactory.CreateParameter();
                        pusuario.ParameterName = "p_usuario";
                        pusuario.Value = vUsuario.codusuario;
                        pusuario.Direction = ParameterDirection.Input;
                        pusuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pusuario);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = 0;
                        pnumero_radicacion.Direction = ParameterDirection.Output;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CONFIRSOLICITUD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        pNumero_Radicacion = Convert.ToInt64(pnumero_radicacion.Value);
                        return pNumero_Radicacion;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return 0;
                    }
                }
            }
        }

    }
}