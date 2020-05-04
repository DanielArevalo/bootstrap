using System;
using System.Collections.Generic;
using System.ServiceModel;
using Xpinn.Riesgo.Business;
using Xpinn.Riesgo.Entities;
using Xpinn.Util;

namespace Xpinn.Riesgo.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    /// 
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]

    public class ActDatosServices
    {

        private ActDatosBusiness BOActDatos;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para DefinirVariables
        /// </summary>
        public ActDatosServices()
        {
            BOActDatos = new ActDatosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "270108"; } }

        public List<ActDatos> ListarActDatos(string Fechaini, string Fechafin, ActDatos pActDatos, Usuario usuario)
        {
            try
            {
                return BOActDatos.ListarActDatos(Fechaini, Fechafin, pActDatos, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActDatosServices", "ListarActDatos", ex);
                return null;
            }
        }

        public List<ActDatos> ListarActDatosNoActualizado(ActDatos pActDatos, Usuario usuario)
        {
            try
            {
                return BOActDatos.ListarActDatosNoActualizado(pActDatos, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActDatosServices", "ListarActDatosNoActualizado", ex);
                return null;
            }
        }

    }
}
