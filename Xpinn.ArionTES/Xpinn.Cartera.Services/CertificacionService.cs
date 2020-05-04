using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Business;
using System.Web;
using Xpinn.Util;


namespace Xpinn.Cartera.Services
{
    /// <summary>
    /// Servicio para Cajero
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CertificacionService
    {
        private RefinanciacionBusiness BORefinanciacion;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CierreHistorio
        /// </summary>
        public CertificacionService()
        {
            BORefinanciacion = new RefinanciacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoProgramaCertificacionAsociados { get { return "60114"; } }
        public string CodigoPrograma { get { return "60109"; } }

        /// <summary>
        /// Método para listado de créditos a refinanciar
        /// </summary>
        /// <param name="pusuario"></param>
        /// <param name="sfiltro"></param>
        /// <returns></returns>
        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarCredito(DateTime pFecha, String filtro, Usuario pUsuario)
        {
            try
            {
                return BORefinanciacion.ListarCredito(pFecha, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RefinanciacionBusiness", "ListarCredito", ex);
                return null;
            }
        }
    }

}
