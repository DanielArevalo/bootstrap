using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Business
{
    /// <summary>
    /// Objeto de negocio para ACHcampo
    /// </summary>
    public class ACHcampoBusiness : GlobalBusiness
    {
        private ACHcampoData DAACHcampo;

        /// <summary>
        /// Constructor del objeto de negocio para ACHcampo
        /// </summary>
        public ACHcampoBusiness()
        {
            DAACHcampo = new ACHcampoData();
        }

        /// <summary>
        /// Crea un ACHcampo
        /// </summary>
        /// <param name="pACHcampo">Entidad ACHcampo</param>
        /// <returns>Entidad ACHcampo creada</returns>
        public ACHcampo CrearACHcampo(ACHcampo pACHcampo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pACHcampo = DAACHcampo.CrearACHcampo(pACHcampo, pUsuario);

                    ts.Complete();
                }

                return pACHcampo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHcampoBusiness", "CrearACHcampo", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un ACHcampo
        /// </summary>
        /// <param name="pACHcampo">Entidad ACHcampo</param>
        /// <returns>Entidad ACHcampo modificada</returns>
        public ACHcampo ModificarACHcampo(ACHcampo pACHcampo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pACHcampo = DAACHcampo.ModificarACHcampo(pACHcampo, pUsuario);

                    ts.Complete();
                }

                return pACHcampo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHcampoBusiness", "ModificarACHcampo", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un ACHcampo
        /// </summary>
        /// <param name="pId">Identificador de ACHcampo</param>
        public void EliminarACHcampo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAACHcampo.EliminarACHcampo(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHcampoBusiness", "EliminarACHcampo", ex);
            }
        }

        /// <summary>
        /// Obtiene un ACHcampo
        /// </summary>
        /// <param name="pId">Identificador de ACHcampo</param>
        /// <returns>Entidad ACHcampo</returns>
        public ACHcampo ConsultarACHcampo(Int64 pId, Usuario vUsuario)
        {
            try
            {
                ACHcampo pACHcampo = new ACHcampo();

                pACHcampo = DAACHcampo.ConsultarACHcampo(pId, vUsuario);

                return pACHcampo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHcampoBusiness", "ConsultarACHcampo", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pACHcampo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ACHcampo obtenidos</returns>
        public List<ACHcampo> ListarACHcampo(ACHcampo pACHcampo, Usuario pUsuario)
        {
            try
            {
                return DAACHcampo.ListarACHcampo(pACHcampo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHcampoBusiness", "ListarACHcampo", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DAACHcampo.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHcampoBusiness", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }


    }
}