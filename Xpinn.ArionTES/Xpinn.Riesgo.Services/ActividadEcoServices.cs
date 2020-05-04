using System;
using System.Collections.Generic;
using System.ServiceModel;
using Xpinn.Riesgo.Business;
using Xpinn.Riesgo.Entities;
using Xpinn.Util;

namespace Xpinn.Riesgo.Services
/// <summary>
/// Servicios para Programa
/// </summary>
{

    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]

    public class ActividadEcoServices
    {
        private ActividadEcoBusiness BOActividades;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para DefinirVariables
        /// </summary>
        public ActividadEcoServices()
        {
            BOActividades = new ActividadEcoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "270201"; } }

        public List<ActividadEco> ListarActividadesEco(ActividadEco Actividades, Usuario usuario)
        {
            try
            {
                return BOActividades.ListarActividadEco(Actividades, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadEcoServices", "ListarActividadesEco", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para crear registro de una Actividad
        /// </summary>
        /// <param name="pActividad">Objeto con los datos del factor de riesgo</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public ActividadEco CrearActividad(ActividadEco pActividad, Usuario vUsuario)
        {
            try
            {
                pActividad = BOActividades.CrearActividades(pActividad, vUsuario);
                return pActividad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadEcoServices", "CrearActividad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar un registro de una actividad de riesgo
        /// </summary>
        /// <param name="pActividadEco">Objeto con los datos de la causa</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public ActividadEco ModificarActividad(ActividadEco pActividadEco, Usuario vUsuario)
        {
            try
            {
                pActividadEco = BOActividades.ModificarActividades(pActividadEco, vUsuario);
                return pActividadEco;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadEcoServices", "ModificarActividad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para eliminar un registro de una actividad de riesgo
        /// </summary>
        /// <param name="pActividadEco">Objeto con el código del factor</param>
        /// <param name="vUsuario"></param>
        public void EliminarActividad(ActividadEco pActividadEco, Usuario vUsuario)
        {
            try
            {
                BOActividades.EliminarActividades(pActividadEco, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadEcoServices", "EliminarActividades", ex);
            }
        }

        /// <summary>
        /// Consultar una actividad de riesgo especifico
        /// </summary>
        /// <param name="pActividad">Objeto con datos para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public ActividadEco ConsultarFactorRiesgo(ActividadEco pActividad, Usuario vUsuario)
        {
            try
            {
                pActividad = BOActividades.ConsultarActividades(pActividad, vUsuario);
                return pActividad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadEcoServices", "ConsultarActividades", ex);
                return null;
            }
        }
    }
}
