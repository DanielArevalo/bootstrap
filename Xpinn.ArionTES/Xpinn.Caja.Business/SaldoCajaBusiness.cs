using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Business
{
    /// <summary>
    /// Objeto de negocio para SaldoCaja
    /// </summary>
    public class SaldoCajaBusiness : GlobalBusiness
    {
        private SaldoCajaData DASaldoCaja;

        /// <summary>
        /// Constructor del objeto de negocio para SaldoCaja
        /// </summary>
        public SaldoCajaBusiness()
        {
            DASaldoCaja = new SaldoCajaData();
        }

        /// <summary>
        /// Crea un SaldoCaja
        /// </summary>
        /// <param name="pSaldoCaja">Entidad SaldoCaja</param>
        /// <returns>Entidad SaldoCaja creada</returns>
        public SaldoCaja CrearSaldoCaja(SaldoCaja pSaldoCaja, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSaldoCaja = DASaldoCaja.CrearSaldoCaja(pSaldoCaja, pUsuario);

                    ts.Complete();
                }

                return pSaldoCaja;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SaldoCajaBusiness", "CrearSaldoCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un SaldoCaja
        /// </summary>
        /// <param name="pSaldoCaja">Entidad SaldoCaja</param>
        /// <returns>Entidad SaldoCaja modificada</returns>
        public SaldoCaja ModificarSaldoCaja(SaldoCaja pSaldoCaja, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSaldoCaja = DASaldoCaja.ModificarSaldoCaja(pSaldoCaja, pUsuario);

                    ts.Complete();
                }

                return pSaldoCaja;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SaldoCajaBusiness", "ModificarSaldoCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un SaldoCaja
        /// </summary>
        /// <param name="pId">Identificador de SaldoCaja</param>
        public void EliminarSaldoCaja(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DASaldoCaja.EliminarSaldoCaja(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SaldoCajaBusiness", "EliminarSaldoCaja", ex);
            }
        }

        /// <summary>
        /// Obtiene un SaldoCaja
        /// </summary>
        /// <param name="pId">Identificador de SaldoCaja</param>
        /// <returns>Entidad SaldoCaja</returns>
        public SaldoCaja ConsultarSaldoCaja(SaldoCaja pSaldoCaja, Usuario pUsuario)
        {
            try
            {
                return DASaldoCaja.ConsultarSaldoCaja(pSaldoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SaldoCajaBusiness", "ConsultarSaldoCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pSaldoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de SaldoCaja obtenidos</returns>
        public List<SaldoCaja> ListarSaldoCaja(SaldoCaja pSaldoCaja, Usuario pUsuario)
        {
            try
            {
                return DASaldoCaja.ListarSaldoCaja(pSaldoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SaldoCajaBusiness", "ListarSaldoCaja", ex);
                return null;
            }
        }

        public SaldoCaja ConsultarSaldoTesoreriaConsig(SaldoCaja saldo, Usuario usuario)
        {
            try
            {
                return DASaldoCaja.ConsultarSaldoTesoreriaConsig(saldo, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SaldoCajaBusiness", "ConsultarSaldoTesoreriaConsig", ex);
                return null;
            }
        }
    }
}