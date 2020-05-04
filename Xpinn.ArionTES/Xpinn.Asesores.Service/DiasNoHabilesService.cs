using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DiasNoHabilesService
    {
        private DiasNoHabilesBusiness BODias;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Atributo
        /// </summary>
        public DiasNoHabilesService()
        {
            BODias = new DiasNoHabilesBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110206"; } }


        public Dias_no_habiles CrearDiasNoHabiles(Dias_no_habiles pDias, Usuario vUsuario)
        {
            try
            {
                return BODias.CrearDiasNoHabiles(pDias, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiasNoHabilesService", "CrearDiasNoHabiles", ex);
                return null;
            }
        }

       

        public List<Dias_no_habiles> ListarDiasNoHabiles(Dias_no_habiles pDias, Usuario vUsuario)
        {
            try
            {
                return BODias.ListarDiasNoHabiles(pDias, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiasNoHabilesService", "ListarDiasNoHabiles", ex);
                return null;
            }
        }
    }
}