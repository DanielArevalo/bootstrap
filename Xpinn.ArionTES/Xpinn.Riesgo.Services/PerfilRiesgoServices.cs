using System;
using System.Collections.Generic;
using System.ServiceModel;
using Xpinn.Riesgo.Business;
using Xpinn.Riesgo.Entities;
using Xpinn.Util;

namespace Xpinn.Riesgo.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]

  public  class PerfilRiesgoServices
    {

        private PerfilRiesgoBusiness BOPerfilRiesgo;
        private ExcepcionBusiness BOExcepcion;


        public PerfilRiesgoServices()
        {
            BOPerfilRiesgo = new PerfilRiesgoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "270204"; } }

        public List<PerfilRiesgo> ListarPerfilRiesgo(PerfilRiesgo pPerfilRiesgo, Usuario usuario)
        {
            try
            {
                return BOPerfilRiesgo.ListarPerfilRiesgo(pPerfilRiesgo, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilRiesgoServices", "ListarPerfilRiesgo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para crear registro de una Actividad
        /// </summary>
        /// <param name="pActividad">Objeto con los datos del factor de riesgo</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public PerfilRiesgo CrearPerfilRiesgo(PerfilRiesgo pPerfilRiesgo, Usuario vUsuario)
        {
            try
            {
                pPerfilRiesgo = BOPerfilRiesgo.CrearPerfilRiesgo(pPerfilRiesgo, vUsuario);
                return pPerfilRiesgo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilRiesgoServices", "CrearPerfilRiesgo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar un registro de una actividad de riesgo
        /// </summary>
        /// <param name="pActividadEco">Objeto con los datos de la causa</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public PerfilRiesgo ModificarPerfilRiesgo(PerfilRiesgo pPerfilRiesgo, Usuario vUsuario)
        {
            try
            {
                pPerfilRiesgo = BOPerfilRiesgo.ModificarPerfilRiesgo(pPerfilRiesgo, vUsuario);
                return pPerfilRiesgo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilRiesgoServices", "ModificarPerfilRiesgo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para eliminar un registro de una actividad de riesgo
        /// </summary>
        /// <param name="pActividadEco">Objeto con el código del factor</param>
        /// <param name="vUsuario"></param>
        public void EliminarPerfilRiesgo(PerfilRiesgo pPerfilRiesgo, Usuario vUsuario)
        {
            try
            {
                BOPerfilRiesgo.EliminarPerfilRiesgo(pPerfilRiesgo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilRiesgoServices", "EliminarPerfilRiesgo", ex);
            }
        }

        /// <summary>
        /// Consultar una actividad de riesgo especifico
        /// </summary>
        /// <param name="pActividad">Objeto con datos para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public PerfilRiesgo ConsultarFactorRiesgo(PerfilRiesgo pPerfilRiesgo, Usuario vUsuario)
        {
            try
            {
                pPerfilRiesgo = BOPerfilRiesgo.ConsultarPerfilRiesgo(pPerfilRiesgo, vUsuario);
                return pPerfilRiesgo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilRiesgoServices", "ConsultarFactorRiesgo", ex);
                return null;
            }
        }

    }
}
