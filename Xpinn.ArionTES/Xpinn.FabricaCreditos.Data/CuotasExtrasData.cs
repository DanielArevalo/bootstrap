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
    /// Objeto de acceso a datos para la tabla CuotasExtras
    /// </summary>
    public class CuotasExtrasData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla CuotasExtras
        /// </summary>
        public CuotasExtrasData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla CuotasExtras de la base de datos
        /// </summary>
        /// <param name="pCuotasExtras">Entidad CuotasExtras</param>
        /// <returns>Entidad CuotasExtras creada</returns>
        public CuotasExtras CrearCuotasExtras(CuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_CUOTA = cmdTransaccionFactory.CreateParameter();
                        pCOD_CUOTA.ParameterName = "p_COD_CUOTA";
                        pCOD_CUOTA.Value = pCuotasExtras.cod_cuota;
                        pCOD_CUOTA.Direction = ParameterDirection.InputOutput;

                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "p_NUMERO_RADICACION";
                        pNUMERO_RADICACION.Value = pCuotasExtras.numero_radicacion;

                        DbParameter pFECHA_PAGO = cmdTransaccionFactory.CreateParameter();
                        pFECHA_PAGO.ParameterName = "p_FECHA_PAGO";
                        pFECHA_PAGO.Value = pCuotasExtras.fecha_pago;

                        DbParameter pVALOR = cmdTransaccionFactory.CreateParameter();
                        pVALOR.ParameterName = "p_VALOR";
                        pVALOR.Value = pCuotasExtras.valor;

                        DbParameter pVALOR_CAPITAL = cmdTransaccionFactory.CreateParameter();
                        pVALOR_CAPITAL.ParameterName = "p_VALOR_CAPITAL";
                        pVALOR_CAPITAL.Value = pCuotasExtras.valor_capital;

                        DbParameter pVALOR_INTERES = cmdTransaccionFactory.CreateParameter();
                        pVALOR_INTERES.ParameterName = "p_VALOR_INTERES";
                        pVALOR_INTERES.Value = pCuotasExtras.valor_interes;

                        DbParameter pSALDO_CAPITAL = cmdTransaccionFactory.CreateParameter();
                        pSALDO_CAPITAL.ParameterName = "p_SALDO_CAPITAL";
                        pSALDO_CAPITAL.Value = pCuotasExtras.saldo_capital;

                        DbParameter pSALDO_INTERES = cmdTransaccionFactory.CreateParameter();
                        pSALDO_INTERES.ParameterName = "p_SALDO_INTERES";
                        pSALDO_INTERES.Value = pCuotasExtras.saldo_interes;

                        DbParameter pFORMA_PAGO = cmdTransaccionFactory.CreateParameter();
                        pFORMA_PAGO.ParameterName = "p_FORMA_PAGO";
                        pFORMA_PAGO.Value = pCuotasExtras.forma_pago;

                        DbParameter pTIPO_CUOTA = cmdTransaccionFactory.CreateParameter();
                        pTIPO_CUOTA.ParameterName = "p_TIPO_CUOTA";
                        if (pCuotasExtras.tipo_cuota == Int32.MinValue || pCuotasExtras.tipo_cuota == 0)
                            pTIPO_CUOTA.Value = DBNull.Value;
                        else
                            pTIPO_CUOTA.Value = pCuotasExtras.tipo_cuota;


                        cmdTransaccionFactory.Parameters.Add(pCOD_CUOTA);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_PAGO);
                        cmdTransaccionFactory.Parameters.Add(pVALOR);
                        cmdTransaccionFactory.Parameters.Add(pVALOR_CAPITAL);
                        cmdTransaccionFactory.Parameters.Add(pVALOR_INTERES);
                        cmdTransaccionFactory.Parameters.Add(pSALDO_CAPITAL);
                        cmdTransaccionFactory.Parameters.Add(pSALDO_INTERES);
                        cmdTransaccionFactory.Parameters.Add(pFORMA_PAGO);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_CUOTA);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_CUOEX_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pCuotasExtras, "CuotasExtras",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pCuotasExtras.cod_cuota = Convert.ToInt64(pCOD_CUOTA.Value);
                        return pCuotasExtras;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuotasExtrasData", "CrearCuotasExtras", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Crea un registro en la tabla CuotasExtras de la base de datos
        /// </summary>
        /// <param name="pCuotasExtras">Entidad CuotasExtras</param>
        /// <returns>Entidad CuotasExtras creada</returns>
        public CuotasExtras CrearSolicitudCuotasExtras(CuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDCUOTAEXTRA = cmdTransaccionFactory.CreateParameter();
                        pIDCUOTAEXTRA.ParameterName = "p_IDCUOTAEXTRA";
                        pIDCUOTAEXTRA.Value = pCuotasExtras.cod_cuota;
                        pIDCUOTAEXTRA.Direction = ParameterDirection.Output;
                        pIDCUOTAEXTRA.DbType = DbType.Int64;


                        DbParameter pNUMEROSOLICITUD = cmdTransaccionFactory.CreateParameter();
                        pNUMEROSOLICITUD.ParameterName = "p_NUMEROSOLICITUD";
                        pNUMEROSOLICITUD.Value = pCuotasExtras.numero_radicacion;
                        pNUMEROSOLICITUD.Direction = ParameterDirection.Input;
                        pNUMEROSOLICITUD.DbType = DbType.Int64;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = "p_FECHA";
                        pFECHA.Value = pCuotasExtras.fecha_pago;
                        pFECHA.Direction = ParameterDirection.Input;
                        pFECHA.DbType = DbType.DateTime;

                        DbParameter pVALOR = cmdTransaccionFactory.CreateParameter();
                        pVALOR.ParameterName = "p_VALOR";
                        pVALOR.Value = pCuotasExtras.valor;
                        pVALOR.Direction = ParameterDirection.Input;
                        pVALOR.DbType = DbType.Int64;

                        DbParameter pTIP_FOR_PAG = cmdTransaccionFactory.CreateParameter();
                        pTIP_FOR_PAG.ParameterName = "p_TIP_FOR_PAG";
                        pTIP_FOR_PAG.Value = pCuotasExtras.forma_pago;
                        pTIP_FOR_PAG.Direction = ParameterDirection.Input;
                        pTIP_FOR_PAG.DbType = DbType.Int64;

                        String[] cod_tipo = null;
                        if (pCuotasExtras.des_tipo_cuota != null)
                        {
                            cod_tipo = pCuotasExtras.des_tipo_cuota.Split('-');
                        }

                        DbParameter P_TIP_CUOTA = cmdTransaccionFactory.CreateParameter();
                        P_TIP_CUOTA.ParameterName = "P_TIP_CUOTA";
                        if (cod_tipo != null)
                            P_TIP_CUOTA.Value = cod_tipo[0] != null ? cod_tipo[0].ToString() : null;
                        else
                            P_TIP_CUOTA.Value = 1;
                        P_TIP_CUOTA.Direction = ParameterDirection.Input;
                        P_TIP_CUOTA.DbType = DbType.Int64;

                        cmdTransaccionFactory.Parameters.Add(pIDCUOTAEXTRA);
                        cmdTransaccionFactory.Parameters.Add(pNUMEROSOLICITUD);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pVALOR);
                        cmdTransaccionFactory.Parameters.Add(pTIP_FOR_PAG);
                        cmdTransaccionFactory.Parameters.Add(P_TIP_CUOTA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CUOEX_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pCuotasExtras, "CuotasExtras",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pCuotasExtras.cod_cuota = Convert.ToInt64(pIDCUOTAEXTRA.Value);
                        return pCuotasExtras;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuotasExtrasData", "CrearCuotasExtras", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla CuotasExtras de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad CuotasExtras modificada</returns>
        public CuotasExtras ModificarCuotasExtras(CuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_CUOTA = cmdTransaccionFactory.CreateParameter();
                        pCOD_CUOTA.ParameterName = "p_COD_CUOTA";
                        pCOD_CUOTA.Value = pCuotasExtras.cod_cuota;

                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "p_NUMERO_RADICACION";
                        pNUMERO_RADICACION.Value = pCuotasExtras.numero_radicacion;

                        DbParameter pFECHA_PAGO = cmdTransaccionFactory.CreateParameter();
                        pFECHA_PAGO.ParameterName = "p_FECHA_PAGO";
                        pFECHA_PAGO.Value = pCuotasExtras.fecha_pago;

                        DbParameter pVALOR = cmdTransaccionFactory.CreateParameter();
                        pVALOR.ParameterName = "p_VALOR";
                        pVALOR.Value = pCuotasExtras.valor;

                        DbParameter pVALOR_CAPITAL = cmdTransaccionFactory.CreateParameter();
                        pVALOR_CAPITAL.ParameterName = "p_VALOR_CAPITAL";
                        pVALOR_CAPITAL.Value = pCuotasExtras.valor_capital;

                        DbParameter pVALOR_INTERES = cmdTransaccionFactory.CreateParameter();
                        pVALOR_INTERES.ParameterName = "p_VALOR_INTERES";
                        pVALOR_INTERES.Value = pCuotasExtras.valor_interes;

                        DbParameter pSALDO_CAPITAL = cmdTransaccionFactory.CreateParameter();
                        pSALDO_CAPITAL.ParameterName = "p_SALDO_CAPITAL";
                        pSALDO_CAPITAL.Value = pCuotasExtras.saldo_capital;

                        DbParameter pSALDO_INTERES = cmdTransaccionFactory.CreateParameter();
                        pSALDO_INTERES.ParameterName = "p_SALDO_INTERES";
                        pSALDO_INTERES.Value = pCuotasExtras.saldo_interes;

                        DbParameter pFORMA_PAGO = cmdTransaccionFactory.CreateParameter();
                        pFORMA_PAGO.ParameterName = "p_FORMA_PAGO";
                        pFORMA_PAGO.Value = pCuotasExtras.forma_pago;

                        cmdTransaccionFactory.Parameters.Add(pCOD_CUOTA);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_PAGO);
                        cmdTransaccionFactory.Parameters.Add(pVALOR);
                        cmdTransaccionFactory.Parameters.Add(pVALOR_CAPITAL);
                        cmdTransaccionFactory.Parameters.Add(pVALOR_INTERES);
                        cmdTransaccionFactory.Parameters.Add(pSALDO_CAPITAL);
                        cmdTransaccionFactory.Parameters.Add(pSALDO_INTERES);
                        cmdTransaccionFactory.Parameters.Add(pFORMA_PAGO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_CUOEX_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pCuotasExtras, "CuotasExtras",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pCuotasExtras;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuotasExtrasData", "ModificarCuotasExtras", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla CuotasExtras de la base de datos
        /// </summary>
        /// <param name="pId">identificador de CuotasExtras</param>
        public void EliminarCuotasExtras(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        CuotasExtras pCuotasExtras = new CuotasExtras();

                        //if (pUsuario.programaGeneraLog)
                        //    pCuotasExtras = ConsultarCuotasExtras(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_CUOTA = cmdTransaccionFactory.CreateParameter();
                        pCOD_CUOTA.ParameterName = "p_COD_CUOTA";
                        pCOD_CUOTA.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_CUOTA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_CUOEX_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pCuotasExtras, "CuotasExtras", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuotasExtrasData", "EliminarCuotasExtras", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla CuotasExtras de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla CuotasExtras</param>
        /// <returns>Entidad CuotasExtras consultado</returns>
        public CuotasExtras ConsultarCuotasExtras(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            CuotasExtras entidad = new CuotasExtras();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  CUOTASEXTRAS WHERE COD_CUOTA = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_CUOTA"] != DBNull.Value) entidad.cod_cuota = Convert.ToInt64(resultado["COD_CUOTA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["FECHA_PAGO"] != DBNull.Value) entidad.fecha_pago = Convert.ToDateTime(resultado["FECHA_PAGO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["VALOR_CAPITAL"] != DBNull.Value) entidad.valor_capital = Convert.ToInt64(resultado["VALOR_CAPITAL"]);
                            if (resultado["VALOR_INTERES"] != DBNull.Value) entidad.valor_interes = Convert.ToInt64(resultado["VALOR_INTERES"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["SALDO_INTERES"] != DBNull.Value) entidad.saldo_interes = Convert.ToInt64(resultado["SALDO_INTERES"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuotasExtrasData", "ConsultarCuotasExtras", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla CuotasExtras dados unos filtros
        /// </summary>
        /// <param name="pCuotasExtras">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CuotasExtras obtenidos</returns>
        public List<CuotasExtras> ListarCuotasExtras(CuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CuotasExtras> lstCuotasExtras = new List<CuotasExtras>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT CUOTASEXTRAS.*, CASE CUOTASEXTRAS.FORMA_PAGO WHEN '1' THEN 'Caja' WHEN '2' then 'Nomina' end as nom_forma_pago,TIPO_CUOTAS_EXTRAS.IDTIPO||'-'||TIPO_CUOTAS_EXTRAS.DESCRIPCION as descripcion FROM CUOTASEXTRAS  LEFT JOIN TIPO_CUOTAS_EXTRAS ON  CUOTASEXTRAS.TIPO_CUOTA = TIPO_CUOTAS_EXTRAS.IDTIPO  " + ObtenerFiltro(pCuotasExtras);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CuotasExtras entidad = new CuotasExtras();

                            if (resultado["COD_CUOTA"] != DBNull.Value) entidad.cod_cuota = Convert.ToInt64(resultado["COD_CUOTA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["FECHA_PAGO"] != DBNull.Value) entidad.fecha_pago = Convert.ToDateTime(resultado["FECHA_PAGO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["VALOR_CAPITAL"] != DBNull.Value) entidad.valor_capital = Convert.ToInt64(resultado["VALOR_CAPITAL"]);
                            if (resultado["VALOR_INTERES"] != DBNull.Value) entidad.valor_interes = Convert.ToInt64(resultado["VALOR_INTERES"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["SALDO_INTERES"] != DBNull.Value) entidad.saldo_interes = Convert.ToInt64(resultado["SALDO_INTERES"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["NOM_FORMA_PAGO"] != DBNull.Value) entidad.des_forma_pago = Convert.ToString(resultado["NOM_FORMA_PAGO"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.des_tipo_cuota = Convert.ToString(resultado["descripcion"]);

                            lstCuotasExtras.Add(entidad);
                        }

                        return lstCuotasExtras;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuotasExtrasData", "ListarCuotasExtras", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla CuotasExtras dados unos filtros
        /// </summary>
        /// <param name="pCuotasExtras">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CuotasExtras obtenidos</returns>
        public List<CuotasExtras> ListarCuotasExtrasId(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CuotasExtras> lstCuotasExtras = new List<CuotasExtras>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT solicitudcredcuotasextras.*,TIPO_CUOTAS_EXTRAS.IDTIPO||'-'||TIPO_CUOTAS_EXTRAS.DESCRIPCION as descripcion FROM  solicitudcredcuotasextras LEFT JOIN TIPO_CUOTAS_EXTRAS ON  solicitudcredcuotasextras.TIPO_CUOTA = TIPO_CUOTAS_EXTRAS.IDTIPO where numerosolicitud=" + pId + " order by FECHA";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CuotasExtras entidad = new CuotasExtras();

                            if (resultado["IDCUOTAEXTRA"] != DBNull.Value) entidad.cod_cuota = Convert.ToInt64(resultado["IDCUOTAEXTRA"]);
                            if (resultado["NUMEROSOLICITUD"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMEROSOLICITUD"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha_pago = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["TIP_FOR_PAG"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["TIP_FOR_PAG"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.des_tipo_cuota = Convert.ToString(resultado["DESCRIPCION"]);

                            lstCuotasExtras.Add(entidad);
                        }

                        return lstCuotasExtras;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuotasExtrasData", "ListarCuotasExtras", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla CuotasExtras dados unos filtros
        /// </summary>
        /// <param name="pCuotasExtras">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CuotasExtras obtenidos</returns>
        public List<CuotasExtras> ListarSolicitudCuotasExtras(CuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CuotasExtras> lstCuotasExtras = new List<CuotasExtras>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT C.*,CASE c.TIP_FOR_PAG WHEN 1 THEN 'Caja' WHEN 2 THEN 'Nomina' ELSE '' END AS DES_FORMA_PAGO FROM  SOLICITUDCREDCUOTASEXTRAS C WHERE IDCUOTAEXTRA=" + pCuotasExtras.cod_cuota + "";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CuotasExtras entidad = new CuotasExtras();
                            if (resultado["IDCUOTAEXTRA"] != DBNull.Value) entidad.cod_cuota = Convert.ToInt64(resultado["IDCUOTAEXTRA"]);
                            if (resultado["NUMEROSOLICITUD"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMEROSOLICITUD"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha_pago = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["TIP_FOR_PAG"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["TIP_FOR_PAG"]);
                            if (resultado["DES_FORMA_PAGO"] != DBNull.Value) entidad.des_forma_pago = Convert.ToString(resultado["DES_FORMA_PAGO"]);

                            lstCuotasExtras.Add(entidad);
                        }

                        return lstCuotasExtras;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuotasExtrasData", "ListarSolicitudCuotasExtras", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Crea un registro en la tabla CuotasExtras de la base de datos
        /// </summary>
        /// <param name="pCuotasExtras">Entidad CuotasExtras</param>
        /// <returns>Entidad CuotasExtras creada</returns>
        public CuotasExtras CrearSimulacionCuotasExtras(CuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            Configuracion conf = new Configuracion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDCUOTAEXTRA = cmdTransaccionFactory.CreateParameter();
                        pIDCUOTAEXTRA.ParameterName = "p_IDCUOTAEXTRA";
                        pIDCUOTAEXTRA.Value = pCuotasExtras.cod_cuota;
                        pIDCUOTAEXTRA.Direction = ParameterDirection.InputOutput;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = "p_FECHA";
                        pFECHA.Value = Convert.ToDateTime(pCuotasExtras.fecha_pago).ToString(conf.ObtenerFormatoFecha());
                        pFECHA.DbType = DbType.DateTime;
                        pFECHA.Direction = ParameterDirection.Input;

                        DbParameter pVALOR = cmdTransaccionFactory.CreateParameter();
                        pVALOR.ParameterName = "p_VALOR";
                        pVALOR.Value = pCuotasExtras.valor;
                        pVALOR.Direction = ParameterDirection.Input;

                        DbParameter pTIP_FOR_PAG = cmdTransaccionFactory.CreateParameter();
                        pTIP_FOR_PAG.ParameterName = "p_TIP_FOR_PAG";
                        pTIP_FOR_PAG.Value = pCuotasExtras.forma_pago;
                        pTIP_FOR_PAG.Direction = ParameterDirection.Input;

                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "p_NUMERO_RADICACION";
                        pNUMERO_RADICACION.Value = pCuotasExtras.numero_radicacion;
                        pNUMERO_RADICACION.Direction = ParameterDirection.Input;
                        pNUMERO_RADICACION.DbType = DbType.Int64;

                        cmdTransaccionFactory.Parameters.Add(pIDCUOTAEXTRA);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pVALOR);
                        cmdTransaccionFactory.Parameters.Add(pTIP_FOR_PAG);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_SIMUCUOEXT_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pCuotasExtras.cod_cuota = Convert.ToInt64(pIDCUOTAEXTRA.Value);
                        return pCuotasExtras;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuotasExtrasData", "CrearCuotasExtras", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarCuotasExtrasTemporales(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "DELETE FROM TEMP_CUOTASEXTRAS WHERE NUMERO_RADICACION = " + pId;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteScalar();
                        connection.Close();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pCuotasExtras, "CuotasExtras", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuotasExtrasData", "EliminarCuotasExtras", ex);
                    }
                }
            }
        }

        public void EliminarCuotasExtrasActuales(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "DELETE FROM CUOTASEXTRAS WHERE NUMERO_RADICACION = " + pId;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteScalar();
                        connection.Close();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pCuotasExtras, "CuotasExtras", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuotasExtrasData", "EliminarCuotasExtras", ex);
                    }
                }
            }
        }



    }
}