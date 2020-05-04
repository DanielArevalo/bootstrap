using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Business
{
    /// <summary>
    /// Objeto de negocio para oficina
    /// </summary>
    public class OficinaBusiness : GlobalData
    {
        private OficinaData DAOficina;

        /// <summary>
        /// Constructor del objeto de negocio para Oficina
        /// </summary>
        public OficinaBusiness()
        {
            DAOficina = new OficinaData();
        }

        /// <summary>
        /// Crea una Oficina
        /// </summary>
        /// <param name="pEntity">Entidad oficina</param>
        /// <returns>Entidad creada</returns>
        public Oficina CrearOficina(Oficina pOficina, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pOficina = DAOficina.InsertarOficina(pOficina, pUsuario);

                    ts.Complete();
                }

                return pOficina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaBusiness", "CrearOficina", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica una Oficina
        /// </summary>
        /// <param name="pEntity">Entidad Oficina</param>
        /// <returns>Entidad modificada</returns>
        public Oficina ModificarOficina(Oficina pOficina, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pOficina = DAOficina.ModificarOficina(pOficina, pUsuario);

                    ts.Complete();
                }

                return pOficina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaBusiness", "ModificarOficina", ex);
                return null;
            }

        }

        /// <summary>
        /// Elimina una Oficina
        /// </summary>
        /// <param name="pId">identificador de la oficina</param>
        public void EliminarOficina(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    DAOficina.EliminarOficina(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaBusiness", "EliminarOficina", ex);
            }
        }

        /// <summary>
        /// Obtiene una Oficina
        /// </summary>
        /// <param name="pId">identificador de la Oficina</param>
        /// <returns>Caja consultada</returns>
        public Oficina ConsultarOficina(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAOficina.ConsultarOficina(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaBusiness", "ConsultarOficina", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene consulta por una Oficina
        /// </summary>
        /// <param name="pId">identificador de la Oficina</param>
        /// <returns>Caja consultada</returns>
        public Oficina ConsultarXOficina(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAOficina.ConsultarXOficina(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaBusiness", "ConsultarXOficina", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Oficinas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Oficinas obtenidos</returns>
        public List<Oficina> ListarOficina(Oficina pOficina, Usuario pUsuario)
        {
            try
            {
                return DAOficina.ListarOficina(pOficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaBusiness", "ListarOficina", ex);
                return null;
            }
        }

        public List<Oficina> ListarOficina(Oficina pOficina, Usuario pUsuario, string filtro)
        {
            try
            {
                return DAOficina.ListarOficina(pOficina, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaBusiness", "ListarOficina", ex);
                return null;
            }
        }

        public Oficina ConsultarDireccionYCiudadDeUnaOficina(long codigoOficina, Usuario usuario)
        {
            try
            {
                return DAOficina.ConsultarDireccionYCiudadDeUnaOficina(codigoOficina, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaBusiness", "ConsultarDireccionYCiudadDeUnaOficina", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Oficinas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Oficinas obtenidos</returns>
        public List<Oficina> ListarOficinaUsuario(Oficina pOficina, Usuario pUsuario)
        {
            try
            {
                return DAOficina.ListarOficinaUsuario(pOficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaBusiness", "ListarOficinaUsuario", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene el conteo de usuarios asociados a la oficina especifica 
        /// </summary>
        /// <param name="pId">identificador de la Oficina</param>
        /// <returns>Caja consultada</returns>
        public Oficina ConsultarUsersXOficina(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAOficina.ConsultarUsersXOficina(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaBusiness", "ConsultarUsersXOficina", ex);
                return null;
            }
        }



    }
}
