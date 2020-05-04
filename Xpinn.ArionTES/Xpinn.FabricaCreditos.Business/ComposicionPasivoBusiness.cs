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
    /// Objeto de negocio para ComposicionPasivo
    /// </summary>
    public class ComposicionPasivoBusiness : GlobalData
    {
        private ComposicionPasivoData DAComposicionPasivo;

        /// <summary>
        /// Constructor del objeto de negocio para ComposicionPasivo
        /// </summary>
        public ComposicionPasivoBusiness()
        {
            DAComposicionPasivo = new ComposicionPasivoData();
        }

        /// <summary>
        /// Crea un ComposicionPasivo
        /// </summary>
        /// <param name="pComposicionPasivo">Entidad ComposicionPasivo</param>
        /// <returns>Entidad ComposicionPasivo creada</returns>
        public ComposicionPasivo CrearComposicionPasivo(ComposicionPasivo pComposicionPasivo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pComposicionPasivo = DAComposicionPasivo.CrearComposicionPasivo(pComposicionPasivo, pUsuario);

                    ts.Complete();
                }

                return pComposicionPasivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComposicionPasivoBusiness", "CrearComposicionPasivo", ex);
                return null;
            }
        }

        public ComposicionPasivo creaobligacion(ComposicionPasivo pComposicionPasivo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pComposicionPasivo = DAComposicionPasivo.creaobligacion(pComposicionPasivo, pUsuario);

                    ts.Complete();
                }

                return pComposicionPasivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComposicionPasivoBusiness", "CrearComposicionPasivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un ComposicionPasivo
        /// </summary>
        /// <param name="pComposicionPasivo">Entidad ComposicionPasivo</param>
        /// <returns>Entidad ComposicionPasivo modificada</returns>
        public ComposicionPasivo ModificarComposicionPasivo(ComposicionPasivo pComposicionPasivo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pComposicionPasivo = DAComposicionPasivo.ModificarComposicionPasivo(pComposicionPasivo, pUsuario);

                    ts.Complete();
                }

                return pComposicionPasivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComposicionPasivoBusiness", "ModificarComposicionPasivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un ComposicionPasivo
        /// </summary>
        /// <param name="pId">Identificador de ComposicionPasivo</param>
        public void EliminarComposicionPasivo(Int64 pId, Usuario pUsuario, Int64 Cod_persona, Int64 Cod_InfFin)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAComposicionPasivo.EliminarComposicionPasivo(pId, pUsuario, Cod_persona, Cod_InfFin);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComposicionPasivoBusiness", "EliminarComposicionPasivo", ex);
            }
        }

        /// <summary>
        /// Obtiene un ComposicionPasivo
        /// </summary>
        /// <param name="pId">Identificador de ComposicionPasivo</param>
        /// <returns>Entidad ComposicionPasivo</returns>
        public ComposicionPasivo ConsultarComposicionPasivo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAComposicionPasivo.ConsultarComposicionPasivo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComposicionPasivoBusiness", "ConsultarComposicionPasivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pComposicionPasivo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ComposicionPasivo obtenidos</returns>
        public List<ComposicionPasivo> ListarComposicionPasivo(ComposicionPasivo pComposicionPasivo, Usuario pUsuario)
        {
            try
            {
                return DAComposicionPasivo.ListarComposicionPasivo(pComposicionPasivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComposicionPasivoBusiness", "ListarComposicionPasivo", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pComposicionPasivo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ComposicionPasivo obtenidos</returns>
        public List<ComposicionPasivo> ListarComposicionPasivoRepo(ComposicionPasivo pComposicionPasivo, Usuario pUsuario)
        {
            try
            {
                return DAComposicionPasivo.ListarComposicionPasivoRepo(pComposicionPasivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComposicionPasivoBusiness", "ListarComposicionPasivoRepo", ex);
                return null;
            }
        }

        public List<ComposicionPasivo> Listarobligacion(long infin, Usuario pUsuario)
        {
            try
            {
                return DAComposicionPasivo.Listarobligacion(infin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComposicionPasivoService", "ListarComposicionPasivo", ex);
                return null;
            }
        }
    }
}