using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Business;

namespace Xpinn.Ahorros.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ParametroGMFService
    {

        private ParametroGMFBusiness BOParametroGMF;
        private ExcepcionBusiness BOExcepcion;

        public ParametroGMFService()
        {
            BOParametroGMF = new ParametroGMFBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "220111"; } }

        public ParametroGMF CrearParametroGMF(ParametroGMF pParametroGMF, Usuario pusuario)
        {
            try
            {
                pParametroGMF = BOParametroGMF.CrearParametroGMF(pParametroGMF, pusuario);
                return pParametroGMF;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroGMFService", "CrearParametroGMF", ex);
                return null;
            }
        }


        public ParametroGMF ModificarParametroGMF(Int64 idobjeto,ParametroGMF pParametroGMF, Usuario pusuario)
        {
            try
            {
                pParametroGMF = BOParametroGMF.ModificarParametroGMF(idobjeto,pParametroGMF, pusuario);
                return pParametroGMF;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroGMFService", "ModificarParametroGMF", ex);
                return null;
            }
        }


        public void EliminarParametroGMF(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOParametroGMF.EliminarParametroGMF(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroGMFService", "EliminarParametroGMF", ex);
            }
        }


        public ParametroGMF ConsultarParametroGMF(Int64 pId, Usuario pusuario)
        {
            try
            {
                ParametroGMF ParametroGMF = new ParametroGMF();
                ParametroGMF = BOParametroGMF.ConsultarParametroGMF(pId, pusuario);
                return ParametroGMF;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroGMFService", "ConsultarParametroGMF", ex);
                return null;
            }
        }


        public List<ParametroGMF> ListarParametroGMF(string filtro ,ParametroGMF pParametroGMF, Usuario pusuario)
        {
            try
            {
                return BOParametroGMF.ListarParametroGMF(filtro, pParametroGMF, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroGMFService", "ListarParametroGMF", ex);
                return null;
            }
        }


        public List<ParametroGMF> combooperacion(Usuario pusuario)
        {
            try
            {
                return BOParametroGMF.combooperacion(pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroGMFBusiness", "ListarParametroGMF", ex);
                return null;
            }
        }

        public int ModificarEstadoTranGmf(ParametroGMF Entidad, Usuario pUsuario, DateTime fecha, DateTime fechafinal)
        {
            int respuesta = 0;
            try
            {
                return BOParametroGMF.ModificarEstadoTranGmf(Entidad, pUsuario, fecha, fechafinal);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroGMFService", "ModificarEstadoTranGmf", ex);
                return respuesta;
            }
        }     
    }
}
