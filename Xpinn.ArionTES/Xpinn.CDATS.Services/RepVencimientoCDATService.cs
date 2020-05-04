using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.CDATS.Entities;
using Xpinn.CDATS.Business;

namespace Xpinn.CDATS.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class RepVencimientoCDATService
    {

        private RepVencimientoCDATBusiness BORepVencimientoCDAT;
        private ExcepcionBusiness BOExcepcion;

        public RepVencimientoCDATService()
        {
            BORepVencimientoCDAT = new RepVencimientoCDATBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "220320"; } }

        public RepVencimientoCDAT CrearRepVencimientoCDAT(RepVencimientoCDAT pRepVencimientoCDAT, Usuario pusuario)
        {
            try
            {
                pRepVencimientoCDAT = BORepVencimientoCDAT.CrearRepVencimientoCDAT(pRepVencimientoCDAT, pusuario);
                return pRepVencimientoCDAT;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RepVencimientoCDATService", "CrearRepVencimientoCDAT", ex);
                return null;
            }
        }


        public RepVencimientoCDAT ModificarRepVencimientoCDAT(RepVencimientoCDAT pRepVencimientoCDAT, Usuario pusuario)
        {
            try
            {
                pRepVencimientoCDAT = BORepVencimientoCDAT.ModificarRepVencimientoCDAT(pRepVencimientoCDAT, pusuario);
                return pRepVencimientoCDAT;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RepVencimientoCDATService", "ModificarRepVencimientoCDAT", ex);
                return null;
            }
        }


        public void EliminarRepVencimientoCDAT(Int64 pId, Usuario pusuario)
        {
            try
            {
                BORepVencimientoCDAT.EliminarRepVencimientoCDAT(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RepVencimientoCDATService", "EliminarRepVencimientoCDAT", ex);
            }
        }


        public RepVencimientoCDAT ConsultarRepVencimientoCDAT(Int64 pId, Usuario pusuario)
        {
            try
            {
                RepVencimientoCDAT RepVencimientoCDAT = new RepVencimientoCDAT();
                RepVencimientoCDAT = BORepVencimientoCDAT.ConsultarRepVencimientoCDAT(pId, pusuario);
                return RepVencimientoCDAT;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RepVencimientoCDATService", "ConsultarRepVencimientoCDAT", ex);
                return null;
            }
        }


        public List<RepVencimientoCDAT> ListarRepVencimientoCDAT(string[] pfiltro, Usuario pusuario)
        {
            try
            {
                return BORepVencimientoCDAT.ListarRepVencimientoCDAT( pfiltro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RepVencimientoCDATService", "ListarRepVencimientoCDAT", ex);
                return null;
            }
        }


    }
}