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
    //Se cambia de acuerdo a la entidad

    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CiudadService
    {
        private CiudadBusiness BOCiudad;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Ciudad
        /// </summary>
        public CiudadService()
        {
            BOCiudad = new CiudadBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "10101"; } }

        /// <summary>
        /// Servicio para crear Ciudad
        /// </summary>
        /// <param name="pEntity">Entidad Ciudad</param>
        /// <returns>Entidad Ciudad creada</returns>
        public Ciudad CrearCiudad(Ciudad pCiudad, Usuario pUsuario)
        {
            try
            {
                return BOCiudad.CrearCiudad(pCiudad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadService", "CrearCiudad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Ciudad
        /// </summary>
        /// <param name="pCiudad">Entidad Ciudad</param>
        /// <returns>Entidad Ciudad modificada</returns>
        public Ciudad ModificarCiudad(Ciudad pCiudad, Usuario pUsuario)
        {
            try
            {
                return BOCiudad.ModificarCiudad(pCiudad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadService", "ModificarCiudad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Ciudad
        /// </summary>
        /// <param name="pId">identificador de Ciudad</param>
        public void EliminarCiudad(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOCiudad.EliminarCiudad(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarCiudad", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Ciudad
        /// </summary>
        /// <param name="pId">identificador de Ciudad</param>
        /// <returns>Entidad Ciudad</returns>
        public Ciudad ConsultarCiudad(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCiudad.ConsultarCiudad(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadService", "ConsultarCiudad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Ciudades a partir de unos filtros
        /// </summary>
        /// <param name="pCiudad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Ciudad obtenidos</returns>
        public List<Ciudad> ListarCiudad(Ciudad pCiudad, Usuario pUsuario)
        {
            try
            {
                return BOCiudad.ListarCiudad(pCiudad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadService", "ListarCiudad", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de Ciudads para Recuperaciones a partir de unos filtros
        /// </summary>
        /// <param name="pCiudad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Ciudad obtenidos</returns>
        public List<Ciudad> ListarCiudadRecuperaciones(Ciudad pCiudad, Usuario pUsuario)
        {
            try
            {
                return BOCiudad.ListarCiudadRecuperaciones(pCiudad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadService", "ListarCiudadRecuperaciones", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Ciudades_juzgados para Recuperaciones a partir de unos filtros
        /// </summary>
        /// <param name="pCiudad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Ciudad obtenidos</returns>
        public List<Ciudad> ListarCiudadjuzgados( Usuario pUsuario)
        {
            try
            {
                return BOCiudad.ListarCiudadjuzgados( pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadService", "ListarCiudadjuzgados", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de Ciudads para Recuperaciones a partir de unos filtros
        /// </summary>
        /// <param name="pCiudad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Ciudad obtenidos</returns>
        public List<Ciudad> ListarZonaRecXAsesor(Ciudad pCiudad, Usuario pUsuario)
        {
            try
            {
                return BOCiudad.ListarZonaRecXAsesor(pCiudad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadService", "ListarZonaRecXAsesor", ex);
                return null;
            }
        }

           public List<Ciudad> ListarZonas( Usuario pUsuario)
        {
            try
            {
                return BOCiudad.ListarZonas(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadService", "ListarZonaS", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Ciudads a partir de unos filtros
        /// </summary>
        /// <param name="pCiudad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Ciudad obtenidos</returns>
        public List<Ciudad> ListarZonasxOficina(Int64 idOficina, Usuario pUsuario)
        {
            try
            {
                return BOCiudad.ListarZonasxOficina(idOficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadService", "ListarZonasxOficina", ex);
                return null;
            }
        }
    }
}