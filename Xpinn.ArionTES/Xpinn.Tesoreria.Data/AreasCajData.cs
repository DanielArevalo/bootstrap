using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla AreasCaj
    /// </summary>
    public class AreasCajData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla AreasCaj
        /// </summary>
        public AreasCajData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla AreasCaj de la base de datos
        /// </summary>
        /// <param name="pAreasCaj">Entidad AreasCaj</param>
        /// <returns>Entidad AreasCaj creada</returns>
        public AreasCaj CrearAreasCaj(AreasCaj pAreas_Caj, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidarea = cmdTransaccionFactory.CreateParameter();
                        pidarea.ParameterName = "p_idarea";
                        pidarea.Value = pAreas_Caj.idarea;
                        pidarea.Direction = ParameterDirection.Output;
                        pidarea.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidarea);

                        DbParameter pfecha_constitucion = cmdTransaccionFactory.CreateParameter();
                        pfecha_constitucion.ParameterName = "p_fecha";                                         
                        pfecha_constitucion.Value = pAreas_Caj.fecha_constitucion;
                        pfecha_constitucion.Direction = ParameterDirection.Input;
                        pfecha_constitucion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_constitucion);

                        DbParameter pbase_valor = cmdTransaccionFactory.CreateParameter();
                        pbase_valor.ParameterName = "p_base";
                        pbase_valor.Value = pAreas_Caj.base_valor;
                        pbase_valor.Direction = ParameterDirection.Input;
                        pbase_valor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pbase_valor);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_usuario";
                        pcod_usuario.Value = pAreas_Caj.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pAreas_Caj.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pminimo = cmdTransaccionFactory.CreateParameter();
                        pminimo.ParameterName = "p_minimo";
                        pminimo.Value = pAreas_Caj.valor_minimo;
                        pminimo.Direction = ParameterDirection.Input;
                        pminimo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pminimo);

                        DbParameter psaldo = cmdTransaccionFactory.CreateParameter();
                        psaldo.ParameterName = "p_saldo";
                        psaldo.Value = pAreas_Caj.saldo_caja;
                        psaldo.Direction = ParameterDirection.Input;
                        psaldo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(psaldo);

                        DbParameter pcentro_costo = cmdTransaccionFactory.CreateParameter();
                        pcentro_costo.ParameterName = "p_centro_costo";
                        pcentro_costo.Value = pAreas_Caj.centro_costo;
                        pcentro_costo.Direction = ParameterDirection.Input;
                        pcentro_costo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcentro_costo);

                        DbParameter PIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        PIDENTIFICACION.ParameterName = "P_IDENTIFICACION";
                        PIDENTIFICACION.Value = pAreas_Caj.identificacion;
                        PIDENTIFICACION.Direction = ParameterDirection.Input;
                        PIDENTIFICACION.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PIDENTIFICACION);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_AREASCAJ_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pAreas_Caj.idarea = Convert.ToInt32(pidarea.Value);

                        return pAreas_Caj;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AreasCajData", "CrearAreasCaj", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla AreasCaj de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad AreasCaj modificada</returns>
        public AreasCaj ModificarAreasCaj(AreasCaj pAreas_Caj, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidarea = cmdTransaccionFactory.CreateParameter();
                        pidarea.ParameterName = "p_idarea";
                        pidarea.Value = pAreas_Caj.idarea;
                        pidarea.Direction = ParameterDirection.Input;
                        pidarea.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidarea);

                        DbParameter pfecha_constitucion = cmdTransaccionFactory.CreateParameter();
                        pfecha_constitucion.ParameterName = "p_fecha";
                        pfecha_constitucion.Value = pAreas_Caj.fecha_constitucion;
                        pfecha_constitucion.Direction = ParameterDirection.Input;
                        pfecha_constitucion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_constitucion);

                        DbParameter pbase_valor = cmdTransaccionFactory.CreateParameter();
                        pbase_valor.ParameterName = "p_base";
                        pbase_valor.Value = pAreas_Caj.base_valor;
                        pbase_valor.Direction = ParameterDirection.Input;
                        pbase_valor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pbase_valor);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_usuario";
                        pcod_usuario.Value = pAreas_Caj.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pAreas_Caj.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pminimo = cmdTransaccionFactory.CreateParameter();
                        pminimo.ParameterName = "p_minimo";
                        pminimo.Value = pAreas_Caj.valor_minimo;
                        pminimo.Direction = ParameterDirection.Input;
                        pminimo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pminimo);

                        DbParameter psaldo = cmdTransaccionFactory.CreateParameter();
                        psaldo.ParameterName = "p_saldo";
                        psaldo.Value = pAreas_Caj.saldo_caja;
                        psaldo.Direction = ParameterDirection.Input;
                        psaldo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(psaldo);

                        DbParameter pcentro_costo = cmdTransaccionFactory.CreateParameter();
                        pcentro_costo.ParameterName = "p_centro_costo";
                        pcentro_costo.Value = pAreas_Caj.centro_costo;
                        pcentro_costo.Direction = ParameterDirection.Input;
                        pcentro_costo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcentro_costo);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        pidentificacion.Value = pAreas_Caj.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_AREASCAJ_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pAreas_Caj;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AreasCajData", "ModificarAreasCaj", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla AreasCaj de la base de datos
        /// </summary>
        /// <param name="pId">identificador de AreasCaj</param>
        public void EliminarAreasCaj(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        AreasCaj pAreasCaj = new AreasCaj();
                        pAreasCaj = ConsultarAreasCaj(pId, vUsuario);

                        DbParameter pidarea = cmdTransaccionFactory.CreateParameter();
                        pidarea.ParameterName = "p_idarea";
                        pidarea.Value = pAreasCaj.idarea;
                        pidarea.Direction = ParameterDirection.Input;
                        pidarea.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidarea);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_AREASCAJ_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AreasCajData", "EliminarAreasCaj", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla AreasCaj de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla AreasCajS</param>
        /// <returns>Entidad AreasCaj consultado</returns>
        public AreasCaj ConsultarAreasCaj(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            AreasCaj entidad = new AreasCaj();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select a.IDAREA, a.FECHA_CONSTITUCION, a.BASE_VALOR, u.NOMBRE as NOMUSUARIO, a.COD_USUARIO, a.NOMBRE, a.VALOR_MINIMO, a.CENTRO_COSTO, a.SALDO_CAJA,a.identificacion
                                from AREAS_CAJ a join USUARIOS u on a.COD_USUARIO = u.CODUSUARIO ";
                        if (pId != 0)
                            sql += " Where a.IDAREA = " + pId;
                        else
                            sql+= " Where a.COD_USUARIO = " + vUsuario.codusuario;

                        sql += " ORDER BY a.IDAREA"; 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDAREA"] != DBNull.Value) entidad.idarea = Convert.ToInt32(resultado["IDAREA"]);
                            if (resultado["FECHA_CONSTITUCION"] != DBNull.Value) entidad.fecha_constitucion = Convert.ToDateTime(resultado["FECHA_CONSTITUCION"]);
                            if (resultado["BASE_VALOR"] != DBNull.Value) entidad.base_valor = Convert.ToDecimal(resultado["BASE_VALOR"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt64(resultado["COD_USUARIO"]);
                            if (resultado["NOMUSUARIO"] != DBNull.Value) entidad.nom_usuario = (resultado["NOMUSUARIO"].ToString());
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = (resultado["NOMBRE"].ToString());
                            if (resultado["VALOR_MINIMO"] != DBNull.Value) entidad.valor_minimo = Convert.ToInt64(resultado["VALOR_MINIMO"]);
                            if (resultado["SALDO_CAJA"] != DBNull.Value) entidad.saldo_caja = Convert.ToInt64(resultado["SALDO_CAJA"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
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
                        BOExcepcion.Throw("AreasCajData", "ConsultarAreasCaj", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla AreasCaj dados unos filtros
        /// </summary>
        /// <param name="pAreasCajS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de AreasCaj obtenidos</returns>
        public List<AreasCaj> ListarAreasCaj(AreasCaj pAreasCaj, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<AreasCaj> lstAreasCaj = new List<AreasCaj>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select a.IDAREA, a.FECHA_CONSTITUCION, a.BASE_VALOR, u.NOMBRE as NOMUSUARIO, a.COD_USUARIO, a.NOMBRE, a.VALOR_MINIMO, a.SALDO_CAJA, a.CENTRO_COSTO,a.identificacion,vp.cod_persona
                                from AREAS_CAJ a join USUARIOS u on a.COD_USUARIO = u.CODUSUARIO left join v_persona vp on vp.identificacion=a.identificacion " + ObtenerFiltro(pAreasCaj) + " ORDER BY IDAREA ";

                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            AreasCaj entidad = new AreasCaj();
                            if (resultado["IDAREA"] != DBNull.Value) entidad.idarea = Convert.ToInt32(resultado["IDAREA"]);
                            if (resultado["FECHA_CONSTITUCION"] != DBNull.Value) entidad.fecha_constitucion = Convert.ToDateTime(resultado["FECHA_CONSTITUCION"]);
                            if (resultado["BASE_VALOR"] != DBNull.Value) entidad.base_valor = Convert.ToDecimal(resultado["BASE_VALOR"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt64(resultado["COD_USUARIO"]);
                            if(resultado["NOMUSUARIO"] != DBNull.Value) entidad.nom_usuario = (resultado ["NOMUSUARIO"].ToString());
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = (resultado["NOMBRE"].ToString());
                            if (resultado["VALOR_MINIMO"] != DBNull.Value) entidad.valor_minimo = Convert.ToInt64(resultado["VALOR_MINIMO"]);
                            if (resultado["SALDO_CAJA"] != DBNull.Value) entidad.saldo_caja = Convert.ToInt64(resultado["SALDO_CAJA"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);

                            lstAreasCaj.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAreasCaj;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AreasCajData", "ListarAreasCaj", ex);
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
                        string sql = "SELECT MAX(idarea) + 1 FROM Areas_Caj ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());

                        return resultado;
                    }
                    catch 
                    {
                         return 0;
                    }
                }
            }
        }
        //Crear arqueo caja menor
        public ArqueoCaj CrearArqueoCajaMenor(ArqueoCaj pArqueo_Caja, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pid_arqueo = cmdTransaccionFactory.CreateParameter();
                        pid_arqueo.ParameterName = "p_idarqueo";
                        pid_arqueo.Value = pArqueo_Caja.id_arqueo;
                        pid_arqueo.Direction = ParameterDirection.Output;
                        pid_arqueo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pid_arqueo);

                        DbParameter pfecha_arqueo = cmdTransaccionFactory.CreateParameter();
                        pfecha_arqueo.ParameterName = "p_fecha";
                        pfecha_arqueo.Value = pArqueo_Caja.fecha_arqueo;
                        pfecha_arqueo.Direction = ParameterDirection.Input;
                        pfecha_arqueo.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_arqueo);

                        DbParameter ptotal_arqueo = cmdTransaccionFactory.CreateParameter();
                        ptotal_arqueo.ParameterName = "p_total";
                        ptotal_arqueo.Value = pArqueo_Caja.total_arqueo;
                        ptotal_arqueo.Direction = ParameterDirection.Input;
                        ptotal_arqueo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptotal_arqueo);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_usuario";
                        pcod_usuario.Value = vUsuario.codusuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter pidarea = cmdTransaccionFactory.CreateParameter();
                        pidarea.ParameterName = "p_area";
                        pidarea.Value = pArqueo_Caja.idarea;
                        pidarea.Direction = ParameterDirection.Input;
                        pidarea.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidarea);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_ARQ_CAJMEN_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pArqueo_Caja.id_arqueo = Convert.ToInt64(pid_arqueo.Value);

                        return pArqueo_Caja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AreasCajData", "CrearArqueoCajaMenor", ex);
                        return null;
                    }
                }
            }
        }
        //Crear detalle arqueo caja menor
        public ArqueoDetalle CrearArqueoCajaDetalle(ArqueoDetalle pArqueo_Det, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pid_det_arqueo = cmdTransaccionFactory.CreateParameter();
                        pid_det_arqueo.ParameterName = "p_id_det_arqueo";
                        pid_det_arqueo.Value = pArqueo_Det.id_det_arqueo;
                        pid_det_arqueo.Direction = ParameterDirection.Output;
                        pid_det_arqueo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pid_det_arqueo);

                        DbParameter pid_arqueo = cmdTransaccionFactory.CreateParameter();
                        pid_arqueo.ParameterName = "p_id_arqueo";
                        pid_arqueo.Value = pArqueo_Det.id_arqueo;
                        pid_arqueo.Direction = ParameterDirection.Input;
                        pid_arqueo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pid_arqueo);

                        DbParameter ptipo_efectivo = cmdTransaccionFactory.CreateParameter();
                        ptipo_efectivo.ParameterName = "p_tipo_efectivo";
                        if (pArqueo_Det.tipo_efectivo == "Billete")
                        {
                            ptipo_efectivo.Value = "B";
                        }
                        else if(pArqueo_Det.tipo_efectivo == "Moneda")
                        {
                            ptipo_efectivo.Value = "M";
                        }                        
                        ptipo_efectivo.Direction = ParameterDirection.Input;
                        ptipo_efectivo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_efectivo);

                        DbParameter pdenominacion = cmdTransaccionFactory.CreateParameter();
                        pdenominacion.ParameterName = "p_denominacion";
                        pdenominacion.Value = pArqueo_Det.denominacion;
                        pdenominacion.Direction = ParameterDirection.Input;
                        pdenominacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pdenominacion);

                        DbParameter pcantidad = cmdTransaccionFactory.CreateParameter();
                        pcantidad.ParameterName = "p_cantidad";
                        pcantidad.Value = pArqueo_Det.cantidad;
                        pcantidad.Direction = ParameterDirection.Input;
                        pcantidad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcantidad);

                        DbParameter ptotal = cmdTransaccionFactory.CreateParameter();
                        ptotal.ParameterName = "p_total";
                        ptotal.Value = pArqueo_Det.total;
                        ptotal.Direction = ParameterDirection.Input;
                        ptotal.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptotal);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_ARQCAJMDET_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        pArqueo_Det.id_det_arqueo = Convert.ToInt64(pid_det_arqueo.Value);

                        return pArqueo_Det;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AreasCajData", "CrearArqueoCajaDetalle", ex);
                        return null;
                    }
                }
            }
        }

        //Consultar caja menor según el responsable
        /// <summary>
        /// Obtiene un registro en la tabla AreasCaj de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla AreasCajS</param>
        /// <returns>Entidad AreasCaj consultado</returns>
        public AreasCaj ConsultarCajaMenor(int codusuario, Usuario vUsuario)
        {
            DbDataReader resultado;
            AreasCaj areasCaj = new AreasCaj();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    
                    try
                    {
                        string sql = @"select a.IDAREA, a.FECHA_CONSTITUCION, a.BASE_VALOR, u.NOMBRE as NOMUSUARIO, a.COD_USUARIO, a.NOMBRE, a.VALOR_MINIMO, a.SALDO_CAJA, a.CENTRO_COSTO
                                from AREAS_CAJ a join USUARIOS u on a.COD_USUARIO = u.CODUSUARIO where a.COD_USUARIO = " + codusuario + " ORDER BY IDAREA";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        AreasCaj entidad = new AreasCaj();
                        if(resultado.Read())
                        {
                            if (resultado["IDAREA"] != DBNull.Value) entidad.idarea = Convert.ToInt32(resultado["IDAREA"]);
                            if (resultado["FECHA_CONSTITUCION"] != DBNull.Value) entidad.fecha_constitucion = Convert.ToDateTime(resultado["FECHA_CONSTITUCION"]);
                            if (resultado["BASE_VALOR"] != DBNull.Value) entidad.base_valor = Convert.ToDecimal(resultado["BASE_VALOR"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt64(resultado["COD_USUARIO"]);
                            if (resultado["NOMUSUARIO"] != DBNull.Value) entidad.nom_usuario = (resultado["NOMUSUARIO"].ToString());
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = (resultado["NOMBRE"].ToString());
                            if (resultado["VALOR_MINIMO"] != DBNull.Value) entidad.valor_minimo = Convert.ToInt64(resultado["VALOR_MINIMO"]);
                            if (resultado["SALDO_CAJA"] != DBNull.Value) entidad.saldo_caja = Convert.ToInt64(resultado["SALDO_CAJA"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AreasCajData", "ConsultarAreasCaj", ex);
                        return null;
                    }
                }
            }
        }
        public void ModificarArqueoCaja(Int64? id_arqueo,Int64 total_arqueo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidarqueo = cmdTransaccionFactory.CreateParameter();
                        pidarqueo.ParameterName = "p_idarqueo";
                        pidarqueo.Value = id_arqueo;
                        pidarqueo.Direction = ParameterDirection.Input;
                        pidarqueo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidarqueo);

                        DbParameter ptotal = cmdTransaccionFactory.CreateParameter();
                        ptotal.ParameterName = "p_total";
                        ptotal.Value = total_arqueo;
                        ptotal.Direction = ParameterDirection.Input;
                        ptotal.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptotal);                       

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_ARQ_CAJMEN_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AreasCajData", "ModificarArqueoCaja", ex);
                    }
                }
            }
        }
    }
}