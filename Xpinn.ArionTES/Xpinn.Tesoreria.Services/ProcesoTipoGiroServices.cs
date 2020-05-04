using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Business;

namespace Xpinn.Tesoreria.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ProcesoTipoGiroService
    {

        private ProcesoTipoGiroBusiness BOProcesoTipoGiro;
        private ExcepcionBusiness BOExcepcion;

        public ProcesoTipoGiroService()
        {
            BOProcesoTipoGiro = new ProcesoTipoGiroBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return ""; } }

        public ProcesoTipoGiro CrearProcesoTipoGiro(ProcesoTipoGiro pProcesoTipoGiro, Usuario pusuario)
        {
            try
            {
                pProcesoTipoGiro = BOProcesoTipoGiro.CrearProcesoTipoGiro(pProcesoTipoGiro, pusuario);
                return pProcesoTipoGiro;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoTipoGiroService", "CrearProcesoTipoGiro", ex);
                return null;
            }
        }


        public ProcesoTipoGiro ModificarProcesoTipoGiro(ProcesoTipoGiro pProcesoTipoGiro, Usuario pusuario)
        {
            try
            {
                pProcesoTipoGiro = BOProcesoTipoGiro.ModificarProcesoTipoGiro(pProcesoTipoGiro, pusuario);
                return pProcesoTipoGiro;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoTipoGiroService", "ModificarProcesoTipoGiro", ex);
                return null;
            }
        }


        public void EliminarProcesoTipoGiro(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOProcesoTipoGiro.EliminarProcesoTipoGiro(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoTipoGiroService", "EliminarProcesoTipoGiro", ex);
            }
        }


        public ProcesoTipoGiro ConsultarProcesoTipoGiro(Int64 pId, Usuario pusuario)
        {
            try
            {
                return BOProcesoTipoGiro.ConsultarProcesoTipoGiro(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoTipoGiroService", "ConsultarProcesoTipoGiro", ex);
                return null;
            }
        }


        public List<ProcesoTipoGiro> ListarProcesoTipoGiro(ProcesoTipoGiro pProcesoTipoGiro, Usuario pusuario)
        {
            try
            {
                return BOProcesoTipoGiro.ListarProcesoTipoGiro(pProcesoTipoGiro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoTipoGiroService", "ListarProcesoTipoGiro", ex);
                return null;
            }
        }


    }
}