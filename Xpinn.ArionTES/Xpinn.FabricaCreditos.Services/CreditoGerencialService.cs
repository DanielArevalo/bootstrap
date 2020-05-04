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
    public class CreditoGerencialService
    {
        private CreditoGerencialBusiness BOGerencial;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Vehiculos
        /// </summary>
        public CreditoGerencialService()
        {
            BOGerencial = new CreditoGerencialBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100156"; } } 


        public List<Credito> ListarCreditoGerencial(Usuario pUsuario, String filtro)
        {
            try
            {
                return BOGerencial.ListarCreditoGerencial(pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoGerencialService", "ListarCreditoGerencial", ex);
                return null;
            }
        }



        public List<Atributos> ListarAtributosXlinea(string pLinea, Usuario pUsuario)
        {
            try
            {
                return BOGerencial.ListarAtributosXlinea(pLinea, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoGerencialService", "ListarAtributosXlinea", ex);
                return null;
            }
        }

        public LineasCredito ConsultarDatosXatributos(LineasCredito pLineas, Usuario pUsuario)
        {
            try
            {
                return BOGerencial.ConsultarDatosXatributos(pLineas, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoGerencialService", "ConsultarDatosXatributos", ex);
                return null;
            }
        }
       
    }
}