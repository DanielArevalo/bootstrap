using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Business;

namespace Xpinn.Nomina.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class Experiencia_LaboralService
    {

        private Experiencia_LaboralBusiness BOExperiencia_Laboral;
        private ExcepcionBusiness BOExcepcion;

        public Experiencia_LaboralService()
        {
            BOExperiencia_Laboral = new Experiencia_LaboralBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return ""; } }

        public Experiencia_Laboral CrearExperiencia_Laboral(Experiencia_Laboral pExperiencia_Laboral, Usuario pusuario)
        {
            try
            {
                pExperiencia_Laboral = BOExperiencia_Laboral.CrearExperiencia_Laboral(pExperiencia_Laboral, pusuario);
                return pExperiencia_Laboral;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Experiencia_LaboralService", "CrearExperiencia_Laboral", ex);
                return null;
            }
        }


        public Experiencia_Laboral ModificarExperiencia_Laboral(Experiencia_Laboral pExperiencia_Laboral, Usuario pusuario)
        {
            try
            {
                pExperiencia_Laboral = BOExperiencia_Laboral.ModificarExperiencia_Laboral(pExperiencia_Laboral, pusuario);
                return pExperiencia_Laboral;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Experiencia_LaboralService", "ModificarExperiencia_Laboral", ex);
                return null;
            }
        }


        public void EliminarExperiencia_Laboral(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOExperiencia_Laboral.EliminarExperiencia_Laboral(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Experiencia_LaboralService", "EliminarExperiencia_Laboral", ex);
            }
        }


        public Experiencia_Laboral ConsultarExperiencia_Laboral(Int64 pId, Usuario pusuario)
        {
            try
            {
                Experiencia_Laboral Experiencia_Laboral = new Experiencia_Laboral();
                Experiencia_Laboral = BOExperiencia_Laboral.ConsultarExperiencia_Laboral(pId, pusuario);
                return Experiencia_Laboral;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Experiencia_LaboralService", "ConsultarExperiencia_Laboral", ex);
                return null;
            }
        }


        public List<Experiencia_Laboral> ListarExperiencia_Laboral(long cod_persona, Usuario pusuario)
        {
            try
            {
                return BOExperiencia_Laboral.ListarExperiencia_Laboral(cod_persona, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Experiencia_LaboralService", "ListarExperiencia_Laboral", ex);
                return null;
            }
        }


    }
}
