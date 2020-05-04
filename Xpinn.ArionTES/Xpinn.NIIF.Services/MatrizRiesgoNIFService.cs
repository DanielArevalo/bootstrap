using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.NIIF.Business;
using Xpinn.NIIF.Entities;

namespace Xpinn.NIIF.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>


    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class MatrizRiesgoNIFService
    {
        private MatrizRiesgoNIFBusiness BOMatrizRiesgoNIF;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para MatrizRiesgoNIF
        /// </summary>
        public MatrizRiesgoNIFService()
        {
            BOMatrizRiesgoNIF = new MatrizRiesgoNIFBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "210202"; } }

        /// <summary>
        /// Servicio para crear MatrizRiesgoNIF
        /// </summary>
        /// <param name="pEntity">Entidad MatrizRiesgoNIF</param>
        /// <returns>Entidad MatrizRiesgoNIF creada</returns>
        public MatrizRiesgoNIF CrearMatrizRiesgoNIF(MatrizRiesgoNIF pMatrizRiesgoNIF, Usuario pUsuario)
        {
            try
            {
                return BOMatrizRiesgoNIF.CrearMatrizRiesgoNIF(pMatrizRiesgoNIF, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizRiesgoNIFService", "CrearMatrizRiesgoNIF", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar MatrizRiesgoNIF
        /// </summary>
        /// <param name="pMatrizRiesgoNIF">Entidad MatrizRiesgoNIF</param>
        /// <returns>Entidad MatrizRiesgoNIF modificada</returns>
        public MatrizRiesgoNIF ModificarMatrizRiesgoNIF(MatrizRiesgoNIF pMatrizRiesgoNIF, Usuario pUsuario)
        {
            try
            {
                return BOMatrizRiesgoNIF.ModificarMatrizRiesgoNIF(pMatrizRiesgoNIF, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizRiesgoNIFService", "ModificarMatrizRiesgoNIF", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar MatrizRiesgoNIF
        /// </summary>
        /// <param name="pId">identificador de MatrizRiesgoNIF</param>
        public void EliminarMatrizRiesgoNIF(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOMatrizRiesgoNIF.EliminarMatrizRiesgoNIF(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarMatrizRiesgoNIF", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener MatrizRiesgoNIF
        /// </summary>
        /// <param name="pId">identificador de MatrizRiesgoNIF</param>
        /// <returns>Entidad MatrizRiesgoNIF</returns>
        public MatrizRiesgoNIF ConsultarMatrizRiesgoNIF(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOMatrizRiesgoNIF.ConsultarMatrizRiesgoNIF(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizRiesgoNIFService", "ConsultarMatrizRiesgoNIF", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de MatrizRiesgoNIFs a partir de unos filtros
        /// </summary>
        /// <param name="pMatrizRiesgoNIF">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MatrizRiesgoNIF obtenidos</returns>
        public List<MatrizRiesgoNIF> ListarMatrizRiesgoNIF(MatrizRiesgoNIF pMatrizRiesgoNIF, Usuario pUsuario)
        {
            try
            {
                return BOMatrizRiesgoNIF.ListarMatrizRiesgoNIF(pMatrizRiesgoNIF, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizRiesgoNIFService", "ListarMatrizRiesgoNIF", ex);
                return null;
            }
        }

        public List<Clasificacion> ListarClasificacion(Clasificacion clasificacion, Usuario pUsuario)
        {
            try
            {
                return BOMatrizRiesgoNIF.ListarClasificacion(clasificacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizRiesgoNIFService", "ListarClasificacion", ex);
                return null;
            }
        }

    }
}