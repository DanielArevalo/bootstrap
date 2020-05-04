using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Scoring.Business;
using Xpinn.Scoring.Entities;

namespace Xpinn.Scoring.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>


    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ParametroService
    {
        private ParametroBusiness BOParametro;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Parametro
        /// </summary>
        public ParametroService()
        {
            BOParametro = new ParametroBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "160202"; } }



        /// <summary>
        /// Servicio para crear Parametro
        /// </summary>
        /// <param name="pEntity">Entidad Parametro</param>
        /// <returns>Entidad Parametro creada</returns>
        public Parametro CrearParametro(Parametro pParametro, Usuario pUsuario)
        {
            try
            {
                return BOParametro.CrearParametro(pParametro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroService", "CrearParametro", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Parametro
        /// </summary>
        /// <param name="pParametro">Entidad Parametro</param>
        /// <returns>Entidad Parametro modificada</returns>
        public Parametro ModificarParametro(Parametro pParametro, Usuario pUsuario)
        {
            try
            {
                return BOParametro.ModificarParametro(pParametro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroService", "ModificaParametro", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Parametro
        /// </summary>
        /// <param name="pId">identificador de Parametro</param>
        public void EliminarParametro(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOParametro.EliminarParametro(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroService", "EliminarParametro", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Parametro
        /// </summary>
        /// <param name="pId">identificador de Parametro</param>
        /// <returns>Entidad Parametro</returns>
        public Parametro ConsultarParametro(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOParametro.ConsultarParametro(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroService", "ConsultarParametro", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Parametros a partir de unos filtros
        /// </summary>
        /// <param name="pParametro">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Parametro obtenidos</returns>
        public List<Parametro> ListarParametro(Parametro pParametro, Usuario pUsuario)
        {
            try
            {
                return BOParametro.ListarParametro(pParametro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroService", "ListarParametro", ex);
                return null;
            }
        }


        public List<Modelo> ListarCampos(Modelo pParametro, Usuario pUsuario)
        {
            try
            {
                return BOParametro.ListarCampos(pParametro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroService", "ListarCampos", ex);
                return null;
            }
        }

    }
}