using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Util;

namespace Xpinn.Sincronizacion.Data
{
    public class SyncCajaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public SyncCajaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public SyncCaja CrearModSyncCaja(SyncCaja pSync_Caja, int pOpcion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                        pcod_caja.ParameterName = "pcodigocaja";
                        pcod_caja.Value = pSync_Caja.cod_caja;
                        pcod_caja.Direction = pOpcion == 1 ? ParameterDirection.Output : ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_caja);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "pcodigooficina";
                        pcod_oficina.Value = pSync_Caja.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "pnomcaja";
                        if (pSync_Caja.nombre == null)
                            pnombre.Value = DBNull.Value;
                        else
                            pnombre.Value = pSync_Caja.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pfecha_creacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_creacion.ParameterName = "pfechacreacion";
                        if (pSync_Caja.fecha_creacion == null)
                            pfecha_creacion.Value = DBNull.Value;
                        else
                            pfecha_creacion.Value = pSync_Caja.fecha_creacion;
                        pfecha_creacion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pfecha_creacion);

                        DbParameter pesprincipal = cmdTransaccionFactory.CreateParameter();
                        pesprincipal.ParameterName = "pesprincipal";
                        if (pSync_Caja.esprincipal == null)
                            pesprincipal.Value = DBNull.Value;
                        else
                            pesprincipal.Value = pSync_Caja.esprincipal;
                        pesprincipal.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pesprincipal);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        if (pSync_Caja.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pSync_Caja.estado;
                        pestado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "pcod_cuenta";
                        if (pSync_Caja.cod_cuenta == null)
                            pcod_cuenta.Value = DBNull.Value;
                        else
                            pcod_cuenta.Value = pSync_Caja.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter pcod_datafono = cmdTransaccionFactory.CreateParameter();
                        pcod_datafono.ParameterName = "pcod_datafono";
                        if (pSync_Caja.cod_datafono == null)
                            pcod_datafono.Value = DBNull.Value;
                        else
                            pcod_datafono.Value = pSync_Caja.cod_datafono;
                        pcod_datafono.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_datafono);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = pOpcion == 1 ? "XPF_CAJAFIN_CAJAINSERTAR" : "XPN_CAJAFIN_CAJA_U";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if(pOpcion == 1)
                            pSync_Caja.cod_caja = Convert.ToInt64(pcod_caja.Value);
                        return pSync_Caja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncCajaData", "CrearModSyncCaja", ex);
                        return null;
                    }
                }
            }
        }

        public EntityGlobal EliminarSyncCaja(SyncCaja pCaja, Usuario vUsuario)
        {
            EntityGlobal pResult = new EntityGlobal();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                        pcod_caja.ParameterName = "pcodigocaja";
                        pcod_caja.Value = pCaja.cod_caja;
                        pcod_caja.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_caja);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPN_CAJAFIN_CAJA_D";
                        pResult.NroRegisterAffected = cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pResult.Success = pResult.NroRegisterAffected > 0 ? true : false;
                    }
                    catch (Exception ex)
                    {
                        pResult.Success = false;
                        pResult.Message = ex.Message;
                    }
                    return pResult;
                }
            }
        }


        #region METODOS DE SALDO CAJA

        public SyncSaldoCaja CrearSaldoCaja(SyncSaldoCaja pSaldoCaja, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "pfecha";
                        pfecha.Value = pSaldoCaja.fecha;
                        pfecha.DbType = DbType.DateTime;
                        pfecha.Direction = ParameterDirection.Input;

                        DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                        pcod_caja.ParameterName = "pcodigocaja";
                        pcod_caja.Value = pSaldoCaja.cod_caja;
                        pcod_caja.DbType = DbType.Int64;
                        pcod_caja.Direction = ParameterDirection.Input;

                        DbParameter pcodx_cajero = cmdTransaccionFactory.CreateParameter();
                        pcodx_cajero.ParameterName = "pcodigocajero";
                        pcodx_cajero.Value = pSaldoCaja.cod_cajero;
                        pcodx_cajero.DbType = DbType.Int64;
                        pcodx_cajero.Direction = ParameterDirection.Input;

                        DbParameter pcodi_moneda = cmdTransaccionFactory.CreateParameter();
                        pcodi_moneda.ParameterName = "pcodigomoneda";
                        pcodi_moneda.Value = pSaldoCaja.cod_moneda;
                        pcodi_moneda.DbType = DbType.Int32;
                        pcodi_moneda.Direction = ParameterDirection.Input;

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = pSaldoCaja.valor;
                        pvalor.DbType = DbType.Decimal;
                        pvalor.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pfecha);
                        cmdTransaccionFactory.Parameters.Add(pcod_caja);
                        cmdTransaccionFactory.Parameters.Add(pcodx_cajero);
                        cmdTransaccionFactory.Parameters.Add(pcodi_moneda);
                        cmdTransaccionFactory.Parameters.Add(pvalor);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_SALDO_CAJA_RECEP";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pSaldoCaja, "SALDOCAJA", pUsuario, Accion.Crear.ToString(), TipoAuditoria.CajaFinanciera, "Crear Saldo Caja");
                            //DAauditoria.InsertarLog(pSaldoCaja, pUsuario, pSaldoCaja.cod_caja, "SALDOCAJA", Accion.Crear.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA
                        return pSaldoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SaldoCajaData", "CrearSaldoCaja", ex);
                        return null;
                    }
                }
            }
        }

        public void AsignarSaldoCaja(SyncCaja pCaja, DateTime pFecProceso, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                        pcod_caja.ParameterName = "pcodigocaja";
                        pcod_caja.Value = pCaja.cod_caja;

                        DbParameter pes_principal = cmdTransaccionFactory.CreateParameter();
                        pes_principal.ParameterName = "pesprincipal";
                        pes_principal.Value = pCaja.esprincipal;

                        DbParameter pfec_proceso = cmdTransaccionFactory.CreateParameter();
                        pfec_proceso.ParameterName = "pfechaproceso";
                        pfec_proceso.Value = pFecProceso;

                        cmdTransaccionFactory.Parameters.Add(pcod_caja);
                        cmdTransaccionFactory.Parameters.Add(pes_principal);
                        cmdTransaccionFactory.Parameters.Add(pfec_proceso);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_ASIG_SALCAJ";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncOficinaData", "AsignarSaldoCaja", ex);
                    }
                }
            }
        }


        public bool ModificarSaldoCaja(SyncSaldoCaja pSaldoCaja, int pTipoMov, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "pfecha";
                        pfecha.Value = pSaldoCaja.fecha;
                        pfecha.Direction = ParameterDirection.Input;

                        DbParameter pcoda_caja = cmdTransaccionFactory.CreateParameter();
                        pcoda_caja.ParameterName = "pcodigocaja";
                        pcoda_caja.Value = pSaldoCaja.cod_caja;
                        pcoda_caja.Direction = ParameterDirection.Input;

                        DbParameter pcoda_cajero = cmdTransaccionFactory.CreateParameter();
                        pcoda_cajero.ParameterName = "pcodigocajero";
                        pcoda_cajero.Value = pSaldoCaja.cod_cajero;
                        pcoda_cajero.Direction = ParameterDirection.Input;

                        DbParameter pcodi_moneda = cmdTransaccionFactory.CreateParameter();
                        pcodi_moneda.ParameterName = "pcodigomoneda";
                        pcodi_moneda.Value = pSaldoCaja.cod_moneda;
                        pcodi_moneda.Direction = ParameterDirection.Input;

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = pSaldoCaja.valor;
                        pvalor.Direction = ParameterDirection.Input;

                        DbParameter ptipomov = cmdTransaccionFactory.CreateParameter();
                        ptipomov.ParameterName = "ptipomov";
                        ptipomov.Value = pTipoMov;
                        pvalor.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pfecha);
                        cmdTransaccionFactory.Parameters.Add(pcoda_caja);
                        cmdTransaccionFactory.Parameters.Add(pcoda_cajero);
                        cmdTransaccionFactory.Parameters.Add(pcodi_moneda);
                        cmdTransaccionFactory.Parameters.Add(pvalor);
                        cmdTransaccionFactory.Parameters.Add(ptipomov);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_SALDO_CAJA_OPE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajaData", "ModificarSaldoCaja", ex);
                        return false;
                    }
                }
            }
        }


        public bool CrearSaldoCajaConsig(SyncSaldoCaja pSaldoCaja, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "pfecha";
                        pfecha.Value = pSaldoCaja.fecha;
                        pfecha.DbType = DbType.DateTime;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.Size = 7;

                        DbParameter pcoda_caja = cmdTransaccionFactory.CreateParameter();
                        pcoda_caja.ParameterName = "pcodigocaja";
                        pcoda_caja.Value = pSaldoCaja.cod_caja;
                        pcoda_caja.Size = 8;
                        pcoda_caja.DbType = DbType.Int32;
                        pcoda_caja.Direction = ParameterDirection.Input;

                        DbParameter pcoda_cajero = cmdTransaccionFactory.CreateParameter();
                        pcoda_cajero.ParameterName = "pcodigocajero";
                        pcoda_cajero.Value = pSaldoCaja.cod_cajero;
                        pcoda_cajero.Size = 8;
                        pcoda_cajero.DbType = DbType.Int32;
                        pcoda_cajero.Direction = ParameterDirection.Input;

                        DbParameter pcodi_moneda = cmdTransaccionFactory.CreateParameter();
                        pcodi_moneda.ParameterName = "pcodigomoneda";
                        pcodi_moneda.Value = pSaldoCaja.cod_moneda;
                        pcodi_moneda.Size = 8;
                        pcodi_moneda.DbType = DbType.Int32;
                        pcodi_moneda.Direction = ParameterDirection.Input;

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = pSaldoCaja.valor;
                        pvalor.DbType = DbType.Decimal;
                        pvalor.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pfecha);
                        cmdTransaccionFactory.Parameters.Add(pcoda_caja);
                        cmdTransaccionFactory.Parameters.Add(pcoda_cajero);
                        cmdTransaccionFactory.Parameters.Add(pcodi_moneda);
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_SALDO_CAJA_CONSIG";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajaData", "CrearSaldoCajaConsig", ex);
                        return false;
                    }
                }
            }
        }


        public bool ModificarSaldoCajaTraslado(SyncSaldoCaja pSaldoCaja, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "pfecha";
                        pfecha.Value = pSaldoCaja.fecha;

                        DbParameter pcodx_caja = cmdTransaccionFactory.CreateParameter();
                        pcodx_caja.ParameterName = "pcodigocaja";
                        pcodx_caja.Value = pSaldoCaja.cod_caja;

                        DbParameter pcodx_cajero = cmdTransaccionFactory.CreateParameter();
                        pcodx_cajero.ParameterName = "pcodigocajero";
                        pcodx_cajero.Value = pSaldoCaja.cod_cajero;

                        DbParameter pcodi_moneda = cmdTransaccionFactory.CreateParameter();
                        pcodi_moneda.ParameterName = "pcodigomoneda";
                        pcodi_moneda.Value = pSaldoCaja.cod_moneda;

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = pSaldoCaja.valor;

                        cmdTransaccionFactory.Parameters.Add(pfecha);
                        cmdTransaccionFactory.Parameters.Add(pcodx_caja);
                        cmdTransaccionFactory.Parameters.Add(pcodx_cajero);
                        cmdTransaccionFactory.Parameters.Add(pcodi_moneda);
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_SALDO_CAJA_TRASLA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajaData", "ModificarSaldoCajaTraslado", ex);
                        return false;
                    }
                }
            }
        }


        #endregion

        #region METODOS DE TOPES CAJA


        public SyncTopesCaja CrearModTopesCaja(SyncTopesCaja pSync_Tope, int pOpcion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_tipoTope = cmdTransaccionFactory.CreateParameter();
                        p_tipoTope.ParameterName = "pcodigotope";
                        p_tipoTope.Value = pSync_Tope.tipo_tope;
                        p_tipoTope.DbType = DbType.Int32;
                        p_tipoTope.Direction = ParameterDirection.Input;

                        DbParameter p_moneda = cmdTransaccionFactory.CreateParameter();
                        p_moneda.ParameterName = "pcodigomoneda";
                        p_moneda.Value = pSync_Tope.cod_moneda;
                        p_moneda.DbType = DbType.Int32;
                        p_moneda.Direction = ParameterDirection.Input;

                        DbParameter p_caja = cmdTransaccionFactory.CreateParameter();
                        p_caja.ParameterName = "pcodigocaja";
                        p_caja.Value = pSync_Tope.cod_caja;
                        p_caja.DbType = DbType.Int16;
                        p_caja.Direction = ParameterDirection.Input;

                        DbParameter pval_minimo = cmdTransaccionFactory.CreateParameter();
                        pval_minimo.ParameterName = "pvalminimo";
                        pval_minimo.Value = pSync_Tope.valor_minimo;
                        pval_minimo.DbType = DbType.Decimal;
                        pval_minimo.Direction = ParameterDirection.Input;

                        DbParameter pval_maximo = cmdTransaccionFactory.CreateParameter();
                        pval_maximo.ParameterName = "pvalmaximo";
                        pval_maximo.Value = pSync_Tope.valor_maximo;
                        pval_maximo.DbType = DbType.Decimal;
                        pval_maximo.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_tipoTope);
                        cmdTransaccionFactory.Parameters.Add(p_moneda);
                        cmdTransaccionFactory.Parameters.Add(p_caja);
                        cmdTransaccionFactory.Parameters.Add(pval_minimo);
                        cmdTransaccionFactory.Parameters.Add(pval_maximo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = pOpcion == 1 ? "XPN_CAJAFIN_CAJA_TOPE_C" : "XPN_CAJAFIN_CAJA_TOPE_U";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pSync_Tope;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncCajaData", "CrearModTopesCaja", ex);
                        return null;
                    }
                }
            }
        }


        #endregion

        #region METODOS DE ATRIBUCIONES DE CAJA

        public SyncAtribucionCaja CrearAtributosCaja(SyncAtribucionCaja pSync_Caja, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigo_caja = cmdTransaccionFactory.CreateParameter();
                        pcodigo_caja.ParameterName = "pcodigocaja";
                        pcodigo_caja.Value = pSync_Caja.cod_caja;
                        pcodigo_caja.DbType = DbType.Int64;
                        pcodigo_caja.Direction = ParameterDirection.Input;

                        DbParameter pcodigo_tipoOpe = cmdTransaccionFactory.CreateParameter();
                        pcodigo_tipoOpe.ParameterName = "pcodigotipope";
                        pcodigo_tipoOpe.Value = pSync_Caja.tipo_tran;
                        pcodigo_tipoOpe.DbType = DbType.Int64;
                        pcodigo_tipoOpe.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcodigo_caja);
                        cmdTransaccionFactory.Parameters.Add(pcodigo_tipoOpe);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPN_CAJAFIN_ATRIB_CAJA_C";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pSync_Caja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncCajaData", "CrearAtributosCaja", ex);
                        return null;
                    }
                }
            }
        }

                                

        public void EliminarAtributosCaja(string pLista, Int64 pCod_caja, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "DELETE FROM ATRIBUCIONES_CAJA WHERE COD_CAJA = " + pCod_caja;
                        sql += " AND TIPO_OPERACION NOT IN (" + pLista + ")";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncCajaData", "EliminarAtributosCaja", ex);
                    }
                }
            }
        }

        #endregion

        public List<SyncCaja> ListarSyncCaja(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<SyncCaja> lstCaja = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CAJA " + pFiltro + " ORDER BY COD_CAJA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.HasRows)
                        {
                            SyncCaja entidad;
                            lstCaja = new List<SyncCaja>();
                            while (resultado.Read())
                            {
                                entidad = new SyncCaja();
                                if (resultado["COD_CAJA"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["COD_CAJA"]);
                                if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                                if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                                if (resultado["FECHA_CREACION"] != DBNull.Value) entidad.fecha_creacion = Convert.ToDateTime(resultado["FECHA_CREACION"]);
                                if (resultado["ESPRINCIPAL"] != DBNull.Value) entidad.esprincipal = Convert.ToInt32(resultado["ESPRINCIPAL"]);
                                if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                                if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                                if (resultado["COD_DATAFONO"] != DBNull.Value) entidad.cod_datafono = Convert.ToString(resultado["COD_DATAFONO"]);
                                lstCaja.Add(entidad);
                            }
                            resultado.Close();
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncCajaData", "ListarSync_Caja", ex);
                        return null;
                    }
                }
            }
        }

        public List<SyncTopesCaja> ListarSyncTopesCaja(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<SyncTopesCaja> lstTopes = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TOPESCAJA " + pFiltro + " ORDER BY COD_CAJA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.HasRows)
                        {
                            SyncTopesCaja entidad;
                            lstTopes = new List<SyncTopesCaja>();
                            while (resultado.Read())
                            {
                                entidad = new SyncTopesCaja();
                                if (resultado["COD_CAJA"] != DBNull.Value) entidad.cod_caja = Convert.ToInt32(resultado["COD_CAJA"]);
                                if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt32(resultado["COD_MONEDA"]);
                                if (resultado["TIPO_TOPE"] != DBNull.Value) entidad.tipo_tope = Convert.ToInt32(resultado["TIPO_TOPE"]);
                                if (resultado["VALOR_MINIMO"] != DBNull.Value) entidad.valor_minimo = Convert.ToDecimal(resultado["VALOR_MINIMO"]);
                                if (resultado["VALOR_MAXIMO"] != DBNull.Value) entidad.valor_maximo = Convert.ToDecimal(resultado["VALOR_MAXIMO"]);
                                lstTopes.Add(entidad);
                            }
                            resultado.Close();
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTopes;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncCajaData", "ListarSyncTopesCaja", ex);
                        return null;
                    }
                }
            }
        }


        public List<SyncAtribucionCaja> ListarSyncAtribucionCaja(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<SyncAtribucionCaja> lstAtribuciones = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM ATRIBUCIONES_CAJA " + pFiltro + " ORDER BY COD_CAJA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.HasRows)
                        {
                            SyncAtribucionCaja entidad;
                            lstAtribuciones = new List<SyncAtribucionCaja>();
                            while (resultado.Read())
                            {
                                entidad = new SyncAtribucionCaja();
                                if (resultado["COD_CAJA"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["COD_CAJA"]);
                                if (resultado["TIPO_OPERACION"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt64(resultado["TIPO_OPERACION"]);
                                lstAtribuciones.Add(entidad);
                            }
                            resultado.Close();
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAtribuciones;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncCajaData", "ListarSyncAtribucionCaja", ex);
                        return null;
                    }
                }
            }
        }


        public SyncAtribucionCaja ConsultarSyncAtribucionCaja(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            SyncAtribucionCaja entidad = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM ATRIBUCIONES_CAJA " + pFiltro + " ORDER BY COD_CAJA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.HasRows)
                        {
                            if (resultado.Read())
                            {
                                entidad = new SyncAtribucionCaja();
                                if (resultado["COD_CAJA"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["COD_CAJA"]);
                                if (resultado["TIPO_OPERACION"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt64(resultado["TIPO_OPERACION"]);
                            }
                            resultado.Close();
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncCajaData", "ConsultarSyncAtribucionCaja", ex);
                        return null;
                    }
                }
            }

        }

        public List<SyncTipoTran> ListarSyncTiposTran(int pCantidadRegistros, Usuario vUsuario)
        {
            DbDataReader resultado;
            DbDataReader resultadoCant;
            int CantReg = 0;
            List<SyncTipoTran> lstTipoTran = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();

                        string sql = @"SELECT count(tipo_tran) CANTIDAD FROM TIPO_TRAN";
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultadoCant = cmdTransaccionFactory.ExecuteReader();
                        if (resultadoCant.HasRows)
                        {
                            if (resultadoCant.Read())
                                if (resultadoCant["CANTIDAD"] != DBNull.Value) CantReg = Convert.ToInt32(resultadoCant["CANTIDAD"]);
                        }

                        if (CantReg != pCantidadRegistros)
                        {
                            sql = "SELECT * FROM TIPO_TRAN";

                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql;
                            resultado = cmdTransaccionFactory.ExecuteReader();

                            if (resultado.HasRows)
                            {
                                SyncTipoTran entidad;
                                lstTipoTran = new List<SyncTipoTran>();
                                while (resultado.Read())
                                {
                                    entidad = new SyncTipoTran();
                                    if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt32(resultado["TIPO_TRAN"]);
                                    if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                                    if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.cod_tipo_producto = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                                    if (resultado["TIPO_MOV"] != DBNull.Value) entidad.tipo_mov = Convert.ToInt32(resultado["TIPO_MOV"]);
                                    if (resultado["TIPO_CAJA"] != DBNull.Value) entidad.tipo_caja = Convert.ToInt32(resultado["TIPO_CAJA"]);
                                    if (resultado["PORC_IMP"] != DBNull.Value) entidad.porc_imp = Convert.ToDecimal(resultado["PORC_IMP"]);
                                    lstTipoTran.Add(entidad);
                                }
                                resultado.Close();
                            }
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipoTran;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncCajaData", "ListarSyncTiposTran", ex);
                        return null;
                    }
                }
            }
        }

    }
}
