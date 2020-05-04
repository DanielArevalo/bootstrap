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
    public class CentroGestionService
    {
        private CentroGestionBusiness BOCentroGestion;
        private ExcepcionBusiness BOExcepcion;

        
        public CentroGestionService()
        {
            BOCentroGestion = new CentroGestionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30403"; } }

        public CentroGestion Crear_Mod_CentroGestion(CentroGestion pCentro, Usuario vUsuario, int opcion)
        {
            try
            {
                return BOCentroGestion.Crear_Mod_CentroGestion(pCentro, vUsuario, opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CentroGestionService", "Crear_Mod_CentroGestion", ex);
                return null;
            }
        }


        public List<CentroGestion> ListarCentroGestion(CentroGestion pCentro, String filtro, Usuario vUsuario)
        {
            try
            {
                return BOCentroGestion.ListarCentroGestion(pCentro,filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CentroGestionService", "ListarCentroGestion", ex);
                return null;
            }
        }


        public void EliminarCentroGestion(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOCentroGestion.EliminarCentroGestion(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CentroGestionService", "EliminarCentroGestion", ex);
            }
        }

        public CentroGestion ConsultarCentroGestion(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOCentroGestion.ConsultarCentroGestion(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CentroGestionService", "ConsultarCentroGestion", ex);
                return null;
            }
        }
                
    }
}