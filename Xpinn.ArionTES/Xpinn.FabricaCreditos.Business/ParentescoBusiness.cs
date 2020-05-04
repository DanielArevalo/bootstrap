using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    /// <summary>
    /// Objeto de negocio para Parentesco
    /// </summary>
    public class ParentescoBusiness : GlobalData
    {
        private ParentescoData DAParentesco;

        /// <summary>
        /// Constructor del objeto de negocio para Parentesco
        /// </summary>
        public ParentescoBusiness()
        {
            DAParentesco = new ParentescoData();
        }

        /// <summary>
        /// Crea un Parentesco
        /// </summary>
        /// <param name="pParentesco">Entidad Parentesco</param>
        /// <returns>Entidad Parentesco creada</returns>
        public Parentesco CrearParentesco(Parentesco pParentesco, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pParentesco = DAParentesco.CrearParentesco(pParentesco, pUsuario);

                    ts.Complete();
                }

                return pParentesco;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParentescoBusiness", "CrearParentesco", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Parentesco
        /// </summary>
        /// <param name="pParentesco">Entidad Parentesco</param>
        /// <returns>Entidad Parentesco modificada</returns>
        public Parentesco ModificarParentesco(Parentesco pParentesco, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pParentesco = DAParentesco.ModificarParentesco(pParentesco, pUsuario);

                    ts.Complete();
                }

                return pParentesco;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParentescoBusiness", "ModificarParentesco", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Parentesco
        /// </summary>
        /// <param name="pId">Identificador de Parentesco</param>
        public void EliminarParentesco(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAParentesco.EliminarParentesco(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParentescoBusiness", "EliminarParentesco", ex);
            }
        }

        /// <summary>
        /// Obtiene un Parentesco
        /// </summary>
        /// <param name="pId">Identificador de Parentesco</param>
        /// <returns>Entidad Parentesco</returns>
        public Parentesco ConsultarParentesco(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAParentesco.ConsultarParentesco(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParentescoBusiness", "ConsultarParentesco", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pParentesco">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Parentesco obtenidos</returns>
        public List<Parentesco> ListarParentesco(Parentesco pParentesco, Usuario pUsuario)
        {
            try
            {
                return DAParentesco.ListarParentesco(pParentesco, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParentescoBusiness", "ListarParentesco", ex);
                return null;
            }
        }

    }
}