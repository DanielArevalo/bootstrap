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
    public class MatrizRiesgoFactorNIFService
    {
        private MatrizRiesgoFactorNIFBusiness BOMatrizRiesgoFactorNIF;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para MatrizRiesgoFactorNIF
        /// </summary>
        public MatrizRiesgoFactorNIFService()
        {
            BOMatrizRiesgoFactorNIF = new MatrizRiesgoFactorNIFBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "210202"; } }

        /// <summary>
        /// Servicio para crear MatrizRiesgoFactorNIF
        /// </summary>
        /// <param name="pEntity">Entidad MatrizRiesgoFactorNIF</param>
        /// <returns>Entidad MatrizRiesgoFactorNIF creada</returns>
        public MatrizRiesgoFactorNIF CrearMatrizRiesgoFactorNIF(MatrizRiesgoFactorNIF pMatrizRiesgoFactorNIF, Usuario pUsuario)
        {
            try
            {
                return BOMatrizRiesgoFactorNIF.CrearMatrizRiesgoFactorNIF(pMatrizRiesgoFactorNIF, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizRiesgoFactorNIFService", "CrearMatrizRiesgoFactorNIF", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar MatrizRiesgoFactorNIF
        /// </summary>
        /// <param name="pMatrizRiesgoFactorNIF">Entidad MatrizRiesgoFactorNIF</param>
        /// <returns>Entidad MatrizRiesgoFactorNIF modificada</returns>
        public MatrizRiesgoFactorNIF ModificarMatrizRiesgoFactorNIF(MatrizRiesgoFactorNIF pMatrizRiesgoFactorNIF, Usuario pUsuario)
        {
            try
            {
                return BOMatrizRiesgoFactorNIF.ModificarMatrizRiesgoFactorNIF(pMatrizRiesgoFactorNIF, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizRiesgoFactorNIFService", "ModificarMatrizRiesgoFactorNIF", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar MatrizRiesgoFactorNIF
        /// </summary>
        /// <param name="pId">identificador de MatrizRiesgoFactorNIF</param>
        public void EliminarMatrizRiesgoFactorNIF(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOMatrizRiesgoFactorNIF.EliminarMatrizRiesgoFactorNIF(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarMatrizRiesgoFactorNIF", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener MatrizRiesgoFactorNIF
        /// </summary>
        /// <param name="pId">identificador de MatrizRiesgoFactorNIF</param>
        /// <returns>Entidad MatrizRiesgoFactorNIF</returns>
        public MatrizRiesgoFactorNIF ConsultarMatrizRiesgoFactorNIF(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOMatrizRiesgoFactorNIF.ConsultarMatrizRiesgoFactorNIF(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizRiesgoFactorNIFService", "ConsultarMatrizRiesgoFactorNIF", ex);
                return null;
            }
        }



        /// <summary>
        /// Servicio para obtener lista de MatrizRiesgoFactorNIFs a partir de unos filtros
        /// </summary>
        /// <param name="pMatrizRiesgoFactorNIF">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MatrizRiesgoFactorNIF obtenidos</returns>
        public List<MatrizRiesgoFactorNIF> ListarMatrizRiesgoFactorNIF(MatrizRiesgoFactorNIF pMatrizRiesgoFactorNIF, Usuario pUsuario)
        {
            try
            {
                return BOMatrizRiesgoFactorNIF.ListarMatrizRiesgoFactorNIF(pMatrizRiesgoFactorNIF, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizRiesgoFactorNIFService", "ListarMatrizRiesgoFactorNIF", ex);
                return null;
            }
        }

    }
}