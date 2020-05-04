using System;
using System.Collections.Generic;
using System.Transactions;
using Xpinn.Sincronizacion.Data;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Util;

namespace Xpinn.Sincronizacion.Business
{
    public class SyncCajeroBusiness : GlobalBusiness
    {
        SyncCajeroData DACajero;
        SyncOperacionData DAOperacion;
        public SyncCajeroBusiness()
        {
            DACajero = new SyncCajeroData();
            DAOperacion = new SyncOperacionData();
        }

        public EntityGlobal CrearModSyncCajero(string[] pObjCajero, int pOpcion, Usuario vUsuario)
        {

            EntityGlobal pResult = new EntityGlobal();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    long pCodigoLocal = Convert.ToInt64(pObjCajero[0]);
                    SyncCajero pCajeroResult = new SyncCajero();
                    SyncCajero pCajero = new SyncCajero();
                    pCajero.cod_cajero = Convert.ToInt64(pObjCajero[0]);
                    pCajero.cod_usuario = Convert.ToInt64(pObjCajero[1]);
                    if (string.IsNullOrEmpty(pObjCajero[2]))
                        pCajero.cod_caja = null;
                    else
                        pCajero.cod_caja = Convert.ToInt64(pObjCajero[2]);
                    if (string.IsNullOrEmpty(pObjCajero[3]))
                        pCajero.fecha_creacion = null;
                    else
                        pCajero.fecha_creacion = DateTime.ParseExact(pObjCajero[3], "dd/MM/yyyy", null);
                    if (string.IsNullOrEmpty(pObjCajero[4]))
                        pCajero.fecha_retiro = null;
                    else
                        pCajero.fecha_retiro = DateTime.ParseExact(pObjCajero[4], "dd/MM/yyyy", null);
                    pCajero.estado = string.IsNullOrEmpty(pObjCajero[5]) ? 0 : Convert.ToInt32(pObjCajero[5]);

                    pCajeroResult = DACajero.CrearModSyncCajero(pCajero, pOpcion, vUsuario);

                    if (pOpcion == 1)
                    {
                        //GENERANDO HOMOLOGACION
                        SyncHomologaOperacion pHomologa = new SyncHomologaOperacion();
                        pHomologa.fecha = DateTime.Now;
                        pHomologa.tabla = "CAJERO";
                        pHomologa.campo_tabla = "COD_CAJERO";
                        pHomologa.codigo_principal = pCajeroResult.cod_cajero.ToString();
                        pHomologa.codigo_local = pCodigoLocal.ToString();
                        pHomologa.proceso = ProcesosOffline.CreacionCajero.ToString();
                        pHomologa = DAOperacion.CrearSyncHomologaOperacion(pHomologa, vUsuario);
                    }

                    pResult.Success = true;
                    pResult.CodigoGenerado = pCajero.cod_cajero.ToString();
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


        public EntityGlobal EliminarSyncCajero(SyncCajero pCajero, Usuario vUsuario)
        {
            EntityGlobal pEntidad = new EntityGlobal();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    pEntidad = DACajero.EliminarSyncCajero(pCajero, vUsuario);
                    pEntidad.CodigoGenerado = pEntidad.Success ? pCajero.cod_cajero.ToString() : null;
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    pEntidad.Success = false;
                    pEntidad.Message = ex.Message;
                    ts.Dispose();
                    return null;
                }
            }
            return pEntidad;
        }

        public EntityGlobal AsignarSyncCajero(string pObjCajero, Usuario vUsuario)
        {

            EntityGlobal pResult = new EntityGlobal();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    string[] pCampos = pObjCajero.Split('|');
                    if (string.IsNullOrEmpty(pCampos[0]))
                    {
                        SyncCajero pCajero = new SyncCajero();
                        if (string.IsNullOrEmpty(pCampos[2]))
                            pCajero.cod_caja = null;
                        else
                            pCajero.cod_caja = Convert.ToInt64(pCampos[2]);
                        pCajero.cod_cajero = Convert.ToInt64(pCampos[0]);
                        pCajero.estado = Convert.ToInt32(pCampos[5]);
                        pCajero.cod_caja_des = Convert.ToInt64(pCampos[6]);
                        pCajero = DACajero.ModificarCajerosAsignacion(pCajero, vUsuario);

                        pResult.Success = true;
                        pResult.CodigoGenerado = pResult.Success ? pCajero.cod_cajero.ToString() : null;
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


        public List<SyncCajero> ListarSyncCajero(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DACajero.ListarSyncCajero(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncCajeroBusiness", "ListarSyncCajero", ex);
                return null;
            }
        }


    }
}
