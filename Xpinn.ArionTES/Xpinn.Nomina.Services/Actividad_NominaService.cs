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
    public class Actividad_NominaEntitiesService
    {

        private Actividad_NominaEntitiesBusiness BOActividad_NominaEntities;
        private ExcepcionBusiness BOExcepcion;

        public Actividad_NominaEntitiesService()
        {
            BOActividad_NominaEntities = new Actividad_NominaEntitiesBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250105"; } }

        public Actividad_Nomina CrearActividad_NominaEntities(Actividad_Nomina pActividad_NominaEntities, Usuario pusuario)
        {
            try
            {
                pActividad_NominaEntities = BOActividad_NominaEntities.CrearActividad_NominaEntities(pActividad_NominaEntities, pusuario);
                return pActividad_NominaEntities;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Actividad_NominaEntitiesService", "CrearActividad_NominaEntities", ex);
                return null;
            }
        }


        public Actividad_Nomina ModificarActividad_NominaEntities(Actividad_Nomina pActividad_NominaEntities, Usuario pusuario)
        {
            try
            {
                pActividad_NominaEntities = BOActividad_NominaEntities.ModificarActividad_NominaEntities(pActividad_NominaEntities, pusuario);
                return pActividad_NominaEntities;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Actividad_NominaEntitiesService", "ModificarActividad_NominaEntities", ex);
                return null;
            }
        }


        public void EliminarActividad_NominaEntities(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOActividad_NominaEntities.EliminarActividad_NominaEntities(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Actividad_NominaEntitiesService", "EliminarActividad_NominaEntities", ex);
            }
        }


        public Actividad_Nomina ConsultarActividad_NominaEntities(Int64 pId, Usuario pusuario)
        {
            try
            {
                Actividad_Nomina Actividad_NominaEntities = new Actividad_Nomina();
                Actividad_NominaEntities = BOActividad_NominaEntities.ConsultarActividad_NominaEntities(pId, pusuario);
                return Actividad_NominaEntities;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Actividad_NominaEntitiesService", "ConsultarActividad_NominaEntities", ex);
                return null;
            }
        }


        public List<Actividad_Nomina> ListarActividad_NominaEntities(string filtro, Usuario pusuario)
        {
            try
            {
                return BOActividad_NominaEntities.ListarActividad_NominaEntities(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Actividad_NominaEntitiesService", "ListarActividad_NominaEntities", ex);
                return null;
            }
        }


    }
}
