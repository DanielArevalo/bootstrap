using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.ActivosFijos.Entities;

namespace Xpinn.ActivosFijos.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla TipoActivoS
    /// </summary>
    public class TipoActivoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

       

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla TipoActivoS
        /// </summary>
        public TipoActivoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla TipoActivoS de la base de datos
        /// </summary>
        /// <param name="pTipoActivo">Entidad TipoActivo</param>
        /// <returns>Entidad TipoActivo creada</returns>
        public TipoActivo CrearTipoActivo(TipoActivo pTipoActivo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.Value = pTipoActivo.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pTipoActivo.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pcod_cuenta_activo = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_activo.ParameterName = "p_cod_cuenta_activo";
                        pcod_cuenta_activo.Value = pTipoActivo.cod_cuenta_activo;
                        pcod_cuenta_activo.Direction = ParameterDirection.Input;
                        pcod_cuenta_activo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_activo);

                        DbParameter pcod_cuenta_depreciacion = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_depreciacion.ParameterName = "p_cod_cuenta_depreciacion";
                        pcod_cuenta_depreciacion.Value = pTipoActivo.cod_cuenta_depreciacion;
                        pcod_cuenta_depreciacion.Direction = ParameterDirection.Input;
                        pcod_cuenta_depreciacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_depreciacion);

                        DbParameter pcod_cuenta_gasto_venta_baja = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_gasto_venta_baja.ParameterName = "p_cod_cuenta_gasto_venta_baja";
                        pcod_cuenta_gasto_venta_baja.Value = pTipoActivo.cod_cuenta_gasto_venta_baja;
                        pcod_cuenta_gasto_venta_baja.Direction = ParameterDirection.Input;
                        pcod_cuenta_gasto_venta_baja.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_gasto_venta_baja);

                        DbParameter pcod_cuenta_ingreso_venta_baja = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_ingreso_venta_baja.ParameterName = "p_cod_cuenta_ingreso";
                        pcod_cuenta_ingreso_venta_baja.Value = pTipoActivo.cod_cuenta_ingreso_venta_baja;
                        pcod_cuenta_ingreso_venta_baja.Direction = ParameterDirection.Input;
                        pcod_cuenta_ingreso_venta_baja.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_ingreso_venta_baja);

                        DbParameter pcod_cuenta_depreciacion_gasto = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_depreciacion_gasto.ParameterName = "p_cod_cuenta_depre_gasto";
                        pcod_cuenta_depreciacion_gasto.Value = pTipoActivo.cod_cuenta_depreciacion_gasto;
                        pcod_cuenta_depreciacion_gasto.Direction = ParameterDirection.Input;
                        pcod_cuenta_depreciacion_gasto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_depreciacion_gasto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_TIPO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTipoActivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoActivoData", "CrearTipoActivo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla TipoActivoS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad TipoActivo modificada</returns>
        public TipoActivo ModificarTipoActivo(TipoActivo pTipoActivo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.Value = pTipoActivo.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pTipoActivo.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pcod_cuenta_activo = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_activo.ParameterName = "p_cod_cuenta_activo";
                        pcod_cuenta_activo.Value = pTipoActivo.cod_cuenta_activo;
                        pcod_cuenta_activo.Direction = ParameterDirection.Input;
                        pcod_cuenta_activo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_activo);

                        DbParameter pcod_cuenta_depreciacion = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_depreciacion.ParameterName = "p_cod_cuenta_depreciacion";
                        pcod_cuenta_depreciacion.Value = pTipoActivo.cod_cuenta_depreciacion;
                        pcod_cuenta_depreciacion.Direction = ParameterDirection.Input;
                        pcod_cuenta_depreciacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_depreciacion);

                        DbParameter pcod_cuenta_gasto_venta_baja = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_gasto_venta_baja.ParameterName = "p_cod_cuenta_gasto_venta_baja";
                        pcod_cuenta_gasto_venta_baja.Value = pTipoActivo.cod_cuenta_gasto_venta_baja;
                        pcod_cuenta_gasto_venta_baja.Direction = ParameterDirection.Input;
                        pcod_cuenta_gasto_venta_baja.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_gasto_venta_baja);

                        DbParameter pcod_cuenta_ingreso_venta_baja = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_ingreso_venta_baja.ParameterName = "p_cod_cuenta_ingreso";
                        pcod_cuenta_ingreso_venta_baja.Value = pTipoActivo.cod_cuenta_ingreso_venta_baja;
                        pcod_cuenta_ingreso_venta_baja.Direction = ParameterDirection.Input;
                        pcod_cuenta_ingreso_venta_baja.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_ingreso_venta_baja);

                        DbParameter pcod_cuenta_depreciacion_gasto = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_depreciacion_gasto.ParameterName = "p_cod_cuenta_depre_gasto";
                        pcod_cuenta_depreciacion_gasto.Value = pTipoActivo.cod_cuenta_depreciacion_gasto;
                        pcod_cuenta_depreciacion_gasto.Direction = ParameterDirection.Input;
                        pcod_cuenta_depreciacion_gasto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_depreciacion_gasto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_TIPO_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTipoActivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoActivoData", "ModificarTipoActivo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla TipoActivoS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de TipoActivoS</param>
        public void EliminarTipoActivo(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        TipoActivo pTipoActivo = new TipoActivo();
                        pTipoActivo = ConsultarTipoActivo(pId, vUsuario);

                        DbParameter pTipo = cmdTransaccionFactory.CreateParameter();
                        pTipo.ParameterName = "p_Tipo";
                        pTipo.Value = pTipoActivo.tipo;
                        pTipo.Direction = ParameterDirection.Input;
                        pTipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pTipo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_TIPO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoData", "EliminarConcepto", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla TipoActivoS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla TipoActivoS</param>
        /// <returns>Entidad TipoActivo consultado</returns>
        public TipoActivo ConsultarTipoActivo(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            TipoActivo entidad = new TipoActivo();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Tipo_Activo WHERE TIPO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_CUENTA_ACTIVO"] != DBNull.Value) entidad.cod_cuenta_activo = Convert.ToString(resultado["COD_CUENTA_ACTIVO"]);
                            if (resultado["COD_CUENTA_DEPRECIACION"] != DBNull.Value) entidad.cod_cuenta_depreciacion = Convert.ToString(resultado["COD_CUENTA_DEPRECIACION"]);
                            if (resultado["COD_CUENTA_GASTO_VENTA_BAJA"] != DBNull.Value) entidad.cod_cuenta_gasto_venta_baja = Convert.ToString(resultado["COD_CUENTA_GASTO_VENTA_BAJA"]);
                            if (resultado["COD_CUENTA_INGRESO_VENTA_BAJA"] != DBNull.Value) entidad.cod_cuenta_ingreso_venta_baja = Convert.ToString(resultado["COD_CUENTA_INGRESO_VENTA_BAJA"]);
                            if (resultado["COD_CUENTA_DEPRECIACION_GASTO"] != DBNull.Value) entidad.cod_cuenta_depreciacion_gasto = Convert.ToString(resultado["COD_CUENTA_DEPRECIACION_GASTO"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoActivoData", "ConsultarTipoActivo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TipoActivo dados unos filtros
        /// </summary>
        /// <param name="pTipoActivo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoActivos obtenidos</returns>
        public List<TipoActivo> ListarTipoActivo(TipoActivo pTipoActivo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TipoActivo> lstTipoActivo = new List<TipoActivo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Tipo_Activo " + ObtenerFiltro(pTipoActivo) + " ORDER BY TIPO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TipoActivo entidad = new TipoActivo();
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_CUENTA_ACTIVO"] != DBNull.Value) entidad.cod_cuenta_activo = Convert.ToString(resultado["COD_CUENTA_ACTIVO"]);
                            if (resultado["COD_CUENTA_DEPRECIACION"] != DBNull.Value) entidad.cod_cuenta_depreciacion = Convert.ToString(resultado["COD_CUENTA_DEPRECIACION"]);
                            if (resultado["COD_CUENTA_GASTO_VENTA_BAJA"] != DBNull.Value) entidad.cod_cuenta_gasto_venta_baja = Convert.ToString(resultado["COD_CUENTA_GASTO_VENTA_BAJA"]);
                            if (resultado["COD_CUENTA_INGRESO_VENTA_BAJA"] != DBNull.Value) entidad.cod_cuenta_ingreso_venta_baja = Convert.ToString(resultado["COD_CUENTA_INGRESO_VENTA_BAJA"]);
                            if (resultado["COD_CUENTA_DEPRECIACION_GASTO"] != DBNull.Value) entidad.cod_cuenta_depreciacion_gasto = Convert.ToString(resultado["COD_CUENTA_DEPRECIACION_GASTO"]);
                            lstTipoActivo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipoActivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoActivoData", "ListarTipoActivo", ex);
                        return null;
                    }
                }
            }
        }


        

        /// <summary>
        /// Obtiene el siguiente codigo disponible de la tabla
        /// </summary>
        /// <returns>codigo disponible</returns>
        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(Tipo) + 1 FROM Tipo_Activo ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoActivoData", "ObtenerSiguienteCodigo", ex);
                        return -1;
                    }
                }
            }
        }

        //AGREGADO

        public List<TipoActivo> ListarTipoActivo_NIIF(TipoActivo pTipoActivo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TipoActivo> lstTipoActivo = new List<TipoActivo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM tipo_activo_nif " + ObtenerFiltro(pTipoActivo) + " ORDER BY TIPO_ACTIVO_NIF ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TipoActivo entidad = new TipoActivo();
                            if (resultado["TIPO_ACTIVO_NIF"] != DBNull.Value) entidad.tipo_activo_nif = Convert.ToInt32(resultado["TIPO_ACTIVO_NIF"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstTipoActivo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipoActivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoActivoData", "ListarTipoActivo_NIIF", ex);
                        return null;
                    }
                }
            }
        }


        public List<TipoActivo> ListarUniGeneradora_NIIF(TipoActivo pTipoActivo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TipoActivo> lstUniGeneradora = new List<TipoActivo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM unigeneradora_activo_nif " + ObtenerFiltro(pTipoActivo) + " ORDER BY CODUNIGENERADORA_NIF ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TipoActivo entidad = new TipoActivo();
                            if (resultado["CODUNIGENERADORA_NIF"] != DBNull.Value) entidad.codunigeneradora_nif = Convert.ToInt32(resultado["CODUNIGENERADORA_NIF"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcionUNIGE = Convert.ToString(resultado["DESCRIPCION"]);

                            lstUniGeneradora.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstUniGeneradora;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoActivoData", "ListarUniGeneradora_NIIF", ex);
                        return null;
                    }
                }
            }
        }

    }
}