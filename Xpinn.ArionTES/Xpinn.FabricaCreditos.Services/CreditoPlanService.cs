using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Services
{   
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CreditoPlanService
    {
        private CreditoPlanBusiness BOCreditoPlan;
        private ExcepcionBusiness BOExcepcion;
                /// <summary>
        /// Constructor del servicio para Credito
        /// </summary>
        public CreditoPlanService()
        {
            BOCreditoPlan = new CreditoPlanBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100141"; } }
        public string CodigoScoringCreditos { get { return "160101"; } }

        /// <summary>
        /// Servicio para obtener lista de Creditos a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<CreditoPlan> ListarCredito(CreditoPlan pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOCreditoPlan.ListarCreditoPlan(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoPlanService", "ListarCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener Credito
        /// </summary>
        /// <param name="pId">identificador de Credito</param>
        /// <returns>Entidad Credito</returns>
        public CreditoPlan ConsultarCredito(Int64 pId, Boolean btasa, Usuario pUsuario)
        {
            try
            {
                return BOCreditoPlan.ConsultarCredito(pId, btasa, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarCredito", ex);
                return null;
            }
        }


        public DatosSolicitud ConsultarProveedorXCredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCreditoPlan.ConsultarProveedorXCredito(pId,  pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarProveedorXCredito", ex);
                return null;
            }
        }



        /// <summary>
        /// Servicio para Liquidar Crédito
        /// </summary>
        /// <param name="pEntity">Entidad Liquidar</param>
        /// <returns>Entidad creada</returns>
        public CreditoPlan Liquidar(CreditoPlan pCredito, Usuario pUsuario)
        {
            try
            {
                return BOCreditoPlan.Liquidar(pCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoPlan", "Liquidar", ex);
                return null;
            }
        }

        public CreditoEntity LiquidarWS(CreditoPlan pCredito, int pDestino, bool pManejaTransferencia, decimal pVr_compra, decimal pVr_beneficio, decimal pVr_Mercado, int pCodProceso, Usuario pUsuario, bool GeneraDesembolso = false)
        {
            try
            {
                return BOCreditoPlan.LiquidarWS(pCredito, pManejaTransferencia, pVr_compra, pVr_beneficio, pVr_Mercado, pCodProceso, pUsuario, GeneraDesembolso);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoPlan", "Liquidar", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de Creditos a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<CreditoPlan> ListarScoringCredito(CreditoPlan pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOCreditoPlan.ListarScoringCredito(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoPlanService", "ListarCredito", ex);
                return null;
            }
        }


    }
}
