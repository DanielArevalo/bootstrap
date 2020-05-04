using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;
using Xpinn.Util;
using System.Data;

namespace Xpinn.Asesores.Services
{
    /// <summary>
    /// Motivos de generación de extracto
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class MotivoGeneracionExtractoService
    {
        private MotivoGeneracionExtractoBusiness BOMotivoGeneracionExtracto;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Caja
        /// </summary>
        public MotivoGeneracionExtractoService()
        {
            BOMotivoGeneracionExtracto = new MotivoGeneracionExtractoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<MotivoGeneracionExtracto> ListarMotivoGeneracionExtractos(MotivoGeneracionExtracto MotivoGeneracionExtracto, Usuario pUsuario)
        {
            try
            {
                return BOMotivoGeneracionExtracto.ListarMotivoGeneracionExtractos(MotivoGeneracionExtracto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoGeneracionExtractoService", "ListarMotivoGeneracionExtractos", ex);
                return null;
            }
        }
    }
}