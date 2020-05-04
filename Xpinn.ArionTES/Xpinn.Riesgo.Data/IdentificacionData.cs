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
    public class IdentificacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor para el acceso a base de datos
        /// </summary>
        public IdentificacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crear un registro del proceso de una entidad
        /// </summary>
        /// <param name="pProceso">Objeto con los datos del proceso</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion CrearProcesoEntidad(Identificacion pProceso, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_proceso = cmdTransaccionFactory.CreateParameter();
                        p_cod_proceso.ParameterName = "p_cod_proceso";
                        p_cod_proceso.Value = pProceso.cod_proceso;
                        p_cod_proceso.Direction = ParameterDirection.Output;
                        p_cod_proceso.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_proceso);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pProceso.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_PROCESOE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pProceso, "GR_PROCESO_ENTIDAD", vUsuario, Accion.Crear.ToString(), TipoAuditoria.GestionRiesgo, "Creacion de proceso entidad " + pProceso.cod_proceso); //REGISTRO DE AUDITORIA

                        pProceso.cod_proceso = Convert.ToInt64(p_cod_proceso.Value);
                        return pProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "CrearProcesoEntidad", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modificar un registro del proceso de una entidad
        /// </summary>
        /// <param name="pProceso">Objeto con los datos del proceso</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ModificarProcesoEntidad(Identificacion pProceso, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_proceso = cmdTransaccionFactory.CreateParameter();
                        p_cod_proceso.ParameterName = "p_cod_proceso";
                        p_cod_proceso.Value = pProceso.cod_proceso;
                        p_cod_proceso.Direction = ParameterDirection.Input;
                        p_cod_proceso.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_proceso);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pProceso.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_PROCESOE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pProceso, "GR_PROCESO_ENTIDAD", vUsuario, Accion.Modificar.ToString(), TipoAuditoria.GestionRiesgo, "Modificacion de proceso entidad " + pProceso.cod_proceso); //REGISTRO DE AUDITORIA

                        return pProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "ModificarProcesoEntidad", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Eliminar un registro del proceso de una entidad
        /// </summary>
        /// <param name="pProceso">Objeto con el código del proceso</param>
        /// <param name="vUsuario"></param>
        public void EliminarProcesoEntidad(Identificacion pProceso, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_proceso = cmdTransaccionFactory.CreateParameter();
                        p_cod_proceso.ParameterName = "p_cod_proceso";
                        p_cod_proceso.Value = pProceso.cod_proceso;
                        p_cod_proceso.Direction = ParameterDirection.Input;
                        p_cod_proceso.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_proceso);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_PROCESOE_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pProceso, "GR_PROCESO_ENTIDAD", vUsuario, Accion.Eliminar.ToString(), TipoAuditoria.GestionRiesgo, "Eliminación de proceso entidad " + pProceso.cod_proceso); //REGISTRO DE AUDITORIA

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "EliminarProcesoEntidad", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Crear registro del subproceso de una entidad
        /// </summary>
        /// <param name="pSubProceso">Objeto con los datos del subproceso</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion CrearSubProcesoEntidad(Identificacion pSubProceso, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_subproceso = cmdTransaccionFactory.CreateParameter();
                        p_cod_subproceso.ParameterName = "p_cod_subproceso";
                        p_cod_subproceso.Value = pSubProceso.cod_subproceso;
                        p_cod_subproceso.Direction = ParameterDirection.Output;
                        p_cod_subproceso.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_subproceso);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pSubProceso.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_cod_proceso = cmdTransaccionFactory.CreateParameter();
                        p_cod_proceso.ParameterName = "p_cod_proceso";
                        p_cod_proceso.Value = pSubProceso.cod_proceso;
                        p_cod_proceso.Direction = ParameterDirection.Input;
                        p_cod_proceso.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_proceso);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_SUBPROCESO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pSubProceso, "GR_SUBPROCESO_ENTIDAD", vUsuario, Accion.Crear.ToString(), TipoAuditoria.GestionRiesgo, "Creacion de subproceso entidad " + pSubProceso.cod_proceso); //REGISTRO DE AUDITORIA

                        pSubProceso.cod_subproceso = Convert.ToInt64(p_cod_subproceso.Value);
                        return pSubProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "CrearSubProcesoEntidad", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modificar registro del subproceso de una entidad
        /// </summary>
        /// <param name="pSubProceso">Objeto con los datos del subproceso</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ModificarSubProcesoEntidad(Identificacion pSubProceso, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_subproceso = cmdTransaccionFactory.CreateParameter();
                        p_cod_subproceso.ParameterName = "p_cod_subproceso";
                        p_cod_subproceso.Value = pSubProceso.cod_subproceso;
                        p_cod_subproceso.Direction = ParameterDirection.Input;
                        p_cod_subproceso.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_subproceso);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pSubProceso.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_cod_proceso = cmdTransaccionFactory.CreateParameter();
                        p_cod_proceso.ParameterName = "p_cod_proceso";
                        p_cod_proceso.Value = pSubProceso.cod_proceso;
                        p_cod_proceso.Direction = ParameterDirection.Input;
                        p_cod_proceso.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_proceso);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_SUBPROCESO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pSubProceso, "GR_SUBPROCESO_ENTIDAD", vUsuario, Accion.Modificar.ToString(), TipoAuditoria.GestionRiesgo, "Modificacion de subproceso entidad " + pSubProceso.cod_proceso); //REGISTRO DE AUDITORIA

                        return pSubProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "ModificarSubProcesoEntidad", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Eliminar registro del subproceso de una entidad
        /// </summary>
        /// <param name="pSubProceso">Objeto con el código del subproceso</param>
        /// <param name="vUsuario"></param>
        public void EliminarSubProcesoEntidad(Identificacion pSubProceso, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_subproceso = cmdTransaccionFactory.CreateParameter();
                        p_cod_subproceso.ParameterName = "p_cod_subproceso";
                        p_cod_subproceso.Value = pSubProceso.cod_subproceso;
                        p_cod_subproceso.Direction = ParameterDirection.Input;
                        p_cod_subproceso.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_subproceso);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_SUBPROCESO_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pSubProceso, "GR_SUBPROCESO_ENTIDAD", vUsuario, Accion.Eliminar.ToString(), TipoAuditoria.GestionRiesgo, "Eliminación de subproceso entidad " + pSubProceso.cod_subproceso); //REGISTRO DE AUDITORIA

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "EliminarSubProcesoEntidad", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Crear registro de un area funcional en la entidad
        /// </summary>
        /// <param name="pArea">Objeto con los datos del area</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion CrearAreaFuncional(Identificacion pArea, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_area = cmdTransaccionFactory.CreateParameter();
                        p_cod_area.ParameterName = "p_cod_area";
                        p_cod_area.Value = pArea.cod_area;
                        p_cod_area.Direction = ParameterDirection.Output;
                        p_cod_area.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_area);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pArea.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_AREAF_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pArea, "GR_AREA_FUNCIONAL", vUsuario, Accion.Crear.ToString(), TipoAuditoria.GestionRiesgo, "Creacion de area funcional " + pArea.cod_area); //REGISTRO DE AUDITORIA

                        pArea.cod_area = Convert.ToInt64(p_cod_area.Value);
                        return pArea;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "CrearAreaFuncional", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro de un area funcional en la entidad
        /// </summary>
        /// <param name="pArea">Objeto con los datos del area</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ModificarAreaFuncional(Identificacion pArea, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_area = cmdTransaccionFactory.CreateParameter();
                        p_cod_area.ParameterName = "p_cod_area";
                        p_cod_area.Value = pArea.cod_area;
                        p_cod_area.Direction = ParameterDirection.Input;
                        p_cod_area.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_area);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pArea.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_AREAF_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pArea, "GR_AREA_FUNCIONAL", vUsuario, Accion.Modificar.ToString(), TipoAuditoria.GestionRiesgo, "Modificacion de area funcional" + pArea.cod_area); //REGISTRO DE AUDITORIA

                        return pArea;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "ModificarAreaFuncional", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Eliminar un registro de un area funcional en la entidad
        /// </summary>
        /// <param name="Area">Objeto con el código del area</param>
        /// <param name="vUsuario"></param>
        public void EliminarAreaFuncional(Identificacion Area, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_area = cmdTransaccionFactory.CreateParameter();
                        p_cod_area.ParameterName = "p_cod_area";
                        p_cod_area.Value = Area.cod_area;
                        p_cod_area.Direction = ParameterDirection.Input;
                        p_cod_area.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_area);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_AREAF_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(Area, "GR_AREA_FUNCIONAL", vUsuario, Accion.Eliminar.ToString(), TipoAuditoria.GestionRiesgo, "Eliminación de area funcional " + Area.cod_area); //REGISTRO DE AUDITORIA

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "EliminarAreaFuncional", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Crear registro de un cargo del organigrama de la entidad
        /// </summary>
        /// <param name="pCargo">Objeto con los datos del cargo</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion CrearCargo(Identificacion pCargo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_cargo = cmdTransaccionFactory.CreateParameter();
                        p_cod_cargo.ParameterName = "p_cod_cargo";
                        p_cod_cargo.Value = pCargo.cod_cargo;
                        p_cod_cargo.Direction = ParameterDirection.Output;
                        p_cod_cargo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_cargo);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pCargo.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_CARGO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pCargo, "GR_CARGO_ORGANIGRAMA", vUsuario, Accion.Crear.ToString(), TipoAuditoria.GestionRiesgo, "Creacion un cargo del organigrama " + pCargo.cod_cargo); //REGISTRO DE AUDITORIA

                        pCargo.cod_cargo = Convert.ToInt64(p_cod_cargo.Value);
                        return pCargo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "CrearCargo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro de un cargo del organigrama de la entidad
        /// </summary>
        /// <param name="pCargo">Objeto con los datos del cargo</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ModificarCargo(Identificacion pCargo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_cargo = cmdTransaccionFactory.CreateParameter();
                        p_cod_cargo.ParameterName = "p_cod_cargo";
                        p_cod_cargo.Value = pCargo.cod_cargo;
                        p_cod_cargo.Direction = ParameterDirection.Input;
                        p_cod_cargo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_cargo);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pCargo.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_CARGO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pCargo, "GR_CARGO_ORGANIGRAMA", vUsuario, Accion.Modificar.ToString(), TipoAuditoria.GestionRiesgo, "Modificación de un cargo del organigrama " + pCargo.cod_cargo); //REGISTRO DE AUDITORIA

                        return pCargo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "ModificarCargo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Eliminar un registro de un cargo del organigrama de la entidad
        /// </summary>
        /// <param name="pCargo">Objeto con el código del cargo</param>
        /// <param name="vUsuario"></param>
        public void EliminarCargo(Identificacion pCargo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_cargo = cmdTransaccionFactory.CreateParameter();
                        p_cod_cargo.ParameterName = "p_cod_cargo";
                        p_cod_cargo.Value = pCargo.cod_cargo;
                        p_cod_cargo.Direction = ParameterDirection.Input;
                        p_cod_cargo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_cargo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_CARGO_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pCargo, "GR_CARGO_ORGANIGRAMA", vUsuario, Accion.Eliminar.ToString(), TipoAuditoria.GestionRiesgo, "Eliminación de un cargo del organigrama " + pCargo.cod_cargo); //REGISTRO DE AUDITORIA

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "EliminarCargo", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Crear registro de una causa de riesgo
        /// </summary>
        /// <param name="pCausa">Objeto con los datos de la causa</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion CrearCausa(Identificacion pCausa, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_causa = cmdTransaccionFactory.CreateParameter();
                        p_cod_causa.ParameterName = "p_cod_causa";
                        p_cod_causa.Value = pCausa.cod_causa;
                        p_cod_causa.Direction = ParameterDirection.Output;
                        p_cod_causa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_causa);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pCausa.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_cod_area = cmdTransaccionFactory.CreateParameter();
                        p_cod_area.ParameterName = "p_cod_area";
                        p_cod_area.Value = pCausa.cod_area;
                        p_cod_area.Direction = ParameterDirection.Input;
                        p_cod_area.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cod_area);

                        DbParameter p_cod_cargo = cmdTransaccionFactory.CreateParameter();
                        p_cod_cargo.ParameterName = "p_cod_cargo";
                        p_cod_cargo.Value = pCausa.cod_cargo;
                        p_cod_cargo.Direction = ParameterDirection.Input;
                        p_cod_cargo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cod_cargo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_CAUSA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pCausa, "GR_CAUSA_RIESGO", vUsuario, Accion.Crear.ToString(), TipoAuditoria.GestionRiesgo, "Creacion de una causa " + pCausa.cod_causa); //REGISTRO DE AUDITORIA

                        pCausa.cod_causa = Convert.ToInt64(p_cod_causa.Value);
                        return pCausa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "CrearCausa", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro de una causa de riesgo
        /// </summary>
        /// <param name="pCargo">Objeto con los datos de la causa</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ModificarCausa(Identificacion pCausa, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_causa = cmdTransaccionFactory.CreateParameter();
                        p_cod_causa.ParameterName = "p_cod_causa";
                        p_cod_causa.Value = pCausa.cod_causa;
                        p_cod_causa.Direction = ParameterDirection.Input;
                        p_cod_causa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_causa);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pCausa.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_cod_area = cmdTransaccionFactory.CreateParameter();
                        p_cod_area.ParameterName = "p_cod_area";
                        p_cod_area.Value = pCausa.cod_area;
                        p_cod_area.Direction = ParameterDirection.Input;
                        p_cod_area.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cod_area);

                        DbParameter p_cod_cargo = cmdTransaccionFactory.CreateParameter();
                        p_cod_cargo.ParameterName = "p_cod_cargo";
                        p_cod_cargo.Value = pCausa.cod_cargo;
                        p_cod_cargo.Direction = ParameterDirection.Input;
                        p_cod_cargo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cod_cargo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_CAUSA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pCausa, "GR_CAUSA_RIESGO", vUsuario, Accion.Modificar.ToString(), TipoAuditoria.GestionRiesgo, "Modificación de una causa " + pCausa.cod_causa); //REGISTRO DE AUDITORIA

                        return pCausa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "ModificarCausa", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Eliminar un registro de una causa de riesgo
        /// </summary>
        /// <param name="pCausa">Objeto con el código de la causa</param>
        /// <param name="vUsuario"></param>
        public void EliminarCausa(Identificacion pCausa, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_causa = cmdTransaccionFactory.CreateParameter();
                        p_cod_causa.ParameterName = "p_cod_causa";
                        p_cod_causa.Value = pCausa.cod_causa;
                        p_cod_causa.Direction = ParameterDirection.Input;
                        p_cod_causa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_causa);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_CAUSA_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pCausa, "GR_CARGO_ORGANIGRAMA", vUsuario, Accion.Eliminar.ToString(), TipoAuditoria.GestionRiesgo, "Eliminación de una causa" + pCausa.cod_causa); //REGISTRO DE AUDITORIA

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "EliminarCausa", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Crear registro de un factor de riesgo
        /// </summary>
        /// <param name="pFactor">Objeto con los datos del factor de riesgo</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion CrearFactorRiesgo(Identificacion pFactor, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_factor = cmdTransaccionFactory.CreateParameter();
                        p_cod_factor.ParameterName = "p_cod_factor";
                        p_cod_factor.Value = pFactor.cod_factor;
                        p_cod_factor.Direction = ParameterDirection.Output;
                        p_cod_factor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_factor);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pFactor.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_cod_subproceso = cmdTransaccionFactory.CreateParameter();
                        p_cod_subproceso.ParameterName = "p_cod_subproceso";
                        p_cod_subproceso.Value = pFactor.cod_subproceso;
                        p_cod_subproceso.Direction = ParameterDirection.Input;
                        p_cod_subproceso.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_subproceso);

                        DbParameter p_factor_riesgo = cmdTransaccionFactory.CreateParameter();
                        p_factor_riesgo.ParameterName = "p_factor_riesgo";
                        p_factor_riesgo.Value = pFactor.factor_riesgo;
                        p_factor_riesgo.Direction = ParameterDirection.Input;
                        p_factor_riesgo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_factor_riesgo);

                        DbParameter p_cod_riesgo = cmdTransaccionFactory.CreateParameter();
                        p_cod_riesgo.ParameterName = "p_cod_riesgo";
                        p_cod_riesgo.Value = pFactor.cod_riesgo;
                        p_cod_riesgo.Direction = ParameterDirection.Input;
                        p_cod_riesgo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_riesgo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_FACTOR_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pFactor, "GR_FACTOR_RIESGO", vUsuario, Accion.Crear.ToString(), TipoAuditoria.GestionRiesgo, "Creacion de un factor de riesgo " + pFactor.cod_factor); //REGISTRO DE AUDITORIA

                        pFactor.cod_factor = Convert.ToInt64(p_cod_factor.Value);
                        return pFactor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "CrearFactorRiesgo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro de un factor de riesgo
        /// </summary>
        /// <param name="pFactor">Objeto con los datos del factor</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ModificarFactorRiesgo(Identificacion pFactor, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_factor = cmdTransaccionFactory.CreateParameter();
                        p_cod_factor.ParameterName = "p_cod_factor";
                        p_cod_factor.Value = pFactor.cod_factor;
                        p_cod_factor.Direction = ParameterDirection.Input;
                        p_cod_factor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_factor);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pFactor.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_cod_subproceso = cmdTransaccionFactory.CreateParameter();
                        p_cod_subproceso.ParameterName = "p_cod_subproceso";
                        p_cod_subproceso.Value = pFactor.cod_subproceso;
                        p_cod_subproceso.Direction = ParameterDirection.Input;
                        p_cod_subproceso.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_subproceso);

                        DbParameter p_factor_riesgo = cmdTransaccionFactory.CreateParameter();
                        p_factor_riesgo.ParameterName = "p_factor_riesgo";
                        p_factor_riesgo.Value = pFactor.factor_riesgo;
                        p_factor_riesgo.Direction = ParameterDirection.Input;
                        p_factor_riesgo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_factor_riesgo);

                        DbParameter p_cod_riesgo = cmdTransaccionFactory.CreateParameter();
                        p_cod_riesgo.ParameterName = "p_cod_riesgo";
                        p_cod_riesgo.Value = pFactor.cod_riesgo;
                        p_cod_riesgo.Direction = ParameterDirection.Input;
                        p_cod_riesgo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_riesgo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_FACTOR_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pFactor, "GR_FACTOR_RIESGO", vUsuario, Accion.Modificar.ToString(), TipoAuditoria.GestionRiesgo, "Modificación de un factor de riesgo " + pFactor.cod_factor); //REGISTRO DE AUDITORIA

                        return pFactor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "ModificarFactorRiesgo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Eliminar un registro de un factor de riesgo
        /// </summary>
        /// <param name="pFactor">Objeto con el código del factor</param>
        /// <param name="vUsuario"></param>
        public void EliminarFactorRiesgo(Identificacion pFactor, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_factor = cmdTransaccionFactory.CreateParameter();
                        p_cod_factor.ParameterName = "p_cod_factor";
                        p_cod_factor.Value = pFactor.cod_factor;
                        p_cod_factor.Direction = ParameterDirection.Input;
                        p_cod_factor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_factor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_FACTOR_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pFactor, "GR_FACTOR_RIESGO", vUsuario, Accion.Eliminar.ToString(), TipoAuditoria.GestionRiesgo, "Eliminación de un factor de riesgo " + pFactor.cod_factor); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "EliminarFactorRiesgo", ex);
                    }
                }
            }
        }   

        /// <summary>
        /// Consultar proceso especifico
        /// </summary>
        /// <param name="pParametro">Objeto con datos para realizar el filtro</param>
        /// <param name="TipoParametro">Define cual tabla se va a consultar</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ConsultarProcesoEntidad(Identificacion pParametro, int TipoParametro, Usuario vUsuario)
        {
            DbDataReader resultado;
            Identificacion vProceso = new Identificacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string tabla = "";
                        if (TipoParametro == 1)
                            tabla = "GR_PROCESO_ENTIDAD";
                        else if (TipoParametro == 2)
                            tabla = "GR_SUBPROCESO_ENTIDAD";

                        string sql = "SELECT * FROM " + tabla + ObtenerFiltro(pParametro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PROCESO"] != DBNull.Value) vProceso.cod_proceso = Convert.ToInt64(resultado["COD_PROCESO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) vProceso.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (TipoParametro == 2)
                            {
                                if (resultado["COD_SUBPROCESO"] != DBNull.Value) vProceso.cod_subproceso = Convert.ToInt64(resultado["COD_SUBPROCESO"]);
                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return vProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "ConsultarProcesoEntidad", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Consultar area especifica
        /// </summary>
        /// <param name="pParametro">Objeto con datos para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ConsultarAreasEntidad(Identificacion pParametro, Usuario vUsuario)
        {
            DbDataReader resultado;
            Identificacion vProceso = new Identificacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM GR_AREA_FUNCIONAL " + ObtenerFiltro(pParametro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_AREA"] != DBNull.Value) vProceso.cod_area = Convert.ToInt64(resultado["COD_AREA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) vProceso.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return vProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "ConsultarAreasEntidad", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Consultar cargo especifico
        /// </summary>
        /// <param name="pParametro">Objeto con datos para el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ConsultarCargosEntidad(Identificacion pParametro, Usuario vUsuario)
        {
            DbDataReader resultado;
            Identificacion vProceso = new Identificacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM GR_CARGO_ORGANIGRAMA " + ObtenerFiltro(pParametro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_CARGO"] != DBNull.Value) vProceso.cod_cargo = Convert.ToInt64(resultado["COD_CARGO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) vProceso.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return vProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "ConsultarCargosEntidad", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Consultar causa especifica
        /// </summary>
        /// <param name="pParametro">Objeto con datos para el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ConsultarCausa(Identificacion pParametro, Usuario vUsuario)
        {
            DbDataReader resultado;
            Identificacion vProceso = new Identificacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM GR_CAUSA_RIESGO " + ObtenerFiltro(pParametro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_CAUSA"] != DBNull.Value) vProceso.cod_causa = Convert.ToInt64(resultado["COD_CAUSA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) vProceso.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["COD_AREA"] != DBNull.Value) vProceso.cod_area = Convert.ToInt64(resultado["COD_AREA"]);
                            if (resultado["COD_CARGO"] != DBNull.Value) vProceso.cod_cargo = Convert.ToInt64(resultado["COD_CARGO"]);                            
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return vProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "ConsultarCausa", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Consultar factor de riesgo especifico
        /// </summary>
        /// <param name="pParametro">Objeto con datos para el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ConsultarFactorRiesgo(Identificacion pParametro, Usuario vUsuario)
        {
            DbDataReader resultado;
            Identificacion vProceso = new Identificacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM GR_FACTOR_RIESGO " + ObtenerFiltro(pParametro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_FACTOR"] != DBNull.Value) vProceso.cod_factor = Convert.ToInt64(resultado["COD_FACTOR"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) vProceso.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["COD_SUBPROCESO"] != DBNull.Value) vProceso.cod_subproceso = Convert.ToInt64(resultado["COD_SUBPROCESO"]);
                            if (resultado["FACTOR_RIESGO"] != DBNull.Value) vProceso.factor_riesgo = Convert.ToString(resultado["FACTOR_RIESGO"]);
                            if (resultado["COD_RIESGO"] != DBNull.Value) vProceso.cod_riesgo = Convert.ToInt64(resultado["COD_RIESGO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return vProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "ConsultarFactorRiesgo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Consultar sistema de riesgo
        /// </summary>
        /// <param name="pParametro">Objeto con datos para el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ConsultarSistemaRiesgo(Identificacion pParametro, Usuario vUsuario)
        {
            DbDataReader resultado;
            Identificacion vProceso = new Identificacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM GR_RIESGO_GENERAL " + ObtenerFiltro(pParametro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_RIESGO"] != DBNull.Value) vProceso.cod_factor = Convert.ToInt64(resultado["COD_RIESGO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) vProceso.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["SIGLA"] != DBNull.Value) vProceso.sigla = Convert.ToString(resultado["SIGLA"]);
                            if (resultado["ABREVIATURA"] != DBNull.Value) vProceso.abreviatura = Convert.ToString(resultado["ABREVIATURA"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return vProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "ConsultarSistemaRiesgo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Listar procesos basado en un filtro
        /// </summary>
        /// <param name="pParametro">Objeto con datos para realizar el filtro</param>
        /// <param name="TipoParametro">Define cual tabla se va a consultar</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<Identificacion> ListarProcesosEntidad(Identificacion pParametro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Identificacion> lstProcesos = new List<Identificacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM GR_PROCESO_ENTIDAD " + ObtenerFiltro(pParametro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Identificacion entidad = new Identificacion();
                            if (resultado["COD_PROCESO"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt64(resultado["COD_PROCESO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstProcesos.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProcesos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "ListarProcesosEntidad", ex);
                        return null;
                    }
                }
            }
        }

        public List<Identificacion> ListarSubProcesosEntidad(Identificacion pParametro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Identificacion> lstProcesos = new List<Identificacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT S.*, P.DESCRIPCION AS NOM_PROCESO FROM GR_SUBPROCESO_ENTIDAD S INNER JOIN GR_PROCESO_ENTIDAD P ON S.COD_PROCESO = P.COD_PROCESO " + ObtenerFiltro(pParametro, "S.");

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Identificacion entidad = new Identificacion();
                            if (resultado["COD_SUBPROCESO"] != DBNull.Value) entidad.cod_subproceso = Convert.ToInt64(resultado["COD_SUBPROCESO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["COD_PROCESO"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt64(resultado["COD_PROCESO"]);
                            if (resultado["NOM_PROCESO"] != DBNull.Value) entidad.nom_proceso = Convert.ToString(resultado["NOM_PROCESO"]);

                            lstProcesos.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProcesos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "ListarProcesosEntidad", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Listar areas basado en un filtro
        /// </summary>
        /// <param name="pParametro">Objeto con datos para el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<Identificacion> ListarAreasEntidad(Identificacion pParametro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Identificacion> lstAreas = new List<Identificacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM GR_AREA_FUNCIONAL "+ ObtenerFiltro(pParametro) + " ORDER BY COD_AREA ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Identificacion entidad = new Identificacion();
                            if (resultado["COD_AREA"] != DBNull.Value) entidad.cod_area = Convert.ToInt64(resultado["COD_AREA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstAreas.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAreas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "ListarAreasEntidad", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Listar cargos en base a un filtro
        /// </summary>
        /// <param name="pParametro">Objeto con datos para el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<Identificacion> ListarCargosEntidad(Identificacion pParametro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Identificacion> lstCargos = new List<Identificacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM GR_CARGO_ORGANIGRAMA " + ObtenerFiltro(pParametro) + " ORDER BY COD_CARGO ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Identificacion entidad = new Identificacion();
                            if (resultado["COD_CARGO"] != DBNull.Value) entidad.cod_cargo = Convert.ToInt64(resultado["COD_CARGO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstCargos.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCargos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "ListarCargosEntidad", ex);
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
        public List<Identificacion> ListarCausas(Identificacion pParametro, string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Identificacion> lstCausas = new List<Identificacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT C.COD_CAUSA, C.DESCRIPCION, A.DESCRIPCION AS NOM_AREA, G.DESCRIPCION AS NOM_CARGO
                                        FROM GR_CAUSA_RIESGO C 
                                        INNER JOIN GR_AREA_FUNCIONAL A ON C.COD_AREA = A.COD_AREA
                                        INNER JOIN GR_CARGO_ORGANIGRAMA G ON C.COD_CARGO = G.COD_CARGO " + ObtenerFiltro(pParametro, "C.") + " ORDER BY C.COD_CAUSA ";
                        if (filtro != "")
                            sql += filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Identificacion entidad = new Identificacion();
                            if (resultado["COD_CAUSA"] != DBNull.Value) entidad.cod_causa = Convert.ToInt64(resultado["COD_CAUSA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["NOM_AREA"] != DBNull.Value) entidad.nom_area = Convert.ToString(resultado["NOM_AREA"]);
                            if (resultado["NOM_CARGO"] != DBNull.Value) entidad.nom_cargo = Convert.ToString(resultado["NOM_CARGO"]);
                            lstCausas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCausas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "ListarCausas", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Listar factores de riesgo en base a un filtro
        /// </summary>
        /// <param name="pParametro">FObjeto con datos para el filtro</param>
        /// <param name="filtro">Cadena con filtro en base a las tablas adicionales</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<Identificacion> ListarFactoresRiesgo(Identificacion pParametro, string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Identificacion> lstCargos = new List<Identificacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT F.COD_FACTOR, R.ABREVIATURA ||'-'|| F.COD_FACTOR AS ABREVIATURA, F.DESCRIPCION, S.DESCRIPCION AS NOM_SUBPROCESO, CASE F.FACTOR_RIESGO WHEN 'H' THEN 'HUMANO' WHEN 'T' THEN 'TECNOLOGICO' WHEN 'M' THEN 'MIXTO' END AS NOM_FACTOR,
                                        R.SIGLA, F.COD_RIESGO
                                        FROM GR_FACTOR_RIESGO F 
                                        INNER JOIN GR_SUBPROCESO_ENTIDAD S ON F.COD_SUBPROCESO = S.COD_SUBPROCESO
                                        INNER JOIN GR_RIESGO_GENERAL R ON F.COD_RIESGO = R.COD_RIESGO " + ObtenerFiltro(pParametro, "F.") + " ORDER BY F.COD_FACTOR ";
                        if (filtro != "")
                            sql += filtro;
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
                            if (resultado["NOM_SUBPROCESO"] != DBNull.Value) entidad.nom_subproceso = Convert.ToString(resultado["NOM_SUBPROCESO"]);
                            if (resultado["NOM_FACTOR"] != DBNull.Value) entidad.nom_factor = Convert.ToString(resultado["NOM_FACTOR"]);
                            if (resultado["SIGLA"] != DBNull.Value) entidad.sigla = Convert.ToString(resultado["SIGLA"]);
                            if (resultado["COD_RIESGO"] != DBNull.Value) entidad.cod_riesgo = Convert.ToInt64(resultado["COD_RIESGO"]);
                            lstCargos.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCargos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "ListarFactoresRiesgo", ex);
                        return null;
                    }
                }
            }
        }
    }
}
