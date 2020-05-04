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
    /// Objeto de acceso a datos para la tabla Credito
    /// </summary>
    public class CreditoPlanData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Credito
        /// </summary>
        public CreditoPlanData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<CreditoPlan> ListarCredito(CreditoPlan pCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CreditoPlan> lstCreditoPlan = new List<CreditoPlan>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from V_CREDITOS where 1=1 " + filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CreditoPlan entidad = new CreditoPlan();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.Numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.Linea = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.Monto = Convert.ToInt64(resultado["MONTO_SOLICITADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.Plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.Cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["NUMEROSOLICITUD"] != DBNull.Value) entidad.NumeroSolicitud = Convert.ToInt64(resultado["NUMEROSOLICITUD"]);
                            if (resultado["NOMBRE_ESTADO"] != DBNull.Value) entidad.Estado = Convert.ToString(resultado["NOMBRE_ESTADO"]);
                            if (resultado["TIPO_LINEA"] != DBNull.Value) entidad.tipo_linea = Convert.ToInt64(resultado["TIPO_LINEA"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.Cod_persona = Convert.ToInt64(resultado["COD_DEUDOR"]);

                            lstCreditoPlan.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCreditoPlan;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoPlanData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene un registro en la tabla Credito de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Credito</param>
        /// <returns>Entidad Credito consultado</returns>
        public CreditoPlan ConsultarCredito(Int64 pId, Boolean bTasa, Usuario pUsuario)
        {
            DbDataReader resultado;
            CreditoPlan entidad = new CreditoPlan();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from VFABRICACREDITOS where numero_radicacion=" + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.Numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.LineaCredito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.Linea = Convert.ToString(resultado["LINEA"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.Plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.Cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.Monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (entidad.Monto == 0)
                                if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.Monto = Convert.ToInt64(resultado["MONTO_SOLICITADO"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.Periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.FormaPago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.FechaInicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["FECHA_PRIMERPAGO"] != DBNull.Value) entidad.FechaPrimerPago = Convert.ToDateTime(resultado["FECHA_PRIMERPAGO"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.FechaSolicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["FECHA_DESEMBOLSO"] != DBNull.Value) entidad.FechaDesembolso = Convert.ToDateTime(resultado["FECHA_DESEMBOLSO"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.FechaAprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["DIAS_AJUSTE"] != DBNull.Value) entidad.DiasAjuste = Convert.ToInt64(resultado["DIAS_AJUSTE"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.TasaInteres = Convert.ToInt64(resultado["TASA_INTERES"]);
                            if (resultado["LEY_MIPYME"] != DBNull.Value) entidad.LeyMiPyme = Convert.ToInt64(resultado["LEY_MIPYME"]);
                            if (resultado["MONEDA"] != DBNull.Value) entidad.Moneda = Convert.ToString(resultado["MONEDA"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToString(resultado["NUMERO_CUOTAS"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToString(resultado["CIUDAD"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.Direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["PAGARE"] != DBNull.Value) entidad.pagare = Convert.ToString(resultado["PAGARE"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt64(resultado["COD_CLASIFICA"]);
                            if (resultado["cod_persona"] != DBNull.Value) entidad.Cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.Estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.Tipo_Identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["TIR"] != DBNull.Value) entidad.tir = Convert.ToDecimal(resultado["TIR"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToDecimal(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["CUOTAS_PENDIENTES"] != DBNull.Value) entidad.cuotas_pendientes = Convert.ToDecimal(resultado["CUOTAS_PENDIENTES"]);
                            if (resultado["TIPO_LINEA"] != DBNull.Value) entidad.tipo_linea = Convert.ToInt64(resultado["TIPO_LINEA"]);
                            if (resultado["NUMERO_SOLICITUD"] != DBNull.Value) entidad.NumeroSolicitud = Convert.ToInt64(resultado["NUMERO_SOLICITUD"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["NOM_ESTADO"]);
                            if (resultado["REESTRUCTURADO"] != DBNull.Value) entidad.Reestructurado = Convert.ToInt32(resultado["REESTRUCTURADO"]);
                            if (bTasa == true)
                            {
                                DbParameter pNUMEROCREDITO = cmdTransaccionFactory.CreateParameter();
                                pNUMEROCREDITO.ParameterName = "N_Radic";
                                pNUMEROCREDITO.Value = pId.ToString();

                                DbParameter pTASA = cmdTransaccionFactory.CreateParameter();
                                pTASA.ParameterName = "n_tasa_IntCte_per";
                                pTASA.Value = 0;
                                pTASA.DbType = DbType.Double;
                                pTASA.Direction = ParameterDirection.Output;

                                DbParameter pTASAEFE = cmdTransaccionFactory.CreateParameter();
                                pTASAEFE.ParameterName = "N_Tasa_IntCte_Efe";
                                pTASAEFE.Value = 0;
                                pTASAEFE.DbType = DbType.Double;
                                pTASAEFE.Direction = ParameterDirection.Output;

                                DbParameter pTASANOM = cmdTransaccionFactory.CreateParameter();
                                pTASANOM.ParameterName = "N_Tasa_IntCte_Nom";
                                pTASANOM.Value = 0;
                                pTASANOM.DbType = DbType.Double;
                                pTASANOM.Direction = ParameterDirection.Output;

                                cmdTransaccionFactory.Parameters.Add(pNUMEROCREDITO);
                                cmdTransaccionFactory.Parameters.Add(pTASA);
                                cmdTransaccionFactory.Parameters.Add(pTASAEFE);
                                cmdTransaccionFactory.Parameters.Add(pTASANOM);

                                cmdTransaccionFactory.Connection = connection;
                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                cmdTransaccionFactory.CommandText = "clscredito.Tasa_Interes";
                                cmdTransaccionFactory.ExecuteNonQuery();

                                entidad.TasaInteres = Convert.ToDouble(pTASA.Value);
                                entidad.TasaEfe = Convert.ToDouble(pTASAEFE.Value);
                                entidad.TasaNom = Convert.ToDouble(pTASANOM.Value);
                            }

                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoPlanData", "ConsultarCredito", ex);
                        return null;
                    }
                }
            }
        }



        public DatosSolicitud ConsultarProveedorXCredito(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            DatosSolicitud entidad = new DatosSolicitud();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM CREDITO_ORDEN_SERVICIO WHERE NUMERO_RADICACION = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDPROVEEDOR"] != DBNull.Value) entidad.identificacionprov = Convert.ToString(resultado["IDPROVEEDOR"]);
                            if (resultado["NOMPROVEEDOR"] != DBNull.Value) entidad.nombreprov = Convert.ToString(resultado["NOMPROVEEDOR"]);
                            if (resultado["NUMERO_PREIMPRESO"] != DBNull.Value) entidad.num_preimpreso = Convert.ToInt64(resultado["NUMERO_PREIMPRESO"]);
                        }
                        else
                        {
                            DbDataReader resultado1;
                            cmdTransaccionFactory.Dispose();
                            cmdTransaccionFactory.Parameters.Clear();

                            string sql1 = "SELECT S.IDPROVEEDOR, S.NOMPROVEEDOR "
                                    + "FROM CREDITO C INNER JOIN SOLICITUDCRED S ON S.NUMEROSOLICITUD = C.NUMERO_OBLIGACION "
                                    + "WHERE C.NUMERO_RADICACION = " + pId.ToString();

                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql1;
                            resultado1 = cmdTransaccionFactory.ExecuteReader();
                            if (resultado1.Read())
                            {
                                if (resultado["IDPROVEEDOR"] != DBNull.Value) entidad.identificacionprov = Convert.ToString(resultado["IDPROVEEDOR"]);
                                if (resultado["NOMPROVEEDOR"] != DBNull.Value) entidad.nombreprov = Convert.ToString(resultado["NOMPROVEEDOR"]);
                            }
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoPlanData", "ConsultarProveedorXCredito", ex);
                        return null;
                    }
                }
            }
        }




        /// <summary>
        /// Servicio para Liquidar Crédito
        /// </summary>
        /// <param name="pEntity">Entidad Liquidar</param>
        /// <returns>Entidad creada</returns>

        public CreditoPlan Liquidar(CreditoPlan pCreditoPlan, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUMEROSOLICITUD = cmdTransaccionFactory.CreateParameter();
                        pNUMEROSOLICITUD.ParameterName = "pnumerosolicitud";
                        pNUMEROSOLICITUD.Value = pCreditoPlan.NumeroSolicitud;

                        DbParameter pNUMERORADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERORADICACION.ParameterName = "pnumero_radicacion";
                        pNUMERORADICACION.Value = 0;
                        pNUMERORADICACION.DbType = DbType.Int64;
                        pNUMERORADICACION.Direction = ParameterDirection.InputOutput;

                        cmdTransaccionFactory.Parameters.Add(pNUMEROSOLICITUD);
                        cmdTransaccionFactory.Parameters.Add(pNUMERORADICACION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "usp_xpinn_solicred_liquidar";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pCreditoPlan.Numero_Radicacion = Convert.ToInt64(pNUMERORADICACION.Value);

                        DAauditoria.InsertarLog(pCreditoPlan, "credito", pUsuario, Accion.Crear.ToString(), TipoAuditoria.Creditos, "Creacion de credito con numero de radicacion " + pCreditoPlan.Numero_Radicacion); //REGISTRO DE AUDITORIA
                        return pCreditoPlan;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoPlan", "Liquidar", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<CreditoPlan> ListarScoringCredito(CreditoPlan pCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CreditoPlan> lstCreditoPlan = new List<CreditoPlan>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from V_SCORING_CREDITOS " + filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CreditoPlan entidad = new CreditoPlan();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.Numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.Linea = Convert.ToString(resultado["LINEA"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.FechaSolicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.Monto = Convert.ToInt64(resultado["MONTO_SOLICITADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.Plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.Periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.Estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.DescripcionIdentificacion = Convert.ToString(resultado["DESCRIPCION"]);

                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["IDENTIFICACION"]);

                            if (resultado["DIRECCION"] != DBNull.Value) entidad.Direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.Telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Oficina = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["EJECUTIVO"] != DBNull.Value) entidad.Ejecutivo = Convert.ToString(resultado["EJECUTIVO"]);
                            lstCreditoPlan.Add(entidad);
                        }

                        return lstCreditoPlan;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoPlanData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }


    }
}
