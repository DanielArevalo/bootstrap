using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.ActivosFijos.Data;
using Xpinn.ActivosFijos.Entities;



namespace Xpinn.ActivosFijos.Business
{
    /// <summary>
    /// Objeto de negocio para ActivosFijos
    /// </summary>
    public class TipoArticuloBusiness : GlobalBusiness
    {
        private TipoArticuloData DATipoArticulo;

        /// <summary>
        /// Constructor del objeto de negocio para ActivosFijos
        /// </summary>
        public TipoArticuloBusiness()
        {
            DATipoArticulo = new TipoArticuloData();
        }

        /// <summary>
        /// Crea un ActivosFijos
        /// </summary>
        /// <param name="pActivosFijos">Entidad ActivosFijos</param>
        /// <returns>Entidad ActivosFijos creada</returns>
        public TipoArticulo CrearTipoArticulo(TipoArticulo pTipoArticulo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoArticulo = DATipoArticulo.CrearTipoArticulo(pTipoArticulo, pUsuario);

                    ts.Complete();
                }

                return pTipoArticulo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreasBusiness", "Areas", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un ActivosFijos
        /// </summary>
        /// <param name="pActivosFijos">Entidad ActivosFijos</param>
        /// <returns>Entidad ActivosFijos modificada</returns>
        public TipoArticulo ModificarTipoArticulo(TipoArticulo pTipoArticulo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoArticulo = DATipoArticulo.ModificarTipoArticulo(pTipoArticulo, pUsuario);


                    ts.Complete();
                }

                return pTipoArticulo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijosBusiness", "ModificarActivosFijos", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un ActivosFijos
        /// </summary>
        /// <param name="pId">Identificador de ActivosFijos</param>
        public void EliminarTipoArticulo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATipoArticulo.EliminarTipoArticulo(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijosBusiness", "EliminarActivosFijos", ex);
            }
        }

        /// <summary>
        /// Obtiene un ActivosFijos
        /// </summary>
        /// <param name="pId">Identificador de ActivosFijos</param>
        /// <returns>Entidad ActivosFijos</returns>
        public TipoArticulo ConsultarTipoArticulo(Int64 pId, Usuario vUsuario)
        {
            try
            {
                TipoArticulo TipoArticulo = new TipoArticulo();

                TipoArticulo = DATipoArticulo.ConsultarTipoArticulo(pId, vUsuario);



                return TipoArticulo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijosBusiness", "ConsultarActivosFijos", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pActivosFijos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ActivosFijos obtenidos</returns>
        public List<TipoArticulo> ListarTipoArticulo(TipoArticulo pTipoArticulo, Usuario pUsuario)
        {
            try
            {
                return DATipoArticulo.ListarTipoArticulo(pTipoArticulo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijosBusiness", "ListarActivosFijos", ex);
                return null;
            }
        }









        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DATipoArticulo.ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 1;
            }
        }






    }
}