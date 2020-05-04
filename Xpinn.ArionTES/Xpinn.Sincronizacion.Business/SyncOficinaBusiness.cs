using System;
using System.Collections.Generic;
using System.Transactions;
using Xpinn.Sincronizacion.Data;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Util;

namespace Xpinn.Sincronizacion.Business
{
    public class SyncOficinaBusiness : GlobalBusiness
    {
        SyncOficinaData DAOficina;
        SyncOperacionData DAOperacion;
        public SyncOficinaBusiness()
        {
            DAOficina = new SyncOficinaData();
            DAOperacion = new SyncOperacionData();
        }


        public EntityGlobal GenerarAperturaCierre(string pObjOficina, Usuario vUsuario)
        {
            EntityGlobal pResult = new EntityGlobal();
            SyncCajaData DACaja = new SyncCajaData();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    string[] pCampos = pObjOficina.Split('|');
                    SyncProcesoOficina pProcesoOfi = new SyncProcesoOficina();
                    pProcesoOfi.cod_oficina = Convert.ToInt64(pCampos[1]);
                    pProcesoOfi.cod_usuario = Convert.ToInt64(pCampos[5]);
                    pProcesoOfi.fecha_proceso = DateTime.ParseExact(pCampos[2], "dd/MM/yyyy", null);
                    pProcesoOfi.tipo_proceso = Convert.ToInt32(pCampos[4]);
                    pProcesoOfi.tipo_horario = Convert.ToInt32(pCampos[3]);

                    // GENERAR VALIDACIÓN
                    string pValidacion = DAOficina.GenerarValidacionProcesoOficina(pProcesoOfi, vUsuario);
                    if (string.IsNullOrWhiteSpace(pValidacion))
                    {
                        pProcesoOfi = DAOficina.CrearProcesoOficina(pProcesoOfi, vUsuario);

                        string pFiltro = " WHERE COD_OFICINA = " + pProcesoOfi.cod_oficina;
                        List<SyncCaja> lstCaja = DACaja.ListarSyncCaja(pFiltro, vUsuario);

                        if (lstCaja != null)
                        {
                            foreach (SyncCaja nCaja in lstCaja)
                            {
                                DACaja.AsignarSaldoCaja(nCaja, pProcesoOfi.fecha_proceso, vUsuario);
                            }
                        }

                        //GENERANDO HOMOLOGACION
                        SyncHomologaOperacion pHomologa = new SyncHomologaOperacion();
                        pHomologa.fecha = DateTime.Now;
                        pHomologa.tabla = "PROCESOOFICINA";
                        pHomologa.campo_tabla = "COD_OFICINA|TIPO_PROCESO|FECHA_PROCESO";
                        pHomologa.codigo_principal = pProcesoOfi.cod_oficina + "|" + pProcesoOfi.tipo_proceso + "|" + pCampos[2];
                        pHomologa.codigo_local = pProcesoOfi.cod_oficina.ToString();
                        pHomologa.proceso = ProcesosOffline.AperturaCierreOficina.ToString();
                        pHomologa = DAOperacion.CrearSyncHomologaOperacion(pHomologa, vUsuario);

                        pResult.Success = true;
                        pResult.CodigoGenerado = pProcesoOfi.cod_oficina.ToString();
                        ts.Complete();
                    }
                    else
                    {
                        pResult.Success = false;
                        pResult.Message = pValidacion;
                        ts.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    pResult.Success = false;
                    pResult.Message = ex.Message;
                    ts.Dispose();
                }
            }
            return pResult;
        }


        public Oficina CrearModSyncOficina(Oficina pSync_Oficina, int pOpcion, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSync_Oficina = DAOficina.CrearModSyncOficina(pSync_Oficina, pOpcion, vUsuario);
                    ts.Complete();
                }
                return pSync_Oficina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaBusiness", "CrearModSyncOficina", ex);
                return null;
            }
        }


        public EntityGlobal EliminarSyncOficina(Oficina pOficina, Usuario vUsuario)
        {
            EntityGlobal pEntidad = null;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEntidad = DAOficina.EliminarSyncOficina(pOficina, vUsuario);
                    ts.Complete();
                }
                return pEntidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaBusiness", "EliminarSyncOficina", ex);
                return null;
            }
        }

        public DateTime? ConsultarFecUltCierre(Int64 pCodOficina, Usuario pUsuario)
        {
            DateTime? pFecUltCierre = null;
            try
            {
                pFecUltCierre = DAOficina.ConsultarFecUltCierre(pCodOficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaBusiness", "ConsultarFecUltCierre", ex);
                return null;
            }
            return pFecUltCierre;
        }


        public List<Oficina> ListarSyncOficina(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAOficina.ListarSyncOficina(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaBusiness", "ListarSyncOficina", ex);
                return null;
            }
        }

        public List<SyncHorarioOficina> ListarSyncHorarioOficina(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAOficina.ListarSyncHorarioOficina(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaBusiness", "ListarSyncHorarioOficina", ex);
                return null;
            }
        }

        public List<SyncProcesoOficina> ListarSyncProcesoOficina(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAOficina.ListarSyncProcesoOficina(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaBusiness", "ListarSyncProcesoOficina", ex);
                return null;
            }
        }

        public SyncProcesoOficina ConsultarSyncProcesoOficina(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAOficina.ConsultarSyncProcesoOficina(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaBusiness", "ConsultarSyncProcesoOficina", ex);
                return null;
            }
        }

    }
}
