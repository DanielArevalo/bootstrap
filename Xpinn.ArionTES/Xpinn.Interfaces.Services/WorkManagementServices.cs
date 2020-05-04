using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Interfaces.Entities;
using Xpinn.Interfaces.Business;

namespace Xpinn.Interfaces.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class WorkManagementServices
    {
        private WorkManagementBusiness DAWorkManagementBusiness;
        private ExcepcionBusiness BOExcepcion;

        public WorkManagementServices()
        {
            DAWorkManagementBusiness = new WorkManagementBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return ""; } }

        public WorkManagement_Aud CrearWorkManagement_Aud(WorkManagement_Aud pWorkManagement_Aud, Usuario pusuario)
        {
            try
            {
                pWorkManagement_Aud = DAWorkManagementBusiness.CrearWorkManagement_Aud(pWorkManagement_Aud, pusuario);
                return pWorkManagement_Aud;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("WorkManagementServices", "CrearWorkManagement_Aud", ex);
                return null;
            }
        }

        public WorkFlowCreditos ConsultarWorkFlowCreditoPorNumeroRadicacion(int numeroRadicacion, Usuario pusuario)
        {
            try
            {
                return DAWorkManagementBusiness.ConsultarWorkFlowCreditoPorNumeroRadicacion(numeroRadicacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("WorkManagementServices", "ConsultarWorkFlowCreditoPorNumeroRadicacion", ex);
                return null;
            }
        }

        public WorkFlowCreditos CrearWorkFlowCreditos(WorkFlowCreditos pWorkFlowCreditos, Usuario pusuario)
        {
            try
            {
                return DAWorkManagementBusiness.CrearWorkFlowCreditos(pWorkFlowCreditos, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("WorkManagementServices", "CrearWorkFlowCreditos", ex);
                return null;
            }
        }

        public bool MarcarDocumentosGeneradosDeUnWorkFlow(WorkFlowCreditos workFlowCredito, Usuario usuario)
        {
            try
            {
                return DAWorkManagementBusiness.MarcarDocumentosGeneradosDeUnWorkFlow(workFlowCredito, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("WorkManagementServices", "MarcarDocumentosGeneradosDeUnWorkFlow", ex);
                return false;
            }
        }

        public List<WorkFlowCreditos> ConsultarGirosRealizadosPertenecientesAUnWorkFlowCreditosSegunCodigoOperacion(long codigoOperacion, Usuario usuario)
        {
            try
            {
                return DAWorkManagementBusiness.ConsultarGirosRealizadosPertenecientesAUnWorkFlowCreditosSegunCodigoOperacion(codigoOperacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("WorkManagementServices", "ConsultarGirosRealizadosPertenecientesAUnWorkFlowCreditosSegunCodigoOperacion", ex);
                return null;
            }
        }

        public WorkFlowCruceCuentas CrearWorkFlowCruceCuentas(WorkFlowCruceCuentas workFlowCruceCuentas, Usuario usuario)
        {
            try
            {
                return DAWorkManagementBusiness.CrearWorkFlowCruceCuentas(workFlowCruceCuentas, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("WorkManagementServices", "CrearWorkFlowCruceCuentas", ex);
                return null;
            }
        }

        public List<WorkFlowCruceCuentas> ConsultarGirosRealizadosPertenecientesAUnWorkFlowCruceCuentasSegunCodigoOperacion(long codigoOperacion, Usuario usuario)
        {
            try
            {
                return DAWorkManagementBusiness.ConsultarGirosRealizadosPertenecientesAUnWorkFlowCruceCuentasSegunCodigoOperacion(codigoOperacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("WorkManagementServices", "ConsultarGirosRealizadosPertenecientesAUnWorkFlowCruceCuentasSegunCodigoOperacion", ex);
                return null;
            }
        }

        public WorkFlowPagoProveedores CrearWorkFlowPagoProveedores(WorkFlowPagoProveedores workFlowPagoProveedores, Usuario usuario)
        {
            try
            {
                return DAWorkManagementBusiness.CrearWorkFlowPagoProveedores(workFlowPagoProveedores, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("WorkManagementServices", "CrearWorkFlowPagoProveedores", ex);
                return null;
            }
        }

        public bool MarcarWorkFlowPagoProveedoresComoPagado(string radicado, Usuario usuario)
        {
            try
            {
                return DAWorkManagementBusiness.MarcarWorkFlowPagoProveedoresComoPagado(radicado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("WorkManagementServices", "MarcarWorkFlowPagoProveedoresComoPagado", ex);
                return false;
            }
        }

        public List<WorkFlowPagoProveedores> ConsultarNumerosFacturasPendientesPorPagarParaEsteBeneficiarioPagoProveedores(long codigoBeneficiario, Usuario usuario)
        {
            try
            {
                return DAWorkManagementBusiness.ConsultarNumerosFacturasPendientesPorPagarParaEsteBeneficiarioPagoProveedores(codigoBeneficiario, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("WorkManagementServices", "ConsultarNumerosFacturasPendientesPorPagarParaEsteBeneficiarioPagoProveedores", ex);
                return null;
            }
        }
    }
}