using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Business;

namespace Xpinn.Aportes.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class EscalafonSalarialService
    {

        private EscalafonSalarialBusiness BOEscalafonSalarial;
        private ExcepcionBusiness BOExcepcion;

        public EscalafonSalarialService()
        {
            BOEscalafonSalarial = new EscalafonSalarialBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "170114"; } }

        public EscalafonSalarial CrearEscalafonSalarial(EscalafonSalarial pEscalafonSalarial, Usuario pusuario)
        {
            try
            {
                pEscalafonSalarial = BOEscalafonSalarial.CrearEscalafonSalarial(pEscalafonSalarial, pusuario);
                return pEscalafonSalarial;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EscalafonSalarialService", "CrearEscalafonSalarial", ex);
                return null;
            }
        }


        public EscalafonSalarial ModificarEscalafonSalarial(EscalafonSalarial pEscalafonSalarial, Usuario pusuario)
        {
            try
            {
                pEscalafonSalarial = BOEscalafonSalarial.ModificarEscalafonSalarial(pEscalafonSalarial, pusuario);
                return pEscalafonSalarial;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EscalafonSalarialService", "ModificarEscalafonSalarial", ex);
                return null;
            }
        }


        public void EliminarEscalafonSalarial(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOEscalafonSalarial.EliminarEscalafonSalarial(pId, pusuario);
            }
            catch 
            {
                return;
            }
        }


        public EscalafonSalarial ConsultarEscalafonSalarial(Int64 pId, Usuario pusuario)
        {
            try
            {
                EscalafonSalarial EscalafonSalarial = new EscalafonSalarial();
                EscalafonSalarial = BOEscalafonSalarial.ConsultarEscalafonSalarial(pId, pusuario);
                return EscalafonSalarial;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EscalafonSalarialService", "ConsultarEscalafonSalarial", ex);
                return null;
            }
        }


        public List<EscalafonSalarial> ListarEscalafonSalarial(string Filtro,EscalafonSalarial pEscalafonSalarial, Usuario pusuario)
        {
            try
            {
                return BOEscalafonSalarial.ListarEscalafonSalarial(Filtro,pEscalafonSalarial, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EscalafonSalarialService", "ListarEscalafonSalarial", ex);
                return null;
            }
        }


    }
}