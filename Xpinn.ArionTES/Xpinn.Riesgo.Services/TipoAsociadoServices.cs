using System;
using System.Collections.Generic;
using System.ServiceModel;
using Xpinn.Riesgo.Business;
using Xpinn.Riesgo.Entities;
using Xpinn.Util;

namespace Xpinn.Riesgo.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]

  public  class TipoAsociadoServices
    {

        private TipoAsociadoBusiness BOTipoAsociado;
        private ExcepcionBusiness BOExcepcion;


        public TipoAsociadoServices()
        {
            BOTipoAsociado = new TipoAsociadoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "270206"; } }

        public List<TipoAsociado> ListarTipoAsociado(TipoAsociado pTipoAsociado, Usuario usuario)
        {
            try
            {
                return BOTipoAsociado.ListarTipoAsociado(pTipoAsociado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoAsociadoServices", "ListarTipoAsociado", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para crear registro de una Actividad
        /// </summary>
        /// <param name="pActividad">Objeto con los datos del factor de riesgo</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public TipoAsociado CrearTipoAsociado(TipoAsociado pTipoAsociado, Usuario vUsuario)
        {
            try
            {
                pTipoAsociado = BOTipoAsociado.CrearTipoAsociado(pTipoAsociado, vUsuario);
                return pTipoAsociado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoAsociadoServices", "CrearTipoAsociado", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar un registro de una actividad de riesgo
        /// </summary>
        /// <param name="pActividadEco">Objeto con los datos de la causa</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public TipoAsociado ModificarTipoAsociado(TipoAsociado pTipoAsociado, Usuario vUsuario)
        {
            try
            {
                pTipoAsociado = BOTipoAsociado.ModificarTipoAsociado(pTipoAsociado, vUsuario);
                return pTipoAsociado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoAsociadoServices", "ModificarTipoAsociado", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para eliminar un registro de una actividad de riesgo
        /// </summary>
        /// <param name="pActividadEco">Objeto con el código del factor</param>
        /// <param name="vUsuario"></param>
        public void EliminarTipoAsociado(TipoAsociado pTipoAsociado, Usuario vUsuario)
        {
            try
            {
                BOTipoAsociado.EliminarTipoAsociado(pTipoAsociado, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoAsociadoServices", "EliminarTipoAsociado", ex);
            }
        }

        /// <summary>
        /// Consultar una actividad de riesgo especifico
        /// </summary>
        /// <param name="pActividad">Objeto con datos para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public TipoAsociado ConsultarTipoAsociado(TipoAsociado pTipoAsociado, Usuario vUsuario)
        {
            try
            {
                pTipoAsociado = BOTipoAsociado.ConsultarTipoAsociado(pTipoAsociado, vUsuario);
                return pTipoAsociado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoAsociadoServices", "ConsultarTipoAsociado", ex);
                return null;
            }
        }

    }
}
