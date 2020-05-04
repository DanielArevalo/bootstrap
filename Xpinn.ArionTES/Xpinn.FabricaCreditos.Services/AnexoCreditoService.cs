using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.Data;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicio para Aprobador
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class AnexoCreditoService
    {
        private AnexoCreditoBusiness BOAnexo;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Caja
        /// </summary>
        public AnexoCreditoService()
        {
            BOAnexo = new AnexoCreditoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "10101"; } }

        /// <summary>
        /// Obtiene la lista de creditos solicitados
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de creditos obtenidos</returns>
        public List<AnexoCredito> ListarAnexos(AnexoCredito pAnexo, Usuario pUsuario)
        {
            try
            {
                return BOAnexo.ListarAnexos(pAnexo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AnexoCreditoService", "ListarAnexos", ex);
                return null;
            }
        }
    }
}
