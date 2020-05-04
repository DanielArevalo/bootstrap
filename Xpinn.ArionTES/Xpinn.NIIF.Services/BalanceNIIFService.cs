using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.NIIF.Business;
using Xpinn.NIIF.Entities;
using Xpinn.Util;
using System.Data;
using System.ServiceModel;
using System.IO;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.NIIF.Services
{
    public class BalanceNIIFService
    {

        private BalanceNIIFBusiness BOBalance;
        private ExcepcionBusiness BOExcepcion;

        public BalanceNIIFService()
        {
            BOBalance = new BalanceNIIFBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "210106"; } }
        public string CodigoProgramaoriginal { get { return "210101"; } }
        public string CodigoProgramaReporteNiif { get { return "210107"; } }
        public string CodigoProgramaBalanceNiif { get { return "210109"; } }
        public string CodigoProgramaCierreNiif  { get { return "210110"; } }
        public string CodigoProgramaCompNiif { get { return "210118"; } }
        public string CodigoProgramaCompTerNiif { get { return "210120"; } }

        public void CrearBalanceNIIF(ref string pError, DateTime pFechaCorte, List<BalanceNIIF> lstBalance, Usuario vUsuario)
        {
            try
            {
                BOBalance.CrearBalanceNIIF(ref pError, pFechaCorte, lstBalance, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFService", "CrearBalanceNIIF", ex);
            }

        }

        public string VerificarComprobantesYCuentasNIIF(DateTime fechaCorte, Usuario pUsuario)
        {
            try
            {
                return BOBalance.VerificarComprobantesYCuentasNIIF(fechaCorte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFServices", "VerificarComprobantesYCuentasNIIF", ex);
                return null;
            }
        }


        public List<PlanCuentasNIIF> CargarArchivo(DateTime pfecha, Stream pstream, ref string perror, ref List<PlanCuentasNIIF> plstErrores, Usuario pUsuario)
        {
            return BOBalance.CargarArchivo(pfecha, pstream,ref perror, ref plstErrores, pUsuario);
        }

        public BalanceNIIF ModificarBalanceNIIF(BalanceNIIF pBalanceNIIF, Usuario vUsuario)
        {
            try
            {
               return  BOBalance.ModificarBalanceNIIF(pBalanceNIIF, vUsuario);                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFService", "ModificarBalanceNIIF", ex);
                return null;
            }
        }



        public void EliminarBalance_NIIF(DateTime pFecha, Usuario vUsuario)
        {

            try
            {                
                BOBalance.EliminarBalance_NIIF(pFecha, vUsuario);
                               
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFService", "EliminarBalance_NIIF", ex);
            }

        }


        public BalanceNIIF ConsultarBalanceNIIF(BalanceNIIF pEntidad, Usuario vUsuario)
        {
            try
            {
                return  BOBalance.ConsultarBalanceNIIF(pEntidad, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFService", "ConsultarBalanceNIIF", ex);
                return null;
            }
        }
       

        public List<BalanceNIIF> ListarBalance_NIIF(DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                return BOBalance.ListarBalance_NIIF(pFecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFService", "ListarBalance_NIIF", ex);
                return null;
            }
        }


        public Boolean GenerarBalance_NIIF(DateTime pFecha, ref int opcion, ref string pError, Usuario vUsuario)
        {
            try
            {
               return BOBalance.GenerarBalance_NIIF(pFecha, ref opcion,ref pError, vUsuario);                  
            }
            catch (Exception ex)
            {
                return false;
                //BOExcepcion.Throw("BalanceNIIFService", "GenerarBalance_NIIF", ex);
            }
        }



        public Boolean ReclasificarBalanceNIIF(string pCodOrigen, string pCodDestino, Decimal pValor, int pTipoAjuste, string pObservacion, DateTime pfechafiltro, Int64 pCentroCosto, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOBalance.ReclasificarBalanceNIIF(pCodOrigen, pCodDestino, pValor, pTipoAjuste, pObservacion, pfechafiltro, pCentroCosto, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFService", "ReclasificarBalanceNIIF", ex);
                return false;
            }
        }

        public Boolean ReclasificarBalancesNIIF(List<BalanceNIIF> lstAjustes, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOBalance.ReclasificarBalancesNIIF(lstAjustes, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFService", "ReclasificarBalanceNIIF", ex);
                return false;
            }
        }


        public List<PlanCuentasNIIF> ListaPlan_Cuentas(PlanCuentasNIIF pPlanCuenta, Usuario vUsuario)
        {

            try
            {
                return BOBalance.ListaPlan_Cuentas(pPlanCuenta, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFService", "ListarPlanCuentas_NIIF", ex);
                return null;
            }
        }


        public void ModificarFechaNIIF(DateTime pfechafiltro, int tipo, Usuario vUsuario)
        {
            try
            {
                BOBalance.ModificarFechaNIIF(pfechafiltro,tipo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFService", "ModificarFechaNIIF", ex);
            }
        }



        public void EliminarFechaBalanceGeneradoNIIF(DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                BOBalance.EliminarFechaBalanceGeneradoNIIF(pFecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFService", "EliminarFechaBalanceGeneradoNIIF", ex);
            }
        }

        public void GenerarPlanCuentasNIIF(List<PlanCuentasNIIF> pLstPlan, ref List<PlanCuentasNIIF> lstNoregistrados, Usuario pUsuario)
        {
            try
            {
                BOBalance.GenerarPlanCuentasNIIF(pLstPlan,ref lstNoregistrados, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFService", "GenerarPlanCuentasNIIF", ex);
            }
        }

        public List<PlanCuentasNIIF> ListarPlanCuentasNIIF(PlanCuentasNIIF pPlanCuentas, Usuario pUsuario, String pfiltro)
        {
            try
            {
                return BOBalance.ListarPlanCuentasNIIF(pPlanCuentas, pUsuario, pfiltro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFService", "ListarPlanCuentasAuxiliares", ex);
                return null;
            }
        }

        public List<PlanCuentasNIIF> ListarPlanCuentasLocal(PlanCuentasNIIF pPlanCuentas, Usuario pUsuario, String pfiltro)
        {
            try
            {
                return BOBalance.ListarPlanCuentasLocal(pPlanCuentas, pUsuario, pfiltro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFService", "ListarPlanCuentasLocal", ex);
                return null;
            }
        }

        public PlanCuentasNIIF Eliminarniff(Int64 Id, Usuario vUsuario)
        {
            try
            {                
                BOBalance.Eliminarniff(Id, vUsuario);                               
                return null;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFService", "CrearBalanceNIIF", ex);
                return null;
            }

        }

        public List<BalanceNIIF> listarbalancereporteXBLR(BalanceNIIF entidad, string pBalanceNiif, Usuario vUsuario)
        {
            try
            {
                return BOBalance.listarbalancereporteXBLR(entidad, pBalanceNiif, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFService", "ListarBalance_NIIF", ex);
                return null;
            }
        }


        public List<BalanceNIIF> listarbalancereporteXBLRConceros(BalanceNIIF entidad, string pBalanceNiif, Usuario vUsuario)
        {
            try
            {
                return BOBalance.listarbalancereporteXBLRConceros(entidad, pBalanceNiif, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFService", "ListarBalance_NIIF", ex);
                return null;
            }
        }

        public List<BalanceNIIF> Consultardatosdecierea(Usuario vUsuario)
        {
            try
            {
                return BOBalance.Consultardatosdecierea(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFService", "ListarBalance_NIIF", ex);
                return null;
            }
        }

        public List<BalanceNIIF> ListarBalance(BalanceNIIF pDatos, Usuario pUsuario)
        {
            return BOBalance.ListarBalance(pDatos, pUsuario);
        }

        public List<BalanceNIIF> ListarFechaCorte(Usuario pUsuario)
        {
            try
            {
                return BOBalance.ListarFechaCierre(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFService", "ListarFechaCorte", ex);
                return null;
            }
        }

        public List<BalanceNIIF> ListarFechasParaCierre(Usuario pUsuario)
        {
            try
            {
                return BOBalance.ListarFechasParaCierre(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFService", "ListarFechasParaCierre", ex);
                return null;
            }
        }

        public BalanceNIIF CrearCierreMensual(BalanceNIIF pcierre, Usuario pUsuario)
        {
            try
            {
                return BOBalance.CrearCierreMensual(pcierre, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFService", "CrearCierreMensual", ex);
                return null;
            }
        }

        #region METODOS DE BALANCE DE COMPROBACION

        public Xpinn.Contabilidad.Entities.BalancePrueba ConsultarBalanceMes13(Xpinn.Contabilidad.Entities.BalancePrueba pDatos, Usuario pUsuario)
        {
            return BOBalance.ConsultarBalanceMes13(pDatos, pUsuario);
        }

        public List<Xpinn.Contabilidad.Entities.BalancePrueba> ListarBalanceComprobacionNiif(Xpinn.Contabilidad.Entities.BalancePrueba pDatos, ref Double TotDeb, ref Double TotCre, Usuario pUsuario)
        {
            return BOBalance.ListarBalanceComprobacionNiif(pDatos, ref  TotDeb, ref TotCre, pUsuario);
        }


        #endregion

        public BalanceNIIF ConsultarBalanceMes13(BalanceNIIF pDatos, Usuario pUsuario)
        {
            try
            {
                return BOBalance.ConsultarBalanceMes13(pDatos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFService", "ConsultarBalanceMes13", ex);
                return null;
            }
        }

        public List<BalancePrueba> ListarBalanceComprobacionTerNiif(BalancePrueba pEntidad, Usuario pUsuario)
        {
            try
            {
                return BOBalance.ListarBalanceComprobacionTerNiif(pEntidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFBusiness", "ListarBalanceComprobacionTerNiif", ex);
                return null;
            }
        }


    }

}
