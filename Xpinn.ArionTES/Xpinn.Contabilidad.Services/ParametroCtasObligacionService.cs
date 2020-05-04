using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Contabilidad.Business;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ParametroCtasObligacionService
    {
        private ParametroCtasObligacionBusiness BOParametro;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para PlanCuentas
        /// </summary>
        public ParametroCtasObligacionService()
        {
            BOParametro = new ParametroCtasObligacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "31005"; } }

        public List<Par_Cue_Obligacion> ListarParametrosCtasOBLI(string filtro, Usuario vUsuario)
        {
            try
            {
                return BOParametro.ListarParametrosCtasOBLI(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroCtasObligacionService", "ListarParametrosCtasOBLI", ex);
                return null;
            }
        }


        public Par_Cue_Obligacion ConsultarParametroCtasOBLI(Int32 pId, Usuario vUsuario)
        {
            try
            {
                return BOParametro.ConsultarParametroCtasOBLI(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroCtasObligacionService", "ConsultarParametroCtasOBLI", ex);
                return null;
            }
        }

        public Par_Cue_Obligacion CrearParamCtasObligacion(Par_Cue_Obligacion pParam, Usuario vUsuario, int opcion)
        {
            try
            {
                return BOParametro.CrearParamCtasObligacion(pParam, vUsuario, opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroCtasObligacionService", "CrearParamCtasObligacion", ex);
                return null;
            }
        }


        public void EliminarParametroCtasOBLI(Int32 pId, Usuario vUsuario)
        {
            try
            {
                BOParametro.EliminarParametroCtasOBLI(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroCtasObligacionService", "EliminarParametroCtasOBLI", ex);
            }
        }


    }
}