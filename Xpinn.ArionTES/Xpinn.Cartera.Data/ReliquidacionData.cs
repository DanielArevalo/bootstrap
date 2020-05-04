using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Cartera.Entities;

namespace Xpinn.Cartera.Data
{
    public class ReliquidacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla  cierre histórico
        /// </summary>
        public ReliquidacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Método para listar los créditos a reliquidar
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarCredito(Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Xpinn.FabricaCreditos.Entities.Credito> lstCredito = new List<Xpinn.FabricaCreditos.Entities.Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from v_creditos " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.FabricaCreditos.Entities.Credito entidad = new Xpinn.FabricaCreditos.Entities.Credito();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);

                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReliquidacionData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }

        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarCreditoss(Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Xpinn.FabricaCreditos.Entities.Credito> lstCredito = new List<Xpinn.FabricaCreditos.Entities.Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select s.*, c.*, lineascredito.nombre as nombres, persona.primer_nombre||''||persona.primer_apellido as nombre, persona.identificacion , persona.cod_nomina
                                        From solicitudcred s 
                                        Left join lineascredito On lineascredito.cod_linea_credito = s.tipocredito
                                        Left join persona on s.cod_persona = persona.cod_persona 
                                        Left Join credito c On s.numerosolicitud = c.numero_obligacion And tipo_credito = 'S' 
                                        Where (c.estado Is Null Or c.estado In ('S', 'V', 'L', 'H')) " + filtro + " Order by 1 ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.FabricaCreditos.Entities.Credito entidad = new Xpinn.FabricaCreditos.Entities.Credito();

                            if (resultado["NUMEROSOLICITUD"] != DBNull.Value) entidad.numero_solicitud = Convert.ToInt64(resultado["NUMEROSOLICITUD"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["IDPROVEEDOR"] != DBNull.Value) entidad.idenprov = Convert.ToString(resultado["IDPROVEEDOR"]);
                            if (resultado["MONTOSOLICITADO"] != DBNull.Value) entidad.monto = Convert.ToInt32(resultado["MONTOSOLICITADO"]);
                            if (resultado["PLAZOSOLICITADO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZOSOLICITADO"]);
                            if (resultado["CUOTASOLICITADA"] != DBNull.Value) entidad.cuotas_pagass = Convert.ToInt32(resultado["CUOTASOLICITADA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nom_linea_credito = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.descrpcion = Convert.ToString(resultado["CONCEPTO"]);
                            if (resultado["OTROMEDIO"] != DBNull.Value) entidad.desc_tasa = Convert.ToString(resultado["OTROMEDIO"]);

                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReliquidacionData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }





        public Reliquidacion CrearReliquidacion(Reliquidacion pReliquidacion, ref string pError, Usuario pUsuario)
        {
            pError = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "pnumero_radicacion";
                        pNUMERO_RADICACION.DbType = DbType.Int64;
                        pNUMERO_RADICACION.Value = pReliquidacion.numero_radicacion;
                        pNUMERO_RADICACION.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);

                        DbParameter pFECHARELIQUIDA = cmdTransaccionFactory.CreateParameter();
                        pFECHARELIQUIDA.ParameterName = "pfecha_reliquida";
                        pFECHARELIQUIDA.DbType = DbType.Date;
                        pFECHARELIQUIDA.Value = pReliquidacion.fecha_reliquida;
                        pFECHARELIQUIDA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pFECHARELIQUIDA);

                        DbParameter pPLAZO = cmdTransaccionFactory.CreateParameter();
                        pPLAZO.ParameterName = "pplazo";
                        pPLAZO.DbType = DbType.Int64;
                        pPLAZO.Value = pReliquidacion.plazo_rel;
                        pPLAZO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pPLAZO);

                        DbParameter pFECHAPROXIMOPAGO = cmdTransaccionFactory.CreateParameter();
                        pFECHAPROXIMOPAGO.ParameterName = "pfecha_proximo_pago";
                        pFECHAPROXIMOPAGO.DbType = DbType.Date;
                        pFECHAPROXIMOPAGO.Value = pReliquidacion.fecha_prox_pago_rel;
                        pFECHAPROXIMOPAGO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pFECHAPROXIMOPAGO);

                        DbParameter pCUOTA = cmdTransaccionFactory.CreateParameter();
                        pCUOTA.ParameterName = "pcuota";
                        pCUOTA.DbType = DbType.Decimal;
                        pCUOTA.Value = pReliquidacion.cuota_rel;
                        pCUOTA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pCUOTA);

                        DbParameter pPERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        pPERIODICIDAD.ParameterName = "pcod_periodicidad";
                        pPERIODICIDAD.DbType = DbType.Decimal;
                        pPERIODICIDAD.Value = pReliquidacion.cod_periodicidad_rel;
                        pPERIODICIDAD.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pPERIODICIDAD);

                        DbParameter pCODUSU = cmdTransaccionFactory.CreateParameter();
                        pCODUSU.ParameterName = "pcod_usu";
                        pCODUSU.DbType = DbType.Int64;
                        pCODUSU.Value = pReliquidacion.cod_usuario;
                        pCODUSU.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pCODUSU);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_RELIQUIDACION";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pReliquidacion, "RELIQUIDACION", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        return pReliquidacion;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        BOExcepcion.Throw("ReliquidacionData", "CrearReliquidacion", ex);
                        return null;
                    }
                }
            }
        }
        public Xpinn.FabricaCreditos.Entities.Credito CreditoReliquidado(string pNumeroRadicacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Xpinn.FabricaCreditos.Entities.Credito CreditoReliquidado = new Xpinn.FabricaCreditos.Entities.Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select * from Reliquidacion where numero_radicacion = " + pNumeroRadicacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) CreditoReliquidado.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return CreditoReliquidado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReliquidacionData", "CreditoReliquidado", ex);
                        return null;
                    }
                }
            }
        }


    }
}
