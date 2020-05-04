using System;
using System.Collections.Generic;
using System.Transactions;
using Xpinn.Sincronizacion.Data;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Util;

namespace Xpinn.Sincronizacion.Business
{
    public class SyncCajaBusiness : GlobalBusiness
    {
        SyncCajaData DaCaja;
        SyncOperacionData DAOperacion;
        public SyncCajaBusiness()
        {
            DaCaja = new SyncCajaData();
            DAOperacion = new SyncOperacionData();
        }

        public EntityGlobal CrearModSyncCaja(string[] pObjCaja, string[] lstTopes, string[] lstAtribuciones, int pOpcion, Usuario vUsuario)
        {
            EntityGlobal pResult = new EntityGlobal();
            SyncCaja pSync_Caja = null;
            string pFiltro = string.Empty;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
            {
                try
                {
                    Int64 pCodigoLocal = Convert.ToInt64(pObjCaja[0]);

                    // CREACION MODIFICACION DE CAJA
                    SyncCaja pCaja = new SyncCaja();
                    pCaja.cod_caja = pOpcion == 1 ? 0 : pCodigoLocal;
                    pCaja.cod_oficina = Convert.ToInt64(pObjCaja[1]);
                    pCaja.nombre = string.IsNullOrEmpty(pObjCaja[2]) ? null : pObjCaja[2];
                    pCaja.fecha_creacion = DateTime.ParseExact(pObjCaja[3], "dd/MM/yyyy", null);
                    pCaja.esprincipal = Convert.ToInt32(pObjCaja[4]);
                    pCaja.estado = Convert.ToInt32(pObjCaja[5]);
                    pCaja.cod_cuenta = string.IsNullOrEmpty(pObjCaja[6]) ? null : pObjCaja[6];
                    pCaja.cod_datafono = string.IsNullOrEmpty(pObjCaja[7]) ? null : pObjCaja[7];

                    pSync_Caja = DaCaja.CrearModSyncCaja(pCaja, pOpcion, vUsuario);

                    string[] campos;
                    // CREACION MODIFICACION DE TOPES
                    if (lstTopes != null)
                    {
                        foreach (string nObjTope in lstTopes)
                        {
                            campos = nObjTope.Split('|');
                            SyncTopesCaja pTopes = new SyncTopesCaja();
                            pTopes.tipo_tope = Convert.ToInt32(campos[2]);
                            pTopes.cod_caja = Convert.ToInt32(pSync_Caja.cod_caja);
                            pTopes.cod_moneda = Convert.ToInt32(campos[1]);
                            pTopes.valor_minimo = string.IsNullOrEmpty(campos[3]) ? 0 : Convert.ToDecimal(campos[3]);
                            pTopes.valor_maximo = string.IsNullOrEmpty(campos[4]) ? 0 : Convert.ToDecimal(campos[4]);
                            DaCaja.CrearModTopesCaja(pTopes, pOpcion, vUsuario);
                        }
                    }

                    // CREACION MODIFICACION DE ATRIBUCIONES
                    if (lstAtribuciones != null)
                    {
                        string pAtributos = string.Empty;
                        SyncAtribucionCaja pAtribu = null;
                        SyncAtribucionCaja pAbribCaja = null;
                        foreach (string nObjAtrib in lstAtribuciones)
                        {
                            campos = nObjAtrib.Split('|');
                            pAtributos += campos[1] + ",";

                            pAtribu = new SyncAtribucionCaja();
                            pAtribu.cod_caja = pSync_Caja.cod_caja;
                            pAtribu.tipo_tran = Convert.ToInt64(campos[1]);
                            if (pOpcion != 1)
                            {
                                pFiltro = " WHERE COD_CAJA = " + pSync_Caja.cod_caja + " AND TIPO_OPERACION = " + Convert.ToInt64(campos[1]);
                                pAbribCaja = DaCaja.ConsultarSyncAtribucionCaja(pFiltro, vUsuario);
                                if (pAbribCaja == null)
                                    DaCaja.CrearAtributosCaja(pAtribu, vUsuario);
                            }
                            else
                                DaCaja.CrearAtributosCaja(pAtribu, vUsuario);
                        }
                        if (pAtributos.Length > 1)
                            pAtributos = pAtributos.Substring(0, pAtributos.Length - 1);
                        // ELIMINANDO LOS ATRIBUTOS SOBRANTES
                        if (pOpcion != 1)
                            DaCaja.EliminarAtributosCaja(pAtributos, pSync_Caja.cod_caja, vUsuario);
                    }

                    if (pOpcion == 1)
                    {
                        //GENERANDO HOMOLOGACION
                        SyncHomologaOperacion pHomologa = new SyncHomologaOperacion();
                        pHomologa.fecha = DateTime.Now;
                        pHomologa.tabla = "CAJA";
                        pHomologa.campo_tabla = "COD_CAJA";
                        pHomologa.codigo_principal = pSync_Caja.cod_caja.ToString();
                        pHomologa.codigo_local = pCodigoLocal.ToString();
                        pHomologa.proceso = ProcesosOffline.CreacionCaja.ToString();
                        pHomologa = DAOperacion.CrearSyncHomologaOperacion(pHomologa, vUsuario);
                    }
                    pResult.Success = true;
                    pResult.CodigoGenerado = pSync_Caja.cod_caja.ToString();
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    pResult.Success = false;
                    pResult.Message = ex.Message;
                }
            }
            return pResult;
        }

