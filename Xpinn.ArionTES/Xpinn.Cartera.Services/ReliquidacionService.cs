using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Business;
using System.Web;


namespace Xpinn.Cartera.Services
{
    /// <summary>
    /// Servicio para Cajero
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ReliquidacionService
    {
        private ReliquidacionBusiness BOReliquidacion;
        private ReliquidacionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CierreHistorio
        /// </summary>
        public ReliquidacionService()
        {
            BOReliquidacion = new ReliquidacionBusiness();
            BOExcepcion = new ReliquidacionBusiness();
        }

        public string CodigoPrograma { get { return "60101"; } }

        /// <summary>
        /// Método para listado de créditos a reliquidar
        /// </summary>
        /// <param name="pusuario"></param>
        /// <param name="sfiltro"></param>
        /// <returns></returns>
        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarCredito(Usuario pusuario, string sfiltro)
        {
            try
            {
                return BOReliquidacion.ListarCredito(pusuario, sfiltro);
            }
            catch
            { return null; }
        }


        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarCreditoss(Usuario pusuario, string sfiltro)
        {
            try
            {
                return BOReliquidacion.ListarCreditoss(pusuario, sfiltro);
            }
            catch
            { return null; }
        }


        public Reliquidacion CrearReliquidacion(Reliquidacion pReliquidacion, ref string pError, Usuario pUsuario)
        {
            try
            {
                return BOReliquidacion.CrearReliquidacion(pReliquidacion, ref pError, pUsuario);
            }
            catch
            { return null; }
        }
        public Xpinn.FabricaCreditos.Entities.Credito CreditoReliquidado(string pNumeroRadicacion, Usuario pusuario)
        {
            try
            {
                return BOReliquidacion.CreditoReliquidado(pNumeroRadicacion, pusuario);
            }
            catch
            { return null; }
        }

    }

}
