using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class EntregaChequesServices
    {

        private EntregaChequesBusiness BOEntregaCheques;
        private ExcepcionBusiness BOExcepcion;

        public int Codigoaporte;
        /// <summary>
        /// Constructor del servicio para Aporte
        /// </summary>
        public EntregaChequesServices()
        {
            BOEntregaCheques = new EntregaChequesBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40502"; } }


        public EntregaCheques CrearEntregaCheque(EntregaCheques pEntregaCheques, Usuario vUsuario)
        {
            try
            {
                return BOEntregaCheques.CrearEntregaCheque(pEntregaCheques, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EntregaChequesServices", "CrearEntregaCheque", ex);
                return null;
            }
        }

        public List<EntregaCheques> ListarEntregaCheques(EntregaCheques pEntregaCheques, Usuario vUsuario)
        {
            try
            {
                return BOEntregaCheques.ListarEntregaCheques(pEntregaCheques, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EntregaChequesServices", "ListarEntregaCheques", ex);
                return null;
            }
        }

    }
}