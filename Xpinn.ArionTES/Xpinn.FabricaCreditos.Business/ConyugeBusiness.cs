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
    /// Objeto de negocio para Conyuge
    /// </summary>
    public class ConyugeBusiness : GlobalData
    {
        private ConyugeData DAConyuge;

        /// <summary>
        /// Constructor del objeto de negocio para Conyuge
        /// </summary>
        public ConyugeBusiness()
        {
            DAConyuge = new ConyugeData();
        }

        /// <summary>
        /// Crea un Conyuge
        /// </summary>
        /// <param name="pConyuge">Entidad Conyuge</param>
        /// <returns>Entidad Conyuge creada</returns>
        public Conyuge CrearConyuge(Conyuge pConyuge, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pConyuge = DAConyuge.CrearConyuge(pConyuge, pUsuario);

                    ts.Complete();
                }

                return pConyuge;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConyugeBusiness", "CrearConyuge", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Conyuge
        /// </summary>
        /// <param name="pConyuge">Entidad Conyuge</param>
        /// <returns>Entidad Conyuge modificada</returns>
        public Conyuge ModificarConyuge(Conyuge pConyuge, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pConyuge = DAConyuge.ModificarConyuge(pConyuge, pUsuario);

                    ts.Complete();
                }

                return pConyuge;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConyugeBusiness", "ModificarConyuge", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Conyuge
        /// </summary>
        /// <param name="pId">Identificador de Conyuge</param>
        public void EliminarConyuge(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAConyuge.EliminarConyuge(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConyugeBusiness", "EliminarConyuge", ex);
            }
        }

        /// <summary>
        /// Obtiene un Conyuge
        /// </summary>
        /// <param name="pId">Identificador de Conyuge</param>
        /// <returns>Entidad Conyuge</returns>
        public Conyuge ConsultarConyuge(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAConyuge.ConsultarConyuge(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConyugeBusiness", "ConsultarConyuge", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pConyuge">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Conyuge obtenidos</returns>
        public List<Conyuge> ListarConyuge(Conyuge pConyuge, Usuario pUsuario)
        {
            try
            {
                return DAConyuge.ListarConyuge(pConyuge, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConyugeBusiness", "ListarConyuge", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene un Conyuge
        /// </summary>
        /// <param name="pId">Identificador de Conyuge</param>
        /// <returns>Entidad Conyuge</returns>
        public Conyuge ConsultarRefConyuge(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAConyuge.ConsultarRefConyuge(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConyugeBusiness", "ConsultarConyuge", ex);
                return null;
            }
        }

        /// Obtiene un Conyuge
        /// </summary>
        /// <param name="pId">Identificador de Conyuge</param>
        /// <returns>Entidad Conyuge</returns>
        public Conyuge ConsultarRefConyugeRepo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAConyuge.ConsultarRefConyugeRepo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConyugeBusiness", "ConsultarRefConyugeRepo", ex);
                return null;
            }
        }

      
        
    }
}