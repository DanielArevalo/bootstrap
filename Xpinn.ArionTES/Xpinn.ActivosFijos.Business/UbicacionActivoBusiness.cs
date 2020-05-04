using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.ActivosFijos.Data;
using Xpinn.ActivosFijos.Entities;

namespace Xpinn.ActivosFijos.Business
{
    /// <summary>
    /// Objeto de negocio para UbicacionActivo
    /// </summary>
    public class UbicacionActivoBusiness : GlobalBusiness
    {
        private UbicacionActivoData DAUbicacionActivo;

        /// <summary>
        /// Constructor del objeto de negocio para UbicacionActivo
        /// </summary>
        public UbicacionActivoBusiness()
        {
            DAUbicacionActivo = new UbicacionActivoData();
        }

        /// <summary>
        /// Crea un UbicacionActivo
        /// </summary>
        /// <param name="pUbicacionActivo">Entidad UbicacionActivo</param>
        /// <returns>Entidad UbicacionActivo creada</returns>
        public UbicacionActivo CrearUbicacionActivo(UbicacionActivo pUbicacionActivo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pUbicacionActivo = DAUbicacionActivo.CrearUbicacionActivo(pUbicacionActivo, pUsuario);

                    ts.Complete();
                }

                return pUbicacionActivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UbicacionActivoBusiness", "CrearUbicacionActivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un UbicacionActivo
        /// </summary>
        /// <param name="pUbicacionActivo">Entidad UbicacionActivo</param>
        /// <returns>Entidad UbicacionActivo modificada</returns>
        public UbicacionActivo ModificarUbicacionActivo(UbicacionActivo pUbicacionActivo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pUbicacionActivo = DAUbicacionActivo.ModificarUbicacionActivo(pUbicacionActivo, pUsuario);

                    ts.Complete();
                }

                return pUbicacionActivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UbicacionActivoBusiness", "ModificarUbicacionActivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un UbicacionActivo
        /// </summary>
        /// <param name="pId">Identificador de UbicacionActivo</param>
        public void EliminarUbicacionActivo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAUbicacionActivo.EliminarUbicacionActivo(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UbicacionActivoBusiness", "EliminarUbicacionActivo", ex);
            }
        }

        /// <summary>
        /// Obtiene un UbicacionActivo
        /// </summary>
        /// <param name="pId">Identificador de UbicacionActivo</param>
        /// <returns>Entidad UbicacionActivo</returns>
        public UbicacionActivo ConsultarUbicacionActivo(Int64 pId, Usuario vUsuario)
        {
            try
            {
                UbicacionActivo UbicacionActivo = new UbicacionActivo();

                UbicacionActivo = DAUbicacionActivo.ConsultarUbicacionActivo(pId, vUsuario);

                return UbicacionActivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UbicacionActivoBusiness", "ConsultarUbicacionActivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pUbicacionActivo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de UbicacionActivo obtenidos</returns>
        public List<UbicacionActivo> ListarUbicacionActivo(UbicacionActivo pUbicacionActivo, Usuario pUsuario)
        {
            try
            {
                return DAUbicacionActivo.ListarUbicacionActivo(pUbicacionActivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UbicacionActivoBusiness", "ListarUbicacionActivo", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DAUbicacionActivo.ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 0;
            }
        }

    }
}