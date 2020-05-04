using System;
using System.Collections.Generic;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Contabilidad.Business;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class InterfazNominaService
    {
        private InterfazNominaBusiness BOInterfazNomina;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para InterfazNomina
        /// </summary>
        public InterfazNominaService()
        {
            BOInterfazNomina = new InterfazNominaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30802"; } }
        public string CodigoProgramaCredito { get { return "30803"; } }
        public string CodigoProgramaProvision { get { return "30804"; } }
        public string CodigoProgramaPrimas { get { return "30808"; } }


        public List<InterfazNomina> ListarNomina(Int64 piden_periodo, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOInterfazNomina.ListarNomina(piden_periodo, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaService", "ListarNomina", ex);
                return null;
            }
        }

        public List<InterfazNomina> ListarNomina(Int64 piden_periodo, String pidentificacion, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOInterfazNomina.ListarNomina(piden_periodo, pidentificacion, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaService", "ListarNomina", ex);
                return null;
            }
        }

        public List<InterfazNomina> ListarNominaConsolidado(Int64 piden_periodo, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOInterfazNomina.ListarNominaConsolidado(piden_periodo, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaService", "ListarNominaConsolidado", ex);
                return null;
            }
        }

        public List<InterfazNomina> ListarPeriodos(Usuario pUsuario)
        {
            try
            {
                return BOInterfazNomina.ListarPeriodos(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaService", "ListarPeriodos", ex);
                return null;
            }
        }


        public List<InterfazNomina> ListarPeriodos(string pTipo, Usuario pUsuario)
        {
            try
            {
                return BOInterfazNomina.ListarPeriodos(pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaService", "ListarPeriodos", ex);
                return null;
            }
        }

        public List<InterfazNomina> ListarPeriodosPrima(Usuario pUsuario)
        {
            try
            {
                return BOInterfazNomina.ListarPeriodosPrima(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaService", "ListarPeriodosPrima", ex);
                return null;
            }
        }

        public List<InterfazNomina> ListarCreditos(Int64 piden_periodo, Usuario pUsuario)
        {
            try
            {
                return BOInterfazNomina.ListarCreditos(piden_periodo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaService", "ListarCreditos", ex);
                return null;
            }
        }

        public List<InterfazNomina> ListarCreditosDelEmpleado(string pIdentificacion, string pConcepto, Usuario pUsuario)
        {
            try
            {
                return BOInterfazNomina.ListarCreditosDelEmpleado(pIdentificacion, pConcepto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaService", "ListarCreditosDelEmpleado", ex);
                return null;
            }
        }

        public List<InterfazNomina> ListarCuentasConcepto(string pConcepto, Usuario pUsuario)
        {
            try
            {
                return BOInterfazNomina.ListarCuentasConcepto(pConcepto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaService", "ListarCuentasConcepto", ex);
                return null;
            }
        }

        public Boolean AplicarCreditos(Int64 pPeriodo, DateTime fecha_aplicacion, List<InterfazNomina> lstCreditos, Usuario pUsuario, ref string Error, ref Int64 pCodOpe)
        {
            try
            {
                return BOInterfazNomina.AplicarCreditos(pPeriodo, fecha_aplicacion, lstCreditos, pUsuario, ref Error, ref pCodOpe);
            }
            catch (Exception ex)
            {
                Error += ex.Message;
                return false;
            }
        }

        public Boolean GenerarComprobantePorEmpleado(Int64 pPeriodo, DateTime fecha_aplicacion, List<InterfazNomina> lstCreditos, Int64 pCodProceso, string pFiltro, string pTipo, Usuario pUsuario, ref string Error, ref List<Xpinn.Tesoreria.Entities.Operacion> lstOperacion)
        {
            try
            {
                return BOInterfazNomina.GenerarComprobantePorEmpleado(pPeriodo, fecha_aplicacion, lstCreditos, pCodProceso, pFiltro, pTipo, pUsuario, ref Error, ref lstOperacion);
            }
            catch (Exception ex)
            {
                Error += ex.Message;
                return false;
            }
        }

        public Boolean GenerarComprobante(Int64 pPeriodo, DateTime fecha_aplicacion, Int64 pCodProceso, List<InterfazNomina> lstCreditos, Usuario pUsuario, ref string Error, ref Int64 pCodOpe)
        {
            try
            {
                return BOInterfazNomina.GenerarComprobante(pPeriodo, fecha_aplicacion, pCodProceso, lstCreditos, pUsuario, ref Error, ref pCodOpe);
            }
            catch (Exception ex)
            {
                Error += ex.Message;
                return false;
            }
        }

        public Int64? CodigoDelEmpleado(string pIdentificacion, Usuario pUsuario)
        {
            try
            {
                return BOInterfazNomina.CodigoDelEmpleado(pIdentificacion, pUsuario); 
            }
            catch 
            {
                return null;
            }
        }


        public List<InterfazNomina> ListarNominaProvision(Int64 pAño, Int64 pMes, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOInterfazNomina.ListarNominaProvision(pAño, pMes, pFiltro, pUsuario);
            }
            catch 
            {
                return null;
            }
        }

        public List<InterfazNomina> ListarPeriodosProvision(Usuario pUsuario)
        {
            try
            {
                return BOInterfazNomina.ListarPeriodosProvision(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaService", "ListarPeriodosProvision", ex);
                return null;
            }
        }

        public List<InterfazNomina> ListarSaludPension(Int64 piden_periodo, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOInterfazNomina.ListarSaludPension(piden_periodo, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaService", "ListarSaludPension", ex);
                return null;
            }
        }

        public Boolean GenerarComprobanteSaludPension(Int64 pPeriodo, DateTime fecha_aplicacion, Int64 pCodProceso, List<InterfazNomina> lstConceptos, Usuario pUsuario, ref string Error, ref Int64 pCodOpe)
        {
            try
            {
                return BOInterfazNomina.GenerarComprobanteSaludPension(pPeriodo, fecha_aplicacion, pCodProceso, lstConceptos, pUsuario, ref Error, ref pCodOpe);
            }
            catch (Exception ex)
            {
                Error += ex.Message;
                return false;
            }
        }

        public List<InterfazNomina> ListarCuentasContraConcepto(string pConcepto, Usuario pUsuario)
        {
            try
            {
                return BOInterfazNomina.ListarCuentasContraConcepto(pConcepto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaService", "ListarCuentasContraConcepto", ex);
                return null;
            }
        }

        public List<InterfazNomina> ListarPeriodosSaludPension(Usuario pUsuario)
        {
            try
            {
                return BOInterfazNomina.ListarPeriodosSaludPension(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaService", "ListarPeriodosSaludPension", ex);
                return null;
            }
        }


    }

}