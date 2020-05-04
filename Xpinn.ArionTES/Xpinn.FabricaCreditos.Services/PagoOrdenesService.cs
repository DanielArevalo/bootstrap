using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Business;

namespace Xpinn.FabricaCreditos.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class PagoOrdenesService
    {

        private PagoOrdenesBusiness BOPagoOrdenes;
        private ExcepcionBusiness BOExcepcion;

        public PagoOrdenesService()
        {
            BOPagoOrdenes = new PagoOrdenesBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100162"; } }

        public bool CrearPagoOrdenes(List<PagoOrdenes> plstOrdenes, Xpinn.FabricaCreditos.Entities.Giro pGiro, ref string pError, Usuario pusuario)
        {
            try
            {
                return BOPagoOrdenes.CrearPagoOrdenes(plstOrdenes, pGiro, ref pError, pusuario);
            }
            catch 
            {
                return false;
            }
        }


        public PagoOrdenes ModificarPagoOrdenes(PagoOrdenes pPagoOrdenes, Usuario pusuario)
        {
            try
            {
                pPagoOrdenes = BOPagoOrdenes.ModificarPagoOrdenes(pPagoOrdenes, pusuario);
                return pPagoOrdenes;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagoOrdenesService", "ModificarPagoOrdenes", ex);
                return null;
            }
        }

  
        public List<PagoOrdenes> ConsultarPagoOrdenes(string pFiltro,Usuario pusuario)
        {
            try
            {
                return BOPagoOrdenes.ConsultarPagoOrdenes(pFiltro,pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagoOrdenesService", "ListarPagoOrdenes", ex);
                return null;
            }
        }

        public string TipoConexion(Usuario vUsuario)
        {
            return BOPagoOrdenes.TipoConexion(vUsuario);
        }


    }
}