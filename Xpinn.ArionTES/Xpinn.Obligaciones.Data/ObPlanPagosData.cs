using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Obligaciones.Entities;

namespace Xpinn.Obligaciones.Data
{
    public class ObPlanPagosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public ObPlanPagosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene la informacion de los creditos solicitados
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Datos del credito solicitado</returns>
        public List<ObPlanPagos> ListarObPlanPagos(ObPlanPagos pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ObPlanPagos> lstPlanPagos = new List<ObPlanPagos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pcod_obligacion = cmdTransaccionFactory.CreateParameter();
                        pcod_obligacion.ParameterName = "pcodigoobligacion";
                        pcod_obligacion.Value = pEntidad.cod_obligacion;
                        pcod_obligacion.Direction = ParameterDirection.Input;

                        DbParameter ptasa_efectiva = cmdTransaccionFactory.CreateParameter();
                        ptasa_efectiva.ParameterName = "ptasaefectiva";
                        ptasa_efectiva.Value = pEntidad.tasa_efectiva;
                        ptasa_efectiva.Direction = ParameterDirection.Input;

                        DbParameter ptasa_per = cmdTransaccionFactory.CreateParameter();
                        ptasa_per.ParameterName = "ptasaper";
                        ptasa_per.Value = pEntidad.tasa_per;
                        ptasa_per.Direction = ParameterDirection.Input;

                        DbParameter pcuota = cmdTransaccionFactory.CreateParameter();
                        pcuota.ParameterName = "pcuota";
                        pcuota.Value = pEntidad.cuota;
                        pcuota.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcod_obligacion);
                        cmdTransaccionFactory.Parameters.Add(ptasa_efectiva);
                        cmdTransaccionFactory.Parameters.Add(ptasa_per);
                        cmdTransaccionFactory.Parameters.Add(pcuota);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_LIQUIDAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObPlanPagosData", "USP_XPINN_OB_LIQUIDAR", ex);
                        return null;
                    }
                };

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = " Select numcuota, fechacuota, capital, int_corriente, int_mora, seguro, otros, cuotaextra, cuotanormal, saldo, cuotatotal from VObligacionesDetalle Where codobligacion = " + pEntidad.cod_obligacion.ToString() + " order by numcuota asc ";
                        
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            pEntidad = new ObPlanPagos();
                            // Asociar todos los valores a la entidad
                            if (resultado["numcuota"] != DBNull.Value) pEntidad.nrocuota = Convert.ToInt64(resultado["numcuota"]);
                            if (resultado["fechacuota"] != DBNull.Value) pEntidad.fecha = Convert.ToDateTime(resultado["fechacuota"]);
                            if (resultado["capital"] != DBNull.Value) pEntidad.amort_cap = Convert.ToInt64(resultado["capital"]);
                            if (resultado["int_corriente"] != DBNull.Value) pEntidad.interes_corriente = Convert.ToInt64(resultado["int_corriente"]);
                            if (resultado["int_mora"] != DBNull.Value) pEntidad.interes_mora = Convert.ToInt64(resultado["int_mora"]);
                            if (resultado["seguro"] != DBNull.Value) pEntidad.seguro = Convert.ToInt64(resultado["seguro"]);
                            if (resultado["otros"] != DBNull.Value) pEntidad.otros = Convert.ToInt64(resultado["otros"]);
                            if (resultado["cuotaextra"] != DBNull.Value) pEntidad.cuotaextra = Convert.ToInt64(resultado["cuotaextra"]);
                            if (resultado["cuotanormal"] != DBNull.Value) pEntidad.cuotanormal = Convert.ToInt64(resultado["cuotanormal"]);
                            if (resultado["saldo"] != DBNull.Value) pEntidad.saldo = Convert.ToInt64(resultado["saldo"]);
                            if (resultado["cuotatotal"] != DBNull.Value) pEntidad.cuotatotal = Convert.ToInt64(resultado["cuotatotal"]);

                            lstPlanPagos.Add(pEntidad);
                        }

                        return lstPlanPagos; 
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObPlanPagosData", "ListarObPlanPagos", ex);
                        return null;
                    }
                }

               
            }
        }


        public List<ObPlanPagos> ConsultarObPlanPagos(ObPlanPagos pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ObPlanPagos> lstPlanPagos = new List<ObPlanPagos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = " Select numcuota, fechacuota, capital, int_corriente, int_mora, seguro, otros, cuotaextra, cuotanormal, saldo, cuotatotal from VObligacionesDetalle Where codobligacion = " + pEntidad.cod_obligacion.ToString() + " order by numcuota asc ";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            pEntidad = new ObPlanPagos();
                            // Asociar todos los valores a la entidad
                            if (resultado["numcuota"] != DBNull.Value) pEntidad.nrocuota = Convert.ToInt64(resultado["numcuota"]);
                            if (resultado["fechacuota"] != DBNull.Value) pEntidad.fecha = Convert.ToDateTime(resultado["fechacuota"]);
                            if (resultado["capital"] != DBNull.Value) pEntidad.amort_cap = Convert.ToInt64(resultado["capital"]);
                            if (resultado["int_corriente"] != DBNull.Value) pEntidad.interes_corriente = Convert.ToInt64(resultado["int_corriente"]);
                            if (resultado["int_mora"] != DBNull.Value) pEntidad.interes_mora = Convert.ToInt64(resultado["int_mora"]);
                            if (resultado["seguro"] != DBNull.Value) pEntidad.seguro = Convert.ToInt64(resultado["seguro"]);
                            if (resultado["otros"] != DBNull.Value) pEntidad.otros = Convert.ToInt64(resultado["otros"]);
                            if (resultado["cuotaextra"] != DBNull.Value) pEntidad.cuotaextra = Convert.ToInt64(resultado["cuotaextra"]);
                            if (resultado["cuotanormal"] != DBNull.Value) pEntidad.cuotanormal = Convert.ToInt64(resultado["cuotanormal"]);
                            if (resultado["saldo"] != DBNull.Value) pEntidad.saldo = Convert.ToInt64(resultado["saldo"]);
                            if (resultado["cuotatotal"] != DBNull.Value) pEntidad.cuotatotal = Convert.ToInt64(resultado["cuotatotal"]);

                            lstPlanPagos.Add(pEntidad);
                        }

                        return lstPlanPagos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObPlanPagosData", "ConsultarObPlanPagos", ex);
                        return null;
                    }
                }


            }
        }


        /// <summary>
        /// Obtiene la informacion de los creditos solicitados
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Datos del credito solicitado</returns>
        public ObPlanPagos ConsultarObcomponente(Int64 numero_radicacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            ObPlanPagos pEntidad = new ObPlanPagos();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();
                             
                 

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = " select * from V_OBCREDITOTASA  where  codobligacion=" + numero_radicacion.ToString();

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                          
                            // Asociar todos los valores a la entidad
                            if (resultado["codobligacion"] != DBNull.Value) pEntidad.cod_obligacion = Convert.ToInt64(resultado["codobligacion"]);
                            if (resultado["codcomponente"] != DBNull.Value) pEntidad.componente = Convert.ToInt64(resultado["codcomponente"]);
                            if (resultado["calculocomponente"] != DBNull.Value) pEntidad.calculocomponente = Convert.ToInt64(resultado["calculocomponente"]);
                            if (resultado["tipo_historico"] != DBNull.Value) pEntidad.tipo_historico = Convert.ToInt64(resultado["tipo_historico"]);
                            if (resultado["cod_tipo_tasa"] != DBNull.Value) pEntidad.tipo_tasa = Convert.ToString(resultado["cod_tipo_tasa"]);
                            if (resultado["tasa"] != DBNull.Value) pEntidad.tasa = Convert.ToDecimal(resultado["tasa"]);
                            if (resultado["spread"] != DBNull.Value) pEntidad.spread = Convert.ToInt64(resultado["spread"]);
                            if (resultado["valor_cuota"] != DBNull.Value) pEntidad.cuota = Convert.ToInt64(resultado["valor_cuota"]);
                            if (resultado["tipo_tasa"] != DBNull.Value) pEntidad.tipo_tasa = Convert.ToString(resultado["tipo_tasa"]);
                        
                      }

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObPlanPagosData", "ConsultarObcomponente", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene la informacion de los creditos solicitados
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Datos del credito solicitado</returns>
        public List<ObPlanPagos> ListarObPlanRegistroPagos(Int64 pId, DateTime pFechaProxPago, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ObPlanPagos> lstPlanPagos = new List<ObPlanPagos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        
                        string opt="";
                        string fech = pFechaProxPago.ToShortDateString();

                        if (fech != "1/01/1900" && fech != "" && fech != "01/01/1900")
                        {
                            opt = " and to_char(fechacuota,'dd/MM/yyyy') = '" + fech + "' ";
                        }                           

                        string sql = " Select * from VObligacionesDetalle " +
                           " where codobligacion=" + pId.ToString() + opt + " Order by numcuota";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ObPlanPagos pEntidad = new ObPlanPagos();
                            // Asociar todos los valores a la entidad
                            if (resultado["codobligacion"] != DBNull.Value) pEntidad.cod_obligacion = Convert.ToInt64(resultado["codobligacion"]);
                            if (resultado["numcuota"] != DBNull.Value) pEntidad.nrocuota = Convert.ToInt64(resultado["numcuota"]);
                            if (resultado["fechacuota"] != DBNull.Value) pEntidad.fecha = Convert.ToDateTime(resultado["fechacuota"]);
                            if (resultado["capital"] != DBNull.Value) pEntidad.amort_cap = Convert.ToInt64(resultado["capital"]);
                            if (resultado["int_corriente"] != DBNull.Value) pEntidad.interes_corriente = Convert.ToInt64(resultado["int_corriente"]);
                            if (resultado["int_mora"] != DBNull.Value) pEntidad.interes_mora = Convert.ToInt64(resultado["int_mora"]);
                            if (resultado["seguro"] != DBNull.Value) pEntidad.seguro = Convert.ToInt64(resultado["seguro"]);
                            if (resultado["saldo"] != DBNull.Value) pEntidad.saldo = Convert.ToInt64(resultado["saldo"]);

                            lstPlanPagos.Add(pEntidad);
                        }

                        return lstPlanPagos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObPlanPagosData", "ListarObPlanRegistroPagos", ex);
                        return null;
                    }
                }
            }
        }

        // <summary>
        /// Crea un registro en la tabla PLAN PAGOS de la base de datos
        /// </summary>
        /// <param name="pConsignacion">Entidad PlanPagos</param>
        /// <returns>Entidad PlanPagos creada</returns>
        public ObPlanPagos ModificarPlanPagos(ObPlanPagos pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.Parameters.Clear();
                        DbParameter pn_cod_obligacion = cmdTransaccionFactory.CreateParameter();
                        pn_cod_obligacion.ParameterName = "pn_cod_obligacion";
                        pn_cod_obligacion.Value = pEntidad.cod_obligacion;

                        DbParameter pn_nro_cuota = cmdTransaccionFactory.CreateParameter();
                        pn_nro_cuota.ParameterName = "pn_nro_cuota";
                        pn_nro_cuota.Value = pEntidad.nrocuota;

                        DbParameter pn_fecha_cuota = cmdTransaccionFactory.CreateParameter();
                        pn_fecha_cuota.ParameterName = "pn_fecha_cuota";
                        pn_fecha_cuota.DbType = DbType.Date;
                        pn_fecha_cuota.Value = pEntidad.fecha;

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
                        cmdTransaccionFactory.Parameters.Add(pn_nro_cuota);
                        cmdTransaccionFactory.Parameters.Add(pn_fecha_cuota);
                        cmdTransaccionFactory.Parameters.Add(pn_valor_capital);
                        cmdTransaccionFactory.Parameters.Add(pn_valor_int_corr);
                        cmdTransaccionFactory.Parameters.Add(pn_valor_int_mora);
                        cmdTransaccionFactory.Parameters.Add(pn_valor_seguro);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_usu);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_PLANPAGOS_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObPlanPagosData", "ModificarPlanPagos", ex);
                        return null;
                    }
                }

            }
        }


        /// <summary>
        /// Elimina un Componenteadicional en la base de datos
        /// </summary>
        /// <param name="pId">identificador de Componenteadicional</param>
        public void EliminarComponenteadicional(Int64 pId,Int64 codcomponente, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ObPlanPagos pEntidad = new ObPlanPagos();

                        DbParameter p_componente= cmdTransaccionFactory.CreateParameter();
                        p_componente.ParameterName = "p_componente";
                        p_componente.Value = pId;
                        p_componente.DbType = DbType.Int64;
                        p_componente.Size = 8;
                        p_componente.Direction = ParameterDirection.Input;

                        DbParameter p_codobligacion= cmdTransaccionFactory.CreateParameter();
                        p_codobligacion.ParameterName = "p_codobligacion";
                        p_codobligacion.Value = codcomponente;
                        p_codobligacion.DbType = DbType.Int64;
                        p_codobligacion.Size = 8;
                        p_codobligacion.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_codobligacion);
                        cmdTransaccionFactory.Parameters.Add(p_componente);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_COM_ADI_D";
                        cmdTransaccionFactory.ExecuteNonQuery();

                   }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ObPlanPagosData", "EliminarComponenteadicional", ex);
                    }
                }

            }
        }





    }
}