        public EntityGlobal AperturaCierreSyncCaja(string pObjCaja, string pObjSaldo, Usuario vUsuario)
        {

            EntityGlobal pResult = new EntityGlobal();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
            {
                try
                {
                    if (!string.IsNullOrEmpty(pObjCaja) && !string.IsNullOrEmpty(pObjSaldo))
                    {
                        string[] pCamposCaja = pObjCaja.Split('|');
                        string[] pCamposSaldo = pObjSaldo.Split('|');

                        SyncSaldoCaja pSaldo = new SyncSaldoCaja();
                        pSaldo.cod_caja = Convert.ToInt64(pCamposSaldo[0]);
                        pSaldo.cod_cajero = Convert.ToInt64(pCamposSaldo[1]);
                        pSaldo.fecha = DateTime.ParseExact(pCamposSaldo[2], "dd/MM/yyyy", null);
                        pSaldo.cod_moneda = 1;
                        pSaldo.valor = 0;

                        DaCaja.CrearSaldoCaja(pSaldo, vUsuario);

                        SyncCaja pCaja = new SyncCaja();
                        pCaja.cod_caja = Convert.ToInt64(pCamposCaja[0]);
                        pCaja.cod_oficina = Convert.ToInt64(pCamposCaja[1]);
                        pCaja.nombre = string.IsNullOrEmpty(pCamposCaja[2]) ? null : pCamposCaja[2];
                        pCaja.fecha_creacion = DateTime.ParseExact(pCamposCaja[3], "dd/MM/yyyy", null);
                        pCaja.esprincipal = string.IsNullOrEmpty(pCamposCaja[4]) ? 0 : Convert.ToInt32(pCamposCaja[4]); ;
                        pCaja.estado = Convert.ToInt32(pCamposCaja[5]);
                        pCaja.cod_cuenta = string.IsNullOrEmpty(pCamposCaja[6]) ? null : pCamposCaja[6];
                        pCaja.cod_datafono = string.IsNullOrEmpty(pCamposCaja[7]) ? null : pCamposCaja[7];

                        DaCaja.CrearModSyncCaja(pCaja, 2, vUsuario);

                        pResult.Success = true;
                        pResult.CodigoGenerado = pCamposCaja[0];
                    }
                    else
                    {
                        pResult.Success = false;
                        pResult.Message = "No se enviaron de forma correcta los registros";
                    }
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    pResult.Success = false;
                    pResult.Message = ex.Message;
                    ts.Dispose();
                    return null;
                }
            }
            return pResult;
        }

        public EntityGlobal EliminarSyncCaja(SyncCaja pCaja, Usuario vUsuario)
        {
            EntityGlobal pEntidad = null;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEntidad = DaCaja.EliminarSyncCaja(pCaja, vUsuario);
                    ts.Complete();
                }
                return pEntidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncCajaBusiness", "CrearSyncCaja", ex);
                return null;
            }
        }

        public List<SyncCaja> ListarSyncCaja(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DaCaja.ListarSyncCaja(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncCajaBusiness", "ListarSyncCaja", ex);
                return null;
            }
        }

        public List<SyncTopesCaja> ListarSyncTopesCaja(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DaCaja.ListarSyncTopesCaja(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncCajaBusiness", "ListarSyncTopesCaja", ex);
                return null;
            }
        }

        public List<SyncAtribucionCaja> ListarSyncAtribucionCaja(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DaCaja.ListarSyncAtribucionCaja(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncCajaBusiness", "ListarSyncAtribucionCaja", ex);
                return null;
            }
        }

        public List<SyncTipoTran> ListarSyncTiposTran(int pCantidadRegistros, Usuario vUsuario)
        {
            try
            {
                return DaCaja.ListarSyncTiposTran(pCantidadRegistros, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncCajaBusiness", "ListarSyncTiposTran", ex);
                return null;
            }
        }

    }
}
