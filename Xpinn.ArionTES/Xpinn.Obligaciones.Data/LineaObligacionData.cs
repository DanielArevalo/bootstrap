using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Obligaciones.Entities;

namespace Xpinn.Obligaciones.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla LineaObligacion
    /// </summary>
    public class LineaObligacionData  : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla LineaObligacion
        /// </summary>
        public LineaObligacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        /// <summary>
        /// Crea una entidad Linea de Obligacion en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Linea de Obligacion</param>
        /// <returns>Entidad creada</returns>
        public LineaObligacion CrearLineaOb(LineaObligacion pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_linea = cmdTransaccionFactory.CreateParameter();
                        pcod_linea.ParameterName = "pcodigolineaobligacion";
                        pcod_linea.Value = pEntidad.CODLINEAOBLIGACION;
                        pcod_linea.Direction = ParameterDirection.InputOutput;

                        DbParameter pnom_linea = cmdTransaccionFactory.CreateParameter();
                        pnom_linea.ParameterName = "pnombrelinea";
                        pnom_linea.Value = pEntidad.NOMBRELINEA;
                        pnom_linea.Direction = ParameterDirection.Input;

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "ptipomoneda";
                        pcod_moneda.Value = pEntidad.TIPOMONEDA;
                        pcod_moneda.Direction = ParameterDirection.Input;

                        DbParameter pcod_tipoliq = cmdTransaccionFactory.CreateParameter();
                        pcod_tipoliq.ParameterName = "ptipoliquidacion";
                        pcod_tipoliq.Value = pEntidad.TIPOLIQUIDACION;
                        pcod_tipoliq.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcod_linea);
                        cmdTransaccionFactory.Parameters.Add(pnom_linea);
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);
                        cmdTransaccionFactory.Parameters.Add(pcod_tipoliq);
                        
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_LINEA_C";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pEntidad, "OBLINEAOBLIGACION", pUsuario, Accion.Crear.ToString(), TipoAuditoria.CajaFinanciera, "Registrar OBLINEAOBLIGACION");
                            //DAauditoria.InsertarLog(pEntidad, pUsuario, Convert.ToInt64(pEntidad.CODLINEAOBLIGACION), "OBLINEAOBLIGACION", Accion.Crear.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaObligacionData", "CrearLineaOb", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica una entidad LineaObligacion en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad LineaObligacion</param>
        /// <returns>Entidad modificada</returns>
        public LineaObligacion ModificarLineaOb(LineaObligacion pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_linea = cmdTransaccionFactory.CreateParameter();
                        pcod_linea.ParameterName = "pcodigolineaobligacion";
                        pcod_linea.Value = pEntidad.CODLINEAOBLIGACION;
                        pcod_linea.Direction = ParameterDirection.InputOutput;

                        DbParameter pnom_linea = cmdTransaccionFactory.CreateParameter();
                        pnom_linea.ParameterName = "pnombrelinea";
                        pnom_linea.Value = pEntidad.NOMBRELINEA;
                        pnom_linea.Direction = ParameterDirection.Input;

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "ptipomoneda";
                        pcod_moneda.Value = pEntidad.TIPOMONEDA;
                        pcod_moneda.Direction = ParameterDirection.Input;

                        DbParameter pcod_tipoliq = cmdTransaccionFactory.CreateParameter();
                        pcod_tipoliq.ParameterName = "ptipoliquidacion";
                        pcod_tipoliq.Value = pEntidad.TIPOLIQUIDACION;
                        pcod_tipoliq.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcod_linea);
                        cmdTransaccionFactory.Parameters.Add(pnom_linea);
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);
                        cmdTransaccionFactory.Parameters.Add(pcod_tipoliq);

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_LINEA_U";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pEntidad, "OBLINEAOBLIGACION", pUsuario, Accion.Modificar.ToString(), TipoAuditoria.CajaFinanciera, "Modificar OBLINEAOBLIGACION");
                            //DAauditoria.InsertarLog(pEntidad, pUsuario, Convert.ToInt64(pEntidad.CODLINEAOBLIGACION), "OBLINEAOBLIGACION", Accion.Modificar.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaObligacionData", "ModificarLineaOb", ex);
                        return null;
                    }

                }

            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TIPOLIQUIDACION dados unos filtros
        /// </summary>
        /// <param name="pTIPOLIQUIDACION">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Tipo Liquidacion obtenidos</returns>
        public List<LineaObligacion> ListarLineaObligacion(LineaObligacion pSolicitud, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LineaObligacion> lstTipoLiq = new List<LineaObligacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT CODLINEAOBLIGACION,NOMBRELINEA,(SELECT t.descripcion FROM tipomoneda t where t.cod_moneda=X.TIPOMONEDA) NOMTIPOMONEDA,( SELECT O.NOMBRE FROM OBTIPOLIQUIDACION O WHERE O.CODTIPOLIQUIDACION=X.TIPOLIQUIDACION ) NOMTIPOLIQUIDACION FROM  OBLINEAOBLIGACION X order by CODLINEAOBLIGACION ";

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LineaObligacion entidad = new LineaObligacion();

                            if (resultado["CODLINEAOBLIGACION"] != DBNull.Value) entidad.CODLINEAOBLIGACION = Convert.ToInt64(resultado["CODLINEAOBLIGACION"]);
                            if (resultado["NOMBRELINEA"] != DBNull.Value) entidad.NOMBRELINEA = Convert.ToString(resultado["NOMBRELINEA"]);
                            if (resultado["NOMTIPOMONEDA"] != DBNull.Value) entidad.NOMTIPOMONEDA = Convert.ToString(resultado["NOMTIPOMONEDA"]);
                            if (resultado["NOMTIPOLIQUIDACION"] != DBNull.Value) entidad.NOMTIPOLIQUIDACION = Convert.ToString(resultado["NOMTIPOLIQUIDACION"]);

                            lstTipoLiq.Add(entidad);
                        }

                        return lstTipoLiq;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaObligacionData", "ListarLineaObligacion", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Elimina una Linea de Obligacion en la base de datos
        /// </summary>
        /// <param name="pId">identificador de la Linea de Obligacion</param>
        public void EliminarLineaOb(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        LineaObligacion pEntidad = new LineaObligacion();


                        DbParameter pcod_linea = cmdTransaccionFactory.CreateParameter();
                        pcod_linea.ParameterName = "pcodlineaobligacion";
                        pcod_linea.Value = pId;
                        pcod_linea.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcod_linea);

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_LINEA_D";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pEntidad, "OBLINEAOBLIGACION", pUsuario, Accion.Eliminar.ToString(), TipoAuditoria.CajaFinanciera, "Eliminar OBLINEAOBLIGACION");
                            //DAauditoria.InsertarLog(pEntidad, pUsuario, Convert.ToInt64(pId), "OBLINEAOBLIGACION", Accion.Eliminar.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaObligacionData", "EliminarLineaOb", ex);
                    }

                }
            }
        }


        /// <summary>
        /// Obtiene un registro de la tabla LineaObligacion de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>LineaObligacion consultada</returns>
        public LineaObligacion ConsultarLineaOb(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            LineaObligacion entidad = new LineaObligacion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM OBLINEAOBLIGACION where codlineaobligacion=" + pId.ToString();

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["codlineaobligacion"] != DBNull.Value) entidad.CODLINEAOBLIGACION = Convert.ToInt64(resultado["codlineaobligacion"]);
                            if (resultado["nombrelinea"] != DBNull.Value) entidad.NOMBRELINEA = Convert.ToString(resultado["nombrelinea"]);
                            if (resultado["tipomoneda"] != DBNull.Value) entidad.TIPOMONEDA = Convert.ToInt64(resultado["tipomoneda"]);
                            if (resultado["tipoliquidacion"] != DBNull.Value) entidad.TIPOLIQUIDACION = Convert.ToInt64(resultado["tipoliquidacion"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaObligacionData", "ConsultarLineaOb", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene el conteo de Tipos de liquidacion asociados a la linea de obligacion 
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>LineaObligacion consultada</returns>
        public LineaObligacion ConsultarObligacionXLineaObligacion(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            LineaObligacion entidad = new LineaObligacion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT count(*) conteo FROM OBCREDITO where codlineaobligacion=" + pId.ToString();

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["conteo"] != DBNull.Value) entidad.conteo = Convert.ToInt64(resultado["conteo"]);
                        }
                        else
                        {
                            entidad.conteo = 0;
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaObligacionData", "ConsultarObligacionXLineaObligacion", ex);
                        return null;
                    }
                }
            }
        }   

    }
}
