using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Interfaces.Data;
using Xpinn.Interfaces.Entities;

namespace Xpinn.Interfaces.Business
{

    public class WorkManagementBusiness : GlobalBusiness
    {
        private WorkManagementData DAWorkManagementData;

        public WorkManagementBusiness()
        {
            DAWorkManagementData = new WorkManagementData();
        }

        public WorkManagement_Aud CrearWorkManagement_Aud(WorkManagement_Aud pWorkManagement_Aud, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pWorkManagement_Aud = DAWorkManagementData.CrearWorkManagement_Aud(pWorkManagement_Aud, pusuario);

                    ts.Complete();

                }

                return pWorkManagement_Aud;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("WorkManagementBusiness", "CrearWorkManagement_Aud", ex);
                return null;
            }
        }

        public WorkFlowCreditos ConsultarWorkFlowCreditoPorNumeroRadicacion(int numeroRadicacion, Usuario pusuario)
        {
            try
            {
                return DAWorkManagementData.ConsultarWorkFlowCreditoPorNumeroRadicacion(numeroRadicacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("WorkManagementBusiness", "ConsultarWorkFlowCreditoPorNumeroRadicacion", ex);
                return null;
            }
        }

        public WorkFlowCreditos CrearWorkFlowCreditos(WorkFlowCreditos pWorkFlowCreditos, Usuario pusuario)
        {
            try
            {
                return DAWorkManagementData.CrearWorkFlowCreditos(pWorkFlowCreditos, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("WorkManagementBusiness", "CrearWorkFlowCreditos", ex);
                return null;
            }
        }

        public bool MarcarDocumentosGeneradosDeUnWorkFlow(WorkFlowCreditos workFlowCredito, Usuario usuario)
        {
            try
            {
                return DAWorkManagementData.MarcarDocumentosGeneradosDeUnWorkFlow(workFlowCredito, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("WorkManagementBusiness", "MarcarDocumentosGeneradosDeUnWorkFlow", ex);
                return false;
            }
        }

        public List<WorkFlowCreditos> ConsultarGirosRealizadosPertenecientesAUnWorkFlowCreditosSegunCodigoOperacion(long codigoOperacion, Usuario usuario)
        {
            try
            {
                return DAWorkManagementData.ConsultarGirosRealizadosPertenecientesAUnWorkFlowCreditosSegunCodigoOperacion(codigoOperacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("WorkManagementBusiness", "ConsultarGirosRealizadosPertenecientesAUnWorkFlowCreditosSegunCodigoOperacion", ex);
                return null;
            }
        }

        public WorkFlowCruceCuentas CrearWorkFlowCruceCuentas(WorkFlowCruceCuentas workFlowCruceCuentas, Usuario usuario)
        {
            try
            {
                return DAWorkManagementData.CrearWorkFlowCruceCuentas(workFlowCruceCuentas, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("WorkManagementBusiness", "CrearWorkFlowCruceCuentas", ex);
                return null;
            }
        }

        public List<WorkFlowCruceCuentas> ConsultarGirosRealizadosPertenecientesAUnWorkFlowCruceCuentasSegunCodigoOperacion(long codigoOperacion, Usuario usuario)
        {
            try
            {
                return DAWorkManagementData.ConsultarGirosRealizadosPertenecientesAUnWorkFlowCruceCuentasSegunCodigoOperacion(codigoOperacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("WorkManagementBusiness", "ConsultarGirosRealizadosPertenecientesAUnWorkFlowCruceCuentasSegunCodigoOperacion", ex);
                return null;
            }
        }

        public WorkFlowPagoProveedores CrearWorkFlowPagoProveedores(WorkFlowPagoProveedores workFlowPagoProveedores, Usuario usuario)
        {
            try
            {
                return DAWorkManagementData.CrearWorkFlowPagoProveedores(workFlowPagoProveedores, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("WorkManagementBusiness", "CrearWorkFlowPagoProveedores", ex);
                return null;
            }
        }

        public bool MarcarWorkFlowPagoProveedoresComoPagado(string radicado, Usuario usuario)
        {
            try
            {
                return DAWorkManagementData.MarcarWorkFlowPagoProveedoresComoPagado(radicado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("WorkManagementBusiness", "MarcarWorkFlowPagoProveedoresComoPagado", ex);
                return false;
            }
        }

        public List<WorkFlowPagoProveedores> ConsultarNumerosFacturasPendientesPorPagarParaEsteBeneficiarioPagoProveedores(long codigoBeneficiario, Usuario usuario)
        {
            try
            {
                return DAWorkManagementData.ConsultarNumerosFacturasPendientesPorPagarParaEsteBeneficiarioPagoProveedores(codigoBeneficiario, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("WorkManagementBusiness", "ConsultarNumerosFacturasPendientesPorPagarParaEsteBeneficiarioPagoProveedores", ex);
                return null;
            }
        }
    }
}