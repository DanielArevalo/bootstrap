using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Util;

namespace Xpinn.Sincronizacion.Data
{
    public class SyncCajeroData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
        public SyncCajeroData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public SyncCajero CrearModSyncCajero(SyncCajero pSync_Cajero, int pOpcion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_cajero = cmdTransaccionFactory.CreateParameter();
                        pcod_cajero.ParameterName = "pcodigocajero";
                        pcod_cajero.Value = pSync_Cajero.cod_cajero;
                        pcod_cajero.Direction = pOpcion == 1 ? ParameterDirection.Output : ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_cajero);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "pcodigopersona";
                        if (pSync_Cajero.cod_usuario == null)
                            pcod_usuario.Value = DBNull.Value;
                        else
                            pcod_usuario.Value = pSync_Cajero.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                        pcod_caja.ParameterName = "pcodigocaja";
                        if (pSync_Cajero.cod_caja == null)
                            pcod_caja.Value = DBNull.Value;
                        else
                            pcod_caja.Value = pSync_Cajero.cod_caja;
                        pcod_caja.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_caja);

                        DbParameter pfecha_creacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_creacion.ParameterName = "pfechaingreso";
                        if (pSync_Cajero.fecha_creacion == null)
                            pfecha_creacion.Value = DBNull.Value;
                        else
                            pfecha_creacion.Value = pSync_Cajero.fecha_creacion;
                        pfecha_creacion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pfecha_creacion);

                        DbParameter pfecha_retiro = cmdTransaccionFactory.CreateParameter();
                        pfecha_retiro.ParameterName = "pfecharetiro";
                        if (pSync_Cajero.fecha_retiro == null)
                            pfecha_retiro.Value = DBNull.Value;
                        else
                            pfecha_retiro.Value = pSync_Cajero.fecha_retiro;
                        pfecha_retiro.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pfecha_retiro);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        if (pSync_Cajero.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pSync_Cajero.estado;
                        pestado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = pOpcion == 1 ? "XPF_CAJAFIN_CAJEROINSERTAR" : "XPN_CAJAFIN_CAJERO_U";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (pOpcion == 1)
                            pSync_Cajero.cod_cajero = Convert.ToInt64(pcod_cajero.Value);
                        return pSync_Cajero;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncCajeroData", "CrearModSyncCajero", ex);
                        return null;
                    }
                }
            }
        }


        public EntityGlobal EliminarSyncCajero(SyncCajero pCajero, Usuario vUsuario)
        {
            EntityGlobal pResult = new EntityGlobal();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_cajero = cmdTransaccionFactory.CreateParameter();
                        pcod_cajero.ParameterName = "pcodigocajero";
                        pcod_cajero.Value = pCajero.cod_cajero;
                        pcod_cajero.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_cajero);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPN_CAJAFIN_CAJERO_D";
                        pResult.NroRegisterAffected = cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        pResult.Success = pResult.NroRegisterAffected > 0 ? true : false;
                    }
                    catch (Exception ex)
                    {
                        pResult.Message = ex.Message;
                        pResult.Success = false;
                    }
                    return pResult;
                }
            }
        }


        public SyncCajero ModificarCajerosAsignacion(SyncCajero pCajero, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                        pcod_caja.ParameterName = "pcodigocaja";
                        pcod_caja.Value = pCajero.cod_caja;
                        pcod_caja.Direction = ParameterDirection.Input;

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "pcodigopersona";
                        pcod_persona.Value = pCajero.cod_cajero;
                        pcod_persona.Direction = ParameterDirection.Input;

                        DbParameter p_seleccion = cmdTransaccionFactory.CreateParameter();
                        p_seleccion.ParameterName = "pestado";
                        p_seleccion.Value = pCajero.estado;
                        p_seleccion.Direction = ParameterDirection.Input;

                        DbParameter pcajerodestino = cmdTransaccionFactory.CreateParameter();
                        pcajerodestino.ParameterName = "pcajadestino";
                        if (pCajero.cod_caja_des != null)
                            pcajerodestino.Value = pCajero.cod_caja_des;
                        else
                            pcajerodestino.Value = DBNull.Value;
                        pcajerodestino.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcod_caja);
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);
                        cmdTransaccionFactory.Parameters.Add(p_seleccion);
                        cmdTransaccionFactory.Parameters.Add(pcajerodestino);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAJ_ASIGNAR_CAJ_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        
                        return pCajero;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajeroData", "ModificarCajerosAsignacion", ex);
                        return null;
                    }
                }
            }
        }

        public List<SyncCajero> ListarSyncCajero(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<SyncCajero> lstCajero = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select c.* from cajero c inner join usuarios u on c.cod_persona = u.codusuario " + pFiltro + " ORDER BY COD_CAJERO";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.HasRows)
                        {
                            lstCajero = new List<SyncCajero>();
                            SyncCajero entidad;
                            while (resultado.Read())
                            {
                                entidad = new SyncCajero();
                                if (resultado["COD_CAJERO"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["COD_CAJERO"]);
                                if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt64(resultado["COD_PERSONA"]);
                                if (resultado["COD_CAJA"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["COD_CAJA"]);
                                if (resultado["FECHA_CREACION"] != DBNull.Value) entidad.fecha_creacion = Convert.ToDateTime(resultado["FECHA_CREACION"]);
                                if (resultado["FECHA_RETIRO"] != DBNull.Value) entidad.fecha_retiro = Convert.ToDateTime(resultado["FECHA_RETIRO"]);
                                if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                                if (resultado["COD_CAJA_DES"] != DBNull.Value) entidad.cod_caja_des = Convert.ToInt64(resultado["COD_CAJA_DES"]);
                                lstCajero.Add(entidad);
                            }
                            resultado.Close();
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCajero;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncCajeroData", "ListarSyncCajero", ex);
                        return null;
                    }
                }
            }
        }


        public SyncCajero ConsultarSyncCajero(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            SyncCajero entidad = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select c.* from cajero c inner join usuarios u on c.cod_persona = u.codusuario " + pFiltro + " ORDER BY COD_CAJERO";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.HasRows)
                        {
                            while (resultado.Read())
                            {
                                entidad = new SyncCajero();
                                if (resultado["COD_CAJERO"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["COD_CAJERO"]);
                                if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt64(resultado["COD_PERSONA"]);
                                if (resultado["COD_CAJA"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["COD_CAJA"]);
                                if (resultado["FECHA_CREACION"] != DBNull.Value) entidad.fecha_creacion = Convert.ToDateTime(resultado["FECHA_CREACION"]);
                                if (resultado["FECHA_RETIRO"] != DBNull.Value) entidad.fecha_retiro = Convert.ToDateTime(resultado["FECHA_RETIRO"]);
                                if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                                if (resultado["COD_CAJA_DES"] != DBNull.Value) entidad.cod_caja_des = Convert.ToInt64(resultado["COD_CAJA_DES"]);
                            }
                            resultado.Close();
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncCajeroData", "ListarSyncCajero", ex);
                        return null;
                    }
                }
            }
        }



    }
}
