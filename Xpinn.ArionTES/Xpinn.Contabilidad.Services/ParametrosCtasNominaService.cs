using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Contabilidad.Entities;
using Xpinn.Contabilidad.Business;

namespace Xpinn.Contabilidad.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ParametrosCtasNominaService
    {

        private ParametrosCtasNominaBusiness BOPar_Cue_Nomina;
        private ExcepcionBusiness BOExcepcion;

        public ParametrosCtasNominaService()
        {
            BOPar_Cue_Nomina = new ParametrosCtasNominaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30807"; } }

        public Par_Cue_Nomina CrearPar_Cue_Nomina(Par_Cue_Nomina pPar_Cue_Nomina, Usuario pusuario)
        {
            try
            {
                pPar_Cue_Nomina = BOPar_Cue_Nomina.CrearPar_Cue_Nomina(pPar_Cue_Nomina, pusuario);
                return pPar_Cue_Nomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_NominaService", "CrearPar_Cue_Nomina", ex);
                return null;
            }
        }


        public Par_Cue_Nomina ModificarPar_Cue_Nomina(Par_Cue_Nomina pPar_Cue_Nomina, Usuario pusuario)
        {
            try
            {
                pPar_Cue_Nomina = BOPar_Cue_Nomina.ModificarPar_Cue_Nomina(pPar_Cue_Nomina, pusuario);
                return pPar_Cue_Nomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_NominaService", "ModificarPar_Cue_Nomina", ex);
                return null;
            }
        }


        public void EliminarPar_Cue_Nomina(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOPar_Cue_Nomina.EliminarPar_Cue_Nomina(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_NominaService", "EliminarPar_Cue_Nomina", ex);
            }
        }


        public Par_Cue_Nomina ConsultarPar_Cue_Nomina(Int64 pId, Usuario pusuario)
        {
            try
            {
                Par_Cue_Nomina Par_Cue_Nomina = new Par_Cue_Nomina();
                Par_Cue_Nomina = BOPar_Cue_Nomina.ConsultarPar_Cue_Nomina(pId, pusuario);
                return Par_Cue_Nomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_NominaService", "ConsultarPar_Cue_Nomina", ex);
                return null;
            }
        }


        public List<Par_Cue_Nomina> ListarPar_Cue_Nomina(string filtro, Usuario pusuario)
        {
            try
            {
                return BOPar_Cue_Nomina.ListarPar_Cue_Nomina(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_NominaService", "ListarPar_Cue_Nomina", ex);
                return null;
            }
        }


    }
}