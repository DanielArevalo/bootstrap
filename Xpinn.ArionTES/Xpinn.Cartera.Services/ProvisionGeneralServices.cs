using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Business;

namespace Xpinn.Cartera.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ProvisionGeneralService
    {

        private ProvisionGeneralBusiness BOProvisionGeneral;
        private ExcepcionBusiness BOExcepcion;

        public ProvisionGeneralService()
        {
            BOProvisionGeneral = new ProvisionGeneralBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "60309"; } }

        public ProvisionGeneral CrearProvisionGeneral(ProvisionGeneral pProvisionGeneral, Usuario pusuario)
        {
            try
            {
                pProvisionGeneral = BOProvisionGeneral.CrearProvisionGeneral(pProvisionGeneral, pusuario);
                return pProvisionGeneral;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProvisionGeneralService", "CrearProvisionGeneral", ex);
                return null;
            }
        }


        public ProvisionGeneral ModificarProvisionGeneral(ProvisionGeneral pProvisionGeneral, Usuario pusuario)
        {
            try
            {
                pProvisionGeneral = BOProvisionGeneral.ModificarProvisionGeneral(pProvisionGeneral, pusuario);
                return pProvisionGeneral;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProvisionGeneralService", "ModificarProvisionGeneral", ex);
                return null;
            }
        }


        public void EliminarProvisionGeneral(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOProvisionGeneral.EliminarProvisionGeneral(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProvisionGeneralService", "EliminarProvisionGeneral", ex);
            }
        }


        public ProvisionGeneral ConsultarProvisionGeneral(Int64 pId, Usuario pusuario)
        {
            try
            {
                ProvisionGeneral ProvisionGeneral = new ProvisionGeneral();
                ProvisionGeneral = BOProvisionGeneral.ConsultarProvisionGeneral(pId, pusuario);
                return ProvisionGeneral;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProvisionGeneralService", "ConsultarProvisionGeneral", ex);
                return null;
            }
        }


        public List<ProvisionGeneral> ListarProvisionGeneral(ProvisionGeneral pProvisionGeneral, Usuario pusuario)
        {
            try
            {
                return BOProvisionGeneral.ListarProvisionGeneral(pProvisionGeneral, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProvisionGeneralService", "ListarProvisionGeneral", ex);
                return null;
            }
        }

        public List<ProvisionGeneral> ProvisionGeneral(DateTime pFechaCorte, Usuario pUsuario)
        {
            try
            {
                return BOProvisionGeneral.ProvisionGeneral(pFechaCorte, pUsuario);
            }
            catch
            {
                return null;
            }

        }


        public bool GuardarProvisionGeneral(DateTime pfecha, List<ProvisionGeneral> pLstProvisionGeneral, Usuario pusuario)
        {
            try
            {
                return BOProvisionGeneral.GuardarProvisionGeneral(pfecha, pLstProvisionGeneral, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProvisionGeneralService", "GuardarProvisionGeneral", ex);
                return false;
            }
        }

    }
}
