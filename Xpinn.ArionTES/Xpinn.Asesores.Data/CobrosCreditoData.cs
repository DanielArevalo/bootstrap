using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla cobros_credito
    /// </summary>
    public class CobrosCreditoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla cobros_credito
        /// </summary>
        public CobrosCreditoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla cobros_credito de la base de datos
        /// </summary>
        /// <param name="pCobrosCredito">Entidad CobrosCredito</param>
        /// <returns>Entidad CobrosCredito creada</returns>
        public CobrosCredito CrearCobrosCredito(CobrosCredito pCobrosCredito, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "pnumero_radicacion";
                        pNUMERO_RADICACION.Value = pCobrosCredito.numero_radicacion;
                        pNUMERO_RADICACION.Direction = ParameterDirection.Input;

                        DbParameter pESTADO_PROCESO = cmdTransaccionFactory.CreateParameter();
                        pESTADO_PROCESO.ParameterName = "pestado_proceso";
                        pESTADO_PROCESO.Value = pCobrosCredito.estado_proceso;
                        pESTADO_PROCESO.Direction = ParameterDirection.Input;

                        DbParameter pENCARGADO = cmdTransaccionFactory.CreateParameter();
                        pENCARGADO.ParameterName = "pencargado";
                        pENCARGADO.Value = pCobrosCredito.encargado;
                        pENCARGADO.Direction = ParameterDirection.Input;

                        DbParameter p_ABOGADOENCARGADO = cmdTransaccionFactory.CreateParameter();
                        p_ABOGADOENCARGADO.ParameterName = "p_abogadoencargado";
                        p_ABOGADOENCARGADO.Value = pCobrosCredito.abogadoencargado;
                        p_ABOGADOENCARGADO.Direction = ParameterDirection.Input;

                        DbParameter pCOD_MOTIVO_CAMBIO = cmdTransaccionFactory.CreateParameter();
                        pCOD_MOTIVO_CAMBIO.ParameterName = "p_cod_motivo_cambio";
                        pCOD_MOTIVO_CAMBIO.Value = pCobrosCredito.cod_motivo_cambio;
                        pCOD_MOTIVO_CAMBIO.Direction = ParameterDirection.Input;
                        
                        DbParameter pCIUDAD = cmdTransaccionFactory.CreateParameter();
                        pCIUDAD.ParameterName = "pCIUDAD";
                        pCIUDAD.Value = pCobrosCredito.ciudad;
                        pCIUDAD.Direction = ParameterDirection.Input;


                        DbParameter p_ciudad_juzgado = cmdTransaccionFactory.CreateParameter();
                        p_ciudad_juzgado.ParameterName = "p_ciudad_juzgado";
                        p_ciudad_juzgado.Value = pCobrosCredito.ciudad_juzgado;
                        p_ciudad_juzgado.Direction = ParameterDirection.Input;

                        DbParameter p_numero_juzgado = cmdTransaccionFactory.CreateParameter();
                        p_numero_juzgado.ParameterName = "p_numero_juzgado";
                        p_numero_juzgado.Value = pCobrosCredito.numero_juzgado;
                        p_numero_juzgado.Direction = ParameterDirection.Input;


                        DbParameter p_observaciones = cmdTransaccionFactory.CreateParameter();
                        p_observaciones.ParameterName = "p_observaciones";
                        p_observaciones.Value = pCobrosCredito.observaciones;
                        p_observaciones.Direction = ParameterDirection.Input;


                        DbParameter pFECHACREA= cmdTransaccionFactory.CreateParameter();
                        pFECHACREA.ParameterName = "pFECHACREA";
                        pFECHACREA.Value = pCobrosCredito.fechacreacion;
                        pFECHACREA.Direction = ParameterDirection.Input;
                        pFECHACREA.DbType = DbType.DateTime;

                        DbParameter pUSUCREA = cmdTransaccionFactory.CreateParameter();
                        pUSUCREA.ParameterName = "pUSUCREA";
                        pUSUCREA.Value = pCobrosCredito.usucreacion;
                        pUSUCREA.Direction = ParameterDirection.Input;



                        DbParameter p_codigo = cmdTransaccionFactory.CreateParameter();
                        p_codigo.ParameterName = "p_codigo";
                        p_codigo.DbType = DbType.Int64;
                        p_codigo.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(pESTADO_PROCESO);
                        cmdTransaccionFactory.Parameters.Add(pENCARGADO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_MOTIVO_CAMBIO);
                        cmdTransaccionFactory.Parameters.Add(p_ABOGADOENCARGADO);
                        cmdTransaccionFactory.Parameters.Add(pCIUDAD);
                        cmdTransaccionFactory.Parameters.Add(p_ciudad_juzgado);
                        cmdTransaccionFactory.Parameters.Add(p_numero_juzgado);
                        cmdTransaccionFactory.Parameters.Add(p_observaciones);
                        cmdTransaccionFactory.Parameters.Add(pFECHACREA);
                        cmdTransaccionFactory.Parameters.Add(pUSUCREA);
                        cmdTransaccionFactory.Parameters.Add(p_codigo);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_COB_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pCobrosCredito, "cobros_credito", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pCobrosCredito.codigo = Convert.ToInt64(p_codigo.Value);
                        return pCobrosCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CobrosCreditoData", "CrearCobrosCredito", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla cobros_credito de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad CobrosCredito modificada</returns>
        public CobrosCredito ModificarCobrosCredito(CobrosCredito pCobrosCredito, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "p_NUMERO_RADICACION";
                        pNUMERO_RADICACION.Value = pCobrosCredito.numero_radicacion;
                        pNUMERO_RADICACION.DbType = DbType.Int64;

                        DbParameter pESTADO_PROCESO = cmdTransaccionFactory.CreateParameter();
                        pESTADO_PROCESO.ParameterName = "p_ESTADO_PROCESO";
                        pESTADO_PROCESO.Value = pCobrosCredito.estado_proceso;
                        pESTADO_PROCESO.DbType = DbType.Int64;

                        DbParameter pENCARGADO = cmdTransaccionFactory.CreateParameter();
                        pENCARGADO.ParameterName = "p_ENCARGADO";
                        pENCARGADO.Value = pCobrosCredito.encargado;
                        pENCARGADO.DbType = DbType.Int64;

                        DbParameter pABOGADOENCARGADO = cmdTransaccionFactory.CreateParameter();
                        pABOGADOENCARGADO.ParameterName = "p_ABOGADOENCARGADO";
                        pABOGADOENCARGADO.Value = pCobrosCredito.abogadoencargado;
                        pABOGADOENCARGADO.DbType = DbType.Int64;

                        DbParameter pCOD_MOTIVO_CAMBIO = cmdTransaccionFactory.CreateParameter();
                        pCOD_MOTIVO_CAMBIO.ParameterName = "p_COD_MOTIVO_CAMBIO";
                        pCOD_MOTIVO_CAMBIO.Value = pCobrosCredito.cod_motivo_cambio;
                        pCOD_MOTIVO_CAMBIO.DbType = DbType.Int64;

                        DbParameter pCIUDAD = cmdTransaccionFactory.CreateParameter();
                        pCIUDAD.ParameterName = "pCIUDAD";
                        pCIUDAD.Value = pCobrosCredito.ciudad;
                        pCIUDAD.Direction = ParameterDirection.Input;
                        pCIUDAD.DbType = DbType.Int64;

                        DbParameter p_ciudad_juzgado = cmdTransaccionFactory.CreateParameter();
                        p_ciudad_juzgado.ParameterName = "p_ciudad_juzgado";
                        p_ciudad_juzgado.Value = pCobrosCredito.ciudad_juzgado;
                        p_ciudad_juzgado.Direction = ParameterDirection.Input;
                        p_ciudad_juzgado.DbType = DbType.Int64;

                        DbParameter p_numero_juzgado = cmdTransaccionFactory.CreateParameter();
                        p_numero_juzgado.ParameterName = "p_numero_juzgado";
                        if (pCobrosCredito.numero_juzgado != null && pCobrosCredito.numero_juzgado != "")
                            p_numero_juzgado.Value = pCobrosCredito.numero_juzgado;
                        else
                            p_numero_juzgado.Value = DBNull.Value;
                        p_numero_juzgado.Direction = ParameterDirection.Input;
                        p_numero_juzgado.DbType = DbType.Int64;
                        
                        DbParameter p_observaciones = cmdTransaccionFactory.CreateParameter();
                        p_observaciones.ParameterName = "p_observaciones";
                        if (pCobrosCredito.observaciones != null)
                            p_observaciones.Value = pCobrosCredito.observaciones;
                        else
                            p_observaciones.Value = DBNull.Value;
                        p_observaciones.Direction = ParameterDirection.Input;
                        p_observaciones.DbType = DbType.String;
                        
                        DbParameter pFECHAMOD= cmdTransaccionFactory.CreateParameter();
                        pFECHAMOD.ParameterName = "pFECHAMOD";                        
                        if (pCobrosCredito.fechamodif != null)
                            pFECHAMOD.Value = pCobrosCredito.fechamodif;
                        else
                            p_observaciones.Value = DateTime.MinValue;
                        pFECHAMOD.Direction = ParameterDirection.Input;
                        pFECHAMOD.DbType = DbType.DateTime;

                        DbParameter pUSUMOD= cmdTransaccionFactory.CreateParameter();
                        pUSUMOD.ParameterName = "pUSUMOD";
                        pUSUMOD.Value = pCobrosCredito.usumodif;
                        pUSUMOD.Direction = ParameterDirection.Input;
                        pUSUMOD.DbType = DbType.String;

                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(pESTADO_PROCESO);
                        cmdTransaccionFactory.Parameters.Add(pENCARGADO);
                        cmdTransaccionFactory.Parameters.Add(pABOGADOENCARGADO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_MOTIVO_CAMBIO);
                        cmdTransaccionFactory.Parameters.Add(p_ciudad_juzgado);
                        cmdTransaccionFactory.Parameters.Add(p_numero_juzgado);
                        cmdTransaccionFactory.Parameters.Add(p_observaciones);
                        cmdTransaccionFactory.Parameters.Add(pCIUDAD);
                        cmdTransaccionFactory.Parameters.Add(pFECHAMOD);
                        cmdTransaccionFactory.Parameters.Add(pUSUMOD);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_COB_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pCobrosCredito, "cobros_credito",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pCobrosCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CobrosCreditoData", "ModificarCobrosCredito", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla cobros_credito de la base de datos
        /// </summary>
        /// <param name="pId">identificador de cobros_credito</param>
        public void EliminarCobrosCredito(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        CobrosCredito pCobrosCredito = new CobrosCredito();

                        if (pUsuario.programaGeneraLog)
                            pCobrosCredito = ConsultarCobrosCredito(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "p_numero_radicacion";
                        pNUMERO_RADICACION.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_Asesores_cobros_credito_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pCobrosCredito, "cobros_credito", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CobrosCreditoData", "EliminarCobrosCredito", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla cobros_credito de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla cobros_credito</param>
        /// <returns>Entidad CobrosCredito consultado</returns>
        public CobrosCredito ConsultarCobrosCredito(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            CobrosCredito entidad = new CobrosCredito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  COBROS_CREDITO WHERE numero_radicacion = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["ESTADO_PROCESO"] != DBNull.Value) entidad.estado_proceso = Convert.ToInt64(resultado["ESTADO_PROCESO"]);
                            if (resultado["ENCARGADO"] != DBNull.Value) entidad.encargado = Convert.ToInt64(resultado["ENCARGADO"]);
                            if (resultado["COD_MOTIVO_CAMBIO"] != DBNull.Value) entidad.cod_motivo_cambio = Convert.ToInt64(resultado["COD_MOTIVO_CAMBIO"]);
                            if (resultado["ABOGADO_ENCARGADO"] != DBNull.Value) entidad.abogadoencargado = Convert.ToInt64(resultado["ABOGADO_ENCARGADO"]);
                            if (resultado["CIUDAD_JUZGADO"] != DBNull.Value) entidad.ciudad_juzgado = Convert.ToInt64(resultado["CIUDAD_JUZGADO"]);
                            if (resultado["NUMERO_JUZGADO"] != DBNull.Value) entidad.numero_juzgado = Convert.ToString(resultado["NUMERO_JUZGADO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);

                        }
                        //else
                       // {
                          //  throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                       // }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CobrosCreditoData", "ConsultarCobrosCredito", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla cobros_credito dados unos filtros
        /// </summary>
        /// <param name="pcobros_credito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CobrosCredito obtenidos</returns>
        public List<CobrosCredito> ListarCobrosCredito(CobrosCredito pCobrosCredito, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CobrosCredito> lstCobrosCredito = new List<CobrosCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  COBROS_CREDITO " + ObtenerFiltro(pCobrosCredito);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CobrosCredito entidad = new CobrosCredito();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["ESTADO_PROCESO"] != DBNull.Value) entidad.estado_proceso = Convert.ToInt64(resultado["ESTADO_PROCESO"]);
                            if (resultado["ENCARGADO"] != DBNull.Value) entidad.encargado = Convert.ToInt64(resultado["ENCARGADO"]);
                            if (resultado["COD_MOTIVO_CAMBIO"] != DBNull.Value) entidad.cod_motivo_cambio = Convert.ToInt64(resultado["COD_MOTIVO_CAMBIO"]);

                            lstCobrosCredito.Add(entidad);
                        }

                        return lstCobrosCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CobrosCreditoData", "ListarCobrosCredito", ex);
                        return null;
                    }
                }
            }
        }
    }
}