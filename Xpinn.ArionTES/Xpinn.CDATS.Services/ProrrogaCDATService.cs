using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using Xpinn.Util;
using Xpinn.CDATS.Entities;
using Xpinn.CDATS.Business;


namespace Xpinn.CDATS.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ProrrogaCDATService
    {
        ProrrogaCDATBusiness BOProrro;
        ExcepcionBusiness BOException;

        public ProrrogaCDATService()
        {
            BOProrro = new ProrrogaCDATBusiness();
            BOException = new ExcepcionBusiness();
        }

        public string CodigoProgramaPRO { get { return "220303"; } }
        public string CodigoProgramasim { get { return "220315"; } }

        public Cdat ModificarCDATProrroga(Cdat pCdat, Usuario vUsuario)
        {
            try
            {
                return BOProrro.ModificarCDATProrroga(pCdat, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("ProrrogaCDATService", "ModificarCDATProrroga", ex);
                return null;
            }
        }



        public ProrrogaCDAT CrearCDATProrroga(ProrrogaCDAT pProrro, Usuario vUsuario)
        {
            try
            {
                return BOProrro.CrearCDATProrroga(pProrro, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("ProrrogaCDATService", "CrearCDATProrroga", ex);
                return null;
            }
        }


        public ProrrogaCDAT ModificarProrroga_CDAT(ProrrogaCDAT pProrro, Usuario vUsuario)
        {
            try
            {
                return BOProrro.ModificarProrroga_CDAT(pProrro, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("ProrrogaCDATService", "ModificarProrroga_CDAT", ex);
                return null;
            }
        }



        public ProrrogaCDAT ConsultarCDATProrroga(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOProrro.ConsultarCDATProrroga(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("ProrrogaCDATService", "ConsultarCDATProrroga", ex);
                return null;
            }
        }

        public Boolean SolicitarRenovacionCdat(List<SolicitudRenovacion> lstRenovacion, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOProrro.SolicitarRenovacionCdat(lstRenovacion, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("ProrrogaCDATService", "SolicitarRenovacionCdat", ex);
                return false;
            }
        }

    }
}
