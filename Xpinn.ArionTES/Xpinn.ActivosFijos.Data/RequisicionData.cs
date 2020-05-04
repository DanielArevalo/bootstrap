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
    /// Objeto de acceso a datos para la tabla ActivoFijos
    /// </summary>
    public class RequisicionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ACTIVOS_FIJOS
        /// </summary>
        public RequisicionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Requisicion CrearRequisicion(Requisicion pRequisicion,List<Detalle_Requisicion> vRequisicionDet, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        DbParameter p_idrequisicion = cmdTransaccionFactory.CreateParameter();
                        p_idrequisicion.ParameterName = "p_idrequisicion";
                        p_idrequisicion.Value = pRequisicion.idrequisicion;
                        p_idrequisicion.Direction = ParameterDirection.Input;
                        p_idrequisicion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_idrequisicion);

                        DbParameter p_fecha_requsicion = cmdTransaccionFactory.CreateParameter();
                        p_fecha_requsicion.ParameterName = "p_fecha_requsicion";
                        p_fecha_requsicion.Value = pRequisicion.fecha_requsicion;
                        p_fecha_requsicion.Direction = ParameterDirection.Input;
                        p_fecha_requsicion.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_requsicion);

                        DbParameter p_fecha_est_entrega = cmdTransaccionFactory.CreateParameter();
                        p_fecha_est_entrega.ParameterName = "p_fecha_est_entrega";
                        p_fecha_est_entrega.Value = pRequisicion.fecha_est_entrega;
                        p_fecha_est_entrega.Direction = ParameterDirection.Input;
                        p_fecha_est_entrega.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_est_entrega);


                        DbParameter p_idarea = cmdTransaccionFactory.CreateParameter();
                        p_idarea.ParameterName = "p_idarea";
                        p_idarea.Value = pRequisicion.idarea;
                        p_idarea.Direction = ParameterDirection.Input;
                        p_idarea.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_idarea);

                        DbParameter p_cod_solicita = cmdTransaccionFactory.CreateParameter();
                        p_cod_solicita.ParameterName = "p_cod_solicita";
                        p_cod_solicita.Value = pRequisicion.cod_solicita;
                        p_cod_solicita.Direction = ParameterDirection.Input;
                        p_cod_solicita.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_solicita);

                        DbParameter p_destino = cmdTransaccionFactory.CreateParameter();
                        p_destino.ParameterName = "p_destino";
                        p_destino.Value = pRequisicion.destino;
                        p_destino.Direction = ParameterDirection.Input;
                        p_destino.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_destino);

                        DbParameter p_observacion = cmdTransaccionFactory.CreateParameter();
                        p_observacion.ParameterName = "p_observacion";
                        p_observacion.Value = pRequisicion.observacion;
                        p_observacion.Direction = ParameterDirection.Input;
                        p_observacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_observacion);

                        DbParameter p_cod_usuario_crea = cmdTransaccionFactory.CreateParameter();
                        p_cod_usuario_crea.ParameterName = "p_cod_usuario_crea";
                        p_cod_usuario_crea.Value = pRequisicion.cod_usuario_crea;
                        p_cod_usuario_crea.Direction = ParameterDirection.Input;
                        p_cod_usuario_crea.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_usuario_crea);


                        DbParameter p_fecha_crea = cmdTransaccionFactory.CreateParameter();
                        p_fecha_crea.ParameterName = "p_fecha_crea";
                        p_fecha_crea.Value = pRequisicion.fecha_crea;
                        p_fecha_crea.Direction = ParameterDirection.Input;
                        p_fecha_crea.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_crea);


                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = pRequisicion.estado;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_estado);

                        //p_idrequisicion,p_fecha_requsicion,p_fecha_est_entrega,p_idarea ,p_cod_solicita,p_destino ,p_observacion ,p_cod_usuario_crea ,p_fecha_crea,p_estado


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_COM_REQUISICION_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);


                        CrearDetRequisicion(vRequisicionDet, vUsuario);








                        //pActivoFijo.consecutivo = Convert.ToInt64(pconsecutivo.Value);

                        return pRequisicion;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "CrearActivoFijo", ex);
                        return null;
                    }
                }
            }




        }



        public Detalle_Requisicion CrearDetRequisicion( List<Detalle_Requisicion> vRequisicionDet, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        foreach (Detalle_Requisicion item in vRequisicionDet)

                        {



                        DbParameter p_iddetrequisicion = cmdTransaccionFactory.CreateParameter();
                            p_iddetrequisicion.ParameterName = "p_iddetrequisicion";
                            p_iddetrequisicion.Value = item.iddetrequisicion ;
                            p_iddetrequisicion.Direction = ParameterDirection.Input;
                            p_iddetrequisicion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_iddetrequisicion);

                        DbParameter p_idrequisicion = cmdTransaccionFactory.CreateParameter();
                            p_idrequisicion.ParameterName = "p_idrequisicion";
                            p_idrequisicion.Value = item.idrequisicion ;
                            p_idrequisicion.Direction = ParameterDirection.Input;
                            p_idrequisicion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_idrequisicion);

                        DbParameter p_idarticulo = cmdTransaccionFactory.CreateParameter();
                            p_idarticulo.ParameterName = "p_idarticulo";
                            p_idarticulo.Value = item.idarticulo ;
                            p_idarticulo.Direction = ParameterDirection.Input;
                            p_idarticulo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_idarticulo);


                        DbParameter p_cantidad = cmdTransaccionFactory.CreateParameter();
                            p_cantidad.ParameterName = "p_cantidad";
                            p_cantidad.Value = item.cantidad ;
                            p_cantidad.Direction = ParameterDirection.Input;
                            p_cantidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cantidad);

                        DbParameter p_detalle = cmdTransaccionFactory.CreateParameter();
                            p_detalle.ParameterName = "p_detalle";
                            p_detalle.Value = item.detalle ;
                            p_detalle.Direction = ParameterDirection.Input;
                            p_detalle.DbType = DbType.String ;
                        cmdTransaccionFactory.Parameters.Add(p_detalle);
                            
                        //p_iddetrequisicion,p_idrequisicion,p_idarticulo,p_cantidad ,p_detalle

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_COM_DETREQUISICION_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);






                        }




                        //pActivoFijo.consecutivo = Convert.ToInt64(pconsecutivo.Value);

                        return null;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "CrearActivoFijo", ex);
                        return null;
                    }
                }
            }




        }


      
    /// <summary>
    /// Modifica un registro en la tabla ActivoFijo de la base de datos
    /// </summary>
    /// <param name="pEntidad">Entidad $Objeto</param>
    /// <returns>Entidad ActivoFijo modificada</returns>
    public Requisicion ModificarRequisicion(Requisicion pArticulo, List<Detalle_Requisicion> vRequisicionDet, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        //DbParameter pidarticulo = cmdTransaccionFactory.CreateParameter();
                        //pidarticulo.ParameterName = "p_idarticulo";
                        //pidarticulo.Value = pArticulo.idarticulo;
                        //pidarticulo.Direction = ParameterDirection.Input;
                        //pidarticulo.DbType = DbType.Int64;
                        //cmdTransaccionFactory.Parameters.Add(pidarticulo);

                        //DbParameter pserial = cmdTransaccionFactory.CreateParameter();
                        //pserial.ParameterName = "p_serial";
                        //pserial.Value = pArticulo.serial;
                        //pserial.Direction = ParameterDirection.Input;
                        //pserial.DbType = DbType.String;
                        //cmdTransaccionFactory.Parameters.Add(pserial);

                        //DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        //pdescripcion.ParameterName = "p_descripcion";
                        //pdescripcion.Value = pArticulo.descripcion;
                        //pdescripcion.Direction = ParameterDirection.Input;
                        //pdescripcion.DbType = DbType.String;
                        //cmdTransaccionFactory.Parameters.Add(pdescripcion);


                        //DbParameter pidtipo_articulo = cmdTransaccionFactory.CreateParameter();
                        //pidtipo_articulo.ParameterName = "p_idtipo_articulo";
                        //pidtipo_articulo.Value = pArticulo.idtipo_articulo;
                        //pidtipo_articulo.Direction = ParameterDirection.Input;
                        //pidtipo_articulo.DbType = DbType.Int32;
                        //cmdTransaccionFactory.Parameters.Add(pidtipo_articulo);

                        //DbParameter preferencia = cmdTransaccionFactory.CreateParameter();
                        //preferencia.ParameterName = "p_referencia";
                        //preferencia.Value = pArticulo.referencia;
                        //preferencia.Direction = ParameterDirection.Input;
                        //preferencia.DbType = DbType.String;
                        //cmdTransaccionFactory.Parameters.Add(preferencia);

                        //DbParameter pmarca = cmdTransaccionFactory.CreateParameter();
                        //pmarca.ParameterName = "p_marca";
                        //pmarca.Value = pArticulo.marca;
                        //pmarca.Direction = ParameterDirection.Input;
                        //pmarca.DbType = DbType.String;
                        //cmdTransaccionFactory.Parameters.Add(pmarca);





                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_COM_Requisicion_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pArticulo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AreasData", "ModificarAreas", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla ActivoFijoS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de ActivoFijoS</param>
        public void EliminarRequisicion(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Requisicion pRequisicion = new Requisicion();

                        DbParameter pIdRequisicion = cmdTransaccionFactory.CreateParameter();
                        pIdRequisicion.ParameterName = "p_IdRequisicion";
                        pIdRequisicion.Value = Convert.ToInt32(pId);
                        pIdRequisicion.Direction = ParameterDirection.Input;
                        pIdRequisicion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pIdRequisicion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_COM_Requisicion_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
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
        /// Obtiene un registro en la tabla ActivoFijoS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla ActivoFijoS</param>
        /// <returns>Entidad ActivoFijo consultado</returns>
        public Requisicion ConsultarRequisicion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Requisicion entidad = new Requisicion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM cm_requisicion
                                            WHERE idrequisicion = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["idrequisicion"] != DBNull.Value) entidad.idrequisicion   = Convert.ToInt64(resultado["idrequisicion"]);
                            if (resultado["fecha_requsicion"] != DBNull.Value) entidad.fecha_requsicion = Convert.ToDateTime(resultado["fecha_requsicion"].ToString());
                            if (resultado["fecha_est_entrega"] != DBNull.Value) entidad.fecha_est_entrega = Convert.ToDateTime(resultado["fecha_est_entrega"].ToString());
                            if (resultado["idarea"] != DBNull.Value) entidad.idarea = Convert.ToInt32(resultado["idarea"].ToString());
                            if (resultado["cod_solicita"] != DBNull.Value) entidad.cod_solicita = Convert.ToInt32(resultado["cod_solicita"].ToString());
                            if (resultado["destino"] != DBNull.Value) entidad.destino = resultado["destino"].ToString();
                            if (resultado["observacion"] != DBNull.Value) entidad.observacion = resultado["observacion"].ToString();
                            if (resultado["cod_usuario_crea"] != DBNull.Value) entidad.cod_usuario_crea = Convert.ToString(resultado["cod_usuario_crea"].ToString());
                            if (resultado["fecha_crea"] != DBNull.Value) entidad.fecha_crea = Convert.ToDateTime(resultado["fecha_crea"].ToString());
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["estado"].ToString());
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
                        BOExcepcion.Throw("AreasData", "ConsultarAreas", ex);
                        return null;
                    }
                }
            }
        }

       

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla ActivoFijoS dados unos filtros
        /// </summary>
        /// <param name="pActivoFijoS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ActivoFijo obtenidos</returns>
        public List<Requisicion> ListarRequisicion(Requisicion pRequisicion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Requisicion> lstArticulo = new List<Requisicion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT *
                                            FROM cm_requisicion                                             
                                      " + ObtenerFiltro(pRequisicion, "Requisicion.") + " ORDER BY idrequisicion";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Requisicion entidad = new Requisicion();

                            if (resultado["idrequisicion"] != DBNull.Value) entidad.idrequisicion = Convert.ToInt64(resultado["idrequisicion"]);
                            if (resultado["fecha_requsicion"] != DBNull.Value) entidad.fecha_requsicion = Convert.ToDateTime(resultado["fecha_requsicion"].ToString());
                            if (resultado["fecha_est_entrega"] != DBNull.Value) entidad.fecha_est_entrega = Convert.ToDateTime(resultado["fecha_est_entrega"].ToString());
                            if (resultado["idarea"] != DBNull.Value) entidad.idarea = Convert.ToInt32(resultado["idarea"].ToString());
                            if (resultado["cod_solicita"] != DBNull.Value) entidad.cod_solicita = Convert.ToInt32(resultado["cod_solicita"].ToString());
                            if (resultado["destino"] != DBNull.Value) entidad.destino = resultado["destino"].ToString();
                            if (resultado["observacion"] != DBNull.Value) entidad.observacion = resultado["observacion"].ToString();
                            if (resultado["cod_usuario_crea"] != DBNull.Value) entidad.cod_usuario_crea = Convert.ToString(resultado["cod_usuario_crea"].ToString());
                            if (resultado["fecha_crea"] != DBNull.Value) entidad.fecha_crea = Convert.ToDateTime(resultado["fecha_crea"].ToString());
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["estado"].ToString());

                            lstArticulo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstArticulo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "ListarActivoFijo", ex);
                        return null;
                    }
                }
            }
        }

    

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(cod_act) + 1 FROM ACTIVOS_FIJOS ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch 
                    {
                        return 1;
                    }
                }
            }
        }



    }
}
