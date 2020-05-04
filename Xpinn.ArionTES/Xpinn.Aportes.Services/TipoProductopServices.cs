using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Aportes.Business;
using Xpinn.Aportes.Entities;

namespace Xpinn.Aportes.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TipoProductoServices
    {
        private TipoProductoBusiness BOTipoProducto;
        private ExcepcionBusiness BOExcepcion;
        public int Codigoaporte;
        /// <summary>
        /// Constructor del servicio para Aporte
        /// </summary>
        public TipoProductoServices()
        {
            BOTipoProducto = new TipoProductoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "170202"; } }



        public List<TipoProducto> ListarTipoProducto(TipoProducto pTipoProducto, Usuario pUsuario)
        {
            try
            {
                return BOTipoProducto.ListarTipoProducto(pTipoProducto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoProductoServices", "TipoProducto", ex);
                return null;
            }
        }
        public List<TipoProducto> ListarTipoTran(TipoProducto pTipoProducto, Usuario pUsuario)
        {
            try
            {
                return BOTipoProducto.ListarTipoTran(pTipoProducto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoProductoServices", "ListarTipoTran", ex);
                return null;
            }
        }

    }
}