using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;
 
namespace Xpinn.Nomina.Business
{

    public class RangosFondoSolidaridadBussiness : GlobalBusiness
    {

        private RangosFondoSolidaridadData DARangosFondoSolidaridad;

        public RangosFondoSolidaridadBussiness()
        {
            DARangosFondoSolidaridad= new RangosFondoSolidaridadData();
        }

        public RangosFondoSolidaridad CrearRangosFondoSolidaridad(RangosFondoSolidaridad pRangosFondoSolidaridad, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pRangosFondoSolidaridad = DARangosFondoSolidaridad.CrearRangosFondoSolidaridad(pRangosFondoSolidaridad, pusuario);

                    ts.Complete();

                }

                return pRangosFondoSolidaridad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RangosFondoSolidaridadBussiness", "CrearRangosCajaCompensacion", ex);
                return null;
            }
        }


        public RangosFondoSolidaridad ModificarRangosFondoSolidaridad(RangosFondoSolidaridad pRangosFondoSolidaridad, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pRangosFondoSolidaridad = DARangosFondoSolidaridad.ModificarRangosFondoSolidaridad(pRangosFondoSolidaridad, pusuario);

                    ts.Complete();

                }

                return pRangosFondoSolidaridad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RangosFondoSolidaridadBussiness", "ModificarRangosFondoSolidaridad", ex);
                return null;
            }
        }



        public RangosFondoSolidaridad ConsultarRangosFondoSolidaridad(Int64 pId, Usuario pusuario)
        {
            try
            {
                RangosFondoSolidaridad RangosCajasCompensacion = new RangosFondoSolidaridad();
                RangosCajasCompensacion = DARangosFondoSolidaridad.ConsultarRangosFondoSolidaridad(pId, pusuario);
                return RangosCajasCompensacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RangosFondoSolidaridadBussiness", "ConsultarRangosFondoSolidaridad", ex);
                return null;
            }
        }

        
        public List<RangosFondoSolidaridad> ListarRangosFondoSolidaridad(string pid, Usuario pusuario)
        {
            try
            {
                return DARangosFondoSolidaridad.ListarRangosFondoSolidaridad(pid, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RangosFondoSolidaridadBussiness", "ListarRangosFondoSolidaridad", ex);
                return null;
            }
        }


    }
}
