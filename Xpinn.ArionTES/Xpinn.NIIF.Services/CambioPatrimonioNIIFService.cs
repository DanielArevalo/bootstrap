using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.NIIF.Business;
using Xpinn.NIIF.Entities;

namespace Xpinn.NIIF.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CambioPatrimonioNIIFService
    {
        private CambioPatrimonioNIIFBusiness BOCambioPatrimonio;
        private ExcepcionBusiness BOExcepcion;
        
        public CambioPatrimonioNIIFService()
        {
            BOCambioPatrimonio = new CambioPatrimonioNIIFBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }
        public string CodigoProgramaNIIF { get { return "210311"; } }


       public List<CambioPatrimonioNIIF> ListarEstadoCambioPatrimonio(CambioPatrimonioNIIF pEntidad, Usuario vUsuario, int pOpcion)
        {
            try
            {
                return BOCambioPatrimonio.ListarEstadoCambioPatrimonio(pEntidad, vUsuario, pOpcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioPatrimonioNIIFService", "ListarEstadoResultado", ex);
                return null;
            }
        }

        public List<CambioPatrimonioNIIF> ListarFecha(Usuario pUsuario)
        {
            try
            {
                return BOCambioPatrimonio.ListarFecha(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioPatrimonioNIIFService", "ListarFecha", ex);
                return null;
            }
        }       

    }
}