using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Seguridad.Data;
using Xpinn.Seguridad.Entities;

namespace Xpinn.Seguridad.Business
{
    /// <summary>
    /// Objeto de negocio para Modulo
    /// </summary>
    public class ModuloBusiness : GlobalBusiness
    {
        private ModuloData DAModulo;

        /// <summary>
        /// Constructor del objeto de negocio para Modulo
        /// </summary>
        public ModuloBusiness()
        {
            DAModulo = new ModuloData();
        }

        /// <summary>
        /// Crea un Modulo
        /// </summary>
        /// <param name="pModulo">Entidad Modulo</param>
        /// <returns>Entidad Modulo creada</returns>
        public Modulo CrearModulo(Modulo pModulo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pModulo = DAModulo.CrearModulo(pModulo, pUsuario);

                    ts.Complete();
                }

                return pModulo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ModuloBusiness", "CrearModulo", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Modulo
        /// </summary>
        /// <param name="pModulo">Entidad Modulo</param>
        /// <returns>Entidad Modulo modificada</returns>
        public Modulo ModificarModulo(Modulo pModulo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pModulo = DAModulo.ModificarModulo(pModulo, pUsuario);

                    ts.Complete();
                }

                return pModulo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ModuloBusiness", "ModificarModulo", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Modulo
        /// </summary>
        /// <param name="pId">Identificador de Modulo</param>
        public void EliminarModulo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAModulo.EliminarModulo(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ModuloBusiness", "EliminarModulo", ex);
            }
        }

        /// <summary>
        /// Obtiene un Modulo
        /// </summary>
        /// <param name="pId">Identificador de Modulo</param>
        /// <returns>Entidad Modulo</returns>
        public Modulo ConsultarModulo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAModulo.ConsultarModulo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ModuloBusiness", "ConsultarModulo", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pModulo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Modulo obtenidos</returns>
        public List<Modulo> ListarModulo(Modulo pModulo, Usuario pUsuario)
        {
            try
            {
                return DAModulo.ListarModulo(pModulo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ModuloBusiness", "ListarModulo", ex);
                return null;
            }
        }

    }
}