using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.NIIF.Entities;
using Xpinn.NIIF.Data;
using System.Transactions;
using System.Data;
using Xpinn.Util;

namespace Xpinn.NIIF.Business
{
    public class PlanCuentasNIIFBusiness
    {
        protected ExcepcionBusiness BOExcepcion = new ExcepcionBusiness();
        /// Objeto de negocio para Credito
        /// 
        private PlanCuentasNIIFData DAPlanCuentas;

                /// <summary>
        /// Constructor del objeto de negocio para Atributo
        /// </summary>
        public PlanCuentasNIIFBusiness()
        {
            DAPlanCuentas = new PlanCuentasNIIFData();
        }

        public Xpinn.Contabilidad.Entities.PlanCuentas CrearPlanCuentasNIIF(Xpinn.Contabilidad.Entities.PlanCuentas pPlanCuentasNIIF, List<PlanCtasHomologacionNIF> lstData, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BalanceNIIFData DaNiif = new BalanceNIIFData();
                    pPlanCuentasNIIF = DAPlanCuentas.CrearPlanCuentasNIIF(pPlanCuentasNIIF, vUsuario);
                    if (lstData.Count > 0)
                    {
                        foreach (PlanCtasHomologacionNIF pCuenta in lstData)
                        {
                            PlanCtasHomologacionNIF pEntidad = new PlanCtasHomologacionNIF();
                            pEntidad = DaNiif.CrearPlanCtasHomologacion(pCuenta, vUsuario);
                        }
                    }
                    ts.Complete();
                }

                return pPlanCuentasNIIF;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasNIIFBusiness", "CrearPlanCuentasNIIF", ex);
                return null;
            }

        }

        public Xpinn.Contabilidad.Entities.PlanCuentas ModificarPlanCuentasNIIF(Xpinn.Contabilidad.Entities.PlanCuentas pPlanCuentasNIIF,List<PlanCtasHomologacionNIF> lstData,  Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BalanceNIIFData DaNiif = new BalanceNIIFData();
                    pPlanCuentasNIIF = DAPlanCuentas.ModificarPlanCuentasNIIF(pPlanCuentasNIIF, vUsuario);
                    if (lstData.Count > 0)
                    {
                        foreach (PlanCtasHomologacionNIF pCuenta in lstData)
                        {
                            PlanCtasHomologacionNIF pEntidad = new PlanCtasHomologacionNIF();
                            if (pCuenta.idhomologa > 0)
                                pEntidad = DaNiif.ModificarPlanCtasHomologacion(pCuenta, vUsuario);
                            else
                                pEntidad = DaNiif.CrearPlanCtasHomologacion(pCuenta, vUsuario);
                        }
                    }
                    ts.Complete();
                }

                return pPlanCuentasNIIF;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasNIIFBusiness", "ModificarPlanCuentasNIIF", ex);
                return null;
            }
        }

        public void EliminarHomologacionNIIFLocal(Int64 pId, Usuario vUsuario)
        {

            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAPlanCuentas.EliminarHomologacionNIIFLocal(pId, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasNIIFBusiness", "EliminarHomologacionNIIFLocal", ex);
            }
        }

        public void EliminarPlanCuentasNIIF(string pId, Usuario vUsuario)
        {

            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAPlanCuentas.EliminarPlanCuentasNIIF(pId, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasNIIFBusiness", "EliminarPlanCuentasNIIF", ex);
            }
        }

        public PlanCuentasNIIF ConsultarPlanCuentasNIIF(string pId, Usuario vUsuario)
        {
            try
            {                
                return DAPlanCuentas.ConsultarPlanCuentasNIIF(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasNIIFBusiness", "ConsultarPlanCuentasNIIF", ex);
                return null;
            }
        }
        

        public List<PlanCuentasNIIF> ListarPlanCuentasNIIF(PlanCuentasNIIF pPlanCuentas_NIIF, Usuario vUsuario)
        {
            try
            {
                return DAPlanCuentas.ListarPlanCuentasNIIF(pPlanCuentas_NIIF, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasNIIFBusiness", "ListarPlanCuentasNIIF", ex);
                return null;
            }
        }


        public void GenerarHomologacionMovimientos(ref string pError, DateTime pFechaIni, DateTime pFechaFin, Int64 pTipoComp, Usuario vUsuario)
        {
            DAPlanCuentas.GenerarHomologacionMovimientos(ref pError, pFechaIni, pFechaFin, pTipoComp, vUsuario);
        }


        public List<PlanCuentasNIIF> ListarReporteComparativoNIIF(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAPlanCuentas.ListarReporteComparativoNIIF(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasNIIFBusiness", "ListarReporteComparativoNIIF", ex);
                return null;
            }
        }

        public List<PlanCtasHomologacionNIF> ListarCuentasHomologadas(string pFiltro, string pOpcion, Usuario vUsuario)
        {
            try
            {
                return DAPlanCuentas.ListarCuentasHomologadas(pFiltro, pOpcion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasNIIFBusiness", "ListarCuentasHomologadas", ex);
                return null;
            }
        }
        
    }
}
