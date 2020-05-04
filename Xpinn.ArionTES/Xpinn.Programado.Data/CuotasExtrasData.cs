using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Programado.Entities;

namespace Xpinn.Programado.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla ProgramadoCuotasExtras
    /// </summary>
    public class CuotasExtrasData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ProgramadoCuotasExtras
        /// </summary>
        public CuotasExtrasData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla CUOTAS_EXT_AHO_PROGRAMADO de la base de datos
        /// </summary>
        /// <param name="pCuotasExtras">Entidad CUOTAS_EXT_AHO_PROGRAMADO</param>
        /// <returns>Entidad CUOTAS_EXT_AHO_PROGRAMADO creada</returns>
        public ProgramadoCuotasExtras CrearCuotasExtras(ProgramadoCuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_CUOTA = cmdTransaccionFactory.CreateParameter();
                        pCOD_CUOTA.ParameterName = "P_COD_CUOTA";
                        pCOD_CUOTA.Value = pCuotasExtras.cod_cuota;
                        pCOD_CUOTA.Direction = ParameterDirection.InputOutput;

                        DbParameter pNUMERO_PROGRAMADO = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_PROGRAMADO.ParameterName = "P_NUMERO_PROGRAMADO";
                        pNUMERO_PROGRAMADO.Value = pCuotasExtras.numero_programado;

                        DbParameter pFECHA_PAGO = cmdTransaccionFactory.CreateParameter();
                        pFECHA_PAGO.ParameterName = "P_FECHA_PAGO";
                        pFECHA_PAGO.Value = pCuotasExtras.fecha_pago;

                        DbParameter pVALOR = cmdTransaccionFactory.CreateParameter();
                        pVALOR.ParameterName = "P_VALOR";
                        pVALOR.Value = pCuotasExtras.valor;

                        DbParameter pVALOR_CAPITAL = cmdTransaccionFactory.CreateParameter();
                        pVALOR_CAPITAL.ParameterName = "P_VALOR_CAPITAL";
                        pVALOR_CAPITAL.Value = pCuotasExtras.valor_capital;
                       
                        DbParameter pSALDO_CAPITAL = cmdTransaccionFactory.CreateParameter();
                        pSALDO_CAPITAL.ParameterName = "P_SALDO_CAPITAL";
                        pSALDO_CAPITAL.Value = pCuotasExtras.saldo_capital;




                        cmdTransaccionFactory.Parameters.Add(pCOD_CUOTA);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_PROGRAMADO);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_PAGO);
                        cmdTransaccionFactory.Parameters.Add(pVALOR);
                        cmdTransaccionFactory.Parameters.Add(pVALOR_CAPITAL);
                        cmdTransaccionFactory.Parameters.Add(pSALDO_CAPITAL);
                     


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_CUOEX_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pCuotasExtras, "ProgramadoCuotasExtras",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pCuotasExtras.cod_cuota = Convert.ToInt64(pCOD_CUOTA.Value);
                        return pCuotasExtras;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProgramadoCuotasExtrasData", "CrearCuotasExtras", ex);
                        return null;
                    }
                }
            }
        }




        /// <summary>
        /// Obtiene un registro en la tabla CUOTAS_EXT_AHO_PROGRAMADO de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla CUOTAS_EXT_AHO_PROGRAMADO</param>
        /// <returns>Entidad CUOTAS_EXT_AHO_PROGRAMADO consultado</returns>
        public ProgramadoCuotasExtras ConsultarCuotasExtras(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            ProgramadoCuotasExtras entidad = new ProgramadoCuotasExtras();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  CUOTAS_EXT_AHO_PROGRAMADO WHERE COD_CUOTA = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_CUOTA"] != DBNull.Value) entidad.cod_cuota = Convert.ToInt64(resultado["COD_CUOTA"]);
                            if (resultado["NUMERO_PROGRAMADO"] != DBNull.Value) entidad.numero_programado = Convert.ToString(resultado["NUMERO_PROGRAMADO"]);
                            if (resultado["FECHA_PAGO"] != DBNull.Value) entidad.fecha_pago = Convert.ToDateTime(resultado["FECHA_PAGO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["VALOR_CAPITAL"] != DBNull.Value) entidad.valor_capital = Convert.ToInt64(resultado["VALOR_CAPITAL"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["SALDO_CAPITAL"]);

                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch(Exception ex)
                    {
                        BOExcepcion.Throw("ProgramadoCuotasExtrasData", "ConsultarCuotasExtras", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla CUOTAS_EXT_AHO_PROGRAMADO dados unos filtros
        /// </summary>
        /// <param name="pCuotasExtras">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CUOTAS_EXT_AHO_PROGRAMADO  obtenidos</returns>
        public List<ProgramadoCuotasExtras> ListarCuotasExtras(ProgramadoCuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ProgramadoCuotasExtras> lstCuotasExtras = new List<ProgramadoCuotasExtras>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                       string sql = "SELECT *  FROM CUOTAS_EXT_AHO_PROGRAMADO WHERE 1=1  " + ObtenerFiltro(pCuotasExtras);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ProgramadoCuotasExtras entidad = new ProgramadoCuotasExtras();

                            if (resultado["COD_CUOTA"] != DBNull.Value) entidad.cod_cuota = Convert.ToInt64(resultado["COD_CUOTA"]);
                            if (resultado["NUMERO_PROGRAMADO"] != DBNull.Value) entidad.numero_programado = Convert.ToString(resultado["NUMERO_PROGRAMADO"]);
                            if (resultado["FECHA_PAGO"] != DBNull.Value) entidad.fecha_pago = Convert.ToDateTime(resultado["FECHA_PAGO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["VALOR_CAPITAL"] != DBNull.Value) entidad.valor_capital = Convert.ToInt64(resultado["VALOR_CAPITAL"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                           
                            lstCuotasExtras.Add(entidad);
                        }

                        return lstCuotasExtras;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProgramadoCuotasExtrasData", "ListarCuotasExtras", ex);
                        return null;
                    }
                }
            }
        }

       
    }
}