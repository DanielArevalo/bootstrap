using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Riesgo.Entities;

namespace Xpinn.Riesgo.Data
{
    public class SeguimientoData : GlobalData
    {
        ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor para el acceso de base de datos
        /// </summary>
        public SeguimientoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crear un registro de una forma de control
        /// </summary>
        /// <param name="pControl">Objeto con los datos de la forma de control</param>
        /// <param name="vUsuario"></param>
        /// <returns>Objeto con el código asignado</returns>
        public Seguimiento CrearFormaControl(Seguimiento pControl, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_control = cmdTransaccionFactory.CreateParameter();
                        p_cod_control.ParameterName = "p_cod_control";
                        p_cod_control.Value = pControl.cod_control;
                        p_cod_control.Direction = ParameterDirection.Output;
                        p_cod_control.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_control);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pControl.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_clase = cmdTransaccionFactory.CreateParameter();
                        p_clase.ParameterName = "p_clase";
                        p_clase.Value = pControl.clase;
                        p_clase.Direction = ParameterDirection.Input;
                        p_clase.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_clase);

                        DbParameter p_cod_area = cmdTransaccionFactory.CreateParameter();
                        p_cod_area.ParameterName = "p_cod_area";
                        p_cod_area.Value = pControl.cod_area;
                        p_cod_area.Direction = ParameterDirection.Input;
                        p_cod_area.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_area);

                        DbParameter p_cod_cargo = cmdTransaccionFactory.CreateParameter();
                        p_cod_cargo.ParameterName = "p_cod_cargo";
                        p_cod_cargo.Value = pControl.cod_cargo;
                        p_cod_cargo.Direction = ParameterDirection.Input;
                        p_cod_cargo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_cargo);

                        DbParameter p_grado_aceptacion = cmdTransaccionFactory.CreateParameter();
                        p_grado_aceptacion.ParameterName = "p_grado_aceptacion";
                        p_grado_aceptacion.Value = pControl.grado_aceptacion;
                        p_grado_aceptacion.Direction = ParameterDirection.Input;
                        p_grado_aceptacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_grado_aceptacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_FCONTROL_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pControl, "GR_FORMA_CONTROL", vUsuario, Accion.Crear.ToString(), TipoAuditoria.GestionRiesgo, "Creacion de forma de control " + pControl.cod_control); //REGISTRO DE AUDITORIA

                        pControl.cod_control = Convert.ToInt64(p_cod_control.Value);
                        return pControl;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SeguimientoData", "CrearFormaControl", ex);
                        return null;
                    }
                }
            }
        }

        ///<summary>
        /// Modificar un registro de una forma de control
        /// </summary>
        /// <param name="pControl">Objeto con los datos de la forma de control</param>
        /// <param name="vUsuario"></param>
        /// <returns>Objeto con el código asignado</returns>
        public Seguimiento ModificarFormaControl(Seguimiento pControl, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_control = cmdTransaccionFactory.CreateParameter();
                        p_cod_control.ParameterName = "p_cod_control";
                        p_cod_control.Value = pControl.cod_control;
                        p_cod_control.Direction = ParameterDirection.Input;
                        p_cod_control.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_control);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pControl.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_clase = cmdTransaccionFactory.CreateParameter();
                        p_clase.ParameterName = "p_clase";
                        p_clase.Value = pControl.clase;
                        p_clase.Direction = ParameterDirection.Input;
                        p_clase.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_clase);

                        DbParameter p_cod_area = cmdTransaccionFactory.CreateParameter();
                        p_cod_area.ParameterName = "p_cod_area";
                        p_cod_area.Value = pControl.cod_area;
                        p_cod_area.Direction = ParameterDirection.Input;
                        p_cod_area.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_area);

                        DbParameter p_cod_cargo = cmdTransaccionFactory.CreateParameter();
                        p_cod_cargo.ParameterName = "p_cod_cargo";
                        p_cod_cargo.Value = pControl.cod_cargo;
                        p_cod_cargo.Direction = ParameterDirection.Input;
                        p_cod_cargo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_cargo);

                        DbParameter p_grado_aceptacion = cmdTransaccionFactory.CreateParameter();
                        p_grado_aceptacion.ParameterName = "p_grado_aceptacion";
                        p_grado_aceptacion.Value = pControl.grado_aceptacion;
                        p_grado_aceptacion.Direction = ParameterDirection.Input;
                        p_grado_aceptacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_grado_aceptacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_FCONTROL_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pControl, "GR_FORMA_CONTROL", vUsuario, Accion.Modificar.ToString(), TipoAuditoria.GestionRiesgo, "Modificación de forma de control " + pControl.cod_control); //REGISTRO DE AUDITORIA
                        return pControl;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SeguimientoData", "ModificarFormaControl", ex);
                        return null;
                    }
                }
            }
        }

        ///<summary>
        /// Eliminar un registro de una forma de control
        /// </summary>
        /// <param name="pControl">Objeto con el código de la forma de control</param>
        /// <param name="vUsuario"></param>
        public void EliminarFormaControl(Seguimiento pControl, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_control = cmdTransaccionFactory.CreateParameter();
                        p_cod_control.ParameterName = "p_cod_control";
                        p_cod_control.Value = pControl.cod_control;
                        p_cod_control.Direction = ParameterDirection.Input;
                        p_cod_control.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_control);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_FCONTROL_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pControl, "GR_FORMA_CONTROL", vUsuario, Accion.Eliminar.ToString(), TipoAuditoria.GestionRiesgo, "Eliminación de forma de control " + pControl.cod_control); //REGISTRO DE AUDITORIA                       
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SeguimientoData", "EliminarFormaControl", ex);
                    }
                }
            }
        }

        ///<summary>
        /// Crear un registro de un tipo de monitoreo
        /// </summary>
        /// <param name="pControl">Objeto con los datos del tipo de monitoreo</param>
        /// <param name="vUsuario"></param>
        /// <returns>Objeto con el código asignado</returns>
        public Seguimiento CrearTipoMonitoreo(Seguimiento pMonitoreo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_monitoreo = cmdTransaccionFactory.CreateParameter();
                        p_cod_monitoreo.ParameterName = "p_cod_monitoreo";
                        p_cod_monitoreo.Value = pMonitoreo.cod_monitoreo;
                        p_cod_monitoreo.Direction = ParameterDirection.Output;
                        p_cod_monitoreo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_monitoreo);

                        DbParameter p_cod_alerta = cmdTransaccionFactory.CreateParameter();
                        p_cod_alerta.ParameterName = "p_cod_alerta";
                        p_cod_alerta.Value = pMonitoreo.cod_alerta;
                        p_cod_alerta.Direction = ParameterDirection.Input;
                        p_cod_alerta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_alerta);

                        DbParameter p_periodicidad = cmdTransaccionFactory.CreateParameter();
                        p_periodicidad.ParameterName = "p_periodicidad";
                        p_periodicidad.Value = pMonitoreo.periodicidad;
                        p_periodicidad.Direction = ParameterDirection.Input;
                        p_periodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_periodicidad);

                        DbParameter p_cod_area = cmdTransaccionFactory.CreateParameter();
                        p_cod_area.ParameterName = "p_cod_area";
                        p_cod_area.Value = pMonitoreo.cod_area;
                        p_cod_area.Direction = ParameterDirection.Input;
                        p_cod_area.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_area);

                        DbParameter p_cod_cargo = cmdTransaccionFactory.CreateParameter();
                        p_cod_cargo.ParameterName = "p_cod_cargo";
                        p_cod_cargo.Value = pMonitoreo.cod_cargo;
                        p_cod_cargo.Direction = ParameterDirection.Input;
                        p_cod_cargo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_cargo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_MONITOREO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pMonitoreo, "GR_MONITOREO", vUsuario, Accion.Crear.ToString(), TipoAuditoria.GestionRiesgo, "Creacion de tipo de monitoreo " + pMonitoreo.cod_monitoreo); //REGISTRO DE AUDITORIA

                        pMonitoreo.cod_monitoreo = Convert.ToInt64(p_cod_monitoreo.Value);
                        return pMonitoreo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SeguimientoData", "CrearTipoMonitoreo", ex);
                        return null;
                    }
                }
            }
        }

        ///<summary>
        /// Modificar un registro de un tipo de monitoreo
        /// </summary>
        /// <param name="pControl">Objeto con los datos del tipo de monitoreo</param>
        /// <param name="vUsuario"></param>
        /// <returns>Objeto con los datos modificados</returns>
        public Seguimiento ModificarTipoMonitoreo(Seguimiento pMonitoreo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_monitoreo = cmdTransaccionFactory.CreateParameter();
                        p_cod_monitoreo.ParameterName = "p_cod_monitoreo";
                        p_cod_monitoreo.Value = pMonitoreo.cod_monitoreo;
                        p_cod_monitoreo.Direction = ParameterDirection.Input;
                        p_cod_monitoreo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_monitoreo);

                        DbParameter p_cod_alerta = cmdTransaccionFactory.CreateParameter();
                        p_cod_alerta.ParameterName = "p_cod_alerta";
                        p_cod_alerta.Value = pMonitoreo.cod_alerta;
                        p_cod_alerta.Direction = ParameterDirection.Input;
                        p_cod_alerta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_alerta);

                        DbParameter p_periodicidad = cmdTransaccionFactory.CreateParameter();
                        p_periodicidad.ParameterName = "p_periodicidad";
                        p_periodicidad.Value = pMonitoreo.periodicidad;
                        p_periodicidad.Direction = ParameterDirection.Input;
                        p_periodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_periodicidad);

                        DbParameter p_cod_area = cmdTransaccionFactory.CreateParameter();
                        p_cod_area.ParameterName = "p_cod_area";
                        p_cod_area.Value = pMonitoreo.cod_area;
                        p_cod_area.Direction = ParameterDirection.Input;
                        p_cod_area.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_area);

                        DbParameter p_cod_cargo = cmdTransaccionFactory.CreateParameter();
                        p_cod_cargo.ParameterName = "p_cod_cargo";
                        p_cod_cargo.Value = pMonitoreo.cod_cargo;
                        p_cod_cargo.Direction = ParameterDirection.Input;
                        p_cod_cargo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_cargo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_MONITOREO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pMonitoreo, "GR_MONITOREO", vUsuario, Accion.Modificar.ToString(), TipoAuditoria.GestionRiesgo, "Modificación de tipo de monitoreo " + pMonitoreo.cod_monitoreo); //REGISTRO DE AUDITORIA
                        return pMonitoreo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SeguimientoData", "ModificarTipoMonitoreo", ex);
                        return null;
                    }
                }
            }
        }

        ///<summary>
        /// Eliminar un registro de un tipo de monitoreo
        /// </summary>
        /// <param name="pControl">Objeto con el código del tipo de monitoreo</param>
        /// <param name="vUsuario"></param>
        public void EliminarTipoMonitoreo(Seguimiento pMonitoreo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_monitoreo = cmdTransaccionFactory.CreateParameter();
                        p_cod_monitoreo.ParameterName = "p_cod_monitoreo";
                        p_cod_monitoreo.Value = pMonitoreo.cod_monitoreo;
                        p_cod_monitoreo.Direction = ParameterDirection.Input;
                        p_cod_monitoreo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_monitoreo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_MONITOREO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pMonitoreo, "GR_MONITOREO", vUsuario, Accion.Eliminar.ToString(), TipoAuditoria.GestionRiesgo, "Eliminación de tipo de monitoreo " + pMonitoreo.cod_monitoreo); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SeguimientoData", "EliminarTipoMonitoreo", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Consultar los datos de una forma de control especifica
        /// </summary>
        /// <param name="pParametro">Objetos con los datos para el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns>Objeto con los datos obtenidos</returns>
        public Seguimiento ConsultarFormaControl(Seguimiento pParametro, Usuario vUsuario)
        {
            DbDataReader resultado;
            Seguimiento vFormaControl = new Seguimiento();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT COD_CONTROL, DESCRIPCION, CLASE, CASE CLASE WHEN 1 THEN 'PREVENTIVO' WHEN 2 THEN 'DETECTIVO' WHEN 3 THEN 'CORRECTIVO' ELSE NULL END AS NOM_CLASE, 
                                        COD_AREA, COD_CARGO, GRADO_ACEPTACION FROM GR_FORMA_CONTROL " + ObtenerFiltro(pParametro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_CONTROL"] != DBNull.Value) vFormaControl.cod_control = Convert.ToInt64(resultado["COD_CONTROL"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) vFormaControl.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CLASE"] != DBNull.Value) vFormaControl.clase = Convert.ToInt64(resultado["CLASE"]);
                            if (resultado["NOM_CLASE"] != DBNull.Value) vFormaControl.nom_clase = Convert.ToString(resultado["NOM_CLASE"]);
                            if (resultado["COD_AREA"] != DBNull.Value) vFormaControl.cod_area = Convert.ToInt64(resultado["COD_AREA"]);
                            if (resultado["COD_CARGO"] != DBNull.Value) vFormaControl.cod_cargo = Convert.ToInt64(resultado["COD_CARGO"]);
                            if (resultado["GRADO_ACEPTACION"] != DBNull.Value) vFormaControl.grado_aceptacion = Convert.ToInt32(resultado["GRADO_ACEPTACION"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return vFormaControl;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SeguimientoData", "ConsultarFormaControl", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Consultar tipo de monitoreo especifico
        /// </summary>
        /// <param name="pParametro">Objeto con los datos para el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns>Objeto con los datos obtenidos</returns>
        public Seguimiento ConsultarTipoMonitoreo(Seguimiento pParametro, Usuario vUsuario)
        {
            DbDataReader resultado;
            Seguimiento vProceso = new Seguimiento();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM GR_MONITOREO " + ObtenerFiltro(pParametro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_MONITOREO"] != DBNull.Value) vProceso.cod_monitoreo = Convert.ToInt64(resultado["COD_MONITOREO"]);
                            if (resultado["COD_ALERTA"] != DBNull.Value) vProceso.cod_alerta = Convert.ToInt32(resultado["COD_ALERTA"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) vProceso.periodicidad = Convert.ToInt32(resultado["PERIODICIDAD"]);
                            if (resultado["COD_AREA"] != DBNull.Value) vProceso.cod_area = Convert.ToInt64(resultado["COD_AREA"]);
                            if (resultado["COD_CARGO"] != DBNull.Value) vProceso.cod_cargo = Convert.ToInt64(resultado["COD_CARGO"]);
                            
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return vProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SeguimientoData", "ConsultarTipoMonitoreo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Lista de formas de control bajo un filtro especifico
        /// </summary>
        /// <param name="pParametro">Objeto con datos para el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns>Lista con los objetos correspondientes al filtro</returns>
        public List<Seguimiento> ListarFormasControl(Seguimiento pParametro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Seguimiento> lstFormasControl = new List<Seguimiento>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT F.COD_CONTROL, F.DESCRIPCION, CASE F.CLASE WHEN 1 THEN 'PREVENTIVO' WHEN 2 THEN 'DETECTIVO' WHEN 3 THEN 'CORRECTIVO' ELSE NULL END AS NOM_CLASE,
                                        A.DESCRIPCION AS NOM_AREA, C.DESCRIPCION AS NOM_CARGO, F.GRADO_ACEPTACION
                                        FROM GR_FORMA_CONTROL F
                                        INNER JOIN GR_AREA_FUNCIONAL A ON F.COD_AREA = A.COD_AREA
                                        INNER JOIN GR_CARGO_ORGANIGRAMA C ON F.COD_CARGO = C.COD_CARGO " + ObtenerFiltro(pParametro, "F.") + " ORDER BY F.COD_CONTROL";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Seguimiento entidad = new Seguimiento();
                            if (resultado["COD_CONTROL"] != DBNull.Value) entidad.cod_control = Convert.ToInt64(resultado["COD_CONTROL"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["NOM_CLASE"] != DBNull.Value) entidad.nom_clase = Convert.ToString(resultado["NOM_CLASE"]);
                            if (resultado["NOM_AREA"] != DBNull.Value) entidad.nom_area = Convert.ToString(resultado["NOM_AREA"]);
                            if (resultado["NOM_CARGO"] != DBNull.Value) entidad.nom_cargo = Convert.ToString(resultado["NOM_CARGO"]);
                            if (resultado["GRADO_ACEPTACION"] != DBNull.Value) entidad.grado_aceptacion = Convert.ToInt32(resultado["GRADO_ACEPTACION"]);
                            lstFormasControl.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstFormasControl;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SeguimientoData", "ListarFormasControl", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Lista de tipos de monitoreo bajo un filtro especifico
        /// </summary>
        /// <param name="pParametro">Objeto con datos para el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns>Lista con los objetos correspondientes al filtro</returns>
        public List<Seguimiento> ListarTiposMonitoreo(Seguimiento pParametro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Seguimiento> lstTiposMonitoreo = new List<Seguimiento>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT M.COD_MONITOREO, M.COD_ALERTA, CASE M.COD_ALERTA WHEN 1 THEN 'PASIVA' WHEN 2 THEN 'PENDIENTE' WHEN 3 THEN 'ACTIVA' ELSE NULL END AS NOM_ALERTA,
                                        P.DESCRIPCION AS NOM_PERIODICIDAD, A.DESCRIPCION AS NOM_AREA, C.DESCRIPCION AS NOM_CARGO
                                        FROM GR_MONITOREO M 
                                        INNER JOIN GR_AREA_FUNCIONAL A ON M.COD_AREA = A.COD_AREA
                                        INNER JOIN GR_CARGO_ORGANIGRAMA C ON M.COD_CARGO = C.COD_CARGO
                                        INNER JOIN GR_PERIODICIDAD P ON M.PERIODICIDAD = P.COD_PERIODICIDAD " + ObtenerFiltro(pParametro, "M.");

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Seguimiento entidad = new Seguimiento();
                            if (resultado["COD_MONITOREO"] != DBNull.Value) entidad.cod_monitoreo = Convert.ToInt64(resultado["COD_MONITOREO"]);
                            if (resultado["COD_ALERTA"] != DBNull.Value) entidad.cod_alerta = Convert.ToInt32(resultado["COD_ALERTA"]);
                            if (resultado["NOM_ALERTA"] != DBNull.Value) entidad.nom_alerta = Convert.ToString(resultado["NOM_ALERTA"]);
                            if (resultado["NOM_PERIODICIDAD"] != DBNull.Value) entidad.nom_periodicidad = Convert.ToString(resultado["NOM_PERIODICIDAD"]);
                            if (resultado["NOM_AREA"] != DBNull.Value) entidad.nom_area = Convert.ToString(resultado["NOM_AREA"]);
                            if (resultado["NOM_CARGO"] != DBNull.Value) entidad.nom_cargo = Convert.ToString(resultado["NOM_CARGO"]);
                            lstTiposMonitoreo.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTiposMonitoreo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SeguimientoData", "ListarTiposMonitoreo", ex);
                        return null;
                    }
                }
            }
        }
    }
}
