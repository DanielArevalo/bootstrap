using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Business
{

    public class ConciliacionBancariaBusiness : GlobalBusiness
    {
        private ConciliacionBancariaData BAConcilia;


        public ConciliacionBancariaBusiness()
        {
            BAConcilia = new ConciliacionBancariaData();
        }


        public ConciliacionBancaria CrearConciliacion(ConciliacionBancaria pConci, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pConci = BAConcilia.CrearConciliacion(pConci, vUsuario);
                    int cod = pConci.idconciliacion;
                    
                    if (pConci.lstResumen != null && pConci.lstResumen.Count > 0)
                    {
                        foreach (CONCBANCARIA_RESUMEN pResu in pConci.lstResumen)
                        {
                            CONCBANCARIA_RESUMEN nResumen = new CONCBANCARIA_RESUMEN();
                            pResu.idconciliacion = cod;
                            nResumen = BAConcilia.CrearResumenConsi(pResu, vUsuario, 1);//CREAR
                        }                    
                    }

                    if (pConci.lstDetalle != null && pConci.lstDetalle.Count > 0)
                    {
                        foreach (CONCBANCARIA_DETALLE pDeta in pConci.lstDetalle)
                        {
                            CONCBANCARIA_DETALLE nResumen = new CONCBANCARIA_DETALLE();
                            pDeta.idconciliacion = cod;
                            nResumen = BAConcilia.CrearDetalleConsi(pDeta, vUsuario, 1);//CREAR
                        }
                    }

                    ts.Complete();
                }
                return pConci;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaBusiness", "CrearConciliacion", ex);
                return null;
            }
        }


        public ConciliacionBancaria ModificarConciliacion(ConciliacionBancaria pConci, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pConci = BAConcilia.ModificarConciliacion(pConci, vUsuario);
                    int cod = pConci.idconciliacion;

                    if (pConci.lstResumen != null && pConci.lstResumen.Count > 0)
                    {
                        foreach (CONCBANCARIA_RESUMEN pResu in pConci.lstResumen)
                        {
                            CONCBANCARIA_RESUMEN nResumen = new CONCBANCARIA_RESUMEN();
                            pResu.idconciliacion = cod;
                            if (pResu.idresumen > 0 && pResu.idresumen != 0 && pResu.idresumen != null)
                                nResumen = BAConcilia.CrearResumenConsi(pResu, vUsuario, 2);//MODIFICAR
                            else
                                nResumen = BAConcilia.CrearResumenConsi(pResu, vUsuario, 1);//CREAR
                        }
                    }

                    if (pConci.lstDetalle != null && pConci.lstDetalle.Count > 0)
                    {
                        foreach (CONCBANCARIA_DETALLE pDeta in pConci.lstDetalle)
                        {
                            CONCBANCARIA_DETALLE nResumen = new CONCBANCARIA_DETALLE();
                            pDeta.idconciliacion = cod;
                            if (pDeta.iddetalle > 0 && pDeta.iddetalle != 0 && pDeta.iddetalle != null)
                                nResumen = BAConcilia.CrearDetalleConsi(pDeta, vUsuario, 2);//MODIFICAR
                            else
                                nResumen = BAConcilia.CrearDetalleConsi(pDeta,vUsuario,1); //CREAR
                        }
                    }

                    ts.Complete();
                }
                return pConci;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaBusiness", "ModificarConciliacion", ex);
                return null;
            }
        }



        public void EliminarConciliacion(Int32 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BAConcilia.EliminarConciliacion(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaBusiness", "EliminarConciliacion", ex);
            }
        }


        public List<ConciliacionBancaria> ListarConciliacion(String filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            try
            {
                return BAConcilia.ListarConciliacion(filtro,pFechaIni, pFechaFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaBusiness", "ListarConciliacion", ex);
                return null;
            }
        }


        public ConciliacionBancaria ConsultarConciliacion(ConciliacionBancaria pConcili, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pConcili = BAConcilia.ConsultarConciliacion(pConcili, vUsuario);

                    ts.Complete();
                }

                return pConcili;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaBusiness", "ConsultarConciliacion", ex);
                return null;
            }
        }



        public List<ConciliacionBancaria> ListarCuentasBancarias(ConciliacionBancaria pConci, Usuario vUsuario)
        {
            try
            {
                return BAConcilia.ListarCuentasBancarias(pConci, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaBusiness", "ListarCuentasBancarias", ex);
                return null;
            }
        }


        public ConciliacionBancaria ConsultarCuentasBancarias(ConciliacionBancaria pConci, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pConci = BAConcilia.ConsultarCuentasBancarias(pConci, vUsuario);

                    ts.Complete();
                }
                return pConci;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaBusiness", "ConsultarCuentasBancarias", ex);
                return null;
            }
        }


        public List<ConciliacionBancaria> ListarPlanCuentas(ConciliacionBancaria pConci, Usuario vUsuario)
        {
            try
            {
                return BAConcilia.ListarPlanCuentas(pConci, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaBusiness", "ListarPlanCuentas", ex);
                return null;
            }
        }


        public List<ConciliacionBancaria> ListarExtracto(ConciliacionBancaria pConci, Usuario vUsuario)
        {
            try
            {
                return BAConcilia.ListarExtracto(pConci, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaBusiness", "ListarExtracto", ex);
                return null;
            }
        }


        public ConciliacionBancaria ConsultarExtracto(ConciliacionBancaria pConci, int pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pConci = BAConcilia.ConsultarExtracto(pConci, pId, vUsuario);

                    ts.Complete();
                }
                return pConci;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaBusiness", "ConsultarExtracto", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BAConcilia.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaBusiness", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }


        public ConciliacionBancaria GenerarConciliacion(ConciliacionBancaria pConci, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pConci = BAConcilia.GenerarConciliacion(pConci, vUsuario);

                    ts.Complete();
                }
                return pConci;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaBusiness", "GenerarConciliacion", ex);
                return null;
            }
        }



        public List<CONCBANCARIA_RESUMEN> ListarTemporalResumen(CONCBANCARIA_RESUMEN pConci, Usuario vUsuario)
        {
            try
            {
                return BAConcilia.ListarTemporalResumen(pConci, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaBusiness", "ListarTemporalResumen", ex);
                return null;
            }
        }


        public List<CONCBANCARIA_DETALLE> ListarTemporalDetalle(CONCBANCARIA_DETALLE pConci, int opcion, Usuario vUsuario)
        {
            try
            {
                return BAConcilia.ListarTemporalDetalle(pConci,opcion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaBusiness", "ListarTemporalDetalle", ex);
                return null;
            }
        }


        public List<CONCBANCARIA_RESUMEN> ListarResumenConciliacion(Int32 pId, Usuario vUsuario)
        {
            try
            {
                return BAConcilia.ListarResumenConciliacion(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaBusiness", "ListarResumenConciliacion", ex);
                return null;
            }
        }


        public List<CONCBANCARIA_DETALLE> ListarDetalleConciliacion(Int32 pId, Usuario vUsuario)
        {
            try
            {
                return BAConcilia.ListarDetalleConciliacion(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaBusiness", "ListarDetalleConciliacion", ex);
                return null;
            }
        }


    }
}