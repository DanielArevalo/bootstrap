using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Asesores.Data;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Business
{
    //Se cambia de acuerdo a la entidad
    /// <summary>
    /// Objeto de negocio para Ciudad
    /// </summary>
    public class CiudadBusiness : GlobalBusiness
    {
        private CiudadData DACiudad;

        /// <summary>
        /// Constructor del objeto de negocio para Ciudad
        /// </summary>
        public CiudadBusiness()
        {
            DACiudad = new CiudadData();
        }

        /// <summary>
        /// Crea un Ciudad
        /// </summary>
        /// <param name="pCiudad">Entidad Ciudad</param>
        /// <returns>Entidad Ciudad creada</returns>
        public Ciudad CrearCiudad(Ciudad pCiudad, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCiudad = DACiudad.CrearCiudad(pCiudad, pUsuario);

                    ts.Complete();
                }

                return pCiudad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadBusiness", "CrearCiudad", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Ciudad
        /// </summary>
        /// <param name="pCiudad">Entidad Ciudad</param>
        /// <returns>Entidad Ciudad modificada</returns>
        public Ciudad ModificarCiudad(Ciudad pCiudad, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCiudad = DACiudad.ModificarCiudad(pCiudad, pUsuario);

                    ts.Complete();
                }

                return pCiudad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadBusiness", "ModificarCiudad", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Ciudad
        /// </summary>
        /// <param name="pId">Identificador de Ciudad</param>
        public void EliminarCiudad(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACiudad.EliminarCiudad(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadBusiness", "EliminarCiudad", ex);
            }
        }

        /// <summary>
        /// Obtiene un Ciudad
        /// </summary>
        /// <param name="pId">Identificador de Ciudad</param>
        /// <returns>Entidad Ciudad</returns>
        public Ciudad ConsultarCiudad(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DACiudad.ConsultarCiudad(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadBusiness", "ConsultarCiudad", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCiudad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Ciudad obtenidos</returns>
        public List<Ciudad> ListarCiudad(Ciudad pCiudad, Usuario pUsuario)
        {
            try
            {
                return DACiudad.ListarCiudad(pCiudad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadBusiness", "ListarCiudad", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Ciudad Para Recuperaciones dados unos filtros
        /// </summary>
        /// <param name="pCiudad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Ciudad obtenidos</returns>
        public List<Ciudad> ListarCiudadRecuperaciones(Ciudad pCiudad, Usuario pUsuario)
        {
            try
            {
                return DACiudad.ListarCiudadRecuperaciones(pCiudad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadBusiness", "ListarCiudadRecuperaciones", ex);
                return null;
            }
        }

        /// Obtiene la lista de Ciudades_juzgados Para Recuperaciones dados unos filtros
        /// </summary>
        /// <param name="pCiudad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Ciudad obtenidos</returns>
        public List<Ciudad> ListarCiudadjuzgados( Usuario pUsuario)
        {
            try
            {
                return DACiudad.ListarCiudadjuzgados(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadBusiness", "ListarCiudadjuzgados", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCiudad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Ciudad obtenidos</returns>
        public List<Ciudad> ListarZonasxOficina(Int64 idOficina, Usuario pUsuario)
        {
            try
            {
                return DACiudad.ListarZonasxOficina(idOficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadBusiness", "ListarZonasxOficina", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Ciudad Para Recuperaciones dados unos filtros
        /// </summary>
        /// <param name="pCiudad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Ciudad obtenidos</returns>
        public List<Ciudad> ListarZonaRecXAsesor(Ciudad pCiudad, Usuario pUsuario)
        {
            try
            {
                return DACiudad.ListarZonaRecupXAsesor(pCiudad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadBusiness", "ListarZonaRecupXAsesor", ex);
                return null;
            }
        }

        public List<Ciudad> ListarZonas( Usuario pUsuario)
        {
            try
            {
                return DACiudad.ListarZonas( pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadBusiness", "ListarZonas", ex);
                return null;
            }
        }
    }
}