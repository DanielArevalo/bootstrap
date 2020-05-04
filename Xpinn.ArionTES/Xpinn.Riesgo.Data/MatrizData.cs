using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Riesgo.Entities;

namespace Xpinn.Riesgo.Data
{
    public class MatrizData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor para el acceso a base de datos
        /// </summary>
        public MatrizData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crear registro perteneciente a la matriz de riesgo inherente
        /// </summary>
        /// <param name="pMatriz">Objeto con los datos del factor</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Matriz CrearMatrizRInherente(Matriz pMatriz, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_matriz = cmdTransaccionFactory.CreateParameter();
                        p_cod_matriz.ParameterName = "p_cod_matriz";
                        p_cod_matriz.Value = pMatriz.cod_matriz;
                        p_cod_matriz.Direction = ParameterDirection.Output;
                        p_cod_matriz.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_matriz);

                        DbParameter p_cod_riesgo = cmdTransaccionFactory.CreateParameter();
                        p_cod_riesgo.ParameterName = "p_cod_riesgo";
                        p_cod_riesgo.Value = pMatriz.cod_riesgo;
                        p_cod_riesgo.Direction = ParameterDirection.Input;
                        p_cod_riesgo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_riesgo);

                        DbParameter p_cod_factor = cmdTransaccionFactory.CreateParameter();
                        p_cod_factor.ParameterName = "p_cod_factor";
                        p_cod_factor.Value = pMatriz.cod_factor;
                        p_cod_factor.Direction = ParameterDirection.Input;
                        p_cod_factor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_factor);

                        DbParameter p_cod_causa = cmdTransaccionFactory.CreateParameter();
                        p_cod_causa.ParameterName = "p_cod_causa";
                        p_cod_causa.Value = pMatriz.cod_causa;
                        p_cod_causa.Direction = ParameterDirection.Input;
                        p_cod_causa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_causa);

                        DbParameter p_cod_probabilidad = cmdTransaccionFactory.CreateParameter();
                        p_cod_probabilidad.ParameterName = "p_cod_probabilidad";
                        p_cod_probabilidad.Value = pMatriz.cod_probabilidad;
                        p_cod_probabilidad.Direction = ParameterDirection.Input;
                        p_cod_probabilidad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_probabilidad);

                        DbParameter p_cod_impacto = cmdTransaccionFactory.CreateParameter();
                        p_cod_impacto.ParameterName = "p_cod_impacto";
                        p_cod_impacto.Value = pMatriz.cod_impacto;
                        p_cod_impacto.Direction = ParameterDirection.Input;
                        p_cod_impacto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_impacto);

                        DbParameter p_calificacion_riesgo = cmdTransaccionFactory.CreateParameter();
                        p_calificacion_riesgo.ParameterName = "p_calificacion_riesgo";
                        p_calificacion_riesgo.Value = pMatriz.valor_rinherente;
                        p_calificacion_riesgo.Direction = ParameterDirection.Input;
                        p_calificacion_riesgo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_calificacion_riesgo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_MATRIZ_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pMatriz.cod_matriz = Convert.ToInt64(p_cod_matriz.Value);
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pMatriz, "GR_MATRIZ_RINHERENTE", vUsuario, Accion.Crear.ToString(), TipoAuditoria.GestionRiesgo, "Crear un registro en la matriz de riesgo inherente: " + pMatriz.cod_matriz); //REGISTRO DE AUDITORIA

                        return pMatriz;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "CrearMatrizRInherente", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modificar registro perteneciente a la matriz de riesgo inherente
        /// </summary>
        /// <param name="pMatriz">Objeto con los datos del factor</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Matriz ModificarMatrizRInherente(Matriz pMatriz, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_matriz = cmdTransaccionFactory.CreateParameter();
                        p_cod_matriz.ParameterName = "p_cod_matriz";
                        p_cod_matriz.Value = pMatriz.cod_matriz;
                        p_cod_matriz.Direction = ParameterDirection.Input;
                        p_cod_matriz.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_matriz);

                        DbParameter p_cod_riesgo = cmdTransaccionFactory.CreateParameter();
                        p_cod_riesgo.ParameterName = "p_cod_riesgo";
                        p_cod_riesgo.Value = pMatriz.cod_riesgo;
                        p_cod_riesgo.Direction = ParameterDirection.Input;
                        p_cod_riesgo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_riesgo);

                        DbParameter p_cod_factor = cmdTransaccionFactory.CreateParameter();
                        p_cod_factor.ParameterName = "p_cod_factor";
                        p_cod_factor.Value = pMatriz.cod_factor;
                        p_cod_factor.Direction = ParameterDirection.Input;
                        p_cod_factor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_factor);

                        DbParameter p_cod_causa = cmdTransaccionFactory.CreateParameter();
                        p_cod_causa.ParameterName = "p_cod_causa";
                        p_cod_causa.Value = pMatriz.cod_causa;
                        p_cod_causa.Direction = ParameterDirection.Input;
                        p_cod_causa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_causa);

                        DbParameter p_cod_probabilidad = cmdTransaccionFactory.CreateParameter();
                        p_cod_probabilidad.ParameterName = "p_cod_probabilidad";
                        p_cod_probabilidad.Value = pMatriz.cod_probabilidad;
                        p_cod_probabilidad.Direction = ParameterDirection.Input;
                        p_cod_probabilidad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_probabilidad);

                        DbParameter p_cod_impacto = cmdTransaccionFactory.CreateParameter();
                        p_cod_impacto.ParameterName = "p_cod_impacto";
                        p_cod_impacto.Value = pMatriz.cod_impacto;
                        p_cod_impacto.Direction = ParameterDirection.Input;
                        p_cod_impacto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_impacto);

                        DbParameter p_calificacion_riesgo = cmdTransaccionFactory.CreateParameter();
                        p_calificacion_riesgo.ParameterName = "p_calificacion_riesgo";
                        p_calificacion_riesgo.Value = pMatriz.valor_rinherente;
                        p_calificacion_riesgo.Direction = ParameterDirection.Input;
                        p_calificacion_riesgo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_calificacion_riesgo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_MATRIZ_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pMatriz, "GR_MATRIZ_RINHERENTE", vUsuario, Accion.Modificar.ToString(), TipoAuditoria.GestionRiesgo, "Modificar un registro en la matriz de riesgo inherente: " + pMatriz.cod_matriz); //REGISTRO DE AUDITORIA

                        return pMatriz;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "ModificarMatrizRInherente", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Eliminar registro perteneciente a la matriz de riesgo inherente
        /// </summary>
        /// <param name="pRegistro">Código del registro en la matriz</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public void EliminarMatrizRInherente(Int64 pRegistro, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_matriz = cmdTransaccionFactory.CreateParameter();
                        p_cod_matriz.ParameterName = "p_cod_matriz";
                        p_cod_matriz.Value = pRegistro;
                        p_cod_matriz.Direction = ParameterDirection.Input;
                        p_cod_matriz.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_matriz);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_MATRIZ_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pRegistro, "GR_MATRIZ_RINHERENTE", vUsuario, Accion.Eliminar.ToString(), TipoAuditoria.GestionRiesgo, "Eliminar un registro en la matriz de riesgo inherente: " + pRegistro); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "EliminarMatrizRInherente", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Crear un registro en la matriz de valoración del control
        /// </summary>
        /// <param name="pMatriz">Objeto con datos del registro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Matriz CrearMatrizRResidual(Matriz pMatriz, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_matriz = cmdTransaccionFactory.CreateParameter();
                        p_cod_matriz.ParameterName = "p_cod_matriz";
                        p_cod_matriz.Value = pMatriz.cod_matriz;
                        p_cod_matriz.Direction = ParameterDirection.Output;
                        p_cod_matriz.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_matriz);

                        DbParameter p_cod_riesgo = cmdTransaccionFactory.CreateParameter();
                        p_cod_riesgo.ParameterName = "p_cod_riesgo";
                        p_cod_riesgo.Value = pMatriz.cod_riesgo;
                        p_cod_riesgo.Direction = ParameterDirection.Input;
                        p_cod_riesgo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_riesgo);

                        DbParameter p_cod_factor = cmdTransaccionFactory.CreateParameter();
                        p_cod_factor.ParameterName = "p_cod_factor";
                        p_cod_factor.Value = pMatriz.cod_factor;
                        p_cod_factor.Direction = ParameterDirection.Input;
                        p_cod_factor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_factor);

                        DbParameter p_valor_rinherente = cmdTransaccionFactory.CreateParameter();
                        p_valor_rinherente.ParameterName = "p_valor_rinherente";
                        p_valor_rinherente.Value = pMatriz.valor_rinherente;
                        p_valor_rinherente.Direction = ParameterDirection.Input;
                        p_valor_rinherente.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_valor_rinherente);

                        DbParameter p_cod_control = cmdTransaccionFactory.CreateParameter();
                        p_cod_control.ParameterName = "p_cod_control";
                        p_cod_control.Value = pMatriz.cod_control;
                        p_cod_control.Direction = ParameterDirection.Input;
                        p_cod_control.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_control);

                        DbParameter p_forma = cmdTransaccionFactory.CreateParameter();
                        p_forma.ParameterName = "p_forma";
                        p_forma.Value = pMatriz.forma;
                        p_forma.Direction = ParameterDirection.Input;
                        p_forma.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_forma);

                        DbParameter p_valor_control = cmdTransaccionFactory.CreateParameter();
                        p_valor_control.ParameterName = "p_valor_control";
                        p_valor_control.Value = pMatriz.valor_control;
                        p_valor_control.Direction = ParameterDirection.Input;
                        p_valor_control.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_valor_control);

                        DbParameter p_valor_rresidual = cmdTransaccionFactory.CreateParameter();
                        p_valor_rresidual.ParameterName = "p_valor_rresidual";
                        p_valor_rresidual.Value = pMatriz.valor_rresidual;
                        p_valor_rresidual.Direction = ParameterDirection.Input;
                        p_valor_rresidual.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_valor_rresidual);

                        DbParameter p_cod_causa = cmdTransaccionFactory.CreateParameter();
                        p_cod_causa.ParameterName = "p_cod_causa";
                        p_cod_causa.Value = pMatriz.cod_causa;
                        p_cod_causa.Direction = ParameterDirection.Input;
                        p_cod_causa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_causa);

                        DbParameter p_ejecucion = cmdTransaccionFactory.CreateParameter();
                        p_ejecucion.ParameterName = "p_ejecucion";
                        p_ejecucion.Value = pMatriz.ejecucion;
                        p_ejecucion.Direction = ParameterDirection.Input;
                        p_ejecucion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_ejecucion);

                        DbParameter p_documentacion = cmdTransaccionFactory.CreateParameter();
                        p_documentacion.ParameterName = "p_documentacion";
                        p_documentacion.Value = pMatriz.documentacion;
                        p_documentacion.Direction = ParameterDirection.Input;
                        p_documentacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_documentacion);

                        DbParameter p_complejidad = cmdTransaccionFactory.CreateParameter();
                        p_complejidad.ParameterName = "p_complejidad";
                        p_complejidad.Value = pMatriz.complejidad;
                        p_complejidad.Direction = ParameterDirection.Input;
                        p_complejidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_complejidad);

                        DbParameter p_fallas = cmdTransaccionFactory.CreateParameter();
                        p_fallas.ParameterName = "p_fallas";
                        p_fallas.Value = pMatriz.fallas;
                        p_fallas.Direction = ParameterDirection.Input;
                        p_fallas.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_fallas);

                        DbParameter p_nivel_reduccion = cmdTransaccionFactory.CreateParameter();
                        p_nivel_reduccion.ParameterName = "p_nivel_reduccion";
                        p_nivel_reduccion.Value = pMatriz.nivel_reduccion;
                        p_nivel_reduccion.Direction = ParameterDirection.Input;
                        p_nivel_reduccion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_nivel_reduccion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_MRESIDUAL_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pMatriz.cod_matriz = Convert.ToInt64(p_cod_matriz.Value);
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pMatriz, "GR_MATRIZ_RRESIDUAL", vUsuario, Accion.Crear.ToString(), TipoAuditoria.GestionRiesgo, "Crear un registro en la matriz de riesgo residual: " + pMatriz.cod_matriz); //REGISTRO DE AUDITORIA

                        return pMatriz;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "CrearMatrizRResidual", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modificar un registro en la matriz de valoración del control
        /// </summary>
        /// <param name="pMatriz">Objeto con datos del registro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Matriz ModificarMatrizRResidual(Matriz pMatriz, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_matriz = cmdTransaccionFactory.CreateParameter();
                        p_cod_matriz.ParameterName = "p_cod_matriz";
                        p_cod_matriz.Value = pMatriz.cod_matriz;
                        p_cod_matriz.Direction = ParameterDirection.Input;
                        p_cod_matriz.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_matriz);

                        DbParameter p_cod_riesgo = cmdTransaccionFactory.CreateParameter();
                        p_cod_riesgo.ParameterName = "p_cod_riesgo";
                        p_cod_riesgo.Value = pMatriz.cod_riesgo;
                        p_cod_riesgo.Direction = ParameterDirection.Input;
                        p_cod_riesgo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_riesgo);

                        DbParameter p_cod_factor = cmdTransaccionFactory.CreateParameter();
                        p_cod_factor.ParameterName = "p_cod_factor";
                        p_cod_factor.Value = pMatriz.cod_factor;
                        p_cod_factor.Direction = ParameterDirection.Input;
                        p_cod_factor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_factor);

                        DbParameter p_valor_rinherente = cmdTransaccionFactory.CreateParameter();
                        p_valor_rinherente.ParameterName = "p_valor_rinherente";
                        p_valor_rinherente.Value = pMatriz.valor_rinherente;
                        p_valor_rinherente.Direction = ParameterDirection.Input;
                        p_valor_rinherente.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_valor_rinherente);

                        DbParameter p_cod_control = cmdTransaccionFactory.CreateParameter();
                        p_cod_control.ParameterName = "p_cod_control";
                        p_cod_control.Value = pMatriz.cod_control;
                        p_cod_control.Direction = ParameterDirection.Input;
                        p_cod_control.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_control);

                        DbParameter p_forma = cmdTransaccionFactory.CreateParameter();
                        p_forma.ParameterName = "p_forma";
                        p_forma.Value = pMatriz.forma;
                        p_forma.Direction = ParameterDirection.Input;
                        p_forma.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_forma);

                        DbParameter p_valor_control = cmdTransaccionFactory.CreateParameter();
                        p_valor_control.ParameterName = "p_valor_control";
                        p_valor_control.Value = pMatriz.valor_control;
                        p_valor_control.Direction = ParameterDirection.Input;
                        p_valor_control.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_valor_control);

                        DbParameter p_valor_rresidual = cmdTransaccionFactory.CreateParameter();
                        p_valor_rresidual.ParameterName = "p_valor_rresidual";
                        p_valor_rresidual.Value = pMatriz.valor_rresidual;
                        p_valor_rresidual.Direction = ParameterDirection.Input;
                        p_valor_rresidual.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_valor_rresidual);

                        DbParameter p_cod_causa = cmdTransaccionFactory.CreateParameter();
                        p_cod_causa.ParameterName = "p_cod_causa";
                        p_cod_causa.Value = pMatriz.cod_causa;
                        p_cod_causa.Direction = ParameterDirection.Input;
                        p_cod_causa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_causa);

                        DbParameter p_ejecucion = cmdTransaccionFactory.CreateParameter();
                        p_ejecucion.ParameterName = "p_ejecucion";
                        p_ejecucion.Value = pMatriz.ejecucion;
                        p_ejecucion.Direction = ParameterDirection.Input;
                        p_ejecucion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_ejecucion);

                        DbParameter p_documentacion = cmdTransaccionFactory.CreateParameter();
                        p_documentacion.ParameterName = "p_documentacion";
                        p_documentacion.Value = pMatriz.documentacion;
                        p_documentacion.Direction = ParameterDirection.Input;
                        p_documentacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_documentacion);

                        DbParameter p_complejidad = cmdTransaccionFactory.CreateParameter();
                        p_complejidad.ParameterName = "p_complejidad";
                        p_complejidad.Value = pMatriz.complejidad;
                        p_complejidad.Direction = ParameterDirection.Input;
                        p_complejidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_complejidad);

                        DbParameter p_fallas = cmdTransaccionFactory.CreateParameter();
                        p_fallas.ParameterName = "p_fallas";
                        p_fallas.Value = pMatriz.fallas;
                        p_fallas.Direction = ParameterDirection.Input;
                        p_fallas.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_fallas);

                        DbParameter p_nivel_reduccion = cmdTransaccionFactory.CreateParameter();
                        p_nivel_reduccion.ParameterName = "p_nivel_reduccion";
                        p_nivel_reduccion.Value = pMatriz.nivel_reduccion;
                        p_nivel_reduccion.Direction = ParameterDirection.Input;
                        p_nivel_reduccion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_nivel_reduccion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_MRESIDUAL_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pMatriz, "GR_MATRIZ_RRESIDUAL", vUsuario, Accion.Modificar.ToString(), TipoAuditoria.GestionRiesgo, "Modificar un registro en la matriz de riesgo residual: " + pMatriz.cod_matriz); //REGISTRO DE AUDITORIA

                        return pMatriz;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "ModificarMatrizRResidual", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Agregar parametro de monitoreo en cada registro de la matriz de riesgo residual
        /// </summary>
        /// <param name="lstMatriz"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Matriz MatrizParametroMonitoreo(Matriz pMatriz, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_matriz = cmdTransaccionFactory.CreateParameter();
                        p_cod_matriz.ParameterName = "p_cod_matriz";
                        p_cod_matriz.Value = pMatriz.cod_matriz;
                        p_cod_matriz.Direction = ParameterDirection.Input;
                        p_cod_matriz.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_matriz);

                        DbParameter p_cod_riesgo = cmdTransaccionFactory.CreateParameter();
                        p_cod_riesgo.ParameterName = "p_cod_riesgo";
                        p_cod_riesgo.Value = pMatriz.cod_riesgo;
                        p_cod_riesgo.Direction = ParameterDirection.Input;
                        p_cod_riesgo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_riesgo);

                        DbParameter p_cod_monitoreo = cmdTransaccionFactory.CreateParameter();
                        p_cod_monitoreo.ParameterName = "p_cod_monitoreo";
                        p_cod_monitoreo.Value = pMatriz.cod_monitoreo;
                        p_cod_monitoreo.Direction = ParameterDirection.Input;
                        p_cod_monitoreo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_monitoreo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_MR_MONITOREO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pMatriz, "GR_MATRIZ_RRESIDUAL", vUsuario, Accion.Modificar.ToString(), TipoAuditoria.GestionRiesgo, "Modificar un registro en la matriz de riesgo residual para agregar monitoreo: " + pMatriz.cod_matriz); //REGISTRO DE AUDITORIA

                        return pMatriz;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "MatrizParametroMonitoreo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        ///Consultar valor de la calificación según los niveles de probabilidad e impacto 
        /// </summary>
        /// <param name="cod_probabilidad">Nivel de probabilidad</param>
        /// <param name="cod_impacto">Nivel de impacto</param>
        /// <param name="vUsuario"></param>
        /// <returns>Valor de la calificación</returns>
        public int ConsultarCalificacion(Int64 cod_probabilidad, Int64 cod_impacto, Usuario vUsuario)
        {
            DbDataReader resultado;
            int calificacion = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT CALIFICACION FROM GR_MATRIZ_CALOR WHERE COD_PROBABILIDAD = " + cod_probabilidad + " AND COD_IMPACTO = " + cod_impacto;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CALIFICACION"] != DBNull.Value) calificacion = Convert.ToInt32(resultado["CALIFICACION"]);
                            calificacion = ConsultarRangoMatrizRiesgo(calificacion, vUsuario);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return calificacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "ConsultarCalificacion", ex);
                        return 0;
                    }
                }
            }
        }

        public int ConsultarRangoMatrizRiesgo(Int64 pCalificacion, Usuario pUsuario)
        {
            DbDataReader resultado;
            int valoracion = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT valoracion FROM GR_RANGO_MATRIZ_RIESGO WHERE " + pCalificacion.ToString() + " Between rango_minimo And rango_maximo";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["VALORACION"] != DBNull.Value) valoracion = Convert.ToInt32(resultado["VALORACION"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return valoracion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "ConsultarRangoMatrizRiesgo", ex);
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// Consultar el valor de calificación para control
        /// </summary>
        /// <param name="cod_clase">Clase de control</param>
        /// <param name="cod_forma">Forma de control</param>
        /// <param name="vUsuario"></param>
        /// <returns>Código calificación</returns>11
        public int ConsultarCalificacionControl(Int64 cod_clase, Int64 cod_forma, Usuario vUsuario)
        {
            DbDataReader resultado;
            int calificacion = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT CALIFICACION FROM GR_MATRIZ_CONTROL WHERE COD_CLASE = " + cod_clase + " AND COD_FORMA = " + cod_forma;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CALIFICACION"] != DBNull.Value) calificacion = Convert.ToInt32(resultado["CALIFICACION"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return calificacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "ConsultarCalificacionControl", ex);
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// Consultar datos según el nivel de impacto
        /// </summary>
        /// <param name="cod_impacto">Nivel de impacto</param>
        /// <param name="vUsuario"></param>
        /// <returns>Objeto con los datos del nivel de impacto</returns>
        public Matriz ConsultarImpacto(Int64 cod_impacto, Usuario vUsuario)
        {
            DbDataReader resultado;
            Matriz pImpacto = new Matriz();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT COD_IMPACTO, NIVEL_IMPACTO FROM GR_IMPACTO WHERE COD_IMPACTO = " + cod_impacto;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_IMPACTO"] != DBNull.Value) pImpacto.cod_impacto = Convert.ToInt64(resultado["COD_IMPACTO"]);
                            if (resultado["NIVEL_IMPACTO"] != DBNull.Value) pImpacto.nivel = Convert.ToString(resultado["NIVEL_IMPACTO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return pImpacto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "ConsultarImpacto", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Listar niveles de impacto
        /// </summary>
        /// <param name="vUsuario"></param>
        /// <returns>Listado con los niveles de impacto</returns>
        public List<Matriz> ListarImpacto(Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Matriz> lstImpacto = new List<Matriz>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT COD_IMPACTO, NIVEL_IMPACTO FROM GR_IMPACTO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Matriz pImpacto = new Matriz();
                            if (resultado["COD_IMPACTO"] != DBNull.Value) pImpacto.cod_impacto = Convert.ToInt64(resultado["COD_IMPACTO"]);
                            if (resultado["NIVEL_IMPACTO"] != DBNull.Value) pImpacto.nivel = Convert.ToString(resultado["NIVEL_IMPACTO"]);
                            lstImpacto.Add(pImpacto);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstImpacto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "ListarImpacto", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Consultar datos según el nivel de probabilidad
        /// </summary>
        /// <param name="cod_probabilidad">Nivel de probabilidad</param>
        /// <param name="vUsuario"></param>
        /// <returns>Objeto con los datos del nivel de probabilidad</returns>
        public Matriz ConsultarProbabilidad(Int64 cod_probabilidad, Usuario vUsuario)
        {
            DbDataReader resultado;
            Matriz pProbabilidad = new Matriz();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT COD_PROBABILIDAD, NIVEL_PROBABILIDAD FROM GR_PROBABILIDAD WHERE COD_PROBABILIDAD = " + cod_probabilidad;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PROBABILIDAD"] != DBNull.Value) pProbabilidad.cod_probabilidad = Convert.ToInt64(resultado["COD_PROBABILIDAD"]);
                            if (resultado["NIVEL_PROBABILIDAD"] != DBNull.Value) pProbabilidad.nivel = Convert.ToString(resultado["NIVEL_PROBABILIDAD"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return pProbabilidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "ConsultarProbabilidad", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Listar niveles de probabilidad
        /// </summary>
        /// <param name="vUsuario"></param>
        /// <returns>Listado con niveles de probabilidad</returns>
        public List<Matriz> ListarProbabilidad(Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Matriz> lstProbabilidad = new List<Matriz>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT COD_PROBABILIDAD, NIVEL_PROBABILIDAD FROM GR_PROBABILIDAD";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Matriz pProbabilidad = new Matriz();
                            if (resultado["COD_PROBABILIDAD"] != DBNull.Value) pProbabilidad.cod_probabilidad = Convert.ToInt64(resultado["COD_PROBABILIDAD"]);
                            if (resultado["NIVEL_PROBABILIDAD"] != DBNull.Value) pProbabilidad.nivel = Convert.ToString(resultado["NIVEL_PROBABILIDAD"]);
                            lstProbabilidad.Add(pProbabilidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProbabilidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "ListarProbabilidad", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Listar matriz de riesgo inherente según el sistema de riesgo
        /// </summary>
        /// /// <param name="cod_riesgo">Código del sistema de riesgo</param>
        /// <param name="vUsuario"></param>
        /// <returns>Listado con valores de la matriz</returns>
        public List<Matriz> ListarMatriz(Int64 cod_riesgo, string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Matriz> lstMatriz = new List<Matriz>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"WITH RESULTADO AS
                                    (
                                        SELECT M.COD_MATRIZ, M.COD_FACTOR, F.DESCRIPCION AS DESC_FACTOR, M.COD_CAUSA, C.DESCRIPCION AS DESC_CAUSA, M.COD_PROBABILIDAD, P.NIVEL_PROBABILIDAD, M.COD_IMPACTO, I.NIVEL_IMPACTO, M.CALIFICACION_RIESGO
                                        FROM GR_MATRIZ_RINHERENTE M
                                        LEFT JOIN GR_FACTOR_RIESGO F ON M.COD_FACTOR = F.COD_FACTOR
                                        LEFT JOIN GR_CAUSA_RIESGO C ON M.COD_CAUSA = C.COD_CAUSA
                                        LEFT JOIN GR_PROBABILIDAD P ON M.COD_PROBABILIDAD = P.COD_PROBABILIDAD
                                        LEFT JOIN GR_IMPACTO I ON M.COD_IMPACTO = I.COD_IMPACTO ";
                        if (cod_riesgo > 0)
                            sql += " WHERE M.COD_RIESGO = " + cod_riesgo + @"
                                    UNION 
                                    SELECT 0, F.COD_FACTOR, F.DESCRIPCION AS DESC_FACTOR, 0, '', 0, '', 0, '', 0
                                    FROM GR_FACTOR_RIESGO F WHERE F.COD_FACTOR NOT IN (SELECT DISTINCT COD_FACTOR FROM GR_MATRIZ_RINHERENTE WHERE COD_RIESGO = " + cod_riesgo + @") AND F.COD_RIESGO = " + cod_riesgo + @"
                                ) SELECT * FROM RESULTADO ";
                        else
                            sql += ") SELECT * FROM RESULTADO ";


                        sql += filtro != null && filtro != "" ? filtro : "";
                        sql += " ORDER BY COD_FACTOR ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Matriz pRegistro = new Matriz();
                            //if (resultado["COD_RIESGO"] != DBNull.Value) pRegistro.COD_R = Convert.ToInt64(resultado["COD_MATRIZ"]);
                            if (resultado["COD_MATRIZ"] != DBNull.Value) pRegistro.cod_matriz = Convert.ToInt64(resultado["COD_MATRIZ"]);
                            if (resultado["COD_FACTOR"] != DBNull.Value) pRegistro.cod_factor = Convert.ToInt64(resultado["COD_FACTOR"]);
                            if (resultado["DESC_FACTOR"] != DBNull.Value) pRegistro.desc_factor = Convert.ToString(resultado["DESC_FACTOR"]);
                            if (resultado["COD_CAUSA"] != DBNull.Value) pRegistro.cod_causa = Convert.ToInt64(resultado["COD_CAUSA"]);
                            if (resultado["DESC_CAUSA"] != DBNull.Value) pRegistro.descripcion = Convert.ToString(resultado["DESC_CAUSA"]);
                            if (resultado["COD_PROBABILIDAD"] != DBNull.Value) pRegistro.cod_probabilidad = Convert.ToInt64(resultado["COD_PROBABILIDAD"]);
                            if (resultado["NIVEL_PROBABILIDAD"] != DBNull.Value) pRegistro.desc_probabilidad = Convert.ToString(resultado["NIVEL_PROBABILIDAD"]);
                            if (resultado["COD_IMPACTO"] != DBNull.Value) pRegistro.cod_impacto = Convert.ToInt64(resultado["COD_IMPACTO"]);
                            if (resultado["NIVEL_IMPACTO"] != DBNull.Value) pRegistro.desc_impacto = Convert.ToString(resultado["NIVEL_IMPACTO"]);
                            if (resultado["CALIFICACION_RIESGO"] != DBNull.Value) pRegistro.valor_rinherente = Convert.ToInt64(resultado["CALIFICACION_RIESGO"]);
                            lstMatriz.Add(pRegistro);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstMatriz;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "ListarMatriz", ex);
                        return null;
                    }
                }
            }
        }

        public List<Matriz> ListarPromedioFactor(Int64 cod_riesgo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Matriz> lstMatriz = new List<Matriz>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT F.COD_FACTOR, F.DESCRIPCION AS DESC_FACTOR, PROMEDIO_FACTOR_RIESGO(F.COD_FACTOR) AS CALIFICACION_RIESGO
                                        FROM GR_FACTOR_RIESGO F WHERE F.COD_FACTOR IN (SELECT DISTINCT COD_FACTOR FROM GR_MATRIZ_RINHERENTE WHERE COD_RIESGO = " + cod_riesgo + ")";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Matriz pRegistro = new Matriz();
                            if (resultado["COD_FACTOR"] != DBNull.Value) pRegistro.cod_factor = Convert.ToInt64(resultado["COD_FACTOR"]);
                            if (resultado["DESC_FACTOR"] != DBNull.Value) pRegistro.desc_factor = Convert.ToString(resultado["DESC_FACTOR"]);
                            if (resultado["CALIFICACION_RIESGO"] != DBNull.Value) pRegistro.valor_rinherente = Convert.ToInt64(resultado["CALIFICACION_RIESGO"]);
                            lstMatriz.Add(pRegistro);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstMatriz;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "ListarPromedioFactor", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Listar matriz de riesgo residual
        /// </summary>
        /// <param name="cod_riesgo">Código del sistema de riesgo</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<Matriz> ListarMatrizRResidual(Int64 cod_riesgo, string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            Matriz pRegistro;
            List<Matriz> lstMatriz = new List<Matriz>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //Consultar la matriz existente y aquellos factores de riesgo que no se encuentren registrados en la matriz
                        string sql = @"WITH RESULTADO AS
                                        (
                                          SELECT R.COD_MATRIZ, R.COD_FACTOR, F.DESCRIPCION AS DESC_FACTOR, I.CALIFICACION AS VALOR_RINHERENTE, R.COD_CONTROL, C.DESCRIPCION AS DESC_CONTROL, C.CLASE, 
                                          CASE C.CLASE WHEN 1 THEN 'PREVENTIVO' WHEN 2 THEN 'DETECTIVO' WHEN 3 THEN 'CORRECTIVO' ELSE NULL END AS NOM_CLASE, R.FORMA, R.VALOR_CONTROL, 
                                          R.VALOR_RRESIDUAL, R.COD_CAUSA, U.DESCRIPCION AS DESC_CAUSA, R.EJECUCION, R.DOCUMENTACION, R.COMPLEJIDAD, R.FALLAS, R.NIVEL_REDUCCION
                                          FROM GR_MATRIZ_RRESIDUAL R 
                                          LEFT JOIN GR_MATRIZ_RINHERENTE H ON R.COD_CAUSA = H.COD_CAUSA AND R.COD_FACTOR = H.COD_FACTOR
                                          LEFT JOIN GR_MATRIZ_CALOR I ON H.COD_PROBABILIDAD = I.COD_PROBABILIDAD AND H.COD_IMPACTO = I.COD_IMPACTO
                                          LEFT JOIN GR_FACTOR_RIESGO F ON R.COD_FACTOR  = F.COD_FACTOR
                                          LEFT JOIN GR_FORMA_CONTROL C ON R.COD_CONTROL = C.COD_CONTROL
                                          LEFT JOIN GR_CAUSA_RIESGO  U ON R.COD_CAUSA   = U.COD_CAUSA ";

                        if (cod_riesgo > 0)
                            sql += " WHERE R.COD_RIESGO = " + cod_riesgo + @"
                                          UNION 
                                          SELECT 0, F.COD_FACTOR, F.DESCRIPCION AS DESC_FACTOR, PROMEDIO_FACTOR_RIESGO(F.COD_FACTOR) AS VALOR_RINHERENTE, 0, '', 0, '', 0, 0, 0, U.COD_CAUSA, U.DESCRIPCION AS DESC_CAUSA, 0, 0, 0, 0, 0
                                          FROM GR_FACTOR_RIESGO F, GR_CAUSA_RIESGO U
                                          WHERE F.COD_FACTOR IN (SELECT DISTINCT x.COD_FACTOR FROM GR_MATRIZ_RINHERENTE x WHERE x.COD_RIESGO = " + cod_riesgo + @" AND x.COD_FACTOR = F.COD_FACTOR AND x.COD_CAUSA = U.COD_CAUSA) 
                                          AND F.COD_FACTOR NOT IN (SELECT COD_FACTOR FROM GR_MATRIZ_RRESIDUAL WHERE COD_RIESGO = " + cod_riesgo + @")
                                        ) SELECT * FROM RESULTADO ";
                        else
                            sql += ") SELECT * FROM RESULTADO ";

                        sql += filtro != null && filtro != "" ? filtro : "";
                        sql += " ORDER BY COD_FACTOR, COD_CAUSA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            pRegistro = new Matriz();
                            if (resultado["COD_MATRIZ"] != DBNull.Value) pRegistro.cod_matriz = Convert.ToInt64(resultado["COD_MATRIZ"]);
                            if (resultado["COD_FACTOR"] != DBNull.Value) pRegistro.cod_factor = Convert.ToInt64(resultado["COD_FACTOR"]);
                            if (resultado["DESC_FACTOR"] != DBNull.Value) pRegistro.desc_factor = Convert.ToString(resultado["DESC_FACTOR"]);
                            if (resultado["COD_CAUSA"] != DBNull.Value) pRegistro.cod_causa = Convert.ToInt64(resultado["COD_CAUSA"]);
                            if (resultado["DESC_CAUSA"] != DBNull.Value) pRegistro.desc_causa = Convert.ToString(resultado["DESC_CAUSA"]);
                            if (resultado["VALOR_RINHERENTE"] != DBNull.Value) pRegistro.valor_rinherente = Convert.ToInt64(resultado["VALOR_RINHERENTE"]);
                            if (resultado["COD_CONTROL"] != DBNull.Value) pRegistro.cod_control = Convert.ToInt64(resultado["COD_CONTROL"]);
                            if (resultado["DESC_CONTROL"] != DBNull.Value) pRegistro.desc_control = Convert.ToString(resultado["DESC_CONTROL"]);
                            if (resultado["CLASE"] != DBNull.Value) pRegistro.clase = Convert.ToInt64(resultado["CLASE"]);
                            if (resultado["NOM_CLASE"] != DBNull.Value) pRegistro.desc_clase = Convert.ToString(resultado["NOM_CLASE"]);
                            if (resultado["FORMA"] != DBNull.Value) pRegistro.forma = Convert.ToInt64(resultado["FORMA"]);

                            if (resultado["EJECUCION"] != DBNull.Value) pRegistro.ejecucion = Convert.ToInt64(resultado["EJECUCION"]);
                            if (resultado["DOCUMENTACION"] != DBNull.Value) pRegistro.documentacion = Convert.ToInt64(resultado["DOCUMENTACION"]);
                            if (resultado["COMPLEJIDAD"] != DBNull.Value) pRegistro.complejidad = Convert.ToInt64(resultado["COMPLEJIDAD"]);
                            if (resultado["FALLAS"] != DBNull.Value) pRegistro.fallas = Convert.ToInt64(resultado["FALLAS"]);
                            if (resultado["NIVEL_REDUCCION"] != DBNull.Value) pRegistro.nivel_reduccion = Convert.ToInt64(resultado["NIVEL_REDUCCION"]);

                            if (resultado["VALOR_CONTROL"] != DBNull.Value) pRegistro.valor_control = Convert.ToInt32(resultado["VALOR_CONTROL"]);
                            if (resultado["VALOR_RRESIDUAL"] != DBNull.Value) pRegistro.valor_rresidual = Convert.ToInt32(resultado["VALOR_RRESIDUAL"]);

                            lstMatriz.Add(pRegistro);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstMatriz;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "ListarMatrizRResidual", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Listar valores para generación de matriz de monitoreo
        /// </summary>
        /// <param name="cod_riesgo"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<Matriz> ListarMatrizMonitoreo(Int64 cod_riesgo, string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            Matriz pRegistro;
            List<Matriz> lstMatriz = new List<Matriz>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT R.COD_MATRIZ, R.COD_FACTOR, F.DESCRIPCION AS DESC_FACTOR, R.VALOR_RINHERENTE, R.VALOR_CONTROL, R.VALOR_RRESIDUAL, R.COD_MONITOREO, 
                                        M.COD_ALERTA, P.DESCRIPCION AS NOM_PERIODICIDAD, A.DESCRIPCION AS NOM_AREA, C.DESCRIPCION AS NOM_CARGO
                                        FROM GR_MATRIZ_RRESIDUAL R 
                                        LEFT JOIN GR_FACTOR_RIESGO F ON R.COD_FACTOR = F.COD_FACTOR
                                        LEFT JOIN GR_MONITOREO M ON R.COD_MONITOREO = M.COD_MONITOREO
                                        LEFT JOIN GR_AREA_FUNCIONAL A ON M.COD_AREA = A.COD_AREA
                                        LEFT JOIN GR_CARGO_ORGANIGRAMA C ON M.COD_CARGO = C.COD_CARGO
                                        LEFT JOIN GR_PERIODICIDAD P ON M.PERIODICIDAD = P.COD_PERIODICIDAD ";
                        if (cod_riesgo > 0)
                            sql += "WHERE R.COD_RIESGO = " + cod_riesgo + " ";

                        sql += filtro != null && filtro != "" ? filtro : "";
                        sql += " ORDER BY COD_FACTOR ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            pRegistro = new Matriz();
                            if (resultado["COD_MATRIZ"] != DBNull.Value) pRegistro.cod_matriz = Convert.ToInt64(resultado["COD_MATRIZ"]);
                            if (resultado["COD_FACTOR"] != DBNull.Value) pRegistro.cod_factor = Convert.ToInt64(resultado["COD_FACTOR"]);
                            if (resultado["DESC_FACTOR"] != DBNull.Value) pRegistro.desc_factor = Convert.ToString(resultado["DESC_FACTOR"]);
                            if (resultado["VALOR_RINHERENTE"] != DBNull.Value) pRegistro.valor_rinherente = Convert.ToInt64(resultado["VALOR_RINHERENTE"]);
                            if (resultado["VALOR_CONTROL"] != DBNull.Value) pRegistro.valor_control = Convert.ToInt32(resultado["VALOR_CONTROL"]);
                            if (resultado["VALOR_RRESIDUAL"] != DBNull.Value) pRegistro.valor_rresidual = Convert.ToInt32(resultado["VALOR_RRESIDUAL"]);
                            if (resultado["COD_MONITOREO"] != DBNull.Value) pRegistro.cod_monitoreo = Convert.ToInt32(resultado["COD_MONITOREO"]);
                            if (resultado["COD_ALERTA"] != DBNull.Value) pRegistro.cod_alerta = Convert.ToInt32(resultado["COD_ALERTA"]);
                            if (resultado["NOM_PERIODICIDAD"] != DBNull.Value) pRegistro.descripcion = Convert.ToString(resultado["NOM_PERIODICIDAD"]);
                            if (resultado["NOM_AREA"] != DBNull.Value) pRegistro.desc_area = Convert.ToString(resultado["NOM_AREA"]);
                            if (resultado["NOM_CARGO"] != DBNull.Value) pRegistro.desc_cargo = Convert.ToString(resultado["NOM_CARGO"]);

                            lstMatriz.Add(pRegistro);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstMatriz;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "ListarMatrizMonitoreo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Listar los sistemas de riesgo con los valores promediados
        /// </summary>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<Matriz> ListarMatrizGlobal(Usuario vUsuario)
        {
            DbDataReader resultado;
            Matriz pRegistro;
            List<Matriz> lstMatriz = new List<Matriz>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT COD_RIESGO, DESCRIPCION, SIGLA, PROMEDIO_RIESGO(2, COD_RIESGO) AS VALOR_RINHERENTE, PROMEDIO_RIESGO(3, COD_RIESGO) AS VALOR_CONTROL, 
                                        PROMEDIO_RIESGO(4, COD_RIESGO) AS VALOR_RRESIDUAL FROM GR_RIESGO_GENERAL";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            pRegistro = new Matriz();
                            if (resultado["COD_RIESGO"] != DBNull.Value) pRegistro.cod_riesgo = Convert.ToInt64(resultado["COD_RIESGO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) pRegistro.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["SIGLA"] != DBNull.Value) pRegistro.nivel = Convert.ToString(resultado["SIGLA"]);
                            if (resultado["VALOR_RINHERENTE"] != DBNull.Value) pRegistro.valor_rinherente = Convert.ToInt32(resultado["VALOR_RINHERENTE"]);
                            if (resultado["VALOR_CONTROL"] != DBNull.Value) pRegistro.valor_control = Convert.ToInt32(resultado["VALOR_CONTROL"]);
                            if (resultado["VALOR_RRESIDUAL"] != DBNull.Value) pRegistro.valor_rresidual = Convert.ToInt32(resultado["VALOR_RRESIDUAL"]);

                            lstMatriz.Add(pRegistro);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstMatriz;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "ListarMatrizRResidual", ex);
                        return null;
                    }
                }
            }
        }

        public string traerRiesgoInherente(string code)
        {
            string nombre = "";
            return nombre;
        }
        /// <summary>
        /// Consultar valores para reporte de evaluación de riesgo
        /// </summary>
        /// <param name="cod_factor">Código del factor de riesgo</param>
        /// <param name="cod_riesgo">Código del sistema de riesgo</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Matriz ConsultarEvaluacionRiesgo(Int64 cod_factor, Int64 cod_riesgo, Usuario vUsuario)
        {
            DbDataReader resultado;
            Matriz pRegistro = pRegistro = new Matriz();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"WITH RESULTADO AS
                                       (
                                        SELECT DISTINCT R.COD_FACTOR, R.COD_RIESGO, R.VALOR_CONTROL, RG.ABREVIATURA, RG.SIGLA, RG.DESCRIPCION AS DESC_SISTEMA, F.DESCRIPCION, R.VALOR_RINHERENTE, R.VALOR_RRESIDUAL, 
                                        CASE C.CLASE WHEN 1 THEN 'PREVENTIVO' WHEN 2 THEN 'DETECTIVO' WHEN 3 THEN 'CORRECTIVO' ELSE NULL END AS NOM_CLASE,
                                        CASE R.FORMA WHEN 1 THEN 'MANUAL' WHEN 2 THEN 'AUTOMÁTICO' WHEN 3 THEN 'SEMIAUTOMÁTICO' ELSE NULL END AS NOM_FORMA, 
                                        CASE M.COD_ALERTA WHEN 1 THEN 'PASIVA' WHEN 2 THEN 'PENDIENTE' WHEN 3 THEN 'ACTIVA' ELSE NULL END AS NOM_ALERTA, 
                                        C.DESCRIPCION AS DESC_CONTROL, A.DESCRIPCION AS DESC_AREA, G.DESCRIPCION AS DESC_CARGO,I.COD_CAUSA, CA.DESCRIPCION AS DESC_CAUSA
                                        FROM GR_MATRIZ_RRESIDUAL R 
                                        INNER JOIN GR_MATRIZ_RINHERENTE I ON R.COD_RIESGO = I.COD_RIESGO
                                        INNER JOIN GR_RIESGO_GENERAL RG ON R.COD_RIESGO = RG.COD_RIESGO
                                        INNER JOIN GR_FACTOR_RIESGO F ON I.COD_FACTOR = F.COD_FACTOR AND F.COD_FACTOR = R.COD_FACTOR
                                        INNER JOIN GR_CAUSA_RIESGO CA ON I.COD_CAUSA= CA.COD_CAUSA
                                        LEFT JOIN GR_FORMA_CONTROL C ON R.COD_CONTROL = C.COD_CONTROL
                                        LEFT JOIN GR_AREA_FUNCIONAL A ON C.COD_AREA = A.COD_AREA
                                        LEFT JOIN GR_CARGO_ORGANIGRAMA G ON C.COD_CARGO = G.COD_CARGO
                                        LEFT JOIN GR_MONITOREO M ON R.COD_MONITOREO = M.COD_MONITOREO
                                       )
                                        SELECT R.*,G.NIVEL_PROBABILIDAD, G.DESCRIPCION AS DESC_PROBABILIDAD, G.DESC_FRECUENCIA, I.NIVEL_IMPACTO, I.IMPACTO_REPUTACIONAL, I.IMPACTO_LEGAL, I.IMPACTO_OPERATIVO, I.CONTAGIO FROM RESULTADO R, GR_PROBABILIDAD G, GR_IMPACTO I 
                                        WHERE G.COD_PROBABILIDAD = PROMEDIO_VALORACION(5, R.COD_FACTOR, R.COD_RIESGO) AND I.COD_IMPACTO = PROMEDIO_VALORACION(6, R.COD_FACTOR, R.COD_RIESGO)
                                        AND R.COD_FACTOR = " + cod_factor + " AND R.COD_RIESGO = " + cod_riesgo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (resultado["COD_FACTOR"] != DBNull.Value) pRegistro.cod_factor = Convert.ToInt64(resultado["COD_FACTOR"]);
                            if (resultado["COD_RIESGO"] != DBNull.Value) pRegistro.cod_riesgo = Convert.ToInt64(resultado["COD_RIESGO"]);
                            if (resultado["ABREVIATURA"] != DBNull.Value) pRegistro.abreviatura = Convert.ToString(resultado["ABREVIATURA"]);
                            if (resultado["SIGLA"] != DBNull.Value) pRegistro.sigla = Convert.ToString(resultado["SIGLA"]);
                            if (resultado["DESC_SISTEMA"] != DBNull.Value) pRegistro.desc_sistema = Convert.ToString(resultado["DESC_SISTEMA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) pRegistro.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR_RINHERENTE"] != DBNull.Value) pRegistro.valor_rinherente = Convert.ToInt32(resultado["VALOR_RINHERENTE"]);
                            if (resultado["VALOR_RRESIDUAL"] != DBNull.Value) pRegistro.valor_rresidual = Convert.ToInt32(resultado["VALOR_RRESIDUAL"]);
                            if (resultado["NOM_CLASE"] != DBNull.Value) pRegistro.desc_clase = Convert.ToString(resultado["NOM_CLASE"]);
                            if (resultado["NOM_FORMA"] != DBNull.Value) pRegistro.desc_forma = Convert.ToString(resultado["NOM_FORMA"]);
                            if (resultado["NOM_ALERTA"] != DBNull.Value) pRegistro.desc_alerta = Convert.ToString(resultado["NOM_ALERTA"]);
                            if (resultado["VALOR_CONTROL"] != DBNull.Value) pRegistro.valor_control = Convert.ToInt32(resultado["VALOR_CONTROL"]);
                            if (resultado["DESC_CONTROL"] != DBNull.Value) pRegistro.desc_control = Convert.ToString(resultado["DESC_CONTROL"]);
                            if (resultado["DESC_AREA"] != DBNull.Value) pRegistro.desc_area = Convert.ToString(resultado["DESC_AREA"]);
                            if (resultado["DESC_CARGO"] != DBNull.Value) pRegistro.desc_cargo = Convert.ToString(resultado["DESC_CARGO"]);
                            if (resultado["NIVEL_PROBABILIDAD"] != DBNull.Value) pRegistro.nivel = Convert.ToString(resultado["NIVEL_PROBABILIDAD"]);
                            if (resultado["DESC_PROBABILIDAD"] != DBNull.Value) pRegistro.desc_probabilidad = Convert.ToString(resultado["DESC_PROBABILIDAD"]);
                            if (resultado["DESC_FRECUENCIA"] != DBNull.Value) pRegistro.frecuencia = Convert.ToString(resultado["DESC_FRECUENCIA"]);
                            if (resultado["NIVEL_IMPACTO"] != DBNull.Value) pRegistro.nivel_impacto = Convert.ToString(resultado["NIVEL_IMPACTO"]);
                            if (resultado["IMPACTO_REPUTACIONAL"] != DBNull.Value) pRegistro.impacto_reputacional = Convert.ToString(resultado["IMPACTO_REPUTACIONAL"]);
                            if (resultado["IMPACTO_LEGAL"] != DBNull.Value) pRegistro.impacto_legal = Convert.ToString(resultado["IMPACTO_LEGAL"]);
                            if (resultado["IMPACTO_OPERATIVO"] != DBNull.Value) pRegistro.impacto_operativo = Convert.ToString(resultado["IMPACTO_OPERATIVO"]);
                            if (resultado["CONTAGIO"] != DBNull.Value) pRegistro.impacto_contagio = Convert.ToString(resultado["CONTAGIO"]);
                            if (resultado["COD_CAUSA"] != DBNull.Value) pRegistro.cod_causa = Convert.ToInt64(resultado["COD_CAUSA"]);
                            if (resultado["DESC_CAUSA"] != DBNull.Value) pRegistro.desc_causa = Convert.ToString(resultado["DESC_CAUSA"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return pRegistro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "ConsultarEvaluacionRiesgo", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Listar causas en base a un filtro
        /// </summary>
        /// <param name="pParametro">FObjeto con datos para el filtro</param>
        /// <param name="filtro">Cadena con filtro en base a las tablas adicionales</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<Matriz> ListarRiesgoXcausa (Int64 cod_factor, Int64 cod_riesgo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Matriz> lstRiesgoXcausa = new List<Matriz>();


            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"WITH RESULTADO AS
                                       (
                                        SELECT  R.COD_FACTOR, R.COD_RIESGO, RG.ABREVIATURA, RG.SIGLA, RG.DESCRIPCION AS DESC_SISTEMA, F.DESCRIPCION, R.VALOR_RINHERENTE, R.VALOR_RRESIDUAL, 
                                        CASE C.CLASE WHEN 1 THEN 'PREVENTIVO' WHEN 2 THEN 'DETECTIVO' WHEN 3 THEN 'CORRECTIVO' ELSE NULL END AS NOM_CLASE,
                                        CASE R.FORMA WHEN 1 THEN 'MANUAL' WHEN 2 THEN 'AUTOMÁTICO' WHEN 3 THEN 'SEMIAUTOMÁTICO' ELSE NULL END AS NOM_FORMA, 
                                        CASE M.COD_ALERTA WHEN 1 THEN 'PASIVA' WHEN 2 THEN 'PENDIENTE' WHEN 3 THEN 'ACTIVA' ELSE NULL END AS NOM_ALERTA, 
                                        C.DESCRIPCION AS DESC_CONTROL, A.DESCRIPCION AS DESC_AREA, G.DESCRIPCION AS DESC_CARGO,I.COD_CAUSA
                                        FROM GR_MATRIZ_RRESIDUAL R 
                                        INNER JOIN GR_MATRIZ_RINHERENTE I ON R.COD_RIESGO = I.COD_RIESGO
                                        INNER JOIN GR_RIESGO_GENERAL RG ON R.COD_RIESGO = RG.COD_RIESGO
                                        INNER JOIN GR_FACTOR_RIESGO F ON I.COD_FACTOR = F.COD_FACTOR AND F.COD_FACTOR = R.COD_FACTOR
                                        INNER JOIN GR_FORMA_CONTROL C ON R.COD_CONTROL = C.COD_CONTROL
                                        INNER JOIN GR_AREA_FUNCIONAL A ON C.COD_AREA = A.COD_AREA
                                        INNER JOIN GR_CARGO_ORGANIGRAMA G ON C.COD_CARGO = G.COD_CARGO
                                        INNER JOIN GR_MONITOREO M ON R.COD_MONITOREO = M.COD_MONITOREO
                                       )
                                        SELECT DISTINCT R.*,G.NIVEL_PROBABILIDAD, G.DESCRIPCION AS DESC_PROBABILIDAD, G.DESC_FRECUENCIA, I.NIVEL_IMPACTO, I.IMPACTO_REPUTACIONAL, I.IMPACTO_LEGAL, I.IMPACTO_OPERATIVO, I.CONTAGIO FROM RESULTADO R, GR_PROBABILIDAD G, GR_IMPACTO I 
                                        WHERE G.COD_PROBABILIDAD = PROMEDIO_VALORACION(5, R.COD_FACTOR, R.COD_RIESGO) AND I.COD_IMPACTO = PROMEDIO_VALORACION(6, R.COD_FACTOR, R.COD_RIESGO)
                                        AND R.COD_FACTOR = " + cod_factor + " AND R.COD_RIESGO = " + cod_riesgo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Matriz pRegistro = new Matriz();
                            if (resultado["COD_FACTOR"] != DBNull.Value) pRegistro.cod_factor = Convert.ToInt64(resultado["COD_FACTOR"]);
                            if (resultado["COD_RIESGO"] != DBNull.Value) pRegistro.cod_riesgo = Convert.ToInt64(resultado["COD_RIESGO"]);
                            if (resultado["ABREVIATURA"] != DBNull.Value) pRegistro.abreviatura = Convert.ToString(resultado["ABREVIATURA"]);
                            if (resultado["SIGLA"] != DBNull.Value) pRegistro.sigla = Convert.ToString(resultado["SIGLA"]);
                            if (resultado["DESC_SISTEMA"] != DBNull.Value) pRegistro.desc_sistema = Convert.ToString(resultado["DESC_SISTEMA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) pRegistro.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR_RINHERENTE"] != DBNull.Value) pRegistro.valor_rinherente = Convert.ToInt32(resultado["VALOR_RINHERENTE"]);
                            if (resultado["VALOR_RRESIDUAL"] != DBNull.Value) pRegistro.valor_rresidual = Convert.ToInt32(resultado["VALOR_RRESIDUAL"]);
                            if (resultado["NOM_CLASE"] != DBNull.Value) pRegistro.desc_clase = Convert.ToString(resultado["NOM_CLASE"]);
                            if (resultado["NOM_FORMA"] != DBNull.Value) pRegistro.desc_forma = Convert.ToString(resultado["NOM_FORMA"]);
                            if (resultado["NOM_ALERTA"] != DBNull.Value) pRegistro.desc_alerta = Convert.ToString(resultado["NOM_ALERTA"]);
                            if (resultado["DESC_CONTROL"] != DBNull.Value) pRegistro.desc_control = Convert.ToString(resultado["DESC_CONTROL"]);
                            if (resultado["DESC_AREA"] != DBNull.Value) pRegistro.desc_area = Convert.ToString(resultado["DESC_AREA"]);
                            if (resultado["DESC_CARGO"] != DBNull.Value) pRegistro.desc_cargo = Convert.ToString(resultado["DESC_CARGO"]);
                            if (resultado["NIVEL_PROBABILIDAD"] != DBNull.Value) pRegistro.nivel = Convert.ToString(resultado["NIVEL_PROBABILIDAD"]);
                            if (resultado["DESC_PROBABILIDAD"] != DBNull.Value) pRegistro.desc_probabilidad = Convert.ToString(resultado["DESC_PROBABILIDAD"]);
                            if (resultado["DESC_FRECUENCIA"] != DBNull.Value) pRegistro.frecuencia = Convert.ToString(resultado["DESC_FRECUENCIA"]);
                            if (resultado["NIVEL_IMPACTO"] != DBNull.Value) pRegistro.nivel_impacto = Convert.ToString(resultado["NIVEL_IMPACTO"]);
                            if (resultado["IMPACTO_REPUTACIONAL"] != DBNull.Value) pRegistro.impacto_reputacional = Convert.ToString(resultado["IMPACTO_REPUTACIONAL"]);
                            if (resultado["IMPACTO_LEGAL"] != DBNull.Value) pRegistro.impacto_legal = Convert.ToString(resultado["IMPACTO_LEGAL"]);
                            if (resultado["IMPACTO_OPERATIVO"] != DBNull.Value) pRegistro.impacto_operativo = Convert.ToString(resultado["IMPACTO_OPERATIVO"]);
                            if (resultado["CONTAGIO"] != DBNull.Value) pRegistro.impacto_contagio = Convert.ToString(resultado["CONTAGIO"]);
                            if (resultado["COD_CAUSA"] != DBNull.Value) pRegistro.cod_causa = Convert.ToInt64(resultado["COD_CAUSA"]);
                            lstRiesgoXcausa.Add(pRegistro);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstRiesgoXcausa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "ListarCausas", ex);
                        return null;
                    }
                }
            }
        }
        public List<Matriz> ListarMatrizCalor(Usuario usuario)
        {
            DbDataReader resultado;
            List<Matriz> listaMatrizCalor = new List<Matriz>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        sql = @"SELECT P.COD_PROBABILIDAD, I.COD_IMPACTO, C.CALIFICACION
                                FROM GR_PROBABILIDAD P 
                                LEFT JOIN GR_MATRIZ_CALOR C ON C.COD_PROBABILIDAD = P.COD_PROBABILIDAD 
                                LEFT JOIN GR_IMPACTO I ON C.COD_IMPACTO = I.COD_IMPACTO
                                ORDER BY P.COD_PROBABILIDAD, I.COD_IMPACTO";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Matriz calor = new Matriz();
                            if (resultado["COD_PROBABILIDAD"] != DBNull.Value) calor.cod_probabilidad = Convert.ToInt64(resultado["COD_PROBABILIDAD"]);
                            if (resultado["COD_IMPACTO"] != DBNull.Value) calor.cod_impacto = Convert.ToInt64(resultado["COD_IMPACTO"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) calor.calificacion = Convert.ToInt32(resultado["CALIFICACION"]);
                            listaMatrizCalor.Add(calor);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaMatrizCalor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "ListarMatrizCalor", ex);
                        return null;
                    }
                }
            }
        }
        
        public List<Identificacion> ListarPonderadoFactores(Int32 pFactor, Usuario usuario)
        {
            DbDataReader resultado;
            List<Identificacion> listaMatrizFactor = new List<Identificacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT F.COD_FACTOR, A.ABREVIATURA || '-' || F.COD_FACTOR AS ABREVIATURA, F.DESCRIPCION, ROUND(SUM(I.CALIFICACION_RIESGO)/COUNT(F.COD_FACTOR)) AS INHERENNTE,
                                      ROUND(SUM(R.VALOR_RRESIDUAL)/COUNT(F.COD_FACTOR)) AS RESIDUAL, ROUND(SUM(R.VALOR_CONTROL)/COUNT(F.COD_FACTOR)) AS VALOR_CONTROL
                                      FROM GR_FACTOR_RIESGO F
                                      INNER JOIN GR_RIESGO_GENERAL A ON F.COD_RIESGO = A.COD_RIESGO
                                      LEFT JOIN GR_MATRIZ_RINHERENTE I ON F.COD_FACTOR = I.COD_FACTOR
                                      LEFT JOIN GR_MATRIZ_RRESIDUAL R ON F.COD_FACTOR = R.COD_FACTOR
                                      INNER JOIN GR_CAUSA_RIESGO C ON I.COD_CAUSA = C.COD_CAUSA
                                ";
                        if (pFactor != 0)
                            sql += "WHERE F.COD_FACTOR = " + pFactor;

                        sql += "GROUP BY F.COD_FACTOR, F.DESCRIPCION, A.ABREVIATURA || '-' || F.COD_FACTOR ORDER BY F.COD_FACTOR ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Identificacion entidad = new Identificacion();
                            if (resultado["COD_FACTOR"] != DBNull.Value) entidad.cod_factor = Convert.ToInt64(resultado["COD_FACTOR"]);
                            if (resultado["ABREVIATURA"] != DBNull.Value) entidad.abreviatura = Convert.ToString(resultado["ABREVIATURA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["INHERENNTE"] != DBNull.Value) entidad.nom_subproceso = Convert.ToString(resultado["INHERENNTE"]);
                            if (resultado["RESIDUAL"] != DBNull.Value) entidad.nom_factor = Convert.ToString(resultado["RESIDUAL"]);
                            if (resultado["VALOR_CONTROL"] != DBNull.Value) entidad.sigla = Convert.ToString(resultado["VALOR_CONTROL"]);
                            listaMatrizFactor.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaMatrizFactor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "ListarPonderadoFactores", ex);
                        return null;
                    }
                }
            }
        }
        public Matriz ModificarMatrizCalor(Matriz eachCalor, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_impacto = cmdTransaccionFactory.CreateParameter();
                        p_cod_impacto.ParameterName = "P_COD_IMPACTO";
                        p_cod_impacto.Value = eachCalor.cod_impacto;
                        p_cod_impacto.Direction = ParameterDirection.Input;
                        p_cod_impacto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_impacto);

                        DbParameter p_cod_probabilidad = cmdTransaccionFactory.CreateParameter();
                        p_cod_probabilidad.ParameterName = "P_COD_PROBABILIDAD";
                        p_cod_probabilidad.Value = eachCalor.cod_probabilidad;
                        p_cod_probabilidad.Direction = ParameterDirection.Input;
                        p_cod_probabilidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_probabilidad);

                        DbParameter p_calificacion = cmdTransaccionFactory.CreateParameter();
                        p_calificacion.ParameterName = "P_CALIFICACION";
                        p_calificacion.Value = eachCalor.calificacion;
                        p_calificacion.Direction = ParameterDirection.Input;
                        p_calificacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_calificacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GR_MATRIZ_CALOR_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return eachCalor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "ModificarMatrizCalor", ex);
                        return null;
                    }
                }
            }
        }

        public List<Matriz> ListarRangosMatrizRiesgo(Usuario usuario)
        {
            DbDataReader resultado;
            List<Matriz> listaMatrizFactor = new List<Matriz>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM GR_RANGO_MATRIZ_RIESGO";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Matriz entidad = new Matriz();
                            if (resultado["COD_RANGO"] != DBNull.Value) entidad.cod_matriz = Convert.ToInt64(resultado["COD_RANGO"]);
                            if (resultado["VALORACION"] != DBNull.Value) entidad.calificacion = Convert.ToInt32(resultado["VALORACION"]);
                            if (resultado["RANGO_MINIMO"] != DBNull.Value) entidad.rango_minimo = Convert.ToInt32(resultado["RANGO_MINIMO"]);
                            if (resultado["RANGO_MAXIMO"] != DBNull.Value) entidad.rango_maximo = Convert.ToInt32(resultado["RANGO_MAXIMO"]);
                            listaMatrizFactor.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaMatrizFactor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "ListarRangosMatrizRiesgo", ex);
                        return null;
                    }
                }
            }
        }
        public Matriz ModificarRangoMatrizRiesgo(Matriz eachRango, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_rango = cmdTransaccionFactory.CreateParameter();
                        p_cod_rango.ParameterName = "P_COD_RANGO";
                        p_cod_rango.Value = Convert.ToInt32(eachRango.cod_matriz);
                        p_cod_rango.Direction = ParameterDirection.Input;
                        p_cod_rango.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_rango);

                        DbParameter p_rango_minimo = cmdTransaccionFactory.CreateParameter();
                        p_rango_minimo.ParameterName = "P_RANGO_MINIMO";
                        p_rango_minimo.Value = eachRango.rango_minimo;
                        p_rango_minimo.Direction = ParameterDirection.Input;
                        p_rango_minimo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_rango_minimo);

                        DbParameter p_rango_maximo = cmdTransaccionFactory.CreateParameter();
                        p_rango_maximo.ParameterName = "P_RANGO_MAXIMO";
                        p_rango_maximo.Value = eachRango.rango_maximo;
                        p_rango_maximo.Direction = ParameterDirection.Input;
                        p_rango_maximo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_rango_maximo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_RANGO_RIESGO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return eachRango;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "ModificarRangoMatrizRiesgo", ex);
                        return null;
                    }
                }
            }
        }

        public Int32 calcularValoracionRango(Int32 val, Usuario usuario)
        {
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT VALORACION FROM GR_RANGO_MATRIZ_RIESGO WHERE RANGO_MINIMO <= " + val+" AND RANGO_MAXIMO >="+val+"";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if(resultado.Read())
                        {
                            if (resultado["VALORACION"] != DBNull.Value) val = Convert.ToInt32(resultado["VALORACION"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return val;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "calcularValoracionRango", ex);
                        return 0;
                    }
                }
            }
        }

        public FormaControl ModificarFormaControlPuntaje(FormaControl eachCalor, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_cod_formacontrol = cmdTransaccionFactory.CreateParameter();
                        p_cod_formacontrol.ParameterName = "P_COD_FORMACONTROL";
                        p_cod_formacontrol.Value = eachCalor.cod_formacontrol;
                        p_cod_formacontrol.Direction = ParameterDirection.Input;
                        p_cod_formacontrol.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_formacontrol);

                        DbParameter p_cod_atr = cmdTransaccionFactory.CreateParameter();
                        p_cod_atr.ParameterName = "P_COD_ATR";
                        p_cod_atr.Value = eachCalor.cod_atributo;
                        p_cod_atr.Direction = ParameterDirection.Input;
                        p_cod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_atr);

                        DbParameter p_cod_opcion = cmdTransaccionFactory.CreateParameter();
                        p_cod_opcion.ParameterName = "P_COD_OPCION";
                        p_cod_opcion.Value = eachCalor.cod_opcion;
                        p_cod_opcion.Direction = ParameterDirection.Input;
                        p_cod_opcion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_opcion);

                        DbParameter p_valor = cmdTransaccionFactory.CreateParameter();
                        p_valor.ParameterName = "P_VALOR";
                        p_valor.Value = eachCalor.valor;
                        p_valor.Direction = ParameterDirection.Input;
                        p_valor.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_valor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GR_FORMA_PUNT_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return eachCalor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "ModificarFormaControlPuntaje", ex);
                        return null;
                    }
                }
            }
        }

        public int ConsultarPuntajeFormaControl(FormaControl pItem, Usuario usuario)
        {
            DbDataReader resultado;
            int valor = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM GR_RANGO_FORMA_CONTROL" + ObtenerFiltro(pItem);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) valor = Convert.ToInt32(resultado["VALOR"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "ConsultarPuntajeFormaControl", ex);
                        return valor;
                    }
                }
            }
        }

        public string ConsultarValoracionFormaControl(int pPuntaje, ref int pValor, Usuario pUsuario)
        {
            DbDataReader resultado;
            pValor = 0;
            string calificacion = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM GR_VALORACION_CONTROL WHERE " + pPuntaje.ToString() + " BETWEEN RANGO_MINIMO AND RANGO_MAXIMO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) pValor = Convert.ToInt32(resultado["VALOR"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) calificacion = Convert.ToString(resultado["CALIFICACION"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return calificacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizData", "ConsultarValoracionFormaControl", ex);
                        return calificacion;
                    }
                }
            }
        }

        public Int64 PuntajeMaximoMatrizRiesgo(Usuario pUsuario)
        {
            DbDataReader resultado;
            Int64 _puntaje_maximo = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"With w_puntajes as (Select cod_atributo, Max(valor) As valor From GR_RANGO_FORMA_CONTROL Group by cod_atributo
                                                            Union All 
                                                           Select -1, Max(grado_aceptacion) From GR_FORMA_CONTROL)
                                        Select sum(valor) As PUNTAJE_MAXIMO From w_puntajes";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["PUNTAJE_MAXIMO"] != DBNull.Value) _puntaje_maximo = Convert.ToInt64(resultado["PUNTAJE_MAXIMO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return _puntaje_maximo;
                    }
                    catch 
                    {                        
                        return _puntaje_maximo;
                    }
                }
            }
        }



    }
}

