using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Presupuesto.Data;
using Xpinn.Presupuesto.Entities;

namespace Xpinn.Presupuesto.Business
{
    /// <summary>
    /// Objeto de negocio para TipoPresupuesto
    /// </summary>
    public class TipoPresupuestoBusiness : GlobalBusiness
    {
        private Xpinn.Presupuesto.Data.TipoPresupuestoData DATipoPresupuesto;

        /// <summary>
        /// Constructor del objeto de negocio para TipoPresupuesto
        /// </summary>
        public TipoPresupuestoBusiness()
        {
            DATipoPresupuesto = new TipoPresupuestoData();
        }

        /// <summary>
        /// Crea un TipoPresupuesto
        /// </summary>
        /// <param name="pTipoPresupuesto">Entidad TipoPresupuesto</param>
        /// <returns>Entidad TipoPresupuesto creada</returns>
        public Xpinn.Presupuesto.Entities.TipoPresupuesto CrearTipoPresupuesto(Xpinn.Presupuesto.Entities.TipoPresupuesto pTipoPresupuesto, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoPresupuesto = DATipoPresupuesto.CrearTipoPresupuesto(pTipoPresupuesto, vusuario);

                    ts.Complete();
                }

                return pTipoPresupuesto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoPresupuestoBusiness", "CrearTipoPresupuesto", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un TipoPresupuesto
        /// </summary>
        /// <param name="pTipoPresupuesto">Entidad TipoPresupuesto</param>
        /// <returns>Entidad TipoPresupuesto modificada</returns>
        public Xpinn.Presupuesto.Entities.TipoPresupuesto ModificarTipoPresupuesto(Xpinn.Presupuesto.Entities.TipoPresupuesto pTipoPresupuesto, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoPresupuesto = DATipoPresupuesto.ModificarTipoPresupuesto(pTipoPresupuesto, vusuario);

                    ts.Complete();
                }

                return pTipoPresupuesto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoPresupuestoBusiness", "ModificarTipoPresupuesto", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un TipoPresupuesto
        /// </summary>
        /// <param name="pId">Identificador de TipoPresupuesto</param>
        public void EliminarTipoPresupuesto(Int64 pId, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATipoPresupuesto.EliminarTipoPresupuesto(pId, vusuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoPresupuestoBusiness", "EliminarTipoPresupuesto", ex);
            }
        }

        /// <summary>
        /// Obtiene un TipoPresupuesto
        /// </summary>
        /// <param name="pId">Identificador de TipoPresupuesto</param>
        /// <returns>Entidad TipoPresupuesto</returns>
        public Xpinn.Presupuesto.Entities.TipoPresupuesto ConsultarTipoPresupuesto(Int64 pId, Usuario vusuario)
        {
            try
            {
                Xpinn.Presupuesto.Entities.TipoPresupuesto TipoPresupuesto = new Xpinn.Presupuesto.Entities.TipoPresupuesto();

                TipoPresupuesto = DATipoPresupuesto.ConsultarTipoPresupuesto(pId, vusuario);

                return TipoPresupuesto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoPresupuestoBusiness", "ConsultarTipoPresupuesto", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTipoPresupuesto">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoPresupuesto obtenidos</returns>
        public List<Xpinn.Presupuesto.Entities.TipoPresupuesto> ListarTipoPresupuesto(Xpinn.Presupuesto.Entities.TipoPresupuesto pTipoPresupuesto, Usuario vUsuario)
        {
            try
            {
                return DATipoPresupuesto.ListarTipoPresupuesto(pTipoPresupuesto, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoPresupuestoBusiness", "ListarTipoPresupuesto", ex);
                return null;
            }
        }

    }
}