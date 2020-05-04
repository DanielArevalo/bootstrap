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
    /// Objeto de negocio para ViabilidadFinanciera
    /// </summary>
    public class ViabilidadFinancieraBusiness : GlobalData
    {
        private ViabilidadFinancieraData DAViabilidadFinanciera;

        /// <summary>
        /// Constructor del objeto de negocio para ViabilidadFinanciera
        /// </summary>
        public ViabilidadFinancieraBusiness()
        {
            DAViabilidadFinanciera = new ViabilidadFinancieraData();
        }

        /// <summary>
        /// Crea un ViabilidadFinanciera
        /// </summary>
        /// <param name="pViabilidadFinanciera">Entidad ViabilidadFinanciera</param>
        /// <returns>Entidad ViabilidadFinanciera creada</returns>
        public ViabilidadFinanciera CrearViabilidadFinanciera(ViabilidadFinanciera pViabilidadFinanciera, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pViabilidadFinanciera = DAViabilidadFinanciera.CrearViabilidadFinanciera(pViabilidadFinanciera, pUsuario);

                    ts.Complete();
                }

                return pViabilidadFinanciera;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ViabilidadFinancieraBusiness", "CrearViabilidadFinanciera", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un ViabilidadFinanciera
        /// </summary>
        /// <param name="pViabilidadFinanciera">Entidad ViabilidadFinanciera</param>
        /// <returns>Entidad ViabilidadFinanciera modificada</returns>
        public ViabilidadFinanciera ModificarViabilidadFinanciera(ViabilidadFinanciera pViabilidadFinanciera, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pViabilidadFinanciera = DAViabilidadFinanciera.ModificarViabilidadFinanciera(pViabilidadFinanciera, pUsuario);

                    ts.Complete();
                }

                return pViabilidadFinanciera;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ViabilidadFinancieraBusiness", "ModificarViabilidadFinanciera", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un ViabilidadFinanciera
        /// </summary>
        /// <param name="pId">Identificador de ViabilidadFinanciera</param>
        public void EliminarViabilidadFinanciera(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAViabilidadFinanciera.EliminarViabilidadFinanciera(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ViabilidadFinancieraBusiness", "EliminarViabilidadFinanciera", ex);
            }
        }

        /// <summary>
        /// Obtiene un ViabilidadFinanciera
        /// </summary>
        /// <param name="pId">Identificador de ViabilidadFinanciera</param>
        /// <returns>Entidad ViabilidadFinanciera</returns>
        public ViabilidadFinanciera ConsultarViabilidadFinanciera(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAViabilidadFinanciera.ConsultarViabilidadFinanciera(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ViabilidadFinancieraBusiness", "ConsultarViabilidadFinanciera", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene un ViabilidadFinanciera
        /// </summary>
        /// <param name="pId">Identificador de ViabilidadFinanciera</param>
        /// <returns>Entidad ViabilidadFinanciera</returns>
        public ViabilidadFinanciera ConsultarViabilidadFin_Control(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAViabilidadFinanciera.ConsultarViabilidadFin_Control(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ViabilidadFinancieraBusiness", "ConsultarViabilidadFin_Control", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pViabilidadFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ViabilidadFinanciera obtenidos</returns>
        public List<ViabilidadFinanciera> ListarViabilidadFinanciera(ViabilidadFinanciera pViabilidadFinanciera, Usuario pUsuario)
        {
            try
            {
                return DAViabilidadFinanciera.ListarViabilidadFinanciera(pViabilidadFinanciera, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ViabilidadFinancieraBusiness", "ListarViabilidadFinanciera", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pViabilidadFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ViabilidadFinanciera obtenidos</returns>
        public List<ViabilidadFinanciera> ListarViabilidadFinancieraRepo(ViabilidadFinanciera pViabilidadFinanciera, Usuario pUsuario)
        {
            try
            {
                return DAViabilidadFinanciera.ListarViabilidadFinancieraRepo(pViabilidadFinanciera, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ViabilidadFinancieraBusiness", "ListarViabilidadFinancieraRepo", ex);
                return null;
            }
        }





    }
}