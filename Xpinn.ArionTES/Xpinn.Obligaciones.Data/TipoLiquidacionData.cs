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
    /// Objeto de acceso a datos para la tabla TipoLiquidacion
    /// </summary>
    public class TipoLiquidacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla TipoLiquidacion
        /// </summary>
        public TipoLiquidacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        /// <summary>
        /// Crea una entidad Tipo de Liquidacion en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Tipo de Liquidacion</param>
        /// <returns>Entidad creada</returns>
        public TipoLiquidacion CrearTipoLiq(TipoLiquidacion pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                  
                        DbParameter pcod_tipoliq = cmdTransaccionFactory.CreateParameter();
                        pcod_tipoliq.ParameterName = "pcodigotipoliquidacion";
                        pcod_tipoliq.Value = pEntidad.codtipoliquidacion;
                        pcod_tipoliq.Direction = ParameterDirection.InputOutput;

                        DbParameter pnom_tipoliq = cmdTransaccionFactory.CreateParameter();
                        pnom_tipoliq.ParameterName = "pnombretipoliq";
                        pnom_tipoliq.Value = pEntidad.descripcion;
                        pnom_tipoliq.Direction = ParameterDirection.Input;

                        DbParameter pcod_tipocuo = cmdTransaccionFactory.CreateParameter();
                        pcod_tipocuo.ParameterName = "ptipocuota";
                        pcod_tipocuo.Value = pEntidad.tipocuota;
                        pcod_tipocuo.Direction = ParameterDirection.Input;
                        

                        DbParameter pcod_tipoamortizacion = cmdTransaccionFactory.CreateParameter();
                        pcod_tipoamortizacion.ParameterName = "ptipoamortizacion";
                        pcod_tipoamortizacion.Value = pEntidad.tipoamortizacion;
                        pcod_tipoamortizacion.Direction = ParameterDirection.Input;

                        DbParameter pcod_tipoint = cmdTransaccionFactory.CreateParameter();
                        pcod_tipoint.ParameterName = "ptipointeres";
                        pcod_tipoint.Value = pEntidad.tipointeres;
                        pcod_tipoint.Direction = ParameterDirection.Input;

                        DbParameter pcod_tipopago = cmdTransaccionFactory.CreateParameter();
                        pcod_tipopago.ParameterName = "ptipopago";
                        pcod_tipopago.Value = pEntidad.tipopago;
                        pcod_tipopago.Direction = ParameterDirection.Input;

                        DbParameter pcod_interajuste = cmdTransaccionFactory.CreateParameter();
                        pcod_interajuste.ParameterName = "pcobrointeresajuste";
                        pcod_interajuste.Value = pEntidad.cobrointeresajuste;
                        pcod_interajuste.Direction = ParameterDirection.Input;

                        DbParameter pcod_tipocuotaextra = cmdTransaccionFactory.CreateParameter();
                        pcod_tipocuotaextra.ParameterName = "ptipocuotasextras";
                        pcod_tipocuotaextra.Value = pEntidad.tipocuotasextras;
                        pcod_tipocuotaextra.Direction = ParameterDirection.Input;

                        DbParameter pcod_tipointextra = cmdTransaccionFactory.CreateParameter();
                        pcod_tipointextra.ParameterName = "ptipointeresextras";
                        pcod_tipointextra.Value = pEntidad.tipointeresextras;
                        pcod_tipointextra.Direction = ParameterDirection.Input;

                        DbParameter pcod_tipopagoextra = cmdTransaccionFactory.CreateParameter();
                        pcod_tipopagoextra.ParameterName = "ptipopagoextras";
                        pcod_tipopagoextra.Value = pEntidad.tipopagosextras;
                        pcod_tipopagoextra.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcod_tipoliq);
                        cmdTransaccionFactory.Parameters.Add(pnom_tipoliq);
                        cmdTransaccionFactory.Parameters.Add(pcod_tipocuo);
                        cmdTransaccionFactory.Parameters.Add(pcod_tipoamortizacion);
                        cmdTransaccionFactory.Parameters.Add(pcod_tipoint);
                        cmdTransaccionFactory.Parameters.Add(pcod_tipopago);
                        cmdTransaccionFactory.Parameters.Add(pcod_interajuste);
                        cmdTransaccionFactory.Parameters.Add(pcod_tipocuotaextra);
                        cmdTransaccionFactory.Parameters.Add(pcod_tipointextra);
                        cmdTransaccionFactory.Parameters.Add(pcod_tipopagoextra);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_TIPOLIQUIDACION_C";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pEntidad, "OBTIPOLIQUIDACION", pUsuario, Accion.Crear.ToString(), TipoAuditoria.CajaFinanciera, "Crear OBTIPOLIQUIDACION");
                            //DAauditoria.InsertarLog(pEntidad, pUsuario, Convert.ToInt64(pEntidad.codtipoliquidacion), "OBTIPOLIQUIDACION", Accion.Crear.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoLiquidacionData", "CrearTipoLiq", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica una entidad TipoLiquidacion en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad TipoLiquidacion</param>
        /// <returns>Entidad modificada</returns>
        public TipoLiquidacion ModificarTipoLiq(TipoLiquidacion pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_tipoliq = cmdTransaccionFactory.CreateParameter();
                        pcod_tipoliq.ParameterName = "pcodigotipoliquidacion";
                        pcod_tipoliq.Value = pEntidad.codtipoliquidacion;
                        pcod_tipoliq.Direction = ParameterDirection.InputOutput;

                        DbParameter pnom_tipoliq = cmdTransaccionFactory.CreateParameter();
                        pnom_tipoliq.ParameterName = "pnombretipoliq";
                        pnom_tipoliq.Value = pEntidad.descripcion;
                        pnom_tipoliq.Direction = ParameterDirection.Input;

                        DbParameter pcod_tipocuo = cmdTransaccionFactory.CreateParameter();
                        pcod_tipocuo.ParameterName = "ptipocuota";
                        pcod_tipocuo.Value = pEntidad.tipocuota;
                        pcod_tipocuo.Direction = ParameterDirection.Input;


                        DbParameter pcod_tipoamortizacion = cmdTransaccionFactory.CreateParameter();
                        pcod_tipoamortizacion.ParameterName = "ptipoamortizacion";
                        pcod_tipoamortizacion.Value = pEntidad.tipoamortizacion;
                        pcod_tipoamortizacion.Direction = ParameterDirection.Input;

                        DbParameter pcod_tipoint = cmdTransaccionFactory.CreateParameter();
                        pcod_tipoint.ParameterName = "ptipointeres";
                        pcod_tipoint.Value = pEntidad.tipointeres;
                        pcod_tipoint.Direction = ParameterDirection.Input;

                        DbParameter pcod_tipopago = cmdTransaccionFactory.CreateParameter();
                        pcod_tipopago.ParameterName = "ptipopago";
                        pcod_tipopago.Value = pEntidad.tipopago;
                        pcod_tipopago.Direction = ParameterDirection.Input;

                        DbParameter pcod_interajuste = cmdTransaccionFactory.CreateParameter();
                        pcod_interajuste.ParameterName = "pcobrointeresajuste";
                        pcod_interajuste.Value = pEntidad.cobrointeresajuste;
                        pcod_interajuste.Direction = ParameterDirection.Input;

                        DbParameter pcod_tipocuotaextra = cmdTransaccionFactory.CreateParameter();
                        pcod_tipocuotaextra.ParameterName = "ptipocuotasextras";
                        pcod_tipocuotaextra.Value = pEntidad.tipocuotasextras;
                        pcod_tipocuotaextra.Direction = ParameterDirection.Input;

                        DbParameter pcod_tipointextra = cmdTransaccionFactory.CreateParameter();
                        pcod_tipointextra.ParameterName = "ptipointeresextras";
                        pcod_tipointextra.Value = pEntidad.tipointeresextras;
                        pcod_tipointextra.Direction = ParameterDirection.Input;

                        DbParameter pcod_tipopagoextra = cmdTransaccionFactory.CreateParameter();
                        pcod_tipopagoextra.ParameterName = "ptipopagoextras";
                        pcod_tipopagoextra.Value = pEntidad.tipointeresextras;
                        pcod_tipopagoextra.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcod_tipoliq);
                        cmdTransaccionFactory.Parameters.Add(pnom_tipoliq);
                        cmdTransaccionFactory.Parameters.Add(pcod_tipocuo);
                        cmdTransaccionFactory.Parameters.Add(pcod_tipoamortizacion);
                        cmdTransaccionFactory.Parameters.Add(pcod_tipoint);
                        cmdTransaccionFactory.Parameters.Add(pcod_tipopago);
                        cmdTransaccionFactory.Parameters.Add(pcod_interajuste);
                        cmdTransaccionFactory.Parameters.Add(pcod_tipocuotaextra);
                        cmdTransaccionFactory.Parameters.Add(pcod_tipointextra);
                        cmdTransaccionFactory.Parameters.Add(pcod_tipopagoextra);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_TIPOLIQUIDACION_U";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pEntidad, "OBTIPOLIQUIDACION", pUsuario, Accion.Modificar.ToString(), TipoAuditoria.CajaFinanciera, "Modificar OBTIPOLIQUIDACION");
                            //DAauditoria.InsertarLog(pEntidad, pUsuario, Convert.ToInt64(pEntidad.codtipoliquidacion), "OBTIPOLIQUIDACION", Accion.Modificar.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoLiquidacionData", "ModificarTipoLiq", ex);
                        return null;
                    }

                }

            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TIPOLIQUIDACION dados unos filtros
        /// </summary>
        /// <param name="pTIPOLIQUIDACION">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Tipo Liquidacion obtenidos</returns>
        public List<TipoLiquidacion> ListarTipoLiquidacion(TipoLiquidacion pSolicitud, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoLiquidacion> lstTipoLiq = new List<TipoLiquidacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT CODTIPOLIQUIDACION,NOMBRE,(decode(TIPOCUOTA,1,'Pago Unico',decode(TIPOCUOTA,2,'Serie Uniforme',decode(TIPOCUOTA,3,'Gradiente')))) TIPO_CUOTA, " +
                                     " decode(TIPOPAGO,1,'Anticipado','Vencido') TIPO_PAGO " +
                                     " FROM  OBTIPOLIQUIDACION  ORDER BY CODTIPOLIQUIDACION ASC ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoLiquidacion entidad = new TipoLiquidacion();

                            if (resultado["CODTIPOLIQUIDACION"] != DBNull.Value) entidad.codtipoliquidacion = Convert.ToInt64(resultado["CODTIPOLIQUIDACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_CUOTA"] != DBNull.Value) entidad.nomtipocuota = Convert.ToString(resultado["TIPO_CUOTA"]);
                            if (resultado["TIPO_PAGO"] != DBNull.Value) entidad.nomtipopago = Convert.ToString(resultado["TIPO_PAGO"]);

                            lstTipoLiq.Add(entidad);
                        }

                        return lstTipoLiq;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoLiquidacionData", "ListarTipoLiquidacion", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina una Tipo de Liquidacion en la base de datos
        /// </summary>
        /// <param name="pId">identificador de la Tipo de Liquidacion</param>
        public void EliminarTipoLiq(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        LineaObligacion pEntidad = new LineaObligacion();


                        DbParameter pcod_tipoliq = cmdTransaccionFactory.CreateParameter();
                        pcod_tipoliq.ParameterName = "pcodtipoliquidacion";
                        pcod_tipoliq.Value = pId;
                        pcod_tipoliq.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcod_tipoliq);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_TIPOLIQUIDACION_D";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pEntidad, "OBTIPOLIQUIDACION", pUsuario, Accion.Eliminar.ToString(), TipoAuditoria.CajaFinanciera, "Eliminar OBTIPOLIQUIDACION");
                            //DAauditoria.InsertarLog(pEntidad, pUsuario, Convert.ToInt64(pId), "OBTIPOLIQUIDACION", Accion.Eliminar.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoLiquidacionData", "EliminarLineaOb", ex);
                    }

                }
            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla TipoLiquidacion de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>TipoLiquidacion consultada</returns>
        public TipoLiquidacion ConsultarTipoLiq(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            TipoLiquidacion entidad = new TipoLiquidacion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM OBTIPOLIQUIDACION where codtipoliquidacion=" + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["codtipoliquidacion"] != DBNull.Value) entidad.codtipoliquidacion = Convert.ToInt64(resultado["codtipoliquidacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["nombre"]);
                            if (resultado["tipopago"] != DBNull.Value) entidad.tipopago = Convert.ToInt64(resultado["tipopago"]);
                            if (resultado["tipoamortizacion"] != DBNull.Value) entidad.tipoamortizacion = Convert.ToInt64(resultado["tipoamortizacion"]);
                            if (resultado["tipocuota"] != DBNull.Value) entidad.tipocuota = Convert.ToInt64(resultado["tipocuota"]);
                            if (resultado["tipointeres"] != DBNull.Value) entidad.tipointeres = Convert.ToInt64(resultado["tipointeres"]);
                            if (resultado["cobrointeresajuste"] != DBNull.Value) entidad.cobrointeresajuste = Convert.ToInt64(resultado["cobrointeresajuste"]);
                            if (resultado["tipocuotasextras"] != DBNull.Value) entidad.tipocuotasextras = Convert.ToInt64(resultado["tipocuotasextras"]);
                            if (resultado["tipointeresextras"] != DBNull.Value) entidad.tipointeresextras = Convert.ToInt64(resultado["tipointeresextras"]);
                            if (resultado["tipopagoextras"] != DBNull.Value) entidad.tipointeresextras = Convert.ToInt64(resultado["tipopagoextras"]);

                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoLiquidacionData", "ConsultarLineaOb", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene el conteo de Tipos de liquidacion asociados a la linea de obligacion 
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>TipoLiquidacion consultada</returns>
        public TipoLiquidacion ConsultarTipoLiquidacionXLineaObligacion(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            TipoLiquidacion entidad = new TipoLiquidacion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT count(*) conteo FROM OBLINEAOBLIGACION where tipoliquidacion=" + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["conteo"] != DBNull.Value) entidad.conteo = Convert.ToInt64(resultado["conteo"]);
                        }
                        else
                        {
                            entidad.conteo = 0;
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoLiquidacionData", "ConsultarTipoLiquidacionXLineaObligacion", ex);
                        return null;
                    }
                }
            }
        }

    }
}
