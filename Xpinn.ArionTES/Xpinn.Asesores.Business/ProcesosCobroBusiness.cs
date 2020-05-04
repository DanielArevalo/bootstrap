using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Asesores.Data;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Business
{
    /// <summary>
    /// Objeto de negocio para ProcesosCobro
    /// </summary>
    public class ProcesosCobroBusiness : GlobalBusiness
    {
        private ProcesosCobroData DAProcesosCobro;

        /// <summary>
        /// Constructor del objeto de negocio para ProcesosCobro
        /// </summary>
        public ProcesosCobroBusiness()
        {
            DAProcesosCobro = new ProcesosCobroData();
        }

        /// <summary>
        /// Crea un ProcesosCobro
        /// </summary>
        /// <param name="pProcesosCobro">Entidad ProcesosCobro</param>
        /// <returns>Entidad ProcesosCobro creada</returns>
        public ProcesosCobro CrearProcesosCobro(ProcesosCobro pProcesosCobro, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProcesosCobro = DAProcesosCobro.CrearProcesosCobro(pProcesosCobro, pUsuario);

                    ts.Complete();
                }

                return pProcesosCobro;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosCobroBusiness", "CrearProcesosCobro", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un ProcesosCobro
        /// </summary>
        /// <param name="pProcesosCobro">Entidad ProcesosCobro</param>
        /// <returns>Entidad ProcesosCobro modificada</returns>
        public ProcesosCobro ModificarProcesosCobro(ProcesosCobro pProcesosCobro, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProcesosCobro = DAProcesosCobro.ModificarProcesosCobro(pProcesosCobro, pUsuario);

                    ts.Complete();
                }

                return pProcesosCobro;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosCobroBusiness", "ModificarProcesosCobro", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un ProcesosCobro
        /// </summary>
        /// <param name="pId">Identificador de ProcesosCobro</param>
        public void EliminarProcesosCobro(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAProcesosCobro.EliminarProcesosCobro(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosCobroBusiness", "EliminarProcesosCobro", ex);
            }
        }

        /// <summary>
        /// Obtiene un ProcesosCobro
        /// </summary>
        /// <param name="pId">Identificador de ProcesosCobro</param>
        /// <returns>Entidad ProcesosCobro</returns>
        public ProcesosCobro ConsultarProcesosCobro(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAProcesosCobro.ConsultarProcesosCobro(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosCobroBusiness", "ConsultarProcesosCobro", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un ProcesosCobro
        /// </summary>
        /// <param name="pId">Identificador de ProcesosCobro</param>
        /// <returns>Entidad ProcesosCobro</returns>
        public ProcesosCobro ConsultarDatosProceso(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAProcesosCobro.ConsultarDatosProceso(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosCobroBusiness", "ConsultarDatosProceso", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene un ProcesosCobro
        /// </summary>
        /// <param name="pId">Identificador de ProcesosCobro</param>
        /// <returns>Entidad ProcesosCobro</returns>
        public ProcesosCobro ConsultarDatosProcesoAbogados(Usuario pUsuario)
        {
            try
            {
                return DAProcesosCobro.ConsultarProcesosCobroAbogados(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosCobroBusiness", "ConsultarDatosProceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pProcesosCobro">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ProcesosCobro obtenidos</returns>
        public List<ProcesosCobro> ListarProcesosCobro(ProcesosCobro pProcesosCobro, Usuario pUsuario)
        {
            try
            {
                return DAProcesosCobro.ListarProcesosCobro(pProcesosCobro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosCobroBusiness", "ListarProcesosCobro", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pProcesosCobro">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ProcesosCobro obtenidos</returns>
        public List<ProcesosCobro> ListarProcesosCobroSiguientes(ProcesosCobro pProcesosCobro, Usuario pUsuario)
        {
            try
            {
                return DAProcesosCobro.ListarProcesosCobroSiguientes(pProcesosCobro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosCobroBusiness", "ListarProcesosCobro", ex);
                return null;
            }
        }

    }
}