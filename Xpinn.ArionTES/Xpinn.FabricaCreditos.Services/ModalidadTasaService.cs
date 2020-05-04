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
    /// Servicio para ModalidadTasas
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ModalidadTasaService
    {
        private ModalidadTasaBusiness DAModalidadTasa;
        private ExcepcionBusiness BOExcepcion;
        public Int32 codusuario;
        /// <summary>
        /// Constructor del servicio para Caja
        /// </summary>
        public ModalidadTasaService()
        {
            DAModalidadTasa = new ModalidadTasaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "10101"; } }



        public List<ModalidadTasa> ListarModalidadTasa(String pIdCodLinea, Usuario pUsuario)
        {
            try
            {
                return DAModalidadTasa.ListarModalidadTasa(pIdCodLinea, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ModalidadTasaBusiness", "ListarModalidadTasa", ex);
                return null;
            }
        }
             
    }
}
