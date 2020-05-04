using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Seguridad.Business;
using Xpinn.Seguridad.Entities;

namespace Xpinn.Seguridad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class OpcionService
    {
        private OpcionBusiness BOOpcion;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Opcion
        /// </summary>
        public OpcionService()
        {
            BOOpcion = new OpcionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "90103"; } }
        public string CodigoProgramaBio { get { return "90109"; } }
        public string CodigoProgramaGenerales { get { return "90110"; } }

        /// <summary>
        /// Servicio para crear Opcion
        /// </summary>
        /// <param name="pEntity">Entidad Opcion</param>
        /// <returns>Entidad Opcion creada</returns>
        public Opcion CrearOpcion(Opcion pOpcion, Usuario pUsuario)
        {
            try
            {
                return BOOpcion.CrearOpcion(pOpcion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OpcionService", "CrearOpcion", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Opcion
        /// </summary>
        /// <param name="pOpcion">Entidad Opcion</param>
        /// <returns>Entidad Opcion modificada</returns>
        public Opcion ModificarOpcion(Opcion pOpcion, Usuario pUsuario)
        {
            try
            {
                return BOOpcion.ModificarOpcion(pOpcion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OpcionService", "ModificarOpcion", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Opcion
        /// </summary>
        /// <param name="pId">identificador de Opcion</param>
        public void EliminarOpcion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOOpcion.EliminarOpcion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarOpcion", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Opcion
        /// </summary>
        /// <param name="pId">identificador de Opcion</param>
        /// <returns>Entidad Opcion</returns>
        public Opcion ConsultarOpcion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOOpcion.ConsultarOpcion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OpcionService", "ConsultarOpcion", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Opcions a partir de unos filtros
        /// </summary>
        /// <param name="pOpcion">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Opcion obtenidos</returns>
        public List<Opcion> ListarOpcion(Opcion pOpcion, Usuario pUsuario)
        {
            try
            {
                return BOOpcion.ListarOpcion(pOpcion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OpcionService", "ListarOpcion", ex);
                return null;
            }
        }



        public Opcion Modificargeneral(Opcion pgeneral, Usuario pUsuario)
        {
            try
            {
                return BOOpcion.Modificargeneral(pgeneral,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OpcionBusiness", "ListarOpcion", ex);
                return null;
            }
        }

        public List<Opcion> ListarOpcionesGeneral(string filtro,Usuario pUsuario)
        {
            try
            {
                return BOOpcion.ListarOpcionesGeneral(filtro,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OpcionBusiness", "ListarOpcion", ex);
                return null;
            }
        }

        public List<Opcion> ListarOpciones(Usuario pUsuario)
        {
            try
            {
                return BOOpcion.ListarOpciones(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OpcionBusiness", "ListarOpcion", ex);
                return null;
            }
        }
    }
}
