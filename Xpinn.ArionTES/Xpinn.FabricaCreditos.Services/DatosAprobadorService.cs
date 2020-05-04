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
    public class DatosAprobadorService
    {
        private DatosAprobadorBusiness BOAprobador;
        private ExcepcionBusiness BOExcepcion; 

        /// <summary>
        /// Constructor del servicio para Caja
        /// </summary>
        public DatosAprobadorService()
        {
            BOAprobador = new DatosAprobadorBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "10101"; } }

        /// <summary>
        /// Obtiene la lista de aprobadores
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public List<DatosAprobador> ListarDatosAprobador(DatosAprobador pDatos, Usuario pUsuario)
        {
            try
            {
                return BOAprobador.ListarDatosAprobador(pDatos, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosAprobadorService", "ListarDatosAprobador", ex);
                return null;
            }
        }
    }
}
