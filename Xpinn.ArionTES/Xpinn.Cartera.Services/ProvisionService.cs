using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Business;
using System.Web;
using Xpinn.Util;


namespace Xpinn.Cartera.Services
{
    /// <summary>
    /// Servicio para cambios en clasificación y provisión
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ProvisionService
    {
        private ProvisionBusiness BOProvisionCartera;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CierreHistorio
        /// </summary>
        public ProvisionService()
        {
            BOProvisionCartera = new ProvisionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "60313"; } }
        public string CodigoProgramaProv { get { return "60310"; } }
              

        #region PROVISION DE CREDITOS

        public List<Provision> ListarProvisiones(string filtro, Usuario pUsuario)
        {
            try
            {
                return BOProvisionCartera.ListarProvisiones(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProvisionService", "ListarProvisiones", ex);
                return null;
            }
        }

        public void ModificacionProvision(List<Provision> lstProv, ref string pError, Usuario vUsuario)
        {
            try
            {
                BOProvisionCartera.ModificacionProvision(lstProv, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProvisionService", "ModificacionProvision", ex);
            }
        }

        #endregion

        public List<Provision> ListarClasificaciones(string filtro, Usuario pUsuario)
        {
            try
            {
                return BOProvisionCartera.ListarClasificaciones(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProvisionService", "ListarClasificaciones", ex);
                return null;
            }
        }


        public void ModificarClasificacion(List<Provision> lstProv, ref string pError, Usuario vUsuario)
        {
            try
            {
                BOProvisionCartera.ModificarClasificacion(lstProv, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProvisionService", "ModificarClasificacion", ex);
            }
        }


    }
}

