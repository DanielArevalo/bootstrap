using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Business
{

    public class Actividad_NominaEntitiesBusiness : GlobalBusiness
    {

        private Actividad_NominaNominaData DAActividad_NominaEntities;

        public Actividad_NominaEntitiesBusiness()
        {
            DAActividad_NominaEntities = new Actividad_NominaNominaData();
        }

        public Actividad_Nomina CrearActividad_NominaEntities(Actividad_Nomina pActividad_NominaEntities, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pActividad_NominaEntities = DAActividad_NominaEntities.CrearActividada_NominaEntities(pActividad_NominaEntities, pusuario);

                    ts.Complete();

                }

                return pActividad_NominaEntities;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Actividad_NominaEntitiesBusiness", "CrearActividad_NominaEntities", ex);
                return null;
            }
        }


        public Actividad_Nomina ModificarActividad_NominaEntities(Actividad_Nomina pActividad_NominaEntities, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pActividad_NominaEntities = DAActividad_NominaEntities.ModificarActividada_NominaEntities(pActividad_NominaEntities, pusuario);

                    ts.Complete();

                }

                return pActividad_NominaEntities;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Actividad_NominaEntitiesBusiness", "ModificarActividad_NominaEntities", ex);
                return null;
            }
        }


        public void EliminarActividad_NominaEntities(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAActividad_NominaEntities.EliminarActividada_NominaEntities(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Actividad_NominaEntitiesBusiness", "EliminarActividad_NominaEntities", ex);
            }
        }


        public Actividad_Nomina ConsultarActividad_NominaEntities(Int64 pId, Usuario pusuario)
        {
            try
            {
                Actividad_Nomina Actividad_NominaEntities = new Actividad_Nomina();
                Actividad_NominaEntities = DAActividad_NominaEntities.ConsultarActividada_NominaEntities(pId, pusuario);
                return Actividad_NominaEntities;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Actividad_NominaEntitiesBusiness", "ConsultarActividad_NominaEntities", ex);
                return null;
            }
        }


        public List<Actividad_Nomina> ListarActividad_NominaEntities(string filtro, Usuario pusuario)
        {
            try
            {
                return DAActividad_NominaEntities.ListarActividada_NominaEntities(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Actividad_NominaEntitiesBusiness", "ListarActividad_NominaEntities", ex);
                return null;
            }
        }


    }
}

