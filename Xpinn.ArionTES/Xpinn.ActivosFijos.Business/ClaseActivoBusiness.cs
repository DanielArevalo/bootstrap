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
    /// Objeto de negocio para ClaseActivo
    /// </summary>
    public class ClaseActivoBusiness : GlobalBusiness
    {
        private ClaseActivoData DAClaseActivo;

        /// <summary>
        /// Constructor del objeto de negocio para ClaseActivo
        /// </summary>
        public ClaseActivoBusiness()
        {
            DAClaseActivo = new ClaseActivoData();
        }

        /// <summary>
        /// Crea un ActivosFijos
        /// </summary>
        /// <param name="pActivosFijos">Entidad ActivosFijos</param>
        /// <returns>Entidad ClaseActivo creada</returns>
        public ClaseActivo CrearClaseActivo(ClaseActivo pClaseActivo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pClaseActivo = DAClaseActivo.CrearClaseActivo(pClaseActivo, pUsuario);

                    ts.Complete();
                }

                return pClaseActivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClaseActivoBusiness", "CrearActivosFijos", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un ClaseActivo
        /// </summary>
        /// <param name="pActivosFijos">Entidad ActivosFijos</param>
        /// <returns>Entidad ClaseActivo modificada</returns>
        public ClaseActivo ModificarClaseActivo(ClaseActivo pClaseActivo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pClaseActivo = DAClaseActivo.ModificarClaseActivo(pClaseActivo, pUsuario);

                    ts.Complete();
                }

                return pClaseActivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClaseActivoBusiness", "ModificarActivosFijos", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un ClaseActivo
        /// </summary>
        /// <param name="pId">Identificador de ClaseActivo</param>
        public void EliminarClaseActivo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAClaseActivo.EliminarClaseActivo(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClaseActivoBusiness", "EliminarActivosFijos", ex);
            }
        }

        /// <summary>
        /// Obtiene un ActivosFijos
        /// </summary>
        /// <param name="pId">Identificador de ActivosFijos</param>
        /// <returns>Entidad ActivosFijos</returns>
        public ClaseActivo ConsultarClaseActivo(Int64 pId, Usuario vUsuario)
        {
            try
            {
                ClaseActivo ClaseActivo = new ClaseActivo();

                ClaseActivo = DAClaseActivo.ConsultarClaseActivo(pId, vUsuario);

                return ClaseActivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClaseActivoBusiness", "ConsultarActivosFijos", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pActivosFijos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ActivosFijos obtenidos</returns>
        public List<ClaseActivo> ListarClaseActivo(ClaseActivo pClaseActivo, Usuario pUsuario)
        {
            try
            {
                return DAClaseActivo.ListarClaseActivo(pClaseActivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClaseActivoBusiness", "ListarActivosFijos", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DAClaseActivo.ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 0;
            }
        }


        //AGREGADO

        public List<ClaseActivo> ListarClasificacion(ClaseActivo pClaseActivo, Usuario pUsuario)
        {
            try
            {
                return DAClaseActivo.ListarClasificacion(pClaseActivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClaseActivoBusiness", "ListarClasificacion", ex);
                return null;
            }
        }

    }
}