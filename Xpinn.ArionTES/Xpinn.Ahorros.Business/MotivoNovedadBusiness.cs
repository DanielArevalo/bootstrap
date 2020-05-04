using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Ahorros.Data;
using Xpinn.Ahorros.Entities;

namespace Xpinn.Ahorros.Business
{
    /// <summary>
    /// Objeto de negocio para MotivoNovedad
    /// </summary>
    public class MotivoNovedadBusiness : GlobalBusiness
    {
        private MotivoNovedadData DAMotivoNovedad;

        /// <summary>
        /// Constructor del objeto de negocio para MotivoNovedad
        /// </summary>
        public MotivoNovedadBusiness()
        {
            DAMotivoNovedad = new MotivoNovedadData();
        }

        /// <summary>
        /// Crea un MotivoNovedad
        /// </summary>
        /// <param name="pMotivoNovedad">Entidad MotivoNovedad</param>
        /// <returns>Entidad MotivoNovedad creada</returns>
        public MotivoNovedad CrearMotivoNovedad(MotivoNovedad pMotivoNovedad, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pMotivoNovedad = DAMotivoNovedad.CrearMotivoNovedad(pMotivoNovedad, pUsuario);
                    ts.Complete();
                }

                return pMotivoNovedad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoNovedadBusiness", "CrearMotivoNovedad", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un MotivoNovedad
        /// </summary>
        /// <param name="pMotivoNovedad">Entidad MotivoNovedad</param>
        /// <returns>Entidad MotivoNovedad modificada</returns>
        public MotivoNovedad ModificarMotivoNovedad(MotivoNovedad pMotivoNovedad, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pMotivoNovedad = DAMotivoNovedad.ModificarMotivoNovedad(pMotivoNovedad, pUsuario);

                    ts.Complete();
                }

                return pMotivoNovedad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoNovedadBusiness", "ModificarMotivoNovedad", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un MotivoNovedad
        /// </summary>
        /// <param name="pId">Identificador de MotivoNovedad</param>
        public void EliminarMotivoNovedad(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAMotivoNovedad.EliminarMotivoNovedad(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoNovedadBusiness", "EliminarMotivoNovedad", ex);
            }
        }

        /// <summary>
        /// Obtiene un MotivoNovedad
        /// </summary>
        /// <param name="pId">Identificador de MotivoNovedad</param>
        /// <returns>Entidad MotivoNovedad</returns>
        public MotivoNovedad ConsultarMotivoNovedad(Int64 pId, Usuario vUsuario)
        {
            try
            {
                MotivoNovedad MotivoNovedad = new MotivoNovedad();
                MotivoNovedad = DAMotivoNovedad.ConsultarMotivoNovedad(pId, vUsuario);
                return MotivoNovedad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoNovedadBusiness", "ConsultarMotivoNovedad", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pMotivoNovedad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MotivoNovedad obtenidos</returns>
        public List<MotivoNovedad> ListarMotivoNovedad(MotivoNovedad pMotivoNovedad, Usuario pUsuario)
        {
            try
            {
                return DAMotivoNovedad.ListarMotivoNovedad(pMotivoNovedad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoNovedadBusiness", "ListarMotivoNovedad", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DAMotivoNovedad.ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 1;
            }
        }

    }
}