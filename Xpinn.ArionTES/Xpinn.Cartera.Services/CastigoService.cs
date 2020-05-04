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
    /// Servicio para Castigo
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CastigoService
    {
        private CastigoBusiness BOCastigo;
        private CastigoBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CierreHistorio
        /// </summary>
        public CastigoService()
        {
            BOCastigo = new CastigoBusiness();
            BOExcepcion = new CastigoBusiness();
        }

        public string CodigoPrograma { get { return "60104"; } }

        /// <summary>
        /// Método para listado de créditos a Castigo
        /// </summary>
        /// <param name="pusuario"></param>
        /// <param name="sfiltro"></param>
        /// <returns></returns>
        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarCredito(Usuario pusuario, string sfiltro)
        {
            try
            {
                return BOCastigo.ListarCredito(pusuario, sfiltro);
            }
            catch
            { 
                return null; 
            }
        }


        public Castigo CrearCastigo(Castigo pCastigo, Usuario pUsuario)
        {
            try
            {
                return BOCastigo.CrearCastigo(pCastigo, pUsuario);
            }
            catch 
            {
                return null;
            }
        }

    }

}
